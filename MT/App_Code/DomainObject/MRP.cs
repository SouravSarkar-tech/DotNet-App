using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MRP
/// </summary>

namespace Accenture.MWT.DomainObject
{
    public class MRP1 : Base
    {
        public int Mat_MRP1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Base_Unit_Of_Measure { get; set; }
        public string Purchasing_Group { get; set; }
        public string MRP_Type { get; set; }
        public string MRP_Controller { get; set; }
        public string Reorder_Point { get; set; }
        public string Lot_Size { get; set; }
        public string Min_Lot_Size { get; set; }
        public string Max_Lot_Size { get; set; }
        public string Fixed_Lot_Size { get; set; }
        public string Rounding_Value { get; set; }
        public string Max_Stock_Level { get; set; }
        public string ABC_Indicator { get; set; }
        public string Scrap { get; set; }
        public string Planning_Time_Fence { get; set; }
        public string Production_Unit { get; set; }
        public string MRP_Group { get; set; }
        public string Takt_Time { get; set; }
        public string Planning_Cycle { get; set; }
        public string Rounding_Profile { get; set; }
        public string Unit_Measure_Grp { get; set; }

        public int IsActive { get; set; }

    }

    public class MRP2 : Base
    {
        public int Mat_MRP2_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Procurement_Type { get; set; }
        public string Spl_Procurement_Type { get; set; }
        public string Proposed_Supply_Area { get; set; }
        public string Planned_Delivery_Time_Days { get; set; }
        public string InHouse_Production_Time { get; set; }
        public string Schedule_Margin_Key_Float { get; set; }
        public string Safety_Stock { get; set; }
        public string Issue_Storage_Location { get; set; }
        public string Range_Coverage_Profile { get; set; }
        public string Indicator_Bulk_Material { get; set; }
        public string Indicator_BackFlush { get; set; }
        public string Default_Storage_Loc_Ext_Proc { get; set; }
        public string Production_Sched_Profile { get; set; }
        public string Safety_Time_Indicator { get; set; }
        public string Safety_Time_WorkDays { get; set; }
        public string Batch_Entry_Production { get; set; }
        public string Indicator_JIT_Delivery { get; set; }
        public string Period_Profile_Safety_Time { get; set; }
        public string Lower_Limit_Safety_Stock { get; set; }
        public string Quota_Arrangement_Usage { get; set; }
        public string GR_Processing_Time { get; set; }
        public string Planning_Calander { get; set; }
        public string Min_Safety_Stock { get; set; }

        public int IsActive { get; set; }
    }


    public class MRP3 : Base
    {
        public int Mat_MRP3_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Period_Indicator { get; set; }
        public string Fiscal_Year_Variant { get; set; }
        public string Splitting_Indicator { get; set; }
        public string Checking_Grp_Availability_Chk { get; set; }
        public string Consumption_Mode { get; set; }
        public string BackWard_Consumption_Period { get; set; }
        public string Forward_Consumption_Period { get; set; }
        public string Mixed_MRP_Indicator { get; set; }
        public string Replenishment_Lead_Time { get; set; }
        public string Planning_Material { get; set; }
        public string Planning_Plant { get; set; }
        public string Conv_Factor_Plng_Mat { get; set; }
        public string Plng_Strategy_Grp { get; set; }

        public int IsActive { get; set; }
    }


    public class MRP4 : Base
    {
        public int Mat_MRP4_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Selection_Method { get; set; }
        public string Dependent_Req_Ind { get; set; }
        public string Indicator_Req_Grping { get; set; }
        public string Storage_Loc_MRP_Indicator { get; set; }
        public string ReOrder_Pt_Storage_Loc { get; set; }
        public string Fxd_Lot_Size_Storage_Loc { get; set; }
        public string Ind_Repetative_Mfg_Allowed { get; set; }
        public string Component_Scrap_Perc { get; set; }
        public string Discontinuation_Indicator { get; set; }
        public string Effective_Out_Date { get; set; }
        public string Follow_Up_Mat { get; set; }
        public string Spl_Procur_Type_Stro_Loc { get; set; }
        public string MRP_Relevance_Dep_Req { get; set; }
        public string Fair_Share_Rule { get; set; }
        public string Indi_Push_Distribution { get; set; }

        public int IsActive { get; set; }
    }
}