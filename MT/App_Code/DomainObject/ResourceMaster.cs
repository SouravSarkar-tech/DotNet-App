using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResourceMaster
/// </summary>
/// 
namespace Accenture.MWT.DomainObject
{
    public class ResourceMaster : Base
    {
        public int Resource_Master_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Resource { get; set; }
        public string Object_Name { get; set; }
        public string Person_Resp_WorkCenter { get; set; }
        public string Standard_Value_Key { get; set; }
        public string Unit_Of_Measure_Std_Value { get; set; }
        public string Unit_Of_Measure_Std_Value2 { get; set; }
        public string Formula_Cap_Req_Int_Process { get; set; }
        public string Capacity_Short_Text { get; set; }
        public string Capacity_Planner_Group { get; set; }
        public string Start_Time { get; set; }
        public string Finish_Time { get; set; }
        public int Capacity_Utilization_Rate { get; set; }
        public string Cumulative_Len_Break_Per_Shift { get; set; }
        public string Number_Of_Ind_Cap { get; set; }
        public string Base_UOM_Capacity { get; set; }
        public string Formula_Duration_Int_Process { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Cost_Center { get; set; }
        public string Activity_Type { get; set; }
        public string Activity_Type2 { get; set; }
        public string Activity_Unit { get; set; }
        public string Activity_Unit2 { get; set; }
        public string Formula_Key { get; set; }
        public string Formula_Key2 { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}