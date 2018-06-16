using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfflineSync.Server.Models;

namespace User.APIApp
{
    public class tblTestASTC : ISyncServerBaseModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string VersionID { get; set; }
        public string TransactionID { get; set; }
        public bool? IsSynced { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime SyncCreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SyncModifiedAt { get; set; }
    }
}