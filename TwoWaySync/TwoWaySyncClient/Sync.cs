using System.Threading.Tasks;
using System;
using TwoWaySyncClient.DB;
using TwoWaySync.DomainModel.Models;
using System.Collections.Generic;
using TwoWaySyncClient.Enums;

namespace TwoWaySyncClient
{
    public class Sync<T> where T : ISyncBaseModel, new()
    {
        private string _DBPath { get; set; }
        private string _token;
        private string _baseURL;

        IDBOperations _dBOperations;

        public Sync(string databasePath, string baseURL, string token, DBTypeEnum dbType = DBTypeEnum.SQLite)
        {
            _DBPath = databasePath;
            _baseURL = baseURL;
            _token = token;

            switch (dbType)
            {
                case DBTypeEnum.SQLite:
                    _dBOperations = new SQLiteDBOperations(databasePath);
                    break;
            }
        }

        public async Task StartSyncAsync()
        {
            try
            {
                IDBOperations operations = new SQLiteDBOperations(_DBPath);
                var settings = operations.GetSyncSettingByTable(typeof(T).Name);

                string data = "?lastSyncDate=" + settings.LastSyncedAt + "&data=" + settings.ControllerData;

                SyncAPI syncAPI = new SyncAPI(_baseURL, _token);

                List<T> res = await syncAPI.Get<List<T>>(settings.ControllerName + data);
            }
            catch (Exception ex)
            {

            }
        }
    }
}