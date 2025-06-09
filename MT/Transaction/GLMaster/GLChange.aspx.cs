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
using System.Configuration;

public partial class Transaction_GLMaster_GLChange : System.Web.UI.Page
{
    GLChangeAccess ObjGLChangeAccess = new GLChangeAccess();
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

                    BindGLChangeData();
                    BindAttachedDocuments(lblMasterHeaderId.Text);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        grdAttachedDocs.Columns[1].Visible = true;
                        file_upload.Visible = true;
                        lnkAddNew.Visible = true;
                        grvGLChange.Columns[5].Visible = true;
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool flag = false;
        string message = "";
        lblMsg1.Text = "";
        if ((ddlField.SelectedValue != "0") && (txtOldValue.Text != "") && (txtNewValue.Text != ""))
        {
            flag = true;
        }
        if (ddlField2.SelectedValue != "0")
        {
            if (txtOldValue2.Text == "")
            {
                message = "alert('Please enter Old Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else if (txtNewValue2.Text == "")
            {
                message = "alert('Please enter New Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                flag = true;
            }
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if (ddlField3.SelectedValue != "0")
        {
            if (txtOldValue3.Text == "")
            {
                message = "alert('Please enter Old Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
            }
            else if (txtNewValue3.Text == "")
            {
                message = "alert('Please enter New Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
            }
            else
            {
                flag = true;
            }
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if (ddlField4.SelectedValue != "0")
        {
            if (txtOldValue4.Text == "")
            {
                message = "alert('Please enter Old Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
            }
            else if (txtNewValue4.Text == "")
            {
                message = "alert('Please enter New Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
            }
            else
            {
                flag = true;
            }
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if (ddlField5.SelectedValue != "0")
        {
            if (txtOldValue5.Text == "")
            {
                message = "alert('Please enter Old Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
            }
            else if (txtNewValue5.Text == "")
            {
                message = "alert('Please enter New Value')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
            }
            else
            {
                flag = true;
            }
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if ((txtOldValue.Text != "" || txtNewValue.Text != "") && (ddlField.SelectedValue == "0"))
        {
            message = "alert('Please select Field')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            flag = false;
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if ((txtOldValue2.Text != "" || txtNewValue2.Text != "") && (ddlField2.SelectedValue == "0"))
        {
            message = "alert('Please select Field')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            flag = false;
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if ((txtOldValue3.Text != "" || txtNewValue3.Text != "") && (ddlField3.SelectedValue == "0"))
        {
            message = "alert('Please select Field')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            flag = false;
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if ((txtOldValue4.Text != "" || txtNewValue4.Text != "") && (ddlField4.SelectedValue == "0"))
        {
            message = "alert('Please select Field')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            flag = false;
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if ((txtOldValue5.Text != "" || txtNewValue5.Text != "") && (ddlField5.SelectedValue == "0"))
        {
            message = "alert('Please select Field')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            flag = false;
            if (!CheckDuplicateGL())
            {
                flag = true;
            }
            else
            { flag = false; }
            ModalPopupExtender.Show();
        }
        if (message.Length > 0)
        {
            flag = false;
        }

        if (flag)
        {
            lblMsg1.Text = "";
            if (SaveGLChangeData())
            {
                ModalPopupExtender.Hide();

                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                BindGLChangeData();
            }
            else
            {
                ModalPopupExtender.Show();
            }
        }
    }

    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        if (SaveGLChangeData())
        {
            ModalPopupExtender.Hide();

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            BindGLChangeData();
        }
        else
        {
            ModalPopupExtender.Show();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton lnkEditValue = (ImageButton)sender;

        lblGLChangeDetailId.Text = lnkEditValue.CommandArgument;
        FillGLChangeDetailData();
        lblGLChangeAction.Text = "E";
        ModalPopupExtender.Show();
        ddlField2.Enabled = false;
        ddlField3.Enabled = false;
        ddlField4.Enabled = false;
        ddlField5.Enabled = false;
        txtOldValue2.Enabled = false;
        txtOldValue3.Enabled = false;
        txtOldValue4.Enabled = false;
        txtOldValue5.Enabled = false;
        txtNewValue2.Enabled = false;
        txtNewValue3.Enabled = false;
        txtNewValue4.Enabled = false;
        txtNewValue5.Enabled = false;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = (ImageButton)sender;

        ObjGLChangeAccess.DeleteGLChangeDetail(btnDelete.CommandArgument);
        BindGLChangeData();
    }

    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        ImageButton lnkAddValue = (ImageButton)sender;
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        lblGLChangeDetailId.Text = "0";
        lblGLChangeAction.Text = "F";
        lblGLChange.Text = lnkAddValue.CommandArgument;
        FillGLChangeDetailData();
        ModalPopupExtender.Show();
        ddlField2.Enabled = true;
        ddlField3.Enabled = true;
        ddlField4.Enabled = true;
        ddlField5.Enabled = true;
        txtOldValue2.Enabled = true;
        txtOldValue3.Enabled = true;
        txtOldValue4.Enabled = true;
        txtOldValue5.Enabled = true;
        txtNewValue2.Enabled = true;
        txtNewValue3.Enabled = true;
        txtNewValue4.Enabled = true;
        txtNewValue5.Enabled = true;

    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        lblGLChange.Text = "0";
        lblGLChangeAction.Text = "G";
        lblGLChangeDetailId.Text = "0";
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        FillGLChangeData();

        ModalPopupExtender.Show();
        ddlField2.Enabled = true;
        ddlField3.Enabled = true;
        ddlField4.Enabled = true;
        ddlField5.Enabled = true;
        txtOldValue2.Enabled = true;
        txtOldValue3.Enabled = true;
        txtOldValue4.Enabled = true;
        txtOldValue5.Enabled = true;
        txtNewValue2.Enabled = true;
        txtNewValue3.Enabled = true;
        txtNewValue4.Enabled = true;
        txtNewValue5.Enabled = true;
    }

    protected void txtGLCode_TextChanged(object sender, EventArgs e)
    {
        txtGLCode.Text = txtGLCode.Text.ToUpper();
        string str = txtGLCode.Text.Substring(0, 1).ToUpper();
        string str1 = txtGLCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtGLCode.Text);
        //SDT17052019 Change By NR , Desc : Get page path  from web config
        if (strcode >= 231001 && strcode < 299999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleASET"]); // "198";
        else if (strcode >= 140000 && strcode < 145999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleBNKL"]); // "199";
        else if (strcode >= 221000 && strcode < 230999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCASH"]); // "201";
        else if (strcode >= 400000 && strcode < 410999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleCONS"]); // "203";
        else if (strcode >= 199001 && strcode < 199999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleDEPN"]); // "204";
        else if (strcode >= 411000 && strcode < 499999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleEXPN"]); // "205";
        else if (strcode >= 200000 && strcode < 204999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleFXAS"]); // "209";
        else if (strcode >= 100000 && strcode < 139999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleLIAB"]); // "210";
        else if (strcode >= 205000 && strcode < 209999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleMATL"]); // "211";
        else if (strcode >= 900000 && strcode < 999999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleMISC"]); // "212";
        else if (strcode >= 146000 && strcode < 150999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModulePABL"]); // "213";
        else if (strcode >= 210000 && strcode < 220999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleRCBL"]); //"214";
        else if (strcode >= 300000 && strcode < 399999)
            ddlGLAccGrp.SelectedValue = Convert.ToString(ConfigurationManager.AppSettings["ModuleREVN"]); //"215";
         //EDT17052019 Change By NR , Desc : Get page path  from web config
        else
        {
            string message = "alert('Please enter valid GLCode')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        //SDT17052019 Change By NR , Desc : Get Module ID from web config  
        int iSECGLN = Convert.ToInt32(ConfigurationManager.AppSettings["SECGLN"]);
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //SDT17052019 Change By NR , Desc : Get Module ID from web config

        //SDT17052019 Commented By NR 
        //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //EDT17052019 Commented By NR 

        ModalPopupExtender.Show();
        txtGLName.Focus();
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (Save())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one field.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
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

                Response.Redirect("GLChange.aspx");
            }
        }
        else
        {
            lblMsg.Text = "Please fill atleast one field.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {

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

    protected void ddlGLAccGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SDT17052019 Change By NR , Desc : Get Module ID from web config  
        int iSECGLN = Convert.ToInt32(ConfigurationManager.AppSettings["SECGLN"]);
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //SDT17052019 Change By NR , Desc : Get Module ID from web config

        //SDT17052019 Commented By NR 
        //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //EDT17052019 Commented By NR 
        ModalPopupExtender.Show();
    }

    protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
    {

        ModalPopupExtender.Show();
    }

    protected void grvGLChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGLChangeId = (Label)e.Row.FindControl("lblGLChangeId");

            GridView grvGLChangeDtl = (GridView)e.Row.FindControl("grvGLChangeDtl");
            bindgrvGLChangeDtl(Convert.ToInt32(lblGLChangeId.Text), grvGLChangeDtl);
            grvGLChangeDtl.Columns[5].Visible = lnkAddNew.Visible;
        }
    }

    protected void ddlField2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = null;
            string[] strArr = null;
            str = Convert.ToString(ddlField2.SelectedValue);
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue2.MaxLength = inputlength;
            txtNewValue2.MaxLength = inputlength;
        }
        catch (Exception ex)
        {

        }
        ModalPopupExtender.Show();
    }
    protected void ddlField3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = null;
            string[] strArr = null;
            str = Convert.ToString(ddlField3.SelectedValue);
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {

        }
        ModalPopupExtender.Show();
    }
    protected void ddlField4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = null;
            string[] strArr = null;
            str = Convert.ToString(ddlField4.SelectedValue);
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {

        }
        ModalPopupExtender.Show();
    }
    protected void ddlField5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = null;
            string[] strArr = null;
            str = Convert.ToString(ddlField5.SelectedValue);
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {

        }
        ModalPopupExtender.Show();
    }

    #endregion

    #region Public Methods


    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        //SDT17052019 Change By NR , Desc : Get Module ID from web config  
        int iSECGLN = Convert.ToInt32(ConfigurationManager.AppSettings["SECGLN"]);
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //SDT17052019 Change By NR , Desc : Get Module ID from web config

        //SDT17052019 Commented By NR 
        //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        //EDT17052019 Commented By NR 

        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        DataSet ds;
        ds = objMatAccess.ReadModules("G");


        ddlGLAccGrp.DataSource = ds;
        ddlGLAccGrp.DataTextField = "Module_Name";
        ddlGLAccGrp.DataValueField = "Module_Id";
        ddlGLAccGrp.DataBind();

        ddlGLAccGrp.Items.Insert(0, new ListItem("---Select---", "0"));

    }

    protected bool SaveGLChangeData1()
    {
        bool Flag = false;
        GLChange ObjGLChange = GetControlsValue(Convert.ToString(ddlField.SelectedValue), txtNewValue.Text, txtOldValue.Text);

        try
        {
            if (!CheckDuplicateGL())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ObjGLChangeAccess.Save(ObjGLChange) > 0)
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
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    protected bool SaveGLChangeData()
    {
        int inputvalue = 0;
        string str = null;
        string[] strArr = null;

        bool Flag = false;
        if (ddlField.SelectedValue != "0")
        {

            try
            {

                str = Convert.ToString(ddlField.SelectedValue);
                char[] splitchar = { '-' };
                strArr = str.Split(splitchar);
                inputvalue = Convert.ToInt32(strArr[0]);

            }
            catch (Exception ex)
            {

            }

            GLChange ObjGLChange = GetControlsValue(Convert.ToString(inputvalue), txtNewValue.Text, txtOldValue.Text);

            try
            {
                if (!CheckDuplicateGL())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjGLChangeAccess.Save(ObjGLChange) > 0)
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        if (ddlField2.SelectedValue != "0")
        {
            try
            {

                str = Convert.ToString(ddlField2.SelectedValue);
                char[] splitchar = { '-' };
                strArr = str.Split(splitchar);
                inputvalue = Convert.ToInt32(strArr[0]);

            }
            catch (Exception ex)
            {

            }
            Flag = false;
            GLChange ObjGLChange = GetControlsValue(ddlField2.SelectedValue, txtNewValue2.Text, txtOldValue2.Text);

            try
            {
                if (!CheckDuplicateGL())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjGLChangeAccess.Save(ObjGLChange) > 0)
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        if (ddlField3.SelectedValue != "0")
        {
            try
            {

                str = Convert.ToString(ddlField3.SelectedValue);
                char[] splitchar = { '-' };
                strArr = str.Split(splitchar);
                inputvalue = Convert.ToInt32(strArr[0]);

            }
            catch (Exception ex)
            {

            }
            Flag = false;
            GLChange ObjGLChange = GetControlsValue(ddlField3.SelectedValue, txtNewValue3.Text, txtOldValue3.Text);

            try
            {
                if (!CheckDuplicateGL())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjGLChangeAccess.Save(ObjGLChange) > 0)
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        if (ddlField4.SelectedValue != "0")
        {
            try
            {

                str = Convert.ToString(ddlField4.SelectedValue);
                char[] splitchar = { '-' };
                strArr = str.Split(splitchar);
                inputvalue = Convert.ToInt32(strArr[0]);

            }
            catch (Exception ex)
            {

            }
            Flag = false;
            GLChange ObjGLChange = GetControlsValue(ddlField4.SelectedValue, txtNewValue4.Text, txtOldValue4.Text);

            try
            {
                if (!CheckDuplicateGL())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjGLChangeAccess.Save(ObjGLChange) > 0)
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        if (ddlField5.SelectedValue != "0")
        {
            try
            {

                str = Convert.ToString(ddlField5.SelectedValue);
                char[] splitchar = { '-' };
                strArr = str.Split(splitchar);
                inputvalue = Convert.ToInt32(strArr[0]);

            }
            catch (Exception ex)
            {

            }
            Flag = false;
            GLChange ObjGLChange = GetControlsValue(ddlField5.SelectedValue, txtNewValue5.Text, txtOldValue5.Text);

            try
            {
                if (!CheckDuplicateGL())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjGLChangeAccess.Save(ObjGLChange) > 0)
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        return Flag;
    }

    //private bool CheckDuplicateGL()
    //{
    //    bool flg = false;



    //    if (lblGLChangeAction.Text == "F")
    //    {
    //        foreach (GridViewRow gr in grvGLChange.Rows)
    //        {
    //            if (txtGLCode.Text == gr.Cells[1].Text)
    //            {
    //                GridView grvGLChangeDtl = (GridView)gr.FindControl("grvGLChangeDtl");
    //                foreach (GridViewRow gr1 in grvGLChangeDtl.Rows)
    //                {
    //                    Label lblSectionFieldMasterId = (Label)gr1.FindControl("lblSectionFieldMasterId");

    //                    if (ddlField.SelectedValue == lblSectionFieldMasterId.Text)
    //                    {
    //                        flg = true;
    //                    }
    //                }
    //            }
    //        }


    //        if (flg)
    //        {
    //            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
    //            pnlMsg1.CssClass = "error";
    //            pnlMsg1.Visible = true;
    //        }

    //        //foreach (GridViewRow gr in grvGLChange.Rows)
    //        //{
    //        //    if (txtGLCode.Text == gr.Cells[1].Text)
    //        //    {
    //        //        flg = true;
    //        //    }
    //        //}

    //        //if (flg)
    //        //{
    //        //    lblMsg1.Text = "Duplicate GL. To enter more fields for the same GL Click the '+' i front of the GL.";
    //        //    pnlMsg1.CssClass = "error";
    //        //    pnlMsg1.Visible = true;
    //        //}

    //        if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
    //        {
    //            flg = true;
    //        }

    //        if (flg)
    //        {
    //            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
    //            pnlMsg1.CssClass = "error";
    //            pnlMsg1.Visible = true;
    //        }
    //    }
    //    else if (lblGLChangeAction.Text == "G")
    //    {
    //        foreach (GridViewRow gr in grvGLChange.Rows)
    //        {
    //            if (txtGLCode.Text == gr.Cells[1].Text)
    //            {
    //                flg = true;
    //            }
    //        }

    //        if (flg)
    //        {
    //            lblMsg1.Text = "Duplicate GL. To enter more fields for the same GL Click the '+' i front of the GL.";
    //            pnlMsg1.CssClass = "error";
    //            pnlMsg1.Visible = true;
    //        }

    //        foreach (GridViewRow gr in grvGLChange.Rows)
    //        {
    //            GridView grvGLChangeDtl = (GridView)gr.FindControl("grvGLChangeDtl");
    //            foreach (GridViewRow gr1 in grvGLChangeDtl.Rows)
    //            {
    //                Label lblSectionFieldMasterId = (Label)gr1.FindControl("lblSectionFieldMasterId");

    //                if (ddlField.SelectedValue == lblSectionFieldMasterId.Text)
    //                {
    //                    flg = true;
    //                }
    //            }
    //        }

    //        if (flg)
    //        {
    //            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
    //            pnlMsg1.CssClass = "error";
    //            pnlMsg1.Visible = true;
    //        }

    //        if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
    //        {
    //            flg = true;
    //        }

    //        if (flg)
    //        {
    //            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
    //            pnlMsg1.CssClass = "error";
    //            pnlMsg1.Visible = true;
    //        }

    //    }
    //    else if (lblGLChangeAction.Text == "E")
    //    {
    //        if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
    //        {
    //            flg = true;
    //        }
    //        else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
    //        {
    //            flg = true;
    //        }

    //        if (flg)
    //        {
    //            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
    //            pnlMsg1.CssClass = "error";
    //            pnlMsg1.Visible = true;
    //        }

    //    }
    //    else if (txtOldValue.Text == "" && txtNewValue.Text == "")
    //    {
    //        lblMsg1.Text = "Both Old value and New Value cannot be blank.";
    //        pnlMsg1.CssClass = "error";
    //        pnlMsg1.Visible = true;
    //        flg = true;
    //    }
    //    else if (txtOldValue.Text == txtNewValue.Text)
    //    {
    //        lblMsg1.Text = "Old value and New Value cannot be same.";
    //        pnlMsg1.CssClass = "error";
    //        pnlMsg1.Visible = true;
    //        flg = true;
    //    }

    //    return flg;
    //}

    private bool CheckDuplicateGL()
    {
        bool flg = false;



        if (lblGLChangeAction.Text == "F")
        {
            foreach (GridViewRow gr in grvGLChange.Rows)
            {
                if (txtGLCode.Text == gr.Cells[1].Text)
                {
                    GridView grvGLChangeDtl = (GridView)gr.FindControl("grvGLChangeDtl");
                    foreach (GridViewRow gr1 in grvGLChangeDtl.Rows)
                    {
                        Label lblSectionFieldMasterId = (Label)gr1.FindControl("lblSectionFieldMasterId");

                        if (ddlField.SelectedValue == lblSectionFieldMasterId.Text)
                        {
                            flg = true;
                            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                            pnlMsg1.CssClass = "error";
                            pnlMsg1.Visible = true;
                            break;
                        }
                    }
                }
            }


            if (flg == false)
            {



                //foreach (GridViewRow gr in grvGLChange.Rows)
                //{
                //    if (txtGLCode.Text == gr.Cells[1].Text)
                //    {
                //        flg = true;
                //    }
                //}

                //if (flg)
                //{
                //    lblMsg1.Text = "Duplicate Cost Center. To enter more fields for the same GL Click the '+' i front of the Cost Center.";
                //    pnlMsg1.CssClass = "error";
                //    pnlMsg1.Visible = true;
                //}

                //if (flg == false)
                //{
                if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
                {
                    flg = true;
                }
                else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
                {
                    flg = true;
                }
                else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
                {
                    flg = true;
                }
                else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
                {
                    flg = true;
                }

                if (flg)
                {
                    lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
                //}
            }

        }
        else if (lblGLChangeAction.Text == "G")
        {
            foreach (GridViewRow gr in grvGLChange.Rows)
            {
                if (txtGLCode.Text == gr.Cells[1].Text)
                {
                    flg = true;
                    lblMsg1.Text = "Duplicate GL. To enter more fields for the same GL Click the '+' i front of the GL.";
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                    break;
                }
            }

            if (flg == false)
            {

                foreach (GridViewRow gr in grvGLChange.Rows)
                {
                    if (txtGLCode.Text == gr.Cells[1].Text)
                    {
                        GridView grvGLChangeDtl = (GridView)gr.FindControl("grvGLChangeDtl");
                        foreach (GridViewRow gr1 in grvGLChangeDtl.Rows)
                        {
                            Label lblSectionFieldMasterId = (Label)gr1.FindControl("lblSectionFieldMasterId");

                            if (ddlField.SelectedValue == lblSectionFieldMasterId.Text)
                            {
                                flg = true;
                                lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                                pnlMsg1.CssClass = "error";
                                pnlMsg1.Visible = true;
                                break;
                            }
                        }
                    }
                }

                //if (flg)
                //{

                //}

                //flg = false;
                if (flg == false)
                {
                    if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
                    {
                        flg = true;
                    }
                    else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
                    {
                        flg = true;
                    }
                    else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
                    {
                        flg = true;
                    }
                    else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
                    {
                        flg = true;
                    }

                    if (flg)
                    {
                        lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                        pnlMsg1.CssClass = "error";
                        pnlMsg1.Visible = true;
                    }
                }
            }
        }

        else if (lblGLChangeAction.Text == "E")
        {
            foreach (GridViewRow gr in grvGLChange.Rows)
            {
                if (txtGLCode.Text == gr.Cells[1].Text)
                {
                    GridView grvGLChangeDtl = (GridView)gr.FindControl("grvGLChangeDtl");
                    foreach (GridViewRow gr1 in grvGLChangeDtl.Rows)
                    {
                        Label lblSectionFieldMasterId = (Label)gr1.FindControl("lblSectionFieldMasterId");

                        if (ddlField.SelectedValue == lblSectionFieldMasterId.Text)
                        {
                            flg = true;
                            lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                            pnlMsg1.CssClass = "error";
                            pnlMsg1.Visible = true;
                            break;
                        }
                    }
                }
            }


            if (flg == false)
            {

                if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
                {
                    flg = true;
                }
                else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
                {
                    flg = true;
                }
                else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
                {
                    flg = true;
                }
                else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
                {
                    flg = true;
                }

                if (flg)
                {
                    lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
            }
            //    if (ddlField.SelectedValue != "0" && ((ddlField.SelectedValue == ddlField2.SelectedValue) || (ddlField.SelectedValue == ddlField3.SelectedValue) || (ddlField.SelectedValue == ddlField4.SelectedValue) || (ddlField.SelectedValue == ddlField5.SelectedValue)))
            //    {
            //        flg = true;
            //    }
            //    else if (ddlField2.SelectedValue != "0" && ((ddlField2.SelectedValue == ddlField3.SelectedValue) || (ddlField2.SelectedValue == ddlField4.SelectedValue) || (ddlField2.SelectedValue == ddlField5.SelectedValue)))
            //    {
            //        flg = true;
            //    }
            //    else if (ddlField3.SelectedValue != "0" && ((ddlField3.SelectedValue == ddlField4.SelectedValue) || (ddlField3.SelectedValue == ddlField5.SelectedValue)))
            //    {
            //        flg = true;
            //    }
            //    else if (ddlField4.SelectedValue != "0" && (ddlField4.SelectedValue == ddlField5.SelectedValue))
            //    {
            //        flg = true;
            //    }

            //    if (flg)
            //    {
            //        lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
            //        pnlMsg1.CssClass = "error";
            //        pnlMsg1.Visible = true;
            //    }

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

    private void BindGLChangeData()
    {
        grvGLChange.DataSource = ObjGLChangeAccess.GetGLChangeData(lblMasterHeaderId.Text);
        grvGLChange.DataBind();
    }

    private void bindgrvGLChangeDtl(int GLChangeId, GridView grvGLChangeDtl)
    {
        grvGLChangeDtl.DataSource = ObjGLChangeAccess.GetGLChangeDetailData(GLChangeId);
        grvGLChangeDtl.DataBind();
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
            boolFlg = SaveGLChange();
        }
        return boolFlg;
    }

    private bool SaveGLChange()
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

    private GLChange GetGLChange()
    {
        return ObjGLChangeAccess.GetGLChange(Convert.ToInt32(lblGLChange.Text));
    }

    private GLChangeDetail GetGLChangeDetail()
    {
        //return ObjGLChangeAccess.GetGLChangeDetail(Convert.ToInt32(lblGLChangeDetailId.Text));
        return ObjGLChangeAccess.GetGLChangeDetail((Convert.ToInt32(lblMasterHeaderId.Text)), (Convert.ToInt32(lblGLChangeDetailId.Text)));
    }

    private GLChange GetControlsValue(string ddlfields, string newval, string oldval)
    {
        GLChange ObjGLChange = new GLChange();
        Utility objUtil = new Utility();


        ObjGLChange.GL_Change_Id = Convert.ToInt32(lblGLChange.Text);
        ObjGLChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjGLChange.GL_Code = txtGLCode.Text;
        ObjGLChange.Company_Code = ddlCompanyCode.SelectedValue;
        ObjGLChange.Account_Group = ddlGLAccGrp.SelectedValue;
        ObjGLChange.GL_Desc = txtGLName.Text;

        ObjGLChange.GL_Change_Detail_Id = Convert.ToInt32(lblGLChangeDetailId.Text);
        ObjGLChange.Section_Field_Master_Id = Convert.ToInt32(ddlfields);
        ObjGLChange.Old_Value = oldval;
        ObjGLChange.New_Value = newval;

        ObjGLChange.UserId = lblUserId.Text;
        ObjGLChange.TodayDate = objUtil.GetDate();
        ObjGLChange.IPAddress = objUtil.GetIpAddress();

        return ObjGLChange;
    }

    private void FillGLChangeData()
    {
        GLChange ObjGLChange = GetGLChange();
        if (ObjGLChange.GL_Change_Id > 0)
        {
            lblGLChange.Text = ObjGLChange.GL_Change_Id.ToString();
            txtGLCode.Text = ObjGLChange.GL_Code;
            ddlCompanyCode.SelectedValue = ObjGLChange.Company_Code;
            ddlGLAccGrp.SelectedValue = ObjGLChange.Account_Group;
            txtGLName.Text = ObjGLChange.GL_Desc;
        }
        else
        {
            ddlCompanyCode.SelectedValue = "32";
            //ddlCompanyCode.Enabled = false;
            txtGLCode.Text = "";
            ddlGLAccGrp.SelectedValue = "0";
            txtGLName.Text = "";

            //SDT17052019 Change By NR , Desc : Get Module ID from web config  
            int iSECGLN = Convert.ToInt32(ConfigurationManager.AppSettings["SECGLN"]);
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //SDT17052019 Change By NR , Desc : Get Module ID from web config

            //SDT17052019 Commented By NR 
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //EDT17052019 Commented By NR 

            txtOldValue.Text = "";
            txtNewValue.Text = "";
            txtOldValue2.Text = "";
            txtNewValue2.Text = "";
            txtOldValue3.Text = "";
            txtNewValue3.Text = "";
            txtOldValue4.Text = "";
            txtNewValue4.Text = "";
            txtOldValue5.Text = "";
            txtNewValue5.Text = "";
        }
    }

    private void FillGLChangeDetailData()
    {
        GLChangeDetail ObjGLChangeDetail = GetGLChangeDetail();

        if (ObjGLChangeDetail.GL_Change_Id > 0)
        {

            lblGLChange.Text = ObjGLChangeDetail.GL_Change_Id.ToString();

            GLChange objGLChange = ObjGLChangeAccess.GetGLChange(Convert.ToInt32(lblGLChange.Text));
            txtGLCode.Text = objGLChange.GL_Code;
            lblModuleId.Text = objGLChange.Account_Group;
            ddlCompanyCode.SelectedValue = objGLChange.Company_Code;
            ddlGLAccGrp.SelectedValue = objGLChange.Account_Group;
            txtGLName.Text = objGLChange.GL_Desc;

            //SDT17052019 Change By NR , Desc : Get Module ID from web config  
            int iSECGLN = Convert.ToInt32(ConfigurationManager.AppSettings["SECGLN"]);
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //SDT17052019 Change By NR , Desc : Get Module ID from web config

            //SDT17052019 Commented By NR 
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //EDT17052019 Commented By NR 

            //ddlField.SelectedValue = ObjGLChangeDetail.Section_Field_Master_Id.ToString();
            ddlField.SelectedValue = ObjGLChangeDetail.Field.ToString();
            if (ObjGLChangeDetail.Field2 != null)
                ddlField2.SelectedValue = ObjGLChangeDetail.Field2.ToString();
            if (ObjGLChangeDetail.Field3 != null)
                ddlField3.SelectedValue = ObjGLChangeDetail.Field3.ToString();
            if (ObjGLChangeDetail.Field4 != null)
                ddlField4.SelectedValue = ObjGLChangeDetail.Field4.ToString();
            if (ObjGLChangeDetail.Field5 != null)
                ddlField5.SelectedValue = ObjGLChangeDetail.Field5.ToString();
            txtOldValue.Text = ObjGLChangeDetail.Old_Value;
            txtNewValue.Text = ObjGLChangeDetail.New_Value;
            txtOldValue2.Text = ObjGLChangeDetail.Old_Value2;
            txtNewValue2.Text = ObjGLChangeDetail.New_Value2;
            txtOldValue3.Text = ObjGLChangeDetail.Old_Value3;
            txtNewValue3.Text = ObjGLChangeDetail.New_Value3;
            txtOldValue4.Text = ObjGLChangeDetail.Old_Value4;
            txtNewValue4.Text = ObjGLChangeDetail.New_Value4;
            txtOldValue5.Text = ObjGLChangeDetail.Old_Value5;
            txtNewValue5.Text = ObjGLChangeDetail.New_Value5;

        }
        else
        {

            GLChange objGLChange = ObjGLChangeAccess.GetGLChange(Convert.ToInt32(lblGLChange.Text));
            txtGLCode.Text = objGLChange.GL_Code;
            lblModuleId.Text = objGLChange.Account_Group;
            ddlCompanyCode.SelectedValue = objGLChange.Company_Code;
            ddlGLAccGrp.SelectedValue = objGLChange.Account_Group;
            txtGLName.Text = objGLChange.GL_Desc;

            //SDT17052019 Change By NR , Desc : Get Module ID from web config  
            int iSECGLN = Convert.ToInt32(ConfigurationManager.AppSettings["SECGLN"]);
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','" + iSECGLN + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //SDT17052019 Change By NR , Desc : Get Module ID from web config

            //SDT17052019 Commented By NR 
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId '" + ddlGLAccGrp.SelectedValue + "','92',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            //EDT17052019 Commented By NR 

            txtOldValue.Text = "";
            txtNewValue.Text = "";
            txtOldValue2.Text = "";
            txtNewValue2.Text = "";
            txtOldValue3.Text = "";
            txtNewValue3.Text = "";
            txtOldValue4.Text = "";
            txtNewValue4.Text = "";
            txtOldValue5.Text = "";
            txtNewValue5.Text = "";
        }
    }

    private bool CheckIsValid()
    {
        bool flg = false;

        if (grvGLChange.Rows.Count > 0)
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
        GLMasterAccess ObjGLMasterAccess = new GLMasterAccess();
        DataTable dt = ObjGLMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);


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
            lblMsg.Text = "Error While Saving GL Details.";
        }


        return flag;
    }

    #endregion
}