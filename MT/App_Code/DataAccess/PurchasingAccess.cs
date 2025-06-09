using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
namespace Accenture.MWT.DataAccess
{
    /// <summary>
    /// Summary description for PurchasingAccess
    /// </summary>
    public class PurchasingAccess
    {
        public PurchasingAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        //public static DataTable GetPlantNameBySection_Purchasing(string MHeaderID, string SectionCode)
        //{
        //    List<TSales1> sales = new List<TSales1>();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Get_Mat_Purchasing_By_MasterHeaderId";

        //    try
        //    {
        //        hashPara.Add("@Master_Header_Id", MHeaderID);
        //        hashPara.Add("@SectionCode", SectionCode);
        //        objDal.OpenConnection();
        //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        //        DataTable dtSales = new DataTable();
        //        if (dsSales != null)
        //            dtSales = dsSales.Tables[0];

        //        return dtSales;

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
        //public static List<TPurchasing> GetPurchasing()
        //{
        //    List<TPurchasing> purchasing = new List<TPurchasing>();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "USP_Get_T_Mat_Purchasing";

        //    try
        //    {
        //        objDal.OpenConnection();
        //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, ref objDal.cnnConnection);
        //        DataTable dtSales = new DataTable();
        //        if (dsSales != null)
        //            dtSales = dsSales.Tables[0];
        //        purchasing = dtSales.AsEnumerable().Select(x => new TPurchasing
        //                      {
        //                          Mat_Purchasing_Id = x["Mat_Purchasing_Id"] != DBNull.Value ? Convert.ToInt32(x["Mat_Purchasing_Id"]) : 0,
        //                          Master_Header_Id = x["Master_Header_Id"] != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0,
        //                          PlantId = x["PlantID"].ToString(),
        //                          Plant_Specific_Mat_Status = x["Plant_Specific_Mat_Status"].ToString(),
        //                          Pur_Order_Unit_Measure = x["Pur_Order_Unit_Measure"].ToString(),
        //                          Purchasing_Value_Key = x["Purchasing_Value_Key"].ToString(),
        //                          Purchasing_Group = x["Purchasing_Group"].ToString(),
        //                          Batch_Mgmt_Req_Indicator = x["Batch_Mgmt_Req_Indicator"].ToString(),
        //                          Processing_Time_Goods_Receipt_Days = x["Processing_Time_Goods_Receipt_Days"] != DBNull.Value ? Convert.ToDecimal(x["Processing_Time_Goods_Receipt_Days"]) : new Nullable<decimal>(),
        //                          Quota_Arrangement_Usage = x["Quota_Arrangement_Usage"].ToString(),
        //                          Indicator_Critical_Part = x["Indicator_Critical_Part"] != DBNull.Value ? Convert.ToBoolean(x["Indicator_Critical_Part"]) : false,
        //                          Post_Inspection_Stock = x["Post_Inspection_Stock"] != DBNull.Value ? Convert.ToBoolean(x["Post_Inspection_Stock"]) : false,
        //                          Indicator_Auto_PO_Allowed = x["Indicator_Auto_PO_Allowed"] != DBNull.Value ? Convert.ToBoolean(x["Indicator_Auto_PO_Allowed"]) : false,
        //                          Ind_Source_List_Req = x["Ind_Source_List_Req"] != DBNull.Value ? Convert.ToBoolean(x["Ind_Source_List_Req"]) : false,
        //                          Variable_Pur_Ord_Unit_Active = x["Variable_Pur_Ord_Unit_Active"].ToString(),
        //                          Tolerance_Limit_OverDelivery = x["Tolerance_Limit_OverDelivery"] != DBNull.Value ? Convert.ToDecimal(x["Tolerance_Limit_OverDelivery"]) : new Nullable<decimal>(),
        //                          Ind_Unlimited_OverDelivery_Allowed = x["Ind_Unlimited_OverDelivery_Allowed"] != DBNull.Value ? Convert.ToBoolean(x["Ind_Unlimited_OverDelivery_Allowed"]) : false,
        //                          Tolerance_Limit_UnderDelivery = x["Tolerance_Limit_UnderDelivery"] != DBNull.Value ? Convert.ToDecimal(x["Tolerance_Limit_UnderDelivery"]) : new Nullable<decimal>(),
        //                          Tax_Indicator_Material = x["Tax_Indicator_Material"].ToString(),
        //                          Mat_Freight_Grp = x["Mat_Freight_Grp"].ToString(),
        //                          No_Of_Manufacturer = x["No_Of_Manufacturer"].ToString(),
        //                          Manufacturer_Part_No = x["Manufacturer_Part_No"].ToString(),
        //                          Manufacturer_Part_Profile = x["Manufacturer_Part_Profile"].ToString(),
        //                          Cross_Plant_Mat_Status = x["Cross_Plant_Mat_Status"].ToString(),
        //                          Mat_Status_Purchasing_From = x["Mat_Status_Purchasing_From"] != DBNull.Value ? Convert.ToDateTime(x["Mat_Status_Purchasing_From"]) : new Nullable<DateTime>(),
        //                          Gen_Mat_Status_Sale_From = x["Gen_Mat_Status_Sale_From"] != DBNull.Value ? Convert.ToDateTime(x["Gen_Mat_Status_Sale_From"]) : new Nullable<DateTime>(),
        //                          Mat_Qualifies_Disc = x["Mat_Qualifies_Disc"] != DBNull.Value ? Convert.ToBoolean(x["Mat_Qualifies_Disc"]) : false,
        //                          IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false

