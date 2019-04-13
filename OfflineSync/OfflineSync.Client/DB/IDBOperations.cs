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

        /* 
          FailedTransactionIDs conflict is highly rare to occur, as TransactionID is combination of GUID + DeviceID + Ticks
          Uncomment Client.DB.SyncUtility.FailedTransactionsSync, Client.DB.SQLiteDBOPerations.UpdateConflictedTransationIDs,
          Client.DB.IDBOperations.UpdateConflictedTransationIDs, Client.DB.SyncUtility.PostDataAsync, Server.DB.SQLServerDBOperations.InsertUpdate
          DomainModel.Models.APIModel 
        */
        //void UpdateConflictedTransationIDs<T>(List<string> transactionIDs, APIModel model ) where T : ISyncClientBaseModel, new();

        void UpdateConflictedVersionIDs<T>(IEnumerable<FailedRecordsModel> conflictRecs, APIModel model) where T : ISyncClientBaseModel, new();
        List<T> GetData<T>(DateTime? lastsync) where T : ISyncClientBaseModel, new();
        void InsertList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new();
        void UpdateList<T>(IEnumerable<T> list) where T : ISyncClientBaseModel, new();
        void UpdateLastSync(int syncSettingsID, DateTime lastModified);
        void UpdateTrasationSuccess<T>(List<string> transactionList, SyncType syncType) where T : ISyncClientBaseModel, new();
        void UpdateTrasationSuccess<T>(string transactionID, SyncType syncType) where T : ISyncClientBaseModel, new();
        void SetTransationIDs<T>(List<T> clientList, string deviceID) where T : ISyncClientBaseModel, new();
        void UpdateSyncSettingsModel(ISyncSettingsBaseModel model);
        void AddSyncSettingsModel(ISyncSettingsBaseModel model);
        FailedTrasationModel GetFailedTransactionDetails<T>() where T : ISyncClientBaseModel, new();
        void UpdateTrasation();
    }
}