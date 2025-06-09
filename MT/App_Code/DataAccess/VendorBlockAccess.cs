using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class VendorBlockAccess
    {
        public VendorBlockAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Save(VendorBlock ObjVendorBlock)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_Block";
            int result = 0;


            hashPara.Add("@Vendor_Block_Id", ObjVendorBlock.Vendor_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjVendorBlock.Master_Header_Id);
            hashPara.Add("@PurchaseOrg", ObjVendorBlock.Purchase_Org);            
            hashPara.Add("@IsAllCompanyBlock", ObjVendorBlock.IsAllCompanyBlock);
            hashPara.Add("@IsSelectedCompanyBlock", ObjVendorBlock.IsSelectedCompanyBlock);
            hashPara.Add("@IsAllPurchaseOrgBlock", ObjVendorBlock.IsAllPurchaseOrgBlock);
            hashPara.Add("@IsSelectedPurchaseOrgBlock", ObjVendorBlock.IsSelectedPurchaseOrgBlock);
            hashPara.Add("@Block_Function", ObjVendorBlock.Block_Function);
            hashPara.Add("@Payment_Block", ObjVendorBlock.Payment_Block);
            hashPara.Add("@Remarks", ObjVendorBlock.Remarks);
            hashPara.Add("@IsActive", ObjVendorBlock.IsActive);
            hashPara.Add("@UserId", ObjVendorBlock.UserId);
            hashPara.Add("@UserIp", ObjVendorBlock.IPAddress);

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



        public VendorBlock GetVendorBlock(int Master_Header_Id)
        {
            VendorBlock ObjVendorBlock = new VendorBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_VendorBlock_By_MasterHeaderId";
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
                        ObjVendorBlock.Vendor_Block_Id = dt.Rows[0]["Vendor_Block_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Vendor_Block_Id"].ToString());
                        ObjVendorBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? Master_Header_Id : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjVendorBlock.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjVendorBlock.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjVendorBlock.Purchase_Org = dt.Rows[0]["Purchase_Org"].ToString();
                        ObjVendorBlock.IsAllCompanyBlock = dt.Rows[0]["IsAllCompanyBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVendorBlock.IsSelectedCompanyBlock = dt.Rows[0]["IsSelectedCompanyBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVendorBlock.IsAllPurchaseOrgBlock = dt.Rows[0]["IsAllPurchaseOrgBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVendorBlock.IsSelectedPurchaseOrgBlock = dt.Rows[0]["IsSelectedPurchaseOrgBlock"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVendorBlock.Block_Function = dt.Rows[0]["Block_Function"].ToString();
                        ObjVendorBlock.Payment_Block = dt.Rows[0]["Payment_Block"].ToString();
                        ObjVendorBlock.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjVendorBlock;
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