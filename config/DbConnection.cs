using System.Data.SqlClient;

namespace mjc_dev.config
{
    public abstract class DbConnection
    {
        private readonly string connectionString;

        public DbConnection()
        {
            connectionString = @"Server=tcp:s11.everleap.com;Initial Catalog=DB_7153_mjcdev;User ID=DB_7153_mjcdev_user;Password=Drew-Cubicle5-Guru;Integrated Security=False";
            //connectionString = @"Server=tcp:s10.everleap.com;Initial Catalog=DB_7153_mjcprod;User ID=DB_7153_mjcprod_user;Password=!n8B6%x&NSCfbyh4ReLG;Integrated Security=False";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
