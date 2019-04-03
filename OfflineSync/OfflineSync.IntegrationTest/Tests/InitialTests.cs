using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OfflineSync.Server.DB;
using System;
using OfflineSync.DomainModel.Utilities;
using OfflineSync.Client.Models.SQLite;
using System.Linq;
using OfflineSync.IntegrationTest.Utilities;
using User.Client.SQLiteModels;
using OfflineSync.IntegrationTest.DB;

namespace OfflineSync.IntegrationTest.Tests
{
    [TestClass]
    public class InitialTests
    {
        /// <summary>
        /// Cheking global sync trigger exists in DB
        /// </summary>
        [TestMethod]
        public void ServerSyncTriggerExistsTest()
        {
            SQLServerDBUtility.CreateGlobalTrigger();

            string result = SQLDBOperations.GetGlobalTriggerName();

            Assert.AreEqual(result, "Trigger_Sync");
        }

        [TestMethod]
        public void InitialClientSetup()
        {
            TestUtility.AddSyncGlobalSettings();
            SQLiteDBOperations.DeleteClientDatabase();

            TestUtility.AddDeviceID();

            ClientApiDefaultTablesExistTest();
            DeviceIdExistsTest();

            SQLiteDBOperations.CreateClientTables();
            ClientTablesExistsTest();

            ACTSAutoSyncFalseValidationTest();
            ACTSHAutoSyncFalseValidationTest();
            AutoSyncFalseControllerRouteDataValidationTest();

            TestUtility.AddAllSettings();
            ClientSettingsTest();
            ClientTriggersTest();
        }

        public void ClientApiDefaultTablesExistTest()
        {
            int result = SQLiteDBOperations.ClientApiDefaultTablesExist();
            Assert.AreEqual(3, result);
        }
        
        public void DeviceIdExistsTest()
        {
            string clientDeviceID = SQLiteDBOperations.GetClientDeviceId();
            Assert.AreEqual(true, clientDeviceID != null);
        }

        public void ClientTablesExistsTest()
        {
            int result = SQLiteDBOperations.ClientTablesExists();
            Assert.AreEqual(2, result);
        }

        public void ACTSAutoSyncFalseValidationTest()
        {
            bool isValidating = true;

            try
            {
                TestUtility.AddSetting(TestDataSourceClient.ACTSAutoSyncFalseVal);
                isValidating = false;
            }
            catch (Exception e)
            {
                if (!e.Message.Equals(StringUtility.AutoSyncforCTS))
                {
                    isValidating = false;
                }
            }
            finally
            {
                SQLiteDBOperations.ResetClientSettings();
            }

            Assert.AreEqual(true, isValidating);
        }

        public void ACTSHAutoSyncFalseValidationTest()
        {
            bool isValidating = true;
            try
            {
                TestUtility.AddSetting(TestDataSourceClient.ACTSHAutoSyncFalseVal);
                isValidating = false;
            }
            catch (Exception e)
            {
                if (!e.Message.Equals(StringUtility.AutoSyncforCTSH))
                {
                    isValidating = false;
                }
            }
            finally
            {
                SQLiteDBOperations.ResetClientSettings();
            }
            Assert.AreEqual(true, isValidating);
        }

        public void AutoSyncFalseControllerRouteDataValidationTest()
        {
            bool isValidating = true;
            try
            {
                TestUtility.AddSetting(TestDataSourceClient.AutoSyncFalseControllerRouteDataVal);
                isValidating = false;
            }
            catch (Exception e)
            {
                if (!e.Message.Equals(StringUtility.ControllerSettingsError))
                {
                    isValidating = false;
                }
            }
            finally
            {
                SQLiteDBOperations.ResetClientSettings();
            }
            Assert.AreEqual(true, isValidating);
        }

        public void ClientSettingsTest()
        {
            List<SQLiteSyncSettingsModel> DBsettings = SQLiteDBOperations.GetAllSyncSettings();

            List<SQLiteSyncSettingsModel> localSettings = new List<SQLiteSyncSettingsModel>
            {TestDataSourceClient.ACTSHSettings, TestDataSourceClient.ACTSSettings, TestDataSourceClient.ASTCSettings,
            TestDataSourceClient.ATWSSettings, TestDataSourceClient.STCSettings, TestDataSourceClient.TWSSettings};

            bool val = true;

            foreach (var item in localSettings)
            {
                SQLiteSyncSettingsModel dbRec = DBsettings.Where(m => m.ClientTableName == item.ClientTableName).FirstOrDefault();

                if (dbRec.AutoSync != item.AutoSync && dbRec.ClientTableName != item.ClientTableName &&
                    dbRec.Priority != item.Priority && dbRec.SyncType != item.SyncType &&
                    dbRec.ServerAssemblyName != item.ServerAssemblyName && dbRec.ServerTableName != item.ServerTableName &&
                    dbRec.ControllerData != item.ControllerData && dbRec.ControllerRoute != item.ControllerRoute)
                {
                    val = false;
                    break;
                }
            }

            Assert.AreEqual(true, val);
        }

        public void ClientTriggersTest()
        {
            bool val = true;

            SQLiteDBOperations.InsertRecords(new List<User.Client.SQLiteModels.tblTestACTS> { TestDataSourceClient.ACTSRecordOne });

            tblTestACTS rec = SQLiteDBOperations.GetAllRecords<tblTestACTS>().First();

            SQLiteDBOperations.ResetClientTable<tblTestACTS>();

            if (rec.SyncCreatedAt == null || rec.VersionID == null || rec.SyncModifiedAt == null)
            {
                val = false;
            }

            Assert.AreEqual(true, val);
        }
    }
}
