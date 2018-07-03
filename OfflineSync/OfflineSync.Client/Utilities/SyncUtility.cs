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
            switch (GlobalConfig.DBType)
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
                List<ISyncSettingsBaseModel> settingslist = _dBOperations.GetSyncSettingByTable< ISyncSettingsBaseModel>(typeof(T).Name);

                SyncAPIUtility syncAPI = new SyncAPIUtility(GlobalConfig.APIUrl, GlobalConfig.Token);

                // Having dublicate entries
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
                        model = await FailedTransactionsSync(model, settings);
                    }
                    else if (model.SyncType == SyncType.SyncTwoWay || model.SyncType == SyncType.SyncServerToClient)
                    {
                        string url = model.ControllerRoute;

                        if (model.AutoSync && model.FailedTrasationData == null)
                        {
                            url = StringUtility.GetData;
                        }
                        //else if (!model.AutoSync)
                        //{
                        //    // Calling the client API method and fetching the data
                        //    url = model.ControllerRoute;
                        //}

                        model = await syncAPI.Post<APIModel, APIModel>(model, url);
                    }

                    List<T> serverList = null;
                    List<T> insertList = null;
                    List<T> modifiedList = null;

                    if (model.Data != null)
                    {
                        serverList = JsonConvert.DeserializeObject<List<T>>(model.Data.ToString());

                        //TODO need to check for null logic, if date can be null in any scenario
                        //var lastSyncedAt = serverList.OrderByDescending(m => m.SyncModifiedAt)
                        //                  .FirstOrDefault().SyncModifiedAt;
                        //_dBOperations.UpdateLastSync(settings.SyncSettingsID, Convert.ToDateTime(lastSyncedAt));
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

                        if (insertList != null)
                        {
                            _dBOperations.InsertList<T>(insertList);
                        }
                        if (modifiedList != null)
                        {
                            _dBOperations.UpdateList<T>(modifiedList);
                        }
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
                                await PostDataAsync(model);

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
                                    var updatedRec = clientList.Where(m => m.VersionID == item.VersionID).FirstOrDefault();

                                    // If record is modified both at the server and at the client
                                    if (updatedRec != null)
                                    {
                                        if (settings.Priority == OveridePriority.LastUpdated)
                                        {
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
                            }

                            if (insertList != null)
                            {
                                _dBOperations.InsertList<T>(insertList);
                            }
                            if (modifiedList != null)
                            {
                                _dBOperations.UpdateList<T>(modifiedList);
                            }

                            if (clientList != null)
                            {
                                model.Data = clientList;
                                PostDataAsync(model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<APIModel> FailedTransactionsSync(APIModel model, ISyncSettingsBaseModel syncSettingsModel)
        {
            SyncAPIUtility syncAPI = new SyncAPIUtility(GlobalConfig.APIUrl, GlobalConfig.Token);

            APIModel res = await syncAPI.Post<APIModel, APIModel>(model, StringUtility.GetData);

            if (res.FailedTransactionIDs != null)
            {
                // Updating existing TransactionID since it already exist in server DB
                _dBOperations.UpdateTransationIDs<T>(res.FailedTransactionIDs);
            }

            if (res.FailedSyncRecords != null)
            {
                // Updating versionID since it already exist in server DB
                var conflictRec = res.FailedSyncRecords.Where(m => m.IsConflicted == true);

                if (conflictRec != null)
                {
                    _dBOperations.UpdateConflictVersionIDs<T>(conflictRec);
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

            if (res.FailedTransactionIDs != null || res.FailedSyncRecords != null)
            {
                await StartSyncAsync();
            }
            else if (res.FailedTrasationData != null)
            {
                var list = (List<T>)res.FailedTrasationData;

                List<string> transactionList = list.Select(m => m.TransactionID).Distinct().ToList();

                // Settting IsSync as null,transationid as null in DB for 'serverList' records
                // Deleting the local records if SyncClientToServerAndHardDelete
                _dBOperations.UpdateTrasationSuccess<T>(transactionList, model.SyncType);
            }

            return res;
        }

        internal async Task<APIModel> GetAPIModel(ISyncSettingsBaseModel settings)
        {
            APIModel model = new APIModel();

            string deviceID = await new DeviceIDUtility().GetDeviceID();

            List<T> failedTransactionData = null;

            if (settings.SyncType != SyncType.SyncServerToClient)
            {
                failedTransactionData = _dBOperations.GetFailedTransactionData<T>();
            }

            model.FailedTrasationData = failedTransactionData;
            model.DeviceID = deviceID;
            model.LastSyncDate = Convert.ToDateTime(settings.LastSyncedAt);
            model.ServerTableName = settings.ServerTableName;
            model.SyncType = settings.SyncType;
            model.AutoSync = settings.AutoSync;
            model.ServerAssemblyName = settings.ServerAssemblyName;

            // checking if the sync is autosync
            if (!settings.AutoSync)
            {
                model.ControllerData = settings.Data;
                model.ControllerRoute = settings.ControllerRoute;
            }

            return model;
        }

        internal async Task PostDataAsync(APIModel model)
        {
            List<T> clientList = (List<T>)model.Data;

            _dBOperations.UpdateIDS(clientList);

            //TODO Pagination
            SyncAPIUtility syncAPI = new SyncAPIUtility(GlobalConfig.APIUrl, GlobalConfig.Token);

            await syncAPI.Post<APIModel, APIModel>(model, StringUtility.PostData);

            List<string> transactionList = clientList.Select(m => m.TransactionID).Distinct().ToList();

            _dBOperations.UpdateTrasationSuccess<T>(transactionList, model.SyncType);
        }
    }
}