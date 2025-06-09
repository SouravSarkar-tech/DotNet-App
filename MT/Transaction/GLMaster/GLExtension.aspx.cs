using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;

public partial class Transaction_GLMaster_GLExtension : System.Web.UI.Page
{
    GLExtensionAccess ObjGLExtensionAccess = new GLExtensionAccess();
    HelperAccess helperAccess = new HelperAccess();
    GLExtension objSavedExtnData = new GLExtension();

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
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

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

                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0)
                    {
                        pnlData.Visible = true;
                        lnlAddDetails.Visible = true;
                    }
                }
                else
                {
                    grvData.Columns[1].Visible = false;
                    lnlAddDetails.Visible = false;
                    pnlData.Visible = false;
                }
                ClearData();
                FillFormDataByMHId();

            }
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);

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
            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0)
                pnlData.Visible = true;

            Response.Redirect("GLExtension.aspx");

        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Session[StaticKeys.AddAlertMsg] = "GL Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
        Response.Redirect("GLMaster.aspx");
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        string GL_Extension_Id = grvData.DataKeys[grdrow.RowIndex]["GL_Extension_Id"].ToString();
        //Carve_LC17&LC23_8400000406 added
		Session[StaticKeys.MaterialProcessModuleId] = grvData.DataKeys[grdrow.RowIndex]["GLGroup"].ToString();
        //Carve_LC17&LC23_8400000406 added
		if (ObjGLExtensionAccess.DeleteGLExtensionData(SafeTypeHandling.ConvertStringToInt32(GL_Extension_Id)) > 0)
        {
            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            txtGLCode.Text = "";
            txtRefCompanyCode.Text = "";
            txtRemarks.Text = "";
            txtGLCode.Enabled = true;
            txtRefCompanyCode.Enabled = true;
            txtRemarks.Enabled = true;
        }

        FillFormDataByMHId();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMatExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["GL_Extension_Id"].ToString();
//Carve_LC17&LC23_8400000406 added
            Session[StaticKeys.MaterialProcessModuleId] = grvData.DataKeys[grdrow.RowIndex]["GLGroup"].ToString();
//Carve_LC17&LC23_8400000406 added
            FillControlData();
            pnlData.Visible = true;
            txtGLCode.Enabled = false;

            lblMatExtensionId.Enabled = false;
            txtGLCode.Enabled = false;
            txtRefCompanyCode.Enabled = false;
            txtRemarks.Enabled = false;
            btnSave.Enabled = false;
            btnNext.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        LinkButton lnkView = (LinkButton)sender;
        GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
        lblMatExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["GL_Extension_Id"].ToString();
