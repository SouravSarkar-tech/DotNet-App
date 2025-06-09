using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accenture.MWT.DomainObject
{
    
   public class Sales1 : Base
    {
        public int Mat_Sales1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Sales_Unit { get; set; }
        public string Distri_Chain_Speci_Mat_Status { get; set; }
        public string Mat_Status_Sales_Valid_From { get; set; }
        public string Delivery_Plant { get; set; }
        public string Min_Order_Quan_Base_Unit { get; set; }
        public string Min_Delivery_Quan_Delivery_Note { get; set; }
        public string Min_Make_To_Order_Quan { get; set; }
        public string Delivery_Unit { get; set; }
        public string Unit_Measure_Delivery_Unit { get; set; }
        public string Is_Cash_Discount { get; set; }
        public string Vari_Sales_Unit_Not_Allowed { get; set; }
        public string Sales_Text { get; set; }
        public int IsActive { get; set; }
    }

   public class Sales2 : Base
    {
        public int Mat_Sales2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Mat_Statistic_Group { get; set; }
        public string Volume_Rebate_Group { get; set; }
        public string Commission_Group { get; set; }
        public string Gen_Item_Category_Grp { get; set; }
        public string Item_Category_Grp { get; set; }
        public string Product_Hierarchy { get; set; }
        public string Pricing_Ref_Mat { get; set; }
        public string Mat_Pricing_Group { get; set; }
        public string Acc_Assignment_Grp { get; set; }
        public string Material_Group1 { get; set; }
        public string Material_Group2 { get; set; }
        public string Material_Group3 { get; set; }
        public string Material_Group4 { get; set; }
        public string Material_Group5 { get; set; }
        public string Cross_Distrib_Chain_Mat_Status { get; set; }
        public string CAS_No_Pharma_Prod_FT { get; set; }
        public int IsActive { get; set; }
    }
   public class Sales3 : Base
    {
        public int Mat_Sales3_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Transportation_Group { get; set; }
        public string Shipping_SetUp_Time { get; set; }
        public string Base_Quan_Capital_Plan { get; set; }
        public string Shipping_Processing_Time { get; set; }
        public string Loading_Group { get; set; }
        public string Availability_Check { get; set; }
        public string Batch_Mgmt { get; set; }
        public string Profit_Center { get; set; }
        public int IsActive { get; set; }
    }
}