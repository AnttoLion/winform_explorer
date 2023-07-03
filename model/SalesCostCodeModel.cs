using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using mjc_dev.config;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace mjc_dev.model
{
    public struct SalesCostCodeData
    {
        public int id { get; set; }
        public string scCode { get; set; }
        public string salesAccount { get; set; }
        public string costAccount { get; set; }
        public string title { get; set; }
        public SalesCostCodeData(int _id, string _scCode, string _salesAccount, string _costAccount, string _title)
        {
            id = _id;
            scCode = _scCode;
            salesAccount = _salesAccount;
            costAccount = _costAccount;
            title = _title;
        }
    }

    public class SalesCostCodeModel : DbConnection
    {
        public List<SalesCostCodeData> SalesCostCodeDataList { get; private set; }

        public bool LoadSalesCostCodeData(string filter = "")
        {
            SalesCostCodeDataList = new List<SalesCostCodeData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, scCode, salesAccount, costAccount, title
                                            from dbo.SalesCostCodes";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, scCode, salesAccount, costAccount, title
                                                from dbo.SalesCostCodes
                                                where salesAccount like @filter OR costAccount like @filter OR title like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        SalesCostCodeDataList.Add(
                            new SalesCostCodeData((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString())
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddSalesCostCode(string _scCode, string _salesAccount, string _costAccount, string _title)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.SalesCostCodes (scCode, salesAccount, costAccount, title, createdAt, createdBy, updatedAt, updatedBy) VALUES (@scCode, @salesAccount, @costAccount, @title, @createdAt, @createdBy, @updatedAt, @updatedBy)";
                    command.Parameters.AddWithValue("@scCode", _scCode);
                    command.Parameters.AddWithValue("@salesAccount", _salesAccount);
                    command.Parameters.AddWithValue("@costAccount", _costAccount);
                    command.Parameters.AddWithValue("@title", _title);

                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    command.Parameters.AddWithValue("@createdBy", 1);
                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedBy", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New SalesCostCode inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateSalesCostCode(string _scCode, string _salesAccount, string _costAccount, string _title, int _id)
        {
//            MessageBox.Show(_scCode);
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SalesCostCodes SET scCode = @scCode, salesAccount = @salesAccount, costAccount = @costAccount, title = @title, updatedAt = @updatedAt, updatedBy = @updatedBy WHERE id = @id";
                    command.Parameters.AddWithValue("@scCode", _scCode);
                    command.Parameters.AddWithValue("@salesAccount", _salesAccount);
                    command.Parameters.AddWithValue("@costAccount", _costAccount);
                    command.Parameters.AddWithValue("@title", _title);

                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedBy", 1);

                    command.Parameters.AddWithValue("@id", _id);
                    command.ExecuteNonQuery();

                    MessageBox.Show("The SalesCostCode updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteSalesCostCode(int _id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "DELETE FROM dbo.SalesCostCodes WHERE id = @id";
                    command.Parameters.AddWithValue("@id", _id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The SalesCostCode was deleted.");
                }

                return true;
            }
        }

        public dynamic GetSalesCostCodeData(int _id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select id, scCode, salesAccount, costAccount, title from dbo.SalesCostCodes where id = @id";
                    command.Parameters.AddWithValue("@id", _id);

                    var reader = command.ExecuteReader();
                    if (reader.Read()) // check if there are any rows returned
                    {
                        // retrieve values from the reader
                        int readerId = (int)reader.GetValue(0);
                        string readerScCode = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        string readerSalesAccount = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        string readerCostAccount = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        string readerTitle = reader.IsDBNull(4) ? "" : reader.GetString(4);

                        // create an object to hold the customer data
                        var salesCostCode = new
                        {
                            id = readerId,
                            scCode = readerScCode,
                            salesAccount = readerSalesAccount,
                            costAccount = readerCostAccount,
                            title = readerTitle,
                        };

                        return salesCostCode;
                    }

                    // no rows returned
                    return null;
                }
            }
        }
    }
}
