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

public partial class Transaction_Vendor_TANExemption : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    HelperAccess helperAccess = new HelperAccess();

    /// <summary>
    /// Bind data on page load event
    /// 8400000388
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                        //try
                        //{
                        //    lblReqStatus.Text = Session[StaticKeys.ReqStatus].ToString();
                        //}
                        //catch (Exception ex) { }

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                        lblFlag.Text = "";
                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            lblFlag.Text = "R";
                            trButton.Visible = true;
                            btnaddRow.Visible = true;


                            //if (lblReqStatus.Text == "Open - Rollbacked")
                            //{
                            //    btnaddRow.Visible = false;
                            //    grdDetailAdd.Columns[0].Visible = false;
                            //}
                        }
                        else
                        {
                            btnaddRow.Visible = false;
                        }
                        BindHeaderData();
                         

                    }
                    else
                    {
                        Response.Redirect("VendorMaster.aspx", false);
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
    /// Bind request header details done
    /// </summary>
    private void BindHeaderData()
    {
        try
        {
            TANExemptionAccess objtanaccess = new TANExemptionAccess();
            DataSet ds1 = objtanaccess.GetDetailsData(lblMasterHeaderId.Text);
            if (ds1 != null && ds1.Tables != null && ds1.Tables[0].Rows.Count > 0)
            {
                grdDetailAdd.DataSource = ds1;
                grdDetailAdd.DataBind();
            }
            else
            {
                grdDetailAdd.DataSource = null;
                grdDetailAdd.DataBind(); 
            }
            if (grdDetailAdd.Rows.Count < 1)
            {
                if(Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2")
                { 
                AddBlankRow();
                }
            }
        }
        catch (Exception ex) { }
    }

    /// <summary>
    /// done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveDetail())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            } 
        }
        catch (Exception ex) { }
    }
     
    /// <summary>
    /// on add row command delete selected row done
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
            TANExemptionAccess objtanaccess = new TANExemptionAccess();
            try
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Get the value of column from the DataKeys using the RowIndex. 
                int id = Convert.ToInt32(rowIndex);
                objtanaccess.DeleteRow(id, Convert.ToInt32(lblMasterHeaderId.Text));
                 
                if (grdDetailAdd.Rows.Count == 0)
                {
                    Clear();
                }

                dt.Columns.Add(new DataColumn("Pk_TANId"));
                dt.Columns.Add(new DataColumn("sSectionCode"));
                dt.Columns.Add(new DataColumn("sExemptNum"));
                dt.Columns.Add(new DataColumn("sExemptRate"));
                dt.Columns.Add(new DataColumn("dExemptFrom"));
                dt.Columns.Add(new DataColumn("dExemptTo"));
                dt.Columns.Add(new DataColumn("sExemptReason"));
                dt.Columns.Add(new DataColumn("sWHTType"));
                dt.Columns.Add(new DataColumn("sWtaxCode"));
                dt.Columns.Add(new DataColumn("sExemThreshold"));
                dt.Columns.Add(new DataColumn("sCurrency"));


                foreach (GridViewRow row in grdDetailAdd.Rows)
                {
                    dr = dt.NewRow();

                    dr["Pk_TANId"] = (row.FindControl("lblPk_TANId") as Label).Text;
                    dr["sSectionCode"] = (row.FindControl("ddlsSectionCode") as DropDownList).SelectedValue;
                    dr["sExemptNum"] = (row.FindControl("ddlsExemptNum") as DropDownList).SelectedValue;
                    dr["sExemptRate"] = (row.FindControl("txtsExemptRate") as TextBox).Text;
                    dr["dExemptFrom"] = (row.FindControl("txtdExemptFrom") as TextBox).Text;
                    dr["dExemptTo"] = (row.FindControl("txtdExemptTo") as TextBox).Text;

                    dr["sExemptReason"] = (row.FindControl("ddlsExemptReason") as DropDownList).SelectedValue;
                    dr["sWHTType"] = (row.FindControl("ddlsWHTType") as DropDownList).SelectedValue;
                    dr["sWtaxCode"] = (row.FindControl("ddlsWtaxCode") as DropDownList).SelectedValue;

                    dr["sExemThreshold"] = (row.FindControl("txtsExemThreshold") as TextBox).Text;
                    dr["sCurrency"] = (row.FindControl("txtsCurrency") as TextBox).Text;

                     
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
    /// Create new blank record done
    /// </summary>
    private void AddBlankRow()
    {
        TANExemptionAccess objtanAccess = new TANExemptionAccess();
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        int tempId = 1;
        if (tempId == 1)
        {
            tempId = objtanAccess.GetTempId(Convert.ToInt32(lblMasterHeaderId.Text));
        }

        int j = 0;
        try
        {
            dt.Columns.Add(new DataColumn("Pk_TANId")); 
            dt.Columns.Add(new DataColumn("sSectionCode"));
            dt.Columns.Add(new DataColumn("sExemptNum"));
            dt.Columns.Add(new DataColumn("sExemptRate"));
            dt.Columns.Add(new DataColumn("dExemptFrom"));
            dt.Columns.Add(new DataColumn("dExemptTo"));
            dt.Columns.Add(new DataColumn("sExemptReason"));
            dt.Columns.Add(new DataColumn("sWHTType"));
            dt.Columns.Add(new DataColumn("sWtaxCode"));
            dt.Columns.Add(new DataColumn("sExemThreshold"));
            dt.Columns.Add(new DataColumn("sCurrency"));

            foreach (GridViewRow row in grdDetailAdd.Rows)
            {
                dr = dt.NewRow();

                dr["Pk_TANId"] = (row.FindControl("lblPk_TANId") as Label).Text;
                dr["sSectionCode"] = (row.FindControl("ddlsSectionCode") as DropDownList).SelectedValue;
                dr["sExemptNum"] = (row.FindControl("ddlsExemptNum") as DropDownList).SelectedValue;
                dr["sExemptRate"] = (row.FindControl("txtsExemptRate") as TextBox).Text;
                dr["dExemptFrom"] = (row.FindControl("txtdExemptFrom") as TextBox).Text;
                dr["dExemptTo"] = (row.FindControl("txtdExemptTo") as TextBox).Text;

                dr["sExemptReason"] = (row.FindControl("ddlsExemptReason") as DropDownList).SelectedValue;
                dr["sWHTType"] = (row.FindControl("ddlsWHTType") as DropDownList).SelectedValue;
                dr["sWtaxCode"] = (row.FindControl("ddlsWtaxCode") as DropDownList).SelectedValue;

                dr["sExemThreshold"] = (row.FindControl("txtsExemThreshold") as TextBox).Text;
                dr["sCurrency"] = (row.FindControl("txtsCurrency") as TextBox).Text;


                dt.Rows.Add(dr);

                if (dr["Pk_TANId"].ToString() == "")
                {
                    tempId += 1;

                }
                else
                {
                    tempId = Convert.ToInt32(dr["Pk_TANId"]) + 1;
                }

            }
            for (int i = 1; i <= SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text); i++)
            {
                dr = dt.NewRow();

                dr["Pk_TANId"] = tempId;
                dr["sCurrency"] = "INR";
                try { 
                dr["dExemptFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
                dr["dExemptTo"] = DateTime.Now.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {

                }
                dt.Rows.Add(dr);
            }

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
    /// done
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
    /// add row data bound add and bind records details done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDetailAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlsSectionCode = (DropDownList)e.Row.FindControl("ddlsSectionCode");
                helperAccess.PopuplateDropDownList(ddlsSectionCode, "pr_GetDropDownListByControlNameModuleType 'V','ddlsSectionCode'", "LookUp_Desc", "LookUp_Code");
                ddlsSectionCode.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[1].ToString();

                DropDownList ddlsExemptNum = (DropDownList)e.Row.FindControl("ddlsExemptNum");
                helperAccess.PopuplateDropDownList(ddlsExemptNum, "pr_GetDropDownListByControlNameModuleType 'V','ddlsExemptNum'", "LookUp_Desc", "LookUp_Code");
                ddlsExemptNum.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[2].ToString();

                TextBox txtsExemptRate = (TextBox)e.Row.FindControl("txtsExemptRate");
                TextBox txtdExemptFrom = (TextBox)e.Row.FindControl("txtdExemptFrom");
                TextBox txtdExemptTo = (TextBox)e.Row.FindControl("txtdExemptTo");

                DropDownList ddlsExemptReason = (DropDownList)e.Row.FindControl("ddlsExemptReason");
                helperAccess.PopuplateDropDownList(ddlsExemptReason, "pr_GetDropDownListByControlNameModuleType 'V','ddlsExemptReason'", "LookUp_Desc", "LookUp_Code");
                ddlsExemptReason.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[3].ToString();

                DropDownList ddlsWHTType = (DropDownList)e.Row.FindControl("ddlsWHTType");
                helperAccess.PopuplateDropDownList(ddlsWHTType, "pr_GetDropDownListByControlNameModuleType 'V','ddlsWHTType'", "LookUp_Desc", "LookUp_Code");
                ddlsWHTType.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[4].ToString();

                DropDownList ddlsWtaxCode = (DropDownList)e.Row.FindControl("ddlsWtaxCode");
                //helperAccess.PopuplateDropDownList(ddlsWtaxCode, "pr_GetDropDownListByControlNameModuleType 'V','ddlsWtaxCode'", "LookUp_Desc", "LookUp_Code");

                helperAccess.PopuplateDropDownList(ddlsWtaxCode, "pr_GetWHTDetailByTaxType 'ddlsWtaxCode','" + ddlsWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                ddlsWtaxCode.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[5].ToString();

                TextBox txtsExemThreshold = (TextBox)e.Row.FindControl("txtsExemThreshold");
                TextBox txtsCurrency = (TextBox)e.Row.FindControl("txtsCurrency");

                if (lblFlag.Text == "R" )//&& lblReqStatus.Text != "Open - Rollbacked")
                {
                    grdDetailAdd.Columns[0].Visible = true;

                }
                else
                {
                    grdDetailAdd.Columns[0].Visible = false;
                }
            }
        }
        catch (Exception ex) { }
    }



    protected void ddlsWHTType_SelectedIndexChanged(object sender, EventArgs e)
    { 
        try
        {
            GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
            DropDownList ddlsWHTType = (DropDownList)row.FindControl("ddlsWHTType");
            DropDownList ddlsWtaxCode = (DropDownList)row.FindControl("ddlsWtaxCode");

            if (ddlsWHTType.SelectedValue != "")
            {
                helperAccess.PopuplateDropDownList(ddlsWtaxCode, "pr_GetWHTDetailByTaxType 'ddlsWtaxCode','" + ddlsWHTType.SelectedValue + "'", "WHT_Detail_Description", "WHT_Detail_Code", "");
                
            }

        }
        catch (Exception ex)
        { _log.Error("ddlsWHTType_SelectedIndexChanged", ex); }
    }


    /// <summary>
    /// inser and update records details in DB table done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveDetail())
        {
            Response.Redirect("TANExemption.aspx");
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            //Response.Redirect("TANExemption.aspx");
        }
        else
        {
            lblMsg.Text = "Error while saving Details.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    /// <summary>
    /// Get and set grdview details records done
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private TANExemptions GetDetailData(GridViewRow row)
    {
        TANExemptions objtan = new TANExemptions();
        Utility objUtil = new Utility();
        try
        {
            Label lblPk_TANId = row.FindControl("lblPk_TANId") as Label;
            DropDownList ddlsSectionCode = row.FindControl("ddlsSectionCode") as DropDownList;
            DropDownList ddlsExemptNum = row.FindControl("ddlsExemptNum") as DropDownList;
            TextBox txtsExemptRate = row.FindControl("txtsExemptRate") as TextBox;
            TextBox txtdExemptFrom = row.FindControl("txtdExemptFrom") as TextBox;
            TextBox txtdExemptTo = row.FindControl("txtdExemptTo") as TextBox;
            DropDownList ddlsExemptReason = row.FindControl("ddlsExemptReason") as DropDownList;
            DropDownList ddlsWHTType = row.FindControl("ddlsWHTType") as DropDownList;
            DropDownList ddlsWtaxCode = row.FindControl("ddlsWtaxCode") as DropDownList;
            TextBox txtsExemThreshold = row.FindControl("txtsExemThreshold") as TextBox;
            TextBox txtsCurrency = row.FindControl("txtsCurrency") as TextBox;

            objtan.Pk_TANId = Convert.ToInt32(lblPk_TANId.Text);
            objtan.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            objtan.sSectionCode = ddlsSectionCode.SelectedValue;
            objtan.sExemptNum = ddlsExemptNum.SelectedValue;
            objtan.sExemptRate = txtsExemptRate.Text.Trim();
            objtan.dExemptFrom = objUtil.GetYYYYMMDD(txtdExemptFrom.Text.Trim()); //txtdExemptFrom.Text.Trim();
            objtan.dExemptTo = objUtil.GetYYYYMMDD(txtdExemptTo.Text.Trim()); //txtdExemptTo.Text.Trim();
            objtan.sExemptReason = ddlsExemptReason.SelectedValue;
            objtan.sWHTType = ddlsWHTType.SelectedValue;
            objtan.sWtaxCode = ddlsWtaxCode.SelectedValue;
            objtan.sExemThreshold = txtsExemThreshold.Text.Trim();
            objtan.sCurrency = txtsCurrency.Text.Trim();

            objtan.IsActive = 1;
            objtan.CreatedBy = Convert.ToInt32(lblUserId.Text);
            objtan.CreatedOn = objUtil.GetDate();
            objtan.CreatedIp = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {

        }
        return objtan;
    }

    /// <summary>
    /// Save details done
    /// </summary>
    /// <returns></returns>
    private bool SaveDetail()
    {

        TANExemptionAccess objtanAccess = new TANExemptionAccess();
        bool flg = false;

        if (grdDetailAdd.Rows.Count >0)
        {
            try
            {
                foreach (GridViewRow row in grdDetailAdd.Rows)
                {
                    TANExemptions objtan = new TANExemptions();
                    objtan = GetDetailData(row);
                    if (objtanAccess.SaveDetails(objtan) > 0)
                    {
                        flg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        else
        {
            lblMsg.Text = "Please add atleast one record.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }

        return flg;
    }

    /// <summary>
    /// Add new blank records in data view done
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

    /// <summary>
    /// done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
    }


}