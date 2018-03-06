using System;

namespace OfflineSync.DomainModel
{
    public class APIModel
    {
        public object Data { get; set; }

        public DateTime LastSyncDate { get; set; }

        public string TableName { get; set; }
    }
}