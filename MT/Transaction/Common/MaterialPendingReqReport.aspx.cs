using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using ExcelLibrary.SpreadSheet;
using log4net;
public partial class Transaction_Common_MaterialPendingReqReport : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
    Utility ObjUtil = new Utility();

    #region PageEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        try { 
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {

                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                PopulateDropDownList();
                // ReadModules();
                //ReadProfileWiseModules(userProfileId, lblUserId.Text); 
            }
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try {
        if (ddlMasterTypeSearch.SelectedValue == "M")
        {
            GetMaterialPendingRequests();
        }
        else if (ddlMasterTypeSearch.SelectedValue == "C")
        {
            GetCustomerPendingRequests();
        }
        else if (ddlMasterTypeSearch.SelectedValue == "V")
        {
            GetVendorPendingRequests();
        }
        else if (ddlMasterTypeSearch.SelectedValue == "I")
        {
            GetCostCenterPendingRequests();
        }
        else if (ddlMasterTypeSearch.SelectedValue == "G")
        {
            GetGLPendingRequests();
        }
        //BOM_NWF_SDT05072019
        else if (ddlMasterTypeSearch.SelectedValue == "B")
        {
            GetBOMPendingRequests();
        }
            //BOM_NWF_SDT05072019
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }

    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { 
        grdSearch.PageIndex = e.NewPageIndex;
        GetMaterialPendingRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearch_PageIndexChanging", ex); }
    }

    /// <summary>
    /// Created By Manali SDT31052019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSearchC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { 
        grdSearchC.PageIndex = e.NewPageIndex;
        GetCustomerPendingRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearchC_PageIndexChanging", ex); }
    }

    protected void grdSearchV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { 
        grdSearchV.PageIndex = e.NewPageIndex;
        GetVendorPendingRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearchV_PageIndexChanging", ex); }
    }

    protected void grdSearchI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { 
        grdSearchI.PageIndex = e.NewPageIndex;
        GetCostCenterPendingRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearchI_PageIndexChanging", ex); }
    }



    protected void grdSearchG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try {
        grdSearchG.PageIndex = e.NewPageIndex;
        GetGLPendingRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearchG_PageIndexChanging", ex); }
    }

    /// <summary>
    /// BOM_NWF_SDT05072019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSearchB_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { 
        grdSearchB.PageIndex = e.NewPageIndex;
        GetBOMPendingRequests();
        }
        catch (Exception ex)
        { _log.Error("grdSearchB_PageIndexChanging", ex); }
    }


    #endregion

    #region Methods

    public void PopulateDropDownList()
    {
        try { 
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_Get_Purchase_Group_By_Plant_Id 'M','ddlPurchasingGroup','12','" + ddlPlant.SelectedValue + "',''", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlApprDept, "pr_Get_Department", "LookUp_Desc", "LookUp_Code", "");
        //27/05/19 changed by manali
        helperAccess.PopuplateDropDownList(ddlTerritory, "pr_Get_DAS_TERRITORY_MASTER", "TERRITORY", "ID", "");
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + 0 + "','SD1','" + 0 + "','0','0'", "Division_Name", "Division_Id", "");
        helperAccess.PopuplateDropDownList(ddliZone, "pr_GetZone", "Zone", "ID", "");
        helperAccess.PopuplateDropDownList(ddlBusinessArea, "pr_GetCompanyCodeListCC 0", "Company_Name", "Company_Id", "");
        //27/05/19 changed by manali
        ddlApprDept.Items.RemoveAt(0);
        ddlApprDept.Items.Insert(0, new ListItem("ALL", ""));
        helperAccess.PopuplateDropDownList(ddlUserName, "pr_Get_Requestors", "LookUp_Desc", "LookUp_Code", "");
        BindModuleDDL();
        }
        catch (Exception ex)
        { _log.Error("PopulateDropDownList", ex); }
    }

    private void ReadModules()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            DataSet ds = objMatAccess.ReadModulesByModuleType("M");

            List<decimal> li = new List<decimal>();
            li.Add(172);
            li.Add(175);
            li.Add(173);
            li.Add(174);

            DataTable dt;

            if (ds.Tables[0].Rows.Count > 0)
            {

                dt = ds.Tables[0].AsEnumerable()
                            .Where(r => !li.Contains(r.Field<decimal>("Module_Id")))
                            .CopyToDataTable();
            }
            else
            {
                dt = null;
            }

            ddlModuleSearch.DataSource = dt;
            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();

            ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", ""));
        }
        catch (Exception ex)
        {
            _log.Error("ReadModules", ex);
            //throw ex;
        }
    }

    private void GetMaterialPendingRequests()
    {
        try
        {
            DataSet dstData = new DataSet();
            dstData = objMatAccess.GetMatPendingRequests(ddlPurchasingGroup.SelectedValue, ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), txtPendingDays.Text.Trim(), ddlStatus.SelectedValue, ddlApprDept.SelectedValue, ddlUserName.SelectedValue);

            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            if (ddlStatus.SelectedValue == "ALL")
            {
                grdSearch.Columns[10].HeaderText = "Approved By";
                grdSearch.Columns[11].Visible = false;
            }
            else
            {
                grdSearch.Columns[10].HeaderText = "First Approver";
                grdSearch.Columns[11].Visible = true;
            }
            if (ddlStatus.SelectedValue == "")
                grdSearch.Columns[7].Visible = true;
            else
                grdSearch.Columns[7].Visible = false;

            if (ddlApprDept.SelectedValue == "")
                grdSearch.Columns[8].Visible = true;
            else
                grdSearch.Columns[8].Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.RptPurchGrp] = ddlPurchasingGroup.SelectedValue;
                Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.RptPendingDays] = txtPendingDays.Text.Trim();
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.RptApprDept] = ddlApprDept.SelectedValue;
                Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;
            }
            else
            {
                Session[StaticKeys.RptPurchGrp] = "";
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";
            }
            grdSearch.DataBind();

        }
        catch (Exception ex)
        {
            _log.Error("ReadModules1", ex);
            //throw ex;
        }
    }
   

    private void ReadProfileWiseModules(string profileId, string userId)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        Utility ObjUtil = new Utility();

        try
        {
            DataSet ds = objMatAccess.ReadProfileWiseModules(profileId, userId, "M");

            List<decimal> li = new List<decimal>();
            li.Add(172);
            li.Add(175);

            DataTable dt;

            if (ds.Tables[0].Rows.Count > 0)
            {

                dt = ds.Tables[0].AsEnumerable()
                            .Where(r => !li.Contains(r.Field<decimal>("Module_Id")))
                            .CopyToDataTable();
            }
            else
            {
                dt = null;
            }

            ddlModuleSearch.DataSource = dt;
            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();

            ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", ""));
        }
        catch (Exception ex)
        {
            _log.Error("ReadProfileWiseModules", ex);
            //throw ex;
        }
    }

    //protected void DownLoadDataInExcel()
    //{
    //    string fileName = "";

    //    DataSet dstData = new DataSet();

    //    fileName = DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();

    //    //dstData = objMatAccess.GetMatPendingRequests(ddlPurchasingGroup.SelectedValue, ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), txtPendingDays.Text.Trim());

    //    string filePath = Server.MapPath("../../tempFile/" + fileName + ".xls");
    //    if (System.IO.File.Exists(filePath))
    //    {
    //        System.IO.File.Delete(filePath);
    //    }

    //    CreateWorkbook(filePath, dstData);
    //    DownloadFile(filePath, fileName);
    //}

    //public void CreateWorkbook(String filePath, DataSet dataset)
    //{
    //    if (dataset.Tables.Count == 0)
    //        throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");

    //    Workbook workbook = new Workbook();
    //    CellStyle style = new CellStyle();
    //    style.BackColor = System.Drawing.Color.Beige;

    //    foreach (DataTable dt in dataset.Tables)
    //    {
    //        Worksheet worksheet = new Worksheet(dt.TableName);

    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            // Add column header
    //            worksheet.Cells[0, i].Style = style;
    //            worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);

    //            // Populate row data
    //            for (int j = 0; j < dt.Rows.Count; j++)
    //                worksheet.Cells[j + 1, i] = new Cell(SafeTypeHandling.ConvertToString(dt.Rows[j][i]));

    //        }
    //        workbook.Worksheets.Add(worksheet);
    //    }

    //    workbook.Save(filePath);
    //}

    //private void DownloadFile(string filePath, string fileName)
    //{
    //    fileName = fileName + ".xls";
    //    //string filePath = Server.MapPath("../../tempFile/" + fileName);

    //    Response.ContentType = "application/octet-stream";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
    //    Response.TransmitFile(filePath);

    //    //Response.End();
    //}

    //private void DeleteFile(string fileName)
    //{
    //    string filePath = Server.MapPath("../../tempFile/");
    //    string type = "File";
    //    if (type == "File")
    //    {
    //        System.IO.File.Delete(filePath);
    //    }
    //    else if (type == "Directory")
    //    {
    //        System.IO.Directory.Delete(filePath, true);
    //    }
    //}

    #endregion


    //27/05/19 changed by manali. Description: onclick on master type corresponding modules get filled in module dropdown.
    protected void ddlMasterTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
        BindModuleDDL();

        if (ddlMasterTypeSearch.SelectedValue == "M")
        {
            txtPendingDays.Text = "5";
        }
        else
        {
            txtPendingDays.Text = "0";
        }

        //if (ddlMasterTypeSearch.SelectedValue != "0")
        //{
        //    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        //    try
        //    {
        //        DataTable dt;
        //        DataSet ds = objMatAccess.ReadModulesByModuleType(Convert.ToString(ddlMasterTypeSearch.SelectedValue));

        //        List<decimal> li = new List<decimal>();

        //        dt = null;
        //        //li.Add(172);
        //        //li.Add(175);
        //        //li.Add(173);
        //        //li.Add(174);



        //        //if (ds.Tables[0].Rows.Count > 0)
        //        //{
        //        //    li.Clear();
        //        //    dt = ds.Tables[0].AsEnumerable()
        //        //                .Where(r => !li.Contains(r.Field<decimal>("Module_Id")))
        //        //                .CopyToDataTable();
        //        //}
        //        //else
        //        //{
        //        //    dt = null;
        //        //}

        //        ddlModuleSearch.DataSource = ds;
        //        ddlModuleSearch.DataTextField = "Module_Name";
        //        ddlModuleSearch.DataValueField = "Module_Id";
        //        ddlModuleSearch.DataBind();
        //        //ddlModuleSearch.Items.RemoveAt(0);
        //        ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        ////27/05/19 changed by manali. Description: onclick on master type corresponding modules get filled in module dropdown.   

        ShowHideControl();
        }
        catch (Exception ex)
        { _log.Error("ddlMasterTypeSearch_SelectedIndexChanged", ex); }
    }

    /// <summary>
    /// manali
    /// </summary>
    private void BindModuleDDL()
    {
        try {
        DataSet ds = objMatAccess.ReadModulesByModuleType(Convert.ToString(ddlMasterTypeSearch.SelectedValue));
        ddlModuleSearch.ClearSelection();
        ddlModuleSearch.DataSource = ds;
        ddlModuleSearch.DataTextField = "Module_Name";
        ddlModuleSearch.DataValueField = "Module_Id";
        ddlModuleSearch.DataBind();
        ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", ""));
        }
        catch (Exception ex)
        { _log.Error("BindModuleDDL", ex); }
    }

    private void ShowHideControl()
    {
        try { 
        ddlPurchasingGroup.Visible = false;
        ddlTerritory.Visible = false;
        ddliZone.Visible = false;
        ddlDivision.Visible = false;
        ddlBusinessArea.Visible = false;

        lblPurchasingGroup.Visible = false;
        lblTerritory.Visible = false;
        lbliZone.Visible = false;
        lblDivision.Visible = false;
        lblBusinessArea.Visible = false;
        ExcelDownload1.Visible = false;

        //BOM_NWF_SDT05072019
        lblMaterialcode.Visible = false;
        txtMaterialcode.Visible = false;
        //BOM_NWF_SDT05072019//
        //divExcelDownload1M.Attributes.Add("style", "width: 98%; display:none !important;");
        //divExcelDownload2CC.Attributes.Add("style", "width: 98%; display:none !important;");
        trgrdSearch.Attributes.Add("style", "display:none !important;");
        trgrdSearchC.Attributes.Add("style", "display:none !important;");
        trgrdSearchV.Attributes.Add("style", "display:none !important;");
        trgrdSearchI.Attributes.Add("style", "display:none !important;");
        trgrdSearchG.Attributes.Add("style", "display:none !important;");
        trgrdSearchB.Attributes.Add("style", "display:none !important;");

        if (ddlMasterTypeSearch.SelectedValue == "M")
        {
            ddlPurchasingGroup.Visible = true;
            lblPurchasingGroup.Visible = true;
            //divExcelDownload1M.Attributes.Add("style", "width: 98%; display:block !important;");
            ExcelDownload1.Visible = true;
            ExcelDownload1.ActionType = "M";
            trgrdSearch.Attributes.Add("style", "");
        }
        else if (ddlMasterTypeSearch.SelectedValue == "C")
        {
            ddlTerritory.Visible = true;
            ddliZone.Visible = true;
            ddlDivision.Visible = true;
            lblTerritory.Visible = true;
            lbliZone.Visible = true;
            lblDivision.Visible = true;
            ExcelDownload1.Visible = true;
            ExcelDownload1.ActionType = "CC";

            //divExcelDownload2CC.Attributes.Add("style", "width: 98%; display:block !important;");
            trgrdSearchC.Attributes.Add("style", "");
        }
        else if (ddlMasterTypeSearch.SelectedValue == "V")
        {
            ExcelDownload1.Visible = true;
            ExcelDownload1.ActionType = "V";
            trgrdSearchV.Attributes.Add("style", "");
        }
        else if (ddlMasterTypeSearch.SelectedValue == "I")
        {
            lblBusinessArea.Visible = true;
            ddlBusinessArea.Visible = true;
            ExcelDownload1.Visible = true;
            ExcelDownload1.ActionType = "I";
            trgrdSearchI.Attributes.Add("style", "");
        }
        else if (ddlMasterTypeSearch.SelectedValue == "G")
        {
            ExcelDownload1.Visible = true;
            ExcelDownload1.ActionType = "G";
            trgrdSearchG.Attributes.Add("style", "");
        }
        //BOM_NWF_SDT05072019
        else if (ddlMasterTypeSearch.SelectedValue == "B")
        {
            lblMaterialcode.Visible = true;
            txtMaterialcode.Visible = true;
            ExcelDownload1.Visible = true;
            ExcelDownload1.ActionType = "B";
            trgrdSearchB.Attributes.Add("style", "");
        }
            //BOM_NWF_SDT05072019
        }
        catch (Exception ex)
        { _log.Error("ShowHideControl", ex); }
    }

    /// <summary>
    /// Created By Manali SDT31052019
    /// Get Customer request data from DB
    /// </summary>
    private void GetCustomerPendingRequests()
    {
        try
        {
            DataSet dstData = new DataSet();

            dstData = objMatAccess.GetCustomerPendingRequests(ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), ddlStatus.SelectedValue, ddlApprDept.SelectedValue, txtPendingDays.Text.Trim(), ddlUserName.SelectedValue, ddliZone.SelectedValue, ddlDivision.SelectedValue, ddlTerritory.SelectedValue);

            grdSearchC.DataSource = dstData.Tables[0].DefaultView;

            //if (ddlStatus.SelectedValue == "ALL")
            //{
            //    grdSearchC.Columns[10].HeaderText = "Approved By";
            //    grdSearchC.Columns[11].Visible = false;
            //}
            //else
            //{
            //    grdSearchC.Columns[10].HeaderText = "First Approver";
            //    grdSearchC.Columns[11].Visible = true;
            //}
            //if (ddlStatus.SelectedValue == "")
            //    grdSearchC.Columns[7].Visible = true;
            //else
            //    grdSearchC.Columns[7].Visible = false;

            //if (ddlApprDept.SelectedValue == "")
            //    grdSearchC.Columns[8].Visible = true;
            //else
            //    grdSearchC.Columns[8].Visible = false;

            if (dstData.Tables[0].Rows.Count > 0)
            {

                Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.RptPendingDays] = txtPendingDays.Text.Trim();
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.RptApprDept] = ddlApprDept.SelectedValue;
                Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;
                /// Created By Manali SDT31052019
                Session[StaticKeys.RptZone] = ddliZone.SelectedValue;
                Session[StaticKeys.RptDivision] = ddlDivision.SelectedValue;
                Session[StaticKeys.RptTerritory] = ddlTerritory.SelectedValue;
                /// Created By Manali SDT31052019

            }
            else
            {
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";

                // Created By Manali SDT31052019
                Session[StaticKeys.RptZone] = "";
                Session[StaticKeys.RptDivision] = "";
                Session[StaticKeys.RptTerritory] = "";
                // Created By Manali SDT31052019
            }
            grdSearchC.DataBind();

        }
        catch (Exception ex)
        {
            _log.Error("GetCustomerPendingRequests", ex);
            //  throw ex;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void GetVendorPendingRequests()
    {
        try
        {
            
            DataSet dstData = new DataSet();

            dstData = objMatAccess.GetVendorPendingRequests(ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), ddlStatus.SelectedValue, ddlApprDept.SelectedValue, txtPendingDays.Text.Trim(), ddlUserName.SelectedValue);

            grdSearchV.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.RptPendingDays] = txtPendingDays.Text.Trim();
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.RptApprDept] = ddlApprDept.SelectedValue;
                Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;

            }
            else
            {
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";

            }
            grdSearchV.DataBind();

        }
        catch (Exception ex)
        {
            _log.Error("GetVendorPendingRequests", ex);
            //  throw ex;
        }
    }


    private void GetGLPendingRequests()
    {
        try
        {
          
            DataSet dstData = new DataSet();

            dstData = objMatAccess.GetGLPendingRequests(ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), ddlStatus.SelectedValue, ddlApprDept.SelectedValue, txtPendingDays.Text.Trim(), ddlUserName.SelectedValue);

            grdSearchG.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.RptPendingDays] = txtPendingDays.Text.Trim();
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.RptApprDept] = ddlApprDept.SelectedValue;
                Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;

            }
            else
            {
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";

            }
            grdSearchG.DataBind();

        }
        catch (Exception ex)
        {
            _log.Error("GetGLPendingRequests", ex);
            //  throw ex;
        }
    }

    private void GetCostCenterPendingRequests()
    {
        try
        {
         
            DataSet dstData = new DataSet();

            dstData = objMatAccess.GetCostCenterPendingRequests(ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), ddlStatus.SelectedValue, ddlApprDept.SelectedValue, txtPendingDays.Text.Trim(), ddlUserName.SelectedValue, ddlBusinessArea.SelectedValue);

            grdSearchI.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.RptPendingDays] = txtPendingDays.Text.Trim();
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.RptApprDept] = ddlApprDept.SelectedValue;
                Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;
                Session[StaticKeys.RptBusinessArea] = ddlBusinessArea.SelectedValue;

            }
            else
            {
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";
                Session[StaticKeys.RptBusinessArea] = "";
            }
            grdSearchI.DataBind();

        }
        catch (Exception ex)
        {
            _log.Error("GetCostCenterPendingRequests", ex);
            //  throw ex;
        }
    }

    /// <summary>
    /// BOM_NWF_SDT05072019
    /// </summary>
    private void GetBOMPendingRequests()
    {
        try
        {
          
            DataSet dstData = new DataSet();

            dstData = objMatAccess.GetBOMPendingRequests(ddlPlant.SelectedValue, ddlModuleSearch.SelectedValue, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), ddlStatus.SelectedValue, ddlApprDept.SelectedValue, txtPendingDays.Text.Trim(), ddlUserName.SelectedValue, Convert.ToString(txtMaterialcode.Text));

            grdSearchB.DataSource = dstData.Tables[0].DefaultView;

            if (dstData.Tables[0].Rows.Count > 0)
            {
                Session[StaticKeys.RptPlant] = ddlPlant.SelectedValue;
                Session[StaticKeys.RptModule] = ddlModuleSearch.SelectedValue;
                Session[StaticKeys.RptFromDate] = ObjUtil.GetMMDDYYYY(txtFromDate.Text);
                Session[StaticKeys.RptToDate] = ObjUtil.GetMMDDYYYY(txtToDate.Text);
                Session[StaticKeys.RptPendingDays] = txtPendingDays.Text.Trim();
                Session[StaticKeys.RptStatus] = ddlStatus.SelectedValue;
                Session[StaticKeys.RptApprDept] = ddlApprDept.SelectedValue;
                Session[StaticKeys.RptCreatedBy] = ddlUserName.SelectedValue;
                Session[StaticKeys.RptMaterialCode] = Convert.ToString(txtMaterialcode.Text);
            }
            else
            {
                Session[StaticKeys.RptPlant] = "";
                Session[StaticKeys.RptModule] = "";
                Session[StaticKeys.RptFromDate] = "";
                Session[StaticKeys.RptToDate] = "";
                Session[StaticKeys.RptPendingDays] = "";
                Session[StaticKeys.RptStatus] = "";
                Session[StaticKeys.RptApprDept] = "";
                Session[StaticKeys.RptCreatedBy] = "";
                Session[StaticKeys.RptMaterialCode] = "";

            }
            grdSearchB.DataBind();

        }
        catch (Exception ex)
        {
            _log.Error("GetBOMPendingRequests", ex);
            //  throw ex;
        }
    }


}