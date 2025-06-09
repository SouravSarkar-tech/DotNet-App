using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CostCenterCreate
/// </summary>

namespace Accenture.MWT.DomainObject
{
    public class CostCenterCreate : Base
    {
        public int ID { get; set; }
        public int Master_Header_Id { get; set; }
        public string Cost_Center { get; set; }
        public string Ref_Cost_Center { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string ControllingArea { get; set; }
        public string Cost_Center_Name { get; set; }
        public string Cost_Center_Desc { get; set; }
        public string User_Responsible { get; set; }
        public string Person_Responsible { get; set; }
        public string Department { get; set; }
        public string Cost_Center_Category { get; set; }
        public string Hierarchy_Area { get; set; }
        public string Company_Code { get; set; }
        public string Business_Area { get; set; }
        public string Profit_Center { get; set; }
        //public string Remarks { get; set; }
        public string Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
    }
}