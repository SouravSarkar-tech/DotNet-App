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

public partial class Transaction_Vendor_VendorGeneral1 : System.Web.UI.Page
{
    VendorGeneralAccess ObjVendorGeneralAccess = new VendorGeneralAccess();
    HelperAccess helperAccess = new HelperAccess();
    string docType = "";

    #region Page Events

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
                        //Vendor workflow modification start
                        grdAttachedDocs.Columns[1].Visible = true;
                        //grdAttachedDocs.Columns[2].Visible = true;
                        //Vendor workflow modification end
                        file_upload.Visible = true;
                    }
                    else
                    {
                        grdAttachedDocs.Columns[1].Visible = false;
                        file_upload.Visible = false;
                        //Vendor workflow modification start
                        btnUploadDoc.Visible = false;
                        //grdMandDocs.Visible = false;
                        //grdAttachedDocs.Columns[2].Visible = false;
                        //Vendor workflow modification end
                    }
                    ConfigureControl();
                }
                else
                {
                    Response.Redirect("VendorMaster.aspx");
                }
            }
            SetAddressVisible();
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            //if (Save())
            //{
            //    string pageURL = btnPrevious.CommandArgument.ToString();
            //    Response.Redirect(pageURL);
            //}
            if (Save() == 1)
            {
                if (SaveVendorGeneral())
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

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (CheckIsValid())
    //    {
    //        if (Save())
    //        {
    //            lblMsg.Text = Messages.GetMessage(1);
    //            pnlMsg.CssClass = "success";
    //            pnlMsg.Visible = true;
    //            BindAttachedDocuments(lblMasterHeaderId.Text);

    //            Response.Redirect("VendorGeneral1.aspx");
    //        }
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Please fill atleast one feild.";
    //        pnlMsg.Visible = true;
    //        pnlMsg.CssClass = "error";
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            if (Save() == 1)
            {
                if (SaveVendorGeneral())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    BindAttachedDocuments(lblMasterHeaderId.Text);

                    Response.Redirect("VendorGeneral1.aspx");
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

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRegionData();
        ddlRegion.Focus();
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedValue != "0" && ddlRegion.SelectedValue != "#")
        {
            DataSet ds = ObjVendorGeneralAccess.GetVendorDuplicate(Convert.ToInt32(lblMasterHeaderId.Text), "", ddlRegion.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "Vendor in same Region. already created with same PAN.<br />Please check if you still want to continue.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        txtMobileNum.Focus();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            //if (Save())
            //{
            //    string pageURL = btnNext.CommandArgument.ToString();
            //    Response.Redirect(pageURL);
            //}
            if (Save() == 1)
            {
                if (SaveVendorGeneral())
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
                    //string StrPath = "~/Transaction/Vendor/VendorDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
                    string savePath = MapPath(lblUploadedFileName.Text);
                   //System.IO.File.Delete(savePath + "/" + lblUploadedFileName.Text);
                    if (System.IO.File.Exists(savePath))
                        System.IO.File.Delete(savePath);
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

    protected void txtTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMemoValidation();
        if (txtTitle.SelectedValue == "INDI")
        {
            txtMemo.Focus();
        }
        else
        {
            txtName1.Focus();
        }
    }

    protected void grdMandDocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkIsMandat = e.Row.FindControl("chkIsMandat") as CheckBox;

            HiddenField hdnIsMandat = e.Row.FindControl("hdnIsMandat") as HiddenField;

            chkIsMandat.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnIsMandat.Value);

        }
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
                BindAttachedDocuments(lblMasterHeaderId.Text);
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

    #endregion

    #region Private Methods

    private void SetMemoValidation()
    {
        if (txtTitle.SelectedValue == "INDI")
        {
            reqtxtMemo.Enabled = true;
            reqtxtMemo.Visible = true;
            labletxtMemo.Visible = true;
            txtMemo.Enabled = true;
        }
        else
        {
            reqtxtMemo.Enabled = false;
            labletxtMemo.Visible = false;
            txtMemo.Enabled = false;
            txtMemo.Text = "";
        }
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlPurchaseOrg, "pr_GetPurchaseOrgList 0", "Purchase_Org_Name", "Purchase_Org_Id");
        helperAccess.PopuplateDropDownList(ddlVendorAccGrp, "pr_GetVendorAccGrpList 0", "Vendor_Acc_Grp_Name", "Vendor_Acc_Grp_Id");
        helperAccess.PopuplateDropDownList(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry 0", "Region_Name", "Region_Id");
        helperAccess.PopuplateDropDownList(txtTitle, "pr_GetDropDownListByControlNameModuleType 'V','txtTitle'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlVendorCategory, "pr_GetDropDownListByControlNameModuleType 'V','ddlVendorCategory'", "LookUp_Desc", "LookUp_Code", "");

        if (lblActionType.Text == "C")
        {
            helperAccess.AddBlankOption(ddlCountry);
            helperAccess.AddBlankOption(ddlRegion);
            helperAccess.AddBlankOption(txtTitle);
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
    //            if ((fileExtension == ".pdf") || (fileExtension == ".tif") || (fileExtension == ".xls") || (fileExtension == ".xlsx") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png"))
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
    //        lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
    //        pnlMsg.Visible = true;
    //        pnlMsg.CssClass = "error";
    //    }
    //    else
    //    {
    //        boolFlg = SaveVendorGeneral();
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
        //    boolFlg = SaveVendorGeneral();
        //}
        return flg;
    }

    private bool SaveVendorGeneral()
    {
        bool Flag = false;
        VendorGeneral1 ObjVendorGeneral = GetControlsValue();

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if ((ObjVendorGeneralAccess.Save(ObjVendorGeneral) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
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

    private VendorGeneral1 GetVendorGeneral()
    {
        return ObjVendorGeneralAccess.GetVendorGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private VendorGeneral1 GetControlsValue()
    {
        VendorGeneral1 ObjVendorGeneral = new VendorGeneral1();
        Utility objUtil = new Utility();

        ObjVendorGeneral.Vendor_General1_Id = Convert.ToInt32(lblVendorGeneralId.Text);
        ObjVendorGeneral.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjVendorGeneral.Customer_Code = txtCustomerCode.Text;
        ObjVendorGeneral.Company_Code = ddlCompanyCode.SelectedValue;
        ObjVendorGeneral.Vendor_Group = ddlVendorAccGrp.SelectedValue;
        ObjVendorGeneral.Purchase_Org = ddlPurchaseOrg.SelectedValue;
        ObjVendorGeneral.Vendor_Category = ddlVendorCategory.SelectedValue;
        ObjVendorGeneral.Title = txtTitle.SelectedValue;
        ObjVendorGeneral.Memo = txtMemo.Text;
        ObjVendorGeneral.Name1 = txtName1.Text;
        ObjVendorGeneral.Name2 = txtName2.Text;
        ObjVendorGeneral.Name3 = txtName3.Text;
        ObjVendorGeneral.Name4 = txtName4.Text;
        ObjVendorGeneral.Sort_Field = txtSortfield.Text;
        ObjVendorGeneral.HouseNo_Street = txtHouseNo.Text;
        ObjVendorGeneral.Street4 = txtStreet4.Text;
        ObjVendorGeneral.Street5 = txtStreet5.Text;
        ObjVendorGeneral.PO_Box = txtPOBox.Text;
        ObjVendorGeneral.City = txtCity.Text;
        ObjVendorGeneral.Postal_Code = txtPostalCode.Text;
        ObjVendorGeneral.District = txtDistrict.Text;
        ObjVendorGeneral.PO_Box_Postal_Code = txtPOBoxPostal.Text;
        ObjVendorGeneral.CountryKey = ddlCountry.SelectedValue;
        ObjVendorGeneral.Region = ddlRegion.SelectedValue;
        ObjVendorGeneral.LanguageAcc = txtLanguage.Text;
        ObjVendorGeneral.Mobile_Num = txtMobileNum.Text;
        ObjVendorGeneral.Mobile_Num2 = txtMobileNum2.Text;
        ObjVendorGeneral.First_Tele_No = txtFirsttelephone.Text;
        ObjVendorGeneral.Fax_NO = txtFaxNumber.Text;
        ObjVendorGeneral.Second_Tele_No = txtSecondTelephoneNumber.Text;
        ObjVendorGeneral.Email_Address = txtEmailAddress.Text;
        ObjVendorGeneral.Email_Address2 = txtEmailAddress2.Text;
        ObjVendorGeneral.Email_Address3 = txtEmailAddress3.Text;
        ObjVendorGeneral.Autorization_Gr = txtAuthorizationGroup.Text;
        ObjVendorGeneral.Com_Id_TradingPat = txtCompanyIDTrading.Text;
        ObjVendorGeneral.Telex_number = txtTelex_number.Text;
        ObjVendorGeneral.Teletex_number = txtTeletex_Number.Text;
        ObjVendorGeneral.Customer_Number = txtCustomer_Number.Text;
        ObjVendorGeneral.Remarks = txtRemarks.Text;
        //GST Changes Start
        ObjVendorGeneral.SupplyPlace = txtSupplyPlace.Text;
        ObjVendorGeneral.ImpVendor = rdbImpVendor.SelectedValue;
        //GST Changes End
        ObjVendorGeneral.IsActive = 1;
        ObjVendorGeneral.UserId = lblUserId.Text;
        ObjVendorGeneral.TodayDate = objUtil.GetDate();
        ObjVendorGeneral.IPAddress = objUtil.GetIpAddress();

        return ObjVendorGeneral;
    }

    private void BindRegionData()
    {
        string Country = ddlCountry.SelectedValue == "#" ? "0" : ddlCountry.SelectedValue;
        helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + Country, "Region_Name", "Region_Id");

        regtxtPostalCode.ValidationExpression = Postal_Code.GetRegex(Country);
        if (Country == "1")
        {
            regtxtMobileNum.Enabled = true;
            regtxtMobileNum2.Enabled = true;

            labletxtPostalCode.Visible = true;
            reqtxtPostalCode.Visible = true;
            //regtxtPostalCode.Enabled = true;
            //GST Changes Start
            lableddlRegion.Visible = true;
            reqddlRegion.Enabled = true;
            //GST Changes End
        }
        else
        {
            regtxtMobileNum.Enabled = false;
            regtxtMobileNum2.Enabled = false;

            labletxtPostalCode.Visible = false;
            reqtxtPostalCode.Enabled = false;
            //regtxtPostalCode.Enabled = false;
            //GST Changes Start
            lableddlRegion.Visible = false;
            reqddlRegion.Enabled = false;
            //GST Changes End
        }
       
        //GST changes start
        //Srinidhi
        //if (Session[StaticKeys.SelectedModuleId].ToString() == "18" || Session[StaticKeys.SelectedModuleId].ToString() == "19" || Session[StaticKeys.SelectedModuleId].ToString() == "28")
        //{
        //    reqddlRegion.Enabled = false;
        //    lableddlRegion.Visible = false;
        //}
        //Srinidhi 
        //GST changes end
                
        if (Session[StaticKeys.ActionType].ToString() == "C")
        {
            helperAccess.AddBlankOption(ddlRegion);
        }
    }

    private void FillGeneralData()
    {
        VendorGeneral1 ObjVendorGeneral = GetVendorGeneral();
        if (ObjVendorGeneral.Vendor_General1_Id > 0)
        {
            lblVendorGeneralId.Text = ObjVendorGeneral.Vendor_General1_Id.ToString();
            lblMasterHeaderId.Text = ObjVendorGeneral.Master_Header_Id.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjVendorGeneral.ModulePlantGroupCode;
            txtCustomerCode.Text = ObjVendorGeneral.Customer_Code.ToString();
            lblVendorName.Text = ObjVendorGeneral.Vendor_Desc;
            ddlCompanyCode.SelectedValue = ObjVendorGeneral.Company_Code.ToString();
            ddlVendorAccGrp.SelectedValue = ObjVendorGeneral.Vendor_Group.ToString();
            ddlPurchaseOrg.SelectedValue = ObjVendorGeneral.Purchase_Org.ToString();
            ddlVendorCategory.SelectedValue = ObjVendorGeneral.Vendor_Category.ToString();
            txtTitle.SelectedValue = ObjVendorGeneral.Title.ToString();
            txtMemo.Text = ObjVendorGeneral.Memo.ToString();
            SetMemoValidation();
            txtName1.Text = ObjVendorGeneral.Name1.ToString();
            txtName2.Text = ObjVendorGeneral.Name2.ToString();
            txtName3.Text = ObjVendorGeneral.Name3.ToString();
            txtName4.Text = ObjVendorGeneral.Name4.ToString();
            txtSortfield.Text = ObjVendorGeneral.Sort_Field.ToString();
            txtHouseNo.Text = ObjVendorGeneral.HouseNo_Street.ToString();
            txtStreet4.Text = ObjVendorGeneral.Street4.ToString();
            txtStreet5.Text = ObjVendorGeneral.Street5.ToString();
            txtPOBox.Text = ObjVendorGeneral.PO_Box.ToString();
            txtCity.Text = ObjVendorGeneral.City.ToString();
            txtPostalCode.Text = ObjVendorGeneral.Postal_Code.ToString();
            txtDistrict.Text = ObjVendorGeneral.District.ToString();
            txtPOBoxPostal.Text = ObjVendorGeneral.PO_Box_Postal_Code.ToString();
            ddlCountry.SelectedValue = ObjVendorGeneral.CountryKey.ToString() == "" ? "0" : ObjVendorGeneral.CountryKey.ToString();

            BindRegionData();

            ddlRegion.SelectedValue = ObjVendorGeneral.Region.ToString() == "" ? "0" : ObjVendorGeneral.Region.ToString();
            txtLanguage.Text = ObjVendorGeneral.LanguageAcc.ToString();
            txtMobileNum.Text = ObjVendorGeneral.Mobile_Num.ToString();
            txtMobileNum2.Text = ObjVendorGeneral.Mobile_Num2.ToString();
            txtFirsttelephone.Text = ObjVendorGeneral.First_Tele_No.ToString();
            txtFaxNumber.Text = ObjVendorGeneral.Fax_NO.ToString();
            txtSecondTelephoneNumber.Text = ObjVendorGeneral.Second_Tele_No.ToString();
            txtEmailAddress.Text = ObjVendorGeneral.Email_Address;
            txtEmailAddress2.Text = ObjVendorGeneral.Email_Address2;
            txtEmailAddress3.Text = ObjVendorGeneral.Email_Address3;
            txtAuthorizationGroup.Text = ObjVendorGeneral.Autorization_Gr.ToString();
            txtCompanyIDTrading.Text = ObjVendorGeneral.Com_Id_TradingPat.ToString();
            txtTelex_number.Text = ObjVendorGeneral.Telex_number.ToString();
            txtTeletex_Number.Text = ObjVendorGeneral.Teletex_number.ToString();
            txtCustomer_Number.Text = ObjVendorGeneral.Customer_Number.ToString();

            txtRemarks.Text = ObjVendorGeneral.Remarks;
            //GST Changes Start
            txtSupplyPlace.Text = ObjVendorGeneral.SupplyPlace;
            rdbImpVendor.SelectedValue = ObjVendorGeneral.ImpVendor;
            //GST Changes End
            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        else
        {
            lblVendorGeneralId.Text = "0";
            txtCustomerCode.Text = ObjVendorGeneral.Customer_Code.ToString();
            lblVendorName.Text = ObjVendorGeneral.Vendor_Desc;
            ddlCompanyCode.SelectedValue = ObjVendorGeneral.Company_Code.ToString();
            ddlVendorAccGrp.SelectedValue = ObjVendorGeneral.Vendor_Group.ToString();
            SetMemoValidation();

            if (lblActionType.Text != "C")
            {
                ddlCountry.SelectedValue = "1";
                txtLanguage.Text = "E";

                if (ObjVendorGeneral.Company_Code.ToString() == "32" && lblModuleId.Text != "27" && lblModuleId.Text != "30")
                {
                    ddlPurchaseOrg.SelectedValue = "2";
                }
                //Carve_LC17&LC23
                if (ObjVendorGeneral.Company_Code.ToString() == "77" && lblModuleId.Text != "27" && lblModuleId.Text != "30")
                {
                    ddlPurchaseOrg.SelectedValue = "8";
                }
                //Carve_LC17&LC23
                if (lblModuleId.Text == "27")
                {
                    txtCustomerCode.Text = "E00";
                    regtxtCustomerCode.ValidationExpression = "E0[\\S]{5,10}$";
                }
            }
            BindRegionData();
            //txtLanguage.Text = "EN";
            Session[StaticKeys.SelectedModulePlantGrp] = ObjVendorGeneral.ModulePlantGroupCode;
        }

        SetAddressVisible();
        txtTitle.Focus();
        //Vendor workflow modification start
        ////BindMandatoryDocList();
        //Vendor workflow modification end
        //GST changes Start
        if (lblModuleId.Text == "194")
        {
            trSupplyPlace.Visible = true;
            reqtxtSupplyPlace.Enabled = true;
            regtxtSupplyPlace.Enabled = true;
        }
        if (lblModuleId.Text == "18" || lblModuleId.Text == "19")
        {
            trImpVendor.Visible = true;
            reqImpVendor.Enabled = true;
            labelImpVendor.Visible = true;
        }
        //GST changes End
    }

    private void SetAddressVisible()
    {
        txtStreet4.Enabled = txtHouseNo.Text.Length > 0 ? true : false;
        txtStreet5.Enabled = txtStreet4.Text.Length > 0 ? true : false;

        txtName2.Enabled = txtName1.Text.Length > 0 ? true : false;
        txtMobileNum2.Enabled = txtMobileNum.Text.Length > 0 ? true : false;
        txtSecondTelephoneNumber.Enabled = txtFirsttelephone.Text.Length > 0 ? true : false;

        txtEmailAddress2.Enabled = txtEmailAddress.Text.Length > 0 ? true : false;
        txtEmailAddress3.Enabled = txtEmailAddress2.Text.Length > 0 ? true : false;
    }

    private void ConfigureControl()
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Vendor_General obj = new SectionConfiguration.Vendor_General();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));

        ddlCompanyCode.Enabled = false;
        ddlVendorAccGrp.Enabled = false;

        //if (lblActionType.Text == "C")
        //{
        //    pnlRemarks.Visible = true;
        //}
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (lblActionType.Text != "C")
            flag = true;
        else
        {
            if (txtTitle.SelectedValue != "")
                flag = true;
            else if (txtName1.Text != "")
                flag = true;
            else if (txtName2.Text != "")
                flag = true;
            else if (txtMemo.Text != "")
                flag = true;
            else if (txtHouseNo.Text != "")
                flag = true;
            else if (txtStreet4.Text != "")
                flag = true;
            else if (txtStreet5.Text != "")
                flag = true;
            else if (txtSortfield.Text != "")
                flag = true;
            else if (txtCity.Text != "")
                flag = true;
            else if (txtDistrict.Text != "")
                flag = true;
            else if (txtPostalCode.Text != "")
                flag = true;
            else if (ddlCountry.SelectedValue != "0")
                flag = true;
            else if (ddlRegion.SelectedValue != "0")
                flag = true;
            else if (ddlPurchaseOrg.SelectedValue != "0")
                flag = true;
            else if (txtMobileNum.Text != "")
                flag = true;
            else if (txtMobileNum2.Text != "")
                flag = true;
            else if (txtFirsttelephone.Text != "")
                flag = true;
            else if (txtSecondTelephoneNumber.Text != "")
                flag = true;
            else if (txtFaxNumber.Text != "")
                flag = true;
            else if (txtTelex_number.Text != "")
                flag = true;
            else if (txtEmailAddress.Text != "")
                flag = true;
            else if (txtEmailAddress2.Text != "")
                flag = true;
            else if (txtEmailAddress3.Text != "")
                flag = true;
            else if (txtCustomer_Number.Text != "")
                flag = true;
            else if (txtRemarks.Text != "")
                flag = true;
        }

        return flag;
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

        Random sufix1 = new Random();
        string sufix = sufix1.NextDouble().ToString().Replace(".","");

        //Vendor workflow modification start
        //docType = GetSelectedPkID();
        //Vendor workflow modification end

        if (uploadfile.ContentLength > 0)
        {
            string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

            string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
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
    ////private void BindMandatoryDocList()
    ////{
    ////    DataAccessLayer objDb = new DataAccessLayer();
    ////    DataSet dstData = new DataSet();
    ////    DocumentUploadAccess objDoc = new DocumentUploadAccess();

    ////    try
    ////    {
    ////        dstData = objDoc.GetMandatoryDocList(Convert.ToInt32(Session[StaticKeys.SelectedModuleId].ToString()));
    ////        if (dstData.Tables[0].Rows.Count > 0)
    ////        {
    ////            grdMandDocs.DataSource = dstData.Tables[0].DefaultView;
    ////            grdMandDocs.DataBind();
    ////            grdMandDocs.Visible = true;
    ////        }
    ////        else
    ////        {
    ////            grdMandDocs.Visible = false;
    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////    finally
    ////    {
    ////        objDb = null;
    ////    }
    ////}
    //Vendor workflow modification end
    #endregion
}