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

public partial class Transaction_Vendor_VendorChange : System.Web.UI.Page
{

    VendorChangeAccess ObjVendorChangeAccess = new VendorChangeAccess();
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
                        //lnkAddNewVendor.Visible = true;
                        lnkAddNew.Visible = true;
                        isEditable = true;
                    }

                    BindVendorChangeData();
                    BindAttachedDocuments(lblMasterHeaderId.Text);

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        grdAttachedDocs.Columns[1].Visible = true;
                        file_upload.Visible = true;
                        lnkAddNew.Visible = true;
                        grvVendorChange.Columns[6].Visible = true;
                    }
                    else
                    {
                        grdAttachedDocs.Columns[1].Visible = false;
                        file_upload.Visible = false;
                        //Added by Swati for Vendor Excel Upload on 12.12.2018

                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            ExcelDownload1.Visible = true;
                            VendorChangeExcelUpload1.Visible = false;
                        }
                        //End Change
                    }
                }
                else
                {
                    Response.Redirect("VendorMaster.aspx");
                }
            }
            else
            {
                if (Request.Form["__EVENTTARGET"] != null)
                {

                    string Ctrl = Request.Form["__EVENTTARGET"].ToString().Replace('_', '$');
                    string[] Ctrllst = Ctrl.Split('$');
                    if (Ctrllst[Ctrllst.Length - 1] == "ddlBankCountrykeyBNK" || Ctrllst[Ctrllst.Length - 1] == "txtBankKey")
                    {
                        ModalPopupExtender.Show();
                    }
                }
            }
            //Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification
            pnlWarning.CssClass = "warning";
            lblWarning.Text = "Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.";
            //End
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SaveVendorChangeData())
        {
            ModalPopupExtender.Hide();

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            BindVendorChangeData();
        }
        else
        {
            ModalPopupExtender.Show();
        }
    }

    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        if (SaveVendorChangeData())
        {
            ModalPopupExtender.Hide();

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            BindVendorChangeData();
        }
        else
        {
            ModalPopupExtender.Show();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton lnkEditValue = (ImageButton)sender;

        lblVendorChangeDetailId.Text = lnkEditValue.CommandArgument;
        FillVendorChangeDetailData();
        lblVendorChangeAction.Text = "E";
        ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = (ImageButton)sender;

        ObjVendorChangeAccess.DeleteVendorChangeDetail(btnDelete.CommandArgument);
        BindVendorChangeData();
    }

    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        ImageButton lnkAddValue = (ImageButton)sender;
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        lblVendorChangeDetailId.Text = "0";
        lblVendorChangeAction.Text = "F";
        lblVendorChange.Text = lnkAddValue.CommandArgument;
        FillVendorChangeDetailData();
        ModalPopupExtender.Show();

    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        lblVendorChange.Text = "0";
        lblVendorChangeAction.Text = "V";
        lblVendorChangeDetailId.Text = "0";
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        FillVendorChangeData();

        ModalPopupExtender.Show();
    }

    protected void txtVendorCode_TextChanged(object sender, EventArgs e)
    {
        txtVendorCode.Text = txtVendorCode.Text.ToUpper();
        string str = txtVendorCode.Text.Substring(0, 1).ToUpper();
        string str1 = txtVendorCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtVendorCode.Text);
        //8400000388 S
        if (str1 == "LP")
        {
            str = "H";
        }
        //8400000388 E
        switch (str)
        { 
            //8400000388 S
            case "H":
                regtxtVendorCode.ValidationExpression = "^[\\S]{4,10}$";
                ddlVendorAccGrp.SelectedValue = "241";
                break;
            //8400000388 E
            case "L":
                regtxtVendorCode.ValidationExpression = "^[\\S]{4}$";
                ddlVendorAccGrp.SelectedValue = "26";
                break;
            case "E":
                regtxtVendorCode.ValidationExpression = "^[\\S]{7,10}$";
                ddlVendorAccGrp.SelectedValue = "27";

                break;

            default:
                regtxtVendorCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 150000) //1-Series
                    ddlVendorAccGrp.SelectedValue = "18";
                else if (strcode >= 150000 && strcode < 200000)//15-series
                    ddlVendorAccGrp.SelectedValue = "19";
                else if (strcode >= 200000 && strcode < 250000)//2-series
                    ddlVendorAccGrp.SelectedValue = "20";
                else if (strcode >= 250000 && strcode < 300000)//25-series
                    ddlVendorAccGrp.SelectedValue = "21";
                else if (strcode >= 300000 && strcode < 350000)//3-series
                    ddlVendorAccGrp.SelectedValue = "22";
                else if (strcode >= 400000 && strcode < 450000)//4-Series
                    ddlVendorAccGrp.SelectedValue = "24";
                else if (strcode >= 450000 && strcode < 500000)//45-Series
                    ddlVendorAccGrp.SelectedValue = "25";
                else if ((strcode >= 600000 && strcode < 650000) || (strcode >= 678001 && strcode < 699999))//6-Series & 67-series
                    ddlVendorAccGrp.SelectedValue = "28";
                else if (strcode >= 650000 && strcode < 700000)//65-Series
                    ddlVendorAccGrp.SelectedValue = "30";
                else if (strcode >= 720000 && strcode < 730000)//72-Series
                    ddlVendorAccGrp.SelectedValue = "32";
                else if (strcode >= 900000 && strcode < 999999)//9-Series
                    ddlVendorAccGrp.SelectedValue = "15";
                //GST changes start
                else if (strcode >= 500000 && strcode < 599999)//5-Series Z068
                    ddlVendorAccGrp.SelectedValue = "194";
                //GST changes end
                break;
        }
        divmainPopUp.Attributes.Add("style", "display:block");
        divValidationModulePopUp.Attributes.Add("style", "display:none");
        //425143
        divEmpValidationModulePopUp.Attributes.Add("style", "display:none");
        //425143
        helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        ModalPopupExtender.Show();
        //if (ddlVendorAccGroupC.SelectedValue != "26" && ddlVendorAccGroupC.SelectedValue != "27")
        //{
        //string message = "alert('Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.')";
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
        //}
        txtVendorName.Focus();
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

                Response.Redirect("VendorChange.aspx");
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
        //
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
                //Changed By Swati Mohandas DT 03.01.2018
                //if (objDb.DeleteRecord("Vendor_Documents", "Document_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                //End Change
                {
                    System.IO.File.Delete(Server.MapPath("VendorDocuments") + "/" + lblUploadedFileName.Text);
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

    protected void ddlVendorAccGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        ModalPopupExtender.Show();
        //if (ddlVendorAccGroupC.SelectedValue != "26" && ddlVendorAccGroupC.SelectedValue != "27")
        //{
        //string message = "alert('Please note that Vendor Creation activities will not be available between Mar 23rd - Mar 25th 2019 for SAP Ariba SLP go-live activities.')";
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
        //}
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        ModalPopupExtender.Show();
        ddlField.Focus();
    }

    protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlField.SelectedValue == "501")
        {
            EnableBank(true);
            ddlBankCountrykey.SelectedValue = "1";
        }
        else
        {
            EnableBank(false);
        }
        ModalPopupExtender.Show();
    }

    protected void grvVendorChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblVendorChangeId = (Label)e.Row.FindControl("lblVendorChangeId");

            GridView grvVendorChangeDtl = (GridView)e.Row.FindControl("grvVendorChangeDtl");
            bindgrvVendorChangeDtl(Convert.ToInt32(lblVendorChangeId.Text), grvVendorChangeDtl); //Bind the child gridvie here ..
            grvVendorChangeDtl.Columns[6].Visible = lnkAddNew.Visible;
        }
    }

    protected void BtnBankSave_Click(object sender, EventArgs e)
    {
        ucBankMaster1.RefMhId = lblMasterHeaderId.Text;
        if (ucBankMaster1.Save())
        {

            string BankMhId = ucBankMaster1.BankMhId;
            ucBankMaster1.ApproveRequest();
            lblMsg.Text = "Bank Data Entered Sucessfully";
            pnlMsg.CssClass = "sucess";
            pnlMsg.Visible = true;

            BankMasterAccess ObjBankMasterAccess = new BankMasterAccess();
            BankMaster ObjBankMaster = ObjBankMasterAccess.GetBankMasterByMasterHeaderId(BankMhId);
            ddlBankCountrykey.SelectedValue = ObjBankMaster.Country_Id.ToString();
            helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlBankCountrykey.SelectedValue, "Region_Name", "Region_Id");

            if (ObjBankMaster.Bank_Id > 0)
            {
                txtBankKey.Text = ObjBankMaster.Swift;
                AssignBankValues(ObjBankMaster);
            }

            pnlNewBank.Visible = false;
            trBank.Visible = false;
        }
        ModalPopupExtender.Show();
    }

    protected void btnBankCancel_Click(object sender, EventArgs e)
    {
        ucBankMaster1.ClearData();
        pnlNewBank.Visible = false;
        trBank.Visible = false;

        txtBankKey.Focus();
        ModalPopupExtender.Show();

    }

    protected void lnkAddBank_Click(object sender, EventArgs e)
    {
        ucBankMaster1.RefMhId = lblMasterHeaderId.Text;
        ucBankMaster1.SetStartData();
        pnlNewBank.Visible = true;
        trBank.Visible = true;
        ModalPopupExtender.Show();
    }

    protected void txtBankKey_TextChanged(object sender, EventArgs e)
    {
        txtBankName.Text = "";
        txtHouseNoStreet.Text = "";
        txtBankNo.Text = "";

        if (txtBankKey.Text != "")
        {
            BankMasterAccess ObjBankMasterAccess = new BankMasterAccess();
            BankMaster ObjBankMaster = ObjBankMasterAccess.GetBankMasterBySwift(txtBankKey.Text);
            if (ObjBankMaster.Swift == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Invalid Input.');", true);
                ObjBankMaster = null;
            }
            AssignBankValues(ObjBankMaster);
        }
        ModalPopupExtender.Show();
    }

    protected void btnbackMsg_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowCreateNewDialog();", true);
        //http://lupin.procurement.ariba.com/
        //string url = "http://lupin.procurement.ariba.com/";

        //string s = "window.open('" + url + "', 'popup_window', 'width=900,height=900,left=100,top=100,resizable=yes');";

        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        Server.Transfer("http://lupin.procurement.ariba.com/");
    }


    #endregion

    #region Public Methods

    private void EnableBank(bool IsVisble)
    {
        tdNew.Visible = !IsVisble;
        tdNew1.Visible = !IsVisble;
        tdbank.Visible = IsVisble;
        tdbank1.Visible = IsVisble;
        tdbank2.Visible = IsVisble;
        tdbank3.Visible = IsVisble;
        tdbank4.Visible = IsVisble;
        tdbank5.Visible = IsVisble;
        tdbank6.Visible = IsVisble;
        lnkAddBank.Visible = IsVisble;
        if (!IsVisble)
            AssignBankValues(null);
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlPurchaseOrg, "pr_GetPurchaseOrgList 0", "Purchase_Org_Name", "Purchase_Org_Id");
        helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
        helperAccess.PopuplateDropDownList(ddlBankCountrykey, "pr_Get_CountryList", "Country_Name", "Country_Id");
        ddlBankCountrykey.SelectedValue = "1";
        ddlCompanyCode.SelectedValue = "32";

        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

        DataSet ds;
        ds = objMatAccess.ReadModules("V");


        ddlVendorAccGrp.DataSource = ds;
        ddlVendorAccGrp.DataTextField = "Module_Name";
        ddlVendorAccGrp.DataValueField = "Module_Id";
        ddlVendorAccGrp.DataBind();

        ddlVendorAccGrp.Items.Insert(0, new ListItem("---Select---", "0"));

    }

    protected void AssignBankValues(BankMaster ObjBankMaster)
    {
        if (ObjBankMaster != null)
        {
            ddlBankCountrykey.SelectedValue = ObjBankMaster.Country_Id.ToString();
            helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlBankCountrykey.SelectedValue, "Region_Name", "Region_Id");
            ddlRegion.SelectedValue = ObjBankMaster.Region_Id.ToString();
            txtBankName.Text = ObjBankMaster.Bank_Name;
            txtHouseNoStreet.Text = ObjBankMaster.House_No_Street;
            txtBankNo.Text = ObjBankMaster.Bank_Number;
        }
        else
        {
            txtBankKey.Text = "";
            ddlRegion.SelectedValue = "0";
            txtBankName.Text = "";
            txtHouseNoStreet.Text = "";
            txtBankNo.Text = "";
        }
    }

    protected bool SaveVendorChangeData()
    {
        bool Flag = false;
        VendorChange ObjVendorChange = GetControlsValue();

        try
        {
            if (!CheckDuplicateVendor())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Start Change By Swati For ARIBA Change DB Config on 28.03.2019
                    DataSet ds = ObjVendorChangeAccess.GetStatus();
                    //string Status = ds.Tables[0].Rows[0]["Status"].ToString().ToLower() == "true" ? "1" : "0";
                    //if (Status == "1")
                    //{
                    //if (ddlVendorAccGrp.SelectedValue != "15" && ddlVendorAccGrp.SelectedValue != "26" && ddlVendorAccGrp.SelectedValue != "27")
                    if (ddlVendorAccGrp.SelectedValue == "27")
                    {
                        divmainPopUp.Attributes.Add("style", "display:none");
                        divValidationModulePopUp.Attributes.Add("style", "display:none");
                        //425143
                        divEmpValidationModulePopUp.Attributes.Add("style", "display:block");
                        //425143
                        ModalPopupExtender.Show();
                    }
                    //8400000388 Added ddlVendorAccGrp.SelectedValue != "241"
                    else if (ddlVendorAccGrp.SelectedValue != "15" && ddlVendorAccGrp.SelectedValue != "26" && ddlVendorAccGrp.SelectedValue != "241")
                    {
                        divmainPopUp.Attributes.Add("style", "display:none");
                        divValidationModulePopUp.Attributes.Add("style", "display:block");
                        //425143
                        divEmpValidationModulePopUp.Attributes.Add("style", "display:none");
                        //425143
                        ModalPopupExtender.Show();
                    }
                    else
                    {
                        if (ObjVendorChangeAccess.Save(ObjVendorChange) > 0)
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
                    //}
                    ////End Change
                    //else
                    //{
                    //    if (ObjVendorChangeAccess.Save(ObjVendorChange) > 0)
                    //    {
                    //        scope.Complete();
                    //        Flag = true;
                    //    }
                    //    else
                    //    {
                    //        lblMsg1.Text = Messages.GetMessage(-1);
                    //        pnlMsg1.CssClass = "error";
                    //        pnlMsg1.Visible = true;
                    //    }
                    //}
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

    private bool CheckDuplicateVendor()
    {
        bool flg = false;



        if (lblVendorChangeAction.Text == "F")
        {
            foreach (GridViewRow gr in grvVendorChange.Rows)
            {
                if (txtVendorCode.Text == gr.Cells[1].Text)
                {
                    GridView grvVendorChangeDtl = (GridView)gr.FindControl("grvVendorChangeDtl");
                    foreach (GridViewRow gr1 in grvVendorChangeDtl.Rows)
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
        else if (lblVendorChangeAction.Text == "V")
        {
            foreach (GridViewRow gr in grvVendorChange.Rows)
            {
                if (txtVendorCode.Text == gr.Cells[1].Text)
                {
                    flg = true;
                }
            }
            if (flg)
            {
                lblMsg1.Text = "Duplicate Vendor. To enter more fields for the same vendor Click the '+' i front of the vendor.";
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

    private void BindVendorChangeData()
    {
        grvVendorChange.DataSource = ObjVendorChangeAccess.GetVendorChangeData(lblMasterHeaderId.Text);
        grvVendorChange.DataBind();
    }

    private void bindgrvVendorChangeDtl(int VendorChangeId, GridView grvVendorChangeDtl)
    {
        grvVendorChangeDtl.DataSource = ObjVendorChangeAccess.GetVendorChangeDetailData(VendorChangeId);
        grvVendorChangeDtl.DataBind();
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
                    //New WF change for Vendor Change Req when Vendor code is 9 series by Swati on 08/08/18

                    DataSet dset = new DataSet();
                    dset = ObjVendorChangeAccess.GetVendorCode(Session[StaticKeys.MasterHeaderId].ToString());
                    if (dset.Tables[0].Rows.Count > 0)
                    {
                        txtVendorCode.Text = dset.Tables[0].Rows[0]["Customer_Code"].ToString().ToUpper();//txtVendorCode.Text.ToUpper();

                        string str = txtVendorCode.Text.Substring(0, 1).ToUpper();
                        string str1 = txtVendorCode.Text.Substring(0, 2);
                        int strcode = SafeTypeHandling.ConvertStringToInt32(txtVendorCode.Text);
                        if (strcode >= 900000 && strcode < 999999)
                        {
                            int masterHeaderId = ObjVendorChangeAccess.SaveVendorHeaderVM(Session[StaticKeys.MasterHeaderId].ToString(), ddlVendorAccGrp.SelectedValue);

                        }

                    }
                    //end 
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
            boolFlg = SaveVendorChange();
        }
        return boolFlg;
    }

    private bool SaveVendorChange()
    {
        bool Flag = false;
        //VendorChange ObjVendorChange = GetControlsValue();

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //Start Change By Swati For ARIBA Change DB Config on 28.03.2019
                //DataSet ds = ObjVendorChangeAccess.GetStatus();
                //string Status = ds.Tables[0].Rows[0]["Status"].ToString().ToLower() == "true" ? "1" : "0";
                //if (Status == "1")
                //{
                //if (ddlVendorAccGrp.SelectedValue != "15" && ddlVendorAccGrp.SelectedValue != "26" && ddlVendorAccGrp.SelectedValue != "27")
                //{
                //    divmainPopUp.Attributes.Add("style", "display:none");
                //    divValidationModulePopUp.Attributes.Add("style", "display:block");
                //    ModalPopupExtender.Show();
                //}
                //else
                //{
                //if ((ObjVendorChangeAccess.Save(ObjVendorChange) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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
                //}
                //}
                ////End Change
                //else
                //{
                //    //if ((ObjVendorChangeAccess.Save(ObjVendorChange) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
                //    if (SaveDocuments(lblMasterHeaderId.Text))
                //    {
                //        scope.Complete();
                //        Flag = true;
                //    }
                //    else
                //    {
                //        lblMsg.Text = Messages.GetMessage(-1);
                //        pnlMsg.CssClass = "error";
                //        pnlMsg.Visible = true;
                //    }
                //}
            }
            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Flag;
    }

    private VendorChange GetVendorChange()
    {
        return ObjVendorChangeAccess.GetVendorChange(Convert.ToInt32(lblVendorChange.Text));
    }

    private VendorChangeDetail GetVendorChangeDetail()
    {
        return ObjVendorChangeAccess.GetVendorChangeDetail(Convert.ToInt32(lblVendorChangeDetailId.Text));
    }

    private VendorChange GetControlsValue()
    {
        VendorChange ObjVendorChange = new VendorChange();
        Utility objUtil = new Utility();


        ObjVendorChange.Vendor_Change_Id = Convert.ToInt32(lblVendorChange.Text);
        ObjVendorChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjVendorChange.Customer_Code = txtVendorCode.Text;
        ObjVendorChange.Company_Code = ddlCompanyCode.SelectedValue;
        ObjVendorChange.Vendor_Group = ddlVendorAccGrp.SelectedValue;
        ObjVendorChange.Purchase_Org = ddlPurchaseOrg.SelectedValue;
        ObjVendorChange.Vendor_Desc = txtVendorName.Text;

        ObjVendorChange.Vendor_Change_Detail_Id = Convert.ToInt32(lblVendorChangeDetailId.Text);
        ObjVendorChange.Section_Id = Convert.ToInt32(ddlSection.SelectedValue);
        ObjVendorChange.Section_Feild_Master_Id = Convert.ToInt32(ddlField.SelectedValue);
        ObjVendorChange.Old_Value = txtOldValue.Text;
        ObjVendorChange.Remarks = txtRemarks.Text;
        if (ddlField.SelectedValue == "501")
        {
            ObjVendorChange.New_Value = txtBankKey.Text;


        }
        else
        {
            ObjVendorChange.New_Value = txtNewValue.Text;
        }

        ObjVendorChange.IsActive = 1;
        ObjVendorChange.UserId = lblUserId.Text;
        ObjVendorChange.TodayDate = objUtil.GetDate();
        ObjVendorChange.IPAddress = objUtil.GetIpAddress();

        return ObjVendorChange;
    }

    private void FillVendorChangeData()
    {
        VendorChange ObjVendorChange = GetVendorChange();
        if (ObjVendorChange.Vendor_Change_Id > 0)
        {
            lblVendorChange.Text = ObjVendorChange.Vendor_Change_Id.ToString();
            txtVendorCode.Text = ObjVendorChange.Customer_Code;
            ddlCompanyCode.SelectedValue = ObjVendorChange.Company_Code;
            ddlVendorAccGrp.SelectedValue = ObjVendorChange.Vendor_Group;
            ddlPurchaseOrg.SelectedValue = ObjVendorChange.Purchase_Org;
            txtVendorName.Text = ObjVendorChange.Vendor_Desc;
        }
        else
        {
            ddlCompanyCode.SelectedValue = "32";
            //LH01DT06052021 Comment Start
            //ddlCompanyCode.Enabled = false;
            //LH01DT06052021 Comment End
            txtVendorCode.Text = "";
            ddlVendorAccGrp.SelectedValue = "0";
            ddlPurchaseOrg.SelectedValue = "0";
            txtVendorName.Text = "";
            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

            txtOldValue.Text = "";
            txtNewValue.Text = "";
            txtRemarks.Text = "";
        }
        EnableBank(false);
        MakeDisable(true);
    }

    private void FillVendorChangeDetailData()
    {
        VendorChangeDetail ObjVendorChangeDetail = GetVendorChangeDetail();

        if (ObjVendorChangeDetail.Vendor_Change_Id > 0)
        {

            lblVendorChange.Text = ObjVendorChangeDetail.Vendor_Change_Id.ToString();

            VendorChange objVendorChange = ObjVendorChangeAccess.GetVendorChange(Convert.ToInt32(lblVendorChange.Text));
            txtVendorCode.Text = objVendorChange.Customer_Code;
            lblModuleId.Text = objVendorChange.Vendor_Group;
            ddlCompanyCode.SelectedValue = objVendorChange.Company_Code;
            ddlVendorAccGrp.SelectedValue = objVendorChange.Vendor_Group;
            ddlPurchaseOrg.SelectedValue = objVendorChange.Purchase_Org;
            txtVendorName.Text = objVendorChange.Vendor_Desc;

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
            ddlSection.SelectedValue = ObjVendorChangeDetail.Section_Id.ToString();
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ObjVendorChangeDetail.Section_Id + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            ddlField.SelectedValue = ObjVendorChangeDetail.Section_Feild_Master_Id.ToString();

            txtOldValue.Text = ObjVendorChangeDetail.Old_Value;
            //txtNewValue.Text = ObjVendorChangeDetail.New_Value;
            txtRemarks.Text = ObjVendorChangeDetail.Remarks;

            if (ddlField.SelectedValue == "501")
            {
                txtBankKey.Text = ObjVendorChangeDetail.New_Value;

                BankMasterAccess ObjBankMasterAccess = new BankMasterAccess();
                BankMaster ObjBankMaster = ObjBankMasterAccess.GetBankMasterBySwift(ObjVendorChangeDetail.New_Value);
                ddlBankCountrykey.SelectedValue = ObjBankMaster.Country_Id.ToString();
                helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlBankCountrykey.SelectedValue, "Region_Name", "Region_Id");

                if (ObjBankMaster.Bank_Id > 0)
                {
                    txtBankKey.Text = ObjBankMaster.Swift;
                    AssignBankValues(ObjBankMaster);
                }
                EnableBank(true);
            }
            else
            {
                txtNewValue.Text = ObjVendorChangeDetail.New_Value;
                EnableBank(false);
            }

            MakeDisable(false);

        }
        else
        {

            VendorChange objVendorChange = ObjVendorChangeAccess.GetVendorChange(Convert.ToInt32(lblVendorChange.Text));
            txtVendorCode.Text = objVendorChange.Customer_Code;
            lblModuleId.Text = objVendorChange.Vendor_Group;
            ddlCompanyCode.SelectedValue = objVendorChange.Company_Code;
            ddlVendorAccGrp.SelectedValue = objVendorChange.Vendor_Group;
            ddlPurchaseOrg.SelectedValue = objVendorChange.Purchase_Org;
            txtVendorName.Text = objVendorChange.Vendor_Desc;

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ObjVendorChangeDetail.Section_Id + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

            txtOldValue.Text = "";
            txtNewValue.Text = "";
            txtRemarks.Text = "";
            MakeDisable(false);
            EnableBank(false);
        }

    }

    private void MakeDisable(bool flg)
    {
        txtVendorCode.Enabled = flg;
        //ddlCompanyCode.Enabled = flg;
        //ddlVendorAccGrp.Enabled = flg;
        ddlPurchaseOrg.Enabled = flg;
        txtVendorName.Enabled = flg;
    }

    private bool CheckIsValid()
    {
        bool flg = false;

        if (grvVendorChange.Rows.Count > 0)
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
            lblMsg.Text = "Error While Saving Vendor Details.";
        }


        return flag;
    }

    #endregion


    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://lupin.procurement.ariba.com/");
    }

    protected void btnclose_Click(Object sender, EventArgs e)
    {
        Response.Redirect("VendorChange.aspx");
    }

    protected void btnHRRedirect_Click(Object sender, EventArgs e)
    {
        //Response.Redirect("VendorChange.aspx");
        Response.Redirect("VendorMaster.aspx");
    }
}