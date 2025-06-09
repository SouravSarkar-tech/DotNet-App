using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using log4net;
//using SectionConfiguration;

//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_Purchasing : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    PurchasingAccess ObjPurchasingAccess = new PurchasingAccess();
    HelperAccess helperAccess = new HelperAccess();
    Purchasing objSavedPurchasing = new Purchasing();
    List<string> DepotPlantsCWH = new List<string> { "32", "83", "85", "86", "87", "140" };

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
                        if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                        {
                            btnSave.Visible = true;
                        }
                        //MSC_8300001775 End 
                    }
                    ClearPurchasingData();
                    //FillPurchasingData();
                    //BindGrid();
                    ConfigureControl();

                    //To manage the Creation Single request
                    FillFormDataByMHId();
                    lnlAddDetails.Visible = false;
                    gvData.Visible = false;

                    //PROSOL_SDT16092019
                    ProsolValidCheck();
                    //PROSOL_SDT16092019

                    //MSC_8300001775 Start
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                    {
                        ConfigureControlForSChange();
                        regtxtNManufacturer.Visible=false;
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
        lableddlPurchasingGroup.Visible = false;
        reqddlPurchasingGroup.Visible = false;
        lableddlPurchasingValueKey.Visible = false;
        reqddlPurchasingValueKey.Visible = false;
        reqtxtNManufacturer.Visible = false;
        labletxtNManufacturer.Visible = false;
        labletxtNameOfManufacturer.Visible = false;
        reqtxtNameOfManufacturer.Visible = false;
        reqtxtMPartNumber.Visible = false;
        labletxtMPartNumber.Visible = false;
        lableddlMPartProfile.Visible = false;
        reqddlMPartProfile.Visible = false;
        labletxtPurchaseOrderText.Visible = false;
        reqtxtPurchaseOrderText.Visible = false;

        txtPurchaseOrderText.Enabled = true;
        txtPurchaseOrderText.CssClass = "textbox";
        txtPurchaseOrderText.ReadOnly = false;
        chkBatchManagReqIndicator.Enabled = true;
        chkIndicatorAutomatic.Enabled = true;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    private void ProsolValidCheck()
    {
        try
        {
            if (Convert.ToString(lblModuleId.Text) == "138")
            {
                txtPurchaseOrderText.Enabled = false;
                txtPurchaseOrderText.ReadOnly = true;
            }
        }
        catch (Exception ex)
        { _log.Error("ProsolValidCheck", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (Save())
        {

            if ((lblModuleId.Text == "162" || lblModuleId.Text == "164" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                Response.Redirect("Sales2.aspx");
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
            Response.Redirect("Purchasing.aspx");
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
        if (Save())
        {
                //8400000410 comment start
                //if ((lblModuleId.Text == "162" || lblModuleId.Text == "164" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
                //        Response.Redirect("MRP1.aspx");
                //    if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                //        Response.Redirect("ForeignTrade.aspx");
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

    protected bool Save()
    {
        bool flg = false;
        try
        {
            Purchasing ObjPurchasing = GetControlsValue();
            objSavedPurchasing = GetPurchasingData();

            if (CheckPurchaseUnitWithBasicData1())
            {
                if (CheckValidPurchasingUnit())
                {
                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                    {
                        if (objSavedPurchasing.Mat_Purchasing_Id > 0)
                        {
                            CheckIfChanges(ObjPurchasing, objSavedPurchasing);
                        }
                    }

                    if (ObjPurchasingAccess.Save(ObjPurchasing) > 0)
                    {
                        //MSC_8300001775
                        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                        {
                            CheckIfChangesLog(ObjPurchasing, objSavedPurchasing);
                        }
                        //MSC_8300001775
                        ClearPurchasingData();
                        //FillPurchasingData();
                        //SectionConfiguration.FieldStatus.ClearPanel(pnlData);
                        //BindGrid();
                        //
                        flg = true;
                        ////MSC_8300001775
                        //if (HelperAccess.ReqType == "SMC")
                        //{
                        //    CheckIfChangesLog(ObjPurchasing, objSavedPurchasing);
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
                    lblMsg.Text = "Please maintain conversion factor according to Purchasing Unit. <a href = 'BasicData2.aspx'> Click here to update </a>";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "The purchase unit of measure cannot be same as base unit of measure";
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

    private bool CheckPurchaseUnitWithBasicData1()
    {
        bool flag = true;
        //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
        //{
        //    if (ddlPurchaseOrder.SelectedValue != "")
        //    {
        //        flag = ObjPurchasingAccess.CheckValidOrderUnit(lblMasterHeaderId.Text, ddlPurchaseOrder.SelectedValue);
        //    }
        //}
        //MSC_8300001775
        try
        {
        if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
        {
            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
            {
                if (ddlPurchaseOrder.SelectedValue != "")
                {
                    flag = ObjPurchasingAccess.CheckValidOrderUnit(lblMasterHeaderId.Text, ddlPurchaseOrder.SelectedValue);
                }
            }
            }
        }
        catch (Exception ex)
        { _log.Error("CheckPurchaseUnitWithBasicData1", ex); }
        //MSC_8300001775
        return flag;
    }

    private bool CheckValidPurchasingUnit()
    {
        bool flag = true;
        try
        {
        //throw new NotImplementedException();
        //if (ddlPurchaseOrder.SelectedValue != "")
        //{
        //    flag = ObjPurchasingAccess.CheckValidPurchasingUnit(lblMasterHeaderId.Text, ddlPurchaseOrder.SelectedValue);
        //}
        //MSC_8300001775
        if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
        {
            if (ddlPurchaseOrder.SelectedValue != "")
            {
                flag = ObjPurchasingAccess.CheckValidPurchasingUnit(lblMasterHeaderId.Text, ddlPurchaseOrder.SelectedValue);
            }
        }
            //MSC_8300001775

        }
        catch (Exception ex)
        { _log.Error("CheckValidPurchasingUnit", ex); }
        return flag;
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        lblPurchasingId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_Purchasing_Id"].ToString();
        ClearPurchasingData();
        FillPurchasingData();
        }
        catch (Exception ex)
        { _log.Error("lnkView_Click", ex); }
    }

    protected void lnlAddDetails_Click(object s, EventArgs e)
    {
        try
        {
        ClearPurchasingData();
        FillPurchasingData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    private void BindGrid()
    {
        try
        {
        DataSet ds;
        ds = ObjPurchasingAccess.GetPurchasingData(Convert.ToInt32(lblMasterHeaderId.Text));

        gvData.DataSource = ds;
        gvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("BindGrid", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlMaterialFreightG, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialFreightG'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlCrossPlantMStatus, "pr_GetDropDownListByControlNameModuleType 'M','ddlCrossPlantMStatus'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPurchaseOrder, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPurchasingValueKey, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingValueKey'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlQuotaArrangement, "pr_GetDropDownListByControlNameModuleType 'M','ddlQuotaArrangement'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlVariablePurchase, "pr_GetDropDownListByControlNameModuleType 'M','ddlVariablePurchase'", "LookUp_Desc", "LookUp_Code");

        helperAccess.PopuplateDropDownList(ddlMPartProfile, "pr_GetDropDownListByControlNameModuleType 'M','ddlMPartProfile'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private Purchasing GetPurchasingData()
    {
        return ObjPurchasingAccess.GetPurchasing(Convert.ToInt32(lblPurchasingId.Text));
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjPurchasingAccess.GetPurchasingData(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblPurchasingId.Text = ds.Tables[0].Rows[0]["Mat_Purchasing_Id"].ToString();
        }
        FillPurchasingData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillPurchasingData()
    {
        try
        {
            Purchasing ObjPurchasing = GetPurchasingData();
            if (ObjPurchasing.Mat_Purchasing_Id > 0)
            {
                lblPurchasingId.Text = ObjPurchasing.Mat_Purchasing_Id.ToString();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjPurchasing.Plant_Id;
                BindPlantWiseDropDown();

                chkIndicatorAutomatic.Checked = ObjPurchasing.Indicator_Auto_PO_Allowed.ToLower() == "true" ? true : false;
                chkIndicatorCritical.Checked = ObjPurchasing.Indicator_Critical_Part.ToLower() == "true" ? true : false;
                chkIndicatorSource.Checked = ObjPurchasing.Ind_Source_List_Req.ToLower() == "true" ? true : false;
                chkIndicatorUnlimited.Checked = ObjPurchasing.Ind_Unlimited_OverDelivery_Allowed.ToLower() == "true" ? true : false;
                chkMQualifiesDiscount.Checked = ObjPurchasing.Mat_Qualifies_Disc.ToLower() == "true" ? true : false;
                chkPostInspectionStock.Checked = ObjPurchasing.Post_Inspection_Stock.ToLower() == "true" ? true : false;
                chkBatchManagReqIndicator.Checked = ObjPurchasing.Batch_Mgmt_Req_Indicator.ToLower() == "true" ? true : false;
                ddlCrossPlantMStatus.SelectedValue = ObjPurchasing.Cross_Plant_Mat_Status;
                txtFromDateGenMStatus.Text = ObjPurchasing.Gen_Mat_Status_Sale_From;
                txtFromDateMStatus.Text = ObjPurchasing.Mat_Status_Purchasing_From;
                ddlMaterialFreightG.SelectedValue = ObjPurchasing.Mat_Freight_Grp;
                txtMPartNumber.Text = ObjPurchasing.Manufacturer_Part_No;
                ddlMPartProfile.SelectedValue = ObjPurchasing.Manufacturer_Part_Profile;
                txtNManufacturer.Text = ObjPurchasing.No_Of_Manufacturer;
                txtNameOfManufacturer.Text = ObjPurchasing.Name_Of_Manufacturer;
                txtPlantSpecificMStatus.Text = ObjPurchasing.Plant_Specific_Mat_Status;
                txtProcessingTime.Text = ObjPurchasing.Processing_Time_Goods_Receipt_Days.ToString();
                ddlPurchaseOrder.SelectedValue = ObjPurchasing.Pur_Order_Unit_Measure;
                ddlPurchasingGroup.SelectedValue = ObjPurchasing.Purchasing_Group;
                ddlPurchasingValueKey.SelectedValue = ObjPurchasing.Purchasing_Value_Key;
                ddlQuotaArrangement.SelectedValue = ObjPurchasing.Quota_Arrangement_Usage;
                ddlTaxIndicatorMPurchasing.SelectedValue = ObjPurchasing.Tax_Indicator_Material;
                txtToleranceLimit.Text = ObjPurchasing.Tolerance_Limit_OverDelivery;
                txtToleranceLimiteUnderdelivery.Text = ObjPurchasing.Tolerance_Limit_UnderDelivery;
                ddlVariablePurchase.SelectedValue = ObjPurchasing.Variable_Pur_Ord_Unit_Active;
                txtGRProcessingTime.Text = ObjPurchasing.GR_Processing_Time;
                txtPurchaseOrderText.Text = ObjPurchasing.Purchase_Order_Text;

            }
            else
            {
                //ClearPurchasingData();
                lblPurchasingId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','P1','" + lblPurchasingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();

                txtGRProcessingTime.Text = "02";

                if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
                {
                    chkBatchManagReqIndicator.Checked = true;
                    if (Session[StaticKeys.MatPurchasingGroupId].ToString() != "")
                        ddlPurchasingGroup.SelectedValue = Session[StaticKeys.MatPurchasingGroupId].ToString();
                }

                if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) && (SafeTypeHandling.ConvertToString(Session[StaticKeys.MatPurchasingGroupId]) == "H02"))
                {

                    //MSC_8300001775 Start Comment 
                    //ddlPurchasingValueKey.SelectedValue = "Z2  ";
                    //MSC_8300001775 End Comment
                    //MSC_8300001775 Start
                    ddlPurchasingValueKey.SelectedValue = "Z2";
                    //MSC_8300001775 End
                }
                else
                {

                    //MSC_8300001775 Start Comment 
                    //ddlPurchasingValueKey.SelectedValue = "Z1  ";
                    //MSC_8300001775 End Comment
                    //MSC_8300001775 Start
                    ddlPurchasingValueKey.SelectedValue = "Z1";
                    //MSC_8300001775 End
                }
                //Promotion code start
                //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                {
                    chkBatchManagReqIndicator.Checked = true;
                    txtGRProcessingTime.Text = "";
                }
                //GST Changes
                ddlTaxIndicatorMPurchasing.SelectedValue = "0";
                //GST Changes
            }
            ddlPlant.Enabled = false;
            if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
            {
                chkBatchManagReqIndicator.Enabled = false;
                if (Session[StaticKeys.MatPurchasingGroupId].ToString() != "")
                    ddlPurchasingGroup.Enabled = false;
            }
            if (lblModuleId.Text == "145" && DepotPlantsCWH.Contains(ddlPlant.SelectedValue.ToString()))
                chkIndicatorSource.Checked = true;
        }
        catch (Exception ex)
        {
            _log.Error("FillPurchasingData", ex);
        }
    }

    private void ClearPurchasingData()
    {
        try
        {
        lblPurchasingId.Text = "0";
        chkIndicatorAutomatic.Checked = false;
        chkIndicatorCritical.Checked = false;
        chkIndicatorSource.Checked = false;
        chkIndicatorUnlimited.Checked = false;
        chkMQualifiesDiscount.Checked = false;
        chkPostInspectionStock.Checked = false;
        chkBatchManagReqIndicator.Checked = false;
        txtFromDateGenMStatus.Text = "";
        txtFromDateMStatus.Text = "";
        txtMPartNumber.Text = "";
        txtNManufacturer.Text = "";
        txtPlantSpecificMStatus.Text = "";
        txtProcessingTime.Text = "";

        txtToleranceLimit.Text = "";
        txtToleranceLimiteUnderdelivery.Text = "";
        txtGRProcessingTime.Text = "";
        txtPurchaseOrderText.Text = "";
        //GST Changes
        ddlTaxIndicatorMPurchasing.SelectedValue = "";
        //GST Changes

        PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearPurchasingData", ex); }
    }

    private Purchasing GetControlsValue()
    {
        Purchasing ObjPurchasing = new Purchasing();
        Utility objUtil = new Utility();

        try
        {
            ObjPurchasing.Mat_Purchasing_Id = Convert.ToInt32(lblPurchasingId.Text);
            ObjPurchasing.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjPurchasing.Plant_Id = ddlPlant.SelectedValue;

            ObjPurchasing.Indicator_Auto_PO_Allowed = chkIndicatorAutomatic.Checked == true ? "1" : "0";
            ObjPurchasing.Indicator_Critical_Part = chkIndicatorCritical.Checked == true ? "1" : "0";
            ObjPurchasing.Ind_Source_List_Req = chkIndicatorSource.Checked == true ? "1" : "0";
            ObjPurchasing.Ind_Unlimited_OverDelivery_Allowed = chkIndicatorUnlimited.Checked == true ? "1" : "0";
            ObjPurchasing.Mat_Qualifies_Disc = chkMQualifiesDiscount.Checked == true ? "1" : "0";
            ObjPurchasing.Post_Inspection_Stock = chkPostInspectionStock.Checked == true ? "1" : "0";
            ObjPurchasing.Batch_Mgmt_Req_Indicator = chkBatchManagReqIndicator.Checked == true ? "1" : "0";

            ObjPurchasing.Cross_Plant_Mat_Status = ddlCrossPlantMStatus.SelectedValue;
            ObjPurchasing.Gen_Mat_Status_Sale_From = objUtil.GetYYYYMMDD(txtFromDateGenMStatus.Text);
            ObjPurchasing.Mat_Status_Purchasing_From = objUtil.GetYYYYMMDD(txtFromDateMStatus.Text);
            ObjPurchasing.Mat_Freight_Grp = ddlMaterialFreightG.SelectedValue;
            ObjPurchasing.Manufacturer_Part_No = txtMPartNumber.Text;
            ObjPurchasing.Manufacturer_Part_Profile = ddlMPartProfile.SelectedValue;
            ObjPurchasing.No_Of_Manufacturer = txtNManufacturer.Text;
            ObjPurchasing.Name_Of_Manufacturer = txtNameOfManufacturer.Text;
            ObjPurchasing.Plant_Specific_Mat_Status = txtPlantSpecificMStatus.Text;
            ObjPurchasing.Processing_Time_Goods_Receipt_Days = txtProcessingTime.Text;
            ObjPurchasing.Pur_Order_Unit_Measure = ddlPurchaseOrder.SelectedValue;
            ObjPurchasing.Purchasing_Group = ddlPurchasingGroup.SelectedValue;
            ObjPurchasing.Purchasing_Value_Key = ddlPurchasingValueKey.SelectedValue;
            ObjPurchasing.Quota_Arrangement_Usage = ddlQuotaArrangement.SelectedValue;
            ObjPurchasing.Tax_Indicator_Material = ddlTaxIndicatorMPurchasing.SelectedValue;
            ObjPurchasing.Tolerance_Limit_OverDelivery = txtToleranceLimit.Text;
            ObjPurchasing.Tolerance_Limit_UnderDelivery = txtToleranceLimiteUnderdelivery.Text;
            ObjPurchasing.Variable_Pur_Ord_Unit_Active = ddlVariablePurchase.SelectedValue;
            ObjPurchasing.GR_Processing_Time = txtGRProcessingTime.Text;
            ObjPurchasing.Purchase_Order_Text = txtPurchaseOrderText.Text;

            ObjPurchasing.IsActive = "1";
            ObjPurchasing.UserId = lblUserId.Text;
            ObjPurchasing.TodayDate = objUtil.GetDate();
            ObjPurchasing.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjPurchasing;
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Purchasing obj = new SectionConfiguration.Purchasing();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Purchasing NewPurchasingData, Purchasing oldPurchasingData)
    {
        try
        {
            if (NewPurchasingData.Mat_Purchasing_Id > 0 && oldPurchasingData.Mat_Purchasing_Id > 0)
            {
                if (NewPurchasingData.Plant_Id != oldPurchasingData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant ID</td> <td>" + oldPurchasingData.Plant_Id + "</td><td>" + NewPurchasingData.Plant_Id + "</td></tr>";
                if (NewPurchasingData.Plant_Specific_Mat_Status != oldPurchasingData.Plant_Specific_Mat_Status)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Specific Mat Status</td> <td>" + oldPurchasingData.Plant_Specific_Mat_Status + "</td><td>" + NewPurchasingData.Plant_Specific_Mat_Status + "</td></tr>";
                if (NewPurchasingData.Pur_Order_Unit_Measure != oldPurchasingData.Pur_Order_Unit_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Pur Order Unit Measure</td> <td>" + oldPurchasingData.Pur_Order_Unit_Measure + "</td><td>" + NewPurchasingData.Pur_Order_Unit_Measure + "</td></tr>";
                if (NewPurchasingData.Purchasing_Value_Key != oldPurchasingData.Purchasing_Value_Key)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Purchasing Value Key</td> <td>" + oldPurchasingData.Purchasing_Value_Key + "</td><td>" + NewPurchasingData.Purchasing_Value_Key + "</td></tr>";
                if (NewPurchasingData.Purchasing_Group != oldPurchasingData.Purchasing_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Purchasing Group</td> <td>" + oldPurchasingData.Purchasing_Group + "</td><td>" + NewPurchasingData.Purchasing_Group + "</td></tr>";
                if (NewPurchasingData.Batch_Mgmt_Req_Indicator != (oldPurchasingData.Batch_Mgmt_Req_Indicator.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Batch Mgmt Req Indicator</td> <td>" + oldPurchasingData.Batch_Mgmt_Req_Indicator + "</td><td>" + NewPurchasingData.Batch_Mgmt_Req_Indicator + "</td></tr>";
                if (NewPurchasingData.Processing_Time_Goods_Receipt_Days != oldPurchasingData.Processing_Time_Goods_Receipt_Days)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Processing Time Goods Receipt Days</td> <td>" + oldPurchasingData.Processing_Time_Goods_Receipt_Days + "</td><td>" + NewPurchasingData.Processing_Time_Goods_Receipt_Days + "</td></tr>";
                if (NewPurchasingData.Quota_Arrangement_Usage != oldPurchasingData.Quota_Arrangement_Usage)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Quota Arrangement Usage</td> <td>" + oldPurchasingData.Quota_Arrangement_Usage + "</td><td>" + NewPurchasingData.Quota_Arrangement_Usage + "</td></tr>";
                if (NewPurchasingData.Indicator_Critical_Part != (oldPurchasingData.Indicator_Critical_Part.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indicator Critical Part</td> <td>" + oldPurchasingData.Indicator_Critical_Part + "</td><td>" + NewPurchasingData.Indicator_Critical_Part + "</td></tr>";
                if (NewPurchasingData.Post_Inspection_Stock != (oldPurchasingData.Post_Inspection_Stock.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Post Inspection Stock</td> <td>" + oldPurchasingData.Post_Inspection_Stock + "</td><td>" + NewPurchasingData.Post_Inspection_Stock + "</td></tr>";
                if (NewPurchasingData.Indicator_Auto_PO_Allowed != (oldPurchasingData.Indicator_Auto_PO_Allowed.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Indicator Auto PO Allowed</td> <td>" + oldPurchasingData.Indicator_Auto_PO_Allowed + "</td><td>" + NewPurchasingData.Indicator_Auto_PO_Allowed + "</td></tr>";
                if (NewPurchasingData.Ind_Source_List_Req != (oldPurchasingData.Ind_Source_List_Req.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Ind Source List Req</td> <td>" + oldPurchasingData.Ind_Source_List_Req + "</td><td>" + NewPurchasingData.Ind_Source_List_Req + "</td></tr>";
                if (NewPurchasingData.Variable_Pur_Ord_Unit_Active != oldPurchasingData.Variable_Pur_Ord_Unit_Active)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Variable Pur Ord Unit Active</td> <td>" + oldPurchasingData.Variable_Pur_Ord_Unit_Active + "</td><td>" + NewPurchasingData.Variable_Pur_Ord_Unit_Active + "</td></tr>";
                if (NewPurchasingData.Tolerance_Limit_OverDelivery != oldPurchasingData.Tolerance_Limit_OverDelivery)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tolerance Limit OverDelivery</td> <td>" + oldPurchasingData.Tolerance_Limit_OverDelivery + "</td><td>" + NewPurchasingData.Tolerance_Limit_OverDelivery + "</td></tr>";
                if (NewPurchasingData.Ind_Unlimited_OverDelivery_Allowed != (oldPurchasingData.Ind_Unlimited_OverDelivery_Allowed.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Ind_Unlimited OverDelivery Allowed</td> <td>" + oldPurchasingData.Ind_Unlimited_OverDelivery_Allowed + "</td><td>" + NewPurchasingData.Ind_Unlimited_OverDelivery_Allowed + "</td></tr>";
                if (NewPurchasingData.Tolerance_Limit_UnderDelivery != oldPurchasingData.Tolerance_Limit_UnderDelivery)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tolerance Limit UnderDelivery</td> <td>" + oldPurchasingData.Tolerance_Limit_UnderDelivery + "</td><td>" + NewPurchasingData.Tolerance_Limit_UnderDelivery + "</td></tr>";
                if (NewPurchasingData.Tax_Indicator_Material != oldPurchasingData.Tax_Indicator_Material)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Indicator Material</td> <td>" + oldPurchasingData.Tax_Indicator_Material + "</td><td>" + NewPurchasingData.Tax_Indicator_Material + "</td></tr>";
                if (NewPurchasingData.Mat_Freight_Grp != oldPurchasingData.Mat_Freight_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mat Freight Grp</td> <td>" + oldPurchasingData.Mat_Freight_Grp + "</td><td>" + NewPurchasingData.Mat_Freight_Grp + "</td></tr>";
                if (NewPurchasingData.No_Of_Manufacturer != oldPurchasingData.No_Of_Manufacturer)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>No Of Manufacturer</td> <td>" + oldPurchasingData.No_Of_Manufacturer + "</td><td>" + NewPurchasingData.No_Of_Manufacturer + "</td></tr>";
                if (NewPurchasingData.Name_Of_Manufacturer != oldPurchasingData.Name_Of_Manufacturer)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Name Of Manufacturer</td> <td>" + oldPurchasingData.Name_Of_Manufacturer + "</td><td>" + NewPurchasingData.Name_Of_Manufacturer + "</td></tr>";
                if (NewPurchasingData.Manufacturer_Part_No != oldPurchasingData.Manufacturer_Part_No)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Manufacturer Part No</td> <td>" + oldPurchasingData.Manufacturer_Part_No + "</td><td>" + NewPurchasingData.Manufacturer_Part_No + "</td></tr>";
                if (NewPurchasingData.Manufacturer_Part_Profile != oldPurchasingData.Manufacturer_Part_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Manufacturer Part Profile</td> <td>" + oldPurchasingData.Manufacturer_Part_Profile + "</td><td>" + NewPurchasingData.Manufacturer_Part_Profile + "</td></tr>";
                if (NewPurchasingData.Cross_Plant_Mat_Status != oldPurchasingData.Cross_Plant_Mat_Status)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Cross Plant Mat Status</td> <td>" + oldPurchasingData.Cross_Plant_Mat_Status + "</td><td>" + NewPurchasingData.Cross_Plant_Mat_Status + "</td></tr>";
                if (NewPurchasingData.Mat_Status_Purchasing_From != oldPurchasingData.Mat_Status_Purchasing_From)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mat Status Purchasing From</td> <td>" + oldPurchasingData.Mat_Status_Purchasing_From + "</td><td>" + NewPurchasingData.Mat_Status_Purchasing_From + "</td></tr>";
                if (NewPurchasingData.Gen_Mat_Status_Sale_From != oldPurchasingData.Gen_Mat_Status_Sale_From)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Gen Mat Status Sale From</td> <td>" + oldPurchasingData.Gen_Mat_Status_Sale_From + "</td><td>" + NewPurchasingData.Gen_Mat_Status_Sale_From + "</td></tr>";
                if (NewPurchasingData.Mat_Qualifies_Disc != (oldPurchasingData.Mat_Qualifies_Disc.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Matl Material Grp Pack Mtl</td> <td>" + oldPurchasingData.Mat_Qualifies_Disc + "</td><td>" + NewPurchasingData.Mat_Qualifies_Disc + "</td></tr>";
                if (NewPurchasingData.GR_Processing_Time != oldPurchasingData.GR_Processing_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>GR Processing Time</td> <td>" + oldPurchasingData.GR_Processing_Time + "</td><td>" + NewPurchasingData.GR_Processing_Time + "</td></tr>";
                if (NewPurchasingData.Purchase_Order_Text != oldPurchasingData.Purchase_Order_Text)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Purchase Order Text</td> <td>" + oldPurchasingData.Purchase_Order_Text + "</td><td>" + NewPurchasingData.Purchase_Order_Text + "</td></tr>";
                if (NewPurchasingData.Change_Ref_Id != oldPurchasingData.Change_Ref_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Change Ref Id</td> <td>" + oldPurchasingData.Change_Ref_Id + "</td><td>" + NewPurchasingData.Change_Ref_Id + "</td></tr>";
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
    private void CheckIfChangesLog(Purchasing NewPurchasingData, Purchasing oldPurchasingData)
    {
        Utility objUtil = new Utility();
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewPurchasingData.Mat_Purchasing_Id > 0 && oldPurchasingData.Mat_Purchasing_Id > 0)
            {
                if (NewPurchasingData.Plant_Specific_Mat_Status != oldPurchasingData.Plant_Specific_Mat_Status)
                    _items.Add(new SMChange { colFieldName = 112, colOldVal = oldPurchasingData.Plant_Specific_Mat_Status, colNewVal = NewPurchasingData.Plant_Specific_Mat_Status });
                if (NewPurchasingData.Pur_Order_Unit_Measure != oldPurchasingData.Pur_Order_Unit_Measure)
                    _items.Add(new SMChange { colFieldName = 113, colOldVal = oldPurchasingData.Pur_Order_Unit_Measure, colNewVal = NewPurchasingData.Pur_Order_Unit_Measure });
                if (NewPurchasingData.Purchasing_Value_Key != oldPurchasingData.Purchasing_Value_Key)
                    _items.Add(new SMChange { colFieldName = 114, colOldVal = oldPurchasingData.Purchasing_Value_Key, colNewVal = NewPurchasingData.Purchasing_Value_Key });
                if (NewPurchasingData.Purchasing_Group != oldPurchasingData.Purchasing_Group)
                    _items.Add(new SMChange { colFieldName = 115, colOldVal = oldPurchasingData.Purchasing_Group, colNewVal = NewPurchasingData.Purchasing_Group });
                if (NewPurchasingData.Processing_Time_Goods_Receipt_Days != oldPurchasingData.Processing_Time_Goods_Receipt_Days)
                    _items.Add(new SMChange { colFieldName = 117, colOldVal = oldPurchasingData.Processing_Time_Goods_Receipt_Days, colNewVal = NewPurchasingData.Processing_Time_Goods_Receipt_Days });
                if (NewPurchasingData.Quota_Arrangement_Usage != oldPurchasingData.Quota_Arrangement_Usage)
                    _items.Add(new SMChange { colFieldName = 118, colOldVal = oldPurchasingData.Quota_Arrangement_Usage, colNewVal = NewPurchasingData.Quota_Arrangement_Usage });

                if (NewPurchasingData.Variable_Pur_Ord_Unit_Active != oldPurchasingData.Variable_Pur_Ord_Unit_Active)
                    _items.Add(new SMChange { colFieldName = 123, colOldVal = oldPurchasingData.Variable_Pur_Ord_Unit_Active, colNewVal = NewPurchasingData.Variable_Pur_Ord_Unit_Active });
                if (NewPurchasingData.Tolerance_Limit_OverDelivery != oldPurchasingData.Tolerance_Limit_OverDelivery)
                    _items.Add(new SMChange { colFieldName = 124, colOldVal = oldPurchasingData.Tolerance_Limit_OverDelivery, colNewVal = NewPurchasingData.Tolerance_Limit_OverDelivery });
                 if (NewPurchasingData.Tolerance_Limit_UnderDelivery != oldPurchasingData.Tolerance_Limit_UnderDelivery)
                    _items.Add(new SMChange { colFieldName = 126, colOldVal = oldPurchasingData.Tolerance_Limit_UnderDelivery, colNewVal = NewPurchasingData.Tolerance_Limit_UnderDelivery });
                if (NewPurchasingData.Tax_Indicator_Material != oldPurchasingData.Tax_Indicator_Material)
                    _items.Add(new SMChange { colFieldName = 127, colOldVal = oldPurchasingData.Tax_Indicator_Material, colNewVal = NewPurchasingData.Tax_Indicator_Material });
                if (NewPurchasingData.Mat_Freight_Grp != oldPurchasingData.Mat_Freight_Grp)
                    _items.Add(new SMChange { colFieldName = 128, colOldVal = oldPurchasingData.Mat_Freight_Grp, colNewVal = NewPurchasingData.Mat_Freight_Grp });
                if (NewPurchasingData.No_Of_Manufacturer != oldPurchasingData.No_Of_Manufacturer)
                    _items.Add(new SMChange { colFieldName = 129, colOldVal = oldPurchasingData.No_Of_Manufacturer, colNewVal = NewPurchasingData.No_Of_Manufacturer });
                if (NewPurchasingData.Name_Of_Manufacturer != oldPurchasingData.Name_Of_Manufacturer)
                    _items.Add(new SMChange { colFieldName = 1285, colOldVal = oldPurchasingData.Name_Of_Manufacturer, colNewVal = NewPurchasingData.Name_Of_Manufacturer });
                if (NewPurchasingData.Manufacturer_Part_No != oldPurchasingData.Manufacturer_Part_No)
                    _items.Add(new SMChange { colFieldName = 130, colOldVal = oldPurchasingData.Manufacturer_Part_No, colNewVal = NewPurchasingData.Manufacturer_Part_No });
                if (NewPurchasingData.Manufacturer_Part_Profile != oldPurchasingData.Manufacturer_Part_Profile)
                    _items.Add(new SMChange { colFieldName = 131, colOldVal = oldPurchasingData.Manufacturer_Part_Profile, colNewVal = NewPurchasingData.Manufacturer_Part_Profile });
                if (NewPurchasingData.Cross_Plant_Mat_Status != oldPurchasingData.Cross_Plant_Mat_Status)
                    _items.Add(new SMChange { colFieldName = 132, colOldVal = oldPurchasingData.Cross_Plant_Mat_Status, colNewVal = NewPurchasingData.Cross_Plant_Mat_Status });


                if (NewPurchasingData.Ind_Unlimited_OverDelivery_Allowed != (oldPurchasingData.Ind_Unlimited_OverDelivery_Allowed.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 125, colOldVal = (oldPurchasingData.Ind_Unlimited_OverDelivery_Allowed.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Ind_Unlimited_OverDelivery_Allowed.ToLower() == "1" ? "X" : "") });


                if (NewPurchasingData.Batch_Mgmt_Req_Indicator != (oldPurchasingData.Batch_Mgmt_Req_Indicator.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 116, colOldVal = (oldPurchasingData.Batch_Mgmt_Req_Indicator.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Batch_Mgmt_Req_Indicator.ToLower() == "1" ? "X" : "") });


                if (NewPurchasingData.Indicator_Critical_Part != (oldPurchasingData.Indicator_Critical_Part.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 119, colOldVal = (oldPurchasingData.Indicator_Critical_Part.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Indicator_Critical_Part.ToLower() == "1" ? "X" : "") });
                if (NewPurchasingData.Post_Inspection_Stock != (oldPurchasingData.Post_Inspection_Stock.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 120, colOldVal = (oldPurchasingData.Post_Inspection_Stock.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Post_Inspection_Stock.ToLower() == "1" ? "X" : "") });
                if (NewPurchasingData.Indicator_Auto_PO_Allowed != (oldPurchasingData.Indicator_Auto_PO_Allowed.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 121, colOldVal = (oldPurchasingData.Indicator_Auto_PO_Allowed.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Indicator_Auto_PO_Allowed.ToLower() == "1" ? "X" : "") });
                if (NewPurchasingData.Ind_Source_List_Req != (oldPurchasingData.Ind_Source_List_Req.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 122, colOldVal = (oldPurchasingData.Ind_Source_List_Req.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Ind_Source_List_Req.ToLower() == "1" ? "X" : "") });

                //if (NewPurchasingData.Mat_Status_Purchasing_From != oldPurchasingData.Mat_Status_Purchasing_From)
                if ((objUtil.GetDDMMYYYYNew(NewPurchasingData.Mat_Status_Purchasing_From) != oldPurchasingData.Mat_Status_Purchasing_From)
                    && (objUtil.GetDDMMYYYYNew(NewPurchasingData.Mat_Status_Purchasing_From) != "01/01/1900")
                    && (NewPurchasingData.Mat_Status_Purchasing_From != "1900-01-01")
                        && (oldPurchasingData.Mat_Status_Purchasing_From != "01/01/1900") && (oldPurchasingData.Mat_Status_Purchasing_From != "1900-01-01")
                        )
                {
                    _items.Add(new SMChange { colFieldName = 133, colOldVal = oldPurchasingData.Mat_Status_Purchasing_From, colNewVal = objUtil.GetDDMMYYYYNew(NewPurchasingData.Mat_Status_Purchasing_From) });

                }
                //if (NewPurchasingData.Gen_Mat_Status_Sale_From != oldPurchasingData.Gen_Mat_Status_Sale_From),,,,,,,,,,,,,,,,,
                if ((objUtil.GetDDMMYYYYNew(NewPurchasingData.Gen_Mat_Status_Sale_From) != oldPurchasingData.Gen_Mat_Status_Sale_From)
                    && (objUtil.GetDDMMYYYYNew(NewPurchasingData.Gen_Mat_Status_Sale_From) != "01/01/1900")
                    && (NewPurchasingData.Gen_Mat_Status_Sale_From != "1900-01-01")
                        && (oldPurchasingData.Gen_Mat_Status_Sale_From != "01/01/1900") && (oldPurchasingData.Gen_Mat_Status_Sale_From != "1900-01-01")
                        )
                {
                    _items.Add(new SMChange { colFieldName = 134, colOldVal = oldPurchasingData.Gen_Mat_Status_Sale_From, colNewVal = objUtil.GetDDMMYYYYNew(NewPurchasingData.Gen_Mat_Status_Sale_From) });
                }
                if (NewPurchasingData.Mat_Qualifies_Disc != (oldPurchasingData.Mat_Qualifies_Disc.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 135, colOldVal = (oldPurchasingData.Mat_Qualifies_Disc.ToLower() == "true" ? "X" : ""), colNewVal = (NewPurchasingData.Mat_Qualifies_Disc.ToLower() == "1" ? "X" : "") });
                if (NewPurchasingData.GR_Processing_Time != oldPurchasingData.GR_Processing_Time)
                    _items.Add(new SMChange { colFieldName = 1181, colOldVal = oldPurchasingData.GR_Processing_Time, colNewVal = NewPurchasingData.GR_Processing_Time });
                if (NewPurchasingData.Purchase_Order_Text != oldPurchasingData.Purchase_Order_Text)
                    _items.Add(new SMChange { colFieldName = 1182, colOldVal = oldPurchasingData.Purchase_Order_Text, colNewVal = NewPurchasingData.Purchase_Order_Text });
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
                ChangeSMatID1 = helperAccess.MaterialChange("12", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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

}