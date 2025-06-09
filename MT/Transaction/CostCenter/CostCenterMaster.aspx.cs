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
using Accenture.MWT.DomainObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using log4net;
using System.Globalization;

public partial class Transaction_CostCenter_CostCenterMaster : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Page Events

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

                BindCostCenterGroup();
                PopulateDropDownList(userProfileId);

                ReadMaterialMasterRequests();
            }
        }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void txtCostCenterCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
        txtCostCenterCode.Text = txtCostCenterCode.Text.ToUpper();
        string str = txtCostCenterCode.Text.Substring(0, 1).ToUpper();
        string str1 = txtCostCenterCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtCostCenterCode.Text);
        txtCostCenterName.Focus();
        }
        catch (Exception ex)
        { _log.Error("txtCostCenterCode_TextChanged", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        try
        {

            //SDT17052019 Commented By NR  
            //    ddlCostCenterAccGroup.SelectedValue = "197";
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
            ddlCostCenterAccGroup.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCC"]);
 /// Carve_LC17&LC23_8400000406
            masterHeaderId = materialAccess.SaveMaterialHeaderC("0", ddlCompany.SelectedValue, ddlCostCenterAccGroup.SelectedValue, lblUserId.Text, lblMode.Text, "", "", ddlBusinessArean.SelectedValue);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlCostCenterAccGroup.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlCostCenterAccGroup.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
 /// Carve_LC17&LC23_8400000406
                Session[StaticKeys.SelectedddlCompany] = ddlCompany.SelectedValue;
                Session[StaticKeys.MaterialPlantId] = ddlBusinessArean.SelectedValue;
               /// Carve_LC17&LC23_8400000406
			     Response.Redirect("CostCenterCreate.aspx", false);
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("btnNext_Click", ex);
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = ObjMasterAccess.GenerateCopyRequest(GetSelectedPkID(), ddlCompany.SelectedValue, ddlCostCenterAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlCostCenterAccGroup.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlCostCenterAccGroup.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "R";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = ObjMasterAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("CostCenterCreate.aspx",false);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            _log.Error("btnCopy_Click", ex);
            //throw ex;
        }
    }

    protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        try
        { /// Carve_LC17&LC23_8400000406
            int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestCCN("0", "CCCHG", lblUserId.Text, txtCostCenterCode.Text, ddlBusinessAreac.SelectedValue, ddlCompanyCodec.SelectedValue, "");
            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "C";
                Session[StaticKeys.MaterialNo] = txtCostCenterCode.Text;
                Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "CostCenter Bulk Change";

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                /// Carve_LC17&LC23_8400000406
			    Session[StaticKeys.SelectedddlCompany] = ddlCompanyCodec.SelectedValue;
                Session[StaticKeys.MaterialPlantId] = ddlBusinessAreac.SelectedValue;
 /// Carve_LC17&LC23_8400000406
                Response.Redirect("CostCenterChange.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnChangeBulkRequest_Click", ex); }

    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        try
        {
        ddlCompanyCode.SelectedValue = "32";
        int MasterHeaderId = objMasterAccess.GenerateChangeRequestC("0", ddlCompanyCode.SelectedValue, txtCostCenterCode.Text, "", lblUserId.Text, txtCostCenterCode.Text, txtCostCenterName.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtCostCenterCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("CostCenterChange.aspx",false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnChangeRequest_Click", ex); }
    }

    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        try
        { /// Carve_LC17&LC23_8400000406 hide
            //ddlCompanyCode.SelectedValue = "32";
/// Carve_LC17&LC23_8400000406 hide
            //SDT17052019 Commented By NR  
            //ddlCostCenterAccGroup.SelectedValue = "197";
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
            ddlCostCenterAccGroup.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCC"]);
/// Carve_LC17&LC23_8400000406 
            int MasterHeaderId = objMasterAccess.GenerateBlockRequestCC("0", ddlCompanyCode.SelectedValue, ddlCostCenterAccGroup.SelectedValue, "CCB", lblUserId.Text, txtCostCenterCode.Text, txtCostCenterName.Text, "", "", ddlBusinessArea.SelectedValue);
            if (MasterHeaderId > 0)
            {
                //SDT17052019 Commented By NR
                //Session[StaticKeys.SelectedModuleId] = "199";
                //EDT17052019 Commented By NR  
                //SDT17052019 Change By NR , Desc : Get Module ID from web config
                Session[StaticKeys.SelectedModuleId] = Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCB"]);

            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "B";
            Session[StaticKeys.MaterialNo] = txtCostCenterCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Cost Center Block";// ddlCostCenterAccGroupC.SelectedItem.Text;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                /// Carve_LC17&LC23_8400000406 
				Session[StaticKeys.SelectedddlCompany] = ddlCompanyCode.SelectedValue;
                Session[StaticKeys.MaterialPlantId] = ddlBusinessArea.SelectedValue;
               /// Carve_LC17&LC23_8400000406 
			    Response.Redirect("CostCenterBlock.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnBlockRequest_Click", ex); }
    }

    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        try
        {/// Carve_LC17&LC23_8400000406 
            //ddlCompanyCode.SelectedValue = "32";
/// Carve_LC17&LC23_8400000406 
            //SDT17052019 Commented By NR  
            //ddlCostCenterAccGroup.SelectedValue = "197";
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
            ddlCostCenterAccGroup.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCC"]);
            //Carve_LC17&LC23_8400000406
            int MasterHeaderId = objMasterAccess.GenerateBlockRequestCC("0", ddlCompanyCode.SelectedValue, ddlCostCenterAccGroup.SelectedValue, "CCU", lblUserId.Text, txtCostCenterCode.Text, txtCostCenterName.Text, "", "", ddlBusinessArea.SelectedValue);
            if (MasterHeaderId > 0)
            {

            //SDT17052019 Commented By NR
            // Session[StaticKeys.SelectedModuleId] = "200";
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
            Session[StaticKeys.SelectedModuleId] = Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCU"]);


                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "U";
                Session[StaticKeys.MaterialNo] = txtCostCenterCode.Text;
                Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "Cost Center UnBlock";// ddlCostCenterAccGroupC.SelectedItem.Text;
                                                                           //CostCenter workflow modification start
                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
               /// Carve_LC17&LC23_8400000406 
			    Session[StaticKeys.SelectedddlCompany] = ddlCompanyCode.SelectedValue;
                Session[StaticKeys.MaterialPlantId] = ddlBusinessArea.SelectedValue;
/// Carve_LC17&LC23_8400000406 

                Response.Redirect("CostCenterBlock.aspx", false);
            }

        }
        catch (Exception ex)
        { _log.Error("btnUnBlockRequest_Click", ex); }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("CostCenterBlock.aspx");
        //SDT17052019 Commented By NR  
        //else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == "198")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Department ID from web config
        else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCHG"]))
            { 
                Response.Redirect("CostCenterChange.aspx", false);
            }
            else
            { 
            Response.Redirect("CostCenterCreate.aspx",false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnView_Click", ex); }
    }

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
            Response.Redirect("CostCenterBlock.aspx", false);
            }
            //SDT17052019 Commented By NR  
            //else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == "198")
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Department ID from web config
            else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCHG"]))
            { 
                Response.Redirect("CostCenterChange.aspx", false);
            }
            else
            { 
            Response.Redirect("CostCenterCreate.aspx",false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnModify_Click", ex); }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
        ReadMaterialMasterRequests();
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }

    DataSet dstData = new DataSet();

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
        grdSearch.PageIndex = e.NewPageIndex;
        dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlCostCenterAccGrp.SelectedValue, "I", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
        grdSearch.DataSource = dstData.Tables[0].DefaultView;
        grdSearch.DataBind();
        }
        catch (Exception ex)
        { _log.Error("grdSearch_PageIndexChanging", ex); }

    }

    void BindCostCenterGroup()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
        ddlCostCenterAccGrp.DataSource = objMatAccess.ReadModules("I");
        ddlCostCenterAccGrp.DataValueField = "Module_Id";
        ddlCostCenterAccGrp.DataTextField = "Module_Name";
        ddlCostCenterAccGrp.DataBind();
        ddlCostCenterAccGrp.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        { _log.Error("BindCostCenterGroup", ex); }
    }

    protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor lnkFrwrdNote = e.Row.FindControl("lnkFrwrdNote") as HtmlAnchor;
            Label lblForward = e.Row.FindControl("lblForward") as Label;
            Label lblPrimaryID = e.Row.FindControl("lblPrimaryID") as Label;
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            Utility ObjUtil = new Utility();
            DataSet ds = new DataSet();
            string CostCenterCode = dstData.Tables[0].Rows[0]["Customer_Code"].ToString();
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "37")
            {
                ds = objMatAccess.FindMDMApproved();
                foreach (TableCell cell in e.Row.Cells)
                {
                    //if ((Session[StaticKeys.SelectedModuleId].ToString() == "126") && (CostCenterCode != null || CostCenterCode != ""))

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string Master_Header_Id = ds.Tables[0].Rows[i]["Master_Header_Id"].ToString();
                            if (lblPrimaryID.Text == Master_Header_Id)
                                cell.BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

        }

        }
        catch (Exception ex)
        { _log.Error("grdSearch_RowDataBound", ex); }
    }

    #endregion

    #region CostCenter Search

    public void PopulateDropDownList(string userProfileId)
    {
        try
        {
            HelperAccess ObjHelperAccess = new HelperAccess();
            ObjHelperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyList", "Company_Name", "Company_Id");
            /// Carve_LC17&LC23_8400000406 
			ObjHelperAccess.PopuplateDropDownList(ddlCompanyCodec, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlBusinessArean, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
            ObjHelperAccess.PopuplateDropDownList(ddlBusinessArea, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
             ObjHelperAccess.PopuplateDropDownList(ddlBusinessAreac, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
/// Carve_LC17&LC23_8400000406 
            try
            {
                ddlCompany.SelectedValue = "32";
                ddlCompanyCode.SelectedValue = "32";
                ddlCompanyCodec.SelectedValue = "32";
                //INCID367612 
                //ddlCompany.Enabled = false;
                //INCID367612

                //ddlCompanyCode.Enabled = false;
            }
            catch (Exception ex)
            { _log.Error("PopulateDropDownList", ex); }

            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            HelperAccess helperAccess = new HelperAccess();
            DataSet ds;
            ds = objMatAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "I");

            ddlCostCenterAccGroup.DataSource = ds;
            ddlCostCenterAccGroup.DataTextField = "Module_Name";
            ddlCostCenterAccGroup.DataValueField = "Module_Id";
            ddlCostCenterAccGroup.DataBind();

        }
        catch (Exception ex)
        { _log.Error("PopulateDropDownList1", ex); }
    }

    private void ReadMaterialMasterRequests()
    {
        if (IsValidSearch() == true)
        {
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
            dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlCostCenterAccGrp.SelectedValue, "I", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
            {
                btnCreateNew.Enabled = false;
                btnChangeBulkRequest.Enabled = false;
                btnBlockRequest.Enabled = false;
            }

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[10].Visible = false;
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        grdSearch.Columns[11].Visible = true;
                    }
                    else
                    {
                        grdSearch.Columns[11].Visible = false;
                    }
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

            grdSearch.DataBind();

        }
        catch (Exception ex)
        { _log.Error("ReadMaterialMasterRequests", ex); }
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
                _log.Error("Exception" + ex.Message);
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
                    Label lblPendingFor = grv.FindControl("lblPendingFor") as Label;
                    Label lblSelectedCostCenterAccGrp = grv.FindControl("lblSelectedCostCenterAccGrp") as Label;
/// Carve_LC17&LC23_8400000406 
                    Label lblSelectedPlant = grv.FindControl("lblSelectedPlant") as Label;
                    Label lblCompany = grv.FindControl("lblCompany") as Label;
/// Carve_LC17&LC23_8400000406 
                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MaterialNo] = lblMasterCode.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.PendingFor] = lblPendingFor.Text;
                    Session[StaticKeys.MaterialProcessModuleId] = lblSelectedCostCenterAccGrp.Text;
/// Carve_LC17&LC23_8400000406 
                    Session[StaticKeys.SelectedddlCompany] = lblCompany.Text;
                    Session[StaticKeys.MaterialPlantId] = lblSelectedPlant.Text;
					/// Carve_LC17&LC23_8400000406 
                }
            }
        }
        catch (Exception ex)
        { _log.Error("GetSelectedPkID", ex); }
        return strPk;
    }

    #endregion
}