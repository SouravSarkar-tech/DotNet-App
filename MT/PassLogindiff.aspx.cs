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
using log4net;
    
public partial class PassLogindiff : System.Web.UI.Page
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
            if (!IsPostBack)
        {
            SetFocus();

            if (Request.Cookies["UNameMWT"] != null)
                txtUserName.Text = Request.Cookies["UNameMWT"].Value;
            if (Request.Cookies["PWDMWT"] != null)
                txtPassword.Attributes.Add("value", Request.Cookies["PWDMWT"].Value);
            if (Request.Cookies["UNameMWT"] != null && Request.Cookies["PWDMWT"] != null)
                chkRememberMe.Checked = true;

            //Email Redirection Start
            Session[StaticKeys.MasterHeaderId] = null;
            Session[StaticKeys.MstrType] = null;

            if (Request.QueryString["mid"] != null)
            {
                Session[StaticKeys.MasterHeaderId] = Request.QueryString["mid"].ToString();
            }
            if (Request.QueryString["mstrType"] != null)
            {
                Session[StaticKeys.MstrType] = Request.QueryString["mstrType"].ToString();
            }
                //Email Redirection End

            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //IsValidUser(txtUserName.Text, txtPassword.Text);
            //IsValidUserAD(txtUserName.Text, txtPassword.Text);
            PassLogins();
    }
        catch (Exception ex)
        { _log.Error("imgBtnLogin_Click", ex); }
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
        }
        catch (Exception ex)
        { _log.Error("PassLogins", ex); }
        //EDT17052019 Change By NR , Desc : 
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
        UserAccess userAccesslog = new UserAccess();
        try
        {
            bool flag = false;
            FailureText.Text = "";
            ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();

            string LDAPDomain = ConfigurationManager.AppSettings["LDAPDomain"];
            if (ObjADH.IsAuthenticated(LDAPDomain, txtUserName.Text, txtPassword.Text))
            {
                //dstData = userAccess.ValidateUser(txtUserName.Text, "");
                dstData = userAccess.ValidateUserAD(txtUserName.Text.Trim());
                if (dstData.Tables[0].Rows.Count > 0)
            {
                //dstData = userAccess.ValidateUser(txtUserName.Text, txtPassword.Text); 
                //MGR_UPD_SDT27062019 Commented by NR
                //if (dstData.Tables[0].Rows.Count < 1)
                //{
                //    ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserName.Text);
                //    if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
                //    {
                //        dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
                //    }
                //}
                //MGR_UPD_SDT27062019  Commented by NR

                //MGR_UPD_SDT27062019
                //ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserName.Text);
               
                    //string sManagerName = "", sManagerEmail = "";
                    //try
                    //{
                    //    if (ObjADUser.ManagerName != null && ObjADUser.ManagerName != "")
                    //    {
                    //        sManagerName = ObjADUser.ManagerName;
                    //        sManagerEmail = ObjADUser.Manager.EmailAddress;
                    //    }
                    //}
                    //catch
                    //{ }
                    //if ((userAccess.SaveUserDetail(ObjADUser, "0") == 1) && ((dstData.Tables[0].Rows[0]["ReportingTo_Name"].ToString() != sManagerName) && (dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString() != sManagerEmail)))
                    //{
                    //    ADUserDetail MObjADUser = ObjADH.GetUserByLoginName(ObjADUser.Manager.LoginName);
                    //    userAccess.SaveRMNSMDetail(MObjADUser, txtUserName.Text);
                    //    dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
                    //}
                    //MGR_UPD_EDT27062019

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

                        //PROSOL_SDT16092019
                        //Session[StaticKeys.LoggedIn_User_Pass] = Convert.ToString(txtPassword.Text);
                        //PROSOL_SDT16092019

                        ////Response.Cookies["uid"].Value = Convert.ToString(Session[StaticKeys.LoggedIn_User_Name]);
                        ////Response.Cookies["upwd"].Value = Convert.ToString(Session[StaticKeys.LoggedIn_User_Pass]);


                        flag = true;

                        userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "Y");
                    }

                    else
                    {
                        FailureText.Text = "User Name Or Password Incorect.";
                        userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NM");
                    }
                }
                else
                {
                    FailureText.Text = "User Name Or Password Incorect.";
                    userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NM");
                }
            }
            else
            {

                FailureText.Text = "User Name Or Password Incorect.";
                userAccesslog.SaveUserLog(txtUserName.Text.Trim(), "NA");
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


        }
        catch (Exception ex)
        {
            _log.Error("IsValidUserAD", ex);
            // throw;
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

    #endregion
}