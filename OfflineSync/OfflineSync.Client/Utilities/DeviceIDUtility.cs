using System.Threading.Tasks;
using OfflineSync.Client.DB;
using OfflineSync.Client.Enums;
using OfflineSync.Client.Models;
using OfflineSync.DomainModel.Utilities;

namespace OfflineSync.Client.Utilities
{
    public class DeviceIDUtility
    {
        IDBOperations _dBOperations;

        public DeviceIDUtility()
        {
            switch (SyncGlobalConfig.DBType)
            {
                case ClientDBType.SQLite:
                    _dBOperations = new SQLiteDBOperations();
                    break;
            }
        }

        public void InitializeDeviceID()
        {
            Task.Run(GetDeviceID).Wait();
        }

        internal async Task<string> GetDeviceID()
        {
            // Checking if deviceID exists
            string deviceID = _dBOperations.GetConfigurationByKey("DeviceID");

            // Get DeviceID from API
            if (deviceID == null)
            {
                SyncAPIUtility syncAPI = new SyncAPIUtility(SyncGlobalConfig.APIUrl, SyncGlobalConfig.Token);

                deviceID = await syncAPI.Get<string>(StringUtility.DeviceIDAPICall);

                // saving the device id to configuration table
                _dBOperations.InsertConfigurationsModel("DeviceID", deviceID);
            }

            return deviceID;
        }
    }
}