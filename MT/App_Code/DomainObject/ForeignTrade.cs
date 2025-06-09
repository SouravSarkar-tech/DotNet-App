using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ForeignTrade
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class ForeignTrade : Base
    {
        public int Mat_Foreign_Trade_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }

        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        
        public string Commodity_Code { get; set; }
        public string Origin_Country_Id { get; set; }
        public string Origin_Region_Id { get; set; }
        public string Imp_Exp_Mat_Grp { get; set; }
        public string Preference_Indicator_Imp_Exp { get; set; }
        public string Exception_Certificate { get; set; }
        public string Control_Code { get; set; }
        public string Chapter_ID { get; set; }
        public string Subcontractors { get; set; }
        public string Material_Type { get; set; }
        public string No_of_Goods_Receipts_per_Excise_Invoice { get; set; }
        public string Output_Material_For_ModVat { get; set; }
        public string Remarks { get; set; }
        //GST Changes
        public string GSTRate { get; set; }
        public string GSTReq { get; set; }
        //GST Changes
        public int IsActive { get; set; }
    }
}