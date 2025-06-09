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
    public class SalesArea1 : Base
    {
        public int Cust_SalesArea1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Division_ID { get; set; }
        
        public string countryKeyExport { get; set; }
        public string SalesDistrict { get; set; }
        public string SalesOffice { get; set; }
        public string SalesGroup { get; set; }
        public string Currency { get; set; }
        public string DeliveringPlant { get; set; }
        public string PriceGroup { get; set; }
        public string InvoiceListSchedule { get; set; }
        public string InvoiceDates { get; set; }

        public string Credit_Control_Area { get; set; }
        public string Customer_credit_limit { get; set; }
        public string Risk_category { get; set; }
        public string Currency_Id { get; set; }
        public int IsActive { get; set; }
    }

    public class SalesArea2 : Base
    {
        public int Cust_SalesArea2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Division_ID { get; set; }
        public int IsActive { get; set; }
        public string BilingBlockCust { get; set; }
        public string IndiCustRebate { get; set; }
        public string EXchangeRateTYpe { get; set; }
        public string CustomerGroup1 { get; set; }
        public string CustomerGroup2 { get; set; }
        public string CustomerGroup3 { get; set; }
        public string CustomerGroup4 { get; set; }
        public string CustomerGroup5 { get; set; }
        public string CustPayGuarantProc { get; set; }
        public string CreditControlArea { get; set; }
        public string SalesBlockCust { get; set; }
        public string SwitchOffRound { get; set; }
        public string CustClassABC { get; set; }
        public string TaxCategory { get; set; }
        public string TaxClassificationCust { get; set; }
        public string LicenceNumber { get; set; }
        public string DateBatchInput { get; set; }
        public string DateBatchin2 { get; set; }
        public string ConfirmationLicenses { get; set; }
        public string OrderProbabilityitem { get; set; }

        public string ItemProposal { get; set; }
        public string CustomerGroup { get; set; }
        public string ShipperAccountCustVendor { get; set; }
        
        public string PricingProcuderAssCust { get; set; }
        public string DeliveryPriority { get; set; }
        public string PriceListType { get; set; }
        public string CustStatisticsGroup { get; set; }
        public string OrderCombinationIndi { get; set; }
        public string ShippingCondition { get; set; }
        
        public string CompleteDeliverySalesOrder { get; set; }
        public int PartialItemLevel { get; set; }
        public string MaxPermittedDeliveries { get; set; }
        
        public string IncotermsPart1 { get; set; }
        public string IncotermsPart2 { get; set; }
        public string TermPaymentKey { get; set; }
        public string AccAssignmentCust { get; set; }
        public string DeletionFlagCust { get; set; }
        public string CustOrderBlock { get; set; }
        public string CustDeliveryBlock { get; set; }
        public string AuthorizationGroup { get; set; }

        public string PartnerFunction { get; set; }
        public string NumberSDBusinPartner { get; set; }
        public string PartnerFunction2 { get; set; }
        public string NumberSDBusinPartner2 { get; set; }
        public string PartnerFunction3 { get; set; }
        public string NumberSDBusinPartner3 { get; set; }
        public string PartnerFunction4 { get; set; }
        public string NumberSDBusinPartner4 { get; set; }
        public string PartnerFunction5 { get; set; }
        public string NumberSDBusinPartner5 { get; set; }
        public string PartnerFunction6 { get; set; }
        public string NumberSDBusinPartner6 { get; set; }
        public string DefaultPartner { get; set; }
        public string CateIndiTaxCodes { get; set; }
        public string TCSYesNo { get; set; }
    }
}