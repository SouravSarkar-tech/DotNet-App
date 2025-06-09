using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorChange
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class VendorChange : Base
    {
        public int Vendor_Change_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Company_Code { get; set; }
        public string Vendor_Group { get; set; }
        public string Purchase_Org { get; set; }
        public string Vendor_Desc { get; set; }

        public int Vendor_Change_Detail_Id { get; set; }
        public int Section_Id { get; set; }
        public int Section_Feild_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }
        public string Remarks { get; set; }

        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }

        
    }

    public class VendorChangeDetail : Base
    {
        public int Vendor_Change_Detail_Id { get; set; }
        public int Vendor_Change_Id { get; set; }

        public int Section_Id { get; set; }
        public int Section_Feild_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }

    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    public class VendorPartnerFun : Base
    {
        public int Vendor_PFun_Detail_Id { get; set; }
        public int Vendor_Change_Id { get; set; }
        public string sPfun_Lookup_Code { get; set; }
        public string sVendor_Code { get; set; }
        public string sVendor_Desc { get; set; }
        public int bIsActive { get; set; }
        public string dCreatedOn { get; set; }
        public int nCreatedBy { get; set; }
        public string sCreatedIp { get; set; } 
    }

    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    public class VendorChangePFun : Base
    {
        public int Vendor_Change_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Company_Code { get; set; }
        public string Vendor_Group { get; set; }
        public string Purchase_Org { get; set; }
        public string Vendor_Desc { get; set; }

        public int Vendor_PFun_Detail_Id { get; set; } 
        public string sPfun_Lookup_Code { get; set; }
        public string sVendor_Code_link { get; set; }
        public string sVendor_Desc_link { get; set; }

        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }


    }
}