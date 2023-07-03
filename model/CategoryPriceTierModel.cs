using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.config;

namespace mjc_dev.model
{
    internal class CategoryPriceTierModel : DbConnection
    {
        public List<KeyValuePair<int, double>> LoadPriceTierDataByCategoryId(int categoryId)
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
                    command.CommandText = @"select priceTierId, margin 
                                            from dbo.CategoryPriceTiers
                                            where categoryId = @Value1";
                    command.Parameters.AddWithValue("@Value1", categoryId);

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

        public bool AddCategoryPriceTier(int _categoryId, int _priceTierId, double _margin)
        {

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.CategoryPriceTiers (active, categoryId, priceTierId, margin, createdAt, createdBy, updatedAt, updatedBy) VALUES (@active, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7)";
                    command.Parameters.AddWithValue("@active", true);
                    command.Parameters.AddWithValue("@Value1", _categoryId);
                    command.Parameters.AddWithValue("@Value2", _priceTierId);
                    command.Parameters.AddWithValue("@Value3", _margin);
                    command.Parameters.AddWithValue("@Value4", DateTime.Now);
                    command.Parameters.AddWithValue("@Value5", 1);
                    command.Parameters.AddWithValue("@Value6", DateTime.Now);
                    command.Parameters.AddWithValue("@Value7", 1);
                    command.ExecuteNonQuery();
                }
                return true;
            }
        }

        public bool UpdateCategoryPriceTier(int _categoryId, int _priceTierId, double _margin)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    // Get PriceTierList by SKUId
                    command.CommandText = "Update dbo.CategoryPriceTiers SET margin =@Value1  WHERE categoryId = @Value2 AND priceTierId = @Value3";
                    command.Parameters.AddWithValue("@Value1", _margin);
                    command.Parameters.AddWithValue("@Value2", _categoryId);
                    command.Parameters.AddWithValue("@Value3", _priceTierId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0) return true;
                    else return false;
                }
            }
        }
    }
}
