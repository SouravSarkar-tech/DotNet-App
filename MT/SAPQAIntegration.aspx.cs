using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SAPQAIntegration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hyper.Text = "Post";

        //main
        //hyper.NavigateUrl = "http://localhost/SAPIntegration/SAPQAIntegration.aspx>mid=" + Session[StaticKeys.MasterHeaderId].ToString() + "&uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&usrName=" + Session[StaticKeys.SAPUserName].ToString() + "&pwd" + Session[StaticKeys.SAPPassword].ToString();
        hyper.NavigateUrl = "http://localhost/SAPIntegration/SAPQAIntegration.aspx>mid=" + Session[StaticKeys.MasterHeaderId].ToString() + "&uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() ;

        //hyper.NavigateUrl = "E:/MWT Development/Project/SAPIntegration/SAPIntegrate/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();
       
    }
}