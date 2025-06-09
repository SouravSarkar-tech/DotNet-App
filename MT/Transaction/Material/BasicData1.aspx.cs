using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Transactions;
using System.Data;
using SectionConfiguration;
using Accenture.MWT.DomainObject;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Net;
using log4net;
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_BasicData1 : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    BasicDataAccess ObjBasicDataAccess = new BasicDataAccess();
    HelperAccess helperAccess = new HelperAccess();
    //Srinidhi
    BasicData objSavedBasic = new BasicData();
    //List<string> IRFDivision = new List<string> { "1", "2", "3", "4", "5", "11", "12", "13", "14", "15", "16", "17", "18", "20", "21", "26", "27", "28", "29", "31", "32", "33", "34", "35" };
    // List<string> IRFDivision = new List<string> { "1", "2", "3", "4", "5", "11", "12", "13", "14", "15", "16", "17", "18", "20", "21", "26", "27", "28", "29", "31", "32", "33", "34", "35", "36", "41", "42", "39", "53", "38", "55", "56", "57","60", "61", "62", "63", "64", "65" };
    List<string> IRFDivision = new List<string>();
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
                    if (Session[StaticKeys.SelectedModuleId] != null)
                    {
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                        //PopulateCheckBoxList();


                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();


                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                        PopuplateDropDownList();

                        //Added By Nitin R
                        //IRFDivisionListMet();
                        //Added By Nitin R
                        FillBasicData1();

                        ConfigureControl();


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

                            grdAttachedDocs.Columns[1].Visible = true;
                            file_upload.Visible = true;
                            //PROSOL_SDT16092019 Added by NR
                            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 147) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 170))

                            //PROSOL_SDT16092019 Old Commented by NR
                            // if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 147) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 138) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 170))                       
                            {
                                if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 4) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 5) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 6) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 19) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 7) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 8) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 11) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 13))
                                {
                                    txtMaterialDescription.Enabled = false;
                                    lnkAddMatDesc.Visible = true;
                                    testImg.Visible = true;
                                }
                            }
                            //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171))
                            //{
                            //    if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 24) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 25) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 13))
                            //    {
                            //        if ((Session[StaticKeys.MarketType].ToString() == "") || (Session[StaticKeys.MarketType].ToString() == "I") || (Session[StaticKeys.MarketType].ToString() == "R"))
                            //            lnkExtnsn.Visible = true;
                            //    }
                            //}
                        }
                        else
                        {
                            grdAttachedDocs.Columns[1].Visible = false;
                            file_upload.Visible = false;

                            //PROSOL_SDT16092019 old Commented by NR
                            //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 147) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 138) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 170))
                            //PROSOL_SDT16092019  Added by NR
                            //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 147) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 170))

                            {
                                if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 4) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 5) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 6) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 19) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 7) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 8) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 11) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.MaterialPlantId]) == 13))
                                {
                                    txtMaterialDescription.Enabled = true;
                                    lnkAddMatDesc.Visible = false;
                                    testImg.Visible = false;
                                }
                            }

                            //if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171))
                            //{
                            //    if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 24) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 25) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 13))
                            //    {
                            //        if ((Session[StaticKeys.MarketType].ToString() == "") || (Session[StaticKeys.MarketType].ToString() == "I") || (Session[StaticKeys.MarketType].ToString() == "R"))
                            //            lnkExtnsn.Visible = false;
                            //    }

                            //}
                        }

                        //PROSOL_SDT16092019
                        ProsolValidCheck();
                        //PROSOL_SDT16092019
                        //MSC_8300001775 Start
                        //if (HelperAccess.ReqType == "SMC")
                        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                        {
                            ConfigureControlForSChange();
                        }
                        else
                        {
                            //PROV-CCP-MM-941-23-0045 in QAMS
                            if (lblModuleId.Text == "144")
                            {
                                Ismatcommercial(true);
                            }
                            else if (lblModuleId.Text == "139" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                            {
                                Ismatcommercial(false);
                                ddlIsMatComm.SelectedValue = "1";
                                ddlIsMatComm.Enabled = true;
                            }
                            //PROV-CCP-MM-941-23-0045 in QAMS
                        }
                        //MSC_8300001775 End

                    }
                    else
                    {
                        Response.Redirect("materialmaster.aspx");
                    }
                }

            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    private void IRFDivisionListMet()
    {
        try
        {
            BasicDataAccess ObjBasicDataAccess = new BasicDataAccess();
            DataSet ds;
            ds = ObjBasicDataAccess.GetIRFDivisionList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string sValNew = Convert.ToString(row["Division_ID"]);
                    IRFDivision.Add(sValNew);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }


    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    private void ProsolValidCheck()
    {
        try
        {
            if (Convert.ToString(lblModuleId.Text) == "138")
            {

                ddlBaseUnit.Enabled = false;
                txtMaterialDescription.Enabled = false;
                txtMaterialDescription.ReadOnly = true;

                if ((Convert.ToString(Session[StaticKeys.LoggedIn_User_DeptId])) == "5")
                {
                    trProsolId.Visible = true;
                }
                else
                {
                    trProsolId.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ProsolValidCheck", ex); }
    }

    protected void ddlProductHierarchy1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ProductHierarchySetUp();
            //if (HelperAccess.ReqType == "SMC")
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                ddlProductHierarchy2.Enabled = true;
            }
            helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlProductHierarchy1_SelectedIndexChanged", ex); }
    }

    protected void ddlProductHierarchy2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (HelperAccess.ReqType == "SMC")
            if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                ddlProductHierarchy3.Enabled = true;
            }
            helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("ddlProductHierarchy2_SelectedIndexChanged", ex); }
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
                Response.Redirect("BasicData1.aspx", false);

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
                //8400000410 comment Start
                //if ((lblModuleId.Text == "162" || lblModuleId.Text == "164") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    //Response.Redirect("Sales2.aspx");
                //    Response.Redirect("Classification.aspx");
                //}
                //else if ((lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145"
                //    || lblModuleId.Text == "171") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("Sales2.aspx", false);
                //}
                //else
                //{
                //    string pageURL = btnNext.CommandArgument.ToString();
                //    Response.Redirect(pageURL);
                //}
                //8400000410 comment End
                //8400000410 add Star
                string pageURL = btnNext.CommandArgument.ToString();
               Response.Redirect(pageURL);
                //8400000410 add End
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
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

    protected void ddlBaseUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BaseUnitWarning();
        }
        catch (Exception ex)
        { _log.Error("ddlBaseUnit_SelectedIndexChanged", ex); }
    }

    protected void btnDescSave_Click(object sender, EventArgs e)
    {
        try
        {
            string materialDesc = ucMatDescription.Save();

            if (!(materialDesc == ""))
            {
                txtMaterialDescription.Text = materialDesc;
                pnlMatDesc.Visible = false;
                trMatDesc.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("btnDescSave_Click", ex); }

    }

    protected void lnkAddMatDesc_Click(object sender, EventArgs e)
    {
        try
        {
            if (pnlMatDesc.Visible == true)
            {
                ShowMatDesc(false);
            }
            else
            {
                ShowMatDesc(true);
            }
        }
        catch (Exception ex)
        { _log.Error("lnkAddMatDesc_Click", ex); }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetExtnsnLinkVisibility();
        }
        catch (Exception ex)
        { _log.Error("ddlDivision_SelectedIndexChanged", ex); }
    }

    /// <summary>
    /// CTRL_SUB_SDT06062019, Desc : Controll Substance , Change By : Nitin R
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlIsContainsChemical_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlIsContainsChemical.SelectedValue == "1")
            {
                Session[StaticKeys.ctrlsubfieldval] = "1";
            }
            else
            {
                Session[StaticKeys.ctrlsubfieldval] = "0";
            }
        }
        catch (Exception ex)
        { _log.Error("ddlIsContainsChemical_SelectedIndexChanged", ex); }
    }


    #endregion

    #region Private Funtions

    private void ProductHierarchySetUp()
    {
        bool flg = true;
        try
        {
            if (ddlProductHierarchy1.SelectedValue == "")
                flg = false;

            lableddlProductHierarchy2.Visible = flg;
            lableddlProductHierarchy3.Visible = flg;

            reqddlProductHierarchy2.Visible = flg;
            reqddlProductHierarchy3.Visible = flg;
        }
        catch (Exception ex)
        { _log.Error("ProductHierarchySetUp", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
            helperAccess.PopuplateDropDownList(ddlMaterialType, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialType'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlIndustrySector, "pr_GetDropDownListByControlNameModuleType 'M','ddlIndustrySector'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlBaseUnit, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','B1','" + lblBasicDataId.Text + "'", "Division_Name", "Division_Id", "");
            //MSC_8300001775 Start
            if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                if (lblModuleId.Text == "138" || lblModuleId.Text == "147")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'EN,SPN'", "LookUp_Desc", "LookUp_Code", "");
                else if (lblModuleId.Text == "163")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'EN,MS,PS,PM,RM'", "LookUp_Desc", "LookUp_Code", "");
                else if (lblModuleId.Text == "162")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'RM,SPN'", "LookUp_Desc", "LookUp_Code", "");
                else if (lblModuleId.Text == "170")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'PS,CP'", "LookUp_Desc", "LookUp_Code", "");
                else if (lblModuleId.Text == "144")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'SF5107,SF5108,SF5109,SF5110,SF5111,SFRM'", "LookUp_Desc", "LookUp_Code", "");
                else if (lblModuleId.Text == "139")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'FG,F0'", "LookUp_Desc", "LookUp_Code", "");
                else if (lblModuleId.Text == "171")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'BF,BS,F0,FG,FP,SPN,SF5107,SF5108,SF5109,SF5110,SF5111,SFRM'", "LookUp_Desc", "LookUp_Code", "");
                //Promotion code start   
                else if (lblModuleId.Text == "195")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'EN'", "LookUp_Desc", "LookUp_Code", "");
                //Promotion code End
                else if (lblModuleId.Text == "145")
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetMaterialGrp 'BS,F0,FG'", "LookUp_Desc", "LookUp_Code", "");
                else
                    helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup'", "LookUp_Desc", "LookUp_Code", "");

            }
            else
            {
                helperAccess.PopuplateDropDownList(ddlMaterialGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup'", "LookUp_Desc", "LookUp_Code", "");
            }
            //MSC_8300001775 End 
            helperAccess.PopuplateDropDownList(ddlCrossPlantMaterialStatus, "pr_GetDropDownListByControlNameModuleType 'M','ddlCrossPlantMaterialStatus'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlGenItemCategoryGrp, "pr_GetDropDownListByControlNameModuleType 'M','ddlGenItemCategoryGrp'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlCategoryIA, "pr_GetDropDownListByControlNameModuleType 'M','ddlCategoryIA'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlLaboratory, "pr_GetDropDownListByControlNameModuleType 'M','ddlLaboratory'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlExternalMaterialGroup, "pr_GetDropDownListByControlNameModuleType 'M','ddlExternalMaterialGroup'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy1, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','1'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlVolumeUnit, "pr_GetDropDownListByControlNameModuleTypeUnitDimension 'M','ddlBaseUnitDim','" + lblSectionId.Text + "','VOLUME'", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlWeightUnit, "pr_GetDropDownListByControlNameModuleTypeUnitDimension 'M','ddlBaseUnitDim','" + lblSectionId.Text + "','MASS  '", "LookUp_Desc", "LookUp_Code", "");

            helperAccess.PopuplateDropDownList(ddlMatlGrpPackMatl, "pr_GetDropDownListByControlNameModuleType 'M','ddlMatlGrpPackMatl'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    /// <summary>
    /// /MSC_8300001775
    /// </summary>
    /// <returns></returns>
    private bool ValDatValid()
    {
        Utility objUtil = new Utility();
        bool flg = false;
        try
        {
            if ((ddlCrossPlantMaterialStatus.SelectedValue == "0" && ddlCrossPlantMaterialStatus.SelectedValue == "")
               && ((objUtil.GetDDMMYYYYNew(txtValidFrom.Text) != "01/01/1900")
               && (objUtil.GetDDMMYYYYNew(txtValidFrom.Text) != "1900-01-01"))
               && (lblModuleId.Text == "162" || lblModuleId.Text == "164")
               )
            {

                lblMsg.Text = "Please select Cross Plant Material Status.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else if ((ddlCrossPlantMaterialStatus.SelectedValue == "0" && ddlCrossPlantMaterialStatus.SelectedValue == "")
               && ((objUtil.GetDDMMYYYYNew(txtValidFrom.Text) == "01/01/1900")
               && (objUtil.GetDDMMYYYYNew(txtValidFrom.Text) == "1900-01-01"))
               && (lblModuleId.Text == "138" || lblModuleId.Text == "144" || lblModuleId.Text == "145"
               || lblModuleId.Text == "170" || lblModuleId.Text == "171" || lblModuleId.Text == "139" || lblModuleId.Text == "195")
               )
            {

                lblMsg.Text = "Cross Plant Material Status and Valid From can not be blank.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else
            {
                flg = true;
                //lblMsg.Text = Messages.GetMessage(-1);
                //pnlMsg.CssClass = "error";
                //pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ValDatValid", ex);
            //throw ex;
        }
        return flg;
    }

    private bool Save()
    {
        bool flg = false;
        if (ValDatValid())
        {
            MaterialMasterAccess materialAccess = new MaterialMasterAccess();
            try
            {
                BasicData ObjBasicData = GetControlsValue();
                objSavedBasic = GetBasicData1();
                //Srinidhi
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                {
                    if (objSavedBasic.Mat_Basic_Data1_Id > 0)
                    {
                        CheckIfChanges(ObjBasicData, objSavedBasic);
                    }
                }
                //Srinidhi End
                if ((ObjBasicDataAccess.SaveBasicData(ObjBasicData) > 0) && (SaveDocuments(lblMasterHeaderId.Text)))
                {
                    //MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                    {
                        CheckIfChangesLog(ObjBasicData, objSavedBasic);
                    }
                    //MSC_8300001775

                    //Session[StaticKeys.MaterialNo] = txtMaterialNo.Text;
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                    Session[StaticKeys.SelectedDivision] = ObjBasicData.Division;
                    flg = true;
                    ////MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    //{
                    //    CheckIfChangesLog(ObjBasicData, objSavedBasic);
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
            catch (Exception ex)
            {
                _log.Error("Save", ex);
                //throw ex;
            }
            return flg;
        }
        return flg;
    }

    private void CheckIfChanges(BasicData NewBasicData, BasicData oldBasicData)
    {
        try
        {
            if (NewBasicData.Mat_Basic_Data1_Id > 0 && oldBasicData.Mat_Basic_Data1_Id > 0)
            {
                if (NewBasicData.Material_Type != oldBasicData.Material_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Type</td> <td>" + oldBasicData.Material_Type + "</td><td>" + NewBasicData.Material_Type + "</td></tr>";
                if (NewBasicData.Industry_Sector != oldBasicData.Industry_Sector)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Industry Sector</td> <td>" + oldBasicData.Industry_Sector + "</td><td>" + NewBasicData.Industry_Sector + "</td></tr>";
                if (NewBasicData.Base_Unit_Of_Measure != oldBasicData.Base_Unit_Of_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Base Unit Of Measure</td> <td>" + oldBasicData.Base_Unit_Of_Measure + "</td><td>" + NewBasicData.Base_Unit_Of_Measure + "</td></tr>";
                if (NewBasicData.Material_Short_Description != oldBasicData.Material_Short_Description)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Description</td> <td>" + oldBasicData.Material_Short_Description + "</td><td>" + NewBasicData.Material_Short_Description + "</td></tr>";
                if (NewBasicData.Material_Group != oldBasicData.Material_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Group</td> <td>" + oldBasicData.Material_Group + "</td><td>" + NewBasicData.Material_Group + "</td></tr>";
                if (NewBasicData.Old_Material_Number != oldBasicData.Old_Material_Number)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Old Material Number</td> <td>" + oldBasicData.Old_Material_Number + "</td><td>" + NewBasicData.Old_Material_Number + "</td></tr>";
                if (NewBasicData.External_Material_Group != oldBasicData.External_Material_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>External Material Group</td> <td>" + oldBasicData.External_Material_Group + "</td><td>" + NewBasicData.External_Material_Group + "</td></tr>";
                if (NewBasicData.Lab_Design_Office != oldBasicData.Lab_Design_Office)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Laboratory Design Office</td> <td>" + oldBasicData.Lab_Design_Office + "</td><td>" + NewBasicData.Lab_Design_Office + "</td></tr>";
                if (NewBasicData.Division != oldBasicData.Division)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Division</td> <td>" + oldBasicData.Division + "</td><td>" + NewBasicData.Division + "</td></tr>";
                if (NewBasicData.Product_Hierarchy != oldBasicData.Product_Hierarchy)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Product Hierarchy</td> <td>" + oldBasicData.Product_Hierarchy + "</td><td>" + NewBasicData.Product_Hierarchy + "</td></tr>";
                if (NewBasicData.Cross_Plant_Material_Status != oldBasicData.Cross_Plant_Material_Status)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Cross Plant Material Status</td> <td>" + oldBasicData.Cross_Plant_Material_Status + "</td><td>" + NewBasicData.Cross_Plant_Material_Status + "</td></tr>";
                if (NewBasicData.Valid_From != oldBasicData.Valid_From && oldBasicData.Valid_From != "1900-01-01" && NewBasicData.Valid_From != "01/01/1900")
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Valid From </td> <td>" + oldBasicData.Valid_From + "</td><td>" + NewBasicData.Valid_From + "</td></tr>";
                if (NewBasicData.Gen_Item_Category_Grp != oldBasicData.Gen_Item_Category_Grp)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>General Item Category Group</td> <td>" + oldBasicData.Gen_Item_Category_Grp + "</td><td>" + NewBasicData.Gen_Item_Category_Grp + "</td></tr>";
                if (NewBasicData.Prod_Inspect_Memo != oldBasicData.Prod_Inspect_Memo)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Production/Inspection Memo</td> <td>" + oldBasicData.Prod_Inspect_Memo + "</td><td>" + NewBasicData.Prod_Inspect_Memo + "</td></tr>";
                if (NewBasicData.Gross_Weight != oldBasicData.Gross_Weight)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Gross Weight</td> <td>" + oldBasicData.Gross_Weight + "</td><td>" + NewBasicData.Gross_Weight + "</td></tr>";
                if (NewBasicData.Net_Weight != oldBasicData.Net_Weight)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Net Weight</td> <td>" + oldBasicData.Net_Weight + "</td><td>" + NewBasicData.Net_Weight + "</td></tr>";
                if (NewBasicData.Weight_Unit != oldBasicData.Weight_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Weight Unit</td> <td>" + oldBasicData.Weight_Unit + "</td><td>" + NewBasicData.Weight_Unit + "</td></tr>";
                if (NewBasicData.Volume != oldBasicData.Volume)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Volume</td> <td>" + oldBasicData.Volume + "</td><td>" + NewBasicData.Volume + "</td></tr>";
                if (NewBasicData.Volume_Unit != oldBasicData.Volume_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Volume Unit</td> <td>" + oldBasicData.Volume_Unit + "</td><td>" + NewBasicData.Volume_Unit + "</td></tr>";
                if (NewBasicData.InterNational_Article_No != oldBasicData.InterNational_Article_No)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>International Article Number</td> <td>" + oldBasicData.InterNational_Article_No + "</td><td>" + NewBasicData.InterNational_Article_No + "</td></tr>";
                if (NewBasicData.Category_InterN_Article_No != oldBasicData.Category_InterN_Article_No)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Category of International Article Number</td> <td>" + oldBasicData.Category_InterN_Article_No + "</td><td>" + NewBasicData.Category_InterN_Article_No + "</td></tr>";
                if (NewBasicData.Material_Grp_Pack_Mtl != oldBasicData.Material_Grp_Pack_Mtl)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Matl Material Grp Pack Mtl</td> <td>" + oldBasicData.Material_Grp_Pack_Mtl + "</td><td>" + NewBasicData.Material_Grp_Pack_Mtl + "</td></tr>";
                if (NewBasicData.Reason_For_Creation != oldBasicData.Reason_For_Creation)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Reason For Creation</td> <td>" + oldBasicData.Reason_For_Creation + "</td><td>" + NewBasicData.Reason_For_Creation + "</td></tr>";
                if (NewBasicData.Remarks != oldBasicData.Remarks)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldBasicData.Remarks + "</td><td>" + NewBasicData.Remarks + "</td></tr>";
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
    /// <param name="NewBasicData"></param>
    /// <param name="oldBasicData"></param>
    private void CheckIfChangesLog(BasicData NewBasicData, BasicData oldBasicData)
    {
        Utility objUtil = new Utility();
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewBasicData.Mat_Basic_Data1_Id >= 0 && oldBasicData.Mat_Basic_Data1_Id >= 0)
            {
                if (NewBasicData.Material_Type != oldBasicData.Material_Type)
                    _items.Add(new SMChange { colFieldName = 87, colOldVal = oldBasicData.Material_Type, colNewVal = NewBasicData.Material_Type });
                if (NewBasicData.Industry_Sector != oldBasicData.Industry_Sector)
                    _items.Add(new SMChange { colFieldName = 1128, colOldVal = oldBasicData.Industry_Sector, colNewVal = NewBasicData.Industry_Sector });
                if (NewBasicData.Base_Unit_Of_Measure != oldBasicData.Base_Unit_Of_Measure)
                    _items.Add(new SMChange { colFieldName = 8, colOldVal = oldBasicData.Base_Unit_Of_Measure, colNewVal = NewBasicData.Base_Unit_Of_Measure });
                if (NewBasicData.Material_Short_Description != oldBasicData.Material_Short_Description)
                    _items.Add(new SMChange { colFieldName = 9, colOldVal = oldBasicData.Material_Short_Description, colNewVal = NewBasicData.Material_Short_Description });
                if (NewBasicData.Material_Group != oldBasicData.Material_Group)
                    _items.Add(new SMChange { colFieldName = 10, colOldVal = oldBasicData.Material_Group, colNewVal = NewBasicData.Material_Group });
                if (NewBasicData.Old_Material_Number != oldBasicData.Old_Material_Number)
                    _items.Add(new SMChange { colFieldName = 11, colOldVal = oldBasicData.Old_Material_Number, colNewVal = NewBasicData.Old_Material_Number });
                if (NewBasicData.External_Material_Group != oldBasicData.External_Material_Group)
                    _items.Add(new SMChange { colFieldName = 1129, colOldVal = oldBasicData.External_Material_Group, colNewVal = NewBasicData.External_Material_Group });
                if (NewBasicData.Lab_Design_Office != oldBasicData.Lab_Design_Office)
                    _items.Add(new SMChange { colFieldName = 12, colOldVal = oldBasicData.Lab_Design_Office, colNewVal = NewBasicData.Lab_Design_Office });
                if (NewBasicData.Division != oldBasicData.Division)
                    _items.Add(new SMChange { colFieldName = 19, colOldVal = oldBasicData.Division, colNewVal = NewBasicData.Division });
                if (NewBasicData.Product_Hierarchy != oldBasicData.Product_Hierarchy)
                    _items.Add(new SMChange { colFieldName = 1130, colOldVal = oldBasicData.Product_Hierarchy, colNewVal = NewBasicData.Product_Hierarchy });
                if (NewBasicData.Cross_Plant_Material_Status != oldBasicData.Cross_Plant_Material_Status)
                    _items.Add(new SMChange { colFieldName = 1133, colOldVal = oldBasicData.Cross_Plant_Material_Status, colNewVal = NewBasicData.Cross_Plant_Material_Status });

                if (NewBasicData.Gen_Item_Category_Grp != oldBasicData.Gen_Item_Category_Grp)
                    _items.Add(new SMChange { colFieldName = 1135, colOldVal = oldBasicData.Gen_Item_Category_Grp, colNewVal = NewBasicData.Gen_Item_Category_Grp });
                if (NewBasicData.Prod_Inspect_Memo != oldBasicData.Prod_Inspect_Memo)
                    _items.Add(new SMChange { colFieldName = 13, colOldVal = oldBasicData.Prod_Inspect_Memo, colNewVal = NewBasicData.Prod_Inspect_Memo });
                if (NewBasicData.Gross_Weight != oldBasicData.Gross_Weight)
                    _items.Add(new SMChange { colFieldName = 14, colOldVal = oldBasicData.Gross_Weight, colNewVal = NewBasicData.Gross_Weight });
                if (NewBasicData.Net_Weight != oldBasicData.Net_Weight)
                    _items.Add(new SMChange { colFieldName = 15, colOldVal = oldBasicData.Net_Weight, colNewVal = NewBasicData.Net_Weight });
                if (NewBasicData.Weight_Unit != oldBasicData.Weight_Unit)
                    _items.Add(new SMChange { colFieldName = 16, colOldVal = oldBasicData.Weight_Unit, colNewVal = NewBasicData.Weight_Unit });
                if (NewBasicData.Volume != oldBasicData.Volume)
                    _items.Add(new SMChange { colFieldName = 17, colOldVal = oldBasicData.Volume, colNewVal = NewBasicData.Volume });
                if (NewBasicData.Volume_Unit != oldBasicData.Volume_Unit)
                    _items.Add(new SMChange { colFieldName = 18, colOldVal = oldBasicData.Volume_Unit, colNewVal = NewBasicData.Volume_Unit });
                if (NewBasicData.InterNational_Article_No != oldBasicData.InterNational_Article_No)
                    _items.Add(new SMChange { colFieldName = 20, colOldVal = oldBasicData.InterNational_Article_No, colNewVal = NewBasicData.InterNational_Article_No });
                if (NewBasicData.Category_InterN_Article_No != oldBasicData.Category_InterN_Article_No)
                    _items.Add(new SMChange { colFieldName = 21, colOldVal = oldBasicData.Category_InterN_Article_No, colNewVal = NewBasicData.Category_InterN_Article_No });
                if (NewBasicData.Material_Grp_Pack_Mtl != oldBasicData.Material_Grp_Pack_Mtl)
                    _items.Add(new SMChange { colFieldName = 1136, colOldVal = oldBasicData.Material_Grp_Pack_Mtl, colNewVal = NewBasicData.Material_Grp_Pack_Mtl });
                //if (NewBasicData.Reason_For_Creation != oldBasicData.Reason_For_Creation)
                //    _items.Add(new SMChange { colFieldName = 1407, colOldVal = oldBasicData.Reason_For_Creation, colNewVal = NewBasicData.Reason_For_Creation });
                ////if (NewBasicData.Valid_From != oldBasicData.Valid_From)
                if ((objUtil.GetDDMMYYYYNew(NewBasicData.Valid_From) != oldBasicData.Valid_From)
                    && (objUtil.GetDDMMYYYYNew(NewBasicData.Valid_From) != "01/01/1900")
                    && (NewBasicData.Valid_From != "1900-01-01")
                        && (oldBasicData.Valid_From != "01/01/1900") && (oldBasicData.Valid_From != "1900-01-01")
                        )
                {
                    //if (NewBasicData.Valid_From != "01/01/1900" || NewBasicData.Valid_From != "1900-01-01"
                    //    || oldBasicData.Valid_From != "01/01/1900" || oldBasicData.Valid_From != "900-01-01" ||
                    //     NewBasicData.Valid_From != "" || oldBasicData.Valid_From != "")
                    //{
                    _items.Add(new SMChange { colFieldName = 1134, colOldVal = oldBasicData.Valid_From, colNewVal = objUtil.GetDDMMYYYYNew(NewBasicData.Valid_From) });
                    //}
                }
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
                ChangeSMatID1 = helperAccess.MaterialChange("3", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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
            _log.Error("CheckIfChangesLog", ex);
            //throw ex;
        }

    }

    private BasicData GetBasicData1()
    {
        return ObjBasicDataAccess.GetBasicData1(lblMasterHeaderId.Text);
    }

    private void FillBasicData1()
    {
        BasicData ObjBasicData = GetBasicData1();
        try
        {
            if (ObjBasicData.Mat_Basic_Data1_Id > 0)
            {
                lblBasicDataId.Text = ObjBasicData.Mat_Basic_Data1_Id.ToString();
                txtMaterialNo.Text = ObjBasicData.Material_Number;

                ddlMaterialType.SelectedValue = ObjBasicData.Material_Type;
                ddlIndustrySector.SelectedValue = ObjBasicData.Industry_Sector;
                ddlBaseUnit.Text = ObjBasicData.Base_Unit_Of_Measure;
                txtMaterialDescription.Text = ObjBasicData.Material_Short_Description;
                ddlMaterialGroup.Text = ObjBasicData.Material_Group;
                txtOldMaterialNo.Text = ObjBasicData.Old_Material_Number;
                ddlExternalMaterialGroup.SelectedValue = ObjBasicData.External_Material_Group;
                ddlLaboratory.Text = ObjBasicData.Lab_Design_Office;
                ddlDivision.Text = ObjBasicData.Division;
                Session[StaticKeys.SelectedDivision] = ObjBasicData.Division;

                foreach (ListItem lst in ddlProductHierarchy1.Items)
                {
                    if (lst.Value.Contains(ObjBasicData.Product_Hierarchy.Length > 4 ? ObjBasicData.Product_Hierarchy.Substring(0, 5) : ObjBasicData.Product_Hierarchy))
                    {
                        ddlProductHierarchy1.SelectedValue = lst.Value;
                        break;
                    }
                }

                ProductHierarchySetUp();

                helperAccess.PopuplateDropDownList(ddlProductHierarchy2, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','2','" + ddlProductHierarchy1.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                foreach (ListItem lst in ddlProductHierarchy2.Items)
                {
                    if (lst.Value.Contains(ObjBasicData.Product_Hierarchy.Length > 5 ? (ObjBasicData.Product_Hierarchy.Length > 9 ? ObjBasicData.Product_Hierarchy.Substring(0, 10) : ObjBasicData.Product_Hierarchy.Substring(0, ObjBasicData.Product_Hierarchy.Length)) : ""))
                    {
                        ddlProductHierarchy2.SelectedValue = lst.Value;
                        break;
                    }
                }

                //ddlProductHierarchy2.SelectedValue = ObjBasicData.Product_Hierarchy.Length > 5 ? (ObjBasicData.Product_Hierarchy.Length > 9 ? ObjBasicData.Product_Hierarchy.Substring(0, 10) : ObjBasicData.Product_Hierarchy.Substring(0, ObjBasicData.Product_Hierarchy.Length)) : "";

                helperAccess.PopuplateDropDownList(ddlProductHierarchy3, "pr_GetDropDownListByControlNameModuleType 'M','ddlProductHierarchy','" + lblSectionId.Text + "','3','" + ddlProductHierarchy2.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");

                foreach (ListItem lst in ddlProductHierarchy3.Items)
                {
                    if (lst.Value.Contains(ObjBasicData.Product_Hierarchy.Length > 10 ? ObjBasicData.Product_Hierarchy : ""))
                    {
                        ddlProductHierarchy3.SelectedValue = lst.Value;
                        break;
                    }
                }

                //ddlProductHierarchy3.SelectedValue = ObjBasicData.Product_Hierarchy.Length > 10 ? ObjBasicData.Product_Hierarchy : "";

                ddlCrossPlantMaterialStatus.SelectedValue = ObjBasicData.Cross_Plant_Material_Status;
                txtValidFrom.Text = ObjBasicData.Valid_From;
                ddlGenItemCategoryGrp.SelectedValue = ObjBasicData.Gen_Item_Category_Grp;
                txtProduction.Text = ObjBasicData.Prod_Inspect_Memo;
                txtGrossWeight.Text = ObjBasicData.Gross_Weight;
                txtNetWeight.Text = ObjBasicData.Net_Weight;
                ddlWeightUnit.Text = ObjBasicData.Weight_Unit;
                txtVolume.Text = ObjBasicData.Volume;
                ddlVolumeUnit.Text = ObjBasicData.Volume_Unit;
                txtInternationalANo.Text = ObjBasicData.InterNational_Article_No;
                ddlCategoryIA.Text = ObjBasicData.Category_InterN_Article_No;
                ddlMatlGrpPackMatl.SelectedValue = ObjBasicData.Material_Grp_Pack_Mtl;

                if (ObjBasicData.Reason_For_Creation == "OTH")
                    ObjBasicData.Reason_For_Creation = "";

                ddlReason.SelectedValue = ObjBasicData.Reason_For_Creation;
                txtRemarks.Text = ObjBasicData.Remarks;


                //CTRL_SUB_SDT06062019, Desc: Controll Substance, Change By: Nitin R
                CheckControlSub(ObjBasicData.sControlSubYN, lblModuleId.Text, ObjBasicData.ReqCreatedOn);
                //CheckControlSub(ObjBasicData.sControlSubYN, lblModuleId.Text);
                //CTRL_SUB_EDT06062019, Desc: Controll Substance, Change By: Nitin R
                //DT05072023_BG_Type
                ddlBGWCF.SelectedValue = ObjBasicData.sBGWCF;
                //DT05072023_BG_Type

                //PROV-CCP-MM-941-23-0045 in QAMS
                ddlIsMatComm.SelectedValue = ObjBasicData.sIsMatComm;
                //PROV-CCP-MM-941-23-0045 in QAMS
            }
            else
            {
                lblBasicDataId.Text = "0";
                ddlMaterialType.SelectedValue = ObjBasicData.Material_Type;

                ddlIndustrySector.SelectedValue = "P";
                //Start :Commented and added by nitish rao 19.04.2019 for production issue : divisaion dropdown is blank by default
                //ddlDivision.SelectedValue = "99";
                ddlDivision.SelectedValue = "30";
                //End :Commented and added by nitish rao 19.04.2019 for production issue : divisaion dropdown is blank by default
                if (lblModuleId.Text == "163")
                {
                    //MSC_8300001775 Comment Start
                    //ddlMaterialGroup.SelectedValue = "MS001    ";
                    //MSC_8300001775 Comment End
                    //MSC_8300001775 added  Start
                    ddlMaterialGroup.SelectedValue = "MS001";
                    //MSC_8300001775 added  Start
                }

                if (lblModuleId.Text == "162" || lblModuleId.Text == "163" || lblModuleId.Text == "164")
                {
                    ddlGenItemCategoryGrp.SelectedValue = "NORM";
                    ddlWeightUnit.SelectedValue = "KG ";
                    //MSC_8300001775 Comment Start
                    //ddlWeightUnit.SelectedValue = "KG ";
                    //MSC_8300001775 Comment End
                    //MSC_8300001775 added  Start
                    ddlWeightUnit.SelectedValue = "KG";
                    //MSC_8300001775 added  Start
                }
                // else if (lblModuleId.Text == "164")
                //  {
                //   ddlGenItemCategoryGrp.SelectedValue = "VERP";

                //    //MSC_8300001775 Comment Start
                //   //ddlWeightUnit.SelectedValue = "KG ";
                //  //MSC_8300001775 Comment End
                //  //MSC_8300001775 added  Start
                //   ddlWeightUnit.SelectedValue = "KG";
                //  //MSC_8300001775 added  Start
                //}
                //Promotion code start
                //else if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
                else if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                //Promotion code End
                {
                    //MSC_8300001775 Comment Start
                    //ddlWeightUnit.SelectedValue = "KG ";
                    //MSC_8300001775 Comment End
                    //MSC_8300001775 added  Start
                    ddlWeightUnit.SelectedValue = "KG";
                    //MSC_8300001775 added  Start
                    ddlGenItemCategoryGrp.SelectedValue = "NORM";
                    ddlCrossPlantMaterialStatus.SelectedValue = "Z1";
                    txtValidFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //Promotion code start
                    //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "171")
                    if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
                    //Promotion code End
                    {
                        txtGrossWeight.Text = "1";
                        txtNetWeight.Text = "1";
                    }
                }


                ProductHierarchySetUp();

                //CTRL_SUB_SDT06062019, Desc: Controll Substance, Change By: Nitin R
                CheckControlSub("Check", lblModuleId.Text, String.Empty);
                ////CheckControlSub("Check", lblModuleId.Text);
                //CTRL_SUB_EDT06062019, Desc: Controll Substance, Change By: Nitin R
            }

            ddlMaterialType.Enabled = false;
            ddlIndustrySector.Enabled = false;

            //Promotion code start
            //if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171")
            if (lblModuleId.Text == "139" || lblModuleId.Text == "144" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
            //Promotion code end
            {
                ddlDivision.Enabled = true;
                ddlCrossPlantMaterialStatus.Enabled = false;
                txtValidFrom.Enabled = false;
                ddlGenItemCategoryGrp.Enabled = false;
                //Depot Extension
                SetExtnsnLinkVisibility();

            }
            else
            {
                ddlDivision.Enabled = false;
            }

            if (lblModuleId.Text == "162" || lblModuleId.Text == "163" || lblModuleId.Text == "164")
            {
                ddlGenItemCategoryGrp.Enabled = false;
            }

            //DT05072023_BG_Type
            if (lblModuleId.Text == "139" || lblModuleId.Text == "145" || lblModuleId.Text == "171" || lblModuleId.Text == "195")
            {
                SetExtnsnLinkVisibility();
            }
            else
            {

                labelddlBGWCF.Enabled = false;
                lblddlBGWCF.Enabled = false;
                rfvddlBGWCF.Enabled = false;
                ddlBGWCF.Enabled = false;
            }
            //DT05072023_BG_Type
            Session[StaticKeys.SelectedModulePlantGrp] = ObjBasicData.ModulePlantGroupCode;
            BindAttachedDocuments(lblMasterHeaderId.Text);
            //txtVolume.Text = Session[StaticKeys.MaterialPlantId].ToString();
            BaseUnitWarning();
        }
        catch (Exception ex)
        {
            _log.Error("GetBasicData1", ex);
            //throw ex;
        }
    }

    /// <summary>
    ///  //CTRL_SUB_SDT06062019, Desc: Controll Substance, Change By: Nitin R
    /// </summary>
    private void CheckControlSub(string psControlSubYN, string SelectedModuleId, string sValid_From)
    {
        try
        {
            string deptIds = Session[StaticKeys.LoggedIn_User_DeptId].ToString();

            DateTime reqCreateDate;
            var ctrlSubLiveDate = DateTime.ParseExact(Convert.ToString(ConfigurationManager.AppSettings["ctrlSubLiveDate"]), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //var ctrlSubLiveDate = DateTime.ParseExact(Convert.ToString(ConfigurationManager.AppSettings["ctrlSubLiveDate"]), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //string splitdt = Convert.ToString(Session[StaticKeys.RequestNo]);



            if (sValid_From != "")
            {
                reqCreateDate = DateTime.ParseExact(sValid_From, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //reqCreateDate = DateTime.ParseExact(sValid_From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                reqCreateDate = DateTime.Today;
            }

            if (ctrlSubLiveDate < reqCreateDate)
            {


                if (psControlSubYN == "Check")
                {
                    //if (SelectedModuleId == "139" || SelectedModuleId == "144" || SelectedModuleId == "145" || SelectedModuleId == "162")
                    if (SelectedModuleId == "162")
                    {
                        //CTRL_SUB_SDT18112019
                        CtrlsubValmsg.Attributes.Add("style", "");
                        //CTRL_SUB_SDT18112019
                        ddlIsContainsChemical.SelectedValue = "1";
                        reqddlIsContainsChemical.Visible = true;
                        lableddlIsContainsChemical.Visible = true;

                        if (deptIds == Convert.ToString(ConfigurationManager.AppSettings["DepIDRDM"]))
                        {
                            ddlIsContainsChemical.Enabled = true;
                        }
                        else
                        {
                            ddlIsContainsChemical.Enabled = false;
                        }


                        Session[StaticKeys.ctrlsubfieldval] = "1";

                    }
                    else
                    {
                        ddlIsContainsChemical.SelectedValue = "";
                        //ddlIsContainsChemical.Enabled = true;
                        ddlIsContainsChemical.Enabled = false;
                        Session[StaticKeys.ctrlsubfieldval] = "0";

                        reqddlIsContainsChemical.Visible = false;
                        lableddlIsContainsChemical.Visible = false;
                    }
                }
                else
                {

                    // if (SelectedModuleId == "139" || SelectedModuleId == "144" || SelectedModuleId == "145" || SelectedModuleId == "162")
                    if (SelectedModuleId == "162")
                    {
                        //CTRL_SUB_SDT18112019
                        CtrlsubValmsg.Attributes.Add("style", "");
                        //CTRL_SUB_SDT18112019

                        ddlIsContainsChemical.SelectedValue = psControlSubYN;
                        reqddlIsContainsChemical.Visible = true;
                        lableddlIsContainsChemical.Visible = true;
                        if (deptIds == Convert.ToString(ConfigurationManager.AppSettings["DepIDRDM"]))
                        {
                            ddlIsContainsChemical.Enabled = true;
                        }
                        else
                        {
                            ddlIsContainsChemical.Enabled = false;
                        }
                        if (psControlSubYN == "1")
                        {
                            Session[StaticKeys.ctrlsubfieldval] = "1";
                        }
                        else
                        {
                            Session[StaticKeys.ctrlsubfieldval] = "0";
                        }
                    }
                    else
                    {
                        //CtrlsubValmsg.Attributes.Add("style", "display:block;");
                        ddlIsContainsChemical.SelectedValue = psControlSubYN;
                        //reqddlIsContainsChemical.Visible = false;
                        //lableddlIsContainsChemical.Visible = false;
                        //ddlIsContainsChemical.Enabled = true;
                        ddlIsContainsChemical.Enabled = false;

                        if (psControlSubYN == "1")
                        {
                            reqddlIsContainsChemical.Visible = true;
                            lableddlIsContainsChemical.Visible = true;
                            ddlIsContainsChemical.Enabled = false;
                            Session[StaticKeys.ctrlsubfieldval] = "1";
                        }
                        else
                        {
                            reqddlIsContainsChemical.Visible = false;
                            lableddlIsContainsChemical.Visible = false;
                            Session[StaticKeys.ctrlsubfieldval] = "0";
                        }
                    }
                }
            }
            else
            {
                //CTRL_SUB_SDT18112019
                CtrlsubValmsg.Attributes.Add("style", "display:none;");
                //CTRL_SUB_SDT18112019
                reqddlIsContainsChemical.Visible = false;
                lableddlIsContainsChemical.Visible = false;
                ddlIsContainsChemical.Enabled = false;
                Session[StaticKeys.ctrlsubfieldval] = "0";
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckControlSub", ex);
        }
    }
    //CTRL_SUB_EDT06062019, Desc: Controll Substance, Change By: Nitin R

    private BasicData GetControlsValue()
    {
        BasicData ObjBasicData = new BasicData();
        Utility objUtil = new Utility();
        try
        {
            ObjBasicData.Mat_Basic_Data1_Id = Convert.ToInt32(lblBasicDataId.Text);
            ObjBasicData.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

            Session[StaticKeys.MaterialNo] = (txtMaterialNo.Text == "" ? "New Request" : txtMaterialNo.Text) + " - " + txtMaterialDescription.Text;

            ObjBasicData.Material_Number = txtMaterialNo.Text;
            ObjBasicData.Material_Type = ddlMaterialType.SelectedValue;
            ObjBasicData.Industry_Sector = ddlIndustrySector.SelectedValue;
            ObjBasicData.Base_Unit_Of_Measure = ddlBaseUnit.SelectedValue;
            ObjBasicData.Material_Short_Description = txtMaterialDescription.Text;
            ObjBasicData.Material_Group = ddlMaterialGroup.SelectedValue;
            ObjBasicData.Old_Material_Number = txtOldMaterialNo.Text;
            ObjBasicData.External_Material_Group = ddlExternalMaterialGroup.SelectedValue;
            ObjBasicData.Lab_Design_Office = ddlLaboratory.SelectedValue;
            ObjBasicData.Division = ddlDivision.SelectedValue;
            ObjBasicData.Product_Hierarchy = ddlProductHierarchy3.SelectedValue != "" ? ddlProductHierarchy3.SelectedValue : (ddlProductHierarchy2.SelectedValue != "" ? ddlProductHierarchy2.SelectedValue : ddlProductHierarchy1.SelectedValue);
            ObjBasicData.Cross_Plant_Material_Status = ddlCrossPlantMaterialStatus.SelectedValue;
            ObjBasicData.Valid_From = objUtil.GetYYYYMMDD(txtValidFrom.Text);
            ObjBasicData.Gen_Item_Category_Grp = ddlGenItemCategoryGrp.SelectedValue;
            ObjBasicData.Prod_Inspect_Memo = txtProduction.Text;
            ObjBasicData.Gross_Weight = txtGrossWeight.Text;
            ObjBasicData.Net_Weight = txtNetWeight.Text;
            ObjBasicData.Weight_Unit = ddlWeightUnit.SelectedValue;
            ObjBasicData.Volume = txtVolume.Text;
            ObjBasicData.Volume_Unit = ddlVolumeUnit.Text;
            ObjBasicData.InterNational_Article_No = txtInternationalANo.Text;
            ObjBasicData.Category_InterN_Article_No = ddlCategoryIA.SelectedValue;
            ObjBasicData.Material_Grp_Pack_Mtl = ddlMatlGrpPackMatl.SelectedValue;
            ObjBasicData.Reason_For_Creation = ddlReason.SelectedValue;
            ObjBasicData.Remarks = txtRemarks.Text;
            ObjBasicData.UserId = lblUserId.Text;
            ObjBasicData.TodayDate = objUtil.GetDate();
            ObjBasicData.IPAddress = objUtil.GetIpAddress();
            ObjBasicData.Mode = lblMode.Text;
            //CTRL_SUB_SDT06062019
            ObjBasicData.sControlSubYN = ddlIsContainsChemical.SelectedValue;
            //CTRL_SUB_SDT06062019
            //DT05072023_BG_Type
            ObjBasicData.sBGWCF = ddlBGWCF.SelectedValue;
            //DT05072023_BG_Type
            //PROV-CCP-MM-941-23-0045 in QAMS
            ObjBasicData.sIsMatComm = ddlIsMatComm.SelectedValue;
            //PROV-CCP-MM-941-23-0045 in QAMS
        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return ObjBasicData;
    }

    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.Basic1 obj = new SectionConfiguration.Basic1();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));

            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
            {
                txtMaterialNo.Enabled = true;
            }
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
    }

    private void BaseUnitWarning()
    {
        try
        {
            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164))
            {
                //MSC_8300001775
                //if ((ddlBaseUnit.SelectedValue == "TS ") || (ddlBaseUnit.SelectedValue == "TO "))
                if ((ddlBaseUnit.SelectedValue == "TS") || (ddlBaseUnit.SelectedValue == "TO"))
                {
                    lblMsg.Text = "Warning : The Base unit of Measure should always be the lowest unit of measure. Maintain 'EA-Each'/'KG-Kilogram' respectively in Base unit of measure and if required TS/TO can be maintained in Purchase Order Unit of Measure in Purchasing section";
                    pnlMsg.CssClass = "warning";
                    pnlMsg.Visible = true;
                }
                else
                {
                    pnlMsg.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { _log.Error("BaseUnitWarning", ex); }
    }

    protected void ShowMatDesc(bool isVisible)
    {
        try
        {
            //ucMatDescription.MhId = lblMasterHeaderId.Text;
            txtMaterialDescription.Enabled = false;
            pnlMatDesc.Visible = isVisible;
            trMatDesc.Visible = isVisible;
        }
        catch (Exception ex)
        { _log.Error("ShowMatDesc", ex); }
    }

    /// <summary>
    /// //PROV-CCP-MM-941-23-0045 in QAMS
    /// </summary>
    /// <param name="flgismc"></param>
    private void Ismatcommercial(bool flgismc)
    {
        lblddlIsMatComm.Enabled = flgismc;
        lableddlIsMatComm.Enabled = flgismc;
        reqddlIsMatComm.Enabled = flgismc;
        ddlIsMatComm.Enabled = flgismc;

        lblddlIsMatComm.Visible = flgismc;
        lableddlIsMatComm.Visible = flgismc;
        reqddlIsMatComm.Visible = flgismc;
        ddlIsMatComm.Visible = flgismc;
    }

    private void SetExtnsnLinkVisibility()
    {
        //Added By Nitin R
        IRFDivisionListMet();
        //Added By Nitin R
        //MSC_8300001775 Start
        try
        {
            string sectionId = lblSectionId.Text.ToString();
            string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
            string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            string mode = Session[StaticKeys.Mode].ToString();
            bool flg = false;
            if (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
            {
                if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171))
                {
                    if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 24) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 25) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 13))
                    {
                        if ((Session[StaticKeys.MarketType].ToString() == "") || (Session[StaticKeys.MarketType].ToString() == "I") || (Session[StaticKeys.MarketType].ToString() == "R"))
                        {
                            if (IRFDivision.Contains(ddlDivision.SelectedValue.ToString()))
                            {
                                flg = true;
                            }
                        }
                    }
                }
            }
            lnkExtnsn.Visible = flg;
            //DT05072023_BG_Type
            labelddlBGWCF.Enabled = flg;
            lblddlBGWCF.Enabled = flg;
            rfvddlBGWCF.Enabled = flg;
            ddlBGWCF.Enabled = flg;

            //labelddlBGWCF.Visible = flg;
            lblddlBGWCF.Visible = flg;
            rfvddlBGWCF.Visible = flg;
            // ddlBGWCF.Visible = flg;
            //DT05072023_BG_Type

        }
        catch (Exception ex)
        { _log.Error("SetExtnsnLinkVisibility", ex); }

        //else
        //{
        //    if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 139) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 145) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 171))
        //    {
        //        if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 0) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 24) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 25) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 13))
        //        {
        //            if ((Session[StaticKeys.MarketType].ToString() == "") || (Session[StaticKeys.MarketType].ToString() == "I") || (Session[StaticKeys.MarketType].ToString() == "R"))
        //                lnkExtnsn.Visible = false;
        //        }

        //    }
        //}
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
            txtValidFrom.CssClass = "textbox";
            txtValidFrom.Enabled = true;
            txtOldMaterialNo.Enabled = true;
            ddlDivision.Enabled = true;
            ddlGenItemCategoryGrp.Enabled = true;
            ddlCrossPlantMaterialStatus.Enabled = true;
            txtInternationalANo.Enabled = true;
            ddlCategoryIA.Enabled = true;
            txtOldMaterialNo.CssClass = "textbox";
            txtInternationalANo.CssClass = "textbox";
            //txtControl.CssClass = status ? "textbox" : "textboxDisable";
            lableddlMaterialGroup.Visible = false;
            reqddlMaterialGroup.Visible = false;
            tridVolume1.Attributes.Add("style", "display:block!;");
            tridVolume.Attributes.Add("style", "display:block!;");
            tridProdHr1.Attributes.Add("style", "display:block!;");
            reqtxtVolume.Enabled = false;
            labletxtVolume.Enabled = false;
            lableddlVolumeUnit.Enabled = false;
            reqddlVolumeUnit.Enabled = false;
            txtVolume.Enabled = true;
            ddlVolumeUnit.Enabled = true;
            tridProdHr3.Attributes.Add("style", "display:block!;");
            lableddlProductHierarchy1.Enabled = false;
            reqddlProductHierarchy1.Enabled = false;
            ddlProductHierarchy1.Enabled = true;
            //ddlProductHierarchy2.Enabled = true;
            //ddlProductHierarchy3.Enabled = true;
            txtVolume.CssClass = "textbox";

            txtMaterialDescription.Enabled = true;
            txtMaterialDescription.CssClass = "textbox";
            txtMaterialDescription.ReadOnly = false;

            ddlBaseUnit.Enabled = true;

            //DT05072023_BG_Type
            labelddlBGWCF.Enabled = false;
            lblddlBGWCF.Enabled = false;
            rfvddlBGWCF.Enabled = false;
            ddlBGWCF.Enabled = false;

            labelddlBGWCF.Visible = false;
            lblddlBGWCF.Visible = false;
            rfvddlBGWCF.Visible = false;
            ddlBGWCF.Visible = false;
            //DT05072023_BG_Type

        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }

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
            //throw ex;
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
            _log.Error("SaveDocuments", ex);
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

    #endregion

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSPOCProsol_Click(object sender, EventArgs e)
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Start of execution Prosol API");
        }
        catch (Exception ex)
        {
            _log.Error("btnSPOCProsol_Click", ex);
        }

        var responseText = "";
        string proslid1 = "";
        try
        {
            //PROSOL_SDT16092019
            BasicDataAccess basicDataAccess = new BasicDataAccess();
            DataSet ds3 = basicDataAccess.GetProsolID(Convert.ToInt32(lblMasterHeaderId.Text));
            if (ds3.Tables[0].Rows.Count > 0)
            {
                //proslid = Convert.ToInt64(ds3.Tables[0].Rows[0]["sProsolID"].ToString());

                proslid1 = Convert.ToString(ds3.Tables[0].Rows[0]["sProsolID"].ToString());
            }
            //PROSOL_SDT16092019
        }
        catch (Exception ex)
        {
            _log.Error("btnSPOCProsol_Click1", ex);
        }
        try
        {

            WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "reqId :" + proslid1 + ", Username:" + Convert.ToString(Session[StaticKeys.LoggedIn_User_Name]) + "");
            //string webAddr = "http://prosolqa.lupin.com/prosol/api/matcode/updatematerialdesc?reqId=100320190004&Username=nitinrajeshirke";
            //string webAddr = "http://prosolqa.lupin.com/prosol/api/matcode/updatematerialdesc?reqId=" + proslid + "&Username=nitinrajeshirke";
            //string webAddr1 = "http://prosolqa.lupin.com/prosol/api/matcode/updatematerialdesc?reqId=" + proslid1 + "&Username=" + Convert.ToString(Session[StaticKeys.LoggedIn_User_Name]) + "";

            string webAddr1 = Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"]) + "/prosol/api/matcode/updatematerialdesc?reqId=" + proslid1 + "&Username=" + Convert.ToString(Session[StaticKeys.LoggedIn_User_Name]) + "";

            //http://prosolqa.lupin.com/prosol/api/matcode/updatematerialdesc?reqId=640811&Username=Ahamed
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr1);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                //WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Success Msg :" + responseText + "");
                responseText = streamReader.ReadToEnd();
                if (responseText == "\"Success\"")
                {
                    //PROSOL_SDT09112021 PROSOL_SDT16092019
                    BasicDataAccess basicDataAccess = new BasicDataAccess();
                    basicDataAccess.UpdateRequestStatus(proslid1, "SPOC Update", "P", "");
                    //PROSOL_SDT09112021 PROSOL_SDT16092019

                    WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Success Msg :" + responseText + "");
                    string ProsolLink = Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"]);
                    Response.Redirect(ProsolLink);

                    //Response.Write("<script>");
                    //Response.Write("window.open('" + ProsolLink + "' ,'_blank')");
                    //Response.Write("</script>");

                }
                else
                {
                    WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Error Msg :" + responseText + "");

                }
                Console.WriteLine(responseText);

            }
        }
        catch (Exception ex)
        {
            _log.Error("btnSPOCProsol_Click2", ex);
            WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Error Msg :" + ex.Message);
        }
        WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "End of execution Prosol API");
    }

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    /// <param name="strFileName"></param>
    /// <param name="strMessage"></param>
    public void WriteProsolLog(string strFileName, string strMessage)
    {
        try
        {
            //Path.GetTempPath()
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ProsolLog", strFileName), FileMode.Append, FileAccess.Write);
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