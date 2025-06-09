using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExcelLibrary.SpreadSheet;
using System.Data.OleDb;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using log4net;
public partial class Transaction_Material_Default : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MaterialDepotExtnsnAccess ObjMaterialDepotExtnsnAccess = new MaterialDepotExtnsnAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();  //data
                if (Session[StaticKeys.SelectedModuleId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString(); //data
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();  //data
                    BindDepotExtnsnData();
                }
            }
        }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void grvExtnsnData_DataBound(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;
        try
        {
        foreach (GridViewRow grv in grvExtnsnData.Rows)
        {
            string msg = "";

            Label lblLot_Size = (Label)grv.FindControl("lblLot_Size");
            DropDownList ddlLotSize = (DropDownList)grv.FindControl("ddlLotSize");

            Label lblFixed_Lot_Size = (Label)grv.FindControl("lblFixed_Lot_Size");

            Label lblMRP_Type = (Label)grv.FindControl("lblMRP_Type");
            DropDownList ddlMrpType = (DropDownList)grv.FindControl("ddlMrpType");

            Label lblMRP_Controller = (Label)grv.FindControl("lblMRP_Controller");
            DropDownList ddlMrpController = (DropDownList)grv.FindControl("ddlMrpController");

            Label lblPlant_Id = (Label)grv.FindControl("lblPlant_Id");
            Label lblPlant = (Label)grv.FindControl("lblPlant");
            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");

            Label lblPurchasing_Group = (Label)grv.FindControl("lblPurchasing_Group");
            DropDownList ddlPurchasingGroup = (DropDownList)grv.FindControl("ddlPurchasingGroup");

            Label lblProcurement_Type = (Label)grv.FindControl("lblProcurement_Type");
            DropDownList ddlProcurmentType = (DropDownList)grv.FindControl("ddlProcurmentType");

            Label lblLoading_Group = (Label)grv.FindControl("lblLoading_Group");
            DropDownList ddlLoadingGroup = (DropDownList)grv.FindControl("ddlLoadingGroup");



            if (lblLot_Size.Text == "" && lblMRP_Type.Text == "")
            {
                grv.Visible = false;
            }
            else
            {
                i++;
                HelperAccess helperAccess = new HelperAccess();

                if (lblLot_Size.Text == "")
                {
                    msg = msg + "Lot Size is Mandatory.";
                }
                if (msg == "")
                {
                    helperAccess.PopuplateDropDownList(ddlLotSize, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");
                    if (lblLot_Size.Text != "")
                    {
                        if (ddlLotSize.Items.FindByValue(lblLot_Size.Text) == null)
                        {
                            msg = msg + "Invalid Lot Size";
                        }
                        else
                        {
                            ddlLotSize.SelectedValue = lblLot_Size.Text;
                        }
                    }
                    else
                    {
                        msg = msg + "Lot Size is mandatory.";
                    }
                }

                if (msg == "")
                {
                    if (lblLot_Size.Text != "")
                    {
                        if (lblLot_Size.Text.ToUpper() == "FX")
                        {
                            if (lblFixed_Lot_Size.Text != "")
                            {
                                if (!System.Text.RegularExpressions.Regex.IsMatch(lblFixed_Lot_Size.Text, "^[\\d]{1,6}$"))
                                    msg = msg + "Fixed Lot Size is invalid";
                            }
                            else
                                msg = msg + "Enter fixed lot size.";
                        }
                        else if (lblLot_Size.Text.ToUpper() == "EX")
                        {
                            if(lblFixed_Lot_Size.Text != "")
                                msg = msg + "Invalid fixed lot size";
                        }
                    }
                }

                if (msg == "")
                {
                    helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
                    if (lblMRP_Type.Text != "")
                    {
                        if (ddlMrpType.Items.FindByValue(lblMRP_Type.Text) == null)
                        {
                            msg = msg + "Invalid MRP Type";
                        }
                        else
                        {
                            ddlMrpType.SelectedValue = lblMRP_Type.Text;
                        }
                    }
                    else
                    {
                        msg = msg + "MRP type is mandatory.";
                    }
                }

                if(msg == "")
                {
                    //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MRP1','0'", "Plant_Name", "Plant_Code", "");
                    //CTRL_SUB_SDT18112019 Commented by NR

                    //CTRL_SUB_SDT18112019 Added by NR
                    if (lblModuleId.Text == "162")
                    {

                        
                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '0','MRP1','0'", "Plant_Name", "Plant_Code", "");
                    }
                    else
                    {
                        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MRP1','0'", "Plant_Name", "Plant_Code", "");
                    }
                    //CTRL_SUB_SDT18112019 Added by NR
                    if (lblPlant_Id.Text != "")
                    {
                        //resolve the plant code issue first
                        if (ddlPlant.Items.FindByValue(lblPlant_Id.Text.ToUpper()) == null)
                        {
                            msg = msg + "Invalid Plant Code.";
                        }
                        else
                        {
                            ddlPlant.SelectedValue = lblPlant_Id.Text;
                        }
                    }
                    else
                    {
                        msg = msg + "Plant Code Mandatory";
                    }

                    if (msg == "")
                    {
                        helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','8','" + lblPlant.Text + "'", "LookUp_Desc", "LookUp_Code", "");

                        if (lblMRP_Type.Text.ToUpper() == "PD" || ddlMrpType.SelectedValue == "X0")
                        {
                            if (lblMRP_Controller.Text != "")
                            {
                                if (ddlMrpController.Items.FindByValue(lblMRP_Controller.Text) == null)
                                {
                                    msg = msg + "Invalid MRP Controller";
                                }
                            }
                            else
                            {
                                msg = msg + "MRP Controller cannot be blank";
                            }
                        }
                    }

                    //if (msg == "")
                    //{
                    //    //helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + lblPlant.Text + "'", "LookUp_Desc", "LookUp_Code", "");
                    //    helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12'", "LookUp_Desc", "LookUp_Code", "");
                    //    if (lblPurchasing_Group.Text != "")
                    //    {
                    //        if (ddlPurchasingGroup.Items.FindByValue(lblPurchasing_Group.Text) == null)
                    //            msg = msg + "Invalid Purchasing group";
                    //    }
                    //    else
                    //        msg = msg + "Purchasing group is mandatory";
                    //}

                    if (msg == "")
                    {
                        helperAccess.PopuplateDropDownList(ddlProcurmentType, "pr_GetDropDownListByControlNameModuleType 'M','ddlProcurmentType'", "LookUp_Desc", "LookUp_Code", "");
                        if (lblProcurement_Type.Text != "")
                        {
                            if (ddlProcurmentType.Items.FindByValue(lblProcurement_Type.Text.ToUpper()) == null)
                                msg = msg + "Invalid Procurement type";
                        }
                        else
                            msg = msg + "Procurement type is mandatory";
                    }

                    if (msg == "")
                    {
                        helperAccess.PopuplateDropDownList(ddlLoadingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlLoadingGroup'", "LookUp_Desc", "LookUp_Code", "");
                        if (lblLoading_Group.Text != "")
                        {
                            if (ddlLoadingGroup.Items.FindByValue(lblLoading_Group.Text) == null)
                                msg = msg + "Invalid Loading Group";
                        }
                        else
                            msg = msg + "Loading Group is mandatory";
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
        catch (Exception ex)
        { _log.Error("grvExtnsnData_DataBound", ex); }
    }

    protected void Process_Click(object sender, EventArgs e)
    {
        try
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
                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            System.IO.File.Delete(fileLocation);
                        }
                        fileUpload.SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            //excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            //fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            
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


                        string query = string.Format("Select * from [Table$]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }

                    grvExtnsnData.DataSource = ds;
                }
                else
                {
                    grvExtnsnData.DataSource = null;
                }

                grvExtnsnData.DataBind();
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
            catch(Exception ex)
                {
                    _log.Error("Process_Click1", ex);
                    lblMsg.Text = ex.Message;

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
        catch (Exception ex)
        { _log.Error("Process_Click", ex); }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveImport())
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Error Occured during Updation";

            ModalPopupExtenderI.Show();
            }
        }
        catch (Exception ex)
        { _log.Error("btnAdd_Click", ex); }
    }

    #endregion    

    #region Excel Download

    protected void lnkExcelDwld_Click(object sender, EventArgs e)
    {
        try
        {
        DownLoadDataInExcel();
        }
        catch (Exception ex)
        { _log.Error("lnkExcelDwld_Click", ex); }
    }

    protected void lnkExcelDwld_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        DownLoadDataInExcel();
        }
        catch (Exception ex)
        { _log.Error("lnkExcelDwld_Click", ex); }
    }

    protected void DownLoadDataInExcel()
    {
        try
        {
        string fileName = Session[StaticKeys.RequestNo].ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
        DataSet dstData = new DataSet();
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
        {
            dstData = ObjMaterialDepotExtnsnAccess.GetMaterialDepotExtnsnLSMWData(lblMasterHeaderId.Text, lblUserId.Text);
        }
        else
        {
           dstData = ObjMaterialDepotExtnsnAccess.GetMaterialDepotExtnsnData(lblMasterHeaderId.Text, lblUserId.Text);
        }

        string filePath = Server.MapPath("../../tempFile/" + fileName + ".xls");
        if (System.IO.File.Exists(filePath))
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            System.IO.File.Delete(filePath);
        }

        CreateWorkbook(filePath, dstData);
        DownloadFile(filePath, fileName);
        }
        catch (Exception ex)
        { _log.Error("DownLoadDataInExcel", ex); }
    }

    public void CreateWorkbook(String filePath, DataSet dataset)
    {
        try
        {
        if (dataset.Tables.Count == 0)
            throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");

        Workbook workbook = new Workbook();
        CellStyle style = new CellStyle();
        style.BackColor = System.Drawing.Color.Beige;

        foreach (DataTable dt in dataset.Tables)
        {
            Worksheet worksheet = new Worksheet(dt.TableName);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                // Add column header
                worksheet.Cells[0, i].Style = style;
                worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);

                // Populate row data
                for (int j = 0; j < dt.Rows.Count; j++)
                    worksheet.Cells[j + 1, i] = new Cell(SafeTypeHandling.ConvertToString(dt.Rows[j][i]));

            }
            workbook.Worksheets.Add(worksheet);
        }

        workbook.Save(filePath);
        }
        catch (Exception ex)
        { _log.Error("CreateWorkbook", ex); }
    }

    private void DownloadFile(string filePath, string fileName)
    {
        try
        {
        GC.Collect();
        GC.WaitForPendingFinalizers();

        fileName = fileName + ".xls";
        //string filePath = Server.MapPath("../../tempFile/" + fileName);

        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

        Response.TransmitFile(filePath);

            //Response.End();
        }
        catch (Exception ex)
        { _log.Error("DownloadFile", ex); }
    }

    private void DeleteFile(string fileName)
    {
        try
        {
        string filePath = Server.MapPath("../../tempFile/");
        string type = "File";
        if (type == "File")
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            System.IO.File.Delete(filePath);
        }
        else if (type == "Directory")
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            System.IO.Directory.Delete(filePath, true);
            }
        }
        catch (Exception ex)
        { _log.Error("DeleteFile", ex); }
    }

    #endregion

    #region Private Methods

    private bool SaveImport()
    {
        bool flg = true;
        try
        {
        foreach (GridViewRow grv in grvExtnsnData.Rows)
        {
            Label lblMat_Extnsn_Data_Id = (Label)grv.FindControl("lblMat_Extnsn_Data_Id");
            Label lblPlant_Group = (Label)grv.FindControl("lblPlant_Group");
            Label lblPlant_Id = (Label)grv.FindControl("lblPlant_Id");
            Label lblPurchasing_Group = (Label)grv.FindControl("lblPurchasing_Group");
            Label lblMRP_Type = (Label)grv.FindControl("lblMRP_Type");
            Label lblMRP_Controller = (Label)grv.FindControl("lblMRP_Controller");
            Label lblReorder_Point = (Label)grv.FindControl("lblReorder_Point");
            Label lblLot_Size = (Label)grv.FindControl("lblLot_Size");
            Label lblFixed_Lot_Size = (Label)grv.FindControl("lblFixed_Lot_Size");
            Label lblRounding_Value = (Label)grv.FindControl("lblRounding_Value");
            Label lblOld_Material_Number = (Label)grv.FindControl("lblOld_Material_Number");
            Label lblRange_Coverage_Profile = (Label)grv.FindControl("lblRange_Coverage_Profile");
            Label lblProcurement_Type = (Label)grv.FindControl("lblProcurement_Type");
            Label lblSafety_Time_WorkDays = (Label)grv.FindControl("lblSafety_Time_WorkDays");
            Label lblPlanned_Delivery_Time_Days = (Label)grv.FindControl("lblPlanned_Delivery_Time_Days");
            Label lblGR_Processing_Time = (Label)grv.FindControl("lblGR_Processing_Time");
            Label lblSpl_Procurement_Type = (Label)grv.FindControl("lblSpl_Procurement_Type");
            Label lblFair_Share_Rule = (Label)grv.FindControl("lblFair_Share_Rule");
            Label lblIndi_Push_Distribution = (Label)grv.FindControl("lblIndi_Push_Distribution");
            Label lblLoading_Group = (Label)grv.FindControl("lblLoading_Group");
            
            MatDepotExtension objDeptExtnsn = new MatDepotExtension();
            Utility objUtil = new Utility();

            objDeptExtnsn.Mat_Extnsn_Data_Id = Convert.ToInt32(lblMat_Extnsn_Data_Id.Text);
            objDeptExtnsn.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            objDeptExtnsn.Plant_Group = lblPlant_Group.Text;
            objDeptExtnsn.Plant_Id = lblPlant_Id.Text;
            objDeptExtnsn.Purchasing_Group = lblPurchasing_Group.Text;
            objDeptExtnsn.MRP_Type = lblMRP_Type.Text;
            objDeptExtnsn.MRP_Controller = lblMRP_Controller.Text;
            objDeptExtnsn.Reorder_Point = lblReorder_Point.Text;
            objDeptExtnsn.Lot_Size = lblLot_Size.Text;
            objDeptExtnsn.Fixed_Lot_Size = lblFixed_Lot_Size.Text;
            objDeptExtnsn.Rounding_Value = lblRounding_Value.Text;
            objDeptExtnsn.Old_Material_Number = lblOld_Material_Number.Text;
            objDeptExtnsn.Range_Coverage_Profile = lblRange_Coverage_Profile.Text;
            objDeptExtnsn.Procurement_Type = lblProcurement_Type.Text;
            objDeptExtnsn.Safety_Time_WorkDays = lblSafety_Time_WorkDays.Text;
            objDeptExtnsn.Planned_Delivery_Time_Days = lblPlanned_Delivery_Time_Days.Text;
            objDeptExtnsn.GR_Processing_Time = lblGR_Processing_Time.Text;
            objDeptExtnsn.Spl_Procurement_Type = lblSpl_Procurement_Type.Text;
            objDeptExtnsn.Fair_Share_Rule = lblFair_Share_Rule.Text;
            objDeptExtnsn.Indi_Push_Distribution = lblIndi_Push_Distribution.Text;
            objDeptExtnsn.Loading_Group = lblLoading_Group.Text;

            objDeptExtnsn.IsActive = 1;
            objDeptExtnsn.UserId = lblUserId.Text;
            objDeptExtnsn.TodayDate = objUtil.GetDate();
            objDeptExtnsn.IPAddress = objUtil.GetIpAddress();

            if (ObjMaterialDepotExtnsnAccess.SaveImport(objDeptExtnsn) != 1)
            {
                flg = false;
            }

        }

        }
        catch (Exception ex)
        { _log.Error("SaveImport", ex); }
        return flg;
    }

    private void BindDepotExtnsnData()
    {
        try
        {
        DataSet ds = ObjMaterialDepotExtnsnAccess.GetMaterialDepotExtnsnData(lblMasterHeaderId.Text,lblUserId.Text);

        grvDepotExtnsn.DataSource = ds.Tables[0];
        grvDepotExtnsn.DataBind();
        }
        catch (Exception ex)
        { _log.Error("BindDepotExtnsnData", ex); }

    }

    #endregion

}