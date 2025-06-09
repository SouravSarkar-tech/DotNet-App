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

public partial class Transaction_Customer_CustomerBlock : System.Web.UI.Page
{
    CustomerBlockAccess ObjCustomerBlockAccess = new CustomerBlockAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Event

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
                    FillCustomerBlock();
                    //BindAttachedDocuments();

                    //HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

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
                    ConfigureControl();

                }
                else
                {
                    Response.Redirect("CustomerMaster.aspx");
                }
            }
        }
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
    }

    protected void ddlDistributionChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
    }

    protected void chkAllCompanies_CheckedChanged(object sender, EventArgs e)
    {
        chkSelectedCompany.Checked = false;
    }

    protected void chkSelectedCompany_CheckedChanged(object sender, EventArgs e)
    {
        chkAllCompanies.Checked = false;
    }

    protected void chkAllSalesAreaOrderBlock_CheckedChanged(object sender, EventArgs e)
    {
        chkSelectedSalesAreaOrderBlock.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkSelectedSalesAreaOrderBlock_CheckedChanged(object sender, EventArgs e)
    {
        chkAllSalesAreaOrderBlock.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkAllSalesAreaDeliveryBlock_CheckedChanged(object sender, EventArgs e)
    {
        chkSelectedSalesAreaDeliveryBlock.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkSelectedSalesAreaDeliveryBlock_CheckedChanged(object sender, EventArgs e)
    {
        chkAllSalesAreaDeliveryBlock.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkAllSalesAreaBillingBlock_CheckedChanged(object sender, EventArgs e)
    {
        chkSelectedSalesAreaBillingBlock.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkSelectedSalesAreaBillingBlock_CheckedChanged(object sender, EventArgs e)
    {
        chkAllSalesAreaBillingBlock.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkAllSalesAreaBlockSalesSupport_CheckedChanged(object sender, EventArgs e)
    {
        chkSelectedSalesAreaBlockSalesSupport.Checked = false;
        SetSalesAreaValidation();
    }

    protected void chkSelectedSalesAreaBlockSalesSupport_CheckedChanged(object sender, EventArgs e)
    {
        chkAllSalesAreaBlockSalesSupport.Checked = false;
        SetSalesAreaValidation();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (Save())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("CustomerBlock.aspx");
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one field.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
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

                //if (objDb.DeleteRecord("Customer_Documents", "Document_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                {
                    System.IO.File.Delete(Server.MapPath("CustomerDocuments") + "/" + lblUploadedFileName.Text);
                    objTrans.Commit();
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

    #endregion

    #region Private Method

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();

        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

        if (lblActionType.Text == "B")
        {
            chkAllCompanies.Text = "  Block";
            chkSelectedCompany.Text = "  Block";

            chkAllSalesAreaOrderBlock.Text = "  Block";
            chkSelectedSalesAreaOrderBlock.Text = "  Block";
            chkAllSalesAreaDeliveryBlock.Text = "  Block";
            chkSelectedSalesAreaDeliveryBlock.Text = "  Block";
            chkAllSalesAreaBillingBlock.Text = "  Block";
            chkSelectedSalesAreaBillingBlock.Text = "  Block";
            chkAllSalesAreaBlockSalesSupport.Text = "  Block";
            chkSelectedSalesAreaBlockSalesSupport.Text = "  Block";
        }
        else
        {
            chkAllCompanies.Text = "  UnBlock";
            chkSelectedCompany.Text = "  UnBlock";

            chkAllSalesAreaOrderBlock.Text = "  UnBlock";
            chkSelectedSalesAreaOrderBlock.Text = "  UnBlock";
            chkAllSalesAreaDeliveryBlock.Text = "  UnBlock";
            chkSelectedSalesAreaDeliveryBlock.Text = "  UnBlock";
            chkAllSalesAreaBillingBlock.Text = "  UnBlock";
            chkSelectedSalesAreaBillingBlock.Text = "  UnBlock";
            chkAllSalesAreaBlockSalesSupport.Text = "  UnBlock";
            chkSelectedSalesAreaBlockSalesSupport.Text = "  UnBlock";
        }
    }

    private void SetSalesAreaValidation()
    {
        bool flg = false;

        if (chkSelectedSalesAreaOrderBlock.Checked || chkSelectedSalesAreaDeliveryBlock.Checked || chkSelectedSalesAreaBillingBlock.Checked || chkSelectedSalesAreaBlockSalesSupport.Checked)
        {
            flg = true;
        }

        reqddlSalesOrginization.Visible = flg;
        lableddlSalesOrginization.Visible = flg;

        reqddlDistributionChannel.Visible = flg;
        lableddlDistributionChannel.Visible = flg;

        reqddlDivision.Visible = flg;
        lableddlDivision.Visible = flg;
    }

    private void FillCustomerBlock()
    {
        CustomerBlock ObjCustomerBlock = GetCustomerBlock();
        if (ObjCustomerBlock.Customer_Block_Id > 0)
        {
            lblCustomerBlockId.Text = ObjCustomerBlock.Customer_Block_Id.ToString();
            ddlCompanyCode.SelectedValue = ObjCustomerBlock.Company_Code.ToString();

            ddlCompanyCode.SelectedValue = ObjCustomerBlock.Company_Code;
            lblCustomerDesc.Text = ObjCustomerBlock.Customer_Desc;
            lblCustomerType.Text = ObjCustomerBlock.Customer_Category;

            ddlSalesOrginization.SelectedValue = ObjCustomerBlock.Sales_Organisation_Id;
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

            ddlDistributionChannel.SelectedValue = ObjCustomerBlock.Distribution_Channel_Id;
            helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerBlockId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

            ddlDivision.SelectedValue = ObjCustomerBlock.Division_Id;

            chkAllCompanies.Checked = ObjCustomerBlock.IsAllCompanyBlock.ToString() == "1" ? true : false;
            chkSelectedCompany.Checked = ObjCustomerBlock.IsSelectedCompanyBlock.ToString() == "1" ? true : false;

            chkAllSalesAreaOrderBlock.Checked = ObjCustomerBlock.IsAllSalesAreaOrderBlock.ToString() == "1" ? true : false;
            chkSelectedSalesAreaOrderBlock.Checked = ObjCustomerBlock.IsSelectedSalesAreaOrderBlock.ToString() == "1" ? true : false;
            chkAllSalesAreaDeliveryBlock.Checked = ObjCustomerBlock.IsAllSalesAreaDeliveryBlock.ToString() == "1" ? true : false;
            chkSelectedSalesAreaDeliveryBlock.Checked = ObjCustomerBlock.IsSelectedSalesAreaDeliveryBlock.ToString() == "1" ? true : false;
            chkAllSalesAreaBillingBlock.Checked = ObjCustomerBlock.IsAllSalesAreaBillingBlock.ToString() == "1" ? true : false;
            chkSelectedSalesAreaBillingBlock.Checked = ObjCustomerBlock.IsSelectedSalesAreaBillingBlock.ToString() == "1" ? true : false;
            chkAllSalesAreaBlockSalesSupport.Checked = ObjCustomerBlock.IsAllSalesAreaBlockSalesSupport.ToString() == "1" ? true : false;
            chkSelectedSalesAreaBlockSalesSupport.Checked = ObjCustomerBlock.IsSelectedSalesAreaBlockSalesSupport.ToString() == "1" ? true : false;

            txtRemarks.Text = ObjCustomerBlock.Remarks.ToString();

            Session[StaticKeys.SelectedModulePlantGrp] = ObjCustomerBlock.ModulePlantGroupCode;

            BindAttachedDocuments();
        }
        else
        {
            lblCustomerBlockId.Text = "0";
            ddlCompanyCode.SelectedValue = ObjCustomerBlock.Company_Code.ToString();
            lblCustomerDesc.Text = ObjCustomerBlock.Customer_Desc;
            lblCustomerType.Text = ObjCustomerBlock.Customer_Category;

            Session[StaticKeys.SelectedModulePlantGrp] = ObjCustomerBlock.ModulePlantGroupCode;
        }

    }

    private CustomerBlock GetCustomerBlock()
    {
        return ObjCustomerBlockAccess.GetCustomerBlock(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private bool Save()
    {
        int flg = 0;
        bool boolFlg = false;

        HttpFileCollection fileCollection = Request.Files;
        string fileExtension = string.Empty;
        for (int i = 0; i < fileCollection.Count; i++)
        {
            HttpPostedFile uploadfile = fileCollection[i];
            if (uploadfile.ContentLength > 0)
            {
                fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();
                if ((fileExtension == ".pdf") || (fileExtension == ".xls") || (fileExtension == ".xlsx") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png"))
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

        if (flg == 2)
        {
            lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .Png, .Pdf files allowed.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
        else
        {
            boolFlg = SaveCustomerBlock();
        }
        return boolFlg;
    }

    private bool SaveCustomerBlock()
    {
        bool flg = false;

        CustomerBlock ObjCustomerBlock = GetControlsValue();
        if ((ObjCustomerBlockAccess.Save(ObjCustomerBlock) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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

    private CustomerBlock GetControlsValue()
    {
        CustomerBlock ObjCustomerBlock = new CustomerBlock();
        Utility objUtil = new Utility();

        ObjCustomerBlock.Customer_Block_Id = Convert.ToInt32(lblCustomerBlockId.Text);
        ObjCustomerBlock.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjCustomerBlock.Company_Code = ddlCompanyCode.SelectedValue;
        ObjCustomerBlock.Customer_Code = "";
        ObjCustomerBlock.Customer_Desc = "";
        ObjCustomerBlock.Customer_Acc_Grp = "";
        ObjCustomerBlock.Sales_Organisation_Id = ddlSalesOrginization.SelectedValue;
        ObjCustomerBlock.Distribution_Channel_Id = ddlDistributionChannel.SelectedValue;
        ObjCustomerBlock.Division_Id = ddlDivision.SelectedValue;

        ObjCustomerBlock.IsAllCompanyBlock = chkAllCompanies.Checked ? "1" : "0";
        ObjCustomerBlock.IsSelectedCompanyBlock = chkSelectedCompany.Checked ? "1" : "0";
        ObjCustomerBlock.IsAllSalesAreaOrderBlock = chkAllSalesAreaOrderBlock.Checked ? "1" : "0";
        ObjCustomerBlock.IsSelectedSalesAreaOrderBlock = chkSelectedSalesAreaOrderBlock.Checked ? "1" : "0";
        ObjCustomerBlock.IsAllSalesAreaDeliveryBlock = chkAllSalesAreaDeliveryBlock.Checked ? "1" : "0";
        ObjCustomerBlock.IsSelectedSalesAreaDeliveryBlock = chkSelectedSalesAreaDeliveryBlock.Checked ? "1" : "0";
        ObjCustomerBlock.IsAllSalesAreaBillingBlock = chkAllSalesAreaBillingBlock.Checked ? "1" : "0";
        ObjCustomerBlock.IsSelectedSalesAreaBillingBlock = chkSelectedSalesAreaBillingBlock.Checked ? "1" : "0";
        ObjCustomerBlock.IsAllSalesAreaBlockSalesSupport = chkAllSalesAreaBlockSalesSupport.Checked ? "1" : "0";
        ObjCustomerBlock.IsSelectedSalesAreaBlockSalesSupport = chkSelectedSalesAreaBlockSalesSupport.Checked ? "1" : "0";
        ObjCustomerBlock.Remarks = txtRemarks.Text;

        ObjCustomerBlock.IsActive = "1";
        ObjCustomerBlock.UserId = lblUserId.Text;
        ObjCustomerBlock.TodayDate = objUtil.GetDate();
        ObjCustomerBlock.IPAddress = objUtil.GetIpAddress();

        return ObjCustomerBlock;
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (chkAllCompanies.Checked)
            flag = true;
        else if (chkSelectedCompany.Checked)
            flag = true;
        else if (chkAllSalesAreaOrderBlock.Checked)
            flag = true;
        else if (chkSelectedSalesAreaOrderBlock.Checked)
            flag = true;
        else if (chkAllSalesAreaDeliveryBlock.Checked)
            flag = true;
        else if (chkSelectedSalesAreaDeliveryBlock.Checked)
            flag = true;
        else if (chkAllSalesAreaBillingBlock.Checked)
            flag = true;
        else if (chkSelectedSalesAreaBillingBlock.Checked)
            flag = true;
        else if (chkAllSalesAreaBlockSalesSupport.Checked)
            flag = true;
        else if (chkSelectedSalesAreaBlockSalesSupport.Checked)
            flag = true;

        return flag;
    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Customer_Master_Block_or_Unblock obj = new SectionConfiguration.Customer_Master_Block_or_Unblock();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    #endregion

    #region Document Upload

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

    private bool SaveDocuments(string CustomerId)
    {
        VendorMasterAccess ObjCustomerMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjCustomerMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/Customer/CustomerDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
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

        if (uploadfile.ContentLength > 0)
        {
            string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

            string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(uploadfile.FileName);
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
            lblMsg.Text = "Error While Saving Customer Details.";
        }


        return flag;
    }

    #endregion

}