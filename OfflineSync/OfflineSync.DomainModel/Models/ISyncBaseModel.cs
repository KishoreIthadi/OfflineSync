using SQLite;
using System;

namespace OfflineSync.DomainModel.Models
{
    public interface ISyncBaseModel
    {
        [Unique]
        string VersionID { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}