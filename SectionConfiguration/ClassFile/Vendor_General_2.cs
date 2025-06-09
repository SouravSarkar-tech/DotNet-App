using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region Class_Vendor_General_2namespace SectionConfiguration{	public class Vendor_General_2	{		public object GetClass(string str)		{			object Obj;			switch (str)			{				case "VAL001MNFR":					Obj = new SectionConfiguration.VAL001MNFR.Vendor_General_2();					return Obj;				case "VAL001VMC":					Obj = new SectionConfiguration.VAL001VMC.Vendor_General_2();					return Obj;				case "VAL001Z001":					Obj = new SectionConfiguration.VAL001Z001.Vendor_General_2();					return Obj;				case "VAL001Z002":					Obj = new SectionConfiguration.VAL001Z002.Vendor_General_2();					return Obj;				case "VAL001Z003":					Obj = new SectionConfiguration.VAL001Z003.Vendor_General_2();					return Obj;				case "VAL001Z004":					Obj = new SectionConfiguration.VAL001Z004.Vendor_General_2();					return Obj;				case "VAL001Z005":					Obj = new SectionConfiguration.VAL001Z005.Vendor_General_2();					return Obj;				case "VAL001Z007":					Obj = new SectionConfiguration.VAL001Z007.Vendor_General_2();					return Obj;				case "VAL001Z008":					Obj = new SectionConfiguration.VAL001Z008.Vendor_General_2();					return Obj;				case "VAL001Z009":					Obj = new SectionConfiguration.VAL001Z009.Vendor_General_2();					return Obj;				case "VAL001Z010":					Obj = new SectionConfiguration.VAL001Z010.Vendor_General_2();					return Obj;				case "VAL001Z011":					Obj = new SectionConfiguration.VAL001Z011.Vendor_General_2();					return Obj;				case "VAL001Z068":					Obj = new SectionConfiguration.VAL001Z068.Vendor_General_2();					return Obj;				case "VCL001MNFR":					Obj = new SectionConfiguration.VCL001MNFR.Vendor_General_2();					return Obj;				case "VCL001VMC":					Obj = new SectionConfiguration.VCL001VMC.Vendor_General_2();					return Obj;				case "VCL001Z001":					Obj = new SectionConfiguration.VCL001Z001.Vendor_General_2();					return Obj;				case "VCL001Z002":					Obj = new SectionConfiguration.VCL001Z002.Vendor_General_2();					return Obj;				case "VCL001Z003":					Obj = new SectionConfiguration.VCL001Z003.Vendor_General_2();					return Obj;				case "VCL001Z004":					Obj = new SectionConfiguration.VCL001Z004.Vendor_General_2();					return Obj;				case "VCL001Z005":					Obj = new SectionConfiguration.VCL001Z005.Vendor_General_2();					return Obj;				case "VCL001Z007":					Obj = new SectionConfiguration.VCL001Z007.Vendor_General_2();					return Obj;				case "VCL001Z008":					Obj = new SectionConfiguration.VCL001Z008.Vendor_General_2();					return Obj;				case "VCL001Z009":					Obj = new SectionConfiguration.VCL001Z009.Vendor_General_2();					return Obj;				case "VCL001Z010":					Obj = new SectionConfiguration.VCL001Z010.Vendor_General_2();					return Obj;				case "VCL001Z011":					Obj = new SectionConfiguration.VCL001Z011.Vendor_General_2();					return Obj;				case "VCL001Z068":					Obj = new SectionConfiguration.VCL001Z068.Vendor_General_2();					return Obj;				//8400000388 Start
				case "VCL001Z093":
					Obj = new SectionConfiguration.VCL001Z093.Vendor_General_2();
					return Obj;
				case "VAL001Z093":
					Obj = new SectionConfiguration.VAL001Z093.Vendor_General_2();
					return Obj;
				//8400000388 End
				default:					return null;			}		}	}}#endregion#region 8400000388
#region Class_Vendor_General_2_VAL001Z093
namespace SectionConfiguration.VAL001Z093
{
	public class Vendor_General_2
	{


		#region Properties
		public static int ddlVendorClass
		{
			get { return 1; }
		}

