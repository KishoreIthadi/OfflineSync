using System.Threading.Tasks;
using System;
using TwoWaySyncClient.DB;
using TwoWaySync.DomainModel;

namespace TwoWaySyncClient
{
    public class Sync<T> where T : ISyncBaseModel, new()
    {
        private string _DBPath { get; set; }
        private string _token;
        private string _baseURL;

        IDBOperations<T> _dBOperations;

        public Sync(string databasePath, string baseURL, string token, DBTypeEnum dbType)
        {
            _DBPath = databasePath;
            _baseURL = baseURL;
            _token = token;

            switch (dbType)
            {
                case TwoWaySync.DomainModel.DBTypeEnum.SQLServer:
                    _dBOperations = new SQLiteDBOperations<T>(databasePath);
                    break;
            }
        }

        public async Task StartSyncAsync()
        {
            try
            {
                APIModel model = new APIModel();
                model.Data = _dBOperations.GetData();

                SyncAPI syncAPI = new SyncAPI(_baseURL, _token);

                APIModel res = await syncAPI.Post<APIModel, APIModel>(model, "Sync");
            }
            catch (Exception ex)
            {

            }
        }
    }
}