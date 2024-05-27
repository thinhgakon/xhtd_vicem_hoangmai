using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XHTD_WEB.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportShift(int Kip = 1)
        {
            var reportShifts = new ReportShift();
            var day = DateTime.Now.Date;
            var startDay = day;
            var startKip = day;
            var endKip = day;
            switch (Kip)
            {
                case 1: //6h -14h
                     startDay = startDay.AddHours(6);
                     startKip = startKip.AddHours(6);
                     endKip = endKip.AddHours(14);
                    break;
                case 2: //14h - 22h
                    startDay = startDay.AddHours(6);
                    startKip = startKip.AddHours(14);
                    endKip = endKip.AddHours(22);
                    break;
                case 3:  //22h - 6h
                    startDay = startDay.AddDays(-1).AddHours(6);
                    startKip = startKip.AddDays(-1).AddHours(22);
                    endKip = endKip.AddHours(6);
                    break;
                default:
                    break;
            }
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = $@"exec uspOperatingGetReportByShift @startDay, @startKip, @endKip";
                var result = db.Database.SqlQuery<ReportShift>(sqlSelect, new SqlParameter("@startDay", startDay), new SqlParameter("@startKip", startKip), new SqlParameter("@endKip", endKip)).ToList();
                ViewBag.ReportShifts = result.FirstOrDefault();
            }
            ViewBag.Kip = Kip;
            
            return View();
        }
    }
    public class ReportShift
    {
        public double? TotalShift { get; set; }
        public double? Total { get; set; }
        public double? Maxpro { get; set; }
        public double? PCB30 { get; set; }
        public double? PCB30_BS { get; set; }
        public double? PCB40 { get; set; }
        public double? PCB40_BS { get; set; }
        public double? Block_XM { get; set; }
        public double? C595 { get; set; }
        public double? C150 { get; set; }
        public double? R_C150 { get; set; }
        public double? Jumbo { get; set; }
        public double? R_CN { get; set; }
        public double? R_DD { get; set; }
        public double? R_I { get; set; }
        public double? R_SULFAT { get; set; }
        public double? R_II { get; set; }
        public double? Clinker { get; set; }
        public double? Tontrong { get; set; }
        public double? Tonngoai { get; set; }
        public double? TonDS { get; set; }
        public double? Tonxiroi { get; set; }
    }
}