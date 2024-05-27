using HMXHTD.Core;
using HMXHTD.Models;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public class BillOrder
    {
        #region BillOrderOperating

        #region method setDataBillOrderOperating
        public int setDataBillOrderOperating(int OrderId, DateTime OrderDate, string Vehicle, string DriverName, string NameDistributor, string ItemId, string NameProduct, string CatId, decimal SumNumber, string DeliveryCode, string State)
        {
            int tmpValue = 0;
            try
            {
                var vehicle = Vehicle.Replace("-", "").Replace("  ", "").Replace(" ", "").Replace("/", "").ToUpper();
                var CardNo = DataUnit.GetFieldValue("tblRFID","Code", @"Vehicle = '"+vehicle+"'");
                string TypeProduct = "";
                if (NameProduct.ToUpper().Contains("RỜI"))
                {
                    TypeProduct = "ROI";
                }
                else if (NameProduct.ToUpper().Contains("PCB30") || NameProduct.ToUpper().Contains("MAX PRO"))
                {
                    TypeProduct = "PCB30";
                }
                else if (NameProduct.ToUpper().Contains("PCB40"))
                {
                    TypeProduct = "PCB40";
                }
                string SQL = "IF NOT EXISTS (SELECT * FROM tblStoreOrderOperating WHERE DeliveryCode = @DeliveryCode) ";
                SQL += "BEGIN INSERT INTO tblStoreOrderOperating(OrderId,Vehicle,DriverName,NameDistributor,ItemId,NameProduct,CatId,CardNo,SumNumber,DeliveryCode,TypeProduct,OrderDate,State) VALUES(@OrderId,@Vehicle,@DriverName,@NameDistributor,@ItemId,@NameProduct,@CatId,@CardNo,@SumNumber,@DeliveryCode,@TypeProduct,@OrderDate,@State) END ";
                SQL += "ELSE UPDATE tblStoreOrderOperating SET State = @State WHERE DeliveryCode = @DeliveryCode ";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
               
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("OrderId", SqlDbType.Int).Value = OrderId;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = vehicle;
                Cmd.Parameters.Add("DriverName", SqlDbType.NVarChar).Value = DriverName;
                Cmd.Parameters.Add("NameDistributor", SqlDbType.NVarChar).Value = NameDistributor;
                Cmd.Parameters.Add("ItemId", SqlDbType.NVarChar).Value = ItemId;
                Cmd.Parameters.Add("NameProduct", SqlDbType.NVarChar).Value = NameProduct;
                Cmd.Parameters.Add("CatId", SqlDbType.NVarChar).Value = CatId;
                if (String.IsNullOrEmpty(CardNo))
                {
                    Cmd.Parameters.Add("CardNo", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    Cmd.Parameters.Add("CardNo", SqlDbType.NVarChar).Value = CardNo;
                }
                Cmd.Parameters.Add("SumNumber", SqlDbType.Decimal).Value = SumNumber;
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                Cmd.Parameters.Add("OrderDate", SqlDbType.DateTime).Value = OrderDate;
                Cmd.Parameters.Add("State", SqlDbType.NVarChar).Value = State; 
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();

                //this.setBillOrderOperatingByDeliveryCode(DeliveryCode.Trim(), 2, 0);
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method getBillOrderOperating
        // lấy danh sách đơn hàng với trạng thái khác 8(chưa hoàn thành việc lấy hàng ra khỏi nhà máy)
        // với indexorder2 = 0
        public DataTable getBillOrderOperating(string SearchKey, string TypeProduct, string Step)
        {
            string SQL_SEARCH = "", SQL_TypeProduct = "", SQL_Step = "";
            DataTable objTable = new DataTable();
            var timeStart = DateTime.Now.AddDays(-5);
            try
            {
                if (TypeProduct.Trim() != "Xem theo chủng loại")
                {
                    SQL_TypeProduct = "AND B.TypeProduct = @TypeProduct ";
                }

                if (Step.Trim() != "Xem theo trạng thái")
                {
                    if (Step == "Đã nhận đơn")
                    {
                        SQL_Step = "AND B.Step = 0 AND ISNULL(DriverUserName,N'') <> N'' ";
                    }
                    else if (Step == "Chưa xác thực")
                    {
                        SQL_Step = "AND B.Step = 0 ";
                    }
                    else if (Step == "Đã xác thực")
                    {
                        SQL_Step = "AND B.Step = 1 ";
                    }
                    else if (Step == "Đã vào cổng")
                    {
                        SQL_Step = "AND B.Step = 2 ";
                    }
                    else if (Step == "Đã cân vào")
                    {
                        SQL_Step = "AND B.Step = 3 ";
                    }
                    else if (Step == "Mời xe vào")
                    {
                        SQL_Step = "AND B.Step = 4 ";
                    }
                    else if (Step == "Đang lấy hàng")
                    {
                        SQL_Step = "AND B.Step = 5 ";
                    }
                    else if (Step == "Đã lấy hàng")
                    {
                        SQL_Step = "AND B.Step = 6 ";
                    }
                    else if (Step == "Đã cân ra")
                    {
                        SQL_Step = "AND B.Step = 7 ";
                    }
                    else if (Step == "Đã hoàn thành")
                    {
                        SQL_Step = "AND B.Step = 8 ";
                    }
                }

                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (B.DeliveryCode LIKE N'%'+@SearchKey+'%'  OR B.DriverName LIKE N'%'+@SearchKey+'%' OR B.Vehicle LIKE N'%'+@SearchKey+'%' OR B.NameDistributor LIKE N'%'+@SearchKey+'%' OR B.NameProduct LIKE N'%'+@SearchKey+'%') ";
                    timeStart = DateTime.Now.AddMonths(-1);
                }
                //string SQLQUERY = "SELECT TOP 200 1000 as tmpOrder, B.LockInDbet, B.Id, B.CardNo, B.DeliveryCode AS IDOrderSyn, NameStore, CONVERT(varchar, B.OrderDate, 103) AS DayCreate , ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm1)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm1)),2) AS nvarchar)),N'') AS TimeConfirm1, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm2)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm2)),2) AS nvarchar)),N'') AS TimeConfirm2, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm3)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm3)),2) AS nvarchar)),N'') AS TimeConfirm3, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm4)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm4)),2) AS nvarchar)),N'') AS TimeConfirm4, B.Step, B.Confirm1, B.Confirm2, B.Confirm3, B.Confirm4, CASE WHEN IndexOrder > 0 THEN CAST(ISNULL(B.IndexOrder,0) AS nvarchar) ELSE REPLACE(CAST(ISNULL(B.IndexOrder,0) AS nvarchar),N'0',N'') END AS IndexOrder, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng'),N'7',N'Đã cân ra'),N'8',N'Đã hoàn thành') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1, B.DriverAccept, B.TimeConfirm5, B.TimeConfirm6, B.TimeConfirm7, B.TimeConfirm8, B.TimeConfirm9, B.DriverUserName FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND B.Step NOT IN (9) AND ISNULL(IndexOrder2,0) = 0 " + SQL_SEARCH + SQL_TypeProduct + SQL_Step + " ORDER BY B.DayCreate DESC, B.TimeConfirm1 DESC";
                string SQLQUERY = "SELECT TOP 200 1000 as tmpOrder, B.LockInDbet, B.Id, B.CardNo, B.DeliveryCode AS IDOrderSyn, NameStore, CONVERT(varchar, B.OrderDate, 103) AS DayCreate , ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm1)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm1)),2) AS nvarchar)),N'') AS TimeConfirm1, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm2)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm2)),2) AS nvarchar)),N'') AS TimeConfirm2, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm3)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm3)),2) AS nvarchar)),N'') AS TimeConfirm3, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm4)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm4)),2) AS nvarchar)),N'') AS TimeConfirm4, B.Step, B.Confirm1, B.Confirm2, B.Confirm3, B.Confirm4, CASE WHEN IndexOrder > 0 THEN CAST(ISNULL(B.IndexOrder,0) AS nvarchar) ELSE REPLACE(CAST(ISNULL(B.IndexOrder,0) AS nvarchar),N'0',N'') END AS IndexOrder, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng'),N'7',N'Đã cân ra'),N'8',N'Đã hoàn thành') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1, B.DriverAccept, B.TimeConfirm5, B.TimeConfirm6, B.TimeConfirm7, B.TimeConfirm8, B.TimeConfirm9, B.DriverUserName, B.LogProcessOrder, B.WarningNotCall FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND B.Step NOT IN (9)  " + SQL_SEARCH + SQL_TypeProduct + SQL_Step + " ORDER BY B.DayCreate DESC, B.TimeConfirm1 DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = timeStart; 
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = DateTime.Now;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    if (objTable.Rows[i]["IndexOrder"].ToString() != "")
                    {
                        objTable.Rows[i]["tmpOrder"] = objTable.Rows[i]["IndexOrder"].ToString();
                    }
                }
                objTable.DefaultView.Sort = "[tmpOrder] ASC";
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method setBillOrderOperating
        public int setBillOrderOperating(int OrderId)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "IF NOT EXISTS (SELECT * FROM tblStoreOrderOperating WHERE OrderId = @OrderId) ";
                SQL += "INSERT INTO tblStoreOrderOperating(OrderId,Step) VALUES(@OrderId,0)";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("OrderId", SqlDbType.Int).Value = OrderId;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method getIdByDeliveryCode
        public int getIdByDeliveryCode(string DeliveryCode, ref string TypeProduct)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQLQUERY = "SELECT * FROM tblStoreOrderOperating WHERE DeliveryCode = @DeliveryCode";
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.CommandText = SQLQUERY;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = int.Parse(Rd["Id"].ToString());
                    TypeProduct = Rd["TypeProduct"].ToString();
                }
                Rd.Close();
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method getDeliveryCodeByCardNo
        public string getDeliveryCodeByCardNo(string CardNo, ref int CountItem)
        {
            string DeliveryCode = "";
            try
            {
                string SQLQUERY = "SELECT DeliveryCode FROM tblStoreOrderOperating WHERE CardNo = @CardNo AND ISNULL(Step,0) = 0 ORDER BY Prioritize DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("CardNo", SqlDbType.NVarChar).Value = CardNo;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    if (DeliveryCode == "")
                    {
                        DeliveryCode = Rd["DeliveryCode"].ToString();
                    }
                    CountItem = CountItem + 1;
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return DeliveryCode;
        }
        #endregion

        #region method getBillOrderByVehicle
        public DataTable getBillOrderByVehicle(string Vehicle)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT *, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng'),N'7',N'Đã cân ra'),N'8',N'Đã hoàn thành') AS State1 FROM [tblStoreOrderOperating] WHERE Vehicle = @Vehicle AND Step NOT IN (8,9) ORDER BY Prioritize";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getBillOrderByVehicleV1
        public DataTable getBillOrderByVehicleV1(string Vehicle)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM [tblStoreOrderOperating] WHERE Vehicle = @Vehicle AND Step IN (0,1,4) ORDER BY Prioritize";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getBillOrderByVehicleV2
        public DataTable getBillOrderByVehicleV2(string Vehicle)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM [tblStoreOrderOperating] WHERE Vehicle = @Vehicle AND Step IN (1,2,3,4,5,6,7) ORDER BY DeliveryCodeParent DESC";
                //string SQLQUERY = "SELECT t.*, v.TonnageDefault FROM [tblStoreOrderOperating] AS t LEFT JOIN dbo.tblVehicle AS v ON v.Vehicle = t.Vehicle WHERE t.Vehicle  = @Vehicle  AND Step IN (1,2,3,4,5,6,7) ORDER BY DeliveryCodeParent DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getTonnageDefaultByDeliveryCode
        public DataTable getTonnageDefaultByDeliveryCode(string Vehicle)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT TOP 1 * FROM dbo.tblVehicle WHERE Vehicle = @Vehicle AND TonnageDefault > 0";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method UpdatePrioritize
        public int UpdatePrioritize(string DeliveryCode, int Prioritize)
        {
            int tmpValue = 0;
            try
            {
                string SQL = " UPDATE tblStoreOrderOperating SET Prioritize = @Prioritize WHERE DeliveryCode = @DeliveryCode";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("Prioritize", SqlDbType.Int).Value = Prioritize;
                Cmd.CommandText = SQL;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
                if (tmpValue < 0)
                {
                    tmpValue = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tmpValue;
        }
        #endregion

        #region method resetBillOrderToDefault
        public int resetBillOrderToDefault(string DeliveryCode)
        {
            int tmpValue = 0;
            try
            {
                string SQL = " UPDATE tblStoreOrderOperating SET Confirm1 = 0, TimeConfirm1 = NULL, Confirm2 = 0, TimeConfirm2 = NULL, Confirm3 = 0, TimeConfirm3 = NULL, Confirm4 = 0, TimeConfirm4 = NULL, Confirm5 = 0, TimeConfirm5 = NULL, Confirm6 = 0, TimeConfirm6 = NULL, Confirm7 = 0, TimeConfirm7 = NULL, Confirm8 = 0, TimeConfirm8 = NULL, Confirm9 = 0, TimeConfirm9 = NULL, Confirm9Note = N'', Step = 0, IndexOrder = 0, IndexOrder1 = 0, IndexOrder2 = 0, Trough = 0, Trough1 = 0 WHERE DeliveryCode = @DeliveryCode";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.CommandText = SQL;
                //tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
                if (tmpValue < 0)
                {
                    tmpValue = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tmpValue;
        }
        #endregion

        #region method UpdateFinishTMP
        public int UpdateFinishTMP(string DeliveryCode)
        {
            int tmpValue = 0;
            try
            {
                string SQL = " UPDATE tblStoreOrderOperating SET Confirm8 = 1, TimeConfirm8 = getdate(), Step = 8 WHERE DeliveryCode = @DeliveryCode";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.CommandText = SQL;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
                if (tmpValue < 0)
                {
                    tmpValue = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tmpValue;
        }
        #endregion

        #endregion

        #region BillOrderConfirm

        #region method getBillOrderConfirm
        public DataTable getBillOrderConfirm(string DeliveryCode, string Vehicle, string SearchKey, string TypeProduct, string Step)
        {
            string SQL_DeliveryCode = "", SQL_Vehicle = "", SQL_DriverName = "", SQL_TypeProduct = "", SQL_Step = "";
            if (DeliveryCode.Trim() != "")
            {
                SQL_DeliveryCode = "AND B.DeliveryCode = @DeliveryCode ";
            }
            if (Vehicle.Trim() != "")
            {
                SQL_Vehicle = "AND B.Vehicle = @Vehicle ";
            }
            if (SearchKey.Trim() != "")
            {
                SQL_DriverName = "AND (B.DriverName LIKE N'%'+ @SearchKey +'%' OR B.NameProduct LIKE N'%'+ @SearchKey +'%' OR NameDistributor LIKE N'%'+ @SearchKey +'%') ";
            }
            if (TypeProduct.Trim() != "-/-")
            {
                SQL_TypeProduct = "AND B.TypeProduct = @TypeProduct";
            }
            if (Step.Trim() != "Xem theo trạng thái")
            {
                if (Step == "Chưa xác thực")
                {
                    SQL_Step = "AND B.Step = 0 ";
                }
                else if (Step == "Đã xác thực")
                {
                    SQL_Step = "AND B.Step = 1 ";
                }
                else if (Step == "Mời xe vào")
                {
                    SQL_Step = "AND B.Step = 4 ";
                }
            }
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT 1000 AS tmpOrder, B.Id, B.DeliveryCode AS IDOrderSyn, B.CardNo, '' AS NameStore, B.OrderDate AS DayCreate, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm1)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm1)),2) AS nvarchar)),N'') AS TimeConfirm1, B.Step, B.Confirm1, B.Confirm2, B.Confirm3, B.Confirm4, CASE WHEN IndexOrder > 0 THEN CAST(ISNULL(B.IndexOrder,0) AS nvarchar) ELSE REPLACE(CAST(ISNULL(B.IndexOrder,0) AS nvarchar),N'0',N'') END AS IndexOrder, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng'),N'7',N'Đã cân ra'),N'8',N'Đã hoàn thành') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND Step IN (0,1,4) AND ISNULL(IndexOrder2,0) = 0 "+ SQL_DeliveryCode + SQL_Vehicle + SQL_DriverName + SQL_TypeProduct + SQL_Step + " ORDER BY B.DayCreate DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-3);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = DateTime.Now;
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                //for (int i = 0; i < objTable.Rows.Count; i++)
                //{
                //    if (objTable.Rows[i]["IndexOrder"].ToString() != "")
                //    {
                //        objTable.Rows[i]["tmpOrder"] = objTable.Rows[i]["IndexOrder"].ToString();
                //    }
                //}
                //objTable.DefaultView.Sort = "[tmpOrder] ASC";
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #endregion

        #region BillOrderInOut

        #region method getBillOrderInOut
        public DataTable getBillOrderInOut(string SearchKey, string TypeProduct, string Step)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQL_SEARCH1 = "", SQL_TypeProduct = "", SQL_Step = "";

                if (TypeProduct.Trim() != "Xem theo chủng loại")
                {
                    SQL_TypeProduct = "AND B.TypeProduct = @TypeProduct ";
                }

                if (Step.Trim() != "Xem theo trạng thái")
                {
                    if (Step == "Đã nhận đơn")
                    {
                        SQL_Step = "AND B.Step = 0 AND ISNULL(DriverUserName,N'') <> N'' ";
                    }
                    else if (Step == "Chưa xác thực")
                    {
                        SQL_Step = "AND B.Step = 0 ";
                    }
                    else if (Step == "Đã xác thực")
                    {
                        SQL_Step = "AND B.Step = 1 ";
                    }
                    else if (Step == "Đã vào cổng")
                    {
                        SQL_Step = "AND B.Step = 2 ";
                    }
                    else if (Step == "Đã cân vào")
                    {
                        SQL_Step = "AND B.Step = 3 ";
                    }
                    else if (Step == "Mời xe vào")
                    {
                        SQL_Step = "AND B.Step = 4 ";
                    }
                    else if (Step == "Đang lấy hàng")
                    {
                        SQL_Step = "AND B.Step = 5 ";
                    }
                    else if (Step == "Đã lấy hàng")
                    {
                        SQL_Step = "AND B.Step = 6 ";
                    }
                    else if (Step == "Đã cân ra")
                    {
                        SQL_Step = "AND B.Step = 7 ";
                    }
                    else if (Step == "Đã hoàn thành")
                    {
                        SQL_Step = "AND B.Step = 8 ";
                    }
                }

                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH1 = " AND (B.DeliveryCode LIKE N'%'+@SearchKey+'%'  OR B.DriverName LIKE N'%'+@SearchKey+'%' OR B.Vehicle LIKE N'%'+@SearchKey+'%' OR B.NameDistributor LIKE N'%'+@SearchKey+'%' OR B.NameProduct LIKE N'%'+@SearchKey+'%') ";
                }

                string SQLQUERY = $@"SELECT TOP 100 1000 AS tmpOrder, B.Id, B.CardNo, B.DeliveryCode AS IDOrderSyn, NameStore, B.State, B.OrderDate AS DayCreate, ";
                SQLQUERY += "CASE ";
                SQLQUERY += "WHEN ISNULL(B.AutoScaleOut,0) = 0 THEN N'Cân tay' ";
                SQLQUERY += "WHEN ISNULL(B.AutoScaleOut,0) = 1 THEN N'Cân tự động' "; 
                SQLQUERY += "ELSE '' ";
                SQLQUERY += "END ";
                SQLQUERY += "AS AutoScaleOut, ";
                SQLQUERY += "B.TimeConfirm2, B.TimeConfirm8, B.Step, B.Confirm1, B.Confirm2,B.Confirm3, B.Confirm4, B.Confirm5, ";
                SQLQUERY += "B.Confirm6, B.Confirm7, B.Confirm8, CASE WHEN IndexOrder > 0 THEN CAST(ISNULL(B.IndexOrder, 0) AS NVARCHAR) ELSE REPLACE(CAST(ISNULL(B.IndexOrder, 0) AS NVARCHAR), N'0', N'') END AS IndexOrder, ";
                SQLQUERY += "REPLACE(CAST(ISNULL(Trough, 0) AS NVARCHAR), N'0', N'') AS Trough, ";
                SQLQUERY += "ISNULL(( CAST(RIGHT('0' + RTRIM(DATEPART(HOUR, TimeIn21)), 2) AS NVARCHAR) ";
                SQLQUERY += "+ ':' ";
                SQLQUERY += "+ CAST(RIGHT('0' + RTRIM(DATEPART(MINUTE, TimeIn21)), 2) AS NVARCHAR) ";
                SQLQUERY += "+ '-' ";
                SQLQUERY += "+ CAST(RIGHT('0' + RTRIM(DATEPART(HOUR, TimeIn22)), 2) AS NVARCHAR) ";
                SQLQUERY += "+ ':' ";
                SQLQUERY += "+ CAST(RIGHT('0' + RTRIM(DATEPART(MINUTE, TimeIn22)), 2) AS NVARCHAR) ), ";
                SQLQUERY += "N'') AS TIMEIN33, ";
                SQLQUERY += "CASE ";
                SQLQUERY += "WHEN Step = 0 THEN N'Chưa xác thực' ";
                SQLQUERY += "WHEN Step = 1 THEN N'Đã xác thực' ";
                SQLQUERY += "WHEN Step = 2 THEN N'Đã vào cổng' ";
                SQLQUERY += "WHEN Step = 3 THEN N'Đã cân vào' ";
                SQLQUERY += "WHEN Step = 4 THEN N'Mời xe vào' ";
                SQLQUERY += "WHEN Step = 5 THEN N'Đang lấy hàng' ";
                SQLQUERY += "WHEN Step = 6 THEN N'Đã lấy hàng' ";
                SQLQUERY += "WHEN Step = 7 THEN N'Đã cân ra' ";
                SQLQUERY += "WHEN Step = 8 THEN N'Đã hoàn thành' ";
                SQLQUERY += "WHEN Step = 9 THEN N'Đã giao hàng' ";
                SQLQUERY += "ELSE '' ";
                SQLQUERY += "END ";
                SQLQUERY += "AS State1, ";
                SQLQUERY += "B.Vehicle, ";
                SQLQUERY += "DriverName, ";
                SQLQUERY += "SumNumber, ";
                SQLQUERY += "NameDistributor, ";
                SQLQUERY += "NameProduct, ";
                SQLQUERY += "B.IndexOrder1, ";
                SQLQUERY += "ISNULL(B.WarningNotCall,0) AS WarningNotCall, ";
                SQLQUERY += "B.WeightIn, ";
                SQLQUERY += "B.WeightOut, ";
                SQLQUERY += "B.WeightInTime, ";
                SQLQUERY += "B.WeightOutTime, ";
                SQLQUERY += "ROUND(B.WeightOut - B.WeightIn,1,1) as WeightReal, ";
                SQLQUERY += "CAST( 100 - (B.WeightOut - B.WeightIn)*100/(B.SumNumber * 1000) AS decimal(10,2))   as Tolerances ";
                SQLQUERY += "FROM tblStoreOrderOperating B  ";
                SQLQUERY += "WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 ";
                SQLQUERY += "AND Step >= 0 AND ISNULL(B.DriverUserName, '') != '' " + SQL_SEARCH1 + SQL_TypeProduct + SQL_Step + " ORDER BY B.IndexOrder1 ASC, B.TimeConfirm1 DESC;";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-3);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = DateTime.Now;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    if (objTable.Rows[i]["IndexOrder"].ToString() != "")
                    {
                        objTable.Rows[i]["tmpOrder"] = objTable.Rows[i]["IndexOrder"].ToString();
                    }
                }
                objTable.DefaultView.Sort = "[tmpOrder] ASC";
            }
            catch (Exception Ex)
            {

            }
            return objTable;
        }
        #endregion

        #region method setInOutVoice
        public int setInOutVoice(int OrderId, string VoiceText, ref string fileName)
        {
            int tmpValue = 0;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "IF NOT EXISTS (SELECT * FROM tblStoreOrderOperatingVoice WHERE OrderId = @OrderId AND Step = 4 AND IndexNumber = 5) BEGIN INSERT INTO tblStoreOrderOperatingVoice(OrderId,Step,VoiceText) VALUES(@OrderId,2,@VoiceText) END";
                Cmd.Parameters.Add("OrderId", SqlDbType.Int).Value = OrderId;
                Cmd.Parameters.Add("VoiceText", SqlDbType.NVarChar).Value = VoiceText;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();

                if (tmpValue > 0)
                {
                    fileName = "";
                    var client = new RestClient("https://tts.mobifone.ai/");

                    var request = new RestRequest("api/tts");

                    RequestTTS requestTTS = new RequestTTS();
                    requestTTS.app_id = "e846648fee1d15106e1b601e";
                    requestTTS.key = "VPv7P0TxnFmRgRYY";
                    //requestTTS.voice = "hn_male_xuantin_vdts_48k-hsmm";
                    //requestTTS.voice = "hn_female_xuanthu_news_48k-hsmm";
                    //requestTTS.voice = "hn_female_thutrang_phrase_48k-hsmm";
                    requestTTS.voice = "sg_male_xuankien_vdts_48k-hsmm";
                    //requestTTS.voice = "sg_female_xuanhong_vdts_48k-hsmm";
                    requestTTS.user_id = "48380";
                    requestTTS.rate = "0.5";
                    requestTTS.time = "1533523698753";
                    requestTTS.input_text = VoiceText;

                    // or just whitelisted properties
                    request.AddJsonBody(requestTTS);

                    // easily add HTTP Headers
                    request.AddHeader("Content-Type", "application/json");

                    // execute the request
                    var response = client.Post(request);
                    if (response.ContentType.Equals("audio/x-wav"))
                    {
                        var byteArray = response.RawBytes;
                        Stream stream = new MemoryStream(byteArray);

                        using (Stream file = File.Create("C:\\TempFile\\V" + OrderId + ".mp3"))
                        {
                            CopyStream(stream, file);
                        }
                        fileName = "C:\\TempFile\\V" + OrderId + ".mp3";
                    }
                }
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #endregion

        #region BillOrderScale

        #region method getBillOrderScale
        public DataTable getBillOrderScale(string SearchKey, string TypeProduct, string Step)
        {
            string SQL_SEARCH = "", SQL_TypeProduct = "", SQL_Step = "";
            DataTable objTable = new DataTable();
            try
            {
                if (TypeProduct.Trim() != "Xem theo chủng loại")
                {
                    SQL_TypeProduct = "AND B.TypeProduct = @TypeProduct ";
                }

                if (Step.Trim() != "Xem theo trạng thái")
                {
                    if (Step == "Đã nhận đơn")
                    {
                        SQL_Step = "AND B.Step = 0 AND ISNULL(DriverUserName,N'') <> N'' ";
                    }
                    else if (Step == "Chưa xác thực")
                    {
                        SQL_Step = "AND B.Step = 0 ";
                    }
                    else if (Step == "Đã xác thực")
                    {
                        SQL_Step = "AND B.Step = 1 ";
                    }
                    else if (Step == "Đã vào cổng")
                    {
                        SQL_Step = "AND B.Step = 2 ";
                    }
                    else if (Step == "Đã cân vào")
                    {
                        SQL_Step = "AND B.Step = 3 ";
                    }
                    else if (Step == "Mời xe vào")
                    {
                        SQL_Step = "AND B.Step = 4 ";
                    }
                    else if (Step == "Đang lấy hàng")
                    {
                        SQL_Step = "AND B.Step = 5 ";
                    }
                    else if (Step == "Đã lấy hàng")
                    {
                        SQL_Step = "AND B.Step = 6 ";
                    }
                    else if (Step == "Đã cân ra")
                    {
                        SQL_Step = "AND B.Step = 7 ";
                    }
                    else if (Step == "Đã hoàn thành")
                    {
                        SQL_Step = "AND B.Step = 8 ";
                    }
                }

                if (SearchKey.Trim() != "")
                {
                    SQL_SEARCH = " AND (B.DeliveryCode LIKE N'%'+@SearchKey+'%'  OR B.DriverName LIKE N'%'+@SearchKey+'%' OR B.Vehicle LIKE N'%'+@SearchKey+'%' OR B.NameDistributor LIKE N'%'+@SearchKey+'%' OR B.NameProduct LIKE N'%'+@SearchKey+'%') ";
                }

                string SQLQUERY = "SELECT TOP 100 B.Id, B.CardNo, B.DeliveryCode AS IDOrderSyn, B.TimeConfirm1, B.TimeConfirm2,  NameStore, B.State, B.OrderDate AS DayCreate, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm1)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm1)),2) AS nvarchar)),N'') AS TimeConfirm1, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm2)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm2)),2) AS nvarchar)),N'') AS TimeConfirm2, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,B.TimeConfirm3)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,B.TimeConfirm3)),2) AS nvarchar)),N'') AS TimeConfirm3, B.Step, B.Confirm1, B.Confirm2,B.Confirm3,B.Confirm4, CASE WHEN IndexOrder > 0 THEN CAST(ISNULL(B.IndexOrder,0) AS nvarchar) ELSE REPLACE(CAST(ISNULL(B.IndexOrder,0) AS nvarchar),N'0',N'') END AS IndexOrder, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, ISNULL(WeightIn,0) AS WeightIn, ISNULL(WeightOut,0) AS WeightOut, ISNULL((SELECT TOP 1 TonnageDefault FROM tblVehicle WHERE Vehicle = B.Vehicle),0) AS TonnageDefault, (ISNULL(WeightIn,0) - ISNULL(WeightOut,0)) AS Weight FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND Step IN (0,1,2,3,4,5,6) AND ISNULL(DriverUserName,N'') <> N'' "+ SQL_SEARCH + SQL_TypeProduct + SQL_Step+ " ORDER BY B.IndexOrder DESC, B.TimeConfirm1 DESC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-3);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = DateTime.Now;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                Cmd.Parameters.Add("TypeProduct", SqlDbType.NVarChar).Value = TypeProduct;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion
        #region method getBillOrderScaleLastest
        public DataTable getBillOrderScaleLastest(string type)
        {
            DataTable objTable = new DataTable();
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string storeProceduce = "";
                if (type == "scale_in")
                {
                    storeProceduce = "uspScaleInLastest";
                }
                else
                {
                    storeProceduce = "uspScaleOutLastest";
                }
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = storeProceduce;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    objTable = ds.Tables[0];
                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return objTable;
        }
        #endregion
        #endregion

        #region BillOrderRelease

        #region method getBillOrderRelease
        public DataTable getBillOrderRelease()
        {
            DataTable objTable = new DataTable();
            try
            {
                //string SQLQUERY = "SELECT B.Shifts, B.Id, B.Step, B.DeliveryCode AS IDOrderSyn, '' AS NameStore, B.State, B.OrderDate AS DayCreate, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, TroughLineCode, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND Step IN (1,2,3,4,5) AND ISNULL(IndexOrder2,0) = 0 AND TypeProduct = N'XK' ORDER BY B.Step DESC, B.TimeConfirm3 ASC";
                string SQLQUERY = "SELECT B.Shifts, B.Id, B.Step, B.DeliveryCode AS IDOrderSyn, '' AS NameStore, B.State, B.OrderDate AS DayCreate, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, TroughLineCode, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND Step IN (1,2,3,4,5) AND ISNULL(IndexOrder2,0) = 0 ORDER BY B.Step DESC, B.TimeConfirm3 ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-3);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = DateTime.Now;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getBillOrderReleaseROI
        public DataTable getBillOrderReleaseROI()
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT B.Id, B.Step, B.XiRoiAttatchmentFile, B.DeliveryCode AS IDOrderSyn, '' AS NameStore, B.State, B.OrderDate AS DayCreate, REPLACE(CAST(ISNULL(Trough,0) AS nvarchar),N'0',N'') AS Trough, TroughLineCode, ISNULL((CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn21)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn21)),2) AS nvarchar)+'-'+CAST(RIGHT('0'+ RTRIM(DATEPART(HOUR,TimeIn22)),2) AS nvarchar)+':'+CAST(RIGHT('0'+ RTRIM(DATEPART(MINUTE,TimeIn22)),2) AS nvarchar)),N'') AS TIMEIN33, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Đã xác thực'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng') AS State1, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE B.OrderDate BETWEEN @objDate1 AND @objDate2 AND (B.TypeProduct = N'ROI' OR  B.NameProduct LIKE N'%rời%') AND ISNULL(Step,0) <= 7 ORDER BY B.Step DESC, B.TimeConfirm3 ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-10);
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = DateTime.Now;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getBillOrderReleaseByTime
        public DataTable getBillOrderReleaseByTime(DateTime objDate1, DateTime objDate2, int Shifts, string SearchKey)
        {
            string SQL_SEARCH = "", SQL_Shifts = "";
            if (SearchKey.Trim() != "")
            {
                SQL_SEARCH = " AND (B.DeliveryCode LIKE N'%'+@SearchKey+'%'  OR B.DriverName LIKE N'%'+@SearchKey+'%' OR B.Vehicle LIKE N'%'+@SearchKey+'%' OR B.NameDistributor LIKE N'%'+@SearchKey+'%' OR B.NameProduct LIKE N'%'+@SearchKey+'%') ";
            }
            if (Shifts > 0)
            {
                SQL_Shifts = " AND Shifts = @Shifts ";
            }
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT B.Id, B.DeliveryCode AS IDOrderSyn, '' AS NameStore, B.State, B.OrderDate AS DayCreate, B.TimeConfirm7, TroughLineCode, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE B.TimeConfirm7 BETWEEN @objDate1 AND @objDate2 AND B.Step > 5 AND ISNULL(B.IsVoiced,0) = 0 "+ SQL_SEARCH + SQL_Shifts + " ORDER BY B.IndexOrder ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = objDate1;
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = objDate2;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                Cmd.Parameters.Add("Shifts", SqlDbType.Int).Value = Shifts; 
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getBillOrderReleaseByTimeROI
        public DataTable getBillOrderReleaseByTimeROI(DateTime objDate1, DateTime objDate2, string SearchKey)
        {
            string SQL_SEARCH = "";
            if (SearchKey.Trim() != "")
            {
                SQL_SEARCH = " AND (B.DeliveryCode LIKE N'%'+@SearchKey+'%'  OR B.DriverName LIKE N'%'+@SearchKey+'%' OR B.Vehicle LIKE N'%'+@SearchKey+'%' OR B.NameDistributor LIKE N'%'+@SearchKey+'%' OR B.NameProduct LIKE N'%'+@SearchKey+'%') ";
            }

            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT B.Id, B.XiRoiAttatchmentFile, B.PackageNumber, B.DeliveryCode AS IDOrderSyn, '' AS NameStore, B.State, B.OrderDate AS DayCreate, B.TimeConfirm7, TroughLineCode, Vehicle, DriverName, SumNumber, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE B.TimeConfirm7 BETWEEN @objDate1 AND @objDate2 AND B.Step > 5 AND ISNULL(B.IsVoiced,0) = 0 AND B.TypeProduct = N'ROI' " + SQL_SEARCH + " ORDER BY B.IndexOrder ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("objDate1", SqlDbType.DateTime).Value = objDate1;
                Cmd.Parameters.Add("objDate2", SqlDbType.DateTime).Value = objDate2;
                Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getBillOrderReleaseByVehicle
        public DataTable getBillOrderReleaseByVehicle(string Vehicle)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM [dbo].[tblStoreOrderOperating] A, tblStoreOrderOperatingPriority B WHERE A.TypeProduct = B.TypeProduct AND A.Vehicle  = @Vehicle AND A.Step > 0 AND A.Step <= 5 ORDER BY B.Priority ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method setBillOrderRelease
        public int setBillOrderRelease(int OrderId, int Trough)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "UPDATE tblStoreOrderOperating SET Confirm2 = 1, Confirm3 = 1, Confirm4 = 1, TimeConfirm4 = getdate(), IndexOrder = 0, Step = 4, Trough = @Trough WHERE OrderId = @OrderId AND Step <> 4";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQL;
                Cmd.Parameters.Add("OrderId", SqlDbType.Int).Value = OrderId;
                Cmd.Parameters.Add("Trough", SqlDbType.Int).Value = Trough;
                tmpValue = Cmd.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }

            return tmpValue;
        }
        #endregion     

        #region method getBillOrderReleaseTrough
        public DataTable getBillOrderReleaseTrough(int Trough)
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT TOP 1 A.Id, A.DeliveryCode AS IDOrderSyn, A.NameStore, A.State, A.DayCreate, B.Step, B.Confirm1, B.Confirm2,B.Confirm3,B.Confirm4, ISNULL((SELECT TOP 1 Vehicle FROM tblStoreOrderVehicle WHERE OrderId = A.Id AND ISNULL(tblStoreOrderVehicle.State,0) = 1 ORDER BY Id DESC),N'') AS Vehicle, ISNULL((SELECT TOP 1 ISNULL(NameDriver,N'') FROM tblStoreOrderVehicle WHERE OrderId = A.Id AND ISNULL(tblStoreOrderVehicle.State,0) = 1 ORDER BY Id DESC),N'') AS DriverName, ISNULL((SELECT SUM(Number) FROM tblStoreOrderDetail WHERE OrderId = A.Id),0) AS SumNumber, (SELECT NameDistributor FROM tblDistributor WHERE IDDistributor = A.IDDistributor) AS NameDistributor, (SELECT TOp 1 NameProduct FROM tblStoreOrderDetail WHERE OrderId = A.Id ORDER BY ID) AS NameProduct FROM tblStoreOrder A, tblStoreOrderOperating B WHERE A.Id = B.OrderId AND Trough = @Trough AND Step = 5";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Trough", SqlDbType.Int).Value = Trough;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method setReleaseVoice
        public int setReleaseVoice(string DeliveryCode, string VoiceText)
        {
            int tmpValue = 0;
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "IF NOT EXISTS (SELECT * FROM tblStoreOrderOperatingVoice WHERE DeliveryCode = @DeliveryCode AND Step = 4 AND IndexNumber = 5) BEGIN INSERT INTO tblStoreOrderOperatingVoice(DeliveryCode,Step,VoiceText) VALUES(@DeliveryCode,4,@VoiceText) END";
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("VoiceText", SqlDbType.NVarChar).Value = VoiceText;
                tmpValue = Cmd.ExecuteNonQuery();

                Cmd.CommandText = "UPDATE tblStoreOrderOperating SET Confirm4 = 2, TimeConfirm4 = getdate(), Step = 4 WHERE DeliveryCode = @DeliveryCode AND ISNULL(IndexOrder2,0) = 0 AND Step IN (1,2,3) AND Trough = 0 AND Confirm1 <> 0 AND ISNULL((SELECT COUNT(*) FROM tblTrough WHERE ISNULL(State,0) = 1),0) >= ISNULL((SELECT COUNT(*) FROM tblStoreOrderOperating WHERE Step = 4),0)";
                tmpValue = Cmd.ExecuteNonQuery();

                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method setTroughForward
        public int setTroughForward(int OrderId, int TroughIdOld, int TroughIdNew)
        {
            int tmpValue = 0;
            try
            {
                string SQL = " UPDATE tblStoreOrderOperating SET [Trough] = @TroughIdNew WHERE Trough = @TroughIdOld AND Trough > 0 AND Step = 5";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("TroughIdOld", SqlDbType.Int).Value = TroughIdOld;
                Cmd.Parameters.Add("TroughIdNew", SqlDbType.Int).Value = TroughIdNew;
                Cmd.CommandText = SQL;
                tmpValue = Cmd.ExecuteNonQuery();

                if (tmpValue > 0)
                {
                    SQL = "UPDATE tblTrough SET Working = 0 WHERE Id = @TroughIdOld";
                    Cmd.CommandText = SQL;
                    tmpValue = Cmd.ExecuteNonQuery();

                    SQL = "UPDATE tblTrough SET Working = 1 WHERE Id = @TroughIdNew";
                    Cmd.CommandText = SQL;
                    tmpValue = Cmd.ExecuteNonQuery();
                }

                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #endregion

        #region VehicleInfo
        #region method getVehicleInfo
        public DataTable getVehicleInfo()
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT TOP 100 1000 AS tmpOrder, B.Id, B.IndexOrder, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(Step AS nvarchar),N'0',N'Chưa xác thực'),N'1',N'Chờ vào cổng'),N'2',N'Đã vào cổng'),N'3',N'Đã cân vào'),N'4',N'Mời xe vào'),N'5',N'Đang lấy hàng'),N'6',N'Đã lấy hàng') AS State1, Vehicle, DriverName, NameDistributor, NameProduct, B.IndexOrder1 FROM tblStoreOrderOperating B WHERE Step IN (0,1,4) AND B.IndexOrder > 0 AND ISNULL(IndexOrder2,0) = 0 ORDER BY B.IndexOrder ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    if (objTable.Rows[i]["IndexOrder"].ToString() != "")
                    {
                        objTable.Rows[i]["tmpOrder"] = objTable.Rows[i]["IndexOrder"].ToString();
                    }
                }
                objTable.DefaultView.Sort = "[tmpOrder] ASC";
            }
            catch
            {

            }
            return objTable;
        }
        #endregion
        #endregion

        #region method CopyStream
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
        #endregion

        #region method getTypeProductByDeliveryCode
        public string getTypeProductByDeliveryCode(string DeliveryCode)
        {
            string TypeProduct = "";
            try
            {
                string SQLQUERY = "SELECT TOP 1 A.TypeProduct FROM tblStoreOrderOperating A, tblStoreOrderOperatingPriority B  WHERE A.TypeProduct = B.TypeProduct AND A.Vehicle = (SELECT Vehicle FROM tblStoreOrderOperating WHERE DeliveryCode = @DeliveryCode) AND A.Step IN (0,1) ORDER BY B.Priority ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    TypeProduct = Rd["TypeProduct"].ToString();
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return TypeProduct;
        }
        #endregion

        #region method getDeliveryCodeParentByDeliveryCode
        public string getDeliveryCodeParentByDeliveryCode(string DeliveryCode)
        {
            string _DeliveryCode = "";
            try
            {
                string SQLQUERY = "SELECT TOP 1 A.DeliveryCode FROM tblStoreOrderOperating A, tblStoreOrderOperatingPriority B  WHERE A.TypeProduct = B.TypeProduct AND A.Vehicle = (SELECT Vehicle FROM tblStoreOrderOperating WHERE DeliveryCode = @DeliveryCode) AND A.Step IN (0,1) ORDER BY B.Priority ASC";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    _DeliveryCode = Rd["DeliveryCode"].ToString();
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return _DeliveryCode;
        }
        #endregion

        #region method getLogOrderById
        public string getLogOrderById(int Id)
        {
            string logOrder = "";
            try
            {
                string SQLQUERY = "SELECT TOP 1 A.LogProcessOrder FROM tblStoreOrderOperating A WHERE A.Id = @Id";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    logOrder = Rd["LogProcessOrder"].ToString();
                    if (logOrder.Trim() != "")
                    {
                        logOrder = logOrder.Substring(1, logOrder.Trim().Length - 1);
                    }
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return logOrder;
        }
        #endregion

        #region method FinishBillOrder
        public int FinishBillOrder(string DeliveryCode)
        {
            int tmpValue = 0;
            SqlConnection sqlCon_INDEX = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQL = $@"UPDATE tblStoreOrderOperating
                                         SET    Confirm9 = 1 ,
                                                TimeConfirm9 = GETDATE() ,
                                                Confirm9Note = N'Hệ thống kết thúc' ,
                                                LogProcessOrder = CONCAT(LogProcessOrder, N'#Kết lúc đơn hàng bằng tay lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')) ,
                                                Step = 9
                                         WHERE DeliveryCode = @DeliveryCode";
                sqlCon_INDEX.Open();
                SqlCommand Cmd_INDEX = sqlCon_INDEX.CreateCommand();
                Cmd_INDEX.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd_INDEX.CommandText = SQL;
                tmpValue = Cmd_INDEX.ExecuteNonQuery();
            }
            catch
            {
                tmpValue = 0;
            }
            finally
            {
                sqlCon_INDEX.Close();
                sqlCon_INDEX.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method CancelAcceptBillOrder
        public int CancelAcceptBillOrder(string DeliveryCode)
        {
            int tmpValue = 0;
            SqlConnection sqlCon_INDEX = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                //string SQL = $@"UPDATE  dbo.tblStoreOrderOperating
                //                        SET     
                //                                Step = 0 ,
                //                                Confirm1 = 0 ,
                //                                TimeConfirm1 = NULL ,
                //                                Confirm2 = 0 ,
                //                                TimeConfirm2 = NULL ,
                //                                Confirm3 = 0 ,
                //                                TimeConfirm3 = NULL ,
                //                                Confirm4 = 0 ,
                //                                TimeConfirm4 = NULL ,
                //                                Confirm5 = 0 ,
                //                                TimeConfirm5 = NULL ,
                //                                Confirm6 = 0 ,
                //                                TimeConfirm6 = NULL ,
                //                                Confirm7 = 0 ,
                //                                TimeConfirm7 = NULL ,
                //                                Confirm8 = 0 ,
                //                                TimeConfirm8 = NULL ,
                //                          Confirm9 = 0,
                //                          TimeConfirm9 = NULL,
                //                                IndexOrder = 0 ,
                //                          DeliveryCodeParent = NULL,
                //                          IndexOrder2 = 0,
                //                                IndexOrder1 = 0,
                //                          DriverUserName = NULL,
                //                                DriverAccept = NULL,
                //                                LogHistory = CONCAT(LogHistory,N', Hủy nhận đơn lúc ', GETDATE()),
                //                                LogProcessOrder = CONCAT(LogProcessOrder, N'#Hủy nhận đơn bằng tay lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss'))
                //                        WHERE   DeliveryCode = @DeliveryCode";
                //sqlCon_INDEX.Open();
                //SqlCommand Cmd_INDEX = sqlCon_INDEX.CreateCommand();
                //Cmd_INDEX.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                //Cmd_INDEX.CommandText = SQL;
                //tmpValue = Cmd_INDEX.ExecuteNonQuery();

                sqlCon_INDEX.Open();
                SqlCommand Cmd_ReIndex = new SqlCommand("uspOperatingCancelReceivedOrderByDeliveryCode", sqlCon_INDEX);
                Cmd_ReIndex.CommandType = CommandType.StoredProcedure;
                Cmd_ReIndex.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd_ReIndex.ExecuteReader();
                tmpValue = 1;

            }
            catch
            {
                tmpValue = 0;
            }
            finally
            {
                sqlCon_INDEX.Close();
                sqlCon_INDEX.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method CancelSTTBillOrder
        public int CancelSTTBillOrder(string DeliveryCode)
        {
            int tmpValue = 0;
            SqlConnection sqlCon_INDEX = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                string SQL = $@"UPDATE  dbo.tblStoreOrderOperating
                                        SET     
                                                Step = 0 ,
                                                Confirm1 = 0 ,
                                                TimeConfirm1 = NULL ,
                                                Confirm2 = 0 ,
                                                TimeConfirm2 = NULL ,
                                                Confirm3 = 0 ,
                                                TimeConfirm3 = NULL ,
                                                Confirm4 = 0 ,
                                                TimeConfirm4 = NULL ,
                                                Confirm5 = 0 ,
                                                TimeConfirm5 = NULL ,
                                                Confirm6 = 0 ,
                                                TimeConfirm6 = NULL ,
                                                Confirm7 = 0 ,
                                                TimeConfirm7 = NULL ,
                                                Confirm8 = 0 ,
                                                TimeConfirm8 = NULL ,
		                                        Confirm9 = 0,
		                                        TimeConfirm9 = NULL,
                                                IndexOrder = 0 ,
		                                        DeliveryCodeParent = NULL,
		                                        IndexOrder2 = 0,
                                                IndexOrder1 = 0,
                                                LogHistory = CONCAT(LogHistory,N', Hủy lốt lúc ', GETDATE()),
                                                LogProcessOrder = CONCAT(LogProcessOrder, N'#Hủy lốt bằng tay lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss'))
                                        WHERE   DeliveryCode = @DeliveryCode";
                sqlCon_INDEX.Open();
                SqlCommand Cmd_INDEX = sqlCon_INDEX.CreateCommand();
                Cmd_INDEX.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd_INDEX.CommandText = SQL;
                tmpValue = Cmd_INDEX.ExecuteNonQuery();
            }
            catch
            {
                tmpValue = 0;
            }
            finally
            {
                sqlCon_INDEX.Close();
                sqlCon_INDEX.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method ConfirmBillOrder
        public int ConfirmBillOrder(string DeliveryCode)
        {
            int tmpValue = 0;
            SqlConnection sqlCon_INDEX = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                //string SQL = $@"UPDATE  dbo.tblStoreOrderOperating
                //                        SET     Step = 1 ,
                //                                Confirm1 = 1 ,
                //                                TimeConfirm1 = GETDATE() ,
                //                                LogHistory = CONCAT(LogHistory,N', Xác thực bằng tay lúc ', GETDATE()) ,
                //                                LogProcessOrder = CONCAT(LogProcessOrder, N'#Xác thực bằng tay lúc ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')),
                //                                IndexOrder = (SELECT MAX(IndexOrder) FROM tblStoreOrderOperating WHERE TypeProduct = (SELECT TOP 1 TypeProduct FROM dbo.tblStoreOrderOperating WHERE DeliveryCode =  @DeliveryCode)) + 1
                //                        WHERE   DeliveryCode = @DeliveryCode";
                //sqlCon_INDEX.Open();
                //SqlCommand Cmd_INDEX = sqlCon_INDEX.CreateCommand();
                //Cmd_INDEX.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                //Cmd_INDEX.CommandText = SQL;
                //tmpValue = Cmd_INDEX.ExecuteNonQuery();
                sqlCon_INDEX.Open();
                SqlCommand Cmd_ReIndex = new SqlCommand("uspOperatingUpdateBillOrderConfirm1ByDeliveryCode", sqlCon_INDEX);
                Cmd_ReIndex.CommandType = CommandType.StoredProcedure;
                Cmd_ReIndex.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd_ReIndex.ExecuteReader();
                tmpValue = 1;
            }
            catch
            {
                tmpValue = 0;
            }
            finally
            {
                sqlCon_INDEX.Close();
                sqlCon_INDEX.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method UpdateIndexBillOrderPriority
        public int UpdateIndexBillOrderPriority(string DeliveryCode, int indexOrder)
        {
            int tmpValue = 0;
            SqlConnection sqlCon_INDEX = new SqlConnection(TVSOracle.SQL_Con);
            try
            {

                //string SQL = $@"UPDATE  dbo.tblStoreOrderOperating
                //                SET     IndexOrder = IndexOrder + 1 ,
                //                        LogProcessOrder = CONCAT(LogProcessOrder,
                //                                                    N'#Đổi lốt do có đơn ưu tiên lúc ',
                //                                                    FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss'))
                //                WHERE   TypeProduct = ( SELECT TOP 1 TypeProduct
                //                                        FROM    dbo.tblStoreOrderOperating
                //                                        WHERE   DeliveryCode = @DeliveryCode
                //                                        )
                //                        AND Step IN (1,4)
                //                        AND IndexOrder >= @IndexOrder";
                //sqlCon_INDEX.Open();
                //SqlCommand Cmd_INDEX = sqlCon_INDEX.CreateCommand();
                //Cmd_INDEX.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                //Cmd_INDEX.Parameters.Add("IndexOrder", SqlDbType.Int).Value = indexOrder;
                //Cmd_INDEX.CommandText = SQL;
                //Cmd_INDEX.ExecuteNonQuery();


                //string SQLUpdate = $@"UPDATE  dbo.tblStoreOrderOperating
                //                        SET     IndexOrder = @IndexOrder ,
                //                                LogProcessOrder = CONCAT(LogProcessOrder,
                //                                                         N'#Đổi lốt do có đơn ưu tiên', N', lốt cũ:',
                //                                                         IndexOrder, N', lốt mới', @IndexOrder,
                //                                                         N' lúc ',
                //                                                         FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss'))
                //                        WHERE   DeliveryCode = @DeliveryCode;";
                //SqlCommand Cmd_Update = sqlCon_INDEX.CreateCommand();
                //Cmd_Update.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                //Cmd_Update.Parameters.Add("IndexOrder", SqlDbType.Int).Value = indexOrder;
                //Cmd_Update.CommandText = SQLUpdate;
                //tmpValue = Cmd_Update.ExecuteNonQuery();
                sqlCon_INDEX.Open();
                SqlCommand Cmd_ReIndex = new SqlCommand("uspOperatingReIndexOrderByDeliveryCode", sqlCon_INDEX);
                Cmd_ReIndex.CommandType = CommandType.StoredProcedure;
                Cmd_ReIndex.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd_ReIndex.Parameters.Add("IndexOrder", SqlDbType.Int).Value = indexOrder;
                Cmd_ReIndex.ExecuteReader();
                tmpValue = 1;
            }
            catch(Exception ex)
            {
                tmpValue = 0;
            }
            finally
            {
                sqlCon_INDEX.Close();
                sqlCon_INDEX.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method updateStoreOrderOperatingDriver
        public int updateStoreOrderOperatingDriver(int Id, string DriverUserName, string Vehicle)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = DriverUserName;
                Cmd.Parameters.Add("DriverUserName", SqlDbType.NVarChar).Value = DriverUserName;
                Cmd.CommandText = "UPDATE tblStoreOrderOperating SET DriverUserName = @DriverUserName, DriverAccept = getdate(), IndexOrder = ISNULL((SELECT MAX(IndexOrder) FROM tblStoreOrderOperating WHERE Vehicle = @Vehicle),0) WHERE Id = @Id AND ISNULL(DriverUserName,N'') = N''";
                tmpValue = Cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method UpdateAutoScale
        public int UpdateAutoScale(string vehicle, bool IsScaleAuto)
        {
            int tmpValue = 0;
            try
            {
                string SQL0 = IsScaleAuto ?
                    "UPDATE dbo.tblStoreOrderOperating SET IsScaleAuto = 1 WHERE Vehicle = @Vehilce AND ISNULL(DriverUserName,'') != '' AND Step > 0 AND Step < 8 AND ISNULL(IsScaleAuto, 1) = 0"
                    : "UPDATE dbo.tblStoreOrderOperating SET IsScaleAuto = 0 WHERE Vehicle = @Vehilce AND ISNULL(DriverUserName,'') != '' AND Step > 0 AND Step < 8 AND ISNULL(IsScaleAuto, 1) = 1"
                    ;
                SqlConnection sqlCon0 = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon0.Open();
                SqlCommand Cmd0 = sqlCon0.CreateCommand();
                Cmd0.CommandText = SQL0;
                Cmd0.Parameters.Add("Vehilce", SqlDbType.NVarChar).Value = vehicle;
                tmpValue = Cmd0.ExecuteNonQuery();
                sqlCon0.Close();
                sqlCon0.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method UpdatePhieuThiNgiem
        public int UpdatePhieuThiNgiem(string deliveryCode, string attachment)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "UPDATE dbo.tblStoreOrderOperating SET XiRoiAttatchmentFile = @XiRoiAttatchmentFile where DeliveryCode = @DeliveryCode";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd0 = sqlCon.CreateCommand();
                Cmd0.CommandText = SQL;
                Cmd0.Parameters.Add("XiRoiAttatchmentFile", SqlDbType.NVarChar).Value = attachment;
                Cmd0.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = deliveryCode;
                tmpValue = Cmd0.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method UpdatePhieuThiNgiemKCS
        public int UpdatePhieuThiNgiemKCS(int idKcs, string attachment)
        {
            int tmpValue = 0;
            try
            {
                string SQL = "UPDATE tblKCSOperating SET LinkKCS = @LinkKCS WHERE Id = @Id";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd0 = sqlCon.CreateCommand();
                Cmd0.CommandText = SQL;
                Cmd0.Parameters.Add("LinkKCS", SqlDbType.VarChar).Value = attachment;
                Cmd0.Parameters.Add("Id", SqlDbType.Int).Value = idKcs;
                tmpValue = Cmd0.ExecuteNonQuery();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method UpdateTroughLineCode
        public int UpdateTroughLineCode(string DeliveryCode, string TroughLineCode)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                //Cmd.CommandText = "UPDATE tblStoreOrderOperating SET TroughLineCode = @TroughLineCode WHERE DeliveryCode = @DeliveryCode AND ISNULL(TroughLineCode,N'')  = N''";
                Cmd.CommandText = "UPDATE tblStoreOrderOperating SET TroughLineCode = @TroughLineCode WHERE DeliveryCode = @DeliveryCode";
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("TroughLineCode", SqlDbType.NVarChar).Value = TroughLineCode;
                tmpValue = Cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method UpdateShiftsInfo
        public int UpdateShiftsInfo(string DeliveryCode, int Shifts)
        {
            int tmpValue = 0;
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "UPDATE tblStoreOrderOperating SET Shifts = @Shifts WHERE DeliveryCode = @DeliveryCode";
                Cmd.Parameters.Add("DeliveryCode", SqlDbType.NVarChar).Value = DeliveryCode;
                Cmd.Parameters.Add("Shifts", SqlDbType.Int).Value = Shifts;
                tmpValue = Cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            return tmpValue;
        }
        #endregion

        #region method getTrough
        public DataTable getTrough()
        {
            DataTable objTable = new DataTable();
            try
            {
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT ISNULL(LineCode,N'') AS LineCode, Name FROM tblTrough WHERE ISNULL(State,0) = 1 ORDER BY Name";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                DataRow objRow = objTable.NewRow();
                objRow[0] = "-/-";
                objRow[1] = "Chọn máng, kho, bãi";
                objTable.Rows.InsertAt(objRow, 0);
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        public OrderOracleModel GetOrderErpDetails(string deliveryCode)
        {
            OrderOracleModel orderModel = new OrderOracleModel();
            double weightNull = 0;
            double weightFull = 0;
            DateTime timeIn;
            DateTime timeOut;
            try
            {

                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                sqlQuery = $@"select so.*, cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL,cvw.ITEMNAME as ITEM_NAME, cvw.TIMEIN, cvw.TIMEOUT  from sales_orders so
                         ,cx_vehicle_weight cvw 
                         where so.delivery_code = cvw.delivery_code 
                         and so.VEHICLE_CODE IS NOT NULL
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
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return orderModel;
        }
    }
}
