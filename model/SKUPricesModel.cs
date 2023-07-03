using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.config;

namespace mjc_dev.model
{
    internal class SKUPricesModel : DbConnection
    {
        public List<KeyValuePair<int, double>> LoadPriceTierDataBySKUId(int skuId)
        {
            List<KeyValuePair<int, double>> priceTierList = new List<KeyValuePair<int, double>>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;

                    command.Connection = connection;
                    // Get PriceTierList by SKUId
                    command.CommandText = @"select priceTierId, price 
                                            from dbo.SKUPrices
                                            where skuId = @Value1";
                    command.Parameters.AddWithValue("@Value1", skuId);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int.TryParse(reader[0].ToString(), out int priceTierId);
                        double.TryParse(reader[1].ToString(), out double price);

                        priceTierList.Add(new KeyValuePair<int, double>(priceTierId, price));
                    }
                }

                return priceTierList;
            }
        }

        public bool AddSKUPrice(int skuId, int priceTierId, double price)
        {

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.SKUPrices (active, skuId, priceTierId, price, createdAt, createdBy, updatedAt, updatedBy) VALUES (@active, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7)";
                    command.Parameters.AddWithValue("@active", true);
                    command.Parameters.AddWithValue("@Value1", skuId);
                    command.Parameters.AddWithValue("@Value2", priceTierId);
                    command.Parameters.AddWithValue("@Value3", price);
                    command.Parameters.AddWithValue("@Value4", DateTime.Now);
                    command.Parameters.AddWithValue("@Value5", 1);
                    command.Parameters.AddWithValue("@Value6", DateTime.Now);
                    command.Parameters.AddWithValue("@Value7", 1);
                    command.ExecuteNonQuery();
                }
                return true;
            }
        }

        public bool UpdateSKUPrice(int skuId, int priceTierId, double price)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    // Get PriceTierList by SKUId
                    command.CommandText = "Update dbo.SKUPrices SET price =@Value1  WHERE skuId = @Value2 AND priceTierId = @Value3";
                    command.Parameters.AddWithValue("@Value1", price);
                    command.Parameters.AddWithValue("@Value2", skuId);
                    command.Parameters.AddWithValue("@Value3", priceTierId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0) return true;
                    else return false;
                }
            }
        }
    }
}
