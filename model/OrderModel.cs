using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjc_dev.model
{
    public struct OrderData
    {
        public int id { get; set; }
        public string sku { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public bool Tax { get; set; }
        public string Disc { get; set; }
        public float UnitPrice { get; set; }
        public float LineTotal { get; set; }

        
    }
    public class OrderModel
    {
    }
}
