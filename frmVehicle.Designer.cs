namespace HMXHTD
{
    partial class frmVehicle
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvVehicle = new System.Windows.Forms.DataGridView();
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.dgvVehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVehicle
            // 
            this.dgvVehicle.AllowUserToAddRows = false;
            this.dgvVehicle.AllowUserToDeleteRows = false;
            this.dgvVehicle.AllowUserToResizeColumns = false;
            this.dgvVehicle.AllowUserToResizeRows = false;
            this.dgvVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVehicle.BackgroundColor = System.Drawing.Color.White;
            this.dgvVehicle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvVehicle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvVehicle.ColumnHeadersHeight = 30;
            this.dgvVehicle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVehicle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvVehicleId,
            this.dgvVehicleCode,
            this.Column1,
            this.Column2,
            this.Column3,
            this.H,
            this.W,
            this.L});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVehicle.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvVehicle.EnableHeadersVisualStyles = false;
            this.dgvVehicle.Location = new System.Drawing.Point(7, 40);
            this.dgvVehicle.MultiSelect = false;
            this.dgvVehicle.Name = "dgvVehicle";
            this.dgvVehicle.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicle.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvVehicle.RowHeadersWidth = 36;
            this.dgvVehicle.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvVehicle.RowTemplate.Height = 26;
            this.dgvVehicle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicle.Size = new System.Drawing.Size(721, 415);
            this.dgvVehicle.TabIndex = 38;
            this.dgvVehicle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVehicle_CellDoubleClick);
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchKey.Font = new System.Drawing.Font("Arial", 12F);
            this.txtSearchKey.Location = new System.Drawing.Point(7, 8);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(720, 26);
            this.txtSearchKey.TabIndex = 39;
            this.txtSearchKey.TextChanged += new System.EventHandler(this.txtSearchKey_TextChanged);
            // 
            // dgvVehicleId
            // 
            this.dgvVehicleId.DataPropertyName = "IDVehicle";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvVehicleId.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvVehicleId.HeaderText = "Id";
            this.dgvVehicleId.Name = "dgvVehicleId";
            this.dgvVehicleId.ReadOnly = true;
            this.dgvVehicleId.Visible = false;
            this.dgvVehicleId.Width = 55;
            // 
            // dgvVehicleCode
            // 
            this.dgvVehicleCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvVehicleCode.DataPropertyName = "Vehicle";
            this.dgvVehicleCode.HeaderText = "Phương tiện";
            this.dgvVehicleCode.Name = "dgvVehicleCode";
            this.dgvVehicleCode.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "NameDriver";
            this.Column1.HeaderText = "Tên tài xế";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Tonnage";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column2.HeaderText = "Trọng tải";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TonnageDefault";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column3.HeaderText = "  Cân bì";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // H
            // 
            this.H.DataPropertyName = "HeightVehicle";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.H.DefaultCellStyle = dataGridViewCellStyle12;
            this.H.HeaderText = "  Cao";
            this.H.Name = "H";
            this.H.Width = 50;
            // 
            // W
            // 
            this.W.DataPropertyName = "WidthVehicle";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.W.DefaultCellStyle = dataGridViewCellStyle13;
            this.W.HeaderText = "  Rộng";
            this.W.Name = "W";
            this.W.Width = 50;
            // 
            // L
            // 
            this.L.DataPropertyName = "LongVehicle";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.L.DefaultCellStyle = dataGridViewCellStyle14;
            this.L.HeaderText = "   Dài";
            this.L.Name = "L";
            this.L.Width = 50;
            // 
            // frmVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.txtSearchKey);
            this.Controls.Add(this.dgvVehicle);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(750, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(750, 500);
            this.Name = "frmVehicle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn phương tiện vận tải";
            this.Load += new System.EventHandler(this.frmVehicle_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVehicle_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVehicle;
        private System.Windows.Forms.TextBox txtSearchKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn W;
        private System.Windows.Forms.DataGridViewTextBoxColumn L;
    }
}