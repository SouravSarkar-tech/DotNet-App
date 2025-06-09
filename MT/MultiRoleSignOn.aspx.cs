using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using log4net;

public partial class MultiRoleSignOn : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();

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

            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    FillForm();
                }
            }
            else
            {
                Response.Redirect("login.aspx",false);
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    private void FillForm()
    {
        try
        {
            txtUserName.Text = Session[StaticKeys.LoggedIn_User_Name].ToString();
            helperAccess.PopuplateDropDownList(ddlRole, "pr_GetUserRoles '" + Session[StaticKeys.LoggedIn_User_Name].ToString() + "'", "DeptName", "DeptID", "");
        }
        catch (Exception ex)
        { _log.Error("FillForm", ex); }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            //bool flag = false;
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();

            dstData = userAccess.ValidateUser(Session[StaticKeys.LoggedIn_User_Name].ToString(), "mwt");

            if (dstData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dstData.Tables[0].Rows)
                {
                    if (dr["Department_Id"].ToString() == ddlRole.SelectedValue.ToString())
                    {
                        Session[StaticKeys.LoggedIn_User_Id] = dr["User_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile_Id] = dr["Profile_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile] = dr["Profile_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_FullName] = dr["Full_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_LastLogin] = dr["Last_Login_On"].ToString();
                        Session[StaticKeys.LoggedIn_User_DeptId] = dr["Department_Id"].ToString();
                        Session[StaticKeys.IsLocationReq] = dr["IsLocationReq"].ToString();
                        Session[StaticKeys.LoggedIn_User_Location] = dr["Location"].ToString();
                        Session[StaticKeys.LoggedIn_User_ContactNo] = dr["ContactNo"].ToString();
                        Session[StaticKeys.LoggedIn_User_Name] = dr["UserName"].ToString();
                        //Srinidhi
                        Session[StaticKeys.LoggedIn_User_DeptName] = dr["Department_Name"].ToString();
                    }
                }

                //flag = true;
            }
            //Email Redirection Start
            //if (Session[StaticKeys.MstrType].ToString() == "SW")
            //    Response.Redirect("Transaction/SoftwareApproval/SoftwareApproval.aspx", false);
            //else
            //Email Redirection End
            Response.Redirect("Shared/Home.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnContinue", ex); }
    }

    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();

            dstData = userAccess.ValidateUser(Session[StaticKeys.LoggedIn_User_Name].ToString(), "mwt");

            if (dstData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dstData.Tables[0].Rows)
                {
                    if (dr["Department_Id"].ToString() == ddlRole.SelectedValue.ToString())
                    {
                        Session[StaticKeys.LoggedIn_User_Id] = dr["User_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile_Id] = dr["Profile_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile] = dr["Profile_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_FullName] = dr["Full_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_LastLogin] = dr["Last_Login_On"].ToString();
                        Session[StaticKeys.LoggedIn_User_DeptId] = dr["Department_Id"].ToString();
                        Session[StaticKeys.IsLocationReq] = dr["IsLocationReq"].ToString();
                        Session[StaticKeys.LoggedIn_User_Location] = dr["Location"].ToString();
                        Session[StaticKeys.LoggedIn_User_ContactNo] = dr["ContactNo"].ToString();
                        Session[StaticKeys.LoggedIn_User_Name] = dr["UserName"].ToString();
                        //Srinidhi
                        Session[StaticKeys.LoggedIn_User_DeptName] = dr["Department_Name"].ToString();
                    }
                }

                //flag = true;
            }
            //Email Redirection Start
            //if (Session[StaticKeys.MstrType].ToString() == "SW")
            //    Response.Redirect("Transaction/SoftwareApproval/SoftwareApproval.aspx", false);
            //else
            //Email Redirection End
            Response.Redirect("Shared/Home.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("imgBtnLogin", ex); }
    }
}