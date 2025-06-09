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
    public class MaterialChange : Base
    {
        public int Material_Change_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Material_Code { get; set; }
        public string Material_Desc { get; set; }
        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Material_Acc_Grp { get; set; }
        public string Sales_Organisation_Id { get; set; }
        public string Distribution_Channel_Id { get; set; }
        //public string Division_Id { get; set; }

        public int Material_Change_Detail_Id { get; set; }
        public int Section_Id { get; set; }
        public int Section_Feild_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }

        public int IsActive { get; set; }
    }

    public class MaterialChangeDetail : Base
    {
        public int Material_Change_Detail_Id { get; set; }
        public int Material_Change_Id { get; set; }

        public int Section_Id { get; set; }
        public int Section_Feild_Master_Id { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }

        public int IsActive { get; set; }
    }
}