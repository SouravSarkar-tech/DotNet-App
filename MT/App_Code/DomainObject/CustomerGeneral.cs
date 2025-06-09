using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerGeneral
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class CustomerGeneral1 : Base
    {
        public int Cust_General1_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Company_Code { get; set; }
        public string Customer_Acc_Grp { get; set; }
        public string Customer_Category { get; set; }

        public string Title { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Sort_Field { get; set; }
        public string Name_CO { get; set; }
        public string HouseNo_Street { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string Street4 { get; set; }
        public string Street5 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Different_City { get; set; }
        public string Postal_Code { get; set; }
        public string PO_Box { get; set; }
        public string PO_Box_Postal_Code { get; set; }
        public string CountryKey { get; set; }
        public string Region { get; set; }
        public string LanguageAcc { get; set; }
        public string Mobile_Num { get; set; }
        public string Mobile_Num2 { get; set; }
        public string First_Tele_No { get; set; }
        public string Second_Tele_No { get; set; }
        public string Fax_NO { get; set; }
        public string Email_Address { get; set; }
        public string Email_Address2 { get; set; }
        public string Email_Address3 { get; set; }
        public string Transportation_Zone { get; set; }
        public string Tax_Jurisdiction { get; set; }
        public string Acc_No_Vendor { get; set; }
        public string Com_Id_TradingPat { get; set; }
        public string Group_Key { get; set; }
        public string LiableVat { get; set; }
        public string Country_Code { get; set; }
        public string City_Code { get; set; }

        public int IsActive { get; set; }

        //Start Addition By Swati M Date: 12.10.2018
        public string Remarks { get; set; }
        //End Addition By Swati M Date: 12.10.2018
    }

    public class CustomerGeneral2 : Base
    {
        public int Cust_General2_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Industry_Key { get; set; }
        public string Tax_type { get; set; }
        public string Tax_Number_Type { get; set; }
        public string Tax_Number1 { get; set; }
        public string Tax_Number2 { get; set; }
        public string Tax_Number3 { get; set; }
        public string Tax_Number4 { get; set; }
        public string Tax_Number5 { get; set; }
        public string Type_of_Business { get; set; }        
        public string VAT_Reg { get; set; }
        public string ECC_Number { get; set; }
        public string Excise_Registration_No { get; set; }
        public string Excise_Range { get; set; }
        public string Excise_Division { get; set; }
        public string Excise_Commissionerate { get; set; }
        public string Customer_Claasifi { get; set; }
        public string Nielsen_Id { get; set; }
        public string Region_Market { get; set; }
        public string AccNo_Payer { get; set; }
        public int Payer_Allow_Doc { get; set; }
        public int Central_Delete_Flag { get; set; }
        public int Central_Posting_Blk { get; set; }
        public string Central_Order { get; set; }
        public string Central_Delivery { get; set; }
        public string Central_Billing { get; set; }
        public string Legal_sts { get; set; }
        public string Ind_Code1 { get; set; }
        public string Ind_Code2 { get; set; }
        public string Ind_Code3 { get; set; }
        public string Ind_Code4 { get; set; }
        public string Ind_Code5 { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }
        public string Attribute6 { get; set; }
        public string Attribute7 { get; set; }
        public string Attribute8 { get; set; }
        public string Attribute9 { get; set; }
        public string Attribute10 { get; set; }
        //GST Changes
        public string GSTNo { get; set; }
        //GST Changes

        //CUST_8300001962 Start
        public string  RegisterPAN { get; set; }
        public string  RegisterUnderGST { get; set; }
        public string PanReason { get; set; }
        //CUST_8300001962 End

        public int IsActive { get; set; }
    }

    public class CustomerGeneral3 : Base
    {
        public int Cust_General3_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Fiscal_Year_Variant { get; set; }
        public string Reference_Account { get; set; }
        public string PO_Box_city { get; set; }
        public string Hierarchy_assignment { get; set; }
        public string Central_sales { get; set; }
        public string Customer_condition1 { get; set; }
        public string Customer_condition2 { get; set; }
        public string Customer_condition3 { get; set; }
        public string Customer_condition4 { get; set; }
        public string Customer_condition5 { get; set; }
        public string Uniform_Resource { get; set; }
        public string Central_deletion { get; set; }
        public string Unloading_Point { get; set; }
        public string Customer_factory { get; set; }
        public string Contact_person_department { get; set; }
        public string First_name { get; set; }
        public string Country_Key { get; set; }
        public string Mobile_Num { get; set; }
        public string Mobile_Num2 { get; set; }
        public string First_Tele_No { get; set; }
        public string Second_Tele_No { get; set; }
        public string Fax_NO { get; set; }
        public string Email_Address { get; set; }
        public string Email_Address2 { get; set; }
        public string First_name_2 { get; set; }
        public string Country_Key_2 { get; set; }
        public string Mobile_Num_2 { get; set; }
        public string Mobile_Num2_2 { get; set; }
        public string First_Tele_No_2 { get; set; }
        public string Second_Tele_No_2 { get; set; }
        public string Fax_NO_2 { get; set; }
        public string Email_Address_2 { get; set; }
        public string Email_Address2_2 { get; set; }
        public string First_name_3 { get; set; }
        public string Country_Key_3 { get; set; }
        public string Mobile_Num_3 { get; set; }
        public string Mobile_Num2_3 { get; set; }
        public string First_Tele_No_3 { get; set; }
        public string Second_Tele_No_3 { get; set; }
        public string Fax_NO_3 { get; set; }
        public string Email_Address_3 { get; set; }
        public string Email_Address2_3 { get; set; }
        public string Form_address { get; set; }
        public string Contact_person_function { get; set; }
        public string Partner_language { get; set; }
        public string Partner_gender { get; set; }
        public string Marital_Status { get; set; }
        public string Date_Batch { get; set; }
        public string Contact_person_department_Cust { get; set; }
        public string VIP_Partner { get; set; }
        public string Partner_Authority { get; set; }
        public string Notes { get; set; }

        public int IsActive { get; set; }
    }
}