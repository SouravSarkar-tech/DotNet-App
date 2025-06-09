using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendCompanyCode
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class VendCompanyCode1 : Base
    {
        public int Vendor_Company_Code_Data1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Recon_Acc { get; set; }
        public string Key_Sort_Assign_No { get; set; }
        public string Planning_Group { get; set; }
        public string HO_Acc_No { get; set; }
        public string Authorization_grp { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public string Term_Payment_Key { get; set; }
        public string Tolerance_Grp_GL { get; set; }
        public string Probable_Time_Cheque_Paid { get; set; }
        public string Is_Double_Invoice { get; set; }
        public string Payment_Method { get; set; }
        public string Block_Key_Payment { get; set; }
        public string Acc_No_Alt_Payee { get; set; }
        public string Short_Key_House_Bank { get; set; }
        public string Is_Pay_All_Item_Seperately { get; set; }
        public string Bill_Exchange_Limit { get; set; }
        public string Is_Clearing_Cust_Vend { get; set; }
        public string Is_Master_Record_Deleted { get; set; }
        public string Is_Posting_Block_Company_Code { get; set; }
        public string Previous_Master_No { get; set; }
        public string Key_Payment_Grp { get; set; }
        public string Payment_Method_Supp { get; set; }
        public string Is_Send_Payment_Advices_EDI { get; set; }
        public string Release_Approval_Grp { get; set; }
        public string Personnel_No { get; set; }
        public string Tolerance_Grp { get; set; }
        public string Internet_Add_partner { get; set; }
        public string Payment_Term_Key_Credit_Meno { get; set; }
        public string Is_Periodic_Acc_Stmt { get; set; }
        public string Certi_Date { get; set; }
        public string Is_Block_Master_Record_Deletion { get; set; }
        public string Is_Prepayment_Relevant { get; set; }
        
        public int IsActive { get; set; }
    }


    public class VendCompanyCode2 : Base
    {
        public int Vendor_Company_Code_Data2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Bank_Country_key { get; set; }
        public string Bank_key { get; set; }
        public string Account_Holder_Name { get; set; }
        public string Bank_Acc_No { get; set; }
        public string Bank_Control_Key { get; set; }
        public string Partner_Bank_Type { get; set; }
        public string Bank_Name { get; set; }
        public string House_No_Street { get; set; }
        public string Bank_No { get; set; }
        public int Region_Id { get; set; }
        public string Account_Number_Alternative_Payee { get; set; }
        public string KOV_Date { get; set; }
        public string KOB_Issue_Date { get; set; }
        public string InterN_Bank_Acc_No { get; set; }
        public string Valid_From_Date { get; set; }
        public string GM_Valid_Date { get; set; }
        public string M_Date { get; set; }
        public string Indicator_WHT_Type { get; set; }
        public string Is_Subject_WHT { get; set; }
        public string Vend_Receipt_Type { get; set; }
        public string Auth_Exemption_WHT { get; set; }
        public string WHT_Country_Key { get; set; }
        public string Type_Recipient { get; set; }
        public string WithHolding_Tax_Code { get; set; }
        public string WHT_Exempt_Cert_No { get; set; }
        public string Date3 { get; set; }
        public string WHT_Identification_No { get; set; }
        public string WHT_Code { get; set; }
        public string Exemption_Cert_No { get; set; }
        public string Exemption_Rate_Batch_Inp { get; set; }
        public string WT_Exempt_From_Date { get; set; }
        public string WT_Exempt_To_Date { get; set; }
        public string Exemption_Reason { get; set; }

        public string Indicator_WHT_Type2 { get; set; }
        public string WithHolding_Tax_Code2 { get; set; }
        public string Type_Recipient2 { get; set; }
        public string WHT_Exempt_Cert_No2 { get; set; }
        public string Exemption_Rate_Batch_Inp2 { get; set; }
        public string Exemption_Reason2 { get; set; }
        public string WT_Exempt_From_Date2 { get; set; }
        public string WT_Exempt_To_Date2 { get; set; }
        public string WHT_Identification_No2 { get; set; }

        public string Indicator_WHT_Type3 { get; set; }
        public string WithHolding_Tax_Code3 { get; set; }
        public string Type_Recipient3 { get; set; }
        public string WHT_Exempt_Cert_No3 { get; set; }
        public string Exemption_Rate_Batch_Inp3 { get; set; }
        public string WT_Exempt_From_Date3 { get; set; }
        public string WT_Exempt_To_Date3 { get; set; }

        public string Indicator_WHT_Type4 { get; set; }
        public string WithHolding_Tax_Code4 { get; set; }
        public string Type_Recipient4 { get; set; }
        public string WHT_Exempt_Cert_No4 { get; set; }
        public string Exemption_Rate_Batch_Inp4 { get; set; }
        public string WT_Exempt_From_Date4 { get; set; }
        public string WT_Exempt_To_Date4 { get; set; }

        public string Indicator_WHT_Type5 { get; set; }
        public string WithHolding_Tax_Code5 { get; set; }
        public string Type_Recipient5 { get; set; }
        public string WHT_Exempt_Cert_No5 { get; set; }
        public string Exemption_Rate_Batch_Inp5 { get; set; }
        public string WT_Exempt_From_Date5 { get; set; }
        public string WT_Exempt_To_Date5 { get; set; }

        public string Indicator_WHT_Type6 { get; set; }
        public string WithHolding_Tax_Code6 { get; set; }
        public string Type_Recipient6 { get; set; }
        public string WHT_Exempt_Cert_No6 { get; set; }
        public string Exemption_Rate_Batch_Inp6 { get; set; }
        public string WT_Exempt_From_Date6 { get; set; }
        public string WT_Exempt_To_Date6 { get; set; }

        public int IsActive { get; set; }

        //Added by Swati 03.01.2019
        public string BankDetailsReq { get; set; }
        public string ReasonNonBankDet { get; set; }
        //End
    }
}