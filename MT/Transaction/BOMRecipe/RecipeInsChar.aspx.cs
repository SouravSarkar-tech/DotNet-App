using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using System.Data.OleDb;
using Accenture.MWT.DomainObject;
using System.Web.Configuration;
using System.IO;
using log4net;
using System.Data.SqlClient;
public partial class Transaction_BOMRecipe_RecipeInsChar : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    RecipeAccess objRecipeAccess = new RecipeAccess();
    BOMAccess objBOMAccess = new BOMAccess();
    List<string> lstOperationPhase = new List<string>();
    List<string> lstSecResPhase = new List<string>();
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    if (Session[StaticKeys.SelectedModuleId] != null)
                    {
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                        PopuplateDropDownList();
                        FillRecipeData(mode);
                        pnlInspChara.Visible = true;

                        //ITSM413605 
                        grdAttachedDocs.Columns[1].Visible = false;
                        trSecResfile.Visible = false;
                        trButtonuf.Visible = false;
                        trgrdAtt.Visible = false;
                        //ITSM413605

                        if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M") && (Session[StaticKeys.RequestStatus].ToString() != "P"))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            //if (Session[StaticKeys.SelectedModuleId].ToString() != "185")
                            //{
                            if (!btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = false;
                            }
                            //}

                            trInspPhase.Visible = true;


                            //ITSM413605 
                            trButtonuf.Visible = true;
                            trSecResfile.Visible = true;
                            trgrdAtt.Visible = false;
                            //ITSM413605

                        }
                        //BOM_NWF_SDT05072019
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            if (!btnPrevious.Visible && !btnNext.Visible)
                                btnSave.Visible = false;
                        }
                        //BOM_NWF_EDT05072019

                        //CQA testing
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            //if (Session[StaticKeys.SelectedModuleId].ToString() != "185")
                            //{
                            if (!btnPrevious.Visible && !btnNext.Visible)
                                btnSave.Visible = false;
                            // }
                        }
                        //CQA testing
                        //CQA testing
                        //BOM_8200050878 for new Module created 227,228,229
                        //manali chavan
                        //DT_26-08-2020 
                        if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && (Session[StaticKeys.SelectedModuleId].ToString() == "227"))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            //if (Session[StaticKeys.SelectedModuleId].ToString() != "185")
                            //{
                            if (!btnPrevious.Visible && !btnNext.Visible)
                                btnSave.Visible = false;

                        }
                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = true;
                            btnNext.Visible = false;
                            //Start Commented on 30.05.2018
                            trInspPhase.Visible = true;
                            //End Commented on 30.05.2018
                        }
                        //CQA testing
                        if ((mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27" && Session[StaticKeys.MaterialPlantId].ToString() == "19")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = true;
                            btnNext.Visible = false;
                            //Start Commented on 30.05.2018
                            //btnInsertRecord.Visible = true;
                            trInspPhase.Visible = true;
                            //trSecRes.Visible = true;
                            //End Commented on 30.05.2018
                        }
                        //Secondary resource Save Authorization to Dabhasa location for BFG department 
                        if ((mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14" && Session[StaticKeys.MaterialPlantId].ToString() == "19" && Session[StaticKeys.SelectedModuleId].ToString() == "186")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = true;
                            btnNext.Visible = false;
                            //Start Commented on 30.05.2018
                            //btnInsertRecord.Visible = true;
                            trInspPhase.Visible = true;
                            //trSecRes.Visible = true;
                            //End Commented on 30.05.2018
                        }
                    }
                    //ITSM413605
                    hlMSImportFormat.NavigateUrl = "~/Transaction/BOMRecipe/UploadFormat/MICFile.xlsx";
                    //ITSM413605

                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    #region Inspection Characteristics

    protected void btnAddInspChara_Click(object sender, EventArgs e)
    {
        try
        {

            ddlSamplingProceduremain.Enabled = true;
            ddlInspPtCmptmain.Enabled = true;
            if (ddlOperationPhase.SelectedValue.ToString() != "")
                AddBlankRowInspectionChara();
        }
        catch (Exception ex)
        { _log.Error("btnAddInspChara_Click", ex); }
    }

    protected void gvInspChara_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSamplingProcedure = (DropDownList)e.Row.FindControl("ddlSamplingProcedure");
                helperAccess.PopuplateDropDownList(ddlSamplingProcedure, "pr_GetDropDownListByControlNameModuleType 'R','ddlSamplingProcedure'", "LookUp_Desc", "LookUp_Code", "");
                ddlSamplingProcedure.SelectedValue = gvInspChara.DataKeys[e.Row.RowIndex].Values[1].ToString();

                //DropDownList ddlMIC = (DropDownList)e.Row.FindControl("ddlMIC");
                //helperAccess.PopuplateDropDownList(ddlMIC, "pr_GetDropDownListForMICByPlantCode " + Session[StaticKeys.MaterialPlantId].ToString(), "LookUp_Desc", "LookUp_Code", "");
                //ddlMIC.SelectedValue = gvInspChara.DataKeys[e.Row.RowIndex].Values[2].ToString();

                // Start- commentted by Nitish Rao 28/03/2018
                //DropDownList ddlInspPtCmpt = (DropDownList)e.Row.FindControl("ddlInspPtCmpt");
                //helperAccess.PopuplateDropDownList(ddlInspPtCmpt, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlInspPtCmpt'", "LookUp_Desc", "LookUp_Code", "");
                //ddlInspPtCmpt.SelectedValue = gvInspChara.DataKeys[e.Row.RowIndex].Values[3].ToString();
                // End- commentted by Nitish Rao 28/03/2018

                CheckBox chkNoRelation = e.Row.FindControl("chkNoRelation") as CheckBox;
                HiddenField hdnNoRelation = e.Row.FindControl("hdnNoRelation") as HiddenField;
                chkNoRelation.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnNoRelation.Value);
            }
        }
        catch (Exception ex)
        { _log.Error("gvInspChara_RowCommand", ex); }
    }

    protected void gvInspChara_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Control ctl = e.CommandSource as Control;
        GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        if (e.CommandName == "D")
        {
            try
            {
                dt.Columns.Add(new DataColumn("Operation_Phase"));
                dt.Columns.Add(new DataColumn("Recipe_InspChara_Id"));
                dt.Columns.Add(new DataColumn("Characteristic_No"));
                dt.Columns.Add(new DataColumn("MIC"));
                dt.Columns.Add(new DataColumn("Sampling_Procedure"));
                dt.Columns.Add(new DataColumn("CodeGrp"));
                //Start -Commented by Nitish Rao 28/03/2018
                //dt.Columns.Add(new DataColumn("InspPtCmpt"));
                //End -Commented by Nitish Rao 28/03/2018
                dt.Columns.Add(new DataColumn("NoRelation"));

                foreach (GridViewRow row in gvInspChara.Rows)
                {
                    dr = dt.NewRow();
                    dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
                    dr["Recipe_InspChara_Id"] = (row.FindControl("lblRecipe_InspChara_Id") as Label).Text;
                    dr["Characteristic_No"] = (row.FindControl("txtCharacteristicNo") as TextBox).Text;
                    //dr["MIC"] = (row.FindControl("ddlMIC") as DropDownList).SelectedValue;
                    dr["MIC"] = (row.FindControl("txtMIC") as TextBox).Text;
                    dr["Sampling_Procedure"] = (row.FindControl("ddlSamplingProcedure") as DropDownList).SelectedValue;
                    dr["CodeGrp"] = (row.FindControl("txtCodeGrp") as TextBox).Text;

                    //Start -Commented by Nitish Rao 28/03/2018
                    //dr["InspPtCmpt"] = (row.FindControl("ddlInspPtCmpt") as DropDownList).SelectedValue;
                    //dr["InspPtCmpt"] = ddlInspPtCmptmain.SelectedValue;
                    //End -Commented by Nitish Rao 28/03/2018

                    dr["NoRelation"] = (row.FindControl("chkNoRelation") as CheckBox).Checked ? "True" : "false";
                    dt.Rows.Add(dr);

                    //start Commented by nitish rao 28/03/2018
                    //if (objRecipeAccess.GetInspCharDetail(((Label)currentRow.FindControl("lblRecipe_InspChara_Id")).Text.ToString(), lblRecipeId.Text) > 0)
                    //{
                    int i = objRecipeAccess.DeleteInspCharData(((Label)currentRow.FindControl("lblRecipe_InspChara_Id")).Text.ToString(), lblRecipeId.Text.Trim());
                    //}
                    //End Commented by nitish rao 28/03/2018
                }
                dstData.Tables.Add(dt);
                dstData.AcceptChanges();

                dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
                dstData.AcceptChanges();

                DataView dv = new DataView(dstData.Tables[0]);
                dv.Sort = "Operation_Phase Asc";
                DataTable dtSorted = dv.ToTable();

                gvInspChara.DataSource = dtSorted;
                gvInspChara.DataBind();

                //ViewState["dstInspChar"] = dstData;


            }
            catch (Exception ex)
            {
                //throw ex;
                _log.Error("gvInspChara_RowCommand", ex);
            }
        }
    }


    //protected void ddlMIC_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
    //    DropDownList ddlMIC = (DropDownList)row.FindControl("ddlMIC");

    //    if (ddlMIC.SelectedValue != "")
    //    {
    //        TextBox txtCodeGrp = (TextBox)row.FindControl("txtCodeGrp");
    //        txtCodeGrp.Text = objRecipeAccess.GetCodeGrpByMICNo(ddlMIC.SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
    //        SetupValidationForInspChar(row.RowIndex);
    //    }
    //}

    protected void txtMIC_TextChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            TextBox txtMIC = (TextBox)row.FindControl("txtMIC");

            if (txtMIC.Text != "")
            {
                SetupValidationForInspChar(row.RowIndex);
            }
        }
        catch (Exception ex)
        { _log.Error("txtMIC_TextChanged", ex); }
    }

    //Start Commented by Nitish Rao 28/03/2018
    //protected void ddlInspPtCmpt_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
    //    DropDownList ddlInspPtCmpt = (DropDownList)row.FindControl("ddlInspPtCmpt");
    //    TextBox txtOperationPhase = (TextBox)row.FindControl("txtOperationPhase");
    //    CheckBox chkNoRelation = (CheckBox)row.FindControl("chkNoRelation");

    //    for (int i = 0; i < gvInspChara.Rows.Count; i++)
    //    {
    //        TextBox txtOperationPhaseAll = (TextBox)row.FindControl("txtOperationPhase");
    //        DropDownList ddlInspPtCmptAll = (DropDownList)gvInspChara.Rows[i].FindControl("ddlInspPtCmpt");
    //        CheckBox chkNoRelationAll = (CheckBox)gvInspChara.Rows[i].FindControl("chkNoRelation");
    //        if (txtOperationPhase.Text == txtOperationPhaseAll.Text)
    //        {
    //            ddlInspPtCmptAll.SelectedValue = ddlInspPtCmpt.SelectedValue;
    //            chkNoRelationAll.Checked = chkNoRelation.Checked;
    //        }
    //    }
    //}
    // End Commented by nitish rao 28/03/2018

    #endregion
    private void AddBlankRowInspectionChara()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dsInspChara = new DataSet();
        int tempId = 1;
        string positionNo = string.Empty;
        string OperpositionNo = "";

        try
        {
            dt.Columns.Add(new DataColumn("Operation_Phase"));
            dt.Columns.Add(new DataColumn("Recipe_InspChara_Id"));
            dt.Columns.Add(new DataColumn("Characteristic_No"));
            dt.Columns.Add(new DataColumn("MIC"));
            dt.Columns.Add(new DataColumn("Sampling_Procedure"));
            dt.Columns.Add(new DataColumn("CodeGrp"));
            //start Commented By nitish Rao 28/03/2018
            //dt.Columns.Add(new DataColumn("InspPtCmpt"));
            dt.Columns.Add(new DataColumn("NoRelation"));
            //End Commented By nitish rao 28/03/2018

            foreach (GridViewRow row in gvInspChara.Rows)
            {
                dr = dt.NewRow();
                dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
                dr["Recipe_InspChara_Id"] = (row.FindControl("lblRecipe_InspChara_Id") as Label).Text;
                dr["Characteristic_No"] = (row.FindControl("txtCharacteristicNo") as TextBox).Text;
                //dr["MIC"] = (row.FindControl("ddlMIC") as DropDownList).SelectedValue;
                dr["MIC"] = (row.FindControl("txtMIC") as TextBox).Text;
                //dr["Sampling_Procedure"] = (row.FindControl("ddlSamplingProcedure") as DropDownList).SelectedValue;
                dr["Sampling_Procedure"] = ddlSamplingProceduremain.SelectedValue;
                dr["CodeGrp"] = (row.FindControl("txtCodeGrp") as TextBox).Text;
                //start Commented By nitish Rao 28/03/2018
                //dr["InspPtCmpt"] = (row.FindControl("ddlInspPtCmpt") as DropDownList).SelectedValue;
                ///dr["InspPtCmpt"] = ddlInspPtCmptmain.SelectedValue;
                //End Commented By nitish rao 28/03/2018

                dr["NoRelation"] = (row.FindControl("chkNoRelation") as CheckBox).Checked ? "True" : "false";

                dt.Rows.Add(dr);
                tempId += 1;
                if (!string.IsNullOrEmpty(Convert.ToString(dr["Characteristic_No"])))
                {
                    positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Characteristic_No"]));
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                }
                if (ddlOperationPhase.SelectedValue == (row.FindControl("txtOperationPhase") as TextBox).Text)
                {
                    OperpositionNo = positionNo;
                }
            }
            positionNo = OperpositionNo;
            for (int i = 1; i <= Convert.ToInt32(txtAddRowInsp.Text); i++)
            {
                dr = dt.NewRow();
                //dr["Recipe_InspChara_Id"] = tempId;
                dr["Recipe_InspChara_Id"] = 0;
                //dr["DFlagValue"] = "I";
                if (ddlOperationPhase.SelectedValue != "Select")
                {
                    dr["Operation_Phase"] = ddlOperationPhase.SelectedItem;
                    //positionNo = OperpositionNo;
                }
                if (!string.IsNullOrEmpty(positionNo))
                {
                    positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
                    dr["Characteristic_No"] = positionNo;
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                    dr["Characteristic_No"] = positionNo;
                }
                dr["Sampling_Procedure"] = ddlSamplingProceduremain.SelectedValue;
                dt.Rows.Add(dr);
            }
            dsInspChara.Tables.Add(dt);
            dsInspChara.AcceptChanges();
            gvInspChara.DataSource = dsInspChara.Tables[0].DefaultView;
            gvInspChara.DataBind();

        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("AddBlankRowInspectionChara", ex);
        }
    }

    private void SetupValidationForInspChar(int rowNo)
    {   //Start Commented By nitish rao 28/03/2018
        //RequiredFieldValidator reqddlInspPtCmpt = (RequiredFieldValidator)gvInspChara.Rows[rowNo].FindControl("reqddlInspPtCmpt");
        //DropDownList ddlInspPtCmpt = (DropDownList)gvInspChara.Rows[rowNo].FindControl("ddlInspPtCmpt");
        //End Commented By nitish rao 28/03/2018

        try
        {



            CheckBox chkNoRelation = (CheckBox)gvInspChara.Rows[rowNo].FindControl("chkNoRelation");
            if (ddlInspPoints.SelectedValue != "" && ddlPartialLot.SelectedValue != "-1")
            {
                if (ddlInspPoints.SelectedValue == "Z03" && ddlPartialLot.SelectedValue == "0")
                {
                    //Start Commented By nitish rao 28/03/2018
                    //ddlInspPtCmpt.SelectedValue = "Valuation";
                    //reqddlInspPtCmpt.Enabled = true;
                    reqddlInspPtCmptmain.Enabled = true;
                    ddlInspPtCmptmain.SelectedValue = "Valuation";
                    //End Commented By nitish rao 28/03/2018

                    chkNoRelation.Checked = true;
                }
                else
                {
                    //Start Commented By nitish rao 28/03/2018
                    //ddlInspPtCmpt.SelectedValue = "";
                    //reqddlInspPtCmpt.Enabled = false;
                    reqddlInspPtCmptmain.Enabled = false;
                    ddlInspPtCmptmain.SelectedValue = "";
                    //End Commented By nitish rao 28/03/2018
                    chkNoRelation.Checked = false;
                }
            }
            else
            {
                //Start Commented By nitish rao 28/03/2018
                //ddlInspPtCmpt.SelectedValue = "";
                //reqddlInspPtCmpt.Enabled = false;
                reqddlInspPtCmptmain.Enabled = false;
                ddlInspPtCmptmain.SelectedValue = "";
                //End Commented By nitish rao 28/03/2018
                chkNoRelation.Checked = false;
            }

        }
        catch (Exception ex)
        { _log.Error("SetupValidationForInspChar", ex); }

    }

    protected void ddlSamplingProceduremain_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            for (int i = 0; i < gvInspChara.Rows.Count; i++)
            {

                DropDownList ddlSamplingProcedure = (DropDownList)gvInspChara.Rows[i].FindControl("ddlSamplingProcedure");


                ddlSamplingProcedure.SelectedValue = ddlSamplingProceduremain.SelectedValue;


            }
        }
        catch (Exception ex)
        { _log.Error("ddlSamplingProceduremain_SelectedIndexChanged", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {

            if (SaveDetail())
            {
                FillRecipeData(lblMode.Text);
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
            else
            {
                if (msg != "")
                    lblMsg.Text = msg;
                else
                    lblMsg.Text = "Error while saving Details";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }

        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
            {
                if (SaveInspCharForQA())
                {
                    FillRecipeData(lblMode.Text);
                    lblMsg.Text = "Inspection Data updated successfully.";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                else
                {
                    if (msg != "")
                        lblMsg.Text = msg;
                    else
                        lblMsg.Text = "Error while saving Details";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            //Secondary resource Save Authorization to Dabhasa location for BFG department 
            else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14")
            {
                //if (SaveSecResDetails())
                //{
                //FillRecipeData(lblMode.Text);
                //lblMsg.Text = "Secondory Resource Data updated successfully.";
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;
                //}
                //else
                //{
                //    if (msg != "")
                //        lblMsg.Text = msg;
                //    else
                //        lblMsg.Text = "Error while saving Details";
                //    pnlMsg.CssClass = "error";
                //    pnlMsg.Visible = true;
                //}
            }
            else
            {
                if (SaveDetail())
                {
                    FillRecipeData(lblMode.Text);
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                else
                {
                    if (msg != "")
                        lblMsg.Text = msg;
                    else
                        lblMsg.Text = "Error while saving Details";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
            {
                if (SaveInspCharForQA())
                {
                    FillRecipeData(lblMode.Text);
                    lblMsg.Text = "Inspection Data updated successfully.";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                else
                {
                    if (msg != "")
                        lblMsg.Text = msg;
                    else
                        lblMsg.Text = "Error while saving Details";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                //if (ValidateOperations())
                //{

                if (SaveDetail())
                {
                    //Start Addition By Nitish Rao Date: 14.02.2017
                    // To Update Last Operation Number to All secondary resource item

                    FillRecipeData(lblMode.Text);
                    //start Commented on 30.05.2018
                    //UpdateLastOperation();
                    //End Commented on 30.05.2018
                    string pageURL = btnNext.CommandArgument.ToString();
                    Response.Redirect(pageURL);


                    //End Addition By Nitish Rao Date: 14.02.2017
                }
                else
                {
                    if (msg != "")
                        lblMsg.Text = msg;
                    else
                        lblMsg.Text = "Error while saving Details";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }

    }

    private void PopuplateDropDownList()
    {
        try
        {

            helperAccess.PopuplateDropDownList(ddlInspPoints, "pr_GetDropDownListByControlNameModuleType 'R','ddlInspPoints'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPartialLot, "pr_GetDropDownListByControlNameModuleType 'R','ddlPartialLot'", "LookUp_Desc", "LookUp_Code", "-1");
            helperAccess.PopuplateDropDownList(ddlInspPtCmptmain, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlInspPtCmpt'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlSamplingProceduremain, "pr_GetDropDownListByControlNameModuleType 'R','ddlSamplingProcedure'", "LookUp_Desc", "LookUp_Code", "");
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }
    private void FillRecipeData(string mode)
    {
        RecipeHeader objRecipeHeader = GetRecipeHeaderData();
        try
        {
            if (objRecipeHeader.Recipe_HeaderID > 0)
            {
                lblRecipeId.Text = objRecipeHeader.Recipe_HeaderID.ToString();
                //txtRGroup.Text = objRecipeHeader.Recipe_Group;
                //txtRecipe.Text = objRecipeHeader.Recipe;
                //ddlPlant.SelectedValue = objRecipeHeader.Plant_Id;
                //txtReciepeDesc.Text = objRecipeHeader.TaskListDesc;
                //txtMaterialNmbr.Text = objRecipeHeader.MaterialNo;
                //txtMaterialDescription.Text = objRecipeHeader.MaterialDesc;
                //Session[StaticKeys.BOMRecipeMatNo] = objRecipeHeader.MaterialNo;
                //Session[StaticKeys.BOMRecipeMatDesc] = objRecipeHeader.MaterialDesc;

                RecipeDetail objRecipeDetail = GetRecipeHeaderDetail(lblRecipeId.Text);
                if (objRecipeDetail.Recipe_HeaderDetail_Id > 0)
                {


                    ddlInspPoints.SelectedValue = objRecipeDetail.Insp_Points;
                    ddlPartialLot.SelectedValue = objRecipeDetail.Partial_Lot;


                    Session[StaticKeys.BOMRecipeBUOM] = objRecipeDetail.Unit;
                    Session[StaticKeys.BOMRecipeBaseQty] = objRecipeDetail.Base_Quantity;
                    Session[StaticKeys.BOMRecipeFrom] = objRecipeDetail.From_LSize;
                    Session[StaticKeys.BOMRecipeTo] = objRecipeDetail.To_LSize;



                }
                else
                {
                    ddlPartialLot.SelectedValue = "";
                    ddlInspPoints.SelectedValue = "";
                }


                DataSet dsInspChar = objRecipeAccess.GetRecipeInspChara(lblRecipeId.Text);
                if (dsInspChar.Tables[0].Rows.Count > 0)
                {
                    ddlSamplingProceduremain.Enabled = true;
                    ddlInspPtCmptmain.Enabled = true;
                    //gvInspChara.DataSource = objRecipeAccess.GetRecipeInspChara(lblRecipeId.Text);
                    gvInspChara.DataSource = dsInspChar;
                    gvInspChara.DataBind();

                    foreach (GridViewRow grdRow in gvInspChara.Rows)
                    {
                        SetupValidationForInspChar(grdRow.RowIndex);

                    }

                    for (int i = 0; i < dsInspChar.Tables[0].Rows.Count; i++)
                    {
                        string InspPtCmpt = dsInspChar.Tables[0].Rows[i]["InspPtCmpt"].ToString();

                        ddlInspPtCmptmain.SelectedValue = InspPtCmpt;

                    }
                }
                else
                {
                    ddlSamplingProceduremain.Enabled = false;
                    ddlInspPtCmptmain.Enabled = false;
                }

                //DataSet dsSecRes = objRecipeAccess.GetRecipeSecResources(lblRecipeId.Text);
                //if (dsSecRes.Tables[0].Rows.Count > 0)
                //{
                //    grdSecResources.DataSource = objRecipeAccess.GetRecipeSecResources(lblRecipeId.Text);
                //    grdSecResources.DataBind();

                //    foreach (GridViewRow gvSec in grdSecResources.Rows)
                //    {
                //        if (((DropDownList)gvSec.FindControl("ddlSecResource")).SelectedValue != "")
                //        {
                //            RecipeSecRes objSecRes = objRecipeAccess.GetDataByResourceName(((DropDownList)gvSec.FindControl("ddlSecResource")).SelectedValue, ddlPlant.SelectedValue);
                //            SetValidationForSecRes(objSecRes.StdValKey, gvSec.RowIndex);
                //        }
                //    }
                //}


                //ITSM413605
                try
                {
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                }
                catch (Exception ex) { }
                //ITSM413605

            }
            else
            {
                //lblRecipeId.Text = "0";
                //lblRecipe_HeaderDetail_Id.Text = "0";
                //ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                //txtFrom.Text = "";
                //txtTo.Text = "99999999";
                //ddlRStatus.SelectedValue = "1";
                //ddlUsages.SelectedValue = "1";

                ////Start Commented on 30.05.2018
                ddlPartialLot.SelectedValue = "";
                ddlInspPoints.SelectedValue = "";
                ////End Commented on 30.05.2018

                Session[StaticKeys.BOMRecipeMatNo] = "";
                Session[StaticKeys.BOMRecipeMatDesc] = "";
                Session[StaticKeys.BOMRecipeBUOM] = "";
                Session[StaticKeys.BOMRecipeTo] = "";
                Session[StaticKeys.BOMRecipeFrom] = "";
                Session[StaticKeys.BOMRecipeBaseQty] = "";
                //start Commented on 30.05.2018
                //Add_10BlankRow();
                //End Commented on 30.05.2018
            }
            //start Commented on 30.05.2018
            //SetBaseQuantityOperation(txtBQty.Text);
            //SetActUOMOperation();
            AddOperationForInspection();
            //AddOperationForSecRes();
            //End Commented on 30.05.2018
            ConfigureHeaderControls();
            //to update the recipe status to 3 if chkStatus is checked and QA is the logged in user Start
            //UpdateRecipeStatus();
            //to update the recipe status to 3 if chkStatus is checked and QA is the logged in user End
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("FillRecipeData", ex);
        }
    }

    private void ConfigureHeaderControls()
    {
        try
        {


            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {

                //Start Commented on 30.05.2018
                //GvOperation.Enabled = false;
                //grdSecResources.Enabled = false;
                gvInspChara.Enabled = false;
                ddlInspPoints.Enabled = false;
                ddlPartialLot.Enabled = false;
                //End Commented on 30.05.2018
            }
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            //DT_26-08-2020 
            else if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && (Session[StaticKeys.SelectedModuleId].ToString() == "227"))
            {
                gvInspChara.Enabled = false;
                ddlInspPoints.Enabled = false;
                ddlPartialLot.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureHeaderControls", ex); }

    }

    private RecipeDetail GetRecipeHeaderDetail(string recipe_headerId)
    {
        return objRecipeAccess.GetRecipeHeaderDetail(recipe_headerId);
    }

    private RecipeHeader GetRecipeHeaderData()
    {
        return objRecipeAccess.GetRecipeHeaderData(lblMasterHeaderId.Text);
    }

    private void AddOperationForInspection()
    {
        try
        {


            lstOperationPhase.Clear();
            int cnt = 0;
            DataSet dsOperation = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
            if (dsOperation.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOperation.Tables[0].Rows.Count; i++)
                {
                    string strOperPhas = dsOperation.Tables[0].Rows[i]["Operation_Phase"].ToString();
                    if (dsOperation.Tables[0].Rows[i]["Control_key"].ToString() == "PI02" || dsOperation.Tables[0].Rows[i]["Control_key"].ToString() == "PI03")
                    {
                        lstOperationPhase.Add(strOperPhas);
                        cnt++;
                    }

                }
            }
            if (cnt > 0)
            {
                ddlInspPoints.SelectedValue = "Z03";
                ddlPartialLot.SelectedValue = "0";
            }
            else
            {
                ddlInspPoints.SelectedValue = "";
                ddlPartialLot.SelectedValue = "";
            }

            BindOperationPhase();
        }
        catch (Exception ex)
        { _log.Error("AddOperationForInspection", ex); }
    }

    private void BindOperationPhase()
    {
        try
        {

            lstOperationPhase.Sort();
            ddlOperationPhase.DataSource = lstOperationPhase.Distinct().ToList();
            ddlOperationPhase.DataBind();
        }
        catch (Exception ex)
        { _log.Error("BindOperationPhase", ex); }
        //ddlInspPoints.SelectedValue = "";
        //ddlPartialLot.SelectedValue = "";
    }

    private bool SaveDetail()
    {
        bool flg = false;
        try
        {

            //Start Commented on 30.05.2018
            //Get the operation filled rows
            //int lstRow = GetOperationDataFilledRows();

            DataSet dsOperation = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
            if (dsOperation.Tables[0].Rows.Count > 0)
            {

                if (gvInspChara.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvInspChara.Rows)
                    {
                        RecipeInspChara objRcpInsChar = new RecipeInspChara();
                        objRcpInsChar = GetInspCharData(row);
                        if (objRecipeAccess.SaveInspCharDetails(objRcpInsChar) > 0)
                        {
                            flg = true;
                        }
                        else
                            flg = false;
                    }
                }
                else
                {
                    flg = true;
                }


            }
            else
            {
                msg = "Atleast one resource and phase indicator must be selected.";
                lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            //End Commented on 30.05.2018
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SaveDetail", ex);
        }

        return flg;
    }

    private RecipeInspChara GetInspCharData(GridViewRow row)
    {
        RecipeInspChara objRcpInsChar = new RecipeInspChara();
        Utility objUtil = new Utility();
        try
        {

            Label lblRecipe_InspChara_Id = row.FindControl("lblRecipe_InspChara_Id") as Label;
            TextBox txtOperationPhase = row.FindControl("txtOperationPhase") as TextBox;

            TextBox txtCharacteristicNo = row.FindControl("txtCharacteristicNo") as TextBox;

            //DropDownList ddlMIC = row.FindControl("ddlMIC") as DropDownList;
            TextBox txtMIC = row.FindControl("txtMIC") as TextBox;
            DropDownList ddlSamplingProcedure = row.FindControl("ddlSamplingProcedure") as DropDownList;
            TextBox txtCodeGrp = row.FindControl("txtCodeGrp") as TextBox;
            //start Commented By nitish Rao 28/03/2018
            //DropDownList ddlInspPtCmpt = row.FindControl("ddlInspPtCmpt") as DropDownList;
            //End Commented By nitish rao 28/03/2018
            CheckBox chkNoRelation = row.FindControl("chkNoRelation") as CheckBox;

            objRcpInsChar.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            objRcpInsChar.Recipe_InspChara_Id = Convert.ToInt32(lblRecipe_InspChara_Id.Text);

            objRcpInsChar.Operation_Phase = txtOperationPhase.Text;
            objRcpInsChar.Characteristic_No = txtCharacteristicNo.Text;
            //objRcpInsChar.Master_Insp_Char_Code = ddlMIC.SelectedValue;
            objRcpInsChar.Master_Insp_Char_Code = txtMIC.Text;
            objRcpInsChar.Sampling_Procedure = ddlSamplingProcedure.SelectedValue;
            objRcpInsChar.CodeGrp = txtCodeGrp.Text;
            //start Commented By nitish Rao 28/03/2018
            //objRcpInsChar.InspPtCmpt = ddlInspPtCmpt.SelectedValue;
            objRcpInsChar.InspPtCmpt = ddlInspPtCmptmain.SelectedValue;
            objRcpInsChar.Insp_Point = ddlInspPoints.SelectedValue;
            objRcpInsChar.Partial_Lot = ddlPartialLot.SelectedValue;
            //End Commented By nitish rao 28/03/2018

            objRcpInsChar.NoRelation = chkNoRelation.Checked == true ? "1" : "0";

            objRcpInsChar.UserId = lblUserId.Text;
            objRcpInsChar.TodayDate = objUtil.GetDate();
            objRcpInsChar.IPAddress = objUtil.GetIpAddress();
            objRcpInsChar.Mode = lblMode.Text;
        }
        catch (Exception ex)
        { _log.Error("GetInspCharData", ex); }
        return objRcpInsChar;
    }

    private bool SaveInspCharForQA()
    {
        bool flg = false;
        try
        {


            foreach (GridViewRow row in gvInspChara.Rows)
            {
                RecipeInspChara objRcpInsChar = new RecipeInspChara();
                objRcpInsChar = GetInspCharData(row);
                if (objRecipeAccess.SaveInspCharDetails(objRcpInsChar) > 0)
                {
                    flg = true;
                }
                else
                    flg = false;
            }
        }
        catch (Exception ex)
        { _log.Error("SaveInspCharForQA", ex); }
        return flg;
    }




    #region ITSM413605


    #region Document Upload

    private void BindAttachedDocuments(string MaterialId)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {
            dstData = objDoc.GetDocumentUploadDataBOM(Convert.ToInt32(lblMasterHeaderId.Text), "MIC");
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

    private void DeleteFlerecord()
    {

        DataAccessLayer objDb = new DataAccessLayer();
        SqlTransaction objTrans;

        try
        {
            objDb.OpenConnection(this.Page);
            objTrans = objDb.cnnConnection.BeginTransaction();

            if (objDb.DeleteRecord("T_Document_Upload", "Request_No=" + "'" + Session[StaticKeys.RequestNo].ToString() + "'", ref objDb.cnnConnection, ref objTrans))
            {
                try
                {
                    System.IO.File.Delete(Server.MapPath("MICFile") + "/" + Session[StaticKeys.RequestNo].ToString() + "/");

                }
                catch (Exception ex) { }
                objTrans.Commit();
                pnlMsg.Visible = false;

                BindAttachedDocuments(lblMasterHeaderId.Text);

            }
            else
            {
                objTrans.Rollback();
                //lblMsg.Text = "Error While Deleting File.";
                _log.Info("Error While Deleting File");
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("grdAttachedDocs_RowCommand2", ex);
            //throw ex;
        }
        finally
        {
            objDb.CloseConnection(objDb.cnnConnection);
            objDb = null;
            objTrans = null;
        }
    }

    #endregion

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
                        System.IO.File.Delete(Server.MapPath("MICFile") + "/" + lblUploadedFileName.Text);
                        objTrans.Commit();
                        pnlMsg.Visible = false;

                        BindAttachedDocuments(lblMasterHeaderId.Text);

                        bool flags1 = false;
                        UpdateFlagSecRes objUpdateFlagSecRes1 = new UpdateFlagSecRes();
                        objUpdateFlagSecRes1 = GetFlagSecRes("I");
                        if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes1) > 0)
                        {
                            flags1 = true;
                        }
                        else
                        { flags1 = false; }


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
                    _log.Error("grdAttachedDocs_RowCommand2", ex);
                    //throw ex;
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


    private void Uploaddata()
    {
        DocumentUpload ObjDoc = new DocumentUpload();
        DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
        string savePath = "";
        string StrPath = String.Empty;
        Utility objUtil = new Utility();
        Random sufix1 = new Random();
        string sufix = sufix1.NextDouble().ToString().Replace(".", "");

        if (fileUpdsecResorce.HasFile)
        {
            string ext = Path.GetExtension(fileUpdsecResorce.FileName).ToLower();

            //string filename = Path.GetFileName(fileUpdsecResorce.FileName).ToLower();

            //if ((ext == ".xls" || ext == ".xlsx") && (filename == "SecResources" || filename == "secresources"))
            if (ext == ".xls" || ext == ".xlsx")
            {
                try
                {
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                    foreach (System.Diagnostics.Process p in process)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            try
                            {
                                p.Kill();
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("Uploaddata", ex);
                }
                StrPath = "/Transaction/BOMRecipe/MICFile/" + Session[StaticKeys.RequestNo].ToString() + "/";
                //getting the path of the file    
                //saving the file inside the MyFolder of the server  
                savePath = MapPath(StrPath);

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(fileUpdsecResorce.FileName);
                savePath = savePath + "\\" + uploadedFileName;

                ObjDoc.Document_Upload_Id = 0;
                ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
                ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
                ObjDoc.Document_Type = "MIC";
                ObjDoc.Document_Name = Path.GetFileName(fileUpdsecResorce.FileName);
                ObjDoc.Document_Path = StrPath + uploadedFileName;
                ObjDoc.Remarks = "";
                ObjDoc.IsActive = 1;
                ObjDoc.UserId = lblUserId.Text;
                ObjDoc.IPAddress = objUtil.GetIpAddress();

                fileUpdsecResorce.SaveAs(savePath);

                ObjDocUploadAccess.Save(ObjDoc);

                //fileUpdsecResorce.SaveAs(savePath);
                //string filePath = fileUpdsecResorce.PostedFile.FileName;
                //string filename1 = Path.GetFileName(filePath);
                int count = 0;
                SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

                try
                {
                    System.Data.DataTable dt = Readexcel(ext, savePath);
                    if (dt.Rows.Count > 1000)
                    {
                        string msg = "Maximum data upload limit exceeded, please upload 1000 or less records";
                        lblMsg.Text = msg;
                        pnlMsg.Visible = true;
                        pnlMsg.CssClass = "error";
                    }
                    else
                    {

                        if (dt.Rows.Count > 0)
                        {
                            bool flag1 = false;
                            UpdateFlagSecRes objUpdateFlagSecRes = new UpdateFlagSecRes();
                            objUpdateFlagSecRes = GetFlagSecRes("I");
                            if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes) > 0)
                            {
                                flag1 = true;
                            }
                            else
                            { flag1 = false; }

                            using (SqlCommand cmd = new SqlCommand("pr_Ins_Upd_T_RcpInspChara_Detail_Data_Temp"))
                            {
                                cmd.Connection = con;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                cmd.CommandType = CommandType.StoredProcedure;
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (dt.Rows[i]["Master_Insp_Char_Code"].ToString() != "")
                                    {
                                        try
                                        {
                                            cmd.Parameters.AddWithValue("@Master_HeaderID", lblMasterHeaderId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Recipe_HeaderID", lblRecipeId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Operation_Phase", dt.Rows[i]["Operation_Phase"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Characteristic_No", dt.Rows[i]["Characteristic_No"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Master_Insp_Char_Code", dt.Rows[i]["Master_Insp_Char_Code"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Sampling_Procedure", dt.Rows[i]["Sampling_Procedure"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@CodeGrp", dt.Rows[i]["CodeGrp"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@InspPtCmpt", dt.Rows[i]["InspPtCmpt"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@NoRelation", dt.Rows[i]["NoRelation"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Insp_Point", "");
                                            cmd.Parameters.AddWithValue("@Partial_Lot", "");
                                            cmd.Parameters.AddWithValue("@IsActive", 1);
                                            cmd.Parameters.AddWithValue("@UserId", Session[StaticKeys.LoggedIn_User_Id].ToString());
                                            cmd.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());
                                            SqlDataReader sdr = cmd.ExecuteReader();
                                            sdr.Close();

                                            cmd.Parameters.RemoveAt("@Master_HeaderID");
                                            cmd.Parameters.RemoveAt("@Recipe_HeaderID");
                                            cmd.Parameters.RemoveAt("@Operation_Phase");
                                            cmd.Parameters.RemoveAt("@Characteristic_No");
                                            cmd.Parameters.RemoveAt("@Master_Insp_Char_Code");
                                            cmd.Parameters.RemoveAt("@Sampling_Procedure");
                                            cmd.Parameters.RemoveAt("@CodeGrp");
                                            cmd.Parameters.RemoveAt("@InspPtCmpt");
                                            cmd.Parameters.RemoveAt("@NoRelation");
                                            cmd.Parameters.RemoveAt("@Partial_Lot");
                                            cmd.Parameters.RemoveAt("@IsActive");
                                            cmd.Parameters.RemoveAt("@UserId");
                                            cmd.Parameters.RemoveAt("@UserIp");
                                            count += 1;
                                        }
                                        catch (Exception ex)
                                        {
                                            string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                                            lblMsg.Text = msg;
                                            pnlMsg.Visible = true;
                                            pnlMsg.CssClass = "error";
                                        }
                                    }
                                }
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                if (count == 0)
                                {
                                    string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                                    lblMsg.Text = msg;
                                    pnlMsg.Visible = true;
                                    pnlMsg.CssClass = "error";
                                }
                                else
                                {
                                    bool flag4 = false;
                                    UpdateFlagSecRes objUpdateFlagSecRes1 = new UpdateFlagSecRes();
                                    objUpdateFlagSecRes1 = GetFlagSecRes("V");
                                    if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes1) > 0)
                                    {
                                        btnMSProcess.Visible = false;
                                        btnSRSubmit.Visible = true;
                                        flag4 = true;
                                    }
                                    else
                                    { flag4 = false; }
                                    string msg = "Please click on submit button for next process.";
                                    lblMsg.Text = msg;
                                    pnlMsg.Visible = true;
                                    pnlMsg.CssClass = "error";
                                }
                            }

                        }
                        else
                        {
                            string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload."; //"No data found.";
                            lblMsg.Text = msg;
                            pnlMsg.Visible = true;
                            pnlMsg.CssClass = "error";
                        }
                        try
                        {

                            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                            foreach (System.Diagnostics.Process p in process)
                            {
                                if (!string.IsNullOrEmpty(p.ProcessName))
                                {
                                    try
                                    {
                                        p.Kill();
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _log.Error("Uploaddata1", ex);
                        }

                    }
                }
                catch (Exception ex)
                {
                    _log.Error("Uploaddata2", ex);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form & re-upload.";
                    //string msg = ex.Message.ToString();
                    lblMsg.Text = msg;
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                    try
                    {

                        System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                        foreach (System.Diagnostics.Process p in process)
                        {
                            if (!string.IsNullOrEmpty(p.ProcessName))
                            {
                                try
                                {
                                    p.Kill();
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        _log.Error("Uploaddata3", ex1);
                    }
                }
            }
            else
            {
                string msg = "Please upload Excel file with correct format";
                lblMsg.Text = msg;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        else
        {
            string msg = "Please Select File";
            lblMsg.Text = msg;
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    public System.Data.DataTable Readexcel(string ext, string path)
    {

        System.Data.DataTable dt = new System.Data.DataTable();
        try
        {
            string ConStr = "";
            if (ext.Trim() == ".xls")
            {
                //connection string for that file which extantion is .xls  

                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
            }
            else if (ext.Trim() == ".xlsx")
            {
                //connection string for that file which extantion is .xlsx  

                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            }



            string query = "select * from [MIC$]";
            //string query = "select * from [SecResources$]";
            OleDbConnection conn = new OleDbConnection(ConStr);
            //checking that connection state is closed or not if closed the   
            //open the connection  
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //create command object  
            OleDbCommand cmd = new OleDbCommand(query, conn);
            // create a data adapter and get the data into dataadapter  
            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                //fill the Excel data to data set  
                da.Fill(ds, "Excel_tbl");


                //DataTable dt1 = ds.Tables[0].Rows
                // .Cast<DataRow>()
                // .Where(row => !row.ItemArray.All(f => f is DBNull))
                // .CopyToDataTable();

                //DataTable dt2 = ds.Tables[0].Rows
                //.Cast<DataRow>()
                //.Where(col => !col.ItemArray.All(f => f is DBNull))
                //.CopyToDataTable();

                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (i == 0)
                    {
                        int c = dr.ItemArray.Count();
                        for (int j = 0; j <= c - 1; j++)
                        {
                            dt.Columns.Add(dr.ItemArray[j].ToString());
                        }
                    }
                    else
                    {
                        if (i != 1)
                        {
                            DataRow dr1 = dt.NewRow();
                            int c = dr.ItemArray.Count();
                            for (int j = 0; j <= c - 1; j++)
                            {
                                dr1[j] = dr.ItemArray[j].ToString();
                            }
                            dt.Rows.Add(dr1);
                        }

                    }

                    i = i + 1;
                }

                conn.Close();
                da.Dispose();
                conn.Dispose();
                cmd.Dispose();


            }
        }
        catch (Exception ex)
        {
            _log.Error("Readexcel", ex);
            string msg = ex.Message.ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(msg);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
        return dt;
    }

    private bool SaveMICDetailsT2T()
    {
        bool flg = false;
        try
        {
            RecipeSecRes objRcpSecRes = new RecipeSecRes();
            if (objRecipeAccess.SaveMICDetailsT2T(Convert.ToInt32(lblRecipeId.Text.Trim()), Convert.ToInt32(lblMasterHeaderId.Text.Trim()), Session[StaticKeys.LoggedIn_User_Id].ToString()) > 0)
            {
                flg = true;
            }
            else
            {
                flg = false;
            }
        }
        catch (Exception ex)
        { _log.Error("SaveMICDetails", ex); }
        return flg;
    }

    private UpdateFlagSecRes GetFlagSecRes(string sFlag)
    {
        UpdateFlagSecRes objUpdateFlagSecRes = new UpdateFlagSecRes();
        Utility objUtil = new Utility();
        try
        {
            objUpdateFlagSecRes.Master_HeaderID = Convert.ToInt32(lblMasterHeaderId.Text.Trim());
            objUpdateFlagSecRes.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            objUpdateFlagSecRes.sScreenFlag = sFlag;
            objUpdateFlagSecRes.Section_ID = 87;
        }
        catch (Exception ex)
        { _log.Error("GetFlagMIC", ex); }
        return objUpdateFlagSecRes;
    }
    protected void btnMSProcess_Click(object sender, EventArgs e)
    {
        try { DeleteFlerecord(); } catch (Exception ex) { }
        try
        {
            DataSet dsOperation = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
            if (dsOperation.Tables[0].Rows.Count > 0)
            {
                Uploaddata();
                try
                {
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                }
                catch (Exception ex) { }

            }
            else
            {
                msg = "Atleast one resource and phase indicator must be selected.";
                lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }


        }
        catch (Exception ex)
        { _log.Error("btnMSProcess_Click", ex); }
    }
    protected void btnSRSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(), 87);
            if (dsSecResv.Tables[0].Rows.Count > 0)
            {
                if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V")
                {
                    if (SaveMICDetailsT2T())
                    {
                        string mode = Session[StaticKeys.Mode].ToString();
                        try
                        {
                            bool flag5 = false;
                            UpdateFlagSecRes objUpdateFlagSecRes1 = new UpdateFlagSecRes();
                            objUpdateFlagSecRes1 = GetFlagSecRes("S");
                            if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes1) > 0)
                            {
                                flag5 = true;
                                DeleteFlerecord();
                            }
                            else
                            { flag5 = false; }
                        }
                        catch (Exception ex) { }
                        btnSRSubmit.Visible = false;

                        FillRecipeData(mode);
                        lblMsg.Text = "Inspection Data updated successfully.";
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;
                    }
                    else
                    {
                        btnSRSubmit.Visible = true;
                        if (msg != "")
                            lblMsg.Text = msg;
                        else
                            lblMsg.Text = "Error while saving Details";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Please upload Excel file with correct format";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Please upload Excel file with correct format";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnSRSubmit_Click", ex); }
    }

    #endregion

}

