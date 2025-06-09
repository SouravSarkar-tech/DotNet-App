using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Configuration;
using log4net;
using System.Globalization;

public partial class Transaction_ProfitCenter_ProfitCenterMaster : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    DataSet dstData = new DataSet();
    #region Page Events

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                    PopulateDropDownList(userProfileId);
                    ReadMasterRequests();

                }
                ShowHideBtn();
            }
        }
        catch (Exception ex) { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    private void ShowHideBtn()
    {
        try
        {
            if (Convert.ToString(Session[StaticKeys.LoggedIn_User_Profile_Id]) == "2")
            {
                btnCreateNew.Attributes.Add("enabled", "enabled");
                btnChangeBulkRequest.Enabled = true;
                btnBlockRequest.Enabled = true;
            }
            else
            {
                btnCreateNew.Attributes.Add("disabled", "disabled");
                btnChangeBulkRequest.Enabled = false;
                btnBlockRequest.Enabled = false;
            }
        }
        catch (Exception ex) { _log.Error("ShowHideBtn", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
        int masterHeaderId;
        try
        { /// Carve_LC17&LC23_8400000406
            masterHeaderId = ObjPCAccess.SavePCHeader("0", ddlCompany.SelectedValue, ddlCreateModule.SelectedValue, lblUserId.Text, lblMode.Text, ddlPlantm.SelectedValue);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlCreateModule.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlCreateModule.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
				 /// Carve_LC17&LC23_8400000406
                Session[StaticKeys.MaterialPlantId] = ddlPlantm.SelectedValue;
                Session[StaticKeys.MaterialPlantName] = ddlPlantm.SelectedItem.Text;
				 /// Carve_LC17&LC23_8400000406
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = ObjPCAccess.mRequestNo; 

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                Session[StaticKeys.SelectedddlCompany] = ddlCompany.SelectedValue;
                Response.Redirect("ProfitCenterCreate.aspx");
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnNext_Click", ex);
        }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            lblPk.Text = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = lblPk.Text;
            Session[StaticKeys.Mode] = "V";
            Session[StaticKeys.MaterialType] = "";
            if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            {
                Response.Redirect("ProfitCenterBlock.aspx");
            }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                Response.Redirect("ProfitCenterChange.aspx", false);
            }
            else
            {
                Response.Redirect("ProfitCenterCreate.aspx", false);
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnView_Click", ex);
        }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            lblPk.Text = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = lblPk.Text;
            Session[StaticKeys.Mode] = "M";
            Session[StaticKeys.MaterialType] = "";
            if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            {
                Response.Redirect("ProfitCenterBlock.aspx");
            }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                Response.Redirect("ProfitCenterChange.aspx", false);
            }
            else
            {
                Response.Redirect("ProfitCenterCreate.aspx", false);
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnModify_Click", ex);
        }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
	 /// Carve_LC17&LC23_8400000406
    protected void btnNextC_Click(object sender, EventArgs e)
    {
        ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
        try
        {
            int MasterHeaderId = ObjPCAccess.GenerateChangeBulkRequest("0", "PCC", lblUserId.Text, ddlPlantc.SelectedValue, ddlCompanyCodec.SelectedValue, "", "");
            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ObjPCAccess.mModule_Id.ToString();
                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.MaterialPlantId] = ddlPlantc.SelectedValue;
                Session[StaticKeys.MaterialPlantName] = ddlPlantc.SelectedItem.Text;
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "C";
                Session[StaticKeys.MaterialNo] = "";
                Session[StaticKeys.RequestNo] = ObjPCAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "Profit Center Bulk Change";

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
               /// Carve_LC17&LC23_8400000406
			    Session[StaticKeys.SelectedddlCompany] = ddlCompanyCodec.SelectedValue;
				 /// Carve_LC17&LC23_8400000406
                Response.Redirect("ProfitCenterChange.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnChangeBulkRequest_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
        try
        {
            int iModuleProfitCB = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCB"]);
            //ddlCompanyCode.SelectedValue = "32";
 /// Carve_LC17&LC23_8400000406
            int MasterHeaderId = ObjPCAccess.GenerateBlockRequest("0", ddlCompanyCode.SelectedValue, Convert.ToString(iModuleProfitCB), "PCB", lblUserId.Text, txtProfitCenterCode.Text, txtProfitCenterName.Text, ddlPlantb.SelectedValue);
            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = Convert.ToString(iModuleProfitCB);

                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
				 /// Carve_LC17&LC23_8400000406
                Session[StaticKeys.MaterialPlantId] = ddlPlantb.SelectedValue;
                Session[StaticKeys.MaterialPlantName] = ddlPlantb.SelectedItem.Text;
				 /// Carve_LC17&LC23_8400000406
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "B";
                Session[StaticKeys.MaterialNo] = txtProfitCenterCode.Text;
                Session[StaticKeys.ProfitCenterName] = txtProfitCenterName.Text;
                Session[StaticKeys.RequestNo] = ObjPCAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "Profit Center Block";
                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
				 /// Carve_LC17&LC23_8400000406
                Session[StaticKeys.SelectedddlCompany] = ddlCompanyCode.SelectedValue;
				 /// Carve_LC17&LC23_8400000406
                Response.Redirect("ProfitCenterBlock.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnBlockRequest_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
        try
        {
            //ddlCompanyCode.SelectedValue = "32";
            int iModuleProfitCU = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCU"]);
 /// Carve_LC17&LC23_8400000406
            int MasterHeaderId = ObjPCAccess.GenerateBlockRequest("0", ddlCompanyCode.SelectedValue, Convert.ToString(iModuleProfitCU), "PCU", lblUserId.Text, txtProfitCenterCode.Text, txtProfitCenterName.Text, ddlPlantb.SelectedValue);
            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = Convert.ToString(iModuleProfitCU);

                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "U";
                Session[StaticKeys.MaterialNo] = txtProfitCenterCode.Text;
                Session[StaticKeys.ProfitCenterName] = txtProfitCenterName.Text;
                Session[StaticKeys.RequestNo] = ObjPCAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "Profit Center UnBlock";
                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                Session[StaticKeys.SelectedddlCompany] = ddlCompanyCode.SelectedValue;
                Response.Redirect("ProfitCenterBlock.aspx", false);
            }

        }
        catch (Exception ex)
        { _log.Error("btnUnBlockRequest_Click", ex); }
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
                _log.Error("Exception" + ex.Message);
            }
        }


        if ((ddlStatus.SelectedValue == "P" || ddlStatus.SelectedValue == "R" || ddlStatus.SelectedValue == "SUB"))
        {
            flg = true;
        }
        else if ((txtRequestNo.Text.Trim() != ""))
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
    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsValidSearch() == true)
        {

        try
        {
            ReadMasterRequests();
        }
        catch (Exception ex)
        {
            _log.Error("btnSearch_Click", ex);
            }
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Please select valid date range and date range should not exceed 90 days.";
        }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdSearch.PageIndex = e.NewPageIndex;
            ReadMasterRequests();
        }
        catch (Exception ex)
        {
            _log.Error("btnSearch_Click", ex);
        }
    }

    #endregion

    #region  Search

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="userProfileId"></param>
    public void PopulateDropDownList(string userProfileId)
    {
        try
        {

            ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();

            HelperAccess ObjHelperAccess = new HelperAccess();
            ObjHelperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyList", "Company_Name", "Company_Id");
           /// Carve_LC17&LC23_8400000406
		    ObjHelperAccess.PopuplateDropDownList(ddlCompanyCodec, "pr_GetCompanyList", "Company_Name", "Company_Id");
			 /// Carve_LC17&LC23_8400000406
            try
            {
                ddlCompany.SelectedValue = "32";
                ddlCompanyCode.SelectedValue = "32";
                ddlCompanyCodec.SelectedValue = "32";

                //ddlCompany.Enabled = false;
                //ddlCompanyCode.Enabled = false;
            }
            catch (Exception ex)
            { _log.Error("PopulateDropDownList", ex); }


            DataSet ds;
            ds = ObjPCAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "Y");


            ddlModuleName.DataSource = ds;
            ddlModuleName.DataTextField = "Module_Name";
            ddlModuleName.DataValueField = "Module_Id";
            ddlModuleName.DataBind();
            ddlModuleName.Items.Insert(0, new ListItem("All", "0"));

            DataSet ds1;
            ds1 = ObjPCAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "X");

            ddlCreateModule.DataSource = ds1;
            ddlCreateModule.DataTextField = "Module_Name";
            ddlCreateModule.DataValueField = "Module_Id";
            ddlCreateModule.DataBind();
            int iModuleProfitCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCC"]);
            ddlCreateModule.SelectedValue = Convert.ToString(iModuleProfitCC);

            ddlCreateModule.Enabled = false;
			 /// Carve_LC17&LC23_8400000406
            ObjHelperAccess.PopuplateDropDownList(ddlPlantm, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            ObjHelperAccess.PopuplateDropDownList(ddlPlantc, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            ObjHelperAccess.PopuplateDropDownList(ddlPlantb, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
 /// Carve_LC17&LC23_8400000406
        }
        catch (Exception ex) { _log.Error("PopulateDropDownList", ex); }
        HelperAccess helperAccess = new HelperAccess();
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        }
        catch (Exception ex) { _log.Error("PopulateDropDownList1", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    private void ReadMasterRequests()
    {
        ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
            dstData = ObjPCAccess.SearchMasterRequestsPC(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleName.SelectedValue, "X", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

            if (dstData != null && dstData.Tables != null)
            {
                grdSearch.DataSource = dstData.Tables[0].DefaultView;

                if (dstData.Tables[0].Rows.Count > 0)
                {
                    btnView.Visible = true;
                    btnModify.Visible = true;

                    if (ddlStatus.SelectedValue == "C")
                    {
                        btnModify.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "I")
                    {
                        btnModify.Visible = true;
                    }
                    else if (ddlStatus.SelectedValue == "R")
                    {
                        btnModify.Visible = true;
                    }
                    else if (ddlStatus.SelectedValue == "REJ")
                    {
                        btnModify.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "ALL")
                    {
                        btnModify.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "P")
                    {
                        btnModify.Visible = true;
                    }
                }
                else
                {
                    btnView.Visible = false;
                    btnModify.Visible = false;
                }

                grdSearch.DataBind();
            }
            else
            {
                grdSearch.DataSource = null;
                grdSearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            _log.Error("PopulateDropDownList1", ex);
        }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
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
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPendingFor = grv.FindControl("lblPendingFor") as Label;
                    Label lblSelectedPlant = grv.FindControl("lblSelectedPlant") as Label;
                     /// Carve_LC17&LC23_8400000406
					Label lblCompany = grv.FindControl("lblCompany") as Label;
 /// Carve_LC17&LC23_8400000406
                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.PendingFor] = lblPendingFor.Text;
                    Session[StaticKeys.MaterialPlantName] = lblSelectedPlant.Text;
					 /// Carve_LC17&LC23_8400000406
                    Session[StaticKeys.SelectedddlCompany] = lblCompany.Text;
					 /// Carve_LC17&LC23_8400000406
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("GetSelectedPkID", ex);
        }
        return strPk;
    }
     
    #endregion
}