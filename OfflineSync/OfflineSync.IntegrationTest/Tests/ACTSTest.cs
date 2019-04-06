using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSync.IntegrationTest.Enums;
using OfflineSync.IntegrationTest.Utilities;
using System.Threading;
using OfflineSync.IntegrationTest.DataSources;
using OfflineSync.IntegrationTest.DB;
using User.Client.SQLiteModels;

namespace OfflineSync.IntegrationTest.Tests
{
    [TestClass]
    public class ACTSTest
    {
        [ClassInitialize]
        public static void ClassInitialization(TestContext context)
        {
            TestUtility.AddSyncGlobalSettings();
        }

        [TestInitialize]
        public void TestMethodSetup()
        {
            SQLDBOperations.ResetServerTable<User.Server.SQLModels.tblTestACTS>(true);
            SQLiteDBOperations.DeleteClientDatabase();
            TestUtility.AddDeviceID();
            SQLiteDBOperations.CreateClientTables();
            TestUtility.AddAllSettings();
        }

        [TestMethod]
        public void AddTest()
        {
            SQLiteDBOperations.InsertRecords(new List<User.Client.SQLiteModels.tblTestACTS> { TestDataSourceClient.ACTSRecordOne });
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void ModifyTest()
        {
            SQLiteDBOperations.InsertRecords(new List<User.Client.SQLiteModels.tblTestACTS> { TestDataSourceClient.ACTSRecordOne });
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            tblTestACTS rec = SQLiteDBOperations.GetRecord<tblTestACTS>(10);
            rec.StringType = "Modified Data";
            SQLiteDBOperations.UpdateRecordData<tblTestACTS>(10, rec);
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);

            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void FailedTransactionServerStatusTrueTest()
        {
            FailedTransactionTestInitialize();
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void FailedTransactionServerStatusFalseTest()
        {
            FailedTransactionTestInitialize();

            SQLDBOperations.ResetServerTable<User.Server.SQLModels.tblTestACTS>();
            SQLDBOperations.SetSyncStatusFalse();
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void FailedTransactionIdNotInsertedInServerTest()
        {
            FailedTransactionTestInitialize();
            SQLDBOperations.ResetServerTable<User.Server.SQLModels.tblTestACTS>(true);
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void VersionIDConflictTest()
        {
            SQLiteDBOperations.InsertRecords(new List<tblTestACTS> { TestDataSourceClient.ACTSRecordOne, TestDataSourceClient.ACTSRecordTwo });
            VersionIDTestInitialize();
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void VersionIDConflictFailedTransactionServerStatusFalseTest()
        {
            FailedTransactionTestInitialize();

            SQLDBOperations.ResetServerTable<User.Server.SQLModels.tblTestACTS>();
            SQLDBOperations.SetSyncStatusFalse();

            VersionIDTestInitialize();

            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        [TestMethod]
        public void VersionIDConflictFailedTransactionIdNotInsertedInServer()
        {
            FailedTransactionTestInitialize();
            SQLDBOperations.ResetServerTable<User.Server.SQLModels.tblTestACTS>(true);
            VersionIDTestInitialize();
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            Assert.AreEqual(true, CheckClientSync());
        }

        public void FailedTransactionTestInitialize()
        {
            SQLiteDBOperations.InsertRecords(new List<tblTestACTS>
            { TestDataSourceClient.ACTSRecordOne, TestDataSourceClient.ACTSRecordTwo , TestDataSourceClient.ACTSRecordThree});
            TestUtility.StartSync(SyncModelTypeEnum.tblTestACTS);
            string transactionID = SQLDBOperations.GetTransactionId(); 

            tblTestACTS rec = SQLiteDBOperations.GetRecord<tblTestACTS>(10);
            rec.IsSynced = false;
            rec.TransactionID = transactionID;
            SQLiteDBOperations.UpdateRecordData<tblTestACTS>(10, rec);

            rec = SQLiteDBOperations.GetRecord<tblTestACTS>(100);
            rec.IsSynced = false;
            rec.TransactionID = transactionID;
            SQLiteDBOperations.UpdateRecordData<tblTestACTS>(100, rec);

            rec = SQLiteDBOperations.GetRecord<tblTestACTS>(1000);
            rec.StringType = "Modified Data";
            SQLiteDBOperations.UpdateRecordData<tblTestACTS>(1000, rec);
        }

        public static void VersionIDTestInitialize()
        {
            Thread.Sleep(2000);

            SQLDBOperations.InsertRecords(new List<User.Server.SQLModels.tblTestACTS> { TestDataSourceServer.ACTSRecordOne });

            string serverVersionId = SQLDBOperations.GetRecord<User.Server.SQLModels.tblTestACTS>(1).VersionID;

            tblTestACTS rec = SQLiteDBOperations.GetRecord<tblTestACTS>(10);
            rec.IsSynced = false;
            rec.VersionID = serverVersionId;
            SQLiteDBOperations.UpdateRecordData<tblTestACTS>(10, rec);

            rec = SQLiteDBOperations.GetRecord<tblTestACTS>(100);
            rec.StringType = "Modified Data";
            SQLiteDBOperations.UpdateRecordData<tblTestACTS>(100, rec);
            //SQLiteDBOperations.SetVersionId<User.Client.SQLiteModels.tblTestACTS>(1, serverVersionId);
        }

        public bool CheckClientSync()
        {
            List<User.Client.SQLiteModels.tblTestACTS> clientList = SQLiteDBOperations.GetAllRecords<User.Client.SQLiteModels.tblTestACTS>();
            List<User.Server.SQLModels.tblTestACTS> serverList = SQLDBOperations.GetAllRecords<User.Server.SQLModels.tblTestACTS>();

            foreach (var item in clientList)
            {
                if (!serverList.Any(m => m.StringType == item.StringType &&
                                         m.IntType == item.IntType &&
                                         m.FloatType == item.FloatType &&
                                         ToString(m.DateType) == ToString(item.DateType) &&
                                         m.VersionID == item.VersionID &&
                                         ToString(m.SyncCreatedAt) == item.SyncCreatedAt))
                {
                    return false;
                }
            }

            return true;
        }

        private static string ToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }
    }
}
