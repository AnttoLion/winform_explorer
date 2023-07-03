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

    public class CategoriesModel : DbConnection
    {
        public List<CategoryData> CategoryDataList { get; private set; }

        private CategoryPriceTierModel CategoryPriceTierModelObj = new CategoryPriceTierModel();

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

        public bool AddCategory(string category_name, int calc, Dictionary<int, double> priceTierDict)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //Get Total Number of Customers
                    command.CommandText = "INSERT INTO dbo.Categories (categoryName, calculateAs, createdAt, createdBy, updatedAt, updatedBy) OUTPUT INSERTED.ID VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6)";
                    command.Parameters.AddWithValue("@Value1", category_name);
                    command.Parameters.AddWithValue("@Value2", calc);
                    command.Parameters.AddWithValue("@Value3", DateTime.Now);
                    command.Parameters.AddWithValue("@Value4", 1);
                    command.Parameters.AddWithValue("@Value5", DateTime.Now);
                    command.Parameters.AddWithValue("@Value6", 1);

                    int newId = (int)command.ExecuteScalar();

                    foreach (KeyValuePair<int, double> pair in priceTierDict)
                    {
                        int key = pair.Key;
                        double value = pair.Value;

                        CategoryPriceTierModelObj.AddCategoryPriceTier(newId, key, value);
                    }

                    MessageBox.Show("New Category inserted successfully.");
                }

                return true;
            }
        }

        public bool UpdateCategory(string category_name, int calc, Dictionary<int, double> priceTierDict, int id)
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

                    foreach (KeyValuePair<int, double> pair in priceTierDict)
                    {
                        int key = pair.Key;
                        double value = pair.Value;

                        if (!CategoryPriceTierModelObj.UpdateCategoryPriceTier(id, key, value)) CategoryPriceTierModelObj.AddCategoryPriceTier(id, key, value);
                    }

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

    }
}
