using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Configuration;
using log4net;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;

public partial class Shared_ProfitCenter_ProfitCenterMaster : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                Page.ClientTarget = "uplevel";
        }
        catch (Exception ex)
        { _log.Error("Page_PreInit", ex); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoStore();

            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            if ((Session[StaticKeys.LoggedIn_User_Id] != null) && (Session[StaticKeys.MasterHeaderId] != null))
            {

                lblRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
                lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();
                lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
                lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();
                /// Carve_LC17&LC23_8400000406
				lblSelectedPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();
 
                try
                {
                    HelperAccess helperAccess = new HelperAccess();
                    helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id", "");
                    if (Session[StaticKeys.SelectedddlCompany].ToString() != null && Session[StaticKeys.SelectedddlCompany].ToString() != "")
                    { ddlCompanyCode.SelectedValue = Session[StaticKeys.SelectedddlCompany].ToString(); }
                    else
                    {
                       
                    }
                }
                catch (Exception ex) { }
				 /// Carve_LC17&LC23_8400000406
                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();

                if (!IsPostBack)
                {
                    ZcapHsnMasterAccess objAccess = new ZcapHsnMasterAccess();
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);
                    string mode = Session[StaticKeys.Mode].ToString();
                    if (mode == "M" || mode == "N")
                    {
                        btnSubmit.Visible = true;
                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text);

                    }
                    else
                    {
                        btnRejectTo.Visible = false;
                    }

                    //8400000359 S
                    FillDashBoard();
                    //8400000359 E

                }
                ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
            }
            else
            {
                Response.Redirect("../../login.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// 8400000359 
    /// </summary>
    private void FillDashBoard()
    {
        try
        {
            ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
            DataTable Dt1;

            Dt1 = zcapHsnaccess.GetRemarksByUser(Convert.ToInt32(lblMasterHeaderId.Text), Convert.ToString("0"));
            if (Dt1.Rows.Count > 0)
            {
                rptCommon.DataSource = Dt1;
                rptCommon.DataBind();
            }
            else
            {
                rptCommon.DataSource = null;
                rptCommon.DataBind();
            }
            //    rptCommon.DataSource = Dt1;
            //rptCommon.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDashBoard", ex); }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
            {
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                {
                    cs.RegisterStartupScript(GetType(), "key", "ShowApprovePopup();", true);
                }
            }
            else
            {
                SubmitRequest();
            }

        }
        catch (Exception ex)
        { _log.Error("btnSubmit_Click", ex); }
    }

    protected void btnApproveRemarks_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtApproveNote.Text));
        }
        catch (Exception ex)
        { _log.Error("btnApproveRemarks_Click", ex); }
    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
            if (RollbackRequest())
            {
                Response.Redirect("../ProfitCenter/ProfitCenterMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../ProfitCenter/ProfitCenterMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    private bool RollbackRequest()
    {
        bool flg = false;
        try
        {
            ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
            if (ObjPCAccess.RollbackRequestPC(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), ddlRejectTo.SelectedValue, Utility.RemoveSpecialChar(txtRejectNote.Text), lblUserId.Text) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-2);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("RollbackRequest", ex);
        }
        return flg;
    }

    public void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        VendorMasterAccess VendorMasterAccess = new VendorMasterAccess();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        StringBuilder strBuilder = new StringBuilder();
        string currentSectionId = ((Label)ContentPlaceHolder1.FindControl("lblSectionId")).Text;
        string sectionId = string.Empty;
        string ActionType = Session[StaticKeys.ActionType].ToString();
        bool flg;
        if (ActionType == "C")
            flg = false;
        else
            flg = true;

        bool flg2 = true;
        int sectionStatus = 0;
        try
        {
            dstData = userAccess.ReadSectionTabs(userId, departmentId, lblMasterHeaderId.Text);

            strBuilder.Append("<table cellspacing='0' width='100%'>");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                strBuilder.Append("<tr><td class='navigationBox'>");
                sectionId = row["Section_Id"].ToString();
                sectionStatus = VendorMasterAccess.CheckSectionStatus(sectionId, lblMasterHeaderId.Text);

                if (sectionStatus <= 0 && ActionType != "C")
                {
                    flg = false;
                }
                else if (sectionStatus > 0 && ActionType == "C")
                {
                    flg = true;
                }

                if (row["Section_Id"].ToString() != currentSectionId)
                {
                    if (sectionStatus > 0)
                    {
                        strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                    }
                    else
                    {
                        strBuilder.Append("<a href='" + row["Page_URL"] + "' >" + row["Section_Name"] + "</a>");
                    }
                }
                else if (row["Section_Id"].ToString() == currentSectionId)
                {
                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='Active'>" + row["Section_Name"] + "</a>");
                }
                strBuilder.Append("</td></tr>");
            }

            strBuilder.Append(" </table>");
            litTab.Text = strBuilder.ToString();

            trSideMenuTab.Width = "0px";

            if ((flg) && (flg2))
            {
                btnSubmit.Enabled = true;
                if (!btnRejectTo.Visible && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B") && (Session[StaticKeys.Mode].ToString() == "M" || Session[StaticKeys.Mode].ToString() == "N"))
                {
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Please click on Submit to send the request for processing.');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ReadSectionTabs", ex);
        }

    }

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {
            ddlRejectTo.DataSource = materialMasterAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }

    private void SubmitRequest(string remarks = "")
    {
        ProfitCenterMasterAccess ObjPCAccess = new ProfitCenterMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (ObjPCAccess.ApproveRequestPC(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                {
                    flg = true;
                    scope.Complete();
                }
            }
            if (flg)
            {
                if (!btnRejectTo.Visible)
                    Response.Write("<script>alert('Profit Center Master Request No: " + lblRequestNo.Text + " created.');window.location.href ='ProfitCenterMaster.aspx'; </script>");
                else
                    Response.Redirect("../ProfitCenter/ProfitCenterMaster.aspx", false);
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
            _log.Error("SubmitRequest", ex);
        }
    }
}
