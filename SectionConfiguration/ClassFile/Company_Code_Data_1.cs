using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region Class_Company_Code_Data_1namespace SectionConfiguration{	public class Company_Code_Data_1	{		public object GetClass(string str)		{			object Obj;			switch (str)			{				case "VAL001MNFR":					Obj = new SectionConfiguration.VAL001MNFR.Company_Code_Data_1();					return Obj;				case "VAL001VMC":					Obj = new SectionConfiguration.VAL001VMC.Company_Code_Data_1();					return Obj;				case "VAL001Z001":					Obj = new SectionConfiguration.VAL001Z001.Company_Code_Data_1();					return Obj;				case "VAL001Z002":					Obj = new SectionConfiguration.VAL001Z002.Company_Code_Data_1();					return Obj;				case "VAL001Z003":					Obj = new SectionConfiguration.VAL001Z003.Company_Code_Data_1();					return Obj;				case "VAL001Z004":					Obj = new SectionConfiguration.VAL001Z004.Company_Code_Data_1();					return Obj;				case "VAL001Z005":					Obj = new SectionConfiguration.VAL001Z005.Company_Code_Data_1();					return Obj;				case "VAL001Z007":					Obj = new SectionConfiguration.VAL001Z007.Company_Code_Data_1();					return Obj;				case "VAL001Z008":					Obj = new SectionConfiguration.VAL001Z008.Company_Code_Data_1();					return Obj;				case "VAL001Z009":					Obj = new SectionConfiguration.VAL001Z009.Company_Code_Data_1();					return Obj;				case "VAL001Z010":					Obj = new SectionConfiguration.VAL001Z010.Company_Code_Data_1();					return Obj;				case "VAL001Z011":					Obj = new SectionConfiguration.VAL001Z011.Company_Code_Data_1();					return Obj;				case "VAL001Z068":					Obj = new SectionConfiguration.VAL001Z068.Company_Code_Data_1();					return Obj;				case "VCL001MNFR":					Obj = new SectionConfiguration.VCL001MNFR.Company_Code_Data_1();					return Obj;				case "VCL001VMC":					Obj = new SectionConfiguration.VCL001VMC.Company_Code_Data_1();					return Obj;				case "VCL001Z001":					Obj = new SectionConfiguration.VCL001Z001.Company_Code_Data_1();					return Obj;				case "VCL001Z002":					Obj = new SectionConfiguration.VCL001Z002.Company_Code_Data_1();					return Obj;				case "VCL001Z003":					Obj = new SectionConfiguration.VCL001Z003.Company_Code_Data_1();					return Obj;				case "VCL001Z004":					Obj = new SectionConfiguration.VCL001Z004.Company_Code_Data_1();					return Obj;				case "VCL001Z005":					Obj = new SectionConfiguration.VCL001Z005.Company_Code_Data_1();					return Obj;				case "VCL001Z007":					Obj = new SectionConfiguration.VCL001Z007.Company_Code_Data_1();					return Obj;				case "VCL001Z008":					Obj = new SectionConfiguration.VCL001Z008.Company_Code_Data_1();					return Obj;				case "VCL001Z009":					Obj = new SectionConfiguration.VCL001Z009.Company_Code_Data_1();					return Obj;				case "VCL001Z010":					Obj = new SectionConfiguration.VCL001Z010.Company_Code_Data_1();					return Obj;				case "VCL001Z011":					Obj = new SectionConfiguration.VCL001Z011.Company_Code_Data_1();					return Obj;				case "VCL001Z068":					Obj = new SectionConfiguration.VCL001Z068.Company_Code_Data_1();					return Obj;				//8400000388 Start
				case "VCL001Z093":
					Obj = new SectionConfiguration.VCL001Z093.Company_Code_Data_1();
					return Obj;
				case "VAL001Z093":
					Obj = new SectionConfiguration.VAL001Z093.Company_Code_Data_1();
					return Obj;
				//8400000388 End
				default:					return null;			}		}	}}#endregion#region 8400000388
#region Class_Company_Code_Data_1_VCL001Z093
namespace SectionConfiguration.VCL001Z093
{
	public class Company_Code_Data_1
	{


