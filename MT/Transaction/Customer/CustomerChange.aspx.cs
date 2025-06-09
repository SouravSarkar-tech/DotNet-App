using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Accenture.MWT.DomainObject;
using System.Transactions;

public partial class Transaction_Customer_CustomerChange : System.Web.UI.Page
{

    CustomerChangeAccess ObjCustomerChangeAccess = new CustomerChangeAccess();
    HelperAccess helperAccess = new HelperAccess();
    public bool isEditable { get; set; }

    #region Page Methods

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
                    PopuplateDropDownList();

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        lnkAddNew.Visible = true;
                        isEditable = true;
                    }

                    BindCustomerChangeData();
                    BindAttachedDocuments(lblMasterHeaderId.Text);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        //btnSave.Visible = !btnNext.Visible;
                        grdAttachedDocs.Columns[1].Visible = true;
                        file_upload.Visible = true;
                        lnkAddNew.Visible = true;
                        grvCustomerChange.Columns[6].Visible = true;
                    }
                    else
                    {
                        grdAttachedDocs.Columns[1].Visible = false;
                        file_upload.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("CustomerMaster.aspx");
                }
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SaveCustomerChangeData())
        {
            ModalPopupExtender.Hide();

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            BindCustomerChangeData();
        }
        else
        {
            ModalPopupExtender.Show();
        }
    }

    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        if (SaveCustomerChangeData())
        {
            ModalPopupExtender.Hide();

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            BindCustomerChangeData();
        }
        else
        {
            ModalPopupExtender.Show();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton lnkEditValue = (ImageButton)sender;

        lblCustomerChangeDetailId.Text = lnkEditValue.CommandArgument;
        FillCustomerChangeDetailData();
        lblCustomerChangeAction.Text = "E";
        ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = (ImageButton)sender;

        ObjCustomerChangeAccess.DeleteCustomerChangeDetail(btnDelete.CommandArgument);
        BindCustomerChangeData();
    }

    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        ImageButton lnkAddValue = (ImageButton)sender;
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        lblCustomerChangeDetailId.Text = "0";
        lblCustomerChangeAction.Text = "F";
        lblCustomerChange.Text = lnkAddValue.CommandArgument;
        FillCustomerChangeDetailData();
        ModalPopupExtender.Show();

    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        lblCustomerChange.Text = "0";
        lblCustomerChangeAction.Text = "C";
        lblCustomerChangeDetailId.Text = "0";
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        FillCustomerChangeData();

        ModalPopupExtender.Show();
    }

    protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
    {
        txtCustomerCode.Text = txtCustomerCode.Text.ToUpper();
        string str = txtCustomerCode.Text.Substring(0, 1).ToUpper();
        //string str1 = txtCustomerCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtCustomerCode.Text);

        switch (str)
        {
            case "L":
                regtxtCustomerCode.ValidationExpression = "^[\\S]{4}$";
                ddlCustAccGrp.SelectedValue = "88";

                break;
            default:
                regtxtCustomerCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 199999) //Z001
                    ddlCustAccGrp.SelectedValue = "84";
                else if (strcode >= 200000 && strcode < 299999)//Z002
                    ddlCustAccGrp.SelectedValue = "85";
                else if (strcode >= 300000 && strcode < 399999)//Z003
                    ddlCustAccGrp.SelectedValue = "86";
                else if (strcode >= 400000 && strcode < 499999)//Z004
                    ddlCustAccGrp.SelectedValue = "87";
                else if (strcode >= 500000 && strcode < 599999)//Z006 500000-599999
                    ddlCustAccGrp.SelectedValue = "89";
                else if (strcode >= 700000 && strcode < 799999)//Z008 700000-799999
                    ddlCustAccGrp.SelectedValue = "91";
                break;
        }
        helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlCustAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        ModalPopupExtender.Show();
        txtCustomerName.Focus();
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        //if (CheckIsValid())
        //{
        //    if (Save())
        //    {
        //        string pageURL = btnPrevious.CommandArgument.ToString();
        //        Response.Redirect(pageURL);
        //    }
        //}
        //else
        //{
        //    lblMsg.Text = "Please fill atleast one feild.";
        //    pnlMsg.Visible = true;
        //    pnlMsg.CssClass = "error";
        //}
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
                BindAttachedDocuments(lblMasterHeaderId.Text);

                Response.Redirect("CustomerChange.aspx");
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one feild.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        //if (CheckIsValid())
        //{
        //    if (Save())
        //    {
        //        string pageURL = btnNext.CommandArgument.ToString();
        //        Response.Redirect(pageURL);
        //    }
        //}
        //else
        //{
        //    lblMsg.Text = "Please fill atleast one feild.";
        //    pnlMsg.Visible = true;
        //    pnlMsg.CssClass = "error";
        //}
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        ModalPopupExtender.Show();
    }

    protected void ddlDistributionChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
        ModalPopupExtender.Show();
        ddlDivision.Focus();
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

                //if (objDb.DeleteRecord("Vendor_Documents", "Document_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                {
                    //System.IO.File.Delete(Server.MapPath("VendorDocuments") + "/" + lblUploadedFileName.Text);
                    System.IO.File.Delete(Server.MapPath("CustomerDocuments") + "/" + lblUploadedFileName.Text);
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

    protected void ddlCustAccGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlCustAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        ModalPopupExtender.Show();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        ModalPopupExtender.Show();
        ddlField.Focus();
    }

    protected void grvCustomerChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCustomerChangeId = (Label)e.Row.FindControl("lblCustomerChangeId");

            GridView grvCustomerChangeDtl = (GridView)e.Row.FindControl("grvCustomerChangeDtl");
            bindgrvCustomerChangeDtl(Convert.ToInt32(lblCustomerChangeId.Text), grvCustomerChangeDtl); //Bind the child gridvie here ..
            grvCustomerChangeDtl.Columns[5].Visible = lnkAddNew.Visible;
        }
    }

    #endregion


    #region Public Methods

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");

        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerChangeId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "','" + ddlDistributionChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");

        helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlCustAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        DataSet ds;
        ds = objMatAccess.ReadModules("C");


        ddlCustAccGrp.DataSource = ds;
        ddlCustAccGrp.DataTextField = "Module_Name";
        ddlCustAccGrp.DataValueField = "Module_Id";
        ddlCustAccGrp.DataBind();

        ddlCustAccGrp.Items.Insert(0, new ListItem("---Select---", "0"));

    }

    protected bool SaveCustomerChangeData()
    {
        bool Flag = false;
        CustomerChange ObjCustomerChange = GetControlsValue();

        try
        {
            if (!CheckDuplicateVendor())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ObjCustomerChangeAccess.Save(ObjCustomerChange) > 0)
                    {
                        scope.Complete();
                        Flag = true;
                    }
                    else
                    {
                        lblMsg1.Text = Messages.GetMessage(-1);
                        pnlMsg1.CssClass = "error";
                        pnlMsg1.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private bool CheckDuplicateVendor()
    {
        bool flg = false;

        if (lblCustomerChangeAction.Text == "F")
        {
            foreach (GridViewRow gr in grvCustomerChange.Rows)
            {
                Label lblSalesOrgId = (Label)gr.Cells[0].FindControl("lblSalesOrgId");
                Label lblDistChnlId = (Label)gr.Cells[0].FindControl("lblDistChnlId");
                Label lblDivisionId = (Label)gr.Cells[0].FindControl("lblDivisionId");

                if (txtCustomerCode.Text == gr.Cells[1].Text && ddlSalesOrginization.SelectedValue == lblSalesOrgId.Text && ddlDistributionChannel.SelectedValue == lblDistChnlId.Text && ddlDivision.SelectedValue == lblDivisionId.Text)
                {
                    //if (txtCustomerCode.Text == gr.Cells[1].Text)
                    //{
                    GridView grvCustomerChangeDtl = (GridView)gr.FindControl("grvCustomerChangeDtl");
                    foreach (GridViewRow gr1 in grvCustomerChangeDtl.Rows)
                    {
                        Label lblSectionFeildMasterId = (Label)gr1.FindControl("lblSectionFeildMasterId");

                        if (ddlField.SelectedValue == lblSectionFeildMasterId.Text)
                        {
                            flg = true;
                        }
                    }
                }
            }

            if (flg)
            {
                lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                pnlMsg1.CssClass = "error";
                pnlMsg1.Visible = true;
            }
        }
        else if (lblCustomerChangeAction.Text == "C")
        {
            foreach (GridViewRow gr in grvCustomerChange.Rows)
            {
                Label lblSalesOrgId = (Label)gr.Cells[0].FindControl("lblSalesOrgId");
                Label lblDistChnlId = (Label)gr.Cells[0].FindControl("lblDistChnlId");
                Label lblDivisionId = (Label)gr.Cells[0].FindControl("lblDivisionId");

                if (txtCustomerCode.Text == gr.Cells[1].Text && ddlSalesOrginization.SelectedValue == lblSalesOrgId.Text && ddlDistributionChannel.SelectedValue == lblDistChnlId.Text && ddlDivision.SelectedValue == lblDivisionId.Text)
                {
                    flg = true;
                }
            }
            if (flg)
            {
                lblMsg1.Text = "Duplicate Customer. To enter more fields for the same customer, Click the '+' in front of the customer.";
                pnlMsg1.CssClass = "error";
                pnlMsg1.Visible = true;
            }
        }
        else if (txtOldValue.Text == "" && txtNewValue.Text == "")
        {
            lblMsg1.Text = "Both Old value and New Value cannot be blank.";
            pnlMsg1.CssClass = "error";
            pnlMsg1.Visible = true;
            flg = true;
        }
        else if (txtOldValue.Text == txtNewValue.Text)
        {
            lblMsg1.Text = "Old value and New Value cannot be same.";
            pnlMsg1.CssClass = "error";
            pnlMsg1.Visible = true;
            flg = true;
        }

        return flg;
    }

    private void BindCustomerChangeData()
    {
        DataSet ds = ObjCustomerChangeAccess.GetCustomerChangeData(lblMasterHeaderId.Text);

        grvCustomerChange.DataSource = ds.Tables[0];
        grvCustomerChange.DataBind();

        if (ds.Tables[1].Rows.Count > 0)
            lblCustomerType.Text = ds.Tables[1].Rows[0]["Customer_Category"].ToString();
    }

    private void bindgrvCustomerChangeDtl(int CustomerChangeId, GridView grvCustomerChangeDtl)
    {
        grvCustomerChangeDtl.DataSource = ObjCustomerChangeAccess.GetCustomerChangeDetailData(CustomerChangeId);
        grvCustomerChangeDtl.DataBind();
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

        if (flg == 2)
        {
            lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
        else
        {
            boolFlg = SaveCustomerChange();
        }
        return boolFlg;
    }

    private bool SaveCustomerChange()
    {
        bool Flag = false;

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (SaveDocuments(lblMasterHeaderId.Text))
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
            throw ex;
        }
        return Flag;
    }

    private CustomerChange GetCustomerChange()
    {
        return ObjCustomerChangeAccess.GetCustomerChange(Convert.ToInt32(lblCustomerChange.Text));
    }

    private CustomerChangeDetail GetCustomerChangeDetail()
    {
        return ObjCustomerChangeAccess.GetCustomerChangeDetail(Convert.ToInt32(lblCustomerChangeDetailId.Text));
    }

    private CustomerChange GetControlsValue()
    {
        CustomerChange ObjCustomerChange = new CustomerChange();
        Utility objUtil = new Utility();

        ObjCustomerChange.Customer_Change_Id = Convert.ToInt32(lblCustomerChange.Text);
        ObjCustomerChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjCustomerChange.Customer_Code = txtCustomerCode.Text;
        ObjCustomerChange.Company_Code = ddlCompanyCode.SelectedValue;
        ObjCustomerChange.Customer_Desc = txtCustomerName.Text;
        ObjCustomerChange.Customer_Acc_Grp = ddlCustAccGrp.SelectedValue;
        ObjCustomerChange.Sales_Organisation_Id = ddlSalesOrginization.SelectedValue;
        ObjCustomerChange.Distribution_Channel_Id = ddlDistributionChannel.SelectedValue;
        ObjCustomerChange.Division_Id = ddlDivision.SelectedValue;

        ObjCustomerChange.Customer_Change_Detail_Id = Convert.ToInt32(lblCustomerChangeDetailId.Text);
        ObjCustomerChange.Section_Id = Convert.ToInt32(ddlSection.SelectedValue);
        ObjCustomerChange.Section_Feild_Master_Id = Convert.ToInt32(ddlField.SelectedValue);
        ObjCustomerChange.Old_Value = txtOldValue.Text;
        ObjCustomerChange.New_Value = txtNewValue.Text;

        ObjCustomerChange.IsActive = 1;
        ObjCustomerChange.UserId = lblUserId.Text;
        ObjCustomerChange.TodayDate = objUtil.GetDate();
        ObjCustomerChange.IPAddress = objUtil.GetIpAddress();

        return ObjCustomerChange;
    }

    private void FillCustomerChangeData()
    {
        CustomerChange ObjCustomerChange = GetCustomerChange();
        if (ObjCustomerChange.Customer_Change_Id > 0)
        {
            lblCustomerChange.Text = ObjCustomerChange.Customer_Change_Id.ToString();
            txtCustomerCode.Text = ObjCustomerChange.Customer_Code;
            ddlCompanyCode.SelectedValue = ObjCustomerChange.Company_Code;
            ddlCustAccGrp.SelectedValue = ObjCustomerChange.Customer_Acc_Grp;
            ddlSalesOrginization.SelectedValue = ObjCustomerChange.Sales_Organisation_Id;
            ddlDistributionChannel.SelectedValue = ObjCustomerChange.Distribution_Channel_Id;
            ddlDivision.SelectedValue = ObjCustomerChange.Division_Id;
            txtCustomerName.Text = ObjCustomerChange.Customer_Desc;
        }
        else
        {
            ddlCompanyCode.SelectedValue = "32";
            ddlCompanyCode.Enabled = false;
            txtCustomerCode.Text = "";
            ddlCustAccGrp.SelectedValue = "0";

            lblModuleId.Text = "0";
            txtCustomerName.Text = "";
            ddlSalesOrginization.SelectedValue = "";
            ddlDistributionChannel.SelectedValue = "";
            ddlDivision.SelectedValue = "";

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlCustAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

            txtOldValue.Text = "";
            txtNewValue.Text = "";
        }
        MakeDisable(true);
    }

    private void FillCustomerChangeDetailData()
    {
        CustomerChangeDetail ObjCustomerChangeDetail = GetCustomerChangeDetail();

        if (ObjCustomerChangeDetail.Customer_Change_Id > 0)
        {
            lblCustomerChange.Text = ObjCustomerChangeDetail.Customer_Change_Id.ToString();

            CustomerChange objCustomerChange = ObjCustomerChangeAccess.GetCustomerChange(Convert.ToInt32(lblCustomerChange.Text));
            txtCustomerCode.Text = objCustomerChange.Customer_Code;
            lblModuleId.Text = objCustomerChange.Customer_Acc_Grp;
            txtCustomerName.Text = objCustomerChange.Customer_Desc;
            ddlCompanyCode.SelectedValue = objCustomerChange.Company_Code;
            ddlCustAccGrp.SelectedValue = objCustomerChange.Customer_Acc_Grp;
            ddlSalesOrginization.SelectedValue = objCustomerChange.Sales_Organisation_Id;
            ddlDistributionChannel.SelectedValue = objCustomerChange.Distribution_Channel_Id;
            ddlDivision.SelectedValue = objCustomerChange.Division_Id;

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlCustAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
            ddlSection.SelectedValue = ObjCustomerChangeDetail.Section_Id.ToString();
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ObjCustomerChangeDetail.Section_Id + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            ddlField.SelectedValue = ObjCustomerChangeDetail.Section_Feild_Master_Id.ToString();

            txtOldValue.Text = ObjCustomerChangeDetail.Old_Value;
            txtNewValue.Text = ObjCustomerChangeDetail.New_Value;

            MakeDisable(false);
        }
        else
        {
            CustomerChange objCustomerChange = ObjCustomerChangeAccess.GetCustomerChange(Convert.ToInt32(lblCustomerChange.Text));
            txtCustomerCode.Text = objCustomerChange.Customer_Code;
            lblModuleId.Text = objCustomerChange.Customer_Acc_Grp;
            txtCustomerName.Text = objCustomerChange.Customer_Desc;
            ddlCompanyCode.SelectedValue = objCustomerChange.Company_Code;
            ddlCustAccGrp.SelectedValue = objCustomerChange.Customer_Acc_Grp;
            ddlSalesOrginization.SelectedValue = objCustomerChange.Sales_Organisation_Id;
            ddlDistributionChannel.SelectedValue = objCustomerChange.Distribution_Channel_Id;
            ddlDivision.SelectedValue = objCustomerChange.Division_Id;

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlCustAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlCustAccGrp.SelectedValue + "','" + ObjCustomerChangeDetail.Section_Id + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

            txtOldValue.Text = "";
            txtNewValue.Text = "";
            MakeDisable(false);
        }

    }

    private void MakeDisable(bool flg)
    {
        txtCustomerCode.Enabled = flg;
        txtCustomerName.Enabled = flg;
        ddlCompanyCode.Enabled = flg;
        //ddlCustAccGrp.Enabled = flg;
        ddlSalesOrginization.Enabled = flg;
        ddlDistributionChannel.Enabled = flg;
        ddlDivision.Enabled = flg;
    }

    private bool CheckIsValid()
    {
        bool flg = false;

        if (grvCustomerChange.Rows.Count > 0)
            flg = true;

        return flg;
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