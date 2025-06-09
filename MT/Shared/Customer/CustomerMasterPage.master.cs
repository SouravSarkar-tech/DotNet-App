using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Accenture.MWT.DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Transactions;
using System.Configuration;
using log4net;
public partial class Shared_CustomerMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Page Events

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
                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text);

                        if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R" || Session[StaticKeys.ActionType].ToString() == "E") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            if (!objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text))
                            {
                                btnSAPUpload.Visible = true;
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                btnSAPUpload.Visible = false;
                                btnRejectTo.Visible = false;
                                btnSubmit.Visible = true;
                                btnSubmit.Text = "Approve";
                            }
                        }
                        else
                        {
                            btnSubmit.Visible = true;
                            if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() != "2")
                            {
                                btnSubmit.Text = "Approve";
                            }
                        }
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

    private void SubmitRequest(string remarks = "")
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {


            using (TransactionScope scope = new TransactionScope())
            {
                //if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                //Change By Swati : New Proc for Approving Customer Master
                if (materialMasterAccess.ApproveRequestCustomer(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                //End Change
                {
                    flg = true;
                    scope.Complete();
                }
            }
            if (flg)
            {
                if (!btnRejectTo.Visible)
                    Response.Write("<script>alert('Customer Master Request No: " + lblRequestNo.Text + " created.');window.location.href ='CustomerMaster.aspx'; </script>");
                else
                    Response.Redirect("../Customer/CustomerMaster.aspx", false);
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
            //throw ex;
        }

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
                Response.Redirect("../Customer/CustomerMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Customer/CustomerMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    #endregion

    #region Public Method

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

    private void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        StringBuilder strBuilder = new StringBuilder();
        string currentSectionId = ((Label)ContentPlaceHolder1.FindControl("lblSectionId")).Text;
        string sectionId = string.Empty;
        string ActionType = Session[StaticKeys.ActionType].ToString();
        bool flg = false;
        if (ActionType == "C")
            flg = false;
        else
            flg = true;

        bool flg2 = true;
        int sectionStatus = 0;
        try
        {
            //Start Addition By Swati M Date: 16.10.2018
            //dstData = userAccess.ReadSectionTabs(userId, departmentId, lblMasterHeaderId.Text);
            DataSet ds = userAccess.GetIfDivExist(lblMasterHeaderId.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string DivisionType = ds.Tables[0].Rows[0]["DivisionType"].ToString();

                dstData = userAccess.ReadSectionTabs_Customer(userId, departmentId, lblMasterHeaderId.Text, DivisionType);
            }
            else
            {
                dstData = userAccess.ReadSectionTabs(userId, departmentId, lblMasterHeaderId.Text);
            }
            //End
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

            if (ActionType == "E")
            {
                strBuilder.Append("<tr><td class='NoteWhiteBackGround'><b><u>Note :</u></b>");
                strBuilder.Append("<br />1. All the fields marked '*' are mandatory.");
                strBuilder.Append("</td></tr>");
            }
            else if (ActionType == "C")
            {
                strBuilder.Append("<tr><td class='NoteWhiteBackGround'><b><u>Note :</u></b>");
                strBuilder.Append("<br />1. Enter '#' as Old/ New Value to denote Blank.");
                strBuilder.Append("<br />2. Save only those sections that needs to be changed.");
                strBuilder.Append("</td></tr>");
            }
            else if (ActionType == "U" || ActionType == "B")
            {
                strBuilder.Append("<tr><td class='NoteWhiteBackGround'>Note :");
                strBuilder.Append("<br />1. Fill only those fields with new value,that are to be changed.");
                strBuilder.Append("</td></tr>");
            }
            else
            {
                strBuilder.Append("<tr><td class='NoteWhiteBackGround'>Note :");
                //GST Changes
                //strBuilder.Append("<br />1. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Servce Tax details.");
                strBuilder.Append("<br />1. Please attach the supporting documents in case of PAN/ CST/ LST / VAT / Servce Tax / GST details.");
                //GST Changes 
                strBuilder.Append("<br />2. Please also attach a declaration from PAN card holder, in case where Customer name does not match the name on PAN card.");
                strBuilder.Append("</td></tr>");
            }

            strBuilder.Append(" </table>");
            litTab.Text = strBuilder.ToString();

            if ((flg) && (flg2))
            {
                btnSubmit.Enabled = true;

                //Added By Nitin R , SDT03052019 , Des : to check request type for Customer IRF
                //QA ID is 40 and PRD is 46
                MaterialMasterAccess materialMasterAccess1 = new MaterialMasterAccess();
                if (materialMasterAccess1.CheckRDMStatus(lblMasterHeaderId.Text) <= 0 &&
                    materialMasterAccess1.CheckRequestType(lblMasterHeaderId.Text) > 0
                    && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["DEPRDM"])
                    )
                {
                    //btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    //btnSubmit.Visible = true;
                    btnSubmit.Enabled = true;
                }
                //Added By Nitin R , SDT03052019 , Des : to check request type for Customer IRF


                if (!btnRejectTo.Visible && Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "13" && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B") && (Session[StaticKeys.Mode].ToString() == "M" || Session[StaticKeys.Mode].ToString() == "N"))
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
            _log.Error("ReadSectionTabs", ex);
            //throw;
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
            //throw ex;
        }
    }
    #endregion
}