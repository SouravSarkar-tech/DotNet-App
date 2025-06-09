using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Transaction_Material_MaterialMassExtension : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    string sdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    DateTime date = System.DateTime.Now;
        //    sdate = date.ToString("dd/MM/yyyy");
        //    sdate = sdate.Replace(@"/", "");
        //    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Home");
        //}
        //catch (Exception ex)
        //{

        //}
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
                        lblMatPlantGrpId.Text = Session[StaticKeys.MatPlantGrp].ToString();

                        ExcelDownloadEXTDATA.Visible = false;
                        ExcelDownloadError.Visible = false;
                        PopuplateDropDownListMass();
                        BindAttachedDocuments(lblMasterHeaderId.Text);
                        hlMSImportFormat.NavigateUrl = "";

                        //if (ddlTypeOfMassUpdm.SelectedValue == "11")
                        //{
                        hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatExtensionDoc/MatExtensionSMMP/MaterialExtension.xlsx";
                        //}
                        //else if (ddlTypeOfMassUpdm.SelectedValue == "12")
                        //{
                        //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/SelectionMethod.xlsx";
                        //}
                        //else if (ddlTypeOfMassUpdm.SelectedValue == "13")
                        //{
                        //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/PlannedPrice.xlsx";
                        //}
                        //else if (ddlTypeOfMassUpdm.SelectedValue == "14")
                        //{
                        //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/TaggingofBOM.xlsx";
                        //}
                        //else if (ddlTypeOfMassUpdm.SelectedValue == "15")
                        //{
                        //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/ProductHierarchy.xlsx";
                        //}
                        //else if (ddlTypeOfMassUpdm.SelectedValue == "16")
                        //{
                        //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Other.xlsx";
                        //}

                        //HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            trMassBtn.Visible = true;
                        }
                        else
                        {
                            trMassBtn.Visible = false;
                            btnMassSave.Visible = false;
                            fileUploadMS.Visible = false;
                            lblFileMessage.Visible = false;
                            btnMSProcess.Visible = false;
                            lblselectcap.Visible = false;
                            lblSelectFile.Visible = false;
                            hlMSImportFormat.Visible = false;
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                ExcelDownloadEXTDATA.Visible = true;
                                ExcelDownloadError.Visible = true;
                            }

                        }


                    }
                    else
                    {
                        Response.Redirect("MaterialMaster.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

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
                ddlTypeOfMassUpdm.SelectedValue = dstData.Tables[0].Rows[0]["Document_Type"].ToString();
                Session[StaticKeys.TypeOfMassUpdm] = ddlTypeOfMassUpdm.SelectedValue;
            }
            else
            {
                grdAttachedDocs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("BindAttachedDocuments", ex);
        }
        finally
        {
            objDb = null;
        }
    }

    private bool SaveDocuments(string MaterialId)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        string savePath = "";
        string StrPath = String.Empty;
        try
        {
            DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

            Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();

            //string StrPath = "~/Transaction/Material/MaterialDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

            //if (ddlTypeOfMassUpdm.SelectedValue == "11")
            //{
            //    StrPath = "/Transaction/Material/MatChangeDoc/MRPUpdExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            //}
            //else if (ddlTypeOfMassUpdm.SelectedValue == "12")
            //{
            StrPath = "/Transaction/Material/MatExtensionDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            //}
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
                //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "HttpPostedFile" + HelperAccess.ReqType);
                HttpPostedFile uploadfile = fileCollection[i];
                //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "HttpPostedFile" + uploadfile);
                if (uploadfile.ContentLength > 0)
                {
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "UploadDocument Start");
                    UploadDocument(uploadfile, StrPath, savePath);
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "UploadDocument End");
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            _log.Error("SaveDocuments1", ex);
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

        try
        {
            //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "sufix" + sufix);
            if (uploadfile.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();
                //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "fileExtension" + fileExtension);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {

                    string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
                    savePath = savePath + "\\" + uploadedFileName;
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "uploadedFileName" + uploadedFileName);
                    // WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "savePath" + savePath);

                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Document_Upload_Id" + sufix);
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Master_Header_Id" + Convert.ToInt32(lblMasterHeaderId.Text));
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Request_No" + Session[StaticKeys.RequestNo].ToString());
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Document_Type" + ddlTypeOfMassUpdm.SelectedValue);
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Document_Name" + Path.GetFileName(uploadfile.FileName));
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Document_Path" + StrPath + uploadedFileName);
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "UserId" + lblUserId.Text);

                    ObjDoc.Document_Upload_Id = 0;
                    ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
                    ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
                    //ObjDoc.Document_Type = "";
                    ObjDoc.Document_Type = ddlTypeOfMassUpdm.SelectedValue;
                    ObjDoc.Document_Name = Path.GetFileName(uploadfile.FileName);
                    ObjDoc.Document_Path = StrPath + uploadedFileName;
                    ObjDoc.Remarks = "";
                    ObjDoc.IsActive = 1;
                    ObjDoc.UserId = lblUserId.Text;
                    ObjDoc.IPAddress = objUtil.GetIpAddress();
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "SaveAs START");
                    uploadfile.SaveAs(savePath);
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "SaveAs EDN");
                    ObjDocUploadAccess.Save(ObjDoc);
                    //WriteMatChangeLog("MatMassExtLog" + sdate + ".txt", "Save END");

                    flag = true;

                }
                else
                {
                    flag = false;
                    lblMsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
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
                        //System.IO.File.Delete(Server.MapPath("MaterialDocuments") + "/" + lblUploadedFileName.Text);
                        System.IO.File.Delete(Server.MapPath("MatExtensionDoc") + "/" + lblUploadedFileName.Text);
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
                    _log.Error("grdAttachedDocs_RowCommand1", ex);
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
    protected void btnbackMsg_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("MaterialMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
    protected void btnMSProcess_Click(object sender, EventArgs e)
    {
        if (fileUploadMS.HasFile)
        {
            lblMsg.Text = "";
            try
            {
                DataAccessLayer objDb = new DataAccessLayer();
                DataSet dstData = new DataSet();
                DocumentUploadAccess objDoc = new DocumentUploadAccess();

                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = "Please remove old file and attache new file";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
                else
                {
                    if ((SaveDocuments(lblMasterHeaderId.Text)))
                    {
                        BindAttachedDocuments(lblMasterHeaderId.Text);
                        MaterialMasterAccess objAccess = new MaterialMasterAccess();
                        Utility objUtil = new Utility();
                        String sstatus = "Vaild";
                        objAccess.SaveMassSync(lblMasterHeaderId.Text, sstatus, lblUserId.Text, objUtil.GetIpAddress(), "E", false);

                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewDialog();", true);
                    }
                    else
                    {
                        lblMsg.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }

                }

            }
            catch (Exception ex)
            {
                _log.Error("btnMSProcess_Click", ex);
                //throw ex;
            }
        }
        else { }
    }

    private bool CheckIsValidMass()
    {
        bool flg = false;
        try
        {
        if (grdAttachedDocs.Rows.Count > 0)
            flg = true;
        }
        catch (Exception ex)
        { _log.Error("CheckIsValidMass", ex); }
        return flg;
    }

    protected void btnMassSave_Click(object sender, EventArgs e)
    {
        try
        {
        if (CheckIsValidMass())
        {
            if (SaveMass())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                Response.Redirect("MaterialMassExtension.aspx");
            }
        }
        else
        {
            lblMsg.Text = "Please upload file.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnMassSave_Click", ex); }
    }

    protected bool SaveMass()
    {
        bool Flag = false;
        //MaterialChange ObjMaterialChange = GetControlsValueMass();

        //try
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (ObjMaterialChangeAccess.SaveMass(ObjMaterialChange) > 0)
        //        {
        //            scope.Complete();
        //            Flag = true;
        //        }
        //        else
        //        {
        //            lblMsg.Text = Messages.GetMessage(-1);
        //            pnlMsg.CssClass = "error";
        //            pnlMsg.Visible = true;
        //        }
        //    }
        //}

        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        return Flag;
    }

    private void PopuplateDropDownListMass()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlTypeOfMassUpdm, "pr_GetDropDownListByControlNameModuleType 'M','ddlTypeOfMassExt'", "LookUp_Desc", "LookUp_Code", "");
            if (Session[StaticKeys.TypeOfMassUpdm].ToString() != "")
                ddlTypeOfMassUpdm.SelectedValue = Convert.ToString(Session[StaticKeys.TypeOfMassUpdm]);
        }
        catch (Exception ex)
        {
            _log.Error("PopuplateDropDownListMass", ex);
        }
    }

    protected void ddlTypeOfMassUpdm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        hlMSImportFormat.NavigateUrl = "";

        if (ddlTypeOfMassUpdm.SelectedValue == "21")
        {
            hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatExtensionDoc/MatExtensionSMMP/MaterialExtension.xlsx";
        }
        else if (ddlTypeOfMassUpdm.SelectedValue == "22")
        {
            hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatExtensionDoc/MatExtensionSMMP/MaterialExtension.xlsx";
            }
        }
        catch (Exception ex)
        { _log.Error("ddlTypeOfMassUpdm_SelectedIndexChanged", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

    }



    //public void WriteMatChangeLog(string strFileName, string strMessage)
    //{
    //    try
    //    {
    //        FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ChangeMaterialLog", strFileName), FileMode.Append, FileAccess.Write);
    //        StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
    //        objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
    //        objStreamWriter.Close();
    //        objFilestream.Close();
    //        //return true;  
    //    }
    //    catch (Exception ex)
    //    {
    //        string x = ex.Message;
    //    }
    //}


}