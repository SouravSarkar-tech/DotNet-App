using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Security;
using System.IO;
using log4net;
using System.Globalization;

public partial class Transaction_MaterialMaster : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //

    DataSet dstData = new DataSet();
    HelperAccess helperAccess = new HelperAccess();

    string sdate = "";
    #region Page Events
    //https://docs.microsoft.com/en-us/learn/modules/aspnet-logging/4-exercise-aspnet-log
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");

        }
        catch (Exception ex)
        {
            _log.Error("Page_Load1", ex);

        }
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                    PopulateDropDownList();
                    ReadModules();
                    ReadProfileWiseModules(userProfileId, lblUserId.Text);
                    if (Session[StaticKeys.MatTypeSelected] != null)
                        ddlModuleSearch.SelectedValue = Session[StaticKeys.MatTypeSelected].ToString();
                    ReadMaterialMasterRequests();
                    //Srinidhi
                    Session[StaticKeys.ApprovalNote] = "";
                }
                ShowHideBtn();
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    private void ShowHideBtn()
    {
        try
        {
            if (Convert.ToString(Session[StaticKeys.LoggedIn_User_Profile_Id]) == "2")
            {
                btnCreateNew.Attributes.Add("enabled", "enabled");
                btnChangeBulkRequestC.Enabled = true;
                btnChangeExtensionC.Enabled = true;
                btnBlockRequest.Enabled = true;
                btnCopyRequest.Enabled = true;
                //DEP_05102023 add start
                btnCreateDepo.Enabled = true;
                //DEP_05102023 add end
            }
            else
            {

                btnCreateNew.Attributes.Add("disabled", "disabled");
                btnChangeBulkRequestC.Enabled = false;
                btnChangeExtensionC.Enabled = false;
                btnBlockRequest.Enabled = false;
                btnCopyRequest.Enabled = false;
                //DEP_05102023 add start
                btnCreateDepo.Enabled = false;
                //DEP_05102023 add end
            }
        }
        catch (Exception ex)
        { _log.Error("ShowHideBtn", ex); }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        int retValue;

        try
        {
            //PROSOL_SDT16092019
            if (ddlModule.SelectedValue == "138")
            {
                //Response.Redirect("http://prosolqa.lupin.com?RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));

                string ProsolLink = Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"]);
                //var oCookie = FormsAuthentication.GetAuthCookie("LP", false);
                //FormsAuthenticationTicket oTicket = new FormsAuthenticationTicket(1, "LP", DateTime.Now, DateTime.Now.AddMinutes(60), true, Convert.ToString(Session["LoggedIn_User_Name"]) + "," + Convert.ToString(Session["LoggedIn_User_Pass"]));
                //string cookieStr = FormsAuthentication.Encrypt(oTicket);
                //oCookie.Value = cookieStr;
                //Response.Cookies.Add(oCookie);

                Response.Redirect(ProsolLink);
                //Response.Write("<script>");
                //Response.Write("window.open('"+ ProsolLink + "' ,'_blank')");
                //Response.Write("</script>");

                //Response.Redirect("http://prosolqa.lupin.com");
                //Response.Redirect(ProsolLink);
                //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl("http://prosolqa.lupin.com")));


                //btnNext.Attributes.Add("href", "" + Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"]));
                //btnNext.Attributes.Add("target", "_blank");

            }
            else
            {
                if (CheckedCreationFeilds())
                {
                    string mode = lblMode.Text;
                    if (trMarketType.Visible == true)
                        mode = ddlMarketType.SelectedValue;
                    if (trEmergency.Visible == true)
                        mode = chkEmergency.Checked ? "E" : lblMode.Text;
                    Session[StaticKeys.MarketType] = mode;

                    masterHeaderId = materialAccess.SaveMaterialHeader("0", ddlModule.SelectedValue, ddlPlantGroup.SelectedValue, lblUserId.Text, mode, ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, ddlPurchasingGroup.SelectedValue);
                    if (masterHeaderId > 0)
                    {
                        Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
                        Session[StaticKeys.MaterialPlantId] = ddlPlant.SelectedValue;
                        Session[StaticKeys.MatStorageLocationId] = ddlStorageLocation.SelectedValue;
                        Session[StaticKeys.MatPurchasingGroupId] = ddlPurchasingGroup.SelectedValue;

                        Session[StaticKeys.MaterialPlantName] = ddlPlant.SelectedItem.Text;
                        Session[StaticKeys.MatStorageLocationName] = ddlStorageLocation.SelectedItem.Text;

                        Session[StaticKeys.MatPlantGrp] = ddlPlantGroup.SelectedValue;
                        Session[StaticKeys.MassRequestProcessId] = "0";

                        Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
                        Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                        Session[StaticKeys.Mode] = "N";
                        Session[StaticKeys.ActionType] = "N";
                        Session[StaticKeys.MaterialNo] = "New Request";
                        Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                        Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                        Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                        Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                        //Check to save default data for ROH(Raw Material)
                        if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164)
                        {
                            string SalesOrgID, DistributionChannelID = "";
                            int ret;
                            materialAccess.SaveMaterialDefaultDataROH(masterHeaderId.ToString(), ddlPlant.SelectedValue, ddlPurchasingGroup.SelectedValue, ddlStorageLocation.SelectedValue, lblUserId.Text, out SalesOrgID, out DistributionChannelID, out ret);
                            if (ret > 0)
                            {
                                Session[StaticKeys.MaterialDistChnlId] = DistributionChannelID;
                                Session[StaticKeys.MaterialSalesOrgId] = SalesOrgID;
                                Response.Redirect("BasicData1.aspx", false);
                            }
                        }
                        else if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145)
                        {
                            string SalesOrgID, DistributionChannelID = "";
                            int ret;
                            materialAccess.SaveMaterialDefaultDataFGSFG(masterHeaderId.ToString(), ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, lblUserId.Text, out SalesOrgID, out DistributionChannelID, out ret);
                            if (ret > 0)
                            {
                                Session[StaticKeys.MaterialDistChnlId] = DistributionChannelID;
                                Session[StaticKeys.MaterialSalesOrgId] = SalesOrgID;
                                Response.Redirect("BasicData1.aspx", false);
                            }
                        }
                        else if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 195)
                        {
                            string SalesOrgID, DistributionChannelID = "";
                            int ret;
                            materialAccess.SaveMaterialDefaultDataPROM(masterHeaderId.ToString(), ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, lblUserId.Text, out SalesOrgID, out DistributionChannelID, out ret);
                            if (ret > 0)
                            {
                                Session[StaticKeys.MaterialDistChnlId] = DistributionChannelID;
                                Session[StaticKeys.MaterialSalesOrgId] = SalesOrgID;
                                Response.Redirect("BasicData1.aspx", false);
                            }
                        }
                        else if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 138 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 147 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 170)
                        {
                            string SalesOrgID, DistributionChannelID = "";
                            int ret;
                            materialAccess.SaveSalesDefaultERSHIBEZMBW(masterHeaderId.ToString(), ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, lblUserId.Text, out SalesOrgID, out DistributionChannelID, out ret);
                            if (ret > 0)
                            {
                                Session[StaticKeys.MaterialDistChnlId] = DistributionChannelID;
                                Session[StaticKeys.MaterialSalesOrgId] = SalesOrgID;
                                Response.Redirect("BasicData1.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("BasicData1.aspx", false);
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnNext_Click", ex);
            //throw ex;
        }

    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        try
        {
            int MasterHeaderId = objMasterAccess.GenerateChangeRequest(GetSelectedPkID(), lblUserId.Text);
            if (MasterHeaderId > 0)
            {

                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.SelectedModule] = objMasterAccess.mModuleName.ToString();
                Session[StaticKeys.MaterialNo] = objMasterAccess.mMasterSAPCode.ToString();
                Session[StaticKeys.MassRequestProcessId] = "0";
                Response.Redirect("basicdata1.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnChangeRequest_Click", ex); }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedValue != "ALL")
            {
                Session[StaticKeys.SearchStatus] = ddlStatus.SelectedValue;
            }
            else
            {
                Session[StaticKeys.SearchStatus] = null;
            }
            if (Session[StaticKeys.MatTypeSelected] != null)
            {
                ddlModuleSearch.SelectedValue = Session[StaticKeys.MatTypeSelected].ToString();
            }
            //bool svalistrue = false;
            //if(txtRequestNo.Text.Trim() != "" || txtSAPCode.Text.Trim() != "")
            //{
            //    svalistrue = false;
            //    reqtxtFromDate.Visible = false;
            //    reqtxtToDate.Visible = false;
            //}
            //else
            //{
            //    svalistrue = true;
            //    reqtxtFromDate.Visible = true;
            //    reqtxtToDate.Visible = true;
            //}
            //if(svalistrue == true)

            ReadMaterialMasterRequests();
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }

    /// <summary>
    /// /add 814364
    /// </summary>
    /// <returns></returns>
    private bool IsValidSearch()
    {
        bool flg = false;

        int diffOfDatesi = 0;
        if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
        {
            //string A1 = "";
            //string A2 = "";
            //string A3 = ""; string A4 = "";

            //_log.Info("Start");

            try
            {
                // Utility objUtil = new Utility();
                 var fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

               //  var fdate = objUtil.GetYYYYMMDD(txtFromDate.Text);
                //var fdate = Convert.ToDateTime(txtFromDate.Text);
               // _log.Info("fdate"+ fdate);
               // var tdate = Convert.ToDateTime(txtToDate.Text);
                //var tdate = objUtil.GetYYYYMMDD(txtToDate.Text);
                //var tdate = DateTime.ParseExact(Convert.ToString(txtToDate.Text), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

               // _log.Info("tdate" + tdate);
                var diffOfDates = (tdate - fdate).TotalDays;
                //_log.Info("diffOfDates" + diffOfDates);
                diffOfDatesi = Convert.ToInt32(diffOfDates);
               // _log.Info("End"); 
              //  A2 = diffOfDatesi.ToString();
              //  _log.Info("A2" + A2);
            }
            catch (Exception ex) {
                //   A3 = ex.Message.ToString();
                  _log.Error("Exception"+ ex.Message);
            }
            // _log.Info("A1" + A1 + "A2" + A2 + "A3" + A3 + "A4" + A4); 
        }


        if ((ddlStatus.SelectedValue == "P" || ddlStatus.SelectedValue == "R" || ddlStatus.SelectedValue == "SUB"))
        {
            flg = true;
        }
        else if ((txtRequestNo.Text.Trim() != "" || txtSAPCode.Text.Trim() != ""))
        {
            flg = true;
        }
        else if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "" && diffOfDatesi == 0)
        {
            flg = false;
        }
        else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && diffOfDatesi <= 90)
        {
            flg = true;
        }
        else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && diffOfDatesi >= 90)
        {
            flg = false;
        }
        return flg;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "V";
            lblPk.Text = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = lblPk.Text;
            //Session[StaticKeys.SelectedModuleId] = ddlModuleSearch.SelectedValue;
            //Session[StaticKeys.SelectedModule] = ddlModuleSearch.SelectedItem.Text;
            Session[StaticKeys.Mode] = "V";
            Session[StaticKeys.MaterialType] = "";
            //Session[StaticKeys.MaterialNo] = "";

            //if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            //    Response.Redirect("MaterialBlock.aspx");
            //else if (Session[StaticKeys.ActionType].ToString() == "C")
            //    Response.Redirect("MaterialChange.aspx");
            //else if (Session[StaticKeys.ActionType].ToString() == "E")
            //    Response.Redirect("MaterialExtension.aspx");
            //else if (Session[StaticKeys.ActionType].ToString() == "M")
            //    Response.Redirect("MaterialMassProcess.aspx");
            //else
            //    Response.Redirect("basicdata1.aspx");

            if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            { Response.Redirect("MaterialBlock.aspx", false); }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                Response.Redirect("MaterialChange.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "E")
            {
                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                if (!objAccess.IsSAPMASSintegrationChkAval(Session[StaticKeys.MasterHeaderId].ToString()))
                {
                    Response.Redirect("MaterialMassExtension.aspx", false);
                }
                else
                {
                    Response.Redirect("MaterialExtension.aspx", false);
                }
            }
            else if (Session[StaticKeys.ActionType].ToString() == "M")
            { Response.Redirect("MaterialMassProcess.aspx", false); }
            else
            {
                Response.Redirect("basicdata1.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnView_Click", ex); }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        int masterHeaderId;
        try
        {
            string ActionType = "";
            string sModuleId = "";

            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    sModuleId = lblModuleId.Text;
                    ActionType = lblActionType.Text;
                    break;
                }
            }
            //PROSOL_SDT16092019
            if (sModuleId == "138")
            {
                lblMsg.Text = "Copy Option not available for ERSA Request.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else
            { //PROSOL_SDT16092019
                if (ActionType == "N" || ActionType == "R")
                {
                    //string mode = chkCopyEmergency.Checked ? "E" : lblMode.Text;
                    string mode = lblMode.Text;
                    if (trMarketTypeCopy.Visible == true)
                        mode = ddlMarketTypeCopy.SelectedValue;
                    if (trCopyEmergency.Visible == true)
                        mode = chkCopyEmergency.Checked ? "E" : lblMode.Text;

                    masterHeaderId = ObjMasterAccess.GenerateCopyRequestM(GetSelectedPkID(), ddlModuleCopy.SelectedValue, ddlPlantGroupCopy.SelectedValue, lblUserId.Text, mode, ddlPlantCopy.SelectedValue, ddlStorageLocationCopy.SelectedValue, ddlPurchasingGroupCopy.SelectedValue);
                    //masterHeaderId = ObjMasterAccess.GenerateCopyRequest(GetSelectedPkID(), ddlCompany.SelectedValue, ddlVendorAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
                    if (masterHeaderId > 0)
                    {
                        Session[StaticKeys.SelectedModuleId] = ddlModuleCopy.SelectedValue;
                        Session[StaticKeys.MaterialPlantId] = ddlPlantCopy.SelectedValue;
                        Session[StaticKeys.MatStorageLocationId] = ddlStorageLocationCopy.SelectedValue;
                        Session[StaticKeys.MatPurchasingGroupId] = ddlPurchasingGroupCopy.SelectedValue;

                        Session[StaticKeys.MaterialPlantName] = ddlPlantCopy.SelectedItem.Text;
                        Session[StaticKeys.MatStorageLocationName] = ddlStorageLocationCopy.SelectedItem.Text;

                        Session[StaticKeys.MatPlantGrp] = ddlPlantGroupCopy.SelectedValue;
                        Session[StaticKeys.MassRequestProcessId] = "0";

                        Session[StaticKeys.SelectedModule] = ddlModuleCopy.SelectedItem.Text;
                        Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                        Session[StaticKeys.Mode] = "N";
                        Session[StaticKeys.ActionType] = "N";
                        Session[StaticKeys.MaterialNo] = "New Request";
                        Session[StaticKeys.RequestNo] = ObjMasterAccess.mRequestNo;

                        Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                        Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                        Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                        Response.Redirect("BasicData1.aspx", false);
                    }
                }
                else
                {
                    lblMsg.Text = "Copy Option only available for Create Request.";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnCopy_Click", ex);
            //throw ex;
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "M";
            lblPk.Text = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = lblPk.Text;
            //Session[StaticKeys.SelectedModuleId] = ddlModuleSearch.SelectedValue;
            //Session[StaticKeys.SelectedModule] = ddlModuleSearch.SelectedItem.Text;
            Session[StaticKeys.Mode] = "M";
            Session[StaticKeys.MaterialType] = "";
            //Session[StaticKeys.MaterialNo] = "";

            if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            { Response.Redirect("MaterialBlock.aspx", false); }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            { Response.Redirect("MaterialChange.aspx", false); }
            else if (Session[StaticKeys.ActionType].ToString() == "E")
            {
                //Response.Redirect("MaterialExtension.aspx");

                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                if (!objAccess.IsSAPMASSintegrationChkAval(Session[StaticKeys.MasterHeaderId].ToString()))
                {
                    Response.Redirect("MaterialMassExtension.aspx", false);
                }
                else
                {
                    Response.Redirect("MaterialExtension.aspx", false);
                }
            }
            else if (Session[StaticKeys.ActionType].ToString() == "M")
            { Response.Redirect("MaterialMassProcess.aspx", false); }
            else
            {
                Response.Redirect("basicdata1.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnModify_Click", ex); }
        //if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
        //    Response.Redirect("MaterialBlock.aspx");
        //else if (Session[StaticKeys.ActionType].ToString() == "C")
        //    Response.Redirect("MaterialChange.aspx");
        //else if (Session[StaticKeys.ActionType].ToString() == "E")
        //    Response.Redirect("MaterialExtension.aspx");
        //else if (Session[StaticKeys.ActionType].ToString() == "M")
        //    Response.Redirect("MaterialMassProcess.aspx");
        //else
        //    //   if (HelperAccess.ReqType == "SMC")
        //    //{
        //    //    try
        //    //    {
        //    //        //MSC_8300001775
        //    //        MasterAccess objMasterAccess1 = new MasterAccess();
        //    //        DataSet ds3 = objMasterAccess1.GetMWTURL(Convert.ToString(Session[StaticKeys.MasterHeaderId]));
        //    //        if (ds3.Tables[0].Rows.Count > 0)
        //    //        {
        //    //            Response.Redirect(Convert.ToString(ds3.Tables[0].Rows[0]["rURL"]));
        //    //        }
        //    //    }
        //    //    catch (Exception ex) { }
        //    //}
        //    //else
        //    //{
        //    //    Response.Redirect("basicdata1.aspx");
        //    //}
        //    ////MSC_8300001775
        //    Response.Redirect("basicdata1.aspx");

    }

    protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        ddlPlantGroupC.SelectedValue = "2";

        try
        {
            if (CheckedChangeFeilds())
            {
                string mode = lblMode.Text;
                if (trMarketTypeExt.Visible == true)
                    mode = ddlMarketTypeExt.SelectedValue;

                //int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequest("0", "MMC", lblUserId.Text, ddlPlantGroupC.SelectedValue, ddlPlantC.SelectedValue);
                int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequest("0", "MMC", lblUserId.Text, mode, ddlPlantGroupC.SelectedValue, ddlPlantC.SelectedValue, ddlModuleC.Text, ddlPurchasingGroupC.SelectedValue);
                if (MasterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupC.SelectedValue;
                    Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                    Session[StaticKeys.MaterialPlantId] = ddlPlantC.SelectedValue;
                    Session[StaticKeys.MaterialProcessModuleId] = ddlModuleC.SelectedValue;
                    Session[StaticKeys.MatStorageLocationId] = "";

                    Session[StaticKeys.MaterialPlantName] = ddlPlantC.SelectedItem.Text;
                    Session[StaticKeys.MatStorageLocationName] = "";

                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.MaterialType] = "";
                    Session[StaticKeys.ActionType] = "C";
                    Session[StaticKeys.MaterialNo] = "Bulk Request";
                    Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                    Session[StaticKeys.SelectedModule] = "Material Bulk Change";
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("MaterialChange.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnChangeBulkRequest_Click", ex);
            //throw ex;
        }
    }

    protected void btnChangeExtension_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();

        try
        {
            if (CheckedExtensionFeilds())
            {
                string mode = lblMode.Text;
                if (trMarketTypeExt.Visible == true)
                    mode = ddlMarketTypeExt.SelectedValue;

                int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequest("0", "MEXT", lblUserId.Text, mode, ddlPlantGroupC.SelectedValue, ddlPlantC.SelectedValue, ddlModuleC.Text, ddlPurchasingGroupC.SelectedValue);
                //int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestC("0", "CEXT", lblUserId.Text, "", ddlCustomerTypeE.SelectedValue, ddlSalesRegionE.SelectedValue);
                if (MasterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupC.SelectedValue;
                    Session[StaticKeys.MaterialProcessModuleId] = ddlModuleC.SelectedValue;

                    Session[StaticKeys.MaterialPlantName] = ddlPlantC.SelectedItem.Text;
                    Session[StaticKeys.MatPurchasingGroupId] = ddlPurchasingGroupC.SelectedValue;
                    Session[StaticKeys.MatStorageLocationName] = "";

                    Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.MaterialType] = "";
                    Session[StaticKeys.ActionType] = "E";
                    Session[StaticKeys.MaterialNo] = "Extension Request";
                    Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                    Session[StaticKeys.SelectedModule] = "Material Extension";
                    Session[StaticKeys.MassRequestProcessId] = "0";
                    Session[StaticKeys.MaterialPlantId] = ddlPlantC.SelectedValue;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("MaterialExtension.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnChangeExtension_Click", ex);
            //throw ex;
        }
    }

    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        try
        {

            if (CheckedBlockFeilds())
            {
                string Module = "173";
                MasterAccess objMasterAccess = new MasterAccess();
                int MasterHeaderId = objMasterAccess.GenerateBlockRequestM("0", ddlPlantGroupB.SelectedValue, Module, "MMB", lblUserId.Text, "", ddlModuleB.SelectedValue, "", ddlPlantB.SelectedValue);
                if (MasterHeaderId > 0)
                {

                    Session[StaticKeys.SelectedModuleId] = Module;
                    Session[StaticKeys.MaterialPlantId] = ddlPlantB.SelectedValue;
                    Session[StaticKeys.MaterialProcessModuleId] = ddlModuleB.SelectedValue;
                    Session[StaticKeys.MatStorageLocationId] = "";

                    Session[StaticKeys.MaterialPlantName] = ddlPlantB.SelectedItem.Text;
                    Session[StaticKeys.MatStorageLocationName] = "";

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupB.SelectedValue;
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = "Material Master Block";
                    Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "B";
                    Session[StaticKeys.MaterialNo] = "Block Request";
                    Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("MaterialBlock.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnBlockRequest_Click", ex);
            //throw ex;
        }
    }

    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        try
        {
            string Module = "174";
            MasterAccess objMasterAccess = new MasterAccess();
            int MasterHeaderId = objMasterAccess.GenerateBlockRequestM("0", ddlPlantGroupB.SelectedValue, Module, "MMU", lblUserId.Text, "", ddlModuleB.SelectedValue, "", ddlPlantB.SelectedValue);

            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = Module;
                Session[StaticKeys.MaterialPlantId] = ddlPlantB.SelectedValue;
                Session[StaticKeys.MaterialProcessModuleId] = ddlModuleB.SelectedValue;
                Session[StaticKeys.MatStorageLocationId] = "";

                Session[StaticKeys.MaterialPlantName] = ddlPlantB.SelectedItem.Text;
                Session[StaticKeys.MatStorageLocationName] = "";

                Session[StaticKeys.MatPlantGrp] = ddlPlantGroupB.SelectedValue;
                Session[StaticKeys.MassRequestProcessId] = "0";

                Session[StaticKeys.SelectedModule] = "Material Master UnBlock";
                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "U";
                Session[StaticKeys.MaterialNo] = "Unblock Request";
                Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("MaterialBlock.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnUnBlockRequest_Click", ex); }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string Req_Id = "";
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                CheckBox chkSelection = grv.FindControl("chkSelection") as CheckBox;
                if (chkSelection.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    Req_Id = Req_Id + lblRequestID.Text + "/";
                }
            }

            MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
            if (ObjMaterialMasterAccess.DeleteMassRequest(Req_Id, lblUserId.Text) > 0)
            {
                if (Session[StaticKeys.MatTypeSelected] != null)
                    ddlModuleSearch.SelectedValue = Session[StaticKeys.MatTypeSelected].ToString();

                ReadMaterialMasterRequests();
                lblMsg.Text = "Request Deleted Successfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }

        }
        catch (Exception ex)
        {
            _log.Error("btnDelete_Click", ex);
            //throw ex;
        }
    }

    protected void btnMassSubmit_Click(object sender, EventArgs e)
    {
        int Count2 = 0;
        string Req_Id = "";
        //Promotion code start
        string RequestNos = "";
        bool PlantGroupIdck = false;
        //Promotion code End
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {

            //Promotion code start
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                CheckBox chkSelection = grv.FindControl("chkSelection") as CheckBox;
                if (chkSelection.Checked == true)
                {
                    Count2++;
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;

                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;

                    if (lblModuleId.Text == "195")
                    {
                        try
                        {
                            if (!(MaterialMasterAccess.Get_IsHSNGSTFilled(lblRequestID.Text, lblUserId.Text)))
                            {

                                RequestNos = RequestNos + " , " + lblRequestNo.Text;
                            }
                        }
                        catch (Exception ex) { }
                    }
                    if (Convert.ToString(lblPlantGroupId.Text) == "2" && Count2 > 1)
                    {
                        PlantGroupIdck = true;
                        break;
                    }
                }
            }
            Count2 = 0;
            if (PlantGroupIdck == true)
            {
                lblMsg.Text = "Please submit single record.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else
            {
                if (RequestNos == "")
                {
                    //Promotion code End
                    foreach (GridViewRow grv in grdSearch.Rows)
                    {
                        CheckBox chkSelection = grv.FindControl("chkSelection") as CheckBox;
                        if (chkSelection.Checked == true)
                        {
                            Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                            Req_Id = Req_Id + lblRequestID.Text + "/";
                        }
                    }
                    MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
                    if (ObjMaterialMasterAccess.GenerateMassRequestProcess(Req_Id, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                    {
                        if (Session[StaticKeys.MatTypeSelected] != null)
                            ddlModuleSearch.SelectedValue = Session[StaticKeys.MatTypeSelected].ToString();

                        ReadMaterialMasterRequests();
                        lblMsg.Text = "Request Generated Successfully";
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;
                    }
                    else
                    {
                        lblMsg.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                    //Promotion code start
                }
                else
                {
                    lblMsg.Text = "Kindly enter Control Code/HSN and GST rate in GST section of request No : " + RequestNos + " before submitting the request/s.";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
                //Promotion code End
            }

        }
        catch (Exception ex)
        {
            _log.Error("btnMassSubmit_Click", ex);
            //throw ex;
        }

        //txtRequestNo.Text = Req_Id;
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (Session[StaticKeys.MatTypeSelected] != null)
                ddlModuleSearch.SelectedValue = Session[StaticKeys.MatTypeSelected].ToString();

            grdSearch.PageIndex = e.NewPageIndex;
            ReadMaterialMasterRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearch_PageIndexChanging", ex); }
    }

    protected void ddlModuleC_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;
        bool flgMkt = false;
        try
        {

            //if (ddlModuleC.SelectedValue == "162" || ddlModuleC.SelectedValue == "164")
            if (ddlModuleC.SelectedValue == "162" || ddlModuleC.SelectedValue == "164")
            {
                flg = true;
            }
            if (ddlModuleC.SelectedValue == "139" || ddlModuleC.SelectedValue == "171")
            {
                if (ddlPlantC.SelectedValue != null)
                {
                    if (ddlPlantC.SelectedValue == "3" || ddlPlantC.SelectedValue == "10" || ddlPlantC.SelectedValue == "21" || ddlPlantC.SelectedValue == "22" || ddlPlantC.SelectedValue == "27" || ddlPlantC.SelectedValue == "13")
                        flgMkt = true;
                }
            }

            //CTRL_SUB_SDT18112019 
            if (ddlModuleC.SelectedValue == "162")
            {
                ddlPlantC.Items.Clear();
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                {
                    helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantListCtrl '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantListCtrl '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                    // Depot Srinidhi 
                    ddlPlantC.Items.Add(new ListItem("IRF-CWH", "8888"));
                    ddlPlantC.Items.Add(new ListItem("IRF-CFA", "8889"));
                }

            }
            else
            {
                ddlPlantC.Items.Clear();
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                {
                    helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                    // Depot Srinidhi 
                    ddlPlantC.Items.Add(new ListItem("IRF-CWH", "8888"));
                    ddlPlantC.Items.Add(new ListItem("IRF-CFA", "8889"));
                }
            }
            //CTRL_SUB_SDT18112019

            trPurchasingGroupC.Visible = flg;
            reqddlPurchasingGroupC.Visible = flg;
            trMarketTypeExt.Visible = flgMkt;
        }
        catch (Exception ex)
        { _log.Error("ddlModuleC_SelectedIndexChanged", ex); }
    }

    protected void ddlPlantC_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flgMkt = false;
        try
        {
            helperAccess.PopuplateDropDownList(ddlPurchasingGroupC, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlantC.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            if (ddlModuleC.SelectedValue == "139" || ddlModuleC.SelectedValue == "171")
            {
                if (ddlPlantC.SelectedValue != null)
                {
                    if (ddlPlantC.SelectedValue == "3" || ddlPlantC.SelectedValue == "10" || ddlPlantC.SelectedValue == "21" || ddlPlantC.SelectedValue == "22" || ddlPlantC.SelectedValue == "27" || ddlPlantC.SelectedValue == "13")
                        flgMkt = true;
                }
            }
            trMarketTypeExt.Visible = flgMkt;
        }
        catch (Exception ex)
        { _log.Error("ddlPlantC_SelectedIndexChanged", ex); }
    }

    //protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    bool flg = false;
    //    bool flgEmer = false;
    //    bool flgMkt = false; 
    //    if (ddlModule.SelectedValue == "162" || ddlModule.SelectedValue == "164" || ddlModule.SelectedValue == "139" || ddlModule.SelectedValue == "144" || ddlModule.SelectedValue == "171")
    //    {
    //        flgEmer = true;
    //        if (ddlModule.SelectedValue == "162" || ddlModule.SelectedValue == "164")
    //            flg = true;

    //        if (ddlModule.SelectedValue == "139" || ddlModule.SelectedValue == "171")
    //        {
    //            if (ddlPlant.SelectedValue != null)
    //            {
    //                if (ddlPlant.SelectedValue == "3" || ddlPlant.SelectedValue == "10" || ddlPlant.SelectedValue == "21" || ddlPlant.SelectedValue == "22" || ddlPlant.SelectedValue == "27" || ddlPlant.SelectedValue == "13")
    //                {
    //                    flgMkt = true;
    //                    flgEmer = false;
    //                }
    //            }

    //        }
    //    }
    //    //if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "5")
    //    //    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetApprovalPlantList '" + ddlModule.SelectedValue + "','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");

    //    trPurchasingGroup.Visible = flg;
    //    trEmergency.Visible = flgEmer;
    //    reqddlPurchasingGroup.Visible = flg;
    //    trMarketType.Visible = flgMkt;
    //    reqddlMarketType.Visible = flgMkt;

    //}

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;
        bool flgEmer = false;
        bool flgMkt = false;
        try
        {

            if (ddlModule.SelectedValue == "138")
            {
                trddlPlant.Visible = false;
                trddlStorageLocation.Visible = false;
                reqddlPlant.Visible = false;
                reqddlStorageLocation.Visible = false;

                //btnNext.Attributes.Add("Style", "display:none;");
                //lnkbtnProsol.Attributes.Add("Style", "display:none;");
                //trbtnNext.Visible = false;
                //trlnkbtnProsol.Visible = true;

                //MaterialMasterAccess materialAccess = new MaterialMasterAccess();
                //int masterHeaderId;
                //int retValue;

                //try
                //{
                //    if (CheckedCreationFeilds())
                //    {
                //        string mode = lblMode.Text;
                //        if (trMarketType.Visible == true)
                //            mode = ddlMarketType.SelectedValue;
                //        if (trEmergency.Visible == true)
                //            mode = chkEmergency.Checked ? "E" : lblMode.Text;
                //        Session[StaticKeys.MarketType] = mode;

                //        masterHeaderId = materialAccess.SaveMaterialHeader("0", ddlModule.SelectedValue, ddlPlantGroup.SelectedValue, lblUserId.Text, mode, ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, ddlPurchasingGroup.SelectedValue);
                //        if (masterHeaderId > 0)
                //        {
                //            Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                //            if (ddlModule.SelectedValue == "138")
                //            { 
                //                string SalesOrgID, DistributionChannelID = "";
                //                int ret;
                //                materialAccess.SaveSalesDefaultERSHIBEZMBW(masterHeaderId.ToString(), ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, lblUserId.Text, out SalesOrgID, out DistributionChannelID, out ret);
                //                if (ret > 0)
                //                {
                //                    //Session[StaticKeys.RequestNo]
                //                    //Response.Redirect("BasicData1.aspx");     
                //                    //ProsolLink
                //                    //lnkbtnProsol.Attributes.Add("href", "https://www.google.com/search?RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));

                //                    lnkbtnProsol.Attributes.Add("href", ""+ Convert.ToString(ConfigurationManager.AppSettings["ctrlSubLiveDate"])+ "?RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));
                //                    lnkbtnProsol.Attributes.Add("target", "_blank");
                //                }
                //            }

                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //throw ex;
                //}
            }
            else
            {
                trddlPlant.Visible = true;
                trddlStorageLocation.Visible = true;
                reqddlPlant.Visible = true;
                reqddlStorageLocation.Visible = true;

                //btnNext.Attributes.Add("Style", "display:none;");
                //lnkbtnProsol.Attributes.Add("Style", "display:none;");

                //btnNext.Visible = true;
                ////lnkbtnProsol.Visible = false; 
                //trbtnNext.Visible = true;
                //trlnkbtnProsol.Visible = false;

                if (ddlModule.SelectedValue == "162" || ddlModule.SelectedValue == "164" || ddlModule.SelectedValue == "139" || ddlModule.SelectedValue == "144" || ddlModule.SelectedValue == "171")
                {
                    flgEmer = true;
                    if (ddlModule.SelectedValue == "162" || ddlModule.SelectedValue == "164")
                        flg = true;

                    if (ddlModule.SelectedValue == "139" || ddlModule.SelectedValue == "171")
                    {
                        if (ddlPlant.SelectedValue != null)
                        {
                            if (ddlPlant.SelectedValue == "3" || ddlPlant.SelectedValue == "10" || ddlPlant.SelectedValue == "21" || ddlPlant.SelectedValue == "22" || ddlPlant.SelectedValue == "27" || ddlPlant.SelectedValue == "13")
                            {
                                flgMkt = true;
                                flgEmer = false;
                            }
                        }

                    }
                }

                //CTRL_SUB_SDT18112019 
                if (ddlModule.SelectedValue == "162")
                {
                    //ddlPlant.Items.Insert(9, new ListItem("Pune R&D : - API  ", "L007"));
                    //ddlPlant.Items.Insert(224, new ListItem("Pune R&D : - Formulation  ", "L007"));
                    //ddlPlant.Items.Add(new ListItem("L007-Pune R&D : - Formulation", "224"));
                    //ddlPlant.
                    //ddlPlant.Items.FindByValue("224").Enabled = true;
                    ddlPlant.Items.Clear();
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                    {

                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                    }
                    else
                    {
                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                    }
                }
                else
                {
                    ddlPlant.Items.Clear();
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                    {

                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                    }
                    else
                    {
                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                    }
                }
                //CTRL_SUB_SDT18112019 


            }
            trPurchasingGroup.Visible = flg;
            trEmergency.Visible = flgEmer;
            reqddlPurchasingGroup.Visible = flg;
            trMarketType.Visible = flgMkt;
            reqddlMarketType.Visible = flgMkt;
        }
        catch (Exception ex)
        { _log.Error("ddlModule_SelectedIndexChanged", ex); }


    }

    protected void ddlModuleCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;
        try
        {

            //if (ddlModuleCopy.SelectedValue == "162" || ddlModuleCopy.SelectedValue == "164")
            if (ddlModuleCopy.SelectedValue == "162" || ddlModuleCopy.SelectedValue == "164")
            {
                flg = true;
            }

            //CTRL_SUB_SDT18112019 
            if (ddlModuleCopy.SelectedValue == "162")
            {
                ddlPlantCopy.Items.Clear();
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                {
                    helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantListCtrl '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantListCtrl '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                }

            }
            else
            {
                ddlPlantCopy.Items.Clear();
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                {
                    helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                }

            }
            //CTRL_SUB_SDT18112019

            trPurchasingGroupCopy.Visible = flg;
            reqddlPurchasingGroupCopy.Visible = flg;
        }
        catch (Exception ex)
        { _log.Error("ddlModuleCopy_SelectedIndexChanged", ex); }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;
        bool flgEmer = false;
        try
        {
            //HelperAccess helperAccess = new HelperAccess();
            helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlant.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");

            //Market selection for Aurangabad, Goa, Pithampur, Mandideep
            if (ddlModule.SelectedValue == "139" || ddlModule.SelectedValue == "171")
            {
                if (ddlPlant.SelectedValue == "3" || ddlPlant.SelectedValue == "10" || ddlPlant.SelectedValue == "21" || ddlPlant.SelectedValue == "22" || ddlPlant.SelectedValue == "27" || ddlPlant.SelectedValue == "13")
                {
                    flg = true;
                }
                flgEmer = true;
                //else
                //{ 
                //    flgEmer = true;
                //}
            }
            if (ddlModule.SelectedValue == "162" || ddlModule.SelectedValue == "164" || ddlModule.SelectedValue == "144")
            {
                flgEmer = true;
            }
            trMarketType.Visible = flg;
            reqddlMarketType.Visible = flg;
            trEmergency.Visible = flgEmer;
        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    protected void ddlPlantCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;
        bool flgEmer = false;
        try
        {
            //HelperAccess helperAccess = new HelperAccess();
            helperAccess.PopuplateDropDownList(ddlStorageLocationCopy, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantCopy.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPurchasingGroupCopy, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlantCopy.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            //Market selection for Aurangabad, Goa, Pithampur, Mandideep
            if (ddlModuleCopy.SelectedValue == "139" || ddlModuleCopy.SelectedValue == "171")
            {
                if (ddlPlantCopy.SelectedValue == "3" || ddlPlantCopy.SelectedValue == "10" || ddlPlantCopy.SelectedValue == "21" || ddlPlantCopy.SelectedValue == "22" || ddlPlantCopy.SelectedValue == "27" || ddlPlantCopy.SelectedValue == "13")
                {
                    flg = true;
                }
                else
                    flgEmer = true;
            }
            if (ddlModule.SelectedValue == "162" || ddlModule.SelectedValue == "164" || ddlModule.SelectedValue == "144")
            {
                flgEmer = true;
            }
            trMarketTypeCopy.Visible = flg;
            reqddlMarketTypeCopy.Visible = flg;
            trCopyEmergency.Visible = flgEmer;
        }
        catch (Exception ex)
        { _log.Error("ddlPlantCopy_SelectedIndexChanged", ex); }
    }

    protected void rdoSelection_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (btnCopyRequest.Visible && ddlModule.Items.Count > 1)
            {
                RadioButton rdoSelection = (RadioButton)sender;
                GridViewRow grv = (GridViewRow)rdoSelection.Parent.Parent;

                Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                Label lblPlantId = grv.FindControl("lblPlantId") as Label;
                Label lblStorageLocation = grv.FindControl("lblStorageLocation") as Label;
                Label lblPurchasingGroup = grv.FindControl("lblPurchasingGroup") as Label;
                Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                Label lblActionType = grv.FindControl("lblActionType") as Label;
                string ActionType = lblActionType.Text;

                if (ActionType == "N" || ActionType == "R")
                {
                    ddlModuleCopy.SelectedValue = lblModuleId.Text;

                    //CTRL_SUB_SDT18112019 
                    if (ddlModuleCopy.SelectedValue == "162")
                    {
                        //ddlPlant.Items.Insert(9, new ListItem("Pune R&D : - API  ", "L007"));
                        //ddlPlant.Items.Insert(224, new ListItem("Pune R&D : - Formulation  ", "L007"));
                        //ddlPlant.Items.Add(new ListItem("L007-Pune R&D : - Formulation", "224"));
                        //ddlPlant.
                        //ddlPlant.Items.FindByValue("224").Enabled = true;
                        ddlPlantCopy.Items.Clear();
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                        {

                            helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantListCtrl '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                        }
                        else
                        {
                            helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantListCtrl '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                        }
                    }
                    else
                    {
                        ddlPlantCopy.Items.Clear();
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                        {

                            helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                        }
                        else
                        {
                            helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                        }
                    }
                    //CTRL_SUB_SDT18112019 

                    ddlPlantCopy.SelectedValue = lblPlantId.Text;
                    helperAccess.PopuplateDropDownList(ddlStorageLocationCopy, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantCopy.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                    helperAccess.PopuplateDropDownList(ddlPurchasingGroupCopy, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlantCopy.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");

                    ddlStorageLocationCopy.SelectedValue = lblStorageLocation.Text;
                    ddlPurchasingGroupCopy.SelectedValue = lblPurchasingGroup.Text;

                    bool flg = false;
                    bool flgEmer = false;
                    bool flgMkt = false;
                    if (ddlModuleCopy.SelectedValue == "162" || ddlModuleCopy.SelectedValue == "164" || ddlModuleCopy.SelectedValue == "144" || ddlModuleCopy.SelectedValue == "139" || ddlModuleCopy.SelectedValue == "171")
                    //if (ddlModuleCopy.SelectedValue == "162" || ddlModuleCopy.SelectedValue == "164" || ddlModuleCopy.SelectedValue == "139" || ddlModuleCopy.SelectedValue == "144" || ddlModuleCopy.SelectedValue == "145" || ddlModuleCopy.SelectedValue == "171")
                    {
                        flgEmer = true;
                        if (ddlModuleCopy.SelectedValue == "164" || ddlModuleCopy.SelectedValue == "162")
                        {
                            flg = true;
                        }
                        if (ddlModuleCopy.SelectedValue == "139" || ddlModuleCopy.SelectedValue == "171")
                        {
                            if (ddlPlantCopy.SelectedValue != null)
                            {
                                if (ddlPlantCopy.SelectedValue == "3" || ddlPlantCopy.SelectedValue == "10" || ddlPlantCopy.SelectedValue == "21" || ddlPlantCopy.SelectedValue == "22" || ddlPlantCopy.SelectedValue == "27" || ddlPlantCopy.SelectedValue == "13")
                                {
                                    flgMkt = true;
                                    flgEmer = false;
                                }
                            }
                        }
                    }

                    trPurchasingGroupCopy.Visible = flg;
                    reqddlPurchasingGroupCopy.Visible = flg;
                    trCopyEmergency.Visible = flgEmer;
                    trMarketTypeCopy.Visible = flgMkt;
                    reqddlMarketTypeCopy.Visible = flgMkt;
                    //trCopyEmergency.Visible = flg;
                }
                else
                {
                    ddlModuleCopy.SelectedValue = "";
                    ddlPlantCopy.SelectedValue = "";
                    helperAccess.PopuplateDropDownList(ddlStorageLocationCopy, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantCopy.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                    helperAccess.PopuplateDropDownList(ddlPurchasingGroupCopy, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlantCopy.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");

                    ddlStorageLocationCopy.SelectedValue = "";

                    //trPurchasingGroupCopy.Visible = false;
                    //reqddlPurchasingGroupCopy.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("rdoSelection_CheckedChanged", ex); }
    }

    protected void ddlModuleSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session[StaticKeys.MatTypeSelected] = ddlModuleSearch.SelectedValue;
        }
        catch (Exception ex)
        { _log.Error("ddlModuleSearch_SelectedIndexChanged", ex); }
    }



    //MSC_8300001775 Start

    private bool IsValidEntry()
    {
        btnGoback.Visible = false;
        lblddlSalesOrgs.Visible = false;
        rfvddlSalesOrgs.Visible = false;
        lblddlDistributionChannels.Visible = false;
        rfvddlDistributionChannels.Visible = false;

        bool flagchk = false;
        try
        {


            foreach (GridViewRow grv in gvSelectSec.Rows)
            {
                Label lblSection_Id = (Label)grv.FindControl("lblSection_Id");
                CheckBox chkSelect = (CheckBox)grv.FindControl("chkSelectionSec");
                //  if (lblSection_Id.Text == "3" && chkSelect.Checked != true)
                //if (chkSelect.Checked != true)
                //{
                //    lblscm.Text = "Please select valid section.";
                //    flagchk = true;
                //    break;
                //}
                //else 
                if ((lblSection_Id.Text == "15" || lblSection_Id.Text == "16" || lblSection_Id.Text == "17" || lblSection_Id.Text == "18") && chkSelect.Checked == true
                    && ((ddlSalesOrgs.SelectedValue == "0" || ddlDistributionChannels.SelectedValue == "0" ||
                    ddlSalesOrgs.SelectedValue == "" || ddlDistributionChannels.SelectedValue == "")))
                {
                    btnGoback.Visible = true;
                    lblddlSalesOrgs.Visible = true;
                    rfvddlSalesOrgs.Visible = true;
                    lblddlDistributionChannels.Visible = true;
                    rfvddlDistributionChannels.Visible = true;
                    lblscm.Text = "Please select Sales Org. & Dist. Channel.";
                    flagchk = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("IsValidEntry", ex); }
        return flagchk;
    }

    private void SingleMatChange()
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        int retValue;

        try
        {
            string mode = "";
            masterHeaderId = materialAccess.SaveSingleMatChangeHeader("0", ddlMaterialTypes.SelectedValue, ddlPlantGroups.SelectedValue, lblUserId.Text, mode, ddlPlants.SelectedValue, ddlStorageLocs.SelectedValue);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlMaterialTypes.SelectedValue;
                Session[StaticKeys.MaterialPlantId] = ddlPlants.SelectedValue;
                Session[StaticKeys.MatStorageLocationId] = ddlStorageLocs.SelectedValue;
                //Session[StaticKeys.MatPurchasingGroupId] = ddlPurchasingGroup.SelectedValue;

                Session[StaticKeys.MaterialPlantName] = ddlPlants.SelectedItem.Text;
                Session[StaticKeys.MatStorageLocationName] = ddlStorageLocs.SelectedItem.Text;

                Session[StaticKeys.MatPlantGrp] = ddlPlantGroups.SelectedValue;
                Session[StaticKeys.MassRequestProcessId] = "0";

                Session[StaticKeys.SelectedModule] = ddlMaterialTypes.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "C";//Change N to C NR
                Session[StaticKeys.ActionType] = "C"; //Change N to C  NR
                Session[StaticKeys.MaterialNo] = txtMaterialCodes.Text;
                Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                //Response.Redirect("BasicData1.aspx");

            }

        }
        catch (Exception ex)
        {
            _log.Error("SingleMatChange", ex);
        }
    }


    protected void btnGetFromSAP_Click(object sender, EventArgs e)
    {
        btnGoback.Visible = false;
        lblscm.Text = "";
        panelscm.Visible = false;
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();

        try
        {

            if (!IsValidEntry())
            {

                //SingleMatChange();
                //int iMasterHeaderId = Convert.ToInt32(Session[StaticKeys.MasterHeaderId]);

                lblGetfromSAP.Text = "";
                List<string> lSec = new List<string>();
                //lSec.Add("3");
                foreach (GridViewRow grv in gvSelectSec.Rows)
                {
                    Label lblSection_Id = (Label)grv.FindControl("lblSection_Id");
                    CheckBox chkSelect = (CheckBox)grv.FindControl("chkSelectionSec");
                    if (chkSelect.Checked == true)
                    {
                        //if (lblSection_Id.Text != "3")
                        //{
                        lSec.Add(lblSection_Id.Text);
                        // }
                        ////materialAccess.SaveSectionSMChange(ddlMaterialTypes.SelectedValue, iMasterHeaderId, lblSection_Id.Text);
                        //if (lblGetfromSAP.Text != "")
                        //{
                        //    lblGetfromSAP.Text += ',' + lblSection_Id.Text;
                        //}
                        //else
                        //{
                        //    lblGetfromSAP.Text = lblSection_Id.Text;
                        //}
                    }
                }
                //string[] test = lSec.ToArray();
                //Session[StaticKeys.mco] = test;
                Session[StaticKeys.mco] = Convert.ToString(txtMaterialCodes.Text);
                Session[StaticKeys.mty] = Convert.ToString(ddlMaterialTypes.SelectedValue);
                Session[StaticKeys.pla] = Convert.ToString(ddlPlants.SelectedValue);
                Session[StaticKeys.stg] = Convert.ToString(ddlStorageLocs.SelectedValue);
                Session[StaticKeys.pog] = Convert.ToString(ddlPurchasingGroups.SelectedValue);
                Session[StaticKeys.sal] = Convert.ToString(ddlSalesOrgs.SelectedValue) == "" ? "0" : Convert.ToString(ddlSalesOrgs.SelectedValue);
                Session[StaticKeys.dch] = Convert.ToString(ddlDistributionChannels.SelectedValue) == "" ? "0" : Convert.ToString(ddlDistributionChannels.SelectedValue);
                //Session[StaticKeys.sec] = Convert.ToString(lblGetfromSAP.Text);
                //Session[StaticKeys.sec] = lSec.ToArray();
                try
                {
                    GetService.GetMaterialDetails service = new GetService.GetMaterialDetails();
                    service.UseDefaultCredentials = true;
                    //webservice.Credentials = new NetworkCredential("userName", "password", "domain");
                    //webservice.PreAuthenticate = true;
                    //String output = service.HelloWorld();


                    //string LoggedIn_User_IP = objUtil.GetIpAddress();

                    var output = service.GetMaterialDetailsWSL(Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]), Convert.ToString(Session[StaticKeys.mco])
                        , Convert.ToString(Session[StaticKeys.mty]), Convert.ToString(Session[StaticKeys.pla]), Convert.ToString(Session[StaticKeys.stg])
                        , Convert.ToString(Session[StaticKeys.pog]), Convert.ToString(Session[StaticKeys.sal]), Convert.ToString(Session[StaticKeys.dch]), "2", lSec.ToArray());

                    if (output.msgdialog == "Done")
                    {
                        int masterHeaderId;
                        masterHeaderId = Convert.ToInt32(output.MasterHId);
                        if (masterHeaderId > 0)
                        {
                            Session[StaticKeys.SelectedModuleId] = ddlMaterialTypes.SelectedValue;
                            Session[StaticKeys.MaterialPlantId] = ddlPlants.SelectedValue;
                            Session[StaticKeys.MatStorageLocationId] = ddlStorageLocs.SelectedValue;
                            //Session[StaticKeys.MatPurchasingGroupId] = ddlPurchasingGroup.SelectedValue;
                            Session[StaticKeys.MatPurchasingGroupId] = "";

                            Session[StaticKeys.MaterialPlantName] = ddlPlants.SelectedItem.Text;
                            Session[StaticKeys.MatStorageLocationName] = ddlStorageLocs.SelectedItem.Text;

                            Session[StaticKeys.MatPlantGrp] = ddlPlantGroups.SelectedValue;
                            Session[StaticKeys.MassRequestProcessId] = "0";

                            Session[StaticKeys.SelectedModule] = ddlMaterialTypes.SelectedItem.Text;
                            Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                            Session[StaticKeys.Mode] = "N";//Change N to C NR
                            Session[StaticKeys.ActionType] = "N"; //Change N to C  NR
                            Session[StaticKeys.MaterialNo] = txtMaterialCodes.Text;
                            Session[StaticKeys.RequestNo] = output.mRequestNo;

                            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                            Response.Redirect("BasicData1.aspx", false);
                            //Response.Redirect(output.rURL);

                        }
                    }
                    else
                    {
                        Type cstype = this.GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        //if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        //{
                        //    String cstext = "Error :" + Convert.ToString(output.msgdialog);
                        //    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        //} 

                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            String cstext = "alert('" + "Error: " + Convert.ToString(output.msgdialog) + "');";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
                    }

                }
                catch (Exception ex)
                {
                    WriteMatChangeLog("MatChangeRFCError" + sdate + ".txt", ex.ToString());
                }


                //Get data from SAP Start
                //MaterialGetFromSAP materialGetFromSAP = new MaterialGetFromSAP();
                //materialGetFromSAP.GetMaterialDetails(iMasterHeaderId, Convert.ToString(txtMaterialCodes.Text), Convert.ToString(ddlMaterialTypes.SelectedValue), Convert.ToString(ddlPlants.SelectedValue), Convert.ToString(ddlStorageLocs.SelectedValue), Convert.ToString(ddlPurchasingGroups.SelectedValue), Convert.ToString(ddlSalesOrgs.SelectedValue), Convert.ToString(ddlDistributionChannels.SelectedValue));
                //Get data from SAP End

                //string Path = ConfigurationManager.AppSettings["SAPIntPath"];
                //btnSMConfOk.NavigateUrl = Path + "/GetMatFromSAP.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() +
                //    "&mco=" + Convert.ToString(txtMaterialCodes.Text) + "&mty=" + Convert.ToString(ddlMaterialTypes.SelectedValue) +
                //    "&pla=" + Convert.ToString(ddlPlants.SelectedValue) + "&stg=" + Convert.ToString(ddlStorageLocs.SelectedValue) +
                //    "&pog=" + Convert.ToString(ddlPurchasingGroups.SelectedValue) + "&sal=" + Convert.ToString(ddlSalesOrgs.SelectedValue) +
                //    "&dch=" + Convert.ToString(ddlDistributionChannels.SelectedValue) + "&sec=" + Convert.ToString(lblGetfromSAP.Text);
                //if (materialGetFromSAP != null)
                //{
                //    Response.Redirect("BasicData1.aspx");
                //}
                //else
                //{

                //}
                // Get a ClientScriptManager reference from the Page class.
                //ClientScriptManager cs = Page.ClientScript;
                //// Check to see if the startup script is already registered.
                //if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                //{
                //    cs.RegisterStartupScript(GetType(), "key", "ShowConfirmSMDialog();", true);
                //}
                //lblSMConf.Text = "Are you sure you want to get material details from SAP.";
                //panelSMConf.CssClass = "error";
                //panelSMConf.Visible = true;

            }
            else
            {
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                {
                    cs.RegisterStartupScript(GetType(), "key", "ShowSectionSMDialog();", true);
                }
                //lblscm.Text = "Please select valid section.";
                panelscm.CssClass = "error";
                panelscm.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnGetFromSAP_Click", ex); }
    }

    protected void txtMaterialCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            MaterialTypeSelection();
        }
        catch (Exception ex)
        { _log.Error("txtMaterialCode_TextChanged", ex); }
    }

    private void MaterialTypeSelection()
    {

        ddlMaterialTypes.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCodes.Text);

        try
        {
            if (ddlMaterialTypes.SelectedValue == "162" || ddlMaterialTypes.SelectedValue == "164")
            {
                tr5.Visible = true;
            }
            else
            {
                tr5.Visible = false;
            }
        }
        catch (Exception ex) { _log.Error("MaterialTypeSelection", ex); }

    }

    protected void ddlSalesOrgs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlDistributionChannels, "pr_GetDistributionChannelList '" + 0 + "','S1','" + 0 + "','" + ddlSalesOrgs.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrgs_SelectedIndexChanged", ex); }
    }

    protected void ddlMaterialTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlMaterialTypesMass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaterialTypesMass.SelectedValue == "162" || ddlMaterialTypesMass.SelectedValue == "164")
            {
                tr8.Visible = true;
                //rfvddlPurchasingGroupm.Visible = true;
            }
            else
            {
                tr8.Visible = false;
                //rfvddlPurchasingGroupm.Visible = false;
            }
        }
        catch (Exception ex) { _log.Error("ddlMaterialTypesMass_SelectedIndexChanged", ex); }
    }

    protected void ddlMaterialTypesMassExt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaterialTypesMassExt.SelectedValue == "162" || ddlMaterialTypesMassExt.SelectedValue == "164")
            {
                tr10.Visible = true;
                //rfvddlExtMassPoGroup.Visible = true;
            }
            else
            {
                tr10.Visible = false;
                //rfvddlExtMassPoGroup.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ddlMaterialTypesMassExt_SelectedIndexChanged", ex);
        }
    }

    protected void ddlPlants_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageLocs, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlants.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlPurchasingGroups, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlants.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlSalesOrgs, "pr_GetSalesOrganisationList '" + '0' + "','S1','" + '0' + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        }
        catch (Exception ex)
        { _log.Error("ddlPlants_SelectedIndexChanged", ex); }
    }

    protected void ddlPlantm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageLocm, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantm.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlPurchasingGroupm, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlantm.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlTypeOfMassUpdm, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlTypeOfMassUpdm','0','" + ddlPlantm.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlTypeOfMassUpdm, "pr_GetDropDownListByControlNameModuleType 'M','ddlTypeOfMassUpdm'", "LookUp_Desc", "LookUp_Code", "");
        }
        catch (Exception ex)
        { _log.Error("ddlPlantm_SelectedIndexChanged", ex); }

    }

    protected void btnMassNext_Click(object sender, EventArgs e)
    {

        MasterAccess objMasterAccess = new MasterAccess();
        ddlPlantGroupm.SelectedValue = "2";

        try
        {
            string mode = "";
            //string mode = lblMode.Text;
            //if (trMarketTypeExt.Visible == true)
            //    mode = ddlMarketTypeExt.SelectedValue;
            //int MasterHeaderId = objMasterAccess.GenerateMatMassRequest("0", "MMC", lblUserId.Text, mode, ddlPlantGroupm.SelectedValue, ddlPlantm.SelectedValue, ddlMaterialTypem.Text, ddlPurchasingGroupm.SelectedValue, ddlStorageLocm.SelectedValue);
            //int MasterHeaderId = objMasterAccess.GenerateMatMassRequest("0", "MMC", lblUserId.Text, mode, ddlPlantGroupm.SelectedValue, ddlPlantm.SelectedValue, "", ddlPurchasingGroupm.SelectedValue, ddlStorageLocm.SelectedValue);
            int MasterHeaderId = objMasterAccess.GenerateMatMassRequest("0", "MMC", lblUserId.Text, mode, ddlPlantGroupm.SelectedValue, ddlPlantm.SelectedValue, ddlMaterialTypesMass.SelectedValue, ddlPurchasingGroupm.SelectedValue, ddlTypeOfMassUpdm.SelectedValue);
            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                Session[StaticKeys.MatPlantGrp] = ddlPlantGroupm.SelectedValue;
                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.MaterialPlantId] = ddlPlantm.SelectedValue;
                Session[StaticKeys.MaterialProcessModuleId] = "";//ddlMaterialTypem.SelectedValue;
                Session[StaticKeys.MatStorageLocationId] = "";

                Session[StaticKeys.MaterialPlantName] = ddlPlantm.SelectedItem.Text;
                Session[StaticKeys.MatStorageLocationName] = "";

                Session[StaticKeys.TypeOfMassUpdm] = ddlTypeOfMassUpdm.SelectedValue;

                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "C";
                Session[StaticKeys.MaterialNo] = "Bulk Request";
                Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "Material Bulk Change";
                Session[StaticKeys.MassRequestProcessId] = "0";

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                Response.Redirect("MaterialChange.aspx", false);
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnMassNext_Click", ex);
            //throw ex;
        }

    }

    protected void btnSelectSec_Click(object sender, EventArgs e)
    {
        //pr_GetSectionByModuleId 164,3,2
        btnGoback.Visible = false;
        lblscm.Text = "";
        panelscm.Visible = false;
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {

            DataSet dstData = objMatAccess.GetSMSection(Convert.ToInt32(ddlMaterialTypes.SelectedValue), lblUserId.Text, Convert.ToInt32(ddlPlantGroups.SelectedValue));


            if (dstData.Tables[0].Rows.Count > 0)
            {
                gvSelectSec.DataSource = dstData.Tables[0].DefaultView;
                gvSelectSec.DataBind();
            }
            else
            {
                gvSelectSec.DataSource = null;
                gvSelectSec.DataBind();
            }
            //gvSelectSec
            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the startup script is already registered.
            if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
            {
                cs.RegisterStartupScript(GetType(), "key", "ShowSectionSMDialog();", true);
            }
        }
        catch (Exception ex)
        { _log.Error("btnSelectSec_Click", ex); }
    }

    //MSC_8300001775 End




    #endregion

    #region MSE_8300002156 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlExtPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlExtStorageLoc, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlExtPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlExtPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlExtPlant.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlExtSalesOrg, "pr_GetSalesOrganisationList '" + '0' + "','S1','" + '0' + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        }
        catch (Exception ex)
        { _log.Error("ddlExtPlant_SelectedIndexChanged", ex); }
    }

    protected void ddlExtRefPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlExtStorageLocRef, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlExtRefPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            //helperAccess.PopuplateDropDownList(ddlExtPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlExtPlant.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlExtSalesOrg, "pr_GetSalesOrganisationList '" + '0' + "','S1','" + '0' + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        }
        catch (Exception ex)
        { _log.Error("ddlExtRefPlant_SelectedIndexChanged", ex); }
    }


    /// <summary>
    /// MSE_8300002156
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnExtMatGet_Click(object sender, EventArgs e)
    //{
    //}
    protected void btnExtMatGet_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        List<string> lSec = new List<string>();
        try
        {

            Session[StaticKeys.extmco] = Convert.ToString(txtExtMatCode.Text);
            Session[StaticKeys.extmty] = Convert.ToString(ddlExtMaterialType.SelectedValue);
            Session[StaticKeys.extpla] = Convert.ToString(ddlExtPlant.SelectedValue);
            Session[StaticKeys.extstg] = Convert.ToString(ddlExtStorageLoc.SelectedValue);
            Session[StaticKeys.extpog] = Convert.ToString(ddlExtPurchasingGroup.SelectedValue);
            Session[StaticKeys.extsal] = Convert.ToString(ddlExtSalesOrg.SelectedValue) == "" ? "0" : Convert.ToString(ddlExtSalesOrg.SelectedValue);
            Session[StaticKeys.extdch] = Convert.ToString(ddlExtDistrChannel.SelectedValue) == "" ? "0" : Convert.ToString(ddlExtDistrChannel.SelectedValue);
            Session[StaticKeys.extrefpla] = Convert.ToString(ddlExtRefPlant.SelectedValue);

            Session[StaticKeys.extrefstg] = Convert.ToString(ddlExtStorageLocRef.SelectedValue);

            try
            {
                _log.Info("Befor Service call");
                GetService.GetMaterialDetails service = new GetService.GetMaterialDetails();
                service.UseDefaultCredentials = true;
                //service.Timeout = 111;
                _log.Info("Befor Service call_1 User : " + lblUserId.Text);
                var output = service.GetMaterialExtDetailsWSL(Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]), Convert.ToString(Session[StaticKeys.extmco])
                    , Convert.ToString(Session[StaticKeys.extmty]), Convert.ToString(Session[StaticKeys.extpla]), Convert.ToString(Session[StaticKeys.extstg])
                    , Convert.ToString(Session[StaticKeys.extpog]), Convert.ToString(Session[StaticKeys.extsal]), Convert.ToString(Session[StaticKeys.extdch]), Convert.ToString(Session[StaticKeys.extvaltyp]), Convert.ToString(Session[StaticKeys.extrefpla]), Convert.ToString(Session[StaticKeys.extrefstg]));

                _log.Info("After" + Convert.ToString(output.msgdialog));

                if (output.msgdialog == "Done")
                {
                    _log.Info("After_Done" + Convert.ToString(output.msgdialog));
                    int masterHeaderId;
                    masterHeaderId = Convert.ToInt32(output.MasterHId);
                    _log.Info("After_Done" + Convert.ToString(output.msgdialog) + "##" + masterHeaderId);
                    if (masterHeaderId > 0)
                    {
                        //_log.Info(masterHeaderId);
                        Session[StaticKeys.SelectedModuleId] = output.mModule_Id.ToString();
                        Session[StaticKeys.MatPlantGrp] = ddlPlantGroupC.SelectedValue;
                        Session[StaticKeys.MaterialProcessModuleId] = ddlExtMaterialType.SelectedValue;

                        Session[StaticKeys.MaterialPlantName] = ddlExtPlant.SelectedItem.Text;
                        Session[StaticKeys.MatPurchasingGroupId] = ddlExtPurchasingGroup.SelectedValue;
                        Session[StaticKeys.MatStorageLocationName] = ddlExtStorageLoc.SelectedValue;

                        Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                        Session[StaticKeys.Mode] = "N";
                        Session[StaticKeys.MaterialType] = "";
                        Session[StaticKeys.ActionType] = "E";
                        Session[StaticKeys.MaterialNo] = "Extension Request";
                        Session[StaticKeys.RequestNo] = output.mRequestNo;
                        Session[StaticKeys.SelectedModule] = "Material Extension";
                        Session[StaticKeys.MassRequestProcessId] = "0";
                        Session[StaticKeys.MaterialPlantId] = ddlExtPlant.SelectedValue;

                        Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                        Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                        Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                        Response.Redirect("MaterialExtension.aspx", false);
                    }
                }
                else
                {
                    _log.Info(Convert.ToString(output.msgdialog));
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('" + "Error: " + Convert.ToString(output.msgdialog) + "');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Info("btnExtMatGet_Click1 " + lblUserId.Text);
                _log.Error("btnExtMatGet_Click1 User: " + lblUserId.Text, ex);
            }
        }
        catch (Exception ex)
        { _log.Error("btnExtMatGet_Click", ex); }

    }

    /// <summary>
    /// MSE_8300002156
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlExtSalesOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlExtDistrChannel, "pr_GetDistributionChannelList '" + 0 + "','S1','" + 0 + "','" + ddlExtSalesOrg.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        }
        catch (Exception ex)
        { _log.Error("ddlExtSalesOrg_SelectedIndexChanged", ex); }
    }

    /// <summary>
    /// MSE_8300002156
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ddlExtPlant_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ddlExtRefPlantt_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}


    /// <summary>
    /// MSE_8300002156
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtExtMatCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            MaterialTypeSelectionE();
        }
        catch (Exception ex)
        { _log.Error("txtExtMatCode_TextChanged", ex); }
    }


    private void MaterialTypeSelectionE()
    {
        try
        {
            ddlExtMaterialType.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtExtMatCode.Text);

            if (ddlExtMaterialType.SelectedValue == "162" || ddlExtMaterialType.SelectedValue == "164")
            {
                trextPogrp.Visible = true;
                rfvExtPurchasingGroup.Visible = true;
            }
            else
            {
                trextPogrp.Visible = false;
                rfvExtPurchasingGroup.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("MaterialTypeSelectionE", ex); }
    }



    #endregion


    #region MME_8300002156
    protected void ddlExMassPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlExtMassStorageLoc, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlExMassPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlExtMassPoGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlExMassPlant.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlExtMassType, "pr_GetDropDownListByControlNameModuleType 'M','ddlTypeOfMassExt'", "LookUp_Desc", "LookUp_Code", "");
        }
        catch (Exception ex)
        { _log.Error("ddlExMassPlant_SelectedIndexChanged", ex); }
    }

    protected void btnMassExtNext_Click(object sender, EventArgs e)
    {

        MasterAccess objMasterAccess = new MasterAccess();
        //ddlPlantGroupm.SelectedValue = "2";

        try
        {
            string mode = "";
            int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequest("0", "MEXT", lblUserId.Text, mode, "2", ddlExMassPlant.SelectedValue, ddlMaterialTypesMassExt.SelectedValue, ddlExtMassPoGroup.SelectedValue);
            //int MasterHeaderId = objMasterAccess.GenerateMatMassRequest("0", "MMC", lblUserId.Text, mode, "2", ddlPlantm.SelectedValue, "", ddlPurchasingGroupm.SelectedValue, ddlStorageLocm.SelectedValue);
            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                Session[StaticKeys.MatPlantGrp] = "2";
                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.MaterialPlantId] = ddlExMassPlant.SelectedValue;
                Session[StaticKeys.MaterialProcessModuleId] = "";
                Session[StaticKeys.MatStorageLocationId] = "";
                Session[StaticKeys.MaterialPlantName] = ddlExMassPlant.SelectedItem.Text;
                Session[StaticKeys.MatStorageLocationName] = ddlExtMassStorageLoc.SelectedItem.Text;

                Session[StaticKeys.TypeOfMassUpdm] = ddlExtMassType.SelectedValue;

                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "E";
                Session[StaticKeys.MaterialNo] = "Bulk Request";
                Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                Session[StaticKeys.SelectedModule] = "Material Bulk Extension";
                Session[StaticKeys.MassRequestProcessId] = "0";

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("MaterialMassExtension.aspx", false);
                //Response.Redirect("MaterialExtension.aspx");
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnMassExtNext_Click", ex);
            //throw ex;
        }

    }


    #endregion

    #region DEP_05102023

    protected void btnGetFromSAPdp_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();

        try
        {

            List<string> lSec = new List<string>();
            lSec.Add("1");
            lSec.Add("2");
            lSec.Add("3");
            lSec.Add("4");
            lSec.Add("5");
            lSec.Add("6");
            lSec.Add("7");
            lSec.Add("8");
            lSec.Add("9");
            lSec.Add("10");
            lSec.Add("11");
            lSec.Add("12");
            lSec.Add("13");
            lSec.Add("14");
            lSec.Add("15");
            lSec.Add("16");
            lSec.Add("17");
            lSec.Add("18");
            lSec.Add("21");
            lSec.Add("51");
            Session[StaticKeys.mco] = Convert.ToString(txtMaterialCodesdp.Text);
            Session[StaticKeys.mty] = Convert.ToString(ddlMaterialTypesdp.SelectedValue);
            Session[StaticKeys.pla] = Convert.ToString(ddlPlantsdp.SelectedValue);
            Session[StaticKeys.stg] = Convert.ToString(ddlStorageLocsdp.SelectedValue);
            Session[StaticKeys.pog] = Convert.ToString(ddlPurchasingGroupsdp.SelectedValue);
            Session[StaticKeys.sal] = Convert.ToString(ddlSalesOrgsdp.SelectedValue) == "" ? "0" : Convert.ToString(ddlSalesOrgsdp.SelectedValue);
            Session[StaticKeys.dch] = Convert.ToString(ddlDistributionChannelsdp.SelectedValue) == "" ? "0" : Convert.ToString(ddlDistributionChannelsdp.SelectedValue);
            try
            {
                GetService.GetMaterialDetails service = new GetService.GetMaterialDetails();
                service.UseDefaultCredentials = true;

                var output = service.GetMaterialDetailsWSL(Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]), Convert.ToString(Session[StaticKeys.mco])
                    , Convert.ToString(Session[StaticKeys.mty]), Convert.ToString(Session[StaticKeys.pla]), Convert.ToString(Session[StaticKeys.stg])
                    , Convert.ToString(Session[StaticKeys.pog]), Convert.ToString(Session[StaticKeys.sal]), Convert.ToString(Session[StaticKeys.dch]), "1", lSec.ToArray());

                if (output.msgdialog == "Done")
                {
                    int masterHeaderId;
                    masterHeaderId = Convert.ToInt32(output.MasterHId);
                    if (masterHeaderId > 0)
                    {
                        Session[StaticKeys.SelectedModuleId] = ddlMaterialTypesdp.SelectedValue;
                        Session[StaticKeys.MaterialPlantId] = ddlPlantsdp.SelectedValue;
                        Session[StaticKeys.MatStorageLocationId] = ddlStorageLocsdp.SelectedValue;
                        Session[StaticKeys.MatPurchasingGroupId] = "";

                        Session[StaticKeys.MaterialPlantName] = ddlPlantsdp.SelectedItem.Text;
                        Session[StaticKeys.MatStorageLocationName] = ddlStorageLocsdp.SelectedItem.Text;

                        Session[StaticKeys.MatPlantGrp] = ddlPlantGroupsdp.SelectedValue;
                        Session[StaticKeys.MassRequestProcessId] = "0";

                        Session[StaticKeys.SelectedModule] = ddlMaterialTypesdp.SelectedItem.Text;
                        Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                        Session[StaticKeys.Mode] = "N";//Change N to C NR
                        Session[StaticKeys.ActionType] = "N"; //Change N to C  NR
                        Session[StaticKeys.MaterialNo] = txtMaterialCodesdp.Text;
                        Session[StaticKeys.RequestNo] = output.mRequestNo;

                        Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                        Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                        Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                        Session[StaticKeys.MarketType] = "";
                        Response.Redirect("BasicData1.aspx", false);

                    }
                }
                else
                {
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('" + "Error: " + Convert.ToString(output.msgdialog) + "');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }

            }
            catch (Exception ex)
            {
                WriteMatChangeLog("MatChangeRFCError" + sdate + ".txt", ex.ToString());
            }

        }
        catch (Exception ex)
        { _log.Error("btnGetFromSAPdp_Click", ex); }
    }

    protected void txtMaterialCodedp_TextChanged(object sender, EventArgs e)
    {
        try
        {
            MaterialTypeSelectiondp();
        }
        catch (Exception ex)
        { _log.Error("txtMaterialCode_TextChanged", ex); }
    }

    private void MaterialTypeSelectiondp()
    {

        ddlMaterialTypesdp.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCodesdp.Text);

        try
        {
            if (ddlMaterialTypesdp.SelectedValue == "162" || ddlMaterialTypesdp.SelectedValue == "164")
            {
                tr13.Visible = true;
            }
            else
            {
                tr13.Visible = false;
            }
        }
        catch (Exception ex) { _log.Error("MaterialTypeSelection", ex); }

    }

    protected void ddlSalesOrgsdp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlDistributionChannelsdp, "pr_GetDistributionChannelList '" + 0 + "','S1','" + 0 + "','" + ddlSalesOrgsdp.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrgs_SelectedIndexChanged", ex); }
    }

    protected void ddlPlantsdp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageLocsdp, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantsdp.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlPurchasingGroupsdp, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlantsdp.SelectedValue + "','RMPM'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlSalesOrgsdp, "pr_GetSalesOrganisationList '" + '0' + "','S1','" + '0' + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        }
        catch (Exception ex)
        { _log.Error("ddlPlants_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Private Functions

    protected bool CheckedCreationFeilds()
    {

        try
        {
            lblMsg.Text = "";
            if (reqddlModule.Visible && ddlModule.SelectedValue == "")
                lblMsg.Text = "Module Code is Mandatory. ";
            if (reqddlPlant.Visible && ddlPlant.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";
            if (reqddlStorageLocation.Visible && ddlStorageLocation.SelectedValue == "")
                lblMsg.Text += "Storage Location is Mandatory. ";

            if (lblMsg.Text == "")
            {
                return true;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowCreateNewDialog();", true);
                //HtmlGenericControl pagebody = (HtmlGenericControl)Master.FindControl("pagebody");
                //pagebody.Attributes.Add("onload", "ShowCreateNewDialog()");
                return false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckedCreationFeilds", ex);
            throw ex;
        }
    }

    protected bool CheckedBlockFeilds()
    {

        try
        {
            lblMsg.Text = "";
            if (reqddlModuleB.Visible && ddlModuleB.SelectedValue == "")
                lblMsg.Text = "Module Code is Mandatory. ";
            if (reqddlPlantB.Visible && ddlPlantB.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";


            if (lblMsg.Text == "")
            {
                return true;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowBlockDialog();", true);
                return false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckedBlockFeilds", ex);
            throw ex;
        }
    }

    protected bool CheckedChangeFeilds()
    {

        try
        {
            lblMsg.Text = "";
            if (reqddlModuleC.Visible && ddlModuleC.SelectedValue == "")
                lblMsg.Text = "Module Code is Mandatory. ";
            if (reqddlPlantC.Visible && ddlPlantC.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";


            if (lblMsg.Text == "")
            {
                return true;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowChangeBulkRequestDialog();", true);
                return false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckedChangeFeilds", ex);
            throw ex;
        }
    }

    protected bool CheckedExtensionFeilds()
    {
        try
        {
            lblMsg.Text = "";
            if (reqddlModuleC.Visible && ddlModuleC.SelectedValue == "")
                lblMsg.Text = "Module Code is Mandatory. ";
            if (reqddlPlantC.Visible && ddlPlantC.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";


            if (lblMsg.Text == "")
            {
                return true;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowChangeExtensionDialog();", true);
                return false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckedExtensionFeilds", ex);
            throw ex;
        }
    }

    public void PopulateDropDownList()
    {
        try
        {

            //HelperAccess helperAccess = new HelperAccess();

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
            {
                //ListItem lst = new ListItem("Pending For Review", "REV");
                ddlStatus.Items.Insert(1, new ListItem("Pending For Review", "REV"));
                ddlStatus.Items.Insert(2, new ListItem("Pending For Final", "FIN"));

            }

            //ObjHelperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id");
            helperAccess.PopuplateDropDownList(ddlPlantGroup, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroup.SelectedValue = "1";

            //if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "5")
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            else
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");

            helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


            helperAccess.PopuplateDropDownList(ddlPlantGroupC, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupC.SelectedValue = "1";
            //if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "5")

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
                helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                // Depot Srinidhi 
                ddlPlantC.Items.Add(new ListItem("IRF-CWH", "8888"));
                ddlPlantC.Items.Add(new ListItem("IRF-CFA", "8889"));
            }

            helperAccess.PopuplateDropDownList(ddlPlantGroupB, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupB.SelectedValue = "1";

            if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "5")
                helperAccess.PopuplateDropDownList(ddlPlantB, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            else
                helperAccess.PopuplateDropDownList(ddlPlantB, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");

            helperAccess.PopuplateDropDownList(ddlPlantGroupCopy, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupCopy.SelectedValue = "1";
            //if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "5")

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
            {
                helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            }
            helperAccess.PopuplateDropDownList(ddlStorageLocationCopy, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantCopy.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


            //MSC_8300001775 Start
            helperAccess.PopuplateDropDownList(ddlPlantGroups, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroups.SelectedValue = "2";

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
            {
                helperAccess.PopuplateDropDownList(ddlPlants, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlants, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            }
            //helperAccess.PopuplateDropDownList(ddlStorageLocs, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlants.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlPlantGroupm, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupm.SelectedValue = "2";

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
            {
                helperAccess.PopuplateDropDownList(ddlPlantm, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlantm, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            }
            //helperAccess.PopuplateDropDownList(ddlStorageLocm, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantm.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            //MSC_8300001775 end

            //MSE_8300002156 Start

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
            {
                helperAccess.PopuplateDropDownList(ddlExtPlant, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                helperAccess.PopuplateDropDownList(ddlExtRefPlant, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
                helperAccess.PopuplateDropDownList(ddlExMassPlant, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");

            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlExtPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                helperAccess.PopuplateDropDownList(ddlExtRefPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                helperAccess.PopuplateDropDownList(ddlExMassPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            }

            //DEP_05102023 Start
            helperAccess.PopuplateDropDownList(ddlPlantGroupsdp, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupsdp.SelectedValue = "1";

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "5")
            {
                helperAccess.PopuplateDropDownList(ddlPlantsdp, "pr_GetPlantList '0','SPOC','" + lblUserId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlantsdp, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            }
            //DEP_05102023 End
        }
        catch (Exception ex)
        { _log.Error("PopulateDropDownList", ex); }

        //MSE_8300002156 End
    }

    /// <summary>
    /// add 814364
    /// IsValidSearch()
    /// </summary>
    private void ReadMaterialMasterRequests()
    {
        if (IsValidSearch() == true)
        {
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            Utility ObjUtil = new Utility();
            pnlMsg.Visible = false;

        try
        {
            //dstData = objMatAccess.ReadMaterialMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "M", txtSAPCode.Text.Trim());
            dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "M", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            //grdSearch.DataBind();
            btnMassSubmit.Visible = false;
            btnDelete.Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = true;
                    grdSearch.Columns[15].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = true;
                    grdSearch.Columns[15].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = true;
                    grdSearch.Columns[15].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "SUB")
                {
                    //grdSearch.Columns[0].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = true;

                    btnCopyRequest.Visible = true;
                    //MSC_8300001775 Comment
                    //grdSearch.AllowPaging = false;
                    //MSC_8300001775 Comment
                    //MSC_8300001775
                    grdSearch.AllowPaging = true;
                    //MSC_8300001775
                    btnDelete.Visible = true;
                    //btnView.Visible = false;
                    //btnModify.Visible = true;
                    btnMassSubmit.Visible = true;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnCopyRequest.Visible = false;
            }

            if (ddlModule.Items.Count <= 1)
            {
                btnChangeBulkRequestC.Enabled = false;
                btnChangeExtensionC.Enabled = false;
                btnBlockRequest.Enabled = false;
            }

                //btnChangeExtensionC.Enabled = false;

                grdSearch.DataBind();
            }
            catch (Exception ex)
            {
                _log.Info(lblUserId.Text);
                _log.Error("ReadMaterialMasterRequests", ex);
                //throw ex;
            }

        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Please select valid date range and date range should not exceed 90 days.";

            //Type cstype = this.GetType(); 
            //ClientScriptManager cs = Page.ClientScript; 
            //if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
            //{
            //    String cstext = "alert('Please select valid date range.');";
            //      cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
            //}
        }
    }

    private void ReadModules()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            //ddlModuleSearch.DataSource = objMatAccess.ReadModules("M");
            ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("M");

            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadModules", ex);
            //throw ex;
        }
    }

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        try
        {
            DataSet ds = objMatAccess.ReadProfileWiseModules(profileId, userId, "M");

            List<decimal> li = new List<decimal>();
            li.Add(172);
            li.Add(175);

            DataTable dt;

            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                dt = ds.Tables[0].AsEnumerable()
                            .Where(r => !li.Contains(r.Field<decimal>("Module_Id")))
                            .CopyToDataTable();
            }
            else
            {
                dt = null;
            }

            ddlModule.DataSource = dt;
            ddlModule.DataTextField = "Module_Name";
            ddlModule.DataValueField = "Module_Id";
            ddlModule.DataBind();

            ddlModuleC.DataSource = dt;
            ddlModuleC.DataTextField = "Module_Name";
            ddlModuleC.DataValueField = "Module_Id";
            ddlModuleC.DataBind();

            ddlModuleB.DataSource = dt;
            ddlModuleB.DataTextField = "Module_Name";
            ddlModuleB.DataValueField = "Module_Id";
            ddlModuleB.DataBind();

            ddlModuleCopy.DataSource = dt;
            ddlModuleCopy.DataTextField = "Module_Name";
            ddlModuleCopy.DataValueField = "Module_Id";
            ddlModuleCopy.DataBind();

            //MSC_8300001775 Start
            ddlMaterialTypes.DataSource = dt;
            ddlMaterialTypes.DataTextField = "Module_Name";
            ddlMaterialTypes.DataValueField = "Module_Id";
            ddlMaterialTypes.DataBind();


            //MSC_8300001775 Start
            ddlMaterialTypesMass.DataSource = dt;
            ddlMaterialTypesMass.DataTextField = "Module_Name";
            ddlMaterialTypesMass.DataValueField = "Module_Id";
            ddlMaterialTypesMass.DataBind();

            //MSE_8300002156 Start
            ddlMaterialTypesMassExt.DataSource = dt;
            ddlMaterialTypesMassExt.DataTextField = "Module_Name";
            ddlMaterialTypesMassExt.DataValueField = "Module_Id";
            ddlMaterialTypesMassExt.DataBind();


            //ddlMaterialTypem.DataSource = dt;
            //ddlMaterialTypem.DataTextField = "Module_Name";
            //ddlMaterialTypem.DataValueField = "Module_Id";
            //ddlMaterialTypem.DataBind();

            //MSC_8300001775 End

            //MSE_8300002156 Start
            ddlExtMaterialType.DataSource = dt;
            ddlExtMaterialType.DataTextField = "Module_Name";
            ddlExtMaterialType.DataValueField = "Module_Id";
            ddlExtMaterialType.DataBind();
            //MSE_8300002156 End

            //DEP_05102023 Start
            ddlMaterialTypesdp.DataSource = dt;
            ddlMaterialTypesdp.DataTextField = "Module_Name";
            ddlMaterialTypesdp.DataValueField = "Module_Id";
            ddlMaterialTypesdp.DataBind();
            //DEP_05102023 End

            bool flg = true;

            if (ddlModule.Items.Count > 1)
            {
                ListItem lst = new ListItem("Ready for Submit", "SUB");
                ddlStatus.Items.Add(lst);
                ddlStatus.SelectedValue = "SUB";


                //dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "M", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));



                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                //if (dstData.Tables[0].Rows.Count > 0)
                if (Session[StaticKeys.LoggedIn_User_Profile_Id] != null)
                {
                    if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2")
                    {
                        flg = false;

                        // Check to see if the startup script is already registered.
                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            String cstext = "alert('";
                            if (Session[StaticKeys.AddAlertMsg] != null)
                            {
                                if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                                {
                                    cstext += Session[StaticKeys.AddAlertMsg].ToString() + "\\n\\n";
                                    Session[StaticKeys.AddAlertMsg] = null;
                                }
                            }

                            //cstext += "Please select the request(s) and click on Mass Submit to send the request(s) for Mass processing.');";
                            cstext += "Please tick(towards right end) in front of the finalized request(s).\\nClick on Mass Submit to send the request(s) for processing.');";
                            //String cstext = "if(confirm('Is request processing Complete?')){RequestSubmitPage();};";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
                    }
                    else
                    {
                        ddlStatus.SelectedValue = "P";
                        if (Session[StaticKeys.AddAlertMsg] != null)
                        {
                            if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                            {
                                // Check to see if the startup script is already registered.
                                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                                {
                                    String cstext = "alert('" + Session[StaticKeys.AddAlertMsg].ToString() + "');";
                                    Session[StaticKeys.AddAlertMsg] = null;
                                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                                }
                            }
                        }
                    }
                }
            }

            if (flg)
            {
                if (Session[StaticKeys.SearchStatus] != null)
                {
                    ddlStatus.SelectedValue = Session[StaticKeys.SearchStatus].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            _log.Error("ReadProfileWiseModules", ex);
            //throw ex;
        }
    }

    private string GetSelectedPkID()
    {
        string strPk = string.Empty;
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    strPk = lblRequestID.Text;

                    Label lblMassRequestProcessId = grv.FindControl("lblMassRequestProcessId") as Label;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPlantId = grv.FindControl("lblPlantId") as Label;
                    Label lblStorageLocation = grv.FindControl("lblStorageLocation") as Label;
                    Label lblPurchasingGroup = grv.FindControl("lblPurchasingGroup") as Label;
                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                    Label lblMaterialShortDescription = grv.FindControl("lblMaterialShortDescription") as Label;
                    Label lblPlantName = grv.FindControl("lblPlantName") as Label;
                    Label lblStorageLocationName = grv.FindControl("lblStorageLocationName") as Label;
                    Label lblMaterialProcessModuleId = grv.FindControl("lblMaterialProcessModuleId") as Label;
                    Label lblSalesOrgID = grv.FindControl("lblSalesOrgID") as Label;
                    Label lblDistChnl = grv.FindControl("lblDistChnl") as Label;

                    //Depot Srinidhi
                    Label lblRequestType = grv.FindControl("lblRequestType") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MassRequestProcessId] = lblMassRequestProcessId.Text;
                    Session[StaticKeys.MaterialNo] = (lblMasterCode.Text == "" ? "New Request" : lblMasterCode.Text) + " - " + lblMaterialShortDescription.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    Session[StaticKeys.MatStorageLocationId] = lblStorageLocation.Text;
                    Session[StaticKeys.MatPurchasingGroupId] = lblPurchasingGroup.Text;
                    Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;

                    Session[StaticKeys.MaterialPlantName] = lblPlantName.Text;
                    Session[StaticKeys.MatStorageLocationName] = lblStorageLocationName.Text;

                    Session[StaticKeys.MaterialProcessModuleId] = lblMaterialProcessModuleId.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;

                    Session[StaticKeys.MaterialSalesOrgId] = lblSalesOrgID.Text;
                    Session[StaticKeys.MaterialDistChnlId] = lblDistChnl.Text;

                    //Depot Srinidhi
                    Session[StaticKeys.MarketType] = lblRequestType.Text;

                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("GetSelectedPkID", ex);
        }
        return strPk;
    }

    #endregion

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnProsol_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        int retValue;

        try
        {
            if (CheckedCreationFeilds())
            {
                string mode = lblMode.Text;
                if (trMarketType.Visible == true)
                    mode = ddlMarketType.SelectedValue;
                if (trEmergency.Visible == true)
                    mode = chkEmergency.Checked ? "E" : lblMode.Text;
                Session[StaticKeys.MarketType] = mode;

                masterHeaderId = materialAccess.SaveMaterialHeader("0", ddlModule.SelectedValue, ddlPlantGroup.SelectedValue, lblUserId.Text, mode, ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, ddlPurchasingGroup.SelectedValue);
                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                    if (ddlModule.SelectedValue == "138")
                    {
                        string SalesOrgID, DistributionChannelID = "";
                        int ret;
                        materialAccess.SaveSalesDefaultERSHIBEZMBW(masterHeaderId.ToString(), ddlPlant.SelectedValue, ddlStorageLocation.SelectedValue, lblUserId.Text, out SalesOrgID, out DistributionChannelID, out ret);
                        if (ret > 0)
                        {
                            //Session[StaticKeys.RequestNo]
                            //Response.Redirect("BasicData1.aspx");     
                            //ProsolLink
                            //lnkbtnProsol.Attributes.Add("href", "https://www.google.com/search?RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));

                            //lnkbtnProsol.Attributes.Add("href", "" + Convert.ToString(ConfigurationManager.AppSettings["ctrlSubLiveDate"]) + "?RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));
                            //lnkbtnProsol.Attributes.Add("target", "_blank");
                            //Response.Redirect("http://prosolqa.lupin.com?RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));

                            string ProsolLink = Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"]);
                            Response.Redirect(ProsolLink + "? RequestNo=" + Convert.ToString(Session[StaticKeys.RequestNo]));
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("lnkbtnProsol_Click", ex);
            //throw ex;
        }
    }


    public void WriteMatChangeLog(string strFileName, string strMessage)
    {
        try
        {
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ChangeMaterialLog", strFileName), FileMode.Append, FileAccess.Write);
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
}