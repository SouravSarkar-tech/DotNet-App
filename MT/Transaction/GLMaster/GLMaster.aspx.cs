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
using Accenture.MWT.DomainObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Globalization;

public partial class Transaction_GLMaster_GLMaster : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                BindGLGroup();
                PopulateDropDownList(userProfileId);

                ReadMaterialMasterRequests();
                
            }
            ShowHideBtn();
        }
    }

    private void ShowHideBtn()
    {
        if (Convert.ToString(Session[StaticKeys.LoggedIn_User_Profile_Id]) == "2")
        {
            btnCreateNew.Attributes.Add("enabled", "enabled");
            btnChangeBulkRequest.Enabled = true;
            //btnChangeRequest.Enabled = true;
            btnChangeExtensionC.Enabled = true;
            btnBlockRequest.Enabled = true;
            btnCopyRequest.Enabled = true;
        }
        else
        {

            btnCreateNew.Attributes.Add("disabled", "disabled");
            btnChangeBulkRequest.Enabled = false;
            btnChangeExtensionC.Enabled = false;
            btnBlockRequest.Enabled = false;
            btnCopyRequest.Enabled = false;
        }
    }



    protected void txtGLCode_TextChanged(object sender, EventArgs e)
    {
        txtGLCode.Text = txtGLCode.Text.ToUpper();
        string str = txtGLCode.Text.Substring(0, 1).ToUpper();
        string str1 = txtGLCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtGLCode.Text);
        //SDT17052019 Change By NR , Desc : Get page path  from web config
        if (strcode >= 231001 && strcode < 299999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleASET"]); // "198";
        else if (strcode >= 140000 && strcode < 145999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleBNKL"]); // "199";
        else if (strcode >= 221000 && strcode < 230999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCASH"]); // "201";
        else if (strcode >= 400000 && strcode < 410999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCONS"]); // "203";
        else if (strcode >= 199001 && strcode < 199999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleDEPN"]); // "204";
        else if (strcode >= 411000 && strcode < 499999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleEXPN"]); // "205";
        else if (strcode >= 200000 && strcode < 204999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleFXAS"]); // "209";
        else if (strcode >= 100000 && strcode < 139999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleLIAB"]); // "210";
        else if (strcode >= 205000 && strcode < 209999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleMATL"]); // "211";
        else if (strcode >= 900000 && strcode < 999999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleMISC"]); // "212";
        else if (strcode >= 146000 && strcode < 150999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModulePABL"]); // "213";
        else if (strcode >= 210000 && strcode < 220999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleRCBL"]); //"214";
        else if (strcode >= 300000 && strcode < 399999)
            ddlGLAccGroupC.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleREVN"]); //"215";
        //EDT17052019 Change By NR , Desc : Get page path  from web config
        else
        {
            string message = "alert('Please enter valid GLCode')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        txtGLName.Focus();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = materialAccess.SaveMaterialHeaderC("0", ddlCompany.SelectedValue, ddlGLAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlGLAccGroup.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlGLAccGroup.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("GLCreate.aspx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = ObjMasterAccess.GenerateCopyRequest(GetSelectedPkID(), ddlCompany.SelectedValue, ddlGLAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlGLAccGroup.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlGLAccGroup.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "R";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = ObjMasterAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("GLCreate.aspx");
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestG("0", "GLC", lblUserId.Text, txtGLCode.Text, "");
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtGLCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "GL Bulk Change";

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("GLChange.aspx");
        }
    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeRequestC("0", ddlCompanyCode.SelectedValue, txtGLCode.Text, ddlGLAccGroupC.SelectedValue, lblUserId.Text, txtGLCode.Text, txtGLName.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = ddlGLAccGroupC.SelectedValue;
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtGLCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = ddlGLAccGroupC.SelectedItem.Text;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("GLCreate.aspx");
        }
    }

    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateBlockRequestC("0", ddlCompanyCode.SelectedValue, ddlGLAccGroupC.SelectedValue, "GLB", lblUserId.Text, txtGLCode.Text, txtGLName.Text);
        if (MasterHeaderId > 0)
        {
            //SDT17052019 Commented By NR  
            //Session[StaticKeys.SelectedModuleId] = "215";//ddlGLAccGroupC.SelectedValue;
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
            Session[StaticKeys.SelectedModuleId] = Convert.ToString(ConfigurationManager.AppSettings["ModuleGLB"]);

            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "B";
            Session[StaticKeys.MaterialNo] = txtGLCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "GL Master Block";// ddlGLAccGroupC.SelectedItem.Text;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("GLBlock.aspx");
        }
    }

    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateBlockRequestC("0", ddlCompanyCode.SelectedValue, ddlGLAccGroupC.SelectedValue, "GLU", lblUserId.Text, txtGLCode.Text, txtGLName.Text);
        if (MasterHeaderId > 0)
        {
            //Session[StaticKeys.SelectedModuleId] = "216";//ddlGLAccGroupC.SelectedValue;

            Session[StaticKeys.SelectedModuleId] = Convert.ToString(ConfigurationManager.AppSettings["ModuleGLU"]);

            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "U";
            Session[StaticKeys.MaterialNo] = txtGLCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "GL Master UnBlock";

            Session[StaticKeys.MaterialProcessModuleId] = ddlGLAccGroupC.SelectedValue;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("GLBlock.aspx");
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

                int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestGL("0", "GEXT", lblUserId.Text, mode, ddlPlantC.SelectedValue, ddlModuleC.Text);
                if (MasterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
                    Session[StaticKeys.MaterialProcessModuleId] = ddlModuleC.SelectedValue;

                    Session[StaticKeys.MaterialPlantName] = ddlPlantC.SelectedItem.Text;
                    Session[StaticKeys.MatStorageLocationName] = "";

                    Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.MaterialType] = "";
                    Session[StaticKeys.ActionType] = "E";
                    Session[StaticKeys.MaterialNo] = "Extension Request";
                    Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
                    Session[StaticKeys.SelectedModule] = "GL Extension";
                    Session[StaticKeys.MassRequestProcessId] = "0";
                    Session[StaticKeys.MaterialPlantId] = ddlPlantC.SelectedValue;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("GLExtension.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("GLBlock.aspx");

        //SDT17052019 Commented By NR  
        ////else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == "214")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Module ID from web config
        else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleGLC"]))
            Response.Redirect("GLChange.aspx");

        //SDT17052019 Commented By NR  
        //else if (Session[StaticKeys.ActionType].ToString() == "E" && Session[StaticKeys.SelectedModuleId].ToString() == "217")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Module ID from web config
        else if (Session[StaticKeys.ActionType].ToString() == "E" && Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleGEXT"]))
        {
            Response.Redirect("GLExtension.aspx");
        }
        else
            Response.Redirect("GLCreate.aspx");
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("GLBlock.aspx");
        //SDT17052019 Commented By NR  
        //else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == "214")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Module ID from web config
        else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleGLC"]))
            Response.Redirect("GLChange.aspx");
        //SDT17052019 Commented By NR  
        //else if (Session[StaticKeys.ActionType].ToString() == "E" && Session[StaticKeys.SelectedModuleId].ToString() == "217")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Module ID from web config
        else if (Session[StaticKeys.ActionType].ToString() == "E" && Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleGEXT"]))
        {
            Response.Redirect("GLExtension.aspx");
        }
        else
            Response.Redirect("GLCreate.aspx");
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
            try
            {
                var fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var diffOfDates = (tdate - fdate).TotalDays;
                diffOfDatesi = Convert.ToInt32(diffOfDates);
            }
            catch (Exception ex)
            {
               // _log.Error("Exception" + ex.Message);
            }
        }


        if ((ddlStatus.SelectedValue == "P" || ddlStatus.SelectedValue == "R" || ddlStatus.SelectedValue == "SUB"))
        {
            flg = true;
        }
        else if ((txtRequestNo.Text.Trim() != ""))
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsValidSearch() == true)
        {
            ReadMaterialMasterRequests();
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Please select valid date range and date range should not exceed 90 days.";
        }
    }

    DataSet dstData = new DataSet();

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        grdSearch.PageIndex = e.NewPageIndex;
        dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlGLAccGrp.SelectedValue, "G", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
        grdSearch.DataSource = dstData.Tables[0].DefaultView;
        grdSearch.DataBind();

    }

    void BindGLGroup()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        ddlGLAccGrp.DataSource = objMatAccess.ReadModules("G");
        ddlGLAccGrp.DataValueField = "Module_Id";
        ddlGLAccGrp.DataTextField = "Module_Name";
        ddlGLAccGrp.DataBind();
        ddlGLAccGrp.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void ddlGLAccGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGLAccGroup.SelectedValue != "")
        {
            MasterAccess ObjMasterAccess = new MasterAccess();
        }

    }

    protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor lnkFrwrdNote = e.Row.FindControl("lnkFrwrdNote") as HtmlAnchor;
            Label lblForward = e.Row.FindControl("lblForward") as Label;
            if (lblForward.Text == "")
            {
            }
            Label lblPrimaryID = e.Row.FindControl("lblPrimaryID") as Label;
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            Utility ObjUtil = new Utility();
            DataSet ds = new DataSet();
            string GLCode = dstData.Tables[0].Rows[0]["Customer_Code"].ToString();
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "37")
            {
                ds = objMatAccess.FindMDMApproved();
                foreach (TableCell cell in e.Row.Cells)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string Master_Header_Id = ds.Tables[0].Rows[i]["Master_Header_Id"].ToString();
                            if (lblPrimaryID.Text == Master_Header_Id)
                                cell.BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

    }

    #endregion

    #region GL Search

    public void PopulateDropDownList(string userProfileId)
    {
        try
        {
            HelperAccess ObjHelperAccess = new HelperAccess();
            ObjHelperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetCompanyList", "Company_Name", "Company_Id", "");
            try
            {
                ddlCompany.SelectedValue = "32";
                ddlCompanyCode.SelectedValue = "32";
                //S4HanaGLDT07122021
                //ddlCompany.Enabled = false;
                //S4HanaGLDT07122021
                //Carve_LC17&LC23 hide
                //ddlCompanyCode.Enabled = false;
                //Carve_LC17&LC23 hide
                ddlPlantC.SelectedValue = "32";
                ddlPlantC.SelectedValue = "32";
            }
            catch { }

            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            HelperAccess helperAccess = new HelperAccess();
            DataSet ds;
            ds = objMatAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "G");

            ddlGLAccGroup.DataSource = ds;
            ddlGLAccGroup.DataTextField = "Module_Name";
            ddlGLAccGroup.DataValueField = "Module_Id";
            ddlGLAccGroup.DataBind();

            ddlGLAccGroupC.DataSource = ds;
            ddlGLAccGroupC.DataTextField = "Module_Name";
            ddlGLAccGroupC.DataValueField = "Module_Id";
            ddlGLAccGroupC.DataBind();

            ddlModuleC.DataSource = ds;
            ddlModuleC.DataTextField = "Module_Name";
            ddlModuleC.DataValueField = "Module_Id";
            ddlModuleC.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadMaterialMasterRequests()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
            dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlGLAccGrp.SelectedValue, "G", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[10].Visible = false;
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        grdSearch.Columns[11].Visible = true;
                    }
                    else
                    {
                        grdSearch.Columns[11].Visible = false;
                    }
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnCopyRequest.Visible = false;
            }

            if (ddlGLAccGroupC.Items.Count < 2)
            {
                btnChangeBulkRequest.Enabled = false;
            }

            grdSearch.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
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
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPendingFor = grv.FindControl("lblPendingFor") as Label;
                    Label lblSelectedGLAccGrp = grv.FindControl("lblSelectedGLAccGrp") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MaterialNo] = lblMasterCode.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.PendingFor] = lblPendingFor.Text;
                    Session[StaticKeys.MaterialProcessModuleId] = lblSelectedGLAccGrp.Text;

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strPk;
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
            throw ex;
        }
    }

    #endregion
}