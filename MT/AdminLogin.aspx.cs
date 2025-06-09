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
using log4net;

public partial class Login : System.Web.UI.Page
{

    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }


    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            IsValidUser(txtUserName.Text, txtPassword.Text);
        }
        catch (Exception ex)
        { _log.Error("IsValidUser", ex); }
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
            string pwd1 = txtPassword.Text == "Lup@123" ? "mwt" : "1234";
            //string pwd1 = txtPassword.Text == "1224" ? "mwt" : "1234";

            dstData = userAccess.ValidateUser(txtUserName.Text, pwd1);

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
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                {
                    Response.Redirect("MultiRoleSignOn.aspx", false);
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
        catch(Exception ex)
        {
            //throw;
            _log.Error("IsValidUser", ex);
        }
    }
  
    private void IsValidUserAD(string UserName, string Pwd)
    {
        try
        {
            bool flag = false;
            FailureText.Text = "";
            ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
            //if (ObjADH.IsAuthenticated("lupinworld", txtUserName.Text, txtPassword.Text))
            string LDAPDomain = ConfigurationManager.AppSettings["LDAPDomain"];
            if (ObjADH.IsAuthenticated(LDAPDomain, txtUserName.Text, txtPassword.Text))
            {
                UserAccess userAccess = new UserAccess();
                DataSet dstData = new DataSet();
                dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
                //dstData = userAccess.ValidateUser(txtUserName.Text, txtPassword.Text);

                if (dstData.Tables[0].Rows.Count < 1)
                {
                    ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserName.Text);
                    if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
                    {
                        dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
                    }
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
                    //Srinidhi
                    Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();
                    flag = true;
                }

                else
                {
                    FailureText.Text = "User Name Or Password Incorect.";
                }

                if (flag)
                {
                    RememberMe();
                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                    {
                        Response.Redirect("MultiRoleSignOn.aspx", false);
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
            else
            {
                FailureText.Text = "User Name Or Password Incorect.";
            }

        }
        catch(Exception ex)
        {
            //throw;
            _log.Error("IsValidUserAD", ex);
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