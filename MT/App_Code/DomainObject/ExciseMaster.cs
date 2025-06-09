using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MRP
/// </summary>

namespace Accenture.MWT.DomainObject
{
    public class ChapterId : Base
    {
        public string ChapterPk_Id { get; set; }
        public string Chapter_Id { get; set; }
        public string UOM { get; set; }
        public string Desc_Per_Law1 { get; set; }
        public string Desc_Per_Law2 { get; set; }
        public string Desc_Per_Law3 { get; set; }
        public string Desc_Per_Law4 { get; set; }
        public string Desc_Per_Law5 { get; set; }
        public string Desc_Per_Law6 { get; set; }
        public string Desc_Per_Law7 { get; set; }
        public string Desc_Per_Law8 { get; set; }
    }

    public class MaterialChapterCombination : Base
    {
        public string Mat_Ch_Combi_Id { get; set; }
        public string Material_No { get; set; }
        public string Plant { get; set; }
        public string Chapter_ID { get; set; }
        public string Mat_Subcontractors { get; set; }
        public string Material_Type { get; set; }
        public string Number_Goods { get; set; }
        public string Indicator { get; set; }
        public string Declaration_date { get; set; }
    }

    public class CENVATDetermination : Base
    {
        public string CENVAT_Determination_Id { get; set; }
        public string Plant { get; set; }
        public string Input_material { get; set; }
        public string Output_material { get; set; }
        public string Default_Indicator { get; set; }
        public string Excise_Intimation { get; set; }
    }

    public class VendorExciseDetails : Base
    {
        public string Vendor_Excise_Details_Id { get; set; }
        public string Account_No { get; set; }
        public string ECC_NO { get; set; }
        public string Excise_Reg_No { get; set; }
        public string Excise_Range { get; set; }
        public string Excise_Division { get; set; }
        public string Excise_Commissionerate { get; set; }
        public string Central_Sales_Tax_No { get; set; }
        public string Local_Sales_Tax_No { get; set; }
        public string PAN { get; set; }
        public string Excise_tax_indicator { get; set; }
        public string SSI { get; set; }
        public string Type_Of_Vendor { get; set; }
        public string CENVAT { get; set; }
        public string Service_Tax_Reg { get; set; }
        public string PAN_Ref_No { get; set; }
    }

    public class CustomerExciseDetails : Base
    {
        public string Customer_Excise_Detail_Id { get; set; }
        public string Customer_Number { get; set; }
        public string ECC_NO { get; set; }
        public string Excise_Reg_No { get; set; }
        public string Excise_Range { get; set; }
        public string Excise_Division { get; set; }
        public string Excise_Commissionerate { get; set; }
        public string Central_Sales_Tax_No { get; set; }
        public string Local_Sales_Tax_No { get; set; }
        public string PAN { get; set; }
        public string Excise_tax_indicator { get; set; }
        public string Service_Tax_Reg { get; set; }
        public string PAN_Ref_No { get; set; }
    }

    public class ExciseTaxRate : Base
    {
        public string Excise_Tax_Rate_Id { get; set; }
        public string Chapter_ID { get; set; }
        public string Excise_tax_indicator { get; set; }
        public string Date_from_rule_valid { get; set; }
        public string Date_To_rule_valid { get; set; }
        public string Rate_Excise_Duty { get; set; }
        public string Excise_Duty_Rate { get; set; }
        public string Rate_Unit { get; set; }
        public string Condition_pricing_unit { get; set; }
        public string Condition_unit { get; set; }
        public string Additional_Excise_Duty { get; set; }
        public string Special_Excise_Duty { get; set; }
        public string NCCD_Rate { get; set; }
        public string ECS_Rate { get; set; }
        public string AT1_rate { get; set; }
        public string AT2_rate { get; set; }
        public string AT3_rate { get; set; }
    }


    public class ExceptionMaterialExciseRate : Base
    {
        public string Exception_Material_Excise_Rate_Id { get; set; }
        public string Plant { get; set; }
        public string Material_Number { get; set; }
        public string Account_No { get; set; }
        public string Date_from_valid { get; set; }
        public string Type_Excise_duty { get; set; }
        public string Date_to_valid { get; set; }
        public string Chapter_ID { get; set; }
        public string Rate_Excise_Duty { get; set; }
        public string Excise_Duty_Rate { get; set; }
        public string Rate_unit { get; set; }
        public string Condition_pricing_unit { get; set; }
        public string Condition_unit { get; set; }
    }
}