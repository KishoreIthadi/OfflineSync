using OfflineSync.Client.Models;
using OfflineSync.Client.Models.SQLite;
using SQLite;
using System.Collections.Generic;
using System.IO;
using User.Client.SQLiteModels;

namespace OfflineSync.IntegrationTest.Utilities
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

        public static void InsertRecord<T>(T obj)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                conn.Insert(obj);
            }
        }

        public static List<T> GetAllRecords<T>() where T : new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                return conn.Table<T>().ToList();
            }
        }

        public static void ResetClientTable<T>(bool cleanTransactionTable= false)
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
    }
}
