namespace HMXHTD
{
    partial class frmAccount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.dgvAccountUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbDriverAccount = new System.Windows.Forms.CheckBox();
            this.ckbDriver = new System.Windows.Forms.CheckBox();
            this.ckbVehicle = new System.Windows.Forms.CheckBox();
            this.ckbDevice = new System.Windows.Forms.CheckBox();
            this.ckbRFID = new System.Windows.Forms.CheckBox();
            this.ckbTrough = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbTaskDbet = new System.Windows.Forms.CheckBox();
            this.ckbTaskRelease2 = new System.Windows.Forms.CheckBox();
            this.ckbTaskRelease = new System.Windows.Forms.CheckBox();
            this.ckbTaskScale = new System.Windows.Forms.CheckBox();
            this.ckbTaskInOut = new System.Windows.Forms.CheckBox();
            this.ckbTaskConfirm = new System.Windows.Forms.CheckBox();
            this.ckbTaskOperating = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbReportRelease = new System.Windows.Forms.CheckBox();
            this.ckbReportScale = new System.Windows.Forms.CheckBox();
            this.ckbReportInOut = new System.Windows.Forms.CheckBox();
            this.ckbReportConfirm = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ckbAccount = new System.Windows.Forms.CheckBox();
            this.ckbSystem = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbbHomePage = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.ckbViewKCS = new System.Windows.Forms.CheckBox();
            this.ckbAdminKCS = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.txtSearch);
            this.groupBox3.Location = new System.Drawing.Point(8, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(618, 65);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin tìm kiếm";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "Tên:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(526, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(37, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(483, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // dgvAccount
            // 
            this.dgvAccount.AllowUserToAddRows = false;
            this.dgvAccount.AllowUserToDeleteRows = false;
            this.dgvAccount.AllowUserToResizeColumns = false;
            this.dgvAccount.AllowUserToResizeRows = false;
            this.dgvAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccount.BackgroundColor = System.Drawing.Color.White;
            this.dgvAccount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAccount.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccount.ColumnHeadersHeight = 30;
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAccountUserName,
            this.dgvAccountFullName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccount.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.Location = new System.Drawing.Point(8, 73);
            this.dgvAccount.MultiSelect = false;
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAccount.RowHeadersWidth = 36;
            this.dgvAccount.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAccount.RowTemplate.Height = 26;
            this.dgvAccount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccount.Size = new System.Drawing.Size(618, 655);
            this.dgvAccount.TabIndex = 3;
            this.dgvAccount.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccount_CellEnter);
            // 
            // dgvAccountUserName
            // 
            this.dgvAccountUserName.DataPropertyName = "UserName";
            this.dgvAccountUserName.HeaderText = "Tài khoản";
            this.dgvAccountUserName.Name = "dgvAccountUserName";
            this.dgvAccountUserName.ReadOnly = true;
            this.dgvAccountUserName.Width = 150;
            // 
            // dgvAccountFullName
            // 
            this.dgvAccountFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAccountFullName.DataPropertyName = "FullName";
            this.dgvAccountFullName.HeaderText = "Tên tài khoản";
            this.dgvAccountFullName.Name = "dgvAccountFullName";
            this.dgvAccountFullName.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ckbDriverAccount);
            this.groupBox1.Controls.Add(this.ckbDriver);
            this.groupBox1.Controls.Add(this.ckbVehicle);
            this.groupBox1.Controls.Add(this.ckbDevice);
            this.groupBox1.Controls.Add(this.ckbRFID);
            this.groupBox1.Controls.Add(this.ckbTrough);
            this.groupBox1.Location = new System.Drawing.Point(633, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 168);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh mục";
            // 
            // ckbDriverAccount
            // 
            this.ckbDriverAccount.AutoSize = true;
            this.ckbDriverAccount.Location = new System.Drawing.Point(17, 142);
            this.ckbDriverAccount.Name = "ckbDriverAccount";
            this.ckbDriverAccount.Size = new System.Drawing.Size(151, 19);
            this.ckbDriverAccount.TabIndex = 18;
            this.ckbDriverAccount.Text = "Quản lý tài khoản lái xe";
            this.ckbDriverAccount.UseVisualStyleBackColor = true;
            // 
            // ckbDriver
            // 
            this.ckbDriver.AutoSize = true;
            this.ckbDriver.Location = new System.Drawing.Point(17, 117);
            this.ckbDriver.Name = "ckbDriver";
            this.ckbDriver.Size = new System.Drawing.Size(98, 19);
            this.ckbDriver.TabIndex = 17;
            this.ckbDriver.Text = "Quản lý lái xe";
            this.ckbDriver.UseVisualStyleBackColor = true;
            // 
            // ckbVehicle
            // 
            this.ckbVehicle.AutoSize = true;
            this.ckbVehicle.Location = new System.Drawing.Point(17, 93);
            this.ckbVehicle.Name = "ckbVehicle";
            this.ckbVehicle.Size = new System.Drawing.Size(137, 19);
            this.ckbVehicle.TabIndex = 16;
            this.ckbVehicle.Text = "Quản lý phương tiện";
            this.ckbVehicle.UseVisualStyleBackColor = true;
            // 
            // ckbDevice
            // 
            this.ckbDevice.AutoSize = true;
            this.ckbDevice.Location = new System.Drawing.Point(17, 69);
            this.ckbDevice.Name = "ckbDevice";
            this.ckbDevice.Size = new System.Drawing.Size(106, 19);
            this.ckbDevice.TabIndex = 15;
            this.ckbDevice.Text = "Quản lý thiết bị";
            this.ckbDevice.UseVisualStyleBackColor = true;
            // 
            // ckbRFID
            // 
            this.ckbRFID.AutoSize = true;
            this.ckbRFID.Location = new System.Drawing.Point(17, 45);
            this.ckbRFID.Name = "ckbRFID";
            this.ckbRFID.Size = new System.Drawing.Size(118, 19);
            this.ckbRFID.TabIndex = 14;
            this.ckbRFID.Text = "Quản lý thẻ RFID";
            this.ckbRFID.UseVisualStyleBackColor = true;
            // 
            // ckbTrough
            // 
            this.ckbTrough.AutoSize = true;
            this.ckbTrough.Location = new System.Drawing.Point(17, 21);
            this.ckbTrough.Name = "ckbTrough";
            this.ckbTrough.Size = new System.Drawing.Size(158, 19);
            this.ckbTrough.TabIndex = 13;
            this.ckbTrough.Text = "Quản lý máng xuất hàng";
            this.ckbTrough.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(941, 699);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 29);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(708, 699);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 29);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Ghi nhận";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ckbTaskDbet);
            this.groupBox2.Controls.Add(this.ckbTaskRelease2);
            this.groupBox2.Controls.Add(this.ckbTaskRelease);
            this.groupBox2.Controls.Add(this.ckbTaskScale);
            this.groupBox2.Controls.Add(this.ckbTaskInOut);
            this.groupBox2.Controls.Add(this.ckbTaskConfirm);
            this.groupBox2.Controls.Add(this.ckbTaskOperating);
            this.groupBox2.Location = new System.Drawing.Point(633, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 199);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quản lý";
            // 
            // ckbTaskDbet
            // 
            this.ckbTaskDbet.AutoSize = true;
            this.ckbTaskDbet.Location = new System.Drawing.Point(17, 167);
            this.ckbTaskDbet.Name = "ckbTaskDbet";
            this.ckbTaskDbet.Size = new System.Drawing.Size(115, 19);
            this.ckbTaskDbet.TabIndex = 10;
            this.ckbTaskDbet.Text = "Quản lý công nợ";
            this.ckbTaskDbet.UseVisualStyleBackColor = true;
            // 
            // ckbTaskRelease2
            // 
            this.ckbTaskRelease2.AutoSize = true;
            this.ckbTaskRelease2.Location = new System.Drawing.Point(17, 142);
            this.ckbTaskRelease2.Name = "ckbTaskRelease2";
            this.ckbTaskRelease2.Size = new System.Drawing.Size(161, 19);
            this.ckbTaskRelease2.TabIndex = 9;
            this.ckbTaskRelease2.Text = "Quản lý xuất hàng - Xi rời";
            this.ckbTaskRelease2.UseVisualStyleBackColor = true;
            // 
            // ckbTaskRelease
            // 
            this.ckbTaskRelease.AutoSize = true;
            this.ckbTaskRelease.Location = new System.Drawing.Point(17, 117);
            this.ckbTaskRelease.Name = "ckbTaskRelease";
            this.ckbTaskRelease.Size = new System.Drawing.Size(167, 19);
            this.ckbTaskRelease.TabIndex = 8;
            this.ckbTaskRelease.Text = "Quản lý xuất hàng - Xi bao";
            this.ckbTaskRelease.UseVisualStyleBackColor = true;
            // 
            // ckbTaskScale
            // 
            this.ckbTaskScale.AutoSize = true;
            this.ckbTaskScale.Location = new System.Drawing.Point(17, 93);
            this.ckbTaskScale.Name = "ckbTaskScale";
            this.ckbTaskScale.Size = new System.Drawing.Size(133, 19);
            this.ckbTaskScale.TabIndex = 7;
            this.ckbTaskScale.Text = "Quản lý cân vào - ra";
            this.ckbTaskScale.UseVisualStyleBackColor = true;
            // 
            // ckbTaskInOut
            // 
            this.ckbTaskInOut.AutoSize = true;
            this.ckbTaskInOut.Location = new System.Drawing.Point(17, 69);
            this.ckbTaskInOut.Name = "ckbTaskInOut";
            this.ckbTaskInOut.Size = new System.Drawing.Size(110, 19);
            this.ckbTaskInOut.TabIndex = 6;
            this.ckbTaskInOut.Text = "Quản lý vào - ra";
            this.ckbTaskInOut.UseVisualStyleBackColor = true;
            // 
            // ckbTaskConfirm
            // 
            this.ckbTaskConfirm.AutoSize = true;
            this.ckbTaskConfirm.Location = new System.Drawing.Point(17, 45);
            this.ckbTaskConfirm.Name = "ckbTaskConfirm";
            this.ckbTaskConfirm.Size = new System.Drawing.Size(115, 19);
            this.ckbTaskConfirm.TabIndex = 5;
            this.ckbTaskConfirm.Text = "Quản lý xác thực";
            this.ckbTaskConfirm.UseVisualStyleBackColor = true;
            // 
            // ckbTaskOperating
            // 
            this.ckbTaskOperating.AutoSize = true;
            this.ckbTaskOperating.Location = new System.Drawing.Point(17, 21);
            this.ckbTaskOperating.Name = "ckbTaskOperating";
            this.ckbTaskOperating.Size = new System.Drawing.Size(125, 19);
            this.ckbTaskOperating.TabIndex = 4;
            this.ckbTaskOperating.Text = "Quản lý điều hành";
            this.ckbTaskOperating.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.ckbReportRelease);
            this.groupBox4.Controls.Add(this.ckbReportScale);
            this.groupBox4.Controls.Add(this.ckbReportInOut);
            this.groupBox4.Controls.Add(this.ckbReportConfirm);
            this.groupBox4.Location = new System.Drawing.Point(633, 207);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(382, 120);
            this.groupBox4.TabIndex = 57;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Báo cáo";
            // 
            // ckbReportRelease
            // 
            this.ckbReportRelease.AutoSize = true;
            this.ckbReportRelease.Location = new System.Drawing.Point(17, 93);
            this.ckbReportRelease.Name = "ckbReportRelease";
            this.ckbReportRelease.Size = new System.Drawing.Size(127, 19);
            this.ckbReportRelease.TabIndex = 12;
            this.ckbReportRelease.Text = "Báo cáo xuất hàng";
            this.ckbReportRelease.UseVisualStyleBackColor = true;
            // 
            // ckbReportScale
            // 
            this.ckbReportScale.AutoSize = true;
            this.ckbReportScale.Location = new System.Drawing.Point(17, 69);
            this.ckbReportScale.Name = "ckbReportScale";
            this.ckbReportScale.Size = new System.Drawing.Size(137, 19);
            this.ckbReportScale.TabIndex = 11;
            this.ckbReportScale.Text = "Báo cáo cân vào - ra";
            this.ckbReportScale.UseVisualStyleBackColor = true;
            // 
            // ckbReportInOut
            // 
            this.ckbReportInOut.AutoSize = true;
            this.ckbReportInOut.Location = new System.Drawing.Point(17, 45);
            this.ckbReportInOut.Name = "ckbReportInOut";
            this.ckbReportInOut.Size = new System.Drawing.Size(114, 19);
            this.ckbReportInOut.TabIndex = 10;
            this.ckbReportInOut.Text = "Báo cáo vào - ra";
            this.ckbReportInOut.UseVisualStyleBackColor = true;
            // 
            // ckbReportConfirm
            // 
            this.ckbReportConfirm.AutoSize = true;
            this.ckbReportConfirm.Location = new System.Drawing.Point(17, 21);
            this.ckbReportConfirm.Name = "ckbReportConfirm";
            this.ckbReportConfirm.Size = new System.Drawing.Size(119, 19);
            this.ckbReportConfirm.TabIndex = 9;
            this.ckbReportConfirm.Text = "Báo cáo xác thực";
            this.ckbReportConfirm.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.ckbAccount);
            this.groupBox5.Controls.Add(this.ckbSystem);
            this.groupBox5.Location = new System.Drawing.Point(633, 500);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(382, 70);
            this.groupBox5.TabIndex = 53;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Hệ thống";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 121);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 19);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Quản lý lái xe";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ckbAccount
            // 
            this.ckbAccount.AutoSize = true;
            this.ckbAccount.Location = new System.Drawing.Point(17, 46);
            this.ckbAccount.Name = "ckbAccount";
            this.ckbAccount.Size = new System.Drawing.Size(120, 19);
            this.ckbAccount.TabIndex = 19;
            this.ckbAccount.Text = "Quản lý tài khoản";
            this.ckbAccount.UseVisualStyleBackColor = true;
            // 
            // ckbSystem
            // 
            this.ckbSystem.AutoSize = true;
            this.ckbSystem.Location = new System.Drawing.Point(17, 22);
            this.ckbSystem.Name = "ckbSystem";
            this.ckbSystem.Size = new System.Drawing.Size(127, 19);
            this.ckbSystem.TabIndex = 18;
            this.ckbSystem.Text = "Quản trị điều hanh";
            this.ckbSystem.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(633, 699);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 29);
            this.btnEdit.TabIndex = 20;
            this.btnEdit.Text = "Cập nhật";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(783, 699);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 29);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbbHomePage
            // 
            this.cbbHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbHomePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.cbbHomePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbHomePage.Enabled = false;
            this.cbbHomePage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbHomePage.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbHomePage.ForeColor = System.Drawing.Color.White;
            this.cbbHomePage.FormattingEnabled = true;
            this.cbbHomePage.Items.AddRange(new object[] {
            "Trang mặc định -> Trang chủ",
            "Trang mặc định -> Quản lý điều hành",
            "Trang mặc định -> Quản lý xác thực",
            "Trang mặc định -> Quản lý vào ra",
            "Trang mặc định -> Quản lý cân",
            "Trang mặc định -> Quản lý xuất hàng",
            "Trang mặc định -> Bảng theo dõi xe"});
            this.cbbHomePage.Location = new System.Drawing.Point(634, 663);
            this.cbbHomePage.Name = "cbbHomePage";
            this.cbbHomePage.Size = new System.Drawing.Size(381, 30);
            this.cbbHomePage.TabIndex = 58;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.checkBox2);
            this.groupBox6.Controls.Add(this.ckbViewKCS);
            this.groupBox6.Controls.Add(this.ckbAdminKCS);
            this.groupBox6.Location = new System.Drawing.Point(633, 576);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(382, 70);
            this.groupBox6.TabIndex = 54;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "KCS";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(17, 121);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 19);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Quản lý lái xe";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // ckbViewKCS
            // 
            this.ckbViewKCS.AutoSize = true;
            this.ckbViewKCS.Location = new System.Drawing.Point(17, 46);
            this.ckbViewKCS.Name = "ckbViewKCS";
            this.ckbViewKCS.Size = new System.Drawing.Size(80, 19);
            this.ckbViewKCS.TabIndex = 19;
            this.ckbViewKCS.Text = "View KCS";
            this.ckbViewKCS.UseVisualStyleBackColor = true;
            // 
            // ckbAdminKCS
            // 
            this.ckbAdminKCS.AutoSize = true;
            this.ckbAdminKCS.Location = new System.Drawing.Point(17, 22);
            this.ckbAdminKCS.Name = "ckbAdminKCS";
            this.ckbAdminKCS.Size = new System.Drawing.Size(97, 19);
            this.ckbAdminKCS.TabIndex = 18;
            this.ckbAdminKCS.Text = "Quản trị KCS";
            this.ckbAdminKCS.UseVisualStyleBackColor = true;
            // 
            // frmAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 735);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.cbbHomePage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvAccount);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmAccount";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccount";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmAccount_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAccount_KeyDown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox ckbTrough;
        private System.Windows.Forms.CheckBox ckbDriver;
        private System.Windows.Forms.CheckBox ckbVehicle;
        private System.Windows.Forms.CheckBox ckbDevice;
        private System.Windows.Forms.CheckBox ckbRFID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbTaskRelease;
        private System.Windows.Forms.CheckBox ckbTaskScale;
        private System.Windows.Forms.CheckBox ckbTaskInOut;
        private System.Windows.Forms.CheckBox ckbTaskConfirm;
        private System.Windows.Forms.CheckBox ckbTaskOperating;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbReportRelease;
        private System.Windows.Forms.CheckBox ckbReportScale;
        private System.Windows.Forms.CheckBox ckbReportInOut;
        private System.Windows.Forms.CheckBox ckbReportConfirm;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox ckbAccount;
        private System.Windows.Forms.CheckBox ckbSystem;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbbHomePage;
        private System.Windows.Forms.CheckBox ckbDriverAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountFullName;
        private System.Windows.Forms.CheckBox ckbTaskRelease2;
        private System.Windows.Forms.CheckBox ckbTaskDbet;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox ckbViewKCS;
        private System.Windows.Forms.CheckBox ckbAdminKCS;
    }
}