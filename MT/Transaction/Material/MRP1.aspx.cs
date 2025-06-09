using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;
using System.IO;
using log4net;
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_MRP1 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MRPDataAccess ObjMRPDataAccess = new MRPDataAccess();
    HelperAccess helperAccess = new HelperAccess();
    MRP1 objSavedMRP1 = new MRP1();
    string sdate = "";
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogM1" + sdate + ".txt", "Start" + HelperAccess.ReqType);
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load", ex);
        }
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                        if (!btnPrevious.Visible && !btnNext.Visible)
                            btnSave.Visible = false;
                        //MSC_8300001775 Start
                        if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)//(HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                        {
                            btnSave.Visible = true;
                        }
                        //MSC_8300001775 End 
                    }
                    //FillDataGrid();
                    ClearData();
                    ConfigureControl();

                    //To manage the Creation Single request
                    FillFormDataByMHId();
                    lnlAddDetails.Visible = false;
                    grvData.Visible = false;
                    //MSC_8300001775 Start
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
                    {
                        ConfigureControlForSChange();
                    }
                    //MSC_8300001775 End
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load1", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
        ComtxtRoundingValue.Visible = false;
        txtMinLotSize.Enabled = true;
        txtMinLotSize.CssClass = "textbox";
        txtMaxLotSize.Enabled = true;
        txtMaxLotSize.CssClass = "textbox";
        txtRoundingValue.Enabled = true;
        txtRoundingValue.CssClass = "textbox";
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }
    //ComtxtRoundingValue

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
        ClearData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMRPId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_MRP1_Id"].ToString();
            FillMRPData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveMRP())
        {

            if ((lblModuleId.Text == "162" || lblModuleId.Text == "164" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
                    Response.Redirect("Purchasing.aspx");
                if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                    Response.Redirect("ForeignTrade.aspx");
            }
            else
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }

            }
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveMRP())
        {
            //lblMsg.Text = Messages.GetMessage(1);
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
            //FillFormDataByMHId();
            ////string pageURL = btnNext.CommandArgument.ToString();
            ////Response.Redirect(pageURL);
            ///

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            FillFormDataByMHId();
            Response.Redirect("MRP1.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveMRP())
        {
                //8400000410 comment Start
                //if (((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164)) && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("PlantStorageLocation.aspx");
                //}
                //else if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("MRP3.aspx");
                //}
                //else
                //{
                //    string pageURL = btnNext.CommandArgument.ToString();
                //    Response.Redirect(pageURL);
                //}
                //8400000410 comment end
                //8400000410 add Start
                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);
                //8400000410 add End
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        BindPlantWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    protected void ddlMrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        MRPTypeWiseSetup();
        }
        catch (Exception ex)
        { _log.Error("ddlMrpType_SelectedIndexChanged", ex); }
    }

    protected void ddlLotSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        LotSizeWiseValidation();
        }
        catch (Exception ex)
        { _log.Error("ddlLotSize_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Method

    private void MRPTypeWiseSetup()
    {
        try
        {
        if (ddlMrpType.SelectedValue == "ND")
        {
            reqddlMrpController.Visible = false;
            reqddlLotSize.Visible = false;
            reqtxtReorder.Visible = false;
            //MSC_8300001775 Start
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
            {
                lableddlMrpController.Visible = true;
                lableddlLotSize.Visible = true;
                labletxtReorder.Visible = true;

                //LotSizeWiseValidation();

                ddlMrpController.Enabled = true;
                ddlLotSize.Enabled = true;
                txtReorder.Enabled = true;
                txtFixedLotSize.Enabled = true;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;
                txtPlanningTimeFence.Enabled = true;
                txtMaxStockLevel.Enabled = true;
            } //MSC_8300001775 Start
            else
            {

                lableddlMrpController.Visible = false;
                lableddlLotSize.Visible = false;
                labletxtReorder.Visible = false;

                LotSizeWiseValidation();

                ddlMrpController.Enabled = false;
                ddlLotSize.Enabled = false;
                txtReorder.Enabled = false;
                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = false;
                txtMaxLotSize.Enabled = false;
                txtRoundingValue.Enabled = false;
                txtPlanningTimeFence.Enabled = false;
                txtMaxStockLevel.Enabled = false;
            }

        }
        else if (ddlMrpType.SelectedValue == "PD" || ddlMrpType.SelectedValue == "X0")
        {
            reqddlMrpController.Visible = true;
            reqddlLotSize.Visible = true;
            reqtxtReorder.Visible = false;

            lableddlMrpController.Visible = true;
            lableddlLotSize.Visible = true;
            labletxtReorder.Visible = false;

            ddlMrpController.Enabled = true;
            ddlLotSize.Enabled = true;
            txtReorder.Enabled = true;
            txtFixedLotSize.Enabled = true;
            txtMinLotSize.Enabled = true;
            txtMaxLotSize.Enabled = true;
            txtRoundingValue.Enabled = true;
            txtPlanningTimeFence.Enabled = true;
            txtMaxStockLevel.Enabled = true;
        }
        else if (ddlMrpType.SelectedValue == "VB")
        {
            reqddlMrpController.Visible = true;
            reqddlLotSize.Visible = true;
            reqtxtReorder.Visible = true;

            lableddlMrpController.Visible = true;
            lableddlLotSize.Visible = true;
            labletxtReorder.Visible = true;

            ddlMrpController.Enabled = true;
            ddlLotSize.Enabled = true;
            txtReorder.Enabled = true;
            txtFixedLotSize.Enabled = true;
            txtMinLotSize.Enabled = true;
            txtMaxLotSize.Enabled = true;
            txtRoundingValue.Enabled = true;
            txtPlanningTimeFence.Enabled = true;
            txtMaxStockLevel.Enabled = true;
            //MSC_8300001775 Start
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
            {
                //LotSizeWiseValidation();
            }//MSC_8300001775 Start
            else
            {
                LotSizeWiseValidation();
            }
            }
        }
        catch (Exception ex)
        { _log.Error("MRPTypeWiseSetup", ex); }
    }

    //private void MRPTypeWiseSetup()
    //{
    //    if (ddlMrpType.SelectedValue == "ND")
    //    {
    //        reqddlMrpController.Visible = false;
    //        reqddlLotSize.Visible = false;
    //        reqtxtReorder.Visible = false;


    //        ddlMrpController.SelectedValue = "";
    //        ddlLotSize.SelectedValue = "";

    //        lableddlMrpController.Visible = false;
    //        lableddlLotSize.Visible = false;
    //        labletxtReorder.Visible = false;

    //        LotSizeWiseValidation();

    //        reqtxtFixedLotSize.Visible = false;
    //        reqtxtMinLotSize.Visible = false;
    //        reqtxtMaxLotSize.Visible = false;
    //        reqtxtRoundingValue.Visible = false;

    //        ddlMrpController.Enabled = false;
    //        ddlLotSize.Enabled = false;
    //        txtReorder.Enabled = false;
    //        txtFixedLotSize.Enabled = false;
    //        txtMinLotSize.Enabled = false;
    //        txtMaxLotSize.Enabled = false;
    //        txtRoundingValue.Enabled = false;

    //    }
    //    else if (ddlMrpType.SelectedValue == "PD")
    //    {
    //        reqddlMrpController.Visible = true;
    //        reqddlLotSize.Visible = true;
    //        reqtxtReorder.Visible = false;

    //        lableddlMrpController.Visible = true;
    //        lableddlLotSize.Visible = true;
    //        labletxtReorder.Visible = false;

    //        reqtxtFixedLotSize.Visible = true;
    //        reqtxtMinLotSize.Visible = true;
    //        reqtxtMaxLotSize.Visible = true;
    //        reqtxtRoundingValue.Visible = true;

    //        ddlMrpController.Enabled = true;
    //        ddlLotSize.Enabled = true;
    //        txtReorder.Enabled = true;
    //        txtFixedLotSize.Enabled = true;
    //        txtMinLotSize.Enabled = true;
    //        txtMaxLotSize.Enabled = true;
    //        txtRoundingValue.Enabled = true;
    //    }
    //    else if (ddlMrpType.SelectedValue == "VB")
    //    {
    //        reqddlMrpController.Visible = true;
    //        reqddlLotSize.Visible = true;
    //        reqtxtReorder.Visible = true;

    //        reqtxtFixedLotSize.Visible = true;
    //        reqtxtMinLotSize.Visible = true;
    //        reqtxtMaxLotSize.Visible = true;
    //        reqtxtRoundingValue.Visible = true;

    //        ddlMrpController.Enabled = true;
    //        ddlLotSize.Enabled = true;
    //        txtReorder.Enabled = true;
    //        txtFixedLotSize.Enabled = true;
    //        txtMinLotSize.Enabled = true;
    //        txtMaxLotSize.Enabled = true;
    //        txtRoundingValue.Enabled = true;

    //        lableddlMrpController.Visible = true;
    //        lableddlLotSize.Visible = true;
    //        labletxtReorder.Visible = true;

    //        LotSizeWiseValidation();
    //    }
    //}

    private void LotSizeWiseValidation()
    {
        try
        {
        if (ddlMrpType.SelectedValue != "ND")
        {
            if (ddlLotSize.SelectedValue == "FX")
            {
                reqtxtFixedLotSize.Visible = true;
                labletxtFixedLotSize.Visible = true;

                reqtxtMaxStockLevel.Visible = false;
                labletxtMaxStockLevel.Visible = false;

                txtFixedLotSize.Enabled = true;
                txtMinLotSize.Enabled = false;
                txtMaxLotSize.Enabled = false;
                txtRoundingValue.Enabled = false;

                txtMinLotSize.Text = "";
                txtMaxLotSize.Text = "";
                txtRoundingValue.Text = "";
            }
            else if (ddlLotSize.SelectedValue == "HB")
            {
                reqtxtFixedLotSize.Visible = false;
                labletxtFixedLotSize.Visible = false;

                reqtxtMaxStockLevel.Visible = true;
                labletxtMaxStockLevel.Visible = true;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;

                txtFixedLotSize.Text = "";
            }
            else
            {
                reqtxtFixedLotSize.Visible = false;
                labletxtFixedLotSize.Visible = false;

                reqtxtMaxStockLevel.Visible = false;
                labletxtMaxStockLevel.Visible = false;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;

                txtFixedLotSize.Text = "";
            }
        }
        else
        {
            //MSC_8300001775 Start
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
            {

            }
            else
            {
                ddlMrpController.SelectedValue = "";
                ddlLotSize.SelectedValue = "";


                reqtxtFixedLotSize.Visible = false;
                labletxtFixedLotSize.Visible = false;

                reqtxtMaxStockLevel.Visible = false;
                labletxtMaxStockLevel.Visible = false;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = false;
                txtMaxLotSize.Enabled = false;
                txtRoundingValue.Enabled = false;

                txtFixedLotSize.Text = "";
            }
        }

        }
        catch (Exception ex)
        { _log.Error("LotSizeWiseValidation", ex); }
    }

    //private void LotSizeWiseValidation()
    //{
    //    if (ddlMrpType.SelectedValue != "ND")
    //    {
    //        if (ddlLotSize.SelectedValue == "FX")
    //        {
    //            reqtxtFixedLotSize.Visible = true;
    //            reqtxtMinLotSize.Visible = false;
    //            reqtxtMaxLotSize.Visible = false;
    //            reqtxtRoundingValue.Visible = false;

    //            labletxtFixedLotSize.Visible = true;
    //            labletxtMinLotSize.Visible = false;
    //            labletxtMaxLotSize.Visible = false;
    //            labletxtRoundingValue.Visible = false;

    //            txtFixedLotSize.Enabled = true;
    //            txtMinLotSize.Enabled = false;
    //            txtMaxLotSize.Enabled = false;
    //            txtRoundingValue.Enabled = false;

    //            txtMinLotSize.Text = "";
    //            txtMaxLotSize.Text = "";
    //            txtRoundingValue.Text = "";
    //        }
    //        else if (ddlLotSize.SelectedValue == "HB")
    //        {
    //            reqtxtFixedLotSize.Visible = false;
    //            reqtxtMinLotSize.Visible = true;
    //            reqtxtMaxLotSize.Visible = true;
    //            reqtxtRoundingValue.Visible = true;

    //            labletxtFixedLotSize.Visible = false;
    //            labletxtMinLotSize.Visible = true;
    //            labletxtMaxLotSize.Visible = true;
    //            labletxtRoundingValue.Visible = true;

    //            txtFixedLotSize.Enabled = false;
    //            txtMinLotSize.Enabled = true;
    //            txtMaxLotSize.Enabled = true;
    //            txtRoundingValue.Enabled = true;

    //            txtFixedLotSize.Text = "";
    //        }
    //        else
    //        {
    //            reqtxtFixedLotSize.Visible = false;
    //            reqtxtMinLotSize.Visible = true;
    //            reqtxtMaxLotSize.Visible = true;
    //            reqtxtRoundingValue.Visible = true;

    //            labletxtFixedLotSize.Visible = false;
    //            labletxtMinLotSize.Visible = true;
    //            labletxtMaxLotSize.Visible = true;
    //            labletxtRoundingValue.Visible = true;

    //            txtFixedLotSize.Enabled = false;
    //            txtMinLotSize.Enabled = true;
    //            txtMaxLotSize.Enabled = true;
    //            txtRoundingValue.Enabled = true;

    //            txtFixedLotSize.Text = "";
    //        }
    //    }
    //    else
    //    {
    //        reqtxtFixedLotSize.Visible = false;
    //        reqtxtMinLotSize.Visible = false;
    //        reqtxtMaxLotSize.Visible = false;
    //        reqtxtRoundingValue.Visible = false;

    //        labletxtFixedLotSize.Visible = false;
    //        labletxtMinLotSize.Visible = false;
    //        labletxtMaxLotSize.Visible = false;
    //        labletxtRoundingValue.Visible = false;

    //        txtFixedLotSize.Enabled = false;
    //        txtMinLotSize.Enabled = false;
    //        txtMaxLotSize.Enabled = false;
    //        txtRoundingValue.Enabled = false;

    //        txtFixedLotSize.Text = "";
    //        txtMinLotSize.Text = "";
    //        txtMaxLotSize.Text = "";
    //        txtRoundingValue.Text = "";
    //    }
    //}

    private void PopuplateDropDownList()
    {
        try
        {
        //CTRL_SUB_SDT18112019 Commented by NR
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMrpGroup, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpGroup','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAbcIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlAbcIndicator'", "LookUp_Desc", "LookUp_Code", "");
        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
        {
            helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleTypeChange 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
        }
        helperAccess.PopuplateDropDownList(ddlPlanningCycle, "pr_GetDropDownListByControlNameModuleType 'M','ddlPlanningCycle'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlLotSize, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRoundingProfile, "pr_GetDropDownListByControlNameModuleType 'M','ddlRoundingProfile'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblMRPId.Text = "0";

            //ddlMrpType.SelectedValue = "";
            //ddlMrpController.SelectedValue = "";
            txtReorder.Text = "";
            //ddlLotSize.SelectedValue = "";
            txtMinLotSize.Text = "";
            txtMaxLotSize.Text = "";
            txtFixedLotSize.Text = "";
            txtRoundingValue.Text = "";
            txtMaxStockLevel.Text = "";
            //ddlAbcIndicator.SelectedValue = "";
            txtScrap.Text = "";
            txtPlanningTimeFence.Text = "";
            //ddlProductionUnit.SelectedValue = "";
            //ddlMrpGroup.SelectedValue = "";
            txtTaktTime.Text = "";
            //ddlPlanningCycle.SelectedValue = "";
            //ddlRoundingProfile.SelectedValue = "";
            //ddlUnitMeasurGroup.SelectedValue = "";

            //ClearSelectedValue(ddlPlant);
            PopuplateDropDownList();
        }
        catch (Exception ex)
        {
            _log.Error("ClearData", ex);
        }
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjMRPDataAccess.GetMRPData1(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
        }
    }

    private bool SaveMRP()
    {
        bool flg = false;
        try
        {
            MRP1 ObjMRP = GetControlsValue();
            objSavedMRP1 = GetMRPData();

            if (ObjMRP.Plant_Id != null)
            {
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                {
                    if (objSavedMRP1.Mat_MRP1_Id > 0)
                    {
                        CheckIfChanges(ObjMRP, objSavedMRP1);
                    }
                }

                if (ObjMRPDataAccess.Save(ObjMRP) > 0)
                {
                    //MSC_8300001775
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
                    {
                        CheckIfChangesLog(ObjMRP, objSavedMRP1);
                    }
                    //MSC_8300001775
                    //FillDataGrid();
                    ClearData();

                    flg = true;
                    ////MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    //{
                    //    CheckIfChangesLog(ObjMRP, objSavedMRP1);
                    //}
                    ////MSC_8300001775
                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Please Select atleast one Plant to proceed.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("SaveMRP", ex);
        }
        return flg;
    }

    private MRP1 GetMRPData()
    {
        return ObjMRPDataAccess.GetMRP1(Convert.ToInt32(lblMRPId.Text));
    }

    private MRP1 GetControlsValue()
    {
        MRP1 ObjMRP = new MRP1();
        Utility objUtil = new Utility();

        try
        {
            ObjMRP.Mat_MRP1_Id = Convert.ToInt32(lblMRPId.Text);
            ObjMRP.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjMRP.Plant_Id = ddlPlant.SelectedValue;
            ObjMRP.Storage_Location = ddlStorageLocation.SelectedValue;

            ObjMRP.Base_Unit_Of_Measure = "";
            ObjMRP.Purchasing_Group = "";

            ObjMRP.MRP_Type = ddlMrpType.SelectedValue;
            ObjMRP.MRP_Controller = ddlMrpController.SelectedValue;
            ObjMRP.Reorder_Point = txtReorder.Text;
            ObjMRP.Lot_Size = ddlLotSize.SelectedValue;
            ObjMRP.Min_Lot_Size = txtMinLotSize.Text;
            ObjMRP.Max_Lot_Size = txtMaxLotSize.Text;
            ObjMRP.Fixed_Lot_Size = txtFixedLotSize.Text;
            ObjMRP.Rounding_Value = txtRoundingValue.Text;
            ObjMRP.Max_Stock_Level = txtMaxStockLevel.Text;
            ObjMRP.ABC_Indicator = ddlAbcIndicator.SelectedValue;
            ObjMRP.Scrap = txtScrap.Text;
            ObjMRP.Planning_Time_Fence = txtPlanningTimeFence.Text;
            ObjMRP.Production_Unit = ddlProductionUnit.SelectedValue;
            ObjMRP.MRP_Group = ddlMrpGroup.SelectedValue;
            ObjMRP.Takt_Time = txtTaktTime.Text;
            ObjMRP.Planning_Cycle = ddlPlanningCycle.SelectedValue;
            ObjMRP.Rounding_Profile = ddlRoundingProfile.SelectedValue;
            ObjMRP.Unit_Measure_Grp = ddlUnitMeasurGroup.SelectedValue;
            ObjMRP.IsActive = 1;
            ObjMRP.UserId = lblUserId.Text;
            ObjMRP.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjMRP;
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        if (lblModuleId.Text == "138" || lblModuleId.Text == "147" || lblModuleId.Text == "163")
        {
            if (ddlStorageLocation.Items.FindByValue("0151") != null)
            {
                ddlStorageLocation.SelectedValue = "0151";
            }
        }

        helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMrpGroup, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpGroup','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
        ds = ObjMRPDataAccess.GetMRPData1(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMRPId.Text = ds.Tables[0].Rows[0]["Mat_MRP1_Id"].ToString();
        }
        FillMRPData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void FillMRPData()
    {
        MRP1 ObjMRP = GetMRPData();
        try
        {
            if (ObjMRP.Mat_MRP1_Id > 0)
            {
                lblMRPId.Text = ObjMRP.Mat_MRP1_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjMRP.Plant_Id;
                BindPlantWiseDropDown();

                ddlStorageLocation.SelectedValue = ObjMRP.Storage_Location;
                ddlMrpType.SelectedValue = ObjMRP.MRP_Type;
                MRPTypeWiseSetup();

                ddlMrpController.SelectedValue = ObjMRP.MRP_Controller;
                txtReorder.Text = ObjMRP.Reorder_Point;
                ddlLotSize.SelectedValue = ObjMRP.Lot_Size;
                LotSizeWiseValidation();

                txtMinLotSize.Text = ObjMRP.Min_Lot_Size.ToString();
                txtMaxLotSize.Text = ObjMRP.Max_Lot_Size.ToString();
                txtFixedLotSize.Text = ObjMRP.Fixed_Lot_Size.ToString();
                txtRoundingValue.Text = ObjMRP.Rounding_Value.ToString();
                txtMaxStockLevel.Text = ObjMRP.Max_Stock_Level.ToString();
                ddlAbcIndicator.SelectedValue = ObjMRP.ABC_Indicator;
                txtScrap.Text = ObjMRP.Scrap;
                txtPlanningTimeFence.Text = ObjMRP.Planning_Time_Fence.ToString();
                ddlProductionUnit.SelectedValue = ObjMRP.Production_Unit;
                ddlMrpGroup.SelectedValue = ObjMRP.MRP_Group;
                txtTaktTime.Text = ObjMRP.Takt_Time;
                ddlPlanningCycle.SelectedValue = ObjMRP.Planning_Cycle;
                ddlRoundingProfile.SelectedValue = ObjMRP.Rounding_Profile;
                ddlUnitMeasurGroup.SelectedValue = ObjMRP.Unit_Measure_Grp;
            }
            else
            {
                lblMRPId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP1','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
                ddlStorageLocation.SelectedValue = Session[StaticKeys.MatStorageLocationId].ToString();
                ddlMrpType.SelectedValue = "ND";
                MRPTypeWiseSetup();
            }

            ddlPlant.Enabled = false;
            ddlStorageLocation.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillMRPData", ex);
        }
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.MRP1 obj = new SectionConfiguration.MRP1();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(MRP1 NewMRP1Data, MRP1 oldMRP1Data)
    {
        try
        {
            if (NewMRP1Data.Mat_MRP1_Id > 0 && oldMRP1Data.Mat_MRP1_Id > 0)
            {
                if (NewMRP1Data.Plant_Id != oldMRP1Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldMRP1Data.Plant_Id + "</td><td>" + NewMRP1Data.Plant_Id + "</td></tr>";
                if (NewMRP1Data.Storage_Location != oldMRP1Data.Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Location</td> <td>" + oldMRP1Data.Storage_Location + "</td><td>" + NewMRP1Data.Storage_Location + "</td></tr>";
                if (NewMRP1Data.Base_Unit_Of_Measure != oldMRP1Data.Base_Unit_Of_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Base Unit Of Measure</td> <td>" + oldMRP1Data.Base_Unit_Of_Measure + "</td><td>" + NewMRP1Data.Base_Unit_Of_Measure + "</td></tr>";
                if (NewMRP1Data.Purchasing_Group != oldMRP1Data.Purchasing_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Purchasing Group</td> <td>" + oldMRP1Data.Purchasing_Group + "</td><td>" + NewMRP1Data.Purchasing_Group + "</td></tr>";
                if (NewMRP1Data.MRP_Type != oldMRP1Data.MRP_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MRP Type</td> <td>" + oldMRP1Data.MRP_Type + "</td><td>" + NewMRP1Data.MRP_Type + "</td></tr>";
                if (NewMRP1Data.MRP_Controller != oldMRP1Data.MRP_Controller)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MRP Controller</td> <td>" + oldMRP1Data.MRP_Controller + "</td><td>" + NewMRP1Data.MRP_Controller + "</td></tr>";
                if (NewMRP1Data.Reorder_Point != oldMRP1Data.Reorder_Point)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Reorder Point</td> <td>" + oldMRP1Data.Reorder_Point + "</td><td>" + NewMRP1Data.Reorder_Point + "</td></tr>";
                if (NewMRP1Data.Lot_Size != oldMRP1Data.Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Lot Size</td> <td>" + oldMRP1Data.Lot_Size + "</td><td>" + NewMRP1Data.Lot_Size + "</td></tr>";
                if (NewMRP1Data.Min_Lot_Size != oldMRP1Data.Min_Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Lot Size</td> <td>" + oldMRP1Data.Min_Lot_Size + "</td><td>" + NewMRP1Data.Min_Lot_Size + "</td></tr>";
                if (NewMRP1Data.Max_Lot_Size != oldMRP1Data.Max_Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Max Lot Size</td> <td>" + oldMRP1Data.Max_Lot_Size + "</td><td>" + NewMRP1Data.Max_Lot_Size + "</td></tr>";
                if (NewMRP1Data.Fixed_Lot_Size != oldMRP1Data.Fixed_Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Fixed Lot Size</td> <td>" + oldMRP1Data.Fixed_Lot_Size + "</td><td>" + NewMRP1Data.Fixed_Lot_Size + "</td></tr>";
                if (NewMRP1Data.Rounding_Value != oldMRP1Data.Rounding_Value)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Rounding Value</td> <td>" + oldMRP1Data.Rounding_Value + "</td><td>" + NewMRP1Data.Rounding_Value + "</td></tr>";
                if (NewMRP1Data.Max_Stock_Level != oldMRP1Data.Max_Stock_Level)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Max Stock Level</td> <td>" + oldMRP1Data.Max_Stock_Level + "</td><td>" + NewMRP1Data.Max_Stock_Level + "</td></tr>";
                if (NewMRP1Data.ABC_Indicator != oldMRP1Data.ABC_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>ABC Indicator</td> <td>" + oldMRP1Data.ABC_Indicator + "</td><td>" + NewMRP1Data.ABC_Indicator + "</td></tr>";
                if (NewMRP1Data.Scrap != oldMRP1Data.Scrap)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Scrap</td> <td>" + oldMRP1Data.Scrap + "</td><td>" + NewMRP1Data.Scrap + "</td></tr>";
                if (NewMRP1Data.Planning_Time_Fence != oldMRP1Data.Planning_Time_Fence)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planning Time Fence</td> <td>" + oldMRP1Data.Planning_Time_Fence + "</td><td>" + NewMRP1Data.Planning_Time_Fence + "</td></tr>";
                if (NewMRP1Data.Production_Unit != oldMRP1Data.Production_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Production Unit</td> <td>" + oldMRP1Data.Production_Unit + "</td><td>" + NewMRP1Data.Production_Unit + "</td></tr>";
                if (NewMRP1Data.MRP_Group != oldMRP1Data.MRP_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MRP Group</td> <td>" + oldMRP1Data.MRP_Group + "</td><td>" + NewMRP1Data.MRP_Group + "</td></tr>";
                if (NewMRP1Data.Takt_Time != oldMRP1Data.Takt_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Takt Time</td> <td>" + oldMRP1Data.Takt_Time + "</td><td>" + NewMRP1Data.Takt_Time + "</td></tr>";
                if (NewMRP1Data.Planning_Cycle != oldMRP1Data.Planning_Cycle)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planning Cycle</td> <td>" + oldMRP1Data.Planning_Cycle + "</td><td>" + NewMRP1Data.Planning_Cycle + "</td></tr>";
                if (NewMRP1Data.Rounding_Profile != oldMRP1Data.Rounding_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Rounding Profile</td> <td>" + oldMRP1Data.Rounding_Profile + "</td><td>" + NewMRP1Data.Rounding_Profile + "</td></tr>";
                if (NewMRP1Data.Unit_Measure_Grp != oldMRP1Data.Unit_Measure_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Measure Grp</td> <td>" + oldMRP1Data.Unit_Measure_Grp + "</td><td>" + NewMRP1Data.Unit_Measure_Grp + "</td></tr>";
            }

            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChanges", ex);
        }

    }

    //MSC_8300001775
    private void CheckIfChangesLog(MRP1 NewMRP1Data, MRP1 oldMRP1Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewMRP1Data.Mat_MRP1_Id > 0 && oldMRP1Data.Mat_MRP1_Id > 0)
            {
                if (NewMRP1Data.MRP_Type != oldMRP1Data.MRP_Type)
                {
                    WriteMatChangeLog("MatChangeLogM1" + sdate + ".txt", "51" + oldMRP1Data.MRP_Type + '-' + NewMRP1Data.MRP_Type);
               
                _items.Add(new SMChange { colFieldName = 51, colOldVal = oldMRP1Data.MRP_Type, colNewVal = NewMRP1Data.MRP_Type });
                }

                if (NewMRP1Data.MRP_Controller != oldMRP1Data.MRP_Controller)
                    _items.Add(new SMChange { colFieldName = 52, colOldVal = oldMRP1Data.MRP_Controller, colNewVal = NewMRP1Data.MRP_Controller });
                if (NewMRP1Data.Reorder_Point != oldMRP1Data.Reorder_Point)
                    _items.Add(new SMChange { colFieldName = 53, colOldVal = oldMRP1Data.Reorder_Point, colNewVal = NewMRP1Data.Reorder_Point });
                if (NewMRP1Data.Lot_Size != oldMRP1Data.Lot_Size)
                    _items.Add(new SMChange { colFieldName = 54, colOldVal = oldMRP1Data.Lot_Size, colNewVal = NewMRP1Data.Lot_Size });
                if (NewMRP1Data.Min_Lot_Size != oldMRP1Data.Min_Lot_Size)
                    _items.Add(new SMChange { colFieldName = 55, colOldVal = oldMRP1Data.Min_Lot_Size, colNewVal = NewMRP1Data.Min_Lot_Size });
                if (NewMRP1Data.Max_Lot_Size != oldMRP1Data.Max_Lot_Size)
                    _items.Add(new SMChange { colFieldName = 56, colOldVal = oldMRP1Data.Max_Lot_Size, colNewVal = NewMRP1Data.Max_Lot_Size });
                if (NewMRP1Data.Fixed_Lot_Size != oldMRP1Data.Fixed_Lot_Size)
                    _items.Add(new SMChange { colFieldName = 57, colOldVal = oldMRP1Data.Fixed_Lot_Size, colNewVal = NewMRP1Data.Fixed_Lot_Size });
                if (NewMRP1Data.Rounding_Value != oldMRP1Data.Rounding_Value)
                    _items.Add(new SMChange { colFieldName = 58, colOldVal = oldMRP1Data.Rounding_Value, colNewVal = NewMRP1Data.Rounding_Value });
                if (NewMRP1Data.Max_Stock_Level != oldMRP1Data.Max_Stock_Level)
                    _items.Add(new SMChange { colFieldName = 59, colOldVal = oldMRP1Data.Max_Stock_Level, colNewVal = NewMRP1Data.Max_Stock_Level });
                if (NewMRP1Data.ABC_Indicator != oldMRP1Data.ABC_Indicator)
                    _items.Add(new SMChange { colFieldName = 60, colOldVal = oldMRP1Data.ABC_Indicator, colNewVal = NewMRP1Data.ABC_Indicator });
                if (NewMRP1Data.Scrap != oldMRP1Data.Scrap)
                    _items.Add(new SMChange { colFieldName = 61, colOldVal = oldMRP1Data.Scrap, colNewVal = NewMRP1Data.Scrap });
                if (NewMRP1Data.Planning_Time_Fence != oldMRP1Data.Planning_Time_Fence)
                    _items.Add(new SMChange { colFieldName = 62, colOldVal = oldMRP1Data.Planning_Time_Fence, colNewVal = NewMRP1Data.Planning_Time_Fence });
                if (NewMRP1Data.Production_Unit != oldMRP1Data.Production_Unit)
                    _items.Add(new SMChange { colFieldName = 63, colOldVal = oldMRP1Data.Production_Unit, colNewVal = NewMRP1Data.Production_Unit });
                if (NewMRP1Data.MRP_Group != oldMRP1Data.MRP_Group)
                    _items.Add(new SMChange { colFieldName = 64, colOldVal = oldMRP1Data.MRP_Group, colNewVal = NewMRP1Data.MRP_Group });
                if (NewMRP1Data.Takt_Time != oldMRP1Data.Takt_Time)
                    _items.Add(new SMChange { colFieldName = 65, colOldVal = oldMRP1Data.Takt_Time, colNewVal = NewMRP1Data.Takt_Time });
                if (NewMRP1Data.Planning_Cycle != oldMRP1Data.Planning_Cycle)
                    _items.Add(new SMChange { colFieldName = 66, colOldVal = oldMRP1Data.Planning_Cycle, colNewVal = NewMRP1Data.Planning_Cycle });
                if (NewMRP1Data.Rounding_Profile != oldMRP1Data.Rounding_Profile)
                    _items.Add(new SMChange { colFieldName = 67, colOldVal = oldMRP1Data.Rounding_Profile, colNewVal = NewMRP1Data.Rounding_Profile });
                if (NewMRP1Data.Unit_Measure_Grp != oldMRP1Data.Unit_Measure_Grp)
                    _items.Add(new SMChange { colFieldName = 68, colOldVal = oldMRP1Data.Unit_Measure_Grp, colNewVal = NewMRP1Data.Unit_Measure_Grp });
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog", ex);
        }
        try
        {
            if (_items.Count > 0)
            {
                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("8", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
                if (ChangeSMatID1 > 0)
                {
                    foreach (var scItem in _items)
                    {
                        helperAccess.MaterialChangeDetails(ChangeSMatID1, scItem.colFieldName, scItem.colOldVal, scItem.colNewVal);
                    }
                }
                _items = new List<SMChange>();
            }
            //MSC_8300001775 End
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog1", ex);
        }

    }
    #endregion


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