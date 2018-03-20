using System.Threading.Tasks;
using System;
using OfflineSyncClient.DB;
using OfflineSync.DomainModel.Models;
using System.Collections.Generic;
using OfflineSyncClient.Enums;
using OfflineSyncClient.Models;
using TwoWaySync.DomainModel;
using OfflineSync.DomainModel;

namespace OfflineSyncClient
{
    public class Sync<T> where T : ISyncBaseModel, new()
    {
        private string _DBPath { get; set; }
        private string _token;
        private string _baseURL;

        IDBOperations _dBOperations;

        public Sync(string databasePath, string baseURL, string token, DBType dbType = DBType.SQLite)
        {
            _DBPath = databasePath;
            _baseURL = baseURL;
            _token = token;

            switch (dbType)
            {
                case DBType.SQLite:
                    _dBOperations = new SQLiteDBOperations(databasePath);
                    break;
            }
        }

        public async Task StartSyncAsync()
        {
            try
            {
                IDBOperations operations = new SQLiteDBOperations(_DBPath);
                List<SyncSettingsModel> settingslist = operations.GetSyncSettingByTable(typeof(T).Name);

                // Having dublicate entries
                if (settingslist.Count > 1)
                {
                    throw new Exception(StringUtility.DulplicateSettings);
                }

                if (settingslist != null)
                {
                    SyncSettingsModel setting = settingslist[0];

                    string data = string.Empty;

                    if (setting.AutoSync)
                    {
                        data = string.Format(StringUtility.AutoSyncAPIGetCall
                                                   , setting.ServerTableName
                                                   , setting.ServerAssemblyName
                                                   , setting.LastSyncedAt
                                                   , setting.ControllerData);
                    }
                    else
                    {
                        data = string.Format(StringUtility.UserAPIGetCall
                                                  , setting.ControllerName
                                                  , setting.LastSyncedAt
                                                  , setting.ControllerData);
                    }

                    SyncAPI syncAPI = new SyncAPI(_baseURL, _token);

                    APIModel model = await syncAPI.Get<APIModel>(data);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}