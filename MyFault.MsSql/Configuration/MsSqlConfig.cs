using System.Data.SqlClient;
using MyFault.Configuration;

namespace MyFault.MsSql.Configuration
{
    public static class MsSqlConfig
    {
        public static MyFaultConfig WithMsSqlData(this MyFaultConfig config, string connectionString)
        {
            config.WithDataProvider(new MsSqlEntryDataProvider(new SqlConnection(connectionString)));
            return config;
        }
        
        public static MyFaultConfig WithMsSqlData(this MyFaultConfig config, SqlConnection connection)
        {
            config.WithDataProvider(new MsSqlEntryDataProvider(connection));
            return config;
        }
    }
}