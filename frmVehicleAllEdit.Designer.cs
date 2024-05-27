
namespace HMXHTD
{
    partial class frmVehicleAllEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtVehicleCode = new System.Windows.Forms.TextBox();
            this.txtvehicleCertificate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbvehicleType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTransportMethodId = new System.Windows.Forms.ComboBox();
            this.dtpRegistrationDeadline = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWeightLimit = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phương tiện:";
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.SystemColors.Control;
            this.txtId.Font = new System.Drawing.Font("Arial", 11F);
            this.txtId.Location = new System.Drawing.Point(136, 22);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(100, 24);
            this.txtId.TabIndex = 1;
            // 
            // txtVehicleCode
            // 
            this.txtVehicleCode.BackColor = System.Drawing.Color.White;
            this.txtVehicleCode.Font = new System.Drawing.Font("Arial", 11F);
            this.txtVehicleCode.Location = new System.Drawing.Point(242, 22);
            this.txtVehicleCode.Name = "txtVehicleCode";
            this.txtVehicleCode.Size = new System.Drawing.Size(316, 24);
            this.txtVehicleCode.TabIndex = 2;
            // 
            // txtvehicleCertificate
            // 
            this.txtvehicleCertificate.BackColor = System.Drawing.Color.White;
            this.txtvehicleCertificate.Font = new System.Drawing.Font("Arial", 11F);
            this.txtvehicleCertificate.Location = new System.Drawing.Point(136, 52);
            this.txtvehicleCertificate.Name = "txtvehicleCertificate";
            this.txtvehicleCertificate.Size = new System.Drawing.Size(422, 24);
            this.txtvehicleCertificate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sổ đăng ký:";
            // 
            // cbbvehicleType
            // 
            this.cbbvehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbvehicleType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbvehicleType.Font = new System.Drawing.Font("Arial", 11F);
            this.cbbvehicleType.FormattingEnabled = true;
            this.cbbvehicleType.Items.AddRange(new object[] {
            "Loại phương tiện",
            "Xe tải",
            "Xe đầu kéo",
            "Tàu thủy",
            "Xà lan",
            "Tàu hỏa"});
            this.cbbvehicleType.Location = new System.Drawing.Point(136, 82);
            this.cbbvehicleType.Name = "cbbvehicleType";
            this.cbbvehicleType.Size = new System.Drawing.Size(422, 25);
            this.cbbvehicleType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Loại phương tiện:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "PT vận chuyển:";
            // 
            // cbbTransportMethodId
            // 
            this.cbbTransportMethodId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTransportMethodId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbTransportMethodId.Font = new System.Drawing.Font("Arial", 11F);
            this.cbbTransportMethodId.FormattingEnabled = true;
            this.cbbTransportMethodId.Items.AddRange(new object[] {
            "Phương thức vận chuyển",
            "Đường bộ",
            "Đường thủy",
            "Đường sắt"});
            this.cbbTransportMethodId.Location = new System.Drawing.Point(136, 113);
            this.cbbTransportMethodId.Name = "cbbTransportMethodId";
            this.cbbTransportMethodId.Size = new System.Drawing.Size(422, 25);
            this.cbbTransportMethodId.TabIndex = 7;
            // 
            // dtpRegistrationDeadline
            // 
            this.dtpRegistrationDeadline.CustomFormat = "dd/MM/yyyy";
            this.dtpRegistrationDeadline.Font = new System.Drawing.Font("Arial", 11F);
            this.dtpRegistrationDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRegistrationDeadline.Location = new System.Drawing.Point(136, 144);
            this.dtpRegistrationDeadline.Name = "dtpRegistrationDeadline";
            this.dtpRegistrationDeadline.Size = new System.Drawing.Size(100, 24);
            this.dtpRegistrationDeadline.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hạn đăng kiểm:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tải trọng:";
            // 
            // txtWeightLimit
            // 
            this.txtWeightLimit.BackColor = System.Drawing.Color.White;
            this.txtWeightLimit.Font = new System.Drawing.Font("Arial", 11F);
            this.txtWeightLimit.Location = new System.Drawing.Point(136, 174);
            this.txtWeightLimit.Name = "txtWeightLimit";
            this.txtWeightLimit.Size = new System.Drawing.Size(100, 24);
            this.txtWeightLimit.TabIndex = 11;
            this.txtWeightLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(136, 219);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Ghi nhận";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(242, 219);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmVehicleAllEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtWeightLimit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpRegistrationDeadline);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbTransportMethodId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbvehicleType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtvehicleCertificate);
            this.Controls.Add(this.txtVehicleCode);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "frmVehicleAllEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật phương tiện";
            this.Load += new System.EventHandler(this.frmVehicleAllEdit_Load);
            this.Shown += new System.EventHandler(this.frmVehicleAllEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVehicleAllEdit_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.TextBox txtId;
        public System.Windows.Forms.TextBox txtVehicleCode;
        public System.Windows.Forms.TextBox txtvehicleCertificate;
        public System.Windows.Forms.ComboBox cbbvehicleType;
        public System.Windows.Forms.ComboBox cbbTransportMethodId;
        public System.Windows.Forms.DateTimePicker dtpRegistrationDeadline;
        public System.Windows.Forms.TextBox txtWeightLimit;
    }
}