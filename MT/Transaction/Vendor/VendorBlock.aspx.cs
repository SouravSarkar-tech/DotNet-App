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

public partial class Transaction_Vendor_VendorBlock : System.Web.UI.Page
{

    VendorBlockAccess ObjVendorBlockAccess = new VendorBlockAccess();
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
                    FillVendorBlock();
                    BindAttachedDocuments();

                    //HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    string mode = Session[StaticKeys.Mode].ToString();
                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        //Vendor workflow modification start
                        //Code commented on 28/09/2018 to display file name during upload
                        //if (Session[StaticKeys.SelectedModuleId].ToString() != "62")
                        //{
                        //    grdAttachedDocs.Columns[0].Visible = false;
                        //}
                        //end
                        //Vendor workflow modification end
                        //Vendor workflow modification start
                        //grdAttachedDocs.Columns[2].Visible = true;
                        //Vendor workflow modification end
                        grdAttachedDocs.Columns[1].Visible = true;
                        file_upload.Visible = true;
                    }
                    else
                    {
                        //Vendor workflow modification start
                        //if (Session[StaticKeys.SelectedModuleId].ToString() == "62")
                        //{
                        //    //grdAttachedDocs.Columns[2].Visible = false;
                        //}
                        ////Vendor workflow modification end
                        //grdAttachedDocs.Columns[1].Visible = false;
                        btnUploadDoc.Visible = false;
                        //Vendor workflow modification start
                        //grdMandDocs.Visible = false;
                        //Vendor workflow modification end

                        file_upload.Visible = false;
                    }
                    ConfigureControl();

                }
                else
                {
                    Response.Redirect("VendorMaster.aspx");
                }
            }
            //Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification
            pnlWarning.CssClass = "warning";
            lblWarning.Text = "Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.";
            //End
        }
    }

    protected void chkSelectedPurchasingOrg_CheckedChanged(object sender, EventArgs e)
    {
        chkAllPurchasingOrg.Checked = false;
        reqddlPurchaseOrg.Visible = chkSelectedPurchasingOrg.Checked;
        lableddlPurchaseOrg.Visible = chkSelectedPurchasingOrg.Checked;
    }

    protected void chkAllPurchasingOrg_CheckedChanged(object sender, EventArgs e)
    {
        chkSelectedPurchasingOrg.Checked = false;
        reqddlPurchaseOrg.Visible = false;
        lableddlPurchaseOrg.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (Save() == 1)
            {
                if (SaveVendorBlock())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    Response.Redirect("VendorBlock.aspx");
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
    
    private void FillVendorBlock()
    {
        VendorBlock ObjVendorBlock = GetVendorBlock();
        if (ObjVendorBlock.Vendor_Block_Id > 0)
        {
            lblVendorBlockId.Text = ObjVendorBlock.Vendor_Block_Id.ToString();
            ddlCompanyCode.SelectedValue = ObjVendorBlock.Company_Code.ToString();
            ddlPurchaseOrg.SelectedValue = ObjVendorBlock.Purchase_Org.ToString();
            chkAllCompanies.Checked = ObjVendorBlock.IsAllCompanyBlock.ToString() == "1" ? true : false;
            chkSelectedCompany.Checked = ObjVendorBlock.IsSelectedCompanyBlock.ToString() == "1" ? true : false;
            chkAllPurchasingOrg.Checked = ObjVendorBlock.IsAllPurchaseOrgBlock.ToString() == "1" ? true : false;
            chkSelectedPurchasingOrg.Checked = ObjVendorBlock.IsSelectedPurchaseOrgBlock.ToString() == "1" ? true : false;
            txtRemarks.Text = ObjVendorBlock.Remarks.ToString();
            ddlBlockFunction.SelectedValue = ObjVendorBlock.Block_Function.ToString();
            ddlPaymentBlock.SelectedValue = ObjVendorBlock.Payment_Block.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjVendorBlock.ModulePlantGroupCode;

            BindAttachedDocuments();
        }
        else
        {
            lblVendorBlockId.Text = "0";
            ddlCompanyCode.SelectedValue = ObjVendorBlock.Company_Code.ToString();

            Session[StaticKeys.SelectedModulePlantGrp] = ObjVendorBlock.ModulePlantGroupCode;
        }
        //Vendor workflow modification start
        //if (Session[StaticKeys.SelectedModuleId].ToString() == "62")
        //    BindMandatoryDocList();
        //Vendor workflow modification end
    }

    private VendorBlock GetVendorBlock()
    {
        return ObjVendorBlockAccess.GetVendorBlock(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();

        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlPurchaseOrg, "pr_GetPurchaseOrgList 0", "Purchase_Org_Name", "Purchase_Org_Id");
        if (lblActionType.Text == "B")
        {
            chkAllCompanies.Text = "  Block";
            chkSelectedCompany.Text = "  Block";
            chkAllPurchasingOrg.Text = "  Block";
            chkSelectedPurchasingOrg.Text = "  Block";
            helperAccess.PopuplateDropDownList(ddlBlockFunction, "pr_GetDropDownListByControlNameModuleType 'V','ddlBlockFunction'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPaymentBlock, "pr_GetDropDownListByControlNameModuleType 'V','ddlPaymentBlock'", "LookUp_Desc", "LookUp_Code");
        }
        else
        {
            chkAllCompanies.Text = "  UnBlock";
            chkSelectedCompany.Text = "  UnBlock";
            chkAllPurchasingOrg.Text = "  UnBlock";
            chkSelectedPurchasingOrg.Text = "  UnBlock";
            helperAccess.AddUnblockOption(ddlBlockFunction);
            helperAccess.AddUnblockOption(ddlPaymentBlock);
        }
    }
    
    //private bool Save()
    //{
    //    int flg = 0;
    //    bool boolFlg = false;

    //    HttpFileCollection fileCollection = Request.Files;
    //    string fileExtension = string.Empty;
    //    for (int i = 0; i < fileCollection.Count; i++)
    //    {
    //        HttpPostedFile uploadfile = fileCollection[i];
    //        if (uploadfile.ContentLength > 0)
    //        {
    //            fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();
    //            if ((fileExtension == ".pdf") || (fileExtension == ".xls") || (fileExtension == ".xlsx") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png"))
    //            {
    //                flg = 1;
    //            }
    //            else
    //            {
    //                flg = 2;
    //                break;
    //            }
    //        }
    //    }

    //    if (flg == 2)
    //    {
    //        lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .Png, .Pdf files allowed.";
    //        pnlMsg.Visible = true;
    //        pnlMsg.CssClass = "error";
    //    }
    //    else
    //    {
    //        boolFlg = SaveVendorBlock();
    //    }
    //    return boolFlg;
    //}

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

        //if (flg == 2)
        //{
        //    lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
        //    pnlMsg.Visible = true;
        //    pnlMsg.CssClass = "error";
        //}
        //else
        //{
        //    boolFlg = SaveVendorBlock();
        //}
        return flg;
    }

    private bool SaveVendorBlock()
    {
        bool flg = false;

        VendorBlock ObjVendorBlock = GetControlsValue();
        if ((ObjVendorBlockAccess.Save(ObjVendorBlock) > 0)&& (SaveDocuments(lblMasterHeaderId.Text)))
        {
            flg = true;
        }
        else
        {
            lblMsg.Text = Messages.GetMessage(-1);
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

        return flg;
    }

    private VendorBlock GetControlsValue()
    {
        VendorBlock ObjVendorBlock = new VendorBlock();
        Utility objUtil = new Utility();

        ObjVendorBlock.Vendor_Block_Id = Convert.ToInt32(lblVendorBlockId.Text);
        ObjVendorBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjVendorBlock.Purchase_Org = ddlPurchaseOrg.SelectedValue.ToString();
        ObjVendorBlock.IsAllCompanyBlock = chkAllCompanies.Checked ? "1" : "0";
        ObjVendorBlock.IsSelectedCompanyBlock = chkSelectedCompany.Checked ? "1" : "0";
        ObjVendorBlock.IsAllPurchaseOrgBlock = chkAllPurchasingOrg.Checked ? "1" : "0";
        ObjVendorBlock.IsSelectedPurchaseOrgBlock = chkSelectedPurchasingOrg.Checked ? "1" : "0";
        ObjVendorBlock.Block_Function = ddlBlockFunction.SelectedValue.ToString();
        ObjVendorBlock.Payment_Block = ddlPaymentBlock.SelectedValue.ToString();
        ObjVendorBlock.Remarks = txtRemarks.Text.ToString();

        ObjVendorBlock.IsActive = 1;
        ObjVendorBlock.UserId = lblUserId.Text;
        ObjVendorBlock.TodayDate = objUtil.GetDate();
        ObjVendorBlock.IPAddress = objUtil.GetIpAddress();

        return ObjVendorBlock;
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (chkAllCompanies.Checked)
            flag = true;
        else if (chkSelectedCompany.Checked)
            flag = true;
        else if (chkAllPurchasingOrg.Checked)
            flag = true;
        else if (chkSelectedPurchasingOrg.Checked)
            flag = true;
        else if (ddlBlockFunction.SelectedValue != "")
            flag = true;
        else if (ddlPaymentBlock.SelectedValue != "0")
            flag = true;

        return flag;
    }

    private void ConfigureControl()
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Vendor_Master_Block_or_Unblock obj = new SectionConfiguration.Vendor_Master_Block_or_Unblock();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
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
                BindAttachedDocuments();
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

    //Vendor workflow modification start
    //private string GetSelectedPkID()
    //{
    //    string strPk = string.Empty;
    //    try
    //    {
    //        foreach (GridViewRow grv in grdMandDocs.Rows)
    //        {
    //            RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
    //            if (rdo.Checked == true)
    //            {
    //                Label lblMandDocId = grv.FindControl("lblMandDocId") as Label;
    //                strPk = lblMandDocId.Text;

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return strPk;
    //}
    //Vendor workflow modification end
   
    #region Document Upload

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
                    //string StrPath = "~/Transaction/Vendor/VendorDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
                    string savePath = MapPath(lblUploadedFileName.Text);
                    //System.IO.File.Delete(savePath + "/" + lblUploadedFileName.Text);
                    if (System.IO.File.Exists(savePath))
                        System.IO.File.Delete(savePath);
                    objTrans.Commit();
                    //System.IO.File.Delete(Server.MapPath("VendorDocuments") + "/" + lblUploadedFileName.Text);
                    //objTrans.Commit();
                    pnlMsg.Visible = false;
                    BindAttachedDocuments();
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
    
    private void BindAttachedDocuments()
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
        string StrPath = "~/Transaction/Vendor/VendorDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
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

        //Vendor workflow modification start
        //docType = GetSelectedPkID();
        //Vendor workflow modification end

        if (uploadfile.ContentLength > 0)
        {
            string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

            string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(uploadfile.FileName);
            savePath = savePath + "\\" + uploadedFileName;

            ObjDoc.Document_Upload_Id = 0;
            ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
            ObjDoc.Document_Type = "";
            //Vendor workflow modification start
            //ObjDoc.Document_Type = docType;
            //Vendor workflow modification end
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
            lblMsg.Text = "Error While Saving Vendor Details.";
        }


        return flag;
    }

    //Vendor workflow modification start
    //private void BindMandatoryDocList()
    //{
    //    DataAccessLayer objDb = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    DocumentUploadAccess objDoc = new DocumentUploadAccess();

    //    try
    //    {
    //        dstData = objDoc.GetMandatoryDocList(Convert.ToInt32(Session[StaticKeys.MaterialProcessModuleId].ToString()));
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            grdMandDocs.DataSource = dstData.Tables[0].DefaultView;
    //            grdMandDocs.DataBind();
    //            grdMandDocs.Visible = true;
    //        }
    //        else
    //        {
    //            grdMandDocs.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objDb = null;
    //    }
    //}
    //Vendor workflow modification end
    #endregion
}