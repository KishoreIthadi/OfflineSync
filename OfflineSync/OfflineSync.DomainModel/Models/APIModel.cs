using OfflineSync.DomainModel.Enums;
using System;
using System.Collections.Generic;

namespace OfflineSync.DomainModel.Models
{
    public class APIModel
    {
        public APIModel()
        {
            FailedSyncRecords = new List<FailedRecordsModel>();
            FailedTransactionIDs = new List<string>();
        }

        public object FailedTrasationData { get; set; }
        public List<string> FailedTransactionIDs { get; set; }
        public object Data { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public string DeviceID { get; set; }
        public List<FailedRecordsModel> FailedSyncRecords { get; set; }
        public string ClientTableName { get; set; }
        public string ControllerRoute { get; set; }
        public string ControllerData { get; set; }
        public string ServerAssemblyName { get; set; }
        public string ServerTableName { get; set; }
        public SyncType SyncType { get; set; }
        public bool AutoSync { get; set; }
    }
}