using SQLite;
using System;

namespace OfflineSync.DomainModel.Models
{
    public interface ISyncBaseModel
    {
        [Unique]
        [NotNull]
        string VersionID { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}