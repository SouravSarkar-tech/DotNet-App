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
using System.Data.SqlClient;
using log4net;
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_ForeignTrade : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    ForeignTradeAccess ObjForeignTradeAccess = new ForeignTradeAccess();
    HelperAccess helperAccess = new HelperAccess();
    ForeignTrade objSavedForeign = new ForeignTrade();
    string sdate = "";
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogFT" + sdate + ".txt", "Start" + HelperAccess.ReqType);
        }
        catch (Exception ex)
        {
            _log.Error("Page_Load", ex);
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
                            //if (HelperAccess.ReqType == "SMC" && !btnPrevious.Visible && !btnNext.Visible)
                            if ((MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)) && !btnPrevious.Visible && !btnNext.Visible)
                            {
                                btnSave.Visible = true;
                            }
                            //MSC_8300001775 End 
                            //srinidhi
                            //Promotion code start
                            //if ((lblModuleId.Text == "139") || (lblModuleId.Text == "144") || (lblModuleId.Text == "171"))
                            if ((lblModuleId.Text == "139") || (lblModuleId.Text == "144") || (lblModuleId.Text == "171") || (lblModuleId.Text == "195"))
                            //Promotion code End
                            {
                                trRemarks.Visible = true;
                                trDocs.Visible = true;
                                trDocsText.Visible = true;
                                grdAttachedDocs.Columns[1].Visible = true;
                                file_upload.Visible = true;
                            }
                            else
                            {
                                trRemarks.Visible = false;
                                trDocs.Visible = false;
                                trDocsText.Visible = false;
                            }
                        }
                        else
                        {
                            //Promotion code start
                            //if ((lblModuleId.Text == "139") || (lblModuleId.Text == "144") || (lblModuleId.Text == "171"))
                            if ((lblModuleId.Text == "139") || (lblModuleId.Text == "144") || (lblModuleId.Text == "171") || (lblModuleId.Text == "195"))
                            //Promotion code End
                            {
                                trRemarks.Visible = true;
                                trDocs.Visible = true;
                                grdAttachedDocs.Columns[1].Visible = false;
                                file_upload.Visible = false;
                                trDocsText.Visible = true;
                            }
                            else
                            {
                                trRemarks.Visible = false;
                                trDocs.Visible = false;
                                trDocsText.Visible = false;
                            }

                        }

                    }
                    else
                    {
                        Response.Redirect("MaterialMaster.aspx");
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
            ddlControlCode.Enabled = true;
            ddlChapter_ID.Enabled = false;
            labletxtRemarks.Visible = false;
            reqtxtRemarks.Visible = false;
            reqddlControlCode.Enabled = false;
            lableddlControlCode.Enabled = false;
            reqddlControlCode.Visible = false;
            lableddlControlCode.Visible = false;
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
            lblForeignTradeId.Text = grvData.DataKeys[grdrow.RowIndex]["Mat_Foreign_Trade_Id"].ToString();
            FillForeignTradeData();
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
            if (SaveForeignTrade())
            {
                if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                {
                    Response.Redirect("Purchasing.aspx");
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
            if (SaveForeignTrade())
            {
                //lblMsg.Text = Messages.GetMessage(1);
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("ForeignTrade.aspx");
            }

        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveForeignTrade())
            {//8400000410 comment star
             //if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
             //{
             //    Response.Redirect("MRP1.aspx");
             //}
             //else
             //{
             //    string pageURL = btnNext.CommandArgument.ToString();
             //    Response.Redirect(pageURL);
             //}
             ////8400000410 comment End


                //8400000410 add Start

                string pageURL = btnNext.CommandArgument.ToString();
                Response.Redirect(pageURL);

                //8400000410 add End
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindByCountry();
        }
        catch (Exception ex)
        { _log.Error("ddlCountry_SelectedIndexChanged", ex); }
    }

    protected void ddlControlCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem li in ddlChapter_ID.Items)
            {
                if (li.Value.Trim() == ddlControlCode.SelectedValue.Trim())
                {
                    ddlChapter_ID.SelectedValue = li.Value;
                    break;
                }
            }

            SetFieldsByControlCode();
        }
        catch (Exception ex)
        { _log.Error("ddlControlCode_SelectedIndexChanged", ex); }
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
                        pnlMsg.Visible = false;
                        BindAttachedDocuments(lblMasterHeaderId.Text);
                    }
                    else
                    {
                        objTrans.Rollback();
                        lblMsg.Text = "Error While Deleting File.";
                        pnlMsg.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("grdAttachedDocs_RowCommand", ex);
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

    //protected void ddlGSTReq_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ConfigureGSTControls();

    //}

    #endregion

    #region Method

    protected void SetFieldsByControlCode()
    {
        bool flg = false;
        try
        {
            if (ddlControlCode.SelectedValue != "")
            {
                flg = true;
                ddlMaterial_Type.SelectedValue = "";
            }
            else
            {
                ddlMaterial_Type.SelectedIndex = 0;
            }

            ddlMaterial_Type.Enabled = flg;
            txtOutput_Material_For_ModVat.Enabled = flg;
            chkSubcontractors.Enabled = flg;
        }
        catch (Exception ex)
        { _log.Error("SetFieldsByControlCode", ex); }

    }

    private void BindByCountry()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + ddlCountry.SelectedValue, "Region_Name", "Region_Id");
            string sCountry = "0";
            if (ddlCountry.SelectedValue != "0")
            {
                sCountry = ddlCountry.SelectedValue;
            }
            else
            {
                sCountry = "1";
            }
            helperAccess.PopuplateDropDownList(ddlControlCode, "pr_GetDropDownListByControlNameModuleTypeByCountryID 'M','ddlControlCode','" + lblSectionId.Text + "','" + sCountry + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("BindByCountry", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplatePlantList(ddlPlant, lblMasterHeaderId.Text, "FT", lblForeignTradeId.Text);

            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblModuleId.Text == "162")
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

            helperAccess.PopuplateDropDownList(ddlChapter_ID, "pr_GetDropDownListByControlNameModuleType 'M','ddlChapter_ID'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlMaterial_Type, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterial_Type'", "LookUp_Desc", "LookUp_Code", "0");
            helperAccess.PopuplateDropDownList(ddlNo_of_Goods_Receipts_per_Excise_Invoice, "pr_GetDropDownListByControlNameModuleType 'M','ddlNo_of_Goods_Receipts_per_Excise_Invoice'", "LookUp_Desc", "LookUp_Code", "0");

            helperAccess.PopuplateDropDownList(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
            //BindByCountry();

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void ClearData()
    {
        try
        {
            lblForeignTradeId.Text = "0";
            txtCommodityCode.Text = "";
            //ddlCountry.SelectedValue = "0";
            //ddlRegion.SelectedValue = "0";
            //ddlExport.SelectedValue = "";
            //ddlPreference.SelectedValue = "";
            //ddlExemption.SelectedValue = "";
            //ddlControlCode.SelectedValue = "";
            //ClearSelectedValue(ddlPlant);
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
            ds = ObjForeignTradeAccess.GetForeignTradeData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvData.DataSource = ds;
            grvData.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillDataGrid", ex);
        }
    }

    private bool SaveForeignTrade()
    {
        bool flg = false;
        try
        {
            ForeignTrade ObjForeignTrade = GetControlsValue();
            objSavedForeign = GetForeignTradeData();

            if (ObjForeignTrade.Plant_Id != null)
            {
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                {
                    if (objSavedForeign.Mat_Foreign_Trade_Id > 0)
                    {
                        CheckIfChanges(ObjForeignTrade, objSavedForeign);
                    }
                }
                //if (ObjForeignTradeAccess.Save(ObjForeignTrade) > 0) 
                if ((ObjForeignTradeAccess.Save(ObjForeignTrade) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
                {
                    //MSC_8300001775
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))// (HelperAccess.ReqType == "SMC"))
                    {
                        CheckIfChangesLog(ObjForeignTrade, objSavedForeign);
                    }
                    //MSC_8300001775

                    //FillDataGrid();
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                    ClearData();
                    FillFormDataByMHId();
                    flg = true;
                    ////MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    //{
                    //    CheckIfChangesLog(ObjForeignTrade, objSavedForeign);
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
            _log.Error("SaveForeignTrade", ex);
            throw ex;
        }
        return flg;
    }

    private ForeignTrade GetForeignTradeData()
    {
        return ObjForeignTradeAccess.GetForeignTrade(Convert.ToInt32(lblForeignTradeId.Text));
    }

    private ForeignTrade GetControlsValue()
    {
        ForeignTrade ObjForeignTrade = new ForeignTrade();
        Utility objUtil = new Utility();

        try
        {
            ObjForeignTrade.Mat_Foreign_Trade_Id = Convert.ToInt32(lblForeignTradeId.Text);
            ObjForeignTrade.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjForeignTrade.Plant_Id = ddlPlant.SelectedValue;// GetSelectedCheckedValue(ddlPlant);

            ObjForeignTrade.Sales_Organization_Id = ddlSalesOrginization.SelectedValue;
            ObjForeignTrade.Distribution_Channel_ID = ddlDistributionChannel.SelectedValue;

            ObjForeignTrade.Commodity_Code = txtCommodityCode.Text;
            ObjForeignTrade.Origin_Country_Id = ddlCountry.SelectedValue;
            ObjForeignTrade.Origin_Region_Id = ddlRegion.SelectedValue;
            ObjForeignTrade.Imp_Exp_Mat_Grp = ddlExport.SelectedValue;
            ObjForeignTrade.Preference_Indicator_Imp_Exp = ddlPreference.SelectedValue;
            ObjForeignTrade.Exception_Certificate = ddlExemption.SelectedValue;
            ObjForeignTrade.Control_Code = ddlControlCode.SelectedValue;

            ObjForeignTrade.Chapter_ID = ddlChapter_ID.SelectedValue;
            ObjForeignTrade.Subcontractors = chkSubcontractors.Checked == true ? "1" : "0";
            ObjForeignTrade.Material_Type = ddlMaterial_Type.SelectedValue;
            ObjForeignTrade.No_of_Goods_Receipts_per_Excise_Invoice = ddlNo_of_Goods_Receipts_per_Excise_Invoice.SelectedValue;
            ObjForeignTrade.Output_Material_For_ModVat = txtOutput_Material_For_ModVat.Text;
            ObjForeignTrade.Remarks = txtRemarks.Text;
            //GST Start
            ObjForeignTrade.GSTRate = txtGSTRate.Text;
            ObjForeignTrade.GSTReq = ddlGSTReq.SelectedValue;
            //GST End

            ObjForeignTrade.IsActive = 1;
            ObjForeignTrade.UserId = lblUserId.Text;
            ObjForeignTrade.TodayDate = objUtil.GetDate();
            ObjForeignTrade.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjForeignTrade;
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
            ds = ObjForeignTradeAccess.GetForeignTradeData(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblForeignTradeId.Text = ds.Tables[0].Rows[0]["Mat_Foreign_Trade_Id"].ToString();
            }
            FillForeignTradeData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        //
    }

    private void FillForeignTradeData()
    {
        try
        {
            ForeignTrade ObjForeignTrade = GetForeignTradeData();
            if (ObjForeignTrade.Mat_Foreign_Trade_Id > 0)
            {
                lblForeignTradeId.Text = ObjForeignTrade.Mat_Foreign_Trade_Id.ToString();
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR

                ddlPlant.SelectedValue = ObjForeignTrade.Plant_Id;

                BindPlantWiseDropDown();

                ddlSalesOrginization.SelectedValue = ObjForeignTrade.Sales_Organization_Id;
                ddlDistributionChannel.SelectedValue = ObjForeignTrade.Distribution_Channel_ID;

                txtCommodityCode.Text = ObjForeignTrade.Commodity_Code;
                ddlCountry.SelectedValue = ObjForeignTrade.Origin_Country_Id;
                BindByCountry();

                ddlRegion.SelectedValue = ObjForeignTrade.Origin_Region_Id;
                ddlExport.SelectedValue = ObjForeignTrade.Imp_Exp_Mat_Grp;
                ddlPreference.SelectedValue = ObjForeignTrade.Preference_Indicator_Imp_Exp;
                ddlExemption.SelectedValue = ObjForeignTrade.Exception_Certificate;
                ddlControlCode.SelectedValue = ObjForeignTrade.Control_Code;

                ddlChapter_ID.SelectedValue = ObjForeignTrade.Chapter_ID;
                chkSubcontractors.Checked = ObjForeignTrade.Subcontractors == "1" ? true : false; ;
                ddlMaterial_Type.SelectedValue = ObjForeignTrade.Material_Type;
                ddlNo_of_Goods_Receipts_per_Excise_Invoice.SelectedValue = ObjForeignTrade.No_of_Goods_Receipts_per_Excise_Invoice;
                txtOutput_Material_For_ModVat.Text = ObjForeignTrade.Output_Material_For_ModVat;
                txtRemarks.Text = ObjForeignTrade.Remarks;
                //GST Start
                txtGSTRate.Text = ObjForeignTrade.GSTRate;
                ddlGSTReq.SelectedValue = ObjForeignTrade.GSTReq;
                //GST Start
            }
            else
            {

                lblForeignTradeId.Text = "0";
                //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
                //CTRL_SUB_SDT18112019 Commented by NR

                //CTRL_SUB_SDT18112019 Added by NR
                if (lblModuleId.Text == "162")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                else
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','FT','" + lblForeignTradeId.Text + "'", "Plant_Name", "Plant_Id", "");
                }
                //CTRL_SUB_SDT18112019 Added by NR
                ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();

                ddlCountry.SelectedValue = "1";
                BindByCountry();
            }
            ddlPlant.Enabled = false;
            ddlChapter_ID.Enabled = false;
            BindAttachedDocuments(lblMasterHeaderId.Text);
            //Promotion code start
            //if (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "171")
            if (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "171" || Session[StaticKeys.SelectedModuleId].ToString() == "195")
            {
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171"))
                {
                    ddlGSTReq.SelectedValue = "No";
                }
                //Promotion code End
                trGSTRate.Visible = true;
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
                if ((Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "3")
                {
                    ddlGSTReq.SelectedValue = "No";
                    trGSTReq.Visible = true;
                    reqddlGSTReq.Enabled = true;
                    LabelGSTReq.Visible = true;
                    //ConfigureGSTControls();
                }

            }
            //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End
        }
        catch (Exception ex)
        {
            _log.Error("FillForeignTradeData", ex);
        }
    }

    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.Foreign_Trade obj = new SectionConfiguration.Foreign_Trade();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));

            reqddlControlCode.Enabled = true;
            lableddlControlCode.Enabled = true;
            ddlControlCode.Enabled = true;
            ddlControlCode.Visible = true;
            reqddlControlCode.Visible = true;
            lableddlControlCode.Visible = true;
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void CheckIfChanges(ForeignTrade NewForeignTradeData, ForeignTrade oldForeignTradeData)
    {
        try
        {
            if (NewForeignTradeData.Mat_Foreign_Trade_Id > 0 && oldForeignTradeData.Mat_Foreign_Trade_Id > 0)
            {
                if (NewForeignTradeData.Plant_Id != oldForeignTradeData.Plant_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Plant ID/td> <td>" + oldForeignTradeData.Plant_Id + "</td><td>" + NewForeignTradeData.Plant_Id + "</td></tr>";
                if (NewForeignTradeData.Sales_Organization_Id != oldForeignTradeData.Sales_Organization_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sales Organization Id</td> <td>" + oldForeignTradeData.Sales_Organization_Id + "</td><td>" + NewForeignTradeData.Sales_Organization_Id + "</td></tr>";
                if (NewForeignTradeData.Distribution_Channel_ID != oldForeignTradeData.Distribution_Channel_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Distribution Channel ID</td> <td>" + oldForeignTradeData.Distribution_Channel_ID + "</td><td>" + NewForeignTradeData.Distribution_Channel_ID + "</td></tr>";
                if (NewForeignTradeData.Commodity_Code != oldForeignTradeData.Commodity_Code)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Commodity Code</td> <td>" + oldForeignTradeData.Commodity_Code + "</td><td>" + NewForeignTradeData.Commodity_Code + "</td></tr>";
                if (NewForeignTradeData.Origin_Country_Id != oldForeignTradeData.Origin_Country_Id)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Origin Country Id</td> <td>" + oldForeignTradeData.Origin_Country_Id + "</td><td>" + NewForeignTradeData.Origin_Country_Id + "</td></tr>";
                //if (NewForeignTradeData.Origin_Region_Id != oldForeignTradeData.Origin_Region_Id )
                //  Session[StaticKeys.ApprovalNote] += "<tr><td>Origin Region Id</td> <td>" + oldForeignTradeData.Origin_Region_Id + "</td><td>" + NewForeignTradeData.Origin_Region_Id + "</td></tr>";
                if (NewForeignTradeData.Imp_Exp_Mat_Grp != oldForeignTradeData.Imp_Exp_Mat_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Imp Exp Mat Grp</td> <td>" + oldForeignTradeData.Imp_Exp_Mat_Grp + "</td><td>" + NewForeignTradeData.Imp_Exp_Mat_Grp + "</td></tr>";
                if (NewForeignTradeData.Preference_Indicator_Imp_Exp != oldForeignTradeData.Preference_Indicator_Imp_Exp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Preference Indicator Imp Exp</td> <td>" + oldForeignTradeData.Preference_Indicator_Imp_Exp + "</td><td>" + NewForeignTradeData.Preference_Indicator_Imp_Exp + "</td></tr>";
                if (NewForeignTradeData.Exception_Certificate != oldForeignTradeData.Exception_Certificate)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Exception Certificate</td> <td>" + oldForeignTradeData.Exception_Certificate + "</td><td>" + NewForeignTradeData.Exception_Certificate + "</td></tr>";
                if (NewForeignTradeData.Control_Code != oldForeignTradeData.Control_Code)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Control Code</td> <td>" + oldForeignTradeData.Control_Code + "</td><td>" + NewForeignTradeData.Control_Code + "</td></tr>";
                if (NewForeignTradeData.Chapter_ID != oldForeignTradeData.Chapter_ID)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Chapter ID</td> <td>" + oldForeignTradeData.Chapter_ID + "</td><td>" + NewForeignTradeData.Chapter_ID + "</td></tr>";
                if (NewForeignTradeData.Subcontractors != (oldForeignTradeData.Subcontractors.ToLower() == "true" ? "1" : "0"))
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Subcontractors</td> <td>" + (oldForeignTradeData.Subcontractors.ToLower() == "true" ? "1" : "0") + "</td><td>" + NewForeignTradeData.Subcontractors + "</td></tr>";
                if (NewForeignTradeData.Material_Type != oldForeignTradeData.Material_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Type</td> <td>" + oldForeignTradeData.Material_Type + "</td><td>" + NewForeignTradeData.Material_Type + "</td></tr>";
                if (NewForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice != oldForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>No of Goods Receipts per Excise Invoice</td> <td>" + oldForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice + "</td><td>" + NewForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice + "</td></tr>";
                if (NewForeignTradeData.Output_Material_For_ModVat != oldForeignTradeData.Output_Material_For_ModVat)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Output Material For ModVat</td> <td>" + oldForeignTradeData.Output_Material_For_ModVat + "</td><td>" + NewForeignTradeData.Output_Material_For_ModVat + "</td></tr>";
                if (NewForeignTradeData.Remarks != oldForeignTradeData.Remarks)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldForeignTradeData.Remarks + "</td><td>" + NewForeignTradeData.Remarks + "</td></tr>";
            }

            //MSC_8300001775 Start Comment
            //if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
            //    Session[StaticKeys.ApprovalNote] = "";
            //MSC_8300001775 End Comment
            //MSC_8300001775 Start
            if (!(Session[StaticKeys.ApprovalNote].ToString().Contains("td")))
                Session[StaticKeys.ApprovalNote] = "";
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChanges", ex);
        }

    }

    //MSC_8300001775
    private void CheckIfChangesLog(ForeignTrade NewForeignTradeData, ForeignTrade oldForeignTradeData)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewForeignTradeData.Mat_Foreign_Trade_Id > 0 && oldForeignTradeData.Mat_Foreign_Trade_Id > 0)
            {
                if (NewForeignTradeData.Commodity_Code != oldForeignTradeData.Commodity_Code)
                {
                    WriteMatChangeLog("MatChangeLogFT" + sdate + ".txt", "44" + oldForeignTradeData.Commodity_Code + '-' + NewForeignTradeData.Commodity_Code);

                    _items.Add(new SMChange { colFieldName = 44, colOldVal = oldForeignTradeData.Commodity_Code, colNewVal = NewForeignTradeData.Commodity_Code });
                }
                if (NewForeignTradeData.Origin_Country_Id != oldForeignTradeData.Origin_Country_Id)
                    _items.Add(new SMChange { colFieldName = 45, colOldVal = oldForeignTradeData.Origin_Country_Id, colNewVal = NewForeignTradeData.Origin_Country_Id });
                if (NewForeignTradeData.Origin_Region_Id != oldForeignTradeData.Origin_Region_Id)
                    _items.Add(new SMChange { colFieldName = 46, colOldVal = oldForeignTradeData.Origin_Region_Id, colNewVal = NewForeignTradeData.Origin_Region_Id });
                if (NewForeignTradeData.Imp_Exp_Mat_Grp != oldForeignTradeData.Imp_Exp_Mat_Grp)
                    _items.Add(new SMChange { colFieldName = 47, colOldVal = oldForeignTradeData.Imp_Exp_Mat_Grp, colNewVal = NewForeignTradeData.Imp_Exp_Mat_Grp });
                if (NewForeignTradeData.Preference_Indicator_Imp_Exp != oldForeignTradeData.Preference_Indicator_Imp_Exp)
                    _items.Add(new SMChange { colFieldName = 48, colOldVal = oldForeignTradeData.Preference_Indicator_Imp_Exp, colNewVal = NewForeignTradeData.Preference_Indicator_Imp_Exp });
                if (NewForeignTradeData.Exception_Certificate != oldForeignTradeData.Exception_Certificate)
                    _items.Add(new SMChange { colFieldName = 49, colOldVal = oldForeignTradeData.Exception_Certificate, colNewVal = NewForeignTradeData.Exception_Certificate });
                if (NewForeignTradeData.Control_Code != oldForeignTradeData.Control_Code)
                    _items.Add(new SMChange { colFieldName = 50, colOldVal = oldForeignTradeData.Control_Code, colNewVal = NewForeignTradeData.Control_Code });
                if (NewForeignTradeData.Chapter_ID != oldForeignTradeData.Chapter_ID)
                    _items.Add(new SMChange { colFieldName = 1279, colOldVal = oldForeignTradeData.Chapter_ID, colNewVal = NewForeignTradeData.Chapter_ID });
                if (NewForeignTradeData.Material_Type != oldForeignTradeData.Material_Type)
                    _items.Add(new SMChange { colFieldName = 1283, colOldVal = oldForeignTradeData.Material_Type, colNewVal = NewForeignTradeData.Material_Type });
                if (NewForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice != oldForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice)
                    _items.Add(new SMChange { colFieldName = 1311, colOldVal = oldForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice, colNewVal = NewForeignTradeData.No_of_Goods_Receipts_per_Excise_Invoice });
                if (NewForeignTradeData.Output_Material_For_ModVat != oldForeignTradeData.Output_Material_For_ModVat)
                    _items.Add(new SMChange { colFieldName = 1284, colOldVal = oldForeignTradeData.Output_Material_For_ModVat, colNewVal = NewForeignTradeData.Output_Material_For_ModVat });
                if (NewForeignTradeData.Subcontractors != oldForeignTradeData.Subcontractors)
                    _items.Add(new SMChange { colFieldName = 1282, colOldVal = (oldForeignTradeData.Subcontractors.ToLower() == "1" ? "X" : ""), colNewVal = (NewForeignTradeData.Subcontractors.ToLower() == "1" ? "X" : "") });

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
                ChangeSMatID1 = helperAccess.MaterialChange("7", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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

    //private void ConfigureGSTControls()
    //{
    //    bool flg;
    //    if (ddlGSTReq.SelectedValue.ToString() == "Yes")
    //        flg = true;
    //    else
    //        flg = false;

    //    reqddlControlCode.Enabled = flg;
    //    reqtxtGSTRate.Enabled = flg;
    //    reqddlChapter_ID.Enabled = flg;

    //    lableGSTRate.Visible = flg;
    //    lableddlChapter_ID.Visible = flg;
    //    lableddlControlCode.Visible = flg;
    //}

    #endregion

    #region Document Upload

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
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/Material/MaterialDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

        try
        {
            savePath = MapPath(StrPath);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }
        catch (Exception ex)
        {
            _log.Error("SaveDocuments", ex);
        }
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
        try
        {
        string sufix = sufix1.NextDouble().ToString().Replace(".", "");

        if (uploadfile.ContentLength > 0)
        {
            string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

            string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
            savePath = savePath + "\\" + uploadedFileName;

            ObjDoc.Document_Upload_Id = 0;
            ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
            ObjDoc.Document_Type = "";
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
            lblMsg.Text = "Error While Saving Material Details.";
        }

        }
        catch (Exception ex)
        { _log.Error("UploadDocument", ex); }
        return flag;
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

    #endregion



}