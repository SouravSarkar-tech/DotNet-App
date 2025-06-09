using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region Class_Material_Extension_Data

namespace SectionConfiguration
{
	public class Material_Extension_Data
	{

		public object GetClass(string str)
		{
			object Obj;
			switch (str)
			{
				case "MACHGERSA":
					Obj = new SectionConfiguration.MACHGERSA.Material_Extension_Data();
					return Obj;
				case "MACHGFERT":
					Obj = new SectionConfiguration.MACHGFERT.Material_Extension_Data();
					return Obj;
				case "MACHGHALB":
					Obj = new SectionConfiguration.MACHGHALB.Material_Extension_Data();
					return Obj;
				case "MACHGHAWA":
					Obj = new SectionConfiguration.MACHGHAWA.Material_Extension_Data();
					return Obj;
				case "MACHGHIBE":
					Obj = new SectionConfiguration.MACHGHIBE.Material_Extension_Data();
					return Obj;
				case "MACHGMEXT":
					Obj = new SectionConfiguration.MACHGMEXT.Material_Extension_Data();
					return Obj;
				case "MACHGMMC":
					Obj = new SectionConfiguration.MACHGMMC.Material_Extension_Data();
					return Obj;
				case "MACHGPROM":
					Obj = new SectionConfiguration.MACHGPROM.Material_Extension_Data();
					return Obj;
				case "MACHGROH":
					Obj = new SectionConfiguration.MACHGROH.Material_Extension_Data();
					return Obj;
				case "MACHGUNBW":
					Obj = new SectionConfiguration.MACHGUNBW.Material_Extension_Data();
					return Obj;
				case "MACHGVERP":
					Obj = new SectionConfiguration.MACHGVERP.Material_Extension_Data();
					return Obj;
				case "MACHGZMBW":
					Obj = new SectionConfiguration.MACHGZMBW.Material_Extension_Data();
					return Obj;
				case "MACHGZNBW":
					Obj = new SectionConfiguration.MACHGZNBW.Material_Extension_Data();
					return Obj;
				case "MAMFGERSA":
					Obj = new SectionConfiguration.MAMFGERSA.Material_Extension_Data();
					return Obj;
				case "MAMFGFERT":
					Obj = new SectionConfiguration.MAMFGFERT.Material_Extension_Data();
					return Obj;
				case "MAMFGHALB":
					Obj = new SectionConfiguration.MAMFGHALB.Material_Extension_Data();
					return Obj;
				case "MAMFGHAWA":
					Obj = new SectionConfiguration.MAMFGHAWA.Material_Extension_Data();
					return Obj;
				case "MAMFGHIBE":
					Obj = new SectionConfiguration.MAMFGHIBE.Material_Extension_Data();
					return Obj;
				case "MAMFGMEXT":
					Obj = new SectionConfiguration.MAMFGMEXT.Material_Extension_Data();
					return Obj;
				case "MAMFGMMC":
					Obj = new SectionConfiguration.MAMFGMMC.Material_Extension_Data();
					return Obj;
				case "MAMFGPROM":
					Obj = new SectionConfiguration.MAMFGPROM.Material_Extension_Data();
					return Obj;
				case "MAMFGROH":
					Obj = new SectionConfiguration.MAMFGROH.Material_Extension_Data();
					return Obj;
				case "MAMFGUNBW":
					Obj = new SectionConfiguration.MAMFGUNBW.Material_Extension_Data();
					return Obj;
				case "MAMFGVERP":
					Obj = new SectionConfiguration.MAMFGVERP.Material_Extension_Data();
					return Obj;
				case "MAMFGZMBW":
					Obj = new SectionConfiguration.MAMFGZMBW.Material_Extension_Data();
					return Obj;
				case "MAMFGZNBW":
					Obj = new SectionConfiguration.MAMFGZNBW.Material_Extension_Data();
					return Obj;
				case "MCCHGERSA":
					Obj = new SectionConfiguration.MCCHGERSA.Material_Extension_Data();
					return Obj;
				case "MCCHGFERT":
					Obj = new SectionConfiguration.MCCHGFERT.Material_Extension_Data();
					return Obj;
				case "MCCHGHALB":
					Obj = new SectionConfiguration.MCCHGHALB.Material_Extension_Data();
					return Obj;
				case "MCCHGHAWA":
					Obj = new SectionConfiguration.MCCHGHAWA.Material_Extension_Data();
					return Obj;
				case "MCCHGHIBE":
					Obj = new SectionConfiguration.MCCHGHIBE.Material_Extension_Data();
					return Obj;
				case "MCCHGMEXT":
					Obj = new SectionConfiguration.MCCHGMEXT.Material_Extension_Data();
					return Obj;
				case "MCCHGMMC":
					Obj = new SectionConfiguration.MCCHGMMC.Material_Extension_Data();
					return Obj;
				case "MCCHGPROM":
					Obj = new SectionConfiguration.MCCHGPROM.Material_Extension_Data();
					return Obj;
				case "MCCHGROH":
					Obj = new SectionConfiguration.MCCHGROH.Material_Extension_Data();
					return Obj;
				case "MCCHGUNBW":
					Obj = new SectionConfiguration.MCCHGUNBW.Material_Extension_Data();
					return Obj;
				case "MCCHGVERP":
					Obj = new SectionConfiguration.MCCHGVERP.Material_Extension_Data();
					return Obj;
				case "MCCHGZMBW":
					Obj = new SectionConfiguration.MCCHGZMBW.Material_Extension_Data();
					return Obj;
				case "MCCHGZNBW":
					Obj = new SectionConfiguration.MCCHGZNBW.Material_Extension_Data();
					return Obj;
				case "MCMFGERSA":
					Obj = new SectionConfiguration.MCMFGERSA.Material_Extension_Data();
					return Obj;
				case "MCMFGFERT":
					Obj = new SectionConfiguration.MCMFGFERT.Material_Extension_Data();
					return Obj;
				case "MCMFGHALB":
					Obj = new SectionConfiguration.MCMFGHALB.Material_Extension_Data();
					return Obj;
				case "MCMFGHAWA":
					Obj = new SectionConfiguration.MCMFGHAWA.Material_Extension_Data();
					return Obj;
				case "MCMFGHIBE":
					Obj = new SectionConfiguration.MCMFGHIBE.Material_Extension_Data();
					return Obj;
				case "MCMFGMEXT":
					Obj = new SectionConfiguration.MCMFGMEXT.Material_Extension_Data();
					return Obj;
				case "MCMFGMMC":
					Obj = new SectionConfiguration.MCMFGMMC.Material_Extension_Data();
					return Obj;
				case "MCMFGPROM":
					Obj = new SectionConfiguration.MCMFGPROM.Material_Extension_Data();
					return Obj;
				case "MCMFGROH":
					Obj = new SectionConfiguration.MCMFGROH.Material_Extension_Data();
					return Obj;
				case "MCMFGUNBW":
					Obj = new SectionConfiguration.MCMFGUNBW.Material_Extension_Data();
					return Obj;
				case "MCMFGVERP":
					Obj = new SectionConfiguration.MCMFGVERP.Material_Extension_Data();
					return Obj;
				case "MCMFGZMBW":
					Obj = new SectionConfiguration.MCMFGZMBW.Material_Extension_Data();
					return Obj;
				case "MCMFGZNBW":
					Obj = new SectionConfiguration.MCMFGZNBW.Material_Extension_Data();
					return Obj;
				default:
					return null;
			}
		}
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGERSA

namespace SectionConfiguration.MACHGERSA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGFERT

namespace SectionConfiguration.MACHGFERT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGHALB

namespace SectionConfiguration.MACHGHALB
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGHAWA

namespace SectionConfiguration.MACHGHAWA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGHIBE

namespace SectionConfiguration.MACHGHIBE
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGMEXT

namespace SectionConfiguration.MACHGMEXT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGMMC

namespace SectionConfiguration.MACHGMMC
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGPROM

namespace SectionConfiguration.MACHGPROM
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGROH

namespace SectionConfiguration.MACHGROH
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGUNBW

namespace SectionConfiguration.MACHGUNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlLotSize
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGVERP

namespace SectionConfiguration.MACHGVERP
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlStorageUnitType2
		{ 
			get { return 1; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 1; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 1; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 1; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 1; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 1; }
		}

