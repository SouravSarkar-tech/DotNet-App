using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region Class_General_data

namespace SectionConfiguration
{
	public class General_data
	{

		public object GetClass(string str)
		{
			object Obj;
			switch (str)
			{
				case "CAL001CEXT":
					Obj = new SectionConfiguration.CAL001CEXT.General_data();
					return Obj;
				case "CAL001CMC":
					Obj = new SectionConfiguration.CAL001CMC.General_data();
					return Obj;
				case "CAL001Z001":
					Obj = new SectionConfiguration.CAL001Z001.General_data();
					return Obj;
				case "CAL001Z002":
					Obj = new SectionConfiguration.CAL001Z002.General_data();
					return Obj;
				case "CAL001Z003":
					Obj = new SectionConfiguration.CAL001Z003.General_data();
					return Obj;
				case "CAL001Z004":
					Obj = new SectionConfiguration.CAL001Z004.General_data();
					return Obj;
				case "CAL001Z005":
					Obj = new SectionConfiguration.CAL001Z005.General_data();
					return Obj;
				case "CAL001Z006":
					Obj = new SectionConfiguration.CAL001Z006.General_data();
					return Obj;
				case "CAL001Z008":
					Obj = new SectionConfiguration.CAL001Z008.General_data();
					return Obj;
				case "CCL001CEXT":
					Obj = new SectionConfiguration.CCL001CEXT.General_data();
					return Obj;
				case "CCL001CMC":
					Obj = new SectionConfiguration.CCL001CMC.General_data();
					return Obj;
				case "CCL001Z001":
					Obj = new SectionConfiguration.CCL001Z001.General_data();
					return Obj;
				case "CCL001Z002":
					Obj = new SectionConfiguration.CCL001Z002.General_data();
					return Obj;
				case "CCL001Z003":
					Obj = new SectionConfiguration.CCL001Z003.General_data();
					return Obj;
				case "CCL001Z004":
					Obj = new SectionConfiguration.CCL001Z004.General_data();
					return Obj;
				case "CCL001Z005":
					Obj = new SectionConfiguration.CCL001Z005.General_data();
					return Obj;
				case "CCL001Z006":
					Obj = new SectionConfiguration.CCL001Z006.General_data();
					return Obj;
				case "CCL001Z008":
					Obj = new SectionConfiguration.CCL001Z008.General_data();
					return Obj;
				//Start Change by Swati
                case "CAL001Z055":
                    Obj = new SectionConfiguration.CAL001Z055.General_data();
                    return Obj;
				//End Change
                default:
					return null;
			}
		}
	}

}

#endregion


#region Class_General_data_CAL001CEXT

namespace SectionConfiguration.CAL001CEXT
{
	public class General_data
	{
		#region Properties

		public static int ddlCompanyCode
		{ 
			get { return 3; }
		}

		public static int ddlCountry
		{ 
			get { return 3; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 3; }
		}

		public static int ddlRegion
		{ 
			get { return 3; }
		}

		public static int ddlTitle
		{ 
			get { return 3; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 3; }
		}

		public static int txtCity
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtCustomerCode
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtEmailAddress
		{ 
			get { return 3; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 3; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 3; }
		}

