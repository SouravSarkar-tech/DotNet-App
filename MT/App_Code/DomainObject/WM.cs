using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Accenture.MWT.DomainObject
{
    public class WareHouseMgmt1 : Base
    {
        public int Mat_WareHouse_Mgmt1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Warehouse_ID { get; set; }
        public string Storage_Type_ID { get; set; }
        public string Capacity_Usage { get; set; }
        public string Capacity_Unit { get; set; }
        public string WM_Unit_Measure { get; set; }
        public string Is_Msg_Inventory_Mgmt { get; set; }
        public string Storage_Section_Ind { get; set; }
        public string Stor_Type_Ind_Stock_Placement { get; set; }
        public string Stor_Type_Ind_Stock_Removal { get; set; }
        public string Is_Allow_Add_Exist_Stock { get; set; }
        public string Bulk_Storage_Ind { get; set; }
        public int IsActive { get; set; }
    }

    public class WareHouseMgmt2 : Base
    {
        public int Mat_WareHouse_Mgmt2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Warehouse_ID { get; set; }
        public string Storage_Type_ID { get; set; }
        public string Loading_Equipment_Quantity { get; set; }
        public string Loading_Equipment_Quantity1 { get; set; }
        public string Loading_Equipment_Quantity2 { get; set; }
        public string Unit_Loading_Equip_Quan { get; set; }
        public string Unit_Loading_Equip_Quan1 { get; set; }
        public string Unit_Loading_Equip_Quan2 { get; set; }
        public string Storage_Unit_Type { get; set; }
        public string Storage_Unit_Type1 { get; set; }
        public string Storage_Unit_Type2 { get; set; }
        public int IsActive { get; set; }
    }
}