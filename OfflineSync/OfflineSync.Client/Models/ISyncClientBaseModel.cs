using SQLite;

namespace OfflineSync.Client.Models
{
    public interface ISyncClientBaseModel
    {
        [Unique, NotNull]
        string VersionID { get; set; }
        string TransactionID { get; set; }
        bool? IsSynced { get; set; }
        string SyncCreatedAt { get; }
        string SyncModifiedAt { get; }
    }
}