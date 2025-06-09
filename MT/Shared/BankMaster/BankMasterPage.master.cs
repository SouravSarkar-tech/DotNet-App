using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.Security;
using System.Transactions;
using log4net;
public partial class Shared_Vendor_BankMasterPage : System.Web.UI.MasterPage
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

        if (IsValidURL(SafeTypeHandling.ConvertToString(HttpContext.Current.Request.UrlReferrer)) == false)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            if ((Session[StaticKeys.LoggedIn_User_Id] != null) && (Session[StaticKeys.MasterHeaderId] != null))
            {

                lblRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
                lblMaterialNo.Text = Session[StaticKeys.MaterialNo].ToString();
                //lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

                lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
                lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                if (!IsPostBack)
                {
                    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    
                    
                }
                //ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
            }
            else
            {
                Response.Redirect("../../login.aspx");
            }
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    public bool IsValidURL(string url)
    {
        string str = "";
        try { 
        if ((url != null) && (url != string.Empty))
        {
            string[] strArray = url.Substring(0, url.ToString().Length - 1).Split(new char[] { '/' });
            str = strArray[strArray.Length - 1];
        }
        }
        catch (Exception ex)
        { _log.Error("IsValidURL", ex); }
        return (str != string.Empty);
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        try {
        Session[StaticKeys.LoggedIn_User_Id] = "";
        Session.Remove(StaticKeys.LoggedIn_User_Id);
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
            //Response.Redirect("../login.aspx?flgError=X");
            Response.Redirect("../login.aspx?flgError=X", false);
        }
        catch (Exception ex)
        { _log.Error("lnkLogout_Click", ex); }
    }

    private string GetMenu(string user_ID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        SqlCommand cmd;
        try
        {
            objDal.OpenConnection(this.Page);
            cmd = new SqlCommand("Proc_Read_User_Menus", objDal.cnnConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserId", user_ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dstMenu = new DataSet();
            dstMenu.DataSetName = "Menus";
            da.Fill(dstMenu, "Menu");


            DataRelation relation = new DataRelation("ParentChild", dstMenu.Tables["Menu"].Columns["MenuID"], dstMenu.Tables["Menu"].Columns["ParentID"], true)
            {
                Nested = true
            };

            dstMenu.Relations.Add(relation);
            return dstMenu.GetXml();
        }
        catch (Exception ex)
        {
            _log.Error("GetMenu", ex);
            throw ex;
        }
        finally
        {
            objDal.CloseConnection(objDal.cnnConnection);
            objDal = null;
        }
    }
   
}
