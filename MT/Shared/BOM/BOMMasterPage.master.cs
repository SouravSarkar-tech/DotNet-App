using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Shared_BOM_BOMMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try { 
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly. 
        // All webpages displaying an ASP.NET menu control must inherit this class. 
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
        }
        catch (Exception ex)
        { _log.Error("Page_PreInit", ex); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try {
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        if ((Session[StaticKeys.LoggedIn_User_Id] != null) && (Session[StaticKeys.MasterHeaderId] != null))
        {
            lnkRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
            lnkRequestNo.Attributes.Add("OnClientClick", "OpenRequestHistory('" + Session[StaticKeys.MasterHeaderId].ToString() + "','0');");

            //lblMaterialNo.Text = Session[StaticKeys.MaterialNo].ToString();
            lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

            lblPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();
            //lblStorageLocation.Text = Session[StaticKeys.MatStorageLocationName].ToString();

            lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
            lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
            lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

            string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            string moduleId = Session[StaticKeys.SelectedModuleId].ToString();

            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                lblMassRequestProcessId.Text = Session[StaticKeys.MassRequestProcessId].ToString();
                //ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);

                string mode = Session[StaticKeys.Mode].ToString();
            }

        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
}
