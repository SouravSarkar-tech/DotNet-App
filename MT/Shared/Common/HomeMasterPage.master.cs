using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using System.Data.SqlClient;
using System.Data;
using Accenture.MWT.DomainObject;
using System.Web.UI.HtmlControls;
using log4net;
public partial class Shared_MasterPage : System.Web.UI.MasterPage
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



        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {

                HelperAccess helperAccess = new HelperAccess();
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "0");

                ddlPlant.Items.Add(new ListItem("PROJ-FML", "99999"));
                ddlPlant.Items.Add(new ListItem("PROJ-API", "99998"));
                ddlPlant.Items.Add(new ListItem("HO - IT", "99997"));
                //srinidhi
                //ddlPlant.Items.Add(new ListItem("Pune - FG/SFG", "99996"));
                ReadDepartments();
                UserAccess ObjUserAccess = new UserAccess();
                User_Master ObjUser_Master = ObjUserAccess.GetUserDataByUserId(Session[StaticKeys.LoggedIn_User_Id].ToString());

                txtUserFirstName.Text = ObjUser_Master.Full_Name;
                txtEmailId.Text = ObjUser_Master.EmailId;
                txtLocation.Text = ObjUser_Master.Location;
                txtCostCenter.Text = ObjUser_Master.Cost_Centre;
                txtMobileNum.Text = ObjUser_Master.ContactNo;
                ddlPlant.SelectedValue = ObjUser_Master.Plant_Id;
                ddlDepartment.SelectedValue = ObjUser_Master.Department_Name;

                ////ClientScript.RegisterStartupScript("onload", "ShowChangeDialog()");
                ////Page.RegisterStartupScript("onload", "ShowChangeDialog()");
                if (Session[StaticKeys.IsLocationReq].ToString() == "1")
                {
                    HtmlGenericControl pagebody = (HtmlGenericControl)Master.FindControl("pagebody");
                    pagebody.Attributes.Add("onload", "ShowChangeDialog()");
                }
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


    /// <summary>
    /// 536453 
    /// </summary>
    /// <param name="UserName"></param>
    private bool IsValidUserEmail()
    {
        bool flag = false;

        ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
        if (Session[StaticKeys.LoggedIn_User_Name].ToString() != "")
        {
            try
            {
                ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(Session[StaticKeys.LoggedIn_User_Name].ToString().Trim());

                if (ObjADUser != null)
                {
                    if (ObjADUser.EmailAddress != null)
                    {
                        if (ObjADUser.EmailAddress == txtEmailId.Text.Trim())
                        {
                            flag = true;
                        }
                        else if (ObjADUser.EmailAddress != txtEmailId.Text.Trim() && Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "3" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        return flag;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UserAccess ObjUserAccess = new UserAccess();

        Utility objUtil = new Utility();

        User_Master ObjUser_Master = new User_Master();
         
        if (IsValidUserEmail())
        { 
            try
            {
                ObjUser_Master.User_Id = Convert.ToInt32(Session[StaticKeys.LoggedIn_User_Id].ToString());

        ObjUser_Master.Full_Name = txtUserFirstName.Text;
        ObjUser_Master.EmailId = txtEmailId.Text;
        ObjUser_Master.Location = txtLocation.Text;
        ObjUser_Master.Cost_Centre = txtCostCenter.Text;
        ObjUser_Master.ContactNo = txtMobileNum.Text;
        ObjUser_Master.Plant_Id = ddlPlant.SelectedValue;
        ObjUser_Master.UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
        ObjUser_Master.TodayDate = objUtil.GetDate();
        ObjUser_Master.IPAddress = objUtil.GetIpAddress();
        //Added new field for Department Name on 28/05/18
        ObjUser_Master.Department_Name = ddlDepartment.Text;
        //
        if (ObjUserAccess.SaveUserLocationDetails(ObjUser_Master) > 0)
        {
            Session[StaticKeys.IsLocationReq] = "0";
            Response.Redirect("Home.aspx", false);

                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            catch (Exception ex)
            { _log.Error("btnUpdate_Click", ex); }
        }
        else
        {
            lblMsg.Text = "Please enter your valid lupin EmailId";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
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

    private void ReadDepartments()
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            ddlDepartment.DataSource = userAccess.ReadDepartments();
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_Id";
            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadDepartments", ex);
            //throw ex;
        }
    }

}
