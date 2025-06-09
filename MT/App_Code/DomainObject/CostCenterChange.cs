using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CostCenterChange
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class CostCenterChange : Base
    {
        public int CostCenter_Change_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Cost_Center { get; set; }
        public string Cost_Center_Name { get; set; }
        //public string Account_Group { get; set; }
        //public string GL_Desc { get; set; }

        public int CostCenter_Change_Detail_Id { get; set; }
        public int Section_Id { get; set; }
        public int Section_Field_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }


    }

    public class CostCenterChangeDetail : Base
    {
        public int CostCenter_Change_Detail_Id { get; set; }
        public int CostCenter_Change_Id { get; set; }

        public int Section_Id { get; set; }
        public int Section_Field_Master_Id { get; set; }
        public string Field { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }
        public string Old_Value2 { get; set; }
        public string New_Value2 { get; set; }
        public string Old_Value3 { get; set; }
        public string New_Value3 { get; set; }
        public string Old_Value4 { get; set; }
        public string New_Value4 { get; set; }
        public string Old_Value5 { get; set; }
        public string New_Value5 { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}