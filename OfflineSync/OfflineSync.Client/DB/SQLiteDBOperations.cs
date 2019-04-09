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
using Newtonsoft.Json;

namespace OfflineSync.Client.DB
{
    internal class SQLiteDBOperations : IDBOperations
    {
        public string _DBPath;
        public SQLiteDBOperations()
        {
            _DBPath = SyncGlobalConfig.DBPath;
           
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.CreateTable<SQLiteConfigurationsModel>();
                conn.CreateTable<SQLiteSyncSettingsModel>();
                conn.CreateTable<SQLiteTransactionsModel>();
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

        public string GetConfigurationByKey(string key)
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
       
        /* FailedTransactionIDs conflict is highly rare to occur, as TransactionID is combination of GUID + DeviceID + Ticks
            Uncomment Client.DB.SyncUtility.FailedTransactionsSync, Client.DB.SQLiteDBOPerations.UpdateConflictedTransationIDs,
            Client.DB.IDBOperations.UpdateConflictedTransationIDs, Client.DB.SyncUtility.PostDataAsync, Server.DB.SQLServerDBOperations.InsertUpdate
            DomainModel.Models.APIModel
        */

        /*public void UpdateConflictedTransationIDs<T>(List<string> failedTransactionIDs, APIModel model) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                List<T> data = null;
                foreach (var id in failedTransactionIDs)
                {
                    var list = conn.Table<T>().ToList().Where(m => m.TransactionID == id).ToList();

                    string newID = Guid.NewGuid().ToString() + "-" + model.DeviceID + "-" + DateTime.Now.Ticks;

                    while (conn.Table<T>().Any(m => m.TransactionID == newID))
                    {
                        newID = Guid.NewGuid().ToString() + "-" + model.DeviceID + "-" + DateTime.Now.Ticks; ;
                    }

                    foreach (var item in list)
                    {
                        item.TransactionID = newID;
                        item.IsSynced = false; 
                    }
                    data = (List<T>)model.Data;
                    data.Where(m => m.TransactionID == id).First().TransactionID = newID;
                    conn.UpdateAll(list);
                }
                model.Data = data;
            }
        }*/

        public void InsertConfigurationsModel(string key, string value)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.Insert(new SQLiteConfigurationsModel { Key = key, Value = value });
            }
        }

