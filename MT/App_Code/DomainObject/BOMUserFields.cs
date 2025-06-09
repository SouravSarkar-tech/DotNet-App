using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOMUserFields
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class BOMUserFields : Base
    {
        public int Pk_BOM_UserFieldsId { get; set; }
        public int Master_Header_Id { get; set; }
        public string sActivity { get; set; }
        public string sFieldkey { get; set; } 
        public string sGFText1 { get; set; }
        public string sGFText2 { get; set; }
        public string sGFText3 { get; set; }
        public string sGFText4 { get; set; }
        public string sNFQty1 { get; set; }
        public string sNFQty2 { get; set; }
        public string sNFValue1 { get; set; }
        public string sNFValue2 { get; set; }

        public string dDTdate1 { get; set; }
        public string dDTdate2 { get; set; }
        public string bCBKX_Sche { get; set; }
        public string bCBIndicator { get; set; }

        public int IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedBy_IP { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedBy_IP { get; set; }

        public string sQUNIT1 { get; set; }
        public string sQUNIT2 { get; set; }
        public string sVUNIT1 { get; set; }
        public string sVUNIT2 { get; set; } 
    }
}