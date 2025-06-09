using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;
using System.Data.SqlClient;

namespace Accenture.MWT.DataAccess
{
    public class MaterialExtensionAccess
    {
        public MaterialExtensionAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(MaterialExtension ObjMaterialExtension)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Material_Extension";
            int result = 0;

            hashPara.Add("@Material_Extension_Id", ObjMaterialExtension.Material_Extension_Id);
            hashPara.Add("@Master_Header_Id", ObjMaterialExtension.Master_Header_Id);
            hashPara.Add("@Material_Number", ObjMaterialExtension.Material_Number);
            hashPara.Add("@Material_Type", ObjMaterialExtension.Material_Type);
            hashPara.Add("@Material_Short_Description", ObjMaterialExtension.Material_Short_Description);
            hashPara.Add("@Plant_Id", ObjMaterialExtension.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMaterialExtension.Storage_Location);
            hashPara.Add("@Sales_Organization_Id", ObjMaterialExtension.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjMaterialExtension.Distribution_Channel_ID);
            hashPara.Add("@Mat_Pricing_Group", ObjMaterialExtension.Mat_Pricing_Group);
            hashPara.Add("@Acc_Assignment_Grp", ObjMaterialExtension.Acc_Assignment_Grp);
            hashPara.Add("@Purchasing_Group", ObjMaterialExtension.Purchasing_Group);
            hashPara.Add("@MRP_Type", ObjMaterialExtension.MRP_Type);
            hashPara.Add("@MRP_Controller", ObjMaterialExtension.MRP_Controller);
            hashPara.Add("@Reorder_Point", ObjMaterialExtension.Reorder_Point);
            hashPara.Add("@Lot_Size", ObjMaterialExtension.Lot_Size);
            hashPara.Add("@Min_Lot_Size", ObjMaterialExtension.Min_Lot_Size);
            hashPara.Add("@Max_Lot_Size", ObjMaterialExtension.Max_Lot_Size);
            hashPara.Add("@Fixed_Lot_Size", ObjMaterialExtension.Fixed_Lot_Size);
            hashPara.Add("@Rounding_Value", ObjMaterialExtension.Rounding_Value);
            hashPara.Add("@Issue_Storage_Location", ObjMaterialExtension.Issue_Storage_Location);
            hashPara.Add("@GR_Processing_Time", ObjMaterialExtension.GR_Processing_Time);
            hashPara.Add("@Planned_Delivery_Time_Days", ObjMaterialExtension.Planned_Delivery_Time_Days);
            hashPara.Add("@Profit_Center", ObjMaterialExtension.Profit_Center);
            hashPara.Add("@Valuation_Class", ObjMaterialExtension.Valuation_Class);
            hashPara.Add("@Price_Ctrl_Indicator", ObjMaterialExtension.Price_Ctrl_Indicator);

            hashPara.Add("@Spl_Procurement_Type", ObjMaterialExtension.Spl_Procurement_Type);
            hashPara.Add("@Inspection_Type", ObjMaterialExtension.Inspection_Type);
            hashPara.Add("@Interval_Nxt_Inspection", ObjMaterialExtension.Interval_Nxt_Inspection);
            //MSE_8300002156
            hashPara.Add("@Is_QM_in_Procurement", ObjMaterialExtension.Is_QM_in_Procurement);
            hashPara.Add("@Certificate_Type", ObjMaterialExtension.Certificate_Type);
            hashPara.Add("@Ctrl_Key_QM_Procurement", ObjMaterialExtension.Ctrl_Key_QM_Procurement);
            hashPara.Add("@Range_Coverage_Profile", ObjMaterialExtension.Range_Coverage_Profile);
            //MSE_8300002156
            hashPara.Add("@Warehouse_ID", ObjMaterialExtension.Warehouse_ID);
            hashPara.Add("@Storage_Type_ID", ObjMaterialExtension.Storage_Type_ID);
            hashPara.Add("@Capacity_Usage", ObjMaterialExtension.Capacity_Usage);
            hashPara.Add("@WM_Unit_Measure", ObjMaterialExtension.WM_Unit_Measure);
            hashPara.Add("@Stor_Type_Ind_Stock_Placement", ObjMaterialExtension.Stor_Type_Ind_Stock_Placement);
            hashPara.Add("@Stor_Type_Ind_Stock_Removal", ObjMaterialExtension.Stor_Type_Ind_Stock_Removal);
            hashPara.Add("@Storage_Section_Ind", ObjMaterialExtension.Storage_Section_Ind);
            hashPara.Add("@Loading_Equipment_Quantity", ObjMaterialExtension.Loading_Equipment_Quantity);
            hashPara.Add("@Loading_Equipment_Quantity1", ObjMaterialExtension.Loading_Equipment_Quantity1);
            hashPara.Add("@Loading_Equipment_Quantity2", ObjMaterialExtension.Loading_Equipment_Quantity2);
            hashPara.Add("@Unit_Loading_Equip_Quan", ObjMaterialExtension.Unit_Loading_Equip_Quan);
            hashPara.Add("@Unit_Loading_Equip_Quan1", ObjMaterialExtension.Unit_Loading_Equip_Quan1);
            hashPara.Add("@Unit_Loading_Equip_Quan2", ObjMaterialExtension.Unit_Loading_Equip_Quan2);
            hashPara.Add("@Storage_Unit_Type", ObjMaterialExtension.Storage_Unit_Type);
            hashPara.Add("@Storage_Unit_Type1", ObjMaterialExtension.Storage_Unit_Type1);
            hashPara.Add("@Storage_Unit_Type2", ObjMaterialExtension.Storage_Unit_Type2);
            //hashPara.Add("@Reason_For_Creation", ObjMaterialExtension.Reason_For_Creation);
            hashPara.Add("@Remarks", ObjMaterialExtension.Remarks);
            //MAT_DT26102020
            hashPara.Add("@MInspType", ObjMaterialExtension.MInspType);
            //MAT_DT26102020
            hashPara.Add("@IsActive", ObjMaterialExtension.IsActive);
            hashPara.Add("@UserId", ObjMaterialExtension.UserId);
            hashPara.Add("@UserIp", ObjMaterialExtension.IPAddress);
            //NRDT02032023 Start
            hashPara.Add("@QMATA_CR", ObjMaterialExtension.MatAuthGrpActQM);
            //NRDT02032023 End
            //MSE_8300002156
            hashPara.Add("@IsDraf", false);
            //MSE_8300002156
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

