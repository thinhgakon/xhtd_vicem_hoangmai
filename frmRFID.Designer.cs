namespace HMXHTD
{
    partial class frmRFID
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRFID));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearchVehicle = new System.Windows.Forms.Button();
            this.txtDayExpired = new System.Windows.Forms.MaskedTextBox();
            this.txtDayReleased = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVehicle = new System.Windows.Forms.TextBox();
            this.ckbState = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvRFID = new System.Windows.Forms.DataGridView();
            this.dgvRFIDId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRFIDCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRFIDVehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRFIDDayReleased = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRFIDDayExpired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRFIDState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.brnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.btnSaveToBill = new System.Windows.Forms.Button();
            this.btnCreateRecordConfirm = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnSaveRecordConfirm = new System.Windows.Forms.Button();
            this.txtH = new System.Windows.Forms.TextBox();
            this.txtW = new System.Windows.Forms.TextBox();
            this.txtL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox_RFID_Confirm = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRFID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RFID_Confirm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pictureBox_RFID_Confirm);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtL);
            this.groupBox1.Controls.Add(this.txtW);
            this.groupBox1.Controls.Add(this.txtH);
            this.groupBox1.Controls.Add(this.btnSearchVehicle);
            this.groupBox1.Controls.Add(this.txtDayExpired);
            this.groupBox1.Controls.Add(this.txtDayReleased);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtVehicle);
            this.groupBox1.Controls.Add(this.ckbState);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Location = new System.Drawing.Point(494, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 351);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin thẻ - phương tiện";
            // 
            // btnSearchVehicle
            // 
            this.btnSearchVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchVehicle.BackColor = System.Drawing.Color.White;
            this.btnSearchVehicle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchVehicle.FlatAppearance.BorderSize = 0;
            this.btnSearchVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchVehicle.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchVehicle.Image")));
            this.btnSearchVehicle.Location = new System.Drawing.Point(356, 54);
            this.btnSearchVehicle.Name = "btnSearchVehicle";
            this.btnSearchVehicle.Size = new System.Drawing.Size(19, 19);
            this.btnSearchVehicle.TabIndex = 42;
            this.btnSearchVehicle.UseVisualStyleBackColor = false;
            this.btnSearchVehicle.Click += new System.EventHandler(this.btnSearchVehicle_Click);
            // 
            // txtDayExpired
            // 
            this.txtDayExpired.BackColor = System.Drawing.Color.White;
            this.txtDayExpired.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDayExpired.Location = new System.Drawing.Point(297, 83);
            this.txtDayExpired.Mask = "00/00/0000";
            this.txtDayExpired.Name = "txtDayExpired";
            this.txtDayExpired.ReadOnly = true;
            this.txtDayExpired.Size = new System.Drawing.Size(90, 25);
            this.txtDayExpired.TabIndex = 43;
            this.txtDayExpired.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDayExpired.ValidatingType = typeof(System.DateTime);
            // 
            // txtDayReleased
            // 
            this.txtDayReleased.BackColor = System.Drawing.Color.White;
            this.txtDayReleased.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDayReleased.Location = new System.Drawing.Point(105, 83);
            this.txtDayReleased.Mask = "00/00/0000";
            this.txtDayReleased.Name = "txtDayReleased";
            this.txtDayReleased.ReadOnly = true;
            this.txtDayReleased.Size = new System.Drawing.Size(90, 25);
            this.txtDayReleased.TabIndex = 42;
            this.txtDayReleased.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDayReleased.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ngày hết hạn:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ngày phát hành:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Phương tiện:";
            // 
            // txtVehicle
            // 
            this.txtVehicle.BackColor = System.Drawing.Color.White;
            this.txtVehicle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicle.Location = new System.Drawing.Point(105, 52);
            this.txtVehicle.Name = "txtVehicle";
            this.txtVehicle.ReadOnly = true;
            this.txtVehicle.Size = new System.Drawing.Size(282, 25);
            this.txtVehicle.TabIndex = 3;
            this.txtVehicle.TextChanged += new System.EventHandler(this.txtVehicle_TextChanged);
            // 
            // ckbState
            // 
            this.ckbState.AutoSize = true;
            this.ckbState.Enabled = false;
            this.ckbState.Location = new System.Drawing.Point(284, 158);
            this.ckbState.Name = "ckbState";
            this.ckbState.Size = new System.Drawing.Size(103, 19);
            this.ckbState.TabIndex = 2;
            this.ckbState.Text = "Trạng thái mở";
            this.ckbState.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Số hiệu:";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(105, 20);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(280, 25);
            this.txtCode.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtSearchKey);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(7, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(481, 57);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tìm kiếm";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(448, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(19, 19);
            this.btnSearch.TabIndex = 43;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchKey.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchKey.Location = new System.Drawing.Point(117, 19);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(355, 25);
            this.txtSearchKey.TabIndex = 0;
            this.txtSearchKey.TextChanged += new System.EventHandler(this.txtSearchKey_TextChanged);
            this.txtSearchKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchKey_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Phương tiện, RFID:";
            // 
            // dgvRFID
            // 
            this.dgvRFID.AllowUserToAddRows = false;
            this.dgvRFID.AllowUserToDeleteRows = false;
            this.dgvRFID.AllowUserToResizeColumns = false;
            this.dgvRFID.AllowUserToResizeRows = false;
            this.dgvRFID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRFID.BackgroundColor = System.Drawing.Color.White;
            this.dgvRFID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvRFID.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRFID.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRFID.ColumnHeadersHeight = 30;
            this.dgvRFID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRFID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRFIDId,
            this.dgvRFIDCode,
            this.dgvRFIDVehicle,
            this.dgvRFIDDayReleased,
            this.dgvRFIDDayExpired,
            this.dgvRFIDState});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRFID.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvRFID.EnableHeadersVisualStyles = false;
            this.dgvRFID.Location = new System.Drawing.Point(7, 63);
            this.dgvRFID.MultiSelect = false;
            this.dgvRFID.Name = "dgvRFID";
            this.dgvRFID.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRFID.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvRFID.RowHeadersWidth = 36;
            this.dgvRFID.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRFID.RowTemplate.Height = 26;
            this.dgvRFID.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRFID.Size = new System.Drawing.Size(481, 401);
            this.dgvRFID.TabIndex = 37;
            this.dgvRFID.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRFID_CellClick);
            // 
            // dgvRFIDId
            // 
            this.dgvRFIDId.DataPropertyName = "Id";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvRFIDId.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvRFIDId.HeaderText = "Id";
            this.dgvRFIDId.Name = "dgvRFIDId";
            this.dgvRFIDId.ReadOnly = true;
            this.dgvRFIDId.Visible = false;
            this.dgvRFIDId.Width = 55;
            // 
            // dgvRFIDCode
            // 
            this.dgvRFIDCode.DataPropertyName = "Code";
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRFIDCode.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvRFIDCode.HeaderText = "     Số hiệu";
            this.dgvRFIDCode.Name = "dgvRFIDCode";
            this.dgvRFIDCode.ReadOnly = true;
            this.dgvRFIDCode.Width = 80;
            // 
            // dgvRFIDVehicle
            // 
            this.dgvRFIDVehicle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvRFIDVehicle.DataPropertyName = "Vehicle";
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRFIDVehicle.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvRFIDVehicle.HeaderText = "Phương tiện";
            this.dgvRFIDVehicle.Name = "dgvRFIDVehicle";
            this.dgvRFIDVehicle.ReadOnly = true;
            // 
            // dgvRFIDDayReleased
            // 
            this.dgvRFIDDayReleased.DataPropertyName = "DayReleased";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "d";
            dataGridViewCellStyle13.NullValue = null;
            this.dgvRFIDDayReleased.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvRFIDDayReleased.HeaderText = "Ngày phát hành";
            this.dgvRFIDDayReleased.Name = "dgvRFIDDayReleased";
            this.dgvRFIDDayReleased.ReadOnly = true;
            // 
            // dgvRFIDDayExpired
            // 
            this.dgvRFIDDayExpired.DataPropertyName = "DayExpired";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Format = "d";
            dataGridViewCellStyle14.NullValue = null;
            this.dgvRFIDDayExpired.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvRFIDDayExpired.HeaderText = "Ngày hết hạn";
            this.dgvRFIDDayExpired.Name = "dgvRFIDDayExpired";
            this.dgvRFIDDayExpired.ReadOnly = true;
            this.dgvRFIDDayExpired.Width = 86;
            // 
            // dgvRFIDState
            // 
            this.dgvRFIDState.DataPropertyName = "State";
            this.dgvRFIDState.HeaderText = "Trạng thái";
            this.dgvRFIDState.Name = "dgvRFIDState";
            this.dgvRFIDState.ReadOnly = true;
            this.dgvRFIDState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRFIDState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvRFIDState.Width = 70;
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
            this.btnClose.Location = new System.Drawing.Point(820, 463);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 29);
            this.btnClose.TabIndex = 39;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(601, 358);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 29);
            this.btnAdd.TabIndex = 38;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(747, 358);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 29);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "Ghi nhận";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // brnEdit
            // 
            this.brnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.brnEdit.Location = new System.Drawing.Point(674, 358);
            this.brnEdit.Name = "brnEdit";
            this.brnEdit.Size = new System.Drawing.Size(70, 29);
            this.brnEdit.TabIndex = 41;
            this.brnEdit.Text = "Cập nhật";
            this.brnEdit.UseVisualStyleBackColor = true;
            this.brnEdit.Click += new System.EventHandler(this.brnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(820, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 29);
            this.btnCancel.TabIndex = 52;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bindingNavigator1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(7, 467);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(194, 25);
            this.bindingNavigator1.TabIndex = 53;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 22);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // btnSaveToBill
            // 
            this.btnSaveToBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveToBill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveToBill.Location = new System.Drawing.Point(601, 393);
            this.btnSaveToBill.Name = "btnSaveToBill";
            this.btnSaveToBill.Size = new System.Drawing.Size(291, 29);
            this.btnSaveToBill.TabIndex = 54;
            this.btnSaveToBill.Text = "Cập nhật số thẻ rfid vào đơn hàng";
            this.btnSaveToBill.UseVisualStyleBackColor = true;
            this.btnSaveToBill.Visible = false;
            this.btnSaveToBill.Click += new System.EventHandler(this.btnSaveToBill_Click);
            // 
            // btnCreateRecordConfirm
            // 
            this.btnCreateRecordConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateRecordConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateRecordConfirm.Location = new System.Drawing.Point(601, 428);
            this.btnCreateRecordConfirm.Name = "btnCreateRecordConfirm";
            this.btnCreateRecordConfirm.Size = new System.Drawing.Size(145, 29);
            this.btnCreateRecordConfirm.TabIndex = 55;
            this.btnCreateRecordConfirm.Text = "Tạo biên bản xác nhận";
            this.btnCreateRecordConfirm.UseVisualStyleBackColor = true;
            this.btnCreateRecordConfirm.Click += new System.EventHandler(this.btnCreateRecordConfirm_Click);
            // 
            // btnSaveRecordConfirm
            // 
            this.btnSaveRecordConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecordConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveRecordConfirm.Location = new System.Drawing.Point(752, 428);
            this.btnSaveRecordConfirm.Name = "btnSaveRecordConfirm";
            this.btnSaveRecordConfirm.Size = new System.Drawing.Size(140, 29);
            this.btnSaveRecordConfirm.TabIndex = 56;
            this.btnSaveRecordConfirm.Text = "Lưu biên bản chữ ký";
            this.btnSaveRecordConfirm.UseVisualStyleBackColor = true;
            this.btnSaveRecordConfirm.Click += new System.EventHandler(this.btnSaveRecordConfirm_Click);
            // 
            // txtH
            // 
            this.txtH.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtH.Location = new System.Drawing.Point(105, 152);
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(90, 25);
            this.txtH.TabIndex = 44;
            this.txtH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtW
            // 
            this.txtW.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtW.Location = new System.Drawing.Point(297, 121);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(90, 25);
            this.txtW.TabIndex = 45;
            this.txtW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtL
            // 
            this.txtL.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtL.Location = new System.Drawing.Point(105, 121);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(90, 25);
            this.txtL.TabIndex = 46;
            this.txtL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 47;
            this.label8.Text = "Chiều cao:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(222, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "Chiều rộng:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 49;
            this.label6.Text = "Chiều dài:";
            // 
            // pictureBox_RFID_Confirm
            // 
            this.pictureBox_RFID_Confirm.Location = new System.Drawing.Point(105, 183);
            this.pictureBox_RFID_Confirm.Name = "pictureBox_RFID_Confirm";
            this.pictureBox_RFID_Confirm.Size = new System.Drawing.Size(282, 148);
            this.pictureBox_RFID_Confirm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_RFID_Confirm.TabIndex = 50;
            this.pictureBox_RFID_Confirm.TabStop = false;
            // 
            // frmRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.btnSaveRecordConfirm);
            this.Controls.Add(this.btnCreateRecordConfirm);
            this.Controls.Add(this.btnSaveToBill);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.brnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvRFID);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmRFID";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRFID";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRFID_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRFID_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRFID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RFID_Confirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchKey;
        private System.Windows.Forms.DataGridView dgvRFID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVehicle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button brnEdit;
        private System.Windows.Forms.MaskedTextBox txtDayReleased;
        private System.Windows.Forms.MaskedTextBox txtDayExpired;
        private System.Windows.Forms.Button btnSearchVehicle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRFIDId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRFIDCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRFIDVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRFIDDayReleased;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRFIDDayExpired;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvRFIDState;
        private System.Windows.Forms.Button btnSaveToBill;
        private System.Windows.Forms.Button btnCreateRecordConfirm;
        private System.Windows.Forms.Button btnSaveRecordConfirm;
        private System.Windows.Forms.PictureBox pictureBox_RFID_Confirm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.TextBox txtH;
    }
}