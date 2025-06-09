using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerExtension
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class CustomerExtension : Base
    {
        public int Cust_Extension_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Company_Code { get; set; }
        public string Customer_Acc_Grp { get; set; }
        public string Customer_Desc { get; set; }

        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_Id { get; set; }
        public string Division_ID { get; set; }

        public string SalesDistrict { get; set; }
        public string SalesOffice { get; set; }
        public string SalesGroup { get; set; }

        public string countryKeyExport { get; set; }
        public string Currency { get; set; }
        public string DeliveringPlant { get; set; }
        public string PriceGroup { get; set; }
        public string InvoiceListSchedule { get; set; }
        public string InvoiceDates { get; set; }
        
        public string Credit_Control_Area { get; set; }
        public string Customer_credit_limit { get; set; }
        public string Risk_category { get; set; }
        public string Currency_Id { get; set; }
        
        public string Remarks { get; set; }
        public string IsActive { get; set; }
    }
}