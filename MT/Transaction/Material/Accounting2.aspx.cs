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
public partial class Transaction_Accounting2 : BasePage
{
    AccountingAccess ObjAccountingAccess = new AccountingAccess();
    HelperAccess helperAccess = new HelperAccess();
    Accounting2 objSavedAcc2 = new Accounting2();

    #region Page Events
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                        //FillDataGrid();
                        ClearData();
                        ConfigureControl();

                        //To manage the Creation Single request
                        FillFormDataByMHId();
                        lnlAddDetails.Visible = false;
                        grvData.Visible = false;
                        //MSC_8300001775 Start
                        //if (HelperAccess.ReqType == "SMC")
                        if (MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId))
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
            labletxtTaxPrice1.Visible = false;
            reqtxtTaxPrice1.Visible = false;
            labletxtTaxPrice2.Visible = false;
            reqtxtTaxPrice2.Visible = false;
            labletxtTaxPrice3.Visible = false;
            reqtxtTaxPrice3.Visible = false;
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
            lblAccountingId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Accounting2_Id"].ToString();
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
            string Mat_Accounting2_Id = grvData.DataKeys[grdrow.RowIndex]["Mat_Accounting2_Id"].ToString();
            MasterAccess.DeleteDataBySectionId(lblMasterHeaderId.Text, lblSectionId.Text, Mat_Accounting2_Id);
        }
        catch (Exception ex)
        {
            _log.Error("lnkDelete_Click", ex);
            //throw ex;
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveAccounting2())
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
            if (SaveAccounting2())
            {
                //lblMsg.Text = Messages.GetMessage(1);
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;

                //string pageURL = btnNext.CommandArgument.ToString();
                //Response.Redirect(pageURL);
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("Accounting2.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveAccounting2())
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

    }

    #endregion

    #region Method

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR
            helperAccess.PopuplateDropDownList(ddlPoolNumberLifo, "pr_GetDropDownListByControlNameModuleType 'M','ddlPoolNumberLifo'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblAccountingId.Text = "0";
            chkLifoFifo.Checked = false;

            txtCommercialPrice1.Text = "";
            txtCommercialPrice2.Text = "";
            txtCommercialPrice3.Text = "";
            txtTaxPrice1.Text = "";
            txtTaxPrice2.Text = "";
            txtTaxPrice3.Text = "";
            PopuplateDropDownList();
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
            ds = ObjAccountingAccess.GetAccountingData2(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
            //throw ex;
        }
    }

    private bool SaveAccounting2()
    {
        bool flg = true;
        try
        {
            Accounting2 ObjAcc = GetControlsValue();
            objSavedAcc2 = GetAccountingData();

            if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
            {
                if (objSavedAcc2.Mat_Accounting2_Id > 0)
                {
                    CheckIfChanges(ObjAcc, objSavedAcc2);
                }
            }

            if (ObjAcc.Plant_Id != null)
            {
                if (ObjAccountingAccess.Save(ObjAcc) > 0)
                {
                    //MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    if (MaterialMasterAccess.IsUserHasSChangeReq(MasterHeaderId))
                    {
                        CheckIfChangesLog(ObjAcc, objSavedAcc2);
                    }
                    //MSC_8300001775

                    //FillDataGrid();
                    ClearData();
                    FillFormDataByMHId();
                    flg = true;
                    ////MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    //{
                    //    CheckIfChangesLog(ObjAcc, objSavedAcc2);
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
            _log.Error("SaveAccounting2", ex);
            //throw ex;
        }
        return flg;
    }

    private Accounting2 GetAccountingData()
    {
        return ObjAccountingAccess.GetAccounting2(Convert.ToInt32(lblAccountingId.Text));
    }

    private Accounting2 GetControlsValue()
    {
        Accounting2 ObjAcc = new Accounting2();
        Utility objUtil = new Utility();

        try
        {
            ObjAcc.Mat_Accounting2_Id = Convert.ToInt32(lblAccountingId.Text);
            ObjAcc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjAcc.Plant_Id = ddlPlant.SelectedValue; //GetSelectedCheckedValue(ddlPlant);

            ObjAcc.Commercial_Price1 = txtCommercialPrice1.Text;
            ObjAcc.Commercial_Price2 = txtCommercialPrice2.Text;
            ObjAcc.Commercial_Price3 = txtCommercialPrice3.Text;

            ObjAcc.Tax_Price1 = txtTaxPrice1.Text;
            ObjAcc.Tax_Price2 = txtTaxPrice2.Text;
            ObjAcc.Tax_Price3 = txtTaxPrice3.Text;

            ObjAcc.Relevant = chkLifoFifo.Checked == true ? 1 : 0;
            ObjAcc.Pool_No_LIFO_Valuation = ddlPoolNumberLifo.SelectedValue;
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

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
        ds = ObjAccountingAccess.GetAccountingData2(Convert.ToInt32(lblMasterHeaderId.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblAccountingId.Text = ds.Tables[0].Rows[0]["Mat_Accounting2_Id"].ToString();
        }
        FillAccountingData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void FillAccountingData()
    {
        try
        {
            Accounting2 ObjAcc = GetAccountingData();
            if (ObjAcc.Mat_Accounting2_Id > 0)
            {
                lblAccountingId.Text = ObjAcc.Mat_Accounting2_Id.ToString();
                PopuplateDropDownList();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjAcc.Plant_Id;
                txtCommercialPrice1.Text = ObjAcc.Commercial_Price1;
                txtCommercialPrice2.Text = ObjAcc.Commercial_Price2;
                txtCommercialPrice3.Text = ObjAcc.Commercial_Price3;

                txtTaxPrice1.Text = ObjAcc.Tax_Price1;
                txtTaxPrice2.Text = ObjAcc.Tax_Price2;
                txtTaxPrice3.Text = ObjAcc.Tax_Price3;

                chkLifoFifo.Checked = ObjAcc.Relevant == 1 ? true : false;
                ddlPoolNumberLifo.SelectedValue = ObjAcc.Pool_No_LIFO_Valuation;
            }
            else
            {
                lblAccountingId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {

                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','A2','" + lblAccountingId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

            }

            ddlPlant.Enabled = false;
        }
        catch (Exception ex)
        {
            _log.Error("FillAccountingData", ex);
            //throw ex;
        }
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Accounting2 obj = new SectionConfiguration.Accounting2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(Accounting2 NewAcc2Data, Accounting2 oldAcc2Data)
    {
        try
        {
            if (NewAcc2Data.Mat_Accounting2_Id > 0 && oldAcc2Data.Mat_Accounting2_Id > 0)
            {
                if (NewAcc2Data.Plant_Id != oldAcc2Data.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant Id</td> <td>" + oldAcc2Data.Plant_Id + "</td><td>" + NewAcc2Data.Plant_Id + "</td></tr>";
                if (NewAcc2Data.Tax_Price1 != oldAcc2Data.Tax_Price1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax_Price1</td> <td>" + oldAcc2Data.Tax_Price1 + "</td><td>" + NewAcc2Data.Tax_Price1 + "</td></tr>";
                if (NewAcc2Data.Tax_Price2 != oldAcc2Data.Tax_Price2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax_Price2</td> <td>" + oldAcc2Data.Tax_Price2 + "</td><td>" + NewAcc2Data.Tax_Price2 + "</td></tr>";
                if (NewAcc2Data.Tax_Price3 != oldAcc2Data.Tax_Price3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Tax_Price3</td> <td>" + oldAcc2Data.Tax_Price3 + "</td><td>" + NewAcc2Data.Tax_Price3 + "</td></tr>";
                if (NewAcc2Data.Commercial_Price1 != oldAcc2Data.Commercial_Price1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Commercial Price1</td> <td>" + oldAcc2Data.Commercial_Price1 + "</td><td>" + NewAcc2Data.Commercial_Price1 + "</td></tr>";
                if (NewAcc2Data.Commercial_Price2 != oldAcc2Data.Commercial_Price2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Commercial Price2</td> <td>" + oldAcc2Data.Commercial_Price2 + "</td><td>" + NewAcc2Data.Commercial_Price2 + "</td></tr>";
                if (NewAcc2Data.Commercial_Price3 != oldAcc2Data.Commercial_Price3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Commercial Price3</td> <td>" + oldAcc2Data.Commercial_Price3 + "</td><td>" + NewAcc2Data.Commercial_Price3 + "</td></tr>";
                if (NewAcc2Data.Relevant != (oldAcc2Data.Relevant))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Relevant</td> <td>" + oldAcc2Data.Relevant + "</td><td>" + NewAcc2Data.Relevant + "</td></tr>";
                //if (NewAcc2Data.Pool_No_LIFO_Valuation != oldAcc2Data.Pool_No_LIFO_Valuation)
                //    Session[StaticKeys.ApprovalNote] += "<tr><td>Pool No LIFO Valuation</td> <td>" + oldAcc2Data.Pool_No_LIFO_Valuation + "</td><td>" + NewAcc2Data.Pool_No_LIFO_Valuation + "</td></tr>";
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
    private void CheckIfChangesLog(Accounting2 NewAcc2Data, Accounting2 oldAcc2Data)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewAcc2Data.Mat_Accounting2_Id >= 0 && oldAcc2Data.Mat_Accounting2_Id >= 0)
            {
                if (NewAcc2Data.Tax_Price1 != oldAcc2Data.Tax_Price1)
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Tax_Price1</td> <td>" + oldAcc2Data.Tax_Price1 + "</td><td>" + NewAcc2Data.Tax_Price1 + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 1286, colOldVal = oldAcc2Data.Tax_Price1, colNewVal = NewAcc2Data.Tax_Price1 });
                if (NewAcc2Data.Tax_Price2 != oldAcc2Data.Tax_Price2)
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Tax_Price2</td> <td>" + oldAcc2Data.Tax_Price2 + "</td><td>" + NewAcc2Data.Tax_Price2 + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 1287, colOldVal = oldAcc2Data.Tax_Price2, colNewVal = NewAcc2Data.Tax_Price2 });
                if (NewAcc2Data.Tax_Price3 != oldAcc2Data.Tax_Price3)
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Tax_Price3</td> <td>" + oldAcc2Data.Tax_Price3 + "</td><td>" + NewAcc2Data.Tax_Price3 + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 1288, colOldVal = oldAcc2Data.Tax_Price3, colNewVal = NewAcc2Data.Tax_Price3 });
                if (NewAcc2Data.Commercial_Price1 != oldAcc2Data.Commercial_Price1)
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Commercial Price1</td> <td>" + oldAcc2Data.Commercial_Price1 + "</td><td>" + NewAcc2Data.Commercial_Price1 + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 1169, colOldVal = oldAcc2Data.Commercial_Price1, colNewVal = NewAcc2Data.Commercial_Price1 });
                if (NewAcc2Data.Commercial_Price2 != oldAcc2Data.Commercial_Price2)
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Commercial Price2</td> <td>" + oldAcc2Data.Commercial_Price2 + "</td><td>" + NewAcc2Data.Commercial_Price2 + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 1170, colOldVal = oldAcc2Data.Commercial_Price2, colNewVal = NewAcc2Data.Commercial_Price2 });
                if (NewAcc2Data.Commercial_Price3 != oldAcc2Data.Commercial_Price3)
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Commercial Price3</td> <td>" + oldAcc2Data.Commercial_Price3 + "</td><td>" + NewAcc2Data.Commercial_Price3 + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 1171, colOldVal = oldAcc2Data.Commercial_Price3, colNewVal = NewAcc2Data.Commercial_Price3 });
                if (NewAcc2Data.Relevant != (oldAcc2Data.Relevant))
                    //Session[StaticKeys.ApprovalNote] += "<tr><td>Relevant</td> <td>" + oldAcc2Data.Relevant + "</td><td>" + NewAcc2Data.Relevant + "</td></tr>";
                    _items.Add(new SMChange { colFieldName = 6, colOldVal = oldAcc2Data.Relevant.ToString(), colNewVal = NewAcc2Data.Relevant.ToString() });
                //if (NewAcc2Data.Pool_No_LIFO_Valuation != oldAcc2Data.Pool_No_LIFO_Valuation)
                //    //Session[StaticKeys.ApprovalNote] += "<tr><td>Pool No LIFO Valuation</td> <td>" + oldAcc2Data.Pool_No_LIFO_Valuation + "</td><td>" + NewAcc2Data.Pool_No_LIFO_Valuation + "</td></tr>";
                //    _items.Add(new SMChange { colFieldName = 7, colOldVal = oldAcc2Data.Pool_No_LIFO_Valuation, colNewVal = NewAcc2Data.Pool_No_LIFO_Valuation });
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
            //throw ex;
        }

    }
    #endregion


}