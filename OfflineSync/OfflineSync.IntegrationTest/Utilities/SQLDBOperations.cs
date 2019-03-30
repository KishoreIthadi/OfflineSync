using System.Configuration;
using System.Data.SqlClient;

namespace OfflineSync.IntegrationTest.Utilities
{
    static class SQLDBOperations
    {
        public static string _Conn = ConfigurationManager.ConnectionStrings["SyncConn"].ConnectionString;

        public static string GetGlobalTriggerName()
        {
            using (SqlConnection conn = new SqlConnection(_Conn))
            {
                SqlCommand cmd = new
                    SqlCommand("SELECT Name FROM sys.triggers WHERE Name = 'Trigger_Sync';"
                    , conn);
                conn.Open();

                return cmd.ExecuteScalar().ToString();
            }
        }
    }
}
