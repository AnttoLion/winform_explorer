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
    public struct SKUCrossRefData
    {
        public int id { get; set; }
        public string crossReference { get; set; }
        public string manufacturer { get; set; }
        public string sku { get; set; }
        public string description { get; set; }

        public SKUCrossRefData(int _id, string _crossReference, string _manufacturer, string _sku, string _description)
        {
            id = _id;
            crossReference = _crossReference;
            manufacturer = _manufacturer;
            sku = _sku;
            description = _description;
        }
    }

    public class SKUCrossRefModel : DbConnection
    {

        public List<SKUCrossRefData> SKUCrossRefList { get; private set; }

        public bool LoadSKUCrossRefData(int? SkuId = null)
        {
            SKUCrossRefList = new List<SKUCrossRefData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select t1.id, t1.crossReference, t2.manufacturer, t2.sku, t2.description 
                                            from dbo.SKUCrossReferences as t1 
                                            left join dbo.SKU as t2 
                                                on t1.SkuId = t2.id";

                    if(SkuId != null)
                    {
                        command.CommandText += " where t2.id = " + SkuId;
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        SKUCrossRefList.Add(
                            new SKUCrossRefData((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString())
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public List<dynamic> GetSKUCrossRef(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    List<dynamic> returnList = new List<dynamic>();

                    command.Connection = connection;

                    command.CommandText = @"select * from dbo.SKUCrossReferences where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i]);
                        }

                        returnList.Add(row);
                    }
                    reader.Close();

                    return returnList;
                }
            }
        }

        public bool AddSKUCrossRef(int SkuId, int vendorId, string crossReference, int createdBy)
        {

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO dbo.SKUCrossReferences (active, SkuId, vendorId, crossReference, createdAt, createdBy, updatedAt, updatedBy) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8)";
                    command.Parameters.AddWithValue("@Value1", true);
                    command.Parameters.AddWithValue("@Value2", SkuId);
                    command.Parameters.AddWithValue("@Value3", vendorId);
                    command.Parameters.AddWithValue("@Value4", crossReference);
                    command.Parameters.AddWithValue("@Value5", DateTime.Now);
                    command.Parameters.AddWithValue("@Value6", createdBy);
                    command.Parameters.AddWithValue("@Value7", DateTime.Now);
                    command.Parameters.AddWithValue("@Value8", createdBy);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New CrossReference inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateSKUCrossRef(int id, int SkuId, int vendorId, string crossReference, int updatedBy)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SKUCrossReferences SET SkuId = @Value1, vendorId = @Value2, crossReference = @Value3, updatedBy = @Value4 WHERE id = @Value5";
                    command.Parameters.AddWithValue("@Value1", SkuId);
                    command.Parameters.AddWithValue("@Value2", vendorId);
                    command.Parameters.AddWithValue("@Value3", crossReference);
                    command.Parameters.AddWithValue("@Value4", updatedBy);
                    command.Parameters.AddWithValue("@Value5", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The CrossReference updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteSKUCrossRef(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "DELETE FROM dbo.SKUCrossReferences WHERE id = @Value1";
                    command.Parameters.AddWithValue("@Value1", id);

                    command.ExecuteNonQuery();

                }

                return true;
            }
        }
    }
}
