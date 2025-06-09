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
public partial class Transaction_Material_ExtensionData : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MaterialCreateExtensionAccess ObjMaterialCreateExtensionAccess = new MaterialCreateExtensionAccess();
    HelperAccess helperAccess = new HelperAccess();
    //LLM_DPT_SDT30072019 Commented By Nitin R
    //List<string> LLMDPTPlantList = new List<string> { "9", "15", "16", "17", "23", "24", "25", "90", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "138", "139", "140", "141", "142", "28", "29", "30", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "83", "84", "85", "86", "87", "88", "89", "90" };
    //LLM_DPT_SDT30072019 Commented By Nitin R

    //LLM_DPT_SDT30072019 Added By Nitin R
    List<string> LLMDPTPlantList = new List<string>();
    //LLM_DPT_SDT30072019 Added By Nitin R
    private short _tabIndex = 0;

    public short TabIndex
    {
        get
        {
            _tabIndex++;
            return _tabIndex;
        }
    }

    #region Page Event

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                //Added for Testing
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                //PopuplateDropDownList();



                string sectionId = lblSectionId.Text.ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string mode = Session[StaticKeys.Mode].ToString();
                lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    //MSC_8300001775 Commented by NR Start
                    //trButton.Visible = true;
                    ////btnSave.Visible = !btnNext.Visible;
                    //btnSave.Visible = true;
                    //btnNext.Visible = true;
                    //MSC_8300001775 Commented by NR End
                    //MSC_8300001775 added by NR Start
                    trButton.Visible = true;
                    btnSave.Visible = !btnNext.Visible;
                    if (!btnPrevious.Visible && !btnNext.Visible)
                        btnSave.Visible = false;
                    //MSC_8300001775 added by NR end
                }
                else
                {
                    grvData.Columns[4].Visible = false;
                    lnlAddDetails.Visible = false;
                    pnlData.Visible = false;
                }

                //ClearCostingData();
                ////FillCostingData();
                ////FillCostingDataGrid();
                //ConfigureControl();

                ////To manage the Creation Single request
                //FillFormDataByMHId();
                //lnlAddDetails.Visible = false;
                //grvCosting1.Visible = false;
                //LLM_DPT_SDT30072019 Added By Nitin R
                LLMDPTPlantListMet();
                //LLM_DPT_EDT30072019 Added By Nitin R
                ClearData();
                FillFormDataByMHId();
                ConfigureControl();

            }
        }
        else
        {
            Response.Redirect("materialmaster.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected override void OnUnload(EventArgs e)
    {
        try
        {
        base.OnUnload(e);

        // your code
        Session["ExtensionMain"] = null;
        Session["ExtensionNew"] = null;
        }
        catch (Exception ex)
        { _log.Error("OnUnload", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (Save())
        {
            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                Response.Redirect("Quality.aspx");
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
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

                //Response.Redirect("ExtensionData.aspx");
                //FillFormDataByMHId();
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }
       
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        //MSC_8300001775 Commented by NR Start
        //Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
        //Response.Redirect("MaterialMaster.aspx");
        //MSC_8300001775 Commented by NR End
        //MSC_8300001775 Added by NR Start
        if (Save())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
        //MSC_8300001775 Added by NR end
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

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
        ClearData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        string CreateExtensionId = grvData.DataKeys[grdrow.RowIndex]["Mat_Create_Extension_Id"].ToString();

        if (ObjMaterialCreateExtensionAccess.DeleteMaterialCreateExtensionData(SafeTypeHandling.ConvertStringToInt32(CreateExtensionId)) > 0)
        {
            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }

        FillFormDataByMHId();
        }
        catch (Exception ex)
        { _log.Error("lnkDelete_Click", ex); }
        //GridViewRow grv1 = Grd_Extension.FooterRow;
        //FillControlData(grv1, "f", ObjMaterialCreateExtension);
    }

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        try
        {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        lblCreateExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Create_Extension_Id"].ToString();
        FillControlData();

        lblCreateExtensionId.Text = "0";
        ddlPlant.SelectedIndex = 0;

        ddlPlant.Enabled = true;

        }
        catch (Exception ex)
        { _log.Error("lnkCopy_Click", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblCreateExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Create_Extension_Id"].ToString();
            FillControlData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkCopy_Click", ex);
        }
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

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        BindSalesOrgWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    protected void lnkRefreshddlInspectionType_Click(object sender, EventArgs e)
    {
        try
        {
        DisplayInspectionType();
        }
        catch (Exception ex)
        { _log.Error("lnkRefreshddlInspectionType_Click", ex); }
    }

    protected void ddlInspectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        DisplayInspectionType();
        }
        catch (Exception ex)
        { _log.Error("ddlInspectionType_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Public Method

    /// <summary>
    /// LLM_DPT_SDT30072019 
    /// Update list
    /// </summary>
    private void LLMDPTPlantListMet()
    {
        try
        {
            QualityAccess ObjQualityAccess = new QualityAccess();
            DataSet ds;
            ds = ObjQualityAccess.GetLLMDPTPlantList();

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
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','All','" + lblCreateExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {


            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '0','MRP1','0'", "Plant_Name", "Plant_Code", "");
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','All','" + lblCreateExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','All','" + lblCreateExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        //BindPlantWiseDropDown(grv, ext);

        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblCreateExtensionId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlAccountAssignment, "pr_GetDropDownListByControlNameModuleType 'M','ddlAccountAssignment'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMaterialPGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialPGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPriceControlIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlPriceControlIndicator'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlLotSize, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownCheckBox(ddlInspectionType, "pr_GetDropDownListByControlNameModuleType 'M','ddlInspectionType'", "LookUp_Desc", "LookUp_Code");

        helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        
        string AccountCat = MaterialHelper.GetAccountCategoryByModuleId(lblModuleId.Text);
        helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass','1','" + AccountCat + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet dataSet = ObjMaterialCreateExtensionAccess.GetMaterialCreateExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));

        grvData.DataSource = dataSet;
        grvData.DataBind();

        FillControlData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','8','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIssueStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','13','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164))
        {
            ClearSelectedValue(ddlInspectionType);
            if (!LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
            {
                lableddlInspectionType1.Text = "";
                //MSC_8300001775 Start Comment 
                //if (lblModuleId.Text == "164" && ddlPlant.SelectedValue.ToString() == "10")
                //    SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,89      ,Z3      ");
                //else
                //    SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,89      ");
                //MSC_8300001775 End Comment
                //MSC_8300001775 Start
                if (lblModuleId.Text == "164" && ddlPlant.SelectedValue.ToString() == "10")
                    SetSelectedValue(ddlInspectionType, "01,05,08,09,89,Z3");
                else
                    SetSelectedValue(ddlInspectionType, "01,05,08,09,89");
                //MSC_8300001775 End

                txtIntervalNPInspector.Text = "360";
            }
            DisplayInspectionType();

            if (LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
            {
                lableddlInspectionType.Visible = false;
            }
            else
            {
                lableddlInspectionType.Visible = true;
            }
        }

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void BindSalesOrgWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblCreateExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("BindSalesOrgWiseDropDown", ex); }
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

    private void MRPTypeWiseSetup()
    {
        try
        {
        if (ddlMrpType.SelectedValue == "ND")
        {
            reqddlMrpController.Visible = false;
            reqddlLotSize.Visible = false;
            reqtxtReorder.Visible = false;

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
            //txtPlanningTimeFence.Enabled = false;
            //txtMaxStockLevel.Enabled = false;

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
            //txtPlanningTimeFence.Enabled = true;
            //txtMaxStockLevel.Enabled = true;
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
            //txtPlanningTimeFence.Enabled = true;
            //txtMaxStockLevel.Enabled = true;

            LotSizeWiseValidation();
        }

        }
        catch (Exception ex)
        { _log.Error("MRPTypeWiseSetup", ex); }
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

                //reqtxtMaxStockLevel.Visible = false;
                //labletxtMaxStockLevel.Visible = false;

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

                //reqtxtMaxStockLevel.Visible = true;
                //labletxtMaxStockLevel.Visible = true;

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

                //reqtxtMaxStockLevel.Visible = false;
                //labletxtMaxStockLevel.Visible = false;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;

                txtFixedLotSize.Text = "";
            }
        }
        else
        {
            ddlMrpController.SelectedValue = "";
            ddlLotSize.SelectedValue = "";


            reqtxtFixedLotSize.Visible = false;
            labletxtFixedLotSize.Visible = false;

            //reqtxtMaxStockLevel.Visible = false;
            //labletxtMaxStockLevel.Visible = false;

            txtFixedLotSize.Enabled = false;
            txtMinLotSize.Enabled = false;
            txtMaxLotSize.Enabled = false;
            txtRoundingValue.Enabled = false;

            txtFixedLotSize.Text = "";
            }
        }
        catch (Exception ex)
        { _log.Error("LotSizeWiseValidation", ex); }
    }

    protected void ClearData()
    {
        try
        {
        lblCreateExtensionId.Text = "0";

        ddlPlant.Enabled = true;

        txtReorder.Text = "";
        txtMinLotSize.Text = "";
        txtMaxLotSize.Text = "";
        txtFixedLotSize.Text = "";
        txtRoundingValue.Text = "";
        txtGRProcessingTime.Text = "";
        txtPlannedDeleveryTime.Text = "";

        ClearSelectedValue(ddlInspectionType);

        PopuplateDropDownList();
        ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

        txtIntervalNPInspector.Text = "";

        BindPlantWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }
   
    private bool CheckPlantValid(string PlantId,string Storage_Location)
    {
        bool flg = true;
        try
        {
        if (PlantId == Session[StaticKeys.MaterialPlantId].ToString() && Storage_Location  == Session[StaticKeys.MatStorageLocationId].ToString() )
        {
            flg = false;
        }
        else
        {
            foreach (GridViewRow grv in grvData.Rows)
            {
                Label lblPlantID = (Label)grv.FindControl("lblPlantID");
                Label lblStorageLocation = (Label)grv.FindControl("lblStorageLocation");

                
                if (lblPlantID.Text == PlantId)
                {
                    if (lblStorageLocation.Text == Storage_Location)
                    {
                        flg = false;
                        break;
                    }
                }
            }
        }

        }
        catch (Exception ex)
        { _log.Error("CheckPlantValid", ex); }
        return flg;
    }

    private bool Save()
    {
        bool flg = false;
        try
        {
            MaterialCreateExtension ObjMaterialCreateExtension = GetControlsValue();

            if (ObjMaterialCreateExtension.Plant_Id != null)
            {
                if (CheckPlantValid(ObjMaterialCreateExtension.Plant_Id, ObjMaterialCreateExtension.Storage_Location) || ObjMaterialCreateExtension.Mat_Create_Extension_Id > 0)
                {
                    if (ObjMaterialCreateExtension.Inspection_Type != null || !lableddlInspectionType.Visible)
                    {


                        if (ObjMaterialCreateExtensionAccess.Save(ObjMaterialCreateExtension) > 0)
                        {
                            ClearData();
                            FillFormDataByMHId();
                            flg = true; 
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

                        lblMsg.Text = "Please Select atleast one Inspection Type to proceed.";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                        flg = false;
                    }
                }
                else
                {
                    lblMsg.Text = "Plant,Storage Location combination already exists, please select other combination.";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Please Select Plant to proceed.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("Save", ex);
        }
        return flg;
    }

    private MaterialCreateExtension GetControlsValue()
    {
       
        MaterialCreateExtension ObjMaterialCreateExtension = new MaterialCreateExtension();
        Utility objUtil = new Utility();

        try
        {
            ObjMaterialCreateExtension.Mat_Create_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblCreateExtensionId.Text);
            ObjMaterialCreateExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);
                        
            ObjMaterialCreateExtension.Plant_Id = ddlPlant.SelectedValue;

            ObjMaterialCreateExtension.Storage_Location = ddlStorageLocation.SelectedValue;
            ObjMaterialCreateExtension.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjMaterialCreateExtension.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;
            ObjMaterialCreateExtension.Mat_Pricing_Group = ddlMaterialPGroup.SelectedValue;
            ObjMaterialCreateExtension.Acc_Assignment_Grp = ddlAccountAssignment.SelectedValue;
            ObjMaterialCreateExtension.Purchasing_Group = ddlPurchasingGroup.SelectedValue;
            ObjMaterialCreateExtension.MRP_Type = ddlMrpType.SelectedValue;
            ObjMaterialCreateExtension.MRP_Controller = ddlMrpController.SelectedValue;
            ObjMaterialCreateExtension.Reorder_Point = txtReorder.Text;
            ObjMaterialCreateExtension.Lot_Size = ddlLotSize.SelectedValue;
            ObjMaterialCreateExtension.Min_Lot_Size = txtMinLotSize.Text;
            ObjMaterialCreateExtension.Max_Lot_Size = txtMaxLotSize.Text;
            ObjMaterialCreateExtension.Fixed_Lot_Size = txtFixedLotSize.Text;
            ObjMaterialCreateExtension.Rounding_Value = txtRoundingValue.Text;
            ObjMaterialCreateExtension.Issue_Storage_Location = ddlIssueStorageLocation.SelectedValue;
            ObjMaterialCreateExtension.GR_Processing_Time = txtGRProcessingTime.Text;
            ObjMaterialCreateExtension.Planned_Delivery_Time_Days = txtPlannedDeleveryTime.Text;
            ObjMaterialCreateExtension.Profit_Center = ddlProfitCenter.SelectedValue;
            ObjMaterialCreateExtension.Valuation_Class = ddlValuationClass.SelectedValue;
            ObjMaterialCreateExtension.Price_Ctrl_Indicator = ddlPriceControlIndicator.SelectedValue;
            ObjMaterialCreateExtension.Inspection_Type = GetSelectedCheckedValue(ddlInspectionType) == null ? "" : GetSelectedCheckedValue(ddlInspectionType);
            ObjMaterialCreateExtension.Spl_Procurement_Type = ddlSpecialProcType.SelectedValue;
            ObjMaterialCreateExtension.Interval_Nxt_Inspection = txtIntervalNPInspector.Text;

            ObjMaterialCreateExtension.IsActive = "1";
            ObjMaterialCreateExtension.UserId = lblUserId.Text;
            ObjMaterialCreateExtension.TodayDate = objUtil.GetDate();
            ObjMaterialCreateExtension.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjMaterialCreateExtension;
    }

    private void FillControlData()
    {
      
        MaterialCreateExtension ObjMaterialCreateExtension = ObjMaterialCreateExtensionAccess.GetMaterialCreateExtension(Convert.ToInt32(lblCreateExtensionId.Text));
        Utility objUtil = new Utility();

        try
        {
            //PopuplateDropDownList();

            if (ObjMaterialCreateExtension.Mat_Create_Extension_Id == 0)
            {
                ObjMaterialCreateExtension = ObjMaterialCreateExtensionAccess.GetMaterialCreateExtensionReferenceObj(lblMasterHeaderId.Text);
            }

            if (ObjMaterialCreateExtension.Plant_Id != "")
            {
                ddlPlant.SelectedValue = ObjMaterialCreateExtension.Plant_Id;
                ddlPlant.Enabled = false;
                BindPlantWiseDropDown();

                ddlStorageLocation.SelectedValue = ObjMaterialCreateExtension.Storage_Location;
                ddlSalesOrginization.SelectedValue = ObjMaterialCreateExtension.Sales_Organization_Id;
                BindSalesOrgWiseDropDown();

                ddlDistributionChannel.SelectedValue = ObjMaterialCreateExtension.Distribution_Channel_ID;
                ddlMaterialPGroup.SelectedValue = ObjMaterialCreateExtension.Mat_Pricing_Group;
                ddlAccountAssignment.SelectedValue = ObjMaterialCreateExtension.Acc_Assignment_Grp;
                ddlPurchasingGroup.SelectedValue = ObjMaterialCreateExtension.Purchasing_Group;
                ddlMrpType.SelectedValue = ObjMaterialCreateExtension.MRP_Type;
                MRPTypeWiseSetup();

                ddlMrpController.SelectedValue = ObjMaterialCreateExtension.MRP_Controller;
                txtReorder.Text = ObjMaterialCreateExtension.Reorder_Point;
                ddlLotSize.SelectedValue = ObjMaterialCreateExtension.Lot_Size;
                LotSizeWiseValidation();

                txtMinLotSize.Text = ObjMaterialCreateExtension.Min_Lot_Size;
                txtMaxLotSize.Text = ObjMaterialCreateExtension.Max_Lot_Size;
                txtFixedLotSize.Text = ObjMaterialCreateExtension.Fixed_Lot_Size;
                txtRoundingValue.Text = ObjMaterialCreateExtension.Rounding_Value;
                ddlIssueStorageLocation.SelectedValue = ObjMaterialCreateExtension.Issue_Storage_Location;
                txtGRProcessingTime.Text = ObjMaterialCreateExtension.GR_Processing_Time;
                txtPlannedDeleveryTime.Text = ObjMaterialCreateExtension.Planned_Delivery_Time_Days;
                ddlProfitCenter.SelectedValue = ObjMaterialCreateExtension.Profit_Center;
                ddlValuationClass.SelectedValue = ObjMaterialCreateExtension.Valuation_Class;
                ddlPriceControlIndicator.SelectedValue = ObjMaterialCreateExtension.Price_Ctrl_Indicator;

                SetSelectedValue(ddlInspectionType, ObjMaterialCreateExtension.Inspection_Type);
                DisplayInspectionType();

                ddlSpecialProcType.SelectedValue = ObjMaterialCreateExtension.Spl_Procurement_Type;
                txtIntervalNPInspector.Text = ObjMaterialCreateExtension.Interval_Nxt_Inspection;
            }
            if (ObjMaterialCreateExtension.Mat_Create_Extension_Id == 0)
            {
                ddlPlant.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("FillControlData", ex);
        }
    }
   
    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Material_Extension_Data obj = new SectionConfiguration.Material_Extension_Data();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void DisplayInspectionType()
    {
        try
        {
        string InspectionType = GetSelectedCheckedValue(ddlInspectionType);
        if (InspectionType != null)
        {
            lableddlInspectionType1.Text = "Inspection Type :  " + InspectionType.Substring(0, InspectionType.Length - 1);
        }
        else
        {
            lableddlInspectionType1.Text = "";
        }
        //"<b><u>Inspection Type</u> :  </b>" + InspectionType.Substring(0, InspectionType.Length - 1);
        reqtxtIntervalNPInspector.Visible = false;
        labletxtIntervalNPInspector.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("DisplayInspectionType", ex); }
    }

    private bool CheckInspectionType()
    {
        bool flg = true;
        try
        {
        if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164)
        {
            string InspectionType = GetSelectedCheckedValue(ddlInspectionType);

            if (InspectionType != null)
            {
                string[] str = InspectionType.Split(',');

                foreach (string insVal in str)
                {
                    //MSC_8300001775 Start Comment 
                    //if (insVal == "09      ")
                    //{
                    //    if (txtIntervalNPInspector.Text == "")
                    //        flg = false;
                    //    break;
                    //}
                    //MSC_8300001775 End Comment
                    //MSC_8300001775 Start
                    if (insVal == "09")
                    {
                        if (txtIntervalNPInspector.Text == "")
                            flg = false;
                        break;
                    }
                    //MSC_8300001775 End

                }
            }
            else if (InspectionType == null)
            {
                if (txtIntervalNPInspector.Text != "")
                    flg = false;
            }
        }

        }
        catch (Exception ex)
        { _log.Error("CheckInspectionType", ex); }
        return flg;
    }

    #endregion





    
}