using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfflineSync.Server.Models.SQLServer;

namespace User.APIApp
{
    public class tblTestATWS : ISQLSyncServerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string VersionID { get; set; }
        public string TransactionID { get; set; }
        public DateTime SyncCreatedAt { get; set; }
        public DateTime SyncModifiedAt { get; set; }
    }
}