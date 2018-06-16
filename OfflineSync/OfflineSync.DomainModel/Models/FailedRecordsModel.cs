namespace OfflineSync.DomainModel.Models
{
    public class FailedRecordsModel
    {
        public string VersionID { get; set; }
        public string ExceptionMessage { get; set; }
        public bool IsConflicted { get; set; }
    }
}
