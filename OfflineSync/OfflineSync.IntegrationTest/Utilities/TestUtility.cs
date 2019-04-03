using OfflineSync.Client.Models;
using OfflineSync.Client.Models.SQLite;
using OfflineSync.Client.Utilities;
using OfflineSync.IntegrationTest.Enums;
using User.Client.SQLiteModels;

namespace OfflineSync.IntegrationTest.Utilities
{
    static class TestUtility
    {
        static SyncAPIUtilityTest _SyncAPIUtility;
        static SyncSettingsUtility _SyncSettings;

        static TestUtility()
        {
            AddSyncGlobalSettings();

            _SyncAPIUtility = new SyncAPIUtilityTest();
            _SyncSettings = new SyncSettingsUtility();
        }

        public static void AddSyncGlobalSettings()
        {
            SyncGlobalConfig.DBPath = @"SyncDB.db";
            SyncGlobalConfig.Token = "";
            SyncGlobalConfig.APIUrl = @"http://localhost:64115/API/";
            //SyncGlobalConfig.IsDateTimeTick = false;
        }

        public static void AddDeviceID()
        {
            new DeviceIDUtility(_SyncAPIUtility).InitializeDeviceID();
        }

        public static void AddSetting(SQLiteSyncSettingsModel model)
        {
            _SyncSettings.Add(model);
        }

        public static void AddAllSettings()
        {
            _SyncSettings = new SyncSettingsUtility();

            AddSetting(TestDataSourceClient.ACTSHSettings);
            AddSetting(TestDataSourceClient.ACTSSettings);
            AddSetting(TestDataSourceClient.ASTCSettings);
            AddSetting(TestDataSourceClient.ATWSSettings);
            AddSetting(TestDataSourceClient.STCSettings);
            AddSetting(TestDataSourceClient.TWSSettings);
        }

        public static void StartSync(SyncModelTypeEnum type)
        {
            switch (type)
            {
                case SyncModelTypeEnum.tblTestACTS:
                    new SyncUtility<tblTestACTS>(_SyncAPIUtility).StartSyncAsync();
                    break;
                case SyncModelTypeEnum.tblTestACTSH:
                    new SyncUtility<tblTestACTSH>(_SyncAPIUtility).StartSyncAsync();
                    break;
                case SyncModelTypeEnum.tblTestASTC:
                    new SyncUtility<tblTestASTC>(_SyncAPIUtility).StartSyncAsync();
                    break;
                case SyncModelTypeEnum.tblTestATWS:
                    new SyncUtility<tblTestATWS>(_SyncAPIUtility).StartSyncAsync();
                    break;
                case SyncModelTypeEnum.tblTestSTC:
                    new SyncUtility<tblTestSTC>(_SyncAPIUtility).StartSyncAsync();
                    break;
                case SyncModelTypeEnum.tblTestTWS:
                    new SyncUtility<tblTestTWS>(_SyncAPIUtility).StartSyncAsync();
                    break;
            }
        }
    }
}