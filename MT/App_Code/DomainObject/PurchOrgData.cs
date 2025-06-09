using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;



namespace Accenture.MWTT.DomainObject
{
    public class PurchOrgData : Base
    {
        public PurchOrgData()
        {
            //
            // TODO: Add constructor logic here
            //
        }
                    public int Master_Header_Id { get; set; }
                    public int Vendor_PurchOrgData_id { get; set; }
                    public string PurchaseOrder_Currency { get; set; }
                    public string TermsPayment_Key { get; set; }
                    public string IncotermsPart1 { get; set; }
                    public string IncotermsPart2 { get; set; }
                    public string MinimumOrder_batchInput { get; set; }
                    public string Responsible_SalesPerson { get; set; }
                    public string Vendor_TelephoneNumber { get; set; }
                    public string ABC_Indicator { get; set; }
                    public string PurchasingBlock_Purchasing { get; set; }
                    public string Deleteflag_purchasinglevel { get; set; }
                    public string IndicatorInvoice_Verification { get; set; }
                    public string OrderAcknowledgment_Requirement { get; set; }
                    public string GroupCalculation_SchemaVendor { get; set; }
                    public string Automatic_Generation { get; set; }
                    public string ModeTransport_ForeignTrade { get; set; }
                    public string CustomsOffice_ForeignTrade { get; set; }
                    public string Purchasing_Group { get; set; }
                    public string Indicator_vendor_accountimng { get; set; }
                    public string PlannedTime_Days_BTCI { get; set; }
                    public string Shipping_Conditions { get; set; }
                    public string Indicator_ServiceBased_Verification { get; set; }
                    public string StagingTime_Days_BatchInput { get; set; }
                    public string Category_tax_codes { get; set; }
                    public string Vendor_Subrange { get; set; }
                    public string Language_BatchInputField { get; set; }
                    public string Purchasing_Organization { get; set; }
                    public string Plant { get; set; }
                    public string Partner_Function { get; set; }
                    public string Number_Of_Business_Master_in_Vendor_Master_Record { get; set; }
                    public string Partner_Function2 { get; set; }
                    public string Number_Of_Business_Master_in_Vendor_Master_Record2 { get; set; }
                    public string Partner_Function3 { get; set; }
                    public string Number_Of_Business_Master_in_Vendor_Master_Record3 { get; set; }
                    public string Partner_Function4 { get; set; }
                    public string Number_Of_Business_Master_in_Vendor_Master_Record4 { get; set; }
                    public string Partner_Function5 { get; set; }
                    public string Number_Of_Business_Master_in_Vendor_Master_Record5 { get; set; }
                    public string Partner_Function6 { get; set; }
                    public string Number_Of_Business_Master_in_Vendor_Master_Record6 { get; set; }
                    public string Partner_counter { get; set; }
                    public string Name_Person_who_CreatedObject { get; set; }
                    public string Date_Which_Record_Created { get; set; }
                    public string Reference_vendor { get; set; }
                    public string Personnel_Number_BatchInputField { get; set; }
                    public string Country_Key { get; set; }
                    public string SupplyRegion_RegionSupplied { get; set; }
                    public string AccountNumber_VendorCreditor { get; set; }
                    public string Material_Number { get; set; }
                    public string Preference_Zone { get; set; }
                    public int IsActive { get; set; }

    }
}