using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLC_Lib;
using System.Threading;

namespace HMXHTD.BarrierLib
{
    public class BarrierScaleBusiness
    {
        private PLCConnect PLC_M221;
        private Result PLC_Result;
        private static bool M221Connected = false;
        public BarrierScaleBusiness()
        {
        }
        public void OpenBarrierScale(bool isScaleCN)
        {
            try
            {
                if (ConnectSensor() == false) return;
                if (isScaleCN)
                {
                    if (!CheckPortIsOn(byte.Parse("8")))
                    {
                        // mở barrier
                        ProcessShuttleOutputPort("5");
                        Thread.Sleep(1000);
                        ProcessShuttleOutputPort("5");
                    }
                    if (!CheckPortIsOn(byte.Parse("9")))
                    {
                        // mở barrier
                        ProcessShuttleOutputPort("6");
                        Thread.Sleep(1000);
                        ProcessShuttleOutputPort("6");
                    }
                }
                else
                {
                    if (!CheckPortIsOn(byte.Parse("6")))
                    {
                        // mở barrier
                        ProcessShuttleOutputPort("0");
                        Thread.Sleep(1000);
                        ProcessShuttleOutputPort("0");
                    }
                    if (!CheckPortIsOn(byte.Parse("7")))
                    {
                        // mở barrier
                        ProcessShuttleOutputPort("3");
                        Thread.Sleep(1000);
                        ProcessShuttleOutputPort("3");
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        public void ProcessShuttleOutputPort(string port)
        {
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
                if (PLC_Result == Result.SUCCESS)
                {
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
            }
            return false;
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
            }
            return isOn;
        }
    }
}
