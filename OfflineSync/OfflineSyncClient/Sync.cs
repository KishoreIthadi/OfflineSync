using System.Threading.Tasks;
using System;
using OfflineSyncClient.DB;
using OfflineSync.DomainModel.Models;
using System.Collections.Generic;
using OfflineSyncClient.Enums;
using OfflineSyncClient.Models;
using TwoWaySync.DomainModel;
using OfflineSync.DomainModel;
using SQLite;
using System.Linq;

namespace OfflineSyncClient
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

        //public async Task StartSyncAsync()
        //{
        //    GuidConflictResolution gcr = new GuidConflictResolution();
        //    // string tableName = typeof(T).Name;
        //    gcr.GetForeignKeyTables();
        //    try
        //    {
        //        IDBOperations operations = new SQLiteDBOperations(_DBPath);
        //        List<SyncSettings> settingslist = operations.GetSyncSettingByTable(typeof(T).Name);
        //        // we are having primary why again to check for duplicate entries 
        //        // Having dublicate entries
        //        if (settingslist.Count > 1)
        //        {
        //            throw new Exception(StringUtility.DulplicateSettings);
        //        }

        //        if (settingslist != null)
        //        {
        //            // enabling auto sync option 
        //            SyncSettings settings = settingslist[0];

        //            string data = string.Empty;

        //            if (settings.AutoSync)
        //            {
        //                data = string.Format(StringUtility.AutoSyncAPIGetCall
        //                                           , settings.ServerTableName
        //                                           , settings.ServerAssemblyName
        //                                           , settings.LastSyncedAt
        //                                           , settings.ControllerData);
        //            }
        //            else
        //            {
        //                data = string.Format(StringUtility.UserAPIGetCall
        //                                          , settings.ControllerName
        //                                          , settings.LastSyncedAt
        //                                          , settings.ControllerData);
        //            }

        //            List<T> UpdatedClientList = _dBOperations.GetData<T>(settings.LastSyncedAt);
        //            SyncAPI syncAPI = new SyncAPI(_baseURL, _token);

        //            APIModel model = await syncAPI.Get<APIModel>(data);
        //            List<T> InsertList = new List<T>();
        //            List<T> ModifyList = new List<T>();
        //            List<T> ServerList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(model.Data.ToString());

        //            if (settings.LastSyncedAt == null)
        //            {


        //                using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
        //                {
        //                    //     conn.InsertAll(ServerList);
        //                }
        //                settings.LastSyncedAt = DateTime.UtcNow;
        //                using (SQLiteConnection context = new SQLiteConnection(_DBPath)) context.Update(settings);


        //            }
        //            //not initial sync 
        //            else
        //            {
        //                // List<T> UpdatedClientList = _dBOperations.GetData<T>(settings.LastSyncedAt);
        //                foreach (T item in ServerList)
        //                {
        //                    // Insert logic
        //                    if (DateTime.Compare(settings.LastSyncedAt.Value, item.CreatedAt) < 0)
        //                    {
        //                        InsertList.Add(item);
        //                    }
        //                    // update and delete logic
        //                    else
        //                    {
        //                        int index = UpdatedClientList.FindIndex(m => m.ID == item.ID);
        //                        // If record is modified both at the server and at the client
        //                        if (index != -1)
        //                        {
        //                            // Based on Timestamps
        //                            if (settings.Priority == OveridePriority.LastUpdated)
        //                            {
        //                                if (DateTime.Compare(item.ModifiedAt, UpdatedClientList[index].ModifiedAt) > 0)
        //                                {
        //                                    ModifyList.Add(item);
        //                                    UpdatedClientList.Remove(UpdatedClientList[index]);

        //                                }
        //                                //else if (DateTime.Compare(item.ModifiedAt, UpdatedClientList[index].ModifiedAt) < 0) break;
        //                            }
        //                            // Based on server priority
        //                            if (settings.Priority == OveridePriority.Server)
        //                            {
        //                                ModifyList.Add(item);
        //                                UpdatedClientList.Remove(UpdatedClientList[index]);

        //                            }
        //                            // Based on Client priority
        //                            //else if (settings.Priority == OveridePriority.Client) break;
        //                            // Ask user choice
        //                            else
        //                            {
        //                                Console.WriteLine("Select 1. Server 2.Client");
        //                                int choice = Int32.Parse(Console.ReadLine());
        //                                if (choice == 1)
        //                                {
        //                                    ModifyList.Add(item);

        //                                    UpdatedClientList.Remove(UpdatedClientList[index]);

        //                                }
        //                                //else if (choice == 2) break;
        //                            }
        //                        }
        //                        // If the record is only modifies at the server.(No Conflict)
        //                        if (index == -1) ModifyList.Add(item);
        //                    }
        //                }
        //                using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
        //                {
        //                    conn.InsertAll(InsertList);
        //                    conn.UpdateAll(ModifyList);
        //                }
        //                Console.WriteLine(settings.LastSyncedAt);
        //                settings.LastSyncedAt = DateTime.UtcNow;
        //                using (SQLiteConnection context = new SQLiteConnection(_DBPath))
        //                {
        //                    context.Update(settings);
        //                }
        //            }


        //        }
        //    } //try 
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }


        //}

        public class sqlite_master
        {
            public sqlite_master() { }

            public string type { get; set; }
            public string Name { get; set; }
            public string tbl_name { get; set; }
            public int rootpage { get; set; }
            public string sql { get; set; }
        }

        public async Task StartSyncAsync()
        {
            try
            {
                IDBOperations operations = new SQLiteDBOperations(_DBPath);
                List<SyncSettings> settingslist = operations.GetSyncSettingByTable(typeof(T).Name);

                SQLiteConnection con;
                using (con = new SQLiteConnection(_DBPath))
                {
                    var list = con.Table<sqlite_master>()
                                  .Where(m => m.sql.Contains("REFERENCES artisttbl (id)"))
                                  .Select(n => n.tbl_name)
                                  .ToList();
                }
                
                // Having dublicate entries
                if (settingslist.Count > 1)
                {
                    throw new Exception(StringUtility.DulplicateSettings);
                }

                if (settingslist != null)
                {
                    SyncSettings setting = settingslist[0];

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