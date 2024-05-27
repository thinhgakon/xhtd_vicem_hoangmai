namespace HMXHTD
{
    partial class frmControlRFID1
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
            this.grDevice = new System.Windows.Forms.GroupBox();
            this.lblDeviceState = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPortNumber = new System.Windows.Forms.TextBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.lsvrtlog = new System.Windows.Forms.ListView();
            this.coltime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colcardno = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.coldoorid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colevtype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInOutState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colvermode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btncls = new System.Windows.Forms.Button();
            this.lblRTEInfo = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.dgvDataTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataPin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataDoorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataInOutState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataVerifyMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDataNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.grDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // grDevice
            // 
            this.grDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grDevice.Controls.Add(this.lblDeviceState);
            this.grDevice.Controls.Add(this.btnConnect);
            this.grDevice.Controls.Add(this.btnSetting);
            this.grDevice.Controls.Add(this.label3);
            this.grDevice.Controls.Add(this.label2);
            this.grDevice.Controls.Add(this.label1);
            this.grDevice.Controls.Add(this.txtName);
            this.grDevice.Controls.Add(this.txtPortNumber);
            this.grDevice.Controls.Add(this.txtIpAddress);
            this.grDevice.Location = new System.Drawing.Point(6, 0);
            this.grDevice.Name = "grDevice";
            this.grDevice.Size = new System.Drawing.Size(888, 80);
            this.grDevice.TabIndex = 0;
            this.grDevice.TabStop = false;
            this.grDevice.Text = "Thông tin thiết bị";
            // 
            // lblDeviceState
            // 
            this.lblDeviceState.Location = new System.Drawing.Point(102, 51);
            this.lblDeviceState.Name = "lblDeviceState";
            this.lblDeviceState.Size = new System.Drawing.Size(778, 20);
            this.lblDeviceState.TabIndex = 1;
            this.lblDeviceState.Text = "Chưa kết nối";
            this.lblDeviceState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(812, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(68, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Kết nối";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(733, 19);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 6;
            this.btnSetting.Text = "Thiết lập";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(629, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cổng:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Địa chỉ IP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tên thiết bị:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(104, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(298, 21);
            this.txtName.TabIndex = 2;
            this.txtName.Text = "Đầu đọc RFID điểm xác thực";
            // 
            // txtPortNumber
            // 
            this.txtPortNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPortNumber.BackColor = System.Drawing.Color.White;
            this.txtPortNumber.Location = new System.Drawing.Point(672, 20);
            this.txtPortNumber.Name = "txtPortNumber";
            this.txtPortNumber.Size = new System.Drawing.Size(60, 21);
            this.txtPortNumber.TabIndex = 1;
            this.txtPortNumber.Text = "4370";
            this.txtPortNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIpAddress.BackColor = System.Drawing.Color.White;
            this.txtIpAddress.Location = new System.Drawing.Point(474, 20);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(150, 21);
            this.txtIpAddress.TabIndex = 0;
            this.txtIpAddress.Text = "192.168.1.201";
            // 
            // lsvrtlog
            // 
            this.lsvrtlog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.coltime,
            this.colpin,
            this.colcardno,
            this.coldoorid,
            this.colevtype,
            this.colInOutState,
            this.colvermode});
            this.lsvrtlog.GridLines = true;
            this.lsvrtlog.Location = new System.Drawing.Point(319, 195);
            this.lsvrtlog.Name = "lsvrtlog";
            this.lsvrtlog.Size = new System.Drawing.Size(373, 77);
            this.lsvrtlog.TabIndex = 2;
            this.lsvrtlog.UseCompatibleStateImageBehavior = false;
            this.lsvrtlog.View = System.Windows.Forms.View.Details;
            // 
            // coltime
            // 
            this.coltime.Text = "Time";
            this.coltime.Width = 126;
            // 
            // colpin
            // 
            this.colpin.Text = "Pin";
            this.colpin.Width = 86;
            // 
            // colcardno
            // 
            this.colcardno.Text = "CardNo";
            this.colcardno.Width = 66;
            // 
            // coldoorid
            // 
            this.coldoorid.Text = "DoorID";
            this.coldoorid.Width = 57;
            // 
            // colevtype
            // 
            this.colevtype.Text = "EventType";
            this.colevtype.Width = 80;
            // 
            // colInOutState
            // 
            this.colInOutState.Text = "InOutState";
            this.colInOutState.Width = 98;
            // 
            // colvermode
            // 
            this.colvermode.Text = "VerifyMode";
            this.colvermode.Width = 152;
            // 
            // btncls
            // 
            this.btncls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btncls.Enabled = false;
            this.btncls.Location = new System.Drawing.Point(58, 570);
            this.btncls.Name = "btncls";
            this.btncls.Size = new System.Drawing.Size(50, 23);
            this.btncls.TabIndex = 7;
            this.btncls.Text = "Xóa";
            this.btncls.UseVisualStyleBackColor = true;
            this.btncls.Click += new System.EventHandler(this.btncls_Click);
            // 
            // lblRTEInfo
            // 
            this.lblRTEInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRTEInfo.Location = new System.Drawing.Point(219, 571);
            this.lblRTEInfo.Name = "lblRTEInfo";
            this.lblRTEInfo.Size = new System.Drawing.Size(675, 20);
            this.lblRTEInfo.TabIndex = 8;
            this.lblRTEInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(110, 570);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 23);
            this.btnStop.TabIndex = 34;
            this.btnStop.Text = "Dừng";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(6, 570);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(50, 23);
            this.btnStart.TabIndex = 33;
            this.btnStart.Text = "Bắt đầu";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvData.ColumnHeadersHeight = 26;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDataTime,
            this.dgvDataPin,
            this.dgvDataCardNo,
            this.dgvDataDoorID,
            this.dgvDataEventType,
            this.dgvDataInOutState,
            this.dgvDataVerifyMode,
            this.dgvDataNote});
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.Location = new System.Drawing.Point(6, 86);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvData.RowHeadersWidth = 30;
            this.dgvData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvData.ShowCellErrors = false;
            this.dgvData.ShowCellToolTips = false;
            this.dgvData.ShowEditingIcon = false;
            this.dgvData.ShowRowErrors = false;
            this.dgvData.Size = new System.Drawing.Size(888, 478);
            this.dgvData.TabIndex = 35;
            // 
            // dgvDataTime
            // 
            this.dgvDataTime.DataPropertyName = "Time";
            this.dgvDataTime.HeaderText = "Thời gian xác nhận";
            this.dgvDataTime.MinimumWidth = 120;
            this.dgvDataTime.Name = "dgvDataTime";
            this.dgvDataTime.Width = 120;
            // 
            // dgvDataPin
            // 
            this.dgvDataPin.DataPropertyName = "Pin";
            this.dgvDataPin.HeaderText = "Mã số pin";
            this.dgvDataPin.Name = "dgvDataPin";
            this.dgvDataPin.Visible = false;
            // 
            // dgvDataCardNo
            // 
            this.dgvDataCardNo.DataPropertyName = "CardNo";
            this.dgvDataCardNo.HeaderText = "     Mã số thẻ";
            this.dgvDataCardNo.Name = "dgvDataCardNo";
            // 
            // dgvDataDoorID
            // 
            this.dgvDataDoorID.DataPropertyName = "EventType";
            this.dgvDataDoorID.HeaderText = "Cổng vào";
            this.dgvDataDoorID.Name = "dgvDataDoorID";
            this.dgvDataDoorID.Visible = false;
            // 
            // dgvDataEventType
            // 
            this.dgvDataEventType.HeaderText = "Kiển sự kiện";
            this.dgvDataEventType.Name = "dgvDataEventType";
            this.dgvDataEventType.Visible = false;
            // 
            // dgvDataInOutState
            // 
            this.dgvDataInOutState.DataPropertyName = "InOutState";
            this.dgvDataInOutState.HeaderText = "Trạng thái vào/ra";
            this.dgvDataInOutState.Name = "dgvDataInOutState";
            this.dgvDataInOutState.Visible = false;
            // 
            // dgvDataVerifyMode
            // 
            this.dgvDataVerifyMode.DataPropertyName = "VerifyMode";
            this.dgvDataVerifyMode.HeaderText = "Kiểu xác thực";
            this.dgvDataVerifyMode.Name = "dgvDataVerifyMode";
            this.dgvDataVerifyMode.Visible = false;
            // 
            // dgvDataNote
            // 
            this.dgvDataNote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvDataNote.DataPropertyName = "Note";
            this.dgvDataNote.HeaderText = "Ghi chú";
            this.dgvDataNote.Name = "dgvDataNote";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(163, 570);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 23);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmControlRFID1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblRTEInfo);
            this.Controls.Add(this.btncls);
            this.Controls.Add(this.lsvrtlog);
            this.Controls.Add(this.grDevice);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmControlRFID1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmControlRFID1";
            this.grDevice.ResumeLayout(false);
            this.grDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPortNumber;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Label lblDeviceState;
        private System.Windows.Forms.ListView lsvrtlog;
        private System.Windows.Forms.ColumnHeader coltime;
        private System.Windows.Forms.ColumnHeader colpin;
        private System.Windows.Forms.ColumnHeader colcardno;
        private System.Windows.Forms.ColumnHeader coldoorid;
        private System.Windows.Forms.ColumnHeader colevtype;
        private System.Windows.Forms.ColumnHeader colInOutState;
        private System.Windows.Forms.ColumnHeader colvermode;
        private System.Windows.Forms.Button btncls;
        private System.Windows.Forms.Label lblRTEInfo;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataPin;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataDoorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataInOutState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataVerifyMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDataNote;
        private System.Windows.Forms.Button btnClose;
    }
}