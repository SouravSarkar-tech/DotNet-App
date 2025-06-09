using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Accenture.MWT.DomainObject;
using System.Transactions;
using System.Configuration;
using System.Globalization;
using System.Data.OleDb;
using System.Web.Configuration;
using log4net;
public partial class Transaction_Material_MaterialChange : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string sdate = "";
    MaterialChangeAccess ObjMaterialChangeAccess = new MaterialChangeAccess();
    HelperAccess helperAccess = new HelperAccess();
    public bool isEditable { get; set; }

    #region Page Methods

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
                        lblMatPlantGrpId.Text = Session[StaticKeys.MatPlantGrp].ToString();
                        //Session[StaticKeys.TypeOfMassUpdm] = "3";
                        //MSC_8300001775 Start 

                        //DateTime reqCreateDate;
                        //var mscLiveDate = DateTime.ParseExact(Convert.ToString(ConfigurationManager.AppSettings["mscLiveDate"]), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        //reqCreateDate = DateTime.Today;
                        //try
                        //{
                        //    MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
                        //    DataSet dsms = materialMasterAccess.ReadReqDateForChange(Session[StaticKeys.MasterHeaderId].ToString());
                        //    if (dsms.Tables[0].Rows.Count > 0)
                        //    {
                        //        if (dsms.Tables[0].Rows[0]["ChangeOn"].ToString() != "")
                        //        {
                        //            reqCreateDate = DateTime.ParseExact(dsms.Tables[0].Rows[0]["ChangeOn"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        //        }
                        //        else
                        //        {
                        //            reqCreateDate = DateTime.Today;
                        //        }
                        //    }
                        //}
                        //catch (Exception ex) { }

                        string oldreq = "";
                        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
                        DataSet dsms = materialMasterAccess.ReadReqDateForChange(Session[StaticKeys.MasterHeaderId].ToString());
                        if (dsms.Tables[0].Rows.Count > 0)
                        {
                            oldreq = dsms.Tables[0].Rows[0]["ChangeOn"].ToString();
                        }
                        //if (reqCreateDate > mscLiveDate)
                        if (oldreq == "NEW")
                        {
                            //pnlData.Visible = false;
                            //pnlMSChange.Visible = true;
                            oldtrMC.Attributes.Add("Style", "display:none!important;");
                            newtrMC.Attributes.Add("Style", "display:block!;");
                            ChangeExcelUpload1.Visible = false;
                            ExcelDownload1.Visible = false;
                            //PopuplateDropDownListMass();
                            BindAttachedDocuments(lblMasterHeaderId.Text);

                            PopuplateDropDownListMass();

                            hlMSImportFormat.NavigateUrl = "";

                            if (ddlTypeOfMassUpdm.SelectedValue == "11")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/MRPViewData.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "12")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/SelectionMethod.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "13")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/PlannedPrice.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "14")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/TaggingofBOM.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "15")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/ProductHierarchy.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "16")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Other.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "17")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/ConvirsionFactor.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "18")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/TaxView.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "19")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/InspectionSetup.xlsx";
                            }
                            else if (ddlTypeOfMassUpdm.SelectedValue == "20")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Classification.xlsx";
                            }
                            //PROV-CCP-MM-941-23-0045 S
                            else if (ddlTypeOfMassUpdm.SelectedValue == "23")
                            {
                                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Kinaxis_Characteristics.xlsx";
                            }
                            //PROV-CCP-MM-941-23-0045 E
                            //PopuplateDropDownListMass();

                            //BindAttachedDocuments(lblMasterHeaderId.Text);

                            HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                            //if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                            //{
                            //    lnkAddNew.Visible = true;
                            //    isEditable = true;
                            //}

                            //BindMaterialChangeData();
                            //FillMaterialChangeData();

                            if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                            {
                                trMassBtn.Visible = true;
                                //btnMassSave.Visible = true;

                                //lnkAddNew.Visible = true;
                                //grvMaterialChange.Columns[1].Visible = true;
                            }
                            else
                            {
                                //grvMaterialChange.Columns[1].Visible = false;
                                //lnkAddNew.Visible = false;
                                trMassBtn.Visible = false;
                                btnMassSave.Visible = false;
                                fileUploadMS.Visible = false;
                                lblFileMessage.Visible = false;
                                btnMSProcess.Visible = false;
                                lblselectcap.Visible = false;
                                lblSelectFile.Visible = false;
                                hlMSImportFormat.Visible = false;
                                //ChangeExcelUpload1.Visible = false;
                                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                                {

                                    ExcelDownload2.Visible = true;
                                }

                            }

                        }//MSC_8300001775 End
                        else
                        {
                            //pnlData.Visible = true;
                            //pnlMSChange.Visible = false;
                            oldtrMC.Attributes.Add("Style", "display:block!;");
                            newtrMC.Attributes.Add("Style", "display:none!important;");

                            //CTRL_SUB_SDT18112019 Added by NR
                            try
                            {
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

                            PopuplateDropDownList();

                            HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                            if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                            {
                                lnkAddNew.Visible = true;
                                isEditable = true;
                            }

                            BindMaterialChangeData();
                            FillMaterialChangeData();
                            //BindAttachedDocuments(lblMasterHeaderId.Text);

                            if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                            {
                                trButton.Visible = true;
                                btnSave.Visible = false;
                                btnNext.Visible = true;

                                lnkAddNew.Visible = true;
                                grvMaterialChange.Columns[6].Visible = true;
                            }
                            else
                            {
                                grvMaterialChange.Columns[6].Visible = false;
                                lnkAddNew.Visible = false;
                                //file_upload.Visible = false;

                                ChangeExcelUpload1.Visible = false;
                                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                                {
                                    //ExcelDownload1.ActionType = "C";
                                    ExcelDownload1.Visible = true;
                                }

                            }
                        }
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


    protected void ddlTypeOfMassUpdm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hlMSImportFormat.NavigateUrl = "";

            if (ddlTypeOfMassUpdm.SelectedValue == "11")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/MRPViewData.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "12")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/SelectionMethod.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "13")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/PlannedPrice.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "14")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/TaggingofBOM.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "15")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/ProductHierarchy.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "16")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Other.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "17")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/ConvirsionFactor.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "18")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/TaxView.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "19")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/InspectionSetup.xlsx";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "20")
            {
                hlMSImportFormat.NavigateUrl = "~/Transaction/Material/MatChangeDoc/Classification.xlsx";
            }
            //PROV-CCP-MM-941-23-0045 S
            else if (ddlTypeOfMassUpdm.SelectedValue == "23")
            {
                hlMSImportFormat.NavigateUrl = "/Transaction/Material/MatChangeDoc/Kinaxis_Characteristics.xlsx";
            }
            //PROV-CCP-MM-941-23-0045 E
        }
        catch (Exception ex)
        { _log.Error("ddlTypeOfMassUpdm_SelectedIndexChanged", ex); }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;

            if (SaveMaterialChangeData())
            {
                ModalPopupExtender.Hide();

                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                BindMaterialChangeData();
            }
            else
            {
                ModalPopupExtender.Show();
            }
        }
        catch (Exception ex)
        { _log.Error("btnAdd_Click", ex); }
    }

    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;

            if (SaveMaterialChangeData())
            {
                ModalPopupExtender.Hide();

                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                BindMaterialChangeData();
            }
            else
            {
                ModalPopupExtender.Show();
            }

        }
        catch (Exception ex)
        { _log.Error("btnAddValue_Click", ex); }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;

            ImageButton lnkEditValue = (ImageButton)sender;

            lblMaterialChangeDetailId.Text = lnkEditValue.CommandArgument;
            FillMaterialChangeDetailData();
            lblMaterialChangeAction.Text = "E";
            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("btnEdit_Click", ex); }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMsg.Visible = false;

            ImageButton btnDelete = (ImageButton)sender;

            ObjMaterialChangeAccess.DeleteMaterialChangeDetail(btnDelete.CommandArgument);

            lblMsg.Text = "Record Deleted Sucessfully";
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            BindMaterialChangeData();
        }
        catch (Exception ex)
        { _log.Error("btnDelete_Click", ex); }
    }

    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton lnkAddValue = (ImageButton)sender;
            pnlMsg.Visible = false;
            pnlMsg1.Visible = false;
            lblMaterialChangeDetailId.Text = "0";
            lblMaterialChangeAction.Text = "F";
            lblMaterialChange.Text = lnkAddValue.CommandArgument;
            FillMaterialChangeDetailData();
            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("lnkAddValue_Click", ex); }

    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            lblMaterialChange.Text = "0";
            lblMaterialChangeAction.Text = "C";
            lblMaterialChangeDetailId.Text = "0";
            pnlMsg.Visible = false;
            pnlMsg1.Visible = false;
            FillMaterialChangeData();

            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("lnkAddNew_Click", ex); }
    }

    protected void txtMaterialCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.MaterialProcessModuleId] != null)
            {
                pnlMsg1.Visible = false;
                if (Session[StaticKeys.MaterialProcessModuleId].ToString() != MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text))
                {
                    lblMsg1.Text = "Please enter only " + ddlMaterialAccGrp.SelectedItem.Text;
                    pnlMsg1.Visible = true;
                    pnlMsg1.CssClass = "error";
                    txtMaterialCode.Text = "";
                }
            }
            else
            {
                ddlMaterialAccGrp.SelectedValue = MaterialHelper.GetMaterialAccGrpByMaterialCode(txtMaterialCode.Text);
            }

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
            ModalPopupExtender.Show();
            txtMaterialName.Focus();
        }
        catch (Exception ex)
        { _log.Error("txtMaterialCode_TextChanged", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        //if (CheckIsValid())
        //{
        //    if (Save())
        //    {
        //        string pageURL = btnPrevious.CommandArgument.ToString();
        //        Response.Redirect(pageURL);
        //    }
        //}
        //else
        //{
        //    lblMsg.Text = "Please fill atleast one feild.";
        //    pnlMsg.Visible = true;
        //    pnlMsg.CssClass = "error";
        //}
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMassSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValidMass())
            {
                if (SaveMass())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;

                    Response.Redirect("MaterialChange.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Please upload file.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnMassSave_Click", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <returns></returns>
    protected bool SaveMass()
    {
        bool Flag = false;
        MaterialChange ObjMaterialChange = GetControlsValueMass();

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (ObjMaterialChangeAccess.SaveMass(ObjMaterialChange) > 0)
                {
                    scope.Complete();
                    Flag = true;
                }
                else
                {
                    lblMsg1.Text = Messages.GetMessage(-1);
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
            }
        }

        catch (Exception ex)
        {
            _log.Error("SaveMass", ex);
        }
        return Flag;
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void PopuplateDropDownListMass()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlTypeOfMassUpdm, "pr_GetDropDownListByControlNameModuleType 'M','ddlTypeOfMassUpdm'", "LookUp_Desc", "LookUp_Code", "");
            
            if (Session[StaticKeys.TypeOfMassUpdm].ToString() != "")
            {
                ddlTypeOfMassUpdm.SelectedValue = Convert.ToString(Session[StaticKeys.TypeOfMassUpdm]);
            }
        }
        catch (Exception ex)
        {
            _log.Error("PopuplateDropDownListMass", ex);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save())
                {
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    //BindAttachedDocuments(lblMasterHeaderId.Text);

                    Response.Redirect("MaterialChange.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnSave_Click", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckIsValid())
            {
                if (Save())
                {
                    Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
                    Response.Redirect("MaterialMaster.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Please fill atleast one feild.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void ddlSalesOrginization_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("ddlSalesOrginization_SelectedIndexChanged", ex); }
    }

    //protected void grdAttachedDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "DEL")
    //    {
    //        DataAccessLayer objDb = new DataAccessLayer();
    //        SqlTransaction objTrans;
    //        Control ctl = e.CommandSource as Control;
    //        GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
    //        string documentId = grdAttachedDocs.DataKeys[CurrentRow.RowIndex].Value.ToString();
    //        Label lblUploadedFileName = grdAttachedDocs.Rows[CurrentRow.RowIndex].FindControl("lblUploadedFileName") as Label;

    //        try
    //        {
    //            objDb.OpenConnection(this.Page);
    //            objTrans = objDb.cnnConnection.BeginTransaction();

    //            if (objDb.DeleteRecord("Vendor_Documents", "Document_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
    //            {
    //                System.IO.File.Delete(Server.MapPath("MaterialDocuments") + "/" + lblUploadedFileName.Text);
    //                objTrans.Commit();
    //                pnlMsg.Visible = false;
    //                BindAttachedDocuments(lblMasterHeaderId.Text);
    //            }
    //            else
    //            {
    //                objTrans.Rollback();
    //                lblMsg.Text = "Error While Deleting File.";
    //                pnlMsg.Visible = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        finally
    //        {
    //            objDb.CloseConnection(objDb.cnnConnection);
    //            objDb = null;
    //            objTrans = null;
    //        }
    //    }
    //}

    protected void ddlMaterialAccGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("ddlMaterialAccGrp_SelectedIndexChanged", ex); }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            ModalPopupExtender.Show();
            ddlField.Focus();
        }
        catch (Exception ex)
        { _log.Error("ddlSection_SelectedIndexChanged", ex); }
    }

    protected void grvMaterialChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMaterialChangeId = (Label)e.Row.FindControl("lblMaterialChangeId");

                GridView grvMaterialChangeDtl = (GridView)e.Row.FindControl("grvMaterialChangeDtl");
                bindgrvMaterialChangeDtl(Convert.ToInt32(lblMaterialChangeId.Text), grvMaterialChangeDtl); //Bind the child gridvie here ..
                grvMaterialChangeDtl.Columns[5].Visible = lnkAddNew.Visible;
            }
        }
        catch (Exception ex)
        { _log.Error("grvMaterialChange_RowDataBound", ex); }
    }

    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPlantWiseDropDown();
            ModalPopupExtender.Show();
        }
        catch (Exception ex)
        { _log.Error("ddlPlant_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Public Methods

    private void PopuplateDropDownList()
    {
        try
        {
            //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialChangeId.Text + "'", "Plant_Name", "Plant_Id", "");
            //CTRL_SUB_SDT18112019 Commented by NR

            //CTRL_SUB_SDT18112019 Added by NR
            if (lblRefModuleId.Text == "162")
            {

                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantListCtrl '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialChangeId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '" + lblMasterHeaderId.Text + "','MC','" + lblMaterialChangeId.Text + "'", "Plant_Name", "Plant_Id", "");
            }
            //CTRL_SUB_SDT18112019 Added by NR

            helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");


            helperAccess.PopuplateDropDownList(ddlSalesOrginization, "pr_GetSalesOrganisationList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialChangeId.Text + "'", "Sales_Organization_Name", "Sales_Organization_Id", "");
            helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");

            helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
            helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
            //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

            DataSet ds;
            ds = objMatAccess.ReadModules("M");


            ddlMaterialAccGrp.DataSource = ds;
            ddlMaterialAccGrp.DataTextField = "Module_Name";
            ddlMaterialAccGrp.DataValueField = "Module_Id";
            ddlMaterialAccGrp.DataBind();

            ddlMaterialAccGrp.Items.Insert(0, new ListItem("---Select---", "0"));

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlStorageLocation, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlStorageLocation','9','" + ddlPlant.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            if (Session[StaticKeys.MaterialPlantId].ToString() == "8888" || Session[StaticKeys.MaterialPlantId].ToString() == "8889")
                ddlStorageLocation.Enabled = true;
        }
        catch (Exception ex)
        { _log.Error("BindPlantWiseDropDown", ex); }
    }

    protected bool SaveMaterialChangeData()
    {
        bool Flag = false;
        MaterialChange ObjMaterialChange = GetControlsValue();

        try
        {
            if (!CheckDuplicateVendor())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ObjMaterialChangeAccess.Save(ObjMaterialChange) > 0)
                    {
                        scope.Complete();
                        Flag = true;
                    }
                    else
                    {
                        lblMsg1.Text = Messages.GetMessage(-1);
                        pnlMsg1.CssClass = "error";
                        pnlMsg1.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("SaveMaterialChangeData", ex);
        }
        return Flag;
    }

    private bool CheckDuplicateVendor()
    {
        bool flg = false;
        try
        {
            if (lblMaterialChangeAction.Text == "F")
            {
                foreach (GridViewRow gr in grvMaterialChange.Rows)
                {
                    Label lblPlantId = (Label)gr.Cells[0].FindControl("lblPlantId");
                    Label lblStorageLocationId = (Label)gr.Cells[0].FindControl("lblStorageLocationId");
                    Label lblSalesOrgId = (Label)gr.Cells[0].FindControl("lblSalesOrgId");
                    Label lblDistChnlId = (Label)gr.Cells[0].FindControl("lblDistChnlId");


                    if (txtMaterialCode.Text == gr.Cells[1].Text && ddlSalesOrginization.SelectedValue == lblSalesOrgId.Text && ddlDistributionChannel.SelectedValue == lblDistChnlId.Text && ddlStorageLocation.SelectedValue == lblStorageLocationId.Text)
                    {
                        //if (txtMaterialCode.Text == gr.Cells[1].Text)
                        //{
                        GridView grvMaterialChangeDtl = (GridView)gr.FindControl("grvMaterialChangeDtl");
                        foreach (GridViewRow gr1 in grvMaterialChangeDtl.Rows)
                        {
                            Label lblSectionFeildMasterId = (Label)gr1.FindControl("lblSectionFeildMasterId");

                            if (ddlField.SelectedValue == lblSectionFeildMasterId.Text)
                            {
                                flg = true;
                            }
                        }
                    }
                }

                if (flg)
                {
                    lblMsg1.Text = "Duplicate Field,You have already made entry for the field.";
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
            }
            else if (lblMaterialChangeAction.Text == "C")
            {
                foreach (GridViewRow gr in grvMaterialChange.Rows)
                {
                    Label lblPlantId = (Label)gr.Cells[0].FindControl("lblPlantId");
                    Label lblStorageLocationId = (Label)gr.Cells[0].FindControl("lblStorageLocationId");
                    Label lblSalesOrgId = (Label)gr.Cells[0].FindControl("lblSalesOrgId");
                    Label lblDistChnlId = (Label)gr.Cells[0].FindControl("lblDistChnlId");

                    if (txtMaterialCode.Text == gr.Cells[1].Text && ddlSalesOrginization.SelectedValue == lblSalesOrgId.Text && ddlDistributionChannel.SelectedValue == lblDistChnlId.Text && ddlStorageLocation.SelectedValue == lblStorageLocationId.Text)
                    {
                        flg = true;
                    }
                }
                if (flg)
                {
                    lblMsg1.Text = "Duplicate Material. To enter more fields for the same Material, Click the '+' in front of the Material.";
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
            }
            else if (txtOldValue.Text == "" && txtNewValue.Text == "")
            {
                lblMsg1.Text = "Both Old value and New Value cannot be blank.";
                pnlMsg1.CssClass = "error";
                pnlMsg1.Visible = true;
                flg = true;
            }
            else if (txtOldValue.Text == txtNewValue.Text)
            {
                lblMsg1.Text = "Old value and New Value cannot be same.";
                pnlMsg1.CssClass = "error";
                pnlMsg1.Visible = true;
                flg = true;
            }

        }
        catch (Exception ex)
        { _log.Error("CheckDuplicateVendor", ex); }
        return flg;
    }

    private void BindMaterialChangeData()
    {
        try
        {
            DataSet ds = ObjMaterialChangeAccess.GetMaterialChangeData(lblMasterHeaderId.Text);

            grvMaterialChange.DataSource = ds.Tables[0];
            grvMaterialChange.DataBind();

        }
        catch (Exception ex)
        { _log.Error("BindMaterialChangeData", ex); }
        //if (ds.Tables[1].Rows.Count > 0)
        //    lblMaterialType.Text = ds.Tables[0].Rows[0]["Material_Category"].ToString();
    }

    private void bindgrvMaterialChangeDtl(int MaterialChangeId, GridView grvMaterialChangeDtl)
    {
        try
        {
            grvMaterialChangeDtl.DataSource = ObjMaterialChangeAccess.GetMaterialChangeDetailData(MaterialChangeId);
            grvMaterialChangeDtl.DataBind();
        }
        catch (Exception ex)
        { _log.Error("bindgrvMaterialChangeDtl", ex); }
    }

    private bool Save()
    {
        int flg = 0;
        bool boolFlg = false;
        try
        {
            HttpFileCollection fileCollection = Request.Files;
            string fileExtension = string.Empty;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];
                if (uploadfile.ContentLength > 0)
                {
                    fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();
                    if ((fileExtension == ".pdf") || (fileExtension == ".tif") || (fileExtension == ".xls") || (fileExtension == ".xlsx") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png"))
                    {
                        flg = 1;
                    }
                    else
                    {
                        flg = 2;
                        break;
                    }
                }
            }

            if (flg == 2)
            {
                lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .xls, .xlsx .Gif, .TIF, .Png, .Pdf files allowed.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            else
            {
                boolFlg = SaveMaterialChange();
            }
        }
        catch (Exception ex)
        { _log.Error("Save", ex); }
        return boolFlg;
    }

    private bool SaveMaterialChange()
    {
        bool Flag = false;

        try
        {
            Flag = true;
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    if (SaveDocuments(lblMasterHeaderId.Text))
            //    {
            //        scope.Complete();
            //        Flag = true;
            //    }
            //    else
            //    {
            //        lblMsg.Text = Messages.GetMessage(-1);
            //        pnlMsg.CssClass = "error";
            //        pnlMsg.Visible = true;
            //    }
            //}
            //BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        catch (Exception ex)
        {
            _log.Error("SaveMaterialChange", ex);
        }
        return Flag;
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <returns></returns>
    private MaterialChange GetControlsValueMass()
    {
        MaterialChange ObjMaterialChange = new MaterialChange();
        Utility objUtil = new Utility();
        try
        {
            ObjMaterialChange.Material_Change_Id = Convert.ToInt32(lblMaterialChange.Text);
            ObjMaterialChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjMaterialChange.Material_Code = "";
            ObjMaterialChange.Plant_Id = Convert.ToString(Session[StaticKeys.MaterialPlantId]);
            ObjMaterialChange.Storage_Location = Convert.ToString(Session[StaticKeys.MatStorageLocationId]);
            ObjMaterialChange.Material_Desc = "Mass Material";
            ObjMaterialChange.Material_Acc_Grp = Convert.ToString(Session[StaticKeys.SelectedModuleId]);
            ObjMaterialChange.Sales_Organisation_Id = "";
            ObjMaterialChange.Distribution_Channel_Id = "";

            ObjMaterialChange.IsActive = 1;
            ObjMaterialChange.UserId = lblUserId.Text;
            ObjMaterialChange.TodayDate = objUtil.GetDate();
            ObjMaterialChange.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValueMass", ex); }
        return ObjMaterialChange;
    }



    private MaterialChange GetMaterialChange()
    {
        return ObjMaterialChangeAccess.GetMaterialChange(Convert.ToInt32(lblMaterialChange.Text));
    }

    private MaterialChangeDetail GetMaterialChangeDetail()
    {
        return ObjMaterialChangeAccess.GetMaterialChangeDetail(Convert.ToInt32(lblMaterialChangeDetailId.Text));
    }

    private MaterialChange GetControlsValue()
    {
        MaterialChange ObjMaterialChange = new MaterialChange();
        Utility objUtil = new Utility();
        try
        {
            ObjMaterialChange.Material_Change_Id = Convert.ToInt32(lblMaterialChange.Text);
            ObjMaterialChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjMaterialChange.Material_Code = txtMaterialCode.Text;
            ObjMaterialChange.Plant_Id = ddlPlant.SelectedValue;
            ObjMaterialChange.Storage_Location = ddlStorageLocation.SelectedValue;
            ObjMaterialChange.Material_Desc = txtMaterialName.Text;
            ObjMaterialChange.Material_Acc_Grp = ddlMaterialAccGrp.SelectedValue;
            ObjMaterialChange.Sales_Organisation_Id = ddlSalesOrginization.SelectedValue;
            ObjMaterialChange.Distribution_Channel_Id = ddlDistributionChannel.SelectedValue;


            ObjMaterialChange.Material_Change_Detail_Id = Convert.ToInt32(lblMaterialChangeDetailId.Text);
            ObjMaterialChange.Section_Id = Convert.ToInt32(ddlSection.SelectedValue);
            ObjMaterialChange.Section_Feild_Master_Id = Convert.ToInt32(ddlField.SelectedValue);
            ObjMaterialChange.Old_Value = txtOldValue.Text;
            ObjMaterialChange.New_Value = txtNewValue.Text;

            ObjMaterialChange.IsActive = 1;
            ObjMaterialChange.UserId = lblUserId.Text;
            ObjMaterialChange.TodayDate = objUtil.GetDate();
            ObjMaterialChange.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjMaterialChange;
    }

    private void FillMaterialChangeData()
    {
        MaterialChange ObjMaterialChange = GetMaterialChange();

        try
        {
            if (ObjMaterialChange.Material_Change_Id > 0)
            {
                lblMaterialChange.Text = ObjMaterialChange.Material_Change_Id.ToString();
                txtMaterialCode.Text = ObjMaterialChange.Material_Code;
                ddlPlant.SelectedValue = ObjMaterialChange.Plant_Id;
                BindPlantWiseDropDown();
                ddlStorageLocation.SelectedValue = ObjMaterialChange.Storage_Location;
                ddlMaterialAccGrp.SelectedValue = ObjMaterialChange.Material_Acc_Grp;
                ddlSalesOrginization.SelectedValue = ObjMaterialChange.Sales_Organisation_Id;

                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                ddlDistributionChannel.SelectedValue = ObjMaterialChange.Distribution_Channel_Id;

                txtMaterialName.Text = ObjMaterialChange.Material_Desc;
            }
            else
            {
                //ddlPlant.SelectedValue = "32";
                if (Session[StaticKeys.MaterialProcessModuleId] != null)
                {
                    ddlMaterialAccGrp.SelectedValue = Session[StaticKeys.MaterialProcessModuleId].ToString();
                }
                else
                {
                    ddlMaterialAccGrp.SelectedValue = "0";
                }

                if (Session[StaticKeys.MaterialPlantId].ToString() == "8888")
                    ddlPlant.SelectedValue = "32";
                else if (Session[StaticKeys.MaterialPlantId].ToString() == "8889")
                    ddlPlant.SelectedValue = "33";
                else
                    ddlPlant.SelectedValue = Session[StaticKeys.MaterialPlantId].ToString();

                BindPlantWiseDropDown();
                ddlStorageLocation.SelectedValue = "";

                txtMaterialCode.Text = "";

                lblModuleId.Text = "0";
                txtMaterialName.Text = "";
                ddlSalesOrginization.SelectedValue = "";
                ddlDistributionChannel.SelectedValue = "";


                helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

                txtOldValue.Text = "";
                txtNewValue.Text = "";
            }
            MakeDisable(true);
        }
        catch (Exception ex)
        { _log.Error("FillMaterialChangeData", ex); }
    }

    private void FillMaterialChangeDetailData()
    {
        MaterialChangeDetail ObjMaterialChangeDetail = GetMaterialChangeDetail();
        try
        {
            if (ObjMaterialChangeDetail.Material_Change_Id > 0)
            {
                lblMaterialChange.Text = ObjMaterialChangeDetail.Material_Change_Id.ToString();

                MaterialChange objMaterialChange = ObjMaterialChangeAccess.GetMaterialChange(Convert.ToInt32(lblMaterialChange.Text));
                txtMaterialCode.Text = objMaterialChange.Material_Code;
                lblModuleId.Text = objMaterialChange.Material_Acc_Grp;
                txtMaterialName.Text = objMaterialChange.Material_Desc;
                ddlPlant.SelectedValue = objMaterialChange.Plant_Id;
                BindPlantWiseDropDown();
                ddlStorageLocation.SelectedValue = objMaterialChange.Storage_Location;
                ddlMaterialAccGrp.SelectedValue = objMaterialChange.Material_Acc_Grp;
                ddlSalesOrginization.SelectedValue = objMaterialChange.Sales_Organisation_Id;

                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                ddlDistributionChannel.SelectedValue = objMaterialChange.Distribution_Channel_Id;


                helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
                ddlSection.SelectedValue = ObjMaterialChangeDetail.Section_Id.ToString();
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ObjMaterialChangeDetail.Section_Id + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");
                ddlField.SelectedValue = ObjMaterialChangeDetail.Section_Feild_Master_Id.ToString();

                txtOldValue.Text = ObjMaterialChangeDetail.Old_Value;
                txtNewValue.Text = ObjMaterialChangeDetail.New_Value;

                MakeDisable(false);
            }
            else
            {
                MaterialChange objMaterialChange = ObjMaterialChangeAccess.GetMaterialChange(Convert.ToInt32(lblMaterialChange.Text));
                txtMaterialCode.Text = objMaterialChange.Material_Code;
                lblModuleId.Text = objMaterialChange.Material_Acc_Grp;
                txtMaterialName.Text = objMaterialChange.Material_Desc;
                ddlPlant.SelectedValue = objMaterialChange.Plant_Id;
                BindPlantWiseDropDown();
                ddlStorageLocation.SelectedValue = objMaterialChange.Storage_Location;
                ddlMaterialAccGrp.SelectedValue = objMaterialChange.Material_Acc_Grp;
                ddlSalesOrginization.SelectedValue = objMaterialChange.Sales_Organisation_Id;

                helperAccess.PopuplateDropDownList(ddlDistributionChannel, "pr_GetDistributionChannelList '" + lblMasterHeaderId.Text + "','SD1','" + lblMaterialChangeId.Text + "','" + ddlSalesOrginization.SelectedValue + "'", "Distribution_Channel_Name", "Distribution_Channel_ID", "");
                ddlDistributionChannel.SelectedValue = objMaterialChange.Distribution_Channel_Id;


                helperAccess.PopuplateDropDownList(ddlSection, "pr_GetSectionByModuleId '" + ddlMaterialAccGrp.SelectedValue + "','" + lblUserId.Text + "','" + lblMatPlantGrpId.Text + "'", "Section_Name", "Section_Id");
                helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ddlSection.SelectedValue + "','" + lblMatPlantGrpId.Text + "',null", "Field_Name", "Field_Id");
                //helperAccess.PopuplateDropDownList(ddlField, "pr_GetFieldsByModuleSectionId '" + ddlMaterialAccGrp.SelectedValue + "','" + ObjMaterialChangeDetail.Section_Id + "',null,'" + ddlCompanyCode.SelectedValue + "'", "Field_Name", "Field_Id");

                txtOldValue.Text = "";
                txtNewValue.Text = "";
                MakeDisable(false);
                //ddlPlant.Enabled = false;
                if ((Session[StaticKeys.MaterialPlantId].ToString() == "8888") || (Session[StaticKeys.MaterialPlantId].ToString() == "8889"))
                    ddlPlant.Enabled = true;
                else
                    ddlPlant.Enabled = false;
            }

        }
        catch (Exception ex)
        { _log.Error("FillMaterialChangeDetailData", ex); }
    }

    private void MakeDisable(bool flg)
    {
        try
        {
            txtMaterialCode.Enabled = flg;
            txtMaterialName.Enabled = flg;
            //ddlPlant.Enabled = flg;
            if ((Session[StaticKeys.MaterialPlantId].ToString() == "8888") || (Session[StaticKeys.MaterialPlantId].ToString() == "8889"))
                ddlPlant.Enabled = true;
            else
                ddlPlant.Enabled = false;
            ddlStorageLocation.Enabled = flg;
            ddlSalesOrginization.Enabled = flg;
            ddlDistributionChannel.Enabled = flg;
        }
        catch (Exception ex)
        { _log.Error("MakeDisable", ex); }
    }

    private bool CheckIsValid()
    {
        bool flg = false;
        try
        {
            if (grvMaterialChange.Rows.Count > 0)
                flg = true;

        }
        catch (Exception ex)
        { _log.Error("CheckIsValid", ex); }
        return flg;
    }

    #endregion

    #region Document Upload

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="MaterialId"></param>
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

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="MaterialId"></param>
    /// <returns></returns>
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

            if (ddlTypeOfMassUpdm.SelectedValue == "11")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/MRPUpdExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "12")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/SMethodExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "13")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/PPUpdExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "14")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/TagBOMExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "15")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/PHUpdExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "16")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/OtherExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "17")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/AUOMExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "18")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/TAXMExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "19")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/INSPExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            else if (ddlTypeOfMassUpdm.SelectedValue == "20")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/ClassExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            //PROV-CCP-MM-941-23-0045 S
            else if (ddlTypeOfMassUpdm.SelectedValue == "23")
            {
                StrPath = "/Transaction/Material/MatChangeDoc/KinaxisExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            }
            //PROV-CCP-MM-941-23-0045 E
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
            _log.Error("SaveDocuments", ex);
            return false;
        }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="uploadfile"></param>
    /// <param name="StrPath"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
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
                //string fileExtension = System.IO.Path.GetExtension(uploadfile.FileName);

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
                    lblMsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
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

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                    throw ex;
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

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbackMsg_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("MaterialMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnbackMsg_Click", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMSProcess_Click(object sender, EventArgs e)
    {
        if (fileUploadMS.HasFile)
        {
            try
            {

                //if(fileUploadMS.HasFile)
                if ((SaveDocuments(lblMasterHeaderId.Text)))
                {
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                    if (ddlTypeOfMassUpdm.SelectedValue == "16")
                    {
                        try
                        {
                            OtherFieldsUpdation();
                        }
                        catch (Exception ex) { }
                    }

                    //WorkflowAssign();
                    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    Utility objUtil = new Utility();
                    String sstatus = "Vaild";
                    objAccess.SaveMassSync(lblMasterHeaderId.Text, sstatus, lblUserId.Text, objUtil.GetIpAddress(), "C", false);

                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowValidationNewDialog();", true);
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

                _log.Error("btnMSProcess_Click", ex);
            }
        }
        else { }
    }

    private void WorkflowAssign()
    {
        //throw new NotImplementedException();
    }


    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void OtherFieldsUpdation()
    {
        string sdate = "";
        //Upload and save the file
        //Session[StaticKeys.RequestNo]   
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");

        }
        catch (Exception ex)
        {
            _log.Error("OtherFieldsUpdation", ex);

        }
        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "Start");

        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
            _log.Error("OtherFieldsUpdation1", ex);
        }

        try
        {
            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;
            //string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //try
            //{
            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03 
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            conString = string.Format(conString, excelPath);
            WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "lblRefModuleId.Text" + lblRefModuleId.Text);

            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[4] {
                 //new DataColumn("Material_Code", typeof(string)),
                //new DataColumn("Plant_Code", typeof(string)),
                //new DataColumn("Storage_Loc", typeof(string)),
                new DataColumn("View_Name", typeof(string)),
                new DataColumn("Field_Name", typeof(string)),
                //new DataColumn("Old_Value", typeof(string)),
                //new DataColumn("New_Value", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string)),
                new DataColumn("ModuleId", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;
                //dtExcelData.Columns["ModuleId"].DefaultValue = lblRefModuleId.Text;
                dtExcelData.Columns["ModuleId"].DefaultValue = "0";
                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "ModuleId Start");
                //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT distinct * FROM [" + sheet1 + "] where  Material_Code <> '123456' and Material_Code is not null and Plant_Code is not null", excel_con))

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where View_Name <> 'Basic Data'  and View_Name is not null and Field_Name is not null", excel_con))
                {
                    //oda.Fill(dtExcelData);
                    oda.Fill(dtExcelData);

                }
                excel_con.Close();
                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "ModuleId End");
                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "T_Mat_Mass_OtherWorkflow_TB Start");
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_OtherWorkflow_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        //sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        //sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        //sqlBulkCopy.ColumnMappings.Add("Storage_Loc", "Storage_Loc");
                        sqlBulkCopy.ColumnMappings.Add("View_Name", "View_Name");
                        sqlBulkCopy.ColumnMappings.Add("Field_Name", "Field_Name");
                        //sqlBulkCopy.ColumnMappings.Add("Old_Value", "Old_Value");
                        //sqlBulkCopy.ColumnMappings.Add("New_Value", "New_Value");
                        sqlBulkCopy.ColumnMappings.Add("ModuleId", "ModuleId");
                        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "T_Mat_Mass_OtherWorkflow_TB End");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }



            }

        }
        catch (Exception ex)
        { _log.Error("OtherFieldsUpdation12", ex); }
        //}
        //catch (Exception ex)
        //{
        //}

    }




    private void OtherFieldsUpdationNew()
    {
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");

        }
        catch (Exception ex)
        {

        }
        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "Start");

        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
        }

        string extension = Path.GetExtension(extensionname).ToLower();
        string excelPath = StrPath;
        string conString = string.Empty;

        StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
        int count = 0;
        try
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            System.Data.DataTable dt = Readexcel(extension, StrPath);

            if (dt.Rows.Count > 2000)
            {

                string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";
                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "msg" + msg);
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append("alert('");
                //sb.Append(msg);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }
            else
            {
                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "Insert_Excel_T_Mat_Mass_OtherWorkflow_TB");
                using (SqlCommand cmd = new SqlCommand("Insert_Excel_T_Mat_Mass_OtherWorkflow_TB"))
                {
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (dt.Rows[i]["View_Name"].ToString() != "" && dt.Rows[i]["View_Name"].ToString() != "Basic Data")
                        {
                            try
                            {
                                cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
                                cmd.Parameters.AddWithValue("@UserId", lblUserId.Text.Trim());
                                cmd.Parameters.AddWithValue("@ModuleId", "0");
                                cmd.Parameters.AddWithValue("@View_Name", dt.Rows[i]["View_Name"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@Field_Name", dt.Rows[i]["Field_Name"].ToString().Trim());

                                SqlDataReader sdr = cmd.ExecuteReader();
                                sdr.Close();

                                cmd.Parameters.RemoveAt("@Master_Header_Id");
                                cmd.Parameters.RemoveAt("@UserId");
                                cmd.Parameters.RemoveAt("@ModuleId");
                                cmd.Parameters.RemoveAt("@View_Name");
                                cmd.Parameters.RemoveAt("@Field_Name");
                                count += 1;
                            }
                            catch (Exception ex)
                            {
                                string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "msg1" + msg + "msg1_ex" + ex);
                                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                //sb.Append("<script type = 'text/javascript'>");
                                //sb.Append("window.onload=function(){");
                                //sb.Append("alert('");
                                //sb.Append(msg);
                                //sb.Append("')};");
                                //sb.Append("</script>");
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                            }
                        }
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (count == 0 || count < dt.Rows.Count)
                    {
                        string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "msg2" + msg);
                    }
                    else
                    {
                        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "count" + count.ToString());
                    }
                }


            }


            try
            {

                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                foreach (System.Diagnostics.Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    public System.Data.DataTable Readexcel(string ext, string path)
    {
        //try
        //{
        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "Readexcel Start");
        string ConStr = "";
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        string query = "SELECT * FROM [OtherFields$]";
        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "query" + query);
        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl");
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "Excel_tbl" + ds.Tables[0].Rows.Count);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "foreach");

                if (i == 0)
                {
                    WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "c");
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "ItemArray1 Start");
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                        WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "ItemArray1 End");
                    }
                }
                else
                {
                    //WriteMassMatLog("MatExtMass" + sdate + ".txt", "dr1");
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 Start");
                        dr1[j] = dr.ItemArray[j].ToString();
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 End");
                    }
                    dt.Rows.Add(dr1);

                }
                WriteMatChangeLog("MatChangeOtherMass" + sdate + ".txt", "Excel_tbl" + i);
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;
        }

        //}
        //catch (Exception ex)
        //{ _log.Error("Readexcel", ex);
        //}
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


    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <returns></returns>
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
    #endregion

    #region Document Upload

    //private void BindAttachedDocuments(string vendorId)
    //{
    //    DataAccessLayer objDb = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    DocumentUploadAccess objDoc = new DocumentUploadAccess();

    //    try
    //    {
    //        dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            grdAttachedDocs.DataSource = dstData.Tables[0].DefaultView;
    //            grdAttachedDocs.DataBind();
    //            grdAttachedDocs.Visible = true;
    //        }
    //        else
    //        {
    //            grdAttachedDocs.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objDb = null;
    //    }
    //}

    //private bool SaveDocuments(string vendorId)
    //{
    //    VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
    //    DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

    //    Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
    //    string savePath = "";
    //    string StrPath = "~/Transaction/Material/MaterialDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
    //    savePath = MapPath(StrPath);

    //    if (!Directory.Exists(savePath))
    //    {
    //        Directory.CreateDirectory(savePath);
    //    }

    //    try
    //    {
    //        HttpFileCollection fileCollection = Request.Files;
    //        for (int i = 0; i < fileCollection.Count; i++)
    //        {
    //            HttpPostedFile uploadfile = fileCollection[i];

    //            if (uploadfile.ContentLength > 0)
    //            {
    //                UploadDocument(uploadfile, StrPath, savePath);
    //            }
    //        }
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    //private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath)
    //{
    //    DocumentUpload ObjDoc = new DocumentUpload();
    //    DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
    //    bool flag = false;
    //    Utility objUtil = new Utility();

    //    if (uploadfile.ContentLength > 0)
    //    {
    //        string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

    //        string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(uploadfile.FileName);
    //        savePath = savePath + "\\" + uploadedFileName;

    //        ObjDoc.Document_Upload_Id = 0;
    //        ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
    //        ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
    //        ObjDoc.Document_Type = "";
    //        ObjDoc.Document_Name = Path.GetFileName(uploadfile.FileName);
    //        ObjDoc.Document_Path = StrPath + uploadedFileName;
    //        ObjDoc.Remarks = "";
    //        ObjDoc.IsActive = 1;
    //        ObjDoc.UserId = lblUserId.Text;
    //        ObjDoc.IPAddress = objUtil.GetIpAddress();

    //        uploadfile.SaveAs(savePath);
    //        ObjDocUploadAccess.Save(ObjDoc);

    //        flag = true;
    //    }
    //    else
    //    {
    //        flag = false;
    //        lblMsg.Text = "Error While Saving Material Details.";
    //    }

    //    return flag;
    //}

    #endregion
}