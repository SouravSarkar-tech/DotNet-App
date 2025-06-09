using Accenture.MWT.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class ProsolRedirect : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    BasicDataAccess basicDataAccess = new BasicDataAccess();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                try
                {
                    RedirectToMainPage();
                }
                catch (Exception ex)
                {
                    _log.Error("Page_Load", ex);
                    Response.Redirect("PassLogin.aspx", false);
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    private void RedirectToMainPage()
    {
        string sProsolID = "0";
        string sFlag = "0";
        try
        {
            if (Request.QueryString["sProsolID"] != null)
            {
                sProsolID = Request.QueryString["sProsolID"].ToString();
            }
            else
            {
                sProsolID = "0";
            }
            if (Request.QueryString["sFlag"] != null)
            {
                sFlag = Request.QueryString["sFlag"].ToString();
            }
            else
            {
                sFlag = "0";
            }

            lblmsg.Text = sProsolID + '-' + sFlag;

            string sdate = "";
            try
            {
                DateTime date = System.DateTime.Now;
                sdate = date.ToString("dd/MM/yyyy");
                sdate = sdate.Replace(@"/", "");
                WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "Start of execution Prosol");

            }
            catch (Exception ex)
            {
                _log.Error("RedirectToMainPage", ex);
            }

            if (sProsolID != "0" && sFlag != "0")
            {
                UserAccess userAccess = new UserAccess();
                DataSet dstData = new DataSet();


                DataSet ds3 = basicDataAccess.GetMWTID(Convert.ToString(sProsolID));
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    Session[StaticKeys.MWTRequestNo_Prosol] = Convert.ToString(ds3.Tables[0].Rows[0]["sMWTRequestNo"]);
                    Session[StaticKeys.LoggedIn_User_Prosol] = Convert.ToString(ds3.Tables[0].Rows[0]["sUsername"]);
                    try
                    {
                        WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "Start of execution SSO");
                        if (Convert.ToString(ds3.Tables[0].Rows[0]["sUsername"]) == (Convert.ToString(HttpContext.Current.User.Identity.Name.ToString().Substring(
                      (HttpContext.Current.User.Identity.Name.ToString().IndexOf("\\")) + 1))))
                        {

                            WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "Msg :" + HttpContext.Current.User.Identity.Name.ToString().Substring(
                            (HttpContext.Current.User.Identity.Name.ToString().IndexOf("\\")) + 1) + "");
                            Session[StaticKeys.LoggedIn_User_Prosol] = "";
                            Session[StaticKeys.LoggedIn_User_Prosol] = HttpContext.Current.User.Identity.Name.ToString().Substring(
                          (HttpContext.Current.User.Identity.Name.ToString().IndexOf("\\")) + 1);
                        }
                        else
                        {
                            WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "Msg : NoSSO");
                        }
                        WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "End of execution SSO");
                    }
                    catch (Exception ex) { _log.Error("RedirectToMainPage1", ex); }

                }
                if (sFlag == "SPOC")
                {
                    WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "Msg :" + Convert.ToString(Session[StaticKeys.LoggedIn_User_Prosol]) + "_" + lblmsg.Text + "");

                    dstData = userAccess.ValidateUserProsol(Convert.ToString(Session[StaticKeys.LoggedIn_User_Prosol]), "aAyh7s4l2yg=", "5");
                    Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
                    if (dstData.Tables[0].Rows.Count > 0)
                    {
                        Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                        Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                        Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
                        Session[StaticKeys.LoggedIn_User_Location] = dstData.Tables[0].Rows[0]["Location"].ToString();
                        Session[StaticKeys.LoggedIn_User_ContactNo] = dstData.Tables[0].Rows[0]["ContactNo"].ToString();
                        Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
                        Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();
                        RedirectToBasicData();
                    }
                }
                else
                {
                    WriteProsoRedirectLog("CreateRedirectLog_" + sdate + ".txt", "Msg_2 :" + Convert.ToString(Session[StaticKeys.LoggedIn_User_Prosol]) + "_" + lblmsg.Text + "");

                    dstData = userAccess.ValidateUserProsol(Convert.ToString(Session[StaticKeys.LoggedIn_User_Prosol]), "aAyh7s4l2yg=", "2");
                    Session[StaticKeys.RoleCount] = dstData.Tables[0].Rows.Count.ToString();
                    if (dstData.Tables[0].Rows.Count > 0)
                    {
                        Session[StaticKeys.LoggedIn_User_Id] = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile_Id] = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                        Session[StaticKeys.LoggedIn_User_Profile] = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_FullName] = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                        Session[StaticKeys.LoggedIn_User_LastLogin] = dstData.Tables[0].Rows[0]["Last_Login_On"].ToString();
                        Session[StaticKeys.LoggedIn_User_DeptId] = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                        Session[StaticKeys.IsLocationReq] = dstData.Tables[0].Rows[0]["IsLocationReq"].ToString();
                        Session[StaticKeys.LoggedIn_User_Location] = dstData.Tables[0].Rows[0]["Location"].ToString();
                        Session[StaticKeys.LoggedIn_User_ContactNo] = dstData.Tables[0].Rows[0]["ContactNo"].ToString();
                        Session[StaticKeys.LoggedIn_User_Name] = dstData.Tables[0].Rows[0]["UserName"].ToString();
                        Session[StaticKeys.LoggedIn_User_DeptName] = dstData.Tables[0].Rows[0]["Department_Name"].ToString();
                        try
                        {
                            RedirectToBasicData();
                        }
                        catch (Exception ex)
                        {
                            _log.Error("RedirectToMainPage2", ex);
                            Response.Redirect("PassLogin.aspx", false);
                        }
                    }
                }

                WriteProsoRedirectLog("CreateSSOLog_" + sdate + ".txt", "End of execution Prosol");
            }
            else
            {
                Response.Redirect("PassLogin.aspx", false);
            }
        }
        catch (Exception ex)
        {
            _log.Error("RedirectToMainPage3", ex);
            Response.Redirect("PassLogin.aspx", false);
        }
    }


    public void WriteProsoRedirectLog(string strFileName, string strMessage)
    {
        try
        {
            //Path.GetTempPath()
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ProsolLog", strFileName), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
            objStreamWriter.Close();
            objFilestream.Close();
            //return true;  
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    private void RedirectToBasicData()
    {

        lblPk.Text = Convert.ToString(Session[StaticKeys.MWTRequestNo_Prosol]);
        if (lblPk.Text != "")
        {
            DataSet dsd = basicDataAccess.GetMWTDetails(Convert.ToInt32(lblPk.Text));
            if (dsd.Tables[0].Rows.Count > 0)
            {
                try
                {
                    Session[StaticKeys.SelectedModuleId] = Convert.ToString(dsd.Tables[0].Rows[0]["Module_Id"]);
                    Session[StaticKeys.SelectedModule] = Convert.ToString(dsd.Tables[0].Rows[0]["Module_Name"]);
                    Session[StaticKeys.RequestNo] = Convert.ToString(dsd.Tables[0].Rows[0]["Request_No"]);
                    Session[StaticKeys.MassRequestProcessId] = Convert.ToString(dsd.Tables[0].Rows[0]["Mass_Request_Process_Id"]);
                    Session[StaticKeys.MaterialNo] = (Convert.ToString(dsd.Tables[0].Rows[0]["Material_Number"]) == "" ? "New Request" : Convert.ToString(dsd.Tables[0].Rows[0]["Material_Number"])) + " - " + Convert.ToString(dsd.Tables[0].Rows[0]["Material_Short_Description"]);
                    Session[StaticKeys.ActionType] = Convert.ToString(dsd.Tables[0].Rows[0]["Action_Type"]);
                    Session[StaticKeys.MaterialPlantId] = Convert.ToString(dsd.Tables[0].Rows[0]["Mat_Plant_Id"]);
                    Session[StaticKeys.MatStorageLocationId] = Convert.ToString(dsd.Tables[0].Rows[0]["Storage_Location"]);
                    Session[StaticKeys.MatPurchasingGroupId] = Convert.ToString(dsd.Tables[0].Rows[0]["Purchasing_Group"]);
                    Session[StaticKeys.MatPlantGrp] = Convert.ToString(dsd.Tables[0].Rows[0]["Plant_Group_Id"]);
                    Session[StaticKeys.MaterialPlantName] = Convert.ToString(dsd.Tables[0].Rows[0]["Plant_Name"]);
                    Session[StaticKeys.MatStorageLocationName] = Convert.ToString(dsd.Tables[0].Rows[0]["Storage_Location_Name"]);
                    Session[StaticKeys.MaterialProcessModuleId] = Convert.ToString(dsd.Tables[0].Rows[0]["Ref_Module_Id"]);
                    Session[StaticKeys.Requestor_User_Name] = Convert.ToString(dsd.Tables[0].Rows[0]["Created_By"]);
                    Session[StaticKeys.Requestor_Location] = Convert.ToString(dsd.Tables[0].Rows[0]["Location"]);
                    Session[StaticKeys.Requestor_ContactNo] = Convert.ToString(dsd.Tables[0].Rows[0]["ContactNo"]);
                    Session[StaticKeys.MaterialSalesOrgId] = Convert.ToString(dsd.Tables[0].Rows[0]["SalesOrg"]);
                    Session[StaticKeys.MaterialDistChnlId] = Convert.ToString(dsd.Tables[0].Rows[0]["DistChnl"]);
                    Session[StaticKeys.MarketType] = Convert.ToString(dsd.Tables[0].Rows[0]["RequestType"]);

                    Session[StaticKeys.MasterHeaderId] = lblPk.Text;
                    Session[StaticKeys.Mode] = "M";
                    Session[StaticKeys.MaterialType] = "";
                    //Response.Redirect("basicdata1.aspx");
                    //Response.Redirect("transaction/Material/basicdata1.aspx");
                    Response.Redirect("transaction/Material/basicdata1.aspx", false);
                }
                catch (Exception ex) { _log.Error("RedirectToBasicData", ex); }
            }
            else
            {
                Response.Redirect("PassLogin.aspx", false);
            }
        }
        else
        {
            Response.Redirect("PassLogin.aspx", false);
        }
    }
}