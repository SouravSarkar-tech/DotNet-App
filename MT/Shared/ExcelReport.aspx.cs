using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExcelLibrary.SpreadSheet;
using Accenture.MWT.DataAccess;
using log4net;
public partial class Shared_ExcelReport : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try { 

        if (Request.QueryString.Count > 0)
        {
            DataSet ds = null;
            string MH_Id = Request.QueryString["MH_Id"];
            if (Request.QueryString["Type"] == "M")
            {
                MaterialMasterAccess objMaterialMasterAccess = new MaterialMasterAccess();
                ds = objMaterialMasterAccess.GetReportByMasterHeaderId(MH_Id);
            }

            if (Request.QueryString["Type"] == "C")
            {
                CustomerGeneralAccess objCustomerGeneralAccess = new CustomerGeneralAccess();
                ds = objCustomerGeneralAccess.GetReportByMasterHeaderId(MH_Id);
            }

            if (Request.QueryString["Type"] == "V")
            {
                VendorMasterAccess objVendorMasterAccess = new VendorMasterAccess();
                ds = objVendorMasterAccess.GetReportByMasterHeaderId(MH_Id);
            }

            GenerateExcl(ds);
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }


    public void GenerateExcl(DataSet dstData)
    {
        try {
        string fileName = "Report";
        CreateWorkbook(Server.MapPath("../tempFile/" + fileName + ".xls"), dstData);
        DownloadFile(fileName);
        }
        catch (Exception ex)
        { _log.Error("GenerateExcl", ex); }
    }


    public void CreateWorkbook(String filePath, DataSet dataset)
    {
        try {

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
                    worksheet.Cells[j + 1, i] = new Cell(dt.Rows[j][i] == string.Empty ? "" : dt.Rows[j][i]);

            }
            workbook.Worksheets.Add(worksheet);
        }
        workbook.Save(filePath);
        }
        catch (Exception ex)
        { _log.Error("CreateWorkbook", ex); }
    }

    private void DownloadFile(string fileName)
    {
        try { 
        fileName = fileName + ".xls";
        string filePath = Server.MapPath("../tempFile/" + fileName);
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.TransmitFile(filePath);

            //Response.End();
        }
        catch (Exception ex)
        { _log.Error("DownloadFile", ex); }
    }
}