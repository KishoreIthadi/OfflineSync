using OfflineSync.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace OfflineSyncServer.DB
{
    public class SQLContext<T> : DbContext where T : class
    {
        public SQLContext() : base("SyncConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            // make sure the table names doesn't changes
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<T> dbSet { get; set; }
    }

    public class SQLServerDBOperations<T> : IDBOperations<T> where T : class, ISyncBaseModel
    {
        public SQLServerDBOperations()
        {
        }

        public List<T> GetData()
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                return db.dbSet.ToList();
            }
        }

        public List<T> GetDataByLastSyncDate(DateTime dt)
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                return db.dbSet.Where((m) => m.ModifiedAt == dt).ToList();
            }
        }

        public List<T> PostData(List<T> list)
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                db.SaveChanges();
            }

            return list;
        }
    }
}