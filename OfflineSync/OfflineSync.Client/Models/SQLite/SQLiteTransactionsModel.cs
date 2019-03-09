using OfflineSync.Client.Models.BaseModels;
using SQLite;

namespace OfflineSync.Client.Models.SQLite
{
    [Table("Transactions")]
    public class SQLiteTransactionsModel : ITransactionsModel
    {
        [PrimaryKey, NotNull]
        public string TransactionID { get; set; }

        [NotNull]
        public string CreatedAt { get; set; }
    }
}