//Carve_LC17&LC23_8400000406 added
        Session[StaticKeys.MaterialProcessModuleId] = grvData.DataKeys[grdrow.RowIndex]["GLGroup"].ToString();
        //Carve_LC17&LC23_8400000406 added
        FillControlData();
        
        txtGLCode.Enabled = true;
    }

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void txtGLCode_TextChanged(object sender, EventArgs e)
    {
        if (Session[StaticKeys.MaterialProcessModuleId] != null)
        {
            pnlMsg.Visible = false;
            if (Session[StaticKeys.MaterialProcessModuleId].ToString() != MaterialHelper.GetGLAccGrpByMaterialCode(txtGLCode.Text))
            {
                lblMsg.Text = "Please enter only " + ddlGLAccGrp.SelectedItem.Text;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtGLCode.Text = "";
            }
        }
        else
        {
            ddlGLAccGrp.SelectedValue = MaterialHelper.GetGLAccGrpByMaterialCode(txtGLCode.Text);
        }
        BindValuationClass();

        ConfigureControl();
    }

    #endregion

    #region Public Method

    protected void ClearData()
    {
        lblMatExtensionId.Text = "0";

        txtGLCode.Text = "";
        txtRefCompanyCode.Text = "";

        txtGLCode.Enabled = true;
        lblMatExtensionId.Enabled = true;
        txtGLCode.Enabled = true;
        txtRefCompanyCode.Enabled = true;
        txtRemarks.Enabled = true;
        btnSave.Enabled = true;
        btnNext.Enabled = true;
        txtRemarks.Text = "";

        PopuplateDropDownList();

    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id", "");
        //ddlCompany.SelectedValue = "32";
        ddlCompany.SelectedValue = "32";
        //Carve_LC17&LC23 added
        try
        {
            GLExtension ObjGLExtCompany = ObjGLExtensionAccess.GetGLExtCompany(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ObjGLExtCompany.Master_Header_Id > 0)
            {
                ddlCompany.SelectedValue = ObjGLExtCompany.Company_Code;
            }
        }
        catch(Exception ex) { }
        //Carve_LC17&LC23 added
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        DataSet ds;
        ds = objMatAccess.ReadModules("G");


        ddlGLAccGrp.DataSource = ds;
        ddlGLAccGrp.DataTextField = "Module_Name";
        ddlGLAccGrp.DataValueField = "Module_Id";
        ddlGLAccGrp.DataBind();

        ddlGLAccGrp.Items.Insert(0, new ListItem("---Select---", "0"));

    }

    private void FillFormDataByMHId()
    {
        DataSet dataSet = ObjGLExtensionAccess.GetGLExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));

        grvData.DataSource = dataSet;
        grvData.DataBind();

        FillControlData();

    }

    private void MaterialTypeSelection()
    {
        if (Session[StaticKeys.MaterialProcessModuleId] != null)
        {
            pnlMsg.Visible = false;
            if (Session[StaticKeys.MaterialProcessModuleId].ToString() != MaterialHelper.GetGLAccGrpByMaterialCode(txtGLCode.Text))
            {
                lblMsg.Text = "Please enter only " + ddlGLAccGrp.SelectedItem.Text;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtGLCode.Text = "";
            }
        }
        else
        {
            ddlGLAccGrp.SelectedValue = MaterialHelper.GetGLAccGrpByMaterialCode(txtGLCode.Text);
        }
        BindValuationClass();

        //if (Session[StaticKeys.MaterialProcessModuleId].ToString() == "139" || Session[StaticKeys.MaterialProcessModuleId].ToString() == "144" || Session[StaticKeys.MaterialProcessModuleId].ToString() == "145" || Session[StaticKeys.MaterialProcessModuleId].ToString() == "171")
        //{
        //}
        //else
        //{
        //}
        ConfigureControl();
    }

    private bool CheckMatValid(string GL_Code, string Company_Code)
    {
        bool flg = true;

        foreach (GridViewRow grv in grvData.Rows)
        {
            Label lblGLCode = (Label)grv.FindControl("lblGLCode");
            Label lblCompanyCode = (Label)grv.FindControl("lblCompanyCode");
            if (lblGLCode.Text == GL_Code && lblCompanyCode.Text == Company_Code)
            {
                flg = false;
                break;
            }
        }

        return flg;
    }

    private bool Save()
    {
        bool flg = false;
        try
        {
            GLExtension ObjGLExtension = GetControlsValue();
            objSavedExtnData = ObjGLExtensionAccess.GetGLExtension(Convert.ToInt32(ObjGLExtension.GL_Extension_Id));

            if (ObjGLExtension.GL_Code != null)
            {
                if (CheckMatValid(ObjGLExtension.GL_Code, ObjGLExtension.Company_Code) || ObjGLExtension.GL_Extension_Id > 0)
                {
                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                    {
                        if (objSavedExtnData.GL_Extension_Id > 0)
                        {
                            CheckIfChanges(ObjGLExtension, objSavedExtnData);
                        }
                    }

                    if (ObjGLExtensionAccess.Save(ObjGLExtension) > 0)
                    {
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
                else
                {
                    lblMsg.Text = "Material/Plant/Stor. Loc. already exists, please enter another combination.";
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

    private void CheckIfChanges(GLExtension NewExtnsnData, GLExtension oldExtnsnData)
    {
        try
        {
            if (NewExtnsnData.GL_Extension_Id > 0 && oldExtnsnData.GL_Extension_Id > 0)
            {
                if (NewExtnsnData.GL_Code != oldExtnsnData.GL_Code)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>GL Code</td> <td>" + oldExtnsnData.GL_Code + "</td><td>" + NewExtnsnData.GL_Code + "</td></tr>";
                if (NewExtnsnData.Company_Code != oldExtnsnData.Company_Code)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Company Code</td> <td>" + oldExtnsnData.Company_Code + "</td><td>" + NewExtnsnData.Company_Code + "</td></tr>";
                if (NewExtnsnData.Ref_Company_Code != oldExtnsnData.Ref_Company_Code)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Reference Company Code</td> <td>" + oldExtnsnData.Ref_Company_Code + "</td><td>" + NewExtnsnData.Ref_Company_Code + "</td></tr>";
                if (NewExtnsnData.Remarks != oldExtnsnData.Remarks)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldExtnsnData.Remarks + "</td><td>" + NewExtnsnData.Remarks + "</td></tr>";
            }

            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private GLExtension GetControlsValue()
    {
        GLExtension ObjGLExtension = new GLExtension();
        Utility objUtil = new Utility();

        try
        {
            ObjGLExtension.GL_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMatExtensionId.Text);
            ObjGLExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);

            ObjGLExtension.GL_Code = txtGLCode.Text;
            ObjGLExtension.Company_Code = ddlCompany.SelectedValue;
            ObjGLExtension.Ref_Company_Code = txtRefCompanyCode.Text;

            ObjGLExtension.Remarks = txtRemarks.Text;

            ObjGLExtension.UserId = lblUserId.Text;
            ObjGLExtension.TodayDate = objUtil.GetDate();
            ObjGLExtension.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjGLExtension;
    }

    public void BindValuationClass()
    {
        string AccountCat = MaterialHelper.GetAccountCategoryByModuleId(ddlCompany.SelectedValue);
    }

    private void FillControlData()
    {
        Utility objUtil = new Utility();

        GLExtension ObjGLExtension = ObjGLExtensionAccess.GetGLExtension(Convert.ToInt32(lblMatExtensionId.Text));

        GLExtension ObjGLExtCompany = ObjGLExtensionAccess.GetGLExtCompany(Convert.ToInt32(lblMasterHeaderId.Text));

        try
        {
            if (ObjGLExtension.GL_Extension_Id > 0)
            {
                txtGLCode.Text = ObjGLExtension.GL_Code;
                ddlCompany.SelectedValue = ObjGLExtension.Company_Code;
                txtRefCompanyCode.Text = ObjGLExtension.Ref_Company_Code;
                ddlGLAccGrp.SelectedValue = ObjGLExtension.GLGroup;
                txtGLCode.Enabled = false;
                txtRemarks.Text = ObjGLExtension.Remarks;

                ConfigureControl();

            }
            else
            {
                if (ObjGLExtCompany.Master_Header_Id > 0)
                {
                     ddlCompany.SelectedValue = ObjGLExtCompany.Company_Code;
                }
                if (Session[StaticKeys.MaterialProcessModuleId] != null)
                {
                    ddlGLAccGrp.SelectedValue = Session[StaticKeys.MaterialProcessModuleId].ToString();
                }

            }

            ddlCompany.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ConfigureControl()
    {
        MasterAccess ObjMasterAccess = new MasterAccess();

        if (ddlGLAccGrp.SelectedValue != "")
        {
            Session[StaticKeys.SelectedModulePlantGrp] = ObjMasterAccess.GetSelectedModulePlantGrpByPlantGrp("1", ddlGLAccGrp.SelectedValue, "G");

            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        }
    }

    #endregion

}