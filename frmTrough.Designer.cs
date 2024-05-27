namespace HMXHTD
{
    partial class frmTrough
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTrough = new System.Windows.Forms.DataGridView();
            this.dgvTroughId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughNameProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughWorking = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTroughProblem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTroughState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.dgvProductSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvProductIDProductSyn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProductNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.txtW = new System.Windows.Forms.TextBox();
            this.txtH = new System.Windows.Forms.TextBox();
            this.ckbState = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrough)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTrough
            // 
            this.dgvTrough.AllowUserToAddRows = false;
            this.dgvTrough.AllowUserToDeleteRows = false;
            this.dgvTrough.AllowUserToResizeColumns = false;
            this.dgvTrough.AllowUserToResizeRows = false;
            this.dgvTrough.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTrough.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrough.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTrough.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTrough.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTrough.ColumnHeadersHeight = 30;
            this.dgvTrough.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTrough.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTroughId,
            this.dgvTroughName,
            this.dgvTroughNameProductId,
            this.dgvTroughL,
            this.dgvTroughW,
            this.dgvTroughH,
            this.dgvTroughWorking,
            this.dgvTroughProblem,
            this.dgvTroughState});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTrough.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTrough.EnableHeadersVisualStyles = false;
            this.dgvTrough.Location = new System.Drawing.Point(7, 7);
            this.dgvTrough.MultiSelect = false;
            this.dgvTrough.Name = "dgvTrough";
            this.dgvTrough.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTrough.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvTrough.RowHeadersWidth = 36;
            this.dgvTrough.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTrough.RowTemplate.Height = 26;
            this.dgvTrough.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrough.Size = new System.Drawing.Size(587, 585);
            this.dgvTrough.TabIndex = 30;
            this.dgvTrough.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrough_CellClick);
            this.dgvTrough.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrough_CellEnter);
            // 
            // dgvTroughId
            // 
            this.dgvTroughId.DataPropertyName = "Id";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvTroughId.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTroughId.HeaderText = "Số hiệu";
            this.dgvTroughId.Name = "dgvTroughId";
            this.dgvTroughId.ReadOnly = true;
            this.dgvTroughId.Width = 55;
            // 
            // dgvTroughName
            // 
            this.dgvTroughName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvTroughName.DataPropertyName = "Name";
            this.dgvTroughName.HeaderText = "Tên gọi";
            this.dgvTroughName.Name = "dgvTroughName";
            this.dgvTroughName.ReadOnly = true;
            // 
            // dgvTroughNameProductId
            // 
            this.dgvTroughNameProductId.DataPropertyName = "ProductId";
            this.dgvTroughNameProductId.HeaderText = "ProductId";
            this.dgvTroughNameProductId.Name = "dgvTroughNameProductId";
            this.dgvTroughNameProductId.Visible = false;
            // 
            // dgvTroughL
            // 
            this.dgvTroughL.DataPropertyName = "Long";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvTroughL.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTroughL.HeaderText = "Chiều dài";
            this.dgvTroughL.Name = "dgvTroughL";
            this.dgvTroughL.ReadOnly = true;
            this.dgvTroughL.Width = 70;
            // 
            // dgvTroughW
            // 
            this.dgvTroughW.DataPropertyName = "Width";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvTroughW.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTroughW.HeaderText = "Chiều rộng";
            this.dgvTroughW.Name = "dgvTroughW";
            this.dgvTroughW.ReadOnly = true;
            this.dgvTroughW.Width = 75;
            // 
            // dgvTroughH
            // 
            this.dgvTroughH.DataPropertyName = "Height";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvTroughH.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTroughH.HeaderText = "Chiều cao";
            this.dgvTroughH.Name = "dgvTroughH";
            this.dgvTroughH.ReadOnly = true;
            this.dgvTroughH.Width = 70;
            // 
            // dgvTroughWorking
            // 
            this.dgvTroughWorking.DataPropertyName = "Working";
            this.dgvTroughWorking.HeaderText = "Hoạt động";
            this.dgvTroughWorking.Name = "dgvTroughWorking";
            this.dgvTroughWorking.ReadOnly = true;
            this.dgvTroughWorking.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTroughWorking.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvTroughWorking.Width = 69;
            // 
            // dgvTroughProblem
            // 
            this.dgvTroughProblem.DataPropertyName = "Problem";
            this.dgvTroughProblem.HeaderText = "Sự cố";
            this.dgvTroughProblem.Name = "dgvTroughProblem";
            this.dgvTroughProblem.ReadOnly = true;
            this.dgvTroughProblem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTroughProblem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvTroughProblem.Width = 46;
            // 
            // dgvTroughState
            // 
            this.dgvTroughState.DataPropertyName = "State";
            this.dgvTroughState.HeaderText = "Trạng thái";
            this.dgvTroughState.Name = "dgvTroughState";
            this.dgvTroughState.ReadOnly = true;
            this.dgvTroughState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTroughState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvTroughState.Width = 70;
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvProduct.ColumnHeadersHeight = 30;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvProductSelect,
            this.dgvProductIDProductSyn,
            this.dgvProductNameProduct});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProduct.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvProduct.EnableHeadersVisualStyles = false;
            this.dgvProduct.Location = new System.Drawing.Point(600, 117);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProduct.RowTemplate.Height = 26;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(392, 440);
            this.dgvProduct.TabIndex = 31;
            // 
            // dgvProductSelect
            // 
            this.dgvProductSelect.DataPropertyName = "Select";
            this.dgvProductSelect.HeaderText = "    #";
            this.dgvProductSelect.Name = "dgvProductSelect";
            this.dgvProductSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvProductSelect.Width = 50;
            // 
            // dgvProductIDProductSyn
            // 
            this.dgvProductIDProductSyn.DataPropertyName = "IDProductSyn";
            this.dgvProductIDProductSyn.HeaderText = "IDProduct";
            this.dgvProductIDProductSyn.Name = "dgvProductIDProductSyn";
            this.dgvProductIDProductSyn.ReadOnly = true;
            this.dgvProductIDProductSyn.Visible = false;
            // 
            // dgvProductNameProduct
            // 
            this.dgvProductNameProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvProductNameProduct.DataPropertyName = "NameProduct";
            this.dgvProductNameProduct.HeaderText = "Sản phẩm";
            this.dgvProductNameProduct.Name = "dgvProductNameProduct";
            this.dgvProductNameProduct.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtL);
            this.groupBox1.Controls.Add(this.txtW);
            this.groupBox1.Controls.Add(this.txtH);
            this.groupBox1.Controls.Add(this.ckbState);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(600, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 110);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Dài:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Rộng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cao:";
            // 
            // txtL
            // 
            this.txtL.Font = new System.Drawing.Font("Arial", 11F);
            this.txtL.Location = new System.Drawing.Point(314, 51);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(67, 24);
            this.txtL.TabIndex = 5;
            this.txtL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtW
            // 
            this.txtW.Font = new System.Drawing.Font("Arial", 11F);
            this.txtW.Location = new System.Drawing.Point(196, 51);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(67, 24);
            this.txtW.TabIndex = 4;
            this.txtW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtH
            // 
            this.txtH.Font = new System.Drawing.Font("Arial", 11F);
            this.txtH.Location = new System.Drawing.Point(75, 51);
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(67, 24);
            this.txtH.TabIndex = 3;
            this.txtH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ckbState
            // 
            this.ckbState.AutoSize = true;
            this.ckbState.Location = new System.Drawing.Point(75, 85);
            this.ckbState.Name = "ckbState";
            this.ckbState.Size = new System.Drawing.Size(103, 19);
            this.ckbState.TabIndex = 2;
            this.ckbState.Text = "Trạng thái mở";
            this.ckbState.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên máng:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Arial", 11F);
            this.txtName.Location = new System.Drawing.Point(75, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(306, 24);
            this.txtName.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(600, 563);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "Cập nhật";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(678, 563);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.Location = new System.Drawing.Point(619, 126);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(15, 14);
            this.ckbSelectAll.TabIndex = 35;
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.ckbSelectAll_CheckedChanged);
            // 
            // frmTrough
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.ckbSelectAll);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.dgvTrough);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmTrough";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTrough";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTrough_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTrough_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrough)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTrough;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox ckbState;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox ckbSelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvProductSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductIDProductSyn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductNameProduct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.TextBox txtH;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughNameProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughW;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughH;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvTroughWorking;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvTroughProblem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvTroughState;
    }
}