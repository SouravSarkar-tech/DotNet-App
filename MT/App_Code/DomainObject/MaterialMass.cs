using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialMass
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class MaterialMass : Base
    {
        public int MHId { get; set; }
        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Material_Code { get; set; }
        public string Material_Type { get; set; }
        public string BUOM { get; set; }
        public string Material_Desc { get; set; }
        public string Material_Grp { get; set; }
        public string Division { get; set; }
        public string Remarks { get; set; }
        public string Acc_Assgnt_Grp { get; set; }
        public string ProdHierarchy { get; set; }
        public string Pur_Order_Unit_Measure { get; set; }
        public string Pur_Value_Key { get; set; }
        public string Pur_Group { get; set; }
        public string No_Of_Manufacturer { get; set; }
        public string Manufacturer_Part_No { get; set; }
        public string Manufacturer_Part_Profile { get; set; }
        public string GR_Processing_Time { get; set; }
        public string Purchase_Order_text { get; set; }
        public string ControlCode { get; set; }
        public string MRP_BUOM { get; set; }
        public string MRP_type { get; set; }
        public string MRP_Controller { get; set; }
        public string Reorder_Point { get; set; }
        public string Lot_Size { get; set; }
        public string Min_Lot_Size { get; set; }
        public string Max_Lot_Size { get; set; }
        public string Fixed_Lot_Size { get; set; }
        public string Rounding_Value { get; set; }
        public string Max_Stock_Level { get; set; }
        public string Planning_Time_Fence { get; set; }
        public string Prod_Unit { get; set; }
        public string Procurement_Type { get; set; }
        public string Delivery_Time { get; set; }
        public string Inhouse_Prod_Time { get; set; }
        public string Min_Safety_Stock { get; set; }
        public string Fxd_Lot_Size_Storage_Loc { get; set; }
        public string Storage_bin { get; set; }
        public string Min_Rem_Shelf_Life { get; set; }
        public string Total_Shelf_Life { get; set; }
        public string Profit_Center { get; set; }
        public string Unit_Issue { get; set; }
        public string Is_QM_is_Procurement { get; set; }
        public string Certificate_Type { get; set; }
        public string Ctrl_Key_QM_Procurement { get; set; }
        public string Interval_Nxt_Inspection { get; set; }
        public string Inspection_Type { get; set; }
        public string Valuation_Class { get; set; }
        public string Price_Ctrl_Indicator { get; set; }
        public string Lot_Size_Prd_Cost_Est { get; set; }

        public int IsActive { get; set; }
    }
      
}