		public static int txtExciseRegistrationNo
		{
			get { return 2; }
		}

		public static int txtECCNumber
		{
			get { return 2; }
		}

		public static int txtExciseCommissionerate
		{
			get { return 2; }
		}

		public static int txtExciseRange
		{
			get { return 2; }
		}

		public static int txtExciseDivision
		{
			get { return 2; }
		}

		public static int txtGSTNo
		{
			get { return 2; }
		}

		public static int txtTax_Numbe_3
		{
			get { return 2; }
		}

		public static int txtTaxNumber2
		{
			get { return 2; }
		}

		public static int txtTaxNumber1
		{
			get { return 2; }
		}

		public static int txtTax_Number_5
		{
			get { return 2; }
		}

		public static int txtTax_Numbe_4
		{
			get { return 2; }
		}

		public static int txtType_Industry
		{
			get { return 2; }
		}

		public static int txtTypeOfBusiness
		{
			get { return 2; }
		}

		public static int txtTransportation_Zone_Goods
		{
			get { return 3; }
		}

		public static int txtVAT_Registration_Number
		{
			get { return 3; }
		}

		public static int txtVendorIndicator_Relevant
		{
			get { return 3; }
		}

		public static int txtTax_Type
		{
			get { return 3; }
		}

		public static int txtTax_Number_Type
		{
			get { return 3; }
		}

		public static int txtTransportation_Chain
		{
			get { return 3; }
		}

		public static int txtTitle
		{
			get { return 3; }
		}

		public static int txtTax_Jurisdiction
		{
			get { return 3; }
		}

		public static int txtStagingTime_Days_BatchInput
		{
			get { return 3; }
		}

		public static int txtPlant
		{
			get { return 3; }
		}

		public static int txtService_AgentProcedure_Group
		{
			get { return 3; }
		}

		public static int txtPlaceBirth_WithholdingTax
		{
			get { return 3; }
		}

		public static int txtName_Representative
		{
			get { return 3; }
		}

		public static int txtFirst_Name
		{
			get { return 3; }
		}

		public static int txtKeySex_PersonWithholding_Tax
		{
			get { return 3; }
		}

		public static int txtIndustry_key
		{
			get { return 3; }
		}

		public static int txtName_3
		{
			get { return 3; }
		}

		public static int txtName_2
		{
			get { return 3; }
		}

		public static int txtName_1
		{
			get { return 3; }
		}

		public static int txtFactoryCalendar_key
		{
			get { return 3; }
		}

		public static int txtExternal_Manufacturer_CodeNumber
		{
			get { return 3; }
		}

		public static int txtDateBatch_Input2
		{
			get { return 3; }
		}

		public static int txtDateBatch_Input
		{
			get { return 3; }
		}

		public static int chkTax_Split
		{
			get { return 3; }
		}

		public static int chkScheduling_Procedure
		{
			get { return 3; }
		}

		public static int chkCrossDocking_Relevant_CollectiveNumbering
		{
			get { return 3; }
		}

		public static int chkCentral_Deletion_MasterRecord
		{
			get { return 3; }
		}




		#endregion
    }

}








#endregion
#region Class_Vendor_General_2_VCL001Z093
namespace SectionConfiguration.VCL001Z093
{
	public class Vendor_General_2
	{


		#region Properties
		public static int ddlVendorClass
		{
			get { return 2; }
		}

		public static int txtECCNumber
		{
			get { return 2; }
		}

		public static int txtGSTNo
		{
			get { return 2; }
		}

		public static int txtExciseRegistrationNo
		{
			get { return 2; }
		}

		public static int txtExciseRange
		{
			get { return 2; }
		}

		public static int txtExciseDivision
		{
			get { return 2; }
		}

		public static int txtExciseCommissionerate
		{
			get { return 2; }
		}

		public static int txtTax_Numbe_3
		{
			get { return 2; }
		}

		public static int txtTax_Number_5
		{
			get { return 2; }
		}

		public static int txtTax_Numbe_4
		{
			get { return 2; }
		}

		public static int txtTaxNumber2
		{
			get { return 2; }
		}

		public static int txtTaxNumber1
		{
			get { return 2; }
		}

