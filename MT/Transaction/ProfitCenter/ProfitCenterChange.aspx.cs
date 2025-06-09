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

public partial class Transaction_ProfitCenter_ProfitCenterChange : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    ProfitCenterMasterAccess ObjProfitCenterMasterAccess = new ProfitCenterMasterAccess();
    HelperAccess helperAccess = new HelperAccess();
    public bool isEditable { get; set; }

    #region Page Methods

    /// <summary>
    /// Done
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
                        PopuplateDropDownList();

                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                        //if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        //{
                        //    lnkAddNew.Visible = true;
                        //    isEditable = true;
                        //}

                        //BindProfitCenterChangeData();
                        //BindAttachedDocuments(lblMasterHeaderId.Text);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            isEditable = true; 
                            lnkAddNew.Visible = true;

                            BindProfitCenterChangeData();
                            BindAttachedDocuments(lblMasterHeaderId.Text);

                            trButton.Visible = true;
                            grdAttachedDocs.Columns[1].Visible = true;
                            file_upload.Visible = true;
                            grvProfitCenterChange.Columns[5].Visible = true;
                            grvProfitCenterChange.Columns[4].Visible = false;
                        }
                        else
                        {
                            BindProfitCenterChangeData();
                            BindAttachedDocuments(lblMasterHeaderId.Text);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveProfitCenterChangeData())
            {
                ModalPopupExtender.Hide();

                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                BindProfitCenterChangeData();
            }
            else
            {
                ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        { _log.Error("btnAdd_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (!CheckDuplicateProfitCenter())
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
                if (SaveProfitCenterChangeData())
                {
                    ModalPopupExtender.Hide();

                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;

                    BindProfitCenterChangeData();
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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton lnkEditValue = (ImageButton)sender;
        try
        {
            lblProfitCenterChangeDetailId.Text = lnkEditValue.CommandArgument;
            FillProfitCenterChangeDetailData();
            lblProfitCenterChangeAction.Text = "E";
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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = (ImageButton)sender;
        try
        {
            ObjProfitCenterMasterAccess.DeleteProfitCenterChangeDetail(btnDelete.CommandArgument);
            BindProfitCenterChangeData();
        }
        catch (Exception ex)
        { _log.Error("btnDelete_Click", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        ImageButton lnkAddValue = (ImageButton)sender;
        try
        {
            pnlMsg.Visible = false;
            pnlMsg1.Visible = false;
            lblProfitCenterChangeDetailId.Text = "0";
            lblProfitCenterChangeAction.Text = "F";
            lblProfitCenterChange.Text = lnkAddValue.CommandArgument;
            FillProfitCenterChangeDetailData();
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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            lblProfitCenterChange.Text = "0";
            lblProfitCenterChangeAction.Text = "I";
            lblProfitCenterChangeDetailId.Text = "0";
            pnlMsg.Visible = false;
            pnlMsg1.Visible = false;
            FillProfitCenterChangeData();

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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
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
            if (CheckIsValid())
            {
                if (Save())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    BindAttachedDocuments(lblMasterHeaderId.Text);

                    Response.Redirect("ProfitCenterChange.aspx");
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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
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

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grvProfitCenterChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPCMaster_Change_Id = (Label)e.Row.FindControl("lblPCMaster_Change_Id");

                GridView grvProfitCenterChangeDtl = (GridView)e.Row.FindControl("grvProfitCenterChangeDtl");
                bindgrvProfitCenterChangeDtl(Convert.ToInt32(lblPCMaster_Change_Id.Text), grvProfitCenterChangeDtl); //Bind the child gridvie here ..
                 grvProfitCenterChangeDtl.Columns[5].Visible = lnkAddNew.Visible;
            }
        }
        catch (Exception ex)
        { _log.Error("grvProfitCenterChange_RowDataBound", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
    { 
        try
        {
            //string str = null;
            //string[] strArr = null;
            //str = Convert.ToString(ddlField.SelectedValue);
            ////if (str.Contains("1368"))
            ////{
            ////    txtOldValue2.MaxLength = 20;
            ////    txtNewValue2.MaxLength = 20;
            ////}
            ////if (str.Contains("1369"))
            ////{
            ////    txtOldValue2.MaxLength = 40;
            ////    txtNewValue2.MaxLength = 40;
            ////}
            //char[] splitchar = { '-' };
            //strArr = str.Split(splitchar);
            //int inputlength = Convert.ToInt32(strArr[1]);
            //txtOldValue.MaxLength = inputlength;
            //txtNewValue.MaxLength = inputlength;

        }
        catch (Exception ex)
        { _log.Error("ddlField_SelectedIndexChanged", ex); }

        ModalPopupExtender.Show();
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlField2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //string str = null;
            //string[] strArr = null;
            //str = Convert.ToString(ddlField2.SelectedValue);

            //if (str.Contains("1368"))
            //{
            //    txtOldValue2.MaxLength = 20;
            //    txtNewValue2.MaxLength = 20;
            //}
            //if (str.Contains("1369"))
            //{
            //    txtOldValue2.MaxLength = 40;
            //    txtNewValue2.MaxLength = 40;
            //}
            //char[] splitchar = { '-' };
            //strArr = str.Split(splitchar);
            //int inputlength = Convert.ToInt32(strArr[1]);
            //txtOldValue.MaxLength = inputlength;
            //txtNewValue.MaxLength = inputlength;

            if(ddlField2.SelectedValue !=null && ddlField2.SelectedValue !="" && ddlField2.SelectedValue != "0")
            {
                rfvtxtOldValue2.Enabled = true;
                   rfvtxtNewValue2.Enabled = true;
            }
            else
            {
                rfvtxtOldValue2.Enabled = false;
                rfvtxtNewValue2.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ddlField2_SelectedIndexChanged", ex);
        }
        ModalPopupExtender.Show();
    }
    
    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlField3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlField3.SelectedValue != null && ddlField3.SelectedValue != "" && ddlField3.SelectedValue != "0")
            {
                rfvtxtOldValue3.Enabled = true;
                rfvtxtNewValue3.Enabled = true;
            }
            else
            {
                rfvtxtOldValue3.Enabled = false;
                rfvtxtNewValue3.Enabled = false;
            }
            //string str = null;
            //string[] strArr = null;
            //str = Convert.ToString(ddlField3.SelectedValue);
            //if (str.Contains("1368"))
            //{
            //    txtOldValue3.MaxLength = 20;
            //    txtNewValue3.MaxLength = 20;
            //}
            //if (str.Contains("1369"))
            //{
            //    txtOldValue3.MaxLength = 40;
            //    txtNewValue3.MaxLength = 40;
            //}
            //char[] splitchar = { '-' };
            //strArr = str.Split(splitchar);
            //int inputlength = Convert.ToInt32(strArr[1]);
            //txtOldValue.MaxLength = inputlength;
            //txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField3_SelectedIndexChanged", ex);
        }
        ModalPopupExtender.Show();
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlField4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlField4.SelectedValue != null && ddlField4.SelectedValue != "" && ddlField4.SelectedValue != "0")
            {
                rfvtxtOldValue4.Enabled = true;
                rfvtxtNewValue4.Enabled = true;
            }
            else
            {
                rfvtxtOldValue4.Enabled = false;
                rfvtxtNewValue4.Enabled = false;
            }
            //string str = null;
            //string[] strArr = null;
            ////str = Convert.ToString(ddlField4.SelectedValue);
            ////if (str.Contains("1368"))
            ////{
            ////    txtOldValue4.MaxLength = 20;
            ////    txtNewValue4.MaxLength = 20;
            ////}
            ////if (str.Contains("1369"))
            ////{
            ////    txtOldValue4.MaxLength = 40;
            ////    txtNewValue4.MaxLength = 40;
            ////}
            //char[] splitchar = { '-' };
            //strArr = str.Split(splitchar);
            //int inputlength = Convert.ToInt32(strArr[1]);
            //txtOldValue.MaxLength = inputlength;
            //txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField4_SelectedIndexChanged", ex);
        }
        ModalPopupExtender.Show();
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlField5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlField5.SelectedValue != null && ddlField5.SelectedValue != "" && ddlField5.SelectedValue != "0")
            {
                rfvtxtOldValue5.Enabled = true;
                rfvtxtNewValue5.Enabled = true;
            }
            else
            {
                rfvtxtOldValue5.Enabled = false;
                rfvtxtNewValue5.Enabled = false;
            }
            //string str = null;
            //string[] strArr = null;
            ////str = Convert.ToString(ddlField5.SelectedValue);
            ////if (str.Contains("1368"))
            ////{
            ////    txtOldValue5.MaxLength = 20;
            ////    txtNewValue5.MaxLength = 20;
            ////}
            ////if (str.Contains("1369"))
            ////{
            ////    txtOldValue5.MaxLength = 40;
            ////    txtNewValue5.MaxLength = 40;
            ////}
            //char[] splitchar = { '-' };
            //strArr = str.Split(splitchar);
            //int inputlength = Convert.ToInt32(strArr[1]);
            //txtOldValue.MaxLength = inputlength;
            //txtNewValue.MaxLength = inputlength;
        }
        catch (Exception ex)
        {
            _log.Error("ddlField5_SelectedIndexChanged", ex);
        }
        ModalPopupExtender.Show();
    }

    #endregion


    #region Public Methods
     
    /// <summary>
    /// Done
    /// </summary>
    private void PopuplateDropDownList()
    {
        try
        {
            int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCC"]);
            int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECPCN"]);
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
            helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");


        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    protected bool SaveProfitCenterChangeData1()
    {
        bool Flag = false;
        PCenterChange ObjPCenterChange = GetControlsValue(Convert.ToString(ddlField.SelectedValue), txtNewValue.Text, txtOldValue.Text);
        try
        {
            if (!CheckDuplicateProfitCenter())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ObjProfitCenterMasterAccess.SaveChange(ObjPCenterChange) > 0)
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
            _log.Error("SaveProfitCenterChangeData1", ex);
            //throw ex;
        }
        return Flag;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    protected bool SaveProfitCenterChangeData()
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
                _log.Error("SaveProfitCenterChangeData", ex);
            }

            PCenterChange ObjPCenterChange = GetControlsValue(Convert.ToString(inputvalue), txtNewValue.Text, txtOldValue.Text);

            try
            {
                if (!CheckDuplicateProfitCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjProfitCenterMasterAccess.SaveChange(ObjPCenterChange) > 0)
                        {
                            lblProfitCenterChange.Text = ObjProfitCenterMasterAccess.pPCMaster_Change_Id.ToString();
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
                _log.Error("SaveProfitCenterChangeData1", ex);
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
                _log.Error("SaveProfitCenterChangeData2", ex);
            }
            Flag = false;
            //count1  = count1 + 1;
            PCenterChange ObjPCenterChange = GetControlsValue(ddlField2.SelectedValue, txtNewValue2.Text, txtOldValue2.Text);

            try
            {
                if (!CheckDuplicateProfitCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjProfitCenterMasterAccess.SaveChange(ObjPCenterChange) > 0)
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
                _log.Error("SaveProfitCenterChangeData4", ex);
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
                _log.Error("SaveProfitCenterChangeData5", ex);
            }
            //count1  = count1 + 1;
            Flag = false;
            //count1  = count1 + 1;
            PCenterChange ObjPCenterChange = GetControlsValue(ddlField3.SelectedValue, txtNewValue3.Text, txtOldValue3.Text);

            try
            {
                if (!CheckDuplicateProfitCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjProfitCenterMasterAccess.SaveChange(ObjPCenterChange) > 0)
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
                _log.Error("SaveProfitCenterChangeData6", ex);
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
                _log.Error("SaveProfitCenterChangeData7", ex);
            }
            //count1 = count1 + 1;
            Flag = false;
            //count1  = count1 + 1;
            PCenterChange ObjPCenterChange = GetControlsValue(ddlField4.SelectedValue, txtNewValue4.Text, txtOldValue4.Text);

            try
            {
                if (!CheckDuplicateProfitCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjProfitCenterMasterAccess.SaveChange(ObjPCenterChange) > 0)
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
                _log.Error("SaveProfitCenterChangeData8", ex);
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
                _log.Error("SaveProfitCenterChangeData77", ex);
            }
            //count1 = count1 + 1;
            Flag = false;
            //count1  = count1 + 1;
            PCenterChange ObjPCenterChange = GetControlsValue(ddlField5.SelectedValue, txtNewValue5.Text, txtOldValue5.Text);

            try
            {
                if (!CheckDuplicateProfitCenter())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ObjProfitCenterMasterAccess.SaveChange(ObjPCenterChange) > 0)
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
                _log.Error("SaveProfitCenterChangeData777", ex);
                //throw ex;
            }
        }

        return Flag;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private bool CheckDuplicateProfitCenter()
    {
        bool flg = false;

        try
        {

            if (lblProfitCenterChangeAction.Text == "F")
            {
                foreach (GridViewRow gr in grvProfitCenterChange.Rows)
                {
                    if (txtProfitCenterCode.Text == gr.Cells[1].Text)
                    {
                        GridView grvCostCenterChangeDtl = (GridView)gr.FindControl("grvProfitCenterChangeDtl");
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

            }
            else if (lblProfitCenterChangeAction.Text == "I")
            {
                foreach (GridViewRow gr in grvProfitCenterChange.Rows)
                {
                    if (txtProfitCenterCode.Text == gr.Cells[1].Text)
                    {
                        flg = true;
                        lblMsg1.Text = "Duplicate Profit Center. To enter more fields for the same Profit Center Click the '+' i front of the Profit Center.";
                        pnlMsg1.CssClass = "error";
                        pnlMsg1.Visible = true;
                        break;
                    }
                }

                if (flg == false)
                {

                    foreach (GridViewRow gr in grvProfitCenterChange.Rows)
                    {
                        if (txtProfitCenterCode.Text == gr.Cells[1].Text)
                        {
                            GridView grvCostCenterChangeDtl = (GridView)gr.FindControl("grvProfitCenterChangeDtl");
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
                }
            }

            //else if (lblProfitCenterChangeAction.Text == "E")
            //{
            //    foreach (GridViewRow gr in grvProfitCenterChange.Rows)
            //    {
            //        if (txtProfitCenterCode.Text == gr.Cells[1].Text)
            //        {
            //            GridView grvCostCenterChangeDtl = (GridView)gr.FindControl("grvProfitCenterChangeDtl");
            //            foreach (GridViewRow gr1 in grvCostCenterChangeDtl.Rows)
            //            {
            //                Label lblSectionFieldMasterId = (Label)gr1.FindControl("lblSectionFieldMasterId");

            //                if (ddlField.SelectedValue == lblSectionFieldMasterId.Text)
            //                {
            //                    flg = true;
            //                    lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
            //                    pnlMsg1.CssClass = "error";
            //                    pnlMsg1.Visible = true;
            //                    break;
            //                }
            //            }
            //        }
            //    }


            //    if (flg == false)
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


            //}

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

    /// <summary>
    /// Done
    /// </summary>
    private void BindProfitCenterChangeData()
    {
        try
        {
            grvProfitCenterChange.DataSource = ObjProfitCenterMasterAccess.GetProfitCenterChangeData(lblMasterHeaderId.Text);
            grvProfitCenterChange.DataBind();
        }
        catch (Exception ex)
        { _log.Error("BindProfitCenterChangeData", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="ProfitCenterChangeId"></param>
    /// <param name="grvProfitCenterChangeDtl"></param>
    private void bindgrvProfitCenterChangeDtl(int ProfitCenterChangeId, GridView grvProfitCenterChangeDtl)
    {
        try
        {
            grvProfitCenterChangeDtl.DataSource = ObjProfitCenterMasterAccess.GetProfitCenterChangeDetailData(ProfitCenterChangeId);
            grvProfitCenterChangeDtl.DataBind();
        }
        catch (Exception ex)
        { _log.Error("bindgrvProfitCenterChangeDtl", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
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
                boolFlg = SaveProfitCenterChange();
            }
        }
        catch (Exception ex)
        { _log.Error("Save", ex); }
        return boolFlg;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private bool SaveProfitCenterChange()
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
            _log.Error("SaveProfitCenterChange", ex);
            //throw ex;
        }
        return Flag;
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private PCenterChange GetProfitCenterChange()
    {
        return ObjProfitCenterMasterAccess.GetProfitCenterChange(Convert.ToInt32(lblProfitCenterChange.Text));
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private PCenterChangeDt GetProfitCenterChangeDetail()
    {
        return ObjProfitCenterMasterAccess.GetProfitCenterChangeDetail((Convert.ToInt32(lblMasterHeaderId.Text)), (Convert.ToInt32(lblProfitCenterChangeDetailId.Text)));
    }

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="ddlfields"></param>
    /// <param name="newval"></param>
    /// <param name="oldval"></param>
    /// <returns></returns>
    private PCenterChange GetControlsValue(string ddlfields, string newval, string oldval)
    {
        PCenterChange ObjPCenterChange = new PCenterChange();
        Utility objUtil = new Utility();

        try
        {
            ObjPCenterChange.PCMaster_Change_Id = Convert.ToInt32(lblProfitCenterChange.Text);
            ObjPCenterChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjPCenterChange.sProfitCenter = txtProfitCenterCode.Text;
            ObjPCenterChange.sProfitCenterName = txtProfitCenterName.Text;
            ObjPCenterChange.Section_Id = Convert.ToInt32(lblSectionId.Text);
            ObjPCenterChange.PCMaster_Change_Details_Id = Convert.ToInt32(lblProfitCenterChangeDetailId.Text);
            ObjPCenterChange.Section_Field_Master_Id = Convert.ToInt32(ddlfields);
            ObjPCenterChange.sOld_Value = oldval;
            ObjPCenterChange.sNew_Value = newval;

            ObjPCenterChange.UserId = lblUserId.Text;
            ObjPCenterChange.TodayDate = objUtil.GetDate();
            ObjPCenterChange.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjPCenterChange;
    }
     
    /// <summary>
    /// Done
    /// </summary>
    private void FillProfitCenterChangeData()
    {
        PCenterChange ObjPCenterChange = GetProfitCenterChange();
        try
        {
            if (ObjPCenterChange.PCMaster_Change_Id > 0)
            {
                lblProfitCenterChange.Text = ObjPCenterChange.PCMaster_Change_Id.ToString();
                txtProfitCenterCode.Text = ObjPCenterChange.sProfitCenter;
                txtProfitCenterName.Text = ObjPCenterChange.sProfitCenterName;
            }
            else
            {
                txtProfitCenterCode.Text = "";
                txtProfitCenterName.Text = "";

                int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCC"]);
                int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECPCN"]);
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                
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
        { _log.Error("FillProfitCenterChangeData", ex); }
    }

    /// <summary>
    /// Done
    /// </summary>
    private void FillProfitCenterChangeDetailData()
    {
        PCenterChangeDt ObjPCenterChangeDt = GetProfitCenterChangeDetail();
        try
        {
            if (ObjPCenterChangeDt.PCMaster_Change_Id > 0)
            {

                lblProfitCenterChange.Text = ObjPCenterChangeDt.PCMaster_Change_Id.ToString();

                PCenterChange objPCenterChange = ObjProfitCenterMasterAccess.GetProfitCenterChange(Convert.ToInt32(lblProfitCenterChange.Text));
                txtProfitCenterCode.Text = objPCenterChange.sProfitCenter;
                lblModuleId.Text = objPCenterChange.sProfitCenterName;

                int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCC"]);
                int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECPCN"]);
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");


                txtProfitCenterName.Text = objPCenterChange.sProfitCenterName;

                ddlField.SelectedValue = ObjPCenterChangeDt.Section_Field_Master_Id.ToString();
                if (ObjPCenterChangeDt.Field2 != null)
                    ddlField2.SelectedValue = ObjPCenterChangeDt.Field2.ToString();
                if (ObjPCenterChangeDt.Field3 != null)
                    ddlField3.SelectedValue = ObjPCenterChangeDt.Field3.ToString();
                if (ObjPCenterChangeDt.Field4 != null)
                    ddlField4.SelectedValue = ObjPCenterChangeDt.Field4.ToString();
                if (ObjPCenterChangeDt.Field5 != null)
                    ddlField5.SelectedValue = ObjPCenterChangeDt.Field5.ToString();
                txtOldValue.Text = ObjPCenterChangeDt.sOld_Value;
                txtNewValue.Text = ObjPCenterChangeDt.sNew_Value;
                txtOldValue2.Text = ObjPCenterChangeDt.sOld_Value2;
                txtNewValue2.Text = ObjPCenterChangeDt.sNew_Value2;
                txtOldValue3.Text = ObjPCenterChangeDt.sOld_Value3;
                txtNewValue3.Text = ObjPCenterChangeDt.sNew_Value3;
                txtOldValue4.Text = ObjPCenterChangeDt.sOld_Value4;
                txtNewValue4.Text = ObjPCenterChangeDt.sNew_Value4;
                txtOldValue5.Text = ObjPCenterChangeDt.sOld_Value5;
                txtNewValue5.Text = ObjPCenterChangeDt.sNew_Value5;
            }
            else
            {

                PCenterChange objPCenterChange = ObjProfitCenterMasterAccess.GetProfitCenterChange(Convert.ToInt32(lblProfitCenterChange.Text));
                txtProfitCenterCode.Text = objPCenterChange.sProfitCenter;
                txtProfitCenterName.Text = objPCenterChange.sProfitCenterName;
                 
                int iModuleCostCC = Convert.ToInt32(ConfigurationManager.AppSettings["ModuleProfitCC"]);
                int iSECCCN = Convert.ToInt32(ConfigurationManager.AppSettings["SECPCN"]);
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField2, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField3, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField4, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                helperAccess.PopuplateDropDownList(ddlField5, "pr_GetFieldsByModuleSectionIdPC " + iModuleCostCC + "," + iSECCCN + ",null,32", "Field_Name", "Field_Id");
                

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
        { _log.Error("FillProfitCenterChangeDetailData", ex); }

    }

    /// <summary>
    /// Done
    /// </summary>
    /// <returns></returns>
    private bool CheckIsValid()
    {
        bool flg = false;
        try
        {
            if (grvProfitCenterChange.Rows.Count > 0)
                flg = true;

        }
        catch (Exception ex)
        { _log.Error("CheckIsValid", ex); }
        return flg;
    }

    #endregion

    #region Document Upload Done

    /// <summary>
    /// Done
    /// </summary>
    /// <param name="vendorId"></param>
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
     
    /// <summary>
    /// Done
    /// </summary>
    /// <param name="vendorId"></param>
    /// <returns></returns>
    private bool SaveDocuments(string vendorId)
    {
        CostCenterMasterAccess ObjCostCenterMasterAccess = new CostCenterMasterAccess();
        DataTable dt = ObjCostCenterMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        //try
        //{
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
                lblMsg.Text = "Error While Saving Profit Center Details.";
            }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }

        return flag;
    }

    #endregion
}