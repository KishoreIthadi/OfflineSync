using System;

namespace OfflineSync.Server.Models
{
    public interface ISyncServerBaseModel
    {
        string VersionID { get; set; }
        string TransactionID { get; set; }
        bool? IsSynced { get; set; }
        DateTime SyncCreatedAt { get; }
        DateTime SyncModifiedAt { get; }
    }
}