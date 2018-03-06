using SQLite;
using System.Collections.Generic;
using System.Linq;
using OfflineSync.DomainModel.Models;
using OfflineSyncClient.Models;

namespace OfflineSyncClient.DB
{
    public class SQLiteDBOperations : IDBOperations
    {
        public string _DBPath;

        public SQLiteDBOperations(string DBpath)
        {
            _DBPath = DBpath;
        }

        public SyncSettings GetSyncSettingByTable(string tableName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                return conn.Table<SyncSettings>().Where(m => m.TableName == tableName).FirstOrDefault();
            }
        }

        public List<T> GetData<T>() where T : ISyncBaseModel, new()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                return conn.Table<T>().ToList();
            }
        }
    }
}