using System;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace WDT_Ass_1
{
    public sealed class SQLConnector
    {
        private static SQLConnector instance = null;

        private SQLConnector()
        {
        }
        private static IConfigurationRoot Configuration { get; } =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private string ConnectionString { get; } = Configuration["ConnectionString"];
        private SqlConnection CreateConnection(string connectionString) => new SqlConnection(connectionString);

        public SqlConnection GetConnection()
        {
            return CreateConnection(ConnectionString);
        }

        public static SQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new SQLConnector();
            }
            return instance;
        }
    }
}