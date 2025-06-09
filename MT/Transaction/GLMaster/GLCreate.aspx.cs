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

 //8400000241 GL Audit point change - all fields are non editable to approval, approval can't change in form
public partial class Transaction_GLMaster_GLCreate : System.Web.UI.Page
{
    GLMasterAccess ObjGLMasterAccess = new GLMasterAccess();
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
                    }
                    else
                    {
                        grdAttachedDocs.Columns[1].Visible = false;
                        file_upload.Visible = false;
                        file_upload.Visible = false;
                    }
                    
                  
                    ConfigureControl();
                    //setDisplayCEC();
                }
                else
                {
                    Response.Redirect("GLMaster.aspx");
                }
            }
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (Save() == 1)
            {
                if (SaveGLMaster())
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            string Module = ddlAccGroup.SelectedValue;

            if (txtGLCode.Text != "" && (Module != MaterialHelper.GetGLAccGrpByMaterialCode(txtGLCode.Text)))
            {
                lblMsg.Text = "GL Code does not match for selected Account Group.";//"Please enter only " + ddlAccGroup.SelectedItem.Text;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtGLCode.Text = "";
            }

            else if (Module != MaterialHelper.GetGLAccGrpByMaterialCode(txtRefGLCode.Text) && txtRefGLCode.Text != "")
            {
                lblMsg.Text = "Reference GL Code does not match for selected Account Group.";//"Please enter only " + ddlAccGroup.SelectedItem.Text;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtGLCode.Text = "";
            }

            else if (txtRefGLCode.Text != "" && ddlRefCompanyCode.SelectedValue == "0")
            {
                string message = "alert('Please select Reference Company Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }


            //SDT17052019 Commented By NR  
            //else if ((Module == "204" || Module == "206" || Module == "213") && ddlAccType.SelectedIndex == 1 && ddlCostElementCategory.SelectedValue == "0")
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
            //S4HanaGLDT07122021
            //else if ((Module == Convert.ToString(ConfigurationManager.AppSettings["ModuleCONS"]) || Module == Convert.ToString(ConfigurationManager.AppSettings["ModuleEXPN"]) || Module == Convert.ToString(ConfigurationManager.AppSettings["ModuleREVN"])) && ddlAccType.SelectedIndex == 1 && ddlCostElementCategory.SelectedValue == "0")
            //{
            //    string message = "alert('Please select Cost Element Category.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //}
             
            //S4HanaGLDT07122021
            //else 
            if (txtRefGLCode.Text == "" && txtGLCode.Text == "" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
            {
                string message = "alert('Please enter either GL Code or Reference GL Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

            else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && (txtGLCode.Text == ""))
            {
                string message = "alert('Please enter GL Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

            //SDT17052019 Commented By NR  
            //else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "44" && (txtGLCode.Text == ""))
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Department ID from web config
            else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["DEPSME"]) && (txtGLCode.Text == ""))
            {
                string message = "alert('Please enter GL Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            //S4HanaGLDT07122021
            else if (ddlClearSpectoLedgerGPS.SelectedValue == "1" && ddlOpenItemMgmt.SelectedValue == "1")
            {
                string message = "alert('Either Clearing Spec. to Ledger GPS is YES  or Open Item Management is YES.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            //S4HanaGLDT07122021

            else
            {
                lblMsg.Text = "";
                SaveGL();
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
        if (CheckIsValid())
        {
            if (Save() == 1)
            {
                if (SaveGLMaster())
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

    /// <summary>
    /// S4HanaGLDT07122021
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlGLAccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GLAccSubType();
    }

    /// <summary>
    ///  S4HanaGLDT07122021
    ///  GLAccSubType()
    /// </summary>
    private void GLAccSubType()
    {
        if (ddlGLAccType.SelectedValue == "C")
        {
            ddlGLAccSubType.Enabled = true;
            reqddlGLAccSubType.Visible = true;
            reqddlGLAccSubType.Enabled = true;
            lableddlGLAccSubType.Visible = true;
        }
        else
        {
            ddlGLAccSubType.Enabled = false;
            reqddlGLAccSubType.Visible = false;
            reqddlGLAccSubType.Enabled = false;
            lableddlGLAccSubType.Visible = false;
        }

        if (ddlGLAccType.SelectedValue == "S" || ddlGLAccType.SelectedValue == "P")
        {
            ddlCostElementCategory.Enabled = true;
            reqddlCostElementCategory.Visible = true;
            reqddlCostElementCategory.Enabled = true;
            labelddlCostElementCategory.Visible = true;
        } 
        else
        {
            ddlCostElementCategory.Enabled = false;
            reqddlCostElementCategory.Visible = false;
            reqddlCostElementCategory.Enabled = false;
            labelddlCostElementCategory.Visible = false;
        }

    }

    protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setDisplayCEC();
    }
    protected void ddlAccGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        setDisplayCEC();
    }

    #endregion

    #region Public Method

    public void setDisplayCEC()
    {
        //if (Session[StaticKeys.SelectedModuleId].ToString() == "205" || Session[StaticKeys.SelectedModuleId].ToString() == "215" || Session[StaticKeys.SelectedModuleId].ToString() == "203")
        
        //SDT17052019 Commented By NR  
           //if (ddlAccGroup.SelectedValue == "206" || ddlAccGroup.SelectedValue == "213" || ddlAccGroup.SelectedValue == "204")
            //EDT17052019 Commented By NR  
            //SDT17052019 Change By NR , Desc : Get Module ID from web config
             if (ddlAccGroup.SelectedValue == Convert.ToString(ConfigurationManager.AppSettings["ModuleCONS"]) || ddlAccGroup.SelectedValue == Convert.ToString(ConfigurationManager.AppSettings["ModuleEXPN"]) || ddlAccGroup.SelectedValue == Convert.ToString(ConfigurationManager.AppSettings["ModuleREVN"]))
            {
            //S4HanaGLDT07122021
            //if (ddlAccType.SelectedIndex == 1)
            //{
            //    ddlCostElementCategory.Enabled = true;
            //    reqddlCostElementCategory.Enabled = true;
            //    labelddlCostElementCategory.Visible = true;
            //}
            //else
            //{
            //    ddlCostElementCategory.Enabled = false;
            //    reqddlCostElementCategory.Enabled = false;
            //    labelddlCostElementCategory.Visible = false;
            //    ddlCostElementCategory.SelectedValue = "0";
            //}
            //S4HanaGLDT07122021
            //S4HanaGLDT07122021
            //ddlCostElementCategory.Enabled = false;
            //reqddlCostElementCategory.Enabled = false;
            //labelddlCostElementCategory.Visible = false;
            //ddlCostElementCategory.SelectedValue = "0";
            //S4HanaGLDT07122021
        }
        else
        {
            //ddlCostElementCategory.Enabled = false;
            //reqddlCostElementCategory.Enabled = false;
            //labelddlCostElementCategory.Visible = false;
            //ddlCostElementCategory.SelectedValue = "0";
        }
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlAccGroup, "pr_GetGLAccGrpList 0", "Module_Name", "Module_Id");
        helperAccess.PopuplateDropDownList(ddlLang1, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlLang2, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlRefCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlCostElementCategory, "pr_Get_Cost_Element_Category_Data", "CostElementCategory", "ID");
        ddlCompanyCode.SelectedValue = "32";
    }

    private void FillGeneralData()
    {
        GLCreate1 ObjGLMaster = GetGLMasterData();
        if (ObjGLMaster.ID > 0)
        {
            lblMasterHeaderId.Text = ObjGLMaster.Master_Header_Id.ToString();
            ddlCompanyCode.SelectedValue = ObjGLMaster.Company_Code.ToString();
            ddlAccGroup.SelectedValue = ObjGLMaster.Account_Group.ToString();
            txtGLCode.Text = ObjGLMaster.GL_Code.ToString();
            txtRefGLCode.Text = ObjGLMaster.Ref_GL_Code.ToString();
            ////S4HanaGLDT07122021
            //ddlAccType.Text = ObjGLMaster.PnL_BalanceSheet.ToString();
            //ddlLineItemDisplay.Text = ObjGLMaster.Line_Item_Display.ToString();
            ////S4HanaGLDT07122021



            txtShortText.Text = ObjGLMaster.Short_Text.ToString();
            txtLongText.Text = ObjGLMaster.GL_Acct_Long_Text.ToString();
            ddlLang1.Text = ObjGLMaster.Language1.ToString();
            ddlLang2.Text = ObjGLMaster.Language2.ToString();
            ddlRecAcc.Text = ObjGLMaster.Rec_Account.ToString();
            ddlOpenItemMgmt.Text = ObjGLMaster.Open_Item_Management.ToString();
            
            txtReason.Text = ObjGLMaster.Reason_For_Creation.ToString();
            txtRemarks.Text = ObjGLMaster.Remarks;
            ddlCostElementCategory.SelectedValue = ObjGLMaster.CostElementCategory;
            ddlRefCompanyCode.SelectedValue = ObjGLMaster.Ref_Company_Code;
            //S4HanaGLDT07122021
            ddlGLAccType.Text = ObjGLMaster.GLAccType.ToString();
            ddlGLAccSubType.Text = ObjGLMaster.GLAccSubType.ToString();
            ddlClearSpectoLedgerGPS.Text = ObjGLMaster.ClearSpectoLedgerGPS.ToString();
            GLAccSubType();
            //S4HanaGLDT07122021 
            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        else
        {
            //S4HanaGLDT07122021
            if (ObjGLMaster.Master_Header_Id > 0)
            { 
                ddlCompanyCode.SelectedValue = ObjGLMaster.Company_Code.ToString();
            }
            //S4HanaGLDT07122021

            ddlAccGroup.SelectedValue = (Session[StaticKeys.SelectedModuleId]).ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjGLMaster.ModulePlantGroupCode;
        }
        if (ddlLang1.SelectedValue == "" || ddlLang2.SelectedValue == "")
        {
            ddlLang1.SelectedValue = "E";
            ddlLang2.SelectedValue = "E";
        }

        //SDT17052019 Commented By NR  
        //if (Session[StaticKeys.SelectedModuleId].ToString() == "206" || Session[StaticKeys.SelectedModuleId].ToString() == "213" || Session[StaticKeys.SelectedModuleId].ToString() == "204")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Module ID from web config
        if (Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleCONS"]) || Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleEXPN"]) || Session[StaticKeys.SelectedModuleId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["ModuleREVN"]))
        {
            //S4HanaGLDT07122021
            //if (ddlAccType.SelectedIndex == 1)
            //{
            //    ddlCostElementCategory.Enabled = true;
            //    reqddlCostElementCategory.Enabled = true;
            //    labelddlCostElementCategory.Visible = true;
            //}
            //else
            //{
            //    ddlCostElementCategory.Enabled = false;
            //    reqddlCostElementCategory.Enabled = false;
            //    labelddlCostElementCategory.Visible = false;
            //}
            //S4HanaGLDT07122021
            //S4HanaGLDT07122021
            //ddlCostElementCategory.Enabled = false;
            //reqddlCostElementCategory.Enabled = false;
            //labelddlCostElementCategory.Visible = false;
            //S4HanaGLDT07122021

        }
        else
        {
            //ddlCostElementCategory.Enabled = false;
            //reqddlCostElementCategory.Enabled = false;
            //labelddlCostElementCategory.Visible = false;
        }


        GLAccSubType();
    }

    private void ConfigureControl()
    {
        ddlCompanyCode.Enabled = false;
        //ddlAccGroup.Enabled = false;

        //SDT17052019 Commented By NR  
        // if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "44" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        //EDT17052019 Commented By NR  
        //SDT17052019 Change By NR , Desc : Get Department ID from web config
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["DEPSME"]) || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        {
            txtGLCode.Enabled = true;
            //txtRefGLCode.Enabled = true;
        }

        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "4" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["DEPSME"]))
        {
            txtRemarks.Enabled = false;
            txtReason.Enabled = false;
            //txtRefGLCode.Enabled = true;
        }
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        {
            txtRefGLCode.Enabled = false;
            ddlAccGroup.Enabled = false;
            ddlRefCompanyCode.Enabled = false;
            ddlCompanyCode.Enabled = false;
            ////S4HanaGLDT07122021
            //ddlAccType.Enabled = false;
            //ddlLineItemDisplay.Enabled = false;
            ////S4HanaGLDT07122021

            //S4HanaGLDT07122021
            ddlGLAccType.Enabled = false;
            ddlGLAccSubType.Enabled = false;
            ddlClearSpectoLedgerGPS.Enabled = false;
            //S4HanaGLDT07122021

            ddlCostElementCategory.Enabled = false;
            txtShortText.Enabled = false;
            txtLongText.Enabled = false;
            ddlRecAcc.Enabled = false;
            ddlOpenItemMgmt.Enabled = false;
            
            txtReason.Enabled = false;
            txtRemarks.Enabled = false;
            file_upload.Enabled = false;
            ddlCostElementCategory.Enabled = false;
        }

        //8400000241 GL Start 
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0" &&  Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "")
        {
            txtGLCode.Enabled = true;
            txtRefGLCode.Enabled = true;
            ddlCompanyCode.Enabled = false;
            ddlRefCompanyCode.Enabled = false;
            ddlAccGroup.Enabled = false;
            ddlGLAccType.Enabled = false;
            ddlCostElementCategory.Enabled = false;
            ddlGLAccSubType.Enabled = false;
            txtShortText.Enabled = false;
            ddlLang1.Enabled = false;
            txtLongText.Enabled = false;
            ddlLang2.Enabled = false; 
            ddlRecAcc.Enabled = false;
            ddlOpenItemMgmt.Enabled = false;
            ddlClearSpectoLedgerGPS.Enabled = false;
            txtReason.Enabled = false;
            txtRemarks.Enabled = false;
            file_upload.Enabled = false; 
        }
        else
        {
            txtGLCode.Enabled = false;
            txtRefGLCode.Enabled = true;
            ddlCompanyCode.Enabled = false;
            ddlRefCompanyCode.Enabled = true;
            ddlAccGroup.Enabled = true;
            ddlGLAccType.Enabled = true;
            ddlCostElementCategory.Enabled = true;
            ddlGLAccSubType.Enabled = true;
            txtShortText.Enabled = true;
            ddlLang1.Enabled = false;
            txtLongText.Enabled = true;
            ddlLang2.Enabled = false;
            ddlRecAcc.Enabled = true;
            ddlOpenItemMgmt.Enabled = true;
            ddlClearSpectoLedgerGPS.Enabled = true;
            txtReason.Enabled = true;
            txtRemarks.Enabled = true;
            file_upload.Enabled = true; 
        }
        //8400000241 GL Start

    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (lblActionType.Text != "C")
            flag = true;
        else
        {
            if (ddlCompanyCode.SelectedValue != "0")
                flag = true;
            else if (ddlAccGroup.SelectedValue != "0")
                flag = true;

            //else if (ddlAccType.SelectedValue != "0")//S4HanaGLDT07122021
            //    flag = true;
            //else if (ddlLineItemDisplay.SelectedValue != "")
            //    flag = true;//S4HanaGLDT07122021


            else if (ddlGLAccType.SelectedValue != "")//S4HanaGLDT07122021
                flag = true;
            else if (ddlGLAccSubType.SelectedValue != "")
                flag = true;
            else if (ddlClearSpectoLedgerGPS.SelectedValue != "")//S4HanaGLDT07122021
                flag = true;


            else if (txtShortText.Text != "")
                flag = true;
            else if (txtLongText.Text != "")
                flag = true;
            else if (ddlRecAcc.SelectedValue != "")
                flag = true;
            else if (ddlOpenItemMgmt.SelectedValue != "")
                flag = true;
            
            else if (txtReason.Text != "0")
                flag = true;
            else if (txtRemarks.Text != "")
                flag = true;
        }
        return flag;
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

    private bool SaveGLMaster()
    {
        bool Flag = false;
        GLCreate1 ObjGLMaster = GetControlsValue();

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if ((ObjGLMasterAccess.Save(ObjGLMaster) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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

    private GLCreate1 GetGLMasterData()
    {
        return ObjGLMasterAccess.GetGLMasterData(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private GLCreate1 GetControlsValue()
    {
        GLCreate1 ObjGLMaster = new GLCreate1();
        Utility objUtil = new Utility();

        ObjGLMaster.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjGLMaster.GL_Code = txtGLCode.Text;
        ObjGLMaster.Ref_GL_Code = txtRefGLCode.Text;
        ObjGLMaster.Company_Code = ddlCompanyCode.SelectedValue;
        ObjGLMaster.Account_Group = ddlAccGroup.SelectedValue;
        ////S4HanaGLDT07122021
        //ObjGLMaster.PnL_BalanceSheet = ddlAccType.SelectedValue;
        //ObjGLMaster.Line_Item_Display = ddlLineItemDisplay.SelectedValue;
        ////S4HanaGLDT07122021

        //S4HanaGLDT07122021
        ObjGLMaster.GLAccType = ddlGLAccType.SelectedValue;
        ObjGLMaster.GLAccSubType = ddlGLAccSubType.SelectedValue;
        ObjGLMaster.ClearSpectoLedgerGPS = ddlClearSpectoLedgerGPS.SelectedValue;
        //S4HanaGLDT07122021

        ObjGLMaster.Short_Text = txtShortText.Text;
        ObjGLMaster.GL_Acct_Long_Text = txtLongText.Text;
        ObjGLMaster.Language1 = ddlLang1.SelectedValue;
        ObjGLMaster.Language2 = ddlLang2.SelectedValue;
        ObjGLMaster.Rec_Account = ddlRecAcc.SelectedValue;
        ObjGLMaster.Open_Item_Management = ddlOpenItemMgmt.SelectedValue;
        
        ObjGLMaster.Reason_For_Creation = txtReason.Text;
        ObjGLMaster.Remarks = txtRemarks.Text;
        ObjGLMaster.Ref_Company_Code = ddlRefCompanyCode.SelectedValue;
        ObjGLMaster.CostElementCategory = ddlCostElementCategory.SelectedValue;
        ObjGLMaster.UserId = lblUserId.Text;
        ObjGLMaster.IPAddress = objUtil.GetIpAddress();

        return ObjGLMaster;
    }

    public void SaveGL()
    {
        if (Save() == 1)
        {
            if (SaveGLMaster())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                BindAttachedDocuments(lblMasterHeaderId.Text);

                Response.Redirect("GLCreate.aspx");
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