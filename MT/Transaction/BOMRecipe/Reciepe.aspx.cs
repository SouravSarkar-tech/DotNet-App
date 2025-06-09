using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.IO;
using log4net;
public partial class Transaction_Receipe_Reciepe : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    RecipeAccess objRecipeAccess = new RecipeAccess();
    BOMAccess objBOMAccess = new BOMAccess();
    List<string> lstOperationPhase = new List<string>();
    List<string> lstSecResPhase = new List<string>();
    string msg = "";

    #region Page Events

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
                    //Start Commented on 30.05.2018
                    //pnlInspChara.Visible = true;
                    //pnlSecRes.Visible = true;
                    //End Commented on 30.05.2018
                    ddlPlantHELP.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                    //Start Adding Nitish Rao 14.02.2018
                    //Start Commented on 30.05.2018
                    //ddlSecRes.Visible = false;
                    //End Commented on 30.05.2018
                    //End Adding  Nitish Rao 14.02.2018
                    if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M") && (Session[StaticKeys.RequestStatus].ToString() != "P"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                        //if (Session[StaticKeys.SelectedModuleId].ToString() != "185")
                        //{
                        if (!btnPrevious.Visible && !btnNext.Visible)
                            btnSave.Visible = false;
                        //}
                        //Start Commented on 30.05.2018
                        //btnInsertRecord.Visible = true;
                        //trInspPhase.Visible = true;
                        //trSecRes.Visible = true;
                        //End Commented on 30.05.2018
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
                    if ((txtRGroup.Text != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                        if (!btnPrevious.Visible && !btnNext.Visible)
                            btnSave.Visible = false;
                    }
                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
                    {
                        trButton.Visible = true;
                        //Start Commented on 25.10.2018
                        //btnSave.Visible = true;
                        btnSave.Visible = false;
                        //End Commented on 25.10.2018

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
                        //trSecRes.Visible = true;
                        //End Commented on 30.05.2018
                    }
                    //Secondary resource Save Authorization to Dabhasa location for BFG department 
                    if ((mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "14" && Session[StaticKeys.MaterialPlantId].ToString() == "19" && Session[StaticKeys.SelectedModuleId].ToString() == "186")
                    {
                        trButton.Visible = true;
                        //Start Commented on 25.10.2018
                        //btnSave.Visible = true;
                        btnSave.Visible = false;
                        //End Commented on 25.10.2018
                        btnNext.Visible = false;
                        //Start Commented on 30.05.2018
                        //btnInsertRecord.Visible = true;
                        //trInspPhase.Visible = true;
                        //trSecRes.Visible = true;
                        //End Commented on 30.05.2018
                    }
                }
            }
        }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
       
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "21")
        {
            //if (SaveInspCharForQA())
            //{
            //FillRecipeData(lblMode.Text);
            //lblMsg.Text = "Inspection Data updated successfully.";
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
            //if (ValidateOperations())
            //{
            if (SaveHeader())
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
                if (msg != "")
                    lblMsg.Text = msg;
                else
                    lblMsg.Text = "Error while saving Details";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //}
            //else
            //{
            //    msg = "Atleast one resource and phase indicator must be selected.";
            //    lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
            //    pnlMsg.Visible = true;
            //    pnlMsg.CssClass = "error";
            //}
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
            if (SaveHeader())
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
                if (msg != "")
                    lblMsg.Text = msg;
                else
                    lblMsg.Text = "Error while saving Details";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //}
            //else
            //{
            //    msg = "Atleast one resource and phase indicator must be selected.";
            //    lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
            //    pnlMsg.Visible = true;
            //    pnlMsg.CssClass = "error";
            //}
        }

        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
        
        //if (ValidateOperations())
        //{
        if (SaveHeader())
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
            if (msg != "")
                lblMsg.Text = msg;
            else
                lblMsg.Text = "Error while saving Details";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
            //}
            //else
            //{
            //    msg = "Atleast one resource and phase indicator must be selected.";
            //    lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
            //    pnlMsg.Visible = true;
            //    pnlMsg.CssClass = "error";
            //} 

        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    #region RecipeHeader

    //Validate and Update Material Description
    protected void txtMaterialNmbr_TextChanged(object sender, EventArgs e)
    {
        try
        {
      

        //BOM_RSeriesDT_08072020 add || matGrp == "147"
        string matGrp = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialNmbr.Text);
        if (matGrp == "162" || matGrp == "164" || matGrp == "139" || matGrp == "144" || matGrp == "171" || matGrp == "147")
        {
            DataSet dstData = BOMAccess.ReadMaterialHelp(txtMaterialNmbr.Text.ToString(), "", ddlPlant.SelectedValue.ToString(), "", "");
            if (dstData.Tables[0].Rows.Count > 0)
            {
                txtMaterialDescription.Text = txtReciepeDesc.Text = dstData.Tables[0].Rows[0]["Material_Desc"].ToString();
                helperAccess.PopuplateDropDownList(ddlRheaderUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlRheaderUnit.SelectedValue = dstData.Tables[0].Rows[0]["BaseUOM"].ToString();
                pnlMsg.Visible = false;
            }
            else
            {
                txtMaterialDescription.Text = txtReciepeDesc.Text = "";
                ddlRheaderUnit.SelectedValue = "";
                txtMaterialNmbr.Text = "";
                txtMaterialNmbr.Focus();

                lblMsg.Text = "Material doesnot exist in the plant " + Session[StaticKeys.MaterialPlantName].ToString();
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        else
        {
            txtMaterialDescription.Text = txtReciepeDesc.Text = "";
            ddlRheaderUnit.SelectedValue = "";
            txtMaterialNmbr.Text = "";
            txtMaterialNmbr.Focus();

            //BOM_RSeriesDT_08072020 Change MSG
            lblMsg.Text = "Please enter correct material code(3/4/8/1/2/7) series.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            //txtMaterialNo.Text = "";
        }
        //SetActUOMOperation();
        Session[StaticKeys.BOMRecipeMatNo] = txtMaterialNmbr.Text;
        Session[StaticKeys.BOMRecipeMatDesc] = txtMaterialDescription.Text;
        Session[StaticKeys.BOMRecipeBUOM] = ddlRheaderUnit.SelectedValue;
        }
        catch (Exception ex)
        { _log.Error("txtMaterialNmbr_TextChanged", ex); }
    }

    #endregion

    #region RecipeHeader Tab

    //Update base quantity in Operations of recipe
    protected void txtBQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
        string strBaseQty = txtBQty.Text;
        Session[StaticKeys.BOMRecipeBaseQty] = strBaseQty;
            //start Commented on 30.05.2018
            //SetBaseQuantityOperation(strBaseQty);
            //End Commented on 30.05.2018

        }
        catch (Exception ex)
        { _log.Error("txtBQty_TextChanged", ex); }
    }

    //Update the UOM in Operations for the resources
    protected void ddlRheaderUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        //start Commented on 30.05.2018
        //SetActUOMOperation();
        //End Commented on 30.05.2018
    }

    #endregion

    #region Operations

    //Insert new row in Operations
    protected void btnInsertRecord_Click(object sender, EventArgs e)
    {
        //start Commented on 30.05.2018
        //AddBlankRowOperation();
        //End Commented on 30.05.2018
    }

    //Start Commented on 30.05.2018
    //protected void btnOpSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lstOperationPhase.Clear();
    //        int cnt = 0;
    //        foreach (GridViewRow gvrow in GvOperation.Rows)
    //        {
    //            CheckBox chkPI = (CheckBox)GvOperation.Rows[gvrow.RowIndex].FindControl("chkPI");

    //            if (chkPI.Checked == true)
    //            {
    //                UpdatePICheckedState(gvrow.RowIndex);
    //                SetupValidation(((CheckBox)gvrow.FindControl("chkPI")).Checked, gvrow.RowIndex);
    //                if (((DropDownList)gvrow.FindControl("ddlResource")).SelectedValue != "" && ((CheckBox)gvrow.FindControl("chkPI")).Checked)
    //                {
    //                    string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)gvrow.FindControl("ddlResource")).SelectedValue, ddlPlant.SelectedValue);
    //                    SetValidationForOperStdKey(stdValKey, gvrow.RowIndex);
    //                }

    //                DropDownList ddlControlKey = (DropDownList)gvrow.FindControl("ddlControlKey");

    //                ////Add operation phase in ddl in inspection chara.


    //                if (ddlControlKey.SelectedValue != "")
    //                {
    //                    string strOperPhas = (gvrow.FindControl("txtOperation_Phase") as TextBox).Text;
    //                    if (chkPI.Checked && (ddlControlKey.SelectedValue == "PI02" || ddlControlKey.SelectedValue == "PI03"))
    //                    {
    //                        lstOperationPhase.Add(strOperPhas);
    //                        cnt++;
    //                    }
    //                }

    //            }




    //        }
    //        if (cnt > 0)
    //        {
    //            ddlInspPoints.SelectedValue = "Z03";
    //            ddlPartialLot.SelectedValue = "0";
    //        }
    //        else
    //        {
    //            ddlInspPoints.SelectedValue = "";
    //            ddlPartialLot.SelectedValue = "";
    //        }

    //        BindOperationPhase();


    //    }
    //    catch (Exception ex)
    //    { 

    //    }
    //}

    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    //List<string> lstOperationPhase = new List<string>();
    //    GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
    //    string strOperationPhase = (row.FindControl("txtOperation_Phase") as TextBox).Text;

    //    //Add operation phase in ddl in inspection chara.
    //    foreach (GridViewRow rowop in GvOperation.Rows)
    //    {
    //        CheckBox chkSelect = (CheckBox)rowop.FindControl("chkSelect");
    //        string strOperPhas = (rowop.FindControl("txtOperation_Phase") as TextBox).Text;
    //        //Label lblDestination = (Label)rowop.FindControl("lblDestination");                    
    //        if (chkSelect.Checked)
    //        {
    //            lstOperationPhase.Add(strOperPhas);
    //            lstSecResPhase.Add(strOperPhas);
    //            //lblDestination.Text = "01";
    //        }
    //    }
    //    BindOperationPhase();
    //    BindSecResOperations();

    //    //Add or delete MIC record on check of phase indicator.
    //    if ((row.FindControl("chkSelect") as CheckBox).Checked)
    //    {
    //        //Add inspection characteristics
    //        int gvrowcount = gvInspChara.Rows.Count;
    //        string positionNo = string.Empty;
    //        for (int i = 0; i < 4; i++)
    //        {
    //            AddBlankRowInspectionChara();
    //            TextBox txtOperationPhase = (TextBox)gvInspChara.Rows[gvrowcount + i].FindControl("txtOperationPhase");
    //            txtOperationPhase.Text = strOperationPhase;
    //            TextBox txtCharacteristicNo = (TextBox)gvInspChara.Rows[gvrowcount + i].FindControl("txtCharacteristicNo");

    //            if (!string.IsNullOrEmpty(positionNo))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //            }
    //            txtCharacteristicNo.Text = positionNo;
    //        }

    //        //Add Secondary resources
    //        int gvSecCount = grdSecResources.Rows.Count;
    //        positionNo = string.Empty;
    //        for (int i = 0; i < 4; i++)
    //        {
    //            AddBlankRowSecondaryResources();
    //            TextBox txtOperationPhase = (TextBox)grdSecResources.Rows[gvrowcount + i].FindControl("txtOperationPhase");
    //            txtOperationPhase.Text = strOperationPhase;
    //            TextBox txtSecRecItem = (TextBox)grdSecResources.Rows[gvrowcount + i].FindControl("txtSecRecItem");

    //            if (!string.IsNullOrEmpty(positionNo))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //            }
    //            txtSecRecItem.Text = positionNo;
    //        }
    //    }
    //    else
    //    {
    //        int index = 0;
    //        foreach (GridViewRow gvrow in gvInspChara.Rows)
    //        {
    //            TextBox txtOperationPhase = (TextBox)gvrow.FindControl("txtOperationPhase");
    //            if (strOperationPhase == txtOperationPhase.Text)
    //            {
    //                DeleteGrdRow(gvrow.RowIndex - index);
    //                index++;
    //            }
    //        }

    //        int index1 = 0;
    //        foreach (GridViewRow gvrow in grdSecResources.Rows)
    //        {
    //            TextBox txtOperationPhase = (TextBox)gvrow.FindControl("txtOperationPhase");
    //            if (strOperationPhase == txtOperationPhase.Text)
    //            {
    //                DeleteGrdRowSec(gvrow.RowIndex - index1);
    //                index1++;
    //            }
    //        }
    //    }
    //}

    //protected void chkPI_CheckedChanged(object sender, EventArgs e)
    //{
    //    //GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
    //    //UpdatePICheckedState(row.RowIndex);
    //}

    //protected void GvOperation_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownList ddlControlKey = (DropDownList)e.Row.FindControl("ddlControlKey");
    //        helperAccess.PopuplateDropDownList(ddlControlKey, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlControlKey'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlControlKey.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[1].ToString();

    //        if (ddlControlKey.SelectedValue == "")
    //        {
    //            ddlControlKey.SelectedValue = "PI01";
    //        }

    //        DropDownList ddlStdTextKey = (DropDownList)e.Row.FindControl("ddlStdTextKey");
    //        helperAccess.PopuplateDropDownList(ddlStdTextKey, "pr_GetDropDownListByControlNameModuleType 'R','ddlStdTextKey'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlStdTextKey.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[2].ToString();

    //        DropDownList ddlIndicatorRelavancyToCosting = (DropDownList)e.Row.FindControl("ddlIndicatorRelavancyToCosting");
    //        helperAccess.PopuplateDropDownList(ddlIndicatorRelavancyToCosting, "pr_GetDropDownListByControlNameModuleType 'B','ddlIndicatorRelavancyToCosting'", "LookUp_Desc", "LookUp_Code");
    //        ddlIndicatorRelavancyToCosting.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[4].ToString();

    //        DropDownList ddlPlantOper = (DropDownList)e.Row.FindControl("ddlPlant");
    //        helperAccess.PopuplateDropDownList(ddlPlantOper, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
    //        ddlPlantOper.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[5].ToString();

    //        DropDownList ddlAct_Operation_UoM = (DropDownList)e.Row.FindControl("ddlAct_Operation_UoM");
    //        //helperAccess.PopuplateDropDownList(ddlAct_Operation_UoM, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
    //        helperAccess.PopuplateDropDownList(ddlAct_Operation_UoM, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlAct_Operation_UoM.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[6].ToString();

    //        DropDownList ddlChngeCUnit = (DropDownList)e.Row.FindControl("ddlChngeCUnit");
    //        //helperAccess.PopuplateDropDownList(ddlChngeCUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
    //        helperAccess.PopuplateDropDownList(ddlChngeCUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlChngeCUnit.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[7].ToString();

    //        DropDownList ddlChngeOUnit = (DropDownList)e.Row.FindControl("ddlChngeOUnit");
    //        //helperAccess.PopuplateDropDownList(ddlChngeOUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
    //        helperAccess.PopuplateDropDownList(ddlChngeOUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlChngeOUnit.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[8].ToString();

    //        DropDownList ddlResource = (DropDownList)e.Row.FindControl("ddlResource");
    //        helperAccess.PopuplateDropDownList(ddlResource, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlResource','35','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlResource.SelectedValue = GvOperation.DataKeys[e.Row.RowIndex].Values[9].ToString();

    //        CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
    //        CheckBox chkPI = e.Row.FindControl("chkPI") as CheckBox;

    //        HiddenField hdnSelect = e.Row.FindControl("hdnSelect") as HiddenField;
    //        HiddenField hdnPI = e.Row.FindControl("hdnPI") as HiddenField;

    //        chkSelect.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnSelect.Value);
    //        chkPI.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPI.Value);

    //        Label lblDestination = (Label)e.Row.FindControl("lblDestination");

    //        TextBox txtOperation_Phase = e.Row.FindControl("txtOperation_Phase") as TextBox;
    //        TextBox txtSup_Operation = e.Row.FindControl("txtSup_Operation") as TextBox;
    //        if (txtOperation_Phase.Text == "0010")
    //        {
    //            chkPI.Enabled = false;
    //            txtSup_Operation.Enabled = false;
    //        }

    //        TextBox txtFirst_Std_Value = (TextBox)e.Row.FindControl("txtFirst_Std_Value");
    //        TextBox txtFirst_Std_Value_Unit = (TextBox)e.Row.FindControl("txtFirst_Std_Value_Unit");
    //        TextBox txtSec_Std_Value = (TextBox)e.Row.FindControl("txtSec_Std_Value");
    //        TextBox txtSec_Std_Value_Unit = (TextBox)e.Row.FindControl("txtSec_Std_Value_Unit");
    //        TextBox txtThird_Std_Value = (TextBox)e.Row.FindControl("txtThird_Std_Value");
    //        TextBox txtThird_Std_Value_Unit = (TextBox)e.Row.FindControl("txtThird_Std_Value_Unit");
    //        TextBox txtChargeQty = (TextBox)e.Row.FindControl("txtChargeQty");
    //        TextBox txtOperQty = (TextBox)e.Row.FindControl("txtOperQty");

    //        RequiredFieldValidator reqtxtChargeQty = (RequiredFieldValidator)e.Row.FindControl("reqtxtChargeQty");
    //        RequiredFieldValidator reqtxtOperQty = (RequiredFieldValidator)e.Row.FindControl("reqtxtOperQty");
    //        //DropDownList ddlResource = (DropDownList)e.Row.FindControl("ddlResource");

    //        if (chkPI.Checked)
    //        {
    //            //txtFirst_Std_Value.Enabled = txtSec_Std_Value.Enabled = txtThird_Std_Value.Enabled = true;
    //            //txtFirst_Std_Value_Unit.Enabled = txtSec_Std_Value_Unit.Enabled = txtThird_Std_Value_Unit.Enabled 
    //            if (ddlAct_Operation_UoM.SelectedValue != ddlRheaderUnit.SelectedValue)
    //            {
    //                txtChargeQty.Enabled = txtOperQty.Enabled = true;
    //                reqtxtChargeQty.Enabled = reqtxtOperQty.Enabled = true;
    //            }
    //            if (txtSup_Operation.Text != "")
    //                ddlResource.Enabled = false;
    //            chkSelect.Enabled = true;
    //        }
    //        //SetupValidation(chkPI.Checked, e.Row.RowIndex);

    //    }
    //}

    //protected void txtBase_Quantity_TextChanged(object sender, EventArgs e)
    //{
    //    //GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    //TextBox txtOprBaseQty = (TextBox)row.FindControl("txtBase_Quantity");
    //    //DropDownList ddlAct_Operation_UoM = (DropDownList)row.FindControl("ddlAct_Operation_UoM");
    //    //if (txtOprBaseQty.Text != txtBQty.Text)
    //    //{
    //    //    txtChngeBQty.Text = txtBQty.Text;
    //    //    txtChngeBQty.Enabled = false;
    //    //    lblRowNo.Text = row.RowIndex.ToString();
    //    //    if (ddlRheaderUnit.SelectedValue != "")
    //    //    {
    //    //        ddlBUnit.SelectedValue = ddlRheaderUnit.SelectedValue;
    //    //        ddlChngeCUnit.SelectedValue = ddlRheaderUnit.SelectedValue;
    //    //    }
    //    //    if (ddlAct_Operation_UoM.SelectedValue != "")
    //    //    {
    //    //        ddlChngeOUnit.SelectedValue = ddlAct_Operation_UoM.SelectedValue;
    //    //    }
    //    //    ddlBUnit.Enabled = false;
    //    //    ddlChngeCUnit.Enabled = false;
    //    //    ddlChngeOUnit.Enabled = false;
    //    //    ModalPopupExtender.Show();
    //    //}
    //}

    //protected void ddlAct_Operation_UoM_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
    //    CheckBox chkPI = (CheckBox)row.FindControl("chkPI");

    //    TextBox txtOprBaseQty = (TextBox)row.FindControl("txtBase_Quantity");
    //    TextBox txtChargeQty = (TextBox)row.FindControl("txtChargeQty");
    //    TextBox txtOperQty = (TextBox)row.FindControl("txtOperQty");

    //    DropDownList ddlAct_Operation_UoM = (DropDownList)row.FindControl("ddlAct_Operation_UoM");
    //    DropDownList ddlChngeCUnit1 = (DropDownList)row.FindControl("ddlChngeCUnit");
    //    DropDownList ddlChngeOUnit1 = (DropDownList)row.FindControl("ddlChngeOUnit");

    //    RequiredFieldValidator reqtxtChargeQty = (RequiredFieldValidator)row.FindControl("reqtxtChargeQty");
    //    RequiredFieldValidator reqtxtOperQty = (RequiredFieldValidator)row.FindControl("reqtxtOperQty");

    //    if (chkPI.Checked)
    //    {
    //        if (ddlAct_Operation_UoM.SelectedValue != ddlRheaderUnit.SelectedValue)
    //        {
    //            txtChargeQty.Enabled = true;
    //            txtOperQty.Enabled = true;
    //            reqtxtChargeQty.Enabled = reqtxtOperQty.Enabled = true;
    //            ddlChngeCUnit1.SelectedValue = ddlRheaderUnit.SelectedValue;
    //            ddlChngeOUnit1.SelectedValue = ddlAct_Operation_UoM.SelectedValue;
    //        }
    //    }
    //}

    //protected void txtSup_Operation_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    TextBox txtSup_Operation = (TextBox)row.FindControl("txtSup_Operation");
    //    TextBox txtOperation_Phase = (TextBox)row.FindControl("txtOperation_Phase");
    //    DropDownList ddlResource = (DropDownList)row.FindControl("ddlResource");
    //    CheckBox chkPI = (CheckBox)row.FindControl("chkPI");

    //    if (txtSup_Operation.Text != "")
    //    {
    //        List<String> operPhase = new List<String>();
    //        for (int i = 0; i < row.RowIndex; i++)
    //        {
    //            TextBox txtOperation_Phase1 = (TextBox)GvOperation.Rows[i].FindControl("txtOperation_Phase");
    //            operPhase.Add(txtOperation_Phase1.Text.ToString());

    //        }
    //        if (!(operPhase.Contains(txtSup_Operation.Text.ToString())))
    //        {
    //            lblMsg.Text = "Superior operation must be the operation phase above the current operation.";
    //            pnlMsg.Visible = true;
    //            pnlMsg.CssClass = "error";
    //        }
    //        else
    //        {
    //            chkPI.Checked = true;
    //            pnlMsg.Visible = false;
    //            string resource = BindResourceFromSupOper(txtSup_Operation.Text.ToString());
    //            ddlResource.SelectedValue = resource;
    //            ddlResource.Enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        chkPI.Checked = false;
    //    }
    //    UpdatePICheckedState(row.RowIndex);
    //}

    //protected void ddlResource_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
    //    DropDownList ddlResource = (DropDownList)row.FindControl("ddlResource");
    //    TextBox txtOperation_Phase = (TextBox)row.FindControl("txtOperation_Phase");
    //    foreach (GridViewRow grow in GvOperation.Rows)
    //    {
    //        TextBox txtSup_Operation = (TextBox)grow.FindControl("txtSup_Operation");
    //        CheckBox chkPI = (CheckBox)grow.FindControl("chkPI");
    //        DropDownList ddlResource1 = (DropDownList)grow.FindControl("ddlResource");
    //        if (txtSup_Operation.Text == txtOperation_Phase.Text && chkPI.Checked)
    //        {
    //            ddlResource1.SelectedValue = ddlResource.SelectedValue;
    //            if (ddlResource1.SelectedValue != "" && chkPI.Checked)
    //            {
    //                string stdValKey = objRecipeAccess.GetOprRescStdUnits(ddlResource1.SelectedValue, ddlPlant.SelectedValue);
    //                if (stdValKey != "")
    //                {
    //                    SetValidationForOperStdKey(stdValKey, grow.RowIndex);
    //                }
    //            }
    //        }
    //    }

    //}

    //protected void ddlControlKey_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;
    //    DropDownList ddlControlKey = (DropDownList)row.FindControl("ddlControlKey");
    //    if (ddlControlKey.SelectedValue != "")
    //        AddOperationForInspection();
    //}
    //End Commented on 30.05.2018

    #endregion

    #region Inspection Characteristics
    //Start Commented on 30.05.2018
    //protected void btnAddInspChara_Click(object sender, EventArgs e)
    //{
    //    if (ddlOperationPhase.SelectedValue.ToString() != "")
    //        AddBlankRowInspectionChara();
    //}

    //protected void gvInspChara_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownList ddlSamplingProcedure = (DropDownList)e.Row.FindControl("ddlSamplingProcedure");
    //        helperAccess.PopuplateDropDownList(ddlSamplingProcedure, "pr_GetDropDownListByControlNameModuleType 'R','ddlSamplingProcedure'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlSamplingProcedure.SelectedValue = gvInspChara.DataKeys[e.Row.RowIndex].Values[1].ToString();

    //        //DropDownList ddlMIC = (DropDownList)e.Row.FindControl("ddlMIC");
    //        //helperAccess.PopuplateDropDownList(ddlMIC, "pr_GetDropDownListForMICByPlantCode " + Session[StaticKeys.MaterialPlantId].ToString(), "LookUp_Desc", "LookUp_Code", "");
    //        //ddlMIC.SelectedValue = gvInspChara.DataKeys[e.Row.RowIndex].Values[2].ToString();

    //        // Start- commentted by Nitish Rao 28/03/2018
    //        //DropDownList ddlInspPtCmpt = (DropDownList)e.Row.FindControl("ddlInspPtCmpt");
    //        //helperAccess.PopuplateDropDownList(ddlInspPtCmpt, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlInspPtCmpt'", "LookUp_Desc", "LookUp_Code", "");
    //        //ddlInspPtCmpt.SelectedValue = gvInspChara.DataKeys[e.Row.RowIndex].Values[3].ToString();
    //        // End- commentted by Nitish Rao 28/03/2018

    //        CheckBox chkNoRelation = e.Row.FindControl("chkNoRelation") as CheckBox;
    //        HiddenField hdnNoRelation = e.Row.FindControl("hdnNoRelation") as HiddenField;
    //        chkNoRelation.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnNoRelation.Value);
    //    }
    //}

    //protected void gvInspChara_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    Control ctl = e.CommandSource as Control;
    //    GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dstData = new DataSet();

    //    if (e.CommandName == "D")
    //    {
    //        try
    //        {
    //            dt.Columns.Add(new DataColumn("Operation_Phase"));
    //            dt.Columns.Add(new DataColumn("Recipe_InspChara_Id"));
    //            dt.Columns.Add(new DataColumn("Characteristic_No"));
    //            dt.Columns.Add(new DataColumn("MIC"));
    //            dt.Columns.Add(new DataColumn("Sampling_Procedure"));
    //            dt.Columns.Add(new DataColumn("CodeGrp"));
    //            //Start -Commented by Nitish Rao 28/03/2018
    //            //dt.Columns.Add(new DataColumn("InspPtCmpt"));
    //            //End -Commented by Nitish Rao 28/03/2018
    //            dt.Columns.Add(new DataColumn("NoRelation"));

    //            foreach (GridViewRow row in gvInspChara.Rows)
    //            {
    //                dr = dt.NewRow();
    //                dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
    //                dr["Recipe_InspChara_Id"] = (row.FindControl("lblRecipe_InspChara_Id") as Label).Text;
    //                dr["Characteristic_No"] = (row.FindControl("txtCharacteristicNo") as TextBox).Text;
    //                //dr["MIC"] = (row.FindControl("ddlMIC") as DropDownList).SelectedValue;
    //                dr["MIC"] = (row.FindControl("txtMIC") as TextBox).Text;
    //                dr["Sampling_Procedure"] = (row.FindControl("ddlSamplingProcedure") as DropDownList).SelectedValue;
    //                dr["CodeGrp"] = (row.FindControl("txtCodeGrp") as TextBox).Text;

    //                //Start -Commented by Nitish Rao 28/03/2018
    //                //dr["InspPtCmpt"] = (row.FindControl("ddlInspPtCmpt") as DropDownList).SelectedValue;
    //                //dr["InspPtCmpt"] = ddlInspPtCmptmain.SelectedValue;
    //                //End -Commented by Nitish Rao 28/03/2018

    //                dr["NoRelation"] = (row.FindControl("chkNoRelation") as CheckBox).Checked ? "True" : "false";
    //                dt.Rows.Add(dr);

    //                //start Commented by nitish rao 28/03/2018
    //                //if (objRecipeAccess.GetInspCharDetail(((Label)currentRow.FindControl("lblRecipe_InspChara_Id")).Text.ToString(), lblRecipeId.Text) > 0)
    //                //{
    //                int i = objRecipeAccess.DeleteInspCharData(((Label)currentRow.FindControl("lblRecipe_InspChara_Id")).Text.ToString(), lblRecipeId.Text);
    //                //}
    //                //End Commented by nitish rao 28/03/2018
    //            }
    //            dstData.Tables.Add(dt);
    //            dstData.AcceptChanges();

    //            dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
    //            dstData.AcceptChanges();

    //            DataView dv = new DataView(dstData.Tables[0]);
    //            dv.Sort = "Operation_Phase Asc";
    //            DataTable dtSorted = dv.ToTable();

    //            gvInspChara.DataSource = dtSorted;
    //            gvInspChara.DataBind();

    //            //ViewState["dstInspChar"] = dstData;


    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
    //End Commented on 30.05.2018

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
    //Start Commented on 30.05.2018
    //protected void txtMIC_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    TextBox txtMIC = (TextBox)row.FindControl("txtMIC");

    //    if (txtMIC.Text != "")
    //    {
    //        SetupValidationForInspChar(row.RowIndex);
    //    }
    //}
    //End Commented on 30.05.2018
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

    #region Secondary Resource
    //start Commented on 30.05.2018
    //protected void btnAddSecRes_Click(object sender, EventArgs e)
    //{
    //    if (ddlSecRes.SelectedValue.ToString() != "")
    //        AddBlankRowSecondaryResources();
    //}

    //protected void grdSecResources_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        // Start Adding  by Nitish Rao  13.02.2018
    //        //to Hide Operation Phase Column from Secondary Resource
    //        e.Row.Cells[1].Visible = false;
    //        // End Adding  by Nitish Rao  13.02.2018

    //        DropDownList ddlSecResource = (DropDownList)e.Row.FindControl("ddlSecResource");
    //        helperAccess.PopuplateDropDownList(ddlSecResource, "pr_GetResourceDropDownListByControlNameModuleTypePlantFilter 'RSRC','ddlSecResource','35','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlSecResource.SelectedValue = grdSecResources.DataKeys[e.Row.RowIndex].Values[1].ToString();
    //    }
    //}

    //protected void ddlSecResource_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;

    //    DropDownList ddlSecResource = (DropDownList)row.FindControl("ddlSecResource");
    //    TextBox txtLabor = (TextBox)row.FindControl("txtLabor");
    //    TextBox txtProcess = (TextBox)row.FindControl("txtProcess");
    //    TextBox txtDuration = (TextBox)row.FindControl("txtDuration");
    //    TextBox txtUnit1 = (TextBox)row.FindControl("txtUnit1");
    //    TextBox txtUnit2 = (TextBox)row.FindControl("txtUnit2");
    //    TextBox txtUnit3 = (TextBox)row.FindControl("txtUnit3");
    //    TextBox txtActivityType1 = (TextBox)row.FindControl("txtActivityType1");
    //    TextBox txtActivityType2 = (TextBox)row.FindControl("txtActivityType2");
    //    TextBox txtActivityType3 = (TextBox)row.FindControl("txtActivityType3");

    //    if (ddlSecResource.SelectedValue != "")
    //    {
    //        DataSet dsRsrc = objRecipeAccess.GetRsrcDataByResourceName(ddlSecResource.SelectedValue, ddlPlant.SelectedValue);
    //        if (dsRsrc.Tables[0].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dsRsrc.Tables[0].Rows.Count; i++)
    //            {
    //                if (dsRsrc.Tables[0].Rows[i]["Number"].ToString() == "0001")
    //                {
    //                    txtUnit1.Text = dsRsrc.Tables[0].Rows[i]["ActivityUnit"].ToString();
    //                    txtActivityType1.Text = dsRsrc.Tables[0].Rows[i]["ActivityType"].ToString();
    //                }
    //                else if (dsRsrc.Tables[0].Rows[i]["Number"].ToString() == "0002")
    //                {
    //                    txtUnit2.Text = dsRsrc.Tables[0].Rows[i]["ActivityUnit"].ToString();
    //                    txtActivityType2.Text = dsRsrc.Tables[0].Rows[i]["ActivityType"].ToString();
    //                }
    //                else if (dsRsrc.Tables[0].Rows[i]["Number"].ToString() == "0003")
    //                {
    //                    txtUnit3.Text = dsRsrc.Tables[0].Rows[i]["ActivityUnit"].ToString();
    //                    txtActivityType3.Text = dsRsrc.Tables[0].Rows[i]["ActivityType"].ToString();
    //                }
    //            }
    //        }
    //        //txtUnit1.Text = txtUnit2.Text = txtUnit3.Text = objSecRes.Unit1;
    //        //txtActivityType1.Text = txtActivityType2.Text = txtActivityType3.Text = objSecRes.ActivityType1;
    //        SetValidationForSecRes(dsRsrc.Tables[0].Rows[0]["StdValKey"].ToString(), row.RowIndex);
    //    }

    //}

    //protected void grdSecResources_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    Control ctl = e.CommandSource as Control;
    //    GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

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

    //        foreach (GridViewRow row in grdSecResources.Rows)
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

    //            if (objRecipeAccess.GetSecResDetail(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text) > 0)
    //            {
    //                int i = objRecipeAccess.DeleteSecResData(((Label)currentRow.FindControl("lblRecipe_SecResource_Id")).Text.ToString(), lblRecipeId.Text);
    //            }
    //        }
    //        dstData.Tables.Add(dt);
    //        dstData.AcceptChanges();

    //        dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
    //        dstData.AcceptChanges();

    //        DataView dv = new DataView(dstData.Tables[0]);
    //        dv.Sort = "Operation_Phase Asc";
    //        DataTable dtSorted = dv.ToTable();

    //        grdSecResources.DataSource = dtSorted;
    //        grdSecResources.DataBind();

    //        ViewState["dstSecRes"] = dstData;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //End Commented on 30.05.2018
    #endregion

    #region Prod. version

    //protected void btnProdVer_Click(object sender, EventArgs e)
    //{
    //    FillProdVersion();
    //    lblPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();
    //    modProdVer.Show();
    //}

    //protected void btnProdSave_Click(object sender, EventArgs e)
    //{
    //    if (SaveProdVersion())
    //    {
    //        lblMsg.Text = "Header saved";
    //        pnlMsg.CssClass = "success";
    //        pnlMsg.Visible = true;
    //    }
    //    else
    //    {
    //        lblMsg.Text = Messages.GetMessage(-1);
    //        pnlMsg.CssClass = "error";
    //        pnlMsg.Visible = true;
    //    }
    //}

    #endregion

    #region Material HELP

    protected void imgHelpSearchMaterial_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        
        modMatSearch.Show();
        }
        catch (Exception ex)
        { _log.Error("imgHelpSearchMaterial_Click", ex); }
    }

    protected void btnSeachMaterialHELP_Click(object sender, EventArgs e)
    {
        try
        {
       
        ReadMaterialHelp();
        modMatSearch.Show();
        }
        catch (Exception ex)
        { _log.Error("btnSeachMaterialHELP_Click", ex); }
    }

    protected void grdMaterialHELP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdMaterialHELP, "MTRSELECT$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
        }
        catch (Exception ex)
        { _log.Error("grdMaterialHELP_RowDataBound", ex); }
    }

    protected void grdMaterialHELP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
       
        if (e.CommandName == "MTRSELECT")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdMaterialHELP.Rows[index];
            Label lblMaterialNumberHELP = row.FindControl("lblMaterialNumberHELP") as Label;
            Label lblMatDescHELP = row.FindControl("lblMatDescHELP") as Label;
            Label lblBOMUSAGEHELP = row.FindControl("lblBOMUSAGEHELP") as Label;
            Label lblPlantHELP = row.FindControl("lblPlantHELP") as Label;
            Label lblAltBOM = row.FindControl("lblAltBOM") as Label;
            Label lblBaseUOM = row.FindControl("lblBaseUOM") as Label;


            txtMaterialNmbr.Text = "";
            txtMaterialNmbr.Text = lblMaterialNumberHELP.Text;
            txtMaterialDescription.Text = txtReciepeDesc.Text = lblMatDescHELP.Text;
            ddlPlant.SelectedValue = lblPlantHELP.Text;
            helperAccess.PopuplateDropDownList(ddlRheaderUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
            ddlRheaderUnit.SelectedValue = lblBaseUOM.Text;

            txtMaterialNmbr.Focus();
            //Start Commented on 30.05.2018
            //SetActUOMOperation();
            //End Commented on 30.05.2018
            modMatSearch.Hide();

            Session[StaticKeys.BOMRecipeMatNo] = txtMaterialNmbr.Text;
            Session[StaticKeys.BOMRecipeMatDesc] = txtMaterialDescription.Text;
            Session[StaticKeys.BOMRecipeBUOM] = ddlRheaderUnit.SelectedValue;

            pnlMsg.Visible = false;
        }

        }
        catch (Exception ex)
        { _log.Error("grdMaterialHELP_RowCommand", ex); }
    }

    #endregion

    #endregion

    #region Methods

    private void FillRecipeData(string mode)
    {
        RecipeHeader objRecipeHeader = GetRecipeHeaderData();
        try
        {
            if (objRecipeHeader.Recipe_HeaderID > 0)
            {
                lblRecipeId.Text = objRecipeHeader.Recipe_HeaderID.ToString();
                txtRGroup.Text = objRecipeHeader.Recipe_Group;
                txtRecipe.Text = objRecipeHeader.Recipe;
                ddlPlant.SelectedValue = objRecipeHeader.Plant_Id;
                txtReciepeDesc.Text = objRecipeHeader.TaskListDesc;
                txtMaterialNmbr.Text = objRecipeHeader.MaterialNo;
                txtMaterialDescription.Text = objRecipeHeader.MaterialDesc;
                Session[StaticKeys.BOMRecipeMatNo] = objRecipeHeader.MaterialNo;
                Session[StaticKeys.BOMRecipeMatDesc] = objRecipeHeader.MaterialDesc;
                Session[StaticKeys.RecipeGroup] = objRecipeHeader.Recipe_Group;//BOM_8200050878 for new Module created 227,228,229
                //manali chavan

                RecipeDetail objRecipeDetail = GetRecipeHeaderDetail(lblRecipeId.Text);
                if (objRecipeDetail.Recipe_HeaderDetail_Id > 0)
                {
                    lblRecipe_HeaderDetail_Id.Text = objRecipeDetail.Recipe_HeaderDetail_Id.ToString();
                    ddlRStatus.SelectedValue = objRecipeDetail.Status;
                    ddlUsages.SelectedValue = objRecipeDetail.Usage;

                    chkRStatus.Checked = objRecipeDetail.chkStatus == "True" ? true : false;

                    txtplannergp.Text = objRecipeDetail.Planner_Group;
                    txtResourcenw.Text = objRecipeDetail.Resource_network;
                    txtNWPlant.Text = objRecipeDetail.Network_Plant;
                    txtFrom.Text = objRecipeDetail.From_LSize;
                    txtTo.Text = objRecipeDetail.To_LSize;
                    txtBQty.Text = objRecipeDetail.Base_Quantity;
                    helperAccess.PopuplateDropDownList(ddlRheaderUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                    ddlRheaderUnit.SelectedValue = objRecipeDetail.Unit;
                    txtchargeqty.Text = objRecipeDetail.Charge_Quantity;
                    txtOperationQty.Text = objRecipeDetail.Operation_Quantity;
                    //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                    txtRemark.Text = objRecipeDetail.Remarks;
                    txtReason.Text = objRecipeDetail.Reason;
                    //-ENded to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                    //start Commented on 30.05.2018
                    //ddlInspPoints.SelectedValue = objRecipeDetail.Insp_Points;
                    //ddlPartialLot.SelectedValue = objRecipeDetail.Partial_Lot;
                    //End Commented on 30.05.2018

                    Session[StaticKeys.BOMRecipeBUOM] = objRecipeDetail.Unit;
                    Session[StaticKeys.BOMRecipeBaseQty] = objRecipeDetail.Base_Quantity;
                    Session[StaticKeys.BOMRecipeFrom] = objRecipeDetail.From_LSize;
                    Session[StaticKeys.BOMRecipeTo] = objRecipeDetail.To_LSize;

                    //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
                    //{
                    //    if(chkRStatus.Checked == true)
                    //        ddlUsages.SelectedValue = "3";
                    //}
                }
                else
                {
                    lblRecipe_HeaderDetail_Id.Text = "0";
                    txtFrom.Text = "";
                    txtTo.Text = "99999999";
                    ddlRStatus.SelectedValue = "1";
                    ddlUsages.SelectedValue = "1";

                    //Start Commented on 30.05.2018
                    //ddlPartialLot.SelectedValue = "";
                    //ddlInspPoints.SelectedValue = "";
                    //End Commented on 30.05.2018
                }

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
                lblRecipeId.Text = "0";
                lblRecipe_HeaderDetail_Id.Text = "0";
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                txtFrom.Text = "";
                txtTo.Text = "99999999";
                ddlRStatus.SelectedValue = "1";
                ddlUsages.SelectedValue = "1";

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
            ddlRStatus.Enabled = true;
            txtMaterialNmbr.Enabled = false;
            txtReciepeDesc.Enabled = false;
            chkRStatus.Enabled = false;
            imgHelpSearchMaterial.Visible = false;
            txtBQty.Enabled = false;

            //Start Commented on 30.05.2018
            //GvOperation.Enabled = false;
            //grdSecResources.Enabled = false;
            //gvInspChara.Enabled = false;
            //ddlInspPoints.Enabled = false;
            //ddlPartialLot.Enabled = false;
            //End Commented on 30.05.2018
        }
        //BOM_8200050878 for new Module created 227,228,229
        //manali chavan
        //DT_26-08-2020 
        else if ((txtRGroup.Text != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
        {
            ddlRStatus.Enabled = true;
            txtMaterialNmbr.Enabled = false;
            txtReciepeDesc.Enabled = false;
            chkRStatus.Enabled = false;
            imgHelpSearchMaterial.Visible = false;
            txtBQty.Enabled = false;
        }

        }
        catch (Exception ex)
        { _log.Error("ConfigureHeaderControls", ex); }
    }

    private void UpdateRecipeStatus()
    {
        try
        {
       
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
        {
            if (chkRStatus.Checked)
                objRecipeAccess.UpdateRecipeStatus(lblRecipeId.Text, lblUserId.Text);
        }
        }
        catch (Exception ex)
        { _log.Error("UpdateRecipeStatus", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlPlantHELP, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlRStatus, "pr_GetDropDownListByControlNameModuleType 'R','ddlRStatus'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlUsages, "pr_GetDropDownListByControlNameModuleType 'R','ddlUsages'", "LookUp_Desc", "LookUp_Code", "");
        //start Commented on 30.05.2018
        //helperAccess.PopuplateDropDownList(ddlInspPoints, "pr_GetDropDownListByControlNameModuleType 'R','ddlInspPoints'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlPartialLot, "pr_GetDropDownListByControlNameModuleType 'R','ddlPartialLot'", "LookUp_Desc", "LookUp_Code", "-1");
        //End Commented on 30.05.2018
        helperAccess.PopuplateDropDownList(ddlRheaderUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

            //Start Commented on 30.05.2018
            //helperAccess.PopuplateDropDownList(ddlInspPtCmptmain, "pr_GetDropDownListByControlNameModuleType_Code 'R','ddlInspPtCmpt'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlSamplingProceduremain, "pr_GetDropDownListByControlNameModuleType 'R','ddlSamplingProcedure'", "LookUp_Desc", "LookUp_Code", "");
            //End Commented on 30.05.2018
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private RecipeDetail GetRcpDetailsControlValues()
    {
        RecipeDetail objRcpDetails = new RecipeDetail();
        Utility objUtil = new Utility();
        try
        {
        objRcpDetails.Recipe_HeaderDetail_Id = Convert.ToInt32(lblRecipe_HeaderDetail_Id.Text);
        objRcpDetails.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
        objRcpDetails.Status = ddlRStatus.SelectedValue;
        objRcpDetails.Usage = ddlUsages.SelectedValue;
        objRcpDetails.chkStatus = chkRStatus.Checked == true ? "1" : "0";
        objRcpDetails.Planner_Group = txtplannergp.Text;
        objRcpDetails.Resource_network = txtResourcenw.Text;
        objRcpDetails.Network_Plant = txtNWPlant.Text;
        objRcpDetails.From_LSize = txtFrom.Text;
        objRcpDetails.To_LSize = txtTo.Text;
        objRcpDetails.Unit = ddlRheaderUnit.SelectedValue;
        objRcpDetails.Base_Quantity = txtBQty.Text;
        objRcpDetails.Charge_Quantity = txtchargeqty.Text;
        objRcpDetails.Operation_Quantity = txtOperationQty.Text;
        //Start Commented on 30.05.2018
        objRcpDetails.Insp_Points = "";
        objRcpDetails.Partial_Lot = "";
        //Start Commented on 30.05.2018
        objRcpDetails.UserId = lblUserId.Text;
        objRcpDetails.TodayDate = objUtil.GetDate();
        objRcpDetails.IPAddress = objUtil.GetIpAddress();
        objRcpDetails.Mode = lblMode.Text;
        //Started to Add Remark and Reason textbox. Ticket number 8200064571
        objRcpDetails.Remarks = txtRemark.Text;
        objRcpDetails.Reason = txtReason.Text;
            //End of Add Remark and Reason textbox. Ticket number 8200064571

        }
        catch (Exception ex)
        { _log.Error("GetRcpDetailsControlValues", ex); }
        return objRcpDetails;
    }

    private bool SaveDetail()
    {
        bool flg = false;
        try
        {
            RecipeDetail objRcpDetails = new RecipeDetail();
            objRcpDetails = GetRcpDetailsControlValues();
            if (objRecipeAccess.SaveRecipeHeaderDetailsData(objRcpDetails) > 0)
                flg = true;
            //Start Commented on 30.05.2018
            //Get the operation filled rows
            //int lstRow = GetOperationDataFilledRows();

            //if (lstRow > 0)
            //{
            //    for (int i = 0; i <= lstRow; i++)
            //    {
            //        RecipeOperations objRcpOperations = new RecipeOperations();
            //        GridViewRow row = GvOperation.Rows[i];
            //        objRcpOperations = GetOperationData(row);
            //        if (objRecipeAccess.SaveOperationDetails(objRcpOperations) > 0)
            //        {
            //            flg = true;
            //        }
            //    }
            //    if (flg)
            //    {
            //        foreach (GridViewRow row in gvInspChara.Rows)
            //        {
            //            RecipeInspChara objRcpInsChar = new RecipeInspChara();
            //            objRcpInsChar = GetInspCharData(row);
            //            if (objRecipeAccess.SaveInspCharDetails(objRcpInsChar) > 0)
            //            {
            //                flg = true;
            //            }
            //            else
            //                flg = false;
            //        }

            //        foreach (GridViewRow row in grdSecResources.Rows)
            //        {
            //            RecipeSecRes objRcpSecRes = new RecipeSecRes();
            //            objRcpSecRes = GetSecResources(row);
            //            if (objRecipeAccess.SaveSecResDetails(objRcpSecRes) > 0)
            //            {
            //                flg = true;
            //            }
            //            else
            //                flg = false;
            //        }
            //    }
            //}
            //else
            //{
            //    msg = "Atleast one resource and phase indicator must be selected.";
            //    lblMsg.Text = "Atleast one resource and phase indicator must be selected.";
            //    pnlMsg.Visible = true;
            //    pnlMsg.CssClass = "error";
            //}
            //End Commented on 30.05.2018
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SaveDetail", ex);
        }

        return flg;
    }

    //Start Addition By Nitish Rao Date: 14.02.2017
    // To Update Last Operation Number to All secondary resource item
    //Start Commented on 30.05.2018
    //private bool UpdateLastOperation()
    //{
    //    bool flg = false;
    //    try
    //    {
    //        RecipeDetail objRcpDetails = new RecipeDetail();
    //        objRcpDetails = GetRcpDetailsControlValues();
    //        if (objRecipeAccess.UpdateRecipeSecResouseDetails(objRcpDetails) >= 0)
    //        {
    //            flg = true;
    //        }

    //        //Get the operation filled rows
    //       // int lstRow = GetOperationDataFilledRows();


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //    return flg;
    //}
    //End Commented on 30.05.2018
    //End Addition By Nitish Rao Date: 14.02.2017


    #region Recipe Header

    private RecipeHeader GetRecipeHeaderData()
    {
        return objRecipeAccess.GetRecipeHeaderData(lblMasterHeaderId.Text);
    }

    private bool SaveHeader()
    {
        bool flg = false;

        try
        {
            RecipeHeader ObjRecipeHeader = GetControlsValue();
            int rcpHeaderId = objRecipeAccess.SaveRecipeHeaderData(ObjRecipeHeader);
            if (rcpHeaderId > 0)
            {
                lblRecipeId.Text = rcpHeaderId.ToString();
                Session[StaticKeys.ReciepeID] = rcpHeaderId;
                flg = true;
                lblMsg.Text = "Header saved";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("SaveHeader", ex);
        }
        return flg;
    }

    private RecipeHeader GetControlsValue()
    {
        RecipeHeader ObjRecipeHeader = new RecipeHeader();
        Utility objUtil = new Utility();
        try
        {
       
        ObjRecipeHeader.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
        ObjRecipeHeader.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjRecipeHeader.Recipe_Group = txtRGroup.Text;
        ObjRecipeHeader.Recipe = txtRecipe.Text;
        ObjRecipeHeader.Plant_Id = ddlPlant.SelectedValue;
        ObjRecipeHeader.TaskListDesc = txtReciepeDesc.Text;
        ObjRecipeHeader.MaterialNo = txtMaterialNmbr.Text;
        ObjRecipeHeader.MaterialDesc = txtMaterialDescription.Text;

        ObjRecipeHeader.UserId = lblUserId.Text;
        ObjRecipeHeader.TodayDate = objUtil.GetDate();
        ObjRecipeHeader.IPAddress = objUtil.GetIpAddress();
        ObjRecipeHeader.Mode = lblMode.Text;
        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }

        return ObjRecipeHeader;
    }

    #endregion

    #region RecipeHeader Tab

    private RecipeDetail GetRecipeHeaderDetail(string recipe_headerId)
    {
        return objRecipeAccess.GetRecipeHeaderDetail(recipe_headerId);
    }

    #endregion

    #region Operations
    //Start Commented on 30.05.2018
    //private void Add_10BlankRow()
    //{
    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dstData = new DataSet();
    //    string positionNo = string.Empty;

    //    int tempId = 1;
    //    try
    //    {
    //        dt.Columns.Add(new DataColumn("Recipe_Operation_Id"));
    //        dt.Columns.Add(new DataColumn("Select"));
    //        dt.Columns.Add(new DataColumn("Operation_Phase"));
    //        dt.Columns.Add(new DataColumn("Phase_Indicator"));
    //        dt.Columns.Add(new DataColumn("Sup_Operation"));
    //        dt.Columns.Add(new DataColumn("Destinatn"));
    //        dt.Columns.Add(new DataColumn("Resource"));
    //        dt.Columns.Add(new DataColumn("Control_key"));
    //        dt.Columns.Add(new DataColumn("StdText_Key"));
    //        dt.Columns.Add(new DataColumn("Description"));

    //        dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));

    //        dt.Columns.Add(new DataColumn("Base_Quantity"));
    //        dt.Columns.Add(new DataColumn("Act_Operation_UoM"));
    //        dt.Columns.Add(new DataColumn("First_Std_Value"));
    //        dt.Columns.Add(new DataColumn("First_Std_Value_Unit"));

    //        dt.Columns.Add(new DataColumn("Sec_Std_Value"));
    //        dt.Columns.Add(new DataColumn("Sec_Std_Value_Unit"));


    //        dt.Columns.Add(new DataColumn("Third_Std_Value"));
    //        dt.Columns.Add(new DataColumn("Third_Std_Value_Unit"));


    //        dt.Columns.Add(new DataColumn("Plant"));
    //        dt.Columns.Add(new DataColumn("ChargeQty"));
    //        dt.Columns.Add(new DataColumn("OperQty"));
    //        dt.Columns.Add(new DataColumn("ChargeUnit"));
    //        dt.Columns.Add(new DataColumn("OperUnit"));

    //        dt.Columns.Add(new DataColumn("DeletionFlag"));
    //        dt.Columns.Add(new DataColumn("DFlagValue"));

    //        for (int i = 1; i <= 6; i++)
    //        {
    //            dr = dt.NewRow();
    //            //dr["Recipe_Operation_Id"] = tempId;
    //            dr["Recipe_Operation_Id"] = 0;
    //            positionNo = String.Format("{0:0000}", i * 10);
    //            dr["Operation_Phase"] = positionNo;
    //            dr["DFlagValue"] = "I";
    //            //dr["Relevancy_To_Costing"] = "";
    //            dr["ChargeQty"] = "1";
    //            dr["OperQty"] = "1";
    //            dr["ChargeUnit"] = ddlRheaderUnit.SelectedValue;
    //            dr["OperUnit"] = ddlRheaderUnit.SelectedValue;
    //            dt.Rows.Add(dr);

    //            for (int j = 1; j <= 1; j++)
    //            {
    //                dr = dt.NewRow();
    //                //dr["Recipe_Operation_Id"] = tempId;
    //                dr["Recipe_Operation_Id"] = 0;
    //                positionNo = String.Format("{0:0000}", (i * 10) + 1);
    //                dr["Operation_Phase"] = positionNo;
    //                dr["DFlagValue"] = "I";
    //                //dr["Relevancy_To_Costing"] = "X";
    //                dr["ChargeQty"] = "1";
    //                dr["OperQty"] = "1";
    //                dr["ChargeUnit"] = ddlRheaderUnit.SelectedValue;
    //                dr["OperUnit"] = ddlRheaderUnit.SelectedValue;
    //                dt.Rows.Add(dr);
    //            }
    //        }


    //        dstData.Tables.Add(dt);
    //        dstData.AcceptChanges();

    //        GvOperation.DataSource = dstData.Tables[0].DefaultView;
    //        if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
    //            GvOperation.Columns[9].Visible = false;
    //        else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
    //            GvOperation.Columns[10].Visible = false;
    //        GvOperation.DataBind();
    //        //DisableOperationField();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void AddBlankRowOperation()
    //{
    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dstData = new DataSet();

    //    int tempId = 1;
    //    string positionNo = string.Empty;
    //    try
    //    {
    //        dt.Columns.Add(new DataColumn("Recipe_Operation_Id"));
    //        dt.Columns.Add(new DataColumn("Select"));
    //        dt.Columns.Add(new DataColumn("Operation_Phase"));
    //        dt.Columns.Add(new DataColumn("Phase_Indicator"));
    //        dt.Columns.Add(new DataColumn("Sup_Operation"));
    //        dt.Columns.Add(new DataColumn("Destinatn"));
    //        dt.Columns.Add(new DataColumn("Resource"));
    //        dt.Columns.Add(new DataColumn("Control_key"));
    //        dt.Columns.Add(new DataColumn("StdText_Key"));
    //        dt.Columns.Add(new DataColumn("Description"));
    //        dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));

    //        //dt.Columns.Add(new DataColumn("Relevancy_to_Costing_Indicator"));

    //        dt.Columns.Add(new DataColumn("Base_Quantity"));
    //        dt.Columns.Add(new DataColumn("Act_Operation_UoM"));
    //        dt.Columns.Add(new DataColumn("First_Std_Value"));
    //        dt.Columns.Add(new DataColumn("First_Std_Value_Unit"));

    //        dt.Columns.Add(new DataColumn("Sec_Std_Value"));
    //        dt.Columns.Add(new DataColumn("Sec_Std_Value_Unit"));

    //        dt.Columns.Add(new DataColumn("Third_Std_Value"));
    //        dt.Columns.Add(new DataColumn("Third_Std_Value_Unit"));

    //        dt.Columns.Add(new DataColumn("Plant"));
    //        dt.Columns.Add(new DataColumn("ChargeQty"));
    //        dt.Columns.Add(new DataColumn("OperQty"));
    //        dt.Columns.Add(new DataColumn("ChargeUnit"));
    //        dt.Columns.Add(new DataColumn("OperUnit"));

    //        dt.Columns.Add(new DataColumn("DeletionFlag"));
    //        dt.Columns.Add(new DataColumn("DFlagValue"));

    //        foreach (GridViewRow row in GvOperation.Rows)
    //        {
    //            dr = dt.NewRow();
    //            dr["Recipe_Operation_Id"] = (row.FindControl("lblRecipe_Operation_Id") as Label).Text;
    //            dr["Select"] = (row.FindControl("chkSelect") as CheckBox).Checked ? "True" : "false";
    //            dr["Operation_Phase"] = (row.FindControl("txtOperation_Phase") as TextBox).Text;
    //            dr["Phase_Indicator"] = (row.FindControl("chkPI") as CheckBox).Checked ? "True" : "false";
    //            dr["Sup_Operation"] = (row.FindControl("txtSup_Operation") as TextBox).Text;
    //            dr["Destinatn"] = (row.FindControl("lblDestination") as Label).Text;
    //            dr["Resource"] = (row.FindControl("ddlResource") as DropDownList).SelectedValue;
    //            dr["Control_key"] = (row.FindControl("ddlControlkey") as DropDownList).SelectedValue;
    //            dr["StdText_Key"] = (row.FindControl("ddlStdTextKey") as DropDownList).SelectedValue;

    //            dr["Description"] = (row.FindControl("txtDescription") as TextBox).Text;
    //            dr["Relevancy_To_Costing"] = (row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList).SelectedValue;

    //            //["Relevancy_to_Costing_Indicator"] = (row.FindControl("ChckRCI") as CheckBox).Checked ? "True" : "false";

    //            dr["Base_Quantity"] = (row.FindControl("txtBase_Quantity") as TextBox).Text;
    //            dr["Act_Operation_UoM"] = (row.FindControl("ddlAct_Operation_UoM") as DropDownList).SelectedValue;
    //            dr["First_Std_Value"] = (row.FindControl("txtFirst_Std_Value") as TextBox).Text;
    //            dr["First_Std_Value_Unit"] = (row.FindControl("txtFirst_Std_Value_Unit") as TextBox).Text;

    //            dr["Sec_Std_Value"] = (row.FindControl("txtSec_Std_Value") as TextBox).Text;
    //            dr["Sec_Std_Value_Unit"] = (row.FindControl("txtSec_Std_Value_Unit") as TextBox).Text;

    //            dr["Third_Std_Value"] = (row.FindControl("txtThird_Std_Value") as TextBox).Text;
    //            dr["Third_Std_Value_Unit"] = (row.FindControl("txtThird_Std_Value_Unit") as TextBox).Text;

    //            dr["Plant"] = (row.FindControl("ddlPlant") as DropDownList).SelectedValue;
    //            dr["ChargeQty"] = (row.FindControl("txtChargeQty") as TextBox).Text;
    //            dr["OperQty"] = (row.FindControl("txtOperQty") as TextBox).Text;
    //            dr["ChargeUnit"] = (row.FindControl("ddlChngeCUnit") as DropDownList).SelectedValue;
    //            dr["OperUnit"] = (row.FindControl("ddlChngeOUnit") as DropDownList).SelectedValue;

    //            dr["DeletionFlag"] = (row.FindControl("chkDeletionFlag") as CheckBox).Checked ? "True" : "false";
    //            dr["DFlagValue"] = (row.FindControl("lblDeleteFlagUDI") as Label).Text;

    //            dt.Rows.Add(dr);
    //            if (!string.IsNullOrEmpty(Convert.ToString(dr["Operation_Phase"])))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Operation_Phase"]));
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //            }
    //            int iResult = Convert.ToInt32(positionNo) / 10;
    //            (row.FindControl("chkPI") as CheckBox).Enabled = (iResult % 2 == 0) ? false : true;
    //            //if (rdoListRecipeType.SelectedValue == "F")
    //            //{
    //            //    CheckBox chkFD = (row.FindControl("chkFD") as CheckBox);
    //            //    chkFD.Enabled = false;
    //            //    //dr["Flex_duration"] = (row.FindControl("chkFD") as CheckBox).Enabled = false;
    //            //    //(row.FindControl("txtChange_Number") as TextBox).Text = "10";
    //            //}
    //            tempId += 1;
    //        }

    //        for (int i = 1; i <= 2; i++)
    //        {
    //            dr = dt.NewRow();
    //            //dr["Recipe_Operation_Id"] = tempId;
    //            dr["Recipe_Operation_Id"] = 0;
    //            dr["DFlagValue"] = "I";
    //            //dr["Relevancy_To_Costing"] = "";
    //            dr["ChargeQty"] = "1";
    //            dr["OperQty"] = "1";
    //            dr["ChargeUnit"] = ddlRheaderUnit.SelectedValue;
    //            dr["OperUnit"] = ddlRheaderUnit.SelectedValue;

    //            if (!string.IsNullOrEmpty(positionNo))
    //            {
    //                positionNo = String.Format("{0:0000}", ((Convert.ToInt32(positionNo) / 10) * 10) + 10);
    //                dr["Operation_Phase"] = positionNo;
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //                dr["Operation_Phase"] = positionNo;
    //            }
    //            dt.Rows.Add(dr);

    //            for (int j = 1; j <= 1; j++)
    //            {
    //                dr = dt.NewRow();
    //                //dr["Recipe_Operation_Id"] = tempId;
    //                dr["Recipe_Operation_Id"] = 0;
    //                positionNo = String.Format("{0:0000}", ((Convert.ToInt32(positionNo) / 10) * 10) + 1);
    //                dr["Operation_Phase"] = positionNo;
    //                dr["DFlagValue"] = "I";
    //                //dr["Relevancy_To_Costing"] = "X";
    //                dr["ChargeQty"] = "1";
    //                dr["OperQty"] = "1";
    //                dr["ChargeUnit"] = ddlRheaderUnit.SelectedValue;
    //                dr["OperUnit"] = ddlRheaderUnit.SelectedValue;
    //                dt.Rows.Add(dr);
    //            }

    //        }

    //        dstData.Tables.Add(dt);
    //        dstData.AcceptChanges();

    //        GvOperation.DataSource = dstData.Tables[0].DefaultView;
    //        if (Session[StaticKeys.PlantType].ToString() == "P")  //for API plants hide Standard Text column
    //            GvOperation.Columns[9].Visible = false;
    //        else if (Session[StaticKeys.PlantType].ToString() == "F")  //for Formulation plants hide Operation Text column
    //            GvOperation.Columns[10].Visible = false;
    //        GvOperation.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    ////Bind lost values again.
    //SetBaseQuantityOperation(txtBQty.Text);
    //SetActUOMOperationAdd();
    //foreach (GridViewRow grow in GvOperation.Rows)
    //{
    //    SetupValidation(((CheckBox)grow.FindControl("chkPI")).Checked, grow.RowIndex);
    //    if (((DropDownList)grow.FindControl("ddlResource")).SelectedValue != "" && ((CheckBox)grow.FindControl("chkPI")).Checked)
    //    {
    //        string stdValKey = objRecipeAccess.GetOprRescStdUnits(((DropDownList)grow.FindControl("ddlResource")).SelectedValue, ddlPlant.SelectedValue);
    //        SetValidationForOperStdKey(stdValKey, grow.RowIndex);
    //    }
    //}

    ////BindResource();
    ////BindControlKey();
    ////BindBOMOperationPhase();
    ////DisableOperationField();
    //}

    //private int GetOperationDataFilledRows()
    //{
    //    int lstRow = 0;
    //    List<int> lstOperDataFilled = new List<int>();
    //    for (int i = 0; i < GvOperation.Rows.Count; i++)
    //    {
    //        CheckBox chkPI = (CheckBox)GvOperation.Rows[i].FindControl("chkPI");
    //        if (chkPI.Checked)
    //            lstOperDataFilled.Add(i);
    //    }
    //    if (lstOperDataFilled.Count() > 0)
    //        lstRow = lstOperDataFilled.Max();
    //    return lstRow;
    //}

    //private RecipeOperations GetOperationData(GridViewRow row)
    //{
    //    RecipeOperations objRcpOperations = new RecipeOperations();
    //    Utility objUtil = new Utility();

    //    Label lblRecipe_Operation_Id = row.FindControl("lblRecipe_Operation_Id") as Label;
    //    TextBox txtOperation_Phase = row.FindControl("txtOperation_Phase") as TextBox;
    //    CheckBox chkPI = row.FindControl("chkPI") as CheckBox;
    //    TextBox txtSup_Operation = row.FindControl("txtSup_Operation") as TextBox;
    //    Label lblDestination = row.FindControl("lblDestination") as Label;
    //    DropDownList ddlResource = row.FindControl("ddlResource") as DropDownList;
    //    DropDownList ddlControl_key = row.FindControl("ddlControlkey") as DropDownList;
    //    DropDownList ddlStdTextKey = row.FindControl("ddlStdTextKey") as DropDownList;

    //    TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
    //    DropDownList ddlIndicatorRelavancyToCosting = row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList;

    //    //CheckBox ChckRCI = row.FindControl("ChckRCI") as CheckBox;
    //    TextBox txtBase_Quantity = row.FindControl("txtBase_Quantity") as TextBox;
    //    DropDownList ddlAct_Operation_UoM = row.FindControl("ddlAct_Operation_UoM") as DropDownList;

    //    TextBox txtFirst_Std_Value = row.FindControl("txtFirst_Std_Value") as TextBox;
    //    TextBox txtFirst_Std_Value_Unit = row.FindControl("txtFirst_Std_Value_Unit") as TextBox;

    //    TextBox txtSec_Std_Value = row.FindControl("txtSec_Std_Value") as TextBox;
    //    TextBox txtSec_Std_Value_Unit = row.FindControl("txtSec_Std_Value_Unit") as TextBox;

    //    TextBox txtThird_Std_Value = row.FindControl("txtThird_Std_Value") as TextBox;
    //    TextBox txtThird_Std_Value_Unit = row.FindControl("txtThird_Std_Value_Unit") as TextBox;


    //    DropDownList ddlPlant = row.FindControl("ddlPlant") as DropDownList;

    //    TextBox txtChargeQty = row.FindControl("txtChargeQty") as TextBox;
    //    TextBox txtOperQty = row.FindControl("txtOperQty") as TextBox;

    //    DropDownList ddlChngeCUnit = row.FindControl("ddlChngeCUnit") as DropDownList;
    //    DropDownList ddlChngeOUnit = row.FindControl("ddlChngeOUnit") as DropDownList;

    //    CheckBox chkDeletionFlag = row.FindControl("chkDeletionFlag") as CheckBox;
    //    Label lblDeleteFlagUDI = row.FindControl("lblDeleteFlagUDI") as Label;
    //    if (lblDeleteFlagUDI.Text == "U")
    //    {
    //        lblDeleteFlagUDI.Text = chkDeletionFlag.Checked ? "D" : "U";
    //    }


    //    objRcpOperations.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
    //    objRcpOperations.Recipe_Operation_Id = Convert.ToInt32(lblRecipe_Operation_Id.Text);

    //    objRcpOperations.Operation_Phase = txtOperation_Phase.Text;
    //    objRcpOperations.Phase_Indicator = chkPI.Checked ? "True" : "False";
    //    objRcpOperations.Sup_Operation = txtSup_Operation.Text;
    //    objRcpOperations.Destinatn = lblDestination.Text;
    //    objRcpOperations.Resource = ddlResource.SelectedValue;
    //    objRcpOperations.Control_key = ddlControl_key.SelectedValue;
    //    objRcpOperations.StdText_Key = ddlStdTextKey.SelectedValue;

    //    objRcpOperations.Description = txtDescription.Text;
    //    objRcpOperations.Relevancy_To_Costing = ddlIndicatorRelavancyToCosting.SelectedValue;
    //    //Relevancy_to_Costing_Indicator= ChckRCI.Checked ? "True" : "False");  //""
    //    objRcpOperations.Base_Quantity = txtBase_Quantity.Text;
    //    objRcpOperations.Act_Operation_UoM = ddlAct_Operation_UoM.SelectedValue;

    //    objRcpOperations.First_Std_Value = txtFirst_Std_Value.Text == "" ? "0" : txtFirst_Std_Value.Text;
    //    objRcpOperations.First_Std_Value_Unit = txtFirst_Std_Value_Unit.Text;

    //    objRcpOperations.Sec_Std_Value = txtSec_Std_Value.Text == "" ? "0" : txtSec_Std_Value.Text;
    //    objRcpOperations.Sec_Std_Value_Unit = txtSec_Std_Value_Unit.Text;

    //    objRcpOperations.Third_Std_Value = txtThird_Std_Value.Text == "" ? "0" : txtThird_Std_Value.Text;
    //    objRcpOperations.Third_Std_Value_Unit = txtThird_Std_Value_Unit.Text;

    //    objRcpOperations.Plant_Id = ddlPlant.SelectedValue;
    //    objRcpOperations.ChargeQty = txtChargeQty.Text;
    //    objRcpOperations.OperQty = txtOperQty.Text;
    //    objRcpOperations.ChargeUnit = ddlChngeCUnit.SelectedValue;
    //    objRcpOperations.OperUnit = ddlChngeOUnit.SelectedValue;
    //    objRcpOperations.DeletionFlag = lblDeleteFlagUDI.Text;

    //    objRcpOperations.UserId = lblUserId.Text;
    //    objRcpOperations.TodayDate = objUtil.GetDate();
    //    objRcpOperations.IPAddress = objUtil.GetIpAddress();
    //    objRcpOperations.Mode = lblMode.Text;

    //    return objRcpOperations;
    //}

    //private void SetBaseQuantityOperation(string strBaseQty)
    //{
    //    for (int i = 0; i < GvOperation.Rows.Count; i++)
    //    {
    //        TextBox txtBaseQty = (TextBox)GvOperation.Rows[i].FindControl("txtBase_Quantity");
    //        if (txtBaseQty.Text == "")
    //            txtBaseQty.Text = strBaseQty;
    //    }
    //}

    //private void SetActUOMOperation()
    //{
    //    if (ddlRheaderUnit.SelectedValue != "")
    //    {
    //        for (int i = 0; i < GvOperation.Rows.Count; i++)
    //        {
    //            DropDownList ddlActUOM = (DropDownList)GvOperation.Rows[i].FindControl("ddlAct_Operation_UoM");
    //            helperAccess.PopuplateDropDownList(ddlActUOM, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
    //            //if (ddlActUOM.SelectedValue == "")
    //                ddlActUOM.SelectedValue = ddlRheaderUnit.SelectedValue;

    //            DropDownList ddlChngeCUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeCUnit");
    //            helperAccess.PopuplateDropDownList(ddlChngeCUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

    //            DropDownList ddlChngeOUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeOUnit");
    //            helperAccess.PopuplateDropDownList(ddlChngeOUnit, "proc_AutoComplete_Material_UOM '" + txtMaterialNmbr.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

    //            if (ddlChngeCUnit.SelectedValue == "")
    //                ddlChngeCUnit.SelectedValue = ddlRheaderUnit.SelectedValue;
    //            if (ddlChngeOUnit.SelectedValue == "")
    //                ddlChngeOUnit.SelectedValue = ddlActUOM.SelectedValue;
    //        }
    //    }
    //}

    //private void SetActUOMOperationAdd()
    //{
    //    if (ddlRheaderUnit.SelectedValue != "")
    //    {
    //        for (int i = 0; i < GvOperation.Rows.Count; i++)
    //        {
    //            DropDownList ddlActUOM = (DropDownList)GvOperation.Rows[i].FindControl("ddlAct_Operation_UoM");
    //            if (ddlActUOM.SelectedValue == "")
    //                ddlActUOM.SelectedValue = ddlRheaderUnit.SelectedValue;

    //            DropDownList ddlChngeCUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeCUnit");
    //            DropDownList ddlChngeOUnit = (DropDownList)GvOperation.Rows[i].FindControl("ddlChngeOUnit");

    //            if (ddlChngeCUnit.SelectedValue == "")
    //                ddlChngeCUnit.SelectedValue = ddlRheaderUnit.SelectedValue;
    //            if (ddlChngeOUnit.SelectedValue == "")
    //                ddlChngeOUnit.SelectedValue = ddlActUOM.SelectedValue;
    //        }
    //    }
    //}

    //private void UpdatePICheckedState(int rowNo)
    //{
    //    bool flg = false;
    //    CheckBox chkPI = (CheckBox)GvOperation.Rows[rowNo].FindControl("chkPI");
    //    CheckBox chkSelect = (CheckBox)GvOperation.Rows[rowNo].FindControl("chkSelect");
    //    Label lblDestination = (Label)GvOperation.Rows[rowNo].FindControl("lblDestination");
    //    DropDownList ddlIndicatorRelavancyToCosting = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlIndicatorRelavancyToCosting");

    //    TextBox txtFirst_Std_Value = (TextBox)GvOperation.Rows[rowNo].FindControl("txtFirst_Std_Value");
    //    TextBox txtFirst_Std_Value_Unit = (TextBox)GvOperation.Rows[rowNo].FindControl("txtFirst_Std_Value_Unit");
    //    TextBox txtSec_Std_Value = (TextBox)GvOperation.Rows[rowNo].FindControl("txtSec_Std_Value");
    //    TextBox txtSec_Std_Value_Unit = (TextBox)GvOperation.Rows[rowNo].FindControl("txtSec_Std_Value_Unit");
    //    TextBox txtThird_Std_Value = (TextBox)GvOperation.Rows[rowNo].FindControl("txtThird_Std_Value");
    //    TextBox txtThird_Std_Value_Unit = (TextBox)GvOperation.Rows[rowNo].FindControl("txtThird_Std_Value_Unit");
    //    TextBox txtSup_Operation = (TextBox)GvOperation.Rows[rowNo].FindControl("txtSup_Operation");
    //    DropDownList ddlResource = (DropDownList)GvOperation.Rows[rowNo].FindControl("ddlResource");

    //    if (chkPI.Checked)
    //    {
    //        lblDestination.Text = "01";
    //        ddlIndicatorRelavancyToCosting.SelectedValue = "X";
    //        if (txtSup_Operation.Text == "")
    //        {
    //            List<int> prevCheckd = new List<int>();
    //            for (int i = 0; i <= rowNo - 1; i++)
    //            {
    //                if ((((CheckBox)GvOperation.Rows[i].FindControl("chkPI")).Checked) == false)
    //                    prevCheckd.Add(i);
    //            }
    //            int newRowNo = prevCheckd.Max();
    //            txtSup_Operation.Text = ((TextBox)GvOperation.Rows[newRowNo].FindControl("txtOperation_Phase")).Text;
    //        }
    //        if (txtSup_Operation.Text != "")
    //        {
    //            string resource = BindResourceFromSupOper(txtSup_Operation.Text.ToString());
    //            ddlResource.SelectedValue = resource;
    //        }
    //        ddlResource.Enabled = false;
    //        chkSelect.Enabled = true;
    //        flg = true;
    //    }
    //    else
    //    {
    //        lblDestination.Text = "";
    //        ddlIndicatorRelavancyToCosting.SelectedValue = "";
    //        txtSup_Operation.Text = "";
    //        ddlResource.SelectedValue = "";
    //        ddlResource.Enabled = true;
    //        chkSelect.Enabled = false;
    //        flg = false;
    //    }

    //    if (flg == false)
    //        txtFirst_Std_Value.Text = txtFirst_Std_Value_Unit.Text = txtSec_Std_Value.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value.Text = txtThird_Std_Value_Unit.Text = "";
    //    else
    //    {
    //        if (ddlResource.SelectedValue != "" && chkPI.Checked)
    //        {
    //            string stdValKey = objRecipeAccess.GetOprRescStdUnits(ddlResource.SelectedValue, ddlPlant.SelectedValue);
    //            if (stdValKey != "")
    //            {
    //                SetValidationForOperStdKey(stdValKey, rowNo);
    //            }
    //        }
    //        //txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value_Unit.Text = "HR";
    //    }
    //    SetupValidation(chkPI.Checked, rowNo);
    //    AddOperationForInspection();
    //    AddOperationForSecRes();
    //}

    //private void SetupValidation(bool statusPI, int rowNo)
    //{
    //    RequiredFieldValidator reqddlResource = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlResource");
    //    RequiredFieldValidator reqddlControlKey = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlControlKey");
    //    RequiredFieldValidator reqtxtBase_Quantity = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqtxtBase_Quantity");
    //    RequiredFieldValidator reqddlAct_Operation_UoM = (RequiredFieldValidator)GvOperation.Rows[rowNo].FindControl("reqddlAct_Operation_UoM");
    //    reqddlResource.Enabled = statusPI;
    //    reqddlControlKey.Enabled = statusPI;
    //    reqtxtBase_Quantity.Enabled = statusPI;
    //    reqddlAct_Operation_UoM.Enabled = statusPI;
    //}

    //private string BindResourceFromSupOper(string supOperation)
    //{
    //    string resource = "";
    //    foreach (GridViewRow row in GvOperation.Rows)
    //    {
    //        TextBox txtOperation_Phase = (TextBox)row.FindControl("txtOperation_Phase");
    //        if (txtOperation_Phase.Text.ToString() == supOperation)
    //        {
    //            DropDownList ddlResource = (DropDownList)row.FindControl("ddlResource");
    //            resource = ddlResource.SelectedValue.ToString();
    //        }
    //    }
    //    return resource;
    //}

    //private void SetValidationForOperStdKey(string stdValKey, int grdRowNo)
    //{
    //    TextBox txtFirst_Std_Value = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtFirst_Std_Value");
    //    TextBox txtSec_Std_Value = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtSec_Std_Value");
    //    TextBox txtThird_Std_Value = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtThird_Std_Value");

    //    TextBox txtFirst_Std_Value_Unit = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtFirst_Std_Value_Unit");
    //    TextBox txtSec_Std_Value_Unit = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtSec_Std_Value_Unit");
    //    TextBox txtThird_Std_Value_Unit = (TextBox)GvOperation.Rows[grdRowNo].FindControl("txtThird_Std_Value_Unit");

    //    txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value_Unit.Text = "";

    //    if (stdValKey == "SAP6")
    //    {
    //        txtFirst_Std_Value.Enabled = true;
    //        txtSec_Std_Value.Enabled = true;
    //        txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = "HR";
    //    }
    //    else if (stdValKey == "ZAP5")
    //    {
    //        txtFirst_Std_Value.Enabled = true;
    //        txtSec_Std_Value.Enabled = true;
    //        txtThird_Std_Value.Enabled = true;
    //        txtFirst_Std_Value_Unit.Text = txtSec_Std_Value_Unit.Text = txtThird_Std_Value_Unit.Text = "HR";
    //    }
    //    else
    //    {
    //        txtFirst_Std_Value.Enabled = true;
    //        txtFirst_Std_Value_Unit.Text = "HR";
    //    }
    //}

    //private bool ValidateOperations()
    //{
    //    bool flg = false;
    //    int lstRow = GetOperationDataFilledRows();
    //    if (lstRow > 0)
    //        flg = true;
    //    return flg;
    //}
    //End Commented on 30.05.2018
    #endregion

    #region Inspection Characteristics
    //Start Commented on 30.05.2018
    //private void AddBlankRowInspectionChara()
    //{
    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dsInspChara = new DataSet();
    //    int tempId = 1;
    //    string positionNo = string.Empty;
    //    string OperpositionNo = "";

    //    try
    //    {
    //        dt.Columns.Add(new DataColumn("Operation_Phase"));
    //        dt.Columns.Add(new DataColumn("Recipe_InspChara_Id"));
    //        dt.Columns.Add(new DataColumn("Characteristic_No"));
    //        dt.Columns.Add(new DataColumn("MIC"));
    //        dt.Columns.Add(new DataColumn("Sampling_Procedure"));
    //        dt.Columns.Add(new DataColumn("CodeGrp"));
    //        //start Commented By nitish Rao 28/03/2018
    //        //dt.Columns.Add(new DataColumn("InspPtCmpt"));
    //        dt.Columns.Add(new DataColumn("NoRelation"));
    //        //End Commented By nitish rao 28/03/2018

    //        foreach (GridViewRow row in gvInspChara.Rows)
    //        {
    //            dr = dt.NewRow();
    //            dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
    //            dr["Recipe_InspChara_Id"] = (row.FindControl("lblRecipe_InspChara_Id") as Label).Text;
    //            dr["Characteristic_No"] = (row.FindControl("txtCharacteristicNo") as TextBox).Text;
    //            //dr["MIC"] = (row.FindControl("ddlMIC") as DropDownList).SelectedValue;
    //            dr["MIC"] = (row.FindControl("txtMIC") as TextBox).Text;
    //            //dr["Sampling_Procedure"] = (row.FindControl("ddlSamplingProcedure") as DropDownList).SelectedValue;
    //            dr["Sampling_Procedure"] = ddlSamplingProceduremain.SelectedValue;
    //            dr["CodeGrp"] = (row.FindControl("txtCodeGrp") as TextBox).Text;
    //            //start Commented By nitish Rao 28/03/2018
    //            //dr["InspPtCmpt"] = (row.FindControl("ddlInspPtCmpt") as DropDownList).SelectedValue;
    //            ///dr["InspPtCmpt"] = ddlInspPtCmptmain.SelectedValue;
    //            //End Commented By nitish rao 28/03/2018

    //            dr["NoRelation"] = (row.FindControl("chkNoRelation") as CheckBox).Checked ? "True" : "false";

    //            dt.Rows.Add(dr);
    //            tempId += 1;
    //            if (!string.IsNullOrEmpty(Convert.ToString(dr["Characteristic_No"])))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Characteristic_No"]));
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //            }
    //            if (ddlOperationPhase.SelectedValue == (row.FindControl("txtOperationPhase") as TextBox).Text)
    //            {
    //                OperpositionNo = positionNo;
    //            }
    //        }
    //        positionNo = OperpositionNo;
    //        for (int i = 1; i <= Convert.ToInt32(txtAddRowInsp.Text); i++)
    //        {
    //            dr = dt.NewRow();
    //            //dr["Recipe_InspChara_Id"] = tempId;
    //            dr["Recipe_InspChara_Id"] = 0;
    //            //dr["DFlagValue"] = "I";
    //            if (ddlOperationPhase.SelectedValue != "Select")
    //            {
    //                dr["Operation_Phase"] = ddlOperationPhase.SelectedItem;
    //                //positionNo = OperpositionNo;
    //            }
    //            if (!string.IsNullOrEmpty(positionNo))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
    //                dr["Characteristic_No"] = positionNo;
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //                dr["Characteristic_No"] = positionNo;
    //            }
    //            dr["Sampling_Procedure"] = ddlSamplingProceduremain.SelectedValue;
    //            dt.Rows.Add(dr);
    //        }
    //        dsInspChara.Tables.Add(dt);
    //        dsInspChara.AcceptChanges();
    //        gvInspChara.DataSource = dsInspChara.Tables[0].DefaultView;
    //        gvInspChara.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void BindOperationPhase()
    //{
    //    lstOperationPhase.Sort();
    //    ddlOperationPhase.DataSource = lstOperationPhase.Distinct().ToList();
    //    ddlOperationPhase.DataBind();

    //    //ddlInspPoints.SelectedValue = "";
    //    //ddlPartialLot.SelectedValue = "";
    //}

    //private void DeleteGrdRow(int currentRow)
    //{
    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dstData = new DataSet();
    //    try
    //    {
    //        dt.Columns.Add(new DataColumn("Operation_Phase"));
    //        dt.Columns.Add(new DataColumn("Recipe_InspChara_Id"));
    //        dt.Columns.Add(new DataColumn("Characteristic_No"));
    //        dt.Columns.Add(new DataColumn("MIC"));
    //        dt.Columns.Add(new DataColumn("Sampling_Procedure"));
    //        dt.Columns.Add(new DataColumn("CodeGrp"));
    //        //start Commented By nitish Rao 28/03/2018
    //        //dt.Columns.Add(new DataColumn("InspPtCmpt"));
    //        //End Commented By nitish rao 28/03/2018
    //        dt.Columns.Add(new DataColumn("NoRelation"));

    //        foreach (GridViewRow row in gvInspChara.Rows)
    //        {
    //            dr = dt.NewRow();
    //            dr["Operation_Phase"] = (row.FindControl("txtOperationPhase") as TextBox).Text;
    //            dr["Recipe_InspChara_Id"] = (row.FindControl("lblRecipe_InspChara_Id") as Label).Text;
    //            dr["Characteristic_No"] = (row.FindControl("txtCharacteristicNo") as TextBox).Text;
    //            //dr["MIC"] = (row.FindControl("ddlMIC") as DropDownList).SelectedValue;
    //            dr["MIC"] = (row.FindControl("txtMIC") as TextBox).Text;
    //            dr["Sampling_Procedure"] = (row.FindControl("ddlSamplingProcedure") as DropDownList).SelectedValue;
    //            dr["CodeGrp"] = (row.FindControl("txtCodeGrp") as TextBox).Text;
    //            //start Commented By nitish Rao 28/03/2018
    //            //dr["InspPtCmpt"] = (row.FindControl("ddlInspPtCmpt") as DropDownList).SelectedValue;
    //            //dr["InspPtCmpt"] = ddlInspPtCmptmain.SelectedValue;
    //            //End Commented By nitish rao 28/03/2018
    //            dr["NoRelation"] = (row.FindControl("chkNoRelation") as CheckBox).Checked ? "True" : "false";

    //            dt.Rows.Add(dr);
    //        }
    //        dstData.Tables.Add(dt);
    //        dstData.Tables[0].Rows[currentRow].Delete();
    //        dstData.AcceptChanges();

    //        gvInspChara.DataSource = dstData.Tables[0].DefaultView;
    //        gvInspChara.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private RecipeInspChara GetInspCharData(GridViewRow row)
    //{
    //    RecipeInspChara objRcpInsChar = new RecipeInspChara();
    //    Utility objUtil = new Utility();

    //    Label lblRecipe_InspChara_Id = row.FindControl("lblRecipe_InspChara_Id") as Label;
    //    TextBox txtOperationPhase = row.FindControl("txtOperationPhase") as TextBox;

    //    TextBox txtCharacteristicNo = row.FindControl("txtCharacteristicNo") as TextBox;

    //    //DropDownList ddlMIC = row.FindControl("ddlMIC") as DropDownList;
    //    TextBox txtMIC = row.FindControl("txtMIC") as TextBox;
    //    DropDownList ddlSamplingProcedure = row.FindControl("ddlSamplingProcedure") as DropDownList;
    //    TextBox txtCodeGrp = row.FindControl("txtCodeGrp") as TextBox;
    //    //start Commented By nitish Rao 28/03/2018
    //    //DropDownList ddlInspPtCmpt = row.FindControl("ddlInspPtCmpt") as DropDownList;
    //    //End Commented By nitish rao 28/03/2018
    //    CheckBox chkNoRelation = row.FindControl("chkNoRelation") as CheckBox;

    //    objRcpInsChar.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
    //    objRcpInsChar.Recipe_InspChara_Id = Convert.ToInt32(lblRecipe_InspChara_Id.Text);

    //    objRcpInsChar.Operation_Phase = txtOperationPhase.Text;
    //    objRcpInsChar.Characteristic_No = txtCharacteristicNo.Text;
    //    //objRcpInsChar.Master_Insp_Char_Code = ddlMIC.SelectedValue;
    //    objRcpInsChar.Master_Insp_Char_Code = txtMIC.Text;
    //    objRcpInsChar.Sampling_Procedure = ddlSamplingProcedure.SelectedValue;
    //    objRcpInsChar.CodeGrp = txtCodeGrp.Text;
    //    //start Commented By nitish Rao 28/03/2018
    //    //objRcpInsChar.InspPtCmpt = ddlInspPtCmpt.SelectedValue;
    //    objRcpInsChar.InspPtCmpt = ddlInspPtCmptmain.SelectedValue;
    //    //End Commented By nitish rao 28/03/2018

    //    objRcpInsChar.NoRelation = chkNoRelation.Checked == true ? "1" : "0";

    //    objRcpInsChar.UserId = lblUserId.Text;
    //    objRcpInsChar.TodayDate = objUtil.GetDate();
    //    objRcpInsChar.IPAddress = objUtil.GetIpAddress();
    //    objRcpInsChar.Mode = lblMode.Text;

    //    return objRcpInsChar;
    //}

    //private void AddOperationForInspection()
    //{
    //    lstOperationPhase.Clear();
    //    int cnt = 0;
    //    //Add operation phase in ddl in inspection chara.
    //    foreach (GridViewRow rowop in GvOperation.Rows)
    //    {
    //        CheckBox chkPI = (CheckBox)rowop.FindControl("chkPI");
    //        DropDownList ddlControlKey = (DropDownList)rowop.FindControl("ddlControlKey");
    //        string strOperPhas = (rowop.FindControl("txtOperation_Phase") as TextBox).Text;
    //        if (chkPI.Checked && (ddlControlKey.SelectedValue == "PI02" || ddlControlKey.SelectedValue == "PI03"))
    //        {
    //            lstOperationPhase.Add(strOperPhas);
    //            cnt++;
    //        }
    //    }
    //    if (cnt > 0)
    //    {
    //        ddlInspPoints.SelectedValue = "Z03";
    //        ddlPartialLot.SelectedValue = "0";
    //    }
    //    else
    //    {
    //        ddlInspPoints.SelectedValue = "";
    //        ddlPartialLot.SelectedValue = "";
    //    }

    //    BindOperationPhase();
    //}


    //private void SetupValidationForInspChar(int rowNo)
    //{   //Start Commented By nitish rao 28/03/2018
    //    //RequiredFieldValidator reqddlInspPtCmpt = (RequiredFieldValidator)gvInspChara.Rows[rowNo].FindControl("reqddlInspPtCmpt");
    //    //DropDownList ddlInspPtCmpt = (DropDownList)gvInspChara.Rows[rowNo].FindControl("ddlInspPtCmpt");
    //    //End Commented By nitish rao 28/03/2018
    //    CheckBox chkNoRelation = (CheckBox)gvInspChara.Rows[rowNo].FindControl("chkNoRelation");
    //    if (ddlInspPoints.SelectedValue != "" && ddlPartialLot.SelectedValue != "-1")
    //    {
    //        if (ddlInspPoints.SelectedValue == "Z03" && ddlPartialLot.SelectedValue == "0")
    //        {
    //            //Start Commented By nitish rao 28/03/2018
    //            //ddlInspPtCmpt.SelectedValue = "Valuation";
    //            //reqddlInspPtCmpt.Enabled = true;
    //            reqddlInspPtCmptmain.Enabled = true;
    //            ddlInspPtCmptmain.SelectedValue = "Valuation";
    //            //End Commented By nitish rao 28/03/2018

    //            chkNoRelation.Checked = true;
    //        }
    //        else
    //        {
    //            //Start Commented By nitish rao 28/03/2018
    //            //ddlInspPtCmpt.SelectedValue = "";
    //            //reqddlInspPtCmpt.Enabled = false;
    //            reqddlInspPtCmptmain.Enabled = false;
    //            ddlInspPtCmptmain.SelectedValue = "";
    //            //End Commented By nitish rao 28/03/2018
    //            chkNoRelation.Checked = false;
    //        }
    //    }
    //    else
    //    {
    //        //Start Commented By nitish rao 28/03/2018
    //        //ddlInspPtCmpt.SelectedValue = "";
    //        //reqddlInspPtCmpt.Enabled = false;
    //        reqddlInspPtCmptmain.Enabled = false;
    //        ddlInspPtCmptmain.SelectedValue = "";
    //        //End Commented By nitish rao 28/03/2018
    //        chkNoRelation.Checked = false;
    //    }
    //}

    //private bool SaveInspCharForQA()
    //{
    //    bool flg = false;
    //    foreach (GridViewRow row in gvInspChara.Rows)
    //    {
    //        RecipeInspChara objRcpInsChar = new RecipeInspChara();
    //        objRcpInsChar = GetInspCharData(row);
    //        if (objRecipeAccess.SaveInspCharDetails(objRcpInsChar) > 0)
    //        {
    //            flg = true;
    //        }
    //        else
    //            flg = false;
    //    }
    //    return flg;
    //}

    //private bool SaveSecResDetails()
    //{
    //    bool flg = false;
    //    foreach (GridViewRow row in grdSecResources.Rows)
    //    {
    //        RecipeSecRes objRcpSecRes = new RecipeSecRes();
    //        objRcpSecRes = GetSecResources(row);
    //        if (objRecipeAccess.SaveSecResDetails(objRcpSecRes) > 0)
    //        {
    //            flg = true;
    //        }
    //        else
    //            flg = false;
    //    }
    //    return flg;
    //}
    //End Commented on 30.05.2018
    #endregion

    #region Secondary Resource
    //Start Commented on 30.05.2018
    //private void AddBlankRowSecondaryResources()
    //{
    //    DataRow dr;
    //    DataTable dt = new DataTable();
    //    DataSet dsSecRes = new DataSet();
    //    int tempId = 1;
    //    string positionNo = string.Empty;
    //    string OperpositionNo = "";

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

    //        foreach (GridViewRow row in grdSecResources.Rows)
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
    //            tempId += 1;
    //            if (!string.IsNullOrEmpty(Convert.ToString(dr["SecResource_Item"])))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["SecResource_Item"]));
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //            }
    //            if (ddlSecRes.SelectedValue == (row.FindControl("txtOperationPhase") as TextBox).Text)
    //            {
    //                OperpositionNo = positionNo;
    //            }
    //        }
    //        positionNo = OperpositionNo;
    //        for (int i = 1; i <= Convert.ToInt32(txtNoSecRes.Text); i++)
    //        {
    //            dr = dt.NewRow();
    //            //dr["Recipe_SecResource_Id"] = tempId;
    //            dr["Recipe_SecResource_Id"] = 0;
    //            //dr["DFlagValue"] = "I";
    //            if (ddlSecRes.SelectedValue != "Select")
    //            {
    //                dr["Operation_Phase"] = ddlSecRes.SelectedItem;
    //                //positionNo = OperpositionNo;
    //            }
    //            if (!string.IsNullOrEmpty(positionNo))
    //            {
    //                positionNo = String.Format("{0:0000}", Convert.ToInt32(positionNo) + 10);
    //                dr["SecResource_Item"] = positionNo;
    //            }
    //            else
    //            {
    //                positionNo = String.Format("{0:0000}", 10);
    //                dr["SecResource_Item"] = positionNo;
    //            }

    //            dt.Rows.Add(dr);
    //        }
    //        dsSecRes.Tables.Add(dt);
    //        dsSecRes.AcceptChanges();
    //        grdSecResources.DataSource = dsSecRes.Tables[0].DefaultView;
    //        grdSecResources.DataBind();

    //        foreach (GridViewRow grow in grdSecResources.Rows)
    //        {
    //            if (((DropDownList)grow.FindControl("ddlSecResource")).SelectedValue != "")
    //            {
    //                RecipeSecRes objSecRes = objRecipeAccess.GetDataByResourceName(((DropDownList)grow.FindControl("ddlSecResource")).SelectedValue, ddlPlant.SelectedValue);
    //                SetValidationForSecRes(objSecRes.StdValKey, grow.RowIndex);
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void BindSecResOperations()
    //{
    //    lstSecResPhase.Sort();
    //    ddlSecRes.DataSource = lstSecResPhase.Distinct().ToList();
    //    ddlSecRes.DataBind();
    //}

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

    //private RecipeSecRes GetSecResources(GridViewRow row)
    //{
    //    RecipeSecRes objRcpSecRes = new RecipeSecRes();
    //    Utility objUtil = new Utility();

    //    Label lblRecipe_SecResource_Id = row.FindControl("lblRecipe_SecResource_Id") as Label;
    //    TextBox txtOperationPhase = row.FindControl("txtOperationPhase") as TextBox;
    //    TextBox txtSecRecItem = row.FindControl("txtSecRecItem") as TextBox;
    //    DropDownList ddlSecResource = row.FindControl("ddlSecResource") as DropDownList;
    //    TextBox txtDuration = row.FindControl("txtDuration") as TextBox;
    //    TextBox txtUnit1 = row.FindControl("txtUnit1") as TextBox;
    //    TextBox txtActivityType1 = row.FindControl("txtActivityType1") as TextBox;
    //    TextBox txtProcess = row.FindControl("txtProcess") as TextBox;
    //    TextBox txtUnit2 = row.FindControl("txtUnit2") as TextBox;
    //    TextBox txtActivityType2 = row.FindControl("txtActivityType2") as TextBox;
    //    TextBox txtLabor = row.FindControl("txtLabor") as TextBox;
    //    TextBox txtUnit3 = row.FindControl("txtUnit3") as TextBox;
    //    TextBox txtActivityType3 = row.FindControl("txtActivityType3") as TextBox;


    //    objRcpSecRes.Recipe_HeaderID = Convert.ToInt32(lblRecipeId.Text);
    //    objRcpSecRes.Recipe_SecResource_Id = Convert.ToInt32(lblRecipe_SecResource_Id.Text);

    //    objRcpSecRes.Operation_Phase = txtOperationPhase.Text;
    //    objRcpSecRes.SecResource_Item = txtSecRecItem.Text;
    //    objRcpSecRes.SecResource = ddlSecResource.SelectedValue;
    //    objRcpSecRes.Duration = txtDuration.Text;
    //    objRcpSecRes.Unit1 = txtUnit1.Text;
    //    objRcpSecRes.ActivityType1 = txtActivityType1.Text;
    //    objRcpSecRes.Process = txtProcess.Text;
    //    objRcpSecRes.Unit2 = txtUnit2.Text;
    //    objRcpSecRes.ActivityType2 = txtActivityType2.Text;
    //    objRcpSecRes.Labor = txtLabor.Text;
    //    objRcpSecRes.Unit3 = txtUnit3.Text;
    //    objRcpSecRes.ActivityType3 = txtActivityType3.Text;

    //    objRcpSecRes.UserId = lblUserId.Text;
    //    objRcpSecRes.TodayDate = objUtil.GetDate();
    //    objRcpSecRes.IPAddress = objUtil.GetIpAddress();
    //    objRcpSecRes.Mode = lblMode.Text;

    //    return objRcpSecRes;
    //}

    //private void AddOperationForSecRes()
    //{
    //    lstSecResPhase.Clear();
    //    int row = GetOperationDataFilledRows();
    //    string strOperPhas = "";
    //    if (row > 0)
    //        strOperPhas = (GvOperation.Rows[row].FindControl("txtOperation_Phase") as TextBox).Text;
    //    lstSecResPhase.Add(strOperPhas);
    //    BindSecResOperations();
    //}

    //private void SetValidationForSecRes(string stdValKey, int grdRowNo)
    //{
    //    RequiredFieldValidator reqtxtDuration = (RequiredFieldValidator)grdSecResources.Rows[grdRowNo].FindControl("reqtxtDuration");
    //    RequiredFieldValidator reqtxtProcess = (RequiredFieldValidator)grdSecResources.Rows[grdRowNo].FindControl("reqtxtProcess");
    //    RequiredFieldValidator reqtxtLabor = (RequiredFieldValidator)grdSecResources.Rows[grdRowNo].FindControl("reqtxtLabor");
    //    TextBox txtLabor = (TextBox)grdSecResources.Rows[grdRowNo].FindControl("txtLabor");
    //    TextBox txtProcess = (TextBox)grdSecResources.Rows[grdRowNo].FindControl("txtProcess");
    //    TextBox txtDuration = (TextBox)grdSecResources.Rows[grdRowNo].FindControl("txtDuration");

    //    //if (stdValKey == "SAP6")
    //    //{
    //    //    txtDuration.Enabled = true;
    //    //    reqtxtDuration.Enabled = true;
    //    //    txtProcess.Enabled = true;
    //    //    reqtxtProcess.Enabled = true;
    //    //}
    //    if (stdValKey == "ZAP5")
    //    {
    //        txtDuration.Enabled = true;
    //        reqtxtDuration.Enabled = true;
    //        txtProcess.Enabled = true;
    //        //reqtxtProcess.Enabled = true;
    //        txtLabor.Enabled = true;
    //        reqtxtLabor.Enabled = true;
    //    }
    //    else
    //    {
    //        txtProcess.Enabled = true;
    //        reqtxtProcess.Enabled = true;
    //    }
    //}
    //End Commented on 30.05.2018
    #endregion

    #region Prod. Version

    //private void FillProdVersion()
    //{
    //    helperAccess.PopuplateDropDownList(ddlDPTaskList, "pr_GetDropDownListByControlNameModuleType 'R','ddlDPTaskList'", "LookUp_Desc", "LookUp_Code", "");
    //    helperAccess.PopuplateDropDownList(ddlLock, "pr_GetDropDownListByControlNameModuleType 'R','ddlLock'", "LookUp_Desc", "LookUp_Code", "0");
    //    ProdVersion objProdVer = GetProdVersionData();
    //    try
    //    {
    //        if (objProdVer.ProdVersion_Id > 0)
    //        {
    //            lblProdVersionID.Text = objProdVer.ProdVersion_Id.ToString();
    //            txtMaterialNo.Text = objProdVer.MaterialNo;
    //            lblMatDesc.Text = objProdVer.MaterialDesc;
    //            txtProdVersion.Text = objProdVer.ProdVersionNo;
    //            txtProdVerDesc.Text = objProdVer.ProdVersion_Text;
    //            ddlLock.SelectedValue = objProdVer.Lock;
    //            txtProdFrom.Text = objProdVer.ProdFrom;
    //            txtProdTo.Text = objProdVer.ProdTo;
    //            txtProdUnit.Text = objProdVer.ProdUnit;
    //            txtProdVFrom.Text = objProdVer.ValidFrom;
    //            txtProdVTo.Text = objProdVer.ValidTo;
    //            ddlDPTaskList.SelectedValue = objProdVer.TaskListType;
    //            txtDPGroup.Text = objProdVer.RecipeGroup;
    //            txtDPGroupCntr.Text = objProdVer.GroupCntr;
    //            txtProdAltBOM.Text = objProdVer.AltBOM;
    //            txtProdBOMUsage.Text = objProdVer.BOMUsage;
    //        }
    //        else
    //        {
    //            lblProdVersionID.Text = "0";
    //            txtMaterialNo.Text = txtMaterialNmbr.Text;
    //            lblMatDesc.Text = txtProdVerDesc.Text = txtMaterialDescription.Text;
    //            ddlDPTaskList.SelectedValue = "2";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private ProdVersion GetProdVersionData()
    //{
    //    return objRecipeAccess.GetProdVersionData(lblMasterHeaderId.Text);
    //}

    //private ProdVersion GetProdVerControlsValue()
    //{
    //    ProdVersion ObjProdVersion = new ProdVersion();
    //    Utility objUtil = new Utility();

    //    ObjProdVersion.ProdVersion_Id = Convert.ToInt32(lblProdVersionID.Text);
    //    ObjProdVersion.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
    //    ObjProdVersion.MaterialNo = txtMaterialNo.Text;
    //    ObjProdVersion.MaterialDesc = lblMatDesc.Text;
    //    ObjProdVersion.ProdVersionNo = txtProdVersion.Text;
    //    ObjProdVersion.ProdVersion_Text = txtProdVerDesc.Text;
    //    ObjProdVersion.Lock = ddlLock.SelectedValue;
    //    ObjProdVersion.ProdFrom = txtProdFrom.Text;
    //    ObjProdVersion.ProdTo = txtProdFrom.Text;
    //    ObjProdVersion.ProdUnit = txtProdUnit.Text;
    //    ObjProdVersion.ValidFrom = string.IsNullOrEmpty(txtProdVFrom.Text) ? "" : objUtil.GetYYYYMMDD(txtProdVFrom.Text);
    //    ObjProdVersion.ValidTo = objUtil.GetYYYYMMDD(txtProdVTo.Text);
    //    ObjProdVersion.TaskListType = ddlDPTaskList.SelectedValue;
    //    ObjProdVersion.RecipeGroup = txtDPGroup.Text;
    //    ObjProdVersion.GroupCntr = txtDPGroupCntr.Text;
    //    ObjProdVersion.AltBOM = txtProdAltBOM.Text;
    //    ObjProdVersion.BOMUsage = txtProdBOMUsage.Text;

    //    ObjProdVersion.UserId = lblUserId.Text;
    //    ObjProdVersion.TodayDate = objUtil.GetDate();
    //    ObjProdVersion.IPAddress = objUtil.GetIpAddress();
    //    ObjProdVersion.Mode = lblMode.Text;

    //    return ObjProdVersion;
    //}

    //private bool SaveProdVersion()
    //{
    //    bool flg = false;

    //    try
    //    {
    //        ProdVersion ObjProdVersion = GetProdVerControlsValue();
    //        if (objRecipeAccess.SaveProdVersionData(ObjProdVersion) > 0)
    //        {
    //            flg = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return flg;
    //}

    #endregion

    #region Material HELP

    private void ReadMaterialHelp()
    {
        DataSet dstData = new DataSet();
        try
        {
            dstData = BOMAccess.ReadMaterialHelp(txtMaterialNoHELP.Text.Trim(), txtMaterialDescHELP.Text.Trim(), ddlPlantHELP.SelectedValue, txtBOMusage.Text.Trim(), txtAltBOM.Text.Trim());

            //if (dstData.Tables[0].Rows.Count < 1)
            //{
                CallWebService(txtMaterialNoHELP.Text.Trim());
                dstData = BOMAccess.ReadMaterialHelp(txtMaterialNoHELP.Text.Trim(), txtMaterialDescHELP.Text.Trim(), ddlPlantHELP.SelectedValue, txtBOMusage.Text.Trim(), txtAltBOM.Text.Trim());

                grdMaterialHELP.DataSource = dstData.Tables[0].DefaultView;
                grdMaterialHELP.DataBind();
            //}
            //else
            //{ 
            //    grdMaterialHELP.DataSource = dstData.Tables[0].DefaultView;
            //    grdMaterialHELP.DataBind();
            //}
        }
        catch (Exception ex)
        {
            _log.Error("ReadMaterialHelp", ex);
        }
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

            var output = service.GetMaterialDetailsForBOMWSL(smaterial);

            //var output = service.GetResourcesDetailsForBOMWSL(smaterial);

            if (output.msgdialog == "Done")
            {
                WriteMatChangeLog("MaterialDetailsForBOM" + sdate + ".txt", output.msgdialog);
            }
            else
            {
                WriteMatChangeLog("MaterialDetailsForBOM" + sdate + ".txt", output.msgdialog);
            }

            }
        catch (Exception ex)
        {
            _log.Error("CallWebService1", ex);
            WriteMatChangeLog("MaterialDetailsForBOM" + sdate + ".txt", ex.ToString());
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
    #endregion

    #endregion


    //protected void ddlSamplingProceduremain_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //    for (int i = 0; i < gvInspChara.Rows.Count; i++)
    //    {

    //        DropDownList ddlSamplingProcedure = (DropDownList)gvInspChara.Rows[i].FindControl("ddlSamplingProcedure");


    //            ddlSamplingProcedure.SelectedValue = ddlSamplingProceduremain.SelectedValue;


    //    }
    //}


}