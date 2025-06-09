using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;

namespace Accenture.MWTT.DomainObject
{
    public class CompanyCodee : Base
    {
        public string Relevant { get; set; }
        public int Master_Header_Id { get; set; }
        public string Company_Id { get; set; }
        public int Cust_CompanyCode_Id { get; set; }
        public string Planning_Group { get; set; }
        public string Autorization_Gr { get; set; }
        public string ReconciliationAccount { get; set; }
        public string HeadOfficeAccNumber { get; set; }
        public string keySortingAssignmen { get; set; }
        public string TermsPaymentKey { get; set; }
        public string ToleranceGroupBussAcc { get; set; }
        public string IndicaterRecordPayHis { get; set; }
        public string ListPaymentConsidered { get; set; }
        public string BlockKeyPayment { get; set; }
        public string AccNumberALterPlayer { get; set; }
        public string ShortKeyBank { get; set; }
        public int IndicatorPayAll { get; set; }
        public string IndiClearingBetwCust { get; set; }
        public string NextPaye { get; set; }
        public string AccountionClerk { get; set; }
        public int indicatorPeriodicAccount { get; set; }
        public string OurAccoCust { get; set; }
        public string Memo { get; set; }
        public string IndiPaymentNotice { get; set; }
        public string IndiPayment { get; set; }
        public string IndipaymentWoCleared { get; set; }
        public string IndiPaymentAccountingDepart { get; set; }
        public string IndiPaymentlegalDepartment { get; set; }
        public string DeletionFlagMasterRecord { get; set; }
        public string PostingBlockCompanyCode { get; set; }
        public string PreviousRecordNumber { get; set; }
        public string KeyPaymentGrouping { get; set; }
        public string PaymentTermCreditMemos { get; set; }
        public string WithholdingTaxCountry { get; set; }
        public string IndiForWithHoldingTaxType { get; set; }
        public string WithholdingTaxCode { get; set; }
        public string WitHoldingTaxIdenNumb { get; set; }
        public int IsActive { get; set; }

    }
}