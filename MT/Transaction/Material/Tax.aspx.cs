using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using log4net;
public partial class Transaction_Tax : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    TaxAccess ObjTaxAccess = new TaxAccess();
    HelperAccess helperAccess = new HelperAccess();
    Taxes objSavedTax = new Taxes();

    #region Page Events

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
                    grvData.Visible = false;
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

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
        ClearData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblTaxId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Tax_Id"].ToString();
            FillTaxData();
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
        string pageURL = btnPrevious.CommandArgument.ToString();
        Response.Redirect(pageURL);
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveTax())
        {
            //lblMsg.Text = Messages.GetMessage(1);
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("Tax.aspx",false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveTax())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblTaxId.Text + "','" + ddlSalesOrg.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Method

    private void ConfigureControlForSChange()
    {
        //MSC_8300001775
        try
        {
        ddlTaxClassificationM.Enabled = true;
        ddlTaxClassificationM2.Enabled = true;
        ddlTaxClassificationM3.Enabled = true;


        lableddlTaxCategory2.Visible = false;
        reqddlTaxCategory2.Visible = false;
        lableddlTaxClassificationM2.Visible = false;
        reqddlTaxClassificationM2.Visible = false;

        lableddlTaxCategory3.Visible = false;
        reqddlTaxCategory3.Visible = false;
        lableddlTaxClassificationM3.Visible = false;
        reqddlTaxClassificationM3.Visible = false;

        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
        //MSC_8300001775
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','T1','" + lblTaxId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        { 
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','T1','" + lblTaxId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','T1','" + lblTaxId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlSalesOrg, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblTaxId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblTaxId.Text + "','" + ddlSalesOrg.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        helperAccess.PopuplateDropDownList(ddlTaxClassificationM, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM2, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        //GST Changes
        //helperAccess.PopuplateDropDownList(ddlTaxClassificationM3, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory3.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        //GST Changes
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM4, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory4.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM5, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory5.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM6, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory6.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM7, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory7.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM8, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory8.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaxClassificationM9, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory9.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblTaxId.Text = "0";

            //ddlTaxCategory.Text = "";
            //ddlTaxClassificationM.Text = "";
            //ddlTaxCategory2.Text = "";
            //ddlTaxClassificationM2.Text = "";
            //ddlTaxCategory3.Text = "";
            //ddlTaxClassificationM3.Text = "";
            //ddlTaxCategory4.Text = "";
            //ddlTaxClassificationM4.Text = "";
            //ddlTaxCategory5.Text = "";
            //ddlTaxClassificationM5.Text = "";
            //ddlTaxCategory6.Text = "";
            //ddlTaxClassificationM6.Text = "";
            //ddlTaxCategory7.Text = "";
            //ddlTaxClassificationM7.Text = "";
            //ddlTaxCategory8.Text = "";
            //ddlTaxClassificationM8.Text = "";
            //ddlTaxCategory9.Text = "";
            //ddlTaxClassificationM9.Text = "";

            //ClearSelectedValue(ddlPlant);
            //ClearSelectedValue(ddlSalesOrg);
            //ClearSelectedValue(ddlDistributionChannel);
            PopuplateDropDownList();
        }
        catch (Exception ex)
        {
            _log.Error("ClearData", ex);
        }
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjTaxAccess.GetTaxData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
        }
    }

    private bool SaveTax()
    {
        bool flg = false;
        try
        {
            Taxes ObjTax = GetControlsValue();
            objSavedTax = GetTaxData();
            //if (ObjTax.Plant_Id != null && ObjTax.Sales_Organization_Id != null && ObjTax.Distribution_Channel_ID != null)
            //{

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedTax.Mat_Tax_Id > 0)
                {
                    CheckIfChanges(ObjTax, objSavedTax);
                }
            }

            if (ObjTaxAccess.Save(ObjTax) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjTax, objSavedTax);
                }
                //MSC_8300001775  

                ClearData();
                FillFormDataByMHId();

                flg = true;
				////MSC_8300001775
    //                if (HelperAccess.ReqType == "SMC")
    //                {
    //                    CheckIfChangesLog(ObjTax, objSavedTax);
    //                }
    //                //MSC_8300001775     
				
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //}
            //else
            //{

            //    if (ObjTax.Plant_Id != null)
            //        lblMsg.Text = "Please Select atleast one Plant to proceed.";
            //    else if (ObjTax.Sales_Organization_Id != null)
            //        lblMsg.Text = "Please Select atleast one Sales Org to proceed.";
            //    else if (ObjTax.Distribution_Channel_ID != null)
            //        lblMsg.Text = "Please Select atleast one Distribution Channel to proceed.";
            //    pnlMsg.CssClass = "error";
            //    pnlMsg.Visible = true;
            //}
        }
        catch (Exception ex)
        {
            _log.Error("SaveTax", ex);
        }
        return flg;
    }

    private Taxes GetTaxData()
    {
        return ObjTaxAccess.GetTax(Convert.ToInt32(lblTaxId.Text));
    }

    private Taxes GetControlsValue()
    {
        Taxes ObjTax = new Taxes();
        Utility objUtil = new Utility();
      
        try
        {
            ObjTax.Mat_Tax_Id = Convert.ToInt32(lblTaxId.Text);
            ObjTax.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjTax.Plant_Id = ddlPlant.SelectedValue;// GetSelectedCheckedValue(ddlPlant);
            ObjTax.Sales_Organization_Id = ddlSalesOrg.SelectedValue;
            ObjTax.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;

            ObjTax.Tax_Category = ddlTaxCategory.SelectedValue;
            ObjTax.Tax_Classification_Mat = ddlTaxClassificationM.SelectedValue;
            ObjTax.Tax_Category1 = ddlTaxCategory2.SelectedValue;
            ObjTax.Tax_Classification_Mat1 = ddlTaxClassificationM2.SelectedValue;
            ObjTax.Tax_Category2 = ddlTaxCategory3.SelectedValue;
            ObjTax.Tax_Classification_Mat2 = ddlTaxClassificationM3.SelectedValue;
            ObjTax.Tax_Category3 = ddlTaxCategory4.SelectedValue;
            ObjTax.Tax_Classification_Mat3 = ddlTaxClassificationM4.SelectedValue;
            ObjTax.Tax_Category4 = ddlTaxCategory5.SelectedValue;
            ObjTax.Tax_Classification_Mat4 = ddlTaxClassificationM5.SelectedValue;
            ObjTax.Tax_Category5 = ddlTaxCategory6.SelectedValue;
            ObjTax.Tax_Classification_Mat5 = ddlTaxClassificationM6.SelectedValue;
            ObjTax.Tax_Category6 = ddlTaxCategory7.SelectedValue;
            ObjTax.Tax_Classification_Mat6 = ddlTaxClassificationM7.SelectedValue;
            ObjTax.Tax_Category7 = ddlTaxCategory8.SelectedValue;
            ObjTax.Tax_Classification_Mat7 = ddlTaxClassificationM8.SelectedValue;
            ObjTax.Tax_Category8 = ddlTaxCategory9.SelectedValue;
            ObjTax.Tax_Classification_Mat8 = ddlTaxClassificationM9.SelectedValue;

            ObjTax.IsActive = 1;
            ObjTax.UserId = lblUserId.Text;
            ObjTax.TodayDate = objUtil.GetDate();
            ObjTax.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjTax;
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjTaxAccess.GetTaxData(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblTaxId.Text = ds.Tables[0].Rows[0]["Mat_Tax_Id"].ToString();
        }
        FillTaxData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        //
    }

    private void FillTaxData()
    {
        try
        {
          
            Taxes ObjTax = GetTaxData();
            if (ObjTax.Mat_Tax_Id > 0)
            {
                lblTaxId.Text = ObjTax.Mat_Tax_Id.ToString();
                PopuplateDropDownList();

                //SetSelectedValue(ddlPlant, ObjTax.Plant_Id);
                //SetSelectedValue(ddlSalesOrg, ObjTax.Sales_Organization_Id);
                //SetSelectedValue(ddlDistributionChannel, ObjTax.Distribution_Channel_ID);

                ddlPlant.SelectedValue = ObjTax.Plant_Id;
                ddlSalesOrg.SelectedValue = ObjTax.Sales_Organization_Id;
                ddlDistributionChannel.SelectedValue = ObjTax.Distribution_Channel_ID;

                ddlTaxCategory.SelectedValue = ObjTax.Tax_Category;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM.SelectedValue = ObjTax.Tax_Classification_Mat;
                ddlTaxCategory2.SelectedValue = ObjTax.Tax_Category1;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM2, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM2.SelectedValue = ObjTax.Tax_Classification_Mat1;
                ddlTaxCategory3.SelectedValue = ObjTax.Tax_Category2;
                //GST Changes
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM3, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory3.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                //GST Changes

                ddlTaxClassificationM3.SelectedValue = ObjTax.Tax_Classification_Mat2;
                ddlTaxCategory4.SelectedValue = ObjTax.Tax_Category3;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM4, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory4.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM4.SelectedValue = ObjTax.Tax_Classification_Mat3;
                ddlTaxCategory5.SelectedValue = ObjTax.Tax_Category4;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM5, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory5.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM5.SelectedValue = ObjTax.Tax_Classification_Mat4;
                ddlTaxCategory6.SelectedValue = ObjTax.Tax_Category5;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM6, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory6.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM6.SelectedValue = ObjTax.Tax_Classification_Mat5;
                ddlTaxCategory7.SelectedValue = ObjTax.Tax_Category6;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM7, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory7.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM7.SelectedValue = ObjTax.Tax_Classification_Mat6;
                ddlTaxCategory8.SelectedValue = ObjTax.Tax_Category7;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM8, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory8.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM8.SelectedValue = ObjTax.Tax_Classification_Mat7;
                ddlTaxCategory9.SelectedValue = ObjTax.Tax_Category8;
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM9, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory9.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM9.SelectedValue = ObjTax.Tax_Classification_Mat8;
            }
            else
            {
                lblTaxId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','T1','" + lblTaxId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','T1','" + lblTaxId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','T1','" + lblTaxId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();

                ddlTaxCategory.SelectedValue = "JCST";
                ddlTaxCategory2.SelectedValue = "JIVP";

                helperAccess.PopuplateDropDownList(ddlTaxClassificationM, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlTaxClassificationM2, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaxClassificationM','" + lblSectionId.Text + "','" + ddlTaxCategory2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlTaxClassificationM.SelectedValue = "1";
                ddlTaxClassificationM2.SelectedValue = "1";

                ddlTaxClassificationM.Enabled = false;
                ddlTaxClassificationM2.Enabled = false;

                //GST Changes
                ddlTaxCategory3.SelectedValue = "JOIG";
                ddlTaxClassificationM3.SelectedValue = "1";
                //GST Changes
            }

            ddlPlant.Enabled = false;
            ddlTaxCategory.Enabled = false;
            ddlTaxCategory2.Enabled = false;
            //GST Changes
            ddlTaxCategory3.Enabled = false;
            //GST Changes

        }
        catch (Exception ex)
        {
            _log.Error("FillTaxData", ex);
        }
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Tax obj = new SectionConfiguration.Tax();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }
    
    private void CheckIfChanges(Taxes NewTaxData, Taxes oldTaxData)
    {
        try
        {
            if (NewTaxData.Mat_Tax_Id > 0 && oldTaxData.Mat_Tax_Id > 0)
            {
                if (NewTaxData.Sales_Organization_Id != oldTaxData.Sales_Organization_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Organization Id</td> <td>" + oldTaxData.Sales_Organization_Id + "</td><td>" + NewTaxData.Sales_Organization_Id + "</td></tr>";
                if (NewTaxData.Plant_Id != oldTaxData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldTaxData.Plant_Id + "</td><td>" + NewTaxData.Plant_Id + "</td></tr>";
                if (NewTaxData.Distribution_Channel_ID != oldTaxData.Distribution_Channel_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distribution Channel ID</td> <td>" + oldTaxData.Distribution_Channel_ID + "</td><td>" + NewTaxData.Distribution_Channel_ID + "</td></tr>";
                if (NewTaxData.Tax_Category != oldTaxData.Tax_Category)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category</td> <td>" + oldTaxData.Tax_Category + "</td><td>" + NewTaxData.Tax_Category + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat != oldTaxData.Tax_Classification_Mat)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat</td> <td>" + oldTaxData.Tax_Classification_Mat + "</td><td>" + NewTaxData.Tax_Classification_Mat + "</td></tr>";
                if (NewTaxData.Tax_Category1 != oldTaxData.Tax_Category1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category1</td> <td>" + oldTaxData.Tax_Category1 + "</td><td>" + NewTaxData.Tax_Category1 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat1 != oldTaxData.Tax_Classification_Mat1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat1</td> <td>" + oldTaxData.Tax_Classification_Mat1 + "</td><td>" + NewTaxData.Tax_Classification_Mat1 + "</td></tr>";
                if (NewTaxData.Tax_Category2 != oldTaxData.Tax_Category2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category2</td> <td>" + oldTaxData.Tax_Category2 + "</td><td>" + NewTaxData.Tax_Category2 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat2 != oldTaxData.Tax_Classification_Mat2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat2</td> <td>" + oldTaxData.Tax_Classification_Mat2 + "</td><td>" + NewTaxData.Tax_Classification_Mat2 + "</td></tr>";
                if (NewTaxData.Tax_Category3 != oldTaxData.Tax_Category3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category3</td> <td>" + oldTaxData.Tax_Category3 + "</td><td>" + NewTaxData.Tax_Category3 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat3 != oldTaxData.Tax_Classification_Mat3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat3</td> <td>" + oldTaxData.Tax_Classification_Mat3 + "</td><td>" + NewTaxData.Tax_Classification_Mat3 + "</td></tr>";
                if (NewTaxData.Tax_Category4 != oldTaxData.Tax_Category4)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category4</td> <td>" + oldTaxData.Tax_Category4 + "</td><td>" + NewTaxData.Tax_Category4 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat4 != oldTaxData.Tax_Classification_Mat4)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat4</td> <td>" + oldTaxData.Tax_Classification_Mat4 + "</td><td>" + NewTaxData.Tax_Classification_Mat4 + "</td></tr>";
                if (NewTaxData.Tax_Category5 != oldTaxData.Tax_Category5)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category5</td> <td>" + oldTaxData.Tax_Category5 + "</td><td>" + NewTaxData.Tax_Category5 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat5 != oldTaxData.Tax_Classification_Mat5)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat5</td> <td>" + oldTaxData.Tax_Classification_Mat5 + "</td><td>" + NewTaxData.Tax_Classification_Mat5 + "</td></tr>";
                if (NewTaxData.Tax_Category6 != oldTaxData.Tax_Category6)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category6</td> <td>" + oldTaxData.Tax_Category6 + "</td><td>" + NewTaxData.Tax_Category6 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat6 != oldTaxData.Tax_Classification_Mat6)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat6</td> <td>" + oldTaxData.Tax_Classification_Mat6 + "</td><td>" + NewTaxData.Tax_Classification_Mat6 + "</td></tr>";
                if (NewTaxData.Tax_Category7 != oldTaxData.Tax_Category7)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category7</td> <td>" + oldTaxData.Tax_Category7 + "</td><td>" + NewTaxData.Tax_Category7 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat7 != oldTaxData.Tax_Classification_Mat7)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat7</td> <td>" + oldTaxData.Tax_Classification_Mat7 + "</td><td>" + NewTaxData.Tax_Classification_Mat7 + "</td></tr>";
                if (NewTaxData.Tax_Category8 != oldTaxData.Tax_Category8)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Category8</td> <td>" + oldTaxData.Tax_Category8 + "</td><td>" + NewTaxData.Tax_Category8 + "</td></tr>";
                if (NewTaxData.Tax_Classification_Mat8 != oldTaxData.Tax_Classification_Mat8)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax Classification Mat8</td> <td>" + oldTaxData.Tax_Classification_Mat8 + "</td><td>" + NewTaxData.Tax_Classification_Mat8 + "</td></tr>";

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
    private void CheckIfChangesLog(Taxes NewTaxData, Taxes oldTaxData)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewTaxData.Mat_Tax_Id > 0 && oldTaxData.Mat_Tax_Id > 0)
            {
                if (NewTaxData.Tax_Category != oldTaxData.Tax_Category)
                    _items.Add(new SMChange { colFieldName = 187, colOldVal = oldTaxData.Tax_Category, colNewVal = NewTaxData.Tax_Category });
                if (NewTaxData.Tax_Classification_Mat != oldTaxData.Tax_Classification_Mat)
                    _items.Add(new SMChange { colFieldName = 188, colOldVal = oldTaxData.Tax_Classification_Mat, colNewVal = NewTaxData.Tax_Classification_Mat });
                if (NewTaxData.Tax_Category1 != oldTaxData.Tax_Category1)
                    _items.Add(new SMChange { colFieldName = 189, colOldVal = oldTaxData.Tax_Category1, colNewVal = NewTaxData.Tax_Category1 });
                if (NewTaxData.Tax_Classification_Mat1 != oldTaxData.Tax_Classification_Mat1)
                    _items.Add(new SMChange { colFieldName = 190, colOldVal = oldTaxData.Tax_Classification_Mat1, colNewVal = NewTaxData.Tax_Classification_Mat1 });
                if (NewTaxData.Tax_Category2 != oldTaxData.Tax_Category2)
                    _items.Add(new SMChange { colFieldName = 191, colOldVal = oldTaxData.Tax_Category2, colNewVal = NewTaxData.Tax_Category2 });
                if (NewTaxData.Tax_Classification_Mat2 != oldTaxData.Tax_Classification_Mat2)
                    _items.Add(new SMChange { colFieldName = 192, colOldVal = oldTaxData.Tax_Classification_Mat2, colNewVal = NewTaxData.Tax_Classification_Mat2 });
                if (NewTaxData.Tax_Category3 != oldTaxData.Tax_Category3)
                    _items.Add(new SMChange { colFieldName = 193, colOldVal = oldTaxData.Tax_Category3, colNewVal = NewTaxData.Tax_Category3 });
                if (NewTaxData.Tax_Classification_Mat3 != oldTaxData.Tax_Classification_Mat3)
                    _items.Add(new SMChange { colFieldName = 194, colOldVal = oldTaxData.Tax_Classification_Mat3, colNewVal = NewTaxData.Tax_Classification_Mat3 });
                if (NewTaxData.Tax_Category4 != oldTaxData.Tax_Category4)
                    _items.Add(new SMChange { colFieldName = 195, colOldVal = oldTaxData.Tax_Category4, colNewVal = NewTaxData.Tax_Category4 });
                if (NewTaxData.Tax_Classification_Mat4 != oldTaxData.Tax_Classification_Mat4)
                    _items.Add(new SMChange { colFieldName = 196, colOldVal = oldTaxData.Tax_Classification_Mat4, colNewVal = NewTaxData.Tax_Classification_Mat4 });
                if (NewTaxData.Tax_Category5 != oldTaxData.Tax_Category5)
                    _items.Add(new SMChange { colFieldName = 197, colOldVal = oldTaxData.Tax_Category5, colNewVal = NewTaxData.Tax_Category5 });
                if (NewTaxData.Tax_Classification_Mat5 != oldTaxData.Tax_Classification_Mat5)
                    _items.Add(new SMChange { colFieldName = 198, colOldVal = oldTaxData.Tax_Classification_Mat5, colNewVal = NewTaxData.Tax_Classification_Mat5 });
                if (NewTaxData.Tax_Category6 != oldTaxData.Tax_Category6)
                    _items.Add(new SMChange { colFieldName = 199, colOldVal = oldTaxData.Tax_Category6, colNewVal = NewTaxData.Tax_Category6 });
                if (NewTaxData.Tax_Classification_Mat6 != oldTaxData.Tax_Classification_Mat6)
                    _items.Add(new SMChange { colFieldName = 200, colOldVal = oldTaxData.Tax_Classification_Mat6, colNewVal = NewTaxData.Tax_Classification_Mat6 });
                if (NewTaxData.Tax_Category7 != oldTaxData.Tax_Category7)
                    _items.Add(new SMChange { colFieldName = 201, colOldVal = oldTaxData.Tax_Category7, colNewVal = NewTaxData.Tax_Category7 });
                if (NewTaxData.Tax_Classification_Mat7 != oldTaxData.Tax_Classification_Mat7)
                    _items.Add(new SMChange { colFieldName = 202, colOldVal = oldTaxData.Tax_Classification_Mat7, colNewVal = NewTaxData.Tax_Classification_Mat7 });
                if (NewTaxData.Tax_Category8 != oldTaxData.Tax_Category8)
                    _items.Add(new SMChange { colFieldName = 203, colOldVal = oldTaxData.Tax_Category8, colNewVal = NewTaxData.Tax_Category8 });
                if (NewTaxData.Tax_Classification_Mat8 != oldTaxData.Tax_Classification_Mat8)
                    _items.Add(new SMChange { colFieldName = 204, colOldVal = oldTaxData.Tax_Classification_Mat8, colNewVal = NewTaxData.Tax_Classification_Mat8 });

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
                ChangeSMatID1 = helperAccess.MaterialChange("18", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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

    #endregion
}