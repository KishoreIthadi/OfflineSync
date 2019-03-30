using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfflineSync.Server.Models.SQLServer;

namespace User.Server.SQLModels
{
    public class tblTestACTSH : ISQLSyncServerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string StringType { get; set; }
        public int IntType { get; set; }
        public double FloatType { get; set; }
        public DateTime? DateType { get; set; }
        public string VersionID { get; set; }
        public string TransactionID { get; set; }
        public DateTime? SyncCreatedAt { get; set; }
        public DateTime? SyncModifiedAt { get; set; }
    }
}