		public static int txtFaxNumber
		{ 
			get { return 3; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
		{ 
			get { return 3; }
		}

		public static int txtHouseNo
		{ 
			get { return 3; }
		}

		public static int txtLanguage
		{ 
			get { return 3; }
		}

		public static int txtMobileNum
		{ 
			get { return 3; }
		}

		public static int txtMobileNum2
		{ 
			get { return 3; }
		}

		public static int txtName1
		{ 
			get { return 3; }
		}

		public static int txtName2
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}

		public static int txtPostalCode
		{ 
			get { return 3; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 3; }
		}

		public static int txtSortfield
		{ 
			get { return 3; }
		}

		public static int txtstreet2
		{ 
			get { return 3; }
		}

		public static int txtstreet3
		{ 
			get { return 3; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001CMC

namespace SectionConfiguration.CAL001CMC
{
	public class General_data
	{
		#region Properties

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
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

		public static int ddlTransportationZone
		{ 
			get { return 1; }
		}

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int ddlCountry
		{ 
			get { return 1; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
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

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtName4
		{ 
			get { return 3; }
		}

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
		{ 
			get { return 3; }
		}

		public static int txtName3
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z001

namespace SectionConfiguration.CAL001Z001
{
	public class General_data
	{
		#region Properties

		public static int txtCity
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

		public static int ddlTransportationZone
		{ 
			get { return 1; }
		}

		public static int txtName1
		{ 
			get { return 1; }
		}

		public static int txtHouseNo
		{ 
			get { return 1; }
		}

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
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

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z002

namespace SectionConfiguration.CAL001Z002
{
	public class General_data
	{
		#region Properties

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
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

		public static int ddlCountry
		{ 
			get { return 1; }
		}

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z003

namespace SectionConfiguration.CAL001Z003
{
	public class General_data
	{
		#region Properties

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int ddlTransportationZone
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

		public static int txtName1
		{ 
			get { return 1; }
		}

		public static int txtHouseNo
		{ 
			get { return 1; }
		}

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
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

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z004

namespace SectionConfiguration.CAL001Z004
{
	public class General_data
	{
		#region Properties

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
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

		public static int ddlCountry
		{ 
			get { return 1; }
		}

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z005

namespace SectionConfiguration.CAL001Z005
{
	public class General_data
	{
		#region Properties

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int txtCustomerCode
		{ 
			get { return 1; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 1; }
		}

		public static int ddlCountry
		{ 
			get { return 1; }
		}

		public static int txtName1
		{ 
			get { return 1; }
		}

		public static int txtHouseNo
		{ 
			get { return 1; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 1; }
		}

		public static int txtFaxNumber
		{ 
			get { return 1; }
		}

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
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

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z006

namespace SectionConfiguration.CAL001Z006
{
	public class General_data
	{
		#region Properties

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
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

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 1; }
		}

		public static int ddlCountry
		{ 
			get { return 1; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CAL001Z008

namespace SectionConfiguration.CAL001Z008
{
	public class General_data
	{
		#region Properties

		public static int ddlTransportationZone
		{ 
			get { return 1; }
		}

		public static int ddlCountry
		{ 
			get { return 1; }
		}

		public static int txtCity
		{ 
			get { return 1; }
		}

		public static int txtName1
		{ 
			get { return 1; }
		}

		public static int txtHouseNo
		{ 
			get { return 1; }
		}

		public static int txtSortfield
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
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

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001CEXT

namespace SectionConfiguration.CCL001CEXT
{
	public class General_data
	{
		#region Properties

		public static int ddlCompanyCode
		{ 
			get { return 3; }
		}

		public static int ddlCountry
		{ 
			get { return 3; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 3; }
		}

		public static int ddlRegion
		{ 
			get { return 3; }
		}

		public static int ddlTitle
		{ 
			get { return 3; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 3; }
		}

		public static int txtCity
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtCustomerCode
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtEmailAddress
		{ 
			get { return 3; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 3; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 3; }
		}

		public static int txtFaxNumber
		{ 
			get { return 3; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
		{ 
			get { return 3; }
		}

		public static int txtHouseNo
		{ 
			get { return 3; }
		}

		public static int txtLanguage
		{ 
			get { return 3; }
		}

		public static int txtMobileNum
		{ 
			get { return 3; }
		}

		public static int txtMobileNum2
		{ 
			get { return 3; }
		}

		public static int txtName1
		{ 
			get { return 3; }
		}

		public static int txtName2
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}

		public static int txtPostalCode
		{ 
			get { return 3; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 3; }
		}

		public static int txtSortfield
		{ 
			get { return 3; }
		}

		public static int txtstreet2
		{ 
			get { return 3; }
		}

		public static int txtstreet3
		{ 
			get { return 3; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001CMC

namespace SectionConfiguration.CCL001CMC
{
	public class General_data
	{
		#region Properties

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
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

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int txtCity
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

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtName3
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
		{ 
			get { return 3; }
		}

		public static int txtName4
		{ 
			get { return 3; }
		}

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z001

namespace SectionConfiguration.CCL001Z001
{
	public class General_data
	{
		#region Properties

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtName1
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

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
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

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int txtCity
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCountry
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtName4
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z002

namespace SectionConfiguration.CCL001Z002
{
	public class General_data
	{
		#region Properties

		public static int txtName1
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int txtCity
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

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z003

namespace SectionConfiguration.CCL001Z003
{
	public class General_data
	{
		#region Properties

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtName1
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

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
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

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtCity
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCountry
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtName4
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z004

namespace SectionConfiguration.CCL001Z004
{
	public class General_data
	{
		#region Properties

		public static int txtName1
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int txtCity
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

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z005

namespace SectionConfiguration.CCL001Z005
{
	public class General_data
	{
		#region Properties

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtName1
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

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
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

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtCity
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCountry
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtName4
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z006

namespace SectionConfiguration.CCL001Z006
{
	public class General_data
	{
		#region Properties

		public static int txtName1
		{ 
			get { return 2; }
		}

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtFaxNumber
		{ 
			get { return 2; }
		}

		public static int txtFirsttelephone
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress2
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress3
		{ 
			get { return 2; }
		}

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int txtCity
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

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtPOBox
		{ 
			get { return 3; }
		}

		public static int txtPOBoxPostal
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


#region Class_General_data_CCL001Z008

namespace SectionConfiguration.CCL001Z008
{
	public class General_data
	{
		#region Properties

		public static int txtName2
		{ 
			get { return 2; }
		}

		public static int txtName1
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

		public static int txtMobileNum2
		{ 
			get { return 2; }
		}

		public static int txtMobileNum
		{ 
			get { return 2; }
		}

		public static int txtLanguage
		{ 
			get { return 2; }
		}

		public static int txtHouseNo
		{ 
			get { return 2; }
		}

		public static int txtstreet3
		{ 
			get { return 2; }
		}

		public static int txtstreet5
		{ 
			get { return 1; }
		}

		public static int txtstreet4
		{ 
			get { return 1; }
		}

		public static int txtstreet2
		{ 
			get { return 2; }
		}

		public static int txtSortfield
		{ 
			get { return 2; }
		}

		public static int txtSecondTelephoneNumber
		{ 
			get { return 2; }
		}

		public static int txtPostalCode
		{ 
			get { return 2; }
		}

		public static int txtTaxJurisdiction
		{ 
			get { return 2; }
		}

		public static int txtCustomerCode
		{ 
			get { return 2; }
		}

		public static int txtEmailAddress
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

		public static int ddlTransportationZone
		{ 
			get { return 2; }
		}

		public static int ddlTitle
		{ 
			get { return 2; }
		}

		public static int txtCity
		{ 
			get { return 2; }
		}

		public static int txtAccountNumberVendor
		{ 
			get { return 2; }
		}

		public static int ddlRegion
		{ 
			get { return 2; }
		}

		public static int ddlCustomerAccGrp
		{ 
			get { return 2; }
		}

		public static int ddlCountry
		{ 
			get { return 2; }
		}

		public static int ddlCompanyCode
		{ 
			get { return 2; }
		}

		public static int txtCompanyIDTrading
		{ 
			get { return 3; }
		}

		public static int txtCityCode
		{ 
			get { return 3; }
		}

		public static int rdlLiableforVAT
		{ 
			get { return 3; }
		}

		public static int txtDistrict
		{ 
			get { return 3; }
		}

		public static int txtDifferentCity
		{ 
			get { return 3; }
		}

		public static int txtCountryCode
		{ 
			get { return 3; }
		}

		public static int txtGroupkey
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

		public static int txtNameCO
		{ 
			get { return 3; }
		}

		public static int txtName4
		{ 
			get { return 3; }
		}


		#endregion
	}

}

#endregion


//Start Change by Swati
#region Class_General_data_CAL001Z055
namespace SectionConfiguration.CAL001Z055
{
    public class General_data
    {


        #region Properties
        public static int txtName2
        {
            get { return 2; }
        }

        public static int txtName1
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

        public static int txtMobileNum2
        {
            get { return 2; }
        }

        public static int txtMobileNum
        {
            get { return 2; }
        }

        public static int txtLanguage
        {
            get { return 2; }
        }

        public static int txtHouseNo
        {
            get { return 2; }
        }

        public static int txtstreet3
        {
            get { return 2; }
        }

        public static int txtstreet5
        {
            get { return 1; }
        }

        public static int txtstreet4
        {
            get { return 1; }
        }

        public static int txtstreet2
        {
            get { return 2; }
        }

        public static int txtSortfield
        {
            get { return 2; }
        }

        public static int txtSecondTelephoneNumber
        {
            get { return 2; }
        }

        public static int txtPostalCode
        {
            get { return 2; }
        }

        public static int txtTaxJurisdiction
        {
            get { return 2; }
        }

        public static int txtCustomerCode
        {
            get { return 2; }
        }

        public static int txtEmailAddress
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

        public static int ddlTransportationZone
        {
            get { return 2; }
        }

        public static int ddlTitle
        {
            get { return 2; }
        }

        public static int txtCity
        {
            get { return 2; }
        }

        public static int txtAccountNumberVendor
        {
            get { return 2; }
        }

        public static int ddlRegion
        {
            get { return 2; }
        }

        public static int ddlCustomerAccGrp
        {
            get { return 2; }
        }

        public static int ddlCountry
        {
            get { return 2; }
        }

        public static int ddlCompanyCode
        {
            get { return 2; }
        }

        public static int txtCompanyIDTrading
        {
            get { return 3; }
        }

        public static int txtCityCode
        {
            get { return 3; }
        }

        public static int rdlLiableforVAT
        {
            get { return 3; }
        }

        public static int txtDistrict
        {
            get { return 3; }
        }

        public static int txtDifferentCity
        {
            get { return 3; }
        }

        public static int txtCountryCode
        {
            get { return 3; }
        }

        public static int txtGroupkey
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

        public static int txtNameCO
        {
            get { return 3; }
        }

        public static int txtName4
        {
            get { return 3; }
        }




        #endregion
    }

}

#endregion
//End Change