using System.Collections.Generic;
using OfflineSync.DomainModel.Models;
using OfflineSyncClient.Models;

namespace OfflineSyncClient.DB
{
    public interface IDBOperations
    {
        List<T> GetData<T>() where T : ISyncBaseModel, new();

        List<SyncSettingsModel> GetSyncSettingByTable(string tableName);
    }
}