using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using log4net;
public partial class Transaction_ZcapHsnMaster_ZcapHsnMaster : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Page Events

    /// <summary>
    /// Bind data on page load event
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
    /// Show and hide base on user profile
    /// </summary>
    private void ShowHideBtn()
    {
        try
        {
            if (Convert.ToString(Session[StaticKeys.LoggedIn_User_Profile_Id]) == "2")
            {
                btnCreateNew.Attributes.Add("enabled", "enabled");
            }
            else
            {
                btnCreateNew.Attributes.Add("disabled", "disabled");
            }
        }
        catch (Exception ex) { _log.Error("ShowHideBtn", ex); }
    }

    /// <summary>
    /// Create new HSN/ZCAP master request
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = zcapHsnaccess.SaveHSNZCAPHeader("0", ddlPlant.SelectedValue, ddlCreateModule.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlCreateModule.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlCreateModule.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = zcapHsnaccess.mRequestNo;
                Session[StaticKeys.MaterialPlantName] = ddlPlant.SelectedItem.Text;
                Session[StaticKeys.ReqStatus] = "I"; 
                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("ZcapHsnCreate.aspx");
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnNext_Click", ex);
        }
    }

    /// <summary>
    /// View existing HSN/ZCAP master request
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
            Response.Redirect("ZcapHsnCreate.aspx");
        }
        catch (Exception ex)
        {
            _log.Error("btnView_Click", ex);
        }
    }

    /// <summary>
    /// Modified existing HSN/ZCAP master request
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
            Response.Redirect("ZcapHsnCreate.aspx");
        }
        catch (Exception ex)
        {
            _log.Error("btnModify_Click", ex);
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
    /// Search HSN/ZCAP master request using filter criteria
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

    DataSet dstData = new DataSet();

    /// <summary>
    /// Create and set paging index
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

    /// <summary>
    /// bind filter data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HtmlAnchor lnkFrwrdNote = e.Row.FindControl("lnkFrwrdNote") as HtmlAnchor;
        //    Label lblForward = e.Row.FindControl("lblForward") as Label;
        //    if (lblForward.Text == "")
        //    {
        //    }
        //    Label lblPrimaryID = e.Row.FindControl("lblPrimaryID") as Label;
        //    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        //    Utility ObjUtil = new Utility();
        //    DataSet ds = new DataSet();
        //    string GLCode = dstData.Tables[0].Rows[0]["Customer_Code"].ToString();
        //    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "37")
        //    {
        //        ds = objMatAccess.FindMDMApproved();
        //        foreach (TableCell cell in e.Row.Cells)
        //        {

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    string Master_Header_Id = ds.Tables[0].Rows[i]["Master_Header_Id"].ToString();
        //                    if (lblPrimaryID.Text == Master_Header_Id)
        //                        cell.BackColor = System.Drawing.Color.Red;
        //                }
        //            }
        //        }
        //    }
        //}

    }

    #endregion

    #region  Search

    /// <summary>
    /// get and bind dropdown data to ddl control
    /// </summary>
    /// <param name="userProfileId"></param>
    public void PopulateDropDownList(string userProfileId)
    {
        try
        {
            ZcapHsnMasterAccess objZcapHsnAccess = new ZcapHsnMasterAccess();
            DataSet ds;
            ds = objZcapHsnAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "Z");

            ddlModuleName.DataSource = ds;
            ddlModuleName.DataTextField = "Module_Name";
            ddlModuleName.DataValueField = "Module_Id";
            ddlModuleName.DataBind();

            ddlCreateModule.DataSource = ds;
            ddlCreateModule.DataTextField = "Module_Name";
            ddlCreateModule.DataValueField = "Module_Id";
            ddlCreateModule.DataBind();

        }
        catch (Exception ex) { _log.Error("PopulateDropDownList", ex); }
        HelperAccess helperAccess = new HelperAccess();
        try
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetSBUList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        }
        catch (Exception ex) { _log.Error("PopulateDropDownList1", ex); }
    }

    /// <summary>
    /// Get HSN/ZCAP records details 
    /// </summary>
    private void ReadMasterRequests()
    {
        ZcapHsnMasterAccess objZcapHsnAccess = new ZcapHsnMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
            dstData = objZcapHsnAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleName.SelectedValue, "Z", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
           
            if(dstData != null  && dstData.Tables != null)
            { 
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = true;
                    //grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = false; 
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = true;
                    //grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = true; 
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    //grdSearch.Columns[10].Visible = true;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = false;
                    //grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = true; 
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    //grdSearch.Columns[10].Visible = true;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = false;
                    //grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false; 
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = false;
                    //grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false; 
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    //grdSearch.Columns[10].Visible = false;
                    //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    //{
                    //    grdSearch.Columns[11].Visible = true;
                    //}
                    //else
                    //{
                    //    grdSearch.Columns[11].Visible = false;
                    //}
                    //grdSearch.Columns[12].Visible = false;
                    //grdSearch.Columns[13].Visible = false;
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
    /// get selected record details
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

                    Label lblStatus = grv.FindControl("lblStatus") as Label;
                    Label lblFITD = grv.FindControl("lblFITD") as Label;
                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.PendingFor] = lblPendingFor.Text;
                    Session[StaticKeys.MaterialPlantName] = lblSelectedPlant.Text;

                    Session[StaticKeys.ReqStatus] = lblStatus.Text;
 
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