        public List<T> GetFailedTransactionData<T>() where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                //TODO alternate for .ToList()
                var res = conn.Table<T>().ToList().Where(m => m.IsSynced == false && m.TransactionID != null).ToList();

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
                              .Where(m => DateTime.Compare(lastsync.Value, Convert.ToDateTime(m.SyncModifiedAt)) < 0).ToList();
                }

                if (res.Count == 0) { return null; }

                return res;
            }
        }

        public void InsertList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                //list = list.Select(m => { m.IsSynced = true; return m; });
                conn.InsertAll(list);
            }
        }

        public void UpdateList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var temp = conn.Table<T>().Table.Columns.Where(m => m.IsAutoInc).FirstOrDefault();

                var autoIncCol = temp != null ? temp.Name : string.Empty;
                var columns = conn.Table<T>().Table.Columns;

                foreach (var item in list)
                {
                    var DBrec = conn.Table<T>().ToList().Where(m => m.VersionID == item.VersionID).FirstOrDefault();

                    if (DBrec != null)
                    {
                        var itemJson = JsonConvert.SerializeObject(item);
                        var itemDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(itemJson);

                        var UpdateItemJson = JsonConvert.SerializeObject(DBrec);
                        var UpdateItemDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(UpdateItemJson);

                        foreach (var key in itemDict.Keys)
                        {
                            // ecluding if it is autoincrement column
                            if (key != autoIncCol)
                            {
                                if (key == "SyncCreatedAt" || key == "SyncModifiedAt")
                                {
                                    UpdateItemDict[key] = Convert.ToDateTime(itemDict[key].ToString().ToLower().Replace("t", " ")).ToString("yyyy-MM-dd hh:mm:ss");
                                }
                                /*else if(key == "IsSynced")
                                {
                                    UpdateItemDict[key] = true;
                                }*/
                                else
                                {
                                    UpdateItemDict[key] = itemDict[key];
                                }
                            }
                        }

                        var finalJson = JsonConvert.SerializeObject(UpdateItemDict);
                        var finalRec = JsonConvert.DeserializeObject<T>(finalJson);

                        conn.Update(finalRec);
                    }
                }
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
                    var list = conn.Table<T>().ToList().Where(m => m.TransactionID == item).ToList();

                    foreach (var rec in list)
                    {
                        if (syncType == SyncType.SyncClientToServerAndHardDelete)
                        {
                            conn.Delete(rec);
                        }
                        else
                        {
                            rec.IsSynced = true;
                            rec.TransactionID = null;
                            conn.Update(rec);
                        }
                    }
                }
            }
        }

        public void SetTransationIDs<T>(List<T> clientList, string deviceID) where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                string transactionID = Guid.NewGuid().ToString() + "-" + deviceID + "-" + DateTime.Now.Ticks;

                while (conn.Table<SQLiteTransactionsModel>().Any(m => m.TransactionID == transactionID))
                {
                    transactionID = Guid.NewGuid().ToString() + "-" + deviceID + "-" + DateTime.Now.Ticks;
                }

                conn.Insert(new SQLiteTransactionsModel
                {
                    TransactionID = transactionID,
                    CreatedAt = DateTime.Now.ToString()
                });

                foreach (var item in clientList)
                {
                    // Version ID is already set using trigger
                    // Double checking if the version ID is null for any unknown scenario
                    if (item.VersionID == null)
                    {
                        string versionID = Guid.NewGuid().ToString() + "-" + deviceID;

                        while (conn.Table<T>().Any(m => m.VersionID == versionID))
                        {
                            item.VersionID = Guid.NewGuid().ToString() + "-" + deviceID;
                        }
                    }

                    item.IsSynced = false;

                    if (item.TransactionID == null)
                    {
                        item.TransactionID = transactionID;
                    }                    
                    conn.Update(item);
                }

            }          
            
        }

        public void UpdateSyncSettingsModel(ISyncSettingsBaseModel model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
            {
                var rec = conn.Table<SQLiteSyncSettingsModel>()
                              .Where(m => m.SyncSettingsID == model.SyncSettingsID).FirstOrDefault();

                if (rec != null)
                {
                    //if (rec.ClientTableName != model.ClientTableName)
                    //{
                    //    throw new Exception(StringUtility.CannotRenameTable);
                    //}

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
            using (SQLiteConnection conn = new SQLiteConnection(SyncGlobalConfig.DBPath))
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
                                     " AFTER INSERT ON {0}" +
                                     " BEGIN" +
                                     " UPDATE {0}" +
                                     " SET SyncCreatedAt = STRFTIME('%Y-%m-%d %H:%M:%S', COALESCE(NEW.SyncCreatedAt, DATETIME('NOW')))," +
                                     " SyncModifiedAt = STRFTIME('%Y-%m-%d %H:%M:%S', COALESCE(NEW.SyncModifiedAt, DATETIME('NOW')))," +
                                     " VersionID = CAST(hex(randomblob(16)) AS TEXT) || '-' || +CAST((SELECT Value FROM Configurations WHERE[KEY] == 'DeviceID')AS TEXT)" +
                                     " WHERE VersionID IS NULL;" +
                                     " UPDATE {0}" +
                                     " SET SyncCreatedAt = STRFTIME('%Y-%m-%d %H:%M:%S', COALESCE(NEW.SyncCreatedAt, DATETIME('NOW')))," +
                                     " SyncModifiedAt = STRFTIME('%Y-%m-%d %H:%M:%S', COALESCE(NEW.SyncModifiedAt, DATETIME('NOW')))" +
                                     " WHERE VersionID = NEW.VersionID;" +
                                     " END;";

                        conn.Execute(string.Format(insertTrigger, model.ClientTableName));

                        string updateTrigger = "CREATE TRIGGER {0}_ModifyTrigger" +
                                     " AFTER UPDATE ON {0}" +
                                     " WHEN old.VersionID == new.VersionID" +
                                     " AND old.SyncModifiedAt == new.SyncModifiedAt" +
                                     " BEGIN" +
                                     " Update {0}" +
                                     " SET SyncModifiedAt = STRFTIME('%Y-%m-%d %H:%M:%S'," +
                                     " CASE" +
                                     " WHEN STRFTIME('%Y-%m-%d %H:%M:%S', NEW.SyncModifiedAt) > STRFTIME('%Y-%m-%d %H:%M:%S', OLD.SyncModifiedAt)" +
                                     " THEN NEW.SyncModifiedAt" +
                                     " WHEN STRFTIME('%Y-%m-%d %H:%M:%S', NEW.SyncModifiedAt) < STRFTIME('%Y-%m-%d %H:%M:%S', OLD.SyncModifiedAt)" +
                                     " THEN OLD.SyncModifiedAt" +
                                     " WHEN STRFTIME('%Y-%m-%d %H:%M:%S', NEW.SyncModifiedAt) = STRFTIME('%Y-%m-%d %H:%M:%S', OLD.SyncModifiedAt) " +
                                     " THEN DATETIME('NOW')" +
                                     " END)" +
                                     " WHERE VersionID == old.VersionID AND" +
                                     " ifnull(TransactionID, 0) == ifnull(old.TransactionID, 0) AND" +
                                     " ifnull(IsSynced, 0) == ifnull(old.IsSynced, 0);" +
                                     " END;";

                        conn.Execute(string.Format(updateTrigger, model.ClientTableName));
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

        public void UpdateConflictedVersionIDs<T>(IEnumerable<FailedRecordsModel> conflictRecs, APIModel model)
        where T : ISyncClientBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                List<T> data = null;
                foreach (var rec in conflictRecs)
                {
                    T dbRec = conn.Table<T>().Where(m => m.VersionID == rec.VersionID).FirstOrDefault();

                    string versionID = Guid.NewGuid().ToString() + "-" + model.DeviceID;

                    while (conn.Table<T>().Any(m => m.VersionID == versionID))
                    {
                        versionID = Guid.NewGuid().ToString() + "-" + model.DeviceID;
                    }

                    dbRec.VersionID = versionID;
                    dbRec.IsSynced = false;

                    //data = (List<T>)model.Data;
                    data = model.FailedTrasationData == null ?
                           JsonConvert.DeserializeObject<List<T>>(model.Data.ToString()) :
                           JsonConvert.DeserializeObject<List<T>>(model.FailedTrasationData.ToString());

                    data.Where(m => m.VersionID == rec.VersionID).First().VersionID = versionID;

                    conn.Update(dbRec);
                }
                if (model.FailedTrasationData != null)
                {
                    model.FailedTrasationData = data;
                }
                else
                {
                    model.Data = data;
                }
            }
        }
    }
}