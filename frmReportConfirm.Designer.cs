
namespace HMXHTD
{
    partial class frmReportConfirm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalItem = new System.Windows.Forms.Label();
            this.dtpToDay = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtQRCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.dgvReportConfirm = new System.Windows.Forms.DataGridView();
            this.dgvListBillOrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderIDOrderSyn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderVehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderDriverName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderNameDistributor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderNameStore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderSumNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderConfirm1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvListBillOrderTimeConfirm1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderConfirm2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvListBillOrderDriverAccept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderTimeConfirm5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderTimeConfirm6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderConfirm7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderTimeConfirm8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvListBillOrderTimeConfirm9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.rtxtHistory = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportConfirm)).BeginInit();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblTotalItem);
            this.groupBox1.Controls.Add(this.dtpToDay);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtQRCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFromDay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(938, 60);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tìm kiếm";
            // 
            // lblTotalItem
            // 
            this.lblTotalItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalItem.Location = new System.Drawing.Point(6, 21);
            this.lblTotalItem.Name = "lblTotalItem";
            this.lblTotalItem.Size = new System.Drawing.Size(81, 25);
            this.lblTotalItem.TabIndex = 58;
            this.lblTotalItem.Text = "0";
            this.lblTotalItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToDay
            // 
            this.dtpToDay.CustomFormat = "dd/MM/yyyy";
            this.dtpToDay.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDay.Location = new System.Drawing.Point(270, 21);
            this.dtpToDay.Name = "dtpToDay";
            this.dtpToDay.Size = new System.Drawing.Size(108, 25);
            this.dtpToDay.TabIndex = 57;
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
            this.btnSearch.Location = new System.Drawing.Point(846, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 55;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtQRCode
            // 
            this.txtQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQRCode.BackColor = System.Drawing.Color.White;
            this.txtQRCode.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQRCode.ForeColor = System.Drawing.Color.Black;
            this.txtQRCode.Location = new System.Drawing.Point(384, 18);
            this.txtQRCode.Name = "txtQRCode";
            this.txtQRCode.Size = new System.Drawing.Size(469, 30);
            this.txtQRCode.TabIndex = 54;
            this.txtQRCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQRCode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 15);
            this.label2.TabIndex = 46;
            this.label2.Text = "-";
            // 
            // dtpFromDay
            // 
            this.dtpFromDay.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDay.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDay.Location = new System.Drawing.Point(143, 21);
            this.dtpFromDay.Name = "dtpFromDay";
            this.dtpFromDay.Size = new System.Drawing.Size(108, 25);
            this.dtpFromDay.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Từ ngày:";
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
            this.btnClose.Location = new System.Drawing.Point(871, 563);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 29);
            this.btnClose.TabIndex = 53;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Location = new System.Drawing.Point(119, 563);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 29);
            this.btnPrint.TabIndex = 52;
            this.btnPrint.Text = "In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.Location = new System.Drawing.Point(6, 563);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(111, 29);
            this.btnExportExcel.TabIndex = 51;
            this.btnExportExcel.Text = "Xuất file Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // dgvReportConfirm
            // 
            this.dgvReportConfirm.AllowUserToAddRows = false;
            this.dgvReportConfirm.AllowUserToDeleteRows = false;
            this.dgvReportConfirm.AllowUserToResizeColumns = false;
            this.dgvReportConfirm.AllowUserToResizeRows = false;
            this.dgvReportConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReportConfirm.BackgroundColor = System.Drawing.Color.White;
            this.dgvReportConfirm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvReportConfirm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportConfirm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvReportConfirm.ColumnHeadersHeight = 30;
            this.dgvReportConfirm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReportConfirm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvListBillOrderId,
            this.dgvListBillOrderOrderDate,
            this.dgvListBillOrderIDOrderSyn,
            this.dgvListBillOrderVehicle,
            this.dgvListBillOrderDriverName,
            this.dgvListBillOrderNameDistributor,
            this.dgvListBillOrderNameProduct,
            this.dgvListBillOrderNameStore,
            this.dgvListBillOrderSumNumber,
            this.dgvListBillOrderConfirm1,
            this.dgvListBillOrderTimeConfirm1,
            this.dgvListBillOrderConfirm2,
            this.dgvListBillOrderDriverAccept,
            this.dgvListBillOrderTimeConfirm5,
            this.dgvListBillOrderTimeConfirm6,
            this.dgvListBillOrderConfirm7,
            this.dgvListBillOrderTimeConfirm8,
            this.dgvListBillOrderTimeConfirm9});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReportConfirm.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvReportConfirm.EnableHeadersVisualStyles = false;
            this.dgvReportConfirm.Location = new System.Drawing.Point(6, 72);
            this.dgvReportConfirm.MultiSelect = false;
            this.dgvReportConfirm.Name = "dgvReportConfirm";
            this.dgvReportConfirm.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportConfirm.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvReportConfirm.RowHeadersVisible = false;
            this.dgvReportConfirm.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReportConfirm.RowTemplate.Height = 26;
            this.dgvReportConfirm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportConfirm.Size = new System.Drawing.Size(938, 304);
            this.dgvReportConfirm.TabIndex = 54;
            this.dgvReportConfirm.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReportConfirm_CellClick);
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.Format = "dd/MM/yyyy";
            dataGridViewCellStyle10.NullValue = null;
            this.dgvListBillOrderOrderDate.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvListBillOrderOrderDate.HeaderText = "    Ngày đặt";
            this.dgvListBillOrderOrderDate.Name = "dgvListBillOrderOrderDate";
            this.dgvListBillOrderOrderDate.ReadOnly = true;
            this.dgvListBillOrderOrderDate.Width = 85;
            // 
            // dgvListBillOrderIDOrderSyn
            // 
            this.dgvListBillOrderIDOrderSyn.DataPropertyName = "DeliveryCode";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListBillOrderIDOrderSyn.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvListBillOrderIDOrderSyn.HeaderText = "      MSGH";
            this.dgvListBillOrderIDOrderSyn.Name = "dgvListBillOrderIDOrderSyn";
            this.dgvListBillOrderIDOrderSyn.ReadOnly = true;
            this.dgvListBillOrderIDOrderSyn.Width = 85;
            // 
            // dgvListBillOrderVehicle
            // 
            this.dgvListBillOrderVehicle.DataPropertyName = "Vehicle";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListBillOrderVehicle.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvListBillOrderVehicle.HeaderText = "    Biển số";
            this.dgvListBillOrderVehicle.Name = "dgvListBillOrderVehicle";
            this.dgvListBillOrderVehicle.ReadOnly = true;
            this.dgvListBillOrderVehicle.Width = 80;
            // 
            // dgvListBillOrderDriverName
            // 
            this.dgvListBillOrderDriverName.DataPropertyName = "DriverName";
            this.dgvListBillOrderDriverName.HeaderText = "Lái xe";
            this.dgvListBillOrderDriverName.Name = "dgvListBillOrderDriverName";
            this.dgvListBillOrderDriverName.ReadOnly = true;
            this.dgvListBillOrderDriverName.Width = 160;
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.dgvListBillOrderSumNumber.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvListBillOrderSumNumber.HeaderText = "    S.L";
            this.dgvListBillOrderSumNumber.Name = "dgvListBillOrderSumNumber";
            this.dgvListBillOrderSumNumber.ReadOnly = true;
            this.dgvListBillOrderSumNumber.Width = 50;
            // 
            // dgvListBillOrderConfirm1
            // 
            this.dgvListBillOrderConfirm1.DataPropertyName = "Confirm1";
            this.dgvListBillOrderConfirm1.HeaderText = "Xác thực";
            this.dgvListBillOrderConfirm1.Name = "dgvListBillOrderConfirm1";
            this.dgvListBillOrderConfirm1.ReadOnly = true;
            this.dgvListBillOrderConfirm1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListBillOrderConfirm1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvListBillOrderConfirm1.Visible = false;
            this.dgvListBillOrderConfirm1.Width = 63;
            // 
            // dgvListBillOrderTimeConfirm1
            // 
            this.dgvListBillOrderTimeConfirm1.DataPropertyName = "TimeConfirm1";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial Narrow", 10.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle14.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle14.NullValue = null;
            this.dgvListBillOrderTimeConfirm1.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvListBillOrderTimeConfirm1.HeaderText = "       Thời gian xác thực";
            this.dgvListBillOrderTimeConfirm1.Name = "dgvListBillOrderTimeConfirm1";
            this.dgvListBillOrderTimeConfirm1.ReadOnly = true;
            this.dgvListBillOrderTimeConfirm1.Width = 150;
            // 
            // dgvListBillOrderConfirm2
            // 
            this.dgvListBillOrderConfirm2.DataPropertyName = "Confirm2";
            this.dgvListBillOrderConfirm2.HeaderText = "Vào cổng";
            this.dgvListBillOrderConfirm2.Name = "dgvListBillOrderConfirm2";
            this.dgvListBillOrderConfirm2.ReadOnly = true;
            this.dgvListBillOrderConfirm2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListBillOrderConfirm2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvListBillOrderConfirm2.Visible = false;
            this.dgvListBillOrderConfirm2.Width = 63;
            // 
            // dgvListBillOrderDriverAccept
            // 
            this.dgvListBillOrderDriverAccept.DataPropertyName = "DriverAccept";
            this.dgvListBillOrderDriverAccept.HeaderText = "DriverAccept";
            this.dgvListBillOrderDriverAccept.Name = "dgvListBillOrderDriverAccept";
            this.dgvListBillOrderDriverAccept.Visible = false;
            // 
            // dgvListBillOrderTimeConfirm5
            // 
            this.dgvListBillOrderTimeConfirm5.DataPropertyName = "TimeConfirm5";
            this.dgvListBillOrderTimeConfirm5.HeaderText = "TimeConfirm5";
            this.dgvListBillOrderTimeConfirm5.Name = "dgvListBillOrderTimeConfirm5";
            this.dgvListBillOrderTimeConfirm5.Visible = false;
            // 
            // dgvListBillOrderTimeConfirm6
            // 
            this.dgvListBillOrderTimeConfirm6.DataPropertyName = "TimeConfirm6";
            this.dgvListBillOrderTimeConfirm6.HeaderText = "TimeConfirm6";
            this.dgvListBillOrderTimeConfirm6.Name = "dgvListBillOrderTimeConfirm6";
            this.dgvListBillOrderTimeConfirm6.Visible = false;
            // 
            // dgvListBillOrderConfirm7
            // 
            this.dgvListBillOrderConfirm7.DataPropertyName = "TimeConfirm7";
            this.dgvListBillOrderConfirm7.HeaderText = "TimeConfirm7";
            this.dgvListBillOrderConfirm7.Name = "dgvListBillOrderConfirm7";
            this.dgvListBillOrderConfirm7.Visible = false;
            // 
            // dgvListBillOrderTimeConfirm8
            // 
            this.dgvListBillOrderTimeConfirm8.DataPropertyName = "TimeConfirm8";
            this.dgvListBillOrderTimeConfirm8.HeaderText = "TimeConfirm8";
            this.dgvListBillOrderTimeConfirm8.Name = "dgvListBillOrderTimeConfirm8";
            this.dgvListBillOrderTimeConfirm8.Visible = false;
            // 
            // dgvListBillOrderTimeConfirm9
            // 
            this.dgvListBillOrderTimeConfirm9.DataPropertyName = "TimeConfirm9";
            this.dgvListBillOrderTimeConfirm9.HeaderText = "TimeConfirm9";
            this.dgvListBillOrderTimeConfirm9.Name = "dgvListBillOrderTimeConfirm9";
            this.dgvListBillOrderTimeConfirm9.Visible = false;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLog.Controls.Add(this.rtxtHistory);
            this.groupBoxLog.Location = new System.Drawing.Point(6, 382);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(938, 175);
            this.groupBoxLog.TabIndex = 55;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Lịch sử đơn hàng";
            // 
            // rtxtHistory
            // 
            this.rtxtHistory.BackColor = System.Drawing.Color.White;
            this.rtxtHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtHistory.Font = new System.Drawing.Font("Arial", 10F);
            this.rtxtHistory.Location = new System.Drawing.Point(3, 17);
            this.rtxtHistory.Name = "rtxtHistory";
            this.rtxtHistory.ReadOnly = true;
            this.rtxtHistory.Size = new System.Drawing.Size(932, 155);
            this.rtxtHistory.TabIndex = 0;
            this.rtxtHistory.Text = "";
            // 
            // frmReportConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.dgvReportConfirm);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmReportConfirm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo xác thực đơn hàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmReportConfirm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportConfirm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportConfirm)).EndInit();
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpToDay;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDay;
        private System.Windows.Forms.TextBox txtQRCode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.DataGridView dgvReportConfirm;
        private System.Windows.Forms.Label lblTotalItem;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.RichTextBox rtxtHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderIDOrderSyn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderDriverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameDistributor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameStore;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderSumNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvListBillOrderConfirm1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvListBillOrderConfirm2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderDriverAccept;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderConfirm7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm9;
    }
}