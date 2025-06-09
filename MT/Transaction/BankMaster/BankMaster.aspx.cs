using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using Accenture.MWT.DomainObject;

public partial class Transaction_Vendor_BankMaster : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                BindBankModules();
                PopulateDropDownList();
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                {
                    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    string menuId = Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                    string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string moduleId = ddlBankMasterModule.SelectedValue;
                    ReadMaterialMasterRequests();
                }
                else
                {

                }
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        VendorMasterAccess materialAccess = new VendorMasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = materialAccess.SaveMaterialHeader("0",ddlCompany.SelectedValue, ddlVendorAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlVendorAccGroup.SelectedValue;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Response.Redirect("BankData.aspx");
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeRequest(GetSelectedPkID(), lblUserId.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "N";
            Session[StaticKeys.SelectedModule] = objMasterAccess.mModuleName.ToString();
            Session[StaticKeys.MaterialNo] = objMasterAccess.mMasterSAPCode.ToString();
            Response.Redirect("BankData.aspx");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";
        //Session[StaticKeys.MaterialNo] = "";
        Response.Redirect("BankData.aspx");
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";
        //Session[StaticKeys.MaterialNo] = "";
        Response.Redirect("BankData.aspx");
    }

    void BindBankModules()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        ddlBankMasterModule.DataSource = objMatAccess.ReadModules("A");
        ddlBankMasterModule.DataValueField = "Module_Id";
        ddlBankMasterModule.DataTextField = "Module_Name";
        ddlBankMasterModule.DataBind();
        ddlBankMasterModule.Items.Insert(0, new ListItem("All", "0"));
    }

    #region Vendor Search

    public void PopulateDropDownList()
    {
        try
        {
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

            ddlVendorAccGroup.DataSource = objMatAccess.ReadModules("A");
            ddlVendorAccGroup.DataTextField = "Module_Name";
            ddlVendorAccGroup.DataValueField = "Module_Id";
            ddlVendorAccGroup.DataBind();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReadMaterialMasterRequests();
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        DataSet dstData = new DataSet();
        dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlBankMasterModule.SelectedValue, "A", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
        grdSearch.DataSource = dstData.Tables[0].DefaultView;
        grdSearch.PageIndex = e.NewPageIndex;
        grdSearch.DataBind();

    }

    private void ReadMaterialMasterRequests()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        DataSet dstData = new DataSet();
        try
        {
            dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlBankMasterModule.SelectedValue, "A", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;
            grdSearch.DataBind();

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;
                //btnChangeRequest.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                }
                if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = true;
                    btnChangeRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = true;
                    btnChangeRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = false;
                    btnChangeRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = false;
                    btnChangeRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = true;
                    btnChangeRequest.Visible = false;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnChangeRequest.Visible = false;
            }
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
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;

                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.MaterialNo] = lblMasterCode.Text;
                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
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