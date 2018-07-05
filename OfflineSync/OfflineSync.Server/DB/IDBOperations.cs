using OfflineSync.DomainModel.Models;
using OfflineSync.Server.Models;
using System;
using System.Collections.Generic;

namespace OfflineSync.Server.DB
{
    public interface IDBOperations
    {
        List<string> GetFailedTransactionInfo(List<string> transactionIDs, string deviceID);

        List<T> GetData<T>() where T : class, ISyncServerBaseModel;

        List<T> GetDataByLastSyncDate<T>(DateTime dt) where T : class, ISyncServerBaseModel;

        void UpdateFailedTransactions<T>(APIModel model) where T : class, ISyncServerBaseModel;

        void InsertUpdate<T>(APIModel model) where T : class, ISyncServerBaseModel;

        string GetDeviceID();

    }
}