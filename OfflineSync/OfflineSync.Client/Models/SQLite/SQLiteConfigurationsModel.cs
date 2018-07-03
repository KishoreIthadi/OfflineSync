using OfflineSync.Client.Models.BaseModels;
using SQLite;

namespace OfflineSync.Client.Models.SQLite
{
    [Table("Configurations")]
    public class SQLiteConfigurationsModel : IConfigurationsBaseModel
    {
        [PrimaryKey, NotNull]
        public string Key { get; set; }

        [NotNull]
        public string Value { get; set; }
    }
}