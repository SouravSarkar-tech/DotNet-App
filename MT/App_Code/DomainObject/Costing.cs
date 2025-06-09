using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Costing
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class Costing1 : Base
    {
        public int Mat_Costing1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }

        public string Profit_Center { get; set; }
        public string Origin_Group { get; set; }
        public string Material_Related_Origin { get; set; }
        public string Alternative_BOM { get; set; }
        public string BOM_Usage { get; set; }
        public string Key_Task_List_Grp { get; set; }
        public string Group_Counter { get; set; }
        public string Task_List_Type { get; set; }
        public string Lot_Size_Prd_Cost_Est { get; set; }
        public string Spl_Procurement_Type { get; set; }
        public string Costing_Overhead_Grp { get; set; }
        public string Is_Mat_Costed_Qnty_Struc { get; set; }
        public string Variance_Key { get; set; }
        public string Do_Not_Cost { get; set; }
        public int IsActive { get; set; }
    }

    public class Costing2 : Base
    {
        public int Mat_Costing2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Planned_Price1 { get; set; }
        public string Planned_Price_Date1 { get; set; }
        public string Planned_Price2 { get; set; }
        public string Planned_Price_Date2 { get; set; }
        public string Planned_Price3 { get; set; }
        public string Planned_Price_Date3 { get; set; }
        public int IsActive { get; set; }
    }
}