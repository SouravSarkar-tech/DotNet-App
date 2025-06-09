using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Transaction_SoftwareApproval_SoftwareApprovalMaster : System.Web.UI.Page
{
    HelperAccess helperAccess = new HelperAccess();
    DataSet dstData = new DataSet();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                PopulateDropDownList();
                ReadProfileWiseModules(userProfileId, lblUserId.Text);
                ReadSWApprovalMasterRequests();
                
                    
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        SWApprovalAccess swApprovalAccess = new SWApprovalAccess();
        int masterHeaderId;
        int retValue;

        try
        {
            if (CheckedCreationFeilds())
            {
                string mode = lblMode.Text;
                if (ddlCategory.SelectedValue != "")
                    mode = ddlCategory.SelectedValue.ToString();
                //Session[StaticKeys.PlantType] = mode;
                masterHeaderId = swApprovalAccess.SaveSWAppHeader("0", ddlPlantGroup.SelectedValue, ddlUserLocation.SelectedValue, txtOtherLoc.Text, ddlModule.SelectedValue, txtOtherSW.Text, mode, txtOtherCategory.Text, lblUserId.Text);
                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
                    //Session[StaticKeys.MaterialPlantId] = ddlPlant.SelectedValue;

                    //Session[StaticKeys.MaterialPlantName] = ddlPlant.SelectedItem.Text;

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroup.SelectedValue;
                    //Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestNo] = swApprovalAccess.mRequestNo;
                    Session[StaticKeys.RequestStatus] = "I";

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    //Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_Location] = ddlUserLocation.SelectedValue;
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                    if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
                    {
                        Response.Redirect("SoftwareApproval.aspx");
                    }

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
        lblMode.Text = "V";
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
        {
            Response.Redirect("SoftwareApproval.aspx");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblMode.Text = "M";
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";


        if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
        {
            Response.Redirect("SoftwareApproval.aspx");
        }

    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearch.PageIndex = e.NewPageIndex;
        ReadSWApprovalMasterRequests();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReadSWApprovalMasterRequests();
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

            SWApprovalAccess objSWApproval = new SWApprovalAccess();
            if (objSWApproval.DeleteMassRequest(Req_Id, lblUserId.Text) > 0)
            {
                ReadSWApprovalMasterRequests();
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
            throw ex;
        }
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false, flg1 = false; ;

        if (ddlModule.SelectedValue.ToString() == "192" || ddlModule.SelectedValue.ToString() == "191" || ddlModule.SelectedValue.ToString() == "193")
        {
            flg = true;
        }
        trCategory.Visible = flg;
        reqddlCategory.Enabled = flg;
        lableddlCategory.Visible = flg;

        if (ddlModule.SelectedValue.ToString() == "193")
        {
            flg1 = true;
        }
        trOtherSW.Visible = flg1;
        reqtxtOtherSW.Enabled = flg1;
        regtxtOtherSW.Enabled = flg1;
        labletxtOtherSW.Visible = flg1;
        if (flg1 == false)
            txtOtherSW.Text = "";

        //Reporting Manager mapping
        if (ddlModule.SelectedValue != "")
        {
            MasterAccess ObjMasterAccess = new MasterAccess();
            txtManager.Text = ObjMasterAccess.GetRMByModuleIdMasterCategory(ddlModule.SelectedValue.ToString(), lblUserId.Text, "");
        }
        //Reporting Manager mapping
    }

    protected void ddlModuleCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false, flg1 = false;

        if (ddlModuleCopy.SelectedValue.ToString() == "192" || ddlModuleCopy.SelectedValue.ToString() == "191" || ddlModule.SelectedValue.ToString() == "193")
        {
            flg = true;
        }
        trCategoryCopy.Visible = flg;
        reqddlCategoryCopy.Enabled = flg;
        lableddlCategoryCopy.Visible = flg;

        if (ddlModuleCopy.SelectedValue.ToString() == "193")
        {
            flg1 = true;
        }
        trOtherSWCopy.Visible = flg1;
        reqtxtOtherSWCopy.Enabled = flg1;
        regtxtOtherSWCopy.Enabled = flg1;
        labletxtOtherSWCopy.Visible = flg1;
        if (flg1 == false)
            txtOtherSWCopy.Text = "";

        //Reporting Manager mapping
        if (ddlModuleCopy.SelectedValue != "")
        {
            MasterAccess ObjMasterAccess = new MasterAccess();
            txtManagerCopy.Text = ObjMasterAccess.GetRMByModuleIdMasterCategory(ddlModuleCopy.SelectedValue.ToString(), lblUserId.Text, "");
        }
        //Reporting Manager mapping
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
            SWApprovalAccess ObjSWMasterAccess = new SWApprovalAccess();
            if (ObjSWMasterAccess.GenerateMassRequestProcess(Req_Id, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
            {
                ReadSWApprovalMasterRequests();
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

        }
        catch (Exception ex)
        {
            throw ex;
        }

        //txtRequestNo.Text = Req_Id;
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        SWApprovalAccess swApprovalAccess = new SWApprovalAccess();
        int masterHeaderId;
        int retValue;
        try
        {
            string ActionType = "";

            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    ActionType = lblActionType.Text;
                    break;
                }
            }

            if (ActionType == "N" || ActionType == "R")
            {
                string mode = lblMode.Text;
                if (ddlCategory.SelectedValue != "")
                    mode = ddlCategory.SelectedValue.ToString();

                masterHeaderId = swApprovalAccess.GenerateCopyRequest(GetSelectedPkID(), ddlPlantGroupCopy.SelectedValue, ddlUserLocationCopy.SelectedValue, txtOtherLocCopy.Text, ddlModuleCopy.SelectedValue, txtOtherSWCopy.Text, mode, txtOtherCategoryCopy.Text, lblUserId.Text);
                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = ddlModuleCopy.SelectedValue;
                    //Session[StaticKeys.MaterialPlantId] = ddlPlantCopy.SelectedValue;
                    //Session[StaticKeys.MatStorageLocationId] = ddlStorageLocationCopy.SelectedValue;
                    //Session[StaticKeys.MatPurchasingGroupId] = ddlPurchasingGroupCopy.SelectedValue;

                    //Session[StaticKeys.MaterialPlantName] = ddlPlantCopy.SelectedItem.Text;
                    //Session[StaticKeys.MatStorageLocationName] = ddlStorageLocationCopy.SelectedItem.Text;

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupCopy.SelectedValue;
                    //Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = ddlModuleCopy.SelectedItem.Text;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestNo] = swApprovalAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    //Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_Location] = ddlUserLocationCopy.SelectedValue;
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("SoftwareApproval.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Copy Option only available for Create Request.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rdoSelection_CheckedChanged(object sender, EventArgs e)
    {
        if (btnCopyRequest.Visible && ddlModule.Items.Count > 1)
        {
            RadioButton rdoSelection = (RadioButton)sender;
            GridViewRow grv = (GridViewRow)rdoSelection.Parent.Parent;

            Label lblModuleId = grv.FindControl("lblModuleId") as Label;
            Label lblPlantId = grv.FindControl("lblPlantId") as Label;
            Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
            Label lblActionType = grv.FindControl("lblActionType") as Label;
            Label lblLocation = grv.FindControl("lblLocation") as Label;

            string ActionType = lblActionType.Text;

            if (ActionType == "N" || ActionType == "R")
            {
                ddlModuleCopy.SelectedValue = lblModuleId.Text;
                ddlUserLocationCopy.SelectedValue = lblLocation.Text;

                if (ddlModuleCopy.SelectedValue.ToString() == "192" || ddlModuleCopy.SelectedValue.ToString() == "191" || ddlModuleCopy.SelectedValue.ToString() == "193")
                {
                    trCategoryCopy.Visible = true;
                    reqddlCategoryCopy.Enabled = true;
                    lableddlCategoryCopy.Visible = true;
                }

                bool flgModule = false;
                if (ddlModuleCopy.SelectedValue.ToString() == "193")
                {
                    flgModule = true;
                }
                trOtherSWCopy.Visible = flgModule;
                reqtxtOtherSWCopy.Enabled = flgModule;
                regtxtOtherSWCopy.Enabled = flgModule;
                labletxtOtherSWCopy.Visible = flgModule;
                if (flgModule == false)
                    txtOtherSWCopy.Text = "";

                bool flgLoc = false;

                if (ddlUserLocationCopy.SelectedValue.ToString() == "Others")
                {
                    flgLoc = true;
                }
                trOtherLocCopy.Visible = flgLoc;
                reqtxtOtherLocCopy.Enabled = flgLoc;
                regtxtOtherLocCopy.Enabled = flgLoc;
                labletxtOtherLocCopy.Visible = flgLoc;
                if (flgLoc == false)
                    txtOtherLocCopy.Text = "";

                bool flgCat = false;

                if (ddlCategoryCopy.SelectedValue.ToString() == "O")
                {
                    flgCat = true;
                }
                trOtherCatCopy.Visible = flgCat;
                reqtxtOtherCategoryCopy.Enabled = flgCat;
                regtxtOtherCategoryCopy.Enabled = flgCat;
                labletxtOtherCategoryCopy.Visible = flgCat;
                if (flgCat == false)
                    txtOtherCategoryCopy.Text = "";

                if (ddlModuleCopy.SelectedValue != "")
                {
                    MasterAccess ObjMasterAccess = new MasterAccess();
                    txtManagerCopy.Text = ObjMasterAccess.GetRMByModuleIdMasterCategory(ddlModuleCopy.SelectedValue.ToString(), lblUserId.Text, "");
                }

            }
            else
            {
                ddlModuleCopy.SelectedValue = "";
                ddlUserLocationCopy.SelectedValue = "";
            }
        }
    }

    protected void ddlUserLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;

        if (ddlUserLocation.SelectedValue.ToString() == "Others")
        {
            flg = true;
        }
        trOtherLoc.Visible = flg;
        reqtxtOtherLoc.Enabled = flg;
        regtxtOtherLoc.Enabled = flg;
        labletxtOtherLoc.Visible = flg;
        if (flg == false)
            txtOtherLoc.Text = "";
    }

    protected void ddlUserLocationCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;

        if (ddlUserLocationCopy.SelectedValue.ToString() == "Others")
        {
            flg = true;
        }
        trOtherLocCopy.Visible = flg;
        reqtxtOtherLocCopy.Enabled = flg;
        regtxtOtherLocCopy.Enabled = flg;
        labletxtOtherLocCopy.Visible = flg;
        if (flg == false)
            txtOtherLocCopy.Text = "";
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;

        if (ddlCategory.SelectedValue.ToString() == "O")
        {
            flg = true;
        }
        trOtherSubCat.Visible = flg;
        reqtxtOtherCategory.Enabled = flg;
        regtxtOtherCategory.Enabled = flg;
        labletxtOtherCategory.Visible = flg;
        if (flg == false)
            txtOtherCategory.Text = "";
    }

    protected void ddlCategoryCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;

        if (ddlCategoryCopy.SelectedValue.ToString() == "O")
        {
            flg = true;
        }
        trOtherCatCopy.Visible = flg;
        reqtxtOtherCategoryCopy.Enabled = flg;
        regtxtOtherCategoryCopy.Enabled = flg;
        labletxtOtherCategoryCopy.Visible = flg;
        if (flg == false)
            txtOtherCategoryCopy.Text = "";
    }

    #endregion

    #region Methods

    private void ReadSWApprovalMasterRequests()
    {
        SWApprovalAccess objSWAppAccess = new SWApprovalAccess();
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;
        try
        {
            dstData = objSWAppAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, "", "S", "", ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            btnSubmit.Visible = false;
            btnDelete.Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "SUB")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;

                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = false;
                    btnDelete.Visible = true;
                    btnSubmit.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "Z")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                    //btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "ZE")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;

                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnCopyRequest.Visible = false;
            }

            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        SWApprovalAccess objSWAppAccess = new SWApprovalAccess();
        Utility ObjUtil = new Utility();

        try
        {
            DataSet ds = objSWAppAccess.ReadProfileWiseModules(profileId, userId, "S");

            ddlModule.DataSource = ds.Tables[0];
            ddlModule.DataTextField = "Module_Name";
            ddlModule.DataValueField = "Module_Id";
            ddlModule.DataBind();

            ddlModuleCopy.DataSource = ds.Tables[0];
            ddlModuleCopy.DataTextField = "Module_Name";
            ddlModuleCopy.DataValueField = "Module_Id";
            ddlModuleCopy.DataBind();

            bool flg = true;

            if (ddlModule.Items.Count > 1)
            {
                ListItem lst = new ListItem("Ready for Submit", "SUB");
                ddlStatus.Items.Add(lst);
                //ddlStatus.SelectedValue = "SUB";
                ddlStatus.SelectedValue = "C";


                dstData = objSWAppAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, "", "S", "", ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    flg = false;

                    //Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('";
                        if (Session[StaticKeys.AddAlertMsg] != null)
                        {
                            if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                            {
                                cstext += Session[StaticKeys.AddAlertMsg].ToString() + "\\n\\n";
                                Session[StaticKeys.AddAlertMsg] = null;
                                //cstext += "'); ";
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
            throw ex;
        }
    }

    private void PopulateDropDownList()
    {
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        {
            ddlStatus.Items.Insert(1, new ListItem("Pending For Review", "REV"));
            ddlStatus.Items.Insert(2, new ListItem("Pending For Final", "FIN"));
        }

        helperAccess.PopuplateDropDownList(ddlPlantGroup, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
        ddlPlantGroup.SelectedValue = "1";

        helperAccess.PopuplateDropDownList(ddlPlantGroupCopy, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
        ddlPlantGroupCopy.SelectedValue = "1";

        helperAccess.PopuplateDropDownList(ddlUserLocation, "pr_GetDropDownListByControlNameModuleType 'S','ddlInstallLocation'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlUserLocationCopy, "pr_GetDropDownListByControlNameModuleType 'S','ddlInstallLocation'", "LookUp_Desc", "LookUp_Code", "");

    }

    private bool CheckedCreationFeilds()
    {
        try
        {
            lblMsg.Text = "";
            if (reqddlModule.Visible && ddlModule.SelectedValue == "")
                lblMsg.Text = "Type of Software is Mandatory. ";
            if (reqddlCategory.Visible && ddlCategory.SelectedValue == "")
                lblMsg.Text += "Sub category is Mandatory. ";

            if (lblMsg.Text == "")
            {
                return true;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowCreateNewDialog();", true);
                return false;
            }
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

                    Label lblMassRequestProcessId = grv.FindControl("lblMassRequestProcessId") as Label;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;

                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPlantId = grv.FindControl("lblPlantId") as Label;
                    //Label lblPlantType = grv.FindControl("lblPlantType") as Label;
                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                    //Label lblPlantName = grv.FindControl("lblPlantName") as Label;
                    //Label lblReqStatus = grv.FindControl("lblReqStatus") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MassRequestProcessId] = lblMassRequestProcessId.Text;
                    //Session[StaticKeys.MaterialNo] = (lblMasterCode.Text == "" ? "New Request" : lblMasterCode.Text) + " - " + lblMaterialShortDescription.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;
                    //Session[StaticKeys.MaterialPlantName] = lblPlantName.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    //Session[StaticKeys.PlantType] = lblPlantType.Text;
                    //Session[StaticKeys.RequestStatus] = lblReqStatus.Text;                  

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strPk;
    }

    #endregion

}