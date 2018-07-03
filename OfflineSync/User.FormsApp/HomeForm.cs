using OfflineSync.Client.Enums;
using OfflineSync.Client.Models;
using OfflineSync.Client.Models.SQLite;
using OfflineSync.Client.Utilities;
using OfflineSync.DomainModel.Enums;
using SQLite;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace User.FormsApp
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
           
        }

        #region Client

        private void btnAddGlobalConfig_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                GlobalConfig.DBPath = @"SyncDB.db";
                GlobalConfig.Token = "";
                GlobalConfig.APIUrl = @"http://localhost:64115/API/";

                lblClientVal.Text = "Global config updated successfully";
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnAddDeviceID_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                new DeviceIDUtility().InitializeDeviceID();

                UpdateDeviceID();

                lblClientVal.Text = "DeviceID Created Sucessfully";
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(GlobalConfig.DBPath);
                db.CreateTable<tblTestACTS>();
                db.CreateTable<tblTestACTSH>();
                db.CreateTable<tblTestASTC>();
                db.CreateTable<tblTestATWS>();
                db.CreateTable<tblTestCTS>();
                db.CreateTable<tblTestCTSH>();
                db.CreateTable<tblTestSTC>();
                db.CreateTable<tblTestTWS>();

                lblClientVal.Text = "Tables Created Sucessfully";
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnClientAddSettings_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                SyncSettingsUtility syncSettings = new SyncSettingsUtility();

                // Autosync is true
                // Sync type is client to server
                syncSettings.Add(
                    new SQLiteSyncSettingsModel
                    {
                        AutoSync = true,
                        ClientTableName = typeof(tblTestACTS).Name,
                        Priority = OveridePriority.LastUpdated,
                        SyncType = SyncType.SyncClientToServer,
                        ServerAssemblyName = "User.APIApp",
                        ServerTableName = "tblTestACTS"
                    }
                );

                syncSettings.Add(
                    new SQLiteSyncSettingsModel
                    {
                        AutoSync = true,
                        ClientTableName = typeof(tblTestACTSH).Name,
                        Priority = OveridePriority.LastUpdated,
                        SyncType = SyncType.SyncClientToServerAndHardDelete,
                        ServerAssemblyName = "User.APIApp",
                        ServerTableName = "tblTestACTSH"
                    }
                );

                syncSettings.Add(
                    new SQLiteSyncSettingsModel
                    {
                        AutoSync = true,
                        ClientTableName = typeof(tblTestASTC).Name,
                        Priority = OveridePriority.LastUpdated,
                        SyncType = SyncType.SyncServerToClient,
                        ServerAssemblyName = "User.APIApp",
                        ServerTableName = "tblTestASTC"
                    }
                );

                syncSettings.Add(
                   new SQLiteSyncSettingsModel
                   {
                       AutoSync = true,
                       ClientTableName = typeof(tblTestATWS).Name,
                       Priority = OveridePriority.LastUpdated,
                       SyncType = SyncType.SyncTwoWay,
                       ServerAssemblyName = "User.APIApp",
                       ServerTableName = "tblTestATWS"
                   }
               );

                // Autosync is false
                // Sync type is client to server
                syncSettings.Add(
                    new SQLiteSyncSettingsModel
                    {
                        AutoSync = true,
                        ClientTableName = typeof(tblTestCTS).Name,
                        Priority = OveridePriority.LastUpdated,
                        SyncType = SyncType.SyncClientToServer,
                        ServerAssemblyName = "User.APIApp",
                        ServerTableName = "tblTestCTS"
                    }
                );

                syncSettings.Add(
                    new SQLiteSyncSettingsModel
                    {
                        AutoSync = true,
                        ClientTableName = typeof(tblTestCTSH).Name,
                        Priority = OveridePriority.LastUpdated,
                        SyncType = SyncType.SyncClientToServerAndHardDelete,
                        ServerAssemblyName = "User.APIApp",
                        ServerTableName = "tblTestCTSH"
                    }
                );

                syncSettings.Add(
                    new SQLiteSyncSettingsModel
                    {
                        AutoSync = true,
                        ClientTableName = typeof(tblTestSTC).Name,
                        Priority = OveridePriority.LastUpdated,
                        SyncType = SyncType.SyncServerToClient,
                        ServerAssemblyName = "User.APIApp",
                        ServerTableName = "tblTestSTC"
                    }
                );

                syncSettings.Add(
                   new SQLiteSyncSettingsModel
                   {
                       AutoSync = true,
                       ClientTableName = typeof(tblTestTWS).Name,
                       Priority = OveridePriority.LastUpdated,
                       SyncType = SyncType.SyncTwoWay,
                       ServerAssemblyName = "User.APIApp",
                       ServerTableName = "tblTestTWS"
                   }
               );

                lblClientVal.Text = "Settings Added Sucessfully";
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnClientRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                UpdateClientGrid();
                UpdateLastSyncDateTime();
                UpdateDeviceID();
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
                {
                    conn.Insert(new tblTestACTS()
                    {
                        Name = txtClientName.Text
                    });
                }

                UpdateClientGrid();

                lblClientVal.Text = "Record Added Sucessfully";

                txtClientName.Text = string.Empty;

                UpdateLastSyncDateTime();
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnClientUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
                {
                    var rec = conn.Table<tblTestACTS>().ToList().Where(m => m.ID == Convert.ToInt16(txtClientID.Text)).FirstOrDefault();
                    rec.Name = txtClientName.Text;
                    conn.Update(rec);
                }

                UpdateClientGrid();

                lblClientVal.Text = "Record Updated Sucessfully";

                txtClientID.Text = string.Empty;
                txtClientName.Text = string.Empty;

                btnClientUpdate.Enabled = false;
                btnClientAdd.Enabled = true;

                UpdateLastSyncDateTime();
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void btnClientSync_Click(object sender, EventArgs e)
        {
            try
            {
                ClearClientValidations();

                SyncUtility<tblTestACTS> syncUtility = new SyncUtility<tblTestACTS>();
                syncUtility.StartSyncAsync();

                lblClientVal.Text = "Sync Sucessfully";

                UpdateLastSyncDateTime();
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void dgvRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearClientValidations();

                if (e.RowIndex > 0)
                {
                    DataGridViewRow selectedRow = dgvRecords.Rows[e.RowIndex];

                    txtClientID.Text = selectedRow.Cells["ID"].Value.ToString();
                    txtClientName.Text = selectedRow.Cells["Name"].Value.ToString();

                    btnClientUpdate.Enabled = true;
                    btnClientAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void UpdateLastSyncDateTime()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
                {
                    var rec = conn.Table<SQLiteSyncSettingsModel>().ToList().Where(m => m.ClientTableName == "tblTestACTS").FirstOrDefault();

                    if (rec != null)
                    {
                        lblClientLastSyncDateTime.Text = rec.LastSyncedAt;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void UpdateDeviceID()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
                {
                    var rec = conn.Table<SQLiteConfigurationsModel>()
                                          .ToList()
                                          .Where(m => m.Key == "DeviceID")
                                          .FirstOrDefault();

                    if (rec != null)
                    {
                        lblDeviceID.Text = rec.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void UpdateClientGrid()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(GlobalConfig.DBPath))
                {
                    var list = conn.Table<tblTestACTS>().ToList();
                    dgvRecords.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                lblErrorVal.Text = ex.Message;
            }
        }

        private void ClearClientValidations()
        {
            lblErrorVal.Text = string.Empty;
            lblClientVal.Text = string.Empty;
        }

        #endregion Client


        #region Server

        private void btnServerAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnServerUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnServerRefresh_Click(object sender, EventArgs e)
        {

        }

        #endregion Server
    }
}
