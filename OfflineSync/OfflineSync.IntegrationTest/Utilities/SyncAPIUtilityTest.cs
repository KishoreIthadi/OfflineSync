using Newtonsoft.Json;
using OfflineSync.DomainModel.Models;
using OfflineSync.DomainModel.Utilities;
using OfflineSync.Server.Controllers;
using System.Threading.Tasks;

namespace OfflineSync.IntegrationTest.Utilities
{
    class SyncAPIUtilityTest : ISyncAPIUtility 
     {
        SyncController _syncController;

        public SyncAPIUtilityTest()
        {
            _syncController = new SyncController();
        }
        
        public async Task<T> Get<T>(string route)
        {
            return (T)(object)(_syncController.GetDeviceIDCall());
        }

        public async Task<U> Post<T,U>(T data, string route)
        {
            APIModel model = null;
            string jsonData = JsonConvert.SerializeObject(data);

            if (route.Equals(StringUtility.GetData))
            {
                model = _syncController.GetCall(JsonConvert.DeserializeObject<APIModel>(jsonData));
            }
            else if (route.Equals(StringUtility.PostData))
            {
                model = _syncController.PostCall(JsonConvert.DeserializeObject<APIModel>(jsonData));
            }
            jsonData = JsonConvert.SerializeObject(model);
            return (U)(object)(JsonConvert.DeserializeObject<APIModel>(jsonData));
        }
    }
}