        //                      }
        //                      ).ToList<TPurchasing>();
        //        return purchasing;

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

        //public static TPurchasing GetPurchasing(int ID, string SectionCode)
        //{
        //    TPurchasing purchasing = new TPurchasing();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "USP_Get_T_Mat_Purchasing";
        //    hashPara.Add("@PurchasingId", ID);
        //    hashPara.Add("@SectionCode", SectionCode);
        //    try
        //    {
        //        objDal.OpenConnection();
        //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        //        DataTable dtSales = new DataTable();
        //        if (dsSales != null)
        //            dtSales = dsSales.Tables[0];
        //        if (dtSales != null)
        //        {
        //            DataRow x = dtSales.Rows[0];

        //            purchasing.Mat_Purchasing_Id = x["Mat_Purchasing_Id"] != DBNull.Value ? Convert.ToInt32(x["Mat_Purchasing_Id"]) : 0;
        //            purchasing.Master_Header_Id = x["Master_Header_Id"] != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0;
        //            purchasing.PlantId = x["PlantID"].ToString();
        //            purchasing.Plant_Specific_Mat_Status = x["Plant_Specific_Mat_Status"].ToString();
        //            purchasing.Pur_Order_Unit_Measure = x["Pur_Order_Unit_Measure"].ToString();
        //            purchasing.Purchasing_Value_Key = x["Purchasing_Value_Key"].ToString();
        //            purchasing.Purchasing_Group = x["Purchasing_Group"].ToString();
        //            purchasing.Batch_Mgmt_Req_Indicator = x["Batch_Mgmt_Req_Indicator"].ToString();
        //            purchasing.Processing_Time_Goods_Receipt_Days = x["Processing_Time_Goods_Receipt_Days"] != DBNull.Value ? Convert.ToDecimal(x["Processing_Time_Goods_Receipt_Days"]) : new Nullable<decimal>();
        //            purchasing.Quota_Arrangement_Usage = x["Quota_Arrangement_Usage"].ToString();
        //            purchasing.Indicator_Critical_Part = x["Indicator_Critical_Part"] != DBNull.Value ? Convert.ToBoolean(x["Indicator_Critical_Part"]) : false;
        //            purchasing.Post_Inspection_Stock = x["Post_Inspection_Stock"] != DBNull.Value ? Convert.ToBoolean(x["Post_Inspection_Stock"]) : false;
        //            purchasing.Indicator_Auto_PO_Allowed = x["Indicator_Auto_PO_Allowed"] != DBNull.Value ? Convert.ToBoolean(x["Indicator_Auto_PO_Allowed"]) : false;
        //            purchasing.Ind_Source_List_Req = x["Ind_Source_List_Req"] != DBNull.Value ? Convert.ToBoolean(x["Ind_Source_List_Req"]) : false;
        //            purchasing.Variable_Pur_Ord_Unit_Active = x["Variable_Pur_Ord_Unit_Active"].ToString();
        //            purchasing.Tolerance_Limit_OverDelivery = x["Tolerance_Limit_OverDelivery"] != DBNull.Value ? Convert.ToDecimal(x["Tolerance_Limit_OverDelivery"]) : new Nullable<decimal>();
        //            purchasing.Ind_Unlimited_OverDelivery_Allowed = x["Ind_Unlimited_OverDelivery_Allowed"] != DBNull.Value ? Convert.ToBoolean(x["Ind_Unlimited_OverDelivery_Allowed"]) : false;
        //            purchasing.Tolerance_Limit_UnderDelivery = x["Tolerance_Limit_UnderDelivery"] != DBNull.Value ? Convert.ToDecimal(x["Tolerance_Limit_UnderDelivery"]) : new Nullable<decimal>();
        //            purchasing.Tax_Indicator_Material = x["Tax_Indicator_Material"].ToString();
        //            purchasing.Mat_Freight_Grp = x["Mat_Freight_Grp"].ToString();
        //            purchasing.No_Of_Manufacturer = x["No_Of_Manufacturer"].ToString();
        //            purchasing.Manufacturer_Part_No = x["Manufacturer_Part_No"].ToString();
        //            purchasing.Manufacturer_Part_Profile = x["Manufacturer_Part_Profile"].ToString();
        //            purchasing.Cross_Plant_Mat_Status = x["Cross_Plant_Mat_Status"].ToString();
        //            purchasing.Mat_Status_Purchasing_From = x["Mat_Status_Purchasing_From"] != DBNull.Value ? Convert.ToDateTime(x["Mat_Status_Purchasing_From"]) : new Nullable<DateTime>();
        //            purchasing.Gen_Mat_Status_Sale_From = x["Gen_Mat_Status_Sale_From"] != DBNull.Value ? Convert.ToDateTime(x["Gen_Mat_Status_Sale_From"]) : new Nullable<DateTime>();
        //            purchasing.Mat_Qualifies_Disc = x["Mat_Qualifies_Disc"] != DBNull.Value ? Convert.ToBoolean(x["Mat_Qualifies_Disc"]) : false;
        //            purchasing.IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false;


