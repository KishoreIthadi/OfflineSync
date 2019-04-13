using System;

namespace OfflineSync.DomainModel.Models
{
    public class FailedTrasationModel
    {
        public string FailedTransactionID { get; set; }
        public DateTime? FailedTransactionMaxTimeStamp { get; set; }
        public bool IsServerSideTransSuccess { get; set; }
    }
}
