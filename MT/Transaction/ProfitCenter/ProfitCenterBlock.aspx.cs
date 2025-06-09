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

public partial class Transaction_ProfitCenter_ProfitCenterBlock : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Page Events

    ProfitCenterMasterAccess ObjProfitCenterMasterAccess = new ProfitCenterMasterAccess();
    string docType = "";

    /// <summary>
    /// done
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

                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        ConfigureControl();
                        FillProfitCenterBlock();
                        BindAttachedDocuments(lblMasterHeaderId.Text);

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Save() == 1)
            {
                if (SaveProfitCenterBlock())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    Response.Redirect("ProfitCenterBlock.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
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
    private void FillProfitCenterBlock()
    {
        try
        {
            ProfitCenterBlock ObjProfitCenterBlock = GetProfitCenterBlock();
            if (ObjProfitCenterBlock.PCMaster_Block_Id > 0)
            {
                lblProfitCenterBlockId.Text = ObjProfitCenterBlock.PCMaster_Block_Id.ToString();
                txtProfitCenter.Text = ObjProfitCenterBlock.sProfitCenter.ToString();
                txtPCName.Text = ObjProfitCenterBlock.sProfitCenterName.ToString();
                ddlBlockValue.SelectedValue = ObjProfitCenterBlock.bBlockUnBlockStatus;
                txtRemarks.Text = ObjProfitCenterBlock.sRemarks;
                BindAttachedDocuments(lblMasterHeaderId.Text);
            }
            else
            {
                lblProfitCenterBlockId.Text = "0";
                txtProfitCenter.Text = Convert.ToString(Session[StaticKeys.MaterialNo]).Trim();
                txtPCName.Text = Convert.ToString(Session[StaticKeys.ProfitCenterName]).Trim(); 
            }

        }
        catch (Exception ex)
        { _log.Error("FillProfitCenterBlock", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private ProfitCenterBlock GetProfitCenterBlock()
    {
        return ObjProfitCenterMasterAccess.GetProfitCenterBlock(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private bool SaveProfitCenterBlock()
    {
        bool flg = false;
        try
        {
            ProfitCenterBlock ObjProfitCenterBlock = GetControlsValue();

            using (TransactionScope scope = new TransactionScope())
            {
                if ((ObjProfitCenterMasterAccess.SaveBlck(ObjProfitCenterBlock) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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
        { _log.Error("SaveProfitCenterBlock", ex); }
        return flg;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private ProfitCenterBlock GetControlsValue()
    {
        ProfitCenterBlock ObjProfitCenterBlock = new ProfitCenterBlock();
        Utility objUtil = new Utility();
        try
        {
            ObjProfitCenterBlock.PCMaster_Block_Id = Convert.ToInt32(lblProfitCenterBlockId.Text);
            ObjProfitCenterBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjProfitCenterBlock.sProfitCenter = txtProfitCenter.Text;
            ObjProfitCenterBlock.sProfitCenterName = txtPCName.Text;
            ObjProfitCenterBlock.bBlockUnBlockStatus = ddlBlockValue.SelectedValue;
            ObjProfitCenterBlock.sRemarks = txtRemarks.Text.ToString();

            ObjProfitCenterBlock.IsActive = 1;
            ObjProfitCenterBlock.UserId = lblUserId.Text;
            ObjProfitCenterBlock.TodayDate = objUtil.GetDate();
            ObjProfitCenterBlock.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjProfitCenterBlock;
    }

    /// <summary>
    /// Done
    /// </summary>
    private void ConfigureControl()
    {
        try
        {
            if (lblActionType.Text == "B")
            {
                lblBlock_Unblock.Text = "Block";
                ddlBlockValue.SelectedValue = "0";
            }
            else
            {
                lblBlock_Unblock.Text = "Unblock";
                ddlBlockValue.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    #region Document Upload Done

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
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
                lblMsg.Text = "Error While Saving Profit Center Master Details.";
            }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }
        return flag;
    }

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


    #endregion
}