using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using SectionConfiguration;
using Accenture.MWT.DomainObject;
using System.Data;
using log4net;
public partial class Transaction_WM1 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    WMAccess ObjWMgmtAccess = new WMAccess();
    HelperAccess helperAccess = new HelperAccess();
    Utility objUtil = new Utility();
    WareHouseMgmt1 objSavedWM1 = new WareHouseMgmt1();

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

                        ClearData();
                        //FillDataGrid();
                        ConfigureControl();

                        //To manage the Creation Single request
                        FillFormDataByMHId();
                        lnlAddDetails.Visible = false;
                        gvData.Visible = false;
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
                Response.Redirect("WM1.aspx");
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

    protected bool Save()
    {
        bool flg = false;
        try
        {
            WareHouseMgmt1 ObjWareHouseMgmt1 = GetControlValue();
            objSavedWM1 = GetWareHouseMgmtData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedWM1.Mat_WareHouse_Mgmt1_Id > 0)
                {
                    CheckIfChanges(ObjWareHouseMgmt1, objSavedWM1);
                }
            }
            if (ObjWMgmtAccess.Save(ObjWareHouseMgmt1) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjWareHouseMgmt1, objSavedWM1);
                }
                //MSC_8300001775 

                //FillDataGrid();
                ClearData();
                FillFormDataByMHId();
                flg = true;
                ////MSC_8300001775
                //               if (HelperAccess.ReqType == "SMC")
                //               {
                //                   CheckIfChangesLog(ObjWareHouseMgmt1, objSavedWM1);
                //               }
                //               //MSC_8300001775 
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("Save", ex); }
        return flg;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblWMId.Text = "0";

            chkIndiAllowAdditionalExisting.Checked = false;
            chkIndicatorMassageInv.Checked = false;
            txtCapacityUsage.Text = "";

            PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblWMId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_WareHouse_Mgmt1_Id"].ToString();
            FillWareHouseMgmtData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
        }
    }

    protected void lnlAddDetails_Click(object s, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception ex)
        { _log.Error("lnlAddDetails_Click", ex); }
    }



    void FillDataGrid()
    {
        try
        {
            DataSet ds = ObjWMgmtAccess.GetWareHouseMgmtData1(Convert.ToInt32(lblMasterHeaderId.Text));
            gvData.DataSource = ds;
            gvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDataGrid", ex); }
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlWarehouse_SelectedIndexChanged", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR


            helperAccess.PopuplateDropDownList(ddlWarehouse, "pr_GetDropDownListByControlNameModuleType 'M','ddlWarehouse'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlWarehouse, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType'", "LookUp_Desc", "LookUp_Code", "");



            helperAccess.PopuplateDropDownList(ddlBulkStorageIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlBulkStorageIndi'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlWareHouseMangUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlWareHouseMangUnit'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCapacityUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private WareHouseMgmt1 GetWareHouseMgmtData()
    {
        return ObjWMgmtAccess.GetWareHouseMgmt1(Convert.ToInt32(lblWMId.Text));
    }

    private void FillFormDataByMHId()
    {
        try
        {
            DataSet ds;
            ds = ObjWMgmtAccess.GetWareHouseMgmtData1(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblWMId.Text = ds.Tables[0].Rows[0]["Mat_WareHouse_Mgmt1_Id"].ToString();
            }
            FillWareHouseMgmtData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        //
    }

    private void FillWareHouseMgmtData()
    {
        try
        {
            WareHouseMgmt1 ObjWareHouseMgmt = GetWareHouseMgmtData();

            if (ObjWareHouseMgmt.Mat_WareHouse_Mgmt1_Id > 0)
            {
                lblWMId.Text = ObjWareHouseMgmt.Mat_WareHouse_Mgmt1_Id.ToString();
                lblMasterHeaderId.Text = ObjWareHouseMgmt.Master_Header_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjWareHouseMgmt.Plant_Id;
                ddlWarehouse.SelectedValue = ObjWareHouseMgmt.Warehouse_ID;

                helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageSectIndi, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageSectIndi','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlacement, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageTypeIndiSPlaceRemoval, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageTypeIndiSPlaceRemoval','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlStorageType.SelectedValue = ObjWareHouseMgmt.Storage_Type_ID;
                txtCapacityUsage.Text = ObjWareHouseMgmt.Capacity_Usage;
                ddlCapacityUnit.SelectedValue = ObjWareHouseMgmt.Capacity_Unit;
                ddlWareHouseMangUnit.SelectedValue = ObjWareHouseMgmt.WM_Unit_Measure;
                chkIndicatorMassageInv.Checked = ObjWareHouseMgmt.Is_Msg_Inventory_Mgmt.ToLower() == "true" ? true : false;
                ddlStorageSectIndi.SelectedValue = ObjWareHouseMgmt.Storage_Section_Ind;
                ddlStorageTypeIndiSPlacement.SelectedValue = ObjWareHouseMgmt.Stor_Type_Ind_Stock_Placement;
                ddlStorageTypeIndiSPlaceRemoval.SelectedValue = ObjWareHouseMgmt.Stor_Type_Ind_Stock_Removal;
                chkIndiAllowAdditionalExisting.Checked = ObjWareHouseMgmt.Is_Allow_Add_Exist_Stock.ToLower() == "true" ? true : false;
                ddlBulkStorageIndi.SelectedValue = ObjWareHouseMgmt.Bulk_Storage_Ind;


            }
            else
            {
                lblWMId.Text = "0";

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM1','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
            }

            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        { _log.Error("FillWareHouseMgmtData", ex); }
    }

    private WareHouseMgmt1 GetControlValue()
    {
        WareHouseMgmt1 ObjWareHouseMgmt = new WareHouseMgmt1();
        try
        {
            ObjWareHouseMgmt.Mat_WareHouse_Mgmt1_Id = Convert.ToInt32(lblWMId.Text);
            ObjWareHouseMgmt.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            ObjWareHouseMgmt.Plant_Id = ddlPlant.SelectedValue;
            ObjWareHouseMgmt.Warehouse_ID = ddlWarehouse.SelectedValue;
            ObjWareHouseMgmt.Storage_Type_ID = ddlStorageType.SelectedValue;
            ObjWareHouseMgmt.Capacity_Usage = txtCapacityUsage.Text;
            ObjWareHouseMgmt.Capacity_Unit = ddlCapacityUnit.SelectedValue;
            ObjWareHouseMgmt.WM_Unit_Measure = ddlWareHouseMangUnit.SelectedValue;
            ObjWareHouseMgmt.Is_Msg_Inventory_Mgmt = chkIndicatorMassageInv.Checked == true ? "1" : "0";
            ObjWareHouseMgmt.Storage_Section_Ind = ddlStorageSectIndi.SelectedValue;
            ObjWareHouseMgmt.Stor_Type_Ind_Stock_Placement = ddlStorageTypeIndiSPlacement.SelectedValue;
            ObjWareHouseMgmt.Stor_Type_Ind_Stock_Removal = ddlStorageTypeIndiSPlaceRemoval.SelectedValue;
            ObjWareHouseMgmt.Is_Allow_Add_Exist_Stock = chkIndiAllowAdditionalExisting.Checked == true ? "1" : "0"; ;
            ObjWareHouseMgmt.Bulk_Storage_Ind = ddlBulkStorageIndi.SelectedValue;

            ObjWareHouseMgmt.IsActive = 1;
            ObjWareHouseMgmt.UserId = lblUserId.Text;
            //ObjWareHouseMgmt.TodayDate = objUtil.GetDate();
            ObjWareHouseMgmt.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlValue", ex);
        }
        return ObjWareHouseMgmt;
    }

    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.WM1 obj = new SectionConfiguration.WM1();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(WareHouseMgmt1 NewWM1Data, WareHouseMgmt1 oldWM1Data)
    {
        try
        {
            if (NewWM1Data.Mat_WareHouse_Mgmt1_Id > 0 && oldWM1Data.Mat_WareHouse_Mgmt1_Id > 0)
            {
                if (NewWM1Data.Plant_Id != oldWM1Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant ID</td> <td>" + oldWM1Data.Plant_Id + "</td><td>" + NewWM1Data.Plant_Id + "</td></tr>";
                if (NewWM1Data.Warehouse_ID != oldWM1Data.Warehouse_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Warehouse ID</td> <td>" + oldWM1Data.Warehouse_ID + "</td><td>" + NewWM1Data.Warehouse_ID + "</td></tr>";
                if (NewWM1Data.Storage_Type_ID != oldWM1Data.Storage_Type_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Type ID</td> <td>" + oldWM1Data.Storage_Type_ID + "</td><td>" + NewWM1Data.Storage_Type_ID + "</td></tr>";
                if (NewWM1Data.Capacity_Usage != oldWM1Data.Capacity_Usage)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Capacity Usage</td> <td>" + oldWM1Data.Capacity_Usage + "</td><td>" + NewWM1Data.Capacity_Usage + "</td></tr>";
                if (NewWM1Data.Capacity_Unit != oldWM1Data.Capacity_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Capacity Unit</td> <td>" + oldWM1Data.Capacity_Unit + "</td><td>" + NewWM1Data.Capacity_Unit + "</td></tr>";
                if (NewWM1Data.WM_Unit_Measure != oldWM1Data.WM_Unit_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>WM Unit Measure</td> <td>" + oldWM1Data.WM_Unit_Measure + "</td><td>" + NewWM1Data.WM_Unit_Measure + "</td></tr>";
                if (NewWM1Data.Is_Msg_Inventory_Mgmt != (oldWM1Data.Is_Msg_Inventory_Mgmt.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Is Msg Inventory Mgmt</td> <td>" + (oldWM1Data.Is_Msg_Inventory_Mgmt.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewWM1Data.Is_Msg_Inventory_Mgmt + "</td></tr>";
                if (NewWM1Data.Storage_Section_Ind != oldWM1Data.Storage_Section_Ind)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Section Ind</td> <td>" + oldWM1Data.Storage_Section_Ind + "</td><td>" + NewWM1Data.Storage_Section_Ind + "</td></tr>";
                if (NewWM1Data.Stor_Type_Ind_Stock_Placement != oldWM1Data.Stor_Type_Ind_Stock_Placement)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Stor Type Ind Stock Placement</td> <td>" + oldWM1Data.Stor_Type_Ind_Stock_Placement + "</td><td>" + NewWM1Data.Stor_Type_Ind_Stock_Placement + "</td></tr>";
                if (NewWM1Data.Stor_Type_Ind_Stock_Removal != oldWM1Data.Stor_Type_Ind_Stock_Removal)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Stor Type Ind Stock Removal</td> <td>" + oldWM1Data.Stor_Type_Ind_Stock_Removal + "</td><td>" + NewWM1Data.Stor_Type_Ind_Stock_Removal + "</td></tr>";
                if (NewWM1Data.Is_Allow_Add_Exist_Stock != (oldWM1Data.Is_Allow_Add_Exist_Stock.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Is Allow Add Exist Stock</td> <td>" + (oldWM1Data.Is_Allow_Add_Exist_Stock.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewWM1Data.Is_Allow_Add_Exist_Stock + "</td></tr>";
                if (NewWM1Data.Bulk_Storage_Ind != oldWM1Data.Bulk_Storage_Ind)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Bulk Storage Ind</td> <td>" + oldWM1Data.Bulk_Storage_Ind + "</td><td>" + NewWM1Data.Bulk_Storage_Ind + "</td></tr>";
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
    private void CheckIfChangesLog(WareHouseMgmt1 NewWM1Data, WareHouseMgmt1 oldWM1Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewWM1Data.Mat_WareHouse_Mgmt1_Id > 0 && oldWM1Data.Mat_WareHouse_Mgmt1_Id > 0)
            {
                if (NewWM1Data.Storage_Type_ID != oldWM1Data.Storage_Type_ID)
                    _items.Add(new SMChange { colFieldName = 1223, colOldVal = oldWM1Data.Storage_Type_ID, colNewVal = NewWM1Data.Storage_Type_ID });
                if (NewWM1Data.Capacity_Usage != oldWM1Data.Capacity_Usage)
                    _items.Add(new SMChange { colFieldName = 1220, colOldVal = oldWM1Data.Capacity_Usage, colNewVal = NewWM1Data.Capacity_Usage });
                if (NewWM1Data.Capacity_Unit != oldWM1Data.Capacity_Unit)
                    _items.Add(new SMChange { colFieldName = 1219, colOldVal = oldWM1Data.Capacity_Unit, colNewVal = NewWM1Data.Capacity_Unit });
                if (NewWM1Data.WM_Unit_Measure != oldWM1Data.WM_Unit_Measure)
                    _items.Add(new SMChange { colFieldName = 205, colOldVal = oldWM1Data.WM_Unit_Measure, colNewVal = NewWM1Data.WM_Unit_Measure });
                if (NewWM1Data.Is_Msg_Inventory_Mgmt != (oldWM1Data.Is_Msg_Inventory_Mgmt.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 206, colOldVal = (oldWM1Data.Is_Msg_Inventory_Mgmt.ToLower() == "true" ? "1" : "0"), colNewVal = NewWM1Data.Is_Msg_Inventory_Mgmt });
                if (NewWM1Data.Storage_Section_Ind != oldWM1Data.Storage_Section_Ind)
                    _items.Add(new SMChange { colFieldName = 207, colOldVal = oldWM1Data.Storage_Section_Ind, colNewVal = NewWM1Data.Storage_Section_Ind });
                if (NewWM1Data.Stor_Type_Ind_Stock_Placement != oldWM1Data.Stor_Type_Ind_Stock_Placement)
                    _items.Add(new SMChange { colFieldName = 208, colOldVal = oldWM1Data.Stor_Type_Ind_Stock_Placement, colNewVal = NewWM1Data.Stor_Type_Ind_Stock_Placement });
                if (NewWM1Data.Stor_Type_Ind_Stock_Removal != oldWM1Data.Stor_Type_Ind_Stock_Removal)
                    _items.Add(new SMChange { colFieldName = 209, colOldVal = oldWM1Data.Stor_Type_Ind_Stock_Removal, colNewVal = NewWM1Data.Stor_Type_Ind_Stock_Removal });
                if (NewWM1Data.Is_Allow_Add_Exist_Stock != (oldWM1Data.Is_Allow_Add_Exist_Stock.ToLower() == "true" ? "1" : "0"))
                    _items.Add(new SMChange { colFieldName = 210, colOldVal = (oldWM1Data.Is_Allow_Add_Exist_Stock.ToLower() == "true" ? "1" : "0"), colNewVal = NewWM1Data.Is_Allow_Add_Exist_Stock });
                if (NewWM1Data.Bulk_Storage_Ind != oldWM1Data.Bulk_Storage_Ind)
                    _items.Add(new SMChange { colFieldName = 211, colOldVal = oldWM1Data.Bulk_Storage_Ind, colNewVal = NewWM1Data.Bulk_Storage_Ind });
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
                ChangeSMatID1 = helperAccess.MaterialChange("19", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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
}