using System.Collections.Generic;

namespace TwoWaySyncServer.DB
{
    public interface IDBOperations<T>
    {
         List<T> GetData();
    }
}