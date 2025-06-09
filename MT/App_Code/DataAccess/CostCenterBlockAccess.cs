using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CostCenterBlockAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class CostCenterBlockAccess
    {
        public CostCenterBlockAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(CostCenterBlock ObjCostCenterBlock)
        {
            DataSet ds = new DataSet();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_CostCenter_Block_Test";
            int result = 0;


            hashPara.Add("@CostCenter_Block_Id", ObjCostCenterBlock.CostCenter_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjCostCenterBlock.Master_Header_Id);
            hashPara.Add("@Cost_Center", ObjCostCenterBlock.Cost_Center);
            hashPara.Add("@Cost_Center_Name", ObjCostCenterBlock.Cost_Center_Name);

            hashPara.Add("@LIActualPrimaryCost", ObjCostCenterBlock.LIActualPrimaryCost);
            hashPara.Add("@LIPlanPrimaryCost", ObjCostCenterBlock.LIPlanPrimaryCost);
            hashPara.Add("@LIActualSecondaryCost", ObjCostCenterBlock.LIActualSecondaryCost);
            hashPara.Add("@LIPlanSecondaryCost", ObjCostCenterBlock.LIPlanSecondaryCost);
            hashPara.Add("@LIActualRevenuePosting", ObjCostCenterBlock.LIActualRevenuePosting);
            hashPara.Add("@LIPlanRevenuePosting", ObjCostCenterBlock.LIPlanRevenuePosting);

            hashPara.Add("@Remarks", ObjCostCenterBlock.Remarks);
            hashPara.Add("@UserId", ObjCostCenterBlock.UserId);
            hashPara.Add("@UserIp", ObjCostCenterBlock.IPAddress);

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

        public CostCenterBlock GetCostCenterBlock(int Master_Header_Id)
        {
            CostCenterBlock ObjCostCenterBlock = new CostCenterBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CostCenterBlock_By_MasterHeaderId";
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
                        ObjCostCenterBlock.CostCenter_Block_Id = dt.Rows[0]["CostCenter_Block_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["CostCenter_Block_Id"].ToString());
                        ObjCostCenterBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? Master_Header_Id : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjCostCenterBlock.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjCostCenterBlock.Cost_Center = dt.Rows[0]["CostCenter"].ToString();
                        ObjCostCenterBlock.Cost_Center_Name = dt.Rows[0]["CostCenterName"].ToString();
                        ObjCostCenterBlock.LIActualPrimaryCost = dt.Rows[0]["LIActualPrimaryCost"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCostCenterBlock.LIPlanPrimaryCost = dt.Rows[0]["LIPlanPrimaryCost"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCostCenterBlock.LIActualSecondaryCost = dt.Rows[0]["LIActualSecondaryCost"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCostCenterBlock.LIPlanSecondaryCost = dt.Rows[0]["LIPlanSecondaryCost"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCostCenterBlock.LIActualRevenuePosting = dt.Rows[0]["LIActualRevenuePosting"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCostCenterBlock.LIPlanRevenuePosting = dt.Rows[0]["LIPlanRevenuePosting"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCostCenterBlock.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjCostCenterBlock;
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