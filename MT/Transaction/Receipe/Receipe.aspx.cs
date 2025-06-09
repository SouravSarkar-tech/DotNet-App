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

public partial class Transaction_Receipe_Receipe : BasePage
{
    RecipeAccess ObjReceipeAccess = new RecipeAccess();
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
                lblReceipeId.Text = "0";
                string pageSequence = Request.QueryString["pgseq"].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string sectionId = Request.QueryString["sid"].ToString();
                string mode = Session[StaticKeys.Mode].ToString();
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
                    fncGetReceipe(mode, isUserApprover);
                }

                btnRejectTo.Visible = isUserApprover;
                lblIsUserApprover.Text = isUserApprover.ToString();
                if (isUserApprover)
                {
                    btnSubmit.Text = "Approve";
                }
                //if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                //{
                //    trButtonSubmit.Visible = true;
                //}
            }
            else
            {
                Response.Redirect("ReceipeMaster.aspx?pg=10");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceipeMaster.aspx?pg=10");
    }

    private void fncGetReceipe(string mode, bool isUserApprover)
    {
        Utility objUtil = new Utility();
        DataSet ds = new DataSet();
        string plants = string.Empty;
        ds = ObjReceipeAccess.GetReceipe(Convert.ToInt16(lblMasterHeaderId.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblReceipeId.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            txtMaterial.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            txtProd_Version.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            txtProfile.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            txtTaskList_Desc.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
            txtRecipe.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
            txtStatus.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
            txtUsage.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();
            txtBase_Quantity.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
            txtCharge_Quantity.Text = ds.Tables[0].Rows[0].ItemArray[10].ToString();
            txtOperation_Quantity.Text = ds.Tables[0].Rows[0].ItemArray[11].ToString();
            txtUOM.Text = ds.Tables[0].Rows[0].ItemArray[12].ToString();
            txtSuperOrdinate_Operation.Text = ds.Tables[0].Rows[0].ItemArray[13].ToString();
            txtControl_Recipe_Destination.Text = ds.Tables[0].Rows[0].ItemArray[14].ToString();
            txtResource.Text = ds.Tables[0].Rows[0].ItemArray[15].ToString();
            txtControl_Key.Text = ds.Tables[0].Rows[0].ItemArray[16].ToString();
            txtStandardTextKey.Text = ds.Tables[0].Rows[0].ItemArray[17].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0].ItemArray[18].ToString();
            if (ds.Tables[0].Rows[0].ItemArray[19].ToString() == "True")
            {
                chkRelevancy_To_Costing.Checked = true;
            }
            else
            {
                chkRelevancy_To_Costing.Checked = false;
            }
            txtFirst_Std_Value.Text = ds.Tables[0].Rows[0].ItemArray[20].ToString();
            txtFirst_Std_Value_Unit.Text = ds.Tables[0].Rows[0].ItemArray[21].ToString();
            txtFirst_Activity_Type.Text = ds.Tables[0].Rows[0].ItemArray[22].ToString();
            txtSec_Std_Value.Text = ds.Tables[0].Rows[0].ItemArray[23].ToString();
            txtSec_Std_Value_Unit.Text = ds.Tables[0].Rows[0].ItemArray[24].ToString();
            txtSec_Activity_Type.Text = ds.Tables[0].Rows[0].ItemArray[25].ToString();
            txtThird_Std_Value.Text = ds.Tables[0].Rows[0].ItemArray[26].ToString();
            txtThird_Std_Value_Unit.Text = ds.Tables[0].Rows[0].ItemArray[27].ToString();
            txtThird_Activity_Type.Text = ds.Tables[0].Rows[0].ItemArray[28].ToString();
            txtFourth_Std_Value.Text = ds.Tables[0].Rows[0].ItemArray[29].ToString();
            txtFourth_Std_Value_Unit.Text = ds.Tables[0].Rows[0].ItemArray[30].ToString();
            txtFourth_Activity_Type.Text = ds.Tables[0].Rows[0].ItemArray[31].ToString();
            txtFifth_Std_Value.Text = ds.Tables[0].Rows[0].ItemArray[32].ToString();
            txtFifth_Std_Value_Unit.Text = ds.Tables[0].Rows[0].ItemArray[33].ToString();
            txtFifth_Activity_Type.Text = ds.Tables[0].Rows[0].ItemArray[34].ToString();
            txtSixth_Std_Value.Text = ds.Tables[0].Rows[0].ItemArray[35].ToString();
            txtSixth_Std_Value_Unit.Text = ds.Tables[0].Rows[0].ItemArray[36].ToString();
            txtSixth_Activity_Type.Text = ds.Tables[0].Rows[0].ItemArray[37].ToString();
            txtValidFrom.Text = objUtil.GetDDMMYYYY(ds.Tables[0].Rows[0].ItemArray[38].ToString());
            txtValidTo.Text = objUtil.GetDDMMYYYY(ds.Tables[0].Rows[0].ItemArray[39].ToString());
            txtBase_Qty.Text = ds.Tables[0].Rows[0].ItemArray[40].ToString();
            plants = ds.Tables[0].Rows[0]["Plant_Id"].ToString();
            SetSelectedValue(ddlPlant, plants);
            if (isUserApprover)
            {
                txtMaterial.Enabled = false;
                txtProd_Version.Enabled = false;
                txtProfile.Enabled = false;
                txtTaskList_Desc.Enabled = false;
                txtRecipe.Enabled = false;
                txtStatus.Enabled = false;
                txtUsage.Enabled = false;
                txtBase_Quantity.Enabled = false;
                txtCharge_Quantity.Enabled = false;
                txtOperation_Quantity.Enabled = false;
                txtUOM.Enabled = false;
                txtSuperOrdinate_Operation.Enabled = false;
                txtControl_Recipe_Destination.Enabled = false;
                txtResource.Enabled = false;
                txtControl_Key.Enabled = false;
                txtStandardTextKey.Enabled = false;
                txtDescription.Enabled = false;
                chkRelevancy_To_Costing.Enabled = false;
                txtFirst_Std_Value.Enabled = false;
                txtFirst_Std_Value_Unit.Enabled = false;
                txtFirst_Activity_Type.Enabled = false;
                txtSec_Std_Value.Enabled = false;
                txtSec_Std_Value_Unit.Enabled = false;
                txtSec_Activity_Type.Enabled = false;
                txtThird_Std_Value.Enabled = false;
                txtThird_Std_Value_Unit.Enabled = false;
                txtThird_Activity_Type.Enabled = false;
                txtFourth_Std_Value.Enabled = false;
                txtFourth_Std_Value_Unit.Enabled = false;
                txtFourth_Activity_Type.Enabled = false;
                txtFifth_Std_Value.Enabled = false;
                txtFifth_Std_Value_Unit.Enabled = false;
                txtFifth_Activity_Type.Enabled = false;
                txtSixth_Std_Value.Enabled = false;
                txtSixth_Std_Value_Unit.Enabled = false;
                txtSixth_Activity_Type.Enabled = false;
                txtBase_Qty.Enabled = false;

                txtValidFrom.Enabled = false;
                txtValidTo.Enabled = false;
                ddlPlant.Enabled = false;
            }
        }


    }
    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplatePlantList(ddlPlant, lblMasterHeaderId.Text, "C1", lblCostingId.Text);
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
    //private bool SaveReceipe()
    //{
    //    bool Flag = false;
    //    Recipe ObjAcc = GetControlsValue();

    //    try
    //    {
    //        if (lblIsUserApprover.Text.ToUpper() != "TRUE")
    //        {
    //            lblReceipeId.Text = Convert.ToString(ObjReceipeAccess.Save(ObjAcc));

    //            if (Convert.ToInt16(lblReceipeId.Text) > 0)
    //            {
    //                Flag = true;
                    
    //            }
    //            else
    //            {
    //                lblMsg.Text = Messages.GetMessage(-1);
    //                pnlMsg.CssClass = "error";
    //                pnlMsg.Visible = true;
    //            }
    //        }
    //        else
    //        {
    //            Flag = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return Flag;
    //}
    //private Recipe GetControlsValue()
    //{
    //    Recipe ObjAcc = new Recipe();
    //    Utility objUtil = new Utility();

    //    ObjAcc.Recipe_Id = Convert.ToInt32(lblReceipeId.Text);
    //    ObjAcc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
    //    ObjAcc.PlantId = GetSelectedCheckedValue(ddlPlant);
    //    ObjAcc.Material = txtMaterial.Text;
    //    ObjAcc.Prod_Version = txtProd_Version.Text;
    //    ObjAcc.Profile = txtProfile.Text;
    //    ObjAcc.TaskList_Desc = txtTaskList_Desc.Text;
    //    ObjAcc.Recipef = txtRecipe.Text;
    //    ObjAcc.Status = txtStatus.Text;
    //    ObjAcc.Usage = txtUsage.Text;
    //    ObjAcc.Base_Quantity = txtBase_Quantity.Text;
    //    ObjAcc.Charge_Quantity = txtCharge_Quantity.Text;
    //    ObjAcc.Operation_Quantity = txtOperation_Quantity.Text;
    //    ObjAcc.UOM = txtUOM.Text;
    //    ObjAcc.SuperOrdinate_Operation = txtSuperOrdinate_Operation.Text;
    //    ObjAcc.Control_Recipe_Destination = txtControl_Recipe_Destination.Text;
    //    ObjAcc.Resource = txtResource.Text;
    //    ObjAcc.Control_Key = txtControl_Key.Text;
    //    ObjAcc.StandardTextKey = txtStandardTextKey.Text;
    //    ObjAcc.Description = txtDescription.Text;
    //    if (chkRelevancy_To_Costing.Checked == true)
    //    {
    //        ObjAcc.Relevancy_To_Costing = 1;
    //    }
    //    else
    //    {
    //        ObjAcc.Relevancy_To_Costing = 0;
    //    }
    //    ObjAcc.First_Std_Value = txtFirst_Std_Value.Text;
    //    ObjAcc.First_Std_Value_Unit = txtFirst_Std_Value_Unit.Text;
    //    ObjAcc.First_Activity_Type = txtFirst_Activity_Type.Text;
    //    ObjAcc.Sec_Std_Value = txtSec_Std_Value.Text;
    //    ObjAcc.Sec_Std_Value_Unit = txtSec_Std_Value_Unit.Text;
    //    ObjAcc.Sec_Activity_Type = txtSec_Activity_Type.Text;
    //    ObjAcc.Third_Std_Value = txtThird_Std_Value.Text;
    //    ObjAcc.Third_Std_Value_Unit = txtThird_Std_Value_Unit.Text;
    //    ObjAcc.Third_Activity_Type = txtThird_Activity_Type.Text;
    //    ObjAcc.Fourth_Std_Value = txtFourth_Std_Value.Text;
    //    ObjAcc.Fourth_Std_Value_Unit = txtFourth_Std_Value_Unit.Text;
    //    ObjAcc.Fourth_Activity_Type = txtFourth_Activity_Type.Text;
    //    ObjAcc.Fifth_Std_Value = txtFifth_Std_Value.Text;
    //    ObjAcc.Fifth_Std_Value_Unit = txtFifth_Std_Value_Unit.Text;
    //    ObjAcc.Fifth_Activity_Type = txtFifth_Activity_Type.Text;
    //    ObjAcc.Sixth_Std_Value = txtSixth_Std_Value.Text;
    //    ObjAcc.Sixth_Std_Value_Unit = txtSixth_Std_Value_Unit.Text;
    //    ObjAcc.Sixth_Activity_Type = txtSixth_Activity_Type.Text;
    //    ObjAcc.Base_Qty = txtBase_Qty.Text;

    //    ObjAcc.ValidFrom = Convert.ToDateTime(objUtil.GetMMDDYYYY(txtValidFrom.Text));
    //    ObjAcc.ValidTo = Convert.ToDateTime(objUtil.GetMMDDYYYY(txtValidTo.Text));


    //    ObjAcc.IsActive = 1;
    //    ObjAcc.UserId = lblUserId.Text;
    //    ObjAcc.TodayDate = objUtil.GetDate();
    //    ObjAcc.IPAddress = objUtil.GetIpAddress();

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
        catch
        {

            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (SaveReceipe())
        //{
        //    MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        //    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        //    bool flg = false;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
        //            {
        //                if (materialMasterAccess.SaveMaterialHeader(lblMasterHeaderId.Text, moduleId, lblUserId.Text, "M") > 0)
        //                {

        //                    flg = true;
        //                    scope.Complete();
        //                }
        //            }
        //        }
        //        if (flg)
        //        {
        //            lblMsg.Text = Messages.GetMessage(1);
        //            pnlMsg.CssClass = "success";
        //            pnlMsg.Visible = true;
        //            btnSubmit.Visible = false;
        //            //Response.Redirect("../Receipe/ReceipeMaster.aspx?pg=9", false);
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
        //}
    }
    protected void btnRollback_Click(object sender, EventArgs e)
    {
        if (RollbackRequest())
        {
            Response.Redirect("../Receipe/ReceipeMaster.aspx?pg=9", false);
        }
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
            throw ex;
        }
        return flg;
    }
}