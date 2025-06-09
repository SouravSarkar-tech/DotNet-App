using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class CustomerBlockAccess
    {
        public CustomerBlockAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(CustomerBlock ObjCustomerBlock)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Customer_Block";
            int result = 0;

            hashPara.Add("@Customer_Block_Id", ObjCustomerBlock.Customer_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjCustomerBlock.Master_Header_Id);

            hashPara.Add("@Customer_Code", ObjCustomerBlock.Customer_Code);
            hashPara.Add("@Customer_Desc", ObjCustomerBlock.Customer_Desc);
            hashPara.Add("@Company_Code", ObjCustomerBlock.Company_Code);
            hashPara.Add("@Customer_Acc_Grp", ObjCustomerBlock.Customer_Acc_Grp);
            hashPara.Add("@Sales_Organisation_Id", ObjCustomerBlock.Sales_Organisation_Id);
            hashPara.Add("@Distribution_Channel_Id", ObjCustomerBlock.Distribution_Channel_Id);
            hashPara.Add("@Division_Id", ObjCustomerBlock.Division_Id);

            hashPara.Add("@IsAllCompanyBlock", ObjCustomerBlock.IsAllCompanyBlock);
            hashPara.Add("@IsSelectedCompanyBlock", ObjCustomerBlock.IsSelectedCompanyBlock);
            hashPara.Add("@IsAllSalesAreaOrderBlock", ObjCustomerBlock.IsAllSalesAreaOrderBlock);
            hashPara.Add("@IsSelectedSalesAreaOrderBlock", ObjCustomerBlock.IsSelectedSalesAreaOrderBlock);
            hashPara.Add("@IsAllSalesAreaDeliveryBlock", ObjCustomerBlock.IsAllSalesAreaDeliveryBlock);
            hashPara.Add("@IsSelectedSalesAreaDeliveryBlock", ObjCustomerBlock.IsSelectedSalesAreaDeliveryBlock);
            hashPara.Add("@IsAllSalesAreaBillingBlock", ObjCustomerBlock.IsAllSalesAreaBillingBlock);
            hashPara.Add("@IsSelectedSalesAreaBillingBlock", ObjCustomerBlock.IsSelectedSalesAreaBillingBlock);
            hashPara.Add("@IsAllSalesAreaBlockSalesSupport", ObjCustomerBlock.IsAllSalesAreaBlockSalesSupport);
            hashPara.Add("@IsSelectedSalesAreaBlockSalesSupport", ObjCustomerBlock.IsSelectedSalesAreaBlockSalesSupport);
            hashPara.Add("@Remarks", ObjCustomerBlock.Remarks);
            hashPara.Add("@IsActive", ObjCustomerBlock.IsActive);
            hashPara.Add("@UserId", ObjCustomerBlock.UserId);
            hashPara.Add("@UserIp", ObjCustomerBlock.IPAddress);

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

        public CustomerBlock GetCustomerBlock(int Master_Header_Id)
        {
            CustomerBlock ObjCustomerBlock = new CustomerBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CustomerBlock_By_MasterHeaderId";
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
                        ObjCustomerBlock.Customer_Block_Id = dt.Rows[0]["Customer_Block_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Customer_Block_Id"].ToString());
                        ObjCustomerBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? Master_Header_Id : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjCustomerBlock.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjCustomerBlock.Company_Code = dt.Rows[0]["Company_Code"].ToString();

                        ObjCustomerBlock.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                        ObjCustomerBlock.Customer_Desc = dt.Rows[0]["Customer_Desc"].ToString();
                        ObjCustomerBlock.Customer_Acc_Grp = dt.Rows[0]["Customer_Acc_Grp"].ToString();
                        ObjCustomerBlock.Customer_Category = dt.Rows[0]["Customer_Category"].ToString();

                        ObjCustomerBlock.Sales_Organisation_Id = dt.Rows[0]["Sales_Organisation_Id"].ToString();
                        ObjCustomerBlock.Distribution_Channel_Id = dt.Rows[0]["Distribution_Channel_Id"].ToString();
                        ObjCustomerBlock.Division_Id = dt.Rows[0]["Division_Id"].ToString();

                        ObjCustomerBlock.IsAllCompanyBlock = dt.Rows[0]["IsAllCompanyBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsSelectedCompanyBlock = dt.Rows[0]["IsSelectedCompanyBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsAllSalesAreaOrderBlock = dt.Rows[0]["IsAllSalesAreaOrderBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsSelectedSalesAreaOrderBlock = dt.Rows[0]["IsSelectedSalesAreaOrderBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsAllSalesAreaDeliveryBlock = dt.Rows[0]["IsAllSalesAreaDeliveryBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsSelectedSalesAreaDeliveryBlock = dt.Rows[0]["IsSelectedSalesAreaDeliveryBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsAllSalesAreaBillingBlock = dt.Rows[0]["IsAllSalesAreaBillingBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsSelectedSalesAreaBillingBlock = dt.Rows[0]["IsSelectedSalesAreaBillingBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsAllSalesAreaBlockSalesSupport = dt.Rows[0]["IsAllSalesAreaBlockSalesSupport"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.IsSelectedSalesAreaBlockSalesSupport = dt.Rows[0]["IsSelectedSalesAreaBlockSalesSupport"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustomerBlock.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjCustomerBlock;
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