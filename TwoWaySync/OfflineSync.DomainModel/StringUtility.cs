namespace TwoWaySync.DomainModel
{
    public static class StringUtility
    {
        public static string DulplicateSettings = "Found duplicate settings";

        public static string AutoSyncAPIGetCall = "Sync?serverTableName={0}&serverAssemblyName={1}&lastSyncDate={2}&data={3}";

        public static string UserAPIGetCall = "{0}?lastSyncDate={1}&data={2}";
    }
}