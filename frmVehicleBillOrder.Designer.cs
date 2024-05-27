
namespace HMXHTD
{
    partial class frmVehicleBillOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.dgvBillOrder = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvBillOrderDeliveryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderNameProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderSumNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBillOrderDriverAccept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVehicle
            // 
            this.lblVehicle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehicle.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle.Location = new System.Drawing.Point(9, 7);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(616, 33);
            this.lblVehicle.TabIndex = 62;
            this.lblVehicle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dgvBillOrder.ColumnHeadersHeight = 30;
            this.dgvBillOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBillOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvBillOrderDeliveryCode,
            this.dgvBillOrderOrderDate,
            this.dgvBillOrderNameProduct,
            this.dgvBillOrderSumNumber,
            this.dgvBillOrderDriverAccept});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBillOrder.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBillOrder.EnableHeadersVisualStyles = false;
            this.dgvBillOrder.Location = new System.Drawing.Point(9, 46);
            this.dgvBillOrder.MultiSelect = false;
            this.dgvBillOrder.Name = "dgvBillOrder";
            this.dgvBillOrder.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBillOrder.RowHeadersVisible = false;
            this.dgvBillOrder.RowHeadersWidth = 36;
            this.dgvBillOrder.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBillOrder.RowTemplate.Height = 26;
            this.dgvBillOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBillOrder.Size = new System.Drawing.Size(616, 173);
            this.dgvBillOrder.TabIndex = 61;
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
            this.btnClose.Location = new System.Drawing.Point(273, 225);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 29);
            this.btnClose.TabIndex = 59;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvBillOrderDeliveryCode
            // 
            this.dgvBillOrderDeliveryCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderDeliveryCode.DataPropertyName = "DeliveryCode";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvBillOrderDeliveryCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBillOrderDeliveryCode.HeaderText = "      MSGH";
            this.dgvBillOrderDeliveryCode.Name = "dgvBillOrderDeliveryCode";
            this.dgvBillOrderDeliveryCode.ReadOnly = true;
            this.dgvBillOrderDeliveryCode.Width = 80;
            // 
            // dgvBillOrderOrderDate
            // 
            this.dgvBillOrderOrderDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderOrderDate.DataPropertyName = "OrderDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.dgvBillOrderOrderDate.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvBillOrderSumNumber.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBillOrderSumNumber.HeaderText = "Số lượng";
            this.dgvBillOrderSumNumber.Name = "dgvBillOrderSumNumber";
            this.dgvBillOrderSumNumber.ReadOnly = true;
            this.dgvBillOrderSumNumber.Width = 65;
            // 
            // dgvBillOrderDriverAccept
            // 
            this.dgvBillOrderDriverAccept.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvBillOrderDriverAccept.DataPropertyName = "DriverAccept";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "f";
            dataGridViewCellStyle4.NullValue = null;
            this.dgvBillOrderDriverAccept.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBillOrderDriverAccept.HeaderText = "TG nhận đơn";
            this.dgvBillOrderDriverAccept.Name = "dgvBillOrderDriverAccept";
            this.dgvBillOrderDriverAccept.Width = 150;
            // 
            // frmVehicleBillOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 261);
            this.Controls.Add(this.lblVehicle);
            this.Controls.Add(this.dgvBillOrder);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(650, 300);
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.Name = "frmVehicleBillOrder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đơn hàng";
            this.Load += new System.EventHandler(this.frmVehicleBillOrder_Load);
            this.Shown += new System.EventHandler(this.frmVehicleBillOrder_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVehicleBillOrder_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.DataGridView dgvBillOrder;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderDeliveryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderNameProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderSumNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBillOrderDriverAccept;
    }
}