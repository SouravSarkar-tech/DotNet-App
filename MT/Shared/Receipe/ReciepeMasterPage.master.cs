using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Transactions;
using log4net;
public partial class Shared_Receipe_ReciepeMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try {
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
        try {
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
            //lblRequestNo.Text = Session[StaticKeys.RequestNo].ToString();

            lnkRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
            lnkRequestNo.Attributes.Add("OnClientClick", "OpenRequestHistory('" + Session[StaticKeys.MasterHeaderId].ToString() + "','0');");

            //lblMaterialNo.Text = Session[StaticKeys.MaterialNo].ToString();
            lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

            lblPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();
            
            lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
            lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
            lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

            string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
            if (!IsPostBack)
            {
                //if (Session[StaticKeys.LoggedIn_User_Profile] != null && Session[StaticKeys.SelectedModuleId] != null)
                //{
                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                lblMassRequestProcessId.Text = Session[StaticKeys.MassRequestProcessId].ToString();
                ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);

                string mode = Session[StaticKeys.Mode].ToString();


                if ((mode == "M" || mode == "N") && (lblMassRequestProcessId.Text == "0" || lblMassRequestProcessId.Text == ""))
                {
                    if (Session[StaticKeys.MaterialProcessModuleId] != null)
                    {
                        if (Session[StaticKeys.MaterialProcessModuleId].ToString() != "")
                        {
                            moduleId = Session[StaticKeys.MaterialProcessModuleId].ToString();
                        }
                    }

                    btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text, lblMasterHeaderId.Text);
                    //ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());

                    //if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R" || Session[StaticKeys.ActionType].ToString() == "E") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        if (!objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text) && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                        {
                            btnSAPUpload.Visible = true;
                            btnSubmit.Visible = false;

                            //if (btnRejectTo.Visible && (moduleId == "162" || moduleId == "164" || moduleId == "139" || moduleId == "145" || moduleId == "144" || moduleId == "171"))
                            if (btnRejectTo.Visible)
                            {
                                lblRejectionType.Text = "M";
                                tdChkReject.Visible = true;
                                tdDdlReject.Visible = false;
                            }
                        }
                        else
                        {
                            btnSAPUpload.Visible = false;
                            if (btnRejectTo.Visible)
                                btnRejectTo.Visible = !objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text);

                            btnSubmit.Visible = true;
                            if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                btnSubmit.Text = "SAP Uploaded";
                            else
                                btnSubmit.Text = "Approve";
                        }
                    }
                    else
                    {
                        if (btnRejectTo.Visible)
                        {
                            btnSubmit.Text = "Approve";
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                    btnSubmit.Text = "SAP Uploaded";
                            }
                        }

                        btnSubmit.Visible = btnRejectTo.Visible;
                    }
                }
                else
                {
                    btnRejectTo.Visible = false;
                    //ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
                }
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
        try { 
        //SPNAIRCR_SDT05122019 Added by NR
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
        {
            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the startup script is already registered.
            if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
            {
                cs.RegisterStartupScript(GetType(), "key", "ShowApprovePopup();", true);
            }
        }//SPNAIRCR_SDT05122019 Added by NR
        else
        {
            SubmitRequest();
        }
        }
        catch (Exception ex)
        { _log.Error("btnSubmit_Click", ex); }
    }

    /// <summary>
    /// SPNAIRCR_SDT05122019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApproveRemarks_Click(object sender, EventArgs e)
    {
        try {
        SubmitRequest(Utility.RemoveSpecialChar(txtApproveNote.Text));
        }
        catch (Exception ex)
        { _log.Error("btnApproveRemarks_Click", ex); }
    }

    private void SubmitRequest(string remarks = "")
    {
        RecipeAccess objRecipeAccess = new RecipeAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
           

            using (TransactionScope scope = new TransactionScope())
            {
                //if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                if (objRecipeAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                {
                    //if (materialMasterAccess.SaveMaterialHeader(lblMasterHeaderId.Text, moduleId, lblUserId.Text, "M") > 0)
                    //{
                    flg = true;
                    scope.Complete();
                    Session[StaticKeys.ApprovalNote] = "";
                    //}
                }
            }
            if (flg)
            {
                Response.Redirect("../Receipe/ReceipeMaster.aspx", false);
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
            _log.Error("SubmitRequest", ex);
        }

    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try { 
        if (RollbackRequest())
        {
            Response.Redirect("ReceipeMaster.aspx", false);
        }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try { 
        Response.Redirect("ReceipeMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    private bool RollbackRequest()
    {
        RecipeAccess objAccess = new RecipeAccess();
        Utility objUtil = new Utility();
        bool flg = false;

        string strReject = "";

        try
        {
            if (lblRejectionType.Text == "M")
            {
                flg = true;
                for (int i = 0; i < ChkRejectTo.Items.Count; i++)
                {
                    if (ChkRejectTo.Items[i].Selected)
                    {
                        if (strReject == "")
                            strReject = ChkRejectTo.Items[i].Value;
                        else
                            strReject += "," + ChkRejectTo.Items[i].Value;


                    }
                }
            }
            else
            {
                strReject = ddlRejectTo.SelectedValue;
            }

            if (objAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectNote.Text), lblUserId.Text) > 0)
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

    public bool IsValidURL(string url)
    {
        string str = "";
        try { 
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

            ChkRejectTo.DataSource = ds;
            ChkRejectTo.DataTextField = "LevelName";
            ChkRejectTo.DataValueField = "Sequence";
            ChkRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }
}
