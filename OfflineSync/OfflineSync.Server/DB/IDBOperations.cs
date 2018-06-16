using OfflineSync.Server.Models;
using System;
using System.Collections.Generic;

namespace OfflineSync.Server.DB
{
    public interface IDBOperations
    {
        List<T> GetData<T>() where T : class, ISyncServerBaseModel;

        List<T> GetDataByLastSyncDate<T>(DateTime dt) where T : class, ISyncServerBaseModel;

        List<string> GetFailedTransactionInfo(List<string> transactionIDs, string deviceID);
    }
}