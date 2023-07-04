using Microsoft.Extensions.Logging;
using ModzExercice.CoreData.Entities;
using ModzExercice.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModzExercice.Services.Implementations
{
    public class ImportService : IImportService
    {

        private readonly ILogger<ImportService> _logger;
        private readonly List<ExportEntities> _exportEntitiesItems;

        public ImportService(ILogger<ImportService> logger)
        {
            _logger = logger;
            _exportEntitiesItems = new List<ExportEntities>();
        }

        //Read file and calculate values to show when OPERATIONTYPE = payment
        public ExportEntities Process(Stream stream, ILogger logger)
        {
            using var reader = new StreamReader(stream);
            string line;
            int cpt = 0;
            int linesOK = 0;
            var errorList = new List<string>();

            //Ignore header line
            reader.ReadLine();

            var exportEntities = new Dictionary<DateTime, ExportEntities>();

            while ((line = reader.ReadLine()) != null)
            {
                try
                {
                    var record = ParseData(line);

                    if (record.OperationType == "payment")
                    {
                        var date = record.Date.Date;

                        if (!exportEntities.TryGetValue(date, out var dataItem))
                        {
                            dataItem = new ExportEntities();
                            exportEntities[date] = dataItem;
                        }

                        dataItem.TotalAmount += record.Amount;
                        dataItem.TotalBillingFee += record.BillingFeesInclVat;
                        dataItem.Total3dsFee += record.SecureFee3D;
                        cpt++;
                        linesOK++;
                    }
                }
                catch (Exception ex)
                {
                    cpt++;
                    errorList.Add($"Échec du traitement de la ligne: {line}. Error: {ex.Message}");
                }
            }
            logger.LogInformation($"Nombre de lignes traitées : {cpt}");
            logger.LogInformation($"Nombre de lignes OK : {linesOK}");

            if (errorList.Count > 0)
            {
                logger.LogError($"Total errors: {errorList.Count}");
                foreach (var error in errorList)
                {
                    _logger.LogError(error);
                }
            }

            return new ExportEntities { ExportEntitiesItems = exportEntities };
        }

        //Write all data needed in new file
        public byte[] Write(ExportEntities exportEntities)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true);

            writer.WriteLine("Date;TotalAmount;TotalBillingFee;Total3dsFee");

            foreach (var item in exportEntities.ExportEntitiesItems)
            {
                writer.WriteLine($"{item.Key:yyyy/MM/dd HH:mm:ss};{item.Value.TotalAmount};{item.Value.TotalBillingFee};{item.Value.Total3dsFee}");
            }
            writer.Flush();
            return stream.ToArray();
        }

        //Data processing from file
        private ImportEntities ParseData(string line)
        {
            var column = line.Split(';');
            if (column.Length != 11)
            {
                throw new Exception("Format d'enregistrement invalide");
            }
            string format = "yyyy-MM-dd HH:mm:ss";

            var record = new ImportEntities
            {
                OrderId = column[0],
                Nature = column[1],
                OperationType = column[2],
                Amount = decimal.Parse(column[3], CultureInfo.InvariantCulture),
                Currency = column[4],
                BillingFeesInclVat = decimal.Parse(column[5], CultureInfo.InvariantCulture),
                Date = DateTime.ParseExact(column[6].Trim('"'), format, CultureInfo.InvariantCulture),
                ChargebackDate = string.IsNullOrEmpty(column[7]) ? null : (DateTime?)DateTime.Parse(column[7], CultureInfo.InvariantCulture),
                TransferReference = column[8],
                ExtraData = column[9],
                SecureFee3D = decimal.Parse(column[10], CultureInfo.InvariantCulture)
            };

            if (record.Amount <= 0)
            {
                throw new Exception("Invalid amount");
            }

            return record;
        }
    }
}
