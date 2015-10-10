using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRegistry.Model
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal StandardPrice { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
