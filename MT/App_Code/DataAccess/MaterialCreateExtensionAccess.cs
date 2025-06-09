using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class MaterialCreateExtensionAccess
    {
        public MaterialCreateExtensionAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(MaterialCreateExtension ObjMaterialCreateExtension)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Create_Extension";
            int result = 0;

            hashPara.Add("@Mat_Create_Extension_Id", ObjMaterialCreateExtension.Mat_Create_Extension_Id);
            hashPara.Add("@Master_Header_Id", ObjMaterialCreateExtension.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjMaterialCreateExtension.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMaterialCreateExtension.Storage_Location);
            hashPara.Add("@Sales_Organization_Id", ObjMaterialCreateExtension.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjMaterialCreateExtension.Distribution_Channel_ID);
            hashPara.Add("@Mat_Pricing_Group", ObjMaterialCreateExtension.Mat_Pricing_Group);
            hashPara.Add("@Acc_Assignment_Grp", ObjMaterialCreateExtension.Acc_Assignment_Grp);
            hashPara.Add("@Purchasing_Group", ObjMaterialCreateExtension.Purchasing_Group);
            hashPara.Add("@MRP_Type", ObjMaterialCreateExtension.MRP_Type);
            hashPara.Add("@MRP_Controller", ObjMaterialCreateExtension.MRP_Controller);
            hashPara.Add("@Reorder_Point", ObjMaterialCreateExtension.Reorder_Point);
            hashPara.Add("@Lot_Size", ObjMaterialCreateExtension.Lot_Size);
            hashPara.Add("@Min_Lot_Size", ObjMaterialCreateExtension.Min_Lot_Size);
            hashPara.Add("@Max_Lot_Size", ObjMaterialCreateExtension.Max_Lot_Size);
            hashPara.Add("@Fixed_Lot_Size", ObjMaterialCreateExtension.Fixed_Lot_Size);
            hashPara.Add("@Rounding_Value", ObjMaterialCreateExtension.Rounding_Value);
            hashPara.Add("@Issue_Storage_Location", ObjMaterialCreateExtension.Issue_Storage_Location);
            hashPara.Add("@GR_Processing_Time", ObjMaterialCreateExtension.GR_Processing_Time);
            hashPara.Add("@Planned_Delivery_Time_Days", ObjMaterialCreateExtension.Planned_Delivery_Time_Days);
            hashPara.Add("@Profit_Center", ObjMaterialCreateExtension.Profit_Center);
            hashPara.Add("@Valuation_Class", ObjMaterialCreateExtension.Valuation_Class);
            hashPara.Add("@Price_Ctrl_Indicator", ObjMaterialCreateExtension.Price_Ctrl_Indicator);
            hashPara.Add("@Spl_Procurement_Type", ObjMaterialCreateExtension.Spl_Procurement_Type);
            hashPara.Add("@Inspection_Type", ObjMaterialCreateExtension.Inspection_Type);
            hashPara.Add("@Interval_Nxt_Inspection", ObjMaterialCreateExtension.Interval_Nxt_Inspection);
            hashPara.Add("@IsActive", ObjMaterialCreateExtension.IsActive);
            hashPara.Add("@UserId", ObjMaterialCreateExtension.UserId);
            hashPara.Add("@UserIp", ObjMaterialCreateExtension.IPAddress);

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

        public MaterialCreateExtension GetMaterialCreateExtension(int Mat_Create_Extension_Id)
        {
            MaterialCreateExtension ObjMaterialCreateExtension = new MaterialCreateExtension();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaterialCreateExtension_By_MaterialCreateExtensionId";
            DataSet ds;

            hashPara.Add("@Mat_Create_Extension_Id", Mat_Create_Extension_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMaterialCreateExtension.Mat_Create_Extension_Id = dt.Rows[0]["Mat_Create_Extension_Id"].ToString() == "" ? Mat_Create_Extension_Id : Convert.ToInt32(dt.Rows[0]["Mat_Create_Extension_Id"].ToString());
                        ObjMaterialCreateExtension.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjMaterialCreateExtension.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMaterialCreateExtension.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMaterialCreateExtension.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjMaterialCreateExtension.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjMaterialCreateExtension.Mat_Pricing_Group = dt.Rows[0]["Mat_Pricing_Group"].ToString();
                        ObjMaterialCreateExtension.Acc_Assignment_Grp = dt.Rows[0]["Acc_Assignment_Grp"].ToString();
                        ObjMaterialCreateExtension.Purchasing_Group = dt.Rows[0]["Purchasing_Group"].ToString();
                        ObjMaterialCreateExtension.MRP_Type = dt.Rows[0]["MRP_Type"].ToString();
                        ObjMaterialCreateExtension.MRP_Controller = dt.Rows[0]["MRP_Controller"].ToString();
                        ObjMaterialCreateExtension.Reorder_Point = dt.Rows[0]["Reorder_Point"].ToString();
                        ObjMaterialCreateExtension.Lot_Size = dt.Rows[0]["Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Min_Lot_Size = dt.Rows[0]["Min_Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Max_Lot_Size = dt.Rows[0]["Max_Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Fixed_Lot_Size = dt.Rows[0]["Fixed_Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Rounding_Value = dt.Rows[0]["Rounding_Value"].ToString();
                        ObjMaterialCreateExtension.Issue_Storage_Location = dt.Rows[0]["Issue_Storage_Location"].ToString();
                        ObjMaterialCreateExtension.GR_Processing_Time = dt.Rows[0]["GR_Processing_Time"].ToString();
                        ObjMaterialCreateExtension.Planned_Delivery_Time_Days = dt.Rows[0]["Planned_Delivery_Time_Days"].ToString();
                        ObjMaterialCreateExtension.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                        ObjMaterialCreateExtension.Valuation_Class = dt.Rows[0]["Valuation_Class"].ToString();
                        ObjMaterialCreateExtension.Price_Ctrl_Indicator = dt.Rows[0]["Price_Ctrl_Indicator"].ToString();
                        ObjMaterialCreateExtension.Spl_Procurement_Type = dt.Rows[0]["Spl_Procurement_Type"].ToString();
                        ObjMaterialCreateExtension.Inspection_Type = dt.Rows[0]["Inspection_Type"].ToString();
                        ObjMaterialCreateExtension.Interval_Nxt_Inspection = dt.Rows[0]["Interval_Nxt_Inspection"].ToString();
                    }
                }
                return ObjMaterialCreateExtension;
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

        public DataSet GetMaterialCreateExtensionData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaterialCreateExtension_By_MasterHeaderId";
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

        public int DeleteMaterialCreateExtensionData(int Mat_Create_Extension_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_MaterialCreateExtension_By_Mat_Create_Extension_Id";
            int result = 0;

            hashPara.Add("@Mat_Create_Extension_Id", Mat_Create_Extension_Id);

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

        public DataSet GetMaterialCreateExtensionReference(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetReferenceExtensionData";
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

        public MaterialCreateExtension GetMaterialCreateExtensionReferenceObj(string MasterHeaderId)
        {
            MaterialCreateExtension ObjMaterialCreateExtension = new MaterialCreateExtension();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetReferenceExtensionData";
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
                        ObjMaterialCreateExtension.Mat_Create_Extension_Id = 0;
                        ObjMaterialCreateExtension.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjMaterialCreateExtension.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMaterialCreateExtension.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMaterialCreateExtension.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjMaterialCreateExtension.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjMaterialCreateExtension.Mat_Pricing_Group = dt.Rows[0]["Mat_Pricing_Group"].ToString();
                        ObjMaterialCreateExtension.Acc_Assignment_Grp = dt.Rows[0]["Acc_Assignment_Grp"].ToString();
                        ObjMaterialCreateExtension.Purchasing_Group = dt.Rows[0]["Purchasing_Group"].ToString();
                        ObjMaterialCreateExtension.MRP_Type = dt.Rows[0]["MRP_Type"].ToString();
                        ObjMaterialCreateExtension.MRP_Controller = dt.Rows[0]["MRP_Controller"].ToString();
                        ObjMaterialCreateExtension.Reorder_Point = dt.Rows[0]["Reorder_Point"].ToString();
                        ObjMaterialCreateExtension.Lot_Size = dt.Rows[0]["Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Min_Lot_Size = dt.Rows[0]["Min_Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Max_Lot_Size = dt.Rows[0]["Max_Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Fixed_Lot_Size = dt.Rows[0]["Fixed_Lot_Size"].ToString();
                        ObjMaterialCreateExtension.Rounding_Value = dt.Rows[0]["Rounding_Value"].ToString();
                        ObjMaterialCreateExtension.Issue_Storage_Location = dt.Rows[0]["Issue_Storage_Location"].ToString();
                        ObjMaterialCreateExtension.GR_Processing_Time = dt.Rows[0]["GR_Processing_Time"].ToString();
                        ObjMaterialCreateExtension.Planned_Delivery_Time_Days = dt.Rows[0]["Planned_Delivery_Time_Days"].ToString();
                        ObjMaterialCreateExtension.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                        ObjMaterialCreateExtension.Valuation_Class = dt.Rows[0]["Valuation_Class"].ToString();
                        ObjMaterialCreateExtension.Price_Ctrl_Indicator = dt.Rows[0]["Price_Ctrl_Indicator"].ToString();
                        ObjMaterialCreateExtension.Spl_Procurement_Type = dt.Rows[0]["Spl_Procurement_Type"].ToString();
                        ObjMaterialCreateExtension.Inspection_Type = dt.Rows[0]["Inspection_Type"].ToString();
                        ObjMaterialCreateExtension.Interval_Nxt_Inspection = dt.Rows[0]["Interval_Nxt_Inspection"].ToString();
                    }
                }
                return ObjMaterialCreateExtension;
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