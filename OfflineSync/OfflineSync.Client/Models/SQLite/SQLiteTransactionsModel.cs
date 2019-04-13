using OfflineSync.Client.Models.BaseModels;
using SQLite;

namespace OfflineSync.Client.Models.SQLite
{
    [Table("Transactions")]
    public class SQLiteTransactionsModel : ITransactionsBaseModel
    {
        [PrimaryKey, NotNull]
        public string TransactionID { get; set; }

        [NotNull]
        public string CreatedAt { get; set; }

        [NotNull]
        public string MaxDateTime { get; set; }
    }
}