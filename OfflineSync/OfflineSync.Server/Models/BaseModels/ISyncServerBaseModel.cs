using System;

namespace OfflineSync.Server.Models.BaseModels
{
    public interface ISyncServerBaseModel
    {
        string VersionID { get; set; }
        string TransactionID { get; set; }
        DateTime SyncCreatedAt { get; }
        DateTime SyncModifiedAt { get; }
    }
}