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
using log4net;
public partial class Transaction_CostCenter_CostCenterChange : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    CostCenterChangeAccess ObjCostCenterChangeAccess = new CostCenterChangeAccess();
    HelperAccess helperAccess = new HelperAccess();
    public bool isEditable { get; set; }

    #region Page Methods

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
                        CCPC();/// Carve_LC17&LC23_8400000406
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

                        BindCostCenterChangeData();
                        BindAttachedDocuments(lblMasterHeaderId.Text);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            trButton.Visible = true;
                            grdAttachedDocs.Columns[1].Visible = true;
                            file_upload.Visible = true;
                            lnkAddNew.Visible = true;
                            grvCostCenterChange.Columns[5].Visible = true;
                            grvCostCenterChange.Columns[4].Visible = false;
                        }
                        else
                        {
                            grdAttachedDocs.Columns[1].Visible = false;
                            file_upload.Visible = false;
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
    /// <summary>
    /// Carve_LC17&LC23_8400000406
    /// </summary>
    private void CCPC()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id", "");

        if (Session[StaticKeys.SelectedddlCompany].ToString() != null && Session[StaticKeys.SelectedddlCompany].ToString() != "")
        {
            ddlCompanyCode.SelectedValue = Session[StaticKeys.SelectedddlCompany].ToString();
        }

        helperAccess.PopuplateDropDownList(ddlBusinessArea, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
        if (Session[StaticKeys.MaterialPlantId].ToString() != null && Session[StaticKeys.MaterialPlantId].ToString() != "")
        {
            ddlBusinessArea.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveCostCenterChangeData())
            {
                ModalPopupExtender.Hide();

                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                BindCostCenterChangeData();
            }
            else
            {
                ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        { _log.Error("btnAdd_Click", ex); }
    }

    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        bool flag = false;
        string message = "";
        lblMsg1.Text = "";
        try
        {
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
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
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
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
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
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
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
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                ModalPopupExtender.Show();
            }
            if ((txtOldValue.Text != "" || txtNewValue.Text != "") && (ddlField.SelectedValue == "0"))
            {
                message = "alert('Please select Field')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                ModalPopupExtender.Show();
            }
            if ((txtOldValue2.Text != "" || txtNewValue2.Text != "") && (ddlField2.SelectedValue == "0"))
            {
                message = "alert('Please select Field')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                ModalPopupExtender.Show();
            }
            if ((txtOldValue3.Text != "" || txtNewValue3.Text != "") && (ddlField3.SelectedValue == "0"))
            {
                message = "alert('Please select Field')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                ModalPopupExtender.Show();
            }
            if ((txtOldValue4.Text != "" || txtNewValue4.Text != "") && (ddlField4.SelectedValue == "0"))
            {
                message = "alert('Please select Field')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                ModalPopupExtender.Show();
            }
            if ((txtOldValue5.Text != "" || txtNewValue5.Text != "") && (ddlField5.SelectedValue == "0"))
            {
                message = "alert('Please select Field')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                flag = false;
                if (!CheckDuplicateCostCenter())
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                ModalPopupExtender.Show();
            }
            if (message.Length > 0)
            {
                flag = false;
            }

            if (flag)
            {
                lblMsg1.Text = "";
                if (SaveCostCenterChangeData())
                {
                    ModalPopupExtender.Hide();

                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;

                    BindCostCenterChangeData();
                }
                else
                {
                    ModalPopupExtender.Show();
                }
            }


        }
        catch (Exception ex)
        { _log.Error("btnAddValue_Click", ex); }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton lnkEditValue = (ImageButton)sender;
        try
        {
            lblCostCenterChangeDetailId.Text = lnkEditValue.CommandArgument;
            FillCostCenterChangeDetailData();
            lblCostCenterChangeAction.Text = "E";
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
        catch (Exception ex)
        { _log.Error("btnEdit_Click", ex); }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = (ImageButton)sender;
        try
        {
            ObjCostCenterChangeAccess.DeleteCostCenterChangeDetail(btnDelete.CommandArgument);
            BindCostCenterChangeData();
        }
        catch (Exception ex)
        { _log.Error("btnDelete_Click", ex); }
    }

    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        ImageButton lnkAddValue = (ImageButton)sender;
        try
        {
            pnlMsg.Visible = false;
            pnlMsg1.Visible = false;
            lblCostCenterChangeDetailId.Text = "0";
            lblCostCenterChangeAction.Text = "F";
            lblCostCenterChange.Text = lnkAddValue.CommandArgument;
            FillCostCenterChangeDetailData();
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
        catch (Exception ex)
        { _log.Error("lnkAddValue_Click", ex); }
    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            lblCostCenterChange.Text = "0";
            lblCostCenterChangeAction.Text = "I";
            lblCostCenterChangeDetailId.Text = "0";
            pnlMsg.Visible = false;
            pnlMsg1.Visible = false;
            FillCostCenterChangeData();

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
        catch (Exception ex)
        { _log.Error("lnkAddNew_Click", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    BindAttachedDocuments(lblMasterHeaderId.Text);

                    Response.Redirect("CostCenterChange.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Please fill atleast one field.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
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
                        System.IO.File.Delete(Server.MapPath("CostCenterDocuments") + "/" + lblUploadedFileName.Text);
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

    protected void ddlCostCenterAccGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //SDT17052019 Change By NR , Desc : Get Module ID from web config 
            int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleCostCC"]);
            int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECCCN"]);
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            //SDT17052019 Change By NR , Desc : Get Module ID from web config

            //SDT17052019 Commented By NR  
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");

            //EDT17052019 Commented By NR  



            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("ddlCostCenterAccGrp_SelectedIndexChanged", ex); }
    }

    protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = null;
        try
        {
            str = Convert.ToString(ddlField.SelectedValue);
            if (str.Contains("1368"))
            {
                txtOldValue.MaxLength = 20;
                txtNewValue.MaxLength = 20;
            }
            if (str.Contains("1369"))
            {
                txtOldValue.MaxLength = 40;
                txtNewValue.MaxLength = 40;
            }
            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("ddlField_SelectedIndexChanged", ex); }
    }

    protected void grvCostCenterChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCostCenterChangeId = (Label)e.Row.FindControl("lblCostCenterChangeId");

                GridView grvCostCenterChangeDtl = (GridView)e.Row.FindControl("grvCostCenterChangeDtl");
                bindgrvCostCenterChangeDtl(Convert.ToInt32(lblCostCenterChangeId.Text), grvCostCenterChangeDtl); //Bind the child gridvie here ..
                grvCostCenterChangeDtl.Columns[5].Visible = lnkAddNew.Visible;
            }
        }
        catch (Exception ex)
        { _log.Error("grvCostCenterChange_RowDataBound", ex); }
    }

    protected void ddlField2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = null;
            string[] strArr = null;
            str = Convert.ToString(ddlField2.SelectedValue);
            if (str.Contains("1368"))
            {
                txtOldValue2.MaxLength = 20;
                txtNewValue2.MaxLength = 20;
            }
            if (str.Contains("1369"))
            {
                txtOldValue2.MaxLength = 40;
                txtNewValue2.MaxLength = 40;
            }
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField2_SelectedIndexChanged", ex);
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
            if (str.Contains("1368"))
            {
                txtOldValue3.MaxLength = 20;
                txtNewValue3.MaxLength = 20;
            }
            if (str.Contains("1369"))
            {
                txtOldValue3.MaxLength = 40;
                txtNewValue3.MaxLength = 40;
            }
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField3_SelectedIndexChanged", ex);
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
            if (str.Contains("1368"))
            {
                txtOldValue4.MaxLength = 20;
                txtNewValue4.MaxLength = 20;
            }
            if (str.Contains("1369"))
            {
                txtOldValue4.MaxLength = 40;
                txtNewValue4.MaxLength = 40;
            }
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField4_SelectedIndexChanged", ex);
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
            if (str.Contains("1368"))
            {
                txtOldValue5.MaxLength = 20;
                txtNewValue5.MaxLength = 20;
            }
            if (str.Contains("1369"))
            {
                txtOldValue5.MaxLength = 40;
                txtNewValue5.MaxLength = 40;
            }
            char[] splitchar = { '-' };
            strArr = str.Split(splitchar);
            int inputlength = Convert.ToInt32(strArr[1]);
            txtOldValue.MaxLength = inputlength;
            txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField5_SelectedIndexChanged", ex);
        }
        ModalPopupExtender.Show();
    }

    #endregion

    #region Public Methods


    private void PopuplateDropDownList()
    {
        try
        {
            //SDT17052019 Change By NR , Desc : Get Module ID from web config 
            int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleCostCC"]);
            int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECCCN"]);
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            //SDT17052019 Change By NR , Desc : Get Module ID from web config

            //SDT17052019 Commented By NR  
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
            //EDT17052019 Commented By NR  

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    protected bool SaveCostCenterChangeData1()
    {
        bool Flag = false;
        CostCenterChange ObjCostCenterChange = GetControlsValue(Convert.ToString(ddlField.SelectedValue), txtNewValue.Text, txtOldValue.Text);
        try
        {
            if (!CheckDuplicateCostCenter())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ObjCostCenterChangeAccess.Save(ObjCostCenterChange) > 0)
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
            _log.Error("SaveCostCenterChangeData1", ex);
            //throw ex;
        }
        return Flag;
    }

    protected bool SaveCostCenterChangeData()
    {
        int count1 = 0;
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
                _log.Error("SaveCostCenterChangeData", ex);
            }

            CostCenterChange ObjCostCenterChange = GetControlsValue(Convert.ToString(inputvalue), txtNewValue.Text, txtOldValue.Text);

            try
            {
                if (!CheckDuplicateCostCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjCostCenterChangeAccess.Save(ObjCostCenterChange) > 0)
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
                _log.Error("SaveCostCenterChangeData1", ex);
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
                _log.Error("SaveCostCenterChangeData2", ex);
            }
            Flag = false;
            //count1  = count1 + 1;
            CostCenterChange ObjCostCenterChange = GetControlsValue(ddlField2.SelectedValue, txtNewValue2.Text, txtOldValue2.Text);

            try
            {
                if (!CheckDuplicateCostCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjCostCenterChangeAccess.Save(ObjCostCenterChange) > 0)
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
                _log.Error("SaveCostCenterChangeData4", ex);
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
                _log.Error("SaveCostCenterChangeData5", ex);
            }
            //count1  = count1 + 1;
            Flag = false;
            //count1  = count1 + 1;
            CostCenterChange ObjCostCenterChange = GetControlsValue(ddlField3.SelectedValue, txtNewValue3.Text, txtOldValue3.Text);

            try
            {
                if (!CheckDuplicateCostCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjCostCenterChangeAccess.Save(ObjCostCenterChange) > 0)
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
                _log.Error("SaveCostCenterChangeData5", ex);
                //throw ex;
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
                _log.Error("SaveCostCenterChangeData6", ex);
            }
            //count1 = count1 + 1;
            Flag = false;
            //count1  = count1 + 1;
            CostCenterChange ObjCostCenterChange = GetControlsValue(ddlField4.SelectedValue, txtNewValue4.Text, txtOldValue4.Text);

            try
            {
                if (!CheckDuplicateCostCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjCostCenterChangeAccess.Save(ObjCostCenterChange) > 0)
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
                _log.Error("SaveCostCenterChangeData6", ex);
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
                _log.Error("SaveCostCenterChangeData7", ex);
            }
            //count1 = count1 + 1;
            Flag = false;
            //count1  = count1 + 1;
            CostCenterChange ObjCostCenterChange = GetControlsValue(ddlField5.SelectedValue, txtNewValue5.Text, txtOldValue5.Text);

            try
            {
                if (!CheckDuplicateCostCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjCostCenterChangeAccess.Save(ObjCostCenterChange) > 0)
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
                _log.Error("SaveCostCenterChangeData77", ex);
                //throw ex;
            }
        }

        return Flag;
    }

    private bool CheckDuplicateCostCenter()
    {
        bool flg = false;

        try
        {

            if (lblCostCenterChangeAction.Text == "F")
            {
                foreach (GridViewRow gr in grvCostCenterChange.Rows)
                {
                    if (txtCostCenterCode.Text == gr.Cells[1].Text)
                    {
                        GridView grvCostCenterChangeDtl = (GridView)gr.FindControl("grvCostCenterChangeDtl");
                        foreach (GridViewRow gr1 in grvCostCenterChangeDtl.Rows)
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



                    //foreach (GridViewRow gr in grvCostCenterChange.Rows)
                    //{
                    //    if (txtCostCenterCode.Text == gr.Cells[1].Text)
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
            else if (lblCostCenterChangeAction.Text == "I")
            {
                foreach (GridViewRow gr in grvCostCenterChange.Rows)
                {
                    if (txtCostCenterCode.Text == gr.Cells[1].Text)
                    {
                        flg = true;
                        lblMsg1.Text = "Duplicate CostCenter. To enter more fields for the same CostCenter Click the '+' i front of the CostCenter.";
                        pnlMsg1.CssClass = "error";
                        pnlMsg1.Visible = true;
                        break;
                    }
                }

                if (flg == false)
                {

                    foreach (GridViewRow gr in grvCostCenterChange.Rows)
                    {
                        if (txtCostCenterCode.Text == gr.Cells[1].Text)
                        {
                            GridView grvCostCenterChangeDtl = (GridView)gr.FindControl("grvCostCenterChangeDtl");
                            foreach (GridViewRow gr1 in grvCostCenterChangeDtl.Rows)
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

            else if (lblCostCenterChangeAction.Text == "E")
            {
                foreach (GridViewRow gr in grvCostCenterChange.Rows)
                {
                    if (txtCostCenterCode.Text == gr.Cells[1].Text)
                    {
                        GridView grvCostCenterChangeDtl = (GridView)gr.FindControl("grvCostCenterChangeDtl");
                        foreach (GridViewRow gr1 in grvCostCenterChangeDtl.Rows)
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

        }
        catch (Exception ex)
        { _log.Error("CheckDuplicateCostCenter", ex); }
        return flg;
    }

    private void BindCostCenterChangeData()
    {
        try
        {
            grvCostCenterChange.DataSource = ObjCostCenterChangeAccess.GetCostCenterChangeData(lblMasterHeaderId.Text);
            grvCostCenterChange.DataBind();
        }
        catch (Exception ex)
        { _log.Error("BindCostCenterChangeData", ex); }
    }

    private void bindgrvCostCenterChangeDtl(int CostCenterChangeId, GridView grvCostCenterChangeDtl)
    {
        try
        {
            grvCostCenterChangeDtl.DataSource = ObjCostCenterChangeAccess.GetCostCenterChangeDetailData(CostCenterChangeId);
            grvCostCenterChangeDtl.DataBind();
        }
        catch (Exception ex)
        { _log.Error("bindgrvCostCenterChangeDtl", ex); }
    }

    private bool Save()
    {
        int flg = 0;
        bool boolFlg = false;
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

            if (flg == 2)
            {
                lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            else
            {
                boolFlg = SaveCostCenterChange();
            }
        }
        catch (Exception ex)
        { _log.Error("Save", ex); }
        return boolFlg;
    }

    private bool SaveCostCenterChange()
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
            _log.Error("SaveCostCenterChange", ex);
            //throw ex;
        }
        return Flag;
    }

    private CostCenterChange GetCostCenterChange()
    {
        return ObjCostCenterChangeAccess.GetCostCenterChange(Convert.ToInt32(lblCostCenterChange.Text));
    }

    private CostCenterChangeDetail GetCostCenterChangeDetail()
    {
        return ObjCostCenterChangeAccess.GetCostCenterChangeDetail((Convert.ToInt32(lblMasterHeaderId.Text)), (Convert.ToInt32(lblCostCenterChangeDetailId.Text)));
        //return ObjCostCenterChangeAccess.GetCostCenterChangeDetail(Convert.ToInt32(lblCostCenterChangeDetailId.Text));
    }

    private CostCenterChange GetControlsValue(string ddlfields, string newval, string oldval)
    {
        CostCenterChange ObjCostCenterChange = new CostCenterChange();
        Utility objUtil = new Utility();

        try
        {
            ObjCostCenterChange.CostCenter_Change_Id = Convert.ToInt32(lblCostCenterChange.Text);
            ObjCostCenterChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjCostCenterChange.Cost_Center = txtCostCenterCode.Text;
            ObjCostCenterChange.Cost_Center_Name = txtCostCenterName.Text;

            ObjCostCenterChange.CostCenter_Change_Detail_Id = Convert.ToInt32(lblCostCenterChangeDetailId.Text);
            ObjCostCenterChange.Section_Field_Master_Id = Convert.ToInt32(ddlfields);
            ObjCostCenterChange.Old_Value = oldval;
            ObjCostCenterChange.New_Value = newval;

            ObjCostCenterChange.UserId = lblUserId.Text;
            ObjCostCenterChange.TodayDate = objUtil.GetDate();
            ObjCostCenterChange.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjCostCenterChange;
    }

    private void FillCostCenterChangeData()
    {
        CostCenterChange ObjCostCenterChange = GetCostCenterChange();
        try
        {
            if (ObjCostCenterChange.CostCenter_Change_Id > 0)
            {
                lblCostCenterChange.Text = ObjCostCenterChange.CostCenter_Change_Id.ToString();
                txtCostCenterCode.Text = ObjCostCenterChange.Cost_Center;
                txtCostCenterName.Text = ObjCostCenterChange.Cost_Center_Name;
            }
            else
            {
                txtCostCenterCode.Text = "";
                txtCostCenterName.Text = "";

                //SDT17052019 Change By NR , Desc : Get Module ID from web config 
                int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleCostCC"]);
                int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECCCN"]);
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                //SDT17052019 Change By NR , Desc : Get Module ID from web config

                //SDT17052019 Commented By NR 
                //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
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
        catch (Exception ex)
        { _log.Error("FillCostCenterChangeData", ex); }
    }

    private void FillCostCenterChangeDetailData()
    {
        CostCenterChangeDetail ObjCostCenterChangeDetail = GetCostCenterChangeDetail();
        try
        {
            if (ObjCostCenterChangeDetail.CostCenter_Change_Id > 0)
            {

                lblCostCenterChange.Text = ObjCostCenterChangeDetail.CostCenter_Change_Id.ToString();

                CostCenterChange objCostCenterChange = ObjCostCenterChangeAccess.GetCostCenterChange(Convert.ToInt32(lblCostCenterChange.Text));
                txtCostCenterCode.Text = objCostCenterChange.Cost_Center;
                lblModuleId.Text = objCostCenterChange.Cost_Center_Name;

                //SDT17052019 Change By NR , Desc : Get Module ID from web config 
                int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleCostCC"]);
                int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECCCN"]);
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                //SDT17052019 Change By NR , Desc : Get Module ID from web config

                //SDT17052019 Commented By NR 
                //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //EDT17052019 Commented By NR 

                txtCostCenterName.Text = objCostCenterChange.Cost_Center_Name;

                ddlField.SelectedValue = ObjCostCenterChangeDetail.Section_Field_Master_Id.ToString();
                if (ObjCostCenterChangeDetail.Field2 != null)
                    ddlField2.SelectedValue = ObjCostCenterChangeDetail.Field2.ToString();
                if (ObjCostCenterChangeDetail.Field3 != null)
                    ddlField3.SelectedValue = ObjCostCenterChangeDetail.Field3.ToString();
                if (ObjCostCenterChangeDetail.Field4 != null)
                    ddlField4.SelectedValue = ObjCostCenterChangeDetail.Field4.ToString();
                if (ObjCostCenterChangeDetail.Field5 != null)
                    ddlField5.SelectedValue = ObjCostCenterChangeDetail.Field5.ToString();
                txtOldValue.Text = ObjCostCenterChangeDetail.Old_Value;
                txtNewValue.Text = ObjCostCenterChangeDetail.New_Value;
                txtOldValue2.Text = ObjCostCenterChangeDetail.Old_Value2;
                txtNewValue2.Text = ObjCostCenterChangeDetail.New_Value2;
                txtOldValue3.Text = ObjCostCenterChangeDetail.Old_Value3;
                txtNewValue3.Text = ObjCostCenterChangeDetail.New_Value3;
                txtOldValue4.Text = ObjCostCenterChangeDetail.Old_Value4;
                txtNewValue4.Text = ObjCostCenterChangeDetail.New_Value4;
                txtOldValue5.Text = ObjCostCenterChangeDetail.Old_Value5;
                txtNewValue5.Text = ObjCostCenterChangeDetail.New_Value5;
            }
            else
            {

                CostCenterChange objCostCenterChange = ObjCostCenterChangeAccess.GetCostCenterChange(Convert.ToInt32(lblCostCenterChange.Text));
                txtCostCenterCode.Text = objCostCenterChange.Cost_Center;
                lblModuleId.Text = objCostCenterChange.Cost_Center_Name;

                //SDT17052019 Change By NR , Desc : Get Module ID from web config 
                int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleCostCC"]);
                int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECCCN"]);
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                //SDT17052019 Change By NR , Desc : Get Module ID from web config

                //SDT17052019 Commented By NR 
                //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionId 197,89,null,32", "Field_Name", "Field_Id");
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
        catch (Exception ex)
        { _log.Error("FillCostCenterChangeDetailData", ex); }

    }


    private bool CheckIsValid()
    {
        bool flg = false;
        try
        {
            if (grvCostCenterChange.Rows.Count > 0)
                flg = true;

        }
        catch (Exception ex)
        { _log.Error("CheckIsValid", ex); }
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
            _log.Error("BindAttachedDocuments", ex);
            //throw ex;
        }
        finally
        {
            objDb = null;
        }
    }

    private bool SaveDocuments(string vendorId)
    {
        CostCenterMasterAccess ObjCostCenterMasterAccess = new CostCenterMasterAccess();
        DataTable dt = ObjCostCenterMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        //try
        //{
        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/CostCenterMaster/CostCenterDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
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

    private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath)
    {
        DocumentUpload ObjDoc = new DocumentUpload();
        DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
        bool flag = false;
        Utility objUtil = new Utility();

        try
        {


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
            lblMsg.Text = "Error While Saving CostCenter Details.";
        }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }

        return flag;
    }

    #endregion
}