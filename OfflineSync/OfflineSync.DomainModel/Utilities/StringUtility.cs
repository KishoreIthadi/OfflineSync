namespace OfflineSync.DomainModel.Utilities
{
    public static class StringUtility
    {
        public static readonly string GetData = "/Sync/GetData";

        public static string DeviceIDAPICall = "/Sync/GetDeviceID";

        public static string PostData = "/Sync/PostData";


        public static string DulplicateSettings = "Found duplicate settings";

        public static string SettingNotFound = "Setting not found";

        public static string RecordNotFound = "Record not found";

        public static string CannotRenameTable = "Table should not be renamed";

        // public static string AutoSyncAPIGetCall = "Sync?serverTableName={0}&serverAssemblyName={1}&lastSyncDate={2}&data={3}";

        // public static string UserAPIGetCall = "{0}?lastSyncDate={1}&data={2}";
    }
}