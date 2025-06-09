using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data.OleDb;
using System.Configuration;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;

public partial class Transaction_BOMRecipe_ReciepeSecRec : System.Web.UI.Page
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
                        //PopuplateDropDownList();
                        FillRecipeData(mode);
                        //Start Commented on 30.05.2018
                        //pnlInspChara.Visible = true;
                        pnlSecRes.Visible = true;
                        //End Commented on 30.05.2018
                        //ddlPlantHELP.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                        //Start Adding Nitish Rao 14.02.2018
                        //Start Commented on 30.05.2018
                        ddlSecRes.Visible = false;
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
  //ITSM413605

                            trButtonuf.Visible = true;
                            trSecResfile.Visible = true;
                            trgrdAtt.Visible = false;
                            //Start Commented on 30.05.2018
                            //btnInsertRecord.Visible = true;
                            //trInspPhase.Visible = true;
                            trSecRes.Visible = true;
                            //End Commented on 30.05.2018
//ddlSecRes.Visible = true;
                            //trButtonuf.Visible = true;
                            //btnMSProcess.Visible = true;
                            ////btnSRValidation.Visible = false;
                            //btnSRSubmit.Visible = false;

                            //trSecResfile.Visible = true;
                            //grdAttachedDocs.Columns[1].Visible = true;

                            //DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(),88);

                            //if (dsSecResv.Tables[0].Rows.Count > 0)
                            //{
                            //    if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "I " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "I")
                            //    {
                            //        btnMSProcess.Visible = true;
                            //        //btnSRValidation.Visible = false;
                            //        btnSRSubmit.Visible = false;
                            //    }
                            //    //else if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "U " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "U")
                            //    //{
                            //    //    btnMSProcess.Visible = false;
                            //    //    btnSRValidation.Visible = true;
                            //    //    btnSRSubmit.Visible = false;
                            //    //}
                            //    else if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V")
                            //    {
                            //        btnMSProcess.Visible = false;
                            //        //btnSRValidation.Visible = false;
                            //        btnSRSubmit.Visible = true;
                            //    }
                            //    else if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "S " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "S")
                            //    {
                            //        trButtonuf.Visible = false;
                            //        btnMSProcess.Visible = false;
                            //        //btnSRValidation.Visible = false;
                            //        btnSRSubmit.Visible = false;

                            //        trButton.Visible = true;
                            //        btnSave.Visible = !btnNext.Visible;
                            //        if (!btnPrevious.Visible && !btnNext.Visible)
                            //        {
                            //            btnSave.Visible = false;
                            //        }
                            //    }
                            //}
                            //ITSM413605
                        }
                        //BOM_NWF_SDT05072019
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            if (!btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = false;
                            }
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
                            {
                                btnSave.Visible = false;
                            }
                            //}


                        }
                        //CQA testing
                        //CQA testing
                        //BOM_8200050878 for new Module created 227,228,229
                        //manali chavan
                        //DT_26-08-2020 
                        if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            //if (Session[StaticKeys.SelectedModuleId].ToString() != "185")
                            //{
                            if (!btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = false;
                            }

                        }
                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = true;
                            btnNext.Visible = false;
                            //Start Commented on 30.05.2018
                            //trInspPhase.Visible = true;
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
                            //trInspPhase.Visible = true;
                            trSecRes.Visible = true;
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
                            //trInspPhase.Visible = true;
                            trSecRes.Visible = true;
                            //End Commented on 30.05.2018
                        }

                        // Started date_08062021 provision to update sec res by Production, QC & BFG 
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "10" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            if (!btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = false;
                            }

                        }
                        //Ended date_08062021 provision to update sec res by Production, QC & BFG 

                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            if (!btnPrevious.Visible && !btnNext.Visible)
                                btnSave.Visible = false;
                            trSecRes.Visible = true;
                        }

                        //ITSM413605
                        //if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)))
                        //{
                        //    trButton.Visible = true;
                        //    btnSave.Visible = true;
                        //    btnNext.Visible = false; 
                        //}
                        //ITSM413605


                    }
                    //ITSM413605
                    hlMSImportFormat.NavigateUrl = "~/Transaction/BOMRecipe/UploadFormat/SecResources.xlsx";
                    //ITSM413605
                }

            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }



    #region Secondary Resource

    protected void btnAddSecRes_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlSecRes.SelectedValue.ToString() != "")
                AddBlankRowSecondaryResources();
        }
        catch (Exception ex)
        { _log.Error("btnAddSecRes_Click", ex); }
    }

    protected void grdSecResources_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Start Adding  by Nitish Rao  13.02.2018
                //to Hide Operation Phase Column from Secondary Resource
                e.Row.Cells[1].Visible = false;
                // End Adding  by Nitish Rao  13.02.2018

                DropDownList ddlSecResource = (DropDownList)e.Row.FindControl("ddlSecResource");
                helperAccess.PopuplateDropDownList(ddlSecResource, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlSecResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlSecResource.SelectedValue = grdSecResources.DataKeys[e.Row.RowIndex].Values[1].ToString();
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("grdSecResources_RowDataBound", ex); }
    }

    protected void ddlSecResource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;

            DropDownList ddlSecResource = (DropDownList)row.FindControl("ddlSecResource");
            TextBox txtLabor = (TextBox)row.FindControl("txtLabor");
            TextBox txtProcess = (TextBox)row.FindControl("txtProcess");
            TextBox txtDuration = (TextBox)row.FindControl("txtDuration");
            TextBox txtUnit1 = (TextBox)row.FindControl("txtUnit1");
            TextBox txtUnit2 = (TextBox)row.FindControl("txtUnit2");
            TextBox txtUnit3 = (TextBox)row.FindControl("txtUnit3");
            TextBox txtActivityType1 = (TextBox)row.FindControl("txtActivityType1");
            TextBox txtActivityType2 = (TextBox)row.FindControl("txtActivityType2");
            TextBox txtActivityType3 = (TextBox)row.FindControl("txtActivityType3");

            if (ddlSecResource.SelectedValue != "")
            {
                DataSet dsRsrc = objRecipeAccess.GetRsrcDataByResourceName(ddlSecResource.SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                if (dsRsrc.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsRsrc.Tables[0].Rows.Count; i++)
                    {
                        if (dsRsrc.Tables[0].Rows[i]["Number"].ToString() == "0001")
                        {
                            txtUnit1.Text = dsRsrc.Tables[0].Rows[i]["ActivityUnit"].ToString();
                            txtActivityType1.Text = dsRsrc.Tables[0].Rows[i]["ActivityType"].ToString();
                        }
                        else if (dsRsrc.Tables[0].Rows[i]["Number"].ToString() == "0002")
                        {
                            txtUnit2.Text = dsRsrc.Tables[0].Rows[i]["ActivityUnit"].ToString();
                            txtActivityType2.Text = dsRsrc.Tables[0].Rows[i]["ActivityType"].ToString();
                        }
                        else if (dsRsrc.Tables[0].Rows[i]["Number"].ToString() == "0003")
                        {
                            txtUnit3.Text = dsRsrc.Tables[0].Rows[i]["ActivityUnit"].ToString();
                            txtActivityType3.Text = dsRsrc.Tables[0].Rows[i]["ActivityType"].ToString();
                        }
                    }
                }
                //txtUnit1.Text = txtUnit2.Text = txtUnit3.Text = objSecRes.Unit1;
                //txtActivityType1.Text = txtActivityType2.Text = txtActivityType3.Text = objSecRes.ActivityType1;
                SetValidationForSecRes(dsRsrc.Tables[0].Rows[0]["StdValKey"].ToString(), row.RowIndex);
            }
        }
        catch (Exception ex)
        { _log.Error("ddlSecResource_SelectedIndexChanged", ex); }
    }

    protected void grdSecResources_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //try
        //{

        Control ctl = e.CommandSource as Control;
        GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        try
        {
            dt.Columns.Add(new DataColumn("Operation_Phase"));
            dt.Columns.Add(new DataColumn("Recipe_SecResource_Id"));
            dt.Columns.Add(new DataColumn("SecResource_Item"));
            dt.Columns.Add(new DataColumn("SecResource"));
            dt.Columns.Add(new DataColumn("Duration"));
            dt.Columns.Add(new DataColumn("Unit1"));
            dt.Columns.Add(new DataColumn("ActivityType1"));
            dt.Columns.Add(new DataColumn("Process"));
            dt.Columns.Add(new DataColumn("Unit2"));
            dt.Columns.Add(new DataColumn("ActivityType2"));
            dt.Columns.Add(new DataColumn("Labor"));
            dt.Columns.Add(new DataColumn("Unit3"));
            dt.Columns.Add(new DataColumn("ActivityType3"));

            foreach (GridViewRow row in grdSecResources.Rows)
            {
                dr = dt.NewRow();
                dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
                dr["Recipe_SecResource_Id"] = (row.FindControl("lblRecipe_SecResource_Id") as Label).Text;
                dr["SecResource_Item"] = (row.FindControl("txtSecRecItem") as TextBox).Text;
                dr["SecResource"] = (row.FindControl("ddlSecResource") as DropDownList).SelectedValue;
                dr["Duration"] = (row.FindControl("txtDuration") as TextBox).Text;
                dr["Unit1"] = (row.FindControl("txtUnit1") as TextBox).Text;
                dr["ActivityType1"] = (row.FindControl("txtActivityType1") as TextBox).Text;
                dr["Process"] = (row.FindControl("txtProcess") as TextBox).Text;
                dr["Unit2"] = (row.FindControl("txtUnit2") as TextBox).Text;
                dr["ActivityType2"] = (row.FindControl("txtActivityType2") as TextBox).Text;
                dr["Labor"] = (row.FindControl("txtLabor") as TextBox).Text;
                dr["Unit3"] = (row.FindControl("txtUnit3") as TextBox).Text;
                dr["ActivityType3"] = (row.FindControl("txtActivityType3") as TextBox).Text;

                dt.Rows.Add(dr);

                //Start Adding By Nitish Rao 30.05.2018
                //if (objRecipeAccess.GetSecResDetail(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text) > 0)
                //{
                //    int i = objRecipeAccess.DeleteSecResData(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text);
                //}
                if (objRecipeAccess.GetSecResDetail(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text) > 0)
                {
                    int i = objRecipeAccess.DeleteSecResData(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text);
                }

                //End adding By nitish Rao 30.05.2018
            }
            dstData.Tables.Add(dt);
            dstData.AcceptChanges();

            dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
            dstData.AcceptChanges();

            DataView dv = new DataView(dstData.Tables[0]);
            dv.Sort = "Operation_Phase Asc";
            DataTable dtSorted = dv.ToTable();

            grdSecResources.DataSource = dtSorted;
            grdSecResources.DataBind();

            ViewState["dstSecRes"] = dstData;
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("grdSecResources_RowCommand", ex);
        }

        //}
        //catch (Exception ex)
        //{ _log.Error("grdSecResources_RowCommand1", ex); }
    }

    #endregion

    #region Secondary Resource

    private void AddBlankRowSecondaryResources()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dsSecRes = new DataSet();
        int tempId = 1;
        string positionNo = string.Empty;
        string OperpositionNo = "";

        try
        {
            dt.Columns.Add(new DataColumn("Operation_Phase"));
            dt.Columns.Add(new DataColumn("Recipe_SecResource_Id"));
            dt.Columns.Add(new DataColumn("SecResource_Item"));
            dt.Columns.Add(new DataColumn("SecResource"));
            dt.Columns.Add(new DataColumn("Duration"));
            dt.Columns.Add(new DataColumn("Unit1"));
            dt.Columns.Add(new DataColumn("ActivityType1"));
            dt.Columns.Add(new DataColumn("Process"));
            dt.Columns.Add(new DataColumn("Unit2"));
            dt.Columns.Add(new DataColumn("ActivityType2"));
            dt.Columns.Add(new DataColumn("Labor"));
            dt.Columns.Add(new DataColumn("Unit3"));
            dt.Columns.Add(new DataColumn("ActivityType3"));

            foreach (GridViewRow row in grdSecResources.Rows)
            {
                dr = dt.NewRow();
                dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
                dr["Recipe_SecResource_Id"] = (row.FindControl("lblRecipe_SecResource_Id") as Label).Text;
                dr["SecResource_Item"] = (row.FindControl("txtSecRecItem") as TextBox).Text;
                dr["SecResource"] = (row.FindControl("ddlSecResource") as DropDownList).SelectedValue;
                dr["Duration"] = (row.FindControl("txtDuration") as TextBox).Text;
                dr["Unit1"] = (row.FindControl("txtUnit1") as TextBox).Text;
                dr["ActivityType1"] = (row.FindControl("txtActivityType1") as TextBox).Text;
                dr["Process"] = (row.FindControl("txtProcess") as TextBox).Text;
                dr["Unit2"] = (row.FindControl("txtUnit2") as TextBox).Text;
                dr["ActivityType2"] = (row.FindControl("txtActivityType2") as TextBox).Text;
                dr["Labor"] = (row.FindControl("txtLabor") as TextBox).Text;
                dr["Unit3"] = (row.FindControl("txtUnit3") as TextBox).Text;
                dr["ActivityType3"] = (row.FindControl("txtActivityType3") as TextBox).Text;


                dt.Rows.Add(dr);
                tempId += 1;
                if (!string.IsNullOrEmpty(Convert.ToString(dr["SecResource_Item"])))
                {
                    positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["SecResource_Item"]));
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                }
                if (ddlSecRes.SelectedValue == (row.FindControl("txtOperationPhase") as TextBox).Text)
                {
                    OperpositionNo = positionNo;
                }
            }
            positionNo = OperpositionNo;
            for (int i = 1; i <= Convert.ToInt32(txtNoSecRes.Text); i++)
            {
                dr = dt.NewRow();
                //dr["Recipe_SecResource_Id"] = tempId;
                dr["Recipe_SecResource_Id"] = 0;
                //dr["DFlagValue"] = "I";
                if (ddlSecRes.SelectedValue != "Select")
                {
                    dr["Operation_Phase"] = ddlSecRes.SelectedItem;
                    //positionNo = OperpositionNo;
                }
                if (!string.IsNullOrEmpty(positionNo))
                {
                    positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
                    dr["SecResource_Item"] = positionNo;
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                    dr["SecResource_Item"] = positionNo;
                }

                dt.Rows.Add(dr);
            }
            dsSecRes.Tables.Add(dt);
            dsSecRes.AcceptChanges();
            grdSecResources.DataSource = dsSecRes.Tables[0].DefaultView;
            grdSecResources.DataBind();

            foreach (GridViewRow grow in grdSecResources.Rows)
            {
                if (((DropDownList)grow.FindControl("ddlSecResource")).SelectedValue != "")
                {
                    RecipeSecRes objSecRes = objRecipeAccess.GetDataByResourceName(((DropDownList)grow.FindControl("ddlSecResource")).SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                    SetValidationForSecRes(objSecRes.StdValKey, grow.RowIndex);
                }
            }

        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("AddBlankRowSecondaryResources", ex);
        }
    }

    private void BindSecResOperations()
    {
        try
        {

            lstSecResPhase.Sort();
            ddlSecRes.DataSource = lstSecResPhase.Distinct().ToList();
            ddlSecRes.DataBind();
        }
        catch (Exception ex)
        { _log.Error("BindSecResOperations", ex); }
    }

    //private void DeleteGrdRowSec(int currentRow)
    //{
    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dstData = new DataSet();
    //    try
    //    {
    //        dt.Columns.Add(new DataColumn("Operation_Phase"));
    //        dt.Columns.Add(new DataColumn("Recipe_SecResource_Id"));
    //        dt.Columns.Add(new DataColumn("SecResource_Item"));
    //        dt.Columns.Add(new DataColumn("SecResource"));
    //        dt.Columns.Add(new DataColumn("Duration"));
    //        dt.Columns.Add(new DataColumn("Unit1"));
    //        dt.Columns.Add(new DataColumn("ActivityType1"));
    //        dt.Columns.Add(new DataColumn("Process"));
    //        dt.Columns.Add(new DataColumn("Unit2"));
    //        dt.Columns.Add(new DataColumn("ActivityType2"));
    //        dt.Columns.Add(new DataColumn("Labor"));
    //        dt.Columns.Add(new DataColumn("Unit3"));
    //        dt.Columns.Add(new DataColumn("ActivityType3"));

    //        foreach (GridViewRow row in gvInspChara.Rows)
    //        {
    //            dr = dt.NewRow();
    //            dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
    //            dr["Recipe_SecResource_Id"] = (row.FindControl("lblRecipe_SecResource_Id") as Label).Text;
    //            dr["SecResource_Item"] = (row.FindControl("txtSecRecItem") as TextBox).Text;
    //            dr["SecResource"] = (row.FindControl("ddlSecResource") as DropDownList).SelectedValue;
    //            dr["Duration"] = (row.FindControl("txtDuration") as TextBox).Text;
    //            dr["Unit1"] = (row.FindControl("txtUnit1") as TextBox).Text;
    //            dr["ActivityType1"] = (row.FindControl("txtActivityType1") as TextBox).Text;
    //            dr["Process"] = (row.FindControl("txtProcess") as TextBox).Text;
    //            dr["Unit2"] = (row.FindControl("txtUnit2") as TextBox).Text;
    //            dr["ActivityType2"] = (row.FindControl("txtActivityType2") as TextBox).Text;
    //            dr["Labor"] = (row.FindControl("txtLabor") as TextBox).Text;
    //            dr["Unit3"] = (row.FindControl("txtUnit3") as TextBox).Text;
    //            dr["ActivityType3"] = (row.FindControl("txtActivityType3") as TextBox).Text;

    //            dt.Rows.Add(dr);
    //        }
    //        dstData.Tables.Add(dt);
    //        dstData.Tables[0].Rows[currentRow].Delete();
    //        dstData.AcceptChanges();

    //        grdSecResources.DataSource = dstData.Tables[0].DefaultView;
    //        grdSecResources.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private RecipeSecRes GetSecResources(GridViewRow row)
    {
        RecipeSecRes objRcpSecRes = new RecipeSecRes();
        Utility objUtil = new Utility();
        try
        {


            Label lblRecipe_SecResource_Id = row.FindControl("lblRecipe_SecResource_Id") as Label;
            TextBox txtOperationPhase = row.FindControl("txtOperationPhase") as TextBox;
            TextBox txtSecRecItem = row.FindControl("txtSecRecItem") as TextBox;
            DropDownList ddlSecResource = row.FindControl("ddlSecResource") as DropDownList;
            TextBox txtDuration = row.FindControl("txtDuration") as TextBox;
            TextBox txtUnit1 = row.FindControl("txtUnit1") as TextBox;
            TextBox txtActivityType1 = row.FindControl("txtActivityType1") as TextBox;
            TextBox txtProcess = row.FindControl("txtProcess") as TextBox;
            TextBox txtUnit2 = row.FindControl("txtUnit2") as TextBox;
            TextBox txtActivityType2 = row.FindControl("txtActivityType2") as TextBox;
            TextBox txtLabor = row.FindControl("txtLabor") as TextBox;
            TextBox txtUnit3 = row.FindControl("txtUnit3") as TextBox;
            TextBox txtActivityType3 = row.FindControl("txtActivityType3") as TextBox;

            //Start Adding By nitish rao 30.05.2018
            //objRcpSecRes.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            objRcpSecRes.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            objRcpSecRes.Recipe_SecResource_Id = Convert.ToInt32(lblRecipe_SecResource_Id.Text);
            //End Adding By Nitish Rao 30.05.2018

            objRcpSecRes.Operation_Phase = txtOperationPhase.Text;
            objRcpSecRes.SecResource_Item = txtSecRecItem.Text;
            objRcpSecRes.SecResource = ddlSecResource.SelectedValue;
            objRcpSecRes.Duration = txtDuration.Text;
            objRcpSecRes.Unit1 = txtUnit1.Text;
            objRcpSecRes.ActivityType1 = txtActivityType1.Text;
            objRcpSecRes.Process = txtProcess.Text;
            objRcpSecRes.Unit2 = txtUnit2.Text;
            objRcpSecRes.ActivityType2 = txtActivityType2.Text;
            objRcpSecRes.Labor = txtLabor.Text;
            objRcpSecRes.Unit3 = txtUnit3.Text;
            objRcpSecRes.ActivityType3 = txtActivityType3.Text;

            objRcpSecRes.UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
            objRcpSecRes.TodayDate = objUtil.GetDate();
            objRcpSecRes.IPAddress = objUtil.GetIpAddress();
            objRcpSecRes.Mode = Session[StaticKeys.Mode].ToString();
        }
        catch (Exception ex)
        { _log.Error("GetSecResources", ex); }
        return objRcpSecRes;
    }

    private void AddOperationForSecRes()
    {
        try
        {

            lstSecResPhase.Clear();
            int row = GetOperationDataFilledRows();
            string strOperPhas = "";
            if (row > 0)
                //strOperPhas = (GvOperation.Rows[row].FindControl("txtOperation_Phase") as TextBox).Text;
                strOperPhas = getLastOperation();
            lstSecResPhase.Add(strOperPhas);
            BindSecResOperations();
        }
        catch (Exception ex)
        { _log.Error("AddOperationForSecRes", ex); }
    }

    private void SetValidationForSecRes(string stdValKey, int grdRowNo)
    {
        try
        {


            RequiredFieldValidator reqtxtDuration = (RequiredFieldValidator)grdSecResources.Rows[grdRowNo].FindControl("reqtxtDuration");
            RequiredFieldValidator reqtxtProcess = (RequiredFieldValidator)grdSecResources.Rows[grdRowNo].FindControl("reqtxtProcess");
            RequiredFieldValidator reqtxtLabor = (RequiredFieldValidator)grdSecResources.Rows[grdRowNo].FindControl("reqtxtLabor");
            TextBox txtLabor = (TextBox)grdSecResources.Rows[grdRowNo].FindControl("txtLabor");
            TextBox txtProcess = (TextBox)grdSecResources.Rows[grdRowNo].FindControl("txtProcess");
            TextBox txtDuration = (TextBox)grdSecResources.Rows[grdRowNo].FindControl("txtDuration");

            //if (stdValKey == "SAP6")
            //{
            //    txtDuration.Enabled = true;
            //    reqtxtDuration.Enabled = true;
            //    txtProcess.Enabled = true;
            //    reqtxtProcess.Enabled = true;
            //}
            if (stdValKey == "ZAP5")
            {
                txtDuration.Enabled = true;
                reqtxtDuration.Enabled = true;
                txtProcess.Enabled = true;
                //reqtxtProcess.Enabled = true;
                txtLabor.Enabled = true;
                reqtxtLabor.Enabled = true;
            }
            else
            {
                txtProcess.Enabled = true;
                reqtxtProcess.Enabled = true;
            }
        }
        catch (Exception ex)
        { _log.Error("SetValidationForSecRes", ex); }
    }

    #endregion

    private int GetOperationDataFilledRows()
    {
        int lstRow = 0;
        List<int> lstOperDataFilled = new List<int>();
        //for (int i = 0; i < GvOperation.Rows.Count; i++)
        //{
        //    CheckBox chkPI = (CheckBox)GvOperation.Rows[i].FindControl("chkPI");
        //    if (chkPI.Checked)
        //        lstOperDataFilled.Add(i);
        //}
        try
        {

            DataSet dsOperation = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
            if (dsOperation.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOperation.Tables[0].Rows.Count; i++)
                {
                    lstOperDataFilled.Add(i);
                }
            }
            if (lstOperDataFilled.Count() > 0)
                lstRow = lstOperDataFilled.Max();
        }
        catch (Exception ex)
        { _log.Error("GetOperationDataFilledRows", ex); }
        return lstRow;
    }

    private void FillRecipeData(string mode)
    {
        RecipeHeader objRecipeHeader = GetRecipeHeaderData();
        try
        {
            if (objRecipeHeader.Recipe_HeaderID > 0)
            {
                lblRecipeId.Text = objRecipeHeader.Recipe_HeaderID.ToString();
                Session[StaticKeys.BOMRecipeMatNo] = objRecipeHeader.MaterialNo;
                Session[StaticKeys.BOMRecipeMatDesc] = objRecipeHeader.MaterialDesc;



                //Start Commented on 30.05.2018
                //DataSet dsOperation = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
                //if (dsOperation.Tables[0].Rows.Count > 0)
                //{
                //    GvOperation.DataSource = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
                //    if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
                //        GvOperation.Columns[9].Visible = false;
                //    else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
                //        GvOperation.Columns[10].Visible = false;
                //    GvOperation.DataBind();
                //    foreach (GridViewRow grow in GvOperation.Rows)
                //    {
                //        SetupValidation(((CheckBox)grow.FindControl("chkPI")).Checked, grow.RowIndex);
                //        if (((DropDownList)grow.FindControl("ddlResource")).SelectedValue != "" && ((CheckBox)grow.FindControl("chkPI")).Checked)
                //        {
                //            string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)grow.FindControl("ddlResource")).SelectedValue, ddlPlant.SelectedValue);
                //            SetValidationForOperStdKey(stdValKey, grow.RowIndex);
                //        }
                //    }
                //}
                //else
                //{
                //if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M"))
                //    Add_10BlankRow();

                //}

                //DataSet dsInspChar = objRecipeAccess.GetRecipeInspChara(lblRecipeId.Text);
                //if (dsInspChar.Tables[0].Rows.Count > 0)
                //{
                //    gvInspChara.DataSource = objRecipeAccess.GetRecipeInspChara(lblRecipeId.Text);
                //    gvInspChara.DataBind();

                //    foreach (GridViewRow grdRow in gvInspChara.Rows)
                //    {
                //        SetupValidationForInspChar(grdRow.RowIndex);
                //    }
                //}
                //ITSM413605
                try
                {
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                }
                catch (Exception ex) { }
                //ITSM413605
                DataSet dsSecRes = objRecipeAccess.GetRecipeSecResources(lblRecipeId.Text);
                if (dsSecRes.Tables[0].Rows.Count > 0)
                {
                    grdSecResources.DataSource = objRecipeAccess.GetRecipeSecResources(lblRecipeId.Text);
                    grdSecResources.DataBind();

                    foreach (GridViewRow gvSec in grdSecResources.Rows)
                    {
                        if (((DropDownList)gvSec.FindControl("ddlSecResource")).SelectedValue != "")
                        {
                            RecipeSecRes objSecRes = objRecipeAccess.GetDataByResourceName(((DropDownList)gvSec.FindControl("ddlSecResource")).SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                            SetValidationForSecRes(objSecRes.StdValKey, gvSec.RowIndex);
                        }
                    }
                }
                else
                {
                    //AddBlankRowSecondaryResources();  
                }

            }
            else
            {
                lblRecipeId.Text = "0";
                //lblRecipe_HeaderDetail_Id.Text = "0";
                //ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                //txtFrom.Text = "";
                //txtTo.Text = "99999999";
                //ddlRStatus.SelectedValue = "1";
                //ddlUsages.SelectedValue = "1";

                //Start Commented on 30.05.2018
                //ddlPartialLot.SelectedValue = "";
                //ddlInspPoints.SelectedValue = "";
                //End Commented on 30.05.2018

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
            //AddOperationForInspection();
            AddOperationForSecRes();
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
                //ddlRStatus.Enabled = true;
                //txtMaterialNmbr.Enabled = false;
                //txtReciepeDesc.Enabled = false;
                //chkRStatus.Enabled = false;
                //imgHelpSearchMaterial.Visible = false;
                //txtBQty.Enabled = false;

                //Start Commented on 30.05.2018
                //GvOperation.Enabled = false;
                grdSecResources.Enabled = false;
                //gvInspChara.Enabled = false;
                //ddlInspPoints.Enabled = false;
                //ddlPartialLot.Enabled = false;
                //End Commented on 30.05.2018
            }
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            //DT_26-08-2020 
            else if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
            {
                grdSecResources.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureHeaderControls", ex); }
    }
    private RecipeHeader GetRecipeHeaderData()
    {
        return objRecipeAccess.GetRecipeHeaderData(lblMasterHeaderId.Text);
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        //if (ValidateOperations())
        //{
        try
        {

            if (SaveDetail())
            {
                FillRecipeData(lblMode.Text);
                UpdateLastOperation();
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

            //Secondary resource Save Authorization to Dabhasa location for BFG department 
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14")
            {
                if (SaveSecResDetails())
                {
                    FillRecipeData(lblMode.Text);
                    lblMsg.Text = "Secondory Resource Data updated successfully.";
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
                //if (SaveInspCharForQA())
                //{
                FillRecipeData(lblMode.Text);
                lblMsg.Text = "Inspection Data updated successfully.";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                //}
                //else
                //{
                //if (msg != "")
                // lblMsg.Text = msg;
                //else
                //lblMsg.Text = "Error while saving Details";
                //pnlMsg.CssClass = "error";
                // pnlMsg.Visible = true;
                //}
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
                    UpdateLastOperation();
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

    private bool SaveSecResDetails()
    {
        bool flg = false;
        try
        {

            foreach (GridViewRow row in grdSecResources.Rows)
            {
                RecipeSecRes objRcpSecRes = new RecipeSecRes();
                objRcpSecRes = GetSecResources(row);
                if (objRecipeAccess.SaveSecResDetails(objRcpSecRes) > 0)
                {
                    flg = true;
                }
                else
                    flg = false;
            }
        }
        catch (Exception ex)
        { _log.Error("SaveSecResDetails", ex); }
        return flg;
    }

    //Start Addition By Nitish Rao Date: 14.02.2017
    // To Update Last Operation Number to All secondary resource item

    private bool UpdateLastOperation()
    {
        bool flg = false;
        try
        {
            RecipeDetail objRcpDetails = new RecipeDetail();
            objRcpDetails.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            if (objRecipeAccess.UpdateRecipeSecResouseDetails(objRcpDetails) >= 0)
            {
                flg = true;
            }

            //Get the operation filled rows
            // int lstRow = GetOperationDataFilledRows();


        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("UpdateLastOperation", ex);
        }

        return flg;
    }

    private string getLastOperation()
    {
        string LastOperation = "";
        try
        {
            RecipeDetail objRcpDetails = new RecipeDetail();
            objRcpDetails.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            LastOperation = objRecipeAccess.getRecipeSecResouseDetails(objRcpDetails);
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("getLastOperation", ex);
        }

        return LastOperation;
    }

    //End Addition By Nitish Rao Date: 14.02.2017
    private bool SaveDetail()
    {
        bool flg = false;
        try
        {
            RecipeDetail objRcpDetails = new RecipeDetail();

            int lstRow = GetOperationDataFilledRows();

            if (lstRow > 0)
            {
                if (grdSecResources.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdSecResources.Rows)
                    {
                        RecipeSecRes objRcpSecRes = new RecipeSecRes();
                        objRcpSecRes = GetSecResources(row);
                        if (objRecipeAccess.SaveSecResDetails(objRcpSecRes) > 0)
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
                //    }
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




    #region ITSM413605


    #region Document Upload

    private void BindAttachedDocuments(string MaterialId)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {
            dstData = objDoc.GetDocumentUploadDataBOM(Convert.ToInt32(lblMasterHeaderId.Text), "SecRec");
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
                    System.IO.File.Delete(Server.MapPath("SecResources") + "/" + Session[StaticKeys.RequestNo].ToString() + "/");

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
                        System.IO.File.Delete(Server.MapPath("SecResources") + "/" + lblUploadedFileName.Text);
                        objTrans.Commit();
                        pnlMsg.Visible = false;

                        BindAttachedDocuments(lblMasterHeaderId.Text);

                        bool flags1 = false;
                        UpdateFlagSecRes objUpdateFlagSecRes1 = new UpdateFlagSecRes();
                        objUpdateFlagSecRes1 = GetFlagSecRes("I");
                        if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes1) > 0)
                        {
                            flags1 = true;
                            //objRecipeAccess.DeleteAllSecResData(lblRecipeId.Text.Trim());

                            //string mode = Session[StaticKeys.Mode].ToString();
                            //FillRecipeData(mode);
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
                //StrPath = "/Transaction/BOMRecipe/SecResources/" + Session[StaticKeys.RequestNo].ToString() + "/";
                StrPath = "/Transaction/BOMRecipe/SecResources/" + Session[StaticKeys.RequestNo].ToString().Trim() + "/";
                //getting the path of the file   
                //   string path = Server.MapPath("~/SecResources/" + fileUpdsecResorce.FileName);
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
                ObjDoc.Document_Type = "SecRec";
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
                        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        //sb.Append("<script type = 'text/javascript'>");
                        //sb.Append("window.onload=function(){");
                        //sb.Append("alert('");
                        //sb.Append(msg);
                        //sb.Append("')};");
                        //sb.Append("</script>");
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
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


                            using (SqlCommand cmd = new SqlCommand("sp_In_T_Recipe_SecResource_Temp"))
                            {
                                cmd.Connection = con;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                cmd.CommandType = CommandType.StoredProcedure;
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    string sProcess = "", sDuration = "", sLabor = "";

                                    
                                    if (dt.Rows[i]["Sec_Resource_Item"].ToString() != "")
                                    {

                                        if (dt.Rows[i]["Process"].ToString() != "")
                                        {
                                            sProcess = dt.Rows[i]["Process"].ToString().Trim();
                                            sProcess = sProcess.Replace(@",", "");
                                        }
                                        else
                                        {
                                            sProcess = "0";
                                        }
                                        if (dt.Rows[i]["Duration"].ToString() != "")
                                        {
                                            sDuration = dt.Rows[i]["Duration"].ToString().Trim();
                                            sDuration = sDuration.Replace(@",", "");
                                        }
                                        else
                                        {
                                            sDuration = "0";
                                        }
                                        if (dt.Rows[i]["Labor"].ToString() != "")
                                        {
                                            sLabor = dt.Rows[i]["Labor"].ToString().Trim();
                                            sLabor = sLabor.Replace(@",", "");
                                        }
                                        else
                                        {
                                            sLabor = "0";
                                        }


                                        try
                                        {
                                            cmd.Parameters.AddWithValue("@Master_HeaderID", lblMasterHeaderId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Recipe_HeaderID", lblRecipeId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Operation_Phase", "");
                                            cmd.Parameters.AddWithValue("@SecResource_Item", dt.Rows[i]["Sec_Resource_Item"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@SecResource", dt.Rows[i]["Sec_Resource"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Duration", sDuration);
                                            cmd.Parameters.AddWithValue("@Unit1", dt.Rows[i]["Duration_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@ActivityType1", dt.Rows[i]["Duration_Activity_Type"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Process", sProcess);
                                            cmd.Parameters.AddWithValue("@Unit2", dt.Rows[i]["Process_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@ActivityType2", dt.Rows[i]["Process_Activity_Type"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Labor", sLabor);
                                            cmd.Parameters.AddWithValue("@Unit3", dt.Rows[i]["Labor_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@ActivityType3", dt.Rows[i]["Labor_Activity_Type"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@UserId", Session[StaticKeys.LoggedIn_User_Id].ToString());
                                            cmd.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());
                                            SqlDataReader sdr = cmd.ExecuteReader();
                                            sdr.Close();

                                            cmd.Parameters.RemoveAt("@Master_HeaderID");
                                            cmd.Parameters.RemoveAt("@Recipe_HeaderID");
                                            cmd.Parameters.RemoveAt("@Operation_Phase");
                                            cmd.Parameters.RemoveAt("@SecResource_Item");
                                            cmd.Parameters.RemoveAt("@SecResource");
                                            cmd.Parameters.RemoveAt("@Duration");
                                            cmd.Parameters.RemoveAt("@Unit1");
                                            cmd.Parameters.RemoveAt("@ActivityType1");
                                            cmd.Parameters.RemoveAt("@Process");
                                            cmd.Parameters.RemoveAt("@Unit2");
                                            cmd.Parameters.RemoveAt("@ActivityType2");
                                            cmd.Parameters.RemoveAt("@Labor");
                                            cmd.Parameters.RemoveAt("@Unit3");
                                            cmd.Parameters.RemoveAt("@ActivityType3");
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
                                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                            //sb.Append("<script type = 'text/javascript'>");
                                            //sb.Append("window.onload=function(){");
                                            //sb.Append("alert('");
                                            //sb.Append(msg);
                                            //sb.Append("')};");
                                            //sb.Append("</script>");
                                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                                        }
                                    }
                                }
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                //if (count == 0 || count < dt.Rows.Count)
                                if (count == 0)
                                {
                                    string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                                    lblMsg.Text = msg;
                                    pnlMsg.Visible = true;
                                    pnlMsg.CssClass = "error";
                                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    //sb.Append("<script type = 'text/javascript'>");
                                    //sb.Append("window.onload=function(){");
                                    //sb.Append("alert('");
                                    //sb.Append(msg);
                                    //sb.Append("')};");
                                    //sb.Append("</script>");
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                                }
                                else
                                {



                                    bool flag4 = false;
                                    UpdateFlagSecRes objUpdateFlagSecRes1 = new UpdateFlagSecRes();
                                    //objUpdateFlagSecRes1 = GetFlagSecRes("U");
                                    objUpdateFlagSecRes1 = GetFlagSecRes("V");
                                    if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes1) > 0)
                                    {
                                        btnMSProcess.Visible = false;
                                        //btnSRValidation.Visible = true;
                                        btnSRSubmit.Visible = true;
                                        flag4 = true;
                                    }
                                    else
                                    { flag4 = false; }
                                    string msg = "Please click on submit button for next process.";
                                    lblMsg.Text = msg;
                                    pnlMsg.Visible = true;
                                    pnlMsg.CssClass = "error";
                                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    //sb.Append("<script type = 'text/javascript'>");
                                    //sb.Append("window.onload=function(){");
                                    //sb.Append("alert('");
                                    //sb.Append(msg);
                                    //sb.Append("')};");
                                    //sb.Append("</script>");
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                                }
                            }

                        }
                        else
                        {
                            string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload."; //"No data found.";
                            lblMsg.Text = msg;
                            pnlMsg.Visible = true;
                            pnlMsg.CssClass = "error";
                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            //sb.Append("<script type = 'text/javascript'>");
                            //sb.Append("window.onload=function(){");
                            //sb.Append("alert('");
                            //sb.Append(msg);
                            //sb.Append("')};");
                            //sb.Append("</script>");
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
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
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(msg);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


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
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append("alert('");
                //sb.Append(msg);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }
        }
        else
        {
            string msg = "Please Select File";
            lblMsg.Text = msg;
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(msg);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
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



            string query = "select * from [SecResources$]";
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

    private bool SaveSecResDetailsT2T()
    {
        bool flg = false;
        try
        {
            RecipeSecRes objRcpSecRes = new RecipeSecRes();
            if (objRecipeAccess.SaveSecResDetailsT2T(Convert.ToInt32(lblRecipeId.Text.Trim()), Convert.ToInt32(lblMasterHeaderId.Text.Trim()), Session[StaticKeys.LoggedIn_User_Id].ToString()) > 0)
            {
                flg = true;
            }
            else
            {
                flg = false;
            }
        }
        catch (Exception ex)
        { _log.Error("SaveSecResDetails", ex); }
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
            objUpdateFlagSecRes.Section_ID = 88;
            // "U";
            //objRcpSecRes.UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
            //objRcpSecRes.TodayDate = objUtil.GetDate();
            //objRcpSecRes.IPAddress = objUtil.GetIpAddress(); 
        }
        catch (Exception ex)
        { _log.Error("GetFlagSecRes", ex); }
        return objUpdateFlagSecRes;
    }
    protected void btnMSProcess_Click(object sender, EventArgs e)
    {
        try { DeleteFlerecord(); } catch (Exception ex) { }
        try
        {

            if (lblRecipeId.Text != "" && lblRecipeId.Text != "0")
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
                lblMsg.Text = "Kindly submit Reciepe Header Screen first.";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }


        }
        catch (Exception ex)
        { _log.Error("btnMSProcess_Click", ex); }
    }
    protected void btnSRSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(), 88);
            if (dsSecResv.Tables[0].Rows.Count > 0)
            {
                if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V")
                {
                    if (SaveSecResDetailsT2T())
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
                        //btnSRValidation.Visible = false;

                        FillRecipeData(mode);
                        lblMsg.Text = "Secondory Resource Data updated successfully.";
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;


                    }
                    else
                    {
                        btnSRSubmit.Visible = true;
                        //btnSRValidation.Visible = false;
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
    protected void btnSRValidation_Click(object sender, EventArgs e)
    {
        try
        {
            bool flg = true;
            int i = 0;
            DataSet dsSecResv = objRecipeAccess.GetRecipeSecResourcesVal(lblMasterHeaderId.Text.Trim());

            string msg = "";

            //if (dsSecResv.Tables[0].Rows.Count > 0)
            //{
            //    helperAccess.PopuplateDropDownList(ddlSecResource, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlSecResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
            //    foreach (DataRow dr in dsSecResv.Tables[0].Rows)
            //    {
            //        i++;
            //        if (dr["SecResource"].ToString() == "" && msg != "")
            //        {
            //            msg = msg + "Resource is Mandatory";
            //            //break;
            //        }
            //        //else if (dr["SecResource"].ToString() != "")
            //        //{
            //        //    //helperAccess.PopuplateDropDownList(ddlSecResource, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlSecResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
            //        //}
            //        else if (ddlSecResource.Items.FindByText(dr["SecResource"].ToString().Trim()) == null)
            //        {
            //            msg = msg + "Invalid Resources.";
            //            //break;
            //        }
            //        //Panel pnlMsg = (Panel)grv.FindControl("pnlMsg");

            //        //Label lblMsg1 = (Label)grv.FindControl("lblMsg");

            //        if (msg == "")
            //        {
            //            //lblMsg1.Text = "OK";
            //            //pnlMsg.CssClass = "success";
            //            //pnlMsg.Visible = true;
            //        }
            //        else
            //        {
            //            flg = false;
            //            //lblMsg1.Text = msg;
            //            //pnlMsg.CssClass = "error";
            //            //pnlMsg.Visible = true;
            //        }
            //    }


            //}

            if (flg == true)
            {
                bool flag1 = false;
                UpdateFlagSecRes objUpdateFlagSecRes = new UpdateFlagSecRes();
                objUpdateFlagSecRes = GetFlagSecRes("V");
                if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes) > 0)
                {
                    flag1 = true;
                }
                else
                { flag1 = false; }
                //btnSRValidation.Visible = false;
                btnSRSubmit.Visible = true;
                lblMsg.Text = "Validation Completed";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            else
            {
                btnSRSubmit.Visible = false;
                //btnSRValidation.Visible = true;
                lblMsg.Text = msg;
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append("alert('");
                //sb.Append(msg);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

            }
            //if (i > 0)
            //btnAdd.Enabled = flg;
        }
        catch (Exception ex)
        { _log.Error("btnSRValidation_Click", ex); }
    }

    protected void lbdownloadexcel_Click(object sender, EventArgs e)
    {
        try
        {
            hlMSImportFormat.NavigateUrl = "~/Transaction/BOMRecipe/UploadFormat/SecResources.xlsx";
            ////http://localhost:62977/Transaction/BOMRecipe/UploadFormat/
            string filepath = "http://localhost:62977/Transaction/BOMRecipe/UploadFormat/SecResources.xlsx";//Server.MapPath("~/Transaction/BOMRecipe/UploadFormat/SecResources.xlsx");
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);
            Response.End();
        }
        catch (Exception ex)
        {
            _log.Error("lbdownloadexcel_Click", ex);
        }
    }

    #endregion
}