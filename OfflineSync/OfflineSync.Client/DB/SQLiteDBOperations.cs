using SQLite;
using System.Collections.Generic;
using System.Linq;
using System;
using OfflineSync.DomainModel.Models;
using OfflineSync.DomainModel.Enums;
using OfflineSync.Client.Models;
using OfflineSync.DomainModel.Utilities;
using OfflineSync.Client.Models.BaseModels;
using OfflineSync.Client.Models.SQLite;

namespace OfflineSync.Client.DB
{
    internal class SQLiteDBOperations : IDBOperations
    {
        public string _DBPath;

        public SQLiteDBOperations()
        {
            _DBPath = GlobalConfig.DBPath;

            using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
            {

                conn.CreateTable<SQLiteConfigurationsModel>();
                conn.CreateTable<SQLiteSyncSettingsModel>();
            }
        }

        public List<ISyncSettingsBaseModel> GetSyncSettingByTable<T>(string tableName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var res = conn.Table<SQLiteSyncSettingsModel>()
                    .Where(m => m.ClientTableName == tableName).ToList<ISyncSettingsBaseModel>();

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
                SQLiteConfigurationsModel configurationsModel = conn.Table<SQLiteConfigurationsModel>()
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
                conn.Insert(new SQLiteConfigurationsModel { Key = key, Value = value });
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
                var rec = conn.Table<SQLiteSyncSettingsModel>()
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
                        var list = conn.Table<T>().ToList().Where(m => m.TransactionID == item).ToList();

                        foreach (var rec in list)
                        {
                            conn.Delete(rec);
                        }
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

        public void UpdateSyncSettingsModel(ISyncSettingsBaseModel model) 
        {
            using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
            {
                var rec = conn.Table<SQLiteSyncSettingsModel>()
                              .Where(m => m.SyncSettingsID == model.SyncSettingsID).FirstOrDefault();

                if (rec != null)
                {
                    if (rec.ClientTableName != model.ClientTableName)
                    {
                        throw new Exception(StringUtility.CannotRenameTable);
                    }

                    conn.Update(model);
                }
                else
                {
                    throw new Exception(StringUtility.RecordNotFound);
                }
            }
        }

        public void AddSyncSettingsModel(ISyncSettingsBaseModel model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
            {
                if (!conn.Table<SQLiteSyncSettingsModel>()
                    .Any(m => m.ClientTableName == model.ClientTableName))
                {
                    conn.Insert(model);

                    // if user tries to insert table which doesnot exists
                    // droping that table in catch, because the trigger will throw an error
                    try
                    {
                        string insertTrigger = "CREATE TRIGGER {0}_CreateTrigger" +
                                     " AFTER INSERT ON {1}" +
                                     " BEGIN" +
                                     " UPDATE {2}" +
                                     " SET SyncCreatedAt = DATETIME('NOW'), " +
                                     " SyncModifiedAt = DATETIME('NOW')," +
                                     " VersionID= CAST(hex(randomblob(16)) AS TEXT) || '-'  || +" +
                                     "CAST((SELECT Value FROM Configurations WHERE KEY == 'DeviceID' )AS TEXT)" +
                                     " WHERE VersionID IS NULL;" +
                                     " END;";

                        conn.Execute(string.Format(insertTrigger, model.ClientTableName, model.ClientTableName, model.ClientTableName));

                        string updateTrigger = "CREATE TRIGGER {0}_ModifyTrigger" +
                                     " AFTER UPDATE ON {1}" +
                                     " WHEN old.VersionID == new.VersionID" +
                                     " AND old.SyncModifiedAt == new.SyncModifiedAt" +
                                     " BEGIN" +
                                     " Update {2}" +
                                     " SET SyncModifiedAt = DATETIME('NOW')" +
                                     " WHERE VersionID == old.VersionID" +
                                     " AND ifnull(TransactionID,0) == ifnull(old.TransactionID,0)" +
                                     " AND ifnull(IsSynced,0) == ifnull(old.IsSynced,0);" +
                                     " END;";

                        conn.Execute(string.Format(updateTrigger, model.ClientTableName, model.ClientTableName, model.ClientTableName));
                    }
                    catch (Exception ex)
                    {
                        conn.Delete(model);
                        throw;
                    }
                }
                else
                {
                    throw new Exception(StringUtility.DulplicateSettings);
                }
            }
        }
    }
}