using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Accenture.MWT.DomainObject;

/// <summary>
/// Summary description for WorkSchedulingDataAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class WorkSchedulingDataAccess
    {
        public WorkSchedulingDataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(WorkScheduling ObjWS)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Work_Scheduling";
            int result = 0;


            hashPara.Add("@Mat_Work_Scheduling_Id", ObjWS.Mat_Work_Scheduling_Id);
            hashPara.Add("@Master_Header_Id", ObjWS.Master_Header_Id);

            hashPara.Add("@Plant_Id", ObjWS.Plant_Id);
            hashPara.Add("@Unit_Of_Issue", ObjWS.Unit_Of_Issue);
            hashPara.Add("@Production_Unit", ObjWS.Production_Unit);
            hashPara.Add("@Production_Supervisor", ObjWS.Production_Supervisor);
            hashPara.Add("@Prod_Sched_Profile", ObjWS.Prod_Sched_Profile);
            hashPara.Add("@Underdelivered_Tolerance_Lmt", ObjWS.Underdelivered_Tolerance_Lmt);
            hashPara.Add("@Overdelivered_Tolerance_Lmt", ObjWS.Overdelivered_Tolerance_Lmt);
            hashPara.Add("@Unlimited", ObjWS.Unlimited);
            hashPara.Add("@Serial_No_Profile", ObjWS.Serial_No_Profile);
            hashPara.Add("@Repetitive_Mfg_Profile", ObjWS.Repetitive_Mfg_Profile);

            hashPara.Add("@IsActive", ObjWS.IsActive);
            hashPara.Add("@UserId", ObjWS.UserId);
            hashPara.Add("@UserIp", ObjWS.IPAddress);
            hashPara.Add("@IsDraft", false);
            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        public WorkScheduling GetWorkScheduling(int Mat_Work_Scheduling_Id)
        {
            WorkScheduling ObjWS = new WorkScheduling();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Work_Scheduling_By_MatWorkSchedulingId";
            DataSet ds;

            hashPara.Add("@Mat_Work_Scheduling_Id", Mat_Work_Scheduling_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjWS.Mat_Work_Scheduling_Id = Convert.ToInt32(dt.Rows[0]["Mat_Work_Scheduling_Id"].ToString());
                        ObjWS.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjWS.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjWS.Unit_Of_Issue = dt.Rows[0]["Unit_Of_Issue"].ToString();
                        ObjWS.Production_Unit = dt.Rows[0]["Production_Unit"].ToString();
                        ObjWS.Production_Supervisor = dt.Rows[0]["Production_Supervisor"].ToString();
                        ObjWS.Prod_Sched_Profile = dt.Rows[0]["Prod_Sched_Profile"].ToString();
                        ObjWS.Underdelivered_Tolerance_Lmt = dt.Rows[0]["Underdelivered_Tolerance_Lmt"].ToString();
                        ObjWS.Overdelivered_Tolerance_Lmt = dt.Rows[0]["Overdelivered_Tolerance_Lmt"].ToString();
                        ObjWS.Unlimited = dt.Rows[0]["Unlimited"].ToString();
                        ObjWS.Serial_No_Profile = dt.Rows[0]["Serial_No_Profile"].ToString();
                        ObjWS.Repetitive_Mfg_Profile = dt.Rows[0]["Repetitive_Mfg_Profile"].ToString();
                    }
                }
                return ObjWS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public DataSet GetWorkSchedulingData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Work_Scheduling_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public bool CheckValidUnitOfIssue(string masterHeaderId, string unitOfIssue)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckValidUnitOfIssue";
            DataSet ds;
            bool flg = true;

            hashPara.Add("@Master_Header_Id", masterHeaderId);
            hashPara.Add("@Unit_Of_Issue", unitOfIssue);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    flg = false;
                }

                return flg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public bool CheckValidIssueUnit(string master_Header_Id, string issueUnit)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckValidIssueUnitInBasicData2";
            DataSet ds;
            bool flg = false;

            hashPara.Add("@Master_Header_Id", master_Header_Id);
            hashPara.Add("@Unit_Of_Issue", issueUnit);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    flg = true;
                }
                return flg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }
    }
}