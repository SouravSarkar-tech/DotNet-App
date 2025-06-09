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
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_Sales2 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    SalesAccess ObjSalesAccess = new SalesAccess();
    HelperAccess helperAccess = new HelperAccess();
    Utility objUtil = new Utility();
    Sales2 objSavedSales2 = new Sales2();
    string sdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogS2" + sdate + ".txt", "Start" + HelperAccess.ReqType);
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load1", ex);
        }
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

                        //PopuplateDropDownList();


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
            lableddlMaterialGroup1.Visible = false;
            reqddlMaterialGroup1.Visible = false;
            ddlMaterialGroup1.Enabled = true;
            //txtMinDeliveryQuantity.CssClass = "txtbox";

            lableddlMaterialGroup2.Visible = false;
            reqddlMaterialGroup2.Visible = false;
            ddlMaterialGroup2.Enabled = true;

            lableddlMaterialGroup3.Visible = false;
            reqddlMaterialGroup3.Visible = false;
            ddlMaterialGroup3.Enabled = true;

            lableddlMaterialGroup4.Visible = false;
            reqddlMaterialGroup4.Visible = false;
            ddlMaterialGroup4.Enabled = true;

            lableddlMaterialGroup5.Visible = false;
            reqddlMaterialGroup5.Visible = false;
            ddlMaterialGroup5.Enabled = true;

            lableddlMaterialPGroup.Visible = false;
            reqddlMaterialPGroup.Visible = false;
            ddlMaterialPGroup.Enabled = true;

            lableddlAccountAssignment.Visible = false;
            reqddlAccountAssignment.Visible = false;
            ddlAccountAssignment.Enabled = true;

            lableddlGenItemCategoryGrp.Visible = true;
            reqddlGenItemCategoryGrp.Visible = true;
            ddlGenItemCategoryGrp.Enabled = true;

            ddlAccountAssignment.Enabled = true;
            ddlProductHierarchy1.Enabled = true;
            ddlItemCategoryGroup.Enabled = true;
            reqddlPricingRefMaterial.Visible = false;

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
            lblSalesId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_Sales2_Id"].ToString();
            FillSalesData();
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
                if ((lblModuleId.Text == "162" || lblModuleId.Text == "164") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                {
                    Response.Redirect("Classification.aspx");
                }
                else if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                {
                    Response.Redirect("BasicData1.aspx");
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
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("Sales2.aspx");
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
                //    Response.Redirect("Purchasing.aspx");
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

    protected bool Save()
    {
        bool flg = false;
        try
        {
            Sales2 ObjSales = GetControlValue();
            objSavedSales2 = GetSalesData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedSales2.Mat_Sales2_Id > 0)
                {
                    CheckIfChanges(ObjSales, objSavedSales2);
                }
            }

            if (ObjSalesAccess.Save(ObjSales) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjSales, objSavedSales2);
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
                //                    CheckIfChangesLog(ObjSales, objSavedSales2);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
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
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblSalesId.Text = "0";
            PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");

            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

            helperAccess.PopuplateDropDownList(ddlAccountAssignment, "pr_GetDropDownListByControlNameModuleType 'M','ddlAccountAssignment'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCasNumPharmaceutical, "pr_GetDropDownListByControlNameModuleType 'M','ddlCasNumPharmaceutical'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCommisionGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlCommisionGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCrossDistribution, "pr_GetDropDownListByControlNameModuleType 'M','ddlCrossDistribution'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlGenItemCategoryGrp, "pr_GetDropDownListByControlNameModuleType 'M','ddlItemCategoryGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlItemCategoryGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlItemCategoryGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialGroup1, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup1'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialGroup2, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup2'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialGroup3, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup3'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialGroup4, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup4'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialGroup5, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup5'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialPGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialPGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialStatisticsG, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialStatisticsG'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPricingRefMaterial, "pr_GetDropDownListByControlNameModuleType 'M','ddlPricingRefMaterial'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlProductHierarchy, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy1, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','1'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlValumeRGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlValumeRGroup'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds = ObjSalesAccess.GetSalesData2(Convert.ToInt32(lblMasterHeaderId.Text));
            gvData.DataSource = ds;
            gvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDataGrid", ex); }
    }

    private Sales2 GetSalesData()
    {
        return ObjSalesAccess.GetSales2(Convert.ToInt32(lblSalesId.Text));
    }

    private void FillFormDataByMHId()
    {
        try
        {
            DataSet ds;
            ds = ObjSalesAccess.GetSalesData2(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSalesId.Text = ds.Tables[0].Rows[0]["Mat_Sales2_Id"].ToString();
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
            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillSalesData()
    {
        try
        {
            Sales2 ObjSales = GetSalesData();

            if (ObjSales.Mat_Sales2_Id > 0)
            {
                lblSalesId.Text = ObjSales.Mat_Sales2_Id.ToString();
                lblMasterHeaderId.Text = ObjSales.Master_Header_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = ObjSales.Plant_Id;
                BindPlantWiseDropDown();


                //ddlSalesOrginization.SelectedValue = ObjSales.Sales_Organization_Id;
                //helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                //ddlDistributionChannel.SelectedValue = ObjSales.Distribution_Channel_ID;

                if (Session[StaticKeys.MaterialSalesOrgId] != null)
                {
                    ddlSalesOrginization.SelectedValue = Session[StaticKeys.MaterialSalesOrgId].ToString();
                    helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                    if (Session[StaticKeys.MaterialDistChnlId] != null)
                        ddlDistributionChannel.SelectedValue = Session[StaticKeys.MaterialDistChnlId].ToString();
                }



                ddlMaterialStatisticsG.SelectedValue = ObjSales.Mat_Statistic_Group;
                ddlValumeRGroup.SelectedValue = ObjSales.Volume_Rebate_Group;
                ddlCommisionGroup.SelectedValue = ObjSales.Commission_Group;
                ddlGenItemCategoryGrp.SelectedValue = ObjSales.Gen_Item_Category_Grp;
                ddlItemCategoryGroup.SelectedValue = ObjSales.Item_Category_Grp;
                //ddlProductHierarchy.SelectedValue = ObjSales.Product_Hierarchy;



                foreach (ListItem lst in ddlProductHierarchy1.Items)
                {
                    if (lst.Value.Contains(ObjSales.Product_Hierarchy.Length > 4 ? ObjSales.Product_Hierarchy.Substring(0, 5) : ObjSales.Product_Hierarchy))
                    {
                        ddlProductHierarchy1.SelectedValue = lst.Value;
                        break;
                    }
                }

                ProductHierarchySetUp();

                helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                foreach (ListItem lst in ddlProductHierarchy2.Items)
                {
                    if (lst.Value.Contains(ObjSales.Product_Hierarchy.Length > 5 ? (ObjSales.Product_Hierarchy.Length > 9 ? ObjSales.Product_Hierarchy.Substring(0, 10) : ObjSales.Product_Hierarchy.Substring(0, ObjSales.Product_Hierarchy.Length)) : ""))
                    {
                        ddlProductHierarchy2.SelectedValue = lst.Value;
                        break;
                    }
                }

                //ddlProductHierarchy2.SelectedValue = ObjBasicData.Product_Hierarchy.Length > 5 ? (ObjBasicData.Product_Hierarchy.Length > 9 ? ObjBasicData.Product_Hierarchy.Substring(0, 10) : ObjBasicData.Product_Hierarchy.Substring(0, ObjBasicData.Product_Hierarchy.Length)) : "";

                helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                foreach (ListItem lst in ddlProductHierarchy3.Items)
                {
                    if (lst.Value.Contains(ObjSales.Product_Hierarchy.Length > 10 ? ObjSales.Product_Hierarchy : ""))
                    {
                        ddlProductHierarchy3.SelectedValue = lst.Value;
                        break;
                    }
                }

                ddlPricingRefMaterial.SelectedValue = ObjSales.Pricing_Ref_Mat;
                ddlMaterialPGroup.SelectedValue = ObjSales.Mat_Pricing_Group;
                ddlAccountAssignment.SelectedValue = ObjSales.Acc_Assignment_Grp;
                ddlMaterialGroup1.SelectedValue = ObjSales.Material_Group1;
                ddlMaterialGroup2.SelectedValue = ObjSales.Material_Group2;
                ddlMaterialGroup3.SelectedValue = ObjSales.Material_Group3;
                ddlMaterialGroup4.SelectedValue = ObjSales.Material_Group4;
                ddlMaterialGroup5.SelectedValue = ObjSales.Material_Group5;
                ddlCrossDistribution.SelectedValue = ObjSales.Cross_Distrib_Chain_Mat_Status;
                ddlCasNumPharmaceutical.SelectedValue = ObjSales.CAS_No_Pharma_Prod_FT;
            }
            else
            {
                lblSalesId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();

                if (Session[StaticKeys.MaterialSalesOrgId] != null)
                {
                    ddlSalesOrginization.SelectedValue = Session[StaticKeys.MaterialSalesOrgId].ToString();
                    helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S2','" + lblSalesId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                    if (Session[StaticKeys.MaterialDistChnlId] != null)
                        ddlDistributionChannel.SelectedValue = Session[StaticKeys.MaterialDistChnlId].ToString();
                }

                if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
                {
                    ddlMaterialStatisticsG.SelectedValue = "1";
                    ddlAccountAssignment.SelectedValue = "Z5";
                    if (lblModuleId.Text == "164")
                        //ddlItemCategoryGroup.SelectedValue = "VERP";
                        ddlItemCategoryGroup.SelectedValue = "NORM";
                    else if (lblModuleId.Text == "162")
                        ddlItemCategoryGroup.SelectedValue = "NORM";

                    ddlItemCategoryGroup.Enabled = false;
                }
                //Srinidhi
                //Promotion code start
                //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                {
                    ddlMaterialStatisticsG.SelectedValue = "1";

                    ddlItemCategoryGroup.SelectedValue = "NORM";
                    ddlGenItemCategoryGrp.SelectedValue = "NORM";

                    if (ddlSalesOrginization.SelectedValue == "2" || ddlSalesOrginization.SelectedValue == "25"
                            || ddlSalesOrginization.SelectedValue == "27"
                                || ddlSalesOrginization.SelectedValue == "28")
                    {
                        //MSC_8300001775 Start Comment 
                        //ddlProductHierarchy1.SelectedValue = "19998             ";
                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        ddlProductHierarchy1.SelectedValue = "19998";
                        //MSC_8300001775 End
                        helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                        //MSC_8300001775 Start Comment 
                        //ddlProductHierarchy2.SelectedValue = "1999829903        ";
                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        ddlProductHierarchy2.SelectedValue = "1999829903";
                        //MSC_8300001775 End
                        helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                        ddlProductHierarchy3.SelectedValue = "199982990339999904";

                    }
                    else if (ddlSalesOrginization.SelectedValue == "3")
                    {
                        //MSC_8300001775 Start Comment 
                        //ddlProductHierarchy1.SelectedValue = "19999             ";
                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        ddlProductHierarchy1.SelectedValue = "19999";
                        //MSC_8300001775 End
                        helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                        //MSC_8300001775 Start Comment 
                        //ddlProductHierarchy2.SelectedValue = "1999929903        ";
                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        ddlProductHierarchy2.SelectedValue = "1999929903";
                        //MSC_8300001775 End
                        helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                        ddlProductHierarchy3.SelectedValue = "199992990339999904";
                    }
                    ProductHierarchySetUp();
                    if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "171")
                    {
                        if (Session[StaticKeys.SelectedDivision] != null)
                        {
                            if (Session[StaticKeys.SelectedDivision].ToString() == "30")
                                ddlAccountAssignment.SelectedValue = "Z3";
                            else
                                ddlAccountAssignment.SelectedValue = "Z1";
                        }
                        else
                            ddlAccountAssignment.SelectedValue = "Z3";

                    }
                    if (lblModuleId.Text == "145")
                    {
                        ddlAccountAssignment.SelectedValue = "Z2";
                    }
                    //Promotion code start
                    if (lblModuleId.Text == "195")
                    {
                        ddlAccountAssignment.SelectedValue = "Z7";
                    }
                    //Promotion code End
                }
            }

            ddlPlant.Enabled = false;
            ddlSalesOrginization.Enabled = false;
            ddlDistributionChannel.Enabled = false;

            if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
            {
                ddlMaterialStatisticsG.Enabled = false;
                ddlAccountAssignment.Enabled = false;
            }
            //Promotion code start
            //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
            if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
            //Promotion code End
            {
                ddlMaterialStatisticsG.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("FillSalesData", ex); }
    }

    private Sales2 GetControlValue()
    {
        Sales2 ObjSales = new Sales2();
        try
        {
            ObjSales.Mat_Sales2_Id = Convert.ToInt32(lblSalesId.Text);
            ObjSales.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjSales.Plant_Id = ddlPlant.SelectedValue;
            ObjSales.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjSales.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;
            ObjSales.Mat_Statistic_Group = ddlMaterialStatisticsG.SelectedValue;
            ObjSales.Volume_Rebate_Group = ddlValumeRGroup.SelectedValue;
            ObjSales.Commission_Group = ddlCommisionGroup.SelectedValue;
            ObjSales.Gen_Item_Category_Grp = ddlGenItemCategoryGrp.SelectedValue;
            ObjSales.Item_Category_Grp = ddlItemCategoryGroup.SelectedValue;
            //ObjSales.Product_Hierarchy = ddlProductHierarchy.SelectedValue;
            ObjSales.Product_Hierarchy = ddlProductHierarchy3.SelectedValue != "" ? ddlProductHierarchy3.SelectedValue : (ddlProductHierarchy2.SelectedValue != "" ? ddlProductHierarchy2.SelectedValue : ddlProductHierarchy1.SelectedValue);
            ObjSales.Pricing_Ref_Mat = ddlPricingRefMaterial.SelectedValue;
            ObjSales.Mat_Pricing_Group = ddlMaterialPGroup.SelectedValue;
            ObjSales.Acc_Assignment_Grp = ddlAccountAssignment.SelectedValue;
            ObjSales.Material_Group1 = ddlMaterialGroup1.SelectedValue;
            ObjSales.Material_Group2 = ddlMaterialGroup2.SelectedValue;
            ObjSales.Material_Group3 = ddlMaterialGroup3.SelectedValue;
            ObjSales.Material_Group4 = ddlMaterialGroup4.SelectedValue;
            ObjSales.Material_Group5 = ddlMaterialGroup5.SelectedValue;
            ObjSales.Cross_Distrib_Chain_Mat_Status = ddlCrossDistribution.SelectedValue;
            ObjSales.CAS_No_Pharma_Prod_FT = ddlCasNumPharmaceutical.SelectedValue;

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
            SectionConfiguration.Sales2 obj = new SectionConfiguration.Sales2();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    protected void ddlProductHierarchy1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ProductHierarchySetUp();
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
            {
                ddlProductHierarchy2.Enabled = true;
            }
            helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlProductHierarchy1_SelectedIndexChanged", ex); }
    }

    private void ProductHierarchySetUp()
    {
        bool flg = true;
        try
        {
            if (ddlProductHierarchy1.SelectedValue == "")
                flg = false;

            lableddlProductHierarchy2.Visible = flg;
            lableddlProductHierarchy3.Visible = flg;

            reqddlProductHierarchy2.Visible = flg;
            reqddlProductHierarchy3.Visible = flg;
        }
        catch (Exception ex)
        { _log.Error("ProductHierarchySetUp", ex); }
    }

    protected void ddlProductHierarchy2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
            {
                ddlProductHierarchy3.Enabled = true;
            }
            helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','3','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlProductHierarchy2_SelectedIndexChanged", ex); }
    }

    private void CheckIfChanges(Sales2 NewSales2Data, Sales2 oldSales2Data)
    {
        try
        {
            if (NewSales2Data.Mat_Sales2_Id > 0 && oldSales2Data.Mat_Sales2_Id > 0)
            {
                if (NewSales2Data.Plant_Id != oldSales2Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant_Id</td> <td>" + oldSales2Data.Plant_Id + "</td><td>" + NewSales2Data.Plant_Id + "</td></tr>";
                if (NewSales2Data.Sales_Organization_Id != oldSales2Data.Sales_Organization_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Organization Id</td> <td>" + oldSales2Data.Sales_Organization_Id + "</td><td>" + NewSales2Data.Sales_Organization_Id + "</td></tr>";
                if (NewSales2Data.Distribution_Channel_ID != oldSales2Data.Distribution_Channel_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distribution Channel ID</td> <td>" + oldSales2Data.Distribution_Channel_ID + "</td><td>" + NewSales2Data.Distribution_Channel_ID + "</td></tr>";
                if (NewSales2Data.Mat_Statistic_Group != oldSales2Data.Mat_Statistic_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mat Statistic Group</td> <td>" + oldSales2Data.Mat_Statistic_Group + "</td><td>" + NewSales2Data.Mat_Statistic_Group + "</td></tr>";
                if (NewSales2Data.Volume_Rebate_Group != oldSales2Data.Volume_Rebate_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Volume Rebate Group</td> <td>" + oldSales2Data.Volume_Rebate_Group + "</td><td>" + NewSales2Data.Volume_Rebate_Group + "</td></tr>";
                if (NewSales2Data.Commission_Group != oldSales2Data.Commission_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Commission Group</td> <td>" + oldSales2Data.Commission_Group + "</td><td>" + NewSales2Data.Commission_Group + "</td></tr>";
                if (NewSales2Data.Gen_Item_Category_Grp != oldSales2Data.Gen_Item_Category_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Gen Item Category Grp</td> <td>" + oldSales2Data.Gen_Item_Category_Grp + "</td><td>" + NewSales2Data.Gen_Item_Category_Grp + "</td></tr>";
                if (NewSales2Data.Item_Category_Grp != oldSales2Data.Item_Category_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Item Category Grp</td> <td>" + oldSales2Data.Item_Category_Grp + "</td><td>" + NewSales2Data.Item_Category_Grp + "</td></tr>";
                if (NewSales2Data.Product_Hierarchy != oldSales2Data.Product_Hierarchy)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Product Hierarchy</td> <td>" + oldSales2Data.Product_Hierarchy + "</td><td>" + NewSales2Data.Product_Hierarchy + "</td></tr>";
                if (NewSales2Data.Pricing_Ref_Mat != oldSales2Data.Pricing_Ref_Mat)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Pricing Ref Mat</td> <td>" + oldSales2Data.Pricing_Ref_Mat + "</td><td>" + NewSales2Data.Pricing_Ref_Mat + "</td></tr>";
                if (NewSales2Data.Mat_Pricing_Group != oldSales2Data.Mat_Pricing_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mat Pricing Group</td> <td>" + oldSales2Data.Mat_Pricing_Group + "</td><td>" + NewSales2Data.Mat_Pricing_Group + "</td></tr>";
                if (NewSales2Data.Acc_Assignment_Grp != oldSales2Data.Acc_Assignment_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Acc Assignment Grp</td> <td>" + oldSales2Data.Acc_Assignment_Grp + "</td><td>" + NewSales2Data.Acc_Assignment_Grp + "</td></tr>";
                if (NewSales2Data.Material_Group1 != oldSales2Data.Material_Group1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Group1</td> <td>" + oldSales2Data.Material_Group1 + "</td><td>" + NewSales2Data.Material_Group1 + "</td></tr>";
                if (NewSales2Data.Material_Group2 != oldSales2Data.Material_Group2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Group2</td> <td>" + oldSales2Data.Material_Group2 + "</td><td>" + NewSales2Data.Material_Group2 + "</td></tr>";
                if (NewSales2Data.Material_Group3 != oldSales2Data.Material_Group3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Group3</td> <td>" + oldSales2Data.Material_Group3 + "</td><td>" + NewSales2Data.Material_Group3 + "</td></tr>";
                if (NewSales2Data.Material_Group4 != oldSales2Data.Material_Group4)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Group4</td> <td>" + oldSales2Data.Material_Group4 + "</td><td>" + NewSales2Data.Material_Group4 + "</td></tr>";
                if (NewSales2Data.Material_Group5 != oldSales2Data.Material_Group5)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Group5</td> <td>" + oldSales2Data.Material_Group5 + "</td><td>" + NewSales2Data.Material_Group5 + "</td></tr>";
                if (NewSales2Data.Cross_Distrib_Chain_Mat_Status != oldSales2Data.Cross_Distrib_Chain_Mat_Status)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Cross Distrib Chain Mat Status</td> <td>" + oldSales2Data.Cross_Distrib_Chain_Mat_Status + "</td><td>" + NewSales2Data.Cross_Distrib_Chain_Mat_Status + "</td></tr>";
                if (NewSales2Data.CAS_No_Pharma_Prod_FT != oldSales2Data.CAS_No_Pharma_Prod_FT)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Category of International Article Number</td> <td>" + oldSales2Data.CAS_No_Pharma_Prod_FT + "</td><td>" + NewSales2Data.CAS_No_Pharma_Prod_FT + "</td></tr>";
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
    private void CheckIfChangesLog(Sales2 NewSales2Data, Sales2 oldSales2Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewSales2Data.Mat_Sales2_Id > 0 && oldSales2Data.Mat_Sales2_Id > 0)
            {
                if (NewSales2Data.Mat_Statistic_Group != oldSales2Data.Mat_Statistic_Group)
                {
                    _items.Add(new SMChange { colFieldName = 167, colOldVal = oldSales2Data.Mat_Statistic_Group, colNewVal = NewSales2Data.Mat_Statistic_Group });
                }
                if (NewSales2Data.Volume_Rebate_Group != oldSales2Data.Volume_Rebate_Group)
                    _items.Add(new SMChange { colFieldName = 168, colOldVal = oldSales2Data.Volume_Rebate_Group, colNewVal = NewSales2Data.Volume_Rebate_Group });
                if (NewSales2Data.Commission_Group != oldSales2Data.Commission_Group)
                    _items.Add(new SMChange { colFieldName = 169, colOldVal = oldSales2Data.Commission_Group, colNewVal = NewSales2Data.Commission_Group });
                if (NewSales2Data.Gen_Item_Category_Grp != oldSales2Data.Gen_Item_Category_Grp)
                {
                    WriteMatChangeLog("MatChangeLogS2" + sdate + ".txt", "1209" + oldSales2Data.Gen_Item_Category_Grp + '-' + NewSales2Data.Gen_Item_Category_Grp);

                    _items.Add(new SMChange { colFieldName = 1209, colOldVal = oldSales2Data.Gen_Item_Category_Grp, colNewVal = NewSales2Data.Gen_Item_Category_Grp });
                }
                if (NewSales2Data.Item_Category_Grp != oldSales2Data.Item_Category_Grp)
                    _items.Add(new SMChange { colFieldName = 170, colOldVal = oldSales2Data.Item_Category_Grp, colNewVal = NewSales2Data.Item_Category_Grp });
                if (NewSales2Data.Product_Hierarchy != oldSales2Data.Product_Hierarchy)
                    _items.Add(new SMChange { colFieldName = 171, colOldVal = oldSales2Data.Product_Hierarchy, colNewVal = NewSales2Data.Product_Hierarchy });
                if (NewSales2Data.Pricing_Ref_Mat != oldSales2Data.Pricing_Ref_Mat)
                    _items.Add(new SMChange { colFieldName = 172, colOldVal = oldSales2Data.Pricing_Ref_Mat, colNewVal = NewSales2Data.Pricing_Ref_Mat });
                if (NewSales2Data.Mat_Pricing_Group != oldSales2Data.Mat_Pricing_Group)
                    _items.Add(new SMChange { colFieldName = 173, colOldVal = oldSales2Data.Mat_Pricing_Group, colNewVal = NewSales2Data.Mat_Pricing_Group });
                if (NewSales2Data.Acc_Assignment_Grp != oldSales2Data.Acc_Assignment_Grp)
                    _items.Add(new SMChange { colFieldName = 174, colOldVal = oldSales2Data.Acc_Assignment_Grp, colNewVal = NewSales2Data.Acc_Assignment_Grp });
                if (NewSales2Data.Material_Group1 != oldSales2Data.Material_Group1)
                    _items.Add(new SMChange { colFieldName = 175, colOldVal = oldSales2Data.Material_Group1, colNewVal = NewSales2Data.Material_Group1 });
                if (NewSales2Data.Material_Group2 != oldSales2Data.Material_Group2)
                    _items.Add(new SMChange { colFieldName = 176, colOldVal = oldSales2Data.Material_Group2, colNewVal = NewSales2Data.Material_Group2 });
                if (NewSales2Data.Material_Group3 != oldSales2Data.Material_Group3)
                    _items.Add(new SMChange { colFieldName = 177, colOldVal = oldSales2Data.Material_Group3, colNewVal = NewSales2Data.Material_Group3 });
                if (NewSales2Data.Material_Group4 != oldSales2Data.Material_Group4)
                    _items.Add(new SMChange { colFieldName = 178, colOldVal = oldSales2Data.Material_Group4, colNewVal = NewSales2Data.Material_Group4 });
                if (NewSales2Data.Material_Group5 != oldSales2Data.Material_Group5)
                    _items.Add(new SMChange { colFieldName = 179, colOldVal = oldSales2Data.Material_Group5, colNewVal = NewSales2Data.Material_Group5 });
                if (NewSales2Data.Cross_Distrib_Chain_Mat_Status != oldSales2Data.Cross_Distrib_Chain_Mat_Status)
                    _items.Add(new SMChange { colFieldName = 180, colOldVal = oldSales2Data.Cross_Distrib_Chain_Mat_Status, colNewVal = NewSales2Data.Cross_Distrib_Chain_Mat_Status });
                if (NewSales2Data.CAS_No_Pharma_Prod_FT != oldSales2Data.CAS_No_Pharma_Prod_FT)
                    _items.Add(new SMChange { colFieldName = 181, colOldVal = oldSales2Data.CAS_No_Pharma_Prod_FT, colNewVal = NewSales2Data.CAS_No_Pharma_Prod_FT });
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
                ChangeSMatID1 = helperAccess.MaterialChange("16", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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