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
    public struct PriceTierData
    {
        public int id { get; set; }
        public string name { get; set; }
        public double profitMargin { get; set; }
        public string priceTierCode { get; set; }

        public PriceTierData(int _id, string _name, double _profitMargin, string _priceTierCode)
        {
            id = _id;
            name = _name;
            profitMargin = _profitMargin;
            priceTierCode = _priceTierCode;
        }
    }

    public class PriceTiersModel : DbConnection
    {
        public List<PriceTierData> PriceTierDataList { get; private set; }

        public bool LoadPriceTierData(string filter)
        {
            PriceTierDataList = new List<PriceTierData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, name, profitMargin, priceTierCode
                                            from dbo.PriceTiers";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, name, profitMargin, priceTierCode
                                                from dbo.PriceTiers
                                                where name like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PriceTierDataList.Add(
                            new PriceTierData((int)reader[0], reader[1].ToString(), Convert.ToDouble(reader[2]), reader[3].ToString())
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddPriceTier(string name, double profitmargin, string pricetiercode)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO dbo.PriceTiers (name, profitMargin, priceTierCode, createdAt, createdBy, updatedAt, updatedBy) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7)";
                    command.Parameters.AddWithValue("@Value1", name);
                    command.Parameters.AddWithValue("@Value2", profitmargin);
                    command.Parameters.AddWithValue("@Value3", pricetiercode);
                    command.Parameters.AddWithValue("@Value4", DateTime.Now);
                    command.Parameters.AddWithValue("@Value5", 1);
                    command.Parameters.AddWithValue("@Value6", DateTime.Now);
                    command.Parameters.AddWithValue("@Value7", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New PriceTier inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdatePriceTier(string name, double profitmargin, string pricetiercode, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.PriceTiers SET name = @Value1, profitMargin = @Value2, priceTierCode = @Value3 WHERE id = @Value4";
                    command.Parameters.AddWithValue("@Value1", name);
                    command.Parameters.AddWithValue("@Value2", profitmargin);
                    command.Parameters.AddWithValue("@Value3", pricetiercode);
                    command.Parameters.AddWithValue("@Value4", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The PriceTier updated successfully.");
                }

                return true;
            }
        }

        public bool DeletePriceTier(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "DELETE FROM dbo.PriceTiers WHERE id = @Value1";
                    command.Parameters.AddWithValue("@Value1", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The PriceTier was deleted.");
                }

                return true;
            }
        }

        public List<KeyValuePair<int, string>> GetPriceTierItems()
        {
            List<KeyValuePair<int, string>> PriceTierList = new List<KeyValuePair<int, string>>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, name
                                            from dbo.PriceTiers";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PriceTierList.Add(
                            new KeyValuePair<int, string>((int)reader[0], reader[1].ToString())
                        );
                    }
                    reader.Close();
                }
            }
            return PriceTierList;
        }
        public List<KeyValuePair<string, double>> GetPriceTierMargin(int _categoryId)
        {
            List <KeyValuePair<string, double>> PriceTierList = new List<KeyValuePair<string, double>>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select t1.name,t2.margin
                                            from dbo.PriceTiers as t1
                                            LEFT JOIN dbo.CategoryPriceTiers as t2 ON t2.priceTierId = t1.id
                                            where t2.categoryId = @categoryId";
                    command.Parameters.AddWithValue("@categoryId", _categoryId);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PriceTierList.Add(
                            new KeyValuePair<string, double>(reader[0].ToString(), Convert.ToDouble(reader[1]))
                        );
                    }
                    reader.Close();
                }
            }
            return PriceTierList;
        }
    }
}
