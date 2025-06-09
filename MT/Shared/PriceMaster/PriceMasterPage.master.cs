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
using System.Configuration;
using System.Net;
using System.IO;

public partial class Shared_PriceMaster_PriceMasterPage : System.Web.UI.MasterPage
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

            if (IsValidURL(SafeTypeHandling.ConvertToString(HttpContext.Current.Request.UrlReferrer)) == false)
            {
                Response.Redirect("../login.aspx");
            }
            else
            {
                if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
                {
                    Request.Browser.Adapters.Clear();
                }

                if ((Session[StaticKeys.LoggedIn_User_Id] != null) && (Session[StaticKeys.MasterHeaderId] != null))
                {

                    xmlDataSource.Data = GetMenu(Session[StaticKeys.LoggedIn_User_Id].ToString());
                    string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    if (!IsPostBack)
                    {
                        MaterialMasterAccess objAccess = new MaterialMasterAccess();
                        lblUserName.Text = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                        lblProfile.Text = Session[StaticKeys.LoggedIn_User_Profile].ToString();
                        lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                        lblDate.Text = DateTime.Now.ToShortDateString();
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                        ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);
                        string mode = Session[StaticKeys.Mode].ToString();
                        if (mode == "M" || mode == "N")
                        {
                            if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                btnSAPUpload.Visible = true;
                            }
                            else
                            {
                                btnSubmit.Visible = true;
                            }
                        }
                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text);

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


    /// <summary>
    ///    //SPNAIRCR_SDT05122019 Added
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// SPNAIRCR_SDT05122019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApproveRemarks_Click(object sender, EventArgs e)
    {
        bool sflag = false;
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        DataSet ds = materialMasterAccess.ReadPR(lblMasterHeaderId.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
        //    if (MaterialMasterAccess.IsUserHasPRReq(lblMasterHeaderId.Text))
        //{
            string sdate = "";
            try
            {
                DateTime date = System.DateTime.Now;
                sdate = date.ToString("dd/MM/yyyy");
                sdate = sdate.Replace(@"/", "");
                WriteWFLog("CreateWFLog_" + sdate + ".txt", "Start of execution WF API");
            }
            catch (Exception ex)
            {
                _log.Error("IsUserHasPRReq", ex);
            }

            var responseText = "";
            string docId = "";
            string emailId = "";
            try
            {

                docId = Convert.ToString(ds.Tables[0].Rows[0]["sWFRequestNo"].ToString());
                emailId = Convert.ToString(Session[StaticKeys.LoggedIn_User_Name]);
                emailId = emailId + "@lupin.com";
                WriteWFLog("CreateWFLog_" + sdate + ".txt", "reqId :" + docId + ", emailId:" + emailId + "");

                string webAddr = Convert.ToString(ConfigurationManager.AppSettings["workflowLink"]) + "/completeTaskByMail?docId=" + docId + "&emailId=" + emailId + "&decision=APPROVED&formID=viaMail";
                string username = Convert.ToString(ConfigurationManager.AppSettings["wfusername"]);
                string password = Convert.ToString(ConfigurationManager.AppSettings["wfpassword"]);
                // string webAddr = "http://172.36.0.157:18086/workflowwebui/mobile/dtr/completeTaskByMail?docId=133084&emailId=alfreddsouza@lupin.com&decision=APPROVED&formID=viaMail";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8"; 
                httpWebRequest.Method = "POST";

                //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("testing:123456");
                //string val = System.Convert.ToBase64String(plainTextBytes);
                //httpWebRequest.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

                //var username = "adminl2j";
                //var password = "mQQ0f5S7"; 
                string encoded = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
                //string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                //                               .GetBytes(username + ":" + password));
                httpWebRequest.Headers.Add("Authorization", "Basic " + encoded); 
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();

                    if (responseText == "\"Success\"")
                    //if (responseText == "\"Fail\"")
                    {
                        WriteWFLog("CreateWFLog_" + sdate + ".txt", "Success Msg :" + responseText + "");
                        //BasicDataAccess basicDataAccess = new BasicDataAccess();
                        //basicDataAccess.UpdateRequestStatus(proslid, Convert.ToString(TxtRemarks.Text), "S", "");

                        //

                        sflag = true;
                    }
                    else
                    {
                        sflag = false;
                        WriteWFLog("CreateWFLog_" + sdate + ".txt", "Error Msg :" + responseText + "");

                    }
                } 
            }
            catch (WebException ex)
            {
                _log.Error("BtnRetrigger_Click11", ex);

                Console.WriteLine(ex.Message);
                WriteWFLog("CreateWFLog_" + sdate + ".txt", "Error Msg :" + ex.Message);
            }
            WriteWFLog("CreateWFLog_" + sdate + ".txt", "End of execution WF API");


            //if (1 == 1)
            //{
            //    sflag = false;
            //}
            //else
            //{
            //    sflag = false;
            //}
        }
        else
        {
            sflag = true;
        }



        if(sflag == true)
        { 
        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtApproveNote.Text));
        }
        catch (Exception ex)
        { _log.Error("btnApproveRemarks_Click", ex); }
        }
        else
        {
            lblMsg.Text = "Please contact to admin";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    public void WriteWFLog(string strFileName, string strMessage)
    {
        try
        {
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ProsolLog", strFileName), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
            objStreamWriter.Close();
            objFilestream.Close();
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }

    //SPNAIRCR_SDT05122019 Added
    private void SubmitRequest(string sApproveNote = "")
    {
        VendorMasterAccess VendorMasterAccess = new VendorMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (VendorMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, sApproveNote) > 0)
                {
                    if (VendorMasterAccess.SaveMaterialHeader(lblMasterHeaderId.Text, moduleId, lblUserId.Text, "M") > 0)
                    {
                        flg = true;
                        scope.Complete();
                    }
                }
            }
            if (flg)
            {
                Response.Redirect("../PriceMaster/PriceMaster.aspx?pg=8", false);
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
        try
        {
            if (RollbackRequest())
            {
                Response.Redirect("../PriceMaster/PriceMaster.aspx?pg=8", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../PriceMaster/PriceMaster.aspx?pg=8", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

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

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        try
        {
            Session[StaticKeys.LoggedIn_User_Id] = "";
            Session.Remove(StaticKeys.LoggedIn_User_Id);
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
            //Response.Redirect("../login.aspx?flgError=X");
            Response.Redirect("../login.aspx?flgError=X", false);
        }
        catch (Exception ex)
        { _log.Error("lnkLogout_Click", ex); }
    }

    private string GetMenu(string user_ID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        SqlCommand cmd;
        try
        {
            objDal.OpenConnection(this.Page);
            cmd = new SqlCommand("Proc_Read_User_Menus", objDal.cnnConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserId", user_ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dstMenu = new DataSet();
            dstMenu.DataSetName = "Menus";
            da.Fill(dstMenu, "Menu");


            DataRelation relation = new DataRelation("ParentChild", dstMenu.Tables["Menu"].Columns["MenuID"], dstMenu.Tables["Menu"].Columns["ParentID"], true)
            {
                Nested = true
            };

            dstMenu.Relations.Add(relation);
            return dstMenu.GetXml();
        }
        catch (Exception ex)
        {
            _log.Error("GetMenu", ex);
            throw ex;
        }
        finally
        {
            objDal.CloseConnection(objDal.cnnConnection);
            objDal = null;
        }
    }

    //private void ReadSectionTabs(string userId, string departmentId, string moduleId)
    //{
    //    VendorMasterAccess VendorMasterAccess = new VendorMasterAccess();
    //    UserAccess userAccess = new UserAccess();
    //    DataSet dstData = new DataSet();
    //    StringBuilder strBuilder = new StringBuilder();
    //    //string currentPageSeq = Request.QueryString["pgseq"].ToString();
    //    //string currentSectionId = Request.QueryString["sid"].ToString();
    //    string sectionId = string.Empty;
    //    bool flg = false;
    //    bool flg2 = true;
    //    int sectionStatus = 0;
    //    try
    //    {
    //        dstData = userAccess.ReadSectionTabs(userId, departmentId, moduleId);

    //        strBuilder.Append("<table cellspacing='0' width='100%'>");
    //        foreach (DataRow row in dstData.Tables[0].Rows)
    //        {
    //            strBuilder.Append("<tr><td class='navigationBox'>");
    //            sectionId = row["Section_Id"].ToString();
    //            sectionStatus = VendorMasterAccess.CheckSectionStatus(sectionId, lblMasterHeaderId.Text);

    //            if (sectionStatus > 0)
    //            {
    //                flg = true;
    //            }
    //            else
    //            {
    //                flg = false;
    //            }

    //            if (row["Sequence"].ToString() != currentPageSeq)
    //            {
    //                if (sectionStatus > 0)
    //                {
    //                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
    //                }
    //                else
    //                {
    //                    //strBuilder.Append("<a href='" + row["Page_URL"] + "' class='redStatus'>" + row["Section_Name"] + "</a>");
    //                    strBuilder.Append("<a href='" + row["Page_URL"] + "' >" + row["Section_Name"] + "</a>");
    //                }
    //            }
    //            else if (row["Sequence"].ToString() == currentPageSeq)
    //            {
    //                strBuilder.Append("<a href='" + row["Page_URL"] + "' class='Active'>" + row["Section_Name"] + "</a>");
    //            }
    //            strBuilder.Append("</td></tr>");
    //        }

    //        strBuilder.Append(" </table>");
    //        litTab.Text = strBuilder.ToString();

    //        if ((flg) && (flg2))
    //        {
    //            btnSubmit.Enabled = true;
    //        }
    //        else
    //        {
    //            btnSubmit.Enabled = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }

    //}

    public void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        VendorMasterAccess VendorMasterAccess = new VendorMasterAccess();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        StringBuilder strBuilder = new StringBuilder();
        string currentSectionId = ((Label)ContentPlaceHolder1.FindControl("lblSectionId")).Text;
        string sectionId = string.Empty;
        //string ActionType = Session[StaticKeys.ActionType].ToString();
        bool flg;
        //if (ActionType == "C")
        //    flg = false;
        //else
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

                //if (sectionStatus <= 0 && ActionType != "C")
                //{
                //    flg = false;
                //}
                //else if (sectionStatus > 0 && ActionType == "C")
                //{
                //    flg = true;
                //}
                if (sectionStatus <= 0)
                {
                    flg = false;
                }
                else if (sectionStatus > 0)
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

            //trSideMenuTab.Width = "0px";

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
            //throw;
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
            //throw ex;
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }
}
