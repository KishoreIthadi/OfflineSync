using OfflineSync.Client.Models.BaseModels;
using SQLite;

namespace OfflineSync.Client.Models.SQLite
{
    public interface ISQLiteSyncClientModel : ISyncClientBaseModel
    {
        [Unique, NotNull]
        new string VersionID { get; set; }
        new string TransactionID { get; set; }
        new bool? IsSynced { get; set; }
        new string SyncCreatedAt { get; }
        new string SyncModifiedAt { get; }
    }
}