using OfflineSync.DomainModel.Models;
using OfflineSync.Server.Models.SQLServer;
using System;
using System.Collections.Generic;

namespace OfflineSync.Server.DB
{
    public interface IDBOperations
    {
        List<string> GetFailedTransactionInfo(List<string> transactionIDs, string deviceID);

        List<T> GetData<T>() where T : class, ISQLSyncServerModel;

        List<T> GetDataByLastSyncDate<T>(DateTime dt) where T : class, ISQLSyncServerModel;

        void UpdateFailedTransactions<T>(APIModel model) where T : class, ISQLSyncServerModel;

        void InsertUpdate<T>(APIModel model) where T : class, ISQLSyncServerModel;

        string GetDeviceID();
    }
}