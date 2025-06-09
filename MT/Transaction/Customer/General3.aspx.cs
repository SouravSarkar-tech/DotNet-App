using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;

public partial class Transaction_Customer_General3 : System.Web.UI.Page
{
    CustomerGeneralAccess ObjCustomerGeneralAccess = new CustomerGeneralAccess();

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
                    }
                    ConfigureControl();
                }
                else
                {
                    Response.Redirect("CustomerMaster.aspx");
                }
            }
            SetFieldEnable();
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (IsDataComplete())
        {
            if (SaveCustomerGeneral3())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsDataComplete())
        {
            if (SaveCustomerGeneral3())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                Response.Redirect("General3.aspx");
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (IsDataComplete())
        {
            if (SaveCustomerGeneral3())
            {
                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMobileNumEnable();
    }

    protected void ddlCountry2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMobileNumEnable();
    }

    protected void ddlCountry3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMobileNumEnable();
    }

    protected void txtFirst_name_TextChanged(object sender, EventArgs e)
    {
        SetFieldEnable();
    }

    protected void txtFirst_name2_TextChanged(object sender, EventArgs e)
    {
        SetFieldEnable();
    }

    protected void txtFirst_name3_TextChanged(object sender, EventArgs e)
    {
        SetFieldEnable();
    }

    #endregion

    #region Private Methods

    private bool IsDataComplete()
    {
        bool errflg = true;

        if (txtFirst_name.Text != "" && ddlCountry.SelectedValue == "0")
        {
            errflg = false;
        }
        else if (txtFirst_name2.Text != "" && ddlCountry2.SelectedValue == "0")
        {
            errflg = false;
        }
        else if (txtFirst_name3.Text != "" && ddlCountry3.SelectedValue == "0")
        {
            errflg = false;
        }

        if (!errflg)
        {
            lblMsg.Text = "Please select the country for Contact Person Name";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
        else
        {
            if (txtFirst_name.Text == "" && (ddlCountry.SelectedValue != "0" || txtMobileNum.Text != "" || txtMobileNum2.Text != "" || txtFirsttelephone.Text != "" || txtSecondTelephoneNumber.Text != "" || txtFaxNumber.Text != "" || txtEmailAddress.Text != "" || txtEmailAddress2.Text != ""))
            {
                errflg = false;
            }
            else if (txtFirst_name2.Text == "" && (ddlCountry2.SelectedValue != "0" || txtMobileNum_2.Text != "" || txtMobileNum22.Text != "" || txtFirsttelephone2.Text != "" || txtSecondTelephoneNumber2.Text != "" || txtFaxNumber2.Text != "" || txtEmailAddress_2.Text != "" || txtEmailAddress22.Text != ""))
            {
                errflg = false;
            }
            else if (txtFirst_name3.Text == "" && (ddlCountry3.SelectedValue != "0" || txtMobileNum3.Text != "" || txtMobileNum23.Text != "" || txtFirsttelephone3.Text != "" || txtSecondTelephoneNumber3.Text != "" || txtFaxNumber3.Text != "" || txtEmailAddress3.Text != "" || txtEmailAddress23.Text != ""))
            {
                errflg = false;
            }

            if (!errflg)
            {
                lblMsg.Text = "Please fill in the Contact Person Name or Clear the Contact details";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        return errflg;
    }

    private void SetMobileNumEnable()
    {
        if (ddlCountry.SelectedValue == "1")
        {
            regtxtMobileNum.Enabled = true;
            regtxtMobileNum2.Enabled = true;
        }
        else
        {
            regtxtMobileNum.Enabled = false;
            regtxtMobileNum2.Enabled = false;
        }

        if (ddlCountry2.SelectedValue == "1")
        {
            regtxtMobileNum_2.Enabled = true;
            regtxtMobileNum22.Enabled = true;
        }
        else
        {
            regtxtMobileNum_2.Enabled = false;
            regtxtMobileNum22.Enabled = false;
        }

        if (ddlCountry3.SelectedValue == "1")
        {
            regtxtMobileNum3.Enabled = true;
            regtxtMobileNum23.Enabled = true;
        }
        else
        {
            regtxtMobileNum3.Enabled = false;
            regtxtMobileNum23.Enabled = false;
        }
    }

    private void SetFieldEnable()
    {
        bool errflg = false;
        bool flg = false;

        if (txtFirst_name.Text != "")
            flg = true;

        lableddlCountry.Visible = flg;
        ddlCountry.Enabled = flg;
        reqddlCountry.Visible = flg;


        txtMobileNum.Enabled = flg;
        if (txtMobileNum.Text != "")
            txtMobileNum2.Enabled = true;
        else
            txtMobileNum2.Enabled = false;

        txtFirsttelephone.Enabled = flg;
        if (txtFirsttelephone.Text != "")
            txtSecondTelephoneNumber.Enabled = true;
        else
            txtSecondTelephoneNumber.Enabled = false;

        txtFaxNumber.Enabled = flg;
        txtEmailAddress.Enabled = flg;
        if (txtEmailAddress.Text != "")
            txtEmailAddress2.Enabled = true;
        else
            txtEmailAddress2.Enabled = false;

        if (!flg && (ddlCountry.SelectedValue != "0" || txtMobileNum.Text != "" || txtMobileNum2.Text != "" || txtFirsttelephone.Text != "" || txtSecondTelephoneNumber.Text != "" || txtFaxNumber.Text != "" || txtEmailAddress.Text != "" || txtEmailAddress2.Text != ""))
        {
            errflg = true;

            lblMsg.Text = "Please fill in the Contact Person Name or Clear the Contact details";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }


        txtFirst_name2.Enabled = flg;

        bool flg1 = false;

        if (txtFirst_name2.Text != "")
            flg1 = true;

        lableddlCountry2.Visible = flg1;
        ddlCountry2.Enabled = flg1;
        reqddlCountry2.Visible = flg1;

        txtMobileNum_2.Enabled = flg1;

        if (txtMobileNum_2.Text != "")
            txtMobileNum22.Enabled = true;
        else
            txtMobileNum22.Enabled = false;

        txtFirsttelephone2.Enabled = flg1;

        if (txtFirsttelephone2.Text != "")
            txtSecondTelephoneNumber2.Enabled = true;
        else
            txtSecondTelephoneNumber2.Enabled = false;

        txtFaxNumber2.Enabled = flg1;
        txtEmailAddress_2.Enabled = flg1;

        if (txtEmailAddress_2.Text != "")
            txtEmailAddress22.Enabled = true;
        else
            txtEmailAddress22.Enabled = false;


        if (!flg1 && (ddlCountry2.SelectedValue != "0" || txtMobileNum_2.Text != "" || txtMobileNum22.Text != "" || txtFirsttelephone2.Text != "" || txtSecondTelephoneNumber2.Text != "" || txtFaxNumber2.Text != "" || txtEmailAddress_2.Text != "" || txtEmailAddress22.Text != ""))
        {
            errflg = true;

            lblMsg.Text = "Please fill in the Contact Person Name or Clear the Contact details";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

        txtFirst_name3.Enabled = flg1;

        bool flg2 = false;

        if (txtFirst_name3.Text != "")
            flg2 = true;

        lableddlCountry3.Visible = flg2;
        ddlCountry3.Enabled = flg2;
        reqddlCountry3.Visible = flg2;

        txtMobileNum3.Enabled = flg2;

        if (txtMobileNum3.Text != "")
            txtMobileNum23.Enabled = true;
        else
            txtMobileNum23.Enabled = false;

        txtFirsttelephone3.Enabled = flg2;

        if (txtFirsttelephone3.Text != "")
            txtSecondTelephoneNumber3.Enabled = true;
        else
            txtSecondTelephoneNumber3.Enabled = false;

        txtFaxNumber3.Enabled = flg2;
        txtEmailAddress3.Enabled = flg2;

        if (txtEmailAddress3.Text != "")
            txtEmailAddress23.Enabled = true;
        else
            txtEmailAddress23.Enabled = false;


        if (!flg2 && (ddlCountry3.SelectedValue != "0" || txtMobileNum3.Text != "" || txtMobileNum23.Text != "" || txtFirsttelephone3.Text != "" || txtSecondTelephoneNumber3.Text != "" || txtFaxNumber3.Text != "" || txtEmailAddress3.Text != "" || txtEmailAddress23.Text != ""))
        {
            errflg = true;

            lblMsg.Text = "Please fill in the Contact Person Name or Clear the Contact details";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

        if (!errflg)
        {
            pnlMsg.Visible = false;
        }
    }

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();

        helperAccess.PopuplateDropDownList(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlCountry2, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlCountry3, "pr_Get_CountryList", "Country_Name", "Country_Id");
    }

    private bool SaveCustomerGeneral3()
    {
        CustomerGeneral3 ObjCustGeneral = GetControlsValue();
        bool flg = false;
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

    private CustomerGeneral3 GetCustomerGeneral3()
    {
        return ObjCustomerGeneralAccess.GetCustomerGeneral3(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private CustomerGeneral3 GetControlsValue()
    {
        CustomerGeneral3 ObjCustGeneral = new CustomerGeneral3();
        Utility objUtil = new Utility();

        ObjCustGeneral.Cust_General3_Id = Convert.ToInt32(lblCustomerGeneral3Id.Text);
        ObjCustGeneral.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjCustGeneral.Fiscal_Year_Variant = txtFiscal_Year_Variant.Text;
        ObjCustGeneral.Reference_Account = txtReference_Account.Text;
        ObjCustGeneral.PO_Box_city = txtPO_Box_city.Text;
        ObjCustGeneral.Hierarchy_assignment = txtHierarchy_assignment.Text;
        ObjCustGeneral.Central_sales = txtCentral_sales.Text;
        ObjCustGeneral.Customer_condition1 = txtCustomer_condition1.Text;
        ObjCustGeneral.Customer_condition2 = txtCustomer_condition2.Text;
        ObjCustGeneral.Customer_condition3 = txtCustomer_condition3.Text;
        ObjCustGeneral.Customer_condition4 = txtCustomer_condition4.Text;
        ObjCustGeneral.Customer_condition5 = txtCustomer_condition5.Text;
        ObjCustGeneral.Uniform_Resource = txtUniform_Resource.Text;
        ObjCustGeneral.Central_deletion = chkCentralDeletionBlock.Checked == true ? "1" : "0";
        ObjCustGeneral.Unloading_Point = txtUnloading_Point.Text;
        ObjCustGeneral.Customer_factory = txtCustomer_factory.Text;
        ObjCustGeneral.Contact_person_department = txtContact_person_department.Text;
        ObjCustGeneral.First_name = txtFirst_name.Text;
        ObjCustGeneral.Country_Key = ddlCountry.SelectedValue;// dt.Rows[0]["Country_Key"].ToString();
        ObjCustGeneral.Mobile_Num = txtMobileNum.Text;// dt.Rows[0]["Mobile_Num"].ToString();
        ObjCustGeneral.Mobile_Num2 = txtMobileNum2.Text;// dt.Rows[0]["Mobile_Num2"].ToString();
        ObjCustGeneral.First_Tele_No = txtFirsttelephone.Text;// dt.Rows[0]["First_Tele_No"].ToString();
        ObjCustGeneral.Second_Tele_No = txtSecondTelephoneNumber.Text;// dt.Rows[0]["Second_Tele_No"].ToString();
        ObjCustGeneral.Fax_NO = txtFaxNumber.Text;// dt.Rows[0]["Fax_NO"].ToString();
        ObjCustGeneral.Email_Address = txtEmailAddress.Text;// dt.Rows[0]["Email_Address"].ToString();
        ObjCustGeneral.Email_Address2 = txtEmailAddress2.Text;// dt.Rows[0]["Email_Address2"].ToString();
        ObjCustGeneral.First_name_2 = txtFirst_name2.Text;// dt.Rows[0]["First_name_2"].ToString();
        ObjCustGeneral.Country_Key_2 = ddlCountry2.SelectedValue;// dt.Rows[0]["Country_Key_2"].ToString();
        ObjCustGeneral.Mobile_Num_2 = txtMobileNum_2.Text;// dt.Rows[0]["Mobile_Num_2"].ToString();
        ObjCustGeneral.Mobile_Num2_2 = txtMobileNum22.Text;// dt.Rows[0]["Mobile_Num2_2"].ToString();
        ObjCustGeneral.First_Tele_No_2 = txtFirsttelephone2.Text;// dt.Rows[0]["First_Tele_No_2"].ToString();
        ObjCustGeneral.Second_Tele_No_2 = txtSecondTelephoneNumber2.Text;// dt.Rows[0]["Second_Tele_No_2"].ToString();
        ObjCustGeneral.Fax_NO_2 = txtFaxNumber2.Text;// dt.Rows[0]["Fax_NO_2"].ToString();
        ObjCustGeneral.Email_Address_2 = txtEmailAddress_2.Text;// dt.Rows[0]["Email_Address_2"].ToString();
        ObjCustGeneral.Email_Address2_2 = txtEmailAddress22.Text;// dt.Rows[0]["Email_Address2_2"].ToString();
        ObjCustGeneral.First_name_3 = txtFirst_name3.Text; //dt.Rows[0]["First_name_3"].ToString();
        ObjCustGeneral.Country_Key_3 = ddlCountry3.SelectedValue;//dt.Rows[0]["Country_Key_3"].ToString();
        ObjCustGeneral.Mobile_Num_3 = txtMobileNum3.Text;//dt.Rows[0]["Mobile_Num_3"].ToString();
        ObjCustGeneral.Mobile_Num2_3 = txtMobileNum23.Text;//dt.Rows[0]["Mobile_Num2_3"].ToString();
        ObjCustGeneral.First_Tele_No_3 = txtFirsttelephone3.Text;//dt.Rows[0]["First_Tele_No_3"].ToString();
        ObjCustGeneral.Second_Tele_No_3 = txtSecondTelephoneNumber3.Text;//dt.Rows[0]["Second_Tele_No_3"].ToString();
        ObjCustGeneral.Fax_NO_3 = txtFaxNumber3.Text;//dt.Rows[0]["Fax_NO_3"].ToString();
        ObjCustGeneral.Email_Address_3 = txtEmailAddress3.Text;//dt.Rows[0]["Email_Address_3"].ToString();
        ObjCustGeneral.Email_Address2_3 = txtEmailAddress23.Text;
        ObjCustGeneral.Form_address = txtForm_address.Text;
        ObjCustGeneral.Contact_person_function = txtContact_person_function.Text;
        ObjCustGeneral.Partner_language = txtPartner_language.Text;
        ObjCustGeneral.Partner_gender = txtPartner_gender.Text;
        ObjCustGeneral.Marital_Status = txtMarital_Status.Text;
        ObjCustGeneral.Date_Batch = objUtil.GetMMDDYYYY(txtDate_Batch.Text);
        ObjCustGeneral.Contact_person_department_Cust = txtContact_person_department_Cust.Text;
        ObjCustGeneral.VIP_Partner = txtVIP_Partner.Text;
        ObjCustGeneral.Partner_Authority = txtPartner_Authority.Text;
        ObjCustGeneral.Notes = txtNotes.Text;

        ObjCustGeneral.IsActive = 1;
        ObjCustGeneral.UserId = lblUserId.Text;
        ObjCustGeneral.TodayDate = objUtil.GetDate();
        ObjCustGeneral.IPAddress = objUtil.GetIpAddress();

        return ObjCustGeneral;
    }

    private void FillGeneralData()
    {
        CustomerGeneral3 ObjCustGeneral = GetCustomerGeneral3();
        if (ObjCustGeneral.Cust_General3_Id > 0)
        {

            lblCustomerGeneral3Id.Text = ObjCustGeneral.Cust_General3_Id.ToString();
            lblMasterHeaderId.Text = ObjCustGeneral.Master_Header_Id.ToString();

            txtFiscal_Year_Variant.Text = ObjCustGeneral.Fiscal_Year_Variant;
            txtReference_Account.Text = ObjCustGeneral.Reference_Account;
            txtPO_Box_city.Text = ObjCustGeneral.PO_Box_city;
            txtHierarchy_assignment.Text = ObjCustGeneral.Hierarchy_assignment;
            txtCentral_sales.Text = ObjCustGeneral.Central_sales;
            txtCustomer_condition1.Text = ObjCustGeneral.Customer_condition1;
            txtCustomer_condition2.Text = ObjCustGeneral.Customer_condition2;
            txtCustomer_condition3.Text = ObjCustGeneral.Customer_condition3;
            txtCustomer_condition4.Text = ObjCustGeneral.Customer_condition4;
            txtCustomer_condition5.Text = ObjCustGeneral.Customer_condition5;
            txtUniform_Resource.Text = ObjCustGeneral.Uniform_Resource;
            chkCentralDeletionBlock.Checked = ObjCustGeneral.Central_deletion == "true" ? true : false;
            txtUnloading_Point.Text = ObjCustGeneral.Unloading_Point;
            txtCustomer_factory.Text = ObjCustGeneral.Customer_factory;
            txtContact_person_department.Text = ObjCustGeneral.Contact_person_department;
            txtFirst_name.Text = ObjCustGeneral.First_name;

            ddlCountry.SelectedValue = ObjCustGeneral.Country_Key;
            txtMobileNum.Text = ObjCustGeneral.Mobile_Num;
            txtMobileNum2.Text = ObjCustGeneral.Mobile_Num2;
            txtFirsttelephone.Text = ObjCustGeneral.First_Tele_No;// dt.Rows[0]["First_Tele_No"].ToString();
            txtSecondTelephoneNumber.Text = ObjCustGeneral.Second_Tele_No;// dt.Rows[0]["Second_Tele_No"].ToString();
            txtFaxNumber.Text = ObjCustGeneral.Fax_NO;// dt.Rows[0]["Fax_NO"].ToString();
            txtEmailAddress.Text = ObjCustGeneral.Email_Address;// dt.Rows[0]["Email_Address"].ToString();
            txtEmailAddress2.Text = ObjCustGeneral.Email_Address2;// dt.Rows[0]["Email_Address2"].ToString();
            txtFirst_name2.Text = ObjCustGeneral.First_name_2;// dt.Rows[0]["First_name_2"].ToString();
            ddlCountry2.SelectedValue = ObjCustGeneral.Country_Key_2;// dt.Rows[0]["Country_Key_2"].ToString();
            txtMobileNum_2.Text = ObjCustGeneral.Mobile_Num_2;// dt.Rows[0]["Mobile_Num_2"].ToString();
            txtMobileNum22.Text = ObjCustGeneral.Mobile_Num2_2;// dt.Rows[0]["Mobile_Num2_2"].ToString();
            txtFirsttelephone2.Text = ObjCustGeneral.First_Tele_No_2;// dt.Rows[0]["First_Tele_No_2"].ToString();
            txtSecondTelephoneNumber2.Text = ObjCustGeneral.Second_Tele_No_2;// dt.Rows[0]["Second_Tele_No_2"].ToString();
            txtFaxNumber2.Text = ObjCustGeneral.Fax_NO_2;// dt.Rows[0]["Fax_NO_2"].ToString();
            txtEmailAddress_2.Text = ObjCustGeneral.Email_Address_2;// dt.Rows[0]["Email_Address_2"].ToString();
            txtEmailAddress22.Text = ObjCustGeneral.Email_Address2_2;// dt.Rows[0]["Email_Address2_2"].ToString();
            txtFirst_name3.Text = ObjCustGeneral.First_name_3; //dt.Rows[0]["First_name_3"].ToString();
            ddlCountry3.SelectedValue = ObjCustGeneral.Country_Key_3;//dt.Rows[0]["Country_Key_3"].ToString();
            txtMobileNum3.Text = ObjCustGeneral.Mobile_Num_3;//dt.Rows[0]["Mobile_Num_3"].ToString();
            txtMobileNum23.Text = ObjCustGeneral.Mobile_Num2_3;//dt.Rows[0]["Mobile_Num2_3"].ToString();
            txtFirsttelephone3.Text = ObjCustGeneral.First_Tele_No_3;//dt.Rows[0]["First_Tele_No_3"].ToString();
            txtSecondTelephoneNumber3.Text = ObjCustGeneral.Second_Tele_No_3;//dt.Rows[0]["Second_Tele_No_3"].ToString();
            txtFaxNumber3.Text = ObjCustGeneral.Fax_NO_3;//dt.Rows[0]["Fax_NO_3"].ToString();
            txtEmailAddress3.Text = ObjCustGeneral.Email_Address_3;//dt.Rows[0]["Email_Address_3"].ToString();
            txtEmailAddress23.Text = ObjCustGeneral.Email_Address2_3;

            txtForm_address.Text = ObjCustGeneral.Form_address;
            txtContact_person_function.Text = ObjCustGeneral.Contact_person_function;
            txtPartner_language.Text = ObjCustGeneral.Partner_language;
            txtPartner_gender.Text = ObjCustGeneral.Partner_gender;
            txtMarital_Status.Text = ObjCustGeneral.Marital_Status;
            txtDate_Batch.Text = ObjCustGeneral.Date_Batch;
            txtContact_person_department_Cust.Text = ObjCustGeneral.Contact_person_department_Cust;
            txtVIP_Partner.Text = ObjCustGeneral.VIP_Partner;
            txtPartner_Authority.Text = ObjCustGeneral.Partner_Authority;
            txtNotes.Text = ObjCustGeneral.Notes;
        }
        else
        {
            lblCustomerGeneral3Id.Text = "0";
        }
        SetMobileNumEnable();
    }

    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.General_Data_3 obj = new SectionConfiguration.General_Data_3();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    #endregion
}