using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModzExercice.CoreData.Entities;

namespace ModzExercice.Services.Interfaces
{
    public interface IImportService
    {
        ExportEntities Process(Stream stream, ILogger logger);
        byte[] Write(ExportEntities exportEntities);
    }
}
