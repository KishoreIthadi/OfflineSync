namespace OfflineSync.Client.Models.BaseModels
{
    internal interface ITransactionsModel
    {
        string TransactionID { get; set; }

        string CreatedAt { get; set; }
    }
}