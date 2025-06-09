using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;
using System.Data.SqlClient;
using System.IO;
using log4net;
//lnkCopy_Click and lnkDelete_Click Visible for MSE_8300002156 in aspx
public partial class Transaction_Material_MaterialExtension : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MaterialExtensionAccess ObjMaterialExtensionAccess = new MaterialExtensionAccess();
    HelperAccess helperAccess = new HelperAccess();
    //LLM_DPT_SDT30072019 Commented By Nitin RddlProfitCenter
    //List<string> LLMDPTPlantList = new List<string> { "9", "15", "16", "17", "23", "24", "25", "90", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "138", "139", "140", "141", "142", "28", "29", "30", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "83", "84", "85", "86", "87", "88", "89", "90" };
    //LLM_DPT_SDT30072019 Commented By Nitin R

    //LLM_DPT_SDT30072019 Added By Nitin R
    List<string> LLMDPTPlantList = new List<string>();
    //LLM_DPT_SDT30072019 Added By Nitin R

    MaterialExtension objSavedExtnData = new MaterialExtension();

    private short _tabIndex = 0;

    public short TabIndex
    {
        get
        {
            _tabIndex++;
            return _tabIndex;
        }
    }

    #region Page Event

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    //MME_8300002156
                    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    if (!objAccess.IsSAPMASSintegrationChkAval(lblMasterHeaderId.Text))
                    {
                        //MME_8300002156 Start
                        pnlSExt.Visible = false;
                        pnlMassExt.Visible = true;
                        //MME_8300002156 End

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
                            //lblMatPlantGrpId.Text = Session[StaticKeys.MatPlantGrp].ToString();

                            ExcelDownloadEXTDATA.Visible = false;
                            ExcelDownloadError.Visible = false;
                            PopuplateDropDownListMass();
                            BindAttachedDocuments(lblMasterHeaderId.Text);
                            hlMSImportFormat.NavigateUrl = "";

                            //if (ddlTypeOfMassUpdm.SelectedValue == "11")
                            //{
                            hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatExtensionDoc/MatExtensionSMMP/MaterialExtension.xlsx";
                            //}
                            //else if (ddlTypeOfMassUpdm.SelectedValue == "12")
                            //{
                            //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/SelectionMethod.xlsx";
                            //}
                            //else if (ddlTypeOfMassUpdm.SelectedValue == "13")
                            //{
                            //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/PlannedPrice.xlsx";
                            //}
                            //else if (ddlTypeOfMassUpdm.SelectedValue == "14")
                            //{
                            //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/TaggingofBOM.xlsx";
                            //}
                            //else if (ddlTypeOfMassUpdm.SelectedValue == "15")
                            //{
                            //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/ProductHierarchy.xlsx";
                            //}
                            //else if (ddlTypeOfMassUpdm.SelectedValue == "16")
                            //{
                            //    hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Other.xlsx";
                            //}

                            //HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                            if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                            {
                                trMassBtn.Visible = true;
                            }
                            else
                            {
                                trMassBtn.Visible = false;
                                btnMassSave.Visible = false;
                                fileUploadMS.Visible = false;
                                lblFileMessage.Visible = false;
                                btnMSProcess.Visible = false;
                                lblselectcap.Visible = false;
                                lblSelectFile.Visible = false;
                                hlMSImportFormat.Visible = false;
                                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                                {
                                    ExcelDownloadEXTDATA.Visible = true;
                                    ExcelDownloadError.Visible = true;
                                }

                            }


                        }
                        else
                        {
                            Response.Redirect("MaterialMaster.aspx");
                        }
                    }
                    else
                    {
                        //MME_8300002156 Start
                        pnlSExt.Visible = true;
                        pnlMassExt.Visible = false;
                        //MME_8300002156 End

                        //Added for Testing
                        lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                        //PopuplateDropDownList();

                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                        //CTRL_SUB_SDT18112019 Added by NR
                        try
                        {
                            MaterialChangeAccess ObjMaterialChangeAccess = new MaterialChangeAccess();
                            DataSet ds;
                            ds = ObjMaterialChangeAccess.GetRefModule(Convert.ToInt32(lblMasterHeaderId.Text));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblRefModuleId.Text = Convert.ToString(ds.Tables[0].Rows[0]["Ref_Module_Id"]); ;
                            }
                        }
                        catch
                        {

                        }
                        //CTRL_SUB_SDT18112019 Added by NR

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = true;
                            // MSE_8300002156 Start Commented By NR
                            //btnNext.Visible = true;
                            // MSE_8300002156 End Commented By NR
                            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0)
                            {
                                pnlData.Visible = true;
                                // MSE_8300002156 Start Commented By NR
                                //lnlAddDetails.Visible = true;

                                // MSE_8300002156 End Commented By NR
                                // MSE_8300002156 Start Added By NR
                                lnlAddDetails.Visible = false;
                                // MSE_8300002156 Start Added By NR
                            }
                        }
                        else
                        {
                            grvData.Columns[5].Visible = false;
                            lnlAddDetails.Visible = false;
                            //pnlData.Visible = false;

                            pnlData.Visible = true;
                        }
                        //LLM_DPT_SDT30072019 Added By Nitin R
                        LLMDPTPlantListMet();
                        //LLM_DPT_EDT30072019 Added By Nitin R
                        ClearData();
                        FillFormDataByMHId();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }


    /// <summary>
    /// CCP-MM-941-22-0082
    /// </summary>
    private void ConfigCertificateType(string pValue, string pValue1)
    {
        try
        {
            lblddlCertificateType.Visible = true;
            lableddlCertificateType.Visible = false;
            reqddlCertificateType.Visible = false;
            ddlCertificateType.Enabled = true;
            ddlCertificateType.Visible = true;
            if (pValue1 != "False")
            {
                ddlCertificateType.SelectedValue = "";
            }
        }
        catch (Exception ex)
        { }
        MaterialMasterAccess objMaterialMasterAccess = new MaterialMasterAccess();
        DataSet dstData = new DataSet();
        //Plant id 0 meanse apply certificate type validation
        dstData = objMaterialMasterAccess.FillPropertiesDataSet(Convert.ToInt32(lblRefModuleId.Text.ToString()), pValue, "CType");
        if (dstData.Tables[0].Rows.Count > 0)
        {
            try
            {
                foreach (DataRow row in dstData.Tables[0].Rows)
                {
                    string bMandatory = Convert.ToString(row["bMandatory"]) == "1" ? "true" : "false";
                    string bEnable = Convert.ToString(row["bEnable"]) == "1" ? "true" : "false";
                    string bShowhide = Convert.ToString(row["bShowhide"]) == "1" ? "true" : "false";
                    string iSectionFieldID = Convert.ToString(row["iSectionFieldID"]);
                    string sDefualtValue = Convert.ToString(row["sDefualtValue"]).ToString();


                    if (iSectionFieldID == "151152")//Certificate Type
                    {
                        lblddlCertificateType.Visible = Convert.ToBoolean(bShowhide);
                        lableddlCertificateType.Visible = Convert.ToBoolean(bMandatory);
                        reqddlCertificateType.Visible = Convert.ToBoolean(bMandatory);
                        ddlCertificateType.Enabled = Convert.ToBoolean(bEnable);
                        ddlCertificateType.Visible = Convert.ToBoolean(bShowhide);
                        if (pValue1 != "False")
                        {
                            ddlCertificateType.SelectedValue = Convert.ToString(sDefualtValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            try
            {
                lblddlCertificateType.Visible = true;
                lableddlCertificateType.Visible = false;
                reqddlCertificateType.Visible = false;
                ddlCertificateType.Enabled = true;
                ddlCertificateType.Visible = true;

            }
            catch (Exception ex)
            {

            }
        }

    }


    /// <summary>
    /// CCP-MM-941-22-0082
    /// </summary>
    private void ConfigureControlQMCtrl(string pValue)
    {
        try
        {
            //if (pValue != "False")
            //{
            lblchkQMProcurmentActive.Visible = true;
            lablechkQMProcurmentActive.Visible = false;
            chkQMProcurmentActive.Enabled = true;
            chkQMProcurmentActive.Visible = true;

            lblddlControlQualityMang.Visible = true;
            lableddlControlQualityMang.Visible = false;
            reqddlControlQualityMang.Visible = false;
            ddlControlQualityMang.Enabled = true;
            ddlControlQualityMang.Visible = true;

            lblddlCertificateType.Visible = true;
            lableddlCertificateType.Visible = false;
            reqddlCertificateType.Visible = false;
            ddlCertificateType.Enabled = true;
            ddlCertificateType.Visible = true;
            //}

        }
        catch (Exception ex)
        { }
        MaterialMasterAccess objMaterialMasterAccess = new MaterialMasterAccess();
        DataSet dstData = new DataSet();
        dstData = objMaterialMasterAccess.FillPropertiesDataSet(Convert.ToInt32(lblRefModuleId.Text.ToString()), Convert.ToString(Session[StaticKeys.MaterialPlantId].ToString()), "QMType");
        if (dstData.Tables[0].Rows.Count > 0)
        {
            try
            {
                foreach (DataRow row in dstData.Tables[0].Rows)
                {
                    string bMandatory = Convert.ToString(row["bMandatory"]) == "1" ? "true" : "false";
                    string bEnable = Convert.ToString(row["bEnable"]) == "1" ? "true" : "false";
                    string bShowhide = Convert.ToString(row["bShowhide"]) == "1" ? "true" : "false";
                    string iSectionFieldID = Convert.ToString(row["iSectionFieldID"]);
                    string sDefualtValue = Convert.ToString(row["sDefualtValue"]).ToString();


                    if (iSectionFieldID == "150")//QM in Procurement is Active
                    {
                        lblchkQMProcurmentActive.Visible = Convert.ToBoolean(bShowhide);
                        lablechkQMProcurmentActive.Visible = Convert.ToBoolean(bMandatory);
                        //reqddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                        chkQMProcurmentActive.Enabled = Convert.ToBoolean(bEnable);
                        chkQMProcurmentActive.Visible = Convert.ToBoolean(bShowhide);
                        if (pValue != "False")
                        {
                            chkQMProcurmentActive.Checked = Convert.ToBoolean(sDefualtValue);
                        }
                    }
                    if (iSectionFieldID == "151")//Certificate Type
                    {
                        lblddlCertificateType.Visible = Convert.ToBoolean(bShowhide);
                        lableddlCertificateType.Visible = Convert.ToBoolean(bMandatory);
                        reqddlCertificateType.Visible = Convert.ToBoolean(bMandatory);
                        ddlCertificateType.Enabled = Convert.ToBoolean(bEnable);
                        ddlCertificateType.Visible = Convert.ToBoolean(bShowhide);
                        if (pValue != "False")
                        {
                            ddlCertificateType.SelectedValue = Convert.ToString(sDefualtValue);
                        }
                    }
                    if (iSectionFieldID == "152")//ddlControlQualityMang
                    {
                        lblddlControlQualityMang.Visible = Convert.ToBoolean(bShowhide);
                        lableddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                        reqddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                        ddlControlQualityMang.Enabled = Convert.ToBoolean(bEnable);
                        ddlControlQualityMang.Visible = Convert.ToBoolean(bShowhide);
                        if (pValue != "False")
                        {
                            ddlControlQualityMang.SelectedValue = Convert.ToString(sDefualtValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            try
            {
                lableddlControlQualityMang.Visible = false;
                reqddlControlQualityMang.Visible = false;
                ddlControlQualityMang.Enabled = true;
                ddlControlQualityMang.Visible = true;

            }
            catch (Exception ex)
            {

            }
        }

    }

    protected override void OnUnload(EventArgs e)
    {
        try
        {
            base.OnUnload(e);

            // your code
            Session["ExtensionMain"] = null;
            Session["ExtensionNew"] = null;
        }
        catch (Exception ex)
        { _log.Error("OnUnload", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (Save())
            {
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Save())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                //Srinidhi
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0)
                    pnlData.Visible = true;
                //Srinidhi

                //Srinidhi extnsn req
                //btnNext.Enabled = true;
                //Srinidhi extnsn req

                Response.Redirect("MaterialExtension.aspx");

            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    //protected void btnSaveAll_Click(object sender, EventArgs e)
    //{
    //    if (Save())
    //    {
    //        lblMsg.Text = Messages.GetMessage(1);
    //        pnlMsg.CssClass = "success";
    //        pnlMsg.Visible = true;

    //        Response.Redirect("MaterialExtension.aspx");
    //    }
    //}

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Save())
            //{
            Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
            Response.Redirect("MaterialMaster.aspx");
            //    string pageURL = btnNext.CommandArgument.ToString();
            //    Response.Redirect(pageURL);
            //}
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPlantWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSalesOrgWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            string Material_Extension_Id = grvData.DataKeys[grdrow.RowIndex]["Material_Extension_Id"].ToString();

            if (ObjMaterialExtensionAccess.DeleteMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(Material_Extension_Id)) > 0)
            {
                lblMsg.Text = "Record Deleted Sucessfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }

            FillFormDataByMHId();
        }
        catch (Exception ex)
        { _log.Error("lnkDelete_Click", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMatExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Material_Extension_Id"].ToString();
            FillControlData();
            //Srinidhi
            pnlData.Visible = true;
            //Srinidhi
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
        }
    }

    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        //ClearData();
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblMatExtensionId.Text = grvData.DataKeys[grdrow.RowIndex]["Material_Extension_Id"].ToString();
            FillControlData();

            txtMaterialCode.Enabled = true;
            ddlPlant.Enabled = true;

            lblMatExtensionId.Text = "";
            txtMaterialCode.Text = "";
            txtMaterialDescription.Text = "";

            //Srinidhi extnsn req
            //btnNext.Enabled = false;
            //Srinidhi extnsn req

        }
        catch (Exception ex)
        { _log.Error("lnkCopy_Click", ex); }
    }

    protected void ddlMrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            MRPTypeWiseSetup();
        }
        catch (Exception ex)
        { _log.Error("ddlMrpType_SelectedIndexChanged", ex); }
    }

    protected void ddlLotSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LotSizeWiseValidation();
        }
        catch (Exception ex)
        { _log.Error("ddlLotSize_SelectedIndexChanged", ex); }
    }

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    protected void txtMaterialCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            MaterialTypeSelection();
        }
        catch (Exception ex)
        { _log.Error("txtMaterialCode_TextChanged", ex); }
    }

    protected void lnkRefreshddlInspectionType_Click(object sender, EventArgs e)
    {
        try
        {
            DisplayInspectionType();
        }
        catch (Exception ex)
        { _log.Error("lnkRefreshddlInspectionType_Click", ex); }
    }

    protected void ddlInspectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DisplayInspectionType();
        }
        catch (Exception ex)
        { _log.Error("ddlInspectionType_SelectedIndexChanged", ex); }
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            if (ddlWarehouse.SelectedValue.Trim() == "")
            {
                ddlStorageType.Enabled = false;
                ddlWareHouseMangUnit.Enabled = false;
                ddlStorageTypeIndiSPlaceRemoval.Enabled = false;
                ddlStorageSectIndi.Enabled = false;
                ddlStorageTypeIndiSPlacement.Enabled = false;
                txtCapacityUsage.Enabled = false;
                txtLoadingEquipQuantity.Enabled = false;

                reqddlStorageType.Visible = false;
                reqddlWareHouseMangUnit.Visible = false;
                reqddlStorageTypeIndiSPlacement.Visible = false;
                reqddlStorageSectIndi.Visible = false;
                reqddlStorageTypeIndiSPlaceRemoval.Visible = false;
                reqtxtCapacityUsage.Visible = false;
                reqtxtLoadingEquipQuantity.Visible = false;

                lableddlStorageType.Visible = false;
                lableddlWareHouseMangUnit.Visible = false;
                lableddlStorageTypeIndiSPlacement.Visible = false;
                lableddlStorageSectIndi.Visible = false;
                lableddlStorageTypeIndiSPlaceRemoval.Visible = false;
                labletxtCapacityUsage.Visible = false;
                labletxtLoadingEquipQuantity.Visible = false;

            }
            else
            {
                ddlStorageType.Enabled = true;
                ddlWareHouseMangUnit.Enabled = true;
                ddlStorageTypeIndiSPlaceRemoval.Enabled = true;
                ddlStorageSectIndi.Enabled = true;
                ddlStorageTypeIndiSPlacement.Enabled = true;
                txtCapacityUsage.Enabled = true;
                txtLoadingEquipQuantity.Enabled = true;

                reqddlStorageType.Visible = true;
                reqddlWareHouseMangUnit.Visible = true;
                reqddlStorageTypeIndiSPlacement.Visible = true;
                reqddlStorageSectIndi.Visible = true;
                reqddlStorageTypeIndiSPlaceRemoval.Visible = true;
                reqtxtCapacityUsage.Visible = true;
                reqtxtLoadingEquipQuantity.Visible = true;

                lableddlStorageType.Visible = true;
                lableddlWareHouseMangUnit.Visible = true;
                lableddlStorageTypeIndiSPlacement.Visible = true;
                lableddlStorageSectIndi.Visible = true;
                lableddlStorageTypeIndiSPlaceRemoval.Visible = true;
                labletxtCapacityUsage.Visible = true;
                labletxtLoadingEquipQuantity.Visible = true;
            }

        }
        catch (Exception ex)
        { _log.Error("ddlWarehouse_SelectedIndexChanged", ex); }
    }

    protected void txtLoadingEquipQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ddlUnitMeasureLoadingEquip.Enabled = true;
            lableddlUnitMeasureLoadingEquip.Visible = true;
            reqddlUnitMeasureLoadingEquip.Visible = true;

            ddlStorageUnitType.Enabled = true;
            lableddlStorageUnitType.Visible = true;
            reqddlStorageUnitType.Visible = true;

            txtLoadingEquipQuantity2.Enabled = true;
            labletxtLoadingEquipQuantity2.Visible = true;
            reqtxtLoadingEquipQuantity2.Visible = true;

            ddlUnitMeasureLoadingEquip2.Enabled = true;
            lableddlUnitMeasureLoadingEquip2.Visible = true;
            reqddlUnitMeasureLoadingEquip2.Visible = true;

            ddlStorageUnitType2.Enabled = true;
            lableddlStorageUnitType2.Visible = true;
            reqddlStorageUnitType2.Visible = true;


            txtloadingEquipQuantity3.Enabled = true;
            labletxtloadingEquipQuantity3.Visible = true;
            reqtxtloadingEquipQuantity3.Visible = true;

            ddlUnitMeasureLoadingEquip3.Enabled = true;
            lableddlUnitMeasureLoadingEquip3.Visible = true;
            reqddlUnitMeasureLoadingEquip3.Visible = true;

            ddlStorageUnitType3.Enabled = true;
            lableddlStorageUnitType3.Visible = true;
            reqddlStorageUnitType3.Visible = true;
        }
        catch (Exception ex)
        { _log.Error("txtLoadingEquipQuantity_TextChanged", ex); }
    }

    /// <summary>
    /// //MSE_8300002156
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlControlQualityMang_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlControlQualityMang.SelectedValue == "Z102" || ddlControlQualityMang.SelectedValue == "Z101" || ddlControlQualityMang.SelectedValue == "0000")
            //{
            //    ddlCertificateType.SelectedValue = "9999";
            //    ddlCertificateType.Enabled = false;
            //}
            //else
            //{
            //    ddlCertificateType.SelectedValue = "";
            //    ddlCertificateType.Enabled = true;
            //}
            ConfigCertificateType(ddlControlQualityMang.SelectedValue, "1");
        }
        catch (Exception ex)
        { _log.Error("ddlControlQualityMang_SelectedIndexChanged", ex); }
    }
    #endregion

    #region Public Method

    /// <summary>
    /// LLM_DPT_SDT30072019 
    /// Update list
    /// </summary>
    private void LLMDPTPlantListMet()
    {
        try
        {
            QualityAccess ObjQualityAccess = new QualityAccess();
            DataSet ds;
            ds = ObjQualityAccess.GetLLMDPTPlantList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string sValNew = Convert.ToString(row["Plant_Id"]);
                    LLMDPTPlantList.Add(sValNew);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("LLMDPTPlantListMet", ex);
        }
    }

    protected void ClearData()
    {
        try
        {
            lblMatExtensionId.Text = "0";

            txtMaterialCode.Text = "";
            txtMaterialDescription.Text = "";

            txtMaterialCode.Enabled = true;
            ddlPlant.Enabled = true;

            txtReorder.Text = "";
            txtMinLotSize.Text = "";
            txtMaxLotSize.Text = "";
            txtFixedLotSize.Text = "";
            txtRoundingValue.Text = "";
            txtGRProcessingTime.Text = "";
            txtPlannedDeleveryTime.Text = "";
            txtCapacityUsage.Text = "";
            txtLoadingEquipQuantity.Text = "";
            txtLoadingEquipQuantity2.Text = "";
            txtloadingEquipQuantity3.Text = "";

            ClearSelectedValue(ddlInspectionType);

            txtIntervalNPInspector.Text = "";
            //ddlReason.SelectedValue = "";
            txtRemarks.Text = "";
            chkQMProcurmentActive.Checked = false;//MSE_8300002156

            PopuplateDropDownList();
            ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

            BindPlantWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlMaterialAccGrp, "pr_GetModuleListByModuleType 'M'", "Module_Name", "Module_Id", "");

            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','All','" + lblMatExtensionId.Text + "'", "Plant_Name", "Plant_Id", "");
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMatExtensionId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblRefModuleId.Text == "162")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MC','" + lblMatExtensionId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMatExtensionId.Text + "','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR
            BindPlantWiseDropDown();

            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlAccountAssignment, "pr_GetDropDownListByControlNameModuleType 'M','ddlAccountAssignment'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialPGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialPGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPurchasingGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlPurchasingGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMrpType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPriceControlIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlPriceControlIndicator'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlLotSize, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");


            helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownCheckBox(ddlInspectionType, "pr_GetDropDownListByControlNameModuleType 'M','ddlInspectionType'", "LookUp_Desc", "LookUp_Code");
            helperAccess.PopuplateDropDownList(ddlWarehouse, "pr_GetDropDownListByControlNameModuleType 'M','ddlWarehouse'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlWareHouseMangUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip2, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip3, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
            //MSE_8300002156
            helperAccess.PopuplateDropDownList(ddlControlQualityMang, "pr_GetDropDownListByControlNameModuleType 'M','ddlControlQualityMang'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCertificateType, "pr_GetDropDownListByControlNameModuleType 'M','ddlCertificateType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlRangeCoverage, "pr_GetDropDownListByControlNameModuleType 'M','ddlRangeCoverage'", "LookUp_Desc", "LookUp_Code", "");
            //MSE_8300002156
            //NRDT02032023 Start
            helperAccess.PopuplateDropDownList(ddlMaterialAuthGroupActQM, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialAuthGroupActQM'", "LookUp_Desc", "LookUp_Code", "");
            //NRDT02032023 End
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void FillFormDataByMHId()
    {
        DataSet dataSet = ObjMaterialExtensionAccess.GetMaterialExtensionData(SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text));
        //MSE_8300002156 Start
        try
        {
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                lblMatExtensionId.Text = Convert.ToString(dataSet.Tables[0].Rows[0]["Material_Extension_Id"].ToString());
            }
            else
            {
                lblMatExtensionId.Text = "0"; //Material_Extension_Id
            }
        }
        catch (Exception ex) { _log.Error("FillFormDataByMHId", ex); }
        //MSE_8300002156 End

        try
        {

            grvData.DataSource = dataSet;
            grvData.DataBind();

            FillControlData();

            if (ddlMaterialAccGrp.SelectedValue == "162" || ddlMaterialAccGrp.SelectedValue == "164")
            {
                if (ddlPlant.SelectedValue == "6" || ddlPlant.SelectedValue == "7" || ddlPlant.SelectedValue == "10" || ddlPlant.SelectedValue == "11" || ddlPlant.SelectedValue == "13" || ddlPlant.SelectedValue == "21" || ddlPlant.SelectedValue == "27")
                {
                    grvData.Columns[3].Visible = true;
                    grvData.Columns[4].Visible = true;
                }
                else
                {
                    ddlWarehouse.Enabled = false;
                    grvData.Columns[3].Visible = false;
                    grvData.Columns[4].Visible = false;
                }
            }
            else
            {
                ddlWarehouse.Enabled = false;
                grvData.Columns[3].Visible = false;
                grvData.Columns[4].Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId1", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMrpController, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','8','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlIssueStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlSpecialProcType, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcType','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','13','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlRangeCoverage, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlRangeCoverage','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void BindSalesOrgWiseDropDown()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

        }
        catch (Exception ex)
        { _log.Error("BindSalesOrgWiseDropDown", ex); }
    }

    //private void MRPTypeWiseSetup()
    //{
    //    if (ddlMrpType.SelectedValue == "ND")
    //    {
    //        reqddlMrpController.Visible = false;
    //        reqddlLotSize.Visible = false;
    //        reqtxtReorder.Visible = false;


    //        ddlMrpController.SelectedValue = "";
    //        ddlLotSize.SelectedValue = "";

    //        lableddlMrpController.Visible = false;
    //        lableddlLotSize.Visible = false;
    //        labletxtReorder.Visible = false;

    //        LotSizeWiseValidation();

    //        reqtxtFixedLotSize.Visible = false;
    //        reqtxtMinLotSize.Visible = false;
    //        reqtxtMaxLotSize.Visible = false;
    //        reqtxtRoundingValue.Visible = false;

    //        ddlMrpController.Enabled = false;
    //        ddlLotSize.Enabled = false;
    //        txtReorder.Enabled = false;
    //        txtFixedLotSize.Enabled = false;
    //        txtMinLotSize.Enabled = false;
    //        txtMaxLotSize.Enabled = false;
    //        txtRoundingValue.Enabled = false;



    //    }
    //    else if (ddlMrpType.SelectedValue == "PD")
    //    {
    //        reqddlMrpController.Visible = true;
    //        reqddlLotSize.Visible = true;
    //        reqtxtReorder.Visible = false;

    //        lableddlMrpController.Visible = true;
    //        lableddlLotSize.Visible = true;
    //        labletxtReorder.Visible = false;

    //        reqtxtFixedLotSize.Visible = true;
    //        reqtxtMinLotSize.Visible = true;
    //        reqtxtMaxLotSize.Visible = true;
    //        reqtxtRoundingValue.Visible = true;

    //        ddlMrpController.Enabled = true;
    //        ddlLotSize.Enabled = true;
    //        txtReorder.Enabled = true;
    //        txtFixedLotSize.Enabled = true;
    //        txtMinLotSize.Enabled = true;
    //        txtMaxLotSize.Enabled = true;
    //        txtRoundingValue.Enabled = true;
    //    }
    //    else if (ddlMrpType.SelectedValue == "VB")
    //    {
    //        reqddlMrpController.Visible = true;
    //        reqddlLotSize.Visible = true;
    //        reqtxtReorder.Visible = true;

    //        reqtxtFixedLotSize.Visible = true;
    //        reqtxtMinLotSize.Visible = true;
    //        reqtxtMaxLotSize.Visible = true;
    //        reqtxtRoundingValue.Visible = true;

    //        ddlMrpController.Enabled = true;
    //        ddlLotSize.Enabled = true;
    //        txtReorder.Enabled = true;
    //        txtFixedLotSize.Enabled = true;
    //        txtMinLotSize.Enabled = true;
    //        txtMaxLotSize.Enabled = true;
    //        txtRoundingValue.Enabled = true;
    //        lableddlMrpController.Visible = true;
    //        lableddlLotSize.Visible = true;
    //        labletxtReorder.Visible = true;

    //        LotSizeWiseValidation();
    //    }
    //}

    private void MRPTypeWiseSetup()
    {
        try
        {
            if (ddlMrpType.SelectedValue == "ND")
            {
                reqddlMrpController.Visible = false;
                reqddlLotSize.Visible = false;
                reqtxtReorder.Visible = false;

                lableddlMrpController.Visible = false;
                lableddlLotSize.Visible = false;
                labletxtReorder.Visible = false;

                LotSizeWiseValidation();

                ddlMrpController.Enabled = false;
                ddlLotSize.Enabled = false;
                txtReorder.Enabled = false;
                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = false;
                txtMaxLotSize.Enabled = false;
                txtRoundingValue.Enabled = false;
                //txtPlanningTimeFence.Enabled = false;
                //txtMaxStockLevel.Enabled = false;

            }
            else if (ddlMrpType.SelectedValue == "PD" || ddlMrpType.SelectedValue == "X0")
            {
                reqddlMrpController.Visible = true;
                reqddlLotSize.Visible = true;
                reqtxtReorder.Visible = false;

                lableddlMrpController.Visible = true;
                lableddlLotSize.Visible = true;
                labletxtReorder.Visible = false;

                ddlMrpController.Enabled = true;
                ddlLotSize.Enabled = true;
                txtReorder.Enabled = true;
                txtFixedLotSize.Enabled = true;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;
                //txtPlanningTimeFence.Enabled = true;
                //txtMaxStockLevel.Enabled = true;
            }
            else if (ddlMrpType.SelectedValue == "VB")
            {
                reqddlMrpController.Visible = true;
                reqddlLotSize.Visible = true;
                reqtxtReorder.Visible = true;

                lableddlMrpController.Visible = true;
                lableddlLotSize.Visible = true;
                labletxtReorder.Visible = true;

                ddlMrpController.Enabled = true;
                ddlLotSize.Enabled = true;
                txtReorder.Enabled = true;
                txtFixedLotSize.Enabled = true;
                txtMinLotSize.Enabled = true;
                txtMaxLotSize.Enabled = true;
                txtRoundingValue.Enabled = true;
                //txtPlanningTimeFence.Enabled = true;
                //txtMaxStockLevel.Enabled = true;

                LotSizeWiseValidation();
            }
        }
        catch (Exception ex)
        { _log.Error("MRPTypeWiseSetup", ex); }
    }

    //private void LotSizeWiseValidation()
    //{
    //    if (ddlMrpType.SelectedValue != "ND")
    //    {
    //        if (ddlLotSize.SelectedValue == "FX")
    //        {
    //            reqtxtFixedLotSize.Visible = true;
    //            reqtxtMinLotSize.Visible = false;
    //            reqtxtMaxLotSize.Visible = false;
    //            reqtxtRoundingValue.Visible = false;

    //            labletxtFixedLotSize.Visible = true;
    //            labletxtMinLotSize.Visible = false;
    //            labletxtMaxLotSize.Visible = false;
    //            labletxtRoundingValue.Visible = false;

    //            txtFixedLotSize.Enabled = true;
    //            txtMinLotSize.Enabled = false;
    //            txtMaxLotSize.Enabled = false;
    //            txtRoundingValue.Enabled = false;

    //            txtMinLotSize.Text = "";
    //            txtMaxLotSize.Text = "";
    //            txtRoundingValue.Text = "";
    //        }
    //        else if (ddlLotSize.SelectedValue == "HB")
    //        {
    //            reqtxtFixedLotSize.Visible = false;
    //            reqtxtMinLotSize.Visible = true;
    //            reqtxtMaxLotSize.Visible = true;
    //            reqtxtRoundingValue.Visible = true;

    //            labletxtFixedLotSize.Visible = false;
    //            labletxtMinLotSize.Visible = true;
    //            labletxtMaxLotSize.Visible = true;
    //            labletxtRoundingValue.Visible = false;

    //            txtFixedLotSize.Enabled = false;
    //            txtMinLotSize.Enabled = true;
    //            txtMaxLotSize.Enabled = true;
    //            txtRoundingValue.Enabled = true;

    //            txtFixedLotSize.Text = "";
    //        }
    //        else
    //        {
    //            reqtxtFixedLotSize.Visible = false;
    //            reqtxtMinLotSize.Visible = true;
    //            reqtxtMaxLotSize.Visible = true;
    //            reqtxtRoundingValue.Visible = true;

    //            labletxtFixedLotSize.Visible = false;
    //            labletxtMinLotSize.Visible = true;
    //            labletxtMaxLotSize.Visible = true;
    //            labletxtRoundingValue.Visible = true;

    //            txtFixedLotSize.Enabled = false;
    //            txtMinLotSize.Enabled = true;
    //            txtMaxLotSize.Enabled = true;
    //            txtRoundingValue.Enabled = true;

    //            txtFixedLotSize.Text = "";
    //        }
    //    }
    //    else
    //    {
    //        reqtxtFixedLotSize.Visible = false;
    //        reqtxtMinLotSize.Visible = false;
    //        reqtxtMaxLotSize.Visible = false;
    //        reqtxtRoundingValue.Visible = false;

    //        labletxtFixedLotSize.Visible = false;
    //        labletxtMinLotSize.Visible = false;
    //        labletxtMaxLotSize.Visible = false;
    //        labletxtRoundingValue.Visible = false;

    //        txtFixedLotSize.Enabled = false;
    //        txtMinLotSize.Enabled = false;
    //        txtMaxLotSize.Enabled = false;
    //        txtRoundingValue.Enabled = false;

    //        txtFixedLotSize.Text = "";
    //        txtMinLotSize.Text = "";
    //        txtMaxLotSize.Text = "";
    //        txtRoundingValue.Text = "";
    //    }
    //}

    private void LotSizeWiseValidation()
    {
        try
        {
            if (ddlMrpType.SelectedValue != "ND")
            {
                if (ddlLotSize.SelectedValue == "FX")
                {
                    reqtxtFixedLotSize.Visible = true;
                    labletxtFixedLotSize.Visible = true;

                    //reqtxtMaxStockLevel.Visible = false;
                    //labletxtMaxStockLevel.Visible = false;

                    txtFixedLotSize.Enabled = true;
                    txtMinLotSize.Enabled = false;
                    txtMaxLotSize.Enabled = false;
                    txtRoundingValue.Enabled = false;

                    txtMinLotSize.Text = "";
                    txtMaxLotSize.Text = "";
                    txtRoundingValue.Text = "";
                }
                else if (ddlLotSize.SelectedValue == "HB")
                {
                    reqtxtFixedLotSize.Visible = false;
                    labletxtFixedLotSize.Visible = false;

                    //reqtxtMaxStockLevel.Visible = true;
                    //labletxtMaxStockLevel.Visible = true;

                    txtFixedLotSize.Enabled = false;
                    txtMinLotSize.Enabled = true;
                    txtMaxLotSize.Enabled = true;
                    txtRoundingValue.Enabled = true;

                    txtFixedLotSize.Text = "";
                }
                else
                {
                    reqtxtFixedLotSize.Visible = false;
                    labletxtFixedLotSize.Visible = false;

                    //reqtxtMaxStockLevel.Visible = false;
                    //labletxtMaxStockLevel.Visible = false;

                    txtFixedLotSize.Enabled = false;
                    txtMinLotSize.Enabled = true;
                    txtMaxLotSize.Enabled = true;
                    txtRoundingValue.Enabled = true;

                    txtFixedLotSize.Text = "";
                }
            }
            else
            {
                ddlMrpController.SelectedValue = "";
                ddlLotSize.SelectedValue = "";


                reqtxtFixedLotSize.Visible = false;
                labletxtFixedLotSize.Visible = false;

                //reqtxtMaxStockLevel.Visible = false;
                //labletxtMaxStockLevel.Visible = false;

                txtFixedLotSize.Enabled = false;
                txtMinLotSize.Enabled = false;
                txtMaxLotSize.Enabled = false;
                txtRoundingValue.Enabled = false;

                txtFixedLotSize.Text = "";
            }
        }
        catch (Exception ex)
        { _log.Error("LotSizeWiseValidation", ex); }
    }

    private void MaterialTypeSelection()
    {
        try
        {
            //txtMaterialCode.Text = txtMaterialCode.Text.ToUpper();
            if (Session[StaticKeys.MaterialProcessModuleId] != null)
            {
                pnlMsg.Visible = false;
                if (Session[StaticKeys.MaterialProcessModuleId].ToString() != MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text))
                {
                    lblMsg.Text = "Please enter only " + ddlMaterialAccGrp.SelectedItem.Text;
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                    txtMaterialCode.Text = "";
                }
            }
            else
            {
                ddlMaterialAccGrp.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text);
            }
            BindValuationClass();
            ddlValuationClass.SelectedValue = MaterialHelper.GetDefaultValuationClassByModuleId(ddlMaterialAccGrp.SelectedValue);

            if (Session[StaticKeys.MaterialProcessModuleId].ToString() == "139" || Session[StaticKeys.MaterialProcessModuleId].ToString() == "144" || Session[StaticKeys.MaterialProcessModuleId].ToString() == "145" || Session[StaticKeys.MaterialProcessModuleId].ToString() == "171")
            {
                ddlPriceControlIndicator.SelectedValue = "S";
                ddlPriceControlIndicator.Enabled = true;
            }
            else
            {
                ddlPriceControlIndicator.SelectedValue = "V";
                ddlPriceControlIndicator.Enabled = false;
            }

            if (ddlValuationClass.SelectedValue != "")
                ddlValuationClass.Enabled = false;

            ConfigureControl();
            ddlInspectionType.Enabled = true;
            if (LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
            {
                lableddlInspectionType.Visible = false;
            }
            //MAT_DT26102020
            //MInspectionType();
            //MAT_DT26102020

        }
        catch (Exception ex)
        { _log.Error("MaterialTypeSelection", ex); }
    }

    private bool CheckMatValid(string MaterialNumber, string PlantID)
    {
        bool flg = true;
        try
        {
            foreach (GridViewRow grv in grvData.Rows)
            {
                Label lblMaterialNumber = (Label)grv.FindControl("lblMaterialNumber");
                Label lblPlantID = (Label)grv.FindControl("lblPlantID");
                Label lblStorageLocation = (Label)grv.FindControl("lblStorageLocation");
                if (lblMaterialNumber.Text == MaterialNumber && lblPlantID.Text == PlantID && lblStorageLocation.Text == ddlStorageLocation.SelectedValue)
                {
                    flg = false;
                    break;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("CheckMatValid", ex); }

        return flg;
    }

    private bool Save()
    {
        bool flg = false;
        if (IsValid())
        {

            try
            {
                MaterialExtension ObjMaterialExtension = GetControlsValue();
                objSavedExtnData = ObjMaterialExtensionAccess.GetMaterialExtension(Convert.ToInt32(ObjMaterialExtension.Material_Extension_Id));

                if (ObjMaterialExtension.Material_Number != null)
                {
                    if (CheckMatValid(ObjMaterialExtension.Material_Number, ObjMaterialExtension.Plant_Id) || ObjMaterialExtension.Material_Extension_Id > 0)
                    {
                        if (ObjMaterialExtension.Inspection_Type != "" || !lableddlInspectionType.Visible)
                        {
                            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                            {
                                if (objSavedExtnData.Material_Extension_Id > 0)
                                {
                                    CheckIfChanges(ObjMaterialExtension, objSavedExtnData);
                                }
                            }

                            if (ObjMaterialExtensionAccess.Save(ObjMaterialExtension) > 0)
                            {
                                ClearData();
                                FillFormDataByMHId();
                                flg = true;
                            }
                            else
                            {
                                lblMsg.Text = Messages.GetMessage(-1);
                                pnlMsg.CssClass = "error";
                                pnlMsg.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please Select atleast one Inspection Type to proceed.";
                            pnlMsg.CssClass = "error";
                            pnlMsg.Visible = true;
                            flg = false;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Material/Plant/Stor. Loc. already exists, please enter another combination.";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Please enter Material Number to proceed.";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Save", ex);
            }
            return flg;
        }
        return flg;
    }

    private void CheckIfChanges(MaterialExtension NewExtnsnData, MaterialExtension oldExtnsnData)
    {
        try
        {
            if (NewExtnsnData.Material_Extension_Id > 0 && oldExtnsnData.Material_Extension_Id > 0)
            {
                if (NewExtnsnData.Material_Type != oldExtnsnData.Material_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Type</td> <td>" + oldExtnsnData.Material_Type + "</td><td>" + NewExtnsnData.Material_Type + "</td></tr>";
                if (NewExtnsnData.Material_Short_Description != oldExtnsnData.Material_Short_Description)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Description</td> <td>" + oldExtnsnData.Material_Short_Description + "</td><td>" + NewExtnsnData.Material_Short_Description + "</td></tr>";
                if (NewExtnsnData.Storage_Location != oldExtnsnData.Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Location</td> <td>" + oldExtnsnData.Storage_Location + "</td><td>" + NewExtnsnData.Storage_Location + "</td></tr>";
                if (NewExtnsnData.Sales_Organization_Id != oldExtnsnData.Sales_Organization_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Organization</td> <td>" + oldExtnsnData.Sales_Organization_Id + "</td><td>" + NewExtnsnData.Sales_Organization_Id + "</td></tr>";
                if (NewExtnsnData.Distribution_Channel_ID != oldExtnsnData.Distribution_Channel_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distribution Channel</td> <td>" + oldExtnsnData.Distribution_Channel_ID + "</td><td>" + NewExtnsnData.Distribution_Channel_ID + "</td></tr>";
                if (NewExtnsnData.Mat_Pricing_Group != oldExtnsnData.Mat_Pricing_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Old Material Pricing Group</td> <td>" + oldExtnsnData.Mat_Pricing_Group + "</td><td>" + NewExtnsnData.Mat_Pricing_Group + "</td></tr>";
                if (NewExtnsnData.Acc_Assignment_Grp != oldExtnsnData.Acc_Assignment_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Acc Assignment Group</td> <td>" + oldExtnsnData.Acc_Assignment_Grp + "</td><td>" + NewExtnsnData.Acc_Assignment_Grp + "</td></tr>";
                if (NewExtnsnData.Purchasing_Group != oldExtnsnData.Purchasing_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Purchasing Group</td> <td>" + oldExtnsnData.Purchasing_Group + "</td><td>" + NewExtnsnData.Purchasing_Group + "</td></tr>";
                if (NewExtnsnData.MRP_Type != oldExtnsnData.MRP_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MRP Type</td> <td>" + oldExtnsnData.MRP_Type + "</td><td>" + NewExtnsnData.MRP_Type + "</td></tr>";
                if (NewExtnsnData.MRP_Controller != oldExtnsnData.MRP_Controller)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MRP Controller</td> <td>" + oldExtnsnData.MRP_Controller + "</td><td>" + NewExtnsnData.MRP_Controller + "</td></tr>";
                if (NewExtnsnData.Reorder_Point != oldExtnsnData.Reorder_Point)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Reorder Point</td> <td>" + oldExtnsnData.Reorder_Point + "</td><td>" + NewExtnsnData.Reorder_Point + "</td></tr>";
                if (NewExtnsnData.Lot_Size != oldExtnsnData.Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Lot Size</td> <td>" + oldExtnsnData.Lot_Size + "</td><td>" + NewExtnsnData.Lot_Size + "</td></tr>";
                if (NewExtnsnData.Min_Lot_Size != oldExtnsnData.Min_Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Lot Size</td> <td>" + oldExtnsnData.Min_Lot_Size + "</td><td>" + NewExtnsnData.Min_Lot_Size + "</td></tr>";
                if (NewExtnsnData.Max_Lot_Size != oldExtnsnData.Max_Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Max Lot Size</td> <td>" + oldExtnsnData.Max_Lot_Size + "</td><td>" + NewExtnsnData.Max_Lot_Size + "</td></tr>";
                if (NewExtnsnData.Fixed_Lot_Size != oldExtnsnData.Fixed_Lot_Size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Fixed Lot Size</td> <td>" + oldExtnsnData.Fixed_Lot_Size + "</td><td>" + NewExtnsnData.Fixed_Lot_Size + "</td></tr>";
                if (NewExtnsnData.Rounding_Value != oldExtnsnData.Rounding_Value)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Rounding Value</td> <td>" + oldExtnsnData.Rounding_Value + "</td><td>" + NewExtnsnData.Rounding_Value + "</td></tr>";
                if (NewExtnsnData.Issue_Storage_Location != oldExtnsnData.Issue_Storage_Location)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Issue Storage Location</td> <td>" + oldExtnsnData.Issue_Storage_Location + "</td><td>" + NewExtnsnData.Issue_Storage_Location + "</td></tr>";
                if (NewExtnsnData.GR_Processing_Time != oldExtnsnData.GR_Processing_Time)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>GR Processing Time</td> <td>" + oldExtnsnData.GR_Processing_Time + "</td><td>" + NewExtnsnData.GR_Processing_Time + "</td></tr>";
                if (NewExtnsnData.Planned_Delivery_Time_Days != oldExtnsnData.Planned_Delivery_Time_Days)
                    Session[StaticKeys.ApprovalNote] += "<tr><td> Planned Delivery Time </td> <td>" + oldExtnsnData.Planned_Delivery_Time_Days + "</td><td>" + NewExtnsnData.Planned_Delivery_Time_Days + "</td></tr>";
                if (NewExtnsnData.Profit_Center != oldExtnsnData.Profit_Center)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Profit Center</td> <td>" + oldExtnsnData.Profit_Center + "</td><td>" + NewExtnsnData.Profit_Center + "</td></tr>";
                if (NewExtnsnData.Valuation_Class != oldExtnsnData.Valuation_Class)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Valuation Class</td> <td>" + oldExtnsnData.Valuation_Class + "</td><td>" + NewExtnsnData.Valuation_Class + "</td></tr>";
                if (NewExtnsnData.Price_Ctrl_Indicator != oldExtnsnData.Price_Ctrl_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Price Ctrl Indicator</td> <td>" + oldExtnsnData.Price_Ctrl_Indicator + "</td><td>" + NewExtnsnData.Price_Ctrl_Indicator + "</td></tr>";
                if (NewExtnsnData.Spl_Procurement_Type != oldExtnsnData.Spl_Procurement_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Spl Procurement Type</td> <td>" + oldExtnsnData.Spl_Procurement_Type + "</td><td>" + NewExtnsnData.Spl_Procurement_Type + "</td></tr>";
                if (NewExtnsnData.Warehouse_ID != oldExtnsnData.Warehouse_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Warehouse ID</td> <td>" + oldExtnsnData.Warehouse_ID + "</td><td>" + NewExtnsnData.Warehouse_ID + "</td></tr>";
                if (NewExtnsnData.Storage_Type_ID != oldExtnsnData.Storage_Type_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Type ID</td> <td>" + oldExtnsnData.Storage_Type_ID + "</td><td>" + NewExtnsnData.Storage_Type_ID + "</td></tr>";
                if (NewExtnsnData.Capacity_Usage != oldExtnsnData.Capacity_Usage)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Capacity Usage</td> <td>" + oldExtnsnData.Capacity_Usage + "</td><td>" + NewExtnsnData.Capacity_Usage + "</td></tr>";
                if (NewExtnsnData.Interval_Nxt_Inspection != oldExtnsnData.Interval_Nxt_Inspection)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Interval to Nxt Inspection</td> <td>" + oldExtnsnData.Interval_Nxt_Inspection + "</td><td>" + NewExtnsnData.Interval_Nxt_Inspection + "</td></tr>";
                if (NewExtnsnData.WM_Unit_Measure != oldExtnsnData.WM_Unit_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>WM Unit Measure</td> <td>" + oldExtnsnData.WM_Unit_Measure + "</td><td>" + NewExtnsnData.WM_Unit_Measure + "</td></tr>";
                if (NewExtnsnData.Stor_Type_Ind_Stock_Placement != oldExtnsnData.Stor_Type_Ind_Stock_Placement)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Stor Type Ind Stock Placement</td> <td>" + oldExtnsnData.Stor_Type_Ind_Stock_Placement + "</td><td>" + NewExtnsnData.Stor_Type_Ind_Stock_Placement + "</td></tr>";
                if (NewExtnsnData.Stor_Type_Ind_Stock_Removal != oldExtnsnData.Stor_Type_Ind_Stock_Removal)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Stor Type Ind Stock Removal</td> <td>" + oldExtnsnData.Stor_Type_Ind_Stock_Removal + "</td><td>" + NewExtnsnData.Stor_Type_Ind_Stock_Removal + "</td></tr>";
                if (NewExtnsnData.Storage_Section_Ind != oldExtnsnData.Storage_Section_Ind)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Section Ind</td> <td>" + oldExtnsnData.Storage_Section_Ind + "</td><td>" + NewExtnsnData.Storage_Section_Ind + "</td></tr>";
                if (NewExtnsnData.Loading_Equipment_Quantity != oldExtnsnData.Loading_Equipment_Quantity)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Equipment Quantity</td> <td>" + oldExtnsnData.Loading_Equipment_Quantity + "</td><td>" + NewExtnsnData.Loading_Equipment_Quantity + "</td></tr>";
                if (NewExtnsnData.Loading_Equipment_Quantity1 != oldExtnsnData.Loading_Equipment_Quantity1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Equipment Quantity1</td> <td>" + oldExtnsnData.Loading_Equipment_Quantity1 + "</td><td>" + NewExtnsnData.Loading_Equipment_Quantity1 + "</td></tr>";
                if (NewExtnsnData.Loading_Equipment_Quantity2 != oldExtnsnData.Loading_Equipment_Quantity2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Equipment Quantity2</td> <td>" + oldExtnsnData.Loading_Equipment_Quantity2 + "</td><td>" + NewExtnsnData.Loading_Equipment_Quantity2 + "</td></tr>";
                if (NewExtnsnData.Unit_Loading_Equip_Quan != oldExtnsnData.Unit_Loading_Equip_Quan)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Loading Equip Quan</td> <td>" + oldExtnsnData.Unit_Loading_Equip_Quan + "</td><td>" + NewExtnsnData.Unit_Loading_Equip_Quan + "</td></tr>";
                if (NewExtnsnData.Unit_Loading_Equip_Quan1 != oldExtnsnData.Unit_Loading_Equip_Quan1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Loading Equip Quan1</td> <td>" + oldExtnsnData.Unit_Loading_Equip_Quan1 + "</td><td>" + NewExtnsnData.Unit_Loading_Equip_Quan1 + "</td></tr>";
                if (NewExtnsnData.Unit_Loading_Equip_Quan2 != oldExtnsnData.Unit_Loading_Equip_Quan2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Loading Equip Quan2</td> <td>" + oldExtnsnData.Unit_Loading_Equip_Quan2 + "</td><td>" + NewExtnsnData.Unit_Loading_Equip_Quan2 + "</td></tr>";
                if (NewExtnsnData.Storage_Unit_Type != oldExtnsnData.Storage_Unit_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Unit Type</td> <td>" + oldExtnsnData.Storage_Unit_Type + "</td><td>" + NewExtnsnData.Storage_Unit_Type + "</td></tr>";
                if (NewExtnsnData.Storage_Unit_Type1 != oldExtnsnData.Storage_Unit_Type1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Unit Type1</td> <td>" + oldExtnsnData.Storage_Unit_Type1 + "</td><td>" + NewExtnsnData.Storage_Unit_Type1 + "</td></tr>";
                if (NewExtnsnData.Storage_Unit_Type2 != oldExtnsnData.Storage_Unit_Type2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Unit Type2</td> <td>" + oldExtnsnData.Storage_Unit_Type2 + "</td><td>" + NewExtnsnData.Storage_Unit_Type2 + "</td></tr>";
                if (NewExtnsnData.Remarks != oldExtnsnData.Remarks)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldExtnsnData.Remarks + "</td><td>" + NewExtnsnData.Remarks + "</td></tr>";
            }

            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChanges", ex);
        }
    }

    private MaterialExtension GetControlsValue()
    {
        MaterialExtension ObjMaterialExtension = new MaterialExtension();
        Utility objUtil = new Utility();

        try
        {
            //Label lblMaterialExtensionId = (Label)grv.FindControl("lblMaterialExtensionId" + ext);

            ObjMaterialExtension.Material_Extension_Id = SafeTypeHandling.ConvertStringToInt32(lblMatExtensionId.Text);
            ObjMaterialExtension.Master_Header_Id = SafeTypeHandling.ConvertStringToInt32(lblMasterHeaderId.Text);

            ObjMaterialExtension.Material_Number = txtMaterialCode.Text;
            ObjMaterialExtension.Material_Type = ddlMaterialAccGrp.SelectedValue;
            ObjMaterialExtension.Material_Short_Description = txtMaterialDescription.Text;

            ObjMaterialExtension.Plant_Id = ddlPlant.SelectedValue;

            ObjMaterialExtension.Storage_Location = ddlStorageLocation.SelectedValue;
            ObjMaterialExtension.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjMaterialExtension.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;
            ObjMaterialExtension.Mat_Pricing_Group = ddlMaterialPGroup.SelectedValue;
            ObjMaterialExtension.Acc_Assignment_Grp = ddlAccountAssignment.SelectedValue;
            ObjMaterialExtension.Purchasing_Group = ddlPurchasingGroup.SelectedValue;
            ObjMaterialExtension.MRP_Type = ddlMrpType.SelectedValue;
            ObjMaterialExtension.MRP_Controller = ddlMrpController.SelectedValue;
            ObjMaterialExtension.Reorder_Point = txtReorder.Text;
            ObjMaterialExtension.Lot_Size = ddlLotSize.SelectedValue;
            ObjMaterialExtension.Min_Lot_Size = txtMinLotSize.Text;
            ObjMaterialExtension.Max_Lot_Size = txtMaxLotSize.Text;
            ObjMaterialExtension.Fixed_Lot_Size = txtFixedLotSize.Text;
            ObjMaterialExtension.Rounding_Value = txtRoundingValue.Text;
            ObjMaterialExtension.Issue_Storage_Location = ddlIssueStorageLocation.SelectedValue;
            ObjMaterialExtension.GR_Processing_Time = txtGRProcessingTime.Text;
            ObjMaterialExtension.Planned_Delivery_Time_Days = txtPlannedDeleveryTime.Text;
            ObjMaterialExtension.Profit_Center = ddlProfitCenter.SelectedValue;
            ObjMaterialExtension.Valuation_Class = ddlValuationClass.SelectedValue;
            ObjMaterialExtension.Price_Ctrl_Indicator = ddlPriceControlIndicator.SelectedValue;

            ObjMaterialExtension.Spl_Procurement_Type = ddlSpecialProcType.SelectedValue;
            ObjMaterialExtension.Inspection_Type = GetSelectedCheckedValue(ddlInspectionType) == null ? "" : GetSelectedCheckedValue(ddlInspectionType);
            ObjMaterialExtension.Warehouse_ID = ddlWarehouse.SelectedValue;
            ObjMaterialExtension.Storage_Type_ID = ddlStorageType.SelectedValue;
            ObjMaterialExtension.Capacity_Usage = txtCapacityUsage.Text;
            ObjMaterialExtension.WM_Unit_Measure = ddlWareHouseMangUnit.SelectedValue;
            ObjMaterialExtension.Stor_Type_Ind_Stock_Removal = ddlStorageTypeIndiSPlaceRemoval.SelectedValue;
            ObjMaterialExtension.Storage_Section_Ind = ddlStorageSectIndi.SelectedValue;
            ObjMaterialExtension.Stor_Type_Ind_Stock_Placement = ddlStorageTypeIndiSPlacement.SelectedValue;

            ObjMaterialExtension.Loading_Equipment_Quantity = txtLoadingEquipQuantity.Text;
            ObjMaterialExtension.Loading_Equipment_Quantity1 = txtLoadingEquipQuantity2.Text;
            ObjMaterialExtension.Loading_Equipment_Quantity2 = txtloadingEquipQuantity3.Text; ;
            ObjMaterialExtension.Unit_Loading_Equip_Quan = ddlUnitMeasureLoadingEquip.SelectedValue;
            ObjMaterialExtension.Unit_Loading_Equip_Quan1 = ddlUnitMeasureLoadingEquip2.SelectedValue;
            ObjMaterialExtension.Unit_Loading_Equip_Quan2 = ddlUnitMeasureLoadingEquip3.SelectedValue;
            ObjMaterialExtension.Storage_Unit_Type = ddlStorageUnitType.SelectedValue;
            ObjMaterialExtension.Storage_Unit_Type1 = ddlStorageUnitType2.SelectedValue;
            ObjMaterialExtension.Storage_Unit_Type2 = ddlStorageUnitType3.SelectedValue;
            ObjMaterialExtension.Interval_Nxt_Inspection = txtIntervalNPInspector.Text;

            //ObjMaterialExtension.Reason_For_Creation = ddlReason.SelectedValue;
            ObjMaterialExtension.Remarks = txtRemarks.Text;
            //MSE_8300002156
            ObjMaterialExtension.Is_QM_in_Procurement = chkQMProcurmentActive.Checked == true ? "1" : "0";
            ObjMaterialExtension.Ctrl_Key_QM_Procurement = ddlControlQualityMang.SelectedValue;
            ObjMaterialExtension.Certificate_Type = ddlCertificateType.SelectedValue;
            ObjMaterialExtension.Range_Coverage_Profile = ddlRangeCoverage.SelectedValue;
            //MSE_8300002156
            //MAT_DT26102020
            ObjMaterialExtension.MInspType = ddlMInspectionType.SelectedValue;
            //MAT_DT26102020
            ObjMaterialExtension.IsActive = "1";
            ObjMaterialExtension.UserId = lblUserId.Text;
            ObjMaterialExtension.TodayDate = objUtil.GetDate();
            ObjMaterialExtension.IPAddress = objUtil.GetIpAddress();
            //NRDT02032023 Start
            ObjMaterialExtension.MatAuthGrpActQM = ddlMaterialAuthGroupActQM.SelectedValue;
            //NRDT02032023 End
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjMaterialExtension;
    }


    private bool IsValid()
    {
        Utility objUtil = new Utility();
        bool flg = false;
        try
        {
            if ((ddlControlQualityMang.SelectedValue == "0" || ddlControlQualityMang.SelectedValue == "")
               && (chkQMProcurmentActive.Checked == true)
               )
            {
                reqddlControlQualityMang.Enabled = true;
                lblMsg.Text = "Control Key for Quality Management in Procurement cannot be blank.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else if ((ddlControlQualityMang.SelectedValue != "0" && ddlControlQualityMang.SelectedValue != "")
               && (chkQMProcurmentActive.Checked == false))
            {
                chkQMProcurmentActive.Checked = true;
                lblMsg.Text = "Please select QM in Procurement is Active.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else
            {
                flg = true;
                //lblMsg.Text = Messages.GetMessage(-1);
                //pnlMsg.CssClass = "error";
                //pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("IsValid", ex);
        }
        return flg;
    }


    public void BindValuationClass()
    {
        try
        {
            string AccountCat = MaterialHelper.GetAccountCategoryByModuleId(ddlMaterialAccGrp.SelectedValue);
            helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass','1','" + AccountCat + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindValuationClass", ex); }
    }

    private void FillControlData()
    {
        Utility objUtil = new Utility();

        MaterialExtension ObjMaterialExtension = ObjMaterialExtensionAccess.GetMaterialExtension(Convert.ToInt32(lblMatExtensionId.Text));
        try
        {
            if (ObjMaterialExtension.Material_Extension_Id > 0)
            {
                if (Session[StaticKeys.MaterialProcessModuleId] != null)
                {
                    ddlMaterialAccGrp.SelectedValue = Session[StaticKeys.MaterialProcessModuleId].ToString();
                }
                //Start Add DT08092022
                ConfigureControl();
                //End Add DT08092022

                txtMaterialCode.Text = ObjMaterialExtension.Material_Number;
                ddlMaterialAccGrp.SelectedValue = ObjMaterialExtension.Material_Type;
                txtMaterialDescription.Text = ObjMaterialExtension.Material_Short_Description;

                txtMaterialCode.Enabled = false;
                ddlPlant.Enabled = false;

                BindValuationClass();

                ddlPlant.SelectedValue = ObjMaterialExtension.Plant_Id;
                BindPlantWiseDropDown();

                ddlStorageLocation.SelectedValue = ObjMaterialExtension.Storage_Location;
                ddlSalesOrginization.SelectedValue = ObjMaterialExtension.Sales_Organization_Id;
                BindSalesOrgWiseDropDown();

                ddlDistributionChannel.SelectedValue = ObjMaterialExtension.Distribution_Channel_ID;
                ddlMaterialPGroup.SelectedValue = ObjMaterialExtension.Mat_Pricing_Group;
                ddlAccountAssignment.SelectedValue = ObjMaterialExtension.Acc_Assignment_Grp;
                ddlPurchasingGroup.SelectedValue = ObjMaterialExtension.Purchasing_Group;
                ddlMrpType.SelectedValue = ObjMaterialExtension.MRP_Type;
                MRPTypeWiseSetup();

                ddlMrpController.SelectedValue = ObjMaterialExtension.MRP_Controller;
                txtReorder.Text = ObjMaterialExtension.Reorder_Point;
                ddlLotSize.SelectedValue = ObjMaterialExtension.Lot_Size;
                LotSizeWiseValidation();

                txtMinLotSize.Text = ObjMaterialExtension.Min_Lot_Size;
                txtMaxLotSize.Text = ObjMaterialExtension.Max_Lot_Size;
                txtFixedLotSize.Text = ObjMaterialExtension.Fixed_Lot_Size;
                txtRoundingValue.Text = ObjMaterialExtension.Rounding_Value;
                ddlIssueStorageLocation.SelectedValue = ObjMaterialExtension.Issue_Storage_Location;
                txtGRProcessingTime.Text = ObjMaterialExtension.GR_Processing_Time;
                txtPlannedDeleveryTime.Text = ObjMaterialExtension.Planned_Delivery_Time_Days;
                ddlProfitCenter.SelectedValue = ObjMaterialExtension.Profit_Center;
                ddlValuationClass.SelectedValue = ObjMaterialExtension.Valuation_Class;
                ddlPriceControlIndicator.SelectedValue = ObjMaterialExtension.Price_Ctrl_Indicator;

                ddlSpecialProcType.SelectedValue = ObjMaterialExtension.Spl_Procurement_Type;
                SetSelectedValue(ddlInspectionType, ObjMaterialExtension.Inspection_Type);
                ddlWarehouse.SelectedValue = ObjMaterialExtension.Warehouse_ID;
                helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','20','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlStorageType.SelectedValue = ObjMaterialExtension.Storage_Type_ID;
                txtCapacityUsage.Text = ObjMaterialExtension.Capacity_Usage;
                ddlWareHouseMangUnit.SelectedValue = ObjMaterialExtension.WM_Unit_Measure;
                ddlStorageTypeIndiSPlaceRemoval.SelectedValue = ObjMaterialExtension.Stor_Type_Ind_Stock_Removal;
                ddlStorageSectIndi.SelectedValue = ObjMaterialExtension.Storage_Section_Ind;
                ddlStorageTypeIndiSPlacement.SelectedValue = ObjMaterialExtension.Stor_Type_Ind_Stock_Placement;

                txtLoadingEquipQuantity.Text = ObjMaterialExtension.Loading_Equipment_Quantity;
                txtLoadingEquipQuantity2.Text = ObjMaterialExtension.Loading_Equipment_Quantity1;
                txtloadingEquipQuantity3.Text = ObjMaterialExtension.Loading_Equipment_Quantity2;
                ddlUnitMeasureLoadingEquip.SelectedValue = ObjMaterialExtension.Unit_Loading_Equip_Quan;
                ddlUnitMeasureLoadingEquip2.SelectedValue = ObjMaterialExtension.Unit_Loading_Equip_Quan1;
                ddlUnitMeasureLoadingEquip3.SelectedValue = ObjMaterialExtension.Unit_Loading_Equip_Quan2;
                ddlStorageUnitType.SelectedValue = ObjMaterialExtension.Storage_Unit_Type;
                ddlStorageUnitType2.SelectedValue = ObjMaterialExtension.Storage_Unit_Type1;
                ddlStorageUnitType3.SelectedValue = ObjMaterialExtension.Storage_Unit_Type2;

                txtIntervalNPInspector.Text = ObjMaterialExtension.Interval_Nxt_Inspection;
                //ddlReason.SelectedValue = ObjMaterialExtension.Reason_For_Creation;
                txtRemarks.Text = ObjMaterialExtension.Remarks;
                //MSE_8300002156
                chkQMProcurmentActive.Checked = ObjMaterialExtension.Is_QM_in_Procurement.ToLower() == "true" ? true : false;
                ddlControlQualityMang.SelectedValue = ObjMaterialExtension.Ctrl_Key_QM_Procurement;
                ddlCertificateType.SelectedValue = ObjMaterialExtension.Certificate_Type;
                //NRDT02032023 Start
                ddlMaterialAuthGroupActQM.SelectedValue = ObjMaterialExtension.MatAuthGrpActQM;
                //NRDT02032023 End
                ConfigureControlQMCtrl(ObjMaterialExtension.IsDraf);
                ConfigCertificateType(ddlControlQualityMang.SelectedValue, ObjMaterialExtension.IsDraf);
                //ddlRangeCoverage.SelectedValue = ObjMaterialExtension.Range_Coverage_Profile;
                //MSE_8300002156
                //MAT_DT26102020
                ddlMInspectionType.SelectedValue = ObjMaterialExtension.MInspType;
                //MAT_DT26102020

                //Start Remove DT08092022
                //ConfigureControl();
                //Start Remove DT08092022

                ddlInspectionType.Enabled = true;
                //MAT_DT26102020
                MInspectionType();
                //MAT_DT26102020
            }
            else
            {
                helperAccess.PopuplateDropDownCheckBox(ddlInspectionType, "pr_GetDropDownListByControlNameModuleType 'M','ddlInspectionType'", "LookUp_Desc", "LookUp_Code");
                if (Session[StaticKeys.MaterialProcessModuleId] != null)
                {
                    ddlMaterialAccGrp.SelectedValue = Session[StaticKeys.MaterialProcessModuleId].ToString();
                }
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                if (Session[StaticKeys.MatPurchasingGroupId].ToString() != "")
                    ddlPurchasingGroup.SelectedValue = Session[StaticKeys.MatPurchasingGroupId].ToString();

                ddlMrpType.SelectedValue = "ND";
                BindPlantWiseDropDown();
                //MAT_DT26102020
                if (ddlMInspectionType.SelectedValue == "1")
                {
                    //MAT_DT26102020
                    if (ddlMaterialAccGrp.SelectedValue == "138")
                    {
                        if (ddlPlant.SelectedValue != "9" && ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                            //MSC_8300001775 Start Comment 
                            //SetSelectedValue(ddlInspectionType, "Z1      ");
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            SetSelectedValue(ddlInspectionType, "Z1");
                        //MSC_8300001775 End

                    }
                    else if (ddlMaterialAccGrp.SelectedValue == "147")
                    {
                        //ddlInspectionType.SelectedValue = "";
                        ddlInspectionType.SelectedIndex = -1;
                    }
                    else if (ddlMaterialAccGrp.SelectedValue == "170")
                    {
                        if (ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                            //MSC_8300001775 Start Comment 
                            //SetSelectedValue(ddlInspectionType, "Z1      ");
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            SetSelectedValue(ddlInspectionType, "Z1");
                        //MSC_8300001775 End

                    }
                    else if (ddlMaterialAccGrp.SelectedValue == "162" || ddlMaterialAccGrp.SelectedValue == "164")
                    {
                        //chkQMProcurmentActive.Checked = true;
                        //chkQMProcurmentActive.Enabled = false;

                        if (ddlPlant.SelectedValue != "")
                        {
                            string SalesOrgID, DistributionChannelID = "";
                            int ret;
                            ObjMaterialExtensionAccess.GetDefaultSalesOrganization(ddlPlant.SelectedValue, SafeTypeHandling.ConvertStringToInt32(ddlMaterialAccGrp.SelectedValue), out SalesOrgID, out DistributionChannelID, out ret);
                            if (ret > 0)
                            {
                                Session[StaticKeys.MaterialDistChnlId] = DistributionChannelID;
                                Session[StaticKeys.MaterialSalesOrgId] = SalesOrgID;
                            }
                            if (Session[StaticKeys.MaterialSalesOrgId] != null)
                            {
                                ddlSalesOrginization.SelectedValue = Session[StaticKeys.MaterialSalesOrgId].ToString();
                                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','S1','" + lblMatExtensionId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

                                if (Session[StaticKeys.MaterialDistChnlId] != null)
                                    ddlDistributionChannel.SelectedValue = Session[StaticKeys.MaterialDistChnlId].ToString();
                            }
                        }

                        // Check for Pune Plant
                        //if (ddlPlant.SelectedValue != "9" && ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                        if (!LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
                        {
                            //MSC_8300001775 Start Comment 
                            //if (ddlMaterialAccGrp.Text == "164" && ddlPlant.SelectedValue.ToString() == "10")
                            //    SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,89      ,Z3      ");
                            //else
                            //    SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,89      ");
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            if (ddlMaterialAccGrp.Text == "164" && ddlPlant.SelectedValue.ToString() == "10")
                                SetSelectedValue(ddlInspectionType, "01,05,08,09,89,Z3");
                            else
                                SetSelectedValue(ddlInspectionType, "01,05,08,09,89");
                            //MSC_8300001775 End


                            //ddlControlQualityMang.SelectedValue = "Z102    ";
                            //ddlCertificateType.SelectedValue = "9999";
                            txtIntervalNPInspector.Text = "360";
                        }
                        //else
                        //{
                        //    ddlControlQualityMang.SelectedValue = "0000    ";
                        //    ddlCertificateType.SelectedValue = "";
                        //}
                        ddlAccountAssignment.SelectedValue = "Z5";

                    }

                    //MAT_DT26102020
                }
                else
                {
                    ddlInspectionType.SelectedIndex = -1;
                }
                //MAT_DT26102020

                MRPTypeWiseSetup();
                LotSizeWiseValidation();
                //CCP-MM-941-22-0082
                ConfigureControlQMCtrl("1");
                //CCP-MM-941-22-0082
            }

            DisplayInspectionType();

            ddlMaterialAccGrp.Enabled = false;
            if (Session[StaticKeys.MatPurchasingGroupId].ToString() != "")
                //ddlPurchasingGroup.Enabled = false;

                ddlPurchasingGroup.Enabled = true;



        }
        catch (Exception ex)
        {
            _log.Error("FillControlData", ex);
            //throw ex;
        }
    }


    /// <summary>
    /// MAT_DT26102020
    /// </summary>
    private void MInspectionType()
    {
        try
        {
            if (ddlMInspectionType.SelectedValue == "1")
            {
                ddlInspectionType.Enabled = true;
                lableddlInspectionType.Visible = true;
            }
            else
            {
                ddlInspectionType.Enabled = false;
                ClearSelectedValue(ddlInspectionType);
                lableddlInspectionType1.Text = "";
                lableddlInspectionType.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("MInspectionType", ex); }
    }

    /// <summary>
    /// MAT_DT26102020
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMInspectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            MInspectionType();
        }
        catch (Exception ex)
        { _log.Error("ddlMInspectionType_SelectedIndexChanged", ex); }

    }

    //private MaterialExtension GetMaterialExtensionfromDR(DataRow dr)
    //{
    //    MaterialExtension ObjMaterialExtension = new MaterialExtension();

    //    ObjMaterialExtension.Material_Extension_Id = dr["Material_Extension_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Material_Extension_Id"].ToString());
    //    ObjMaterialExtension.Master_Header_Id = dr["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dr["Master_Header_Id"].ToString());

    //    ObjMaterialExtension.Material_Number = dr["Material_Number"].ToString();
    //    ObjMaterialExtension.Material_Type = dr["Material_Type"].ToString();
    //    ObjMaterialExtension.Material_Short_Description = dr["Material_Short_Description"].ToString();

    //    ObjMaterialExtension.Plant_Id = dr["Plant_Id"].ToString();
    //    ObjMaterialExtension.Storage_Location = dr["Storage_Location"].ToString();
    //    ObjMaterialExtension.Sales_Organization_Id = dr["Sales_Organization_Id"].ToString();
    //    ObjMaterialExtension.Distribution_Channel_ID = dr["Distribution_Channel_ID"].ToString();
    //    ObjMaterialExtension.Mat_Pricing_Group = dr["Mat_Pricing_Group"].ToString();
    //    ObjMaterialExtension.Acc_Assignment_Grp = dr["Acc_Assignment_Grp"].ToString();
    //    ObjMaterialExtension.Purchasing_Group = dr["Purchasing_Group"].ToString();
    //    ObjMaterialExtension.MRP_Type = dr["MRP_Type"].ToString();
    //    ObjMaterialExtension.MRP_Controller = dr["MRP_Controller"].ToString();
    //    ObjMaterialExtension.Reorder_Point = dr["Reorder_Point"].ToString();
    //    ObjMaterialExtension.Lot_Size = dr["Lot_Size"].ToString();
    //    ObjMaterialExtension.Min_Lot_Size = dr["Min_Lot_Size"].ToString();
    //    ObjMaterialExtension.Max_Lot_Size = dr["Max_Lot_Size"].ToString();
    //    ObjMaterialExtension.Fixed_Lot_Size = dr["Fixed_Lot_Size"].ToString();
    //    ObjMaterialExtension.Rounding_Value = dr["Rounding_Value"].ToString();
    //    ObjMaterialExtension.Issue_Storage_Location = dr["Issue_Storage_Location"].ToString();
    //    ObjMaterialExtension.GR_Processing_Time = dr["GR_Processing_Time"].ToString();
    //    ObjMaterialExtension.Planned_Delivery_Time_Days = dr["Planned_Delivery_Time_Days"].ToString();
    //    ObjMaterialExtension.Profit_Center = dr["Profit_Center"].ToString();
    //    ObjMaterialExtension.Valuation_Class = dr["Valuation_Class"].ToString();
    //    ObjMaterialExtension.Price_Ctrl_Indicator = dr["Price_Ctrl_Indicator"].ToString();

    //    return ObjMaterialExtension;
    //}

    private void ConfigureControl()
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        try
        {
            if (ddlMaterialAccGrp.SelectedValue != "")
            {
                Session[StaticKeys.SelectedModulePlantGrp] = ObjMasterAccess.GetSelectedModulePlantGrpByPlantGrp("1", ddlMaterialAccGrp.SelectedValue, "M");

                string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();

                SectionConfiguration.Material_Extension_Data obj = new SectionConfiguration.Material_Extension_Data();
                SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
            }

        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
        try
        {
            lableddlAccountAssignment.Visible = true;
            reqddlAccountAssignment.Visible = true;
            ddlAccountAssignment.Enabled = true;
            lableddlSalesOrginization.Visible = true;
            reqddlSalesOrginization.Visible = true;
            ddlSalesOrginization.Enabled = true;
            lableddlDistributionChannel.Visible = true;
            reqddlDistributionChannel.Visible = true;
            ddlDistributionChannel.Enabled = true;
        }
        catch (Exception ex) { _log.Error("ConfigureControl1", ex); }
    }

    private void DisplayInspectionType()
    {
        try
        {
            string InspectionType = GetSelectedCheckedValue(ddlInspectionType);
            if (InspectionType != null)
            {
                lableddlInspectionType1.Text = "Inspection Type :  " + InspectionType.Substring(0, InspectionType.Length - 1);
            }
            else
            {
                lableddlInspectionType1.Text = "";
            }
            //"<b><u>Inspection Type</u> :  </b>" + InspectionType.Substring(0, InspectionType.Length - 1);
            reqtxtIntervalNPInspector.Visible = false;
            labletxtIntervalNPInspector.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("DisplayInspectionType", ex); }
    }

    private bool CheckInspectionType()
    {
        bool flg = true;
        try
        {
            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162 || SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164)
            {
                string InspectionType = GetSelectedCheckedValue(ddlInspectionType);

                if (InspectionType != null)
                {
                    string[] str = InspectionType.Split(',');

                    foreach (string insVal in str)
                    {
                        //MSC_8300001775 Start Comment
                        //if (insVal == "09      ")
                        //{
                        //    if (txtIntervalNPInspector.Text == "")
                        //        flg = false;
                        //    break;
                        //}

                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        if (insVal == "09")
                        {
                            if (txtIntervalNPInspector.Text == "")
                                flg = false;
                            break;
                        }

                        //MSC_8300001775 End
                    }
                }
                else if (InspectionType == null)
                {
                    if (txtIntervalNPInspector.Text != "")
                        flg = false;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("CheckInspectionType", ex); }
        return flg;
    }

    #endregion


    #region MME_8300002156

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
                ddlTypeOfMassUpdm.SelectedValue = dstData.Tables[0].Rows[0]["Document_Type"].ToString();
                Session[StaticKeys.TypeOfMassUpdm] = ddlTypeOfMassUpdm.SelectedValue;
            }
            else
            {
                grdAttachedDocs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("BindAttachedDocuments", ex);
        }
        finally
        {
            objDb = null;
        }
    }


    private bool SaveDocuments(string MaterialId)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        string savePath = "";
        string StrPath = String.Empty;
        try
        {

            DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

            Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();

            //string StrPath = "~/Transaction/Material/MaterialDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
            //string StrPath = String.Empty;
            //if (ddlTypeOfMassUpdm.SelectedValue == "11")
            //{
            //    StrPath = "/Transaction/Material/MatChangeDoc/MRPUpdExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            //}
            //else if (ddlTypeOfMassUpdm.SelectedValue == "12")
            //{
            StrPath = "/Transaction/Material/MatExtensionDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            //}
            savePath = MapPath(StrPath);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }
        catch (Exception ex)
        { _log.Error("SaveDocuments", ex); }
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
            _log.Error("SaveDocuments1", ex);
            return false;
        }


    }
    private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath)
    {
        DocumentUpload ObjDoc = new DocumentUpload();
        DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
        bool flag = false;
        Utility objUtil = new Utility();

        Random sufix1 = new Random();
        string sufix = sufix1.NextDouble().ToString().Replace(".", "");
        try
        {
            if (uploadfile.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {

                    string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
                    savePath = savePath + "\\" + uploadedFileName;

                    ObjDoc.Document_Upload_Id = 0;
                    ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
                    ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
                    //ObjDoc.Document_Type = "";
                    ObjDoc.Document_Type = ddlTypeOfMassUpdm.SelectedValue;
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
                    lblMassExtmsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMassExtmsg.CssClass = "error";
                    pnlMassExtmsg.Visible = true;
                }
            }
            else
            {
                flag = false;
                lblMassExtmsg.Text = "Error While Saving Material Details.";
            }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }
        return flag;
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
                        System.IO.File.Delete(Server.MapPath("MaterialDocuments") + "/" + lblUploadedFileName.Text);
                        objTrans.Commit();
                        pnlMassExtmsg.Visible = false;
                        BindAttachedDocuments(lblMasterHeaderId.Text);
                    }
                    else
                    {
                        objTrans.Rollback();
                        lblMassExtmsg.Text = "Error While Deleting File.";
                        pnlMassExtmsg.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("grdAttachedDocs_RowCommand1", ex);
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
    protected void btnbackMsg_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("MaterialMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
    protected void btnMSProcess_Click(object sender, EventArgs e)
    {
        try
        {
            if ((SaveDocuments(lblMasterHeaderId.Text)))
            {
                BindAttachedDocuments(lblMasterHeaderId.Text);
                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                Utility objUtil = new Utility();
                String sstatus = "Vaild";
                objAccess.SaveMassSync(lblMasterHeaderId.Text, sstatus, lblUserId.Text, objUtil.GetIpAddress(), "E", false);

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewDialog();", true);
            }
            else
            {
                lblMassExtmsg.Text = Messages.GetMessage(-1);
                pnlMassExtmsg.CssClass = "error";
                pnlMassExtmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("btnMSProcess_Click", ex);
        }

    }

    private bool CheckIsValidMass()
    {
        bool flg = false;
        try
        {
            if (grdAttachedDocs.Rows.Count > 0)
                flg = true;
        }
        catch (Exception ex)
        { _log.Error("CheckIsValidMass", ex); }
        return flg;
    }

    protected void btnMassSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValidMass())
            {
                if (SaveMass())
                {
                    lblMassExtmsg.Text = Messages.GetMessage(1);
                    pnlMassExtmsg.CssClass = "success";
                    pnlMassExtmsg.Visible = true;

                    Response.Redirect("MaterialMassExtension.aspx");
                }
            }
            else
            {
                lblMassExtmsg.Text = "Please upload file.";
                pnlMassExtmsg.Visible = true;
                pnlMassExtmsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnMassSave_Click", ex); }
    }

    protected bool SaveMass()
    {
        bool Flag = false;
        //MaterialChange ObjMaterialChange = GetControlsValueMass();

        //try
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (ObjMaterialChangeAccess.SaveMass(ObjMaterialChange) > 0)
        //        {
        //            scope.Complete();
        //            Flag = true;
        //        }
        //        else
        //        {
        //            lblMassExtmsg.Text = Messages.GetMessage(-1);
        //            pnlMassExtmsg.CssClass = "error";
        //            pnlMassExtmsg.Visible = true;
        //        }
        //    }
        //}

        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        return Flag;
    }

    private void PopuplateDropDownListMass()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlTypeOfMassUpdm, "pr_GetDropDownListByControlNameModuleType 'M','ddlTypeOfMassExt'", "LookUp_Desc", "LookUp_Code", "");
            if (Session[StaticKeys.TypeOfMassUpdm].ToString() != "")
                ddlTypeOfMassUpdm.SelectedValue = Convert.ToString(Session[StaticKeys.TypeOfMassUpdm]);
        }
        catch (Exception ex)
        {
            _log.Error("PopuplateDropDownListMass", ex);
        }
    }

    protected void ddlTypeOfMassUpdm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hlMSImportFormat.NavigateUrl = "";

            if (ddlTypeOfMassUpdm.SelectedValue == "21")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatExtensionDoc/MatExtensionSMMP/MaterialExtension.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "22")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatExtensionDoc/MatExtensionSMMP/MaterialExtension.xlsx";
            }
        }
        catch (Exception ex)
        { _log.Error("ddlTypeOfMassUpdm_SelectedIndexChanged", ex); }
    }

    protected void btnMassNext_Click(object sender, EventArgs e)
    {

    }

    #endregion
}