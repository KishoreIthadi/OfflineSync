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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClientAddSettings = new System.Windows.Forms.Button();
            this.btnClientRefresh = new System.Windows.Forms.Button();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.btnClientAdd = new System.Windows.Forms.Button();
            this.btnClientUpdate = new System.Windows.Forms.Button();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.lblClientLastSyncDateTime = new System.Windows.Forms.Label();
            this.btnClientSync = new System.Windows.Forms.Button();
            this.lblClientErrorVal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblServerVal = new System.Windows.Forms.Label();
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
            this.cbTblType = new System.Windows.Forms.ComboBox();
            this.btnCreateServerTable = new System.Windows.Forms.Button();
            this.lblServerErrorVal = new System.Windows.Forms.Label();
            this.cbServerTableType = new System.Windows.Forms.ComboBox();
            this.btnClientReset = new System.Windows.Forms.Button();
            this.btnServerReset = new System.Windows.Forms.Button();
            this.dgvServerRecords = new System.Windows.Forms.DataGridView();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClientAddSettings
            // 
            this.btnClientAddSettings.Location = new System.Drawing.Point(302, 10);
            this.btnClientAddSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientAddSettings.Name = "btnClientAddSettings";
            this.btnClientAddSettings.Size = new System.Drawing.Size(75, 28);
            this.btnClientAddSettings.TabIndex = 0;
            this.btnClientAddSettings.Text = "Add Settings";
            this.btnClientAddSettings.UseVisualStyleBackColor = true;
            this.btnClientAddSettings.Click += new System.EventHandler(this.btnClientAddSettings_Click);
            // 
            // btnClientRefresh
            // 
            this.btnClientRefresh.Location = new System.Drawing.Point(105, 95);
            this.btnClientRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientRefresh.Name = "btnClientRefresh";
            this.btnClientRefresh.Size = new System.Drawing.Size(54, 28);
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
            this.dgvRecords.Location = new System.Drawing.Point(13, 311);
            this.dgvRecords.Margin = new System.Windows.Forms.Padding(2);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowTemplate.Height = 24;
            this.dgvRecords.Size = new System.Drawing.Size(555, 444);
            this.dgvRecords.TabIndex = 2;
            this.dgvRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecords_CellClick);
            // 
            // btnClientAdd
            // 
            this.btnClientAdd.Location = new System.Drawing.Point(32, 212);
            this.btnClientAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientAdd.Name = "btnClientAdd";
            this.btnClientAdd.Size = new System.Drawing.Size(56, 28);
            this.btnClientAdd.TabIndex = 3;
            this.btnClientAdd.Text = "Add";
            this.btnClientAdd.UseVisualStyleBackColor = true;
            this.btnClientAdd.Click += new System.EventHandler(this.btnClientAdd_Click);
            // 
            // btnClientUpdate
            // 
            this.btnClientUpdate.Enabled = false;
            this.btnClientUpdate.Location = new System.Drawing.Point(105, 212);
            this.btnClientUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientUpdate.Name = "btnClientUpdate";
            this.btnClientUpdate.Size = new System.Drawing.Size(56, 28);
            this.btnClientUpdate.TabIndex = 4;
            this.btnClientUpdate.Text = "Update";
            this.btnClientUpdate.UseVisualStyleBackColor = true;
            this.btnClientUpdate.Click += new System.EventHandler(this.btnClientUpdate_Click);
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(287, 60);
            this.txtClientID.Margin = new System.Windows.Forms.Padding(2);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.ReadOnly = true;
            this.txtClientID.Size = new System.Drawing.Size(197, 20);
            this.txtClientID.TabIndex = 6;
            // 
            // lblClientLastSyncDateTime
            // 
            this.lblClientLastSyncDateTime.AutoSize = true;
            this.lblClientLastSyncDateTime.Location = new System.Drawing.Point(410, 296);
            this.lblClientLastSyncDateTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClientLastSyncDateTime.Name = "lblClientLastSyncDateTime";
            this.lblClientLastSyncDateTime.Size = new System.Drawing.Size(100, 13);
            this.lblClientLastSyncDateTime.TabIndex = 7;
            this.lblClientLastSyncDateTime.Text = "LastSync DateTime";
            this.lblClientLastSyncDateTime.Click += new System.EventHandler(this.lblClientLastSyncDateTime_Click);
            // 
            // btnClientSync
            // 
            this.btnClientSync.Location = new System.Drawing.Point(571, 137);
            this.btnClientSync.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientSync.Name = "btnClientSync";
            this.btnClientSync.Size = new System.Drawing.Size(47, 28);
            this.btnClientSync.TabIndex = 8;
            this.btnClientSync.Text = "Sync";
            this.btnClientSync.UseVisualStyleBackColor = true;
            this.btnClientSync.Click += new System.EventHandler(this.btnClientSync_ClickAsync);
            // 
            // lblClientErrorVal
            // 
            this.lblClientErrorVal.AutoSize = true;
            this.lblClientErrorVal.ForeColor = System.Drawing.Color.Red;
            this.lblClientErrorVal.Location = new System.Drawing.Point(21, 140);
            this.lblClientErrorVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClientErrorVal.Name = "lblClientErrorVal";
            this.lblClientErrorVal.Size = new System.Drawing.Size(0, 13);
            this.lblClientErrorVal.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 252);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "Client";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(643, 252);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "Server";
            // 
            // lblServerVal
            // 
            this.lblServerVal.AutoSize = true;
            this.lblServerVal.ForeColor = System.Drawing.Color.Green;
            this.lblServerVal.Location = new System.Drawing.Point(914, 18);
            this.lblServerVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServerVal.Name = "lblServerVal";
            this.lblServerVal.Size = new System.Drawing.Size(0, 13);
            this.lblServerVal.TabIndex = 28;
            // 
            // txtServerID
            // 
            this.txtServerID.Location = new System.Drawing.Point(954, 59);
            this.txtServerID.Margin = new System.Windows.Forms.Padding(2);
            this.txtServerID.Name = "txtServerID";
            this.txtServerID.ReadOnly = true;
            this.txtServerID.Size = new System.Drawing.Size(200, 20);
            this.txtServerID.TabIndex = 27;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(954, 100);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(200, 20);
            this.txtServerName.TabIndex = 26;
            this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
            // 
            // btnServerUpdate
            // 
            this.btnServerUpdate.Enabled = false;
            this.btnServerUpdate.Location = new System.Drawing.Point(777, 212);
            this.btnServerUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnServerUpdate.Name = "btnServerUpdate";
            this.btnServerUpdate.Size = new System.Drawing.Size(56, 28);
            this.btnServerUpdate.TabIndex = 25;
            this.btnServerUpdate.Text = "Update";
            this.btnServerUpdate.UseVisualStyleBackColor = true;
            this.btnServerUpdate.Click += new System.EventHandler(this.btnServerUpdate_Click);
            // 
            // btnServerAdd
            // 
            this.btnServerAdd.Location = new System.Drawing.Point(702, 212);
            this.btnServerAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnServerAdd.Name = "btnServerAdd";
            this.btnServerAdd.Size = new System.Drawing.Size(56, 28);
            this.btnServerAdd.TabIndex = 24;
            this.btnServerAdd.Text = "Add";
            this.btnServerAdd.UseVisualStyleBackColor = true;
            this.btnServerAdd.Click += new System.EventHandler(this.btnServerAdd_Click);
            // 
            // btnServerRefresh
            // 
            this.btnServerRefresh.Location = new System.Drawing.Point(777, 96);
            this.btnServerRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnServerRefresh.Name = "btnServerRefresh";
            this.btnServerRefresh.Size = new System.Drawing.Size(56, 27);
            this.btnServerRefresh.TabIndex = 29;
            this.btnServerRefresh.Text = "Refresh";
            this.btnServerRefresh.UseVisualStyleBackColor = true;
            this.btnServerRefresh.Click += new System.EventHandler(this.btnServerRefresh_Click);
            // 
            // btnAddGlobalConfig
            // 
            this.btnAddGlobalConfig.Location = new System.Drawing.Point(7, 10);
            this.btnAddGlobalConfig.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddGlobalConfig.Name = "btnAddGlobalConfig";
            this.btnAddGlobalConfig.Size = new System.Drawing.Size(101, 28);
            this.btnAddGlobalConfig.TabIndex = 30;
            this.btnAddGlobalConfig.Text = "Add Global Config";
            this.btnAddGlobalConfig.UseVisualStyleBackColor = true;
            this.btnAddGlobalConfig.Click += new System.EventHandler(this.btnAddGlobalConfig_Click);
            // 
            // btnAddDeviceID
            // 
            this.btnAddDeviceID.Location = new System.Drawing.Point(119, 10);
            this.btnAddDeviceID.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddDeviceID.Name = "btnAddDeviceID";
            this.btnAddDeviceID.Size = new System.Drawing.Size(83, 28);
            this.btnAddDeviceID.TabIndex = 31;
            this.btnAddDeviceID.Text = "Add DeviceID";
            this.btnAddDeviceID.UseVisualStyleBackColor = true;
            this.btnAddDeviceID.Click += new System.EventHandler(this.btnAddDeviceID_Click);
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(214, 10);
            this.btnCreateTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(76, 28);
            this.btnCreateTable.TabIndex = 32;
            this.btnCreateTable.Text = "Create Table";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // lblClientVal
            // 
            this.lblClientVal.AutoSize = true;
            this.lblClientVal.ForeColor = System.Drawing.Color.Green;
            this.lblClientVal.Location = new System.Drawing.Point(391, 18);
            this.lblClientVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClientVal.Name = "lblClientVal";
            this.lblClientVal.Size = new System.Drawing.Size(0, 13);
            this.lblClientVal.TabIndex = 33;
            this.lblClientVal.Click += new System.EventHandler(this.lblClientVal_Click);
            // 
            // lblDeviceID
            // 
            this.lblDeviceID.AutoSize = true;
            this.lblDeviceID.Location = new System.Drawing.Point(102, 296);
            this.lblDeviceID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeviceID.Name = "lblDeviceID";
            this.lblDeviceID.Size = new System.Drawing.Size(52, 13);
            this.lblDeviceID.TabIndex = 34;
            this.lblDeviceID.Text = "DeviceID";
            this.lblDeviceID.Click += new System.EventHandler(this.lblDeviceID_Click);
            // 
            // cbTblType
            // 
            this.cbTblType.FormattingEnabled = true;
            this.cbTblType.Items.AddRange(new object[] {
            "tblTestACTS",
            "tblTestACTSH",
            "tblTestASTC",
            "tblTestATWS",
            "tblTestSTC",
            "tblTestTWS"});
            this.cbTblType.Location = new System.Drawing.Point(163, 60);
            this.cbTblType.Margin = new System.Windows.Forms.Padding(2);
            this.cbTblType.Name = "cbTblType";
            this.cbTblType.Size = new System.Drawing.Size(120, 21);
            this.cbTblType.TabIndex = 35;
            this.cbTblType.SelectedIndexChanged += new System.EventHandler(this.cbTblType_SelectedIndexChanged);
            // 
            // btnCreateServerTable
            // 
            this.btnCreateServerTable.Location = new System.Drawing.Point(823, 10);
            this.btnCreateServerTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateServerTable.Name = "btnCreateServerTable";
            this.btnCreateServerTable.Size = new System.Drawing.Size(76, 28);
            this.btnCreateServerTable.TabIndex = 36;
            this.btnCreateServerTable.Text = "Create Table";
            this.btnCreateServerTable.UseVisualStyleBackColor = true;
            this.btnCreateServerTable.Click += new System.EventHandler(this.btnCreateServerTable_Click);
            // 
            // lblServerErrorVal
            // 
            this.lblServerErrorVal.AutoSize = true;
            this.lblServerErrorVal.ForeColor = System.Drawing.Color.Red;
            this.lblServerErrorVal.Location = new System.Drawing.Point(651, 145);
            this.lblServerErrorVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServerErrorVal.Name = "lblServerErrorVal";
            this.lblServerErrorVal.Size = new System.Drawing.Size(0, 13);
            this.lblServerErrorVal.TabIndex = 37;
            // 
            // cbServerTableType
            // 
            this.cbServerTableType.FormattingEnabled = true;
            this.cbServerTableType.Items.AddRange(new object[] {
            "tblTestACTS",
            "tblTestACTSH",
            "tblTestASTC",
            "tblTestATWS",
            "tblTestSTC",
            "tblTestTWS"});
            this.cbServerTableType.Location = new System.Drawing.Point(823, 59);
            this.cbServerTableType.Margin = new System.Windows.Forms.Padding(2);
            this.cbServerTableType.Name = "cbServerTableType";
            this.cbServerTableType.Size = new System.Drawing.Size(120, 21);
            this.cbServerTableType.TabIndex = 39;
            this.cbServerTableType.SelectedIndexChanged += new System.EventHandler(this.cbServerTableType_SelectedIndexChanged);
            // 
            // btnClientReset
            // 
            this.btnClientReset.Location = new System.Drawing.Point(183, 212);
            this.btnClientReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientReset.Name = "btnClientReset";
            this.btnClientReset.Size = new System.Drawing.Size(44, 28);
            this.btnClientReset.TabIndex = 40;
            this.btnClientReset.Text = "Reset";
            this.btnClientReset.UseVisualStyleBackColor = true;
            this.btnClientReset.Click += new System.EventHandler(this.btnClientReset_Click);
            // 
            // btnServerReset
            // 
            this.btnServerReset.Location = new System.Drawing.Point(858, 212);
            this.btnServerReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnServerReset.Name = "btnServerReset";
            this.btnServerReset.Size = new System.Drawing.Size(44, 28);
            this.btnServerReset.TabIndex = 41;
            this.btnServerReset.Text = "Reset";
            this.btnServerReset.UseVisualStyleBackColor = true;
            this.btnServerReset.Click += new System.EventHandler(this.btnServerReset_Click);
            // 
            // dgvServerRecords
            // 
            this.dgvServerRecords.AllowUserToAddRows = false;
            this.dgvServerRecords.AllowUserToDeleteRows = false;
            this.dgvServerRecords.AllowUserToOrderColumns = true;
            this.dgvServerRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvServerRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerRecords.Location = new System.Drawing.Point(641, 311);
            this.dgvServerRecords.Margin = new System.Windows.Forms.Padding(2);
            this.dgvServerRecords.Name = "dgvServerRecords";
            this.dgvServerRecords.ReadOnly = true;
            dataGridViewCellStyle2.NullValue = null;
            this.dgvServerRecords.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvServerRecords.RowTemplate.Height = 24;
            this.dgvServerRecords.Size = new System.Drawing.Size(541, 444);
            this.dgvServerRecords.TabIndex = 42;
            this.dgvServerRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServerRecords_CellClick);
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(287, 100);
            this.txtClientName.Margin = new System.Windows.Forms.Padding(2);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(197, 20);
            this.txtClientName.TabIndex = 5;
            this.txtClientName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClientName.TextChanged += new System.EventHandler(this.txtClientName_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(287, 137);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 20);
            this.textBox1.TabIndex = 43;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(287, 174);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 20);
            this.textBox2.TabIndex = 44;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(287, 212);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(197, 20);
            this.dateTimePicker1.TabIndex = 45;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(954, 137);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 20);
            this.textBox3.TabIndex = 46;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(954, 212);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 47;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(954, 174);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(200, 20);
            this.textBox4.TabIndex = 48;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(206, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 49;
            this.label1.Text = "StringType";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(865, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 50;
            this.label2.Text = "StringType";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(207, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 51;
            this.label5.Text = "FloatType";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(210, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 17);
            this.label6.TabIndex = 52;
            this.label6.Text = "IntType";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(882, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 17);
            this.label7.TabIndex = 53;
            this.label7.Text = "IntType";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(870, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 17);
            this.label8.TabIndex = 54;
            this.label8.Text = "FloatType";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 749);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvServerRecords);
            this.Controls.Add(this.btnServerReset);
            this.Controls.Add(this.btnClientReset);
            this.Controls.Add(this.cbServerTableType);
            this.Controls.Add(this.lblServerErrorVal);
            this.Controls.Add(this.btnCreateServerTable);
            this.Controls.Add(this.cbTblType);
            this.Controls.Add(this.lblDeviceID);
            this.Controls.Add(this.lblClientVal);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.btnAddDeviceID);
            this.Controls.Add(this.btnAddGlobalConfig);
            this.Controls.Add(this.btnServerRefresh);
            this.Controls.Add(this.lblServerVal);
            this.Controls.Add(this.txtServerID);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.btnServerUpdate);
            this.Controls.Add(this.btnServerAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblClientErrorVal);
            this.Controls.Add(this.btnClientSync);
            this.Controls.Add(this.lblClientLastSyncDateTime);
            this.Controls.Add(this.txtClientID);
            this.Controls.Add(this.txtClientName);
            this.Controls.Add(this.btnClientUpdate);
            this.Controls.Add(this.btnClientAdd);
            this.Controls.Add(this.dgvRecords);
            this.Controls.Add(this.btnClientRefresh);
            this.Controls.Add(this.btnClientAddSettings);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HomeForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HomeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClientAddSettings;
        private System.Windows.Forms.Button btnClientRefresh;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.Button btnClientAdd;
        private System.Windows.Forms.Button btnClientUpdate;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.Label lblClientLastSyncDateTime;
        private System.Windows.Forms.Button btnClientSync;
        private System.Windows.Forms.Label lblClientErrorVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblServerVal;
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
        private System.Windows.Forms.ComboBox cbTblType;
        private System.Windows.Forms.Button btnCreateServerTable;
        private System.Windows.Forms.Label lblServerErrorVal;
        private System.Windows.Forms.ComboBox cbServerTableType;
        private System.Windows.Forms.Button btnClientReset;
        private System.Windows.Forms.Button btnServerReset;
        private System.Windows.Forms.DataGridView dgvServerRecords;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}