using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.LDAPHelper;
using System.Data.SqlClient;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.IO;
using log4net;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class Login : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //Email Redirection Start
    string mstrType;
    string modId;
    string mode;
    string actn;
    //Email Redirection End

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.ssouid] != null)
            {
                string Paths = ConfigurationManager.AppSettings["SSOIntPath1"];
                Paths = Paths + "/SSORedirect.aspx?ssouid=" + Convert.ToString(Session[StaticKeys.ssouid]);
                Response.Redirect("" + Paths + "", false);
            }
            else
            {
                string Paths = ConfigurationManager.AppSettings["SSOIntPath"];
                Response.Redirect("" + Paths + "", false);
            }
            //if (!IsPostBack)
            //{
            //    SetFocus();
            //    Session[StaticKeys.MasterHeaderId] = null;
            //    Session[StaticKeys.MstrType] = null;

            //    if (Request.QueryString["mid"] != null)
            //    {
            //        Session[StaticKeys.MasterHeaderId] = Request.QueryString["mid"].ToString();
            //    }
            //    if (Request.QueryString["mstrType"] != null)
            //    {
            //        Session[StaticKeys.MstrType] = Request.QueryString["mstrType"].ToString();
            //    }

            //    //Email Redirection End
            //    // PassLogins();

            //}
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        //IsValidUser(txtUserName.Text, txtPassword.Text);
        //IsValidUserAD(txtUserName.Text, txtPassword.Text);

        //txtUserName.Text = txtUserName.Text.Trim();
        // PassLogins();
        //_log.Info("TEST");
        CallSSOMWT(txtUserName.Text, txtPassword.Text);

    }

    private void PassLogins()
    { //SDT17052019 Change By NR , Desc :  
        try
        {
            string LDAPServer = ConfigurationManager.AppSettings["LDAPServer"];
            if (LDAPServer == "PRD")
            {

                IsValidUserAD(txtUserName.Text, txtPassword.Text);
            }
            else if (LDAPServer == "QAS" || LDAPServer == "DEV")
            {
                IsValidUser(txtUserName.Text, txtPassword.Text);
            }
            //EDT17052019 Change By NR , Desc :
        }
        catch (Exception ex)
        { _log.Error("PassLogins", ex); }
    }

    #endregion

    #region Private Methods

    private void IsValidUser(string UserName, string Pwd)
    {
        try
        {
            bool flag = false;
            FailureText.Text = "";

            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();

            dstData = userAccess.ValidateUser(txtUserName.Text, txtPassword.Text);
            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
                Session[StaticKeys.LoggedIn_User_Location] = dstData.Tables[0].Rows[0]["Location"].ToString();
                Session[StaticKeys.LoggedIn_User_ContactNo] = dstData.Tables[0].Rows[0]["ContactNo"].ToString();
                Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
                //Srinidhi
                Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();
                flag = true;
            }
            else
            {
                IsValidUserAD(UserName, Pwd);
            }

            if (flag)
            {
                RememberMe();
                //Email Redirection Start
                if (Session[StaticKeys.MasterHeaderId] != null)
                {
                    DataSet dstReqData = new DataSet();
                    dstReqData = userAccess.GetRequestorDetailsByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString(), Session[StaticKeys.LoggedIn_User_Id].ToString());
                    if (dstReqData.Tables[0].Rows.Count > 0)
                    {
                        Session[StaticKeys.RequestNo] = dstReqData.Tables[0].Rows[0]["Request_No"].ToString();
                        Session[StaticKeys.Requestor_User_Name] = dstReqData.Tables[0].Rows[0]["CreatedBy"].ToString();
                        Session[StaticKeys.Requestor_Location] = dstReqData.Tables[0].Rows[0]["Location"].ToString();
                        Session[StaticKeys.Requestor_ContactNo] = dstReqData.Tables[0].Rows[0]["ContactNo"].ToString();
                        Session[StaticKeys.ActionType] = dstReqData.Tables[0].Rows[0]["Action_Type"].ToString();
                        Session[StaticKeys.SelectedModuleId] = dstReqData.Tables[0].Rows[0]["Module_Id"].ToString();
                        Session[StaticKeys.Mode] = dstReqData.Tables[0].Rows[0]["Mode"].ToString();
                    }
                }
                //Email Redirection End
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                {
                    Response.Redirect("MultiRoleSignOn.aspx", false);
                }
                else if (Session[StaticKeys.MstrType] == "SW")
                {
                    Response.Redirect("Transaction/SoftwareApproval/SoftwareApproval.aspx", false);
                }
                else
                {
                    Response.Redirect("Shared/Home.aspx", false);
                }

            }

            else
            {
                FailureText.Text = "error Occured";
                return;
            }
            //}
            //else
            //{
            //    FailureText.Text = "User Name Or Password Incorect.";
            //}

        }
        catch
        {
            //throw;
        }
    }

    private void IsValidUserAD(string UserName, string Pwd)
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Start of execution SSO");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Msg :" + HttpContext.Current.User.Identity.Name.ToString().Substring(
            (HttpContext.Current.User.Identity.Name.ToString().IndexOf("\\")) + 1) + "");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Msg1 :" + txtUserName.Text);
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "End of execution SSO");
        }
        catch (Exception ex)
        {
            //_log.Error("Now", ex);
            //_log.Info("Now1", ex);
            //DT07042022
            WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of Date" + ex.ToString());
        }

        try
        {
            bool flag = false;
            FailureText.Text = "";
            ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();


            if (txtUserName.Text.Trim() != "")
            {
                txtUserName.Text = txtUserName.Text.Trim();
            }
            else
            {

                txtUserName.Text = HttpContext.Current.User.Identity.Name.ToString().Substring(
                (HttpContext.Current.User.Identity.Name.ToString().IndexOf("\\")) + 1).Trim();

                WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "User Name" + txtUserName.Text.Trim());
            }
            //var test = Request.ServerVariables.Get("AUTH_USER").Substring(
            //    (Request.ServerVariables.Get("AUTH_USER").IndexOf("\\")) + 1);
            UserAccess userAccesslog = new UserAccess();

            if (txtUserName.Text.Trim() != "")
            {
                try
                {
                    ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserName.Text.Trim());

                    if (ObjADUser != null)
                    {
                        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "ObjADUser");
                        UserAccess userAccess = new UserAccess();
                        DataSet dstData = new DataSet();
                        dstData = userAccess.ValidateUserAD(txtUserName.Text.Trim());
                        //dstData = userAccess.ValidateUser(txtUserName.Text, txtPassword.Text);

                        string sManagerName = "", sManagerEmail = ""; string sManagerName1 = "", sManagerEmail1 = "";
                        try
                        {
                            if (ObjADUser.ManagerName != null && ObjADUser.ManagerName != "")
                            {

                                sManagerName = ObjADUser.ManagerName;
                                if (ObjADUser.Manager != null)
                                {
                                    sManagerEmail = ObjADUser.Manager.EmailAddress;
                                }
                                //ADUserDetail ObjADUser1 = ObjADH.GetUserByLoginName("ERRChandrashekar");
                                //if (ObjADUser != null)
                                //{
                                //    sManagerEmail = ObjADUser1.EmailAddress;
                                //}
                                WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerName" + sManagerName + "_" + sManagerEmail);
                            }
                        }
                        catch (Exception ex)
                        {
                            _log.Error("sManagerName", ex);
                            WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of ManagerName" + ex.ToString());
                        }
                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            sManagerName1 = dstData.Tables[0].Rows[0]["ReportingTo_Name"].ToString();
                            sManagerEmail1 = dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString();
                        }

                        //if ((userAccess.SaveUserDetail(ObjADUser, "0") == 1) && ((dstData.Tables[0].Rows[0]["ReportingTo_Name"].ToString() != sManagerName) && (dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString() != sManagerEmail)))
                        //

                        if ((userAccess.SaveUserDetail(ObjADUser, "0") == 1) && ((sManagerName1 != sManagerName || sManagerName1 == "" || sManagerName == "") && (sManagerEmail1 != sManagerEmail || sManagerEmail1 == "" || sManagerEmail == "")))
                        {
                            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerName" + sManagerName + "_" + sManagerEmail);
                            if (sManagerName != "")
                            {
                                WriteSSOLog("CreateSSOLog_" + sdate + ".txt", " sManagerName Start");
                                if (ObjADUser.Manager != null)
                                {
                                    ADUserDetail MObjADUser = ObjADH.GetUserByLoginName(ObjADUser.Manager.LoginName);
                                    userAccess.SaveRMNSMDetail(MObjADUser, txtUserName.Text);
                                    dstData = userAccess.ValidateUserAD(txtUserName.Text.Trim());
                                }
                                WriteSSOLog("CreateSSOLog_" + sdate + ".txt", " sManagerName End");
                            }
                            else { dstData = userAccess.ValidateUserAD(txtUserName.Text.Trim()); }
                        }

                        Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                            Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                            Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                            Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                            Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                            Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                            Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
                            Session[StaticKeys.LoggedIn_User_Location] = dstData.Tables[0].Rows[0]["Location"].ToString();
                            Session[StaticKeys.LoggedIn_User_ContactNo] = dstData.Tables[0].Rows[0]["ContactNo"].ToString();
                            Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
                            Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();

                            flag = true;

                            userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "Y");
                        }

                        else
                        {
                            //User is not exist in MWT
                            userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NM");
                            FailureText.Text = "User Name Or Password Incorect.";
                        }

                        if (flag)
                        {
                            if (Session[StaticKeys.MasterHeaderId] != null)
                            {
                                DataSet dstReqData = new DataSet();
                                dstReqData = userAccess.GetRequestorDetailsByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString(), Session[StaticKeys.LoggedIn_User_Id].ToString());
                                if (dstReqData.Tables[0].Rows.Count > 0)
                                {
                                    Session[StaticKeys.RequestNo] = dstReqData.Tables[0].Rows[0]["Request_No"].ToString();
                                    Session[StaticKeys.Requestor_User_Name] = dstReqData.Tables[0].Rows[0]["CreatedBy"].ToString();
                                    Session[StaticKeys.Requestor_Location] = dstReqData.Tables[0].Rows[0]["Location"].ToString();
                                    Session[StaticKeys.Requestor_ContactNo] = dstReqData.Tables[0].Rows[0]["ContactNo"].ToString();
                                    Session[StaticKeys.ActionType] = dstReqData.Tables[0].Rows[0]["Action_Type"].ToString();
                                    Session[StaticKeys.SelectedModuleId] = dstReqData.Tables[0].Rows[0]["Module_Id"].ToString();
                                    Session[StaticKeys.Mode] = dstReqData.Tables[0].Rows[0]["Mode"].ToString();
                                }
                            }
                            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                            {
                                Response.Redirect("MultiRoleSignOn.aspx", false);
                            }
                            else if (Session[StaticKeys.MstrType] == "SW")
                            {
                                Response.Redirect("Transaction/SoftwareApproval/SoftwareApproval.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("Shared/Home.aspx", false);
                            }
                        }
                        else
                        {
                            //User is not exist in MWT

                            FailureText.Text = "Invalid User";
                            userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NM");
                            return;
                        }
                    }
                    else
                    {
                        //User is not exist in Ads
                        //DT07042022


                        FailureText.Text = "User Name Or Password Incorect.";
                        userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NA");
                        WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "UserIsnotInADs" + ObjADUser);

                    }
                }
                catch (Exception ex)
                {
                    _log.Error("InvalidUser", ex);
                    //userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NA");
                    WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception_ValidUser0" + ex.ToString());
                }
            }
            else
            {
                //User name is blank
                userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "BL");
                FailureText.Text = "User Name Or Password Incorect.";
            }
        }
        catch (Exception ex)
        {
            _log.Error("IsValidUserAD", ex);
            // throw;
            //DT07042022
            WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception_ValidUser" + ex.ToString());
        }
    }

    private void RememberMe()
    {
        if (chkRememberMe.Checked == true)
        {
            Response.Cookies["UNameMWT"].Value = txtUserName.Text;
            Response.Cookies["PWDMWT"].Value = txtPassword.Text;
            Response.Cookies["UNameMWT"].Expires = DateTime.Now.AddMonths(2);
            Response.Cookies["PWDMWT"].Expires = DateTime.Now.AddMonths(2);
        }
        else
        {
            Response.Cookies["UNameMWT"].Expires = DateTime.Now.AddMonths(-1);
            Response.Cookies["PWDMWT"].Expires = DateTime.Now.AddMonths(-1);
        }
    }

    private void SetFocus()
    {
        txtUserName.Focus();
    }


    public void WriteSSOLog(string strFileName, string strMessage)
    {
        try
        {
            //Path.GetTempPath()
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ProsolLog", strFileName), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
            objStreamWriter.Close();
            objFilestream.Close();
            //return true;  
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }
    #endregion

    private void CallSSOMWT(string UserName, string Pwd)
    {
        string sdate = "";
        authtokens token = Gettoken();
        users users1 = GetUserByMail(token.authtokend, UserName);
        bool flag = false;
        FailureText.Text = "";
        ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
        UserAccess userAccesslog = new UserAccess();
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "CallSSOMWT");
            if (users1 != null)
            {
                WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "displayName" + users1.displayName);
                if (users1.displayName != "" && users1.displayName != null)
                {
                    try
                    {
                        UserAccess userAccess = new UserAccess();
                        DataSet dstData = new DataSet();
                        string puserPrincipalName = "";
                        try
                        {
                            puserPrincipalName = (users1.userPrincipalName.Split('@')[0]);
                        }
                        catch (Exception ex)
                        {
                            puserPrincipalName = UserName;// users1.mail;
                        }
                        dstData = userAccess.ValidateUserAD(puserPrincipalName);
                        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "users1.mail" + users1.mail);
                        string sManagerName = "", sManagerEmail = ""; string sManagerName1 = "", sManagerEmail1 = "";
                        try
                        {
                            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerName");
                            if (users1.manager != null)
                            {
                                if (users1.manager.displayName != null && users1.manager.displayName != "")
                                {
                                    WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerName" + users1.manager.displayName);
                                    sManagerName = users1.manager.displayName;
                                }
                                if (users1.manager.mail != null && users1.manager.mail != "")
                                {
                                    WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerName" + users1.manager.mail);
                                    sManagerEmail = users1.manager.mail;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerNameex");
                            _log.Error("sManagerName", ex);
                        }
                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            sManagerName1 = dstData.Tables[0].Rows[0]["ReportingTo_Name"].ToString();
                            sManagerEmail1 = dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString();
                        }
                        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "MshSSO1");
                        //if ((userAccess.SaveUserDetail(ObjADUser, "0") == 1) && ((sManagerName1 != sManagerName || sManagerName1 == "" || sManagerName == "") && (sManagerEmail1 != sManagerEmail || sManagerEmail1 == "" || sManagerEmail == "")))
                        //if ((userAccess.SaveUserDetail(ObjADUser, "0") == 1) && ((sManagerName1 != sManagerName || sManagerName1 == "" || sManagerName == "") && (sManagerEmail1 != sManagerEmail || sManagerEmail1 == "" || sManagerEmail == "")))
                        //{
                        //    WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "sManagerName" + sManagerName + "_" + sManagerEmail);
                        //    if (sManagerName != "")
                        //    {
                        //        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", " sManagerName Start");
                        //        if (ObjADUser.Manager != null)
                        //        {
                        //            ADUserDetail MObjADUser = ObjADH.GetUserByLoginName(ObjADUser.Manager.LoginName);
                        //            userAccess.SaveRMNSMDetail(MObjADUser, txtUserName.Text);
                        //            dstData = userAccess.ValidateUserAD(txtUserName.Text.Trim());
                        //        }
                        //        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", " sManagerName End");
                        //    }
                        //    else { dstData = userAccess.ValidateUserAD(txtUserName.Text.Trim()); }
                        //}

                        Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
                        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Session[StaticKeys.RoleCount]" + Session[StaticKeys.RoleCount]);

                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "1Session[StaticKeys.RoleCount]");

                            Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                            Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                            Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                            Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                            Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                            Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                            Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
                            Session[StaticKeys.LoggedIn_User_Location] = dstData.Tables[0].Rows[0]["Location"].ToString();
                            Session[StaticKeys.LoggedIn_User_ContactNo] = dstData.Tables[0].Rows[0]["ContactNo"].ToString();
                            Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
                            Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();

                            flag = true;
                            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "2Session[StaticKeys.RoleCount]");

                            userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "Y");
                        }

                        else
                        {
                            userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NM");
                            FailureText.Text = "User Name Or Password Incorect.";
                        }

                        if (flag)
                        {
                            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "flag");

                            if (Session[StaticKeys.MasterHeaderId] != null)
                            {
                                DataSet dstReqData = new DataSet();
                                dstReqData = userAccess.GetRequestorDetailsByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString(), Session[StaticKeys.LoggedIn_User_Id].ToString());
                                if (dstReqData.Tables[0].Rows.Count > 0)
                                {
                                    Session[StaticKeys.RequestNo] = dstReqData.Tables[0].Rows[0]["Request_No"].ToString();
                                    Session[StaticKeys.Requestor_User_Name] = dstReqData.Tables[0].Rows[0]["CreatedBy"].ToString();
                                    Session[StaticKeys.Requestor_Location] = dstReqData.Tables[0].Rows[0]["Location"].ToString();
                                    Session[StaticKeys.Requestor_ContactNo] = dstReqData.Tables[0].Rows[0]["ContactNo"].ToString();
                                    Session[StaticKeys.ActionType] = dstReqData.Tables[0].Rows[0]["Action_Type"].ToString();
                                    Session[StaticKeys.SelectedModuleId] = dstReqData.Tables[0].Rows[0]["Module_Id"].ToString();
                                    Session[StaticKeys.Mode] = dstReqData.Tables[0].Rows[0]["Mode"].ToString();
                                }
                            }
                            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                            {
                                Response.Redirect("MultiRoleSignOn.aspx", false);
                            }
                            else if (Session[StaticKeys.MstrType] == "SW")
                            {
                                Response.Redirect("Transaction/SoftwareApproval/SoftwareApproval.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("Shared/Home.aspx", false);
                            }
                        }
                        else
                        {
                            //User is not exist in MWT

                            FailureText.Text = "Invalid User";
                            userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NM");
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        _log.Error("InvalidUser", ex);
                        //userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NA");
                        WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception_ValidUser0" + ex.ToString());
                    }
                }
                else
                {
                    FailureText.Text = "User Name Or Password Incorect.";
                    userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NA");
                }
            }
            else
            {
                FailureText.Text = "User not exist in AD Sysytem.";
                userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NA");
            }
        }
        catch (Exception ex)
        {
            _log.Error("IsValidUserAD", ex);
            WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception_ValidUser" + ex.ToString());
        }
    }
    private authtokens Gettoken()
    {
        string sdate = "";
        //try
        //{
        //    DateTime date = System.DateTime.Now;
        //    sdate = date.ToString("dd/MM/yyyy");
        //    sdate = sdate.Replace(@"/", "");
        //    WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Start of execution SSO");
        //    WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "MsgUser Star :");
        //    if (txtUserName.Text == "")
        //    {
        //        txtUserName.Text = HttpContext.Current.User.Identity.Name.ToString().Substring(
        //        (HttpContext.Current.User.Identity.Name.ToString().IndexOf("\\")) + 1);
        //        WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "MsgUser :" + txtUserName.Text);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of Date1" + ex.ToString());
        //}
        authtokens authtokensu = new authtokens();
        try
        {
            string urlStr = "https://login.microsoftonline.com/a9c6fa70-d6e2-4563-adf3-0b24d921b140/oauth2/v2.0/token";
            string jsRequest = "grant_type=client_credentials" + "&client_id=f87ff8b8-20e2-47f2-bc56-3a7338c45943" + "&client_secret=Xcu8Q~.uNKupBXXCTQnQtVVeYZr8jdSTsMRkma9m" + "&scope=https://graph.microsoft.com/.default";
            var httpConn = (HttpWebRequest)WebRequest.Create(urlStr);

            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;//SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            httpConn.Method = "POST";
            httpConn.ContentType = "application/x-www-form-urlencoded";
            httpConn.Accept = "*/*";
            httpConn.AllowWriteStreamBuffering = true;
            // httpConn.AllowReadStreamBuffering = true; 
            httpConn.KeepAlive = false;
            using (var streamWriter = new StreamWriter(httpConn.GetRequestStream()))
            {
                streamWriter.Write(jsRequest);
            }
            using (HttpWebResponse response = (HttpWebResponse)httpConn.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    var obj = JsonConvert.DeserializeObject<JObject>(responseText);
                    //var token = obj["access_token"].Value<string>();
                    //GetUserByMail(token);

                    authtokensu.authtokend = obj["access_token"].Value<string>();
                    WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "access_token :" + authtokensu.authtokend);
                }
            }
        }
        catch (Exception ex)
        {
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "access_tokene :" + ex.Message.ToString());
        }
        return authtokensu;
    }
    private users GetUserByMail(string token, string UserName)
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Start of execution SSO");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "MsgUser Star :");

            UserName = (UserName.Split('@')[0]);

        }
        catch (Exception ex)
        {
            WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of Date1" + ex.ToString());
        }
        users objusers = new users();
        try
        {
            string userwid = UserName;// txtUserName.Text.Trim();
            //string userwid2 = "'" + userwid + "@lupin.com" + "','" + userwid + "@lyfe.in'";
            string userwid2 = "'" + userwid + "@lupin.com" + "','" + userwid + "@lupindigitalhealth.com" + "','" + userwid + "@lupinworld.com" + "','" + userwid + "@generichealth.com.au" + "','" + userwid + "@lupinpharma.com" + "','" + userwid + "@lupindiagnostics.com" + "','" + userwid + "@lyfe.in'";
            var url = "https://graph.microsoft.com/v1.0/users?$filter=mail in  " + "(" + userwid2 + ")" + " &$select=AccountEnabled,displayName,id,userPrincipalName,mail,mobilePhone,officeLocation,onPremisesSamAccountName&$expand=manager($levels=max;$select=mail,officeLocation,mobilePhone,displayName,employeeId,userPrincipalName,onPremisesSamAccountName)";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            httpRequest.Headers.Add("Authorization", "Bearer " + token);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var outer = JsonConvert.DeserializeObject<OData<object[]>>(result);
                if (outer.value.Length > 0)
                {
                    for (int i = 0; i < outer.value.Length; i++)
                    {
                        objusers = JsonConvert.DeserializeObject<users>(outer.value[0].ToString());

                    }
                }
                else
                {
                    objusers = GetUserByPname(token, UserName);
                }

            }
        }
        catch (Exception ex) { WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "access_token :" + ex.Message.ToString()); }
        return objusers;
    }
    private users GetUserByPname(string token, string UserName)
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "Start of execution SSO");
            WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "MsgUser Star :");

            UserName = (UserName.Split('@')[0]);

        }
        catch (Exception ex)
        {
            WriteSSOLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of Date1" + ex.ToString());
        }
        users objusers = new users();
        try
        {
            // string userwid = "dilipkram";
            string userwid = UserName;// txtUserName.Text.Trim();
            //string userwid2 = "'" + userwid + "@lupin.com" + "','" + userwid + "@lyfe.in'";
            string userwid2 = "'" + userwid + "@lupin.com" + "','" + userwid + "@lupindigitalhealth.com" + "','" + userwid + "@lupinworld.com" + "','" + userwid + "@generichealth.com.au" + "','" + userwid + "@lupinpharma.com" + "','" + userwid + "@lupindiagnostics.com" + "','" + userwid + "@lyfe.in'";
            var url = "https://graph.microsoft.com/v1.0/users?$filter=userPrincipalName in  " + "(" + userwid2 + ")" + " &$select=AccountEnabled,displayName,id,userPrincipalName,mail,mobilePhone,officeLocation,onPremisesSamAccountName&$expand=manager($levels=max;$select=mail,officeLocation,mobilePhone,displayName,employeeId,userPrincipalName,onPremisesSamAccountName)";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            httpRequest.Headers.Add("Authorization", "Bearer " + token);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var outer = JsonConvert.DeserializeObject<OData<object[]>>(result);
                if (outer.value.Length > 0)
                {
                    for (int i = 0; i < outer.value.Length; i++)
                    {
                        objusers = JsonConvert.DeserializeObject<users>(outer.value[0].ToString());
                    }
                }
            }
        }
        catch (Exception ex) { WriteSSOLog("CreateSSOLog_" + sdate + ".txt", "access_token :" + ex.Message.ToString()); }
        return objusers;
    }



}

public class OData<T>
{
    [JsonProperty("odata.context")]
    public string Metadata { get; set; }
    public T value { get; set; }
}

public class users
{
    public string displayName { get; set; }
    public string onPremisesSamAccountName { get; set; }
    public string accountEnabled { get; set; }
    public string id { get; set; }
    public string userPrincipalName { get; set; }
    public string mail { get; set; }
    public string mobilePhone { get; set; }
    public string officeLocation { get; set; }
    public manager manager { get; set; }
}

public class manager
{
    [JsonProperty("odata.type")]
    public string Metadata { get; set; }
    public string displayName { get; set; }
    public string onPremisesSamAccountName { get; set; }
    public string accountEnabled { get; set; }
    public string id { get; set; }
    public string userPrincipalName { get; set; }
    public string mail { get; set; }
    public string mobilePhone { get; set; }
    public string officeLocation { get; set; }
}

public class authtokens
{
    public string authtokend { get; set; }
}