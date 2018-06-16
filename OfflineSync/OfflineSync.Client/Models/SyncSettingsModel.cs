using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using OfflineSync.Client.Enums;
using OfflineSync.DomainModel.Enums;
using OfflineSync.DomainModel.Utilities;

namespace OfflineSync.Client.Models
{
    [Table("SyncSettings")]
    public class SyncSettingsModel
    {
        [PrimaryKey, AutoIncrement]
        public int SyncSettingsID { get; set; }
        [Unique]
        public string ClientTableName { get; set; }
        public string ControllerRoute { get; set; }
        public string Data { get; set; }
        public string ServerAssemblyName { get; set; }
        public string ServerTableName { get; set; }
        public bool AutoSync { get; set; }
        public string LastSyncedAt { get; set; }
        public OveridePriority Priority { get; set; }
        public SyncType SyncType { get; set; }
    }

    public class SyncSettings
    {
        string _DBPath = string.Empty;

        public SyncSettings(string dbPath)
        {
            _DBPath = dbPath;

            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.CreateTable<SyncSettingsModel>();
            }
        }

        public void Update(SyncSettingsModel model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var rec = conn.Table<SyncSettingsModel>()
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

        public void Add(SyncSettingsModel model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                if (!conn.Table<SyncSettingsModel>()
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
                                     " VersionID= hex(randomblob(16) + (SELECT Value FROM Configurations WHERE KEY == 'DeviceID' ))" +
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
                    catch
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

        public void AddMany(List<SyncSettingsModel> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }
    }
}