        //        }
        //        return purchasing;

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

        //public int SavePurchasing(TPurchasing purchase)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_Mat_Purchasing";
        //    int result = 0;

        //    hashPara.Add("@Mat_Purchasing_Id", purchase.Mat_Purchasing_Id);
        //    hashPara.Add("@Master_Header_Id", purchase.Master_Header_Id);
        //    hashPara.Add("@Plant_Specific_Mat_Status", purchase.Plant_Specific_Mat_Status);
        //    hashPara.Add("@Pur_Order_Unit_Measure", purchase.Pur_Order_Unit_Measure);
        //    hashPara.Add("@Purchasing_Value_Key", purchase.Purchasing_Value_Key);
        //    hashPara.Add("@Purchasing_Group", purchase.Purchasing_Group);
        //    hashPara.Add("@Batch_Mgmt_Req_Indicator", purchase.Batch_Mgmt_Req_Indicator);
        //    hashPara.Add("@Processing_Time_Goods_Receipt_Days", purchase.Processing_Time_Goods_Receipt_Days);
        //    hashPara.Add("@Quota_Arrangement_Usage", purchase.Quota_Arrangement_Usage);
        //    hashPara.Add("@Indicator_Critical_Part", purchase.Indicator_Critical_Part);
        //    hashPara.Add("@Post_Inspection_Stock", purchase.Post_Inspection_Stock);
        //    hashPara.Add("@Indicator_Auto_PO_Allowed", purchase.Indicator_Auto_PO_Allowed);
        //    hashPara.Add("@Ind_Source_List_Req", purchase.Ind_Source_List_Req);
        //    hashPara.Add("@Variable_Pur_Ord_Unit_Active", purchase.Variable_Pur_Ord_Unit_Active);
        //    hashPara.Add("@Tolerance_Limit_OverDelivery", purchase.Tolerance_Limit_OverDelivery);
        //    hashPara.Add("@Ind_Unlimited_OverDelivery_Allowed", purchase.Ind_Unlimited_OverDelivery_Allowed);
        //    hashPara.Add("@Tolerance_Limit_UnderDelivery", purchase.Tolerance_Limit_UnderDelivery);
        //    hashPara.Add("@Tax_Indicator_Material", purchase.Tax_Indicator_Material);
        //    hashPara.Add("@Mat_Freight_Grp", purchase.Mat_Freight_Grp);
        //    hashPara.Add("@No_Of_Manufacturer", purchase.No_Of_Manufacturer);
        //    hashPara.Add("@Manufacturer_Part_No", purchase.Manufacturer_Part_No);
        //    hashPara.Add("@Manufacturer_Part_Profile", purchase.Manufacturer_Part_Profile);
        //    hashPara.Add("@Cross_Plant_Mat_Status", purchase.Cross_Plant_Mat_Status);
        //    hashPara.Add("@Mat_Status_Purchasing_From", purchase.Mat_Status_Purchasing_From);
        //    hashPara.Add("@Gen_Mat_Status_Sale_From", purchase.Gen_Mat_Status_Sale_From);
        //    hashPara.Add("@Mat_Qualifies_Disc", purchase.Mat_Qualifies_Disc);
        //    hashPara.Add("@IsActive", purchase.IsActive);
        //    hashPara.Add("@UserId", purchase.UserId);
        //    hashPara.Add("@UserIp", purchase.IPAddress);
        //    hashPara.Add("@SectionCode", purchase.SectionCode);
        //    hashPara.Add("@Plant_Id", purchase.PlantId);

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


