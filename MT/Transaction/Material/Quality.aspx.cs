using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;
using System.IO;
using log4net;
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_Quality : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    QualityAccess ObjQualityAccess = new QualityAccess();
    HelperAccess helperAccess = new HelperAccess();
    //LLM_DPT_SDT30072019 Commented By Nitin R
    //List<string> LLMDPTPlantList = new List<string> { "9", "15", "16", "17", "23", "24", "25", "90", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "138", "139", "140", "141", "142", "28", "29", "30", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "83", "84", "85", "86", "87", "88", "89", "90" };
    //LLM_DPT_SDT30072019 Commented By Nitin R

    //LLM_DPT_SDT30072019 Added By Nitin R
    List<string> LLMDPTPlantList = new List<string>();
    //LLM_DPT_SDT30072019 Added By Nitin R
    Quality objSavedQuality = new Quality();
    string sdate = "";
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "Start" + HelperAccess.ReqType);
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load1", ex);
        }
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

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, UserID.ToString())) && (mode == "M" || mode == "N"))
                        {
                            trButton.Visible = true;
                            btnSave.Visible = !btnNext.Visible;
                            if (!btnPrevious.Visible && !btnNext.Visible)
                                btnSave.Visible = false;
                            //MSC_8300001775 Start
                            //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                            if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = true;
                            }
                            //MSC_8300001775 End 
                        }
                        //LLM_DPT_SDT30072019 Added By Nitin R
                        LLMDPTPlantListMet();
                        //LLM_DPT_EDT30072019 Added By Nitin R
                        ClearData();
                        ConfigureControl();

                        //To manage the Creation Single request
                        FillFormDataByMHId();
                        lnlAddDetails.Visible = false;
                        grvData.Visible = false;
                        //MSC_8300001775 Start
                        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                        {
                            ConfigureControlForSChange();
                            reqtxtMinRemainingShelfLife.Visible = false;
                            labletxtMinRemainingShelfLife.Visible = false;
                            //chkQMProcurmentActive.Enabled = false;
                        }
                        else
                        {
						//8400000540 - Make the QM Material Authorization Group field Mandatory
                             lableddlMaterialAuthGroupActQM.Visible = true;
                             reqddlMaterialAuthGroupActQM.Visible = true;
							 //8400000540 - Make the QM Material Authorization Group field Mandatory
                        }
                        //else
                        //{
                        //    reqtxtMinRemainingShelfLife.Visible = true;
                        //    labletxtMinRemainingShelfLife.Visible = true;
                        //}
                        //MSC_8300001775 End
                    }
                    else
                    {
                        Response.Redirect("MaterialMaster.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    /// <summary>
    /// LLM_DPT_SDT30072019 
    /// Update list
    /// </summary>
    private void LLMDPTPlantListMet()
    {
        try
        {
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

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
            FillQualityData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblQualityId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Quality_Id"].ToString();
            FillQualityData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveQuality())
            {

                if ((lblModuleId.Text == "164" || lblModuleId.Text == "162" || lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                {
                    Response.Redirect("PlantStorageLocation.aspx", false);
                }
                else
                {
                    string pageURL = btnPrevious.CommandArgument.ToString();
                    Response.Redirect(pageURL);
                }

            }

        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveQuality())
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("Quality.aspx", false);
                //string pageURL = btnNext.CommandArgument.ToString();
                //Response.Redirect(pageURL); 
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveQuality())
            {
                //8400000410 comment start
                //if (((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164)) && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                //{
                //    Response.Redirect("ExtensionData.aspx", false);
                //}
                //else if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                //{
                //    Response.Redirect("Accounting1.aspx", false);
                //}
                //else
                //{
                //    string pageURL = btnNext.CommandArgument.ToString();
                //    Response.Redirect(pageURL);
                //}
                //8400000410 comment end
                //8400000410 add Start
                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);
                //8400000410 add End
            }

        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
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

    protected void lnkRefreshddlInspectionType_Click(object sender, EventArgs e)
    {
        try
        {
            DisplayInspectionType();
        }
        catch (Exception ex)
        { _log.Error("lnkRefreshddlInspectionType_Click", ex); }
    }

    //New Addition for HU tick Start
    //protected void btnInspData_Click(object sender, EventArgs e)
    //{
    //    if (chklstInspectionType.Items.Count > 0)
    //    {
    //        for (int i = 0; i < chklstInspectionType.Items.Count; i++)
    //        {
    //            chklstInspectionType.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
    //        }
    //    }

    //    ucInspdata.Save();

    //    //if (!(materialDesc == ""))
    //    //{
    //    //    txtMaterialDescription.Text = materialDesc;
    //    //    pnlMatDesc.Visible = false;
    //    //    trMatDesc.Visible = false;
    //    //}

    //}

    //protected void chklstInspectionType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (chklstInspectionType.Items.Count > 0)
    //    {
    //        for (int i = 0; i < chklstInspectionType.Items.Count; i++)
    //        {
    //            chklstInspectionType.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
    //        }
    //    }

    //    //if (pnlInspData.Visible == true)
    //    //    ShowInspData(false);
    //    //else

    //    ShowInspData(true);

    //}

    //New Addition for HU tick End

    #endregion

    #region Method

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
            //chkQMProcurmentActive.Enabled = false;

            //lableddlControlQualityMang.Visible = false;
            //reqddlControlQualityMang.Visible = false;

            //lableddlCertificateType.Visible = false;
            //reqddlCertificateType.Visible = false;

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

            lableddlInspectionType.Visible = false;


            labletxtTotalShelfLifeDays.Visible = false;
            reqtxtTotalShelfLifeDays.Visible = false;

            lableddlMaterialAuthGroupActQM.Visible = false;
            reqddlMaterialAuthGroupActQM.Visible = false;

            txtTotalShelfLifeDays.Enabled = true;
            txtTotalShelfLifeDays.CssClass = "textbox";
            txtTotalShelfLifeDays.ReadOnly = false;



            lableddlUnitIssue.Visible = false;
            reqddlUnitIssue.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }

    }

    private void DisplayInspectionType()
    {
        //bool flg = false;
        try
        {
            string InspectionType = GetSelectedCheckedValue(ddlInspectionType);
            if (InspectionType != null)
            {
                lableddlInspectionType1.Text = "Inspection Type :  " + InspectionType.Substring(0, InspectionType.Length - 1);
                //string[] str = InspectionType.Split(',');

                //foreach (string insVal in str)
                //{
                //    if (insVal == "09      ")
                //    {
                //        flg = true;
                //    }
                //}

                //New Addition for HU tick Start
                //string[] str = InspectionType.Split(',');
                //DataSet ds = new DataSet();
                //DataTable dt = new DataTable("InspectionType");
                //dt.Columns.Add("LookupCode", typeof(string));
                //dt.Columns.Add("LookupDesc", typeof(string));


                //foreach (string value in str)
                //{
                //    if (value != "")
                //        dt.Rows.Add(value, value);
                //}
                //ds.Tables.Add(dt);

                //if (ds.Tables["InspectionType"].Rows.Count > 0)
                //{
                //    chklstInspectionType.DataSource = ds;
                //    chklstInspectionType.DataTextField = "LookupDesc";
                //    chklstInspectionType.DataValueField = "LookupCode";
                //    chklstInspectionType.DataBind();
                //}

                //if (chklstInspectionType.Items.Count > 0)
                //{
                //    for (int i = 0; i < chklstInspectionType.Items.Count; i++)
                //    {
                //        chklstInspectionType.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
                //    }
                //}
                //New Addition for HU tick End
            }
            else
            {
                lableddlInspectionType1.Text = "";
            }

            reqtxtIntervalNPInspector.Visible = false;
            labletxtIntervalNPInspector.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("DisplayInspectionType", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR
            helperAccess.PopuplateDropDownList(ddlCatalogProfile, "pr_GetDropDownListByControlNameModuleType 'M','ddlCatalogProfile'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCertificateType, "pr_GetDropDownListByControlNameModuleType 'M','ddlCertificateType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlControlQualityMang, "pr_GetDropDownListByControlNameModuleType 'M','ddlControlQualityMang'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterialAuthGroupActQM, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialAuthGroupActQM'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlUnitIssue, "pr_GetDropDownListByControlNameModuleType 'M','ddlUnitIssue'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlUnitIssue, "pr_GetDropDownListByControlNameModuleType 'M','ddlCommUnit'", "LookUp_Desc", "LookUp_Code", "");

            //helperAccess.PopuplateDropDownList(ddlInspectionType, "pr_GetDropDownListByControlNameModuleType 'M','ddlInspectionType'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownCheckBox(ddlInspectionType, "pr_GetDropDownListByControlNameModuleType 'M','ddlInspectionType'", "LookUp_Desc", "LookUp_Code");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblQualityId.Text = "0";
            chkQMProcurmentActive.Checked = false;
            chkDocumentationReqIndi.Checked = false;
            txtIntervalNPInspector.Text = "";

            ClearSelectedValue(ddlInspectionType);
            PopuplateDropDownList();
        }
        catch (Exception ex)
        {
            _log.Error("ClearData", ex);
        }
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjQualityAccess.GetQualityData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
        }
    }

    /// <summary>
    /// /MSC_8300001775
    /// </summary>
    /// <returns></returns>
    private bool IsValid()
    {
        Utility objUtil = new Utility();
        bool flg1 = false;
        try
        {
            if ((ddlControlQualityMang.SelectedValue == "0" || ddlControlQualityMang.SelectedValue == "")
               && (chkQMProcurmentActive.Checked == true))
            {
                reqddlControlQualityMang.Enabled = true;
                lblMsg.Text = "Control Key for Quality Management in Procurement cannot be blank.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                //flg = false;

            }
            else if ((ddlControlQualityMang.SelectedValue != "0" && ddlControlQualityMang.SelectedValue != "")
               && (chkQMProcurmentActive.Checked == false))
            {
                chkQMProcurmentActive.Checked = true;
                lblMsg.Text = "Please select QM in Procurement is Active.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                //flg = false;
            }
            else
            {
                flg1 = true;
                //lblMsg.Text = Messages.GetMessage(-1);
                //pnlMsg.CssClass = "error";
                //pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("IsValid", ex);
        }
        return flg1;
    }



    private bool SaveQuality()
    {
        bool flg = true;
        if (IsValid() == true)
        {
            try
            {
                Quality ObjQuality = GetControlsValue();
                objSavedQuality = GetQualityData();
                if (CheckInspectionType())
                {
                    if (ObjQuality.Inspection_Type != null || !lableddlInspectionType.Visible)
                    {
                        if (CheckValidShelfLife())
                        {
                            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                            {
                                if (objSavedQuality.Mat_Quality_Id > 0)
                                {
                                    CheckIfChanges(ObjQuality, objSavedQuality);
                                }
                            }

                            if (ObjQualityAccess.Save(ObjQuality) > 0)
                            {
                                //MSC_8300001775

                                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "After End");
                                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                                {
                                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "Start");
                                    CheckIfChangesLog(ObjQuality, objSavedQuality);
                                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "End");
                                }
                                //MSC_8300001775

                                //FillDataGrid();
                                ClearData();
                                //FillQualityData();
                                FillFormDataByMHId();
                                flg = true;
                                ////MSC_8300001775

                                //WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "After End");
                                //if (HelperAccess.ReqType == "SMC")
                                //{
                                //    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "Start");
                                //    CheckIfChangesLog(ObjQuality, objSavedQuality);
                                //    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "End");
                                //}
                                ////MSC_8300001775
                            }
                            else
                            {
                                lblMsg.Text = Messages.GetMessage(-1);
                                pnlMsg.CssClass = "error";
                                pnlMsg.Visible = true;
                                flg = false;
                            }

                        }
                        else
                        {
                            lblMsg.Text = "You have entered a total shelf life but no minimum remaining shelf life. Please enter Minimum Remaining shelf life";
                            pnlMsg.CssClass = "error";
                            pnlMsg.Visible = true;
                            flg = false;
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
                    lblMsg.Text = "If Inspection Type '09' is selected. Interval to Next Period Inspection must be entered";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                    flg = false;
                }


            }
            catch (Exception ex)
            {
                _log.Error("SaveQuality", ex);
            }
            return flg;
        }
        else
        {
            flg = false; 
        }
        return flg;
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

    private Quality GetQualityData()
    {
        return ObjQualityAccess.GetQuality(Convert.ToInt32(lblQualityId.Text));
    }

    private Quality GetControlsValue()
    {
        Quality ObjQuality = new Quality();
        Utility objUtil = new Utility();

        try
        {
            ObjQuality.Mat_Quality_Id = Convert.ToInt32(lblQualityId.Text);
            ObjQuality.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjQuality.Plant_Id = ddlPlant.SelectedValue;// GetSelectedCheckedValue(ddlPlant);


            ObjQuality.Unit_Issue = ddlUnitIssue.SelectedValue;
            ObjQuality.Is_QM_in_Procurement = chkQMProcurmentActive.Checked == true ? "1" : "0";
            ObjQuality.Certificate_Type = ddlCertificateType.SelectedValue;
            ObjQuality.Ctrl_Key_QM_Procurement = ddlControlQualityMang.SelectedValue;
            ObjQuality.Is_Doc_Required = chkDocumentationReqIndi.Checked == true ? "1" : "0";
            ObjQuality.Catlog_Profile = ddlCatalogProfile.SelectedValue;
            ObjQuality.Mat_Auth_Grp_Activities = ddlMaterialAuthGroupActQM.SelectedValue;
            ObjQuality.Interval_Nxt_Inspection = txtIntervalNPInspector.Text;
            //ObjQuality.Inspection_Type = ddlInspectionType.SelectedValue;
            ObjQuality.Inspection_Type = GetSelectedCheckedValue(ddlInspectionType) == null ? "" : GetSelectedCheckedValue(ddlInspectionType);

            ObjQuality.Min_Remaining_Shelf_Life = txtMinRemainingShelfLife.Text.ToString();
            ObjQuality.Total_Shelf_Life_Days = txtTotalShelfLifeDays.Text.ToString();

            ObjQuality.IsActive = 1;
            ObjQuality.UserId = lblUserId.Text;
            ObjQuality.TodayDate = objUtil.GetDate();
            ObjQuality.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjQuality;
    }

    private void FillFormDataByMHId()
    {
        try
        {
            DataSet ds;
            ds = ObjQualityAccess.GetQualityData(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblQualityId.Text = ds.Tables[0].Rows[0]["Mat_Quality_Id"].ToString();
            }
            FillQualityData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        //
    }

    private void FillQualityData()
    {
        try
        {
            //CCP - MM - 941 - 22 - 0082 S
            ConfigureControlQMCtrl();

            //CCP - MM - 941 - 22 - 0082 E
            Quality ObjQuality = GetQualityData();
            if (ObjQuality.Mat_Quality_Id > 0)
            {

                lblQualityId.Text = ObjQuality.Mat_Quality_Id.ToString();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjQuality.Plant_Id;
                ddlUnitIssue.SelectedValue = ObjQuality.Unit_Issue;
                chkQMProcurmentActive.Checked = ObjQuality.Is_QM_in_Procurement.ToLower() == "true" ? true : false;
                ddlCertificateType.SelectedValue = ObjQuality.Certificate_Type;
                ddlControlQualityMang.SelectedValue = ObjQuality.Ctrl_Key_QM_Procurement;
                //CCP-MM-941-22-0082
                //if (ObjQuality.IsDraf == "0" || ObjQuality.IsDraf == null)
                //{
                //    ConfigCertificateType(ddlControlQualityMang.SelectedValue);
                //}
                //else
                //{
                //    ddlCertificateType.SelectedValue = ObjQuality.Certificate_Type;
                //}
                //CCP-MM-941-22-0082
                chkDocumentationReqIndi.Checked = ObjQuality.Is_Doc_Required.ToLower() == "true" ? true : false;
                ddlCatalogProfile.SelectedValue = ObjQuality.Catlog_Profile;
                ddlMaterialAuthGroupActQM.SelectedValue = ObjQuality.Mat_Auth_Grp_Activities;
                txtIntervalNPInspector.Text = ObjQuality.Interval_Nxt_Inspection;

                SetSelectedValue(ddlInspectionType, ObjQuality.Inspection_Type);

                txtMinRemainingShelfLife.Text = ObjQuality.Min_Remaining_Shelf_Life.ToString();
                txtTotalShelfLifeDays.Text = ObjQuality.Total_Shelf_Life_Days.ToString();
                //ddlInspectionType.SelectedValue = ObjQuality.Inspection_Type;
                if (LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
                {
                    lableddlInspectionType.Visible = false;
                }
            }
            else
            {
                lblQualityId.Text = "0";

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','Q1','" + lblQualityId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
                if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                {

                    if (lblModuleId.Text == "138")
                    {
                        ddlMaterialAuthGroupActQM.SelectedValue = "ZERSA6";
                        if (ddlPlant.SelectedValue != "9" && ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")

                            //MSC_8300001775 Start Comment 
                            //SetSelectedValue(ddlInspectionType, "Z1      ");
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            SetSelectedValue(ddlInspectionType, "Z1");
                        //MSC_8300001775 End
                        // ddlInspectionType.SelectedValue = "Z1      ";
                    }
                    else if (lblModuleId.Text == "147")
                    {
                        ddlMaterialAuthGroupActQM.SelectedValue = "ZHIBE5";
                        ddlInspectionType.SelectedValue = "";
                    }
                    else if (lblModuleId.Text == "170")
                    {
                        ddlMaterialAuthGroupActQM.SelectedValue = "ZZMBW7";
                        if (ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")

                            //MSC_8300001775 Start Comment 
                            //SetSelectedValue(ddlInspectionType, "Z1      ");
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            SetSelectedValue(ddlInspectionType, "Z1");
                        //MSC_8300001775 End
                    }
                    else if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
                    {
                        //CCP-MM-941-22-0082 Comment S
                        //chkQMProcurmentActive.Checked = true;
                        //CCP-MM-941-22-0082 Comment E

                        txtMinRemainingShelfLife.Text = "12";
                        txtTotalShelfLifeDays.Text = "59";

                        if (lblModuleId.Text == "162")
                        {
                            ddlMaterialAuthGroupActQM.SelectedValue = "ZROH01";

                        }

                        if (lblModuleId.Text == "164")
                        {
                            ddlMaterialAuthGroupActQM.SelectedValue = "ZVERP2";

                        }

                        // Check for Pune Plant

                        //if (ddlPlant.SelectedValue != "9" && ddlPlant.SelectedValue != "15" && ddlPlant.SelectedValue != "16" && ddlPlant.SelectedValue != "17" && ddlPlant.SelectedValue != "23" && ddlPlant.SelectedValue != "24" && ddlPlant.SelectedValue != "25")
                        if (!LLMDPTPlantList.Contains(ddlPlant.SelectedValue.ToString()))
                        {
                            //MSC_8300001775 Start Comment 
                            //if (lblModuleId.Text == "164" && ddlPlant.SelectedValue.ToString() == "10")
                            //    SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,89      ,Z3      ");
                            //else
                            //    SetSelectedValue(ddlInspectionType, "01      ,05      ,08      ,09      ,89      ");
                            //ddlControlQualityMang.SelectedValue = "Z102    ";
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            if (lblModuleId.Text == "164" && ddlPlant.SelectedValue.ToString() == "10")
                                SetSelectedValue(ddlInspectionType, "01,05,08,09,89,Z3");
                            else
                                SetSelectedValue(ddlInspectionType, "01,05,08,09,89");
                            //CCP-MM-941-22-0082 Comment S
                            //ddlControlQualityMang.SelectedValue = "Z102";
                            //ddlCertificateType.SelectedValue = "9999";
                            //CCP-MM-941-22-0082 Comment E
                            //MSC_8300001775 End

                            txtIntervalNPInspector.Text = "360";
                        }
                        else
                        {
                            //MSC_8300001775 Start Comment 
                            //ddlControlQualityMang.SelectedValue = "0000    ";
                            //MSC_8300001775 End Comment
                            //MSC_8300001775 Start
                            //CCP-MM-941-22-0082 Comment S
                            //ddlControlQualityMang.SelectedValue = "0000";
                            //ddlCertificateType.SelectedValue = "";
                            //CCP-MM-941-22-0082 Comment E
                            //MSC_8300001775 End

                            lableddlInspectionType.Visible = false;
                        }
                    }
                    //Promotion code start
                    //else if (lblModuleId.Text == "144" || lblModuleId.Text == "139" || lblModuleId.Text == "171" || lblModuleId.Text == "145")
                    else if (lblModuleId.Text == "144" || lblModuleId.Text == "139" || lblModuleId.Text == "171" || lblModuleId.Text == "145" || lblModuleId.Text == "195")
                    //Promotion code start
                    {
                        if (lblModuleId.Text == "144")
                            ddlMaterialAuthGroupActQM.SelectedValue = "ZHALB3";
                        else if (lblModuleId.Text == "139")
                            ddlMaterialAuthGroupActQM.SelectedValue = "ZFERT4";
                        //Promotion code start
                        //else if (lblModuleId.Text == "171")
                        else if (lblModuleId.Text == "171" || lblModuleId.Text == "195")
                            //Promotion code start
                            ddlMaterialAuthGroupActQM.SelectedValue = "ZZNBW8";
                        else if (lblModuleId.Text == "145")
                            ddlMaterialAuthGroupActQM.SelectedValue = "";
                        txtMinRemainingShelfLife.Text = "1";
                        txtTotalShelfLifeDays.Text = "23";
                    }
                }
            }

            DisplayInspectionType();
            ddlPlant.Enabled = false;
            //CCP-MM-941-22-0082 Comment S
            //if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            //{
            //    if (lblModuleId.Text == "162" || lblModuleId.Text == "164")
            //    { chkQMProcurmentActive.Enabled = false; 
            //    }
            //}
            //CCP-MM-941-22-0082 Comment E
        }
        catch (Exception ex)
        {
            _log.Error("FillQualityData", ex);
        }
    }

    /// <summary>
    /// CCP-MM-941-22-0082
    /// </summary>
    private void ConfigCertificateType(string pValue)
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
        { }
        MaterialMasterAccess objMaterialMasterAccess = new MaterialMasterAccess();
        DataSet dstData = new DataSet();
        //Plant id 0 meanse apply certificate type validation
        dstData = objMaterialMasterAccess.FillPropertiesDataSet(Convert.ToInt32(lblModuleId.Text.ToString()), pValue, "CType");
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
                        ddlCertificateType.SelectedValue = Convert.ToString(sDefualtValue);
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

    protected void ddlControlQualityMang_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //MSC_8300001775 Start Comment 
            //if (ddlControlQualityMang.SelectedValue == "Z102    " || ddlControlQualityMang.SelectedValue == "Z101    ")
            //MSC_8300001775 End Comment
            //MSC_8300001775 Start as below
            //if (ddlControlQualityMang.SelectedValue == "Z102" || ddlControlQualityMang.SelectedValue == "Z101")
            //MSC_8300001775 End

            //if (ddlControlQualityMang.SelectedValue == "Z102" || ddlControlQualityMang.SelectedValue == "Z101")
            //{
            //    ddlCertificateType.SelectedValue = "9999";
            //    ddlCertificateType.Enabled = false;
            //}
            //else
            //{
            //    ddlCertificateType.SelectedValue = "";
            //    ddlCertificateType.Enabled = true;
            //}

            ConfigCertificateType(ddlControlQualityMang.SelectedValue);
        }
        catch (Exception ex)
        { _log.Error("ddlControlQualityMang_SelectedIndexChanged", ex); }
    }

    /// <summary>
    /// CCP-MM-941-22-0082
    /// </summary>
    private void ConfigureControlQMCtrl()
    {
        try
        {
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

        }
        catch (Exception ex)
        { }
        MaterialMasterAccess objMaterialMasterAccess = new MaterialMasterAccess();
        DataSet dstData = new DataSet();
        dstData = objMaterialMasterAccess.FillPropertiesDataSet(Convert.ToInt32(lblModuleId.Text.ToString()), Convert.ToString(Session[StaticKeys.MaterialPlantId].ToString()), "QMType");
        if (dstData.Tables[0].Rows.Count > 0)
        {
            try
            {
                //string bMandatory = Convert.ToString(dstData.Tables[0].Rows[0]["bMandatory"]) == "1" ? "true" : "false";
                //string bEnable = Convert.ToString(dstData.Tables[0].Rows[0]["bEnable"]) == "1" ? "true" : "false";
                //string bShowhide = Convert.ToString(dstData.Tables[0].Rows[0]["bShowhide"]) == "1" ? "true" : "false";
                //string iSectionFieldID = Convert.ToString(dstData.Tables[0].Rows[0]["iSectionFieldID"]) == "1" ? "true" : "false";
                //string sDefualtValue = Convert.ToString(dstData.Tables[0].Rows[0]["sDefualtValue"]).ToString();


                //if (iSectionFieldID == "150")//QM in Procurement is Active
                //{ }
                //if (iSectionFieldID == "151")//Certificate Type
                //{ }
                //if (iSectionFieldID == "152")//ddlControlQualityMang
                //{ }
                //lblddlControlQualityMang.Visible = Convert.ToBoolean(bShowhide);
                //lableddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                //reqddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                //ddlControlQualityMang.Enabled = Convert.ToBoolean(bEnable);
                //ddlControlQualityMang.Visible = Convert.ToBoolean(bShowhide);
                //ddlControlQualityMang.SelectedValue = Convert.ToString(sDefualtValue);
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
                        chkQMProcurmentActive.Checked = Convert.ToBoolean(sDefualtValue);
                    }
                    if (iSectionFieldID == "151")//Certificate Type
                    {
                        lblddlCertificateType.Visible = Convert.ToBoolean(bShowhide);
                        lableddlCertificateType.Visible = Convert.ToBoolean(bMandatory);
                        reqddlCertificateType.Visible = Convert.ToBoolean(bMandatory);
                        ddlCertificateType.Enabled = Convert.ToBoolean(bEnable);
                        ddlCertificateType.Visible = Convert.ToBoolean(bShowhide);
                        ddlCertificateType.SelectedValue = Convert.ToString(sDefualtValue);
                    }
                    if (iSectionFieldID == "152")//ddlControlQualityMang
                    {
                        lblddlControlQualityMang.Visible = Convert.ToBoolean(bShowhide);
                        lableddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                        reqddlControlQualityMang.Visible = Convert.ToBoolean(bMandatory);
                        ddlControlQualityMang.Enabled = Convert.ToBoolean(bEnable);
                        ddlControlQualityMang.Visible = Convert.ToBoolean(bShowhide);
                        ddlControlQualityMang.SelectedValue = Convert.ToString(sDefualtValue);
                    }
                }
                //lableddlControlQualityMang.Visible = false;
                //reqddlControlQualityMang.Visible = false;
                //ddlControlQualityMang.Enabled = false;
                //ddlControlQualityMang.Visible = false;
                //ddlMaterialAuthGroupActQM.SelectedValue = "ZERSA6";

                //helperAccess.PopuplateDropDownList(ddlControlQualityMang, "pr_GetDropDownListByControlNameModuleType 'M','ddlControlQualityMang'", "LookUp_Desc", "LookUp_Code", "");

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
        //foreach (DataRow row in dstData.Tables[0].Rows)
        //{
        //    try
        //    {
        //        // obj1.cName = Convert.ToString(row["FieldName"].ToString());
        //        //obj1.cMaxLength = Convert.ToInt32(row["cMaxLength"].ToString());
        //        //obj1.cValType = Convert.ToInt32(row["cValType"].ToString());
        //        //obj1.cShowHide = Convert.ToInt32(row["cShowHide"].ToString());
        //        //obj1.cVisible = Convert.ToInt32(row["cVisible"].ToString());
        //        //obj1.CDataType = Convert.ToString(row["CDataType"].ToString());
        //        //obj1.MandatoryNonM = Convert.ToInt32(row["MandatoryNonM"].ToString()); 
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }


    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.Quality obj = new SectionConfiguration.Quality();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Quality NewQualityData, Quality oldQualityData)
    {
        try
        {
            //Session[StaticKeys.ApprovalNote] = "";
            if (NewQualityData.Mat_Quality_Id > 0 && oldQualityData.Mat_Quality_Id > 0)
            {
                if (NewQualityData.Plant_Id != oldQualityData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant ID</td> <td>" + oldQualityData.Plant_Id + "</td><td>" + NewQualityData.Plant_Id + "</td></tr>";
                if (NewQualityData.Unit_Issue != oldQualityData.Unit_Issue)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit_Issue</td> <td>" + oldQualityData.Unit_Issue + "</td><td>" + NewQualityData.Unit_Issue + "</td></tr>";
                if (NewQualityData.Is_QM_in_Procurement != (oldQualityData.Is_QM_in_Procurement.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Is QM in Procurement</td> <td>" + (oldQualityData.Is_QM_in_Procurement.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewQualityData.Is_QM_in_Procurement + "</td></tr>";
                if (NewQualityData.Certificate_Type != oldQualityData.Certificate_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Certificate Type</td> <td>" + oldQualityData.Certificate_Type + "</td><td>" + NewQualityData.Certificate_Type + "</td></tr>";
                if (NewQualityData.Ctrl_Key_QM_Procurement != oldQualityData.Ctrl_Key_QM_Procurement)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Ctrl Key QM Procurement</td> <td>" + oldQualityData.Ctrl_Key_QM_Procurement + "</td><td>" + NewQualityData.Ctrl_Key_QM_Procurement + "</td></tr>";
                if (NewQualityData.Is_Doc_Required != (oldQualityData.Is_Doc_Required.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Is Doc Required</td> <td>" + (oldQualityData.Is_Doc_Required.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewQualityData.Is_Doc_Required + "</td></tr>";
                if (NewQualityData.Catlog_Profile != oldQualityData.Catlog_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Catlog Profile</td> <td>" + oldQualityData.Catlog_Profile + "</td><td>" + NewQualityData.Catlog_Profile + "</td></tr>";
                if (NewQualityData.Mat_Auth_Grp_Activities != oldQualityData.Mat_Auth_Grp_Activities)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Mat_Auth_Grp_Activities</td> <td>" + oldQualityData.Mat_Auth_Grp_Activities + "</td><td>" + NewQualityData.Mat_Auth_Grp_Activities + "</td></tr>";
                if (NewQualityData.Inspection_Type != "" && oldQualityData.Inspection_Type != "")
                {
                    if (NewQualityData.Inspection_Type.Substring(1, NewQualityData.Inspection_Type.Length - 1) != oldQualityData.Inspection_Type.Substring(1, oldQualityData.Inspection_Type.Length - 1))
                        Session[StaticKeys.ApprovalNote] += "<tr><td>Inspection Type</td> <td>" + oldQualityData.Inspection_Type + "</td><td>" + NewQualityData.Inspection_Type + "</td></tr>";
                }
                if (NewQualityData.Interval_Nxt_Inspection != oldQualityData.Interval_Nxt_Inspection)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Interval to Nxt Periodic Inspection</td> <td>" + oldQualityData.Interval_Nxt_Inspection + "</td><td>" + NewQualityData.Interval_Nxt_Inspection + "</td></tr>";
                if (NewQualityData.Min_Remaining_Shelf_Life != oldQualityData.Min_Remaining_Shelf_Life)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Min Remaining Shelf Life</td> <td>" + oldQualityData.Min_Remaining_Shelf_Life + "</td><td>" + NewQualityData.Min_Remaining_Shelf_Life + "</td></tr>";
                if (NewQualityData.Total_Shelf_Life_Days != oldQualityData.Total_Shelf_Life_Days)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Total Shelf Life Days</td> <td>" + oldQualityData.Total_Shelf_Life_Days + "</td><td>" + NewQualityData.Total_Shelf_Life_Days + "</td></tr>";
            }

            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChanges", ex);
        }

    }

    //MSC_8300001775
    private void CheckIfChangesLog(Quality NewQualityData, Quality oldQualityData)
    {
        bool inTypendsFlag = false;
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewQualityData.Mat_Quality_Id > 0 && oldQualityData.Mat_Quality_Id > 0)
            {

                try
                {
                    inTypendsFlag = false;
                    if (NewQualityData.Inspection_Type != "" && oldQualityData.Inspection_Type == "")
                    {
                        inTypendsFlag = true;
                    }
                    if (NewQualityData.Inspection_Type == "" && oldQualityData.Inspection_Type != "")
                    {
                        inTypendsFlag = true;
                    }
                    if (NewQualityData.Inspection_Type != "" && oldQualityData.Inspection_Type != "")
                    {
                        inTypendsFlag = true;
                    }
                }
                catch (Exception ex)
                { }


                if (NewQualityData.Unit_Issue != oldQualityData.Unit_Issue)
                {
                    _items.Add(new SMChange { colFieldName = 149, colOldVal = oldQualityData.Unit_Issue, colNewVal = NewQualityData.Unit_Issue });
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "149" + oldQualityData.Unit_Issue + '_' + NewQualityData.Unit_Issue);
                }
                if (NewQualityData.Is_QM_in_Procurement != (oldQualityData.Is_QM_in_Procurement.ToLower() == "true" ? "1" : "0"))
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "150" + (oldQualityData.Is_QM_in_Procurement.ToLower() == "true" ? "X" : "") + '_' + (NewQualityData.Is_QM_in_Procurement.ToLower() == "1" ? "X" : ""));
                    _items.Add(new SMChange { colFieldName = 150, colOldVal = (oldQualityData.Is_QM_in_Procurement.ToLower() == "true" ? "X" : ""), colNewVal = (NewQualityData.Is_QM_in_Procurement.ToLower() == "1" ? "X" : "") });
                }
                if (NewQualityData.Certificate_Type != oldQualityData.Certificate_Type)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "151");
                    _items.Add(new SMChange { colFieldName = 151, colOldVal = oldQualityData.Certificate_Type, colNewVal = NewQualityData.Certificate_Type });
                }
                if (NewQualityData.Ctrl_Key_QM_Procurement != oldQualityData.Ctrl_Key_QM_Procurement)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "152");
                    _items.Add(new SMChange { colFieldName = 152, colOldVal = oldQualityData.Ctrl_Key_QM_Procurement, colNewVal = NewQualityData.Ctrl_Key_QM_Procurement });
                }
                if (NewQualityData.Is_Doc_Required != (oldQualityData.Is_Doc_Required.ToLower() == "true" ? "1" : "0"))
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "153");
                    //_items.Add(new SMChange { colFieldName = 153, colOldVal = (oldQualityData.Is_Doc_Required.ToLower() == "true" ? "X" : ""), colNewVal = (NewQualityData.Is_Doc_Required.ToLower() == "true" ? "X" : "") });
                    _items.Add(new SMChange { colFieldName = 153, colOldVal = (oldQualityData.Is_Doc_Required.ToLower() == "true" ? "X" : ""), colNewVal = (NewQualityData.Is_Doc_Required.ToLower() == "1" ? "X" : "") });
                }
                if (NewQualityData.Catlog_Profile != oldQualityData.Catlog_Profile)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "154");
                    _items.Add(new SMChange { colFieldName = 154, colOldVal = oldQualityData.Catlog_Profile, colNewVal = NewQualityData.Catlog_Profile });
                }
                if (NewQualityData.Mat_Auth_Grp_Activities != oldQualityData.Mat_Auth_Grp_Activities)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "155");
                    _items.Add(new SMChange { colFieldName = 155, colOldVal = oldQualityData.Mat_Auth_Grp_Activities, colNewVal = NewQualityData.Mat_Auth_Grp_Activities });
                }
                if (NewQualityData.Interval_Nxt_Inspection != oldQualityData.Interval_Nxt_Inspection)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "156");
                    _items.Add(new SMChange { colFieldName = 156, colOldVal = oldQualityData.Interval_Nxt_Inspection, colNewVal = NewQualityData.Interval_Nxt_Inspection });
                }
                if (NewQualityData.Min_Remaining_Shelf_Life != oldQualityData.Min_Remaining_Shelf_Life)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "1309");
                    _items.Add(new SMChange { colFieldName = 1309, colOldVal = oldQualityData.Min_Remaining_Shelf_Life, colNewVal = NewQualityData.Min_Remaining_Shelf_Life });
                }
                if (NewQualityData.Total_Shelf_Life_Days != oldQualityData.Total_Shelf_Life_Days)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "1310");
                    _items.Add(new SMChange { colFieldName = 1310, colOldVal = oldQualityData.Total_Shelf_Life_Days, colNewVal = NewQualityData.Total_Shelf_Life_Days });
                }
                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", NewQualityData.Inspection_Type);
                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", oldQualityData.Inspection_Type);

                //if ((NewQualityData.Inspection_Type != "" && oldQualityData.Inspection_Type == "") ||
                //    (NewQualityData.Inspection_Type == "" && oldQualityData.Inspection_Type != "") ||
                //    (NewQualityData.Inspection_Type != "" && oldQualityData.Inspection_Type != ""))
                if (inTypendsFlag == true)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "1237");
                    bool ndsFlag = false;
                    string sInspection_Type = "";
                    try
                    {
                        sInspection_Type = NewQualityData.Inspection_Type.Remove(NewQualityData.Inspection_Type.Length - 1, 1);
                    }
                    catch (Exception ex) { }

                    string sInspection_Typeold = "";
                    try
                    {
                        sInspection_Typeold = oldQualityData.Inspection_Type;
                    }
                    catch (Exception ex) { }
                    string[] nds = sInspection_Type.Split(',');
                    //string[] ods = oldQualityData.Inspection_Type.Split(',');
                    string[] ods = sInspection_Typeold.Split(',');
                    for (var i = 0; i < nds.Length; i++)
                    {
                        WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "nds" + nds[i]);
                        if (nds[i] != "")
                        {

                            if (!ods.Contains(nds[i]))
                            {
                                ndsFlag = true;
                                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "nds-TRUE" + nds[i]);
                                break;
                            }

                        }
                    }
                    for (var j = 0; j < ods.Length; j++)
                    {
                        WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "ods" + ods[j]);
                        if (ods[j] != "")
                        {
                            if (!nds.Contains(ods[j]))
                            {
                                ndsFlag = true;
                                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "ods-TRUE" + ods[j]);
                                break;
                            }
                        }
                    }


                    if (nds.Length == 0 && ods.Length > 0 && ndsFlag == false)
                    {
                        ndsFlag = true;
                    }
                    if (nds.Length > 0 && ods.Length == 0 && ndsFlag == false)
                    {
                        ndsFlag = true;
                    }

                    if (NewQualityData.Inspection_Type == "")
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1237, colOldVal = oldQualityData.Inspection_Type, colNewVal = NewQualityData.Inspection_Type });
                        }
                    }
                    else if (oldQualityData.Inspection_Type == "")
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1237, colOldVal = oldQualityData.Inspection_Type, colNewVal = NewQualityData.Inspection_Type });
                        }
                    }
                    else if (NewQualityData.Inspection_Type.Substring(1, NewQualityData.Inspection_Type.Length - 1) != oldQualityData.Inspection_Type.Substring(1, oldQualityData.Inspection_Type.Length - 1))
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1237, colOldVal = oldQualityData.Inspection_Type, colNewVal = NewQualityData.Inspection_Type });
                        }
                    }
                }

            }

        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog", ex);
        }
        try
        {
            if (_items.Count > 0)
            {
                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "ChangeSMatID1 Start");

                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("14", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
                if (ChangeSMatID1 > 0)
                {
                    WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "ChangeSMatID1" + ChangeSMatID1);
                    foreach (var scItem in _items)
                    {
                        helperAccess.MaterialChangeDetails(ChangeSMatID1, scItem.colFieldName, scItem.colOldVal, scItem.colNewVal);
                        WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "MaterialChangeDetails" + ChangeSMatID1 + '/' + scItem.colFieldName + '/' + scItem.colOldVal + '/' + scItem.colNewVal);
                    }
                }
                _items = new List<SMChange>();
                WriteMatChangeLog("MatChangeLogQM" + sdate + ".txt", "ChangeSMatID1 End");
            }
            //MSC_8300001775 End


        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog1", ex);
        }

    }

    private bool CheckValidShelfLife()
    {
        bool flag = true;
        //throw new NotImplementedException();
        try
        {
            if ((txtTotalShelfLifeDays.Text != "") && (SafeTypeHandling.ConvertStringToInt32(txtTotalShelfLifeDays.Text.ToString()) >= 1))
            {
                if (txtMinRemainingShelfLife.Text == "" || SafeTypeHandling.ConvertStringToInt32(txtMinRemainingShelfLife.Text.ToString()) <= 0)
                    flag = false;
            }

        }
        catch (Exception ex)
        { _log.Error("CheckValidShelfLife", ex); }
        return flag;
    }

    //New Addition for HU tick Start
    //private void ShowInspData(bool isVisible)
    //{
    //    ucInspdata.InspectionType = chklstInspectionType.SelectedValue.ToString();
    //    pnlInspData.Visible = isVisible;
    //    trQualityData.Visible = isVisible;
    //    ucInspdata.FillData();
    //}
    //New Addition for HU tick End

    #endregion


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

}