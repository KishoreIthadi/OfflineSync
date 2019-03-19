using OfflineSync.Server.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfflineSync.Server.Models.SQLServer
{
    internal class tblSyncTransaction : IBaseSyncTransaction
    {
        [Key]
        public string TransactionID { get; set; }
        public string DeviceID { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IBaseSyncDevice tblSyncDevice { get; set; }
    }
}