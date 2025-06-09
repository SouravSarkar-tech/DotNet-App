using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriceMaster
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class PriceHeader : Base
    {
        public int Price_Header_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Code { get; set; }
        public int Batch_Id { get; set; }
        public string Validity_Date_From { get; set; }
        public string Customer_Code { get; set; }
        public string Vendor_Code { get; set; }
        public int Plant_Id { get; set; }
        public string Price_Group { get; set; }
        public string Processing_Status { get; set; }
        public string Base_Unit_of_Measure { get; set; }
        public string Trade_Price { get; set; }
        public string Excise_Duty { get; set; }
        public string Education_Cess { get; set; }
        public string Sec_High_Edu_Cess { get; set; }
        public string MRP { get; set; }
        public string Rate_Unit { get; set; }
        public int IsActive { get; set; }

    }

    public class PriceDetail : Base
    {
        public int Price_Detail_Id { get; set; }
        public int Price_Header_Id { get; set; }
        public int Region_Id_Delivery_Plant { get; set; }
        public int Region_Id { get; set; }
        public string Trade_Price { get; set; }
        public int IsActive { get; set; }
    }
}