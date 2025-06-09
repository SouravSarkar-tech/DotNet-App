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


public partial class Transaction_Customer_PurchOrgData : System.Web.UI.Page
{
    PurchOrgDataAccess ObjPurchOrgDataAccess = new PurchOrgDataAccess();
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

                    FillGeneralData();

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
            }
        }
    }


    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (SavePurchOrgData())
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
            if (SavePurchOrgData())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("PurchOrgData.aspx");
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
            if (SavePurchOrgData())
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

    #region Private Methods

    public void bindPartnerFunction(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Insert(0, new ListItem("---Select---", ""));
        ddl.Items.Insert(1, new ListItem("MN - Manufacturing Plant", "MN"));
        ddl.Items.Insert(2, new ListItem("VN - Vendor", "VN"));

    }

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplateComboBox(ddlPurchaseOrder_Currency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code");
        helperAccess.PopuplateDropDownList(ddlTermsPayment_Key, "pr_GetDropDownListByControlNameModuleType 'V','txtTermsPayment_Key'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIncotermsPart1, "pr_GetDropDownListByControlNameModuleType 'V','txtIncotermsPart1'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlGroupCalculation_SchemaVendor, "pr_GetDropDownListByControlNameModuleType 'V','txtGroupCalculation_SchemaVendor'", "LookUp_Desc", "LookUp_Code");

        bindPartnerFunction(ddlPartner_Function);
        bindPartnerFunction(ddlPartner_Function2);
        bindPartnerFunction(ddlPartner_Function3);
        bindPartnerFunction(ddlPartner_Function4);
        bindPartnerFunction(ddlPartner_Function5);
        bindPartnerFunction(ddlPartner_Function6);

        //helperAccess.PopuplateComboBox(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','ALL','0'", "Plant_Name", "Plant_Id");
    }


    private bool SavePurchOrgData()
    {
        bool flg = false;
        PurchOrgData ObjPurchase = GetControlsValue();

        if (ObjPurchOrgDataAccess.Save(ObjPurchase) > 0)
        {
            flg = true;
        }
        else
        {
            lblMsg.Text = Messages.GetMessage(-1);
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
        return flg;
    }

    private PurchOrgData GetPurchOrgData()
    {
        return ObjPurchOrgDataAccess.GetPurchOrgData(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private PurchOrgData GetControlsValue()
    {
        PurchOrgData ObjPurchase = new PurchOrgData();
        Utility objUtil = new Utility();

        ObjPurchase.Vendor_PurchOrgData_id = Convert.ToInt32(lblCustomerGeneralId.Text);
        ObjPurchase.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);


        ObjPurchase.PurchaseOrder_Currency = ddlPurchaseOrder_Currency.SelectedValue;
        ObjPurchase.TermsPayment_Key = ddlTermsPayment_Key.SelectedValue;
        ObjPurchase.IncotermsPart1 = ddlIncotermsPart1.SelectedValue;
        ObjPurchase.IncotermsPart2 = txtIncotermsPart2.Text;
        ObjPurchase.MinimumOrder_batchInput = txtMinimumOrder_batchInput.Text;
        ObjPurchase.Responsible_SalesPerson = txtResponsible_SalesPerson.Text;
        ObjPurchase.Vendor_TelephoneNumber = txtVendor_TelephoneNumber.Text;
        if (chkABC_Indicator.Enabled)
            ObjPurchase.ABC_Indicator = chkABC_Indicator.Checked == true ? "1" : "0";
        if (chkPurchasingBlock_Purchasing.Enabled)
            ObjPurchase.PurchasingBlock_Purchasing = chkPurchasingBlock_Purchasing.Checked == true ? "1" : "0";
        if (chkDeleteflag_purchasinglevel.Enabled)
            ObjPurchase.Deleteflag_purchasinglevel = chkDeleteflag_purchasinglevel.Checked == true ? "1" : "0";
        if (chkIndicatorInvoice_Verification.Enabled || lblActionType.Text != "C")
            ObjPurchase.IndicatorInvoice_Verification = chkIndicatorInvoice_Verification.Checked == true ? "1" : "0";
        if (chkOrderAcknowledgment_Requirement.Enabled)
            ObjPurchase.OrderAcknowledgment_Requirement = chkOrderAcknowledgment_Requirement.Checked == true ? "1" : "0";

        ObjPurchase.GroupCalculation_SchemaVendor = ddlGroupCalculation_SchemaVendor.SelectedValue;
        if (chkAutomatic_Generation.Enabled)
            ObjPurchase.Automatic_Generation = chkAutomatic_Generation.Checked == true ? "1" : "0";
        ObjPurchase.ModeTransport_ForeignTrade = txtModeTransport_ForeignTrade.Text;
        ObjPurchase.CustomsOffice_ForeignTrade = txtCustomsOffice_ForeignTrade.Text;
        ObjPurchase.Purchasing_Group = txtPurchasing_Group.Text;
        if (chkIndicator_vendor_accountimng.Enabled)
            ObjPurchase.Indicator_vendor_accountimng = chkIndicator_vendor_accountimng.Checked == true ? "1" : "0";
        ObjPurchase.PlannedTime_Days_BTCI = txtPlannedTime_Days_BTCI.Text;
        ObjPurchase.Shipping_Conditions = txtShipping_Conditions.Text;
        if (chkIndicator_ServiceBased_Verification.Enabled || lblActionType.Text != "C")
            ObjPurchase.Indicator_ServiceBased_Verification = chkIndicator_ServiceBased_Verification.Checked == true ? "1" : "0";
        ObjPurchase.StagingTime_Days_BatchInput = txtStagingTime_Days_BatchInput.Text;
        ObjPurchase.Category_tax_codes = txtCategory_tax_codes.Text;
        ObjPurchase.Vendor_Subrange = txtVendor_Subrange.Text;
        ObjPurchase.Language_BatchInputField = txtLanguage_BatchInputField.Text;
        ObjPurchase.Purchasing_Organization = txtPurchasing_Organization.Text;
        ObjPurchase.Plant = ddlPlant.SelectedValue;
        ObjPurchase.Partner_Function = ddlPartner_Function.SelectedValue;
        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record = txtNameBusinessPartnerVendorMaster.Text;

        ObjPurchase.Partner_Function2 = ddlPartner_Function2.SelectedValue;
        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record2 = txtNameBusinessPartnerVendorMaster2.Text;

        ObjPurchase.Partner_Function3 = ddlPartner_Function3.SelectedValue;
        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record3 = txtNameBusinessPartnerVendorMaster3.Text;

        ObjPurchase.Partner_Function4 = ddlPartner_Function4.SelectedValue;
        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record4 = txtNameBusinessPartnerVendorMaster4.Text;

        ObjPurchase.Partner_Function5 = ddlPartner_Function5.SelectedValue;
        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record5 = txtNameBusinessPartnerVendorMaster5.Text;

        ObjPurchase.Partner_Function6 = ddlPartner_Function6.SelectedValue;
        ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record6 = txtNameBusinessPartnerVendorMaster6.Text;

        ObjPurchase.Partner_counter = txtPartner_counter.Text;
        ObjPurchase.Name_Person_who_CreatedObject = "";// txtName_Person_who_CreatedObject.Text;
        ObjPurchase.Date_Which_Record_Created = objUtil.GetMMDDYYYY(txtDate_Which_Record_Created.Text);
        ObjPurchase.Reference_vendor = txtReference_vendor.Text;
        if (chkPersonnel_Number_BatchInputField.Enabled)
            ObjPurchase.Personnel_Number_BatchInputField = chkPersonnel_Number_BatchInputField.Checked == true ? "1" : "0";
        ObjPurchase.Country_Key = ddlCountry.SelectedValue;
        if (chkSupplyRegion_RegionSupplied.Enabled)
            ObjPurchase.SupplyRegion_RegionSupplied = chkSupplyRegion_RegionSupplied.Checked == true ? "1" : "0";
        if (chkAccountNumber_VendorCreditor.Enabled)
            ObjPurchase.AccountNumber_VendorCreditor = chkAccountNumber_VendorCreditor.Checked == true ? "1" : "0";
        if (chkMaterial_Number.Enabled)
            ObjPurchase.Material_Number = chkMaterial_Number.Checked == true ? "1" : "0";
        ObjPurchase.Preference_Zone = txtPreference_Zone.Text;
        ObjPurchase.IsActive = 1;
        ObjPurchase.UserId = lblUserId.Text;
        ObjPurchase.TodayDate = objUtil.GetDate();
        ObjPurchase.IPAddress = objUtil.GetIpAddress();

        return ObjPurchase;
    }

    private void FillGeneralData()
    {
        PurchOrgData ObjPurchase = GetPurchOrgData();
        if (ObjPurchase.Vendor_PurchOrgData_id > 0)
        {
            lblCustomerGeneralId.Text = ObjPurchase.Vendor_PurchOrgData_id.ToString();
            lblMasterHeaderId.Text = ObjPurchase.Master_Header_Id.ToString();

            ddlPurchaseOrder_Currency.SelectedValue = ObjPurchase.PurchaseOrder_Currency;
            ddlTermsPayment_Key.SelectedValue = ObjPurchase.TermsPayment_Key;
            ddlIncotermsPart1.SelectedValue = ObjPurchase.IncotermsPart1;
            txtIncotermsPart2.Text = ObjPurchase.IncotermsPart2;
            txtMinimumOrder_batchInput.Text = ObjPurchase.MinimumOrder_batchInput;
            txtResponsible_SalesPerson.Text = ObjPurchase.Responsible_SalesPerson;
            txtVendor_TelephoneNumber.Text = ObjPurchase.Vendor_TelephoneNumber;
            chkABC_Indicator.Checked = ObjPurchase.ABC_Indicator == "1" ? true : false;
            chkPurchasingBlock_Purchasing.Checked = ObjPurchase.PurchasingBlock_Purchasing == "1" ? true : false;
            chkDeleteflag_purchasinglevel.Checked = ObjPurchase.Deleteflag_purchasinglevel == "1" ? true : false;
            chkIndicatorInvoice_Verification.Checked = ObjPurchase.IndicatorInvoice_Verification == "1" ? true : false;
            chkOrderAcknowledgment_Requirement.Checked = ObjPurchase.OrderAcknowledgment_Requirement == "1" ? true : false;
            ddlGroupCalculation_SchemaVendor.SelectedValue = ObjPurchase.GroupCalculation_SchemaVendor;
            chkAutomatic_Generation.Checked = ObjPurchase.Automatic_Generation == "1" ? true : false;
            txtModeTransport_ForeignTrade.Text = ObjPurchase.ModeTransport_ForeignTrade;
            txtCustomsOffice_ForeignTrade.Text = ObjPurchase.CustomsOffice_ForeignTrade;
            txtPurchasing_Group.Text = ObjPurchase.Purchasing_Group;
            chkIndicator_vendor_accountimng.Checked = ObjPurchase.Indicator_vendor_accountimng == "1" ? true : false;
            txtPlannedTime_Days_BTCI.Text = ObjPurchase.PlannedTime_Days_BTCI;
            txtShipping_Conditions.Text = ObjPurchase.Shipping_Conditions;
            chkIndicator_ServiceBased_Verification.Checked = ObjPurchase.Indicator_ServiceBased_Verification == "1" ? true : false;
            txtStagingTime_Days_BatchInput.Text = ObjPurchase.StagingTime_Days_BatchInput;
            txtCategory_tax_codes.Text = ObjPurchase.Category_tax_codes;
            txtVendor_Subrange.Text = ObjPurchase.Vendor_Subrange;
            txtLanguage_BatchInputField.Text = ObjPurchase.Language_BatchInputField;
            txtPurchasing_Organization.Text = ObjPurchase.Purchasing_Organization;
            ddlPlant.SelectedValue = ObjPurchase.Plant.Trim();
            ddlPartner_Function.SelectedValue = ObjPurchase.Partner_Function;
            txtNameBusinessPartnerVendorMaster.Text = ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record;

            ddlPartner_Function2.SelectedValue = ObjPurchase.Partner_Function2;
            txtNameBusinessPartnerVendorMaster2.Text = ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record2;

            ddlPartner_Function3.SelectedValue = ObjPurchase.Partner_Function3;
            txtNameBusinessPartnerVendorMaster3.Text = ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record3;

            ddlPartner_Function4.SelectedValue = ObjPurchase.Partner_Function4;
            txtNameBusinessPartnerVendorMaster4.Text = ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record4;

            ddlPartner_Function5.SelectedValue = ObjPurchase.Partner_Function5;
            txtNameBusinessPartnerVendorMaster5.Text = ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record5;

            ddlPartner_Function6.SelectedValue = ObjPurchase.Partner_Function6;
            txtNameBusinessPartnerVendorMaster6.Text = ObjPurchase.Number_Of_Business_Master_in_Vendor_Master_Record6;

            txtPartner_counter.Text = ObjPurchase.Partner_counter;
            //txtName_Person_who_CreatedObject.Text=ObjPurchase.Name_Person_who_CreatedObject;
            txtDate_Which_Record_Created.Text = Convert.ToString(ObjPurchase.Date_Which_Record_Created);
            txtReference_vendor.Text = ObjPurchase.Reference_vendor;
            chkPersonnel_Number_BatchInputField.Checked = ObjPurchase.Personnel_Number_BatchInputField == "1" ? true : false;
            ddlCountry.SelectedValue = ObjPurchase.Country_Key;
            chkSupplyRegion_RegionSupplied.Checked = ObjPurchase.SupplyRegion_RegionSupplied == "1" ? true : false;
            chkAccountNumber_VendorCreditor.Checked = ObjPurchase.AccountNumber_VendorCreditor == "1" ? true : false;
            chkMaterial_Number.Checked = ObjPurchase.Material_Number == "1" ? true : false;
            txtPreference_Zone.Text = ObjPurchase.Preference_Zone;
        }
        else
        {
            lblCustomerGeneralId.Text = "0";
            if (lblActionType.Text != "C")
            {
                if (lblModuleId.Text == "24")
                {
                    chkIndicator_ServiceBased_Verification.Checked = true;
                    chkIndicatorInvoice_Verification.Checked = true;
                }
                else
                {
                    chkIndicatorInvoice_Verification.Checked = true;
                }


                if (lblModuleId.Text == "18" || lblModuleId.Text == "19")
                {
                    ddlGroupCalculation_SchemaVendor.SelectedValue = "03";
                    chkIndicatorInvoice_Verification.Checked = false;
                }
                else
                {
                    ddlGroupCalculation_SchemaVendor.SelectedValue = " ";
                }
                ddlPurchaseOrder_Currency.SelectedValue = "INR";
                ddlTermsPayment_Key.SelectedValue = ObjPurchase.TermsPayment_Key;
                txtIncotermsPart2.Text = ObjPurchase.IncotermsPart2;
            }
        }

        ddlPurchaseOrder_Currency.Focus();
    }

    private void ConfigureControl()
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Purchase_Org_Data obj = new SectionConfiguration.Purchase_Org_Data();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (lblActionType.Text != "C")
            flag = true;
        else
        {
            if (ddlPurchaseOrder_Currency.SelectedValue != "")
                flag = true;
            else if (ddlIncotermsPart1.SelectedValue != "")
                flag = true;
            else if (ddlGroupCalculation_SchemaVendor.Text != "")
                flag = true;
            else if (txtIncotermsPart2.Text != "")
                flag = true;
            else if (ddlTermsPayment_Key.SelectedValue != "")
                flag = true;
            else if (ddlPartner_Function.Text != "")
                flag = true;
            else if (ddlPartner_Function2.Text != "")
                flag = true;
            else if (ddlPartner_Function3.Text != "")
                flag = true;
            else if (ddlPartner_Function4.Text != "")
                flag = true;
            else if (ddlPartner_Function5.Text != "")
                flag = true;
            else if (ddlPartner_Function6.Text != "")
                flag = true;
            else if (txtNameBusinessPartnerVendorMaster.Text != "")
                flag = true;
            else if (txtNameBusinessPartnerVendorMaster2.Text != "")
                flag = true;
            else if (txtNameBusinessPartnerVendorMaster3.Text != "")
                flag = true;
            else if (txtNameBusinessPartnerVendorMaster4.Text != "")
                flag = true;
            else if (txtNameBusinessPartnerVendorMaster5.Text != "")
                flag = true;
            else if (txtNameBusinessPartnerVendorMaster6.Text != "")
                flag = true;
        }

        return flag;
    }

    #endregion
}