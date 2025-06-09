using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;
using Accenture.MWTT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class PurchOrgDataAccess
    {
        public PurchOrgDataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int Save(PurchOrgData ObjPurchase)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_PurchOrgData";
            int result = 0;
            hashPara.Add("@Master_Header_Id", ObjPurchase.Master_Header_Id);
            hashPara.Add("@Vendor_PurchOrgData", ObjPurchase.Vendor_PurchOrgData_id);
            hashPara.Add("@PurchaseOrder_Currency", ObjPurchase.PurchaseOrder_Currency);
            hashPara.Add("@TermsPayment_Key", ObjPurchase.TermsPayment_Key);
            hashPara.Add("@IncotermsPart1", ObjPurchase.IncotermsPart1);
            hashPara.Add("@IncotermsPart2", ObjPurchase.IncotermsPart2);
            hashPara.Add("@MinimumOrder_batchInput", ObjPurchase.MinimumOrder_batchInput);
            hashPara.Add("@Responsible_SalesPerson", ObjPurchase.Responsible_SalesPerson);
            hashPara.Add("@Vendor_TelephoneNumber", ObjPurchase.Vendor_TelephoneNumber);
            hashPara.Add("@ABC_Indicator", ObjPurchase.ABC_Indicator);
            hashPara.Add("@PurchasingBlock_Purchasing", ObjPurchase.PurchasingBlock_Purchasing);
            hashPara.Add("@Deleteflag_purchasinglevel", ObjPurchase.Deleteflag_purchasinglevel);
            hashPara.Add("@IndicatorInvoice_Verification", ObjPurchase.IndicatorInvoice_Verification);
            hashPara.Add("@OrderAcknowledgment_Requirement", ObjPurchase.OrderAcknowledgment_Requirement);
            hashPara.Add("@GroupCalculation_SchemaVendor", ObjPurchase.GroupCalculation_SchemaVendor);
            hashPara.Add("@Automatic_Generation", ObjPurchase.Automatic_Generation);
            hashPara.Add("@ModeTransport_ForeignTrade", ObjPurchase.ModeTransport_ForeignTrade);
            hashPara.Add("@CustomsOffice_ForeignTrade", ObjPurchase.CustomsOffice_ForeignTrade);
            hashPara.Add("@Purchasing_Group", ObjPurchase.Purchasing_Group);
            hashPara.Add("@Indicator_vendor_accountimng", ObjPurchase.Indicator_vendor_accountimng);
            hashPara.Add("@PlannedTime_Days_BTCI", ObjPurchase.PlannedTime_Days_BTCI);
            hashPara.Add("@Shipping_Conditions", ObjPurchase.Shipping_Conditions);
            hashPara.Add("@Indicator_ServiceBased_Verification", ObjPurchase.Indicator_ServiceBased_Verification);
            hashPara.Add("@StagingTime_Days_BatchInput", ObjPurchase.StagingTime_Days_BatchInput);
            hashPara.Add("@Category_tax_codes", ObjPurchase.Category_tax_codes);
            hashPara.Add("@Vendor_Subrange", ObjPurchase.Vendor_Subrange);
            hashPara.Add("@Language_BatchInputField", ObjPurchase.Language_BatchInputField);
            hashPara.Add("@Purchasing_Organization", ObjPurchase.Purchasing_Organization);
            hashPara.Add("@Plant", ObjPurchase.Plant);
            hashPara.Add("@Partner_Function", ObjPurchase.Partner_Function);
            hashPara.Add("@Number_Of_Business_Master_in_Vendor_Master_Record", ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record);
            hashPara.Add("@Partner_Function2", ObjPurchase.Partner_Function2);
            hashPara.Add("@Number_Of_Business_Master_in_Vendor_Master_Record2", ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record2);
            hashPara.Add("@Partner_Function3", ObjPurchase.Partner_Function3);
            hashPara.Add("@Number_Of_Business_Master_in_Vendor_Master_Record3", ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record3);
            hashPara.Add("@Partner_Function4", ObjPurchase.Partner_Function4);
            hashPara.Add("@Number_Of_Business_Master_in_Vendor_Master_Record4", ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record4);
            hashPara.Add("@Partner_Function5", ObjPurchase.Partner_Function5);
            hashPara.Add("@Number_Of_Business_Master_in_Vendor_Master_Record5", ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record5);
            hashPara.Add("@Partner_Function6", ObjPurchase.Partner_Function6);
            hashPara.Add("@Number_Of_Business_Master_in_Vendor_Master_Record6", ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record6);
            hashPara.Add("@Partner_counter", ObjPurchase.Partner_counter);
            hashPara.Add("@Name_Person_who_CreatedObject", ObjPurchase.Name_Person_who_CreatedObject);
            hashPara.Add("@Date_Which_Record_Created", ObjPurchase.Date_Which_Record_Created);
            hashPara.Add("@Reference_vendor", ObjPurchase.Reference_vendor);
            hashPara.Add("@Personnel_Number_BatchInputField", ObjPurchase.Personnel_Number_BatchInputField);
            hashPara.Add("@Country_Key", ObjPurchase.Country_Key);
            hashPara.Add("@SupplyRegion_RegionSupplied", ObjPurchase.SupplyRegion_RegionSupplied);
            hashPara.Add("@AccountNumber_VendorCreditor", ObjPurchase.AccountNumber_VendorCreditor);
            hashPara.Add("@Material_Number", ObjPurchase.Material_Number);
            hashPara.Add("@Preference_Zone", ObjPurchase.Preference_Zone);
            hashPara.Add("@IsActive", ObjPurchase.IsActive);
            hashPara.Add("@UserId", ObjPurchase.UserId);
            hashPara.Add("@UserIp", ObjPurchase.IPAddress);
            





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


        public PurchOrgData GetPurchOrgData(int intMasterHeaderId)
        {
            PurchOrgData ObjPurchase = new PurchOrgData();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Vendor_PurchOrgData_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", intMasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //ObjPurchase.Mat_Accounting2_Id = Convert.ToInt32(dt.Rows[0]["Mat_Accounting2_Id"].ToString());
                        //ObjPurchase.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        //ObjPurchase.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        //ObjPurchase.Relevant = dt.Rows[0]["Relevant"].ToString().ToLower() == "true" ? 1 : 0;
                        //ObjPurchase.Pool_No_LIFO_Valuation = dt.Rows[0]["Pool_No_LIFO_Valuation"].ToString();

                        ObjPurchase.Vendor_PurchOrgData_id = Convert.ToInt32(dt.Rows[0]["Vendor_PurchOrgData"].ToString());
                        ObjPurchase.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjPurchase.PurchaseOrder_Currency = dt.Rows[0]["PurchaseOrder_Currency"].ToString();
                        ObjPurchase.TermsPayment_Key = dt.Rows[0]["TermsPayment_Key"].ToString();
                        ObjPurchase.IncotermsPart1 = dt.Rows[0]["IncotermsPart1"].ToString();
                        ObjPurchase.IncotermsPart2 = dt.Rows[0]["IncotermsPart2"].ToString();
                        ObjPurchase.MinimumOrder_batchInput = dt.Rows[0]["MinimumOrder_batchInput"].ToString();
                        ObjPurchase.Responsible_SalesPerson = dt.Rows[0]["Responsible_SalesPerson"].ToString();
                        ObjPurchase.Vendor_TelephoneNumber = dt.Rows[0]["Vendor_TelephoneNumber"].ToString();
                        ObjPurchase.ABC_Indicator = dt.Rows[0]["ABC_Indicator"].ToString() == "" ? "0" :dt.Rows[0]["ABC_Indicator"].ToString();
                        ObjPurchase.PurchasingBlock_Purchasing = dt.Rows[0]["PurchasingBlock_Purchasing"].ToString() == "" ? "0" :  dt.Rows[0]["PurchasingBlock_Purchasing"].ToString();
                        ObjPurchase.Deleteflag_purchasinglevel = dt.Rows[0]["Deleteflag_purchasinglevel"].ToString() == "" ? "0" :  dt.Rows[0]["Deleteflag_purchasinglevel"].ToString();
                        ObjPurchase.IndicatorInvoice_Verification = dt.Rows[0]["IndicatorInvoice_Verification"].ToString() == "" ? "0" :  dt.Rows[0]["IndicatorInvoice_Verification"].ToString();
                        ObjPurchase.OrderAcknowledgment_Requirement = dt.Rows[0]["OrderAcknowledgment_Requirement"].ToString() == "" ? "0" :  dt.Rows[0]["OrderAcknowledgment_Requirement"].ToString();
                        ObjPurchase.GroupCalculation_SchemaVendor = dt.Rows[0]["GroupCalculation_SchemaVendor"].ToString();
                        ObjPurchase.Automatic_Generation = dt.Rows[0]["Automatic_Generation"].ToString() == "" ? "0" :  dt.Rows[0]["Automatic_Generation"].ToString();
                        ObjPurchase.ModeTransport_ForeignTrade = dt.Rows[0]["ModeTransport_ForeignTrade"].ToString();
                        ObjPurchase.CustomsOffice_ForeignTrade = dt.Rows[0]["CustomsOffice_ForeignTrade"].ToString();
                        ObjPurchase.Purchasing_Group = dt.Rows[0]["Purchasing_Group"].ToString();
                        ObjPurchase.Indicator_vendor_accountimng = dt.Rows[0]["Indicator_vendor_accountimng"].ToString() == "" ? "0" :  dt.Rows[0]["Indicator_vendor_accountimng"].ToString();
                        ObjPurchase.PlannedTime_Days_BTCI = dt.Rows[0]["PlannedTime_Days_BTCI"].ToString();
                        ObjPurchase.Shipping_Conditions = dt.Rows[0]["Shipping_Conditions"].ToString();
                        ObjPurchase.Indicator_ServiceBased_Verification = dt.Rows[0]["Indicator_ServiceBased_Verification"].ToString() == "" ? "0" :  dt.Rows[0]["Indicator_ServiceBased_Verification"].ToString();
                        ObjPurchase.StagingTime_Days_BatchInput = dt.Rows[0]["StagingTime_Days_BatchInput"].ToString();
                        ObjPurchase.Category_tax_codes = dt.Rows[0]["Category_tax_codes"].ToString();
                        ObjPurchase.Vendor_Subrange = dt.Rows[0]["Vendor_Subrange"].ToString();
                        ObjPurchase.Language_BatchInputField = dt.Rows[0]["Language_BatchInputField"].ToString();
                        ObjPurchase.Purchasing_Organization = dt.Rows[0]["Purchasing_Organization"].ToString();
                        ObjPurchase.Plant = dt.Rows[0]["Plant"].ToString();
                        ObjPurchase.Partner_Function = dt.Rows[0]["Partner_Function"].ToString();
                        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record = dt.Rows[0]["Number_Of_Business_Master_in_Vendor_Master_Record"].ToString();
                        ObjPurchase.Partner_Function2 = dt.Rows[0]["Partner_Function2"].ToString();
                        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record2 = dt.Rows[0]["Number_Of_Business_Master_in_Vendor_Master_Record2"].ToString();
                        ObjPurchase.Partner_Function3 = dt.Rows[0]["Partner_Function3"].ToString();
                        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record3 = dt.Rows[0]["Number_Of_Business_Master_in_Vendor_Master_Record3"].ToString();
                        ObjPurchase.Partner_Function4 = dt.Rows[0]["Partner_Function4"].ToString();
                        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record4 = dt.Rows[0]["Number_Of_Business_Master_in_Vendor_Master_Record4"].ToString();
                        ObjPurchase.Partner_Function5 = dt.Rows[0]["Partner_Function5"].ToString();
                        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record5 = dt.Rows[0]["Number_Of_Business_Master_in_Vendor_Master_Record5"].ToString();
                        ObjPurchase.Partner_Function6 = dt.Rows[0]["Partner_Function6"].ToString();
                        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record6 = dt.Rows[0]["Number_Of_Business_Master_in_Vendor_Master_Record6"].ToString();
                        ObjPurchase.Partner_counter = dt.Rows[0]["Partner_counter"].ToString();
                        ObjPurchase.Name_Person_who_CreatedObject = dt.Rows[0]["Name_Person_who_CreatedObject"].ToString();
                        ObjPurchase.Date_Which_Record_Created = dt.Rows[0]["Date_Which_Record_Created"].ToString();
                        ObjPurchase.Reference_vendor = dt.Rows[0]["Reference_vendor"].ToString();
                        ObjPurchase.Personnel_Number_BatchInputField = dt.Rows[0]["Personnel_Number_BatchInputField"].ToString() == "" ? "0" :  dt.Rows[0]["Personnel_Number_BatchInputField"].ToString();
                        ObjPurchase.Country_Key = dt.Rows[0]["Country_Key"].ToString();
                        ObjPurchase.SupplyRegion_RegionSupplied = dt.Rows[0]["SupplyRegion_RegionSupplied"].ToString() == "" ? "0" :  dt.Rows[0]["SupplyRegion_RegionSupplied"].ToString();
                        ObjPurchase.AccountNumber_VendorCreditor = dt.Rows[0]["AccountNumber_VendorCreditor"].ToString() == "" ? "0" :  dt.Rows[0]["AccountNumber_VendorCreditor"].ToString();
                        ObjPurchase.Material_Number = dt.Rows[0]["Material_Number"].ToString() == "" ? "0" :  dt.Rows[0]["Material_Number"].ToString();
                        ObjPurchase.Preference_Zone = dt.Rows[0]["Preference_Zone"].ToString();
                    }
                }
                return ObjPurchase;
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