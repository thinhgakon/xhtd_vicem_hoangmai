
namespace HMXHTD
{
    partial class frmReportDriver
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpToDay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDay = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.dgvAccountUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountVehicleList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountTotalNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblMSG = new System.Windows.Forms.Label();
            this.dgvListBillOrder = new System.Windows.Forms.DataGridView();
            this.lblDriver = new System.Windows.Forms.Label();
            this.dgvListBillOrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderDeliveryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderVehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderNameDistributor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderNameStore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderSumNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderTimeConfirm9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDriverTotalNumber = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBillOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dtpToDay);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dtpFromDay);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.txtSearch);
            this.groupBox3.Location = new System.Drawing.Point(8, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(884, 65);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin tìm kiếm";
            // 
            // dtpToDay
            // 
            this.dtpToDay.CustomFormat = "dd/MM/yyyy";
            this.dtpToDay.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDay.Location = new System.Drawing.Point(195, 25);
            this.dtpToDay.Name = "dtpToDay";
            this.dtpToDay.Size = new System.Drawing.Size(108, 25);
            this.dtpToDay.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 58;
            this.label1.Text = "Từ ngày:";
            // 
            // dtpFromDay
            // 
            this.dtpFromDay.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDay.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDay.Location = new System.Drawing.Point(68, 25);
            this.dtpFromDay.Name = "dtpFromDay";
            this.dtpFromDay.Size = new System.Drawing.Size(108, 25);
            this.dtpFromDay.TabIndex = 60;
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
            this.btnSearch.Location = new System.Drawing.Point(774, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(98, 25);
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
            this.txtSearch.Font = new System.Drawing.Font("Arial", 11.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(309, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(563, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // dgvAccount
            // 
            this.dgvAccount.AllowUserToAddRows = false;
            this.dgvAccount.AllowUserToDeleteRows = false;
            this.dgvAccount.AllowUserToResizeColumns = false;
            this.dgvAccount.AllowUserToResizeRows = false;
            this.dgvAccount.BackgroundColor = System.Drawing.Color.White;
            this.dgvAccount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAccount.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvAccount.ColumnHeadersHeight = 30;
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAccountUserName,
            this.dgvAccountFullName,
            this.dgvAccountVehicleList,
            this.dgvAccountTotalNumber});
            this.dgvAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccount.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.Location = new System.Drawing.Point(3, 3);
            this.dgvAccount.MultiSelect = false;
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvAccount.RowHeadersWidth = 36;
            this.dgvAccount.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAccount.RowTemplate.Height = 26;
            this.dgvAccount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccount.Size = new System.Drawing.Size(870, 401);
            this.dgvAccount.TabIndex = 62;
            this.dgvAccount.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccount_CellDoubleClick);
            // 
            // dgvAccountUserName
            // 
            this.dgvAccountUserName.DataPropertyName = "User_Name";
            this.dgvAccountUserName.HeaderText = "Tài khoản";
            this.dgvAccountUserName.Name = "dgvAccountUserName";
            this.dgvAccountUserName.ReadOnly = true;
            // 
            // dgvAccountFullName
            // 
            this.dgvAccountFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvAccountFullName.DataPropertyName = "Full_Name";
            this.dgvAccountFullName.HeaderText = "Họ và tên";
            this.dgvAccountFullName.Name = "dgvAccountFullName";
            this.dgvAccountFullName.ReadOnly = true;
            this.dgvAccountFullName.Width = 170;
            // 
            // dgvAccountVehicleList
            // 
            this.dgvAccountVehicleList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAccountVehicleList.DataPropertyName = "VEHICLE_LIST";
            this.dgvAccountVehicleList.HeaderText = "Danh sách xe";
            this.dgvAccountVehicleList.Name = "dgvAccountVehicleList";
            this.dgvAccountVehicleList.ReadOnly = true;
            // 
            // dgvAccountTotalNumber
            // 
            this.dgvAccountTotalNumber.DataPropertyName = "TotalNumber";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAccountTotalNumber.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvAccountTotalNumber.HeaderText = "Sản lượng (Tấn)";
            this.dgvAccountTotalNumber.Name = "dgvAccountTotalNumber";
            this.dgvAccountTotalNumber.ReadOnly = true;
            this.dgvAccountTotalNumber.Width = 102;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 73);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(884, 435);
            this.tabControl1.TabIndex = 63;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvAccount);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(876, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Báo cáo tổng hợp";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvListBillOrder);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(876, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Báo cáo chi tiết";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.Location = new System.Drawing.Point(8, 514);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 29);
            this.btnExportExcel.TabIndex = 64;
            this.btnExportExcel.Text = "Xuất file Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(114, 514);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 29);
            this.btnClose.TabIndex = 65;
            this.btnClose.Text = "Thoát";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblMSG
            // 
            this.lblMSG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMSG.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMSG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMSG.Location = new System.Drawing.Point(8, 514);
            this.lblMSG.Name = "lblMSG";
            this.lblMSG.Size = new System.Drawing.Size(884, 29);
            this.lblMSG.TabIndex = 63;
            this.lblMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvListBillOrder
            // 
            this.dgvListBillOrder.AllowUserToAddRows = false;
            this.dgvListBillOrder.AllowUserToDeleteRows = false;
            this.dgvListBillOrder.AllowUserToResizeColumns = false;
            this.dgvListBillOrder.AllowUserToResizeRows = false;
            this.dgvListBillOrder.BackgroundColor = System.Drawing.Color.White;
            this.dgvListBillOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvListBillOrder.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListBillOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvListBillOrder.ColumnHeadersHeight = 30;
            this.dgvListBillOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvListBillOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvListBillOrderId,
            this.dgvListBillOrderOrderDate,
            this.dgvListBillOrderDeliveryCode,
            this.dgvListBillOrderVehicle,
            this.dgvListBillOrderNameDistributor,
            this.dgvListBillOrderNameProduct,
            this.dgvListBillOrderNameStore,
            this.dgvListBillOrderSumNumber,
            this.dgvListBillOrderTimeConfirm9});
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListBillOrder.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgvListBillOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListBillOrder.EnableHeadersVisualStyles = false;
            this.dgvListBillOrder.Location = new System.Drawing.Point(3, 3);
            this.dgvListBillOrder.MultiSelect = false;
            this.dgvListBillOrder.Name = "dgvListBillOrder";
            this.dgvListBillOrder.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListBillOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvListBillOrder.RowHeadersVisible = false;
            this.dgvListBillOrder.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvListBillOrder.RowTemplate.Height = 26;
            this.dgvListBillOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListBillOrder.Size = new System.Drawing.Size(870, 401);
            this.dgvListBillOrder.TabIndex = 1;
            // 
            // lblDriver
            // 
            this.lblDriver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDriver.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDriver.Location = new System.Drawing.Point(317, 69);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(460, 24);
            this.lblDriver.TabIndex = 66;
            this.lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvListBillOrderId
            // 
            this.dgvListBillOrderId.DataPropertyName = "Id";
            this.dgvListBillOrderId.HeaderText = "Id";
            this.dgvListBillOrderId.Name = "dgvListBillOrderId";
            this.dgvListBillOrderId.Visible = false;
            // 
            // dgvListBillOrderOrderDate
            // 
            this.dgvListBillOrderOrderDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvListBillOrderOrderDate.DataPropertyName = "OrderDate";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.Format = "dd/MM/yyyy";
            dataGridViewCellStyle17.NullValue = null;
            this.dgvListBillOrderOrderDate.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvListBillOrderOrderDate.HeaderText = "    Ngày đặt";
            this.dgvListBillOrderOrderDate.Name = "dgvListBillOrderOrderDate";
            this.dgvListBillOrderOrderDate.ReadOnly = true;
            this.dgvListBillOrderOrderDate.Width = 85;
            // 
            // dgvListBillOrderDeliveryCode
            // 
            this.dgvListBillOrderDeliveryCode.DataPropertyName = "DeliveryCode";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListBillOrderDeliveryCode.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvListBillOrderDeliveryCode.HeaderText = "      MSGH";
            this.dgvListBillOrderDeliveryCode.Name = "dgvListBillOrderDeliveryCode";
            this.dgvListBillOrderDeliveryCode.ReadOnly = true;
            this.dgvListBillOrderDeliveryCode.Width = 85;
            // 
            // dgvListBillOrderVehicle
            // 
            this.dgvListBillOrderVehicle.DataPropertyName = "Vehicle";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListBillOrderVehicle.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvListBillOrderVehicle.HeaderText = "    Biển số";
            this.dgvListBillOrderVehicle.Name = "dgvListBillOrderVehicle";
            this.dgvListBillOrderVehicle.ReadOnly = true;
            this.dgvListBillOrderVehicle.Width = 80;
            // 
            // dgvListBillOrderNameDistributor
            // 
            this.dgvListBillOrderNameDistributor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvListBillOrderNameDistributor.DataPropertyName = "NameDistributor";
            this.dgvListBillOrderNameDistributor.HeaderText = "Nhà phân phối";
            this.dgvListBillOrderNameDistributor.Name = "dgvListBillOrderNameDistributor";
            this.dgvListBillOrderNameDistributor.ReadOnly = true;
            // 
            // dgvListBillOrderNameProduct
            // 
            this.dgvListBillOrderNameProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvListBillOrderNameProduct.DataPropertyName = "NameProduct";
            this.dgvListBillOrderNameProduct.HeaderText = "Hàng hóa";
            this.dgvListBillOrderNameProduct.Name = "dgvListBillOrderNameProduct";
            this.dgvListBillOrderNameProduct.ReadOnly = true;
            // 
            // dgvListBillOrderNameStore
            // 
            this.dgvListBillOrderNameStore.DataPropertyName = "NameStore";
            this.dgvListBillOrderNameStore.HeaderText = "NameStore";
            this.dgvListBillOrderNameStore.Name = "dgvListBillOrderNameStore";
            this.dgvListBillOrderNameStore.Visible = false;
            // 
            // dgvListBillOrderSumNumber
            // 
            this.dgvListBillOrderSumNumber.DataPropertyName = "SumNumber";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.dgvListBillOrderSumNumber.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvListBillOrderSumNumber.HeaderText = "Sản lượng (Tấn)";
            this.dgvListBillOrderSumNumber.Name = "dgvListBillOrderSumNumber";
            this.dgvListBillOrderSumNumber.ReadOnly = true;
            this.dgvListBillOrderSumNumber.Width = 102;
            // 
            // dgvListBillOrderTimeConfirm9
            // 
            this.dgvListBillOrderTimeConfirm9.DataPropertyName = "TimeConfirm9";
            this.dgvListBillOrderTimeConfirm9.HeaderText = "TimeConfirm9";
            this.dgvListBillOrderTimeConfirm9.Name = "dgvListBillOrderTimeConfirm9";
            this.dgvListBillOrderTimeConfirm9.Visible = false;
            // 
            // lblDriverTotalNumber
            // 
            this.lblDriverTotalNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDriverTotalNumber.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriverTotalNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDriverTotalNumber.Location = new System.Drawing.Point(782, 69);
            this.lblDriverTotalNumber.Name = "lblDriverTotalNumber";
            this.lblDriverTotalNumber.Size = new System.Drawing.Size(103, 24);
            this.lblDriverTotalNumber.TabIndex = 67;
            this.lblDriverTotalNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmReportDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblDriverTotalNumber);
            this.Controls.Add(this.lblDriver);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblMSG);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmReportDriver";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo sản lượng theo lái xe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportDriver_Load);
            this.Shown += new System.EventHandler(this.frmReportDriver_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportDriver_KeyDown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBillOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DateTimePicker dtpToDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDay;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountVehicleList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountTotalNumber;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMSG;
        private System.Windows.Forms.DataGridView dgvListBillOrder;
        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderDeliveryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameDistributor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameStore;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderSumNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm9;
        private System.Windows.Forms.Label lblDriverTotalNumber;
    }
}