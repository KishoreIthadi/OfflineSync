using System.Collections.Generic;
using System;
using OfflineSync.DomainModel.Models;
using OfflineSync.DomainModel.Enums;
using OfflineSync.Client.Models.BaseModels;

namespace OfflineSync.Client.DB
{
    internal interface IDBOperations
    {
        List<T> GetFailedTransactionData<T>() where T : ISyncClientBaseModel, new();
        void UpdateTransationIDs<T>(string[] transactionIDs) where T : ISyncClientBaseModel, new();
        void UpdateConflictVersionIDs<T>(IEnumerable<FailedRecordsModel> conflictRecs) where T : ISyncClientBaseModel, new();
        List<T> GetData<T>(DateTime? lastsync) where T : ISyncClientBaseModel, new();
        void InsertList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new();
        void UpdateList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new();
        void UpdateTrasationSuccess<T>(List<string> transactionList, SyncType syncType) where T : ISyncClientBaseModel, new();
        void UpdateIDS<T>(List<T> clientList) where T : ISyncClientBaseModel, new();

        string GetConfigurationsByKey(string key);
        void InsertConfigurationsModel(string key, string value);
        void UpdateLastSync(int syncSettingsID, DateTime lastModified);
        List<ISyncSettingsBaseModel> GetSyncSettingByTable<T>(string tableName);
        void UpdateSyncSettingsModel(ISyncSettingsBaseModel model);
        void AddSyncSettingsModel(ISyncSettingsBaseModel model);
    }
}