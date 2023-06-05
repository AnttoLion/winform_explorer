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
    public struct RevenueByDate
    {
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public struct SKUDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int QtyAvail { get; set; }
        public int QtyTracking { get; set; }

        public SKUDetail(int id, string name, string category, string description, int qtyAvail, int qtyTracking)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            QtyAvail = qtyAvail;
            QtyTracking = qtyTracking;
        }
    }

    public struct CategoryData
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public int calculateAs { get; set; }

        public CategoryData(int _id, string _categoryName, int _calcuateAs)
        {
            id = _id;
            categoryName = _categoryName;
            calculateAs = _calcuateAs;
        }
    }

    public struct VendorData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phone { get; set; }

        public VendorData(int _id, string _name, string _city, string _state, string _phone)
        {
            id = _id;
            name = _name;
            city = _city;
            state = _state;
            phone = _phone;
        }
    }

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
    public class DashboardModel : DbConnection
    {
        #region Fields & Properties

        private DateTime startDate;
        private DateTime endDate;
        private int numberDays;
        public int NumCustomers { get; private set; }
        public int NumSuppliers { get; private set; }
        public int NumProducts { get; private set; }
        public List<KeyValuePair<string, int>> TopProductsList { get; private set; }
        public List<KeyValuePair<string, int>> UnderstockList { get; private set; }
        public List<RevenueByDate> GrossRevenueList { get; private set; }
        public int NumOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }

        public List<SKUDetail> SKUDataList { get; private set; }
        public List<CategoryData> CategoryDataList { get; private set; }
        public List<VendorData> VendorDataList { get; private set; }
        public List<PriceTierData> PriceTierDataList { get; private set; }
        public List<CustomerData> CustomerDataList { get; private set; }

        #endregion Fields & Properties

        #region Constructor

        public DashboardModel()
        {
        }

        #endregion Constructor

        #region Private methods

        private void GetNumberItems()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "select count(id) from Customer";
                    NumCustomers = (int)command.ExecuteScalar();
                    //Get Total Number of Suppliers
                    command.CommandText = "select count(id) from Supplier";
                    NumSuppliers = (int)command.ExecuteScalar();
                    //Get Total Number of Products
                    command.CommandText = "select count(id) from Product";
                    NumProducts = (int)command.ExecuteScalar();
                    //Get Total Number of Orders
                    command.CommandText = @"select count(id) from [Order]" +
                                            "where OrderDate between  @fromDate and @toDate";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    NumOrders = (int)command.ExecuteScalar();
                }
            }
        }
        private void GetProductAnalisys()
        {
            TopProductsList = new List<KeyValuePair<string, int>>();
            UnderstockList = new List<KeyValuePair<string, int>>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    command.Connection = connection;
                    //Get Top 5 products
                    command.CommandText = @"select top 5 P.ProductName, sum(OrderItem.Quantity) as Q
                                            from OrderItem
                                            inner join Product P on P.Id = OrderItem.ProductId
                                            inner
                                            join [Order] O on O.Id = OrderItem.OrderId
                                            where OrderDate between @fromDate and @toDate
                                            group by P.ProductName
                                            order by Q desc ";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TopProductsList.Add(
                            new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
                    }
                    reader.Close();
                    //Get Understock
                    command.CommandText = @"select ProductName, Stock
                                            from Product
                                            where Stock <= 6 and IsDiscontinued = 0";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        UnderstockList.Add(
                            new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
                    }
                    reader.Close();
                }
            }
        }
        private void GetOrderAnalisys()
        {
            GrossRevenueList = new List<RevenueByDate>();
            TotalProfit = 0;
            TotalRevenue = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select OrderDate, sum(TotalAmount)
                                            from[Order]
                                            where OrderDate between @fromDate and @toDate
                                            group by OrderDate";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    var reader = command.ExecuteReader();
                    var resultTable = new List<KeyValuePair<DateTime, decimal>>();
                    while (reader.Read())
                    {
                        resultTable.Add(
                            new KeyValuePair<DateTime, decimal>((DateTime)reader[0], (decimal)reader[1])
                            );
                        TotalRevenue += (decimal)reader[1];
                    }
                    TotalProfit = TotalRevenue * 0.2m;//20%
                    reader.Close();
                    //Group by Hours
                    if (numberDays <= 1)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("hh")
                                            into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Days
                    else if (numberDays <= 30)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("dd MMM")
                                            into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Weeks
                    else if (numberDays <= 92)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                            into order
                                            select new RevenueByDate
                                            {
                                                Date = "Week " + order.Key.ToString(),
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Months
                    else if (numberDays <= (365 * 2))
                    {
                        bool isYear = numberDays <= 365 ? true : false;
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("MMM yyyy")
                                            into order
                                            select new RevenueByDate
                                            {
                                                Date = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Years
                    else
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("yyyy")
                                            into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                }
            }
        }

        #endregion Private methods

        #region Public methods

        public bool LoadData(DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day,
                endDate.Hour, endDate.Minute, 59);
            if (startDate != this.startDate || endDate != this.endDate)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                this.numberDays = (endDate - startDate).Days;
                GetNumberItems();
                GetProductAnalisys();
                GetOrderAnalisys();
                Console.WriteLine("Refreshed data: {0} - {1}", startDate.ToString(), endDate.ToString());
                return true;
            }
            else
            {
                Console.WriteLine("Data not refreshed, same query: {0} - {1}", startDate.ToString(), endDate.ToString());
                return false;
            }
        }

        public bool LoadSKUData(string filter)
        {
            SKUDataList = new List<SKUDetail>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select S_Table.id, sku, C_Table.categoryName, description, qtyAvailable, quantity
                                            from dbo.SKU as S_Table
                                            inner join dbo.Categories C_Table on category = C_Table.id
                                            ";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, sku, category, description, qtyAvailable, quantity
                                                from dbo.SKU
                                                where description like @filter or sku like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        SKUDataList.Add(
                            new SKUDetail((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], (int)reader[5])
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddSKU(string sku__name, int category, string description, string measurement_unit, int weight, int cost_code, int asset_acct, bool taxable, bool maintain_qty, bool allow_discount, bool commissionable, int order_from, DateTime last_sold, string manufacturer, string location, int quantity, int qty_allocated, int qty_available, int critical_qty, int reorder_qty, int sold_this_month, int sold_ytd, bool freeze_prices, int core_cost, int inv_value, int price_tier1, int price_tier2)
        {

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.SKU (sku, category, description, measurementUnit, weight, costCode, assetAccount, taxable, manageStock, allowDiscounts, commissionable, orderFrom, lastSold, manufacturer, location, quantity, qtyAllocated, qtyAvailable, qtyCritical, qtyReorder, soldMonthToDate, soldYearToDate, freezePrices, coreCost, inventoryValue, createdAt, createdBy, updatedAt, updatedBy, subassemblyStatus, subassemblyPrint) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13, @Value14, @Value15, @Value16, @Value17, @Value18, @Value19, @Value20, @Value21, @Value22, @Value23, @Value24, @Value25, @Value26, @Value27, @Value28, @Value29, @Value30, @Value31)";
                    command.Parameters.AddWithValue("@Value1", sku__name);
                    command.Parameters.AddWithValue("@Value2", category);
                    command.Parameters.AddWithValue("@Value3", description);
                    command.Parameters.AddWithValue("@Value4", measurement_unit);
                    command.Parameters.AddWithValue("@Value5", weight);
                    command.Parameters.AddWithValue("@Value6", cost_code);
                    command.Parameters.AddWithValue("@Value7", asset_acct);
                    command.Parameters.AddWithValue("@Value8", taxable);
                    command.Parameters.AddWithValue("@Value9", maintain_qty);
                    command.Parameters.AddWithValue("@Value10", allow_discount);
                    command.Parameters.AddWithValue("@Value11", commissionable);
                    command.Parameters.AddWithValue("@Value12", order_from);
                    command.Parameters.AddWithValue("@Value13", last_sold);
                    command.Parameters.AddWithValue("@Value14", manufacturer);
                    command.Parameters.AddWithValue("@Value15", location);
                    command.Parameters.AddWithValue("@Value16", quantity);
                    command.Parameters.AddWithValue("@Value17", qty_allocated);
                    command.Parameters.AddWithValue("@Value18", qty_available);
                    command.Parameters.AddWithValue("@Value19", critical_qty);
                    command.Parameters.AddWithValue("@Value20", reorder_qty);
                    command.Parameters.AddWithValue("@Value21", sold_this_month);
                    command.Parameters.AddWithValue("@Value22", sold_ytd);
                    command.Parameters.AddWithValue("@Value23", freeze_prices);
                    command.Parameters.AddWithValue("@Value24", core_cost);
                    command.Parameters.AddWithValue("@Value25", inv_value);
                    command.Parameters.AddWithValue("@Value26", DateTime.Now);
                    command.Parameters.AddWithValue("@Value27", 1);
                    command.Parameters.AddWithValue("@Value28", DateTime.Now);
                    command.Parameters.AddWithValue("@Value29", 1);
                    command.Parameters.AddWithValue("@Value30", false);
                    command.Parameters.AddWithValue("@Value31", false);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New SKU inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateSKU(string sku__name, int category, string description, string measurement_unit, int weight, int cost_code, int asset_acct, bool taxable, bool maintain_qty, bool allow_discount, bool commissionable, int order_from, DateTime last_sold, string manufacturer, string location, int quantity, int qty_allocated, int qty_available, int critical_qty, int reorder_qty, int sold_this_month, int sold_ytd, bool freeze_prices, int core_cost, int inv_value, int price_tier1, int price_tier2, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SKU SET sku = @Value1, category = @Value2, description = @Value3, measurementUnit = @Value4, weight = @Value5, costCode = @Value6, assetAccount = @Value7, taxable = @Value8, manageStock = @Value9, allowDiscounts = @Value10, commissionable = @Value11, orderFrom = @Value12, lastSold = @Value13, manufacturer = @Value14, location = @Value15, quantity = @Value16, qtyAllocated = @Value17, qtyAvailable = @Value18, qtyCritical = @Value19, qtyReorder = @Value20, soldMonthToDate = @Value21, soldYearToDate = @Value22, freezePrices = @Value23, coreCost = @Value24,inventoryValue = @Value25 WHERE id = @Value26";
                    command.Parameters.AddWithValue("@Value1", sku__name);
                    command.Parameters.AddWithValue("@Value2", category);
                    command.Parameters.AddWithValue("@Value3", description);
                    command.Parameters.AddWithValue("@Value4", measurement_unit);
                    command.Parameters.AddWithValue("@Value5", weight);
                    command.Parameters.AddWithValue("@Value6", cost_code);
                    command.Parameters.AddWithValue("@Value7", asset_acct);
                    command.Parameters.AddWithValue("@Value8", taxable);
                    command.Parameters.AddWithValue("@Value9", maintain_qty);
                    command.Parameters.AddWithValue("@Value10", allow_discount);
                    command.Parameters.AddWithValue("@Value11", commissionable);
                    command.Parameters.AddWithValue("@Value12", order_from);
                    command.Parameters.AddWithValue("@Value13", last_sold);
                    command.Parameters.AddWithValue("@Value14", manufacturer);
                    command.Parameters.AddWithValue("@Value15", location);
                    command.Parameters.AddWithValue("@Value16", quantity);
                    command.Parameters.AddWithValue("@Value17", qty_allocated);
                    command.Parameters.AddWithValue("@Value18", qty_available);
                    command.Parameters.AddWithValue("@Value19", critical_qty);
                    command.Parameters.AddWithValue("@Value20", reorder_qty);
                    command.Parameters.AddWithValue("@Value21", sold_this_month);
                    command.Parameters.AddWithValue("@Value22", sold_ytd);
                    command.Parameters.AddWithValue("@Value23", freeze_prices);
                    command.Parameters.AddWithValue("@Value24", core_cost);
                    command.Parameters.AddWithValue("@Value25", inv_value);
                    command.Parameters.AddWithValue("@Value26", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Vendor updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteSKU(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "DELETE FROM dbo.SKU WHERE id = @Value1";
                    command.Parameters.AddWithValue("@Value1", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The SKU was deleted.");
                }

                return true;
            }
        }

        public List<dynamic> GetSKUData(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    List<dynamic> returnList = new List<dynamic>();

                    command.Connection = connection;

                    command.CommandText = @"select * from dbo.SKU where id = @id";
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
        public bool LoadCategoryData(string filter)
        {
            CategoryDataList = new List<CategoryData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, categoryName, calculateAs
                                            from dbo.Categories";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, categoryName, calculateAs
                                                from dbo.Categories
                                                where categoryName like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoryDataList.Add(
                            new CategoryData((int)reader[0], reader[1].ToString(), (int)reader[2])
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddCategory(string category_name, int calc)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.Categories (categoryName, calculateAs, createdAt, createdBy, updatedAt, updatedBy) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6)";
                    command.Parameters.AddWithValue("@Value1", category_name);
                    command.Parameters.AddWithValue("@Value2", calc);
                    command.Parameters.AddWithValue("@Value3", DateTime.Now);
                    command.Parameters.AddWithValue("@Value4", 1);
                    command.Parameters.AddWithValue("@Value5", DateTime.Now);
                    command.Parameters.AddWithValue("@Value6", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New Category inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateCategory(string category_name, int calc, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.Categories SET categoryName = @Value1, calculateAs = @Value2 WHERE id = @Value3";
                    command.Parameters.AddWithValue("@Value1", category_name);
                    command.Parameters.AddWithValue("@Value2", calc);
                    command.Parameters.AddWithValue("@Value3", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Category updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "DELETE FROM dbo.Categories WHERE id = @Value1";
                    command.Parameters.AddWithValue("@Value1", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Category was deleted.");
                }

                return true;
            }
        }


        public bool LoadVendorData(string filter)
        {
            VendorDataList = new List<VendorData>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, vendorName, city, state, businessPhone
                                            from dbo.Vendors";
                    if (filter != "")
                    {
                        command.CommandText = @"select id, vendorName, city, state, businessPhone
                                                from dbo.Vendors
                                                where vendorName like @filter or city like @filter or state like @filter or businessPhone like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        VendorDataList.Add(
                            new VendorData((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString())
                        );
                    }
                    reader.Close();
                }
            }

            return true;
        }

        public bool AddVendor(string vendor_name, string address1, string address2, string city, string state, string zipcode, string business_phone, string fax)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO dbo.Vendors (archived, vendorName, address1, address2, city, state, zipcode, businessPhone, fax, createdAt, createdBy, updatedAt, updatedBy) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13)";
                    command.Parameters.AddWithValue("@Value1", true);
                    command.Parameters.AddWithValue("@Value2", vendor_name);
                    command.Parameters.AddWithValue("@Value3", address1);
                    command.Parameters.AddWithValue("@Value4", address2);
                    command.Parameters.AddWithValue("@Value5", city);
                    command.Parameters.AddWithValue("@Value6", state);
                    command.Parameters.AddWithValue("@Value7", zipcode);
                    command.Parameters.AddWithValue("@Value8", business_phone);
                    command.Parameters.AddWithValue("@Value9", fax);
                    command.Parameters.AddWithValue("@Value10", DateTime.Now);
                    command.Parameters.AddWithValue("@Value11", 1);
                    command.Parameters.AddWithValue("@Value12", DateTime.Now);
                    command.Parameters.AddWithValue("@Value13", 1);

                    command.ExecuteNonQuery();

                    MessageBox.Show("New Vendor inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateVendor(string vendor_name, string address1, string address2, string city, string state, string zipcode, string business_phone, string fax, int id)
        {
            //            MessageBox.Show("asdf");
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.Vendors SET vendorName = @Value1, address1 = @Value2, address2 = @Value3, city = @Value4, state = @Value5, zipcode = @Value6, businessPhone = @Value7, fax = @Value8 WHERE id = @Value9";
                    command.Parameters.AddWithValue("@Value1", vendor_name);
                    command.Parameters.AddWithValue("@Value2", address1);
                    command.Parameters.AddWithValue("@Value3", address2);
                    command.Parameters.AddWithValue("@Value4", city);
                    command.Parameters.AddWithValue("@Value5", state);
                    command.Parameters.AddWithValue("@Value6", zipcode);
                    command.Parameters.AddWithValue("@Value7", business_phone);
                    command.Parameters.AddWithValue("@Value8", fax);
                    command.Parameters.AddWithValue("@Value9", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Vendor updated successfully.");
                }

                return true;
            }
        }

        public bool DeleteVendor(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "DELETE FROM dbo.Vendors WHERE id = @Value1";
                    command.Parameters.AddWithValue("@Value1", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Vendor was deleted.");
                }

                return true;
            }
        }

        public List<dynamic> GetVendorData(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    List<dynamic> returnList = new List<dynamic>();

                    command.Connection = connection;

                    command.CommandText = @"select * from dbo.Vendors where id = @id";
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
                    command.CommandText = @"UPDATE dbo.Customers SET customerNumber = @Value1, customerName = @Value2, address1 = @Value3, address2 = @Value4, city = @Value5, state = @Value6, zipcode = @Value7, businessPhone = @Value8, fax = @Value9, email = @Value10, dateOpened = @Value11, salesman = @Value12, resale = @Value13, statementCustomerNumber = @Value14, statementName = @Value15, priceTierId = @Value16, terms = @Value17, limit = @Value18, memo = @Value19, taxable = @Value20, sendStatements = @Value21, coreTracking = @Value22, coreBalance = @Value23,priceCoreTotal = @Value24, accountType = @Value25,poRequired = @Value26, creditCodeId = @Value27, interestRate = @Value28, accountBalance = @Value29, yearToDatePurchases = @Value30, yearToDateInterest = @Value31, dateLastPurchased = @Value32 WHERE id = @Value33";
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

                    MessageBox.Show("The Vendor was deleted.");
                }

                return true;
            }
        }

        public List<dynamic> GetCustomerData(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    List<dynamic> returnList = new List<dynamic>();

                    command.Connection = connection;

                    command.CommandText = @"select * from dbo.Customers where id = @id";
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

        public List<KeyValuePair<int, string>> GetCategoryItems()
        {
            List<KeyValuePair<int, string>> CategoryList = new List<KeyValuePair<int, string>>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, categoryName
                                            from dbo.Categories";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoryList.Add(
                            new KeyValuePair<int, string>((int)reader[0], reader[1].ToString())
                        );
                    }
                    reader.Close();
                }
            }
            return CategoryList;
        }
        #endregion Public methods
    }
}
