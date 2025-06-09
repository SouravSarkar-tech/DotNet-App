using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Recipe
/// </summary>
namespace Accenture.MWT.DomainObject
{
    //public class Recipe : Base
    //{
    //    public int Recipe_Id { get; set; }
    //    public int Master_Header_Id { get; set; }
    //    public string Material { get; set; }
    //    public string Profile { get; set; }
    //    public string Prod_Version { get; set; }
    //    public string TaskList_Desc { get; set; }
    //    public string Recipef { get; set; }
    //    public string Status { get; set; }
    //    public string Usage { get; set; }
    //    public string Base_Quantity { get; set; }
    //    public string Charge_Quantity { get; set; }
    //    public string Operation_Quantity { get; set; }
    //    public string UOM { get; set; }
    //    public string SuperOrdinate_Operation { get; set; }
    //    public string Control_Recipe_Destination { get; set; }
    //    public string Resource { get; set; }
    //    public string Control_Key { get; set; }
    //    public string StandardTextKey { get; set; }
    //    public string Description { get; set; }
    //    public int Relevancy_To_Costing { get; set; }
    //    public string First_Std_Value { get; set; }
    //    public string First_Std_Value_Unit { get; set; }
    //    public string First_Activity_Type { get; set; }
    //    public string Sec_Std_Value { get; set; }
    //    public string Sec_Std_Value_Unit { get; set; }
    //    public string Sec_Activity_Type { get; set; }
    //    public string Third_Std_Value { get; set; }
    //    public string Third_Std_Value_Unit { get; set; }
    //    public string Third_Activity_Type { get; set; }
    //    public string Fourth_Std_Value { get; set; }
    //    public string Fourth_Std_Value_Unit { get; set; }
    //    public string Fourth_Activity_Type { get; set; }
    //    public string Fifth_Std_Value { get; set; }
    //    public string Fifth_Std_Value_Unit { get; set; }
    //    public string Fifth_Activity_Type { get; set; }
    //    public string Sixth_Std_Value { get; set; }
    //    public string Sixth_Std_Value_Unit { get; set; }
    //    public string Sixth_Activity_Type { get; set; }
    //    public string Base_Qty { get; set; }
    //    public DateTime ValidFrom { get; set; }
    //    public DateTime ValidTo { get; set; }
    //    public int IsActive { get; set; }
    //}

    public class RecipeHeader : Base
    {
        public int Recipe_HeaderID { get; set; }
        public int Master_Header_Id { get; set; }
        public string Recipe_Group { get; set; }
        public string Recipe { get; set; }
        public string TaskListDesc { get; set; }
        public string Plant_Id { get; set; }
        public string MaterialNo { get; set; }
        public string MaterialDesc { get; set; }
        public int IsActive { get; set; }
    }

    public class RecipeDetail : Base
    {
        public int Recipe_HeaderID { get; set; }
        public int Recipe_HeaderDetail_Id { get; set; }
        public string Status { get; set; }
        public string Usage { get; set; }
        public string chkStatus { get; set; }
        public string From_LSize { get; set; }
        public string To_LSize { get; set; }
        public string Base_Quantity { get; set; }
        public string Unit { get; set; }
        public string Charge_Quantity { get; set; }
        public string Operation_Quantity { get; set; }
        public string Planner_Group { get; set; }
        public string Resource_network { get; set; }
        public string Network_Plant { get; set; }
        public string Insp_Points { get; set; }
        public string Partial_Lot { get; set; }
        public string Remarks { get; set; }
        public string Reason { get; set; }
        public int IsActive { get; set; }
    }

    public class RecipeOperations : Base
    {
        public int Recipe_HeaderID { get; set; }
        public int Recipe_Operation_Id { get; set; }
        public string Operation_Phase { get; set; }
        public string Phase_Indicator { get; set; }
        public string Sup_Operation { get; set; }
        public string Destinatn { get; set; }
        public string Resource { get; set; }
        public string Control_key { get; set; }
        public string StdText_Key { get; set; }       
        public string Description { get; set; }
        public string Relevancy_To_Costing { get; set; }        
        public string Base_Quantity { get; set; }
        public string Act_Operation_UoM { get; set; }
        public string First_Std_Value { get; set; }
        public string First_Std_Value_Unit { get; set; }        
        public string Sec_Std_Value { get; set; }
        public string Sec_Std_Value_Unit { get; set; }        
        public string Third_Std_Value { get; set; }
        public string Third_Std_Value_Unit { get; set; }        
        public string Plant_Id { get; set; }
        public string ChargeQty { get; set; }
        public string OperQty { get; set; }
        public string ChargeUnit { get; set; }
        public string OperUnit { get; set; }
        public string DeletionFlag { get; set; }
        public int IsActive { get; set; }

