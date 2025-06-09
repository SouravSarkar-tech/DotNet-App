using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using log4net;

public partial class Transaction_CostCenter_CostCenterBlock : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Page Events
	 /// Carve_LC17&LC23_8400000406
    HelperAccess helperAccess = new HelperAccess();
    CostCenterBlockAccess ObjCostCenterBlockAccess = new CostCenterBlockAccess();
    string docType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                    if (Session[StaticKeys.MasterHeaderId] != null)
                    {
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        PopuplateDropDownList();
                        ConfigureControl();
                        FillCostCenterBlock();
                        BindAttachedDocuments(lblMasterHeaderId.Text);
						 /// Carve_LC17&LC23_8400000406
                        CCPC();
						 /// Carve_LC17&LC23_8400000406
                        string mode = Session[StaticKeys.Mode].ToString();
                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            trButton.Visible = true;
                            grdAttachedDocs.Columns[1].Visible = true;
                            file_upload.Visible = true;
                        }
                        else
                        {
                            grdAttachedDocs.Columns[1].Visible = false;

                        file_upload.Visible = false;
                    }
                    

                }
                else
                {
                    Response.Redirect("CostCenterMaster.aspx");
                }
            }
        }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void grdAttachedDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
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
                    System.IO.File.Delete(Server.MapPath("CostCenterDocuments") + "/" + lblUploadedFileName.Text);
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
        catch (Exception ex)
        { _log.Error("grdAttachedDocs_RowCommand", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
        if (CheckIsValid())
        {
            if (Save() == 1)
            {
                if (SaveCostCenterBlock())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    Response.Redirect("CostCenterBlock.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one field.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }

        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    private void FillCostCenterBlock()
    {
        try
        {
        CostCenterBlock ObjCostCenterBlock = GetCostCenterBlock();
        if (ObjCostCenterBlock.CostCenter_Block_Id > 0)
        {
            lblCostCenterBlockId.Text = ObjCostCenterBlock.CostCenter_Block_Id.ToString();
            txtCostCenter.Text = ObjCostCenterBlock.Cost_Center.ToString();
            txtCCName.Text = ObjCostCenterBlock.Cost_Center_Name.ToString();
            chkActPrimaryCost.Checked = ObjCostCenterBlock.LIActualPrimaryCost.ToString() == "1" ? true : false;
            chkPlnPrimaryCost.Checked = ObjCostCenterBlock.LIPlanPrimaryCost.ToString() == "1" ? true : false;
            chkActSecCost.Checked = ObjCostCenterBlock.LIActualSecondaryCost.ToString() == "1" ? true : false;
            chkPlnSecCost.Checked = ObjCostCenterBlock.LIPlanSecondaryCost.ToString() == "1" ? true : false;
            chkActRevPostings.Checked = ObjCostCenterBlock.LIActualRevenuePosting.ToString() == "1" ? true : false;
            chkPlnRevPostings.Checked = ObjCostCenterBlock.LIPlanRevenuePosting.ToString() == "1" ? true : false;
            txtRemarks.Text = ObjCostCenterBlock.Remarks.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjCostCenterBlock.ModulePlantGroupCode;

            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        else
        {
            lblCostCenterBlockId.Text = "0";
            txtCostCenter.Text = ObjCostCenterBlock.Cost_Center.ToString();
            txtCCName.Text = ObjCostCenterBlock.Cost_Center_Name.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjCostCenterBlock.ModulePlantGroupCode;
        }

        }
        catch (Exception ex)
        { _log.Error("FillCostCenterBlock", ex); }
    }

    private CostCenterBlock GetCostCenterBlock()
    {
        return ObjCostCenterBlockAccess.GetCostCenterBlock(Convert.ToInt32(lblMasterHeaderId.Text));
    }
    /// <summary>
    /// Carve_LC17&LC23_8400000406
    /// </summary>
    private void CCPC()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id", "");

        if (Session[StaticKeys.SelectedddlCompany].ToString() != null && Session[StaticKeys.SelectedddlCompany].ToString() != "")
        {
            ddlCompanyCode.SelectedValue = Session[StaticKeys.SelectedddlCompany].ToString();
        }
        //else
        //{
        //    ddlCompanyCode.SelectedValue = "32";
        //}
        helperAccess.PopuplateDropDownList(ddlBusinessArea, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
        //helperAccess.PopuplateDropDownList(ddlAccGroup, "pr_GetCostCenterAccGrpList 0", "Module_Name", "Module_Id");
        //ddlCompanyCode.SelectedValue = "32";
        if (Session[StaticKeys.MaterialPlantId].ToString() != null && Session[StaticKeys.MaterialPlantId].ToString() != "")
        {
            ddlBusinessArea.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
        }
        //else
        //{
        //    ddlBusinessArea.Enabled = true;
        //}
    }
    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        try
        {
        if (lblActionType.Text == "B")
        {
            lblSubHeading.Text = "BLOCK";
            chkActPrimaryCost.Text = "  Block";
            chkPlnPrimaryCost.Text = "  Block";
            chkActSecCost.Text = "  Block";
            chkPlnSecCost.Text = "  Block";
            chkActRevPostings.Text = "  Block";
            chkPlnRevPostings.Text = "  Block";
        }
        else
        {
            lblSubHeading.Text = "UNBLOCK";
            chkActPrimaryCost.Text = "  UnBlock";
            chkPlnPrimaryCost.Text = "  UnBlock";
            chkActSecCost.Text = "  UnBlock";
            chkPlnSecCost.Text = "  UnBlock";
            lblchkActRevPostings.Visible = false;
            labelchkActRevPostings.Visible = false;
            chkActRevPostings.Visible = false;
            lblchkPlnRevPostings.Visible = false;
            labelchkPlnRevPostings.Visible = false;
            chkPlnRevPostings.Visible = false;
        }

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private int Save()
    {
        int flg = 1;
        try
        {
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

        }
        catch (Exception ex)
        { _log.Error("Save", ex); }
        return flg;
    }

    private bool SaveCostCenterBlock()
    {
        bool flg = false;
        try
        {
        CostCenterBlock ObjCostCenterBlock = GetControlsValue();

        using (TransactionScope scope = new TransactionScope())
        {
            if ((ObjCostCenterBlockAccess.Save(ObjCostCenterBlock) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
            {
                scope.Complete();
                flg = true;
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
        { _log.Error("SaveCostCenterBlock", ex); }
        return flg;
    }

    private CostCenterBlock GetControlsValue()
    {
        CostCenterBlock ObjCostCenterBlock = new CostCenterBlock();
        Utility objUtil = new Utility();
        try
        {
        ObjCostCenterBlock.CostCenter_Block_Id = Convert.ToInt32(lblCostCenterBlockId.Text);
        ObjCostCenterBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjCostCenterBlock.Cost_Center = txtCostCenter.Text;
        ObjCostCenterBlock.Cost_Center_Name = txtCCName.Text;
        ObjCostCenterBlock.LIActualPrimaryCost = chkActPrimaryCost.Checked ? "1" : "0";
        ObjCostCenterBlock.LIPlanPrimaryCost = chkPlnPrimaryCost.Checked ? "1" : "0";
        ObjCostCenterBlock.LIActualSecondaryCost = chkActSecCost.Checked ? "1" : "0";
        ObjCostCenterBlock.LIPlanSecondaryCost = chkPlnSecCost.Checked ? "1" : "0";
        ObjCostCenterBlock.LIActualRevenuePosting = chkActRevPostings.Checked ? "1" : "0";
        ObjCostCenterBlock.LIPlanRevenuePosting = chkPlnRevPostings.Checked ? "1" : "0";
        ObjCostCenterBlock.Remarks = txtRemarks.Text.ToString();

        ObjCostCenterBlock.IsActive = 1;
        ObjCostCenterBlock.UserId = lblUserId.Text;
        ObjCostCenterBlock.TodayDate = objUtil.GetDate();
        ObjCostCenterBlock.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjCostCenterBlock;
    }

    private bool CheckIsValid()
    {
        bool flag = false;
        try
        {
        if (chkActPrimaryCost.Checked)
            flag = true;
        else if (chkPlnPrimaryCost.Checked)
            flag = true;
        else if (chkActSecCost.Checked)
            flag = true;
        else if (chkPlnSecCost.Checked)
            flag = true;
        else if (chkActRevPostings.Checked)
            flag = true;
        else if (chkPlnRevPostings.Checked)
            flag = true;
        }
        catch (Exception ex)
        { _log.Error("CheckIsValid", ex); }
        return flag;
    }

    private void ConfigureControl()
    {
        try
        {
        if (lblActionType.Text == "B")
        {
            chkActPrimaryCost.Checked = true;
            chkPlnPrimaryCost.Checked = true;
            chkActSecCost.Checked = true;
            chkPlnSecCost.Checked = true;
            chkActRevPostings.Checked = true;
            chkPlnRevPostings.Checked = true;
        }
        else
        {
            chkActPrimaryCost.Checked = false;
            chkPlnPrimaryCost.Checked = false;
            chkActSecCost.Checked = false;
            chkPlnSecCost.Checked = false;
            chkActRevPostings.Checked = false;
            chkPlnRevPostings.Checked = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        if (Save() == 1)
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
                _log.Error("btnUploadDoc_Click", ex);
                //throw ex;
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

    #region Document Upload

    private void BindAttachedDocuments(string MaterialId)
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
            _log.Error("BindAttachedDocuments", ex);
            //throw ex;
        }
        finally
        {
            objDb = null;
        }
    }

    private bool SaveDocuments(string MasterHeaderID)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/CostCenter/CostCenterDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

        try
        {
        savePath = MapPath(StrPath);

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        }
        catch (Exception ex)
        { _log.Error("SaveDocuments", ex); }
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
        catch(Exception ex)
        {
            _log.Error("SaveDocuments", ex);
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
        try
        {

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
            lblMsg.Text = "Error While Saving CostCenter Master Details.";
        }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }
        return flag;
    }

    #endregion
}