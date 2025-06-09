using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class Transaction_UserControl_ucZcapHsnExcelUpload : System.Web.UI.UserControl
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// Set File format base on the module id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[StaticKeys.ModuleZCAP] = "";
        Session[StaticKeys.ModuleHSN] = "";
        Session[StaticKeys.ModuleZHG] = "";

        Session[StaticKeys.ModuleZCAP] = ConfigurationManager.AppSettings["ModuleZCAP"].ToString();
        Session[StaticKeys.ModuleHSN] = ConfigurationManager.AppSettings["ModuleHSN"].ToString();
        Session[StaticKeys.ModuleZHG] = ConfigurationManager.AppSettings["ModuleZHG"].ToString();


        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                try
                {


                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                    //Session[StaticKeys.ModuleZCAP] = ConfigurationManager.AppSettings["ModuleZCAP"].ToString();
                    //Session[StaticKeys.ModuleHSN] = ConfigurationManager.AppSettings["ModuleHSN"].ToString();
                    //Session[StaticKeys.ModuleZHG] = ConfigurationManager.AppSettings["ModuleZHG"].ToString();


                    //string sModuleZCAP = ConfigurationManager.AppSettings["ModuleZCAP"].ToString();
                    //string sModuleHSN = ConfigurationManager.AppSettings["ModuleHSN"].ToString();
                    //string sModuleZHG = ConfigurationManager.AppSettings["ModuleZHG"].ToString();


                    if (lblModuleId.Text.Trim() == Convert.ToString(Session[StaticKeys.ModuleZCAP]))
                    {//ZCAP/ZPEX 233
                        hlImportFormat.NavigateUrl = "~/Transaction/ZcapHsnMaster/UploadFormat/Zcap_ZpexMaster.xlsx";
                    }
                    else if (lblModuleId.Text.Trim() == Convert.ToString(Session[StaticKeys.ModuleHSN]))
                    {//HSN/GST% 234 
                        hlImportFormat.NavigateUrl = "~/Transaction/ZcapHsnMaster/UploadFormat/HSN_GST.xlsx";
                    }
                    else if (lblModuleId.Text.Trim() == Convert.ToString(Session[StaticKeys.ModuleZHG]))
                    {//ZCAP/ZPEX + HSN/GST% 235
                        hlImportFormat.NavigateUrl = "~/Transaction/ZcapHsnMaster/UploadFormat/ZcapHsnMaster.xlsx";
                    }
                }
                catch (Exception ex) { _log.Error("Page_Load", ex); }
            }
        }
    }

    /// <summary>
    /// on click event triggred Save or Import data in MWT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveImport())
            {
                Response.Redirect("ZcapHsnCreate.aspx");
            }
            else
            {
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                lblMsg.Text = "Error Occured during Updation";

                ModalPopupExtenderI.Show();
            }
        }
        catch (Exception ex) { _log.Error("btnAdd_Click", ex); }
    }

    /// <summary>
    /// Zcap and Hsn Data validate 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grvData1_DataBound(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;
        try
        {
            foreach (GridViewRow grv in grvData1.Rows)
            {
                string msg = "";

                TextBox txtsMaterial_Code = (TextBox)grv.FindControl("txtsMaterial_Code");
                TextBox txtsSupp_plant = (TextBox)grv.FindControl("txtsSupp_plant");
                TextBox txtsRece_plant = (TextBox)grv.FindControl("txtsRece_plant");
                TextBox txtsZcapRate = (TextBox)grv.FindControl("txtsZcapRate");
                TextBox txtsUOM = (TextBox)grv.FindControl("txtsUOM");
                TextBox txtsSTONum = (TextBox)grv.FindControl("txtsSTONum");
                TextBox txtsHSN_Code = (TextBox)grv.FindControl("txtsHSN_Code");
                TextBox txtsGST_Code = (TextBox)grv.FindControl("txtsGST_Code");
                TextBox txtsRemarks = (TextBox)grv.FindControl("txtsRemarks");
                TextBox txtsMaterial_Name = (TextBox)grv.FindControl("txtsMaterial_Name");

                if (txtsMaterial_Code.Text.Trim() == "")
                {
                    grv.Visible = false;
                }
                else
                {
                    i++;
                    HelperAccess helperAccess = new HelperAccess();


                    //if (txtsZcapRate.Text.Trim() == "")
                    //{
                    //    txtsZcapRate.Text = "0.00";
                    //}
                    //else
                    //{
                    //    string ptxtsZcapRate = "", ptxtsZcapRate1 = "";
                    //    ptxtsZcapRate = txtsZcapRate.Text.Trim().Split('.').First();
                    //    ptxtsZcapRate1 = txtsZcapRate.Text.Trim().Split('.').Last();
                    //    if(ptxtsZcapRate1 =="")
                    //    {
                    //        txtsZcapRate.Text = ptxtsZcapRate + ".00";
                    //    }
                    //}

                    //if (txtsGST_Code.Text.Trim() == "")
                    //{
                    //    txtsGST_Code.Text = "0.00";
                    //}
                    //else
                    //{
                    //    string ptxtsGST_Code = "", ptxtsGST_Code1 = "";
                    //    ptxtsGST_Code = txtsGST_Code.Text.Trim().Split('.').First();
                    //    ptxtsGST_Code1 = txtsGST_Code.Text.Trim().Split('.').Last();
                    //    if (ptxtsGST_Code1 == "")
                    //    {
                    //        txtsGST_Code.Text = ptxtsGST_Code + ".00";
                    //    }
                    //}


                    if (txtsMaterial_Code.Text.Trim() == "")
                    {
                        msg = msg + "Material Code cannot be blank.";
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(txtsMaterial_Code.Text.Trim(), "^[\\d]{4,7}$"))
                    {
                        msg = msg + "Invalid Material Code.";
                    }

                    if (msg == "")
                    {
                        if (txtsMaterial_Name.Text.Trim() == "")
                        {
                            msg = msg + "Material name cannot be blank.";
                        }

                        if (txtsSupp_plant.Text.Trim() == "")
                        {
                            msg = msg + "Supp. plant cannot be blank.";
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(txtsSupp_plant.Text.Trim(), "^[a-zA-Z0-9]{4,4}$"))
                        {
                            msg = msg + "Invalid Supp. plant.";
                        }
                        if (txtsRece_plant.Text.Trim() == "" && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleHSN]))
                        {
                            msg = msg + "Rec. plant cannot be blank.";
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(txtsRece_plant.Text.Trim(), "^[a-zA-Z0-9]{4,4}$") && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleHSN]))
                        {
                            msg = msg + "Invalid Rec. plant.";
                        }
                    }

                    if (msg == "")
                    {
                        Label lblCondintiontype = (Label)grv.FindControl("lblCondintiontype");
                        DropDownList ddlsCondintion_type = (DropDownList)grv.FindControl("ddlsCondintion_type");
                        string[] sCondintiontype = lblCondintiontype.Text.Split('-');
                        helperAccess.PopuplateDropDownList(ddlsCondintion_type, "pr_GetDropDownListByControlName_Hsn 'Z','ddlsCondintion_type','0','" + lblModuleId.Text.Trim() + "'", "LookUp_Desc", "LookUp_Code");


                        if (lblCondintiontype.Text.Trim() != "")
                        {

                            if (ddlsCondintion_type.Items.FindByValue(sCondintiontype[0].Trim()) == null)
                            {
                                msg = msg + "Invalid Condintion type.";
                            }
                            else
                            {
                                ddlsCondintion_type.SelectedValue = sCondintiontype[0].Trim();
                            }
                        }
                        else
                        {
                            msg = msg + "Condintion type Mandatory";
                        }

                    }

                    if (msg == "")
                    {
                        Label lblIsLUTCond = (Label)grv.FindControl("lblIsLUTCond");
                        DropDownList ddlsIsLUTCond = (DropDownList)grv.FindControl("ddlsIsLUTCond");

                        string[] sIsLUTCond = lblIsLUTCond.Text.Split('-');
                        helperAccess.PopuplateDropDownList(ddlsIsLUTCond, "pr_GetDropDownListByControlName_Hsn 'Z','ddlsIsLUTCond'", "LookUp_Desc", "LookUp_Code");

                        if (lblIsLUTCond.Text != "")
                        {

                            if (ddlsIsLUTCond.Items.FindByValue(sIsLUTCond[0].Trim()) == null)
                            {
                                msg = msg + "Invalid Is LUT Cond.";
                            }
                            else
                            {
                                ddlsIsLUTCond.SelectedValue = sIsLUTCond[0].Trim();
                            }
                        }
                        else
                        {

                        }
                    }

                    if (msg == "")
                    {
                        //System.Text.RegularExpressions.Regex decimalRegex = new System.Text.RegularExpressions.Regex(@"^[0-9]([\.\,][0-9]{0,2})?$");

                        //System.Text.RegularExpressions.Regex decimalRegex = new System.Text.RegularExpressions.Regex(@"^\d+\.\d{0,2}$");

                        System.Text.RegularExpressions.Regex decimalRegex = new System.Text.RegularExpressions.Regex(@"^[0-9]*(\.[0-9]{0,2})?$");
                        System.Text.RegularExpressions.Regex decimalRegexgst = new System.Text.RegularExpressions.Regex(@"^[0-9]*(\.[0-9]{0,3})?$");

                        if (txtsZcapRate.Text.Trim() == "" && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleHSN]))
                        {
                            msg = msg + "Zcap rate cannot be blank.";
                        }
                        //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtsZcapRate.Text, "^[0-9]([\.\,][0-9]{1,3})?$"))
                        else if (!decimalRegex.IsMatch(txtsZcapRate.Text.Trim()) && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleHSN]))
                        {
                            msg = msg + "Invalid Zcap rate.";
                        }

                        if (txtsUOM.Text.Trim() == "" && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleHSN]))
                        {//234
                            msg = msg + "UOM cannot be blank.";
                        }
                        if (txtsHSN_Code.Text.Trim() == "" && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleZCAP]))
                        {//233
                            msg = msg + "HSN code cannot be blank.";
                        }
                        if (txtsGST_Code.Text.Trim() == "" && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleZCAP]))
                        {//233
                            msg = msg + "GST code cannot be blank.";
                        }
                        else if (!decimalRegexgst.IsMatch(txtsGST_Code.Text.Trim()) && lblModuleId.Text.Trim() != Convert.ToString(Session[StaticKeys.ModuleZCAP]))
                        {
                            msg = msg + "Invalid GST code.";
                        }
                        ////else if (!System.Text.RegularExpressions.Regex.IsMatch(txtsGST_Code.Text, "^[\\d+\\.\\d]{0,2}$"))
                        //else if (!decimalRegex.IsMatch(txtsGST_Code.Text.Trim()))
                        //{
                        //    msg = msg + "Invalid GST code.";
                        //}
                        if (txtsRemarks.Text.Trim() == "")
                        {
                            msg = msg + "Remarks cannot be blank.";
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
        catch (Exception ex) { _log.Error("grvData1_DataBound", ex); }
    }

    /// <summary>
    /// Procced to Excel/ data bind records update in MWT table and create in table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Process_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;
            DataSet ds = new DataSet();

            string StrPath = "~/Transaction/ZcapHsnMaster/ZcapHsnDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";

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
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);


                            excelConnection.Open();
                            // DataTable dt = new DataTable();

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
                            // OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            //excelConnection1.Open();
                            string sheet1 = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                            //string sheet1 = "";
                            // string query = "";
                            //string query = string.Format("Select * from [Sheet1$]", excelSheets[0]);
                            string query = string.Format("Select * from [" + sheet1 + "]", excelConnection);

                            //if (lblModuleId.Text.Trim() == "233")
                            //{//ZCAP/ZPEX
                            //    query = string.Format("Select * from [ZcapZpex$]", excelSheets[0]);
                            //}
                            //else if (lblModuleId.Text.Trim() == "234")
                            //{//HSN/GST%
                            //    query = string.Format("Select * from [HSN_GST$]", excelSheets[0]);
                            //}
                            //else if (lblModuleId.Text.Trim() == "235")
                            //{//ZCAP/ZPEX + HSN/GST%
                            //    query = string.Format("Select * from [ZcapHsn$]", excelSheets[0]);
                            //}

                            //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            //{
                            //    dataAdapter.Fill(ds);
                            //}

                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection))
                            {
                                dataAdapter.Fill(ds);
                            }

                            //excelConnection.Close();
                        }

                        if (lblModuleId.Text.Trim() == Convert.ToString(Session[StaticKeys.ModuleZCAP]))
                        {//ZCAP/ZPEX
                            grvData1.Columns[6].Visible = false;
                            grvData1.Columns[7].Visible = false;
                            grvData1.Columns[8].Visible = false;
                            ds.Tables.Add(new DataTable());
                            ds.Tables[0].Columns.Add("HSN Code", typeof(string));
                            ds.Tables[0].Columns.Add("GST Code", typeof(string));
                            ds.Tables[0].Columns.Add("Is LUT Cond", typeof(string));

                        }
                        else if (lblModuleId.Text.Trim() == Convert.ToString(Session[StaticKeys.ModuleHSN]))
                        {//HSN/GST%
                            grvData1.Columns[4].Visible = false;
                            grvData1.Columns[5].Visible = false;
                            ds.Tables.Add(new DataTable());
                            ds.Tables[0].Columns.Add("Zcap Rate", typeof(string));
                            ds.Tables[0].Columns.Add("UOM", typeof(string));
                        }

                        //DataSet ds = new DataSet();


                        grvData1.DataSource = ds;
                    }
                    else
                    {
                        grvData1.DataSource = null;
                    }
                    //if (lblModuleId.Text.Trim() == "233")
                    //{//ZCAP/ZPEX
                    //    grvData1.Columns[6].Visible = false;
                    //    grvData1.Columns[7].Visible = false;
                    //    grvData1.Columns[8].Visible = false;
                    //}
                    //else if (lblModuleId.Text.Trim() == "234")
                    //{//HSN/GST%
                    //    grvData1.Columns[4].Visible = false;
                    //    grvData1.Columns[5].Visible = false;
                    //}


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
        catch (Exception ex) { _log.Error("Process_Click", ex); }
    }

    /// <summary>
    /// Create data tabe,data set of excel data
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
    /// Save or import data in MWT
    /// </summary>
    /// <returns></returns>
    protected bool SaveImport()
    {
        bool flg = true;
        try
        {
            foreach (GridViewRow grv in grvData1.Rows)
            {

                Label lblCondintiontype = (Label)grv.FindControl("lblCondintiontype");
                Label lblIsLUTCond = (Label)grv.FindControl("lblIsLUTCond");
                Label lblMsg = (Label)grv.FindControl("lblMsg");

                TextBox txtsMaterial_Code = (TextBox)grv.FindControl("txtsMaterial_Code");
                TextBox txtsSupp_plant = (TextBox)grv.FindControl("txtsSupp_plant");
                TextBox txtsRece_plant = (TextBox)grv.FindControl("txtsRece_plant");
                DropDownList ddlsCondintion_type = (DropDownList)grv.FindControl("ddlsCondintion_type");

                TextBox txtsZcapRate = (TextBox)grv.FindControl("txtsZcapRate");
                TextBox txtsUOM = (TextBox)grv.FindControl("txtsUOM");
                TextBox txtsSTONum = (TextBox)grv.FindControl("txtsSTONum");
                TextBox txtsHSN_Code = (TextBox)grv.FindControl("txtsHSN_Code");
                TextBox txtsGST_Code = (TextBox)grv.FindControl("txtsGST_Code");
                DropDownList ddlsIsLUTCond = (DropDownList)grv.FindControl("ddlsIsLUTCond");
                TextBox txtsRemarks = (TextBox)grv.FindControl("txtsRemarks");

                TextBox txtsMaterial_Name = (TextBox)grv.FindControl("txtsMaterial_Name");
                ZcapHsnMasterAccess objZcapHsnAccess = new ZcapHsnMasterAccess();
                ZcapHsnMasterCreate objZcapHsn = new ZcapHsnMasterCreate();
                Utility objUtil = new Utility();

                if (txtsMaterial_Code.Text.Trim() != "")
                {
                    objZcapHsn.HSN_ZCAP_Detaiils_Id = Convert.ToInt32(0);
                    objZcapHsn.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
                    objZcapHsn.sMaterial_Code = txtsMaterial_Code.Text.Trim();
                    objZcapHsn.sSupp_plant = txtsSupp_plant.Text.Trim();
                    objZcapHsn.sRece_plant = txtsRece_plant.Text.Trim();
                    objZcapHsn.sCondintion_type = ddlsCondintion_type.SelectedValue;
                    objZcapHsn.sZcapRate = txtsZcapRate.Text.Trim();
                    objZcapHsn.sUOM = txtsUOM.Text.Trim();
                    objZcapHsn.sSTONum = txtsSTONum.Text.Trim();
                    objZcapHsn.sHSN_Code = txtsHSN_Code.Text.Trim();
                    //objZcapHsn.sGST_Code = "";
                    objZcapHsn.sGST_Code = txtsGST_Code.Text.Trim();
                    objZcapHsn.sIsLUTCond = ddlsIsLUTCond.SelectedValue;
                    objZcapHsn.sRemarks = txtsRemarks.Text.Trim();
                    objZcapHsn.IsActive = 1;
                    objZcapHsn.CreatedBy = Convert.ToInt32(lblUserId.Text);
                    objZcapHsn.CreatedOn = objUtil.GetDate();
                    objZcapHsn.CreatedIp = objUtil.GetIpAddress();
                    objZcapHsn.sMaterial_Name = txtsMaterial_Name.Text.Trim();
                    if (objZcapHsnAccess.SaveDetails(objZcapHsn) != 1)
                    {
                        flg = false;
                    }
                }
            }
        }
        catch (Exception ex) { _log.Error("SaveImport", ex); }
        return flg;
    }
}