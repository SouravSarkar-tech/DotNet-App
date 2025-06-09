using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MatDepotExtension
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class MatDepotExtension : Base
    {
        public int Mat_Extnsn_Data_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Group { get; set; }
        public string Plant_Id { get; set; }
        public string Purchasing_Group { get; set; }
        public string MRP_Type { get; set; }
        public string MRP_Controller { get; set; }
        public string Reorder_Point { get; set; }
        public string Lot_Size { get; set; }
        public string Fixed_Lot_Size { get; set; }
        public string Rounding_Value { get; set; }
        public string Old_Material_Number { get; set; }
        public string Range_Coverage_Profile { get; set; }
        public string Procurement_Type { get; set; }
        public string Safety_Time_WorkDays { get; set; }
        public string Planned_Delivery_Time_Days { get; set; }
        public string GR_Processing_Time { get; set; }
        public string Spl_Procurement_Type { get; set; }
        public string Fair_Share_Rule { get; set; }
        public string Indi_Push_Distribution { get; set; }
        public string Loading_Group { get; set; }
        public int IsActive { get; set; }

    }
}