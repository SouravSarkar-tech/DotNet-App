using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Transactions;
using System.Text;
using System.Web.UI.HtmlControls;
using ExcelLibrary.SpreadSheet;
using log4net;

public partial class Shared_BOMRecipe_BOMRecipeMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    BOMAccess bomAccess = new BOMAccess();    

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


                    //if ((mode == "M" || mode == "N") && (lblMassRequestProcessId.Text == "0" || lblMassRequestProcessId.Text == ""))
                    if ((mode == "M" || mode == "N") )
                    {
                        if (Session[StaticKeys.MaterialProcessModuleId] != null)
                    {
                        if (Session[StaticKeys.MaterialProcessModuleId].ToString() != "")
                        {
                            moduleId = Session[StaticKeys.MaterialProcessModuleId].ToString();
                        }
                    }

                    //CQA testing
                    btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text, lblMasterHeaderId.Text);
                    //btnRejectTo.Visible = btnRejectTo.Visible = objAccess.IsInitiatorApprover(lblMasterHeaderId.Text, userDeptId, lblUserId.Text);
                    //CQA testing
                    ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());

                    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel           
                    if (userDeptId=="13")
                    {
                        lnkExcelDwld.Visible = true;
                        imgExcelDwld.Visible = true;
                    }
                    else
                    {
                        lnkExcelDwld.Visible = false;
                        imgExcelDwld.Visible = false;
                    }

                    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
                    if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        if (!objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text))
                        {
                            //Validating the BOM before integration Start
                            //btnSAPUpload.Visible = true;
                            //CQA testing
                            //BOM_8200050878 for new Module created 227,228,229
                            //manali chavan
                            if (Session[StaticKeys.SelectedModuleId].ToString() == "186" || Session[StaticKeys.SelectedModuleId].ToString() == "227" || Session[StaticKeys.SelectedModuleId].ToString() == "229")
                            {
                                if (!objAccess.IsSapValidationPending(lblMasterHeaderId.Text))
                                    btnValidate.Visible = true;
                                else
                                    btnSAPUpload.Visible = true;
                            }
                            else
                            {
                                btnSAPUpload.Visible = true;
                            }
                            //CQA testing
                            //Validating the BOM before integration End
                            btnSubmit.Visible = false;
                                                        
                            if (btnRejectTo.Visible)
                            {
                                //lblRejectionType.Text = "M";
                                //tdChkReject.Visible = true;
                                //tdDdlReject.Visible = false;
                                tdDdlReject.Visible = true;
                            }
                        }
                        else if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                        {
                            //BOM_8200050878 for new Module created 227,228,229
                            //manali chavan
                            if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")) 
                            {
                                if (!objAccess.IsSAPQAIntegrationPending(lblMasterHeaderId.Text))
                                    { btnQAUpload.Visible = true; }
                                    
                                else
                                {
                                    btnSubmit.Visible = true;
                                    btnSubmit.Text = "Approve";
                                }
                            }
                            else
                            {
                                btnSubmit.Visible = true;
                                btnSubmit.Text = "Approve";
                            }
                            if (btnRejectTo.Visible)
                            {
                                //lblRejectionType.Text = "M";
                                //tdChkReject.Visible = true;
                                //tdDdlReject.Visible = false;
                                tdDdlReject.Visible = true;
                            }
                        }
                        else
                        {
                            btnSAPUpload.Visible = false;
                            if (btnRejectTo.Visible)
                                //btnRejectTo.Visible = !bomAccess.IsSAPintegrationPending(lblMasterHeaderId.Text);
                                btnRejectTo.Visible = !objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text);

                            btnSubmit.Visible = true;
                            if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                btnSubmit.Text = "SAP Uploaded";
                            else
                                btnSubmit.Text = "Approve";
                        }
                    }
                    else if ((Session[StaticKeys.RequestStatus].ToString() == "P" || Session[StaticKeys.RequestStatus].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0")
                    {
                        btnSubmit.Visible = true;
                    }
                    //else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11" && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                    //{
                    //    if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "186"))
                    //    {
                    //        if (!objAccess.IsSAPQAIntegrationPending(lblMasterHeaderId.Text))
                    //            btnQAUpload.Visible = true;
                    //        else
                    //        {
                    //            btnSubmit.Visible = true;
                    //            btnSubmit.Text = "Approve";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        btnSubmit.Text = "Approve";
                    //        btnSubmit.Visible = true;
                    //    }
                    //}
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
                    ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
                }

                try { 
                //manali Chavan BOM_8200050878 Download bom/recipe/prd excel           
                if (userDeptId == "13")
                {
                    lnkExcelDwld.Visible = true;
                    imgExcelDwld.Visible = true;
                }
                else
                {
                    lnkExcelDwld.Visible = false;
                    imgExcelDwld.Visible = false;
                }
                }catch(Exception ex) { _log.Error("Page_Load1", ex); }
                    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel


                    //8400000359 S
                    FillDashBoard();
                    //8400000359 E
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
        //RecipeAccess objRecipeAccess = new RecipeAccess();
        //string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        //bool flg = false;
        //try
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        //if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
        //        if (objRecipeAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
        //        {
        //            //if (materialMasterAccess.SaveMaterialHeader(lblMasterHeaderId.Text, moduleId, lblUserId.Text, "M") > 0)
        //            //{
        //            flg = true;
        //            scope.Complete();
        //            Session[StaticKeys.ApprovalNote] = "";
        //            //}
        //        }
        //    }
        //    if (flg)
        //    {
        //        Response.Redirect("../BOMRecipe/BOMRecipeMaster.aspx");
        //    }
        //    else
        //    {
        //        lblMsg.Text = Messages.GetMessage(-1);
        //        pnlMsg.CssClass = "error";
        //        pnlMsg.Visible = true;
        //    }
        //}

        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //BOM_8200050878 for new Module created 227,228,229
        //manali chavan
        try
        {
       

        if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13") && (Session[StaticKeys.SelectedModuleId].ToString() == "185" || Session[StaticKeys.SelectedModuleId].ToString() == "186" || Session[StaticKeys.SelectedModuleId].ToString() == "227" || Session[StaticKeys.SelectedModuleId].ToString() == "229"))
        {
            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the startup script is already registered.
            if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
            {
                cs.RegisterStartupScript(GetType(), "key", "ShowInspCharCnfrmPopup();", true);
            }
        }
        else
        {
            //SPNAIRCR_SDT05122019 Old Commented by NR
            //SubmitRequest();
            //SPNAIRCR_SDT05122019 Old Commented by NR
              
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
            Response.Redirect("BOMRecipeMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

        if (SafeTypeHandling.ConvertStringToInt32(lblMassRequestProcessId.Text) > 0)
            { 
            Response.Redirect("BOMRecipeMassProcess.aspx", false);
            }
            else
            {  
            Response.Redirect("BOMRecipeMaster.aspx", false);
            }

        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }

    }

    protected void btnMDMCancel_Click(object sender, EventArgs e)
    {
        try
        {
        Response.Redirect("Reciepe.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnMDMCancel_Click", ex); }
    }

    protected void btnMDMApprove_Click(object sender, EventArgs e)
    {
        try
        {
        SubmitRequest();
        }
        catch (Exception ex)
        { _log.Error("btnMDMApprove_Click", ex); }
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
            _log.Error("RollbackRequest", ex);
            //throw ex;
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
                if (SafeTypeHandling.ConvertStringToInt32(lblMassRequestProcessId.Text) == 0)
                {
                    btnSubmit.Enabled = true;
                    if (!btnRejectTo.Visible && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R" || Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B") && (Session[StaticKeys.Mode].ToString() == "M" || Session[StaticKeys.Mode].ToString() == "N"))
                    {
                        Type cstype = this.GetType();

                        //Get a ClientScriptManager reference from the Page class.
                        ClientScriptManager cs = Page.ClientScript;

                        //Check to see if the startup script is already registered.
                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            //String cstext = "alert('Please click on Submit to send the request for processing.');";
                            //String cstext = "if(confirm('Proceed for submiting?')){RequestSubmitPage();};";
                            //String cstext = "if(confirm('Proceed for submiting? Click cancel to continue editing')){RequestSubmitPage();};";
                            String cstext = "if(!confirm('Click OK to further modify current request.\\n Click CANCEL to proceed for Submit / Mass Submit\\n \\n [MASS SUBMIT will club similar requests and send a single requests to the approver.]')){RequestSubmitPage();};";
                            //String cstext = "if(confirm('You are about to delete 5 rows. \nWARNING: Strawberry cakes won\'t be effected!)){RequestSubmitPage();};";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
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
                //throw;
                _log.Error("ReadSectionTabs", ex);
            }

        }
        catch (Exception ex)
        { _log.Error("ReadSectionTabs1", ex); }
    }

    /// <summary>
    /// SPNAIRCR_SDT05122019 add remarks parameter for remarks
    /// </summary>
    /// <param name="remarks"></param>
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
                Response.Redirect("../BOMRecipe/BOMRecipeMaster.aspx", false);
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

    //protected void btnDone_Click(object sender, EventArgs e)
    //{
    //    Session[StaticKeys.SAPUserName] = txtUserName.Text;
    //    Session[StaticKeys.SAPPassword] = txtPassword.Text;
               
    //    HtmlGenericControl pagebody = (HtmlGenericControl)Master.FindControl("pagebody");

    //    pagebody.Attributes.Add("onload", "ShowSAPQAUploadPopup()");
    //}


    //Excel download for BOM recipe start

    //#region Excel Download

    protected void lnkExcelDwld_Click(object sender, EventArgs e)
    {
        try
        {
        DownLoadDataInExcel();

        }
        catch (Exception ex)
        { _log.Error("lnkExcelDwld_Click", ex); }
    }

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
    protected void lnkExcelDwld_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        DownLoadDataInExcel();
        }
        catch (Exception ex)
        { _log.Error("lnkExcelDwld_Click", ex); }
    }

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
    protected void DownLoadDataInExcel()
    {
        try
        {
        string fileName = Session[StaticKeys.RequestNo].ToString();

        DataSet dstData = bomAccess.GetBOMRecipeDataByMasterheaderID(lblMasterHeaderId.Text, lblUserId.Text);
        string filePath = Server.MapPath("../../tempFile/" + fileName + ".xls");
        if (System.IO.File.Exists(filePath))
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            System.IO.File.Delete(filePath);
        }

        CreateWorkbook(filePath, dstData);
        DownloadFile(filePath, fileName);

        }
        catch (Exception ex)
        { _log.Error("DownLoadDataInExcel", ex); }
    }

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
    public void CreateWorkbook(String filePath, DataSet dataset)
    {
        try
        {

        if (dataset.Tables.Count == 0)
            throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");

        Workbook workbook = new Workbook();
        CellStyle style = new CellStyle();
        style.BackColor = System.Drawing.Color.Beige;
        //Manali chava : Added below code for applying worksheet name on dt_25052021
        if (Session[StaticKeys.SelectedModuleId].ToString() == "186" || Session[StaticKeys.SelectedModuleId].ToString() == "229" || Session[StaticKeys.SelectedModuleId].ToString() == "227")
        {
            dataset.Tables[0].TableName = "Bom Header";
            dataset.Tables[1].TableName = "Bom Detail";
            dataset.Tables[2].TableName = "Recipe Header";
            dataset.Tables[3].TableName = "Recipe Operation";
            dataset.Tables[4].TableName = "Secondary Resourcs";
            dataset.Tables[5].TableName = "Production Version";
            dataset.Tables[6].TableName = "Inspection Characteristics";

        }
        else if (Session[StaticKeys.SelectedModuleId].ToString() == "180" || Session[StaticKeys.SelectedModuleId].ToString() == "228")
        {
            
            dataset.Tables[0].TableName = "Bom Header";
            dataset.Tables[1].TableName = "Bom Detail";
            dataset.Tables[2].TableName = "Production Version";
            dataset.Tables[3].TableName = "Inspection Characteristics";

        }
        else if (Session[StaticKeys.SelectedModuleId].ToString() == "185")
        {
            dataset.Tables[0].TableName = "Recipe Header";
            dataset.Tables[1].TableName = "Recipe Operation";
            dataset.Tables[2].TableName = "Secondary Resources";
            dataset.Tables[3].TableName = "Inspection Characteristics";

        }
        else if (Session[StaticKeys.SelectedModuleId].ToString() == "187")
        {
            dataset.Tables[0].TableName = "Bom Header";
            dataset.Tables[1].TableName = "Bom Detail";
        }
        //Manali chava : Added below code for applying worksheet name on dt_25052021
        foreach (DataTable dt in dataset.Tables)
        {
            Worksheet worksheet = new Worksheet(dt.TableName);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                // Add column header
                worksheet.Cells[0, i].Style = style;
                worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);

                // Populate row data
                for (int j = 0; j < dt.Rows.Count; j++)
                    worksheet.Cells[j + 1, i] = new Cell(SafeTypeHandling.ConvertToString(dt.Rows[j][i]));

            }
            workbook.Worksheets.Add(worksheet);
        }

        workbook.Save(filePath);

        }
        catch (Exception ex)
        { _log.Error("CreateWorkbook", ex); }
    }

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
    private void DownloadFile(string filePath, string fileName)
    {
        try
        {

        GC.Collect();
        GC.WaitForPendingFinalizers();

        fileName = fileName + ".xls";
        //string filePath = Server.MapPath("../../tempFile/" + fileName);

        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

        Response.TransmitFile(filePath);

        Response.End();

        }
        catch (Exception ex)
        { _log.Error("DownloadFile", ex); }
    }

    //manali Chavan BOM_8200050878 Download bom/recipe/prd excel
    //private void DeleteFile(string fileName)
    //{
    //    string filePath = Server.MapPath("../../tempFile/");
    //    string type = "File";
    //    if (type == "File")
    //    {
    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();

    //        System.IO.File.Delete(filePath);
    //    }
    //    else if (type == "Directory")
    //    {
    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();

    //        System.IO.Directory.Delete(filePath, true);
    //    }
    //}

    //#endregion

    //Excel download for BOM recipe end
    protected void btnQAUpload_Click(object sender, EventArgs e)
    {
        //DropDownList dd = (DropDownList)ContentPlaceHolder1.FindControl("ddlRStatus");
        BOMAccess ObjBOMAccess = new BOMAccess();
       
        DataSet ds2 = new DataSet();
        
       
        var status = "";
        var prdlockstatus = "";
        try
        {
        ds2 = ObjBOMAccess.GetBOMPRODSTATUSDetail(Convert.ToInt32(lblMasterHeaderId.Text));
        status = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
        prdlockstatus = ds2.Tables[0].Rows[0].ItemArray[1].ToString();
        
        
        ClientScriptManager cs = Page.ClientScript;

        // Check to see if the startup script is already registered.
        if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
        {
            if (status != "1" && prdlockstatus.Trim() == "")
            {
                cs.RegisterStartupScript(GetType(), "key", "ShowSAPQAUploadPopup();", true);
            }
            else
            {
                //BOM_8200050878 for new Module created 227,228,229
                //manali chavan
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                {
                    cs.RegisterStartupScript(GetType(), "key", "alert('System not released recipe/prod.Version since PDL person has not set either Recipe header status as Released Or/And Production version as Not Locked');", true);
                }
                else
                {
                    cs.RegisterStartupScript(GetType(), "key", "alert('System not released recipe/prod.Version since QA person has not set either Recipe header status as Released Or/And Production version as Not Locked');", true);
                }
            }
        }

        }
        catch (Exception ex)
        { _log.Error("btnQAUpload_Click", ex); }
    }
}