		#region Properties
		public static int ddlTermPaymentKey
		{
			//get { return 2; }
			get { return 3; }
		}

		public static int txtAccNoAltPayee
		{
			get { return 2; }
		}

		public static int ddlPlanningGroup
		{
			get { return 2; }
		}

		public static int ddlPaymentMethod
		{
			get { return 2; }
		}

		public static int txtHOAccNo
		{
			get { return 2; }
		}

		public static int txtPersonnelNo
		{
			get { return 2; }
		}

		public static int txtPreviousMasterNo
		{
			get { return 2; }
		}

		public static int txtShortKeyHouseBank
		{
			get { return 2; }
		}

		public static int txtReconAcc
		{
			get { return 2; }
		}

		public static int txtProbableTimeChequePaid
		{
			get { return 3; }
		}

		public static int txtReleaseApprovalGrp
		{
			get { return 3; }
		}

		public static int txtPaymentTermKeyCreditMeno
		{
			get { return 3; }
		}

		public static int txtToleranceGrpGL
		{
			get { return 3; }
		}

		public static int txtToleranceGrp
		{
			get { return 3; }
		}

		public static int txtDate2
		{
			get { return 3; }
		}

		public static int txtDate1
		{
			get { return 3; }
		}

		public static int txtCertiDate
		{
			get { return 3; }
		}

		public static int txtPaymentMethodSupp
		{
			get { return 3; }
		}

		public static int txtKeySortAssignNo
		{
			get { return 3; }
		}

		public static int txtKeyPaymentGrp
		{
			get { return 3; }
		}

		public static int txtInternetAddpartner
		{
			get { return 3; }
		}

		public static int txtBlockKeyPayment
		{
			get { return 3; }
		}

		public static int txtBillExchangeLimit
		{
			get { return 3; }
		}

		public static int txtAuthorizationgrp
		{
			get { return 3; }
		}

		public static int chkIsSendPaymentAdvicesEDI
		{
			get { return 3; }
		}

		public static int chkIsPrepaymentRelevant
		{
			get { return 3; }
		}

		public static int chkIsPostingBlockCompanyCode
		{
			get { return 3; }
		}

		public static int chkIsMasterRecordDeleted
		{
			get { return 3; }
		}

		public static int chkIsDoubleInvoice
		{
			get { return 3; }
		}

		public static int chkIsPeriodicAccStmt
		{
			get { return 3; }
		}

		public static int chkIsPayAllItemSeperately
		{
			get { return 3; }
		}

		public static int chkIsClearingCustVend
		{
			get { return 3; }
		}

		public static int chkIsBlockMasterRecordDeletion
		{
			get { return 3; }
		}




		#endregion
    }

}



#endregion


#region Class_Company_Code_Data_1_VAL001Z093
namespace SectionConfiguration.VAL001Z093
{
	public class Company_Code_Data_1
	{


		#region Properties
		public static int txtReconAcc
		{
			get { return 1; }
		}

		public static int ddlPlanningGroup
		{
			get { return 1; }
		}

		public static int ddlPaymentMethod
		{
			get { return 1; }
		}

		public static int txtPersonnelNo
		{
			get { return 2; }
		}

		public static int txtPreviousMasterNo
		{
			get { return 2; }
		}

		public static int txtHOAccNo
		{
			get { return 2; }
		}

		public static int txtAccNoAltPayee
		{
			get { return 2; }
		}

		public static int ddlTermPaymentKey
		{
			get { return 2; }
		}

		public static int txtShortKeyHouseBank
		{
			get { return 2; }
		}

		public static int txtToleranceGrp
		{
			get { return 3; }
		}

		public static int txtReleaseApprovalGrp
		{
			get { return 3; }
		}

		public static int txtToleranceGrpGL
		{
			get { return 3; }
		}

		public static int txtAuthorizationgrp
		{
			get { return 3; }
		}

		public static int chkIsPrepaymentRelevant
		{
			get { return 3; }
		}

		public static int chkIsSendPaymentAdvicesEDI
		{
			get { return 3; }
		}

