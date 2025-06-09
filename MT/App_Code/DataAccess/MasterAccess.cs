using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Accenture.MWT.DataAccess;
using System.Collections;

/// <summary>
/// Summary description for CustomerMasterAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class MasterAccess
    {
        public int mModule_Id { get; set; }
        public int mMasterHeaderId { get; set; }
        public string mModuleName { get; set; }
        public string mMasterSAPCode { get; set; }
        public string mRequestNo { get; set; }

        public MasterAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetSAPIntegrationDataByMasterHeaderID(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_SAP_IntegrationDataByMasterHeaderID";
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

        public DataSet GetMWTWorkFlowDataByMasterHeaderID(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MWT_WorkFlowDataByMasterHeaderID";
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

        public DataSet GetMWTMasterDetailsByMasterHeaderID(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetMWTMasterDetailsByMasterHeaderID";
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

        public DataSet GetSAPIntegrationDataByMassRequestProcessId(string MassRequestProcessId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_SAP_IntegrationDataByMassRequestProcessId";
            DataSet ds;

            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);

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

        public DataSet GetMWTWorkFlowDataByMassRequestProcessId(string MassRequestProcessId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MWT_WorkFlowDataByMassRequestProcessId";
            DataSet ds;

            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);

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

        public DataSet GetMWTMasterDetailsByMassRequestProcessId(string MassRequestProcessId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetMWTMasterDetailsByMassRequestProcessId";
            DataSet ds;

            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);

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

        public static bool Get_IsFinalApproval(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_IsFinalApproval";
            DataSet ds;
            bool flg = false;
            
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);
            hashPara.Add("@User_Id", User_Id);

            try
            {                
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds.Tables[0].Rows.Count > 0)
                    flg = true;
                
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

        public DataSet GetLSMWMaterialCreateData()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string procName = "pr_LSMW_Material_Create";
            DataSet ds;

            try
            {
                ds = objDal.FillDataSet(procName);
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

        public int GenerateBlockRequestC(string masterHeaderId, string companyId, string moduleId, string ModuleCode, string userId, string SAPMasterCode, string Master_Description, string Master_Category = "", string Reference_Id = "", string Zone = "")//Zone =""  for genearting customer block request and passing zone to save in table. By manali Chavan 01-10-2020
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Block_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@ModuleCode", ModuleCode);
            objCommand.Parameters.AddWithValue("@SAPMasterCode", SAPMasterCode);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            objCommand.Parameters.AddWithValue("@Master_Description", Master_Description);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Zone", Zone);// Added by manali for saving zone in databse and workflow load

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

        public int GenerateBlockRequestM(string masterHeaderId, string Plant_Group_Id, string moduleId, string ModuleCode, string userId, string SAPMasterCode = "", string Master_Description="", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Block_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@ModuleCode", ModuleCode);
            objCommand.Parameters.AddWithValue("@SAPMasterCode", SAPMasterCode);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", Plant_Group_Id);
            
            objCommand.Parameters.AddWithValue("@Master_Description", Master_Description);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());

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
        /// /-- Carve_LC17&LC23_8400000406
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="companyId"></param>
        /// <param name="moduleId"></param>
        /// <param name="ModuleCode"></param>
        /// <param name="userId"></param>
        /// <param name="SAPMasterCode"></param>
        /// <param name="Master_Description"></param>
        /// <param name="Master_Category"></param>
        /// <param name="Reference_Id"></param>
        /// <returns></returns>
        public int GenerateBlockRequestCC(string masterHeaderId, string companyId, string moduleId, string ModuleCode, string userId, string SAPMasterCode, string Master_Description, string Master_Category = "", string Reference_Id = "", string pPlant_Id = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Block_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@ModuleCode", ModuleCode);
            objCommand.Parameters.AddWithValue("@SAPMasterCode", SAPMasterCode);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            objCommand.Parameters.AddWithValue("@Master_Description", Master_Description);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@pPlant_Id", pPlant_Id);
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

        public int GenerateChangeRequestC(string masterHeaderId, string companyId, string GL_Code, string moduleId, string userId, string SAPMasterCode, string Master_Description = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Change_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@GL_Code", GL_Code);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@SAPMasterCode", SAPMasterCode);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            objCommand.Parameters.AddWithValue("@Master_Description", Master_Description);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());

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

        public int GenerateChangeBulkRequestC(string masterHeaderId, string moduleCode, string userId, string companyId = "", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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

        public int GenerateChangeBulkRequestG(string masterHeaderId, string moduleCode, string userId, string GL_Code, string companyId = "32", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_Request_GL";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            //objCommand.Parameters.AddWithValue("@GL_Code", GL_Code);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            //objCommand.Parameters.AddWithValue("@GLGroup", AccGroup);

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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
        /// Carve_LC17&LC23_8400000406
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="moduleCode"></param>
        /// <param name="userId"></param>
        /// <param name="GL_Code"></param>
        /// <param name="companyId"></param>
        /// <param name="Master_Category"></param>
        /// <param name="Reference_Id"></param>
        /// <returns></returns>
        public int GenerateChangeBulkRequestCCN(string masterHeaderId, string moduleCode, string userId, string GL_Code, string Plant_Id = "0", string companyId = "32", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_Request_GL";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId); 
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant_Id);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress()); 
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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


        public int GenerateChangeRequest(string masterHeaderId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_GenerateChangeRequest";
            int retVal = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outModuleId = objCommand.Parameters.Add("@RModuleId", SqlDbType.Int);
            outModuleId.Direction = ParameterDirection.Output;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@RMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter RModuleName = objCommand.Parameters.Add("@RModuleName", SqlDbType.VarChar);
            RModuleName.Direction = ParameterDirection.Output;
            RModuleName.Size = 50;

            SqlParameter RMasterSAPCode = objCommand.Parameters.Add("@RMasterSAPCode", SqlDbType.VarChar);
            RMasterSAPCode.Direction = ParameterDirection.Output;
            RMasterSAPCode.Size = 50;

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
                    mMasterHeaderId = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(outModuleId.Value);
                    mModuleName = RModuleName.Value.ToString();
                    mMasterSAPCode = RMasterSAPCode.Value.ToString();
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
            return mMasterHeaderId;
        }

        public int GenerateChangeBulkRequest(string masterHeaderId, string moduleCode, string userId, string flg,string Plant_Group_Id = "", string Plant_Id = "", string Reference_Id = "", string Purchasing_Group = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", Plant_Group_Id);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant_Id);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@Purchasing_Group", Purchasing_Group);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);


            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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

        public int GenerateChangeBulkRequestGL(string masterHeaderId, string moduleCode, string userId, string flg, string Plant_Id = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_Request_GL";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant_Id);
            //objCommand.Parameters.AddWithValue("@Company_Id", Company_Id);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);


            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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

        public int GenerateCopyRequest(string masterHeaderId, string companyId, string moduleId, string userId, string flg, string Master_Category = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_GenerateCopyRequest";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
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

        public int GenerateCopyRequestM(string masterHeaderId, string moduleId, string PlantGroupID, string userId, string flg, string Plant = "", string Reference_Id = "", string Purchasing_Group = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_GenerateCopyRequest";
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

        public string GetSelectedModulePlantGrp(string CompanyCode, string MasterAccGrp, string ModuleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetModulePlantGrpByCompanyCodeModuleCode";
            DataSet ds;

            hashPara.Add("@CompanyCode_Id", CompanyCode);
            hashPara.Add("@MasterAccGrp_Id", MasterAccGrp);
            hashPara.Add("@ModuleType", ModuleType);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                return ds.Tables[0].Rows[0][0].ToString();
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

        public string GetSelectedModulePlantGrpByPlantGrp(string PlantGroup_Id, string MasterAccGrp, string ModuleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetModulePlantGrpByPlantGroupIdModuleCode";
            DataSet ds;

            hashPara.Add("@PlantGroup_Id", PlantGroup_Id);
            hashPara.Add("@MasterAccGrp_Id", MasterAccGrp);
            hashPara.Add("@ModuleType", ModuleType);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                return ds.Tables[0].Rows[0][0].ToString();
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

        public string[] GetMWTMasterDetailsByRequestNo(string Request_No)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetMWTMasterDetailsByRequestNo";
            DataSet ds;
            string[] StrId = new string[2];

            hashPara.Add("@Request_No", Request_No);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                //string Master_Header_Id;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StrId[0] = ds.Tables[0].Rows[0]["Mass_Request_Process_Id"].ToString();
                    StrId[1] = ds.Tables[0].Rows[0]["Master_Header_Id"].ToString();

                    //Master_Header_Id = ds.Tables[0].Rows[0]["Master_Header_Id"].ToString();
                }
                else
                {
                    StrId[0] = "0";
                    StrId[1] = "0";
                    //Master_Header_Id = "0";
                }
                return StrId;
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

        public string GetFirstApproverByModuleIdMasterCategory(string Module, string UserId, string CustomerType, string Sales_Region_Id = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetFirstApproverByModuleUserId";
            DataSet ds;

            if (Module == "")
                Module = "0";

            if (Sales_Region_Id == "")
                Sales_Region_Id = "0";

            hashPara.Add("@Module_Id", Module);
            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Master_Category_Code", CustomerType);
            hashPara.Add("@Sales_Region_Id", Sales_Region_Id);


            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                string Full_Name;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Full_Name = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                }
                else
                {
                    Full_Name = "";
                }
                return Full_Name;
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

        public static int DeleteDataBySectionId(string Master_Header_Id, string Section_Id, string Detail_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_Data_By_MHID_SectionID";
            int result = 0;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Id", Section_Id);
            hashPara.Add("@Detail_Id", Detail_Id);

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

        public DataSet GetSAPValidationDataByMasterHeaderID(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_SAP_ValidationDataByMasterHeaderID";
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

        public DataSet GetSAPQAIntegrationDataByMasterHeaderID(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_SAP_QAIntegrationDataByMasterHeaderID";
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

        public string GetRMByModuleIdMasterCategory(string Module, string UserId, string masterCategory)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetReportingMangerByModuleUserId";
            DataSet ds;         

            hashPara.Add("@Module_Id", Module);
            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Master_Category_Code", masterCategory);
            
            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                string Full_Name;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Full_Name = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                }
                else
                {
                    Full_Name = "";
                }
                return Full_Name;
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

        public static bool Get_IsFinalApprovalVendor(string Master_Header_Id, string User_Id, string MassRequestProcessId = "0")
        {

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_IsFinalApproval_Vendor";
            DataSet ds;
            bool flg = false;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Mass_Request_Process_Id", MassRequestProcessId);
            hashPara.Add("@User_Id", User_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds.Tables[0].Rows.Count > 0)
                    flg = true;

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

        //Start Addition By Swati M Date: 16.10.2018
        public int GenerateChangeBulkRequestCust(string masterHeaderId, string moduleCode, string userId, string Zone, string companyId = "", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_Request";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Zone", Zone);

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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
            //End Addition By Swati M Date: 16.10.2018
        }

        public string GetFirstApproverByDivTypeOrZone(string Module, string UserId, string CustomerType, string DivisionType, string Zone)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetFirstApproverByDivTypeOrZone";
            DataSet ds;



            hashPara.Add("@Module_Id", Module);
            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Master_Category", CustomerType);
            hashPara.Add("@DivisionType", DivisionType);
            hashPara.Add("@Zone", Zone);


            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                string Full_Name;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Full_Name = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                }
                else
                {
                    Full_Name = "";
                }
                return Full_Name;
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

        //END Addition By Swati M Date: 16.10.2018


        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public DataSet GetMWTURL(string MWTID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MWTURL";
            hashPara.Add("@mId", MWTID);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="masterHeaderId"></param>
        /// <param name="moduleCode"></param>
        /// <param name="userId"></param>
        /// <param name="flg"></param>
        /// <param name="Plant_Group_Id"></param>
        /// <param name="Plant_Id"></param>
        /// <param name="Reference_Id"></param>
        /// <param name="Purchasing_Group"></param>
        /// <returns></returns>
        public int GenerateMatMassRequest(string masterHeaderId, string moduleCode, string userId, string flg, string Plant_Group_Id = "", string Plant_Id = "", string Reference_Id = "", string Purchasing_Group = "", string StorgLoc_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_MatMass_Change_Req";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", Plant_Group_Id);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant_Id);
            objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
            objCommand.Parameters.AddWithValue("@Purchasing_Group", Purchasing_Group);
            objCommand.Parameters.AddWithValue("@StorgLoc_Id", StorgLoc_Id);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);


            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter OutModuleId = objCommand.Parameters.Add("@OutModuleId", SqlDbType.Int);
            OutModuleId.Direction = ParameterDirection.Output;

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
                    mModule_Id = SafeTypeHandling.ConvertStringToInt32(OutModuleId.Value);
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

    }
}