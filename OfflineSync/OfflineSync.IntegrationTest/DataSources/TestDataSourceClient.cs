using OfflineSync.Client.Enums;
using OfflineSync.Client.Models.SQLite;
using OfflineSync.DomainModel.Enums;
using System;
using User.Client.SQLiteModels;

namespace OfflineSync.IntegrationTest
{
    static class TestDataSourceClient
    {
        public static SQLiteSyncSettingsModel ACTSAutoSyncFalseVal = new SQLiteSyncSettingsModel
        {
            AutoSync = false,
            ClientTableName = typeof(tblTestACTS).Name,
            ControllerData = null,
            ControllerRoute = null,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncClientToServer,
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
            ServerTableName = "tblTestTWS"
        };

        public static SQLiteSyncSettingsModel ACTSSettings = new SQLiteSyncSettingsModel
        {
            AutoSync = true,
            ClientTableName = typeof(tblTestACTS).Name,
            Priority = OveridePriority.LastUpdated,
            SyncType = SyncType.SyncClientToServer,
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
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
            ServerAssemblyName = "User.Server.SQLModels",
            ServerTableName = "tblTestSTC",
            ControllerData = "tblTestSTC",
            ControllerRoute = "Home/GetData"
        };

        public static tblTestACTS ACTSRecordOne = new tblTestACTS
        {
            StringType = "Record One",
            IntType = 10,
            FloatType = 10.01f,
            DateType = null
        };

        public static tblTestACTS ACTSRecordTwo = new tblTestACTS
        {
            StringType = "Record Two",
            IntType = 100,
            FloatType = 99.9999393f,
            DateType = DateTime.Now
        };

        public static tblTestACTS ACTSRecordThree = new tblTestACTS
        {
            StringType = "Record Three",
            IntType = 1000,
            FloatType = 100.9399393233f,
            DateType = DateTime.Now
        };

        public static tblTestACTSH ACTSHRecordOne = new tblTestACTSH
        {
            StringType = "Record One",
            IntType = 10,
            FloatType = 10.01f,
            DateType = null
        };

        public static tblTestACTSH ACTSHRecordTwo = new tblTestACTSH
        {
            StringType = "Record Two",
            IntType = 10000,
            FloatType = 99.9999393f,
            DateType = DateTime.Now
        };

        public static tblTestACTSH ACTSHRecordThree = new tblTestACTSH
        {
            StringType = "Record Three",
            IntType = 592,
            FloatType = 100.9399393233f,
            DateType = DateTime.Now
        };
    }
}

