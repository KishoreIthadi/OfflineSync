using Newtonsoft.Json;
using OfflineSync.DomainModel.Models;
using OfflineSync.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace OfflineSync.Server.DB
{
    public class SQLContext<T> : DbContext where T : class
    {
        public SQLContext() : base("SyncConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // make sure the table names doesn't changes
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<T> dbSet { get; set; }
    }

    public class SQLServerDBOperations : IDBOperations
    {
        public SQLServerDBOperations()
        {
        }

        public List<string> GetFailedTransactionInfo(List<string> transactionIDs, string deviceID)
        {
            using (SQLContext<tblSyncTransaction> db = new SQLContext<tblSyncTransaction>())
            {
                List<string> failedTransactionList = new List<string>();

                foreach (var id in transactionIDs)
                {
                    if (!db.dbSet.Any(m => m.TransactionID == id && m.DeviceID == deviceID))
                    {
                        db.dbSet.Add(new tblSyncTransaction { Status = false, DeviceID = deviceID, TransactionID = id });
                        failedTransactionList.Add(id);
                    }
                    else if (db.dbSet.Any(m => m.TransactionID == id && m.DeviceID == deviceID && m.Status == false))
                    {
                        failedTransactionList.Add(id);
                    }
                }

                if (failedTransactionList.Count > 0) { return failedTransactionList; }

                return null;
            }
        }

        public List<T> GetData<T>() where T : class, ISyncServerBaseModel
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                var res = db.dbSet.ToList();

                if (res.Count > 0) { return res; }

                return null;
            }
        }

        public List<T> GetDataByLastSyncDate<T>(DateTime dt) where T : class, ISyncServerBaseModel
        {
            using (SQLContext<T> db = new SQLContext<T>())
            {
                var res = db.dbSet.Where(m => DateTime.Compare(dt, m.SyncModifiedAt) < 0).ToList();

                if (res.Count > 0) { return res; }

                return null;
            }
        }

        public void UpdateFailedTransactions<T>(APIModel model) where T : class, ISyncServerBaseModel
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(model.FailedTrasationData.ToString());

            List<string> uniqueTransactionIDs = new List<string>();

            foreach (var item in list)
            {
                if (!uniqueTransactionIDs.Contains(item.TransactionID.ToString()))
                {
                    uniqueTransactionIDs.Add(item.TransactionID.ToString());
                }
            }

            // checking for failed transactions
            List<string> failedTransactionIDs = (List<string>)GetFailedTransactionInfo(uniqueTransactionIDs, model.DeviceID);

            List<T> failedRecords = new List<T>();

            foreach (var item in list)
            {
                if (failedTransactionIDs.Contains(item.TransactionID))
                {
                    failedRecords.Add(item);
                }
            }

            using (SQLContext<T> db = new SQLContext<T>())
            {
                List<T> newList = new List<T>();
                List<T> updateList = new List<T>();

                foreach (var item in failedRecords)
                {
                    var updatedItem = db.dbSet.Where(m => m.VersionID == item.VersionID).FirstOrDefault();

                    if (updatedItem != null)
                    {
                        var adapter = (IObjectContextAdapter)db;
                        var keyNames = adapter.ObjectContext.CreateObjectSet<T>()
                                        .EntitySet.ElementType.KeyMembers.Select(m => m.Name);

                        // Need to get the list of autoincrement columns

                        var itemJson = JsonConvert.SerializeObject(item);
                        var itemDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(itemJson);

                        var updatedItemDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(updatedItem));

                        foreach (var keyname in keyNames)
                        {
                            itemDict[keyname] = updatedItemDict[keyname];
                        }

                        var finalJson = JsonConvert.SerializeObject(itemDict);
                        var finalRec = JsonConvert.DeserializeObject<T>(finalJson);

                        db.Entry(updatedItem).CurrentValues.SetValues(finalRec);
                    }
                    else
                    {
                        db.dbSet.Add(item);
                    }
                }

                using (SQLContext<tblSyncTransaction> DB = new SQLContext<tblSyncTransaction>())
                {
                    foreach (var item in failedTransactionIDs)
                    {
                        var rec = DB.Set<tblSyncTransaction>().Where(m => m.TransactionID == item && m.DeviceID == model.DeviceID).FirstOrDefault();
                        rec.Status = true;
                    }

                    DB.SaveChanges();
                }

                db.SaveChanges();
            }
        }

        public void InsertUpdate<T>(APIModel model) where T : class, ISyncServerBaseModel
        {
            try
            {
                List<T> list = JsonConvert.DeserializeObject<List<T>>(model.Data.ToString());

                string transactionID = list.FirstOrDefault().TransactionID;

                using (SQLContext<T> db = new SQLContext<T>())
                {
                    using (SQLContext<tblSyncTransaction> DB = new SQLContext<tblSyncTransaction>())
                    {
                        if (!DB.Set<tblSyncTransaction>().Any(m => m.TransactionID == transactionID && m.DeviceID == model.DeviceID))
                        {
                            DB.Set<tblSyncTransaction>().Add(
                               new tblSyncTransaction
                               {
                                   DeviceID = model.DeviceID,
                                   TransactionID = transactionID,
                                   Status = false
                               }
                            );
                            DB.SaveChanges();
                        }
                        else
                        {
                            // TransactionID with DeviceID already exists
                            // Dublicate transactionID error
                            model.FailedTransactionIDs[0] = transactionID;
                        }
                    }

                    foreach (var item in list)
                    {
                        try
                        {
                            var updatedItem = db.dbSet.Where(m => m.VersionID == item.VersionID).FirstOrDefault();

                            if (updatedItem != null)
                            {
                                var adapter = (IObjectContextAdapter)db;
                                var keyNames = adapter.ObjectContext.CreateObjectSet<T>()
                                                .EntitySet.ElementType.KeyMembers.Select(m => m.Name);

                                // Need to get the list of autoincrement columns

                                var itemJson = JsonConvert.SerializeObject(item);
                                var itemDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(itemJson);

                                var updatedItemDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(updatedItem));

                                foreach (var keyname in keyNames)
                                {
                                    itemDict[keyname] = updatedItemDict[keyname];
                                }

                                var finalJson = JsonConvert.SerializeObject(itemDict);
                                var finalRec = JsonConvert.DeserializeObject<T>(finalJson);

                                db.Entry(updatedItem).CurrentValues.SetValues(finalRec);
                            }
                            else
                            {
                                db.dbSet.Add(item);
                            }
                        }
                        catch (Exception ex)
                        {
                            // need to write logic for capturing dublicate versionid
                        }
                    }

                    db.SaveChanges();

                    using (SQLContext<tblSyncTransaction> DB = new SQLContext<tblSyncTransaction>())
                    {
                        var transc = DB.Set<tblSyncTransaction>().Where(m => m.TransactionID == transactionID).FirstOrDefault();
                        transc.Status = true;
                        DB.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}