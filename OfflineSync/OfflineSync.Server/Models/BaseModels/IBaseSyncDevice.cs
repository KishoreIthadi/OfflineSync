using System.Collections.Generic;

namespace OfflineSync.Server.Models.BaseModels
{
    internal interface IBaseSyncDevice
    {
         string DeviceID { get; set; }

         List<IBaseSyncTransaction> tblSyncTransactions { get; set; }
    }
}
