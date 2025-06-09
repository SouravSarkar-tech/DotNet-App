using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using SectionConfiguration;
using log4net;
public partial class Transaction_WM2 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    WMAccess ObjWMgmtAccess = new WMAccess();
    HelperAccess helperAccess = new HelperAccess();
    Utility objUtil = new Utility();
    WareHouseMgmt2 objSavedWM2 = new WareHouseMgmt2();

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
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("WM2.aspx");
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
            WareHouseMgmt2 ObjWareHouseMgmt = GetControlValue();
            objSavedWM2 = GetWareHouseMgmtData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedWM2.Mat_WareHouse_Mgmt2_Id > 0)
                {
                    CheckIfChanges(ObjWareHouseMgmt, objSavedWM2);
                }
            }

            if (ObjWMgmtAccess.Save(ObjWareHouseMgmt) > 0)
            {
                //MSC_8300001775
                if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) //(HelperAccess.ReqType == "SMC")
                {
                    CheckIfChangesLog(ObjWareHouseMgmt, objSavedWM2);
                }
                //MSC_8300001775 
                ClearData();
                //FillDataGrid();
                FillFormDataByMHId();
                flg = true;
                ////MSC_8300001775
                //               if (HelperAccess.ReqType == "SMC")
                //               {
                //                   CheckIfChangesLog(ObjWareHouseMgmt, objSavedWM2);
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
        {
            _log.Error("Save", ex);
        }
        return flg;

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            lblWMId.Text = gvData.DataKeys[grdrow.RowIndex]["Mat_WareHouse_Mgmt2_Id"].ToString();
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

            txtLoadingEquipQuantity.Text = "";
            txtLoadingEquipQuantity2.Text = "";
            txtloadingEquipQuantity3.Text = "";

            PopuplateDropDownList();
        }
        catch (Exception ex)
        { _log.Error("ClearData", ex); }
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlWarehouse_SelectedIndexChanged", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");

            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {


                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            helperAccess.PopuplateDropDownList(ddlWarehouse, "pr_GetDropDownListByControlNameModuleType 'M','ddlWarehouse'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


            helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip2, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlUnitMeasureLoadingEquip3, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    void FillDataGrid()
    {
        try
        {
            DataSet ds = ObjWMgmtAccess.GetWareHouseMgmtData2(Convert.ToInt32(lblMasterHeaderId.Text));
            gvData.DataSource = ds;
            gvData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDataGrid", ex); }
    }

    private WareHouseMgmt2 GetWareHouseMgmtData()
    {
        return ObjWMgmtAccess.GetWareHouseMgmt2(Convert.ToInt32(lblWMId.Text));
    }

    private void FillFormDataByMHId()
    {
        try
        {
            DataSet ds;
            ds = ObjWMgmtAccess.GetWareHouseMgmtData2(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblWMId.Text = ds.Tables[0].Rows[0]["Mat_WareHouse_Mgmt2_Id"].ToString();
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
        WareHouseMgmt2 ObjWareHouseMgmt = GetWareHouseMgmtData();

        try
        {
            if (ObjWareHouseMgmt.Mat_WareHouse_Mgmt2_Id > 0)
            {
                lblWMId.Text = ObjWareHouseMgmt.Mat_WareHouse_Mgmt2_Id.ToString();
                lblMasterHeaderId.Text = ObjWareHouseMgmt.Master_Header_Id.ToString();

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {


                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjWareHouseMgmt.Plant_Id;
                ddlWarehouse.SelectedValue = ObjWareHouseMgmt.Warehouse_ID;

                helperAccess.PopuplateDropDownList(ddlStorageType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageType','19','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                helperAccess.PopuplateDropDownList(ddlStorageUnitType, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageUnitType2, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                helperAccess.PopuplateDropDownList(ddlStorageUnitType3, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageUnitType','" + lblSectionId.Text + "','" + ddlWarehouse.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                ddlStorageType.SelectedValue = ObjWareHouseMgmt.Storage_Type_ID;

                txtLoadingEquipQuantity.Text = ObjWareHouseMgmt.Loading_Equipment_Quantity;
                txtLoadingEquipQuantity2.Text = ObjWareHouseMgmt.Loading_Equipment_Quantity1;
                txtloadingEquipQuantity3.Text = ObjWareHouseMgmt.Loading_Equipment_Quantity2;
                ddlUnitMeasureLoadingEquip.SelectedValue = ObjWareHouseMgmt.Unit_Loading_Equip_Quan;
                ddlUnitMeasureLoadingEquip2.SelectedValue = ObjWareHouseMgmt.Unit_Loading_Equip_Quan1;
                ddlUnitMeasureLoadingEquip3.SelectedValue = ObjWareHouseMgmt.Unit_Loading_Equip_Quan2;
                ddlStorageUnitType.SelectedValue = ObjWareHouseMgmt.Storage_Unit_Type;
                ddlStorageUnitType2.SelectedValue = ObjWareHouseMgmt.Storage_Unit_Type1;
                ddlStorageUnitType3.SelectedValue = ObjWareHouseMgmt.Storage_Unit_Type2;
            }
            else
            {
                lblWMId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {


                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','WM2','" + lblWMId.Text + "'", "Plant_Name", "Plant_Id", "");
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

    private WareHouseMgmt2 GetControlValue()
    {
        WareHouseMgmt2 ObjWareHouseMgmt = new WareHouseMgmt2();
        try
        {
            ObjWareHouseMgmt.Mat_WareHouse_Mgmt2_Id = Convert.ToInt32(lblWMId.Text);
            ObjWareHouseMgmt.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);


            ObjWareHouseMgmt.Plant_Id = ddlPlant.SelectedValue;
            ObjWareHouseMgmt.Warehouse_ID = ddlWarehouse.SelectedValue;
            ObjWareHouseMgmt.Storage_Type_ID = ddlStorageType.SelectedValue;

            ObjWareHouseMgmt.Loading_Equipment_Quantity = txtLoadingEquipQuantity.Text;
            ObjWareHouseMgmt.Loading_Equipment_Quantity1 = txtLoadingEquipQuantity2.Text;
            ObjWareHouseMgmt.Loading_Equipment_Quantity2 = txtloadingEquipQuantity3.Text; ;
            ObjWareHouseMgmt.Unit_Loading_Equip_Quan = ddlUnitMeasureLoadingEquip.SelectedValue;
            ObjWareHouseMgmt.Unit_Loading_Equip_Quan1 = ddlUnitMeasureLoadingEquip2.SelectedValue;
            ObjWareHouseMgmt.Unit_Loading_Equip_Quan2 = ddlUnitMeasureLoadingEquip3.SelectedValue;
            ObjWareHouseMgmt.Storage_Unit_Type = ddlStorageUnitType.SelectedValue;
            ObjWareHouseMgmt.Storage_Unit_Type1 = ddlStorageUnitType2.SelectedValue;
            ObjWareHouseMgmt.Storage_Unit_Type2 = ddlStorageUnitType3.SelectedValue;

            ObjWareHouseMgmt.IsActive = 1;
            ObjWareHouseMgmt.UserId = lblUserId.Text;
            ObjWareHouseMgmt.TodayDate = objUtil.GetDate();
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
            SectionConfiguration.WM2 obj = new SectionConfiguration.WM2();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(WareHouseMgmt2 NewWM2Data, WareHouseMgmt2 oldWM2Data)
    {
        try
        {
            if (NewWM2Data.Mat_WareHouse_Mgmt2_Id > 0 && oldWM2Data.Mat_WareHouse_Mgmt2_Id > 0)
            {
                if (NewWM2Data.Plant_Id != oldWM2Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant ID</td> <td>" + oldWM2Data.Plant_Id + "</td><td>" + NewWM2Data.Plant_Id + "</td></tr>";
                if (NewWM2Data.Warehouse_ID != oldWM2Data.Warehouse_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Warehouse ID</td> <td>" + oldWM2Data.Warehouse_ID + "</td><td>" + NewWM2Data.Warehouse_ID + "</td></tr>";
                if (NewWM2Data.Storage_Type_ID != oldWM2Data.Storage_Type_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Type ID</td> <td>" + oldWM2Data.Storage_Type_ID + "</td><td>" + NewWM2Data.Storage_Type_ID + "</td></tr>";
                if (NewWM2Data.Loading_Equipment_Quantity != oldWM2Data.Loading_Equipment_Quantity)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Equipment Quantity</td> <td>" + oldWM2Data.Loading_Equipment_Quantity + "</td><td>" + NewWM2Data.Loading_Equipment_Quantity + "</td></tr>";
                if (NewWM2Data.Loading_Equipment_Quantity1 != oldWM2Data.Loading_Equipment_Quantity1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Equipment Quantity1</td> <td>" + oldWM2Data.Loading_Equipment_Quantity1 + "</td><td>" + NewWM2Data.Loading_Equipment_Quantity1 + "</td></tr>";
                if (NewWM2Data.Loading_Equipment_Quantity2 != oldWM2Data.Loading_Equipment_Quantity2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loading Equipment Quantity2</td> <td>" + oldWM2Data.Loading_Equipment_Quantity2 + "</td><td>" + NewWM2Data.Loading_Equipment_Quantity2 + "</td></tr>";
                if (NewWM2Data.Unit_Loading_Equip_Quan != oldWM2Data.Unit_Loading_Equip_Quan)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Loading Equip Quantity</td> <td>" + oldWM2Data.Unit_Loading_Equip_Quan + "</td><td>" + NewWM2Data.Unit_Loading_Equip_Quan + "</td></tr>";
                if (NewWM2Data.Unit_Loading_Equip_Quan1 != oldWM2Data.Unit_Loading_Equip_Quan1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Loading Equip Quantity1</td> <td>" + oldWM2Data.Unit_Loading_Equip_Quan1 + "</td><td>" + NewWM2Data.Unit_Loading_Equip_Quan1 + "</td></tr>";
                if (NewWM2Data.Unit_Loading_Equip_Quan2 != oldWM2Data.Unit_Loading_Equip_Quan2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Loading Equip Quantity2</td> <td>" + oldWM2Data.Unit_Loading_Equip_Quan2 + "</td><td>" + NewWM2Data.Unit_Loading_Equip_Quan2 + "</td></tr>";
                if (NewWM2Data.Storage_Unit_Type != oldWM2Data.Storage_Unit_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Unit Type</td> <td>" + oldWM2Data.Storage_Unit_Type + "</td><td>" + NewWM2Data.Storage_Unit_Type + "</td></tr>";
                if (NewWM2Data.Storage_Unit_Type1 != oldWM2Data.Storage_Unit_Type1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Unit Type1</td> <td>" + oldWM2Data.Storage_Unit_Type1 + "</td><td>" + NewWM2Data.Storage_Unit_Type1 + "</td></tr>";
                if (NewWM2Data.Storage_Unit_Type2 != oldWM2Data.Storage_Unit_Type2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Unit Type2</td> <td>" + oldWM2Data.Storage_Unit_Type2 + "</td><td>" + NewWM2Data.Storage_Unit_Type2 + "</td></tr>";
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
    private void CheckIfChangesLog(WareHouseMgmt2 NewWM2Data, WareHouseMgmt2 oldWM2Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewWM2Data.Mat_WareHouse_Mgmt2_Id > 0 && oldWM2Data.Mat_WareHouse_Mgmt2_Id > 0)
            {
                if (NewWM2Data.Storage_Type_ID != oldWM2Data.Storage_Type_ID)
                    _items.Add(new SMChange { colFieldName = 1226, colOldVal = oldWM2Data.Storage_Type_ID, colNewVal = NewWM2Data.Storage_Type_ID });
                if (NewWM2Data.Loading_Equipment_Quantity != oldWM2Data.Loading_Equipment_Quantity)
                    _items.Add(new SMChange { colFieldName = 212, colOldVal = oldWM2Data.Loading_Equipment_Quantity, colNewVal = NewWM2Data.Loading_Equipment_Quantity });
                if (NewWM2Data.Loading_Equipment_Quantity1 != oldWM2Data.Loading_Equipment_Quantity1)
                    _items.Add(new SMChange { colFieldName = 3, colOldVal = oldWM2Data.Loading_Equipment_Quantity1, colNewVal = NewWM2Data.Loading_Equipment_Quantity1 });
                if (NewWM2Data.Loading_Equipment_Quantity2 != oldWM2Data.Loading_Equipment_Quantity2)
                    _items.Add(new SMChange { colFieldName = 214, colOldVal = oldWM2Data.Loading_Equipment_Quantity2, colNewVal = NewWM2Data.Loading_Equipment_Quantity2 });
                if (NewWM2Data.Unit_Loading_Equip_Quan != oldWM2Data.Unit_Loading_Equip_Quan)
                    _items.Add(new SMChange { colFieldName = 215, colOldVal = oldWM2Data.Unit_Loading_Equip_Quan, colNewVal = NewWM2Data.Unit_Loading_Equip_Quan });
                if (NewWM2Data.Unit_Loading_Equip_Quan1 != oldWM2Data.Unit_Loading_Equip_Quan1)
                    _items.Add(new SMChange { colFieldName = 216, colOldVal = oldWM2Data.Unit_Loading_Equip_Quan1, colNewVal = NewWM2Data.Unit_Loading_Equip_Quan1 });
                if (NewWM2Data.Unit_Loading_Equip_Quan2 != oldWM2Data.Unit_Loading_Equip_Quan2)
                    _items.Add(new SMChange { colFieldName = 217, colOldVal = oldWM2Data.Unit_Loading_Equip_Quan2, colNewVal = NewWM2Data.Unit_Loading_Equip_Quan2 });
                if (NewWM2Data.Storage_Unit_Type != oldWM2Data.Storage_Unit_Type)
                    _items.Add(new SMChange { colFieldName = 218, colOldVal = oldWM2Data.Storage_Unit_Type, colNewVal = NewWM2Data.Storage_Unit_Type });
                if (NewWM2Data.Storage_Unit_Type1 != oldWM2Data.Storage_Unit_Type1)
                    _items.Add(new SMChange { colFieldName = 219, colOldVal = oldWM2Data.Storage_Unit_Type1, colNewVal = NewWM2Data.Storage_Unit_Type1 });
                if (NewWM2Data.Storage_Unit_Type2 != oldWM2Data.Storage_Unit_Type2)
                    _items.Add(new SMChange { colFieldName = 220, colOldVal = oldWM2Data.Storage_Unit_Type2, colNewVal = NewWM2Data.Storage_Unit_Type2 });
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
                ChangeSMatID1 = helperAccess.MaterialChange("20", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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