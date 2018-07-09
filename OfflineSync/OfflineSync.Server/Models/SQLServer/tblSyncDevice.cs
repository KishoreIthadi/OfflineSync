using OfflineSync.Server.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfflineSync.Server.Models.SQLServer
{
    internal class tblSyncDevice : IBaseSyncDevice
    {
        [Key]
        public string DeviceID { get; set; }
        public List<IBaseSyncTransaction> tblSyncTransactions { get; set; }
    }
}