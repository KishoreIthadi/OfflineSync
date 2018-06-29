using OfflineSync.Client.Enums;
using OfflineSync.Client.Models;
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

        string _DBPath = @"SyncDB.db";
        string _APIURL = @"http://localhost:64115/API/";

        private void HomeForm_Load(object sender, EventArgs e)
        {
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(_DBPath);
            db.CreateTable<tblTestACTS>();
            db.CreateTable<tblTestACTSH>();
            db.CreateTable<tblTestASTC>();
            db.CreateTable<tblTestATWS>();
            db.CreateTable<tblTestCTS>();
            db.CreateTable<tblTestCTSH>();
            db.CreateTable<tblTestSTC>();
            db.CreateTable<tblTestTWS>();
        }

        #region Client

        private void btnClientAddSettings_Click(object sender, EventArgs e)
        {
            SyncSettings syncSettings = new SyncSettings(_DBPath);

            // Autosync is true
            // Sync type is client to server
            syncSettings.Add(
                new SyncSettingsModel
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
                new SyncSettingsModel
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
                new SyncSettingsModel
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
               new SyncSettingsModel
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
                new SyncSettingsModel
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
                new SyncSettingsModel
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
                new SyncSettingsModel
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
               new SyncSettingsModel
               {
                   AutoSync = true,
                   ClientTableName = typeof(tblTestTWS).Name,
                   Priority = OveridePriority.LastUpdated,
                   SyncType = SyncType.SyncTwoWay,
                   ServerAssemblyName = "User.APIApp",
                   ServerTableName = "tblTestTWS"
               }
           );
        }

        private void btnClientRefresh_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var list = conn.Table<tblTestACTS>().ToList();

                dgvRecords.DataSource = list;
            }

            UpdateLastSyncDateTime();
        }

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                conn.Insert(new tblTestACTS()
                {
                    Name = txtClientName.Text
                });
            }

            UpdateLastSyncDateTime();
        }

        private void btnClientUpdate_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var rec = conn.Table<tblTestACTS>().ToList().Where(m => m.ID == Convert.ToInt16(txtClientID.Text)).FirstOrDefault();

                rec.Name = txtClientName.Text;

                conn.Update(rec);

                UpdateLastSyncDateTime();
            }
        }

        private void UpdateLastSyncDateTime()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_DBPath))
            {
                var rec = conn.Table<SyncSettingsModel>().ToList().Where(m => m.ClientTableName == "tblTestACTS").FirstOrDefault();

                lblClientLastSyncDateTime.Text = rec.LastSyncedAt;
            }
        }

        private void btnClientSync_Click(object sender, EventArgs e)
        {
            SyncUtility<tblTestACTS> syncUtility = new SyncUtility<tblTestACTS>(_DBPath, _APIURL, null);
            syncUtility.StartSyncAsync();

            UpdateLastSyncDateTime();
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