		public static int ddlWarehouse
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 1; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 1; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 1; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 1; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 1; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 1; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 1; }
		}

		public static int ddlStorageType
		{ 
			get { return 1; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 1; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 1; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlInspectionType
		{
            //get { return 1; }
            //MAT_DT26102020
            get { return 2; }
        }

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGZMBW

namespace SectionConfiguration.MACHGZMBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MACHGZNBW

namespace SectionConfiguration.MACHGZNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGERSA

namespace SectionConfiguration.MAMFGERSA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{
            //get { return 2; }
            //MAT_DT26102020
            get { return 3; }
        }

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGFERT

namespace SectionConfiguration.MAMFGFERT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGHALB

namespace SectionConfiguration.MAMFGHALB
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGHAWA

namespace SectionConfiguration.MAMFGHAWA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 1; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGHIBE

namespace SectionConfiguration.MAMFGHIBE
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGMEXT

namespace SectionConfiguration.MAMFGMEXT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGMMC

namespace SectionConfiguration.MAMFGMMC
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGPROM

namespace SectionConfiguration.MAMFGPROM
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGROH

namespace SectionConfiguration.MAMFGROH
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlInspectionType
		{
            //get { return 1; }
            //MAT_DT26102020
            get { return 2; }
        }

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGUNBW

namespace SectionConfiguration.MAMFGUNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGVERP

namespace SectionConfiguration.MAMFGVERP
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 1; }
		}

		public static int ddlInspectionType
		{
            //get { return 1; }
            //MAT_DT26102020
            get { return 2; }
        }

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 1; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 1; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGZMBW

