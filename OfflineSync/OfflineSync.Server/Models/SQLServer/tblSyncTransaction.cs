using OfflineSync.Server.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace OfflineSync.Server.Models.SQLServer
{
    internal class tblSyncTransaction : IBaseSyncTransaction
    {
        [Key]
        public string TransactionID { get; set; }
        public string DeviceID { get; set; }
        public bool Status { get; set; }       
        public DateTime CreatedAt { get; set; }
        public IBaseSyncDevice tblSyncDevice { get; set; }
    }
}