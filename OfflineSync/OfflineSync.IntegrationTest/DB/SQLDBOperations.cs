using OfflineSync.IntegrationTest.Tests.SQLServer;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using User.Server.SQLModels;

namespace OfflineSync.IntegrationTest.DB
{
    internal class SQLContext<T> : DbContext where T : class
    {
        public SQLContext() : base("SyncConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // table names doesn't changes
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<T> dbSet { get; set; }
    }

    static class SQLDBOperations
    {
        public static string _Conn = ConfigurationManager.ConnectionStrings["SyncConn"].ConnectionString;

        public static string GetGlobalTriggerName()
        {
            using (SQLContext<tblSyncDevice> db = new SQLContext<tblSyncDevice>())
            {
                return db.Database.SqlQuery<string>("SELECT Name FROM sys.triggers WHERE Name = 'Trigger_Sync';").FirstOrDefault();
            }
        }

        public static void ResetServerTable<T>(bool cleanTransactionTable = false) where T : class
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE " + typeof(T).Name);

                if (cleanTransactionTable)
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE tblSyncTransaction");
                }
            }
        }

        public static List<T> GetAllRecords<T>() where T : class
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                var res = db.dbSet.ToList();

                return res ?? null;
            }
        }

        public static T GetRecord<T>(int id)
        {
            dynamic rec = null;

            switch (typeof(T).Name)
            {
                case "tblTestACTS":
                    using (SQLContext<tblTestACTS> db = new SQLContext<tblTestACTS>())
                    {
                        rec = db.dbSet.Where(m => m.ID == id).FirstOrDefault();
                    }
                    break;
                case "tblTestACTSH":
                    using (SQLContext<tblTestACTSH> db = new SQLContext<tblTestACTSH>())
                    {
                        rec = db.dbSet.Where(m => m.ID == id).FirstOrDefault();
                    }
                    break;
                case "tblTestASTC":
                    using (SQLContext<tblTestASTC> db = new SQLContext<tblTestASTC>())
                    {
                        rec = db.dbSet.Where(m => m.ID == id).FirstOrDefault();
                    }
                    break;
                case "tblTestATWS":
                    using (SQLContext<tblTestATWS> db = new SQLContext<tblTestATWS>())
                    {
                        rec = db.dbSet.Where(m => m.ID == id).FirstOrDefault();
                    }
                    break;
                case "tblTestSTC":
                    using (SQLContext<tblTestSTC> db = new SQLContext<tblTestSTC>())
                    {
                        rec = db.dbSet.Where(m => m.ID == id).FirstOrDefault();
                    }
                    break;
                case "tblTestTWS":
                    using (SQLContext<tblTestTWS> db = new SQLContext<tblTestTWS>())
                    {
                        rec = db.dbSet.Where(m => m.ID == id).FirstOrDefault();
                    }
                    break;
            }
            return rec;
        }

        public static void InsertRecords<T>(List<T> data) where T : class
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                foreach (T item in data)
                {
                    db.dbSet.Add(item);
                }

                db.SaveChanges();
            }
        }

        public static string GetTransactionId()
        {
            using (SQLContext<tblSyncTransaction> db = new SQLContext<tblSyncTransaction>())
            {
                return db.dbSet.ToList().FirstOrDefault().TransactionID;
            }
        }

        public static void SetSyncStatusFalse()
        {
            using (SQLContext<tblSyncTransaction> db = new SQLContext<tblSyncTransaction>())
            {
                foreach (var item in db.dbSet.ToList())
                {
                    item.Status = false;
                }
                db.SaveChanges();
            }
        }
    }
}