using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Core
{
    class Trough
    {
        #region method getTrough
        public DataTable getTrough()
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT * FROM tblTrough ORDER BY Name";
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
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getTroughForward
        public DataTable getTroughForward()
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT Id, Name FROM tblTrough WHERE ISNULL(Working,0) = 0 ORDER BY Name";
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
            }
            catch
            {

            }
            return objTable;
        }
        #endregion

        #region method getTroughWorking
        public int getTroughWorking()
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "SELECT COUNT(*) AS CountItem FROM tblTrough WHERE ISNULL(Working,0) = 0";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    tmpValue = int.Parse(Rd["CountItem"].ToString());
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {

            }
            return tmpValue;
        }
        #endregion

        #region method setTrough
        public int setTrough(int Id, string Name, string ProductId, bool State, string H, string W, string L)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "UPDATE tblTrough SET Name = @Name, ProductId = @ProductId, State = @State, Height = @Height, Width = @Width, Long = @Long WHERE Id = @Id";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id",SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
                Cmd.Parameters.Add("ProductId", SqlDbType.NVarChar).Value = ProductId;
                Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
                Cmd.Parameters.Add("Height", SqlDbType.NVarChar).Value = H;
                Cmd.Parameters.Add("Width", SqlDbType.NVarChar).Value = W;
                Cmd.Parameters.Add("Long", SqlDbType.NVarChar).Value = L;
                Cmd.CommandText = SQLQUERY;
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

        #region method setTroughProblem
        public int setTroughProblem(int Id, bool Problem)
        {
            int tmpValue = 0;
            try
            {
                string SQLQUERY = "UPDATE tblTrough SET Problem = @Problem WHERE Id = @Id";
                SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
                Cmd.Parameters.Add("Problem", SqlDbType.Bit).Value = Problem;
                Cmd.CommandText = SQLQUERY;
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

        #region method getProducts
        public DataTable getProducts()
        {
            DataTable objTable = new DataTable();
            try
            {
                string SQLQUERY = "SELECT 0 AS 'Select', * FROM tblProduct WHERE State = 1 ORDER BY NameProduct";
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
            }
            catch
            {

            }
            return objTable;
        }
        #endregion
    }
}
