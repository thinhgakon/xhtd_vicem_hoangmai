using HMXHTD.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMXHTD.Data.DataEntity;
using XHTD_Schedules.ApiScales;
using Autofac;
using XHTD_Schedules.SignalRNotification;
using PLC_Lib;
using System.Threading;

namespace XHTD_Schedules.BarrierLib
{
    public class BarrierScaleBusiness
    {
        private PLCConnect PLC_M221;
        private Result PLC_Result;
        private static bool M221Connected = false;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public BarrierScaleBusiness(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public void ProcessOffBarrierScale(bool isScaleCN)
        {

            try
            {
                log.Info($@"===========Đóng barrier 1========isCN {isScaleCN}");
                var CheckSensor = false;
                if (!CheckIsActiveBarrierScale(isScaleCN)) return;
                var connected = ConnectSensor();
                if (connected == false) return;
                if (isScaleCN)
                {
                    //while (!CheckSensor)
                    //{
                        log.Info($@"===========Đóng barrier 2========");
                        CheckSensor =  CheckSensorScale(true);
                    if (!CheckSensor)
                    {
                        log.Warn($"============Sensor Covered cn=========");
                        return;
                    }
                    //}
                    // đóng barrier
                    OnOffBarrierScale(true, false, false);
                }
                else
                {
                    CheckSensor = CheckSensorScale(false);
                    if (!CheckSensor)
                    {
                        log.Warn($"============Sensor Covered cc=========");
                        return;
                    }
                    //while (!CheckSensor)
                    //{
                    //    CheckSensor = CheckSensorScale(false);
                    //    if (!CheckSensor)
                    //    {
                    //        log.Warn($"============Sensor Covered cc=========");
                    //    }
                    //}
                    // đóng barrier
                    OnOffBarrierScale(false, false, false);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void ResetOutPort(string port)
        {
            try
            {
                if (ConnectSensor() == false) return;
                if (ReadPortsOut(byte.Parse(port)))
                {
                    PLC_M221.ShuttleOutputPort((byte.Parse(port)));
                } 
            }
            catch (Exception ex)
            {

            }
        }
        private bool ReadPortsOut(byte k)
        {
            bool[] Ports = new bool[23]; //There are 24 input ports on M221 PLC
            PLC_Result = PLC_M221.CheckOutputPorts(Ports);
            if (PLC_Result == Result.SUCCESS)
            {
                if (Ports[k])
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;

        }
        public void OnOffBarrierScale(bool isScaleCN, bool isOpen, bool waitSleep)
        {
            if (!CheckIsActiveBarrierScale(isScaleCN)) return;
            try
            {
                log.Info($@"===========OnOffBarrierScale 1 isOpen :{isOpen}========");
                if (ConnectSensor() == false) return;
                if (waitSleep)
                {
                    Thread.Sleep(15000);
                }
                if (isScaleCN)
                {
                    if (isOpen)
                    {
                        
                        log.Info($@"===========OnOffBarrierScale 2========");
                        if (!CheckPortIsOn(byte.Parse("8"))) {
                            // mở barrier
                            ResetOutPort("5");
                            log.Info($@"===========OnOffBarrierScale mở barrier 8========");
                            ProcessShuttleOutputPort("5");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("5");
                        }
                        if(!CheckPortIsOn(byte.Parse("9"))) {
                            // mở barrier
                            ResetOutPort("6");
                            log.Info($@"===========OnOffBarrierScale mở barrier 9========");
                            ProcessShuttleOutputPort("6");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("6");
                        }
                        SetLightScaleStatus(true, true, false); 
                        SetLightScaleStatus(true, false, true);
                    }
                    else
                    {
                        if (CheckPortIsOn(byte.Parse("8"))) {
                            // đóng barrier
                            ResetOutPort("4");
                            ProcessShuttleOutputPort("4");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("4");
                        }
                        if (CheckPortIsOn(byte.Parse("9"))) {
                            //đóng barrier
                            ResetOutPort("7");
                            ProcessShuttleOutputPort("7");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("7");
                        }
                    }
                }
                else
                {
                    if (isOpen)
                    {
                        if (!CheckPortIsOn(byte.Parse("6")))
                        {
                            // mở barrier
                            ResetOutPort("0");
                            ProcessShuttleOutputPort("0");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("0");
                        }
                        if (!CheckPortIsOn(byte.Parse("7")))
                        {
                            // mở barrier
                            ResetOutPort("3");
                            ProcessShuttleOutputPort("3");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("3");
                        }
                        SetLightScaleStatus(false, true, false);
                        SetLightScaleStatus(false, false, true);
                    }
                    else
                    {
                        if (CheckPortIsOn(byte.Parse("6")))
                        {
                            // đóng barrier
                            ResetOutPort("1");
                            ProcessShuttleOutputPort("1");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("1");
                        }
                        if (CheckPortIsOn(byte.Parse("7")))
                        {
                            //đóng barrier
                            ResetOutPort("2");
                            ProcessShuttleOutputPort("2");
                            Thread.Sleep(1000);
                            ProcessShuttleOutputPort("2");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void ProcessShuttleOutputPort(string port)
        {
            log.Info($@"===============port: {port}============");
            PLC_M221.ShuttleOutputPort((byte.Parse(port)));
        }
        public bool ConnectSensor()
        {
            try
            {
               //if (M221Connected) return true;
                PLC_M221 = new PLCConnect();
                PLC_M221.Mode = Mode.TCP_IP;
                PLC_M221.ResponseTimeout = 1000;
                PLC_Result = PLC_M221.Connect("192.168.22.38", 502);
                if (PLC_Result == Result.SUCCESS) {
                    M221Connected = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return false;
        }
        public bool CheckIsActiveBarrierScale(bool isScaleCN)
        {
            bool IsActive = false;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var code = isScaleCN ? "ActiveBarrierCN" : "ActiveBarrierCC";
                    var config = db.tblConfigOperatings.FirstOrDefault(x => x.Code.Equals(code));
                    if (config.Value == 1) IsActive = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return IsActive;
        }
        public bool CheckSensorScale(bool isScaleCN)
        {
            bool IsUnobstructed = false;
            try
            {
                if (isScaleCN)
                {
                    var checkInCN = CheckPortIsOn(byte.Parse("4"));
                    var checkOutCN = CheckPortIsOn(byte.Parse("5"));
                    if (checkInCN && checkOutCN) IsUnobstructed = true;
                }
                else
                {
                    //var checkInCC = CheckPortIsOn(byte.Parse("0"));
                    //var checkOutCC = CheckPortIsOn(byte.Parse("2"));
                    //var checkVerticalRightCC = CheckPortIsOn(byte.Parse("1"));
                    //var checkVerticalLeftCC = CheckPortIsOn(byte.Parse("3"));
                    //if (checkInCC && checkOutCC && checkVerticalRightCC && checkVerticalLeftCC) IsUnobstructed = true;
                    IsUnobstructed = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return IsUnobstructed;
        }
        
        private bool CheckPortIsOn(byte k)
        {
            bool isOn = false;
            try
            {

                bool[] Ports = new bool[24];
                PLC_Result = PLC_M221.CheckInputPorts(Ports);
                if (PLC_Result == Result.SUCCESS)
                {
                    if (Ports[k])
                    {
                        isOn = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return isOn;
        }
        private void SetLightScaleStatus(bool isScaleCN, bool green, bool isSleep)
        {
            try
            {
                if (isSleep)
                {
                    var sleepTime = _serviceFactory.ConfigOperating.GetValueByCode("SleepWakeUpScaleLight");
                    Thread.Sleep(sleepTime * 1000);
                }
                if (isScaleCN)
                {
                    ResetOutPort("10"); ResetOutPort("11"); 
                    if (green)
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("10")));
                    }
                    else
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("11")));
                    }
                }
                else
                {
                    ResetOutPort("8"); ResetOutPort("9");
                    if (green)
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("8")));
                    }
                    else
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("9")));
                    }
                } 
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
