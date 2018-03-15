using System;

namespace OfflineSync.DomainModel.Models
{
    public interface ISyncBaseModel
    {
         string ID { get; set; }
         DateTime CreatedAt { get; set; }
         DateTime ModifiedAt { get; set; }
         bool IsDeleted { get; set; }
    }
}