        public int Save(Purchasing ObjPurchasing)
        {
            DataAccessLayer objDal = new DataAccessLayer();

            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Purchasing";
            int result = 0;


            hashPara.Add("@Mat_Purchasing_Id", ObjPurchasing.Mat_Purchasing_Id);
            hashPara.Add("@Master_Header_Id", ObjPurchasing.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjPurchasing.Plant_Id);
            hashPara.Add("@Plant_Specific_Mat_Status", ObjPurchasing.Plant_Specific_Mat_Status);
            hashPara.Add("@Pur_Order_Unit_Measure", ObjPurchasing.Pur_Order_Unit_Measure);
            hashPara.Add("@Purchasing_Value_Key", ObjPurchasing.Purchasing_Value_Key);
            hashPara.Add("@Purchasing_Group", ObjPurchasing.Purchasing_Group);
            hashPara.Add("@Batch_Mgmt_Req_Indicator", ObjPurchasing.Batch_Mgmt_Req_Indicator);
            hashPara.Add("@Processing_Time_Goods_Receipt_Days", ObjPurchasing.Processing_Time_Goods_Receipt_Days);
            hashPara.Add("@Quota_Arrangement_Usage", ObjPurchasing.Quota_Arrangement_Usage);
            hashPara.Add("@Indicator_Critical_Part", ObjPurchasing.Indicator_Critical_Part);
            hashPara.Add("@Post_Inspection_Stock", ObjPurchasing.Post_Inspection_Stock);
            hashPara.Add("@Indicator_Auto_PO_Allowed", ObjPurchasing.Indicator_Auto_PO_Allowed);
            hashPara.Add("@Ind_Source_List_Req", ObjPurchasing.Ind_Source_List_Req);
            hashPara.Add("@Variable_Pur_Ord_Unit_Active", ObjPurchasing.Variable_Pur_Ord_Unit_Active);
            hashPara.Add("@Tolerance_Limit_OverDelivery", ObjPurchasing.Tolerance_Limit_OverDelivery);
            hashPara.Add("@Ind_Unlimited_OverDelivery_Allowed", ObjPurchasing.Ind_Unlimited_OverDelivery_Allowed);
            hashPara.Add("@Tolerance_Limit_UnderDelivery", ObjPurchasing.Tolerance_Limit_UnderDelivery);
            hashPara.Add("@Tax_Indicator_Material", ObjPurchasing.Tax_Indicator_Material);
            hashPara.Add("@Mat_Freight_Grp", ObjPurchasing.Mat_Freight_Grp);
            hashPara.Add("@No_Of_Manufacturer", ObjPurchasing.No_Of_Manufacturer);
            hashPara.Add("@Name_Of_Manufacturer", ObjPurchasing.Name_Of_Manufacturer);
            hashPara.Add("@Manufacturer_Part_No", ObjPurchasing.Manufacturer_Part_No);
            hashPara.Add("@Manufacturer_Part_Profile", ObjPurchasing.Manufacturer_Part_Profile);
            hashPara.Add("@Cross_Plant_Mat_Status", ObjPurchasing.Cross_Plant_Mat_Status);
            hashPara.Add("@Mat_Status_Purchasing_From", ObjPurchasing.Mat_Status_Purchasing_From);
            hashPara.Add("@Gen_Mat_Status_Sale_From", ObjPurchasing.Gen_Mat_Status_Sale_From);
            hashPara.Add("@Mat_Qualifies_Disc", ObjPurchasing.Mat_Qualifies_Disc);
            hashPara.Add("@GR_Processing_Time", ObjPurchasing.GR_Processing_Time);
            hashPara.Add("@Purchase_Order_Text", ObjPurchasing.Purchase_Order_Text);
            //hashPara.Add("@Change_Ref_Id", ObjPurchasing.Change_Ref_Id);

            hashPara.Add("@IsActive", ObjPurchasing.IsActive);
            hashPara.Add("@UserId", ObjPurchasing.UserId);
            hashPara.Add("@UserIp", ObjPurchasing.IPAddress);

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

        public Purchasing GetPurchasing(int Mat_Purchasing_Id)
        {
            Purchasing ObjPurchasing = new Purchasing();
            Utility ObjUtil = new Utility();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Purchasing_By_MatPurchasingId";
            DataSet ds;

            hashPara.Add("@Mat_Purchasing_Id", Mat_Purchasing_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjPurchasing.Mat_Purchasing_Id = Convert.ToInt32(dt.Rows[0]["Mat_Purchasing_Id"].ToString());
                        
                        ObjPurchasing.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjPurchasing.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjPurchasing.Plant_Specific_Mat_Status = dt.Rows[0]["Plant_Specific_Mat_Status"].ToString();
                        ObjPurchasing.Pur_Order_Unit_Measure = dt.Rows[0]["Pur_Order_Unit_Measure"].ToString();
                        ObjPurchasing.Purchasing_Value_Key = dt.Rows[0]["Purchasing_Value_Key"].ToString();
                        ObjPurchasing.Purchasing_Group = dt.Rows[0]["Purchasing_Group"].ToString();
                        ObjPurchasing.Batch_Mgmt_Req_Indicator = dt.Rows[0]["Batch_Mgmt_Req_Indicator"].ToString();
                        ObjPurchasing.Processing_Time_Goods_Receipt_Days = dt.Rows[0]["Processing_Time_Goods_Receipt_Days"].ToString();
                        ObjPurchasing.Quota_Arrangement_Usage = dt.Rows[0]["Quota_Arrangement_Usage"].ToString();
                        ObjPurchasing.Indicator_Critical_Part = dt.Rows[0]["Indicator_Critical_Part"].ToString();
                        ObjPurchasing.Post_Inspection_Stock = dt.Rows[0]["Post_Inspection_Stock"].ToString();
                        ObjPurchasing.Indicator_Auto_PO_Allowed = dt.Rows[0]["Indicator_Auto_PO_Allowed"].ToString();
                        ObjPurchasing.Ind_Source_List_Req = dt.Rows[0]["Ind_Source_List_Req"].ToString();
                        ObjPurchasing.Variable_Pur_Ord_Unit_Active = dt.Rows[0]["Variable_Pur_Ord_Unit_Active"].ToString();
                        ObjPurchasing.Tolerance_Limit_OverDelivery = dt.Rows[0]["Tolerance_Limit_OverDelivery"].ToString();
                        ObjPurchasing.Ind_Unlimited_OverDelivery_Allowed = dt.Rows[0]["Ind_Unlimited_OverDelivery_Allowed"].ToString();
                        ObjPurchasing.Tolerance_Limit_UnderDelivery = dt.Rows[0]["Tolerance_Limit_UnderDelivery"].ToString();
                        ObjPurchasing.Tax_Indicator_Material = dt.Rows[0]["Tax_Indicator_Material"].ToString();
                        ObjPurchasing.Mat_Freight_Grp = dt.Rows[0]["Mat_Freight_Grp"].ToString();
                        ObjPurchasing.No_Of_Manufacturer = dt.Rows[0]["No_Of_Manufacturer"].ToString();
                        ObjPurchasing.Name_Of_Manufacturer = dt.Rows[0]["Name_Of_Manufacturer"].ToString();                        
                        ObjPurchasing.Manufacturer_Part_No = dt.Rows[0]["Manufacturer_Part_No"].ToString();
                        ObjPurchasing.Manufacturer_Part_Profile = dt.Rows[0]["Manufacturer_Part_Profile"].ToString();
                        ObjPurchasing.Cross_Plant_Mat_Status = dt.Rows[0]["Cross_Plant_Mat_Status"].ToString();
                        ObjPurchasing.Mat_Status_Purchasing_From = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Mat_Status_Purchasing_From"].ToString());
                        ObjPurchasing.Gen_Mat_Status_Sale_From = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Gen_Mat_Status_Sale_From"].ToString());
                        ObjPurchasing.Mat_Qualifies_Disc = dt.Rows[0]["Mat_Qualifies_Disc"].ToString();
                        ObjPurchasing.GR_Processing_Time = dt.Rows[0]["GR_Processing_Time"].ToString();
                        ObjPurchasing.Purchase_Order_Text = dt.Rows[0]["Purchase_Order_Text"].ToString();


                        //ObjPurchasing.Do_Not_Cost = dt.Rows[0]["Do_Not_Cost"].ToString().ToLower() == "true" ? "1" : "0";

                    }
                }
                return ObjPurchasing;
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

        public DataSet GetPurchasingData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Purchasing_By_MasterHeaderId";
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



        public bool CheckValidPurchasingUnit(string Master_Header_Id, string Purchase_Order_Unit)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckValidPurchasingUnit";
            DataSet ds;
            bool flg = false;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Purchase_Order_Unit", Purchase_Order_Unit);

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

        public bool CheckValidOrderUnit(string masterHeaderID, string purchaseOrder)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckValidSalesUnit";
            DataSet ds;
            bool flg = true;

            hashPara.Add("@Master_Header_Id", masterHeaderID);
            hashPara.Add("@Sales_Unit", purchaseOrder);

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
    }
}