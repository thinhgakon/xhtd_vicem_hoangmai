using PLC_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XHTD_WEB.Models;

namespace XHTD_WEB.Controllers
{
    public class PLCController : Controller
    {
        private Result PLC_Result;
        // GET: PLC
        public ActionResult Index()
        {
            var res = new List<PLCM221OutputStatus>();
            try
            
            {
                PLCConnect PLC_M221 = new PLCConnect();
                PLC_M221.Mode = Mode.TCP_IP;
                PLC_M221.ResponseTimeout = 1000;
                Result PLC_Result = PLC_M221.Connect("192.168.22.38", 502);
                if (PLC_Result == Result.SUCCESS)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        bool[] Ports = new bool[23];
                        PLC_Result = PLC_M221.CheckOutputPorts(Ports);
                        if (PLC_Result == Result.SUCCESS)
                        {
                            if (Ports[byte.Parse(i.ToString())])
                            {
                                res.Add(new PLCM221OutputStatus { 
                                Port = i,
                                Active = true
                                });
                            }
                            else
                            {
                                res.Add(new PLCM221OutputStatus
                                {
                                    Port = i,
                                    Active = false
                                });
                            }

                        }
                    }
                }
                PLC_M221.Close();
            }
            catch (Exception ex)
            {

            }
           
            return View(res);
        }
        [HttpPost]
        public JsonResult SetLightM221(string value)
        {
            if (String.IsNullOrEmpty(value)) return Json("Chưa nhập giá trị", JsonRequestBehavior.AllowGet);
            var valueInt = Int32.Parse(value);
            try
            {
                PLCConnect PLC_M221 = new PLCConnect();
                PLC_M221.Mode = Mode.TCP_IP;
                PLC_M221.ResponseTimeout = 1000;
                Result PLC_Result = PLC_M221.Connect("192.168.22.38", 502);
                if (PLC_Result == Result.SUCCESS)
                {
                    PLC_M221.ShuttleOutputPort((byte.Parse(valueInt.ToString())));
                }
                PLC_M221.Close();
            }
            catch (Exception ex)
            {
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}