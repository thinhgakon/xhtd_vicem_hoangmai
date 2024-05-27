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

namespace XHTD_Schedules.ScaleBusiness
{
    public class DesicionScaleBusiness
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public DesicionScaleBusiness(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public ScaleResponseModel MakeDecisionScaleIn(string deliveryCode, int weight, bool isCN)
        {
            var respontModel = new ScaleResponseModel
            {
                code = "02",
                message = "Cân thất bại"
            };
            try
            {
                if (!CheckIsAutoScale()) return new ScaleResponseModel { code = "02", message = "cân tự động đang tắt"};
                var cardno = _serviceFactory.StoreOrderOperating.GetCardNoByDeliveryCode(deliveryCode);
                var orders = _serviceFactory.StoreOrderOperating.GetAllOrderReceivingByCardNo(cardno);
                var deliveryCodes = String.Join(";", orders.Select(x=>x.DeliveryCode).ToArray());
                if (orders.Count == 1)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        //if (orderScale == null || orderScale.IsSentScaleIn == true) return new ScaleResponseModel { code = "02", message = "đã cân" };
                        var response = AutoFacBootstrapper.Init().Resolve<ScaleApiLib>().ScaleIn(deliveryCode, weight);
                        log.Warn($@"=========== ScaleIn 1d========{orderScale.DeliveryCode}==={response.code}====={response.message}");
                        _serviceFactory.Notification.SendNotificationToTelegramMonitor($"==scalein==={orderScale.DeliveryCode}==={response.code}====={response.message}");
                        var messageScaleType = isCN ? "Cân nổi: " : "Cân chìm: ";
                        if (response.code == "01")
                        {
                            _serviceFactory.StoreOrderOperating.UpdateShiftOrderByDeliveryCode(deliveryCode);
                            new MyHub().Send("Scale_Send_Successed", $@"{messageScaleType} : {response.message}");
                        }
                        else
                        {
                            new MyHub().Send("Scale_Send_Failed", $@"{messageScaleType} : {response.message}");
                        }
                        if (response.code == "01")
                        {
                            orderScale.IsSentScaleIn = true;
                            orderScale.Note = $@"{orderScale.Note} # Scale In at {DateTime.Now} {response.message}";
                            db.SaveChanges();
                        }
                        else
                        {
                            orderScale.Note = $@"{orderScale.Note} # Scale In at {DateTime.Now} {response.message}";
                            db.SaveChanges();
                        }
                        respontModel.code = response.code;
                        respontModel.message = response.message;
                    }
                }
                else
                {
                    var response = AutoFacBootstrapper.Init().Resolve<ScaleApiLib>().ScaleIn(deliveryCodes, weight);
                    log.Warn($@"=========== ScaleIn 2d ======{deliveryCodes}====={response.code}====={response.message}");
                    _serviceFactory.Notification.SendNotificationToTelegramMonitor($"==scalein==={deliveryCodes}==={response.code}====={response.message}");
                    var messageScaleType = isCN ? "Cân nổi: " : "Cân chìm: ";
                    if (response.code == "01")
                    {
                        foreach (var order in orders)
                        {
                            _serviceFactory.StoreOrderOperating.UpdateShiftOrderByDeliveryCode(order.DeliveryCode);
                        }
                        new MyHub().Send("Scale_Send_Successed", $@"{messageScaleType} : {response.message}");
                    }
                    else
                    {
                        new MyHub().Send("Scale_Send_Failed", $@"{messageScaleType} : {response.message}");
                    }
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        foreach (var order in orders)
                        {
                            var orderScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == order.DeliveryCode);
                            //if (orderScale == null || orderScale.IsSentScaleIn == true) return new ScaleResponseModel { code = "02", message = "đã cân" };
                            if (orderScale == null) continue;
                            if (response.code == "01")
                            {
                                orderScale.IsSentScaleIn = true;
                                orderScale.Note = $@"{orderScale.Note} # Scale In at {DateTime.Now} {response.message}";
                                db.SaveChanges();
                            }
                            else
                            {
                                orderScale.Note = $@"{orderScale.Note} # Scale In at {DateTime.Now} {response.message}";
                                db.SaveChanges();
                            }
                        }
                    }
                    respontModel.code = response.code;
                    respontModel.message = response.message;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return respontModel;
        }
        public ScaleResponseModel MakeDecisionScaleOut(string deliveryCode, int weight, bool isCN)
        {
            var respontModel = new ScaleResponseModel {
               code = "02",
               message = "Cân thất bại"
            };
            try
            {
                if (!CheckIsAutoScale()) return new ScaleResponseModel { code = "02", message = "cân tự động đang tắt" };
                var cardno = _serviceFactory.StoreOrderOperating.GetCardNoByDeliveryCode(deliveryCode);
                var orders = _serviceFactory.StoreOrderOperating.GetAllOrderReceivingByCardNo(cardno);
                var deliveryCodes = String.Join(";", orders.Select(x => x.DeliveryCode).ToArray());
                if (CheckToleranceLimitXiRoi(orders, weight))
                {
                    log.Info($"======CheckToleranceLimit xi roi======{deliveryCode}==={weight}");
                    new MyHub().Send("Scale_Send_Failed", "Vượt quá 10% dung sai cho phép, loại hàng xi rời");
                    _serviceFactory.Notification.SendNotificationToTelegramMonitor($"==scaleout==={deliveryCodes}========Vượt quá 10% dung sai cho phép, loại hàng xi rời");
                    return new ScaleResponseModel { code = "02", message = "Vượt quá 10% dung sai cho phép, loại hàng xi rời" };
                }
                if (CheckToleranceLimitClinker(orders, weight))
                {
                    log.Info($"======CheckToleranceLimit clinker======{deliveryCode}==={weight}");
                    new MyHub().Send("Scale_Send_Failed", "Vượt quá 30% dung sai cho phép, loại hàng clinker");
                    _serviceFactory.Notification.SendNotificationToTelegramMonitor($"==scaleout==={deliveryCodes}========Vượt quá 30% dung sai cho phép, loại hàng clinker");
                    return new ScaleResponseModel { code = "02", message = "Vượt quá 30% dung sai cho phép, loại hàng clinker" };
                }
                if (CheckToleranceLimit(orders, weight))
                {
                    log.Info($"======CheckToleranceLimit======{deliveryCode}==={weight}");
                    new MyHub().Send("Scale_Send_Failed", "Vượt quá dung sai cho phép, loại hàng jumbo");
                    return new ScaleResponseModel { code = "02", message = "Vượt quá dung sai cho phép, loại hàng jumbo" };
                }
                if (orders.Count == 1)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        //if (orderScale == null || orderScale.IsSentScaleOut == true) return new ScaleResponseModel { code = "02", message = "đã cân" }; 
                        //if (CheckToleranceLimit(orders.FirstOrDefault(), weight))
                        //{
                        //    log.Info($"======CheckToleranceLimit======{deliveryCode}==={weight}");
                        //    new MyHub().Send("Scale_Send_Failed", "Vượt quá dung sai cho phép, loại hàng jumbo");
                        //    return new ScaleResponseModel { code = "02", message = "Vượt quá dung sai cho phép, loại hàng jumbo" };
                        //}
                        
                        var response = AutoFacBootstrapper.Init().Resolve<ScaleApiLib>().ScaleOut(deliveryCode, weight);
                        log.Warn($@"=========== Scaleout 1d ===={orderScale.DeliveryCode}======={response.code}====={response.message}");
                        _serviceFactory.Notification.SendNotificationToTelegramMonitor($"==scaleout==={orderScale.DeliveryCode}==={response.code}====={response.message}");
                        var messageScaleType = isCN ? "Cân nổi: " : "Cân chìm: ";
                        if (response.code == "01")
                        {
                            new MyHub().Send("Scale_Send_Successed", $@"{messageScaleType} : {response.message}");
                            _serviceFactory.StoreOrderOperating.UpdateScaleOrderSuccessedByDeliveryCode(deliveryCode,false,true);
                        }
                        else
                        {
                            new MyHub().Send("Scale_Send_Failed", $@"{messageScaleType} : {response.message}");
                        }
                        if (response.code == "01")
                        {
                            orderScale.IsSentScaleOut = true;
                            orderScale.Note = $@"{orderScale.Note} # Scale Out at {DateTime.Now} {response.message}";
                            db.SaveChanges();
                        }
                        else
                        {
                            orderScale.Note = $@"{orderScale.Note} # Scale Out at {DateTime.Now} {response.message}";
                            db.SaveChanges();
                        }
                        respontModel.code = response.code;
                        respontModel.message = response.message;
                    }
                }
                else
                {
                    log.Warn($@"=========== Scaleout2d1 ======");
                    var response = AutoFacBootstrapper.Init().Resolve<ScaleApiLib>().ScaleOut(deliveryCodes, weight);
                    log.Warn($@"=========== Scaleout2d2 ======");
                    log.Warn($@"=========== Scaleout 2d ======{deliveryCodes}===={weight}====={response?.code}====={response?.message}");
                    _serviceFactory.Notification.SendNotificationToTelegramMonitor($"==scaleout==={deliveryCodes}==={response.code}====={response.message}");
                    var messageScaleType = isCN ? "Cân nổi: " : "Cân chìm: ";
                    if (response.code == "01")
                    {
                        new MyHub().Send("Scale_Send_Successed", $@"{messageScaleType} : {response.message}");
                        foreach (var order in orders)
                        {
                            _serviceFactory.StoreOrderOperating.UpdateScaleOrderSuccessedByDeliveryCode(order.DeliveryCode, false, true);
                        }
                    }
                    else
                    {
                        new MyHub().Send("Scale_Send_Failed", $@"{messageScaleType} : {response.message}");
                    }
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        foreach (var order in orders)
                        {
                            var orderScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == order.DeliveryCode);
                            //if (orderScale == null || orderScale.IsSentScaleOut == true) return new ScaleResponseModel { code = "02", message = "đã cân" };
                            if (orderScale == null) continue;
                            if (response.code == "01")
                            {
                                orderScale.IsSentScaleOut = true;
                                orderScale.Note = $@"{orderScale.Note} # Scale Out at {DateTime.Now} {response.message}";
                                db.SaveChanges();
                            }
                            else
                            {
                                orderScale.Note = $@"{orderScale.Note} # Scale Out at {DateTime.Now} {response.message}";
                                db.SaveChanges();
                            }
                        }
                    }
                    respontModel.code = response.code;
                    respontModel.message = response.message;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return respontModel;
        }
        //public bool CheckToleranceLimit(tblStoreOrderOperating order, int weight)
        //{
        //    bool isCheck = false;
        //    try
        //    {
        //        if (order.NameProduct.Contains("Jumbo"))
        //        {
        //            var tolerance = (weight - order.WeightIn - order.SumNumber *1000) / (order.SumNumber*1000);
        //            tolerance = tolerance < 0 ? (-1) * tolerance : tolerance;
        //            isCheck = (double)tolerance > 0.01 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error($"====CheckToleranceLimit====={ex.StackTrace}");
        //    }
        //    return isCheck;
        //}
        public bool CheckToleranceLimit(List<tblStoreOrderOperating> orders, int weight)
        {
            bool isCheck = false;
            try
            {
                if (orders.FirstOrDefault().NameProduct.Contains("Jumbo"))
                {
                    var tolerance = (weight - orders.FirstOrDefault(x => x.WeightIn > 0).WeightIn - orders.Sum(x => x.SumNumber) * 1000) / (orders.Sum(x => x.SumNumber) * 1000);
                    tolerance = tolerance < 0 ? (-1) * tolerance : tolerance;
                    isCheck = (double)tolerance > 0.01 ? true : false;
                }
            }
            catch (Exception ex)
            {
                log.Error($"====CheckToleranceLimit====={ex.StackTrace}");
            }
            return isCheck;
        }
        public bool CheckToleranceLimitXiRoi(List<tblStoreOrderOperating> orders, int weight)
        {
            bool isCheck = false;
            try
            {
                if (orders.FirstOrDefault().NameProduct.ToUpper().Contains("RỜI"))
                {
                    var tolerance = (weight - orders.FirstOrDefault(x=>x.WeightIn > 0).WeightIn - orders.Sum(x=>x.SumNumber) * 1000) / (orders.Sum(x => x.SumNumber) * 1000);
                    tolerance = tolerance < 0 ? (-1) * tolerance : tolerance;
                    isCheck = (double)tolerance > 0.1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                log.Error($"====CheckToleranceLimitXiRoi====={ex.StackTrace}");
            }
            return isCheck;
        }
        public bool CheckToleranceLimitClinker(List<tblStoreOrderOperating> orders, int weight)
        {
            bool isCheck = false;
            try
            {
                if (orders.FirstOrDefault().NameProduct.ToUpper().Contains("CLINKER")) //Clinker 
                {
                    var tolerance = (weight - orders.FirstOrDefault(x => x.WeightIn > 0).WeightIn - orders.Sum(x => x.SumNumber) * 1000) / (orders.Sum(x => x.SumNumber) * 1000);
                    tolerance = tolerance < 0 ? (-1) * tolerance : tolerance;
                    isCheck = (double)tolerance > 0.3 ? true : false;
                }
            }
            catch (Exception ex)
            {
                log.Error($"====CheckToleranceLimitXiRoi====={ex.StackTrace}");
            }
            return isCheck;
        }
        public bool CheckIsAutoScale()
        {
            bool IsAutoScale = false;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var config = db.tblConfigOperatings.FirstOrDefault(x=>x.Code == "IsAutoScale");
                    if (config.Value == 1) IsAutoScale = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return IsAutoScale;
        }
    }
}
