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
public partial class Transaction_Accounting1 : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    AccountingAccess ObjAccountingAccess = new AccountingAccess();
    Accounting1 objSavedAcc1 = new Accounting1();
    //LLM_DPT_SDT30072019 Commented By Nitin R
    //List<string> DepotPlants = new List<string> { "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87" };
    //LLM_DPT_SDT30072019 Commented By Nitin R
    //LLM_DPT_SDT30072019 Added By Nitin R
    List<string> DepotPlants = new List<string>();
    //LLM_DPT_SDT30072019 Added By Nitin R

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
                            //MSC_8300001775 Start
                            //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                            if ((MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId)) && !btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = true;
                            }
                            //MSC_8300001775 End 
                        }
                        //LLM_DPT_SDT30072019 Added By Nitin R
                        DPTPlantListMet();
                        //LLM_DPT_EDT30072019 Added By Nitin R

                        ClearData();
                        ConfigureControl();
                        //FillDataGrid();

                        //To manage the Creation Single request
                        FillFormDataByMHId();
                        lnlAddDetails.Visible = false;
                        grvData.Visible = false;
                        //MSC_8300001775 Start
                        //if (HelperAccess.ReqType == "SMC" )
                        if (MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId))
                        {
                            ConfigureControlForSChange();
                        }
                        //MSC_8300001775 End
                    }
                    else
                    {
                        Response.Redirect("MaterialMaster.aspx", false);
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
            ddlValuationCategory.Enabled = true;
            ddlValuationClass.Enabled = true;
            txtPriceUnit.Enabled = true;
            txtPriceUnit.Text = "1";
            ddlPriceControlIndicator.Enabled = true;
            txtPriceUnit.CssClass = "txtbox";

            txtMovingAvgPrice.Enabled = true;
            txtMovingAvgPrice.CssClass = "txtbox";
            txtStandardPrice.Enabled = true;
            txtStandardPrice.CssClass = "txtbox";
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
            lblAccountingId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Accounting1_Id"].ToString();
            FillAccountingData();
        }
        catch (Exception ex)
        {
            _log.Error("lnkView_Click", ex);
            //throw ex;
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkView = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkView.Parent.Parent;
            string Mat_Accounting1_Id = grvData.DataKeys[grdrow.RowIndex]["Mat_Accounting1_Id"].ToString();
            MasterAccess.DeleteDataBySectionId(lblMasterHeaderId.Text, lblSectionId.Text, Mat_Accounting1_Id);

            FillDataGrid();
        }
        catch (Exception ex)
        {
            _log.Error("lnkDelete_Click", ex);
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveAccounting1())
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
            if (SaveAccounting1())
            {
                //lblMsg.Text = Messages.GetMessage(1);
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;

                //string pageURL = btnNext.CommandArgument.ToString();
                //Response.Redirect(pageURL);
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("Accounting1.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveAccounting1())
            {
                ////8400000410 comment Start
                //if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("Costing2.aspx", false);
                //}
                //else
                //{
                //    string pageURL = btnNext.CommandArgument.ToString();
                //    Response.Redirect(pageURL);
                //}
                ////8400000410 comment End

                //8400000410 comment Start
                
                    string pageURL = btnNext.CommandArgument.ToString();
                    Response.Redirect(pageURL);
                 
                //8400000410 comment End
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlValuationCategory, "pr_Get_Valuation_Category_By_Plant_Id 'M','ddlValuationCategory','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
    }

    protected void ddlValuationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindByValuationType();
        }
        catch (Exception ex)
        { _log.Error("ddlValuationType_SelectedIndexChanged", ex); }
    }

    protected void BindByValuationType()
    {
        try
        {
            if (lblModuleId.Text == "162")
            {
                if (ddlValuationType.SelectedValue == "DOMESTIC")
                    ddlValuationClass.SelectedValue = "1010";
                else if (ddlValuationType.SelectedValue == "IMPORT")
                    ddlValuationClass.SelectedValue = "1030";
                else if (ddlValuationType.SelectedValue == "")
                    ddlValuationClass.SelectedValue = "";
            }
        }
        catch (Exception ex)
        { _log.Error("BindByValuationType", ex); }
    }


    #endregion

    #region Method

    /// <summary>
    /// LLM_DPT_SDT30072019 
    /// Update list
    /// </summary>
    private void DPTPlantListMet()
    {
        QualityAccess ObjQualityAccess = new QualityAccess();
        try
        {
            DataSet ds;
            ds = ObjQualityAccess.GetDPTPlantList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string sValNew = Convert.ToString(row["Plant_Id"]);
                    DepotPlants.Add(sValNew);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("DPTPlantListMet", ex);
        }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR
            bindValuationType(ddlValuationType);
            //helperAccess.PopuplateDropDownList(ddlValuationType, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationType','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlValuationCategory, "pr_Get_Valuation_Category_By_Plant_Id 'M','ddlValuationCategory','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


            string AccountCat = MaterialHelper.GetAccountCategoryByModuleId(lblModuleId.Text);
            helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass','" + lblSectionId.Text + "','" + AccountCat + "'", "LookUp_Desc", "LookUp_Code", "");

            //helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlPriceControlIndicator, "pr_GetDropDownListByControlNameModuleType 'M','ddlPriceControlIndicator'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    public void bindValuationType(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new ListItem("---Select---", ""));
            ddl.Items.Insert(1, new ListItem("DOMESTIC", "DOMESTIC"));
            ddl.Items.Insert(1, new ListItem("IMPORT", "IMPORT"));
        }
        catch (Exception ex)
        { _log.Error("bindValuationType", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblAccountingId.Text = "0";
            txtMovingAvgPrice.Text = "";
            txtStandardPrice.Text = "";
            txtPriceUnit.Text = "";
            PopuplateDropDownList();

            //FillAccountingData();
        }
        catch (Exception ex)
        {
            _log.Error("ClearData", ex);
            //throw ex;
        }
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjAccountingAccess.GetAccountingData1(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
            //throw ex;
        }
    }

    private bool SaveAccounting1()
    {
        bool flg = false;
        try
        {
            Accounting1 ObjAcc = GetControlsValue();
            objSavedAcc1 = GetAccountingData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedAcc1.Mat_Accounting1_Id > 0)
                {
                    CheckIfChanges(ObjAcc, objSavedAcc1);
                }
            }

            if (ObjAcc.Plant_Id != null)
            {
                if (CheckValidValuationClass(ObjAcc))
                {
                    if (ObjAccountingAccess.Save(ObjAcc) > 0)
                    {
                        //MSC_8300001775
                        //if (HelperAccess.ReqType == "SMC")
                        if (MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId))
                        {
                            CheckIfChangesLog(ObjAcc, objSavedAcc1);
                        }
                        //MSC_8300001775

                        //FillDataGrid();
                        ClearData();
                        FillFormDataByMHId();
                        flg = true;
                        ////MSC_8300001775
                        //if (HelperAccess.ReqType == "SMC")
                        //{
                        //    CheckIfChangesLog(ObjAcc, objSavedAcc1);
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
                    lblMsg.Text = "For Division 07 and 08 valuation class cannot be 8010 and 8020 respectively.";
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
            _log.Error("SaveAccounting1", ex);
            //throw ex;
        }
        return flg;
    }

    private bool CheckValidValuationClass(Accounting1 ObjAcc)
    {
        bool flag = true;
        try
        {
            if (lblModuleId.Text == "144")
            {
                if ((Session[StaticKeys.SelectedDivision].ToString() == "7" && ObjAcc.Valuation_Class == "8010") || (Session[StaticKeys.SelectedDivision].ToString() == "8" && ObjAcc.Valuation_Class == "8020"))
                    flag = false;
            }
        }
        catch (Exception ex)
        { _log.Error("CheckValidValuationClass", ex); }
        return flag;
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
            ds = ObjAccountingAccess.GetAccountingData1(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAccountingId.Text = ds.Tables[0].Rows[0]["Mat_Accounting1_Id"].ToString();
            }
            FillAccountingData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private Accounting1 GetAccountingData()
    {
        return ObjAccountingAccess.GetAccounting1(Convert.ToInt32(lblAccountingId.Text));
    }

    private Accounting1 GetControlsValue()
    {
        Accounting1 ObjAcc = new Accounting1();
        Utility objUtil = new Utility();

        try
        {
            ObjAcc.Mat_Accounting1_Id = Convert.ToInt32(lblAccountingId.Text);
            ObjAcc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjAcc.Plant_Id = ddlPlant.SelectedValue;
            ObjAcc.Valuation_Type = ddlValuationType.SelectedValue;
            ObjAcc.Valuation_Category = ddlValuationCategory.SelectedValue;
            ObjAcc.Price_Ctrl_Indicator = ddlPriceControlIndicator.SelectedValue;
            ObjAcc.Moving_Avg_Price = txtMovingAvgPrice.Text;
            ObjAcc.Standard_Price = txtStandardPrice.Text;
            ObjAcc.Price_Unit = txtPriceUnit.Text;
            ObjAcc.Valuation_Class = ddlValuationClass.SelectedValue;
            ObjAcc.IsActive = 1;
            ObjAcc.UserId = lblUserId.Text;
            ObjAcc.TodayDate = objUtil.GetDate();
            ObjAcc.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
            //throw ex;
        }
        return ObjAcc;
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlValuationCategory, "pr_Get_Valuation_Category_By_Plant_Id 'M','ddlValuationCategory','" + lblSectionId.Text + "','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    private void FillAccountingData()
    {
        try
        {
            Accounting1 ObjAcc = GetAccountingData();
            if (ObjAcc.Mat_Accounting1_Id > 0)
            {
                lblAccountingId.Text = ObjAcc.Mat_Accounting1_Id.ToString();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = ObjAcc.Plant_Id;
                BindPlantWiseDropDown();


                ddlValuationType.SelectedValue = ObjAcc.Valuation_Type;
                ddlValuationCategory.SelectedValue = ObjAcc.Valuation_Category;
                ddlPriceControlIndicator.SelectedValue = ObjAcc.Price_Ctrl_Indicator;
                txtMovingAvgPrice.Text = ObjAcc.Moving_Avg_Price;
                txtStandardPrice.Text = ObjAcc.Standard_Price;
                txtPriceUnit.Text = ObjAcc.Price_Unit;
                ddlValuationClass.SelectedValue = ObjAcc.Valuation_Class;
            }
            else
            {
                lblAccountingId.Text = "0";

                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A1','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();
                BindPlantWiseDropDown();
                //if (lblModuleId.Text == "138" || lblModuleId.Text == "147")
                //{

                txtPriceUnit.Text = "1";

                ddlValuationClass.SelectedValue = MaterialHelper.GetDefaultValuationClassByModuleId(lblModuleId.Text);

                if (lblModuleId.Text == "162")
                {
                    ddlValuationCategory.SelectedValue = "H";
                    BindByValuationType();
                }
                if (lblModuleId.Text == "164")
                {
                    ddlValuationClass.SelectedValue = "2010";
                }
                //Promotion code start
                //if (lblModuleId.Text == "145" || lblModuleId.Text == "139" || lblModuleId.Text == "171" || lblModuleId.Text == "144")
                if (lblModuleId.Text == "145" || lblModuleId.Text == "139" || lblModuleId.Text == "171" || lblModuleId.Text == "144" || lblModuleId.Text == "195")
                //Promotion code End
                {
                    if (lblModuleId.Text == "145")
                        ddlValuationClass.SelectedValue = "3010";
                    if (lblModuleId.Text == "139")
                        ddlValuationClass.SelectedValue = "8020";
                    //Promotion code start
                    //if (lblModuleId.Text == "171")
                    if (lblModuleId.Text == "171" || lblModuleId.Text == "195")
                        //Promotion code End
                        ddlValuationClass.SelectedValue = "7020";
                    if (lblModuleId.Text == "144")
                    {
                        if (Session[StaticKeys.SelectedDivision].ToString() == "8" || Session[StaticKeys.SelectedDivision].ToString() == "30")
                            ddlValuationClass.SelectedValue = "8010";
                        else if (Session[StaticKeys.SelectedDivision].ToString() == "7")
                            ddlValuationClass.SelectedValue = "8020";
                        else
                            ddlValuationClass.SelectedValue = "";
                    }
                    if (DepotPlants.Contains(ddlPlant.SelectedValue.ToString()))
                        ddlPriceControlIndicator.SelectedValue = "V";
                    else
                        ddlPriceControlIndicator.SelectedValue = "S";
                }
                else
                    ddlPriceControlIndicator.SelectedValue = "V";
            }

            ddlPriceControlIndicator.Enabled = false;
            if (lblModuleId.Text == "138" || lblModuleId.Text == "147" || lblModuleId.Text == "162" || lblModuleId.Text == "164" || lblModuleId.Text == "170")
                ddlValuationClass.Enabled = false;
            if (lblModuleId.Text == "162")
                ddlValuationCategory.Enabled = false;
            //Promotion code start
            //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
            if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
            //Promotion code End
            {
                ddlValuationClass.Enabled = true;
                ddlPriceControlIndicator.Enabled = true;
            }


            ddlPlant.Enabled = false;
            //Srinidhi
            txtPriceUnit.Enabled = false;

            if (lblModuleId.Text == "138" || lblModuleId.Text == "147")
            {
                if (ddlPlant.SelectedValue == "5")
                {
                    txtMovingAvgPrice.Enabled = true;
                    txtMovingAvgPrice.CssClass = "textbox";
                }
            }

        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("BindPlantWiseDropDown", ex);
        }
    }

    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.Accounting1 obj = new SectionConfiguration.Accounting1();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Accounting1 NewAccData, Accounting1 oldAccData)
    {
        try
        {
            if (NewAccData.Mat_Accounting1_Id > 0 && oldAccData.Mat_Accounting1_Id > 0)
            {
                if (NewAccData.Plant_Id != oldAccData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldAccData.Plant_Id + "</td><td>" + NewAccData.Plant_Id + "</td></tr>";
                if (NewAccData.Valuation_Category != oldAccData.Valuation_Category)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Valuation Categoryr</td> <td>" + oldAccData.Valuation_Category + "</td><td>" + NewAccData.Valuation_Category + "</td></tr>";
                if (NewAccData.Valuation_Class != oldAccData.Valuation_Class)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Valuation Class</td> <td>" + oldAccData.Valuation_Class + "</td><td>" + NewAccData.Valuation_Class + "</td></tr>";
                if (NewAccData.Valuation_Type != oldAccData.Valuation_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Valuation Type</td> <td>" + oldAccData.Valuation_Type + "</td><td>" + NewAccData.Valuation_Type + "</td></tr>";
                if (NewAccData.Price_Ctrl_Indicator != oldAccData.Price_Ctrl_Indicator)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Price Control Indicator</td> <td>" + oldAccData.Price_Ctrl_Indicator + "</td><td>" + NewAccData.Price_Ctrl_Indicator + "</td></tr>";
                if (NewAccData.Moving_Avg_Price != oldAccData.Moving_Avg_Price)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Moving Avg Price</td> <td>" + oldAccData.Moving_Avg_Price + "</td><td>" + NewAccData.Moving_Avg_Price + "</td></tr>";
                if (NewAccData.Standard_Price != oldAccData.Standard_Price)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Standard Price</td> <td>" + oldAccData.Standard_Price + "</td><td>" + NewAccData.Standard_Price + "</td></tr>";
                if (NewAccData.Price_Unit != oldAccData.Price_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Price Unit</td> <td>" + oldAccData.Price_Unit + "</td><td>" + NewAccData.Price_Unit + "</td></tr>";
            }

            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";

        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChanges", ex);
            //throw ex;
        }

    }

    /// <summary>
    /// //MSC_8300001775
    /// </summary>
    /// <param name="NewAccData"></param>
    /// <param name="oldAccData"></param>
    private void CheckIfChangesLog(Accounting1 NewAccData, Accounting1 oldAccData)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewAccData.Mat_Accounting1_Id >= 0 && oldAccData.Mat_Accounting1_Id >= 0)
            {
                if (NewAccData.Valuation_Category != oldAccData.Valuation_Category)
                    _items.Add(new SMChange { colFieldName = 0, colOldVal = oldAccData.Valuation_Category, colNewVal = NewAccData.Valuation_Category });
                if (NewAccData.Valuation_Class != oldAccData.Valuation_Class)
                    _items.Add(new SMChange { colFieldName = 5, colOldVal = oldAccData.Valuation_Class, colNewVal = NewAccData.Valuation_Class });
                if (NewAccData.Valuation_Type != oldAccData.Valuation_Type)
                    _items.Add(new SMChange { colFieldName = 1168, colOldVal = oldAccData.Valuation_Type, colNewVal = NewAccData.Valuation_Type });
                if (NewAccData.Price_Ctrl_Indicator != oldAccData.Price_Ctrl_Indicator)
                    _items.Add(new SMChange { colFieldName = 1, colOldVal = oldAccData.Price_Ctrl_Indicator, colNewVal = NewAccData.Price_Ctrl_Indicator });
                if (NewAccData.Moving_Avg_Price != oldAccData.Moving_Avg_Price)
                    _items.Add(new SMChange { colFieldName = 2, colOldVal = oldAccData.Moving_Avg_Price, colNewVal = NewAccData.Moving_Avg_Price });
                if (NewAccData.Standard_Price != oldAccData.Standard_Price)
                    _items.Add(new SMChange { colFieldName = 3, colOldVal = oldAccData.Standard_Price, colNewVal = NewAccData.Standard_Price });
                if (NewAccData.Price_Unit != oldAccData.Price_Unit)
                    _items.Add(new SMChange { colFieldName = 4, colOldVal = oldAccData.Price_Unit, colNewVal = NewAccData.Price_Unit });
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog", ex);
            //throw ex;
        }
        try
        {
            if (_items.Count > 0)
            {
                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("1", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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
            //throw ex;
        }

    }

    #endregion



}