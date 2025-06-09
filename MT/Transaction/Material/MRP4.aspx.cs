using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;
using log4net;
public partial class Transaction_MRP4 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MRPDataAccess ObjMRPDataAccess = new MRPDataAccess();
    HelperAccess helperAccess = new HelperAccess();
    MRP4 objSavedMRP4 = new MRP4();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, new SectionConfiguration.MRP4());
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.MasterHeaderId] != null)
                {
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
                        if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
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
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                    {
                        ConfigureControlForSChange();
                    }
                    //MSC_8300001775 End
                }
                else
                {
                    Response.Redirect("MaterialMaster.aspx");
                }
            }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {

        ddlSelectionMethod.Enabled = true;
        ddlDependentReq.Enabled = true;
        ddlMRPRelevanceDepReq.Enabled = true;


        ddlFairShareRule.Enabled = true;
        ddlIndiPushDistribution.Enabled = true;
        ddlStorageLocMrpIndi.Enabled = true;
        ddlSpecialProcTypeSloc.Enabled = true;
        txtReorderPointSLocMrp.Enabled = true;
        txtReorderPointSLocMrp.CssClass = "textbox";
        txtFixedLSizeStorage.Enabled = true;
        txtFixedLSizeStorage.CssClass = "textbox";
        chkIndRepetitiveMfg.Enabled = true;
        txtComponentScrap.Enabled = true;
        txtComponentScrap.CssClass = "textbox";
        ddlDiscontinuationIndi.Enabled = true;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }


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
        lblMRPId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_MRP4_Id"].ToString();
        FillMRPData();
        }
        catch (Exception ex)
        { _log.Error("lnkView_Click", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveMRP())
        {
            string pageURL = btnPrevious.CommandArgument.ToString();
            Response.Redirect(pageURL);
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
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            FillFormDataByMHId();
            Response.Redirect("MRP4.aspx");
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
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
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


    #endregion

    #region Method

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlDiscontinuationIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlDiscontinuationIndi'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlFollowUpMaterial, "pr_GetDropDownListByControlNameModuleType 'M','ddlFollowUpMaterial'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIndiReqGrouping, "pr_GetDropDownListByControlNameModuleType 'M','ddlIndiReqGrouping'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSpecialProcTypeSloc, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcTypeSloc','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlSelectionMethod, "pr_GetDropDownListByControlNameModuleType 'M','ddlSelectionMethod'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIndiPushDistribution, "pr_GetDropDownListByControlNameModuleType 'M','ddlIndiPushDistribution'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownList(ddlMRPRelevanceDepReq, "pr_GetDropDownListByControlNameModuleType 'M','ddlMRPRelevanceDepReq'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownList(ddlFairShareRule, "pr_GetDropDownListByControlNameModuleType 'M','ddlFairShareRule'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageLocMrpIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageLocMrpIndi'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownList(ddlDependentReq, "pr_GetDropDownListByControlNameModuleType 'M','ddlDependentReq'", "LookUp_Desc", "LookUp_Code");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
        lblMRPId.Text = "0";

        txtReorderPointSLocMrp.Text = "";
        txtFixedLSizeStorage.Text = "";
        chkIndRepetitiveMfg.Checked = false;
        txtComponentScrap.Text = "";
        txtEffectiveOutDate.Text = "";
        PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    private void FillDataGrid()
    {
        try
        {
        DataSet ds;
        ds = ObjMRPDataAccess.GetMRPData4(Convert.ToInt32(lblMasterHeaderId.Text));

        grvData.DataSource = ds;
        grvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDataGrid", ex); }
    }

    private bool SaveMRP()
    {
        bool flg = false;
        try
        {
        MRP4 ObjMRP = GetControlsValue();
        objSavedMRP4 = GetMRPData();

        if (ObjMRP.Plant_Id != null)
        {
            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedMRP4.Mat_MRP4_Id > 0)
                {
                    CheckIfChanges(ObjMRP, objSavedMRP4);
                }
            }
            if (ObjMRPDataAccess.Save(ObjMRP) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjMRP, objSavedMRP4);
                }
                //MSC_8300001775

                //FillDataGrid();
                ClearData();
                //FillFormDataByMHId();
                flg = true;
                ////MSC_8300001775
                //if (HelperAccess.ReqType == "SMC")
                //{
                //    CheckIfChangesLog(ObjMRP, objSavedMRP4);
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
        { _log.Error("SaveMRP", ex); }
        return flg;
    }

    private MRP4 GetMRPData()
    {
        return ObjMRPDataAccess.GetMRP4(Convert.ToInt32(lblMRPId.Text));
    }

    private MRP4 GetControlsValue()
    {
        MRP4 ObjMRP = new MRP4();
        Utility objUtil = new Utility();
        try
        {
        ObjMRP.Mat_MRP4_Id = Convert.ToInt32(lblMRPId.Text);
        ObjMRP.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjMRP.Plant_Id = ddlPlant.SelectedValue;
        ObjMRP.Storage_Location = ddlStorageLocation.SelectedValue;

        ObjMRP.Dependent_Req_Ind = ddlDependentReq.SelectedValue;
        ObjMRP.Indicator_Req_Grping = ddlIndiReqGrouping.SelectedValue;
        ObjMRP.Storage_Loc_MRP_Indicator = ddlStorageLocMrpIndi.SelectedValue;
        ObjMRP.ReOrder_Pt_Storage_Loc = txtReorderPointSLocMrp.Text;
        ObjMRP.Fxd_Lot_Size_Storage_Loc = txtFixedLSizeStorage.Text;
        ObjMRP.Ind_Repetative_Mfg_Allowed = chkIndRepetitiveMfg.Checked == true ? "1" : "0";
        ObjMRP.Component_Scrap_Perc = txtComponentScrap.Text;
        ObjMRP.Discontinuation_Indicator = ddlDiscontinuationIndi.SelectedValue;
        ObjMRP.Effective_Out_Date = objUtil.GetYYYYMMDD(txtEffectiveOutDate.Text);
        ObjMRP.Follow_Up_Mat = ddlFollowUpMaterial.SelectedValue;
        ObjMRP.Spl_Procur_Type_Stro_Loc = ddlSpecialProcTypeSloc.SelectedValue;

        ObjMRP.Selection_Method = ddlSelectionMethod.SelectedValue;
        ObjMRP.MRP_Relevance_Dep_Req = ddlMRPRelevanceDepReq.SelectedValue;
        ObjMRP.Fair_Share_Rule = ddlFairShareRule.SelectedValue;
        ObjMRP.Indi_Push_Distribution = ddlIndiPushDistribution.SelectedValue;

        ObjMRP.IsActive = 1;
        ObjMRP.UserId = lblUserId.Text;
        ObjMRP.TodayDate = objUtil.GetDate();
        ObjMRP.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
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
        helperAccess.PopuplateDropDownList(ddlSpecialProcTypeSloc, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcTypeSloc','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjMRPDataAccess.GetMRPData4(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMRPId.Text = ds.Tables[0].Rows[0]["Mat_MRP4_Id"].ToString();
        }
        FillMRPData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void FillMRPData()
    {
        try
        {
        MRP4 ObjMRP = GetMRPData();
        if (ObjMRP.Mat_MRP4_Id > 0)
        {
            lblMRPId.Text = ObjMRP.Mat_MRP4_Id.ToString();

            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            ddlPlant.SelectedValue = ObjMRP.Plant_Id;
            BindPlantWiseDropDown();

            ddlStorageLocation.SelectedValue = ObjMRP.Storage_Location;
            ddlDependentReq.SelectedValue = ObjMRP.Dependent_Req_Ind;
            ddlIndiReqGrouping.SelectedValue = ObjMRP.Indicator_Req_Grping;
            ddlStorageLocMrpIndi.SelectedValue = ObjMRP.Storage_Loc_MRP_Indicator;
            txtReorderPointSLocMrp.Text = ObjMRP.ReOrder_Pt_Storage_Loc.ToString();
            txtFixedLSizeStorage.Text = ObjMRP.Fxd_Lot_Size_Storage_Loc.ToString();
            chkIndRepetitiveMfg.Checked = ObjMRP.Ind_Repetative_Mfg_Allowed.ToLower() == "true" ? true : false;
            txtComponentScrap.Text = ObjMRP.Component_Scrap_Perc;
            ddlDiscontinuationIndi.SelectedValue = ObjMRP.Discontinuation_Indicator;
            txtEffectiveOutDate.Text = ObjMRP.Effective_Out_Date;
            ddlFollowUpMaterial.SelectedValue = ObjMRP.Follow_Up_Mat;
            ddlSpecialProcTypeSloc.SelectedValue = ObjMRP.Spl_Procur_Type_Stro_Loc;

            ddlSelectionMethod.SelectedValue = ObjMRP.Selection_Method;
            ddlMRPRelevanceDepReq.SelectedValue = ObjMRP.MRP_Relevance_Dep_Req;
            ddlFairShareRule.SelectedValue = ObjMRP.Fair_Share_Rule;
            ddlIndiPushDistribution.SelectedValue = ObjMRP.Indi_Push_Distribution;
        }
        else
        {
            lblMRPId.Text = "0";
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP4','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR
            ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

            BindPlantWiseDropDown();
            ddlStorageLocation.SelectedValue = Session[StaticKeys.MatStorageLocationId].ToString();

            //Promotion code start
            //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "171")
            if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                ddlSelectionMethod.SelectedValue = "2";
        }

        ddlPlant.Enabled = false;
        ddlStorageLocation.Enabled = false;
        }
        catch (Exception ex)
        { _log.Error("FillMRPData", ex); }
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.MRP4 obj = new SectionConfiguration.MRP4();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(MRP4 NewMRP4Data, MRP4 oldMRP4Data)
    {
        try
        {
            if (NewMRP4Data.Mat_MRP4_Id > 0 && oldMRP4Data.Mat_MRP4_Id > 0)
            {
                if (NewMRP4Data.Plant_Id != oldMRP4Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldMRP4Data.Plant_Id + "</td><td>" + NewMRP4Data.Plant_Id + "</td></tr>";
                if (NewMRP4Data.Storage_Location != oldMRP4Data.Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Location</td> <td>" + oldMRP4Data.Storage_Location + "</td><td>" + NewMRP4Data.Storage_Location + "</td></tr>";
                if (NewMRP4Data.Selection_Method != oldMRP4Data.Selection_Method)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Selection Method</td> <td>" + oldMRP4Data.Selection_Method + "</td><td>" + NewMRP4Data.Selection_Method + "</td></tr>";
                if (NewMRP4Data.Dependent_Req_Ind != oldMRP4Data.Dependent_Req_Ind)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Dependent Req Ind</td> <td>" + oldMRP4Data.Dependent_Req_Ind + "</td><td>" + NewMRP4Data.Dependent_Req_Ind + "</td></tr>";
                if (NewMRP4Data.Indicator_Req_Grping != oldMRP4Data.Indicator_Req_Grping)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indicator Req Grping</td> <td>" + oldMRP4Data.Indicator_Req_Grping + "</td><td>" + NewMRP4Data.Indicator_Req_Grping + "</td></tr>";
                if (NewMRP4Data.Storage_Loc_MRP_Indicator != oldMRP4Data.Storage_Loc_MRP_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Loc MRP Indicator</td> <td>" + oldMRP4Data.Storage_Loc_MRP_Indicator + "</td><td>" + NewMRP4Data.Storage_Loc_MRP_Indicator + "</td></tr>";
                if (NewMRP4Data.ReOrder_Pt_Storage_Loc != oldMRP4Data.ReOrder_Pt_Storage_Loc)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>ReOrder Pt Storage Loc</td> <td>" + oldMRP4Data.ReOrder_Pt_Storage_Loc + "</td><td>" + NewMRP4Data.ReOrder_Pt_Storage_Loc + "</td></tr>";
                if (NewMRP4Data.Fxd_Lot_Size_Storage_Loc != oldMRP4Data.Fxd_Lot_Size_Storage_Loc)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Fxd Lot Size Storage Loc</td> <td>" + oldMRP4Data.Fxd_Lot_Size_Storage_Loc + "</td><td>" + NewMRP4Data.Fxd_Lot_Size_Storage_Loc + "</td></tr>";
                if (NewMRP4Data.Ind_Repetative_Mfg_Allowed != (oldMRP4Data.Ind_Repetative_Mfg_Allowed.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Ind Repetative Mfg Allowed</td> <td>" + (oldMRP4Data.Ind_Repetative_Mfg_Allowed.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewMRP4Data.Ind_Repetative_Mfg_Allowed + "</td></tr>";
                if (NewMRP4Data.Component_Scrap_Perc != oldMRP4Data.Component_Scrap_Perc)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Component Scrap Perc</td> <td>" + oldMRP4Data.Component_Scrap_Perc + "</td><td>" + NewMRP4Data.Component_Scrap_Perc + "</td></tr>";
                if (NewMRP4Data.Discontinuation_Indicator != oldMRP4Data.Discontinuation_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Discontinuation Indicator</td> <td>" + oldMRP4Data.Discontinuation_Indicator + "</td><td>" + NewMRP4Data.Discontinuation_Indicator + "</td></tr>";
                if (NewMRP4Data.Effective_Out_Date != oldMRP4Data.Effective_Out_Date)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Effective Out Date</td> <td>" + oldMRP4Data.Effective_Out_Date + "</td><td>" + NewMRP4Data.Effective_Out_Date + "</td></tr>";
                if (NewMRP4Data.Follow_Up_Mat != oldMRP4Data.Follow_Up_Mat)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Follow Up Mat</td> <td>" + oldMRP4Data.Follow_Up_Mat + "</td><td>" + NewMRP4Data.Follow_Up_Mat + "</td></tr>";
                if (NewMRP4Data.Spl_Procur_Type_Stro_Loc != oldMRP4Data.Spl_Procur_Type_Stro_Loc)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Spl Procur Type Stor Loc</td> <td>" + oldMRP4Data.Spl_Procur_Type_Stro_Loc + "</td><td>" + NewMRP4Data.Spl_Procur_Type_Stro_Loc + "</td></tr>";
                if (NewMRP4Data.MRP_Relevance_Dep_Req != oldMRP4Data.MRP_Relevance_Dep_Req)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MRP Relevance Dep Reqt</td> <td>" + oldMRP4Data.MRP_Relevance_Dep_Req + "</td><td>" + NewMRP4Data.MRP_Relevance_Dep_Req + "</td></tr>";
                if (NewMRP4Data.Fair_Share_Rule != oldMRP4Data.Fair_Share_Rule)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Fair Share Rule</td> <td>" + oldMRP4Data.Fair_Share_Rule + "</td><td>" + NewMRP4Data.Fair_Share_Rule + "</td></tr>";
                if (NewMRP4Data.Indi_Push_Distribution != oldMRP4Data.Indi_Push_Distribution)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indi Push Distribution</td> <td>" + oldMRP4Data.Indi_Push_Distribution + "</td><td>" + NewMRP4Data.Indi_Push_Distribution + "</td></tr>";
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
    private void CheckIfChangesLog(MRP4 NewMRP4Data, MRP4 oldMRP4Data)
    {
        Utility objUtil = new Utility();
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewMRP4Data.Mat_MRP4_Id > 0 && oldMRP4Data.Mat_MRP4_Id > 0)
            {
                if (NewMRP4Data.Selection_Method != oldMRP4Data.Selection_Method)
                    _items.Add(new SMChange { colFieldName = 1196, colOldVal = oldMRP4Data.Selection_Method, colNewVal = NewMRP4Data.Selection_Method });
                if (NewMRP4Data.Dependent_Req_Ind != oldMRP4Data.Dependent_Req_Ind)
                    _items.Add(new SMChange { colFieldName = 101, colOldVal = oldMRP4Data.Dependent_Req_Ind, colNewVal = NewMRP4Data.Dependent_Req_Ind });
                if (NewMRP4Data.Indicator_Req_Grping != oldMRP4Data.Indicator_Req_Grping)
                    _items.Add(new SMChange { colFieldName = 102, colOldVal = oldMRP4Data.Indicator_Req_Grping, colNewVal = NewMRP4Data.Indicator_Req_Grping });
                if (NewMRP4Data.Storage_Loc_MRP_Indicator != oldMRP4Data.Storage_Loc_MRP_Indicator)
                    _items.Add(new SMChange { colFieldName = 103, colOldVal = oldMRP4Data.Storage_Loc_MRP_Indicator, colNewVal = NewMRP4Data.Storage_Loc_MRP_Indicator });
                if (NewMRP4Data.ReOrder_Pt_Storage_Loc != oldMRP4Data.ReOrder_Pt_Storage_Loc)
                    _items.Add(new SMChange { colFieldName = 104, colOldVal = oldMRP4Data.ReOrder_Pt_Storage_Loc, colNewVal = NewMRP4Data.ReOrder_Pt_Storage_Loc });
                if (NewMRP4Data.Fxd_Lot_Size_Storage_Loc != oldMRP4Data.Fxd_Lot_Size_Storage_Loc)
                    _items.Add(new SMChange { colFieldName = 105, colOldVal = oldMRP4Data.Fxd_Lot_Size_Storage_Loc, colNewVal = NewMRP4Data.Fxd_Lot_Size_Storage_Loc });
                if (NewMRP4Data.Ind_Repetative_Mfg_Allowed != (oldMRP4Data.Ind_Repetative_Mfg_Allowed.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 106, colOldVal = (oldMRP4Data.Ind_Repetative_Mfg_Allowed.ToLower() == "true" ? "X" : ""), colNewVal = NewMRP4Data.Ind_Repetative_Mfg_Allowed });
                if (NewMRP4Data.Component_Scrap_Perc != oldMRP4Data.Component_Scrap_Perc)
                    _items.Add(new SMChange { colFieldName = 107, colOldVal = oldMRP4Data.Component_Scrap_Perc, colNewVal = NewMRP4Data.Component_Scrap_Perc });
                if (NewMRP4Data.Discontinuation_Indicator != oldMRP4Data.Discontinuation_Indicator)
                    _items.Add(new SMChange { colFieldName = 108, colOldVal = oldMRP4Data.Discontinuation_Indicator, colNewVal = NewMRP4Data.Discontinuation_Indicator });
                //if (NewMRP4Data.Effective_Out_Date != oldMRP4Data.Effective_Out_Date)
                if ((objUtil.GetDDMMYYYYNew(NewMRP4Data.Effective_Out_Date) != oldMRP4Data.Effective_Out_Date)
                   && (objUtil.GetDDMMYYYYNew(NewMRP4Data.Effective_Out_Date) != "01/01/1900")
                   && (NewMRP4Data.Effective_Out_Date != "1900-01-01")
                       && (oldMRP4Data.Effective_Out_Date != "01/01/1900") && (oldMRP4Data.Effective_Out_Date != "1900-01-01")
                       )
                {
                    _items.Add(new SMChange { colFieldName = 109, colOldVal = oldMRP4Data.Effective_Out_Date, colNewVal = objUtil.GetDDMMYYYYNew(NewMRP4Data.Effective_Out_Date) });
                }
                if (NewMRP4Data.Follow_Up_Mat != oldMRP4Data.Follow_Up_Mat)
                    _items.Add(new SMChange { colFieldName = 110, colOldVal = oldMRP4Data.Follow_Up_Mat, colNewVal = NewMRP4Data.Follow_Up_Mat });
                if (NewMRP4Data.Spl_Procur_Type_Stro_Loc != oldMRP4Data.Spl_Procur_Type_Stro_Loc)
                    _items.Add(new SMChange { colFieldName = 111, colOldVal = oldMRP4Data.Spl_Procur_Type_Stro_Loc, colNewVal = NewMRP4Data.Spl_Procur_Type_Stro_Loc });
                if (NewMRP4Data.MRP_Relevance_Dep_Req != oldMRP4Data.MRP_Relevance_Dep_Req)
                    _items.Add(new SMChange { colFieldName = 1197, colOldVal = oldMRP4Data.MRP_Relevance_Dep_Req, colNewVal = NewMRP4Data.MRP_Relevance_Dep_Req });
                if (NewMRP4Data.Fair_Share_Rule != oldMRP4Data.Fair_Share_Rule)
                    _items.Add(new SMChange { colFieldName = 1198, colOldVal = oldMRP4Data.Fair_Share_Rule, colNewVal = NewMRP4Data.Fair_Share_Rule });
                if (NewMRP4Data.Indi_Push_Distribution != oldMRP4Data.Indi_Push_Distribution)
                    _items.Add(new SMChange { colFieldName = 1199, colOldVal = oldMRP4Data.Indi_Push_Distribution, colNewVal = NewMRP4Data.Indi_Push_Distribution });
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
                ChangeSMatID1 = helperAccess.MaterialChange("11", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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
}