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

public partial class Transaction_Receipe_ReceipeMaster : System.Web.UI.Page
{
    HelperAccess helperAccess = new HelperAccess();
    DataSet dstData = new DataSet();

    #region Page Events 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                PopulateDropDownList();
                ReadProfileWiseModules(userProfileId, lblUserId.Text);
                ReadRecipeMasterRequests();

                //if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                //{
                //    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                //    string menuId = Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                //    string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                //    string moduleId = "17";
                //    // UserRights(userProfileId, menuId);
                //    //ReadModules();
                //    btnNext.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text) ? false : true;

                //    ReadMaterialMasterRequests();
                //}
                //else
                //{

                //}
            }
        }
    }      

    protected void btnNext_Click(object sender, EventArgs e)
    {
        //VendorMasterAccess materialAccess = new VendorMasterAccess();
        //int masterHeaderId;
        //try
        //{
        //    masterHeaderId = materialAccess.SaveMaterialHeader("0", "17", lblUserId.Text, lblMode.Text);
        //    if (masterHeaderId > 0)
        //    {
        //        Session[StaticKeys.SelectedModuleId] = "17";
        //        Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
        //        Session[StaticKeys.Mode] = "N";
        //        Response.Redirect("Receipe.aspx?pgseq=1&sid=34");
        //    }
        //    else
        //    {

        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        RecipeAccess recipeAccess = new RecipeAccess();
        int masterHeaderId;
        int retValue;

        try
        {
            if (CheckedCreationFeilds())
            {
                string mode = lblMode.Text;
                masterHeaderId = recipeAccess.SaveMaterialHeader("0", ddlModule.SelectedValue, ddlPlantGroup.SelectedValue, lblUserId.Text, mode, ddlPlant.SelectedValue);
                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = ddlModule.SelectedValue;
                    Session[StaticKeys.MaterialPlantId] = ddlPlant.SelectedValue;

                    Session[StaticKeys.MaterialPlantName] = ddlPlant.SelectedItem.Text;

                    Session[StaticKeys.MatPlantGrp] = ddlPlantGroup.SelectedValue;
                    Session[StaticKeys.MassRequestProcessId] = "0";

                    Session[StaticKeys.SelectedModule] = ddlModule.SelectedItem.Text;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestNo] = recipeAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                    Response.Redirect("Reciepe.aspx");

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //lblPk.Text = GetSelectedPkID();
        //Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        //Session[StaticKeys.SelectedModuleId] = "17";
        //Session[StaticKeys.SelectedModule] = "";
        //Session[StaticKeys.Mode] = "V";
        //Session[StaticKeys.MaterialType] = "";
        //Session[StaticKeys.MaterialNo] = "";
        //Response.Redirect("../Receipe/Receipe.aspx?pgseq=1&sid=34");

        lblMode.Text = "V";
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";
        Response.Redirect("Reciepe.aspx");
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        //lblPk.Text = GetSelectedPkID();
        //Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        //Session[StaticKeys.SelectedModuleId] = "17";
        //Session[StaticKeys.SelectedModule] = "";
        //Session[StaticKeys.Mode] = "M";
        //Session[StaticKeys.MaterialType] = "";
        //Session[StaticKeys.MaterialNo] = "";
        //Response.Redirect("../Receipe/Receipe.aspx?pgseq=1&sid=34");
        //Response.Redirect("basicdata1.aspx?pgseq=1&sid=3");

        lblMode.Text = "M";
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";
        Response.Redirect("Reciepe.aspx");
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearch.PageIndex = e.NewPageIndex;
        ReadRecipeMasterRequests();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReadRecipeMasterRequests();
    }

    protected void btnMassSubmit_Click(object sender, EventArgs e)
    {
        string Req_Id = "";
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                CheckBox chkSelection = grv.FindControl("chkSelection") as CheckBox;
                if (chkSelection.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    Req_Id = Req_Id + lblRequestID.Text + "/";
                }
            }
            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (objRecipeAccess.GenerateMassRequestProcess(Req_Id, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
            {
                ReadRecipeMasterRequests();
                lblMsg.Text = "Request Generated Successfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
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
            throw ex;
        }

        //txtRequestNo.Text = Req_Id;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string Req_Id = "";
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                CheckBox chkSelection = grv.FindControl("chkSelection") as CheckBox;
                if (chkSelection.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    Req_Id = Req_Id + lblRequestID.Text + "/";
                }
            }

            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (objRecipeAccess.DeleteMassRequest(Req_Id, lblUserId.Text) > 0)
            {
                ReadRecipeMasterRequests();
                lblMsg.Text = "Request Deleted Successfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
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
            throw ex;
        }
    }

    #endregion
        
    #region Methods

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

                    Label lblMassRequestProcessId = grv.FindControl("lblMassRequestProcessId") as Label;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;

                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPlantId = grv.FindControl("lblPlantId") as Label;

                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;

                    Label lblPlantName = grv.FindControl("lblPlantName") as Label;


                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MassRequestProcessId] = lblMassRequestProcessId.Text;
                    //Session[StaticKeys.MaterialNo] = (lblMasterCode.Text == "" ? "New Request" : lblMasterCode.Text) + " - " + lblMaterialShortDescription.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;
                    Session[StaticKeys.MaterialPlantName] = lblPlantName.Text;

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

    private void PopulateDropDownList()
    {
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        {
            ddlStatus.Items.Insert(1, new ListItem("Pending For Review", "REV"));
            ddlStatus.Items.Insert(2, new ListItem("Pending For Final", "FIN"));
        }

        helperAccess.PopuplateDropDownList(ddlPlantGroup, "pr_GetAllPlantGroup", "Plant_group_Name", "Plant_Group_Id");
        ddlPlantGroup.SelectedValue = "1";

        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        
    }

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        BOMAccess objBOMAccess = new BOMAccess();
        Utility ObjUtil = new Utility();

        try
        {
            DataSet ds = objBOMAccess.ReadProfileWiseModules(profileId, userId, "R");

            ddlModule.DataSource = ds.Tables[0];
            ddlModule.DataTextField = "Module_Name";
            ddlModule.DataValueField = "Module_Id";
            ddlModule.DataBind();

            bool flg = true;

            if (ddlModule.Items.Count > 1)
            {
                ListItem lst = new ListItem("Ready for Submit", "SUB");
                ddlStatus.Items.Add(lst);
                ddlStatus.SelectedValue = "SUB";


                dstData = objBOMAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, "", "R", "", ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));

                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    flg = false;

                    // Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('";
                        if (Session[StaticKeys.AddAlertMsg] != null)
                        {
                            if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                            {
                                cstext += Session[StaticKeys.AddAlertMsg].ToString() + "\\n\\n";
                                Session[StaticKeys.AddAlertMsg] = null;
                            }
                        }

                        //cstext += "Please select the request(s) and click on Mass Submit to send the request(s) for Mass processing.');";
                        cstext += "Please tick(towards right end) in front of the finalized request(s).\\nClick on Mass Submit to send the request(s) for processing.');";
                        //String cstext = "if(confirm('Is request processing Complete?')){RequestSubmitPage();};";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }
                else
                {
                    ddlStatus.SelectedValue = "P";
                    if (Session[StaticKeys.AddAlertMsg] != null)
                    {
                        if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                        {
                            // Check to see if the startup script is already registered.
                            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                            {
                                String cstext = "alert('" + Session[StaticKeys.AddAlertMsg].ToString() + "');";
                                Session[StaticKeys.AddAlertMsg] = null;
                                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                            }
                        }
                    }
                }
            }

            if (flg)
            {
                if (Session[StaticKeys.SearchStatus] != null)
                {
                    ddlStatus.SelectedValue = Session[StaticKeys.SearchStatus].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadRecipeMasterRequests()
    {
        RecipeAccess objRecipeAccess = new RecipeAccess();
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;
        try
        {
            dstData = objRecipeAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, "", "R", "", ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            btnMassSubmit.Visible = false;
            btnDelete.Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    //btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = true;
                    //btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = true;
                    btnModify.Visible = true;
                    //btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                    btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    //btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = true;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = false;
                    //btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[0].Visible = true;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    btnModify.Visible = true;
                    //btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "SUB")
                {
                    //grdSearch.Columns[0].Visible = false;
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;

                    grdSearch.AllowPaging = false;
                    btnDelete.Visible = true;
                    btnMassSubmit.Visible = true;
                }

            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                //btnCopyRequest.Visible = false;
            }

            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected bool CheckedCreationFeilds()
    {
        try
        {
            lblMsg.Text = "";
            if (reqddlModule.Visible && ddlModule.SelectedValue == "")
                lblMsg.Text = "Plant type is Mandatory. ";
            if (reqddlPlant.Visible && ddlPlant.SelectedValue == "")
                lblMsg.Text += "Plant is Mandatory. ";

            if (lblMsg.Text == "")
            {
                return true;
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "key", "ShowCreateNewDialog();", true);
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }      

    #endregion
}