namespace HMXHTD
{
    partial class frmTaskVehiceInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvVehicleInfo = new System.Windows.Forms.DataGridView();
            this.dgvVehicleInfoVehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleInfoIndexOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleInfoNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleInfoState1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicleInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(900, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "BẢNG THÔNG TIN PHƯƠNG TIỆN CHỜ LẤY HÀNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvVehicleInfo
            // 
            this.dgvVehicleInfo.AllowUserToAddRows = false;
            this.dgvVehicleInfo.AllowUserToDeleteRows = false;
            this.dgvVehicleInfo.AllowUserToResizeColumns = false;
            this.dgvVehicleInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvVehicleInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVehicleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVehicleInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            this.dgvVehicleInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVehicleInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicleInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVehicleInfo.ColumnHeadersHeight = 46;
            this.dgvVehicleInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVehicleInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvVehicleInfoVehicle,
            this.dgvVehicleInfoIndexOrder,
            this.dgvVehicleInfoNameProduct,
            this.dgvVehicleInfoState1});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVehicleInfo.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvVehicleInfo.EnableHeadersVisualStyles = false;
            this.dgvVehicleInfo.Location = new System.Drawing.Point(12, 44);
            this.dgvVehicleInfo.MultiSelect = false;
            this.dgvVehicleInfo.Name = "dgvVehicleInfo";
            this.dgvVehicleInfo.ReadOnly = true;
            this.dgvVehicleInfo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicleInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvVehicleInfo.RowHeadersVisible = false;
            this.dgvVehicleInfo.RowHeadersWidth = 36;
            this.dgvVehicleInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            this.dgvVehicleInfo.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvVehicleInfo.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            this.dgvVehicleInfo.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVehicleInfo.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvVehicleInfo.RowTemplate.Height = 42;
            this.dgvVehicleInfo.RowTemplate.ReadOnly = true;
            this.dgvVehicleInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicleInfo.Size = new System.Drawing.Size(876, 444);
            this.dgvVehicleInfo.TabIndex = 40;
            this.dgvVehicleInfo.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvVehicleInfo_RowPrePaint);
            // 
            // dgvVehicleInfoVehicle
            // 
            this.dgvVehicleInfoVehicle.DataPropertyName = "Vehicle";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVehicleInfoVehicle.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVehicleInfoVehicle.HeaderText = "  BIỂN SỐ";
            this.dgvVehicleInfoVehicle.Name = "dgvVehicleInfoVehicle";
            this.dgvVehicleInfoVehicle.ReadOnly = true;
            this.dgvVehicleInfoVehicle.Width = 150;
            // 
            // dgvVehicleInfoIndexOrder
            // 
            this.dgvVehicleInfoIndexOrder.DataPropertyName = "IndexOrder";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.dgvVehicleInfoIndexOrder.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVehicleInfoIndexOrder.HeaderText = "THỨ TỰ";
            this.dgvVehicleInfoIndexOrder.Name = "dgvVehicleInfoIndexOrder";
            this.dgvVehicleInfoIndexOrder.ReadOnly = true;
            this.dgvVehicleInfoIndexOrder.Width = 130;
            // 
            // dgvVehicleInfoNameProduct
            // 
            this.dgvVehicleInfoNameProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvVehicleInfoNameProduct.DataPropertyName = "NameProduct";
            this.dgvVehicleInfoNameProduct.HeaderText = "CHỦNG LOẠI HÀNG HÓA";
            this.dgvVehicleInfoNameProduct.Name = "dgvVehicleInfoNameProduct";
            this.dgvVehicleInfoNameProduct.ReadOnly = true;
            // 
            // dgvVehicleInfoState1
            // 
            this.dgvVehicleInfoState1.DataPropertyName = "State1";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVehicleInfoState1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvVehicleInfoState1.HeaderText = "TRẠNG THÁI";
            this.dgvVehicleInfoState1.Name = "dgvVehicleInfoState1";
            this.dgvVehicleInfoState1.ReadOnly = true;
            this.dgvVehicleInfoState1.Width = 195;
            // 
            // frmTaskVehiceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(210)))));
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dgvVehicleInfo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmTaskVehiceInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTaskVehiceInfo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTaskVehiceInfo_FormClosing);
            this.Shown += new System.EventHandler(this.frmTaskVehiceInfo_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTaskVehiceInfo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicleInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvVehicleInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleInfoVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleInfoIndexOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleInfoNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleInfoState1;
    }
}