		public static int chkIsPayAllItemSeperately
		{
			get { return 3; }
		}

		public static int chkIsPostingBlockCompanyCode
		{
			get { return 3; }
		}

		public static int chkIsPeriodicAccStmt
		{
			get { return 3; }
		}

		public static int chkIsMasterRecordDeleted
		{
			get { return 3; }
		}

		public static int chkIsDoubleInvoice
		{
			get { return 3; }
		}

		public static int chkIsClearingCustVend
		{
			get { return 3; }
		}

		public static int chkIsBlockMasterRecordDeletion
		{
			get { return 3; }
		}

		public static int txtDate2
		{
			get { return 3; }
		}

		public static int txtInternetAddpartner
		{
			get { return 3; }
		}

		public static int txtBlockKeyPayment
		{
			get { return 3; }
		}

		public static int txtBillExchangeLimit
		{
			get { return 3; }
		}

		public static int txtDate1
		{
			get { return 3; }
		}

		public static int txtCertiDate
		{
			get { return 3; }
		}

		public static int txtProbableTimeChequePaid
		{
			get { return 3; }
		}

		public static int txtPaymentMethodSupp
		{
			get { return 3; }
		}

		public static int txtPaymentTermKeyCreditMeno
		{
			get { return 3; }
		}

		public static int txtKeySortAssignNo
		{
			get { return 3; }
		}

		public static int txtKeyPaymentGrp
		{
			get { return 3; }
		}




		#endregion
    }

}



#endregion

