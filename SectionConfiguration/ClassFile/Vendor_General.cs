using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region Class_Vendor_Generalnamespace SectionConfiguration{	public class Vendor_General	{		public object GetClass(string str)		{			object Obj;			switch (str)			{				case "VAL001MNFR":					Obj = new SectionConfiguration.VAL001MNFR.Vendor_General();					return Obj;				case "VAL001VMC":					Obj = new SectionConfiguration.VAL001VMC.Vendor_General();					return Obj;				case "VAL001Z001":					Obj = new SectionConfiguration.VAL001Z001.Vendor_General();					return Obj;				case "VAL001Z002":					Obj = new SectionConfiguration.VAL001Z002.Vendor_General();					return Obj;				case "VAL001Z003":					Obj = new SectionConfiguration.VAL001Z003.Vendor_General();					return Obj;				case "VAL001Z004":					Obj = new SectionConfiguration.VAL001Z004.Vendor_General();					return Obj;				case "VAL001Z005":					Obj = new SectionConfiguration.VAL001Z005.Vendor_General();					return Obj;				case "VAL001Z007":					Obj = new SectionConfiguration.VAL001Z007.Vendor_General();					return Obj;				case "VAL001Z008":					Obj = new SectionConfiguration.VAL001Z008.Vendor_General();					return Obj;				case "VAL001Z009":					Obj = new SectionConfiguration.VAL001Z009.Vendor_General();					return Obj;				case "VAL001Z010":					Obj = new SectionConfiguration.VAL001Z010.Vendor_General();					return Obj;				case "VAL001Z011":					Obj = new SectionConfiguration.VAL001Z011.Vendor_General();					return Obj;				case "VAL001Z068":					Obj = new SectionConfiguration.VAL001Z068.Vendor_General();					return Obj;				case "VCL001MNFR":					Obj = new SectionConfiguration.VCL001MNFR.Vendor_General();					return Obj;				case "VCL001VMC":					Obj = new SectionConfiguration.VCL001VMC.Vendor_General();					return Obj;				case "VCL001Z001":					Obj = new SectionConfiguration.VCL001Z001.Vendor_General();					return Obj;				case "VCL001Z002":					Obj = new SectionConfiguration.VCL001Z002.Vendor_General();					return Obj;				case "VCL001Z003":					Obj = new SectionConfiguration.VCL001Z003.Vendor_General();					return Obj;				case "VCL001Z004":					Obj = new SectionConfiguration.VCL001Z004.Vendor_General();					return Obj;				case "VCL001Z005":					Obj = new SectionConfiguration.VCL001Z005.Vendor_General();					return Obj;				case "VCL001Z007":					Obj = new SectionConfiguration.VCL001Z007.Vendor_General();					return Obj;				case "VCL001Z008":					Obj = new SectionConfiguration.VCL001Z008.Vendor_General();					return Obj;				case "VCL001Z009":					Obj = new SectionConfiguration.VCL001Z009.Vendor_General();					return Obj;				case "VCL001Z010":					Obj = new SectionConfiguration.VCL001Z010.Vendor_General();					return Obj;				case "VCL001Z011":					Obj = new SectionConfiguration.VCL001Z011.Vendor_General();					return Obj;				case "VCL001Z068":					Obj = new SectionConfiguration.VCL001Z068.Vendor_General();					return Obj;				//8400000388 Start
				case "VCL001Z093":
					Obj = new SectionConfiguration.VCL001Z093.Vendor_General();
					return Obj;
				case "VAL001Z093":
					Obj = new SectionConfiguration.VAL001Z093.Vendor_General();
					return Obj;
				//8400000388 End
				default:					return null;			}		}	}}#endregion
#region 8400000388 Start
#region Class_Vendor_General_VAL001Z093
namespace SectionConfiguration.VAL001Z093
{
	public class Vendor_General
	{


		#region Properties
		public static int ddlVendorCategory
		{
			get { return 1; }
		}

		public static int ddlVendorAccGrp
		{
			get { return 1; }
		}

		public static int ddlRegion
		{
			get { return 1; }
		}

		public static int ddlCountry
		{
			get { return 1; }
		}

		public static int ddlCompanyCode
		{
			get { return 1; }
		}

		public static int txtCity
		{
			get { return 1; }
		}

		public static int txtCustomerCode
		{
			get { return 1; }
		}

