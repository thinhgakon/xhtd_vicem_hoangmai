using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;

namespace Server_Monitor_Service.Schedules
{
    public class CPUJob : IJob
    {

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        int totalHits = 0;
        static List<float> AvailableCPU = new List<float>();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CPUJob()
        {
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                MonitorCPUAndRAM();
            });
        }
        public void CheckCPU()
        {
            try
            {
                while (true)
                {

                    int cpuPercent = (int)getCPUCounter();
                  //  Console.WriteLine($@"{cpuPercent} %");
                    if (cpuPercent >= 20)
                    {
                        totalHits = totalHits + 1;
                        if (totalHits == 60)
                        {
                       //     Console.WriteLine("ALERT 90% usage for 1 minute");
                            totalHits = 0;
                        }
                    }
                    else
                    {
                        totalHits = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public float getCPUCounter()
        {
            float secondValue = 0;

            try
            {
                PerformanceCounter cpuCounter = new PerformanceCounter();
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";

                // will always start at 0
                dynamic firstValue = cpuCounter.NextValue();
                System.Threading.Thread.Sleep(1000);
                // now matches task manager reading
                secondValue = cpuCounter.NextValue();

            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }

            return secondValue;
        }
        public void MonitorCPUAndRAMTEST()
        {
            while (true)
            {
                try
                {
                    
                    Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
                    Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
                    Int64 t = PerformanceInfo.GetPerfomance();
                    decimal percentFree = ((decimal)phav / (decimal)tot) * 100;
                    decimal percentOccupied = 100 - percentFree;
                    float cpu = getCPUCounter();
                    log.Info($@"CPU:{cpu} ====== RAM:{percentOccupied}");
                    if (cpu > 75 || percentOccupied > 75)
                    {
                        SendNotificationToTelegram($@"c {cpu} r {percentOccupied}");
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
        }
        public void MonitorCPUAndRAM()
        {
            try
            {
                Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
                Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
                Int64 t = PerformanceInfo.GetPerfomance();
                decimal percentFree = ((decimal)phav / (decimal)tot) * 100;
                decimal percentOccupied = 100 - percentFree;
                float cpu = getCPUCounter();
                log.Info($@"CPU:{cpu} ====== RAM:{percentOccupied}");
                if (cpu > 85 || percentOccupied > 85)
                {
                    SendNotificationToTelegram($@"ServerName: {Program.ServerName} === CPU {(int)cpu} % ======= RAM {(int)percentOccupied} %");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public async Task SendNotificationToTelegram(string message)
        {
            try
            {
                string apiToken = "1847865636:AAEi3f9oUTqQGqqjM785rPZn51P3mmu_pxg";
                string chatId = "-506163941";
                var bot = new TelegramBotClient(apiToken);
                var s = await bot.SendTextMessageAsync(chatId, message);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public async Task SendNotificationToTelegram1(string message)
        {
            try
            {
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "1847865636:AAEi3f9oUTqQGqqjM785rPZn51P3mmu_pxg";
                string chatId = "-506163941";
                string text = message;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
               var res = await request.GetResponseAsync();
                log.Info(res.ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

    }
    public static class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInformation
        {
            public int Size;
            public IntPtr CommitTotal;
            public IntPtr CommitLimit;
            public IntPtr CommitPeak;
            public IntPtr PhysicalTotal;
            public IntPtr PhysicalAvailable;
            public IntPtr SystemCache;
            public IntPtr KernelTotal;
            public IntPtr KernelPaged;
            public IntPtr KernelNonPaged;
            public IntPtr PageSize;
            public int HandlesCount;
            public int ProcessCount;
            public int ThreadCount;
        }

        public static Int64 GetPhysicalAvailableMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }

        }

        public static Int64 GetTotalMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }

        }
        public static Int64 GetPerfomance()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }

        }
    }
}
