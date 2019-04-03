using System;
using System.ComponentModel.DataAnnotations;

namespace OfflineSync.IntegrationTest.Tests.SQLServer
{
    internal class tblSyncTransaction
    {
        [Key]
        public string TransactionID { get; set; }
        public string DeviceID { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public tblSyncDevice tblSyncDevice { get; set; }
    }
}