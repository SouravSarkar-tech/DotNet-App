using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using log4net;
public partial class Transaction_MaterialBlock : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    MaterialBlockAccess ObjMaterialBlockAccess = new MaterialBlockAccess();

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
                    //PopuplateDropDownList();
                    
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    if (lblActionType.Text == "U")
                        lblSectionId.Text = "60";



                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {

                        trButton.Visible = true;
                        btnSave.Visible = btnNext.Visible = true;

                        //if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() != "2")
                        //{
                        //    lnlAddDetails.Visible = false;
                        //    pnlData.Visible = false;
                        //}
                    }
                    else
                    {
                        grvData.Columns[3].Visible = false;
                        lnlAddDetails.Visible = false;
                        pnlData.Visible = false;

                        ExcelUpload1.Visible = false;
                        //hlImportFormat.Visible = false;
                    }
                 

                    ClearData();
                    ConfigureControl();
                    //FillDataGrid();

                    //To manage the Creation Single request
                    FillFormDataByMHId();
                    //lnlAddDetails.Visible = false;
                    //grvData.Visible = false;
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

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;

            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMaterialBlockId.Text = grvData.DataKeys[grdrow.RowIndex]["Material_Block_Id"].ToString();


            MaterialBlock ObjMaterialBlock = GetMaterialBlockData();
            if (ObjMaterialBlock.Material_Block_Id > 0)
            {
                lblMaterialBlockId.Text = "0";

                txtMaterialCode.Text ="";
                txtMaterialName.Text = "";
                //ddlMaterialAccGrp.SelectedIndex = 0;
                ddlMaterialAccGrp.SelectedValue = ObjMaterialBlock.Material_Type;

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialBlockId.Text + "'", "Plant_Name", "Plant_Id", "");
                ddlPlant.SelectedValue = ObjMaterialBlock.Plant_Id;
                BindPlantWiseDropDown();

                rdlBlockValue.SelectedValue = ObjMaterialBlock.Blocking_Level;
                ddlSalesOrginization.SelectedValue = ObjMaterialBlock.Sales_Organization_Id;
                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                ddlDistributionChannel.SelectedValue = ObjMaterialBlock.Distribution_Channel_ID;
                txtRemarks.Text = ObjMaterialBlock.Remarks;
            }
            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("lnkCopy_Click", ex);
        }
    }
    
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMaterialBlockId.Text = grvData.DataKeys[grdrow.RowIndex]["Material_Block_Id"].ToString();
            FillMaterialBlockData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            string Material_Block_Id = grvData.DataKeys[grdrow.RowIndex]["Material_Block_Id"].ToString();
            MasterAccess.DeleteDataBySectionId(lblMasterHeaderId.Text, lblSectionId.Text, Material_Block_Id);

            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            FillDataGrid();
        }
        catch (Exception ex)
        {
            _log.Error("lnkDelete_Click", ex);
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveMaterialBlock())
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
        if (IsValidMaterial())
        {
            if (SaveMaterialBlock())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "Entry for Material already exists.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
        Response.Redirect("MaterialMaster.aspx",false);
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

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    protected void txtMaterialCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.MaterialProcessModuleId] != null)
        {
            pnlMsg.Visible = false;
            if (Session[StaticKeys.MaterialProcessModuleId].ToString() != MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text))
            {
                lblMsg.Text = "Please enter only " + ddlMaterialAccGrp.SelectedItem.Text;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtMaterialCode.Text = "";
                txtMaterialCode.Focus();
            }
        }
        else
        {
            ddlMaterialAccGrp.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text);
            txtMaterialName.Focus();
        }

        }
        catch (Exception ex)
        { _log.Error("txtMaterialCode_TextChanged", ex); }

    }

    protected void rdlBlockValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        BlockValueWiseSetup();
        }
        catch (Exception ex)
        { _log.Error("rdlBlockValue_SelectedIndexChanged", ex); }
    }
        
    #endregion

    #region Method

    protected bool IsValidMaterial()
    {
        bool flg = true;
        try
        {
        MaterialBlock ObjMaterialBlock = GetControlsValue();
        if (ObjMaterialBlock.Material_Block_Id > 0)
        {
            flg =  true;
        }
        else
        {
            foreach (GridViewRow gvr in grvData.Rows)
            {
                Label lblMaterialNumber = (Label)gvr.FindControl("lblMaterialNumber");

                if (lblMaterialNumber != null)
                {
                    if (lblMaterialNumber.Text == txtMaterialCode.Text)
                    {
                        flg = false;
                        break;
                    }
                }
            }
            }
        }
        catch (Exception ex)
        { _log.Error("IsValidMaterial", ex); }

        return flg;

    }

    protected void BlockValueWiseSetup()
    {
        try
        {
        if (rdlBlockValue.SelectedValue == "S")
        {
            ddlSalesOrginization.Enabled = true;
            ddlDistributionChannel.Enabled = true;

            reqddlSalesOrginization.Visible = true;
            reqddlDistributionChannel.Visible = true;

            lableddlSalesOrginization.Visible = true;
            lableddlDistributionChannel.Visible = true;
        }
        else
        {
            ddlSalesOrginization.Enabled = false;
            ddlDistributionChannel.Enabled = false;

            reqddlSalesOrginization.Visible = false;
            reqddlDistributionChannel.Visible = false;

            lableddlSalesOrginization.Visible = false;
            lableddlDistributionChannel.Visible = false;

            ddlSalesOrginization.SelectedIndex = 0;

            //helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
            }
        }
        catch (Exception ex)
        { _log.Error("BlockValueWiseSetup", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialBlockId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");

        //helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        
        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        helperAccess.PopuplateDropDownList(ddlMaterialAccGrp, "pr_GetModuleListByModuleType 'M'", "Module_Name", "Module_Id", "0");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }
    
    private void ClearData()
    {
        try
        {
            lblMaterialBlockId.Text = "0";
            txtMaterialCode.Text = "";
            txtMaterialName.Text = "";
            txtRemarks.Text = "";
            PopuplateDropDownList();
            FillMaterialBlockData();
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
            ds = ObjMaterialBlockAccess.GetMaterialBlockData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
        }
    }

    private bool SaveMaterialBlock()
    {
        bool flg = false;
        try
        {
            MaterialBlock ObjMaterialBlock = GetControlsValue();
            
            if (ObjMaterialBlockAccess.Save(ObjMaterialBlock) > 0)
            {
                //FillDataGrid();
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
        catch (Exception ex)
        {
            _log.Error("SaveMaterialBlock", ex);
        }
        return flg;
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
        ds = ObjMaterialBlockAccess.GetMaterialBlockData(Convert.ToInt32(lblMasterHeaderId.Text));
        grvData.DataSource = ds;
        grvData.DataBind();

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    lblMaterialBlockId.Text = ds.Tables[0].Rows[0]["Material_Block_Id"].ToString();
        //}
        FillMaterialBlockData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private MaterialBlock GetMaterialBlockData()
    {
        return ObjMaterialBlockAccess.GetMaterialBlock(Convert.ToInt32(lblMaterialBlockId.Text));
    }

    private MaterialBlock GetControlsValue()
    {
        MaterialBlock ObjMaterialBlock = new MaterialBlock();
        Utility objUtil = new Utility();
       
        try
        {
            ObjMaterialBlock.Material_Block_Id = Convert.ToInt32(lblMaterialBlockId.Text);
            ObjMaterialBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjMaterialBlock.Material_Number = txtMaterialCode.Text;
            ObjMaterialBlock.Material_Short_Description = txtMaterialName.Text;
            ObjMaterialBlock.Material_Type = ddlMaterialAccGrp.SelectedValue;

            ObjMaterialBlock.Plant_Id = ddlPlant.SelectedValue;
            ObjMaterialBlock.Blocking_Level = rdlBlockValue.SelectedValue;
            ObjMaterialBlock.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjMaterialBlock.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;

            ObjMaterialBlock.Purchase_Status = "";
            ObjMaterialBlock.Material_Status = "";
            ObjMaterialBlock.Remarks = txtRemarks.Text;

            ObjMaterialBlock.IsActive = "1";
            ObjMaterialBlock.UserId = lblUserId.Text;
            ObjMaterialBlock.TodayDate = objUtil.GetDate();
            ObjMaterialBlock.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjMaterialBlock;
    }

    private void BindPlantWiseDropDown()
    {
        //helperAccess.PopuplateDropDownList(ddlValuationCategory, "pr_Get_Valuation_Category_By_Plant_Id 'M','ddlValuationCategory','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    }

    private void FillMaterialBlockData()
    {
      
        try
        {
            MaterialBlock ObjMaterialBlock = GetMaterialBlockData();
            if (ObjMaterialBlock.Material_Block_Id > 0)
            {
                lblMaterialBlockId.Text = ObjMaterialBlock.Material_Block_Id.ToString();

                txtMaterialCode.Text = ObjMaterialBlock.Material_Number;
                txtMaterialName.Text = ObjMaterialBlock.Material_Short_Description;
                ddlMaterialAccGrp.SelectedValue = ObjMaterialBlock.Material_Type;
                
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialBlockId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialBlockId.Text + "'", "Plant_Name", "Plant_Id", "");
                ddlPlant.SelectedValue = ObjMaterialBlock.Plant_Id;
                BindPlantWiseDropDown();
                
                rdlBlockValue.SelectedValue = ObjMaterialBlock.Blocking_Level;
                ddlSalesOrginization.SelectedValue = ObjMaterialBlock.Sales_Organization_Id;
                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                ddlDistributionChannel.SelectedValue = ObjMaterialBlock.Distribution_Channel_ID;
                txtRemarks.Text = ObjMaterialBlock.Remarks;
            }
            else
            {
                lblMaterialBlockId.Text = "0";

                if (Session[StaticKeys.MaterialProcessModuleId] != null)
                {
                    ddlMaterialAccGrp.SelectedValue = Session[StaticKeys.MaterialProcessModuleId].ToString();
                }

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialBlockId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialBlockId.Text + "'", "Plant_Name", "Plant_Id", "");
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                BindPlantWiseDropDown();
                //if (lblModuleId.Text == "138" || lblModuleId.Text == "147")
                //{

                
            }
            BlockValueWiseSetup();
            //ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillMaterialBlockData", ex);
        }
    }

    private void ConfigureControl()
    {
        //string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        //SectionConfiguration.MaterialBlock obj = new SectionConfiguration.MaterialBlock();
        //SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    
    #endregion    
}