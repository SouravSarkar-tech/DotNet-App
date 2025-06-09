using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;


public partial class Transaction_CustomerExtension : BasePage
{
    CustomerExtensionAccess ObjCustomerExtensionAccess = new CustomerExtensionAccess();
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

                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    trButton.Visible = true;
                    //btnSave.Visible = !btnNext.Visible;
                    lnlAddDetails.Visible = true;
                    grvData.Columns[6].Visible = true;
                    pnlAddData.Visible = true;
                    //CS_8200049196 Start
                    ucCustExtView.Visible = true;
                    //CS_8200049196 End
                }
                else
                {
                    lnlAddDetails.Visible = false;
                    grvData.Columns[6].Visible = false;
                    pnlAddData.Visible = false;
                    //CS_8200049196 Start
                    ucCustExtView.Visible = false;
                    //CS_8200049196 End
                }

                FillDataGrid();
                FillCustomerExtensionData();

                //ConfigureControl();
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
            lblCustomerExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Cust_Extension_Id"].ToString();
            pnlMsg.Visible = false;
            FillCustomerExtensionData();
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
            lblCustomerExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Cust_Extension_Id"].ToString();
            pnlMsg.Visible = false;
            DeleteCustomerExtension();
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
            if (SaveCustomerExtension())
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
            if (SaveCustomerExtension())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("CustomerExtension.aspx");
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
            if (SaveCustomerExtension())
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

    protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
    {
        txtCustomerCode.Text = txtCustomerCode.Text.ToUpper();
        string str = txtCustomerCode.Text.Substring(0, 1).ToUpper();
        //string str1 = txtCustomerCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtCustomerCode.Text);

        switch (str)
        {
            case "L":
                //regtxtCustomerCode.ValidationExpression = "^[\\S]{4}$";
                ddlCustomerAccGrp.SelectedValue = "26";

                break;
            default:
                //regtxtCustomerCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 199999) //Z001
                    ddlCustomerAccGrp.SelectedValue = "22";
                else if (strcode >= 200000 && strcode < 299999)//Z002
                    ddlCustomerAccGrp.SelectedValue = "23";
                else if (strcode >= 300000 && strcode < 399999)//Z003
                    ddlCustomerAccGrp.SelectedValue = "24";
                else if (strcode >= 400000 && strcode < 499999)//Z004
                    ddlCustomerAccGrp.SelectedValue = "25";
                else if (strcode >= 500000 && strcode < 599999)//Z006 500000-599999
                    ddlCustomerAccGrp.SelectedValue = "27";
                else if (strcode >= 700000 && strcode < 799999)//Z008 700000-799999
                    ddlCustomerAccGrp.SelectedValue = "29";
                break;
        }
        txtName.Focus();

        ConfigureControl();
    }

    protected void ddlCustomerAccGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConfigureControl();
    }

    protected void ddlCreditControlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        SetCreditControlVisible();
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
    }

    protected void ddlDistributionChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        ddlCreditCurrency.SelectedValue = "";
        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        SetCreditControlVisible();

        if (ddlDivision.SelectedValue == "4" || ddlDivision.SelectedValue == "5" || ddlDivision.SelectedValue == "15")
        {
            ddlInvoiceDates.SelectedValue = "Z1";
            ddlInvoiceListSchedule.SelectedValue = "Z1";

            ddlInvoiceDates.Enabled = false;
            ddlInvoiceListSchedule.Enabled = false;

            reqddlDeliveringPlant.Enabled = true;
            lableddlDeliveringPlant.Visible = true;

            if (ddlDivision.SelectedValue == "4")
                ddlPriceGroup.SelectedValue = "57";

            if (ddlDivision.SelectedValue == "5")
                ddlPriceGroup.SelectedValue = "58";
        }
        else
        {
            ddlInvoiceDates.Enabled = true;
            ddlInvoiceListSchedule.Enabled = true;

            reqddlDeliveringPlant.Enabled = false;
            lableddlDeliveringPlant.Visible = false;

            ddlInvoiceDates.SelectedValue = "";
            ddlInvoiceListSchedule.SelectedValue = "";
            ddlPriceGroup.SelectedValue = "";
        }

        ddlInvoiceDates.Enabled = false;
        ddlInvoiceListSchedule.Enabled = false;

        reqddlInvoiceDates.Visible = false;
        reqddlInvoiceListSchedule.Visible = false;

        //SDT20052019 Added by NR
        IRFSetDefualt();
        //EDT20052019 Added by NR
    }

    /// <summary>
    /// DT20052019 Added by NR
    /// set defualt value
    /// </summary>
    private void IRFSetDefualt()
    {
        MaterialMasterAccess materialMasterAccess1 = new MaterialMasterAccess();
        if (materialMasterAccess1.CheckRequestType(lblMasterHeaderId.Text) > 0)
        {
            ddlInvoiceDates.SelectedValue = "Z1";
            ddlInvoiceListSchedule.SelectedValue = "Z1";

            ddlInvoiceDates.Enabled = false;
            ddlInvoiceListSchedule.Enabled = false;

            reqddlInvoiceDates.Visible = true;
            reqddlInvoiceListSchedule.Visible = true;
        }
        else
        {
            ddlInvoiceDates.SelectedValue = "Z1";
            ddlInvoiceListSchedule.SelectedValue = "Z1";

            ddlInvoiceDates.Enabled = true;
            ddlInvoiceListSchedule.Enabled = true;
        }
    }

    protected void ddlSalesOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetSalesGroupList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "Sales_Group_Desc", "Sales_Group_Id", "");
    }

    #endregion

    #region Private Methods

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlCustomerAccGrp, "pr_GetCustomerAccGrpList 0", "Customer_Acc_Grp_Name", "Customer_Acc_Grp_Id");

        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','1'", "Division_Name", "Division_Id", "");

        helperAccess.PopuplateComboBox(ddlCurrency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code", "");
        helperAccess.PopuplateDropDownList(ddlSalesDistrict, "pr_GetDropDownListByControlNameModuleType 'C','ddlSalesDistrict'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetSalesGroupList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "Sales_Group_Desc", "Sales_Group_Id", "");

        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlPriceGroup, "pr_GetDropDownListByControlNameModuleType 'C','ddlPriceGroup','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateComboBox(ddlInvoiceDates, "pr_GetDropDownListByControlNameModuleType 'C','ddlInvoiceDates','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlInvoiceListSchedule, "pr_GetDropDownListByControlNameModuleType 'C','ddlInvoiceListSchedule','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateComboBox(ddlCreditCurrency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code", "");
        helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
		//Start Change By Swati to set default value
        ddlCurrency.SelectedValue = "INR";
		//End Change
    }

    private bool SaveCustomerExtension()
    {
        CustomerExtension ObjCustomerExtension = GetControlsValue();
        bool flg = false;
        try
        {
            if (ObjCustomerExtensionAccess.Save(ObjCustomerExtension) > 0)
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
            txtCustomerCode.Text = "";
            txtName.Text = "";

            lblCustomerExtensionId.Text = "0";
            txtcountryKeyExport.Text = "";

            ddlCompanyCode.SelectedValue = "32";

            ddlCreditCurrency.SelectedValue = "INR";
            txtCustomer_credit_limit.Text = "";
            SetCreditControlVisible();

            if (Convert.ToString(Session[StaticKeys.EXTDASIRF]) == "EXTDASIRF")
            {
                ddlInvoiceDates.SelectedValue = "Z1";
                ddlInvoiceListSchedule.SelectedValue = "Z1";
                ddlInvoiceDates.Enabled = false;
                ddlInvoiceListSchedule.Enabled = false;

            }
            else
            {
                ddlInvoiceDates.SelectedValue = "";
                ddlInvoiceListSchedule.SelectedValue = "";
                ddlInvoiceDates.Enabled = true;
                ddlInvoiceListSchedule.Enabled = true;
                if (ddlDivision.SelectedValue == "4" || ddlDivision.SelectedValue == "5" || ddlDivision.SelectedValue == "15")
                {
                    ddlInvoiceDates.SelectedValue = "Z1";
                    ddlInvoiceListSchedule.SelectedValue = "Z1";

                    ddlInvoiceDates.Enabled = false;
                    ddlInvoiceListSchedule.Enabled = false;
                     
                }
            }

            //SDT20052019 Added by NR
            IRFSetDefualt();
            //EDT20052019 Added by NR

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool IsDuplicateEntry()
    {
        bool flg = false;

        if (lblCustomerExtensionId.Text == "0")
        {

            //CS_8200049196

            if (ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(lblMasterHeaderId.Text), "Csa", ddlSalesOrginization.SelectedValue, ddlDistributionChannel.SelectedValue, ddlDivision.SelectedValue) > 0)
            {
                flg = true;
            }

            //DataSet dstData = new DataSet();
            //dstData = ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(lblMasterHeaderId.Text), "Ext"
            //    , ddlSalesOrginization.SelectedValue, ddlDistributionChannel.SelectedValue, ddlDivision.SelectedValue);

            //if (dstData.Tables[0].Rows.Count > 0)
            //{
            //    flg = true;
            //}
            //foreach (GridViewRow gr in grvData.Rows)
            //{
            //    if (txtCustomerCode.Text == ((Label)gr.FindControl("lblCustomerCode")).Text)
            //    {
            //        if (ddlSalesOrginization.SelectedValue == ((Label)gr.FindControl("lblSalesOrgId")).Text && ddlDistributionChannel.SelectedValue == ((Label)gr.FindControl("lblDistributionChnlId")).Text && ddlDivision.SelectedValue == ((Label)gr.FindControl("lblDivisionId")).Text)
            //        {
            //            flg = true;
            //        }
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

    private void DeleteCustomerExtension()
    {
        ObjCustomerExtensionAccess.DeleteCustomerExtensionData(Convert.ToInt32(lblCustomerExtensionId.Text));
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjCustomerExtensionAccess.GetCustomerExtensionData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds.Tables[0];
            grvData.DataBind();

            if (ds.Tables[1].Rows.Count > 0)
                lblCustomerType.Text = ds.Tables[1].Rows[0]["Customer_Category"].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private CustomerExtension GetCustomerExtension()
    {
        return ObjCustomerExtensionAccess.GetCustomerExtension(Convert.ToInt32(lblCustomerExtensionId.Text));
    }

    private CustomerExtension GetControlsValue()
    {
        CustomerExtension ObjCustomerExtension = new CustomerExtension();
        Utility objUtil = new Utility();

        ObjCustomerExtension.Cust_Extension_Id = Convert.ToInt32(lblCustomerExtensionId.Text);
        ObjCustomerExtension.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjCustomerExtension.Customer_Code = txtCustomerCode.Text;
        ObjCustomerExtension.Company_Code = ddlCompanyCode.SelectedValue;
        ObjCustomerExtension.Customer_Acc_Grp = ddlCustomerAccGrp.SelectedValue;
        ObjCustomerExtension.Customer_Desc = txtName.Text;

        ObjCustomerExtension.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
        ObjCustomerExtension.Distribution_Channel_Id = ddlDistributionChannel.SelectedValue;
        ObjCustomerExtension.Division_ID = ddlDivision.SelectedValue;

        ObjCustomerExtension.SalesDistrict = ddlSalesDistrict.SelectedValue;
        ObjCustomerExtension.SalesOffice = ddlSalesOffice.SelectedValue;
        ObjCustomerExtension.Currency = ddlCurrency.SelectedValue;
        ObjCustomerExtension.SalesGroup = ddlSalesGroup.SelectedValue;
        ObjCustomerExtension.countryKeyExport = txtcountryKeyExport.Text;
        ObjCustomerExtension.DeliveringPlant = ddlDeliveringPlant.SelectedValue;

        ObjCustomerExtension.PriceGroup = ddlPriceGroup.SelectedValue;
        ObjCustomerExtension.InvoiceDates = ddlInvoiceDates.SelectedValue;
        ObjCustomerExtension.InvoiceListSchedule = ddlInvoiceListSchedule.SelectedValue;

        ObjCustomerExtension.Credit_Control_Area = ddlCreditControlArea.SelectedValue;
        ObjCustomerExtension.Customer_credit_limit = txtCustomer_credit_limit.Text;
        ObjCustomerExtension.Risk_category = ddlRiskcategory.SelectedValue;
        ObjCustomerExtension.Currency_Id = ddlCreditCurrency.SelectedValue;
        ObjCustomerExtension.Remarks = txtRemarks.Text;

        ObjCustomerExtension.IsActive = "1";
        ObjCustomerExtension.UserId = lblUserId.Text;
        ObjCustomerExtension.TodayDate = objUtil.GetDate();
        ObjCustomerExtension.IPAddress = objUtil.GetIpAddress();

        return ObjCustomerExtension;
    }

    private void FillCustomerExtensionData()
    {
        CustomerExtension ObjCustomerExtension = GetCustomerExtension();
        if (ObjCustomerExtension.Cust_Extension_Id > 0)
        {
            lblCustomerExtensionId.Text = ObjCustomerExtension.Cust_Extension_Id.ToString();
            lblMasterHeaderId.Text = ObjCustomerExtension.Master_Header_Id.ToString();

            txtCustomerCode.Text = ObjCustomerExtension.Customer_Code;
            ddlCompanyCode.SelectedValue = ObjCustomerExtension.Company_Code;
            ddlCustomerAccGrp.SelectedValue = ObjCustomerExtension.Customer_Acc_Grp;
            txtName.Text = ObjCustomerExtension.Customer_Desc;

            ddlSalesOrginization.SelectedValue = ObjCustomerExtension.Sales_Organization_Id;
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

            ddlDistributionChannel.SelectedValue = ObjCustomerExtension.Distribution_Channel_Id;
            helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

            ddlDivision.SelectedValue = ObjCustomerExtension.Division_ID;

            helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlCreditControlArea, "pr_GetCreditControlAreaList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            ddlSalesDistrict.SelectedValue = ObjCustomerExtension.SalesDistrict.ToString();
            txtcountryKeyExport.Text = ObjCustomerExtension.countryKeyExport.ToString();
            ddlDeliveringPlant.SelectedValue = ObjCustomerExtension.DeliveringPlant;

            ddlPriceGroup.SelectedValue = ObjCustomerExtension.PriceGroup;
            ddlInvoiceDates.SelectedValue = ObjCustomerExtension.InvoiceDates;
            ddlInvoiceListSchedule.SelectedValue = ObjCustomerExtension.InvoiceListSchedule;

            try { ddlSalesOffice.SelectedValue = ObjCustomerExtension.SalesOffice.ToString(); }
            catch { }

            helperAccess.PopuplateDropDownList(ddlSalesGroup, "pr_GetSalesGroupList '" + lblMasterHeaderId.Text + "','CEXT','" + lblCustomerExtensionId.Text + "','" + ddlSalesOffice.SelectedValue + "'", "Sales_Group_Desc", "Sales_Group_Id", "");
            ddlSalesGroup.SelectedValue = ObjCustomerExtension.SalesGroup.ToString();

            ddlCurrency.SelectedValue = ObjCustomerExtension.Currency.ToString();

            ddlCreditControlArea.SelectedValue = ObjCustomerExtension.Credit_Control_Area;
            txtCustomer_credit_limit.Text = ObjCustomerExtension.Customer_credit_limit;
            helperAccess.PopuplateDropDownList(ddlRiskcategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + lblSectionId.Text + "','" + ddlCreditControlArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            ddlRiskcategory.SelectedValue = ObjCustomerExtension.Risk_category;
            ddlCreditCurrency.SelectedValue = ObjCustomerExtension.Currency_Id;
            txtRemarks.Text = ObjCustomerExtension.Remarks;
        }
        else
        {
            lblCustomerExtensionId.Text = "0";

            ddlCompanyCode.SelectedValue = "32";
            ddlCreditCurrency.SelectedValue = "INR";

            if(Convert.ToString(Session[StaticKeys.EXTDASIRF]) == "EXTDASIRF")
            {
                ddlInvoiceDates.SelectedValue = "Z1";
                ddlInvoiceListSchedule.SelectedValue = "Z1";
                ddlInvoiceDates.Enabled = false;
                ddlInvoiceListSchedule.Enabled = false;
            }
            else
            {
                ddlInvoiceDates.SelectedValue = "";
                ddlInvoiceListSchedule.SelectedValue = "";
                ddlInvoiceDates.Enabled = true;
                ddlInvoiceListSchedule.Enabled = true;
                if (ddlDivision.SelectedValue == "4" || ddlDivision.SelectedValue == "5" || ddlDivision.SelectedValue == "15")
                {
                    ddlInvoiceDates.SelectedValue = "Z1";
                    ddlInvoiceListSchedule.SelectedValue = "Z1";

                    ddlInvoiceDates.Enabled = false;
                    ddlInvoiceListSchedule.Enabled = false;

                }

                //SDT20052019 Added by NR
                IRFSetDefualt();
                //EDT20052019 Added by NR
            }
        }
		/// Carve_LC17&LC23_8400000406 
        //ddlCompanyCode.Enabled = false;
		/// Carve_LC17&LC23_8400000406 
        ConfigureControl();
        SetCreditControlVisible();
    }

    private void ConfigureControl()
    {
        MasterAccess ObjMasterAccess = new MasterAccess();

        //if (Session[StaticKeys.SelectedModulePlantGrp].ToString() == null)
        Session[StaticKeys.SelectedModulePlantGrp] = ObjMasterAccess.GetSelectedModulePlantGrp(ddlCompanyCode.SelectedValue, ddlCustomerAccGrp.SelectedValue, "C");

        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        if (str != "")
        {
            SectionConfiguration.Sales_area_data obj = new SectionConfiguration.Sales_area_data();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
    }

    #endregion
}