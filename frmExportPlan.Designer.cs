namespace HMXHTD
{
    partial class frmExportPlan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPlan = new System.Windows.Forms.DataGridView();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.grbPlanInfo = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.txtDateEnd = new System.Windows.Forms.MaskedTextBox();
            this.txtDateStart = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtShipName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlanName = new System.Windows.Forms.TextBox();
            this.ckbSelectAllProduct = new System.Windows.Forms.CheckBox();
            this.dgvDistributor = new System.Windows.Forms.DataGridView();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ckbSelectAllDistributor = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPlanId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlanDistributorIds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlanStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlanEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlanShipName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPlanStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDistributorSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvDistributorIdSyn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDistributorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProductSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvProductIDProductSyn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProductNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProductOrderNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.grbPlanInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPlan
            // 
            this.dgvPlan.AllowUserToAddRows = false;
            this.dgvPlan.AllowUserToDeleteRows = false;
            this.dgvPlan.AllowUserToResizeColumns = false;
            this.dgvPlan.AllowUserToResizeRows = false;
            this.dgvPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPlan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPlan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlan.ColumnHeadersHeight = 30;
            this.dgvPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvPlanId,
            this.dgvPlanName,
            this.dgvPlanDistributorIds,
            this.dgvPlanStartDate,
            this.dgvPlanEndDate,
            this.dgvPlanShipName,
            this.dgvPlanStatus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlan.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPlan.EnableHeadersVisualStyles = false;
            this.dgvPlan.Location = new System.Drawing.Point(7, 7);
            this.dgvPlan.MultiSelect = false;
            this.dgvPlan.Name = "dgvPlan";
            this.dgvPlan.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlan.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPlan.RowHeadersWidth = 36;
            this.dgvPlan.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPlan.RowTemplate.Height = 26;
            this.dgvPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlan.Size = new System.Drawing.Size(601, 830);
            this.dgvPlan.TabIndex = 30;
            this.dgvPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlan_CellClick);
            this.dgvPlan.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlan_CellEnter);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgvProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProduct.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProduct.ColumnHeadersHeight = 30;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvProductSelect,
            this.dgvProductIDProductSyn,
            this.dgvProductNameProduct,
            this.dgvProductOrderNumber});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProduct.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvProduct.EnableHeadersVisualStyles = false;
            this.dgvProduct.Location = new System.Drawing.Point(0, 20);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProduct.RowTemplate.Height = 26;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(475, 206);
            this.dgvProduct.TabIndex = 31;
            this.dgvProduct.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellEndEdit);
            this.dgvProduct.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvProduct_EditingControlShowing);
            // 
            // grbPlanInfo
            // 
            this.grbPlanInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbPlanInfo.Controls.Add(this.label3);
            this.grbPlanInfo.Controls.Add(this.cbbStatus);
            this.grbPlanInfo.Controls.Add(this.txtDateEnd);
            this.grbPlanInfo.Controls.Add(this.txtDateStart);
            this.grbPlanInfo.Controls.Add(this.label5);
            this.grbPlanInfo.Controls.Add(this.label4);
            this.grbPlanInfo.Controls.Add(this.txtShipName);
            this.grbPlanInfo.Controls.Add(this.label2);
            this.grbPlanInfo.Controls.Add(this.label1);
            this.grbPlanInfo.Controls.Add(this.txtPlanName);
            this.grbPlanInfo.Location = new System.Drawing.Point(614, 0);
            this.grbPlanInfo.Name = "grbPlanInfo";
            this.grbPlanInfo.Size = new System.Drawing.Size(475, 186);
            this.grbPlanInfo.TabIndex = 32;
            this.grbPlanInfo.TabStop = false;
            this.grbPlanInfo.Text = "Thông tin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 49;
            this.label3.Text = "Trạng thái:";
            // 
            // cbbStatus
            // 
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Items.AddRange(new object[] {
            "Khởi tạo",
            "Đã duyệt",
            "Đã hoàn thành",
            "Đã bị hủy"});
            this.cbbStatus.Location = new System.Drawing.Point(138, 131);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(285, 23);
            this.cbbStatus.TabIndex = 48;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.BackColor = System.Drawing.Color.White;
            this.txtDateEnd.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateEnd.Location = new System.Drawing.Point(335, 97);
            this.txtDateEnd.Mask = "00/00/0000";
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(88, 25);
            this.txtDateEnd.TabIndex = 47;
            this.txtDateEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDateEnd.ValidatingType = typeof(System.DateTime);
            // 
            // txtDateStart
            // 
            this.txtDateStart.BackColor = System.Drawing.Color.White;
            this.txtDateStart.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateStart.Location = new System.Drawing.Point(138, 97);
            this.txtDateStart.Mask = "00/00/0000";
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(88, 25);
            this.txtDateStart.TabIndex = 46;
            this.txtDateStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDateStart.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 45;
            this.label5.Text = "Đến ngày:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Từ ngày:";
            // 
            // txtShipName
            // 
            this.txtShipName.Font = new System.Drawing.Font("Arial", 11F);
            this.txtShipName.Location = new System.Drawing.Point(138, 67);
            this.txtShipName.Name = "txtShipName";
            this.txtShipName.Size = new System.Drawing.Size(285, 24);
            this.txtShipName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên tàu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên kế hoạch:";
            // 
            // txtPlanName
            // 
            this.txtPlanName.Font = new System.Drawing.Font("Arial", 11F);
            this.txtPlanName.Location = new System.Drawing.Point(138, 37);
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.Size = new System.Drawing.Size(285, 24);
            this.txtPlanName.TabIndex = 0;
            // 
            // ckbSelectAllProduct
            // 
            this.ckbSelectAllProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSelectAllProduct.AutoSize = true;
            this.ckbSelectAllProduct.Location = new System.Drawing.Point(633, 600);
            this.ckbSelectAllProduct.Name = "ckbSelectAllProduct";
            this.ckbSelectAllProduct.Size = new System.Drawing.Size(15, 14);
            this.ckbSelectAllProduct.TabIndex = 35;
            this.ckbSelectAllProduct.UseVisualStyleBackColor = true;
            this.ckbSelectAllProduct.CheckedChanged += new System.EventHandler(this.ckbSelectAllProduct_CheckedChanged);
            // 
            // dgvDistributor
            // 
            this.dgvDistributor.AllowUserToAddRows = false;
            this.dgvDistributor.AllowUserToDeleteRows = false;
            this.dgvDistributor.AllowUserToResizeColumns = false;
            this.dgvDistributor.AllowUserToResizeRows = false;
            this.dgvDistributor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDistributor.BackgroundColor = System.Drawing.Color.White;
            this.dgvDistributor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDistributor.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDistributor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDistributor.ColumnHeadersHeight = 30;
            this.dgvDistributor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDistributor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDistributorSelect,
            this.dgvDistributorIdSyn,
            this.dgvDistributorName});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDistributor.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDistributor.EnableHeadersVisualStyles = false;
            this.dgvDistributor.Location = new System.Drawing.Point(0, 20);
            this.dgvDistributor.MultiSelect = false;
            this.dgvDistributor.Name = "dgvDistributor";
            this.dgvDistributor.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDistributor.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvDistributor.RowHeadersVisible = false;
            this.dgvDistributor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDistributor.RowTemplate.Height = 26;
            this.dgvDistributor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDistributor.Size = new System.Drawing.Size(475, 331);
            this.dgvDistributor.TabIndex = 36;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.Enabled = false;
            this.btnDel.Location = new System.Drawing.Point(937, 808);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 29);
            this.btnDel.TabIndex = 41;
            this.btnDel.Text = "Xóa";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(859, 808);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 29);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Location = new System.Drawing.Point(693, 808);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 29);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "Cập nhật";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(776, 808);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Ghi nhận";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(614, 808);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 29);
            this.btnAdd.TabIndex = 37;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ckbSelectAllDistributor
            // 
            this.ckbSelectAllDistributor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSelectAllDistributor.AutoSize = true;
            this.ckbSelectAllDistributor.Location = new System.Drawing.Point(19, 29);
            this.ckbSelectAllDistributor.Name = "ckbSelectAllDistributor";
            this.ckbSelectAllDistributor.Size = new System.Drawing.Size(15, 14);
            this.ckbSelectAllDistributor.TabIndex = 42;
            this.ckbSelectAllDistributor.UseVisualStyleBackColor = true;
            this.ckbSelectAllDistributor.CheckedChanged += new System.EventHandler(this.ckbSelectAllDistributor_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ckbSelectAllDistributor);
            this.groupBox1.Controls.Add(this.dgvDistributor);
            this.groupBox1.Location = new System.Drawing.Point(614, 202);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 351);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách nhà phân phối";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvProduct);
            this.groupBox2.Location = new System.Drawing.Point(614, 571);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 226);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách sản phẩm";
            // 
            // dgvPlanId
            // 
            this.dgvPlanId.DataPropertyName = "Id";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPlanId.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPlanId.HeaderText = "Số hiệu";
            this.dgvPlanId.Name = "dgvPlanId";
            this.dgvPlanId.ReadOnly = true;
            // 
            // dgvPlanName
            // 
            this.dgvPlanName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPlanName.DataPropertyName = "Name";
            this.dgvPlanName.HeaderText = "Kế hoạch";
            this.dgvPlanName.Name = "dgvPlanName";
            this.dgvPlanName.ReadOnly = true;
            // 
            // dgvPlanDistributorIds
            // 
            this.dgvPlanDistributorIds.DataPropertyName = "SynDistributorIds";
            this.dgvPlanDistributorIds.HeaderText = "DS Nhà phân phối";
            this.dgvPlanDistributorIds.Name = "dgvPlanDistributorIds";
            this.dgvPlanDistributorIds.Visible = false;
            // 
            // dgvPlanStartDate
            // 
            this.dgvPlanStartDate.DataPropertyName = "StartDate";
            this.dgvPlanStartDate.HeaderText = "Từ ngày";
            this.dgvPlanStartDate.Name = "dgvPlanStartDate";
            this.dgvPlanStartDate.Width = 120;
            // 
            // dgvPlanEndDate
            // 
            this.dgvPlanEndDate.DataPropertyName = "EndDate";
            this.dgvPlanEndDate.HeaderText = "Đến ngày";
            this.dgvPlanEndDate.Name = "dgvPlanEndDate";
            this.dgvPlanEndDate.Width = 120;
            // 
            // dgvPlanShipName
            // 
            this.dgvPlanShipName.DataPropertyName = "ShipName";
            this.dgvPlanShipName.HeaderText = "Tên tàu";
            this.dgvPlanShipName.Name = "dgvPlanShipName";
            this.dgvPlanShipName.ReadOnly = true;
            this.dgvPlanShipName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlanShipName.Width = 320;
            // 
            // dgvPlanStatus
            // 
            this.dgvPlanStatus.DataPropertyName = "State";
            this.dgvPlanStatus.HeaderText = "Trạng thái";
            this.dgvPlanStatus.Name = "dgvPlanStatus";
            this.dgvPlanStatus.ReadOnly = true;
            this.dgvPlanStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlanStatus.Width = 220;
            // 
            // dgvDistributorSelect
            // 
            this.dgvDistributorSelect.DataPropertyName = "Select";
            this.dgvDistributorSelect.HeaderText = "    #";
            this.dgvDistributorSelect.Name = "dgvDistributorSelect";
            this.dgvDistributorSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDistributorSelect.Width = 50;
            // 
            // dgvDistributorIdSyn
            // 
            this.dgvDistributorIdSyn.DataPropertyName = "IDDistributorSyn";
            this.dgvDistributorIdSyn.HeaderText = "ID";
            this.dgvDistributorIdSyn.Name = "dgvDistributorIdSyn";
            this.dgvDistributorIdSyn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvDistributorIdSyn.Visible = false;
            // 
            // dgvDistributorName
            // 
            this.dgvDistributorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvDistributorName.DataPropertyName = "NameDistributor";
            this.dgvDistributorName.HeaderText = "Tên nhà phân phối";
            this.dgvDistributorName.Name = "dgvDistributorName";
            this.dgvDistributorName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvProductSelect
            // 
            this.dgvProductSelect.HeaderText = "    #";
            this.dgvProductSelect.Name = "dgvProductSelect";
            this.dgvProductSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductSelect.Width = 50;
            // 
            // dgvProductIDProductSyn
            // 
            this.dgvProductIDProductSyn.DataPropertyName = "IDProductSyn";
            this.dgvProductIDProductSyn.HeaderText = "IDProduct";
            this.dgvProductIDProductSyn.Name = "dgvProductIDProductSyn";
            this.dgvProductIDProductSyn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvProductNameProduct
            // 
            this.dgvProductNameProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvProductNameProduct.DataPropertyName = "NameProduct";
            this.dgvProductNameProduct.HeaderText = "Sản phẩm";
            this.dgvProductNameProduct.Name = "dgvProductNameProduct";
            this.dgvProductNameProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvProductOrderNumber
            // 
            dataGridViewCellStyle6.NullValue = null;
            this.dgvProductOrderNumber.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvProductOrderNumber.HeaderText = "Khối lượng (tấn)";
            this.dgvProductOrderNumber.Name = "dgvProductOrderNumber";
            this.dgvProductOrderNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvProductOrderNumber.Width = 120;
            // 
            // frmExportPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 845);
            this.Controls.Add(this.ckbSelectAllProduct);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grbPlanInfo);
            this.Controls.Add(this.dgvPlan);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmExportPlan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExportPlan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExportPlan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmExportPlan_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.grbPlanInfo.ResumeLayout(false);
            this.grbPlanInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlan;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.GroupBox grbPlanInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlanName;
        private System.Windows.Forms.CheckBox ckbSelectAllProduct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDistributor;
        private System.Windows.Forms.TextBox txtShipName;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox ckbSelectAllDistributor;
        private System.Windows.Forms.MaskedTextBox txtDateEnd;
        private System.Windows.Forms.MaskedTextBox txtDateStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanDistributorIds;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanShipName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlanStatus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvDistributorSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDistributorIdSyn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDistributorName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvProductSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductIDProductSyn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductOrderNumber;
    }
}