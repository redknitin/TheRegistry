using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRegistry.Model
{
    public class Transaction
    {
        public DateTime PerformedOn { get; set; }
        public string ItemCode { get; set; }
        public string StoreCode { get; set; }
        public decimal QuantityDelta { get; set; }
        public string Narration { get; set; }
        public decimal Amount { get; set; }
    }
}
