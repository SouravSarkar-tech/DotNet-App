using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SWApprovalAccess
/// </summary>

namespace Accenture.MWT.DataAccess
{
    public class SWApprovalAccess
    {
        public string mRequestNo { get; set; }
        public int mModule_Id { get; set; }


        //public int SaveMaterialHeader(string masterHeaderId, string moduleId, string PlantGroupID, string userId, string flg, string Reference_Id = "", string Plant = "", string Purchasing_Group = "")
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Utility objUtil = new Utility();
        //    SqlCommand objCommand = new SqlCommand();
        //    string procName = "pr_Save_Master_Header_SWApp";
        //    int retVal = 0;
        //    int masterHeaderId2 = 0;
        //    objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
        //    objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
        //    objCommand.Parameters.AddWithValue("@Plant_Group_Id", PlantGroupID);
        //    objCommand.Parameters.AddWithValue("@Plant_Id", Plant);
        //    objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
        //    objCommand.Parameters.AddWithValue("@Purchasing_Group", Purchasing_Group);
        //    objCommand.Parameters.AddWithValue("@UserId", userId);
        //    objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
        //    objCommand.Parameters.AddWithValue("@Flg", flg);

        //    SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
        //    ret.Direction = ParameterDirection.ReturnValue;

        //    SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
        //    outMasterHeaderId.Direction = ParameterDirection.Output;

        //    SqlParameter outRequestNo = objCommand.Parameters.Add("@RequestNo", SqlDbType.VarChar, 50);
        //    outRequestNo.Direction = ParameterDirection.Output;

        //    try
        //    {
        //        objDal.OpenConnection();
        //        objCommand.Connection = objDal.cnnConnection;
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.CommandText = procName;
        //        //Srinidhi
        //        objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

