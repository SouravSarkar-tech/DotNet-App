using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using log4net;
public partial class Transaction_Sales1 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    SalesAccess ObjSalesAccess = new SalesAccess();
    Utility objUtil = new Utility();
    HelperAccess helperAccess = new HelperAccess();
    Sales1 objSavedSales1 = new Sales1();
    List<string> DepotPlants = new List<string> { "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87" };
    string sdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogS1" + sdate + ".txt", "Start" + HelperAccess.ReqType);
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load", ex);
        }
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            lblMessage.Text = null;
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
                        //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                        if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                        {
                            btnSave.Visible = true;
                        }
                        //MSC_8300001775 End 
                    }

                    ClearData();
                    //FillDataGrid();
                    ConfigureControl();

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
        labletxtMinDeliveryQuantity.Visible = false;
        reqtxtMinDeliveryQuantity.Visible = false;
        txtMinDeliveryQuantity.Enabled = true;
        txtMinDeliveryQuantity.CssClass = "txtbox";

        labletxtMinOrderQBaseUnitM.Visible = false;
        reqtxtMinOrderQBaseUnitM.Visible = false;
        txtMinOrderQBaseUnitM.Enabled = true;
        txtMinOrderQBaseUnitM.CssClass = "txtbox";

        ddlSalesOrginization.Enabled = false;
        ddlDistributionChannel.Enabled = false;

        chkVariSalesUnitNotAllowed.Enabled = true;
        ddlSalesUnit.Enabled = true;
        txtDeliveryUnit.Enabled = true;
        txtDeliveryUnit.CssClass = "txtbox";
        ddlUnitMeasureDelivery.Enabled = true;

        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }

    }



    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (Save())
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
        if (Save())
        {
            //lblMessage.Text = Messages.GetMessage(1);
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
            lblMessage.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true; 
            Response.Redirect("Sales1.aspx");
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
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected bool Save()
    {
        bool flg = false;
        try
        {
            Sales1 ObjSales = GetControlsValue();
            objSavedSales1 = GetSalesData();
            if (CheckValidSalesUnit())
            {
                if (CheckValidSalesUnitInBasicData2())
                {
                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                    {
                        if (objSavedSales1.Mat_Sales1_Id > 0)
                        {
                            CheckIfChanges(ObjSales, objSavedSales1);
                        }
                    }

                    if (ObjSalesAccess.Save(ObjSales) > 0)
                    {
                        //MSC_8300001775
                        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                        {
                            CheckIfChangesLog(ObjSales, objSavedSales1);
                        }
                        //MSC_8300001775

                        ClearData();
                        //FillDataGrid();
                        FillFormDataByMHId();
                        lblMessage.Text = Messages.GetMessage(1);
                        flg = true;
                        ////MSC_8300001775
                        //if (HelperAccess.ReqType == "SMC")
                        //{
                        //    CheckIfChangesLog(ObjSales, objSavedSales1);
                        //}
                        ////MSC_8300001775
                    }
                    else
                    {
                        lblMessage.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "Please maintain conversion factor according to Sales Unit. <a href = 'BasicData2.aspx'> Click here to update </a>";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = "The alternative unit of measure cannot be same as base unit of measure";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }



        }
        catch (Exception ex)
        { _log.Error("Save", ex); 

        throw;
        }

        return flg;
    }

    private bool CheckValidSalesUnitInBasicData2()
    {
        bool flag = true;
        //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
        //{
        //    if (ddlSalesUnit.SelectedValue != "")
        //    {
        //        flag = ObjSalesAccess.CheckValidSalesUnitInBasicData2(lblMasterHeaderId.Text, ddlSalesUnit.SelectedValue);
        //    }
        //}
        //MSC_8300001775
        try
        {
        if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
        {
            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
            {
                if (ddlSalesUnit.SelectedValue != "")
                {
                    flag = ObjSalesAccess.CheckValidSalesUnitInBasicData2(lblMasterHeaderId.Text, ddlSalesUnit.SelectedValue);
                }
            }
            }
        }
        catch (Exception ex)
        { _log.Error("CheckValidSalesUnitInBasicData2", ex); }
        //MSC_8300001775
        return flag;
    }

    private bool CheckValidSalesUnit()
    {
        bool flag = true;
        try
        {
        //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
        //{
        //    if (ddlSalesUnit.SelectedValue != "")
        //    {
        //        flag = ObjSalesAccess.CheckValidSalesUnit(lblMasterHeaderId.Text, ddlSalesUnit.SelectedValue);
        //    }
        //}
        //MSC_8300001775
        if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
        {
            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
            {
                if (ddlSalesUnit.SelectedValue != "")
                {
                    flag = ObjSalesAccess.CheckValidSalesUnit(lblMasterHeaderId.Text, ddlSalesUnit.SelectedValue);
                }
            }
            }
        }
        catch (Exception ex)
        { _log.Error("CheckValidSalesUnit", ex); }
        //MSC_8300001775
        return flag;
    }

    protected void lnlAddDetails_Click(object s, EventArgs e)
    {
        try
        {
        ClearData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
        ClearData();
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        BindPlantWiseDropDown();
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblSalesId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_Sales1_Id"].ToString();
            FillSalesData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
        }
    }

    private void ClearData()
    {
        try
        {
        lblSalesId.Text = "0";
        txtDistributionCSpecMStatus.Text = "";
        txtMaterialSSaleValid.Text = "";
        txtDeliveryUnit.Text = "";
        txtMinOrderQBaseUnitM.Text = "";
        txtMinDeliveryQuantity.Text = "";
        txtMinMOrderQuantity.Text = "";
        chkCashDiscountIndi.Checked = false;
        chkVariSalesUnitNotAllowed.Checked = false;
        txtSalesText.Text = "";

        PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDropDownListByControlNameModuleType 'M','ddlDeliveringPlant'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSalesUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlUnitMeasureDelivery, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    void FillDataGrid()
    {
        try
        {
        DataSet ds = ObjSalesAccess.GetSalesData1(Convert.ToInt32(lblMasterHeaderId.Text));
        gvData.DataSource = ds;
        gvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDataGrid", ex); }
    }

    private Sales1 GetSalesData()
    {
        return ObjSalesAccess.GetSales1(Convert.ToInt32(lblSalesId.Text));
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjSalesAccess.GetSalesData1(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblSalesId.Text = ds.Tables[0].Rows[0]["Mat_Sales1_Id"].ToString();
        }
        FillSalesData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillSalesData()
    {
        Sales1 ObjSales = GetSalesData();
        try
        {
            if (ObjSales.Mat_Sales1_Id > 0)
            {
                lblSalesId.Text = ObjSales.Mat_Sales1_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = ObjSales.Plant_Id;
                BindPlantWiseDropDown();

                ddlSalesOrginization.SelectedValue = ObjSales.Sales_Organization_Id;
                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                ddlDistributionChannel.SelectedValue = ObjSales.Distribution_Channel_ID;

                Session[StaticKeys.MaterialSalesOrgId] = ddlSalesOrginization.SelectedValue;
                Session[StaticKeys.MaterialDistChnlId] = ddlDistributionChannel.SelectedValue;

                ddlSalesUnit.SelectedValue = ObjSales.Sales_Unit;
                txtDistributionCSpecMStatus.Text = ObjSales.Distri_Chain_Speci_Mat_Status;
                txtMaterialSSaleValid.Text = ObjSales.Mat_Status_Sales_Valid_From;
                ddlDeliveringPlant.SelectedValue = ObjSales.Delivery_Plant;
                txtMinOrderQBaseUnitM.Text = ObjSales.Min_Order_Quan_Base_Unit;
                txtMinDeliveryQuantity.Text = ObjSales.Min_Delivery_Quan_Delivery_Note;
                txtMinMOrderQuantity.Text = ObjSales.Min_Make_To_Order_Quan;
                txtDeliveryUnit.Text = ObjSales.Delivery_Unit;
                ddlUnitMeasureDelivery.SelectedValue = ObjSales.Unit_Measure_Delivery_Unit;
                chkCashDiscountIndi.Checked = ObjSales.Is_Cash_Discount.ToLower() == "true" ? true : false;
                chkVariSalesUnitNotAllowed.Checked = ObjSales.Vari_Sales_Unit_Not_Allowed.ToLower() == "true" ? true : false;
                txtSalesText.Text = ObjSales.Sales_Text;
            }
            else
            {
                lblSalesId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S1','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
            }

            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillSalesData", ex);
        }
    }
   
    private Sales1 GetControlsValue()
    {
        Sales1 ObjSales = new Sales1();
        try
        {

            ObjSales.Mat_Sales1_Id = Convert.ToInt32(lblSalesId.Text);
            ObjSales.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjSales.Plant_Id = ddlPlant.SelectedValue;
            ObjSales.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjSales.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;

            Session[StaticKeys.MaterialSalesOrgId] = ddlSalesOrginization.SelectedValue;
            Session[StaticKeys.MaterialDistChnlId] = ddlDistributionChannel.SelectedValue;

            ObjSales.Sales_Unit = ddlSalesUnit.SelectedValue;
            ObjSales.Distri_Chain_Speci_Mat_Status = txtDistributionCSpecMStatus.Text;
            ObjSales.Mat_Status_Sales_Valid_From = objUtil.GetYYYYMMDD(txtMaterialSSaleValid.Text);
            ObjSales.Delivery_Plant = ddlDeliveringPlant.SelectedValue;
            ObjSales.Min_Order_Quan_Base_Unit = txtMinOrderQBaseUnitM.Text;
            ObjSales.Min_Delivery_Quan_Delivery_Note = txtMinDeliveryQuantity.Text;
            ObjSales.Min_Make_To_Order_Quan = txtMinMOrderQuantity.Text;
            ObjSales.Delivery_Unit = txtDeliveryUnit.Text;
            ObjSales.Unit_Measure_Delivery_Unit = ddlUnitMeasureDelivery.SelectedValue;
            ObjSales.Is_Cash_Discount = chkCashDiscountIndi.Checked == true ? "1" : "0";
            ObjSales.Vari_Sales_Unit_Not_Allowed = chkVariSalesUnitNotAllowed.Checked == true ? "1" : "0";
            ObjSales.Sales_Text = txtSalesText.Text;

            ObjSales.IsActive = 1;
            ObjSales.UserId = lblUserId.Text;
            ObjSales.TodayDate = objUtil.GetDate();
            ObjSales.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }

        return ObjSales;
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Sales1 obj = new SectionConfiguration.Sales1();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Sales1 NewSales1Data, Sales1 oldSales1Data)
    {
        try
        {
            if (NewSales1Data.Mat_Sales1_Id > 0 && oldSales1Data.Mat_Sales1_Id > 0)
            {
                if (NewSales1Data.Plant_Id != oldSales1Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldSales1Data.Plant_Id + "</td><td>" + NewSales1Data.Plant_Id + "</td></tr>";
                if (NewSales1Data.Sales_Organization_Id != oldSales1Data.Sales_Organization_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Organization Id</td> <td>" + oldSales1Data.Sales_Organization_Id + "</td><td>" + NewSales1Data.Sales_Organization_Id + "</td></tr>";
                if (NewSales1Data.Distribution_Channel_ID != oldSales1Data.Distribution_Channel_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distribution Channel ID</td> <td>" + oldSales1Data.Distribution_Channel_ID + "</td><td>" + NewSales1Data.Distribution_Channel_ID + "</td></tr>";
                if (NewSales1Data.Sales_Unit != oldSales1Data.Sales_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Unit</td> <td>" + oldSales1Data.Sales_Unit + "</td><td>" + NewSales1Data.Sales_Unit + "</td></tr>";
                if (NewSales1Data.Distri_Chain_Speci_Mat_Status != oldSales1Data.Distri_Chain_Speci_Mat_Status)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distri Chain Speci Mat Status</td> <td>" + oldSales1Data.Distri_Chain_Speci_Mat_Status + "</td><td>" + NewSales1Data.Distri_Chain_Speci_Mat_Status + "</td></tr>";
                if (NewSales1Data.Mat_Status_Sales_Valid_From != oldSales1Data.Mat_Status_Sales_Valid_From)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mat Status Sales Valid From</td> <td>" + oldSales1Data.Mat_Status_Sales_Valid_From + "</td><td>" + NewSales1Data.Mat_Status_Sales_Valid_From + "</td></tr>";
                if (NewSales1Data.Delivery_Plant != oldSales1Data.Delivery_Plant)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Delivery Plant</td> <td>" + oldSales1Data.Delivery_Plant + "</td><td>" + NewSales1Data.Delivery_Plant + "</td></tr>";
                if (NewSales1Data.Min_Order_Quan_Base_Unit != oldSales1Data.Min_Order_Quan_Base_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Order Quan Base Unit</td> <td>" + oldSales1Data.Min_Order_Quan_Base_Unit + "</td><td>" + NewSales1Data.Min_Order_Quan_Base_Unit + "</td></tr>";
                if (NewSales1Data.Min_Delivery_Quan_Delivery_Note != oldSales1Data.Min_Delivery_Quan_Delivery_Note)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Delivery Quan Delivery Note</td> <td>" + oldSales1Data.Min_Delivery_Quan_Delivery_Note + "</td><td>" + NewSales1Data.Min_Delivery_Quan_Delivery_Note + "</td></tr>";
                if (NewSales1Data.Min_Make_To_Order_Quan != oldSales1Data.Min_Make_To_Order_Quan)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Make To Order Quan</td> <td>" + oldSales1Data.Min_Make_To_Order_Quan + "</td><td>" + NewSales1Data.Min_Make_To_Order_Quan + "</td></tr>";
                if (NewSales1Data.Delivery_Unit != oldSales1Data.Delivery_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Delivery Unit</td> <td>" + oldSales1Data.Delivery_Unit + "</td><td>" + NewSales1Data.Delivery_Unit + "</td></tr>";
                if (NewSales1Data.Unit_Measure_Delivery_Unit != oldSales1Data.Unit_Measure_Delivery_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Measure Delivery Unit</td> <td>" + oldSales1Data.Unit_Measure_Delivery_Unit + "</td><td>" + NewSales1Data.Unit_Measure_Delivery_Unit + "</td></tr>";
                if (NewSales1Data.Is_Cash_Discount != (oldSales1Data.Is_Cash_Discount.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Is Cash Discount</td> <td>" + (oldSales1Data.Is_Cash_Discount.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewSales1Data.Is_Cash_Discount + "</td></tr>";
                if (NewSales1Data.Vari_Sales_Unit_Not_Allowed != (oldSales1Data.Vari_Sales_Unit_Not_Allowed.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Vari Sales Unit Not Allowed</td> <td>" + oldSales1Data.Vari_Sales_Unit_Not_Allowed + "</td><td>" + NewSales1Data.Vari_Sales_Unit_Not_Allowed + "</td></tr>";
                if (NewSales1Data.Sales_Text != oldSales1Data.Sales_Text)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Text</td> <td>" + oldSales1Data.Sales_Text + "</td><td>" + NewSales1Data.Sales_Text + "</td></tr>";
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
    private void CheckIfChangesLog(Sales1 NewSales1Data, Sales1 oldSales1Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewSales1Data.Mat_Sales1_Id > 0 && oldSales1Data.Mat_Sales1_Id > 0)
            {
                if (NewSales1Data.Sales_Unit != oldSales1Data.Sales_Unit)
                {
                    WriteMatChangeLog("MatChangeLogS1" + sdate + ".txt", "157"+ NewSales1Data.Sales_Unit +'-'+ oldSales1Data.Sales_Unit);
                    _items.Add(new SMChange { colFieldName = 157, colOldVal = oldSales1Data.Sales_Unit, colNewVal = NewSales1Data.Sales_Unit });
                }
                if (NewSales1Data.Distri_Chain_Speci_Mat_Status != oldSales1Data.Distri_Chain_Speci_Mat_Status)
                    _items.Add(new SMChange { colFieldName = 158, colOldVal = oldSales1Data.Distri_Chain_Speci_Mat_Status, colNewVal = NewSales1Data.Distri_Chain_Speci_Mat_Status });
                //if (NewSales1Data.Mat_Status_Sales_Valid_From != oldSales1Data.Mat_Status_Sales_Valid_From)
               
                if (NewSales1Data.Delivery_Plant != oldSales1Data.Delivery_Plant)
                    _items.Add(new SMChange { colFieldName = 160, colOldVal = oldSales1Data.Delivery_Plant, colNewVal = NewSales1Data.Delivery_Plant });
                if (NewSales1Data.Min_Order_Quan_Base_Unit != oldSales1Data.Min_Order_Quan_Base_Unit)
                    _items.Add(new SMChange { colFieldName = 161, colOldVal = oldSales1Data.Min_Order_Quan_Base_Unit, colNewVal = NewSales1Data.Min_Order_Quan_Base_Unit });
                if (NewSales1Data.Min_Delivery_Quan_Delivery_Note != oldSales1Data.Min_Delivery_Quan_Delivery_Note)
                    _items.Add(new SMChange { colFieldName = 162, colOldVal = oldSales1Data.Min_Delivery_Quan_Delivery_Note, colNewVal = NewSales1Data.Min_Delivery_Quan_Delivery_Note });
                if (NewSales1Data.Min_Make_To_Order_Quan != oldSales1Data.Min_Make_To_Order_Quan)
                    _items.Add(new SMChange { colFieldName = 163, colOldVal = oldSales1Data.Min_Make_To_Order_Quan, colNewVal = NewSales1Data.Min_Make_To_Order_Quan });
                if (NewSales1Data.Delivery_Unit != oldSales1Data.Delivery_Unit)
                    _items.Add(new SMChange { colFieldName = 164, colOldVal = oldSales1Data.Delivery_Unit, colNewVal = NewSales1Data.Delivery_Unit });
                if (NewSales1Data.Unit_Measure_Delivery_Unit != oldSales1Data.Unit_Measure_Delivery_Unit)
                    _items.Add(new SMChange { colFieldName = 165, colOldVal = oldSales1Data.Unit_Measure_Delivery_Unit, colNewVal = NewSales1Data.Unit_Measure_Delivery_Unit });
                if (NewSales1Data.Is_Cash_Discount != (oldSales1Data.Is_Cash_Discount.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 166, colOldVal = (oldSales1Data.Is_Cash_Discount.ToLower() == "true" ? "X" : ""), colNewVal = (NewSales1Data.Is_Cash_Discount.ToLower() == "1" ? "X" : "0") });
                if (NewSales1Data.Vari_Sales_Unit_Not_Allowed != (oldSales1Data.Vari_Sales_Unit_Not_Allowed.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 1235, colOldVal = (oldSales1Data.Vari_Sales_Unit_Not_Allowed.ToLower() == "true" ? "X" : ""), colNewVal = (NewSales1Data.Vari_Sales_Unit_Not_Allowed.ToLower() == "1" ? "X" : "") });
                if (NewSales1Data.Sales_Text != oldSales1Data.Sales_Text)
                    _items.Add(new SMChange { colFieldName = 1208, colOldVal = oldSales1Data.Sales_Text, colNewVal = NewSales1Data.Sales_Text });
                if ((objUtil.GetDDMMYYYYNew(NewSales1Data.Mat_Status_Sales_Valid_From) != oldSales1Data.Mat_Status_Sales_Valid_From)
                 && (objUtil.GetDDMMYYYYNew(NewSales1Data.Mat_Status_Sales_Valid_From) != "01/01/1900")
                 && (NewSales1Data.Mat_Status_Sales_Valid_From != "1900-01-01")
                     && (oldSales1Data.Mat_Status_Sales_Valid_From != "01/01/1900") && (oldSales1Data.Mat_Status_Sales_Valid_From != "1900-01-01")
                     )
                {
                    _items.Add(new SMChange { colFieldName = 159, colOldVal = oldSales1Data.Mat_Status_Sales_Valid_From, colNewVal = objUtil.GetDDMMYYYYNew(NewSales1Data.Mat_Status_Sales_Valid_From) });
                }

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
                ChangeSMatID1 = helperAccess.MaterialChange("15", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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

    protected void ddlSalesUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        //Promotion code start
        //if (lblModuleId.Text == "145" || lblModuleId.Text == "139" || lblModuleId.Text == "171")
        if (lblModuleId.Text == "145" || lblModuleId.Text == "139" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
        //Promotion code End
        {
            if (DepotPlants.Contains(ddlPlant.SelectedValue.ToString()))
            {
                if (ddlSalesUnit.SelectedValue != "")
                {
                    chkVariSalesUnitNotAllowed.Checked = true;
                    txtMinDeliveryQuantity.Text = "1";
                    txtMinOrderQBaseUnitM.Text = "1";
                    txtDeliveryUnit.Text = "1";
                }
                else
                {
                    chkVariSalesUnitNotAllowed.Checked = false;
                    txtMinDeliveryQuantity.Text = "";
                    txtMinOrderQBaseUnitM.Text = "";
                    txtDeliveryUnit.Text = "";
                }

            }
        }

        }
        catch (Exception ex)
        { _log.Error("ddlSalesUnit_SelectedIndexChanged", ex); }
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