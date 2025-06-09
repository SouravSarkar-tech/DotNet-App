using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accenture.MWT.DomainObject
{
    public class BasicData : Base
    {
        public int Mat_Basic_Data1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Number { get; set; }
        public string Material_Type { get; set; }
        public string Industry_Sector { get; set; }
        public string Base_Unit_Of_Measure { get; set; }
        public string Material_Short_Description { get; set; }
        public string Material_Group { get; set; }
        public string Old_Material_Number { get; set; }
        public string External_Material_Group { get; set; }
        public string Lab_Design_Office { get; set; }
        public string Division { get; set; }
        public string Product_Hierarchy { get; set; }
        public string Cross_Plant_Material_Status { get; set; }
        public string Valid_From { get; set; }
        public string Gen_Item_Category_Grp { get; set; }
        public string Prod_Inspect_Memo { get; set; }
        public string Gross_Weight { get; set; }
        public string Net_Weight { get; set; }
        public string Weight_Unit { get; set; }
        public string Volume { get; set; }
        public string Volume_Unit { get; set; }
        public string InterNational_Article_No { get; set; }
        public string Category_InterN_Article_No { get; set; }
        public string Material_Grp_Pack_Mtl { get; set; }
        public string Reason_For_Creation { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
        //CTRL_SUB_SDT06062019
        public string sControlSubYN { get; set; }
        public string ReqCreatedOn { get; set; }
        //CTRL_SUB_EDT06062019
        //DT05072023_BG_Type
        public string sBGWCF { get; set; }
        //DT05072023_BG_Type
        // PROV-CCP-MM-941-23-0045 in QAMS
        public string sIsMatComm { get; set; }
        // PROV-CCP-MM-941-23-0045 in QAMS
    }


    public class BasicData2 : Base
    {
        public int Mat_Basic_Data2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Unit_Of_Dimension { get; set; }

        public string Desc_Language { get; set; }
        public string Desc_Text { get; set; }
        public string Desc_Language1 { get; set; }
        public string Desc_Text1 { get; set; }
        public string Basic_Data_Language { get; set; }
        public string Basic_Data_Text { get; set; }
        public string Basic_Data_Language1 { get; set; }
        public string Basic_Data_Text1 { get; set; }
        public string Inspection_Language { get; set; }
        public string Inspection_Text { get; set; }
        public string Inspection_Language1 { get; set; }
        public string Inspection_Text1 { get; set; }
        public string Internal_Comment_Language { get; set; }
        public string Internal_Comment_Text { get; set; }
        public string Internal_Comment_Language1 { get; set; }
        public string Internal_Comment_Text1 { get; set; }
        public string Alt_Unit_Value_X { get; set; }
        public string Alt_Unit_Of_Measure { get; set; }
        public string Alt_Unit_Value_Y { get; set; }
        public string Alt_Unit_Value_X1 { get; set; }
        public string Alt_Unit_Of_Measure1 { get; set; }
        public string Alt_Unit_Value_Y1 { get; set; }
        public string Alt_Unit_Value_X2 { get; set; }
        public string Alt_Unit_Of_Measure2 { get; set; }
        public string Alt_Unit_Value_Y2 { get; set; }
        public string Shipper_Gross_Weight { get; set; }
        public string Shipper_Weight_Unit { get; set; }
        public string Unit_Of_Measure_Usage { get; set; }
        public string Characteristic_Name { get; set; }
        public string Planned_Value_For_Unit_Measure { get; set; }
        public string Batch_Spcf_Matl_Unit_Measure { get; set; }
        public int IsActive { get; set; }

   public string Alt_Unit_Value_X3 { get; set; }
        public string Alt_Unit_Of_Measure3 { get; set; }
        public string Alt_Unit_Value_Y3 { get; set; }

        public string Alt_Unit_Value_X4 { get; set; }
        public string Alt_Unit_Of_Measure4 { get; set; }
        public string Alt_Unit_Value_Y4 { get; set; }

        public string Alt_Unit_Value_X5 { get; set; }
        public string Alt_Unit_Of_Measure5 { get; set; }
        public string Alt_Unit_Value_Y5 { get; set; } 
         public string Alt_Unit_Value_X6 { get; set; }
        public string Alt_Unit_Of_Measure6 { get; set; }
        public string Alt_Unit_Value_Y6 { get; set; }



    }

    public class MaterialDesc : Base
    {
        public int Mat_Material_Desc_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Item_Description { get; set; }
        public string Item_Type { get; set; }
        public string MOC { get; set; }
        public string Size { get; set; }
        public string Process_Connection_Size { get; set; }
        public string Class_Rating_Grade { get; set; }
        public string MFG_Std { get; set; }
        public string Range_Capacity { get; set; }
        public string Accuracy_LeastCount { get; set; }
        public string Supply_Voltage { get; set; }
        public string Flame_Proof { get; set; }
        public string Protection_Class { get; set; }
        public string Input_Output { get; set; }
        public string Manufacturer_PartNo { get; set; }
        public string Make_MachName_ModelNo { get; set; }
        public string Material_Desc { get; set; }
        public int IsActive { get; set; }

    }

    public class SMChange
    {
        public int colFieldName { get; set; }
        public string colOldVal { get; set; }
        public string colNewVal { get; set; }
    }
}