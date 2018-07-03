using OfflineSync.Client.Enums;

namespace OfflineSync.Client.Models
{
    public static class GlobalConfig
    {
        public static string APIUrl { get; set; }
        public static string DBPath { get; set; }
        public static string Token { get; set; }
        public static ClientDBType DBType { get; set; } = ClientDBType.SQLite;
    }
}