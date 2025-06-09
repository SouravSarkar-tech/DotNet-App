using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accenture.MWT.DomainObject
{
    public class Purchasing : Base
    {
        public int Mat_Purchasing_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Plant_Id { get; set; }
        public string Plant_Specific_Mat_Status { get; set; }
        public string Pur_Order_Unit_Measure { get; set; }
        public string Purchasing_Value_Key { get; set; }
        public string Purchasing_Group { get; set; }
        public string Batch_Mgmt_Req_Indicator { get; set; }
        public string Processing_Time_Goods_Receipt_Days { get; set; }
        public string Quota_Arrangement_Usage { get; set; }
        public string Indicator_Critical_Part { get; set; }
        public string Post_Inspection_Stock { get; set; }
        public string Indicator_Auto_PO_Allowed { get; set; }
        public string Ind_Source_List_Req { get; set; }
        public string Variable_Pur_Ord_Unit_Active { get; set; }
        public string Tolerance_Limit_OverDelivery { get; set; }
        public string Ind_Unlimited_OverDelivery_Allowed { get; set; }
        public string Tolerance_Limit_UnderDelivery { get; set; }
        public string Tax_Indicator_Material { get; set; }
        public string Mat_Freight_Grp { get; set; }
        public string No_Of_Manufacturer { get; set; }
        public string Name_Of_Manufacturer { get; set; }
        public string Manufacturer_Part_No { get; set; }
        public string Manufacturer_Part_Profile { get; set; }
        public string Cross_Plant_Mat_Status { get; set; }
        public string Mat_Status_Purchasing_From { get; set; }
        public string Gen_Mat_Status_Sale_From { get; set; }
        public string Mat_Qualifies_Disc { get; set; }
        public string GR_Processing_Time { get; set; }
        public string Purchase_Order_Text { get; set; }
        public string Change_Ref_Id { get; set; }
        public string IsActive { get; set; }
    }
}