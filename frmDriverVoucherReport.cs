﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmDriverVoucherReport : Form
    {
        public DataTable objTableData = new DataTable();
        public string Pr_Time = "", Pr_Ca = "", Pr_State = "";
        public frmDriverVoucherReport()
        {
            InitializeComponent();
        }

        private void frmDriverVoucherReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmDriverVoucherReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.ProcessingMode = ProcessingMode.Local;

                //this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "/HTS_Task/frmDriverVoucherReportPrint.rdlc";
                //this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "../../../frmDriverVoucherReportPrint.rdlc";

                this.reportViewer1.LocalReport.ReportEmbeddedResource = "HMXHTD.frmDriverVoucherReportPrint.rdlc";


                Microsoft.Reporting.WinForms.ReportParameter[] rParmas = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("Pr_Time", Pr_Time+" "),
                    new Microsoft.Reporting.WinForms.ReportParameter("Pr_State", Pr_State+" ")
                };
                this.reportViewer1.LocalReport.SetParameters(rParmas);

                ReportDataSource datasource = new ReportDataSource();

                datasource.Name = "Voucher";
                datasource.Value = this.objTableData;
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(datasource);

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.FullPage;
                this.reportViewer1.ZoomPercent = 100;
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message} {Ex.StackTrace}");
            }
        }
    }
}
