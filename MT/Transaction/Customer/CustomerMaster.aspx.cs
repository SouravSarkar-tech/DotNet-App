using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using System.Globalization;

public partial class Transaction_Customer_CustomerMaster : System.Web.UI.Page
{
    DataSet dstData = new DataSet();
    HelperAccess ObjHelperAccess = new HelperAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                PopulateDropDownList();
                ReadModules();
                ReadProfileWiseModules(userProfileId, lblUserId.Text);
                ReadCustomerMasterRequests();
            }

            ShowHideBtn();
        }
    }


    private void ShowHideBtn()
    {
        if (Convert.ToString(Session[StaticKeys.LoggedIn_User_Profile_Id]) == "2")
        {
            btnCreateNew.Attributes.Add("enabled", "enabled");
            btnChangeBulkRequestC.Enabled = true;
            btnChangeExtensionC.Enabled = true;
            btnBlockRequest.Enabled = true;
            btnCopyRequest.Enabled = true; 
        }
        else
        {

            btnCreateNew.Attributes.Add("disabled", "disabled");
            btnChangeBulkRequestC.Enabled = false;
            btnChangeExtensionC.Enabled = false;
            btnBlockRequest.Enabled = false;
            btnCopyRequest.Enabled = false; 
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
                regtxtCustomerCode.ValidationExpression = "^[\\S]{4}$";
                ddlCustomerAccGroupC.SelectedValue = "88";

                break;
            default:
                regtxtCustomerCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 199999) //Z001
                    ddlCustomerAccGroupC.SelectedValue = "84";
                else if (strcode >= 200000 && strcode < 299999)//Z002
                    ddlCustomerAccGroupC.SelectedValue = "85";
                else if (strcode >= 300000 && strcode < 399999)//Z003
                    ddlCustomerAccGroupC.SelectedValue = "86";
                else if (strcode >= 400000 && strcode < 499999)//Z004
                    ddlCustomerAccGroupC.SelectedValue = "87";
                else if (strcode >= 500000 && strcode < 599999)//Z006 500000-599999
                    ddlCustomerAccGroupC.SelectedValue = "89";
                else if (strcode >= 700000 && strcode < 799999)//Z008 700000-799999
                    ddlCustomerAccGroupC.SelectedValue = "91";
                break;
        }
        txtCustomerName.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsValidSearch() == true)
        {
            ReadCustomerMasterRequests();
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Please select valid date range and date range should not exceed 90 days.";
        }
    }
    /// <summary>
    /// /add 814364
    /// </summary>
    /// <returns></returns>
    private bool IsValidSearch()
    {
        bool flg = false;

        int diffOfDatesi = 0;
        if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
        {
            try
            {
                var fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var diffOfDates = (tdate - fdate).TotalDays;
                diffOfDatesi = Convert.ToInt32(diffOfDates);
            }
            catch (Exception ex)
            {
               // _log.Error("Exception" + ex.Message);
            }
        }


        if ((ddlStatus.SelectedValue == "P" || ddlStatus.SelectedValue == "R" || ddlStatus.SelectedValue == "SUB"))
        {
            flg = true;
        }
        else if ((txtRequestNo.Text.Trim() != "" || txtSAPCode.Text.Trim() != ""))
        {
            flg = true;
        }
        else if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "" && diffOfDatesi == 0)
        {
            flg = false;
        }
        else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && diffOfDatesi <= 90)
        {
            flg = true;
        }
        else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && diffOfDatesi >= 90)
        {
            flg = false;
        }
        return flg;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        try
        {
            //Start Addition By Swati M Date: 15.10.2018
            if (ddlDivisionType.SelectedValue == null || ddlDivisionType.SelectedValue == "")
            {
                masterHeaderId = materialAccess.SaveMaterialHeaderC("0", ddlCompany.SelectedValue, ddlModule.SelectedValue, lblUserId.Text, lblMode.Text, ddlCustomerType.SelectedValue, ddlSalesRegion.SelectedValue);
            }
            else
            {
                masterHeaderId = materialAccess.SaveMaterialHeaderCM("0", ddlCompany.SelectedValue, ddlModule.SelectedValue, lblUserId.Text, lblMode.Text, ddlDivisionType.SelectedValue, ddlDivision.SelectedValue, ddlZone.SelectedValue, "", ddlSalesRegion.SelectedValue);
            }
            //End Addition By Swati M Date: 15.10.2018
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                Response.Redirect("General.aspx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = ObjMasterAccess.GenerateCopyRequest(GetSelectedPkID(), ddlCompany.SelectedValue, ddlModule.SelectedValue, lblUserId.Text, lblMode.Text, ddlCustomerType.SelectedValue);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "R";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = ObjMasterAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("General.aspx");
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("CustomerBlock.aspx");
        else if (Session[StaticKeys.ActionType].ToString() == "C")
            Response.Redirect("CustomerChange.aspx");
        else if (Session[StaticKeys.ActionType].ToString() == "E")
            Response.Redirect("CustomerExtension.aspx");
        else
            Response.Redirect("General.aspx");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("CustomerBlock.aspx");
        else if (Session[StaticKeys.ActionType].ToString() == "C")
            Response.Redirect("CustomerChange.aspx");
        else if (Session[StaticKeys.ActionType].ToString() == "E")
            Response.Redirect("CustomerExtension.aspx");
        else
            Response.Redirect("General.aspx");
    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeRequest(GetSelectedPkID(), lblUserId.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.SelectedModule] = objMasterAccess.mModuleName.ToString();
            Session[StaticKeys.MaterialNo] = objMasterAccess.mMasterSAPCode.ToString();
            Response.Redirect("General.aspx");
        }
    }

    protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
		//Start Addition By Swati M Date: 08.10.2018
		//int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestC("0", "CMC", lblUserId.Text, "", ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);
        int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestCust("0", "CMC", lblUserId.Text,ddlZoneE.SelectedValue, "", ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);
		//End Change
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = "Bulk Request";
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Customer Bulk Change";

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("CustomerChange.aspx");
        }
    }

    protected void btnChangeExtension_Click(object sender, EventArgs e)
    {

        if (ddlCustomerTypeE.SelectedValue == "DASIRF")
        {
            Session[StaticKeys.EXTDASIRF] = "EXTDASIRF";
        }
        else
        {
            Session[StaticKeys.EXTDASIRF] = "";
        }

        MasterAccess objMasterAccess = new MasterAccess();
		//Start Addition By Swati M Date: 08.10.2018
		//int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestC("0", "CEXT", lblUserId.Text, "", ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);
        int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestCust("0", "CEXT", lblUserId.Text,ddlZoneE.SelectedValue, "", ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);
		//End Change
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "E";
            Session[StaticKeys.MaterialNo] = "Extension Request";
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Customer Extension";

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("CustomerExtension.aspx");
        }
    }

    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        // Added by manali for saving zone in databse and workflow load
        int MasterHeaderId = objMasterAccess.GenerateBlockRequestC("0", ddlCompanyCode.SelectedValue, ddlCustomerAccGroupC.SelectedValue, "CMB", lblUserId.Text, txtCustomerCode.Text, txtCustomerName.Text, ddlCustomerTypeC.SelectedValue, ddlSalesRegionC.SelectedValue, ddlZoneB.SelectedValue); 
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = "128";//ddlVendorAccGroupC.SelectedValue;
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "B";
            Session[StaticKeys.MaterialNo] = txtCustomerCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Customer Master Block";// ddlVendorAccGroupC.SelectedItem.Text;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("CustomerBlock.aspx");
        }
    }

    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        // Added by manali for saving zone in databse and workflow load
        int MasterHeaderId = objMasterAccess.GenerateBlockRequestC("0", ddlCompanyCode.SelectedValue, ddlCustomerAccGroupC.SelectedValue, "CMU", lblUserId.Text, txtCustomerCode.Text, txtCustomerName.Text, ddlCustomerTypeC.SelectedValue, ddlSalesRegionC.SelectedValue, ddlZoneB.SelectedValue);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = "129";//ddlVendorAccGroupC.SelectedValue;
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "U";
            Session[StaticKeys.MaterialNo] = txtCustomerCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Customer Master UnBlock";// ddlVendorAccGroupC.SelectedItem.Text;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("CustomerBlock.aspx");
        }
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        grdSearch.PageIndex = e.NewPageIndex;
        //SDT29052019 (Update Date : 29072019 Commented By Nitin R)
        //dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "C", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

        //Nitin R SDT29052019 (Update Date : 29072019 Updated By Nitin R)
        dstData = objMatAccess.SearchCustomerMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "C", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

        grdSearch.DataSource = dstData.Tables[0].DefaultView;
        grdSearch.DataBind();
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApprover.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory(ddlModule.SelectedValue, lblUserId.Text, ddlCustomerType.SelectedValue, ddlSalesRegion.SelectedValue);
    }

    protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApprover.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory(ddlModule.SelectedValue, lblUserId.Text, ddlCustomerType.SelectedValue, ddlSalesRegion.SelectedValue);

        ObjHelperAccess.PopuplateDropDownList(ddlSalesRegion, "pr_Get_Sales_Region 'C','" + ddlCustomerType.SelectedValue + "'", "Sales_Region_Name", "Sales_Region_Id", "");

        bool flg;
        if (ddlCustomerType.SelectedValue == "ExpFor")
        {
            flg = true;
        }
        else
        {
            flg = false;
        }
        trSalesRegion.Visible = flg;
        reqddlSalesRegion.Enabled = flg;
        //Start Addition By Swati M Date: 08.10.2018
        if (ddlCustomerType.SelectedValue == "DASIRF")
        {
            trDivision.Visible = true;
            trDivisionType.Visible = true;
            trZone.Visible = true;
        }
        else
        {
            trDivisionType.Visible = false;
            trDivision.Visible = false;
            trZone.Visible = false;
        }
        Session[StaticKeys.DivTypeCusts] = "";
        //End Addition By Swati M Date: 08.10.2018
    }

    protected void ddlSalesRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApprover.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory(ddlModule.SelectedValue, lblUserId.Text, ddlCustomerType.SelectedValue, ddlSalesRegion.SelectedValue);
    }

    protected void ddlCustomerTypeC_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApproverC.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory("128", lblUserId.Text, ddlCustomerTypeC.SelectedValue, ddlSalesRegionC.SelectedValue);

        ObjHelperAccess.PopuplateDropDownList(ddlSalesRegionC, "pr_Get_Sales_Region 'C','" + ddlCustomerTypeC.SelectedValue + "'", "Sales_Region_Name", "Sales_Region_Id", "");

        bool flg;
        if (ddlCustomerTypeC.SelectedValue == "ExpFor")
        {
            flg = true;
        }
        else
        {
            flg = false;
        }
        trSalesRegionC.Visible = flg;
        reqddlSalesRegionC.Enabled = flg;
		//Start Addition By Swati M Date: 08.10.2018
        if (ddlCustomerTypeC.SelectedValue == "DASIRF")
        {
            trZoneB.Visible = true;
        }
        else
        {
            trZoneB.Visible = false;
        }
		//End Change
    }

    protected void ddlSalesRegionC_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApproverC.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory("128", lblUserId.Text, ddlCustomerTypeC.SelectedValue, ddlSalesRegionC.SelectedValue);
    }

    protected void ddlCustomerTypeE_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApproverE.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory("130", lblUserId.Text, ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);

        ObjHelperAccess.PopuplateDropDownList(ddlSalesRegionE, "pr_Get_Sales_Region 'C','" + ddlCustomerTypeE.SelectedValue + "'", "Sales_Region_Name", "Sales_Region_Id", "");

        bool flg;
        if (ddlCustomerTypeE.SelectedValue == "ExpFor")
        {
            flg = true;
        }
        else
        {
            flg = false;
        }
        trSalesRegionE.Visible = flg;
        reqddlSalesRegionE.Enabled = flg;
		//Start Addition By Swati M Date: 08.10.2018
        if (ddlCustomerTypeE.SelectedValue == "DASIRF")
        {
            trZoneE.Visible = true;
        }
        else
        {
            trZoneE.Visible = false;
        }
		//End Change
    }

    protected void ddlSalesRegionE_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApproverE.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByModuleIdMasterCategory("130", lblUserId.Text, ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);
    }

    //Start Addition By Swati M Date: 08.10.2018
    protected void ddlDivisionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjHelperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivision " + ddlDivisionType.SelectedValue + "", "Division", "ID", "");
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApprover.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByDivTypeOrZone(ddlModule.SelectedValue, lblUserId.Text, ddlCustomerType.SelectedValue, ddlDivisionType.SelectedValue, ddlZone.SelectedValue);
        //STD07052019 add div sestion for select defualt div in Sales
        Session[StaticKeys.DivTypeCusts] = Convert.ToString(ddlDivisionType.SelectedValue);
        Session[StaticKeys.DivCusts] = Convert.ToString(ddlDivision.SelectedValue);
        //ETD07052019
    }
    //End Addition By Swati M Date: 08.10.2018

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        //STD07052019 add div sestion for select defualt div in Sales
        Session[StaticKeys.DivCusts] = Convert.ToString(ddlDivision.SelectedValue);
        //ETD07052019
    }

    #endregion

    #region Private Functions

    public void PopulateDropDownList()
    {
        try
        {
            ObjHelperAccess.PopuplateDropDownList(ddlCustomerType, "pr_GetMasterCategoryByModuleType 'C'", "Master_Category_Name", "Master_Category_Code", "");
            ObjHelperAccess.PopuplateDropDownList(ddlCustomerTypeC, "pr_GetMasterCategoryByModuleType 'C'", "Master_Category_Name", "Master_Category_Code", "");
            ObjHelperAccess.PopuplateDropDownList(ddlCustomerTypeE, "pr_GetMasterCategoryByModuleType 'C'", "Master_Category_Name", "Master_Category_Code", "");

            //Start Addition By Swati M Date: 08.10.2018
            ObjHelperAccess.PopuplateDropDownList(ddlDivisionType, "pr_GetDivisionType", "Division_Type", "ID",  "");
            //ObjHelperAccess.PopuplateDropDownList(ddlCustomerType, "pr_GetDivision " + ddlDivisionType.SelectedValue + "", "ID", "Division_Type", "");
            ObjHelperAccess.PopuplateDropDownList(ddlZone, "pr_GetZone", "Zone", "ID", "");
            ObjHelperAccess.PopuplateDropDownList(ddlZoneB, "pr_GetZone", "Zone", "ID", "");
            ObjHelperAccess.PopuplateDropDownList(ddlZoneE, "pr_GetZone", "Zone", "ID", "");
            //End Addition By Swati M Date: 08.10.2018


            ObjHelperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyList", "Company_Name", "Company_Id");

            try
            {
                ddlCompany.SelectedValue = "32";
                ddlCompanyCode.SelectedValue = "32";
 /// Carve_LC17&LC23_8400000406
                //ddlCompany.Enabled = false;
                //ddlCompanyCode.Enabled = false;
				 /// Carve_LC17&LC23_8400000406
            }
            catch { }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadCustomerMasterRequests()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
            //Code commented and new code added by Swati on 17.04.2019
            //dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "C", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            dstData = objMatAccess.SearchCustomerMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "C", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            //End Changes
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnCopyRequest.Visible = false;
            }

            if (ddlCustomerAccGroupC.Items.Count <= 1)
            {
                btnChangeBulkRequest.Enabled = false;
                btnChangeExtension.Enabled = false;
            }
            else
            {
                btnChangeBulkRequest.Enabled = true;
                btnChangeExtension.Enabled = true;
            }

            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadModules()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            ddlModuleSearch.DataSource = objMatAccess.ReadModules("C");
            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            DataSet ds = objMatAccess.ReadProfileWiseModules(profileId, userId, "C");
            ddlModule.DataSource = ds;
            ddlModule.DataTextField = "Module_Name";
            ddlModule.DataValueField = "Module_Id";
            ddlModule.DataBind();

            ddlCustomerAccGroupC.DataSource = ds;
            ddlCustomerAccGroupC.DataTextField = "Module_Name";
            ddlCustomerAccGroupC.DataValueField = "Module_Id";
            ddlCustomerAccGroupC.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetSelectedPkID()
    {
        string strPk = string.Empty;
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    strPk = lblRequestID.Text;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblDivisionType = grv.FindControl("lblDivisionType") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MaterialNo] = lblMasterCode.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    //SDT29052019
                    Session[StaticKeys.DivTypeCusts] = lblDivisionType.Text;
                    //EDT29052019
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strPk;
    }

    #endregion

    //Start Addition By Swati M Date: 30.10.2018
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        lblFirstApprover.Text = "First Approver : " + ObjMasterAccess.GetFirstApproverByDivTypeOrZone(ddlModule.SelectedValue, lblUserId.Text, ddlCustomerType.SelectedValue, ddlDivisionType.SelectedValue, ddlZone.SelectedValue);
    }
    //END Addition By Swati M Date: 30.10.2018
}