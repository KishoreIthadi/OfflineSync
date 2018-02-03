using System.Collections.Generic;

namespace TwoWaySyncClient.DB
{
    public interface IDBOperations<T>
    {
         List<T> GetData();
    }
}