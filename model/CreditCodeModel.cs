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
    public struct CreditCodeData
    {
        public int id { get; set; }
        public string code { get; set; }
        public string paymentAllowed { get; set; }
        public string creditLimit { get; set; }
        public CreditCodeData(int _id, string _code, string _paymentAllowed, string _creditLimit)
        {
            id = _id;
            code = _code;
            paymentAllowed = _paymentAllowed;
            creditLimit = _creditLimit;
        }
    }

    public class CreditCodeModel : DbConnection
    {
        public List<CreditCodeData> CreditCodeDataList { get; private set; }

        public bool LoadCreditCodeData(string filter = "")
        {
            CreditCodeDataList = new List<CreditCodeData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, code, paymentAllowed, creditLimit
                                            from dbo.CreditCodes";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, code, paymentAllowed, creditLimit
                                                from dbo.CreditCodes
                                                where code like @filter OR paymentAllowed like @filter OR creditLimit like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CreditCodeDataList.Add(
                            new CreditCodeData((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString())
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddCreditCode(string _code, string _paymentAllowed, string _creditLimit)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.CreditCodes (code, paymentAllowed, creditLimit, createdAt, createdBy, updatedAt, updatedBy) VALUES (@code, @paymentAllowed, @creditLimit, @createdAt, @createdBy, @updatedAt, @updatedBy)";
                    command.Parameters.AddWithValue("@code", _code);
                    command.Parameters.AddWithValue("@paymentAllowed", _paymentAllowed);
                    command.Parameters.AddWithValue("@creditLimit", _creditLimit);

                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    command.Parameters.AddWithValue("@createdBy", 1);
                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedBy", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New CreditCode inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateCreditCode(string _code, string _paymentAllowed, string _creditLimit, int _id)
        {
            //            MessageBox.Show(_scCode);
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.CreditCodes SET code = @code, paymentAllowed = @paymentAllowed, creditLimit = @creditLimit, updatedAt = @updatedAt, updatedBy = @updatedBy WHERE id = @id";
                    command.Parameters.AddWithValue("@code", _code);
                    command.Parameters.AddWithValue("@paymentAllowed", _paymentAllowed);
                    command.Parameters.AddWithValue("@creditLimit", _creditLimit);

                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedBy", 1);

                    command.Parameters.AddWithValue("@id", _id);
                    command.ExecuteNonQuery();

                    MessageBox.Show("The CreditCode updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteCreditCode(int _id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "DELETE FROM dbo.CreditCodes WHERE id = @id";
                    command.Parameters.AddWithValue("@id", _id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The CreditCode was deleted.");
                }

                return true;
            }
        }

        public dynamic GetCreditCodeData(int _id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select id, code, paymentAllowed, creditLimit from dbo.CreditCodes where id = @id";
                    command.Parameters.AddWithValue("@id", _id);

                    var reader = command.ExecuteReader();
                    if (reader.Read()) // check if there are any rows returned
                    {
                        // retrieve values from the reader
                        int readerId = (int)reader.GetValue(0);
                        string readerCode = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        string readerPaymentAllowed = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        string readerCreditLimit = reader.IsDBNull(3) ? "" : reader.GetString(3);

                        // create an object to hold the taxcode data
                        var CreditCode = new
                        {
                            id = readerId,
                            code = readerCode,
                            paymentAllowed = readerPaymentAllowed,
                            creditLimit = readerCreditLimit,
                        };

                        return CreditCode;
                    }

                    // no rows returned
                    return null;
                }
            }
        }
    }
}
