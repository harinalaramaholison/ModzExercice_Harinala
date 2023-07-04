using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModzExercice.CoreData.Entities
{
    public class ImportEntities
    {
        public string? OrderId { get; set; }
        public string? Nature { get; set; }
        public string? OperationType { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public decimal BillingFeesInclVat { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ChargebackDate { get; set; }
        public string? TransferReference { get; set; }
        public string? ExtraData { get; set; }
        public decimal SecureFee3D { get; set; }
    }
}
