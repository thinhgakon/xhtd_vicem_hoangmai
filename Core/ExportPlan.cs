using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace HMXHTD.Core
{
    public class ExportPlan
    {
        public DataTable Get_Export_Plan_List()
        {

            DataTable objTable = new DataTable();

            string sqlQuery = "select * from tblExportPlan";

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlCon.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlCon))
                    {
                        using (SqlDataReader reader = Cmd.ExecuteReader())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                objTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            while (reader.Read())
                            {
                                DataRow newRow = objTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    newRow[i] = reader[i];
                                }
                                objTable.Rows.Add(newRow);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get_Export_Plan_List-" + ex.Message);
            }

            return objTable;
        }

        public DataRow Get_Export_Plan_By_Id(int id)
        {
            DataTable objTable = new DataTable();

            string sqlQuery = "select * from tblExportPlan where Id = @Id";

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlCon.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlCon))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@Id", id));

                        using (SqlDataReader reader = Cmd.ExecuteReader())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                objTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            while (reader.Read())
                            {
                                DataRow newRow = objTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    newRow[i] = reader[i];
                                }
                                objTable.Rows.Add(newRow);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get_Export_Plan_By_Id-" + ex.Message);
            }

            return objTable.Rows[0];
        }

        public int Insert(string name, string ship_name, string syn_distributor_ids, DateTime start_date, DateTime end_date)
        {
            int tmp = 0;

            string sqlQuery = @"insert into tblExportPlan (Name, ShipName, SynDistributorIds, StartDate, EndDate, Status)
                                output inserted.Id values (@Name, @ShipName, @SynDistributorIds, @StartDate, @EndDate, @Status)";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlConn.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@Name", name));
                        Cmd.Parameters.Add(new SqlParameter("@ShipName", ship_name));
                        Cmd.Parameters.Add(new SqlParameter("@SynDistributorIds", syn_distributor_ids));
                        Cmd.Parameters.Add(new SqlParameter("@StartDate", start_date));
                        Cmd.Parameters.Add(new SqlParameter("@EndDate", end_date));
                        Cmd.Parameters.Add(new SqlParameter("@Status", 1));

                        var result = Cmd.ExecuteScalar();

                        if (result != null)
                        {
                            tmp = (int)result;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                tmp = 0;
                throw new Exception("Insert-" + ex.Message);
            }

            return tmp;
        }

        public bool Update(int id, string name, string ship_name, string syn_distributor_ids, DateTime start_date, DateTime end_date, int status)
        {
            bool tmp = false;

            var sqlQuery = @"update tblExportPlan
                             set Name = @Name,
                                 ShipName = @ShipName,
                                 SynDistributorIds = @SynDistributorIds,
                                 StartDate = @StartDate,
                                 EndDate = @EndDate,
                                 Status = @Status
                             where Id = @Id";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlConn.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@Id", id));
                        Cmd.Parameters.Add(new SqlParameter("@Name", name));
                        Cmd.Parameters.Add(new SqlParameter("@ShipName", ship_name));
                        Cmd.Parameters.Add(new SqlParameter("@SynDistributorIds", syn_distributor_ids));
                        Cmd.Parameters.Add(new SqlParameter("@StartDate", start_date));
                        Cmd.Parameters.Add(new SqlParameter("@EndDate", end_date));
                        Cmd.Parameters.Add(new SqlParameter("@Status", status));

                        tmp = Cmd.ExecuteNonQuery() > 0;
                    }
                }
            }

            catch (Exception ex)
            {
                tmp = false;
                throw new Exception("Update-" + ex.Message);
            }

            return tmp;
        }

        public bool Delete(int id)
        {
            bool tmp = false;

            var sqlQuery = @"delete from tblExportPlan where Id = @Id";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlConn.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@Id", id));

                        tmp = Cmd.ExecuteNonQuery() > 0;
                    }
                }
            }

            catch (Exception ex)
            {
                tmp = false;
                throw new Exception("Delete-" + ex.Message);
            }

            return tmp;
        }
    }

    public class ExportPlanDetail
    {
        public DataTable Get_Export_Plan_Detail_List(int plan_id)
        {
            DataTable objTable = new DataTable();

            string sqlQuery = "select * from tblExportPlanDetail where PlanId = @PlanId";

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlCon.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlCon))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@PlanId", plan_id));

                        using (SqlDataReader reader = Cmd.ExecuteReader())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                objTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            while (reader.Read())
                            {
                                DataRow newRow = objTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    newRow[i] = reader[i];
                                }
                                objTable.Rows.Add(newRow);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get_Export_Plan_Detail_List-" + ex.Message);
            }

            return objTable;
        }

        public int Insert(int plan_id, int product_id_syn, double order_number, double release_number)
        {
            int tmp = 0;

            string sqlQuery = @"insert into tblExportPlanDetail (PlanId, SynProductId, OrderNumber, ReleaseNumber)
                                output inserted.Id values (@PlanId, @SynProductId, @OrderNumber, @ReleaseNumber)";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlConn.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@PlanId", plan_id));
                        Cmd.Parameters.Add(new SqlParameter("@SynProductId", product_id_syn));
                        Cmd.Parameters.Add(new SqlParameter("@OrderNumber", order_number));
                        Cmd.Parameters.Add(new SqlParameter("@ReleaseNumber", release_number));

                        var result = Cmd.ExecuteScalar();

                        if (result != null)
                        {
                            tmp = (int)result;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                tmp = 0;
                throw new Exception("Insert-" + ex.Message);
            }

            return tmp;
        }

        public bool Update(int plan_id, int product_id_syn, double order_number, double release_number)
        {
            bool tmp = false;

            var sqlQuery = @"update tblExportPlanDetail
                             set OrderNumber = @OrderNumber,
                                 ReleaseNumber = @ReleaseNumber,
                             where PlanId = @PlanId and SynProductId = @SynProductId";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlConn.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@PlanId", plan_id));
                        Cmd.Parameters.Add(new SqlParameter("@SynProductId", product_id_syn));
                        Cmd.Parameters.Add(new SqlParameter("@OrderNumber", order_number));
                        Cmd.Parameters.Add(new SqlParameter("@ReleaseNumber", release_number));

                        tmp = Cmd.ExecuteNonQuery() > 0;
                    }
                }
            }

            catch (Exception ex)
            {
                tmp = false;
                throw new Exception("Update-" + ex.Message);
            }

            return tmp;
        }

        public bool Delete(int plan_id, int product_id_syn)
        {
            bool tmp = false;

            var sqlQuery = @"delete from tblExportPlanDetail where PlanId = @PlanId and SynProductId = @SynProductId";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(TVSOracle.SQL_Con))
                {
                    sqlConn.Open();

                    using (SqlCommand Cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@PlanId", plan_id));
                        Cmd.Parameters.Add(new SqlParameter("@SynProductId", product_id_syn));

                        tmp = Cmd.ExecuteNonQuery() > 0;
                    }
                }
            }

            catch (Exception ex)
            {
                tmp = false;
                throw new Exception("Delete-" + ex.Message);
            }

            return tmp;
        }
    }
}
