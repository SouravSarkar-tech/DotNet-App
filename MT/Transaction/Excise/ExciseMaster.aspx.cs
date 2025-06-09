using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Transactions;
using System.Data;

public partial class Transaction_Excise_ExciseMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string exciseType = ddlExciseType.SelectedValue;
        lblExciseType.Text = ddlExciseType.SelectedItem.Text;
        pnlChapterId.Visible = false;
        pnlmcCombination.Visible = false;
        pnlCENVATDetermination.Visible = false;
        pnlVendorExciseDetails.Visible = false;
        pnlCustomerExciseDetails.Visible = false;
        pnlExciseTaxRate.Visible = false;
        pnlExciseTaxRate.Visible = false;

        pnlSearch.Visible = false;
        pnlNew.Visible = true;
        pnlMsg.Visible = false;
        if (exciseType == "21")
        {
            pnlChapterId.Visible = true;
        }
        else if (exciseType == "22")
        {
            pnlmcCombination.Visible = true;
        }
        else if (exciseType == "23")
        {
            pnlCENVATDetermination.Visible = true;
        }
        else if (exciseType == "24")
        {
            pnlVendorExciseDetails.Visible = true;
        }
        else if (exciseType == "25")
        {
            pnlCustomerExciseDetails.Visible = true;
        }
        else if (exciseType == "26")
        {
            pnlExciseTaxRate.Visible = true;
        }
        else if (exciseType == "27")
        {
            pnlExceptionMaterialExciseRate.Visible = true;
        }


        MaterialMasterAccess objAccess = new MaterialMasterAccess();
        //lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
        lblExciseId.Text = "0";

        string moduleId = exciseType;
        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
        string mode = "N";
        bool isUserApprover = objAccess.IsUserInitiator(moduleId, deptId, lblUserId.Text);
        ReadDeparmentListForRollback(lblMasterHeaderId.Text, deptId, moduleId);

        if (mode == "M" || mode == "N")
        {
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
        }
        if (mode == "M" || mode == "V")
        {

        }

        btnRejectTo.Visible = isUserApprover;
        lblIsUserApprover.Text = isUserApprover.ToString();
        if (isUserApprover)
        {
            btnSubmit.Text = "Approve";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        VendorMasterAccess materialAccess = new VendorMasterAccess();
        int masterHeaderId;
        string exciseType = ddlExciseType.SelectedValue;
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                masterHeaderId = materialAccess.SaveMaterialHeader("0", ddlExciseType.SelectedValue, lblUserId.Text, lblMode.Text);
                lblMasterHeaderId.Text = masterHeaderId.ToString();
                if (masterHeaderId > 0)
                {

                    if (exciseType == "18")
                    {
                        flg = SaveChapterId();
                    }
                    else if (exciseType == "19")
                    {
                        flg = SaveMaterialChapterCombination();
                    }
                    else if (exciseType == "20")
                    {
                        flg = SaveCENVATDetermination();
                    }
                    else if (exciseType == "21")
                    {
                        flg = SaveVendorExciseDetails();
                    }
                    else if (exciseType == "22")
                    {
                        flg = SaveCustomerExciseDetails();
                    }
                    else if (exciseType == "23")
                    {
                        flg = SaveExciseTaxRate();
                    }
                    else if (exciseType == "24")
                    {
                        flg = SaveExceptionMaterialExciseRate();
                    }
                    if (flg)
                    {
                        Session[StaticKeys.SelectedModuleId] = ddlExciseType.SelectedValue;
                        Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                        Session[StaticKeys.Mode] = "N";

                        pnlSearch.Visible = true;
                        pnlNew.Visible = false;
                        lblMsg.Text = Messages.GetMessage(1);
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;
                        lblExciseId.Text = "0";
                        scope.Complete();
                    }
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        pnlNew.Visible = false;
        pnlMsg.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReadMaterialMasterRequests();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {

    }

    protected void btnModify_Click(object sender, EventArgs e)
    {

    }


    protected void btnRollback_Click(object sender, EventArgs e)
    {

    }

    #region Private

    private void ReadMaterialMasterRequests()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        DataSet dstData = new DataSet();
        try
        {
            dstData = objMatAccess.ReadMaterialMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlExciseTypeSearch.SelectedValue, "E");
            grdSearch.DataSource = dstData.Tables[0].DefaultView;
            grdSearch.DataBind();

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[5].Visible = false;
                    grdSearch.Columns[6].Visible = true;
                    btnModify.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[5].Visible = true;
                    grdSearch.Columns[6].Visible = false;
                    btnModify.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[5].Visible = false;
                    grdSearch.Columns[6].Visible = false;
                    btnModify.Visible = true;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SaveChapterId()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        ChapterId ObjAcc = GetControlsValueChapterId();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveChapterId(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool SaveMaterialChapterCombination()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        MaterialChapterCombination ObjAcc = GetControlsValueMaterialChapterCombination();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveMaterialChapterCombination(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool SaveCENVATDetermination()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        CENVATDetermination ObjAcc = GetControlsValueCENVATDetermination();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveCENVATDetermination(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool SaveVendorExciseDetails()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        VendorExciseDetails ObjAcc = GetControlsValueVendorExciseDetails();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveVendorExciseDetails(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool SaveCustomerExciseDetails()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        CustomerExciseDetails ObjAcc = GetControlsValueCustomerExciseDetails();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveCustomerExciseDetails(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool SaveExciseTaxRate()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        ExciseTaxRate ObjAcc = GetControlsValueExciseTaxRate();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveExciseTaxRate(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool SaveExceptionMaterialExciseRate()
    {
        bool Flag = false;
        ExciseMasterAccess objAcess = new ExciseMasterAccess();
        ExceptionMaterialExciseRate ObjAcc = GetControlsValueExceptionMaterialExciseRate();

        try
        {
            if (lblIsUserApprover.Text.ToUpper() != "TRUE")
            {
                lblExciseId.Text = Convert.ToString(objAcess.SaveExceptionMaterialExciseRate(ObjAcc));

                if (Convert.ToInt16(lblExciseId.Text) > 0)
                {
                    Flag = true;

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
                Flag = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private ChapterId GetControlsValueChapterId()
    {
        ChapterId ObjAcc = new ChapterId();
        Utility objUtil = new Utility();

        ObjAcc.ChapterPk_Id = lblExciseId.Text;
        ObjAcc.Chapter_Id = txtChapterId.Text;
        ObjAcc.UOM = txtUOMExcise.Text;
        ObjAcc.Desc_Per_Law1 = txtDescAsPerLaw1.Text;
        ObjAcc.Desc_Per_Law2 = txtDescAsPerLaw2.Text;
        ObjAcc.Desc_Per_Law3 = txtDescAsPerLaw3.Text;
        ObjAcc.Desc_Per_Law4 = txtDescAsPerLaw4.Text;
        ObjAcc.Desc_Per_Law5 = txtDescAsPerLaw5.Text;
        ObjAcc.Desc_Per_Law6 = txtDescAsPerLaw6.Text;
        ObjAcc.Desc_Per_Law7 = txtDescAsPerLaw7.Text;
        ObjAcc.Desc_Per_Law8 = txtDescAsPerLaw8.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();

        return ObjAcc;
    }

    private MaterialChapterCombination GetControlsValueMaterialChapterCombination()
    {
        MaterialChapterCombination ObjAcc = new MaterialChapterCombination();
        Utility objUtil = new Utility();

        ObjAcc.Mat_Ch_Combi_Id = lblExciseId.Text;

        ObjAcc.Material_No = txtMaterialNumber.Text;
        ObjAcc.Plant = txtPlant.Text;
        ObjAcc.Chapter_ID = txtChapterId_mcCombination.Text;
        ObjAcc.Mat_Subcontractors = chkSubcontractors.Checked ? "Y" : "N";
        ObjAcc.Material_Type = txtMaterialType.Text;
        ObjAcc.Number_Goods = txtNoOfGoodsReceipts.Text;
        ObjAcc.Indicator = chkIndicatorMaterialDeclared.Checked ? "Y" : "N";
        ObjAcc.Declaration_date = txtDeclarationDate.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();
        return ObjAcc;
    }

    private CENVATDetermination GetControlsValueCENVATDetermination()
    {
        CENVATDetermination ObjAcc = new CENVATDetermination();
        Utility objUtil = new Utility();

        ObjAcc.CENVAT_Determination_Id = lblExciseId.Text;
        ObjAcc.Plant = txtPlant.Text;
        ObjAcc.Input_material = txtInputMaterialModvat.Text;
        ObjAcc.Output_material = txtOutputMaterialModvat.Text;
        ObjAcc.Default_Indicator = chkDefaultIndicatorModvat.Checked ? "Y" : "N";
        ObjAcc.Excise_Intimation = txtExciseIntimationDate.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();
        return ObjAcc;
    }

    private VendorExciseDetails GetControlsValueVendorExciseDetails()
    {
        VendorExciseDetails ObjAcc = new VendorExciseDetails();
        Utility objUtil = new Utility();

        ObjAcc.Vendor_Excise_Details_Id = lblExciseId.Text;
        ObjAcc.Account_No = txtAccountNoEX.Text;
        ObjAcc.ECC_NO = txtECCNumber.Text;
        ObjAcc.Excise_Reg_No = txtExciseRegNo.Text;
        ObjAcc.Excise_Range = txtExciseRange.Text;
        ObjAcc.Excise_Division = txtExciseDivision.Text;
        ObjAcc.Excise_Commissionerate = txtExciseCommissionerate.Text;
        ObjAcc.Central_Sales_Tax_No = txtCentralSalesTaxNo.Text;
        ObjAcc.Local_Sales_Tax_No = txtLocalSalesTaxNo.Text;
        ObjAcc.PAN = txtPermanentAccountNo.Text;
        ObjAcc.Excise_tax_indicator = txtExciseTaxndicator.Text;
        ObjAcc.SSI = txtSSIStatus.Text;
        ObjAcc.Type_Of_Vendor = txtTypeOfVendor.Text;
        ObjAcc.CENVAT = txtCentralSalesTaxNo.Text;
        ObjAcc.Service_Tax_Reg = txtServiceTaxRegNo.Text;
        ObjAcc.PAN_Ref_No = txtPANReferenceNumber.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();
        return ObjAcc;
    }

    private CustomerExciseDetails GetControlsValueCustomerExciseDetails()
    {
        CustomerExciseDetails ObjAcc = new CustomerExciseDetails();
        Utility objUtil = new Utility();

        ObjAcc.Customer_Excise_Detail_Id = lblExciseId.Text;
        ObjAcc.Customer_Number = txtCustomerNo.Text;
        ObjAcc.ECC_NO = txtECCNumberCust.Text;
        ObjAcc.Excise_Reg_No = txtExciseRegNoCust.Text;
        ObjAcc.Excise_Range = txtExciseRangeCust.Text;
        ObjAcc.Excise_Division = txtExciseDivisionCust.Text;
        ObjAcc.Excise_Commissionerate = txtExciseCommissionerateCust.Text;
        ObjAcc.Central_Sales_Tax_No = txtCentralSalesTaxNoCust.Text;
        ObjAcc.Local_Sales_Tax_No = txtLocalSalesTaxNoCust.Text;
        ObjAcc.PAN = txtPermanentAccountNoCust.Text;
        ObjAcc.Excise_tax_indicator = txtExciseTaxndicatorCust.Text;
        ObjAcc.Service_Tax_Reg = txtServiceTaxRegNoCust.Text;
        ObjAcc.PAN_Ref_No = txtPANReferenceNumberCust.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();
        return ObjAcc;
    }

    private ExciseTaxRate GetControlsValueExciseTaxRate()
    {
        ExciseTaxRate ObjAcc = new ExciseTaxRate();
        Utility objUtil = new Utility();

        ObjAcc.Excise_Tax_Rate_Id = lblExciseId.Text;
        ObjAcc.Chapter_ID = txtChapterIDEX.Text;
        ObjAcc.Excise_tax_indicator = txtExciseTaxndicatorCust.Text;
        ObjAcc.Date_from_rule_valid = txtDateFromTaxRuleValidEX.Text;
        ObjAcc.Date_To_rule_valid = txtDateToTaxRuleValidEX.Text;
        ObjAcc.Rate_Excise_Duty = txtRateExciseDutyEX.Text;
        ObjAcc.Excise_Duty_Rate = txtExciseDutyRateEX.Text;
        ObjAcc.Rate_Unit = txtRateUnitEX.Text;
        ObjAcc.Condition_pricing_unit = txtConditionPricingUnitEX.Text;
        ObjAcc.Condition_unit = txtConditionunitEX.Text;
        ObjAcc.Additional_Excise_Duty = txtAdditionExciseDuty.Text;
        ObjAcc.Special_Excise_Duty = txtSpecialExciseDuty.Text;
        ObjAcc.NCCD_Rate = txtMCCDRate.Text;
        ObjAcc.ECS_Rate = txtECSRate.Text;
        ObjAcc.AT1_rate = txtAT1Rate.Text;
        ObjAcc.AT2_rate = txtAT2Rate.Text;
        ObjAcc.AT3_rate = txtAT3Rate.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();
        return ObjAcc;
    }

    private ExceptionMaterialExciseRate GetControlsValueExceptionMaterialExciseRate()
    {
        ExceptionMaterialExciseRate ObjAcc = new ExceptionMaterialExciseRate();
        Utility objUtil = new Utility();

        ObjAcc.Exception_Material_Excise_Rate_Id = lblExciseId.Text;
        ObjAcc.Plant = txtPlantException.Text;
        ObjAcc.Material_Number = txtMaterialNumberEX.Text;
        ObjAcc.Account_No = txtAccountNoEX.Text;
        ObjAcc.Date_from_valid = txtDateFromTaxRuleValidEX.Text;
        ObjAcc.Type_Excise_duty = txtTypeExciseDutyEX.Text;
        ObjAcc.Date_to_valid = txtDateToTaxRuleValidEX.Text;
        ObjAcc.Chapter_ID = txtChapterIDEX.Text;
        ObjAcc.Rate_Excise_Duty = txtRateExciseDutyEX.Text;
        ObjAcc.Excise_Duty_Rate = txtExciseDutyRateEX.Text;
        ObjAcc.Rate_unit = txtRateUnitEX.Text;
        ObjAcc.Condition_pricing_unit = txtConditionPricingUnitEX.Text;
        ObjAcc.Condition_unit = txtConditionunitEX.Text;
        ObjAcc.UserId = lblUserId.Text;
        ObjAcc.TodayDate = objUtil.GetDate();
        return ObjAcc;
    }

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {
            ddlRejectTo.DataSource = materialMasterAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string GetSelectedPkID()
    {
        string strPk = string.Empty;
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    strPk = lblRequestID.Text;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strPk;
    }
    #endregion
}