		public static int txtEmailAddress
		{
			get { return 1; }
		}

		public static int txtHouseNo
		{
			get { return 1; }
		}

		public static int txtName1
		{
			get { return 1; }
		}

		public static int txtSortField
		{
			get { return 1; }
		}

		public static int txtPostalCode
		{
			get { return 1; }
		}

		public static int txtSecondTelephoneNumber
		{
			get { return 2; }
		}

		public static int txtStreet4
		{
			get { return 2; }
		}

		public static int txtStreet5
		{
			get { return 2; }
		}

		public static int txtMobileNum2
		{
			get { return 2; }
		}

		public static int txtName2
		{
			get { return 2; }
		}

		public static int txtFirsttelephone
		{
			get { return 2; }
		}

		public static int txtFaxNumber
		{
			get { return 2; }
		}

		public static int txtEmailAddress3
		{
			get { return 2; }
		}

		public static int txtMobileNum
		{
			get { return 2; }
		}

		public static int txtEmailAddress2
		{
			get { return 2; }
		}

		public static int txtDistrict
		{
			get { return 2; }
		}

		public static int txtCustomer_Number
		{
			get { return 2; }
		}

		public static int txtCompanyIDTrading
		{
			get { return 3; }
		}

		public static int ddlPurchaseOrg
		{
			get { return 3; }
		}

		public static int txtAuthorizationGroup
		{
			get { return 3; }
		}

		public static int txtLanguage
		{
			get { return 3; }
		}

		public static int txtMemo
		{
			get { return 3; }
		}

		public static int txtName3
		{
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{
			get { return 3; }
		}

		public static int txtPOBox
		{
			get { return 3; }
		}

		public static int txtName4
		{
			get { return 3; }
		}

		public static int txtTeletex_Number
		{
			get { return 3; }
		}

		public static int txtTelex_number
		{
			get { return 3; }
		}

		public static int txtTitle
		{
			get { return 3; }
		}




		#endregion
    }

}
 

#endregion

#region Class_Vendor_General_VCL001Z093
namespace SectionConfiguration.VCL001Z093
{
	public class Vendor_General
	{


		#region Properties
		public static int txtStreet5
		{
			get { return 2; }
		}

		public static int txtStreet4
		{
			get { return 2; }
		}

		public static int txtPostalCode
		{
			get { return 2; }
		}

