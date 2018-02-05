using System;
using TwoWaySync.DomainModel.Models;

namespace TwoWaySyncClient.Models
{
    internal class SyncSettings : ISyncBaseModel
    {
        public string ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }
}