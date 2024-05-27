using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMXHTD.Data.DataEntity;
using HMXHTD.Data.Models;
using HMXHTD.Services.Models;
using Oracle.ManagedDataAccess.Client;

namespace HMXHTD.Services.Services
{
    public interface IStoreOrderOperatingService : IBaseService<tblStoreOrderOperating>
    {
        List<tblStoreOrderOperating> GetTopStoreOrderOperating(int topX);
        int CountStoreOrderWaitingIntoTroughByType(string typeProduct);
        tblStoreOrderOperating GetByDeliveryCode(string deliveryCode);
        tblStoreOrderOperating GetStoreOrderPrepareIntoTroughByType(List<string> typeProducts);
        tblStoreOrderOperating GetStoreOrderPrepareIntoTroughAll();
        bool UpdateStepIntoTroughByOrderId(int orderId, int step);
        void UpdateStepIntoTrough(string typeProduct, int topX);
        bool UpdateBillOrderConfirm1(string cardNo);
        bool UpdateBillOrderConfirm2(string cardNo);
        bool UpdateBillOrderConfirm3(string cardNo);
        bool UpdateBillOrderConfirm4(string cardNo);
        bool UpdateBillOrderConfirm5(string cardNo);
        bool UpdateBillOrderConfirm6(string cardNo);
        bool UpdateBillOrderConfirm7(string cardNo);
        bool UpdateBillOrderConfirm7And8(string cardNo);
        bool UpdateBillOrderConfirm8(string cardNo);
        void ReIndexOrder(string cardNo);
        void ReIndexOrderByTypeProduct(string typeProduct);
        void UpdateIndexOrderForNewConfirm(string cardNo);
        void ReIndexOrderWhenSyncOrderWithConfirm(int orderId);
        void ReIndexOrderWhenSyncOrderWithEnd(int orderId);
        void ReIndexOrderWhenIgnoreOrder(int orderId);
        List<StoreOrderForLED12> GetStoreOrderForLED12();
        List<StoreOrderForLEDTroughModel> GetStoreOrderForLEDMainTrough();
        int GetStepByCardNo(string cardNo);
        tblStoreOrderOperating GetCurrentOrderByCardNoReceiving(string cardNo);
        List<tblStoreOrderOperating> GetAllOrderReceivingByCardNo(string cardNo);
        string GetCardNoByDeliveryCode(string deliverycode);
        List<tblStoreOrderOperating> GetAllOrderReceivingByDeliverycodeFirst(string deliverycode);
        bool UpdateOrderExpiredTime();
        bool UpdateOrdercompletedByDeliverycode(string deliveryCode);
        bool UpdateOrderDoneByDeliverycode(string deliveryCode);
        bool UpdateIsVoicedWhenSyncOrderByDeliverycode(string deliveryCode);
        OrderOracleScaleModel GetScaleInfoByDeliveryCode(string deliveryCode);
        bool CancelOrderOverTime();
        int CountVehicleBetweenGetwayAndScale();
        void UpdateScaleOrderSuccessedByDeliveryCode(string deliveryCode, bool scaleIn, bool status);
        void UpdateShiftOrderByDeliveryCode(string deliveryCode);
        void UpdateWeightERPByDeliveryCode(string deliveryCode);
    }
    public class StoreOrderOperatingService : BaseService<tblStoreOrderOperating>, IStoreOrderOperatingService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
     (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public StoreOrderOperatingService()
        {
        }

        public List<tblStoreOrderOperating> GetTopStoreOrderOperating(int topX)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                //return db.Database.SqlQuery<StoreOrderOperatingService>(@" SELECT TOP 8 P.* FROM dbo.ProductBrand AS P WITH (NOLOCK)
                //  INNER JOIN dbo.ProductBrandBlockProductBrand AS BBP WITH (NOLOCK) ON BBP.ProductBrand_id = P.ProductBrand_ID
                //  INNER JOIN dbo.ProductBrandBlock AS BB WITH (NOLOCK) ON BBP.ProductBrandBlock_id = BB.ProductBrandBlock_id
                //  WHERE BB.Name = @BlockName", new SqlParameter("@BlockName", blockName)).ToList();
                var storeOrderOperating = db.tblStoreOrderOperatings.Take(topX).ToList();
                return storeOrderOperating;
            }
        }
        public int CountStoreOrderWaitingIntoTroughByType(string typeProduct)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlCount = "SELECT COUNT(DISTINCT Vehicle) FROM dbo.tblStoreOrderOperating WHERE Step IN (2,3,4,5) AND IndexOrder2 = 0 AND TypeProduct = @TypeProduct";
                var count = db.Database.SqlQuery<int>(sqlCount, new SqlParameter("@TypeProduct", typeProduct)).Single();
                return count;
            }
        }
        public tblStoreOrderOperating GetByDeliveryCode(string deliveryCode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var order = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode.Equals(deliveryCode));
                return order;
            }
        }
        public tblStoreOrderOperating GetStoreOrderPrepareIntoTroughByType(List<string> typeProducts)
        {
            var order = new tblStoreOrderOperating();
            List<int> listStep = new List<int>() { 0, 1, 4 };
            using (var db = new HMXuathangtudong_Entities())
            {
              order = db.tblStoreOrderOperatings.Where(x => x.IndexOrder > 0 && (x.IndexOrder2 ?? 0) == 0 && listStep.Contains((int)x.Step) && typeProducts.Contains(x.TypeProduct)).OrderBy(x => x.IndexOrder).FirstOrDefault();

            }
            return order;
        }
        public tblStoreOrderOperating GetStoreOrderPrepareIntoTroughAll()
        {
            var order = new tblStoreOrderOperating();
            List<int> listStep = new List<int>() { 0, 1, 4 };
            using (var db = new HMXuathangtudong_Entities())
            {
               var orderList = db.tblStoreOrderOperatings.Where(x => x.IndexOrder > 0 && (x.IndexOrder2 ?? 0) == 0 && listStep.Contains((int)x.Step)).OrderBy(x => x.IndexOrder).ToList();

            }
            return order;
        }
        public bool UpdateStepIntoTroughByOrderId(int orderId, int step)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Step = @Step WHERE OrderId = @OrderId AND ISNULL(Step,0) <> 4";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@Step", step), new SqlParameter("@OrderId", orderId));
                return updateResponse > 0;
            }
        }
        public void UpdateStepIntoTrough(string typeProduct, int topX)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 1 && x.TypeProduct.Equals(typeProduct)).OrderBy(x => x.IndexOrder).Take(topX).ToList();
                foreach (var order in orders)
                {
                    var sqlUpdate = "UPDATE tblStoreOrderOperating SET Step =  4 WHERE OrderId = @OrderId AND ISNULL(Step,0) <> 4";
                    var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@OrderId", order.OrderId));
                }
            }
        }
        public bool UpdateBillOrderConfirm1(string cardNo)
        {
            bool res = false;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => x.CardNo == cardNo && (x.Step ?? 0) == 0 && (x.IndexOrder2 ?? 0) == 0 && (x.DriverUserName ?? "") != "").ToList();
                    if (orders.Count < 1) return false;
                    // lấy đơn có ưu tiên
                    var priorities = db.tblStoreOrderOperatingPriorities.OrderBy(x => x.Priority).ToList();
                    var ordersFist = new tblStoreOrderOperating();
                    var PriorityPCB30 = db.tblConfigOperatings.FirstOrDefault(x => x.Code == "IsPriorityPCB30");
                    if (PriorityPCB30.Value == 0)
                    {
                        if (orders.Count(x => x.TypeProduct == "PCB40") > 0)
                        ordersFist = orders.FirstOrDefault(x=>x.TypeProduct == "PCB40");
                    }
                    else
                    {
                        foreach (var priority in priorities)
                        {
                            if (orders.Count(x => x.TypeProduct == priority.TypeProduct) == 0) continue;
                            ordersFist = orders.FirstOrDefault();
                            break;
                        }
                    }
                    
                    if (ordersFist == null || ordersFist.Id < 1)
                    {
                        ordersFist = orders.FirstOrDefault();
                    }
                    var configLocation = db.tblConfigOperatings.FirstOrDefault(x => x.Code == "LocationXiGiong")?.ValueString;
                    foreach (var order in orders)
                    {
                        
                        string TypeProduct = "";
                        if (order.LocationCode.Contains(".XK") || configLocation.Contains(order.LocationCode))
                        {
                            if (!String.IsNullOrEmpty(order.LocationCode))
                            {
                                TypeProduct = "XK";
                                order.TypeProduct = TypeProduct;
                                db.SaveChanges();
                            }
                        }
                    }
                    var sqlUpdateOrder = $@"UPDATE  tblStoreOrderOperating
                                        SET     Confirm1 = 1 ,
                                                TimeConfirm1 = GETDATE() ,
                                                Step = 1 ,
                                                CountReindex = 0 ,
                                                TimeConfirmHistory = GETDATE() ,
                                                LogHistory = CONCAT(LogHistory, '#confirm by rfid at ',
                                                                    GETDATE()) ,
                                                LogProcessOrder = CONCAT(LogProcessOrder,
                                                                         N'#Xác thực bước 1 lúc ',
                                                                         FORMAT(GETDATE(),
                                                                                'dd/MM/yyyy HH:mm:ss'))
                                        WHERE   DeliveryCode = @DeliveryCode
                                                AND Step = 0
                                                AND ISNULL(IndexOrder2, 0) = 0
                                                AND ISNULL(DriverUserName, '') != ''";
                    var updateResponseOrder = db.Database.ExecuteSqlCommand(sqlUpdateOrder, new SqlParameter("@DeliveryCode", ordersFist.DeliveryCode));
                    if (updateResponseOrder > 0 && orders.Count > 1)
                    {
                        var sqlUpdateIndexOrder2 = $@"UPDATE tblStoreOrderOperating
                                                 SET    Confirm1 = 1 ,
                                                        TimeConfirm1 = GETDATE() ,
                                                        Step = 1 ,
                                                        IndexOrder2 = 1 ,
                                                        DeliveryCodeParent = @DeliveryCode ,
                                                        CountReindex = 0 ,
                                                        TimeConfirmHistory = GETDATE() ,
                                                        LogHistory = CONCAT(LogHistory, '#confirm by rfid at ', GETDATE()) ,
                                                        LogProcessOrder = CONCAT(LogProcessOrder, N'#Xác thực bước 1 lúc ',
                                                                                 FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss'))
                                                 WHERE  DeliveryCode <> @DeliveryCode
                                                        AND Vehicle = @Vehicle
                                                        AND ISNULL(Step, 0) = 0
                                                        AND ISNULL(DriverUserName, '') != ''";
                        db.Database.ExecuteSqlCommand(sqlUpdateIndexOrder2, new SqlParameter("@DeliveryCode", ordersFist.DeliveryCode), new SqlParameter("@Vehicle", ordersFist.Vehicle));
                    }
                    res =  updateResponseOrder > 0;
                }
            }
            catch (Exception ex)
            {
                LogError($@"UpdateBillOrderConfirm1, card no {cardNo}, {ex.Message}");
            }
            return res;
        }
        public bool UpdateBillOrderConfirm2(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm1 = 1 , Confirm2 = 1, TimeConfirm2 = getdate(), Step = 2, IndexOrder = 0, LogProcessOrder = CONCAT(LogProcessOrder, N'#Xác thực vào cổng lúc ', FORMAT(getdate(), 'dd/MM/yyyy hh:mm:ss')) WHERE CardNo = @CardNo AND Step IN (4)  AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm3(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm1 = 1 , Confirm2 = 1, TimeConfirm2 = ISNULL(TimeConfirm2, GETDATE()), Confirm3 = 1, TimeConfirm3 = getdate(), Confirm4 = 1, TimeConfirm4 = ISNULL(TimeConfirm4, GETDATE()) , Step = 3, IndexOrder = 0, LogProcessOrder = CONCAT(LogProcessOrder, N'#Cân vào lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')), WarningNotCall =  CASE WHEN Step = 1 THEN 1   WHEN Step = 0 THEN 1    ELSE 0 END WHERE CardNo = @CardNo AND Step IN (1,2,4) AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm4(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm1 = 4, TimeConfirm4 = getdate(), Step = 4 WHERE CardNo = @CardNo  AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm5(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm1 = 5, TimeConfirm5 = getdate(), Step = 5 WHERE CardNo = @CardNo  AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm6(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm1 = 6, TimeConfirm6 = getdate(), Step = 6 WHERE CardNo = @CardNo  AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm7(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm7 = 1, TimeConfirm7 = getdate(), Step = 7, IndexOrder = 0, LogProcessOrder = CONCAT(LogProcessOrder, N'#Cân ra lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) WHERE CardNo = @CardNo AND Step IN (5,6)  AND TimeConfirm3 < DATEADD(MINUTE , -10, GETDATE())  AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm7And8(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm7 = 1, TimeConfirm7 = getdate(), Confirm8 = 1, TimeConfirm8 = getdate(), Step = 8, IndexOrder = 0 WHERE CardNo = @CardNo AND Step IN (5,6)  AND ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public bool UpdateBillOrderConfirm8(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE tblStoreOrderOperating SET Confirm8 = 1, TimeConfirm8 = getdate(), Step = 8, LogProcessOrder = CONCAT(LogProcessOrder, N'#Xác thực ra cổng lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) WHERE CardNo = @CardNo AND Step >= 5 and Step < 8  AND  ISNULL(DriverUserName,'') != ''";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", cardNo));
                return updateResponse > 0;
            }
        }
        public void UpdateIndexOrderForNewConfirm(string cardNo)
        {
            try
            {
                var logProccess = "";
                using (var db = new HMXuathangtudong_Entities())
                {
                    var currentOrder = db.tblStoreOrderOperatings.Where(x => x.CardNo == cardNo && x.Step == 1 && (x.IndexOrder2 ?? 0) == 0).FirstOrDefault();
                    //logProccess += "Đơn đang xử lý: " + string.Join(",", orders.Select(x => x.Id).ToList());
                    logProccess += $@"Don dang xu ly: {currentOrder.Id} loai sp: {currentOrder.TypeProduct}";
                    if (currentOrder == null || currentOrder.IndexOrder > 0) return;
                    var orderIndexMax = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(currentOrder.TypeProduct)).Max(x => x.IndexOrder) ?? 0;
                    // log thêm các đơn cùng loại đã được xếp lốt
                    var orderReceivings = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(currentOrder.TypeProduct)).ToList();
                    logProccess += $@", Cac don duoc xep lot truoc do: ";
                    foreach (var orderReceiving in orderReceivings)
                    {
                        logProccess += $@"Order {orderReceiving.Id} - lot hien tai: {orderReceiving.IndexOrder} - loai sp: {orderReceiving.TypeProduct} - step: {orderReceiving.Step},";
                    }
                    var indexOrderSet = orderIndexMax + 1;
                    logProccess += $@", xep lot cho xe {indexOrderSet}";
                    currentOrder.IndexOrder = indexOrderSet;
                    currentOrder.IndexOrderTemp = indexOrderSet;
                    currentOrder.IndexOrder1 = indexOrderSet;
                    currentOrder.LogHistory = currentOrder.LogHistory + $@" #IndexOrder: {indexOrderSet}";
                    currentOrder.LogProcessOrder = currentOrder.LogProcessOrder + $@" #Xếp lốt : {indexOrderSet}";
                    db.SaveChanges();
                    LogInfo(logProccess);
                }
            }
            catch (Exception ex)
            {
                LogError($@"UpdateIndexOrderForNewConfirm with cardno {cardNo}, {ex.Message}");
            }
        }
        public void ReIndexOrder(string cardNo)
        {
            ProcessReIndexOrder("PCB40", cardNo);
            ProcessReIndexOrder("PCB30", cardNo);
            ProcessReIndexOrder("ROI", cardNo);
            ProcessReIndexOrder("CLINKER", cardNo);
        }
        public void ReIndexOrderByTypeProduct(string typeProduct)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.IndexOrder > 0 && x.TypeProduct.Equals(typeProduct)).OrderBy(x => x.IndexOrder).ToList();

                    int index = 0;
                    foreach (var order in orders)
                    {
                        index = index + 1;
                        UpdateIndexOrder((int)order.OrderId, index);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ProcessReIndexOrder(string typeProduct, string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var currentOrder = db.tblStoreOrderOperatings.Where(x => x.CardNo == cardNo && x.Confirm1 > 0 && x.Confirm8 != 1).FirstOrDefault();
                if (currentOrder.IndexOrder > 0) return;
                var orderIndexMax = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(typeProduct)).OrderBy(x => x.TimeConfirm1)?.Max(x=>x.IndexOrder) ?? 0;
                //var orderIndexMax = orders?.OrderByDescending(x => x.IndexOrder).FirstOrDefault()?.IndexOrder ?? 0;
                currentOrder.IndexOrder = orderIndexMax + 1;
                db.SaveChanges();
            }

            //var orders = new List<tblStoreOrderOperating>();
            //using (var db = new HMXuathangtudong_Entities())
            //{
            //    orders = db.tblStoreOrderOperatings.Where(x => (x.Confirm1 ?? 0) > 0 && (x.Confirm2 ?? 0) == 0 && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(typeProduct)).OrderBy(x => x.TimeConfirm1).ToList();
            //}
            //var orderIndexMax = orders.OrderByDescending(x => x.IndexOrder);

            //int index = 0;
            //foreach (var order in orders)
            //{
            //    index = index + 1;
            //    UpdateIndexOrder((int)order.OrderId, index);
            //}
        }
        public void UpdateIndexOrder(int orderId, int index)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.OrderId == orderId && (x.IndexOrder2 ?? 0) == 0);
                    if (orderExist == null) return;
                    orderExist.LogProcessOrder = orderExist.LogProcessOrder + $@" #Xếp lại lốt lúc {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}, lốt cũ: {orderExist.IndexOrder}, lốt mới: {index}";
                    orderExist.IndexOrder1 = orderExist.IndexOrder;
                    orderExist.IndexOrder = index;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ReIndexOrderWhenSyncOrderWithConfirm(int orderId)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var currentOrder = db.tblStoreOrderOperatings.FirstOrDefault(x=>x.OrderId == orderId);
                if (currentOrder == null || currentOrder.IndexOrder > 0) return;
                var orderIndexMax = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(currentOrder.TypeProduct)).OrderBy(x => x.TimeConfirm1)?.Max(x => x.IndexOrder) ?? 0;
                currentOrder.IndexOrder = orderIndexMax + 1;
                db.SaveChanges();
            }
        }
        public void ReIndexOrderWhenSyncOrderWithEnd(int orderId)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var currentOrder = db.tblStoreOrderOperatings.FirstOrDefault(x => x.OrderId == orderId);
                    var orders = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.IndexOrder > 0 && x.TypeProduct.Equals(currentOrder.TypeProduct)).OrderBy(x => x.IndexOrder).ToList();

                    int index = 0;
                    foreach (var order in orders)
                    {
                        index = index + 1;
                        UpdateIndexOrder((int)order.OrderId, index);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        public void ReIndexOrderWhenIgnoreOrder(int orderId)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var currentOrder = db.tblStoreOrderOperatings.FirstOrDefault(x=>x.OrderId == orderId);
                var orders = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.IndexOrder > 0 && x.TypeProduct.Equals(currentOrder.TypeProduct)).OrderBy(x => x.IndexOrder).ToList();
                var ordersHasIndexHighThanCurrent = orders.Where(x=>x.IndexOrder > currentOrder.IndexOrder).ToList();
                if(ordersHasIndexHighThanCurrent.Count > 5)
                {

                }
                else
                {

                }
            }
        }

        public List<StoreOrderForLED12> GetStoreOrderForLED12()
        {
            var orders = new List<StoreOrderForLED12>();
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlSelect = "SELECT TOP 12 B.Id, B.IndexOrder, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar), N'0', N'Chưa xác thực'), N'1', N'Chờ vào cổng'), N'2', N'Đã vào cổng'), N'3', N'Đã cân vào'), N'4', N'Chờ loa gọi'), N'5', N'Đang lấy hàng'), N'6', N'Đã lấy hàng') AS State1, Vehicle, DriverName, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE Step IN(0, 1, 4) AND B.IndexOrder > 0 AND ISNULL(IndexOrder2,0) = 0 ORDER BY B.Step DESC , B.IndexOrder ASC";
                    orders = db.Database.SqlQuery<StoreOrderForLED12>(sqlSelect).ToList();
                    return orders;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return orders;
        }
        public List<StoreOrderForLEDTroughModel> GetStoreOrderForLEDMainTrough()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = "SELECT DISTINCT top 20 Vehicle, Step FROM dbo.tblStoreOrderOperating WHERE Step IN (3,5) AND TypeProduct != 'CLINKER' AND TypeProduct != 'ROI' AND TroughLineCode IS NULL";
                var orders = db.Database.SqlQuery<StoreOrderForLEDTroughModel>(sqlSelect).ToList();
                return orders;
            }
        }
        //
        public int GetStepByCardNo(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var order = db.tblStoreOrderOperatings.OrderByDescending(x => x.Id).FirstOrDefault(x => x.CardNo == cardNo && (x.IndexOrder2 ?? 0) == 0);
                return order?.Step ?? 0;
            }
        }
        public tblStoreOrderOperating GetCurrentOrderByCardNoReceiving(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var order = db.tblStoreOrderOperatings.OrderByDescending(x => x.Id).FirstOrDefault(x => x.CardNo == cardNo && (x.IndexOrder2 ?? 0) == 0 && (x.DriverUserName ?? "") != ""  && x.Step < 8);
                return order;
            }
        }
        public List<tblStoreOrderOperating> GetAllOrderReceivingByCardNo(string cardNo)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var order = db.tblStoreOrderOperatings.Where(x => x.CardNo == cardNo && (x.DriverUserName ?? "") != "" && x.Step < 8 && x.Step > 0).ToList();
                return order;
            }
        }
        public string GetCardNoByDeliveryCode(string deliverycode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var order = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliverycode);
                return order.CardNo;
            }
        }
        public List<tblStoreOrderOperating> GetAllOrderReceivingByDeliverycodeFirst(string deliverycode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = $@"SELECT TOP 100 * FROM dbo.tblStoreOrderOperating WHERE Step > 0 AND Step < 8 AND ISNULL(DriverUserName, '') != '' AND (CardNo = (SELECT TOP 1 CardNo FROM dbo.tblStoreOrderOperating WHERE DeliveryCode = @DeliveryCode))";
                var orders = db.Database.SqlQuery<tblStoreOrderOperating>(sqlSelect, new SqlParameter("@DeliveryCode", deliverycode)).ToList();
                return orders;
            }
        }
        public bool UpdateOrderExpiredTime()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var timeAfter = DateTime.Now.AddHours(-96);
                var sqlUpdate = $@"UPDATE dbo.tblStoreOrderOperating
                                SET
                                Step = 9,
                                Confirm1 = 1,
                                TimeConfirm1 = ISNULL(TimeConfirm1, GETDATE()),
                                Confirm2 = 1,
                                TimeConfirm2 = ISNULL(TimeConfirm2, GETDATE()),
                                Confirm3 = 1,
                                TimeConfirm3 = ISNULL(TimeConfirm3, GETDATE()),
                                Confirm4 = 1,
                                TimeConfirm4 = ISNULL(TimeConfirm4, GETDATE()),
                                Confirm5 = 1,
                                TimeConfirm5 = ISNULL(TimeConfirm5, GETDATE()),
                                Confirm6 = 1,
                                TimeConfirm6 = ISNULL(TimeConfirm6, GETDATE()),
                                Confirm7 = 1,
                                TimeConfirm7 = ISNULL(TimeConfirm7, GETDATE()),
                                Confirm8 = 1,
                                TimeConfirm8 = ISNULL(TimeConfirm8, GETDATE()),
                                Confirm9 = 1,
                                TimeConfirm9 = ISNULL(TimeConfirm9, GETDATE()),
                                IndexOrder = 0,
                                IsVoiced = 1,
                                NoteFinish = NoteFinish + N' Hệ thống tự hủy' 
                                WHERE OrderDate<@TimeAfter AND Step <= 8";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@TimeAfter", timeAfter));
                return updateResponse > 0;
            }
        }
        public bool UpdateOrdercompletedByDeliverycode(string deliveryCode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Step = 8, Confirm1 = 1, Confirm2 = 1, Confirm3 = 1, Confirm4 = 1, Confirm5 = 1, Confirm6 = 1, Confirm7 = 1, Confirm8 = 1, IndexOrder = 0 WHERE DeliveryCode = @DeliveryCode";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@DeliveryCode", deliveryCode));
                return updateResponse > 0;
            }
        }
        public bool UpdateOrderDoneByDeliverycode(string deliveryCode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Step = 9, Confirm1 = 1, Confirm2 = 1, Confirm3 = 1, Confirm4 = 1, Confirm5 = 1, Confirm6 = 1, Confirm7 = 1, Confirm8 = 1, Confirm9 = 1, IndexOrder = 0 WHERE DeliveryCode = @DeliveryCode";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@DeliveryCode", deliveryCode));
                return updateResponse > 0;
            }
        }
        public bool UpdateIsVoicedWhenSyncOrderByDeliverycode(string deliveryCode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET IndexOrder = 0, Step = 9, TimeConfirm9 = GETDATE() , NoteFinish = N'Hủy', State = 'VOIDED', IsVoiced = 1, DriverUserName = '' WHERE DeliveryCode = @DeliveryCode AND IsVoiced != 1";
                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@DeliveryCode", deliveryCode));
                return updateResponse > 0;
            }
        }
        public OrderOracleScaleModel GetScaleInfoByDeliveryCode(string deliveryCode)
        {
            var orderModel = new OrderOracleScaleModel();
            try
            {
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                double weightNull = 0;
                double weightFull = 0;
                DateTime timeIn;
                DateTime timeOut;
                sqlQuery = $@"select so.*, cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL,cvw.ITEMNAME as ITEM_NAME, cvw.TIMEIN, cvw.TIMEOUT  from sales_orders so
                         ,cx_vehicle_weight cvw 
                         where so.delivery_code = cvw.delivery_code 
                         and so.DELIVERY_CODE = :DELIVERY_CODE";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                    Cmd.Parameters.Add(new OracleParameter("DELIVERY_CODE", deliveryCode));
                    connection.Open();
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            Double.TryParse(Rd["LOADWEIGHTNULL"]?.ToString(), out weightNull);
                            Double.TryParse(Rd["LOADWEIGHTFULL"]?.ToString(), out weightFull);
                            DateTime.TryParse(Rd["TIMEIN"]?.ToString(), out timeIn);
                            DateTime.TryParse(Rd["TIMEOUT"]?.ToString(), out timeOut);
                            orderModel.ORDER_ID = Int32.Parse(Rd["ORDER_ID"].ToString());
                            orderModel.STATUS = Rd["STATUS"].ToString();
                            orderModel.ORDER_QUANTITY = Double.Parse(Rd["ORDER_QUANTITY"].ToString());
                            orderModel.DRIVER_NAME = Rd["DRIVER_NAME"].ToString();
                            orderModel.VEHICLE_CODE = Rd["VEHICLE_CODE"].ToString();
                            orderModel.MOOC_CODE = Rd["MOOC_CODE"].ToString();
                            orderModel.DELIVERY_CODE = Rd["DELIVERY_CODE"].ToString();
                            orderModel.CUSTOMER_ID = Int32.Parse(Rd["CUSTOMER_ID"].ToString());
                            orderModel.BOOK_QUANTITY = Double.Parse(Rd["BOOK_QUANTITY"].ToString());
                            orderModel.PRINT_STATUS = Rd["PRINT_STATUS"].ToString();
                            orderModel.LOCATION_CODE = Rd["LOCATION_CODE"].ToString();
                            orderModel.AREA_ID = Int32.Parse(Rd["AREA_ID"].ToString());
                            orderModel.ITEM_NAME = Rd["ITEM_NAME"]?.ToString();
                            orderModel.TIMEIN = timeIn;
                            orderModel.TIMEOUT = timeOut;
                            orderModel.WEIGHTNULL = weightNull;
                            orderModel.WEIGHTFULL = weightFull;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($@"GetScaleInfoByDeliveryCode {ex.Message}");   
            }
            return orderModel;
        }
        public bool CancelOrderOverTime()
        {
            bool res = false;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    res = db.Database.ExecuteSqlCommand("uspOperatingCancelOrderOverTime") > 0;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return res;
        }
        public int CountVehicleBetweenGetwayAndScale()
        {
            int res = 0;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    SqlParameter returnCode = new SqlParameter("@countvehicle", SqlDbType.Int);
                    returnCode.Direction = ParameterDirection.Output;
                    db.Database.ExecuteSqlCommand("uspOperatingCountVehicleBetweenGetwayAndTrough @countvehicle OUT", returnCode);
                    Int32.TryParse(returnCode.Value?.ToString(), out res);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return res;
        }
        public void UpdateScaleOrderSuccessedByDeliveryCode(string deliveryCode, bool scaleIn, bool status)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET AutoScaleOut = 1 WHERE DeliveryCode = @DeliveryCode";
                    var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@DeliveryCode", deliveryCode)); 
                }
            }
            catch (Exception ex)
            {
                 
            }
        }
        public void UpdateShiftOrderByDeliveryCode(string deliveryCode)
        {
            try
            {
                var hour = DateTime.Now.Hour;
                var shift = 1;
                if(hour >= 6 && hour < 14)
                {
                    shift = 1;
                }else if(hour >= 14 && hour < 22)
                {
                    shift = 2;
                }
                else
                {
                    shift = 3;
                }
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Shifts = @Shifts WHERE DeliveryCode = @DeliveryCode";
                    var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@Shifts", shift), new SqlParameter("@DeliveryCode", deliveryCode));
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateWeightERPByDeliveryCode(string deliveryCode)
        {
            try
            {
                var orderDetails = GetScaleInfoByDeliveryCode(deliveryCode);
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET WeightIn = @WeightIn, WeightOut = @WeightOut WHERE DeliveryCode = @DeliveryCode";
                    var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@WeightIn", orderDetails.WEIGHTNULL * 1000), new SqlParameter("@WeightOut", orderDetails.WEIGHTFULL * 1000), new SqlParameter("@DeliveryCode", deliveryCode));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
