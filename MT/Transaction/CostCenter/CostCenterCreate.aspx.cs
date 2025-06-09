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
public partial class Transaction_CostCenter_CostCenterCreate : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    CostCenterMasterAccess ObjCostCenterMasterAccess = new CostCenterMasterAccess();
    HelperAccess helperAccess = new HelperAccess();

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
                        ConfigureControl();
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            txtCostCenter.Enabled = true;
                            txtRefCostCenter.Enabled = false;
                            txtValidFrom.Enabled = false;
                            txtValidTo.Enabled = false;
                            txtCCName.Enabled = false;
                            txtCCDesc.Enabled = false;
                            txtUserResp.Enabled = false;
                            txtPersonResp.Enabled = false;
                            txtDepartment.Enabled = false;
                            ddlCCCat.Enabled = false;
                            ddlHierarchyArea.Enabled = false;
                            ddlBusinessArea.Enabled = false;
                            ddlProfitCenter.Enabled = false;
                        }


                        //SDT17052019 Commented By NR  
                        //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "44")
                        //EDT17052019 Commented By NR  
                        //SDT17052019 Change By NR , Desc : Get Department ID from web config
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["DEPSME"]))
                        {
                            txtCostCenter.Enabled = true;
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

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save() == 1)
                {
                    if (SaveCostCenterMaster())
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (CheckIsValid())
            {
                string Module = Session[StaticKeys.SelectedModuleId].ToString();

                if (txtCostCenter.Text == "" && txtRefCostCenter.Text == "" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                {
                    string message = "alert('Please enter either cost center or reference cost center')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }

                else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && (txtCostCenter.Text == ""))
                {
                    string message = "alert('Please enter Cost Center.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }

                else
                {
                    if (Save() == 1)
                    {
                        if (SaveCostCenterMaster())
                        {
                            lblMsg.Text = Messages.GetMessage(1);
                            pnlMsg.CssClass = "success";
                            pnlMsg.Visible = true;
                            BindAttachedDocuments(lblMasterHeaderId.Text);
                            Response.Redirect("CostCenterCreate.aspx");
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
                        pnlMsg.Visible = true;
                        pnlMsg.CssClass = "error";
                    }
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

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save() == 1)
                {
                    if (SaveCostCenterMaster())
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
                        System.IO.File.Delete(Server.MapPath("MaterialDocuments") + "/" + lblUploadedFileName.Text);
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

    private void PopuplateDropDownList()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id", "");
           //Carve_LC17&LC23 add
		    if (Session[StaticKeys.SelectedddlCompany].ToString() != null && Session[StaticKeys.SelectedddlCompany].ToString() != "")
            { 
                ddlCompanyCode.SelectedValue = Session[StaticKeys.SelectedddlCompany].ToString();
            }
            else
            {
                ddlCompanyCode.SelectedValue = "32";
            } 
			//Carve_LC17&LC23 add 
            helperAccess.PopuplateDropDownList(ddlControllingArea, "pr_GetControllingArea", "Name", "ID", "");
            helperAccess.PopuplateDropDownList(ddlCCCat, "pr_GetCostCenterCategory", "Name", "ID", "");
            helperAccess.PopuplateDropDownList(ddlHierarchyArea, "pr_GetHierarchyArea", "Name", "ID", "");
            helperAccess.PopuplateDropDownList(ddlBusinessArea, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
            //helperAccess.PopuplateDropDownList(ddlAccGroup, "pr_GetCostCenterAccGrpList 0", "Module_Name", "Module_Id");
           //Carve_LC17&LC23 add
		    //ddlCompanyCode.SelectedValue = "32";
            if (Session[StaticKeys.MaterialPlantId].ToString() != null && Session[StaticKeys.MaterialPlantId].ToString() != "")
            {
                ddlBusinessArea.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
            }
            else
            {
                ddlBusinessArea.Enabled = true;
            }
			//Carve_LC17&LC23 add
            ddlControllingArea.SelectedValue = "19";
            //helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByCompanyId 'ddlProfitCenter','" + lblSectionId.Text + "','" + ddlCompanyCode.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','" + "13" + "','" + ddlBusinessArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void FillGeneralData()
    {
        try
        {
            CostCenterCreate ObjCostCenterMaster = GetCostCenterMasterData();
            if (ObjCostCenterMaster.ID > 0)
            {
                lblMasterHeaderId.Text = ObjCostCenterMaster.Master_Header_Id.ToString();
                ddlCompanyCode.SelectedValue = ObjCostCenterMaster.Company_Code.ToString();
                txtCostCenter.Text = ObjCostCenterMaster.Cost_Center.ToString();
                txtRefCostCenter.Text = ObjCostCenterMaster.Ref_Cost_Center.ToString();
                txtValidFrom.Text = "01/04/2017";// ObjCostCenterMaster.ValidFrom.ToString();
                txtValidTo.Text = "31/12/9999";// ObjCostCenterMaster.ValidTo.ToString();
                ddlControllingArea.SelectedValue = "19";// ObjCostCenterMaster.ControllingArea.ToString();
                txtCCName.Text = ObjCostCenterMaster.Cost_Center_Name.ToString();
                txtCCDesc.Text = ObjCostCenterMaster.Cost_Center_Desc.ToString();
                txtUserResp.Text = ObjCostCenterMaster.User_Responsible.ToString();
                txtPersonResp.Text = ObjCostCenterMaster.Person_Responsible.ToString();
                txtDepartment.Text = ObjCostCenterMaster.Department;
                ddlCCCat.SelectedValue = ObjCostCenterMaster.Cost_Center_Category;
                ddlHierarchyArea.SelectedValue = ObjCostCenterMaster.Hierarchy_Area;
                //Carve_LC17&LC23 comment
                //ddlCompanyCode.SelectedValue = "32";// ObjCostCenterMaster.Company_Code;
                //Carve_LC17&LC23 comment

                //Carve_LC17&LC23 add
                ddlCompanyCode.SelectedValue =  ObjCostCenterMaster.Company_Code;
                //Carve_LC17&LC23 add
                ddlBusinessArea.SelectedValue = ObjCostCenterMaster.Business_Area;
                helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','" + "13" + "','" + ddlBusinessArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlProfitCenter.SelectedValue = ObjCostCenterMaster.Profit_Center;
                BindAttachedDocuments(lblMasterHeaderId.Text);
            }
            else
            {
                Session[StaticKeys.SelectedModulePlantGrp] = ObjCostCenterMaster.ModulePlantGroupCode;
            }
        }
        catch (Exception ex)
        { _log.Error("FillGeneralData", ex); }
    }

    private void ConfigureControl()
    {
        try
        {
            ddlControllingArea.Enabled = false;
            //INCID367612 
            //ddlCompanyCode.Enabled = false;
            //INCID367612 
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private bool CheckIsValid()
    {
        bool flag = false;
        try
        {
            if (lblActionType.Text != "C")
                flag = true;
            else
            {
                if (txtCCName.Text != "")
                    flag = true;
                else if (txtCCDesc.Text != "")
                    flag = true;
                else if (txtPersonResp.Text != "")
                    flag = true;
                else if (ddlCCCat.SelectedValue != "0")
                    flag = true;
                else if (ddlHierarchyArea.SelectedValue != "0")
                    flag = true;
                else if (ddlBusinessArea.SelectedValue != "0")
                    flag = true;
                else if (ddlProfitCenter.SelectedValue != "0")
                    flag = true;

            }

        }
        catch (Exception ex)
        { _log.Error("CheckIsValid", ex); }

        return flag;
    }

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

    private bool SaveCostCenterMaster()
    {
        bool Flag = false;
        CostCenterCreate ObjCostCenterMaster = GetControlsValue();

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if ((ObjCostCenterMasterAccess.Save(ObjCostCenterMaster) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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
            //throw ex;
            _log.Error("SaveCostCenterMaster", ex);
        }
        return Flag;
    }

    private CostCenterCreate GetCostCenterMasterData()
    {
        return ObjCostCenterMasterAccess.GetCostCenterMasterData(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private CostCenterCreate GetControlsValue()
    {
        CostCenterCreate ObjCostCenterMaster = new CostCenterCreate();
        Utility objUtil = new Utility();
        try
        {
            ObjCostCenterMaster.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjCostCenterMaster.Cost_Center = txtCostCenter.Text;
            ObjCostCenterMaster.Ref_Cost_Center = txtRefCostCenter.Text;
            ObjCostCenterMaster.ValidFrom = txtValidFrom.Text;
            ObjCostCenterMaster.ValidTo = txtValidTo.Text;
            ObjCostCenterMaster.ControllingArea = ddlControllingArea.SelectedValue;
            ObjCostCenterMaster.Cost_Center_Name = txtCCName.Text;
            ObjCostCenterMaster.Cost_Center_Desc = txtCCDesc.Text;
            ObjCostCenterMaster.User_Responsible = txtUserResp.Text;
            ObjCostCenterMaster.Person_Responsible = txtPersonResp.Text;
            ObjCostCenterMaster.Department = txtDepartment.Text;
            ObjCostCenterMaster.Cost_Center_Category = ddlCCCat.SelectedValue;
            ObjCostCenterMaster.Hierarchy_Area = ddlHierarchyArea.SelectedValue;
            ObjCostCenterMaster.Company_Code = ddlCompanyCode.SelectedValue;
            ObjCostCenterMaster.Business_Area = ddlBusinessArea.SelectedValue;
            ObjCostCenterMaster.Profit_Center = ddlProfitCenter.SelectedValue;
            ObjCostCenterMaster.UserId = lblUserId.Text;
            ObjCostCenterMaster.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjCostCenterMaster;
    }

    #region Doc Upload
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


    protected void ddlCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByCompanyId 'ddlProfitCenter','" + lblSectionId.Text + "','" + ddlCompanyCode.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlCompanyCode_SelectedIndexChanged", ex); }
    }

    protected void ddlBusinessArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','" + "13" + "','" + ddlBusinessArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlBusinessArea_SelectedIndexChanged", ex); }
    }
}