using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;

public partial class Transaction_Material_MaterialExtension : BasePage
{
    MaterialExtensionAccess ObjMaterialExtensionAccess = new MaterialExtensionAccess();
    HelperAccess helperAccess = new HelperAccess();

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
                    trButton.Visible = true;
                    btnSave.Visible = true;
                    btnNext.Visible = true;
                }
                else
                {
                    grvData.Columns[6].Visible = false;
                    lnlAddDetails.Visible = false;
                    pnlData.Visible = false;
                }

                ClearData();
                FillFormDataByMHId();

            }
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);

        // your code
        Session["ExtensionMain"] = null;
        Session["ExtensionNew"] = null;
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            string pageURL = btnPrevious.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            //Response.Redirect("MaterialExtension.aspx");
        }
    }

    //protected void btnSaveAll_Click(object sender, EventArgs e)
    //{
    //    if (Save())
    //    {
    //        lblMsg.Text = Messages.GetMessage(1);
    //        pnlMsg.CssClass = "success";
    //        pnlMsg.Visible = true;

    //        Response.Redirect("MaterialExtension.aspx");
    //    }
    //}

    protected void btnNext_Click(object sender, EventArgs e)
    {
        //if (Save())
        //{
        Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
        Response.Redirect("MaterialMaster.aspx");
        //string pageURL = btnNext.CommandArgument.ToString();
        //Response.Redirect(pageURL);
        //}
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPlantWiseDropDown();
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSalesOrgWiseDropDown();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        string Material_Extension_Id = grvData.DataKeys[grdrow.RowIndex]["Material_Extension_Id"].ToString();

        if (ObjMaterialExtensionAccess.DeleteMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(Material_Extension_Id)) > 0)
        {
            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }

        FillFormDataByMHId();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMatExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Material_Extension_Id"].ToString();
            FillControlData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        //ClearData();

        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        lblMatExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Material_Extension_Id"].ToString();
        FillControlData();

        txtMaterialCode.Enabled = true;
        ddlPlant.Enabled = true;

        lblMatExtensionId.Text = "";
        txtMaterialCode.Text = "";
        txtMaterialDescription.Text = "";
    }

    protected void ddlMrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MRPTypeWiseSetup();
    }

    protected void ddlLotSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LotSizeWiseValidation();
    }

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void txtMaterialCode_TextChanged(object sender, EventArgs e)
    {
        MaterialTypeSelection();
    }

    protected void lnkRefreshddlInspectionType_Click(object sender, EventArgs e)
    {
        DisplayInspectionType();
    }

    protected void ddlInspectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayInspectionType();
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

    }
    #endregion

    #region Public Method

    protected void ClearData()
    {
        lblMatExtensionId.Text = "0";

        txtMaterialCode.Text = "";
        txtMaterialDescription.Text = "";

        txtMaterialCode.Enabled = true;
        ddlPlant.Enabled = true;

        txtReorder.Text = "";
        txtMinLotSize.Text = "";
        txtMaxLotSize.Text = "";
        txtFixedLotSize.Text = "";
        txtRoundingValue.Text = "";
        txtGRProcessingTime.Text = "";
        txtPlannedDeleveryTime.Text = "";
        txtCapacityUsage.Text = "";
        txtLoadingEquipQuantity.Text = "";
        txtLoadingEquipQuantity2.Text = "";
        txtloadingEquipQuantity3.Text = "";

        ClearSelectedValue(ddlInspectionType);

        PopuplateDropDownList();
        ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

        BindPlantWiseDropDown();
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlMaterialAccGrp, "pr_GetModuleListByModuleType 'M'", "Module_Name", "Module_Id", "");

        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','All','" + lblMatExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMatExtensionId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");
        BindPlantWiseDropDown();

        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlAccountAssignment, "pr_GetDropDownListByControlNameModuleType 'M','ddlAccountAssignment'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMaterialPGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialPGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlPriceControlIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlPriceControlIndicator'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlLotSize, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");


        helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownCheckBox(ddlInspectionType, "pr_GetDropDownListByControlNameModuleType 'M','ddlInspectionType'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownList(ddlWarehouse, "pr_GetDropDownListByControlNameModuleType 'M','ddlWarehouse'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlWareHouseMangUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip2,"pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip3, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");

    }

    private void FillFormDataByMHId()
    {
        DataSet dataSet = ObjMaterialExtensionAccess.GetMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));

        grvData.DataSource = dataSet;
        grvData.DataBind();

        FillControlData();
    }

    private void BindPlantWiseDropDown()
    {

        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','8','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlIssueStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','13','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    }

    private void BindSalesOrgWiseDropDown()
    {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
    }

    private void MRPTypeWiseSetup()
    {
        if (ddlMrpType.SelectedValue == "ND")
        {
            reqddlMrpController.Visible = false;
            reqddlLotSize.Visible = false;
            reqtxtReorder.Visible = false;


            ddlMrpController.SelectedValue = "";
            ddlLotSize.SelectedValue = "";

            lableddlMrpController.Visible = false;
            lableddlLotSize.Visible = false;
            labletxtReorder.Visible = false;

            LotSizeWiseValidation();

            reqtxtFixedLotSize.Visible = false;
            reqtxtMinLotSize.Visible = false;
            reqtxtMaxLotSize.Visible = false;
            reqtxtRoundingValue.Visible = false;

            ddlMrpController.Enabled = false;
            ddlLotSize.Enabled = false;
            txtReorder.Enabled = false;
            txtFixedLotSize.Enabled = false;
            txtMinLotSize.Enabled = false;
            txtMaxLotSize.Enabled = false;
            txtRoundingValue.Enabled = false;



        }
        else if (ddlMrpType.SelectedValue == "PD")
        {
            reqddlMrpController.Visible = true;
            reqddlLotSize.Visible = true;
            reqtxtReorder.Visible = false;

            lableddlMrpController.Visible = true;
            lableddlLotSize.Visible = true;
            labletxtReorder.Visible = false;

            reqtxtFixedLotSize.Visible = true;
            reqtxtMinLotSize.Visible = true;
            reqtxtMaxLotSize.Visible = true;
            reqtxtRoundingValue.Visible = true;

            ddlMrpController.Enabled = true;
            ddlLotSize.Enabled = true;
            txtReorder.Enabled = true;
            txtFixedLotSize.Enabled = true;
            txtMinLotSize.Enabled = true;
            txtMaxLotSize.Enabled = true;
            txtRoundingValue.Enabled = true;
        }
        else if (ddlMrpType.SelectedValue == "VB")
        {
            reqddlMrpController.Visible = true;
            reqddlLotSize.Visible = true;
            reqtxtReorder.Visible = true;

            reqtxtFixedLotSize.Visible = true;
            reqtxtMinLotSize.Visible = true;
            reqtxtMaxLotSize.Visible = true;
            reqtxtRoundingValue.Visible = true;

            ddlMrpController.Enabled = true;
            ddlLotSize.Enabled = true;
            txtReorder.Enabled = true;
            txtFixedLotSize.Enabled = true;
            txtMinLotSize.Enabled = true;
            txtMaxLotSize.Enabled = true;
            txtRoundingValue.Enabled = true;
            lableddlMrpController.Visible = true;
            lableddlLotSize.Visible = true;
            labletxtReorder.Visible = true;

            LotSizeWiseValidation();
        }
    }

    private void LotSizeWiseValidation()
    {
        if (ddlMrpType.SelectedValue != "ND")
        {
            if (ddlLotSize.SelectedValue == "FX")
            {
                reqtxtFixedLotSize.Visible = true;
                reqtxtMinLotSize.Visible = false;
                reqtxtMaxLotSize.Visible = false;
                reqtxtRoundingValue.Visible = false;

                labletxtFixedLotSize.Visible = true;
                labletxtMinLotSize.Visible = false;
                labletxtMaxLotSize.Visible = false;
                labletxtRoundingValue.Visible = false;

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
                reqtxtMinLotSize.Visible = true;
                reqtxtMaxLotSize.Visible = true;
                reqtxtRoundingValue.Visible = true;

                labletxtFixedLotSize.Visible = false;
                labletxtMinLotSize.Visible = true;
                labletxtMaxLotSize.Visible = true;
                labletxtRoundingValue.Visible = false;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;

                txtFixedLotSize.Text = "";
            }
            else
            {
                reqtxtFixedLotSize.Visible = false;
                reqtxtMinLotSize.Visible = true;
                reqtxtMaxLotSize.Visible = true;
                reqtxtRoundingValue.Visible = true;

                labletxtFixedLotSize.Visible = false;
                labletxtMinLotSize.Visible = true;
                labletxtMaxLotSize.Visible = true;
                labletxtRoundingValue.Visible = true;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;

                txtFixedLotSize.Text = "";
            }
        }
        else
        {
            reqtxtFixedLotSize.Visible = false;
            reqtxtMinLotSize.Visible = false;
            reqtxtMaxLotSize.Visible = false;
            reqtxtRoundingValue.Visible = false;

            labletxtFixedLotSize.Visible = false;
            labletxtMinLotSize.Visible = false;
            labletxtMaxLotSize.Visible = false;
            labletxtRoundingValue.Visible = false;

            txtFixedLotSize.Enabled = false;
            txtMinLotSize.Enabled = false;
            txtMaxLotSize.Enabled = false;
            txtRoundingValue.Enabled = false;

            txtFixedLotSize.Text = "";
            txtMinLotSize.Text = "";
            txtMaxLotSize.Text = "";
            txtRoundingValue.Text = "";
        }
    }

    private void MaterialTypeSelection()
    {
        //txtMaterialCode.Text = txtMaterialCode.Text.ToUpper();
        if (Session[StaticKeys.MaterialProcessModuleId] != null)
        {
            pnlMsg.Visible = false;
            if (Session[StaticKeys.MaterialProcessModuleId].ToString() != MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text))
            {
                lblMsg.Text = "Please enter only " + ddlMaterialAccGrp.SelectedItem.Text;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtMaterialCode.Text = "";
            }
        }
        else
        {
            ddlMaterialAccGrp.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text);
        }
        BindValuationClass();
        ddlValuationClass.SelectedValue = MaterialHelper.GetDefaultValuationClassByModuleId(ddlMaterialAccGrp.SelectedValue);

        ddlPriceControlIndicator.SelectedValue = "V";
        ddlPriceControlIndicator.Enabled = false;
        if (ddlValuationClass.SelectedValue != "")
            ddlValuationClass.Enabled = false;

        ConfigureControl();
    }

    private bool CheckMatValid(string MaterialNumber, string PlantID)
    {
        bool flg = true;

        foreach (GridViewRow grv in grvData.Rows)
        {
            Label lblMaterialNumber = (Label)grv.FindControl("lblMaterialNumber");
            Label lblPlantID = (Label)grv.FindControl("lblPlantID");
            Label lblStorageLocation = (Label)grv.FindControl("lblStorageLocation");
            if (lblMaterialNumber.Text == MaterialNumber && lblPlantID.Text == PlantID && lblStorageLocation.Text == ddlStorageLocation.SelectedValue)
            {
                flg = false;
                break;
            }
        }

        return flg;
    }

    private bool Save()
    {
        bool flg = false;
        try
        {
            MaterialExtension ObjMaterialExtension = GetControlsValue();

            if (ObjMaterialExtension.Material_Number != null)
            {
                if (CheckMatValid(ObjMaterialExtension.Material_Number, ObjMaterialExtension.Plant_Id) || ObjMaterialExtension.Material_Extension_Id > 0)
                {
                    if (ObjMaterialExtension.Inspection_Type != "" || !lableddlInspectionType.Visible)
                    {
                        if (ObjMaterialExtensionAccess.Save(ObjMaterialExtension) > 0)
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
                    lblMsg.Text = "Material/Plant/Stor. Loc. already exists, please enter another combination.";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Please enter Material Number to proceed.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private MaterialExtension GetControlsValue()
    {
        MaterialExtension ObjMaterialExtension = new MaterialExtension();
        Utility objUtil = new Utility();

        try
        {
            //Label lblMaterialExtensionId = (Label)grv.FindControl("lblMaterialExtensionId" + ext);

            ObjMaterialExtension.Material_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMatExtensionId.Text);
            ObjMaterialExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);

            ObjMaterialExtension.Material_Number = txtMaterialCode.Text;
            ObjMaterialExtension.Material_Type = ddlMaterialAccGrp.SelectedValue;
            ObjMaterialExtension.Material_Short_Description = txtMaterialDescription.Text;

            ObjMaterialExtension.Plant_Id = ddlPlant.SelectedValue;

            ObjMaterialExtension.Storage_Location = ddlStorageLocation.SelectedValue;
            ObjMaterialExtension.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjMaterialExtension.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;
            ObjMaterialExtension.Mat_Pricing_Group = ddlMaterialPGroup.SelectedValue;
            ObjMaterialExtension.Acc_Assignment_Grp = ddlAccountAssignment.SelectedValue;
            ObjMaterialExtension.Purchasing_Group = ddlPurchasingGroup.SelectedValue;
            ObjMaterialExtension.MRP_Type = ddlMrpType.SelectedValue;
            ObjMaterialExtension.MRP_Controller = ddlMrpController.SelectedValue;
            ObjMaterialExtension.Reorder_Point = txtReorder.Text;
            ObjMaterialExtension.Lot_Size = ddlLotSize.SelectedValue;
            ObjMaterialExtension.Min_Lot_Size = txtMinLotSize.Text;
            ObjMaterialExtension.Max_Lot_Size = txtMaxLotSize.Text;
            ObjMaterialExtension.Fixed_Lot_Size = txtFixedLotSize.Text;
            ObjMaterialExtension.Rounding_Value = txtRoundingValue.Text;
            ObjMaterialExtension.Issue_Storage_Location = ddlIssueStorageLocation.SelectedValue;
            ObjMaterialExtension.GR_Processing_Time = txtGRProcessingTime.Text;
            ObjMaterialExtension.Planned_Delivery_Time_Days = txtPlannedDeleveryTime.Text;
            ObjMaterialExtension.Profit_Center = ddlProfitCenter.SelectedValue;
            ObjMaterialExtension.Valuation_Class = ddlValuationClass.SelectedValue;
            ObjMaterialExtension.Price_Ctrl_Indicator = ddlPriceControlIndicator.SelectedValue;

            ObjMaterialExtension.Spl_Procurement_Type = ddlSpecialProcType.SelectedValue;
            ObjMaterialExtension.Inspection_Type = GetSelectedCheckedValue(ddlInspectionType) == null ? "" : GetSelectedCheckedValue(ddlInspectionType);
            ObjMaterialExtension.Warehouse_ID = ddlWarehouse.SelectedValue;
            ObjMaterialExtension.Storage_Type_ID = ddlStorageType.SelectedValue;
            ObjMaterialExtension.Capacity_Usage = txtCapacityUsage.Text;
            ObjMaterialExtension.WM_Unit_Measure = ddlWareHouseMangUnit.SelectedValue;
            ObjMaterialExtension.Stor_Type_Ind_Stock_Removal = ddlStorageTypeIndiSPlaceRemoval.SelectedValue;
            ObjMaterialExtension.Storage_Section_Ind = ddlStorageSectIndi.SelectedValue;
            ObjMaterialExtension.Stor_Type_Ind_Stock_Placement = ddlStorageTypeIndiSPlacement.SelectedValue;

            ObjMaterialExtension.Loading_Equipment_Quantity = txtLoadingEquipQuantity.Text;
            ObjMaterialExtension.Loading_Equipment_Quantity1 = txtLoadingEquipQuantity2.Text;
            ObjMaterialExtension.Loading_Equipment_Quantity2 = txtloadingEquipQuantity3.Text; ;
            ObjMaterialExtension.Unit_Loading_Equip_Quan = ddlUnitMeasureLoadingEquip.SelectedValue;
            ObjMaterialExtension.Unit_Loading_Equip_Quan1 = ddlUnitMeasureLoadingEquip2.SelectedValue;
            ObjMaterialExtension.Unit_Loading_Equip_Quan2 = ddlUnitMeasureLoadingEquip3.SelectedValue;
            ObjMaterialExtension.Storage_Unit_Type = ddlStorageUnitType.SelectedValue;
            ObjMaterialExtension.Storage_Unit_Type1 = ddlStorageUnitType2.SelectedValue;
            ObjMaterialExtension.Storage_Unit_Type2 = ddlStorageUnitType3.SelectedValue;

            ObjMaterialExtension.IsActive = "1";
            ObjMaterialExtension.UserId = lblUserId.Text;
            ObjMaterialExtension.TodayDate = objUtil.GetDate();
            ObjMaterialExtension.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjMaterialExtension;
    }

    public void BindValuationClass()
    {
        string AccountCat = MaterialHelper.GetAccountCategoryByModuleId(ddlMaterialAccGrp.SelectedValue);
        helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass','1','" + AccountCat + "'", "LookUp_Desc", "LookUp_Code", "");
    }

    private void FillControlData()
    {
        Utility objUtil = new Utility();

        MaterialExtension ObjMaterialExtension = ObjMaterialExtensionAccess.GetMaterialExtension(Convert.ToInt32(lblMatExtensionId.Text));
        try
        {

            if (ObjMaterialExtension.Material_Extension_Id > 0)
            {
                txtMaterialCode.Text = ObjMaterialExtension.Material_Number;
                ddlMaterialAccGrp.SelectedValue = ObjMaterialExtension.Material_Type;
                txtMaterialDescription.Text = ObjMaterialExtension.Material_Short_Description;

                txtMaterialCode.Enabled = false;
                ddlPlant.Enabled = false;

                BindValuationClass();

                ddlPlant.SelectedValue = ObjMaterialExtension.Plant_Id;
                BindPlantWiseDropDown();

                ddlStorageLocation.SelectedValue = ObjMaterialExtension.Storage_Location;
                ddlSalesOrginization.SelectedValue = ObjMaterialExtension.Sales_Organization_Id;
                BindSalesOrgWiseDropDown();

                ddlDistributionChannel.SelectedValue = ObjMaterialExtension.Distribution_Channel_ID;
                ddlMaterialPGroup.SelectedValue = ObjMaterialExtension.Mat_Pricing_Group;
                ddlAccountAssignment.SelectedValue = ObjMaterialExtension.Acc_Assignment_Grp;
                ddlPurchasingGroup.SelectedValue = ObjMaterialExtension.Purchasing_Group;
                ddlMrpType.SelectedValue = ObjMaterialExtension.MRP_Type;
                MRPTypeWiseSetup();

                ddlMrpController.SelectedValue = ObjMaterialExtension.MRP_Controller;
                txtReorder.Text = ObjMaterialExtension.Reorder_Point;
                ddlLotSize.SelectedValue = ObjMaterialExtension.Lot_Size;
                LotSizeWiseValidation();

                txtMinLotSize.Text = ObjMaterialExtension.Min_Lot_Size;
                txtMaxLotSize.Text = ObjMaterialExtension.Max_Lot_Size;
                txtFixedLotSize.Text = ObjMaterialExtension.Fixed_Lot_Size;
                txtRoundingValue.Text = ObjMaterialExtension.Rounding_Value;
                ddlIssueStorageLocation.SelectedValue = ObjMaterialExtension.Issue_Storage_Location;
                txtGRProcessingTime.Text = ObjMaterialExtension.GR_Processing_Time;
                txtPlannedDeleveryTime.Text = ObjMaterialExtension.Planned_Delivery_Time_Days;
                ddlProfitCenter.SelectedValue = ObjMaterialExtension.Profit_Center;
                ddlValuationClass.SelectedValue = ObjMaterialExtension.Valuation_Class;
                ddlPriceControlIndicator.SelectedValue = ObjMaterialExtension.Price_Ctrl_Indicator;

                ddlSpecialProcType.SelectedValue = ObjMaterialExtension.Spl_Procurement_Type;
                SetSelectedValue(ddlInspectionType, ObjMaterialExtension.Inspection_Type);
                ddlWarehouse.SelectedValue = ObjMaterialExtension.Warehouse_ID;
                helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlStorageType.SelectedValue = ObjMaterialExtension.Storage_Type_ID;
                txtCapacityUsage.Text = ObjMaterialExtension.Capacity_Usage;
                ddlWareHouseMangUnit.SelectedValue = ObjMaterialExtension.WM_Unit_Measure;
                ddlStorageTypeIndiSPlaceRemoval.SelectedValue = ObjMaterialExtension.Stor_Type_Ind_Stock_Removal;
                ddlStorageSectIndi.SelectedValue = ObjMaterialExtension.Storage_Section_Ind;
                ddlStorageTypeIndiSPlacement.SelectedValue = ObjMaterialExtension.Stor_Type_Ind_Stock_Placement;

                txtLoadingEquipQuantity.Text = ObjMaterialExtension.Loading_Equipment_Quantity;
                txtLoadingEquipQuantity2.Text = ObjMaterialExtension.Loading_Equipment_Quantity1;
                txtloadingEquipQuantity3.Text = ObjMaterialExtension.Loading_Equipment_Quantity2;
                ddlUnitMeasureLoadingEquip.SelectedValue = ObjMaterialExtension.Unit_Loading_Equip_Quan;
                ddlUnitMeasureLoadingEquip2.SelectedValue = ObjMaterialExtension.Unit_Loading_Equip_Quan1;
                ddlUnitMeasureLoadingEquip3.SelectedValue = ObjMaterialExtension.Unit_Loading_Equip_Quan2;
                ddlStorageUnitType.SelectedValue = ObjMaterialExtension.Storage_Unit_Type;
                ddlStorageUnitType2.SelectedValue = ObjMaterialExtension.Storage_Unit_Type1;
                ddlStorageUnitType3.SelectedValue = ObjMaterialExtension.Storage_Unit_Type2;

                ConfigureControl();
            }
            else
            {
                if(Session[StaticKeys.MaterialProcessModuleId] != null)
                {
                    ddlMaterialAccGrp.SelectedValue = Session[StaticKeys.MaterialProcessModuleId].ToString();
                }
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                if (Session[StaticKeys.MatPurchasingGroupId].ToString() != "")
                    ddlPurchasingGroup.SelectedValue = Session[StaticKeys.MatPurchasingGroupId].ToString();

                ddlMrpType.SelectedValue = "ND";
                BindPlantWiseDropDown();

              
                if (lblModuleId.Text == "138")
                {
                     if (ddlPlant.SelectedValue != "9" && ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                        SetSelectedValue(ddlInspectionType, "Z1      ");
                   
                }
                else if (lblModuleId.Text == "147")
                {
                    ddlInspectionType.SelectedValue = "";
                }
                else if (lblModuleId.Text == "170")
                {
                    if (ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                        SetSelectedValue(ddlInspectionType, "Z1      ");
                }
                else if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
                {
                    // Check for Pune Plant
                    if (ddlPlant.SelectedValue != "9" && ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                    {
                        SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,15      ,89      ");
                    }
                }
             
                MRPTypeWiseSetup();
                LotSizeWiseValidation();
            }

            DisplayInspectionType();

            ddlMaterialAccGrp.Enabled = false;
            if (Session[StaticKeys.MatPurchasingGroupId].ToString() != "")
                ddlPurchasingGroup.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //private MaterialExtension GetMaterialExtensionfromDR(DataRow dr)
    //{
    //    MaterialExtension ObjMaterialExtension = new MaterialExtension();

    //    ObjMaterialExtension.Material_Extension_Id = dr["Material_Extension_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Material_Extension_Id"].ToString());
    //    ObjMaterialExtension.Master_Header_Id = dr["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Master_Header_Id"].ToString());

    //    ObjMaterialExtension.Material_Number = dr["Material_Number"].ToString();
    //    ObjMaterialExtension.Material_Type = dr["Material_Type"].ToString();
    //    ObjMaterialExtension.Material_Short_Description = dr["Material_Short_Description"].ToString();

    //    ObjMaterialExtension.Plant_Id = dr["Plant_Id"].ToString();
    //    ObjMaterialExtension.Storage_Location = dr["Storage_Location"].ToString();
    //    ObjMaterialExtension.Sales_Organization_Id = dr["Sales_Organization_Id"].ToString();
    //    ObjMaterialExtension.Distribution_Channel_ID = dr["Distribution_Channel_ID"].ToString();
    //    ObjMaterialExtension.Mat_Pricing_Group = dr["Mat_Pricing_Group"].ToString();
    //    ObjMaterialExtension.Acc_Assignment_Grp = dr["Acc_Assignment_Grp"].ToString();
    //    ObjMaterialExtension.Purchasing_Group = dr["Purchasing_Group"].ToString();
    //    ObjMaterialExtension.MRP_Type = dr["MRP_Type"].ToString();
    //    ObjMaterialExtension.MRP_Controller = dr["MRP_Controller"].ToString();
    //    ObjMaterialExtension.Reorder_Point = dr["Reorder_Point"].ToString();
    //    ObjMaterialExtension.Lot_Size = dr["Lot_Size"].ToString();
    //    ObjMaterialExtension.Min_Lot_Size = dr["Min_Lot_Size"].ToString();
    //    ObjMaterialExtension.Max_Lot_Size = dr["Max_Lot_Size"].ToString();
    //    ObjMaterialExtension.Fixed_Lot_Size = dr["Fixed_Lot_Size"].ToString();
    //    ObjMaterialExtension.Rounding_Value = dr["Rounding_Value"].ToString();
    //    ObjMaterialExtension.Issue_Storage_Location = dr["Issue_Storage_Location"].ToString();
    //    ObjMaterialExtension.GR_Processing_Time = dr["GR_Processing_Time"].ToString();
    //    ObjMaterialExtension.Planned_Delivery_Time_Days = dr["Planned_Delivery_Time_Days"].ToString();
    //    ObjMaterialExtension.Profit_Center = dr["Profit_Center"].ToString();
    //    ObjMaterialExtension.Valuation_Class = dr["Valuation_Class"].ToString();
    //    ObjMaterialExtension.Price_Ctrl_Indicator = dr["Price_Ctrl_Indicator"].ToString();

    //    return ObjMaterialExtension;
    //}

    private void ConfigureControl()
    {
        MasterAccess ObjMasterAccess = new MasterAccess();

        if (ddlMaterialAccGrp.SelectedValue != "")
        {
            Session[StaticKeys.SelectedModulePlantGrp] = ObjMasterAccess.GetSelectedModulePlantGrpByPlantGrp("1", ddlMaterialAccGrp.SelectedValue, "M");

            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();

            SectionConfiguration.Material_Extension_Data obj = new SectionConfiguration.Material_Extension_Data();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
    }

    private void DisplayInspectionType()
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
    }


    #endregion






}