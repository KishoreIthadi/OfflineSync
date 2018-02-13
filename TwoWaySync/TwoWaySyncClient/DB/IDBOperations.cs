using System.Collections.Generic;
using TwoWaySync.DomainModel.Models;
using TwoWaySyncClient.Models;

namespace TwoWaySyncClient.DB
{
    public interface IDBOperations
    {
        List<T> GetData<T>() where T : ISyncBaseModel, new();

        SyncSettings GetSyncSettingByTable(string tableName);
    }
}