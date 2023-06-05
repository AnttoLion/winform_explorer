using System.Data.SqlClient;

namespace mjc_dev.config
{
    public abstract class DbConnection
    {
        private readonly string connectionString;

        public DbConnection()
        {
            connectionString = @"Server=DESKTOP-GI6T9UD\MSSQLSERVER01;DataBase=dbo;integrated security= true";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
