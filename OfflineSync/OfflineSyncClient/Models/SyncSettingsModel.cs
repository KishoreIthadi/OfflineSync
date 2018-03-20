using System;
using System.Collections.Generic;
using System.Linq;
using OfflineSyncClient.Enums;
using SQLite;
using TwoWaySync.DomainModel;

namespace OfflineSyncClient.Models
{
    public class SyncSettingsModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int SyncSettingsID { get; set; }
        public string ClientTableName { get; set; }
        public string ControllerName { get; set; }
        public string ControllerData { get; set; }
        public string ServerAssemblyName { get; set; }
        public string ServerTableName { get; set; }
        public bool AutoSync { get; set; }
        public DateTime? LastSyncedAt { get; set; }
        public OveridePriority Priority { get; set; }
        public SyncType SyncType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
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

        public void Add(SyncSettingsModel model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                if (!conn.Table<SyncSettingsModel>().Any(m => m.ClientTableName == model.ClientTableName))
                {
                    conn.Insert(model);
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