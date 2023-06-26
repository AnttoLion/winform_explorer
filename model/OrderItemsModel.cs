using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjc_dev.model
{
    public struct OrderItemsData
    {
        public int id { get; set; }
        public string sku { get; set; }
        public double quantity { get; set; }
        public string description { get; set; }
        public bool tax { get; set; }
        public string disc { get; set; }
        public float unitPrice { get; set; }
        public float lineTotal { get; set; }
        public string SC { get; set; }

        public OrderItemsData(int _id, string _sku, double _quantity, string _description, bool _tax, string _disc, float _unitPrice, float _lineTotal, string _SC)
        {
            id = _id;
            sku = _sku;
            quantity = _quantity;
            description = _description;
            tax = _tax;
            disc = _disc;
            unitPrice = _unitPrice;
            lineTotal = _lineTotal;
            unitPrice = _unitPrice;
            SC = _SC;
        }
    }
    public class OrderItemsModel
    {
        
    }
}
