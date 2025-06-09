using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Transactions;
using log4net;

public partial class Transaction_BOM_BOMHeader : System.Web.UI.Page
{ 
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    BOMAccess ObjBOMAccess = new BOMAccess();
    HelperAccess helperAccess = new HelperAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
            if (Session[StaticKeys.MasterHeaderId] != null)
            {
                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                PopuplateDropDownList();

                string pageSequence = Request.QueryString["pgseq"].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string sectionId = Request.QueryString["sid"].ToString();
                string mode = Session[StaticKeys.Mode].ToString();

                ReadDeparmentListForRollback(lblMasterHeaderId.Text, deptId, moduleId);
                //if (mode == "M1" || mode == "N")
                //{
                //    if (mode == "M")
                //        btnSubmit.Visible = true;
                //}
                //else
                if (mode == "M" || mode == "V")
                {
                    pnldetailV.Visible = true;
                    fncGetBOMHeader();
                    fncBindGridV();
                    btnNext.Visible = false;
                    if (mode == "M")
                        btnSubmit.Visible = true;
                }

                btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, deptId, lblUserId.Text);
                //if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                //{
                //    trButton.Visible = true;
                //}


            }
            else
            {
                Response.Redirect("MaterialMaster.aspx");
            }
        }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplatePlantList(ddlPlant, lblMasterHeaderId.Text, "C1", lblCostingId.Text);
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }


    //private bool SaveBOMHeader()
    //{
    //    bool Flag = false;
    //    BOMHeader ObjAcc = GetControlsValue();

    //    try
    //    {
    //        lblBOMHeaderlId.Text = Convert.ToString(ObjBOMAccess.Save(ObjAcc));

    //        if (Convert.ToInt16(lblBOMHeaderlId.Text) > 0)
    //        {

    //            Flag = true;
    //        }
    //        else
    //        {
    //            lblMsg.Text = Messages.GetMessage(-1);
    //            pnlMsg.CssClass = "error";
    //            pnlMsg.Visible = true;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return Flag;
    //}

    //private BOMHeader GetControlsValue()
    //{
    //    BOMHeader ObjAcc = new BOMHeader();
    //    Utility objUtil = new Utility();

    //    lblBOMHeaderlId.Text = "0";

    //    ObjAcc.BOM_HeaderID = Convert.ToInt32(lblBOMHeaderlId.Text);
    //    ObjAcc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
    //    ObjAcc.PlantId = GetSelectedCheckedValue(ddlPlant);
    //    ObjAcc.Material_Id = txtMaterial.Text;
    //    ObjAcc.BOM_Usage = Convert.ToInt32(ddlBOMUsage.SelectedValue);
    //    ObjAcc.ValidFrom = Convert.ToDateTime(objUtil.GetMMDDYYYY(txtValidFrom.Text));
    //    ObjAcc.ValidTo = Convert.ToDateTime(objUtil.GetMMDDYYYY(txtValidTo.Text));


    //    ObjAcc.IsActive = 1;
    //    ObjAcc.UserId = lblUserId.Text;
    //    ObjAcc.TodayDate = objUtil.GetDate();
    //    ObjAcc.IPAddress = objUtil.GetIpAddress();

    //    return ObjAcc;
    //}
    //protected void btnNext_Click(object sender, EventArgs e)
    //{
    //    if (SaveBOMHeader())
    //    {
    //        lblMsg.Text = Messages.GetMessage(1);
    //        pnlMsg.CssClass = "success";
    //        pnlMsg.Visible = true;

    //        txtMaterial.Enabled = false;
    //        txtValidFrom.Enabled = false;
    //        txtValidTo.Enabled = false;
    //        ddlBOMUsage.Enabled = false;
    //        ddlPlant.Enabled = false;
    //        btnNext.Visible = false;
    //        pnldetail.Visible = true;


    //    }
    //}
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{

    //    if (SaveBOMDetail())
    //    {
    //        fncBindGrid();
    //    }
    //}

    private void fncBindGrid()
    {
        try
        { 
        DataSet ds = new DataSet();
        ds = ObjBOMAccess.GetBOMDetail(Convert.ToInt16(lblBOMHeaderlId.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            grd.DataSource = ds.Tables[0].DefaultView;
            grd.DataBind();
            btnSubmit.Visible = true;
            grd.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
            grd.Visible = false;
        }
        }
        catch (Exception ex)
        { _log.Error("fncBindGrid", ex); }

    }
    private void fncBindGridV()
    {
        try {  
        DataSet ds = new DataSet();
        ds = ObjBOMAccess.GetBOMDetail(Convert.ToInt16(lblBOMHeaderlId.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdV.DataSource = ds.Tables[0].DefaultView;
            grdV.DataBind();
            grdV.Visible = true;
            pnldetailV.Visible = true;
        }
        else
        {
            grdV.Visible = false;
            pnldetailV.Visible = false;
        }
        }
        catch (Exception ex)
        { _log.Error("fncBindGridV", ex); }
    }
    private void fncGetBOMHeader()
    {
        try
        { 
        Utility objUtil = new Utility();
        DataSet ds = new DataSet();

        ds = ObjBOMAccess.GetBOMHeader(Convert.ToInt16(lblMasterHeaderId.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblBOMHeaderlId.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            txtMaterial.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            ddlBOMUsage.SelectedValue = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            txtValidFrom.Text = objUtil.GetDDMMYYYY(ds.Tables[0].Rows[0].ItemArray[4].ToString());
            txtValidTo.Text = objUtil.GetDDMMYYYY(ds.Tables[0].Rows[0].ItemArray[4].ToString());

            txtMaterial.Enabled = false;
            ddlBOMUsage.Enabled = false;
            txtValidFrom.Enabled = false;
            txtValidTo.Enabled = false;
            ddlPlant.Enabled = false;
        }
        }
        catch (Exception ex)
        { _log.Error("fncGetBOMHeader", ex); }

    }

    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strBOMDetailId = e.CommandArgument.ToString();
        try
        {
            if (ObjBOMAccess.Delete(Convert.ToInt16(strBOMDetailId)) > 0)
            {
                fncBindGrid();
            }


        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("grd_RowCommand", ex);
        }

    }
    //private bool SaveBOMDetail()
    //{
    //    bool Flag = false;
    //    BOMDetail ObjAcc = GetControlsDetailsValue();

    //    try
    //    {
    //        if (ObjBOMAccess.Save(ObjAcc) > 0)
    //        {
    //            Flag = true;
    //            ClearData();
    //            pnlMsg.Visible = false;
    //        }
    //        else
    //        {
    //            lblMsg.Text = Messages.GetMessage(-1);
    //            pnlMsg.CssClass = "error";
    //            pnlMsg.Visible = true;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return Flag;
    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                {
                    if (materialMasterAccess.SaveMaterialHeader(lblMasterHeaderId.Text, moduleId, lblUserId.Text, "M") > 0)
                    {

                        flg = true;
                        scope.Complete();
                    }
                }
            }
            if (flg)
            {
                Response.Redirect("../BOM/BOMMaster.aspx?pg=9", false);
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
            _log.Error("btnSubmit_Click", ex);
        }

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
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
        if (RollbackRequest())
        {
            Response.Redirect("../bom/BOMMaster.aspx?pg=7");
        }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }
    private bool RollbackRequest()
    {
        bool flg = false;
        try
        {
            MaterialMasterAccess objAccess = new MaterialMasterAccess();

            if (objAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), ddlRejectTo.SelectedValue, Utility.RemoveSpecialChar(txtRejectNote.Text), lblUserId.Text) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-2);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("btnRollback_Click", ex);
        }
        return flg;
    }


    //private BOMDetail GetControlsDetailsValue()
    //{
    //    BOMDetail ObjAcc = new BOMDetail();
    //    Utility objUtil = new Utility();


    //    ObjAcc.BOM_HeaderID = Convert.ToInt32(lblBOMHeaderlId.Text);
    //    ObjAcc.Component = txtComponent.Text;
    //    ObjAcc.Quantity = Convert.ToInt32(txtQuantity.Text);
    //    ObjAcc.Component_UOM = txtComponentUOM.Text;
    //    if (chkASM.Checked == true)
    //    {
    //        ObjAcc.ASM = 1;
    //    }
    //    else
    //    {
    //        ObjAcc.ASM = 0;
    //    }
    //    ObjAcc.Base_Quantity = txtBaseQty.Text;
    //    ObjAcc.Base_Quantity_UOM = txtBaseQtyUOM.Text;
    //    ObjAcc.BOM_Status = txtBOMStatus.Text;
    //    ObjAcc.Comp_Scrap_Per = Convert.ToInt32(txtCompScrap.Text);
    //    ObjAcc.Item_Category = txtItemCat.Text;
    //    ObjAcc.Costing_Relevncy = txtCostingRel.Text;

    //    return ObjAcc;
    //}

    protected string GetSelectedCheckedValue(CheckBoxList chkList)
    {
        try
        {
            string str = null;
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                    str += item.Value + ",";
            }
            return str;

        }
        catch(Exception ex)
        {
            _log.Error("GetSelectedCheckedValue", ex);
            throw;
        }
    }
    private void ClearData()
    {
        try
        {

            txtComponent.Text = "";
            txtQuantity.Text = "";
            txtComponentUOM.Text = "";
            chkASM.Checked = false;
            txtBaseQty.Text = "";
            txtBaseQtyUOM.Text = "";
            txtBOMStatus.Text = "";
            txtCompScrap.Text = "";
            txtItemCat.Text = "";
            txtCostingRel.Text = "";

        }
        catch (Exception ex)
        {
            _log.Error("ClearData", ex);
            //throw ex;
        }
    }

    
}