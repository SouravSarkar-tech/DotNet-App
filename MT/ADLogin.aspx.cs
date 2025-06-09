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
using log4net;

public partial class Login : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
        {
            
            if (Request.Cookies["ADUNameMWT"] != null)
            {
                if (Request.Cookies["ADUNameMWT"].Value != "")
                {
                    //IsValidADUser(Request.Cookies["ADUNameMWT"].Value);
                    IsValidUser(Request.Cookies["ADUNameMWT"].Value, "mwt");
                }
                else
                {
                    Response.Redirect("PassLogin.aspx", false);
                }
            }
            else
            {
                Response.Redirect("PassLogin.aspx", false);
            }
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    private void IsValidADUser(string UserName)
    {
        try
        {
            bool flag = false;
            ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
            
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            dstData = userAccess.ValidateUser(UserName, "mwt");


            if (dstData.Tables[0].Rows.Count < 1)
            {
                ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(UserName);
                if (ObjADUser.LoginName != "")
                {
                    if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
                    {
                        dstData = userAccess.ValidateUser(UserName, "mwt");
                    }
                }
            }

            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
                //Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
                //Srinidhi
                Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();
                flag = true;
            }
            else
            {
                Response.Redirect("PassLogin.aspx", false);
            }

            
            if (flag)
            {
                Response.Redirect("Shared/Home.aspx", false);
            }
        }
        catch(Exception ex)
        {
            //throw;
            _log.Error("IsValidADUser", ex);
        }
    }

    private void IsValidUser(string UserName, string Pwd)
    {
        try
        {

            string _userName = UserName;
            if (Session[StaticKeys.AddAlertMsg] != null)
            {
                _userName += Session[StaticKeys.AddAlertMsg].ToString();
                Session[StaticKeys.AddAlertMsg] = null;
            }


            bool flag = false;
            
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            dstData = userAccess.ValidateUser(_userName, Pwd);

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
                return;
            }
        }
        catch(Exception ex)
        {
            //throw;
            _log.Error("IsValidADUser1", ex);
        }
    }

    private void IsValidUserAD(string UserName, string Pwd)
    {
        try
        {
            bool flag = false;
            ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();

            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            dstData = userAccess.ValidateUser(UserName, Pwd);

            if (dstData.Tables[0].Rows.Count < 1)
            {
                ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(UserName);
                if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
                {
                    dstData = userAccess.ValidateUser(UserName, Pwd);
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

            if (flag)
            {
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                {
                    Response.Redirect("MultiRoleSignOn.aspx", false);
                }
                else
                {
                    Response.Redirect("Shared/Home.aspx", false);
                }

                //Response.Redirect("Shared/Home.aspx", false);
            }
            else
            {
                return;
            }
        }
        catch(Exception ex)
        {
            //throw;
            _log.Error("IsValidUserAD", ex);
        }
    }


}