        //        objCommand.ExecuteNonQuery();
        //        retVal = (int)ret.Value;
        //        if (retVal > 0)
        //        {
        //            masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
        //            mRequestNo = SafeTypeHandling.ConvertToString(outRequestNo.Value);
        //        }

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
        //    return masterHeaderId2;
        //}
        //*************** Change By Rahul on 08/02/2018   ***********************For Update Manager only Software module****
        public void UpdateManager(string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_M_approval_authority_manager";
            objCommand.Parameters.AddWithValue("@UserId", userId);
            
            //SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            //ret.Direction = ParameterDirection.ReturnValue;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                //retVal = (int)ret.Value;
                //if (retVal > 0)
                //{
                //    masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
                //    mRequestNo = SafeTypeHandling.ConvertToString(outRequestNo.Value);
                //}

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
        //END *************** Change By Rahul on 08/02/2018   ***********************For Update Manager only Software module****

        public int SaveSWAppHeader(string masterHeaderId, string PlantGroupID, string userLocation, string otherLocation, string moduleId, string otherModule, string subCategory, string otherCategory, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header_SWApp";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", PlantGroupID);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@OtherModuleId", otherModule);
            objCommand.Parameters.AddWithValue("@SubCategory", subCategory);
            objCommand.Parameters.AddWithValue("@OtherCategory", otherCategory);
            objCommand.Parameters.AddWithValue("@UserLocation", userLocation);
            objCommand.Parameters.AddWithValue("@OtherLocation", otherLocation);
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

        public int SaveSWApprovalData(SWApproval ObjSWApproval)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_SWApproval_Data";
            int result = 0;

            hashPara.Add("@SWApproval_Id", ObjSWApproval.SWApproval_Id);
            hashPara.Add("@Master_Header_Id", ObjSWApproval.Master_Header_Id);
            hashPara.Add("@UserName", ObjSWApproval.UserName);
            hashPara.Add("@Dept", ObjSWApproval.Dept);
            hashPara.Add("@InstallLocation", ObjSWApproval.InstallLocation);
            hashPara.Add("@OtherLoc", ObjSWApproval.OtherLoc);
            hashPara.Add("@InstallDept", ObjSWApproval.InstallDept);
            hashPara.Add("@OtherDept", ObjSWApproval.OtherDept);
            hashPara.Add("@SWName", ObjSWApproval.SWName);
            hashPara.Add("@Manufacturer", ObjSWApproval.Manufacturer);
            hashPara.Add("@MnfrWebsite", ObjSWApproval.MnfrWebsite);
            hashPara.Add("@MnfrCntctName", ObjSWApproval.MnfrCntctName);
            hashPara.Add("@MnfrEmail", ObjSWApproval.MnfrEmail);
            hashPara.Add("@MnfrCntctNo", ObjSWApproval.MnfrCntctNo);
            hashPara.Add("@SWCost", ObjSWApproval.SWCost);
            hashPara.Add("@SWUse", ObjSWApproval.SWUse);
            hashPara.Add("@BusinessJustification", ObjSWApproval.BusinessJustification);
            hashPara.Add("@InstalledServer", ObjSWApproval.InstalledServer);
            hashPara.Add("@ServerQty", ObjSWApproval.ServerQty);
            hashPara.Add("@PCLapReq", ObjSWApproval.PCLapReq);
            hashPara.Add("@PCLapQty", ObjSWApproval.PCLapQty);
            hashPara.Add("@ExpectedUsers", ObjSWApproval.ExpectedUsers);
            hashPara.Add("@ApproxSize", ObjSWApproval.ApproxSize);
            hashPara.Add("@NoOfPagesPD", ObjSWApproval.NoOfPagesPD);
            hashPara.Add("@Requirements", ObjSWApproval.Requirements);
            hashPara.Add("@SecurityIssues", ObjSWApproval.SecurityIssues);
            hashPara.Add("@ITReqCost", ObjSWApproval.ITReqCost);
            hashPara.Add("@TotalCost", ObjSWApproval.TotalCost);
            hashPara.Add("@ITRemarks", ObjSWApproval.ITRemarks);

            hashPara.Add("@UserId", ObjSWApproval.UserId);
            hashPara.Add("@UserIp", ObjSWApproval.IPAddress);

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

        public SWApproval GetSWApprovalData(string masterHeaderId)
        {
            SWApproval ObjSWAppData = new SWApproval();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_SWApproval_Data_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", masterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjSWAppData.SWApproval_Id = Convert.ToInt32(dt.Rows[0]["SWApproval_Id"].ToString());
                        ObjSWAppData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjSWAppData.UserName = dt.Rows[0]["UserName"].ToString();
                        ObjSWAppData.Dept = dt.Rows[0]["Dept"].ToString();
                        ObjSWAppData.InstallLocation = dt.Rows[0]["InstallLocation"].ToString();
                        ObjSWAppData.OtherLoc = dt.Rows[0]["OtherLoc"].ToString();
                        ObjSWAppData.InstallDept = dt.Rows[0]["InstallDept"].ToString();
                        ObjSWAppData.OtherDept = dt.Rows[0]["OtherDept"].ToString();
                        ObjSWAppData.SWName = dt.Rows[0]["SWName"].ToString();
                        ObjSWAppData.Manufacturer = dt.Rows[0]["Manufacturer"].ToString();
                        ObjSWAppData.MnfrWebsite = dt.Rows[0]["MnfrWebsite"].ToString();
                        ObjSWAppData.MnfrCntctName = dt.Rows[0]["MnfrCntctName"].ToString();
                        ObjSWAppData.MnfrEmail = dt.Rows[0]["MnfrEmail"].ToString();
                        ObjSWAppData.MnfrCntctNo = dt.Rows[0]["MnfrCntctNo"].ToString();
                        ObjSWAppData.SWCost = dt.Rows[0]["SWCost"].ToString();
                        ObjSWAppData.SWUse = dt.Rows[0]["SWUse"].ToString();
                        ObjSWAppData.BusinessJustification = dt.Rows[0]["BusinessJustification"].ToString();
                        ObjSWAppData.InstalledServer = dt.Rows[0]["InstalledServer"].ToString();
                        ObjSWAppData.ServerQty = dt.Rows[0]["ServerQty"].ToString();
                        ObjSWAppData.PCLapReq = dt.Rows[0]["PCLapReq"].ToString();
                        ObjSWAppData.PCLapQty = dt.Rows[0]["PCLapQty"].ToString();
                        ObjSWAppData.ExpectedUsers = dt.Rows[0]["ExpectedUsers"].ToString();
                        ObjSWAppData.ApproxSize = dt.Rows[0]["ApproxSize"].ToString();
                        ObjSWAppData.NoOfPagesPD = dt.Rows[0]["NoOfPagesPD"].ToString();
                        ObjSWAppData.Requirements = dt.Rows[0]["Requirements"].ToString();
                        ObjSWAppData.SecurityIssues = dt.Rows[0]["SecurityIssues"].ToString();
                        ObjSWAppData.ITReqCost = dt.Rows[0]["ITReqCost"].ToString();
                        ObjSWAppData.TotalCost = dt.Rows[0]["TotalCost"].ToString();
                        ObjSWAppData.ITRemarks = dt.Rows[0]["ITRemarks"].ToString();
                    }
                }
                return ObjSWAppData;
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