		public static int txtTypeOfBusiness
		{
			get { return 2; }
		}

		public static int txtType_Industry
		{
			get { return 2; }
		}

		public static int txtVAT_Registration_Number
		{
			get { return 3; }
		}

		public static int txtVendorIndicator_Relevant
		{
			get { return 3; }
		}

		public static int txtTax_Type
		{
			get { return 3; }
		}

		public static int txtTax_Number_Type
		{
			get { return 3; }
		}

		public static int txtTitle
		{
			get { return 3; }
		}

		public static int txtTransportation_Chain
		{
			get { return 3; }
		}

		public static int txtTransportation_Zone_Goods
		{
			get { return 3; }
		}

		public static int txtTax_Jurisdiction
		{
			get { return 3; }
		}

		public static int txtPlant
		{
			get { return 3; }
		}

		public static int txtStagingTime_Days_BatchInput
		{
			get { return 3; }
		}

		public static int txtService_AgentProcedure_Group
		{
			get { return 3; }
		}

		public static int txtIndustry_key
		{
			get { return 3; }
		}

		public static int txtName_1
		{
			get { return 3; }
		}

		public static int txtKeySex_PersonWithholding_Tax
		{
			get { return 3; }
		}

		public static int txtName_2
		{
			get { return 3; }
		}

		public static int txtName_3
		{
			get { return 3; }
		}

		public static int txtPlaceBirth_WithholdingTax
		{
			get { return 3; }
		}

		public static int txtName_Representative
		{
			get { return 3; }
		}

		public static int txtFirst_Name
		{
			get { return 3; }
		}

		public static int txtFactoryCalendar_key
		{
			get { return 3; }
		}

		public static int txtExternal_Manufacturer_CodeNumber
		{
			get { return 3; }
		}

		public static int txtDateBatch_Input2
		{
			get { return 3; }
		}

		public static int txtDateBatch_Input
		{
			get { return 3; }
		}

		public static int chkTax_Split
		{
			get { return 3; }
		}

		public static int chkScheduling_Procedure
		{
			get { return 3; }
		}

		public static int chkCrossDocking_Relevant_CollectiveNumbering
		{
			get { return 3; }
		}

		public static int chkCentral_Deletion_MasterRecord
		{
			get { return 3; }
		}




		#endregion
    }

}


