using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;

public partial class Transaction_Material_ExtensionData : System.Web.UI.Page
{
    MaterialCreateExtensionAccess ObjMaterialCreateExtensionAccess = new MaterialCreateExtensionAccess();
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
                    //btnSave.Visible = !btnNext.Visible;
                    btnSave.Visible = true;
                    btnNext.Visible = true;
                }

                //ClearCostingData();
                ////FillCostingData();
                ////FillCostingDataGrid();
                //ConfigureControl();

                ////To manage the Creation Single request
                //FillFormDataByMHId();
                //lnlAddDetails.Visible = false;
                //grvCosting1.Visible = false;
                FillFormDataByMHId();

            }
        }
        else
        {
            Response.Redirect("materialmaster.aspx");
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

            //Response.Redirect("ExtensionData.aspx");
            FillFormDataByMHId();
        }
    }

    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        GridViewRow grv = Grd_Extension.FooterRow;

        if (Save() && SaveNew(grv))
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            //Response.Redirect("ExtensionData.aspx");
            FillFormDataByMHId();
        }
    }
    
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //if (Save())
        //{
        //    string pageURL = btnNext.CommandArgument.ToString();
        //    Response.Redirect(pageURL);
        //}
        Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";

        Response.Redirect("MaterialMaster.aspx");

    }

    protected void Grd_Extension_DataBound(object sender, EventArgs e)
    {
        DataSet dataSet = ObjMaterialCreateExtensionAccess.GetMaterialCreateExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow grv in Grd_Extension.Rows)
            {
                DataRow dr = dataSet.Tables[0].Rows[grv.RowIndex];
                MaterialCreateExtension ObjMaterialCreateExtension = GetMaterialCreateExtensionfromDR(dr);
                ConfigureControl(grv);
                FillControlData(grv, "", ObjMaterialCreateExtension);

                //PopuplateDropDownList(grv);
                //BindPlantWiseDropDown(grv);
            }
        }

        if (trButton.Visible)
        {
            GridViewRow grv1 = Grd_Extension.FooterRow;

            DataSet ds = ObjMaterialCreateExtensionAccess.GetMaterialCreateExtensionReference(lblMasterHeaderId.Text);
            

            MaterialCreateExtension ObjMaterialCreateExtension1 = new MaterialCreateExtension();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ObjMaterialCreateExtension1 = GetMaterialCreateExtensionfromDR(ds.Tables[0].Rows[0]);
            }

            ConfigureControl(grv1, "f");
            FillControlData(grv1, "f", ObjMaterialCreateExtension1);
        }
        //PopuplateDropDownList(grv1, "f");
        //BindPlantWiseDropDown(grv1, "f");
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlPlant = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlPlant.Parent.Parent;
        //GridViewRow grv = (GridViewRow)ddlPlant.Parent.Parent.Parent.Parent;

        BindPlantWiseDropDown(grv, "");
    }

    protected void ddlPlantf_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlPlant = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlPlant.Parent.Parent;

        BindPlantWiseDropDown(grv, "f");
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlSalesOrginizationN = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlSalesOrginizationN.Parent.Parent;

        BindSalesOrgWiseDropDown(grv, "", ddlSalesOrginizationN);
    }

    protected void ddlSalesOrginizationf_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlSalesOrginizationN = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlSalesOrginizationN.Parent.Parent;

        BindSalesOrgWiseDropDown(grv, "f", ddlSalesOrginizationN);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkDelete = (LinkButton)sender;
        GridViewRow grv = (GridViewRow)lnkDelete.Parent.Parent;

        Label lblMatCreateExtensionId = (Label)grv.FindControl("lblMatCreateExtensionId");

        if (ObjMaterialCreateExtensionAccess.DeleteMaterialCreateExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMatCreateExtensionId.Text)) > 0)
        {
            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }

        FillFormDataByMHId();
        //GridViewRow grv1 = Grd_Extension.FooterRow;
        //FillControlData(grv1, "f", ObjMaterialCreateExtension);
    }

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        LinkButton lnkCopy = (LinkButton)sender;
        GridViewRow grv = (GridViewRow)lnkCopy.Parent.Parent;

        MaterialCreateExtension ObjMaterialCreateExtension = GetControlsValue(grv);

        GridViewRow grv1 = Grd_Extension.FooterRow;
        FillControlData(grv1, "f", ObjMaterialCreateExtension);
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        LinkButton lnkSave = (LinkButton)sender;
        GridViewRow grv = (GridViewRow)lnkSave.Parent.Parent;
        if (SaveNew(grv))
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }
    }

    protected void ddlMrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMrpType = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlMrpType.Parent.Parent;
        MRPTypeWiseSetup(grv, "");
    }

    protected void ddlLotSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlLotSize = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlLotSize.Parent.Parent;
        LotSizeWiseValidation(grv, "");
    }

    protected void ddlMrpTypef_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMrpType = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlMrpType.Parent.Parent;
        MRPTypeWiseSetup(grv, "f");
    }

    protected void ddlLotSizef_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlLotSize = (DropDownList)sender;
        GridViewRow grv = (GridViewRow)ddlLotSize.Parent.Parent;
        LotSizeWiseValidation(grv, "f");
    }

    #endregion

    #region Public Method

    private void PopuplateDropDownList(GridViewRow grv, string ext = "")
    {
        DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant" + ext);
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','All','" + lblCreateExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");
        //BindPlantWiseDropDown(grv, ext);

        DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization" + ext);
        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblCreateExtensionId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        DropDownList ddlAccountAssignment = (DropDownList)grv.FindControl("ddlAccountAssignment" + ext);
        helperAccess.PopuplateDropDownList(ddlAccountAssignment, "pr_GetDropDownListByControlNameModuleType 'M','ddlAccountAssignment'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlMaterialPGroup = (DropDownList)grv.FindControl("ddlMaterialPGroup" + ext);
        helperAccess.PopuplateDropDownList(ddlMaterialPGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialPGroup'", "LookUp_Desc", "LookUp_Code", "");
        //DropDownList ddlPurchasingGroup = (DropDownList)grv.FindControl("ddlPurchasingGroup" + ext);
        //helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlMrpType = (DropDownList)grv.FindControl("ddlMrpType" + ext);
        helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlPriceControlIndicator = (DropDownList)grv.FindControl("ddlPriceControlIndicator" + ext);
        helperAccess.PopuplateDropDownList(ddlPriceControlIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlPriceControlIndicator'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlLotSize = (DropDownList)grv.FindControl("ddlLotSize" + ext);
        helperAccess.PopuplateDropDownList(ddlLotSize, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlValuationClass = (DropDownList)grv.FindControl("ddlValuationClass" + ext);
        helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass'", "LookUp_Desc", "LookUp_Code", "");
    }

    private void FillFormDataByMHId()
    {
        DataSet dataSet = ObjMaterialCreateExtensionAccess.GetMaterialCreateExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));



        if (dataSet.Tables[0].Rows.Count == 0 && trButton.Visible)
        {
            dataSet.Tables[0].Rows.Add(dataSet.Tables[0].NewRow());
            Grd_Extension.DataSource = dataSet;
            Grd_Extension.DataBind();
            Grd_Extension.Rows[0].Visible = false;
            //Grd_Extension.Rows[0].Cells.Clear();
            //Grd_Extension.Rows[0].Cells.Add(new TableCell());
            //Grd_Extension.Rows[0].Cells[0].ColumnSpan = dataSet.Tables[0].Columns.Count;
            //Grd_Extension.Rows[0].Cells[0].Text = "No Records Found.";

        }
        else
        {
            Grd_Extension.DataSource = dataSet;
            Grd_Extension.DataBind();

            if (!trButton.Visible)
            {
                Grd_Extension.Columns[0].Visible = false;
                if (dataSet.Tables[0].Rows.Count > 0)
                    Grd_Extension.FooterRow.Visible = false;
            }
           
        }

        //if (!trButton.Visible)
        //{
        //    Grd_Extension.Columns[0].Visible = false;
        //    Grd_Extension.FooterRow.Visible = false;
        //}

    }

    private void BindPlantWiseDropDown(GridViewRow grv, string ext = "")
    {
        DropDownList ddlPlantN = (DropDownList)grv.FindControl("ddlPlant" + ext);
        DropDownList ddlStorageLocationN = (DropDownList)grv.FindControl("ddlStorageLocation" + ext);
        helperAccess.PopuplateDropDownList(ddlStorageLocationN, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantN.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        DropDownList ddlPurchasingGroupN = (DropDownList)grv.FindControl("ddlPurchasingGroup" + ext);
        helperAccess.PopuplateDropDownList(ddlPurchasingGroupN, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','" + lblSectionId.Text + "','" + ddlPlantN.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        
        
        DropDownList ddlMrpController = (DropDownList)grv.FindControl("ddlMrpController" + ext);
        helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','8','" + ddlPlantN.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        DropDownList ddlIssueStorageLocation = (DropDownList)grv.FindControl("ddlIssueStorageLocation" + ext);
        helperAccess.PopuplateDropDownList(ddlIssueStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlantN.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlProfitCenter = (DropDownList)grv.FindControl("ddlProfitCenter" + ext);
        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','13','" + ddlPlantN.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");




    }

    private void BindSalesOrgWiseDropDown(GridViewRow grv, string ext, DropDownList ddlSalesOrginizationN)
    {
        DropDownList ddlDistributionChannelN = (DropDownList)grv.FindControl("ddlDistributionChannel" + ext);

        helperAccess.PopuplateDropDownList(ddlDistributionChannelN, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblCreateExtensionId.Text + "','" + ddlSalesOrginizationN.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
    }

    private void MRPTypeWiseSetup(GridViewRow grv, string ext = "")
    {
        DropDownList ddlMrpType = (DropDownList)grv.FindControl("ddlMrpType" + ext);

        RequiredFieldValidator reqddlMrpController = (RequiredFieldValidator)grv.FindControl("reqddlMrpController" + ext);
        RequiredFieldValidator reqddlLotSize = (RequiredFieldValidator)grv.FindControl("reqddlLotSize" + ext);
        RequiredFieldValidator reqtxtReorder = (RequiredFieldValidator)grv.FindControl("reqtxtReorder" + ext);
        RequiredFieldValidator reqtxtFixedLotSize = (RequiredFieldValidator)grv.FindControl("reqtxtFixedLotSize" + ext);
        RequiredFieldValidator reqtxtMinLotSize = (RequiredFieldValidator)grv.FindControl("reqtxtMinLotSize" + ext);
        RequiredFieldValidator reqtxtMaxLotSize = (RequiredFieldValidator)grv.FindControl("reqtxtMaxLotSize" + ext);
        RequiredFieldValidator reqtxtRoundingValue = (RequiredFieldValidator)grv.FindControl("reqtxtRoundingValue" + ext);


        DropDownList ddlMrpController = (DropDownList)grv.FindControl("ddlMrpController" + ext);
        DropDownList ddlLotSize = (DropDownList)grv.FindControl("ddlLotSize" + ext);
        TextBox txtReorder = (TextBox)grv.FindControl("txtReorder" + ext);
        TextBox txtFixedLotSize = (TextBox)grv.FindControl("txtFixedLotSize" + ext);
        TextBox txtMinLotSize = (TextBox)grv.FindControl("txtMinLotSize" + ext);
        TextBox txtMaxLotSize = (TextBox)grv.FindControl("txtMaxLotSize" + ext);
        TextBox txtRoundingValue = (TextBox)grv.FindControl("txtRoundingValue" + ext);


        if (ddlMrpType.SelectedValue == "ND")
        {
            reqddlMrpController.Visible = false;
            reqddlLotSize.Visible = false;
            reqtxtReorder.Visible = false;


            ddlMrpController.SelectedValue = "";
            ddlLotSize.SelectedValue = "";

            //lableddlMrpController.Visible = false;
            //lableddlLotSize.Visible = false;
            //labletxtReorder.Visible = false;

            LotSizeWiseValidation(grv, ext);

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

            //lableddlMrpController.Visible = true;
            //lableddlLotSize.Visible = true;
            //labletxtReorder.Visible = false;

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
            //lableddlMrpController.Visible = true;
            //lableddlLotSize.Visible = true;
            //labletxtReorder.Visible = true;

            LotSizeWiseValidation(grv, ext);
        }
    }

    private void LotSizeWiseValidation(GridViewRow grv, string ext = "")
    {
        DropDownList ddlLotSize = (DropDownList)grv.FindControl("ddlLotSize" + ext);
        DropDownList ddlMrpType = (DropDownList)grv.FindControl("ddlMrpType" + ext);

        RequiredFieldValidator reqtxtFixedLotSize = (RequiredFieldValidator)grv.FindControl("reqtxtFixedLotSize" + ext);
        RequiredFieldValidator reqtxtMinLotSize = (RequiredFieldValidator)grv.FindControl("reqtxtMinLotSize" + ext);
        RequiredFieldValidator reqtxtMaxLotSize = (RequiredFieldValidator)grv.FindControl("reqtxtMaxLotSize" + ext);
        RequiredFieldValidator reqtxtRoundingValue = (RequiredFieldValidator)grv.FindControl("reqtxtRoundingValue" + ext);

        TextBox txtFixedLotSize = (TextBox)grv.FindControl("txtFixedLotSize" + ext);
        TextBox txtMinLotSize = (TextBox)grv.FindControl("txtMinLotSize" + ext);
        TextBox txtMaxLotSize = (TextBox)grv.FindControl("txtMaxLotSize" + ext);
        TextBox txtRoundingValue = (TextBox)grv.FindControl("txtRoundingValue" + ext);

        if (ddlMrpType.SelectedValue != "ND")
        {
            if (ddlLotSize.SelectedValue == "FX")
            {
                reqtxtFixedLotSize.Visible = true;
                reqtxtMinLotSize.Visible = false;
                reqtxtMaxLotSize.Visible = false;
                reqtxtRoundingValue.Visible = false;
                //labletxtFixedLotSize.Visible = true;

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
                reqtxtMinLotSize.Visible = true;
                reqtxtMaxLotSize.Visible = true;
                reqtxtRoundingValue.Visible = true;

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

    private bool Save()
    {
        bool flg = true;
        int i = 0;
        try
        {
            foreach (GridViewRow grv in Grd_Extension.Rows)
            {
                MaterialCreateExtension ObjMaterialCreateExtension = GetControlsValue(grv, "");
                if (ObjMaterialCreateExtension.Plant_Id != "")
                {
                    i++;
                    if (!(ObjMaterialCreateExtensionAccess.Save(ObjMaterialCreateExtension) > 0))
                    {
                        lblMsg.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;

                        flg = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private bool CheckPlantValid(string PlantId)
    {
        bool flg = true;
        if (PlantId == Session[StaticKeys.MaterialPlantId].ToString())
        {
            flg = false;
        }
        else
        {
            foreach (GridViewRow grv in Grd_Extension.Rows)
            {
                DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");
                if (ddlPlant.SelectedValue == PlantId)
                {
                    flg = false;
                    break;
                }
            }
        }

        return flg;
    }

    private bool SaveNew(GridViewRow grv)
    {
        bool flg = false;
        try
        {
            MaterialCreateExtension ObjMaterialCreateExtension = GetControlsValue(grv, "f");

            if (ObjMaterialCreateExtension.Plant_Id != null)
            {
                if (CheckPlantValid(ObjMaterialCreateExtension.Plant_Id))
                {
                    if (ObjMaterialCreateExtensionAccess.Save(ObjMaterialCreateExtension) > 0)
                    {
                        //ClearCostingData();
                        //FillCostingDataGrid();
                        //FillCostingData();
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
                    lblMsg.Text = "Plant already exists, please select other plant.";
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
            throw ex;
        }
        return flg;
    }

    private MaterialCreateExtension GetControlsValue(GridViewRow grv, string ext = "")
    {
        MaterialCreateExtension ObjMaterialCreateExtension = new MaterialCreateExtension();
        Utility objUtil = new Utility();


        try
        {
            Label lblMatCreateExtensionId = (Label)grv.FindControl("lblMatCreateExtensionId" + ext);

            ObjMaterialCreateExtension.Mat_Create_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMatCreateExtensionId.Text);
            ObjMaterialCreateExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant" + ext);
            DropDownList ddlStorageLocation = (DropDownList)grv.FindControl("ddlStorageLocation" + ext);
            DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization" + ext);
            DropDownList ddlDistributionChannel = (DropDownList)grv.FindControl("ddlDistributionChannel" + ext);
            DropDownList ddlMaterialPGroup = (DropDownList)grv.FindControl("ddlMaterialPGroup" + ext);
            DropDownList ddlAccountAssignment = (DropDownList)grv.FindControl("ddlAccountAssignment" + ext);
            DropDownList ddlPurchasingGroup = (DropDownList)grv.FindControl("ddlPurchasingGroup" + ext);
            DropDownList ddlMrpType = (DropDownList)grv.FindControl("ddlMrpType" + ext);
            DropDownList ddlMrpController = (DropDownList)grv.FindControl("ddlMrpController" + ext);
            TextBox txtReorder = (TextBox)grv.FindControl("txtReorder" + ext);
            DropDownList ddlLotSize = (DropDownList)grv.FindControl("ddlLotSize" + ext);
            TextBox txtMinLotSize = (TextBox)grv.FindControl("txtMinLotSize" + ext);
            TextBox txtMaxLotSize = (TextBox)grv.FindControl("txtMaxLotSize" + ext);
            TextBox txtFixedLotSize = (TextBox)grv.FindControl("txtFixedLotSize" + ext);
            TextBox txtRoundingValue = (TextBox)grv.FindControl("txtRoundingValue" + ext);
            DropDownList ddlIssueStorageLocation = (DropDownList)grv.FindControl("ddlIssueStorageLocation" + ext);
            TextBox txtGRProcessingTime = (TextBox)grv.FindControl("txtGRProcessingTime" + ext);
            TextBox txtPlannedDeleveryTime = (TextBox)grv.FindControl("txtPlannedDeleveryTime" + ext);
            DropDownList ddlProfitCenter = (DropDownList)grv.FindControl("ddlProfitCenter" + ext);
            DropDownList ddlValuationClass = (DropDownList)grv.FindControl("ddlValuationClass" + ext);
            DropDownList ddlPriceControlIndicator = (DropDownList)grv.FindControl("ddlPriceControlIndicator" + ext);

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

            ObjMaterialCreateExtension.IsActive = "1";
            ObjMaterialCreateExtension.UserId = lblUserId.Text;
            ObjMaterialCreateExtension.TodayDate = objUtil.GetDate();
            ObjMaterialCreateExtension.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjMaterialCreateExtension;
    }

    private void FillControlData(GridViewRow grv, string ext, MaterialCreateExtension ObjMaterialCreateExtension)
    {
        Utility objUtil = new Utility();

        try
        {
            PopuplateDropDownList(grv, ext);
            //Label lblMatCreateExtensionId = (Label)grv.FindControl("lblMatCreateExtensionId" + ext);

            //ObjMaterialCreateExtension.Mat_Create_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMatCreateExtensionId.Text);
            //ObjMaterialCreateExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant" + ext);
            DropDownList ddlStorageLocation = (DropDownList)grv.FindControl("ddlStorageLocation" + ext);
            DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization" + ext);
            DropDownList ddlDistributionChannel = (DropDownList)grv.FindControl("ddlDistributionChannel" + ext);
            DropDownList ddlMaterialPGroup = (DropDownList)grv.FindControl("ddlMaterialPGroup" + ext);
            DropDownList ddlAccountAssignment = (DropDownList)grv.FindControl("ddlAccountAssignment" + ext);
            DropDownList ddlPurchasingGroup = (DropDownList)grv.FindControl("ddlPurchasingGroup" + ext);
            DropDownList ddlMrpType = (DropDownList)grv.FindControl("ddlMrpType" + ext);
            DropDownList ddlMrpController = (DropDownList)grv.FindControl("ddlMrpController" + ext);
            TextBox txtReorder = (TextBox)grv.FindControl("txtReorder" + ext);
            DropDownList ddlLotSize = (DropDownList)grv.FindControl("ddlLotSize" + ext);
            TextBox txtMinLotSize = (TextBox)grv.FindControl("txtMinLotSize" + ext);
            TextBox txtMaxLotSize = (TextBox)grv.FindControl("txtMaxLotSize" + ext);
            TextBox txtFixedLotSize = (TextBox)grv.FindControl("txtFixedLotSize" + ext);
            TextBox txtRoundingValue = (TextBox)grv.FindControl("txtRoundingValue" + ext);
            DropDownList ddlIssueStorageLocation = (DropDownList)grv.FindControl("ddlIssueStorageLocation" + ext);
            TextBox txtGRProcessingTime = (TextBox)grv.FindControl("txtGRProcessingTime" + ext);
            TextBox txtPlannedDeleveryTime = (TextBox)grv.FindControl("txtPlannedDeleveryTime" + ext);
            DropDownList ddlProfitCenter = (DropDownList)grv.FindControl("ddlProfitCenter" + ext);
            DropDownList ddlValuationClass = (DropDownList)grv.FindControl("ddlValuationClass" + ext);
            DropDownList ddlPriceControlIndicator = (DropDownList)grv.FindControl("ddlPriceControlIndicator" + ext);

            if (ObjMaterialCreateExtension.Plant_Id != "")
            {
                ddlPlant.SelectedValue = ObjMaterialCreateExtension.Plant_Id;
                if (ext == "")
                {
                    ddlPlant.Enabled = false;
                }
                BindPlantWiseDropDown(grv, ext);

                ddlStorageLocation.SelectedValue = ObjMaterialCreateExtension.Storage_Location;
                ddlSalesOrginization.SelectedValue = ObjMaterialCreateExtension.Sales_Organization_Id;
                BindSalesOrgWiseDropDown(grv, ext, ddlSalesOrginization);

                ddlDistributionChannel.SelectedValue = ObjMaterialCreateExtension.Distribution_Channel_ID;
                ddlMaterialPGroup.SelectedValue = ObjMaterialCreateExtension.Mat_Pricing_Group;
                ddlAccountAssignment.SelectedValue = ObjMaterialCreateExtension.Acc_Assignment_Grp;
                ddlPurchasingGroup.SelectedValue = ObjMaterialCreateExtension.Purchasing_Group;
                ddlMrpType.SelectedValue = ObjMaterialCreateExtension.MRP_Type;
                MRPTypeWiseSetup(grv, ext);

                ddlMrpController.SelectedValue = ObjMaterialCreateExtension.MRP_Controller;
                txtReorder.Text = ObjMaterialCreateExtension.Reorder_Point;
                ddlLotSize.SelectedValue = ObjMaterialCreateExtension.Lot_Size;
                LotSizeWiseValidation(grv, ext);

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
            }


            //
            ddlPlant.TabIndex = TabIndex;
            ddlStorageLocation.TabIndex = TabIndex;
            ddlSalesOrginization.TabIndex = TabIndex;

            ddlDistributionChannel.TabIndex = TabIndex;
            ddlMaterialPGroup.TabIndex = TabIndex;
            ddlAccountAssignment.TabIndex = TabIndex;
            ddlPurchasingGroup.TabIndex = TabIndex;
            ddlMrpType.TabIndex = TabIndex;


            ddlMrpController.TabIndex = TabIndex;
            txtReorder.TabIndex = TabIndex;
            ddlLotSize.TabIndex = TabIndex;


            txtMinLotSize.TabIndex = TabIndex;
            txtMaxLotSize.TabIndex = TabIndex;
            txtFixedLotSize.TabIndex = TabIndex;
            txtRoundingValue.TabIndex = TabIndex;
            ddlIssueStorageLocation.TabIndex = TabIndex;
            txtGRProcessingTime.TabIndex = TabIndex;
            txtPlannedDeleveryTime.TabIndex = TabIndex;
            ddlProfitCenter.TabIndex = TabIndex;
            ddlValuationClass.TabIndex = TabIndex;
            ddlPriceControlIndicator.TabIndex = TabIndex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //return ObjMaterialCreateExtension;
    }

    private MaterialCreateExtension GetMaterialCreateExtensionfromDR(DataRow dr)
    {
        MaterialCreateExtension ObjMaterialCreateExtension = new MaterialCreateExtension();

        ObjMaterialCreateExtension.Mat_Create_Extension_Id = dr["Mat_Create_Extension_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Mat_Create_Extension_Id"].ToString());
        ObjMaterialCreateExtension.Master_Header_Id = dr["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Master_Header_Id"].ToString());

        ObjMaterialCreateExtension.Plant_Id = dr["Plant_Id"].ToString();
        ObjMaterialCreateExtension.Storage_Location = dr["Storage_Location"].ToString();
        ObjMaterialCreateExtension.Sales_Organization_Id = dr["Sales_Organization_Id"].ToString();
        ObjMaterialCreateExtension.Distribution_Channel_ID = dr["Distribution_Channel_ID"].ToString();
        ObjMaterialCreateExtension.Mat_Pricing_Group = dr["Mat_Pricing_Group"].ToString();
        ObjMaterialCreateExtension.Acc_Assignment_Grp = dr["Acc_Assignment_Grp"].ToString();
        ObjMaterialCreateExtension.Purchasing_Group = dr["Purchasing_Group"].ToString();
        ObjMaterialCreateExtension.MRP_Type = dr["MRP_Type"].ToString();
        ObjMaterialCreateExtension.MRP_Controller = dr["MRP_Controller"].ToString();
        ObjMaterialCreateExtension.Reorder_Point = dr["Reorder_Point"].ToString();
        ObjMaterialCreateExtension.Lot_Size = dr["Lot_Size"].ToString();
        ObjMaterialCreateExtension.Min_Lot_Size = dr["Min_Lot_Size"].ToString();
        ObjMaterialCreateExtension.Max_Lot_Size = dr["Max_Lot_Size"].ToString();
        ObjMaterialCreateExtension.Fixed_Lot_Size = dr["Fixed_Lot_Size"].ToString();
        ObjMaterialCreateExtension.Rounding_Value = dr["Rounding_Value"].ToString();
        ObjMaterialCreateExtension.Issue_Storage_Location = dr["Issue_Storage_Location"].ToString();
        ObjMaterialCreateExtension.GR_Processing_Time = dr["GR_Processing_Time"].ToString();
        ObjMaterialCreateExtension.Planned_Delivery_Time_Days = dr["Planned_Delivery_Time_Days"].ToString();
        ObjMaterialCreateExtension.Profit_Center = dr["Profit_Center"].ToString();
        ObjMaterialCreateExtension.Valuation_Class = dr["Valuation_Class"].ToString();
        ObjMaterialCreateExtension.Price_Ctrl_Indicator = dr["Price_Ctrl_Indicator"].ToString();

        return ObjMaterialCreateExtension;
    }

    private void ConfigureControl(GridViewRow grv, string ext = "")
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Material_Extension_Data obj = new SectionConfiguration.Material_Extension_Data();
        SectionConfiguration.FieldStatus.SetFieldStatus(grv, obj.GetClass(str), ext);
    }

    #endregion





}