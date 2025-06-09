using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_UserControl_ucCustSalseView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!IsDuplicateEntry())
        {
            if (SaveImport())
            {
                Response.Redirect("SalesArea1.aspx");
            }
            else
            {
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                lblMsg.Text = "Error Occured during Updation";

                ModalPopupExtenderI.Show();
            }
        }
        else
        {
            lblMsg.Text = "Duplicate Entry.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    protected void grvData1_DataBound(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;

        foreach (GridViewRow grv in grvData1.Rows)
        {
            string msg = "";

            DropDownList ddlSalesOrg = (DropDownList)grv.FindControl("ddlSalesOrg");
            DropDownList ddlDistChannel = (DropDownList)grv.FindControl("ddlDistChannel");
            DropDownList ddlDivision = (DropDownList)grv.FindControl("ddlDivision");
            DropDownList ddlSalesDistrict = (DropDownList)grv.FindControl("ddlSalesDistrict");
            DropDownList ddlSalesOffice = (DropDownList)grv.FindControl("ddlSalesOffice");
            DropDownList ddlSalesGrp = (DropDownList)grv.FindControl("ddlSalesGrp");
            DropDownList ddlCurrency = (DropDownList)grv.FindControl("ddlCurrency");
            DropDownList ddlDeliveringPlant = (DropDownList)grv.FindControl("ddlDeliveringPlant");
            DropDownList ddlPriceGroup = (DropDownList)grv.FindControl("ddlPriceGroup");
            DropDownList ddlInvoiceDate = (DropDownList)grv.FindControl("ddlInvoiceDate");
            DropDownList ddlInvoiceList = (DropDownList)grv.FindControl("ddlInvoiceList");
            DropDownList ddlCreditcontrolArea = (DropDownList)grv.FindControl("ddlCreditcontrolArea");
            DropDownList ddlCreditCurrency = (DropDownList)grv.FindControl("ddlCreditCurrency");
            DropDownList ddlRiskCategory = (DropDownList)grv.FindControl("ddlRiskCategory");

            TextBox txtCustomerCreditLimit = (TextBox)grv.FindControl("txtCustomerCreditLimit");


            //if (txtCustomerCreditLimit.Text == "" && txtCustomerCreditLimit.Text == "")
            //{
            //    grv.Visible = false;
            //}
            //else
            //{
            i++;
            HelperAccess helperAccess = new HelperAccess();

            if (msg == "")
            {
                Label lblSalesOrg = (Label)grv.FindControl("lblSalesOrg");
                string[] sSalesOrg = lblSalesOrg.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlSalesOrg, "pr_GetSalesOrganisationList '" + 0 + "','SD1','" + 0 + "'", "Sales_Organization_Code", "Sales_Organization_Id", "");
                //helperAccess.PopuplateDropDownList(ddlSalesOrg, "pr_GetSalesOrganisationList '" + 0 + "','SD1','" + 0 + "'", "Sales_Organization_Name", "Sales_Organization_Code", "");
                if (lblSalesOrg.Text != "")
                {
                    //if (ddlSalesOrg.Items.FindByValue(sSalesOrg[0].Trim()) == null)
                    //{
                    //    if (ddlSalesOrg.Items.FindByText(sSalesOrg[0].Trim()) == null)
                    //    {
                    //        msg = msg + "Invalid Sales Organization.";
                    //    }
                    //    else
                    //    {

                    //    } 
                    //}
                    //else
                    //{
                    //    ddlSalesOrg.SelectedValue = sSalesOrg[0].Trim();
                    //}
                    if (ddlSalesOrg.Items.FindByText(sSalesOrg[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Sales Organization.";
                    }
                    else
                    {
                        ddlSalesOrg.SelectedValue = Convert.ToString(ddlSalesOrg.Items.FindByText(sSalesOrg[0].Trim()).Value);
                    }
                }
                else
                {
                    msg = msg + "Sales Organization is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblDistChannel = (Label)grv.FindControl("lblDistChannel");
                string[] sDistChannel = lblDistChannel.Text.Split('-');
                //helperAccess.PopuplateDropDownList(ddlDistChannel, "pr_GetDistributionChannelList '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOrg.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                helperAccess.PopuplateDropDownList(ddlDistChannel, "pr_GetDistributionChannelList '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOrg.SelectedValue + "'", "Distribution_Channel_Code", "Distribution_Channel_ID", "");
                if (lblDistChannel.Text != "")
                {
                    //if (ddlDistChannel.Items.FindByValue(sDistChannel[0].Trim()) == null)
                    //{
                    //    msg = msg + "Invalid Dist. Channel.";
                    //}
                    //else
                    //{
                    //    ddlDistChannel.SelectedValue = sDistChannel[0].Trim();
                    //}
                    if (ddlDistChannel.Items.FindByText(sDistChannel[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Dist. Channel.";
                    }
                    else
                    {
                        ddlDistChannel.SelectedValue = Convert.ToString(ddlDistChannel.Items.FindByText(sDistChannel[0].Trim()).Value);
                    }
                }
                else
                {
                    msg = msg + "Dist. Channel is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblDivision = (Label)grv.FindControl("lblDivision");
                string[] sDivision = lblDivision.Text.Split('-');
                //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + 0 + "','CSD','" + 0 + "','" + ddlSalesOrg.SelectedValue + "','" + ddlDistChannel.SelectedValue + "'", "Division_Name", "Division_Id", "");
                helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + 0 + "','CSD','" + 0 + "','" + ddlSalesOrg.SelectedValue + "','" + ddlDistChannel.SelectedValue + "'", "Division_CodeIRF", "Division_Id", "");
                if (lblDivision.Text != "")
                {
                    //if (ddlDivision.Items.FindByValue(sDivision[0].Trim()) == null)
                    //{
                    //    msg = msg + "Invalid Division.";
                    //}
                    //else
                    //{
                    //    ddlDivision.SelectedValue = sDivision[0].Trim();
                    //}
                    if (ddlDivision.Items.FindByText(sDivision[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Division.";
                    }
                    else
                    {
                        ddlDivision.SelectedValue = Convert.ToString(ddlDivision.Items.FindByText(sDivision[0].Trim()).Value);
                    }
                }
                else
                {
                    msg = msg + "Division is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblSalesDistrict = (Label)grv.FindControl("lblSalesDistrict");
                string[] sSalesDistrict = lblSalesDistrict.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlSalesDistrict, "pr_GetDropDownListByControlNameModuleType 'C','ddlSalesDistrict'", "LookUp_Desc", "LookUp_Code", "");
                if (lblSalesDistrict.Text != "")
                {
                    if (ddlSalesDistrict.Items.FindByValue(sSalesDistrict[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Sales District.";
                    }
                    else
                    {
                        ddlSalesDistrict.SelectedValue = sSalesDistrict[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Sales District is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblSalesOffice = (Label)grv.FindControl("lblSalesOffice");
                string[] sSalesOffice = lblSalesOffice.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlSalesOffice, "pr_GetSalesOfficeBySalesArea '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOrg.SelectedValue + "','" + ddlDistChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                if (lblSalesOffice.Text != "")
                {
                    if (ddlSalesOffice.Items.FindByValue(sSalesOffice[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Sales Office.";
                    }
                    else
                    {
                        ddlSalesOffice.SelectedValue = sSalesOffice[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Sales Office is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblSalesGrp = (Label)grv.FindControl("lblSalesGrp");
                string[] sSalesGrp = lblSalesGrp.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlSalesGrp, "pr_GetSalesGroupList '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOffice.SelectedValue + "'", "Sales_Group_Desc", "Sales_Group_Id", "");

                if (lblSalesGrp.Text != "")
                {
                    if (ddlSalesGrp.Items.FindByValue(sSalesGrp[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Sales Grp.";
                    }
                    else
                    {
                        ddlSalesGrp.SelectedValue = sSalesGrp[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Sales Grp.is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblCurrency = (Label)grv.FindControl("lblCurrency");
                string[] sCurrency = lblCurrency.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlCurrency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code", "");
                if (lblCurrency.Text != "")
                {
                    if (ddlCurrency.Items.FindByValue(sCurrency[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Currency.";
                    }
                    else
                    {
                        ddlCurrency.SelectedValue = sCurrency[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Currency is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblDeliveringPlant = (Label)grv.FindControl("lblDeliveringPlant");
                string[] sDeliveringPlant = lblDeliveringPlant.Text.Split('-');
                //helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOrg.SelectedValue + "','" + ddlDistChannel.SelectedValue + "'", "Plant_Name", "Plant_Id", "");
                helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_GetDeliveringPlantList '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOrg.SelectedValue + "','" + ddlDistChannel.SelectedValue + "'", "DPlant_CodeIRF", "Plant_Id", "");
                if (lblDeliveringPlant.Text != "")
                {
                    //if (ddlDeliveringPlant.Items.FindByValue(sDeliveringPlant[0].Trim()) == null)
                    //{
                    //    msg = msg + "Invalid Delivering Plant.";
                    //}
                    //else
                    //{
                    //    ddlDeliveringPlant.SelectedValue = sDeliveringPlant[0].Trim();
                    //}

                    if (ddlDeliveringPlant.Items.FindByText(sDeliveringPlant[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Delivering Plant.";
                    }
                    else
                    {
                        ddlDeliveringPlant.SelectedValue = Convert.ToString(ddlDeliveringPlant.Items.FindByText(sDeliveringPlant[0].Trim()).Value);
                    }
                }
                else
                {
                    msg = msg + "Delivering Plant is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblPriceGroup = (Label)grv.FindControl("lblPriceGroup");
                string[] sPriceGroup = lblPriceGroup.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlPriceGroup, "pr_GetDropDownListByControlNameModuleType 'C','ddlPriceGroup','" + 24 + "'", "LookUp_Desc", "LookUp_Code", "");
                if (lblPriceGroup.Text != "")
                {
                    //if (ddlPriceGroup.Items.FindByValue(sPriceGroup[0].Trim()) == null)
                    //{
                    //    msg = msg + "Invalid Price Group.";
                    //}
                    //else
                    //{
                    //    ddlPriceGroup.SelectedValue = sPriceGroup[0].Trim();
                    //}
                    ddlPriceGroup.SelectedValue = sPriceGroup[0].Trim();
                }
                else
                {
                    //  msg = msg + "Price Group is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblInvoiceDate = (Label)grv.FindControl("lblInvoiceDate");
                string[] sInvoiceDate = lblInvoiceDate.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlInvoiceDate, "pr_GetDropDownListByControlNameModuleType 'C','ddlInvoiceDates','" + 24 + "'", "LookUp_Desc", "LookUp_Code", "");
                if (lblInvoiceDate.Text != "")
                {
                    if (ddlInvoiceDate.Items.FindByValue(sInvoiceDate[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Invoice Date.";
                    }
                    else
                    {
                        ddlInvoiceDate.SelectedValue = sInvoiceDate[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Invoice Date is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblInvoiceList = (Label)grv.FindControl("lblInvoiceList");
                string[] sInvoiceList = lblInvoiceList.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlInvoiceList, "pr_GetDropDownListByControlNameModuleType 'C','ddlInvoiceListSchedule','" + 24 + "'", "LookUp_Desc", "LookUp_Code", "");
                if (lblInvoiceList.Text != "")
                {
                    if (ddlInvoiceList.Items.FindByValue(sInvoiceList[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Invoice List.";
                    }
                    else
                    {
                        ddlInvoiceList.SelectedValue = sInvoiceList[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Invoice List is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblCreditcontrolArea = (Label)grv.FindControl("lblCreditcontrolArea");
                string[] sCreditcontrolArea = lblCreditcontrolArea.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlCreditcontrolArea, "pr_GetCreditControlAreaList '" + 0 + "','SD1','" + 0 + "','" + ddlSalesOrg.SelectedValue + "','" + ddlDistChannel.SelectedValue + "','" + ddlDivision.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                if (lblCreditcontrolArea.Text != "")
                {
                    if (ddlCreditcontrolArea.Items.FindByValue(sCreditcontrolArea[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Credit Control Area.";
                    }
                    else
                    {
                        ddlCreditcontrolArea.SelectedValue = sCreditcontrolArea[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Credit Control Area is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblCreditCurrency = (Label)grv.FindControl("lblCreditCurrency");
                string[] sCreditCurrency = lblCreditCurrency.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlCreditCurrency, "pr_GetAllCurrencyList", "Currency_Name", "Currency_Code", "");
                if (lblCreditCurrency.Text != "")
                {
                    if (ddlCreditCurrency.Items.FindByValue(sCreditCurrency[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Credit Currency.";
                    }
                    else
                    {
                        ddlCreditCurrency.SelectedValue = sCreditCurrency[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Credit Currency is Mandatory";
                }

            }
            if (msg == "")
            {
                Label lblRiskCategory = (Label)grv.FindControl("lblRiskCategory");
                string[] sRiskCategory = lblRiskCategory.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlRiskCategory, "pr_GetDropDownListByControlNameModuleType 'C','ddlRiskcategory','" + 24 + "','" + ddlCreditcontrolArea.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                if (lblRiskCategory.Text != "")
                {
                    if (ddlRiskCategory.Items.FindByValue(sRiskCategory[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Risk Category.";
                    }
                    else
                    {
                        ddlRiskCategory.SelectedValue = sRiskCategory[0].Trim();
                    }
                }
                else
                {
                    msg = msg + "Risk Category is Mandatory";
                }

            }
            if (msg == "")
            {
                if (txtCustomerCreditLimit.Text != "")
                {

                }
                else
                {
                    msg = msg + "Customer Credit Limit is Mandatory";
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
        //}

        if (i > 0)
            btnAdd.Enabled = flg;
    }

    /// <summary>
    /// CS_8200049196
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Process_Click(object sender, EventArgs e)
    {

        bool fduplicate = false;
        pnlMsg.Visible = false;
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        string StrPath = "~/Transaction/Customer/CustomerDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

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
                        //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);                        

                        //excelConnection.Open();
                        //DataTable dt = new DataTable();

                        //dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null); 

                        //if (dt == null)
                        //{
                        //    return;
                        //}


                        //String[] excelSheets = new String[dt.Rows.Count];
                        //int t = 0;
                        ////excel data saves in temp file here.
                        //foreach (DataRow row in dt.Rows)
                        //{
                        //    excelSheets[t] = row["TABLE_NAME"].ToString();
                        //    t++;
                        //}
                        //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                        //string query = string.Format("Select * from [Sheet3$]", excelSheets[0]);                        
                        //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        //{
                        //    dataAdapter.Fill(ds);
                        //}

                        OleDbConnection conn = new OleDbConnection(excelConnectionString);
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        conn.Open();
                        DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        //string sheetName = dtSheet.Rows[0]["table_name"].ToString();

                        //cmd.CommandText = "select * from [Sheet3$] where Ref_Customer_code !=''";
                        //cmd.CommandText = "select * from [Sheet3$]";

                        //cmd.CommandText = "select * from [Sheet3$] where Sales_District <> ''";
                        cmd.CommandText = "select Sales_Org,Dist_Channel,Division,Sales_District,Sales_Office,Sales_Grp,Currency,Delivering_Plant,Price_Group,Invoice_Date,Invoice_List,Credit_control_Area,Credit_Currency,Customer_Credit_Limit,Risk_Category from[Sheet3$] where Sales_District<> ''";

                        da.SelectCommand = cmd;



                        da.Fill(dt);
                        conn.Close();

                        if (dt.Rows.Count > 0)
                        {
                            var duplicatesData = dt.AsEnumerable().GroupBy(x => new
                            {
                                Sales_Org = x.Field<string>("Sales_Org"),
                                Dist_Channel = x.Field<string>("Dist_Channel"),
                                Division = x.Field<string>("Division")
                            })
                                 .Where(gr => gr.Count() > 1).SelectMany(dupRec => dupRec);

                            //var duplicates = dt.AsEnumerable().GroupBy(r => r[0] ).Where(gr => gr.Count() > 1).ToList();
                            //DataTable duplicates = null;
                            //if (EmployeeData != null && EmployeeDat.AsEnumerable().Any())
                            //{
                            //    var duplicatesData = EmployeeData.AsEnumerable().GroupBy(x => new
                            //    {
                            //        EmployeeId = x.Field<int>("EmployeeId"),
                            //        CountryId = x.Field<int>("CountryId"),
                            //        City = x.Field<string>("City")
                            //    })
                            //     .Where(gr => gr.Count() > 1).SelectMany(dupRec => dupRec);

                            //    //Check if duplicate records exists
                            //    if (duplicatesData != null &&
                            //        duplicatesData.Any())
                            //    {
                            //        duplicates = duplicatesData.CopyToDataTable();
                            //    }
                            //}

                            if (duplicatesData.Any() && duplicatesData != null)
                            {
                                fduplicate = true;

                            }
                            else
                            {
                                dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull ||
                                                      string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();

                                ds1.Tables.Add(dt);

                            }

                        }
                    }

                    //grvData1.DataSource = ds;
                    //grvData1.DataSource = ds1;
                    //grvData1.DataBind();
                }
                else
                {
                    grvData1.DataSource = null;
                    grvData1.DataBind();
                }

                if (fduplicate != true)
                {
                    //if (ds.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        grvData1.DataSource = ds1;
                        grvData1.DataBind();
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
                else
                {
                    lblMsg.Text = "Duplicate record.";
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

    /// <summary>
    /// CS_8200049196
    /// </summary>
    /// <param name="ExcelConnectionStr"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
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

    /// <summary>
    /// CS_8200049196
    /// </summary>
    /// <returns></returns>
    protected bool SaveImport()
    {
        bool flg = true;
        foreach (GridViewRow grv in grvData1.Rows)
        {
            DropDownList ddlSalesOrg = (DropDownList)grv.FindControl("ddlSalesOrg");
            DropDownList ddlDistChannel = (DropDownList)grv.FindControl("ddlDistChannel");
            DropDownList ddlDivision = (DropDownList)grv.FindControl("ddlDivision");
            DropDownList ddlSalesDistrict = (DropDownList)grv.FindControl("ddlSalesDistrict");
            DropDownList ddlSalesOffice = (DropDownList)grv.FindControl("ddlSalesOffice");
            DropDownList ddlSalesGrp = (DropDownList)grv.FindControl("ddlSalesGrp");
            DropDownList ddlCurrency = (DropDownList)grv.FindControl("ddlCurrency");
            DropDownList ddlDeliveringPlant = (DropDownList)grv.FindControl("ddlDeliveringPlant");
            DropDownList ddlPriceGroup = (DropDownList)grv.FindControl("ddlPriceGroup");
            DropDownList ddlInvoiceDate = (DropDownList)grv.FindControl("ddlInvoiceDate");
            DropDownList ddlInvoiceList = (DropDownList)grv.FindControl("ddlInvoiceList");
            DropDownList ddlCreditcontrolArea = (DropDownList)grv.FindControl("ddlCreditcontrolArea");
            DropDownList ddlCreditCurrency = (DropDownList)grv.FindControl("ddlCreditCurrency");
            DropDownList ddlRiskCategory = (DropDownList)grv.FindControl("ddlRiskCategory");

            TextBox txtCustomerCreditLimit = (TextBox)grv.FindControl("txtCustomerCreditLimit");
            //bool flg1 = false;
            //if (ddlSalesOrginization.SelectedValue == ((Label)gr.FindControl("lblSalesOrgId")).Text 
            //    && ddlDistributionChannel.SelectedValue == ((Label)gr.FindControl("lblDistributionChnlId")).Text
            //    && ddlDivision.SelectedValue == ((Label)gr.FindControl("lblDivisionId")).Text)
            //{
            //    flg1 = true;
            //}

            SalesArea1 ObjSalesArea = new SalesArea1();
            Utility objUtil = new Utility();

            ObjSalesArea.Cust_SalesArea1_Id = 0;
            ObjSalesArea.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            ObjSalesArea.Sales_Organization_Id = ddlSalesOrg.SelectedValue;
            ObjSalesArea.Distribution_Channel_ID = ddlDistChannel.SelectedValue;
            ObjSalesArea.Division_ID = ddlDivision.SelectedValue;

            ObjSalesArea.SalesDistrict = ddlSalesDistrict.SelectedValue;
            ObjSalesArea.SalesOffice = ddlSalesOffice.SelectedValue;
            ObjSalesArea.Currency = ddlCurrency.SelectedValue;
            ObjSalesArea.SalesGroup = ddlSalesGrp.SelectedValue;
            ObjSalesArea.countryKeyExport = "";
            ObjSalesArea.DeliveringPlant = ddlDeliveringPlant.SelectedValue;

            ObjSalesArea.PriceGroup = ddlPriceGroup.SelectedValue;
            ObjSalesArea.InvoiceDates = ddlInvoiceDate.SelectedValue;
            ObjSalesArea.InvoiceListSchedule = ddlInvoiceList.SelectedValue;

            ObjSalesArea.Credit_Control_Area = ddlCreditcontrolArea.SelectedValue;
            ObjSalesArea.Customer_credit_limit = txtCustomerCreditLimit.Text;
            ObjSalesArea.Risk_category = ddlRiskCategory.SelectedValue;
            ObjSalesArea.Currency_Id = ddlCreditCurrency.SelectedValue;

            ObjSalesArea.IsActive = 1;
            ObjSalesArea.UserId = lblUserId.Text;
            ObjSalesArea.TodayDate = objUtil.GetDate();
            ObjSalesArea.IPAddress = objUtil.GetIpAddress();
            SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();

            //VendorChangeAccess ObjVendorChangeAccess = new VendorChangeAccess();

            try
            {

                if (ObjSalesAreaAccess.SaveImportCSales(ObjSalesArea) != 1)
                {
                    flg = false;
                }

            }
            catch (Exception ex)
            {

            }



            //VendorChangePFun ObjVendorChange = new VendorChangePFun();
            //Utility objUtil = new Utility();

            //ObjVendorChange.Vendor_Change_Id = 0;
            //ObjVendorChange.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            //if (txtVendorCode.Text != "")
            //{
            //    ObjVendorChange.Customer_Code = txtVendorCode.Text;
            //    ObjVendorChange.Company_Code = ddlCompanyCode.SelectedValue;
            //    ObjVendorChange.Vendor_Desc = txtVendorName.Text;
            //    ObjVendorChange.Vendor_Group = ddlVendorAccGrp.SelectedValue;
            //    ObjVendorChange.Purchase_Org = ddlPurchaseOrg.SelectedValue;
            //    ObjVendorChange.sPfun_Lookup_Code = "0";
            //    ObjVendorChange.sPfun_Lookup_Code = ddlPartnerFun.SelectedValue == "" ? "0" : Convert.ToString(ddlPartnerFun.SelectedValue);
            //    ObjVendorChange.sVendor_Code_link = txtLinkVendorCode.Text;
            //    ObjVendorChange.sVendor_Desc_link = txtLinkVendorName.Text;
            //    ObjVendorChange.IsActive = 1;
            //    ObjVendorChange.UserId = lblUserId.Text;
            //    ObjVendorChange.TodayDate = objUtil.GetDate();
            //    ObjVendorChange.IPAddress = objUtil.GetIpAddress();
            //    if (ObjVendorChangeAccess.SaveImportPF(ObjVendorChange) != 1)
            //    {
            //        flg = false;
            //    }
            //}
        }
        return flg;
    }

    private bool IsDuplicateEntry()
    {
        bool flg = false;

        //CS_8200049196
        CustomerExtensionAccess ObjCustomerExtensionAccess = new CustomerExtensionAccess();
        foreach (GridViewRow grv in grvData1.Rows)
        {
            DropDownList ddlSalesOrg = (DropDownList)grv.FindControl("ddlSalesOrg");
            DropDownList ddlDistChannel = (DropDownList)grv.FindControl("ddlDistChannel");
            DropDownList ddlDivision = (DropDownList)grv.FindControl("ddlDivision");
            if (ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString()), "Csa", ddlSalesOrg.SelectedValue, ddlDistChannel.SelectedValue, ddlDivision.SelectedValue) > 0)
            {
                flg = true;
                break;
            }
        }
        return flg;
    }

}
