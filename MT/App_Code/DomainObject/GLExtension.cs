using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLExtension
/// </summary>


namespace Accenture.MWT.DomainObject
{
    public class GLExtension : Base
    {
        public int GL_Extension_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string GL_Code { get; set; }
        public string GLGroup { get; set; }
        public string Company_Code { get; set; }
        public string Ref_Company_Code { get; set; }
        public string Remarks { get; set; }
        public string Company_Name { get; set; }
    }
}