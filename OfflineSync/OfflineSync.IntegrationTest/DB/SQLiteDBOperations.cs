using OfflineSync.Client.Models;
using OfflineSync.Client.Models.SQLite;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using User.Client.SQLiteModels;

namespace OfflineSync.IntegrationTest.DB
{
    static class SQLiteDBOperations
    {
        public static void DeleteClientDatabase()
        {
            if (File.Exists(SyncGlobalConfig.DBPath))
            {
                File.Delete(SyncGlobalConfig.DBPath);
            }
        }

        public static string GetClientDeviceId()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                return conn.Table<SQLiteConfigurationsModel>()
                           .Where(m => m.Key == "DeviceID")
                           .First()
                           .Value;
            }
        }

        public static int ClientApiDefaultTablesExist()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                return conn.Table<sqlite_master>()
                     .Where(m => m.type == "table" &&
                           (m.name == "Configurations" || m.name == "SyncSettings" || m.name == "Transactions"))
                     .Count();
            }
        }

        public static void CreateClientTables()
        {
            using (SQLiteConnection db = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                db.CreateTable<tblTestACTS>();
                db.CreateTable<tblTestACTSH>();
                db.CreateTable<tblTestASTC>();
                db.CreateTable<tblTestATWS>();
                db.CreateTable<tblTestSTC>();
                db.CreateTable<tblTestTWS>();
            }
        }

        public static int ClientTablesExists()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                return conn.Table<sqlite_master>()
                      .Where(m => m.type == "table" &&
                            (m.name == "tblTestACTS" || m.name == "tblTestACTSH"))
                      .Count();
            }
        }

        public static void ResetClientSettings()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                conn.DeleteAll<SQLiteSyncSettingsModel>();
                conn.Execute("UPDATE sqlite_sequence SET seq = 0 where name='SyncSettings'");
            }
        }

        public static List<SQLiteSyncSettingsModel> GetAllSyncSettings()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                return conn.Table<SQLiteSyncSettingsModel>().ToList();
            }
        }

        public static void ResetClientTable<T>(bool cleanTransactionTable = false) where T : new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                conn.DeleteAll<T>();
                conn.Execute("UPDATE sqlite_sequence SET seq = 0 where name='" + typeof(T).Name + "'");

                if (cleanTransactionTable)
                {
                    conn.DeleteAll<SQLiteTransactionsModel>();
                }
            }
        }

        //To be Reviewed
        public static List<T> GetAllRecords<T>() where T : new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                var res = conn.Table<T>().ToList();

                return res ?? null;
            }
        }

        public static T GetRecord<T>(int intType)
        {
            dynamic rec = null;

            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                switch (typeof(T).Name)
                {
                    case "tblTestACTS":
                        rec = conn.Table<tblTestACTS>().ToList().Where(m => m.IntType == intType).FirstOrDefault();
                        break;
                    case "tblTestACTSH":
                        rec = conn.Table<tblTestACTSH>().ToList().Where(m => m.IntType == intType).FirstOrDefault();
                        break;
                    case "tblTestASTC":
                        rec = conn.Table<tblTestASTC>().ToList().Where(m => m.IntType == intType).FirstOrDefault();
                        break;
                    case "tblTestATWS":
                        rec = conn.Table<tblTestATWS>().ToList().Where(m => m.IntType == intType).FirstOrDefault();
                        break;
                    case "tblTestSTC":
                        rec = conn.Table<tblTestSTC>().ToList().Where(m => m.IntType == intType).FirstOrDefault();
                        break;
                    case "tblTestTWS":
                        rec = conn.Table<tblTestTWS>().ToList().Where(m => m.IntType == intType).FirstOrDefault();
                        break;
                }
            }

            return rec;
        }

        public static void InsertRecords<T>(List<T> data)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                conn.InsertAll(data);
            }
        }

        public static void UpdateRecordData<T>(int id,dynamic obj)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                dynamic rec = GetRecord<T>(id);
                rec.StringType = obj.StringType;
                rec.IntType = obj.IntType;
                rec.FloatType = obj.FloatType;
                rec.DateType = obj.DateType;
                rec.VersionID = obj.VersionID;
                rec.TransactionID = obj.TransactionID;
                rec.SyncCreatedAt = obj.SyncCreatedAt;
                rec.SyncModifiedAt = obj.SyncModifiedAt;
                rec.IsSynced = obj.IsSynced;
                conn.Update(rec);
            }
        }
    }
}
