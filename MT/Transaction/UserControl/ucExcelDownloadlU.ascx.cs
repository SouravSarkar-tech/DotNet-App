using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using ExcelLibrary.SpreadSheet;
using System.IO;

public partial class Transaction_UserControl_ucExcelDownloadlU : System.Web.UI.UserControl
{

    public string ActionType
    {
        get { return hdnFieldActionType.Value; }
        set { hdnFieldActionType.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //
    }

    protected void lnkExcelDwld_Click(object sender, EventArgs e)
    {
        DownLoadDataInExcel();
    }

    protected void DownLoadDataInExcel()
    {

        //Session[StaticKeys.fileName] = "U";
        //Session[StaticKeys.UserNameSearch] = txtUserNameSearch.Text;
        //Session[StaticKeys.rtpfromdate] = txtFromDate.Text;
        //Session[StaticKeys.rtptodate] = txtToDate.Text;
        string fileName = "";


        DataSet dstData = new DataSet();
        Utility ObjUtil = new Utility();
        UserAccess userAccess = new UserAccess();

        //fileName = Session[StaticKeys.fileName] + "_" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
        ////dstData = userAccess.GetBulkUserList1();
        //dstData = userAccess.GetBulkUserList(Session[StaticKeys.UserNameSearch].ToString(), Session[StaticKeys.fileName].ToString(), ObjUtil.GetMMDDYYYY(Session[StaticKeys.rtpfromdate].ToString()), ObjUtil.GetMMDDYYYY(Session[StaticKeys.rtptodate].ToString()));
        if (ActionType == "U")
        {
            fileName = "U" + "_" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();

            dstData = userAccess.GetBulkUserList(Session[StaticKeys.UserNameSearch].ToString(), "U", ObjUtil.GetMMDDYYYY(Session[StaticKeys.rtpfromdate].ToString()), ObjUtil.GetMMDDYYYY(Session[StaticKeys.rtptodate].ToString()));
        }
        else if (ActionType == "A")
        {
            Session[StaticKeys.UserNameSearch] = "";
            fileName = "A" + "_" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
            dstData = userAccess.GetBulkUserList(Session[StaticKeys.UserNameSearch].ToString(), "A", ObjUtil.GetMMDDYYYY(Session[StaticKeys.rtpfromdate].ToString()), ObjUtil.GetMMDDYYYY(Session[StaticKeys.rtptodate].ToString()));
        }

        string filePath = Server.MapPath("../tempFile/" + fileName + ".xls");
        //string filePath = "E:/MWTQAS/tempFile/tempFile/" + fileName + ".xls";
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        CreateWorkbook(filePath, dstData);
        DownloadFile(filePath, fileName);
    }

    public void CreateWorkbook(String filePath, DataSet dataset)
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


    public void CreateWorkSheet(String filePath, DataSet dataset)
    {
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[3]
        //{
        //new DataColumn("Id"),
        //new DataColumn("Name"),
        //new DataColumn("Country")
        //});
        //dt.Rows.Add(1, "John Hammond", "United States");
        //dt.Rows.Add(2, "Mudassar Khan", "India");
        //dt.Rows.Add(3, "Suzanne Mathews", "France");
        //dt.Rows.Add(4, "Robert Schidner", "Russia");
        //string filePath = Server.MapPath("~/Test.xls");

        //FileInfo info = new FileInfo(filePath);
        //bool isNew = !info.Exists ? true : false;
        //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //using (var package = new ExcelPackage(info))
        //{
        //    ExcelWorksheet workSheet;
        //    if (isNew)
        //    {
        //        workSheet = package.Workbook.Worksheets.Add("Customers");
        //        ExcelWorksheetView wv = workSheet.View;
        //        wv.RightToLeft = Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
        //        // Load DataTable.
        //        workSheet.Cells[1, 1].LoadFromDataTable(dt, isNew, TableStyles.Light8);
        //    }
        //    else
        //    {
        //        workSheet = package.Workbook.Worksheets["Customers"];
        //        // Load DataTable.
        //        workSheet.Cells[2, 1].LoadFromDataTable(dt, isNew);
        //    }
        //    workSheet.PrinterSettings.Orientation = eOrientation.Landscape;
        //    workSheet.Cells.AutoFitColumns();

        //    int rows = workSheet.Dimension.End.Row;
        //    int columns = workSheet.Dimension.End.Column;
        //    for (int row = 0; row < rows; row++)
        //    {
        //        for (int column = 1; column <= columns; column++)
        //        {
        //            if (workSheet.Cells[row + 2, column].Value != null)
        //            {
        //                if (workSheet.Cells[row + 2, column].Value.ToString().ToLower() == "india")
        //                {
        //                    // Set dynamic Field Color.
        //                    workSheet.Cells[row + 2, column].Style.Font.Color.SetColor(Color.Red);
        //                    workSheet.Cells[row + 2, column].Style.Font.Bold = true;
        //                }
        //            }
        //        }
        //    }
        //    package.Save();
        //}

    }
    private void DownloadFile(string filePath, string fileName)
    {
        fileName = fileName + ".xls";
        //string filePath = Server.MapPath("../../tempFile/" + fileName);
        //string filePath1 = "E:/MWTQAS/tempFile/" + fileName;
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.TransmitFile(filePath);
        //Response.TransmitFile(filePath1);
        //Response.End();
    }

    private void DeleteFile(string fileName)
    {
        string filePath = Server.MapPath("../../tempFile/");
        string type = "File";
        if (type == "File")
        {
            System.IO.File.Delete(filePath);
        }
        else if (type == "Directory")
        {
            System.IO.Directory.Delete(filePath, true);
        }
    }

}