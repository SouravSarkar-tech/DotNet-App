using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
public partial class Transaction_Vendor_VendorGeneral2 : System.Web.UI.Page
{
    VendorGeneralAccess ObjVendorGeneralAccess = new VendorGeneralAccess();

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

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();

                    FillGeneralData();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    string mode = Session[StaticKeys.Mode].ToString();
                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                    }
                    ConfigureControl();
                    //Make GSTN mandatory if vendor class is selected and other than Not registered Start
                    GSTNoValidation();
                    //Make GSTN mandatory if vendor class is selected and other than Not registered End
                }
                else
                {
                    Response.Redirect("VendorMaster.aspx");
                }
            }
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CheckRegEx())
        {
            if (CheckIsValid())
            {
                if (SaveVendorGeneral())
                {
                    string pageURL = btnPrevious.CommandArgument.ToString();
                    Response.Redirect(pageURL);
                }
            }
            else
            {
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //GST Changes Start
        if (CheckVendorClass())
        {
            //GST Changes End
            if (CheckRegEx())
            {
                if (CheckIsValid())
                {
                    if (SaveVendorGeneral())
                    {
                        lblMsg.Text = Messages.GetMessage(1);
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;

                        Response.Redirect("VendorGeneral2.aspx");
                    }
                }
                else
                {
                    lblMsg.Text = "Please fill atleast one feild.";
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }
            }
            //GST Changes Start
        }
        //GST Changes End
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (CheckRegEx())
        {
            if (CheckIsValid())
            {
                if (SaveVendorGeneral())
                {
                    string pageURL = btnNext.CommandArgument.ToString();
                    Response.Redirect(pageURL);
                }
            }
            else
            {
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
    }

    //Make GSTN mandatory if vendor class is selected and other than Not registered Start
    protected void ddlVendorClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GSTNoValidation();
    }        
    //Make GSTN mandatory if vendor class is selected and other than Not registered End

    #endregion

    #region Methods

    private bool SaveVendorGeneral()
    {
        bool flg = false;

        VendorGeneral2 ObjVendorGeneral = GetControlsValue();
        if (ObjVendorGeneralAccess.Save2(ObjVendorGeneral) > 0)
        {
            flg = true;
        }
        else
        {
            lblMsg.Text = Messages.GetMessage(-1);
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

        return flg;

    }

    private VendorGeneral2 GetVendorGeneral()
    {
        return ObjVendorGeneralAccess.GetVendorGeneral2(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private VendorGeneral2 GetControlsValue()
    {
        VendorGeneral2 ObjVendorGeneral = new VendorGeneral2();
        Utility objUtil = new Utility();

        ObjVendorGeneral.Vendor_General2_Id = Convert.ToInt32(lblVendorGeneralId.Text);
        ObjVendorGeneral.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        ObjVendorGeneral.Industry_key = txtIndustry_key.Text;
        ObjVendorGeneral.VAT_Registration_Number = txtVAT_Registration_Number.Text;
        ObjVendorGeneral.PlaceBirth_WithholdingTax = txtPlaceBirth_WithholdingTax.Text;
        ObjVendorGeneral.DateBatch_Input = objUtil.GetMMDDYYYY(txtDateBatch_Input.Text);
        ObjVendorGeneral.KeySex_PersonWithholding_Tax = txtKeySex_PersonWithholding_Tax.Text;
        ObjVendorGeneral.Tax_Jurisdiction = txtTax_Jurisdiction.Text;
        ObjVendorGeneral.Plant = txtPlant.Text;
        ObjVendorGeneral.Transportation_Zone_Goods = txtTransportation_Zone_Goods.Text;
        ObjVendorGeneral.Service_AgentProcedure_Group = txtService_AgentProcedure_Group.Text;
        ObjVendorGeneral.Tax_Type = txtTax_Type.Text;
        ObjVendorGeneral.Tax_Number_Type = txtTax_Number_Type.Text;
        ObjVendorGeneral.Tax_Number1 = txtTaxNumber1.Text;
        ObjVendorGeneral.Tax_Number2 = txtTaxNumber2.Text;
        ObjVendorGeneral.Tax_Numbe_3 = txtTax_Numbe_3.Text;
        ObjVendorGeneral.Tax_Numbe_4 = txtTax_Numbe_4.Text;
        ObjVendorGeneral.Type_of_Business = txtTypeOfBusiness.Text;
        if (chkTax_Split.Enabled)
            ObjVendorGeneral.Tax_Split = chkTax_Split.Checked == true ? "1" : "0";
        ObjVendorGeneral.External_Manufacturer_CodeNumber = txtExternal_Manufacturer_CodeNumber.Text;
        ObjVendorGeneral.Name_Representative = txtName_Representative.Text;
        ObjVendorGeneral.Type_Industry = txtType_Industry.Text;
        if (chkCentral_Deletion_MasterRecord.Enabled)
            ObjVendorGeneral.Central_Deletion_MasterRecord = chkCentral_Deletion_MasterRecord.Checked == true ? "1" : "0";
        ObjVendorGeneral.DateBatch_Input2 = objUtil.GetMMDDYYYY(txtDateBatch_Input.Text);
        ObjVendorGeneral.VendorIndicator_Relevant = txtVendorIndicator_Relevant.Text;
        ObjVendorGeneral.Name_1 = txtName_1.Text;
        ObjVendorGeneral.Name_2 = txtName_2.Text;
        ObjVendorGeneral.Name_3 = txtName_3.Text;
        ObjVendorGeneral.First_Name = txtFirst_Name.Text;
        ObjVendorGeneral.Title = txtTitle.Text;
        ObjVendorGeneral.FactoryCalendar_key = txtFactoryCalendar_key.Text;
        ObjVendorGeneral.Transportation_Chain = txtTransportation_Chain.Text;
        ObjVendorGeneral.StagingTime_Days_BatchInput = txtStagingTime_Days_BatchInput.Text;
        if (chkCrossDocking_Relevant_CollectiveNumbering.Enabled)
            ObjVendorGeneral.CrossDocking_Relevant_CollectiveNumbering = chkCrossDocking_Relevant_CollectiveNumbering.Checked == true ? "1" : "0";
        if (chkScheduling_Procedure.Enabled)
            ObjVendorGeneral.Scheduling_Procedure = chkScheduling_Procedure.Checked == true ? "1" : "0";
        ObjVendorGeneral.Tax_Number_5 = txtTax_Number_5.Text;
        ObjVendorGeneral.ECC_Number = txtECCNumber.Text;
        ObjVendorGeneral.Excise_Registration_No = txtExciseRegistrationNo.Text;
        ObjVendorGeneral.Excise_Range = txtExciseRange.Text;
        ObjVendorGeneral.Excise_Division = txtExciseDivision.Text;
        ObjVendorGeneral.Excise_Commissionerate = txtExciseCommissionerate.Text;
        //GST Changes
        ObjVendorGeneral.GSTNo = txtGSTNo.Text;
        ObjVendorGeneral.VendorClass = ddlVendorClass.SelectedValue;
        //GST Changes

        ObjVendorGeneral.IsActive = 1;
        ObjVendorGeneral.UserId = lblUserId.Text;
        ObjVendorGeneral.TodayDate = objUtil.GetDate();
        ObjVendorGeneral.IPAddress = objUtil.GetIpAddress();

        return ObjVendorGeneral;
    }

    private void FillGeneralData()
    {
        VendorGeneral2 ObjVendorGeneral = GetVendorGeneral();
        if (ObjVendorGeneral.Vendor_General2_Id > 0)
        {
            lblVendorGeneralId.Text = ObjVendorGeneral.Vendor_General2_Id.ToString();
            lblMasterHeaderId.Text = ObjVendorGeneral.Master_Header_Id.ToString();
            txtIndustry_key.Text = ObjVendorGeneral.Industry_key;
            txtVAT_Registration_Number.Text = ObjVendorGeneral.VAT_Registration_Number;
            txtPlaceBirth_WithholdingTax.Text = ObjVendorGeneral.PlaceBirth_WithholdingTax;
            txtDateBatch_Input.Text = ObjVendorGeneral.DateBatch_Input == "01/01/1900" ? "" : ObjVendorGeneral.DateBatch_Input;
            txtKeySex_PersonWithholding_Tax.Text = ObjVendorGeneral.KeySex_PersonWithholding_Tax;
            txtTax_Jurisdiction.Text = ObjVendorGeneral.Tax_Jurisdiction;
            txtPlant.Text = ObjVendorGeneral.Plant;
            txtTransportation_Zone_Goods.Text = ObjVendorGeneral.Transportation_Zone_Goods;
            txtService_AgentProcedure_Group.Text = ObjVendorGeneral.Service_AgentProcedure_Group;
            txtTax_Type.Text = ObjVendorGeneral.Tax_Type;
            txtTax_Number_Type.Text = ObjVendorGeneral.Tax_Number_Type;
            txtTaxNumber1.Text = ObjVendorGeneral.Tax_Number1.ToString();
            txtTaxNumber2.Text = ObjVendorGeneral.Tax_Number2.ToString();
            txtTax_Numbe_3.Text = ObjVendorGeneral.Tax_Numbe_3;
            txtTax_Numbe_4.Text = ObjVendorGeneral.Tax_Numbe_4;
            txtTypeOfBusiness.Text = ObjVendorGeneral.Type_of_Business;
            chkTax_Split.Checked = ObjVendorGeneral.Tax_Split.ToLower() == "true" ? true : false;
            txtExternal_Manufacturer_CodeNumber.Text = ObjVendorGeneral.External_Manufacturer_CodeNumber;
            txtName_Representative.Text = ObjVendorGeneral.Name_Representative;
            txtType_Industry.Text = ObjVendorGeneral.Type_Industry;
            chkCentral_Deletion_MasterRecord.Checked = ObjVendorGeneral.Central_Deletion_MasterRecord.ToLower() == "true" ? true : false;
            txtDateBatch_Input2.Text = ObjVendorGeneral.DateBatch_Input2 == "01/01/1900" ? "" : ObjVendorGeneral.DateBatch_Input2;
            txtVendorIndicator_Relevant.Text = ObjVendorGeneral.VendorIndicator_Relevant;
            txtName_1.Text = ObjVendorGeneral.Name_1;
            txtName_2.Text = ObjVendorGeneral.Name_2;
            txtName_3.Text = ObjVendorGeneral.Name_3;
            txtFirst_Name.Text = ObjVendorGeneral.First_Name;
            txtTitle.Text = ObjVendorGeneral.Title;
            txtFactoryCalendar_key.Text = ObjVendorGeneral.FactoryCalendar_key;
            txtTransportation_Chain.Text = ObjVendorGeneral.Transportation_Chain;
            txtStagingTime_Days_BatchInput.Text = ObjVendorGeneral.StagingTime_Days_BatchInput;
            chkCrossDocking_Relevant_CollectiveNumbering.Checked = ObjVendorGeneral.CrossDocking_Relevant_CollectiveNumbering.ToLower() == "true" ? true : false;
            chkScheduling_Procedure.Checked = ObjVendorGeneral.Scheduling_Procedure.ToLower() == "true" ? true : false;
            txtTax_Number_5.Text = ObjVendorGeneral.Tax_Number_5;
            txtECCNumber.Text = ObjVendorGeneral.ECC_Number;
            txtExciseRegistrationNo.Text = ObjVendorGeneral.Excise_Registration_No;
            txtExciseRange.Text = ObjVendorGeneral.Excise_Range;
            txtExciseDivision.Text = ObjVendorGeneral.Excise_Division;
            txtExciseCommissionerate.Text = ObjVendorGeneral.Excise_Commissionerate;
            //GST Changes
            txtGSTNo.Text = ObjVendorGeneral.GSTNo;
            ddlVendorClass.SelectedValue = ObjVendorGeneral.VendorClass;
            //GST Changes
        }
        else
        {
            lblVendorGeneralId.Text = "0";
            //GST Changes
            VendorGeneral1 Objvg1 = ObjVendorGeneralAccess.GetVendorGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
            if (Objvg1.ImpVendor.ToString() == "S")
                ddlVendorClass.SelectedValue = "0";
            else if (Objvg1.ImpVendor.ToString() == "G")
                ddlVendorClass.SelectedValue = "";
            else if ((Objvg1.CountryKey.ToString() == "1") && (Objvg1.ImpVendor.ToString() != "S") && (Objvg1.ImpVendor.ToString() != "G"))
            {
                ddlVendorClass.SelectedValue = "";
            }
            //GST Changes
        }
        txtTaxNumber1.Focus();
        
    }

    private void ConfigureControl()
    {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Vendor_General_2 obj = new SectionConfiguration.Vendor_General_2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));

        VendorGeneral1 Objvg1 = ObjVendorGeneralAccess.GetVendorGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));

        lblVendorCategory.Text = Objvg1.Vendor_Category;

        if (lblVendorCategory.Text == "GOVT")
        {
            reqtxtTax_Number_5.Enabled = false;
            labletxtTax_Number_5.Visible = false;
        }

        if (Objvg1.Region != "0")
        {
            switch (Objvg1.Region)
            {
                case "709":
                    lblTinNo.Text = "37";
                    break;
                case "710":
                    lblTinNo.Text = "12";
                    break;
                case "711":
                    lblTinNo.Text = "18";
                    break;
                case "712":
                    lblTinNo.Text = "10";
                    break;
                case "713": lblTinNo.Text = "30"; break;
                case "714": lblTinNo.Text = "24"; break;
                case "715": lblTinNo.Text = "06"; break;
                case "716": lblTinNo.Text = "02"; break;
                case "717": lblTinNo.Text = "01"; break;
                case "718": lblTinNo.Text = "29"; break;
                case "719": lblTinNo.Text = "32"; break;
                case "720": lblTinNo.Text = "23"; break;
                case "721": lblTinNo.Text = "27"; break;
                case "722": lblTinNo.Text = "14"; break;
                case "723": lblTinNo.Text = "17"; break;
                case "724": lblTinNo.Text = "15"; break;
                case "725": lblTinNo.Text = "13"; break;
                case "726": lblTinNo.Text = "21"; break;
                case "727": lblTinNo.Text = "03"; break;
                case "728": lblTinNo.Text = "08"; break;
                case "729": lblTinNo.Text = "11"; break;
                case "730": lblTinNo.Text = "33"; break;
                case "731": lblTinNo.Text = "16"; break;
                case "732": lblTinNo.Text = "09"; break;
                case "733": lblTinNo.Text = "19"; break;
                case "734": lblTinNo.Text = "35"; break;
                case "735": lblTinNo.Text = "04"; break;
                case "736": lblTinNo.Text = "26"; break;
                case "737": lblTinNo.Text = "25"; break;
                case "738": lblTinNo.Text = "07"; break;
                case "739": lblTinNo.Text = "31"; break;
                case "740": lblTinNo.Text = "34"; break;
                case "741": lblTinNo.Text = "22"; break;
                case "742": lblTinNo.Text = "20"; break;
                case "743": lblTinNo.Text = "05"; break;
                case "1683": lblTinNo.Text = "36"; break;

                default:
                    lblTinNo.Text = "";
                    break;
            }

            if (lblTinNo.Text != "")
                lblTinNo.Text = "^(" + lblTinNo.Text + ")";

            //GST changes Start
            switch (Objvg1.Region)
            {
                case "709":
                    lblGstNo.Text = "28";  //Andhra Pradesh
                    break;
                case "710":
                    lblGstNo.Text = "12";  //Arunachal pradesh
                    break;
                case "711":
                    lblGstNo.Text = "18";  //Assam 
                    break;
                case "712":
                    lblGstNo.Text = "10";  //Bihar
                    break;
                case "713": lblGstNo.Text = "30"; break;   //Goa
                case "714": lblGstNo.Text = "24"; break;   //Gujrat
                case "715": lblGstNo.Text = "06"; break;   //Harayana
                case "716": lblGstNo.Text = "02"; break;   //Himachal pradesh
                case "717": lblGstNo.Text = "01"; break;   //J&K
                case "718": lblGstNo.Text = "29"; break;   //Karnataka
                case "719": lblGstNo.Text = "32"; break;   //Kerala
                case "720": lblGstNo.Text = "23"; break;   //MP
                case "721": lblGstNo.Text = "27"; break;   //MH
                case "722": lblGstNo.Text = "14"; break;   //Manipur
                case "723": lblGstNo.Text = "17"; break;   //Meghalaya
                case "724": lblGstNo.Text = "15"; break;   //Mizoram
                case "725": lblGstNo.Text = "13"; break;   //Nagaland
                case "726": lblGstNo.Text = "21"; break;   //Odhisha
                case "727": lblGstNo.Text = "03"; break;   //Punjab
                case "728": lblGstNo.Text = "08"; break;   //Rajasthan
                case "729": lblGstNo.Text = "11"; break;   //Sikkim
                case "730": lblGstNo.Text = "33"; break;   //TN
                case "731": lblGstNo.Text = "16"; break;   //Tripura
                case "732": lblGstNo.Text = "09"; break;   //UP
                case "733": lblGstNo.Text = "19"; break;   //WB
                case "734": lblGstNo.Text = "35"; break;  //Andaman und Nico.In.
                case "735": lblGstNo.Text = "04"; break;  //Chandigarh
                case "736": lblGstNo.Text = "26"; break;  //Dadra und Nagar Hav.
                case "737": lblGstNo.Text = "25"; break;  //Daman Diu
                case "738": lblGstNo.Text = "07"; break;   //Delhi
                case "739": lblGstNo.Text = "31"; break;  //Lakshadweep Islands
                case "740": lblGstNo.Text = "34"; break;  //Pondicherry
                case "741": lblGstNo.Text = "22"; break;  //Chattisgarh
                case "742": lblGstNo.Text = "20"; break;   //Jharkhand
                case "743": lblGstNo.Text = "05"; break;   //Uttarakhand
                case "1683": lblGstNo.Text = "36"; break;   // Telangana


                default:
                    lblGstNo.Text = "";
                    break;
            }

            if (lblGstNo.Text != "")
                lblGstNo.Text = "^(" + lblGstNo.Text + ")";
           
            //GST changes End
        }
    }

    private bool CheckIsValid()
    {
        bool flag = false;

        if (lblActionType.Text != "C")
            flag = true;
        else
        {
            if (txtTaxNumber1.Text != "")
                flag = true;
            else if (txtTaxNumber2.Text != "")
                flag = true;
            else if (txtTax_Numbe_3.Text != "")
                flag = true;
            else if (txtTax_Numbe_4.Text != "")
                flag = true;
            else if (txtTax_Number_5.Text != "")
                flag = true;
            else if (txtType_Industry.Text != "")
                flag = true;
            else if (txtTypeOfBusiness.Text != "")
                flag = true;
            else if (txtVAT_Registration_Number.Text != "")
                flag = true;
            else if (txtECCNumber.Text != "")
                flag = true;
            else if (txtExciseRegistrationNo.Text != "")
                flag = true;
            else if (txtExciseRange.Text != "")
                flag = true;
            else if (txtExciseDivision.Text != "")
                flag = true;
            else if (txtExciseCommissionerate.Text != "")
                flag = true;
            //GST Changes
            else if (txtGSTNo.Text != "")
                flag = true;
            //GST Changes
        }

        return flag;
    }

    private bool CheckRegEx()
    {
        bool flg = true;
        string ErrorMsg = "";

        //System.Text.RegularExpressions.Regex.IsMatch("23060012470", "24[\\w]{2,12}");

        if (!System.Text.RegularExpressions.Regex.IsMatch(txtTax_Number_5.Text, "[A-Za-z]{3}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[A-Za-z]{1}[\\d]{4}[A-Za-z]{1}") && txtTax_Number_5.Text.ToUpper() != "" && reqtxtTax_Number_5.Visible)
        {
            ErrorMsg = "Invalid Pan.";
            flg = false;
        }
        //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtTaxNumber1.Text.ToUpper(), lblTinNo.Text + "[\\w]{2,12}") && (txtTaxNumber1.Text.ToUpper() != "NA" || lblVendorCategory.Text == "TRD" || lblVendorCategory.Text == "MFG") && reqtxtTaxNumber1.Visible)
        //{
        //    ErrorMsg = "Invalid CST No.";
        //    flg = false;
        //}
        //GST Changes remove validation
        //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtTax_Numbe_3.Text.ToUpper(), lblTinNo.Text + "[\\w]{2,12}$") && (txtTax_Numbe_3.Text.ToUpper() != "NA" || lblVendorCategory.Text == "TRD" || lblVendorCategory.Text == "MFG") && lblActionType.Text != "C" && reqtxtTax_Numbe_3.Visible)
        //{
        //    ErrorMsg = "Invalid LST/ VAT No.";
        //    flg = false;
        //}
        //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtTypeOfBusiness.Text.ToUpper(), txtTax_Number_5.Text.ToUpper() + "[A-Za-z]{2}[\\d]{3}$") && txtTypeOfBusiness.Text.ToUpper() != "NA" && lblActionType.Text != "C" && reqtxtTypeOfBusiness.Visible)
        //{
        //    ErrorMsg = "Invalid Service Tax No.";
        //    flg = false;
        //}
        //GST Changes remove validation
        //GST Changes 
        //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && (txtGSTNo.Text.ToUpper() != "NA") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
        //{
        //    ErrorMsg = "Invalid GST No.";
        //    flg = false;
        //}
        else
        {
            bool flgDigit = true;

            if (reqtxtGSTNo.Visible)
            {
                if (lblGstNo.Text == "^(28)")
                {
                    if (!lblGstNo.Text.Contains(txtGSTNo.Text.Substring(0, 2).ToString()))
                    {
                        lblGstNo.Text = "^(37)";
                        if (!lblGstNo.Text.Contains(txtGSTNo.Text.Substring(0, 2).ToString()))
                        {
                            ErrorMsg = "First two digit codes of GST No not matching with vendor region code";
                            flgDigit = false;
                        }
                    }

                }
                else if (!lblGstNo.Text.Contains(txtGSTNo.Text.Substring(0, 2).ToString()))
                {
                    ErrorMsg = "First two digit codes of GST No not matching with vendor region code";
                    flgDigit = false;
                }

                if (flgDigit)
                {
                    if (lblGstNo.Text == "^(28)")
                    {
                        //if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && (txtGSTNo.Text.ToUpper() != "NA") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
                        if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
                        {
                            lblGstNo.Text = "^(37)";
                            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && (txtGSTNo.Text.ToUpper() != "NA") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
                            if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
                            {
                                ErrorMsg = "Invalid GSTN. GSTN should be 15 digit alphanumeric with first two digits equal to the selected region code, next 10 digits equal to PAN number and last 3 digits random alphanumeric characters.";
                                flg = false;
                            }
                        }
                    }
                    //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && (txtGSTNo.Text.ToUpper() != "NA") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(txtGSTNo.Text.ToUpper(), lblGstNo.Text + txtTax_Number_5.Text.ToUpper() + "[\\w]{3}$") && lblActionType.Text != "C" && reqtxtGSTNo.Visible)
                    {
                        ErrorMsg = "Invalid GSTN. GSTN should be 15 digit alphanumeric with first two digits equal to the selected region code, next 10 digits equal to PAN number and last 3 digits random alphanumeric characters.";
                        flg = false;
                    }
                }
                else
                    flg = false;
            }            
        }
        //GST Changes
        

        if (!flg)
        {
            lblMsg.Text = ErrorMsg;
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }

        return flg;
    }

    //Make GSTN mandatory if vendor class is selected and other than Not registered Start
    private void GSTNoValidation()
    {
        VendorGeneral1 Objvg1 = ObjVendorGeneralAccess.GetVendorGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
               
        //if (Objvg1.CountryKey.ToString() == "1")
        if ((Objvg1.CountryKey.ToString() == "1") && (Objvg1.ImpVendor.ToString() != "S") && (Objvg1.ImpVendor.ToString() != "G"))
        {
            reqddlVendorClass.Enabled = true;
            lableddlVendorClass.Visible = true;
            reqddlVendorClass.Visible = true;
        }
        else
        {
            reqddlVendorClass.Enabled = false;
            lableddlVendorClass.Visible = false;
            reqddlVendorClass.Visible = false;            
            txtGSTNo.Text = "";
        }

        if ((reqddlVendorClass.Enabled == true) && (ddlVendorClass.SelectedValue == "" || ddlVendorClass.SelectedValue == "1" || ddlVendorClass.SelectedValue == "2"))
        {
            reqtxtGSTNo.Enabled = true;
            labletxtGSTNo.Visible = true;
            reqtxtGSTNo.Visible = true;
        }
        else
        {
            reqtxtGSTNo.Enabled = false;
            labletxtGSTNo.Visible = false;
            reqtxtGSTNo.Visible = false;
            txtGSTNo.Text = "";
        }
    }

    private bool CheckVendorClass()
    {
        bool flg = true;
        string ErrorMsg = "";

        VendorGeneral1 Objvg1 = ObjVendorGeneralAccess.GetVendorGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
        if (Objvg1.ImpVendor.ToString() == "S" && ddlVendorClass.SelectedValue != "0")
        {
            ErrorMsg = "For Service Vendor classification should be non-registered by default.";
            flg = false;
        }   
        else if (Objvg1.ImpVendor.ToString() == "G" && ddlVendorClass.SelectedValue != "")
        {
            ErrorMsg = "For Goods Vendor classification should be Registered by default.";
            flg = false;
        }            
       
        if (!flg)
        {
            lblMsg.Text = ErrorMsg;
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
        return flg;
    }
    //Make GSTN mandatory if vendor class is selected and other than Not registered End

    #endregion
}