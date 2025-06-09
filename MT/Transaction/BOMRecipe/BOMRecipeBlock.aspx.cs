using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;
using log4net;
public partial class Transaction_BOMRecipe_BOMRecipeBlock : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    BOMAccess objBOMRcpBlockAccess = new BOMAccess();

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

                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        if (lblActionType.Text == "U")
                            lblSectionId.Text = "82";

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
                        }
                        else
                        {
                            //grvData.Columns[3].Visible = false;
                            grvData.Columns[4].Visible = false;
                            lnlAddDetails.Visible = false;
                            pnlData.Visible = false;

                            ExcelUpload1.Visible = false;
                        }

                        ClearData();
                        //To manage the Creation Single request
                        FillFormDataByMHId();
                    }
                    else
                    {
                        Response.Redirect("BOMRecipeMaster.aspx");
                    }

                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveBOMRecipeBlock())
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

            //if (IsValidMaterial())
            //{
            if (ValidateBlockData())
            {
                if (ValidateRecipeData())
                {
                    if (ValidateBOMData())
                    {
                        if (ValidateProdVerData())
                        {
                            if (SaveBOMRecipeBlock())
                            {
                                lblMsg.Text = Messages.GetMessage(1);
                                pnlMsg.CssClass = "success";
                                pnlMsg.Visible = true;
                            }
                            else
                            {
                                lblMsg.Text = "Error while adding record.";
                                pnlMsg.CssClass = "error";
                                pnlMsg.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Enter Lock Status.";
                            pnlMsg.CssClass = "error";
                            pnlMsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Enter BOM status.";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Enter recipe status.";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }


            }
            else
            {
                lblMsg.Text = "Enter atleast one component to be blocked/unblocked.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //}
            //else
            //{
            //    lblMsg.Text = "Entry for Material already exists.";
            //    pnlMsg.CssClass = "error";
            //    pnlMsg.Visible = true;
            //}
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = grvData.Rows.Count;
            if (cnt > 0)
            {
                Session[StaticKeys.AddAlertMsg] = "BOM Recipe Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
                Response.Redirect("BOMRecipeMaster.aspx");
            }
            else
            {
                lblMsg.Text = "Enter atleast one material data before proceeding to submit.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
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
            lblBOMRecipeBlockId.Text = grvData.DataKeys[grdrow.RowIndex]["BOMRecipe_Block_Id"].ToString();


            BOMRecipeBlock ObjBOMRecipeBlock = GetBOMRecipeBlockData();
            if (ObjBOMRecipeBlock.BOMRecipe_Block_Id > 0)
            {
                lblBOMRecipeBlockId.Text = "0";

                txtMaterialCode.Text = "";

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                ddlPlant.SelectedValue = ObjBOMRecipeBlock.Plant_Id;
                txtRecipeGroup.Text = ObjBOMRecipeBlock.Recipe_Group;
                ddlRStatus.SelectedValue = ObjBOMRecipeBlock.Status;
                txtAltBOM.Text = ObjBOMRecipeBlock.AlternativeBOM;
                ddlBOMStatus.SelectedValue = ObjBOMRecipeBlock.BOMStatus;
                txtProdVersion.Text = ObjBOMRecipeBlock.ProdVersionNo;
                ddlLock.SelectedValue = ObjBOMRecipeBlock.Lock;
                txtRemarks.Text = ObjBOMRecipeBlock.Remarks;
            }
            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            //throw ex;
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
            lblBOMRecipeBlockId.Text = grvData.DataKeys[grdrow.RowIndex]["BOMRecipe_Block_Id"].ToString();
            FillBOMRecipeBlockData();
        }
        catch (Exception ex)
        {
            //throw ex;
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
            string BOMRecipe_Block_Id = grvData.DataKeys[grdrow.RowIndex]["BOMRecipe_Block_Id"].ToString();
            MasterAccess.DeleteDataBySectionId(lblMasterHeaderId.Text, lblSectionId.Text, BOMRecipe_Block_Id);

            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            FillDataGrid();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("lnkDelete_Click", ex);
        }
    }

    protected void txtRecipeGroup_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtRecipeGroup.Text != "")
            {
                lableddlRStatus.Visible = true;
                reqddlRStatus.Enabled = true;
            }
            else
            {
                lableddlRStatus.Visible = false;
                reqddlRStatus.Enabled = false;
                ddlRStatus.SelectedValue = "";
            }
        }
        catch (Exception ex)
        { _log.Error("txtRecipeGroup_TextChanged", ex); }
    }

    protected void txtAltBOM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAltBOM.Text != "")
            {

                txtAltBOM.Text = Convert.ToInt32(txtAltBOM.Text).ToString("D2");
                labelddlBOMStatus.Visible = true;
                reqddlBOMStatus.Enabled = true;
            }
            else
            {
                labelddlBOMStatus.Visible = false;
                reqddlBOMStatus.Enabled = false;
                ddlBOMStatus.SelectedValue = "";
            }
        }
        catch (Exception ex)
        { _log.Error("txtAltBOM_TextChanged", ex); }
    }

    //protected void txtProdVersion_TextChanged(object sender, EventArgs e)
    //{
    //    //if (txtProdVersion.Text != "")
    //    //{
    //    //    txtProdVersion.Text = Convert.ToInt32(txtProdVersion.Text).ToString("D4");                
    //    //    labelddlLock.Visible = true;
    //    //    //reqddlLock.Enabled = true;            
    //    //}
    //    //else
    //    //{
    //    //    labelddlLock.Visible = false;
    //    //    //reqddlLock.Enabled = false;
    //    //    ddlLock.SelectedValue = "0";
    //    //}
    //}

    #endregion

    #region Methods

    private void ClearData()
    {
        try
        {
            lblBOMRecipeBlockId.Text = "0";
            txtMaterialCode.Text = "";
            txtRemarks.Text = "";
            txtRecipeGroup.Text = "";
            txtAltBOM.Text = "";
            //txtProdVersion.Text = "";

            PopuplateDropDownList();
            ddlRStatus.SelectedValue = "";
            ddlLock.SelectedValue = "0";
            ddlBOMStatus.SelectedValue = "";
            FillBOMRecipeBlockData();
        }
        catch (Exception ex)
        {
            _log.Error("ClearData", ex);
        }
    }

    private void FillBOMRecipeBlockData()
    {
        try
        {
            BOMRecipeBlock ObjBOMRecipeBlock = GetBOMRecipeBlockData();
            if (ObjBOMRecipeBlock.BOMRecipe_Block_Id > 0)
            {
                lblBOMRecipeBlockId.Text = ObjBOMRecipeBlock.BOMRecipe_Block_Id.ToString();

                txtMaterialCode.Text = ObjBOMRecipeBlock.Material_Number;

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                ddlPlant.SelectedValue = ObjBOMRecipeBlock.Plant_Id;

                txtRecipeGroup.Text = ObjBOMRecipeBlock.Recipe_Group;
                ddlRStatus.SelectedValue = ObjBOMRecipeBlock.Status;
                txtAltBOM.Text = ObjBOMRecipeBlock.AlternativeBOM;
                ddlBOMStatus.SelectedValue = ObjBOMRecipeBlock.BOMStatus;
                txtProdVersion.Text = ObjBOMRecipeBlock.ProdVersionNo;
                ddlLock.SelectedValue = ObjBOMRecipeBlock.Lock;
                txtRemarks.Text = ObjBOMRecipeBlock.Remarks;
            }
            else
            {
                lblBOMRecipeBlockId.Text = "0";

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
            }

        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("FillBOMRecipeBlockData", ex);
        }
    }

    private BOMRecipeBlock GetBOMRecipeBlockData()
    {
        return objBOMRcpBlockAccess.GetBOMRecipeBlock(Convert.ToInt32(lblBOMRecipeBlockId.Text));
    }

    private void PopuplateDropDownList()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlRStatus, "pr_GetDropDownListByControlNameModuleType 'R','ddlRStatus'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlLock, "pr_GetDropDownListByControlNameModuleType 'R','ddlLock'", "LookUp_Desc", "LookUp_Code", "0");
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
            ds = objBOMRcpBlockAccess.GetBOMRecipeBlockData(Convert.ToInt32(lblMasterHeaderId.Text));
            grvData.DataSource = ds;
            grvData.DataBind();

            FillBOMRecipeBlockData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private bool SaveBOMRecipeBlock()
    {
        bool flg = false;
        try
        {
            BOMRecipeBlock ObjBOMRecipeBlock = GetControlsValue();

            if (objBOMRcpBlockAccess.Save(ObjBOMRecipeBlock) > 0)
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
            //throw ex;
            _log.Error("SaveBOMRecipeBlock", ex);
        }
        return flg;
    }

    private BOMRecipeBlock GetControlsValue()
    {
        BOMRecipeBlock ObjBOMRecipeBlock = new BOMRecipeBlock();
        Utility objUtil = new Utility();

        try
        {
            ObjBOMRecipeBlock.BOMRecipe_Block_Id = Convert.ToInt32(lblBOMRecipeBlockId.Text);
            ObjBOMRecipeBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjBOMRecipeBlock.Material_Number = txtMaterialCode.Text;
            ObjBOMRecipeBlock.Plant_Id = ddlPlant.SelectedValue;

            ObjBOMRecipeBlock.Recipe_Group = txtRecipeGroup.Text;
            ObjBOMRecipeBlock.Status = ddlRStatus.SelectedValue;
            ObjBOMRecipeBlock.AlternativeBOM = txtAltBOM.Text;
            ObjBOMRecipeBlock.BOMStatus = ddlBOMStatus.SelectedValue;
            ObjBOMRecipeBlock.ProdVersionNo = txtProdVersion.Text;
            //NRDD Start
            //ObjBOMRecipeBlock.ProdVersionNo = "";
            //NRDD end
            ObjBOMRecipeBlock.Lock = ddlLock.SelectedValue;
            ObjBOMRecipeBlock.Remarks = txtRemarks.Text;

            ObjBOMRecipeBlock.IsActive = "1";
            ObjBOMRecipeBlock.UserId = lblUserId.Text;
            ObjBOMRecipeBlock.TodayDate = objUtil.GetDate();
            ObjBOMRecipeBlock.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("GetControlsValue", ex);
        }
        return ObjBOMRecipeBlock;
    }

    protected bool IsValidMaterial()
    {
        bool flg = true;
        BOMRecipeBlock ObjBOMRecipeBlock = GetControlsValue();
        try
        { 

        if (ObjBOMRecipeBlock.BOMRecipe_Block_Id > 0)
        {
            flg = true;
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

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = objBOMRcpBlockAccess.GetBOMRecipeBlockData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("FillDataGrid", ex);
        }
    }

    protected bool ValidateBlockData()
    {
        //bool flg = true;
        //if (txtMaterialCode.Text == "" && txtRecipeGroup.Text == "" && txtAltBOM.Text == "" && txtProdVersion.Text == "")
        //    flg = false;
        //else if (!(txtRecipeGroup.Text != "" || txtAltBOM.Text != "" || txtProdVersion.Text != ""))
        //    flg = false;       
        //return flg;

        bool flg = true;
        try
        { 

        if (txtMaterialCode.Text == "" && txtRecipeGroup.Text == "" && txtAltBOM.Text == "")
            flg = false;
        else if (!(txtRecipeGroup.Text != "" || txtAltBOM.Text != ""))
            flg = false;
        }
        catch (Exception ex)
        { _log.Error("ValidateBlockData", ex); }

        return flg;
    }

    protected bool ValidateRecipeData()
    {
        bool flg = true;
        try
        { 
        if (txtRecipeGroup.Text != "" && ddlRStatus.SelectedValue == "")
            flg = false;
        }
        catch (Exception ex)
        { _log.Error("ValidateRecipeData", ex); }
        return flg;
    }

    protected bool ValidateBOMData()
    {
        bool flg = true;
        try
        { 

        if (txtAltBOM.Text != "" && ddlBOMStatus.SelectedValue == "")
            flg = false;
        }
        catch (Exception ex)
        { _log.Error("ValidateBOMData", ex); }
        return flg;
    }

    protected bool ValidateProdVerData()
    {
        //bool flg = true;
        //if (txtProdVersion.Text != "" && ddlLock.SelectedValue == "0")
        //    flg = false;
        //return flg;

        bool flg = true;
        try
        { 
        if (ddlLock.SelectedValue == "0")
            flg = false;
        }
        catch (Exception ex)
        { _log.Error("ValidateProdVerData", ex); }

        return flg;
    }

    #endregion
}