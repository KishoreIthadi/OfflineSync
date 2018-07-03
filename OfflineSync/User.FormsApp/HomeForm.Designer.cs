namespace User.FormsApp
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClientAddSettings = new System.Windows.Forms.Button();
            this.btnClientRefresh = new System.Windows.Forms.Button();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.btnClientAdd = new System.Windows.Forms.Button();
            this.btnClientUpdate = new System.Windows.Forms.Button();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.lblClientLastSyncDateTime = new System.Windows.Forms.Label();
            this.btnClientSync = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblErrorVal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSeverVal = new System.Windows.Forms.Label();
            this.txtServerID = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.btnServerUpdate = new System.Windows.Forms.Button();
            this.btnServerAdd = new System.Windows.Forms.Button();
            this.btnServerRefresh = new System.Windows.Forms.Button();
            this.btnAddGlobalConfig = new System.Windows.Forms.Button();
            this.btnAddDeviceID = new System.Windows.Forms.Button();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.lblClientVal = new System.Windows.Forms.Label();
            this.lblDeviceID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClientAddSettings
            // 
            this.btnClientAddSettings.Location = new System.Drawing.Point(403, 12);
            this.btnClientAddSettings.Name = "btnClientAddSettings";
            this.btnClientAddSettings.Size = new System.Drawing.Size(100, 35);
            this.btnClientAddSettings.TabIndex = 0;
            this.btnClientAddSettings.Text = "Add Settings";
            this.btnClientAddSettings.UseVisualStyleBackColor = true;
            this.btnClientAddSettings.Click += new System.EventHandler(this.btnClientAddSettings_Click);
            // 
            // btnClientRefresh
            // 
            this.btnClientRefresh.Location = new System.Drawing.Point(89, 78);
            this.btnClientRefresh.Name = "btnClientRefresh";
            this.btnClientRefresh.Size = new System.Drawing.Size(72, 35);
            this.btnClientRefresh.TabIndex = 1;
            this.btnClientRefresh.Text = "Refresh";
            this.btnClientRefresh.UseVisualStyleBackColor = true;
            this.btnClientRefresh.Click += new System.EventHandler(this.btnClientRefresh_Click);
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AllowUserToOrderColumns = true;
            this.dgvRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Location = new System.Drawing.Point(16, 227);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowTemplate.Height = 24;
            this.dgvRecords.Size = new System.Drawing.Size(782, 547);
            this.dgvRecords.TabIndex = 2;
            this.dgvRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecords_CellClick);
            // 
            // btnClientAdd
            // 
            this.btnClientAdd.Location = new System.Drawing.Point(336, 158);
            this.btnClientAdd.Name = "btnClientAdd";
            this.btnClientAdd.Size = new System.Drawing.Size(75, 35);
            this.btnClientAdd.TabIndex = 3;
            this.btnClientAdd.Text = "Add";
            this.btnClientAdd.UseVisualStyleBackColor = true;
            this.btnClientAdd.Click += new System.EventHandler(this.btnClientAdd_Click);
            // 
            // btnClientUpdate
            // 
            this.btnClientUpdate.Enabled = false;
            this.btnClientUpdate.Location = new System.Drawing.Point(427, 158);
            this.btnClientUpdate.Name = "btnClientUpdate";
            this.btnClientUpdate.Size = new System.Drawing.Size(75, 35);
            this.btnClientUpdate.TabIndex = 4;
            this.btnClientUpdate.Text = "Update";
            this.btnClientUpdate.UseVisualStyleBackColor = true;
            this.btnClientUpdate.Click += new System.EventHandler(this.btnClientUpdate_Click);
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(336, 117);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(166, 22);
            this.txtClientName.TabIndex = 5;
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(336, 84);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.ReadOnly = true;
            this.txtClientID.Size = new System.Drawing.Size(166, 22);
            this.txtClientID.TabIndex = 6;
            // 
            // lblClientLastSyncDateTime
            // 
            this.lblClientLastSyncDateTime.AutoSize = true;
            this.lblClientLastSyncDateTime.Location = new System.Drawing.Point(13, 137);
            this.lblClientLastSyncDateTime.Name = "lblClientLastSyncDateTime";
            this.lblClientLastSyncDateTime.Size = new System.Drawing.Size(131, 17);
            this.lblClientLastSyncDateTime.TabIndex = 7;
            this.lblClientLastSyncDateTime.Text = "LastSync DateTime";
            // 
            // btnClientSync
            // 
            this.btnClientSync.Location = new System.Drawing.Point(12, 76);
            this.btnClientSync.Name = "btnClientSync";
            this.btnClientSync.Size = new System.Drawing.Size(63, 35);
            this.btnClientSync.TabIndex = 8;
            this.btnClientSync.Text = "Sync";
            this.btnClientSync.UseVisualStyleBackColor = true;
            this.btnClientSync.Click += new System.EventHandler(this.btnClientSync_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(825, 227);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(864, 547);
            this.dataGridView1.TabIndex = 11;
            // 
            // lblErrorVal
            // 
            this.lblErrorVal.AutoSize = true;
            this.lblErrorVal.ForeColor = System.Drawing.Color.Red;
            this.lblErrorVal.Location = new System.Drawing.Point(661, 30);
            this.lblErrorVal.Name = "lblErrorVal";
            this.lblErrorVal.Size = new System.Drawing.Size(0, 17);
            this.lblErrorVal.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 24);
            this.label3.TabIndex = 22;
            this.label3.Text = "Client";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(821, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 24);
            this.label4.TabIndex = 23;
            this.label4.Text = "Server";
            // 
            // lblSeverVal
            // 
            this.lblSeverVal.AutoSize = true;
            this.lblSeverVal.Location = new System.Drawing.Point(1399, 166);
            this.lblSeverVal.Name = "lblSeverVal";
            this.lblSeverVal.Size = new System.Drawing.Size(0, 17);
            this.lblSeverVal.TabIndex = 28;
            // 
            // txtServerID
            // 
            this.txtServerID.Location = new System.Drawing.Point(1224, 85);
            this.txtServerID.Name = "txtServerID";
            this.txtServerID.ReadOnly = true;
            this.txtServerID.Size = new System.Drawing.Size(167, 22);
            this.txtServerID.TabIndex = 27;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(1224, 118);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(167, 22);
            this.txtServerName.TabIndex = 26;
            // 
            // btnServerUpdate
            // 
            this.btnServerUpdate.Location = new System.Drawing.Point(1316, 158);
            this.btnServerUpdate.Name = "btnServerUpdate";
            this.btnServerUpdate.Size = new System.Drawing.Size(75, 35);
            this.btnServerUpdate.TabIndex = 25;
            this.btnServerUpdate.Text = "Update";
            this.btnServerUpdate.UseVisualStyleBackColor = true;
            this.btnServerUpdate.Click += new System.EventHandler(this.btnServerUpdate_Click);
            // 
            // btnServerAdd
            // 
            this.btnServerAdd.Location = new System.Drawing.Point(1225, 158);
            this.btnServerAdd.Name = "btnServerAdd";
            this.btnServerAdd.Size = new System.Drawing.Size(75, 35);
            this.btnServerAdd.TabIndex = 24;
            this.btnServerAdd.Text = "Add";
            this.btnServerAdd.UseVisualStyleBackColor = true;
            this.btnServerAdd.Click += new System.EventHandler(this.btnServerAdd_Click);
            // 
            // btnServerRefresh
            // 
            this.btnServerRefresh.Location = new System.Drawing.Point(1098, 89);
            this.btnServerRefresh.Name = "btnServerRefresh";
            this.btnServerRefresh.Size = new System.Drawing.Size(84, 35);
            this.btnServerRefresh.TabIndex = 29;
            this.btnServerRefresh.Text = "Refresh";
            this.btnServerRefresh.UseVisualStyleBackColor = true;
            this.btnServerRefresh.Click += new System.EventHandler(this.btnServerRefresh_Click);
            // 
            // btnAddGlobalConfig
            // 
            this.btnAddGlobalConfig.Location = new System.Drawing.Point(9, 12);
            this.btnAddGlobalConfig.Name = "btnAddGlobalConfig";
            this.btnAddGlobalConfig.Size = new System.Drawing.Size(135, 35);
            this.btnAddGlobalConfig.TabIndex = 30;
            this.btnAddGlobalConfig.Text = "Add Global Config";
            this.btnAddGlobalConfig.UseVisualStyleBackColor = true;
            this.btnAddGlobalConfig.Click += new System.EventHandler(this.btnAddGlobalConfig_Click);
            // 
            // btnAddDeviceID
            // 
            this.btnAddDeviceID.Location = new System.Drawing.Point(159, 12);
            this.btnAddDeviceID.Name = "btnAddDeviceID";
            this.btnAddDeviceID.Size = new System.Drawing.Size(111, 35);
            this.btnAddDeviceID.TabIndex = 31;
            this.btnAddDeviceID.Text = "Add DeviceID";
            this.btnAddDeviceID.UseVisualStyleBackColor = true;
            this.btnAddDeviceID.Click += new System.EventHandler(this.btnAddDeviceID_Click);
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(285, 12);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(102, 35);
            this.btnCreateTable.TabIndex = 32;
            this.btnCreateTable.Text = "Create Table";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // lblClientVal
            // 
            this.lblClientVal.AutoSize = true;
            this.lblClientVal.ForeColor = System.Drawing.Color.Green;
            this.lblClientVal.Location = new System.Drawing.Point(509, 166);
            this.lblClientVal.Name = "lblClientVal";
            this.lblClientVal.Size = new System.Drawing.Size(0, 17);
            this.lblClientVal.TabIndex = 33;
            // 
            // lblDeviceID
            // 
            this.lblDeviceID.AutoSize = true;
            this.lblDeviceID.Location = new System.Drawing.Point(13, 167);
            this.lblDeviceID.Name = "lblDeviceID";
            this.lblDeviceID.Size = new System.Drawing.Size(64, 17);
            this.lblDeviceID.TabIndex = 34;
            this.lblDeviceID.Text = "DeviceID";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 797);
            this.Controls.Add(this.lblDeviceID);
            this.Controls.Add(this.lblClientVal);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.btnAddDeviceID);
            this.Controls.Add(this.btnAddGlobalConfig);
            this.Controls.Add(this.btnServerRefresh);
            this.Controls.Add(this.lblSeverVal);
            this.Controls.Add(this.txtServerID);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.btnServerUpdate);
            this.Controls.Add(this.btnServerAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblErrorVal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnClientSync);
            this.Controls.Add(this.lblClientLastSyncDateTime);
            this.Controls.Add(this.txtClientID);
            this.Controls.Add(this.txtClientName);
            this.Controls.Add(this.btnClientUpdate);
            this.Controls.Add(this.btnClientAdd);
            this.Controls.Add(this.dgvRecords);
            this.Controls.Add(this.btnClientRefresh);
            this.Controls.Add(this.btnClientAddSettings);
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HomeForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HomeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClientAddSettings;
        private System.Windows.Forms.Button btnClientRefresh;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.Button btnClientAdd;
        private System.Windows.Forms.Button btnClientUpdate;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.Label lblClientLastSyncDateTime;
        private System.Windows.Forms.Button btnClientSync;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblErrorVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSeverVal;
        private System.Windows.Forms.TextBox txtServerID;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Button btnServerUpdate;
        private System.Windows.Forms.Button btnServerAdd;
        private System.Windows.Forms.Button btnServerRefresh;
        private System.Windows.Forms.Button btnAddGlobalConfig;
        private System.Windows.Forms.Button btnAddDeviceID;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Label lblClientVal;
        private System.Windows.Forms.Label lblDeviceID;
    }
}