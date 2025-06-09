using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorBlock
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class VendorBlock : Base
    {
        public int Vendor_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Company_Code { get; set; }
        public string Purchase_Org { get; set; }

        public string IsAllCompanyBlock { get; set; }
        public string IsSelectedCompanyBlock { get; set; }
        public string IsAllPurchaseOrgBlock { get; set; }
        public string IsSelectedPurchaseOrgBlock { get; set; }
        public string Block_Function { get; set; }
        public string Payment_Block { get; set; }
        public string Remarks { get; set; }
        public int Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}