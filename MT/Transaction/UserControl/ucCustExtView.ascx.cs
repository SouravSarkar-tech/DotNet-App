using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_UserControl_ucCustExtView : System.Web.UI.UserControl
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
                Response.Redirect("CustomerExtension.aspx");
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


            TextBox txtCustomer_Code = (TextBox)grv.FindControl("txtCustomer_Code");
            TextBox txtCustomer_Name = (TextBox)grv.FindControl("txtCustomer_Name");
            DropDownList ddlCompany_Code = (DropDownList)grv.FindControl("ddlCompany_Code");
            DropDownList ddlCust_Account_Grp = (DropDownList)grv.FindControl("ddlCust_Account_Grp");

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
                if (txtCustomer_Code.Text != "")
                {

                }
                else
                {
                    msg = msg + "Customer Code is Mandatory";
                }

            }

            if (msg == "")
            {
                if (txtCustomer_Name.Text != "")
                {

                }
                else
                {
                    msg = msg + "Customer name is Mandatory";
                }

            }

            if (msg == "")
            {

                helperAccess.PopuplateDropDownList(ddlCust_Account_Grp, "pr_GetCustomerAccGrpList 0", "Customer_Acc_Grp_Name", "Customer_Acc_Grp_Id");
                CustCodeWiseSetup(txtCustomer_Code, ddlCust_Account_Grp);

                if (ddlCust_Account_Grp.SelectedValue == "")
                {
                    msg = msg + "Vendor Code Invalid.";
                }
            }

            if (msg == "")
            {
                Label lblCompany_Code = (Label)grv.FindControl("lblCompany_Code");
                string[] sCompany_Code = lblCompany_Code.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlCompany_Code, "pr_GetCompanyCodeList 0", "Company_Code", "Company_Id");
                if (lblCompany_Code.Text != "")
                {

                    if (ddlCompany_Code.Items.FindByText(sCompany_Code[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Company Code.";
                    }
                    else
                    {
                        ddlCompany_Code.SelectedValue = Convert.ToString(ddlCompany_Code.Items.FindByText(sCompany_Code[0].Trim()).Value);
                    }
                }
                else
                {
                    msg = msg + "Company Code is Mandatory";
                }

            }


            if (msg == "")
            {

                Label lblCust_Account_Grp = (Label)grv.FindControl("lblCust_Account_Grp");
                string[] sCust_Account_Grp = lblCust_Account_Grp.Text.Split('-');
                helperAccess.PopuplateDropDownList(ddlCust_Account_Grp, "pr_GetCustomerAccGrpList 0", "Customer_Acc_Grp_Code", "Customer_Acc_Grp_Id");
                if (lblCust_Account_Grp.Text != "")
                {
                    if (ddlCust_Account_Grp.Items.FindByText(sCust_Account_Grp[0].Trim()) == null)
                    {
                        msg = msg + "Invalid Customer account Group.";
                    }
                    else
                    {
                        ddlCust_Account_Grp.SelectedValue = Convert.ToString(ddlCust_Account_Grp.Items.FindByText(sCust_Account_Grp[0].Trim()).Value);
                    }
                }
                else
                {
                    msg = msg + "Customer account Group is Mandatory";
                }

            }


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

                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        OleDbConnection conn = new OleDbConnection(excelConnectionString);
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        conn.Open();
                        DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        cmd.CommandText = "select Customer_Code,Customer_Name,Company_Code,Cust_Account_Grp,Sales_Org,Dist_Channel,Division,Sales_District,Sales_Office,Sales_Grp,Currency,Delivering_Plant,Price_Group,Invoice_Date,Invoice_List,Credit_control_Area,Credit_Currency,Customer_Credit_Limit,Risk_Category from[Sheet3$] where Customer_Code is not null";
                        da.SelectCommand = cmd;



                        da.Fill(dt);
                        conn.Close();

                        //System.Data.DataTable dt = ReadexcelExt(fileExtension, fileLocation);

                        //ds1.Tables.Add(dt);
                        //try
                        //{

                        //    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                        //    foreach (System.Diagnostics.Process p in process)
                        //    {
                        //        if (!string.IsNullOrEmpty(p.ProcessName))
                        //        {
                        //            try
                        //            {
                        //                p.Kill();
                        //            }
                        //            catch { }
                        //        }
                        //    }
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        ds1.Tables.Add(dt);
                    }
                }
                else
                {
                    grvData1.DataSource = null;
                    grvData1.DataBind();
                }

                if (fduplicate != true)
                {
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




    //private void ExcelDataExt()
    //{

    //    string StrPath = String.Empty;
    //    string extensionname = String.Empty;
    //    DataAccessLayer objDb = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    DocumentUploadAccess objDoc = new DocumentUploadAccess();
    //    try
    //    {
    //        dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
    //            extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }

    //    string extension = Path.GetExtension(extensionname).ToLower();
    //    string excelPath = StrPath;
    //    string conString = string.Empty;

    //    StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
    //    int count = 0;
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    //        System.Data.DataTable dt = ReadexcelExt(extension, StrPath);

    //        if (dt.Rows.Count > 2000)
    //        {

    //            string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";

    //        }
    //        else
    //        {
    //            using (SqlCommand cmd = new SqlCommand("Insert_Excel_T_Mat_Mass_OtherWorkflow_TB"))
    //            {
    //                cmd.Connection = con;

    //                if (con.State == ConnectionState.Closed)
    //                {
    //                    con.Open();
    //                }
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //                {
    //                    if (dt.Rows[i]["View_Name"].ToString() != "" && dt.Rows[i]["View_Name"].ToString() != "Basic Data")
    //                    {
    //                        try
    //                        {
    //                            cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
    //                            cmd.Parameters.AddWithValue("@UserId", lblUserId.Text.Trim());
    //                            cmd.Parameters.AddWithValue("@ModuleId", "0");
    //                            cmd.Parameters.AddWithValue("@View_Name", dt.Rows[i]["View_Name"].ToString().Trim());
    //                            cmd.Parameters.AddWithValue("@Field_Name", dt.Rows[i]["Field_Name"].ToString().Trim());

    //                            SqlDataReader sdr = cmd.ExecuteReader();
    //                            sdr.Close();

    //                            cmd.Parameters.RemoveAt("@Master_Header_Id");
    //                            cmd.Parameters.RemoveAt("@UserId");
    //                            cmd.Parameters.RemoveAt("@ModuleId");
    //                            cmd.Parameters.RemoveAt("@View_Name");
    //                            cmd.Parameters.RemoveAt("@Field_Name");
    //                            count += 1;
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";

    //                        }
    //                    }
    //                }

    //                if (con.State == ConnectionState.Open)
    //                {
    //                    con.Close();
    //                }

    //                if (count == 0 || count < dt.Rows.Count)
    //                {
    //                    string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";

    //                }
    //                else
    //                {

    //                }
    //            }


    //        }


    //        try
    //        {

    //            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
    //            foreach (System.Diagnostics.Process p in process)
    //            {
    //                if (!string.IsNullOrEmpty(p.ProcessName))
    //                {
    //                    try
    //                    {
    //                        p.Kill();
    //                    }
    //                    catch { }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    public System.Data.DataTable ReadexcelExt(string ext, string path)
    {
        string ConStr = string.Empty;
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        //string query = "SELECT * FROM [Sheet3$]";

        string query = "SELECT Customer_Code, Customer_Name, Company_Code, Cust_Account_Grp, Sales_Org, Dist_Channel, Division, Sales_District, Sales_Office, Sales_Grp, Currency, Delivering_Plant, Price_Group, Invoice_Date, Invoice_List, Credit_control_Area, Credit_Currency, Customer_Credit_Limit, Risk_Category from[Sheet3$]";


        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (i == 0)
                {
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                    }
                }
                else
                {
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        dr1[j] = dr.ItemArray[j].ToString();
                    }
                    dt.Rows.Add(dr1);

                }
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;
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

    protected void CustCodeWiseSetup(TextBox txtCustCode, DropDownList ddlCustAccGrp)
    {
        txtCustCode.Text = txtCustCode.Text.ToUpper();
        string str = txtCustCode.Text.Substring(0, 1).ToUpper();
        //string str1 = txtCustomerCode.Text.Substring(0, 2);
        int strcode = SafeTypeHandling.ConvertStringToInt32(txtCustCode.Text);

        switch (str)
        {
            case "L":
                //regtxtCustomerCode.ValidationExpression = "^[\\S]{4}$";
                ddlCustAccGrp.SelectedValue = "26";

                break;
            default:
                //regtxtCustomerCode.ValidationExpression = "^[\\d]{6}$";
                if (strcode >= 100000 && strcode < 199999) //Z001
                    ddlCustAccGrp.SelectedValue = "22";
                else if (strcode >= 200000 && strcode < 299999)//Z002
                    ddlCustAccGrp.SelectedValue = "23";
                else if (strcode >= 300000 && strcode < 399999)//Z003
                    ddlCustAccGrp.SelectedValue = "24";
                else if (strcode >= 400000 && strcode < 499999)//Z004
                    ddlCustAccGrp.SelectedValue = "25";
                else if (strcode >= 500000 && strcode < 599999)//Z006 500000-599999
                    ddlCustAccGrp.SelectedValue = "27";
                else if (strcode >= 700000 && strcode < 799999)//Z008 700000-799999
                    ddlCustAccGrp.SelectedValue = "29";
                break;
        }
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
            TextBox txtCustomer_Code = (TextBox)grv.FindControl("txtCustomer_Code");
            TextBox txtCustomer_Name = (TextBox)grv.FindControl("txtCustomer_Name");
            DropDownList ddlCompany_Code = (DropDownList)grv.FindControl("ddlCompany_Code");
            DropDownList ddlCust_Account_Grp = (DropDownList)grv.FindControl("ddlCust_Account_Grp");

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

            ////IsDuplicateEntry(ddlSalesOrg.SelectedValue, ddlDistChannel.SelectedValue, ddlDivision.SelectedValue);
            //CustomerExtensionAccess ObjCustomerExtensionAccess = new CustomerExtensionAccess();

            //if (ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString()), "Ext", vSalesOrg, vDistChan, vDivision) > 0)
            //{
            //}
            //else
            //{

            //}
            CustomerExtension ObjCustomerExtension = new CustomerExtension();
            Utility objUtil = new Utility();

            ObjCustomerExtension.Cust_Extension_Id = Convert.ToInt32(0);
            ObjCustomerExtension.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());

            ObjCustomerExtension.Customer_Code = txtCustomer_Code.Text;
            ObjCustomerExtension.Company_Code = ddlCompany_Code.SelectedValue;
            ObjCustomerExtension.Customer_Acc_Grp = ddlCust_Account_Grp.SelectedValue;
            ObjCustomerExtension.Customer_Desc = txtCustomer_Name.Text;

            ObjCustomerExtension.Sales_Organization_Id = ddlSalesOrg.SelectedValue;
            ObjCustomerExtension.Distribution_Channel_Id = ddlDistChannel.SelectedValue;
            ObjCustomerExtension.Division_ID = ddlDivision.SelectedValue;

            ObjCustomerExtension.SalesDistrict = ddlSalesDistrict.SelectedValue;
            ObjCustomerExtension.SalesOffice = ddlSalesOffice.SelectedValue;
            ObjCustomerExtension.Currency = ddlCurrency.SelectedValue;
            ObjCustomerExtension.SalesGroup = ddlSalesGrp.SelectedValue;
            ObjCustomerExtension.countryKeyExport = "";
            ObjCustomerExtension.DeliveringPlant = ddlDeliveringPlant.SelectedValue;

            ObjCustomerExtension.PriceGroup = ddlPriceGroup.SelectedValue;
            ObjCustomerExtension.InvoiceDates = ddlInvoiceDate.SelectedValue;
            ObjCustomerExtension.InvoiceListSchedule = ddlInvoiceList.SelectedValue;

            ObjCustomerExtension.Credit_Control_Area = ddlCreditcontrolArea.SelectedValue;
            ObjCustomerExtension.Customer_credit_limit = txtCustomerCreditLimit.Text;
            ObjCustomerExtension.Risk_category = ddlRiskCategory.SelectedValue;
            ObjCustomerExtension.Currency_Id = ddlCreditCurrency.SelectedValue;
            ObjCustomerExtension.Remarks = "Excel Upload";

            ObjCustomerExtension.IsActive = "1";
            ObjCustomerExtension.UserId = lblUserId.Text;
            ObjCustomerExtension.TodayDate = objUtil.GetDate();
            ObjCustomerExtension.IPAddress = objUtil.GetIpAddress();


            CustomerExtensionAccess ObjCustomerExtensionAccess = new CustomerExtensionAccess();
            try
            {

                if (ObjCustomerExtensionAccess.Save(ObjCustomerExtension) != 1)
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
        //DataSet dstData = new DataSet();
        //dstData = ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString()), "Ext"
        //    , vSalesOrg, vDistChan, vDivision);

        //if (dstData.Tables[0].Rows.Count > 0)
        //{
        //    flg = true;
        //}
        //if (ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString()), "Ext" , vSalesOrg, vDistChan, vDivision) > 0)
        //{
        //    flg = true;
        //}

        foreach (GridViewRow grv in grvData1.Rows)
        {
            //DropDownList ddlSalesOrg = (DropDownList)grv.FindControl("ddlSalesOrg");
            TextBox txtCustomer_Code = (TextBox)grv.FindControl("txtCustomer_Code");
            DropDownList ddlSalesOrg = (DropDownList)grv.FindControl("ddlSalesOrg");
            DropDownList ddlDistChannel = (DropDownList)grv.FindControl("ddlDistChannel");
            DropDownList ddlDivision = (DropDownList)grv.FindControl("ddlDivision");
            if (ObjCustomerExtensionAccess.IsDuplicateEntry(Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString()), "Ext", ddlSalesOrg.SelectedValue, ddlDistChannel.SelectedValue, ddlDivision.SelectedValue) > 0)
            {
                flg = true;
                break;
            }
        }

        return flg;
    }

}
