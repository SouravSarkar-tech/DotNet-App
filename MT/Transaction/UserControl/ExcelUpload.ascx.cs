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

public partial class Transaction_UserControl_ExcelUpload : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SaveImport())
        {
            Response.Redirect("MaterialBlock.aspx");
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Error Occured during Updation";

            ModalPopupExtender.Show();
        }
    }

    protected void grvData_DataBound(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;

        foreach (GridViewRow grv in grvData.Rows)
        {
            //GridViewRow grv = (GridViewRow)sender;

            string msg = "";

            Label lblPlant = (Label)grv.FindControl("lblPlant");
            Label lblBlockType = (Label)grv.FindControl("lblBlockType");
            Label lblSalesOrg = (Label)grv.FindControl("lblSalesOrg");
            Label lblDistributionChannel = (Label)grv.FindControl("lblDistributionChannel");
            Label lblMsg = (Label)grv.FindControl("lblMsg");

            RadioButtonList rdlBlockValue = (RadioButtonList)grv.FindControl("rdlBlockValue");

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");

            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");
            TextBox txtMaterialName = (TextBox)grv.FindControl("txtMaterialName");
            DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp");
            DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization");
            DropDownList ddlDistributionChannel = (DropDownList)grv.FindControl("ddlDistributionChannel");

            if (txtMaterialCode.Text == "")
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
                        msg = msg + "Material Description is Mandatory";
                    }
                }

                if (msg == "")
                {
                    if (lblBlockType.Text != "")
                    {
                        if (rdlBlockValue.Items.FindByText(lblBlockType.Text) == null)
                        {
                            msg = msg + "Invalid BlockType.";
                        }
                        else
                        {
                            (rdlBlockValue.Items.FindByText(lblBlockType.Text)).Selected = true;
                        }
                    }
                    else
                    {
                        msg = msg + "Block Type Mandatory";
                    }
                }


                if (msg == "")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");


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

                }

                if (rdlBlockValue.SelectedValue == "S")
                {
                    if (msg == "")
                    {
                        string[] SalesOrg = lblSalesOrg.Text.Split('-');
                        helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '0','SD1','0'", "Sales_Organization_Name", "Sales_Organization_Code", "");
                        if (lblSalesOrg.Text == "")
                        {
                            msg = msg + "Sales Organization is Mandatory.";
                        }
                        else if (ddlSalesOrginization.Items.FindByValue(SalesOrg[0].Trim()) == null)
                        {
                            msg = msg + "Invalid Sales Organization.";
                        }
                        else
                        {
                            ddlSalesOrginization.SelectedValue = SalesOrg[0].Trim();
                        }
                    }

                    if (msg == "")
                    {

                        helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '0','SD1','0','0','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_Code", "");

                        string[] DistributionChannel = lblDistributionChannel.Text.Split('-');
                        if (lblDistributionChannel.Text == "")
                        {
                            msg = msg + "Distribution Channel is Mandatory.";
                        }
                        else if (ddlDistributionChannel.Items.FindByValue(DistributionChannel[0].Trim()) == null)
                        {
                            msg = msg + "Invalid Distribution Channel.";
                        }
                        else
                        {
                            ddlDistributionChannel.SelectedValue = DistributionChannel[0].Trim();
                        }
                    }
                }

                Panel pnlMsg1 = (Panel)grv.FindControl("pnlMsg");

                if (msg == "")
                {
                    lblMsg.Text = "OK";
                    pnlMsg1.CssClass = "success";
                    pnlMsg1.Visible = true;
                }
                else
                {
                    flg = false;
                    lblMsg.Text = msg;
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
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

            HttpPostedFile uploadfile = fileUpload.PostedFile;//fileCollection[0];

            try
            {

                if (uploadfile.ContentLength > 0)
                {
                    //HttpPostedFile ObjHttpPostedFile = UploadBlock.SaveAs();

                    string fileExtension = System.IO.Path.GetExtension(uploadfile.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/tempfile/") + Session[StaticKeys.RequestNo].ToString() + fileExtension;
                        if (System.IO.File.Exists(fileLocation))
                        {
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
                        if (dt == null)
                        {
                            return;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["TABLE_NAME"].ToString() == "Sheet1$")
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        if (t > 0)
                        {
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                        }
                        else
                        {
                            //
                        }
                    }

                    grvData.DataSource = ds;
                }
                else
                {
                    grvData.DataSource = null;
                }

                grvData.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    pnlMsg.Visible = false;
                    ModalPopupExtender.Show();
                }
                else
                {
                    lblMsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            catch
            {
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
        foreach (GridViewRow grv in grvData.Rows)
        {
            Label lblPlant = (Label)grv.FindControl("lblPlant");
            Label lblBlockType = (Label)grv.FindControl("lblBlockType");
            Label lblSalesOrg = (Label)grv.FindControl("lblSalesOrg");
            Label lblDistributionChannel = (Label)grv.FindControl("lblDistributionChannel");
            Label lblMsg = (Label)grv.FindControl("lblMsg");

            RadioButtonList rdlBlockValue = (RadioButtonList)grv.FindControl("rdlBlockValue");

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");

            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");
            TextBox txtMaterialName = (TextBox)grv.FindControl("txtMaterialName");
            TextBox txtRemarks = (TextBox)grv.FindControl("txtRemarks");
            DropDownList ddlMaterialAccGrp = (DropDownList)grv.FindControl("ddlMaterialAccGrp");
            DropDownList ddlSalesOrginization = (DropDownList)grv.FindControl("ddlSalesOrginization");
            DropDownList ddlDistributionChannel = (DropDownList)grv.FindControl("ddlDistributionChannel");


            MaterialBlockAccess ObjMaterialBlockAccess = new MaterialBlockAccess();
            MaterialBlock ObjMaterialBlock = new MaterialBlock();

            Utility objUtil = new Utility();

            ObjMaterialBlock.Material_Block_Id = 0;
            ObjMaterialBlock.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            if (txtMaterialCode.Text != "")
            {

                ObjMaterialBlock.Material_Number = txtMaterialCode.Text;
                ObjMaterialBlock.Material_Short_Description = txtMaterialName.Text;
                ObjMaterialBlock.Material_Type = ddlMaterialAccGrp.SelectedValue;

                ObjMaterialBlock.Plant_Id = ddlPlant.SelectedValue;
                ObjMaterialBlock.Blocking_Level = rdlBlockValue.SelectedValue;
                ObjMaterialBlock.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
                ObjMaterialBlock.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;

                ObjMaterialBlock.Purchase_Status = "";
                ObjMaterialBlock.Material_Status = "";
                ObjMaterialBlock.Remarks = txtRemarks.Text;

                ObjMaterialBlock.IsActive = "1";
                ObjMaterialBlock.UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
                ObjMaterialBlock.TodayDate = objUtil.GetDate();
                ObjMaterialBlock.IPAddress = objUtil.GetIpAddress();


                if(ObjMaterialBlockAccess.SaveMass(ObjMaterialBlock) != 1)
                {
                    flg = false;
                }
            }
        }

        return flg;
    }

}
