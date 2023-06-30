using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.config;

namespace mjc_dev.model
{
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

    public class SKUModel : DbConnection
    {

        public SKUModel() { }

        public List<SKUDetail> SKUDataList { get; private set; }

        public bool LoadSKUData(string filter, bool archived)
        {
            SKUDataList = new List<SKUDetail>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select S_Table.id, sku, C_Table.categoryName, description, qtyAvailable, qtyReorder
                                            from dbo.SKU as S_Table
                                            inner join dbo.Categories C_Table on category = C_Table.id 
                                            
                                            ";

                    if (archived) command.CommandText += " where S_Table.archived = 1";

                    if (filter != "")
                    {
                        command.CommandText = @"select S_Table.id, sku, C_Table.categoryName, description, qtyAvailable, qtyReorder
                                                from dbo.SKU as S_Table
                                                inner join dbo.Categories C_Table on category = C_Table.id 
                                                where S_Table.description like @filter 
                                                or S_Table.sku like @filter
                                                or C_Table.categoryName like @filter";
                        command.Parameters.Add("@filter", System.Data.SqlDbType.VarChar).Value = "%" + filter + "%";
                    }

                    //MessageBox.Show(command.CommandText);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //MessageBox.Show(reader[0].ToString());
                        int.TryParse(reader[0].ToString(), out int id);
                        string name = reader[1].ToString();
                        string desc = reader[2].ToString();
                        string category = reader[3].ToString();
                        int price = (int)reader[4];
                        int stock = (int)reader[5];

                        SKUDetail skuItem = new SKUDetail(id, name, desc, category, price, stock);
                        SKUDataList.Add(skuItem);
                    }
                    reader.Close();
                }
            }

            return true;
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

        public bool AddSKU(string sku__name,
            int category,
            string description,
            string measurement_unit,
            int weight,
            int cost_code,
            int asset_acct,
            bool taxable,
            bool maintain_qty,
            bool allow_discount,
            bool commissionable,
            int order_from,
            DateTime last_sold,
            string manufacturer,
            string location,
            int quantity,
            int qty_allocated,
            int qty_available,
            int critical_qty,
            int reorder_qty,
            int sold_this_month,
            int sold_ytd,
            bool freeze_prices,
            int core_cost,
            int inv_value,
            string memo,
            Dictionary<int, double> priceTierDict
            )
        {

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.SKU (active, sku, category, description, measurementUnit, weight, costCode, assetAccount, taxable, manageStock, allowDiscounts, commissionable, orderFrom, lastSold, manufacturer, location, quantity, qtyAllocated, qtyAvailable, qtyCritical, qtyReorder, soldMonthToDate, soldYearToDate, freezePrices, coreCost, inventoryValue, createdAt, createdBy, updatedAt, updatedBy, subassemblyStatus, subassemblyPrint, memo) OUTPUT INSERTED.ID VALUES (@active, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13, @Value14, @Value15, @Value16, @Value17, @Value18, @Value19, @Value20, @Value21, @Value22, @Value23, @Value24, @Value25, @Value26, @Value27, @Value28, @Value29, @Value30, @Value31, @memo)";
                    command.Parameters.AddWithValue("@active", true);
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
                    command.Parameters.AddWithValue("@memo", memo);

                    int newId = (int)command.ExecuteScalar();

                    foreach (KeyValuePair<int, double> pair in priceTierDict)
                    {
                        int key = pair.Key;
                        double value = pair.Value;

                        AddSKUPrice(newId, key, value);
                    }

                    MessageBox.Show("New SKU inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateSKU(string sku__name,
            int category,
            string description,
            string measurement_unit,
            int weight,
            int cost_code,
            int asset_acct,
            bool taxable,
            bool maintain_qty,
            bool allow_discount,
            bool commissionable,
            int order_from,
            DateTime last_sold,
            string manufacturer,
            string location,
            int quantity,
            int qty_allocated,
            int qty_available,
            int critical_qty,
            int reorder_qty,
            int sold_this_month,
            int sold_ytd,
            bool freeze_prices,
            int core_cost,
            int inv_value,
            string memo,
            Dictionary<int, double> priceTierDict,
            int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SKU SET sku = @Value1, category = @Value2, description = @Value3, measurementUnit = @Value4, weight = @Value5, costCode = @Value6, assetAccount = @Value7, taxable = @Value8, manageStock = @Value9, allowDiscounts = @Value10, commissionable = @Value11, orderFrom = @Value12, lastSold = @Value13, manufacturer = @Value14, location = @Value15, quantity = @Value16, qtyAllocated = @Value17, qtyAvailable = @Value18, qtyCritical = @Value19, qtyReorder = @Value20, soldMonthToDate = @Value21, soldYearToDate = @Value22, freezePrices = @Value23, coreCost = @Value24, inventoryValue = @Value25, memo = @memo WHERE id = @Value26";
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
                    command.Parameters.AddWithValue("@memo", memo);
                    command.Parameters.AddWithValue("@Value26", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("The Vendor updated successfully.");
                }

                return true;
            }
        }

        public bool UpdateSKUMemo(string sku__memo, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SKU SET memo = @Value1 WHERE id = @Value2";
                    command.Parameters.AddWithValue("@Value1", sku__memo);
                    command.Parameters.AddWithValue("@Value2", id);

                    command.ExecuteNonQuery();
                }

                return true;
            }
        }

        public bool UpdateSKUArchived(bool archived, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SKU SET archived = @Value1 WHERE id = @Value2";
                    command.Parameters.AddWithValue("@Value1", archived);
                    command.Parameters.AddWithValue("@Value2", id);

                    command.ExecuteNonQuery();
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

        public int GetQuantity(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select quantity from dbo.SKU where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    // Execute the command and retrieve the quantity value.
                    object result = command.ExecuteScalar();

                    // Convert the result to an int before returning it.
                    if (result == null || result == DBNull.Value)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
        }

        public bool UpdateQuantity(int quantity, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE dbo.SKU SET quantity = @Value1 WHERE id = @Value2";
                    command.Parameters.AddWithValue("@Value1", quantity);
                    command.Parameters.AddWithValue("@Value2", id);

                    command.ExecuteNonQuery();
                }

                return true;
            }
        }

        public List<KeyValuePair<int, string>> GetSKUItems()
        {
            List<KeyValuePair<int, string>> SKUList = new List<KeyValuePair<int, string>>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    SqlDataReader reader;

                    command.CommandText = @"select id, sku
                                            from dbo.sku";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        SKUList.Add(
                            new KeyValuePair<int, string>((int)reader[0], reader[1].ToString())
                        );
                    }
                    reader.Close();
                }
            }
            return SKUList;
        }
    }
}
