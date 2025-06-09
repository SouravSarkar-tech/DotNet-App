using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

public partial class Transaction_Vendor_CompanyCodeData2 : System.Web.UI.Page
{
    VendCompanyCodeAccess ObjVendCompanyCodeAccess = new VendCompanyCodeAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Events

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


                    string mode = Session[StaticKeys.Mode].ToString();
                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();

                    FillVendCompanyCodeData();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                        lnkAddBank.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("VendorMaster.aspx");
                }
                ConfigureControl();
                //Start change by Swati on 03.01.2019
                reqtxtReasonNonBankDet.Enabled = false;
                labeltxtReasonNonBankDet.Visible = false;
                txtReasonNonBankDet.Enabled = false;
                setConfig();
                //End Change
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
            if (ddlBankDetailsReq.SelectedValue == "1")
            {
                txtReasonNonBankDet.Enabled = false;
                reqtxtBankKey.Enabled = true;
                labletxtBankKey.Visible = true;
                reqtxtBankAccNo.Enabled = true;
                labletxtBankAccNo.Visible = true;
                reqtxtAccountHolderName.Enabled = true;
                labletxtAccountHolderName.Visible = true;
                if (txtBankKey.Text == "")
                {
                    string message = "alert('Bank Key cannot be blank.')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                }
                else if (txtBankAccNo.Text == "")
                {
                    string message = "alert('Bank account number cannot be blank.')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                }
                else if (txtAccountHolderName.Text == "")
                {
                    string message = "alert('Account Holder Name cannot be blank.')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                }
                else
                {
                    reqtxtBankAccNo.Enabled = false;
                    reqtxtAccountHolderName.Enabled = false;
                    labletxtAccountHolderName.Visible = false;
                    labletxtBankAccNo.Visible = false;

                    if (SaveCompanyCodeData())
                    {
                        lblMsg.Text = Messages.GetMessage(1);
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;

                        Response.Redirect("CompanyCodeData2.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "Please fill atleast one feild.";
                        pnlMsg.Visible = true;
                        pnlMsg.CssClass = "error";
                    }
                }
            }

            else if (SaveCompanyCodeData())
            {
                if (ddlBankDetailsReq.SelectedValue == "0")
                {
                    txtReasonNonBankDet.Enabled = true;
                    reqtxtReasonNonBankDet.Enabled = true;
                    labeltxtReasonNonBankDet.Visible = true;
                    if (txtReasonNonBankDet.Text == "")
                    {
                        string message = "alert('Reason for not maintaining Bank Details cannot be blank.')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        lblMsg.Text = Messages.GetMessage(1);
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;

                        Response.Redirect("CompanyCodeData2.aspx");
                    }
                }

            }
            else
            {
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
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
            //Start Change By Swati on 03.01.2019
            if (ddlBankDetailsReq.SelectedValue == "1")
            {
                txtReasonNonBankDet.Enabled = false;
                reqtxtBankKey.Enabled = true;
                labletxtBankKey.Visible = true;
                reqtxtBankAccNo.Enabled = true;
                labletxtBankAccNo.Visible = true;
                reqtxtAccountHolderName.Enabled = true;
                labletxtAccountHolderName.Visible = true;
                if (txtBankKey.Text == "")
                {
                    string message = "alert('Bank Key cannot be blank.')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                }
                else if (txtBankAccNo.Text == "")
                {
                    string message = "alert('Bank account number cannot be blank.')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                }
                else if (txtAccountHolderName.Text == "")
                {
                    string message = "alert('Account Holder Name cannot be blank.')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                }
                else
                {
                    reqtxtBankAccNo.Enabled = false;
                    reqtxtAccountHolderName.Enabled = false;
                    labletxtAccountHolderName.Visible = false;
                    labletxtBankAccNo.Visible = false;
                    if (SaveCompanyCodeData())
                    {
                        string pageURL = btnNext.CommandArgument.ToString();
                        Response.Redirect(pageURL);
                    }
                    else
                    {
                        lblMsg.Text = "Please fill atleast one feild.";
                        pnlMsg.Visible = true;
                        pnlMsg.CssClass = "error";
                    }
                }
            }
            //if(txtBankKey.Text != "")
            //{
            //    reqtxtBankAccNo.Enabled = true;
            //    reqtxtAccountHolderName.Enabled = true;
            //    labletxtAccountHolderName.Visible = true;
            //    labletxtBankAccNo.Visible = true;
            //    if (txtBankAccNo.Text == "")
            //    {
            //        string message = "alert('Bank account number cannot be blank.')";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
            //    }
            //    else if (txtAccountHolderName.Text == "")
            //    {
            //        string message = "alert('Account Holder Name cannot be blank.')";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
            //    }
            //    else
            //    {
            //        reqtxtBankAccNo.Enabled = false;
            //        reqtxtAccountHolderName.Enabled = false;
            //        labletxtAccountHolderName.Visible = false;
            //        labletxtBankAccNo.Visible = false;
            //        string pageURL = btnNext.CommandArgument.ToString();
            //        Response.Redirect(pageURL);
            //    }
            //}
            else if (SaveCompanyCodeData())
            {
                if (ddlBankDetailsReq.SelectedValue == "0")
                {
                    txtReasonNonBankDet.Enabled = true;
                    reqtxtReasonNonBankDet.Enabled = true;
                    labeltxtReasonNonBankDet.Visible = true;
                    if (txtReasonNonBankDet.Text == "")
                    {
                        string message = "alert('Reason for not maintaining Bank Details cannot be blank.')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        string pageURL = btnNext.CommandArgument.ToString();
                        Response.Redirect(pageURL);
                    }
                }

            }
            else
            //End Change
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

    protected void BtnBankSave_Click(object sender, EventArgs e)
    {
        ucBankMaster1.RefMhId = lblMasterHeaderId.Text;
        if (ucBankMaster1.Save())
        {
            ucBankMaster1.ApproveRequest();
            lblMsg.Text = "Bank Data Entered Sucessfully";
            pnlMsg.CssClass = "sucess";
            pnlMsg.Visible = true;

            BankMasterAccess ObjBankMasterAccess = new BankMasterAccess();
            BankMaster ObjBankMaster = ObjBankMasterAccess.GetBankMasterByRefMasterHeaderId(lblMasterHeaderId.Text);
            ddlBankCountrykey.SelectedValue = ObjBankMaster.Country_Id.ToString();
            helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlBankCountrykey.SelectedValue, "Region_Name", "Region_Id");

            if (ObjBankMaster.Bank_Id > 0)
            {
                txtBankKey.Text = ObjBankMaster.Swift;
                AssignBankValues(ObjBankMaster);
            }

            pnlNewBank.Visible = false;
            trBank.Visible = false;
            txtAccountHolderName.Focus();
        }
    }

    protected void btnBankCancel_Click(object sender, EventArgs e)
    {
        ucBankMaster1.ClearData();
        pnlNewBank.Visible = false;
        trBank.Visible = false;

        txtBankKey.Focus();
    }

    protected void lnkAddBank_Click(object sender, EventArgs e)
    {
        ucBankMaster1.RefMhId = lblMasterHeaderId.Text;
        ucBankMaster1.SetStartData();
        pnlNewBank.Visible = true;
        trBank.Visible = true;
    }

    protected void txtBankKey_TextChanged(object sender, EventArgs e)
    {
        txtBankName.Text = "";
        txtHouseNoStreet.Text = "";
        txtBankNo.Text = "";

        if (txtBankKey.Text == "")
        {
            ConfigureControl();
        }
        else
        {
            //Start Change by Swati on 03.01.2019
            reqtxtBankAccNo.Enabled = true;
            reqtxtAccountHolderName.Enabled = true;
            labletxtAccountHolderName.Visible = true;
            labletxtBankAccNo.Visible = true;
            //End Change
            BankMasterAccess ObjBankMasterAccess = new BankMasterAccess();
            BankMaster ObjBankMaster = ObjBankMasterAccess.GetBankMasterBySwift(txtBankKey.Text);
            if (ObjBankMaster.Swift == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Invalid Input.');", true);
                ObjBankMaster = null;
            }
            AssignBankValues(ObjBankMaster);
        }
        txtAccountHolderName.Focus();
    }

    protected void ddlWHTCountryKey_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtWHTIdentificationNo.Text = "";
        txtWHTExemptCertNo.Text = "";
        txtExemptionRateBatchInp.Text = "";
        txtWTExemptFromDate.Text = "";
        txtWTExemptToDate.Text = "";
        ddlExemptionReason.SelectedValue = "";

        txtWHTIdentificationNo2.Text = "";
        txtWHTExemptCertNo2.Text = "";
        txtExemptionRateBatchInp2.Text = "";
        txtWTExemptFromDate2.Text = "";
        txtWTExemptToDate2.Text = "";
        ddlExemptionReason2.SelectedValue = "";

        ddlIndicatorWHTType.Focus();
    }

    protected void ddlIndicatorWHTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIndicatorWHTType_Manage();
        if (ddlIndicatorWHTType.SelectedValue != "")
        {
            rdlIsSubjectWHT.SelectedValue = "1";
            rdlIsSubjectWHT.Enabled = false;
            ddlWHTCountryKey.SelectedValue = "1";
            ddlWHTCountryKey.Enabled = false;
        }
        else
        {
            rdlIsSubjectWHT.SelectedItem.Selected = false;
            ddlWHTCountryKey.SelectedValue = "0";
        }
        ddlIndicatorWHTType2.Enabled = true;
        ddlWithHoldingTaxCode.Focus();
    }

    protected void ddlIndicatorWHTType2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIndicatorWHTType2_Manage();
        ddlIndicatorWHTType3.Enabled = true;
        ddlWithHoldingTaxCode2.Focus();
    }

    protected void ddlIndicatorWHTType3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIndicatorWHTType3_Manage();
        ddlIndicatorWHTType4.Enabled = true;
        ddlWithHoldingTaxCode3.Focus();
    }

    protected void ddlIndicatorWHTType4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIndicatorWHTType4_Manage();
        ddlIndicatorWHTType5.Enabled = true;
        ddlWithHoldingTaxCode4.Focus();
    }

    protected void ddlIndicatorWHTType5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIndicatorWHTType5_Manage();
        ddlIndicatorWHTType6.Enabled = true;
        ddlWithHoldingTaxCode5.Focus();
    }

    protected void ddlIndicatorWHTType6_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIndicatorWHTType6_Manage();
        ddlWithHoldingTaxCode6.Focus();
    }

    //Code added by Swati on 03.01.2019
    protected void ddlBankDetailsReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        setConfig();
    }
    //End

    #endregion

    #region Methods

    protected void ddlIndicatorWHTType_Manage()
    {
        helperAccess.PopuplateComboBox(ddlTypeRecipient, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        if (ddlIndicatorWHTType.SelectedValue != "")
        {
            reqddlWithHoldingTaxCode.Enabled = true;
            reqddlWithHoldingTaxCode.Visible = true;
        }
        else
        {
            reqddlWithHoldingTaxCode.Enabled = false;
        }

    }

    protected void ddlIndicatorWHTType2_Manage()
    {
        helperAccess.PopuplateComboBox(ddlTypeRecipient2, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType2.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode2, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType2.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");

        if (ddlIndicatorWHTType2.SelectedValue != "")
        {
            reqddlWithHoldingTaxCode2.Enabled = true;
            reqddlWithHoldingTaxCode2.Visible = true;
        }
        else
        {
            reqddlWithHoldingTaxCode2.Enabled = false;
        }
    }

    protected void ddlIndicatorWHTType3_Manage()
    {
        helperAccess.PopuplateComboBox(ddlTypeRecipient3, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType3.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode3, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType3.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");

        if (ddlIndicatorWHTType3.SelectedValue != "")
        {
            reqddlWithHoldingTaxCode3.Enabled = true;
            reqddlWithHoldingTaxCode3.Visible = true;
        }
        else
        {
            reqddlWithHoldingTaxCode3.Enabled = false;
        }
    }

    protected void ddlIndicatorWHTType4_Manage()
    {
        helperAccess.PopuplateComboBox(ddlTypeRecipient4, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType4.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode4, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType4.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        if (ddlIndicatorWHTType4.SelectedValue != "")
        {
            reqddlWithHoldingTaxCode4.Enabled = true;
            reqddlWithHoldingTaxCode4.Visible = true;
        }
        else
        {
            reqddlWithHoldingTaxCode4.Enabled = false;
        }
    }

    protected void ddlIndicatorWHTType5_Manage()
    {
        helperAccess.PopuplateComboBox(ddlTypeRecipient5, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType5.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode5, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType5.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");

        if (ddlIndicatorWHTType5.SelectedValue != "")
        {
            reqddlWithHoldingTaxCode5.Enabled = true;
            reqddlWithHoldingTaxCode5.Visible = true;
        }
        else
        {
            reqddlWithHoldingTaxCode5.Enabled = false;
        }
    }

    protected void ddlIndicatorWHTType6_Manage()
    {
        helperAccess.PopuplateComboBox(ddlTypeRecipient6, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType6.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode6, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType6.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
        if (ddlIndicatorWHTType6.SelectedValue != "")
        {
            reqddlWithHoldingTaxCode6.Enabled = true;
            reqddlWithHoldingTaxCode6.Visible = true;
        }
        else
        {
            reqddlWithHoldingTaxCode6.Enabled = false;
        }


    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlBankCountrykey, "pr_Get_CountryList", "Country_Name", "Country_Id");
        //helperAccess.PopuplateComboBox(ddlWHTCountryKey, "pr_Get_CountryList", "Country_Name", "Country_Id");
        ddlWHTCountryKey.Items.Insert(1, new ListItem("IN - India", "1"));

        helperAccess.PopuplateComboBox(ddlIndicatorWHTType, "pr_GetDropDownListByControlNameModuleType 'V','ddlIndicatorWHTType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlIndicatorWHTType2, "pr_GetDropDownListByControlNameModuleType 'V','ddlIndicatorWHTType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlIndicatorWHTType3, "pr_GetDropDownListByControlNameModuleType 'V','ddlIndicatorWHTType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlIndicatorWHTType4, "pr_GetDropDownListByControlNameModuleType 'V','ddlIndicatorWHTType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlIndicatorWHTType5, "pr_GetDropDownListByControlNameModuleType 'V','ddlIndicatorWHTType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlIndicatorWHTType6, "pr_GetDropDownListByControlNameModuleType 'V','ddlIndicatorWHTType'", "LookUp_Desc", "LookUp_Code", "");

    }

    protected void AssignBankValues(BankMaster ObjBankMaster)
    {
        if (ObjBankMaster != null)
        {
            ddlBankCountrykey.SelectedValue = ObjBankMaster.Country_Id.ToString();
            helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlBankCountrykey.SelectedValue, "Region_Name", "Region_Id");
            ddlRegion.SelectedValue = ObjBankMaster.Region_Id.ToString();
            txtBankName.Text = ObjBankMaster.Bank_Name;
            txtHouseNoStreet.Text = ObjBankMaster.House_No_Street;
            txtBankNo.Text = ObjBankMaster.Bank_Number;

            reqtxtAccountHolderName.Enabled = true;
            reqtxtBankAccNo.Enabled = true;
        }
        else
        {
            txtBankKey.Text = "";
            ddlRegion.SelectedValue = "0";
            txtBankName.Text = "";
            txtHouseNoStreet.Text = "";
            txtBankNo.Text = "";
        }
    }

    private bool SaveCompanyCodeData()
    {
        bool flg = true;
        try
        {
            VendCompanyCode2 ObjVComp = GetControlsValue();

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

    private VendCompanyCode2 GetControlsValue()
    {
        VendCompanyCode2 ObjVComp = new VendCompanyCode2();
        Utility objUtil = new Utility();

        try
        {
            ObjVComp.Vendor_Company_Code_Data2_Id = Convert.ToInt32(lblCompanyCodeId.Text);
            ObjVComp.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjVComp.Bank_Country_key = ddlBankCountrykey.SelectedValue;
            ObjVComp.Bank_key = txtBankKey.Text;
            ObjVComp.Bank_Acc_No = txtBankAccNo.Text;
            ObjVComp.Account_Holder_Name = txtAccountHolderName.Text;
            ObjVComp.Bank_Control_Key = txtBankControlKey.Text;
            ObjVComp.Partner_Bank_Type = txtPartnerBankType.Text;
            ObjVComp.Bank_Name = txtBankName.Text;
            ObjVComp.House_No_Street = txtHouseNoStreet.Text;
            ObjVComp.Bank_No = txtBankNo.Text;
            ObjVComp.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            ObjVComp.Account_Number_Alternative_Payee = txtAccount_Number_Alternative_Payee.Text;
            ObjVComp.KOV_Date = objUtil.GetYYYYMMDD(txtKOVDate.Text);
            ObjVComp.KOB_Issue_Date = objUtil.GetYYYYMMDD(txtKOBIssueDate.Text);
            ObjVComp.InterN_Bank_Acc_No = txtInterNBankAccNo.Text;
            ObjVComp.Valid_From_Date = objUtil.GetYYYYMMDD(txtValidFromDate.Text);
            ObjVComp.GM_Valid_Date = objUtil.GetYYYYMMDD(txtGMValidDate.Text);
            ObjVComp.M_Date = objUtil.GetYYYYMMDD(txtMDate.Text);
            ObjVComp.Indicator_WHT_Type = ddlIndicatorWHTType.SelectedValue;
            if (rdlIsSubjectWHT.SelectedItem != null)
                ObjVComp.Is_Subject_WHT = rdlIsSubjectWHT.SelectedValue.ToString();
            else
                ObjVComp.Is_Subject_WHT = null;
            //ObjVComp.Is_Subject_WHT = chkIsSubjectWHT.Checked == true ? 1 : 0;
            ObjVComp.Vend_Receipt_Type = txtVendReceiptType.Text;
            ObjVComp.Auth_Exemption_WHT = txtAuthExemptionWHT.Text;
            ObjVComp.WHT_Country_Key = ddlWHTCountryKey.Text;
            ObjVComp.Type_Recipient = ddlTypeRecipient.SelectedValue;
            ObjVComp.WithHolding_Tax_Code = ddlWithHoldingTaxCode.SelectedValue;
            ObjVComp.WHT_Exempt_Cert_No = txtWHTExemptCertNo.Text;
            ObjVComp.Date3 = objUtil.GetYYYYMMDD(txtDate3.Text);
            ObjVComp.WHT_Identification_No = txtWHTIdentificationNo.Text;
            ObjVComp.WHT_Code = "";// txtWHTCode.Text;
            ObjVComp.Exemption_Cert_No = "";// txtExemptionCertNo.Text;
            ObjVComp.Exemption_Rate_Batch_Inp = txtExemptionRateBatchInp.Text;
            ObjVComp.WT_Exempt_From_Date = objUtil.GetYYYYMMDD(txtWTExemptFromDate.Text);
            ObjVComp.WT_Exempt_To_Date = objUtil.GetYYYYMMDD(txtWTExemptToDate.Text);
            ObjVComp.Exemption_Reason = ddlExemptionReason.Text;

            ObjVComp.Indicator_WHT_Type2 = ddlIndicatorWHTType2.SelectedValue;
            ObjVComp.WHT_Identification_No2 = txtWHTIdentificationNo2.Text;
            ObjVComp.Type_Recipient2 = ddlTypeRecipient2.SelectedValue;
            ObjVComp.WithHolding_Tax_Code2 = ddlWithHoldingTaxCode2.SelectedValue;
            ObjVComp.WHT_Exempt_Cert_No2 = txtWHTExemptCertNo2.Text;
            ObjVComp.Exemption_Rate_Batch_Inp2 = txtExemptionRateBatchInp2.Text;
            ObjVComp.WT_Exempt_From_Date2 = objUtil.GetYYYYMMDD(txtWTExemptFromDate2.Text);
            ObjVComp.WT_Exempt_To_Date2 = objUtil.GetYYYYMMDD(txtWTExemptToDate2.Text);
            ObjVComp.Exemption_Reason2 = ddlExemptionReason2.Text;

            ObjVComp.Indicator_WHT_Type3 = ddlIndicatorWHTType3.SelectedValue;
            ObjVComp.Type_Recipient3 = ddlTypeRecipient3.SelectedValue;
            ObjVComp.WithHolding_Tax_Code3 = ddlWithHoldingTaxCode3.SelectedValue;
            ObjVComp.WHT_Exempt_Cert_No3 = txtWHTExemptCertNo3.Text;
            ObjVComp.Exemption_Rate_Batch_Inp3 = txtExemptionRateBatchInp3.Text;
            ObjVComp.WT_Exempt_From_Date3 = objUtil.GetYYYYMMDD(txtWTExemptFromDate3.Text);
            ObjVComp.WT_Exempt_To_Date3 = objUtil.GetYYYYMMDD(txtWTExemptToDate3.Text);

            ObjVComp.Indicator_WHT_Type4 = ddlIndicatorWHTType4.SelectedValue;
            ObjVComp.Type_Recipient4 = ddlTypeRecipient4.SelectedValue;
            ObjVComp.WithHolding_Tax_Code4 = ddlWithHoldingTaxCode4.SelectedValue;
            ObjVComp.WHT_Exempt_Cert_No4 = txtWHTExemptCertNo4.Text;
            ObjVComp.Exemption_Rate_Batch_Inp4 = txtExemptionRateBatchInp4.Text;
            ObjVComp.WT_Exempt_From_Date4 = objUtil.GetYYYYMMDD(txtWTExemptFromDate4.Text);
            ObjVComp.WT_Exempt_To_Date4 = objUtil.GetYYYYMMDD(txtWTExemptToDate4.Text);

            ObjVComp.Indicator_WHT_Type5 = ddlIndicatorWHTType5.SelectedValue;
            ObjVComp.Type_Recipient5 = ddlTypeRecipient5.SelectedValue;
            ObjVComp.WithHolding_Tax_Code5 = ddlWithHoldingTaxCode5.SelectedValue;
            ObjVComp.WHT_Exempt_Cert_No5 = txtWHTExemptCertNo5.Text;
            ObjVComp.Exemption_Rate_Batch_Inp5 = txtExemptionRateBatchInp5.Text;
            ObjVComp.WT_Exempt_From_Date5 = objUtil.GetYYYYMMDD(txtWTExemptFromDate5.Text);
            ObjVComp.WT_Exempt_To_Date5 = objUtil.GetYYYYMMDD(txtWTExemptToDate5.Text);

            ObjVComp.Indicator_WHT_Type6 = ddlIndicatorWHTType6.SelectedValue;
            ObjVComp.Type_Recipient6 = ddlTypeRecipient6.SelectedValue;
            ObjVComp.WithHolding_Tax_Code6 = ddlWithHoldingTaxCode6.SelectedValue;
            ObjVComp.WHT_Exempt_Cert_No6 = txtWHTExemptCertNo6.Text;
            ObjVComp.Exemption_Rate_Batch_Inp6 = txtExemptionRateBatchInp6.Text;
            ObjVComp.WT_Exempt_From_Date6 = objUtil.GetYYYYMMDD(txtWTExemptFromDate6.Text);
            ObjVComp.WT_Exempt_To_Date6 = objUtil.GetYYYYMMDD(txtWTExemptToDate6.Text);

            ObjVComp.IsActive = 1;
            ObjVComp.UserId = lblUserId.Text;
            ObjVComp.TodayDate = objUtil.GetDate();
            ObjVComp.IPAddress = objUtil.GetIpAddress();

            //Code added by Swati on 03.01.2019
            ObjVComp.BankDetailsReq = ddlBankDetailsReq.SelectedValue;
            ObjVComp.ReasonNonBankDet = txtReasonNonBankDet.Text;
            //End
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
            VendCompanyCode2 ObjVComp = GetVendCompanyCodeData();
            if (ObjVComp.Vendor_Company_Code_Data2_Id > 0)
            {
                lblCompanyCodeId.Text = ObjVComp.Vendor_Company_Code_Data2_Id.ToString();

                ddlBankCountrykey.SelectedValue = ObjVComp.Bank_Country_key;
                helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlBankCountrykey.SelectedValue, "Region_Name", "Region_Id");

                ddlRegion.SelectedValue = ObjVComp.Region_Id.ToString();
                txtBankKey.Text = ObjVComp.Bank_key;
                txtAccountHolderName.Text = ObjVComp.Account_Holder_Name;
                txtBankAccNo.Text = ObjVComp.Bank_Acc_No;
                txtBankControlKey.Text = ObjVComp.Bank_Control_Key;
                txtPartnerBankType.Text = ObjVComp.Partner_Bank_Type;
                txtBankName.Text = ObjVComp.Bank_Name;
                txtHouseNoStreet.Text = ObjVComp.House_No_Street;
                txtBankNo.Text = ObjVComp.Bank_No;
                txtAccount_Number_Alternative_Payee.Text = ObjVComp.Account_Number_Alternative_Payee;
                txtKOVDate.Text = ObjVComp.KOV_Date;
                txtKOBIssueDate.Text = ObjVComp.KOB_Issue_Date;
                txtInterNBankAccNo.Text = ObjVComp.InterN_Bank_Acc_No;
                txtValidFromDate.Text = ObjVComp.Valid_From_Date;
                txtGMValidDate.Text = ObjVComp.GM_Valid_Date;
                txtMDate.Text = ObjVComp.M_Date;
                ddlIndicatorWHTType.SelectedValue = ObjVComp.Indicator_WHT_Type;

                //Code added by Swati on 03.01.2019
                ddlBankDetailsReq.SelectedValue = ObjVComp.BankDetailsReq;
                txtReasonNonBankDet.Text = ObjVComp.ReasonNonBankDet;
                //End

                if (ObjVComp.Indicator_WHT_Type != "")
                {
                    helperAccess.PopuplateComboBox(ddlTypeRecipient, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    ddlIndicatorWHTType2.Enabled = true;
                }
                else
                {
                    ddlIndicatorWHTType2.Enabled = false;
                }
                if (ObjVComp.Is_Subject_WHT != "")
                    rdlIsSubjectWHT.SelectedValue = ObjVComp.Is_Subject_WHT.ToLower() == "true" ? "1" : "0";

                txtVendReceiptType.Text = ObjVComp.Vend_Receipt_Type;
                txtAuthExemptionWHT.Text = ObjVComp.Auth_Exemption_WHT;
                ddlWHTCountryKey.SelectedValue = ObjVComp.WHT_Country_Key;
                ddlTypeRecipient.SelectedValue = ObjVComp.Type_Recipient == "" ? "0" : ObjVComp.Type_Recipient;
                ddlWithHoldingTaxCode.SelectedValue = ObjVComp.WithHolding_Tax_Code == "" ? "0" : ObjVComp.WithHolding_Tax_Code;
                txtWHTExemptCertNo.Text = ObjVComp.WHT_Exempt_Cert_No;
                txtDate3.Text = ObjVComp.Date3;
                txtWHTIdentificationNo.Text = ObjVComp.WHT_Identification_No;
                //txtWHTCode.Text = ObjVComp.WHT_Code;
                //txtExemptionCertNo.Text = ObjVComp.Exemption_Cert_No;
                txtExemptionRateBatchInp.Text = ObjVComp.Exemption_Rate_Batch_Inp;
                txtWTExemptFromDate.Text = ObjVComp.WT_Exempt_From_Date;
                txtWTExemptToDate.Text = ObjVComp.WT_Exempt_To_Date;
                ddlExemptionReason.Text = ObjVComp.Exemption_Reason;

                ddlIndicatorWHTType2.SelectedValue = ObjVComp.Indicator_WHT_Type2;
                if (ObjVComp.Indicator_WHT_Type2 != "")
                {
                    helperAccess.PopuplateComboBox(ddlTypeRecipient2, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType2.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode2, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType2.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    ddlIndicatorWHTType3.Enabled = true;
                }
                else
                    ddlIndicatorWHTType3.Enabled = false;



                txtWHTIdentificationNo2.Text = ObjVComp.WHT_Identification_No2;
                ddlTypeRecipient2.SelectedValue = ObjVComp.Type_Recipient2 == "" ? "0" : ObjVComp.Type_Recipient2;
                ddlWithHoldingTaxCode2.SelectedValue = ObjVComp.WithHolding_Tax_Code2 == "" ? "0" : ObjVComp.WithHolding_Tax_Code2;
                txtWHTExemptCertNo2.Text = ObjVComp.WHT_Exempt_Cert_No2;
                txtExemptionRateBatchInp2.Text = ObjVComp.Exemption_Rate_Batch_Inp2;
                txtWTExemptFromDate2.Text = ObjVComp.WT_Exempt_From_Date2;
                txtWTExemptToDate2.Text = ObjVComp.WT_Exempt_To_Date2;
                ddlExemptionReason2.Text = ObjVComp.Exemption_Reason2;


                ddlIndicatorWHTType3.SelectedValue = ObjVComp.Indicator_WHT_Type3;
                if (ObjVComp.Indicator_WHT_Type3 != "")
                {
                    helperAccess.PopuplateComboBox(ddlTypeRecipient3, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType3.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode3, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType3.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    ddlIndicatorWHTType4.Enabled = true;
                }
                else
                    ddlIndicatorWHTType4.Enabled = false;


                ddlTypeRecipient3.SelectedValue = ObjVComp.Type_Recipient3;
                ddlWithHoldingTaxCode3.SelectedValue = ObjVComp.WithHolding_Tax_Code3;
                txtWHTExemptCertNo3.Text = ObjVComp.WHT_Exempt_Cert_No3;
                txtExemptionRateBatchInp3.Text = ObjVComp.Exemption_Rate_Batch_Inp3;
                txtWTExemptFromDate3.Text = ObjVComp.WT_Exempt_From_Date3;
                txtWTExemptToDate3.Text = ObjVComp.WT_Exempt_To_Date3;

                ddlIndicatorWHTType4.SelectedValue = ObjVComp.Indicator_WHT_Type4;
                if (ObjVComp.Indicator_WHT_Type4 != "")
                {
                    helperAccess.PopuplateComboBox(ddlTypeRecipient4, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType4.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode4, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType4.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    ddlIndicatorWHTType5.Enabled = true;
                }
                else
                    ddlIndicatorWHTType5.Enabled = false;



                ddlTypeRecipient4.SelectedValue = ObjVComp.Type_Recipient4;
                ddlWithHoldingTaxCode4.SelectedValue = ObjVComp.WithHolding_Tax_Code4;
                txtWHTExemptCertNo4.Text = ObjVComp.WHT_Exempt_Cert_No4;
                txtExemptionRateBatchInp4.Text = ObjVComp.Exemption_Rate_Batch_Inp4;
                txtWTExemptFromDate4.Text = ObjVComp.WT_Exempt_From_Date4;
                txtWTExemptToDate4.Text = ObjVComp.WT_Exempt_To_Date4;


                ddlIndicatorWHTType5.SelectedValue = ObjVComp.Indicator_WHT_Type5;
                if (ObjVComp.Indicator_WHT_Type5 != "")
                {
                    helperAccess.PopuplateComboBox(ddlTypeRecipient5, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType5.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode5, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType5.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    ddlIndicatorWHTType6.Enabled = true;
                }
                else
                    ddlIndicatorWHTType6.Enabled = false;

                ddlTypeRecipient5.SelectedValue = ObjVComp.Type_Recipient5;
                ddlWithHoldingTaxCode5.SelectedValue = ObjVComp.WithHolding_Tax_Code5;
                txtWHTExemptCertNo5.Text = ObjVComp.WHT_Exempt_Cert_No5;
                txtExemptionRateBatchInp5.Text = ObjVComp.Exemption_Rate_Batch_Inp5;
                txtWTExemptFromDate5.Text = ObjVComp.WT_Exempt_From_Date5;
                txtWTExemptToDate5.Text = ObjVComp.WT_Exempt_To_Date5;


                ddlIndicatorWHTType6.SelectedValue = ObjVComp.Indicator_WHT_Type6;
                if (ObjVComp.Indicator_WHT_Type6 != "")
                {
                    helperAccess.PopuplateComboBox(ddlTypeRecipient6, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType6.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                    helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode6, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType6.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                }
                ddlTypeRecipient6.SelectedValue = ObjVComp.Type_Recipient6;
                ddlWithHoldingTaxCode6.SelectedValue = ObjVComp.WithHolding_Tax_Code6;
                txtWHTExemptCertNo6.Text = ObjVComp.WHT_Exempt_Cert_No6;
                txtExemptionRateBatchInp6.Text = ObjVComp.Exemption_Rate_Batch_Inp6;
                txtWTExemptFromDate6.Text = ObjVComp.WT_Exempt_From_Date6;
                txtWTExemptToDate6.Text = ObjVComp.WT_Exempt_To_Date6;
            }
            else
            {
                lblCompanyCodeId.Text = "0";

                ddlIndicatorWHTType2.Enabled = false;
                ddlIndicatorWHTType3.Enabled = false;
                ddlIndicatorWHTType4.Enabled = false;
                ddlIndicatorWHTType5.Enabled = false;
                ddlIndicatorWHTType6.Enabled = false;

                if (lblActionType.Text != "C")
                {
                    ddlIndicatorWHTType.SelectedValue = ObjVComp.Indicator_WHT_Type;
                    ddlWHTCountryKey.SelectedValue = ObjVComp.WHT_Country_Key;

                    if (ObjVComp.Indicator_WHT_Type != "")
                    {
                        helperAccess.PopuplateComboBox(ddlTypeRecipient, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                        ddlIndicatorWHTType2.Enabled = true;
                    }
                    else
                    {
                        ddlIndicatorWHTType2.Enabled = false;
                    }
                    if (ObjVComp.Is_Subject_WHT != "")
                        rdlIsSubjectWHT.SelectedValue = ObjVComp.Is_Subject_WHT.ToLower() == "true" ? "1" : "0";


                    ddlTypeRecipient.SelectedValue = ObjVComp.Type_Recipient == "" ? "0" : ObjVComp.Type_Recipient;
                    ddlWithHoldingTaxCode.SelectedValue = ObjVComp.WithHolding_Tax_Code == "" ? "0" : ObjVComp.WithHolding_Tax_Code;

                    ddlIndicatorWHTType2.SelectedValue = ObjVComp.Indicator_WHT_Type2;
                    if (ObjVComp.Indicator_WHT_Type2 != "")
                    {
                        helperAccess.PopuplateComboBox(ddlTypeRecipient2, "pr_GetWHTDetailByTaxType 'RECE_TYPE','" + ddlIndicatorWHTType2.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                        helperAccess.PopuplateComboBox(ddlWithHoldingTaxCode2, "pr_GetWHTDetailByTaxType 'TAX_CODE','" + ddlIndicatorWHTType2.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                        ddlIndicatorWHTType3.Enabled = true;
                    }
                    else
                        ddlIndicatorWHTType3.Enabled = false;


                    ddlTypeRecipient2.SelectedValue = ObjVComp.Type_Recipient2 == "" ? "0" : ObjVComp.Type_Recipient2;
                    ddlWithHoldingTaxCode2.SelectedValue = ObjVComp.WithHolding_Tax_Code2 == "" ? "0" : ObjVComp.WithHolding_Tax_Code2;
                }

            }
            ddlBankCountrykey.Focus();
            rdlIsSubjectWHT.Enabled = false;
            ddlWHTCountryKey.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private VendCompanyCode2 GetVendCompanyCodeData()
    {
        return ObjVendCompanyCodeAccess.GetVendCompanyCode2(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private void ConfigureControl()
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Company_Code_Data_2 obj = new SectionConfiguration.Company_Code_Data_2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        //Code added by Swati on 03.01.2019
        reqtxtBankAccNo.Enabled = false;
        reqtxtAccountHolderName.Enabled = false;
        //End Change
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (lblActionType.Text != "C")
            flag = true;
        else
        {
            if (ddlBankCountrykey.SelectedValue != "0")
                flag = true;
            else if (txtBankKey.Text != "")
                flag = true;
            else if (txtBankAccNo.Text != "")
                flag = true;
            else if (txtAccountHolderName.Text != "")
                flag = true;
            else if (txtAccount_Number_Alternative_Payee.Text != "")
                flag = true;
            else if (rdlIsSubjectWHT.SelectedValue != "")
                flag = true;
            else if (txtBankControlKey.Text != "")
                flag = true;
            else if (ddlWHTCountryKey.SelectedValue != "0")
                flag = true;
            else if (ddlIndicatorWHTType.SelectedValue != "")
                flag = true;
            else if (ddlIndicatorWHTType2.SelectedValue != "")
                flag = true;
            else if (ddlIndicatorWHTType3.SelectedValue != "")
                flag = true;
            else if (ddlIndicatorWHTType4.SelectedValue != "")
                flag = true;
            else if (ddlIndicatorWHTType5.SelectedValue != "")
                flag = true;
            else if (ddlIndicatorWHTType6.SelectedValue != "")
                flag = true;
            else if (ddlWithHoldingTaxCode.SelectedValue != "")
                flag = true;
            else if (ddlWithHoldingTaxCode2.SelectedValue != "")
                flag = true;
            else if (ddlWithHoldingTaxCode3.SelectedValue != "")
                flag = true;
            else if (ddlWithHoldingTaxCode4.SelectedValue != "")
                flag = true;
            else if (ddlWithHoldingTaxCode5.SelectedValue != "")
                flag = true;
            else if (ddlWithHoldingTaxCode6.SelectedValue != "")
                flag = true;
            else if (ddlTypeRecipient.SelectedValue != "")
                flag = true;
            else if (ddlTypeRecipient2.SelectedValue != "")
                flag = true;
            else if (ddlTypeRecipient3.SelectedValue != "")
                flag = true;
            else if (ddlTypeRecipient4.SelectedValue != "")
                flag = true;
            else if (ddlTypeRecipient5.SelectedValue != "")
                flag = true;
            else if (ddlTypeRecipient6.SelectedValue != "")
                flag = true;
            else if (txtExemptionRateBatchInp.Text != "")
                flag = true;
            else if (txtExemptionRateBatchInp2.Text != "")
                flag = true;
            else if (txtExemptionRateBatchInp3.Text != "")
                flag = true;
            else if (txtExemptionRateBatchInp4.Text != "")
                flag = true;
            else if (txtExemptionRateBatchInp5.Text != "")
                flag = true;
            else if (txtExemptionRateBatchInp6.Text != "")
                flag = true;
            else if (txtWHTExemptCertNo.Text != "")
                flag = true;
            else if (txtWHTExemptCertNo2.Text != "")
                flag = true;
            else if (txtWHTExemptCertNo3.Text != "")
                flag = true;
            else if (txtWHTExemptCertNo4.Text != "")
                flag = true;
            else if (txtWHTExemptCertNo5.Text != "")
                flag = true;
            else if (txtWHTExemptCertNo6.Text != "")
                flag = true;
            else if (txtWTExemptFromDate.Text != "")
                flag = true;
            else if (txtWTExemptFromDate2.Text != "")
                flag = true;
            else if (txtWTExemptFromDate3.Text != "")
                flag = true;
            else if (txtWTExemptFromDate4.Text != "")
                flag = true;
            else if (txtWTExemptFromDate5.Text != "")
                flag = true;
            else if (txtWTExemptFromDate6.Text != "")
                flag = true;
            else if (txtWTExemptToDate.Text != "")
                flag = true;
            else if (txtWTExemptToDate2.Text != "")
                flag = true;
            else if (txtWTExemptToDate3.Text != "")
                flag = true;
            else if (txtWTExemptToDate4.Text != "")
                flag = true;
            else if (txtWTExemptToDate5.Text != "")
                flag = true;
            else if (txtWTExemptToDate6.Text != "")
                flag = true;
        }

        return flag;
    }

    //Code Added by Swati on 03.01.2019
    private void setConfig()
    {
        if (ddlBankDetailsReq.SelectedValue == "0")
        {

            txtReasonNonBankDet.Enabled = true;
            reqtxtBankKey.Enabled = false;
            labletxtBankKey.Visible = false;
            reqtxtBankAccNo.Enabled = false;
            labletxtBankAccNo.Visible = false;
            reqtxtAccountHolderName.Enabled = false;
            labletxtAccountHolderName.Visible = false;
            reqtxtReasonNonBankDet.Enabled = true;
            labeltxtReasonNonBankDet.Visible = true;
            txtBankKey.Text = "";
            ddlBankCountrykey.SelectedValue = "0";
            txtBankAccNo.Text = "";
            ddlRegion.SelectedValue = "0";
            txtBankName.Text = "";
            txtHouseNoStreet.Text = "";
            txtBankNo.Text = "";
            txtAccountHolderName.Text = "";
            txtBankAccNo.Text = "";
            txtBankControlKey.Text = "";
            txtAccount_Number_Alternative_Payee.Text = "";
            txtInterNBankAccNo.Text = "";
            txtPartnerBankType.Text = "";
            //rdlIsSubjectWHT.SelectedValue = "";
            ddlWHTCountryKey.SelectedValue = "0";

            txtBankKey.Enabled = false;
            ddlBankCountrykey.Enabled = false;
            txtBankAccNo.Enabled = false;
            txtAccountHolderName.Enabled = false;
            txtBankAccNo.Enabled = false;
            txtBankControlKey.Enabled = false;
            txtAccount_Number_Alternative_Payee.Enabled = false;
            txtInterNBankAccNo.Enabled = false;
        }
        else
        {
            txtReasonNonBankDet.Enabled = false;
            reqtxtBankKey.Enabled = true;
            labletxtBankKey.Visible = true;
            reqtxtBankAccNo.Enabled = true;
            labletxtBankAccNo.Visible = true;
            reqtxtAccountHolderName.Enabled = true;
            labletxtAccountHolderName.Visible = true;
            reqtxtReasonNonBankDet.Enabled = false;
            labeltxtReasonNonBankDet.Visible = false;

            txtBankKey.Enabled = true;
            ddlBankCountrykey.Enabled = true;
            txtBankAccNo.Enabled = true;
            txtAccountHolderName.Enabled = true;
            txtBankAccNo.Enabled = true;
            txtBankControlKey.Enabled = true;
            txtAccount_Number_Alternative_Payee.Enabled = true;
            txtInterNBankAccNo.Enabled = true;

            txtReasonNonBankDet.Text = "";
        }
    }
    //End

    #endregion

}