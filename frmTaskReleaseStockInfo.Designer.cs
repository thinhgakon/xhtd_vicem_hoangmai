
namespace HMXHTD
{
    partial class frmTaskReleaseStockInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.dgvAccountUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ckbPCB30 = new System.Windows.Forms.CheckBox();
            this.ckbPCB40 = new System.Windows.Forms.CheckBox();
            this.ckbROI = new System.Windows.Forms.CheckBox();
            this.ckbClinker = new System.Windows.Forms.CheckBox();
            this.ckbXuatKhau = new System.Windows.Forms.CheckBox();
            this.dgvTrough = new System.Windows.Forms.DataGridView();
            this.dgvTroughSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTroughLineCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTroughName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrough)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvAccount);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 514);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách tài khoản";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbXuatKhau);
            this.groupBox2.Controls.Add(this.ckbClinker);
            this.groupBox2.Controls.Add(this.ckbROI);
            this.groupBox2.Controls.Add(this.ckbPCB40);
            this.groupBox2.Controls.Add(this.ckbPCB30);
            this.groupBox2.Location = new System.Drawing.Point(362, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chủng loại sản phẩm";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvTrough);
            this.groupBox3.Location = new System.Drawing.Point(362, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(516, 348);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Danh sách kho, bãi";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(362, 526);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 29);
            this.btnSave.TabIndex = 69;
            this.btnSave.Text = "Ghi nhận";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(456, 526);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 29);
            this.btnClose.TabIndex = 70;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvAccount.ColumnHeadersHeight = 30;
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAccountUserName,
            this.dgvAccountFullName});
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
            this.dgvAccount.Location = new System.Drawing.Point(3, 17);
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
            this.dgvAccount.Size = new System.Drawing.Size(344, 494);
            this.dgvAccount.TabIndex = 4;
            this.dgvAccount.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccount_CellClick);
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
            this.dgvAccountFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAccountFullName.DataPropertyName = "Full_Name";
            this.dgvAccountFullName.HeaderText = "Tên tài khoản";
            this.dgvAccountFullName.Name = "dgvAccountFullName";
            this.dgvAccountFullName.ReadOnly = true;
            // 
            // ckbPCB30
            // 
            this.ckbPCB30.AutoSize = true;
            this.ckbPCB30.Location = new System.Drawing.Point(18, 26);
            this.ckbPCB30.Name = "ckbPCB30";
            this.ckbPCB30.Size = new System.Drawing.Size(137, 19);
            this.ckbPCB30.TabIndex = 0;
            this.ckbPCB30.Text = "Xi măng bao PCB30";
            this.ckbPCB30.UseVisualStyleBackColor = true;
            // 
            // ckbPCB40
            // 
            this.ckbPCB40.AutoSize = true;
            this.ckbPCB40.Location = new System.Drawing.Point(18, 51);
            this.ckbPCB40.Name = "ckbPCB40";
            this.ckbPCB40.Size = new System.Drawing.Size(137, 19);
            this.ckbPCB40.TabIndex = 1;
            this.ckbPCB40.Text = "Xi măng bao PCB40";
            this.ckbPCB40.UseVisualStyleBackColor = true;
            // 
            // ckbROI
            // 
            this.ckbROI.AutoSize = true;
            this.ckbROI.Location = new System.Drawing.Point(18, 76);
            this.ckbROI.Name = "ckbROI";
            this.ckbROI.Size = new System.Drawing.Size(89, 19);
            this.ckbROI.TabIndex = 2;
            this.ckbROI.Text = "Xi măng rời";
            this.ckbROI.UseVisualStyleBackColor = true;
            // 
            // ckbClinker
            // 
            this.ckbClinker.AutoSize = true;
            this.ckbClinker.Location = new System.Drawing.Point(18, 101);
            this.ckbClinker.Name = "ckbClinker";
            this.ckbClinker.Size = new System.Drawing.Size(65, 19);
            this.ckbClinker.TabIndex = 3;
            this.ckbClinker.Text = "Clinker";
            this.ckbClinker.UseVisualStyleBackColor = true;
            // 
            // ckbXuatKhau
            // 
            this.ckbXuatKhau.AutoSize = true;
            this.ckbXuatKhau.Location = new System.Drawing.Point(18, 126);
            this.ckbXuatKhau.Name = "ckbXuatKhau";
            this.ckbXuatKhau.Size = new System.Drawing.Size(80, 19);
            this.ckbXuatKhau.TabIndex = 4;
            this.ckbXuatKhau.Text = "Xuất khẩu";
            this.ckbXuatKhau.UseVisualStyleBackColor = true;
            // 
            // dgvTrough
            // 
            this.dgvTrough.AllowUserToAddRows = false;
            this.dgvTrough.AllowUserToDeleteRows = false;
            this.dgvTrough.AllowUserToResizeColumns = false;
            this.dgvTrough.AllowUserToResizeRows = false;
            this.dgvTrough.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrough.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTrough.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTrough.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvTrough.ColumnHeadersHeight = 30;
            this.dgvTrough.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTrough.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTroughSelect,
            this.dgvTroughLineCode,
            this.dgvTroughName});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTrough.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvTrough.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrough.EnableHeadersVisualStyles = false;
            this.dgvTrough.Location = new System.Drawing.Point(3, 17);
            this.dgvTrough.MultiSelect = false;
            this.dgvTrough.Name = "dgvTrough";
            this.dgvTrough.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTrough.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvTrough.RowHeadersVisible = false;
            this.dgvTrough.RowHeadersWidth = 36;
            this.dgvTrough.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTrough.RowTemplate.Height = 26;
            this.dgvTrough.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrough.Size = new System.Drawing.Size(510, 328);
            this.dgvTrough.TabIndex = 5;
            this.dgvTrough.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrough_CellContentClick);
            // 
            // dgvTroughSelect
            // 
            this.dgvTroughSelect.DataPropertyName = "Select";
            this.dgvTroughSelect.HeaderText = "   ::";
            this.dgvTroughSelect.Name = "dgvTroughSelect";
            this.dgvTroughSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTroughSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvTroughSelect.Width = 35;
            // 
            // dgvTroughLineCode
            // 
            this.dgvTroughLineCode.DataPropertyName = "LineCode";
            this.dgvTroughLineCode.HeaderText = "Số hiệu";
            this.dgvTroughLineCode.Name = "dgvTroughLineCode";
            this.dgvTroughLineCode.ReadOnly = true;
            // 
            // dgvTroughName
            // 
            this.dgvTroughName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvTroughName.DataPropertyName = "Name";
            this.dgvTroughName.HeaderText = "Kho, bãi";
            this.dgvTroughName.Name = "dgvTroughName";
            this.dgvTroughName.ReadOnly = true;
            // 
            // frmTaskReleaseStockInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmTaskReleaseStockInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cấu hình kho, bãi";
            this.Load += new System.EventHandler(this.frmTaskReleaseStockInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTaskReleaseStockInfo_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrough)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountFullName;
        private System.Windows.Forms.CheckBox ckbPCB40;
        private System.Windows.Forms.CheckBox ckbPCB30;
        private System.Windows.Forms.CheckBox ckbXuatKhau;
        private System.Windows.Forms.CheckBox ckbClinker;
        private System.Windows.Forms.CheckBox ckbROI;
        private System.Windows.Forms.DataGridView dgvTrough;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvTroughSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughLineCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTroughName;
    }
}