using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using System.Configuration;
using log4net;

public partial class Transaction_ProfitCenter_ProfitCenterCreate : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    ProfitCenterMasterAccess ObjProfitCenterMasterAccess = new ProfitCenterMasterAccess();
    HelperAccess helperAccess = new HelperAccess();

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

                        PopuplateDropDownList();

                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                        FillGeneralData();

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            file_upload.Visible = true;
                            grdAttachedDocs.Columns[1].Visible = true;
                        }
                        else
                        {
                            grdAttachedDocs.Columns[1].Visible = false;
                            file_upload.Visible = false;
                            file_upload.Visible = false;
                        }

                    }
                    else
                    {
                        Response.Redirect("ProfitCenterMaster.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save() == 1)
                {
                    if (SaveProfitCenterMaster())
                    {
                        string pageURL = btnPrevious.CommandArgument.ToString();
                        Response.Redirect(pageURL);
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
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                string Module = Session[StaticKeys.SelectedModuleId].ToString();

                if (Save() == 1)
                {
                    if (SaveProfitCenterMaster())
                    {
                        lblMsg.Text = Messages.GetMessage(1);
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;
                        BindAttachedDocuments(lblMasterHeaderId.Text);
                        Response.Redirect("ProfitCenterCreate.aspx");
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
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save() == 1)
                {
                    if (SaveProfitCenterMaster())
                    {
                        string pageURL = btnNext.CommandArgument.ToString();
                        Response.Redirect(pageURL);
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
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    private void PopuplateDropDownList()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id", "");
            helperAccess.PopuplateDropDownList(ddlControllingArea, "pr_GetControllingArea", "Name", "ID", "");
            //helperAccess.PopuplateDropDownList(ddlpcGroup, "pr_GetCostCenterCategory", "Name", "ID", "");
            //helperAccess.PopuplateDropDownList(ddlSegment, "pr_GetHierarchyArea", "Name", "ID", "");
            helperAccess.PopuplateDropDownList(ddlpcGroup, "pr_GetDropDownListByControlName_PCM 'X','ddlpcGroup'", "LookUp_Desc", "LookUp_Code");
            helperAccess.PopuplateDropDownList(ddlSegment, "pr_GetDropDownListByControlName_PCM 'X','ddlSegment'", "LookUp_Desc", "LookUp_Code");
            //ddlCompanyCode.SelectedValue = "32";
            ddlSegment.SelectedValue = "PHARMA";
            ddlControllingArea.SelectedValue = "19";
            ddlControllingArea.Enabled = false;
            ddlCompanyCode.Enabled = false;

            if (Session[StaticKeys.SelectedddlCompany].ToString() != null && Session[StaticKeys.SelectedddlCompany].ToString() != "")
            { ddlCompanyCode.SelectedValue = Session[StaticKeys.SelectedddlCompany].ToString(); }
            else
            {
                ddlCompanyCode.SelectedValue = "32";
            }

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    private void FillGeneralData()
    {
        try
        {
            ProfitCenterCreate ObjProfitCenterCreate = GetProfitCenterMasterData();
            Utility ObjUtil = new Utility();
            if (ObjProfitCenterCreate.PCMaster_Create_Id > 0)
            {
                lblMasterHeaderId.Text = ObjProfitCenterCreate.Master_Header_Id.ToString();
                ddlCompanyCode.SelectedValue = ObjProfitCenterCreate.Company_Code.ToString();
                txtProfitCenter.Text = ObjProfitCenterCreate.sProfitCenter.ToString();
                txtRefProfitCenter.Text = ObjProfitCenterCreate.sRef_ProfitCenter.ToString();
                txtValidFrom.Text = ObjUtil.GetMMDDYYYY(ObjProfitCenterCreate.dAnalysisPeriodF);//ObjProfitCenterCreate.dAnalysisPeriodF.ToString();
                //txtValidFrom.Text = "10/11/2022";
                txtValidTo.Text = ObjUtil.GetMMDDYYYY(ObjProfitCenterCreate.dAnalysisPeriodT);// "31/12/9999";
                ddlControllingArea.SelectedValue = "19";
                txtName.Text = ObjProfitCenterCreate.sName.ToString();
                txtLongText.Text = ObjProfitCenterCreate.sLongText.ToString();
                txtUserResp.Text = ObjProfitCenterCreate.sUserRespons.ToString();
                txtPersonResp.Text = ObjProfitCenterCreate.sPersonRespons.ToString();
                txtDepartment.Text = ObjProfitCenterCreate.sDepartment;
                ddlpcGroup.SelectedValue = ObjProfitCenterCreate.sProfitCtrGrp;
                ddlSegment.SelectedValue = ObjProfitCenterCreate.sSegment;
                txtRemarks.Text = ObjProfitCenterCreate.sRemarks;
                BindAttachedDocuments(lblMasterHeaderId.Text);
            }

        }
        catch (Exception ex)
        { _log.Error("FillGeneralData", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private bool CheckIsValid()
    {
        bool flag = false;
        try
        {
            if (lblActionType.Text != "C")
                flag = true;
            else
            {
                if (txtName.Text != "")
                    flag = true;
                else if (txtLongText.Text != "")
                    flag = true;
                else if (txtPersonResp.Text != "")
                    flag = true;
                else if (ddlControllingArea.SelectedValue != "0")
                    flag = true;
                else if (txtUserResp.Text != "")
                    flag = true;
                else if (txtPersonResp.Text != "")
                    flag = true;
                else if (ddlSegment.SelectedValue != "0")
                    flag = true;
                else if (ddlpcGroup.SelectedValue != "0")
                    flag = true;
            }

        }
        catch (Exception ex)
        { _log.Error("CheckIsValid", ex); }

        return flag;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private int Save()
    {
        int flg = 1;
        //bool boolFlg = false;
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

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private bool SaveProfitCenterMaster()
    {
        bool Flag = false;
        ProfitCenterCreate ObjProfitCenterCreate = GetControlsValue();

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if ((ObjProfitCenterMasterAccess.Save(ObjProfitCenterCreate) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
                {
                    scope.Complete();
                    Flag = true;
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
            _log.Error("SaveProfitCenterMaster", ex);
        }
        return Flag;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private ProfitCenterCreate GetProfitCenterMasterData()
    {
        return ObjProfitCenterMasterAccess.GetProfitCenterMasterData(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private ProfitCenterCreate GetControlsValue()
    {
        ProfitCenterCreate ObjProfitCenterCreate = new ProfitCenterCreate();
        Utility objUtil = new Utility(); 
        try
        {
            ObjProfitCenterCreate.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjProfitCenterCreate.sProfitCenter = txtProfitCenter.Text.Trim();
            ObjProfitCenterCreate.sRef_ProfitCenter = txtRefProfitCenter.Text.Trim();
            ObjProfitCenterCreate.sContrlArea = ddlControllingArea.SelectedValue;
            //ObjProfitCenterCreate.dAnalysisPeriodF = txtValidFrom.Text;
            //ObjProfitCenterCreate.dAnalysisPeriodT = txtValidTo.Text;

            ObjProfitCenterCreate.dAnalysisPeriodF = objUtil.GetMMDDYYYY(txtValidFrom.Text);
            ObjProfitCenterCreate.dAnalysisPeriodT = objUtil.GetMMDDYYYY(txtValidTo.Text);

            ObjProfitCenterCreate.sName = txtName.Text.Trim();
            ObjProfitCenterCreate.sLongText = txtLongText.Text.Trim();
            ObjProfitCenterCreate.sUserRespons = txtUserResp.Text.Trim();
            ObjProfitCenterCreate.sPersonRespons = txtPersonResp.Text.Trim();
            ObjProfitCenterCreate.sDepartment = txtDepartment.Text;
            ObjProfitCenterCreate.sProfitCtrGrp = ddlpcGroup.SelectedValue;
            ObjProfitCenterCreate.sSegment = ddlSegment.SelectedValue;
            ObjProfitCenterCreate.sRemarks = txtRemarks.Text.Trim();
            ObjProfitCenterCreate.UserId = lblUserId.Text;
            ObjProfitCenterCreate.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjProfitCenterCreate;
    }

    #region Doc Upload Done

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                        System.IO.File.Delete(Server.MapPath("ProfitCenterDoc") + "/" + lblUploadedFileName.Text);
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


    /// <summary>
    /// Done
    /// </summary>
    /// <param name="MaterialId"></param>
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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="MasterHeaderID"></param>
    /// <returns></returns>
    private bool SaveDocuments(string MasterHeaderID)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/ProfitCenter/ProfitCenterDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
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
        catch (Exception ex)
        {
            _log.Error("SaveDocuments", ex);
            return false;
        }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="uploadfile"></param>
    /// <param name="StrPath"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
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