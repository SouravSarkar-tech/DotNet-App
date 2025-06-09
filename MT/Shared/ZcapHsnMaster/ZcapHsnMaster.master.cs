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

public partial class Shared_ZcapHsnMaster_ZcapHsnMaster : System.Web.UI.MasterPage
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
                lblSelectedPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();

                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();

                if (!IsPostBack)
                {
                    //MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    ZcapHsnMasterAccess objAccess = new ZcapHsnMasterAccess();
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);
                    string mode = Session[StaticKeys.Mode].ToString();
                    if (mode == "M" || mode == "N")
                    {
                        btnSubmit.Visible = true;
                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text);
                        //if (Session[StaticKeys.PendingFor].ToString() == "2" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && (moduleId == "234" || moduleId == "235"))
                        //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && Session[StaticKeys.FITD].ToString() == "0" && (moduleId == "234" || moduleId == "235"))
                        //{ 
                        Session[StaticKeys.ModuleHSN] = "";
                        Session[StaticKeys.ModuleZHG] = "";
                        Session[StaticKeys.ModuleHSN] = ConfigurationManager.AppSettings["ModuleHSN"].ToString();
                        Session[StaticKeys.ModuleZHG] = ConfigurationManager.AppSettings["ModuleZHG"].ToString();


                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && (moduleId == Convert.ToString(Session[StaticKeys.ModuleHSN]) || moduleId == Convert.ToString(Session[StaticKeys.ModuleZHG])))
                        {
                            btnForward.Visible = true;
                            DataSet dstData = new DataSet();
                            dstData = objAccess.ValidateFITD(lblMasterHeaderId.Text);
                            if (dstData.Tables[0].Rows.Count > 0)
                            {
                                btnForward.Visible = false;
                            }

                        }
                    }
                    else
                    {
                        btnRejectTo.Visible = false;
                    }
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
    protected void btnForwardTo_Click(object sender, EventArgs e)
    {
        try
        {
            ForwardToRequest(Utility.RemoveSpecialChar(txtQuery.Text));
        }
        catch (Exception ex)
        { _log.Error("btnForwardTo_Click", ex); }
    }

    private void ForwardToRequest(string remarks = "")
    {
        ZcapHsnMasterAccess ObjHSNAccess = new ZcapHsnMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (ObjHSNAccess.ForwardToRequestHSNZACP(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                {
                    flg = true;
                    scope.Complete();
                }
            }
            if (flg)
            {
                //if (!btnRejectTo.Visible)
                Response.Write("<script>alert('ZCAP/ZPEX+HSN/GST% Master Request No: " + lblRequestNo.Text + " forwarded.');window.location.href ='ZcapHsnMaster.aspx'; </script>");
                //else
                //    Response.Redirect("../ZcapHsnMaster/ZcapHsnMaster.aspx", false);
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
            _log.Error("ForwardToRequest", ex);
        }
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

    protected void btnAcntSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtAppComments.Text));
        }
        catch (Exception ex)
        { _log.Error("btnAcntSubmit_Click", ex); }
    }
    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
            if (RollbackRequest())
            {
                Response.Redirect("../ZcapHsnMaster/ZcapHsnMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../ZcapHsnMaster/ZcapHsnMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    private bool RollbackRequest()
    {
        bool flg = false;
        try
        {
            ZcapHsnMasterAccess ObjHSNAccess = new ZcapHsnMasterAccess();
            if (ObjHSNAccess.RollbackRequestHSNZACP(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), ddlRejectTo.SelectedValue, Utility.RemoveSpecialChar(txtRejectNote.Text), lblUserId.Text) > 0)
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
        ZcapHsnMasterAccess ObjHSNAccess = new ZcapHsnMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (ObjHSNAccess.ApproveRequestHSNZACP(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                    {
                        flg = true;
                        scope.Complete();
                        scope.Dispose();
                        _log.Info("Transaction Exception Occured ZcapHsn Successful");
                    }
                }
                catch (TransactionException ex)
                {
                    scope.Dispose();
                    _log.Info("Transaction Exception Occured ZcapHsn Error"); 
                }
            }
            if (flg)
            {
                if (!btnRejectTo.Visible)
                    Response.Write("<script>alert('ZCAP/ZPEX+HSN/GST% Master Request No: " + lblRequestNo.Text + " created.');window.location.href ='ZcapHsnMaster.aspx'; </script>");
                else
                    Response.Redirect("../ZcapHsnMaster/ZcapHsnMaster.aspx", false);
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
