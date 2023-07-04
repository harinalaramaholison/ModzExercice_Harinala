using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModzExercice.CoreData.Entities
{
    public class ExportEntities
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalBillingFee { get; set; }
        public decimal Total3dsFee { get; set; }
        public Dictionary<DateTime, ExportEntities>? ExportEntitiesItems { get; set; }
    }
}
