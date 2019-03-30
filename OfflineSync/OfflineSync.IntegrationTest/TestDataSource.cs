using OfflineSync.Client.Enums;
using OfflineSync.Client.Models.SQLite;
using OfflineSync.DomainModel.Enums;
using User.Client.SQLiteModels;

namespace OfflineSync.IntegrationTest
{
    static class TestDataSource
    {
        public static SQLiteSyncSettingsModel ACTSAutoSyncFalseVal = new SQLiteSyncSettingsModel
        {
            AutoSync = false,
            ClientTableName = typeof(tblTestACTS).Name,
            ControllerData = null,
            ControllerRoute = null,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncClientToServer,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestACTS"
        };

        public static SQLiteSyncSettingsModel ACTSHAutoSyncFalseVal = new SQLiteSyncSettingsModel
        {
            AutoSync = false,
            ClientTableName = typeof(tblTestACTSH).Name,
            ControllerData = null,
            ControllerRoute = null,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncClientToServerAndHardDelete,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestACTSH"
        };

        public static SQLiteSyncSettingsModel AutoSyncFalseControllerRouteDataVal = new SQLiteSyncSettingsModel
        {
            AutoSync = false,
            ClientTableName = typeof(tblTestTWS).Name,
            ControllerData = null,
            ControllerRoute = null,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncTwoWay,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestTWS"
        };

        public static SQLiteSyncSettingsModel ACTSSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = true,
            ClientTableName = typeof(tblTestACTS).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncClientToServer,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestACTS",
            ControllerData = null,
            ControllerRoute = null
        };

        public static SQLiteSyncSettingsModel ACTSHSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = true,
            ClientTableName = typeof(tblTestACTSH).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncClientToServerAndHardDelete,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestACTSH",
            ControllerData = null,
            ControllerRoute = null
        };

        public static SQLiteSyncSettingsModel ASTCSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = true,
            ClientTableName = typeof(tblTestASTC).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncServerToClient,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestASTC",
            ControllerData = null,
            ControllerRoute = null
        };

        public static SQLiteSyncSettingsModel ATWSSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = true,
            ClientTableName = typeof(tblTestATWS).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncTwoWay,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestATWS",
            ControllerData = null,
            ControllerRoute = null
        };

        public static SQLiteSyncSettingsModel TWSSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = false,
            ClientTableName = typeof(tblTestTWS).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncTwoWay,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestTWS",
            ControllerData = "tblTestTWS",
            ControllerRoute = "Home/GetData"
        };

        public static SQLiteSyncSettingsModel STCSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = false,
            ClientTableName = typeof(tblTestSTC).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncServerToClient,
            ServerAssemblyName = "User.APIApp",
            ServerTableName = "tblTestSTC",
            ControllerData = "tblTestSTC",
            ControllerRoute = "Home/GetData"
        };
      
    }
}