        //PROV-CCP-MM-941-23-0076 
        public string Std_Value_4 { get; set; }
        public string Std_Value_Unit_4 { get; set; }
        public string Std_Value_5 { get; set; }
        public string Std_Value_Unit_5 { get; set; }
        public string Std_Value_6 { get; set; }
        public string Std_Value_Unit_6 { get; set; }
        public string AltResource1 { get; set; }
        public string AltResource2 { get; set; }
        public string AltResource3 { get; set; }
        public string AltResource4 { get; set; }
        public string Class_type { get; set; }
        public string WC_Area { get; set; }
        public string WC_Area_grp { get; set; }
        public string IsKX_Sche { get; set; }
        //PROV-CCP-MM-941-23-0076 
    }

    public class RecipeInspChara : Base
    {
        public int Recipe_HeaderID { get; set; }
        public int Recipe_InspChara_Id { get; set; }
        public string Operation_Phase { get; set; }
        public string Characteristic_No { get; set; }
        public string Master_Insp_Char_Code { get; set; }
        public string CodeGrp { get; set; }
        public string Sampling_Procedure { get; set; }
        public string InspPtCmpt { get; set; }
        public string NoRelation { get; set; }
        //Start Adding by Nitish Rao 28/06/2018
        public string Insp_Point { get; set; }
        public string Partial_Lot { get; set; }
        //End Adding by Nitish Rao  28/06/2018
        public string QN_QL { get; set; }
        public string Plant_MIC { get; set; }
        public string Version_MIC { get; set; }
        public string Characteristic_Desc { get; set; }
        public string Long_Text { get; set; }        
        public string Sample_Unit_Measure { get; set; }
        public string Base_Sample_Quantity { get; set; }
        public string Decimal_Places { get; set; }
        public string Unit_Measure { get; set; }
        public string Target_Value { get; set; }
        public string Lower_Spec_Limit { get; set; }
        public string Upper_Spec_Limit { get; set; }
        public string Plant_Selected_Set { get; set; }
        public string Selected_Set { get; set; }
        public string DeleteFlag { get; set; }
        public string Operation_Desc { get; set; }
        public int IsActive { get; set; }
    }

    public class RecipeSecRes : Base
    {
        public int Recipe_HeaderID { get; set; }
        public int Recipe_SecResource_Id { get; set; }
        public string Operation_Phase { get; set; }
        public string SecResource_Item { get; set; }
        public string SecResource { get; set; }
        public string Duration { get; set; }
        public string Unit1 { get; set; }
        public string ActivityType1 { get; set; }
        public string Process { get; set; }
        public string Unit2 { get; set; }
        public string ActivityType2 { get; set; }
        public string Labor { get; set; }
        public string Unit3 { get; set; }
        public string ActivityType3 { get; set; }
        public string StdValKey { get; set; }
        public int IsActive { get; set; }
    }

    public class ProdVersion : Base
    {
        public int ProdVersion_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string MaterialNo { get; set; }
        public string MaterialDesc { get; set; }
        public string ProdVersionNo { get; set; }
        public string ProdVersion_Text { get; set; }
        public string Lock { get; set; }
        public string ProdFrom { get; set; }
        public string ProdTo { get; set; }
        public string ProdUnit { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string TaskListType { get; set; }
        public string RecipeGroup { get; set; }
        public string GroupCntr { get; set; }
        public string AltBOM { get; set; }
        public string BOMUsage { get; set; }
        public int IsActive { get; set; }

        public string RStatus { get; set; }
    }

    #region ITSM413605
    public class UpdateFlagSecRes : Base
    {
        public string sScreenFlag { get; set; }
        public int Master_HeaderID { get; set; }
        public int Recipe_HeaderID { get; set; }
        public int Section_ID { get; set; }
    }

    #endregion




}