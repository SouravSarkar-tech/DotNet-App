using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class Transaction_ZcapHsnMaster_ZcapHsnCreate : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();

    /// <summary>
    /// Bind data on page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    if (Session[StaticKeys.MasterHeaderId] != null)
                    {
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                        string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();
                        //PopulateDropDownList(userProfileId);
                        try
                        {
                            lblReqStatus.Text = Session[StaticKeys.ReqStatus].ToString();
                        }
                        catch (Exception ex) { }

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                        lblFlag.Text = "";
                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            lblFlag.Text = "R";
                            trButton.Visible = true;
                            grdAttachedDocs.Columns[1].Visible = true;
                            file_upload.Visible = true;
                            //btnSave.Visible = !btnNext.Visible;
                            //if (!btnPrevious.Visible && !btnNext.Visible)
                            //{
                            //    btnSave.Visible = false;
                            //}
                            ExcelDownload1.Visible = false;
                            ZcapHsnExcelUpload.Visible = true;
                            btnaddRow.Visible = true;
                            //try
                            //{
                            //    if (lblReqStatus.Text == "Open - Rollbacked")
                            //    {
                            //        ZcapHsnExcelUpload.Visible = false;
                            //        btnaddRow.Visible = false;
                            //        grdDetailAdd.Columns[0].Visible = false;
                            //        //grdDetailAdd.Columns[1]. = false;
                            //    }
                            //}
                            //catch (Exception ex) { }

                            if (lblReqStatus.Text == "Open - Rollbacked")
                            {
                                ZcapHsnExcelUpload.Visible = false;
                                btnaddRow.Visible = false;
                                grdDetailAdd.Columns[0].Visible = false;
                                //grdDetailAdd.Columns[1]. = false;
                            }
                        }
                        else
                        {
                            grdAttachedDocs.Columns[1].Visible = false;
                            file_upload.Visible = false;
                            btnaddRow.Visible = false;
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                ExcelDownload1.Visible = true;
                                ZcapHsnExcelUpload.Visible = false;
                            }
                        }
                        BindHeaderData();
                        FillDashBoard();

                        BindAttachedDocuments(lblMasterHeaderId.Text);
                    }
                    else
                    {
                        Response.Redirect("ZcapHsnMaster.aspx", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load", ex);
        }
    }

    /// <summary>
    /// Bind request header details
    /// </summary>
    private void BindHeaderData()
    {
        try
        {
            ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
            DataSet ds1 = zcapHsnaccess.GetDetailsData(lblMasterHeaderId.Text);
            if (ds1 != null && ds1.Tables != null && ds1.Tables[0].Rows.Count > 0)
            {
                grdDetailAdd.DataSource = ds1;
                grdDetailAdd.DataBind();
            }
            else
            {
                grdDetailAdd.DataSource = null;
                grdDetailAdd.DataBind();
                //AddBlankRow();
            }
            if (grdDetailAdd.Rows.Count < 1)
            {
                AddBlankRow();
            }
            //if (grdDetailAdd == null)
            //{
            //    if (grdDetailAdd.Rows.Count <= 0)
            //    {
            //        AddBlankRow();
            //    }
            //}
        }
        catch (Exception ex) { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            //string pageURL = btnPrevious.CommandArgument.ToString();
            //Response.Redirect(pageURL);
        }
        catch (Exception ex) { }
    }

    private bool Valid()
    {
        bool flg = false;
        string moduleIds = lblModuleId.Text.Trim();// Session[StaticKeys.SelectedModuleId].ToString();
        if (grdDetailAdd.Rows.Count != 0)
        {
            foreach (GridViewRow row in grdDetailAdd.Rows)
            {
                string p1 = (row.FindControl("txtsMaterial_Code") as TextBox).Text;
                string p2 = (row.FindControl("txtsSupp_plant") as TextBox).Text;
                string p3 = (row.FindControl("txtsRece_plant") as TextBox).Text;
                string p4 = (row.FindControl("ddlsCondintion_type") as DropDownList).SelectedValue;
                string p5 = (row.FindControl("txtsZcapRate") as TextBox).Text;
                string p6 = (row.FindControl("txtsUOM") as TextBox).Text;
                //string p7 = (row.FindControl("txtsSTONum") as TextBox).Text;
                string p8 = (row.FindControl("txtsHSN_Code") as TextBox).Text;
                string p9 = (row.FindControl("txtsGST_Code") as TextBox).Text;
                //string p10 = (row.FindControl("txtsIsLUTCond") as TextBox).Text;
                //string p11 = (row.FindControl("txtsRemarks") as TextBox).Text;
                string p12 = (row.FindControl("txtsMaterial_Name") as TextBox).Text;
                if (moduleIds == Convert.ToString(Session[StaticKeys.ModuleZCAP]) && (p1.Trim() == "" || p2.Trim() == "" || p3.Trim() == "" || p4.Trim() == "0" || p5.Trim() == "" || p6.Trim() == "" || p12.Trim() == ""))
                {//ZCAP/ZPEX
                    flg = false;
                    lblMsg.Text += "Fill all Mandatory details.";
                    return flg;
                }
                else if (moduleIds == Convert.ToString(Session[StaticKeys.ModuleZCAP]) && (p1.Trim() == "" || p2.Trim() == "" || p4.Trim() == "0" || p8.Trim() == "" || p9.Trim() == "" || p12.Trim() == ""))
                {//HSN/GST%
                    flg = false;
                    lblMsg.Text += "Fill all Mandatory details.";
                    return flg;
                }
                else if (moduleIds == Convert.ToString(Session[StaticKeys.ModuleZHG]) && (p1.Trim() == "" || p2.Trim() == "" || p3.Trim() == "" || p4.Trim() == "0" || p5.Trim() == "" || p6.Trim() == "" || p8.Trim() == "" || p9.Trim() == "" || p12.Trim() == ""))
                {//ZCAP/ZPEX + HSN/GST%
                    flg = false;
                    lblMsg.Text += "Fill all Mandatory details.";
                    return flg;
                }
                //if (p1.Trim() == "")
                //{
                //    flg = false;
                //    lblMsg.Text += "";
                //    return flg;
                //}
                //else if (p1.Trim() == "" || p2.Trim() == "" || p3.Trim() == "0" || p4.Trim() == "0" || p5.Trim() == "")
                //{
                //    flg = false;
                //    lblMsg.Text += "";
                //    return flg;
                //}
                else
                {
                    flg = true;
                    return flg;
                }
            }
        }
        return flg;
    }
     
    /// <summary>
    /// on add row command delete selected row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDetailAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Control ctl = e.CommandSource as Control;
        GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        if (e.CommandName == "D")
        {
            ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
            try
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Get the value of column from the DataKeys using the RowIndex. 
                int id = Convert.ToInt32(rowIndex);
                zcapHsnaccess.DeleteRow(id, Convert.ToInt32(lblMasterHeaderId.Text));

                //BindHeaderData();

                if (grdDetailAdd.Rows.Count == 0)
                {
                    Clear();
                }

                dt.Columns.Add(new DataColumn("HSN_ZCAP_Detaiils_Id"));
                dt.Columns.Add(new DataColumn("sMaterial_Code"));
                dt.Columns.Add(new DataColumn("sSupp_plant"));
                dt.Columns.Add(new DataColumn("sRece_plant"));
                dt.Columns.Add(new DataColumn("sCondintion_type"));
                dt.Columns.Add(new DataColumn("sZcapRate"));
                dt.Columns.Add(new DataColumn("sUOM"));
                dt.Columns.Add(new DataColumn("sSTONum"));
                dt.Columns.Add(new DataColumn("sHSN_Code"));
                dt.Columns.Add(new DataColumn("sGST_Code"));
                dt.Columns.Add(new DataColumn("sIsLUTCond"));
                dt.Columns.Add(new DataColumn("sRemarks"));
                dt.Columns.Add(new DataColumn("sMaterial_Name"));

                foreach (GridViewRow row in grdDetailAdd.Rows)
                {
                    dr = dt.NewRow();

                    dr["HSN_ZCAP_Detaiils_Id"] = (row.FindControl("lblHSN_ZCAP_Detaiils_Id") as Label).Text;
                    dr["sMaterial_Code"] = (row.FindControl("txtsMaterial_Code") as TextBox).Text;
                    dr["sSupp_plant"] = (row.FindControl("txtsSupp_plant") as TextBox).Text;
                    dr["sRece_plant"] = (row.FindControl("txtsRece_plant") as TextBox).Text;
                    dr["sCondintion_type"] = (row.FindControl("ddlsCondintion_type") as DropDownList).SelectedValue;
                    dr["sZcapRate"] = (row.FindControl("txtsZcapRate") as TextBox).Text;
                    dr["sUOM"] = (row.FindControl("txtsUOM") as TextBox).Text;
                    dr["sSTONum"] = (row.FindControl("txtsSTONum") as TextBox).Text;
                    dr["sHSN_Code"] = (row.FindControl("txtsHSN_Code") as TextBox).Text;
                    dr["sGST_Code"] = (row.FindControl("txtsGST_Code") as TextBox).Text;
                    //dr["sIsLUTCond"] = (row.FindControl("txtsIsLUTCond") as TextBox).Text;
                    dr["sIsLUTCond"] = (row.FindControl("ddlsIsLUTCond") as DropDownList).SelectedValue;

                    dr["sRemarks"] = (row.FindControl("txtsRemarks") as TextBox).Text;
                    dr["sMaterial_Name"] = (row.FindControl("txtsMaterial_Name") as TextBox).Text;
                    dt.Rows.Add(dr);
                }

                dstData.Tables.Add(dt);
                dstData.AcceptChanges();

                dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
                dstData.AcceptChanges();

                grdDetailAdd.DataSource = dstData;
                grdDetailAdd.DataBind();
                ViewState["dstDetail"] = dstData;
            }
            catch (Exception ex)
            {

            }
        }

        pnlMsg.Visible = false;
    }

    /// <summary>
    /// Fill dash board details
    /// </summary>
    private void FillDashBoard()
    {
        try
        {
            ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
            DataTable Dt1;

            Dt1 = zcapHsnaccess.GetRemarksByUser(Convert.ToInt32(lblMasterHeaderId.Text),"0");
            rptZcapHsn.DataSource = Dt1;
            rptZcapHsn.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDashBoard", ex); }

    }

    /// <summary>
    /// Create new blank record
    /// </summary>
    private void AddBlankRow()
    {
        ZcapHsnMasterAccess objZcapHsnAccess = new ZcapHsnMasterAccess();
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        int tempId = 1;
        if (tempId == 1)
        {
            tempId = objZcapHsnAccess.GetTempId(Convert.ToInt32(lblMasterHeaderId.Text));
        }

        int j = 0;
        try
        {
            dt.Columns.Add(new DataColumn("HSN_ZCAP_Detaiils_Id"));
            dt.Columns.Add(new DataColumn("sMaterial_Code"));
            dt.Columns.Add(new DataColumn("sSupp_plant"));
            dt.Columns.Add(new DataColumn("sRece_plant"));
            dt.Columns.Add(new DataColumn("sCondintion_type"));
            dt.Columns.Add(new DataColumn("sZcapRate"));
            dt.Columns.Add(new DataColumn("sUOM"));
            dt.Columns.Add(new DataColumn("sSTONum"));
            dt.Columns.Add(new DataColumn("sHSN_Code"));
            dt.Columns.Add(new DataColumn("sGST_Code"));
            dt.Columns.Add(new DataColumn("sIsLUTCond"));
            dt.Columns.Add(new DataColumn("sRemarks"));
            dt.Columns.Add(new DataColumn("sMaterial_Name"));
            foreach (GridViewRow row in grdDetailAdd.Rows)
            {
                dr = dt.NewRow();

                dr["HSN_ZCAP_Detaiils_Id"] = (row.FindControl("lblHSN_ZCAP_Detaiils_Id") as Label).Text;
                dr["sMaterial_Code"] = (row.FindControl("txtsMaterial_Code") as TextBox).Text;
                dr["sSupp_plant"] = (row.FindControl("txtsSupp_plant") as TextBox).Text;
                dr["sRece_plant"] = (row.FindControl("txtsRece_plant") as TextBox).Text;
                dr["sCondintion_type"] = (row.FindControl("ddlsCondintion_type") as DropDownList).SelectedValue;
                dr["sZcapRate"] = (row.FindControl("txtsZcapRate") as TextBox).Text;
                dr["sUOM"] = (row.FindControl("txtsUOM") as TextBox).Text;
                dr["sSTONum"] = (row.FindControl("txtsSTONum") as TextBox).Text;
                dr["sHSN_Code"] = (row.FindControl("txtsHSN_Code") as TextBox).Text;
                dr["sGST_Code"] = (row.FindControl("txtsGST_Code") as TextBox).Text;
                //dr["sIsLUTCond"] = (row.FindControl("txtsIsLUTCond") as TextBox).Text;
                dr["sIsLUTCond"] = (row.FindControl("ddlsIsLUTCond") as DropDownList).SelectedValue;

                dr["sRemarks"] = (row.FindControl("txtsRemarks") as TextBox).Text;
                dr["sMaterial_Name"] = (row.FindControl("txtsMaterial_Name") as TextBox).Text;
                dt.Rows.Add(dr);

                if (dr["HSN_ZCAP_Detaiils_Id"].ToString() == "")
                {
                    tempId += 1;

                }
                else
                {
                    tempId = Convert.ToInt32(dr["HSN_ZCAP_Detaiils_Id"]) + 1;
                }

                // tempId = Convert.ToInt32(dr["HSN_ZCAP_Detaiils_Id"]) +1;

            }
            for (int i = 1; i <= SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text); i++)
            {
                dr = dt.NewRow();

                dr["HSN_ZCAP_Detaiils_Id"] = tempId;

                //dr["sZcapRate"] = "0.00";
                //dr["sGST_Code"] = "0.00";
                dt.Rows.Add(dr);
            }

            //if (tempId == 1)
            //    j = 1;
            //for (int i = tempId; i < SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text) + tempId; i++)
            //{
            //    dr = dt.NewRow();
            //    dr["HSN_ZCAP_Detaiils_Id"] = tempId;
            //    j++;
            //    dt.Rows.Add(dr);
            //}



            dstData.Tables.Add(dt);
            dstData.AcceptChanges();


            grdDetailAdd.DataSource = dstData;
            grdDetailAdd.DataBind(); ;
            ViewState["dstDetail"] = dstData;
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Clear()
    {
        try
        {
            grdDetailAdd.DataSource = null;
            grdDetailAdd.DataBind();
            if (grdDetailAdd.Rows.Count < 1)
            {
                AddBlankRow();
            }
        }
        catch (Exception ex) { }
    }

    /// <summary>
    /// add row data bound add and bind records details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDetailAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtsMaterial_Code = (TextBox)e.Row.FindControl("txtsMaterial_Code");
                TextBox txtsReceitxtsSupp_plantverMaterial = (TextBox)e.Row.FindControl("txtsSupp_plant");
                TextBox txtsRece_plant = (TextBox)e.Row.FindControl("txtsRece_plant");

                DropDownList ddlsCondintion_type = (DropDownList)e.Row.FindControl("ddlsCondintion_type");
                helperAccess.PopuplateDropDownList(ddlsCondintion_type, "pr_GetDropDownListByControlName_Hsn 'Z','ddlsCondintion_type','0','" + lblModuleId.Text.Trim() + "'", "LookUp_Desc", "LookUp_Code");
                ddlsCondintion_type.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[1].ToString();

                TextBox txtsZcapRate = (TextBox)e.Row.FindControl("txtsZcapRate");
                TextBox txtsUOM = (TextBox)e.Row.FindControl("txtsUOM");
                TextBox txtsSTONum = (TextBox)e.Row.FindControl("txtsSTONum");
                TextBox txtsHSN_Code = (TextBox)e.Row.FindControl("txtsHSN_Code");
                TextBox txtsGST_Code = (TextBox)e.Row.FindControl("txtsGST_Code");
                //TextBox txtsIsLUTCond = (TextBox)e.Row.FindControl("txtsIsLUTCond");

                DropDownList ddlsIsLUTCond = (DropDownList)e.Row.FindControl("ddlsIsLUTCond");
                helperAccess.PopuplateDropDownList(ddlsIsLUTCond, "pr_GetDropDownListByControlName_Hsn 'Z','ddlsIsLUTCond'", "LookUp_Desc", "LookUp_Code");
                ddlsIsLUTCond.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[2].ToString();

                TextBox txtsRemarks = (TextBox)e.Row.FindControl("txtsRemarks");
                TextBox txtsMaterial_Name = (TextBox)e.Row.FindControl("txtsMaterial_Name");
                RequiredFieldValidator reqtxtsRece_plant = (RequiredFieldValidator)e.Row.FindControl("reqtxtsRece_plant");
                //string v1 = (row.FindControl("reqtxtsRece_plant") as RequiredFieldValidator).Text;
                //reqtxtsRece_plant.Visible = false;
                //try
                //{
                //    lblReqStatus.Text
                //}
                //catch (Exception ex) { }
                if (lblFlag.Text == "R" && lblReqStatus.Text != "Open - Rollbacked")
                {
                    grdDetailAdd.Columns[0].Visible = true;
                    //grdDetailAdd.Columns[0].ReadOnly = true;
                    txtsMaterial_Code.ReadOnly = false;
                    //ExcelDownload1

                }
                else
                {
                    grdDetailAdd.Columns[0].Visible = false;
                    txtsMaterial_Code.ReadOnly = true;
                    //grdDetailAdd.Columns[0].Visible = false;
                }
                //Session[StaticKeys.SelectedModuleId].ToString()
                string moduleIds = lblModuleId.Text.Trim();
                if (moduleIds == Convert.ToString(Session[StaticKeys.ModuleZCAP]))
                {//ZCAP/ZPEX
                   
                    grdDetailAdd.Columns[10].Visible = false;
                    grdDetailAdd.Columns[11].Visible = false;
                    grdDetailAdd.Columns[12].Visible = false;

                }
                else if (moduleIds == Convert.ToString(Session[StaticKeys.ModuleHSN]))
                {//HSN/GST%
                    grdDetailAdd.Columns[7].Visible = false;
                    grdDetailAdd.Columns[8].Visible = false;
                    reqtxtsRece_plant.Visible = false;
                }
                //reqtxtsRece_plant.Visible = false;

            }
        }
        catch (Exception ex) { }
    }

    /// <summary>
    /// inser and update records details in DB table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (CheckIsValid())
        //if (Valid())
        //{
        if (SaveDetail() && (SaveDocuments(lblMasterHeaderId.Text)))
        {
            BindAttachedDocuments(lblMasterHeaderId.Text);
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("ZcapHsnCreate.aspx");
        }
        else
        {
            lblMsg.Text = "Error while saving Details.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }

        //}
        //else
        //{
        //    //lblMsg.Text = "Please fill atleast one feild.";
        //    lblMsg.Text = lblMsg.Text;
        //    pnlMsg.Visible = true;
        //    pnlMsg.CssClass = "error";
        //}
    }

    /// <summary>
    /// Get and set grdview details records
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private ZcapHsnMasterCreate GetDetailData(GridViewRow row)
    {
        ZcapHsnMasterCreate objZcapHsn = new ZcapHsnMasterCreate();
        Utility objUtil = new Utility();
        try
        {
            Label lblHSN_ZCAP_Detaiils_Id = row.FindControl("lblHSN_ZCAP_Detaiils_Id") as Label;
            TextBox txtsMaterial_Code = row.FindControl("txtsMaterial_Code") as TextBox;
            TextBox txtsSupp_plant = row.FindControl("txtsSupp_plant") as TextBox;
            TextBox txtsRece_plant = row.FindControl("txtsRece_plant") as TextBox;
            DropDownList ddlsCondintion_type = row.FindControl("ddlsCondintion_type") as DropDownList;
            TextBox txtsZcapRate = row.FindControl("txtsZcapRate") as TextBox;
            TextBox txtsUOM = row.FindControl("txtsUOM") as TextBox;
            TextBox txtsSTONum = row.FindControl("txtsSTONum") as TextBox;
            TextBox txtsHSN_Code = row.FindControl("txtsHSN_Code") as TextBox;
            TextBox txtsGST_Code = row.FindControl("txtsGST_Code") as TextBox;
            //TextBox txtsIsLUTCond = row.FindControl("txtsIsLUTCond") as TextBox;
            DropDownList ddlsIsLUTCond = row.FindControl("ddlsIsLUTCond") as DropDownList;
            TextBox txtsRemarks = row.FindControl("txtsRemarks") as TextBox;
            TextBox txtsMaterial_Name = row.FindControl("txtsMaterial_Name") as TextBox;

            objZcapHsn.HSN_ZCAP_Detaiils_Id = Convert.ToInt32(lblHSN_ZCAP_Detaiils_Id.Text);
            objZcapHsn.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            objZcapHsn.sMaterial_Code = txtsMaterial_Code.Text.Trim();
            objZcapHsn.sSupp_plant = txtsSupp_plant.Text.Trim();
            objZcapHsn.sRece_plant = txtsRece_plant.Text.Trim();
            objZcapHsn.sCondintion_type = ddlsCondintion_type.SelectedValue;
            objZcapHsn.sZcapRate = txtsZcapRate.Text.Trim();
            objZcapHsn.sUOM = txtsUOM.Text.Trim();
            objZcapHsn.sSTONum = txtsSTONum.Text.Trim();
            objZcapHsn.sHSN_Code = txtsHSN_Code.Text.Trim();
            objZcapHsn.sGST_Code = txtsGST_Code.Text.Trim();
            //objZcapHsn.sIsLUTCond = txtsIsLUTCond.Text.Trim();
            objZcapHsn.sIsLUTCond = ddlsIsLUTCond.SelectedValue;
            objZcapHsn.sRemarks = txtsRemarks.Text.Trim();
            objZcapHsn.sMaterial_Name = txtsMaterial_Name.Text.Trim(); 
            objZcapHsn.IsActive = 1;
            objZcapHsn.CreatedBy = Convert.ToInt32(lblUserId.Text);
            objZcapHsn.CreatedOn = objUtil.GetDate();
            objZcapHsn.CreatedIp = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {

        }
        return objZcapHsn;
    }

    /// <summary>
    /// Save details
    /// </summary>
    /// <returns></returns>
    private bool SaveDetail()
    {
        ZcapHsnMasterAccess objZcapHsnAccess = new ZcapHsnMasterAccess();
        bool flg = false;
        try
        {
            foreach (GridViewRow row in grdDetailAdd.Rows)
            {
                ZcapHsnMasterCreate objZcapHsn = new ZcapHsnMasterCreate();
                objZcapHsn = GetDetailData(row);
                if (objZcapHsnAccess.SaveDetails(objZcapHsn) > 0)
                {
                    flg = true;
                }
            }
            if (flg == true)
            {
                var objupdat = objZcapHsnAccess.UpdateDetails(Convert.ToInt32(lblMasterHeaderId.Text));
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return flg;
    }

    private bool CheckIsValid()
    {
        bool flg = false;

        if (grdDetailAdd.Rows.Count > 0)
            flg = true;

        return flg;
    }

    /// <summary>
    /// Add new blank records in data view
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddRow_Click(object sender, EventArgs e)
    {
        try
        {
            AddBlankRow();
        }
        catch (Exception ex) { }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
    }

    #region Document Upload

    /// <summary>
    /// bind attached document
    /// </summary>
    /// <param name="MaterialId"></param>
    private void BindAttachedDocuments(string MaterialId)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                grdAttachedDocs.DataSource = dstData.Tables[0].DefaultView;
                grdAttachedDocs.DataBind();
                grdAttachedDocs.Visible = true;
            }
            else
            {
                grdAttachedDocs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("BindAttachedDocuments", ex);
            //throw ex;
        }
        finally
        {
            objDb = null;
        }
    }

    /// <summary>
    /// Save document
    /// </summary>
    /// <param name="MaterialId"></param>
    /// <returns></returns>
    private bool SaveDocuments(string MaterialId)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/ZcapHsnMaster/ZcapHsnDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
        try
        {
            savePath = MapPath(StrPath);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }
        catch (Exception ex)
        {
            _log.Error("SaveDocuments", ex);
        }
        try
        {
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];

                if (uploadfile.ContentLength > 0)
                {
                    UploadDocument(uploadfile, StrPath, savePath);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            _log.Error("SaveDocuments", ex);
            return false;
        }
    }

    /// <summary>
    /// Upload document in temp folder as well as save path in db table
    /// </summary>
    /// <param name="uploadfile"></param>
    /// <param name="StrPath"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
    private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath)
    {
        DocumentUpload ObjDoc = new DocumentUpload();
        DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
        bool flag = false;
        Utility objUtil = new Utility();

        Random sufix1 = new Random();
        try
        {
            string sufix = sufix1.NextDouble().ToString().Replace(".", "");

            if (uploadfile.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

                string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
                savePath = savePath + "\\" + uploadedFileName;

                ObjDoc.Document_Upload_Id = 0;
                ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
                ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
                ObjDoc.Document_Type = "";
                ObjDoc.Document_Name = Path.GetFileName(uploadfile.FileName);
                ObjDoc.Document_Path = StrPath + uploadedFileName;
                ObjDoc.Remarks = "";
                ObjDoc.IsActive = 1;
                ObjDoc.UserId = lblUserId.Text;
                ObjDoc.IPAddress = objUtil.GetIpAddress();

                uploadfile.SaveAs(savePath);

                ObjDocUploadAccess.Save(ObjDoc);

                flag = true;
            }
            else
            {
                flag = false;
                lblMsg.Text = "Error While Saving Material Details.";
            }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }
        return flag;
    }

    /// <summary>
    /// bind records details in rowcommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdAttachedDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DEL")
            {
                DataAccessLayer objDb = new DataAccessLayer();
                SqlTransaction objTrans;
                Control ctl = e.CommandSource as Control;
                GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
                string documentId = grdAttachedDocs.DataKeys[CurrentRow.RowIndex].Value.ToString();
                Label lblUploadedFileName = grdAttachedDocs.Rows[CurrentRow.RowIndex].FindControl("lblUploadedFileName") as Label;

                try
                {
                    objDb.OpenConnection(this.Page);
                    objTrans = objDb.cnnConnection.BeginTransaction();

                    if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                    {
                        System.IO.File.Delete(Server.MapPath("ZcapHsnDocuments") + "/" + lblUploadedFileName.Text);
                        objTrans.Commit();
                        pnlMsg.Visible = false;
                        BindAttachedDocuments(lblMasterHeaderId.Text);
                    }
                    else
                    {
                        objTrans.Rollback();
                        lblMsg.Text = "Error While Deleting File.";
                        pnlMsg.Visible = true;
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
                    objTrans = null;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("grdAttachedDocs_RowCommand", ex); }
    }
     

    #endregion

    /// <summary>
    /// get and bind dropdown data to ddl control
    /// </summary>
    /// <param name="userProfileId"></param>
    public void PopulateDropDownList(string userProfileId)
    {
        try
        {
            ZcapHsnMasterAccess objZcapHsnAccess = new ZcapHsnMasterAccess();
            DataSet ds;
            ds = objZcapHsnAccess.ReadProfileWiseModules(userProfileId, lblUserId.Text, "Z");

            //ddlModuleName.DataSource = ds;
            //ddlModuleName.DataTextField = "Module_Name";
            //ddlModuleName.DataValueField = "Module_Id";
            //ddlModuleName.DataBind();
        }
        catch (Exception ex) { _log.Error("PopulateDropDownList", ex); }
        HelperAccess helperAccess = new HelperAccess();
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        }
        catch (Exception ex) { _log.Error("PopulateDropDownList1", ex); }
    }

}