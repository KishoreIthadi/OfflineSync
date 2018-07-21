using OfflineSync.Server.Models.BaseModels;
using System;

namespace OfflineSync.Server.Models.SQLServer
{
    public interface ISQLSyncServerModel : ISyncServerBaseModel
    {
        new string VersionID { get; set; }
        new string TransactionID { get; set; }
        new DateTime? SyncCreatedAt { get; }
        new DateTime? SyncModifiedAt { get; }
    }
}