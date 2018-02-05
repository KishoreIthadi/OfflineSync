using System.Collections.Generic;
using TwoWaySync.DomainModel.Models;

namespace TwoWaySyncClient.DB
{
    public interface IDBOperations<T> where T : ISyncBaseModel, new()
    {
        List<T> GetData();
    }
}