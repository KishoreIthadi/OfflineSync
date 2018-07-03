namespace OfflineSync.Client.Models.BaseModels
{
    public interface ISyncClientBaseModel
    {
        string VersionID { get; set; }
        string TransactionID { get; set; }
        bool? IsSynced { get; set; }
        string SyncCreatedAt { get; }
        string SyncModifiedAt { get; }
    }
}