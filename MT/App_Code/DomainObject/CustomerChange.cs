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
    public class CustomerChange : Base
    {
        public int Customer_Change_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Customer_Desc { get; set; }
        public string Company_Code { get; set; }
        public string Customer_Acc_Grp { get; set; }
        public string Sales_Organisation_Id { get; set; }
        public string Distribution_Channel_Id { get; set; }
        public string Division_Id { get; set; }

        public int Customer_Change_Detail_Id { get; set; }
        public int Section_Id { get; set; }
        public int Section_Feild_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }

        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }


    }

    public class CustomerChangeDetail : Base
    {
        public int Customer_Change_Detail_Id { get; set; }
        public int Customer_Change_Id { get; set; }

        public int Section_Id { get; set; }
        public int Section_Feild_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }

        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}