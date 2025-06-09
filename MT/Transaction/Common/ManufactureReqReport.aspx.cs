using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using ExcelLibrary.SpreadSheet;
using log4net;
public partial class Reporting_ManufactureReqReport : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    DataSet dstData = new DataSet();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                ddlStatus.Items.Clear();
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                {
                    ddlStatus.Items.Add(new ListItem("Pending For My Approval", "P"));
                    ddlStatus.Items.Add(new ListItem("Rollbacked By Me", "REJ"));
                    ddlStatus.Items.Add(new ListItem("Rejected By Me", "ZE"));
                    ddlStatus.Items.Add(new ListItem("Approved", "ALL"));
                }
                else
                {
                    ddlStatus.Items.Add(new ListItem("Created By Me", "C"));
                    ddlStatus.Items.Add(new ListItem("Incomplete Request", "I"));
                    ddlStatus.Items.Add(new ListItem("Rollbacked To Me", "R"));
                    ddlStatus.Items.Add(new ListItem("Rejected To Me", "Z"));
                    ddlStatus.Items.Add(new ListItem("Approved", "ALL"));
                }
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();
                ReadModules();
                ReadProfileWiseModules(userProfileId, lblUserId.Text);
                ReadMaterialMasterRequests();
            }
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    //protected void btnCreate_Click(object sender, EventArgs e)
    //{
    //    EAuditAccess auditAccess = new EAuditAccess();
    //    int masterHeaderId;

    //    try
    //    {
    //        string mode = lblMode.Text;
    //        masterHeaderId = auditAccess.SaveAuditHeader("0", "176", lblUserId.Text, mode);
    //        if (masterHeaderId > 0)
    //        {
    //            Session[StaticKeys.SelectedModuleId] = 176;
    //            Session[StaticKeys.SelectedModule] = "EAUD - Audit Request Form";
    //            Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
    //            Session[StaticKeys.Mode] = "N";
    //            Session[StaticKeys.ActionType] = "N";
    //            Session[StaticKeys.MaterialNo] = "New Request";
    //            Session[StaticKeys.RequestNo] = auditAccess.mRequestNo;

    //            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
    //            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
    //            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

    //            Response.Redirect("EAudit.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }


    //    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
    //    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
    //    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
    //    Response.Redirect("EAudit.aspx");
    //}
        
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try { 
        if (ddlStatus.SelectedValue != "ALL")
            { 
            Session[StaticKeys.SearchStatus] = ddlStatus.SelectedValue;
            }
            else
            { 
            Session[StaticKeys.SearchStatus] = null;
            }

            ReadMaterialMasterRequests();
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }
     
    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try {
        grdSearch.PageIndex = e.NewPageIndex;
        ReadMaterialMasterRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearch_PageIndexChanging", ex); }
    }

    #endregion

    #region Methods

    private void ReadMaterialMasterRequests()
    {

        EAuditAccess objAuditAccess = new EAuditAccess();
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;

        try
        {
            dstData = objAuditAccess.SearchauditRequests(ddlStatus.SelectedValue, ddlModuleSearch.SelectedValue, "E", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;


            if (dstData.Tables[0].Rows.Count > 0)
            {
                //hashPara.Add("@Status", status);
                ////hashPara.Add("@RequestNo", requestNo + "%");
                //hashPara.Add("@SAPCode", sapcodeNo + "%");
                ////hashPara.Add("@UserId", userId);
                //hashPara.Add("@ModuleId", moduleId);
                //hashPara.Add("@ModuleType", moduleType);
                //hashPara.Add("@Start_Date", StartDate);
                //hashPara.Add("@End_Date", EndDate);

                grdSearch.DataSource = dstData.Tables[0].DefaultView;
                grdSearch.DataBind();
            }
            //
            if (dstData.Tables[0].Rows.Count > 0)
            {
                //Session[StaticKeys.RptPurchGrp] = ddlPurchasingGroup.SelectedValue;
                //Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.SelectedModuleId] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.Moduletype] = "E";
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.sapcode] = txtSAPCode.Text.Trim();
                // Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;
            }
            else
            {
                Session[StaticKeys.RptPurchGrp] = "";
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";
            }
        }
        catch (Exception ex)
        {
            _log.Error("ReadMaterialMasterRequests", ex);
            //throw ex;
        }
    }

    private void ReadModules()
    {
        EAuditAccess objAuditAccess = new EAuditAccess();
        try
        {
            //ddlModuleSearch.DataSource = objMatAccess.ReadModules("M");
            ddlModuleSearch.DataSource = objAuditAccess.ReadModulesByModuleType("E");

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
        EAuditAccess objAuditAccess = new EAuditAccess();
        Utility ObjUtil = new Utility();

        try
        {
            bool flg = true;

            if (Convert.ToInt32(profileId) == 2)
            {
                ListItem lst = new ListItem("Ready for Submit", "SUB");
                ddlStatus.Items.Add(lst);
                ddlStatus.SelectedValue = "SUB";


                dstData = objAuditAccess.SearchauditRequests(ddlStatus.SelectedValue, ddlModuleSearch.SelectedValue, "E", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                //ClientScriptManager cs = Page.ClientScript;
                //if (dstData.Tables[0].Rows.Count > 0)
                //{
                //    flg = false;

                //    // Check to see if the startup script is already registered.
                //    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                //    {
                //        String cstext = "alert('";
                //        if (Session[StaticKeys.AddAlertMsg] != null)
                //        {
                //            if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                //            {
                //                cstext += Session[StaticKeys.AddAlertMsg].ToString() + "\\n\\n";
                //                Session[StaticKeys.AddAlertMsg] = null;
                //            }
                //        }
                //        cstext += "Please tick(towards right end) in front of the finalized request(s).\\nClick on Mass Submit to send the request(s) for processing.');";
                //        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                //    }
                //}
                //else
                //{
                //    ddlStatus.SelectedValue = "P";
                //    if (Session[StaticKeys.AddAlertMsg] != null)
                //    {
                //        if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                //        {
                //            // Check to see if the startup script is already registered.
                //            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                //            {
                //                String cstext = "alert('" + Session[StaticKeys.AddAlertMsg].ToString() + "');";
                //                Session[StaticKeys.AddAlertMsg] = null;
                //                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                //            }
                //        }
                //    }
                //}
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

    
    #endregion
}