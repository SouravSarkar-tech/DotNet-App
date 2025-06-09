using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProfitCenterCreate
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class ProfitCenterCreate : Base
    {
        public int PCMaster_Create_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string sProfitCenter { get; set; }
        public string sRef_ProfitCenter { get; set; }
        public string dAnalysisPeriodF { get; set; }
        public string dAnalysisPeriodT { get; set; }
        public string sContrlArea { get; set; }
        public string sName { get; set; }
        public string sLongText { get; set; }
        public string sUserRespons { get; set; }
        public string sPersonRespons { get; set; }
        public string sDepartment { get; set; }
        public string sProfitCtrGrp { get; set; }
        public string sSegment { get; set; }
        public string sRemarks { get; set; }
        public string Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
        public int IsDraf { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
        public string Company_Code { get; set; }
    }

    public class ProfitCenterBlock : Base
    {
        public int PCMaster_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string sProfitCenter { get; set; }
        public string sProfitCenterName { get; set; }
        public string bBlockUnBlockStatus { get; set; }
        public string sRemarks { get; set; }
        public string Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
        public int IsDraf { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
        public string Company_Code { get; set; }
    }


    public class PCenterChange : Base
    {
        public int PCMaster_Change_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string sProfitCenter { get; set; }
        public string sProfitCenterName { get; set; }

        public int PCMaster_Change_Details_Id { get; set; }
        public int Section_Id { get; set; }
        public int Section_Field_Master_Id { get; set; }
        public string sOld_Value { get; set; }
        public string sNew_Value { get; set; }

        public int IsActive { get; set; }
        public int IsDraf { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIp { get; set; } 
    }


    public class PCenterChangeDt : Base
    {
        public int PCMaster_Change_Details_Id { get; set; }
        public int PCMaster_Change_Id { get; set; }
        public int Section_Id { get; set; }
        public int Section_Field_Master_Id { get; set; }
        public string Field { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string sOld_Value { get; set; }
        public string sNew_Value { get; set; }
        public string sOld_Value2 { get; set; }
        public string sNew_Value2 { get; set; }
        public string sOld_Value3 { get; set; }
        public string sNew_Value3 { get; set; }
        public string sOld_Value4 { get; set; }
        public string sNew_Value4 { get; set; }
        public string sOld_Value5 { get; set; }
        public string sNew_Value5 { get; set; }
        public string sRemarks { get; set; }
        public int IsActive { get; set; } 
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIp { get; set; } 
    }
}