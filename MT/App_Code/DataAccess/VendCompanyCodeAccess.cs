using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for VendCompanyCodeAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class VendCompanyCodeAccess
    {

        Utility ObjUtil = new Utility();
        public VendCompanyCodeAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(VendCompanyCode1 ObjVComp)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_Company_Code_Data1";
            int result = 0;


            hashPara.Add("@Vendor_Company_Code_Data1_Id", ObjVComp.Vendor_Company_Code_Data1_Id);
            hashPara.Add("@Master_Header_Id", ObjVComp.Master_Header_Id);
            hashPara.Add("@Recon_Acc", ObjVComp.Recon_Acc);
            hashPara.Add("@Key_Sort_Assign_No", ObjVComp.Key_Sort_Assign_No);
            hashPara.Add("@Planning_Group", ObjVComp.Planning_Group);
            hashPara.Add("@HO_Acc_No", ObjVComp.HO_Acc_No);
            hashPara.Add("@Authorization_grp", ObjVComp.Authorization_grp);
            hashPara.Add("@Date1", ObjVComp.Date1);
            hashPara.Add("@Date2", ObjVComp.Date2);
            hashPara.Add("@Term_Payment_Key", ObjVComp.Term_Payment_Key);
            hashPara.Add("@Tolerance_Grp_GL", ObjVComp.Tolerance_Grp_GL);
            hashPara.Add("@Probable_Time_Cheque_Paid", ObjVComp.Probable_Time_Cheque_Paid);
            hashPara.Add("@Is_Double_Invoice", ObjVComp.Is_Double_Invoice);
            hashPara.Add("@Payment_Method", ObjVComp.Payment_Method);
            hashPara.Add("@Block_Key_Payment", ObjVComp.Block_Key_Payment);
            hashPara.Add("@Acc_No_Alt_Payee", ObjVComp.Acc_No_Alt_Payee);
            hashPara.Add("@Short_Key_House_Bank", ObjVComp.Short_Key_House_Bank);
            hashPara.Add("@Is_Pay_All_Item_Seperately", ObjVComp.Is_Pay_All_Item_Seperately);
            hashPara.Add("@Bill_Exchange_Limit", ObjVComp.Bill_Exchange_Limit);
            hashPara.Add("@Is_Clearing_Cust_Vend", ObjVComp.Is_Clearing_Cust_Vend);
            hashPara.Add("@Is_Master_Record_Deleted", ObjVComp.Is_Master_Record_Deleted);
            hashPara.Add("@Is_Posting_Block_Company_Code", ObjVComp.Is_Posting_Block_Company_Code);
            hashPara.Add("@Previous_Master_No", ObjVComp.Previous_Master_No);
            hashPara.Add("@Key_Payment_Grp", ObjVComp.Key_Payment_Grp);
            hashPara.Add("@Payment_Method_Supp", ObjVComp.Payment_Method_Supp);
            hashPara.Add("@Is_Send_Payment_Advices_EDI", ObjVComp.Is_Send_Payment_Advices_EDI);
            hashPara.Add("@Release_Approval_Grp", ObjVComp.Release_Approval_Grp);
            hashPara.Add("@Personnel_No", ObjVComp.Personnel_No);
            hashPara.Add("@Tolerance_Grp", ObjVComp.Tolerance_Grp);
            hashPara.Add("@Internet_Add_partner", ObjVComp.Internet_Add_partner);
            hashPara.Add("@Payment_Term_Key_Credit_Meno", ObjVComp.Payment_Term_Key_Credit_Meno);
            hashPara.Add("@Is_Periodic_Acc_Stmt", ObjVComp.Is_Periodic_Acc_Stmt);
            hashPara.Add("@Certi_Date", ObjVComp.Certi_Date);
            hashPara.Add("@Is_Block_Master_Record_Deletion", ObjVComp.Is_Block_Master_Record_Deletion);
            hashPara.Add("@Is_Prepayment_Relevant", ObjVComp.Is_Prepayment_Relevant);
            hashPara.Add("@IsActive", ObjVComp.IsActive);
            hashPara.Add("@UserId", ObjVComp.UserId);
            hashPara.Add("@UserIp", ObjVComp.IPAddress);

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

        public int Save(VendCompanyCode2 ObjVComp)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_Company_Code_Data2";
            int result = 0;


            hashPara.Add("@Vendor_Company_Code_Data2_Id", ObjVComp.Vendor_Company_Code_Data2_Id);
            hashPara.Add("@Master_Header_Id", ObjVComp.Master_Header_Id);
            hashPara.Add("@Bank_Country_key", ObjVComp.Bank_Country_key);
            hashPara.Add("@Bank_key", ObjVComp.Bank_key);
            hashPara.Add("@Account_Holder_Name", ObjVComp.Account_Holder_Name);
            hashPara.Add("@Bank_Acc_No", ObjVComp.Bank_Acc_No);
            hashPara.Add("@Bank_Control_Key", ObjVComp.Bank_Control_Key);
            hashPara.Add("@Partner_Bank_Type", ObjVComp.Partner_Bank_Type);
            hashPara.Add("@Bank_Name", ObjVComp.Bank_Name);
            hashPara.Add("@House_No_Street", ObjVComp.House_No_Street);
            hashPara.Add("@Bank_No", ObjVComp.Bank_No);
            hashPara.Add("@Region_Id", ObjVComp.Region_Id);
            hashPara.Add("@Account_Number_Alternative_Payee", ObjVComp.Account_Number_Alternative_Payee);
            hashPara.Add("@KOV_Date", ObjVComp.KOV_Date);
            hashPara.Add("@KOB_Issue_Date", ObjVComp.KOB_Issue_Date);
            hashPara.Add("@InterN_Bank_Acc_No", ObjVComp.InterN_Bank_Acc_No);
            hashPara.Add("@Valid_From_Date", ObjVComp.Valid_From_Date);
            hashPara.Add("@GM_Valid_Date", ObjVComp.GM_Valid_Date);
            hashPara.Add("@M_Date", ObjVComp.M_Date);
            hashPara.Add("@Indicator_WHT_Type", ObjVComp.Indicator_WHT_Type);
            hashPara.Add("@Is_Subject_WHT", ObjVComp.Is_Subject_WHT);
            hashPara.Add("@Vend_Receipt_Type", ObjVComp.Vend_Receipt_Type);
            hashPara.Add("@Auth_Exemption_WHT", ObjVComp.Auth_Exemption_WHT);
            hashPara.Add("@WHT_Country_Key", ObjVComp.WHT_Country_Key);
            hashPara.Add("@Type_Recipient", ObjVComp.Type_Recipient);
            hashPara.Add("@WithHolding_Tax_Code", ObjVComp.WithHolding_Tax_Code);
            hashPara.Add("@WHT_Exempt_Cert_No", ObjVComp.WHT_Exempt_Cert_No);
            hashPara.Add("@Date3", ObjVComp.Date3);
            hashPara.Add("@WHT_Identification_No", ObjVComp.WHT_Identification_No);
            hashPara.Add("@WHT_Code", ObjVComp.WHT_Code);
            hashPara.Add("@Exemption_Cert_No", ObjVComp.Exemption_Cert_No);
            hashPara.Add("@Exemption_Rate_Batch_Inp", ObjVComp.Exemption_Rate_Batch_Inp);
            hashPara.Add("@WT_Exempt_From_Date", ObjVComp.WT_Exempt_From_Date);
            hashPara.Add("@WT_Exempt_To_Date", ObjVComp.WT_Exempt_To_Date);
            hashPara.Add("@Exemption_Reason", ObjVComp.Exemption_Reason);

            hashPara.Add("@Indicator_WHT_Type2", ObjVComp.Indicator_WHT_Type2);
            hashPara.Add("@WithHolding_Tax_Code2", ObjVComp.WithHolding_Tax_Code2);
            hashPara.Add("@Type_Recipient2", ObjVComp.Type_Recipient2);
            hashPara.Add("@WHT_Exempt_Cert_No2", ObjVComp.WHT_Exempt_Cert_No2);
            hashPara.Add("@Exemption_Rate_Batch_Inp2", ObjVComp.Exemption_Rate_Batch_Inp2);
            hashPara.Add("@Exemption_Reason2", ObjVComp.Exemption_Reason2);
            hashPara.Add("@WT_Exempt_From_Date2", ObjVComp.WT_Exempt_From_Date2);
            hashPara.Add("@WT_Exempt_To_Date2", ObjVComp.WT_Exempt_To_Date2);
            hashPara.Add("@WHT_Identification_No2", ObjVComp.WHT_Identification_No2);

            hashPara.Add("@Indicator_WHT_Type3", ObjVComp.Indicator_WHT_Type3);
            hashPara.Add("@WithHolding_Tax_Code3", ObjVComp.WithHolding_Tax_Code3);
            hashPara.Add("@Type_Recipient3", ObjVComp.Type_Recipient3);
            hashPara.Add("@WHT_Exempt_Cert_No3", ObjVComp.WHT_Exempt_Cert_No3);
            hashPara.Add("@Exemption_Rate_Batch_Inp3", ObjVComp.Exemption_Rate_Batch_Inp3);
            hashPara.Add("@WT_Exempt_From_Date3", ObjVComp.WT_Exempt_From_Date3);
            hashPara.Add("@WT_Exempt_To_Date3", ObjVComp.WT_Exempt_To_Date3);

            hashPara.Add("@Indicator_WHT_Type4", ObjVComp.Indicator_WHT_Type4);
            hashPara.Add("@WithHolding_Tax_Code4", ObjVComp.WithHolding_Tax_Code4);
            hashPara.Add("@Type_Recipient4", ObjVComp.Type_Recipient4);
            hashPara.Add("@WHT_Exempt_Cert_No4", ObjVComp.WHT_Exempt_Cert_No4);
            hashPara.Add("@Exemption_Rate_Batch_Inp4", ObjVComp.Exemption_Rate_Batch_Inp4);
            hashPara.Add("@WT_Exempt_From_Date4", ObjVComp.WT_Exempt_From_Date4);
            hashPara.Add("@WT_Exempt_To_Date4", ObjVComp.WT_Exempt_To_Date4);

            hashPara.Add("@Indicator_WHT_Type5", ObjVComp.Indicator_WHT_Type5);
            hashPara.Add("@WithHolding_Tax_Code5", ObjVComp.WithHolding_Tax_Code5);
            hashPara.Add("@Type_Recipient5", ObjVComp.Type_Recipient5);
            hashPara.Add("@WHT_Exempt_Cert_No5", ObjVComp.WHT_Exempt_Cert_No5);
            hashPara.Add("@Exemption_Rate_Batch_Inp5", ObjVComp.Exemption_Rate_Batch_Inp5);
            hashPara.Add("@WT_Exempt_From_Date5", ObjVComp.WT_Exempt_From_Date5);
            hashPara.Add("@WT_Exempt_To_Date5", ObjVComp.WT_Exempt_To_Date5);

            hashPara.Add("@Indicator_WHT_Type6", ObjVComp.Indicator_WHT_Type6);
            hashPara.Add("@WithHolding_Tax_Code6", ObjVComp.WithHolding_Tax_Code6);
            hashPara.Add("@Type_Recipient6", ObjVComp.Type_Recipient6);
            hashPara.Add("@WHT_Exempt_Cert_No6", ObjVComp.WHT_Exempt_Cert_No6);
            hashPara.Add("@Exemption_Rate_Batch_Inp6", ObjVComp.Exemption_Rate_Batch_Inp6);
            hashPara.Add("@WT_Exempt_From_Date6", ObjVComp.WT_Exempt_From_Date6);
            hashPara.Add("@WT_Exempt_To_Date6", ObjVComp.WT_Exempt_To_Date6);

            hashPara.Add("@IsActive", ObjVComp.IsActive);
            hashPara.Add("@UserId", ObjVComp.UserId);
            hashPara.Add("@UserIp", ObjVComp.IPAddress);

            //Code added by Swati on 03.01.2019
            hashPara.Add("@BankDetailsReq", ObjVComp.BankDetailsReq);
            hashPara.Add("@ReasonNonBankDet", ObjVComp.ReasonNonBankDet);
            //End

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


        public VendCompanyCode1 GetVendCompanyCode1(int Master_Header_Id)
        {
            VendCompanyCode1 ObjVComp = new VendCompanyCode1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Vendor_Company_Code_Data1_By_MasterHeaderId";
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
                        ObjVComp.Vendor_Company_Code_Data1_Id = Convert.ToInt32(dt.Rows[0]["Vendor_Company_Code_Data1_Id"].ToString());
                        ObjVComp.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjVComp.Recon_Acc = dt.Rows[0]["Recon_Acc"].ToString();
                        ObjVComp.Key_Sort_Assign_No = dt.Rows[0]["Key_Sort_Assign_No"].ToString();
                        ObjVComp.Planning_Group = dt.Rows[0]["Planning_Group"].ToString();
                        ObjVComp.HO_Acc_No = dt.Rows[0]["HO_Acc_No"].ToString();
                        ObjVComp.Authorization_grp = dt.Rows[0]["Authorization_grp"].ToString();
                        ObjVComp.Date1 = dt.Rows[0]["Date1"].ToString();
                        ObjVComp.Date2 = dt.Rows[0]["Date2"].ToString();
                        ObjVComp.Term_Payment_Key = dt.Rows[0]["Term_Payment_Key"].ToString();
                        ObjVComp.Tolerance_Grp_GL = dt.Rows[0]["Tolerance_Grp_GL"].ToString();
                        ObjVComp.Probable_Time_Cheque_Paid = dt.Rows[0]["Probable_Time_Cheque_Paid"].ToString();
                        ObjVComp.Is_Double_Invoice = dt.Rows[0]["Is_Double_Invoice"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Payment_Method = dt.Rows[0]["Payment_Method"].ToString();
                        ObjVComp.Block_Key_Payment = dt.Rows[0]["Block_Key_Payment"].ToString();
                        ObjVComp.Acc_No_Alt_Payee = dt.Rows[0]["Acc_No_Alt_Payee"].ToString();
                        ObjVComp.Short_Key_House_Bank = dt.Rows[0]["Short_Key_House_Bank"].ToString();
                        ObjVComp.Is_Pay_All_Item_Seperately = dt.Rows[0]["Is_Pay_All_Item_Seperately"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Bill_Exchange_Limit = dt.Rows[0]["Bill_Exchange_Limit"].ToString();
                        ObjVComp.Is_Clearing_Cust_Vend = dt.Rows[0]["Is_Clearing_Cust_Vend"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Is_Master_Record_Deleted = dt.Rows[0]["Is_Master_Record_Deleted"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Is_Posting_Block_Company_Code = dt.Rows[0]["Is_Posting_Block_Company_Code"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Previous_Master_No = dt.Rows[0]["Previous_Master_No"].ToString();
                        ObjVComp.Key_Payment_Grp = dt.Rows[0]["Key_Payment_Grp"].ToString();
                        ObjVComp.Payment_Method_Supp = dt.Rows[0]["Payment_Method_Supp"].ToString();
                        ObjVComp.Is_Send_Payment_Advices_EDI = dt.Rows[0]["Is_Send_Payment_Advices_EDI"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Release_Approval_Grp = dt.Rows[0]["Release_Approval_Grp"].ToString();
                        ObjVComp.Personnel_No = dt.Rows[0]["Personnel_No"].ToString();
                        ObjVComp.Tolerance_Grp = dt.Rows[0]["Tolerance_Grp"].ToString();
                        ObjVComp.Internet_Add_partner = dt.Rows[0]["Internet_Add_partner"].ToString();
                        ObjVComp.Payment_Term_Key_Credit_Meno = dt.Rows[0]["Payment_Term_Key_Credit_Meno"].ToString();
                        ObjVComp.Is_Periodic_Acc_Stmt = dt.Rows[0]["Is_Periodic_Acc_Stmt"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Certi_Date = dt.Rows[0]["Certi_Date"].ToString();
                        ObjVComp.Is_Block_Master_Record_Deletion = dt.Rows[0]["Is_Block_Master_Record_Deletion"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjVComp.Is_Prepayment_Relevant = dt.Rows[0]["Is_Prepayment_Relevant"].ToString().ToLower() == "true" ? "1" : "0";
                    }
                }
                return ObjVComp;
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

        public VendCompanyCode2 GetVendCompanyCode2(int Master_Header_Id)
        {
            VendCompanyCode2 ObjVComp = new VendCompanyCode2();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Vendor_Company_Code_Data2_By_MasterHeaderId";
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
                        ObjVComp.Vendor_Company_Code_Data2_Id = Convert.ToInt32( dt.Rows[0]["Vendor_Company_Code_Data2_Id"].ToString());
                        ObjVComp.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjVComp.Bank_Country_key = dt.Rows[0]["Bank_Country_key"].ToString();
                        ObjVComp.Bank_key = dt.Rows[0]["Bank_key"].ToString();
                        ObjVComp.Bank_Acc_No = dt.Rows[0]["Bank_Acc_No"].ToString();
                        ObjVComp.Account_Holder_Name = dt.Rows[0]["Account_Holder_Name"].ToString();
                        ObjVComp.Bank_Control_Key = dt.Rows[0]["Bank_Control_Key"].ToString();
                        ObjVComp.Partner_Bank_Type = dt.Rows[0]["Partner_Bank_Type"].ToString();
                        ObjVComp.Bank_Name = dt.Rows[0]["Bank_Name"].ToString();
                        ObjVComp.House_No_Street = dt.Rows[0]["House_No_Street"].ToString();
                        ObjVComp.Bank_No = dt.Rows[0]["Bank_No"].ToString();
                        ObjVComp.Region_Id = Convert.ToInt32(dt.Rows[0]["Region_Id"].ToString() == "" ? "0" : dt.Rows[0]["Region_Id"].ToString());
                        ObjVComp.Account_Number_Alternative_Payee = dt.Rows[0]["Account_Number_Alternative_Payee"].ToString();
                        ObjVComp.KOV_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["KOV_Date"].ToString());
                        ObjVComp.KOB_Issue_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["KOB_Issue_Date"].ToString());
                        ObjVComp.InterN_Bank_Acc_No = dt.Rows[0]["InterN_Bank_Acc_No"].ToString();
                        ObjVComp.Valid_From_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Valid_From_Date"].ToString());
                        ObjVComp.GM_Valid_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["GM_Valid_Date"].ToString());
                        ObjVComp.M_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["M_Date"].ToString());
                        ObjVComp.Indicator_WHT_Type = dt.Rows[0]["Indicator_WHT_Type"].ToString();
                        ObjVComp.Is_Subject_WHT = dt.Rows[0]["Is_Subject_WHT"].ToString();
                        ObjVComp.Vend_Receipt_Type = dt.Rows[0]["Vend_Receipt_Type"].ToString();
                        ObjVComp.Auth_Exemption_WHT = dt.Rows[0]["Auth_Exemption_WHT"].ToString();
                        ObjVComp.WHT_Country_Key = dt.Rows[0]["WHT_Country_Key"].ToString();
                        ObjVComp.Type_Recipient = dt.Rows[0]["Type_Recipient"].ToString();
                        ObjVComp.WithHolding_Tax_Code = dt.Rows[0]["WithHolding_Tax_Code"].ToString();
                        ObjVComp.WHT_Exempt_Cert_No = dt.Rows[0]["WHT_Exempt_Cert_No"].ToString();
                        ObjVComp.Date3 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date3"].ToString());
                        ObjVComp.WHT_Identification_No = dt.Rows[0]["WHT_Identification_No"].ToString();
                        ObjVComp.WHT_Code = dt.Rows[0]["WHT_Code"].ToString();
                        ObjVComp.Exemption_Cert_No = dt.Rows[0]["Exemption_Cert_No"].ToString();
                        ObjVComp.Exemption_Rate_Batch_Inp = dt.Rows[0]["Exemption_Rate_Batch_Inp"].ToString();
                        ObjVComp.WT_Exempt_From_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_From_Date"].ToString());
                        ObjVComp.WT_Exempt_To_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_To_Date"].ToString());
                        ObjVComp.Exemption_Reason = dt.Rows[0]["Exemption_Reason"].ToString();

                        ObjVComp.Indicator_WHT_Type2 = dt.Rows[0]["Indicator_WHT_Type2"].ToString();
                        ObjVComp.WithHolding_Tax_Code2 = dt.Rows[0]["WithHolding_Tax_Code2"].ToString();
                        ObjVComp.Type_Recipient2 = dt.Rows[0]["Type_Recipient2"].ToString();
                        ObjVComp.WHT_Exempt_Cert_No2 = dt.Rows[0]["WHT_Exempt_Cert_No2"].ToString();
                        ObjVComp.Exemption_Rate_Batch_Inp2 = dt.Rows[0]["Exemption_Rate_Batch_Inp2"].ToString();
                        ObjVComp.Exemption_Reason2 = dt.Rows[0]["Exemption_Reason2"].ToString();
                        ObjVComp.WT_Exempt_From_Date2 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_From_Date2"].ToString());
                        ObjVComp.WT_Exempt_To_Date2 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_To_Date2"].ToString());
                        ObjVComp.WHT_Identification_No2 = dt.Rows[0]["WHT_Identification_No2"].ToString();

                        ObjVComp.Indicator_WHT_Type3 = dt.Rows[0]["Indicator_WHT_Type3"].ToString();
                        ObjVComp.WithHolding_Tax_Code3 = dt.Rows[0]["WithHolding_Tax_Code3"].ToString();
                        ObjVComp.Type_Recipient3 = dt.Rows[0]["Type_Recipient3"].ToString();
                        ObjVComp.WHT_Exempt_Cert_No3 = dt.Rows[0]["WHT_Exempt_Cert_No3"].ToString();
                        ObjVComp.Exemption_Rate_Batch_Inp3 = dt.Rows[0]["Exemption_Rate_Batch_Inp3"].ToString();
                        ObjVComp.WT_Exempt_From_Date3 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_From_Date3"].ToString());
                        ObjVComp.WT_Exempt_To_Date3 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_To_Date3"].ToString());

                        ObjVComp.Indicator_WHT_Type4 = dt.Rows[0]["Indicator_WHT_Type4"].ToString();
                        ObjVComp.WithHolding_Tax_Code4 = dt.Rows[0]["WithHolding_Tax_Code4"].ToString();
                        ObjVComp.Type_Recipient4 = dt.Rows[0]["Type_Recipient4"].ToString();
                        ObjVComp.WHT_Exempt_Cert_No4 = dt.Rows[0]["WHT_Exempt_Cert_No4"].ToString();
                        ObjVComp.Exemption_Rate_Batch_Inp4 = dt.Rows[0]["Exemption_Rate_Batch_Inp4"].ToString();
                        ObjVComp.WT_Exempt_From_Date4 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_From_Date4"].ToString());
                        ObjVComp.WT_Exempt_To_Date4 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_To_Date4"].ToString());

                        ObjVComp.Indicator_WHT_Type5 = dt.Rows[0]["Indicator_WHT_Type5"].ToString();
                        ObjVComp.WithHolding_Tax_Code5 = dt.Rows[0]["WithHolding_Tax_Code5"].ToString();
                        ObjVComp.Type_Recipient5 = dt.Rows[0]["Type_Recipient5"].ToString();
                        ObjVComp.WHT_Exempt_Cert_No5 = dt.Rows[0]["WHT_Exempt_Cert_No5"].ToString();
                        ObjVComp.Exemption_Rate_Batch_Inp5 = dt.Rows[0]["Exemption_Rate_Batch_Inp5"].ToString();
                        ObjVComp.WT_Exempt_From_Date5 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_From_Date5"].ToString());
                        ObjVComp.WT_Exempt_To_Date5 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_To_Date5"].ToString());

                        ObjVComp.Indicator_WHT_Type6 = dt.Rows[0]["Indicator_WHT_Type6"].ToString();
                        ObjVComp.WithHolding_Tax_Code6 = dt.Rows[0]["WithHolding_Tax_Code6"].ToString();
                        ObjVComp.Type_Recipient6 = dt.Rows[0]["Type_Recipient6"].ToString();
                        ObjVComp.WHT_Exempt_Cert_No6 = dt.Rows[0]["WHT_Exempt_Cert_No6"].ToString();
                        ObjVComp.Exemption_Rate_Batch_Inp6 = dt.Rows[0]["Exemption_Rate_Batch_Inp6"].ToString();
                        ObjVComp.WT_Exempt_From_Date6 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_From_Date6"].ToString());
                        ObjVComp.WT_Exempt_To_Date6 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["WT_Exempt_To_Date6"].ToString());
                        //Add Code by Swati on 03.01.2019
                        ObjVComp.BankDetailsReq = dt.Rows[0]["BankDetailsReq"].ToString();
                        ObjVComp.ReasonNonBankDet = dt.Rows[0]["ReasonNonBankDet"].ToString();
                        //End
                    }
                }
                return ObjVComp;
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