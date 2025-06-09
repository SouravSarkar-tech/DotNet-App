using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using SectionConfiguration;
using Accenture.MWT.DomainObject;
using System.Configuration;
using log4net;
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_PlantStorageLocation : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    PlantStorageAccess ObjPlantStorageAccess = new PlantStorageAccess();
    HelperAccess helperAccess = new HelperAccess();
    PlantStorage objSavedPlantStorage = new PlantStorage();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.MasterHeaderId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    MasterHeaderId = Session[StaticKeys.MasterHeaderId].ToString();

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, UserID.ToString())) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                        if (!btnPrevious.Visible && !btnNext.Visible)
                            btnSave.Visible = false;
                        //MSC_8300001775 Start

                        //if (MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId))
                        //{
                        //}
                        //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                        if ((MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId)) && !btnPrevious.Visible && !btnNext.Visible)
                        {
                            btnSave.Visible = true;
                        }
                        //MSC_8300001775 End 
                    }

                    ClearData();
                    //BindGrid();
                    //FillPlantStorageData();
                    ConfigureControl();
                    //CTRL_SUB_SDT06062019
                    CheckControlSubValid();
                    //CTRL_SUB_EDT06062019

                    //To manage the Creation Single request
                    FillFormDataByMHId();
                    lnlAddDetails.Visible = false;
                    gvData.Visible = false;
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
        lableddlPeriodIndiShelfLifeExpDate.Visible = false;
        reqddlPeriodIndiShelfLifeExpDate.Visible = false;
        lableddlRoundingRuleCalcSled.Visible = false;
        reqddlRoundingRuleCalcSled.Visible = false;

        ddlRoundingRuleCalcSled.Enabled = true;
        labletxtMinRemainingShelfLife.Visible = false;
        reqtxtMinRemainingShelfLife.Visible = false;
        txtMinRemainingShelfLife.Enabled = true;
        txtMinRemainingShelfLife.CssClass = "txtbox";
        labletxtTotalShelfLifeDays.Visible = false;
        reqtxtTotalShelfLifeDays.Visible = false;
        txtTotalShelfLifeDays.Enabled = true;
        txtTotalShelfLifeDays.CssClass = "txtbox";

        ddlLabelType.Enabled = true;
        ddlLabelForm.Enabled = true;

        ddlIsMatCtrlSub.Enabled = true;
        lblmrsl.Visible = false;
        labletxtMinRemainingShelfLife.Visible = false;
        txtMinRemainingShelfLife.Visible = false;
        reqtxtMinRemainingShelfLife.Visible = false;
        lbltsl.Visible = false;
        labletxtTotalShelfLifeDays.Visible = false;
        txtTotalShelfLifeDays.Visible = false;
        reqtxtTotalShelfLifeDays.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }


    /// <summary>
    ///  //CTRL_SUB_SDT06062019
    /// </summary>
    private void CheckControlSubValid()
    {
        try
        {
        string deptIds = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
        //if (Convert.ToString(Session[StaticKeys.ctrlsubfieldval]) == "1" && deptIds == Convert.ToString(ConfigurationManager.AppSettings["DepIDRDM"]))
        //{
        //    lblddlIsMatCtrlSub.Visible = true;
        //    lableddlIsMatCtrlSub.Visible = true;
        //    ddlIsMatCtrlSub.Enabled = true;
        //    //ddlIsMatCtrlSub.Visible = true;
        //    reqddlIsMatCtrlSub.Visible = true;
        //}
        //else
        if (Convert.ToString(Session[StaticKeys.ctrlsubfieldval]) == "1")
        {
            lblddlIsMatCtrlSub.Visible = true;
            lableddlIsMatCtrlSub.Visible = true;
            reqddlIsMatCtrlSub.Visible = true;
            if ((Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2") || (deptIds == Convert.ToString(ConfigurationManager.AppSettings["DepIDRDM"])))
            {
                ddlIsMatCtrlSub.Enabled = true;
            }
            else
            {
                ddlIsMatCtrlSub.Enabled = false;
            }
        }
        else
        {
            lblddlIsMatCtrlSub.Visible = true;
            lableddlIsMatCtrlSub.Visible = false;
            ddlIsMatCtrlSub.Enabled = false;
            //ddlIsMatCtrlSub.Visible = false;
            reqddlIsMatCtrlSub.Visible = false;
            if ((deptIds == Convert.ToString(ConfigurationManager.AppSettings["DepIDRDM"])))
            {
                ddlIsMatCtrlSub.Enabled = true;
            }
            else
            {
                ddlIsMatCtrlSub.Enabled = false;
            }

        }

        }
        catch (Exception ex)
        { _log.Error("CheckControlSubValid", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (Save())
        {

            if (((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164)) && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                Response.Redirect("MRP1.aspx");
            }
            else if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                Response.Redirect("WorkScheduling.aspx");
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
        if (Save())
        {
            //lblMsg.Text = Messages.GetMessage(1);
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
            //FillFormDataByMHId();
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            FillFormDataByMHId();
            Response.Redirect("PlantStorageLocation.aspx");
        }

        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        if (Save())
            {//8400000410 comment start

                //if ((lblModuleId.Text == "164" || lblModuleId.Text == "162" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("Quality.aspx");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
        ClearData();
        FillPlantStorageData();
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        lblPlantStorageLocId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_Plant_Storage_Id"].ToString();
        FillPlantStorageData();
        }
        catch (Exception ex)
        { _log.Error("lnkView_Click", ex); }
    }

    protected void lnlAddDetails_Click(object s, EventArgs e)
    {
        try
        {
        ClearData();
        FillPlantStorageData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
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

    private void ClearData()
    {
        try
        {
        lblPlantStorageLocId.Text = "0";
        txtMinRemainingShelfLife.Text = "";
        txtTotalShelfLifeDays.Text = "";
        FieldStatus.ClearPanel(pnlData);
        PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    private bool Save()
    {
        bool flg = false;
        try
        {
            PlantStorage ObjPlantStorage = GetControlValue();
            objSavedPlantStorage = GetPlantStorageData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedPlantStorage.Mat_Plant_Storage_Id > 0)
                {
                    CheckIfChanges(ObjPlantStorage, objSavedPlantStorage);
                }
            }

            if (ObjPlantStorageAccess.Save(ObjPlantStorage) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjPlantStorage, objSavedPlantStorage);
                }
                //MSC_8300001775
                flg = true;
                //BindGrid();
                ClearData();

                //FillPlantStorageData();
                ////MSC_8300001775
                //if (HelperAccess.ReqType == "SMC")
                //{
                //    CheckIfChangesLog(ObjPlantStorage, objSavedPlantStorage);
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
        catch(Exception ex)
        {
            _log.Error("Save", ex);
        }
        return flg;
    }

    private void BindGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjPlantStorageAccess.GetPlantStorageData(Convert.ToInt32(lblMasterHeaderId.Text));

            gvData.DataSource = ds;
            gvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("BindGrid", ex);
        }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {

            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR
        helperAccess.PopuplateDropDownList(ddlStorageLoc, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlLabelForm, "pr_GetDropDownListByControlNameModuleType 'M','ddlLabelForm'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlLabelType, "pr_GetDropDownListByControlNameModuleType 'M','ddlLabelType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRoundingRuleCalcSled, "pr_GetDropDownListByControlNameModuleType 'M','ddlRoundingRuleCalcSled'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlPeriodIndiShelfLifeExpDate, "pr_GetDropDownListByControlNameModuleType 'M','ddlPeriodIndiShelfLifeExpDate'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageCondition, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageCondition'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlUnitMaxStoragePeriod, "pr_GetDropDownListByControlNameModuleType 'M','ddlUnitMaxStoragePeriod'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        //added By : NR, ddlsTypeofChemical CTRL_SUB_SDT06062019
        helperAccess.PopuplateDropDownList(ddlsTypeofChemical, "pr_GetDropDownListByControlNameModuleType 'M','ddlsTypeofChemical'", "LookUp_Desc", "LookUp_Code", "");
            //added By : NR, ddlsTypeofChemical CTRL_SUB_EDT06062019

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private PlantStorage GetPlantStorageData()
    {
        return ObjPlantStorageAccess.GetPlantStorage(Convert.ToInt32(lblPlantStorageLocId.Text));
    }

    public void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlStorageLoc, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        //if (lblModuleId.Text == "138" || lblModuleId.Text == "147" || lblModuleId.Text == "163")
        //{
        //    if (ddlStorageLoc.Items.FindByValue("0151") != null)
        //    {
        //        ddlStorageLoc.SelectedValue = "0151";
        //    }
        //}

        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjPlantStorageAccess.GetPlantStorageData(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblPlantStorageLocId.Text = ds.Tables[0].Rows[0]["Mat_Plant_Storage_Id"].ToString();
        }
        FillPlantStorageData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void FillPlantStorageData()
    {
        try
        {
            PlantStorage ObjPlantStorage = GetPlantStorageData();
            if (ObjPlantStorage.Mat_Plant_Storage_Id > 0)
            {
                lblPlantStorageLocId.Text = ObjPlantStorage.Mat_Plant_Storage_Id.ToString();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjPlantStorage.Plant_Id;

                BindPlantWiseDropDown();

                ddlStorageLoc.SelectedValue = ObjPlantStorage.Storage_Location;
                chkCCIndicatorFixed.Checked = ObjPlantStorage.CC_Indicator_Fixed.ToLower() == "true" ? true : false;
                chkNegativeStockAPlant.Checked = ObjPlantStorage.Negative_Stock_Allowed.ToLower() == "true" ? true : false;
                txtHazardousMNumber.Text = ObjPlantStorage.Hazardous_Mat_No;
                txtMaxStoragePeriod.Text = ObjPlantStorage.Max_Storage_Period.ToString();
                txtMinRemainingShelfLife.Text = ObjPlantStorage.Min_Remaining_Shelf_Life.ToString();
                txtTotalShelfLifeDays.Text = ObjPlantStorage.Total_Shelf_Life_Days.ToString();
                ddlPeriodIndiShelfLifeExpDate.SelectedValue = ObjPlantStorage.Period_Ind_Shelf_Life_Exp_Dt;
                ddlRoundingRuleCalcSled.SelectedValue = ObjPlantStorage.Rnding_Rule_Calc_SLED;
                txtStorageBin.Text = ObjPlantStorage.Storage_bin;
                ddlStorageCondition.SelectedValue = ObjPlantStorage.Storage_Condition;
                txtStoragePercentage.Text = ObjPlantStorage.Storage_Perc.ToString();
                txtTemperatureCondIndi.Text = ObjPlantStorage.Temperature_Cond_Ind;

                ddlUnitMaxStoragePeriod.SelectedValue = ObjPlantStorage.Unit_Max_Storage_Period;

                ddlLabelType.SelectedValue = ObjPlantStorage.Label_Type;
                ddlLabelForm.SelectedValue = ObjPlantStorage.Label_Form;
                ddlProfitCenter.SelectedValue = ObjPlantStorage.Profit_Center;
                //CTRL_SUB_SDT06062019
                //SDT31072019
                ddlIsMatCtrlSub.SelectedValue = ObjPlantStorage.sIsMatCtrlSub;
                DDLIsMatCtrlSub1(ddlIsMatCtrlSub.SelectedValue);
                ddlsTypeofChemical.SelectedValue = ObjPlantStorage.sTypeofChemical;



                //CTRL_SUB_SDT06062019
            }
            else
            {
                lblPlantStorageLocId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','PS1','" + lblPlantStorageLocId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
                ddlStorageLoc.SelectedValue = Session[StaticKeys.MatStorageLocationId].ToString();

                ddlPeriodIndiShelfLifeExpDate.SelectedValue = "M";
                ddlRoundingRuleCalcSled.SelectedValue = "+";


            }

            if (lblModuleId.Text != "162" && lblModuleId.Text != "164")
                ddlPeriodIndiShelfLifeExpDate.Enabled = false;
            ddlRoundingRuleCalcSled.Enabled = false;

            ddlPlant.Enabled = false;
            ddlStorageLoc.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillPlantStorageData", ex); 
        }
    }

    public PlantStorage GetControlValue()
    {
        Utility objUtil = new Utility();
       
            PlantStorage ObjPlantStorage = new PlantStorage();
        try
        {
            ObjPlantStorage.Mat_Plant_Storage_Id = Convert.ToInt32(lblPlantStorageLocId.Text);
            ObjPlantStorage.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjPlantStorage.Plant_Id = ddlPlant.SelectedValue;
            ObjPlantStorage.Storage_Location = ddlStorageLoc.SelectedValue;

            ObjPlantStorage.CC_Indicator_Fixed = chkCCIndicatorFixed.Checked == true ? "1" : "0";
            ObjPlantStorage.Negative_Stock_Allowed = chkNegativeStockAPlant.Checked == true ? "1" : "0";
            ObjPlantStorage.Hazardous_Mat_No = txtHazardousMNumber.Text;
            ObjPlantStorage.Max_Storage_Period = txtMaxStoragePeriod.Text;
            ObjPlantStorage.Min_Remaining_Shelf_Life = txtMinRemainingShelfLife.Text;
            ObjPlantStorage.Period_Ind_Shelf_Life_Exp_Dt = ddlPeriodIndiShelfLifeExpDate.SelectedValue;
            ObjPlantStorage.Rnding_Rule_Calc_SLED = ddlRoundingRuleCalcSled.SelectedValue;
            ObjPlantStorage.Storage_bin = txtStorageBin.Text;
            ObjPlantStorage.Storage_Condition = ddlStorageCondition.SelectedValue;
            ObjPlantStorage.Storage_Perc = txtStoragePercentage.Text;
            ObjPlantStorage.Temperature_Cond_Ind = txtTemperatureCondIndi.Text;
            ObjPlantStorage.Total_Shelf_Life_Days = txtTotalShelfLifeDays.Text;
            ObjPlantStorage.Unit_Max_Storage_Period = ddlUnitMaxStoragePeriod.SelectedValue;

            ObjPlantStorage.Label_Type = ddlLabelType.SelectedValue;
            ObjPlantStorage.Label_Form = ddlLabelForm.SelectedValue;
            ObjPlantStorage.Profit_Center = ddlProfitCenter.SelectedValue;


            ObjPlantStorage.IsActive = 1;
            ObjPlantStorage.UserId = lblUserId.Text;
            ObjPlantStorage.IPAddress = objUtil.GetIpAddress();
            //CTRL_SUB_SDT06062019
            ObjPlantStorage.sTypeofChemical = ddlsTypeofChemical.SelectedValue;
            //SDT31072019
            ObjPlantStorage.sIsMatCtrlSub = ddlIsMatCtrlSub.SelectedValue;
            //CTRL_SUB_SDT06062019
           
        }
        catch(Exception ex)
        {

            _log.Error("GetControlValue", ex);
        }
        return ObjPlantStorage;
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Plant_Storage_Location obj = new SectionConfiguration.Plant_Storage_Location();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(PlantStorage NewPlantStorageData, PlantStorage oldPlantStorageData)
    {
        try
        {
            if (NewPlantStorageData.Mat_Plant_Storage_Id > 0 && oldPlantStorageData.Mat_Plant_Storage_Id > 0)
            {
                if (NewPlantStorageData.Plant_Id != oldPlantStorageData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldPlantStorageData.Plant_Id + "</td><td>" + NewPlantStorageData.Plant_Id + "</td></tr>";
                if (NewPlantStorageData.Storage_Location != oldPlantStorageData.Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Location</td> <td>" + oldPlantStorageData.Storage_Location + "</td><td>" + NewPlantStorageData.Storage_Location + "</td></tr>";
                if (NewPlantStorageData.Storage_bin != oldPlantStorageData.Storage_bin)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage bin</td> <td>" + oldPlantStorageData.Storage_bin + "</td><td>" + NewPlantStorageData.Storage_bin + "</td></tr>";
                if (NewPlantStorageData.Storage_Condition != oldPlantStorageData.Storage_Condition)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Condition</td> <td>" + oldPlantStorageData.Storage_Condition + "</td><td>" + NewPlantStorageData.Storage_Condition + "</td></tr>";
                if (NewPlantStorageData.Temperature_Cond_Ind != oldPlantStorageData.Temperature_Cond_Ind)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Temperature Cond Ind</td> <td>" + oldPlantStorageData.Temperature_Cond_Ind + "</td><td>" + NewPlantStorageData.Temperature_Cond_Ind + "</td></tr>";
                if (NewPlantStorageData.Hazardous_Mat_No != oldPlantStorageData.Hazardous_Mat_No)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Old Material Number</td> <td>" + oldPlantStorageData.Hazardous_Mat_No + "</td><td>" + NewPlantStorageData.Hazardous_Mat_No + "</td></tr>";
                if (NewPlantStorageData.Max_Storage_Period != oldPlantStorageData.Max_Storage_Period)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Max Storage Period</td> <td>" + oldPlantStorageData.Max_Storage_Period + "</td><td>" + NewPlantStorageData.Max_Storage_Period + "</td></tr>";
                if (NewPlantStorageData.Unit_Max_Storage_Period != oldPlantStorageData.Unit_Max_Storage_Period)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Max Storage Period</td> <td>" + oldPlantStorageData.Unit_Max_Storage_Period + "</td><td>" + NewPlantStorageData.Unit_Max_Storage_Period + "</td></tr>";
                if (NewPlantStorageData.Min_Remaining_Shelf_Life != oldPlantStorageData.Min_Remaining_Shelf_Life)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Remaining Shelf Life</td> <td>" + oldPlantStorageData.Min_Remaining_Shelf_Life + "</td><td>" + NewPlantStorageData.Min_Remaining_Shelf_Life + "</td></tr>";
                if (NewPlantStorageData.Total_Shelf_Life_Days != oldPlantStorageData.Total_Shelf_Life_Days)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Total Shelf Life Days</td> <td>" + oldPlantStorageData.Total_Shelf_Life_Days + "</td><td>" + NewPlantStorageData.Total_Shelf_Life_Days + "</td></tr>";
                if (NewPlantStorageData.Storage_Perc != oldPlantStorageData.Storage_Perc)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Perc</td> <td>" + oldPlantStorageData.Storage_Perc + "</td><td>" + NewPlantStorageData.Storage_Perc + "</td></tr>";
                if (NewPlantStorageData.Negative_Stock_Allowed != (oldPlantStorageData.Negative_Stock_Allowed.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Negative Stock Allowed</td> <td>" + (oldPlantStorageData.Negative_Stock_Allowed.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewPlantStorageData.Negative_Stock_Allowed + "</td></tr>";
                if (NewPlantStorageData.CC_Indicator_Fixed != (oldPlantStorageData.CC_Indicator_Fixed.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>CC Indicator Fixed</td> <td>" + (oldPlantStorageData.CC_Indicator_Fixed.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewPlantStorageData.CC_Indicator_Fixed + "</td></tr>";
                if (NewPlantStorageData.Rnding_Rule_Calc_SLED != oldPlantStorageData.Rnding_Rule_Calc_SLED)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Rounding Rule Calc SLED</td> <td>" + oldPlantStorageData.Rnding_Rule_Calc_SLED + "</td><td>" + NewPlantStorageData.Rnding_Rule_Calc_SLED + "</td></tr>";
                if (NewPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt != oldPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Period Ind Shelf Life Exp Dt</td> <td>" + oldPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt + "</td><td>" + NewPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt + "</td></tr>";
                if (NewPlantStorageData.Label_Type != oldPlantStorageData.Label_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Label Type</td> <td>" + oldPlantStorageData.Label_Type + "</td><td>" + NewPlantStorageData.Label_Type + "</td></tr>";
                if (NewPlantStorageData.Label_Form != oldPlantStorageData.Label_Form)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Label Form</td> <td>" + oldPlantStorageData.Label_Form + "</td><td>" + NewPlantStorageData.Label_Form + "</td></tr>";
                if (NewPlantStorageData.Profit_Center != oldPlantStorageData.Profit_Center)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Profit Center</td> <td>" + oldPlantStorageData.Profit_Center + "</td><td>" + NewPlantStorageData.Profit_Center + "</td></tr>";
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
    private void CheckIfChangesLog(PlantStorage NewPlantStorageData, PlantStorage oldPlantStorageData)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewPlantStorageData.Mat_Plant_Storage_Id > 0 && oldPlantStorageData.Mat_Plant_Storage_Id > 0)
            {
                if (NewPlantStorageData.Storage_bin != oldPlantStorageData.Storage_bin)
                    _items.Add(new SMChange { colFieldName = 136, colOldVal = oldPlantStorageData.Storage_bin, colNewVal = NewPlantStorageData.Storage_bin });
                //if (NewPlantStorageData.Storage_Condition != oldPlantStorageData.Storage_Condition)
                //    _items.Add(new SMChange { colFieldName = 137, colOldVal = oldPlantStorageData.Storage_Condition, colNewVal = NewPlantStorageData.Storage_Condition });
                if (NewPlantStorageData.Temperature_Cond_Ind != oldPlantStorageData.Temperature_Cond_Ind)
                    _items.Add(new SMChange { colFieldName = 138, colOldVal = oldPlantStorageData.Temperature_Cond_Ind, colNewVal = NewPlantStorageData.Temperature_Cond_Ind });
                if (NewPlantStorageData.Hazardous_Mat_No != oldPlantStorageData.Hazardous_Mat_No)
                    _items.Add(new SMChange { colFieldName = 139, colOldVal = oldPlantStorageData.Hazardous_Mat_No, colNewVal = NewPlantStorageData.Hazardous_Mat_No });
                if (NewPlantStorageData.Max_Storage_Period != oldPlantStorageData.Max_Storage_Period)
                    _items.Add(new SMChange { colFieldName = 140, colOldVal = oldPlantStorageData.Max_Storage_Period, colNewVal = NewPlantStorageData.Max_Storage_Period });
                if (NewPlantStorageData.Unit_Max_Storage_Period != oldPlantStorageData.Unit_Max_Storage_Period)
                    _items.Add(new SMChange { colFieldName = 141, colOldVal = oldPlantStorageData.Unit_Max_Storage_Period, colNewVal = NewPlantStorageData.Unit_Max_Storage_Period });
                if (NewPlantStorageData.Min_Remaining_Shelf_Life != oldPlantStorageData.Min_Remaining_Shelf_Life)
                    _items.Add(new SMChange { colFieldName = 142, colOldVal = oldPlantStorageData.Min_Remaining_Shelf_Life, colNewVal = NewPlantStorageData.Min_Remaining_Shelf_Life });
                if (NewPlantStorageData.Total_Shelf_Life_Days != oldPlantStorageData.Total_Shelf_Life_Days)
                    _items.Add(new SMChange { colFieldName = 143, colOldVal = oldPlantStorageData.Total_Shelf_Life_Days, colNewVal = NewPlantStorageData.Total_Shelf_Life_Days });
                if (NewPlantStorageData.Storage_Perc != oldPlantStorageData.Storage_Perc)
                    _items.Add(new SMChange { colFieldName = 144, colOldVal = oldPlantStorageData.Storage_Perc, colNewVal = NewPlantStorageData.Storage_Perc });
                if ((NewPlantStorageData.Negative_Stock_Allowed.ToLower() == "true" ? "1" : "0") != (oldPlantStorageData.Negative_Stock_Allowed.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 145, colOldVal = (oldPlantStorageData.Negative_Stock_Allowed.ToLower() == "true" ? "X" : ""), colNewVal = (NewPlantStorageData.Negative_Stock_Allowed.ToLower() == "true" ? "X" : "") });
                if ((NewPlantStorageData.CC_Indicator_Fixed.ToLower() == "true" ? "1" : "0") != (oldPlantStorageData.CC_Indicator_Fixed.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 146, colOldVal = (oldPlantStorageData.CC_Indicator_Fixed.ToLower() == "true" ? "X" : ""), colNewVal = (NewPlantStorageData.CC_Indicator_Fixed.ToLower() == "true" ? "X" : "") });
                if (NewPlantStorageData.Rnding_Rule_Calc_SLED != oldPlantStorageData.Rnding_Rule_Calc_SLED)
                    _items.Add(new SMChange { colFieldName = 147, colOldVal = oldPlantStorageData.Rnding_Rule_Calc_SLED, colNewVal = NewPlantStorageData.Rnding_Rule_Calc_SLED });
                if (NewPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt != oldPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt)
                    _items.Add(new SMChange { colFieldName = 148, colOldVal = oldPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt, colNewVal = NewPlantStorageData.Period_Ind_Shelf_Life_Exp_Dt });
                if (NewPlantStorageData.Label_Type != oldPlantStorageData.Label_Type)
                    _items.Add(new SMChange { colFieldName = 1203, colOldVal = oldPlantStorageData.Label_Type, colNewVal = NewPlantStorageData.Label_Type });
                if (NewPlantStorageData.Label_Form != oldPlantStorageData.Label_Form)
                    _items.Add(new SMChange { colFieldName = 1204, colOldVal = oldPlantStorageData.Label_Form, colNewVal = NewPlantStorageData.Label_Form });
                if (NewPlantStorageData.Profit_Center != oldPlantStorageData.Profit_Center)
                    _items.Add(new SMChange { colFieldName = 1346, colOldVal = oldPlantStorageData.Profit_Center, colNewVal = NewPlantStorageData.Profit_Center });
                if (NewPlantStorageData.sTypeofChemical != oldPlantStorageData.sTypeofChemical)
                    _items.Add(new SMChange { colFieldName = 1405, colOldVal = oldPlantStorageData.sTypeofChemical, colNewVal = NewPlantStorageData.sTypeofChemical });
            }
            //sIsMatCtrlSub

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
                ChangeSMatID1 = helperAccess.MaterialChange("13", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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

    /// <summary>
    /// CTRL_SUB_SDT06062019, Desc : Controll Substance , Change By : Nitin R
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlIsMatCtrlSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        //added By : NR, ddlsTypeofChemical CTRL_SUB_SDT06062019
        helperAccess.PopuplateDropDownList(ddlsTypeofChemical, "pr_GetDropDownListByControlNameModuleType 'M','ddlsTypeofChemical'", "LookUp_Desc", "LookUp_Code", "");
        //added By : NR, ddlsTypeofChemical CTRL_SUB_EDT06062019

        DDLIsMatCtrlSub1(ddlIsMatCtrlSub.SelectedValue);
        }
        catch (Exception ex)
        { _log.Error("ddlIsMatCtrlSub_SelectedIndexChanged", ex); }
    }

    ///CTRL_SUB_SDT06062019
    private void DDLIsMatCtrlSub1(string sddlIsMatCtrlSub)
    {
        try
        {
        //MSC_8300001775 Start
        if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
        {
            string deptIds = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            if (ddlIsMatCtrlSub.SelectedValue == "1")
            {
                lblddlsTypeofChemical.Visible = true;
                labelddlsTypeofChemical.Visible = true;
                reqddlsTypeofChemical.Visible = true;
                if ((Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2") || (deptIds == Convert.ToString(ConfigurationManager.AppSettings["DepIDRDM"])))
                {
                    ddlsTypeofChemical.Enabled = true;
                }
                else
                {
                    ddlsTypeofChemical.Enabled = false;
                }
            }
            else
            {
                lblddlsTypeofChemical.Visible = true;
                labelddlsTypeofChemical.Visible = false;
                ddlsTypeofChemical.Enabled = false;
                //ddlsTypeofChemical.Visible = false;
                reqddlsTypeofChemical.Visible = false;

            }
        }

        }
        catch (Exception ex)
        { _log.Error("DDLIsMatCtrlSub1", ex); }
    }
}