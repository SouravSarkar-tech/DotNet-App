using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Shared_Common_homets : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try { 
        string Path = ConfigurationManager.AppSettings["TSPathURL"];
            Path = Path + "/Login.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&unm=" + Session[StaticKeys.LoggedIn_User_Name].ToString() + "&pid=" + Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() + "&did=" + Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            Response.Redirect(Path, false);
        }
        catch (Exception ex) { _log.Error("Page_Load", ex); }
    }

    protected void btnYES_Click(object sender, EventArgs e)
    {
        try
        {
            //Session["sRole"] = ddlDepartment.SelectedValue;
            //Response.Redirect("CreateTicket.aspx", false);

            string StrRedirect = Convert.ToString(ConfigurationManager.AppSettings["StrRedirect"]);
            Response.Redirect(StrRedirect, false);
        }
        catch (Exception ex) { _log.Error("btnYES_Click", ex); }
    }

    protected void btnNO_Click(object sender, EventArgs e)
    {
        try
        {
            //Session["sRole"] = ddlDepartment.SelectedValue;
            Response.Redirect("home.aspx", false);

            //string StrRedirect = Convert.ToString(ConfigurationManager.AppSettings["StrRedirect"]);
            //Response.Redirect(StrRedirect, false);
        }
        catch (Exception ex) { _log.Error("btnNO_Click", ex); }
    }
}