		public static int txtSortField
		{
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{
			get { return 2; }
		}

		public static int txtMobileNum2
		{
			get { return 2; }
		}

		public static int txtName1
		{
			get { return 2; }
		}

		public static int txtName2
		{
			get { return 2; }
		}

		public static int txtHouseNo
		{
			get { return 2; }
		}

		public static int txtMobileNum
		{
			get { return 2; }
		}

		public static int txtFirsttelephone
		{
			get { return 2; }
		}

		public static int txtFaxNumber
		{
			get { return 2; }
		}

		public static int txtEmailAddress3
		{
			get { return 2; }
		}

		public static int txtEmailAddress2
		{
			get { return 2; }
		}

		public static int txtCity
		{
			get { return 2; }
		}

		public static int txtCustomer_Number
		{
			get { return 2; }
		}

		public static int txtEmailAddress
		{
			get { return 2; }
		}

		public static int txtDistrict
		{
			get { return 2; }
		}

		public static int txtCustomerCode
		{
			get { return 2; }
		}

		public static int ddlCompanyCode
		{
			get { return 2; }
		}

		public static int ddlCountry
		{
			get { return 2; }
		}

		public static int ddlVendorAccGrp
		{
			get { return 2; }
		}

		public static int ddlRegion
		{
			get { return 2; }
		}

		public static int ddlVendorCategory
		{
			get { return 2; }
		}

		public static int ddlPurchaseOrg
		{
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{
			get { return 3; }
		}

		public static int txtAuthorizationGroup
		{
			get { return 3; }
		}

		public static int txtMemo
		{
			get { return 3; }
		}

		public static int txtLanguage
		{
			get { return 3; }
		}

		public static int txtName3
		{
			get { return 3; }
		}

		public static int txtName4
		{
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{
			get { return 3; }
		}

		public static int txtPOBox
		{
			get { return 3; }
		}

		public static int txtTelex_number
		{
			get { return 3; }
		}

		public static int txtTeletex_Number
		{
			get { return 3; }
		}

		public static int txtTitle
		{
			get { return 3; }
		}




		#endregion
    }

}





#endregion
#endregion
#region Class_Vendor_General_VAL001MNFRnamespace SectionConfiguration.VAL001MNFR{	public class Vendor_General	{		#region Properties		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001VMCnamespace SectionConfiguration.VAL001VMC{	public class Vendor_General	{		#region Properties		public static int txtPostalCode		{ 			get { return 3; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 3; }		}		public static int txtSortField		{ 			get { return 3; }		}		public static int txtStreet4		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		public static int txtStreet5		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtMobileNum		{ 			get { return 3; }		}		public static int txtMobileNum2		{ 			get { return 3; }		}		public static int txtName1		{ 			get { return 3; }		}		public static int txtName2		{ 			get { return 3; }		}		public static int txtFirsttelephone		{ 			get { return 3; }		}		public static int txtHouseNo		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtMemo		{ 			get { return 3; }		}		public static int txtEmailAddress		{ 			get { return 3; }		}		public static int txtEmailAddress2		{ 			get { return 3; }		}		public static int txtEmailAddress3		{ 			get { return 3; }		}		public static int txtFaxNumber		{ 			get { return 3; }		}		public static int ddlVendorAccGrp		{ 			get { return 3; }		}		public static int ddlVendorCategory		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCity		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomer_Number		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtDistrict		{ 			get { return 3; }		}		public static int ddlCompanyCode		{ 			get { return 3; }		}		public static int ddlCountry		{ 			get { return 3; }		}		public static int ddlPurchaseOrg		{ 			get { return 3; }		}		public static int ddlRegion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z001namespace SectionConfiguration.VAL001Z001{	public class Vendor_General	{		#region Properties		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z002namespace SectionConfiguration.VAL001Z002{	public class Vendor_General	{		#region Properties		public static int txtSortField		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z003namespace SectionConfiguration.VAL001Z003{	public class Vendor_General	{		#region Properties		public static int txtCity		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtMobileNum		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z004namespace SectionConfiguration.VAL001Z004{	public class Vendor_General	{		#region Properties		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtMobileNum		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z005namespace SectionConfiguration.VAL001Z005{	public class Vendor_General	{		#region Properties		public static int txtCity		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtMobileNum		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z007namespace SectionConfiguration.VAL001Z007{	public class Vendor_General	{		#region Properties		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtMobileNum		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z008namespace SectionConfiguration.VAL001Z008{	public class Vendor_General	{		#region Properties		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtMobileNum		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z009namespace SectionConfiguration.VAL001Z009{	public class Vendor_General	{		#region Properties		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int txtCustomerCode		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z010namespace SectionConfiguration.VAL001Z010{	public class Vendor_General	{		#region Properties		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int txtCustomerCode		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int ddlPurchaseOrg		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtMemo		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z011namespace SectionConfiguration.VAL001Z011{	public class Vendor_General	{		#region Properties		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VAL001Z068namespace SectionConfiguration.VAL001Z068{	public class Vendor_General	{		#region Properties		public static int txtEmailAddress		{ 			get { return 1; }		}		public static int txtCity		{ 			get { return 1; }		}		public static int ddlVendorCategory		{ 			get { return 1; }		}		public static int ddlVendorAccGrp		{ 			get { return 1; }		}		public static int ddlRegion		{ 			get { return 1; }		}		public static int ddlCountry		{ 			get { return 1; }		}		public static int ddlCompanyCode		{ 			get { return 1; }		}		public static int txtMemo		{ 			get { return 1; }		}		public static int txtHouseNo		{ 			get { return 1; }		}		public static int txtPostalCode		{ 			get { return 1; }		}		public static int txtName1		{ 			get { return 1; }		}		public static int txtTitle		{ 			get { return 1; }		}		public static int txtSortField		{ 			get { return 1; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001MNFRnamespace SectionConfiguration.VCL001MNFR{	public class Vendor_General	{		#region Properties		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001VMCnamespace SectionConfiguration.VCL001VMC{	public class Vendor_General	{		#region Properties		public static int ddlCountry		{ 			get { return 3; }		}		public static int ddlPurchaseOrg		{ 			get { return 3; }		}		public static int ddlRegion		{ 			get { return 3; }		}		public static int ddlVendorAccGrp		{ 			get { return 3; }		}		public static int ddlCompanyCode		{ 			get { return 3; }		}		public static int ddlVendorCategory		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCity		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomer_Number		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtDistrict		{ 			get { return 3; }		}		public static int txtEmailAddress		{ 			get { return 3; }		}		public static int txtEmailAddress2		{ 			get { return 3; }		}		public static int txtEmailAddress3		{ 			get { return 3; }		}		public static int txtFaxNumber		{ 			get { return 3; }		}		public static int txtFirsttelephone		{ 			get { return 3; }		}		public static int txtMobileNum2		{ 			get { return 3; }		}		public static int txtName1		{ 			get { return 3; }		}		public static int txtName2		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtHouseNo		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtMemo		{ 			get { return 3; }		}		public static int txtMobileNum		{ 			get { return 3; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 3; }		}		public static int txtSortField		{ 			get { return 3; }		}		public static int txtStreet4		{ 			get { return 3; }		}		public static int txtStreet5		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPostalCode		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z001namespace SectionConfiguration.VCL001Z001{	public class Vendor_General	{		#region Properties		public static int txtTitle		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z002namespace SectionConfiguration.VCL001Z002{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z003namespace SectionConfiguration.VCL001Z003{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z004namespace SectionConfiguration.VCL001Z004{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z005namespace SectionConfiguration.VCL001Z005{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z007namespace SectionConfiguration.VCL001Z007{	public class Vendor_General	{		#region Properties		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z008namespace SectionConfiguration.VCL001Z008{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z009namespace SectionConfiguration.VCL001Z009{	public class Vendor_General	{		#region Properties		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtCustomerCode		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z010namespace SectionConfiguration.VCL001Z010{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtCustomerCode		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtMemo		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTitle		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z011namespace SectionConfiguration.VCL001Z011{	public class Vendor_General	{		#region Properties		public static int txtTitle		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Vendor_General_VCL001Z068namespace SectionConfiguration.VCL001Z068{	public class Vendor_General	{		#region Properties		public static int txtStreet5		{ 			get { return 2; }		}		public static int txtPostalCode		{ 			get { return 2; }		}		public static int txtSecondTelephoneNumber		{ 			get { return 2; }		}		public static int txtStreet4		{ 			get { return 2; }		}		public static int txtSortField		{ 			get { return 2; }		}		public static int txtName1		{ 			get { return 2; }		}		public static int txtMobileNum2		{ 			get { return 2; }		}		public static int txtName2		{ 			get { return 2; }		}		public static int txtTitle		{ 			get { return 2; }		}		public static int txtHouseNo		{ 			get { return 2; }		}		public static int txtMobileNum		{ 			get { return 2; }		}		public static int txtMemo		{ 			get { return 2; }		}		public static int txtFirsttelephone		{ 			get { return 2; }		}		public static int txtFaxNumber		{ 			get { return 2; }		}		public static int txtEmailAddress3		{ 			get { return 2; }		}		public static int txtCity		{ 			get { return 2; }		}		public static int txtCustomer_Number		{ 			get { return 2; }		}		public static int txtDistrict		{ 			get { return 2; }		}		public static int txtEmailAddress		{ 			get { return 2; }		}		public static int txtEmailAddress2		{ 			get { return 2; }		}		public static int ddlVendorAccGrp		{ 			get { return 2; }		}		public static int ddlRegion		{ 			get { return 2; }		}		public static int ddlVendorCategory		{ 			get { return 2; }		}		public static int ddlCompanyCode		{ 			get { return 2; }		}		public static int ddlCountry		{ 			get { return 2; }		}		public static int ddlPurchaseOrg		{ 			get { return 2; }		}		public static int txtAuthorizationGroup		{ 			get { return 3; }		}		public static int txtCustomerCode		{ 			get { return 3; }		}		public static int txtCompanyIDTrading		{ 			get { return 3; }		}		public static int txtLanguage		{ 			get { return 3; }		}		public static int txtName3		{ 			get { return 3; }		}		public static int txtName4		{ 			get { return 3; }		}		public static int txtPOBoxPostal		{ 			get { return 3; }		}		public static int txtPOBox		{ 			get { return 3; }		}		public static int txtTelex_number		{ 			get { return 3; }		}		public static int txtTeletex_Number		{ 			get { return 3; }		}		#endregion	}}#endregion
