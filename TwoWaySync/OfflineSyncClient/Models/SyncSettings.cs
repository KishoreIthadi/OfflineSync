using System;
using OfflineSyncClient.Enums;

namespace OfflineSyncClient.Models
{
    public class SyncSettings
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string TableName { get; set; }
        public string ControllerName { get; set; }
        public string ControllerData { get; set; }
        public string ServerTableName { get; set; }
        public bool AutoSync { get; set; }
        public DateTime? LastSyncedAt { get; set; }
        public OveridePriority priority { get; set; }
    }
}