using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using log4net;
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_WorkScheduling : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    WorkSchedulingDataAccess ObjWorkSchedulingDataAccess = new WorkSchedulingDataAccess();
    HelperAccess helperAccess = new HelperAccess();
    WorkScheduling objSavedWS = new WorkScheduling();

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

                    //FillDataGrid();
                    ClearData();
                    ConfigureControl();

                    //To manage the Creation Single request
                    FillFormDataByMHId();
                    lnlAddDetails.Visible = false;
                    grvData.Visible = false;
                    //MSC_8300001775 Start
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
                    {
                        ConfigureControlForSChange();
                    }
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
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
        lableddlProductionUnit.Visible = false;
        reqddlProductionUnit.Visible = false;
        lableddlUnitOfIssue.Visible = false;
        reqddlUnitOfIssue.Visible = false;
        lableddlProductionSupervisor.Visible = false;
        reqddlProductionSupervisor.Visible = false;
        lableddlProdSchedProfile.Visible = false;
        reqddlProdSchedProfile.Visible = false;
        labletxtUnderdeliveredToleranceLmt.Visible = false;
        reqtxtUnderdeliveredToleranceLmt.Visible = false;
        labletxtOverdeliveredToleranceLmt.Visible = false;
        reqtxtOverdeliveredToleranceLmt.Visible = false;
        lablechkUnlimited.Visible = false;

        reqddlSerialNumberProfile.Visible = false;
        lableddlSerialNumberProfile.Visible = false;
        lableddlRepetitiveManProfile.Visible = false;
        reqddlRepetitiveManProfile.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }

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

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblWSId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Work_Scheduling_Id"].ToString();
            FillWorkSchedulingData();
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
        if (SaveWorkScheduling())
        {
            if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                Response.Redirect("MRP3.aspx");
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
        if (SaveWorkScheduling())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("WorkScheduling.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        if (SaveWorkScheduling())
            {
                //8400000410 comment start
                //if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("PlantStorageLocation.aspx");
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

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        BindPlantWiseDropDown();
        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Method

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");

        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        { 
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        helperAccess.PopuplateDropDownList(ddlUnitOfIssue, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProductionUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlProductionSupervisor, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlProductionSupervisor','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProdSchedProfile, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlProdSchedProfile','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlRepetitiveManProfile, "pr_GetDropDownListByControlNameModuleType 'M','ddlRepetitiveManProfile'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSerialNumberProfile, "pr_GetDropDownListByControlNameModuleType 'M','ddlSerialNumberProfile'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblWSId.Text = "0";
            txtUnderdeliveredToleranceLmt.Text = "";
            txtOverdeliveredToleranceLmt.Text = "";
            chkUnlimited.Checked = false;

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
            ds = ObjWorkSchedulingDataAccess.GetWorkSchedulingData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
        }
    }

    private bool SaveWorkScheduling()
    {
       
        bool flg = false;
        try
        {
            WorkScheduling ObjWS = GetControlsValue();
            objSavedWS = GetWorkSchedulingData();

            if (CheckUnitOfIssueWithBasicData1() && CheckProdUnitWithBasicData1())
            {
                if (CheckValidIssueUnt() && CheckValidProdUnit())
                {
                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                    {
                        if (objSavedWS.Mat_Work_Scheduling_Id > 0)
                        {
                            CheckIfChanges(ObjWS, objSavedWS);
                        }
                    }

                    if (ObjWS.Plant_Id != null)
                    {
                        if (ObjWorkSchedulingDataAccess.Save(ObjWS) > 0)
                        {
                            //MSC_8300001775
                            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
                            {
                                CheckIfChangesLog(ObjWS, objSavedWS);
                            }
                            //MSC_8300001775 
                            //FillDataGrid();
                            ClearData();
                            FillFormDataByMHId();
                            flg = true;
							////MSC_8300001775
       //             if (HelperAccess.ReqType == "SMC")
       //             {
       //                 CheckIfChangesLog(ObjWS, objSavedWS);
       //             }
       //             //MSC_8300001775 
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
                        lblMsg.Text = "Please Select atleast one Plant to proceed.";
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Please maintain conversion factor according to Unit Of Issue / Production unit. <a href = 'BasicData2.aspx'> Click here to update </a>";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "The unit of issue / Production Unit cannot be same as base unit of measure";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }

            
        }
        catch (Exception ex)
        {
            _log.Error("SaveWorkScheduling", ex);
        }
        return flg;
    }

    private WorkScheduling GetWorkSchedulingData()
    {
        return ObjWorkSchedulingDataAccess.GetWorkScheduling(Convert.ToInt32(lblWSId.Text));
    }

    private WorkScheduling GetControlsValue()
    {
        WorkScheduling ObjWS = new WorkScheduling();
        Utility objUtil = new Utility();
  
        try
        {
            ObjWS.Mat_Work_Scheduling_Id = Convert.ToInt32(lblWSId.Text);
            ObjWS.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjWS.Plant_Id = ddlPlant.SelectedValue;
            ObjWS.Unit_Of_Issue = ddlUnitOfIssue.SelectedValue;
            ObjWS.Production_Unit = ddlProductionUnit.SelectedValue;
            ObjWS.Production_Supervisor = ddlProductionSupervisor.SelectedValue;
            ObjWS.Prod_Sched_Profile = ddlProdSchedProfile.SelectedValue;
            ObjWS.Underdelivered_Tolerance_Lmt = txtUnderdeliveredToleranceLmt.Text;
            ObjWS.Overdelivered_Tolerance_Lmt = txtOverdeliveredToleranceLmt.Text;
            ObjWS.Unlimited = chkUnlimited.Checked == true ? "1" : "0";


            ObjWS.Serial_No_Profile = ddlSerialNumberProfile.SelectedValue;
            ObjWS.Repetitive_Mfg_Profile = ddlRepetitiveManProfile.SelectedValue;

            ObjWS.IsActive = 1;
            ObjWS.UserId = lblUserId.Text;
            ObjWS.TodayDate = objUtil.GetDate();
            ObjWS.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjWS;
    }

    private void FillFormDataByMHId()
    {
        try
        {
        DataSet ds;
        ds = ObjWorkSchedulingDataAccess.GetWorkSchedulingData(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblWSId.Text = ds.Tables[0].Rows[0]["Mat_Work_Scheduling_Id"].ToString();
        }
        FillWorkSchedulingData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlProductionSupervisor, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlProductionSupervisor','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProdSchedProfile, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlProdSchedProfile','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillWorkSchedulingData()
    {
        try
        {
            WorkScheduling ObjWS = GetWorkSchedulingData();
            if (ObjWS.Mat_Work_Scheduling_Id > 0)
            {
                lblWSId.Text = ObjWS.Mat_Work_Scheduling_Id.ToString();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjWS.Plant_Id;
                BindPlantWiseDropDown();

                ddlUnitOfIssue.SelectedValue = ObjWS.Unit_Of_Issue;
                ddlProductionUnit.SelectedValue = ObjWS.Production_Unit;
                ddlProductionSupervisor.SelectedValue = ObjWS.Production_Supervisor;
                ddlProdSchedProfile.SelectedValue = ObjWS.Prod_Sched_Profile;
                txtUnderdeliveredToleranceLmt.Text = ObjWS.Underdelivered_Tolerance_Lmt;
                txtOverdeliveredToleranceLmt.Text = ObjWS.Overdelivered_Tolerance_Lmt;
                chkUnlimited.Checked = ObjWS.Unlimited.ToLower() == "true" ? true : false;

                ddlSerialNumberProfile.SelectedValue = ObjWS.Serial_No_Profile;
                ddlRepetitiveManProfile.SelectedValue = ObjWS.Repetitive_Mfg_Profile;
            }
            else
            {
                lblWSId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WS','" + lblWSId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
            }

            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillWorkSchedulingData", ex);
        }
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Work_Scheduling obj = new SectionConfiguration.Work_Scheduling();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(WorkScheduling NewWSData, WorkScheduling oldWSData)
    {
        try
        {
            if (NewWSData.Mat_Work_Scheduling_Id > 0 && oldWSData.Mat_Work_Scheduling_Id > 0)
            {
                if (NewWSData.Plant_Id != oldWSData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldWSData.Plant_Id + "</td><td>" + NewWSData.Plant_Id + "</td></tr>";
                if (NewWSData.Unit_Of_Issue != oldWSData.Unit_Of_Issue)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit of Issue</td> <td>" + oldWSData.Unit_Of_Issue + "</td><td>" + NewWSData.Unit_Of_Issue + "</td></tr>";
                if (NewWSData.Production_Unit != oldWSData.Production_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Production Unit</td> <td>" + oldWSData.Production_Unit + "</td><td>" + NewWSData.Production_Unit + "</td></tr>";
                if (NewWSData.Production_Supervisor != oldWSData.Production_Supervisor)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Production Supervisor</td> <td>" + oldWSData.Production_Supervisor + "</td><td>" + NewWSData.Production_Supervisor + "</td></tr>";
                if (NewWSData.Prod_Sched_Profile != oldWSData.Prod_Sched_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Production Schedule Profile</td> <td>" + oldWSData.Prod_Sched_Profile + "</td><td>" + NewWSData.Prod_Sched_Profile + "</td></tr>";
                if (NewWSData.Underdelivered_Tolerance_Lmt != oldWSData.Underdelivered_Tolerance_Lmt)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Underdelivered Tolerance Limit</td> <td>" + oldWSData.Underdelivered_Tolerance_Lmt + "</td><td>" + NewWSData.Underdelivered_Tolerance_Lmt + "</td></tr>";
                if (NewWSData.Overdelivered_Tolerance_Lmt != oldWSData.Overdelivered_Tolerance_Lmt)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Overdelivered Tolerance Limit</td> <td>" + oldWSData.Overdelivered_Tolerance_Lmt + "</td><td>" + NewWSData.Overdelivered_Tolerance_Lmt + "</td></tr>";
                if (NewWSData.Unlimited != (oldWSData.Unlimited.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unlimited</td> <td>" + (oldWSData.Unlimited.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewWSData.Unlimited + "</td></tr>";
                if (NewWSData.Serial_No_Profile != oldWSData.Serial_No_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Serial No Profile</td> <td>" + oldWSData.Serial_No_Profile + "</td><td>" + NewWSData.Serial_No_Profile + "</td></tr>";
                if (NewWSData.Repetitive_Mfg_Profile != oldWSData.Repetitive_Mfg_Profile)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Repetitive Mfg Profile</td> <td>" + oldWSData.Repetitive_Mfg_Profile + "</td><td>" + NewWSData.Repetitive_Mfg_Profile + "</td></tr>";
                
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
    private void CheckIfChangesLog(WorkScheduling NewWSData, WorkScheduling oldWSData)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewWSData.Mat_Work_Scheduling_Id > 0 && oldWSData.Mat_Work_Scheduling_Id > 0)
            {
                if (NewWSData.Unit_Of_Issue != oldWSData.Unit_Of_Issue)
                    _items.Add(new SMChange { colFieldName = 1229, colOldVal = oldWSData.Unit_Of_Issue, colNewVal = NewWSData.Unit_Of_Issue });
                if (NewWSData.Production_Unit != oldWSData.Production_Unit)
                    _items.Add(new SMChange { colFieldName = 1230, colOldVal = oldWSData.Production_Unit, colNewVal = NewWSData.Production_Unit });
                if (NewWSData.Production_Supervisor != oldWSData.Production_Supervisor)
                    _items.Add(new SMChange { colFieldName = 1231, colOldVal = oldWSData.Production_Supervisor, colNewVal = NewWSData.Production_Supervisor });
                if (NewWSData.Prod_Sched_Profile != oldWSData.Prod_Sched_Profile)
                    _items.Add(new SMChange { colFieldName = 1232, colOldVal = oldWSData.Prod_Sched_Profile, colNewVal = NewWSData.Prod_Sched_Profile });
                if (NewWSData.Underdelivered_Tolerance_Lmt != oldWSData.Underdelivered_Tolerance_Lmt)
                    _items.Add(new SMChange { colFieldName = 1233, colOldVal = oldWSData.Underdelivered_Tolerance_Lmt, colNewVal = NewWSData.Underdelivered_Tolerance_Lmt });
                if (NewWSData.Overdelivered_Tolerance_Lmt != oldWSData.Overdelivered_Tolerance_Lmt)
                    _items.Add(new SMChange { colFieldName = 1234, colOldVal = oldWSData.Overdelivered_Tolerance_Lmt, colNewVal = NewWSData.Overdelivered_Tolerance_Lmt });
                if (NewWSData.Unlimited != (oldWSData.Unlimited.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 1227, colOldVal = (oldWSData.Unlimited.ToLower() == "true" ? "X" : ""), colNewVal = (NewWSData.Unlimited.ToLower() == "1" ? "X" : "") });
                if (NewWSData.Serial_No_Profile != oldWSData.Serial_No_Profile)
                    _items.Add(new SMChange { colFieldName = 221, colOldVal = oldWSData.Serial_No_Profile, colNewVal = NewWSData.Serial_No_Profile });
                if (NewWSData.Repetitive_Mfg_Profile != oldWSData.Repetitive_Mfg_Profile)
                    _items.Add(new SMChange { colFieldName = 222, colOldVal = oldWSData.Repetitive_Mfg_Profile, colNewVal = NewWSData.Repetitive_Mfg_Profile });
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
                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("21", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
                if (ChangeSMatID1 > 0)
                {
                    foreach (var scItem in _items)
                    {
                        helperAccess.MaterialChangeDetails(ChangeSMatID1, scItem.colFieldName, scItem.colOldVal, scItem.colNewVal);
                    }
                }
                _items = new List<SMChange>();
            }
            //MSC_8300001775 End
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog1", ex);
        } 

        }

    private bool CheckUnitOfIssueWithBasicData1()
    {
        bool flag = true;
        try
        {
        if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
        {
            if (ddlUnitOfIssue.SelectedValue != "")
            {
                flag = ObjWorkSchedulingDataAccess.CheckValidUnitOfIssue(lblMasterHeaderId.Text, ddlUnitOfIssue.SelectedValue);
            }
            }
        }
        catch (Exception ex)
        { _log.Error("CheckUnitOfIssueWithBasicData1", ex); }
        return flag;
    }

    private bool CheckProdUnitWithBasicData1()
    {
        bool flag = true;
        try
        {
        if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 144))
        {
            if (ddlProductionUnit.SelectedValue != "")
            {
                flag = ObjWorkSchedulingDataAccess.CheckValidUnitOfIssue(lblMasterHeaderId.Text, ddlProductionUnit.SelectedValue);
            }
            }
        }
        catch (Exception ex)
        { _log.Error("CheckProdUnitWithBasicData1", ex); }
        return flag;
    }


    private bool CheckValidIssueUnt()
    {
        bool flag = true;
        try
        {
        if (ddlUnitOfIssue.SelectedValue != "")
        {
            flag = ObjWorkSchedulingDataAccess.CheckValidIssueUnit(lblMasterHeaderId.Text, ddlUnitOfIssue.SelectedValue);
        }

        }
        catch (Exception ex)
        { _log.Error("CheckValidIssueUnt", ex); }
        return flag;
    }

    private bool CheckValidProdUnit()
    {
        bool flag = true;
        try
        {
        if (ddlProductionUnit.SelectedValue != "")
        {
            flag = ObjWorkSchedulingDataAccess.CheckValidIssueUnit(lblMasterHeaderId.Text, ddlProductionUnit.SelectedValue);
        }

        }
        catch (Exception ex)
        { _log.Error("CheckValidProdUnit", ex); }
        return flag;
    }

    #endregion
}