using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using mjc_dev.config;
using System.Windows.Forms;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace mjc_dev.model
{
    public struct SalesTaxCodeData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string classification { get; set; }
        public double rate { get; set; }
        public SalesTaxCodeData(int _id, string _name, string _classification, double _rate)
        {
            id = _id;
            name = _name;
            classification = _classification;
            rate = _rate;
        }
    }

    public class SalesTaxCodeModel : DbConnection
    {
        public List<SalesTaxCodeData> SalesTaxCodeDataList { get; private set; }

        public bool LoadSalesTaxCodeData(string filter = "")
        {
            SalesTaxCodeDataList = new List<SalesTaxCodeData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, name, classification, rate
                                            from dbo.TaxCodes";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, name, classification, rate
                                                from dbo.TaxCodes
                                                where name like @filter OR classification like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        SalesTaxCodeDataList.Add(
                            new SalesTaxCodeData((int)reader[0], reader[1].ToString(), reader[2].ToString(), Convert.ToDouble(reader[3]))
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddSalesTaxCode(string _name, string _classification, double _rate)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.TaxCodes (name, classification, rate, createdAt, createdBy, updatedAt, updatedBy) VALUES (@name, @classification, @rate, @createdAt, @createdBy, @updatedAt, @updatedBy)";
                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@classification", _classification);
                    command.Parameters.AddWithValue("@rate", _rate);

                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    command.Parameters.AddWithValue("@createdBy", 1);
                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedBy", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New SalesTaxCode inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateSalesTaxCode(string _name, string _classification, double _rate, int _id)
        {
            //            MessageBox.Show(_scCode);
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.TaxCodes SET name = @name, classification = @classification, rate = @rate, updatedAt = @updatedAt, updatedBy = @updatedBy WHERE id = @id";
                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@classification", _classification);
                    command.Parameters.AddWithValue("@rate", _rate);

                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedBy", 1);

                    command.Parameters.AddWithValue("@id", _id);
                    command.ExecuteNonQuery();

                    MessageBox.Show("The SalesTaxCode updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteSalesTaxCode(int _id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "DELETE FROM dbo.TaxCodes WHERE id = @id";
                    command.Parameters.AddWithValue("@id", _id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The SalesTaxCode was deleted.");
                }

                return true;
            }
        }

        public dynamic GetSalesTaxCodeData(int _id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select id, name, classification, rate from dbo.TaxCodes where id = @id";
                    command.Parameters.AddWithValue("@id", _id);

                    var reader = command.ExecuteReader();
                    if (reader.Read()) // check if there are any rows returned
                    {
                        // retrieve values from the reader
                        int readerId = (int)reader.GetValue(0);
                        string readerName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        string readerClassification = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        double readerRate = reader.IsDBNull(3) ? 0 : Convert.ToDouble(reader[3]);

                        // create an object to hold the taxcode data
                        var SalesTaxCode = new
                        {
                            id = readerId,
                            name = readerName,
                            classification = readerClassification,
                            rate = readerRate,
                        };

                        return SalesTaxCode;
                    }

                    // no rows returned
                    return null;
                }
            }
        }
    }
}
