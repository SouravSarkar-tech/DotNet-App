using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for VendorGeneral
/// </summary>
namespace Accenture.MWT.DomainObject
{
   public class VendorGeneral1 : Base
    {
        public int Vendor_General1_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Company_Code { get; set; }
        public string Vendor_Group { get; set; }
        public string Purchase_Org { get; set; }
        public string Vendor_Desc { get; set; }
        public string Vendor_Category { get; set; }
       
        public string Title { get; set; }
        public string Memo { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Sort_Field { get; set; }
        public string HouseNo_Street { get; set; }
        public string Street4 { get; set; }
        public string Street5 { get; set; }
        public string PO_Box { get; set; }
        public string City { get; set; }
        public string Postal_Code { get; set; }
        public string District { get; set; }
        public string PO_Box_Postal_Code { get; set; }
        public string CountryKey { get; set; }
        public string Region { get; set; }
        public string LanguageAcc { get; set; }
        public string Mobile_Num { get; set; }
        public string Mobile_Num2 { get; set; }
        public string First_Tele_No { get; set; }
        public string Fax_NO { get; set; }
        public string Second_Tele_No { get; set; }  
        public string Com_Id_TradingPat { get; set; }
        public string Group_Key { get; set; }
        public string Telex_number { get; set; }
        public string Teletex_number { get; set; }
        public string Customer_Number { get; set; }
        public string Email_Address { get; set; }
        public string Email_Address2 { get; set; }
        public string Email_Address3 { get; set; }
        public string Remarks { get; set; }
        //GST Changes Start
        public string SupplyPlace { get; set; }
        public string ImpVendor { get; set; }
        //GST Changes End
        public int IsActive { get; set; }

        public string Autorization_Gr { get; set; }
    }
   public class VendorGeneral2 : Base
   {

       public int Vendor_General2_Id { get; set; }
       public int Master_Header_Id { get; set; }
       public string Customer_Code { get; set; }
       public string Company_Code { get; set; }
       public string Vendor_Group { get; set; }
       public string Purchase_Org { get; set; }
       public string Industry_key { get; set; }
       public string VAT_Registration_Number { get; set; }
       public string PlaceBirth_WithholdingTax { get; set; }
       public string DateBatch_Input { get; set; }
       public string KeySex_PersonWithholding_Tax { get; set; }
       public string Tax_Jurisdiction { get; set; }
       public string Plant { get; set; }
       public string Transportation_Zone_Goods { get; set; }
       public string Service_AgentProcedure_Group { get; set; }
       public string Tax_Type { get; set; }
       public string Tax_Number_Type { get; set; }
       public string Tax_Number1 { get; set; }
       public string Tax_Number2 { get; set; }
       public string Tax_Numbe_3 { get; set; }
       public string Tax_Numbe_4 { get; set; }
       public string Type_of_Business { get; set; }
       public string Tax_Split { get; set; }
       public string External_Manufacturer_CodeNumber { get; set; }
       public string Name_Representative { get; set; }
       public string Type_Industry { get; set; }
       public string Central_Deletion_MasterRecord { get; set; }
       public string DateBatch_Input2 { get; set; }
       public string VendorIndicator_Relevant { get; set; }
       public string Name_1 { get; set; }
       public string Name_2 { get; set; }
       public string Name_3 { get; set; }
       public string First_Name { get; set; }
       public string Title { get; set; }
       public string FactoryCalendar_key { get; set; }
       public string Transportation_Chain { get; set; }
       public string StagingTime_Days_BatchInput { get; set; }
       public string CrossDocking_Relevant_CollectiveNumbering { get; set; }
       public string Scheduling_Procedure { get; set; }
       public string Tax_Number_5 { get; set; }
       public string ECC_Number { get; set; }
       public string Excise_Registration_No { get; set; }
       public string Excise_Range { get; set; }
       public string Excise_Division { get; set; }
       public string Excise_Commissionerate { get; set; }
       //GST changes
       public string GSTNo { get; set; }
       public string VendorClass { get; set; }
       //GST changes
       public int IsActive { get; set; }

       public string Autorization_Gr { get; set; }




   }

}