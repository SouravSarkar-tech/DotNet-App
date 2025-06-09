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
public partial class Transaction_MRP2 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MRPDataAccess ObjMRPDataAccess = new MRPDataAccess();
    HelperAccess helperAccess = new HelperAccess();
    MRP2 objSavedMRP2 = new MRP2();
    string sdate = "";
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogM2" + sdate + ".txt", "Start" + HelperAccess.ReqType);
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
                    SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, new SectionConfiguration.MRP2());
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
                            if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = true;
                            }
                            //MSC_8300001775 End 
                        }
                        ClearData();
                        //FillDataGrid();
                        ConfigureControl();
                        //FillMRPData();

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

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
            FillMRPData();
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
            lblMRPId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_MRP2_Id"].ToString();
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
            Response.Redirect("MRP2.aspx",false);
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


    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
        lableddlSchedulingMKey.Visible = false;
        reqddlSchedulingMKey.Visible = false;

        ddlProcurmentType.Enabled = true;
        ddlDefaultStorage.Enabled = true;
        ddlPlanningCalander.Enabled = true;
        txtInHousePTime.Enabled = true;
        txtInHousePTime.CssClass = "textbox";
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }


    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {

            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIssueStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlDefaultStorage, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


        helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlDetermination, "pr_GetDropDownListByControlNameModuleType 'M','ddlDetermination'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIndicatorItemRelevant, "pr_GetDropDownListByControlNameModuleType 'M','ddlIndicatorItemRelevant'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPeriodProfileSafetyTime, "pr_GetDropDownListByControlNameModuleType 'M','ddlPeriodProfileSafetyTime'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProcurmentType, "pr_GetDropDownListByControlNameModuleType 'M','ddlProcurmentType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProductionSProfile, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductionSProfile'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRangeCoverage, "pr_GetDropDownListByControlNameModuleType 'M','ddlRangeCoverage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSafetyTimeIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlSafetyTimeIndicator'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSchedulingMKey, "pr_GetDropDownListByControlNameModuleType 'M','ddlSchedulingMKey'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPlanningCalander, "pr_GetDropDownListByControlNameModuleType 'M','ddlPlanningCalander'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIndicatorBackflush, "pr_GetDropDownListByControlNameModuleType 'M','ddlIndicatorBackflush'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProposedSupplyArea, "pr_GetDropDownListByControlNameModuleType 'M','ddlProposedSupplyArea'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlQuotaArrangementUsage, "pr_GetDropDownListByControlNameModuleType 'M','ddlQuotaArrangementUsage'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblMRPId.Text = "0";

            txtPlannedDeleveryTime.Text = "";
            txtInHousePTime.Text = "";
            txtSafetyStock.Text = "";
            chkIndicatorBulkMaterial.Checked = false;
            txtSafetyTimeWorkdays.Text = "";
            txtLowerLimitSafetyStock.Text = "";

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
            ds = ObjMRPDataAccess.GetMRPData2(Convert.ToInt32(lblMasterHeaderId.Text));

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
            MRP2 ObjMRP = GetControlsValue();

            objSavedMRP2 = GetMRPData();

            if (ObjMRP.Plant_Id != null)
            {
                //MSC_8300001775 Start Comment 
                //if (ddlIssueStorageLocation.SelectedValue == "" ||
                //     (ddlProposedSupplyArea.SelectedValue != "CAMPPM    " && ddlProposedSupplyArea.SelectedValue != "REGULARPM " && ddlProposedSupplyArea.SelectedValue != "CAMPRM    " && ddlProposedSupplyArea.SelectedValue != "REGULARRM ") ||
                //     ((ddlProposedSupplyArea.SelectedValue == "CAMPPM    " || ddlProposedSupplyArea.SelectedValue == "REGULARPM ") && ddlIssueStorageLocation.SelectedValue == "0101") ||
                //     ((ddlProposedSupplyArea.SelectedValue == "CAMPRM    " || ddlProposedSupplyArea.SelectedValue == "REGULARRM ") && ddlIssueStorageLocation.SelectedValue == "0051"))
                //MSC_8300001775 End Comment
                //MSC_8300001775 Start 


                if (ddlIssueStorageLocation.SelectedValue == "" ||
                    (ddlProposedSupplyArea.SelectedValue != "CAMPPM" && ddlProposedSupplyArea.SelectedValue != "REGULARPM" && ddlProposedSupplyArea.SelectedValue != "CAMPRM" && ddlProposedSupplyArea.SelectedValue != "REGULARRM") ||
                    ((ddlProposedSupplyArea.SelectedValue == "CAMPPM" || ddlProposedSupplyArea.SelectedValue == "REGULARPM") && ddlIssueStorageLocation.SelectedValue == "0101") ||
                    ((ddlProposedSupplyArea.SelectedValue == "CAMPRM" || ddlProposedSupplyArea.SelectedValue == "REGULARRM") && ddlIssueStorageLocation.SelectedValue == "0051"))
                {//MSC_8300001775 End
                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                    {
                        if (objSavedMRP2.Mat_MRP2_Id > 0)
                        {
                            CheckIfChanges(ObjMRP, objSavedMRP2);
                        }
                    }


                    if (ObjMRPDataAccess.Save(ObjMRP) > 0)
                    {
                        //MSC_8300001775
                        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                        {
                            CheckIfChangesLog(ObjMRP, objSavedMRP2);
                        }
                        //MSC_8300001775

                        ClearData();
                        flg = true;

                        ////MSC_8300001775
                        //if (HelperAccess.ReqType == "SMC")
                        //{
                        //    CheckIfChangesLog(ObjMRP, objSavedMRP2);
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
                    //MSC_8300001775 Start Comment 
                    //if ((ddlProposedSupplyArea.SelectedValue == "CAMPPM    " || ddlProposedSupplyArea.SelectedValue == "REGULARPM ") && ddlIssueStorageLocation.SelectedValue != "0101")
                    //     else if ((ddlProposedSupplyArea.SelectedValue == "CAMPRM    " || ddlProposedSupplyArea.SelectedValue == "REGULARRM ") && ddlIssueStorageLocation.SelectedValue != "0051")
                    //MSC_8300001775 End Comment
                    //MSC_8300001775 Start


                    if ((ddlProposedSupplyArea.SelectedValue == "CAMPPM" || ddlProposedSupplyArea.SelectedValue == "REGULARPM") && ddlIssueStorageLocation.SelectedValue != "0101")
                    {
                        lblMsg.Text = "Issue Storage Location should be either blank or '0101'";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                    else if ((ddlProposedSupplyArea.SelectedValue == "CAMPRM" || ddlProposedSupplyArea.SelectedValue == "REGULARRM") && ddlIssueStorageLocation.SelectedValue != "0051")
                    {
                        lblMsg.Text = "Issue Storage Location should be either blank or '0051'";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                    //MSC_8300001775 End
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

    private MRP2 GetMRPData()
    {
        return ObjMRPDataAccess.GetMRP2(Convert.ToInt32(lblMRPId.Text));
    }

    private MRP2 GetControlsValue()
    {
        MRP2 ObjMRP = new MRP2();
        Utility objUtil = new Utility();

        try
        {
            ObjMRP.Mat_MRP2_Id = Convert.ToInt32(lblMRPId.Text);
            ObjMRP.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjMRP.Plant_Id = ddlPlant.SelectedValue;// GetSelectedCheckedValue(ddlPlant);
            ObjMRP.Storage_Location = ddlStorageLocation.SelectedValue;

            ObjMRP.Procurement_Type = ddlProcurmentType.SelectedValue;
            ObjMRP.Spl_Procurement_Type = ddlSpecialProcType.SelectedValue;
            ObjMRP.Proposed_Supply_Area = ddlProposedSupplyArea.SelectedValue;
            ObjMRP.Planned_Delivery_Time_Days = txtPlannedDeleveryTime.Text;
            ObjMRP.InHouse_Production_Time = txtInHousePTime.Text;
            ObjMRP.Schedule_Margin_Key_Float = ddlSchedulingMKey.SelectedValue;
            ObjMRP.Safety_Stock = txtSafetyStock.Text;
            ObjMRP.Issue_Storage_Location = ddlIssueStorageLocation.SelectedValue;
            ObjMRP.Range_Coverage_Profile = ddlRangeCoverage.SelectedValue;
            ObjMRP.Indicator_Bulk_Material = chkIndicatorBulkMaterial.Checked == true ? "1" : "0";
            ObjMRP.Indicator_BackFlush = ddlIndicatorBackflush.SelectedValue;
            ObjMRP.Default_Storage_Loc_Ext_Proc = ddlDefaultStorage.SelectedValue;
            ObjMRP.Production_Sched_Profile = ddlProductionSProfile.SelectedValue;
            ObjMRP.Safety_Time_Indicator = ddlSafetyTimeIndicator.SelectedValue;
            ObjMRP.Safety_Time_WorkDays = txtSafetyTimeWorkdays.Text;
            ObjMRP.Batch_Entry_Production = ddlDetermination.SelectedValue;
            ObjMRP.Indicator_JIT_Delivery = ddlIndicatorItemRelevant.SelectedValue;
            ObjMRP.Period_Profile_Safety_Time = ddlPeriodProfileSafetyTime.SelectedValue;
            ObjMRP.Lower_Limit_Safety_Stock = txtLowerLimitSafetyStock.Text;

            ObjMRP.Quota_Arrangement_Usage = ddlQuotaArrangementUsage.SelectedValue;
            ObjMRP.GR_Processing_Time = txtGRProcessingTime.Text;
            ObjMRP.Planning_Calander = ddlPlanningCalander.SelectedValue;
            ObjMRP.Min_Safety_Stock = txtMinSafetyStock.Text;

            ObjMRP.IsActive = 1;
            ObjMRP.UserId = lblUserId.Text;
            ObjMRP.TodayDate = objUtil.GetDate();
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

        helperAccess.PopuplateDropDownList(ddlIssueStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlDefaultStorage, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProposedSupplyArea, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlProposedSupplyArea','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSchedulingMKey, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSchedulingMKey','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRangeCoverage, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlRangeCoverage','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        if (lblModuleId.Text == "138" || lblModuleId.Text == "147" || lblModuleId.Text == "163")
        {
            if (ddlStorageLocation.Items.FindByValue("0151") != null)
            {
                ddlStorageLocation.SelectedValue = "0151";
            }

            if (ddlSchedulingMKey.Items.Count > 1)
            {
                ddlSchedulingMKey.SelectedValue = "000";
            }
            }
        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
        ds = ObjMRPDataAccess.GetMRPData2(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMRPId.Text = ds.Tables[0].Rows[0]["Mat_MRP2_Id"].ToString();
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
            MRP2 ObjMRP = GetMRPData();
            if (ObjMRP.Mat_MRP2_Id > 0)
            {
                lblMRPId.Text = ObjMRP.Mat_MRP2_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjMRP.Plant_Id;
                BindPlantWiseDropDown();

                ddlStorageLocation.SelectedValue = ObjMRP.Storage_Location;
                ddlProposedSupplyArea.SelectedValue = ObjMRP.Proposed_Supply_Area;
                ddlProcurmentType.SelectedValue = ObjMRP.Procurement_Type;
                ddlSpecialProcType.SelectedValue = ObjMRP.Spl_Procurement_Type;
                txtPlannedDeleveryTime.Text = ObjMRP.Planned_Delivery_Time_Days;
                txtInHousePTime.Text = ObjMRP.InHouse_Production_Time;
                ddlSchedulingMKey.SelectedValue = ObjMRP.Schedule_Margin_Key_Float;
                txtSafetyStock.Text = ObjMRP.Safety_Stock.ToString();
                ddlIssueStorageLocation.SelectedValue = ObjMRP.Issue_Storage_Location;
                ddlRangeCoverage.SelectedValue = ObjMRP.Range_Coverage_Profile;
                chkIndicatorBulkMaterial.Checked = ObjMRP.Indicator_Bulk_Material == "1" ? true : false;
                ddlIndicatorBackflush.SelectedValue = ObjMRP.Indicator_BackFlush;
                ddlDefaultStorage.SelectedValue = ObjMRP.Default_Storage_Loc_Ext_Proc;
                ddlProductionSProfile.SelectedValue = ObjMRP.Production_Sched_Profile;
                ddlSafetyTimeIndicator.SelectedValue = ObjMRP.Safety_Time_Indicator;
                txtSafetyTimeWorkdays.Text = ObjMRP.Safety_Time_WorkDays.ToString();
                ddlDetermination.SelectedValue = ObjMRP.Batch_Entry_Production;
                ddlIndicatorItemRelevant.SelectedValue = ObjMRP.Indicator_JIT_Delivery;
                ddlPeriodProfileSafetyTime.SelectedValue = ObjMRP.Period_Profile_Safety_Time;
                txtLowerLimitSafetyStock.Text = ObjMRP.Lower_Limit_Safety_Stock.ToString();

                ddlQuotaArrangementUsage.SelectedValue = ObjMRP.Quota_Arrangement_Usage;
                txtGRProcessingTime.Text = ObjMRP.GR_Processing_Time;
                ddlPlanningCalander.SelectedValue = ObjMRP.Planning_Calander;
                txtMinSafetyStock.Text = ObjMRP.Min_Safety_Stock;
            }
            else
            {
                lblMRPId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP2','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
                ddlStorageLocation.SelectedValue = Session[StaticKeys.MatStorageLocationId].ToString();
                ddlIssueStorageLocation.SelectedValue = Session[StaticKeys.MatStorageLocationId].ToString();

                //ddlProcurmentType.SelectedValue = "F";
                ddlSchedulingMKey.SelectedValue = "000";

                //txtPlannedDeleveryTime.Text = "21";
                //Promotion code start
                //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                {
                    ddlProcurmentType.SelectedValue = "E";
                    txtPlannedDeleveryTime.Text = "";
                }
                else
                {
                    ddlProcurmentType.SelectedValue = "F";
                    txtPlannedDeleveryTime.Text = "21";
                }
            }

            if (lblModuleId.Text == "138" || lblModuleId.Text == "147" || lblModuleId.Text == "162" || lblModuleId.Text == "164")
                ddlProcurmentType.Enabled = false;

            //Promotion code start
            //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
            if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                inHouseSetup();

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
        SectionConfiguration.MRP2 obj = new SectionConfiguration.MRP2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(MRP2 NewMRP2Data, MRP2 oldMRP2Data)
    {
        try
        {
            if (NewMRP2Data.Mat_MRP2_Id > 0 && oldMRP2Data.Mat_MRP2_Id > 0)
            {
                if (NewMRP2Data.Plant_Id != oldMRP2Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldMRP2Data.Plant_Id + "</td><td>" + NewMRP2Data.Plant_Id + "</td></tr>";
                if (NewMRP2Data.Storage_Location != oldMRP2Data.Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Location</td> <td>" + oldMRP2Data.Storage_Location + "</td><td>" + NewMRP2Data.Storage_Location + "</td></tr>";
                if (NewMRP2Data.Procurement_Type != oldMRP2Data.Procurement_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Procurement Type</td> <td>" + oldMRP2Data.Procurement_Type + "</td><td>" + NewMRP2Data.Procurement_Type + "</td></tr>";
                if (NewMRP2Data.Spl_Procurement_Type != oldMRP2Data.Spl_Procurement_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Spl Procurement Type</td> <td>" + oldMRP2Data.Spl_Procurement_Type + "</td><td>" + NewMRP2Data.Spl_Procurement_Type + "</td></tr>";
                if (NewMRP2Data.Proposed_Supply_Area != oldMRP2Data.Proposed_Supply_Area)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Proposed Supply Area</td> <td>" + oldMRP2Data.Proposed_Supply_Area + "</td><td>" + NewMRP2Data.Proposed_Supply_Area + "</td></tr>";
                if (NewMRP2Data.Planned_Delivery_Time_Days != oldMRP2Data.Planned_Delivery_Time_Days)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Delivery Time Days</td> <td>" + oldMRP2Data.Planned_Delivery_Time_Days + "</td><td>" + NewMRP2Data.Planned_Delivery_Time_Days + "</td></tr>";
                if (NewMRP2Data.InHouse_Production_Time != oldMRP2Data.InHouse_Production_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>InHouse Production Time</td> <td>" + oldMRP2Data.InHouse_Production_Time + "</td><td>" + NewMRP2Data.InHouse_Production_Time + "</td></tr>";
                if (NewMRP2Data.Schedule_Margin_Key_Float != oldMRP2Data.Schedule_Margin_Key_Float)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Schedule Margin Key Float</td> <td>" + oldMRP2Data.Schedule_Margin_Key_Float + "</td><td>" + NewMRP2Data.Schedule_Margin_Key_Float + "</td></tr>";
                if (NewMRP2Data.Safety_Stock != oldMRP2Data.Safety_Stock)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Safety Stock</td> <td>" + oldMRP2Data.Safety_Stock + "</td><td>" + NewMRP2Data.Safety_Stock + "</td></tr>";
                if (NewMRP2Data.Issue_Storage_Location != oldMRP2Data.Issue_Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Issue Storage Location</td> <td>" + oldMRP2Data.Issue_Storage_Location + "</td><td>" + NewMRP2Data.Issue_Storage_Location + "</td></tr>";
                if (NewMRP2Data.Range_Coverage_Profile != oldMRP2Data.Range_Coverage_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Range Coverage Profile</td> <td>" + oldMRP2Data.Range_Coverage_Profile + "</td><td>" + NewMRP2Data.Range_Coverage_Profile + "</td></tr>";
                if (NewMRP2Data.Indicator_Bulk_Material != (oldMRP2Data.Indicator_Bulk_Material.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indicator Bulk Material </td> <td>" + (oldMRP2Data.Indicator_Bulk_Material.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewMRP2Data.Indicator_Bulk_Material + "</td></tr>";
                if (NewMRP2Data.Indicator_BackFlush != oldMRP2Data.Indicator_BackFlush)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indicator BackFlush</td> <td>" + oldMRP2Data.Indicator_BackFlush + "</td><td>" + NewMRP2Data.Indicator_BackFlush + "</td></tr>";
                if (NewMRP2Data.Default_Storage_Loc_Ext_Proc != oldMRP2Data.Default_Storage_Loc_Ext_Proc)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Default Storage Loc Ext Proc</td> <td>" + oldMRP2Data.Default_Storage_Loc_Ext_Proc + "</td><td>" + NewMRP2Data.Default_Storage_Loc_Ext_Proc + "</td></tr>";
                if (NewMRP2Data.Production_Sched_Profile != oldMRP2Data.Production_Sched_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Production Sched Profile</td> <td>" + oldMRP2Data.Production_Sched_Profile + "</td><td>" + NewMRP2Data.Production_Sched_Profile + "</td></tr>";
                if (NewMRP2Data.Safety_Time_Indicator != oldMRP2Data.Safety_Time_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Safety Time Indicator</td> <td>" + oldMRP2Data.Safety_Time_Indicator + "</td><td>" + NewMRP2Data.Safety_Time_Indicator + "</td></tr>";
                if (NewMRP2Data.Safety_Time_WorkDays != oldMRP2Data.Safety_Time_WorkDays)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Safety Time WorkDays</td> <td>" + oldMRP2Data.Safety_Time_WorkDays + "</td><td>" + NewMRP2Data.Safety_Time_WorkDays + "</td></tr>";
                if (NewMRP2Data.Batch_Entry_Production != oldMRP2Data.Batch_Entry_Production)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Batch Entry Production</td> <td>" + oldMRP2Data.Batch_Entry_Production + "</td><td>" + NewMRP2Data.Batch_Entry_Production + "</td></tr>";
                if (NewMRP2Data.Indicator_JIT_Delivery != oldMRP2Data.Indicator_JIT_Delivery)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indicator JIT Delivery</td> <td>" + oldMRP2Data.Indicator_JIT_Delivery + "</td><td>" + NewMRP2Data.Indicator_JIT_Delivery + "</td></tr>";
                if (NewMRP2Data.Period_Profile_Safety_Time != oldMRP2Data.Period_Profile_Safety_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Period Profile Safety Time</td> <td>" + oldMRP2Data.Period_Profile_Safety_Time + "</td><td>" + NewMRP2Data.Period_Profile_Safety_Time + "</td></tr>";
                if (NewMRP2Data.Lower_Limit_Safety_Stock != oldMRP2Data.Lower_Limit_Safety_Stock)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Lower Limit Safety Stock</td> <td>" + oldMRP2Data.Lower_Limit_Safety_Stock + "</td><td>" + NewMRP2Data.Lower_Limit_Safety_Stock + "</td></tr>";
                if (NewMRP2Data.Quota_Arrangement_Usage != oldMRP2Data.Quota_Arrangement_Usage)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Quota Arrangement Usage</td> <td>" + oldMRP2Data.Quota_Arrangement_Usage + "</td><td>" + NewMRP2Data.Quota_Arrangement_Usage + "</td></tr>";
                if (NewMRP2Data.GR_Processing_Time != oldMRP2Data.GR_Processing_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>GR Processing Time</td> <td>" + oldMRP2Data.GR_Processing_Time + "</td><td>" + NewMRP2Data.GR_Processing_Time + "</td></tr>";
                if (NewMRP2Data.Planning_Calander != oldMRP2Data.Planning_Calander)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planning Calander</td> <td>" + oldMRP2Data.Planning_Calander + "</td><td>" + NewMRP2Data.Planning_Calander + "</td></tr>";
                if (NewMRP2Data.Min_Safety_Stock != oldMRP2Data.Min_Safety_Stock)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Safety Stock</td> <td>" + oldMRP2Data.Min_Safety_Stock + "</td><td>" + NewMRP2Data.Min_Safety_Stock + "</td></tr>";

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
    private void CheckIfChangesLog(MRP2 NewMRP2Data, MRP2 oldMRP2Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewMRP2Data.Mat_MRP2_Id > 0 && oldMRP2Data.Mat_MRP2_Id > 0)
            {
                if (NewMRP2Data.Procurement_Type != oldMRP2Data.Procurement_Type)
                {
                    WriteMatChangeLog("MatChangeLogM2" + sdate + ".txt", "69" + oldMRP2Data.Procurement_Type + '-' + NewMRP2Data.Procurement_Type);

                    _items.Add(new SMChange { colFieldName = 69, colOldVal = oldMRP2Data.Procurement_Type, colNewVal = NewMRP2Data.Procurement_Type });
                }
                if (NewMRP2Data.Spl_Procurement_Type != oldMRP2Data.Spl_Procurement_Type)
                    _items.Add(new SMChange { colFieldName = 70, colOldVal = oldMRP2Data.Spl_Procurement_Type, colNewVal = NewMRP2Data.Spl_Procurement_Type });
                if (NewMRP2Data.Proposed_Supply_Area != oldMRP2Data.Proposed_Supply_Area)
                    _items.Add(new SMChange { colFieldName = 1187, colOldVal = oldMRP2Data.Proposed_Supply_Area, colNewVal = NewMRP2Data.Proposed_Supply_Area });
                if (NewMRP2Data.Planned_Delivery_Time_Days != oldMRP2Data.Planned_Delivery_Time_Days)
                    _items.Add(new SMChange { colFieldName = 71, colOldVal = oldMRP2Data.Planned_Delivery_Time_Days, colNewVal = NewMRP2Data.Planned_Delivery_Time_Days });
                if (NewMRP2Data.InHouse_Production_Time != oldMRP2Data.InHouse_Production_Time)
                    _items.Add(new SMChange { colFieldName = 72, colOldVal = oldMRP2Data.InHouse_Production_Time, colNewVal = NewMRP2Data.InHouse_Production_Time });
                if (NewMRP2Data.Schedule_Margin_Key_Float != oldMRP2Data.Schedule_Margin_Key_Float)
                    _items.Add(new SMChange { colFieldName = 73, colOldVal = oldMRP2Data.Schedule_Margin_Key_Float, colNewVal = NewMRP2Data.Schedule_Margin_Key_Float });
                if (NewMRP2Data.Safety_Stock != oldMRP2Data.Safety_Stock)
                    _items.Add(new SMChange { colFieldName = 74, colOldVal = oldMRP2Data.Safety_Stock, colNewVal = NewMRP2Data.Safety_Stock });
                if (NewMRP2Data.Issue_Storage_Location != oldMRP2Data.Issue_Storage_Location)
                    _items.Add(new SMChange { colFieldName = 75, colOldVal = oldMRP2Data.Issue_Storage_Location, colNewVal = NewMRP2Data.Issue_Storage_Location });
                if (NewMRP2Data.Range_Coverage_Profile != oldMRP2Data.Range_Coverage_Profile)
                    _items.Add(new SMChange { colFieldName = 76, colOldVal = oldMRP2Data.Range_Coverage_Profile, colNewVal = NewMRP2Data.Range_Coverage_Profile });
                if (NewMRP2Data.Indicator_Bulk_Material != (oldMRP2Data.Indicator_Bulk_Material.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 77, colOldVal = (oldMRP2Data.Indicator_Bulk_Material.ToLower() == "true" ? "X" : ""), colNewVal = oldMRP2Data.Indicator_Bulk_Material });
                if (NewMRP2Data.Indicator_BackFlush != oldMRP2Data.Indicator_BackFlush)
                    _items.Add(new SMChange { colFieldName = 78, colOldVal = oldMRP2Data.Indicator_BackFlush, colNewVal = NewMRP2Data.Indicator_BackFlush });
                if (NewMRP2Data.Default_Storage_Loc_Ext_Proc != oldMRP2Data.Default_Storage_Loc_Ext_Proc)
                    _items.Add(new SMChange { colFieldName = 79, colOldVal = oldMRP2Data.Default_Storage_Loc_Ext_Proc, colNewVal = NewMRP2Data.Default_Storage_Loc_Ext_Proc });
                if (NewMRP2Data.Production_Sched_Profile != oldMRP2Data.Production_Sched_Profile)
                    _items.Add(new SMChange { colFieldName = 80, colOldVal = oldMRP2Data.Production_Sched_Profile, colNewVal = NewMRP2Data.Production_Sched_Profile });
                //if (NewMRP2Data.Safety_Time_Indicator != oldMRP2Data.Safety_Time_Indicator)
                //    _items.Add(new SMChange { colFieldName = 81, colOldVal = oldMRP2Data.Safety_Time_Indicator, colNewVal = NewMRP2Data.Safety_Time_Indicator });
                if (NewMRP2Data.Safety_Time_WorkDays != oldMRP2Data.Safety_Time_WorkDays)
                    _items.Add(new SMChange { colFieldName = 82, colOldVal = oldMRP2Data.Safety_Time_WorkDays, colNewVal = NewMRP2Data.Safety_Time_WorkDays });
                if (NewMRP2Data.Batch_Entry_Production != oldMRP2Data.Batch_Entry_Production)
                    _items.Add(new SMChange { colFieldName = 83, colOldVal = oldMRP2Data.Batch_Entry_Production, colNewVal = NewMRP2Data.Batch_Entry_Production });
                if (NewMRP2Data.Indicator_JIT_Delivery != oldMRP2Data.Indicator_JIT_Delivery)
                    _items.Add(new SMChange { colFieldName = 84, colOldVal = oldMRP2Data.Indicator_JIT_Delivery, colNewVal = NewMRP2Data.Indicator_JIT_Delivery });
                if (NewMRP2Data.Period_Profile_Safety_Time != oldMRP2Data.Period_Profile_Safety_Time)
                    _items.Add(new SMChange { colFieldName = 85, colOldVal = oldMRP2Data.Period_Profile_Safety_Time, colNewVal = NewMRP2Data.Period_Profile_Safety_Time });
                if (NewMRP2Data.Lower_Limit_Safety_Stock != oldMRP2Data.Lower_Limit_Safety_Stock)
                    _items.Add(new SMChange { colFieldName = 86, colOldVal = oldMRP2Data.Lower_Limit_Safety_Stock, colNewVal = NewMRP2Data.Lower_Limit_Safety_Stock });
                if (NewMRP2Data.Quota_Arrangement_Usage != oldMRP2Data.Quota_Arrangement_Usage)
                    _items.Add(new SMChange { colFieldName = 1188, colOldVal = oldMRP2Data.Quota_Arrangement_Usage, colNewVal = NewMRP2Data.Quota_Arrangement_Usage });
                if (NewMRP2Data.GR_Processing_Time != oldMRP2Data.GR_Processing_Time)
                    _items.Add(new SMChange { colFieldName = 1189, colOldVal = oldMRP2Data.GR_Processing_Time, colNewVal = NewMRP2Data.GR_Processing_Time });
                if (NewMRP2Data.Planning_Calander != oldMRP2Data.Planning_Calander)
                    _items.Add(new SMChange { colFieldName = 1190, colOldVal = oldMRP2Data.Planning_Calander, colNewVal = NewMRP2Data.Planning_Calander });
                if (NewMRP2Data.Min_Safety_Stock != oldMRP2Data.Min_Safety_Stock)
                    _items.Add(new SMChange { colFieldName = 1191, colOldVal = oldMRP2Data.Min_Safety_Stock, colNewVal = NewMRP2Data.Min_Safety_Stock });

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
                ChangeSMatID1 = helperAccess.MaterialChange("9", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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
    protected void ddlProcurmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        //Promotion code start
        //if(lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
        if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
             inHouseSetup();
        }
        catch (Exception ex)
        { _log.Error("ddlProcurmentType_SelectedIndexChanged", ex); }
    }

    private void inHouseSetup()
    {
        try
        {
        if (ddlProcurmentType.SelectedValue == "E") { 
            txtInHousePTime.Enabled = true;
        }
        else if (ddlProcurmentType.SelectedValue == "F") { 
            txtInHousePTime.Enabled = false;
        }
        else { 
            txtInHousePTime.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("inHouseSetup", ex); }
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