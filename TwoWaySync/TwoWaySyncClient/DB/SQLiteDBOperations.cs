using SQLite;
using System.Collections.Generic;
using System.Linq;
using TwoWaySync.DomainModel.Models;
using TwoWaySyncClient.Models;

namespace TwoWaySyncClient.DB
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