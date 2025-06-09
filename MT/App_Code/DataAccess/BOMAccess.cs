using System;
using System.Collections;
using System.Collections.Generic;
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
    public class BOMAccess
    {
        public string mRequestNo { get; set; }

        //public int Save(BOMHeader ObjAcc)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_BOM_Header";
        //    int result = 0;

        //    hashPara.Add("@BOM_HeaderID", ObjAcc.BOM_HeaderID);
        //    hashPara.Add("@Master_Header_Id", ObjAcc.Master_Header_Id);

        //    hashPara.Add("@Material_Id", ObjAcc.Material_Id);
        //    hashPara.Add("@Plant_Id", ObjAcc.PlantId);
        //    hashPara.Add("@BOM_Usage", ObjAcc.BOM_Usage);
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

        //public int Save(BOMDetail ObjAcc)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_BOM_Detail";
        //    int result = 0;

        //    hashPara.Add("@BOM_HeaderID", ObjAcc.BOM_HeaderID);
        //    hashPara.Add("@Component", ObjAcc.Component);

        //    hashPara.Add("@Quantity", ObjAcc.Quantity);
        //    hashPara.Add("@Component_UOM", ObjAcc.Component_UOM);
        //    hashPara.Add("@ASM", ObjAcc.ASM);
        //    hashPara.Add("@Base_Quantity", ObjAcc.Base_Quantity);
        //    hashPara.Add("@Base_Quantity_UOM", ObjAcc.Base_Quantity_UOM);
        //    hashPara.Add("@BOM_Status", ObjAcc.BOM_Status);
        //    hashPara.Add("@Comp_Scrap_Per", ObjAcc.Comp_Scrap_Per);
        //    hashPara.Add("@Item_Category", ObjAcc.Item_Category);
        //    hashPara.Add("@Costing_Relevncy", ObjAcc.Costing_Relevncy);

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

        public int Delete(int BOM_DetailId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_delete_BOM_Detail";
            int result = 0;

            hashPara.Add("@BOM_HeaderDetailId", BOM_DetailId);

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

        public DataSet GetBOMDetail(int BOM_HeaderId)
        {
            Costing1 ObjCost = new Costing1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOM_Detail";
            DataSet ds;

            hashPara.Add("@BOM_HeaderId", BOM_HeaderId);

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

        public DataSet GetBOMPRODSTATUSDetail( int master_header_id)
        {
            Costing1 ObjCost = new Costing1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOM_PRODVER_Status";
            DataSet ds;

            
            hashPara.Add("@MasterHeaderId", master_header_id);

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

        public DataSet GetBOMHeader(int MasterHeaderId)
        {
            Costing1 ObjCost = new Costing1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOM_Header";
            DataSet ds;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);

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

        public BOMHeader GetBOMHeaderData(string masterHeaderId)
        {
            BOMHeader ObjBOMHeaderData = new BOMHeader();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOMHeader_Data_By_MasterHeaderId";
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
                        ObjBOMHeaderData.BOM_HeaderID = Convert.ToInt32(dt.Rows[0]["BOM_HeaderID"].ToString());
                        ObjBOMHeaderData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBOMHeaderData.Material_Number = dt.Rows[0]["Material_Number"].ToString();
                        ObjBOMHeaderData.Material_Desc = dt.Rows[0]["Material_Desc"].ToString();
                        ObjBOMHeaderData.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjBOMHeaderData.BOM_Usage = dt.Rows[0]["BOM_Usage"].ToString();
                        ObjBOMHeaderData.AlternativeBOM = dt.Rows[0]["AlternativeBOM"].ToString();
                        ObjBOMHeaderData.BOMText = dt.Rows[0]["BOMText"].ToString();
                        ObjBOMHeaderData.AlternativeText = dt.Rows[0]["AlternativeText"].ToString();
                        ObjBOMHeaderData.BaseQty = dt.Rows[0]["BaseQty"].ToString();
                        ObjBOMHeaderData.BaseUOM = dt.Rows[0]["BaseUOM"].ToString();
                        ObjBOMHeaderData.BOMStatus = dt.Rows[0]["BOMStatus"].ToString();
                        ObjBOMHeaderData.From = dt.Rows[0]["From_LSize"].ToString();
                        ObjBOMHeaderData.To = dt.Rows[0]["To_LSize"].ToString();
                        //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                        ObjBOMHeaderData.Remarks = dt.Rows[0]["Remarks"].ToString();
                        ObjBOMHeaderData.Reason = dt.Rows[0]["Reason"].ToString();
                        //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                    }
                }
                return ObjBOMHeaderData;
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

        public DataSet GetBOMDetail(string bomHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@BOM_HeaderID", bomHeaderId);

            string procName = "pr_Get_BOMDetail_Data_By_MasterHeaderId";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public int SaveBOMHeaderData(BOMHeader ObjBOMHeader)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_Upd_T_BOM_Header_Data";
            int retVal = 0;
            int bomHeaderID = 0;

            objCommand.Parameters.AddWithValue("@BOM_HeaderID", ObjBOMHeader.BOM_HeaderID);
            objCommand.Parameters.AddWithValue("@Master_Header_Id", ObjBOMHeader.Master_Header_Id);
            objCommand.Parameters.AddWithValue("@Material_Number", ObjBOMHeader.Material_Number);
            objCommand.Parameters.AddWithValue("@Material_Desc", ObjBOMHeader.Material_Desc);
            objCommand.Parameters.AddWithValue("@Plant_Id", ObjBOMHeader.Plant_Id);
            objCommand.Parameters.AddWithValue("@BOM_Usage", ObjBOMHeader.BOM_Usage);
            objCommand.Parameters.AddWithValue("@AlternativeBOM", ObjBOMHeader.AlternativeBOM);
            objCommand.Parameters.AddWithValue("@BOMText", ObjBOMHeader.BOMText);
            objCommand.Parameters.AddWithValue("@AlternativeText", ObjBOMHeader.AlternativeText);
            objCommand.Parameters.AddWithValue("@BaseQty", ObjBOMHeader.BaseQty);
            objCommand.Parameters.AddWithValue("@BaseUOM", ObjBOMHeader.BaseUOM);
            objCommand.Parameters.AddWithValue("@BOMStatus", ObjBOMHeader.BOMStatus);
            objCommand.Parameters.AddWithValue("@From_LSize", ObjBOMHeader.From);
            objCommand.Parameters.AddWithValue("@To_LSize", ObjBOMHeader.To);
            objCommand.Parameters.AddWithValue("@UserId", ObjBOMHeader.UserId);
            objCommand.Parameters.AddWithValue("@IpAddress", ObjBOMHeader.IPAddress);
            //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
            objCommand.Parameters.AddWithValue("@Remarks", ObjBOMHeader.Remarks);
            objCommand.Parameters.AddWithValue("@Reason", ObjBOMHeader.Reason);
            //-ENded to Add Remark and Reason textbox. Ticket number 8200064571-- %>
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outBOMHeaderId = objCommand.Parameters.Add("@OutBOMHeaderId", SqlDbType.Int);
            outBOMHeaderId.Direction = ParameterDirection.Output;

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
                    bomHeaderID = SafeTypeHandling.ConvertStringToInt32(outBOMHeaderId.Value);
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
            return bomHeaderID;
        }

        public int SaveBOMDetails(BOMDetail objBOMDetail)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_BOM_Detail_Data";
            int result = 0;

            hashPara.Add("@BOM_HeaderDetail_Id", objBOMDetail.BOM_HeaderDetail_Id);
            hashPara.Add("@BOM_HeaderID", objBOMDetail.BOM_HeaderID);
            hashPara.Add("@Postion_No", objBOMDetail.Postion_No);
            hashPara.Add("@Item_Category", objBOMDetail.Item_Category);
            hashPara.Add("@Component", objBOMDetail.Component);
            hashPara.Add("@Component_desc", objBOMDetail.Component_desc);
            hashPara.Add("@Quantity", objBOMDetail.Quantity);
            hashPara.Add("@Component_UOM", objBOMDetail.Component_UOM);
            hashPara.Add("@Comp_SortString", objBOMDetail.Comp_SortString);
            hashPara.Add("@Qty_Is_Fixed1", objBOMDetail.Qty_Is_Fixed1);
            hashPara.Add("@Spare_Part_Indicator", objBOMDetail.Spare_Part_Indicator);
            hashPara.Add("@StorageLocation", objBOMDetail.StorageLocation);
            hashPara.Add("@Alt_Item_Group", objBOMDetail.Alt_Item_Group);
            hashPara.Add("@Priority", objBOMDetail.Priority);
            hashPara.Add("@starategy", objBOMDetail.starategy);
            hashPara.Add("@Usage_Probebilty", objBOMDetail.Usage_Probebilty);
            hashPara.Add("@Relevancy_To_Costing", objBOMDetail.Relevancy_To_Costing);
            hashPara.Add("@Remarks", objBOMDetail.Remarks);
            hashPara.Add("@ASM", objBOMDetail.ASM);
            hashPara.Add("@PhantomIndicator", objBOMDetail.Phantom_Indicator);
            hashPara.Add("@Component_Scrap", objBOMDetail.Component_Scrap);
            hashPara.Add("@RecursiveBOM", objBOMDetail.RecursiveBOM);
            hashPara.Add("@Valid_From", objBOMDetail.Valid_From);
            hashPara.Add("@Valid_to", objBOMDetail.Valid_to);
            hashPara.Add("@BOM_Item_Text1", objBOMDetail.BOM_Item_Text1);
            hashPara.Add("@BOM_Item_Text2", objBOMDetail.BOM_Item_Text2);
            hashPara.Add("@ActiveFiller", objBOMDetail.ActiveFiller);
            hashPara.Add("@Combination", objBOMDetail.Combination);
            hashPara.Add("@Upd_Flag", objBOMDetail.Upd_Flag);
            hashPara.Add("@Item_node_number", objBOMDetail.ItemNode);

            hashPara.Add("@UserId", objBOMDetail.UserId);
            hashPara.Add("@UserIp", objBOMDetail.IPAddress);

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

        public int DeleteBOMDetailData(int bomDetailId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_BOMDetailData_By_BOMDetailId";
            int result = 0;

            hashPara.Add("@BOM_HeaderDetail_Id", bomDetailId);

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

        public static DataSet ReadMaterialHelp(string materialNo, string materialDesc, string plant, string BOMUsage, string altBOM)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MaterialNo", materialNo.Replace("*", "") + "%");
            hashPara.Add("@MaterialDesc", materialDesc.Replace("*", "") + "%");
            hashPara.Add("@Plant", plant.Replace("*", "%"));
            hashPara.Add("@BOMusage", BOMUsage.Replace("*", "%"));
            hashPara.Add("@AltBOM", altBOM.Replace("*", "%"));

            return objDal.FillDataSet(CommandType.StoredProcedure, "Proc_Search_Material", hashPara);
        }

        public string GetComponentDesc(string materialNo, string materialDesc, string plant, string BOMUsage, string altBOM)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Proc_Search_Material";
            DataSet ds;
            string componentDesc = "";

            hashPara.Add("@MaterialNo", materialNo.Replace("*", "") + "%");
            hashPara.Add("@MaterialDesc", materialDesc.Replace("*", "") + "%");
            hashPara.Add("@Plant", plant.Replace("*", "%"));
            hashPara.Add("@BOMusage", BOMUsage.Replace("*", "%"));
            hashPara.Add("@AltBOM", altBOM.Replace("*", "%"));

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        componentDesc = ds.Tables[0].Rows[0]["Material_Desc"].ToString();

                    }
                }
                return componentDesc;
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

        public DataSet AutoCompleteMaterialUOM(string UOM, string materialNo, string plantCode = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            UOM = UOM.Replace("`", "");
            hashPara.Add("@UOM", UOM);
            hashPara.Add("@MaterialNo", materialNo);
            hashPara.Add("@plant", plantCode);
            try
            {
                return objDal.FillDataSet(CommandType.StoredProcedure, "proc_AutoComplete_Material_UOM", hashPara);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUOM(string UOM)
        {
            DataAccessLayer objDal = new DataAccessLayer();

            try
            {
                objDal.OpenConnection();
                UOM = UOM.Replace("`", "");
                return objDal.ReturnBool("SELECT LookUp_Code from M_LookUp_Material where Control_Name LIKE 'ddlBaseUnit' and Section_Id = 3 and Is_Hidden = 0 and LookUp_Code ='" + UOM + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }

        }

        public DataSet AutoCompleteUOM(string UOM)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            UOM = UOM.Replace("`", "");
            return objDal.FillDataSet("SELECT TOP 10 UOM,Technical_Text ,Common_Text FROM M_UOM WHERE UOM LIKE '" + UOM + "%' AND IsActive = 'TRUE' ORDER BY UOM");
        }

        public DataSet AutoCompleteUOM(string UOM, string materialNo)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            UOM = UOM.Replace("`", "");
            hashPara.Add("@UOM", UOM);
            hashPara.Add("@MaterialNo", materialNo);
            //hashPara.Add("@plant", plant);
            try
            {
                return objDal.FillDataSet(CommandType.StoredProcedure, "Proc_Auto_Material_UOM", hashPara);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetBOMDetail(string bomDetailID, string bomHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_BOMDetail_Data_Exists";
            int result = 0;

            hashPara.Add("@BOM_HeaderDetail_Id", bomDetailID);
            hashPara.Add("@BOM_HeaderID", bomHeaderId);
            
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

        public int GetMaxAltBOMCreatedForMat(string materialNo, string plant_Code, string bomUsage)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaxAltBOM_ByMatNo";
            DataSet ds;
            int maxAltBOM = 0;

            hashPara.Add("@MaterialNo", "%"+ materialNo + "%");
            hashPara.Add("@Plant_code", plant_Code);
            hashPara.Add("@BOMusage", bomUsage);
            
            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        maxAltBOM = Convert.ToInt32(ds.Tables[0].Rows[0]["AltBOM"].ToString());
                    }
                }
                return maxAltBOM;
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

        public BOMRecipeChange GetRemarks(string master_header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOMUpload_Data_By_MasterHeaderId";
            DataSet ds;
            BOMRecipeChange objBRChange = new BOMRecipeChange();
            
            hashPara.Add("@Master_Header_Id", master_header_Id);
           
            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        objBRChange.BRChangeId = Convert.ToInt32(ds.Tables[0].Rows[0]["BOM_UploadID"].ToString());
                        objBRChange.Master_Header_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Master_Header_Id"].ToString());
                        objBRChange.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();

                    }
                }
                return objBRChange;
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

        public int SaveRemarks(BOMRecipeChange objBRChange)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_BOM_UploadData";
            int result = 0;

            hashPara.Add("@BOM_UploadID", objBRChange.BRChangeId);
            hashPara.Add("@Master_Header_Id", objBRChange.Master_Header_Id);
            hashPara.Add("@Remarks", objBRChange.Remarks);

            hashPara.Add("@UserId", objBRChange.UserId);
            hashPara.Add("@UserIp", objBRChange.IPAddress);

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

        public DataSet GetBOMRecipeListByMassProcessID(string MassRequestProcessId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MassRequestProcessId", MassRequestProcessId);
            hashPara.Add("@UserId", userId);

            string procName = "pr_GetBOMRecipeListByMassMaterialProcessID";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public bool IsSAPintegrationPending(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            bool retVal = false;
            string procName = "Pr_IsBRPSAPintegrationPending";

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

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

        public DataSet GetBOMRecipeBlockData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOMRecipeBlock_By_MasterHeaderId";
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

        public BOMRecipeBlock GetBOMRecipeBlock(int BOMRecipe_Block_Id)
        {
            BOMRecipeBlock ObjBOMRecipeBlock = new BOMRecipeBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_BOMRecipeBlock_By_BOMRecipeBlockId";
            DataSet ds;

            hashPara.Add("@BOMRecipe_Block_Id", BOMRecipe_Block_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjBOMRecipeBlock.BOMRecipe_Block_Id = dt.Rows[0]["BOMRecipe_Block_Id"].ToString() == "" ? BOMRecipe_Block_Id : Convert.ToInt32(dt.Rows[0]["BOMRecipe_Block_Id"].ToString());
                        ObjBOMRecipeBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjBOMRecipeBlock.Material_Number = dt.Rows[0]["Material_Number"].ToString();                        
                        ObjBOMRecipeBlock.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjBOMRecipeBlock.Recipe_Group = dt.Rows[0]["Recipe_Group"].ToString();
                        ObjBOMRecipeBlock.Status = dt.Rows[0]["Status"].ToString();
                        ObjBOMRecipeBlock.AlternativeBOM = dt.Rows[0]["AlternativeBOM"].ToString();
                        ObjBOMRecipeBlock.BOMStatus = dt.Rows[0]["BOMStatus"].ToString();
                        ObjBOMRecipeBlock.ProdVersionNo = dt.Rows[0]["ProdVersionNo"].ToString();
                        ObjBOMRecipeBlock.Lock = dt.Rows[0]["Lock"].ToString();
                        ObjBOMRecipeBlock.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjBOMRecipeBlock;
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

        public int Save(BOMRecipeBlock ObjBOMRecipeBlock)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_BOMRecipe_Block";
            int result = 0;

            hashPara.Add("@BOMRecipe_Block_Id", ObjBOMRecipeBlock.BOMRecipe_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjBOMRecipeBlock.Master_Header_Id);
            hashPara.Add("@Material_Number", ObjBOMRecipeBlock.Material_Number);            
            hashPara.Add("@Plant_Id", ObjBOMRecipeBlock.Plant_Id);
            hashPara.Add("@Recipe_Group", ObjBOMRecipeBlock.Recipe_Group);
            hashPara.Add("@Status", ObjBOMRecipeBlock.Status);
            hashPara.Add("@AlternativeBOM", ObjBOMRecipeBlock.AlternativeBOM);
            hashPara.Add("@BOMStatus", ObjBOMRecipeBlock.BOMStatus);
            hashPara.Add("@ProdVersionNo", ObjBOMRecipeBlock.ProdVersionNo);
            hashPara.Add("@Lock", ObjBOMRecipeBlock.Lock);
            hashPara.Add("@Remarks", ObjBOMRecipeBlock.Remarks);
            hashPara.Add("@IsActive", ObjBOMRecipeBlock.IsActive);
            hashPara.Add("@UserId", ObjBOMRecipeBlock.UserId);
            hashPara.Add("@UserIp", ObjBOMRecipeBlock.IPAddress);

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

        public int SaveMass(BOMRecipeBlock ObjBOMRecipeBlock)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_BOMRecipe_Block_Mass";
            int result = 0;

            hashPara.Add("@BOMRecipe_Block_Id", ObjBOMRecipeBlock.BOMRecipe_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjBOMRecipeBlock.Master_Header_Id);
            hashPara.Add("@Material_Number", ObjBOMRecipeBlock.Material_Number);            
            hashPara.Add("@Plant_Id", ObjBOMRecipeBlock.Plant_Id);
            hashPara.Add("@Recipe_Group", ObjBOMRecipeBlock.Recipe_Group);
            hashPara.Add("@Status", ObjBOMRecipeBlock.Status);
            hashPara.Add("@AlternativeBOM", ObjBOMRecipeBlock.AlternativeBOM);
            hashPara.Add("@BOMStatus", ObjBOMRecipeBlock.BOMStatus);
            hashPara.Add("@ProdVersionNo", ObjBOMRecipeBlock.ProdVersionNo);
            hashPara.Add("@Lock", ObjBOMRecipeBlock.Lock);
            hashPara.Add("@Remarks", ObjBOMRecipeBlock.Remarks);
            hashPara.Add("@IsActive", ObjBOMRecipeBlock.IsActive);
            hashPara.Add("@UserId", ObjBOMRecipeBlock.UserId);
            hashPara.Add("@UserIp", ObjBOMRecipeBlock.IPAddress);

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

        //Excel download for BOM recipe start
        //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
        public DataSet GetBOMRecipeDataByMasterheaderID(string MasterHeaderId, string userId)
        {
            //throw new NotImplementedException();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Excel_BOM_Recipe_Prod_Data_byMasterHeaderID";
            DataSet ds;

            hashPara.Add("@Master_Header_ID", MasterHeaderId);
            hashPara.Add("@userID", userId);
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

        //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
        //Excel download for BOM recipe end
    }

    
}