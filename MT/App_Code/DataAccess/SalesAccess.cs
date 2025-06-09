using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    /// <summary>
    /// Summary description for SalesAccess
    /// </summary>
    public class SalesAccess
    {
        public SalesAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Sales 1

        public int Save(Sales1 ObjSales)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Sales1";
            int result = 0;


            hashPara.Add("@Mat_Sales1_Id", ObjSales.Mat_Sales1_Id);
            hashPara.Add("@Master_Header_Id", ObjSales.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjSales.Plant_Id);
            hashPara.Add("@Sales_Organization_Id", ObjSales.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjSales.Distribution_Channel_ID);
            hashPara.Add("@Sales_Unit", ObjSales.Sales_Unit);
            hashPara.Add("@Distri_Chain_Speci_Mat_Status", ObjSales.Distri_Chain_Speci_Mat_Status);
            hashPara.Add("@Mat_Status_Sales_Valid_From", ObjSales.Mat_Status_Sales_Valid_From);
            hashPara.Add("@Delivery_Plant", ObjSales.Delivery_Plant);
            hashPara.Add("@Min_Order_Quan_Base_Unit", ObjSales.Min_Order_Quan_Base_Unit);
            hashPara.Add("@Min_Delivery_Quan_Delivery_Note", ObjSales.Min_Delivery_Quan_Delivery_Note);
            hashPara.Add("@Min_Make_To_Order_Quan", ObjSales.Min_Make_To_Order_Quan);
            hashPara.Add("@Delivery_Unit", ObjSales.Delivery_Unit);
            hashPara.Add("@Unit_Measure_Delivery_Unit", ObjSales.Unit_Measure_Delivery_Unit);
            hashPara.Add("@Is_Cash_Discount", ObjSales.Is_Cash_Discount);
            hashPara.Add("@Vari_Sales_Unit_Not_Allowed", ObjSales.Vari_Sales_Unit_Not_Allowed);
            hashPara.Add("@Sales_Text", ObjSales.Sales_Text);
            hashPara.Add("@IsActive", ObjSales.IsActive);
            hashPara.Add("@UserId", ObjSales.UserId);
            hashPara.Add("@UserIp", ObjSales.IPAddress);
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

        public Sales1 GetSales1(int Mat_Sales1_Id)
        {
            Sales1 ObjSales = new Sales1();

            DataAccessLayer objDal = new DataAccessLayer();

            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Sales1_By_MatSales1Id";
            DataSet ds;

            hashPara.Add("@Mat_Sales1_Id", Mat_Sales1_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjSales.Mat_Sales1_Id = Convert.ToInt32(dt.Rows[0]["Mat_Sales1_Id"].ToString());
                        ObjSales.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjSales.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjSales.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjSales.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjSales.Sales_Unit = dt.Rows[0]["Sales_Unit"].ToString();
                        ObjSales.Distri_Chain_Speci_Mat_Status = dt.Rows[0]["Distri_Chain_Speci_Mat_Status"].ToString();
                        ObjSales.Mat_Status_Sales_Valid_From = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Mat_Status_Sales_Valid_From"].ToString());
                        ObjSales.Delivery_Plant = dt.Rows[0]["Delivery_Plant"].ToString();
                        ObjSales.Min_Order_Quan_Base_Unit = dt.Rows[0]["Min_Order_Quan_Base_Unit"].ToString();
                        ObjSales.Min_Delivery_Quan_Delivery_Note = dt.Rows[0]["Min_Delivery_Quan_Delivery_Note"].ToString();
                        ObjSales.Min_Make_To_Order_Quan = dt.Rows[0]["Min_Make_To_Order_Quan"].ToString();
                        ObjSales.Delivery_Unit = dt.Rows[0]["Delivery_Unit"].ToString();
                        ObjSales.Unit_Measure_Delivery_Unit = dt.Rows[0]["Unit_Measure_Delivery_Unit"].ToString();
                        ObjSales.Is_Cash_Discount = dt.Rows[0]["Is_Cash_Discount"].ToString();
                        ObjSales.Vari_Sales_Unit_Not_Allowed = dt.Rows[0]["Vari_Sales_Unit_Not_Allowed"].ToString();
                        ObjSales.Sales_Text = dt.Rows[0]["Sales_Text"].ToString();
                    }
                }
                return ObjSales;
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

        public DataSet GetSalesData1(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Sales1_By_MasterHeaderId";
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

        public bool CheckValidSalesUnit(string masterHeaderID, string SalesUnit)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckValidSalesUnit";
            DataSet ds;
            bool flg = true;

            hashPara.Add("@Master_Header_Id", masterHeaderID);
            hashPara.Add("@Sales_Unit", SalesUnit);

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

        public bool CheckValidSalesUnitInBasicData2(string masterHeaderID, string SalesUnit)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckValidSalesUnitInBasicData2";
            DataSet ds;
            bool flg = false;

            hashPara.Add("@Master_Header_Id", masterHeaderID);
            hashPara.Add("@Sales_Unit", SalesUnit);

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

        #endregion

        #region Sales 2

        public int Save(Sales2 ObjSales)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Sales2";
            int result = 0;


            hashPara.Add("@Mat_Sales2_Id", ObjSales.Mat_Sales2_Id);
            hashPara.Add("@Master_Header_Id", ObjSales.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjSales.Plant_Id);
            hashPara.Add("@Sales_Organization_Id", ObjSales.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjSales.Distribution_Channel_ID);
            hashPara.Add("@Mat_Statistic_Group", ObjSales.Mat_Statistic_Group);
            hashPara.Add("@Volume_Rebate_Group", ObjSales.Volume_Rebate_Group);
            hashPara.Add("@Commission_Group", ObjSales.Commission_Group);
            hashPara.Add("@Gen_Item_Category_Grp", ObjSales.Gen_Item_Category_Grp);
            hashPara.Add("@Item_Category_Grp", ObjSales.Item_Category_Grp);
            hashPara.Add("@Product_Hierarchy", ObjSales.Product_Hierarchy);
            hashPara.Add("@Pricing_Ref_Mat", ObjSales.Pricing_Ref_Mat);
            hashPara.Add("@Mat_Pricing_Group", ObjSales.Mat_Pricing_Group);
            hashPara.Add("@Acc_Assignment_Grp", ObjSales.Acc_Assignment_Grp);
            hashPara.Add("@Material_Group1", ObjSales.Material_Group1);
            hashPara.Add("@Material_Group2", ObjSales.Material_Group2);
            hashPara.Add("@Material_Group3", ObjSales.Material_Group3);
            hashPara.Add("@Material_Group4", ObjSales.Material_Group4);
            hashPara.Add("@Material_Group5", ObjSales.Material_Group5);
            hashPara.Add("@Cross_Distrib_Chain_Mat_Status", ObjSales.Cross_Distrib_Chain_Mat_Status);
            hashPara.Add("@CAS_No_Pharma_Prod_FT", ObjSales.CAS_No_Pharma_Prod_FT);
            hashPara.Add("@IsActive", ObjSales.IsActive);
            hashPara.Add("@UserId", ObjSales.UserId);
            hashPara.Add("@UserIp", ObjSales.IPAddress);
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

        public Sales2 GetSales2(int Mat_Sales2_Id)
        {
            Sales2 ObjSales = new Sales2();

            DataAccessLayer objDal = new DataAccessLayer();

            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Sales2_By_MatSales2Id";
            DataSet ds;

            hashPara.Add("@Mat_Sales2_Id", Mat_Sales2_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjSales.Mat_Sales2_Id = Convert.ToInt32(dt.Rows[0]["Mat_Sales2_Id"].ToString());
                        ObjSales.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjSales.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjSales.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjSales.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjSales.Mat_Statistic_Group = dt.Rows[0]["Mat_Statistic_Group"].ToString();
                        ObjSales.Volume_Rebate_Group = dt.Rows[0]["Volume_Rebate_Group"].ToString();
                        ObjSales.Commission_Group = dt.Rows[0]["Commission_Group"].ToString();
                        ObjSales.Gen_Item_Category_Grp = dt.Rows[0]["Gen_Item_Category_Grp"].ToString();
                        ObjSales.Item_Category_Grp = dt.Rows[0]["Item_Category_Grp"].ToString();
                        ObjSales.Product_Hierarchy = dt.Rows[0]["Product_Hierarchy"].ToString();
                        ObjSales.Pricing_Ref_Mat = dt.Rows[0]["Pricing_Ref_Mat"].ToString();
                        ObjSales.Mat_Pricing_Group = dt.Rows[0]["Mat_Pricing_Group"].ToString();
                        ObjSales.Acc_Assignment_Grp = dt.Rows[0]["Acc_Assignment_Grp"].ToString();
                        ObjSales.Material_Group1 = dt.Rows[0]["Material_Group1"].ToString();
                        ObjSales.Material_Group2 = dt.Rows[0]["Material_Group2"].ToString();
                        ObjSales.Material_Group3 = dt.Rows[0]["Material_Group3"].ToString();
                        ObjSales.Material_Group4 = dt.Rows[0]["Material_Group4"].ToString();
                        ObjSales.Material_Group5 = dt.Rows[0]["Material_Group5"].ToString();
                        ObjSales.Cross_Distrib_Chain_Mat_Status = dt.Rows[0]["Cross_Distrib_Chain_Mat_Status"].ToString();
                        ObjSales.CAS_No_Pharma_Prod_FT = dt.Rows[0]["CAS_No_Pharma_Prod_FT"].ToString();
                    }
                }
                return ObjSales;
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

        public DataSet GetSalesData2(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Sales2_By_MasterHeaderId";
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

        #endregion

        #region Sales 3

        public int Save(Sales3 ObjSales)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Sales3";
            int result = 0;


            hashPara.Add("@Mat_Sales3_Id", ObjSales.Mat_Sales3_Id);
            hashPara.Add("@Master_Header_Id", ObjSales.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjSales.Plant_Id);
            hashPara.Add("@Sales_Organization_Id", ObjSales.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjSales.Distribution_Channel_ID);
            hashPara.Add("@Transportation_Group", ObjSales.Transportation_Group);
            hashPara.Add("@Shipping_SetUp_Time", ObjSales.Shipping_SetUp_Time);
            hashPara.Add("@Base_Quan_Capital_Plan", ObjSales.Base_Quan_Capital_Plan);
            hashPara.Add("@Shipping_Processing_Time", ObjSales.Shipping_Processing_Time);
            hashPara.Add("@Loading_Group", ObjSales.Loading_Group);
            hashPara.Add("@Availability_Check", ObjSales.Availability_Check);
            hashPara.Add("@Batch_Mgmt", ObjSales.Batch_Mgmt);
            hashPara.Add("@Profit_Center", ObjSales.Profit_Center);
            hashPara.Add("@IsActive", ObjSales.IsActive);
            hashPara.Add("@UserId", ObjSales.UserId);
            hashPara.Add("@UserIp", ObjSales.IPAddress);
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

        public Sales3 GetSales3(int Mat_Sales3_Id)
        {
            Sales3 ObjSales = new Sales3();

            DataAccessLayer objDal = new DataAccessLayer();

            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Sales3_By_MatSales3Id";
            DataSet ds;

            hashPara.Add("@Mat_Sales3_Id", Mat_Sales3_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjSales.Mat_Sales3_Id = Convert.ToInt32(dt.Rows[0]["Mat_Sales3_Id"].ToString());
                        ObjSales.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjSales.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjSales.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjSales.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjSales.Transportation_Group = dt.Rows[0]["Transportation_Group"].ToString();
                        ObjSales.Shipping_SetUp_Time = dt.Rows[0]["Shipping_SetUp_Time"].ToString();
                        ObjSales.Base_Quan_Capital_Plan = dt.Rows[0]["Base_Quan_Capital_Plan"].ToString();
                        ObjSales.Shipping_Processing_Time = dt.Rows[0]["Shipping_Processing_Time"].ToString();
                        ObjSales.Loading_Group = dt.Rows[0]["Loading_Group"].ToString();
                        ObjSales.Availability_Check = dt.Rows[0]["Availability_Check"].ToString();
                        ObjSales.Batch_Mgmt = dt.Rows[0]["Batch_Mgmt"].ToString();
                        ObjSales.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                    }
                }
                return ObjSales;
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

        public DataSet GetSalesData3(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Sales3_By_MasterHeaderId";
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

        #endregion




        
    }
}