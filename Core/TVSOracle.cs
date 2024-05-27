using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Data.SqlClient;
//using Oracle.ManagedDataAccess.Client;
using SMSVLand;
//using System.Data.OracleClient;

namespace HMXHTD
{
    class TVSOracle
    {
        #region declare objects
        private clsConfigXML objConfig = new clsConfigXML();
        public string oracleConnectionString = "", oracleConnectionString1 = "", sqlConnectionString = "";
        public static string SQL_Con = "Server = '192.168.158.19\\XHTD'; Uid = 'hmxhtd'; pwd = 'bh123!@#'; Database = 'QLBanhang_Test'", ORC_Con, API = "http://upwebsale.ximanghoangmai.vn:5555";
        #endregion

        #region method TVSOracle
        public TVSOracle()
        {
            string sqlServer = objConfig.GetKey("Server");
            string sqlDataBase = objConfig.GetKey("Database");
            string sqlUid = objConfig.GetKey("Uid");
            string sqlPwd = objConfig.GetKey("Pwd");
            this.sqlConnectionString = "Server = " + sqlServer + ";DataBase = " + sqlDataBase + ";Uid = " + sqlUid + "; Pwd = " + sqlPwd;

            TVSOracle.SQL_Con = "Server = '192.168.158.19\\XHTD'; Uid = 'hmxhtd'; pwd = 'bh123!@#'; Database = 'QLBanhang_Test'";//this.sqlConnectionString;
           // TVSOracle.SQL_Con = "Server = '192.168.0.12'; Uid = 'sa'; pwd = 'hmc@123'; Database = 'QLBanhang_Test'"; //db dev

            string sqlServer_O = objConfig.GetKey("Server_O");
            string sqlDataBase_O = objConfig.GetKey("Database_O");
            string sqlUid_O = objConfig.GetKey("Uid_O");
            string sqlPwd_O = objConfig.GetKey("Pwd_O");

            this.oracleConnectionString = "DATA SOURCE=" + sqlServer_O + ":1529/" + sqlDataBase_O + ";PASSWORD=" + sqlPwd_O + ";PERSIST SECURITY INFO=True;USER ID=" + sqlUid_O + "";
            this.oracleConnectionString1 = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=117.6.94.155)(PORT=1529)))(CONNECT_DATA=(SID=DEV8)));User ID=mbf;Password=mbf";
            //this.oracleConnectionString1 = "DATA SOURCE=192.168.158.95:1523/DEV2; PASSWORD=mobi123;PERSIST SECURITY INFO=True;USER ID=mobifone";

            TVSOracle.ORC_Con = this.oracleConnectionString1;
        }
        #endregion

        //#region method Test
        //[Obsolete]
        //public void Test()
        //{
        //    try
        //    {
        //        OracleConnection ORC_CONN = new OracleConnection(TVSOracle.ORC_Con);
        //        ORC_CONN.Open();
        //        ORC_CONN.Close();

        //        using (OracleConnection connectionlog0 = new OracleConnection(TVSOracle.ORC_Con))
        //        {
        //            connectionlog0.Open();
        //            //OracleCommand cmd = new OracleCommand(sqlQueryget0, connectionlog0);
        //            //cmd.CommandType = CommandType.Text;
        //            ////cmd.Parameters.Add(new OracleParameter("USER_NAME_RECEIVE", objInboxNotificationParams.UserName.ToUpper()));
        //            //OracleDataReader dr = cmd.ExecuteReader();
        //            //while (dr.Read())
        //            //{
                       
        //            //}
        //            //dr.Close();
        //            connectionlog0.Close();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //    //using (OracleConnection objConn = new OracleConnection(TVSOracle.ORC_Con))
        //    //{
        //    //    OracleCommand Cmd = new OracleCommand();
        //    //    Cmd.Connection = objConn;
        //    //    string sqlQuery = "select * ";
        //    //    sqlQuery += "from (select TO_CHAR(pll.operand * apps.inv_convert.inv_um_convert(to_number('1977'), 'TAN', pll.uom_code)) operand ";
        //    //    sqlQuery += ", plh.CURRENCY_CODE ";
        //    //    sqlQuery += ", plh.list_name price_list_name ";
        //    //    sqlQuery += ", plh.list_header_id price_list_id  ";
        //    //    sqlQuery += "from ht_price_list_headers plh ";
        //    //    sqlQuery += ", ht_price_list_lines pll  ";
        //    //    sqlQuery += "where 1=1  ";
        //    //    sqlQuery += "and pll.list_header_id = plh.LIST_HEADER_ID  ";
        //    //    sqlQuery += "and plh.active_flag = 'Y'  ";
        //    //    sqlQuery += "and plh.currency_code = 'VND'  ";
        //    //    sqlQuery += "and plh.line_type = 9  ";
        //    //    sqlQuery += "and pll.inventory_item_id = 1977  ";
        //    //    sqlQuery += "and plh.customer_id = 2173  ";
        //    //    sqlQuery += "and (pll.shippoint_id = 1 or pll.shippoint_id is null)  ";
        //    //    sqlQuery += "and ( 1425  in (select t.checkpoint_id from ht_price_list_checkpoints_l t where t.list_line_id = pll.list_line_id and t.check_flag = 'Y')  ";
        //    //    sqlQuery += "or (select count(*)from ht_price_list_checkpoints_l t where t.list_line_id = pll.list_line_id and t.check_flag = 'Y') = 0)  ";
        //    //    sqlQuery += "and (pll.start_date_effective <= to_date( '14/09/2019', 'dd/MM/RRRR') or pll.start_date_effective is null)  ";
        //    //    sqlQuery += "and (pll.end_date_effective >= to_date( '14/09/2019', 'dd/MM/RRRR') or pll.end_date_effective is null)  ";
        //    //    sqlQuery += "and 1 = 1 order by pll.start_date_effective  desc  ";
        //    //    sqlQuery += ")  ";
        //    //    sqlQuery += "where rownum = 1 ";
        //    //    Cmd.CommandText = sqlQuery;
        //    //    try
        //    //    {
        //    //        objConn.Open();
        //    //        OracleDataReader Rd = Cmd.ExecuteReader();
        //    //        while (Rd.Read())
        //    //        {
        //    //            int a = 0;
        //    //        }
        //    //        Rd.Close();
        //    //    }
        //    //    catch
        //    //    {
        //    //    }
        //    //    objConn.Close();
        //    //    objConn.Dispose();
        //    //}

        //    //OracleConnection con = new OracleConnection();
        //    //con.ConnectionString = this.oracleConnectionString1;
        //    //con.Open();

        //    //string sqlQuery = "";
        //    //sqlQuery += "select * ";
        //    //sqlQuery += "from (select TO_CHAR(pll.operand * apps.inv_convert.inv_um_convert(to_number('1983'), 'TAN', pll.uom_code)) operand ";
        //    //sqlQuery += ", plh.CURRENCY_CODE ";
        //    //sqlQuery += ", plh.list_name price_list_name ";
        //    //sqlQuery += ", plh.list_header_id price_list_id  ";
        //    //sqlQuery += "from ht_price_list_headers plh ";
        //    //sqlQuery += ", ht_price_list_lines pll  ";
        //    //sqlQuery += "where 1=1  ";
        //    //sqlQuery += "and pll.list_header_id = plh.LIST_HEADER_ID  ";
        //    //sqlQuery += "and plh.active_flag = 'Y'  ";
        //    //sqlQuery += "and plh.currency_code = 'VND'  ";
        //    //sqlQuery += "and plh.line_type = 9  ";
        //    //sqlQuery += "and pll.inventory_item_id = 1983  ";
        //    //sqlQuery += "and plh.customer_id = 2173  ";
        //    //sqlQuery += "and (pll.shippoint_id = 1 or pll.shippoint_id is null)  ";
        //    //sqlQuery += "and ( 1425  in (select t.checkpoint_id from ht_price_list_checkpoints_l t where t.list_line_id = pll.list_line_id and t.check_flag = 'Y')  ";
        //    //sqlQuery += "or (select count(*)from ht_price_list_checkpoints_l t where t.list_line_id = pll.list_line_id and t.check_flag = 'Y') = 0)  ";
        //    //sqlQuery += "and (pll.start_date_effective <= to_date( '05/10/2019', 'dd/MM/RRRR') or pll.start_date_effective is null)  ";
        //    //sqlQuery += "and (pll.end_date_effective >= to_date( '05/10/2019', 'dd/MM/RRRR') or pll.end_date_effective is null)  ";
        //    //sqlQuery += "and 1 = 1 order by pll.start_date_effective  desc  ";
        //    //sqlQuery += ")  ";
        //    //sqlQuery += "where rownum = 1 ";

        //    //OracleCommand cmd = con.CreateCommand();
        //    //cmd.CommandText = sqlQuery;
        //    //cmd.CommandTimeout = 1000;
        //    //OracleDataAdapter da1 = new OracleDataAdapter();
        //    //DataSet ds1 = new DataSet();
        //    //da1.SelectCommand = cmd;
        //    //da1.Fill(ds1);
        //    //con.Close();
        //    //con.Dispose();

        //    //OracleConnection con = new OracleConnection();
        //    //con.ConnectionString = this.oracleConnectionString1;
        //    //con.Open();
        //    //OracleCommand cmd = con.CreateCommand();
        //    //cmd.CommandType = CommandType.StoredProcedure;
        //    //cmd.CommandText = "dev_canxe_pkg.sp_insert_so";
        //    //cmd.CommandTimeout = 1000;
        //    //cmd.Parameters.Add("p_bcc_vb",OracleDbType.NVarchar2).Value = "1";
        //    //cmd.Parameters.Add("p_order_type", OracleDbType.NVarchar2).Value = "HD";
        //    //cmd.Parameters.Add("p_order_date", OracleDbType.NVarchar2).Value = "2019-09-17";
        //    //cmd.Parameters.Add("p_doc_num", OracleDbType.NVarchar2).Value = "1212";
        //    //cmd.Parameters.Add("p_doc_series", OracleDbType.NVarchar2).Value = "12343";
        //    //cmd.Parameters.Add("p_customer_code", OracleDbType.NVarchar2).Value = "2144";
        //    //cmd.Parameters.Add("p_shippoint_code", OracleDbType.NVarchar2).Value = "HM";
        //    //cmd.Parameters.Add("p_area_code", OracleDbType.NVarchar2).Value = "BS";
        //    //cmd.Parameters.Add("p_transport_method", OracleDbType.NVarchar2).Value = "1";
        //    //cmd.Parameters.Add("p_item_code", OracleDbType.NVarchar2).Value = "011701600071";
        //    //cmd.Parameters.Add("p_quantity", OracleDbType.NVarchar2).Value = "123";
        //    //cmd.Parameters.Add("p_vehicle_code", OracleDbType.NVarchar2).Value = "14C-00663";
        //    //cmd.Parameters.Add("p_driver_name", OracleDbType.NVarchar2).Value = "Nguyễn Vô Biên";
        //    //cmd.Parameters.Add("p_driver_info", OracleDbType.NVarchar2).Value = "123456789";
        //    //cmd.Parameters.Add("p_mooc_code", OracleDbType.NVarchar2).Value = "1";
        //    //cmd.Parameters.Add("p_description", OracleDbType.NVarchar2).Value = "Test";
        //    //cmd.Parameters.Add("p_print_status", OracleDbType.NVarchar2).Value = "PENDING";
        //    //cmd.Parameters.Add("p_user_id", OracleDbType.NVarchar2).Value = "3791";
        //    //cmd.Parameters.Add("p_created_date", OracleDbType.NVarchar2).Value = "2019-09-17 16:17:42";
        //    //cmd.Parameters.Add("p_ex_customer_code", OracleDbType.NVarchar2).Value = "1";
        //    //cmd.Parameters.Add("p_message", OracleDbType.NVarchar2).Value = "";
        //    //cmd.Parameters["p_message"].Direction = ParameterDirection.Output;
        //    //cmd.ExecuteNonQuery();
        //    //con.Close();
        //    //con.Dispose();
        //}
        //#endregion

        #region method distributorSyn
        public int distributorSyn(int IDDistributorSyn, string NameDistributor)
        {
            int tmpValue = 0;

            try
            {
                string sqlQuery = "IF NOT EXISTS (SELECT * FROM tblDistributor WHERE IDDistributorSyn = @IDDistributorSyn) ";
                sqlQuery += "BEGIN INSERT INTO tblDistributor(CodeDistributor,IDDistributorSyn,NameDistributor,DaySyn) VALUES(@CodeDistributor,@IDDistributorSyn,@NameDistributor,getdate()) END";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CodeDistributor", SqlDbType.NVarChar).Value = IDDistributorSyn;
                Cmd.Parameters.Add("IDDistributorSyn", SqlDbType.Int).Value = IDDistributorSyn;
                Cmd.Parameters.Add("NameDistributor", SqlDbType.NVarChar).Value = NameDistributor;
                tmpValue = Cmd.ExecuteNonQuery();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion

        #region method getIDDistributorFromIDDistributorSyn
        public int getIDDistributorSynFromIDDistributor(int IDDistributorSyn)
        {
            int tmpValue = 0;

            try
            {
                string sqlQuery = "SELECT TOP 1 ISNULL(IDDistributor,0) AS IDDistributor FROM tblDistributor WHERE IDDistributorSyn = @IDDistributorSyn";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("IDDistributorSyn", SqlDbType.Int).Value = IDDistributorSyn;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    tmpValue = int.Parse(Rd["IDDistributor"].ToString());
                }
                Rd.Close();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion

        #region method orderSyn
        public int orderSyn(string IDOrderSyn, string DateCreate, int IDDistributor, string NameDistributor, string CodeOrder, string Vehicle, string NameDriver)
        {
            int tmpValue = 0;

            try
            {
                string sqlQuery = "IF NOT EXISTS (SELECT * FROM tblOrder WHERE IDDistributor = @IDDistributor AND IDOrderSyn = @IDOrderSyn) ";
                sqlQuery += "BEGIN INSERT INTO tblOrder(IDOrderSyn,DateCreate,IDDistributor,NameDistributor,CodeOrder,Vehicle,NameDriver,DaySyn) VALUES(@IDOrderSyn,@DateCreate,@IDDistributor,@NameDistributor,@CodeOrder,@Vehicle,@NameDriver,getdate()) END";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("IDOrderSyn", SqlDbType.NVarChar).Value = IDOrderSyn;
                try
                {
                    Cmd.Parameters.Add("DateCreate", SqlDbType.DateTime).Value = DateCreate;
                }
                catch
                {
                    Cmd.Parameters.Add("DateCreate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                Cmd.Parameters.Add("IDDistributor", SqlDbType.Int).Value = IDDistributor; 
                Cmd.Parameters.Add("NameDistributor", SqlDbType.NVarChar).Value = NameDistributor;
                Cmd.Parameters.Add("CodeOrder", SqlDbType.NVarChar).Value = CodeOrder;
                Cmd.Parameters.Add("Vehicle", SqlDbType.NVarChar).Value = Vehicle;
                Cmd.Parameters.Add("NameDriver", SqlDbType.NVarChar).Value = NameDriver;
                tmpValue = Cmd.ExecuteNonQuery();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion

        #region method getIDOrderFromIDOrderSyn
        public int getIDOrderFromIDOrderSyn(string IDOrderSyn)
        {
            int tmpValue = 0;

            try
            {
                string sqlQuery = "SELECT TOP 1 ISNULL(IDOrder,0) AS IDOrder FROM tblOrder WHERE IDOrderSyn = @IDOrderSyn";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("IDOrderSyn", SqlDbType.NVarChar).Value = IDOrderSyn;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = int.Parse(Rd["IDOrder"].ToString());
                }
                Rd.Close();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion

        #region method productSyn
        public int productSyn(long IDProductSyn, string CodeProduct, string NameProduct)
        {
            int tmpValue = 0;

            try
            {
                string sqlQuery = "IF NOT EXISTS (SELECT * FROM tblProduct WHERE IDProductSyn = @IDProductSyn) ";
                sqlQuery += "BEGIN INSERT INTO tblProduct(CodeProduct,IDProductSyn,NameProduct,IDUnit,NameUnit,State,DaySyn) VALUES(@CodeProduct,@IDProductSyn,@NameProduct,@IDUnit,@NameUnit,1,getdate()) END";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("CodeProduct", SqlDbType.NVarChar).Value = CodeProduct;
                Cmd.Parameters.Add("IDProductSyn", SqlDbType.BigInt).Value = IDProductSyn;
                Cmd.Parameters.Add("NameProduct", SqlDbType.NVarChar).Value = NameProduct;
                Cmd.Parameters.Add("IDUnit", SqlDbType.Int).Value = 2;
                Cmd.Parameters.Add("NameUnit", SqlDbType.NVarChar).Value = "Tấn";
                tmpValue = Cmd.ExecuteNonQuery();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion

        #region method getIDProductFromIDProductSyn
        public long getIDProductFromIDProductSyn(long IDProductSyn)
        {
            long tmpValue = 0;

            try
            {
                string sqlQuery = "SELECT TOP 1 ISNULL(IDProduct,0) AS IDProduct FROM tblProduct WHERE IDProductSyn = @IDProductSyn";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("IDProductSyn", SqlDbType.BigInt).Value = IDProductSyn;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    tmpValue = long.Parse(Rd["IDProduct"].ToString());
                }
                Rd.Close();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion

        #region method orderDetailSyn
        public int orderDetailSyn(int IDOrder, long IDProduct, string NameProduct, int IDUnit, float Quantity)
        {
            int tmpValue = 0;

            try
            {
                string sqlQuery = "IF NOT EXISTS (SELECT * FROM tblOrderDetail WHERE IDOrder = @IDOrder AND IDProduct = @IDProduct) ";
                sqlQuery += "BEGIN INSERT INTO tblOrderDetail(IDOrder,IDProduct,NameProduct,IDUnit,Quantity,DaySyn) VALUES(@IDOrder,@IDProduct,@NameProduct,@IDUnit,@Quantity,getdate()) END";
                SqlConnection Sqlcon = new SqlConnection(this.sqlConnectionString);
                Sqlcon.Open();
                SqlCommand Cmd = Sqlcon.CreateCommand();
                Cmd.CommandText = sqlQuery;
                Cmd.Parameters.Add("IDOrder", SqlDbType.Int).Value = IDOrder;
                Cmd.Parameters.Add("IDProduct", SqlDbType.BigInt).Value = IDProduct;
                Cmd.Parameters.Add("NameProduct", SqlDbType.NVarChar).Value = NameProduct;
                Cmd.Parameters.Add("IDUnit", SqlDbType.Int).Value = IDUnit;
                Cmd.Parameters.Add("Quantity", SqlDbType.Float).Value = Quantity;
                tmpValue = Cmd.ExecuteNonQuery();
                Sqlcon.Close();
                Sqlcon.Dispose();
            }
            catch
            {
                tmpValue = 0;
            }

            if (tmpValue < 0)
            {
                tmpValue = 0;
            }

            return tmpValue;
        }
        #endregion
    }
}
