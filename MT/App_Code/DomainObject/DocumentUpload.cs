using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DocumentUpload
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class DocumentUpload : Base
    {
        public int Document_Upload_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Request_No { get; set; }
        public string Document_Type { get; set; }
        public string Document_Name { get; set; }
        public string Document_Path { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}