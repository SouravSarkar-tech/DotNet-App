
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialCreateExtension
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class MaterialCreateExtension : Base
    {
        public int Mat_Create_Extension_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Mat_Pricing_Group { get; set; }
        public string Acc_Assignment_Grp { get; set; }
        public string Purchasing_Group { get; set; }
        public string MRP_Type { get; set; }
        public string MRP_Controller { get; set; }
        public string Reorder_Point { get; set; }
        public string Lot_Size { get; set; }
        public string Min_Lot_Size { get; set; }
        public string Max_Lot_Size { get; set; }
        public string Fixed_Lot_Size { get; set; }
        public string Rounding_Value { get; set; }
        public string Issue_Storage_Location { get; set; }
        public string GR_Processing_Time { get; set; }
        public string Planned_Delivery_Time_Days { get; set; }
        public string Profit_Center { get; set; }
        public string Valuation_Class { get; set; }
        public string Price_Ctrl_Indicator { get; set; }
        public string Spl_Procurement_Type { get; set; }
        public string Inspection_Type { get; set; }
        public string Interval_Nxt_Inspection { get; set; }
        public string IsActive { get; set; }
    }
}