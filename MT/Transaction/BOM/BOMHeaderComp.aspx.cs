using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.IO;
using log4net;
public partial class Transaction_BOM_BOMHeaderComp : System.Web.UI.Page
{

    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    BOMAccess objBOMAccess = new BOMAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.SelectedModuleId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    PopuplateDropDownList();
                    FillBOMData(mode);

                    ddlPlantHELP.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                    if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M"))
                    {
                        trButton.Visible = true;
                    }
                }
            }
        }
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
                    _log.Error("grdBOMDetailAdd_RowCommand", ex);
                }
            }
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
        catch (Exception ex)
        { _log.Error("grdBOMDetailAdd_RowCommand1", ex); }
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
                ddlIndicatorRelavancyToCosting.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[5].ToString();

                DropDownList ddlCompSortString = (DropDownList)e.Row.FindControl("ddlCompSortString");
                helperAccess.PopuplateDropDownList(ddlCompSortString, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlCompSortString'", "LookUp_Desc", "LookUp_Code");
                ddlCompSortString.SelectedValue = grdBOMDetailAdd.DataKeys[e.Row.RowIndex].Values[6].ToString();

                DropDownList ddlAlternativeItemGroup = (DropDownList)e.Row.FindControl("ddlAlternativeItemGroup");
                helperAccess.PopuplateDropDownList(ddlAlternativeItemGroup, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlAlternativeItemGroup'", "LookUp_Desc", "LookUp_Code");
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

                    if (matGrp == "162" || matGrp == "144")
                    {
                        //helperAccess.PopuplateDropDownList(ddlCompSortString, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlCompSortString'", "LookUp_Desc", "LookUp_Code");
                        if (Session[StaticKeys.SelectedModuleId].ToString() == "185")
                            ddlCompSortString.SelectedValue = "KR";
                        else
                            ddlCompSortString.SelectedValue = "0";
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
        try
        {
            bool flg = false;
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
        { _log.Error("txtMaterialNo_TextChanged", ex); }
    }

    protected void ddlBOMUsage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool flg = false;
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
                //chkRecursiveBOM.Checked = ObjBOMHeader.RecursiveBOM.ToLower() == "true" ? true : false;

                DataSet dsBOMDetails = new DataSet();
                dsBOMDetails = objBOMAccess.GetBOMDetail(lblBOMHeaderId.Text);

                //if (mode == "V")
                //{
                grdBOMDetailAdd.DataSource = objBOMAccess.GetBOMDetail(lblBOMHeaderId.Text);
                grdBOMDetailAdd.DataBind();
                grdBOMDetailAdd.Visible = true;
                //}
                //else if (mode == "N" || mode == "M")
                //{
                //    grdBOMDetailAdd.DataSource = objBOMAccess.GetBOMDetail(lblBOMHeaderId.Text);
                //    grdBOMDetailAdd.DataBind();
                //    grdBomDetailsView.Visible = false;
                //    grdBOMDetailAdd.Visible = true; 
                //}
            }
            else
            {
                lblBOMHeaderId.Text = "0";
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                AddBlankRow();
            }
        }
        catch (Exception ex)
        { _log.Error("FillBOMData", ex); }

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
            grdBOMDetailAdd.DataBind();
            ViewState["dstBomDetail"] = dstData;
        }
        catch (Exception ex)
        {
            //throw ex;
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
        { _log.Error("SaveDetail", ex); }

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
        {
            _log.Error("GetBOMDetailData", ex);

        }
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
        { _log.Error("ReadMaterialHelp", ex); }
    }

    private void ValidateMaterial()
    {
        try
        { 
        bool flg = false;
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

                        if (matGrp == "162" || matGrp == "144")
                        {
                            //helperAccess.PopuplateDropDownList(ddlCompSortString, "pr_GetDropDownListByControlNameModuleType_Code 'B','ddlCompSortString'", "LookUp_Desc", "LookUp_Code");
                            if (Session[StaticKeys.SelectedModuleId].ToString() == "185")
                                ddlCompSortString.SelectedValue = "KR";
                            else
                                ddlCompSortString.SelectedValue = "0";
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

    #endregion



}