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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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

    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        IsValidUser(txtUserName.Text, txtPassword.Text);
    }

    private void IsValidUser(string UserName,string Pwd)
    {
        try
        {
            bool flag = false;
            FailureText.Text = "";
            ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
            //if (ObjADH.IsAuthenticated("lupinworld", txtUserName.Text, txtPassword.Text))
            //{
                UserAccess userAccess = new UserAccess();
                DataSet dstData = new DataSet();
                //dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
                dstData = userAccess.ValidateUser(txtUserName.Text, txtPassword.Text);

                //if (dstData.Tables[0].Rows.Count < 1)
                //{
                //    ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserName.Text);
                //    if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
                //    {
                //        dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
                //    }
                //}
                Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                    Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                    Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                    Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                    Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                    Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
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
                //else
                //{
                //    FailureText.Text = "error Occured";
                //    return;
                //}
            //}
            //else
            //{
            //    FailureText.Text = "User Name Or Password Incorect.";
            //}

        }
        catch
        {
            throw;
        }
    }


    #region Private Methods

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

    /// <summary>
    /// Sets the focus.
    /// </summary>
    private void SetFocus()
    {
        txtUserName.Focus();
    }
    #endregion
}