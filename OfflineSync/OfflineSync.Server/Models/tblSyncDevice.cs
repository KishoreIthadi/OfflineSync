using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfflineSync.Server.Models
{
    public class tblSyncDevice
    {
        [Key]
        public string DeviceID { get; set; }

        public List<tblSyncTransaction> tblSyncTransactions { get; set; }
    }
}
