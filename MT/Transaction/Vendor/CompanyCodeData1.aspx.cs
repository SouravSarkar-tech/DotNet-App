using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

public partial class Transaction_Vendor_CompanyCodeData1 : BasePage
{
    VendCompanyCodeAccess ObjVendCompanyCodeAccess = new VendCompanyCodeAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                if (Session[StaticKeys.MasterHeaderId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    PopuplateDropDownList();


                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();

                    FillVendCompanyCodeData();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    string mode = Session[StaticKeys.Mode].ToString();
                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                    }
                }
                else
                {
                    Response.Redirect("VendorMaster.aspx");
                }
                ConfigureControl();
                string moduleId1 = Session[StaticKeys.SelectedModuleId].ToString();
                if (moduleId1 == "27")
                {
                    ddlTermPaymentKey.Enabled = false;
                }
            }
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (SaveCompanyCodeData())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one feild.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (SaveCompanyCodeData())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                Response.Redirect("CompanyCodeData1.aspx");
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one feild.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (SaveCompanyCodeData())
            {
                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one feild.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    #endregion

    #region Methods

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        //helperAccess.PopuplateDropDownCheckBox(ddlPaymentMethod, "pr_GetDropDownListByControlNameModuleType 'V','ddlPaymentMethod'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownList(ddlTermPaymentKey, "pr_GetDropDownListByControlNameModuleType 'V','txtTermPaymentKey'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPlanningGroup, "pr_GetDropDownListByControlNameModuleType 'V','txtPlanningGroup'", "LookUp_Desc", "LookUp_Code", "");
    }

    private bool SaveCompanyCodeData()
    {
        bool flg = false;
        try
        {
            VendCompanyCode1 ObjVComp = GetControlsValue();

            if (ObjVendCompanyCodeAccess.Save(ObjVComp) > 0)
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

    private VendCompanyCode1 GetControlsValue()
    {
        VendCompanyCode1 ObjVComp = new VendCompanyCode1();
        Utility objUtil = new Utility();

        try
        {
            ObjVComp.Vendor_Company_Code_Data1_Id = Convert.ToInt32(lblCompanyCodeId.Text);
            ObjVComp.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjVComp.Recon_Acc = txtReconAcc.Text;
            ObjVComp.Key_Sort_Assign_No = txtKeySortAssignNo.Text;
            ObjVComp.Planning_Group = ddlPlanningGroup.SelectedValue;
            ObjVComp.HO_Acc_No = txtHOAccNo.Text;
            ObjVComp.Authorization_grp = txtAuthorizationgrp.Text;
            ObjVComp.Date1 = objUtil.GetMMDDYYYY(txtDate1.Text);
            ObjVComp.Date2 = objUtil.GetMMDDYYYY(txtDate2.Text);
            ObjVComp.Term_Payment_Key = ddlTermPaymentKey.SelectedValue;
            ObjVComp.Tolerance_Grp_GL = txtToleranceGrpGL.Text;
            ObjVComp.Probable_Time_Cheque_Paid = txtProbableTimeChequePaid.Text;
            if (chkIsDoubleInvoice.Enabled || lblActionType.Text != "C")
                ObjVComp.Is_Double_Invoice = chkIsDoubleInvoice.Checked == true ? "1" : "0";
            ObjVComp.Payment_Method = lblListPaymentMethod.Text;// GetSelectedCheckedValue(ddlPaymentMethod);
            ObjVComp.Block_Key_Payment = txtBlockKeyPayment.Text;
            ObjVComp.Acc_No_Alt_Payee = txtAccNoAltPayee.Text;
            ObjVComp.Short_Key_House_Bank = txtShortKeyHouseBank.Text;
            if (chkIsPayAllItemSeperately.Enabled)
                ObjVComp.Is_Pay_All_Item_Seperately = chkIsPayAllItemSeperately.Checked == true ? "1" : "0";
            ObjVComp.Bill_Exchange_Limit = txtBillExchangeLimit.Text;
            if (chkIsClearingCustVend.Enabled)
                ObjVComp.Is_Clearing_Cust_Vend = chkIsClearingCustVend.Checked == true ? "1" : "0";
            if (chkIsMasterRecordDeleted.Enabled)
                ObjVComp.Is_Master_Record_Deleted = chkIsMasterRecordDeleted.Checked == true ? "1" : "0";
            if (chkIsPostingBlockCompanyCode.Enabled)
                ObjVComp.Is_Posting_Block_Company_Code = chkIsPostingBlockCompanyCode.Checked == true ? "1" : "0";
            ObjVComp.Previous_Master_No = txtPreviousMasterNo.Text;
            ObjVComp.Key_Payment_Grp = txtKeyPaymentGrp.Text;
            ObjVComp.Payment_Method_Supp = txtPaymentMethodSupp.Text;
            if (chkIsSendPaymentAdvicesEDI.Enabled)
                ObjVComp.Is_Send_Payment_Advices_EDI = chkIsSendPaymentAdvicesEDI.Checked == true ? "1" : "0";
            ObjVComp.Release_Approval_Grp = txtReleaseApprovalGrp.Text;
            ObjVComp.Personnel_No = txtPersonnelNo.Text;
            ObjVComp.Tolerance_Grp = txtToleranceGrp.Text;
            ObjVComp.Internet_Add_partner = txtInternetAddpartner.Text;
            ObjVComp.Payment_Term_Key_Credit_Meno = txtPaymentTermKeyCreditMeno.Text;
            if (chkIsPeriodicAccStmt.Enabled)
                ObjVComp.Is_Periodic_Acc_Stmt = chkIsPeriodicAccStmt.Checked == true ? "1" : "0";
            ObjVComp.Certi_Date = objUtil.GetMMDDYYYY(txtCertiDate.Text);
            if (chkIsBlockMasterRecordDeletion.Enabled)
                ObjVComp.Is_Block_Master_Record_Deletion = chkIsBlockMasterRecordDeletion.Checked == true ? "1" : "0";
            if (chkIsPrepaymentRelevant.Enabled)
                ObjVComp.Is_Prepayment_Relevant = chkIsPrepaymentRelevant.Checked == true ? "1" : "0";
            ObjVComp.IsActive = 1;
            ObjVComp.UserId = lblUserId.Text;
            ObjVComp.TodayDate = objUtil.GetDate();
            ObjVComp.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjVComp;
    }

    private void FillVendCompanyCodeData()
    {
        try
        {
            VendCompanyCode1 ObjVComp = GetVendCompanyCodeData();
            if (ObjVComp.Vendor_Company_Code_Data1_Id > 0)
            {
                lblCompanyCodeId.Text = ObjVComp.Vendor_Company_Code_Data1_Id.ToString();

                txtReconAcc.Text = ObjVComp.Recon_Acc;
                txtKeySortAssignNo.Text = ObjVComp.Key_Sort_Assign_No;
                txtHOAccNo.Text = ObjVComp.HO_Acc_No;
                ddlPlanningGroup.SelectedValue = ObjVComp.Planning_Group;
                txtAuthorizationgrp.Text = ObjVComp.Authorization_grp;
                txtDate1.Text = ObjVComp.Date1;
                txtDate2.Text = ObjVComp.Date2;
                ddlTermPaymentKey.SelectedValue = ObjVComp.Term_Payment_Key;
                txtToleranceGrpGL.Text = ObjVComp.Tolerance_Grp_GL;
                txtProbableTimeChequePaid.Text = ObjVComp.Probable_Time_Cheque_Paid;
                chkIsDoubleInvoice.Checked = ObjVComp.Is_Double_Invoice == "1" ? true : false;
                //SetSelectedValue(ddlPaymentMethod, ObjVComp.Payment_Method);
                //lblListPaymentMethod.Text = "List of the Payment Methods : " + ObjVComp.Payment_Method.Replace(",", "");
                txtBlockKeyPayment.Text = ObjVComp.Block_Key_Payment;
                txtAccNoAltPayee.Text = ObjVComp.Acc_No_Alt_Payee;
                txtShortKeyHouseBank.Text = ObjVComp.Short_Key_House_Bank;
                chkIsPayAllItemSeperately.Checked = ObjVComp.Is_Pay_All_Item_Seperately == "1" ? true : false;
                txtBillExchangeLimit.Text = ObjVComp.Bill_Exchange_Limit;
                chkIsClearingCustVend.Checked = ObjVComp.Is_Clearing_Cust_Vend == "1" ? true : false;
                chkIsMasterRecordDeleted.Checked = ObjVComp.Is_Master_Record_Deleted == "1" ? true : false;
                chkIsPostingBlockCompanyCode.Checked = ObjVComp.Is_Posting_Block_Company_Code == "1" ? true : false;
                txtPreviousMasterNo.Text = ObjVComp.Previous_Master_No;
                txtKeyPaymentGrp.Text = ObjVComp.Key_Payment_Grp;
                txtPaymentMethodSupp.Text = ObjVComp.Payment_Method_Supp;
                chkIsSendPaymentAdvicesEDI.Checked = ObjVComp.Is_Send_Payment_Advices_EDI == "1" ? true : false;
                txtReleaseApprovalGrp.Text = ObjVComp.Release_Approval_Grp;
                txtPersonnelNo.Text = ObjVComp.Personnel_No;
                txtToleranceGrp.Text = ObjVComp.Tolerance_Grp;
                txtInternetAddpartner.Text = ObjVComp.Internet_Add_partner;
                txtPaymentTermKeyCreditMeno.Text = ObjVComp.Payment_Term_Key_Credit_Meno;
                chkIsPeriodicAccStmt.Checked = ObjVComp.Is_Periodic_Acc_Stmt == "1" ? true : false;
                txtCertiDate.Text = ObjVComp.Certi_Date;
                chkIsBlockMasterRecordDeletion.Checked = ObjVComp.Is_Block_Master_Record_Deletion == "1" ? true : false;
                chkIsPrepaymentRelevant.Checked = ObjVComp.Is_Prepayment_Relevant == "1" ? true : false;

            }
            else
            {
                lblCompanyCodeId.Text = "0";
                if (lblActionType.Text != "C")
                {
                    txtReconAcc.Text = ObjVComp.Recon_Acc;
                    chkIsDoubleInvoice.Checked = true;

                    if (lblModuleId.Text == "18" || lblModuleId.Text == "19")
                    {
                        ddlPlanningGroup.SelectedValue = "A6";
                    }
                    else if (lblModuleId.Text == "27")
                    {
                        ddlPlanningGroup.SelectedValue = "A7";
                        ddlPlanningGroup.Enabled = false;
                        txtPersonnelNo.Text = ObjVComp.Personnel_No;
                        if (txtPersonnelNo.Text != "")
                            txtPersonnelNo.Enabled = false; 
                        txtShortKeyHouseBank.Text = "CIT06";
                        txtShortKeyHouseBank.Enabled = false;

                        try { 
                        //LH01DT06052021 Add Start
                        if (Session[StaticKeys.SelectedModuleId].ToString() != "32")
                        {
                            txtShortKeyHouseBank.Text = "";
                            txtShortKeyHouseBank.Text = "CIT23";
                        }
                            //LH01DT06052021 Add end
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        ddlPlanningGroup.SelectedValue = "A3";
                    }
                }
            }

            if (lblActionType.Text != "C")
            {
                if (lblModuleId.Text == "27")
                {
                    lblListPaymentMethod.Text = "CET";
                    lblListPaymentMethod1.Text = "C-Cheque,E-Cash Payment, T- Bank Transfer";
                }
                else
                {
                    lblListPaymentMethod.Text = "CT";
                    lblListPaymentMethod1.Text = "C-Cheque, T- Bank Transfer";
                }

                if (txtReconAcc.Text != "")
                {
                    txtReconAcc.Enabled = false;
                }
            }
            
            txtHOAccNo.Focus();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private VendCompanyCode1 GetVendCompanyCodeData()
    {
        return ObjVendCompanyCodeAccess.GetVendCompanyCode1(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private void ConfigureControl()
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Company_Code_Data_1 obj = new SectionConfiguration.Company_Code_Data_1();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (lblActionType.Text != "C")
            flag = true;
        else
        {
            if (txtReconAcc.Text != "")
                flag = true;
            else if (ddlPlanningGroup.SelectedValue != "")
                flag = true;
            else if (ddlTermPaymentKey.SelectedValue != "")
                flag = true;
            else if (txtAccNoAltPayee.Text != "")
                flag = true;
            else if (txtHOAccNo.Text != "")
                flag = true;
            else if (txtPersonnelNo.Text != "")
                flag = true;
            else if (txtPreviousMasterNo.Text != "")
                flag = true;
            else if (txtShortKeyHouseBank.Text != "")
                flag = true;
        }

        return flag;
    }
    
    #endregion

}