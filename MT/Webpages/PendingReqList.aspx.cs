using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Webpages_PendingReqList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (Request.QueryString["seq"] != null)
            {
                if (!IsPostBack)
                {
                    ReadModules();
                    ReadPendingReqDetail();
                }
            }
        }
    }

    protected void ddlMaterialModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReadPendingReqDetail();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../shared/home.aspx");
    }

    private void ReadPendingReqDetail()
    {
        DashBoard objAccess = new DashBoard();
        DataSet dstData = new DataSet();
        string seq = Request.QueryString["seq"].ToString();
        string moduleType = Request.QueryString["mt"].ToString();
        if (Request.QueryString["mid"].ToString() != null)
        {
            try
            {
                ddlMaterialModule.SelectedValue = Request.QueryString["mid"].ToString();
            }
            catch{}
        }

        try
        {
            dstData = objAccess.ReadPendingReqDetail(ddlMaterialModule.SelectedValue, seq, moduleType);
            if (moduleType == "M")
            {
                grdMaterial.DataSource = dstData.Tables[0].DefaultView;
                grdMaterial.DataBind();
                grdMaterial.Visible = true;
                lblHeader.Text = "Material Master";
                lblType.Text = "Material Type";
                trDDL.Visible = true;
            }
            else if (moduleType == "C")
            {
                grdCust.DataSource = dstData.Tables[0].DefaultView;
                grdCust.DataBind();
                grdCust.Visible = true;
                lblHeader.Text = "Customer Master";
                lblType.Text = "Account Group";
                trDDL.Visible = true;
            }
            else if (moduleType == "V")
            {
                grdVendor.DataSource = dstData.Tables[0].DefaultView;
                grdVendor.DataBind();
                grdVendor.Visible = true;
                lblHeader.Text = "Vendor Master";
                trDDL.Visible = true;
            }
            else if (moduleType == "B")
            {
                grdBom.DataSource = dstData.Tables[0].DefaultView;
                grdBom.DataBind();
                grdBom.Visible = true;
                trDDL.Visible = false;
                lblHeader.Text = "BOM Master";
            }
            else if (moduleType == "R")
            {
                grdRecipe.DataSource = dstData.Tables[0].DefaultView;
                grdRecipe.DataBind();
                grdRecipe.Visible = true;
                trDDL.Visible = false;
                lblHeader.Text = "Recipe Master";
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
        string moduleType = Request.QueryString["mt"].ToString();
        try
        {
            ddlMaterialModule.DataSource = objMatAccess.ReadModules(moduleType);
            ddlMaterialModule.DataTextField = "Module_Name";
            ddlMaterialModule.DataValueField = "Module_Id";
            ddlMaterialModule.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}