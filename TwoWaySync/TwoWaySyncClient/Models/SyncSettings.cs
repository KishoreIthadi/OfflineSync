using System;

namespace TwoWaySyncClient.Models
{
    internal class SyncSettings : ISyncBaseModel
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }
}