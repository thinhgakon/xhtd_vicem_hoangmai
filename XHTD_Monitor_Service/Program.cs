﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XHTD_Monitor_Service.Schedules;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace XHTD_Monitor_Service
{
    
    static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main()
        {
            //AutoFacBootstrapper.Init().Resolve<CPUJob>().MonitorCPUAndRAM();

            //Console.ReadKey();

            var s = new Service();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                s
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
