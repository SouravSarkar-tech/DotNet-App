using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for GL Creation
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class GLCreate1 : Base
    {
        public int ID { get; set; }
        public int Master_Header_Id { get; set; }
        public string GL_Code { get; set; }
        public string Ref_GL_Code { get; set; }
        public string Company_Code { get; set; }
        public string GLGroup { get; set; }
        public string Change_Ref_Id { get; set; }
        public int IsActive { get; set; }
        public string Account_Group { get; set; }
        public string PnL_BalanceSheet { get; set; }
        public string Short_Text { get; set; }
        public string GL_Acct_Long_Text { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public string Rec_Account { get; set; }
        public string Open_Item_Management { get; set; }
        public string Line_Item_Display { get; set; }
        public string Reason_For_Creation { get; set; }
        public string Remarks { get; set; }
        public string Ref_Company_Code { get; set; }
        public string CostElementCategory { get; set; }

        public string GLAccType { get; set; } //S4HanaGLDT07122021
        public string GLAccSubType { get; set; } //S4HanaGLDT07122021
        public string ClearSpectoLedgerGPS { get; set; } //S4HanaGLDT07122021

    }
}