#endregion
#endregion
#region Class_Vendor_General_2_VAL001MNFRnamespace SectionConfiguration.VAL001MNFR{	public class Vendor_General_2	{		#region Properties		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 1; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 1; }		}		public static int chkScheduling_Procedure		{ 			get { return 1; }		}		public static int chkTax_Split		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtDateBatch_Input		{ 			get { return 1; }		}		public static int txtDateBatch_Input2		{ 			get { return 1; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 1; }		}		public static int txtFactoryCalendar_key		{ 			get { return 1; }		}		public static int txtFirst_Name		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtIndustry_key		{ 			get { return 1; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 1; }		}		public static int txtName_1		{ 			get { return 1; }		}		public static int txtName_2		{ 			get { return 1; }		}		public static int txtName_3		{ 			get { return 1; }		}		public static int txtName_Representative		{ 			get { return 1; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 1; }		}		public static int txtPlant		{ 			get { return 1; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 1; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 1; }		}		public static int txtTax_Jurisdiction		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int txtTax_Numbe_4		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTax_Number_Type		{ 			get { return 1; }		}		public static int txtTax_Type		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTaxNumber2		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtTransportation_Chain		{ 			get { return 1; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 1; }		}		public static int txtType_Industry		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtVAT_Registration_Number		{ 			get { return 1; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 1; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001VMCnamespace SectionConfiguration.VAL001VMC{	public class Vendor_General_2	{		#region Properties		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 1; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 1; }		}		public static int chkScheduling_Procedure		{ 			get { return 1; }		}		public static int chkTax_Split		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtDateBatch_Input		{ 			get { return 1; }		}		public static int txtDateBatch_Input2		{ 			get { return 1; }		}		public static int txtECCNumber		{ 			get { return 1; }		}		public static int txtExciseCommissionerate		{ 			get { return 1; }		}		public static int txtExciseDivision		{ 			get { return 1; }		}		public static int txtExciseRange		{ 			get { return 1; }		}		public static int txtExciseRegistrationNo		{ 			get { return 1; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 1; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 1; }		}		public static int txtTax_Jurisdiction		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int txtName_3		{ 			get { return 1; }		}		public static int txtName_Representative		{ 			get { return 1; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 1; }		}		public static int txtPlant		{ 			get { return 1; }		}		public static int txtIndustry_key		{ 			get { return 1; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 1; }		}		public static int txtName_1		{ 			get { return 1; }		}		public static int txtName_2		{ 			get { return 1; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 1; }		}		public static int txtFactoryCalendar_key		{ 			get { return 1; }		}		public static int txtFirst_Name		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 1; }		}		public static int txtType_Industry		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtVAT_Registration_Number		{ 			get { return 1; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTaxNumber2		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtTransportation_Chain		{ 			get { return 1; }		}		public static int txtTax_Numbe_4		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTax_Number_Type		{ 			get { return 1; }		}		public static int txtTax_Type		{ 			get { return 1; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z001namespace SectionConfiguration.VAL001Z001{	public class Vendor_General_2	{		#region Properties		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z002namespace SectionConfiguration.VAL001Z002{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z003namespace SectionConfiguration.VAL001Z003{	public class Vendor_General_2	{		#region Properties		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z004namespace SectionConfiguration.VAL001Z004{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z005namespace SectionConfiguration.VAL001Z005{	public class Vendor_General_2	{		#region Properties		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z007namespace SectionConfiguration.VAL001Z007{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z008namespace SectionConfiguration.VAL001Z008{	public class Vendor_General_2	{		#region Properties		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z009namespace SectionConfiguration.VAL001Z009{	public class Vendor_General_2	{		#region Properties		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z010namespace SectionConfiguration.VAL001Z010{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z011namespace SectionConfiguration.VAL001Z011{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VAL001Z068namespace SectionConfiguration.VAL001Z068{	public class Vendor_General_2	{		#region Properties		public static int txtTypeOfBusiness		{ 			get { return 1; }		}		public static int txtTax_Number_5		{ 			get { return 1; }		}		public static int txtTaxNumber1		{ 			get { return 1; }		}		public static int txtGSTNo		{ 			get { return 1; }		}		public static int txtTax_Numbe_3		{ 			get { return 1; }		}		public static int ddlVendorClass		{ 			get { return 1; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001MNFRnamespace SectionConfiguration.VCL001MNFR{	public class Vendor_General_2	{		#region Properties		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 2; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 2; }		}		public static int chkScheduling_Procedure		{ 			get { return 2; }		}		public static int chkTax_Split		{ 			get { return 2; }		}		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtDateBatch_Input		{ 			get { return 2; }		}		public static int txtDateBatch_Input2		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 2; }		}		public static int txtFactoryCalendar_key		{ 			get { return 2; }		}		public static int txtFirst_Name		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtIndustry_key		{ 			get { return 2; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 2; }		}		public static int txtName_1		{ 			get { return 2; }		}		public static int txtName_2		{ 			get { return 2; }		}		public static int txtName_3		{ 			get { return 2; }		}		public static int txtName_Representative		{ 			get { return 2; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 2; }		}		public static int txtPlant		{ 			get { return 2; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 2; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 2; }		}		public static int txtTax_Jurisdiction		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Number_Type		{ 			get { return 2; }		}		public static int txtTax_Type		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtTransportation_Chain		{ 			get { return 2; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 2; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 2; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001VMCnamespace SectionConfiguration.VCL001VMC{	public class Vendor_General_2	{		#region Properties		public static int txtTransportation_Zone_Goods		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 2; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtTransportation_Chain		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Number_Type		{ 			get { return 2; }		}		public static int txtTax_Type		{ 			get { return 2; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 2; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 2; }		}		public static int txtTax_Jurisdiction		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtName_3		{ 			get { return 2; }		}		public static int txtName_Representative		{ 			get { return 2; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 2; }		}		public static int txtPlant		{ 			get { return 2; }		}		public static int txtIndustry_key		{ 			get { return 2; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 2; }		}		public static int txtName_1		{ 			get { return 2; }		}		public static int txtName_2		{ 			get { return 2; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 2; }		}		public static int txtFactoryCalendar_key		{ 			get { return 2; }		}		public static int txtFirst_Name		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtDateBatch_Input		{ 			get { return 2; }		}		public static int txtDateBatch_Input2		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 2; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 2; }		}		public static int chkScheduling_Procedure		{ 			get { return 2; }		}		public static int chkTax_Split		{ 			get { return 2; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z001namespace SectionConfiguration.VCL001Z001{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z002namespace SectionConfiguration.VCL001Z002{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z003namespace SectionConfiguration.VCL001Z003{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z004namespace SectionConfiguration.VCL001Z004{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z005namespace SectionConfiguration.VCL001Z005{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z007namespace SectionConfiguration.VCL001Z007{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z008namespace SectionConfiguration.VCL001Z008{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z009namespace SectionConfiguration.VCL001Z009{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z010namespace SectionConfiguration.VCL001Z010{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z011namespace SectionConfiguration.VCL001Z011{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_2_VCL001Z068namespace SectionConfiguration.VCL001Z068{	public class Vendor_General_2	{		#region Properties		public static int ddlVendorClass		{ 			get { return 2; }		}		public static int txtECCNumber		{ 			get { return 2; }		}		public static int txtExciseCommissionerate		{ 			get { return 2; }		}		public static int txtGSTNo		{ 			get { return 2; }		}		public static int txtExciseRegistrationNo		{ 			get { return 2; }		}		public static int txtExciseRange		{ 			get { return 2; }		}		public static int txtExciseDivision		{ 			get { return 2; }		}		public static int txtTax_Numbe_3		{ 			get { return 2; }		}		public static int txtTax_Number_5		{ 			get { return 2; }		}		public static int txtTax_Numbe_4		{ 			get { return 2; }		}		public static int txtTaxNumber2		{ 			get { return 2; }		}		public static int txtTaxNumber1		{ 			get { return 2; }		}		public static int txtTypeOfBusiness		{ 			get { return 2; }		}		public static int txtType_Industry		{ 			get { return 2; }		}		public static int txtVAT_Registration_Number		{ 			get { return 3; }		}		public static int txtVendorIndicator_Relevant		{ 			get { return 3; }		}		public static int txtTax_Type		{ 			get { return 3; }		}		public static int txtTax_Number_Type		{ 			get { return 3; }		}		public static int txtTransportation_Zone_Goods		{ 			get { return 3; }		}		public static int txtTransportation_Chain		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtPlant		{ 			get { return 3; }		}		public static int txtService_AgentProcedure_Group		{ 			get { return 3; }		}		public static int txtTax_Jurisdiction		{ 			get { return 3; }		}		public static int txtStagingTime_Days_BatchInput		{ 			get { return 3; }		}		public static int txtKeySex_PersonWithholding_Tax		{ 			get { return 3; }		}		public static int txtIndustry_key		{ 			get { return 3; }		}		public static int txtName_2		{ 			get { return 3; }		}		public static int txtName_1		{ 			get { return 3; }		}		public static int txtName_3		{ 			get { return 3; }		}		public static int txtPlaceBirth_WithholdingTax		{ 			get { return 3; }		}		public static int txtName_Representative		{ 			get { return 3; }		}		public static int txtFirst_Name		{ 			get { return 3; }		}		public static int txtFactoryCalendar_key		{ 			get { return 3; }		}		public static int txtExternal_Manufacturer_CodeNumber		{ 			get { return 3; }		}		public static int txtDateBatch_Input2		{ 			get { return 3; }		}		public static int txtDateBatch_Input		{ 			get { return 3; }		}		public static int chkTax_Split		{ 			get { return 3; }		}		public static int chkScheduling_Procedure		{ 			get { return 3; }		}		public static int chkCrossDocking_Relevant_CollectiveNumbering		{ 			get { return 3; }		}		public static int chkCentral_Deletion_MasterRecord		{ 			get { return 3; }		}		#endregion	}}#endregion
