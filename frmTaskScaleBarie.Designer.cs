
namespace HMXHTD
{
    partial class frmTaskScaleBarie
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cbbSubject = new System.Windows.Forms.ComboBox();
            this.btnBarieTop = new System.Windows.Forms.Button();
            this.btnBarieBottom = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.cbbSubject);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 166);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin mở Barie";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(12, 54);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(348, 100);
            this.txtNote.TabIndex = 53;
            // 
            // cbbSubject
            // 
            this.cbbSubject.AllowDrop = true;
            this.cbbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbSubject.BackColor = System.Drawing.Color.White;
            this.cbbSubject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbSubject.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSubject.ForeColor = System.Drawing.Color.Black;
            this.cbbSubject.FormattingEnabled = true;
            this.cbbSubject.Items.AddRange(new object[] {
            "Chọn lý do mở Barie",
            "- Mở để cân hàng",
            "- Mở xử lý sự cố",
            "- Mở vì nguyên nhân khác"});
            this.cbbSubject.Location = new System.Drawing.Point(12, 23);
            this.cbbSubject.Name = "cbbSubject";
            this.cbbSubject.Size = new System.Drawing.Size(348, 25);
            this.cbbSubject.TabIndex = 52;
            this.cbbSubject.SelectedIndexChanged += new System.EventHandler(this.cbbSubject_SelectedIndexChanged);
            // 
            // btnBarieTop
            // 
            this.btnBarieTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnBarieTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBarieTop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnBarieTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBarieTop.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarieTop.ForeColor = System.Drawing.Color.White;
            this.btnBarieTop.Location = new System.Drawing.Point(6, 187);
            this.btnBarieTop.Name = "btnBarieTop";
            this.btnBarieTop.Size = new System.Drawing.Size(182, 51);
            this.btnBarieTop.TabIndex = 1;
            this.btnBarieTop.Text = "ĐÓNG, MỞ BARIE CÂN NỔI";
            this.btnBarieTop.UseVisualStyleBackColor = false;
            this.btnBarieTop.Click += new System.EventHandler(this.btnBarieTop_Click);
            // 
            // btnBarieBottom
            // 
            this.btnBarieBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnBarieBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBarieBottom.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
            this.btnBarieBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBarieBottom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarieBottom.ForeColor = System.Drawing.Color.White;
            this.btnBarieBottom.Location = new System.Drawing.Point(196, 187);
            this.btnBarieBottom.Name = "btnBarieBottom";
            this.btnBarieBottom.Size = new System.Drawing.Size(182, 51);
            this.btnBarieBottom.TabIndex = 2;
            this.btnBarieBottom.Text = "ĐÓNG, MỞ BARIE CÂN CHÌM";
            this.btnBarieBottom.UseVisualStyleBackColor = false;
            this.btnBarieBottom.Click += new System.EventHandler(this.btnBarieBottom_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(6, 264);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(372, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Thoát";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmTaskScaleBarie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnBarieBottom);
            this.Controls.Add(this.btnBarieTop);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(400, 350);
            this.MaximumSize = new System.Drawing.Size(400, 350);
            this.Name = "frmTaskScaleBarie";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mở barie";
            this.Shown += new System.EventHandler(this.frmTaskScaleBarie_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTaskScaleBarie_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbbSubject;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnBarieTop;
        private System.Windows.Forms.Button btnBarieBottom;
        private System.Windows.Forms.Button btnClose;
    }
}