using HMXHTD.Core;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace HMXHTD
{
    public partial class frmTaskRelease2 : Form
    {
        #region declare objects
        private IHubProxy HubProxy { get; set; }
        private HubConnection Connection { get; set; }
        private Thread threadArea;
        private Thread threadVoice;
        private Thread threadVoice1;
        delegate void SetTextCallback(string text);
        int TimeSleep = 60000, TimeSleepVoice1 = 7000, TimeSleepVoice2 = 8000, TimeSleepVoice3 = 8000, OrderId = 0, Trough = 0, StepId = -1, IndexOrder = 0;
        string deliveryCode = "";
        string kcsCode = ""; int idKcs = 0;
        BillOrder objBillOrder = new BillOrder();
        FileKCS objKcs = new FileKCS();
        Trough objTrough = new Trough();
        DataTable objTableTrough = new DataTable();
        private DataTable BillOrderRelease = new DataTable();
        #endregion

        #region method frmTaskRelease2
        public frmTaskRelease2()
        {
            InitializeComponent();
        }
        #endregion

        #region method SetTextArea
        private void SetTextArea(string text)
        {
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextArea);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }
        #endregion

        #region method btnStart_Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            threadArea = new Thread(t =>
            {
                while (1 > 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        

                        //this.getData();

                        //DataTable objTable1 = this.objBillOrder.getBillOrderReleaseTrough(1);
                        //if (objTable1.Rows.Count > 0)
                        //{
                        //    this.lblVehicle1.Text = objTable1.Rows[0]["Vehicle"].ToString().ToUpper();
                        //    this.btn1.Text = "ĐANG LẤY HÀNG";
                        //    this.btn1.Enabled = true;
                        //    this.btn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
                        //    this.btn1.ForeColor = Color.White;
                        //}
                        //else
                        //{
                        //    this.lblVehicle1.Text = "-:-";
                        //    this.btn1.Text = "ĐANG CHỜ XE";
                        //    this.btn1.Enabled = true;
                        //    this.btn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        //    this.btn1.ForeColor = Color.White;
                        //}

                        //DataTable objTable2 = this.objBillOrder.getBillOrderReleaseTrough(2);
                        //if (objTable2.Rows.Count > 0)
                        //{
                        //    this.lblVehicle2.Text = objTable2.Rows[0]["Vehicle"].ToString().ToUpper();
                        //    this.btn2.Text = "ĐANG LẤY HÀNG";
                        //    this.btn2.Enabled = true;
                        //    this.btn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
                        //    this.btn2.ForeColor = Color.White;
                        //}
                        //else
                        //{
                        //    this.lblVehicle2.Text = "-:-";
                        //    this.btn2.Text = "ĐANG CHỜ XE";
                        //    this.btn2.Enabled = true;
                        //    this.btn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        //    this.btn2.ForeColor = Color.White;
                        //}

                        //DataTable objTable3 = this.objBillOrder.getBillOrderReleaseTrough(3);
                        //if (objTable3.Rows.Count > 0)
                        //{
                        //    this.lblVehicle3.Text = objTable3.Rows[0]["Vehicle"].ToString().ToUpper();
                        //    this.btn3.Text = "ĐANG LẤY HÀNG";
                        //    this.btn3.Enabled = true;
                        //    this.btn3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
                        //    this.btn3.ForeColor = Color.White;
                        //}
                        //else
                        //{
                        //    this.lblVehicle3.Text = "-:-";
                        //    this.btn3.Text = "ĐANG CHỜ XE";
                        //    this.btn3.Enabled = true;
                        //    this.btn3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        //    this.btn3.ForeColor = Color.White;
                        //}

                        //DataTable objTable4 = this.objBillOrder.getBillOrderReleaseTrough(4);
                        //if (objTable4.Rows.Count > 0)
                        //{
                        //    this.lblVehicle4.Text = objTable4.Rows[0]["Vehicle"].ToString().ToUpper();
                        //    this.btn4.Text = "ĐANG LẤY HÀNG";
                        //    this.btn4.Enabled = true;
                        //    this.btn4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
                        //    this.btn4.ForeColor = Color.White;
                        //}
                        //else
                        //{
                        //    this.lblVehicle4.Text = "-:-";
                        //    this.btn4.Text = "ĐANG CHỜ XE";
                        //    this.btn4.Enabled = true;
                        //    this.btn4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        //    this.btn4.ForeColor = Color.White;
                        //}

                        //DataTable objTable5 = this.objBillOrder.getBillOrderReleaseTrough(5);
                        //if (objTable5.Rows.Count > 0)
                        //{
                        //    this.lblVehicle5.Text = objTable5.Rows[0]["Vehicle"].ToString().ToUpper();
                        //    this.btn5.Text = "ĐANG LẤY HÀNG";
                        //    this.btn5.Enabled = true;
                        //    this.btn5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
                        //    this.btn5.ForeColor = Color.White;
                        //}
                        //else
                        //{
                        //    this.lblVehicle5.Text = "-:-";
                        //    this.btn5.Text = "ĐANG CHỜ XE";
                        //    this.btn5.Enabled = true;
                        //    this.btn5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        //    this.btn5.ForeColor = Color.White;
                        //}

                        //DataTable objTable6 = this.objBillOrder.getBillOrderReleaseTrough(6);
                        //if (objTable6.Rows.Count > 0)
                        //{
                        //    this.lblVehicle6.Text = objTable6.Rows[0]["Vehicle"].ToString().ToUpper();
                        //    this.btn6.Text = "ĐANG LẤY HÀNG";
                        //    this.btn6.Enabled = true;
                        //    this.btn6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
                        //    this.btn6.ForeColor = Color.White;
                        //}
                        //else
                        //{
                        //    this.lblVehicle6.Text = "-:-";
                        //    this.btn6.Text = "ĐANG CHỜ XE";
                        //    this.btn6.Enabled = true;
                        //    this.btn6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                        //    this.btn6.ForeColor = Color.White;
                        //}

                        this.getDataFull();

                    });

                    this.SetTextArea(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ", " + this.dgvListBillOrder.RowCount.ToString() + " đơn hàng");
                    Thread.Sleep(this.TimeSleep);
                }
            }) { IsBackground = true };
            threadArea.Start();
        }
        #endregion

        #region method btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.threadArea.Abort();
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
        #endregion

        #region method frmTaskRelease2_Load
        private void frmTaskRelease2_Load(object sender, EventArgs e)
        {
            this.dtpFromDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-1);
            this.btnStart.PerformClick();
            //this.CallVehicle();
        }
        #endregion

        #region method dgvListBillOrder_CellClick
        private void dgvListBillOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.OrderId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderId"].Value.ToString());
            this.deliveryCode = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIDOrderSyn"].Value.ToString();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvListBillOrder.AutoGenerateColumns = false;
            this.dgvListBillOrder.EnableHeadersVisualStyles = false;
            this.BillOrderRelease = this.objBillOrder.getBillOrderReleaseROI();
            this.dgvListBillOrder.DataSource = this.BillOrderRelease;
        }
        #endregion

        #region method getDataFull
        private void getDataFull()
        {
            this.getData();
        }
        #endregion

        #region method frmTaskRelease2_KeyDown
        private void frmTaskRelease2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTaskRelease2_FormClosing
        private void frmTaskRelease2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.threadArea != null)
                {
                    if (this.threadArea.IsAlive)
                    {
                        this.threadArea.Abort();
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadVoice != null)
                {
                    if (this.threadVoice.IsAlive)
                    {
                        this.threadVoice.Abort();
                    }
                }
            }
            catch
            {

            }
            try
            {
                if (this.threadVoice1 != null)
                {
                    if (this.threadVoice1.IsAlive)
                    {
                        this.threadVoice1.Abort();
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region method btnCallDriver_Click
        private void btnCallDriver_Click(object sender, EventArgs e)
        {
            //string VoiceFile = "";
            //if (this.objBillOrder.setInOutVoice(this.OrderId, "Mời xe " + this.txtVehicle.Text.Trim().ToUpper() + "cân vào", ref VoiceFile) == 1)
            //{
            //    if (VoiceFile.Trim() != "")
            //    {
            //        this.axWindowsMediaPlayer1.URL = VoiceFile;
            //        this.axWindowsMediaPlayer1.windowlessVideo = true;
            //    }
            //}
            //this.VoiceVehicle();
            //return;

            //try
            //{
            //    if (this.txtIDOrderSyn.Text.Trim() == "")
            //    {
            //        return;
            //    }

            //    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //    String encoding = "Byte";
            //    if (encoding == "Byte")
            //    {
            //        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //    }
            //    else if (encoding == "AlphaNumeric")
            //    {
            //        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            //    }
            //    else if (encoding == "Numeric")
            //    {
            //        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            //    }
            //    try
            //    {
            //        int scale = 3;
            //        qrCodeEncoder.QRCodeScale = scale;
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //    try
            //    {
            //        int version = 7;
            //        qrCodeEncoder.QRCodeVersion = version;
            //    }
            //    catch
            //    {
            //    }

            //    string errorCorrect = "M";
            //    if (errorCorrect == "L")
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            //    else if (errorCorrect == "M")
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //    else if (errorCorrect == "Q")
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            //    else if (errorCorrect == "H")
            //        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            //    Image image;
            //    String data = txtIDOrderSyn.Text;
            //    image = qrCodeEncoder.Encode(data);
            //    picEncode.Image = image;
            //}
            //catch
            //{

            //}

            //if (this.OrderId == 0)
            //{
            //    MessageBox.Show("Bạn chưa xác định đơn hàng - phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //if (this.objBillOrder.setReleaseVoice(this.OrderId, "Mời xe " + this.txtVehicle.Text.Trim().ToUpper() + " vào lấy hàng") > 0)
            //{
            //    this.VoiceVehicle();
            //    MessageBox.Show("Gọi xe vào lấy hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.getData();
            //}
            //else
            //{
            //    MessageBox.Show("Gọi xe vào lấy hàng thất bại, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        #endregion

        #region method CallVehicle
        private void CallVehicle()
        {
            threadVoice1 = new Thread(t =>
            {
                while (1 < 2)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        int Action = 0;
                        for (int i = 0; i < this.BillOrderRelease.Rows.Count; i++)
                        {
                            if (this.BillOrderRelease.Rows[i]["Step"].ToString() == "1" || this.BillOrderRelease.Rows[i]["Step"].ToString() == "2" || this.BillOrderRelease.Rows[i]["Step"].ToString() == "3" || this.BillOrderRelease.Rows[i]["Step"].ToString() == "4")
                            {
                                if (this.objBillOrder.setReleaseVoice(this.BillOrderRelease.Rows[i]["IDOrderSyn"].ToString(), "Mời xe " + this.BillOrderRelease.Rows[i]["Vehicle"].ToString().ToUpper() + " vào lấy hàng") > 0)
                                {
                                    Action = Action + 1;
                                    //this.VoiceVehicle();
                                    //Thread.Sleep(10000);
                                    //this.getData();
                                }
                            }
                        }
                        if (Action > 0)
                        {
                            this.getData();
                        }
                    });
                    Thread.Sleep(30000);
                }
            }) { IsBackground = true };
            threadVoice1.Start();
        }
        #endregion

        #region method dgvListBillOrder_CellEnter
        private void dgvListBillOrder_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvListBillOrder_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderStep"].Value.ToString()) > 3)
            {
                this.dgvListBillOrder.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                this.dgvListBillOrder.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.deliveryCode))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để đính kèm file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Chọn file đính kèm";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html)|*.pdf; *.docx; *.xlsx; *.html";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            string path = "";
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                    }
                }
                else
                {
                    
                }
                if (!String.IsNullOrEmpty(path))
                {
                   var uploadResult =  UploadFilePdfToServer(path, this.deliveryCode);
                    if (uploadResult) {
                        this.objBillOrder.UpdatePhieuThiNgiem(this.deliveryCode, $"http://tv.ximanghoangmai.vn:8189/pdf/xiroi/phieuthinghiem_{this.deliveryCode}.pdf");
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getDataByTime();
                    }
                    else
                    {
                        MessageBox.Show("Upload file thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        public bool UploadFilePdfToServer(string pathFile, string deliveryCode)
        {
            bool result = false;
            try
            {
                var client = new RestClient($@"http://tv.ximanghoangmai.vn:8189/api/v1/AttachmentPdf/attachment-xiroi?id={this.OrderId}&deliveryCode={this.deliveryCode}");
                var request = new RestRequest(Method.POST);
                request.AddFile($"phieuthinghiem_{deliveryCode}", pathFile);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AlwaysMultipartFormData = true;

                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);
                if (jsonData["Status"] == 200) result = true;

            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public bool UploadFileKcsPdfToServer(int id, string pathFile, string codeKCS)
        {
            bool result = false;
            try
            {
                var client = new RestClient($@"http://tv.ximanghoangmai.vn:8189/api/v1/AttachmentPdf/attachment-xiroi-kcs?id={id}&codeKCS={codeKCS}");
                var request = new RestRequest(Method.POST);
                request.AddFile($"kcs-{codeKCS}", pathFile);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AlwaysMultipartFormData = true;

                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);
                if (jsonData["Status"] == 200) result = true;

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        #region method btnExportExcel_Click
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "BAOCAO-XUATHANG";
                // storing header part in Excel  
                worksheet.Cells[1, 1] = "Ngày xuất";
                worksheet.Columns[1].ColumnWidth = 16.5;
                worksheet.Cells[1, 1].Font.Size = 12;
                worksheet.Cells[1, 1].Font.Name = "Times New Roman";

                worksheet.Cells[1, 2] = "MSGH";
                worksheet.Columns[2].ColumnWidth = 10.5;
                
                worksheet.Cells[1, 3] = "Phương tiện";
                worksheet.Columns[3].ColumnWidth = 12;

                worksheet.Cells[1, 4] = "Lái xe";
                worksheet.Columns[4].ColumnWidth = 27.5;
                
                worksheet.Cells[1, 5] = "Nhà phân phối";
                worksheet.Columns[5].ColumnWidth = 65;

                worksheet.Cells[1, 6] = "Hàng hóa";
                worksheet.Columns[6].ColumnWidth = 66;

                worksheet.Cells[1, 7] = "Khối lượng";
                worksheet.Columns[7].ColumnWidth = 11;

                worksheet.Cells[1, 8] = "Vị trí xuất";
                worksheet.Columns[8].ColumnWidth = 10;

                //for (int i = 1; i < dgvListBillOrderAll.Columns.Count + 1; i++)
                //{
                //    worksheet.Cells[1, i] = dgvListBillOrderAll.Columns[i - 1].HeaderText;
                //}
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dgvListBillOrderAll.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvListBillOrderAll.Columns.Count; j++)
                    {
                        if (dgvListBillOrderAll.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dgvListBillOrderAll.Rows[i].Cells[j].Value.ToString().Trim();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = "";
                        }
                    }
                }
                // save the application  
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                app.Quit();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnSearchKCS_Click(object sender, EventArgs e)
        {
            this.getDataKCSByTime();
        }

        private void btnAddKCS_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.kcsCode))
            {
                MessageBox.Show("Vui lòng chọn lô để đính kèm file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Chọn file đính kèm";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html)|*.pdf; *.docx; *.xlsx; *.html";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            string path = "";
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                    }
                }
                else
                {

                }
                if (!String.IsNullOrEmpty(path))
                {
                    var uploadResult = UploadFileKcsPdfToServer(this.idKcs, path, this.kcsCode);
                    if (uploadResult)
                    {
                        this.objBillOrder.UpdatePhieuThiNgiemKCS(this.idKcs, $"http://tv.ximanghoangmai.vn:8189/pdf/xiroi/kcs-{this.kcsCode}.pdf");
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.getDataKCSByTime();
                    }
                    else
                    {
                        MessageBox.Show("Upload file thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvKCS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.idKcs = int.Parse(this.dgvListKCS.Rows[e.RowIndex].Cells["dgvListKCS_Id"].Value.ToString());
            this.kcsCode = this.dgvListKCS.Rows[e.RowIndex].Cells["dgvListKCS_PackageNumber"].Value.ToString();
        }

        private void btnCloseFrmKCS_Click(object sender, EventArgs e)
        {
            this.kcsCode = "";
            this.Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn file đính kèm";
            openFileDialog1.Filter = "Select Valid Document(*.xls; *.xlsx;)|*xls; *.xlsx;";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists)
                {
                    this.txtFilePath.Text = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                }
            }
            else
            {

            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            Microsoft.Office.Interop.Excel.Range range;

            string str;
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;

            string No = "";

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(@""+this.txtFilePath.Text+"", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(7);

            Microsoft.Office.Interop.Excel.Range rng = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[3, 3]);

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            No = (string)(range.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value2;

            //for (rCnt = 1; rCnt <= rw; rCnt++)
            //{
            //    for (cCnt = 1; cCnt <= cl; cCnt++)
            //    {
            //        str = (string)(range.Cells[rCnt, cCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
            //        MessageBox.Show(str);
            //    }
            //}

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }
        #endregion

        #region method btnPrint_Click
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method tabControl1_SelectedIndexChanged
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                this.getDataByTime();
            }else
                if(this.tabControl1.SelectedIndex == 2)
            {
                this.dtpKCSFromDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-1);
                this.getDataKCSByTime();
            }
        }
        #endregion

        #region method getDataByTime
        private void getDataByTime()
        {
            this.dgvListBillOrderAll.AutoGenerateColumns = false;
            this.dgvListBillOrderAll.EnableHeadersVisualStyles = false;
            this.dgvListBillOrderAll.DataSource = this.objBillOrder.getBillOrderReleaseByTimeROI(this.dtpFromDay.Value, this.dtpToDay.Value,this.txtSearch.Text);
            this.lblTotalItem.Text = " "+this.dgvListBillOrderAll.Rows.Count.ToString();
            
            decimal tmpValue = 0;
            for (int i = 0; i < this.dgvListBillOrderAll.RowCount; i++)
            {
                tmpValue += decimal.Parse(this.dgvListBillOrderAll.Rows[i].Cells["dgvListBillOrderAllSumNumber"].Value.ToString());
            }
            this.lblTotalNumber.Text = tmpValue.ToString();
        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getDataByTime();
        }
        #endregion

        #region method getDataKCSByTime
        private void getDataKCSByTime()
        {
            this.dgvListKCS.AutoGenerateColumns = false;
            this.dgvListKCS.EnableHeadersVisualStyles = false;
            this.dgvListKCS.DataSource = this.objKcs.GetTableKCS(this.dtpKCSFromDay.Value, this.dtpKCSToDay.Value, this.txtSearchKCS.Text);

        }
        #endregion

        #region method txtSearch_KeyDown
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getDataByTime();
            }
        }
        #endregion  
    }
}
