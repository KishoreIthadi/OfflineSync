using SQLite;

namespace OfflineSync.Client.Models
{
    [Table("Configurations")]
    public class ConfigurationsModel
    {
        [PrimaryKey,NotNull]
        public string Key { get; set; }

        [NotNull]
        public string Value { get; set; }
    }
}
