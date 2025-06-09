using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using System.Transactions;
using System.Web.UI.HtmlControls;
using log4net;
public partial class Shared_SoftwareApproval_SoftwareApprovalMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly. 
            // All webpages displaying an ASP.NET menu control must inherit this class. 
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
                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                lnkRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
                lnkRequestNo.Attributes.Add("OnClientClick", "OpenRequestHistory('" + Session[StaticKeys.MasterHeaderId].ToString() + "','0');");

                lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
                lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();

                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);

                    string mode = Session[StaticKeys.Mode].ToString();
                    if ((mode == "M" || mode == "N"))
                    {
                        btnReject.Visible = btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text, lblMasterHeaderId.Text);
                        //btnReject.Visible = btnRejectTo.Visible = objAccess.IsInitiatorApprover(lblMasterHeaderId.Text, userDeptId, lblUserId.Text);
                        if (btnRejectTo.Visible)
                        {
                            btnSubmit.Text = "Approve";
                        }
                        btnSubmit.Visible = btnRejectTo.Visible;
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "29" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "30" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "31")
                        {
                            trMisc.Visible = true;
                            reqddlMISC.Enabled = true;
                        }
                    }
                    ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
                }
            }
            else
            {
                Response.Redirect("../login.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SWApprovalAccess objSWAppAccess = new SWApprovalAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (objSWAppAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                {
                    flg = true;
                    scope.Complete();
                    Session[StaticKeys.ApprovalNote] = "";
                }
            }
            if (flg)
            {
                Response.Redirect("../SoftwareApproval/SoftwareApprovalMaster.aspx", false);
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
            //throw ex;
            _log.Error("btnSubmit_Click", ex);
        }

    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
            if (RollbackRequest())
            {
                Response.Redirect("SoftwareApprovalMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnRejectA_Click(object sender, EventArgs e)
    {
        try
        {
            if (FinalRejectRequest())
            {
                Response.Redirect("SoftwareApprovalMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRejectA_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("SoftwareApprovalMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    protected void btnApproveComments_Click(object sender, EventArgs e)
    {
        SWApprovalAccess swApprovalAccess = new SWApprovalAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        Utility objUtil = new Utility();
        bool flg = false;

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (swApprovalAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, objUtil.ReplaceEscapeSequenceChar(txtApprovalComments.Text), ddlMISC.SelectedValue.ToString()) > 0)
                {
                    flg = true;
                    scope.Complete();
                    Session[StaticKeys.ApprovalNote] = "";
                }
            }
            if (flg)
            {
                Response.Redirect("../SoftwareApproval/SoftwareApprovalMaster.aspx", false);
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
            //throw ex;
            _log.Error("btnApproveComments_Click", ex);
        }

    }

    private bool RollbackRequest()
    {
        SWApprovalAccess objSWAccess = new SWApprovalAccess();
        Utility objUtil = new Utility();
        bool flg = false;

        string strReject = "";

        try
        {
            strReject = ddlRejectTo.SelectedValue;

            if (objSWAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectNote.Text), lblUserId.Text) > 0)
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
            //throw ex;
            _log.Error("RollbackRequest", ex);
        }
        return flg;
    }

    private bool FinalRejectRequest()
    {
        SWApprovalAccess objAccess = new SWApprovalAccess();
        Utility objUtil = new Utility();
        bool flg = false;

        string strReject = "";

        try
        {
            strReject = ddlRejectA.SelectedValue;

            if (objAccess.FinalRejectRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectRemark.Text), lblUserId.Text) > 0)
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
            //throw ex;
            _log.Error("FinalRejectRequest", ex);
        }
        return flg;
    }

    public bool IsValidURL(string url)
    {
        string str = "";
        try
        {
            if ((url != null) && (url != string.Empty))
            {
                string[] strArray = url.Substring(0, url.ToString().Length - 1).Split(new char[] { '/' });
                str = strArray[strArray.Length - 1];
            }
        }
        catch (Exception ex)
        { _log.Error("IsValidURL", ex); }
        return (str != string.Empty);
    }

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {
            DataSet ds = materialMasterAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);

            ddlRejectTo.DataSource = ds;
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();

            ddlRejectA.DataSource = ds;
            ddlRejectA.DataTextField = "LevelName";
            ddlRejectA.DataValueField = "Sequence";
            ddlRejectA.DataBind();

            ddlRejectA.SelectedIndex = 1;
            ddlRejectA.Enabled = false;
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }

    private void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        StringBuilder strBuilder = new StringBuilder();
        try
        {
            //string currentPageSeq = Request.QueryString["pgseq"].ToString();
            string currentSectionId = ((Label)ContentPlaceHolder1.FindControl("lblSectionId")).Text;
            //string currentSectionId = Request.QueryString["sid"].ToString();
            string sectionId = string.Empty;
            string ActionType = Session[StaticKeys.ActionType].ToString();

            string selectedModuleID = Session[StaticKeys.SelectedModuleId].ToString();
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

                //dstData = userAccess.ReadSectionTabs(userId, departmentId, moduleId);

                strBuilder.Append("<table cellspacing='0' width='100%'>");
                foreach (DataRow row in dstData.Tables[0].Rows)
                {
                    strBuilder.Append("<tr><td class='navigationBox'>");
                    sectionId = row["Section_Id"].ToString();
                    sectionStatus = materialMasterAccess.CheckSectionStatus(sectionId, lblMasterHeaderId.Text);

                    if (sectionStatus <= 0 && ActionType != "C")
                    {
                        flg = false;
                    }
                    else if (sectionStatus > 0 && ActionType == "C")
                    {
                        flg = true;
                    }

                    //else if (sectionStatus > 0 && ActionType != "C" && (Convert.ToInt32(selectedModuleID) == 162))
                    //{
                    //    flg = false;
                    //}

                    if (row["Section_Id"].ToString() != currentSectionId)
                    {
                        if (sectionStatus > 0)
                        {
                            if (Convert.ToInt32(selectedModuleID) == 162 || Convert.ToInt32(selectedModuleID) == 164)
                            {
                                if (row["Section_Id"].ToString() == "3" || row["Section_Id"].ToString() == "12" || row["Section_Id"].ToString() == "8" || row["Section_Id"].ToString() == "13" || row["Section_Id"].ToString() == "14" || row["Section_Id"].ToString() == "16" || row["Section_Id"].ToString() == "51")
                                {
                                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='ROHStatus'>" + row["Section_Name"] + "</a>");
                                }
                                else
                                {
                                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                                }
                            }
                            else if (Convert.ToInt32(selectedModuleID) == 139 || Convert.ToInt32(selectedModuleID) == 144 || Convert.ToInt32(selectedModuleID) == 145 || Convert.ToInt32(selectedModuleID) == 171)
                            {
                                if (row["Section_Id"].ToString() == "3" || row["Section_Id"].ToString() == "12" || row["Section_Id"].ToString() == "8" || row["Section_Id"].ToString() == "13" || row["Section_Id"].ToString() == "14" || row["Section_Id"].ToString() == "16" || row["Section_Id"].ToString() == "10" || row["Section_Id"].ToString() == "1" || row["Section_Id"].ToString() == "21" || row["Section_Id"].ToString() == "7")
                                {
                                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='ROHStatus'>" + row["Section_Name"] + "</a>");
                                }
                                else
                                {
                                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                                }
                            }
                            else
                            {
                                strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                            }

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

                if (ActionType == "C")
                {
                    strBuilder.Append("<tr><td class='NoteWhiteBackGround'><b><u>Note :</u></b>");
                    strBuilder.Append("<br />1. Enter '#' as Old/ New Value to denote Blank.");
                    strBuilder.Append("<br />2. Save only those sections that needs to be changed.");
                    strBuilder.Append("</td></tr>");
                }
                strBuilder.Append(" </table>");
                litTab.Text = strBuilder.ToString();

                trSideMenuTab.Width = "0px";

                if ((flg) && (flg2))
                {
                    btnSubmit.Enabled = true;
                    if (!btnRejectTo.Visible && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R" || Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B") && (Session[StaticKeys.Mode].ToString() == "M" || Session[StaticKeys.Mode].ToString() == "N"))
                    {
                        //Type cstype = this.GetType();

                        //Get a ClientScriptManager reference from the Page class.
                        //ClientScriptManager cs = Page.ClientScript;

                        //Check to see if the startup script is already registered.
                        //if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        //{
                        //    //String cstext = "alert('Please click on Submit to send the request for processing.');";
                        //    //String cstext = "if(confirm('Proceed for submiting?')){RequestSubmitPage();};";
                        //    //String cstext = "if(confirm('Proceed for submiting? Click cancel to continue editing')){RequestSubmitPage();};";
                        //    String cstext = "if(!confirm('Click OK to further modify current request.\\n Click CANCEL to proceed for Submit / Mass Submit\\n \\n [MASS SUBMIT will club similar requests and send a single requests to the approver.]')){RequestSubmitPage();};";
                        //    //String cstext = "if(confirm('You are about to delete 5 rows. \nWARNING: Strawberry cakes won\'t be effected!)){RequestSubmitPage();};";
                        //    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        //}
                    }

                }
                else
                {
                    btnSubmit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //throw;
                _log.Error("ReadSectionTabs1", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("ReadSectionTabs", ex); }
    }
}
