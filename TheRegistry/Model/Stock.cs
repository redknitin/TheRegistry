using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRegistry.Model
{
    public class Stock
    {
        public string ItemCode { get; set; }
        public string StoreCode { get; set; }
        public decimal Quantity { get; set; }
    }
}
