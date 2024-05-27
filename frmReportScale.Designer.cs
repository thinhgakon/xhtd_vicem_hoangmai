
namespace HMXHTD
{
    partial class frmReportScale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalItem = new System.Windows.Forms.Label();
            this.dtpToDay = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
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
            this.dgvListBillOrderTimeConfirm1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportConfirm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblTotalItem);
            this.groupBox1.Controls.Add(this.dtpToDay);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFromDay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(938, 60);
            this.groupBox1.TabIndex = 60;
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
            this.btnSearch.Location = new System.Drawing.Point(846, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 55;
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
            this.txtSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(384, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(542, 26);
            this.txtSearch.TabIndex = 54;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
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
            this.btnClose.Location = new System.Drawing.Point(871, 564);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 29);
            this.btnClose.TabIndex = 63;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Location = new System.Drawing.Point(119, 564);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 29);
            this.btnPrint.TabIndex = 62;
            this.btnPrint.Text = "In";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.Location = new System.Drawing.Point(6, 564);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(111, 29);
            this.btnExportExcel.TabIndex = 61;
            this.btnExportExcel.Text = "Xuất file Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportConfirm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.dgvListBillOrderTimeConfirm1,
            this.Column1});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReportConfirm.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvReportConfirm.EnableHeadersVisualStyles = false;
            this.dgvReportConfirm.Location = new System.Drawing.Point(6, 73);
            this.dgvReportConfirm.MultiSelect = false;
            this.dgvReportConfirm.Name = "dgvReportConfirm";
            this.dgvReportConfirm.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportConfirm.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvReportConfirm.RowHeadersVisible = false;
            this.dgvReportConfirm.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReportConfirm.RowTemplate.Height = 26;
            this.dgvReportConfirm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportConfirm.Size = new System.Drawing.Size(938, 485);
            this.dgvReportConfirm.TabIndex = 64;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.dgvListBillOrderOrderDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListBillOrderOrderDate.HeaderText = "    Ngày đặt";
            this.dgvListBillOrderOrderDate.Name = "dgvListBillOrderOrderDate";
            this.dgvListBillOrderOrderDate.ReadOnly = true;
            this.dgvListBillOrderOrderDate.Width = 85;
            // 
            // dgvListBillOrderIDOrderSyn
            // 
            this.dgvListBillOrderIDOrderSyn.DataPropertyName = "DeliveryCode";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListBillOrderIDOrderSyn.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvListBillOrderIDOrderSyn.HeaderText = "      MSGH";
            this.dgvListBillOrderIDOrderSyn.Name = "dgvListBillOrderIDOrderSyn";
            this.dgvListBillOrderIDOrderSyn.ReadOnly = true;
            this.dgvListBillOrderIDOrderSyn.Width = 85;
            // 
            // dgvListBillOrderVehicle
            // 
            this.dgvListBillOrderVehicle.DataPropertyName = "Vehicle";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListBillOrderVehicle.DefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.dgvListBillOrderSumNumber.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvListBillOrderSumNumber.HeaderText = "    S.L";
            this.dgvListBillOrderSumNumber.Name = "dgvListBillOrderSumNumber";
            this.dgvListBillOrderSumNumber.ReadOnly = true;
            this.dgvListBillOrderSumNumber.Width = 50;
            // 
            // dgvListBillOrderTimeConfirm1
            // 
            this.dgvListBillOrderTimeConfirm1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvListBillOrderTimeConfirm1.DataPropertyName = "TimeConfirm3";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial Narrow", 10.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle6.NullValue = null;
            this.dgvListBillOrderTimeConfirm1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvListBillOrderTimeConfirm1.HeaderText = "           Giờ cần vào";
            this.dgvListBillOrderTimeConfirm1.Name = "dgvListBillOrderTimeConfirm1";
            this.dgvListBillOrderTimeConfirm1.ReadOnly = true;
            this.dgvListBillOrderTimeConfirm1.Width = 150;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "TimeConfirm7";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial Narrow", 10.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.Format = "dd/MM/yyyy HH:mm:ss";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column1.HeaderText = "         Giờ cân cổng";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // frmReportScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dgvReportConfirm);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmReportScale";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo cân vào, ra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmReportScale_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportScale_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportConfirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTotalItem;
        private System.Windows.Forms.DateTimePicker dtpToDay;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.DataGridView dgvReportConfirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderIDOrderSyn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderDriverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameDistributor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderNameStore;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderSumNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvListBillOrderTimeConfirm1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}