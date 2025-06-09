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
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;
public partial class Transaction_BOMRecipe_ReciepeOperation : System.Web.UI.Page
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
                        //pnlSecRes.Visible = true;
                        //End Commented on 30.05.2018
                        //ddlPlantHELP.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                        //Start Adding Nitish Rao 14.02.2018
                        //Start Commented on 30.05.2018
                        //ddlSecRes.Visible = false;
                        //End Commented on 30.05.2018
                        //End Adding  Nitish Rao 14.02.2018
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
                            //Start Commented on 30.05.2018
                            btnInsertRecord.Visible = true;
                            //trInspPhase.Visible = true;
                            //trSecRes.Visible = true;
                            //End Commented on 30.05.2018


                            //ITSM413605

                            //btnMSProcess.Visible = true;
                            ////btnSRValidation.Visible = false;
                            //btnSRSubmit.Visible = false;
                            trButtonuf.Visible = true;
                            trSecResfile.Visible = true;
                            trgrdAtt.Visible = false;

                            //grdAttachedDocs.Columns[1].Visible = true;

                            //DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(), 85);

                            //if (dsSecResv.Tables[0].Rows.Count > 0)
                            //{
                            //    if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "I " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "I")
                            //    {
                            //        //btnMSProcess.Visible = true;
                            //        ////btnSRValidation.Visible = false;
                            //        //btnSRSubmit.Visible = false;
                            //        trButtonuf.Visible = true;
                            //        trSecResfile.Visible = true;
                            //        trgrdAtt.Visible = true;
                            //    }
                            //    //else if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "U " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "U")
                            //    //{
                            //    //    btnMSProcess.Visible = false;
                            //    //    btnSRValidation.Visible = true;
                            //    //    btnSRSubmit.Visible = false;
                            //    //}
                            //    //else if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V")
                            //    //{
                            //    //    btnMSProcess.Visible = false;
                            //    //    //btnSRValidation.Visible = false;
                            //    //    btnSRSubmit.Visible = true;
                            //    //}
                            //    else if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "S " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "S")
                            //    {
                            //        //trButtonuf.Visible = false;
                            //        //btnMSProcess.Visible = false;
                            //        ////btnSRValidation.Visible = false;
                            //        //btnSRSubmit.Visible = false;

                            //        trButtonuf.Visible = false;
                            //        trSecResfile.Visible = false;
                            //        trgrdAtt.Visible = false;

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
                                btnSave.Visible = false;
                            //}
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
                            btnInsertRecord.Visible = true;
                            //trInspPhase.Visible = true;
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
                            btnInsertRecord.Visible = true;
                            //trInspPhase.Visible = true;
                            //trSecRes.Visible = true;
                            //End Commented on 30.05.2018
                        }
                        // Started date_08062021 provision to update sec res by Production, QC & BFG 
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "10" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14")
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            if (!btnPrevious.Visible && !btnNext.Visible)
                                btnSave.Visible = false;
                        }
                        //Ended date_08062021 provision to update sec res by Production, QC & BFG 
                    }

                    //ITSM413605
                    hlMSImportFormat.NavigateUrl = "~/Transaction/BOMRecipe/UploadFormat/RecipeOperation.xlsx";
                    //ITSM413605
                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }

    }

    #region Operations

    //Insert new row in Operations
    protected void btnInsertRecord_Click(object sender, EventArgs e)
    {
        try
        {

            AddBlankRowOperation();
        }
        catch (Exception ex)
        { _log.Error("btnInsertRecord_Click", ex); }
    }

    protected void btnOpSave_Click(object sender, EventArgs e)
    {
        try
        {
            lstOperationPhase.Clear();
            int cnt = 0;
            foreach (GridViewRow gvrow in GvOperation.Rows)
            {
                CheckBox chkPI = (CheckBox)GvOperation.Rows[gvrow.RowIndex].FindControl("chkPI");

                if (chkPI.Checked == true)
                {
                    UpdatePICheckedState(gvrow.RowIndex);
                    SetupValidation(((CheckBox)gvrow.FindControl("chkPI")).Checked, gvrow.RowIndex);
                    if (((DropDownList)gvrow.FindControl("ddlResource")).SelectedValue != "" && ((CheckBox)gvrow.FindControl("chkPI")).Checked)
                    {
                        //string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)gvrow.FindControl("ddlResource")).SelectedValue, ddlPlant.SelectedValue);
                        string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)gvrow.FindControl("ddlResource")).SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                        SetValidationForOperStdKey(stdValKey, gvrow.RowIndex);
                    }

                    DropDownList ddlControlKey = (DropDownList)gvrow.FindControl("ddlControlKey");

                    ////Add operation phase in ddl in inspection chara.


                    if (ddlControlKey.SelectedValue != "")
                    {
                        string strOperPhas = (gvrow.FindControl("txtOperation_Phase") as TextBox).Text;
                        if (chkPI.Checked && (ddlControlKey.SelectedValue == "PI02" || ddlControlKey.SelectedValue == "PI03"))
                        {
                            lstOperationPhase.Add(strOperPhas);
                            cnt++;
                        }
                    }

                }
                else
                {
                    SetupValidation(((CheckBox)gvrow.FindControl("chkPI")).Checked, gvrow.RowIndex);
                }




            }
            //if (cnt > 0)
            //{
            //    ddlInspPoints.SelectedValue = "Z03";
            //    ddlPartialLot.SelectedValue = "0";
            //}
            //else
            //{
            //    ddlInspPoints.SelectedValue = "";
            //    ddlPartialLot.SelectedValue = "";
            //}

            //BindOperationPhase();


        }
        catch (Exception ex)
        {
            _log.Error("btnOpSave_Click", ex);
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            //List<string> lstOperationPhase = new List<string>();
            GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
            string strOperationPhase = (row.FindControl("txtOperation_Phase") as TextBox).Text;

            //Add operation phase in ddl in inspection chara.
            foreach (GridViewRow rowop in GvOperation.Rows)
            {
                CheckBox chkSelect = (CheckBox)rowop.FindControl("chkSelect");
                string strOperPhas = (rowop.FindControl("txtOperation_Phase") as TextBox).Text;
                //Label lblDestination = (Label)rowop.FindControl("lblDestination");                    
                if (chkSelect.Checked)
                {
                    lstOperationPhase.Add(strOperPhas);
                    lstSecResPhase.Add(strOperPhas);
                    //lblDestination.Text = "01";
                }
            }
            //BindOperationPhase();
            //BindSecResOperations();

            //Add or delete MIC record on check of phase indicator.
            //if ((row.FindControl("chkSelect") as CheckBox).Checked)
            //{
            //    //Add inspection characteristics
            //    int gvrowcount = gvInspChara.Rows.Count;
            //    string positionNo = string.Empty;
            //    for (int i = 0; i < 4; i++)
            //    {
            //        AddBlankRowInspectionChara();
            //        TextBox txtOperationPhase = (TextBox)gvInspChara.Rows[gvrowcount + i].FindControl("txtOperationPhase");
            //        txtOperationPhase.Text = strOperationPhase;
            //        TextBox txtCharacteristicNo = (TextBox)gvInspChara.Rows[gvrowcount + i].FindControl("txtCharacteristicNo");

            //        if (!string.IsNullOrEmpty(positionNo))
            //        {
            //            positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
            //        }
            //        else
            //        {
            //            positionNo = String.Format("{0:0000}", 10);
            //        }
            //        txtCharacteristicNo.Text = positionNo;
            //    }

            //Add Secondary resources
            //    int gvSecCount = grdSecResources.Rows.Count;
            //    positionNo = string.Empty;
            //    for (int i = 0; i < 4; i++)
            //    {
            //        AddBlankRowSecondaryResources();
            //        TextBox txtOperationPhase = (TextBox)grdSecResources.Rows[gvrowcount + i].FindControl("txtOperationPhase");
            //        txtOperationPhase.Text = strOperationPhase;
            //        TextBox txtSecRecItem = (TextBox)grdSecResources.Rows[gvrowcount + i].FindControl("txtSecRecItem");

            //        if (!string.IsNullOrEmpty(positionNo))
            //        {
            //            positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
            //        }
            //        else
            //        {
            //            positionNo = String.Format("{0:0000}", 10);
            //        }
            //        txtSecRecItem.Text = positionNo;
            //    }
            //}
            //else
            //{
            //    int index = 0;
            //    foreach (GridViewRow gvrow in gvInspChara.Rows)
            //    {
            //        TextBox txtOperationPhase = (TextBox)gvrow.FindControl("txtOperationPhase");
            //        if (strOperationPhase == txtOperationPhase.Text)
            //        {
            //            DeleteGrdRow(gvrow.RowIndex - index);
            //            index++;
            //        }
            //    }

            //    int index1 = 0;
            //    foreach (GridViewRow gvrow in grdSecResources.Rows)
            //    {
            //        TextBox txtOperationPhase = (TextBox)gvrow.FindControl("txtOperationPhase");
            //        if (strOperationPhase == txtOperationPhase.Text)
            //        {
            //            DeleteGrdRowSec(gvrow.RowIndex - index1);
            //            index1++;
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        { _log.Error("chkSelect_CheckedChanged", ex); }
    }

    protected void chkPI_CheckedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
        //UpdatePICheckedState(row.RowIndex);
    }

    protected void GvOperation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlControlKey = (DropDownList)e.Row.FindControl("ddlControlKey");
                helperAccess.PopuplateDropDownList(ddlControlKey, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlControlKey'", "LookUp_Desc", "LookUp_Code", "");
                ddlControlKey.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[1].ToString();

                if (ddlControlKey.SelectedValue == "")
                {
                    ddlControlKey.SelectedValue = "PI01";
                }

                DropDownList ddlStdTextKey = (DropDownList)e.Row.FindControl("ddlStdTextKey");
                helperAccess.PopuplateDropDownList(ddlStdTextKey, "pr_GetDropDownListByControlNameModuleType 'R','ddlStdTextKey'", "LookUp_Desc", "LookUp_Code", "");
                ddlStdTextKey.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[2].ToString();

                DropDownList ddlIndicatorRelavancyToCosting = (DropDownList)e.Row.FindControl("ddlIndicatorRelavancyToCosting");
                helperAccess.PopuplateDropDownList(ddlIndicatorRelavancyToCosting, "pr_GetDropDownListByControlNameModuleType 'B','ddlIndicatorRelavancyToCosting'", "LookUp_Desc", "LookUp_Code");
                ddlIndicatorRelavancyToCosting.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[4].ToString();

                DropDownList ddlPlantOper = (DropDownList)e.Row.FindControl("ddlPlant");
                helperAccess.PopuplateDropDownList(ddlPlantOper, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
                ddlPlantOper.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[5].ToString();

                DropDownList ddlAct_Operation_UoM = (DropDownList)e.Row.FindControl("ddlAct_Operation_UoM");
                //helperAccess.PopuplateDropDownList(ddlAct_Operation_UoM, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
                //helperAccess.PopuplateDropDownList(ddlAct_Operation_UoM, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlAct_Operation_UoM, "proc_AutoComplete_Material_UOM '" + Session[StaticKeys.BOMRecipeMatNo].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlAct_Operation_UoM.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[6].ToString();

                DropDownList ddlChngeCUnit = (DropDownList)e.Row.FindControl("ddlChngeCUnit");
                //helperAccess.PopuplateDropDownList(ddlChngeCUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlChngeCUnit, "proc_AutoComplete_Material_UOM '" + Session[StaticKeys.BOMRecipeMatNo].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlChngeCUnit.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[7].ToString();

                DropDownList ddlChngeOUnit = (DropDownList)e.Row.FindControl("ddlChngeOUnit");
                //helperAccess.PopuplateDropDownList(ddlChngeOUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlChngeOUnit, "proc_AutoComplete_Material_UOM '" + Session[StaticKeys.BOMRecipeMatNo].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlChngeOUnit.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[8].ToString();

                DropDownList ddlResource = (DropDownList)e.Row.FindControl("ddlResource");
                helperAccess.PopuplateDropDownList(ddlResource, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlResource.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[9].ToString();

                //PROV-CCP-MM-941-23-0076 

                DropDownList ddlAltResource1 = (DropDownList)e.Row.FindControl("ddlAltResource1");
                helperAccess.PopuplateDropDownList(ddlAltResource1, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlAltResource1.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[10].ToString();

                DropDownList ddlAltResource2 = (DropDownList)e.Row.FindControl("ddlAltResource2");
                helperAccess.PopuplateDropDownList(ddlAltResource2, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlAltResource2.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[11].ToString();

                DropDownList ddlAltResource3 = (DropDownList)e.Row.FindControl("ddlAltResource3");
                helperAccess.PopuplateDropDownList(ddlAltResource3, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlAltResource3.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[12].ToString();

                DropDownList ddlAltResource4 = (DropDownList)e.Row.FindControl("ddlAltResource4");
                helperAccess.PopuplateDropDownList(ddlAltResource4, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlResource','35','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlAltResource4.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[13].ToString();


                //PROV-CCP-MM-941-23-0076 

                CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                CheckBox chkPI = e.Row.FindControl("chkPI") as CheckBox;

                HiddenField hdnSelect = e.Row.FindControl("hdnSelect") as HiddenField;
                HiddenField hdnPI = e.Row.FindControl("hdnPI") as HiddenField;

                chkSelect.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnSelect.Value);
                chkPI.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPI.Value);
                //PROV-CCP-MM-941-23-0076 
                //CheckBox chkPIKX = e.Row.FindControl("chkPIKX") as CheckBox;
                //HiddenField hdnPIKX = e.Row.FindControl("hdnPIKX") as HiddenField;
                //chkPIKX.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPIKX.Value);

                DropDownList ddlchkPIKX = (DropDownList)e.Row.FindControl("ddlchkPIKX");
                helperAccess.PopuplateDropDownList(ddlchkPIKX, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlchkPIKX'", "LookUp_Desc", "LookUp_Code", "");
                //helperAccess.PopuplateDropDownList(ddlchkPIKX, "pr_GetDropDownListByControlNameModuleType 'B','ddlchkPIKX'", "LookUp_Desc", "LookUp_Code");
                ddlchkPIKX.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[14].ToString();

                // RequiredFieldValidator reqddlchkPIKX = (RequiredFieldValidator)e.Row.FindControl("reqddlchkPIKX");
                //PROV-CCP-MM-941-23-0076 
                Label lblDestination = (Label)e.Row.FindControl("lblDestination");

                TextBox txtOperation_Phase = e.Row.FindControl("txtOperation_Phase") as TextBox;
                TextBox txtSup_Operation = e.Row.FindControl("txtSup_Operation") as TextBox;


                // if (txtOperation_Phase.Text == "0010")
                //if (txtOperation_Phase.Text == "0010" || txtOperation_Phase.Text == "0020" || txtOperation_Phase.Text == "0030"
                //    || txtOperation_Phase.Text == "0040" || txtOperation_Phase.Text == "0050" || txtOperation_Phase.Text == "0060"
                //    || txtOperation_Phase.Text == "0070" || txtOperation_Phase.Text == "0080" || txtOperation_Phase.Text == "0090"
                //    || txtOperation_Phase.Text == "0110" || txtOperation_Phase.Text == "0120" || txtOperation_Phase.Text == "0130"
                //    || txtOperation_Phase.Text == "0140" || txtOperation_Phase.Text == "0150" || txtOperation_Phase.Text == "0160"
                //    || txtOperation_Phase.Text == "0170" || txtOperation_Phase.Text == "0180" || txtOperation_Phase.Text == "0190"
                //    || txtOperation_Phase.Text == "0210" || txtOperation_Phase.Text == "0220" || txtOperation_Phase.Text == "0230"
                //    || txtOperation_Phase.Text == "0240" || txtOperation_Phase.Text == "0250" || txtOperation_Phase.Text == "0260"
                //    || txtOperation_Phase.Text == "0270" || txtOperation_Phase.Text == "0280" || txtOperation_Phase.Text == "0290"
                //    || txtOperation_Phase.Text == "0310" || txtOperation_Phase.Text == "0320" || txtOperation_Phase.Text == "0330"
                //    || txtOperation_Phase.Text == "0340" || txtOperation_Phase.Text == "0350" || txtOperation_Phase.Text == "0360"
                //    || txtOperation_Phase.Text == "0370" || txtOperation_Phase.Text == "0380" || txtOperation_Phase.Text == "0390"
                //    || txtOperation_Phase.Text == "0410" || txtOperation_Phase.Text == "0420" || txtOperation_Phase.Text == "0430"
                //    || txtOperation_Phase.Text == "0440" || txtOperation_Phase.Text == "0450" || txtOperation_Phase.Text == "0460"
                //    || txtOperation_Phase.Text == "0470" || txtOperation_Phase.Text == "0480" || txtOperation_Phase.Text == "0490"
                //    || txtOperation_Phase.Text == "0510" || txtOperation_Phase.Text == "0520" || txtOperation_Phase.Text == "0530"
                //    || txtOperation_Phase.Text == "0540" || txtOperation_Phase.Text == "0550" || txtOperation_Phase.Text == "0560"
                //    || txtOperation_Phase.Text == "0570" || txtOperation_Phase.Text == "0580" || txtOperation_Phase.Text == "0590")

                //{
                //    chkPI.Enabled = false;
                //    txtSup_Operation.Enabled = false;
                //    chkPIKX.Enabled = false;
                //}

                TextBox txtFirst_Std_Value = (TextBox)e.Row.FindControl("txtFirst_Std_Value");
                TextBox txtFirst_Std_Value_Unit = (TextBox)e.Row.FindControl("txtFirst_Std_Value_Unit");
                TextBox txtSec_Std_Value = (TextBox)e.Row.FindControl("txtSec_Std_Value");
                TextBox txtSec_Std_Value_Unit = (TextBox)e.Row.FindControl("txtSec_Std_Value_Unit");
                TextBox txtThird_Std_Value = (TextBox)e.Row.FindControl("txtThird_Std_Value");
                TextBox txtThird_Std_Value_Unit = (TextBox)e.Row.FindControl("txtThird_Std_Value_Unit");
                TextBox txtChargeQty = (TextBox)e.Row.FindControl("txtChargeQty");
                TextBox txtOperQty = (TextBox)e.Row.FindControl("txtOperQty");

                RequiredFieldValidator reqtxtChargeQty = (RequiredFieldValidator)e.Row.FindControl("reqtxtChargeQty");
                RequiredFieldValidator reqtxtOperQty = (RequiredFieldValidator)e.Row.FindControl("reqtxtOperQty");
                //DropDownList ddlResource = (DropDownList)e.Row.FindControl("ddlResource");

                //PROV-CCP-MM-941-23-0076 
                //TextBox txtStd_Value_4 = (TextBox)e.Row.FindControl("txtStd_Value_4");
                //TextBox txtStd_Value_Unit_4 = (TextBox)e.Row.FindControl("txtStd_Value_Unit_4");
                //TextBox txtStd_Value_5 = (TextBox)e.Row.FindControl("txtStd_Value_5");
                //TextBox txtStd_Value_Unit_5 = (TextBox)e.Row.FindControl("txtStd_Value_Unit_5");
                //TextBox txtStd_Value_6 = (TextBox)e.Row.FindControl("txtStd_Value_6");
                //TextBox txtStd_Value_Unit_6 = (TextBox)e.Row.FindControl("txtStd_Value_Unit_6");

                TextBox txtClass_type = (TextBox)e.Row.FindControl("txtClass_type");
                TextBox txtWC_Area = (TextBox)e.Row.FindControl("txtWC_Area");
                TextBox txtWC_Area_grp = (TextBox)e.Row.FindControl("txtWC_Area_grp");



                int RowNum = e.Row.RowIndex;
                if (RowNum % 2 == 1)
                {
                    txtClass_type.Text = "";
                    chkPI.Enabled = true;
                    txtSup_Operation.Enabled = true;
                    //chkPIKX.Enabled = true;
                    ddlchkPIKX.Enabled = true;
                    //reqddlchkPIKX.Enabled = true;

                    ddlAltResource1.Enabled = false;
                    ddlAltResource2.Enabled = false;
                    ddlAltResource3.Enabled = false;
                    ddlAltResource4.Enabled = false;
                    txtClass_type.Enabled = false;
                    txtWC_Area.Enabled = false;
                    txtWC_Area_grp.Enabled = false;

                    ddlAltResource1.Visible = false;
                    ddlAltResource2.Visible = false;
                    ddlAltResource3.Visible = false;
                    ddlAltResource4.Visible = false;
                    txtWC_Area.Visible = false;
                    txtWC_Area_grp.Visible = false;
                    txtClass_type.Visible = false;
                }
                else
                {
                    //txtClass_type.Text = "019-KX_ALT_RESOURCE";

                    chkPI.Enabled = false;
                    txtSup_Operation.Enabled = false;
                    //chkPIKX.Enabled = false;
                    //chkPIKX.Visible = false;

                    ddlchkPIKX.Enabled = false;
                    ddlchkPIKX.Visible = false;

                    //reqddlchkPIKX.Enabled = true;
                    //reqddlchkPIKX.Visible = false;
                    ddlAltResource1.Enabled = false;
                    ddlAltResource2.Enabled = false;
                    ddlAltResource3.Enabled = false;
                    ddlAltResource4.Enabled = false;
                    //ddlAltResource1.Enabled = true; 
                    //ddlAltResource2.Enabled = true;
                    //ddlAltResource3.Enabled = true;
                    //ddlAltResource4.Enabled = true;
                    txtClass_type.Enabled = false;
                    txtWC_Area.Enabled = true;
                    txtWC_Area_grp.Enabled = true;
                }

                //if (chkPIKX.Checked == true && RowNum % 2 == 1)
                if (ddlchkPIKX.SelectedValue == "YES" && RowNum % 2 == 1)
                {
                    //GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
                    //GvOperation.DataKeys[e.Row.RowIndex].Values[6].ToString();
                    TextBox txtClass_type1 = (TextBox)GvOperation.Rows[RowNum - 1].FindControl("txtClass_type");
                    txtClass_type1.Text = "019-KX_ALT_RESOURCE";
                    DropDownList ddlAltResource11 = (DropDownList)GvOperation.Rows[RowNum - 1].FindControl("ddlAltResource1");
                    ddlAltResource11.Enabled = true;
                    DropDownList ddlAltResource12 = (DropDownList)GvOperation.Rows[RowNum - 1].FindControl("ddlAltResource2");
                    ddlAltResource12.Enabled = true;
                    DropDownList ddlAltResource13 = (DropDownList)GvOperation.Rows[RowNum - 1].FindControl("ddlAltResource3");
                    ddlAltResource13.Enabled = true;
                    DropDownList ddlAltResource14 = (DropDownList)GvOperation.Rows[RowNum - 1].FindControl("ddlAltResource4");
                    ddlAltResource14.Enabled = true;
                }
                //else if (ddlchkPIKX.SelectedValue == "YES")
                //{
                //    txtClass_type.Text = "019-KX_ALT_RESOURCE";
                //}

                //PROV-CCP-MM-941-23-0076
                if (chkPI.Checked)
                {
                    //txtFirst_Std_Value.Enabled = txtSec_Std_Value.Enabled = txtThird_Std_Value.Enabled = true;
                    //txtFirst_Std_Value_Unit.Enabled = txtSec_Std_Value_Unit.Enabled = txtThird_Std_Value_Unit.Enabled 
                    if (ddlAct_Operation_UoM.SelectedValue != Session[StaticKeys.BOMRecipeBUOM].ToString())
                    {
                        txtChargeQty.Enabled = txtOperQty.Enabled = true;
                        reqtxtChargeQty.Enabled = reqtxtOperQty.Enabled = true;
                    }
                    if (txtSup_Operation.Text != "")
                        ddlResource.Enabled = false;
                    chkSelect.Enabled = true;
                }
                //SetupValidation(chkPI.Checked, e.Row.RowIndex);

            }


        }
        catch (Exception ex)
        { _log.Error("GvOperation_RowDataBound", ex); }
    }

    protected void txtBase_Quantity_TextChanged(object sender, EventArgs e)
    {
        //GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        //TextBox txtOprBaseQty = (TextBox)row.FindControl("txtBase_Quantity");
        //DropDownList ddlAct_Operation_UoM = (DropDownList)row.FindControl("ddlAct_Operation_UoM");
        //if (txtOprBaseQty.Text != txtBQty.Text)
        //{
        //    txtChngeBQty.Text = txtBQty.Text;
        //    txtChngeBQty.Enabled = false;
        //    lblRowNo.Text = row.RowIndex.ToString();
        //    if (ddlRheaderUnit.SelectedValue != "")
        //    {
        //        ddlBUnit.SelectedValue = ddlRheaderUnit.SelectedValue;
        //        ddlChngeCUnit.SelectedValue = ddlRheaderUnit.SelectedValue;
        //    }
        //    if (ddlAct_Operation_UoM.SelectedValue != "")
        //    {
        //        ddlChngeOUnit.SelectedValue = ddlAct_Operation_UoM.SelectedValue;
        //    }
        //    ddlBUnit.Enabled = false;
        //    ddlChngeCUnit.Enabled = false;
        //    ddlChngeOUnit.Enabled = false;
        //    ModalPopupExtender.Show();
        //}
    }

    protected void ddlAct_Operation_UoM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
            CheckBox chkPI = (CheckBox)row.FindControl("chkPI");

            TextBox txtOprBaseQty = (TextBox)row.FindControl("txtBase_Quantity");
            TextBox txtChargeQty = (TextBox)row.FindControl("txtChargeQty");
            TextBox txtOperQty = (TextBox)row.FindControl("txtOperQty");

            DropDownList ddlAct_Operation_UoM = (DropDownList)row.FindControl("ddlAct_Operation_UoM");
            DropDownList ddlChngeCUnit1 = (DropDownList)row.FindControl("ddlChngeCUnit");
            DropDownList ddlChngeOUnit1 = (DropDownList)row.FindControl("ddlChngeOUnit");

            RequiredFieldValidator reqtxtChargeQty = (RequiredFieldValidator)row.FindControl("reqtxtChargeQty");
            RequiredFieldValidator reqtxtOperQty = (RequiredFieldValidator)row.FindControl("reqtxtOperQty");

            if (chkPI.Checked)
            {
                if (ddlAct_Operation_UoM.SelectedValue != Session[StaticKeys.BOMRecipeBUOM].ToString())
                {
                    txtChargeQty.Enabled = true;
                    txtOperQty.Enabled = true;
                    reqtxtChargeQty.Enabled = reqtxtOperQty.Enabled = true;
                    ddlChngeCUnit1.SelectedValue = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    ddlChngeOUnit1.SelectedValue = ddlAct_Operation_UoM.SelectedValue;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ddlAct_Operation_UoM_SelectedIndexChanged", ex); }
    }

    protected void txtSup_Operation_TextChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            TextBox txtSup_Operation = (TextBox)row.FindControl("txtSup_Operation");
            TextBox txtOperation_Phase = (TextBox)row.FindControl("txtOperation_Phase");
            DropDownList ddlResource = (DropDownList)row.FindControl("ddlResource");
            CheckBox chkPI = (CheckBox)row.FindControl("chkPI");

            if (txtSup_Operation.Text != "")
            {
                List<String> operPhase = new List<String>();
                for (int i = 0; i < row.RowIndex; i++)
                {
                    TextBox txtOperation_Phase1 = (TextBox)GvOperation.Rows[i].FindControl("txtOperation_Phase");
                    operPhase.Add(txtOperation_Phase1.Text.ToString());

                }
                if (!(operPhase.Contains(txtSup_Operation.Text.ToString())))
                {
                    lblMsg.Text = "Superior operation must be the operation phase above the current operation.";
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }
                else
                {
                    chkPI.Checked = true;
                    pnlMsg.Visible = false;
                    string resource = BindResourceFromSupOper(txtSup_Operation.Text.ToString());
                    ddlResource.SelectedValue = resource;
                    ddlResource.Enabled = false;
                }
            }
            else
            {
                chkPI.Checked = false;
            }
            UpdatePICheckedState(row.RowIndex);
        }
        catch (Exception ex)
        { _log.Error("txtSup_Operation_TextChanged", ex); }
    }

    protected void ddlResource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
            DropDownList ddlResource = (DropDownList)row.FindControl("ddlResource");
            TextBox txtOperation_Phase = (TextBox)row.FindControl("txtOperation_Phase");
            foreach (GridViewRow grow in GvOperation.Rows)
            {
                TextBox txtSup_Operation = (TextBox)grow.FindControl("txtSup_Operation");
                CheckBox chkPI = (CheckBox)grow.FindControl("chkPI");
                DropDownList ddlResource1 = (DropDownList)grow.FindControl("ddlResource");
                if (txtSup_Operation.Text == txtOperation_Phase.Text && chkPI.Checked)
                {
                    ddlResource1.SelectedValue = ddlResource.SelectedValue;
                    if (ddlResource1.SelectedValue != "" && chkPI.Checked)
                    {
                        string stdValKey = objRecipeAccess.GetOprRescStdUnits(ddlResource1.SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                        if (stdValKey != "")
                        {
                            SetValidationForOperStdKey(stdValKey, grow.RowIndex);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        { _log.Error("ddlResource_SelectedIndexChanged", ex); }
    }

    protected void ddlControlKey_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
        //DropDownList ddlControlKey = (DropDownList)row.FindControl("ddlControlKey");
        //if (ddlControlKey.SelectedValue != "")
        //    AddOperationForInspection();
    }

    #endregion

    #region Operations

    private void Add_10BlankRow()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        string positionNo = string.Empty;

        int tempId = 1;
        try
        {
            dt.Columns.Add(new DataColumn("Recipe_Operation_Id"));
            dt.Columns.Add(new DataColumn("Select"));
            dt.Columns.Add(new DataColumn("Operation_Phase"));
            dt.Columns.Add(new DataColumn("Phase_Indicator"));
            dt.Columns.Add(new DataColumn("Sup_Operation"));
            dt.Columns.Add(new DataColumn("Destinatn"));
            dt.Columns.Add(new DataColumn("Resource"));
            dt.Columns.Add(new DataColumn("Control_key"));
            dt.Columns.Add(new DataColumn("StdText_Key"));
            dt.Columns.Add(new DataColumn("Description"));

            dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));

            dt.Columns.Add(new DataColumn("Base_Quantity"));
            dt.Columns.Add(new DataColumn("Act_Operation_UoM"));
            dt.Columns.Add(new DataColumn("First_Std_Value"));
            dt.Columns.Add(new DataColumn("First_Std_Value_Unit"));

            dt.Columns.Add(new DataColumn("Sec_Std_Value"));
            dt.Columns.Add(new DataColumn("Sec_Std_Value_Unit"));


            dt.Columns.Add(new DataColumn("Third_Std_Value"));
            dt.Columns.Add(new DataColumn("Third_Std_Value_Unit"));


            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("ChargeQty"));
            dt.Columns.Add(new DataColumn("OperQty"));
            dt.Columns.Add(new DataColumn("ChargeUnit"));
            dt.Columns.Add(new DataColumn("OperUnit"));

            dt.Columns.Add(new DataColumn("DeletionFlag"));
            dt.Columns.Add(new DataColumn("DFlagValue"));

            //PROV-CCP-MM-941-23-0076 
            dt.Columns.Add(new DataColumn("Std_Value_4"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_4"));
            dt.Columns.Add(new DataColumn("Std_Value_5"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_5"));
            dt.Columns.Add(new DataColumn("Std_Value_6"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_6"));

            dt.Columns.Add(new DataColumn("AltResource1"));
            dt.Columns.Add(new DataColumn("AltResource2"));
            dt.Columns.Add(new DataColumn("AltResource3"));
            dt.Columns.Add(new DataColumn("AltResource4"));

            dt.Columns.Add(new DataColumn("Class_type"));
            dt.Columns.Add(new DataColumn("WC_Area"));
            dt.Columns.Add(new DataColumn("WC_Area_grp"));
            dt.Columns.Add(new DataColumn("IsKX_Sche"));


            //PROV-CCP-MM-941-23-0076 



            for (int i = 1; i <= 6; i++)
            {
                dr = dt.NewRow();
                //dr["Recipe_Operation_Id"] = tempId;
                dr["Recipe_Operation_Id"] = 0;
                positionNo = String.Format("{0:0000}", i * 10);
                dr["Operation_Phase"] = positionNo;
                dr["DFlagValue"] = "I";
                //dr["Relevancy_To_Costing"] = "";
                dr["ChargeQty"] = "1";
                dr["OperQty"] = "1";
                dr["ChargeUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                dr["OperUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                //PROV-CCP-MM-941-23-0076 
                //dr["Class_type"] = "0019-KX_ALT_RESOURCE";
                //PROV-CCP-MM-941-23-0076 
                dt.Rows.Add(dr);

                ////PROV-CCP-MM-941-23-0076 
                //if (!string.IsNullOrEmpty(Convert.ToString(dr["Operation_Phase"])))
                //{
                //    positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Operation_Phase"]));
                //}
                //else
                //{
                //    positionNo = String.Format("{0:0000}", 10);
                //}
                //int iResult = Convert.ToInt32(positionNo) / 10;
                //CheckBox chkPI = (CheckBox)GvOperation.Rows[i].FindControl("chkPI");
                //chkPI.Enabled = (iResult % 2 == 0) ? false : true; 

                ////PROV-CCP-MM-941-23-0076 



                for (int j = 1; j <= 1; j++)
                {
                    dr = dt.NewRow();
                    //dr["Recipe_Operation_Id"] = tempId;
                    dr["Recipe_Operation_Id"] = 0;
                    positionNo = String.Format("{0:0000}", (i * 10) + 1);
                    dr["Operation_Phase"] = positionNo;
                    dr["DFlagValue"] = "I";
                    //dr["Relevancy_To_Costing"] = "X";
                    dr["ChargeQty"] = "1";
                    dr["OperQty"] = "1";
                    dr["ChargeUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    dr["OperUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    //PROV-CCP-MM-941-23-0076 
                    //dr["Class_type"] = "0019-KX_ALT_RESOURCE";
                    //PROV-CCP-MM-941-23-0076 
                    dt.Rows.Add(dr);

                    //PROV-CCP-MM-941-23-0076 
                    //if (!string.IsNullOrEmpty(Convert.ToString(dr["Operation_Phase"])))
                    //{
                    //    positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Operation_Phase"]));
                    //}
                    //else
                    //{
                    //    positionNo = String.Format("{0:0000}", 10);
                    //}
                    //int iResult = Convert.ToInt32(positionNo) / 10;
                    //CheckBox chkPI = (CheckBox)GvOperation.Rows[j].FindControl("chkPI");
                    //chkPI.Enabled = (iResult % 2 == 0) ? false : true;

                    //PROV-CCP-MM-941-23-0076 
                }
            }


            dstData.Tables.Add(dt);
            dstData.AcceptChanges();

            GvOperation.DataSource = dstData.Tables[0].DefaultView;
            if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
            {                           //GvOperation.Columns[9].Visible = false; // start from Zero
                GvOperation.Columns[14].Visible = false;
            }
            else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
            {                                                //GvOperation.Columns[10].Visible = false;
                GvOperation.Columns[15].Visible = false;
            }
            GvOperation.DataBind();
            //DisableOperationField();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("Add_10BlankRow", ex);
        }
    }

    private void AddBlankRowOperation()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        int tempId = 1;
        string positionNo = string.Empty;
        try
        {
            dt.Columns.Add(new DataColumn("Recipe_Operation_Id"));
            dt.Columns.Add(new DataColumn("Select"));
            dt.Columns.Add(new DataColumn("Operation_Phase"));
            dt.Columns.Add(new DataColumn("Phase_Indicator"));
            dt.Columns.Add(new DataColumn("Sup_Operation"));
            dt.Columns.Add(new DataColumn("Destinatn"));
            dt.Columns.Add(new DataColumn("Resource"));
            dt.Columns.Add(new DataColumn("Control_key"));
            dt.Columns.Add(new DataColumn("StdText_Key"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));

            //dt.Columns.Add(new DataColumn("Relevancy_to_Costing_Indicator"));

            dt.Columns.Add(new DataColumn("Base_Quantity"));
            dt.Columns.Add(new DataColumn("Act_Operation_UoM"));
            dt.Columns.Add(new DataColumn("First_Std_Value"));
            dt.Columns.Add(new DataColumn("First_Std_Value_Unit"));

            dt.Columns.Add(new DataColumn("Sec_Std_Value"));
            dt.Columns.Add(new DataColumn("Sec_Std_Value_Unit"));

            dt.Columns.Add(new DataColumn("Third_Std_Value"));
            dt.Columns.Add(new DataColumn("Third_Std_Value_Unit"));

            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("ChargeQty"));
            dt.Columns.Add(new DataColumn("OperQty"));
            dt.Columns.Add(new DataColumn("ChargeUnit"));
            dt.Columns.Add(new DataColumn("OperUnit"));

            dt.Columns.Add(new DataColumn("DeletionFlag"));
            dt.Columns.Add(new DataColumn("DFlagValue"));
            //PROV-CCP-MM-941-23-0076 
            dt.Columns.Add(new DataColumn("Std_Value_4"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_4"));
            dt.Columns.Add(new DataColumn("Std_Value_5"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_5"));
            dt.Columns.Add(new DataColumn("Std_Value_6"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_6"));

            dt.Columns.Add(new DataColumn("AltResource1"));
            dt.Columns.Add(new DataColumn("AltResource2"));
            dt.Columns.Add(new DataColumn("AltResource3"));
            dt.Columns.Add(new DataColumn("AltResource4"));

            dt.Columns.Add(new DataColumn("Class_type"));
            dt.Columns.Add(new DataColumn("WC_Area"));
            dt.Columns.Add(new DataColumn("WC_Area_grp"));
            dt.Columns.Add(new DataColumn("IsKX_Sche"));
            //PROV-CCP-MM-941-23-0076 
            foreach (GridViewRow row in GvOperation.Rows)
            {
                dr = dt.NewRow();
                dr["Recipe_Operation_Id"] = (row.FindControl("lblRecipe_Operation_Id") as Label).Text;
                dr["Select"] = (row.FindControl("chkSelect") as CheckBox).Checked ? "True" : "false";
                dr["Operation_Phase"] = (row.FindControl("txtOperation_Phase") as TextBox).Text;
                dr["Phase_Indicator"] = (row.FindControl("chkPI") as CheckBox).Checked ? "True" : "false";
                dr["Sup_Operation"] = (row.FindControl("txtSup_Operation") as TextBox).Text;
                dr["Destinatn"] = (row.FindControl("lblDestination") as Label).Text;
                dr["Resource"] = (row.FindControl("ddlResource") as DropDownList).SelectedValue;
                dr["Control_key"] = (row.FindControl("ddlControlkey") as DropDownList).SelectedValue;
                dr["StdText_Key"] = (row.FindControl("ddlStdTextKey") as DropDownList).SelectedValue;

                dr["Description"] = (row.FindControl("txtDescription") as TextBox).Text;
                dr["Relevancy_To_Costing"] = (row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList).SelectedValue;

                //["Relevancy_to_Costing_Indicator"] = (row.FindControl("ChckRCI") as CheckBox).Checked ? "True" : "false";

                dr["Base_Quantity"] = (row.FindControl("txtBase_Quantity") as TextBox).Text;
                dr["Act_Operation_UoM"] = (row.FindControl("ddlAct_Operation_UoM") as DropDownList).SelectedValue;
                dr["First_Std_Value"] = (row.FindControl("txtFirst_Std_Value") as TextBox).Text;
                dr["First_Std_Value_Unit"] = (row.FindControl("txtFirst_Std_Value_Unit") as TextBox).Text;

                dr["Sec_Std_Value"] = (row.FindControl("txtSec_Std_Value") as TextBox).Text;
                dr["Sec_Std_Value_Unit"] = (row.FindControl("txtSec_Std_Value_Unit") as TextBox).Text;

                dr["Third_Std_Value"] = (row.FindControl("txtThird_Std_Value") as TextBox).Text;
                dr["Third_Std_Value_Unit"] = (row.FindControl("txtThird_Std_Value_Unit") as TextBox).Text;

                dr["Plant"] = (row.FindControl("ddlPlant") as DropDownList).SelectedValue;
                dr["ChargeQty"] = (row.FindControl("txtChargeQty") as TextBox).Text;
                dr["OperQty"] = (row.FindControl("txtOperQty") as TextBox).Text;
                dr["ChargeUnit"] = (row.FindControl("ddlChngeCUnit") as DropDownList).SelectedValue;
                dr["OperUnit"] = (row.FindControl("ddlChngeOUnit") as DropDownList).SelectedValue;

                //dr["DeletionFlag"] = (row.FindControl("chkDeletionFlag") as CheckBox).Checked ? "True" : "false";
                dr["DFlagValue"] = (row.FindControl("lblDeleteFlagUDI") as Label).Text;


                //PROV-CCP-MM-941-23-0076
                dr["Std_Value_4"] = (row.FindControl("txtStd_Value_4") as TextBox).Text;
                dr["Std_Value_Unit_4"] = (row.FindControl("txtStd_Value_Unit_4") as TextBox).Text;
                dr["Std_Value_5"] = (row.FindControl("txtStd_Value_5") as TextBox).Text;
                dr["Std_Value_Unit_5"] = (row.FindControl("txtStd_Value_Unit_5") as TextBox).Text;
                dr["Std_Value_6"] = (row.FindControl("txtStd_Value_6") as TextBox).Text;
                dr["Std_Value_Unit_6"] = (row.FindControl("txtStd_Value_Unit_6") as TextBox).Text;

                dr["AltResource1"] = (row.FindControl("ddlAltResource1") as DropDownList).SelectedValue;
                dr["AltResource2"] = (row.FindControl("ddlAltResource2") as DropDownList).SelectedValue;
                dr["AltResource3"] = (row.FindControl("ddlAltResource3") as DropDownList).SelectedValue;
                dr["AltResource4"] = (row.FindControl("ddlAltResource4") as DropDownList).SelectedValue;


                dr["Class_type"] = (row.FindControl("txtClass_type") as TextBox).Text;
                dr["WC_Area"] = (row.FindControl("txtWC_Area") as TextBox).Text;
                dr["WC_Area_grp"] = (row.FindControl("txtWC_Area_grp") as TextBox).Text;
                //dr["IsKX_Sche"] = (row.FindControl("chkPIKX") as CheckBox).Checked ? "True" : "false";
                dr["IsKX_Sche"] = (row.FindControl("ddlchkPIKX") as DropDownList).SelectedValue;
                //PROV-CCP-MM-941-23-0076

                dt.Rows.Add(dr);
                if (!string.IsNullOrEmpty(Convert.ToString(dr["Operation_Phase"])))
                {
                    positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Operation_Phase"]));
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                }
                int iResult = Convert.ToInt32(positionNo) / 10;
                (row.FindControl("chkPI") as CheckBox).Enabled = (iResult % 2 == 0) ? false : true;

                //if (rdoListRecipeType.SelectedValue == "F")
                //{
                //    CheckBox chkFD = (row.FindControl("chkFD") as CheckBox);
                //    chkFD.Enabled = false;
                //    //dr["Flex_duration"] = (row.FindControl("chkFD") as CheckBox).Enabled = false;
                //    //(row.FindControl("txtChange_Number") as TextBox).Text = "10";
                //}
                tempId += 1;
            }
            if (Session["dsOperation"] != null)
            {

                DataSet dsOperation = Session["dsOperation"] as DataSet;
                int c = dsOperation.Tables[0].Rows.Count;
                if (c > 0)
                {
                    string p = dsOperation.Tables[0].Rows[c - 1]["Operation_Phase"].ToString();
                    positionNo = p;
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                }
                Session["dsOperation"] = null;
            }
            for (int i = 1; i <= 2; i++)
            {
                dr = dt.NewRow();
                //dr["Recipe_Operation_Id"] = tempId;
                dr["Recipe_Operation_Id"] = 0;
                dr["DFlagValue"] = "I";
                //dr["Relevancy_To_Costing"] = "";
                dr["ChargeQty"] = "1";
                dr["OperQty"] = "1";
                dr["ChargeUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                dr["OperUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                //PROV-CCP-MM-941-23-0076 
                //dr["Class_type"] = "0019-KX_ALT_RESOURCE";
                //PROV-CCP-MM-941-23-0076 
                if (!string.IsNullOrEmpty(positionNo))
                {
                    positionNo = String.Format("{0:0000}", ((Convert.ToInt32(positionNo) / 10) * 10) + 10);
                    dr["Operation_Phase"] = positionNo;
                }
                else
                {
                    positionNo = String.Format("{0:0000}", 10);
                    dr["Operation_Phase"] = positionNo;
                }
                dt.Rows.Add(dr);

                for (int j = 1; j <= 1; j++)
                {
                    dr = dt.NewRow();
                    //dr["Recipe_Operation_Id"] = tempId;
                    dr["Recipe_Operation_Id"] = 0;
                    positionNo = String.Format("{0:0000}", ((Convert.ToInt32(positionNo) / 10) * 10) + 1);
                    dr["Operation_Phase"] = positionNo;
                    dr["DFlagValue"] = "I";
                    //dr["Relevancy_To_Costing"] = "X";
                    dr["ChargeQty"] = "1";
                    dr["OperQty"] = "1";
                    dr["ChargeUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    dr["OperUnit"] = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    //PROV-CCP-MM-941-23-0076 
                    //dr["Class_type"] = "0019-KX_ALT_RESOURCE";
                    //PROV-CCP-MM-941-23-0076 
                    dt.Rows.Add(dr);
                }

            }

            dstData.Tables.Add(dt);
            dstData.AcceptChanges();

            GvOperation.DataSource = dstData.Tables[0].DefaultView;
            if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
            {                                             //GvOperation.Columns[9].Visible = false;
                GvOperation.Columns[14].Visible = false;
            }
            else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
            {                                                        //GvOperation.Columns[10].Visible = false;
                GvOperation.Columns[15].Visible = false;
            }
            GvOperation.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("AddBlankRowOperation", ex);
        }

        try
        {

            //Bind lost values again.
            SetBaseQuantityOperation(Session[StaticKeys.BOMRecipeBaseQty].ToString());
            //Comment By YS DT01102021
            //SetActUOMOperationAdd();
            //Comment By YS DT01102021
            foreach (GridViewRow grow in GvOperation.Rows)
            {
                SetupValidation(((CheckBox)grow.FindControl("chkPI")).Checked, grow.RowIndex);

                if (((DropDownList)grow.FindControl("ddlResource")).SelectedValue != "" && ((CheckBox)grow.FindControl("chkPI")).Checked)
                {
                    string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)grow.FindControl("ddlResource")).SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                    SetValidationForOperStdKey(stdValKey, grow.RowIndex);
                }
            }
        }
        catch (Exception ex)
        { _log.Error("AddBlankRowOperation1", ex); }
        //BindResource();
        //BindControlKey();
        //BindBOMOperationPhase();
        //DisableOperationField();
    }

    private int GetOperationDataFilledRows()
    {
        int lstRow = 0;
        try
        {

            List<int> lstOperDataFilled = new List<int>();
            for (int i = 0; i < GvOperation.Rows.Count; i++)
            {
                CheckBox chkPI = (CheckBox)GvOperation.Rows[i].FindControl("chkPI");
                if (chkPI.Checked)
                    lstOperDataFilled.Add(i);
            }
            if (lstOperDataFilled.Count() > 0)
                lstRow = lstOperDataFilled.Max();
        }
        catch (Exception ex)
        { _log.Error("GetOperationDataFilledRows", ex); }
        return lstRow;
    }

    private RecipeOperations GetOperationData(GridViewRow row)
    {
        RecipeOperations objRcpOperations = new RecipeOperations();
        Utility objUtil = new Utility();
        try
        {

            Label lblRecipe_Operation_Id = row.FindControl("lblRecipe_Operation_Id") as Label;
            TextBox txtOperation_Phase = row.FindControl("txtOperation_Phase") as TextBox;
            CheckBox chkPI = row.FindControl("chkPI") as CheckBox;
            TextBox txtSup_Operation = row.FindControl("txtSup_Operation") as TextBox;
            Label lblDestination = row.FindControl("lblDestination") as Label;
            DropDownList ddlResource = row.FindControl("ddlResource") as DropDownList;
            DropDownList ddlControl_key = row.FindControl("ddlControlkey") as DropDownList;
            DropDownList ddlStdTextKey = row.FindControl("ddlStdTextKey") as DropDownList;

            TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
            DropDownList ddlIndicatorRelavancyToCosting = row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList;

            //CheckBox ChckRCI = row.FindControl("ChckRCI") as CheckBox;
            TextBox txtBase_Quantity = row.FindControl("txtBase_Quantity") as TextBox;
            DropDownList ddlAct_Operation_UoM = row.FindControl("ddlAct_Operation_UoM") as DropDownList;

            TextBox txtFirst_Std_Value = row.FindControl("txtFirst_Std_Value") as TextBox;
            TextBox txtFirst_Std_Value_Unit = row.FindControl("txtFirst_Std_Value_Unit") as TextBox;

            TextBox txtSec_Std_Value = row.FindControl("txtSec_Std_Value") as TextBox;
            TextBox txtSec_Std_Value_Unit = row.FindControl("txtSec_Std_Value_Unit") as TextBox;

            TextBox txtThird_Std_Value = row.FindControl("txtThird_Std_Value") as TextBox;
            TextBox txtThird_Std_Value_Unit = row.FindControl("txtThird_Std_Value_Unit") as TextBox;


            DropDownList ddlPlant = row.FindControl("ddlPlant") as DropDownList;

            TextBox txtChargeQty = row.FindControl("txtChargeQty") as TextBox;
            TextBox txtOperQty = row.FindControl("txtOperQty") as TextBox;

            DropDownList ddlChngeCUnit = row.FindControl("ddlChngeCUnit") as DropDownList;
            DropDownList ddlChngeOUnit = row.FindControl("ddlChngeOUnit") as DropDownList;

            CheckBox chkDeletionFlag = row.FindControl("chkDeletionFlag") as CheckBox;
            Label lblDeleteFlagUDI = row.FindControl("lblDeleteFlagUDI") as Label;
            if (lblDeleteFlagUDI.Text == "U")
            {
                lblDeleteFlagUDI.Text = chkDeletionFlag.Checked ? "D" : "U";
            }
            //PROV-CCP-MM-941-23-0076  
            TextBox txtStd_Value_4 = row.FindControl("txtStd_Value_4") as TextBox;
            TextBox txtStd_Value_Unit_4 = row.FindControl("txtStd_Value_Unit_4") as TextBox;
            TextBox txtStd_Value_5 = row.FindControl("txtStd_Value_5") as TextBox;
            TextBox txtStd_Value_Unit_5 = row.FindControl("txtStd_Value_Unit_5") as TextBox;
            TextBox txtStd_Value_6 = row.FindControl("txtStd_Value_6") as TextBox;
            TextBox txtStd_Value_Unit_6 = row.FindControl("txtStd_Value_Unit_6") as TextBox;

            DropDownList ddlAltResource1 = row.FindControl("ddlAltResource1") as DropDownList;
            DropDownList ddlAltResource2 = row.FindControl("ddlAltResource2") as DropDownList;
            DropDownList ddlAltResource3 = row.FindControl("ddlAltResource3") as DropDownList;
            DropDownList ddlAltResource4 = row.FindControl("ddlAltResource4") as DropDownList;

            TextBox txtClass_type = row.FindControl("txtClass_type") as TextBox;
            TextBox txtWC_Area = row.FindControl("txtWC_Area") as TextBox;
            TextBox txtWC_Area_grp = row.FindControl("txtWC_Area_grp") as TextBox;
            //CheckBox chkPIKX = row.FindControl("chkPIKX") as CheckBox;
            DropDownList ddlchkPIKX = row.FindControl("ddlchkPIKX") as DropDownList;
            //PROV-CCP-MM-941-23-0076

            //Start Adding By Nitish Rao 30.05.2018
            //objRcpOperations.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            objRcpOperations.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
            objRcpOperations.Recipe_Operation_Id = Convert.ToInt32(lblRecipe_Operation_Id.Text);
            //End Adding By Nitish Rao 30.05.2018

            objRcpOperations.Operation_Phase = txtOperation_Phase.Text;
            objRcpOperations.Phase_Indicator = chkPI.Checked ? "True" : "False";
            objRcpOperations.Sup_Operation = txtSup_Operation.Text;
            objRcpOperations.Destinatn = lblDestination.Text;
            objRcpOperations.Resource = ddlResource.SelectedValue;
            objRcpOperations.Control_key = ddlControl_key.SelectedValue;
            objRcpOperations.StdText_Key = ddlStdTextKey.SelectedValue;

            objRcpOperations.Description = txtDescription.Text;
            objRcpOperations.Relevancy_To_Costing = ddlIndicatorRelavancyToCosting.SelectedValue;
            //Relevancy_to_Costing_Indicator= ChckRCI.Checked ? "True" : "False");  //""
            objRcpOperations.Base_Quantity = txtBase_Quantity.Text;
            objRcpOperations.Act_Operation_UoM = ddlAct_Operation_UoM.SelectedValue;

            objRcpOperations.First_Std_Value = txtFirst_Std_Value.Text == "" ? "0" : txtFirst_Std_Value.Text;
            objRcpOperations.First_Std_Value_Unit = txtFirst_Std_Value_Unit.Text;

            objRcpOperations.Sec_Std_Value = txtSec_Std_Value.Text == "" ? "0" : txtSec_Std_Value.Text;
            objRcpOperations.Sec_Std_Value_Unit = txtSec_Std_Value_Unit.Text;

            objRcpOperations.Third_Std_Value = txtThird_Std_Value.Text == "" ? "0" : txtThird_Std_Value.Text;
            objRcpOperations.Third_Std_Value_Unit = txtThird_Std_Value_Unit.Text;

            objRcpOperations.Plant_Id = ddlPlant.SelectedValue;
            objRcpOperations.ChargeQty = txtChargeQty.Text;
            objRcpOperations.OperQty = txtOperQty.Text;
            objRcpOperations.ChargeUnit = ddlChngeCUnit.SelectedValue;
            objRcpOperations.OperUnit = ddlChngeOUnit.SelectedValue;
            objRcpOperations.DeletionFlag = lblDeleteFlagUDI.Text;

            objRcpOperations.UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
            objRcpOperations.TodayDate = objUtil.GetDate();
            objRcpOperations.IPAddress = objUtil.GetIpAddress();
            objRcpOperations.Mode = Session[StaticKeys.Mode].ToString();

            //PROV-CCP-MM-941-23-0076  
            objRcpOperations.Std_Value_4 = txtStd_Value_4.Text == "" ? "0" : txtStd_Value_4.Text;
            objRcpOperations.Std_Value_Unit_4 = txtStd_Value_Unit_4.Text;
            objRcpOperations.Std_Value_5 = txtStd_Value_5.Text == "" ? "0" : txtStd_Value_5.Text;
            objRcpOperations.Std_Value_Unit_5 = txtStd_Value_Unit_5.Text;
            objRcpOperations.Std_Value_6 = txtStd_Value_6.Text == "" ? "0" : txtStd_Value_6.Text;
            objRcpOperations.Std_Value_Unit_6 = txtStd_Value_Unit_6.Text;


            objRcpOperations.AltResource1 = ddlAltResource1.SelectedValue;
            objRcpOperations.AltResource2 = ddlAltResource2.SelectedValue;
            objRcpOperations.AltResource3 = ddlAltResource3.SelectedValue;
            objRcpOperations.AltResource4 = ddlAltResource4.SelectedValue;
            objRcpOperations.Class_type = txtClass_type.Text;
            objRcpOperations.WC_Area = txtWC_Area.Text;
            objRcpOperations.WC_Area_grp = txtWC_Area_grp.Text;
            //objRcpOperations.IsKX_Sche = chkPIKX.Checked ? "True" : "False";
            objRcpOperations.IsKX_Sche = ddlchkPIKX.SelectedValue;
            //PROV-CCP-MM-941-23-0076 
        }
        catch (Exception ex)
        { _log.Error("GetOperationData", ex); }
        return objRcpOperations;
    }

    private void SetBaseQuantityOperation(string strBaseQty)
    {
        try
        {

            for (int i = 0; i < GvOperation.Rows.Count; i++)
            {
                TextBox txtBaseQty = (TextBox)GvOperation.Rows[i].FindControl("txtBase_Quantity");
                if (txtBaseQty.Text == "")
                    txtBaseQty.Text = strBaseQty;
            }
        }
        catch (Exception ex)
        { _log.Error("SetBaseQuantityOperation", ex); }
    }

    private void SetActUOMOperation()
    {
        try
        {

            if (Session[StaticKeys.BOMRecipeBUOM].ToString() != "")
            {
                for (int i = 0; i < GvOperation.Rows.Count; i++)
                {
                    DropDownList ddlActUOM = (DropDownList)GvOperation.Rows[i].FindControl("ddlAct_Operation_UoM");
                    try
                    {
                        helperAccess.PopuplateDropDownList(ddlActUOM, "proc_AutoComplete_Material_UOM '" + Session[StaticKeys.BOMRecipeMatNo].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                    }
                    catch (Exception ex)
                    {

                    }
                    //if (ddlActUOM.SelectedValue == "")
                    ddlActUOM.SelectedValue = Session[StaticKeys.BOMRecipeBUOM].ToString();

                    DropDownList ddlChngeCUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeCUnit");
                    helperAccess.PopuplateDropDownList(ddlChngeCUnit, "proc_AutoComplete_Material_UOM '" + Session[StaticKeys.BOMRecipeMatNo].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

                    DropDownList ddlChngeOUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeOUnit");
                    helperAccess.PopuplateDropDownList(ddlChngeOUnit, "proc_AutoComplete_Material_UOM '" + Session[StaticKeys.BOMRecipeMatNo].ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

                    if (ddlChngeCUnit.SelectedValue == "")
                        ddlChngeCUnit.SelectedValue = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    if (ddlChngeOUnit.SelectedValue == "")
                        ddlChngeOUnit.SelectedValue = ddlActUOM.SelectedValue;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("SetActUOMOperation", ex); }
    }

    private void SetActUOMOperationAdd()
    {
        try
        {

            if (Session[StaticKeys.BOMRecipeBUOM].ToString() != "")
            {
                for (int i = 0; i < GvOperation.Rows.Count; i++)
                {
                    DropDownList ddlActUOM = (DropDownList)GvOperation.Rows[i].FindControl("ddlAct_Operation_UoM");
                    if (ddlActUOM.SelectedValue == "")
                        ddlActUOM.SelectedValue = Session[StaticKeys.BOMRecipeBUOM].ToString();

                    DropDownList ddlChngeCUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeCUnit");
                    DropDownList ddlChngeOUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeOUnit");

                    if (ddlChngeCUnit.SelectedValue == "")
                        ddlChngeCUnit.SelectedValue = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    if (ddlChngeOUnit.SelectedValue == "")
                        ddlChngeOUnit.SelectedValue = ddlActUOM.SelectedValue;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("SetActUOMOperationAdd", ex); }
    }

    private void UpdatePICheckedState(int rowNo)
    {
        bool flg = false;
        try
        {

            CheckBox chkPI = (CheckBox)GvOperation.Rows[rowNo].FindControl("chkPI");
            CheckBox chkSelect = (CheckBox)GvOperation.Rows[rowNo].FindControl("chkSelect");
            Label lblDestination = (Label)GvOperation.Rows[rowNo].FindControl("lblDestination");
            DropDownList ddlIndicatorRelavancyToCosting = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlIndicatorRelavancyToCosting");

            TextBox txtFirst_Std_Value = (TextBox)GvOperation.Rows[rowNo].FindControl("txtFirst_Std_Value");
            TextBox txtFirst_Std_Value_Unit = (TextBox)GvOperation.Rows[rowNo].FindControl("txtFirst_Std_Value_Unit");
            TextBox txtSec_Std_Value = (TextBox)GvOperation.Rows[rowNo].FindControl("txtSec_Std_Value");
            TextBox txtSec_Std_Value_Unit = (TextBox)GvOperation.Rows[rowNo].FindControl("txtSec_Std_Value_Unit");
            TextBox txtThird_Std_Value = (TextBox)GvOperation.Rows[rowNo].FindControl("txtThird_Std_Value");
            TextBox txtThird_Std_Value_Unit = (TextBox)GvOperation.Rows[rowNo].FindControl("txtThird_Std_Value_Unit");
            TextBox txtSup_Operation = (TextBox)GvOperation.Rows[rowNo].FindControl("txtSup_Operation");
            DropDownList ddlResource = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlResource");

            //PROV-CCP-MM-941-23-0076 
            //DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlAltResource1");
            //DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlAltResource2");
            //DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlAltResource3");
            //DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlAltResource4");

            TextBox txtStd_Value_4 = (TextBox)GvOperation.Rows[rowNo].FindControl("txtStd_Value_4");
            TextBox txtStd_Value_Unit_4 = (TextBox)GvOperation.Rows[rowNo].FindControl("txtStd_Value_Unit_4");
            TextBox txtStd_Value_5 = (TextBox)GvOperation.Rows[rowNo].FindControl("txtStd_Value_5");
            TextBox txtStd_Value_Unit_5 = (TextBox)GvOperation.Rows[rowNo].FindControl("txtStd_Value_Unit_5");
            TextBox txtStd_Value_6 = (TextBox)GvOperation.Rows[rowNo].FindControl("txtStd_Value_6");
            TextBox txtStd_Value_Unit_6 = (TextBox)GvOperation.Rows[rowNo].FindControl("txtStd_Value_Unit_6");


            //TextBox txtClass_type = (TextBox)GvOperation.Rows[rowNo].FindControl("txtClass_type");
            //TextBox txtWC_Area = (TextBox)GvOperation.Rows[rowNo].FindControl("txtWC_Area");
            //TextBox txtWC_Area_grp = (TextBox)GvOperation.Rows[rowNo].FindControl("txtWC_Area_grp");

            //PROV-CCP-MM-941-23-0076 

            if (chkPI.Checked)
            {
                //PROV-CCP-MM-941-23-0076 
                //ddlAltResource1.Enabled = false;
                //ddlAltResource2.Enabled = false;
                //ddlAltResource3.Enabled = false;
                //ddlAltResource4.Enabled = false;
                //txtClass_type.Text = "";
                //txtWC_Area.Text = "";
                //txtWC_Area_grp.Text = "";
                //txtWC_Area.Enabled = false;
                //txtWC_Area_grp.Enabled = false;
                //PROV-CCP-MM-941-23-0076 

                lblDestination.Text = "01";
                ddlIndicatorRelavancyToCosting.SelectedValue = "X";
                if (txtSup_Operation.Text == "")
                {
                    //txtClass_type.Text = "019";
                    //ddlAltResource1.Enabled = false;
                    //ddlAltResource2.Enabled = false;
                    //ddlAltResource3.Enabled = false;
                    //ddlAltResource4.Enabled = false;

                    List<int> prevCheckd = new List<int>();
                    for (int i = 0; i <= rowNo - 1; i++)
                    {
                        if ((((CheckBox)GvOperation.Rows[i].FindControl("chkPI")).Checked) == false)
                            prevCheckd.Add(i);
                    }
                    int newRowNo = prevCheckd.Max();
                    txtSup_Operation.Text = ((TextBox)GvOperation.Rows[newRowNo].FindControl("txtOperation_Phase")).Text;
                }
                if (txtSup_Operation.Text != "")
                {//PROV-CCP-MM-941-23-0076
                    //txtClass_type.Text = "";
                    //ddlAltResource1.Enabled = true;
                    //ddlAltResource2.Enabled = true;
                    //ddlAltResource3.Enabled = true;
                    //ddlAltResource4.Enabled = true;
                    //PROV-CCP-MM-941-23-0076 
                    string resource = BindResourceFromSupOper(txtSup_Operation.Text.ToString());
                    ddlResource.SelectedValue = resource;
                }
                ddlResource.Enabled = false;
                chkSelect.Enabled = true;
                flg = true;
            }
            else
            {
                ////PROV-CCP-MM-941-23-0076
                //txtClass_type.Text = "019";
                //ddlAltResource1.Enabled = true;
                //ddlAltResource2.Enabled = true;
                //ddlAltResource3.Enabled = true;
                //ddlAltResource4.Enabled = true;
                ////PROV-CCP-MM-941-23-0076 

                lblDestination.Text = "";
                ddlIndicatorRelavancyToCosting.SelectedValue = "";
                txtSup_Operation.Text = "";
                ddlResource.SelectedValue = "";
                ddlResource.Enabled = true;
                chkSelect.Enabled = false;
                flg = false;
            }

            if (flg == false)
            {
                //PROV-CCP-MM-941-23-0076
                txtFirst_Std_Value.Text = txtFirst_Std_Value_Unit.Text = txtSec_Std_Value.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value.Text = txtThird_Std_Value_Unit.Text = txtStd_Value_4.Text = txtStd_Value_Unit_4.Text = txtStd_Value_5.Text = txtStd_Value_Unit_5.Text = txtStd_Value_6.Text = txtStd_Value_Unit_6.Text = "";
                //PROV-CCP-MM-941-23-0076
                //txtFirst_Std_Value.Text = txtFirst_Std_Value_Unit.Text = txtSec_Std_Value.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value.Text = txtThird_Std_Value_Unit.Text = "";
            }
            else
            {
                if (ddlResource.SelectedValue != "" && chkPI.Checked)
                {
                    string stdValKey = objRecipeAccess.GetOprRescStdUnits(ddlResource.SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                    if (stdValKey != "")
                    {
                        SetValidationForOperStdKey(stdValKey, rowNo);
                    }
                }
                //txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value_Unit.Text = "HR";
            }
            SetupValidation(chkPI.Checked, rowNo);
        }
        catch (Exception ex)
        { _log.Error("UpdatePICheckedState", ex); }
        //AddOperationForInspection();
        //AddOperationForSecRes();
    }

    private void SetupValidation(bool statusPI, int rowNo)
    {
        try
        {

            RequiredFieldValidator reqddlResource = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlResource");
            RequiredFieldValidator reqddlControlKey = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlControlKey");
            RequiredFieldValidator reqtxtBase_Quantity = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqtxtBase_Quantity");
            RequiredFieldValidator reqddlAct_Operation_UoM = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlAct_Operation_UoM");
            RequiredFieldValidator reqddlStdTextKey;
            RequiredFieldValidator reqtxtDescription;

            reqddlResource.Enabled = statusPI;
            reqddlControlKey.Enabled = statusPI;
            reqtxtBase_Quantity.Enabled = statusPI;
            reqddlAct_Operation_UoM.Enabled = statusPI;

            RequiredFieldValidator reqddlchkPIKX = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlchkPIKX");
            reqddlchkPIKX.Enabled = statusPI;



            if (statusPI == true)
            {

                if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
                {
                    reqtxtDescription = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqtxtDescription");
                    //reqtxtDescription.Enabled = statusPI;
                    reqtxtDescription.Enabled = false;

                }
                else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
                {
                    reqddlStdTextKey = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlStdTextKey");
                    reqddlStdTextKey.Enabled = statusPI;

                }
            }
            else
            {

                if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
                {
                    reqtxtDescription = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqtxtDescription");
                    //reqtxtDescription.Enabled = statusPI;
                    reqtxtDescription.Enabled = false;
                }
                else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
                {
                    reqddlStdTextKey = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlStdTextKey");
                    reqddlStdTextKey.Enabled = statusPI;

                }
            }

        }
        catch (Exception ex)
        { _log.Error("SetupValidation", ex); }
    }

    private string BindResourceFromSupOper(string supOperation)
    {
        string resource = "";
        try
        {

            foreach (GridViewRow row in GvOperation.Rows)
            {
                TextBox txtOperation_Phase = (TextBox)row.FindControl("txtOperation_Phase");
                if (txtOperation_Phase.Text.ToString() == supOperation)
                {
                    DropDownList ddlResource = (DropDownList)row.FindControl("ddlResource");
                    resource = ddlResource.SelectedValue.ToString();
                }
            }
        }
        catch (Exception ex)
        { _log.Error("BindResourceFromSupOper", ex); }
        return resource;
    }

    private void SetValidationForOperStdKey(string stdValKey, int grdRowNo)
    {
        try
        {

            TextBox txtFirst_Std_Value = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtFirst_Std_Value");
            TextBox txtSec_Std_Value = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtSec_Std_Value");
            TextBox txtThird_Std_Value = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtThird_Std_Value");

            TextBox txtFirst_Std_Value_Unit = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtFirst_Std_Value_Unit");
            TextBox txtSec_Std_Value_Unit = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtSec_Std_Value_Unit");
            TextBox txtThird_Std_Value_Unit = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtThird_Std_Value_Unit");

            txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value_Unit.Text = "";

            if (stdValKey == "SAP6")
            {
                txtFirst_Std_Value.Enabled = true;
                txtSec_Std_Value.Enabled = true;
                txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = "HR";
            }
            else if (stdValKey == "ZAP5")
            {
                txtFirst_Std_Value.Enabled = true;
                txtSec_Std_Value.Enabled = true;
                txtThird_Std_Value.Enabled = true;
                txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value_Unit.Text = "HR";
            }
            else
            {
                txtFirst_Std_Value.Enabled = true;
                txtFirst_Std_Value_Unit.Text = "HR";
            }

            //PROV-CCP-MM-941-23-0076 
            TextBox txtStd_Value_4 = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtStd_Value_4");
            TextBox txtStd_Value_5 = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtStd_Value_5");
            TextBox txtStd_Value_6 = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtStd_Value_6");

            TextBox txtStd_Value_Unit_4 = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtStd_Value_Unit_4");
            TextBox txtStd_Value_Unit_5 = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtStd_Value_Unit_5");
            TextBox txtStd_Value_Unit_6 = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtStd_Value_Unit_6");
            txtStd_Value_4.Enabled = true;
            txtStd_Value_5.Enabled = true;
            txtStd_Value_6.Enabled = true;
            txtStd_Value_Unit_4.Text = "HR";
            txtStd_Value_Unit_5.Text = "HR";
            txtStd_Value_Unit_6.Text = "HR";


            //DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[grdRowNo].FindControl("ddlAltResource1");
            //DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[grdRowNo].FindControl("ddlAltResource2");
            //DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[grdRowNo].FindControl("ddlAltResource3");
            //DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[grdRowNo].FindControl("ddlAltResource4");

            //TextBox txtClass_type = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtClass_type");
            //TextBox txtWC_Area = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtWC_Area");
            //TextBox txtWC_Area_grp = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtWC_Area_grp");

            //txtClass_type.Text = "";
            //txtWC_Area.Text = "";
            //txtWC_Area_grp.Text = "";
            //ddlAltResource1.SelectedValue = "";
            //ddlAltResource2.SelectedValue = "";
            //ddlAltResource3.SelectedValue = "";
            //ddlAltResource4.SelectedValue = "";
            //txtClass_type.Enabled = false;
            //txtWC_Area.Enabled = false;
            //txtWC_Area_grp.Enabled = false;
            //ddlAltResource1.Enabled = false;
            //ddlAltResource2.Enabled = false;
            //ddlAltResource3.Enabled = false;
            //ddlAltResource4.Enabled = false;

            //PROV-CCP-MM-941-23-0076 

        }
        catch (Exception ex)
        { _log.Error("SetValidationForOperStdKey", ex); }
    }

    private bool ValidateOperations()
    {
        bool flg = false;
        try
        {

            int lstRow = GetOperationDataFilledRows();
            if (lstRow > 0)
                flg = true;
        }
        catch (Exception ex)
        { _log.Error("ValidateOperations", ex); }
        return flg;
    }

    private void FillRecipeData(string mode)
    {
        try
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

                    //RecipeDetail objRecipeDetail = GetRecipeHeaderDetail(lblRecipeId.Text);
                    //if (objRecipeDetail.Recipe_HeaderDetail_Id > 0)
                    //{
                    //    lblRecipe_HeaderDetail_Id.Text = objRecipeDetail.Recipe_HeaderDetail_Id.ToString();
                    //    ddlRStatus.SelectedValue = objRecipeDetail.Status;
                    //    ddlUsages.SelectedValue = objRecipeDetail.Usage;

                    //    chkRStatus.Checked = objRecipeDetail.chkStatus == "True" ? true : false;

                    //    txtplannergp.Text = objRecipeDetail.Planner_Group;
                    //    txtResourcenw.Text = objRecipeDetail.Resource_network;
                    //    txtNWPlant.Text = objRecipeDetail.Network_Plant;
                    //    txtFrom.Text = objRecipeDetail.From_LSize;
                    //    txtTo.Text = objRecipeDetail.To_LSize;
                    //    txtBQty.Text = objRecipeDetail.Base_Quantity;
                    //    helperAccess.PopuplateDropDownList(ddlRheaderUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                    //    ddlRheaderUnit.SelectedValue = objRecipeDetail.Unit;
                    //    txtchargeqty.Text = objRecipeDetail.Charge_Quantity;
                    //    txtOperationQty.Text = objRecipeDetail.Operation_Quantity;

                    //    //start Commented on 30.05.2018
                    //    //ddlInspPoints.SelectedValue = objRecipeDetail.Insp_Points;
                    //    //ddlPartialLot.SelectedValue = objRecipeDetail.Partial_Lot;
                    //    //End Commented on 30.05.2018

                    //    Session[StaticKeys.BOMRecipeBUOM] = objRecipeDetail.Unit;
                    //    Session[StaticKeys.BOMRecipeBaseQty] = objRecipeDetail.Base_Quantity;
                    //    Session[StaticKeys.BOMRecipeFrom] = objRecipeDetail.From_LSize;
                    //    Session[StaticKeys.BOMRecipeTo] = objRecipeDetail.To_LSize;

                    //    //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
                    //    //{
                    //    //    if(chkRStatus.Checked == true)
                    //    //        ddlUsages.SelectedValue = "3";
                    //    //}
                    //}
                    //else
                    //{
                    //    lblRecipe_HeaderDetail_Id.Text = "0";
                    //    txtFrom.Text = "";
                    //    txtTo.Text = "99999999";
                    //    ddlRStatus.SelectedValue = "1";
                    //    ddlUsages.SelectedValue = "1";

                    //    //Start Commented on 30.05.2018
                    //    //ddlPartialLot.SelectedValue = "";
                    //    //ddlInspPoints.SelectedValue = "";
                    //    //End Commented on 30.05.2018
                    //}

                    //Start Commented on 30.05.2018
                    DataSet dsOperation = objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);

                    if (dsOperation.Tables[0].Rows.Count > 0)
                    {
                        Session["dsOperation"] = dsOperation;
                        GvOperation.DataSource = dsOperation;// objRecipeAccess.GetRecipeOperation(lblRecipeId.Text);
                        if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
                        {                                                //GvOperation.Columns[9].Visible = false;
                            GvOperation.Columns[14].Visible = false;
                        }
                        else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
                        {                                                       //GvOperation.Columns[10].Visible = false;
                            GvOperation.Columns[15].Visible = false;
                        }
                        GvOperation.DataBind();
                        foreach (GridViewRow grow in GvOperation.Rows)
                        {
                            SetupValidation(((CheckBox)grow.FindControl("chkPI")).Checked, grow.RowIndex);
                            if (((DropDownList)grow.FindControl("ddlResource")).SelectedValue != "" && ((CheckBox)grow.FindControl("chkPI")).Checked)
                            {
                                string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)grow.FindControl("ddlResource")).SelectedValue, Session[StaticKeys.MaterialPlantId].ToString());
                                SetValidationForOperStdKey(stdValKey, grow.RowIndex);
                            }
                        }
                    }
                    else
                    {
                        if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M"))
                            Add_10BlankRow();

                    }

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
                    ////ddlPartialLot.SelectedValue = "";
                    ////ddlInspPoints.SelectedValue = "";
                    ////End Commented on 30.05.2018

                    //Session[StaticKeys.BOMRecipeMatNo] = "";
                    //Session[StaticKeys.BOMRecipeMatDesc] = "";
                    //Session[StaticKeys.BOMRecipeBUOM] = "";
                    //Session[StaticKeys.BOMRecipeTo] = "";
                    //Session[StaticKeys.BOMRecipeFrom] = "";
                    //Session[StaticKeys.BOMRecipeBaseQty] = "";
                    //start Commented on 30.05.2018
                    //Add_10BlankRow();
                    //End Commented on 30.05.2018
                    Session["dsOperation"] = "";
                }
                //start Commented on 30.05.2018
                SetBaseQuantityOperation(Session[StaticKeys.BOMRecipeBaseQty].ToString());
                //Comment By YS DT01102021
                //SetActUOMOperation();
                //Comment By YS DT01102021
                //AddOperationForInspection();
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
        catch (Exception ex)
        { _log.Error("FillRecipeData1", ex); }
    }

    private RecipeHeader GetRecipeHeaderData()
    {
        return objRecipeAccess.GetRecipeHeaderData(lblMasterHeaderId.Text);
    }
    #endregion

    private void ConfigureHeaderControls()
    {
        try
        {

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {

                //Start Commented on 30.05.2018
                GvOperation.Enabled = false;

                //End Commented on 30.05.2018
            }
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            //DT_26-08-2020 
            else if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
            {
                GvOperation.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureHeaderControls", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {

            if (ValidateOperations())
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
            else
            {
                msg = "Atleast one resource and phase indicator must be selected.";
                lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            // Started date_08062021 provision to update sec res by Production, QC & BFG 
            //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
            //{
            //if (SaveInspCharForQA())
            //{
            //FillRecipeData(lblMode.Text);
            //lblMsg.Text = "Inspection Data updated successfully.";
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
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
            //}
            //else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14")
            //{

            //}
            //else
            //{
            if (ValidateOperations())
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
            else
            {
                msg = "Atleast one resource and phase indicator must be selected.";
                lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
        //}
        // Started date_08062021 provision to update sec res by Production, QC & BFG 
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {

            // Started date_08062021 provision to update sec res by Production, QC & BFG 
            //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
            // {
            //if (SaveInspCharForQA())
            //{
            //FillRecipeData(lblMode.Text);
            //lblMsg.Text = "Inspection Data updated successfully.";
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
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
            //}
            //else
            // {
            if (ValidateOperations())
            {


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
            else
            {
                msg = "Atleast one resource and phase indicator must be selected.";
                lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
        //}
        //Ended date_08062021 provision to update sec res by Production, QC & BFG 
    }

    private bool SaveDetail()
    {
        bool flg = false;
        try
        {

            //Start Adding  Commented on 30.05.2018
            //Get the operation filled rows
            int lstRow = GetOperationDataFilledRows();

            if (lstRow > 0)
            {
                for (int i = 0; i <= lstRow; i++)
                {
                    RecipeOperations objRcpOperations = new RecipeOperations();
                    GridViewRow row = GvOperation.Rows[i];
                    objRcpOperations = GetOperationData(row);

                    //DT28072022 Start Commented
                    //if (objRecipeAccess.SaveOperationDetails(objRcpOperations) > 0)
                    //{
                    //    flg = true;
                    //}
                    //DT28072022 End Commented

                    //DT28072022 Start Added Error log in logfile Operation_Phase is blank
                    if (objRcpOperations.Operation_Phase != "")
                    {
                        if (objRecipeAccess.SaveOperationDetails(objRcpOperations) > 0)
                        {
                            flg = true;
                        }
                    }
                    else
                    {
                        _log.Info("Operation_Phase:_ Operation_Phase is blank #Operation_Phase" + objRcpOperations.Operation_Phase + "#_#Recipe_HeaderID" + objRcpOperations.Recipe_HeaderID + "#_#MasterHeaderId" + lblMasterHeaderId.Text + "#");
                    }
                    //DT28072022 End Added
                }
                //if (flg)
                //{
                //foreach (GridViewRow row in gvInspChara.Rows)
                //{
                //    RecipeInspChara objRcpInsChar = new RecipeInspChara();
                //    objRcpInsChar = GetInspCharData(row);
                //    if (objRecipeAccess.SaveInspCharDetails(objRcpInsChar) > 0)
                //    {
                //        flg = true;
                //    }
                //    else
                //        flg = false;
                //}

                //foreach (GridViewRow row in grdSecResources.Rows)
                //{
                //    RecipeSecRes objRcpSecRes = new RecipeSecRes();
                //    objRcpSecRes = GetSecResources(row);
                //    if (objRecipeAccess.SaveSecResDetails(objRcpSecRes) > 0)
                //    {
                //        flg = true;
                //    }
                //    else
                //        flg = false;
                //}
                //}
            }
            else
            {
                msg = "Atleast one resource and phase indicator must be selected.";
                lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            //End Adding  Commented on 30.05.2018
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SaveDetail", ex);
        }

        return flg;
    }

    //-Started to Add Remark and Reason textbox and delete option. Ticket number 8200064571-- %>
    protected void GvOperation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Control ctl = e.CommandSource as Control;
        GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        try
        {
            dt.Columns.Add(new DataColumn("Select"));
            dt.Columns.Add(new DataColumn("Operation_Phase"));
            dt.Columns.Add(new DataColumn("Recipe_Operation_Id"));
            dt.Columns.Add(new DataColumn("Phase_Indicator"));
            dt.Columns.Add(new DataColumn("Sup_Operation"));
            dt.Columns.Add(new DataColumn("Destinatn"));
            dt.Columns.Add(new DataColumn("Resource"));
            dt.Columns.Add(new DataColumn("Control_key"));
            dt.Columns.Add(new DataColumn("StdText_Key"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));
            dt.Columns.Add(new DataColumn("Base_Quantity"));
            dt.Columns.Add(new DataColumn("Act_Operation_UoM"));
            dt.Columns.Add(new DataColumn("First_Std_Value"));
            dt.Columns.Add(new DataColumn("First_Std_Value_Unit"));
            dt.Columns.Add(new DataColumn("Sec_Std_Value"));
            dt.Columns.Add(new DataColumn("Sec_Std_Value_Unit"));
            dt.Columns.Add(new DataColumn("Third_Std_Value"));
            dt.Columns.Add(new DataColumn("Third_Std_Value_Unit"));
            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("ChargeQty"));
            dt.Columns.Add(new DataColumn("ChargeUnit"));
            dt.Columns.Add(new DataColumn("OperQty"));
            dt.Columns.Add(new DataColumn("OperUnit"));
            dt.Columns.Add(new DataColumn("DFlagValue"));

            //PROV-CCP-MM-941-23-0076
            dt.Columns.Add(new DataColumn("Std_Value_4"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_4"));
            dt.Columns.Add(new DataColumn("Std_Value_5"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_5"));
            dt.Columns.Add(new DataColumn("Std_Value_6"));
            dt.Columns.Add(new DataColumn("Std_Value_Unit_6"));
            //PROV-CCP-MM-941-23-0076
            dt.Columns.Add(new DataColumn("AltResource1"));
            dt.Columns.Add(new DataColumn("AltResource2"));
            dt.Columns.Add(new DataColumn("AltResource3"));
            dt.Columns.Add(new DataColumn("AltResource4"));

            dt.Columns.Add(new DataColumn("Class_type"));
            dt.Columns.Add(new DataColumn("WC_Area"));
            dt.Columns.Add(new DataColumn("WC_Area_grp"));
            dt.Columns.Add(new DataColumn("IsKX_Sche"));
            foreach (GridViewRow row in GvOperation.Rows)
            {
                dr = dt.NewRow();
                dr["Select"] = "0";
                dr["Operation_Phase"] = (row.FindControl("txtOperation_Phase") as TextBox).Text;
                dr["Recipe_Operation_Id"] = (row.FindControl("lblRecipe_Operation_Id") as Label).Text;
                dr["Phase_Indicator"] = (row.FindControl("chkPI") as CheckBox).Checked;
                dr["Sup_Operation"] = (row.FindControl("txtSup_Operation") as TextBox).Text;
                dr["Destinatn"] = (row.FindControl("lblDestination") as Label).Text;
                dr["Resource"] = (row.FindControl("ddlResource") as DropDownList).SelectedValue;
                dr["Control_key"] = (row.FindControl("ddlControlKey") as DropDownList).SelectedValue;
                dr["StdText_Key"] = (row.FindControl("ddlStdTextKey") as DropDownList).SelectedValue;
                dr["Description"] = (row.FindControl("txtDescription") as TextBox).Text;
                dr["Relevancy_To_Costing"] = dr["Phase_Indicator"].ToString() == "True" ? "X" : " ";
                dr["Base_Quantity"] = (row.FindControl("txtBase_Quantity") as TextBox).Text;
                dr["Act_Operation_UoM"] = (row.FindControl("ddlAct_Operation_UoM") as DropDownList).SelectedValue;
                dr["First_Std_Value"] = (row.FindControl("txtFirst_Std_Value") as TextBox).Text;
                dr["First_Std_Value_Unit"] = (row.FindControl("txtFirst_Std_Value_Unit") as TextBox).Text;
                dr["Sec_Std_Value"] = (row.FindControl("txtSec_Std_Value") as TextBox).Text;
                dr["Sec_Std_Value_Unit"] = (row.FindControl("txtSec_Std_Value_Unit") as TextBox).Text;
                dr["Third_Std_Value"] = (row.FindControl("txtThird_Std_Value") as TextBox).Text;
                dr["Third_Std_Value_Unit"] = (row.FindControl("txtThird_Std_Value_Unit") as TextBox).Text;
                dr["Plant"] = (row.FindControl("ddlPlant") as DropDownList).SelectedValue;
                dr["ChargeQty"] = (row.FindControl("txtChargeQty") as TextBox).Text;
                dr["ChargeUnit"] = (row.FindControl("ddlChngeCUnit") as DropDownList).SelectedValue;
                dr["OperQty"] = (row.FindControl("txtOperQty") as TextBox).Text;
                dr["OperUnit"] = (row.FindControl("ddlChngeOUnit") as DropDownList).SelectedValue;
                dr["DFlagValue"] = "U";

                //PROV-CCP-MM-941-23-0076 
                dr["Std_Value_4"] = (row.FindControl("txtStd_Value_4") as TextBox).Text;
                dr["Std_Value_Unit_4"] = (row.FindControl("txtStd_Value_Unit_4") as TextBox).Text;
                dr["Std_Value_5"] = (row.FindControl("txtStd_Value_5") as TextBox).Text;
                dr["Std_Value_Unit_5"] = (row.FindControl("txtStd_Value_Unit_5") as TextBox).Text;
                dr["Std_Value_6"] = (row.FindControl("txtStd_Value_6") as TextBox).Text;
                dr["Std_Value_Unit_6"] = (row.FindControl("txtStd_Value_Unit_6") as TextBox).Text;

                dr["AltResource1"] = (row.FindControl("ddlAltResource1") as DropDownList).SelectedValue;
                dr["AltResource2"] = (row.FindControl("ddlAltResource2") as DropDownList).SelectedValue;
                dr["AltResource3"] = (row.FindControl("ddlAltResource3") as DropDownList).SelectedValue;
                dr["AltResource4"] = (row.FindControl("ddlAltResource4") as DropDownList).SelectedValue;

                dr["Class_type"] = (row.FindControl("txtClass_type") as TextBox).Text;
                dr["WC_Area"] = (row.FindControl("txtWC_Area") as TextBox).Text;
                dr["WC_Area_grp"] = (row.FindControl("txtWC_Area_grp") as TextBox).Text;
                //dr["IsKX_Sche"] = (row.FindControl("chkPIKX") as CheckBox).Checked;
                dr["IsKX_Sche"] = (row.FindControl("ddlchkPIKX") as DropDownList).SelectedValue;
                //PROV-CCP-MM-941-23-0076 

                dt.Rows.Add(dr);

                //Start Adding By Nitish Rao 30.05.2018
                //if (objRecipeAccess.GetSecResDetail(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text) > 0)
                //{
                //    int i = objRecipeAccess.DeleteSecResData(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text);
                //}
                if (objRecipeAccess.GetRecOperationData(((Label)currentRow.FindControl("lblRecipe_Operation_Id")).Text.ToString(), lblRecipeId.Text) > 0)
                {
                    int i = objRecipeAccess.DeleteOperData(((Label)currentRow.FindControl("lblRecipe_Operation_Id")).Text.ToString(), lblRecipeId.Text);
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

            GvOperation.DataSource = dtSorted;
            GvOperation.DataBind();

            ViewState["dstRecOper"] = dstData;
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("GvOperation_RowCommand", ex);
        }
    }
    //-Ended to Add Remark and Reason textbox and delete option. Ticket number 8200064571-- %>

    protected void btnMissingResource_Click(object sender, EventArgs e)
    {
        try
        {
            idtxtResource.Visible = true;
            if (txtResource.Text.Trim() != "")
            {
                CallWebService(txtResource.Text.Trim());

            }
        }
        catch (Exception ex) { _log.Error("btnMissingResource_Click", ex); }



    }

    private void CallWebService(string smaterial)
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");

        }
        catch (Exception ex)
        {
            _log.Error("CallWebService", ex);
        }

        try
        {
            GetService.GetMaterialDetails service = new GetService.GetMaterialDetails();
            service.UseDefaultCredentials = true;

            var output = service.GetResourcesDetailsForBOMWSL(smaterial);

            if (output.msgdialog == "Done")
            {
                WriteMatChangeLog("MaterialDetailsForResource" + sdate + ".txt", output.msgdialog);
            }
            else
            {
                WriteMatChangeLog("MaterialDetailsForResource" + sdate + ".txt", output.msgdialog);
            }

        }
        catch (Exception ex)
        {
            _log.Error("CallWebService1", ex);
            WriteMatChangeLog("MaterialDetailsForResource" + sdate + ".txt", ex.ToString());
        }

    }

    public void WriteMatChangeLog(string strFileName, string strMessage)
    {
        try
        {
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ChangeMaterialLog", strFileName), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
            objStreamWriter.Close();
            objFilestream.Close();
            //return true;  
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
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
            dstData = objDoc.GetDocumentUploadDataBOM(Convert.ToInt32(lblMasterHeaderId.Text), "RecOpe");
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


    #endregion
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
                    System.IO.File.Delete(Server.MapPath("RecipeOperation") + "/" + Session[StaticKeys.RequestNo].ToString() + "/");

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
                        System.IO.File.Delete(Server.MapPath("RecipeOperation") + "/" + lblUploadedFileName.Text);
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
                StrPath = "/Transaction/BOMRecipe/RecipeOperation/" + Session[StaticKeys.RequestNo].ToString() + "/";
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
                ObjDoc.Document_Type = "RecOpe";
                ObjDoc.Document_Name = Path.GetFileName(fileUpdsecResorce.FileName);
                ObjDoc.Document_Path = StrPath + uploadedFileName;
                ObjDoc.Remarks = "";
                ObjDoc.IsActive = 1;
                ObjDoc.UserId = lblUserId.Text;
                ObjDoc.IPAddress = objUtil.GetIpAddress();

                fileUpdsecResorce.SaveAs(savePath);

                ObjDocUploadAccess.Save(ObjDoc);

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
                            //fileUpdsecResorce.SaveAs(savePath);

                            //ObjDocUploadAccess.Save(ObjDoc);

                            bool flag1 = false;
                            UpdateFlagSecRes objUpdateFlagSecRes = new UpdateFlagSecRes();
                            objUpdateFlagSecRes = GetFlagSecRes("I");
                            if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes) > 0)
                            {
                                flag1 = true;
                            }
                            else
                            { flag1 = false; }

                            string sckplantype = "";
                            try
                            { sckplantype = Convert.ToString(Session[StaticKeys.PlantType]); }
                            catch (Exception ex) { }
                            using (SqlCommand cmd = new SqlCommand("sp_In_T_Recipe_Operation_Temp"))
                            {
                                cmd.Connection = con;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                cmd.CommandType = CommandType.StoredProcedure;
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (dt.Rows[i]["Resource_Code"].ToString() != "")
                                    {
                                        string  sChargeQty = "", sChargeUnit = "", sOperQty = "", sOperUnit = "", sBase_Quantity = "";
                                        //if (i > 1)
                                        //{sClass_type = "",
                                        //    if (dt.Rows[i]["KX_Scheduling"].ToString() != "" && dt.Rows[i]["KX_Scheduling"].ToString() == "YES")
                                        //    {
                                        //        sClass_type = "019-KX_ALT_RESOURCE";
                                        //    }
                                        //    else
                                        //    {
                                        //        sClass_type = "";
                                        //    }
                                        //}


                                        if (dt.Rows[i]["Charge_Quantity"].ToString() != "")
                                        {
                                            sChargeQty = dt.Rows[i]["Charge_Quantity"].ToString().Trim();
                                        }
                                        else
                                        {
                                            sChargeQty = "1";
                                        }
                                        if (dt.Rows[i]["Charge_Unit"].ToString() != "")
                                        {
                                            sChargeUnit = dt.Rows[i]["Charge_Unit"].ToString().Trim();
                                        }
                                        else
                                        {
                                            sChargeUnit = Session[StaticKeys.BOMRecipeBUOM].ToString().Trim();
                                        }
                                        if (dt.Rows[i]["Operation_Quantity"].ToString() != "")
                                        {
                                            sOperQty = dt.Rows[i]["Operation_Quantity"].ToString().Trim();
                                        }
                                        else
                                        {
                                            sOperQty = "1";
                                        }
                                        if (dt.Rows[i]["Operation_Unit"].ToString() != "")
                                        {
                                            sOperUnit = dt.Rows[i]["Operation_Unit"].ToString().Trim();
                                        }
                                        else
                                        {
                                            sOperUnit = Session[StaticKeys.BOMRecipeBUOM].ToString().Trim();
                                        }

                                        if (dt.Rows[i]["Base_Quantity"].ToString() != "")
                                        {
                                            sBase_Quantity = dt.Rows[i]["Base_Quantity"].ToString().Trim();
                                            sBase_Quantity = sBase_Quantity.Replace(@",", "");
                                        }
                                        else
                                        {
                                            sBase_Quantity = "0";
                                        }
                                        try
                                        {
                                            cmd.Parameters.AddWithValue("@Master_HeaderID", lblMasterHeaderId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Recipe_HeaderID", lblRecipeId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Operation_Phase", dt.Rows[i]["Operation_Phase"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Phase_Indicator", dt.Rows[i]["Phase_Indicator"].ToString().Trim());// (dt.Rows[i]["Phase_Indicator"].ToString().Trim() == "X" ? "True" : "false"));
                                            cmd.Parameters.AddWithValue("@Sup_Operation", dt.Rows[i]["Superior_Operation"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Destinatn", dt.Rows[i]["Destination"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Resource", dt.Rows[i]["Resource_Code"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Control_key", dt.Rows[i]["Control_Key"].ToString().Trim());

                                            if (sckplantype != "P")
                                            {
                                                cmd.Parameters.AddWithValue("@StdText_Key", dt.Rows[i]["Std_Key_Code"].ToString().Trim());
                                                cmd.Parameters.AddWithValue("@Description", "");
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@StdText_Key", "");
                                                cmd.Parameters.AddWithValue("@Description", dt.Rows[i]["Std_Key_Code"].ToString().Trim());

                                            }
                                            cmd.Parameters.AddWithValue("@Relevancy_To_Costing", dt.Rows[i]["Phase_Indicator"].ToString().Trim());// (dt.Rows[i]["Phase_Indicator"].ToString().Trim() == "X" ? "True" : "false"));
                                            cmd.Parameters.AddWithValue("@Base_Quantity", sBase_Quantity);
                                            cmd.Parameters.AddWithValue("@Act_Operation_UoM", dt.Rows[i]["Act_Operation_UOM"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@First_Std_Value", dt.Rows[i]["Ist_Std_Duration"].ToString().Trim());// (dt.Rows[i]["Ist_Std_Duration"].ToString().Trim() == "" ? "0" : dt.Rows[i]["Ist_Std_Duration"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@First_Std_Value_Unit", dt.Rows[i]["Ist_Std_Value_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Sec_Std_Value", dt.Rows[i]["IIst_Std_Duration"].ToString().Trim());// (dt.Rows[i]["IIst_Std_Duration"].ToString().Trim() == "" ? "0" : dt.Rows[i]["IIst_Std_Duration"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@Sec_Std_Value_Unit", dt.Rows[i]["IIst_Std_Value_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Third_Std_Value", dt.Rows[i]["IIIst_Std_Duration"].ToString().Trim());// (dt.Rows[i]["IIIst_Std_Duration"].ToString().Trim() == "" ? "0" : dt.Rows[i]["IIIst_Std_Duration"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@Third_Std_Value_Unit", dt.Rows[i]["IIIst_Std_Value_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Plant", "");
                                            cmd.Parameters.AddWithValue("@ChargeQty", sChargeQty);
                                            cmd.Parameters.AddWithValue("@ChargeUnit", sChargeUnit);
                                            cmd.Parameters.AddWithValue("@OperQty", sOperQty);
                                            cmd.Parameters.AddWithValue("@OperUnit", sOperUnit);
                                            cmd.Parameters.AddWithValue("@DeletionFlag", "I");
                                            cmd.Parameters.AddWithValue("@IsActive", 1);
                                            cmd.Parameters.AddWithValue("@UserId", Session[StaticKeys.LoggedIn_User_Id].ToString());
                                            cmd.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());


                                            //PROV-CCP-MM-941-23-0076 
                                            cmd.Parameters.AddWithValue("@Std_Value_4", dt.Rows[i]["IVth_Std_Duration"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Std_Value_Unit_4", dt.Rows[i]["IVth_Std_Value_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Std_Value_5", dt.Rows[i]["Vth_Std_Duration"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Std_Value_Unit_5", dt.Rows[i]["Vth_Std_Value_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Std_Value_6", dt.Rows[i]["VIth_Std_Duration"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Std_Value_Unit_6", dt.Rows[i]["VIth_Std_Value_Unit"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@AltResource1", dt.Rows[i]["Alt_Resource_Code1"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@AltResource2", dt.Rows[i]["Alt_Resource_Code2"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@AltResource3", dt.Rows[i]["Alt_Resource_Code3"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@AltResource4", dt.Rows[i]["Alt_Resource_Code4"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Class_type", "");
                                            cmd.Parameters.AddWithValue("@WC_Area", "");
                                            cmd.Parameters.AddWithValue("@WC_Area_grp", "");
                                            cmd.Parameters.AddWithValue("@IsKX_Sche", dt.Rows[i]["KX_Scheduling"].ToString().Trim());
                                            //PROV-CCP-MM-941-23-0076 


                                            SqlDataReader sdr = cmd.ExecuteReader();
                                            sdr.Close();
                                            cmd.Parameters.RemoveAt("@Master_HeaderID");
                                            cmd.Parameters.RemoveAt("@Recipe_HeaderID");
                                            cmd.Parameters.RemoveAt("@Operation_Phase");
                                            cmd.Parameters.RemoveAt("@Phase_Indicator");
                                            cmd.Parameters.RemoveAt("@Sup_Operation");
                                            cmd.Parameters.RemoveAt("@Destinatn");
                                            cmd.Parameters.RemoveAt("@Resource");
                                            cmd.Parameters.RemoveAt("@Control_key");
                                            cmd.Parameters.RemoveAt("@StdText_Key");
                                            cmd.Parameters.RemoveAt("@Description");
                                            cmd.Parameters.RemoveAt("@Relevancy_To_Costing");
                                            cmd.Parameters.RemoveAt("@Base_Quantity");
                                            cmd.Parameters.RemoveAt("@Act_Operation_UoM");
                                            cmd.Parameters.RemoveAt("@First_Std_Value");
                                            cmd.Parameters.RemoveAt("@First_Std_Value_Unit");
                                            cmd.Parameters.RemoveAt("@Sec_Std_Value");
                                            cmd.Parameters.RemoveAt("@Sec_Std_Value_Unit");
                                            cmd.Parameters.RemoveAt("@Third_Std_Value");
                                            cmd.Parameters.RemoveAt("@Third_Std_Value_Unit");
                                            cmd.Parameters.RemoveAt("@Plant");
                                            cmd.Parameters.RemoveAt("@ChargeQty");
                                            cmd.Parameters.RemoveAt("@ChargeUnit");
                                            cmd.Parameters.RemoveAt("@OperQty");
                                            cmd.Parameters.RemoveAt("@OperUnit");
                                            cmd.Parameters.RemoveAt("@DeletionFlag");
                                            cmd.Parameters.RemoveAt("@IsActive");
                                            cmd.Parameters.RemoveAt("@UserId");
                                            cmd.Parameters.RemoveAt("@UserIp");

                                            //PROV-CCP-MM-941-23-0076 
                                            cmd.Parameters.RemoveAt("@Std_Value_4");
                                            cmd.Parameters.RemoveAt("@Std_Value_Unit_4");
                                            cmd.Parameters.RemoveAt("@Std_Value_5");
                                            cmd.Parameters.RemoveAt("@Std_Value_Unit_5");
                                            cmd.Parameters.RemoveAt("@Std_Value_6");
                                            cmd.Parameters.RemoveAt("@Std_Value_Unit_6");
                                            cmd.Parameters.RemoveAt("@AltResource1");
                                            cmd.Parameters.RemoveAt("@AltResource2");
                                            cmd.Parameters.RemoveAt("@AltResource3");
                                            cmd.Parameters.RemoveAt("@AltResource4");
                                            cmd.Parameters.RemoveAt("@Class_type");
                                            cmd.Parameters.RemoveAt("@WC_Area");
                                            cmd.Parameters.RemoveAt("@WC_Area_grp");
                                            cmd.Parameters.RemoveAt("@IsKX_Sche");
                                            //PROV-CCP-MM-941-23-0076  

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



            string query = "select * from [RecipeOperation$]";
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
                        if (i > 2)
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

    private bool SaveRecOperationDetailsT2T()
    {
        bool flg = false;
        try
        {
            RecipeSecRes objRcpSecRes = new RecipeSecRes();
            if (objRecipeAccess.SaveRecOperationDetailsT2T(Convert.ToInt32(lblRecipeId.Text.Trim()), Convert.ToInt32(lblMasterHeaderId.Text.Trim()), Session[StaticKeys.LoggedIn_User_Id].ToString()) > 0)
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
            objUpdateFlagSecRes.Section_ID = 85;
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
            DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(), 85);
            if (dsSecResv.Tables[0].Rows.Count > 0)
            {
                if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V")
                {

                    if (SaveRecOperationDetailsT2T())
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
                        lblMsg.Text = "Data updated successfully.";
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



    protected void ddlchkPIKX_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;

            DropDownList ddlchkPIKX = (DropDownList)row.FindControl("ddlchkPIKX");

            if (ddlchkPIKX.SelectedValue == "YES")
            {
                TextBox txtClass_type = (TextBox)GvOperation.Rows[row.RowIndex - 1].FindControl("txtClass_type");
                txtClass_type.Text = "019-KX_ALT_RESOURCE";

                DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource1");
                ddlAltResource1.Enabled = true;
                DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource2");
                ddlAltResource2.Enabled = true;
                DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource3");
                ddlAltResource3.Enabled = true;
                DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource4");
                ddlAltResource4.Enabled = true;
            }
            else if (ddlchkPIKX.SelectedValue == "NO")
            {
                TextBox txtClass_type = (TextBox)GvOperation.Rows[row.RowIndex - 1].FindControl("txtClass_type");
                txtClass_type.Text = "";
                DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource1");
                ddlAltResource1.Enabled = false;
                ddlAltResource1.SelectedValue = "";
                DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource2");
                ddlAltResource2.Enabled = false;
                ddlAltResource2.SelectedValue = "";
                DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource3");
                ddlAltResource3.Enabled = false;
                ddlAltResource3.SelectedValue = "";
                DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource4");
                ddlAltResource4.Enabled = false;
                ddlAltResource4.SelectedValue = "";
            }
            else
            {
                TextBox txtClass_type = (TextBox)GvOperation.Rows[row.RowIndex - 1].FindControl("txtClass_type");
                txtClass_type.Text = "";
                DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource1");
                ddlAltResource1.Enabled = false;
                ddlAltResource1.SelectedValue = "";
                DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource2");
                ddlAltResource2.Enabled = false;
                ddlAltResource2.SelectedValue = "";
                DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource3");
                ddlAltResource3.Enabled = false;
                ddlAltResource3.SelectedValue = "";
                DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource4");
                ddlAltResource4.Enabled = false;
                ddlAltResource4.SelectedValue = "";
            }

        }
        catch (Exception ex)
        { _log.Error("ddlchkPIKX_SelectedIndexChanged", ex); }
    }


    //protected void chkPIKX_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
    //        CheckBox chkPIKX = (CheckBox)row.FindControl("chkPIKX");
    //        if (chkPIKX.Checked == true)
    //        {
    //            DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource1");
    //            ddlAltResource1.Enabled = true;
    //            DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource2");
    //            ddlAltResource2.Enabled = true;
    //            DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource3");
    //            ddlAltResource3.Enabled = true;
    //            DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource4");
    //            ddlAltResource4.Enabled = true;
    //        }
    //        else
    //        {
    //            DropDownList ddlAltResource1 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource1");
    //            ddlAltResource1.Enabled = false;
    //            ddlAltResource1.SelectedValue = "";
    //            DropDownList ddlAltResource2 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource2");
    //            ddlAltResource2.Enabled = false;
    //            ddlAltResource2.SelectedValue = "";
    //            DropDownList ddlAltResource3 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource3");
    //            ddlAltResource3.Enabled = false;
    //            ddlAltResource3.SelectedValue = "";
    //            DropDownList ddlAltResource4 = (DropDownList)GvOperation.Rows[row.RowIndex - 1].FindControl("ddlAltResource4");
    //            ddlAltResource4.Enabled = false;
    //            ddlAltResource4.SelectedValue = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    { _log.Error("", ex); }
    //}
}