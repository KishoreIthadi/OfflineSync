using OfflineSync.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;

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

        //public List<T> PostData(APIModel model)
        //{
        //    using (var cntxt = new ServerContext<T>())
        //    {
        //        List<T> tObj = JsonConvert.DeserializeObject<List<T>>(model.Data.ToString());

        //        //T obj = (T)(model.Data);
        //        cntxt.dbSet.AddRange(tObj);
        //        cntxt.SaveChanges();
        //    }

        //    return null;
        //}
    }
}