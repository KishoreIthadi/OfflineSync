namespace OfflineSync.Server.Models.BaseModels
{
    internal interface IBaseSyncTransaction
    {
        string TransactionID { get; set; }
        string DeviceID { get; set; }
        bool Status { get; set; }

        IBaseSyncDevice tblSyncDevice { get; set; }
    }
}