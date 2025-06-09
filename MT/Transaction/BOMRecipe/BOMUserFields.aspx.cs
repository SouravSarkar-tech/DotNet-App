using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_BOMRecipe_BOMUserFields : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    RecipeAccess objRecipeAccess = new RecipeAccess();
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
                        FillRecipeData();
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
                            btnaddRow.Visible = true;
                            if (lblReqStatus.Text == "Open - Rollbacked")
                            {
                                btnaddRow.Visible = false;
                            }
                        }
                        else
                        {
                            btnaddRow.Visible = false;
                        }
                        BindHeaderData();

                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load", ex);
        }
    }



    protected void ddlsActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;

            DropDownList ddlsActivity = (DropDownList)row.FindControl("ddlsActivity");
            CheckBox chkPIKX = (CheckBox)row.FindControl("chkPIKX");
            //CheckBox chkPIKX = (CheckBox)grdDetailAdd.Rows[row.RowIndex].FindControl("chkPIKX");
            DataSet dsbomuser = objRecipeAccess.GetYSNOStatusTB(lblMasterHeaderId.Text.Trim(), ddlsActivity.SelectedValue);
            if (dsbomuser.Tables[0].Rows.Count > 0)
            {
                if (dsbomuser.Tables[0].Rows[0]["sActivity"].ToString() == "YES")
                {

                    chkPIKX.Checked = true;
                }
                else
                {
                    chkPIKX.Checked = false;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("ddlchkPIKX_SelectedIndexChanged", ex); }
    }


    /// <summary>
    /// Bind request header details
    /// </summary>
    private void BindHeaderData()
    {
        try
        {
            UserFieldsMasterAccess UFMasterAccess = new UserFieldsMasterAccess();
            DataSet ds1 = UFMasterAccess.GetDetailsData(lblMasterHeaderId.Text);
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
                AddBlankRow();
            }
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
            string pageURL = btnPrevious.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
        catch (Exception ex) { }
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
            UserFieldsMasterAccess UFMasterAccess = new UserFieldsMasterAccess();
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Get the value of column from the DataKeys using the RowIndex. 
                int id = Convert.ToInt32(rowIndex);
                UFMasterAccess.DeleteRow(id, Convert.ToInt32(lblMasterHeaderId.Text));

                if (grdDetailAdd.Rows.Count == 0)
                {
                    Clear();
                }

                dt.Columns.Add(new DataColumn("Pk_BOM_UserFieldsId"));
                dt.Columns.Add(new DataColumn("sActivity"));
                dt.Columns.Add(new DataColumn("sFieldkey"));
                dt.Columns.Add(new DataColumn("sGFText1"));
                dt.Columns.Add(new DataColumn("sGFText2"));
                dt.Columns.Add(new DataColumn("sGFText3"));
                dt.Columns.Add(new DataColumn("sGFText4"));
                dt.Columns.Add(new DataColumn("sNFQty1"));
                dt.Columns.Add(new DataColumn("sNFQty2"));
                dt.Columns.Add(new DataColumn("sNFValue1"));
                dt.Columns.Add(new DataColumn("sNFValue2"));
                dt.Columns.Add(new DataColumn("dDTdate1"));
                dt.Columns.Add(new DataColumn("dDTdate2"));
                dt.Columns.Add(new DataColumn("bCBKX_Sche"));
                dt.Columns.Add(new DataColumn("bCBIndicator"));

                dt.Columns.Add(new DataColumn("sQUNIT1"));
                dt.Columns.Add(new DataColumn("sQUNIT2"));
                dt.Columns.Add(new DataColumn("sVUNIT1"));
                dt.Columns.Add(new DataColumn("sVUNIT2"));

                foreach (GridViewRow row in grdDetailAdd.Rows)
                {
                    dr = dt.NewRow();

                    dr["Pk_BOM_UserFieldsId"] = (row.FindControl("lblPk_BOM_UserFieldsId") as Label).Text;
                    dr["sActivity"] = (row.FindControl("ddlsActivity") as DropDownList).SelectedValue;
                    dr["sFieldkey"] = (row.FindControl("ddlsFieldkey") as DropDownList).SelectedValue;
                    dr["sGFText1"] = (row.FindControl("txtsGFText1") as TextBox).Text;
                    dr["sGFText2"] = (row.FindControl("txtsGFText2") as TextBox).Text;
                    dr["sGFText3"] = (row.FindControl("txtsGFText3") as TextBox).Text;
                    dr["sGFText4"] = (row.FindControl("txtsGFText4") as TextBox).Text;
                    dr["sNFQty1"] = (row.FindControl("txtsNFQty1") as TextBox).Text;
                    dr["sNFQty2"] = (row.FindControl("txtsNFQty2") as TextBox).Text;
                    dr["sNFValue1"] = (row.FindControl("txtsNFValue1") as TextBox).Text;
                    dr["sNFValue2"] = (row.FindControl("txtsNFValue2") as TextBox).Text;
                    dr["dDTdate1"] = (row.FindControl("txtdDTdate1") as TextBox).Text;
                    dr["dDTdate2"] = (row.FindControl("txtdDTdate2") as TextBox).Text;
                    dr["bCBKX_Sche"] = (row.FindControl("chkPIKX") as CheckBox).Checked ? "True" : "false";
                    dr["bCBIndicator"] = (row.FindControl("chkPIInd") as CheckBox).Checked ? "True" : "false";

                    dr["sQUNIT1"] = (row.FindControl("txtsQUNIT1") as TextBox).Text;
                    dr["sQUNIT2"] = (row.FindControl("txtsQUNIT2") as TextBox).Text;
                    dr["sVUNIT1"] = (row.FindControl("txtsVUNIT1") as TextBox).Text;
                    dr["sVUNIT2"] = (row.FindControl("txtsVUNIT2") as TextBox).Text;

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
    /// Create new blank record
    /// </summary>
    private void AddBlankRow()
    {
        UserFieldsMasterAccess objUFMasterAccess = new UserFieldsMasterAccess();
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        int tempId = 1;
        if (tempId == 1)
        {
            tempId = objUFMasterAccess.GetTempId(Convert.ToInt32(lblMasterHeaderId.Text));
        }

        int j = 0;
        try
        {
            dt.Columns.Add(new DataColumn("Pk_BOM_UserFieldsId"));
            dt.Columns.Add(new DataColumn("sActivity"));
            dt.Columns.Add(new DataColumn("sFieldkey"));
            dt.Columns.Add(new DataColumn("sGFText1"));
            dt.Columns.Add(new DataColumn("sGFText2"));
            dt.Columns.Add(new DataColumn("sGFText3"));
            dt.Columns.Add(new DataColumn("sGFText4"));
            dt.Columns.Add(new DataColumn("sNFQty1"));
            dt.Columns.Add(new DataColumn("sNFQty2"));
            dt.Columns.Add(new DataColumn("sNFValue1"));
            dt.Columns.Add(new DataColumn("sNFValue2"));
            dt.Columns.Add(new DataColumn("dDTdate1"));
            dt.Columns.Add(new DataColumn("dDTdate2"));
            dt.Columns.Add(new DataColumn("bCBKX_Sche"));
            dt.Columns.Add(new DataColumn("bCBIndicator"));

            dt.Columns.Add(new DataColumn("sQUNIT1"));
            dt.Columns.Add(new DataColumn("sQUNIT2"));
            dt.Columns.Add(new DataColumn("sVUNIT1"));
            dt.Columns.Add(new DataColumn("sVUNIT2"));


            foreach (GridViewRow row in grdDetailAdd.Rows)
            {
                dr = dt.NewRow();

                dr["Pk_BOM_UserFieldsId"] = (row.FindControl("lblPk_BOM_UserFieldsId") as Label).Text;
                dr["sActivity"] = (row.FindControl("ddlsActivity") as DropDownList).SelectedValue;
                dr["sFieldkey"] = (row.FindControl("ddlsFieldkey") as DropDownList).SelectedValue;
                dr["sGFText1"] = (row.FindControl("txtsGFText1") as TextBox).Text;
                dr["sGFText2"] = (row.FindControl("txtsGFText2") as TextBox).Text;
                dr["sGFText3"] = (row.FindControl("txtsGFText3") as TextBox).Text;
                dr["sGFText4"] = (row.FindControl("txtsGFText4") as TextBox).Text;
                dr["sNFQty1"] = (row.FindControl("txtsNFQty1") as TextBox).Text;
                dr["sNFQty2"] = (row.FindControl("txtsNFQty2") as TextBox).Text;
                dr["sNFValue1"] = (row.FindControl("txtsNFValue1") as TextBox).Text;
                dr["sNFValue2"] = (row.FindControl("txtsNFValue2") as TextBox).Text;
                dr["dDTdate1"] = (row.FindControl("txtdDTdate1") as TextBox).Text;
                dr["dDTdate2"] = (row.FindControl("txtdDTdate2") as TextBox).Text;
                dr["bCBKX_Sche"] = (row.FindControl("chkPIKX") as CheckBox).Checked ? "True" : "false";
                dr["bCBIndicator"] = (row.FindControl("chkPIInd") as CheckBox).Checked ? "True" : "false";

                dr["sQUNIT1"] = (row.FindControl("txtsQUNIT1") as TextBox).Text;
                dr["sQUNIT2"] = (row.FindControl("txtsQUNIT2") as TextBox).Text;
                dr["sVUNIT1"] = (row.FindControl("txtsVUNIT1") as TextBox).Text;
                dr["sVUNIT2"] = (row.FindControl("txtsVUNIT2") as TextBox).Text;

                dt.Rows.Add(dr);

                if (dr["Pk_BOM_UserFieldsId"].ToString() == "")
                {
                    tempId += 1;

                }
                else
                {
                    tempId = Convert.ToInt32(dr["Pk_BOM_UserFieldsId"]) + 1;
                }

            }
            for (int i = 1; i <= SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text); i++)
            {
                dr = dt.NewRow();

                dr["Pk_BOM_UserFieldsId"] = tempId;
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

    private RecipeHeader GetRecipeHeaderData()
    {
        return objRecipeAccess.GetRecipeHeaderData(lblMasterHeaderId.Text);
    }
    private void FillRecipeData()
    {
        RecipeHeader objRecipeHeader = GetRecipeHeaderData();
        try
        {
            if (objRecipeHeader.Recipe_HeaderID > 0)
            {
                lblRecipeId.Text = objRecipeHeader.Recipe_HeaderID.ToString();
            }
            else
            {
                lblRecipeId.Text = "0";
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("FillRecipeData", ex);
        }

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
                DropDownList ddlsActivity = (DropDownList)e.Row.FindControl("ddlsActivity");
                helperAccess.PopuplateDropDownList(ddlsActivity, "pr_Get_ActivityUF_By_RecipeId 'B','ddlsFieldkey','" + lblRecipeId.Text.Trim() + "'", "Operation_Phase", "Operation_Phase");
                ddlsActivity.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[1].ToString();
                DropDownList ddlsFieldkey = (DropDownList)e.Row.FindControl("ddlsFieldkey");
                helperAccess.PopuplateDropDownList(ddlsFieldkey, "pr_GetDropDownListByControlNameModuleType 'B','ddlsFieldkey'", "LookUp_Desc", "LookUp_Code");
                ddlsFieldkey.SelectedValue = grdDetailAdd.DataKeys[e.Row.RowIndex].Values[2].ToString();
                TextBox txtsGFText1 = (TextBox)e.Row.FindControl("txtsGFText1");
                TextBox txtsGFText2 = (TextBox)e.Row.FindControl("txtsGFText2");
                TextBox txtsGFText3 = (TextBox)e.Row.FindControl("txtsGFText3");
                TextBox txtsGFText4 = (TextBox)e.Row.FindControl("txtsGFText4");
                TextBox txtsNFQty1 = (TextBox)e.Row.FindControl("txtsNFQty1");
                TextBox txtsNFQty2 = (TextBox)e.Row.FindControl("txtsNFQty2");
                TextBox txtsNFValue1 = (TextBox)e.Row.FindControl("txtsNFValue1");
                TextBox txtsNFValue2 = (TextBox)e.Row.FindControl("txtsNFValue2");
                TextBox txtdDTdate1 = (TextBox)e.Row.FindControl("txtdDTdate1");
                TextBox txtdDTdate2 = (TextBox)e.Row.FindControl("txtdDTdate2");
                CheckBox chkPIKX = (CheckBox)e.Row.FindControl("chkPIKX");
                CheckBox chkPIInd = (CheckBox)e.Row.FindControl("chkPIInd");

                TextBox txtsQUNIT1 = (TextBox)e.Row.FindControl("txtsQUNIT1");
                TextBox txtsQUNIT2 = (TextBox)e.Row.FindControl("txtsQUNIT2");
                TextBox txtsVUNIT1 = (TextBox)e.Row.FindControl("txtsVUNIT1");
                TextBox txtsVUNIT2 = (TextBox)e.Row.FindControl("txtsVUNIT2");

                //PROV-CCP-MM-941-23-0076  
                //HiddenField hdnPIKX = e.Row.FindControl("hdnPIKX") as HiddenField;
                //chkPIKX.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPIKX.Value);                
                chkPIKX.Enabled = false;
                //chkPIKX.Checked = true;
                //PROV-CCP-MM-941-23-0076 
                HiddenField hdnPIKX = e.Row.FindControl("hdnPIKX") as HiddenField;
                chkPIKX.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPIKX.Value);

                HiddenField hdnPIInd = e.Row.FindControl("hdnPIInd") as HiddenField;
                chkPIInd.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPIInd.Value);
                //HiddenField hdnPIKX = e.Row.FindControl("hdnPIKX") as HiddenField;
                //chkPIKX.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPIKX.Value);


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
        if (SaveDetail())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("BOMUserFields.aspx");
        }
        else
        {
            lblMsg.Text = "Error while saving Details.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    /// <summary>
    /// Get and set grdview details records
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private BOMUserFields GetDetailData(GridViewRow row)
    {
        BOMUserFields objuf = new BOMUserFields();
        Utility objUtil = new Utility();
        try
        {
            Label lblPk_BOM_UserFieldsId = row.FindControl("lblPk_BOM_UserFieldsId") as Label;
            DropDownList ddlsActivity = row.FindControl("ddlsActivity") as DropDownList;
            DropDownList ddlsFieldkey = row.FindControl("ddlsFieldkey") as DropDownList;
            TextBox txtsGFText1 = row.FindControl("txtsGFText1") as TextBox;
            TextBox txtsGFText2 = row.FindControl("txtsGFText2") as TextBox;
            TextBox txtsGFText3 = row.FindControl("txtsGFText3") as TextBox;
            TextBox txtsGFText4 = row.FindControl("txtsGFText4") as TextBox;
            TextBox txtsNFQty1 = row.FindControl("txtsNFQty1") as TextBox;
            TextBox txtsNFQty2 = row.FindControl("txtsNFQty2") as TextBox;
            TextBox txtsNFValue1 = row.FindControl("txtsNFValue1") as TextBox;
            TextBox txtsNFValue2 = row.FindControl("txtsNFValue2") as TextBox;
            TextBox txtdDTdate1 = row.FindControl("txtdDTdate1") as TextBox;
            TextBox txtdDTdate2 = row.FindControl("txtdDTdate2") as TextBox;
            CheckBox chkPIKX = row.FindControl("chkPIKX") as CheckBox;
            CheckBox chkPIInd = row.FindControl("chkPIInd") as CheckBox;

            TextBox txtsQUNIT1 = row.FindControl("txtsQUNIT1") as TextBox;
            TextBox txtsQUNIT2 = row.FindControl("txtsQUNIT2") as TextBox;
            TextBox txtsVUNIT1 = row.FindControl("txtsVUNIT1") as TextBox;
            TextBox txtsVUNIT2 = row.FindControl("txtsVUNIT2") as TextBox;

            objuf.Pk_BOM_UserFieldsId = Convert.ToInt32(lblPk_BOM_UserFieldsId.Text);
            objuf.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            objuf.sActivity = ddlsActivity.SelectedValue;
            objuf.sFieldkey = ddlsFieldkey.SelectedValue;
            objuf.sGFText1 = txtsGFText1.Text.Trim();
            objuf.sGFText2 = txtsGFText2.Text.Trim();
            objuf.sGFText3 = txtsGFText3.Text.Trim();
            objuf.sGFText4 = txtsGFText4.Text.Trim();
            objuf.sNFQty1 = txtsNFQty1.Text.Trim();
            objuf.sNFQty2 = txtsNFQty2.Text.Trim();
            objuf.sNFValue1 = txtsNFValue1.Text.Trim();
            objuf.sNFValue2 = txtsNFValue2.Text.Trim();
            objuf.dDTdate1 = txtdDTdate1.Text.Trim();
            objuf.dDTdate2 = txtdDTdate2.Text.Trim();
            objuf.bCBKX_Sche = chkPIKX.Checked ? "True" : "False";
            objuf.bCBIndicator = chkPIInd.Checked ? "True" : "False";
            objuf.IsActive = 1;
            objuf.CreatedBy = Convert.ToInt32(lblUserId.Text);
            objuf.CreatedOn = objUtil.GetDate();
            objuf.CreatedBy_IP = objUtil.GetIpAddress();

            objuf.sQUNIT1 = txtsQUNIT1.Text.Trim();
            objuf.sQUNIT2 = txtsQUNIT2.Text.Trim();
            objuf.sVUNIT1 = txtsVUNIT1.Text.Trim();
            objuf.sVUNIT2 = txtsVUNIT2.Text.Trim();
        }
        catch (Exception ex)
        {

        }
        return objuf;
    }

    /// <summary>
    /// Save details
    /// </summary>
    /// <returns></returns>
    private bool SaveDetail()
    {
        UserFieldsMasterAccess objUFAccess = new UserFieldsMasterAccess();
        bool flg = false;
        try
        {
            foreach (GridViewRow row in grdDetailAdd.Rows)
            {
                BOMUserFields objBOMuf = new BOMUserFields();
                objBOMuf = GetDetailData(row);
                if (objUFAccess.SaveDetails(objBOMuf) > 0)
                {
                    flg = true;
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

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
        if (SaveDetail())
        {
            lblMsg.Text = Messages.GetMessage(1);
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
        else
        {
            lblMsg.Text = "Error while saving Details.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

}