using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("../login.aspx?flgError=X");
        Response.Redirect("../login.aspx?flgError=X", false);
    }
}