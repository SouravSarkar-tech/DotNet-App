using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

using System.IO;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

public partial class Transaction_UserControl_ChangeExcelUpload : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMatPlantGrpId.Text = Session[StaticKeys.MatPlantGrp].ToString();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SaveImport())
        {
            Response.Redirect("MaterialChange.aspx");
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

            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");
            TextBox txtMaterialName = (TextBox)grv.FindControl("txtMaterialName");
            DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp");

            TextBox txtOldValue = (TextBox)grv.FindControl("txtOldValue");
            TextBox txtNewValue = (TextBox)grv.FindControl("txtNewValue");

            if (txtMaterialCode.Text == "" && txtMaterialName.Text == "" && txtOldValue.Text.Trim() == "")
            {
                grv.Visible = false;
            }
            else
            {
                i++;
                HelperAccess helperAccess = new HelperAccess();

                if (txtMaterialCode.Text == "")
                {
                    msg = msg + "Material Code Mandatory.";
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txtMaterialCode.Text, "^[\\d]{6,10}$"))
                {
                    msg = msg + "Material Code Invalid.";
                }

                if (msg == "")
                {

                    helperAccess.PopuplateDropDownList(ddlMaterialAccGrp, "pr_GetModuleListByModuleType 'M'", "Module_Name", "Module_Id", "");
                    MaterialCodeWiseSetup(txtMaterialCode, ddlMaterialAccGrp);

                    if (ddlMaterialAccGrp.SelectedValue == "")
                    {
                        msg = msg + "Material Code Invalid.";
                    }
                    else if (ddlMaterialAccGrp.SelectedValue != Session[StaticKeys.MaterialProcessModuleId].ToString())
                    {
                        msg = msg + "Please enter only " + ddlMaterialAccGrp.Items.FindByValue(Session[StaticKeys.MaterialProcessModuleId].ToString()).Text;
                    }


                    if (txtMaterialName.Text == "")
                    {
                        msg = msg + "Material Description Mandatory.";
                    }

                    if (txtOldValue.Text.Trim() == "")
                    {
                        msg = msg + "Old Value is Mandatory";
                    }

                    if (txtNewValue.Text.Trim() == "")
                    {
                        msg = msg + "New Value is Mandatory";
                    }
                }

                if (msg == "")
                {
                    Label lblPlant = (Label)grv.FindControl("lblPlant");
                    DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");

                    //Depot Srinidhi
                    //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                    if (Session[StaticKeys.MaterialPlantId].ToString() == "8888" || Session[StaticKeys.MaterialPlantId].ToString() == "8889")
                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList_ByDeptType '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                    else
                        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0',''", "Plant_Name", "Plant_Code", "");
                    if (lblPlant.Text != "")
                    {
                        if (ddlPlant.Items.FindByValue(lblPlant.Text) == null)
                        {
                            msg = msg + "Invalid Plant Code.";
                        }
                        else
                        {
                            ddlPlant.SelectedValue = lblPlant.Text;
                        }
                    }
                    else
                    {
                        msg = msg + "Plant Code Mandatory";
                    }

                    if (msg == "")
                    {
                        Label lblStorageLocation = (Label)grv.FindControl("lblStorageLocation");
                        DropDownList ddlStorageLocation = (DropDownList)grv.FindControl("ddlStorageLocation");

                        string[] StorageLoc = lblStorageLocation.Text.Split('-');
                        helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                        if (lblStorageLocation.Text == "")
                        {
                            msg = msg + "Storage Location is Mandatory.";
                        }
                        else if (ddlStorageLocation.Items.FindByValue(StorageLoc[0].Trim()) == null)
                        {
                            msg = msg + "Invalid Storage Location.";
                        }
                        else
                        {
                            ddlStorageLocation.SelectedValue = StorageLoc[0].Trim();
                        }
                    }
                }

                if (msg == "")
                {
                    Label lblSalesOrg = (Label)grv.FindControl("lblSalesOrg");
                    DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization");

                    string[] SalesOrg = lblSalesOrg.Text.Split('-');
                    helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '0','SD1','0'", "Sales_Organization_Name", "Sales_Organization_Code", "");

                    if (ddlSalesOrginization.Items.FindByValue(SalesOrg[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Sales Organization.";
                    }
                    else
                    {
                        ddlSalesOrginization.SelectedValue = SalesOrg[0].Trim();
                    }


                    if (msg == "")
                    {
                        Label lblDistributionChannel = (Label)grv.FindControl("lblDistributionChannel");
                        DropDownList ddlDistributionChannel = (DropDownList)grv.FindControl("ddlDistributionChannel");

                        string[] DistributionChannel = lblDistributionChannel.Text.Split('-');
                        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '0','SD1','0','0','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_Code", "");

                        if (ddlDistributionChannel.Items.FindByValue(DistributionChannel[0].Trim()) == null)
                        {
                            msg = msg + "Invalid Distribution Channel.";
                        }
                        else
                        {
                            ddlDistributionChannel.SelectedValue = DistributionChannel[0].Trim();
                        }
                    }
                }

                if (msg == "")
                {
                    Label lblSection = (Label)grv.FindControl("lblSection");
                    DropDownList ddlSection = (DropDownList)grv.FindControl("ddlSection");

                    string[] Section = lblSection.Text.Split('-');
                    helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");

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

                        string[] Field = lblField.Text.Split('-');
                        helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");

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

        string StrPath = "~/Transaction/Material/MaterialDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

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


                        string query = string.Format("Select * from [Sheet1$] where [Material code] is not null", excelSheets[0]);
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

    protected void MaterialCodeWiseSetup(TextBox txtMaterialCode, DropDownList ddlMaterialAccGrp)
    {

        ddlMaterialAccGrp.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text);

        //txtMaterialCode.Text = txtMaterialCode.Text.ToUpper();
        //string str = txtMaterialCode.Text.Substring(0, 1).ToUpper();
        ////string str1 = txtMaterialCode.Text.Substring(0, 2);
        //int strcode = SafeTypeHandling.ConvertStringToInt32(txtMaterialCode.Text);

        //switch (str)
        //{
        //    default:
        //        //regtxtMaterialCode.ValidationExpression = "^[\\d]{6}$";
        //        if (strcode >= 100000 && strcode < 199999) //ROH 1- Series
        //            ddlMaterialAccGrp.SelectedValue = "162";
        //        else if (strcode >= 200000 && strcode < 299999)//VERP  2- Series
        //            ddlMaterialAccGrp.SelectedValue = "164";
        //        else if (strcode >= 300000 && strcode < 399999)//HALB  3- Series
        //            ddlMaterialAccGrp.SelectedValue = "144";
        //        else if (strcode >= 400000 && strcode < 499999)//FERT  4- Series
        //            ddlMaterialAccGrp.SelectedValue = "139";
        //        else if (strcode >= 500000 && strcode < 599999)//HAWA  5- Series
        //            ddlMaterialAccGrp.SelectedValue = "145";
        //        else if ((strcode >= 600000 && strcode < 699999) || (strcode >= 6600000 && strcode < 6699999))//ERSA  6- Series
        //            ddlMaterialAccGrp.SelectedValue = "138";
        //        else if (strcode >= 700000 && strcode < 799999)//HIBE  7- Series
        //            ddlMaterialAccGrp.SelectedValue = "147";
        //        else if (strcode >= 800000 && strcode < 849999)//ZNBW  8- Series
        //            ddlMaterialAccGrp.SelectedValue = "171";
        //        else if (strcode >= 850000 && strcode < 889999)//UNBW 85- Series
        //            ddlMaterialAccGrp.SelectedValue = "163";
        //        else if (strcode >= 890000 && strcode < 999999)//ZMBW  89/9- Series
        //            ddlMaterialAccGrp.SelectedValue = "170";
        //        break;
        //}
    }

    protected bool SaveImport()
    {
        bool flg = true;
        foreach (GridViewRow grv in grvData1.Rows)
        {
            Label lblPlant = (Label)grv.FindControl("lblPlant");
            Label lblBlockType = (Label)grv.FindControl("lblBlockType");
            Label lblSalesOrg = (Label)grv.FindControl("lblSalesOrg");
            Label lblDistributionChannel = (Label)grv.FindControl("lblDistributionChannel");
            Label lblMsg = (Label)grv.FindControl("lblMsg");

            RadioButtonList rdlBlockValue = (RadioButtonList)grv.FindControl("rdlBlockValue");



            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");
            TextBox txtMaterialName = (TextBox)grv.FindControl("txtMaterialName");
            DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp");

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");
            DropDownList ddlStorageLocation = (DropDownList)grv.FindControl("ddlStorageLocation");

            DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization");
            DropDownList ddlDistributionChannel = (DropDownList)grv.FindControl("ddlDistributionChannel");

            DropDownList ddlSection = (DropDownList)grv.FindControl("ddlSection");
            DropDownList ddlField = (DropDownList)grv.FindControl("ddlField");

            TextBox txtOldValue = (TextBox)grv.FindControl("txtOldValue");
            TextBox txtNewValue = (TextBox)grv.FindControl("txtNewValue");

            MaterialChangeAccess ObjMaterialChangeAccess = new MaterialChangeAccess();

            MaterialChange ObjMaterialChange = new MaterialChange();
            Utility objUtil = new Utility();

            ObjMaterialChange.Material_Change_Id = 0;
            ObjMaterialChange.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            if (txtMaterialCode.Text != "")
            {
                ObjMaterialChange.Material_Code = txtMaterialCode.Text;
                ObjMaterialChange.Plant_Id = ddlPlant.SelectedValue;
                ObjMaterialChange.Storage_Location = ddlStorageLocation.SelectedValue;
                ObjMaterialChange.Material_Desc = txtMaterialName.Text;
                ObjMaterialChange.Material_Acc_Grp = ddlMaterialAccGrp.SelectedValue;
                ObjMaterialChange.Sales_Organisation_Id = ddlSalesOrginization.SelectedValue;
                ObjMaterialChange.Distribution_Channel_Id = ddlDistributionChannel.SelectedValue;


                ObjMaterialChange.Material_Change_Detail_Id = 0;
                ObjMaterialChange.Section_Id = ddlSection.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSection.SelectedValue);
                ObjMaterialChange.Section_Feild_Master_Id = ddlField.SelectedValue == "" ? 0 : Convert.ToInt32(ddlField.SelectedValue);
                ObjMaterialChange.Old_Value = txtOldValue.Text.Trim();
                ObjMaterialChange.New_Value = txtNewValue.Text.Trim();

                ObjMaterialChange.IsActive = 1;
                ObjMaterialChange.UserId = lblUserId.Text;
                ObjMaterialChange.TodayDate = objUtil.GetDate();
                ObjMaterialChange.IPAddress = objUtil.GetIpAddress();

                if (ObjMaterialChangeAccess.SaveImport(ObjMaterialChange) != 1)
                {
                    flg = false;
                }
            }
        }

        return flg;
    }
}
