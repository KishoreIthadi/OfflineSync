using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfflineSync.IntegrationTest.Tests.SQLServer
{
    internal class tblSyncDevice
    {
        [Key]
        public string DeviceID { get; set; }
        public List<tblSyncTransaction> tblSyncTransactions { get; set; }
    }
}