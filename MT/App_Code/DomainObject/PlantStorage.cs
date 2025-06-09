using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accenture.MWT.DomainObject
{
    /// <summary>
    /// Summary description for PlantStorage
    /// </summary>
    public class PlantStorage : Base
    {
        public int Mat_Plant_Storage_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Storage_bin { get; set; }
        public string Storage_Condition { get; set; }
        public string Temperature_Cond_Ind { get; set; }
        public string Hazardous_Mat_No { get; set; }
        public string Max_Storage_Period { get; set; }
        public string Unit_Max_Storage_Period { get; set; }
        public string Min_Remaining_Shelf_Life { get; set; }
        public string Total_Shelf_Life_Days { get; set; }
        public string Storage_Perc { get; set; }
        public string Negative_Stock_Allowed { get; set; }
        public string CC_Indicator_Fixed { get; set; }
        public string Rnding_Rule_Calc_SLED { get; set; }
        public string Period_Ind_Shelf_Life_Exp_Dt { get; set; }
        public string Label_Type { get; set; }
        public string Label_Form { get; set; }
        public string Profit_Center { get; set; }
        public int IsActive { get; set; }
        //CTRL_SUB_SDT06062019
        public string sTypeofChemical { get; set; }
        //SDT31072019
        public string sIsMatCtrlSub { get; set; }

    }
}