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

public partial class Transaction_Cutomer_General : System.Web.UI.Page
{
    CustomerGeneralAccess ObjCustomerGeneralAccess = new CustomerGeneralAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                PopuplateDropDownList();
                FillGeneralData();

                string sectionId = lblSectionId.Text.ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string mode = Session[StaticKeys.Mode].ToString();
                lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    trButton.Visible = true;
                    btnSave.Visible = !btnNext.Visible;
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
            SetAddressVisible();
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string pageURL = btnPrevious.CommandArgument.ToString();
        Response.Redirect(pageURL);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            Response.Redirect("General.aspx");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
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
				//Start Change By Swati
				//if (objDb.DeleteRecord("Customer_Documents", "Document_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
				//End Change
                {
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

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRegionData();
        ddlRegion.Focus();
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlRegion.SelectedValue != "0" && ddlRegion.SelectedValue != "#")
        //{
        //DataSet ds = ObjCustomerGeneralAccess.GetVendorDuplicate(Convert.ToInt32(lblMasterHeaderId.Text), "", ddlRegion.SelectedValue);

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    lblMsg.Text = "Vendor in same Region. already created with same PAN.<br />Please check if you still want to continue.";
        //    pnlMsg.CssClass = "error";
        //    pnlMsg.Visible = true;
        //}
        //}
        txtMobileNum.Focus();
    }

    #endregion

    #region Private Methods

    private void SetAddressVisible()
    {
        txtstreet2.Enabled = txtHouseNo.Text.Length > 0 ? true : false;
        txtstreet3.Enabled = txtstreet2.Text.Length > 0 ? true : false;

        txtName2.Enabled = txtName1.Text.Length > 0 ? true : false;
        txtMobileNum2.Enabled = txtMobileNum.Text.Length > 0 ? true : false;
        txtSecondTelephoneNumber.Enabled = txtFirsttelephone.Text.Length > 0 ? true : false;

        txtEmailAddress2.Enabled = txtEmailAddress.Text.Length > 0 ? true : false;
        txtEmailAddress3.Enabled = txtEmailAddress2.Text.Length > 0 ? true : false;
    }

    private void PopuplateDropDownList()
    {
		//Start Change By Swati
        //bindCustomerType(ddlCustomerType);
        helperAccess.PopuplateDropDownList(ddlCustomerType, "pr_GetMasterCategoryByModuleType 'C'", "Master_Category_Name", "Master_Category_Code", "");
		//End Change
        helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
        helperAccess.PopuplateDropDownList(ddlCustomerAccGrp, "pr_GetCustomerAccGrpList 0", "Customer_Acc_Grp_Name", "Customer_Acc_Grp_Id");
        helperAccess.PopuplateDropDownList(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlTitle, "pr_GetDropDownListByControlNameModuleType 'C','txtTitle'", "LookUp_Desc", "LookUp_Code", "");

        if (lblActionType.Text == "C")
        {
            helperAccess.AddBlankOption(ddlCountry);
            helperAccess.AddBlankOption(ddlRegion);
            helperAccess.AddBlankOption(ddlTitle);
        }
    }

    public void bindCustomerType(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Insert(0, new ListItem("---Select---", ""));
        ddl.Items.Insert(1, new ListItem("Export - Formulation", "ExpFor"));
        ddl.Items.Insert(1, new ListItem("Export - API", "ExpAPI"));
        ddl.Items.Insert(1, new ListItem("Domestic - API", "DomAPI"));
        ddl.Items.Insert(2, new ListItem("Plant Misc. - Scrap Sale", "PlnMisc"));
        ddl.Items.Insert(3, new ListItem("P2P", "P2P"));
        ddl.Items.Insert(4, new ListItem("Mass Marketing", "MassMkt"));
		//Start Change By Swati
        ddl.Items.Insert(5, new ListItem("DAS/IRF-Customer", "DASIRF"));
		//End Change
    }

    private void BindRegionData()
    {
        string Country = ddlCountry.SelectedValue == "#" ? "0" : ddlCountry.SelectedValue;
        helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + Country, "Region_Name", "Region_Id");
        string CountryCode = ddlCountry.SelectedValue == "#" ? "" : ddlCountry.SelectedItem.Text.Split('-')[0].Trim();
        helperAccess.PopuplateDropDownList(ddlTransportationZone, "pr_GetDropDownListByControlNameModuleType 'C','ddlTransportationZone','" + lblSectionId.Text + "','" + CountryCode + "'", "LookUp_Desc", "LookUp_Code", "");

        ListItem lst;
        lst = ddlTransportationZone.Items.FindByValue("Z00001    ");

        if (lst == null)
        {
            lst = ddlTransportationZone.Items.FindByValue("Z99999    ");
            if (lst != null)
                ddlTransportationZone.Items.FindByValue("Z99999    ").Selected = true;
        }
        else
        {
            ddlTransportationZone.Items.FindByValue("Z00001    ").Selected = true;
        }

        if (Country == "1")
        {
            txtTaxJurisdiction.Text = "IN00";
        }
        else
        {
            ConfigureControl();
            txtTaxJurisdiction.Text = "9900";
        }

        SetCountryWiseValidation();

        if (Session[StaticKeys.ActionType].ToString() == "C")
        {
            helperAccess.AddBlankOption(ddlRegion);
        }
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
				//Start Change By Swati
                if ((fileExtension == ".pdf") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".doc") || (fileExtension == ".docx") || (fileExtension == ".xls") || (fileExtension == ".xlsx"))
                //End Change
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
			//Start Change By Swati
            lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .Gif, .Png, .Pdf, .xlsx, .xls, .doc, .docx files allowed.";
            //End Change
			pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
        else
        {
            boolFlg = SaveCustomerGeneral();
        }
        return boolFlg;
    }

    private bool SaveCustomerGeneral()
    {
        CustomerGeneral1 ObjCustGeneral = GetControlsValue();
        bool flg = false;
        try
        {
			////Start Change By Swati
            if (ddlCountry.SelectedValue == "1" && txtPostalCode.Text == "")
            {
                string message = "alert('Postal Code cannot be blank.')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if ((ObjCustomerGeneralAccess.Save(ObjCustGeneral) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
                    {
                        scope.Complete();
                        flg = true;
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
			//End Change
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private CustomerGeneral1 GetCustomerGeneral()
    {
        return ObjCustomerGeneralAccess.GetCustomerGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private CustomerGeneral1 GetControlsValue()
    {
        CustomerGeneral1 ObjCustGeneral = new CustomerGeneral1();
        Utility objUtil = new Utility();

        ObjCustGeneral.Cust_General1_Id = Convert.ToInt32(lblCustomerGeneralId.Text);
        ObjCustGeneral.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjCustGeneral.Customer_Code = txtCustomerCode.Text;
        ObjCustGeneral.Customer_Acc_Grp = ddlCustomerAccGrp.SelectedValue;
        ObjCustGeneral.Company_Code = ddlCompanyCode.SelectedValue;
        ObjCustGeneral.Customer_Category = ddlCustomerType.SelectedValue;

        ObjCustGeneral.Title = ddlTitle.SelectedValue;
        ObjCustGeneral.Name1 = txtName1.Text;
        ObjCustGeneral.Name2 = txtName2.Text;
        ObjCustGeneral.Name3 = txtName3.Text;
        ObjCustGeneral.Name4 = txtName4.Text;
        ObjCustGeneral.Sort_Field = txtSortfield.Text;
        ObjCustGeneral.Name_CO = txtNameCO.Text;
        ObjCustGeneral.HouseNo_Street = txtHouseNo.Text;
        ObjCustGeneral.Street2 = txtstreet2.Text;
        ObjCustGeneral.Street3 = txtstreet3.Text;
        ObjCustGeneral.Street4 = txtstreet4.Text;
        ObjCustGeneral.Street5 = txtstreet5.Text;
        ObjCustGeneral.City = txtCity.Text;
        ObjCustGeneral.Postal_Code = txtPostalCode.Text;
        ObjCustGeneral.Different_City = txtDifferentCity.Text;
        ObjCustGeneral.District = txtDistrict.Text;
        ObjCustGeneral.PO_Box = txtPOBox.Text;
        ObjCustGeneral.PO_Box_Postal_Code = txtPOBoxPostal.Text;
        ObjCustGeneral.CountryKey = ddlCountry.SelectedValue;
        ObjCustGeneral.Region = ddlRegion.SelectedValue;
        ObjCustGeneral.LanguageAcc = txtLanguage.Text;
        ObjCustGeneral.Mobile_Num = txtMobileNum.Text;
        ObjCustGeneral.Mobile_Num2 = txtMobileNum2.Text;
        ObjCustGeneral.First_Tele_No = txtFirsttelephone.Text;
        ObjCustGeneral.Second_Tele_No = txtSecondTelephoneNumber.Text;
        ObjCustGeneral.Fax_NO = txtFaxNumber.Text;
        ObjCustGeneral.Email_Address = txtEmailAddress.Text;
        ObjCustGeneral.Email_Address2 = txtEmailAddress2.Text;
        ObjCustGeneral.Email_Address3 = txtEmailAddress3.Text;
        ObjCustGeneral.Transportation_Zone = ddlTransportationZone.SelectedValue;
        ObjCustGeneral.Tax_Jurisdiction = txtTaxJurisdiction.Text;
        ObjCustGeneral.Acc_No_Vendor = txtAccountNumberVendor.Text;

        ObjCustGeneral.Com_Id_TradingPat = txtCompanyIDTrading.Text;
        ObjCustGeneral.Group_Key = txtGroupkey.Text;
        if (rdlLiableforVAT.SelectedItem != null)
            ObjCustGeneral.LiableVat = rdlLiableforVAT.SelectedValue.ToString();
        else
            ObjCustGeneral.LiableVat = null;

        ObjCustGeneral.Country_Code = txtCountryCode.Text;
        ObjCustGeneral.City_Code = txtCityCode.Text;
        ObjCustGeneral.IsActive = 1;
        ObjCustGeneral.UserId = lblUserId.Text;
        ObjCustGeneral.TodayDate = objUtil.GetDate();
        ObjCustGeneral.IPAddress = objUtil.GetIpAddress();
        //Start Addition By Swati M Date: 12.10.2018
        ObjCustGeneral.Remarks = txtRemarks.Text;
        //End Addition By Swati M Date: 12.10.2018

        return ObjCustGeneral;
    }

    private void FillGeneralData()
    {
        CustomerGeneral1 ObjCustGeneral = GetCustomerGeneral();
        if (ObjCustGeneral.Cust_General1_Id > 0)
        {
            lblCustomerGeneralId.Text = ObjCustGeneral.Cust_General1_Id.ToString();
            lblMasterHeaderId.Text = ObjCustGeneral.Master_Header_Id.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjCustGeneral.ModulePlantGroupCode;
            txtCustomerCode.Text = ObjCustGeneral.Customer_Code.ToString();
            Session[StaticKeys.MaterialNo] = ObjCustGeneral.Customer_Code.ToString();

            ddlCustomerAccGrp.SelectedValue = ObjCustGeneral.Customer_Acc_Grp;
            ddlCompanyCode.SelectedValue = ObjCustGeneral.Company_Code;
            ddlCustomerType.SelectedValue = ObjCustGeneral.Customer_Category;

            ddlTitle.SelectedValue = ObjCustGeneral.Title;
            txtName1.Text = ObjCustGeneral.Name1;
            txtName2.Text = ObjCustGeneral.Name2;
            txtName3.Text = ObjCustGeneral.Name3;
            txtName4.Text = ObjCustGeneral.Name4;
            txtSortfield.Text = ObjCustGeneral.Sort_Field;
            txtNameCO.Text = ObjCustGeneral.Name_CO;
            txtHouseNo.Text = ObjCustGeneral.HouseNo_Street;
            txtstreet2.Text = ObjCustGeneral.Street2;
            txtstreet3.Text = ObjCustGeneral.Street3;
            txtstreet4.Text = ObjCustGeneral.Street4;
            txtstreet5.Text = ObjCustGeneral.Street5;
            txtCity.Text = ObjCustGeneral.City;
            txtPostalCode.Text = ObjCustGeneral.Postal_Code;
            txtDifferentCity.Text = ObjCustGeneral.Different_City;
            txtDistrict.Text = ObjCustGeneral.District;
            txtPOBox.Text = ObjCustGeneral.PO_Box;
            txtPOBoxPostal.Text = ObjCustGeneral.PO_Box_Postal_Code;
            ddlCountry.SelectedValue = ObjCustGeneral.CountryKey;
            BindRegionData();
            ddlRegion.SelectedValue = ObjCustGeneral.Region;
            txtLanguage.Text = ObjCustGeneral.LanguageAcc;
            txtMobileNum.Text = ObjCustGeneral.Mobile_Num;
            txtMobileNum2.Text = ObjCustGeneral.Mobile_Num2;
            txtFirsttelephone.Text = ObjCustGeneral.First_Tele_No;
            txtSecondTelephoneNumber.Text = ObjCustGeneral.Second_Tele_No;
            txtFaxNumber.Text = ObjCustGeneral.Fax_NO;
            txtEmailAddress.Text = ObjCustGeneral.Email_Address;
            txtEmailAddress2.Text = ObjCustGeneral.Email_Address2;
            txtEmailAddress3.Text = ObjCustGeneral.Email_Address3;
            ddlTransportationZone.SelectedValue = ObjCustGeneral.Transportation_Zone;
            txtTaxJurisdiction.Text = ObjCustGeneral.Tax_Jurisdiction;
            txtAccountNumberVendor.Text = ObjCustGeneral.Acc_No_Vendor;
            txtCompanyIDTrading.Text = ObjCustGeneral.Com_Id_TradingPat;
            txtGroupkey.Text = ObjCustGeneral.Group_Key;
            if (ObjCustGeneral.LiableVat != "")
                rdlLiableforVAT.SelectedValue = ObjCustGeneral.LiableVat.ToLower() == "true" ? "1" : "0";
            txtCountryCode.Text = ObjCustGeneral.Country_Code;
            txtCityCode.Text = ObjCustGeneral.City_Code;
            //Start Addition By Swati M Date: 12.10.2018
            txtRemarks.Text = ObjCustGeneral.Remarks;
            //End Addition By Swati M Date: 12.10.2018

            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        else
        {
            lblCustomerGeneralId.Text = "0";
            txtCustomerCode.Text = ObjCustGeneral.Customer_Code;
            ddlCompanyCode.SelectedValue = ObjCustGeneral.Company_Code;
            ddlCustomerAccGrp.SelectedValue = ObjCustGeneral.Customer_Acc_Grp;
            ddlTitle.SelectedValue = "COMP";
            txtLanguage.Text = "E";
            ddlCustomerType.SelectedValue = ObjCustGeneral.Customer_Category;

            Session[StaticKeys.MaterialNo] = ObjCustGeneral.Customer_Code;
            Session[StaticKeys.SelectedModulePlantGrp] = ObjCustGeneral.ModulePlantGroupCode;
        }
        ddlCompanyCode.Enabled = false;
        ddlCustomerAccGrp.Enabled = false;
        ddlCustomerType.Enabled = false;
        txtLanguage.Enabled = false;
        txtTaxJurisdiction.Enabled = false;
    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.General_data obj = new SectionConfiguration.General_data();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));

        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
        if (deptId != "13")
        {
            txtCustomerCode.Enabled = labletxtCustomerCode.Visible;
        }
        else
        {
            labletxtCustomerCode.Visible = true;
            txtCustomerCode.Enabled = true;
            reqtxtCustomerCode.Visible = true;
        }

        SetCountryWiseValidation();
    }

    private void SetCountryWiseValidation()
    {
        regtxtPostalCode.ValidationExpression = Postal_Code.GetRegex(ddlCountry.SelectedValue);

        if (ddlCountry.SelectedValue == "1")
        {
            lableddlRegion.Visible = true;
            reqddlRegion.Visible = true;

            regtxtMobileNum.Enabled = true;
            regtxtMobileNum2.Enabled = true;

            labletxtPostalCode.Visible = true;
            reqtxtPostalCode.Visible = true;
            //regtxtPostalCode.Enabled = true;
        }
        else
        {
            regtxtMobileNum.Enabled = false;
            regtxtMobileNum2.Enabled = false;

            labletxtPostalCode.Visible = false;
            reqtxtPostalCode.Enabled = false;
            //regtxtPostalCode.Enabled = false;
        }
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
                    //UploadDocument(uploadfile, StrPath, savePath);
                    UploadDocument(uploadfile, StrPath, savePath, i);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath, int pvali)
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

            //string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
            string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + pvali + sufix + Path.GetExtension(uploadfile.FileName);
             
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