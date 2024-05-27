namespace HMXHTD
{
    partial class frmVehicleDeliveryCode
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvBillOrder = new System.Windows.Forms.DataGridView();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.dgvBillOrderDeliveryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderSumNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderPrioritize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(319, 224);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 29);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(214, 224);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 29);
            this.btnSave.TabIndex = 56;
            this.btnSave.Text = "Hủy số thứ tự";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvBillOrder
            // 
            this.dgvBillOrder.AllowUserToAddRows = false;
            this.dgvBillOrder.AllowUserToDeleteRows = false;
            this.dgvBillOrder.AllowUserToResizeColumns = false;
            this.dgvBillOrder.AllowUserToResizeRows = false;
            this.dgvBillOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBillOrder.BackgroundColor = System.Drawing.Color.White;
            this.dgvBillOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvBillOrder.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvBillOrder.ColumnHeadersHeight = 30;
            this.dgvBillOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBillOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvBillOrderDeliveryCode,
            this.dgvBillOrderOrderDate,
            this.dgvBillOrderNameProduct,
            this.dgvBillOrderSumNumber,
            this.dgvBillOrderPrioritize,
            this.Column1});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBillOrder.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvBillOrder.EnableHeadersVisualStyles = false;
            this.dgvBillOrder.Location = new System.Drawing.Point(9, 45);
            this.dgvBillOrder.MultiSelect = false;
            this.dgvBillOrder.Name = "dgvBillOrder";
            this.dgvBillOrder.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvBillOrder.RowHeadersVisible = false;
            this.dgvBillOrder.RowHeadersWidth = 36;
            this.dgvBillOrder.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBillOrder.RowTemplate.Height = 26;
            this.dgvBillOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillOrder.Size = new System.Drawing.Size(616, 173);
            this.dgvBillOrder.TabIndex = 57;
            // 
            // lblVehicle
            // 
            this.lblVehicle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehicle.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle.Location = new System.Drawing.Point(9, 6);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(616, 33);
            this.lblVehicle.TabIndex = 58;
            this.lblVehicle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvBillOrderDeliveryCode
            // 
            this.dgvBillOrderDeliveryCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderDeliveryCode.DataPropertyName = "DeliveryCode";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvBillOrderDeliveryCode.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvBillOrderDeliveryCode.HeaderText = "      MSGH";
            this.dgvBillOrderDeliveryCode.Name = "dgvBillOrderDeliveryCode";
            this.dgvBillOrderDeliveryCode.ReadOnly = true;
            this.dgvBillOrderDeliveryCode.Width = 80;
            // 
            // dgvBillOrderOrderDate
            // 
            this.dgvBillOrderOrderDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderOrderDate.DataPropertyName = "OrderDate";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.dgvBillOrderOrderDate.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvBillOrderOrderDate.HeaderText = "   Ngày đặt";
            this.dgvBillOrderOrderDate.Name = "dgvBillOrderOrderDate";
            this.dgvBillOrderOrderDate.ReadOnly = true;
            this.dgvBillOrderOrderDate.Width = 80;
            // 
            // dgvBillOrderNameProduct
            // 
            this.dgvBillOrderNameProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvBillOrderNameProduct.DataPropertyName = "NameProduct";
            this.dgvBillOrderNameProduct.HeaderText = "Hàng hóa";
            this.dgvBillOrderNameProduct.Name = "dgvBillOrderNameProduct";
            this.dgvBillOrderNameProduct.ReadOnly = true;
            // 
            // dgvBillOrderSumNumber
            // 
            this.dgvBillOrderSumNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderSumNumber.DataPropertyName = "SumNumber";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvBillOrderSumNumber.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvBillOrderSumNumber.HeaderText = "Số lượng";
            this.dgvBillOrderSumNumber.Name = "dgvBillOrderSumNumber";
            this.dgvBillOrderSumNumber.ReadOnly = true;
            this.dgvBillOrderSumNumber.Width = 65;
            // 
            // dgvBillOrderPrioritize
            // 
            this.dgvBillOrderPrioritize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderPrioritize.DataPropertyName = "Prioritize";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvBillOrderPrioritize.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvBillOrderPrioritize.HeaderText = "Ưu tiên";
            this.dgvBillOrderPrioritize.Name = "dgvBillOrderPrioritize";
            this.dgvBillOrderPrioritize.Visible = false;
            this.dgvBillOrderPrioritize.Width = 55;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "State1";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column1.HeaderText = "         Trạng thái";
            this.Column1.Name = "Column1";
            this.Column1.Width = 120;
            // 
            // frmVehicleDeliveryCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 261);
            this.Controls.Add(this.lblVehicle);
            this.Controls.Add(this.dgvBillOrder);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(650, 300);
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.Name = "frmVehicleDeliveryCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đơn hàng";
            this.Shown += new System.EventHandler(this.frmVehicleDeliveryCode_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVehicleDeliveryCode_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvBillOrder;
        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderDeliveryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderSumNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderPrioritize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}