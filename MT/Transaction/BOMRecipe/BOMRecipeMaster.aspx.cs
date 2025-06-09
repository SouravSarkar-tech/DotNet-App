using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using log4net;
using System.Globalization;

public partial class Transaction_BOMRecipe_BOMRecipeMaster : System.Web.UI.Page
{

    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    DataSet dstData = new DataSet();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                    PopulateDropDownList();
                    ReadProfileWiseModules(userProfileId, lblUserId.Text);
                    ReadBOMRecipeMasterRequests();
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
                btnBlockRequest.Enabled = true;
                btnCopyRequest.Enabled = true;
            }
            else
            {

                btnCreateNew.Attributes.Add("disabled", "disabled");
                btnChangeBulkRequestC.Enabled = false;
                btnBlockRequest.Enabled = false;
                btnCopyRequest.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ShowHideBtn", ex); }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        RecipeAccess recipeAccess = new RecipeAccess();
        int masterHeaderId;
        int retValue;

        try
        {
            if (CheckedCreationFeilds())
            {
                string mode = lblMode.Text;
                if (ddlPlantType.SelectedValue != "")
                    mode = ddlPlantType.SelectedValue.ToString();
                Session[StaticKeys.PlantType] = mode;
                masterHeaderId = recipeAccess.SaveMaterialHeader("0", ddlModule.SelectedValue, ddlPlantGroup.SelectedValue, lblUserId.Text, mode, ddlPlant.SelectedValue);
                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
                    Session[StaticKeys.MaterialPlantId] = ddlPlant.SelectedValue;

                    Session[StaticKeys.MaterialPlantName] = ddlPlant.SelectedItem.Text;

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroup.SelectedValue;
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestNo] = recipeAccess.mRequestNo;
                    Session[StaticKeys.RequestStatus] = "I";

                    Session[StaticKeys.BOMRecipeMatNo] = "";
                    Session[StaticKeys.BOMRecipeMatDesc] = "";

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                    if (Session[StaticKeys.ActionType].ToString() == "M")
                        Response.Redirect("BOMRecipeMassProcess.aspx", false);
                    else if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
                    {
                        //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                        if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "187")
                            || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                            //Response.Redirect("BOMHeaderComp.aspx");
                            Response.Redirect("BOMHeaderComp.aspx", false);
                        //Context.ApplicationInstance.CompleteRequest(); 
                        else if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "185")
                            || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                            Response.Redirect("Reciepe.aspx", false);
                    }
                    else if (Session[StaticKeys.ActionType].ToString() == "C")
                    {
                        if ((Session[StaticKeys.SelectedModuleId].ToString() == "188"))
                            Response.Redirect("BOMRecipeChange.aspx", false);
                    }
                    else if (Session[StaticKeys.ActionType].ToString() == "B" || Session[StaticKeys.ActionType].ToString() == "U")
                    {
                        //if ((Session[StaticKeys.SelectedModuleId].ToString() == "189") || (Session[StaticKeys.SelectedModuleId].ToString() == "190"))
                        Response.Redirect("BOMRecipeBlock.aspx", false);
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "V";
            lblPk.Text = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = lblPk.Text;
            Session[StaticKeys.Mode] = "V";
            Session[StaticKeys.MaterialType] = "";

            if (Session[StaticKeys.ActionType].ToString() == "M")
                Response.Redirect("BOMRecipeMassProcess.aspx", false);
            else if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
            {
                //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "187")
                    || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                    //Response.Redirect("BOMHeaderComp.aspx");
                    Response.Redirect("BOMHeaderComp.aspx", false);
                else if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "185")
                    || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                    Response.Redirect("Reciepe.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "188"))
                    Response.Redirect("BOMRecipeChange.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "B" || Session[StaticKeys.ActionType].ToString() == "U")
            {
                //if ((Session[StaticKeys.SelectedModuleId].ToString() == "189") || (Session[StaticKeys.SelectedModuleId].ToString() == "190"))
                Response.Redirect("BOMRecipeBlock.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnView_Click", ex); }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "M";
            lblPk.Text = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = lblPk.Text;
            Session[StaticKeys.Mode] = "M";
            Session[StaticKeys.MaterialType] = "";


            if (Session[StaticKeys.ActionType].ToString() == "M")
                Response.Redirect("BOMRecipeMassProcess.aspx", false);
            else if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
            {
                //if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "186"))
                //    UpdateRcpProdLockStatus();
                //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "187")
                    || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                    //Response.Redirect("BOMHeaderComp.aspx");
                    Response.Redirect("BOMHeaderComp.aspx", false);
                else if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "185")
                    || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                    Response.Redirect("Reciepe.aspx", false);

            }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "188"))
                    Response.Redirect("BOMRecipeChange.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "B" || Session[StaticKeys.ActionType].ToString() == "U")
            {
                //if ((Session[StaticKeys.SelectedModuleId].ToString() == "189") || (Session[StaticKeys.SelectedModuleId].ToString() == "190"))
                Response.Redirect("BOMRecipeBlock.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnModify_Click", ex); }
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdSearch.PageIndex = e.NewPageIndex;
            ReadBOMRecipeMasterRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearch_PageIndexChanging", ex); }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsValidSearch() == true)
        {
            try
            {
                ReadBOMRecipeMasterRequests();
            }
            catch (Exception ex)
            { _log.Error("btnSearch_Click", ex); }

        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Please select valid date range and date range should not exceed 90 days.";
        }
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
                _log.Error("Exception" + ex.Message);
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

    protected void btnMassSubmit_Click(object sender, EventArgs e)
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
            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (objRecipeAccess.GenerateMassRequestProcess(Req_Id, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
            {
                ReadBOMRecipeMasterRequests();
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
            //throw ex;
            _log.Error("btnMassSubmit_Click", ex);
        }

        //txtRequestNo.Text = Req_Id;
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

            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (objRecipeAccess.DeleteMassRequest(Req_Id, lblUserId.Text) > 0)
            {
                ReadBOMRecipeMasterRequests();
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
            //throw ex;
            _log.Error("btnDelete_Click", ex);
        }
    }

    protected void ddlPlantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPlantType.SelectedValue != "")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetFilterPlantList " + ddlPlantType.SelectedValue, "Plant_Name", "Plant_Id", "");
            }
        }
        catch (Exception ex)
        { _log.Error("ddlPlantType_SelectedIndexChanged", ex); }
    }

    protected void ddlPlantTypeC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPlantTypeC.SelectedValue != "")
            {
                helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetFilterPlantList " + ddlPlantTypeC.SelectedValue, "Plant_Name", "Plant_Id", "");
            }
        }
        catch (Exception ex)
        { _log.Error("ddlPlantTypeC_SelectedIndexChanged", ex); }

    }

    protected void ddlPlantTypeB_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPlantTypeB.SelectedValue != "")
            {
                helperAccess.PopuplateDropDownList(ddlPlantB, "pr_GetFilterPlantList " + ddlPlantTypeB.SelectedValue, "Plant_Name", "Plant_Id", "");
            }
        }
        catch (Exception ex)
        { _log.Error("ddlPlantTypeB_SelectedIndexChanged", ex); }
    }

    protected void ddlPlantTypeCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPlantTypeCopy.SelectedValue != "")
            {
                helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetFilterPlantList " + ddlPlantTypeCopy.SelectedValue, "Plant_Name", "Plant_Id", "");
            }
        }
        catch (Exception ex)
        { _log.Error("ddlPlantTypeCopy_SelectedIndexChanged", ex); }
    }

    protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
    {
        RecipeAccess recipeAccess = new RecipeAccess();
        ddlPlantGroupC.SelectedValue = "2";
        try
        {
            if (CheckedChangeFeilds())
            {
                string mode = lblMode.Text;
                if (ddlPlantTypeC.SelectedValue != "")
                    mode = ddlPlantTypeC.SelectedValue.ToString();
                Session[StaticKeys.PlantType] = mode;

                int MasterHeaderId = recipeAccess.GenerateChangeBulkRequest("0", "BRC", lblUserId.Text, mode, ddlPlantGroupC.SelectedValue, ddlPlantC.SelectedValue, ddlModuleC.SelectedValue);
                if (MasterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = recipeAccess.mModule_Id.ToString();
                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupC.SelectedValue;
                    Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                    Session[StaticKeys.MaterialPlantId] = ddlPlantC.SelectedValue;
                    Session[StaticKeys.MaterialProcessModuleId] = "188";
                    Session[StaticKeys.MatStorageLocationId] = "";

                    Session[StaticKeys.MaterialPlantName] = ddlPlantC.SelectedItem.Text;
                    Session[StaticKeys.MatStorageLocationName] = "";

                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.MaterialType] = "";
                    Session[StaticKeys.ActionType] = "C";
                    Session[StaticKeys.MaterialNo] = "Bulk Request";
                    Session[StaticKeys.RequestNo] = recipeAccess.mRequestNo;
                    //Session[StaticKeys.SelectedModule] = "BOM Recipe Bulk Change";
                    Session[StaticKeys.SelectedModule] = "BOM Recipe Bulk Change" + "[" + ddlModuleC.SelectedItem.Text + "]";
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.RequestStatus] = "I";

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("BOMRecipeChange.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("btnChangeBulkRequest_Click", ex);
        }
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
                Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                Label lblActionType = grv.FindControl("lblActionType") as Label;
                Label lblPlantType = grv.FindControl("lblPlantType") as Label;
                string ActionType = lblActionType.Text;

                if (ActionType == "N" || ActionType == "R")
                {
                    ddlModuleCopy.SelectedValue = lblModuleId.Text;
                    ddlPlantCopy.SelectedValue = lblPlantId.Text;
                    ddlPlantTypeCopy.SelectedValue = lblPlantType.Text;
                    ddlModuleCopy.Enabled = false;
                    ddlPlantCopy.Enabled = false;
                    ddlPlantTypeCopy.Enabled = false;
                }
                else
                {
                    ddlModuleCopy.SelectedValue = "";
                    ddlPlantCopy.SelectedValue = "";
                    ddlPlantTypeCopy.SelectedValue = "";
                }
            }

        }
        catch (Exception ex)
        { _log.Error("btnCopy_Click", ex); }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        int masterHeaderId;
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
                if (ddlPlantTypeCopy.SelectedValue != "")
                    mode = ddlPlantTypeCopy.SelectedValue.ToString();

                masterHeaderId = ObjMasterAccess.GenerateCopyRequestM(GetSelectedPkID(), ddlModuleCopy.SelectedValue, ddlPlantGroupCopy.SelectedValue, lblUserId.Text, mode, ddlPlantCopy.SelectedValue);

                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = ddlModuleCopy.SelectedValue;
                    Session[StaticKeys.MaterialPlantId] = ddlPlantCopy.SelectedValue;

                    Session[StaticKeys.MaterialPlantName] = ddlPlantCopy.SelectedItem.Text;

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupCopy.SelectedValue;
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = ddlModuleCopy.SelectedItem.Text;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestStatus] = "I";
                    Session[StaticKeys.RequestNo] = ObjMasterAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                    if (Session[StaticKeys.ActionType].ToString() == "M")
                        Response.Redirect("BOMRecipeMassProcess.aspx", false);
                    else if (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R")
                    {
                        //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                        if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "187")
                            || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                            //Response.Redirect("BOMHeaderComp.aspx");
                            Response.Redirect("BOMHeaderComp.aspx", false);
                        else if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "185")
                            || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                            Response.Redirect("Reciepe.aspx", false);
                    }
                    else if (Session[StaticKeys.ActionType].ToString() == "C")
                    {
                        if ((Session[StaticKeys.SelectedModuleId].ToString() == "188"))
                            Response.Redirect("BOMRecipeChange.aspx", false);
                    }
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
            //throw ex;
            _log.Error("btnCopy_Click", ex);
        }
    }

    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckedBlockFeilds())
            {
                string Module = "189";
                MasterAccess objMasterAccess = new MasterAccess();
                int MasterHeaderId = objMasterAccess.GenerateBlockRequestM("0", ddlPlantGroupB.SelectedValue, Module, "BRB", lblUserId.Text, "", "", ddlPlantTypeB.SelectedValue, ddlPlantB.SelectedValue);
                if (MasterHeaderId > 0)
                {

                    Session[StaticKeys.SelectedModuleId] = Module;
                    Session[StaticKeys.MaterialPlantId] = ddlPlantB.SelectedValue;
                    //Session[StaticKeys.MaterialProcessModuleId] = ddlModuleB.SelectedValue;
                    Session[StaticKeys.MatStorageLocationId] = "";

                    Session[StaticKeys.MaterialPlantName] = ddlPlantB.SelectedItem.Text;
                    Session[StaticKeys.MatStorageLocationName] = "";

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroupB.SelectedValue;
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = "BOM Recipe Master Block";
                    Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "B";
                    Session[StaticKeys.RequestStatus] = "I";
                    Session[StaticKeys.MaterialNo] = "Block Request";
                    Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Response.Redirect("BOMRecipeBlock.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("btnBlockRequest_Click", ex);
        }
    }

    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        try
        {
            string Module = "190";
            MasterAccess objMasterAccess = new MasterAccess();
            int MasterHeaderId = objMasterAccess.GenerateBlockRequestM("0", ddlPlantGroupB.SelectedValue, Module, "BRU", lblUserId.Text, "", "", ddlPlantTypeB.SelectedValue, ddlPlantB.SelectedValue);

            if (MasterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = Module;
                Session[StaticKeys.MaterialPlantId] = ddlPlantB.SelectedValue;
                //Session[StaticKeys.MaterialProcessModuleId] = ddlModuleB.SelectedValue;
                Session[StaticKeys.MatStorageLocationId] = "";

                Session[StaticKeys.MaterialPlantName] = ddlPlantB.SelectedItem.Text;
                Session[StaticKeys.MatStorageLocationName] = "";

                Session[StaticKeys.MatPlantGrp] = ddlPlantGroupB.SelectedValue;
                Session[StaticKeys.MassRequestProcessId] = "0";

                Session[StaticKeys.SelectedModule] = "BOM Recipe Master UnBlock";
                Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "U";
                Session[StaticKeys.RequestStatus] = "I";
                Session[StaticKeys.MaterialNo] = "Unblock Request";
                Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("BOMRecipeBlock.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnUnBlockRequest_Click", ex); }
    }

    #endregion

    #region Methods

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
                    Label lblPlantType = grv.FindControl("lblPlantType") as Label;
                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                    Label lblPlantName = grv.FindControl("lblPlantName") as Label;
                    Label lblReqStatus = grv.FindControl("lblReqStatus") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MassRequestProcessId] = lblMassRequestProcessId.Text;
                    //Session[StaticKeys.MaterialNo] = (lblMasterCode.Text == "" ? "New Request" : lblMasterCode.Text) + " - " + lblMaterialShortDescription.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;
                    Session[StaticKeys.MaterialPlantName] = lblPlantName.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.PlantType] = lblPlantType.Text;
                    Session[StaticKeys.RequestStatus] = lblReqStatus.Text;

                    Session[StaticKeys.BOMRecipeMatNo] = "";
                    Session[StaticKeys.BOMRecipeMatDesc] = "";

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

    private void PopulateDropDownList()
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
            {
                ddlStatus.Items.Insert(1, new ListItem("Pending For Review", "REV"));
                ddlStatus.Items.Insert(2, new ListItem("Pending For Final", "FIN"));
            }

            helperAccess.PopuplateDropDownList(ddlPlantGroup, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroup.SelectedValue = "1";

            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlPlantC, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlPlantCopy, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");

            helperAccess.PopuplateDropDownList(ddlPlantGroupC, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupC.SelectedValue = "1";

            helperAccess.PopuplateDropDownList(ddlPlantGroupCopy, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupCopy.SelectedValue = "1";

            helperAccess.PopuplateDropDownList(ddlPlantGroupB, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
            ddlPlantGroupB.SelectedValue = "1";
        }
        catch (Exception ex)
        { _log.Error("PopulateDropDownList", ex); }
    }

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        BOMAccess objBOMAccess = new BOMAccess();
        Utility ObjUtil = new Utility();

        try
        {
            DataSet ds = objBOMAccess.ReadProfileWiseModules(profileId, userId, "B");

            ddlModule.DataSource = ds.Tables[0];
            ddlModule.DataTextField = "Module_Name";
            ddlModule.DataValueField = "Module_Id";
            ddlModule.DataBind();

            ddlModuleCopy.DataSource = ds.Tables[0];
            ddlModuleCopy.DataTextField = "Module_Name";
            ddlModuleCopy.DataValueField = "Module_Id";
            ddlModuleCopy.DataBind();

            ddlModuleC.DataSource = ds.Tables[0];
            ddlModuleC.DataTextField = "Module_Name";
            ddlModuleC.DataValueField = "Module_Id";
            ddlModuleC.DataBind();

            bool flg = true;

            if (ddlModule.Items.Count > 1)
            {
                ListItem lst = new ListItem("Ready for Submit", "SUB");
                ddlStatus.Items.Add(lst);
                ddlStatus.SelectedValue = "SUB";

                //dstData = dstData;
                // dstData = objBOMAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, "", "B", "", ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

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
            //throw ex;
            _log.Error("ReadProfileWiseModules", ex);
        }
    }

    private void ReadBOMRecipeMasterRequests()
    {
        RecipeAccess objRecipeAccess = new RecipeAccess();
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;
        try
        {
            dstData = objRecipeAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, "", "B", "", ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            btnMassSubmit.Visible = false;
            btnDelete.Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[0].Visible = true;
                    //BOM_NWF_SDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = false;
                    //grdSearch.Columns[9].Visible = true;
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = false;
                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    //BOM_NWF_EDT05072019 Updated by NR


                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[0].Visible = true;
                    //BOM_NWF_SDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = false;
                    //grdSearch.Columns[9].Visible = true;
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = true;
                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    //BOM_NWF_EDT05072019 Updated by NR

                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[0].Visible = true;
                    //BOM_NWF_SDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = true;
                    //grdSearch.Columns[9].Visible = false;
                    //grdSearch.Columns[10].Visible = true;
                    //grdSearch.Columns[11].Visible = true;
                    //grdSearch.Columns[12].Visible = true;
                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = true;
                    //BOM_NWF_EDT05072019 Updated by NR

                    btnModify.Visible = true;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    //BOM_NWF_EDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = true;
                    //grdSearch.Columns[9].Visible = false;
                    //grdSearch.Columns[10].Visible = true;
                    //grdSearch.Columns[11].Visible = true;
                    //grdSearch.Columns[12].Visible = false;

                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    //BOM_NWF_EDT05072019 Updated by NR

                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[0].Visible = true;
                    //BOM_NWF_SDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = false;
                    //grdSearch.Columns[9].Visible = false;
                    //grdSearch.Columns[10].Visible = true;
                    //grdSearch.Columns[11].Visible = true;
                    //grdSearch.Columns[12].Visible = false;
                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    //BOM_NWF_EDT05072019 Updated by NR

                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[0].Visible = true;
                    //BOM_NWF_SDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = false;
                    //grdSearch.Columns[9].Visible = false;
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = false;
                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    //BOM_NWF_EDT05072019 Updated by NR

                    btnModify.Visible = true;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "SUB")
                {
                    //BOM_NWF_SDT05072019 Commeted by NR
                    //grdSearch.Columns[8].Visible = false;
                    //grdSearch.Columns[9].Visible = true;
                    //grdSearch.Columns[10].Visible = false;
                    //grdSearch.Columns[11].Visible = false;
                    //grdSearch.Columns[12].Visible = true;
                    //BOM_NWF_EDT05072019 Commeted by NR

                    //BOM_NWF_SDT05072019 Updated by NR
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    //BOM_NWF_EDT05072019 Updated by NR


                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = false;
                    btnDelete.Visible = true;
                    btnMassSubmit.Visible = true;
                }

            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                //btnCopyRequest.Visible = false;
            }

            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("ReadBOMRecipeMasterRequests", ex);
        }

    }

    protected bool CheckedCreationFeilds()
    {
        bool bflag = false;
        try
        {
            lblMsg.Text = "";
            if (reqddlModule.Visible && ddlModule.SelectedValue == "")
                lblMsg.Text = "Plant type is Mandatory. ";
            if (reqddlPlant.Visible && ddlPlant.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";

            if (lblMsg.Text == "")
            {
                bflag = true;
                return bflag;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowCreateNewDialog();", true);
                //return false;
                bflag = false;
                return bflag;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("CheckedChangeFeilds", ex);
        }
        return bflag;
    }

    private bool CheckedChangeFeilds()
    {
        bool bflag = false;
        try
        {
            lblMsg.Text = "";
            if (reqddlPlantTypeC.Visible && ddlPlantTypeC.SelectedValue == "")
                lblMsg.Text = "Plant Type is Mandatory. ";
            if (reqddlPlantC.Visible && ddlPlantC.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";

            if (lblMsg.Text == "")
            {
                //return true;
                bflag = true;
                return bflag;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowChangeBulkRequestDialog();", true);
                //return false;
                bflag = false;
                return bflag;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("CheckedChangeFeilds", ex);
        }
        return bflag;
    }

    private void UpdateRcpProdLockStatus()
    {
        try
        {
            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {
                objRecipeAccess.UpdateProdLockStatusByMasthdrId(Session[StaticKeys.MasterHeaderId].ToString(), lblUserId.Text);
            }
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            else if ((Session[StaticKeys.RecipeGroup].ToString() != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
            {
                objRecipeAccess.UpdateProdLockStatusByMasthdrId(Session[StaticKeys.MasterHeaderId].ToString(), lblUserId.Text);
            }
        }
        catch (Exception ex)
        { _log.Error("UpdateRcpProdLockStatus", ex); }
    }

    protected bool CheckedBlockFeilds()
    {
        bool bflag = false;
        try
        {
            lblMsg.Text = "";
            if (reqddlPlantB.Visible && ddlPlantB.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";


            if (lblMsg.Text == "")
            {
                //return true;
                bflag = true;
                return bflag;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowBlockDialog();", true);
                //return false;
                bflag = false;
                return bflag;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("CheckedBlockFeilds", ex);
        }
        //
        return bflag;
    }

    #endregion
}