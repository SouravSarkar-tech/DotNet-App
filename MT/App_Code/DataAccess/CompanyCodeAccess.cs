using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;
using Accenture.MWTT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class CompanyCodeAccess
    {
        public CompanyCodeAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Save(CompanyCodee ObjCustCompany)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Cust_CompanyCode";
            int result = 0;

            hashPara.Add("@Master_Header_Id", ObjCustCompany.Master_Header_Id);
            hashPara.Add("@Cust_CompanyCode_Id", ObjCustCompany.Cust_CompanyCode_Id);
            hashPara.Add("@Company_Id", ObjCustCompany.Company_Id);
            hashPara.Add("@Autorization_Gr", ObjCustCompany.Autorization_Gr);
            hashPara.Add("@ReconciliationAccount", ObjCustCompany.ReconciliationAccount);
            hashPara.Add("@Planning_Group", ObjCustCompany.Planning_Group);
            hashPara.Add("@keySortingAssignment", ObjCustCompany.keySortingAssignmen);
            hashPara.Add("@HeadOfficeAccNumber", ObjCustCompany.HeadOfficeAccNumber);
            hashPara.Add("@TermsPaymentKey", ObjCustCompany.TermsPaymentKey);
            hashPara.Add("@ToleranceGroupBussAcc", ObjCustCompany.ToleranceGroupBussAcc);
            hashPara.Add("@ShortKeyBank", ObjCustCompany.ShortKeyBank);
            hashPara.Add("@IndicaterRecordPayHis", ObjCustCompany.IndicaterRecordPayHis);
            hashPara.Add("@ListPaymentConsidered", ObjCustCompany.ListPaymentConsidered);
            hashPara.Add("@BlockKeyPayment", ObjCustCompany.BlockKeyPayment);
            hashPara.Add("@AccNumberALterPlayer", ObjCustCompany.AccNumberALterPlayer);
            hashPara.Add("@chkIndicatorPayAll", ObjCustCompany.IndicatorPayAll);
            hashPara.Add("@IndiClearingBetwCust", ObjCustCompany.IndiClearingBetwCust);
            hashPara.Add("@NextPaye", ObjCustCompany.NextPaye);
            hashPara.Add("@AccountionClerk", ObjCustCompany.AccountionClerk);
            hashPara.Add("@chkindicatorPeriodicAccount", ObjCustCompany.indicatorPeriodicAccount);
            hashPara.Add("@OurAccoCust", ObjCustCompany.OurAccoCust);
            hashPara.Add("@WitHoldingTaxIdenNumb", ObjCustCompany.WitHoldingTaxIdenNumb);
            hashPara.Add("@Memo", ObjCustCompany.Memo);
            hashPara.Add("@IndiPaymentNotice", ObjCustCompany.IndiPaymentNotice);
            hashPara.Add("@IndiPayment", ObjCustCompany.IndiPayment);
            hashPara.Add("@IndipaymentWoCleared", ObjCustCompany.IndipaymentWoCleared);
            hashPara.Add("@IndiPaymentAccountingDepart", ObjCustCompany.IndiPaymentAccountingDepart);
            hashPara.Add("@IndiPaymentlegalDepartment", ObjCustCompany.IndiPaymentlegalDepartment);
            hashPara.Add("@DeletionFlagMasterRecord", ObjCustCompany.DeletionFlagMasterRecord);
            hashPara.Add("@PostingBlockCompanyCode", ObjCustCompany.PostingBlockCompanyCode);
            hashPara.Add("@PreviousRecordNumber", ObjCustCompany.PreviousRecordNumber);
            hashPara.Add("@KeyPaymentGrouping", ObjCustCompany.KeyPaymentGrouping);
            hashPara.Add("@PaymentTermCreditMemos", ObjCustCompany.PaymentTermCreditMemos);
            hashPara.Add("@WithholdingTaxCountry", ObjCustCompany.WithholdingTaxCountry);
            hashPara.Add("@IndiForWithHoldingTaxType", ObjCustCompany.IndiForWithHoldingTaxType);
            hashPara.Add("@WithholdingTaxCode", ObjCustCompany.WithholdingTaxCode);
            hashPara.Add("@IsActive", ObjCustCompany.IsActive);
            hashPara.Add("@UserId", ObjCustCompany.UserId);
            hashPara.Add("@UserIp", ObjCustCompany.IPAddress);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }


        public CompanyCodee GetCompanyCode(int intMasterHeaderId)
        {
            CompanyCodee ObjCustCompany = new CompanyCodee();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_CompanyCode_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", intMasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCustCompany.Cust_CompanyCode_Id = Convert.ToInt32(dt.Rows[0]["Cust_CompanyCode_Id"].ToString());
                        ObjCustCompany.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjCustCompany.Company_Id = dt.Rows[0]["Company_Id"].ToString();
                        ObjCustCompany.Autorization_Gr = dt.Rows[0]["Autorization_Gr"].ToString();
                        ObjCustCompany.ReconciliationAccount = dt.Rows[0]["ReconciliationAccount"].ToString();
                        ObjCustCompany.Planning_Group = dt.Rows[0]["Planning_Group"].ToString();
                        ObjCustCompany.keySortingAssignmen = dt.Rows[0]["keySortingAssignment"].ToString();
                        ObjCustCompany.HeadOfficeAccNumber = dt.Rows[0]["HeadOfficeAccNumber"].ToString();
                        ObjCustCompany.TermsPaymentKey = dt.Rows[0]["TermsPaymentKey"].ToString();
                        ObjCustCompany.ToleranceGroupBussAcc = dt.Rows[0]["ToleranceGroupBussAcc"].ToString();
                        ObjCustCompany.IndicaterRecordPayHis = dt.Rows[0]["IndicaterRecordPayHis"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCustCompany.ListPaymentConsidered = dt.Rows[0]["ListPaymentConsidered"].ToString();
                        ObjCustCompany.BlockKeyPayment = dt.Rows[0]["BlockKeyPayment"].ToString();
                        ObjCustCompany.AccNumberALterPlayer = dt.Rows[0]["AccNumberALterPlayer"].ToString();
                        ObjCustCompany.ShortKeyBank = dt.Rows[0]["ShortKeyBank"].ToString();
                        ObjCustCompany.IndicatorPayAll = Convert.ToInt32(dt.Rows[0]["chkIndicatorPayAll"].ToString());
                        ObjCustCompany.IndiClearingBetwCust = dt.Rows[0]["IndiClearingBetwCust"].ToString();
                        ObjCustCompany.NextPaye = dt.Rows[0]["NextPaye"].ToString();
                        ObjCustCompany.AccountionClerk = dt.Rows[0]["AccountionClerk"].ToString();
                        ObjCustCompany.indicatorPeriodicAccount = Convert.ToInt32(dt.Rows[0]["chkindicatorPeriodicAccount"].ToString());
                        ObjCustCompany.OurAccoCust = dt.Rows[0]["OurAccoCust"].ToString();
                        ObjCustCompany.Memo = dt.Rows[0]["Memo"].ToString();
                        ObjCustCompany.IndiPaymentNotice = dt.Rows[0]["IndiPaymentNotice"].ToString();
                        ObjCustCompany.IndiPayment = dt.Rows[0]["IndiPayment"].ToString();
                        ObjCustCompany.IndipaymentWoCleared = dt.Rows[0]["IndipaymentWoCleared"].ToString();
                        ObjCustCompany.IndiPaymentAccountingDepart = dt.Rows[0]["IndiPaymentAccountingDepart"].ToString();
                        ObjCustCompany.IndiPaymentlegalDepartment = dt.Rows[0]["IndiPaymentlegalDepartment"].ToString();
                        ObjCustCompany.DeletionFlagMasterRecord = dt.Rows[0]["DeletionFlagMasterRecord"].ToString();
                        ObjCustCompany.PostingBlockCompanyCode = dt.Rows[0]["PostingBlockCompanyCode"].ToString();
                        ObjCustCompany.PreviousRecordNumber = dt.Rows[0]["PreviousRecordNumber"].ToString();
                        ObjCustCompany.KeyPaymentGrouping = dt.Rows[0]["KeyPaymentGrouping"].ToString();
                        ObjCustCompany.PaymentTermCreditMemos = dt.Rows[0]["PaymentTermCreditMemos"].ToString();
                        ObjCustCompany.WithholdingTaxCountry = dt.Rows[0]["WithholdingTaxCountry"].ToString();
                        ObjCustCompany.IndiForWithHoldingTaxType = dt.Rows[0]["IndiForWithHoldingTaxType"].ToString();
                        ObjCustCompany.WithholdingTaxCode = dt.Rows[0]["WithholdingTaxCode"].ToString();
                        ObjCustCompany.WitHoldingTaxIdenNumb = dt.Rows[0]["WitHoldingTaxIdenNumb"].ToString();
                    }
                }
                return ObjCustCompany;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

    }
}