#endregion
#region Class_Company_Code_Data_1_VAL001MNFRnamespace SectionConfiguration.VAL001MNFR{	public class Company_Code_Data_1	{		#region Properties		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int ddlPaymentMethod		{ 			get { return 3; }		}		public static int ddlPlanningGroup		{ 			get { return 3; }		}		public static int ddlTermPaymentKey		{ 			get { return 3; }		}		public static int txtAccNoAltPayee		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtHOAccNo		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPreviousMasterNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReconAcc		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtShortKeyHouseBank		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001VMCnamespace SectionConfiguration.VAL001VMC{	public class Company_Code_Data_1	{		#region Properties		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtReconAcc		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtShortKeyHouseBank		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPreviousMasterNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtHOAccNo		{ 			get { return 3; }		}		public static int txtAccNoAltPayee		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int ddlPaymentMethod		{ 			get { return 3; }		}		public static int ddlPlanningGroup		{ 			get { return 3; }		}		public static int ddlTermPaymentKey		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z001namespace SectionConfiguration.VAL001Z001{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z002namespace SectionConfiguration.VAL001Z002{	public class Company_Code_Data_1	{		#region Properties		public static int txtReconAcc		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z003namespace SectionConfiguration.VAL001Z003{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z004namespace SectionConfiguration.VAL001Z004{	public class Company_Code_Data_1	{		#region Properties		public static int txtReconAcc		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z005namespace SectionConfiguration.VAL001Z005{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z007namespace SectionConfiguration.VAL001Z007{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z008namespace SectionConfiguration.VAL001Z008{	public class Company_Code_Data_1	{		#region Properties		public static int txtReconAcc		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z009namespace SectionConfiguration.VAL001Z009{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z010namespace SectionConfiguration.VAL001Z010{	public class Company_Code_Data_1	{		#region Properties		public static int txtReconAcc		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int txtPersonnelNo		{ 			get { return 1; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z011namespace SectionConfiguration.VAL001Z011{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VAL001Z068namespace SectionConfiguration.VAL001Z068{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPlanningGroup		{ 			get { return 1; }		}		public static int ddlPaymentMethod		{ 			get { return 1; }		}		public static int txtReconAcc		{ 			get { return 1; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001MNFRnamespace SectionConfiguration.VCL001MNFR{	public class Company_Code_Data_1	{		#region Properties		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int ddlPaymentMethod		{ 			get { return 3; }		}		public static int ddlPlanningGroup		{ 			get { return 3; }		}		public static int ddlTermPaymentKey		{ 			get { return 3; }		}		public static int txtAccNoAltPayee		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtHOAccNo		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPreviousMasterNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReconAcc		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtShortKeyHouseBank		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001VMCnamespace SectionConfiguration.VCL001VMC{	public class Company_Code_Data_1	{		#region Properties		public static int txtReconAcc		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtShortKeyHouseBank		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPreviousMasterNo		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtHOAccNo		{ 			get { return 3; }		}		public static int txtAccNoAltPayee		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int ddlPaymentMethod		{ 			get { return 3; }		}		public static int ddlPlanningGroup		{ 			get { return 3; }		}		public static int ddlTermPaymentKey		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z001namespace SectionConfiguration.VCL001Z001{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z002namespace SectionConfiguration.VCL001Z002{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z003namespace SectionConfiguration.VCL001Z003{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z004namespace SectionConfiguration.VCL001Z004{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z005namespace SectionConfiguration.VCL001Z005{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z007namespace SectionConfiguration.VCL001Z007{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z008namespace SectionConfiguration.VCL001Z008{	public class Company_Code_Data_1	{		#region Properties		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z009namespace SectionConfiguration.VCL001Z009{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z010namespace SectionConfiguration.VCL001Z010{	public class Company_Code_Data_1	{		#region Properties		public static int ddlTermPaymentKey		{
            //get { return 2; }
            get { return 3; }
        }		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPersonnelNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z011namespace SectionConfiguration.VCL001Z011{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		#endregion	}}#endregion
#region Class_Company_Code_Data_1_VCL001Z068namespace SectionConfiguration.VCL001Z068{	public class Company_Code_Data_1	{		#region Properties		public static int ddlPaymentMethod		{ 			get { return 2; }		}		public static int txtAccNoAltPayee		{ 			get { return 2; }		}		public static int ddlTermPaymentKey		{ 			get { return 2; }		}		public static int ddlPlanningGroup		{ 			get { return 2; }		}		public static int txtHOAccNo		{ 			get { return 2; }		}		public static int txtPreviousMasterNo		{ 			get { return 2; }		}		public static int txtShortKeyHouseBank		{ 			get { return 2; }		}		public static int txtReconAcc		{ 			get { return 2; }		}		public static int txtReleaseApprovalGrp		{ 			get { return 3; }		}		public static int txtProbableTimeChequePaid		{ 			get { return 3; }		}		public static int txtPersonnelNo		{ 			get { return 3; }		}		public static int txtPaymentTermKeyCreditMeno		{ 			get { return 3; }		}		public static int txtToleranceGrp		{ 			get { return 3; }		}		public static int txtToleranceGrpGL		{ 			get { return 3; }		}		public static int txtDate2		{ 			get { return 3; }		}		public static int txtDate1		{ 			get { return 3; }		}		public static int txtPaymentMethodSupp		{ 			get { return 3; }		}		public static int txtKeySortAssignNo		{ 			get { return 3; }		}		public static int txtKeyPaymentGrp		{ 			get { return 3; }		}		public static int txtInternetAddpartner		{ 			get { return 3; }		}		public static int txtBlockKeyPayment		{ 			get { return 3; }		}		public static int txtCertiDate		{ 			get { return 3; }		}		public static int txtBillExchangeLimit		{ 			get { return 3; }		}		public static int txtAuthorizationgrp		{ 			get { return 3; }		}		public static int chkIsSendPaymentAdvicesEDI		{ 			get { return 3; }		}		public static int chkIsPrepaymentRelevant		{ 			get { return 3; }		}		public static int chkIsPostingBlockCompanyCode		{ 			get { return 3; }		}		public static int chkIsMasterRecordDeleted		{ 			get { return 3; }		}		public static int chkIsPeriodicAccStmt		{ 			get { return 3; }		}		public static int chkIsPayAllItemSeperately		{ 			get { return 3; }		}		public static int chkIsDoubleInvoice		{ 			get { return 3; }		}		public static int chkIsClearingCustVend		{ 			get { return 3; }		}		public static int chkIsBlockMasterRecordDeletion		{ 			get { return 3; }		}		#endregion	}}#endregion
