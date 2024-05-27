using ClosedXML.Excel;
using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace XHTD_WEB.Controllers
{
    public class DistributorsController : Controller
    {
        // GET: Distributors
        public ActionResult Index(int IdDistributor = 0)
        {
            var distributors = new List<tblDistributor>();
            var vehicles = new List<ReportVehicles>();
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = $@"SELECT TOP 100 * FROM dbo.tblDistributor";
                distributors = db.Database.SqlQuery<tblDistributor>(sqlSelect).ToList();

            }
            if(IdDistributor == 0)
            {
                IdDistributor = distributors.FirstOrDefault().IDDistributor;
            }
            //using (var db = new HMXuathangtudong_Entities())
            //{
            //    var sqlSelect = $@"SELECT DISTINCT v.Vehicle, v.NameDriver FROM tblVehicle v WHERE IDStore IN (SELECT TOP 1000 s.IDStore FROM dbo.tblStore s WHERE s.IDDistributor = @IDDistributor)";
            //    vehicles = db.Database.SqlQuery<ReportVehicles>(sqlSelect, new SqlParameter("@IDDistributor", IdDistributor)).ToList();
            //    ViewBag.Vehicles = vehicles;
            //}
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = $@"WITH listvehicles AS ( SELECT DISTINCT v.Vehicle
                                     FROM tblVehicle v
                                     WHERE IDStore IN (SELECT TOP 1000 s.IDStore FROM dbo.tblStore s WHERE s.IDDistributor = @IDDistributor))
                                     SELECT a.FullName as NameDriver,a.UserName, a.Phone, a.IdCard, listvehicles.Vehicle FROM dbo.tblAccount AS a
                                     INNER JOIN listvehicles ON  a.VehicleList LIKE '%'+listvehicles.Vehicle+'%'";
                vehicles = db.Database.SqlQuery<ReportVehicles>(sqlSelect, new SqlParameter("@IDDistributor", IdDistributor)).ToList();
                ViewBag.Vehicles = vehicles;
            }
            ViewBag.IdDistributor = IdDistributor;
            ViewBag.DistributorName = distributors.FirstOrDefault(x=>x.IDDistributor == IdDistributor).NameDistributor;
            return View(distributors);
        }
        [HttpGet]
        public FileResult ExportToExcel(int IdDistributor = 0, string fileName = "")
        {
            var file_name = fileName;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Vehicle"),
                                                     new DataColumn("DriverName"),
                                                     new DataColumn("Phone"),
                                                     new DataColumn("IdCard")});

            using (var db = new HMXuathangtudong_Entities())
            {
                //var sqlSelect = $@"SELECT DISTINCT v.Vehicle, v.NameDriver FROM tblVehicle v WHERE IDStore IN (SELECT TOP 1000 s.IDStore FROM dbo.tblStore s WHERE s.IDDistributor = @IDDistributor)";
                var sqlSelect = $@"WITH listvehicles AS ( SELECT DISTINCT v.Vehicle
                                     FROM tblVehicle v
                                     WHERE IDStore IN (SELECT TOP 1000 s.IDStore FROM dbo.tblStore s WHERE s.IDDistributor = @IDDistributor))
                                     SELECT a.FullName as NameDriver,a.UserName, a.Phone, a.IdCard, listvehicles.Vehicle FROM dbo.tblAccount AS a
                                     INNER JOIN listvehicles ON  a.VehicleList LIKE '%'+listvehicles.Vehicle+'%'";
                var vehicles = db.Database.SqlQuery<ReportVehicles>(sqlSelect, new SqlParameter("@IDDistributor", IdDistributor)).ToList();
                foreach (var vehicle in vehicles)
                {
                    dt.Rows.Add(vehicle.Vehicle, vehicle.NameDriver, vehicle.Phone, vehicle.IdCard);
                }
            }

            using (XLWorkbook wb = new XLWorkbook()) 
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $@"{file_name}.xlsx");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $@"{file_name}.xlsx");
                }
            }
        }
    }
   
    public class ReportVehicles
    {
        public string Vehicle { get; set; }
        public string NameDriver { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
    }
}