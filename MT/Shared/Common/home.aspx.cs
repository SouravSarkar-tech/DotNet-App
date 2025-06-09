using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using System.Web.UI.HtmlControls;
using log4net;
public partial class Shared_Home : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    DashBoard ObjDashBoard = new DashBoard();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    PopulateDropDownList();
                    if (ddlMaterialModule.Items.Count > 0)
                    {
                        FillDashBoard(ddlMaterialModule.SelectedValue, rptMaterialDashBoard);
                    }
                    else
                    {
                        pnlDashBoard.Visible = false;
                    }
                    if (ddlCustomer.Items.Count > 0)
                    {
                        //FillDashBoard(ddlCustomer.SelectedValue, rptCustomerashBoard);
                        pnlCustomer.Visible = false;
                    }
                    else
                    {
                        pnlCustomer.Visible = false;
                    }
                    if (ddlVenderModule.Items.Count > 0)
                    {
                        FillDashBoard(ddlVenderModule.SelectedValue, rptVendorDashboard);
                    }
                    else
                    {
                        pnlVendor.Visible = false;
                    }
                    if (ddlBomModule.Items.Count > 0)
                    {
                        FillDashBoard(ddlBomModule.SelectedValue, rptBomDashboard);
                    }
                    else
                    {
                        pnlBomDashboard.Visible = false;
                    }

                    if (ddlReceipeModule.Items.Count > 0)
                    {
                        FillDashBoard(ddlReceipeModule.SelectedValue, rptRecipeDashboard);
                    }
                    else
                    {
                        pnlRecipe.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("../login.aspx");
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
    protected void ddlMaterialModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDashBoard(ddlMaterialModule.SelectedValue, rptMaterialDashBoard);
        }
        catch (Exception ex)
        { _log.Error("ddlMaterialModule_SelectedIndexChanged", ex); }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDashBoard(ddlCustomer.SelectedValue, rptCustomerashBoard);
        }
        catch (Exception ex)
        { _log.Error("ddlCustomer_SelectedIndexChanged", ex); }
    }

    protected void ddlVenderModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDashBoard(ddlVenderModule.SelectedValue, rptVendorDashboard);
        }
        catch (Exception ex)
        { _log.Error("ddlVenderModule_SelectedIndexChanged", ex); }
    }

    protected void ddlBomModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDashBoard(ddlBomModule.SelectedValue, rptBomDashboard);
        }
        catch (Exception ex)
        { _log.Error("ddlBomModule_SelectedIndexChanged", ex); }
    }
    protected void ddlReceipeModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDashBoard(ddlReceipeModule.SelectedValue, rptRecipeDashboard);
        }
        catch (Exception ex)
        { _log.Error("ddlReceipeModule_SelectedIndexChanged", ex); }
    }

    private void PopulateDropDownList()
    {
        try
        {
            string UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
            string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

            ddlMaterialModule.DataSource = ObjDashBoard.ReadAllModules("M");
            ddlMaterialModule.DataTextField = "Module_Name";
            ddlMaterialModule.DataValueField = "Module_Id";
            ddlMaterialModule.DataBind();

            ddlCustomer.DataSource = ObjDashBoard.ReadAllModules("C");
            ddlCustomer.DataTextField = "Module_Name";
            ddlCustomer.DataValueField = "Module_Id";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("ALL", "0"));

            ddlVenderModule.DataSource = ObjDashBoard.ReadAllModules("V");
            ddlVenderModule.DataTextField = "Module_Name";
            ddlVenderModule.DataValueField = "Module_Id";
            ddlVenderModule.DataBind();
            ddlVenderModule.Items.Insert(0, new ListItem("ALL", "0"));

            ddlBomModule.DataSource = ObjDashBoard.ReadAllModules("B");
            ddlBomModule.DataTextField = "Module_Name";
            ddlBomModule.DataValueField = "Module_Id";
            ddlBomModule.DataBind();
            //ddlBomModule.Items.Insert(0, new ListItem("ALL", "0"));

            //ddlReceipeModule.DataSource = ObjDashBoard.ReadAllModules("R");
            //ddlReceipeModule.DataTextField = "Module_Name";
            //ddlReceipeModule.DataValueField = "Module_Id";
            //ddlReceipeModule.DataBind();
        }
        catch (Exception ex)
        { _log.Error("PopulateDropDownList", ex); }
    }

    private void FillDashBoard(string ModuleId, Repeater rptDashBoard)
    {
        try
        {
            DataTable Dt1;
            Dt1 = ObjDashBoard.GetDashBoardByModuleId(ModuleId, lblUserId.Text);
            rptDashBoard.DataSource = Dt1;
            rptDashBoard.DataBind();
            FillrptWorkflow(Dt1, rptDashBoard, ModuleId);
        }
        catch (Exception ex)
        { _log.Error("FillDashBoard", ex); }

    }

    protected void FillrptWorkflow(DataTable dt, Repeater rptWorkflow, string moduleId)
    {
        try
        {
            MaterialMasterAccess objAccess = new MaterialMasterAccess();
            Label lblWorkflow;
            Label lblTotalPendings;
            HtmlTableCell tdimg = new HtmlTableCell();
            HtmlAnchor lnkRedirect = new HtmlAnchor();
            string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();
            string tempDeptId = string.Empty;
            string normalDashboardBG = "normalDashboardBG";
            string alertDashboardBG = "alertDashboardBG";
            string ModuleType = string.Empty;
            string ModuleId = string.Empty;

            if (rptWorkflow.ID == "rptMaterialDashBoard")
            {
                ModuleType = "M";
                ModuleId = ddlMaterialModule.SelectedValue.ToString();
            }
            else if (rptWorkflow.ID == "rptCustomerashBoard")
            {
                ModuleType = "C";
                ModuleId = ddlCustomer.SelectedValue.ToString();
            }
            else if (rptWorkflow.ID == "rptVendorDashboard")
            {
                ModuleType = "V";
                ModuleId = ddlVenderModule.SelectedValue.ToString();
            }
            else if (rptWorkflow.ID == "rptBomDashboard")
            {
                ModuleType = "B";
                ModuleId = ddlBomModule.SelectedValue.ToString();
            }
            else if (rptWorkflow.ID == "rptRecipeDashboard")
            {
                ModuleType = "R";
                ModuleId = ddlReceipeModule.SelectedValue.ToString();
            }





            bool isUserInitiator = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text);
            if (!isUserInitiator)
            {
                userDeptId = "0";
            }

            for (int i = 0; i < rptWorkflow.Items.Count; i++)
            {

                tdimg = (HtmlTableCell)rptWorkflow.Items[i].FindControl("tdimg");
                lnkRedirect = (HtmlAnchor)rptWorkflow.Items[i].FindControl("lnkRedirect");
                lblWorkflow = (Label)rptWorkflow.Items[i].FindControl("lblWorkflow");
                lblTotalPendings = (Label)rptWorkflow.Items[i].FindControl("lblTotalPendings");

                lblWorkflow.Text = dt.Rows[i]["Department_Name"].ToString();
                tempDeptId = dt.Rows[i]["Department_Id"].ToString();

                if (userProfileId != "1")
                {
                    if (userDeptId == tempDeptId)
                    {
                        lblTotalPendings.Text = "Pending: " + dt.Rows[i]["Pending"].ToString();
                        normalDashboardBG = "normalDashboardBG";
                        alertDashboardBG = "alertDashboardBG";
                    }
                    else
                    {
                        lnkRedirect.HRef = "";
                        lblTotalPendings.Text = "&nbsp;";
                        normalDashboardBG = "normalDashboardBG";
                        alertDashboardBG = "normalDashboardBG";
                    }
                }
                else
                {
                    lnkRedirect.HRef = "../../webpages/PendingReqList.aspx?mt=" + ModuleType + "&seq=" + (i + 1).ToString() + "&mid=" + ModuleId;
                    lblTotalPendings.Text = "Pending: " + dt.Rows[i]["Pending"].ToString();
                }

                switch (dt.Rows[i]["Pending"].ToString())
                {
                    case "0":
                        tdimg.Attributes["class"] = normalDashboardBG;
                        break;
                    default:
                        tdimg.Attributes["class"] = alertDashboardBG;
                        break;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("FillrptWorkflow", ex); }
    }



}
//lblWorkflow.Text = "<div class='normalDashboardBG'>" + dt.Rows[i]["Department_Name"].ToString() + "<br/> WorkFlow Seq : " + dt.Rows[i]["Department_Name"].ToString() + "<br/> Pending : " + dt.Rows[i]["Pending"].ToString()+"</div>";
