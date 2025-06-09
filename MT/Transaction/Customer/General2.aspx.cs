using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Configuration;

public partial class Transaction_Customer_General2 : System.Web.UI.Page
{
    CustomerGeneralAccess ObjCustomerGeneralAccess = new CustomerGeneralAccess();
    HelperAccess helperAccess = new HelperAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                FillGeneralData();
                ConfigureControl();

                //CUST_8300001962 Start
                SetValidPAN(ddlRegisterPAN.SelectedValue);
                SetValidGTN(ddlRegisterUnderGST.SelectedValue);
                //CUST_8300001962 End

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
                }

                //CUST_8300001962
                RDMGSTNValid();
                //CUST_8300001962

            }
        }
    }

    /// <summary>
    /// CUST_8300001962
    /// </summary>
    private void RDMGSTNValid()
    {
        if (ddlRegisterUnderGST.SelectedValue == "2" &&
             Session[StaticKeys.LoggedIn_User_DeptId].ToString() == Convert.ToString(ConfigurationManager.AppSettings["DEPRDM"]))
        {

            Type cstype = this.GetType();
            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;
            // Check to see if the startup script is already registered.
            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
            {
                String cstext = "alert('Customer will not get GST Credit.');";
                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
            }
        }
          
    }

    /// <summary>
    /// CUST_8300001962
    /// </summary>
    //private void PopuplateDropDownList()
    //{
    //    helperAccess.PopuplateDropDownList(ddlRegisterPAN, "pr_GetDropDownListByControlNameModuleType 'C','ddlRegisterPAN'", "LookUp_Desc", "LookUp_Code", "");
    //    ddlRegisterPAN.SelectedValue = "01";
    //}

    /// <summary>
    /// CUST_8300001962
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRegisterPAN_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetValidPAN(ddlRegisterPAN.SelectedValue);
    }

    /// <summary>
    /// //CUST_8300001962
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRegisterUnderGST_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetValidGTN(ddlRegisterUnderGST.SelectedValue);
        //if (ddlRegisterUnderGST.SelectedValue == "1")
        //{
        //    reqtxtGSTNo.Visible = true;
        //    labletxtGSTNo.Visible = false;
        //}
        //else
        //{
        //    reqtxtGSTNo.Visible = false;
        //    labletxtGSTNo.Visible = false;
        //}
    }
    /// <summary>
    /// CUST_8300001962
    /// </summary>
    /// <param name="selectedValue"></param>
    private void SetValidPAN(string selectedValue)
    {
        try
        {
            string pVal1 = "";
            string pVal2 = "";
            string pValIN = "";
            SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
            DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dsdata.Tables[0].Rows.Count > 0)
            {
                pVal1 = dsdata.Tables[0].Rows[0]["Customer_Category"].ToString();
                pVal2 = dsdata.Tables[0].Rows[0]["Division"].ToString();
            }
            if (dsdata.Tables[1].Rows.Count > 0)
            {
                pValIN = dsdata.Tables[1].Rows[0]["CountryKey"].ToString();
            }
            lblddlRegisterPAN.Visible = false;
            lableddlRegisterPAN.Visible = false;
            ddlRegisterPAN.Visible = false;
            reqddlRegisterPAN.Visible = false;

            lbltxtPanReason.Visible = false;
            labletxtPanReason.Visible = false;
            txtPanReason.Visible = false;
            reqtxtPanReason.Visible = false;

            if (pValIN == "1" && pVal1 == "DASIRF" && pVal2 != "4")
            {
                reqtxtTax_Number_5.Visible = true;
                labletxtTax_Number_5.Visible = true;

                ddlRegisterUnderGST.SelectedValue = "1";
                ddlRegisterUnderGST.Enabled = false;
            }
            else if (pValIN == "1" && pVal1 != "DASIRF" && selectedValue == "1")
            {
                reqtxtTax_Number_5.Visible = true;
                labletxtTax_Number_5.Visible = true;

                lblddlRegisterPAN.Visible = true;
                lableddlRegisterPAN.Visible = true;
                ddlRegisterPAN.Visible = true;
                reqddlRegisterPAN.Visible = true;

            }
            else if (pValIN == "1" && pVal1 != "DASIRF" && selectedValue != "1")
            {
                reqtxtTax_Number_5.Visible = false;
                labletxtTax_Number_5.Visible = false;

                lblddlRegisterPAN.Visible = true;
                lableddlRegisterPAN.Visible = true;
                ddlRegisterPAN.Visible = true;
                reqddlRegisterPAN.Visible = true;

                lbltxtPanReason.Visible = true;
                labletxtPanReason.Visible = true;
                txtPanReason.Visible = true;
                reqtxtPanReason.Visible = true;

            }
            else
            {
                reqtxtTax_Number_5.Visible = false;
                labletxtTax_Number_5.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }
    /// <summary>
    /// CUST_8300001962
    /// </summary>
    /// <param name="selectedValue"></param>
    private void SetValidGTN(string selectedValue)
    {
        try
        {
            //if (selectedValue == "1") 
            string pValIN = "";
            string pValIRF = "";
            SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
            DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dsdata.Tables[1].Rows.Count > 0)
            {
                pValIN = dsdata.Tables[1].Rows[0]["CountryKey"].ToString(); 
            }
            if (dsdata.Tables[0].Rows.Count > 0)
            { 
                pValIRF = dsdata.Tables[0].Rows[0]["DivisionType"].ToString();
            }
            if (pValIN == "1" && pValIRF != "4")
            {
                lableddlRegisterUnderGST.Visible = true;
                reqddlRegisterUnderGST.Visible = true;

            }
            else if (pValIN == "1" && pValIRF == "4" && selectedValue == "1")
            {
                lableddlRegisterUnderGST.Visible = true;
                reqddlRegisterUnderGST.Visible = true; 
                ddlRegisterUnderGST.Enabled = true;
            }
            else
            {
                lableddlRegisterUnderGST.Visible = false;
                reqddlRegisterUnderGST.Visible = false;
                ddlRegisterUnderGST.SelectedValue = "2";
            }



            if (ddlRegisterUnderGST.SelectedValue == "1")
            {
                reqtxtGSTNo.Visible = true;
                labletxtGSTNo.Visible = true;
                txtGSTNo.Enabled = true;
            }
            else if (ddlRegisterUnderGST.SelectedValue == "2")
            {
                reqtxtGSTNo.Visible = false;
                labletxtGSTNo.Visible = false;
                txtGSTNo.Enabled = false;
                txtGSTNo.Text = "";

                Type cstype = this.GetType();
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Customer will not get GST Credit.');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            else
            {
                reqtxtGSTNo.Visible = false;
                labletxtGSTNo.Visible = false;
                txtGSTNo.Enabled = false;
            }

        }
        catch (Exception ex)
        { }
    }

    /// <summary>
    /// CUST_8300001962
    /// </summary>
    /// <param name="selectedValue"></param>
    private void SetValidGTNold(string selectedValue)
    {
        try
        {
            //if (selectedValue == "1") 
            string pValIN = "";
            SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
            DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text)); 
            if (dsdata.Tables[1].Rows.Count > 0)
            {
                pValIN = dsdata.Tables[1].Rows[0]["CountryKey"].ToString();
            }
            if(pValIN =="1")
            {
                lableddlRegisterUnderGST.Visible = true;
                reqddlRegisterUnderGST.Visible = true;
                
            }
            else
            {
                lableddlRegisterUnderGST.Visible = false;
                reqddlRegisterUnderGST.Visible = false;
                ddlRegisterUnderGST.SelectedValue = "2";
            }
             


            if (ddlRegisterUnderGST.SelectedValue == "1")
            {
                reqtxtGSTNo.Visible = true;
                labletxtGSTNo.Visible = true;
                txtGSTNo.Enabled = true;
            }
            else if (ddlRegisterUnderGST.SelectedValue == "2")
            {
                reqtxtGSTNo.Visible = false;
                labletxtGSTNo.Visible = false;
                txtGSTNo.Enabled = false;
                txtGSTNo.Text = "";

                Type cstype = this.GetType();
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Customer will not get GST Credit.');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            else
            {
                reqtxtGSTNo.Visible = false;
                labletxtGSTNo.Visible = false;
                txtGSTNo.Enabled = false;
            }

        }
        catch (Exception ex)
        { }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        //GST Changes  , SDT27052019 Added By : NR Desc : GST Validation
        if (GSTValidation())
        {
            if (SaveCustomerGeneral2())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //GST Changes  , SDT27052019 Added By : NR Desc : GST Validation
        if (GSTValidation())
        {
            if (SaveCustomerGeneral2())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("General2.aspx");
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        //GST Changes  , SDT27052019 Added By : NR Desc : GST Validation
        if (GSTValidation())
        {
            if (SaveCustomerGeneral2())
            {
                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
    }

    private bool SaveCustomerGeneral2()
    {
        bool flg = false;
        CustomerGeneral2 ObjCustGeneral = GetControlsValue();
        try
        {

            if (ObjCustomerGeneralAccess.Save(ObjCustGeneral) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private CustomerGeneral2 GetCustomerGeneral2()
    {
        return ObjCustomerGeneralAccess.GetCustomerGeneral2(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private CustomerGeneral2 GetControlsValue()
    {
        CustomerGeneral2 ObjCustGeneral = new CustomerGeneral2();
        Utility objUtil = new Utility();

        try
        {
            ObjCustGeneral.Cust_General2_Id = Convert.ToInt32(lblCustomerGeneral2Id.Text);
            ObjCustGeneral.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);


            ObjCustGeneral.Industry_Key = txtIndustrykey.Text;
            ObjCustGeneral.Tax_type = txtTax_Type.Text;
            ObjCustGeneral.Tax_Number_Type = txtTax_Number_Type.Text;
            ObjCustGeneral.Tax_Number1 = txtTaxNumber1.Text;
            ObjCustGeneral.Tax_Number2 = txtTaxNumber2.Text;
            ObjCustGeneral.Tax_Number3 = txtTax_Numbe_3.Text;
            ObjCustGeneral.Tax_Number4 = txtTax_Numbe_4.Text;
            ObjCustGeneral.Tax_Number5 = txtTax_Number_5.Text;
            ObjCustGeneral.Type_of_Business = txtTypeOfBusiness.Text;
            ObjCustGeneral.VAT_Reg = txtVATRegistration.Text;
            ObjCustGeneral.ECC_Number = txtECCNumber.Text;
            ObjCustGeneral.Excise_Registration_No = txtExciseRegistrationNo.Text;
            ObjCustGeneral.Excise_Range = txtExciseRange.Text;
            ObjCustGeneral.Excise_Division = txtExciseDivision.Text;
            ObjCustGeneral.Excise_Commissionerate = txtExciseCommissionerate.Text;
            ObjCustGeneral.Customer_Claasifi = txtCustomerclassification.Text;
            ObjCustGeneral.Nielsen_Id = txtNielsenID.Text;
            ObjCustGeneral.Region_Market = txtRegionalMarket.Text;
            ObjCustGeneral.AccNo_Payer = txtAccountNumberPayer.Text;
            ObjCustGeneral.Payer_Allow_Doc = chkPayerAllowDocs.Checked ? 1 : 0;
            ObjCustGeneral.Central_Delete_Flag = chkCentralDeletion.Checked ? 1 : 0;
            ObjCustGeneral.Central_Posting_Blk = chkCentralPosting.Checked ? 1 : 0;
            ObjCustGeneral.Central_Order = txtCentralorderblock.Text;
            ObjCustGeneral.Central_Delivery = txtCentraldeliveryblock.Text;
            ObjCustGeneral.Central_Billing = txtCentralbillingblock.Text;
            ObjCustGeneral.VAT_Reg = txtVATRegistration.Text;
            ObjCustGeneral.Legal_sts = txtLegalstatus.Text;
            ObjCustGeneral.Ind_Code1 = txtIndustryCode1.Text;
            ObjCustGeneral.Ind_Code2 = txtIndustryCode2.Text;
            ObjCustGeneral.Ind_Code3 = txtIndustryCode3.Text;
            ObjCustGeneral.Ind_Code4 = txtIndustryCode4.Text;
            ObjCustGeneral.Ind_Code5 = txtIndustryCode5.Text;
            ObjCustGeneral.Attribute1 = txtAttribute1.Text;
            ObjCustGeneral.Attribute2 = txtAttribute2.Text;
            ObjCustGeneral.Attribute3 = txtAttribute3.Text;
            ObjCustGeneral.Attribute4 = txtAttribute4.Text;
            ObjCustGeneral.Attribute5 = txtAttribute5.Text;
            ObjCustGeneral.Attribute6 = txtAttribute6.Text;
            ObjCustGeneral.Attribute7 = txtAttribute7.Text;
            ObjCustGeneral.Attribute8 = txtAttribute8.Text;
            ObjCustGeneral.Attribute9 = txtAttribute9.Text;
            ObjCustGeneral.Attribute10 = txtAttribute10.Text;
            //GST Changes
            ObjCustGeneral.GSTNo = txtGSTNo.Text;
            //GST Changes

            //CUST_8300001962 Start
            ObjCustGeneral.RegisterPAN = ddlRegisterPAN.SelectedValue;
            ObjCustGeneral.RegisterUnderGST = ddlRegisterUnderGST.SelectedValue;
            ObjCustGeneral.PanReason = txtPanReason.Text;
            //CUST_8300001962 End

            ObjCustGeneral.IsActive = 1;
            ObjCustGeneral.UserId = lblUserId.Text;
            ObjCustGeneral.TodayDate = objUtil.GetDate();
            ObjCustGeneral.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ObjCustGeneral;
    }

    private void FillGeneralData()
    {
        CustomerGeneral2 ObjCustGeneral = GetCustomerGeneral2();
        if (ObjCustGeneral.Cust_General2_Id > 0)
        {
            lblCustomerGeneral2Id.Text = ObjCustGeneral.Cust_General2_Id.ToString();
            lblMasterHeaderId.Text = ObjCustGeneral.Master_Header_Id.ToString();

            txtIndustrykey.Text = ObjCustGeneral.Industry_Key.ToString();
            txtTax_Type.Text = ObjCustGeneral.Tax_type;
            txtTax_Number_Type.Text = ObjCustGeneral.Tax_Number_Type;
            txtTaxNumber1.Text = ObjCustGeneral.Tax_Number1;
            txtTaxNumber2.Text = ObjCustGeneral.Tax_Number2;
            txtTax_Numbe_3.Text = ObjCustGeneral.Tax_Number3;
            txtTax_Numbe_4.Text = ObjCustGeneral.Tax_Number4;
            txtTax_Number_5.Text = ObjCustGeneral.Tax_Number5;
            txtTypeOfBusiness.Text = ObjCustGeneral.Type_of_Business;
            txtVATRegistration.Text = ObjCustGeneral.VAT_Reg;
            txtECCNumber.Text = ObjCustGeneral.ECC_Number;
            txtExciseRegistrationNo.Text = ObjCustGeneral.Excise_Registration_No;
            txtExciseRange.Text = ObjCustGeneral.Excise_Range;
            txtExciseDivision.Text = ObjCustGeneral.Excise_Division;
            txtExciseCommissionerate.Text = ObjCustGeneral.Excise_Commissionerate;

            txtCustomerclassification.Text = ObjCustGeneral.Customer_Claasifi.ToString();
            txtNielsenID.Text = ObjCustGeneral.Nielsen_Id.ToString();
            txtRegionalMarket.Text = ObjCustGeneral.Region_Market.ToString();
            txtAccountNumberPayer.Text = ObjCustGeneral.AccNo_Payer.ToString();

            chkPayerAllowDocs.Checked = ObjCustGeneral.Payer_Allow_Doc == 1 ? true : false;
            chkCentralDeletion.Checked = ObjCustGeneral.Central_Delete_Flag == 1 ? true : false;
            chkCentralPosting.Checked = ObjCustGeneral.Central_Posting_Blk == 1 ? true : false;

            txtCentralorderblock.Text = ObjCustGeneral.Central_Order.ToString();
            txtCentraldeliveryblock.Text = ObjCustGeneral.Central_Delivery.ToString();
            txtCentralbillingblock.Text = ObjCustGeneral.Central_Billing.ToString();
            txtVATRegistration.Text = ObjCustGeneral.VAT_Reg.ToString();
            txtLegalstatus.Text = ObjCustGeneral.Legal_sts.ToString();
            txtIndustryCode1.Text = ObjCustGeneral.Ind_Code1.ToString();
            txtIndustryCode2.Text = ObjCustGeneral.Ind_Code2.ToString();
            txtIndustryCode3.Text = ObjCustGeneral.Ind_Code3.ToString();
            txtIndustryCode4.Text = ObjCustGeneral.Ind_Code4.ToString();
            txtIndustryCode5.Text = ObjCustGeneral.Ind_Code5.ToString();
            txtAttribute1.Text = ObjCustGeneral.Attribute1.ToString();
            txtAttribute2.Text = ObjCustGeneral.Attribute2.ToString();
            txtAttribute3.Text = ObjCustGeneral.Attribute3.ToString();
            txtAttribute4.Text = ObjCustGeneral.Attribute4.ToString();
            txtAttribute5.Text = ObjCustGeneral.Attribute5.ToString();
            txtAttribute6.Text = ObjCustGeneral.Attribute6.ToString();
            txtAttribute7.Text = ObjCustGeneral.Attribute7.ToString();
            txtAttribute8.Text = ObjCustGeneral.Attribute8.ToString();
            txtAttribute9.Text = ObjCustGeneral.Attribute9.ToString();
            txtAttribute10.Text = ObjCustGeneral.Attribute10.ToString();

            //GST Changes
            txtGSTNo.Text = ObjCustGeneral.GSTNo;
            //GST Changes


            //CUST_8300001962 Start
            ddlRegisterPAN.SelectedValue = ObjCustGeneral.RegisterPAN;
            ddlRegisterUnderGST.SelectedValue = ObjCustGeneral.RegisterUnderGST;
            txtPanReason.Text = ObjCustGeneral.PanReason;
            //CUST_8300001962 End
        }
        else
        {
            lblCustomerGeneral2Id.Text = "0";
        }
    }


    /// <summary>
    /// //SDT27052019, Added By Nitin R, Disc : GST Validation
    /// </summary>
    private bool GSTValidation()
    {
        bool flg1 = true;
        string ErrorMsg1 = "";

        if (!System.Text.RegularExpressions.Regex.IsMatch(txtTax_Number_5.Text, "[A-Za-z]{3}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[A-Za-z]{1}[\\d]{4}[A-Za-z]{1}") && txtTax_Number_5.Text.ToUpper() != "")
        {
            ErrorMsg1 = "Invalid Pan.";
            flg1 = false;
        }
        else
        {
            bool flgDigit = true;

            if (txtGSTNo.Text != "" && txtTax_Number_5.Text != "")
            {
                if (lblGstNo.Text == "^(28)")
                {
                    if (!lblGstNo.Text.Contains(txtGSTNo.Text.Substring(0, 2).ToString()))
                    {
                        lblGstNo.Text = "^(37)";
                        if (!lblGstNo.Text.Contains(txtGSTNo.Text.Substring(0, 2).ToString()))
                        {
                            ErrorMsg1 = "First two digit codes of GST No not matching with customer region code";
                            flgDigit = false;
                        }
                    }

                }
                else if (!lblGstNo.Text.Contains(txtGSTNo.Text.Substring(0, 2).ToString()))
                {
                    ErrorMsg1 = "First two digit codes of GST No not matching with customer region code";
                    flgDigit = false;
                }

                if (flgDigit)
                {
                    if (lblGstNo.Text == "^(28)")
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && lblActionType.Text != "C")
                        {
                            lblGstNo.Text = "^(37)";
                            if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && lblActionType.Text != "C")
                            {
                                ErrorMsg1 = "Invalid GSTN. GSTN should be 15 digit alphanumeric with first two digits equal to the selected region code, next 10 digits equal to PAN number and last 3 digits random alphanumeric characters.";
                                flg1 = false;
                            }
                        }
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && lblActionType.Text != "C")
                    {
                        ErrorMsg1 = "Invalid GSTN. GSTN should be 15 digit alphanumeric with first two digits equal to the selected region code, next 10 digits equal to PAN number and last 3 digits random alphanumeric characters.";
                        flg1 = false;
                    }
                }
                else
                    flg1 = false;
            }
        }
        if (!flg1)
        {
            lblMsg.Text = ErrorMsg1;
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }

        return flg1;

    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.General_Data_2 obj = new SectionConfiguration.General_Data_2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
        DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
        if (dsdata.Tables[0].Rows.Count > 0)
        {
            if (dsdata.Tables[0].Rows[0]["Customer_Category"].ToString() == "DASIRF")
            {
                lbltxtTaxNumber1.Visible = false;
                txtTaxNumber1.Visible = false;
                lbltxtTaxNumber2.Visible = false;
                txtTaxNumber2.Visible = false;
                lbltxtTax_Numbe_3.Visible = false;
                txtTax_Numbe_3.Visible = false;
                lbltxtTax_Numbe_4.Visible = false;
                txtTax_Numbe_4.Visible = false;
                lbltxtTypeOfBusiness.Visible = false;
                txtTypeOfBusiness.Visible = false;
                //txtVATRegistration.Visible = false;
                //txtTax_Number_Type.Visible = false;
                //txtTax_Type.Visible = false;
                lbltxtECCNumber.Visible = false;
                txtECCNumber.Visible = false;
                lbltxtExciseRegistrationNo.Visible = false;
                txtExciseRegistrationNo.Visible = false;
                lbltxtExciseRange.Visible = false;
                txtExciseRange.Visible = false;
                lbltxtExciseDivision.Visible = false;
                txtExciseDivision.Visible = false;
                lbltxtExciseCommissionerate.Visible = false;
                txtExciseCommissionerate.Visible = false;
            }
        }

        //GST changes Start SDT27052019 Added By : NR
        CustomerGeneral1 Objvg1 = ObjCustomerGeneralAccess.GetCustomerGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
        if (Objvg1.Region != "0")
        {
            switch (Objvg1.Region)
            {
                case "709": lblGstNo.Text = "28"; break;  //Andhra Pradesh
                case "710": lblGstNo.Text = "12"; break;  //Arunachal pradesh
                case "711": lblGstNo.Text = "18"; break;  //Assam 
                case "712": lblGstNo.Text = "10"; break;  //Bihar
                case "713": lblGstNo.Text = "30"; break;  //Goa
                case "714": lblGstNo.Text = "24"; break;  //Gujrat
                case "715": lblGstNo.Text = "06"; break;  //Harayana
                case "716": lblGstNo.Text = "02"; break;  //Himachal pradesh
                case "717": lblGstNo.Text = "01"; break;  //J&K
                case "718": lblGstNo.Text = "29"; break;  //Karnataka
                case "719": lblGstNo.Text = "32"; break;  //Kerala
                case "720": lblGstNo.Text = "23"; break;  //MP
                case "721": lblGstNo.Text = "27"; break;  //MH
                case "722": lblGstNo.Text = "14"; break;  //Manipur
                case "723": lblGstNo.Text = "17"; break;  //Meghalaya
                case "724": lblGstNo.Text = "15"; break;  //Mizoram
                case "725": lblGstNo.Text = "13"; break;  //Nagaland
                case "726": lblGstNo.Text = "21"; break;  //Odhisha
                case "727": lblGstNo.Text = "03"; break;  //Punjab
                case "728": lblGstNo.Text = "08"; break;  //Rajasthan
                case "729": lblGstNo.Text = "11"; break;  //Sikkim
                case "730": lblGstNo.Text = "33"; break;  //TN
                case "731": lblGstNo.Text = "16"; break;  //Tripura
                case "732": lblGstNo.Text = "09"; break;  //UP
                case "733": lblGstNo.Text = "19"; break;  //WB
                case "734": lblGstNo.Text = "35"; break;  //Andaman und Nico.In.
                case "735": lblGstNo.Text = "04"; break;  //Chandigarh
                case "736": lblGstNo.Text = "26"; break;  //Dadra und Nagar Hav.
                case "737": lblGstNo.Text = "25"; break;  //Daman Diu
                case "738": lblGstNo.Text = "07"; break;  //Delhi
                case "739": lblGstNo.Text = "31"; break;  //Lakshadweep Islands
                case "740": lblGstNo.Text = "34"; break;  //Pondicherry
                case "741": lblGstNo.Text = "22"; break;  //Chattisgarh
                case "742": lblGstNo.Text = "20"; break;  //Jharkhand
                case "743": lblGstNo.Text = "05"; break;  //Uttarakhand
                case "1683": lblGstNo.Text = "36"; break; //Telangana
                default: lblGstNo.Text = ""; break;
            }
            if (lblGstNo.Text != "")
                lblGstNo.Text = "^(" + lblGstNo.Text + ")";
        }
        //GST changes End EDT27052019 Added By : NR
    }
}