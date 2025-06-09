using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _DefaultReq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[StaticKeys.AddAlertMsg] = "U";
        Response.Redirect("Login/Login.aspx");
    }
}