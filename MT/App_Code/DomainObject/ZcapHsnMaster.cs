using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZcapHsnMaster
/// </summary> 
namespace Accenture.MWT.DomainObject
{
    public class ZcapHsnMasterCreate : Base
    {
        public int HSN_ZCAP_Detaiils_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string sMaterial_Code { get; set; }
        public string sSupp_plant { get; set; }
        public string sRece_plant { get; set; }
        public string sCondintion_type { get; set; }
        public string sZcapRate { get; set; }
        public string sUOM { get; set; }
        public string sSTONum { get; set; }
        public string sHSN_Code { get; set; }
        public string sGST_Code { get; set; }
        public string sIsLUTCond { get; set; }
        public string sRemarks { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
        public string sMaterial_Name { get; set; }
    }

    public class ZcapHsnMaster : Base
    {
        public int HSN_ZPAC_Type_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Company_Code { get; set; }
        public string Module_Id { get; set; }
        public string Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
        public int IsDraf { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }

    }
}