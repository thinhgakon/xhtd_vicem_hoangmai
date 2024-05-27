namespace HMXHTD
{
    partial class frmVehicleCategory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvVehicle = new System.Windows.Forms.DataGridView();
            this.dgvVehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleTonnage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVehicleTonnageDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTonnageDefault = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTrongTai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGPLX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.txtW = new System.Windows.Forms.TextBox();
            this.txtH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaiXe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPhuongTien = new System.Windows.Forms.TextBox();
            this.btnCardEdit = new System.Windows.Forms.Button();
            this.btnCardSave = new System.Windows.Forms.Button();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicle)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVehicle.ColumnHeadersHeight = 30;
            this.dgvVehicle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVehicle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvVehicleId,
            this.dgvVehicleCode,
            this.dgvVehicleCardNo,
            this.dgvname,
            this.IdCardNumber,
            this.dgvVehicleTonnage,
            this.dgvVehicleTonnageDefault,
            this.H,
            this.W,
            this.L});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVehicle.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvVehicle.EnableHeadersVisualStyles = false;
            this.dgvVehicle.Location = new System.Drawing.Point(7, 60);
            this.dgvVehicle.MultiSelect = false;
            this.dgvVehicle.Name = "dgvVehicle";
            this.dgvVehicle.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicle.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvVehicle.RowHeadersWidth = 36;
            this.dgvVehicle.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvVehicle.RowTemplate.Height = 26;
            this.dgvVehicle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicle.Size = new System.Drawing.Size(887, 374);
            this.dgvVehicle.TabIndex = 2;
            this.dgvVehicle.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVehicle_CellEnter);
            // 
            // dgvVehicleId
            // 
            this.dgvVehicleId.DataPropertyName = "IDVehicle";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvVehicleId.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVehicleId.HeaderText = "Id";
            this.dgvVehicleId.Name = "dgvVehicleId";
            this.dgvVehicleId.ReadOnly = true;
            this.dgvVehicleId.Visible = false;
            this.dgvVehicleId.Width = 55;
            // 
            // dgvVehicleCode
            // 
            this.dgvVehicleCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvVehicleCode.DataPropertyName = "Vehicle";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVehicleCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVehicleCode.HeaderText = "Phương tiện";
            this.dgvVehicleCode.Name = "dgvVehicleCode";
            this.dgvVehicleCode.ReadOnly = true;
            this.dgvVehicleCode.Width = 150;
            // 
            // dgvVehicleCardNo
            // 
            this.dgvVehicleCardNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvVehicleCardNo.DataPropertyName = "CardNo";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVehicleCardNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVehicleCardNo.HeaderText = "   Mã RFID";
            this.dgvVehicleCardNo.MinimumWidth = 60;
            this.dgvVehicleCardNo.Name = "dgvVehicleCardNo";
            // 
            // dgvname
            // 
            this.dgvname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvname.DataPropertyName = "NameDriver";
            this.dgvname.HeaderText = "Tên tài xế";
            this.dgvname.Name = "dgvname";
            // 
            // IdCardNumber
            // 
            this.IdCardNumber.DataPropertyName = "IdCardNumber";
            this.IdCardNumber.HeaderText = "Giấy phép lái xe";
            this.IdCardNumber.Name = "IdCardNumber";
            this.IdCardNumber.Width = 200;
            // 
            // dgvVehicleTonnage
            // 
            this.dgvVehicleTonnage.DataPropertyName = "Tonnage";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvVehicleTonnage.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvVehicleTonnage.HeaderText = "Trọng tải";
            this.dgvVehicleTonnage.Name = "dgvVehicleTonnage";
            this.dgvVehicleTonnage.Width = 60;
            // 
            // dgvVehicleTonnageDefault
            // 
            this.dgvVehicleTonnageDefault.DataPropertyName = "TonnageDefault";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N1";
            dataGridViewCellStyle6.NullValue = null;
            this.dgvVehicleTonnageDefault.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvVehicleTonnageDefault.HeaderText = "  TL Bì";
            this.dgvVehicleTonnageDefault.Name = "dgvVehicleTonnageDefault";
            this.dgvVehicleTonnageDefault.Width = 60;
            // 
            // H
            // 
            this.H.DataPropertyName = "HeightVehicle";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.H.DefaultCellStyle = dataGridViewCellStyle7;
            this.H.HeaderText = "     Cao";
            this.H.Name = "H";
            this.H.Width = 60;
            // 
            // W
            // 
            this.W.DataPropertyName = "WidthVehicle";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.W.DefaultCellStyle = dataGridViewCellStyle8;
            this.W.HeaderText = "    Rộng";
            this.W.Name = "W";
            this.W.Width = 60;
            // 
            // L
            // 
            this.L.DataPropertyName = "LongVehicle";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.L.DefaultCellStyle = dataGridViewCellStyle9;
            this.L.HeaderText = "    Dài";
            this.L.Name = "L";
            this.L.Width = 60;
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchKey.Font = new System.Drawing.Font("Arial", 11.25F);
            this.txtSearchKey.Location = new System.Drawing.Point(261, 17);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(610, 25);
            this.txtSearchKey.TabIndex = 1;
            this.txtSearchKey.TextChanged += new System.EventHandler(this.txtSearchKey_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSearchKey);
            this.groupBox2.Location = new System.Drawing.Point(7, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(887, 54);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tìm kiếm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 15);
            this.label5.TabIndex = 40;
            this.label5.Text = "Tìm theo biển số xe hoặc mã RFID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTonnageDefault);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTrongTai);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGPLX);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtL);
            this.groupBox1.Controls.Add(this.txtW);
            this.groupBox1.Controls.Add(this.txtH);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTaiXe);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPhuongTien);
            this.groupBox1.Controls.Add(this.btnCardEdit);
            this.groupBox1.Controls.Add(this.btnCardSave);
            this.groupBox1.Controls.Add(this.txtCardNo);
            this.groupBox1.Location = new System.Drawing.Point(6, 440);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(888, 120);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phương tiện";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(221, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 15);
            this.label10.TabIndex = 57;
            this.label10.Text = "RFID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 55;
            this.label9.Text = "Cân bì:";
            // 
            // txtTonnageDefault
            // 
            this.txtTonnageDefault.BackColor = System.Drawing.Color.White;
            this.txtTonnageDefault.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTonnageDefault.Location = new System.Drawing.Point(90, 84);
            this.txtTonnageDefault.Name = "txtTonnageDefault";
            this.txtTonnageDefault.ReadOnly = true;
            this.txtTonnageDefault.Size = new System.Drawing.Size(100, 25);
            this.txtTonnageDefault.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 53;
            this.label4.Text = "Trọng tải:";
            // 
            // txtTrongTai
            // 
            this.txtTrongTai.BackColor = System.Drawing.Color.White;
            this.txtTrongTai.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrongTai.Location = new System.Drawing.Point(90, 53);
            this.txtTrongTai.Name = "txtTrongTai";
            this.txtTrongTai.Size = new System.Drawing.Size(100, 25);
            this.txtTrongTai.TabIndex = 6;
            this.txtTrongTai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(629, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 51;
            this.label2.Text = "GPLX:";
            // 
            // txtGPLX
            // 
            this.txtGPLX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGPLX.BackColor = System.Drawing.Color.White;
            this.txtGPLX.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGPLX.Location = new System.Drawing.Point(675, 22);
            this.txtGPLX.Name = "txtGPLX";
            this.txtGPLX.Size = new System.Drawing.Size(201, 25);
            this.txtGPLX.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(482, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 49;
            this.label6.Text = "Chiều dài:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "Chiều rộng:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(193, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 47;
            this.label8.Text = "Chiều cao:";
            // 
            // txtL
            // 
            this.txtL.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtL.Location = new System.Drawing.Point(548, 53);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(70, 25);
            this.txtL.TabIndex = 9;
            this.txtL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtW
            // 
            this.txtW.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtW.Location = new System.Drawing.Point(409, 53);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(70, 25);
            this.txtW.TabIndex = 8;
            this.txtW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtH
            // 
            this.txtH.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtH.Location = new System.Drawing.Point(262, 53);
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(70, 25);
            this.txtH.TabIndex = 7;
            this.txtH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên tài xế:";
            // 
            // txtTaiXe
            // 
            this.txtTaiXe.BackColor = System.Drawing.Color.White;
            this.txtTaiXe.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaiXe.Location = new System.Drawing.Point(262, 22);
            this.txtTaiXe.Name = "txtTaiXe";
            this.txtTaiXe.Size = new System.Drawing.Size(356, 25);
            this.txtTaiXe.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Phương tiện:";
            // 
            // txtPhuongTien
            // 
            this.txtPhuongTien.BackColor = System.Drawing.Color.White;
            this.txtPhuongTien.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongTien.Location = new System.Drawing.Point(90, 22);
            this.txtPhuongTien.Name = "txtPhuongTien";
            this.txtPhuongTien.Size = new System.Drawing.Size(100, 25);
            this.txtPhuongTien.TabIndex = 3;
            // 
            // btnCardEdit
            // 
            this.btnCardEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCardEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardEdit.Location = new System.Drawing.Point(548, 85);
            this.btnCardEdit.Name = "btnCardEdit";
            this.btnCardEdit.Size = new System.Drawing.Size(69, 23);
            this.btnCardEdit.TabIndex = 42;
            this.btnCardEdit.Text = "Sửa";
            this.btnCardEdit.UseVisualStyleBackColor = true;
            this.btnCardEdit.Click += new System.EventHandler(this.btnCardEdit_Click);
            // 
            // btnCardSave
            // 
            this.btnCardSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCardSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardSave.Location = new System.Drawing.Point(548, 85);
            this.btnCardSave.Name = "btnCardSave";
            this.btnCardSave.Size = new System.Drawing.Size(69, 23);
            this.btnCardSave.TabIndex = 58;
            this.btnCardSave.Text = "Lưu";
            this.btnCardSave.UseVisualStyleBackColor = true;
            this.btnCardSave.Visible = false;
            this.btnCardSave.Click += new System.EventHandler(this.btnCardSave_Click);
            // 
            // txtCardNo
            // 
            this.txtCardNo.BackColor = System.Drawing.Color.White;
            this.txtCardNo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNo.Location = new System.Drawing.Point(262, 84);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.ReadOnly = true;
            this.txtCardNo.Size = new System.Drawing.Size(356, 25);
            this.txtCardNo.TabIndex = 56;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Location = new System.Drawing.Point(77, 565);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 29);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Cập nhật";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(153, 565);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Ghi nhận";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.btnClose.Location = new System.Drawing.Point(824, 565);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 29);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(6, 565);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 29);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(229, 565);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 29);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.Enabled = false;
            this.btnDel.Location = new System.Drawing.Point(300, 565);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 29);
            this.btnDel.TabIndex = 14;
            this.btnDel.Text = "Xóa";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // frmVehicleCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvVehicle);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVehicleCategory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phương tiện";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVehicle_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVehicleCategory_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicle)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVehicle;
        private System.Windows.Forms.TextBox txtSearchKey;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.TextBox txtH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaiXe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPhuongTien;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGPLX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTrongTai;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTonnageDefault;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Button btnCardEdit;
        private System.Windows.Forms.Button btnCardSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvname;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleTonnage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVehicleTonnageDefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn W;
        private System.Windows.Forms.DataGridViewTextBoxColumn L;
    }
}