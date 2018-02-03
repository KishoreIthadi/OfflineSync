using System;

namespace TwoWaySync.DomainModel
{
    public class APIModel
    {
        public object Data { get; set; }

        public DateTime LastSyncDate { get; set; }

        public string Type { get; set; }
    }
}