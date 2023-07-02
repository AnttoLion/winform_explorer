using System.Data.SqlClient;

namespace mjc_dev.config
{
    public abstract class DbConnection
    {

        private readonly string connectionString;

        public DbConnection()
        {
            //connectionString = @"Server=tcp:s11.everleap.com;Initial Catalog=DB_7153_mjcdev;User ID=DB_7153_mjcdev_user;Password=Drew-Cubicle5-Guru;Integrated Security=False";
            connectionString = @"Server=DESKTOP-GI6T9UD\MSSQLSERVER01;DataBase=DB_7153_mjcdev;integrated security= true";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
