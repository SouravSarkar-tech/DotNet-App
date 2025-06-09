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
public partial class Transaction_SalesPlant : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    SalesAccess ObjSalesAccess = new SalesAccess();
    HelperAccess helperAccess = new HelperAccess();
    Utility objUtil = new Utility();
    Sales3 objSavedSales3 = new Sales3();

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
                            //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                            if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = true;
                            }
                            //MSC_8300001775 End 
                        }

                        //FillDataGrid();
                        ClearData();
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
            ddlTransportationGroup.Enabled = true;
            ddlLoadingGroup.Enabled = true;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblSalesPlantId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_Sales3_Id"].ToString();
            FillSalesPlantData();
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
                //lblMsg.Text = Messages.GetMessage(1);
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("SalesPlant.aspx");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    protected void ClearData()
    {
        try
        {
            lblSalesPlantId.Text = "0";

            txtShippingSTime.Text = "";
            txtBaseQuantCapacity.Text = "";
            txtShippingProcessingTime.Text = "";

            PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
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

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPlantWiseDropDown();
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }
    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }


    private void PopuplateDropDownList()
    {
        try
        {

            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

            helperAccess.PopuplateDropDownList(ddlLoadingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlLoadingGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlTransportationGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlTransportationGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlAvailabilityCheck, "pr_GetDropDownListByControlNameModuleType 'M','ddlAvailabilityCheck'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','0','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    protected bool Save()
    {
        bool flg = false;
        try
        {
            Sales3 ObjSales = GetControlValue();
            objSavedSales3 = GetSalesData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedSales3.Mat_Sales3_Id > 0)
                {
                    CheckIfChanges(ObjSales, objSavedSales3);
                }
            }

            if (ObjSalesAccess.Save(ObjSales) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjSales, objSavedSales3);
                }
                //MSC_8300001775   

                //FillDataGrid();
                ClearData();
                FillFormDataByMHId();
                lblMsg.Text = Messages.GetMessage(1);
                flg = true;

                ////MSC_8300001775
                //                if (HelperAccess.ReqType == "SMC")
                //                {
                //                    CheckIfChangesLog(ObjSales, objSavedSales3);
                //                }
                //                //MSC_8300001775     
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
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


    void FillDataGrid()
    {
        try
        {
            DataSet ds = ObjSalesAccess.GetSalesData3(Convert.ToInt32(lblMasterHeaderId.Text));
            gvData.DataSource = ds;
            gvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDataGrid", ex); }
    }

    private Sales3 GetSalesData()
    {
        return ObjSalesAccess.GetSales3(Convert.ToInt32(lblSalesPlantId.Text));
    }

    private void FillFormDataByMHId()
    {
        try
        {
            DataSet ds;
            ds = ObjSalesAccess.GetSalesData3(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSalesPlantId.Text = ds.Tables[0].Rows[0]["Mat_Sales3_Id"].ToString();
            }
            FillSalesPlantData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesPlantId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','0','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillSalesPlantData()
    {
        try
        {
            Sales3 ObjSales = GetSalesData();

            if (ObjSales.Mat_Sales3_Id > 0)
            {
                lblSalesPlantId.Text = ObjSales.Mat_Sales3_Id.ToString();
                lblMasterHeaderId.Text = ObjSales.Master_Header_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjSales.Plant_Id;
                BindPlantWiseDropDown();


                //ddlSalesOrginization.SelectedValue = ObjSales.Sales_Organization_Id;
                //helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesPlantId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                //ddlDistributionChannel.SelectedValue = ObjSales.Distribution_Channel_ID;

                if (Session[StaticKeys.MaterialSalesOrgId] != null)
                {
                    ddlSalesOrginization.SelectedValue = Session[StaticKeys.MaterialSalesOrgId].ToString();
                    helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesPlantId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                    if (Session[StaticKeys.MaterialDistChnlId] != null)
                        ddlDistributionChannel.SelectedValue = Session[StaticKeys.MaterialDistChnlId].ToString();
                }

                ddlTransportationGroup.SelectedValue = ObjSales.Transportation_Group;
                txtShippingSTime.Text = ObjSales.Shipping_SetUp_Time;
                txtBaseQuantCapacity.Text = ObjSales.Base_Quan_Capital_Plan;
                txtShippingProcessingTime.Text = ObjSales.Shipping_Processing_Time;
                ddlLoadingGroup.SelectedValue = ObjSales.Loading_Group;
                ddlAvailabilityCheck.SelectedValue = ObjSales.Availability_Check;
                chkBatchMgmt.Checked = ObjSales.Batch_Mgmt.ToLower() == "true" ? true : false;
                ddlProfitCenter.SelectedValue = ObjSales.Profit_Center;
            }
            else
            {
                lblSalesPlantId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S3','" + lblSalesPlantId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();

                if (Session[StaticKeys.MaterialSalesOrgId] != null)
                {
                    ddlSalesOrginization.SelectedValue = Session[StaticKeys.MaterialSalesOrgId].ToString();
                    helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesPlantId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                    if (Session[StaticKeys.MaterialDistChnlId] != null)
                        ddlDistributionChannel.SelectedValue = Session[StaticKeys.MaterialDistChnlId].ToString();
                }
                //Promotion code start
                //if (lblModuleId.Text == "162" || lblModuleId.Text == "164" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                if (lblModuleId.Text == "162" || lblModuleId.Text == "164" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                {
                    //ddlAvailabilityCheck.SelectedValue = "02";
                    ddlTransportationGroup.SelectedValue = "Z001";
                    ddlLoadingGroup.SelectedValue = "0001";
                    //chkBatchMgmt.Checked = true;
                }
            }

            ddlPlant.Enabled = false;
            ddlSalesOrginization.Enabled = false;
            ddlDistributionChannel.Enabled = false;


            if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
            {
                ddlAvailabilityCheck.Enabled = false;

                ddlTransportationGroup.Enabled = false;
                ddlLoadingGroup.Enabled = false;
            }

        }
        catch (Exception ex)
        { _log.Error("FillSalesPlantData", ex); }
    }

    private Sales3 GetControlValue()
    {
        Sales3 ObjSales = new Sales3();
        try
        {
            ObjSales.Mat_Sales3_Id = Convert.ToInt32(lblSalesPlantId.Text);
            ObjSales.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjSales.Plant_Id = ddlPlant.SelectedValue;
            ObjSales.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjSales.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;

            ObjSales.Transportation_Group = ddlTransportationGroup.SelectedValue;
            ObjSales.Shipping_SetUp_Time = txtShippingSTime.Text;
            ObjSales.Base_Quan_Capital_Plan = txtBaseQuantCapacity.Text;
            ObjSales.Shipping_Processing_Time = txtShippingProcessingTime.Text;
            ObjSales.Loading_Group = ddlLoadingGroup.SelectedValue;
            ObjSales.Availability_Check = ddlAvailabilityCheck.SelectedValue;
            ObjSales.Batch_Mgmt = chkBatchMgmt.Checked == true ? "1" : "0";
            ObjSales.Profit_Center = ddlProfitCenter.SelectedValue;

            ObjSales.IsActive = 1;
            ObjSales.UserId = lblUserId.Text;
            ObjSales.TodayDate = objUtil.GetDate();
            ObjSales.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlValue", ex);
        }
        return ObjSales;
    }

    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.Sales_Plant obj = new SectionConfiguration.Sales_Plant();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Sales3 NewSales3Data, Sales3 oldSales3Data)
    {
        try
        {
            if (NewSales3Data.Mat_Sales3_Id > 0 && oldSales3Data.Mat_Sales3_Id > 0)
            {
                if (NewSales3Data.Plant_Id != oldSales3Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldSales3Data.Plant_Id + "</td><td>" + NewSales3Data.Plant_Id + "</td></tr>";
                if (NewSales3Data.Sales_Organization_Id != oldSales3Data.Sales_Organization_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Organization Id</td> <td>" + oldSales3Data.Sales_Organization_Id + "</td><td>" + NewSales3Data.Sales_Organization_Id + "</td></tr>";
                if (NewSales3Data.Distribution_Channel_ID != oldSales3Data.Distribution_Channel_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distribution Channel ID</td> <td>" + oldSales3Data.Distribution_Channel_ID + "</td><td>" + NewSales3Data.Distribution_Channel_ID + "</td></tr>";
                if (NewSales3Data.Transportation_Group != oldSales3Data.Transportation_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Transportation Group</td> <td>" + oldSales3Data.Transportation_Group + "</td><td>" + NewSales3Data.Transportation_Group + "</td></tr>";
                if (NewSales3Data.Shipping_SetUp_Time != oldSales3Data.Shipping_SetUp_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Shipping SetUp Time</td> <td>" + oldSales3Data.Shipping_SetUp_Time + "</td><td>" + NewSales3Data.Shipping_SetUp_Time + "</td></tr>";
                if (NewSales3Data.Base_Quan_Capital_Plan != oldSales3Data.Base_Quan_Capital_Plan)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Base Quan Capital Plan</td> <td>" + oldSales3Data.Base_Quan_Capital_Plan + "</td><td>" + NewSales3Data.Base_Quan_Capital_Plan + "</td></tr>";
                if (NewSales3Data.Shipping_Processing_Time != oldSales3Data.Shipping_Processing_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Shipping Processing Time</td> <td>" + oldSales3Data.Shipping_Processing_Time + "</td><td>" + NewSales3Data.Shipping_Processing_Time + "</td></tr>";
                if (NewSales3Data.Loading_Group != oldSales3Data.Loading_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Group</td> <td>" + oldSales3Data.Loading_Group + "</td><td>" + NewSales3Data.Loading_Group + "</td></tr>";
                if (NewSales3Data.Availability_Check != oldSales3Data.Availability_Check)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Availability Check</td> <td>" + oldSales3Data.Availability_Check + "</td><td>" + NewSales3Data.Availability_Check + "</td></tr>";
                if (NewSales3Data.Batch_Mgmt != (oldSales3Data.Batch_Mgmt.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Batch Mgmt</td> <td>" + (oldSales3Data.Batch_Mgmt.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewSales3Data.Batch_Mgmt + "</td></tr>";
                if (NewSales3Data.Profit_Center != oldSales3Data.Profit_Center)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Profit Center</td> <td>" + oldSales3Data.Profit_Center + "</td><td>" + NewSales3Data.Profit_Center + "</td></tr>";
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
    private void CheckIfChangesLog(Sales3 NewSales3Data, Sales3 oldSales3Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewSales3Data.Mat_Sales3_Id > 0 && oldSales3Data.Mat_Sales3_Id > 0)
            {
                if (NewSales3Data.Transportation_Group != oldSales3Data.Transportation_Group)
                    _items.Add(new SMChange { colFieldName = 182, colOldVal = oldSales3Data.Transportation_Group, colNewVal = NewSales3Data.Transportation_Group });
                if (NewSales3Data.Shipping_SetUp_Time != oldSales3Data.Shipping_SetUp_Time)
                    _items.Add(new SMChange { colFieldName = 183, colOldVal = oldSales3Data.Shipping_SetUp_Time, colNewVal = NewSales3Data.Shipping_SetUp_Time });
                if (NewSales3Data.Base_Quan_Capital_Plan != oldSales3Data.Base_Quan_Capital_Plan)
                    _items.Add(new SMChange { colFieldName = 184, colOldVal = oldSales3Data.Base_Quan_Capital_Plan, colNewVal = NewSales3Data.Base_Quan_Capital_Plan });
                if (NewSales3Data.Shipping_Processing_Time != oldSales3Data.Shipping_Processing_Time)
                    _items.Add(new SMChange { colFieldName = 185, colOldVal = oldSales3Data.Shipping_Processing_Time, colNewVal = NewSales3Data.Shipping_Processing_Time });
                if (NewSales3Data.Loading_Group != oldSales3Data.Loading_Group)
                    _items.Add(new SMChange { colFieldName = 186, colOldVal = oldSales3Data.Loading_Group, colNewVal = NewSales3Data.Loading_Group });
                if (NewSales3Data.Availability_Check != oldSales3Data.Availability_Check)
                    _items.Add(new SMChange { colFieldName = 1215, colOldVal = oldSales3Data.Availability_Check, colNewVal = NewSales3Data.Availability_Check });
                if (NewSales3Data.Batch_Mgmt != (oldSales3Data.Batch_Mgmt.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 1214, colOldVal = (oldSales3Data.Batch_Mgmt.ToLower() == "true" ? "X" : ""), colNewVal = (NewSales3Data.Batch_Mgmt.ToLower() == "1" ? "X" : "") });
                if (NewSales3Data.Profit_Center != oldSales3Data.Profit_Center)
                    _items.Add(new SMChange { colFieldName = 1213, colOldVal = oldSales3Data.Profit_Center, colNewVal = NewSales3Data.Profit_Center });
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
                ChangeSMatID1 = helperAccess.MaterialChange("17", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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