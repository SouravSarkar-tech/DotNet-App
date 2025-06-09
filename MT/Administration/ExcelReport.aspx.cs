using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using ExcelLibrary.SpreadSheet;
public partial class Administration_ExcelReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                //lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

            }
        }
    }

    protected void lnkExcelDwld_Click(object sender, EventArgs e)
    { 
        DownLoadDataInExcel();
    }




    protected void DownLoadDataInExcel()
    {
        try
        {
            string Flag = ddlReportType.SelectedValue;
            string fileName = "";
            string fileNamep = "";
            //if (Flag == "L")
            //{
            //    fileNamep = "Last_Tranzaction";
            //}
            //else
            if (Flag == "U")
            {
                fileNamep = txtUserNameSearch.Text.Trim();
            }
            else if (Flag == "A" || Flag == "R")
            {
                fileNamep = "Admin";
            }
            else if (Flag == "S")
            {
                fileNamep = "SLA";
            }
            else
            {
                fileNamep = Flag;
            }
            Utility ObjUtil = new Utility();
            UserAccess userAccess = new UserAccess();
            DataSet dstData = new DataSet();
            fileName = fileNamep + "_" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
            // dstData = userAccess.GetBulkUserList();
            dstData = userAccess.GetBulkUserList(txtUserNameSearch.Text.Trim(), Flag, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
             
            string filePath = Server.MapPath("../tempFile/" + fileName + ".xls");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            CreateWorkbook(filePath, dstData);
            DownloadFile(filePath, fileName);

        }
        catch (Exception ex) { }
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
        //string filePath1 = "E:/MWTQAS/tempFile/" + fileName;
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.TransmitFile(filePath);

        //Response.End();

        //string filepath = Server.MapPath("~/FormatHRexcel/Sample_HR_Upload_File.xlsx");

        //string filepaths = "E:/MWTQAS/tempFile";
        //filepaths = filepaths +"/"+ fileName;
        //Response.ContentType = ContentType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
        //Response.WriteFile(filepaths);
        //Response.End();
    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        Session[StaticKeys.fileName] = "U";
        Session[StaticKeys.UserNameSearch] = txtUserNameSearch.Text.Trim();
        Session[StaticKeys.rtpfromdate] = txtFromDate.Text;
        Session[StaticKeys.rtptodate] = txtToDate.Text;

    }
}