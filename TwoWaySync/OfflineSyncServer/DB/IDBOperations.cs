using System.Collections.Generic;

namespace OfflineSyncServer.DB
{
    public interface IDBOperations<T>
    {
         List<T> GetData();
    }
}