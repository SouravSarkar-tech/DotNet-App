using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.Security;
using System.Transactions;
using log4net;
public partial class Shared_Vendor_VendorMasterPage : System.Web.UI.MasterPage
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

            //if (IsValidURL(SafeTypeHandling.ConvertToString(HttpContext.Current.Request.UrlReferrer)) == false)
            //{
            //    Response.Redirect("../login.aspx");
            //}
            //else
            //{
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            if ((Session[StaticKeys.LoggedIn_User_Id] != null) && (Session[StaticKeys.MasterHeaderId] != null))
            {

                lblRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
                lblMaterialNo.Text = Session[StaticKeys.MaterialNo].ToString();
                lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

                lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
                lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                if (!IsPostBack)
                {
                    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);
                    string mode = Session[StaticKeys.Mode].ToString();
                    if (mode == "M" || mode == "N")
                    {
                        if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            btnSAPUpload.Visible = true;
                            //Vendor Workflow Modification changes Start
                            if (Session[StaticKeys.PendingFor].ToString() == "3" || Session[StaticKeys.PendingFor].ToString() == "5")
                                btnForward.Visible = true;
                            //Vendor Workflow Modification changes End
                        }
                        else
                        {
                            //Vendor Workflow Modification changes Start
                            if (Session[StaticKeys.ActionType].ToString() == "U" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                btnForward.Visible = true;
                                btnSubmit.Visible = true;
                            }
                            //PFun_DT06032020 Start
                            else if (Session[StaticKeys.ActionType].ToString() == "C" && (
                                  Session[StaticKeys.SelectedModuleId].ToString() == "224" ||
                                  Session[StaticKeys.SelectedModuleId].ToString() == "226")
                                && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                if (!objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text)
                                    && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                {
                                    btnSAPUpload.Visible = true;
                                    btnForward.Visible = false;
                                    btnSubmit.Visible = false;
                                    btnRejectTo.Visible = true;
                                }
                                else
                                {
                                    btnSAPUpload.Visible = false;
                                    btnForward.Visible = false;
                                    btnSubmit.Visible = true;
                                    btnRejectTo.Visible = false;

                                }
                            }
                            //PFun_DT06032020 Start
                            //Vendor Workflow Modification changes End
                            else
                            {
                                btnSubmit.Visible = true;
                            }
                        }

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
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "37")
                {
                    btnSubmit.Enabled = true;
                }
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
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" && Session[StaticKeys.PendingFor].ToString() == "4")
            {
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                {
                    cs.RegisterStartupScript(GetType(), "key", "ShowAcntPopup();", true);
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
    //start Vendor Workflow
    protected void btnAcntSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtAppComments.Text));
        }
        catch (Exception ex)
        { _log.Error("btnAcntSubmit_Click", ex); }
    }
    // End Vendor Workflow

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
                Response.Redirect("../Vendor/VendorMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Vendor/VendorMaster.aspx", false);
    }

    //Vendor Workflow Modification changes Start
    protected void btnForwardTo_Click(object sender, EventArgs e)
    {
        try
        {
            if (ForwardRequest())
            {
                Response.Redirect("../Vendor/VendorMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnForwardTo_Click", ex); }
    }


    private bool ForwardRequest()
    {
        bool flg = false;
        try
        {
            MaterialMasterAccess objAccess = new MaterialMasterAccess();

            if (objAccess.ForwardRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Utility.RemoveSpecialChar(txtQuery.Text), lblUserId.Text) > 0)
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
            _log.Error("ForwardRequest", ex);
        }
        return flg;
    }

    //Vendor Workflow Modification changes End

    private bool RollbackRequest()
    {
        bool flg = false;
        try
        {
            MaterialMasterAccess objAccess = new MaterialMasterAccess();

            if (objAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), ddlRejectTo.SelectedValue, Utility.RemoveSpecialChar(txtRejectNote.Text), lblUserId.Text) > 0)
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

    public void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        VendorMasterAccess VendorMasterAccess = new VendorMasterAccess();
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
                            //strBuilder.Append("<a href='" + row["Page_URL"] + "' class='redStatus'>" + row["Section_Name"] + "</a>");
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
                    //PFun_DT06032020 Added by NR
                    if (Session[StaticKeys.SelectedModuleId].ToString() == "224" || Session[StaticKeys.SelectedModuleId].ToString() == "226")
                    {
                    }
                    else
                    {
                        //PFun_DT06032020 Added by NR

                        strBuilder.Append("<tr><td class='NoteWhiteBackGround'><b><u>Note :</u></b>");
                        strBuilder.Append("<br />1. Enter '#' as Old/ New Value to denote Blank.");
                        strBuilder.Append("<br />2. Save only those sections that needs to be changed.");
                        //strBuilder.Append("<br />3. Fill only those fields with new value,that are to be changed.");
                        //GST changes
                        //strBuilder.Append("<br />3. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Servce Tax details are to be changed.");
                        strBuilder.Append("<br />3. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Service Tax / GST details are to be changed.");
                        //GST changes
                        strBuilder.Append("</td></tr>");
                    }

                }
                else if (ActionType == "U" || ActionType == "B")
                {
                    strBuilder.Append("<tr><td class='NoteWhiteBackGround'>Note :");
                    strBuilder.Append("<br />1. Fill only those fields with new value,that are to be changed.");
                    //strBuilder.Append("<br />2. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Servce Tax details.");
                    strBuilder.Append("</td></tr>");
                }
                else
                {
                    strBuilder.Append("<tr><td class='NoteWhiteBackGround'>Note :");
                    //GST changes
                    //strBuilder.Append("<br />1. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Servce Tax details.");
                    strBuilder.Append("<br />1. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Service Tax / GST details.");
                    //GST changes
                    strBuilder.Append("<br />2. Please also attach a declaration from PAN card holder, in case where Vendor name does not match the name on PAN card.");
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

                        // Get a ClientScriptManager reference from the Page class.
                        ClientScriptManager cs = Page.ClientScript;

                        // Check to see if the startup script is already registered.
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
            /*throw*/;
                _log.Error("ReadSectionTabs1", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("ReadSectionTabs", ex); }
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
            //throw ex;
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }

    private void SubmitToNxtWF()
    {
        Response.Redirect("../Vendor/VendorMaster.aspx", false);
    }

    private void SubmitRequest(string remarks = "")
    {
        MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
        VendorMasterAccess vendorMasterAccess = new VendorMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            //Vendor Workflow Modification changes Start
            //if (vendorMasterAccess.CheckDocStatus(lblMasterHeaderId.Text) > 0)
            //{
            //Vendor Workflow Modification changes End
            using (TransactionScope scope = new TransactionScope())
            {

                if (ObjMaterialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                {
                    flg = true;
                    scope.Complete();
                }
            }
            if (flg)
            {
                if (!btnRejectTo.Visible)
                    Response.Write("<script>alert('Vendor Master Request No: " + lblRequestNo.Text + " created.');window.location.href ='VendorMaster.aspx'; </script>");
                else
                    Response.Redirect("../Vendor/VendorMaster.aspx", false);
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //Vendor Workflow Modification changes Start
            //}
            //else
            //{
            //    lblMsg.Text = "Kindly attach all the mandatory documents.";
            //    pnlMsg.CssClass = "error";
            //    pnlMsg.Visible = true;
            //}
            //Vendor Workflow Modification changes End
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SubmitRequest", ex);
        }
    }
}
