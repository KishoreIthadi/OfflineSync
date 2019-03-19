using OfflineSync.Client.Models.SQLite;
using SQLite;
using System;

namespace User.FormsApp
{
    class tblTestASTC : ISQLiteSyncClientModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string StringType { get; set; }
        public int IntType { get; set; }
        public double FloatType { get; set; }
        public DateTime? DateType { get; set; }
        public string VersionID { get; set; }
        public string TransactionID { get; set; }
        public bool? IsSynced { get; set; }
        public string SyncCreatedAt { get; set; }
        public string SyncModifiedAt { get; set; }
    }
}