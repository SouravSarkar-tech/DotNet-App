using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TANExemptions
/// 8400000388
/// </summary> 

namespace Accenture.MWT.DomainObject
{
    public class TANExemptions : Base
    {
        public int Pk_TANId { get; set; }
        public int Master_Header_Id { get; set; }
        public string sSectionCode { get; set; }
        public string sExemptNum { get; set; }
        public string sExemptRate { get; set; }
        public string dExemptFrom { get; set; }
        public string dExemptTo { get; set; }         
        public string sExemptReason { get; set; }
        public string sWHTType { get; set; }
        public string sWtaxCode { get; set; }
        public string sExemThreshold { get; set; }
        public string sCurrency { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }


    }
     
}