namespace SectionConfiguration.MAMFGZMBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MAMFGZNBW

namespace SectionConfiguration.MAMFGZNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlMrpType
		{ 
			get { return 1; }
		}

		public static int ddlPlant
		{ 
			get { return 1; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 1; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 1; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 1; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 1; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 1; }
		}

		public static int ddlValuationClass
		{ 
			get { return 1; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGERSA

namespace SectionConfiguration.MCCHGERSA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGFERT

namespace SectionConfiguration.MCCHGFERT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGHALB

namespace SectionConfiguration.MCCHGHALB
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGHAWA

namespace SectionConfiguration.MCCHGHAWA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGHIBE

namespace SectionConfiguration.MCCHGHIBE
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGMEXT

namespace SectionConfiguration.MCCHGMEXT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGMMC

namespace SectionConfiguration.MCCHGMMC
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGPROM

namespace SectionConfiguration.MCCHGPROM
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGROH

namespace SectionConfiguration.MCCHGROH
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGUNBW

namespace SectionConfiguration.MCCHGUNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGVERP

namespace SectionConfiguration.MCCHGVERP
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 2; }
		}

		public static int ddlStorageType
		{ 
			get { return 2; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 2; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 2; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 2; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 2; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 2; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 2; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 2; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGZMBW

namespace SectionConfiguration.MCCHGZMBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCCHGZNBW

namespace SectionConfiguration.MCCHGZNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGERSA

namespace SectionConfiguration.MCMFGERSA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGFERT

namespace SectionConfiguration.MCMFGFERT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGHALB

namespace SectionConfiguration.MCMFGHALB
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlStorageUnitType3
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGHAWA

namespace SectionConfiguration.MCMFGHAWA
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGHIBE

namespace SectionConfiguration.MCMFGHIBE
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGMEXT

namespace SectionConfiguration.MCMFGMEXT
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGMMC

namespace SectionConfiguration.MCMFGMMC
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlInspectionType
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlPlant
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 3; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 3; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 3; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 3; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGPROM

namespace SectionConfiguration.MCMFGPROM
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGROH

namespace SectionConfiguration.MCMFGROH
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGUNBW

namespace SectionConfiguration.MCMFGUNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlValuationClass
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGVERP

namespace SectionConfiguration.MCMFGVERP
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGZMBW

namespace SectionConfiguration.MCMFGZMBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 3; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 3; }
		}

		public static int ddlMrpType
		{ 
			get { return 3; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 3; }
		}

		public static int ddlMrpController
		{ 
			get { return 3; }
		}

		public static int ddlLotSize
		{ 
			get { return 3; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 3; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 3; }
		}

		public static int txtMinLotSize
		{ 
			get { return 3; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 3; }
		}

		public static int txtReorder
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int ddlWarehouse
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int txtRoundingValue
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_Material_Extension_Data_MCMFGZNBW

