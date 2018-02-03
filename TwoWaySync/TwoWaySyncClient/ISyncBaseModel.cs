using System;

namespace TwoWaySyncClient
{
    public interface ISyncBaseModel
    {
        Guid ID { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        bool IsActive { get; set; }
    }
}