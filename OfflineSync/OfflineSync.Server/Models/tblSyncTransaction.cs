using System.ComponentModel.DataAnnotations;

namespace OfflineSync.Server.Models
{
    public class tblSyncTransaction
    {
        [Key]
        public string TransactionID { get; set; }
        public string DeviceID { get; set; }
        public bool Status { get; set; }

        public tblSyncDevice tblSyncDevice { get; set; }
    }
}