            string procName = "pr_Search_Master_Request_SWApp";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public int GenerateMassRequestProcess(string masterHeaderId, string approvedByDeptId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GenerateMassRequestProcess_SWApp";
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

        public int ApproveRequest(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "", string miscReq = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_SWApp";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            hashPara.Add("@ApprovalComment", approvalNote);
            hashPara.Add("@MISCReq", miscReq);
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

        public int RollbackRequest(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Reject_Request_SWApp";
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

        public int FinalRejectRequest(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Final_Reject_Request_SWApp";
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

        //public int GenerateCopyRequestM(string masterHeaderId, string moduleId, string PlantGroupID, string userId, string flg, string Reference_Id = "", string Plant = "", string Purchasing_Group = "")
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Utility objUtil = new Utility();
        //    SqlCommand objCommand = new SqlCommand();
        //    string procName = "pr_GenerateCopyRequest_SWApp";
        //    int retVal = 0;
        //    int masterHeaderId2 = 0;
        //    objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
        //    objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
        //    objCommand.Parameters.AddWithValue("@Plant_Group_Id", PlantGroupID);
        //    objCommand.Parameters.AddWithValue("@Plant_Id", Plant);
        //    objCommand.Parameters.AddWithValue("@Reference_Id", Reference_Id);
        //    objCommand.Parameters.AddWithValue("@Purchasing_Group", Purchasing_Group);
        //    objCommand.Parameters.AddWithValue("@UserId", userId);
        //    objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
        //    objCommand.Parameters.AddWithValue("@Flg", flg);
        //    SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
        //    ret.Direction = ParameterDirection.ReturnValue;

        //    SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
        //    outMasterHeaderId.Direction = ParameterDirection.Output;

        //    SqlParameter outRequestNo = objCommand.Parameters.Add("@RequestNo", SqlDbType.VarChar, 50);
        //    outRequestNo.Direction = ParameterDirection.Output;

        //    try
        //    {
        //        objDal.OpenConnection();
        //        objCommand.Connection = objDal.cnnConnection;
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.CommandText = procName;
        //        //Srinidhi
        //        objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

        //        objCommand.ExecuteNonQuery();
        //        retVal = (int)ret.Value;
        //        if (retVal > 0)
        //        {
        //            masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
        //            mRequestNo = SafeTypeHandling.ConvertToString(outRequestNo.Value);
        //        }

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
        //    return masterHeaderId2;
        //}

        public int GenerateCopyRequest(string masterHeaderId, string PlantGroupID, string userLocation, string otherLocation, string moduleId, string otherModule, string subCategory, string otherCategory, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_GenerateCopyRequest_SWApp";
            int retVal = 0;
            int masterHeaderId2 = 0;

            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", PlantGroupID);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@OtherModuleId", otherModule);
            objCommand.Parameters.AddWithValue("@SubCategory", subCategory);
            objCommand.Parameters.AddWithValue("@OtherCategory", otherCategory);
            objCommand.Parameters.AddWithValue("@UserLocation", userLocation);
            objCommand.Parameters.AddWithValue("@OtherLocation", otherLocation);
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



        public MISCData GetMISCData(string masterHeaderId)
        {
            MISCData ObjMISCData = new MISCData();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MISC_Data_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", masterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMISCData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjMISCData.SWApp_Org_Data_Id = Convert.ToInt32(dt.Rows[0]["SWApp_Org_Data_Id"].ToString());
                        ObjMISCData.Panel1MISC = dt.Rows[0]["Panel1MISC"].ToString();
                        ObjMISCData.Panel2MISC = dt.Rows[0]["Panel2MISC"].ToString();
                        ObjMISCData.Panel3MISC = dt.Rows[0]["Panel3MISC"].ToString();

                    }
                }
                return ObjMISCData;
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

        //Created by Swati Mohandas on 25/06/18 to get Department Name on first User Login
        public DataSet GetDepartmentName(string UserID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "select top 1 Department_Name from User_Master where User_Id = '" + UserID + "' AND IsActive = 'TRUE'";
            return objDal.FillDataSet(query, "User_Master");
        }
    }
}