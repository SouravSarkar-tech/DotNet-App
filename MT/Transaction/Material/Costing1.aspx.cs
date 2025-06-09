using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Data;
using log4net;
public partial class Transaction_Costing1 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    CostingAccess ObjCostingAccess = new CostingAccess();
    Costing1 objSavedCosting1 = new Costing1();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
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

                HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    trButton.Visible = true;
                    btnSave.Visible = !btnNext.Visible;
                    if (!btnPrevious.Visible && !btnNext.Visible)
                        btnSave.Visible = false;
                }

                ClearCostingData();
                //FillCostingData();
                //FillCostingDataGrid();
                ConfigureControl();
               
                //To manage the Creation Single request
                FillFormDataByMHId();
                lnlAddDetails.Visible = false;
                grvCosting1.Visible = false;
                //MSC_8300001775 Start
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    ConfigureControlForSChange();
                }
                //MSC_8300001775 End
            }
        }
        else
        {
            Response.Redirect("materialmaster.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
        ClearCostingData();
        FillCostingData();
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
            lblCostingId.Text = grvCosting1.DataKeys[grdrow.RowIndex]["Mat_Costing1_Id"].ToString();
            FillCostingData();
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
            //lblMsg.Text = Messages.GetMessage(1);
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("Costing1.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        if (Save())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
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

    #region Methods

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
        ddlCosting.Enabled = true;
        txtBom.Enabled = true;
        ddlMBomUsage.Enabled = true;
        txtKeyTaskListGroup.Enabled = true;
        txtGroupCenter.Enabled = true;
        ddlTaskListType.Enabled = true;
        txtBom.CssClass = "txtbox";
        txtKeyTaskListGroup.CssClass = "txtbox";
        txtGroupCenter.CssClass = "txtbox";
        txtBom.CssClass = "txtbox";
        chkMaterialRelatedOrigin.Checked = true;
        chkMaterialCosted.Checked = true;
        ddlSpecialProcurement.Enabled = true;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {

            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");

        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
        }
        //CTRL_SUB_SDT18112019 Added by NR

        //helperAccess.PopuplateDropDownList(ddlCosting, "pr_GetDropDownListByControlNameModuleType 'M','ddlCosting','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue+"LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlCosting, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlCosting','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        //helperAccess.PopuplateDropDownList(ddlKeyTaskListGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlKeyTaskListGroup'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlMBomUsage, "pr_GetDropDownListByControlNameModuleType 'M','ddlMBomUsage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlOriginGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlOriginGroup','" + lblSectionId.Text + "','L0IN'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','0','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlTaskListType, "pr_GetDropDownListByControlNameModuleType 'M','ddlTaskListType'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlSpecialProcurement, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcurement','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlVarianceKey, "pr_GetDropDownListByControlNameModuleType 'M','ddlVarianceKey','" + lblSectionId.Text + "','OR'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }
    

    #region Get

    private void FillCostingDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjCostingAccess.GetCostingData1(Convert.ToInt32(lblMasterHeaderId.Text));

            grvCosting1.DataSource = ds;
            grvCosting1.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillCostingDataGrid", ex);
        }
    }

    private void FillCostingData()
    {
        try
        {
            Costing1 ObjCost = GetCostingData();
            if (ObjCost.Mat_Costing1_Id > 0)
            {
                lblCostingId.Text = ObjCost.Mat_Costing1_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");

                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjCost.Plant_Id;
                BindPlantWiseDropDown();

                ddlProfitCenter.SelectedValue = ObjCost.Profit_Center;
                ddlOriginGroup.SelectedValue = ObjCost.Origin_Group;
                txtBom.Text = ObjCost.Alternative_BOM;
                ddlMBomUsage.SelectedValue = ObjCost.BOM_Usage;
                txtKeyTaskListGroup.Text = ObjCost.Key_Task_List_Grp;
                txtGroupCenter.Text = ObjCost.Group_Counter;
                txtBTCI.Text = ObjCost.Lot_Size_Prd_Cost_Est;
                ddlSpecialProcurement.SelectedValue = ObjCost.Spl_Procurement_Type;
                ddlCosting.SelectedValue = ObjCost.Costing_Overhead_Grp;
                chkMaterialCosted.Checked = ObjCost.Is_Mat_Costed_Qnty_Struc == "1" ? true : false;
                ddlVarianceKey.SelectedValue = ObjCost.Variance_Key;
                chkDoNotCost.Checked = ObjCost.Do_Not_Cost == "1" ? true : false;
                chkMaterialRelatedOrigin.Checked = ObjCost.Material_Related_Origin == "1" ? true : false;
                ddlTaskListType.SelectedValue = ObjCost.Task_List_Type;
            }
            else
            {
                //ClearCostingData();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
                    
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C1','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();

                chkMaterialCosted.Checked = true;
                chkMaterialRelatedOrigin.Checked = true;

                txtBTCI.Text = "100";
                ddlVarianceKey.SelectedValue = "000001";

                if (lblModuleId.Text == "162")
                {
                    ddlOriginGroup.SelectedValue = "M001";
                }
                else if (lblModuleId.Text == "164")
                {
                    ddlOriginGroup.SelectedValue = "M002";
                }
            }

            chkMaterialCosted.Enabled = false;
            chkMaterialRelatedOrigin.Enabled = false;
            //ddlOriginGroup.Enabled = false;

            ddlPlant.Enabled = false;

        }
        catch (Exception ex)
        {
            _log.Error("FillCostingData", ex);
        }
    }

    private void ClearCostingData()
    {
        try
        {
            lblCostingId.Text = "0";
            txtBom.Text = "";
            txtGroupCenter.Text = "";
            txtBTCI.Text = "";
            chkMaterialCosted.Checked = false;
            chkDoNotCost.Checked = false;
            chkMaterialRelatedOrigin.Checked = false;

            PopuplateDropDownList();
            //FillCostingData();
        }
        catch (Exception ex)
        {
            _log.Error("ClearCostingData", ex);
        }
    }

    private Costing1 GetCostingData()
    {
        return ObjCostingAccess.GetCosting1(Convert.ToInt32(lblCostingId.Text)); //Convert.ToInt32(ddlPlant.SelectedValue));
    }

    #endregion

    #region Save

    private bool Save()
    {
        bool flg = false;
        try
        {
            Costing1 ObjCost = GetControlsValue();
            objSavedCosting1 = GetCostingData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedCosting1.Mat_Costing1_Id > 0)
                {
                    CheckIfChanges(ObjCost, objSavedCosting1);
                }
            }

            if (ObjCost.Plant_Id != null)
            {
                if (ObjCostingAccess.Save(ObjCost) > 0)
                {
                    //MSC_8300001775
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                    {
                        CheckIfChangesLog(ObjCost, objSavedCosting1);
                    }
                    //MSC_8300001775
                    ClearCostingData();
                    //FillCostingDataGrid();
                    //FillCostingData();
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
                lblMsg.Text = "Please Select atleast one Plant to proceed.";
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

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
     
        ds = ObjCostingAccess.GetCostingData1(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblCostingId.Text = ds.Tables[0].Rows[0]["Mat_Costing1_Id"].ToString();
        }
        FillCostingData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlSpecialProcurement, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlSpecialProcurement','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','0','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlCosting, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlCosting','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private Costing1 GetControlsValue()
    {
        Costing1 ObjCost = new Costing1();
        Utility objUtil = new Utility();

        try
        {
            ObjCost.Mat_Costing1_Id = Convert.ToInt32(lblCostingId.Text);
            ObjCost.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjCost.Plant_Id = ddlPlant.SelectedValue; //GetSelectedCheckedValue(ddlPlant);
            ObjCost.Profit_Center = ddlProfitCenter.SelectedValue;
            ObjCost.Origin_Group = ddlOriginGroup.SelectedValue;
            ObjCost.Alternative_BOM = txtBom.Text;
            ObjCost.BOM_Usage = ddlMBomUsage.SelectedValue;
            ObjCost.Key_Task_List_Grp = txtKeyTaskListGroup.Text;
            ObjCost.Group_Counter = txtGroupCenter.Text;
            ObjCost.Lot_Size_Prd_Cost_Est = txtBTCI.Text;
            ObjCost.Spl_Procurement_Type = ddlSpecialProcurement.SelectedValue;
            ObjCost.Costing_Overhead_Grp = ddlCosting.SelectedValue;
            ObjCost.Is_Mat_Costed_Qnty_Struc = chkMaterialCosted.Checked == true ? "1" : "0";
            ObjCost.Variance_Key = ddlVarianceKey.SelectedValue;
            ObjCost.Do_Not_Cost = chkDoNotCost.Checked == true ? "1" : "0";

            ObjCost.Material_Related_Origin = chkMaterialRelatedOrigin.Checked == true ? "1" : "0";
            ObjCost.Task_List_Type = ddlTaskListType.SelectedValue;

            ObjCost.IsActive = 1;
            ObjCost.UserId = lblUserId.Text;
            ObjCost.TodayDate = objUtil.GetDate();
            ObjCost.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjCost;
    }

    #endregion

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Costing1 obj = new SectionConfiguration.Costing1();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Costing1 NewCosting1Data, Costing1 oldCosting1Data)
    {
        try
        {
            if (NewCosting1Data.Mat_Costing1_Id > 0 && oldCosting1Data.Mat_Costing1_Id > 0)
            {
                if (NewCosting1Data.Plant_Id != oldCosting1Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldCosting1Data.Plant_Id + "</td><td>" + NewCosting1Data.Plant_Id + "</td></tr>";
                if (NewCosting1Data.Profit_Center != oldCosting1Data.Profit_Center)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Profit Center</td> <td>" + oldCosting1Data.Profit_Center + "</td><td>" + NewCosting1Data.Profit_Center + "</td></tr>";
                if (NewCosting1Data.Origin_Group != oldCosting1Data.Origin_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Origin Group</td> <td>" + oldCosting1Data.Origin_Group + "</td><td>" + NewCosting1Data.Origin_Group + "</td></tr>";
                if (NewCosting1Data.Material_Related_Origin != (oldCosting1Data.Material_Related_Origin.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Related Origin</td> <td>" + (oldCosting1Data.Material_Related_Origin.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewCosting1Data.Material_Related_Origin + "</td></tr>";
                if (NewCosting1Data.Alternative_BOM != oldCosting1Data.Alternative_BOM)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alternative BOM</td> <td>" + oldCosting1Data.Alternative_BOM + "</td><td>" + NewCosting1Data.Alternative_BOM + "</td></tr>";
                if (NewCosting1Data.BOM_Usage != oldCosting1Data.BOM_Usage)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>BOM Usage</td> <td>" + oldCosting1Data.BOM_Usage + "</td><td>" + NewCosting1Data.BOM_Usage + "</td></tr>";
                if (NewCosting1Data.Key_Task_List_Grp != oldCosting1Data.Key_Task_List_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Key Task List Grp</td> <td>" + oldCosting1Data.Key_Task_List_Grp + "</td><td>" + NewCosting1Data.Key_Task_List_Grp + "</td></tr>";
                if (NewCosting1Data.Group_Counter != oldCosting1Data.Group_Counter)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Group Counter</td> <td>" + oldCosting1Data.Group_Counter + "</td><td>" + NewCosting1Data.Group_Counter + "</td></tr>";
                if (NewCosting1Data.Task_List_Type != oldCosting1Data.Task_List_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Task List Type</td> <td>" + oldCosting1Data.Task_List_Type + "</td><td>" + NewCosting1Data.Task_List_Type + "</td></tr>";
                if (NewCosting1Data.Lot_Size_Prd_Cost_Est != oldCosting1Data.Lot_Size_Prd_Cost_Est)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Lot Size Prd Cost Est</td> <td>" + oldCosting1Data.Lot_Size_Prd_Cost_Est + "</td><td>" + NewCosting1Data.Lot_Size_Prd_Cost_Est + "</td></tr>";
                if (NewCosting1Data.Spl_Procurement_Type != oldCosting1Data.Spl_Procurement_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Special Procurement Type</td> <td>" + oldCosting1Data.Spl_Procurement_Type + "</td><td>" + NewCosting1Data.Spl_Procurement_Type + "</td></tr>";
                if (NewCosting1Data.Costing_Overhead_Grp != oldCosting1Data.Costing_Overhead_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Costing Overhead Grp </td> <td>" + oldCosting1Data.Costing_Overhead_Grp + "</td><td>" + NewCosting1Data.Costing_Overhead_Grp + "</td></tr>";
                if (NewCosting1Data.Is_Mat_Costed_Qnty_Struc != (oldCosting1Data.Is_Mat_Costed_Qnty_Struc.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Is Mat Costed Qnty Struc</td> <td>" + (oldCosting1Data.Is_Mat_Costed_Qnty_Struc.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewCosting1Data.Is_Mat_Costed_Qnty_Struc + "</td></tr>";
                if (NewCosting1Data.Variance_Key != oldCosting1Data.Variance_Key)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Variance Key</td> <td>" + oldCosting1Data.Variance_Key + "</td><td>" + NewCosting1Data.Variance_Key + "</td></tr>";
                if (NewCosting1Data.Do_Not_Cost != (oldCosting1Data.Do_Not_Cost.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Do Not Cost</td> <td>" + (oldCosting1Data.Do_Not_Cost.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewCosting1Data.Do_Not_Cost + "</td></tr>";
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
    private void CheckIfChangesLog(Costing1 NewCosting1Data, Costing1 oldCosting1Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewCosting1Data.Mat_Costing1_Id >= 0 && oldCosting1Data.Mat_Costing1_Id >= 0)
            {
                if (NewCosting1Data.Profit_Center != oldCosting1Data.Profit_Center)
                    _items.Add(new SMChange { colFieldName = 26, colOldVal = oldCosting1Data.Profit_Center, colNewVal = NewCosting1Data.Profit_Center });
                if (NewCosting1Data.Origin_Group != oldCosting1Data.Origin_Group)
                    _items.Add(new SMChange { colFieldName = 27, colOldVal = oldCosting1Data.Origin_Group, colNewVal = NewCosting1Data.Origin_Group });
                if ((NewCosting1Data.Material_Related_Origin.ToLower() == "1" ? "true" : "false") != (oldCosting1Data.Material_Related_Origin.ToLower() == "1" ? "true" : "false"))
                    _items.Add(new SMChange { colFieldName = 1173, colOldVal = (oldCosting1Data.Material_Related_Origin.ToLower() == "1" ? "X" : ""), colNewVal = (NewCosting1Data.Material_Related_Origin.ToLower() == "1" ? "X" : "") });
                if (NewCosting1Data.Alternative_BOM != oldCosting1Data.Alternative_BOM)
                    _items.Add(new SMChange { colFieldName = 28, colOldVal = oldCosting1Data.Alternative_BOM, colNewVal = NewCosting1Data.Alternative_BOM });
                if (NewCosting1Data.BOM_Usage != oldCosting1Data.BOM_Usage)
                    _items.Add(new SMChange { colFieldName = 29, colOldVal = oldCosting1Data.BOM_Usage, colNewVal = NewCosting1Data.BOM_Usage });
                if (NewCosting1Data.Key_Task_List_Grp != oldCosting1Data.Key_Task_List_Grp)
                    _items.Add(new SMChange { colFieldName = 30, colOldVal = oldCosting1Data.Key_Task_List_Grp, colNewVal = NewCosting1Data.Key_Task_List_Grp });
                if (NewCosting1Data.Group_Counter != oldCosting1Data.Group_Counter)
                    _items.Add(new SMChange { colFieldName = 31, colOldVal = oldCosting1Data.Group_Counter, colNewVal = NewCosting1Data.Group_Counter });
                if (NewCosting1Data.Task_List_Type != oldCosting1Data.Task_List_Type)
                    _items.Add(new SMChange { colFieldName = 1172, colOldVal = oldCosting1Data.Task_List_Type, colNewVal = NewCosting1Data.Task_List_Type });
                if (NewCosting1Data.Lot_Size_Prd_Cost_Est != oldCosting1Data.Lot_Size_Prd_Cost_Est)
                    _items.Add(new SMChange { colFieldName = 32, colOldVal = oldCosting1Data.Lot_Size_Prd_Cost_Est, colNewVal = NewCosting1Data.Lot_Size_Prd_Cost_Est });
                if (NewCosting1Data.Spl_Procurement_Type != oldCosting1Data.Spl_Procurement_Type)
                    _items.Add(new SMChange { colFieldName = 33, colOldVal = oldCosting1Data.Spl_Procurement_Type, colNewVal = NewCosting1Data.Spl_Procurement_Type });
                if (NewCosting1Data.Costing_Overhead_Grp != oldCosting1Data.Costing_Overhead_Grp)
                    _items.Add(new SMChange { colFieldName = 34, colOldVal = oldCosting1Data.Costing_Overhead_Grp, colNewVal = NewCosting1Data.Costing_Overhead_Grp });
                if ((NewCosting1Data.Is_Mat_Costed_Qnty_Struc.ToLower() == "1" ? "true" : "false") != (oldCosting1Data.Is_Mat_Costed_Qnty_Struc.ToLower() == "1" ? "true" : "false"))
                    _items.Add(new SMChange { colFieldName = 35, colOldVal = (oldCosting1Data.Is_Mat_Costed_Qnty_Struc.ToLower() == "1" ? "X" : ""), colNewVal = (NewCosting1Data.Is_Mat_Costed_Qnty_Struc.ToLower() == "1" ? "X" : "") });
                if (NewCosting1Data.Variance_Key != oldCosting1Data.Variance_Key)
                    _items.Add(new SMChange { colFieldName = 36, colOldVal = oldCosting1Data.Variance_Key, colNewVal = NewCosting1Data.Variance_Key });
                if ((NewCosting1Data.Do_Not_Cost.ToLower() == "1" ? "true" : "false") != (oldCosting1Data.Do_Not_Cost.ToLower() == "1" ? "true" : "false"))
                    _items.Add(new SMChange { colFieldName = 37, colOldVal = (oldCosting1Data.Do_Not_Cost.ToLower() == "1" ? "X" : ""), colNewVal = (NewCosting1Data.Do_Not_Cost.ToLower() == "1" ? "X" : "") });
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
                ChangeSMatID1 = helperAccess.MaterialChange("5", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
                if (ChangeSMatID1 > 0)
                {
                    foreach (var scItem in _items)
                    {
                        helperAccess.MaterialChangeDetails(ChangeSMatID1, scItem.colFieldName, scItem.colOldVal, scItem.colNewVal);
                    }
                }
                _items = new List<SMChange>();
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog1", ex);
        }

    }


    #endregion
}