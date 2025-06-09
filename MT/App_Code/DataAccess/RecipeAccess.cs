using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

/// <summary>
/// Summary description for BOMAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class RecipeAccess
    {
        public string mRequestNo { get; set; }
        public int mModule_Id { get; set; }

        //public int Save(Recipe ObjAcc)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_Receipe";
        //    int result = 0;

        //    hashPara.Add("@Recipe_Id", ObjAcc.Recipe_Id);
        //    hashPara.Add("@Master_Header_Id", ObjAcc.Master_Header_Id);
        //    hashPara.Add("@Material", ObjAcc.Material);
        //    hashPara.Add("@Plant_Id", ObjAcc.PlantId);
        //    hashPara.Add("@Prod_Version", ObjAcc.Prod_Version);
        //    hashPara.Add("@Profile", ObjAcc.Profile);
        //    hashPara.Add("@TaskList_Desc", ObjAcc.TaskList_Desc);
        //    hashPara.Add("@Recipe", ObjAcc.Recipef);
        //    hashPara.Add("@Status", ObjAcc.Status);
        //    hashPara.Add("@Usage", ObjAcc.Usage);
        //    hashPara.Add("@Base_Quantity", ObjAcc.Base_Quantity);
        //    hashPara.Add("@Charge_Quantity", ObjAcc.Charge_Quantity);
        //    hashPara.Add("@Operation_Quantity", ObjAcc.Operation_Quantity);
        //    hashPara.Add("@UOM", ObjAcc.UOM);
        //    hashPara.Add("@SuperOrdinate_Operation", ObjAcc.SuperOrdinate_Operation);
        //    hashPara.Add("@Control_Recipe_Destination", ObjAcc.Control_Recipe_Destination);
        //    hashPara.Add("@Resource", ObjAcc.Resource);
        //    hashPara.Add("@Control_Key", ObjAcc.Control_Key);
        //    hashPara.Add("@StandardTextKey", ObjAcc.StandardTextKey);
        //    hashPara.Add("@Description", ObjAcc.Description);
        //    hashPara.Add("@Relevancy_To_Costing", ObjAcc.Relevancy_To_Costing);
        //    hashPara.Add("@First_Std_Value", ObjAcc.First_Std_Value);
        //    hashPara.Add("@First_Std_Value_Unit", ObjAcc.First_Std_Value_Unit);
        //    hashPara.Add("@First_Activity_Type", ObjAcc.First_Activity_Type);
        //    hashPara.Add("@Sec_Std_Value", ObjAcc.Sec_Std_Value);
        //    hashPara.Add("@Sec_Std_Value_Unit", ObjAcc.Sec_Std_Value_Unit);
        //    hashPara.Add("@Sec_Activity_Type", ObjAcc.Sec_Activity_Type);
        //    hashPara.Add("@Third_Std_Value", ObjAcc.Third_Std_Value);
        //    hashPara.Add("@Third_Std_Value_Unit", ObjAcc.Third_Std_Value_Unit);
        //    hashPara.Add("@Third_Activity_Type", ObjAcc.Third_Activity_Type);
        //    hashPara.Add("@Fourth_Std_Value", ObjAcc.Fourth_Std_Value);
        //    hashPara.Add("@Fourth_Std_Value_Unit", ObjAcc.Fourth_Std_Value_Unit);
        //    hashPara.Add("@Fourth_Activity_Type", ObjAcc.Fourth_Activity_Type);
        //    hashPara.Add("@Fifth_Std_Value", ObjAcc.Fifth_Std_Value);
        //    hashPara.Add("@Fifth_Std_Value_Unit", ObjAcc.Fifth_Std_Value_Unit);
        //    hashPara.Add("@Fifth_Activity_Type", ObjAcc.Fifth_Activity_Type);
        //    hashPara.Add("@Sixth_Std_Value", ObjAcc.Sixth_Std_Value);
        //    hashPara.Add("@Sixth_Std_Value_Unit", ObjAcc.Sixth_Std_Value_Unit);
        //    hashPara.Add("@Sixth_Activity_Type", ObjAcc.Sixth_Activity_Type);
        //    hashPara.Add("@Base_Qty", ObjAcc.Base_Qty);
        //    hashPara.Add("@ValidFrom", ObjAcc.ValidFrom);
        //    hashPara.Add("@ValidTo", ObjAcc.ValidTo);

        //    hashPara.Add("@IsActive", ObjAcc.IsActive);
        //    hashPara.Add("@UserId", ObjAcc.UserId);
        //    hashPara.Add("@UserIp", ObjAcc.IPAddress);

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

        public DataSet GetReceipe(int MasterHeaderId)
        {
            Costing1 ObjCost = new Costing1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Receipe_Header_By_MasterHeaderId";
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

        public RecipeHeader GetRecipeHeaderData(string masterHeaderId)
        {
            RecipeHeader ObjRecipeHeaderData = new RecipeHeader();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_RecipeHeader_Data_By_MasterHeaderId";
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
                        ObjRecipeHeaderData.Recipe_HeaderID = Convert.ToInt32(dt.Rows[0]["Recipe_HeaderID"].ToString());
                        ObjRecipeHeaderData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjRecipeHeaderData.Recipe_Group = dt.Rows[0]["Recipe_Group"].ToString();
                        ObjRecipeHeaderData.Recipe = dt.Rows[0]["Recipe"].ToString();
                        ObjRecipeHeaderData.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjRecipeHeaderData.TaskListDesc = dt.Rows[0]["TaskListDesc"].ToString();
                        ObjRecipeHeaderData.MaterialNo = dt.Rows[0]["MaterialNo"].ToString();
                        ObjRecipeHeaderData.MaterialDesc = dt.Rows[0]["MaterialDesc"].ToString();
                    }
                }
                return ObjRecipeHeaderData;
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

        public RecipeDetail GetRecipeHeaderDetail(string recipeHeaderId)
        {
            RecipeDetail ObjRecipeDetail = new RecipeDetail();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_RecipeHeaderDetail_Data_By_RecipeHeaderId";
            DataSet ds;

            hashPara.Add("@Recipe_HeaderId", recipeHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        ObjRecipeDetail.Recipe_HeaderDetail_Id = Convert.ToInt32(dt.Rows[0]["Recipe_HeaderDetail_Id"].ToString());
                        ObjRecipeDetail.Recipe_HeaderID = Convert.ToInt32(dt.Rows[0]["Recipe_HeaderID"].ToString());
                        ObjRecipeDetail.Status = dt.Rows[0]["Status"].ToString();
                        ObjRecipeDetail.Usage = dt.Rows[0]["Usage"].ToString();
                        ObjRecipeDetail.chkStatus = dt.Rows[0]["chkStatus"].ToString();
                        ObjRecipeDetail.Planner_Group = dt.Rows[0]["Planner_Group"].ToString();
                        ObjRecipeDetail.Resource_network = dt.Rows[0]["Resource_network"].ToString();
                        ObjRecipeDetail.Network_Plant = dt.Rows[0]["Network_Plant"].ToString();
                        ObjRecipeDetail.From_LSize = dt.Rows[0]["From_LSize"].ToString();
                        ObjRecipeDetail.To_LSize = dt.Rows[0]["To_LSize"].ToString();
                        ObjRecipeDetail.Base_Quantity = dt.Rows[0]["Base_Quantity"].ToString();
                        ObjRecipeDetail.Unit = dt.Rows[0]["Unit"].ToString();
                        ObjRecipeDetail.Charge_Quantity = dt.Rows[0]["Charge_Quantity"].ToString();
                        ObjRecipeDetail.Operation_Quantity = dt.Rows[0]["Operation_Quantity"].ToString();
                        ObjRecipeDetail.Insp_Points = dt.Rows[0]["Insp_Points"].ToString();
                        ObjRecipeDetail.Partial_Lot = dt.Rows[0]["Partial_Lot"].ToString();
                        //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                        ObjRecipeDetail.Remarks = dt.Rows[0]["Remarks"].ToString();
                        ObjRecipeDetail.Reason = dt.Rows[0]["Reason"].ToString();
                        //-ENded to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                    }
                }
                return ObjRecipeDetail;
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

        public int SaveRecipeHeaderData(RecipeHeader ObjRecipeHeader)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_Upd_T_Recipe_Header_Data";
            int retVal = 0;
            int recipeHeaderID = 0;

            objCommand.Parameters.AddWithValue("@Recipe_HeaderID", ObjRecipeHeader.Recipe_HeaderID);
            objCommand.Parameters.AddWithValue("@Master_Header_Id", ObjRecipeHeader.Master_Header_Id);
            objCommand.Parameters.AddWithValue("@Recipe_Group", ObjRecipeHeader.Recipe_Group);
            objCommand.Parameters.AddWithValue("@Recipe", ObjRecipeHeader.Recipe);
            objCommand.Parameters.AddWithValue("@Plant_Id", ObjRecipeHeader.Plant_Id);
            objCommand.Parameters.AddWithValue("@TaskListDesc", ObjRecipeHeader.TaskListDesc);
            objCommand.Parameters.AddWithValue("@MaterialNo", ObjRecipeHeader.MaterialNo);
            objCommand.Parameters.AddWithValue("@MaterialDesc", ObjRecipeHeader.MaterialDesc);

            objCommand.Parameters.AddWithValue("@UserId", ObjRecipeHeader.UserId);
            objCommand.Parameters.AddWithValue("@IpAddress", ObjRecipeHeader.IPAddress);

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outRecipeHeaderId = objCommand.Parameters.Add("@OutRecipeHeaderId", SqlDbType.Int);
            outRecipeHeaderId.Direction = ParameterDirection.Output;

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
                    recipeHeaderID = SafeTypeHandling.ConvertStringToInt32(outRecipeHeaderId.Value);
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
            return recipeHeaderID;
        }

        public DataSet GetRecipeOperation(string recipeHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Recipe_HeaderID", recipeHeaderId);

            string procName = "pr_Get_RecipeOperations_Data_By_MasterHeaderId";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public int CheckAllRecipeDetail(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Master_HeaderID", masterHeaderId);
            int result = 0;
            string procName = "pr_Get_AllRecipeData_By_MasterHeaderId";
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

        public int SaveOperationDetails(RecipeOperations objRcpOperations)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_RcpOperation_Detail_Data";
            int result = 0;

            hashPara.Add("@Recipe_Operation_Id", objRcpOperations.Recipe_Operation_Id);
            hashPara.Add("@Recipe_HeaderID", objRcpOperations.Recipe_HeaderID);
            hashPara.Add("@Operation_Phase", objRcpOperations.Operation_Phase);
            hashPara.Add("@Phase_Indicator", objRcpOperations.Phase_Indicator);
            hashPara.Add("@Sup_Operation", objRcpOperations.Sup_Operation);
            hashPara.Add("@Destinatn", objRcpOperations.Destinatn);
            hashPara.Add("@Resource", objRcpOperations.Resource);
            hashPara.Add("@Control_key", objRcpOperations.Control_key);
            hashPara.Add("@StdText_Key", objRcpOperations.StdText_Key);
            hashPara.Add("@Description", objRcpOperations.Description);
            hashPara.Add("@Relevancy_To_Costing", objRcpOperations.Relevancy_To_Costing);
            hashPara.Add("@Base_Quantity", objRcpOperations.Base_Quantity);
            hashPara.Add("@Act_Operation_UoM", objRcpOperations.Act_Operation_UoM);
            hashPara.Add("@First_Std_Value", objRcpOperations.First_Std_Value);
            hashPara.Add("@First_Std_Value_Unit", objRcpOperations.First_Std_Value_Unit);
            hashPara.Add("@Sec_Std_Value", objRcpOperations.Sec_Std_Value);
            hashPara.Add("@Sec_Std_Value_Unit", objRcpOperations.Sec_Std_Value_Unit);
            hashPara.Add("@Third_Std_Value", objRcpOperations.Third_Std_Value);
            hashPara.Add("@Third_Std_Value_Unit", objRcpOperations.Third_Std_Value_Unit);
            hashPara.Add("@Plant", objRcpOperations.Plant_Id);
            hashPara.Add("@ChargeQty", objRcpOperations.ChargeQty);
            hashPara.Add("@OperQty", objRcpOperations.OperQty);
            hashPara.Add("@ChargeUnit", objRcpOperations.ChargeUnit);
            hashPara.Add("@OperUnit", objRcpOperations.OperUnit);
            hashPara.Add("@DeletionFlag", objRcpOperations.DeletionFlag);
            hashPara.Add("@UserId", objRcpOperations.UserId);
            hashPara.Add("@UserIp", objRcpOperations.IPAddress);
            //PROV-CCP-MM-941-23-0076 
            hashPara.Add("@Std_Value_4", objRcpOperations.Std_Value_4);
            hashPara.Add("@Std_Value_Unit_4", objRcpOperations.Std_Value_Unit_4);
            hashPara.Add("@Std_Value_5", objRcpOperations.Std_Value_5);
            hashPara.Add("@Std_Value_Unit_5", objRcpOperations.Std_Value_Unit_5);
            hashPara.Add("@Std_Value_6", objRcpOperations.Std_Value_6);
            hashPara.Add("@Std_Value_Unit_6", objRcpOperations.Std_Value_Unit_6);
            hashPara.Add("@AltResource1", objRcpOperations.AltResource1);
            hashPara.Add("@AltResource2", objRcpOperations.AltResource2);
            hashPara.Add("@AltResource3", objRcpOperations.AltResource3);
            hashPara.Add("@AltResource4", objRcpOperations.AltResource4);
            hashPara.Add("@Class_type", objRcpOperations.Class_type);
            hashPara.Add("@WC_Area", objRcpOperations.WC_Area);
            hashPara.Add("@WC_Area_grp", objRcpOperations.WC_Area_grp);
            hashPara.Add("@IsKX_Sche", objRcpOperations.IsKX_Sche);
            //PROV-CCP-MM-941-23-0076 
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

        public DataSet GetRecipeInspChara(string recipeHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Recipe_HeaderID", recipeHeaderId);

            string procName = "pr_Get_RecipeInspChar_Data_By_MasterHeaderId";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet GetRecipeSecResources(string recipeHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Recipe_HeaderID", recipeHeaderId);

            string procName = "pr_Get_RecipeSecRes_Data_By_MasterHeaderId";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public int SaveInspCharDetails(RecipeInspChara objRcpInsChar)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_RcpInspChara_Detail_Data";
            int result = 0;

            hashPara.Add("@Recipe_InspChara_Id", objRcpInsChar.Recipe_InspChara_Id);
            hashPara.Add("@Recipe_HeaderID", objRcpInsChar.Recipe_HeaderID);
            hashPara.Add("@Operation_Phase", objRcpInsChar.Operation_Phase);
            hashPara.Add("@Characteristic_No", objRcpInsChar.Characteristic_No);
            hashPara.Add("@Master_Insp_Char_Code", objRcpInsChar.Master_Insp_Char_Code);
            hashPara.Add("@Sampling_Procedure", objRcpInsChar.Sampling_Procedure);
            hashPara.Add("@CodeGrp", objRcpInsChar.CodeGrp);
            hashPara.Add("@InspPtCmpt", objRcpInsChar.InspPtCmpt);
            hashPara.Add("@NoRelation", objRcpInsChar.NoRelation);
            //Start adding by Nitish Rao 28/06/2018
            hashPara.Add("@Insp_Point", objRcpInsChar.Insp_Point);
            hashPara.Add("@Partial_Lot", objRcpInsChar.Partial_Lot);
            //End adding by Nitish Rao 28/06/2018
            hashPara.Add("@UserId", objRcpInsChar.UserId);
            hashPara.Add("@UserIp", objRcpInsChar.IPAddress);

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

        public int SaveRecipeHeaderDetailsData(RecipeDetail objRcpDetails)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Recipe_HeaderDetail_Data";
            int result = 0;

            hashPara.Add("@Recipe_HeaderDetail_Id", objRcpDetails.Recipe_HeaderDetail_Id);
            hashPara.Add("@Recipe_HeaderID", objRcpDetails.Recipe_HeaderID);
            hashPara.Add("@Status", objRcpDetails.Status);
            hashPara.Add("@Usage", objRcpDetails.Usage);
            hashPara.Add("@chkStatus", objRcpDetails.chkStatus);
            hashPara.Add("@Planner_Group", objRcpDetails.Planner_Group);
            hashPara.Add("@Resource_network", objRcpDetails.Resource_network);
            hashPara.Add("@Network_Plant", objRcpDetails.Network_Plant);
            hashPara.Add("@From_LSize", objRcpDetails.From_LSize);
            hashPara.Add("@To_LSize", objRcpDetails.To_LSize);
            hashPara.Add("@Base_Quantity", objRcpDetails.Base_Quantity);
            hashPara.Add("@Unit", objRcpDetails.Unit);
            hashPara.Add("@Charge_Quantity", objRcpDetails.Charge_Quantity);
            hashPara.Add("@Operation_Quantity", objRcpDetails.Operation_Quantity);
            hashPara.Add("@Insp_Points", objRcpDetails.Insp_Points);
            hashPara.Add("@Partial_Lot", objRcpDetails.Partial_Lot);
            //Started to Add Remark and Reason textbox. Ticket number 8200064571
            hashPara.Add("@Remarks", objRcpDetails.Remarks);
            hashPara.Add("@Reason", objRcpDetails.Reason);
            //End to Add Remark and Reason textbox. Ticket number 8200064571
            hashPara.Add("@UserId", objRcpDetails.UserId);
            hashPara.Add("@IpAddress", objRcpDetails.IPAddress);

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

        //Start Adding by Nitish Rao 14.02.2018
        // To Update Last Operation Number to All secondary resource item
        public int UpdateRecipeSecResouseDetails(RecipeDetail objRcpDetails)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Update_last_Operation_to_AllSecRsc_Item";
            int result = 0;
            hashPara.Add("@Recipe_HeaderID", objRcpDetails.Recipe_HeaderID);

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
        //End Adding by Nitish Rao 14.02.2018

        //Start Adding by Nitish Rao 14.02.2018
        // To Update Last Operation Number to All secondary resource item
        public string getRecipeSecResouseDetails(RecipeDetail objRcpDetails)
        {
            string op = "";
            string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection cnnConnection = new SqlConnection(strConnString);
            using (SqlCommand cmd = new SqlCommand("pr_get_last_Operation_to_AllSecRsc_Item"))
            {

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Recipe_HeaderID", objRcpDetails.Recipe_HeaderID);

                    var p1 = new SqlParameter("@Last_Op", SqlDbType.VarChar, 50);
                    p1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p1);


                    cmd.Connection = cnnConnection;
                    if (cnnConnection.State == ConnectionState.Closed)
                    {
                        cnnConnection.Open();
                    }
                    SqlDataReader sdr = cmd.ExecuteReader();
                    op = cmd.Parameters["@Last_Op"].Value.ToString();

                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }

                }
                catch (Exception ex)
                {
                    cnnConnection.Close();

                }
            }
            return op;
        }
        //End Adding by Nitish Rao 14.02.2018

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

        public int ApproveRequest(string masterHeaderId, string approvedByDeptId, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request";
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

        public int SaveSecResDetails(RecipeSecRes objRcpSecRes)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_RcpSecRes_Detail_Data";
            int result = 0;

            hashPara.Add("@Recipe_SecResource_Id", objRcpSecRes.Recipe_SecResource_Id);
            hashPara.Add("@Recipe_HeaderID", objRcpSecRes.Recipe_HeaderID);
            hashPara.Add("@Operation_Phase", objRcpSecRes.Operation_Phase);
            hashPara.Add("@SecResource_Item", objRcpSecRes.SecResource_Item);
            hashPara.Add("@SecResource", objRcpSecRes.SecResource);
            hashPara.Add("@Duration", objRcpSecRes.Duration);
            hashPara.Add("@Unit1", objRcpSecRes.Unit1);
            hashPara.Add("@ActivityType1", objRcpSecRes.ActivityType1);
            hashPara.Add("@Process", objRcpSecRes.Process);
            hashPara.Add("@Unit2", objRcpSecRes.Unit2);
            hashPara.Add("@ActivityType2", objRcpSecRes.ActivityType2);
            hashPara.Add("@Labor", objRcpSecRes.Labor);
            hashPara.Add("@Unit3", objRcpSecRes.Unit3);
            hashPara.Add("@ActivityType3", objRcpSecRes.ActivityType3);

            hashPara.Add("@UserId", objRcpSecRes.UserId);
            hashPara.Add("@UserIp", objRcpSecRes.IPAddress);

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


        public int SaveBOMDetailsT2T(int Bom_HeaderID, int MasterHeaderId, string LoggedIn_User_Id)
        {
            Utility objUtil = new Utility();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_BOM_Detail_Temp_DataT2T";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@Bom_HeaderID", Bom_HeaderID);
            hashPara.Add("@UserId", LoggedIn_User_Id);
            hashPara.Add("@UserIp", objUtil.GetIpAddress());

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



        public int SaveRecOperationDetailsT2T(int Recipe_HeaderID, int MasterHeaderId, string LoggedIn_User_Id)
        {
            Utility objUtil = new Utility();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Recipe_Operation_Temp_DataT2T";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@Recipe_HeaderID", Recipe_HeaderID);
            hashPara.Add("@UserId", LoggedIn_User_Id);
            hashPara.Add("@UserIp", objUtil.GetIpAddress());

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


        public int SaveSecResDetailsT2T(int Recipe_HeaderID, int MasterHeaderId, string LoggedIn_User_Id)
        {
            Utility objUtil = new Utility();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_RcpSecRes_Detail_DataT2T";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@Recipe_HeaderID", Recipe_HeaderID);
            hashPara.Add("@UserId", LoggedIn_User_Id);
            hashPara.Add("@UserIp", objUtil.GetIpAddress());

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

        public int SaveMICDetailsT2T(int Recipe_HeaderID, int MasterHeaderId, string LoggedIn_User_Id)
        {
            Utility objUtil = new Utility();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_T_RcpInspChara_Detail_Data_TempT2T";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@Recipe_HeaderID", Recipe_HeaderID);
            hashPara.Add("@UserId", LoggedIn_User_Id);
            hashPara.Add("@UserIp", objUtil.GetIpAddress());

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


        public RecipeSecRes GetDataByResourceName(string resource, string plant_id)
        {
            RecipeSecRes ObjRcpSecResData = new RecipeSecRes();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Data_By_ResourceName";
            DataSet ds;

            hashPara.Add("@Resource", resource);
            hashPara.Add("@Filter_Field", plant_id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        ObjRcpSecResData.ActivityType1 = dt.Rows[0]["ActivityType"].ToString();
                        ObjRcpSecResData.Unit1 = dt.Rows[0]["ActivityUnit"].ToString();
                        ObjRcpSecResData.StdValKey = dt.Rows[0]["StdValKey"].ToString();
                    }
                }
                return ObjRcpSecResData;
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

        public string GetOprRescStdUnits(string resourceName, string plant_id)
        {
            string stdValKey = "";

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_OprStdUnit_Data_By_ResourceName";
            DataSet ds;

            hashPara.Add("@Resource", resourceName);
            hashPara.Add("@Filter_Field", plant_id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        stdValKey = dt.Rows[0]["StdValKey"].ToString();
                    }
                }
                return stdValKey;
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

        public int SaveProdVersionData(ProdVersion ObjProdVersion)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_ProdVer_Header_Data";
            int result = 0;

            hashPara.Add("@ProdVersion_Id", ObjProdVersion.ProdVersion_Id);
            hashPara.Add("@Master_Header_Id", ObjProdVersion.Master_Header_Id);
            hashPara.Add("@MaterialNo", ObjProdVersion.MaterialNo);
            hashPara.Add("@MaterialDesc", ObjProdVersion.MaterialDesc);
            hashPara.Add("@ProdVersionNo", ObjProdVersion.ProdVersionNo);
            hashPara.Add("@ProdVersion_Text", ObjProdVersion.ProdVersion_Text);
            hashPara.Add("@Lock", ObjProdVersion.Lock);
            hashPara.Add("@ProdFrom", ObjProdVersion.ProdFrom);
            hashPara.Add("@ProdTo", ObjProdVersion.ProdTo);
            hashPara.Add("@ProdUnit", ObjProdVersion.ProdUnit);
            hashPara.Add("@ValidFrom", ObjProdVersion.ValidFrom);
            hashPara.Add("@ValidTo", ObjProdVersion.ValidTo);
            hashPara.Add("@TaskListType", ObjProdVersion.TaskListType);
            hashPara.Add("@RecipeGroup", ObjProdVersion.RecipeGroup);
            hashPara.Add("@GroupCntr", ObjProdVersion.GroupCntr);
            hashPara.Add("@AltBOM", ObjProdVersion.AltBOM);
            hashPara.Add("@BOMUsage", ObjProdVersion.BOMUsage);

            hashPara.Add("@UserId", ObjProdVersion.UserId);
            hashPara.Add("@UserIp", ObjProdVersion.IPAddress);
            hashPara.Add("@RStatus", ObjProdVersion.RStatus);
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

        public ProdVersion GetProdVersionData(string masterHeaderId)
        {
            ProdVersion ObjProdVerData = new ProdVersion();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_ProdVersion_Header_Data_By_MasterHeaderId";
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
                        ObjProdVerData.ProdVersion_Id = Convert.ToInt32(dt.Rows[0]["ProdVersion_Id"].ToString());
                        ObjProdVerData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjProdVerData.MaterialNo = dt.Rows[0]["MaterialNo"].ToString();
                        ObjProdVerData.MaterialDesc = dt.Rows[0]["MaterialDesc"].ToString();
                        ObjProdVerData.ProdVersionNo = dt.Rows[0]["ProdVersionNo"].ToString();
                        ObjProdVerData.ProdVersion_Text = dt.Rows[0]["ProdVersion_Text"].ToString();
                        ObjProdVerData.Lock = dt.Rows[0]["Lock"].ToString();
                        ObjProdVerData.ProdFrom = dt.Rows[0]["ProdFrom"].ToString();
                        ObjProdVerData.ProdTo = dt.Rows[0]["ProdTo"].ToString();
                        ObjProdVerData.ProdUnit = dt.Rows[0]["ProdUnit"].ToString();
                        ObjProdVerData.ValidFrom = dt.Rows[0]["ValidFrom"].ToString();
                        ObjProdVerData.ValidTo = dt.Rows[0]["ValidTo"].ToString();
                        ObjProdVerData.TaskListType = dt.Rows[0]["TaskListType"].ToString();
                        ObjProdVerData.RecipeGroup = dt.Rows[0]["RecipeGroup"].ToString();
                        ObjProdVerData.GroupCntr = dt.Rows[0]["GroupCntr"].ToString();
                        ObjProdVerData.AltBOM = dt.Rows[0]["AltBOM"].ToString();
                        ObjProdVerData.BOMUsage = dt.Rows[0]["BOMUsage"].ToString();
                        ObjProdVerData.RStatus = dt.Rows[0]["RStatus"].ToString();
                    }
                }
                return ObjProdVerData;
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

        public int GetInspCharDetail(string inspCharID, string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_InspChar_Data_Exists";
            int result = 0;

            hashPara.Add("@Recipe_InspChara_Id", inspCharID);
            hashPara.Add("@Recipe_HeaderID", recipeID);

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

        public int DeleteInspCharData(string inspCharID, string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_InspCharData_By_RecipeId";
            int result = 0;

            hashPara.Add("@Recipe_InspChara_Id", inspCharID);
            hashPara.Add("@Recipe_HeaderID", recipeID);

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

        public int GetSecResDetail(string secResID, string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_SecResource_Data_Exists";
            int result = 0;

            hashPara.Add("@Recipe_SecResource_Id", secResID);
            hashPara.Add("@Recipe_HeaderID", recipeID);

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

        public int DeleteSecResData(string secResID, string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_SecResData_By_RecipeId";
            int result = 0;

            hashPara.Add("@Recipe_SecResource_Id", secResID);
            hashPara.Add("@Recipe_HeaderID", recipeID);

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
        //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
        public int GetRecOperationData(string RecOperID, string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_RecipeOperationData_Exist";
            int result = 0;

            hashPara.Add("@Recipe_Operation_Id", RecOperID);
            hashPara.Add("@Recipe_HeaderID", recipeID);

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

        public int DeleteOperData(string RecOprID, string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_RecOperation_By_RecipeId";
            int result = 0;

            hashPara.Add("@Recipe_Operation_Id", RecOprID);
            hashPara.Add("@Recipe_HeaderID", recipeID);

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
        //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
        public string GetRecipeDescription(string recipeGrp, string prodVer, string plant_code)
        {
            string recipeDesc = "";

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_RecipeDesc_By_RecipeGrp";
            DataSet ds;

            hashPara.Add("@RecipeGrp", recipeGrp);
            hashPara.Add("@Plant_code", plant_code);
            hashPara.Add("@ProdVer", prodVer);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        recipeDesc = dt.Rows[0]["RecipeDesc"].ToString();
                    }
                }
                return recipeDesc;
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

        public ProdVersion GetRecipeData(string recipeGrp, string materialNo, string plant_code)
        {
            ProdVersion ObjProdVerData = new ProdVersion();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_RecipeDesc_By_RecipeGrp";
            DataSet ds;

            hashPara.Add("@RecipeGrp", recipeGrp);
            hashPara.Add("@Plant_code", plant_code);
            hashPara.Add("@MaterialNo", materialNo);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        ObjProdVerData.MaterialNo = dt.Rows[0]["MaterialNo"].ToString();
                        //ObjProdVerData.ProdVersionNo = dt.Rows[0]["ProdVer"].ToString();
                        //ObjProdVerData.ProdVersion_Text = dt.Rows[0]["RecipeDesc"].ToString();
                        //ObjProdVerData.ProdFrom = dt.Rows[0]["FromLSize"].ToString();
                        //ObjProdVerData.ProdTo = dt.Rows[0]["ToLSize"].ToString();
                        ObjProdVerData.RecipeGroup = dt.Rows[0]["RecipeGrp"].ToString();
                        ObjProdVerData.GroupCntr = dt.Rows[0]["GrpCntr"].ToString();
                        //ObjProdVerData.Lock = dt.Rows[0]["LockStatus"].ToString();  
                    }
                }
                return ObjProdVerData;
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

        public int GetMaxProdVerCreatedForMat(string materialNo, string plant_Code)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaxProdVer_ByMatNo";
            DataSet ds;
            int maxProdVer = 0;

            hashPara.Add("@MaterialNo", "%" + materialNo + "%");
            hashPara.Add("@Plant_code", plant_Code);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        maxProdVer = Convert.ToInt32(ds.Tables[0].Rows[0]["ProdVer"].ToString());
                    }
                }
                return maxProdVer;
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

        public string GetCodeGrpByMICNo(string micNo, string plant_Code)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CodeGrp_ByMICNo";
            DataSet ds;
            string codeGrp = "";

            hashPara.Add("@MIC", micNo);
            hashPara.Add("@Plant_code", plant_Code);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        codeGrp = ds.Tables[0].Rows[0]["CodeGrp"].ToString();
                    }
                }
                return codeGrp;
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

        public int GenerateChangeBulkRequest(string masterHeaderId, string moduleCode, string userId, string flg, string Plant_Group_Id = "", string Plant = "", string Reference_Id = "", string Purchasing_Group = "")
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

        public int UpdateRecipeStatus(string recipe_header_id, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            Utility util = new Utility();
            string procName = "pr_UpdateRecipeStatus";
            int result = 0;

            hashPara.Add("@Recipe_Header_Id", recipe_header_id);

            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", util.GetIpAddress());

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

        public int UpdateProdLockStatus(string prodVersionId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            Utility util = new Utility();
            string procName = "pr_UpdateProdVer_LockStatus";
            int result = 0;

            hashPara.Add("@ProdVersion_Id", prodVersionId);

            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", util.GetIpAddress());

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

        public int UpdateMassProdLockStatus(string MassProcessId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            Utility util = new Utility();
            string procName = "pr_UpdateMass_ProdLockStatus";
            int result = 0;

            hashPara.Add("@Mass_Process_Id", MassProcessId);

            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", util.GetIpAddress());

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

        public int UpdateProdLockStatusByMasthdrId(string master_header_id, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            Utility util = new Utility();
            string procName = "pr_Update_ProdLockStatus_ByMstrHdrId";
            int result = 0;

            hashPara.Add("@Master_Header_Id", master_header_id);

            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", util.GetIpAddress());

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

        public DataSet GetRsrcDataByResourceName(string resource, string plant_id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Resource", resource);
            hashPara.Add("@Filter_Field", plant_id);

            string procName = "pr_Get_RsrcData_By_ResourceName";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        #region ITSM413605


        public DataSet GetYSNOStatusTB(string MasterHeaderId, string sActivity)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Master_HeaderID", MasterHeaderId);
            hashPara.Add("@sActivity", sActivity);
            string procName = "pr_GetYSNOStatusTB";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet GetExlUpdStatusTB(string MasterHeaderId, int Section_ID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Master_HeaderID", MasterHeaderId);
            hashPara.Add("@Section_ID", Section_ID);
            string procName = "pr_Get_T_ExlUpdStatusTB";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }
        public DataSet GetRecipeSecResourcesVal(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Master_Header_Id", MasterHeaderId);
            string procName = "pr_Get_ValRecipeSecRes_Data_By_MasterHeaderId";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public int SaveUpdateFlag(UpdateFlagSecRes objUpdateFlagSecRes)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_ExlUpdStatusTB";
            int result = 0;

            hashPara.Add("@Master_HeaderID", objUpdateFlagSecRes.Master_HeaderID);
            hashPara.Add("@Recipe_HeaderID", objUpdateFlagSecRes.Recipe_HeaderID);
            hashPara.Add("@sScreenFlag", objUpdateFlagSecRes.sScreenFlag);
            hashPara.Add("@Section_ID", objUpdateFlagSecRes.Section_ID);
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


        public int DeleteAllSecResData(string recipeID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_AllSecResData_By_RecipeId";
            int result = 0;
             
            hashPara.Add("@Recipe_HeaderID", recipeID);

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

        #endregion
    }
}