using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

public partial class Transaction_UserControl_VendorChangeExcelUpload : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                //lblMatPlantGrpId.Text = Session[StaticKeys.MatPlantGrp].ToString();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SaveImport())
        {
            Response.Redirect("VendorChange.aspx");
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Error Occured during Updation";

            ModalPopupExtenderI.Show();
        }
    }

    protected void grvData1_DataBound(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;

        foreach (GridViewRow grv in grvData1.Rows)
        {
            string msg = "";

            TextBox txtVendorCode = (TextBox)grv.FindControl("txtVendorCode");
            TextBox txtVendorName = (TextBox)grv.FindControl("txtVendorName");
            DropDownList ddlVendorAccGrp = (DropDownList)grv.FindControl("ddlVendorAccGrp");

            DropDownList ddlCompanyCode = (DropDownList)grv.FindControl("ddlCompanyCode");
            TextBox txtOldValue = (TextBox)grv.FindControl("txtOldValue");
            TextBox txtNewValue = (TextBox)grv.FindControl("txtNewValue");

            TextBox txtRemarks = (TextBox)grv.FindControl("txtRemarks");

            if (txtVendorCode.Text == "" && txtVendorName.Text == "" && txtOldValue.Text == "")
            {
                grv.Visible = false;
            }
            else
            {
                i++;
                HelperAccess helperAccess = new HelperAccess();

                if (txtVendorCode.Text == "")
                {
                    msg = msg + "Vendor Code Mandatory.";
                }
                //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtVendorCode.Text, "^[\\d]{6,10}$"))
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txtVendorCode.Text, "^[\\S]{4,10}$"))
                {
                    msg = msg + "Vendor Code Invalid.";
                }

                if (msg == "")
                {

                    helperAccess.PopuplateDropDownList(ddlVendorAccGrp, "pr_GetModuleListByModuleType 'V'", "Module_Name", "Module_Id", "");
                    VendorCodeWiseSetup(txtVendorCode, ddlVendorAccGrp);

                    if (ddlVendorAccGrp.SelectedValue == "")
                    {
                        msg = msg + "Vendor Code Invalid.";
                    }


                    //if (ddlVendorAccGrp.SelectedValue != "15" || ddlVendorAccGrp.SelectedValue != "26" || ddlVendorAccGrp.SelectedValue != "27" || ddlVendorAccGrp.SelectedValue != "241")
                    if (ddlVendorAccGrp.SelectedValue != "15" && ddlVendorAccGrp.SelectedValue != "26" && ddlVendorAccGrp.SelectedValue != "27" && ddlVendorAccGrp.SelectedValue != "241")
                    {
                        msg = msg + "Lupin has started vendor onboarding process on the new tool ‘SAP Ariba SLP’. http://lupin.procurement.ariba.com/";
                    }


                    //else if (ddlVendorAccGrp.SelectedValue != Session[StaticKeys.MaterialProcessModuleId].ToString())
                    //{
                    //    msg = msg + "Please enter only " + ddlVendorAccGrp.Items.FindByValue(Session[StaticKeys.MaterialProcessModuleId].ToString()).Text;
                    //}


                    if (txtVendorName.Text == "")
                    {
                        msg = msg + "Vendor Description Mandatory.";
                    }

                    if (txtOldValue.Text == "")
                    {
                        msg = msg + "Old Value is Mandatory";
                    }

                    if (txtNewValue.Text == "")
                    {
                        msg = msg + "New Value is Mandatory";
                    }

                    if (txtRemarks.Text == "")
                    {
                        msg = msg + "Remarks is mandatory";
                    }
                }

                if (msg == "")
                {
                    Label lblCompanyCode = (Label)grv.FindControl("lblCompanyCode");

                    //DropDownList ddlCompanyCode = (DropDownList)grv.FindControl("ddlCompanyCode");
                    //Depot Srinidhi
                    //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                    //if (Session[StaticKeys.MaterialPlantId].ToString() == "8888" || Session[StaticKeys.MaterialPlantId].ToString() == "8889")
                    //    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList_ByDeptType '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                    //else
                    //    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                    string[] CompanyCode = lblCompanyCode.Text.Split('-');
                    helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");

                    if (lblCompanyCode.Text != "")
                    {
                        //if (ddlCompanyCode.Items.FindByValue(lblCompanyCode.Text) == null)
                        if (ddlCompanyCode.Items.FindByValue(CompanyCode[0].Trim()) == null)
                        {
                            msg = msg + "Invalid Company Code.";
                        }
                        else
                        {
                            //ddlCompanyCode.SelectedValue = lblCompanyCode.Text;
                            ddlCompanyCode.SelectedValue = CompanyCode[0].Trim();
                        }
                    }
                    else
                    {
                        msg = msg + "Company Code Mandatory";
                    }

                }

                if (msg == "")
                {
                    Label lblPurchaseOrg = (Label)grv.FindControl("lblPurchaseOrg");
                    DropDownList ddlPurchaseOrg = (DropDownList)grv.FindControl("ddlPurchaseOrg");

                    string[] PurchaseOrg = lblPurchaseOrg.Text.Split('-');
                    //helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '0','SD1','0'", "Sales_Organization_Name", "Sales_Organization_Code", "");
                    helperAccess.PopuplateDropDownList(ddlPurchaseOrg, "pr_GetPurchaseOrgList 0", "Purchase_Org_Name", "Purchase_Org_Id");
                    if (ddlPurchaseOrg.Items.FindByValue(PurchaseOrg[0].Trim()) == null)
                    //if (ddlPurchaseOrg.Items.FindByValue(lblPurchaseOrg.Text) == null)
                    {
                        msg = msg + "Invalid Purchase Organization.";
                    }
                    else
                    {
                        ddlPurchaseOrg.SelectedValue = PurchaseOrg[0].Trim();
                        //ddlPurchaseOrg.SelectedValue = lblPurchaseOrg.Text;
                    }
                }

                if (msg == "")
                {
                    Label lblSection = (Label)grv.FindControl("lblSection");
                    DropDownList ddlSection = (DropDownList)grv.FindControl("ddlSection");

                    string[] Section = lblSection.Text.Split('-');
                    //helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
                    helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlVendorAccGrp.SelectedValue + "','" + lblUserId.Text + "'", "Section_Name", "Section_Id");

                    if (lblSection.Text == "")
                    {
                        msg = msg + "Section is Mandatory.";
                    }
                    else if (ddlSection.Items.FindByValue(Section[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Section.";
                    }
                    else
                    {
                        ddlSection.SelectedValue = Section[0].Trim();
                    }


                    if (msg == "")
                    {
                        Label lblField = (Label)grv.FindControl("lblField");
                        DropDownList ddlField = (DropDownList)grv.FindControl("ddlField");
                        //DropDownList ddlCompanyCode = (DropDownList)grv.FindControl("ddlCompanyCode");
                        string[] Field = lblField.Text.Split('-');
                        //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
                        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlVendorAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

                        if (lblField.Text == "")
                        {
                            msg = msg + "Field is Mandatory.";
                        }
                        else if (ddlField.Items.FindByValue(Field[0].Trim()) == null)
                        {
                            msg = msg + "Invalid Field.";
                        }
                        else
                        {
                            ddlField.SelectedValue = Field[0].Trim();
                        }
                    }
                }

                Panel pnlMsg = (Panel)grv.FindControl("pnlMsg");

                Label lblMsg1 = (Label)grv.FindControl("lblMsg");

                if (msg == "")
                {
                    lblMsg1.Text = "OK";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                else
                {


                    flg = false;
                    lblMsg1.Text = msg;
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
        }

        if (i > 0)
            btnAdd.Enabled = flg;
    }

    protected void Process_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        DataSet ds = new DataSet();

        string StrPath = "~/Transaction/Vendor/VendorDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

        //HttpFileCollection fileCollection = Request.Files;

        if (fileUpload.HasFile)
        {
            HttpPostedFile uploadfile = fileUpload.PostedFile;

            try
            {

                if (uploadfile.ContentLength > 0)
                {
                    string fileExtension = System.IO.Path.GetExtension(uploadfile.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/tempfile/") + Session[StaticKeys.RequestNo].ToString() + fileExtension;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers();
                            System.IO.File.Delete(fileLocation);
                        }
                        fileUpload.SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";


                            //DataTable dt1 = readExcel(excelConnectionString, "Select * from [Sheet1$]");

                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);


                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        //dt = readExcel(excelConnectionString, "Select * from [Sheet1$]");

                        if (dt == null)
                        {
                            return;
                        }

                        //ds.Tables.Add(dt);

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [Sheet1$]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }

                    grvData1.DataSource = ds;
                }
                else
                {
                    grvData1.DataSource = null;
                }

                grvData1.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlMsg.Visible = false;
                    ModalPopupExtenderI.Show();
                }
                else
                {
                    lblMsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;

                lblMsg.Text = "Error Occured \\Invalid Format.Please download and use the Excel format provided above.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to be uploaded";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

    }

    private DataTable readExcel(string ExcelConnectionStr, string sql)
    {
        OleDbConnection conn = new OleDbConnection(ExcelConnectionStr);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        OleDbDataAdapter adp = new OleDbDataAdapter();
        adp.SelectCommand = cmd;
        DataTable dt = new DataTable();

        try
        {
            adp.FillSchema(dt, SchemaType.Source);
            adp.Fill(dt);
        }
        catch
        {

        }
        return dt;
    }

    protected void VendorCodeWiseSetup(TextBox txtVendorCode, DropDownList ddlVendorAccGrp)
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
               // regtxtVendorCode.ValidationExpression = "^[\\S]{4,10}$";
                ddlVendorAccGrp.SelectedValue = "241";
                break;
            //8400000388 E
            case "L":
                //regtxtVendorCode.ValidationExpression = "^[\\S]{4}$";
                ddlVendorAccGrp.SelectedValue = "26";

                break;
            case "E":
                //regtxtVendorCode.ValidationExpression = "^[\\S]{7,10}$";
                ddlVendorAccGrp.SelectedValue = "27";

                break;

            default:
                //regtxtVendorCode.ValidationExpression = "^[\\d]{6}$";
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
                else if ((strcode >= 600000 && strcode < 650000) || (strcode >= 678001 && strcode < 699999)) //6-Series & 67-series
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


    }

    protected bool SaveImport()
    {
        bool flg = true;
        foreach (GridViewRow grv in grvData1.Rows)
        {
            Label lblCompanyCode = (Label)grv.FindControl("lblCompanyCode");
            Label lblBlockType = (Label)grv.FindControl("lblBlockType");
            Label lblPurchaseOrg = (Label)grv.FindControl("lblPurchaseOrg");
            Label lblMsg = (Label)grv.FindControl("lblMsg");

            RadioButtonList rdlBlockValue = (RadioButtonList)grv.FindControl("rdlBlockValue");



            TextBox txtVendorCode = (TextBox)grv.FindControl("txtVendorCode");
            TextBox txtVendorName = (TextBox)grv.FindControl("txtVendorName");
            DropDownList ddlVendorAccGrp = (DropDownList)grv.FindControl("ddlVendorAccGrp");

            DropDownList ddlCompanyCode = (DropDownList)grv.FindControl("ddlCompanyCode");

            DropDownList ddlPurchaseOrg = (DropDownList)grv.FindControl("ddlPurchaseOrg");

            DropDownList ddlSection = (DropDownList)grv.FindControl("ddlSection");
            DropDownList ddlField = (DropDownList)grv.FindControl("ddlField");

            TextBox txtOldValue = (TextBox)grv.FindControl("txtOldValue");
            TextBox txtNewValue = (TextBox)grv.FindControl("txtNewValue");

            TextBox txtRemarks = (TextBox)grv.FindControl("txtRemarks");

            VendorChangeAccess ObjVendorChangeAccess = new VendorChangeAccess();

            VendorChange ObjVendorChange = new VendorChange();
            Utility objUtil = new Utility();

            ObjVendorChange.Vendor_Change_Id = 0;
            ObjVendorChange.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            if (txtVendorCode.Text != "")
            {
                //if (ddlVendorAccGrp.SelectedValue != "15" || ddlVendorAccGrp.SelectedValue != "26" || ddlVendorAccGrp.SelectedValue != "27" || ddlVendorAccGrp.SelectedValue != "241")
                if (ddlVendorAccGrp.SelectedValue != "15" && ddlVendorAccGrp.SelectedValue != "26" && ddlVendorAccGrp.SelectedValue != "27" && ddlVendorAccGrp.SelectedValue != "241")
                {
                    //msg = msg + "Lupin has started vendor onboarding process on the new tool ‘SAP Ariba SLP’. http://lupin.procurement.ariba.com/";
                    lblMsg.Text = "Lupin has started vendor onboarding process on the new tool ‘SAP Ariba SLP’. http://lupin.procurement.ariba.com/";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
                else
                {
                    ObjVendorChange.Customer_Code = txtVendorCode.Text;
                    ObjVendorChange.Company_Code = ddlCompanyCode.SelectedValue;
                    ObjVendorChange.Vendor_Desc = txtVendorName.Text;
                    ObjVendorChange.Vendor_Group = ddlVendorAccGrp.SelectedValue;
                    ObjVendorChange.Purchase_Org = ddlPurchaseOrg.SelectedValue;
                    //ObjVendorChange.Distribution_Channel_Id = ddlDistributionChannel.SelectedValue;


                    ObjVendorChange.Vendor_Change_Detail_Id = 0;
                    ObjVendorChange.Section_Id = ddlSection.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSection.SelectedValue);
                    ObjVendorChange.Section_Feild_Master_Id = ddlField.SelectedValue == "" ? 0 : Convert.ToInt32(ddlField.SelectedValue);
                    ObjVendorChange.Old_Value = txtOldValue.Text;
                    ObjVendorChange.New_Value = txtNewValue.Text;
                    ObjVendorChange.Remarks = txtRemarks.Text;

                    ObjVendorChange.IsActive = 1;
                    ObjVendorChange.UserId = lblUserId.Text;
                    ObjVendorChange.TodayDate = objUtil.GetDate();
                    ObjVendorChange.IPAddress = objUtil.GetIpAddress();

                    if (ObjVendorChangeAccess.SaveImport(ObjVendorChange) != 1)
                    {
                        flg = false;
                    }
                }
            }
        }

        return flg;
    }
}