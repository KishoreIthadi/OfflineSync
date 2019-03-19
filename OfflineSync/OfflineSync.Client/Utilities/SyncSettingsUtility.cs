using OfflineSync.Client.DB;
using OfflineSync.Client.Enums;
using OfflineSync.Client.Models;
using OfflineSync.Client.Models.BaseModels;
using OfflineSync.DomainModel.Enums;
using OfflineSync.DomainModel.Utilities;
using System;

namespace OfflineSync.Client.Utilities
{
    public class SyncSettingsUtility
    {
        IDBOperations _dBOperations;

        public SyncSettingsUtility()
        {
            switch (SyncGlobalConfig.DBType)
            {
                case ClientDBType.SQLite:
                    _dBOperations = new SQLiteDBOperations();
                    break;
            }
        }

        public void Update(ISyncSettingsBaseModel model)
        {
            _dBOperations.UpdateSyncSettingsModel(model);
        }

        public void Add(ISyncSettingsBaseModel model)
        {
            if (model.SyncType == SyncType.SyncClientToServer && !model.AutoSync)
            {
                throw new Exception(StringUtility.AutoSyncforCTS);
            }
            else if (model.SyncType == SyncType.SyncClientToServerAndHardDelete && !model.AutoSync )
            {
                throw new Exception(StringUtility.AutoSyncforCTSH);
            }
            else if(!model.AutoSync && (model.ControllerData == null || model.ControllerRoute == null))
            {
                throw new Exception(StringUtility.ControllerSettingsError);
            }
            else
            {
                _dBOperations.AddSyncSettingsModel(model);
            }           
        }
    }
}
