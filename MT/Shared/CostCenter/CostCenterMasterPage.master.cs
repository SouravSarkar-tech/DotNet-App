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
using System.Configuration;
using log4net;
public partial class Shared_CostCenter_CostCenterMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                Page.ClientTarget = "uplevel";
        }
        catch (Exception ex)
        { _log.Error("Page_PreInit", ex); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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

                lblRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
                lblMaterialNo.Text = Session[StaticKeys.MaterialNo].ToString();
                lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

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
                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);
                    string mode = Session[StaticKeys.Mode].ToString();
                    if (mode == "M" || mode == "N")
                    {
                        btnSubmit.Visible = true;
                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text);
                    }
                    else
                    {
                        btnRejectTo.Visible = false;
                    }

                    //8400000359 S
                    FillDashBoard();
                    //8400000359 E

                }
                ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
            }
            else
            {
                Response.Redirect("../../login.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// 8400000359 
    /// </summary>
    private void FillDashBoard()
    {
        try
        {
            ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
            DataTable Dt1;

            Dt1 = zcapHsnaccess.GetRemarksByUser(Convert.ToInt32(lblMasterHeaderId.Text), Convert.ToString("0"));
            if (Dt1.Rows.Count > 0)
            {
                rptCommon.DataSource = Dt1;
                rptCommon.DataBind();
            }
            else
            {
                rptCommon.DataSource = null;
                rptCommon.DataBind();
            }
            //    rptCommon.DataSource = Dt1;
            //rptCommon.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDashBoard", ex); }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" && Session[StaticKeys.PendingFor].ToString() == "4")
            {
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                {
                    cs.RegisterStartupScript(GetType(), "key", "ShowAcntPopup();", true);
                }

            }
            else
            {

                //SPNAIRCR_SDT05122019 Old Commented by NR
                //SubmitRequest();
                //SPNAIRCR_SDT05122019 Old Commented by NR

                //SPNAIRCR_SDT05122019 Added by NR
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                {
                    // Get a ClientScriptManager reference from the Page class.
                    ClientScriptManager cs = Page.ClientScript;

                    // Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                    {
                        cs.RegisterStartupScript(GetType(), "key", "ShowApprovePopup();", true);
                    }
                }//SPNAIRCR_SDT05122019 Added by NR
                else
                {
                    SubmitRequest();
                }
            }
        }
        catch (Exception ex)
        { _log.Error("btnSubmit_Click", ex); }
    }

    /// <summary>
    /// SPNAIRCR_SDT05122019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApproveRemarks_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtApproveNote.Text));
        }
        catch (Exception ex)
        { _log.Error("btnApproveRemarks_Click", ex); }
    }

    protected void btnAcntSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtAppComments.Text));
        }
        catch (Exception ex)
        { _log.Error("btnAcntSubmit_Click", ex); }
    }
    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
            if (RollbackRequest())
            {
                Response.Redirect("../CostCenter/CostCenterMaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../CostCenter/CostCenterMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    private bool RollbackRequest()
    {
        bool flg = false;
        try
        {
            MaterialMasterAccess objAccess = new MaterialMasterAccess();

            if (objAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), ddlRejectTo.SelectedValue, Utility.RemoveSpecialChar(txtRejectNote.Text), lblUserId.Text) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-2);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("RollbackRequest", ex);
            //throw ex;
        }
        return flg;
    }

    public bool IsValidURL(string url)
    {
        string str = "";
        try
        {
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

    public void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        VendorMasterAccess VendorMasterAccess = new VendorMasterAccess();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        StringBuilder strBuilder = new StringBuilder();
        string currentSectionId = ((Label)ContentPlaceHolder1.FindControl("lblSectionId")).Text;
        string sectionId = string.Empty;
        string ActionType = Session[StaticKeys.ActionType].ToString();
        bool flg;
        if (ActionType == "C")
            flg = false;
        else
            flg = true;

        bool flg2 = true;
        int sectionStatus = 0;
        try
        {
            dstData = userAccess.ReadSectionTabs(userId, departmentId, lblMasterHeaderId.Text);

            strBuilder.Append("<table cellspacing='0' width='100%'>");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                strBuilder.Append("<tr><td class='navigationBox'>");
                sectionId = row["Section_Id"].ToString();
                sectionStatus = VendorMasterAccess.CheckSectionStatus(sectionId, lblMasterHeaderId.Text);

                if (sectionStatus <= 0 && ActionType != "C")
                {
                    flg = false;
                }
                else if (sectionStatus > 0 && ActionType == "C")
                {
                    flg = true;
                }

                if (row["Section_Id"].ToString() != currentSectionId)
                {
                    if (sectionStatus > 0)
                    {
                        strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                    }
                    else
                    {
                        strBuilder.Append("<a href='" + row["Page_URL"] + "' >" + row["Section_Name"] + "</a>");
                    }
                }
                else if (row["Section_Id"].ToString() == currentSectionId)
                {
                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='Active'>" + row["Section_Name"] + "</a>");
                }
                strBuilder.Append("</td></tr>");
            }

            strBuilder.Append(" </table>");
            litTab.Text = strBuilder.ToString();

            trSideMenuTab.Width = "0px";

            if ((flg) && (flg2))
            {
                btnSubmit.Enabled = true;


                if (!btnRejectTo.Visible && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B") && (Session[StaticKeys.Mode].ToString() == "M" || Session[StaticKeys.Mode].ToString() == "N"))
                {
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Please click on Submit to send the request for processing.');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ReadSectionTabs", ex);
            //throw;
        }

    }

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {
            ddlRejectTo.DataSource = materialMasterAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadDeparmentListForRollback", ex);
            //throw ex;
        }
    }

    private void SubmitToNxtWF()
    {
        try
        {
            Response.Redirect("../CostCenter/CostCenterMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("SubmitToNxtWF", ex); }
    }

    private void SubmitRequest(string remarks = "")
    {
        MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
        VendorMasterAccess vendorMasterAccess = new VendorMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                bool flag = false;
                //SDT17052019 Commented By NR  
                //  if (Session[StaticKeys.SelectedModuleId].ToString() == "197") 
                //EDT17052019 Commented By NR  
                //SDT17052019 Change By NR , Desc : Get Module ID from web config
                if (Convert.ToString(Session[StaticKeys.SelectedModuleId]) == Convert.ToString(ConfigurationManager.AppSettings["ModuleCostCC"]))
                {
                    CostCenterMasterAccess costCenterMasterAccess = new CostCenterMasterAccess();
                    DataSet ds = costCenterMasterAccess.GetCostCenterMasterDataForValidation(Session[StaticKeys.MasterHeaderId].ToString());
                    if (string.IsNullOrEmpty((ds.Tables[0].Rows[0]["Cost_Center"]).ToString()) && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        string message = "alert('Please enter Cost Center.')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

                    }
                    else if (((string.IsNullOrEmpty((ds.Tables[0].Rows[0]["Cost_Center"]).ToString())) && (string.IsNullOrEmpty((ds.Tables[0].Rows[0]["Ref_Cost_Center"]).ToString()))) && Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                    {
                        string message = "alert('Please enter either cost center or reference cost center')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag == true)
                {
                    if (ObjMaterialMasterAccess.ApproveRequestGL(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, remarks) > 0)
                    {
                        flg = true;
                        scope.Complete();
                    }
                }
            }
            if (flg)
            {
                if (!btnRejectTo.Visible)
                    Response.Write("<script>alert('CostCenter Master Request No: " + lblRequestNo.Text + " created.');window.location.href ='CostCenterMaster.aspx'; </script>");
                else
                    Response.Redirect("../CostCenter/CostCenterMaster.aspx", false);
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
            _log.Error("SubmitRequest", ex);
            //throw ex;
        }
    }
}
