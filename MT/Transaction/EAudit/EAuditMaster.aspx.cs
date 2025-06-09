using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections;

public partial class Transaction_EAudit_EAuditMaster : System.Web.UI.Page
{
    DataSet dstData = new DataSet();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                ddlStatus.Items.Clear();
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                {
                    ddlStatus.Items.Add(new ListItem("Pending For My Approval", "P"));
                    ddlStatus.Items.Add(new ListItem("Rollbacked By Me", "REJ"));
                    ddlStatus.Items.Add(new ListItem("Rejected By Me", "ZE"));
                    ddlStatus.Items.Add(new ListItem("Approved", "ALL"));
                }
                else
                {
                    ddlStatus.Items.Add(new ListItem("Created By Me", "C"));
                    ddlStatus.Items.Add(new ListItem("Incomplete Request", "I"));
                    ddlStatus.Items.Add(new ListItem("Rollbacked To Me", "R"));
                    ddlStatus.Items.Add(new ListItem("Rejected To Me", "Z"));
                    ddlStatus.Items.Add(new ListItem("Approved", "ALL"));
                }
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();
                PopulateDropDownList();
                ReadModules();
                ReadProfileWiseModules(userProfileId, lblUserId.Text);
                ReadMaterialMasterRequests();
            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMode.Text = "V";
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "V";
        Session[StaticKeys.MaterialType] = "";
        Response.Redirect("EAudit.aspx");
    }

    //protected void btnCreate_Click(object sender, EventArgs e)
    //{
    //    EAuditAccess auditAccess = new EAuditAccess();
    //    int masterHeaderId;

    //    try
    //    {
    //        string mode = lblMode.Text;
    //        masterHeaderId = auditAccess.SaveAuditHeader("0", "176", lblUserId.Text, mode);
    //        if (masterHeaderId > 0)
    //        {
    //            Session[StaticKeys.SelectedModuleId] = 176;
    //            Session[StaticKeys.SelectedModule] = "EAUD - Audit Request Form";
    //            Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
    //            Session[StaticKeys.Mode] = "N";
    //            Session[StaticKeys.ActionType] = "N";
    //            Session[StaticKeys.MaterialNo] = "New Request";
    //            Session[StaticKeys.RequestNo] = auditAccess.mRequestNo;

    //            Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
    //            Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
    //            Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();

    //            Response.Redirect("EAudit.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }


    //    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
    //    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
    //    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
    //    Response.Redirect("EAudit.aspx");
    //}

    protected void btnNext_Click(object sender, EventArgs e)
    {
        EAuditAccess auditAccess = new EAuditAccess();
        int masterHeaderId;

        try
        {
            string mode = lblMode.Text;
            if (trAppDept.Visible == true)
                mode = ddlRNDRA.SelectedValue;
            
            masterHeaderId = auditAccess.SaveAuditHeader("0", "176", lblUserId.Text, mode, ddlDeptHead.SelectedValue);
            if (masterHeaderId > 0)
            {
                Session[StaticKeys.SelectedModuleId] = 176;
                Session[StaticKeys.SelectedModule] = "EAUD - Audit Request Form";
                Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                Session[StaticKeys.Mode] = "N";
                Session[StaticKeys.ActionType] = "N";
                Session[StaticKeys.MaterialNo] = "New Request";
                Session[StaticKeys.RequestNo] = auditAccess.mRequestNo;

                Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                Session[StaticKeys.Requestor_DeptName] = Session[StaticKeys.LoggedIn_User_DeptName].ToString();

                Response.Redirect("EAudit.aspx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue != "ALL")
            Session[StaticKeys.SearchStatus] = ddlStatus.SelectedValue;
        else
            Session[StaticKeys.SearchStatus] = null;

        ReadMaterialMasterRequests();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0")
        //{
        ////    if (ddlStatus.SelectedValue == "R")
        ////    {
        ////        HtmlGenericControl pagebody = (HtmlGenericControl)Page.FindControl("pagebody");
        ////        pagebody.Attributes.Add("onload", "ShowModifyDialog()");
        ////    }
        ////}

        lblMode.Text = "M";
        lblPk.Text = GetSelectedPkID();
        Session[StaticKeys.MasterHeaderId] = lblPk.Text;
        Session[StaticKeys.Mode] = "M";
        Session[StaticKeys.MaterialType] = "";
        Response.Redirect("EAudit.aspx");
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

            MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
            if (ObjMaterialMasterAccess.DeleteMassRequest(Req_Id, lblUserId.Text) > 0)
            {
                ReadMaterialMasterRequests();
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
            EAuditAccess ObjAuditMasterAccess = new EAuditAccess();
            if (ObjAuditMasterAccess.GenerateMassRequestProcess(Req_Id, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
            {
                ReadMaterialMasterRequests();
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

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        EAuditAccess ObjAuditAccess = new EAuditAccess();
        int masterHeaderId;
        try
        {
            string ActionType = "";
            string ModuleId = "";
            string ModuleName = "";

            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    ActionType = lblActionType.Text;
                    
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    ModuleId = lblModuleId.Text;

                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    ModuleName = lblModuleName.Text;                     
                    break;
                }
            }

            if (ActionType == "N" || ActionType == "R")
            {
                //string mode = chkCopyEmergency.Checked ? "E" : lblMode.Text;
                string mode = lblMode.Text;
                if (trAppDept.Visible == true)
                    mode = ddlRNDRA.SelectedValue;
                
                masterHeaderId = ObjAuditAccess.GenerateCopyRequestAudit(GetSelectedPkID(),ModuleId, lblUserId.Text, mode,ddlDeptHeadCopy.SelectedValue);
                //masterHeaderId = ObjMasterAccess.GenerateCopyRequest(GetSelectedPkID(), ddlCompany.SelectedValue, ddlVendorAccGroup.SelectedValue, lblUserId.Text, lblMode.Text);
                if (masterHeaderId > 0)
                {
                    Session[StaticKeys.SelectedModuleId] = ModuleId;
                    Session[StaticKeys.SelectedModule] = ModuleName;
                    Session[StaticKeys.MasterHeaderId] = masterHeaderId.ToString();
                    Session[StaticKeys.Mode] = "N";
                    Session[StaticKeys.ActionType] = "N";
                    Session[StaticKeys.MaterialNo] = "New Request";
                    Session[StaticKeys.RequestNo] = ObjAuditAccess.mRequestNo;

                    Session[StaticKeys.Requestor_User_Name] = Session[StaticKeys.LoggedIn_User_FullName].ToString();
                    Session[StaticKeys.Requestor_Location] = Session[StaticKeys.LoggedIn_User_Location].ToString();
                    Session[StaticKeys.Requestor_ContactNo] = Session[StaticKeys.LoggedIn_User_ContactNo].ToString();
                    Session[StaticKeys.Requestor_DeptName] = Session[StaticKeys.LoggedIn_User_DeptName].ToString();
                    Response.Redirect("EAudit.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Copy Option only available for Create Request.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearch.PageIndex = e.NewPageIndex;
        ReadMaterialMasterRequests();
    }

    #endregion

    #region Methods

    private void ReadMaterialMasterRequests()
    {
        
        EAuditAccess objAuditAccess = new EAuditAccess();
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;

        try
        {
            dstData = objAuditAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "E", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            btnMassSubmit.Visible = false;
            btnDelete.Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                btnModify.Visible = true;

                if (ddlStatus.SelectedValue == "C")
                {
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = true;
                    grdSearch.Columns[16].Visible = false;
                    grdSearch.Columns[17].Visible = false;
                    grdSearch.Columns[18].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "I")
                {
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = true;
                    grdSearch.Columns[16].Visible = false;
                    grdSearch.Columns[17].Visible = false;
                    grdSearch.Columns[18].Visible = true;
                    btnModify.Visible = true;                    
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                    //btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "R")
                {
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    grdSearch.Columns[16].Visible = true;
                    grdSearch.Columns[17].Visible = true;
                    grdSearch.Columns[18].Visible = true;
                    btnModify.Visible = true;
                    //btnModRollBack.Visible = true;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                    //btnDelete.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "REJ")
                {
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    grdSearch.Columns[16].Visible = true;
                    grdSearch.Columns[17].Visible = true;
                    grdSearch.Columns[18].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "ALL")
                {
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = true;
                    grdSearch.Columns[15].Visible = false;
                    grdSearch.Columns[16].Visible = true;
                    grdSearch.Columns[17].Visible = true;
                    grdSearch.Columns[18].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "P")
                {
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    grdSearch.Columns[16].Visible = false;
                    grdSearch.Columns[17].Visible = false;
                    grdSearch.Columns[18].Visible = false;
                    btnModify.Visible = true;
                    btnModify.Text = "Review";
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                else if (ddlStatus.SelectedValue == "SUB")
                {
                    grdSearch.Columns[13].Visible = false;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = true;
                    grdSearch.Columns[16].Visible = false;
                    grdSearch.Columns[17].Visible = false;
                    grdSearch.Columns[18].Visible = true;
                    btnCopyRequest.Visible = true;
                    grdSearch.AllowPaging = false;
                    //btnDelete.Visible = true;
                    btnMassSubmit.Visible = true;
                }
                else if (ddlStatus.SelectedValue == "Z")
                {
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    grdSearch.Columns[16].Visible = true;
                    grdSearch.Columns[17].Visible = true;
                    grdSearch.Columns[18].Visible = true;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                    //btnDelete.Visible = false;
                }
                else if (ddlStatus.SelectedValue == "ZE")
                {
                    grdSearch.Columns[13].Visible = true;
                    grdSearch.Columns[14].Visible = false;
                    grdSearch.Columns[15].Visible = false;
                    grdSearch.Columns[16].Visible = true;
                    grdSearch.Columns[17].Visible = true;
                    grdSearch.Columns[18].Visible = false;
                    btnModify.Visible = false;
                    btnCopyRequest.Visible = false;
                    grdSearch.AllowPaging = true;
                }
                
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnCopyRequest.Visible = false;
            }
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                btnCreateNew.Enabled = false;
            
            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadModules()
    {
        EAuditAccess objAuditAccess = new EAuditAccess();
        try
        {
            //ddlModuleSearch.DataSource = objMatAccess.ReadModules("M");
            ddlModuleSearch.DataSource = objAuditAccess.ReadModulesByModuleType("E");

            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pDepartmentid"></param>
    private void GetUserByDepartment(int pDepartmentid)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        Hashtable hashPara = new Hashtable();
        string procName = "sp_GetUserByDepaEAudit";
        hashPara.Add("@pDepartmentid", pDepartmentid);
        try
        {
            //DNRCOMM//objDal.OpenConnection();
            DataSet ds = new DataSet();
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDeptHead.DataSource = ds;
                ddlDeptHead.DataTextField = "LookUp_Desc";
                ddlDeptHead.DataValueField = "LookUp_Code";
                ddlDeptHead.DataBind();

                ddlDeptHeadCopy.DataSource = ds;
                ddlDeptHeadCopy.DataTextField = "LookUp_Desc";
                ddlDeptHeadCopy.DataValueField = "LookUp_Code";
                ddlDeptHeadCopy.DataBind();

                ddlDeptHeadModify.DataSource = ds;
                ddlDeptHeadModify.DataTextField = "LookUp_Desc";
                ddlDeptHeadModify.DataValueField = "LookUp_Code";
                ddlDeptHeadModify.DataBind();
            }
            else
            {
                ddlDeptHead.DataSource = null;
                ddlDeptHead.DataTextField = "LookUp_Desc";
                ddlDeptHead.DataValueField = "LookUp_Code";
                ddlDeptHead.DataBind();

                ddlDeptHeadCopy.DataSource = null;
                ddlDeptHeadCopy.DataTextField = "LookUp_Desc";
                ddlDeptHeadCopy.DataValueField = "LookUp_Code";
                ddlDeptHeadCopy.DataBind();

                ddlDeptHeadModify.DataSource = null;
                ddlDeptHeadModify.DataTextField = "LookUp_Desc";
                ddlDeptHeadModify.DataValueField = "LookUp_Code";
                ddlDeptHeadModify.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }


    private void PopulateDropDownList()
    {
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        {
            //ListItem lst = new ListItem("Pending For Review", "REV");
            ddlStatus.Items.Insert(1, new ListItem("Pending For Review", "REV"));
            ddlStatus.Items.Insert(2, new ListItem("Pending For Final", "FIN"));

        }
        GetUserByDepartment(19);

        //ddlDeptHead.Items.Insert(1, new ListItem("Mukund Pattapu", "1"));
        //ddlDeptHead.Items.Insert(2, new ListItem("Vikas Kulkarni", "2"));
        //ddlDeptHead.Items.Insert(3, new ListItem("Pradeep Singh", "3"));
        //ddlDeptHead.Items.Insert(4, new ListItem("Sandesh Bambolkar", "4"));
        //ddlDeptHead.Items.Insert(5, new ListItem("Abhijeet Shinde", "5"));
        //ddlDeptHead.Items.Insert(6, new ListItem("Abhijit Dhupad", "6"));
        //ddlDeptHead.Items.Insert(7, new ListItem("Arjit Bose", "7"));
        ////Added new Dept Head on 20.11.2018
        //ddlDeptHead.Items.Insert(8, new ListItem("Devendra Mishra", "8"));
        ////End
        ////Added new Dept Head on 29.01.2019
        //ddlDeptHead.Items.Insert(8, new ListItem("Bhoopendra Gharat", "9"));
        //ddlDeptHead.Items.Insert(10, new ListItem("Haresh Pandya", "10"));
        ////End
        ////Added new Dept Head on 21.06.2021
        ////ddlDeptHead.Items.Insert(11, new ListItem("Prashant Shukla", "11"));
        //ddlDeptHead.Items.Insert(11, new ListItem("Divyesh Patel", "11"));
        ////Added new Dept Head on 21.06.2021
        //ddlDeptHead.Items.Insert(12, new ListItem("Nitin Shrirao", "12"));

        //ddlDeptHead.Items.Insert(13, new ListItem("Shrikant Chaudhari", "13"));

        //ddlDeptHeadCopy.Items.Insert(1, new ListItem("Mukund Pattapu", "1"));
        //ddlDeptHeadCopy.Items.Insert(2, new ListItem("Vikas Kulkarni", "2"));
        //ddlDeptHeadCopy.Items.Insert(3, new ListItem("Pradeep Singh", "3"));
        //ddlDeptHeadCopy.Items.Insert(4, new ListItem("Sandesh Bambolkar", "4"));
        //ddlDeptHeadCopy.Items.Insert(5, new ListItem("Abhijeet Shinde", "5"));
        //ddlDeptHeadCopy.Items.Insert(6, new ListItem("Abhijit Dhupad", "6"));
        //ddlDeptHeadCopy.Items.Insert(7, new ListItem("Arjit Bose", "7"));
        ////Added new Dept Head on 20.11.2018
        //ddlDeptHeadCopy.Items.Insert(8, new ListItem("Devendra Mishra", "8"));
        ////End
        ////Added new Dept Head on 29.01.2019
        //ddlDeptHeadCopy.Items.Insert(8, new ListItem("Bhoopendra Gharat", "9"));
        //ddlDeptHeadCopy.Items.Insert(10, new ListItem("Haresh Pandya", "10"));
        ////End
        ////Added new Dept Head on 21.06.2021
        ////ddlDeptHeadCopy.Items.Insert(11, new ListItem("Prashant Shukla", "11"));
        //ddlDeptHeadCopy.Items.Insert(11, new ListItem("Divyesh Patel", "11"));
        ////Added new Dept Head on 21.06.2021
        //ddlDeptHeadCopy.Items.Insert(12, new ListItem("Nitin Shrirao", "12"));
        //ddlDeptHeadCopy.Items.Insert(13, new ListItem("Shrikant Chaudhari", "13"));

        //ddlDeptHeadModify.Items.Insert(1, new ListItem("Mukund Pattapu", "1"));
        //ddlDeptHeadModify.Items.Insert(2, new ListItem("Vikas Kulkarni", "2"));
        //ddlDeptHeadModify.Items.Insert(3, new ListItem("Pradeep Singh", "3"));
        //ddlDeptHeadModify.Items.Insert(4, new ListItem("Sandesh Bambolkar", "4"));
        //ddlDeptHeadModify.Items.Insert(5, new ListItem("Abhijeet Shinde", "5"));
        //ddlDeptHeadModify.Items.Insert(6, new ListItem("Abhijit Dhupad", "6"));
        //ddlDeptHeadModify.Items.Insert(7, new ListItem("Arjit Bose", "7"));
        ////Added new Dept Head on 20.11.2018
        //ddlDeptHeadModify.Items.Insert(8, new ListItem("Devendra Mishra", "8"));
        ////End
        ////Added new Dept Head on 29.01.2019
        //ddlDeptHeadModify.Items.Insert(8, new ListItem("Bhoopendra Gharat", "9"));
        ////End
        //ddlDeptHeadModify.Items.Insert(10, new ListItem("Haresh Pandya", "10"));
        //ddlDeptHeadModify.Items.Insert(11, new ListItem("Divyesh Patel", "11"));
        ////Added new Dept Head on 21.06.2021
        ////ddlDeptHeadModify.Items.Insert(11, new ListItem("Prashant Shukla", "11"));
        ////Added new Dept Head on 21.06.2021
        //ddlDeptHeadModify.Items.Insert(12, new ListItem("Nitin Shrirao", "12"));
        //ddlDeptHeadModify.Items.Insert(13, new ListItem("Shrikant Chaudhari", "13"));
    }

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        EAuditAccess objAuditAccess = new EAuditAccess();
        Utility ObjUtil = new Utility();

        try
        {
            bool flg = true;

            if (Convert.ToInt32(profileId) == 2)
            {
                ListItem lst = new ListItem("Ready for Submit", "SUB");
                ddlStatus.Items.Add(lst);
                ddlStatus.SelectedValue = "SUB";


                dstData = objAuditAccess.SearchMasterRequests(ddlStatus.SelectedValue, txtRequestNo.Text.Trim(), lblUserId.Text, ddlModuleSearch.SelectedValue, "E", txtSAPCode.Text.Trim(), ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
                
                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                //ClientScriptManager cs = Page.ClientScript;
                //if (dstData.Tables[0].Rows.Count > 0)
                //{
                //    flg = false;

                //    // Check to see if the startup script is already registered.
                //    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                //    {
                //        String cstext = "alert('";
                //        if (Session[StaticKeys.AddAlertMsg] != null)
                //        {
                //            if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                //            {
                //                cstext += Session[StaticKeys.AddAlertMsg].ToString() + "\\n\\n";
                //                Session[StaticKeys.AddAlertMsg] = null;
                //            }
                //        }
                //        cstext += "Please tick(towards right end) in front of the finalized request(s).\\nClick on Mass Submit to send the request(s) for processing.');";
                //        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                //    }
                //}
                //else
                //{
                //    ddlStatus.SelectedValue = "P";
                //    if (Session[StaticKeys.AddAlertMsg] != null)
                //    {
                //        if (Session[StaticKeys.AddAlertMsg].ToString() != "")
                //        {
                //            // Check to see if the startup script is already registered.
                //            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                //            {
                //                String cstext = "alert('" + Session[StaticKeys.AddAlertMsg].ToString() + "');";
                //                Session[StaticKeys.AddAlertMsg] = null;
                //                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                //            }
                //        }
                //    }
                //}
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

                    //Label lblMassRequestProcessId = grv.FindControl("lblMassRequestProcessId") as Label;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequestNo = grv.FindControl("lblRequestNo") as Label;
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblCreatorDept = grv.FindControl("lblCreatorDept") as Label;
                    //Label lblPlantId = grv.FindControl("lblPlantId") as Label;
                    //Label lblStorageLocation = grv.FindControl("lblStorageLocation") as Label;
                    //Label lblPurchasingGroup = grv.FindControl("lblPurchasingGroup") as Label;
                    //Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                    //Label lblMaterialShortDescription = grv.FindControl("lblMaterialShortDescription") as Label;
                    //Label lblPlantName = grv.FindControl("lblPlantName") as Label;
                    //Label lblStorageLocationName = grv.FindControl("lblStorageLocationName") as Label;
                    //Label lblMaterialProcessModuleId = grv.FindControl("lblMaterialProcessModuleId") as Label;
                    //Label lblSalesOrgID = grv.FindControl("lblSalesOrgID") as Label;
                    //Label lblDistChnl = grv.FindControl("lblDistChnl") as Label;


                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text;
                    //Session[StaticKeys.MassRequestProcessId] = lblMassRequestProcessId.Text;
                    //Session[StaticKeys.MaterialNo] = (lblMasterCode.Text == "" ? "New Request" : lblMasterCode.Text) + " - " + lblMaterialShortDescription.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    //Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    //Session[StaticKeys.MatStorageLocationId] = lblStorageLocation.Text;
                    //Session[StaticKeys.MatPurchasingGroupId] = lblPurchasingGroup.Text;
                    //Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;

                    //Session[StaticKeys.MaterialPlantName] = lblPlantName.Text;
                    //Session[StaticKeys.MatStorageLocationName] = lblStorageLocationName.Text;

                    //Session[StaticKeys.MaterialProcessModuleId] = lblMaterialProcessModuleId.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;
                    Session[StaticKeys.Requestor_DeptName] = lblCreatorDept.Text;

                    //Session[StaticKeys.MaterialSalesOrgId] = lblSalesOrgID.Text;
                    //Session[StaticKeys.MaterialDistChnlId] = lblDistChnl.Text;

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