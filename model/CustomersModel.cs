using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using mjc_dev.config;
using static mjc_dev.forms.sku.SKUInformation;

namespace mjc_dev.model
{
    public struct CustomerData
    {
        public int id { get; set; }
        public string num { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phone { get; set; }

        public CustomerData(int _id, string _num, string _name, string _address, string _city, string _state, string _phone)
        {
            id = _id;
            num = _num;
            name = _name;
            address = _address;
            city = _city;
            state = _state;
            phone = _phone;
        }
    }

    public class CustomersModel : DbConnection
    {
        public int NumCustomers { get; private set; }
        public List<CustomerData> CustomerDataList { get; private set; }

        public bool LoadCustomerData(string filter)
        {
            CustomerDataList = new List<CustomerData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, customerNumber, customerName, address1, city, state, zipcode
                                            from dbo.Customers";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, customerNumber, customerName, address1, city, state, zipcode
                                                from dbo.Customers
                                                where customerNumber like @filter or customerName like @filter or address1 or city like @filter or state like @filter or zipcode like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CustomerDataList.Add(
                            new CustomerData((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString())
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddCustomer(string customer_num, string customer_name, string address1, string address2, string city, string state, string zipcode, string business_phone, string fax, string email, DateTime date_opened, string salesman, bool resale, string stmt_num, string stmt_name, int pricetier, string terms, string limit, string memo, bool taxable, bool send_stm, string core_tracking, double core_balance, bool print_core_tot, string acct_type, bool porequired, int credit_card, double interest_rate, double acct_balance, int ytd_purch, double ytd_interest, DateTime last_date_purch)
        {
            //MessageBox.Show(price)
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO dbo.Customers (active, customerNumber, customerName, address1, address2, city, state, zipcode, businessPhone, businessPhoneExtension, fax, homePhone, email, dateOpened, salesman, resale, taxable, sendStatements, statementCustomerNumber, statementName, priceTierId, terms, limit, coreTracking, coreBalance, priceCoreTotal, accountType, poRequired, creditCodeId, interestRate, accountBalance, yearToDatePurchases, yearToDateInterest, dateLastPurchased, memo, archived, createdAt, createdBy, updatedAt, updatedBy) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13, @Value14, @Value15, @Value16, @Value17, @Value18, @Value19, @Value20, @Value21, @Value22, @Value23, @Value24, @Value25, @Value26, @Value27, @Value28, @Value29, @Value30, @Value31, @Value32, @Value33, @Value34, @Value35, @Value36, @Value37, @Value38, @Value39, @Value40)";
                    command.Parameters.AddWithValue("@Value1", 1);
                    command.Parameters.AddWithValue("@Value2", customer_num);
                    command.Parameters.AddWithValue("@Value3", customer_name);
                    command.Parameters.AddWithValue("@Value4", address1);
                    command.Parameters.AddWithValue("@Value5", address2);
                    command.Parameters.AddWithValue("@Value6", city);
                    command.Parameters.AddWithValue("@Value7", state);
                    command.Parameters.AddWithValue("@Value8", zipcode);
                    command.Parameters.AddWithValue("@Value9", business_phone);
                    command.Parameters.AddWithValue("@Value10", "1");
                    command.Parameters.AddWithValue("@Value11", fax);
                    command.Parameters.AddWithValue("@Value12", "");
                    command.Parameters.AddWithValue("@Value13", email);
                    command.Parameters.AddWithValue("@Value14", date_opened.Date);
                    command.Parameters.AddWithValue("@Value15", salesman);
                    command.Parameters.AddWithValue("@Value16", resale);
                    command.Parameters.AddWithValue("@Value17", taxable);
                    command.Parameters.AddWithValue("@Value18", send_stm);
                    command.Parameters.AddWithValue("@Value19", stmt_num);
                    command.Parameters.AddWithValue("@Value20", stmt_name);
                    command.Parameters.AddWithValue("@Value21", pricetier);
                    command.Parameters.AddWithValue("@Value22", terms);
                    command.Parameters.AddWithValue("@Value23", limit);
                    command.Parameters.AddWithValue("@Value24", core_tracking);
                    command.Parameters.AddWithValue("@Value25", core_balance);
                    command.Parameters.AddWithValue("@Value26", print_core_tot);
                    command.Parameters.AddWithValue("@Value27", acct_type);
                    command.Parameters.AddWithValue("@Value28", porequired);
                    command.Parameters.AddWithValue("@Value29", credit_card);
                    command.Parameters.AddWithValue("@Value30", interest_rate);
                    command.Parameters.AddWithValue("@Value31", acct_balance);
                    command.Parameters.AddWithValue("@Value32", ytd_purch);
                    command.Parameters.AddWithValue("@Value33", ytd_interest);
                    command.Parameters.AddWithValue("@Value34", last_date_purch.Date);
                    command.Parameters.AddWithValue("@Value35", memo);
                    command.Parameters.AddWithValue("@Value36", true);

                    command.Parameters.AddWithValue("@Value37", DateTime.Now);
                    command.Parameters.AddWithValue("@Value38", 1);
                    command.Parameters.AddWithValue("@Value39", DateTime.Now);
                    command.Parameters.AddWithValue("@Value40", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New Customer inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateCustomer(string customer_num, string customer_name, string address1, string address2, string city, string state, string zipcode, string business_phone, string fax, string email, DateTime date_opened, string salesman, bool resale, string stmt_num, string stmt_name, int pricetier, string terms, string limit, string memo, bool taxable, bool send_stm, string core_tracking, double core_balance, bool print_core_tot, string acct_type, bool porequired, int credit_card, double interest_rate, double acct_balance, int ytd_purch, double ytd_interest, DateTime last_date_purch, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.Customers SET customerNumber = @Value1, customerName = @Value2, address1 = @Value3, address2 = @Value4, city = @Value5, state = @Value6, zipcode = @Value7, businessPhone = @Value8, fax = @Value9, email = @Value10, dateOpened = @Value11, salesman = @Value12, resale = @Value13, statementCustomerNumber = @Value14, statementName = @Value15, priceTierId = @Value16, terms = @Value17, limit = @Value18, memo = @Value19, taxable = @Value20, sendStatements = @Value21, coreTracking = @Value22, coreBalance = @Value23, priceCoreTotal = @Value24, accountType = @Value25,poRequired = @Value26, creditCodeId = @Value27, interestRate = @Value28, accountBalance = @Value29, yearToDatePurchases = @Value30, yearToDateInterest = @Value31, dateLastPurchased = @Value32 WHERE id = @Value33";
                    command.Parameters.AddWithValue("@Value1", customer_num);
                    command.Parameters.AddWithValue("@Value2", customer_name);
                    command.Parameters.AddWithValue("@Value3", address1);
                    command.Parameters.AddWithValue("@Value4", address2);
                    command.Parameters.AddWithValue("@Value5", city);
                    command.Parameters.AddWithValue("@Value6", state);
                    command.Parameters.AddWithValue("@Value7", zipcode);
                    command.Parameters.AddWithValue("@Value8", business_phone);
                    command.Parameters.AddWithValue("@Value9", fax);
                    command.Parameters.AddWithValue("@Value10", email);
                    command.Parameters.AddWithValue("@Value11", date_opened);
                    command.Parameters.AddWithValue("@Value12", salesman);
                    command.Parameters.AddWithValue("@Value13", resale);
                    command.Parameters.AddWithValue("@Value14", stmt_num);
                    command.Parameters.AddWithValue("@Value15", stmt_name);
                    command.Parameters.AddWithValue("@Value16", pricetier);
                    command.Parameters.AddWithValue("@Value17", terms);
                    command.Parameters.AddWithValue("@Value18", limit);
                    command.Parameters.AddWithValue("@Value19", memo);
                    command.Parameters.AddWithValue("@Value20", taxable);
                    command.Parameters.AddWithValue("@Value21", send_stm);
                    command.Parameters.AddWithValue("@Value22", core_tracking);
                    command.Parameters.AddWithValue("@Value23", core_balance);
                    command.Parameters.AddWithValue("@Value24", print_core_tot);
                    command.Parameters.AddWithValue("@Value25", acct_type);
                    command.Parameters.AddWithValue("@Value26", porequired);
                    command.Parameters.AddWithValue("@Value27", credit_card);
                    command.Parameters.AddWithValue("@Value28", interest_rate);
                    command.Parameters.AddWithValue("@Value29", acct_balance);
                    command.Parameters.AddWithValue("@Value30", ytd_purch);
                    command.Parameters.AddWithValue("@Value31", ytd_interest);
                    command.Parameters.AddWithValue("@Value32", last_date_purch);
                    command.Parameters.AddWithValue("@Value33", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Vendor updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteCustomer(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "DELETE FROM dbo.Customers WHERE id = @Value1";
                    command.Parameters.AddWithValue("@Value1", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Customer was deleted.");
                }

                return true;
            }
        }

        public List<KeyValuePair<int, string>> GetCustomerList()
        {
            List<KeyValuePair<int, string>> PriceTierList = new List<KeyValuePair<int, string>>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, customerNumber
                                            from dbo.Customers";
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

        public dynamic GetCustomerData(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select id, customerNumber, customerName, terms, zipcode, poRequired from dbo.Customers where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();
                    if (reader.Read()) // check if there are any rows returned
                    {
                        // retrieve values from the reader
                        int customerId = (int)reader.GetValue(0);
                        string customerNumber = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        string customerName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        string terms = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        string zipcode = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        string poRequired = reader.IsDBNull(5) ? "" : reader.GetString(5);

                        // create an object to hold the customer data
                        var customer = new
                        {
                            id = customerId,
                            customerNumber = customerNumber,
                            customerName = customerName,
                            terms = terms,
                            zipcode = zipcode,
                            poRequired = poRequired
                        };

                        return customer;
                    }

                    // no rows returned
                    return null;
                }
            }
        }
    }
}
