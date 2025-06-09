using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BankMaster
/// </summary>

namespace Accenture.MWT.DomainObject
{
    public class BankMaster : Base
    {
        public int Bank_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Bank_Key { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
        public string House_No_Street { get; set; }
        public string City { get; set; }
        public int Country_Id { get; set; }
        public int Region_Id { get; set; }
        public string Swift { get; set; }
        public string Bank_Number { get; set; }
        public string Bank_Group { get; set; }
        public int Ref_Master_Header_Id { get; set; }
        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}