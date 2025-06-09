using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using ExcelLibrary.SpreadSheet;


public partial class Webpages_Download : System.Web.UI.Page
{

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mt"] != null)
                {
                    string masterType = Request.QueryString["mt"].ToString().ToUpper();

                    PopulateDropDownList(masterType);
                    Search();
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../shared/home.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    #region related Events


    protected void btnDownloadMultipleFiles_OnClick(object sender, EventArgs e)
    {
        string requestId = "";
        string fileName = "";
        string proc = string.Empty;
        bool flg = false;
        foreach (GridViewRow row in grdNewFiles.Rows)
        {
            CheckBox chkCheck = row.FindControl("chkCheck") as CheckBox;
            if (chkCheck.Checked)
            {
                Label lblRequestID = row.FindControl("lblRequestID") as Label;
                requestId = requestId + lblRequestID.Text + ",";
                flg = true;
            }
        }

        if (Request.QueryString["mt"] != null)
        {
            string masterType = Request.QueryString["mt"].ToString();

            switch (masterType.ToUpper())
            {
                case "M":
                    proc = "pr_rpt_MaterialMaster";
                    fileName = "Material Master";
                    break;
                case "C":
                    proc = "pr_rpt_CustomerMaster";
                    fileName = "Customer Master";
                    break;
                case "V":
                    proc = "pr_rpt_VendorMaster";
                    fileName = "Vendor Master";
                    break;
                case "B":
                    proc = "pr_rpt_BOMMaster";
                    fileName = "BOM Master";
                    break;
                case "R":
                    proc = "pr_rpt_ReceipeMaster";
                    fileName = "Receipe Master";
                    break;
                default:
                    proc = "";
                    break;

            }
        }


        if (flg)
        {
            DataSet dstData = new DataSet();

            if (UpdateDonwloadStatus(requestId))
            {
                //foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("~/tempFile")))
                //{
                //    System.IO.File.Delete(f);
                //}

                dstData = GenerateExcel(requestId, proc);
                dstData.Tables[0].TableName = fileName;

                CreateWorkbook(Server.MapPath("../tempFile/" + fileName + ".xls"), dstData);
                DownloadFile(fileName);
            }
        }
    }
    protected void btnDownloadMultipleFiles2_OnClick(object sender, EventArgs e)
    {
        string requestId = "";
        string fileName = "";
        string proc = string.Empty;
        bool flg = false;
        foreach (GridViewRow row in grdDownloadedFiles.Rows)
        {
            CheckBox chkCheck = row.FindControl("chkCheck") as CheckBox;
            if (chkCheck.Checked)
            {
                Label lblRequestID = row.FindControl("lblRequestID") as Label;
                requestId = requestId + lblRequestID.Text + ",";
                flg = true;
            }
        }

        if (Request.QueryString["mt"] != null)
        {
            string masterType = Request.QueryString["mt"].ToString();

            switch (masterType.ToUpper())
            {
                case "M":
                    proc = "pr_rpt_MaterialMaster";
                    fileName = "Material Master";
                    break;
                case "C":
                    proc = "pr_rpt_CustomerMaster";
                    fileName = "Customer Master";
                    break;
                case "V":
                    proc = "pr_rpt_VendorMaster";
                    fileName = "Vendor Master";
                    break;
                case "B":
                    proc = "pr_rpt_BOMMaster";
                    fileName = "BOM Master";
                    break;
                case "R":
                    proc = "pr_rpt_ReceipeMaster";
                    fileName = "Receipe Master";
                    break;
                default:
                    proc = "";
                    break;

            }
        }


        if (flg)
        {
            DataSet dstData = new DataSet();


            //foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("~/tempFile")))
            //{
            //    System.IO.File.Delete(f);
            //}

            dstData = GenerateExcel(requestId, proc);
            dstData.Tables[0].TableName = fileName;

            CreateWorkbook(Server.MapPath("../tempFile/" + fileName + ".xls"), dstData);
            DownloadFile(fileName);

        }
    }

    #region Gridview Events

    protected void grdNewFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNewFiles.PageIndex = e.NewPageIndex;
        string masterType = Request.QueryString["mt"].ToString().ToUpper();
        NewExcelFileForMaterialMaster(masterType);
    }
    protected void grdDownloadedFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDownloadedFiles.PageIndex = e.NewPageIndex;
        string masterType = Request.QueryString["mt"].ToString().ToUpper();
        DownloadedExcelFileForMaterialMaster(masterType);
    }

    #endregion
    #endregion

    #endregion

    #region Private Methods
    #region Material Master Related Methods

    private void Search()
    {
        string masterType = Request.QueryString["mt"].ToString().ToUpper();
        if (ddlFileStatus.SelectedValue == "A")
        {
            NewExcelFileForMaterialMaster(masterType);
            pnlNewFiles.Visible = true;
            pnlDownloadedFile.Visible = false;
        }
        else
        {
            DownloadedExcelFileForMaterialMaster(masterType);
            pnlNewFiles.Visible = false;
            pnlDownloadedFile.Visible = true;
        }
    }

    private void NewExcelFileForMaterialMaster(string masterType)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        Utility objUtil = new Utility();
        DataSet dstData = new DataSet();
        StringBuilder query = new StringBuilder();
        string fromDate = txtDateFrom.Text.Trim();
        string toDate = txtToDate.Text.Trim();

        if (masterType == "M")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,B1.Material_Number AS 'Code' FROM T_Master_Header H JOIN T_Mat_Basic_Data1 B1 ");
            query.Append(" ON H.Master_Header_Id = B1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + " AND H.Request_Status = 'A' AND H.IsActive = 'TRUE' AND B1.IsActive = 'TRUE' ");
        }
        else if (masterType == "V")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,G1.Customer_Code AS 'Code' FROM T_Master_Header H JOIN T_Vendor_General_Type G1 ");
            query.Append(" ON H.Master_Header_Id = G1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = 15 AND H.Request_Status = 'A' AND H.IsActive = 'TRUE' AND G1.IsActive = 'TRUE' ");
        }
        else if (masterType == "C")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,G1.Customer_Code AS 'Code' FROM T_Master_Header H JOIN T_Cust_General1 G1 ");
            query.Append(" ON H.Master_Header_Id = G1.Master_Header_Id  ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + " AND H.Request_Status = 'A' AND H.IsActive = 'TRUE' AND G1.IsActive = 'TRUE'  ");

        }
        else if (masterType == "B")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,'' AS 'Code' FROM T_Master_Header H JOIN T_BOM_Header B1 ");
            query.Append(" ON H.Master_Header_Id = B1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + " AND H.Request_Status = 'A' AND H.IsActive = 'TRUE' AND B1.IsActive = 'TRUE' ");

        }
        else if (masterType == "R")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,'' AS 'Code' FROM T_Master_Header H JOIN T_Receipe R1 ");
            query.Append(" ON H.Master_Header_Id = R1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + "  AND H.Request_Status = 'A' AND H.IsActive = 'TRUE' AND R1.IsActive = 'TRUE'   ");

        }

        if (!string.IsNullOrEmpty(fromDate))
        {
            query.Append(" AND H.ModifiedOn >= '" + objUtil.GetMMDDYYYY(fromDate) + "'");
        }
        if (!string.IsNullOrEmpty(toDate))
        {
            query.Append(" AND H.ModifiedOn <= DATEADD(D,1,'" + objUtil.GetMMDDYYYY(toDate) + "')");
        }
       
        try
        {
            dstData = objDb.FillDataSet(query.ToString(), "Table");
            grdNewFiles.DataSource = dstData.Tables[0].DefaultView;
            grdNewFiles.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDb = null;
        }
    }
    private void DownloadedExcelFileForMaterialMaster(string masterType)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        Utility objUtil = new Utility();
        DataSet dstData = new DataSet();
        StringBuilder query = new StringBuilder();
        string fromDate = txtDateFrom.Text.Trim();
        string toDate = txtToDate.Text.Trim();
        if (masterType == "M")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,B1.Material_Number as 'Code' FROM T_Master_Header H JOIN T_Mat_Basic_Data1 B1 ");
            query.Append(" ON H.Master_Header_Id = B1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + " AND H.Request_Status = 'D' AND H.IsActive = 'TRUE' AND B1.IsActive = 'TRUE' ");
        }
        else if (masterType == "V")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,G1.Customer_Code AS 'Code' FROM T_Master_Header H JOIN T_Vendor_General_Type G1 ");
            query.Append(" ON H.Master_Header_Id = G1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = 15 AND H.Request_Status = 'D' AND H.IsActive = 'TRUE' AND G1.IsActive = 'TRUE' ");
        }
        else if (masterType == "C")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,G1.Customer_Code AS 'Code' FROM T_Master_Header H JOIN T_Cust_General1 G1 ");
            query.Append(" ON H.Master_Header_Id = G1.Master_Header_Id  ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + " AND H.Request_Status = 'D' AND H.IsActive = 'TRUE' AND G1.IsActive = 'TRUE'  ");
        }
        else if (masterType == "B")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,'' AS 'Code' FROM T_Master_Header H JOIN T_BOM_Header B1 ");
            query.Append(" ON H.Master_Header_Id = B1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + " AND H.Request_Status = 'D' AND H.IsActive = 'TRUE' AND B1.IsActive = 'TRUE' ");

        }
        else if (masterType == "R")
        {
            query.Append(" SELECT H.Master_Header_Id, H.Request_No,'' AS 'Code' FROM T_Master_Header H JOIN T_Receipe R1 ");
            query.Append(" ON H.Master_Header_Id = R1.Master_Header_Id ");
            query.Append(" WHERE H.Module_Id = " + ddlMaterialModule.SelectedValue + "  AND H.Request_Status = 'D' AND H.IsActive = 'TRUE' AND R1.IsActive = 'TRUE'   ");

        }

        if (!string.IsNullOrEmpty(fromDate))
        {
            query.Append(" AND H.ModifiedOn >= '" + objUtil.GetMMDDYYYY(fromDate) + "'");
        }
        if (!string.IsNullOrEmpty(toDate))
        {
            query.Append(" AND H.ModifiedOn <= DATEADD(D,1,'" + objUtil.GetMMDDYYYY(toDate) + "')");

        }

        try
        {
            dstData = objDb.FillDataSet(query.ToString(), "Table");
            grdDownloadedFiles.DataSource = dstData.Tables[0].DefaultView;
            grdDownloadedFiles.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDb = null;
        }
    }
    private DataSet GenerateExcel(string headerId, string Proc)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();

        Hashtable hasPara = new Hashtable();
        try
        {
            //DNRCOMM//objDb.OpenConnection(this.Page);
            hasPara.Add("@Master_Header_Id", headerId);
            dstData = objDb.FillDataSet(CommandType.StoredProcedure, Proc, hasPara);
            return dstData;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDb.CloseConnection(objDb.cnnConnection);
            objDb = null;

        }
    }
    private void PopulateDropDownList(string masterType)
    {
        DashBoard ObjDashBoard = new DashBoard();

        ddlMaterialModule.DataSource = ObjDashBoard.ReadAllModules(masterType.ToUpper());
        ddlMaterialModule.DataTextField = "Module_Name";
        ddlMaterialModule.DataValueField = "Module_Id";
        ddlMaterialModule.DataBind();
    }
    #endregion

    #region
    private bool UpdateDonwloadStatus(string masterHeaderID)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        SqlTransaction objTrans;
        bool flg = false;
        try
        {
            objDb.OpenConnection(this.Page);
            objTrans = objDb.cnnConnection.BeginTransaction();
            if (objDb.ModifyRecord("T_Master_Header", "Request_Status$", "'D'$", "Master_Header_Id in (select * from dbo.Split('" + masterHeaderID + "',','))", ref objDb.cnnConnection, ref objTrans))
            {
                flg = true;
                objTrans.Commit();
            }
            else
            {
                flg = false;
                objTrans.Rollback();
                pnlMsg.Visible = true;
                lblMsg.Text = Messages.GetMessage(0);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDb.CloseConnection(objDb.cnnConnection);
            objDb = null;
        }
        return flg;
    }

    /// <summary>
    /// Populate all data from the given DataSet into a new Excel workbook
    /// </summary>
    /// <param name="filePath">File path to new Excel workbook to be created</param>
    /// <param name="dataset">Source DataSet</param>
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
                    worksheet.Cells[j + 1, i] = new Cell(dt.Rows[j][i]);

            }
            workbook.Worksheets.Add(worksheet);
        }
        workbook.Save(filePath);
    }
    public void CreateWorkbookWithOutHeader(String filePath, DataSet dataset)
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
                // Populate row data
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    worksheet.Cells[j + 1, i] = new Cell(dt.Rows[j][i]);
                    worksheet.Cells[j + 1, i].Style = style;
                }
            }
            workbook.Worksheets.Add(worksheet);

        }
        workbook.Save(filePath);
    }
    private void DownloadFile(string fileName)
    {
        fileName = fileName + ".xls";
        string filePath = Server.MapPath("../tempFile/" + fileName);
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.TransmitFile(filePath);

        //Response.End();
    }

    private void DeleteFile(string fileName)
    {
        string filePath = Server.MapPath("../tempFile/");
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

    #endregion
    #endregion
}