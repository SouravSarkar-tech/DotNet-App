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

public partial class Transaction_GLMaster_GLBlock : System.Web.UI.Page
{
    #region Page Events

    GLBlockAccess ObjGLBlockAccess = new GLBlockAccess();
    string docType = "";

    protected void Page_Load(object sender, EventArgs e)
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
                    FillGLBlock();
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
                    Response.Redirect("GLMaster.aspx");
                }
            }
        }
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
                    System.IO.File.Delete(Server.MapPath("GLDocuments") + "/" + lblUploadedFileName.Text);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (Save() == 1)
            {
                if (SaveGLBlock())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    Response.Redirect("GLBlock.aspx");
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

    private void FillGLBlock()
    {
        GLBlock ObjGLBlock = GetGLBlock();
        if (ObjGLBlock.GL_Block_Id > 0)
        {
            txtGLCode.Text = ObjGLBlock.GL_Code;
            lblGLBlockId.Text = ObjGLBlock.GL_Block_Id.ToString();
            ddlCompanyCode.SelectedValue = ObjGLBlock.Company_Code.ToString();
            chkBlockedforCreation.Checked = ObjGLBlock.Blocked_For_Creation.ToString() == "1" ? true : false;
            chkBlockedforPosting.Checked = ObjGLBlock.Blocked_For_Posting.ToString() == "1" ? true : false;
            chkBlockedforPlanning.Checked = ObjGLBlock.Blocked_For_Planning.ToString() == "1" ? true : false;
            chkSelectedPurchasingOrg.Checked = ObjGLBlock.Blocked_For_Posting_CC.ToString() == "1" ? true : false;
            txtRemarks.Text = ObjGLBlock.Remarks.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjGLBlock.ModulePlantGroupCode;

            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        else
        {
            lblGLBlockId.Text = "0";
            ddlCompanyCode.SelectedValue = ObjGLBlock.Company_Code.ToString();

            Session[StaticKeys.SelectedModulePlantGrp] = ObjGLBlock.ModulePlantGroupCode;
        }

    }

    private GLBlock GetGLBlock()
    {
        return ObjGLBlockAccess.GetGLBlock(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();

        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        if (lblActionType.Text == "B")
        {
            chkBlockedforCreation.Text = "  Block";
            chkBlockedforPosting.Text = "  Block";
            chkBlockedforPlanning.Text = "  Block";
            chkSelectedPurchasingOrg.Text = "  Block";
        }
        else
        {
            chkBlockedforCreation.Text = "  UnBlock";
            chkBlockedforPosting.Text = "  UnBlock";
            chkBlockedforPlanning.Text = "  UnBlock";
            chkSelectedPurchasingOrg.Text = "  UnBlock";
        }
    }

    private int Save()
    {
        int flg = 1;
        //bool boolFlg = false;

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

    private bool SaveGLBlock()
    {
        bool flg = false;

        GLBlock ObjGLBlock = GetControlsValue();

        using (TransactionScope scope = new TransactionScope())
        {
            if ((ObjGLBlockAccess.Save(ObjGLBlock) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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

        return flg;
    }

    private GLBlock GetControlsValue()
    {
        GLBlock ObjGLBlock = new GLBlock();
        Utility objUtil = new Utility();

        ObjGLBlock.GL_Block_Id = Convert.ToInt32(lblGLBlockId.Text);
        ObjGLBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjGLBlock.Blocked_For_Creation = chkBlockedforCreation.Checked ? "1" : "0";
        ObjGLBlock.Blocked_For_Posting = chkBlockedforPosting.Checked ? "1" : "0";
        ObjGLBlock.Blocked_For_Planning = chkBlockedforPlanning.Checked ? "1" : "0";
        ObjGLBlock.Blocked_For_Posting_CC = chkSelectedPurchasingOrg.Checked ? "1" : "0";
        ObjGLBlock.Remarks = txtRemarks.Text.ToString();

        ObjGLBlock.IsActive = 1;
        ObjGLBlock.UserId = lblUserId.Text;
        ObjGLBlock.TodayDate = objUtil.GetDate();
        ObjGLBlock.IPAddress = objUtil.GetIpAddress();

        return ObjGLBlock;
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (chkBlockedforCreation.Checked)
            flag = true;
        else if (chkBlockedforPosting.Checked)
            flag = true;
        else if (chkBlockedforPlanning.Checked)
            flag = true;
        else if (chkSelectedPurchasingOrg.Checked)
            flag = true;
        return flag;
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
            throw ex;
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
        string StrPath = "~/Transaction/GLMaster/GLDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
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
            lblMsg.Text = "Error While Saving GL Master Details.";
        }

        return flag;
    }

    #endregion
}