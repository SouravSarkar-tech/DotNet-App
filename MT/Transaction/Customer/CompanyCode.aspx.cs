using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using Accenture.MWTT.DomainObject;
using System.Data;

public partial class Transaction_Customer_CompanyCode : BasePage
{
    #region Page Events

    CompanyCodeAccess ObjCompanyCodeAccess = new CompanyCodeAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.MasterHeaderId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    PopuplateDropDownList();
                    FillCompanyCode();
                    // ClearData();

                    ConfigureControl();

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                    }
                }
                else
                {
                    Response.Redirect("MaterialMaster.aspx");
                }
            }
        }
    }

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        ClearData();
    }



    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (SaveCompanyCodeData())
        {
            string pageURL = btnPrevious.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveCompanyCodeData())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SaveCompanyCodeData())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    #endregion

    #region Method

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplateCompanyList(ddlCompany, lblMasterHeaderId.Text, "CC1", lblAccountingId.Text);
        helperAccess.PopuplateDropDownList(ddlReconAcc, "pr_GetReconAccList '" + lblMasterHeaderId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPlanningGroup, "pr_GetDropDownListByControlNameModuleType 'C','ddlPlanningGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlTermPaymentKey, "pr_GetDropDownListByControlNameModuleType 'C','ddlTermPaymentKey'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlShortKeyBank, "pr_GetDropDownListByControlNameModuleType 'C','ddlShortKeyBank'", "LookUp_Desc", "LookUp_Code", "");

    }

    private void ClearData()
    {
        lblAccountingId.Text = "0";
        ddlReconAcc.SelectedValue = "";
        txtkeySortingAssignment.Text = "";
        txtHeadOfficeAccNumber.Text = "";
        ddlTermPaymentKey.SelectedValue = "";
        txtToleranceGroupBussAcc.Text = "";
        chkIndicaterRecordPayHis.Checked = false;
        //txtListPaymentConsidered.Text = "";
        txtBlockKeyPayment.Text = "";
        txtAccNumberALterPlayer.Text = "";
        ddlShortKeyBank.SelectedValue = "";
        chkIndicatorPayAll.Text = "";
        txtIndiClearingBetwCust.Text = "";
        txtNextPaye.Text = "";
        txtAccountionClerk.Text = "";
        chkindicatorPeriodicAccount.Text = "";
        txtOurAccoCust.Text = "";
        txtMemo.Text = "";
        txtIndiPaymentNotice.Text = "";
        txtIndiPayment.Text = "";
        txtIndipaymentWoCleared.Text = "";
        txtIndiPaymentAccountingDepart.Text = "";
        txtIndiPaymentlegalDepartment.Text = "";
        txtDeletionFlagMasterRecord.Text = "";
        txtPostingBlockCompanyCode.Text = "";
        txtPreviousRecordNumber.Text = "";
        txtKeyPaymentGrouping.Text = "";
        txtPaymentTermCreditMemos.Text = "";
        txtWithholdingTaxCountry.Text = "";
        txtIndiForWithHoldingTaxType.Text = "";
        txtWithholdingTaxCode.Text = "";
        txtWitHoldingTaxIdenNumb.Text = "";
        PopuplateDropDownList();
    }

    private bool SaveCompanyCodeData()
    {
        bool flg = false;
        CompanyCodee ObjCompanyCode = GetControlsValue();
        try
        {
            if (ObjCompanyCodeAccess.Save(ObjCompanyCode) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private CompanyCodee GetControlsValue()
    {
        CompanyCodee ObjComp = new CompanyCodee();
        Utility objUtil = new Utility();

        ObjComp.Cust_CompanyCode_Id = Convert.ToInt32(lblCustomerGeneralId.Text);
        ObjComp.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjComp.Company_Id = GetSelectedCheckedValue(ddlCompany);
        ObjComp.IsActive = 1;
        ObjComp.ReconciliationAccount = ddlReconAcc.SelectedValue;
        ObjComp.Autorization_Gr = txtAuthorizationGroup.Text;
        ObjComp.Planning_Group = ddlPlanningGroup.SelectedValue;
        ObjComp.keySortingAssignmen = txtkeySortingAssignment.Text;
        ObjComp.HeadOfficeAccNumber = txtHeadOfficeAccNumber.Text;
        ObjComp.TermsPaymentKey = ddlTermPaymentKey.SelectedValue;
        ObjComp.ToleranceGroupBussAcc = txtToleranceGroupBussAcc.Text;
        ObjComp.IndicaterRecordPayHis = chkIndicaterRecordPayHis.Checked == true ? "1" : "0";
        ObjComp.ListPaymentConsidered = "";// txtListPaymentConsidered.Text;
        ObjComp.BlockKeyPayment = txtBlockKeyPayment.Text;
        ObjComp.AccNumberALterPlayer = txtAccNumberALterPlayer.Text;
        ObjComp.ShortKeyBank = ddlShortKeyBank.SelectedValue;
        ObjComp.IndicatorPayAll = chkIndicatorPayAll.Checked == true ? 1 : 0;
        ObjComp.IndiClearingBetwCust = txtIndiClearingBetwCust.Text;
        ObjComp.NextPaye = txtNextPaye.Text;
        ObjComp.AccountionClerk = txtAccountionClerk.Text;
        ObjComp.indicatorPeriodicAccount = chkindicatorPeriodicAccount.Checked == true ? 1 : 0;
        ObjComp.OurAccoCust = txtOurAccoCust.Text;
        ObjComp.Memo = txtMemo.Text;
        ObjComp.IndiPaymentNotice = txtIndiPaymentNotice.Text;
        ObjComp.IndiPayment = txtIndiPayment.Text;
        ObjComp.IndipaymentWoCleared = txtIndipaymentWoCleared.Text;
        ObjComp.IndiPaymentAccountingDepart = txtIndiPaymentAccountingDepart.Text;
        ObjComp.IndiPaymentlegalDepartment = txtIndiPaymentlegalDepartment.Text;
        ObjComp.DeletionFlagMasterRecord = txtDeletionFlagMasterRecord.Text;
        ObjComp.PostingBlockCompanyCode = txtPostingBlockCompanyCode.Text;
        ObjComp.PreviousRecordNumber = txtPreviousRecordNumber.Text;
        ObjComp.KeyPaymentGrouping = txtKeyPaymentGrouping.Text;
        ObjComp.PaymentTermCreditMemos = txtPaymentTermCreditMemos.Text;
        ObjComp.WithholdingTaxCountry = txtWithholdingTaxCountry.Text;
        ObjComp.IndiForWithHoldingTaxType = txtIndiForWithHoldingTaxType.Text;
        ObjComp.WithholdingTaxCode = txtWithholdingTaxCode.Text;
        ObjComp.WitHoldingTaxIdenNumb = txtWitHoldingTaxIdenNumb.Text;
        ObjComp.IsActive = 1;
        ObjComp.UserId = lblUserId.Text;
        ObjComp.TodayDate = objUtil.GetDate();
        ObjComp.IPAddress = objUtil.GetIpAddress();
        return ObjComp;
    }

    private CompanyCodee GetCompanyCode()
    {
        return ObjCompanyCodeAccess.GetCompanyCode(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private void FillCompanyCode()
    {
        CompanyCodee ObjComp = GetCompanyCode();
        Utility objUtil = new Utility();
        if (ObjComp.Cust_CompanyCode_Id > 0)
        {
            lblCustomerGeneralId.Text = ObjComp.Cust_CompanyCode_Id.ToString();
            lblMasterHeaderId.Text = ObjComp.Master_Header_Id.ToString();
            SetSelectedValue(ddlCompany, ObjComp.Company_Id);
            ddlReconAcc.SelectedValue = ObjComp.ReconciliationAccount;
            txtkeySortingAssignment.Text = ObjComp.keySortingAssignmen;
            ddlPlanningGroup.SelectedValue = ObjComp.Planning_Group;
            txtHeadOfficeAccNumber.Text = ObjComp.HeadOfficeAccNumber;
            ddlTermPaymentKey.SelectedValue = ObjComp.TermsPaymentKey;
            txtToleranceGroupBussAcc.Text = ObjComp.ToleranceGroupBussAcc;
            chkIndicaterRecordPayHis.Checked = ObjComp.IndicaterRecordPayHis == "1" ? true : false;
            //txtListPaymentConsidered.Text = "";// ObjComp.ListPaymentConsidered;
            txtBlockKeyPayment.Text = ObjComp.BlockKeyPayment;
            txtAccNumberALterPlayer.Text = ObjComp.AccNumberALterPlayer;
            ddlShortKeyBank.SelectedValue = ObjComp.ShortKeyBank;
            chkIndicatorPayAll.Checked = ObjComp.IndicatorPayAll == 1 ? true : false;
            txtIndiClearingBetwCust.Text = ObjComp.IndiClearingBetwCust;
            txtNextPaye.Text = ObjComp.NextPaye;
            txtAccountionClerk.Text = ObjComp.AccountionClerk;
            chkindicatorPeriodicAccount.Checked = ObjComp.indicatorPeriodicAccount == 1 ? true : false;
            txtOurAccoCust.Text = ObjComp.OurAccoCust;
            txtMemo.Text = ObjComp.Memo;
            txtIndiPaymentNotice.Text = ObjComp.IndiPaymentNotice;
            txtIndiPayment.Text = ObjComp.IndiPayment;
            txtIndipaymentWoCleared.Text = ObjComp.IndipaymentWoCleared;
            txtIndiPaymentAccountingDepart.Text = ObjComp.IndiPaymentAccountingDepart;
            txtIndiPaymentlegalDepartment.Text = ObjComp.IndiPaymentlegalDepartment;
            txtDeletionFlagMasterRecord.Text = ObjComp.DeletionFlagMasterRecord;
            txtPostingBlockCompanyCode.Text = ObjComp.PostingBlockCompanyCode;
            txtPreviousRecordNumber.Text = ObjComp.PreviousRecordNumber;
            txtKeyPaymentGrouping.Text = ObjComp.KeyPaymentGrouping;
            txtAuthorizationGroup.Text = ObjComp.Autorization_Gr;
            txtPaymentTermCreditMemos.Text = ObjComp.PaymentTermCreditMemos;
            txtWithholdingTaxCountry.Text = ObjComp.WithholdingTaxCountry;
            txtIndiForWithHoldingTaxType.Text = ObjComp.IndiForWithHoldingTaxType;
            txtWithholdingTaxCode.Text = ObjComp.WithholdingTaxCode;
            txtWitHoldingTaxIdenNumb.Text = ObjComp.WitHoldingTaxIdenNumb;
            ObjComp.TodayDate = objUtil.GetDate();
            ObjComp.IPAddress = objUtil.GetIpAddress();
        }
        else
        {
            lblCustomerGeneralId.Text = "0";

            chkIndicaterRecordPayHis.Checked = true;
            //txtAuthorizationGroup.Text = "LUPI";
            txtAuthorizationGroup.Text = ObjComp.Autorization_Gr;

            if (ddlReconAcc.Items.Count == 2)
            {
                ddlReconAcc.SelectedIndex = 1;
            }
        }
        txtAuthorizationGroup.Enabled = false;
        chkIndicaterRecordPayHis.Enabled = false;
    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Company_code_data obj = new SectionConfiguration.Company_code_data();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    #endregion
}