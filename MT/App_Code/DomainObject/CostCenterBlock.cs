using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CostCenterBlock
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class CostCenterBlock : Base
    {
        public int CostCenter_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Cost_Center { get; set; }
        public string Cost_Center_Name { get; set; }

        public string LIActualPrimaryCost { get; set; }
        public string LIPlanPrimaryCost { get; set; }
        public string LIActualSecondaryCost { get; set; }
        public string LIPlanSecondaryCost { get; set; }
        public string LIActualRevenuePosting { get; set; }
        public string LIPlanRevenuePosting { get; set; }

        public string Remarks { get; set; }
        public int Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIP { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIP { get; set; }
    }
}