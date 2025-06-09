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
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class Transaction_Vendor_VendorMaster : BasePage
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

                BindVendorGroup();
                PopulateDropDownList(userProfileId);

                //if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                //{
                //    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                //    string menuId = Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                //    string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                //    string moduleId = ddlVendorAccGrp.SelectedValue;
                //    ReadMaterialMasterRequests();
                //}
                //else
                //{
                ReadMaterialMasterRequests();
                //}
                //Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification
                pnlMsg.CssClass = "warning";
                lblMsg.Text = "Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.";
                //End
            }

            ShowHideBtn();
        }
    }


    private void ShowHideBtn()
    {
        if (Convert.ToString(Session[StaticKeys.LoggedIn_User_Profile_Id]) == "2")
        {
            btnCreateNew.Attributes.Add("enabled", "enabled");
            btnChangeBulkRequest.Attributes.Add("enabled", "enabled");
            btnBlockRequest.Enabled = true;
            btnCopyRequest.Enabled = true;
        }
        else
        {

            btnCreateNew.Attributes.Add("disabled", "disabled");
            btnChangeBulkRequest.Attributes.Add("disabled", "disabled");
            btnBlockRequest.Enabled = false;
            btnCopyRequest.Enabled = false;
        }
    }

    protected void txtVendorCode_TextChanged(object sender, EventArgs e)
    {
        txtVendorCode.Text = txtVendorCode.Text.ToUpper();
        string str = txtVendorCode.Text.Substring(0, 1).ToUpper();
        string str1 = txtVendorCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtVendorCode.Text);
        //8400000388 S
        if (str1 == "LP")
        {
            str = "H";
        }
        //8400000388 E
        switch (str)
        {//8400000388 S
            case "H":
                regtxtVendorCode.ValidationExpression = "^[\\S]{4,10}$";
                ddlVendorAccGroupC.SelectedValue = "241";
                break;
            //8400000388 E
            case "L":
                regtxtVendorCode.ValidationExpression = "^[\\S]{4}$";
                ddlVendorAccGroupC.SelectedValue = "26";

                break;
            case "E":
                regtxtVendorCode.ValidationExpression = "^[\\S]{7,10}$";
                ddlVendorAccGroupC.SelectedValue = "27";

                break;

            default:
                regtxtVendorCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 150000) //1-Series
                    ddlVendorAccGroupC.SelectedValue = "18";
                else if (strcode >= 150000 && strcode < 200000)//15-series
                    ddlVendorAccGroupC.SelectedValue = "19";
                else if (strcode >= 200000 && strcode < 250000)//2-series
                    ddlVendorAccGroupC.SelectedValue = "20";
                else if (strcode >= 250000 && strcode < 300000)//25-series
                    ddlVendorAccGroupC.SelectedValue = "21";
                else if (strcode >= 300000 && strcode < 350000)//3-series
                    ddlVendorAccGroupC.SelectedValue = "22";
                else if (strcode >= 400000 && strcode < 450000)//4-Series
                    ddlVendorAccGroupC.SelectedValue = "24";
                else if (strcode >= 450000 && strcode < 500000)//45-Series
                    ddlVendorAccGroupC.SelectedValue = "25";
                else if ((strcode >= 600000 && strcode < 650000) || (strcode >= 678001 && strcode < 699999))//6-Series & 67-series
                    ddlVendorAccGroupC.SelectedValue = "28";
                else if (strcode >= 650000 && strcode < 700000)//65-Series
                    ddlVendorAccGroupC.SelectedValue = "30";
                else if (strcode >= 720000 && strcode < 730000)//72-Series
                    ddlVendorAccGroupC.SelectedValue = "32";
                else if (strcode >= 900000 && strcode < 999999)//9-Series
                    ddlVendorAccGroupC.SelectedValue = "15";
                break;
        }
        txtVendorName.Focus();
    }

    protected void btnbackMsg_Click(object sender, EventArgs e)
    {
        string url = "http://lupin.procurement.ariba.com/";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
        txtVendorCode.Text = "";
        ddlVendorAccGroup.SelectedValue = "";
        ddlVendorAccGroupC.SelectedValue = "";
        txtVendorName.Text = "";
    }
    protected void btnbackHRMsg_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorMaster.aspx");
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        try
        {

            //if (ddlVendorAccGroup.SelectedValue != "15" && ddlVendorAccGroup.SelectedValue != "26" && ddlVendorAccGroup.SelectedValue != "27" && ddlVendorAccGroup.SelectedValue != "18")
            //if (ddlVendorAccGroup.SelectedValue != "15" && ddlVendorAccGroup.SelectedValue != "26" && ddlVendorAccGroup.SelectedValue != "27")
            if (ddlVendorAccGroup.SelectedValue == "27")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewHRDialog();", true);//425143
            }
			//8400000388 add 241
            else if (ddlVendorAccGroup.SelectedValue != "15" && ddlVendorAccGroup.SelectedValue != "26" && ddlVendorAccGroup.SelectedValue != "241")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewDialog();", true);
            }
            else
            {
                masterHeaderId = materialAccess.SaveMaterialHeaderC("0", ddlCompany.SelectedValue, ddlVendorAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
                if (masterHeaderId > 0)
                {
                    //LH01DT06052021 Add Start
                    Session[StaticKeys.SelectedddlCompany] = ddlCompany.SelectedValue;
                    //LH01DT06052021 Add Start

                    Session[StaticKeys.SelectedModuleId] = ddlVendorAccGroup.SelectedValue;
                    Session[StaticKeys.SelectedModule] = ddlVendorAccGroup.SelectedItem.Text;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestNo] = materialAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                    Response.Redirect("VendorGeneral1.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = ObjMasterAccess.GenerateCopyRequest(GetSelectedPkID(), ddlCompany.SelectedValue, ddlVendorAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = ddlVendorAccGroup.SelectedValue;
                Session[StaticKeys.SelectedModule] = ddlVendorAccGroup.SelectedItem.Text;
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.MaterialType] = "";
                Session[StaticKeys.ActionType] = "R";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = ObjMasterAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

                Response.Redirect("VendorGeneral1.aspx");
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

    /// <summary>
    /// PFun_DT06032020
    /// old function
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChangeBulkRequest_Click(object sender, EventArgs e)
    {


        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestC("0", "VMC", lblUserId.Text, "");
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtVendorCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Vendor Bulk Change";

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("VendorChange.aspx");
        }


    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeRequestC("0", ddlCompanyCode.SelectedValue, ddlVendorAccGroupC.SelectedValue, lblUserId.Text, txtVendorCode.Text, txtVendorName.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = ddlVendorAccGroupC.SelectedValue;
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtVendorCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = ddlVendorAccGroupC.SelectedItem.Text;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("VendorGeneral1.aspx");
        }
    }

    protected void btnBlockRequest_Click(object sender, EventArgs e)
    {
        //SDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R
        //if (ddlVendorAccGroupC.SelectedValue != "15" && ddlVendorAccGroupC.SelectedValue != "26" && ddlVendorAccGroupC.SelectedValue != "27")
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewDialog();", true);
        //}
        //else
        //{ 
        //EDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R

        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateBlockRequestC("0", ddlCompanyCode.SelectedValue, ddlVendorAccGroupC.SelectedValue, "VMB", lblUserId.Text, txtVendorCode.Text, txtVendorName.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = "61";//ddlVendorAccGroupC.SelectedValue;
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "B";
            Session[StaticKeys.MaterialNo] = txtVendorCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Vendor Master Block";// ddlVendorAccGroupC.SelectedItem.Text;

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("VendorBlock.aspx");
        }
        //SDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R
        //}
        //EDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R
    }

    protected void btnUnBlockRequest_Click(object sender, EventArgs e)
    {
        //SDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R
        //if (ddlVendorAccGroupC.SelectedValue != "15" && ddlVendorAccGroupC.SelectedValue != "26" && ddlVendorAccGroupC.SelectedValue != "27")
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewDialog();", true);

        //}
        //else
        //{
        //EDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R

        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateBlockRequestC("0", ddlCompanyCode.SelectedValue, ddlVendorAccGroupC.SelectedValue, "VMU", lblUserId.Text, txtVendorCode.Text, txtVendorName.Text);
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = "62";//ddlVendorAccGroupC.SelectedValue;
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "U";
            Session[StaticKeys.MaterialNo] = txtVendorCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Vendor Master UnBlock";// ddlVendorAccGroupC.SelectedItem.Text;
                                                                         //Vendor workflow modification start
            Session[StaticKeys.MaterialProcessModuleId] = ddlVendorAccGroupC.SelectedValue;
            //Vendor workflow modification end

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("VendorBlock.aspx");
        }
        //SDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R
        //}
        //EDT 29042019, Desc: Hide ariba validation popup window, Comment By Nitin R
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";
        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("VendorBlock.aspx");
        else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == "126")
            Response.Redirect("VendorChange.aspx");
        //PFun_DT06032020 Added by NR
        else if (Session[StaticKeys.ActionType].ToString() == "C" && (Session[StaticKeys.SelectedModuleId].ToString() == "224" || Session[StaticKeys.SelectedModuleId].ToString() == "226"))
            Response.Redirect("PartnerFunction.aspx");
        //PFun_DT06032020 Added by NR
        else
            Response.Redirect("VendorGeneral1.aspx");
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";

        if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            Response.Redirect("VendorBlock.aspx");
        else if (Session[StaticKeys.ActionType].ToString() == "C" && Session[StaticKeys.SelectedModuleId].ToString() == "126")
            Response.Redirect("VendorChange.aspx");
        //PFun_DT06032020 Added by NR
        else if (Session[StaticKeys.ActionType].ToString() == "C" && (Session[StaticKeys.SelectedModuleId].ToString() == "224" || Session[StaticKeys.SelectedModuleId].ToString() == "226"))
            Response.Redirect("PartnerFunction.aspx");
        //PFun_DT06032020 Added by NR
        else
            Response.Redirect("VendorGeneral1.aspx");
        //Response.Redirect("VendorGeneral1.aspx?pgseq=1&sid=29");
    }
    /// <summary>
    /// /add 814364
    /// </summary>
    /// <returns></returns>
    private bool IsValidSearch()
    {
        bool flg = false;

        int diffOfDatesi = 0;
        if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
        {
            try
            {
                var fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var diffOfDates = (tdate - fdate).TotalDays;
                diffOfDatesi = Convert.ToInt32(diffOfDates);
            }
            catch (Exception ex)
            {
               // _log.Error("Exception" + ex.Message);
            }
        }


        if ((ddlStatus.SelectedValue == "P" || ddlStatus.SelectedValue == "R" || ddlStatus.SelectedValue == "SUB"))
        {
            flg = true;
        }
        else if ((txtRequestNo.Text.Trim() != "" || txtSAPCode.Text.Trim() != ""))
        {
            flg = true;
        }
        else if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "" && diffOfDatesi == 0)
        {
            flg = false;
        }
        else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && diffOfDatesi <= 90)
        {
            flg = true;
        }
        else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && diffOfDatesi >= 90)
        {
            flg = false;
        }
        return flg;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsValidSearch() == true)
        {
        ReadMaterialMasterRequests();

        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Please select valid date range and date range should not exceed 90 days.";
        }
    }

    DataSet dstData = new DataSet();

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        grdSearch.PageIndex = e.NewPageIndex;
        dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlVendorAccGrp.SelectedValue, "V", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
        grdSearch.DataSource = dstData.Tables[0].DefaultView;
        grdSearch.DataBind();

    }

    void BindVendorGroup()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        ddlVendorAccGrp.DataSource = objMatAccess.ReadModules("V");
        ddlVendorAccGrp.DataValueField = "Module_Id";
        ddlVendorAccGrp.DataTextField = "Module_Name";
        ddlVendorAccGrp.DataBind();
        ddlVendorAccGrp.Items.Insert(0, new ListItem("All", "0"));
    }

    //Vendor workflow modification start
    protected void ddlVendorAccGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //8400000388 S
        txtManager.Visible = false;
        lblManager.Visible = false;
        if (ddlVendorAccGroup.SelectedValue != "" && ddlVendorAccGroup.SelectedValue != "241")
        {
            //8400000388 Commented if (ddlVendorAccGroup.SelectedValue != "")
            //8400000388 E 
            //8400000388 S
            txtManager.Visible = true;
            lblManager.Visible = true;
            //8400000388 E
            MasterAccess ObjMasterAccess = new MasterAccess();
            txtManager.Text = ObjMasterAccess.GetRMByModuleIdMasterCategory(ddlVendorAccGroup.SelectedValue.ToString(), lblUserId.Text, "");
            //Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification
            //if (ddlVendorAccGroup.SelectedValue != "26" && ddlVendorAccGroup.SelectedValue != "27")
            //{
            //string message = "alert('Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.')";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
            //}
            //End
        }

    }
    //Vendor workflow modification end

    protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor lnkFrwrdNote = e.Row.FindControl("lnkFrwrdNote") as HtmlAnchor;
            Label lblForward = e.Row.FindControl("lblForward") as Label;
            if (lblForward.Text == "")
            {
                lnkFrwrdNote.Visible = false;
            }
            Label lblPrimaryID = e.Row.FindControl("lblPrimaryID") as Label;
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            Utility ObjUtil = new Utility();
            DataSet ds = new DataSet();
            string VendorCode = dstData.Tables[0].Rows[0]["Customer_Code"].ToString();
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "37")
            {
                ds = objMatAccess.FindMDMApproved();
                foreach (TableCell cell in e.Row.Cells)
                {
                    //if ((Session[StaticKeys.SelectedModuleId].ToString() == "126") && (VendorCode != null || VendorCode != ""))

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string Master_Header_Id = ds.Tables[0].Rows[i]["Master_Header_Id"].ToString();
                            if (lblPrimaryID.Text == Master_Header_Id)
                                cell.BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

            //dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlVendorAccGrp.SelectedValue, "V", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            //for (int i = 0; i <= dstData.Tables[0].Rows.Count; i++ )
            //{
            //    if ((Session[StaticKeys.SelectedModuleId].ToString() == "126") && (VendorCode != null || VendorCode != ""))
            //    {
            //        ds = objMatAccess.FindMDMApproved();
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {

            //        }
            //    }
            //}


        }

    }

    #endregion

    #region Vendor Search

    public void PopulateDropDownList(string userProfileId)
    {
        try
        {
            HelperAccess ObjHelperAccess = new HelperAccess();
            ObjHelperAccess.PopuplateDropDownList(ddlCompany, "pr_GetCompanyList", "Company_Name", "Company_Id");
            ObjHelperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyList", "Company_Name", "Company_Id");
            try
            {
                ddlCompany.SelectedValue = "32";
                ddlCompanyCode.SelectedValue = "32";
                //LH01DT06052021 Comment Start
                //ddlCompany.Enabled = false;
                //ddlCompanyCode.Enabled = false;
                //LH01DT06052021 Comment End
            }
            catch { }

            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            HelperAccess helperAccess = new HelperAccess();
            DataSet ds;
            ds = objMatAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "V");

            ddlVendorAccGroup.DataSource = ds;
            ddlVendorAccGroup.DataTextField = "Module_Name";
            ddlVendorAccGroup.DataValueField = "Module_Id";
            ddlVendorAccGroup.DataBind();

            ddlVendorAccGroupC.DataSource = ds;
            ddlVendorAccGroupC.DataTextField = "Module_Name";
            ddlVendorAccGroupC.DataValueField = "Module_Id";
            ddlVendorAccGroupC.DataBind();

            //helperAccess.PopuplateDropDownList(ddlManager, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadMaterialMasterRequests()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();
        try
        {
            dstData = objMatAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlVendorAccGrp.SelectedValue, "V", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    //grdSearch.Columns[5].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    btnModify.Visible = false;
                    //btnChangeRequest.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    //grdSearch.Columns[5].Visible = false;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = true;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    btnModify.Visible = true;
                    //btnChangeRequest.Visible = false;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    //grdSearch.Columns[5].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = true;
                    btnModify.Visible = true;
                    //btnChangeRequest.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    //grdSearch.Columns[5].Visible = false;
                    grdSearch.Columns[10].Visible = true;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    //grdSearch.Columns[5].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    grdSearch.Columns[11].Visible = false;
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = true;
                    btnModify.Visible = false;
                    //btnChangeRequest.Visible = false;
                    btnCopyRequest.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    //grdSearch.Columns[5].Visible = true;
                    grdSearch.Columns[10].Visible = false;
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        grdSearch.Columns[11].Visible = true;
                    }
                    else
                    {
                        grdSearch.Columns[11].Visible = false;
                    }
                    grdSearch.Columns[12].Visible = false;
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    btnModify.Visible = true;
                    //btnChangeRequest.Visible = false;
                    btnCopyRequest.Visible = false;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnCopyRequest.Visible = false;
                //btnChangeRequest.Visible = false;
            }

            if (ddlVendorAccGroupC.Items.Count < 2)
            {
                //PFun_DT06032020 comment
                //btnChangeBulkRequest.Enabled = false;
                //PFun_DT06032020

                btnChangeBulkRequest.Attributes.Add("enabled", "enabled");
                //btnCreateNew.Attributes.Add("disabled", "disabled");



            }

            grdSearch.DataBind();

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
                    //Vendor workflow modification start
                    Label lblPendingFor = grv.FindControl("lblPendingFor") as Label;
                    Label lblSelectedVendorAccGrp = grv.FindControl("lblSelectedVendorAccGrp") as Label;
                    //Vendor workflow modification end

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    Session[StaticKeys.MaterialNo] = lblMasterCode.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    //Vendor workflow modification start
                    Session[StaticKeys.PendingFor] = lblPendingFor.Text;
                    Session[StaticKeys.MaterialProcessModuleId] = lblSelectedVendorAccGrp.Text;
                    //Vendor workflow modification end

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

    //Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification
    protected void ddlVendorAccGroupC_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlVendorAccGroupC.SelectedValue != "26" && ddlVendorAccGroupC.SelectedValue != "27")
        //8400000388 add 241
        if (ddlVendorAccGroupC.SelectedValue != "241")
        {
        string message = "alert('Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.')";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
        }
    }
    //End

    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ddlSelectRequest_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if(ddlSelectRequest.SelectedValue == "2")
    //    {
    //        //btnChangeAdd.Enabled = true;
    //        //btnChangeRemove.Enabled = true;
    //        //btnChangeNext.Enabled = false;

    //        btnChangeAdd.Attributes.Add("style", "display: block !important;");
    //        btnChangeRemove.Attributes.Add("style", "display: block !important;");
    //        btnChangeNext.Attributes.Add("style", "display: block !important;");
    //    }
    //    else
    //    {


    //        btnChangeAdd.Attributes.Add("style", "display: block !important;");
    //        btnChangeRemove.Attributes.Add("style", "display: block !important;");
    //        btnChangeNext.Attributes.Add("style", "display: block !important;");

    //    }

    //}


    /// <summary>
    /// PFun_DT06032020
    /// old function
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChangeAdd_Click(object sender, EventArgs e)
    {


        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestC("0", "VTL", lblUserId.Text, "");
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtVendorCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Vendor to be Link";

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("PartnerFunction.aspx");
        }


    }

    /// <summary>
    /// PFun_DT06032020
    /// old function
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChangeRemove_Click(object sender, EventArgs e)
    {
        MasterAccess objMasterAccess = new MasterAccess();
        int MasterHeaderId = objMasterAccess.GenerateChangeBulkRequestC("0", "VTR", lblUserId.Text, "");
        if (MasterHeaderId > 0)
        {
            Session[StaticKeys.SelectedModuleId] = objMasterAccess.mModule_Id.ToString();
            Session[StaticKeys.MasterHeaderId] = MasterHeaderId.ToString();
            Session[StaticKeys.Mode] = "N";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.ActionType] = "C";
            Session[StaticKeys.MaterialNo] = txtVendorCode.Text;
            Session[StaticKeys.RequestNo] = objMasterAccess.mRequestNo;
            Session[StaticKeys.SelectedModule] = "Vendor to be Remove";

            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
            Response.Redirect("PartnerFunction.aspx");
        }


    }
}