namespace SectionConfiguration.MCMFGZNBW
{
	public class Material_Extension_Data
	{
		#region Properties

		public static int txtRoundingValue
		{ 
			get { return 2; }
		}

		public static int ddlValuationClass
		{ 
			get { return 2; }
		}

		public static int ddlWarehouse
		{ 
			get { return 2; }
		}

		public static int txtReorder
		{ 
			get { return 2; }
		}

		public static int txtPlannedDeleveryTime
		{ 
			get { return 2; }
		}

		public static int txtMaxLotSize
		{ 
			get { return 2; }
		}

		public static int txtMinLotSize
		{ 
			get { return 2; }
		}

		public static int txtFixedLotSize
		{ 
			get { return 2; }
		}

		public static int txtGRProcessingTime
		{ 
			get { return 2; }
		}

		public static int txtIntervalNPInspector
		{ 
			get { return 2; }
		}

		public static int ddlStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlSalesOrginization
		{ 
			get { return 2; }
		}

		public static int ddlSpecialProcType
		{ 
			get { return 2; }
		}

		public static int ddlPurchasingGroup
		{ 
			get { return 2; }
		}

		public static int ddlPriceControlIndicator
		{ 
			get { return 2; }
		}

		public static int ddlProfitCenter
		{ 
			get { return 2; }
		}

		public static int ddlMaterialPGroup
		{ 
			get { return 2; }
		}

		public static int ddlMrpController
		{ 
			get { return 2; }
		}

		public static int ddlMrpType
		{ 
			get { return 2; }
		}

		public static int ddlPlant
		{ 
			get { return 2; }
		}

		public static int ddlDistributionChannel
		{ 
			get { return 2; }
		}

		public static int ddlAccountAssignment
		{ 
			get { return 2; }
		}

		public static int ddlIssueStorageLocation
		{ 
			get { return 2; }
		}

		public static int ddlLotSize
		{ 
			get { return 2; }
		}

		public static int ddlInspectionType
		{ 
			get { return 2; }
		}

		public static int ddlStorageSectIndi
		{ 
			get { return 3; }
		}

		public static int ddlStorageType
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlaceRemoval
		{ 
			get { return 3; }
		}

		public static int ddlStorageTypeIndiSPlacement
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity2
		{ 
			get { return 3; }
		}

		public static int txtLoadingEquipQuantity
		{ 
			get { return 3; }
		}

		public static int txtloadingEquipQuantity3
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip3
		{ 
			get { return 3; }
		}

		public static int txtCapacityUsage
		{ 
			get { return 3; }
		}

		public static int ddlWareHouseMangUnit
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType3
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType
		{ 
			get { return 3; }
		}

		public static int ddlStorageUnitType2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip2
		{ 
			get { return 3; }
		}

		public static int ddlUnitMeasureLoadingEquip
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


