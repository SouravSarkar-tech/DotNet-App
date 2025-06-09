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
//&& HelperAccess.ReqType != "SMC" added for Single material change MSC_8300001775
public partial class Transaction_Classification : BasePage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    ClassificationAccess ObjClassificationAccess = new ClassificationAccess();
    Classification objSavedClassification = new Classification();

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
                    }

                    ClearClassificationData();
                    ConfigureControl();

                    //To manage the Creation Single request
                    FillFormDataByMHId();
                    lnlAddDetails.Visible = false;
                    grvClassification.Visible = false;
                    //MSC_8300001775 Start
                    //if (HelperAccess.ReqType == "SMC")
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
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
            //lableddlClass_Type.Visible =false;	
            //reqddlClass_Type.Visible =false;
            //lableddlClass.Visible =false;
            //reqddlClass.Visible =false;
            labletxtStrengthofmatPacktype.Visible = false;
            reqtxtStrengthofmatPacktype.Visible = false;
            labletxtMarket.Visible = false;
            reqtxtMarket.Visible = false;
            labletxtNDCNoLPI.Visible = false;
            reqtxtNDCNoLPI.Visible = false;
            labletxtNDCNoLL.Visible = false;
            reqtxtNDCNoLL.Visible = false;
            labletxtHTS.Visible = false;
            reqtxtHTS.Visible = false;
            labletxtANDA.Visible = false;
            reqtxtANDA.Visible = false;
            labletxtFDANo.Visible = false;
            reqtxtFDANo.Visible = false;
            lableddlLPIMaterialIdentifier.Visible = false;
            reqddlLPIMaterialIdentifier.Visible = false;
            lableddlMaterialGroupingforMES.Visible = false;
            reqddlMaterialGroupingforMES.Visible = false;
            labletxtShortdescriptionfor3PL.Visible = false;
            reqtxtShortdescriptionfor3PL.Visible = false;
            labletxtPackagepresentation3PL.Visible = false;
            reqtxtPackagepresentation3PL.Visible = false;
            labletxtNumberofTablet3PL.Visible = false;
            reqtxtNumberofTablet3PL.Visible = false;
            lableddlMaterialCategoryA3PL.Visible = false;
            reqddlMaterialCategoryA3PL.Visible = false;
            lableddlMaterialCategoryB3PL.Visible = false;
            reqddlMaterialCategoryB3PL.Visible = false;
            labletxtSortingforinventoryreport.Visible = false;
            reqtxtSortingforinventoryreport.Visible = false;
            labletxtPacksize.Visible = false;
            reqtxtPacksize.Visible = false;
            labletxtProductGroup.Visible = false;
            reqtxtProductGroup.Visible = false;
            lableddlDrugCategory.Visible = false;
            reqddlDrugCategory.Visible = false;
            labletxtMarketEntryDate.Visible = false;
            reqtxtMarketEntryDate.Visible = false;
            labletxtPZNHORMOSAN.Visible = false;
            reqtxtPZNHORMOSAN.Visible = false;
            lableddlStorageCond.Visible = false;
            reqddlStorageCond.Visible = false;
            labletxtAllowed_Manufacturers.Visible = false;
            reqtxtAllowed_Manufacturers.Visible = false;
            labletxtHSAN_MATERIAL_IDENTIFIER.Visible = false;
            reqtxtHSAN_MATERIAL_IDENTIFIER.Visible = false;
            labletxtExpiration_date_shelf_life.Visible = false;
            reqtxtExpiration_date_shelf_life.Visible = false;
            labletxtNext_Insp_Date_for_Batch.Visible = false;
            reqtxtNext_Insp_Date_for_Batch.Visible = false;
            labletxtBatch_number.Visible = false;
            reqtxtBatch_number.Visible = false;
            labletxtASSAY_ASIS.Visible = false;
            reqtxtASSAY_ASIS.Visible = false;
            labletxtMANUFACTURER.Visible = false;
            reqtxtMANUFACTURER.Visible = false;
            labletxtPotency_as_is_basis.Visible = false;
            reqtxtPotency_as_is_basis.Visible = false;
            labletxtLoss_on_Drying.Visible = false;
            reqtxtLoss_on_Drying.Visible = false;
            labletxtPotency_as_is_basis1.Visible = false;
            reqtxtPotency_as_is_basis1.Visible = false;
            labletxtRM402217.Visible = false;
            reqtxtRM402217.Visible = false;
            labletxtRM323350.Visible = false;
            reqtxtRM323350.Visible = false;
            labletxtSF110063.Visible = false;
            reqtxtSF110063.Visible = false;
            labletxtSF900052.Visible = false;
            reqtxtSF900052.Visible = false;
            labletxtIP4A0047.Visible = false;
            reqtxtIP4A0047.Visible = false;
            labletxtAssay_by_GC.Visible = false;
            reqtxtAssay_by_GC.Visible = false;
            labletxtExternal_Material_Group.Visible = false;
            reqtxtExternal_Material_Group.Visible = false;
            labletxtVersion_Number.Visible = false;
            reqtxtVersion_Number.Visible = false;
            reqddlMaterialGroupingforMES.Enabled = false;
            reqddlStorageCond.Enabled = false;
            lableddlStorageCond.Visible = false;
            lableddlMaterialGroupingforMES.Visible = false;

            KinaxisFielsCR();
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }

    private void KinaxisFielsCR()
    {
        try
        {

            rfvtxtKXSBU.Enabled = false;
            labeltxtKXSBU.Visible = false;

            rfvtxtKXMARKT.Enabled = false;
            labeltxtKXMARKT.Visible = false;
            rfvtxtKXSELLCTRY.Enabled = false;
            labeltxtKXSELLCTRY.Visible = false;
            rfvtxtKXBUSI.Enabled = false;
            labeltxtKXBUSI.Visible = false;
            rfvtxtKXDIV.Enabled = false;
            labeltxtKXDIV.Visible = false;
            rfvtxtKXTHER.Enabled = false;
            labeltxtKXTHER.Visible = false;
            rfvtxtKXDOSFRM.Enabled = false;
            labeltxtKXDOSFRM.Visible = false;
            rfvtxtKXMINSL.Enabled = false;
            labeltxtKXMINSL.Visible = false;
            rfvtxtMKTMNGER.Enabled = false;
            labeltxtMKTMNGER.Visible = false;
            rfvddlCS_MOLECULE.Enabled = false;
            labelddlCS_MOLECULE.Visible = false;
            txtKXSBU.Enabled = true;
            txtKXMARKT.Enabled = true;
            txtKXSELLCTRY.Enabled = true;
            txtKXBUSI.Enabled = true;
            txtKXDIV.Enabled = true;
            txtKXTHER.Enabled = true;
            txtKXDOSFRM.Enabled = true;
            txtKXMINSL.Enabled = true;
            txtMKTMNGER.Enabled = true;
            ddlCS_MOLECULE.Enabled = true;
            reqtxtStrengthofmatPacktype.Enabled = false;
            labletxtStrengthofmatPacktype.Visible = false;
            txtStrengthofmatPacktype.Enabled = true;
            reqtxtPacksize.Enabled = false;
            labletxtPacksize.Visible = false;
            txtPacksize.Enabled = true;
            reqtxtProductGroup.Enabled = true;
            labletxtProductGroup.Visible = false;
            txtProductGroup.Enabled = true;

            //PROV-CCP-MM-941-23-0045 End
        }
        catch (Exception ex)
        { _log.Error("KinaxisFielsCR", ex); }
    }

    /// <summary>
    /// PROV-CCP-MM-941-23-0045
    /// </summary>
    private void KinaxisFiels()
    {
        try
        {
            if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "8" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27") && (Session[StaticKeys.SelectedModuleId].ToString() == "144"))
            {
                rfvtxtKXSBU.Enabled = true;
                labeltxtKXSBU.Visible = true;
                rfvtxtKXMARKT.Enabled = true;
                labeltxtKXMARKT.Visible = true;
                rfvtxtKXSELLCTRY.Enabled = true;
                labeltxtKXSELLCTRY.Visible = true;
                rfvtxtKXBUSI.Enabled = true;
                labeltxtKXBUSI.Visible = true;
                rfvtxtKXDIV.Enabled = true;
                labeltxtKXDIV.Visible = true;
                rfvtxtKXTHER.Enabled = true;
                labeltxtKXTHER.Visible = true;
                rfvtxtKXDOSFRM.Enabled = true;
                labeltxtKXDOSFRM.Visible = true;
                rfvtxtKXMINSL.Enabled = true;
                labeltxtKXMINSL.Visible = true;
                rfvtxtMKTMNGER.Enabled = true;
                labeltxtMKTMNGER.Visible = true;
                rfvddlCS_MOLECULE.Enabled = true;
                labelddlCS_MOLECULE.Visible = true;
                reqtxtStrengthofmatPacktype.Enabled = true;
                labletxtStrengthofmatPacktype.Visible = true;
                reqtxtPacksize.Enabled = true;
                labletxtPacksize.Visible = true;
                reqtxtProductGroup.Enabled = true;
                labletxtProductGroup.Visible = true;

                txtKXSBU.Enabled = true;
                txtKXMARKT.Enabled = true;
                txtKXSELLCTRY.Enabled = true;
                txtKXBUSI.Enabled = true;
                txtKXDIV.Enabled = true;
                txtKXTHER.Enabled = true;
                txtKXDOSFRM.Enabled = true;
                txtKXMINSL.Enabled = true;
                txtMKTMNGER.Enabled = true;
                ddlCS_MOLECULE.Enabled = true;
                txtStrengthofmatPacktype.Enabled = true;
                txtPacksize.Enabled = true;
                txtProductGroup.Enabled = true;

                reqtxtPacksize.Visible = true;
                reqtxtProductGroup.Visible = true;
                reqtxtStrengthofmatPacktype.Visible = true;

                reqtxtPacksize.Enabled = true;
                reqtxtProductGroup.Enabled = true;
                reqtxtStrengthofmatPacktype.Enabled = true;

            }
            else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0" && (Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "171"))
            {
                rfvtxtKXSBU.Enabled = true;
                labeltxtKXSBU.Visible = true;
                rfvtxtKXMARKT.Enabled = true;
                labeltxtKXMARKT.Visible = true;
                rfvtxtKXSELLCTRY.Enabled = true;
                labeltxtKXSELLCTRY.Visible = true;
                rfvtxtKXBUSI.Enabled = true;
                labeltxtKXBUSI.Visible = true;
                rfvtxtKXDIV.Enabled = true;
                labeltxtKXDIV.Visible = true;
                rfvtxtKXTHER.Enabled = true;
                labeltxtKXTHER.Visible = true;
                rfvtxtKXDOSFRM.Enabled = true;
                labeltxtKXDOSFRM.Visible = true;
                rfvtxtKXMINSL.Enabled = true;
                labeltxtKXMINSL.Visible = true;
                rfvtxtMKTMNGER.Enabled = true;
                labeltxtMKTMNGER.Visible = true;
                rfvddlCS_MOLECULE.Enabled = true;
                labelddlCS_MOLECULE.Visible = true;
                reqtxtStrengthofmatPacktype.Enabled = true;
                labletxtStrengthofmatPacktype.Visible = true;
                reqtxtPacksize.Enabled = true;
                labletxtPacksize.Visible = true;
                reqtxtProductGroup.Enabled = true;
                labletxtProductGroup.Visible = true;

                txtKXSBU.Enabled = true;
                txtKXMARKT.Enabled = true;
                txtKXSELLCTRY.Enabled = true;
                txtKXBUSI.Enabled = true;
                txtKXDIV.Enabled = true;
                txtKXTHER.Enabled = true;
                txtKXDOSFRM.Enabled = true;
                txtKXMINSL.Enabled = true;
                txtMKTMNGER.Enabled = true;
                ddlCS_MOLECULE.Enabled = true;
                txtStrengthofmatPacktype.Enabled = true;
                txtPacksize.Enabled = true;
                txtProductGroup.Enabled = true;

                reqtxtPacksize.Enabled = true;
                reqtxtProductGroup.Enabled = true;
                reqtxtStrengthofmatPacktype.Enabled = true;

                reqtxtPacksize.Visible = true;
                reqtxtProductGroup.Visible = true;
                reqtxtStrengthofmatPacktype.Visible = true;

            }
            else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0" && (Session[StaticKeys.SelectedModuleId].ToString() == "144"))
            {
                rfvtxtKXSBU.Enabled = false;
                labeltxtKXSBU.Visible = false;
                rfvtxtKXMARKT.Enabled = false;
                labeltxtKXMARKT.Visible = false;
                rfvtxtKXSELLCTRY.Enabled = false;
                labeltxtKXSELLCTRY.Visible = false;
                rfvtxtKXBUSI.Enabled = false;
                labeltxtKXBUSI.Visible = false;
                rfvtxtKXDIV.Enabled = false;
                labeltxtKXDIV.Visible = false;
                rfvtxtKXTHER.Enabled = false;
                labeltxtKXTHER.Visible = false;
                rfvtxtKXDOSFRM.Enabled = false;
                labeltxtKXDOSFRM.Visible = false;
                rfvtxtKXMINSL.Enabled = false;
                labeltxtKXMINSL.Visible = false;
                rfvtxtMKTMNGER.Enabled = false;
                labeltxtMKTMNGER.Visible = false;
                rfvddlCS_MOLECULE.Enabled = false;
                labelddlCS_MOLECULE.Visible = false;
                reqtxtStrengthofmatPacktype.Enabled = false;
                labletxtStrengthofmatPacktype.Visible = false;
                reqtxtPacksize.Enabled = false;
                labletxtPacksize.Visible = false;
                reqtxtProductGroup.Enabled = false;
                labletxtProductGroup.Visible = false;
                reqtxtPacksize.Enabled = false;
                reqtxtProductGroup.Enabled = false;
                reqtxtStrengthofmatPacktype.Enabled = false;
                //txtKXSBU.Enabled = true;
                //txtKXMARKT.Enabled = true;
                //txtKXSELLCTRY.Enabled = true;
                //txtKXBUSI.Enabled = true;
                //txtKXDIV.Enabled = true;
                //txtKXTHER.Enabled = true;
                //txtKXDOSFRM.Enabled = true;
                //txtKXMINSL.Enabled = true;
                //txtMKTMNGER.Enabled = true;
                //ddlCS_MOLECULE.Enabled = true;
                //txtStrengthofmatPacktype.Enabled = true;
                //txtPacksize.Enabled = true;
                //txtProductGroup.Enabled = true;



            }
            else
            {
                rfvtxtKXSBU.Enabled = false;
                labeltxtKXSBU.Visible = false;
                rfvtxtKXMARKT.Enabled = false;
                labeltxtKXMARKT.Visible = false;
                rfvtxtKXSELLCTRY.Enabled = false;
                labeltxtKXSELLCTRY.Visible = false;
                rfvtxtKXBUSI.Enabled = false;
                labeltxtKXBUSI.Visible = false;
                rfvtxtKXDIV.Enabled = false;
                labeltxtKXDIV.Visible = false;
                rfvtxtKXTHER.Enabled = false;
                labeltxtKXTHER.Visible = false;
                rfvtxtKXDOSFRM.Enabled = false;
                labeltxtKXDOSFRM.Visible = false;
                rfvtxtKXMINSL.Enabled = false;
                labeltxtKXMINSL.Visible = false;
                rfvtxtMKTMNGER.Enabled = false;
                labeltxtMKTMNGER.Visible = false;
                rfvddlCS_MOLECULE.Enabled = false;
                labelddlCS_MOLECULE.Visible = false;
                txtKXSBU.Enabled = false;
                txtKXMARKT.Enabled = false;
                txtKXSELLCTRY.Enabled = false;
                txtKXBUSI.Enabled = false;
                txtKXDIV.Enabled = false;
                txtKXTHER.Enabled = false;
                txtKXDOSFRM.Enabled = false;
                txtKXMINSL.Enabled = false;
                txtMKTMNGER.Enabled = false;
                ddlCS_MOLECULE.Enabled = false;
                reqtxtStrengthofmatPacktype.Enabled = false;
                labletxtStrengthofmatPacktype.Visible = false;
                txtStrengthofmatPacktype.Enabled = false;
                reqtxtPacksize.Enabled = false;
                labletxtPacksize.Visible = false;
                txtPacksize.Enabled = false;
                reqtxtProductGroup.Enabled = false;
                labletxtProductGroup.Visible = false;
                txtProductGroup.Enabled = false;

                reqtxtPacksize.Enabled = false;
                reqtxtProductGroup.Enabled = false;
                reqtxtStrengthofmatPacktype.Enabled = false;
            }
            //PROV-CCP-MM-941-23-0045 End
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }
    /// <summary>
    /// PROV-CCP-MM-941-23-0045
    /// </summary>
    private void KinaxisFielsOld()
    {
        try
        {
            //ddlKXSBU.Enabled = true;
            //ddlKXMARKT.Enabled = true;
            //ddlKXSELLCTRY.Enabled = true;
            //ddlKXBUSI.Enabled = true;
            //ddlKXDIV.Enabled = true;
            //ddlKXTHER.Enabled = true;
            //ddlKXDOSFRM.Enabled = true;
            //ddlKXMINSL.Enabled = true;
            //ddlMKTMNGER.Enabled = true;
            //ddlCS_MOLECULE.Enabled = true;
            //PROV-CCP-MM-941-23-0045 Start
            if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "8" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27") && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171"))
            {
                //mandatory
                //rfvddlKXSBU.Enabled = true;
                //lableddlKXSBU.Visible = true;
                //rfvddlKXMARKT.Enabled = true;
                //labelddlKXMARKT.Visible = true;
                //rfvddlKXSELLCTRY.Enabled = true;
                //labelddlKXSELLCTRY.Visible = true;
                //rfvddlKXBUSI.Enabled = true;
                //labelddlKXBUSI.Visible = true;
                //rfvddlKXDIV.Enabled = true;
                //labelddlKXDIV.Visible = true;

                rfvtxtKXSBU.Enabled = true;
                labeltxtKXSBU.Visible = true;
                rfvtxtKXMARKT.Enabled = true;
                labeltxtKXMARKT.Visible = true;
                rfvtxtKXSELLCTRY.Enabled = true;
                labeltxtKXSELLCTRY.Visible = true;
                rfvtxtKXBUSI.Enabled = true;
                labeltxtKXBUSI.Visible = true;
                rfvtxtKXDIV.Enabled = true;
                labeltxtKXDIV.Visible = true;

                rfvtxtKXTHER.Enabled = true;
                labeltxtKXTHER.Visible = true;
                rfvtxtKXDOSFRM.Enabled = true;
                labeltxtKXDOSFRM.Visible = true;
                rfvtxtKXMINSL.Enabled = true;
                labeltxtKXMINSL.Visible = true;
                rfvtxtMKTMNGER.Enabled = true;
                labeltxtMKTMNGER.Visible = true;

                //rfvddlKXTHER.Enabled = true;
                //labelddlKXTHER.Visible = true;
                //rfvddlKXDOSFRM.Enabled = true;
                //labelddlKXDOSFRM.Visible = true;
                //rfvddlKXMINSL.Enabled = true;
                //labelddlKXMINSL.Visible = true;
                //rfvddlMKTMNGER.Enabled = true;
                //labelddlMKTMNGER.Visible = true;
                rfvddlCS_MOLECULE.Enabled = true;
                labelddlCS_MOLECULE.Visible = true;

                reqtxtStrengthofmatPacktype.Enabled = true;
                labletxtStrengthofmatPacktype.Visible = true;
                reqtxtPacksize.Enabled = true;
                labletxtPacksize.Visible = true;
                reqtxtProductGroup.Enabled = true;
                labletxtProductGroup.Visible = true;
            }
            else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0" && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171"))
            {
                rfvtxtKXSBU.Enabled = false;
                labeltxtKXSBU.Visible = false;
                rfvtxtKXMARKT.Enabled = false;
                labeltxtKXMARKT.Visible = false;
                rfvtxtKXSELLCTRY.Enabled = false;
                labeltxtKXSELLCTRY.Visible = false;
                rfvtxtKXBUSI.Enabled = false;
                labeltxtKXBUSI.Visible = false;
                rfvtxtKXDIV.Enabled = false;
                labeltxtKXDIV.Visible = false;
                rfvtxtKXTHER.Enabled = false;
                labeltxtKXTHER.Visible = false;
                rfvtxtKXDOSFRM.Enabled = false;
                labeltxtKXDOSFRM.Visible = false;
                rfvtxtKXMINSL.Enabled = false;
                labeltxtKXMINSL.Visible = false;
                rfvtxtMKTMNGER.Enabled = false;
                labeltxtMKTMNGER.Visible = false;
                //rfvddlKXTHER.Enabled = false;
                //labelddlKXTHER.Visible = false;
                //rfvddlKXDOSFRM.Enabled = false;
                //labelddlKXDOSFRM.Visible = false;
                //rfvddlKXMINSL.Enabled = false;
                //labelddlKXMINSL.Visible = false;
                //rfvddlMKTMNGER.Enabled = false;
                //labelddlMKTMNGER.Visible = false;
                rfvddlCS_MOLECULE.Enabled = false;
                labelddlCS_MOLECULE.Visible = false;

                reqtxtStrengthofmatPacktype.Enabled = false;
                labletxtStrengthofmatPacktype.Visible = false;
                reqtxtPacksize.Enabled = false;
                labletxtPacksize.Visible = false;
                reqtxtProductGroup.Enabled = false;
                labletxtProductGroup.Visible = false;

            }
            //else if ((Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171"))
            //{
            //    rfvddlKXSBU.Enabled = false;
            //    lableddlKXSBU.Visible = false;
            //    rfvddlKXMARKT.Enabled = false;
            //    labelddlKXMARKT.Visible = false;
            //    rfvddlKXSELLCTRY.Enabled = false;
            //    labelddlKXSELLCTRY.Visible = false;
            //    rfvddlKXBUSI.Enabled = false;
            //    labelddlKXBUSI.Visible = false;
            //    rfvddlKXDIV.Enabled = false;
            //    labelddlKXDIV.Visible = false;
            //    rfvddlKXTHER.Enabled = false;
            //    labelddlKXTHER.Visible = false;
            //    rfvddlKXDOSFRM.Enabled = false;
            //    labelddlKXDOSFRM.Visible = false;
            //    rfvddlKXMINSL.Enabled = false;
            //    labelddlKXMINSL.Visible = false;
            //    rfvddlMKTMNGER.Enabled = false;
            //    labelddlMKTMNGER.Visible = false;
            //    rfvddlCS_MOLECULE.Enabled = false;
            //    labelddlCS_MOLECULE.Visible = false;

            //    ddlKXSBU.Enabled = false;
            //    ddlKXMARKT.Enabled = false;
            //    ddlKXSELLCTRY.Enabled = false;
            //    ddlKXBUSI.Enabled = false;
            //    ddlKXDIV.Enabled = false;
            //    ddlKXTHER.Enabled = false;
            //    ddlKXDOSFRM.Enabled = false;
            //    ddlKXMINSL.Enabled = false;
            //    ddlMKTMNGER.Enabled = false;
            //    ddlCS_MOLECULE.Enabled = false;
            //}
            else
            {
                rfvtxtKXSBU.Enabled = false;
                labeltxtKXSBU.Visible = false;
                rfvtxtKXMARKT.Enabled = false;
                labeltxtKXMARKT.Visible = false;
                rfvtxtKXSELLCTRY.Enabled = false;
                labeltxtKXSELLCTRY.Visible = false;
                rfvtxtKXBUSI.Enabled = false;
                labeltxtKXBUSI.Visible = false;
                rfvtxtKXDIV.Enabled = false;
                labeltxtKXDIV.Visible = false;
                rfvtxtKXTHER.Enabled = false;
                labeltxtKXTHER.Visible = false;
                rfvtxtKXDOSFRM.Enabled = false;
                labeltxtKXDOSFRM.Visible = false;
                rfvtxtKXMINSL.Enabled = false;
                labeltxtKXMINSL.Visible = false;
                rfvtxtMKTMNGER.Enabled = false;
                labeltxtMKTMNGER.Visible = false;
                //rfvddlKXTHER.Enabled = false;
                //labelddlKXTHER.Visible = false;
                //rfvddlKXDOSFRM.Enabled = false;
                //labelddlKXDOSFRM.Visible = false;
                //rfvddlKXMINSL.Enabled = false;
                //labelddlKXMINSL.Visible = false;
                //rfvddlMKTMNGER.Enabled = false;
                //labelddlMKTMNGER.Visible = false;
                rfvddlCS_MOLECULE.Enabled = false;
                labelddlCS_MOLECULE.Visible = false;

                txtKXSBU.Enabled = false;
                txtKXMARKT.Enabled = false;
                txtKXSELLCTRY.Enabled = false;
                txtKXBUSI.Enabled = false;
                txtKXDIV.Enabled = false;
                txtKXTHER.Enabled = false;
                txtKXDOSFRM.Enabled = false;
                txtKXMINSL.Enabled = false;
                txtMKTMNGER.Enabled = false;
                //ddlKXTHER.Enabled = false;
                //ddlKXDOSFRM.Enabled = false;
                //ddlKXMINSL.Enabled = false;
                //ddlMKTMNGER.Enabled = false;
                ddlCS_MOLECULE.Enabled = false;

                reqtxtStrengthofmatPacktype.Enabled = false;
                labletxtStrengthofmatPacktype.Visible = false;
                txtStrengthofmatPacktype.Enabled = false;
                reqtxtPacksize.Enabled = false;
                labletxtPacksize.Visible = false;
                txtPacksize.Enabled = false;
                reqtxtProductGroup.Enabled = false;
                labletxtProductGroup.Visible = false;
                txtProductGroup.Enabled = false;
            }
            //PROV-CCP-MM-941-23-0045 End
        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
    }
    protected void lnlAddDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClearClassificationData();
            FillClassificationData();
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
            lblClassificationId.Text = grvClassification.DataKeys[grdrow.RowIndex]["Mat_Classification_Id"].ToString();
            FillClassificationData();
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
                if ((lblModuleId.Text == "162" || lblModuleId.Text == "164") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                {
                    Response.Redirect("BasicData1.aspx");
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
            if (Save())
            {
                //lblMsg.Text = Messages.GetMessage(1);
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                Response.Redirect("Classification.aspx");
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
                ////8400000410 comment Start
                //if ((lblModuleId.Text == "162" || lblModuleId.Text == "164") && !MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                //{
                //    Response.Redirect("Sales2.aspx");
                //}
                //else
                //{
                //    string pageURL = btnNext.CommandArgument.ToString();
                //    Response.Redirect(pageURL);
                //}
                ////8400000410 comment End
                ///

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

    /// <summary>
    /// IND_DT14012020
    /// </summary>
    private void DisplayClass_Type()
    {
        try
        {
            string Class_TypeType = GetSelectedCheckedValue(ddlClass_Type);
            if (Class_TypeType != null)
            {
                lblc1ddlClass_Type.Text = "Class Type :- " + Class_TypeType.Substring(0, Class_TypeType.Length - 1);

            }
            else
            {
                lblc1ddlClass_Type.Text = "";
            }
        }
        catch (Exception ex)
        { _log.Error("DisplayClass_Type", ex); }

    }

    /// <summary>
    /// IND_DT14012020
    /// </summary>
    private void DisplayClass()
    {
        try
        {
            string Classm = GetSelectedCheckedValue(ddlClass);
            if (Classm != null)
            {
                lblc1ddlClass.Text = "Class :- " + Classm.Substring(0, Classm.Length - 1);

            }
            else
            {
                lblc1ddlClass.Text = "";
            }
        }
        catch (Exception ex)
        { _log.Error("DisplayClass", ex); }

    }

    protected void ddlClass_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Old Commented  IND_DT14012020 
        // helperAccess.PopuplateDropDownList(ddlClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass','" + lblSectionId.Text + "','" + ddlClass_Type.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
        //Old Commented  IND_DT14012020 
        //Added  IND_DT14012020 
        DisplayClass_Type();
        try
        {
            ClearSelectedValue(ddlClass);
            string classtype = GetSelectedCheckedValue(ddlClass_Type);
            helperAccess.PopuplateDropDownCheckBox(ddlClass, "pr_GetDropDownListByControlNameModuleType_MultiClass 'M','ddlClass','" + lblSectionId.Text + "','" + classtype + "'", "LookUp_Desc", "LookUp_Code");
        }
        catch (Exception ex)
        { _log.Error("ddlClass_Type_SelectedIndexChanged", ex); }
        DisplayClass();
        //Added  IND_DT14012020 
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DisplayClass();
            SetFieldsByClassification();
        }
        catch (Exception ex)
        { _log.Error("ddlClass_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Methods

    private void PopuplateDropDownList()
    {
        try
        {
            // IND_DT14012020 Commented
            //helperAccess.PopuplateDropDownList(ddlClass_Type, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass_Type'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass','" + lblSectionId.Text + "','" + ddlClass_Type.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
            // IND_DT14012020 Commented
            //Added  IND_DT14012020 
            helperAccess.PopuplateDropDownCheckBox(ddlClass_Type, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass_Type','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code");

            try
            {
                string classtype = GetSelectedCheckedValue(ddlClass_Type);
                helperAccess.PopuplateDropDownCheckBox(ddlClass, "pr_GetDropDownListByControlNameModuleType_MultiClass 'M','ddlClass','" + lblSectionId.Text + "','" + classtype + "'", "LookUp_Desc", "LookUp_Code");
            }
            catch (Exception ex)
            { _log.Error("PopuplateDropDownList1", ex); }

            SubPopupateDropDown();
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void SubPopupateDropDown()
    {
        try
        {
            //Added  IND_DT14012020 
            helperAccess.PopuplateDropDownListCV(ddlDrugCategory, "pr_GetDropDownListByControlNameModuleType 'M','ddlDrugCategory','" + lblSectionId.Text + "','0000003275'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlLPIMaterialIdentifier, "pr_GetDropDownListByControlNameModuleType 'M','ddlLPIMaterialIdentifier','" + lblSectionId.Text + "','0000002268'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlMaterialCategoryA3PL, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialCategoryA3PL','" + lblSectionId.Text + "','0000002865'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlMaterialCategoryB3PL, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialCategoryB3PL','" + lblSectionId.Text + "','0000002866'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlMaterialGroupingforMES, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroupingforMES','" + lblSectionId.Text + "','0000002289'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlStorageCond, "pr_GetDropDownListByControlNameModuleType 'M','ddlStorageCond','" + lblSectionId.Text + "','0000003499'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlMaterialGroupingforMES, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroupingforMES','" + lblSectionId.Text + "','L0IN'", "LookUp_Desc", "LookUp_Code", "");

            //PROV-CCP-MM-941-23-0045 Start
            //helperAccess.PopuplateDropDownListCV(ddlKXSBU, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXSBU','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXMARKT, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXMARKT','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXSELLCTRY, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXSELLCTRY','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXBUSI, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXBUSI','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXDIV, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXDIV','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXTHER, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXTHER','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXDOSFRM, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXDOSFRM','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlKXMINSL, "pr_GetDropDownListByControlNameModuleType 'M','ddlKXMINSL','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownListCV(ddlMKTMNGER, "pr_GetDropDownListByControlNameModuleType 'M','ddlMKTMNGER','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlCS_MOLECULE, "pr_GetDropDownListByControlNameModuleType 'M','ddlCS_MOLECULE ','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownListCV(ddlMGRPPX, "pr_GetDropDownListByControlNameModuleType 'M','ddlMGRPPX','" + lblSectionId.Text + "'", "LookUp_Desc", "LookUp_Code", "");
            //PROV-CCP-MM-941-23-0045 End


        }
        catch (Exception ex)
        { _log.Error("SubPopupateDropDown", ex); }
    }

    private void SetFieldsByClassification()
    {
        try
        {

            bool flg = false;
            bool flg1 = false;
            bool bMCLASS = false;
            bool bSLED = false;
            string sub1 = lblc1ddlClass.Text;
            try
            {

                string sub2 = "MCLASS";
                bMCLASS = sub1.Contains(sub2);
                if (bMCLASS == false)
                {

                    //MSC_8300001775 Start Comment 
                    //string sub22 = "MCLASS            ";
                    //MSC_8300001775 End Comment
                    //MSC_8300001775 Start
                    string sub22 = "MCLASS";
                    //MSC_8300001775 End
                    bMCLASS = sub1.Contains(sub22);
                }
            }
            catch (Exception ex)
            { _log.Error("SetFieldsByClassification", ex); }
            try
            {
                string sub3 = "SLED";
                bSLED = sub1.Contains(sub3);
                if (bSLED == false)
                {

                    //MSC_8300001775 Start Comment 
                    //string sub33 = "SLED              ";
                    //MSC_8300001775 End Comment
                    //MSC_8300001775 Start
                    string sub33 = "SLED";
                    //MSC_8300001775 End
                    bSLED = sub1.Contains(sub33);
                }
            }
            catch (Exception ex)
            { _log.Error("SetFieldsByClassification1", ex); }
            // if (ddlClass.SelectedValue == "MCLASS            ")

            if (bMCLASS)
            {
                flg = true;
            }
            else
            {
                flg = false;
                txtStrengthofmatPacktype.Text = "";
                txtMarket.Text = "";
                txtNDCNoLPI.Text = "";
                txtNDCNoLL.Text = "";
                txtHTS.Text = "";
                txtANDA.Text = "";
                txtFDANo.Text = "";
                txtShortdescriptionfor3PL.Text = "";
                txtPackagepresentation3PL.Text = "";
                txtNumberofTablet3PL.Text = "";
                txtSortingforinventoryreport.Text = "";
                txtPacksize.Text = "";
                txtProductGroup.Text = "";
                txtMarketEntryDate.Text = "";
                txtPZNHORMOSAN.Text = "";
                txtMANUFACTURER.Text = "";

                SubPopupateDropDown();
                //ddlDrugCategory.Text = "";
                //ddlStorageCond.Text = "";
                //ddlMaterialCategoryA3PL.Text = "";
                //ddlMaterialCategoryB3PL.Text = "";
                //ddlLPIMaterialIdentifier.Text = "";
                //ddlMaterialGroupingforMES.Text = "";
            }
            txtStrengthofmatPacktype.Enabled = flg;
            txtMarket.Enabled = flg;
            txtNDCNoLPI.Enabled = flg;
            txtNDCNoLL.Enabled = flg;
            txtHTS.Enabled = flg;
            txtANDA.Enabled = flg;
            txtFDANo.Enabled = flg;
            ddlLPIMaterialIdentifier.Enabled = flg;
            ddlMaterialGroupingforMES.Enabled = flg;
            /*MES Mandatory for MCLASS Start*/
            if ((Session[StaticKeys.SelectedModuleId].ToString() == "162") || (Session[StaticKeys.SelectedModuleId].ToString() == "164"))
            {
                reqddlMaterialGroupingforMES.Visible = flg;
                lableddlMaterialGroupingforMES.Visible = flg;
            }
            /*MES Mandatory for MCLASS End*/
            txtShortdescriptionfor3PL.Enabled = flg;
            txtPackagepresentation3PL.Enabled = flg;
            txtNumberofTablet3PL.Enabled = flg;
            ddlMaterialCategoryA3PL.Enabled = flg;
            ddlMaterialCategoryB3PL.Enabled = flg;
            txtSortingforinventoryreport.Enabled = flg;
            txtPacksize.Enabled = flg;
            txtProductGroup.Enabled = flg;
            ddlDrugCategory.Enabled = flg;
            txtMarketEntryDate.Enabled = flg;
            txtPZNHORMOSAN.Enabled = flg;
            txtMANUFACTURER.Enabled = flg;
            ddlStorageCond.Enabled = flg;
            reqddlStorageCond.Visible = flg;
            lableddlStorageCond.Visible = flg;

            //PROV-CCP-MM-941-23-0045 Start
            ddlMGRPPX.Enabled = flg;
            txtKXSBU.Enabled = flg;
            txtKXMARKT.Enabled = flg;
            txtKXSELLCTRY.Enabled = flg;
            txtKXBUSI.Enabled = flg;
            txtKXDIV.Enabled = flg;

            txtKXTHER.Enabled = flg;
            txtKXDOSFRM.Enabled = flg;
            txtKXMINSL.Enabled = flg;
            txtMKTMNGER.Enabled = flg;
            //ddlKXTHER.Enabled = flg;
            //ddlKXDOSFRM.Enabled = flg;
            //ddlKXMINSL.Enabled = flg;
            //ddlMKTMNGER.Enabled = flg;
            ddlCS_MOLECULE.Enabled = flg;
            rfvtxtKXSBU.Visible = flg;
            rfvtxtKXMARKT.Visible = flg;
            rfvtxtKXSELLCTRY.Visible = flg;
            rfvtxtKXBUSI.Visible = flg;
            rfvtxtKXDIV.Visible = flg;

            rfvtxtKXTHER.Visible = flg;
            rfvtxtKXDOSFRM.Visible = flg;
            rfvtxtKXMINSL.Visible = flg;
            rfvtxtMKTMNGER.Visible = flg;
            //rfvddlKXTHER.Visible = flg;
            //rfvddlKXDOSFRM.Visible = flg;
            //rfvddlKXMINSL.Visible = flg;
            //rfvddlMKTMNGER.Visible = flg;
            rfvddlCS_MOLECULE.Visible = flg;
            //PROV-CCP-MM-941-23-0045 End



            //if (ddlClass.SelectedValue == "SLED              ")
            if (bSLED)
            {
                flg1 = false;
            }
            else
            {
                flg1 = false;
                txtAllowed_Manufacturers.Text = "";
                txtHSAN_MATERIAL_IDENTIFIER.Text = "";
                txtExpiration_date_shelf_life.Text = "";
                txtNext_Insp_Date_for_Batch.Text = "";
                txtBatch_number.Text = "";
                txtASSAY_ASIS.Text = "";
                txtMANUFACTURER.Text = "";
                txtPotency_as_is_basis.Text = "";
                txtLoss_on_Drying.Text = "";
                txtPotency_as_is_basis1.Text = "";
                txtRM402217.Text = "";
                txtRM323350.Text = "";
                txtSF900052.Text = "";
                txtSF110063.Text = "";
                txtIP4A0047.Text = "";
                txtAssay_by_GC.Text = "";
                txtExternal_Material_Group.Text = "";
                txtVersion_Number.Text = "";
            }

            txtAllowed_Manufacturers.Enabled = flg1;
            txtHSAN_MATERIAL_IDENTIFIER.Enabled = flg1;
            txtExpiration_date_shelf_life.Enabled = flg1;
            txtNext_Insp_Date_for_Batch.Enabled = flg1;
            txtBatch_number.Enabled = flg1;
            txtASSAY_ASIS.Enabled = flg1;
            txtMANUFACTURER.Enabled = flg1;
            txtPotency_as_is_basis.Enabled = flg1;
            txtLoss_on_Drying.Enabled = flg1;
            txtPotency_as_is_basis1.Enabled = flg1;
            txtRM402217.Enabled = flg1;
            txtRM323350.Enabled = flg1;
            txtSF900052.Enabled = flg1;
            txtSF110063.Enabled = flg1;
            txtIP4A0047.Enabled = flg1;
            txtAssay_by_GC.Enabled = flg1;
            txtExternal_Material_Group.Enabled = flg1;
            txtVersion_Number.Enabled = flg1;

        }
        catch (Exception ex) { _log.Error("SetFieldsByClassification3", ex); }

        //ConfigureControlForSChange();
        //MSC_8300001775 Start 
        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
        {
            ConfigureControlForSChange();
        }
        //MSC_8300001775 End
    }

    #region Get

    private void FillClassificationDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjClassificationAccess.GetClassificationData(Convert.ToInt32(lblMasterHeaderId.Text));

            grvClassification.DataSource = ds;
            grvClassification.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("FillClassificationDataGrid", ex);
        }
    }

    private void FillClassificationData()
    {
        try
        {
            Classification ObjClassification = GetClassificationData();
            if (ObjClassification.Mat_Classification_Id > 0)
            {
                lblClassificationId.Text = ObjClassification.Mat_Classification_Id.ToString();

                BindPlantWiseDropDown();

                //Old Commented  IND_DT14012020 
                //ddlClass_Type.SelectedValue = ObjClassification.Class_Type;
                //helperAccess.PopuplateDropDownList(ddlClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass','" + lblSectionId.Text + "','" + ddlClass_Type.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                //Old Commented  IND_DT14012020 
                //Added IND_DT14012020  
                SetSelectedValue(ddlClass_Type, ObjClassification.Class_Type);

                try
                {
                    string classtype = GetSelectedCheckedValue(ddlClass_Type);
                    helperAccess.PopuplateDropDownCheckBox(ddlClass, "pr_GetDropDownListByControlNameModuleType_MultiClass 'M','ddlClass','" + lblSectionId.Text + "','" + classtype + "'", "LookUp_Desc", "LookUp_Code");
                }
                catch (Exception ex)
                { }

                //Added  IND_DT14012020 
                //Old Commented  IND_DT14012020 
                //ddlClass.SelectedValue = ObjClassification.Class;
                //Old Commented  IND_DT14012020 
                //IND_DT14012020 Added

                SetSelectedValue(ddlClass, ObjClassification.Class);
                //Added IND_DT14012020 


                txtStrengthofmatPacktype.Text = ObjClassification.Strength_of_mat_Pack_type;
                txtMarket.Text = ObjClassification.Market;
                txtNDCNoLPI.Text = ObjClassification.NDC_No_LPI;
                txtNDCNoLL.Text = ObjClassification.NDC_No_LL;
                txtHTS.Text = ObjClassification.HTS;
                txtANDA.Text = ObjClassification.ANDA;
                txtFDANo.Text = ObjClassification.FDA_No;
                ddlLPIMaterialIdentifier.SelectedValue = ObjClassification.LPI_Material_Identifier;
                ddlMaterialGroupingforMES.SelectedValue = ObjClassification.Material_Grouping_for_MES;
                txtShortdescriptionfor3PL.Text = ObjClassification.Short_description_for_3PL;
                txtPackagepresentation3PL.Text = ObjClassification.Package_Presentation_3PL;
                txtNumberofTablet3PL.Text = ObjClassification.Number_of_Tablet_3PL;
                ddlMaterialCategoryA3PL.SelectedValue = ObjClassification.Material_Category_A_3PL;
                ddlMaterialCategoryB3PL.SelectedValue = ObjClassification.Material_Category_B_3PL;
                txtSortingforinventoryreport.Text = ObjClassification.Sorting_for_inventory_report;
                txtPacksize.Text = ObjClassification.Pack_size;
                txtProductGroup.Text = ObjClassification.Product_Group;
                ddlDrugCategory.SelectedValue = ObjClassification.DRUG_CATEGORY;
                txtMarketEntryDate.Text = ObjClassification.MARKET_ENTRY_DATE;
                txtPZNHORMOSAN.Text = ObjClassification.PZN_HORMOSAN;
                ddlStorageCond.SelectedValue = ObjClassification.StorageCondition;

                txtAllowed_Manufacturers.Text = ObjClassification.Allowed_Manufacturers;
                txtHSAN_MATERIAL_IDENTIFIER.Text = ObjClassification.HSAN_MATERIAL_IDENTIFIER;
                txtExpiration_date_shelf_life.Text = ObjClassification.Expiration_date_shelf_life;
                txtNext_Insp_Date_for_Batch.Text = ObjClassification.Next_Insp_Date_for_Batch;
                txtBatch_number.Text = ObjClassification.Batch_number;
                txtASSAY_ASIS.Text = ObjClassification.ASSAY_ASIS;
                txtMANUFACTURER.Text = ObjClassification.MANUFACTURER;
                txtPotency_as_is_basis.Text = ObjClassification.Potency_as_is_basis;
                txtLoss_on_Drying.Text = ObjClassification.Loss_on_Drying;
                txtPotency_as_is_basis1.Text = ObjClassification.Potency_as_is_basis1;
                txtRM402217.Text = ObjClassification.RM402217;
                txtRM323350.Text = ObjClassification.RM323350;
                txtSF900052.Text = ObjClassification.SF110063;
                txtSF110063.Text = ObjClassification.SF900052;
                txtIP4A0047.Text = ObjClassification.IP4A0047;
                txtAssay_by_GC.Text = ObjClassification.Assay_by_GC;
                txtExternal_Material_Group.Text = ObjClassification.External_Material_Group;
                txtVersion_Number.Text = ObjClassification.Version_Number;

                //PROV-CCP-MM-941-23-0045 Start
                txtKXSBU.Text = ObjClassification.sKXSBU;
                txtKXMARKT.Text = ObjClassification.sKXMARKT;
                txtKXSELLCTRY.Text = ObjClassification.sKXSELLCTRY;
                txtKXBUSI.Text = ObjClassification.sKXBUSI;
                txtKXDIV.Text = ObjClassification.sKXDIV;

                txtKXTHER.Text = ObjClassification.sKXTHER;
                txtKXDOSFRM.Text = ObjClassification.sKXDOSFRM;
                txtKXMINSL.Text = ObjClassification.sKXMINSL;
                txtMKTMNGER.Text = ObjClassification.sMKTMNGER;

                //ddlKXTHER.SelectedValue = ObjClassification.sKXTHER;
                //ddlKXDOSFRM.SelectedValue = ObjClassification.sKXDOSFRM;
                //ddlKXMINSL.SelectedValue = ObjClassification.sKXMINSL;
                //ddlMKTMNGER.SelectedValue = ObjClassification.sMKTMNGER;
                ddlCS_MOLECULE.SelectedValue = ObjClassification.sCS_MOLECULE;
                ddlMGRPPX.SelectedValue = ObjClassification.sMGRPPX;
                //PROV-CCP-MM-941-23-0045 End


                //JPY_DT26112019 Added
                DisplayClass_Type();
                DisplayClass();
                //JPY_DT26112019 Added

            }
            else
            {
                lblClassificationId.Text = "0";

                if (Session[StaticKeys.SelectedModuleId].ToString() == "162" || Session[StaticKeys.SelectedModuleId].ToString() == "164")
                {
                    //Old Commented  IND_DT14012020 
                    //ddlClass_Type.SelectedValue = "001";
                    //helperAccess.PopuplateDropDownList(ddlClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass','" + lblSectionId.Text + "','" + ddlClass_Type.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                    //ddlClass.SelectedValue = "MCLASS            ";
                    //Old Commented  IND_DT14012020 
                    //Added  IND_DT14012020 
                    try
                    {
                        ddlClass_Type.SelectedValue = "001";
                        string classtype = GetSelectedCheckedValue(ddlClass_Type);
                        helperAccess.PopuplateDropDownCheckBox(ddlClass, "pr_GetDropDownListByControlNameModuleType_MultiClass 'M','ddlClass','" + lblSectionId.Text + "','" + classtype + "'", "LookUp_Desc", "LookUp_Code");

                        //MSC_8300001775 Start Comment
                        //ddlClass.SelectedValue = "MCLASS            ";
                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        ddlClass.SelectedValue = "MCLASS";
                        //MSC_8300001775 End

                    }
                    catch (Exception ex)
                    { }

                    //Added  IND_DT14012020 
                }
                else
                {
                    //Old Commented  IND_DT14012020 
                    //ddlClass_Type.SelectedValue = "023";
                    //helperAccess.PopuplateDropDownList(ddlClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlClass','" + lblSectionId.Text + "','" + ddlClass_Type.SelectedValue + "'", "LookUp_Desc", "LookUp_Code", "");
                    //ddlClass.SelectedValue = "SLED              ";
                    //Old Commented  IND_DT14012020 
                    //Added  IND_DT14012020 
                    try
                    {
                        ddlClass_Type.SelectedValue = "023";
                        string classtype = GetSelectedCheckedValue(ddlClass_Type);
                        helperAccess.PopuplateDropDownCheckBox(ddlClass, "pr_GetDropDownListByControlNameModuleType_MultiClass 'M','ddlClass','" + lblSectionId.Text + "','" + classtype + "'", "LookUp_Desc", "LookUp_Code");

                        //MSC_8300001775 Start Comment 
                        //ddlClass.SelectedValue = "SLED              ";
                        //MSC_8300001775 End Comment
                        //MSC_8300001775 Start
                        ddlClass.SelectedValue = "SLED";
                        //MSC_8300001775 End
                    }
                    catch (Exception ex)
                    { }
                    //Added  IND_DT14012020 
                }

            }
            SetFieldsByClassification();

        }
        catch (Exception ex)
        {
            _log.Error("FillClassificationData", ex);
        }
    }

    private void ClearClassificationData()
    {
        try
        {
            lblClassificationId.Text = "0";
            txtStrengthofmatPacktype.Text = "";
            txtMarket.Text = "";
            txtNDCNoLPI.Text = "";
            txtNDCNoLL.Text = "";
            txtHTS.Text = "";
            txtANDA.Text = "";
            txtFDANo.Text = "";
            txtShortdescriptionfor3PL.Text = "";
            txtPackagepresentation3PL.Text = "";
            txtNumberofTablet3PL.Text = "";
            txtSortingforinventoryreport.Text = "";
            txtPacksize.Text = "";
            txtProductGroup.Text = "";
            txtMarketEntryDate.Text = "";
            txtPZNHORMOSAN.Text = "";

            //PROV-CCP-MM-941-23-0045 Start
            txtKXSBU.Text = "";
            txtKXMARKT.Text = "";
            txtKXSELLCTRY.Text = "";
            txtKXBUSI.Text = "";
            txtKXDIV.Text = "";
            txtKXTHER.Text = "";
            txtKXDOSFRM.Text = "";
            txtKXMINSL.Text = "";
            txtMKTMNGER.Text = "";
            //PROV-CCP-MM-941-23-0045 End

            //IND_DT14012020 Add 
            ClearSelectedValue(ddlClass_Type);
            ClearSelectedValue(ddlClass);
            //IND_DT14012020 Add
            PopuplateDropDownList();
        }
        catch (Exception ex)
        {
            _log.Error("ClearClassificationData", ex);
        }
    }

    private Classification GetClassificationData()
    {
        return ObjClassificationAccess.GetClassification(Convert.ToInt32(lblClassificationId.Text)); //Convert.ToInt32(ddlPlant.SelectedValue));
    }

    #endregion

    #region Save

    private bool Save()
    {
        bool flg = false;
        try
        {

            //IND_DT14012020
            if (ValidClass() == true)
            {
                try
                {
                    Classification ObjClassification = GetControlsValue();
                    objSavedClassification = GetClassificationData();

                    if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                    {
                        if (objSavedClassification.Mat_Classification_Id > 0)
                        {
                            CheckIfChanges(ObjClassification, objSavedClassification);
                        }
                    }

                    if (ObjClassificationAccess.Save(ObjClassification) > 0)
                    {
                        //MSC_8300001775
                        //if (HelperAccess.ReqType == "SMC")
                        if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                        {
                            CheckIfChangesLog(ObjClassification, objSavedClassification);
                        }
                        //MSC_8300001775

                        ClearClassificationData();
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
                catch (Exception ex)
                {
                    _log.Error("Save1", ex);
                }
            }
            else
            {
                //ClientScriptManager cs = Page.ClientScript; 
                //// Check to see if the startup script is already registered.
                //if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                //{
                //    cs.RegisterStartupScript(GetType(), "key", "alert('Please select valid Class.');", true);
                //} 

                //txtforValid.Text = "";
                //rfvforValid.Visible = true;
                //rfvforValid.Enabled = true;

                //string message = "Please select valid Class.";
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append("alert('");
                //sb.Append(message);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                lblMsg.Text = "Please select valid Class.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("Save", ex); }
        return flg;
    }

    /// <summary>
    /// IND_DT14012020
    /// Check valid class selected or not
    /// </summary>
    /// <returns></returns>
    private bool ValidClass()
    {
        bool ctVal = false;
        CheckBoxList chkListct = new CheckBoxList();
        chkListct = ddlClass_Type;

        CheckBoxList chkListc = new CheckBoxList();
        chkListc = ddlClass;
        //int ctbCount = 0;
        //int ctCount = 0;
        try
        {

            foreach (ListItem itemct in chkListct.Items)
            {
                lblspclasstype.Text = "";
                //ctbCount++;
                if (itemct.Selected)// && (ctbCount > ctCount))
                {
                    //ctCount = 0;
                    DataSet dsct = new DataSet();
                    string spclasstype = itemct.Value;
                    dsct = ObjClassificationAccess.GetClassList("M", "ddlClass", lblSectionId.Text, spclasstype);

                    foreach (DataTable table in dsct.Tables)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            if (lblspclasstype.Text == "")
                            {
                                lblspclasstype.Text = dr["LookUp_Code"].ToString();
                            }
                            else
                            {
                                lblspclasstype.Text = lblspclasstype.Text + "," + dr["LookUp_Code"].ToString();
                            }
                        }
                    }

                    string sclasstype1 = lblspclasstype.Text;

                    //string Class_TypeType = GetSelectedCheckedValue(ddlClass_Type);
                    //if (Class_TypeType != null)
                    //{
                    //    lblc1ddlClass_Type.Text = "Class :- " + Class_TypeType.Substring(0, Class_TypeType.Length - 1);

                    //}
                    foreach (ListItem itemc in chkListc.Items)
                    {
                        string spclass = itemc.Value;
                        if (itemc.Selected && sclasstype1.Contains(spclass))
                        {
                            ctVal = true;
                            break;
                        }
                        else
                        {
                            ctVal = false;
                        }
                    }
                    if (ctVal == false)
                    {
                        break;
                    }
                }
                //ctCount++;
            }


        }
        catch (Exception ex)
        { _log.Error("ValidClass", ex); }

        //string Class_TypeType = GetSelectedCheckedValue(ddlClass_Type);
        //if (Class_TypeType != null)
        //{
        //    lblc1ddlClass_Type.Text = "Class :- " + Class_TypeType.Substring(0, Class_TypeType.Length - 1);

        //}
        //else
        //{
        //    lblc1ddlClass_Type.Text = "";
        //}

        //bool bMCLASS = false;
        //bool bSLED = false;

        //string vClass = lblc1ddlClass.Text;
        //string vClassType = lblc1ddlClass_Type.Text;
        //try
        //{

        //    string sub2 = "MCLASS";
        //    bMCLASS = vClass.Contains(sub2);
        //}
        //catch (Exception ex)
        //{ }

        return ctVal;
    }

    private void FillFormDataByMHId()
    {
        DataSet ds;
        try
        {
            ds = ObjClassificationAccess.GetClassificationData(Convert.ToInt32(lblMasterHeaderId.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblClassificationId.Text = ds.Tables[0].Rows[0]["Mat_Classification_Id"].ToString();
            }
            FillClassificationData();
        }
        catch (Exception ex)
        { _log.Error("FillFormDataByMHId", ex); }
    }

    private void BindPlantWiseDropDown()
    {
        //
    }

    private Classification GetControlsValue()
    {
        Classification ObjClassification = new Classification();
        Utility objUtil = new Utility();

        try
        {
            ObjClassification.Mat_Classification_Id = Convert.ToInt32(lblClassificationId.Text);
            ObjClassification.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            //IND_DT14012020 Commented
            //ObjClassification.Class_Type = ddlClass_Type.SelectedValue;
            //ObjClassification.Class = ddlClass.SelectedValue;
            //IND_DT14012020 Commented
            //IND_DT14012020 Added
            ObjClassification.Class_Type = GetSelectedCheckedValue(ddlClass_Type) == null ? "" : GetSelectedCheckedValue(ddlClass_Type);
            ObjClassification.Class = GetSelectedCheckedValue(ddlClass) == null ? "" : GetSelectedCheckedValue(ddlClass);
            //IND_DT14012020 Added

            ObjClassification.Strength_of_mat_Pack_type = txtStrengthofmatPacktype.Text;
            ObjClassification.Market = txtMarket.Text;
            ObjClassification.NDC_No_LPI = txtNDCNoLPI.Text;
            ObjClassification.NDC_No_LL = txtNDCNoLL.Text;
            ObjClassification.HTS = txtHTS.Text;
            ObjClassification.ANDA = txtANDA.Text;
            ObjClassification.FDA_No = txtFDANo.Text;
            ObjClassification.LPI_Material_Identifier = ddlLPIMaterialIdentifier.SelectedValue;
            ObjClassification.Material_Grouping_for_MES = ddlMaterialGroupingforMES.SelectedValue;
            ObjClassification.Short_description_for_3PL = txtShortdescriptionfor3PL.Text;
            ObjClassification.Package_Presentation_3PL = txtPackagepresentation3PL.Text;
            ObjClassification.Number_of_Tablet_3PL = txtNumberofTablet3PL.Text;
            ObjClassification.Material_Category_A_3PL = ddlMaterialCategoryA3PL.SelectedValue;
            ObjClassification.Material_Category_B_3PL = ddlMaterialCategoryB3PL.SelectedValue;
            ObjClassification.Sorting_for_inventory_report = txtSortingforinventoryreport.Text;
            ObjClassification.Pack_size = txtPacksize.Text;
            ObjClassification.Product_Group = txtProductGroup.Text;
            ObjClassification.DRUG_CATEGORY = ddlDrugCategory.SelectedValue;
            ObjClassification.MARKET_ENTRY_DATE = objUtil.GetYYYYMMDD(txtMarketEntryDate.Text);
            ObjClassification.PZN_HORMOSAN = txtPZNHORMOSAN.Text;
            ObjClassification.StorageCondition = ddlStorageCond.SelectedValue;

            ObjClassification.Allowed_Manufacturers = txtAllowed_Manufacturers.Text;
            ObjClassification.HSAN_MATERIAL_IDENTIFIER = txtHSAN_MATERIAL_IDENTIFIER.Text;
            ObjClassification.Expiration_date_shelf_life = txtExpiration_date_shelf_life.Text;
            ObjClassification.Next_Insp_Date_for_Batch = txtNext_Insp_Date_for_Batch.Text;
            ObjClassification.Batch_number = txtBatch_number.Text;
            ObjClassification.ASSAY_ASIS = txtASSAY_ASIS.Text;
            ObjClassification.MANUFACTURER = txtMANUFACTURER.Text;
            ObjClassification.Potency_as_is_basis = txtPotency_as_is_basis.Text;
            ObjClassification.Loss_on_Drying = txtLoss_on_Drying.Text;
            ObjClassification.Potency_as_is_basis1 = txtPotency_as_is_basis1.Text;
            ObjClassification.RM402217 = txtRM402217.Text;
            ObjClassification.RM323350 = txtRM323350.Text;
            ObjClassification.SF110063 = txtSF900052.Text;
            ObjClassification.SF900052 = txtSF110063.Text;
            ObjClassification.IP4A0047 = txtIP4A0047.Text;
            ObjClassification.Assay_by_GC = txtAssay_by_GC.Text;
            ObjClassification.External_Material_Group = txtExternal_Material_Group.Text;
            ObjClassification.Version_Number = txtVersion_Number.Text;
            //PROV-CCP-MM-941-23-0045 Start
            //ObjClassification.sKXSBU = ddlKXSBU.SelectedValue;
            //ObjClassification.sKXMARKT = ddlKXMARKT.SelectedValue;
            //ObjClassification.sKXSELLCTRY = ddlKXSELLCTRY.SelectedValue;
            //ObjClassification.sKXBUSI = ddlKXBUSI.SelectedValue;
            //ObjClassification.sKXDIV = ddlKXDIV.SelectedValue;

            ObjClassification.sKXSBU = txtKXSBU.Text.Trim();
            ObjClassification.sKXMARKT = txtKXMARKT.Text.Trim();
            ObjClassification.sKXSELLCTRY = txtKXSELLCTRY.Text.Trim();
            ObjClassification.sKXBUSI = txtKXBUSI.Text.Trim();
            ObjClassification.sKXDIV = txtKXDIV.Text.Trim();

            ObjClassification.sKXTHER = txtKXTHER.Text.Trim();
            ObjClassification.sKXDOSFRM = txtKXDOSFRM.Text.Trim();
            ObjClassification.sKXMINSL = txtKXMINSL.Text.Trim();
            ObjClassification.sMKTMNGER = txtMKTMNGER.Text.Trim();
            //ObjClassification.sKXTHER = ddlKXTHER.SelectedValue;
            //ObjClassification.sKXDOSFRM = ddlKXDOSFRM.SelectedValue;
            //ObjClassification.sKXMINSL = ddlKXMINSL.SelectedValue;
            //ObjClassification.sMKTMNGER = ddlMKTMNGER.SelectedValue;
            ObjClassification.sCS_MOLECULE = ddlCS_MOLECULE.SelectedValue;
            ObjClassification.sMGRPPX = ddlMGRPPX.SelectedValue;
            //PROV-CCP-MM-941-23-0045 End
            ObjClassification.IsActive = 1;
            ObjClassification.UserId = lblUserId.Text;
            ObjClassification.TodayDate = objUtil.GetDate();
            ObjClassification.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjClassification;
    }

    #endregion

    private void ConfigureControl()
    {
        try
        {
            string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
            SectionConfiguration.Classification obj = new SectionConfiguration.Classification();
            SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }

        KinaxisFiels();
    }

    private void CheckIfChanges(Classification NewClassificationData, Classification oldClassificationData)
    {
        try
        {
            if (NewClassificationData.Mat_Classification_Id > 0 && oldClassificationData.Mat_Classification_Id > 0)
            {
                if (NewClassificationData.Class_Type != oldClassificationData.Class_Type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Class Type</td> <td>" + oldClassificationData.Class_Type + "</td><td>" + NewClassificationData.Class_Type + "</td></tr>";
                if (NewClassificationData.Class != oldClassificationData.Class)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Class</td> <td>" + oldClassificationData.Class + "</td><td>" + NewClassificationData.Class + "</td></tr>";
                if (NewClassificationData.Strength_of_mat_Pack_type != oldClassificationData.Strength_of_mat_Pack_type)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Strength of mat Pack type</td> <td>" + oldClassificationData.Strength_of_mat_Pack_type + "</td><td>" + NewClassificationData.Strength_of_mat_Pack_type + "</td></tr>";
                if (NewClassificationData.Market != oldClassificationData.Market)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Market</td> <td>" + oldClassificationData.Market + "</td><td>" + NewClassificationData.Market + "</td></tr>";
                if (NewClassificationData.NDC_No_LPI != oldClassificationData.NDC_No_LPI)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>NDC No LPI</td> <td>" + oldClassificationData.NDC_No_LPI + "</td><td>" + NewClassificationData.NDC_No_LPI + "</td></tr>";
                if (NewClassificationData.NDC_No_LL != oldClassificationData.NDC_No_LL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>NDC No LL</td> <td>" + oldClassificationData.NDC_No_LL + "</td><td>" + NewClassificationData.NDC_No_LL + "</td></tr>";
                if (NewClassificationData.HTS != oldClassificationData.HTS)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>HTS</td> <td>" + oldClassificationData.HTS + "</td><td>" + NewClassificationData.HTS + "</td></tr>";
                if (NewClassificationData.ANDA != oldClassificationData.ANDA)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>ANDA</td> <td>" + oldClassificationData.ANDA + "</td><td>" + NewClassificationData.ANDA + "</td></tr>";
                if (NewClassificationData.FDA_No != oldClassificationData.FDA_No)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>FDA No</td> <td>" + oldClassificationData.FDA_No + "</td><td>" + NewClassificationData.FDA_No + "</td></tr>";
                if (NewClassificationData.LPI_Material_Identifier != oldClassificationData.LPI_Material_Identifier)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>LPI Material Identifier</td> <td>" + oldClassificationData.LPI_Material_Identifier + "</td><td>" + NewClassificationData.LPI_Material_Identifier + "</td></tr>";
                if (NewClassificationData.Material_Grouping_for_MES != oldClassificationData.Material_Grouping_for_MES)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Grouping for MES</td> <td>" + oldClassificationData.Material_Grouping_for_MES + "</td><td>" + NewClassificationData.Material_Grouping_for_MES + "</td></tr>";
                if (NewClassificationData.Short_description_for_3PL != oldClassificationData.Short_description_for_3PL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Short description for 3PL</td> <td>" + oldClassificationData.Short_description_for_3PL + "</td><td>" + NewClassificationData.Short_description_for_3PL + "</td></tr>";
                if (NewClassificationData.Package_Presentation_3PL != oldClassificationData.Package_Presentation_3PL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Package Presentation 3PL</td> <td>" + oldClassificationData.Package_Presentation_3PL + "</td><td>" + NewClassificationData.Package_Presentation_3PL + "</td></tr>";
                if (NewClassificationData.Number_of_Tablet_3PL != oldClassificationData.Number_of_Tablet_3PL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Number of Tablet 3PL</td> <td>" + oldClassificationData.Number_of_Tablet_3PL + "</td><td>" + NewClassificationData.Number_of_Tablet_3PL + "</td></tr>";
                if (NewClassificationData.Material_Category_A_3PL != oldClassificationData.Material_Category_A_3PL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Category A 3PL</td> <td>" + oldClassificationData.Material_Category_A_3PL + "</td><td>" + NewClassificationData.Material_Category_A_3PL + "</td></tr>";
                if (NewClassificationData.Material_Category_B_3PL != oldClassificationData.Material_Category_B_3PL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Category B 3PL</td> <td>" + oldClassificationData.Material_Category_B_3PL + "</td><td>" + NewClassificationData.Material_Category_B_3PL + "</td></tr>";
                if (NewClassificationData.Sorting_for_inventory_report != oldClassificationData.Sorting_for_inventory_report)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Sorting for Inventory Report</td> <td>" + oldClassificationData.Sorting_for_inventory_report + "</td><td>" + NewClassificationData.Sorting_for_inventory_report + "</td></tr>";
                if (NewClassificationData.Pack_size != oldClassificationData.Pack_size)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Pack size</td> <td>" + oldClassificationData.Pack_size + "</td><td>" + NewClassificationData.Pack_size + "</td></tr>";
                if (NewClassificationData.DRUG_CATEGORY != oldClassificationData.DRUG_CATEGORY)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Drug Category</td> <td>" + oldClassificationData.DRUG_CATEGORY + "</td><td>" + NewClassificationData.DRUG_CATEGORY + "</td></tr>";
                if (NewClassificationData.Product_Group != oldClassificationData.Product_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Product Group</td> <td>" + oldClassificationData.Product_Group + "</td><td>" + NewClassificationData.Product_Group + "</td></tr>";
                if (NewClassificationData.MARKET_ENTRY_DATE != oldClassificationData.MARKET_ENTRY_DATE)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Market Entry Date</td> <td>" + oldClassificationData.MARKET_ENTRY_DATE + "</td><td>" + NewClassificationData.MARKET_ENTRY_DATE + "</td></tr>";
                if (NewClassificationData.PZN_HORMOSAN != oldClassificationData.PZN_HORMOSAN)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>PZN HORMOSAN</td> <td>" + oldClassificationData.PZN_HORMOSAN + "</td><td>" + NewClassificationData.PZN_HORMOSAN + "</td></tr>";
                if (NewClassificationData.StorageCondition != oldClassificationData.StorageCondition)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Storage Condition</td> <td>" + oldClassificationData.StorageCondition + "</td><td>" + NewClassificationData.StorageCondition + "</td></tr>";
                if (NewClassificationData.Allowed_Manufacturers != oldClassificationData.Allowed_Manufacturers)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Allowed Manufacturers</td> <td>" + oldClassificationData.Allowed_Manufacturers + "</td><td>" + NewClassificationData.Allowed_Manufacturers + "</td></tr>";
                if (NewClassificationData.HSAN_MATERIAL_IDENTIFIER != oldClassificationData.HSAN_MATERIAL_IDENTIFIER)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>HSAN MATERIAL IDENTIFIER</td> <td>" + oldClassificationData.HSAN_MATERIAL_IDENTIFIER + "</td><td>" + NewClassificationData.HSAN_MATERIAL_IDENTIFIER + "</td></tr>";
                if (NewClassificationData.Expiration_date_shelf_life != oldClassificationData.Expiration_date_shelf_life)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Expiration date shelf life</td> <td>" + oldClassificationData.Expiration_date_shelf_life + "</td><td>" + NewClassificationData.Expiration_date_shelf_life + "</td></tr>";
                if (NewClassificationData.Next_Insp_Date_for_Batch != oldClassificationData.Next_Insp_Date_for_Batch)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Next Insp Date for Batch</td> <td>" + oldClassificationData.Next_Insp_Date_for_Batch + "</td><td>" + NewClassificationData.Next_Insp_Date_for_Batch + "</td></tr>";
                if (NewClassificationData.Batch_number != oldClassificationData.Batch_number)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Batch number</td> <td>" + oldClassificationData.Batch_number + "</td><td>" + NewClassificationData.Batch_number + "</td></tr>";
                if (NewClassificationData.ASSAY_ASIS != oldClassificationData.ASSAY_ASIS)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>ASSAY ASIS</td> <td>" + oldClassificationData.ASSAY_ASIS + "</td><td>" + NewClassificationData.ASSAY_ASIS + "</td></tr>";
                if (NewClassificationData.MANUFACTURER != oldClassificationData.MANUFACTURER)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>MANUFACTURER</td> <td>" + oldClassificationData.MANUFACTURER + "</td><td>" + NewClassificationData.MANUFACTURER + "</td></tr>";
                if (NewClassificationData.Potency_as_is_basis != oldClassificationData.Potency_as_is_basis)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Potency as is basis</td> <td>" + oldClassificationData.Potency_as_is_basis + "</td><td>" + NewClassificationData.Potency_as_is_basis + "</td></tr>";
                if (NewClassificationData.Loss_on_Drying != oldClassificationData.Loss_on_Drying)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Loss on Drying</td> <td>" + oldClassificationData.Loss_on_Drying + "</td><td>" + NewClassificationData.Loss_on_Drying + "</td></tr>";
                if (NewClassificationData.Potency_as_is_basis1 != oldClassificationData.Potency_as_is_basis1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Potency as is basis1</td> <td>" + oldClassificationData.Potency_as_is_basis1 + "</td><td>" + NewClassificationData.Potency_as_is_basis1 + "</td></tr>";
                if (NewClassificationData.RM402217 != oldClassificationData.RM402217)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>RM402217</td> <td>" + oldClassificationData.RM402217 + "</td><td>" + NewClassificationData.RM402217 + "</td></tr>";
                if (NewClassificationData.RM323350 != oldClassificationData.RM323350)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>RM323350</td> <td>" + oldClassificationData.RM323350 + "</td><td>" + NewClassificationData.RM323350 + "</td></tr>";
                if (NewClassificationData.SF110063 != oldClassificationData.SF110063)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>SF110063</td> <td>" + oldClassificationData.SF110063 + "</td><td>" + NewClassificationData.SF110063 + "</td></tr>";
                if (NewClassificationData.SF900052 != oldClassificationData.SF900052)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>SF900052</td> <td>" + oldClassificationData.SF900052 + "</td><td>" + NewClassificationData.SF900052 + "</td></tr>";
                if (NewClassificationData.IP4A0047 != oldClassificationData.IP4A0047)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldClassificationData.IP4A0047 + "</td><td>" + NewClassificationData.IP4A0047 + "</td></tr>";
                if (NewClassificationData.External_Material_Group != oldClassificationData.External_Material_Group)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldClassificationData.External_Material_Group + "</td><td>" + NewClassificationData.External_Material_Group + "</td></tr>";
                if (NewClassificationData.Assay_by_GC != oldClassificationData.Assay_by_GC)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldClassificationData.Assay_by_GC + "</td><td>" + NewClassificationData.Assay_by_GC + "</td></tr>";
                if (NewClassificationData.Version_Number != oldClassificationData.Version_Number)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Remarks</td> <td>" + oldClassificationData.Version_Number + "</td><td>" + NewClassificationData.Version_Number + "</td></tr>";

                //PROV-CCP-MM-941-23-0045 Start
                if (NewClassificationData.sKXSBU != oldClassificationData.sKXSBU)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-SBU</td> <td>" + oldClassificationData.sKXSBU + "</td><td>" + NewClassificationData.sKXSBU + "</td></tr>";
                if (NewClassificationData.sKXMARKT != oldClassificationData.sKXMARKT)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Market</td> <td>" + oldClassificationData.sKXMARKT + "</td><td>" + NewClassificationData.sKXMARKT + "</td></tr>";
                if (NewClassificationData.sKXSELLCTRY != oldClassificationData.sKXSELLCTRY)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Selling Country</td> <td>" + oldClassificationData.sKXSELLCTRY + "</td><td>" + NewClassificationData.sKXSELLCTRY + "</td></tr>";
                if (NewClassificationData.sKXBUSI != oldClassificationData.sKXBUSI)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Business</td> <td>" + oldClassificationData.sKXBUSI + "</td><td>" + NewClassificationData.sKXBUSI + "</td></tr>";
                if (NewClassificationData.sKXDIV != oldClassificationData.sKXDIV)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Division</td> <td>" + oldClassificationData.sKXDIV + "</td><td>" + NewClassificationData.sKXDIV + "</td></tr>";
                if (NewClassificationData.sKXTHER != oldClassificationData.sKXTHER)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Therapy</td> <td>" + oldClassificationData.sKXTHER + "</td><td>" + NewClassificationData.sKXTHER + "</td></tr>";
                if (NewClassificationData.sKXDOSFRM != oldClassificationData.sKXDOSFRM)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Dosage Form</td> <td>" + oldClassificationData.sKXDOSFRM + "</td><td>" + NewClassificationData.sKXDOSFRM + "</td></tr>";
                if (NewClassificationData.sKXMINSL != oldClassificationData.sKXMINSL)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Minimum Shelf Life</td> <td>" + oldClassificationData.sKXMINSL + "</td><td>" + NewClassificationData.sKXMINSL + "</td></tr>";
                if (NewClassificationData.sMKTMNGER != oldClassificationData.sMKTMNGER)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Kinaxis-Marketing Manager</td> <td>" + oldClassificationData.sMKTMNGER + "</td><td>" + NewClassificationData.sMKTMNGER + "</td></tr>";
                if (NewClassificationData.sCS_MOLECULE != oldClassificationData.sCS_MOLECULE)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Molecule Details</td> <td>" + oldClassificationData.sCS_MOLECULE + "</td><td>" + NewClassificationData.sCS_MOLECULE + "</td></tr>";
                if (NewClassificationData.sMGRPPX != oldClassificationData.sMGRPPX)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Material Grouping for PASX</td> <td>" + oldClassificationData.sMGRPPX + "</td><td>" + NewClassificationData.sMGRPPX + "</td></tr>";

                //PROV-CCP-MM-941-23-0045 End 
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
    private void CheckIfChangesLog(Classification NewClassificationData, Classification oldClassificationData)
    {
        bool classtypesFlag = false;
        bool classsFlag = false;
        Utility objUtil = new Utility();
        List<SMChange> _items = new List<SMChange>();
        try
        {
            if (NewClassificationData.Mat_Classification_Id > 0 && oldClassificationData.Mat_Classification_Id > 0)
            {

                try
                {
                    classtypesFlag = false;
                    if (NewClassificationData.Class_Type != "" && oldClassificationData.Class_Type == "")
                    {
                        classtypesFlag = true;
                    }
                    if (NewClassificationData.Class_Type == "" && oldClassificationData.Class_Type != "")
                    {
                        classtypesFlag = true;
                    }
                    if (NewClassificationData.Class_Type != "" && oldClassificationData.Class_Type != "")
                    {
                        classtypesFlag = true;
                    }
                }
                catch (Exception ex)
                { }


                if (classtypesFlag == true)
                {
                    bool ndsFlag = false;
                    string sclasstypes = "";
                    try
                    {
                        sclasstypes = NewClassificationData.Class_Type.Remove(NewClassificationData.Class_Type.Length - 1, 1);
                    }
                    catch (Exception ex) { }

                    string sclasstypesold = "";
                    try
                    {
                        sclasstypesold = oldClassificationData.Class_Type;
                    }
                    catch (Exception ex) { }
                    string[] nds = sclasstypes.Split(',');
                    string[] ods = sclasstypesold.Split(',');
                    for (var i = 0; i < nds.Length; i++)
                    {
                        if (nds[i] != "")
                        {

                            if (!ods.Contains(nds[i]))
                            {
                                ndsFlag = true;
                                break;
                            }

                        }
                    }
                    for (var j = 0; j < ods.Length; j++)
                    {
                        if (ods[j] != "")
                        {
                            if (!nds.Contains(ods[j]))
                            {
                                ndsFlag = true;
                                break;
                            }
                        }
                    }


                    if (nds.Length == 0 && ods.Length > 0 && ndsFlag == false)
                    {
                        ndsFlag = true;
                    }
                    if (nds.Length > 0 && ods.Length == 0 && ndsFlag == false)
                    {
                        ndsFlag = true;
                    }

                    if (NewClassificationData.Class_Type == "")
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1290, colOldVal = oldClassificationData.Class_Type, colNewVal = NewClassificationData.Class_Type });
                        }
                    }
                    else if (oldClassificationData.Class_Type == "")
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1290, colOldVal = oldClassificationData.Class_Type, colNewVal = NewClassificationData.Class_Type });
                        }
                    }
                    else if (NewClassificationData.Class_Type.Substring(1, NewClassificationData.Class_Type.Length - 1) != oldClassificationData.Class_Type.Substring(1, oldClassificationData.Class_Type.Length - 1))
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1290, colOldVal = oldClassificationData.Class_Type, colNewVal = NewClassificationData.Class_Type });
                        }
                    }
                }

                try
                {

                    classsFlag = false;
                    if (NewClassificationData.Class != "" && oldClassificationData.Class == "")
                    {
                        classsFlag = true;
                    }
                    if (NewClassificationData.Class == "" && oldClassificationData.Class != "")
                    {
                        classsFlag = true;
                    }
                    if (NewClassificationData.Class != "" && oldClassificationData.Class != "")
                    {
                        classsFlag = true;
                    }
                }
                catch (Exception ex)
                { }

                if (classsFlag == true)
                {
                    bool ndsFlag = false;
                    string sclasss = "";
                    try
                    {
                        sclasss = NewClassificationData.Class.Remove(NewClassificationData.Class.Length - 1, 1);
                    }
                    catch (Exception ex) { }

                    string sclasssold = "";
                    try
                    {
                        sclasssold = oldClassificationData.Class;
                    }
                    catch (Exception ex) { }
                    string[] nds = sclasss.Split(',');
                    string[] ods = sclasssold.Split(',');
                    for (var i = 0; i < nds.Length; i++)
                    {
                        if (nds[i] != "")
                        {

                            if (!ods.Contains(nds[i]))
                            {
                                ndsFlag = true;
                                break;
                            }

                        }
                    }
                    for (var j = 0; j < ods.Length; j++)
                    {
                        if (ods[j] != "")
                        {
                            if (!nds.Contains(ods[j]))
                            {
                                ndsFlag = true;
                                break;
                            }
                        }
                    }


                    if (nds.Length == 0 && ods.Length > 0 && ndsFlag == false)
                    {
                        ndsFlag = true;
                    }
                    if (nds.Length > 0 && ods.Length == 0 && ndsFlag == false)
                    {
                        ndsFlag = true;
                    }

                    if (NewClassificationData.Class == "")
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1291, colOldVal = oldClassificationData.Class, colNewVal = NewClassificationData.Class });
                        }
                    }
                    else if (oldClassificationData.Class == "")
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1291, colOldVal = oldClassificationData.Class, colNewVal = NewClassificationData.Class });
                        }
                    }
                    else if (NewClassificationData.Class.Substring(1, NewClassificationData.Class.Length - 1) != oldClassificationData.Class.Substring(1, oldClassificationData.Class.Length - 1))
                    {
                        if (ndsFlag == true)
                        {
                            _items.Add(new SMChange { colFieldName = 1291, colOldVal = oldClassificationData.Class, colNewVal = NewClassificationData.Class });
                        }
                    }
                }

                //if (NewClassificationData.Class_Type != "" && oldClassificationData.Class_Type != "")
                //{
                //    bool ndsFlag = false;
                //    //string[] nds = NewClassificationData.Class_Type.Split(',');
                //    string sClass_Type = NewClassificationData.Class_Type.Remove(NewClassificationData.Class_Type.Length - 1, 1);
                //    string[] nds = sClass_Type.Split(',');

                //    string[] ods = oldClassificationData.Class_Type.Split(',');
                //    for (var i = 0; i < nds.Length; i++)
                //    {
                //        if (!ods.Contains(nds[i]))
                //        {
                //            ndsFlag = true;
                //            break;
                //        }
                //    }
                //    for (var j = 0; j < ods.Length; j++)
                //    {
                //        if (!nds.Contains(ods[j]))
                //        {
                //            ndsFlag = true;
                //            break;
                //        }
                //    }
                //    // Create and initialize array
                //    //string[] nds = { NewClassificationData.Class_Type };
                //    //string[] ods = { oldClassificationData.Class_Type };
                //    //foreach (string i in nds)
                //    //{
                //    //    //Console.WriteLine(i);
                //    //    if (!ods.Contains(i))
                //    //    {
                //    //        ndsFlag = true;
                //    //        break;
                //    //    }
                //    //}
                //    //foreach (string j in ods)
                //    //{
                //    //    if (!nds.Contains(j))
                //    //    {
                //    //        ndsFlag = true;
                //    //        break;
                //    //    }
                //    //}
                //    if (NewClassificationData.Class_Type == "")
                //    {
                //        if (ndsFlag == true)
                //        {
                //            _items.Add(new SMChange { colFieldName = 1290, colOldVal = oldClassificationData.Class_Type, colNewVal = NewClassificationData.Class_Type });
                //        }
                //    }
                //    else if (oldClassificationData.Class_Type == "")
                //    {
                //        if (ndsFlag == true)
                //        {
                //            _items.Add(new SMChange { colFieldName = 1290, colOldVal = oldClassificationData.Class_Type, colNewVal = NewClassificationData.Class_Type });
                //        }
                //    }
                //    else if (NewClassificationData.Class_Type.Substring(1, NewClassificationData.Class_Type.Length - 1) != oldClassificationData.Class_Type.Substring(1, oldClassificationData.Class_Type.Length - 1))
                //    {
                //        if (ndsFlag == true)
                //        {
                //            _items.Add(new SMChange { colFieldName = 1290, colOldVal = oldClassificationData.Class_Type, colNewVal = NewClassificationData.Class_Type });
                //        }
                //    }
                //}
                //if (NewClassificationData.Class != "" && oldClassificationData.Class != "")
                //{

                //    bool ndsFlag = false;
                //    // Create and initialize array
                //    //string[] nds = { NewClassificationData.Class };
                //    string sClass = NewClassificationData.Class.Remove(NewClassificationData.Class.Length - 1, 1);
                //    string[] nds = sClass.Split(',');

                //    string[] ods = { oldClassificationData.Class };
                //    foreach (string i in nds)
                //    {
                //        //Console.WriteLine(i);
                //        if (!ods.Contains(i))
                //        {
                //            ndsFlag = true;
                //            break;
                //        }
                //    }
                //    foreach (string j in ods)
                //    {
                //        if (!nds.Contains(j))
                //        {
                //            ndsFlag = true;
                //            break;
                //        }
                //    }
                //    if (NewClassificationData.Class == "")
                //    {
                //        if (ndsFlag == true)
                //        {
                //            _items.Add(new SMChange { colFieldName = 1291, colOldVal = oldClassificationData.Class, colNewVal = NewClassificationData.Class });
                //        }
                //    }
                //    else if (oldClassificationData.Class == "")
                //    {
                //        if (ndsFlag == true)
                //        {
                //            _items.Add(new SMChange { colFieldName = 1291, colOldVal = oldClassificationData.Class, colNewVal = NewClassificationData.Class });
                //        }
                //    }
                //    else if (NewClassificationData.Class.Substring(1, NewClassificationData.Class.Length - 1) != oldClassificationData.Class.Substring(1, oldClassificationData.Class.Length - 1))
                //    {
                //        if (ndsFlag == true)
                //        {
                //            _items.Add(new SMChange { colFieldName = 1291, colOldVal = oldClassificationData.Class, colNewVal = NewClassificationData.Class });
                //        }
                //    }
                //}




                if (NewClassificationData.Strength_of_mat_Pack_type != oldClassificationData.Strength_of_mat_Pack_type)
                    _items.Add(new SMChange { colFieldName = 1238, colOldVal = oldClassificationData.Strength_of_mat_Pack_type, colNewVal = NewClassificationData.Strength_of_mat_Pack_type });
                if (NewClassificationData.Market != oldClassificationData.Market)
                    _items.Add(new SMChange { colFieldName = 1239, colOldVal = oldClassificationData.Market, colNewVal = NewClassificationData.Market });
                if (NewClassificationData.NDC_No_LPI != oldClassificationData.NDC_No_LPI)
                    _items.Add(new SMChange { colFieldName = 1240, colOldVal = oldClassificationData.NDC_No_LPI, colNewVal = NewClassificationData.NDC_No_LPI });
                if (NewClassificationData.NDC_No_LL != oldClassificationData.NDC_No_LL)
                    _items.Add(new SMChange { colFieldName = 1241, colOldVal = oldClassificationData.NDC_No_LL, colNewVal = NewClassificationData.NDC_No_LL });
                if (NewClassificationData.HTS != oldClassificationData.HTS)
                    _items.Add(new SMChange { colFieldName = 1242, colOldVal = oldClassificationData.HTS, colNewVal = NewClassificationData.HTS });
                if (NewClassificationData.ANDA != oldClassificationData.ANDA)
                    _items.Add(new SMChange { colFieldName = 1243, colOldVal = oldClassificationData.ANDA, colNewVal = NewClassificationData.ANDA });
                if (NewClassificationData.FDA_No != oldClassificationData.FDA_No)
                    _items.Add(new SMChange { colFieldName = 1244, colOldVal = oldClassificationData.FDA_No, colNewVal = NewClassificationData.FDA_No });
                if (NewClassificationData.LPI_Material_Identifier != oldClassificationData.LPI_Material_Identifier)
                    _items.Add(new SMChange { colFieldName = 1245, colOldVal = oldClassificationData.LPI_Material_Identifier, colNewVal = NewClassificationData.LPI_Material_Identifier });
                if (NewClassificationData.Material_Grouping_for_MES != oldClassificationData.Material_Grouping_for_MES)
                    _items.Add(new SMChange { colFieldName = 1246, colOldVal = oldClassificationData.Material_Grouping_for_MES, colNewVal = NewClassificationData.Material_Grouping_for_MES });
                if (NewClassificationData.Short_description_for_3PL != oldClassificationData.Short_description_for_3PL)
                    _items.Add(new SMChange { colFieldName = 1247, colOldVal = oldClassificationData.Short_description_for_3PL, colNewVal = NewClassificationData.Short_description_for_3PL });
                if (NewClassificationData.Package_Presentation_3PL != oldClassificationData.Package_Presentation_3PL)
                    _items.Add(new SMChange { colFieldName = 1248, colOldVal = oldClassificationData.Package_Presentation_3PL, colNewVal = NewClassificationData.Package_Presentation_3PL });
                if (NewClassificationData.Number_of_Tablet_3PL != oldClassificationData.Number_of_Tablet_3PL)
                    _items.Add(new SMChange { colFieldName = 1249, colOldVal = oldClassificationData.Number_of_Tablet_3PL, colNewVal = NewClassificationData.Number_of_Tablet_3PL });
                if (NewClassificationData.Material_Category_A_3PL != oldClassificationData.Material_Category_A_3PL)
                    _items.Add(new SMChange { colFieldName = 1250, colOldVal = oldClassificationData.Material_Category_A_3PL, colNewVal = NewClassificationData.Material_Category_A_3PL });
                if (NewClassificationData.Material_Category_B_3PL != oldClassificationData.Material_Category_B_3PL)
                    _items.Add(new SMChange { colFieldName = 1251, colOldVal = oldClassificationData.Material_Category_B_3PL, colNewVal = NewClassificationData.Material_Category_B_3PL });
                if (NewClassificationData.Sorting_for_inventory_report != oldClassificationData.Sorting_for_inventory_report)
                    _items.Add(new SMChange { colFieldName = 1252, colOldVal = oldClassificationData.Sorting_for_inventory_report, colNewVal = NewClassificationData.Sorting_for_inventory_report });
                if (NewClassificationData.Pack_size != oldClassificationData.Pack_size)
                    _items.Add(new SMChange { colFieldName = 1253, colOldVal = oldClassificationData.Pack_size, colNewVal = NewClassificationData.Pack_size });
                if (NewClassificationData.Product_Group != oldClassificationData.Product_Group)
                    _items.Add(new SMChange { colFieldName = 1254, colOldVal = oldClassificationData.Product_Group, colNewVal = NewClassificationData.Product_Group });

                if (NewClassificationData.DRUG_CATEGORY != oldClassificationData.DRUG_CATEGORY)
                    _items.Add(new SMChange { colFieldName = 1255, colOldVal = oldClassificationData.DRUG_CATEGORY, colNewVal = NewClassificationData.DRUG_CATEGORY });

                if (NewClassificationData.MARKET_ENTRY_DATE != oldClassificationData.MARKET_ENTRY_DATE)
                    _items.Add(new SMChange { colFieldName = 1256, colOldVal = oldClassificationData.MARKET_ENTRY_DATE, colNewVal = NewClassificationData.MARKET_ENTRY_DATE });
                if (NewClassificationData.PZN_HORMOSAN != oldClassificationData.PZN_HORMOSAN)
                    _items.Add(new SMChange { colFieldName = 1257, colOldVal = oldClassificationData.PZN_HORMOSAN, colNewVal = NewClassificationData.PZN_HORMOSAN });
                if (NewClassificationData.StorageCondition != oldClassificationData.StorageCondition)
                    _items.Add(new SMChange { colFieldName = 1339, colOldVal = oldClassificationData.StorageCondition, colNewVal = NewClassificationData.StorageCondition });
                if (NewClassificationData.Allowed_Manufacturers != oldClassificationData.Allowed_Manufacturers)
                    _items.Add(new SMChange { colFieldName = 1289, colOldVal = oldClassificationData.Allowed_Manufacturers, colNewVal = NewClassificationData.Allowed_Manufacturers });
                if (NewClassificationData.HSAN_MATERIAL_IDENTIFIER != oldClassificationData.HSAN_MATERIAL_IDENTIFIER)
                    _items.Add(new SMChange { colFieldName = 1292, colOldVal = oldClassificationData.HSAN_MATERIAL_IDENTIFIER, colNewVal = NewClassificationData.HSAN_MATERIAL_IDENTIFIER });
                if (NewClassificationData.Expiration_date_shelf_life != oldClassificationData.Expiration_date_shelf_life)
                    _items.Add(new SMChange { colFieldName = 1293, colOldVal = oldClassificationData.Expiration_date_shelf_life, colNewVal = NewClassificationData.Expiration_date_shelf_life });
                //if (NewClassificationData.Next_Insp_Date_for_Batch != oldClassificationData.Next_Insp_Date_for_Batch)
                if ((objUtil.GetDDMMYYYYNew(NewClassificationData.Next_Insp_Date_for_Batch) != oldClassificationData.Next_Insp_Date_for_Batch)
                 && (objUtil.GetDDMMYYYYNew(NewClassificationData.Next_Insp_Date_for_Batch) != "01/01/1900")
                 && (NewClassificationData.Next_Insp_Date_for_Batch != "1900-01-01")
                     && (oldClassificationData.Next_Insp_Date_for_Batch != "01/01/1900") && (oldClassificationData.Next_Insp_Date_for_Batch != "1900-01-01")
                     )
                    _items.Add(new SMChange { colFieldName = 1294, colOldVal = oldClassificationData.Next_Insp_Date_for_Batch, colNewVal = objUtil.GetDDMMYYYYNew(NewClassificationData.Next_Insp_Date_for_Batch) });
                if (NewClassificationData.Batch_number != oldClassificationData.Batch_number)
                    _items.Add(new SMChange { colFieldName = 1295, colOldVal = oldClassificationData.Batch_number, colNewVal = NewClassificationData.Batch_number });
                if (NewClassificationData.ASSAY_ASIS != oldClassificationData.ASSAY_ASIS)
                    _items.Add(new SMChange { colFieldName = 1296, colOldVal = oldClassificationData.ASSAY_ASIS, colNewVal = NewClassificationData.ASSAY_ASIS });
                if (NewClassificationData.MANUFACTURER != oldClassificationData.MANUFACTURER)
                    _items.Add(new SMChange { colFieldName = 1297, colOldVal = oldClassificationData.MANUFACTURER, colNewVal = NewClassificationData.MANUFACTURER });
                if (NewClassificationData.Potency_as_is_basis != oldClassificationData.Potency_as_is_basis)
                    _items.Add(new SMChange { colFieldName = 1298, colOldVal = oldClassificationData.Potency_as_is_basis, colNewVal = NewClassificationData.Potency_as_is_basis });
                if (NewClassificationData.Loss_on_Drying != oldClassificationData.Loss_on_Drying)
                    _items.Add(new SMChange { colFieldName = 1299, colOldVal = oldClassificationData.Loss_on_Drying, colNewVal = NewClassificationData.Loss_on_Drying });
                if (NewClassificationData.Potency_as_is_basis1 != oldClassificationData.Potency_as_is_basis1)
                    _items.Add(new SMChange { colFieldName = 1300, colOldVal = oldClassificationData.Potency_as_is_basis1, colNewVal = NewClassificationData.Potency_as_is_basis1 });
                if (NewClassificationData.RM402217 != oldClassificationData.RM402217)
                    _items.Add(new SMChange { colFieldName = 1301, colOldVal = oldClassificationData.RM402217, colNewVal = NewClassificationData.RM402217 });
                if (NewClassificationData.RM323350 != oldClassificationData.RM323350)
                    _items.Add(new SMChange { colFieldName = 1302, colOldVal = oldClassificationData.RM323350, colNewVal = NewClassificationData.RM323350 });
                if (NewClassificationData.SF900052 != oldClassificationData.SF900052)
                    _items.Add(new SMChange { colFieldName = 1303, colOldVal = oldClassificationData.SF900052, colNewVal = NewClassificationData.SF900052 });

                if (NewClassificationData.SF110063 != oldClassificationData.SF110063)
                    _items.Add(new SMChange { colFieldName = 1304, colOldVal = oldClassificationData.SF110063, colNewVal = NewClassificationData.SF110063 });
                if (NewClassificationData.IP4A0047 != oldClassificationData.IP4A0047)
                    _items.Add(new SMChange { colFieldName = 1305, colOldVal = oldClassificationData.IP4A0047, colNewVal = NewClassificationData.IP4A0047 });
                if (NewClassificationData.External_Material_Group != oldClassificationData.External_Material_Group)
                    _items.Add(new SMChange { colFieldName = 1307, colOldVal = oldClassificationData.External_Material_Group, colNewVal = NewClassificationData.External_Material_Group });
                if (NewClassificationData.Assay_by_GC != oldClassificationData.Assay_by_GC)
                    _items.Add(new SMChange { colFieldName = 1306, colOldVal = oldClassificationData.Assay_by_GC, colNewVal = NewClassificationData.Assay_by_GC });
                if (NewClassificationData.Version_Number != oldClassificationData.Version_Number)
                    _items.Add(new SMChange { colFieldName = 1308, colOldVal = oldClassificationData.Version_Number, colNewVal = NewClassificationData.Version_Number });


                //PROV-CCP-MM-941-23-0045 Start
                if (NewClassificationData.sKXSBU != oldClassificationData.sKXSBU)
                    _items.Add(new SMChange { colFieldName = 1432, colOldVal = oldClassificationData.sKXSBU, colNewVal = NewClassificationData.sKXSBU });
                if (NewClassificationData.sKXMARKT != oldClassificationData.sKXMARKT)
                    _items.Add(new SMChange { colFieldName = 1433, colOldVal = oldClassificationData.sKXMARKT, colNewVal = NewClassificationData.sKXMARKT });
                if (NewClassificationData.sKXSELLCTRY != oldClassificationData.sKXSELLCTRY)
                    _items.Add(new SMChange { colFieldName = 1435, colOldVal = oldClassificationData.sKXSELLCTRY, colNewVal = NewClassificationData.sKXSELLCTRY });
                if (NewClassificationData.sKXBUSI != oldClassificationData.sKXBUSI)
                    _items.Add(new SMChange { colFieldName = 1436, colOldVal = oldClassificationData.sKXBUSI, colNewVal = NewClassificationData.sKXBUSI });
                if (NewClassificationData.sKXDIV != oldClassificationData.sKXDIV)
                    _items.Add(new SMChange { colFieldName = 1437, colOldVal = oldClassificationData.sKXDIV, colNewVal = NewClassificationData.sKXDIV });
                if (NewClassificationData.sKXTHER != oldClassificationData.sKXTHER)
                    _items.Add(new SMChange { colFieldName = 1438, colOldVal = oldClassificationData.sKXTHER, colNewVal = NewClassificationData.sKXTHER });
                if (NewClassificationData.sKXDOSFRM != oldClassificationData.sKXDOSFRM)
                    _items.Add(new SMChange { colFieldName = 1439, colOldVal = oldClassificationData.sKXDOSFRM, colNewVal = NewClassificationData.sKXDOSFRM });
                if (NewClassificationData.sKXMINSL != oldClassificationData.sKXMINSL)
                    _items.Add(new SMChange { colFieldName = 1440, colOldVal = oldClassificationData.sKXMINSL, colNewVal = NewClassificationData.sKXMINSL });
                if (NewClassificationData.sMKTMNGER != oldClassificationData.sMKTMNGER)
                    _items.Add(new SMChange { colFieldName = 1441, colOldVal = oldClassificationData.sMKTMNGER, colNewVal = NewClassificationData.sMKTMNGER });
                if (NewClassificationData.sCS_MOLECULE != oldClassificationData.sCS_MOLECULE)
                    _items.Add(new SMChange { colFieldName = 1442, colOldVal = oldClassificationData.sCS_MOLECULE, colNewVal = NewClassificationData.sCS_MOLECULE });
                if (NewClassificationData.sMGRPPX != oldClassificationData.sMGRPPX)
                    _items.Add(new SMChange { colFieldName = 1443, colOldVal = oldClassificationData.sMGRPPX, colNewVal = NewClassificationData.sMGRPPX });

                //PROV-CCP-MM-941-23-0045 End

            }

            if (_items.Count > 0)
            {
                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("51", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
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
        }

    }

    #endregion

}