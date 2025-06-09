using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Transactions;

public partial class Transaction_SoftwareApproval_SoftwareApproval : System.Web.UI.Page
{
    HelperAccess helperAccess = new HelperAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.SelectedModuleId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                    PopuplateDropDownList();
                    FillSWApprovalData();

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        //btnSave.Visible = !btnNext.Visible;
                        //if (!btnNext.Visible)
                            //    btnSave.Visible = false;
                        grdAttachedDocs.Columns[1].Visible = true;
                        file_upload.Visible = true;
                        btnUploadDoc.Visible = true;
                    }
                    else
                    {
                        grdAttachedDocs.Columns[1].Visible = false;
                        file_upload.Visible = false;
                        btnUploadDoc.Visible = false;
                    }
                    ConfigureControls();
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "32")
                        trMISC.Visible = true;
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            SWApprovalAccess ObjSWMasterAccess = new SWApprovalAccess();
            if (ObjSWMasterAccess.GenerateMassRequestProcess(lblMasterHeaderId.Text + "/", Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
            {
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
            Session[StaticKeys.AddAlertMsg] = "Software Approval Request No: " + Session[StaticKeys.RequestNo].ToString() + " created successfully.";
            Response.Redirect("SoftwareApprovalMaster.aspx");
        }
    }

    protected void lnkRefreshddlInstallLocation_Click(object sender, EventArgs e)
    {
        DisplayInstallLocation();
    }

    protected void lnkddlInstallDept_Click(object sender, EventArgs e)
    {
        DisplayInstallDepts();
    }

    protected void ddlInstalledServer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstalledServer.SelectedValue == "Yes")
        {
            txtServerQty.Enabled = true;
            reqtxtServerQty.Enabled = true;
            labletxtServerQty.Visible = true;
        }
        else
        {
            txtServerQty.Enabled = false;
            reqtxtServerQty.Enabled = false;
            labletxtServerQty.Visible = false;
            txtServerQty.Text = "";
        }
    }

    protected void ddlPCLapReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPCLapReq.SelectedValue == "Yes")
        {
            txtPCLapQty.Enabled = true;
            reqtxtPCLapQty.Enabled = true;
            labeltxtPCLapQty.Visible = true;
        }
        else
        {
            txtPCLapQty.Enabled = false;
            reqtxtPCLapQty.Enabled = false;
            labeltxtPCLapQty.Visible = false;
            txtPCLapQty.Text = "";
        }
    }

    protected void txtITCost_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalCost();
    }

    protected void txtSWCost_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalCost();
    }

    protected void ddlInstallLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayInstallLocation();
        ConfigureOtherLoc();

    }

    protected void ddlInstallDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayInstallDepts();
        ConfigureOtherDept();
    }

    protected void grdAttachedDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            DataAccessLayer objDb = new DataAccessLayer();
            SqlTransaction objTrans;
            Control ctl = e.CommandSource as Control;
            GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
            string documentId = grdAttachedDocs.DataKeys[CurrentRow.RowIndex].Value.ToString();
            Label lblUploadedFileName = grdAttachedDocs.Rows[CurrentRow.RowIndex].FindControl("lblUploadedFileName") as Label;

            try
            {
                objDb.OpenConnection(this.Page);
                objTrans = objDb.cnnConnection.BeginTransaction();

                if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                {
                    string savePath = MapPath(lblUploadedFileName.Text);
                    if (System.IO.File.Exists(savePath))
                        System.IO.File.Delete(savePath);
                    objTrans.Commit();
                    pnlMsg.Visible = false;
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                }
                else
                {
                    objTrans.Rollback();
                    lblMsg.Text = "Error While Deleting File.";
                    pnlMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDb.CloseConnection(objDb.cnnConnection);
                objDb = null;
                objTrans = null;
            }
        }
    }

    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        if (SaveDoc() == 1)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if ((SaveDocuments(lblMasterHeaderId.Text)))
                    {
                        scope.Complete();
                        pnlMsg.Visible = false;
                    }
                    else
                    {
                        lblMsg.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }
                BindAttachedDocuments(lblMasterHeaderId.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    #endregion

    #region Methods

    private bool Save()
    {
        bool flg = false;
        SWApprovalAccess objSWApprovalAccess = new SWApprovalAccess();
        try
        {
            SWApproval ObjSWApproval = GetControlsValue();
            if (objSWApprovalAccess.SaveSWApprovalData(ObjSWApproval) > 0)
            {
                flg = true;
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
        return flg;
    }

    private SWApproval GetControlsValue()
    {
        SWApproval objSWApproval = new SWApproval();
        Utility objUtil = new Utility();

        objSWApproval.SWApproval_Id = Convert.ToInt32(lblSWFormId.Text);
        objSWApproval.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        objSWApproval.UserName = txtUserName.Text;
        objSWApproval.Dept = txtDept.Text;
        objSWApproval.InstallLocation = GetSelectedCheckedValue(ddlInstallLocation) == null ? "" : GetSelectedCheckedValue(ddlInstallLocation);
        objSWApproval.OtherLoc = txtOtherLoc.Text;
        objSWApproval.InstallDept = GetSelectedCheckedValue(ddlInstallDept) == null ? "" : GetSelectedCheckedValue(ddlInstallDept);
        objSWApproval.OtherDept = txtOtherDept.Text;
        objSWApproval.SWName = txtSWName.Text;
        objSWApproval.Manufacturer = txtMnfr.Text;
        objSWApproval.MnfrWebsite = txtMnfrSite.Text;
        objSWApproval.MnfrCntctName = txtMnfrPersonName.Text;
        objSWApproval.MnfrEmail = txtEmailAdd.Text;
        objSWApproval.MnfrCntctNo = txtPhneNo.Text;
        objSWApproval.SWCost = txtSWCost.Text;
        objSWApproval.SWUse = txtSWUse.Text;
        objSWApproval.BusinessJustification = txtBJustification.Text;
        objSWApproval.InstalledServer = ddlInstalledServer.SelectedValue;
        objSWApproval.ServerQty = txtServerQty.Text;
        objSWApproval.PCLapReq = ddlPCLapReq.SelectedValue;
        objSWApproval.PCLapQty = txtPCLapQty.Text;
        objSWApproval.ExpectedUsers = txtExpectedUsers.Text;
        objSWApproval.ApproxSize = txtDataSize.Text;
        objSWApproval.NoOfPagesPD = txtPages.Text;
        objSWApproval.Requirements = txtRequirements.Text;
        objSWApproval.SecurityIssues = txtSecurityIssues.Text;
        objSWApproval.ITReqCost = txtITCost.Text;
        objSWApproval.TotalCost = txtTotalCost.Text;
        objSWApproval.ITRemarks = txtITRemarks.Text;

        objSWApproval.UserId = lblUserId.Text;
        objSWApproval.TodayDate = objUtil.GetDate();
        objSWApproval.IPAddress = objUtil.GetIpAddress();
        objSWApproval.Mode = lblMode.Text;

        return objSWApproval;
    }

    private void FillSWApprovalData()
    {
        SWApproval ObjSWApproval = GetSWApprovalData();
        try
        {
            if (ObjSWApproval.SWApproval_Id > 0)
            {
                lblSWFormId.Text = ObjSWApproval.SWApproval_Id.ToString();
                txtUserName.Text = ObjSWApproval.UserName;
                txtDept.Text = ObjSWApproval.Dept;
                SetSelectedValue(ddlInstallLocation, ObjSWApproval.InstallLocation);
                txtOtherLoc.Text = ObjSWApproval.OtherLoc;
                SetSelectedValue(ddlInstallDept, ObjSWApproval.InstallDept);
                txtOtherDept.Text = ObjSWApproval.OtherDept;
                txtSWName.Text = ObjSWApproval.SWName;
                txtMnfr.Text = ObjSWApproval.Manufacturer;
                txtMnfrSite.Text = ObjSWApproval.MnfrWebsite;
                txtMnfrPersonName.Text = ObjSWApproval.MnfrCntctName;
                txtEmailAdd.Text = ObjSWApproval.MnfrEmail;
                txtPhneNo.Text = ObjSWApproval.MnfrCntctNo;

                txtSWCost.Text = ObjSWApproval.SWCost;
                txtSWUse.Text = ObjSWApproval.SWUse;
                txtBJustification.Text = ObjSWApproval.BusinessJustification;
                ddlInstalledServer.SelectedValue = ObjSWApproval.InstalledServer;
                txtServerQty.Text = ObjSWApproval.ServerQty;
                ddlPCLapReq.SelectedValue = ObjSWApproval.PCLapReq;
                txtPCLapQty.Text = ObjSWApproval.PCLapQty;

                txtExpectedUsers.Text = ObjSWApproval.ExpectedUsers;
                txtDataSize.Text = ObjSWApproval.ApproxSize;
                txtPages.Text = ObjSWApproval.NoOfPagesPD;
                txtRequirements.Text = ObjSWApproval.Requirements;
                txtSecurityIssues.Text = ObjSWApproval.SecurityIssues;
                txtITCost.Text = ObjSWApproval.ITReqCost;
                txtTotalCost.Text = ObjSWApproval.TotalCost;
                txtITRemarks.Text = ObjSWApproval.ITRemarks;
                FillMISCData();
            }
            else
            {
                SWApprovalAccess ObjSWMasterAccess = new SWApprovalAccess();
                lblSWFormId.Text = "0";
                txtUserName.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                //txtDept.Text = Session[StaticKeys.LoggedIn_User_DeptName].ToString();
                //Created by Swati Mohandas on 25/06/18 to get Department Name on first User Login
                DataSet ds = ObjSWMasterAccess.GetDepartmentName(Session[StaticKeys.LoggedIn_User_Id].ToString());
                txtDept.Text = ds.Tables[0].Rows[0]["Department_Name"].ToString();
                //Created by Swati Mohandas on 25/06/18 to get Department Name on first User Login
            }
            DisplayInstallLocation();
            DisplayInstallDepts();
            ConfigureServerQty();
            ConfigurePCLapQty();
            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private SWApproval GetSWApprovalData()
    {
        SWApprovalAccess objSWApprovalAccess = new SWApprovalAccess();
        return objSWApprovalAccess.GetSWApprovalData(lblMasterHeaderId.Text);
    }

    private void ConfigureControls()
    {
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0")
        {
            ddlInstallLocation.Enabled = true;
            ddlInstallDept.Enabled = true;
            txtSWName.Enabled = true;
            txtMnfr.Enabled = true;
            txtMnfrSite.Enabled = true;
            txtMnfrPersonName.Enabled = true;
            txtEmailAdd.Enabled = true;
            txtPhneNo.Enabled = true;
            txtSWCost.Enabled = true;
            txtSWUse.Enabled = true;
            txtBJustification.Enabled = true;

            reqddlInstallLocation.Enabled = true;
            reqddlInstallDept.Enabled = true;
            reqtxtSWName.Enabled = true;
            reqtxtMnfr.Enabled = true;
            reqtxtMnfrSite.Enabled = false;
            reqtxtMnfrPersonName.Enabled = true;
            reqtxtEmailAdd.Enabled = true;
            regtxtEmailAdd.Enabled = true;
            reqtxtPhneNo.Enabled = true;
            reqtxtSWCost.Enabled = true;
            reqtxtSWUse.Enabled = false;
            reqtxtBJustification.Enabled = true;

            lableddlInstallLocation.Visible = true;
            labelddlInstallDept.Visible = true;
            labletxtSWName.Visible = true;
            labletxtMnfr.Visible = true;
            labletxtMnfrSite.Visible = false;
            labeltxtMnfrPersonName.Visible = true;
            labeltxtEmailAdd.Visible = true;
            labeltxtPhneNo.Visible = true;
            labletxtSWCost.Visible = true;
            labletxtSWUse.Visible = false;
            labletxtBJustification.Visible = true;

            ddlInstalledServer.Enabled = true;
            //txtServerQty.Enabled = false;
            ddlPCLapReq.Enabled = true;
            //txtPCLapQty.Enabled = false;
            txtExpectedUsers.Enabled = true;
            txtDataSize.Enabled = true;
            txtPages.Enabled = true;
            txtRequirements.Enabled = true;
            txtSecurityIssues.Enabled = true;
            txtITRemarks.Enabled = true;
            txtITCost.Enabled = true;
            //txtTotalCost.Enabled = false;

            reqddlInstalledServer.Enabled = false;
            //reqtxtServerQty.Enabled = false;
            reqddlPCLapReq.Enabled = false;
            //reqtxtPCLapQty.Enabled = false;
            reqtxtExpectedUsers.Enabled = false;
            reqtxtDataSize.Enabled = false;
            reqtxtPages.Enabled = false;
            reqtxtRequirements.Enabled = false;
            reqtxtSecurityIssues.Enabled = false;
            reqtxtITRemarks.Enabled = false;
            reqtxtITCost.Enabled = false;
            //reqtxtTotalCost.Enabled = false;

            labletxtInstalledServer.Visible = false;
            //labletxtServerQty.Visible = false;
            labelddlPCLapReq.Visible = false;
            //labeltxtPCLapQty.Visible = false;
            labletxtExpectedUsers.Visible = false;
            labletxtDataSize.Visible = false;
            labletxtPages.Visible = false;
            labletxtRequirements.Visible = false;
            labletxtSecurityIssues.Visible = false;
            labletxtITRemarks.Visible = false;
            labeltxtITCost.Visible = false;
            //labeltxtTotalCost.Visible = false;

        }
        else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "26")
        {
            ddlInstalledServer.Enabled = true;
            //txtServerQty.Enabled = true;
            ddlPCLapReq.Enabled = true;
            //txtPCLapQty.Enabled = true;
            txtExpectedUsers.Enabled = true;
            txtDataSize.Enabled = true;
            txtPages.Enabled = true;
            txtRequirements.Enabled = true;
            txtSecurityIssues.Enabled = true;
            txtITRemarks.Enabled = true;
            txtITCost.Enabled = true;
            //txtTotalCost.Enabled = true;

            reqddlInstalledServer.Enabled = true;
            //reqtxtServerQty.Enabled = true;
            reqddlPCLapReq.Enabled = true;
            //reqtxtPCLapQty.Enabled = true;
            reqtxtExpectedUsers.Enabled = true;
            reqtxtDataSize.Enabled = true;
            reqtxtPages.Enabled = true;
            reqtxtRequirements.Enabled = true;
            reqtxtSecurityIssues.Enabled = true;
            reqtxtITRemarks.Enabled = true;
            reqtxtITCost.Enabled = true;
            //reqtxtTotalCost.Enabled = true;

            labletxtInstalledServer.Visible = true;
            //labletxtServerQty.Visible = true;
            labelddlPCLapReq.Visible = true;
            //labeltxtPCLapQty.Visible = true;
            labletxtExpectedUsers.Visible = true;
            labletxtDataSize.Visible = true;
            labletxtPages.Visible = true;
            labletxtRequirements.Visible = true;
            labletxtSecurityIssues.Visible = true;
            labletxtITRemarks.Visible = true;
            labeltxtITCost.Visible = true;
            //labeltxtTotalCost.Visible = true;

            ddlInstallLocation.Enabled = false;
            ddlInstallDept.Enabled = false;
            txtSWName.Enabled = false;
            txtMnfr.Enabled = false;
            txtSWCost.Enabled = false;
            txtSWUse.Enabled = false;
            txtBJustification.Enabled = false;

            reqddlInstallLocation.Enabled = false;
            reqddlInstallDept.Enabled = false;
            reqtxtSWName.Enabled = false;
            reqtxtMnfr.Enabled = false;
            reqtxtMnfrSite.Enabled = false;
            reqtxtMnfrPersonName.Enabled = false;
            reqtxtEmailAdd.Enabled = false;
            regtxtEmailAdd.Enabled = false;
            reqtxtPhneNo.Enabled = false;
            reqtxtSWCost.Enabled = false;
            reqtxtSWUse.Enabled = false;
            reqtxtBJustification.Enabled = false;

            lableddlInstallLocation.Visible = false;
            labelddlInstallDept.Visible = false;
            labletxtSWName.Visible = false;
            labletxtMnfr.Visible = false;
            labletxtMnfrSite.Visible = false;
            labeltxtMnfrPersonName.Visible = false;
            labeltxtEmailAdd.Visible = false;
            labeltxtPhneNo.Visible = false;
            labletxtSWCost.Visible = false;
            labletxtSWUse.Visible = false;
            labletxtBJustification.Visible = false;
            labletxtUserName.Visible = false;
            labletxtDept.Visible = false;

            trButton.Visible = true;
            btnNext.Visible = false;
            //MaterialMasterAccess objAccess = new MaterialMasterAccess();
            //if(objAccess.IsInitiatorApprover(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text))
                btnSave.Visible = true;
        }
        else
        {
            ddlInstalledServer.Enabled = false;
            //txtServerQty.Enabled = false;
            ddlPCLapReq.Enabled = false;
            //txtPCLapQty.Enabled = false;
            txtExpectedUsers.Enabled = false;
            txtDataSize.Enabled = false;
            txtPages.Enabled = false;
            txtRequirements.Enabled = false;
            txtSecurityIssues.Enabled = false;
            txtITRemarks.Enabled = false;
            txtITCost.Enabled = false;
            //txtTotalCost.Enabled = false;

            reqddlInstalledServer.Enabled = false;
            //reqtxtServerQty.Enabled = false;
            reqddlPCLapReq.Enabled = false;
            //reqtxtPCLapQty.Enabled = false;
            reqtxtExpectedUsers.Enabled = false;
            reqtxtDataSize.Enabled = false;
            reqtxtPages.Enabled = false;
            reqtxtRequirements.Enabled = false;
            reqtxtSecurityIssues.Enabled = false;
            reqtxtITRemarks.Enabled = false;
            reqtxtITCost.Enabled = false;
            //reqtxtTotalCost.Enabled = false;

            labletxtInstalledServer.Visible = false;
            //labletxtServerQty.Visible = false;
            labelddlPCLapReq.Visible = false;
            //labeltxtPCLapQty.Visible = false;
            labletxtExpectedUsers.Visible = false;
            labletxtDataSize.Visible = false;
            labletxtPages.Visible = false;
            labletxtRequirements.Visible = false;
            labletxtSecurityIssues.Visible = false;
            labletxtITRemarks.Visible = false;
            labeltxtITCost.Visible = false;
            //labeltxtTotalCost.Visible = false;

            ddlInstallLocation.Enabled = false;
            ddlInstallDept.Enabled = false;
            txtSWName.Enabled = false;
            txtMnfr.Enabled = false;
            txtMnfrSite.Enabled = false;
            txtMnfrPersonName.Enabled = false;
            txtEmailAdd.Enabled = false;
            txtPhneNo.Enabled = false;
            txtSWCost.Enabled = false;
            txtSWUse.Enabled = false;
            txtBJustification.Enabled = false;

            reqddlInstallLocation.Enabled = false;
            reqddlInstallDept.Enabled = false;
            reqtxtSWName.Enabled = false;
            reqtxtMnfr.Enabled = false;
            reqtxtMnfrSite.Enabled = false;
            reqtxtMnfrPersonName.Enabled = false;
            reqtxtEmailAdd.Enabled = false;
            regtxtEmailAdd.Enabled = false;
            reqtxtPhneNo.Enabled = false;
            reqtxtSWCost.Enabled = false;
            reqtxtSWUse.Enabled = false;
            reqtxtBJustification.Enabled = false;

            lableddlInstallLocation.Visible = false;
            labelddlInstallDept.Visible = false;
            labletxtSWName.Visible = false;
            labletxtMnfr.Visible = false;
            labletxtMnfrSite.Visible = false;
            labeltxtMnfrPersonName.Visible = false;
            labeltxtEmailAdd.Visible = false;
            labeltxtPhneNo.Visible = false;
            labletxtSWCost.Visible = false;
            labletxtSWUse.Visible = false;
            labletxtBJustification.Visible = false;
            labletxtUserName.Visible = false;
            labletxtDept.Visible = false;
        }
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownCheckBox(ddlInstallLocation, "pr_GetDropDownListByControlNameModuleType 'S','ddlInstallLocation'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownCheckBox(ddlInstallDept, "pr_GetDropDownListByControlNameModuleType 'S','ddlInstallDept'", "LookUp_Desc", "LookUp_Code");
    }

    private void DisplayInstallLocation()
    {
        string InstallLoc = GetSelectedCheckedValue(ddlInstallLocation);
        if (InstallLoc != null)
        {
            lableRddlInstallLocation.Text = "Installation Locations :  " + InstallLoc.Substring(0, InstallLoc.Length - 1);
        }
        else
        {
            lableRddlInstallLocation.Text = "";
        }
    }

    private void DisplayInstallDepts()
    {
        string InstallDept = GetSelectedCheckedValue(ddlInstallDept);
        if (InstallDept != null)
        {
            labelRddlInstallDept.Text = "Installation Depts :  " + InstallDept.Substring(0, InstallDept.Length - 1);
        }
        else
        {
            labelRddlInstallDept.Text = "";
        }
    }

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

    protected void SetSelectedValue(CheckBoxList chkList, string str)
    {
        try
        {
            if (!string.IsNullOrEmpty(str))
            {
                string[] strValue = str.Split(',');
                foreach (ListItem item in chkList.Items)
                {
                    for (var i = 0; i < strValue.Length; i++)
                    {
                        if (item.Value == strValue[i])
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }
        catch
        {

            throw;
        }
    }

    private void CalculateTotalCost()
    {
        if (txtITCost.Text != "" || txtSWCost.Text != "")
        {
            txtTotalCost.Text = (Convert.ToDecimal(string.IsNullOrEmpty(txtITCost.Text) ? "0" : txtITCost.Text) + Convert.ToDecimal(string.IsNullOrEmpty(txtSWCost.Text) ? "0" : txtSWCost.Text)).ToString();
        }
    }

    private void ConfigureServerQty()
    {
        if (ddlInstalledServer.SelectedValue == "Yes")
        {
            txtServerQty.Enabled = true;
            reqtxtServerQty.Enabled = true;
            labletxtServerQty.Visible = true;
        }
        else
        {
            txtServerQty.Enabled = false;
            reqtxtServerQty.Enabled = false;
            labletxtServerQty.Visible = false;
            txtServerQty.Text = "";
        }
    }

    private void ConfigurePCLapQty()
    {
        if (ddlPCLapReq.SelectedValue == "Yes")
        {
            txtPCLapQty.Enabled = true;
            reqtxtPCLapQty.Enabled = true;
            labeltxtPCLapQty.Visible = true;
        }
        else
        {
            txtPCLapQty.Enabled = false;
            reqtxtPCLapQty.Enabled = false;
            labeltxtPCLapQty.Visible = false;
            txtPCLapQty.Text = "";
        }
    }

    private void ConfigureOtherLoc()
    {
        bool flg = false;
        foreach (ListItem item in ddlInstallLocation.Items)
        {
            if (item.Selected)
            {
                if (item.Value.ToString() == "Others")
                {
                    flg = true;
                    break;
                }
            }
        }
        txtOtherLoc.Enabled = flg;
        reqtxtOtherLoc.Enabled = flg;
        labeltxtOtherLoc.Visible = flg;
        if (flg == false)
            txtOtherLoc.Text = "";
    }

    private void ConfigureOtherDept()
    {
        bool flg = false;
        foreach (ListItem item in ddlInstallDept.Items)
        {
            if (item.Selected)
            {
                if (item.Value.ToString() == "Others")
                {
                    flg = true;
                    break;
                }
            }
        }
        txtOtherDept.Enabled = flg;
        reqtxtOtherDept.Enabled = flg;
        labeltxtOtherDept.Visible = flg;
        if (flg == false)
            txtOtherDept.Text = "";
    }

    private int SaveDoc()
    {
        int flg = 1;

        HttpFileCollection fileCollection = Request.Files;
        string fileExtension = string.Empty;
        for (int i = 0; i < fileCollection.Count; i++)
        {
            HttpPostedFile uploadfile = fileCollection[i];
            if (uploadfile.ContentLength > 0)
            {
                fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();
                if ((fileExtension == ".pdf") || (fileExtension == ".tif") || (fileExtension == ".xls") || (fileExtension == ".xlsx") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png"))
                {
                    flg = 1;
                }
                else
                {
                    flg = 2;
                    break;
                }
            }
        }

        return flg;
    }

    private void FillMISCData()
    {
        MISCData objMISCData = GetMISCData();
        try
        {
            if (objMISCData.SWApp_Org_Data_Id > 0)
            {
                txtMISCPanel1.Text = objMISCData.Panel1MISC;
                txtMISCPanel2.Text = objMISCData.Panel2MISC;
                txtMISCPanel3.Text = objMISCData.Panel3MISC;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private MISCData GetMISCData()
    {
        SWApprovalAccess objSWApprovalAccess = new SWApprovalAccess();
        return objSWApprovalAccess.GetMISCData(lblMasterHeaderId.Text);
    }

    #endregion

    #region Document Upload

    private void BindAttachedDocuments(string vendorId)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                grdAttachedDocs.DataSource = dstData.Tables[0].DefaultView;
                grdAttachedDocs.DataBind();
                grdAttachedDocs.Visible = true;
            }
            else
            {
                grdAttachedDocs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDb = null;
        }
    }

    private bool SaveDocuments(string vendorId)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/SoftwareApproval/SWDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
        savePath = MapPath(StrPath);

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        try
        {
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];

                if (uploadfile.ContentLength > 0)
                {
                    UploadDocument(uploadfile, StrPath, savePath);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath)
    {
        DocumentUpload ObjDoc = new DocumentUpload();
        DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
        bool flag = false;
        Utility objUtil = new Utility();

        Random sufix1 = new Random();
        string sufix = sufix1.NextDouble().ToString().Replace(".", "");


        if (uploadfile.ContentLength > 0)
        {
            string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

            string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
            savePath = savePath + "\\" + uploadedFileName;

            ObjDoc.Document_Upload_Id = 0;
            ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
            ObjDoc.Document_Type = "";
            ObjDoc.Document_Name = Path.GetFileName(uploadfile.FileName);
            ObjDoc.Document_Path = StrPath + uploadedFileName;
            ObjDoc.Remarks = "";
            ObjDoc.IsActive = 1;
            ObjDoc.UserId = lblUserId.Text;
            ObjDoc.IPAddress = objUtil.GetIpAddress();

            uploadfile.SaveAs(savePath);

            ObjDocUploadAccess.Save(ObjDoc);

            flag = true;
        }
        else
        {
            flag = false;
            lblMsg.Text = "Error While Saving documents.";
        }

        return flag;
    }

    #endregion

}