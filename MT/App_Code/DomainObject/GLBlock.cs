using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLBlock
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class GLBlock : Base
    {
        public int GL_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Company_Code { get; set; }
        public string GL_Code { get; set; }

        public string Blocked_For_Creation { get; set; }
        public string Blocked_For_Posting { get; set; }
        public string Blocked_For_Planning { get; set; }
        public string Blocked_For_Posting_CC { get; set; }
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