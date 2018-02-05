using SQLite;
using System.Collections.Generic;
using System.Linq;
using TwoWaySync.DomainModel.Models;
using TwoWaySyncClient.Models;

namespace TwoWaySyncClient.DB
{
    public class SQLiteDBOperations<T> : IDBOperations<T> where T : ISyncBaseModel, new()
    {
        public string _DBPath;

        public SQLiteDBOperations(string DBpath)
        {
            _DBPath = DBpath;

            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.CreateTable<SyncSettings>();
            }
        }

        public List<T> GetData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                return conn.Table<T>().ToList();
            }
        }

    }
}