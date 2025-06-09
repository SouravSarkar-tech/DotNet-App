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
    public class MaterialMasterAccess
    {

        public string mRequestNo { get; set; }

        public DataSet GetBulkChangeDataListByMasterHeaderId(string Master_Header_Id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetBulkChangeDataListByMasterHeaderId";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }

        public DataSet FillPropertiesDataSet(int ModuleID, string PlantID, string sflag)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "sp_Get_Fields_MatMasterCMS";
            hashPara.Add("@ModuleID", ModuleID);
            hashPara.Add("@PlantID", PlantID);
            hashPara.Add("@sflag", sflag);

            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet ReadMaterialMasterRequests(string status, string requestNo, string userId, string moduleId, string moduleType, string sapcodeNo = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Status", status);
            hashPara.Add("@RequestNo", requestNo + "%");
            hashPara.Add("@SAPCode", sapcodeNo + "%");
            hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@ModuleType", moduleType);

            string procName = "pr_Search_Master_Request";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

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

            string procName = "pr_Search_Master_Request";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <returns></returns>
        public DataSet ReadReqDateForChange(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Get_Mat_ReqDate_By_MasterHeaderId";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <param name="sstatus"></param>
        /// <param name="text2"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public int SaveMassSync(string MasterHeaderId, string sstatus, string UserId, string IpAddress, string sFlag, bool IsDraf)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_MASS_SAP_Sync_Job_Log";
            int result = 0;
            hashPara.Add("@Master_Header_Id", MasterHeaderId);
            hashPara.Add("@sStatus", sstatus);
            hashPara.Add("@CreatedBy", UserId);
            hashPara.Add("@CreatedIp", IpAddress);
            hashPara.Add("@sFlag", sFlag);
            hashPara.Add("@IsDraf", IsDraf);
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
        ///     MME_8300002156
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public bool IsSAPMASSintegrationChkAval(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsSAPMASSintegrationChkAval";

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

            try
            {
                //DNRCOMM// objDal.OpenConnection();
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
        /// MSC_8300001775
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public bool IsSAPintegrationMASSPending(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsSAPMASSintegrationPending";

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
        /// MSC_8300001775
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public DataSet GetSyncMassData(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MasterHeaderId", MasterHeaderId);

            string procName = "pr_GetSyncMassPendingJobListChk";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet FindMDMApproved()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();

            string procName = "pr_Find_MDM_Request_Pending";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet GetMaterialListByMassMaterialProcessID(string MassRequestProcessId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
            hashPara.Add("@UserId", userId);

            string procName = "pr_GetMaterialListByMassMaterialProcessID";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet GetMaterialDataByMassMaterialProcessID(string MassRequestProcessId, string userId)
        {
            try
            {
                DataAccessLayer objDal = new DataAccessLayer();
                Hashtable hashPara = new Hashtable();
                hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);
                hashPara.Add("@UserId", userId);

                //string procName = "pr_GetMaterialMasterMassRequestDetails";
                string procName = "pr_GetMaterialMasterMassRequestDetails";
                return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveMaterialHeader(string masterHeaderId, string moduleId, string userId, string flg)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                retVal = (int)ret.Value;
                if (retVal > 0)
                {
                    masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
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

/// Carve_LC17&LC23_8400000406
        public int SaveMaterialHeaderC(string masterHeaderId, string companyId, string moduleId, string userId, string flg, string Master_Category = "", string Reference_Id = "", string Plant_Id="0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant_Id);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
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
                //Srinidhi
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

        public int SaveMaterialHeader(string masterHeaderId, string moduleId, string PlantGroupID, string userId, string flg, string Plant = "", string Reference_Id = "", string Purchasing_Group = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", PlantGroupID);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@Purchasing_Group", Purchasing_Group);
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
                //Srinidhi
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

        public int DeleteMassRequest(string masterHeaderId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_DeleteMassRequest";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
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

        public int GenerateMassRequestProcess(string masterHeaderId, string approvedByDeptId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GenerateMassRequestProcess";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
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

        public DataSet ReadModules(string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Module_Id,Module_Name FROM M_Module WHERE ModuleType = '" + moduleType + "' AND IsActive = 'TRUE' ORDER BY Module_Name";
            return objDal.FillDataSet(query, "M_Module");
        }

        public DataSet ReadModulesByModuleType(string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_ReadModulesByModuleType";
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

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

        public int CheckSectionStatus(string sectionId, string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Check_Request_Status";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@SectionId", sectionId);
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
        /// to check RDM request status for Customer IRF
        /// Create By Nitin R , DT03052019 
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="masterHeaderId"></param>
        /// <returns></returns>
        public int CheckRDMStatus(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Check_RDMRequest_Status";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
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
        /// to check request type for Customer IRF
        /// Create By Nitin R , DT03052019
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="masterHeaderId"></param>
        /// <returns></returns>
        public int CheckRequestType(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Check_Request_Type";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
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


        public int ApproveRequestM(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_M";
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

        public int ApproveRequest(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_Vendor";
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

        public int ApproveRequestGL(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_GL";
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

        //public int MassApproveRequest(string MassRequestProcessId, string approvedByDeptId, string userId)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Utility objUtil = new Utility();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Mass_Approve_Request";
        //    hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
        //    hashPara.Add("@ApprovedByDept", approvedByDeptId);
        //    hashPara.Add("@UserId", userId);
        //    hashPara.Add("@IpAddress", objUtil.GetIpAddress());
        //    hashPara.Add("@SendMail", "1");

        //    try
        //    {
        //        objDal.OpenConnection();
        //        return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objDal.CloseConnection(objDal.cnnConnection);
        //        objDal = null;
        //    }
        //}

        public int MassApproveRequest(string MassRequestProcessId, string approvedByDeptId, string userId, string approvalNote)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Mass_Approve_Request";
            hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            hashPara.Add("@ApprovalComment", approvalNote);
            hashPara.Add("@SendMail", "1");
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

        public int RollbackRequest(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Reject_Request";
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

        public int UngroupRequestFromMassProcess(string MassRequestProcessDetailId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_UngroupRequestFromMassProcess";
            hashPara.Add("@MassRequestProcessDetailId", MassRequestProcessDetailId);
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

        public int UpdateRejectionRemark(string MassRequestProcessDetailId, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_UpdateRejectionRemark";
            hashPara.Add("@MassRequestProcessDetailId", MassRequestProcessDetailId);
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

        public int MassRollbackRequest(string MassRequestProcessId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Mass_Reject_Request";
            hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
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

        public DataSet ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId = null)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@DepartmentId", departmentId);

            string procName = "pr_GetRollBackAllowedDepartment";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet ReadDeparmentListForRollbackOld(string masterHeaderId, string departmentId, string moduleId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@DepartmentId", departmentId);
            hashPara.Add("@ModuleId", moduleId);

            string procName = "Proc_Get_Dept_List_For_Rollback";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataTable GetDashBoardByModuleId(string ModuleId)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDashBoardData";
            hashPara.Add("@ModuleId", ModuleId);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds.Tables[0];
        }

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


        public static bool IsUserHasInputRights(string moduleId, string deptId, string sectionId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Proc_Chk_UserInput_Rights_On_Section";

            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@DeptId", deptId);
            hashPara.Add("@SectionId", sectionId);
            hashPara.Add("@UserId", userId);
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
            { throw ex; }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
            return retVal;
        }

        /// <summary>
        /// //MSC_8300001775 Start
        /// </summary>
        /// <param name="MasterId"></param>
        /// <returns></returns>
        public static bool IsUserHasSChangeReq(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Proc_Chk_SChangeReq_MasterHeaderId";

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            try
            {
                //objDal.OpenConnection();
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            { //throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
            return retVal;
        }

        public static bool IsUserHasPRReq(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Proc_Chk_SChangeReq_MasterHeaderId";

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            try
            {
                //objDal.OpenConnection();
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            { //throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
            return retVal;
        }



        public DataSet GetReportByMasterHeaderId(string Master_Header_id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_rpt_MaterialMaster";
            hashPara.Add("@Master_Header_id", Master_Header_id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }

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
        /// PROSOL_SDT16092019
        /// PROSOL_SDT09112021
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public bool IsProsolSPOCPending(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsProsolSPOCPending";

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



        public bool IsFinalApproval(string MasterHeaderId, string MassRequestProcessId, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "pr_IsFinalApproval";

            hashPara.Add("@Master_Header_Id", MasterHeaderId);
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);
            hashPara.Add("@User_Id", UserId);

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

        public bool IsInitiatorApprover(string MasterHeaderId, string deptId, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Proc_IsUserInitiator";

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@DeptId", deptId);
            hashPara.Add("@UserId", UserId);

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

        public bool IsMassSAPintegrationPending(string MassRequestProcessId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = true;
            string procName = "Pr_IsMassSAPintegrationPending";
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);

            try
            {
                //DNRCOMM//objDal.OpenConnection();
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    retVal = false;
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

        public bool IsMassSAPValidationPending(string MassRequestProcessId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = true;
            string procName = "Pr_IsMassSAPValidationPending";
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);

            try
            {
                objDal.OpenConnection();
                int result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                if (result == 0)
                {
                    retVal = false;
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

        public int UpdateRequestComplete(string Master_Header_Id, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_UpdateRequestComplete";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Created_By", UserId);
            hashPara.Add("@Created_IP", objUtil.GetIpAddress());
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

        //public int SaveMaterialDefaultDataROH(string masterHeaderId, string companyId, string moduleId, string userId, string flg, string Master_Category = "", string Reference_Id = "")
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Utility objUtil = new Utility();
        //    SqlCommand objCommand = new SqlCommand();
        //    string procName = "pr_Ins_T_Mat_Default_Data_ROH";
        //    int retVal = 0;

        //    objCommand.Parameters.AddWithValue("@Master_Header_Id", masterHeaderId);
        //    objCommand.Parameters.AddWithValue("@PlantId", moduleId);
        //    objCommand.Parameters.AddWithValue("@Purchasing_Group", moduleId);
        //    objCommand.Parameters.AddWithValue("@Origin_Country_Id", moduleId);
        //    objCommand.Parameters.AddWithValue("@Storage_Location", moduleId);
        //    objCommand.Parameters.AddWithValue("@Inspection_Type", moduleId);
        //    objCommand.Parameters.AddWithValue("@Certificate_Type", moduleId);
        //    objCommand.Parameters.AddWithValue("@Ctrl_Key_QM_Procurement", moduleId);
        //    objCommand.Parameters.AddWithValue("@UserId", userId);
        //    objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());

        //    SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
        //    ret.Direction = ParameterDirection.ReturnValue;

        //    try
        //    {
        //        objDal.OpenConnection();
        //        objCommand.Connection = objDal.cnnConnection;
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.CommandText = procName;
        //        objCommand.ExecuteNonQuery();
        //        retVal = (int)ret.Value;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objDal.CloseConnection(objDal.cnnConnection);
        //        objDal = null;
        //    }
        //    return retVal;
        //}

        public void SaveMaterialDefaultDataROH(string masterHeaderID, string plantID, string purchasingGroup, string storageLocation, string userId, out string SalesOrgID, out string DistributionChannelID, out int ret)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_T_Mat_Default_Data_ROH";
            SalesOrgID = DistributionChannelID = "";
            //int retVal = 0;

            objCommand.Parameters.AddWithValue("@Master_Header_Id", masterHeaderID);
            objCommand.Parameters.AddWithValue("@PlantId", plantID);
            objCommand.Parameters.AddWithValue("@Purchasing_Group", purchasingGroup);
            objCommand.Parameters.AddWithValue("@Storage_Location", storageLocation);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());

            SqlParameter retrn = objCommand.Parameters.Add("@return", SqlDbType.Int);
            retrn.Direction = ParameterDirection.ReturnValue;

            SqlParameter outSalesOrgID = objCommand.Parameters.Add("@SalesOrg", SqlDbType.VarChar, 4);
            outSalesOrgID.Direction = ParameterDirection.Output;

            SqlParameter outDistChannelID = objCommand.Parameters.Add("@DistChannel", SqlDbType.VarChar, 4);
            outDistChannelID.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                ret = (int)retrn.Value;
                if (ret > 0)
                {
                    SalesOrgID = SafeTypeHandling.ConvertToString(outSalesOrgID.Value);
                    DistributionChannelID = SafeTypeHandling.ConvertToString(outDistChannelID.Value);
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
            //return retVal;
        }

        public void SaveMaterialDefaultDataFGSFG(string masterHeaderID, string plantID, string storageLocation, string userId, out string SalesOrgID, out string DistributionChannelID, out int ret)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_T_Mat_Default_Data_FG_SFG";
            SalesOrgID = DistributionChannelID = "";
            //int retVal = 0;

            objCommand.Parameters.AddWithValue("@Master_Header_Id", masterHeaderID);
            objCommand.Parameters.AddWithValue("@PlantId", plantID);
            //objCommand.Parameters.AddWithValue("@Purchasing_Group", purchasingGroup);
            objCommand.Parameters.AddWithValue("@Storage_Location", storageLocation);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());

            SqlParameter retrn = objCommand.Parameters.Add("@return", SqlDbType.Int);
            retrn.Direction = ParameterDirection.ReturnValue;

            SqlParameter outSalesOrgID = objCommand.Parameters.Add("@SalesOrg", SqlDbType.VarChar, 4);
            outSalesOrgID.Direction = ParameterDirection.Output;

            SqlParameter outDistChannelID = objCommand.Parameters.Add("@DistChannel", SqlDbType.VarChar, 4);
            outDistChannelID.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                ret = (int)retrn.Value;
                if (ret > 0)
                {
                    SalesOrgID = SafeTypeHandling.ConvertToString(outSalesOrgID.Value);
                    DistributionChannelID = SafeTypeHandling.ConvertToString(outDistChannelID.Value);
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
            //return retVal;
        }

        public void SaveMaterialDefaultDataPROM(string masterHeaderID, string plantID, string storageLocation, string userId, out string SalesOrgID, out string DistributionChannelID, out int ret)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_T_Mat_Default_Data_PROM";
            SalesOrgID = DistributionChannelID = "";
            //int retVal = 0;

            objCommand.Parameters.AddWithValue("@Master_Header_Id", masterHeaderID);
            objCommand.Parameters.AddWithValue("@PlantId", plantID);
            objCommand.Parameters.AddWithValue("@Storage_Location", storageLocation);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());

            SqlParameter retrn = objCommand.Parameters.Add("@return", SqlDbType.Int);
            retrn.Direction = ParameterDirection.ReturnValue;

            SqlParameter outSalesOrgID = objCommand.Parameters.Add("@SalesOrg", SqlDbType.VarChar, 4);
            outSalesOrgID.Direction = ParameterDirection.Output;

            SqlParameter outDistChannelID = objCommand.Parameters.Add("@DistChannel", SqlDbType.VarChar, 4);
            outDistChannelID.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                ret = (int)retrn.Value;
                if (ret > 0)
                {
                    SalesOrgID = SafeTypeHandling.ConvertToString(outSalesOrgID.Value);
                    DistributionChannelID = SafeTypeHandling.ConvertToString(outDistChannelID.Value);
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
            //return retVal;
        }

        public void SaveSalesDefaultERSHIBEZMBW(string masterHeaderID, string plantID, string storageLocation, string userId, out string SalesOrgID, out string DistributionChannelID, out int ret)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_T_Mat_Sales_Default_ERSA_HIBE_ZMBW";
            SalesOrgID = DistributionChannelID = "";
            //int retVal = 0;

            objCommand.Parameters.AddWithValue("@Master_Header_Id", masterHeaderID);
            objCommand.Parameters.AddWithValue("@PlantId", plantID);
            objCommand.Parameters.AddWithValue("@Storage_Location", storageLocation);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());

            SqlParameter retrn = objCommand.Parameters.Add("@return", SqlDbType.Int);
            retrn.Direction = ParameterDirection.ReturnValue;

            SqlParameter outSalesOrgID = objCommand.Parameters.Add("@SalesOrg", SqlDbType.VarChar, 4);
            outSalesOrgID.Direction = ParameterDirection.Output;

            SqlParameter outDistChannelID = objCommand.Parameters.Add("@DistChannel", SqlDbType.VarChar, 4);
            outDistChannelID.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                ret = (int)retrn.Value;
                if (ret > 0)
                {
                    SalesOrgID = SafeTypeHandling.ConvertToString(outSalesOrgID.Value);
                    DistributionChannelID = SafeTypeHandling.ConvertToString(outDistChannelID.Value);
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
            //return retVal;
        }

        public DataSet GetMatPendingRequests(string purchaseGrp, string plant_ID, string module_ID, string start_Date, string end_Date, string pendingDays, string status, string dept, string createdBy)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@PurchaseGrp", purchaseGrp + "%");
            hashPara.Add("@Plant_Id", plant_ID + "%");
            hashPara.Add("@Module_Id", module_ID);
            hashPara.Add("@Start_Date", start_Date);
            hashPara.Add("@End_Date", end_Date);
            hashPara.Add("@Status", status);
            hashPara.Add("@ApprDept", dept);
            hashPara.Add("@PendingDays", pendingDays);
            hashPara.Add("@CreatedBy", createdBy);

            string procName = "pr_Pending_Request_Report";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            //pr_Pending_Request_Report '','','162','','','','','1'
        }

        /// <summary>
        /// Created By Manali SDT31052019
        /// Get Customer request data from DB
        /// </summary>
        /// <param name="purchaseGrp"></param>
        /// <param name="plant_ID"></param>
        /// <param name="module_ID"></param>
        /// <param name="start_Date"></param>
        /// <param name="end_Date"></param>
        /// <param name="pendingDays"></param>
        /// <param name="status"></param>
        /// <param name="dept"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataSet GetCustomerPendingRequests(string plant_ID, string module_ID, string start_Date, string end_Date, string status, string dept, string pendingDays, string createdBy, string pZone, string pDivision, string pTerritory)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Plant_Id", plant_ID);
            hashPara.Add("@Module_Id", module_ID);
            hashPara.Add("@Start_Date", start_Date);
            hashPara.Add("@End_Date", end_Date);
            hashPara.Add("@Status", status);
            hashPara.Add("@ApprDept", dept);
            hashPara.Add("@PendingDays", pendingDays);
            hashPara.Add("@CreatedBy", createdBy);
            hashPara.Add("@Zone", pZone);
            hashPara.Add("@Division", pDivision);
            hashPara.Add("@Territory", pTerritory);

            string procName = "pr_Cust_PendingReport";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public DataSet GetVendorPendingRequests(string plant_ID, string module_ID, string start_Date, string end_Date, string status, string dept, string pendingDays, string createdBy)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Plant_Id", plant_ID);
            hashPara.Add("@Module_Id", module_ID);
            hashPara.Add("@Start_Date", start_Date);
            hashPara.Add("@End_Date", end_Date);
            hashPara.Add("@Status", status);
            hashPara.Add("@ApprDept", dept);
            hashPara.Add("@PendingDays", pendingDays);
            hashPara.Add("@CreatedBy", createdBy);

            string procName = "pr_Vend_PendingReport";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public DataSet GetCostCenterPendingRequests(string plant_ID, string module_ID, string start_Date, string end_Date, string status, string dept, string pendingDays, string createdBy, string pBusinessArea)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Plant_Id", plant_ID);
            hashPara.Add("@Module_Id", module_ID);
            hashPara.Add("@Start_Date", start_Date);
            hashPara.Add("@End_Date", end_Date);
            hashPara.Add("@Status", status);
            hashPara.Add("@ApprDept", dept);
            hashPara.Add("@PendingDays", pendingDays);
            hashPara.Add("@CreatedBy", createdBy);
            hashPara.Add("@BusinessArea", pBusinessArea);

            string procName = "pr_CostCenter_PendingReport";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public DataSet GetGLPendingRequests(string plant_ID, string module_ID, string start_Date, string end_Date, string status, string dept, string pendingDays, string createdBy)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Plant_Id", plant_ID);
            hashPara.Add("@Module_Id", module_ID);
            hashPara.Add("@Start_Date", start_Date);
            hashPara.Add("@End_Date", end_Date);
            hashPara.Add("@Status", status);
            hashPara.Add("@ApprDept", dept);
            hashPara.Add("@PendingDays", pendingDays);
            hashPara.Add("@CreatedBy", createdBy);

            string procName = "pr_GlMaster_PendingReport";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        /// <summary>
        /// BOM_NWF_SDT05072019
        /// </summary>
        /// <param name="plant_ID"></param>
        /// <param name="module_ID"></param>
        /// <param name="start_Date"></param>
        /// <param name="end_Date"></param>
        /// <param name="status"></param>
        /// <param name="dept"></param>
        /// <param name="pendingDays"></param>
        /// <param name="createdBy"></param>
        /// <param name="pmaterialCode"></param>
        /// <returns></returns>
        public DataSet GetBOMPendingRequests(string plant_ID, string module_ID, string start_Date, string end_Date, string status, string dept, string pendingDays, string createdBy, string pmaterialCode)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Plant_Id", plant_ID);
            hashPara.Add("@Module_Id", module_ID);
            hashPara.Add("@Start_Date", start_Date);
            hashPara.Add("@End_Date", end_Date);
            hashPara.Add("@Status", status);
            hashPara.Add("@ApprDept", dept);
            hashPara.Add("@PendingDays", pendingDays);
            hashPara.Add("@CreatedBy", createdBy);
            hashPara.Add("@pmaterialCode", pmaterialCode);

            string procName = "pr_BOMMaster_PendingReport";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }



        public int SaveImport(MaterialMass objMatMass)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Update_Mat_Mass_Data";
            int result = 0;

            hashPara.Add("@Master_Header_Id", objMatMass.MHId);
            hashPara.Add("@Plant", objMatMass.Plant_Id);
            hashPara.Add("@Material_Code", objMatMass.Material_Code);
            hashPara.Add("@Material_Short_Description", objMatMass.Material_Desc);
            hashPara.Add("@Material_Group", objMatMass.Material_Grp);
            hashPara.Add("@Division", objMatMass.Division);
            hashPara.Add("@Acc_Assignment_Grp", objMatMass.Acc_Assgnt_Grp);
            hashPara.Add("@Product_Hierarchy", objMatMass.ProdHierarchy);
            hashPara.Add("@Pur_Order_Unit_Measure", objMatMass.Pur_Order_Unit_Measure);
            hashPara.Add("@No_Of_Manufacturer", objMatMass.No_Of_Manufacturer);
            hashPara.Add("@Manufacturer_Part_No", objMatMass.Manufacturer_Part_No);
            hashPara.Add("@Manufacturer_Part_Profile", objMatMass.Manufacturer_Part_Profile);
            hashPara.Add("@GR_Processing_Time", objMatMass.GR_Processing_Time);
            hashPara.Add("@Purchase_Order_Text", objMatMass.Purchase_Order_text);
            hashPara.Add("@Control_Code", objMatMass.ControlCode);
            hashPara.Add("@MRP_Type", objMatMass.MRP_type);
            hashPara.Add("@MRP_Controller", objMatMass.MRP_Controller);
            hashPara.Add("@Reorder_Point", objMatMass.Reorder_Point);
            hashPara.Add("@Lot_Size", objMatMass.Lot_Size);
            hashPara.Add("@Min_Lot_Size", objMatMass.Min_Lot_Size);
            hashPara.Add("@Max_Lot_Size", objMatMass.Max_Lot_Size);
            hashPara.Add("@Fixed_Lot_Size", objMatMass.Fixed_Lot_Size);
            hashPara.Add("@Rounding_Value", objMatMass.Rounding_Value);
            hashPara.Add("@Max_Stock_Level", objMatMass.Max_Stock_Level);
            hashPara.Add("@Planning_Time_Fence", objMatMass.Planning_Time_Fence);
            hashPara.Add("@Production_Unit", objMatMass.Prod_Unit);
            hashPara.Add("@Procurement_Type", objMatMass.Procurement_Type);
            hashPara.Add("@Planned_Delivery_Time_Days", objMatMass.Delivery_Time);
            hashPara.Add("@InHouse_Production_Time", objMatMass.Inhouse_Prod_Time);
            hashPara.Add("@Min_Safety_Stock", objMatMass.Min_Safety_Stock);
            hashPara.Add("@Fxd_Lot_Size_Storage_Loc", objMatMass.Fxd_Lot_Size_Storage_Loc);
            hashPara.Add("@Storage_bin", objMatMass.Storage_bin);
            hashPara.Add("@Min_Remaining_Shelf_Life", objMatMass.Min_Rem_Shelf_Life);
            hashPara.Add("@Total_Shelf_Life_Days", objMatMass.Total_Shelf_Life);
            hashPara.Add("@Profit_Center", objMatMass.Profit_Center);
            hashPara.Add("@Unit_Issue", objMatMass.Unit_Issue);
            hashPara.Add("@Is_QM_in_Procurement", objMatMass.Is_QM_is_Procurement);
            hashPara.Add("@Certificate_Type", objMatMass.Certificate_Type);
            hashPara.Add("@Ctrl_Key_QM_Procurement", objMatMass.Ctrl_Key_QM_Procurement);
            hashPara.Add("@Interval_Nxt_Inspection", objMatMass.Interval_Nxt_Inspection);
            hashPara.Add("@Inspection_Type", objMatMass.Inspection_Type);
            hashPara.Add("@Valuation_Class", objMatMass.Valuation_Class);
            hashPara.Add("@Price_Ctrl_Indicator", objMatMass.Price_Ctrl_Indicator);
            hashPara.Add("@Lot_Size_Prd_Cost_Est", objMatMass.Lot_Size_Prd_Cost_Est);

            hashPara.Add("@IsActive", objMatMass.IsActive);
            hashPara.Add("@UserId", objMatMass.UserId);
            hashPara.Add("@UserIp", objMatMass.IPAddress);

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

        //Srinidhi
        public DataSet Testing()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("Select * from Testing", "Testing");
        }

        public DataSet TestingProd()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("Select * from Testing_Prod", "Testing");
            //("select * from Testing");
        }

        public DataSet GetPharmaData()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("SELECT LookUp_Desc AS LookUp_Desc,LookUp_Code FROM M_LookUp_Audit WHERE Control_Name = 'ddlPharmo' AND IsActive = 1 and isnull(Is_Hidden,0) <> 1 ", "PharmoStatus");
        }

        public bool InsertPharmo(int id, string p, string p_2, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            //SqlTransaction objTrans;
            //objDal.OpenConnection();
            //objTrans = objDal.cnnConnection.BeginTransaction();
            return objDal.ExecuteQuery("Insert into Testing(SNo,MatName,Pharmo) Values(" + id + ",'" + p + "','" + p_2 + "')", ref cnn, ref objTrans);
        }

        public bool InsertProds(int id, string p, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            //SqlTransaction objTrans;
            //objDal.OpenConnection();
            //objTrans = objDal.cnnConnection.BeginTransaction();
            return objDal.ExecuteQuery("Insert into Testing_Prod(PNo,ProdName) Values(" + id + ",'" + p + "')", ref cnn, ref objTrans);
        }

        public bool UpdatePharmo(string p, string p_2, int p_3, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            //SqlTransaction objTrans;
            //objDal.OpenConnection();
            //objTrans = objDal.cnnConnection.BeginTransaction();
            return objDal.ExecuteQuery("Update Testing Set MatName = '" + p + "',Pharmo='" + p_2 + "' where SNo = " + p_3, ref cnn, ref objTrans);
        }

        public bool UpdateProds(string p, int p_2, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            //SqlTransaction objTrans;
            //objDal.OpenConnection();
            //objTrans = objDal.cnnConnection.BeginTransaction();
            return objDal.ExecuteQuery("Update Testing_Prod Set ProdName = '" + p + "' where PNo = " + p_2, ref cnn, ref objTrans);
        }

        public bool Delete(int id, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            //SqlTransaction objTrans;
            //objDal.OpenConnection();
            //objTrans = objDal.cnnConnection.BeginTransaction();
            return objDal.ExecuteQuery("Delete from Testing where SNo = " + id, ref cnn, ref objTrans);
        }

        public bool Delete_Prod(int id, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.ExecuteQuery("Delete from Testing_Prod where PNo = " + id, ref cnn, ref objTrans);
        }

        public int GetMasterHeaderId(string p)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.ReturnInt("Select Master_Header_Id from T_Vendor_General_Type where Customer_Code LIKE" + "'0000" + p + "'");
        }

        public bool IsSapValidationPending(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsSAPValidationPending";

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

        public bool IsSAPQAIntegrationPending(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsSAPQAIntegrationPending";

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

            try
            {
                //DNRCOMM// objDal.OpenConnection();
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

        public DataSet GetListByMassMaterialProcessID(string MassRequestProcessId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
            hashPara.Add("@UserId", userId);

            string procName = "pr_GetMassListByMassProcessID";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public int ForwardRequest(string masterHeaderId, string forwardedByDeptId, string query, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Forward_Request";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ForwardedByDept", forwardedByDeptId);
            hashPara.Add("@Query", query);
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

        public static bool Get_IsHSNGSTFilledNew1(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {
            //try
            //{
            bool flg = false;
            SqlConnection con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            using (SqlCommand cmd = new SqlCommand("pr_IsHSNGSTFilled"))
            {
                cmd.Connection = con1;
                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
                if (con1.State == ConnectionState.Closed)
                {
                    con1.Open();
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Master_Header_Id", Master_Header_Id));
                cmd.Parameters.Add(new SqlParameter("@Mass_Request_Process_Id", MassRequestProcessId));
                cmd.Parameters.Add(new SqlParameter("@User_Id", User_Id));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        flg = true;
                    }
                }
                catch (Exception ex)
                {
                }
                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
            }
            return flg;
            //}
            //catch (Exception ex)
            //{
            //}
            //return flg;
        }
        public static bool Get_IsHSNGSTFilled1(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {
            SqlConnection con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            //DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();
            bool flg = false;

            string procName = "pr_IsHSNGSTFilled";
            try
            {
                cmd.Connection = con1;

                if (con1.State == ConnectionState.Closed)
                {
                    con1.Open();
                }

                //objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@Master_Header_Id", Master_Header_Id));
                cmd.Parameters.Add(new SqlParameter("@Mass_Request_Process_Id", MassRequestProcessId));
                cmd.Parameters.Add(new SqlParameter("@User_Id", User_Id));

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "Datav");

                if (dstData.Tables[0].Rows.Count > 0)
                {
                    flg = true;
                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    objDal.CloseConnection(objDal.cnnConnection);
            //}


            return flg;
        }

        public DataSet Get_IsHSNGSTFilledNEW(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();

            string procName = "pr_IsHSNGSTFilled";
            try
            {

                //objDal.CloseConnection(objDal.cnnConnection);

                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                //Srinidhi
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@Master_Header_Id", Master_Header_Id));
                cmd.Parameters.Add(new SqlParameter("@Mass_Request_Process_Id", MassRequestProcessId));
                cmd.Parameters.Add(new SqlParameter("@User_Id", User_Id));

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "HSNData");
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
        /// PROV-CCP-MM-941-23-0045 Start
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <param name="User_Id"></param>
        /// <param name="MassRequestProcessId"></param>
        /// <returns></returns>
        //Make Kinaxis field mandatory for  3,4,8 & 5 series code for Excise approver Start
        public static bool Get_IsKinaxisFilled(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_IsKinaxisTFilled";
            DataSet ds;
            bool flg = false;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);
            hashPara.Add("@User_Id", User_Id);

            try
            { 
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flg = true;
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
            return flg; 
        }
        //Make Kinaxis field mandatory for  3,4,8 & 5 series code for Excise approver End



        //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
        public static bool Get_IsHSNGSTFilled(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_IsHSNGSTFilled";
            DataSet ds;
            bool flg = false;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);
            hashPara.Add("@User_Id", User_Id);

            try
            {
                //objDal.OpenConnection();
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flg = true;
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
            return flg;



            //try
            //{
            //    ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            //    if(ds.Tables[0] != null)
            //    if (ds.Tables[0].Rows.Count > 0)
            //        flg = true;

            //    return flg;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    //return flg;
            //}
            //finally
            //{
            //    objDal = null;
            //}
        }
        //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End

        //Start Addition By Swati M Date: 15.10.2018
        public int SaveMaterialHeaderCM(string masterHeaderId, string companyId, string moduleId, string userId, string flg, string divisionType, string division, string zone, string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header_Customer";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);
            objCommand.Parameters.AddWithValue("@DivisionType", divisionType);
            objCommand.Parameters.AddWithValue("@Division", division);
            objCommand.Parameters.AddWithValue("@Zone", zone);
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
                //Srinidhi
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

        public int ApproveRequestCustomer(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_Customer";
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

        //End Addition By Swati M Date: 15.10.2018

        //Start Addition by Swati on 16.01.2019

        public int SaveMaterialHeaderPM(string masterHeaderId, string moduleId, string userId, string flg)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
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
                //Srinidhi
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

        //End

        //Code added by Swati on 17.04.2019
        public DataSet SearchCustomerMasterRequests(string status, string requestNo, string userId, string moduleId, string moduleType, string sapcodeNo = "", string StartDate = "", string EndDate = "")
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
            string procName = "pr_Search_Master_Request_Customer";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }
        //End

        /// <summary>
        /// PROSOL_SDT16092019
        /// added by Manali Chavan
        /// for generating reports as per parameters passed.
        /// added GetProsolreport in stored procedure.
        /// </summary>
        /// <param name="ProsolId"></param>
        /// <param name="MWTId"></param>
        /// <param name="Start_Date"></param>
        /// <param name="End_Date"></param>
        /// <param name="mode">RC-Retrigger/SS-Send Success</param>
        /// <returns></returns>
        public DataSet GetProsolRecord(string ProsolId, string MWTId, string Start_Date, string End_Date, string mode)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@ProsolId", ProsolId);
            hashPara.Add("@MWTId", MWTId);
            hashPara.Add("@Start_Date", Start_Date);
            hashPara.Add("@End_Date", End_Date);
            hashPara.Add("@mode", mode);

            string procName = "pr_GetProsolReport";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        /// <summary>
        /// PROSOL_SDT16092019
        /// </summary>
        /// <param name="ProsolId"></param>
        /// <param name="MWTId"></param>
        /// <param name="Start_Date"></param>
        /// <param name="End_Date"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public DataSet RetriggerProsolRecord(string ProsolId, string MWTId, string Start_Date, string End_Date, char mode)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@ProsolId", ProsolId);
            hashPara.Add("@MWTId", MWTId);
            hashPara.Add("@Start_Date", Start_Date);
            hashPara.Add("@End_Date", End_Date);
            hashPara.Add("@mode", mode);
            string procName = "pr_GetProsolRetriggerLog";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }


        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="Module_Id"></param>
        /// <param name="User_Id"></param>
        /// <param name="Plant_Group_Id"></param>
        /// <returns></returns>
        public DataSet GetSMSection(int Module_Id, string User_Id, int Plant_Group_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Module_Id", Module_Id);
            hashPara.Add("@User_Id", User_Id);
            hashPara.Add("@Plant_Group_Id", Plant_Group_Id);
            string procName = "pr_GetSectionByModuleId";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="moduleId"></param>
        /// <param name="PlantGroupID"></param>
        /// <param name="userId"></param>
        /// <param name="flg"></param>
        /// <param name="Plant"></param>
        /// <param name="Reference_Id"></param>
        /// <returns></returns>
        public int SaveSingleMatChangeHeader(string masterHeaderId, string moduleId, string PlantGroupID, string userId, string flg, string Plant = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header_SingleChg";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", PlantGroupID);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            //objCommand.Parameters.AddWithValue("@Purchasing_Group", Purchasing_Group);
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
                //Srinidhi
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
        /// MSC_8300001775
        /// </summary>
        /// <param name="Module_Id"></param>
        /// <param name="Master_Header_Id"></param>
        /// <param name="Section_Id"></param>
        /// <returns></returns>
        public int SaveSectionSMChange(string Module_Id, int Master_Header_Id, string Section_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_SectionSMChangeTB";
            int result = 0;
            hashPara.Add("@Module_Id", Module_Id);
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Id", Section_Id);
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
        /// MSC_8300001775
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <returns></returns>
        public DataSet ReadSMatChange(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Get_Mat_SChangeLog_By_MasterHeaderId";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public DataSet ReadPR(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Get_PR_By_MasterHeaderId";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }


        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <returns></returns>
        public DataSet ReadSMatMassChange(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Get_Mat_MassChangeLog_By_MasterHeaderId";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

    }
}