using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using OfflineSync.Client.DB;
using OfflineSync.DomainModel.Models;
using OfflineSync.Client.Enums;
using OfflineSync.Client.Models;
using OfflineSync.DomainModel.Utilities;
using OfflineSync.DomainModel.Enums;
using Newtonsoft.Json;
using OfflineSync.Client.Models.BaseModels;

namespace OfflineSync.Client.Utilities
{
    public class SyncUtility<T> where T : ISyncClientBaseModel, new()
    {
        IDBOperations _dBOperations;

        public SyncUtility()
        {
            switch (SyncGlobalConfig.DBType)
            {
                case ClientDBType.SQLite:
                    _dBOperations = new SQLiteDBOperations();
                    break;
            }
        }

        public async Task StartSyncAsync()
        {
            try
            {
                List<ISyncSettingsBaseModel> settingslist = _dBOperations.GetSyncSettingByTable<ISyncSettingsBaseModel>(typeof(T).Name);

                SyncAPIUtility syncAPI = new SyncAPIUtility(SyncGlobalConfig.APIUrl, SyncGlobalConfig.Token);

                // Having duplicate entries
                if (settingslist == null)
                {
                    throw new Exception(StringUtility.SettingNotFound);
                }
                else if (settingslist.Count > 1)
                {
                    throw new Exception(StringUtility.DulplicateSettings);
                }             
                else
                {
                    ISyncSettingsBaseModel settings = settingslist[0];                    

                    APIModel model = await GetAPIModel(settings);

                    if (model.FailedTrasationData != null && model.SyncType != SyncType.SyncServerToClient)
                    {
                        // The 'Data' property will be filled at API controller if 'AutoSync' is true
                        // based on 'LastSyncDate' only if it is TwoWaySync
                        model = await FailedTransactionsSync(model, settings, true);
                    }
                    else if (model.SyncType == SyncType.SyncTwoWay || model.SyncType == SyncType.SyncServerToClient)
                    {
                        string url = model.ControllerRoute;

                        if (model.AutoSync && model.FailedTrasationData == null)
                        {
                            url = StringUtility.GetData;
                        }
                        else if (!model.AutoSync)
                        {
                            // Calling the client API method and fetching the data
                            url = model.ControllerRoute;
                        }

                        model = await syncAPI.Post<APIModel, APIModel>(model, url);
                    }

                    List<T> serverList = new List<T>();
                    List<T> insertList = new List<T>();
                    List<T> modifiedList = new List<T>();

                    if (model.Data != null)
                    {
                        serverList = JsonConvert.DeserializeObject<List<T>>(model.Data.ToString());
                    }

                    if (model.SyncType == SyncType.SyncServerToClient && serverList != null)
                    {
                        foreach (var item in serverList)
                        {
                            if (DateTime.Compare(Convert.ToDateTime(settings.LastSyncedAt), Convert.ToDateTime(item.SyncCreatedAt)) < 0)
                            {
                                insertList.Add(item);
                            }
                            else
                            {
                                modifiedList.Add(item);
                            }
                        }

                        if (insertList.Count > 0)
                        {
                            _dBOperations.InsertList<T>(insertList);
                        }
                        if (modifiedList.Count > 0)
                        {
                            _dBOperations.UpdateList<T>(modifiedList);
                        }

                        var lastSyncedAt = serverList.OrderByDescending(m => m.SyncModifiedAt)
                                          .FirstOrDefault().SyncModifiedAt;
                        _dBOperations.UpdateLastSync(settings.SyncSettingsID, Convert.ToDateTime(lastSyncedAt));
                    }
                    else
                    {
                        List<T> clientList = _dBOperations.GetData<T>(Convert.ToDateTime(settings.LastSyncedAt));

                        if (model.SyncType == SyncType.SyncClientToServer ||
                            model.SyncType == SyncType.SyncClientToServerAndHardDelete)
                        {
                            if (clientList != null)
                            {
                                model.Data = clientList;
                                await PostDataAsync(model, settings, true);

                                //TODO need to check for null logic, if date can be null in any scenario
                                var lastSyncedAt = ((List<T>)model.Data).OrderByDescending(m => m.SyncModifiedAt)
                                                  .FirstOrDefault().SyncModifiedAt;

                                _dBOperations.UpdateLastSync(settings.SyncSettingsID, Convert.ToDateTime(lastSyncedAt));
                            }
                        }
                        else
                        {
                            // Two way sync
                            foreach (var item in serverList)
                            {
                                // Insert logic
                                if (DateTime.Compare(Convert.ToDateTime(settings.LastSyncedAt), Convert.ToDateTime(item.SyncCreatedAt)) < 0)
                                {
                                    insertList.Add(item);
                                }
                                else
                                {
                                    // update and delete logic

                                    // If record is modified both at the server and at the client
                                    if (clientList != null)
                                    {
                                        T updatedRec = clientList.Where(m => m.VersionID == item.VersionID).FirstOrDefault();

                                        if (updatedRec != null)
                                        {
                                            if (settings.Priority == OveridePriority.LastUpdated)
                                            {
                                                //if (DateTime.Compare(Convert.ToDateTime(Convert.ToDateTime(item.SyncModifiedAt).ToString("yyyy-MM-dd hh:mm:ss")),
                                                //    Convert.ToDateTime(Convert.ToDateTime(updatedRec.SyncModifiedAt).ToString("yyyy-MM-dd hh:mm:ss"))) > 0)
                                                if (DateTime.Compare(Convert.ToDateTime(item.SyncModifiedAt), Convert.ToDateTime(updatedRec.SyncModifiedAt)) > 0)
                                                {
                                                    modifiedList.Add(item);
                                                    clientList.Remove(updatedRec);
                                                }
                                            }
                                            else if (settings.Priority == OveridePriority.Server)
                                            {
                                                modifiedList.Add(item);
                                                clientList.Remove(updatedRec);
                                            }
                                            //else if (settings.Priority == OveridePriority.User)
                                            //{
                                            //    // TODO Next release
                                            //}
                                        }
                                        else
                                        {
                                            //If the record is only modified at the server.(No Conflict)
                                            modifiedList.Add(item);
                                        }
                                    }
                                    else
                                    {
                                        //If the record is only modified at the server.(No Conflict)
                                        modifiedList.Add(item);
                                    }
                                }
                            }

                            if (insertList.Count > 0)
                            {
                                _dBOperations.InsertList<T>(insertList);
                            }
                            if (modifiedList.Count > 0)
                            {
                                _dBOperations.UpdateList<T>(modifiedList);
                            }

                            var clientLastSyncedAt = DateTime.MinValue.ToString();
                            var serverLastSyncedAt = DateTime.MinValue.ToString();

                            if (clientList != null && clientList.Count > 0)
                            {
                                model.Data = clientList;
                                await PostDataAsync(model, settings, true);

                                //TODO need to check for null logic, if date can be null in any scenario
                                clientLastSyncedAt = clientList.OrderByDescending(m => m.SyncModifiedAt)
                                                 .FirstOrDefault().SyncModifiedAt;
                            }

                            if (serverList != null && serverList.Count > 0)
                            {
                                serverLastSyncedAt = serverList.OrderByDescending(m => m.SyncModifiedAt)
                                                 .FirstOrDefault().SyncModifiedAt;
                            }

                            if ((clientList != null && clientList.Count > 0) || (serverList != null && serverList.Count > 0))
                            {
                                if (DateTime.Compare(Convert.ToDateTime(clientLastSyncedAt), Convert.ToDateTime(serverLastSyncedAt)) > 0)
                                {
                                    _dBOperations.UpdateLastSync(settings.SyncSettingsID, Convert.ToDateTime(clientLastSyncedAt));
                                }
                                else
                                {
                                    _dBOperations.UpdateLastSync(settings.SyncSettingsID, Convert.ToDateTime(serverLastSyncedAt));
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        internal async Task<APIModel> GetAPIModel(ISyncSettingsBaseModel settings)
        {
            string deviceID = await new DeviceIDUtility().GetDeviceID();

            APIModel model = new APIModel();

            if (settings.SyncType != SyncType.SyncServerToClient)
            {
                model.FailedTrasationData = _dBOperations.GetFailedTransactionData<T>();
            }

            model.DeviceID = deviceID;
            model.LastSyncDate = Convert.ToDateTime(settings.LastSyncedAt);
            model.ServerTableName = settings.ServerTableName;
            model.SyncType = settings.SyncType;
            model.AutoSync = settings.AutoSync;
            model.ServerAssemblyName = settings.ServerAssemblyName;

            // checking if the sync is autosync
            if (!settings.AutoSync)
            {
                model.ControllerData = settings.ControllerData;
                model.ControllerRoute = settings.ControllerRoute;
            }

            return model;
        }

        internal async Task<APIModel> FailedTransactionsSync(APIModel model, ISyncSettingsBaseModel syncSettingsModel, bool makeAPICall)
        {
            SyncAPIUtility syncAPI = new SyncAPIUtility(SyncGlobalConfig.APIUrl, SyncGlobalConfig.Token);

            APIModel res = model;

            // makeAPICall is false, The API call is already made and transation/version confilt occured
            if (makeAPICall)
            {
                res = await syncAPI.Post<APIModel, APIModel>(model, StringUtility.GetData);
            }

            /* 
              FailedTransactionIDs conflict is highly rare to occur, as TransactionID is combination of GUID + DeviceID + Ticks
              Uncomment Client.DB.SyncUtility.FailedTransactionsSync, Client.DB.SQLiteDBOPerations.UpdateConflictedTransationIDs,
              Client.DB.IDBOperations.UpdateConflictedTransationIDs, Client.DB.SyncUtility.PostDataAsync, Server.DB.SQLServerDBOperations.InsertUpdate
              DomainModel.Models.APIModel 
            */
            /* if (res.FailedTransactionIDs.Count > 0)
            {
                // Updating existing TransactionID since it already exist in server DB
                _dBOperations.UpdateConflictedTransationIDs<T>(res.FailedTransactionIDs, model.DeviceID);
            }*/

            if (res.FailedSyncRecords.Count > 0)
            {
                // Updating versionID since it already exist in server DB
                var conflictRec = res.FailedSyncRecords.Where(m => m.IsConflicted == true);

                if (conflictRec != null)
                {
                    _dBOperations.UpdateConflictedVersionIDs<T>(conflictRec, model);
                }

                // If any other error occurs rather than duplicate transactionID, versionID
                if (conflictRec.Count() != res.FailedSyncRecords.Count)
                {
                    var errRec = res.FailedSyncRecords.Where(m => m.IsConflicted == false).FirstOrDefault();

                    if (errRec != null)
                    {
                        // Displaying the error to the user
                        throw new Exception(errRec.ExceptionMessage);
                    }
                }
            }
            /* FailedTransactionIDs conflict is highly rare to occur, as TransactionID is combination of GUID + DeviceID + Ticks
            Uncomment Client.DB.SyncUtility.FailedTransactionsSync, Client.DB.SQLiteDBOPerations.UpdateConflictedTransationIDs,
            Client.DB.IDBOperations.UpdateConflictedTransationIDs, Client.DB.SyncUtility.PostDataAsync, Server.DB.SQLServerDBOperations.InsertUpdate
            DomainModel.Models.APIModel */
            if (res.FailedSyncRecords.Count > 0)//|| res.FailedTransactionIDs.Count > 0)
            {
                model.FailedSyncRecords.Clear();
                //model.FailedTransactionIDs.Clear();
                //model.FailedTrasationData = null;
                await PostDataAsync(model, syncSettingsModel, false);
                //await StartSyncAsync();
            }
            else if (res.FailedTrasationData != null)
            {
                var list = JsonConvert.DeserializeObject<List<T>>(res.FailedTrasationData.ToString());

                List<string> transactionList = list.Select(m => m.TransactionID).Distinct().ToList();

                // Settting IsSync as true,transationid as null in DB for 'serverList' records
                // Deleting the local records if SyncClientToServerAndHardDelete
                _dBOperations.UpdateTrasationSuccess<T>(transactionList, model.SyncType);
            }

            return res;
        }

        internal async Task PostDataAsync(APIModel model, ISyncSettingsBaseModel settings, bool isTransactionIDSet)
        {
            List<T> clientList = (List<T>)model.Data;

            if(isTransactionIDSet)
            {
                _dBOperations.SetTransationIDs(clientList, model.DeviceID);
            }
          
            //TODO Pagination
            SyncAPIUtility syncAPI = new SyncAPIUtility(SyncGlobalConfig.APIUrl, SyncGlobalConfig.Token);

            var data = await syncAPI.Post<APIModel, APIModel>(model, StringUtility.PostData);
            /* 
              FailedTransactionIDs conflict is highly rare to occur, as TransactionID is combination of GUID + DeviceID + Ticks
              Uncomment Client.DB.SyncUtility.FailedTransactionsSync, Client.DB.SQLiteDBOPerations.UpdateConflictedTransationIDs,
              Client.DB.IDBOperations.UpdateConflictedTransationIDs, Client.DB.SyncUtility.PostDataAsync, Server.DB.SQLServerDBOperations.InsertUpdate
              DomainModel.Models.APIModel
            */
            if (data.FailedSyncRecords.Count > 0)//|| data.FailedTransactionIDs.Count> 0)
            {
                await FailedTransactionsSync(data, settings, false);
            }
            else
            {
                List<string> transactionList = clientList.Select(m => m.TransactionID).Distinct().ToList();
                _dBOperations.UpdateTrasationSuccess<T>(transactionList, model.SyncType);
            }
        }
    }
}