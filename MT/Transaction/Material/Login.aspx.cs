using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Transaction_Login : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        Response.Redirect("../../login.aspx?flgError=X");
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
}