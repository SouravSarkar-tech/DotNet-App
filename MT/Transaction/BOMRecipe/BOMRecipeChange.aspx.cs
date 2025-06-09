using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.IO;
using Accenture.MWT.DomainObject;
using System.Data.SqlClient;
using log4net;
public partial class Transaction_BOMRecipe_BOMRecipeChange : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public bool isEditable { get; set; }

    #region Page Events

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
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                        lblBRPlantGrpId.Text = Session[StaticKeys.MatPlantGrp].ToString();

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            isEditable = true;
                        }

                        FillBRChangeData();

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = false;
                            btnNext.Visible = true;

                            grdAttachedDocs.Columns[1].Visible = true;

                            //lnkAddNew.Visible = true;
                            //grvMaterialChange.Columns[6].Visible = true;
                        }
                        else
                        {
                            //grvMaterialChange.Columns[6].Visible = false;
                            //lnkAddNew.Visible = false;
                            //file_upload.Visible = false;

                            //ChangeExcelUpload1.Visible = false;
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                //ExcelDownload1.ActionType = "C";
                                //ExcelDownload1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("BOMRecipeMaster.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateDoc())
            {
                if (Save())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    FillBRChangeData();
                    Response.Redirect("BOMRecipeChange.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Kindly attach atleast one change document.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (Save())
            {
                Session[StaticKeys.AddAlertMsg] = "BOM Recipe Change Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
                Response.Redirect("BOMRecipeMaster.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveDocuments(lblMasterHeaderId.Text))
            {
                BindAttachedDocuments(lblMasterHeaderId.Text);
            }
            else
            {
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnAttach_Click", ex); }
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
                        System.IO.File.Delete(Server.MapPath("BOMRecipeDocs") + "/" + lblUploadedFileName.Text);
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

    #region Methods

    private bool Save()
    {
        bool flg = false;
        try
        {
            if (SaveDocuments(lblMasterHeaderId.Text) && SaveRemarks())
            {
                FillBRChangeData();
                flg = true;
            }
            else
            {
                lblMsg.Text = "Error while saving data.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("Save", ex);
        }

        return flg;
    }

    private bool SaveRemarks()
    {
        bool flg = false;
        BOMRecipeChange objBRChange = new BOMRecipeChange();
        BOMAccess objBOMAccess = new BOMAccess();

        try
        {
            objBRChange = GetControValues();
            if (objBOMAccess.SaveRemarks(objBRChange) > 0)
                flg = true;
            else
            {
                flg = false;
                lblMsg.Text = "Error while saving data.";
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SaveRemarks", ex);
        }

        return flg;
    }

    private BOMRecipeChange GetControValues()
    {
        BOMRecipeChange objBRChange = new BOMRecipeChange();
        Utility objUtil = new Utility();
        try
        {
            objBRChange.BRChangeId = Convert.ToInt32(lblBRChangeId.Text);
            objBRChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            objBRChange.Remarks = txtRemarks.Text;

            objBRChange.UserId = lblUserId.Text;
            objBRChange.TodayDate = objUtil.GetDate();
            objBRChange.IPAddress = objUtil.GetIpAddress();
            objBRChange.Mode = lblMode.Text;
        }
        catch (Exception ex)
        { _log.Error("GetControValues", ex); }
        return objBRChange;
    }

    private void FillBRChangeData()
    {
        BOMAccess objBOMAccess = new BOMAccess();
        BOMRecipeChange objBRChange = new BOMRecipeChange();
        try
        {
            objBRChange = objBOMAccess.GetRemarks(lblMasterHeaderId.Text);
            if (objBRChange.BRChangeId > 0)
            {
                lblBRChangeId.Text = objBRChange.BRChangeId.ToString();
                txtRemarks.Text = objBRChange.Remarks;
            }
            else
                lblBRChangeId.Text = "0";

            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        catch (Exception ex)
        {
            _log.Error("FillBRChangeData", ex);
            //throw ex;
        }
    }

    private bool ValidateDoc()
    {
        bool flg = false;
        try
        {
            if (grdAttachedDocs.Rows.Count > 0)
                flg = true;
        }
        catch (Exception ex)
        { _log.Error("ValidateDoc", ex); }
        return flg;
    }

    #endregion

    #region Document Upload

    private void BindAttachedDocuments(string master_header_id)
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

    private bool SaveDocuments(string master_header_id)
    {
        bool flg = false;
        string savePath = "";
        string StrPath = "";
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        try
        {
            DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

            Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
            //string savePath = "";
            StrPath = "~/Transaction/BOMRecipe/BOMRecipeDocs/" + Session[StaticKeys.RequestNo].ToString() + "/";
            savePath = MapPath(StrPath);
            //bool flg = false;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }
        catch (Exception ex)
        { _log.Error("SaveDocuments1", ex); }
        try
        {
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];

                if (uploadfile.ContentLength > 0)
                {
                    if (Path.GetExtension(uploadfile.FileName).ToLower() == ".xls" || Path.GetExtension(uploadfile.FileName).ToLower() == ".xlsx")
                    {
                        UploadDocument(uploadfile, StrPath, savePath);
                        flg = true;
                    }
                    else
                    {
                        lblMsg.Text = "Kindly attach only .xls or .xlsx files.";
                        pnlMsg.Visible = true;
                        flg = false;
                    }
                }
                else
                {
                    flg = true;
                }
            }
            return flg;
        }
        catch (Exception ex)
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
        try
        {
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
                lblMsg.Text = "Error While Saving Material Details.";
            }
        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }

        return flag;
    }

    #endregion

}