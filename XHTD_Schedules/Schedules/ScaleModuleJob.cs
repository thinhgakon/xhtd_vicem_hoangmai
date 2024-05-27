using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using RestSharp;
using XHTD_Schedules.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using HMXHTD.Services.Services;
using XHTD_Schedules.LEDControl;
using XHTD_Schedules.SignalRNotification;
using XHTD_Schedules.ApiTroughs;
using Autofac;
using HMXHTD.Data.Models;

namespace XHTD_Schedules.Schedules
{
    public class ScaleModuleJob : IJob
    {
        private IntPtr h21 = IntPtr.Zero;
        private IntPtr h = IntPtr.Zero;
        private static bool DeviceConnected = false;

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        private List<string> tmpCardNoIn_CN = new List<string>() { };
        private List<string> tmpCardNoOut_CN = new List<string>() { };
        private List<string> tmpCardNoIn_CC = new List<string>() { };
        private List<string> tmpCardNoOut_CC = new List<string>() { };
        private List<CardNoLog> tmpCardNoLst_CN = new List<CardNoLog>();
        private List<CardNoLog> tmpCardNoLst_CC = new List<CardNoLog>();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public ScaleModuleJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                ScaleModule();
            });
        }
        public void ScaleModule()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            while (!DeviceConnected)
            {
                ConnectScaleModule();
            }
            AuthenticateCardNoInScaleModule();
        }
        public bool ConnectScaleModule()
        {
            try
            {
                string str = "protocol=TCP,ipaddress=192.168.22.34,port=4370,timeout=2000,passwd=";
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                        log.Info("--------------ScaleModule connected--------------");
                        DeviceConnected = true;
                    }
                    else
                    {
                        log.Info("------------- ScaleModule -connected failed--------------");
                        ret = PullLastError();
                        DeviceConnected = false;
                    }
                }
                return DeviceConnected;
            }
            catch (Exception ex)
            {
                log.Error($@"ScaleModule : {ex.StackTrace}");
                return false;
            }
        }
        public void Test()
        {
            _serviceFactory.RFID.CheckRFIDByCardNo("1048627");
        }
        public void AuthenticateCardNoInScaleModule()
        {
            try
            {
                if (DeviceConnected)
                {
                    while (DeviceConnected)
                    {
                        int ret = 0, i = 0, buffersize = 256;
                        string str = "";
                        string[] tmp = null;
                        byte[] buffer = new byte[256];
                        if (IntPtr.Zero != h21)
                        {
                            ret = GetRTLog(h21, ref buffer[0], buffersize);
                            if (ret >= 0)
                            {
                                try
                                {
                                    str = Encoding.Default.GetString(buffer);
                                    tmp = str.Split(',');
                                    if (tmp[2] != "0")
                                    {
                                       //t    log.Info($@"============================tramcan_cardno================================= {tmp[2]?.ToString()}   ========= {tmp[3].ToString()}");
                                    }
                                    if (tmp[2] == "0" || tmp[2] == "")
                                    {
                                        ProcessLogFollow();
                                    }
                                    else
                                    {
                                        var cardNoCurrent = tmp[2]?.ToString();
                                        if (!_serviceFactory.RFID.CheckRFIDByCardNo(cardNoCurrent)) continue;
                                       // new MyHub().Send("TramCan_CardNo", $@"{tmp[2]?.ToString()} --- {tmp[3]?.ToString()}");
                                        // check step của đơn hàng hiện tại để xác định đang vào hay ra

                                        // chuyển ra cân nổi và vào cân nổi thành 1 hàm, tránh việc bắt được ở rfid này mà không bắt được rfid kia

                                        if(tmp[3].ToString() == "1" || tmp[3].ToString() == "2") // scale cn
                                        {
                                            // chỗ này xử lý thêm lock scale, khóa cân khi có xe đang cân, không xử lý nếu có xe khác bò lên
                                            // if (Program.IsScallingCN) continue;
                                            // nếu iSscalling true thì cần check thêm ở trong bảng scale để xem đơn đó đã cân chưa, để giải phóng nếu nó bị lock lâu quá


                                            //t  log.Info($@"========== scale CN ======== {cardNoCurrent}");
                                            if (tmpCardNoLst_CN.Count > 5) tmpCardNoLst_CN.RemoveRange(0, 2);
                                            var cardLogs = String.Join(";", tmpCardNoLst_CN.Select(x => x.LogCat).ToArray());
                                            //t log.Info($@"========== log list ======== {cardLogs}");
                                            if (tmpCardNoLst_CN.Exists(x => x.CardNo.Equals(cardNoCurrent) && x.DateTime > DateTime.Now.AddMinutes(-1)))
                                            {
                                                //t   log.Warn($@"========== cardno exist on list, ra cn======== {cardNoCurrent}");
                                                continue;
                                            }
                                            #region start code lock scale
                                            if (Program.IsScallingCN)
                                            {
                                                //t   log.Info($@"===============isscallingCN============");
                                                //check nếu đơn đang lock đã cân thì giải phóng
                                                var scaleInfo = _serviceFactory.Scale.GetByScaleCode("CN");
                                                //t   log.Info($@"===============isscallingCN============{scaleInfo.DeliveryCode}");
                                                if (!String.IsNullOrEmpty(scaleInfo.DeliveryCode))
                                                {
                                                    var weightInfo = _serviceFactory.StoreOrderOperating.GetScaleInfoByDeliveryCode(scaleInfo.DeliveryCode);
                                                    if (weightInfo != null && weightInfo.WEIGHTNULL < 0)
                                                    {
                                                   //t      log.Info($@"===============isscallingCN======is scalling but has other vehicle go up======{scaleInfo.DeliveryCode}");
                                                        continue;
                                                    }
                                                }
                                            }
                                            #endregion end code lock scale
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if(orderCurrent == null)
                                            {
                                                //t log.Info($@"========== log list ======== {cardLogs}");
                                                tmpCardNoLst_CN.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now});
                                                new MyHub().Send("Scale_CN_Warning", $@"Không nhận diện được xe bằng thẻ rfid này");
                                                continue;
                                            }
                                            else
                                            {
                                                if(orderCurrent.Step < 5) // cân vào
                                                {
                                                    log.Warn($@"========== scale in cn======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm3(cardNoCurrent))
                                                    {
                                                        log.Warn($@"========== xac thuc b3 thanh cong======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                        new MyHub().Send("Scale_In_CN", orderCurrent.Vehicle);
                                                      //t  log.Warn($@"========== send to trough======== {cardNoCurrent}");
                                                        AutoFacBootstrapper.Init().Resolve<TroughApiLib>().SendOrderToTrough(orderCurrent.CardNo);
                                                      //t  log.Warn($@"========== update scale======== {cardNoCurrent}");
                                                        _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CN", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: true, scaleOut: false);
                                                      //t  log.Warn($@"========== update log scale======== {cardNoCurrent}");
                                                        _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: true);
                                                        Program.IsScallingCN = true;
                                                        _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                                        tmpCardNoLst_CN.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                                    }
                                                    else
                                                    {
                                                        log.Warn($@"========== confirm 3 failed, scale in cn ======== {cardNoCurrent}");
                                                        new MyHub().Send("Scale_CN_Warning", $@"Xe {orderCurrent?.Vehicle} chưa được xác thực");
                                                    }
                                                }
                                                else
                                                {
                                                    log.Warn($@"==========scale out cn======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm7(cardNoCurrent))
                                                    {
                                                        log.Warn($@"==========confirm scale out successed, step 7======= {cardNoCurrent} {orderCurrent.Vehicle}");
                                                        new MyHub().Send("Scale_Out_CN", orderCurrent.Vehicle);
                                                        log.Warn($@"========== update scale======== {cardNoCurrent}");
                                                        _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CN", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: false, scaleOut: true);
                                                        _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: false);
                                                        Program.IsScallingCN = true;
                                                        tmpCardNoLst_CN.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                                    }
                                                    else
                                                    {
                                                        log.Warn($@"==========confirm scale out cn failed, step 7======= {cardNoCurrent} {orderCurrent.Vehicle}");
                                                    }
                                                    
                                                }
                                            }
                                        }
                                        else if (tmp[3].ToString() == "3" || tmp[3].ToString() == "4") // scale CC
                                        {
                                            log.Info($@"========== scale CC ======== {cardNoCurrent}");
                                            if (tmpCardNoLst_CC.Count > 5) tmpCardNoLst_CC.RemoveRange(0, 2);
                                            var cardLogs = String.Join(";", tmpCardNoLst_CC.Select(x => x.LogCat).ToArray());
                                          //t  log.Info($@"========== log list CC ======== {cardLogs}");
                                            if (tmpCardNoLst_CC.Exists(x => x.CardNo.Equals(cardNoCurrent) && x.DateTime > DateTime.Now.AddMinutes(-1)))
                                            {
                                          //t      log.Warn($@"========== cardno exist on list, ra cn CC======== {cardNoCurrent}");
                                                continue;
                                            }
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if (orderCurrent == null)
                                            {
                                            //t    log.Info($@"========== log list CC ======== {cardLogs}");
                                                tmpCardNoLst_CC.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                new MyHub().Send("Scale_CC_Warning", $@"Không nhận diện được xe bằng thẻ rfid này");
                                                continue;
                                            }
                                            else
                                            {
                                                if (orderCurrent.Step < 5) // cân vào
                                                {
                                                    log.Warn($@"========== scale in  cc======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm3(cardNoCurrent))
                                                    {
                                                        log.Warn($@"========== xac thuc b3 thanh cong CC======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                        new MyHub().Send("Scale_In_CC", orderCurrent.Vehicle);
                                                      //t  log.Warn($@"========== send to trough CC======== {cardNoCurrent}");
                                                        AutoFacBootstrapper.Init().Resolve<TroughApiLib>().SendOrderToTrough(orderCurrent.CardNo);
                                                      //t  log.Warn($@"========== update scale cc======== {cardNoCurrent}");
                                                        _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CC", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: true, scaleOut: false);
                                                      //t  log.Warn($@"========== update log scale cc======== {cardNoCurrent}");
                                                        _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: true);
                                                        Program.IsScallingCC = true;
                                                        _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                                        tmpCardNoLst_CC.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                                    }
                                                    else
                                                    {
                                                        log.Warn($@"========== confirm 3 failed, vao cc ======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                        new MyHub().Send("Scale_CC_Warning", $@"Xe {orderCurrent?.Vehicle} chưa được xác thực");
                                                    }
                                                }
                                                else
                                                {
                                                    log.Warn($@"==========ra can chim cc======== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm7(cardNoCurrent))
                                                    {
                                                        log.Warn($@"==========scale out cc, step 7======= {cardNoCurrent} {orderCurrent.Vehicle}");
                                                        new MyHub().Send("Scale_Out_CC", orderCurrent.Vehicle);
                                                      //t  log.Warn($@"========== update scale cc======== {cardNoCurrent}");
                                                        _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CC", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: false, scaleOut: true);
                                                        _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: false);
                                                        Program.IsScallingCC = true;
                                                        tmpCardNoLst_CC.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                                    }
                                                    else
                                                    {
                                                        log.Warn($@"========== confirm scale out cc failed=== {cardNoCurrent} {orderCurrent.Vehicle}");
                                                    }
                                                }
                                            }
                                        }

                                        #region process old

                                        //if (tmp[3].ToString() == "1")  // ra cân nổi
                                        //{
                                        //   // if (Program.IsScallingCN) continue;
                                        //   // _serviceFactory.Notification.SendNotificationToTelegram($@"cân nổi {cardNoCurrent}");
                                        //    if (tmpCardNoOut_CN.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null)
                                        //    {
                                        //        log.Warn($@"========== cardno exist on list, ra cn======== {cardNoCurrent}");
                                        //        continue;
                                        //    }


                                        //    // if (orderCurrent.Step < 7 && _serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm2(cardNoCurrent))
                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm7(cardNoCurrent))
                                        //    {
                                        //        log.Warn($@"========== confirm 7, ra cn ======== {cardNoCurrent}");
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        //        new MyHub().Send("Scale_Out_CN", orderCurrent.Vehicle);
                                        //        //scale job
                                        //        if (orderCurrent != null)
                                        //        {
                                        //            _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CN", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: false, scaleOut: true);
                                        //            _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: false);
                                        //            Program.IsScallingCN = true;
                                        //           // _serviceFactory.Notification.SendNotificationToTelegram($@"{orderCurrent.Vehicle} ra cân nổi");
                                        //        }
                                        //        //end scale job

                                        //        tmpCardNoOut_CN.Add(cardNoCurrent);
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                        //        //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        //    }
                                        //    if (tmpCardNoOut_CN.Count > 2) tmpCardNoOut_CN.RemoveRange(0, 2);
                                        //}
                                        //else if (tmp[3].ToString() == "2")//  && orderCurrent.Step == 2)  // vào cân nổi
                                        //{
                                        //   // if (Program.IsScallingCN) continue;
                                        //    if (tmpCardNoIn_CN.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null)
                                        //    {
                                        //        log.Warn($@"========== cardno exist on list, vao cn======== {cardNoCurrent}");
                                        //        continue;
                                        //    }
                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm3(cardNoCurrent))
                                        //    {
                                        //        log.Warn($@"========== confirm 3, vao cn ======== {cardNoCurrent}");
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        //        new MyHub().Send("Scale_In_CN", orderCurrent.Vehicle);
                                        //        //scale job
                                        //        if (orderCurrent != null)
                                        //        {
                                        //            AutoFacBootstrapper.Init().Resolve<TroughApiLib>().SendOrderToTrough(orderCurrent.CardNo);
                                        //           // log.Info($@"=========Vào cân nổi================== 2 ==={orderCurrent.DeliveryCode}");
                                        //            _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CN", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: true, scaleOut: false);
                                        //            _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: true);
                                        //            Program.IsScallingCN = true;
                                        //          //  _serviceFactory.Notification.SendNotificationToTelegram($@"{orderCurrent.Vehicle} vào cân nổi");
                                        //            _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                        //        }
                                        //        //end scale job
                                        //        tmpCardNoIn_CN.Add(cardNoCurrent);
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                        //        // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        //    }else
                                        //    {
                                        //        log.Warn($@"========== confirm 3 failed, vao cn ======== {cardNoCurrent}");
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        //        if(orderCurrent == null || orderCurrent.Step == 0)
                                        //        {
                                        //            new MyHub().Send("Scale_CN_Warning", $@"Xe {orderCurrent?.Vehicle} chưa được xác thực");
                                        //        }
                                        //    }
                                        //    if (tmpCardNoIn_CN.Count > 2) tmpCardNoIn_CN.RemoveRange(0, 2);
                                        //}
                                        //else 



                                        //if (tmp[3].ToString() == "3")  // vào cân chìm
                                        //{
                                        //    //if (Program.IsScallingCC) continue;
                                        //    if (tmpCardNoIn_CC.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null)
                                        //    {
                                        //        log.Warn($@"========== cardno exist on list, vao cc======== {cardNoCurrent}");
                                        //        continue;
                                        //    }

                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm3(cardNoCurrent))
                                        //    {
                                        //        log.Warn($@"========== confirm 3, vao cc ======== {cardNoCurrent}");
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        //        new MyHub().Send("Scale_In_CC", orderCurrent.Vehicle);
                                        //        //scale job
                                        //        if (orderCurrent != null)
                                        //        {
                                        //            AutoFacBootstrapper.Init().Resolve<TroughApiLib>().SendOrderToTrough(orderCurrent.CardNo);
                                        //            _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CC", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: true, scaleOut: false);
                                        //            _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: true);
                                        //            Program.IsScallingCC = true;
                                        //          //  _serviceFactory.Notification.SendNotificationToTelegram($@"{orderCurrent.Vehicle} vào cân chìm");
                                        //        }
                                        //        //end scale job
                                        //        tmpCardNoIn_CC.Add(cardNoCurrent);
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                        //        _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                        //        // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        //    }
                                        //    else
                                        //    {
                                        //        log.Warn($@"========== confirm 3 failed, vao cc ======== {cardNoCurrent}");
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        //        if (orderCurrent == null || orderCurrent.Step == 0)
                                        //        {
                                        //            new MyHub().Send("Scale_CN_Warning", $@"Xe {orderCurrent?.Vehicle} chưa được xác thực");
                                        //        }
                                        //    }
                                        //    if (tmpCardNoIn_CC.Count > 2) tmpCardNoIn_CC.RemoveRange(0, 2);
                                        //}
                                        //else if (tmp[3].ToString() == "4")  // ra cân chìm
                                        //{
                                        //    //if (Program.IsScallingCC) continue;
                                        //    if (tmpCardNoOut_CC.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null)
                                        //    {
                                        //        log.Warn($@"========== cardno exist on list, ra cc======== {cardNoCurrent}");
                                        //        continue;
                                        //    }

                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm7(cardNoCurrent))
                                        //    {
                                        //        log.Warn($@"========== confirm 7, ra cc ======== {cardNoCurrent}");
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        //        new MyHub().Send("Scale_Out_CC", orderCurrent.Vehicle);
                                        //        //scale job
                                        //        if (orderCurrent != null)
                                        //        {
                                        //            _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CC", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: false, scaleOut: true);
                                        //            _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: false);
                                        //            Program.IsScallingCC = true;
                                        //         //   _serviceFactory.Notification.SendNotificationToTelegram($@"{orderCurrent.Vehicle} ra cân chìm");
                                        //        }
                                        //        //end scale job

                                        //        tmpCardNoOut_CC.Add(cardNoCurrent);
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                        //        //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        //    }
                                        //    if (tmpCardNoOut_CC.Count > 2) tmpCardNoOut_CC.RemoveRange(0, 2);
                                        //}

                                        #endregion

                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.Error($@"Lỗi xẩy ra {ex.StackTrace} {ex.Message} ");
                                    continue;
                                }
                            }
                            else
                            {
                                log.Warn("Lỗi không đọc được dữ liệu, có thể do mất kết nối");
                                DeviceConnected = false;
                                h21 = IntPtr.Zero;
                                ScaleModule();
                            }
                        }
                    }
                }
                else
                {
                    DeviceConnected = false;
                    h21 = IntPtr.Zero;
                    ScaleModule();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===============ScaleModule=========================== {Ex.StackTrace}");
            }
        }
        public void ProcessLogFollow()
        {
            try
            {
                var unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                if (unixTimestamp % 300000 < 2)
                {
                   log.Info("====================Log status is running==========================");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
