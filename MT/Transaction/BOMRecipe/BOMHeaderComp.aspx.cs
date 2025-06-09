using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data.OleDb;
using System.Web.Configuration;
using System.IO;
using log4net;
using System.Data.SqlClient;
public partial class Transaction_BOM_BOMHeaderComp : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    BOMAccess objBOMAccess = new BOMAccess();
    RecipeAccess objRecipeAccess = new RecipeAccess();
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
                        bool flag = false;
                        //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                        if ((checkPrevioussection() && deptId == "0" && moduleId == "186") || (checkPrevioussection() && deptId == "0" && moduleId == "227") || (checkPrevioussection() && deptId == "0" && moduleId == "229"))
                        {
                            flag = true;
                        }
                        else if (deptId != "0")
                        {
                            flag = true;
                        }
                        else if (moduleId == "187" || moduleId == "180" || moduleId == "228")
                        {
                            flag = true;
                        }
                        if (flag == true)
                        {
                            HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                            PopuplateDropDownList();
                            FillBOMData(mode);

                            //ITSM413605

                            grdAttachedDocs.Columns[1].Visible = false;
                            trSecResfile.Visible = false;
                            trButtonuf.Visible = false;
                            trgrdAtt.Visible = false;
                            //ITSM413605


                            ddlPlantHELP.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                            if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M") && (Session[StaticKeys.RequestStatus].ToString() != "P"))
                            {
                                trButton.Visible = true;
                                btnSave.Visible = !btnNext.Visible;
                                if (Session[StaticKeys.SelectedModuleId].ToString() != "187")
                                {
                                    if (!btnPrevious.Visible && !btnNext.Visible)
                                        btnSave.Visible = false;
                                }

                                trAddNewBOMRow.Visible = true;


                                //ITSM413605

                                trButtonuf.Visible = true;
                                trSecResfile.Visible = true;
                                trgrdAtt.Visible = false;

                                //trButtonuf.Visible = true;
                                //btnMSProcess.Visible = true;
                                ////btnSRValidation.Visible = false;
                                //btnSRSubmit.Visible = false;

                                //trSecResfile.Visible = true;
                                //grdAttachedDocs.Columns[1].Visible = true;

                                //DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(), 70);

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
                                btnSave.Visible = true;
                                btnNext.Visible = false;
                                trAddNewBOMRow.Visible = true;
                            }
                            //BOM_NWF_EDT05072019
                            if ((mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27" && Session[StaticKeys.MaterialPlantId].ToString() == "19")
                            {
                                trButton.Visible = true;
                                btnSave.Visible = true;
                                btnNext.Visible = false;
                                trAddNewBOMRow.Visible = true;
                            }
                            ConfigureBOMScreen();
                        }
                        else
                        {
                            Type cstype = this.GetType();
                            ClientScriptManager cs = Page.ClientScript;
                            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                            {
                                String cstext = "alert('";
                                cstext += "Please Fill Reciepe Header Details in Reciepe section or Operation details in Operation section ');";

                                //cstext += "Please select the request(s) and click on Mass Submit to send the request(s) for Mass processing.');";

                                //String cstext = "if(confirm('Is request processing Complete?')){RequestSubmitPage();};";
                                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                                Response.Redirect("Reciepe.aspx");
                            }

                        }
                    }

                    //ITSM413605
                    hlMSImportFormat.NavigateUrl = "~/Transaction/BOMRecipe/UploadFormat/Bom.xlsx";
                    //ITSM413605

                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    public Boolean checkPrevioussection()
    {
        bool flg = false;
        try
        {
            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (objRecipeAccess.CheckAllRecipeDetail(lblMasterHeaderId.Text) > 0)
            {
                flg = true;
            }
        }
        catch (Exception ex)
        { _log.Error("checkPrevioussection", ex); }
        return flg;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            AddBlankRow();
        }
        catch (Exception ex)
        { _log.Error("btnAdd_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateComponent())
            {
                if (ValidateCompBaseQty())
                {
                    if (ValidateFixedQty())
                    {
                        if (SaveHeader())
                        {
                            if (SaveDetail())
                            {
                                FillBOMData(lblMode.Text);
                                lblMsg.Text = Messages.GetMessage(1);
                                pnlMsg.CssClass = "success";
                                pnlMsg.Visible = true;
                            }
                            else
                            {
                                lblMsg.Text = "Error while saving Details";
                                pnlMsg.CssClass = "error";
                                pnlMsg.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Error while saving Details";
                            pnlMsg.CssClass = "error";
                            pnlMsg.Visible = true;
                        }
                    }
                    else
                    {
                        pnlMsg.Visible = true;
                        lblMsg.Text = "Fixed quantity not supported for alternative items";
                        pnlMsg.CssClass = "error";
                    }
                }
                else
                {
                    pnlMsg.Visible = true;
                    lblMsg.Text = "Component base quantity cannot be zero.";
                    pnlMsg.CssClass = "error";
                }
            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Please enter atleast one component";
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void grdBOMDetailAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
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
                    dt.Columns.Add(new DataColumn("BOM_HeaderDetail_Id"));
                    dt.Columns.Add(new DataColumn("Postion_No"));
                    dt.Columns.Add(new DataColumn("Comtype"));
                    dt.Columns.Add(new DataColumn("Item_Category"));
                    dt.Columns.Add(new DataColumn("Component"));
                    dt.Columns.Add(new DataColumn("Component_desc"));
                    dt.Columns.Add(new DataColumn("Quantity"));
                    dt.Columns.Add(new DataColumn("Component_UOM"));
                    dt.Columns.Add(new DataColumn("Comp_SortString1"));
                    dt.Columns.Add(new DataColumn("Qty_Is_Fixed1"));
                    dt.Columns.Add(new DataColumn("Spare_Part_Indicator"));
                    dt.Columns.Add(new DataColumn("StorageLocation"));
                    dt.Columns.Add(new DataColumn("Alt_Item_Group"));
                    dt.Columns.Add(new DataColumn("Priority"));
                    dt.Columns.Add(new DataColumn("starategy"));
                    dt.Columns.Add(new DataColumn("Usage_Probebilty"));
                    dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));
                    dt.Columns.Add(new DataColumn("Remarks"));
                    dt.Columns.Add(new DataColumn("ASM"));
                    dt.Columns.Add(new DataColumn("Phantom_Indicator"));
                    dt.Columns.Add(new DataColumn("Component_Scrap"));
                    dt.Columns.Add(new DataColumn("RecursiveBOM"));
                    dt.Columns.Add(new DataColumn("Valid_From"));
                    dt.Columns.Add(new DataColumn("Valid_to"));
                    dt.Columns.Add(new DataColumn("BOM_Item_Text1"));
                    dt.Columns.Add(new DataColumn("BOM_Item_Text2"));
                    dt.Columns.Add(new DataColumn("ActiveFiller"));
                    dt.Columns.Add(new DataColumn("Combination"));
                    dt.Columns.Add(new DataColumn("Upd_Flag"));
                    dt.Columns.Add(new DataColumn("Item_node_number"));

                    foreach (GridViewRow row in grdBOMDetailAdd.Rows)
                    {
                        dr = dt.NewRow();
                        Label lblUpd_Flag = row.FindControl("lblUpd_Flag") as Label;
                        dr["BOM_HeaderDetail_Id"] = (row.FindControl("lblBomDetailId") as Label).Text;
                        dr["Postion_No"] = (row.FindControl("txtPositionNumber") as TextBox).Text;
                        dr["Comtype"] = (row.FindControl("hdnCompType") as TextBox).Text;
                        dr["Item_Category"] = (row.FindControl("ddlItemCategory") as DropDownList).SelectedValue;
                        dr["Component"] = (row.FindControl("txtComponent") as TextBox).Text;
                        dr["Component_desc"] = (row.FindControl("txtComponentDesc") as TextBox).Text;
                        dr["Quantity"] = (row.FindControl("txtQuantity") as TextBox).Text;
                        //dr["Component_UOM"] = (row.FindControl("txtCompenentUnitOfMeasure") as TextBox).Text;
                        dr["Component_UOM"] = (row.FindControl("ddlCompenentUnitOfMeasure") as DropDownList).SelectedValue;
                        dr["Comp_SortString1"] = (row.FindControl("ddlCompSortString") as DropDownList).SelectedValue;

                        //if (hdnBOMType.Value != "6")
                        //{
                        dr["Qty_Is_Fixed1"] = (row.FindControl("chkQtyIsFixed") as CheckBox).Checked ? "True" : "false";
                        //}
                        //if (hdnBOMType.Value == "6")
                        //{
                        //    dr["Qty_Is_Fixed"] = (row.FindControl("hdnQtyIsFixed") as HiddenField).Value;
                        //}
                        dr["Spare_Part_Indicator"] = (row.FindControl("ddlSparePartIndicator") as DropDownList).SelectedValue;
                        dr["StorageLocation"] = (row.FindControl("ddlStorageLocation") as DropDownList).SelectedValue;
                        dr["Alt_Item_Group"] = (row.FindControl("ddlAlternativeItemGroup") as DropDownList).SelectedValue;
                        dr["Priority"] = (row.FindControl("txtPriority") as TextBox).Text;
                        dr["starategy"] = (row.FindControl("ddlStrategy") as DropDownList).SelectedValue;
                        dr["Usage_Probebilty"] = (row.FindControl("txtUsageProbability") as TextBox).Text;
                        dr["Relevancy_To_Costing"] = (row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList).SelectedValue;
                        dr["Remarks"] = (row.FindControl("txtRemarks") as TextBox).Text;
                        dr["ASM"] = (row.FindControl("chkASM") as CheckBox).Checked ? "True" : "false";
                        dr["Phantom_Indicator"] = (row.FindControl("hdnPhantomIndicator") as HiddenField).Value; //(row.FindControl("chkPhantomIndicator") as CheckBox).Checked ? "True" : "false";
                        dr["Component_Scrap"] = (row.FindControl("txtComponentScrap") as TextBox).Text;
                        dr["RecursiveBOM"] = (row.FindControl("chkRecursiveBOM") as CheckBox).Checked ? "True" : "false";
                        dr["Valid_From"] = (row.FindControl("txtValidFrom") as TextBox).Text;
                        dr["Valid_to"] = (row.FindControl("txtValidTo") as TextBox).Text;
                        dr["BOM_Item_Text1"] = (row.FindControl("txtBomItemText1") as TextBox).Text;
                        dr["BOM_Item_Text2"] = (row.FindControl("txtBomItemText2") as TextBox).Text;
                        dr["ActiveFiller"] = (row.FindControl("ddlActFil") as DropDownList).SelectedValue;
                        dr["Combination"] = (row.FindControl("txtComb") as TextBox).Text;
                        dr["Upd_Flag"] = currentRow == row ? "D" : (row.FindControl("lblUpd_Flag") as Label).Text;
                        dr["Item_node_number"] = (row.FindControl("hdnItemNode") as TextBox).Text;
                        if (currentRow == row)
                            lblUpd_Flag.Text = "D";
                        dt.Rows.Add(dr);

                        if (objBOMAccess.GetBOMDetail(dr["BOM_HeaderDetail_Id"].ToString(), lblBOMHeaderId.Text) > 0)
                        {
                            BOMDetail objBOMDetail = GetBOMDetailData(row);
                            objBOMAccess.SaveBOMDetails(objBOMDetail);
                        }
                    }

                    dstData.Tables.Add(dt);
                    dstData.AcceptChanges();

                    if ((currentRow.FindControl("lblUpd_Flag") as Label).Text == "I" || (currentRow.FindControl("lblUpd_Flag") as Label).Text == "U" || (currentRow.FindControl("lblUpd_Flag") as Label).Text == "D")
                    {
                        dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
                        dstData.AcceptChanges();
                    }

                    DataView dv = new DataView(dstData.Tables[0]);
                    dv.Sort = "Postion_No Asc";
                    DataTable dtSorted = dv.ToTable();

                    grdBOMDetailAdd.DataSource = dtSorted;
                    grdBOMDetailAdd.DataBind();

                    ViewState["dstBomDetail"] = dstData;
                }
                catch (Exception ex)
                {
                    //throw ex;
                    _log.Error("grdBOMDetailAdd_RowCommand1", ex);
                }
            }

        }
        catch (Exception ex)
        { _log.Error("grdBOMDetailAdd_RowCommand1", ex); }
        //string bomHeader = grdBOMDetailAdd.DataKeys[currentRow.RowIndex].Value.ToString();

        //if (e.CommandName == "D")
        //{
        //    try
        //    {
        //        Label lblBomDetailId = (Label)currentRow.FindControl("lblBomDetailId");
        //        int bomDetailId = Convert.ToInt32(lblBomDetailId.Text);
        //        int flg = objBOMAccess.GetBOMDetail(lblBomDetailId.Text, lblBOMHeaderId.Text);
        //        if (flg == 1)
        //        {
        //            int res = objBOMAccess.DeleteBOMDetailData(bomDetailId);
        //            if (res > 0)
        //            {
        //                FillBOMData(lblMode.Text);
        //                lblMsg.Text = "Line Item deleted successfully";
        //                pnlMsg.CssClass = "success";
        //                pnlMsg.Visible = true;
        //            }
        //            else
        //            {
        //                lblMsg.Text = "Error while deleting line item";
        //                pnlMsg.CssClass = "error";
        //                pnlMsg.Visible = true;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }

    protected void grdBOMDetailAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtComponent = (TextBox)e.Row.FindControl("txtComponent");

                DropDownList ddlItemCategory = (DropDownList)e.Row.FindControl("ddlItemCategory");
                helperAccess.PopuplateDropDownList(ddlItemCategory, "pr_GetDropDownListByControlNameModuleType 'B','ddlItemCategory'", "LookUp_Desc", "LookUp_Code", "");
                ddlItemCategory.SelectedValue = "L";
                ddlItemCategory.Enabled = false;

                DropDownList ddlSparePartIndicator = (DropDownList)e.Row.FindControl("ddlSparePartIndicator");
                helperAccess.PopuplateDropDownList(ddlSparePartIndicator, "pr_GetDropDownListByControlNameModuleType 'B','ddlSparePartIndicator'", "LookUp_Desc", "LookUp_Code", "");
                ddlSparePartIndicator.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[2].ToString();

                DropDownList ddlStorageLocation = (DropDownList)e.Row.FindControl("ddlStorageLocation");
                helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlStorageLocation.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[3].ToString();

                DropDownList ddlStrategy = (DropDownList)e.Row.FindControl("ddlStrategy");
                helperAccess.PopuplateDropDownList(ddlStrategy, "pr_GetDropDownListByControlNameModuleType 'B','ddlStrategy'", "LookUp_Desc", "LookUp_Code", "");
                ddlStrategy.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[4].ToString();

                DropDownList ddlIndicatorRelavancyToCosting = (DropDownList)e.Row.FindControl("ddlIndicatorRelavancyToCosting");
                helperAccess.PopuplateDropDownList(ddlIndicatorRelavancyToCosting, "pr_GetDropDownListByControlNameModuleType 'B','ddlIndicatorRelavancyToCosting'", "LookUp_Desc", "LookUp_Code");
                //ddlIndicatorRelavancyToCosting.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[5].ToString();
                ddlIndicatorRelavancyToCosting.SelectedValue = "X";
                ddlIndicatorRelavancyToCosting.Enabled = false;

                DropDownList ddlCompSortString = (DropDownList)e.Row.FindControl("ddlCompSortString");
                helperAccess.PopuplateDropDownList(ddlCompSortString, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlCompSortString'", "LookUp_Desc", "LookUp_Code", "");
                ddlCompSortString.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[6].ToString();

                DropDownList ddlAlternativeItemGroup = (DropDownList)e.Row.FindControl("ddlAlternativeItemGroup");
                helperAccess.PopuplateDropDownList(ddlAlternativeItemGroup, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlAlternativeItemGroup'", "LookUp_Desc", "LookUp_Code", "");
                ddlAlternativeItemGroup.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[7].ToString();

                DropDownList ddlCompenentUnitOfMeasure = (DropDownList)e.Row.FindControl("ddlCompenentUnitOfMeasure");
                helperAccess.PopuplateDropDownList(ddlCompenentUnitOfMeasure, "proc_AutoComplete_Material_UOM '" + txtComponent.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");
                ddlCompenentUnitOfMeasure.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[8].ToString();

                DropDownList ddlActFil = (DropDownList)e.Row.FindControl("ddlActFil");
                ddlActFil.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[9].ToString();

                CheckBox chkASM = e.Row.FindControl("chkASM") as CheckBox;
                CheckBox chkPhantomIndicator = e.Row.FindControl("chkPhantomIndicator") as CheckBox;
                CheckBox chkQtyIsFixed = e.Row.FindControl("chkQtyIsFixed") as CheckBox;
                CheckBox chkRecursiveBOM = e.Row.FindControl("chkRecursiveBOM") as CheckBox;

                HiddenField hdnASM = e.Row.FindControl("hdnASM") as HiddenField;
                HiddenField hdnPhantomIndicator = e.Row.FindControl("hdnPhantomIndicator") as HiddenField;
                HiddenField hdnQtyIsFixed = e.Row.FindControl("hdnQtyIsFixed") as HiddenField;
                HiddenField hdnRecursiveBOM = e.Row.FindControl("hdnRecursiveBOM") as HiddenField;

                chkASM.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnASM.Value);
                chkPhantomIndicator.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnPhantomIndicator.Value);
                chkQtyIsFixed.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnQtyIsFixed.Value);
                chkRecursiveBOM.Checked = SafeTypeHandling.ConvertStringToBoolean(hdnRecursiveBOM.Value);
            }

        }
        catch (Exception ex)
        { _log.Error("grdBOMDetailAdd_RowDataBound", ex); }
    }

    protected void imgHelpSearchMaterial_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblType.Text = "M";
            lblRow.Text = "-1";
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

                if (lblType.Text == "M")
                {
                    txtMaterialNo.Text = "";
                    txtMaterialNo.Text = lblMaterialNumberHELP.Text;
                    txtMaterialDesc.Text = lblMatDescHELP.Text;
                    ddlPlant.SelectedValue = lblPlantHELP.Text;
                    txtBaseUOM.Text = lblBaseUOM.Text;

                    txtMaterialNo.Focus();

                    ValidateMaterial();
                }
                else if (lblType.Text == "C")
                {
                    int rowNo = Convert.ToInt32(lblRow.Text.ToString());
                    foreach (GridViewRow grdRow in grdBOMDetailAdd.Rows)
                    {
                        if (grdRow.RowIndex == rowNo)
                        {
                            TextBox txtComponent = (TextBox)grdRow.FindControl("txtComponent");
                            TextBox txtComponentDesc = (TextBox)grdRow.FindControl("txtComponentDesc");
                            DropDownList ddlCompenentUnitOfMeasure = (DropDownList)grdRow.FindControl("ddlCompenentUnitOfMeasure");
                            helperAccess.PopuplateDropDownList(ddlCompenentUnitOfMeasure, "proc_AutoComplete_Material_UOM '" + lblMaterialNumberHELP.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

                            txtComponent.Text = "";
                            txtComponent.Text = lblMaterialNumberHELP.Text;
                            txtComponentDesc.Text = lblMatDescHELP.Text;
                            ddlCompenentUnitOfMeasure.SelectedValue = lblBaseUOM.Text.Trim();
                        }
                    }
                    ValidateComp(rowNo);
                }
                modMatSearch.Hide();
                pnlMsg.Visible = false;
                //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "180" && lblType.Text == "M") || (Session[StaticKeys.SelectedModuleId].ToString() == "228" && lblType.Text == "M"))
                {
                    Session[StaticKeys.BOMRecipeMatNo] = txtMaterialNo.Text;
                    Session[StaticKeys.BOMRecipeMatDesc] = txtMaterialDesc.Text;
                    Session[StaticKeys.BOMRecipeBUOM] = txtBaseUOM.Text;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("grdMaterialHELP_RowCommand", ex); }
    }

    protected void txtComponent_TextChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            TextBox txtComponent = (TextBox)row.FindControl("txtComponent");
            TextBox txtComponentDesc = (TextBox)row.FindControl("txtComponentDesc");
            CheckBox chkRecursiveBOM = (CheckBox)row.FindControl("chkRecursiveBOM");
            DropDownList ddlCompSortString = (DropDownList)row.FindControl("ddlCompSortString");
            //BOM_RSeriesDT_08072020 add || matGrp == "147"
            string matGrp = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtComponent.Text);
            if (matGrp == "162" || matGrp == "164" || matGrp == "139" || matGrp == "144" || matGrp == "171" || matGrp == "147")
            {
                DataSet dstData = BOMAccess.ReadMaterialHelp(txtComponent.Text.ToString(), "", ddlPlant.SelectedValue.ToString(), "", "");
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    txtComponentDesc.Text = dstData.Tables[0].Rows[0]["Material_Desc"].ToString();

                    DropDownList ddlCompenentUnitOfMeasure = (DropDownList)row.FindControl("ddlCompenentUnitOfMeasure");
                    helperAccess.PopuplateDropDownList(ddlCompenentUnitOfMeasure, "proc_AutoComplete_Material_UOM '" + txtComponent.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

                    ddlCompenentUnitOfMeasure.SelectedValue = dstData.Tables[0].Rows[0]["BaseUOM"].ToString().Trim();

                    if (txtComponent.Text == txtMaterialNo.Text)
                        chkRecursiveBOM.Checked = true;
                    else
                        chkRecursiveBOM.Checked = false;
                    helperAccess.PopuplateDropDownList(ddlCompSortString, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlCompSortString'", "LookUp_Desc", "LookUp_Code", "");
                    if (matGrp == "162" || matGrp == "144")
                    {
                        if (Session[StaticKeys.PlantType].ToString() == "F")
                            ddlCompSortString.SelectedValue = "KR";
                        else
                            ddlCompSortString.SelectedValue = "";
                    }
                    else
                        ddlCompSortString.SelectedValue = "";

                    pnlMsg.Visible = false;
                }
                else
                {
                    lblMsg.Text = "Material doesnot exist in the plant " + Session[StaticKeys.MaterialPlantName].ToString();
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";

                    txtComponentDesc.Text = "";
                    txtComponent.Text = "";
                    chkRecursiveBOM.Checked = false;
                    txtComponent.Focus();
                }
            }
            else
            {
                //BOM_RSeriesDT_08072020 Change MSG
                lblMsg.Text = "Please enter correct material code(3/4/8/1/2/7) series.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                txtComponent.Text = "";
                txtComponentDesc.Text = "";
                chkRecursiveBOM.Checked = false;
                txtComponent.Focus();
            }
        }
        catch (Exception ex)
        { _log.Error("txtComponent_TextChanged", ex); }
    }

    protected void txtMaterialNo_TextChanged(object sender, EventArgs e)
    {
        bool flg = false;
        try
        {
            if (txtMaterialNo.Text != "")
            {
                string matGrp = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialNo.Text);
                if (matGrp == "162" || matGrp == "164" || matGrp == "139" || matGrp == "171" || matGrp == "144")
                {
                    if (ddlBOMUsage.SelectedValue != "")
                    {
                        if (ddlBOMUsage.SelectedValue == "1" || ddlBOMUsage.SelectedValue == "6")
                        {
                            if (matGrp == "162" || matGrp == "164" || matGrp == "145" || matGrp == "138" || matGrp == "147" || matGrp == "170")
                            {
                                lblMsg.Text = "Select proper material code series (3/4/8 series)";
                                pnlMsg.Visible = true;
                                pnlMsg.CssClass = "error";
                            }
                            else
                            {
                                flg = true;
                            }
                        }
                        else if (ddlBOMUsage.SelectedValue == "3")
                        {
                            if (matGrp == "145" || matGrp == "138" || matGrp == "147" || matGrp == "170")
                            {
                                lblMsg.Text = "Select proper material code series (1/2/3/4/8 series)";
                                pnlMsg.Visible = true;
                                pnlMsg.CssClass = "error";
                            }
                            else
                            {
                                flg = true;
                            }
                        }
                    }
                    else
                        flg = true;
                }
                else
                {
                    lblMsg.Text = "Select proper material code series (1/2/3/4/8 series)";
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }

            }
            if (flg)
            {
                DataSet dstData = BOMAccess.ReadMaterialHelp(txtMaterialNo.Text.ToString(), "", ddlPlant.SelectedValue.ToString(), "", "");
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    txtMaterialDesc.Text = dstData.Tables[0].Rows[0]["Material_Desc"].ToString();
                    txtBaseUOM.Text = dstData.Tables[0].Rows[0]["BaseUOM"].ToString();
                    pnlMsg.Visible = false;
                }
                else
                {
                    txtMaterialDesc.Text = "";
                    txtBaseUOM.Text = "";
                    txtMaterialNo.Text = "";
                    txtMaterialNo.Focus();

                    lblMsg.Text = "Material doesnot exist in the plant " + Session[StaticKeys.MaterialPlantName].ToString();
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }
            }
            //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
            if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
            {
                Session[StaticKeys.BOMRecipeMatNo] = txtMaterialNo.Text;
                Session[StaticKeys.BOMRecipeMatDesc] = txtMaterialDesc.Text;
                Session[StaticKeys.BOMRecipeBUOM] = txtBaseUOM.Text;
            }
            UpdateRecursiveBOM();
        }
        catch (Exception ex)
        { _log.Error("txtMaterialNo_TextChanged", ex); }
    }

    protected void ddlBOMUsage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flg = false;

        try
        {
            if (txtMaterialNo.Text != "")
            {
                string matGrp = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialNo.Text);
                if (matGrp == "162" || matGrp == "164" || matGrp == "139" || matGrp == "171" || matGrp == "144")
                {
                    if (ddlBOMUsage.SelectedValue != "")
                    {
                        if (ddlBOMUsage.SelectedValue == "1" || ddlBOMUsage.SelectedValue == "6")
                        {
                            if (matGrp == "162" || matGrp == "164" || matGrp == "145" || matGrp == "138" || matGrp == "147" || matGrp == "170")
                            {
                                lblMsg.Text = "Select proper material code series (3/4/8 series)";
                                pnlMsg.Visible = true;
                                pnlMsg.CssClass = "error";
                            }
                            else
                            {
                                flg = true;
                            }
                        }
                        else if (ddlBOMUsage.SelectedValue == "3")
                        {
                            if (matGrp == "145" || matGrp == "138" || matGrp == "147" || matGrp == "170")
                            {
                                lblMsg.Text = "Select proper material code series (1/2/3/4/8 series)";
                                pnlMsg.Visible = true;
                                pnlMsg.CssClass = "error";
                            }
                            else
                            {
                                flg = true;
                            }
                        }
                    }
                    else
                        flg = true;
                }
                else
                {
                    lblMsg.Text = "Select proper material code series (1/2/3/4/8 series)";
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }

            }
            if (flg)
            {
                DataSet dstData = BOMAccess.ReadMaterialHelp(txtMaterialNo.Text.ToString(), "", ddlPlant.SelectedValue.ToString(), "", "");
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    txtMaterialDesc.Text = dstData.Tables[0].Rows[0]["Material_Desc"].ToString();
                    txtBaseUOM.Text = dstData.Tables[0].Rows[0]["BaseUOM"].ToString();
                    pnlMsg.Visible = false;
                }
                else
                {
                    txtMaterialDesc.Text = "";
                    txtBaseUOM.Text = "";
                    txtMaterialNo.Text = "";
                    txtMaterialNo.Focus();

                    lblMsg.Text = "Material doesnot exist in the plant " + Session[StaticKeys.MaterialPlantName].ToString();
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ddlBOMUsage_SelectedIndexChanged", ex); }
    }

    protected void imgHSrchMat_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            int index = row.RowIndex;
            lblType.Text = "C";
            lblRow.Text = index.ToString();
            modMatSearch.Show();
        }
        catch (Exception ex)
        { _log.Error("imgHSrchMat_Click", ex); }
    }

    protected void txtUsageProbability_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            TextBox txtUsageProbability = (TextBox)row.FindControl("txtUsageProbability");
            if (txtUsageProbability.Text != "" && Convert.ToInt32(txtUsageProbability.Text) > 0)
            {
                RequiredFieldValidator reqddlAlternativeItemGroup = (RequiredFieldValidator)row.FindControl("reqddlAlternativeItemGroup");
                reqddlAlternativeItemGroup.Enabled = true;
            }
        }
        catch (Exception ex)
        { _log.Error("txtUsageProbability_TextChanged", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateComponent())
            {
                if (ValidateCompBaseQty())
                {
                    if (ValidateFixedQty())
                    {
                        if (SaveHeader())
                        {
                            if (SaveDetail())
                            {
                                FillBOMData(lblMode.Text);
                                string pageURL = btnNext.CommandArgument.ToString();
                                Response.Redirect(pageURL);
                            }
                            else
                            {
                                lblMsg.Text = "Error while saving Details";
                                pnlMsg.CssClass = "error";
                                pnlMsg.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Error while saving Details";
                            pnlMsg.CssClass = "error";
                            pnlMsg.Visible = true;
                        }
                    }
                    else
                    {
                        pnlMsg.Visible = true;
                        lblMsg.Text = "Fixed quantity not supported for alternative items";
                        pnlMsg.CssClass = "error";
                    }
                }
                else
                {
                    pnlMsg.Visible = true;
                    lblMsg.Text = "Component base quantity cannot be zero.";
                    pnlMsg.CssClass = "error";
                }
            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Please enter atleast one component";
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateComponent())
            {
                if (ValidateCompBaseQty())
                {
                    if (ValidateFixedQty())
                    {
                        if (SaveHeader())
                        {
                            if (SaveDetail())
                            {
                                FillBOMData(lblMode.Text);
                                string pageURL = btnPrevious.CommandArgument.ToString();
                                Response.Redirect(pageURL);
                            }
                            else
                            {
                                lblMsg.Text = "Error while saving Details";
                                pnlMsg.CssClass = "error";
                                pnlMsg.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Error while saving Details";
                            pnlMsg.CssClass = "error";
                            pnlMsg.Visible = true;
                        }
                    }
                    else
                    {
                        pnlMsg.Visible = true;
                        lblMsg.Text = "Fixed quantity not supported for alternative items";
                        pnlMsg.CssClass = "error";
                    }
                }
                else
                {
                    pnlMsg.Visible = true;
                    lblMsg.Text = "Component base quantity cannot zero.";
                    pnlMsg.CssClass = "error";
                }

            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Please enter atleast one component";
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    //Start  Comment by Nitish Rao 15.02.2018 
    //Commented for , Alternative BOM will auto generate by SAP
    //protected void txtAlternativeBOM_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtAlternativeBOM.Text != "")
    //    {
    //        txtAlternativeBOM.Text = Convert.ToInt32(txtAlternativeBOM.Text).ToString("D2");
    //        ValidateAltBOMData();
    //    }
    //}
    //End  Comment by Nitish Rao 15.02.2018 
    #endregion

    #region Methods

    private void FillBOMData(string mode)
    {
        BOMHeader ObjBOMHeader = GetBOMHeaderData();
        try
        {
            if (ObjBOMHeader.BOM_HeaderID > 0)
            {
                lblBOMHeaderId.Text = ObjBOMHeader.BOM_HeaderID.ToString();
                txtMaterialNo.Text = ObjBOMHeader.Material_Number;
                txtMaterialDesc.Text = ObjBOMHeader.Material_Desc;

                ddlPlant.SelectedValue = ObjBOMHeader.Plant_Id;
                ddlBOMUsage.SelectedValue = ObjBOMHeader.BOM_Usage;
                txtAlternativeBOM.Text = ObjBOMHeader.AlternativeBOM;
                txtBOMText.Text = ObjBOMHeader.BOMText;
                txtAlternativeText.Text = ObjBOMHeader.AlternativeText;
                txtBaseQuantity.Text = ObjBOMHeader.BaseQty;
                txtBaseUOM.Text = ObjBOMHeader.BaseUOM;
                ddlBomStatus.SelectedValue = ObjBOMHeader.BOMStatus;
                txtFrom.Text = ObjBOMHeader.From;
                txtTo.Text = ObjBOMHeader.To;
                //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                txtBomRemark.Text = ObjBOMHeader.Remarks;
                txtBomReason.Text = ObjBOMHeader.Reason;
                //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
                //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                {
                    Session[StaticKeys.BOMRecipeMatNo] = txtMaterialNo.Text;
                    Session[StaticKeys.BOMRecipeMatDesc] = txtMaterialDesc.Text;
                    Session[StaticKeys.BOMRecipeBUOM] = txtBaseUOM.Text;
                }
                Session[StaticKeys.BOMRecipeAltBOM] = ObjBOMHeader.AlternativeBOM;
                Session[StaticKeys.BOMRecipeBOMUsage] = ObjBOMHeader.BOM_Usage;

                //DataSet dsBOMDetails = new DataSet();                           
                grdBOMDetailAdd.DataSource = objBOMAccess.GetBOMDetail(lblBOMHeaderId.Text);
                //for costing/ universal BOM formula is not applicable
                if (Session[StaticKeys.SelectedModuleId].ToString() == "187")
                {
                    grdBOMDetailAdd.Columns[22].Visible = false;
                    grdBOMDetailAdd.Columns[23].Visible = false;
                }
                grdBOMDetailAdd.DataBind();
                grdBOMDetailAdd.Visible = true;

            }
            else
            {
                lblBOMHeaderId.Text = "0";
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                //if (Session[StaticKeys.SelectedModuleId].ToString() == "186")
                //{
                //    ddlBOMUsage.SelectedValue = "1";
                //    txtMaterialNo.Text = Session[StaticKeys.BOMRecipeMatNo].ToString();
                //    txtMaterialDesc.Text = Session[StaticKeys.BOMRecipeMatDesc].ToString();
                //    txtBaseUOM.Text = Session[StaticKeys.BOMRecipeBUOM].ToString();
                //    txtBaseQuantity.Text = Session[StaticKeys.BOMRecipeBaseQty].ToString();
                //}
                //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                {
                    ddlBOMUsage.SelectedValue = "1";
                    Session[StaticKeys.BOMRecipeMatNo] = "";
                    Session[StaticKeys.BOMRecipeMatDesc] = "";
                    Session[StaticKeys.BOMRecipeBUOM] = "";
                    Session[StaticKeys.BOMRecipeBaseQty] = "";
                }
                Session[StaticKeys.BOMRecipeAltBOM] = "";
                Session[StaticKeys.BOMRecipeBOMUsage] = "";
                AddBlankRow();
                //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                {
                    ddlBOMUsage.SelectedValue = "1";
                    txtMaterialNo.Text = Session[StaticKeys.BOMRecipeMatNo].ToString();
                    txtMaterialDesc.Text = Session[StaticKeys.BOMRecipeMatDesc].ToString();
                    txtBaseUOM.Text = Session[StaticKeys.BOMRecipeBUOM].ToString();
                    txtBaseQuantity.Text = Session[StaticKeys.BOMRecipeBaseQty].ToString();
                }
            }
            //if (Session[StaticKeys.SelectedModuleId].ToString() == "186")
            //{
            //    ddlBOMUsage.SelectedValue = "1";
            //    txtMaterialNo.Text = Session[StaticKeys.BOMRecipeMatNo].ToString();
            //    txtMaterialDesc.Text = Session[StaticKeys.BOMRecipeMatDesc].ToString();
            //    txtBaseUOM.Text = Session[StaticKeys.BOMRecipeBUOM].ToString();
            //    txtBaseQuantity.Text = Session[StaticKeys.BOMRecipeBaseQty].ToString();
            //}
            UpdateRecursiveBOM();
            ConfigureHeaderControls();

        }
        catch (Exception ex)
        {
            _log.Error("FillBOMData", ex);
        }

    }

    private void ConfigureHeaderControls()
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {
                grdBOMDetailAdd.Enabled = false;

                //Start  Comment by Nitish Rao 15.02.2018 
                //Commented for , Alternative BOM will auto generate by SAP
                //txtAlternativeBOM.Enabled = false;
                //End  Comment by Nitish Rao 15.02.2018 
                txtBOMText.Enabled = false;
                txtAlternativeText.Enabled = false;
                txtBaseQuantity.Enabled = false;
            }
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
            {
                grdBOMDetailAdd.Enabled = false;
                txtBOMText.Enabled = false;
                txtAlternativeText.Enabled = false;
                txtBaseQuantity.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureHeaderControls", ex); }
    }

    private void UpdateRecursiveBOM()
    {
        try
        {
            if (grdBOMDetailAdd.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdBOMDetailAdd.Rows)
                {
                    TextBox txtComponent = (TextBox)row.FindControl("txtComponent");
                    CheckBox chkRecursiveBOM = (CheckBox)row.FindControl("chkRecursiveBOM");

                    if (txtComponent.Text != "" && txtMaterialNo.Text != "")
                    {
                        if (txtComponent.Text == txtMaterialNo.Text)
                            chkRecursiveBOM.Checked = true;
                        else
                            chkRecursiveBOM.Checked = false;
                    }
                    else
                        chkRecursiveBOM.Checked = false;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("UpdateRecursiveBOM", ex); }
    }

    private BOMHeader GetBOMHeaderData()
    {
        return objBOMAccess.GetBOMHeaderData(lblMasterHeaderId.Text);
    }

    private void AddBlankRow()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        int tempId = 1;
        int j = 0;
        string positionNo = "";
        try
        {
            //Columns
            dt.Columns.Add(new DataColumn("BOM_HeaderDetail_Id"));
            dt.Columns.Add(new DataColumn("Postion_No"));
            dt.Columns.Add(new DataColumn("Comtype"));
            dt.Columns.Add(new DataColumn("Item_Category"));
            dt.Columns.Add(new DataColumn("Component"));
            dt.Columns.Add(new DataColumn("Component_desc"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Component_UOM"));
            dt.Columns.Add(new DataColumn("Comp_SortString1"));
            dt.Columns.Add(new DataColumn("Qty_Is_Fixed1"));
            dt.Columns.Add(new DataColumn("Spare_Part_Indicator"));
            dt.Columns.Add(new DataColumn("StorageLocation"));
            dt.Columns.Add(new DataColumn("Alt_Item_Group"));
            dt.Columns.Add(new DataColumn("Priority"));
            dt.Columns.Add(new DataColumn("starategy"));
            dt.Columns.Add(new DataColumn("Usage_Probebilty"));
            dt.Columns.Add(new DataColumn("Relevancy_To_Costing"));
            dt.Columns.Add(new DataColumn("Remarks"));
            dt.Columns.Add(new DataColumn("ASM"));
            dt.Columns.Add(new DataColumn("Phantom_Indicator"));
            dt.Columns.Add(new DataColumn("Component_Scrap"));
            dt.Columns.Add(new DataColumn("RecursiveBOM"));
            dt.Columns.Add(new DataColumn("Valid_From"));
            dt.Columns.Add(new DataColumn("Valid_to"));
            dt.Columns.Add(new DataColumn("BOM_Item_Text1"));
            dt.Columns.Add(new DataColumn("BOM_Item_Text2"));
            dt.Columns.Add(new DataColumn("ActiveFiller"));
            dt.Columns.Add(new DataColumn("Combination"));
            dt.Columns.Add(new DataColumn("Upd_Flag"));
            dt.Columns.Add(new DataColumn("Item_node_number"));

            foreach (GridViewRow row in grdBOMDetailAdd.Rows)
            {
                dr = dt.NewRow();
                dr["BOM_HeaderDetail_Id"] = (row.FindControl("lblBomDetailId") as Label).Text;
                dr["Postion_No"] = (row.FindControl("txtPositionNumber") as TextBox).Text;
                dr["Comtype"] = (row.FindControl("hdnCompType") as TextBox).Text;
                dr["Item_Category"] = (row.FindControl("ddlItemCategory") as DropDownList).SelectedValue;
                dr["Component"] = (row.FindControl("txtComponent") as TextBox).Text;
                dr["Component_desc"] = (row.FindControl("txtComponentDesc") as TextBox).Text;
                dr["Quantity"] = (row.FindControl("txtQuantity") as TextBox).Text;
                //dr["Component_UOM"] = (row.FindControl("txtCompenentUnitOfMeasure") as TextBox).Text;
                dr["Component_UOM"] = (row.FindControl("ddlCompenentUnitOfMeasure") as DropDownList).SelectedValue;
                dr["Comp_SortString1"] = (row.FindControl("ddlCompSortString") as DropDownList).SelectedValue;

                //if (hdnBOMType.Value != "6")
                //{
                dr["Qty_Is_Fixed1"] = (row.FindControl("chkQtyIsFixed") as CheckBox).Checked ? "True" : "false";
                //}
                //if (hdnBOMType.Value == "6")
                //{
                //    dr["Qty_Is_Fixed"] = (row.FindControl("hdnQtyIsFixed") as HiddenField).Value;
                //}
                dr["Spare_Part_Indicator"] = (row.FindControl("ddlSparePartIndicator") as DropDownList).SelectedValue;
                dr["StorageLocation"] = (row.FindControl("ddlStorageLocation") as DropDownList).SelectedValue;
                dr["Alt_Item_Group"] = (row.FindControl("ddlAlternativeItemGroup") as DropDownList).SelectedValue;
                dr["Priority"] = (row.FindControl("txtPriority") as TextBox).Text;
                dr["starategy"] = (row.FindControl("ddlStrategy") as DropDownList).SelectedValue;
                dr["Usage_Probebilty"] = (row.FindControl("txtUsageProbability") as TextBox).Text;
                dr["Relevancy_To_Costing"] = (row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList).SelectedValue;
                dr["Remarks"] = (row.FindControl("txtRemarks") as TextBox).Text;
                dr["ASM"] = (row.FindControl("chkASM") as CheckBox).Checked ? "True" : "false";
                dr["Phantom_Indicator"] = (row.FindControl("hdnPhantomIndicator") as HiddenField).Value; //(row.FindControl("chkPhantomIndicator") as CheckBox).Checked ? "True" : "false";
                dr["Component_Scrap"] = (row.FindControl("txtComponentScrap") as TextBox).Text;
                dr["RecursiveBOM"] = (row.FindControl("chkRecursiveBOM") as CheckBox).Checked ? "True" : "false";
                dr["Valid_From"] = (row.FindControl("txtValidFrom") as TextBox).Text;
                dr["Valid_to"] = (row.FindControl("txtValidTo") as TextBox).Text;
                dr["BOM_Item_Text1"] = (row.FindControl("txtBomItemText1") as TextBox).Text;
                dr["BOM_Item_Text2"] = (row.FindControl("txtBomItemText2") as TextBox).Text;
                dr["ActiveFiller"] = (row.FindControl("ddlActFil") as DropDownList).SelectedValue;
                dr["Combination"] = (row.FindControl("txtComb") as TextBox).Text;
                dr["Upd_Flag"] = (row.FindControl("lblUpd_Flag") as Label).Text;
                dr["Item_node_number"] = (row.FindControl("hdnItemNode") as TextBox).Text;

                dt.Rows.Add(dr);
                positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Postion_No"]) + 10);
                tempId += 1;
            }


            if (tempId == 1)
                j = 1;
            for (int i = tempId; i < SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text) + tempId; i++)
            {
                dr = dt.NewRow();
                dr["BOM_HeaderDetail_Id"] = tempId;
                dr["Postion_No"] = String.Format("{0:0000}", SafeTypeHandling.ConvertStringToInt32(positionNo) + j * 10);
                dr["Valid_From"] = DateTime.Now.ToString("dd/MM/yyyy");
                dr["Valid_To"] = "31/12/9999";
                dr["Upd_Flag"] = "I";
                dr["Relevancy_To_Costing"] = "X";

                j++;
                dt.Rows.Add(dr);
            }

            dstData.Tables.Add(dt);
            dstData.AcceptChanges();

            DataView dv = new DataView(dstData.Tables[0]);
            dv.Sort = "Postion_No Asc";
            DataTable dtSorted = dv.ToTable();

            grdBOMDetailAdd.DataSource = dtSorted;
            if (Session[StaticKeys.SelectedModuleId].ToString() == "187")
            {
                grdBOMDetailAdd.Columns[22].Visible = false;
                grdBOMDetailAdd.Columns[23].Visible = false;
            }
            grdBOMDetailAdd.DataBind();
            ViewState["dstBomDetail"] = dstData;
        }
        catch (Exception ex)
        {
            _log.Error("AddBlankRow", ex);
        }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlPlantHELP, "pr_GetPlantList '0','ALL','0'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlBOMUsage, "pr_GetDropDownListByControlNameModuleType 'B','ddlBOMUsage'", "LookUp_Desc", "LookUp_Code", "");
            if (Session[StaticKeys.SelectedModuleId].ToString() == "187")
            {
                ddlBOMUsage.Items.Remove(ddlBOMUsage.Items.FindByValue("1"));
            }
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private bool SaveDetail()
    {
        bool flg = false;
        try
        {
            foreach (GridViewRow row in grdBOMDetailAdd.Rows)
            {
                BOMDetail objBOMDetail = new BOMDetail();
                objBOMDetail = GetBOMDetailData(row);
                if (objBOMAccess.SaveBOMDetails(objBOMDetail) > 0)
                {
                    flg = true;
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SaveDetail", ex);
        }

        return flg;
    }

    private BOMDetail GetBOMDetailData(GridViewRow row)
    {
        BOMDetail objBOMDetail = new BOMDetail();
        Utility objUtil = new Utility();
        try
        {
            Label lblBomDetailId = row.FindControl("lblBomDetailId") as Label;
            TextBox txtPositionNumber = row.FindControl("txtPositionNumber") as TextBox;
            DropDownList ddlItemCategory = row.FindControl("ddlItemCategory") as DropDownList;
            TextBox txtComponent = row.FindControl("txtComponent") as TextBox;
            TextBox txtComponentDesc = row.FindControl("txtComponentDesc") as TextBox;
            TextBox txtQuantity = row.FindControl("txtQuantity") as TextBox;
            //TextBox txtCompenentUnitOfMeasure = row.FindControl("txtCompenentUnitOfMeasure") as TextBox;
            DropDownList ddlCompenentUnitOfMeasure = row.FindControl("ddlCompenentUnitOfMeasure") as DropDownList;
            DropDownList ddlCompSortString = row.FindControl("ddlCompSortString") as DropDownList;
            CheckBox chkQtyIsFixed = row.FindControl("chkQtyIsFixed") as CheckBox;
            DropDownList ddlSparePartIndicator = row.FindControl("ddlSparePartIndicator") as DropDownList;
            DropDownList ddlStorageLocation = row.FindControl("ddlStorageLocation") as DropDownList;
            DropDownList ddlAlternativeItemGroup = row.FindControl("ddlAlternativeItemGroup") as DropDownList;
            TextBox txtPriority = row.FindControl("txtPriority") as TextBox;
            DropDownList ddlStrategy = row.FindControl("ddlStrategy") as DropDownList;
            TextBox txtUsageProbability = row.FindControl("txtUsageProbability") as TextBox;
            DropDownList ddlIndicatorRelavancyToCosting = row.FindControl("ddlIndicatorRelavancyToCosting") as DropDownList;
            TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
            CheckBox chkASM = row.FindControl("chkASM") as CheckBox;
            CheckBox chkPhantomIndicator = row.FindControl("chkPhantomIndicator") as CheckBox;
            TextBox txtComponentScrap = row.FindControl("txtComponentScrap") as TextBox;
            CheckBox chkRecursiveBOM = row.FindControl("chkRecursiveBOM") as CheckBox;
            TextBox txtValidFrom = row.FindControl("txtValidFrom") as TextBox;
            TextBox txtValidTo = row.FindControl("txtValidTo") as TextBox;
            TextBox txtBomItemText1 = row.FindControl("txtBomItemText1") as TextBox;
            TextBox txtBomItemText2 = row.FindControl("txtBomItemText2") as TextBox;
            DropDownList ddlActFil = row.FindControl("ddlActFil") as DropDownList;
            TextBox txtComb = row.FindControl("txtComb") as TextBox;

            Label lblUpd_Flag = row.FindControl("lblUpd_Flag") as Label;
            TextBox hdnItemNode = row.FindControl("hdnItemNode") as TextBox;

            objBOMDetail.BOM_HeaderID = Convert.ToInt32(lblBOMHeaderId.Text);
            objBOMDetail.BOM_HeaderDetail_Id = Convert.ToInt32(lblBomDetailId.Text);
            objBOMDetail.Postion_No = txtPositionNumber.Text;
            objBOMDetail.Item_Category = ddlItemCategory.SelectedValue;
            objBOMDetail.Component = txtComponent.Text;
            objBOMDetail.Component_desc = txtComponentDesc.Text;
            objBOMDetail.Quantity = txtQuantity.Text;
            objBOMDetail.Component_UOM = ddlCompenentUnitOfMeasure.SelectedValue;
            objBOMDetail.Comp_SortString = ddlCompSortString.SelectedValue;
            objBOMDetail.Qty_Is_Fixed1 = chkQtyIsFixed.Checked == true ? "1" : "0";
            objBOMDetail.Spare_Part_Indicator = ddlSparePartIndicator.SelectedValue;
            objBOMDetail.StorageLocation = ddlStorageLocation.SelectedValue;
            objBOMDetail.Alt_Item_Group = ddlAlternativeItemGroup.SelectedValue;
            objBOMDetail.Priority = txtPriority.Text;
            objBOMDetail.starategy = ddlStrategy.SelectedValue;
            objBOMDetail.Usage_Probebilty = txtUsageProbability.Text;
            objBOMDetail.Relevancy_To_Costing = ddlIndicatorRelavancyToCosting.SelectedValue;
            objBOMDetail.Remarks = txtRemarks.Text;
            objBOMDetail.ASM = chkASM.Checked == true ? "1" : "0";
            objBOMDetail.Phantom_Indicator = chkPhantomIndicator.Checked == true ? "1" : "0";
            objBOMDetail.Component_Scrap = txtComponentScrap.Text;
            objBOMDetail.RecursiveBOM = chkRecursiveBOM.Checked == true ? "1" : "0";
            objBOMDetail.Valid_From = objUtil.GetYYYYMMDD(txtValidFrom.Text);
            objBOMDetail.Valid_to = objUtil.GetYYYYMMDD(txtValidTo.Text);
            objBOMDetail.BOM_Item_Text1 = txtBomItemText1.Text;
            objBOMDetail.BOM_Item_Text2 = txtBomItemText2.Text;
            objBOMDetail.ActiveFiller = ddlActFil.SelectedValue;
            objBOMDetail.Combination = txtComb.Text;
            objBOMDetail.Upd_Flag = lblUpd_Flag.Text;
            objBOMDetail.ItemNode = hdnItemNode.Text;

            objBOMDetail.UserId = lblUserId.Text;
            objBOMDetail.TodayDate = objUtil.GetDate();
            objBOMDetail.IPAddress = objUtil.GetIpAddress();
            objBOMDetail.Mode = lblMode.Text;
        }
        catch (Exception ex)
        { _log.Error("GetBOMDetailData", ex); }
        return objBOMDetail;
    }

    private bool SaveHeader()
    {
        bool flg = false;

        try
        {
            BOMHeader ObjBOMHeader = GetControlsValue();
            int bomHeaderId = objBOMAccess.SaveBOMHeaderData(ObjBOMHeader);
            if (bomHeaderId > 0)
            {
                lblBOMHeaderId.Text = bomHeaderId.ToString();
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
            //throw ex;
            _log.Error("SaveHeader", ex);
        }
        return flg;
    }

    private BOMHeader GetControlsValue()
    {
        BOMHeader ObjBOMHeader = new BOMHeader();
        Utility objUtil = new Utility();
        try
        {
            ObjBOMHeader.BOM_HeaderID = Convert.ToInt32(lblBOMHeaderId.Text);
            ObjBOMHeader.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjBOMHeader.Material_Number = txtMaterialNo.Text;
            ObjBOMHeader.Material_Desc = txtMaterialDesc.Text;
            ObjBOMHeader.Plant_Id = ddlPlant.SelectedValue;
            ObjBOMHeader.BOM_Usage = ddlBOMUsage.SelectedValue;
            ObjBOMHeader.AlternativeBOM = txtAlternativeBOM.Text;
            ObjBOMHeader.BOMText = txtBOMText.Text;
            ObjBOMHeader.AlternativeText = txtAlternativeText.Text;
            ObjBOMHeader.BaseQty = txtBaseQuantity.Text;
            ObjBOMHeader.BaseUOM = txtBaseUOM.Text;
            ObjBOMHeader.BOMStatus = ddlBomStatus.SelectedValue;
            ObjBOMHeader.From = txtFrom.Text;
            ObjBOMHeader.To = txtTo.Text;
            //-Started to Add Remark and Reason textbox. Ticket number 8200064571-- %>
            ObjBOMHeader.Remarks = txtBomRemark.Text;
            ObjBOMHeader.Reason = txtBomReason.Text;
            //-ENded to Add Remark and Reason textbox. Ticket number 8200064571-- %>
            ObjBOMHeader.UserId = lblUserId.Text;
            ObjBOMHeader.TodayDate = objUtil.GetDate();
            ObjBOMHeader.IPAddress = objUtil.GetIpAddress();
            ObjBOMHeader.Mode = lblMode.Text;

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjBOMHeader;
    }

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

            //grdMaterialHELP.DataSource = dstData.Tables[0].DefaultView;
            //grdMaterialHELP.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
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

    private void ValidateMaterial()
    {
        bool flg = false;
        try
        {
            if (txtMaterialNo.Text != "")
            {
                string matGrp = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialNo.Text);
                if (matGrp == "162" || matGrp == "164" || matGrp == "139" || matGrp == "171" || matGrp == "144")
                {
                    if (ddlBOMUsage.SelectedValue != "")
                    {
                        if (ddlBOMUsage.SelectedValue == "1" || ddlBOMUsage.SelectedValue == "6")
                        {
                            if (matGrp == "162" || matGrp == "164" || matGrp == "145" || matGrp == "138" || matGrp == "147" || matGrp == "170")
                            {
                                lblMsg.Text = "Select proper material code series (3/4/8 series)";
                                pnlMsg.Visible = true;
                                pnlMsg.CssClass = "error";
                            }
                            else
                            {
                                flg = true;
                            }
                        }
                        else if (ddlBOMUsage.SelectedValue == "3")
                        {
                            if (matGrp == "145" || matGrp == "138" || matGrp == "147" || matGrp == "170")
                            {
                                lblMsg.Text = "Select proper material code series (1/2/3/4/8 series)";
                                pnlMsg.Visible = true;
                                pnlMsg.CssClass = "error";
                            }
                            else
                            {
                                flg = true;
                            }
                        }
                    }
                    else
                        flg = true;
                }
                else
                {
                    lblMsg.Text = "Select proper material code series (1/2/3/4/8 series)";
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }

            }
            if (flg)
            {
                DataSet dstData = BOMAccess.ReadMaterialHelp(txtMaterialNo.Text.ToString(), "", ddlPlant.SelectedValue.ToString(), "", "");
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    txtMaterialDesc.Text = dstData.Tables[0].Rows[0]["Material_Desc"].ToString();
                    txtBaseUOM.Text = dstData.Tables[0].Rows[0]["BaseUOM"].ToString();
                    pnlMsg.Visible = false;
                }
                else
                {
                    txtMaterialDesc.Text = "";
                    txtBaseUOM.Text = "";
                    txtMaterialNo.Text = "";
                    txtMaterialNo.Focus();

                    lblMsg.Text = "Material doesnot exist in the plant " + Session[StaticKeys.MaterialPlantName].ToString();
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ValidateMaterial", ex); }
    }

    private bool ValidateComponent()
    {
        bool flg = false;
        try
        {
            if (grdBOMDetailAdd.Rows.Count != 0)
            {
                flg = true;
            }
        }
        catch (Exception ex)
        { _log.Error("ValidateComponent", ex); }

        return flg;
    }

    private bool ValidateFixedQty()
    {
        bool flg = true;
        try
        {
            if (grdBOMDetailAdd.Rows.Count != 0)
            {
                for (int i = 0; i < grdBOMDetailAdd.Rows.Count; i++)
                {
                    DropDownList ddlAlternativeItemGroup = (DropDownList)grdBOMDetailAdd.Rows[i].FindControl("ddlAlternativeItemGroup");
                    TextBox txtUsageProbability = (TextBox)grdBOMDetailAdd.Rows[i].FindControl("txtUsageProbability");
                    CheckBox chkQtyIsFixed = (CheckBox)grdBOMDetailAdd.Rows[i].FindControl("chkQtyIsFixed");

                    if (chkQtyIsFixed.Checked)
                    {
                        if (ddlAlternativeItemGroup.SelectedValue.ToString() != "" || txtUsageProbability.Text != "")
                        {
                            flg = false;
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ValidateFixedQty", ex); }
        return flg;
    }

    private bool ValidateCompBaseQty()
    {
        bool flg = true;
        try
        {

            if (grdBOMDetailAdd.Rows.Count != 0)
            {
                for (int i = 0; i < grdBOMDetailAdd.Rows.Count; i++)
                {
                    TextBox txtQuantity = (TextBox)grdBOMDetailAdd.Rows[i].FindControl("txtQuantity");
                    if (Convert.ToDouble(txtQuantity.Text.ToString()) == 0)
                    {
                        flg = false;
                        break;
                    }
                }

            }
        }
        catch (Exception ex)
        { _log.Error("ValidateCompBaseQty", ex); }
        return flg;
    }

    private void ValidateComp(int rowNo)
    {
        try
        {
            foreach (GridViewRow grdRow in grdBOMDetailAdd.Rows)
            {
                if (grdRow.RowIndex == rowNo)
                {
                    TextBox txtComponent = (TextBox)grdRow.FindControl("txtComponent");
                    TextBox txtComponentDesc = (TextBox)grdRow.FindControl("txtComponentDesc");
                    CheckBox chkRecursiveBOM = (CheckBox)grdRow.FindControl("chkRecursiveBOM");
                    DropDownList ddlCompSortString = (DropDownList)grdRow.FindControl("ddlCompSortString");
                    //BOM_RSeriesDT_08072020 add || matGrp == "147"
                    string matGrp = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtComponent.Text);
                    if (matGrp == "162" || matGrp == "164" || matGrp == "139" || matGrp == "144" || matGrp == "171" || matGrp == "147")
                    {
                        DataSet dstData = BOMAccess.ReadMaterialHelp(txtComponent.Text.ToString(), "", ddlPlant.SelectedValue.ToString(), "", "");
                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            txtComponentDesc.Text = dstData.Tables[0].Rows[0]["Material_Desc"].ToString();

                            DropDownList ddlCompenentUnitOfMeasure = (DropDownList)grdRow.FindControl("ddlCompenentUnitOfMeasure");
                            helperAccess.PopuplateDropDownList(ddlCompenentUnitOfMeasure, "proc_AutoComplete_Material_UOM '" + txtComponent.Text.ToString() + "'", "LookUp_Desc", "LookUp_Code", "");

                            ddlCompenentUnitOfMeasure.SelectedValue = dstData.Tables[0].Rows[0]["BaseUOM"].ToString().Trim();

                            if (txtComponent.Text == txtMaterialNo.Text)
                                chkRecursiveBOM.Checked = true;
                            else
                                chkRecursiveBOM.Checked = false;

                            helperAccess.PopuplateDropDownList(ddlCompSortString, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlCompSortString'", "LookUp_Desc", "LookUp_Code", "");
                            if (matGrp == "162" || matGrp == "144")
                            {
                                if (Session[StaticKeys.PlantType].ToString() == "F")
                                    ddlCompSortString.SelectedValue = "KR";
                                else
                                    ddlCompSortString.SelectedValue = "";
                            }
                            else
                                ddlCompSortString.SelectedValue = "";

                            pnlMsg.Visible = false;
                        }
                        else
                        {
                            lblMsg.Text = "Material doesnot exist in the plant " + Session[StaticKeys.MaterialPlantName].ToString();
                            pnlMsg.Visible = true;
                            pnlMsg.CssClass = "error";

                            txtComponentDesc.Text = "";
                            txtComponent.Text = "";
                            chkRecursiveBOM.Checked = false;
                            txtComponent.Focus();
                        }
                    }
                    else
                    {
                        //BOM_RSeriesDT_08072020 Change MSG
                        lblMsg.Text = "Please enter correct material code(3/4/8/1/2/7) series.";
                        pnlMsg.Visible = true;
                        pnlMsg.CssClass = "error";
                        txtComponent.Text = "";
                        txtComponentDesc.Text = "";
                        chkRecursiveBOM.Checked = false;
                        txtComponent.Focus();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ValidateComp", ex); }
    }

    private void ConfigureBOMScreen()
    {
        try
        {
            //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
            if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
            {
                if (Session[StaticKeys.SelectedModuleId].ToString() == "186")
                {
                    txtMaterialNo.Enabled = false;
                    txtMaterialDesc.Enabled = false;
                    imgHelpSearchMaterial.Visible = false;
                }
                ddlBOMUsage.Enabled = false;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureBOMScreen", ex); }
    }

    //private void ValidateAltBOMData()
    //{
    //    if (txtAlternativeBOM.Text != "")
    //    {
    //        int maxAltBOM = objBOMAccess.GetMaxAltBOMCreatedForMat(txtMaterialNo.Text, ddlPlant.SelectedValue.ToString(), ddlBOMUsage.SelectedValue.ToString());
    //        if (maxAltBOM == 0)
    //        {
    //            //If no BOM is created for material
    //            if (Convert.ToInt32(txtAlternativeBOM.Text) > maxAltBOM + 1)
    //            {
    //                lblMsg.Text = "No BOM is created for material " + txtMaterialNo.Text + " .Mention Alt BOM as 01.";
    //                pnlMsg.Visible = true;
    //                pnlMsg.CssClass = "error";
    //                txtAlternativeBOM.Text = "";
    //            }
    //            else
    //            {
    //                pnlMsg.Visible = false;
    //            }
    //        }
    //        else if (maxAltBOM > 0)
    //        {
    //            //If Alt BOM entered is less than the max BOM created
    //            if (Convert.ToInt32(txtAlternativeBOM.Text) < maxAltBOM + 1)
    //            {
    //                lblMsg.Text = "Alternative BOM " + txtAlternativeBOM.Text + " already exists for this material";
    //                pnlMsg.Visible = true;
    //                pnlMsg.CssClass = "error";
    //                txtAlternativeBOM.Text = "";
    //            }
    //            //if Alt BOM entered is greater the next BOM to be created
    //            else if ((Convert.ToInt32(txtAlternativeBOM.Text) > maxAltBOM + 1))
    //            {
    //                lblMsg.Text = "Maximum Alt BOM created for material " + txtMaterialNo.Text + " is " + maxAltBOM + ". Mention Alt BOM as " + (maxAltBOM + 1);
    //                pnlMsg.Visible = true;
    //                pnlMsg.CssClass = "error";
    //                txtAlternativeBOM.Text = "";
    //            }
    //            else
    //            {
    //                pnlMsg.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            pnlMsg.Visible = false;
    //        }
    //    }
    //}

    #endregion




    #region ITSM413605


    #region Document Upload

    private void BindAttachedDocuments(string MaterialId)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {
            dstData = objDoc.GetDocumentUploadDataBOM(Convert.ToInt32(lblMasterHeaderId.Text), "Bom");
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
                    System.IO.File.Delete(Server.MapPath("Bom") + "/" + Session[StaticKeys.RequestNo].ToString() + "/");

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
                        System.IO.File.Delete(Server.MapPath("Bom") + "/" + lblUploadedFileName.Text);
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
                StrPath = "/Transaction/BOMRecipe/Bom/" + Session[StaticKeys.RequestNo].ToString() + "/";
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
                ObjDoc.Document_Type = "Bom";
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
                            bool flag1 = false;
                            UpdateFlagSecRes objUpdateFlagSecRes = new UpdateFlagSecRes();
                            objUpdateFlagSecRes = GetFlagSecRes("I");
                            if (objRecipeAccess.SaveUpdateFlag(objUpdateFlagSecRes) > 0)
                            {
                                flag1 = true;
                            }
                            else
                            { flag1 = false; }


                            using (SqlCommand cmd = new SqlCommand("pr_Ins_Upd_T_BOM_Detail_Temp"))
                            { 
                                cmd.Connection = con;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                cmd.CommandType = CommandType.StoredProcedure;
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {

                                    string sQuantity = "";
                                    if (dt.Rows[i]["Component"].ToString() != "")
                                    {
                                        if (dt.Rows[i]["Quantity"].ToString() != "")
                                        {
                                            sQuantity = dt.Rows[i]["Quantity"].ToString().Trim();
                                            sQuantity = sQuantity.Replace(@",", "");
                                        }
                                        else
                                        {
                                            sQuantity = "0";
                                        }

                                        try
                                        {

                                        }
                                        catch (Exception ex) { }
                                        try
                                        {
                                            cmd.Parameters.AddWithValue("@Master_HeaderID", lblMasterHeaderId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@BOM_HeaderID", lblBOMHeaderId.Text.Trim());
                                            cmd.Parameters.AddWithValue("@Item_Category", "");
                                            cmd.Parameters.AddWithValue("@Postion_No", dt.Rows[i]["Position_No"].ToString().Trim());// (dt.Rows[i]["Phase_Indicator"].ToString().Trim() == "X" ? "True" : "false"));
                                            cmd.Parameters.AddWithValue("@Component", dt.Rows[i]["Component"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Component_desc", dt.Rows[i]["Component_Desc"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Quantity", sQuantity);
                                            cmd.Parameters.AddWithValue("@Component_UOM", dt.Rows[i]["UOM"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Comp_SortString", dt.Rows[i]["Storage_Loc"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Qty_Is_Fixed1", dt.Rows[i]["Qty_Is_Fixed"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Spare_Part_Indicator", "");
                                            cmd.Parameters.AddWithValue("@StorageLocation", "");
                                            cmd.Parameters.AddWithValue("@Alt_Item_Group", dt.Rows[i]["Alt_Item_Grp"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Priority", "");
                                            cmd.Parameters.AddWithValue("@starategy", "");
                                            cmd.Parameters.AddWithValue("@Usage_Probebilty", dt.Rows[i]["Usage_Prob"].ToString().Trim());// (dt.Rows[i]["IIst_Std_Duration"].ToString().Trim() == "" ? "0" : dt.Rows[i]["IIst_Std_Duration"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@Relevancy_To_Costing", "");
                                            cmd.Parameters.AddWithValue("@Remarks", dt.Rows[i]["Remarks"].ToString().Trim());// (dt.Rows[i]["IIIst_Std_Duration"].ToString().Trim() == "" ? "0" : dt.Rows[i]["IIIst_Std_Duration"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@ASM", 0);
                                            cmd.Parameters.AddWithValue("@PhantomIndicator", 0);
                                            cmd.Parameters.AddWithValue("@Component_Scrap", "");
                                            cmd.Parameters.AddWithValue("@RecursiveBOM", dt.Rows[i]["Recursive_BOM"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Valid_From", objUtil.GetYYYYMMDDExcel(dt.Rows[i]["Valid_From"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@Valid_to", objUtil.GetYYYYMMDDExcel(dt.Rows[i]["Valid_To"].ToString().Trim()));
                                            cmd.Parameters.AddWithValue("@BOM_Item_Text1", "");
                                            cmd.Parameters.AddWithValue("@BOM_Item_Text2", "");
                                            cmd.Parameters.AddWithValue("@ActiveFiller", dt.Rows[i]["Active_Filler"].ToString().Trim());
                                            cmd.Parameters.AddWithValue("@Combination", "");
                                            cmd.Parameters.AddWithValue("@Upd_Flag", "");
                                            cmd.Parameters.AddWithValue("@Item_node_number", "");
                                            cmd.Parameters.AddWithValue("@IsActive", 1);
                                            cmd.Parameters.AddWithValue("@UserId", Session[StaticKeys.LoggedIn_User_Id].ToString());
                                            cmd.Parameters.AddWithValue("@UserIp", objUtil.GetIpAddress());
                                            SqlDataReader sdr = cmd.ExecuteReader();
                                            sdr.Close();

                                            cmd.Parameters.RemoveAt("@Master_HeaderID");
                                            cmd.Parameters.RemoveAt("@BOM_HeaderID");
                                            cmd.Parameters.RemoveAt("@Item_Category");
                                            cmd.Parameters.RemoveAt("@Postion_No");
                                            cmd.Parameters.RemoveAt("@Component");
                                            cmd.Parameters.RemoveAt("@Component_desc");
                                            cmd.Parameters.RemoveAt("@Quantity");
                                            cmd.Parameters.RemoveAt("@Component_UOM");
                                            cmd.Parameters.RemoveAt("@Comp_SortString");
                                            cmd.Parameters.RemoveAt("@Qty_Is_Fixed1");
                                            cmd.Parameters.RemoveAt("@Spare_Part_Indicator");
                                            cmd.Parameters.RemoveAt("@StorageLocation");
                                            cmd.Parameters.RemoveAt("@Alt_Item_Group");
                                            cmd.Parameters.RemoveAt("@Priority");
                                            cmd.Parameters.RemoveAt("@starategy");
                                            cmd.Parameters.RemoveAt("@Usage_Probebilty");
                                            cmd.Parameters.RemoveAt("@Relevancy_To_Costing");
                                            cmd.Parameters.RemoveAt("@Remarks");
                                            cmd.Parameters.RemoveAt("@ASM");
                                            cmd.Parameters.RemoveAt("@PhantomIndicator");
                                            cmd.Parameters.RemoveAt("@Component_Scrap");
                                            cmd.Parameters.RemoveAt("@RecursiveBOM");
                                            cmd.Parameters.RemoveAt("@Valid_From");
                                            cmd.Parameters.RemoveAt("@Valid_to");
                                            cmd.Parameters.RemoveAt("@BOM_Item_Text1");
                                            cmd.Parameters.RemoveAt("@BOM_Item_Text2");
                                            cmd.Parameters.RemoveAt("@ActiveFiller");
                                            cmd.Parameters.RemoveAt("@Combination");
                                            cmd.Parameters.RemoveAt("@Upd_Flag");
                                            cmd.Parameters.RemoveAt("@Item_node_number");

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
                            string msg = "Incorrect header name / sequence, kindly refer input format on form & re - upload.";
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



            string query = "select * from [Bom$]";
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

    private bool SaveBOMDetailsT2T()
    {
        bool flg = false;
        try
        {
            RecipeSecRes objRcpSecRes = new RecipeSecRes();
            if (objRecipeAccess.SaveBOMDetailsT2T(Convert.ToInt32(lblBOMHeaderId.Text.Trim()), Convert.ToInt32(lblMasterHeaderId.Text.Trim()), Session[StaticKeys.LoggedIn_User_Id].ToString()) > 0)
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
            objUpdateFlagSecRes.Recipe_HeaderID = Convert.ToInt32(lblBOMHeaderId.Text);// 0;//Convert.ToInt32(lblBOMHeaderId.Text);
            objUpdateFlagSecRes.sScreenFlag = sFlag;
            objUpdateFlagSecRes.Section_ID = 70;
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

            //if (lblBOMHeaderId.Text != "" && lblBOMHeaderId.Text != "0")
            //{

            if (SaveHeader())
            {
                if (lblBOMHeaderId.Text != "" && lblBOMHeaderId.Text != "0")
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
                    lblMsg.Text = "Kindly submit Bom Header Screen first.";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Error while saving Details";
                pnlMsg.CssClass = "error";
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
            DataSet dsSecResv = objRecipeAccess.GetExlUpdStatusTB(lblMasterHeaderId.Text.Trim(), 70);
            if (dsSecResv.Tables[0].Rows.Count > 0)
            {
                if (dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V " || dsSecResv.Tables[0].Rows[0]["sScreenFlag"].ToString() == "V")
                {

                    if (SaveBOMDetailsT2T())
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

                        //FillRecipeData(mode);
                        FillBOMData(mode);
                        lblMsg.Text = "Bom Data updated successfully.";
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;


                    }
                    else
                    {
                        btnSRSubmit.Visible = true;
                        string msg = "";
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