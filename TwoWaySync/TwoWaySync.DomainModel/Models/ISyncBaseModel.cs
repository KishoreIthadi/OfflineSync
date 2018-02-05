using System;

namespace TwoWaySync.DomainModel.Models
{
    public interface ISyncBaseModel
    {
        string ID { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        bool IsActive { get; set; }
    }
}
