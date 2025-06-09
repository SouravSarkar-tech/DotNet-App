using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;


public partial class Transaction_Customer_SalesArea2 : BasePage
{
    SalesAreaAccess ObjCustomerGeneralAccess = new SalesAreaAccess();

    #region Page Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    PopuplateDropDownList();


                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    FillGeneralData();
                    ConfigureControl();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        //SDT29052019 Comment Old Code
                        //trButton.Visible = true;
                        //btnSave.Visible = !btnNext.Visible;
                        //SDT29052019 Comment Old Code

                        //SDT29052019 Update for save and next button
                        try
                        { 
                            if (Session[StaticKeys.DivTypeCusts] != null && Session[StaticKeys.DivTypeCusts].ToString() != "")
                            {
                                trButton.Visible = true;
                                btnSave.Visible = !btnNext.Visible;
                            }

                            else
                            {
                                trButton.Visible = true;
                                btnNext.Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        //SDT29052019 Update for save and next button

                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string pageURL = btnPrevious.CommandArgument.ToString();
        Response.Redirect(pageURL);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveCustomerGeneral())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("SalesArea2.aspx");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SaveCustomerGeneral())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    protected void ddlAccAssignmentCust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccAssignmentCust.SelectedValue == "Z2" || lblModuleId.Text == "85")
        {
            txtEXchangeRateTYpe.Text = "CUST";
        }
        else
        {
            txtEXchangeRateTYpe.Text = "";
        }
    }

    protected void ddlPartnerFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartnerFunction.SelectedValue == "")
        {
            reqtxtNumberSDBusinPartner.Visible = false;
            labletxtNumberSDBusinPartner.Visible = false;
        }
        else
        {
            reqtxtNumberSDBusinPartner.Visible = true;
            labletxtNumberSDBusinPartner.Visible = true;
        }
    }

    protected void ddlPartnerFunction2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartnerFunction2.SelectedValue == "")
        {
            reqtxtNumberSDBusinPartner2.Visible = false;
            labletxtNumberSDBusinPartner2.Visible = false;
        }
        else
        {
            reqtxtNumberSDBusinPartner2.Visible = true;
            labletxtNumberSDBusinPartner2.Visible = true;
        }
    }

    protected void ddlPartnerFunction3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartnerFunction3.SelectedValue == "")
        {
            reqtxtNumberSDBusinPartner3.Visible = false;
            labletxtNumberSDBusinPartner3.Visible = false;
        }
        else
        {
            reqtxtNumberSDBusinPartner3.Visible = true;
            labletxtNumberSDBusinPartner3.Visible = true;
        }
    }

    protected void ddlPartnerFunction4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartnerFunction4.SelectedValue == "")
        {
            reqtxtNumberSDBusinPartner4.Visible = false;
            labletxtNumberSDBusinPartner4.Visible = false;
        }
        else
        {
            reqtxtNumberSDBusinPartner4.Visible = true;
            labletxtNumberSDBusinPartner4.Visible = true;
        }
    }

    protected void ddlPartnerFunction5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartnerFunction5.SelectedValue == "")
        {
            reqtxtNumberSDBusinPartner5.Visible = false;
            labletxtNumberSDBusinPartner5.Visible = false;
        }
        else
        {
            reqtxtNumberSDBusinPartner5.Visible = true;
            labletxtNumberSDBusinPartner5.Visible = true;
        }
    }

    protected void ddlPartnerFunction6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartnerFunction6.SelectedValue == "")
        {
            reqtxtNumberSDBusinPartner6.Visible = false;
            labletxtNumberSDBusinPartner6.Visible = false;
        }
        else
        {
            reqtxtNumberSDBusinPartner6.Visible = true;
            labletxtNumberSDBusinPartner6.Visible = true;
        }
    }

    #endregion

    #region Private Methods

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplateDistributionChannelList(ddlDistributionChannel, lblMasterHeaderId.Text, "SD1", lblCustomerGeneralId.Text);
        helperAccess.PopuplateSalesOrganisationList(ddlSalesOrginization, lblMasterHeaderId.Text, "SD1", lblCustomerGeneralId.Text);
        helperAccess.PopuplateDivisionList(ddlDivision, lblMasterHeaderId.Text, "SD1", lblCustomerGeneralId.Text);
        helperAccess.PopuplateComboBox(ddlTermPaymentKey, "pr_GetDropDownListByControlNameModuleType 'C','ddlTermPaymentKey'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlIncotermsPart1, "pr_GetDropDownListByControlNameModuleType 'C','ddlIncotermsPart1'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAccAssignmentCust, "pr_GetDropDownListByControlNameModuleType 'C','ddlAccAssignmentCust','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");

        bindPartnerFunction(ddlPartnerFunction);
        bindPartnerFunction(ddlPartnerFunction2);
        bindPartnerFunction(ddlPartnerFunction3);
        bindPartnerFunction(ddlPartnerFunction4);
        bindPartnerFunction(ddlPartnerFunction5);
        bindPartnerFunction(ddlPartnerFunction6);
    }

    public void bindPartnerFunction(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Insert(0, new ListItem("---Select---", ""));
        ddl.Items.Insert(1, new ListItem("SP - Sold-to Party", "SP"));
        ddl.Items.Insert(1, new ListItem("SH - Ship-to Party", "SH"));
        ddl.Items.Insert(2, new ListItem("BP - Bill-to Party", "BP"));
        ddl.Items.Insert(3, new ListItem("PY - Payer", "PY"));
        ddl.Items.Insert(4, new ListItem("PE - Sales Employee", "PE"));
    }

    private bool SaveCustomerGeneral()
    {

        SalesArea2 ObjSalesArea = GetControlsValue();
        bool flg = false;
        try
        {
            if (ObjCustomerGeneralAccess.Save2(ObjSalesArea) > 0)
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

    private SalesArea2 GetSalesArea2()
    {
        return ObjCustomerGeneralAccess.GetSalesArea2(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private SalesArea2 GetControlsValue()
    {
        SalesArea2 ObjSalesArea = new SalesArea2();
        Utility objUtil = new Utility();

        ObjSalesArea.Cust_SalesArea2_Id = Convert.ToInt32(lblCustomerGeneralId.Text);
        ObjSalesArea.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjSalesArea.Sales_Organization_Id = GetSelectedCheckedValue(ddlSalesOrginization);
        ObjSalesArea.Distribution_Channel_ID = GetSelectedCheckedValue(ddlDistributionChannel);
        ObjSalesArea.Division_ID = GetSelectedCheckedValue(ddlDivision);

        ObjSalesArea.BilingBlockCust = txtBilingBlockCust.Text;
        ObjSalesArea.IndiCustRebate = txtIndiCustRebate.Text;
        ObjSalesArea.EXchangeRateTYpe = txtEXchangeRateTYpe.Text;
        ObjSalesArea.CustomerGroup1 = txtCustomerGroup1.Text;
        ObjSalesArea.CustomerGroup2 = txtCustomerGroup2.Text;
        ObjSalesArea.CustomerGroup3 = txtCustomerGroup3.Text;
        ObjSalesArea.CustomerGroup4 = txtCustomerGroup4.Text;
        ObjSalesArea.CustomerGroup5 = txtCustomerGroup5.Text;
        ObjSalesArea.CustPayGuarantProc = txtCustPayGuarantProc.Text;
        ObjSalesArea.CreditControlArea = txtCreditControlArea.Text;
        ObjSalesArea.SalesBlockCust = txtSalesBlockCust.Text;
        ObjSalesArea.SwitchOffRound = txtSwitchOffRound.Text;
        ObjSalesArea.CustClassABC = txtCustClassABC.Text;
        ObjSalesArea.TaxCategory = txtTaxCategory.Text;
        ObjSalesArea.TaxClassificationCust = txtTaxClassificationCust.Text;
        ObjSalesArea.LicenceNumber = txtLicenceNumber.Text;
        ObjSalesArea.DateBatchInput = objUtil.GetMMDDYYYY(txtDateBatchInput.Text.ToString());
        ObjSalesArea.DateBatchin2 = objUtil.GetMMDDYYYY(txtDateBatchin2.Text.ToString());
        ObjSalesArea.ConfirmationLicenses = txtConfirmationLicenses.Text;


        ObjSalesArea.AuthorizationGroup = txtAuthorizationGroup.Text;
        ObjSalesArea.OrderProbabilityitem = txtOrderProbabilityitem.Text == "" ? "0" : txtOrderProbabilityitem.Text;
        ObjSalesArea.ItemProposal = txtItemProposal.Text;
        ObjSalesArea.CustomerGroup = txtCustomerGroup.Text;
        ObjSalesArea.ShipperAccountCustVendor = txtShipperAccountCustVendor.Text;
        ObjSalesArea.PricingProcuderAssCust = txtPricingProcuderAssCust.Text;
        ObjSalesArea.DeliveryPriority = txtDeliveryPriority.Text == "" ? "0" : txtDeliveryPriority.Text;
        ObjSalesArea.PriceListType = txtPriceListType.Text;
        ObjSalesArea.CustStatisticsGroup = txtCustStatisticsGroup.Text;
        ObjSalesArea.OrderCombinationIndi = chkOrderCombinationIndi.Checked == true ? "1" : "0";
        ObjSalesArea.ShippingCondition = txtShippingCondition.Text;

        ObjSalesArea.CompleteDeliverySalesOrder = txtCompleteDeliverySalesOrder.Text;
        ObjSalesArea.PartialItemLevel = chkPartialItemLevel.Checked == true ? 1 : 0;
        ObjSalesArea.MaxPermittedDeliveries = txtMaxPermittedDeliveries.Text == "" ? "0" : txtMaxPermittedDeliveries.Text;

        ObjSalesArea.IncotermsPart1 = ddlIncotermsPart1.SelectedValue;
        ObjSalesArea.IncotermsPart2 = txtIncotermsPart2.Text;
        ObjSalesArea.TermPaymentKey = ddlTermPaymentKey.SelectedValue;
        ObjSalesArea.AccAssignmentCust = ddlAccAssignmentCust.SelectedValue;
        ObjSalesArea.DeletionFlagCust = txtDeletionFlagCust.Text;
        ObjSalesArea.CustOrderBlock = txtCustOrderBlock.Text;
        ObjSalesArea.CustDeliveryBlock = txtCustDeliveryBlock.Text;

        ObjSalesArea.PartnerFunction = ddlPartnerFunction.SelectedValue;
        ObjSalesArea.NumberSDBusinPartner = txtNumberSDBusinPartner.Text;
        ObjSalesArea.PartnerFunction2 = ddlPartnerFunction2.SelectedValue;
        ObjSalesArea.NumberSDBusinPartner2 = txtNumberSDBusinPartner2.Text;
        ObjSalesArea.PartnerFunction3 = ddlPartnerFunction3.SelectedValue;
        ObjSalesArea.NumberSDBusinPartner3 = txtNumberSDBusinPartner3.Text;
        ObjSalesArea.PartnerFunction4 = ddlPartnerFunction4.SelectedValue;
        ObjSalesArea.NumberSDBusinPartner4 = txtNumberSDBusinPartner4.Text;
        ObjSalesArea.PartnerFunction5 = ddlPartnerFunction5.SelectedValue;
        ObjSalesArea.NumberSDBusinPartner5 = txtNumberSDBusinPartner5.Text;
        ObjSalesArea.PartnerFunction6 = ddlPartnerFunction6.SelectedValue;
        ObjSalesArea.NumberSDBusinPartner6 = txtNumberSDBusinPartner6.Text;
        ObjSalesArea.DefaultPartner = txtDefaultPartner.Text;
        ObjSalesArea.CateIndiTaxCodes = txtCateIndiTaxCodes.Text;

        ObjSalesArea.IsActive = 1;
        ObjSalesArea.UserId = lblUserId.Text;
        ObjSalesArea.TodayDate = objUtil.GetDate();
        ObjSalesArea.IPAddress = objUtil.GetIpAddress();
        //TCSDT20072021 Start
        ObjSalesArea.TCSYesNo = ddlTCSYesNo.SelectedValue;
        //TCSDT20072021 End
        return ObjSalesArea;
    }

    private void FillGeneralData()
    {
        SalesArea2 ObjSalesArea = GetSalesArea2();
        if (ObjSalesArea.Cust_SalesArea2_Id > 0)
        {

            lblCustomerGeneralId.Text = ObjSalesArea.Cust_SalesArea2_Id.ToString();
            lblMasterHeaderId.Text = ObjSalesArea.Master_Header_Id.ToString();
			//Start Change by Swati
            Session[StaticKeys.SelectedModulePlantGrp] = ObjSalesArea.ModulePlantGroupCode;
			//End Change
            SetSelectedValue(ddlSalesOrginization, ObjSalesArea.Sales_Organization_Id);
            SetSelectedValue(ddlDistributionChannel, ObjSalesArea.Distribution_Channel_ID);
            SetSelectedValue(ddlDivision, ObjSalesArea.Division_ID);

            txtBilingBlockCust.Text = ObjSalesArea.BilingBlockCust;
            txtIndiCustRebate.Text = ObjSalesArea.IndiCustRebate;
            txtEXchangeRateTYpe.Text = ObjSalesArea.EXchangeRateTYpe;
            txtCustomerGroup1.Text = ObjSalesArea.CustomerGroup1;
            txtCustomerGroup2.Text = ObjSalesArea.CustomerGroup2;
            txtCustomerGroup3.Text = ObjSalesArea.CustomerGroup3;
            txtCustomerGroup4.Text = ObjSalesArea.CustomerGroup4;
            txtCustomerGroup5.Text = ObjSalesArea.CustomerGroup5;
            txtCustPayGuarantProc.Text = ObjSalesArea.CustPayGuarantProc;
            txtCreditControlArea.Text = ObjSalesArea.CreditControlArea;
            txtSalesBlockCust.Text = ObjSalesArea.SalesBlockCust;
            txtSwitchOffRound.Text = ObjSalesArea.SwitchOffRound;
            txtCustClassABC.Text = ObjSalesArea.CustClassABC;
            txtTaxCategory.Text = ObjSalesArea.TaxCategory;
            txtTaxClassificationCust.Text = ObjSalesArea.TaxClassificationCust;
            txtLicenceNumber.Text = ObjSalesArea.LicenceNumber;
            txtDateBatchInput.Text = ObjSalesArea.DateBatchInput;
            txtDateBatchin2.Text = ObjSalesArea.DateBatchin2;
            txtConfirmationLicenses.Text = ObjSalesArea.ConfirmationLicenses;

            txtOrderProbabilityitem.Text = ObjSalesArea.OrderProbabilityitem;
            txtItemProposal.Text = ObjSalesArea.ItemProposal;
            txtCustomerGroup.Text = ObjSalesArea.CustomerGroup;
            txtShipperAccountCustVendor.Text = ObjSalesArea.ShipperAccountCustVendor;

            txtPricingProcuderAssCust.Text = ObjSalesArea.PricingProcuderAssCust;
            txtDeliveryPriority.Text = ObjSalesArea.DeliveryPriority;
            txtPriceListType.Text = ObjSalesArea.PriceListType;
            txtCustStatisticsGroup.Text = ObjSalesArea.CustStatisticsGroup;
            chkOrderCombinationIndi.Checked = ObjSalesArea.OrderCombinationIndi.ToLower() == "true" ? true : false;
            txtShippingCondition.Text = ObjSalesArea.ShippingCondition;
            txtCompleteDeliverySalesOrder.Text = ObjSalesArea.CompleteDeliverySalesOrder;
            
            if (ObjSalesArea.PartialItemLevel == 1)
            {
                chkPartialItemLevel.Checked = true;
            }
            
            txtMaxPermittedDeliveries.Text = ObjSalesArea.MaxPermittedDeliveries;
            ddlIncotermsPart1.SelectedValue = ObjSalesArea.IncotermsPart1;
            txtIncotermsPart2.Text = ObjSalesArea.IncotermsPart2;
            ddlTermPaymentKey.SelectedValue = ObjSalesArea.TermPaymentKey;
            ddlAccAssignmentCust.SelectedValue = ObjSalesArea.AccAssignmentCust;
            txtDeletionFlagCust.Text = ObjSalesArea.DeletionFlagCust;
            txtCustOrderBlock.Text = ObjSalesArea.CustOrderBlock;
            txtCustDeliveryBlock.Text = ObjSalesArea.CustDeliveryBlock;
            txtAuthorizationGroup.Text = ObjSalesArea.AuthorizationGroup;

            ddlPartnerFunction.SelectedValue = ObjSalesArea.PartnerFunction;
            txtNumberSDBusinPartner.Text = ObjSalesArea.NumberSDBusinPartner;
            ddlPartnerFunction2.SelectedValue = ObjSalesArea.PartnerFunction2;
            txtNumberSDBusinPartner2.Text = ObjSalesArea.NumberSDBusinPartner2;
            ddlPartnerFunction3.SelectedValue = ObjSalesArea.PartnerFunction3;
            txtNumberSDBusinPartner3.Text = ObjSalesArea.NumberSDBusinPartner3;
            ddlPartnerFunction4.SelectedValue = ObjSalesArea.PartnerFunction4;
            txtNumberSDBusinPartner4.Text = ObjSalesArea.NumberSDBusinPartner4;
            ddlPartnerFunction5.SelectedValue = ObjSalesArea.PartnerFunction5;
            txtNumberSDBusinPartner5.Text = ObjSalesArea.NumberSDBusinPartner5;
            ddlPartnerFunction6.SelectedValue = ObjSalesArea.PartnerFunction6;
            txtNumberSDBusinPartner6.Text = ObjSalesArea.NumberSDBusinPartner6;

            txtDefaultPartner.Text = ObjSalesArea.DefaultPartner;
            txtCateIndiTaxCodes.Text = ObjSalesArea.CateIndiTaxCodes;


            if (lblModuleId.Text == "86")
            {
                ddlPartnerFunction.Enabled = false;
            }
            //TCSDT20072021 Start
            ddlTCSYesNo.SelectedValue = ObjSalesArea.TCSYesNo;
            //TCSDT20072021 End
        }
        else
        {
            lblCustomerGeneralId.Text = "0";

            txtCustStatisticsGroup.Text = "1";
            txtPricingProcuderAssCust.Text = "1";
            txtDeliveryPriority.Text = "1";
            txtShippingCondition.Text = "01";
            txtMaxPermittedDeliveries.Text = "9";
            txtOrderProbabilityitem.Text = "100";

            if (lblModuleId.Text == "86")
            {
                ddlPartnerFunction.SelectedValue = "PY";
                ddlPartnerFunction.Enabled = false;
            }

            if (lblModuleId.Text == "85")
                txtEXchangeRateTYpe.Text = "CUST";
            chkOrderCombinationIndi.Checked = true;
            txtIncotermsPart2.Text = ObjSalesArea.IncotermsPart2;
            ddlTermPaymentKey.SelectedValue = ObjSalesArea.TermPaymentKey;
        }

        txtCustStatisticsGroup.Enabled = false;
        txtPricingProcuderAssCust.Enabled = false;
        txtDeliveryPriority.Enabled = false;
        txtShippingCondition.Enabled = false;
        txtMaxPermittedDeliveries.Enabled = false;
        txtOrderProbabilityitem.Enabled = false;
        txtEXchangeRateTYpe.Enabled = false;
        chkOrderCombinationIndi.Enabled = false;
    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Sales_area_data_2 obj = new SectionConfiguration.Sales_area_data_2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    #endregion
    
}