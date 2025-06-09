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
    public class ProfitCenterMasterAccess
    {
        public string mRequestNo { get; set; }
        public int mModule_Id { get; set; }
        public int pPCMaster_Change_Id { get; set; }

        //public int SaveChange(PCenterChange ObjPCenterChange)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_PCMaster_ChangeTB";
        //    int result = 0;


        //    hashPara.Add("@PCMaster_Change_Id", ObjPCenterChange.PCMaster_Change_Id);
        //    hashPara.Add("@Master_Header_Id", ObjPCenterChange.Master_Header_Id);
        //    hashPara.Add("@sProfitCenter", ObjPCenterChange.sProfitCenter);
        //    hashPara.Add("@sProfitCenterName", ObjPCenterChange.sProfitCenterName);

        //    hashPara.Add("@PCMaster_Change_Details_Id", ObjPCenterChange.PCMaster_Change_Details_Id);
        //    hashPara.Add("@Section_Id", ObjPCenterChange.Section_Id);
        //    hashPara.Add("@Section_Field_Master_Id", ObjPCenterChange.Section_Field_Master_Id);
        //    hashPara.Add("@sOld_Value", ObjPCenterChange.sOld_Value);
        //    hashPara.Add("@sNew_Value", ObjPCenterChange.sNew_Value);

        //    hashPara.Add("@CreatedBy", ObjPCenterChange.UserId);
        //    hashPara.Add("@CreatedIP", ObjPCenterChange.IPAddress);

        //    try
        //    {
        //        objDal.OpenConnection();
        //        result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
        //        return result;
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

        public int SaveChange(PCenterChange ObjPCenterChange)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_Upd_T_PCMaster_ChangeTB";
            int retVal = 0;
            //int pPCMaster_Change_Id = 0;
            objCommand.Parameters.AddWithValue("@PCMaster_Change_Id", ObjPCenterChange.PCMaster_Change_Id);
            objCommand.Parameters.AddWithValue("@Master_Header_Id", ObjPCenterChange.Master_Header_Id);
            objCommand.Parameters.AddWithValue("@sProfitCenter", ObjPCenterChange.sProfitCenter);
            objCommand.Parameters.AddWithValue("@sProfitCenterName", ObjPCenterChange.sProfitCenterName);

            objCommand.Parameters.AddWithValue("@PCMaster_Change_Details_Id", ObjPCenterChange.PCMaster_Change_Details_Id);
            objCommand.Parameters.AddWithValue("@Section_Id", ObjPCenterChange.Section_Id);
            objCommand.Parameters.AddWithValue("@Section_Field_Master_Id", ObjPCenterChange.Section_Field_Master_Id);
            objCommand.Parameters.AddWithValue("@sOld_Value", ObjPCenterChange.sOld_Value);
            objCommand.Parameters.AddWithValue("@sNew_Value", ObjPCenterChange.sNew_Value);

            objCommand.Parameters.AddWithValue("@CreatedBy", ObjPCenterChange.UserId);
            objCommand.Parameters.AddWithValue("@CreatedIP", ObjPCenterChange.IPAddress);

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter OutPCMChange_Id = objCommand.Parameters.Add("@OutPCMChange_Id", SqlDbType.Int);
            OutPCMChange_Id.Direction = ParameterDirection.Output;

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
                    pPCMaster_Change_Id = SafeTypeHandling.ConvertStringToInt32(OutPCMChange_Id.Value);

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


        public int SaveChangeOld(PCenterChange ObjPCenterChange)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_PCMaster_ChangeTB";
            int result = 0;


            hashPara.Add("@PCMaster_Change_Id", ObjPCenterChange.PCMaster_Change_Id);
            hashPara.Add("@Master_Header_Id", ObjPCenterChange.Master_Header_Id);
            hashPara.Add("@sProfitCenter", ObjPCenterChange.sProfitCenter);
            hashPara.Add("@sProfitCenterName", ObjPCenterChange.sProfitCenterName);

            hashPara.Add("@PCMaster_Change_Details_Id", ObjPCenterChange.PCMaster_Change_Details_Id);
            hashPara.Add("@Section_Id", ObjPCenterChange.Section_Id);
            hashPara.Add("@Section_Field_Master_Id", ObjPCenterChange.Section_Field_Master_Id);
            hashPara.Add("@sOld_Value", ObjPCenterChange.sOld_Value);
            hashPara.Add("@sNew_Value", ObjPCenterChange.sNew_Value);

            hashPara.Add("@CreatedBy", ObjPCenterChange.UserId);
            hashPara.Add("@CreatedIP", ObjPCenterChange.IPAddress);

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


        public int SaveChangeDetail(PCenterChangeDt ObjPCenterChangeDt)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_PCMaster_Change_DetailsTB";
            int result = 0;


            hashPara.Add("@PCMaster_Change_Details_Id", ObjPCenterChangeDt.PCMaster_Change_Details_Id);
            hashPara.Add("@PCMaster_Change_Id", ObjPCenterChangeDt.PCMaster_Change_Id);
            hashPara.Add("@Section_Id", ObjPCenterChangeDt.Section_Id);
            hashPara.Add("@Section_Field_Master_Id", ObjPCenterChangeDt.Section_Field_Master_Id);
            hashPara.Add("@sOld_Value", ObjPCenterChangeDt.sOld_Value);
            hashPara.Add("@sNew_Value", ObjPCenterChangeDt.sNew_Value);
            hashPara.Add("@CreatedBy", ObjPCenterChangeDt.UserId);
            hashPara.Add("@CreatedIP", ObjPCenterChangeDt.IPAddress);

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

        public PCenterChange GetProfitCenterChange(int PCMaster_Change_Id)
        {
            PCenterChange ObjPCenterChange = new PCenterChange();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_PCMaster_ChangeTB_By_PCMaster_Change_Id";
            DataSet ds;

            hashPara.Add("@PCMaster_Change_Id", PCMaster_Change_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjPCenterChange.PCMaster_Change_Id = Convert.ToInt32(dt.Rows[0]["PCMaster_Change_Id"].ToString());
                        ObjPCenterChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjPCenterChange.sProfitCenter = dt.Rows[0]["sProfitCenter"].ToString();
                        ObjPCenterChange.sProfitCenterName = dt.Rows[0]["sProfitCenterName"].ToString();
                    }
                }
                return ObjPCenterChange;
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

        public PCenterChangeDt GetProfitCenterChangeDetail(int Master_Header_Id, int PCMaster_Change_Details_Id)
        {
            PCenterChangeDt ObjPCenterChangeDt = new PCenterChangeDt();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_PCMaster_Change_DetailsTB_By_PC_Id";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@PCMaster_Change_Details_Id", PCMaster_Change_Details_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjPCenterChangeDt.PCMaster_Change_Details_Id = Convert.ToInt32(dt.Rows[0]["PCMaster_Change_Details_Id"].ToString());
                        ObjPCenterChangeDt.PCMaster_Change_Id = Convert.ToInt32(dt.Rows[0]["PCMaster_Change_Id"].ToString());
                        ObjPCenterChangeDt.Section_Id = Convert.ToInt32(dt.Rows[0]["Section_Id"].ToString());
                        ObjPCenterChangeDt.Section_Field_Master_Id = Convert.ToInt32(dt.Rows[0]["Section_Field_Master_Id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            ObjPCenterChangeDt.Field = dt.Rows[0]["Section_Field_Master_Id"].ToString();
                            ObjPCenterChangeDt.sOld_Value = dt.Rows[0]["sOld_Value"].ToString();
                            ObjPCenterChangeDt.sNew_Value = dt.Rows[0]["sNew_Value"].ToString();
                        }
                        if (dt.Rows.Count > 1)
                        {
                            ObjPCenterChangeDt.Field2 = dt.Rows[1]["Section_Field_Master_Id"].ToString();
                            ObjPCenterChangeDt.sOld_Value2 = dt.Rows[1]["sOld_Value"].ToString();
                            ObjPCenterChangeDt.sNew_Value2 = dt.Rows[1]["sNew_Value"].ToString();
                        }
                        if (dt.Rows.Count > 2)
                        {
                            ObjPCenterChangeDt.Field3 = dt.Rows[2]["Section_Field_Master_Id"].ToString();
                            ObjPCenterChangeDt.sOld_Value3 = dt.Rows[2]["sOld_Value3"].ToString();
                            ObjPCenterChangeDt.sNew_Value3 = dt.Rows[2]["sNew_Value3"].ToString();
                        }
                        if (dt.Rows.Count > 3)
                        {
                            ObjPCenterChangeDt.Field4 = dt.Rows[3]["Section_Field_Master_Id"].ToString();
                            ObjPCenterChangeDt.sOld_Value4 = dt.Rows[3]["sOld_Value4"].ToString();
                            ObjPCenterChangeDt.sNew_Value4 = dt.Rows[3]["sNew_Value4"].ToString();
                        }
                        if (dt.Rows.Count > 4)
                        {
                            ObjPCenterChangeDt.Field5 = dt.Rows[4]["Section_Field_Master_Id"].ToString();
                            ObjPCenterChangeDt.sOld_Value5 = dt.Rows[4]["sOld_Value5"].ToString();
                            ObjPCenterChangeDt.sNew_Value5 = dt.Rows[4]["sNew_Value5"].ToString();
                        }
                    }
                }
                return ObjPCenterChangeDt;
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

        public int DeleteProfitCenterChangeDetail(string CostCenter_Change_Detail_Id)
        {
            CostCenterChangeDetail ObjCostCenterChange = new CostCenterChangeDetail();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_ProfitCenter_Change_Detail_By_PCMaster_Change_Details_Id";
            int result = 0;

            hashPara.Add("@PCMaster_Change_Details_Id", CostCenter_Change_Detail_Id);

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

        public DataSet GetProfitCenterChangeData(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_PCMaster_ChangeTB_By_MasterHeaderId";
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

        public DataSet GetProfitCenterChangeDetailData(int PCMaster_Change_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_PCMaster_Change_DetailsTB_By_PCMaster_Change_Id";
            DataSet ds;

            hashPara.Add("@PCMaster_Change_Id", PCMaster_Change_Id);

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

        public int SaveBlck(ProfitCenterBlock ObjProfitCenterBlock)
        {
            DataSet ds = new DataSet();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_PCMaster_BlockTB";
            int result = 0;


            hashPara.Add("@PCMaster_Block_Id", ObjProfitCenterBlock.PCMaster_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjProfitCenterBlock.Master_Header_Id);
            hashPara.Add("@sProfitCenter", ObjProfitCenterBlock.sProfitCenter);
            hashPara.Add("@sProfitCenterName", ObjProfitCenterBlock.sProfitCenterName);

            hashPara.Add("@bBlockUnBlockStatus", ObjProfitCenterBlock.bBlockUnBlockStatus);

            hashPara.Add("@sRemarks", ObjProfitCenterBlock.sRemarks);
            hashPara.Add("@UserId", ObjProfitCenterBlock.UserId);
            hashPara.Add("@UserIp", ObjProfitCenterBlock.IPAddress);

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

        public ProfitCenterBlock GetProfitCenterBlock(int Master_Header_Id)
        {
            ProfitCenterBlock ObjProfitCenterBlock = new ProfitCenterBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_ProfitCenterBlock_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjProfitCenterBlock.PCMaster_Block_Id = dt.Rows[0]["PCMaster_Block_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["PCMaster_Block_Id"].ToString());
                        ObjProfitCenterBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? Master_Header_Id : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        // ObjProfitCenterBlock.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjProfitCenterBlock.sProfitCenter = dt.Rows[0]["sProfitCenter"].ToString();
                        ObjProfitCenterBlock.sProfitCenterName = dt.Rows[0]["sProfitCenterName"].ToString();
                        ObjProfitCenterBlock.bBlockUnBlockStatus = dt.Rows[0]["bBlockUnBlockStatus"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjProfitCenterBlock.sRemarks = dt.Rows[0]["sRemarks"].ToString();
                    }
                }
                return ObjProfitCenterBlock;
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

        public int Save(ProfitCenterCreate ObjProfitCenterCreate)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_PCMaster_CreateTB";
            int result = 0;


            hashPara.Add("@PCMaster_Create_Id", ObjProfitCenterCreate.PCMaster_Create_Id);
            hashPara.Add("@Master_Header_Id", ObjProfitCenterCreate.Master_Header_Id);

            hashPara.Add("@sProfitCenter", ObjProfitCenterCreate.sProfitCenter);
            hashPara.Add("@sRef_ProfitCenter", ObjProfitCenterCreate.sRef_ProfitCenter);
            hashPara.Add("@sContrlArea", ObjProfitCenterCreate.sContrlArea);
            hashPara.Add("@dAnalysisPeriodF", ObjProfitCenterCreate.dAnalysisPeriodF);
            //hashPara.Add("@dAnalysisPeriodT", ObjProfitCenterCreate.dAnalysisPeriodT);
            hashPara.Add("@sName", ObjProfitCenterCreate.sName);

            hashPara.Add("@sLongText", ObjProfitCenterCreate.sLongText);
            hashPara.Add("@sUserRespons", ObjProfitCenterCreate.sUserRespons);
            hashPara.Add("@sPersonRespons", ObjProfitCenterCreate.sPersonRespons);
            hashPara.Add("@sDepartment", ObjProfitCenterCreate.sDepartment);
            hashPara.Add("@sProfitCtrGrp", ObjProfitCenterCreate.sProfitCtrGrp);
            hashPara.Add("@sSegment", ObjProfitCenterCreate.sSegment);
            hashPara.Add("@sRemarks", ObjProfitCenterCreate.sRemarks);
            hashPara.Add("@CreatedBy", ObjProfitCenterCreate.UserId);
            hashPara.Add("@CreatedIP", ObjProfitCenterCreate.IPAddress);

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


        public ProfitCenterCreate GetProfitCenterMasterData(int MasterHeaderId)
        {
            ProfitCenterCreate ObjProfitCenterCreate = new ProfitCenterCreate();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_ProfitCenterMaster_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjProfitCenterCreate.PCMaster_Create_Id = Convert.ToInt32(dt.Rows[0]["PCMaster_Create_Id"].ToString());
                        ObjProfitCenterCreate.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjProfitCenterCreate.sProfitCenter = dt.Rows[0]["sProfitCenter"].ToString();
                        ObjProfitCenterCreate.sRef_ProfitCenter = dt.Rows[0]["sRef_ProfitCenter"].ToString();
                        ObjProfitCenterCreate.dAnalysisPeriodF = dt.Rows[0]["dAnalysisPeriodF"].ToString();
                        ObjProfitCenterCreate.dAnalysisPeriodT = dt.Rows[0]["dAnalysisPeriodT"].ToString();
                        ObjProfitCenterCreate.sContrlArea = dt.Rows[0]["sContrlArea"].ToString();
                        ObjProfitCenterCreate.sName = dt.Rows[0]["sName"].ToString();
                        ObjProfitCenterCreate.sLongText = dt.Rows[0]["sLongText"].ToString();
                        ObjProfitCenterCreate.sUserRespons = dt.Rows[0]["sUserRespons"].ToString();
                        ObjProfitCenterCreate.sPersonRespons = dt.Rows[0]["sPersonRespons"].ToString();
                        ObjProfitCenterCreate.sDepartment = dt.Rows[0]["sDepartment"].ToString();
                        ObjProfitCenterCreate.sProfitCtrGrp = dt.Rows[0]["sProfitCtrGrp"].ToString();
                        ObjProfitCenterCreate.sSegment = dt.Rows[0]["sSegment"].ToString();
                        ObjProfitCenterCreate.sRemarks = dt.Rows[0]["sRemarks"].ToString();
                        ObjProfitCenterCreate.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                    }
                }
                return ObjProfitCenterCreate;
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
            string procName = "Proc_Get_Profile_Wise_Module_PC";
            hashPara.Add("@ProfileId", profileId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }



        public DataSet SearchMasterRequestsPC(string status, string requestNo, string userId, string moduleId, string moduleType, string sapcodeNo = "", string StartDate = "", string EndDate = "")
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

            string procName = "pr_Search_Master_Request_PC";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

/// Carve_LC17&LC23_8400000406
        public int SavePCHeader(string masterHeaderId, string Company_Id, string moduleId, string userId, string flg, string Plant = "", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header_PC";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant);
            objCommand.Parameters.AddWithValue("@Company_Id", Company_Id);
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

        public int RollbackRequestPC(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Reject_Request_PC";
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

        public int ApproveRequestPC(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_PC";
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
/// Carve_LC17&LC23_8400000406
        //public int GenerateChangeBulkRequest(string masterHeaderId, string moduleCode, string userId, string Plant = "", string companyId = "32", string Master_Category = "", string Reference_Id = "")
        public int GenerateChangeBulkRequest(string masterHeaderId, string moduleCode, string userId, string Plant = "", string companyId = "", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Bulk_Change_RequestPC";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@Module_Code", moduleCode);
            objCommand.Parameters.AddWithValue("@Plant_Id", Plant);
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
/// Carve_LC17&LC23_8400000406
        public int GenerateBlockRequest(string masterHeaderId, string companyId, string moduleId, string ModuleCode, string userId, string SAPMasterCode, string Master_Description, string Plant = "", string Master_Category = "", string Reference_Id = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Generate_Block_RequestPC";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@ModuleCode", ModuleCode);
            objCommand.Parameters.AddWithValue("@SAPMasterCode", SAPMasterCode);
            objCommand.Parameters.AddWithValue("@Plant_Group_Id", 1);
            objCommand.Parameters.AddWithValue("@PlantId", Plant);
            objCommand.Parameters.AddWithValue("@Company_Id", companyId);
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

    }
}