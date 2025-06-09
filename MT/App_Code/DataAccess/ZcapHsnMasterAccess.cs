using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using Accenture.MWT.DomainObject;
using System.Configuration;
using System.Web.Configuration;

namespace Accenture.MWT.DataAccess
{

    /// <summary>
    /// Summary description for ZcapHsnMasterAccess
    /// </summary>
    public class ZcapHsnMasterAccess
    {
        public string mRequestNo { get; set; }
        //public ZcapHsnMasterAccess()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}


        /// <summary>
        /// Validate WF code and assinged to request
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public DataSet ValidateFITD(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet(); 
            string procName = "Proc_GetFITDByWF_Code"; 
            try
            {
                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                //Srinidhi
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@MasterHeaderId", MasterHeaderId)); 

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "FITD");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }
            return dstData;
        }

        /// <summary>
        /// create new temp row in grid details
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int GetTempId(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetTempId";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);

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

        /// <summary>
        /// check request pending status
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public bool IsSAPintegrationPending(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsSAPintegrationPending";

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

            try
            {
                //DNRCOMM//objDal.OpenConnection();
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                }
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

            return retVal;
        }

        /// <summary>
        /// check initiator authorizatioin status
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="deptId"></param>
        /// <param name="userId"></param>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public bool IsUserInitiator(string moduleId, string deptId, string userId, string MasterHeaderId = "")
        {
            bool flg = true;
            if (moduleId == "")
            {
                moduleId = "0";
            }
            if (MasterHeaderId != "")
            {
                DataAccessLayer objDal1 = new DataAccessLayer();
                string query = "SELECT * FROM T_Master_Header WHERE Master_Header_Id = " + MasterHeaderId + " AND CreatedBy = " + userId + " AND IsActive = 'TRUE'";
                //string query = "SELECT * FROM T_Master_Header WHERE Master_Header_Id = " + MasterHeaderId + " AND CreatedBy = " + userId + " AND IsActive = 'TRUE'  AND IsDraft = 'TRUE'";
                try
                {
                    objDal1.OpenConnection();
                    flg = !objDal1.ReturnBool(query, ref objDal1.cnnConnection);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objDal1.CloseConnection(objDal1.cnnConnection);
                    objDal1 = null;
                }
            }

            if (flg)
            {
                DataAccessLayer objDal = new DataAccessLayer();
                string query1 = "SELECT * FROM M_Approving_Authority WHERE Module_Id = " + moduleId + " AND Department_Id = " + deptId + " AND User_Id = " + userId + " AND IsActive = 'TRUE'";
                try
                {
                    objDal.OpenConnection();
                    flg = objDal.ReturnBool(query1, ref objDal.cnnConnection);
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
            return flg;
        }

        /// <summary>
        /// Create new ZCAP / HSN master request 
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="plantId"></param>
        /// <param name="moduleId"></param>
        /// <param name="userId"></param>
        /// <param name="flg"></param>
        /// <param name="Master_Category"></param>
        /// <param name="Reference_Id"></param>
        /// <returns></returns>
        public int SaveHSNZCAPHeader(string masterHeaderId, string plantId, string moduleId, string userId, string flg, string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header_HSNZCAP";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Plant_Id", plantId);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter outRequestNo = objCommand.Parameters.Add("@RequestNo", SqlDbType.VarChar, 50);
            outRequestNo.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                retVal = (int)ret.Value;
                if (retVal > 0)
                {
                    masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
                    mRequestNo = SafeTypeHandling.ConvertToString(outRequestNo.Value);
                }

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
            return masterHeaderId2;
        }

        /// <summary>
        /// request rollback to user or spacific department
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="approvedByDeptId"></param>
        /// <param name="rollbackToWorkflowSeq"></param>
        /// <param name="remarks"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RollbackRequestHSNZACP(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Reject_Request_HSNZCAPL";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@RollbackToWorkFlowId", rollbackToWorkflowSeq);
            hashPara.Add("@Remarks", remarks);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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

        /// <summary>
        /// Approve request with comment
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="approvedByDeptId"></param>
        /// <param name="userId"></param>
        /// <param name="approvalNote"></param>
        /// <returns></returns>
        public int ApproveRequestHSNZACP(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_HSNZCAPL";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            hashPara.Add("@ApprovalComment", approvalNote);
            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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

        /// <summary>
        /// Request forwor to IDT department
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="approvedByDeptId"></param>
        /// <param name="userId"></param>
        /// <param name="approvalNote"></param>
        /// <returns></returns>
        public int ForwardToRequestHSNZACP(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_ForwardTo_Request_HSNZCAP";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            hashPara.Add("@Query", approvalNote);
            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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

        /// <summary>
        /// read modules details as per paramenter details
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="userId"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public DataSet ReadProfileWiseModules(string profileId, string userId, string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Proc_Get_Profile_Wise_Module";
            hashPara.Add("@ProfileId", profileId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }


        /// <summary>
        /// Filter HSN and ZCAP details as per filter parameter
        /// </summary>
        /// <param name="status"></param>
        /// <param name="requestNo"></param>
        /// <param name="userId"></param>
        /// <param name="moduleId"></param>
        /// <param name="moduleType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet SearchMasterRequests(string status, string requestNo, string userId, string moduleId, string moduleType, string sapcodeNo = "", string StartDate = "", string EndDate = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Status", status);
            hashPara.Add("@RequestNo", requestNo + "%");
            hashPara.Add("@SAPCode", sapcodeNo + "%");  
            hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@ModuleType", moduleType);
            hashPara.Add("@Start_Date", StartDate);
            hashPara.Add("@End_Date", EndDate);

            string procName = "pr_Search_Master_Request_HSNZCAP";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        /// <summary>
        /// delete records from table base on id and master header id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int DeleteRow(int id, int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_T_HSN_ZCAP_DetailsTB_By_HSN_ZCAP_Detaiils_Id";
            int result = 0;

            hashPara.Add("@HSN_ZCAP_Detaiils_Id", id);
            hashPara.Add("@MasterHeaderId", MasterHeaderId);
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

        /// <summary>
        /// Get details data by master header id
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public DataSet GetDetailsData(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_HSN_ZCAP_DetailsTB_By_Master_Header_Id";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

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

        /// <summary>
        /// Get HSN/ZCAP master details by master heder id and Module id
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public DataSet GetDetailsDataListByMasterHeaderId(string Master_Header_Id, string ModuleId)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetHSN_ZCAPDataListByMasterHeaderId";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@ModuleId", ModuleId);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }

        /// <summary>
        /// Save and update records details in DB
        /// </summary>
        /// <param name="objZcapHsn"></param>
        /// <returns></returns>
        public int SaveDetails(ZcapHsnMasterCreate objZcapHsn)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_HSN_ZCAP_DetailsTB";
            int result = 0;

            hashPara.Add("@HSN_ZCAP_Detaiils_Id", objZcapHsn.HSN_ZCAP_Detaiils_Id);
            hashPara.Add("@Master_Header_Id", objZcapHsn.Master_Header_Id);
            hashPara.Add("@sMaterial_Code", objZcapHsn.sMaterial_Code);
            hashPara.Add("@sSupp_plant", objZcapHsn.sSupp_plant);
            hashPara.Add("@sRece_plant", objZcapHsn.sRece_plant);
            hashPara.Add("@sCondintion_type", objZcapHsn.sCondintion_type);
            hashPara.Add("@sZcapRate", objZcapHsn.sZcapRate);
            hashPara.Add("@sUOM", objZcapHsn.sUOM);
            hashPara.Add("@sSTONum", objZcapHsn.sSTONum);
            hashPara.Add("@sHSN_Code", objZcapHsn.sHSN_Code);
            hashPara.Add("@sGST_Code", objZcapHsn.sGST_Code);
            hashPara.Add("@sIsLUTCond", objZcapHsn.sIsLUTCond);
            hashPara.Add("@sRemarks", objZcapHsn.sRemarks);
            hashPara.Add("@IsActive", objZcapHsn.IsActive);
            hashPara.Add("@CreatedBy", objZcapHsn.CreatedBy);
            hashPara.Add("@CreatedIp", objZcapHsn.CreatedIp);
            hashPara.Add("@sMaterial_Name", objZcapHsn.sMaterial_Name);
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

        /// <summary>
        /// update workflow base on the updated material series with number of parameter
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int UpdateDetails(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Update_T_HSN_ZPAC_TypeTB_By_MasterHeaderId";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);

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

        /// <summary>
        /// get list of remarks as per department approval
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public DataTable GetRemarksByUser(int MasterHeaderId, string MassRequestProcessId)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetRemarksData";
            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            return ds.Tables[0];
        }

    }

}