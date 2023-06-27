using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.config;


namespace mjc_dev.model
{
    public struct OrderItemsList
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int skuId { get; set; }
        public string sku { get; set; }
        public double quantity { get; set; }
        public string description { get; set; }
        public bool tax { get; set; }
        public string disc { get; set; }
        public double unitPrice { get; set; }
        public double lineTotal { get; set; }
        public string SC { get; set; }

        public OrderItemsList(int _id, int _orderId, int _skuId, string _sku, double _quantity, string _description, bool _tax, string _disc, double _unitPrice, double _lineTotal, string _SC)
        {
            id = _id;
            orderId = _orderId;
            skuId = _skuId;
            sku = _sku;
            quantity = _quantity;
            description = _description;
            tax = _tax;
            disc = _disc;
            unitPrice = _unitPrice;
            lineTotal = _lineTotal;
            SC = _SC;
        }
    }
    public class OrderItemsModel : DbConnection
    {
        public List<OrderItemsList> OIList { get; private set; }

        public bool LoadOrderItemsList(string filter)
        {
            OIList = new List<OrderItemsList>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, orderId, skuId, sku, quantity, description, tax, unitPrice, lineTotal 
                                                from dbo.OrderItems";

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OIList.Add(
                            new OrderItemsList((int)reader[0], (int)reader[1], (int)reader[2], reader[3].ToString(), Convert.ToDouble(reader[4]),
                            reader[5].ToString(), (bool)reader[6], "", Convert.ToDouble(reader[7]), Convert.ToDouble(reader[8]), "" )
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }
    }
}
