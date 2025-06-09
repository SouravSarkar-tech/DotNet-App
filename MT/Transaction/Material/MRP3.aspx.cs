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
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_MRP3 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MRPDataAccess ObjMRPDataAccess = new MRPDataAccess();
    HelperAccess helperAccess = new HelperAccess();
    MRP3 objSavedMRP3 = new MRP3();
    //LLM_DPT_SDT30072019 Commented By Nitin R
    //List<string> LLMDPTPlantList = new List<string> {"90","115","116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "138", "139"};
    //LLM_DPT_SDT30072019 Commented By Nitin R
    //LLM_DPT_SDT30072019 Added By Nitin R
    List<string> LLMDPTPlantList = new List<string>();
    //LLM_DPT_SDT30072019 Added By Nitin R

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, new SectionConfiguration.MRP3());
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
                        //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                        if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                        {
                            btnSave.Visible = true;
                        }
                        //MSC_8300001775 End 
                    }

                    //FillDataGrid();
                    //FillMRPData();
                    //LLM_DPT_SDT30072019 Added By Nitin R
                    LLMDPTPlantListMet();
                    //LLM_DPT_EDT30072019 Added By Nitin R
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
        lblMRPId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_MRP3_Id"].ToString();
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
            if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                Response.Redirect("MRP1.aspx");
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
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            FillFormDataByMHId();
            Response.Redirect("MRP3.aspx");
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
                //8400000410 comment start
                //if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("WorkScheduling.aspx");
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

    #endregion

    #region Method

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
        lableddlPerioIndicator.Visible = false;
        reqddlPerioIndicator.Visible = false;
        ddlAvailabilityCheck.Enabled = true;

        ddlPlanningSGroup.Enabled = true;
        ddlConsumptionMode.Enabled = true;
        ddlSplitingIndicator.Enabled = true;


        ddlFiscalYearVariant.Enabled = true;
        txtBackwardCPeriod.Enabled = true;
        txtForwardCPeriod.Enabled = true;
        txtBackwardCPeriod.CssClass = "textbox";
        txtForwardCPeriod.CssClass = "textbox";

        ddlMixedMrpIndicator.Enabled = true;
        txtTotalReplenishment.CssClass = "textbox";
        txtTotalReplenishment.Enabled = true; 

        ddlPlanningMaterial.Enabled = true;
        ddlPlanningPlant.Enabled = true;
        txtConvFacyor.Enabled = true;
        txtConvFacyor.CssClass = "textbox";

        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }

    }

    /// <summary>
    /// LLM_DPT_SDT30072019 
    /// Update list
    /// </summary>
    private void LLMDPTPlantListMet()
    {
        QualityAccess ObjQualityAccess = new QualityAccess();
        try
        {
            DataSet ds;
            ds = ObjQualityAccess.GetLLMPlantList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string sValNew = Convert.ToString(row["Plant_Id"]);
                    LLMDPTPlantList.Add(sValNew);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("LLMDPTPlantListMet", ex);
        }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        //helperAccess.PopuplateDropDownList(ddlPlanningPlant, "pr_GetPlantList '" + lblMRPId.Text + "','ALL','0'", "Plant_Name", "Plant_Id");
        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {
            
            helperAccess.PopuplateDropDownList(ddlPlanningPlant, "pr_GetPlantListCtrl '" + lblMRPId.Text + "','ALL','0'", "Plant_Name", "Plant_Id");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlanningPlant, "pr_GetPlantList '" + lblMRPId.Text + "','ALL','0'", "Plant_Name", "Plant_Id");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlAvailabilityCheck, "pr_GetDropDownListByControlNameModuleType 'M','ddlAvailabilityCheck'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlConsumptionMode, "pr_GetDropDownListByControlNameModuleType 'M','ddlConsumptionMode'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlFiscalYearVariant, "pr_GetDropDownListByControlNameModuleType 'M','ddlFiscalYearVariant'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMixedMrpIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlMixedMrpIndicator'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPerioIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlPerioIndicator'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPlanningMaterial, "pr_GetDropDownListByControlNameModuleType 'M','ddlPlanningMaterial'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPlanningSGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPlanningSGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSplitingIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlSplitingIndicator'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
        lblMRPId.Text = "0";
        txtTotalReplenishment.Text = "";
        txtConvFacyor.Text = "";
        txtBackwardCPeriod.Text = "";
        txtForwardCPeriod.Text = "";

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
        ds = ObjMRPDataAccess.GetMRPData3(Convert.ToInt32(lblMasterHeaderId.Text));

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
        MRP3 ObjMRP = GetControlsValue();
        objSavedMRP3 = GetMRPData();
        if (ObjMRP.Plant_Id != null)
        {
            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedMRP3.Mat_MRP3_Id > 0)
                {
                    CheckIfChanges(ObjMRP , objSavedMRP3);
                }
            }
            if (ObjMRPDataAccess.Save(ObjMRP) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjMRP, objSavedMRP3);
                }
                //MSC_8300001775

                //FillDataGrid();
                ClearData();
                //FillMRPData();

                //FillFormDataByMHId();
                flg = true;
					////MSC_8300001775
     //               if (HelperAccess.ReqType == "SMC")
     //               {
     //                   CheckIfChangesLog(ObjMRP, objSavedMRP3);
     //               }
     //               //MSC_8300001775
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

    private MRP3 GetMRPData()
    {
        return ObjMRPDataAccess.GetMRP3(Convert.ToInt32(lblMRPId.Text));
    }

    private MRP3 GetControlsValue()
    {
        MRP3 ObjMRP = new MRP3();
        Utility objUtil = new Utility();
        try
        {
        ObjMRP.Mat_MRP3_Id = Convert.ToInt32(lblMRPId.Text);
        ObjMRP.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjMRP.Plant_Id = ddlPlant.SelectedValue;
        ObjMRP.Storage_Location = ddlStorageLocation.SelectedValue;

        ObjMRP.Period_Indicator = ddlPerioIndicator.SelectedValue;
        ObjMRP.Fiscal_Year_Variant = ddlFiscalYearVariant.SelectedValue;
        ObjMRP.Splitting_Indicator = ddlSplitingIndicator.SelectedValue;
        ObjMRP.Checking_Grp_Availability_Chk = ddlAvailabilityCheck.SelectedValue;
        ObjMRP.Consumption_Mode = ddlConsumptionMode.SelectedValue;
        ObjMRP.BackWard_Consumption_Period = txtBackwardCPeriod.Text;
        ObjMRP.Forward_Consumption_Period = txtForwardCPeriod.Text;
        ObjMRP.Mixed_MRP_Indicator = ddlMixedMrpIndicator.SelectedValue;
        ObjMRP.Replenishment_Lead_Time = txtTotalReplenishment.Text;
        ObjMRP.Planning_Material = ddlPlanningMaterial.SelectedValue;
        ObjMRP.Planning_Plant = ddlPlanningPlant.SelectedValue;
        ObjMRP.Conv_Factor_Plng_Mat = txtConvFacyor.Text;
        ObjMRP.Plng_Strategy_Grp = ddlPlanningSGroup.SelectedValue;

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
        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjMRPDataAccess.GetMRPData3(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMRPId.Text = ds.Tables[0].Rows[0]["Mat_MRP3_Id"].ToString();
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
        MRP3 ObjMRP = GetMRPData();
        if (ObjMRP.Mat_MRP3_Id > 0)
        {
            lblMRPId.Text = ObjMRP.Mat_MRP3_Id.ToString();

            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            ddlPlant.SelectedValue = ObjMRP.Plant_Id;
            BindPlantWiseDropDown();

            ddlStorageLocation.SelectedValue = ObjMRP.Storage_Location;

            ddlPerioIndicator.SelectedValue = ObjMRP.Period_Indicator;
            ddlFiscalYearVariant.SelectedValue = ObjMRP.Fiscal_Year_Variant;
            ddlSplitingIndicator.SelectedValue = ObjMRP.Splitting_Indicator;
            ddlAvailabilityCheck.SelectedValue = ObjMRP.Checking_Grp_Availability_Chk;
            ddlConsumptionMode.SelectedValue = ObjMRP.Consumption_Mode;
            txtBackwardCPeriod.Text = ObjMRP.BackWard_Consumption_Period.ToString();
            txtForwardCPeriod.Text = ObjMRP.Forward_Consumption_Period.ToString();
            ddlMixedMrpIndicator.SelectedValue = ObjMRP.Mixed_MRP_Indicator;
            txtTotalReplenishment.Text = ObjMRP.Replenishment_Lead_Time;
            ddlPlanningMaterial.SelectedValue = ObjMRP.Planning_Material;
            ddlPlanningPlant.SelectedValue = ObjMRP.Planning_Plant.Trim();
            txtConvFacyor.Text = ObjMRP.Conv_Factor_Plng_Mat;
            ddlPlanningSGroup.SelectedValue = ObjMRP.Plng_Strategy_Grp;
        }
        else
        {
            lblMRPId.Text = "0";
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MRP3','" + lblMRPId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR
            ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

            BindPlantWiseDropDown();
            ddlStorageLocation.SelectedValue = Session[StaticKeys.MatStorageLocationId].ToString();

            ddlPerioIndicator.SelectedValue = "M";
            ddlAvailabilityCheck.SelectedValue = "02";

            if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
            {
                if (lblModuleId.Text == "139")
                {
                    if (ddlPlant.SelectedValue.ToString() == "3" || LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
                    {
                        ddlPlanningSGroup.SelectedValue = "11";
                        ddlMixedMrpIndicator.SelectedValue = "2";
                    }
                    else
                    {
                        ddlPlanningSGroup.SelectedValue = "10";
                        ddlMixedMrpIndicator.SelectedValue = "";
                    }
                       
                }
                else if (lblModuleId.Text == "171")
                {
                    if (LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
                    {
                        ddlPlanningSGroup.SelectedValue = "11";
                        ddlMixedMrpIndicator.SelectedValue = "2";
                    }
                    else
                    {
                        ddlPlanningSGroup.SelectedValue = "10";
                        ddlMixedMrpIndicator.SelectedValue = "";
                    }
                }
                else
                {
                    ddlPlanningSGroup.SelectedValue = "10";
                    ddlMixedMrpIndicator.SelectedValue = "";
                }

                ddlConsumptionMode.SelectedValue = "2";
                txtForwardCPeriod.Text = "7";
                txtBackwardCPeriod.Text = "30";
                //dlMixedMrpIndicator.SelectedValue = "2";
            }

            
        }

        ddlPerioIndicator.Enabled = false;
        ddlAvailabilityCheck.Enabled = false;

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
        SectionConfiguration.MRP3 obj = new SectionConfiguration.MRP3();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(MRP3 NewMRP3Data, MRP3 oldMRP3Data)
    {
        try
        {
            if (NewMRP3Data.Mat_MRP3_Id > 0 && oldMRP3Data.Mat_MRP3_Id > 0)
            {
                if (NewMRP3Data.Plant_Id != oldMRP3Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant ID</td> <td>" + oldMRP3Data.Plant_Id + "</td><td>" + NewMRP3Data.Plant_Id + "</td></tr>";
                if (NewMRP3Data.Storage_Location != oldMRP3Data.Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Location</td> <td>" + oldMRP3Data.Storage_Location + "</td><td>" + NewMRP3Data.Storage_Location + "</td></tr>";
                if (NewMRP3Data.Period_Indicator != oldMRP3Data.Period_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Period Indicator</td> <td>" + oldMRP3Data.Period_Indicator + "</td><td>" + NewMRP3Data.Period_Indicator + "</td></tr>";
                if (NewMRP3Data.Fiscal_Year_Variant != oldMRP3Data.Fiscal_Year_Variant)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Fiscal Year Variant</td> <td>" + oldMRP3Data.Fiscal_Year_Variant + "</td><td>" + NewMRP3Data.Fiscal_Year_Variant + "</td></tr>";
                if (NewMRP3Data.Splitting_Indicator != oldMRP3Data.Splitting_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Splitting Indicator</td> <td>" + oldMRP3Data.Splitting_Indicator + "</td><td>" + NewMRP3Data.Splitting_Indicator + "</td></tr>";
                if (NewMRP3Data.Checking_Grp_Availability_Chk != oldMRP3Data.Checking_Grp_Availability_Chk)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Checking Grp Availability Chk</td> <td>" + oldMRP3Data.Checking_Grp_Availability_Chk + "</td><td>" + NewMRP3Data.Checking_Grp_Availability_Chk + "</td></tr>";
                if (NewMRP3Data.Consumption_Mode != oldMRP3Data.Consumption_Mode)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Consumption Mode</td> <td>" + oldMRP3Data.Consumption_Mode + "</td><td>" + NewMRP3Data.Consumption_Mode + "</td></tr>";
                if (NewMRP3Data.BackWard_Consumption_Period != oldMRP3Data.BackWard_Consumption_Period)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>BackWard Consumption Period</td> <td>" + oldMRP3Data.BackWard_Consumption_Period + "</td><td>" + NewMRP3Data.BackWard_Consumption_Period + "</td></tr>";
                if (NewMRP3Data.Forward_Consumption_Period != oldMRP3Data.Forward_Consumption_Period)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Forward Consumption Period</td> <td>" + oldMRP3Data.Forward_Consumption_Period + "</td><td>" + NewMRP3Data.Forward_Consumption_Period + "</td></tr>";
                if (NewMRP3Data.Mixed_MRP_Indicator != oldMRP3Data.Mixed_MRP_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mixed MRP Indicator</td> <td>" + oldMRP3Data.Mixed_MRP_Indicator + "</td><td>" + NewMRP3Data.Mixed_MRP_Indicator + "</td></tr>";
                if (NewMRP3Data.Replenishment_Lead_Time != oldMRP3Data.Replenishment_Lead_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Replenishment Lead Times</td> <td>" + oldMRP3Data.Replenishment_Lead_Time + "</td><td>" + NewMRP3Data.Replenishment_Lead_Time + "</td></tr>";
                if (NewMRP3Data.Planning_Material != oldMRP3Data.Planning_Material)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planning Material </td> <td>" + oldMRP3Data.Planning_Material + "</td><td>" + NewMRP3Data.Planning_Material + "</td></tr>";
                if (NewMRP3Data.Planning_Plant != oldMRP3Data.Planning_Plant)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planning Plant</td> <td>" + oldMRP3Data.Planning_Plant + "</td><td>" + NewMRP3Data.Planning_Plant + "</td></tr>";
                if (NewMRP3Data.Conv_Factor_Plng_Mat != oldMRP3Data.Conv_Factor_Plng_Mat)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Conv Factor Plng Mat</td> <td>" + oldMRP3Data.Conv_Factor_Plng_Mat + "</td><td>" + NewMRP3Data.Conv_Factor_Plng_Mat + "</td></tr>";
                if (NewMRP3Data.Plng_Strategy_Grp != oldMRP3Data.Plng_Strategy_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plng Strategy Grp</td> <td>" + oldMRP3Data.Plng_Strategy_Grp + "</td><td>" + NewMRP3Data.Plng_Strategy_Grp + "</td></tr>";
                
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
    private void CheckIfChangesLog(MRP3 NewMRP3Data, MRP3 oldMRP3Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewMRP3Data.Mat_MRP3_Id > 0 && oldMRP3Data.Mat_MRP3_Id > 0)
            {
                if (NewMRP3Data.Period_Indicator != oldMRP3Data.Period_Indicator)
                    _items.Add(new SMChange { colFieldName = 88, colOldVal = oldMRP3Data.Period_Indicator, colNewVal = NewMRP3Data.Period_Indicator });
                if (NewMRP3Data.Fiscal_Year_Variant != oldMRP3Data.Fiscal_Year_Variant)
                    _items.Add(new SMChange { colFieldName = 89, colOldVal = oldMRP3Data.Fiscal_Year_Variant, colNewVal = NewMRP3Data.Fiscal_Year_Variant });
                if (NewMRP3Data.Splitting_Indicator != oldMRP3Data.Splitting_Indicator)
                    _items.Add(new SMChange { colFieldName = 90, colOldVal = oldMRP3Data.Splitting_Indicator, colNewVal = NewMRP3Data.Splitting_Indicator });
                if (NewMRP3Data.Checking_Grp_Availability_Chk != oldMRP3Data.Checking_Grp_Availability_Chk)
                    _items.Add(new SMChange { colFieldName = 91, colOldVal = oldMRP3Data.Checking_Grp_Availability_Chk, colNewVal = NewMRP3Data.Checking_Grp_Availability_Chk });
                if (NewMRP3Data.Consumption_Mode != oldMRP3Data.Consumption_Mode)
                    _items.Add(new SMChange { colFieldName = 92, colOldVal = oldMRP3Data.Consumption_Mode, colNewVal = NewMRP3Data.Consumption_Mode });
                if (NewMRP3Data.BackWard_Consumption_Period != oldMRP3Data.BackWard_Consumption_Period)
                    _items.Add(new SMChange { colFieldName = 93, colOldVal = oldMRP3Data.BackWard_Consumption_Period, colNewVal = NewMRP3Data.BackWard_Consumption_Period });
                if (NewMRP3Data.Forward_Consumption_Period != oldMRP3Data.Forward_Consumption_Period)
                    _items.Add(new SMChange { colFieldName = 94, colOldVal = oldMRP3Data.Forward_Consumption_Period, colNewVal = NewMRP3Data.Forward_Consumption_Period });
                if (NewMRP3Data.Mixed_MRP_Indicator != oldMRP3Data.Mixed_MRP_Indicator)
                    _items.Add(new SMChange { colFieldName = 95, colOldVal = oldMRP3Data.Mixed_MRP_Indicator, colNewVal = NewMRP3Data.Mixed_MRP_Indicator });
                if (NewMRP3Data.Replenishment_Lead_Time != oldMRP3Data.Replenishment_Lead_Time)
                    _items.Add(new SMChange { colFieldName = 96, colOldVal = oldMRP3Data.Replenishment_Lead_Time, colNewVal = NewMRP3Data.Replenishment_Lead_Time });
                if (NewMRP3Data.Planning_Material != oldMRP3Data.Planning_Material)
                    _items.Add(new SMChange { colFieldName = 97, colOldVal = oldMRP3Data.Planning_Material, colNewVal = NewMRP3Data.Planning_Material });
                if ((NewMRP3Data.Planning_Plant != oldMRP3Data.Planning_Plant) && (oldMRP3Data.Planning_Plant != "0") && (NewMRP3Data.Planning_Plant != "0"))
                    _items.Add(new SMChange { colFieldName = 98, colOldVal = oldMRP3Data.Planning_Plant, colNewVal = NewMRP3Data.Planning_Plant });
                if (NewMRP3Data.Conv_Factor_Plng_Mat != oldMRP3Data.Conv_Factor_Plng_Mat)
                    _items.Add(new SMChange { colFieldName = 99, colOldVal = oldMRP3Data.Conv_Factor_Plng_Mat, colNewVal = NewMRP3Data.Conv_Factor_Plng_Mat });
                if (NewMRP3Data.Plng_Strategy_Grp != oldMRP3Data.Plng_Strategy_Grp)
                    _items.Add(new SMChange { colFieldName = 100, colOldVal = oldMRP3Data.Plng_Strategy_Grp, colNewVal = NewMRP3Data.Plng_Strategy_Grp });

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
                ChangeSMatID1 = helperAccess.MaterialChange("10", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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