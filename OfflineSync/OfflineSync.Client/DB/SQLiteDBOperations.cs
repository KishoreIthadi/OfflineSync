using SQLite;
using System.Collections.Generic;
using System.Linq;
using OfflineSync.Client.Models;
using System;
using OfflineSync.DomainModel.Models;
using OfflineSync.DomainModel.Enums;

namespace OfflineSync.Client.DB
{
    public class SQLiteDBOperations : IDBOperations
    {
        public string _DBPath;

        public SQLiteDBOperations(string DBpath)
        {
            _DBPath = DBpath;

            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.CreateTable<ConfigurationsModel>();
            }
        }

        public List<SyncSettingsModel> GetSyncSettingByTable(string clientTableName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var res = conn.Table<SyncSettingsModel>()
                    .Where(m => m.ClientTableName == clientTableName).ToList();

                if (res.Count == 0) { return null; }

                return res;
            }
        }

        public List<T> GetData<T>(DateTime? lastsync) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                //TODO need to check the comarision is valid or not
                //TODO alternate for .ToList()

                List<T> res;

                if (lastsync.Value == DateTime.MinValue)
                {
                    res = conn.Table<T>().ToList();
                }
                else
                {
                    res = conn.Table<T>().ToList()
                              .Where(m => DateTime.Compare(lastsync.Value, Convert.ToDateTime(m.SyncModifiedAt)) < 0)
                                                 .ToList();
                }

                if (res.Count == 0) { return null; }

                return res;
            }
        }

        public List<T> GetFailedTransactionData<T>() where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                //  return conn.Table<T>().Where(m => m.IsSynced == false).ToList();
                //TODO alternate for .ToList()
                var res = conn.Table<T>().ToList().Where(m => m.IsSynced == false && m.TransactionID != null).ToList();

                if (res.Count == 0) { return null; }

                return res;
            }
        }

        public string GetConfigurationsByKey(string key)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                ConfigurationsModel configurationsModel = conn.Table<ConfigurationsModel>()
                    .Where(m => m.Key == key).FirstOrDefault();

                if (configurationsModel != null)
                {
                    return configurationsModel.Value;
                }

                return null;
            }
        }

        public void InsertConfigurationsModel(string key, string value)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.Insert(new ConfigurationsModel { Key = key, Value = value });
            }
        }

        public void UpdateTransationIDs<T>(string[] failedTransactionIDs) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                foreach (var id in failedTransactionIDs)
                {
                    var list = conn.Table<T>().Where(m => m.TransactionID == id).ToList();

                    string newID = Guid.NewGuid().ToString();

                    while (conn.Table<T>().Any(m => m.TransactionID == newID))
                    {
                        newID = Guid.NewGuid().ToString();
                    }

                    foreach (var item in list)
                    {
                        item.TransactionID = newID;
                    }

                    conn.UpdateAll(list);
                }
            }
        }

        public void UpdateConflictVersionIDs<T>(IEnumerable<FailedRecordsModel> conflictRecs)
            where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                foreach (var rec in conflictRecs)
                {
                    var list = conn.Table<T>().Where(m => m.VersionID == rec.VersionID).ToList();

                    string newID = Guid.NewGuid().ToString();

                    while (conn.Table<T>().Any(m => m.VersionID == newID))
                    {
                        newID = Guid.NewGuid().ToString();
                    }

                    foreach (var item in list)
                    {
                        item.VersionID = newID;
                    }

                    conn.UpdateAll(list);
                }
            }
        }

        public void InsertList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.InsertAll(list);
            }
        }

        public void UpdateList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.UpdateAll(list);
            }
        }

        public void UpdateLastSync(int syncSettingsID, DateTime lastModified)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var rec = conn.Table<SyncSettingsModel>()
                    .Where(m => m.SyncSettingsID == syncSettingsID).FirstOrDefault();

                if (rec != null)
                {
                    rec.LastSyncedAt = lastModified.ToString();
                    conn.Update(rec);
                }
            }
        }

        public void UpdateTrasationSuccess<T>(List<string> transactionList, SyncType syncType) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                //TODO need to optimize this logic
                foreach (var item in transactionList)
                {
                    if (syncType == SyncType.SyncClientToServerAndHardDelete)
                    {
                        conn.Table<T>().Delete(m => m.TransactionID == item);
                    }
                    else
                    {
                        var list = conn.Table<T>().ToList().Where(m => m.TransactionID == item).ToList();

                        foreach (var rec in list)
                        {
                            rec.IsSynced = true;
                            rec.TransactionID = null;
                            conn.Update(rec);
                        }
                    }
                }
            }
        }

        public void UpdateIDS<T>(List<T> clientList) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                string transactionID = Guid.NewGuid().ToString();

                while (conn.Table<T>().Any(m => m.TransactionID == transactionID))
                {
                    transactionID = Guid.NewGuid().ToString();
                }

                foreach (var item in clientList)
                {
                    string versionID = Guid.NewGuid().ToString();

                    while (conn.Table<T>().Any(m => m.VersionID == versionID))
                    {
                        versionID = Guid.NewGuid().ToString();
                    }

                    item.VersionID = item.VersionID == null ? versionID : item.VersionID;
                    item.IsSynced = false;
                    item.TransactionID = item.TransactionID == null ? transactionID : item.TransactionID;

                    conn.Update(item);
                }
            }
        }

        public void UpdateDeviceIDAllTransactions()
        {
            
        }
    }
}