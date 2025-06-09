using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Collections;
using System.Configuration;
using log4net;
public partial class Shared_Common_MainMaster : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            xmlDataSource.Data = GetMenu(Session[StaticKeys.LoggedIn_User_Id].ToString());
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
        try
        {
            if (!IsPostBack)
            {
                lblUserName.Text = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                lblProfile.Text = Session[StaticKeys.LoggedIn_User_Profile].ToString();
                //lblDate.Text = DateTime.Now.ToShortDateString();
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
                {
                    lnkChangeRole.Visible = true;
                }
                else
                {
                    lnkChangeRole.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load1", ex); }
        try
        {
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
            {
                lnkVendorCount();
                lnkMaterialCount();
                lnkCustomerCount();
                Td1.Attributes.Add("style", "display:inline-block; color:red;");


            }
            else
            {
                Td1.Attributes.Add("style", "display:none");
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load2", ex); }
    }

    private string GetMenu(string user_ID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        SqlCommand cmd;
        try
        {
            objDal.OpenConnection(this.Page);
            cmd = new SqlCommand("pr_Get_MenuList_By_UserId", objDal.cnnConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@User_Id", user_ID));

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

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        try
        {
            Session[StaticKeys.LoggedIn_User_Id] = "";
            Session.Remove(StaticKeys.LoggedIn_User_Id);
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
            //Response.Redirect("../login.aspx");
            Response.Redirect("../login.aspx?flgError=X", false);
        }
        catch (Exception ex)
        { _log.Error("lnkLogout_Click", ex); }
    }
    protected void lnkChangeRole_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../MultiRoleSignOn.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("lnkChangeRole_Click", ex); }
    }

    /// <summary>
    /// Added by Swati 
    /// link for global count for master cell team
    /// </summary>
    protected void lnkVendorCount()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = getGlobalVendorCount(Session[StaticKeys.LoggedIn_User_Id].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                lnkVendor.Text = "Vendor(" + ds.Tables[0].Rows[0]["count"].ToString() + ") ";
            }
        }
        catch (Exception ex)
        { _log.Error("lnkVendorCount", ex); }
    }

    protected void lnkMaterialCount()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = getGlobalMaterialCount(Session[StaticKeys.LoggedIn_User_Id].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                lnkMaterial.Text = "Material(" + ds.Tables[0].Rows[0]["count"].ToString() + ") ";
            }
        }
        catch (Exception ex)
        { _log.Error("lnkMaterialCount", ex); }
    }

    protected void lnkCustomerCount()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = getGlobalCustomerCount(Session[StaticKeys.LoggedIn_User_Id].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                lnkCustomer.Text = "Customer(" + ds.Tables[0].Rows[0]["count"].ToString() + ") ";
            }
        }
        catch (Exception ex)
        { _log.Error("lnkCustomerCount", ex); }
    }

    public DataSet getGlobalVendorCount(string UserID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Vendor_Count";
        DataSet ds;

        hashPara.Add("@UserID", UserID);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            return ds;
        }
        catch (Exception ex)
        {
            _log.Error("getGlobalVendorCount", ex);
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataSet getGlobalMaterialCount(string UserID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Count";
        DataSet ds;

        hashPara.Add("@UserID", UserID);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            return ds;
        }
        catch (Exception ex)
        {
            _log.Error("getGlobalMaterialCount", ex);
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    public DataSet getGlobalCustomerCount(string UserID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Customer_Count";
        DataSet ds;

        hashPara.Add("@UserID", UserID);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            return ds;
        }
        catch (Exception ex)
        {
            _log.Error("getGlobalCustomerCount", ex);
            throw ex;
        }
        finally
        {
            objDal = null;
        }

    }

    protected void A1_Click(object sender, EventArgs e)
    {

        try
        {
            //IsValidUser(Session[StaticKeys.LoggedIn_User_Name].ToString(), "aAyh7s4l2yg=", "3");
            bool flag = false;

            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();

            dstData = userAccess.ValidateGlobalUser(Session[StaticKeys.LoggedIn_User_Name].ToString(), "aAyh7s4l2yg=", "3");
            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                //Response.Redirect("http://www.mwtqasglobal.com/Shared/Home.aspx", false);
                //Response.Redirect("http://mwtprdglobal.lupin.com/transaction/Vendor/VendorMaster.aspx", false);
                //SDT17052019 Change By NR , Desc : Get page path  from web config
                string PathURL = ConfigurationManager.AppSettings["MainPathURL"];
                Response.Redirect(PathURL + "/transaction/Vendor/VendorMaster.aspx", false);
                //EDT17052019 Change By NR , Desc : 
            }
            else
            {

            }
        }
        catch (Exception ex)
        { _log.Error("A1_Click", ex); }
    }

    private void IsValidUser(string UserName, string Pwd, string Profile)
    {
        try
        {
            bool flag = false;

            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();

            dstData = userAccess.ValidateGlobalUser(UserName, Pwd, Profile);
            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                //Response.Redirect("http://www.mwtqasglobal.com/Shared/Home.aspx", false);
                //Response.Redirect("http://mwtprdglobal.lupin.com/transaction/Vendor/VendorMaster.aspx", false);
                //SDT17052019 Change By NR , Desc : Get page path  from web config
                string PathURL = ConfigurationManager.AppSettings["MainPathURL"];
                Response.Redirect(PathURL + "/transaction/Vendor/VendorMaster.aspx", false);
                //EDT17052019 Change By NR , Desc : 
            }
            else
            {

            }
        }
        catch
        {
            throw;
        }
    }

    //private void IsValidUserAD(string UserName, string Pwd)
    //{
    //    try
    //    {
    //        bool flag = false;
    //        FailureText.Text = "";
    //        ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
    //        //if (ObjADH.IsAuthenticated("lupinworld", txtUserName.Text, txtPassword.Text))
    //        if (ObjADH.IsAuthenticated("lupin", txtUserName.Text, txtPassword.Text))
    //        {
    //            UserAccess userAccess = new UserAccess();
    //            DataSet dstData = new DataSet();
    //            dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
    //            //dstData = userAccess.ValidateUser(txtUserName.Text, txtPassword.Text);

    //            if (dstData.Tables[0].Rows.Count < 1)
    //            {
    //                ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserName.Text);
    //                if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
    //                {
    //                    dstData = userAccess.ValidateUser(txtUserName.Text, "mwt");
    //                }
    //            }
    //            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
    //            if (dstData.Tables[0].Rows.Count > 0)
    //            {
    //                Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
    //                Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
    //                Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
    //                Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
    //                Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
    //                Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
    //                Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
    //                Session[StaticKeys.LoggedIn_User_Location] = dstData.Tables[0].Rows[0]["Location"].ToString();
    //                Session[StaticKeys.LoggedIn_User_ContactNo] = dstData.Tables[0].Rows[0]["ContactNo"].ToString();
    //                Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
    //                //Srinidhi
    //                Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();
    //                flag = true;
    //            }

    //            else
    //            {
    //                FailureText.Text = "User Name Or Password Incorect.";
    //            }

    //            if (flag)
    //            {
    //                RememberMe();
    //                //Email Redirection Start
    //                if (Session[StaticKeys.MasterHeaderId] != null)
    //                {
    //                    DataSet dstReqData = new DataSet();
    //                    dstReqData = userAccess.GetRequestorDetailsByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString(), Session[StaticKeys.LoggedIn_User_Id].ToString());
    //                    if (dstReqData.Tables[0].Rows.Count > 0)
    //                    {
    //                        Session[StaticKeys.RequestNo] = dstReqData.Tables[0].Rows[0]["Request_No"].ToString();
    //                        Session[StaticKeys.Requestor_User_Name] = dstReqData.Tables[0].Rows[0]["CreatedBy"].ToString();
    //                        Session[StaticKeys.Requestor_Location] = dstReqData.Tables[0].Rows[0]["Location"].ToString();
    //                        Session[StaticKeys.Requestor_ContactNo] = dstReqData.Tables[0].Rows[0]["ContactNo"].ToString();
    //                        Session[StaticKeys.ActionType] = dstReqData.Tables[0].Rows[0]["Action_Type"].ToString();
    //                        Session[StaticKeys.SelectedModuleId] = dstReqData.Tables[0].Rows[0]["Module_Id"].ToString();
    //                        Session[StaticKeys.Mode] = dstReqData.Tables[0].Rows[0]["Mode"].ToString();
    //                    }
    //                }
    //                //Email Redirection End
    //                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.RoleCount].ToString()) > 1)
    //                {
    //                    Response.Redirect("MultiRoleSignOn.aspx", false);
    //                }
    //                //Email Redirection Start
    //                else if (Session[StaticKeys.MstrType] == "SW")
    //                    Response.Redirect("Transaction/SoftwareApproval/SoftwareApproval.aspx", false);
    //                //Email Redirection End
    //                else
    //                {
    //                    Response.Redirect("Shared/Home.aspx", false);
    //                }
    //            }
    //            else
    //            {
    //                FailureText.Text = "error Occured";
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            FailureText.Text = "User Name Or Password Incorect.";
    //        }

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //private void RememberMe()
    //{
    //    if (chkRememberMe.Checked == true)
    //    {
    //        Response.Cookies["UNameMWT"].Value = txtUserName.Text;
    //        Response.Cookies["PWDMWT"].Value = txtPassword.Text;
    //        Response.Cookies["UNameMWT"].Expires = DateTime.Now.AddMonths(2);
    //        Response.Cookies["PWDMWT"].Expires = DateTime.Now.AddMonths(2);
    //    }
    //    else
    //    {
    //        Response.Cookies["UNameMWT"].Expires = DateTime.Now.AddMonths(-1);
    //        Response.Cookies["PWDMWT"].Expires = DateTime.Now.AddMonths(-1);
    //    }
    //}

    protected void lnkVendor_Click(object sender, EventArgs e)
    {
        try
        {
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            string UserName = Session[StaticKeys.LoggedIn_User_Name].ToString();
            dstData = userAccess.ValidateGlobalUser(UserName, "aAyh7s4l2yg=", "3");
            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                //Response.Redirect("http://mwtqasglobal.lupin.com/transaction/Vendor/VendorMaster.aspx", false);
                string Path = ConfigurationManager.AppSettings["GlobalPath"];
                Response.Redirect(Path + "/transaction/Vendor/VendorMaster.aspx?UserName=" + UserName, false);
            }
            else
            {

            }
        }
        catch (Exception ex)
        { _log.Error("lnkVendor_Click", ex); }
    }

    protected void lnkMaterial_Click(object sender, EventArgs e)
    {
        try
        {
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            string UserName = Session[StaticKeys.LoggedIn_User_Name].ToString();
            dstData = userAccess.ValidateGlobalUser(Session[StaticKeys.LoggedIn_User_Name].ToString(), "aAyh7s4l2yg=", "3");
            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                string Path = ConfigurationManager.AppSettings["GlobalPath"];
                Response.Redirect(Path + "/transaction/Material/MaterialMaster.aspx?UserName=" + UserName, false);
            }
            else
            {

            }
        }
        catch (Exception ex)
        { _log.Error("lnkMaterial_Click", ex); }
    }

    protected void lnkCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            string UserName = Session[StaticKeys.LoggedIn_User_Name].ToString();
            dstData = userAccess.ValidateGlobalUser(Session[StaticKeys.LoggedIn_User_Name].ToString(), "aAyh7s4l2yg=", "3");
            Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                string Path = ConfigurationManager.AppSettings["GlobalPath"];
                Response.Redirect(Path + "/transaction/Customer/CustomerMaster.aspx?UserName=" + UserName, false);
            }
            else
            {

            }
        }
        catch (Exception ex)
        { _log.Error("lnkCustomer_Click", ex); }
    }
    ////end
}