        public MaterialExtension GetMaterialExtension(int Material_Extension_Id)
        {
            MaterialExtension ObjMaterialExtension = new MaterialExtension();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaterialExtension_By_MaterialExtensionId";
            DataSet ds;

            hashPara.Add("@Material_Extension_Id", Material_Extension_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMaterialExtension.Material_Extension_Id = dt.Rows[0]["Material_Extension_Id"].ToString() == "" ? Material_Extension_Id : Convert.ToInt32(dt.Rows[0]["Material_Extension_Id"].ToString());
                        ObjMaterialExtension.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjMaterialExtension.Material_Number = dt.Rows[0]["Material_Number"].ToString();
                        ObjMaterialExtension.Material_Type = dt.Rows[0]["Material_Type"].ToString();
                        ObjMaterialExtension.Material_Short_Description = dt.Rows[0]["Material_Short_Description"].ToString();
                        ObjMaterialExtension.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMaterialExtension.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMaterialExtension.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjMaterialExtension.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjMaterialExtension.Mat_Pricing_Group = dt.Rows[0]["Mat_Pricing_Group"].ToString();
                        ObjMaterialExtension.Acc_Assignment_Grp = dt.Rows[0]["Acc_Assignment_Grp"].ToString();
                        ObjMaterialExtension.Purchasing_Group = dt.Rows[0]["Purchasing_Group"].ToString();
                        ObjMaterialExtension.MRP_Type = dt.Rows[0]["MRP_Type"].ToString();
                        ObjMaterialExtension.MRP_Controller = dt.Rows[0]["MRP_Controller"].ToString();
                        ObjMaterialExtension.Reorder_Point = dt.Rows[0]["Reorder_Point"].ToString();
                        ObjMaterialExtension.Lot_Size = dt.Rows[0]["Lot_Size"].ToString();
                        ObjMaterialExtension.Min_Lot_Size = dt.Rows[0]["Min_Lot_Size"].ToString();
                        ObjMaterialExtension.Max_Lot_Size = dt.Rows[0]["Max_Lot_Size"].ToString();
                        ObjMaterialExtension.Fixed_Lot_Size = dt.Rows[0]["Fixed_Lot_Size"].ToString();
                        ObjMaterialExtension.Rounding_Value = dt.Rows[0]["Rounding_Value"].ToString();
                        ObjMaterialExtension.Issue_Storage_Location = dt.Rows[0]["Issue_Storage_Location"].ToString();
                        ObjMaterialExtension.GR_Processing_Time = dt.Rows[0]["GR_Processing_Time"].ToString();
                        ObjMaterialExtension.Planned_Delivery_Time_Days = dt.Rows[0]["Planned_Delivery_Time_Days"].ToString();
                        ObjMaterialExtension.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                        ObjMaterialExtension.Valuation_Class = dt.Rows[0]["Valuation_Class"].ToString();
                        ObjMaterialExtension.Price_Ctrl_Indicator = dt.Rows[0]["Price_Ctrl_Indicator"].ToString();

                        ObjMaterialExtension.Spl_Procurement_Type = dt.Rows[0]["Spl_Procurement_Type"].ToString();
                        ObjMaterialExtension.Inspection_Type = dt.Rows[0]["Inspection_Type"].ToString();
                        ObjMaterialExtension.Interval_Nxt_Inspection = dt.Rows[0]["Interval_Nxt_Inspection"].ToString();
                        //MSE_8300002156
                        ObjMaterialExtension.Is_QM_in_Procurement = dt.Rows[0]["Is_QM_in_Procurement"].ToString();
                        ObjMaterialExtension.Certificate_Type = dt.Rows[0]["Certificate_Type"].ToString();
                        ObjMaterialExtension.Ctrl_Key_QM_Procurement = dt.Rows[0]["Ctrl_Key_QM_Procurement"].ToString();
                        ObjMaterialExtension.Range_Coverage_Profile = dt.Rows[0]["Range_Coverage_Profile"].ToString();
                        //MSE_8300002156


                        ObjMaterialExtension.Warehouse_ID = dt.Rows[0]["Warehouse_ID"].ToString();
                        ObjMaterialExtension.Storage_Type_ID = dt.Rows[0]["Storage_Type_ID"].ToString();
                        ObjMaterialExtension.Capacity_Usage = dt.Rows[0]["Capacity_Usage"].ToString();
                        ObjMaterialExtension.WM_Unit_Measure = dt.Rows[0]["WM_Unit_Measure"].ToString();
                        ObjMaterialExtension.Stor_Type_Ind_Stock_Placement = dt.Rows[0]["Stor_Type_Ind_Stock_Placement"].ToString();
                        ObjMaterialExtension.Stor_Type_Ind_Stock_Removal = dt.Rows[0]["Stor_Type_Ind_Stock_Removal"].ToString();
                        ObjMaterialExtension.Storage_Section_Ind = dt.Rows[0]["Storage_Section_Ind"].ToString();
                        ObjMaterialExtension.Loading_Equipment_Quantity = dt.Rows[0]["Loading_Equipment_Quantity"].ToString();
                        ObjMaterialExtension.Loading_Equipment_Quantity1 = dt.Rows[0]["Loading_Equipment_Quantity1"].ToString();
                        ObjMaterialExtension.Loading_Equipment_Quantity2 = dt.Rows[0]["Loading_Equipment_Quantity2"].ToString();
                        ObjMaterialExtension.Unit_Loading_Equip_Quan = dt.Rows[0]["Unit_Loading_Equip_Quan"].ToString();
                        ObjMaterialExtension.Unit_Loading_Equip_Quan1 = dt.Rows[0]["Unit_Loading_Equip_Quan1"].ToString();
                        ObjMaterialExtension.Unit_Loading_Equip_Quan2 = dt.Rows[0]["Unit_Loading_Equip_Quan2"].ToString();
                        ObjMaterialExtension.Storage_Unit_Type = dt.Rows[0]["Storage_Unit_Type"].ToString();
                        ObjMaterialExtension.Storage_Unit_Type1 = dt.Rows[0]["Storage_Unit_Type1"].ToString();
                        ObjMaterialExtension.Storage_Unit_Type2 = dt.Rows[0]["Storage_Unit_Type2"].ToString();

                        //ObjMaterialExtension.Reason_For_Creation = dt.Rows[0]["Reason_For_Creation"].ToString();
                        ObjMaterialExtension.Remarks = dt.Rows[0]["Remarks"].ToString();
                        //MAT_DT26102020
                        ObjMaterialExtension.MInspType = dt.Rows[0]["MInspType"].ToString();
                        ObjMaterialExtension.IsDraf = dt.Rows[0]["IsDraf"].ToString();
                        //MAT_DT26102020

                        //NRDT02032023 Start
                        ObjMaterialExtension.MatAuthGrpActQM = dt.Rows[0]["MatAuthGrpActQM"].ToString();
                        //NRDT02032023 End
                    }
                }
                return ObjMaterialExtension;
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

        public DataSet GetMaterialExtensionData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaterialExtension_By_MasterHeaderId";
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

        public int DeleteMaterialExtensionData(int Material_Extension_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_MaterialExtension_By_Material_Extension_Id";
            int result = 0;

            hashPara.Add("@Material_Extension_Id", Material_Extension_Id);

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

        public void GetDefaultSalesOrganization(string plantID, int moduleID, out string SalesOrgID, out string DistributionChannelID, out int ret)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Get_SalesOrg_DistChnl_By_PlantID";
            SalesOrgID = DistributionChannelID = "";
            //int retVal = 0;


            objCommand.Parameters.AddWithValue("@PlantId", plantID);
            objCommand.Parameters.AddWithValue("@ModuleID", moduleID);

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
    }
}