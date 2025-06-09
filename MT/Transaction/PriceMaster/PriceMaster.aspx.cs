using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Transaction_PriceMaster_PriceMaster : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                //if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                //{
                //    string menuId = Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                    // UserRights(userProfileId, menuId);
                    ReadModules();
                    ReadProfileWiseModules(userProfileId, lblUserId.Text);
                    ReadPriceMasterRequests();
                //}
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReadPriceMasterRequests();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        try
        {
            Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
            Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
            masterHeaderId = materialAccess.SaveMaterialHeaderPM("0", ddlModule.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                Response.Redirect("MaterialBatchPrice.aspx", false);
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

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        //Session[StaticKeys.SelectedModuleId] = ddlModuleSearch.SelectedValue;
        //Session[StaticKeys.SelectedModule] = ddlModuleSearch.SelectedItem.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";

        Response.Redirect("MaterialBatchPrice.aspx", false);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        //Session[StaticKeys.SelectedModuleId] = ddlModuleSearch.SelectedValue;
        //Session[StaticKeys.SelectedModule] = ddlModuleSearch.SelectedItem.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";
        Response.Redirect("MaterialBatchPrice.aspx");
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
            Session[StaticKeys.SelectedModule] = objMasterAccess.mModuleName.ToString();
            Session[StaticKeys.MaterialNo] = objMasterAccess.mMasterSAPCode.ToString();
            Response.Redirect("MaterialBatchPrice.aspx");
        }
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearch.PageIndex = e.NewPageIndex;
        ReadPriceMasterRequests();
    }
    #endregion

    #region Private Functions
    private void ReadPriceMasterRequests()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        DataSet dstData = new DataSet();
        try
        {
            dstData = objMatAccess.ReadMaterialMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "P", txtSAPCode.Text.Trim());

            if (dstData.Tables.Count > 0)
            {
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    grdSearch.DataSource = dstData.Tables[0].DefaultView;
                    grdSearch.DataBind();

                    btnView.Visible = true;
                    btnModify.Visible = true;
                    btnChangeRequest.Visible = false;

                    if (ddlStatus.SelectedValue == "C")
                    {
                        grdSearch.Columns[8].Visible = false;
                        grdSearch.Columns[9].Visible = true;
                        btnModify.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "I")
                    {
                        grdSearch.Columns[8].Visible = false;
                        //grdSearch.Columns[7].Visible = true;
                        btnModify.Visible = true;
                        btnChangeRequest.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "R")
                    {
                        grdSearch.Columns[8].Visible = true;
                        grdSearch.Columns[9].Visible = false;
                        btnModify.Visible = true;
                        btnChangeRequest.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "P")
                    {
                        grdSearch.Columns[8].Visible = false;
                        grdSearch.Columns[9].Visible = false;
                        btnModify.Visible = true;
                        btnChangeRequest.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "REJ")
                    {

                        grdSearch.Columns[8].Visible = true;
                        grdSearch.Columns[9].Visible = true;
                        btnModify.Visible = false;
                        //btnCopyRequest.Visible = false;
                        grdSearch.AllowPaging = true;
                    }
                    else if (ddlStatus.SelectedValue == "ALL")
                    {
                        grdSearch.Columns[0].Visible = true;
                        //grdSearch.Columns[8].Visible = true;
                        //grdSearch.Columns[9].Visible = true;
                        btnModify.Visible = false;
                        //btnCopyRequest.Visible = true;
                        grdSearch.AllowPaging = true;
                    }
                    
                }
                else
                {
                    btnView.Visible = false;
                    btnModify.Visible = false;
                    btnChangeRequest.Visible = false;
                    grdSearch.DataSource = dstData.Tables[0].DefaultView;
                    grdSearch.DataBind();
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnChangeRequest.Visible = false;
                grdSearch.DataSource = dstData.Tables[0].DefaultView;
                grdSearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadModules()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            ddlModuleSearch.DataSource = objMatAccess.ReadModules("P");
            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void ReadProfileWiseModules(string profileId, string userId)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            ddlModule.DataSource = objMatAccess.ReadProfileWiseModules(profileId, userId, "P");
            ddlModule.DataTextField = "Module_Name";
            ddlModule.DataValueField = "Module_Id";
            ddlModule.DataBind();
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
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPendingFor = grv.FindControl("lblPendingFor") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    //Session[StaticKeys.MaterialNo] = lblMasterCode.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.PendingFor] = lblPendingFor.Text;
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