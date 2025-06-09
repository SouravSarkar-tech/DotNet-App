using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Configuration;


public partial class Transaction_Customer_SalesArea1 : BasePage
{
    SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
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

                HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                FillDataGrid();
                FillSalesAreaData();

                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    trButton.Visible = true;
                    //btnSave.Visible = !btnNext.Visible;
                    grvData.Columns[4].Visible = true;
                    pnlAddData.Visible = true;
                    lnlAddDetails.Visible = true;
                    //CS_8200049196 Start
                    //ucCustSalseView.Visible = true;
                    //CS_8200049196 End
                }
                else
                {
                    grvData.Columns[4].Visible = false;
                    pnlAddData.Visible = false;
                    lnlAddDetails.Visible = false;
                    //CS_8200049196 Start
                    //ucCustSalseView.Visible = false;
                    //CS_8200049196 End
                }
                ConfigureControl();
            }
        }
    }

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        ClearData();
        pnlMsg.Visible = false;
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblCustomerGeneralId.Text = grvData.DataKeys[grdrow.RowIndex]["Cust_SalesArea1"].ToString();
            pnlMsg.Visible = false;
            FillSalesAreaData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblCustomerGeneralId.Text = grvData.DataKeys[grdrow.RowIndex]["Cust_SalesArea1"].ToString();
            pnlMsg.Visible = false;
            DeleteSalesAreaData();
            FillDataGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (!IsDuplicateEntry())
        {
            if (SaveCustomerSales())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        else
        {
            lblMsg.Text = "Duplicate Entry.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsDuplicateEntry())
        {
            if (SaveCustomerSales())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("SalesArea1.aspx");
            }
            FillDataGrid();
        }
        else
        {
            lblMsg.Text = "Duplicate Entry.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!IsDuplicateEntry())
        {
            if (SaveCustomerSales())
            {
                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        else
        {
            lblMsg.Text = "Duplicate Entry.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    protected void ddlCreditControlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        SetCreditControlVisible();
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        ddlDistributionChannel.SelectedValue = "1";
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CSD','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        if (Session[StaticKeys.DivCusts] != null)
        {
            ddlDivision.SelectedValue = Convert.ToString(Session[StaticKeys.DivCusts]);
        }
        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
        //helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDropDownListByControlNameModuleType 'C','ddlDeliveringPlant'", "LookUp_Desc", "LookUp_Code", "");
        //Start Change by Swati
        //ddlDistributionChannel.SelectedValue = "1";
        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
        //End Change
        //New changes if Sales organization is "L002" Start
        //SetSalesOrgConfiguration();
        //New changes if Sales organization is "L002" End
    }

    protected void ddlDistributionChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CSD','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        if (Session[StaticKeys.DivCusts] != null)
        {
            ddlDivision.SelectedValue = Convert.ToString(Session[StaticKeys.DivCusts]);
        }
        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session[StaticKeys.DivCusts] = "";
        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        ddlCreditCurrency.SelectedValue = "";
        helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        SetCreditControlVisible();

        if (ddlDivision.SelectedValue == "4" || ddlDivision.SelectedValue == "5" || ddlDivision.SelectedValue == "15")
        {
            //Start Change by Swati
            if (Session[StaticKeys.SelectedModuleId].ToString() != "89" || Session[StaticKeys.SelectedModuleId].ToString() == "")
            {
                ddlInvoiceDates.SelectedValue = "Z1";
                ddlInvoiceListSchedule.SelectedValue = "Z1";
            }
            //End Change

            if (ddlDivision.SelectedValue == "4")
                ddlPriceGroup.SelectedValue = "57";

            if (ddlDivision.SelectedValue == "5")
                ddlPriceGroup.SelectedValue = "58";

        }
        else
        {
            ddlInvoiceDates.SelectedValue = "";
            ddlInvoiceListSchedule.SelectedValue = "";
            ddlPriceGroup.SelectedValue = "";
        }

        ddlInvoiceDates.Enabled = false;
        ddlInvoiceListSchedule.Enabled = false;

        reqddlInvoiceDates.Visible = false;
        reqddlInvoiceListSchedule.Visible = false;

        if ((Session[StaticKeys.SelectedModuleId].ToString() == "91"))
        {
            if (ddlDistributionChannel.SelectedValue == "5" && ddlDivision.SelectedValue == "30")
                ddlCreditControlArea.Enabled = true;
            else
                ddlCreditControlArea.Enabled = false;
        }
        //Start Change by Swati
        if (Session[StaticKeys.SelectedModuleId].ToString() != "89" || Session[StaticKeys.SelectedModuleId].ToString() == "")
        {
            ddlInvoiceDates.SelectedValue = "Z1";
            ddlInvoiceListSchedule.SelectedValue = "Z1";
        }
        //End Change
    }

    protected void ddlSalesOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        //helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetDropDownListByControlNameModuleType 'C','ddlSalesGroup','" + lblSectionId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetSalesGroupList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "Sales_Group_Desc", "Sales_Group_Id", "");
    }

    #endregion

    #region Private Methods

    private void PopuplateDropDownList()
    {
        //helperAccess.PopuplateDistributionChannelList(ddlDistributionChannel, lblMasterHeaderId.Text, "SD1", lblCustomerGeneralId.Text);
        //helperAccess.PopuplateSalesOrganisationList(ddlSalesOrginization, lblMasterHeaderId.Text, "SD1", lblCustomerGeneralId.Text);
        //helperAccess.PopuplateDivisionList(ddlDivision, lblMasterHeaderId.Text, "SD1", lblCustomerGeneralId.Text);

        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        //helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "'", "Division_Name", "Division_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CSD','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

        if (Session[StaticKeys.DivCusts] != null && Convert.ToString(Session[StaticKeys.DivCusts]) != "")
        {
            ddlDivision.SelectedValue = Convert.ToString(Session[StaticKeys.DivCusts]);
        }
        //else
        //        {

        //        }


        helperAccess.PopuplateComboBox(ddlCurrency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code", "");
        helperAccess.PopuplateDropDownList(ddlSalesDistrict, "pr_GetDropDownListByControlNameModuleType 'C','ddlSalesDistrict'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetDropDownListByControlNameModuleType 'C','ddlSalesOffice'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlPriceGroup, "pr_GetDropDownListByControlNameModuleType 'C','ddlPriceGroup','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlInvoiceDates, "pr_GetDropDownListByControlNameModuleType 'C','ddlInvoiceDates','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlInvoiceListSchedule, "pr_GetDropDownListByControlNameModuleType 'C','ddlInvoiceListSchedule','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetDropDownListByControlNameModuleType 'C','ddlSalesGroup','" + lblSectionId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDropDownListByControlNameModuleType 'C','ddlDeliveringPlant'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlCreditCurrency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code", "");
        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    }

    private bool SaveCustomerSales()
    {
        SalesArea1 ObjSalesArea = GetControlsValue();
        bool flg = false;
        try
        {
            if (ObjSalesAreaAccess.Save1(ObjSalesArea) > 0)
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

    private void ClearData()
    {
        try
        {
            PopuplateDropDownList();
            lblCustomerGeneralId.Text = "0";

            txtcountryKeyExport.Text = "";
            txtCustomer_credit_limit.Text = "";
            ddlCreditCurrency.SelectedValue = "INR";
            SetCreditControlVisible();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool IsDuplicateEntry()
    {
        bool flg = false;

        if (lblCustomerGeneralId.Text == "0")
        {
            //CS_8200049196
             CustomerExtensionAccess ObjCustomerExtensionAccess = new CustomerExtensionAccess();
            //DataSet dstData = new DataSet();
            //dstData = ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(lblMasterHeaderId.Text), "Csa"
            //    , ddlSalesOrginization.SelectedValue, ddlDistributionChannel.SelectedValue, ddlDivision.SelectedValue);

            //if (dstData.Tables[0].Rows.Count > 0)
            if (ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(lblMasterHeaderId.Text), "Csa", ddlSalesOrginization.SelectedValue, ddlDistributionChannel.SelectedValue, ddlDivision.SelectedValue) > 0)
            { 
                flg = true;
            }
            //foreach (GridViewRow gr in grvData.Rows)
            //{

            //    if (ddlSalesOrginization.SelectedValue == ((Label)gr.FindControl("lblSalesOrgId")).Text && ddlDistributionChannel.SelectedValue == ((Label)gr.FindControl("lblDistributionChnlId")).Text && ddlDivision.SelectedValue == ((Label)gr.FindControl("lblDivisionId")).Text)
            //    {
            //        flg = true;
            //    }

            //}
        }

        return flg;
    }

    private void SetCreditControlVisible()
    {
        bool IsEnable = false;
        if (ddlCreditControlArea.SelectedValue != "")
        {
            IsEnable = true;
        }

        ddlCreditCurrency.Enabled = IsEnable;
        reqddlCreditCurrency.Visible = IsEnable;
        lableddlCreditCurrency.Visible = IsEnable;

        if (IsEnable)
        {
            ddlCreditCurrency.SelectedValue = "INR";
        }
        else
        {
            ddlCreditCurrency.SelectedValue = "";
        }

        txtCustomer_credit_limit.Enabled = IsEnable;
        reqtxtCustomer_credit_limit.Visible = IsEnable;
        labletxtCustomer_credit_limit.Visible = IsEnable;

        ddlRiskcategory.Enabled = IsEnable;
        reqddlRiskcategory.Visible = IsEnable;
        lableddlRiskcategory.Visible = IsEnable;
    }

    private void DeleteSalesAreaData()
    {
        ObjSalesAreaAccess.DeleteSalesArea1Data(Convert.ToInt32(lblCustomerGeneralId.Text));
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjSalesAreaAccess.GetSalesArea1Data(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private SalesArea1 GetSalesArea1()
    {
        return ObjSalesAreaAccess.GetSalesArea1(Convert.ToInt32(lblCustomerGeneralId.Text));
    }

    private SalesArea1 GetControlsValue()
    {
        SalesArea1 ObjSalesArea = new SalesArea1();
        Utility objUtil = new Utility();

        ObjSalesArea.Cust_SalesArea1_Id = Convert.ToInt32(lblCustomerGeneralId.Text);
        ObjSalesArea.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjSalesArea.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
        ObjSalesArea.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;
        ObjSalesArea.Division_ID = ddlDivision.SelectedValue;

        ObjSalesArea.SalesDistrict = ddlSalesDistrict.SelectedValue;
        ObjSalesArea.SalesOffice = ddlSalesOffice.SelectedValue;
        ObjSalesArea.Currency = ddlCurrency.SelectedValue;
        ObjSalesArea.SalesGroup = ddlSalesGroup.SelectedValue;
        ObjSalesArea.countryKeyExport = txtcountryKeyExport.Text;
        ObjSalesArea.DeliveringPlant = ddlDeliveringPlant.SelectedValue;

        ObjSalesArea.PriceGroup = ddlPriceGroup.SelectedValue;
        ObjSalesArea.InvoiceDates = ddlInvoiceDates.SelectedValue;
        ObjSalesArea.InvoiceListSchedule = ddlInvoiceListSchedule.SelectedValue;

        ObjSalesArea.Credit_Control_Area = ddlCreditControlArea.SelectedValue;
        ObjSalesArea.Customer_credit_limit = txtCustomer_credit_limit.Text;
        ObjSalesArea.Risk_category = ddlRiskcategory.SelectedValue;
        ObjSalesArea.Currency_Id = ddlCreditCurrency.SelectedValue;

        ObjSalesArea.IsActive = 1;
        ObjSalesArea.UserId = lblUserId.Text;
        ObjSalesArea.TodayDate = objUtil.GetDate();
        ObjSalesArea.IPAddress = objUtil.GetIpAddress();

        return ObjSalesArea;
    }

    private void FillSalesAreaData()
    {
        try
        {
            SalesArea1 ObjSalesArea = GetSalesArea1();
            if (ObjSalesArea.Cust_SalesArea1_Id > 0)
            {
                lblCustomerGeneralId.Text = ObjSalesArea.Cust_SalesArea1_Id.ToString();
                lblMasterHeaderId.Text = ObjSalesArea.Master_Header_Id.ToString();

                ddlSalesOrginization.SelectedValue = ObjSalesArea.Sales_Organization_Id;
                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                ddlDistributionChannel.SelectedValue = ObjSalesArea.Distribution_Channel_ID;
                //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
                helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CSD','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

                ddlDivision.SelectedValue = ObjSalesArea.Division_ID;

                helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
                helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlSalesDistrict.SelectedValue = ObjSalesArea.SalesDistrict.ToString();
                txtcountryKeyExport.Text = ObjSalesArea.countryKeyExport.ToString();
                ddlDeliveringPlant.SelectedValue = ObjSalesArea.DeliveringPlant;

                ddlPriceGroup.SelectedValue = ObjSalesArea.PriceGroup;
                ddlInvoiceDates.SelectedValue = ObjSalesArea.InvoiceDates;
                ddlInvoiceListSchedule.SelectedValue = ObjSalesArea.InvoiceListSchedule;

                try { ddlSalesOffice.SelectedValue = ObjSalesArea.SalesOffice.ToString(); }
                catch { }
                helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetSalesGroupList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "Sales_Group_Desc", "Sales_Group_Id", "");
                ddlSalesGroup.SelectedValue = ObjSalesArea.SalesGroup.ToString();

                ddlCurrency.SelectedValue = ObjSalesArea.Currency.ToString();

                ddlCreditControlArea.SelectedValue = ObjSalesArea.Credit_Control_Area.ToString();
                txtCustomer_credit_limit.Text = ObjSalesArea.Customer_credit_limit.ToString();
                helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlRiskcategory.SelectedValue = ObjSalesArea.Risk_category.ToString();
                ddlCreditCurrency.SelectedValue = ObjSalesArea.Currency_Id;
                //Carve_LC17&LC23_8400000406 added
                if (ddlCreditControlArea.SelectedValue != "")
                {
                    SetCreditControlVisible();
                    //ddlSalesOrginization.SelectedValue = "25";
                }
 
                //SDT29052019, Commented By Nitin R , Dis : no need this condition due to already get save data form Sales table
                //Start Change: DT 03.12.2018 - If request is DASIRF then set default values 
                //ddlInvoiceDates.SelectedValue = "Z1";
                //ddlInvoiceListSchedule.SelectedValue = "Z1";



                //DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
                //if (dsdata.Tables[0].Rows.Count > 0)
                //{
                //    if (dsdata.Tables[0].Rows[0]["Customer_Category"].ToString() == "DASIRF")
                //    {
                //        ddlSalesOrginization.SelectedValue = "2";
                //        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                //        ddlDistributionChannel.SelectedValue = "1";
                //        ddlCurrency.SelectedValue = "INR";
                //        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
                //        ddlDivision.SelectedValue = dsdata.Tables[0].Rows[0]["Division"].ToString();
                //        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                //        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
                //        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                //    }
                //}

                //if (Session[StaticKeys.SelectedModuleId].ToString() != "89" || Session[StaticKeys.SelectedModuleId].ToString() == "")
                //{
                //    ddlInvoiceDates.SelectedValue = "Z1";
                //    ddlInvoiceListSchedule.SelectedValue = "Z1";
                //}
                //EDT29052019, Commented By Nitin R , Dis : no need this condition due to already get save data form Sales table

                //End Change
            }
            else
            {
                lblCustomerGeneralId.Text = "0";
                ddlCreditCurrency.SelectedValue = "INR";
                //Start Change: DT 03.12.2018 - If request is DASIRF then set default values 
                if (Session[StaticKeys.SelectedModuleId].ToString() != "89" || Session[StaticKeys.SelectedModuleId].ToString() == "")
                {
                    ddlInvoiceDates.SelectedValue = "Z1";
                    ddlInvoiceListSchedule.SelectedValue = "Z1";
                }

                SetCreditControlVisible();

                DataSet ds = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        //Carve_LC17&LC23_8400000406 added
                        String sCMLC01 = ConfigurationManager.AppSettings["CMLC01"].ToString();
                        if (ds.Tables[0].Rows[0]["Company_Code"].ToString() == sCMLC01.ToString())
                        {
                            String sSOLC01 = ConfigurationManager.AppSettings["SOLC01"].ToString();
                            ddlSalesOrginization.SelectedValue = sSOLC01.ToString();
                        }
                        else
                        {
                            ddlSalesOrginization.SelectedValue = "2";
                        }
                        //Carve_LC17&LC23_8400000406 added
                    }
                    catch (Exception ex) { }
                    if (ds.Tables[0].Rows[0]["Customer_Category"].ToString() == "DASIRF")
                    {
                        //Carve_LC17&LC23_8400000406 Comment
                        //ddlSalesOrginization.SelectedValue = "2";
                        //Carve_LC17&LC23_8400000406 Comment
                        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                        ddlDistributionChannel.SelectedValue = "1";
                        ddlCurrency.SelectedValue = "INR";
                        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
                        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CSD','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

                        try { ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["Division"].ToString(); }
                        catch { }
                        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
                        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


                        ddlInvoiceDates.SelectedValue = "Z1";
                        ddlInvoiceListSchedule.SelectedValue = "Z1";
                        ddlInvoiceDates.Enabled = false;
                        ddlInvoiceListSchedule.Enabled = false;
                    }
                    else
                    {
                        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                        ddlDistributionChannel.SelectedValue = "1";
                        ddlCurrency.SelectedValue = "INR";
                        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CSD','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
                        if (Session[StaticKeys.DivCusts] != null)
                        {
                            ddlDivision.SelectedValue = Convert.ToString(Session[StaticKeys.DivCusts]);
                        }
                        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
                        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


                        ddlInvoiceDates.SelectedValue = "Z1";
                        ddlInvoiceListSchedule.SelectedValue = "Z1";
                        ddlInvoiceDates.Enabled = false;
                        ddlInvoiceListSchedule.Enabled = false;
                    }
                }
            }
                //End Change
                //New changes if Sales organization is "L002" Start
                //SetSalesOrgConfiguration();
                //New changes if Sales organization is "L002" End
            }
        catch (Exception ex)
        {
            //throw ex;
        }
    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Sales_area_data obj = new SectionConfiguration.Sales_area_data();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    //New changes if Sales organization is "L002" Start
    //private void SetSalesOrgConfiguration()
    //{        
    //    if (ddlSalesOrginization.SelectedValue.ToString() == "3")
    //    {
    //        reqddlSalesDistrict.Enabled = false;
    //        reqddlSalesGroup.Enabled = false;
    //        reqddlSalesOffice.Enabled = false;
    //        lableddlSalesDistrict.Visible = false;
    //        lableddlSalesGroup.Visible = false;
    //        lableddlSalesOffice.Visible = false;
    //    }
    //    else
    //    {
    //        reqddlSalesDistrict.Enabled = true;
    //        reqddlSalesGroup.Enabled = true;
    //        reqddlSalesOffice.Enabled = true;
    //        lableddlSalesDistrict.Visible = true;
    //        lableddlSalesGroup.Visible = true;
    //        lableddlSalesOffice.Visible = true;
    //    }        
    //}
    //New changes if Sales organization is "L002" End

    #endregion

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblCustomerGeneralId.Text = grvData.DataKeys[grdrow.RowIndex]["Cust_SalesArea1"].ToString();
            pnlMsg.Visible = false;
            FillSalesAreaData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}