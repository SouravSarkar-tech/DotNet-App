using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Data;
using System.IO;
using log4net;
public partial class Transaction_Costing2 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    CostingAccess ObjCostingAccess = new CostingAccess();
    HelperAccess helperAccess = new HelperAccess();
    Costing2 objSavedCosting2 = new Costing2();

    string sdate = "";
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "Start" + HelperAccess.ReqType);
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

                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                        btnSave.Visible = !btnNext.Visible;
                        if (!btnPrevious.Visible && !btnNext.Visible)
                            btnSave.Visible = false;
                        //MSC_8300001775 Start
                        if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                        {
                            btnSave.Visible = true;
                        }
                        //MSC_8300001775 End 
                        //btnNext.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("MaterialMaster.aspx");
                }

                //FillCostingDataGrid();
                ClearCostingData();
                ConfigureControl();

                //To manage the Creation Single request
                FillFormDataByMHId();
                lnlAddDetails.Visible = false;
                grvCosting2.Visible = false;
            }
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
            lblCostingId.Text = grvCosting2.DataKeys[grdrow.RowIndex]["Mat_Costing2_Id"].ToString();
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
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("Costing2.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
        //MSC_8300001775 Added by NR Start
        if (Save())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
        //MSC_8300001775 Added by NR end
        //MSC_8300001775 Commented by NR Start
        //Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
        //Response.Redirect("MaterialMaster.aspx");
        //MSC_8300001775 Commented by NR End
    }


    #endregion

    #region Methods

    private void ClearCostingData()
    {
        try
        {
            lblCostingId.Text = "0";
            txtPlannedPrice.Text = "";
            txtPlannedPriceDate.Text = "";
            txtPlannedPrice2.Text = "";
            txtPlannedPriceDate2.Text = "";
            txtPlannedPrice3.Text = "";
            txtPlannedPriceDate3.Text = "";
            //ClearSelectedValue(ddlPlant);
            PopuplateDropDownList();
        }
        catch (Exception ex)
        {
            _log.Error("ClearCostingData", ex);
            //throw ex;
        }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C2','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
        //CTRL_SUB_SDT18112019 Commented by NR

        //CTRL_SUB_SDT18112019 Added by NR
        if (lblModuleId.Text == "162")
        {

            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','C2','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");

        }
        else
        {
            helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','C2','" + lblCostingId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
        //CTRL_SUB_SDT18112019 Added by NR
    }



    #region Get

    private void FillCostingDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjCostingAccess.GetCostingData2(Convert.ToInt32(lblMasterHeaderId.Text));

            grvCosting2.DataSource = ds;
            grvCosting2.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillCostingDataGrid", ex);
        }
    }

    private void FillFormDataByMHId()
    {
        DataSet ds; 
        try
        {
        ds = ObjCostingAccess.GetCostingData2(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblCostingId.Text = ds.Tables[0].Rows[0]["Mat_Costing2_Id"].ToString();
        }
        FillCostingData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        //
    }

    private void FillCostingData()
    {
        try
        {
            Costing2 ObjCost = GetCostingData();
            if (ObjCost.Mat_Costing2_Id > 0)
            {
                lblCostingId.Text = ObjCost.Mat_Costing2_Id.ToString();
                PopuplateDropDownList();

                ddlPlant.SelectedValue = ObjCost.Plant_Id;
                txtPlannedPrice.Text = ObjCost.Planned_Price1;
                txtPlannedPriceDate.Text = ObjCost.Planned_Price_Date1;
                txtPlannedPrice2.Text = ObjCost.Planned_Price2;
                txtPlannedPriceDate2.Text = ObjCost.Planned_Price_Date2;
                txtPlannedPrice3.Text = ObjCost.Planned_Price3;
                txtPlannedPriceDate3.Text = ObjCost.Planned_Price_Date3;
            }
            else
            {
                lblCostingId.Text = "0";

                PopuplateDropDownList();
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                //BindPlantWiseDropDown();
            }

            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillCostingData", ex);
        }
    }

    private Costing2 GetCostingData()
    {
        return ObjCostingAccess.GetCosting2(Convert.ToInt32(lblCostingId.Text));
    }

    #endregion

    #region Save

    private bool Save()
    {
      
        bool flg = false;
        try
        {
            Costing2 ObjCost = GetControlsValue();
            objSavedCosting2 = GetCostingData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedCosting2.Mat_Costing2_Id > 0)
                {
                    CheckIfChanges(ObjCost, objSavedCosting2);
                }
            }

            if (ObjCost.Plant_Id != null)
            {
                if (ObjCostingAccess.Save(ObjCost) > 0)
                {
                    //MSC_8300001775
                    if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                    {
                        WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "After Save 3 CheckIfChangesLog() Start");
                        CheckIfChangesLog(ObjCost, objSavedCosting2);
                        WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "After Save 4 CheckIfChangesLog() end");
                    }
                    //MSC_8300001775

                    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "After Save 1 ClearCostingData()");
                    ClearCostingData();
                    //FillCostingDataGrid();
                    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "After Save 2 FillFormDataByMHId()");
                    FillFormDataByMHId();
                    flg = true;
                    ////MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    //{
                    //    CheckIfChangesLog(ObjCost, objSavedCosting2);
                    //}
                    ////MSC_8300001775

                    ////MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    //{
                    //    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "After Save 3 CheckIfChangesLog() Start");
                    //    CheckIfChangesLog(ObjCost, objSavedCosting2);
                    //    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "After Save 4 CheckIfChangesLog() end");
                    //}
                    ////MSC_8300001775
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

    private Costing2 GetControlsValue()
    {
        Costing2 ObjCost = new Costing2();
        Utility objUtil = new Utility();

        try
        {
            ObjCost.Mat_Costing2_Id = Convert.ToInt32(lblCostingId.Text);
            ObjCost.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjCost.Plant_Id = ddlPlant.SelectedValue; //GetSelectedCheckedValue(ddlPlant);
            ObjCost.Planned_Price1 = txtPlannedPrice.Text;
            ObjCost.Planned_Price_Date1 = objUtil.GetYYYYMMDD(txtPlannedPriceDate.Text);
            ObjCost.Planned_Price2 = txtPlannedPrice2.Text;
            ObjCost.Planned_Price_Date2 = objUtil.GetYYYYMMDD(txtPlannedPriceDate2.Text);
            ObjCost.Planned_Price3 = txtPlannedPrice3.Text;
            ObjCost.Planned_Price_Date3 = objUtil.GetYYYYMMDD(txtPlannedPriceDate3.Text);
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
        SectionConfiguration.Costing2 obj = new SectionConfiguration.Costing2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Costing2 NewCosting2Data, Costing2 oldCosting2Data)
    {
        try
        {
            if (NewCosting2Data.Mat_Costing2_Id > 0 && oldCosting2Data.Mat_Costing2_Id > 0)
            {
                if (NewCosting2Data.Plant_Id != oldCosting2Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldCosting2Data.Plant_Id + "</td><td>" + NewCosting2Data.Plant_Id + "</td></tr>";
                if (NewCosting2Data.Planned_Price1 != oldCosting2Data.Planned_Price1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Price1</td> <td>" + oldCosting2Data.Planned_Price1 + "</td><td>" + NewCosting2Data.Planned_Price1 + "</td></tr>";
                if (NewCosting2Data.Planned_Price_Date1 != oldCosting2Data.Planned_Price_Date1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Price Date1</td> <td>" + oldCosting2Data.Planned_Price_Date1 + "</td><td>" + NewCosting2Data.Planned_Price_Date1 + "</td></tr>";
                if (NewCosting2Data.Planned_Price2 != oldCosting2Data.Planned_Price2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Price2</td> <td>" + oldCosting2Data.Planned_Price2 + "</td><td>" + NewCosting2Data.Planned_Price2 + "</td></tr>";
                if (NewCosting2Data.Planned_Price_Date2 != oldCosting2Data.Planned_Price_Date2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Price Date2</td> <td>" + oldCosting2Data.Planned_Price_Date2 + "</td><td>" + NewCosting2Data.Planned_Price_Date2 + "</td></tr>";
                if (NewCosting2Data.Planned_Price3 != oldCosting2Data.Planned_Price3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Price3</td> <td>" + oldCosting2Data.Planned_Price3 + "</td><td>" + NewCosting2Data.Planned_Price3 + "</td></tr>";
                if (NewCosting2Data.Planned_Price_Date3 != oldCosting2Data.Planned_Price_Date3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Price Date3</td> <td>" + oldCosting2Data.Planned_Price_Date3 + "</td><td>" + NewCosting2Data.Planned_Price_Date3 + "</td></tr>";
            }

            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";

        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChanges", ex);
        }

    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="NewCosting2Data"></param>
    /// <param name="oldCosting2Data"></param>
    private void CheckIfChangesLog(Costing2 NewCosting2Data, Costing2 oldCosting2Data)
    {
        Utility objUtil = new Utility();
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewCosting2Data.Mat_Costing2_Id >= 0 && oldCosting2Data.Mat_Costing2_Id >= 0)
            {
                if (NewCosting2Data.Planned_Price1 != oldCosting2Data.Planned_Price1)
                {
                    _items.Add(new SMChange { colFieldName = 38, colOldVal = oldCosting2Data.Planned_Price1, colNewVal = NewCosting2Data.Planned_Price1 });
                }
                if (NewCosting2Data.Planned_Price2 != oldCosting2Data.Planned_Price2)
                {
                    _items.Add(new SMChange { colFieldName = 40, colOldVal = oldCosting2Data.Planned_Price2, colNewVal = NewCosting2Data.Planned_Price2 });
                }
                if (NewCosting2Data.Planned_Price3 != oldCosting2Data.Planned_Price3)
                {
                    _items.Add(new SMChange { colFieldName = 42, colOldVal = oldCosting2Data.Planned_Price3, colNewVal = NewCosting2Data.Planned_Price3 });
                }

                //if (NewCosting2Data.Planned_Price_Date1 != oldCosting2Data.Planned_Price_Date1)
                if ((objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date1) != oldCosting2Data.Planned_Price_Date1)
                 && (objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date1) != "01/01/1900")
                 && (NewCosting2Data.Planned_Price_Date1 != "1900-01-01")
                     && (oldCosting2Data.Planned_Price_Date1 != "01/01/1900") && (oldCosting2Data.Planned_Price_Date1 != "1900-01-01")
                     )
                {
                    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "39");
                    _items.Add(new SMChange { colFieldName = 39, colOldVal = oldCosting2Data.Planned_Price_Date1, colNewVal = objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date1) });
                }
                //if (NewCosting2Data.Planned_Price_Date2 != oldCosting2Data.Planned_Price_Date2)
                if ((objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date2) != oldCosting2Data.Planned_Price_Date2)
                 && (objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date2) != "01/01/1900")
                 && (NewCosting2Data.Planned_Price_Date2 != "1900-01-01")
                     && (oldCosting2Data.Planned_Price_Date2 != "01/01/1900") && (oldCosting2Data.Planned_Price_Date2 != "1900-01-01")
                     )
                {
                    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "41");
                    _items.Add(new SMChange { colFieldName = 41, colOldVal = oldCosting2Data.Planned_Price_Date2, colNewVal = objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date2) });
                }
                //if (NewCosting2Data.Planned_Price_Date3 != oldCosting2Data.Planned_Price_Date3)
                if ((objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price3) != oldCosting2Data.Planned_Price3)
                 && (objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price3) != "01/01/1900")
                 && (NewCosting2Data.Planned_Price3 != "1900-01-01")
                     && (oldCosting2Data.Planned_Price3 != "01/01/1900") && (oldCosting2Data.Planned_Price3 != "1900-01-01")
                     )
                {
                    WriteMatChangeLog("MatChangeLogCost2" + sdate + ".txt", "43");
                    _items.Add(new SMChange { colFieldName = 43, colOldVal = oldCosting2Data.Planned_Price_Date3, colNewVal = objUtil.GetDDMMYYYYNew(NewCosting2Data.Planned_Price_Date3) });
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
                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("6", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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