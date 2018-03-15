using System;
using System.Collections.Generic;

namespace OfflineSyncServer.DB
{
    public interface IDBOperations<T> where T : class
    {
        List<T> GetData();

        List<T> GetDataByLastSyncDate(DateTime dt);
    }
}