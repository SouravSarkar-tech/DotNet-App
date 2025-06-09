using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_BOMRecipe_SAPLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        Session[StaticKeys.SAPUserName] = txtUserName.Text;
        Session[StaticKeys.SAPPassword] = txtPassword.Text;
    }
}