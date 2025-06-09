using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using ExcelLibrary.SpreadSheet;

public partial class Transaction_UserControl_ucExcelDownloadz : System.Web.UI.UserControl
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
        string fileName = "";

        
        DataSet dstData = new DataSet();
        if (ActionType == "")
        {
            fileName = DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
            MasterAccess ObjMasterAccess = new MasterAccess();
            dstData = ObjMasterAccess.GetLSMWMaterialCreateData();
        }
        else if (ActionType == "C")
        {
            fileName = Session[StaticKeys.RequestNo].ToString();
            MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
            dstData = ObjMaterialMasterAccess.GetBulkChangeDataListByMasterHeaderId(Session[StaticKeys.MasterHeaderId].ToString());
        }
        else if (ActionType == "M")
        {
            fileName = DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
            EAuditAccess objAuditAccess = new EAuditAccess();
            

            dstData = objAuditAccess.SearchauditRequests(Session[StaticKeys.RptStatus].ToString(), Session[StaticKeys.SelectedModuleId].ToString(), Session[StaticKeys.Moduletype].ToString(), Session[StaticKeys.sapcode].ToString(), Session[StaticKeys.RptFromDate].ToString(), Session[StaticKeys.RptToDate].ToString());
        }

        //string filePath = Server.MapPath("../../tempFile/" + fileName + ".xls");
        string filePath = Server.MapPath("~//tempFile//" + fileName + ".xls");
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

    private void DownloadFile(string filePath, string fileName)
    {
        fileName = fileName + ".xls";
        //string filePath = Server.MapPath("../../tempFile/" + fileName);

        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.TransmitFile(filePath);

        Response.End();
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