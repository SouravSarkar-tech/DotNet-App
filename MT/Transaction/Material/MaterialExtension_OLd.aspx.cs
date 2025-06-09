using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;

public partial class Transaction_Material_MaterialExtension : System.Web.UI.Page
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

            Response.Redirect("MaterialExtension.aspx");
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

            Response.Redirect("MaterialExtension.aspx");
        }
    }

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

    protected void Grd_Extension_DataBound(object sender, EventArgs e)
    {
        DataSet dataSet = ObjMaterialExtensionAccess.GetMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow grv in Grd_Extension.Rows)
            {
                DataRow dr = dataSet.Tables[0].Rows[grv.RowIndex];
                MaterialExtension ObjMaterialExtension = GetMaterialExtensionfromDR(dr);
                FillControlData(grv, "", ObjMaterialExtension);
                ConfigureControl(grv);
                //PopuplateDropDownList(grv);
                //BindPlantWiseDropDown(grv);
            }
        }

        if (trButton.Visible)
        {
            GridViewRow grv1 = Grd_Extension.FooterRow;

            MaterialExtension ObjMaterialExtension1 = new MaterialExtension();
            //ConfigureControl(grv1, "f");
            FillControlData(grv1, "f", ObjMaterialExtension1);
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

        Label lblMaterialExtensionId = (Label)grv.FindControl("lblMaterialExtensionId");

        if (ObjMaterialExtensionAccess.DeleteMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMaterialExtensionId.Text)) > 0)
        {
            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }

        FillFormDataByMHId();
        //GridViewRow grv1 = Grd_Extension.FooterRow;
        //FillControlData(grv1, "f", ObjMaterialExtension);
    }

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        LinkButton lnkCopy = (LinkButton)sender;
        GridViewRow grv = (GridViewRow)lnkCopy.Parent.Parent;

        MaterialExtension ObjMaterialExtension = GetControlsValue(grv);

        GridViewRow grv1 = Grd_Extension.FooterRow;
        FillControlData(grv1, "f", ObjMaterialExtension);
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

    protected void txtMaterialCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txtMaterialCode = (TextBox)sender;
        GridViewRow grv = (GridViewRow)txtMaterialCode.Parent.Parent;

        MaterialTypeSelection(grv, "");
    }



    protected void txtMaterialCodef_TextChanged(object sender, EventArgs e)
    {
        TextBox txtMaterialCode = (TextBox)sender;
        GridViewRow grv = (GridViewRow)txtMaterialCode.Parent.Parent;

        MaterialTypeSelection(grv, "f");
    }



    #endregion

    #region Public Method

    private void PopuplateDropDownList(GridViewRow grv, string ext = "")
    {
        DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp" + ext);
        helperAccess.PopuplateDropDownList(ddlMaterialAccGrp, "pr_GetModuleListByModuleType 'M'", "Module_Name", "Module_Id", "");

        DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant" + ext);
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','All','" + lblMatExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");
        //BindPlantWiseDropDown(grv, ext);

        DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization" + ext);
        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        DropDownList ddlAccountAssignment = (DropDownList)grv.FindControl("ddlAccountAssignment" + ext);
        helperAccess.PopuplateDropDownList(ddlAccountAssignment, "pr_GetDropDownListByControlNameModuleType 'M','ddlAccountAssignment'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlMaterialPGroup = (DropDownList)grv.FindControl("ddlMaterialPGroup" + ext);
        helperAccess.PopuplateDropDownList(ddlMaterialPGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialPGroup'", "LookUp_Desc", "LookUp_Code", "");
        DropDownList ddlPurchasingGroup = (DropDownList)grv.FindControl("ddlPurchasingGroup" + ext);
        helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
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
        DataSet dataSet = ObjMaterialExtensionAccess.GetMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));

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

        helperAccess.PopuplateDropDownList(ddlDistributionChannelN, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "','" + ddlSalesOrginizationN.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
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

    private void MaterialTypeSelection(GridViewRow grv, string ext = "")
    {
        TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode" + ext);
        DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp" + ext);
        DropDownList ddlValuationClass = (DropDownList)grv.FindControl("ddlValuationClass" + ext);
        DropDownList ddlPriceControlIndicator = (DropDownList)grv.FindControl("ddlPriceControlIndicator" + ext);
        
        txtMaterialCode.Text = txtMaterialCode.Text.ToUpper();
        string str = txtMaterialCode.Text.Substring(0, 1).ToUpper();
        //string str1 = txtMaterialCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtMaterialCode.Text);

        switch (str)
        {
            //case "L":
            //    regtxtMaterialCode.ValidationExpression = "^[\\S]{4}$";
            //    ddlMaterialAccGrp.SelectedValue = "88";

            //    break;
            default:
                //regtxtMaterialCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 199999) //ROH 1- Series
                {
                    ddlMaterialAccGrp.SelectedValue = "162";
                    ddlValuationClass.SelectedValue = "4010";
                }
                else if (strcode >= 200000 && strcode < 299999)//VERP  2- Series
                    ddlMaterialAccGrp.SelectedValue = "164";
                else if (strcode >= 300000 && strcode < 399999)//HALB  3- Series
                    ddlMaterialAccGrp.SelectedValue = "144";
                else if (strcode >= 400000 && strcode < 499999)//FERT  4- Series
                    ddlMaterialAccGrp.SelectedValue = "139";
                else if (strcode >= 500000 && strcode < 599999)//HAWA  5- Series
                    ddlMaterialAccGrp.SelectedValue = "145";
                else if (strcode >= 600000 && strcode < 6799999)//ERSA  6- Series
                {
                    ddlMaterialAccGrp.SelectedValue = "138";
                    ddlValuationClass.SelectedValue = "6030";
                }
                else if (strcode >= 700000 && strcode < 7799999)//HIBE  7- Series
                {
                    ddlMaterialAccGrp.SelectedValue = "147";
                    ddlValuationClass.SelectedValue = "4010";
                }
                else if (strcode >= 800000 && strcode < 849999)//ZNBW  8- Series
                {
                    ddlMaterialAccGrp.SelectedValue = "171";
                }
                else if (strcode >= 850000 && strcode < 889999)//UNBW 85- Series
                {
                    ddlMaterialAccGrp.SelectedValue = "163";
                }
                else if (strcode >= 890000 && strcode < 959999)//ZMBW  89/9- Series
                {
                    ddlMaterialAccGrp.SelectedValue = "170";
                    ddlValuationClass.SelectedValue = "7060";
                }

                ddlPriceControlIndicator.SelectedValue = "V";
                ddlPriceControlIndicator.Enabled = false;
                ddlValuationClass.Enabled = false;
                break;
        }

        ConfigureControl(grv, ext);
    }

    private bool Save()
    {
        bool flg = false;
        int i = 0;
        try
        {
            foreach (GridViewRow grv in Grd_Extension.Rows)
            {
                MaterialExtension ObjMaterialExtension = GetControlsValue(grv, "");
                if (ObjMaterialExtension.Plant_Id != "")
                {
                    i++;
                    if (!(ObjMaterialExtensionAccess.Save(ObjMaterialExtension) > 0))
                    {
                        lblMsg.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                    flg = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private bool CheckMatValid(string MaterialNumber)
    {
        bool flg = true;
        //if (PlantId == Session[StaticKeys.MaterialPlantId].ToString())
        //{
        //    flg = false;
        //}
        //else
        //{
        foreach (GridViewRow grv in Grd_Extension.Rows)
        {
            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");
            if (txtMaterialCode.Text == MaterialNumber)
            {
                flg = false;
                break;
            }
        }
        //}

        return flg;
    }

    private bool SaveNew(GridViewRow grv)
    {
        bool flg = false;
        try
        {
            MaterialExtension ObjMaterialExtension = GetControlsValue(grv, "f");

            if (ObjMaterialExtension.Material_Number != null)
            {
                if (CheckMatValid(ObjMaterialExtension.Material_Number))
                {
                    if (ObjMaterialExtensionAccess.Save(ObjMaterialExtension) > 0)
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
                    lblMsg.Text = "Material already exists, please enter another Material.";
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

    private MaterialExtension GetControlsValue(GridViewRow grv, string ext = "")
    {
        MaterialExtension ObjMaterialExtension = new MaterialExtension();
        Utility objUtil = new Utility();


        try
        {
            Label lblMaterialExtensionId = (Label)grv.FindControl("lblMaterialExtensionId" + ext);

            ObjMaterialExtension.Material_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMaterialExtensionId.Text);
            ObjMaterialExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);

            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode" + ext);
            DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp" + ext);
            TextBox txtMaterialDescription = (TextBox)grv.FindControl("txtMaterialDescription" + ext);

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

    private void FillControlData(GridViewRow grv, string ext, MaterialExtension ObjMaterialExtension)
    {
        Utility objUtil = new Utility();

        try
        {
            PopuplateDropDownList(grv, ext);
            //Label lblMaterialExtensionId = (Label)grv.FindControl("lblMaterialExtensionId" + ext);

            //ObjMaterialExtension.Mat_Create_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMaterialExtensionId.Text);
            //ObjMaterialExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);


            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode" + ext);
            DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp" + ext);
            TextBox txtMaterialDescription = (TextBox)grv.FindControl("txtMaterialDescription" + ext);

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

            if (ObjMaterialExtension.Plant_Id != "" && ObjMaterialExtension.Plant_Id != null)
            {

                txtMaterialCode.Text = ObjMaterialExtension.Material_Number;
                ddlMaterialAccGrp.SelectedValue = ObjMaterialExtension.Material_Type;
                txtMaterialDescription.Text = ObjMaterialExtension.Material_Short_Description;

                ddlPlant.SelectedValue = ObjMaterialExtension.Plant_Id;
                if (ext == "")
                {
                    ddlPlant.Enabled = false;
                    txtMaterialCode.Enabled = false;

                    txtMaterialDescription.Enabled = false;
                }
                BindPlantWiseDropDown(grv, ext);

                ddlStorageLocation.SelectedValue = ObjMaterialExtension.Storage_Location;
                ddlSalesOrginization.SelectedValue = ObjMaterialExtension.Sales_Organization_Id;
                BindSalesOrgWiseDropDown(grv, ext, ddlSalesOrginization);

                ddlDistributionChannel.SelectedValue = ObjMaterialExtension.Distribution_Channel_ID;
                ddlMaterialPGroup.SelectedValue = ObjMaterialExtension.Mat_Pricing_Group;
                ddlAccountAssignment.SelectedValue = ObjMaterialExtension.Acc_Assignment_Grp;
                ddlPurchasingGroup.SelectedValue = ObjMaterialExtension.Purchasing_Group;
                ddlMrpType.SelectedValue = ObjMaterialExtension.MRP_Type;
                MRPTypeWiseSetup(grv, ext);

                ddlMrpController.SelectedValue = ObjMaterialExtension.MRP_Controller;
                txtReorder.Text = ObjMaterialExtension.Reorder_Point;
                ddlLotSize.SelectedValue = ObjMaterialExtension.Lot_Size;
                LotSizeWiseValidation(grv, ext);

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
            }
            else
            {
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                ddlPlant.Enabled = false;

                BindPlantWiseDropDown(grv, ext);
            }


            ddlMaterialAccGrp.Enabled = false;

            //

            txtMaterialCode.TabIndex = TabIndex;
            ddlMaterialAccGrp.TabIndex = TabIndex;
            txtMaterialDescription.TabIndex = TabIndex;
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
        //return ObjMaterialExtension;
    }

    private MaterialExtension GetMaterialExtensionfromDR(DataRow dr)
    {
        MaterialExtension ObjMaterialExtension = new MaterialExtension();

        ObjMaterialExtension.Material_Extension_Id = dr["Material_Extension_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Material_Extension_Id"].ToString());
        ObjMaterialExtension.Master_Header_Id = dr["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Master_Header_Id"].ToString());

        ObjMaterialExtension.Material_Number = dr["Material_Number"].ToString();
        ObjMaterialExtension.Material_Type = dr["Material_Type"].ToString();
        ObjMaterialExtension.Material_Short_Description = dr["Material_Short_Description"].ToString();

        ObjMaterialExtension.Plant_Id = dr["Plant_Id"].ToString();
        ObjMaterialExtension.Storage_Location = dr["Storage_Location"].ToString();
        ObjMaterialExtension.Sales_Organization_Id = dr["Sales_Organization_Id"].ToString();
        ObjMaterialExtension.Distribution_Channel_ID = dr["Distribution_Channel_ID"].ToString();
        ObjMaterialExtension.Mat_Pricing_Group = dr["Mat_Pricing_Group"].ToString();
        ObjMaterialExtension.Acc_Assignment_Grp = dr["Acc_Assignment_Grp"].ToString();
        ObjMaterialExtension.Purchasing_Group = dr["Purchasing_Group"].ToString();
        ObjMaterialExtension.MRP_Type = dr["MRP_Type"].ToString();
        ObjMaterialExtension.MRP_Controller = dr["MRP_Controller"].ToString();
        ObjMaterialExtension.Reorder_Point = dr["Reorder_Point"].ToString();
        ObjMaterialExtension.Lot_Size = dr["Lot_Size"].ToString();
        ObjMaterialExtension.Min_Lot_Size = dr["Min_Lot_Size"].ToString();
        ObjMaterialExtension.Max_Lot_Size = dr["Max_Lot_Size"].ToString();
        ObjMaterialExtension.Fixed_Lot_Size = dr["Fixed_Lot_Size"].ToString();
        ObjMaterialExtension.Rounding_Value = dr["Rounding_Value"].ToString();
        ObjMaterialExtension.Issue_Storage_Location = dr["Issue_Storage_Location"].ToString();
        ObjMaterialExtension.GR_Processing_Time = dr["GR_Processing_Time"].ToString();
        ObjMaterialExtension.Planned_Delivery_Time_Days = dr["Planned_Delivery_Time_Days"].ToString();
        ObjMaterialExtension.Profit_Center = dr["Profit_Center"].ToString();
        ObjMaterialExtension.Valuation_Class = dr["Valuation_Class"].ToString();
        ObjMaterialExtension.Price_Ctrl_Indicator = dr["Price_Ctrl_Indicator"].ToString();

        return ObjMaterialExtension;
    }

    private void ConfigureControl(GridViewRow grv, string ext = "")
    {
        //string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();

        MasterAccess ObjMasterAccess = new MasterAccess();

        DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp" + ext);
        if (ddlMaterialAccGrp.SelectedValue != "")
        {
            Session[StaticKeys.SelectedModulePlantGrp] = ObjMasterAccess.GetSelectedModulePlantGrpByPlantGrp("1", ddlMaterialAccGrp.SelectedValue, "M");

            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();

            SectionConfiguration.Material_Extension_Data obj = new SectionConfiguration.Material_Extension_Data();
            SectionConfiguration.FieldStatus.SetFieldStatus(grv, obj.GetClass(str), ext);
        }
    }

    #endregion





}