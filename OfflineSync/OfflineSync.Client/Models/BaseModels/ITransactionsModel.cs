namespace OfflineSync.Client.Models.BaseModels
{
    internal interface ITransactionsBaseModel
    {
        string TransactionID { get; set; }

        string CreatedAt { get; set; }

        string MaxDateTime { get; set; }
    }
}