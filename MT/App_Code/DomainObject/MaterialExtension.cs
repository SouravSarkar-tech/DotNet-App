
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
    public class MaterialExtension : Base
    {
        public int Material_Extension_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Number { get; set; }
        public string Material_Type { get; set; }
        public string Material_Short_Description { get; set; }
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
        public string Warehouse_ID { get; set; }
        public string Storage_Type_ID { get; set; }
        public string Capacity_Usage { get; set; }
        public string Interval_Nxt_Inspection { get; set; }
        public string WM_Unit_Measure { get; set; }
        public string Stor_Type_Ind_Stock_Placement { get; set; }
        public string Stor_Type_Ind_Stock_Removal { get; set; }
        public string Storage_Section_Ind { get; set; }
        public string Loading_Equipment_Quantity { get; set; }
        public string Loading_Equipment_Quantity1 { get; set; }
        public string Loading_Equipment_Quantity2 { get; set; }
        public string Unit_Loading_Equip_Quan { get; set; }
        public string Unit_Loading_Equip_Quan1 { get; set; }
        public string Unit_Loading_Equip_Quan2 { get; set; }
        public string Storage_Unit_Type { get; set; }
        public string Storage_Unit_Type1 { get; set; }
        public string Storage_Unit_Type2 { get; set; }
        //public string Reason_For_Creation { get; set; }
        public string Remarks { get; set; }
        //MSE_8300002156
        public string Is_QM_in_Procurement { get; set; }
        public string Certificate_Type { get; set; }
        public string Ctrl_Key_QM_Procurement { get; set; }
        public string Range_Coverage_Profile { get; set; }
        //NRDT02032023 Start
        public string MatAuthGrpActQM { get; set; }
        //NRDT02032023 End
        //MSE_8300002156
        //MAT_DT26102020
        public string MInspType { get; set; }
        //MAT_DT26102020
        public string IsActive { get; set; }
        public string IsDraf { get; set; }
    }
}