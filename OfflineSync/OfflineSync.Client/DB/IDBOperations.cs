using System.Collections.Generic;
using System;
using OfflineSync.DomainModel.Models;
using OfflineSync.DomainModel.Enums;
using OfflineSync.Client.Models.BaseModels;

namespace OfflineSync.Client.DB
{
    internal interface IDBOperations
    {
        List<ISyncSettingsBaseModel> GetSyncSettingByTable<T>(string tableName);

        string GetConfigurationByKey(string key);

        void InsertConfigurationsModel(string key, string value);


        List<T> GetFailedTransactionData<T>() where T : ISyncClientBaseModel, new();
        void UpdateConflictedTransationIDs<T>(List<string> transactionIDs, string deviceID) where T : ISyncClientBaseModel, new();
        void UpdateConflictedVersionIDs<T>(IEnumerable<FailedRecordsModel> conflictRecs, string deviceID) where T : ISyncClientBaseModel, new();






        List<T> GetData<T>(DateTime? lastsync) where T : ISyncClientBaseModel, new();
        void InsertList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new();
        void UpdateList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new();
        void UpdateLastSync(int syncSettingsID, DateTime lastModified);
        void UpdateTrasationSuccess<T>(List<string> transactionList, SyncType syncType) where T : ISyncClientBaseModel, new();
        void SetTransationIDs<T>(List<T> clientList,string deviceID) where T : ISyncClientBaseModel, new();
        void UpdateSyncSettingsModel(ISyncSettingsBaseModel model);
        void AddSyncSettingsModel(ISyncSettingsBaseModel model);
    }
}