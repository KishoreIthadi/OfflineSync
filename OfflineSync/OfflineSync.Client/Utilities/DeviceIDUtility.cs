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

        ISyncAPIUtility _syncAPIUtility;

        public DeviceIDUtility(ISyncAPIUtility syncAPIUtility = null)
        {
            if (syncAPIUtility == null)
            {
                _syncAPIUtility = new SyncAPIUtility(SyncGlobalConfig.APIUrl, SyncGlobalConfig.Token);
            }
            else
            {
                _syncAPIUtility = syncAPIUtility;
            }

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
                deviceID = await _syncAPIUtility.Get<string>(StringUtility.DeviceIDAPICall);

                // saving the device id to configuration table
                _dBOperations.InsertConfigurationsModel("DeviceID", deviceID);
            }

            return deviceID;
        }
    }
}