using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Classification
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class Classification : Base
    {
        public int Mat_Classification_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Class_Type { get; set; }
        public string Class { get; set; }

        public string Strength_of_mat_Pack_type { get; set; }
        public string Market { get; set; }
        public string NDC_No_LPI { get; set; }
        public string NDC_No_LL { get; set; }
        public string HTS { get; set; }
        public string ANDA { get; set; }
        public string FDA_No { get; set; }
        public string LPI_Material_Identifier { get; set; }
        public string Material_Grouping_for_MES { get; set; }
        public string Short_description_for_3PL { get; set; }
        public string Package_Presentation_3PL { get; set; }
        public string Number_of_Tablet_3PL { get; set; }
        public string Material_Category_A_3PL { get; set; }
        public string Material_Category_B_3PL { get; set; }
        public string Sorting_for_inventory_report { get; set; }
        public string Pack_size { get; set; }
        public string Product_Group { get; set; }
        public string DRUG_CATEGORY { get; set; }
        public string MARKET_ENTRY_DATE { get; set; }
        public string PZN_HORMOSAN { get; set; }
        public string StorageCondition { get; set; }

        public string Allowed_Manufacturers { get; set; }
        public string HSAN_MATERIAL_IDENTIFIER { get; set; }
        public string Expiration_date_shelf_life { get; set; }
        public string Next_Insp_Date_for_Batch { get; set; }
        public string Batch_number { get; set; }
        public string ASSAY_ASIS { get; set; }
        public string MANUFACTURER { get; set; }
        public string Potency_as_is_basis { get; set; }
        public string Loss_on_Drying { get; set; }
        public string Potency_as_is_basis1 { get; set; }
        public string RM402217 { get; set; }
        public string RM323350 { get; set; }
        public string SF110063 { get; set; }
        public string SF900052 { get; set; }
        public string IP4A0047 { get; set; }
        public string Assay_by_GC { get; set; }
        public string External_Material_Group { get; set; }
        public string Version_Number { get; set; }

        //PROV-CCP-MM-941-23-0045 Start
        public string sKXSBU { get; set; }
        public string sKXMARKT { get; set; }
        public string sKXSELLCTRY { get; set; }
        public string sKXBUSI { get; set; }
        public string sKXDIV { get; set; }
        public string sKXTHER { get; set; }
        public string sKXDOSFRM { get; set; }
        public string sKXMINSL { get; set; }
        public string sMKTMNGER { get; set; }
        public string sCS_MOLECULE { get; set; }
        public string sMGRPPX { get; set; }
        //PROV-CCP-MM-941-23-0045 End


        public int IsActive { get; set; }
    }
}