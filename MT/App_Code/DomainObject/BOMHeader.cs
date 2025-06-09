using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOMHeader
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class BOMHeader : Base
    {
        public int BOM_HeaderID { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Number { get; set; }
        public string Material_Desc { get; set; }
        public string Plant_Id { get; set; }
        public string BOM_Usage { get; set; }
        public string AlternativeBOM { get; set; }
        public string BOMText { get; set; }
        public string AlternativeText { get; set; }
        public string BaseQty { get; set; }
        public string BaseUOM { get; set; }
        public string BOMStatus { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int IsActive { get; set; }

        public string Remarks { get; set; }
        public string Reason { get; set; }
    }

    public class BOMDetail : Base
    {
        public int BOM_HeaderID { get; set; }
        public int BOM_HeaderDetail_Id { get; set; }
        public string Postion_No { get; set; }
        public string Item_Category { get; set; }
        public string Component { get; set; }
        public string Component_desc { get; set; }
        public string Quantity { get; set; }
        public string Component_UOM { get; set; }
        public string Comp_SortString { get; set; }
        public string Qty_Is_Fixed1 { get; set; }
        public string Spare_Part_Indicator { get; set; }
        public string StorageLocation { get; set; }
        public string Alt_Item_Group { get; set; }
        public string Priority { get; set; }
        public string starategy { get; set; }
        public string Usage_Probebilty { get; set; }
        public string Relevancy_To_Costing { get; set; }
        public string Remarks { get; set; }
        public string ASM { get; set; }
        public string Phantom_Indicator { get; set; }
        public string Component_Scrap { get; set; }
        public string RecursiveBOM { get; set; }
        public string Valid_From { get; set; }
        public string Valid_to { get; set; }
        public string BOM_Item_Text1 { get; set; }
        public string BOM_Item_Text2 { get; set; }
        public string ActiveFiller { get; set; }
        public string Combination { get; set; }
        public string Upd_Flag { get; set; }
        public string ItemNode { get; set; }
        public int IsActive { get; set; }
    }

    public class BOMPlantMapping : Base
    {
        public int BOM_HeaderID { get; set; }
        public int BOM_PlantMapping_Id { get; set; }
        public int Plant_Id { get; set; }

    }

    public class BOMRecipeChange : Base
    {
        public int BRChangeId { get; set; }
        public int Master_Header_Id { get; set; }
        public string Remarks { get; set; }
    }
}