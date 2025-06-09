using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using SectionConfiguration;
using Accenture.MWT.DomainObject;
using System.IO;
using log4net;
public partial class Transaction_BasicData2 : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    BasicData2 objSavedBasic2 = new BasicData2();
    BasicDataAccess basicDataAccess = new BasicDataAccess();
    string sdate = "";
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "Start" + HelperAccess.ReqType);
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
                if (Session[StaticKeys.SelectedModuleId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    PopuplateDropDownList();


                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                    HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                    ConfigureControl();
                    ReadBasicData2(lblMasterHeaderId.Text);

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

                    //PROSOL_SDT16092019
                      ProsolValidCheck();
                    //PROSOL_SDT16092019

                    //MSC_8300001775 Start
                    //if (HelperAccess.ReqType == "SMC")
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                    {
                        ConfigureControlForSChange();
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

    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    private void ProsolValidCheck()
    {
        try
        {
            if (Convert.ToString(lblModuleId.Text) == "138")
            {
                txtBasicDataText.Enabled = false;
                txtBasicDataText.ReadOnly = true;
            }
        }
        catch (Exception ex)
        { _log.Error("ProsolValidCheck", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void ConfigureControlForSChange()
    {
        try
        {
        txtShipperGrossWeight.Enabled = true;
        txtShipperGrossWeight.CssClass = "textbox";
        ddlShipperWeightUnit.Enabled = true;

        lableddlBasicDataLanguage.Visible = false;
        reqddlBasicDataLanguage.Visible = false;
        labletxtBasicDataText.Visible = false;
        reqtxtBasicDataText.Visible = false;
        txtBasicDataText.Enabled = true;
        txtBasicDataText.ReadOnly = false;

        txtBasicDataText.CssClass = "textarea";
        lableddlInspectionLanguage.Visible = false;
        reqddlInspectionLanguage.Visible = false;
        labletxtInspectionText.Visible = false;
        reqtxtInspectionText.Visible = false;
        txtInspectionText.Enabled = true;
        txtInspectionText.CssClass = "textarea";

        txtLength.Enabled = true;
        txtLength.CssClass = "textbox";
        txtWidth.Enabled = true;
        txtWidth.CssClass = "textbox";
        txtHeight.Enabled = true;
        txtHeight.CssClass = "textbox";
        ddlUnitDimension.Enabled = true;

        }
        catch (Exception ex)
        { _log.Error("ConfigureControlForSChange", ex); }
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
            Response.Redirect("BasicData2.aspx?pgseq=2&sid=4");
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

    protected void ddlAltUnitOfMeasure_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasureSetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure_SelectedIndexChanged", ex); }
    }

    protected void ddlAltUnitOfMeasure1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasure1SetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure1_SelectedIndexChanged", ex); }
    }

    protected void ddlAltUnitOfMeasure2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasure2SetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure2_SelectedIndexChanged", ex); }
    }

    protected void ddlAltUnitOfMeasure3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasure3SetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure3_SelectedIndexChanged", ex); }
    }

    protected void ddlAltUnitOfMeasure4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasure4SetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure4_SelectedIndexChanged", ex); }
    }

    protected void ddlAltUnitOfMeasure5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasure5SetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure5_SelectedIndexChanged", ex); }
    }

    protected void ddlAltUnitOfMeasure6_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        AltUnitOfMeasure6SetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlAltUnitOfMeasure6_SelectedIndexChanged", ex); }
    }

    protected void ddlUnitOfMeasureUsage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        ProportionUnitSetUp();
        }
        catch (Exception ex)
        { _log.Error("ddlUnitOfMeasureUsage_SelectedIndexChanged", ex); }
    }

    #endregion

    #region Private Funtions


    private void ProportionUnitSetUp()
    {
        try
        {
        if (ddlUnitOfMeasureUsage.SelectedValue == "")
        {
            ddlCharacteristicName.SelectedValue = "";
            txtPlannedValueForUnitMeasure.Text = "";
            ddlBatchSpcfMatlUnitMeasure.SelectedValue = "";

            ddlCharacteristicName.Enabled = false;
            txtPlannedValueForUnitMeasure.Enabled = false;
            ddlBatchSpcfMatlUnitMeasure.Enabled = false;

            reqddlCharacteristicName.Visible = false;
            reqtxtPlannedValueForUnitMeasure.Visible = false;
            reqddlBatchSpcfMatlUnitMeasure.Visible = false;

            lableddlCharacteristicName.Visible = false;
            labletxtPlannedValueForUnitMeasure.Visible = false;
            lableddlBatchSpcfMatlUnitMeasure.Visible = false;
        }
        else
        {
            ddlCharacteristicName.Items.Clear();

            ddlCharacteristicName.Items.Add(new ListItem("---Select---", ""));
            if (ddlUnitOfMeasureUsage.SelectedValue == "A")
                ddlCharacteristicName.Items.Add(new ListItem("POTENCY_1 - Potency as is basis1", "POTENCY_1"));
            else
                ddlCharacteristicName.Items.Add(new ListItem("POTENCY_1 - Potency as is basis1", "POTENCY_1"));
            //ddlCharacteristicName.Items.Add(new ListItem("POTENCY_1 - Potency as is basis1", "POTENCY_1"));

            //For ROH and VERP
            if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164))
            {
                ddlCharacteristicName.Items.Add(new ListItem("AVGWT", "AVGWT"));
                ddlCharacteristicName.Items.Add(new ListItem("AVGWT1", "AVGWT1"));

                ddlBatchSpcfMatlUnitMeasure.Items.Clear();
                ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("---Select---", ""));

                //MSC_8300001775 Commented Start
                //ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("KG -Kilogram", "KG "));
                //ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("G  -Gram", "G  "));
                //ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("KGA-Kilogram act. ingrd.", "KGA"));
                //ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("GI -Gram act. ingrd.", "GI "));
                //MSC_8300001775 Commented End
                //MSC_8300001775 Added start
                ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("KG -Kilogram", "KG"));
                ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("G  -Gram", "G"));
                ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("KGA-Kilogram act. ingrd.", "KGA"));
                ddlBatchSpcfMatlUnitMeasure.Items.Add(new ListItem("GI -Gram act. ingrd.", "GI"));
                //MSC_8300001775 Added End
            }
            ddlCharacteristicName.Enabled = true;
            txtPlannedValueForUnitMeasure.Enabled = true;
            ddlBatchSpcfMatlUnitMeasure.Enabled = true;

            reqddlCharacteristicName.Visible = true;
            reqtxtPlannedValueForUnitMeasure.Visible = true;
            reqddlBatchSpcfMatlUnitMeasure.Visible = true;

            lableddlCharacteristicName.Visible = true;
            labletxtPlannedValueForUnitMeasure.Visible = true;
            lableddlBatchSpcfMatlUnitMeasure.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("ProportionUnitSetUp", ex); }
    }

    private void AltUnitOfMeasureSetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure.SelectedValue != "")
        {
            //if (txtAltUnitValueX.Text == "")
            //    txtAltUnitValueX.Text = "1";

            //if (txtAltUnitValueY.Text == "")
            //    txtAltUnitValueY.Text = "1";

            txtAltUnitValueX.Enabled = true;
            txtAltUnitValueY.Enabled = true;

            reqtxtAltUnitValueX.Visible = true;
            reqtxtAltUnitValueY.Visible = true;

            labletxtAltUnitValueX.Visible = true;
            labletxtAltUnitValueY.Visible = true;
        }
        else
        {
            txtAltUnitValueX.Text = "";
            txtAltUnitValueY.Text = "";

            txtAltUnitValueX.Enabled = false;
            txtAltUnitValueY.Enabled = false;

            reqtxtAltUnitValueX.Visible = false;
            reqtxtAltUnitValueY.Visible = false;

            labletxtAltUnitValueX.Visible = false;
            labletxtAltUnitValueY.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasureSetUp", ex); }
    }

    private void AltUnitOfMeasure1SetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure1.SelectedValue != "")
        {
            //if (txtAltUnitValueX1.Text == "")
            //    txtAltUnitValueX1.Text = "1";

            //if (txtAltUnitValueY1.Text == "")
            //    txtAltUnitValueY1.Text = "1";

            txtAltUnitValueX1.Enabled = true;
            txtAltUnitValueY1.Enabled = true;

            reqtxtAltUnitValueX1.Visible = true;
            reqtxtAltUnitValueY1.Visible = true;

            labletxtAltUnitValueX1.Visible = true;
            labletxtAltUnitValueY1.Visible = true;
        }
        else
        {
            txtAltUnitValueX1.Text = "";
            txtAltUnitValueY1.Text = "";

            txtAltUnitValueX1.Enabled = false;
            txtAltUnitValueY1.Enabled = false;

            reqtxtAltUnitValueX1.Visible = false;
            reqtxtAltUnitValueY1.Visible = false;

            labletxtAltUnitValueX1.Visible = false;
            labletxtAltUnitValueY1.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasure1SetUp", ex); }
    }

    private void AltUnitOfMeasure2SetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure2.SelectedValue != "")
        {
            //if (txtAltUnitValueX2.Text == "")
            //    txtAltUnitValueX2.Text = "1";

            //if (txtAltUnitValueY2.Text == "")
            //    txtAltUnitValueY2.Text = "1";

            txtAltUnitValueX2.Enabled = true;
            txtAltUnitValueY2.Enabled = true;

            reqtxtAltUnitValueX2.Visible = true;
            reqtxtAltUnitValueY2.Visible = true;

            labletxtAltUnitValueX2.Visible = true;
            labletxtAltUnitValueY2.Visible = true;
        }
        else
        {
            txtAltUnitValueX2.Text = "";
            txtAltUnitValueY2.Text = "";

            txtAltUnitValueX2.Enabled = false;
            txtAltUnitValueY2.Enabled = false;

            reqtxtAltUnitValueX2.Visible = false;
            reqtxtAltUnitValueY2.Visible = false;

            labletxtAltUnitValueX2.Visible = false;
            labletxtAltUnitValueY2.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasure2SetUp", ex); }
    }

    private void AltUnitOfMeasure3SetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure3.SelectedValue != "")
        {
            //if (txtAltUnitValueX2.Text == "")
            //    txtAltUnitValueX2.Text = "1";

            //if (txtAltUnitValueY2.Text == "")
            //    txtAltUnitValueY2.Text = "1";

            txtAltUnitValueX3.Enabled = true;
            txtAltUnitValueY3.Enabled = true;

            reqtxtAltUnitValueX3.Visible = true;
            reqtxtAltUnitValueY3.Visible = true;

            labletxtAltUnitValueX3.Visible = true;
            labletxtAltUnitValueY3.Visible = true;
        }
        else
        {
            txtAltUnitValueX3.Text = "";
            txtAltUnitValueY3.Text = "";

            txtAltUnitValueX3.Enabled = false;
            txtAltUnitValueY3.Enabled = false;

            reqtxtAltUnitValueX3.Visible = false;
            reqtxtAltUnitValueY3.Visible = false;

            labletxtAltUnitValueX3.Visible = false;
            labletxtAltUnitValueY3.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasure3SetUp", ex); }
    }

    private void AltUnitOfMeasure4SetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure4.SelectedValue != "")
        {
            txtAltUnitValueX4.Enabled = true;
            txtAltUnitValueY4.Enabled = true;

            reqtxtAltUnitValueX4.Visible = true;
            reqtxtAltUnitValueY4.Visible = true;

            labletxtAltUnitValueX4.Visible = true;
            labletxtAltUnitValueY4.Visible = true;
        }
        else
        {
            txtAltUnitValueX4.Text = "";
            txtAltUnitValueY4.Text = "";

            txtAltUnitValueX4.Enabled = false;
            txtAltUnitValueY4.Enabled = false;

            reqtxtAltUnitValueX4.Visible = false;
            reqtxtAltUnitValueY4.Visible = false;

            labletxtAltUnitValueX4.Visible = false;
            labletxtAltUnitValueY4.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasure4SetUp", ex); }
    }

    private void AltUnitOfMeasure5SetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure5.SelectedValue != "")
        {
            txtAltUnitValueX5.Enabled = true;
            txtAltUnitValueY5.Enabled = true;

            reqtxtAltUnitValueX5.Visible = true;
            reqtxtAltUnitValueY5.Visible = true;

            labletxtAltUnitValueX5.Visible = true;
            labletxtAltUnitValueY5.Visible = true;
        }
        else
        {
            txtAltUnitValueX5.Text = "";
            txtAltUnitValueY5.Text = "";

            txtAltUnitValueX5.Enabled = false;
            txtAltUnitValueY5.Enabled = false;

            reqtxtAltUnitValueX5.Visible = false;
            reqtxtAltUnitValueY5.Visible = false;

            labletxtAltUnitValueX5.Visible = false;
            labletxtAltUnitValueY5.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasure5SetUp", ex); }
    }

    private void AltUnitOfMeasure6SetUp()
    {
        try
        {
        if (ddlAltUnitOfMeasure6.SelectedValue != "")
        {
            txtAltUnitValueX6.Enabled = true;
            txtAltUnitValueY6.Enabled = true;

            reqtxtAltUnitValueX6.Visible = true;
            reqtxtAltUnitValueY6.Visible = true;

            labletxtAltUnitValueX6.Visible = true;
            labletxtAltUnitValueY6.Visible = true;
        }
        else
        {
            txtAltUnitValueX6.Text = "";
            txtAltUnitValueY6.Text = "";

            txtAltUnitValueX6.Enabled = false;
            txtAltUnitValueY6.Enabled = false;

            reqtxtAltUnitValueX6.Visible = false;
            reqtxtAltUnitValueY6.Visible = false;

            labletxtAltUnitValueX6.Visible = false;
            labletxtAltUnitValueY6.Visible = false;
            }
        }
        catch (Exception ex)
        { _log.Error("AltUnitOfMeasure6SetUp", ex); }
    }

    private void PopuplateDropDownList()
    {
        try
        {
        helperAccess.PopuplateDropDownList(ddlUnitDimension, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnitDim','3','LENGTH'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlBasicDataLanguage, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlBasicDataLanguage1, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlDescLanguage, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlDescLanguage1, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlInspectionLanguage, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlInspectionLanguage1, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlInternalCommentLanguage, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlInternalCommentLanguage1, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");

        //ROH/VERP Change
        //helperAccess.PopuplateDropDownList(ddlBatchSpcfMatlUnitMeasure, "pr_GetDropDownListByControlNameModuleType 'M','ddlBatchSpcfMatlUnitMeasure'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure1, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure2, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure3, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure4, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure5, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlAltUnitOfMeasure6, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlShipperWeightUnit, "pr_GetDropDownListByControlNameModuleTypeUnitDimension 'M','ddlBaseUnitDim','3','MASS  '", "LookUp_Desc", "LookUp_Code", "");

        helperAccess.PopuplateDropDownList(ddlUnitOfMeasureUsage, "pr_GetDropDownListByControlNameModuleType 'M','ddlUnitOfMeasureUsage'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlCharacteristicName, "pr_GetDropDownListByControlNameModuleType 'M','ddlCharacteristicName'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlInspectionLanguage1, "pr_GetDropDownListByControlNameModuleType 'M','ddlLanguage'", "LookUp_Desc", "LookUp_Code", "");
            //helperAccess.PopuplateDropDownList(ddlBatchSpcfMatlUnitMeasure, "pr_GetDropDownListByControlNameModuleType 'M','ddlBaseUnit'", "LookUp_Desc", "LookUp_Code", "");

        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private BasicData2 GetBasicData2Data()
    {
        return basicDataAccess.GetBasicData2(lblMasterHeaderId.Text);
    }
    private bool Save()
    {
        BasicDataAccess basicDataAccess = new BasicDataAccess();
        BasicData2 objBasicData = new BasicData2();
        bool flg = false;

        try
        {
            if (CheckValidBasicData2())
            {
                objBasicData = GetControlsValue();
                //objSavedBasic2 = basicDataAccess.GetBasicData2(lblMasterHeaderId.Text);
                objSavedBasic2 = GetBasicData2Data();
                if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) != 0)
                {
                    if (objSavedBasic2.Mat_Basic_Data2_Id > 0)
                    {
                        CheckIfChanges(objBasicData, objSavedBasic2);
                    }
                }

                if (basicDataAccess.SaveBasicData2(objBasicData) > 0)
                {
                    flg = true;
                    //MSC_8300001775
                    //if (HelperAccess.ReqType == "SMC")
                    if (MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text))
                    {
                        CheckIfChangesLog(objBasicData, objSavedBasic2);
                    }
                    //MSC_8300001775
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
                lblMsg.Text = "Please enter Length,Width,Height and Unit of Dimension";
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

private void CheckIfChanges(BasicData2 NewBasicData2, BasicData2 oldBasicData2)
    {
        try
        {
            if (NewBasicData2.Mat_Basic_Data2_Id > 0 && oldBasicData2.Mat_Basic_Data2_Id > 0)
            {
                if (NewBasicData2.Length != oldBasicData2.Length)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Length</td> <td>" + oldBasicData2.Length + "</td><td>" + NewBasicData2.Length + "</td></tr>";
                if (NewBasicData2.Width != oldBasicData2.Width)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Width</td> <td>" + oldBasicData2.Width + "</td><td>" + NewBasicData2.Width + "</td></tr>";
                if (NewBasicData2.Height != oldBasicData2.Height)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Height</td> <td>" + oldBasicData2.Height + "</td><td>" + NewBasicData2.Height + "</td></tr>";
                if (NewBasicData2.Unit_Of_Dimension != oldBasicData2.Unit_Of_Dimension)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Of Dimension</td> <td>" + oldBasicData2.Unit_Of_Dimension + "</td><td>" + NewBasicData2.Unit_Of_Dimension + "</td></tr>";
                if (NewBasicData2.Desc_Language != oldBasicData2.Desc_Language)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Desc Language</td> <td>" + oldBasicData2.Desc_Language + "</td><td>" + NewBasicData2.Desc_Language + "</td></tr>";
                if (NewBasicData2.Desc_Text != oldBasicData2.Desc_Text)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Desc Text</td> <td>" + oldBasicData2.Desc_Text + "</td><td>" + NewBasicData2.Desc_Text + "</td></tr>";
                if (NewBasicData2.Desc_Language1 != oldBasicData2.Desc_Language1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Desc Language1</td> <td>" + oldBasicData2.Desc_Language1 + "</td><td>" + NewBasicData2.Desc_Language1 + "</td></tr>";
                if (NewBasicData2.Desc_Text1 != oldBasicData2.Desc_Text1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Desc Text1</td> <td>" + oldBasicData2.Desc_Text1 + "</td><td>" + NewBasicData2.Desc_Text1 + "</td></tr>";
                if (NewBasicData2.Basic_Data_Language != oldBasicData2.Basic_Data_Language)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Basic Data Language</td> <td>" + oldBasicData2.Basic_Data_Language + "</td><td>" + NewBasicData2.Basic_Data_Language + "</td></tr>";
                if (NewBasicData2.Basic_Data_Text != oldBasicData2.Basic_Data_Text)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Basic Data Text</td> <td>" + oldBasicData2.Basic_Data_Text + "</td><td>" + NewBasicData2.Basic_Data_Text + "</td></tr>";
                if (NewBasicData2.Basic_Data_Language1 != oldBasicData2.Basic_Data_Language1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Basic Data Language1</td> <td>" + oldBasicData2.Basic_Data_Language1 + "</td><td>" + NewBasicData2.Basic_Data_Language1 + "</td></tr>";
                if (NewBasicData2.Basic_Data_Text1 != oldBasicData2.Basic_Data_Text1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Basic Data Text1 </td> <td>" + oldBasicData2.Basic_Data_Text1 + "</td><td>" + NewBasicData2.Basic_Data_Text1 + "</td></tr>";
                if (NewBasicData2.Inspection_Language != oldBasicData2.Inspection_Language)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Inspection Language</td> <td>" + oldBasicData2.Inspection_Language + "</td><td>" + NewBasicData2.Inspection_Language + "</td></tr>";
                if (NewBasicData2.Inspection_Text != oldBasicData2.Inspection_Text)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Inspection Text</td> <td>" + oldBasicData2.Inspection_Text + "</td><td>" + NewBasicData2.Inspection_Text + "</td></tr>";
                if (NewBasicData2.Inspection_Language1 != oldBasicData2.Inspection_Language1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Inspection Language1</td> <td>" + oldBasicData2.Inspection_Language1 + "</td><td>" + NewBasicData2.Inspection_Language1 + "</td></tr>";
                if (NewBasicData2.Inspection_Text1 != oldBasicData2.Inspection_Text1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Inspection Text1</td> <td>" + oldBasicData2.Inspection_Text1 + "</td><td>" + NewBasicData2.Inspection_Text1 + "</td></tr>";
                if (NewBasicData2.Internal_Comment_Language != oldBasicData2.Internal_Comment_Language)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Internal Comment Language</td> <td>" + oldBasicData2.Internal_Comment_Language + "</td><td>" + NewBasicData2.Internal_Comment_Language + "</td></tr>";
                if (NewBasicData2.Internal_Comment_Text != oldBasicData2.Internal_Comment_Text)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Internal Comment Text</td> <td>" + oldBasicData2.Internal_Comment_Text + "</td><td>" + NewBasicData2.Internal_Comment_Text + "</td></tr>";
                if (NewBasicData2.Internal_Comment_Language1 != oldBasicData2.Internal_Comment_Language1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Internal Comment Language1</td> <td>" + oldBasicData2.Internal_Comment_Language1 + "</td><td>" + NewBasicData2.Internal_Comment_Language1 + "</td></tr>";
                if (NewBasicData2.Internal_Comment_Text1 != oldBasicData2.Internal_Comment_Text1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Internal Comment Text1</td> <td>" + oldBasicData2.Internal_Comment_Text1 + "</td><td>" + NewBasicData2.Internal_Comment_Text1 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_X != oldBasicData2.Alt_Unit_Value_X)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X</td> <td>" + oldBasicData2.Alt_Unit_Value_X + "</td><td>" + NewBasicData2.Alt_Unit_Value_X + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure != oldBasicData2.Alt_Unit_Of_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Of Measure</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y != oldBasicData2.Alt_Unit_Value_Y)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y</td> <td>" + oldBasicData2.Alt_Unit_Value_Y + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_X1 != oldBasicData2.Alt_Unit_Value_X1)
                   Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X1</td> <td>" + oldBasicData2.Alt_Unit_Value_X1 + "</td><td>" + NewBasicData2.Alt_Unit_Value_X1 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure1 != oldBasicData2.Alt_Unit_Of_Measure1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt_Unit Of Measure1</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure1 + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure1 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y1 != oldBasicData2.Alt_Unit_Value_Y1)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y1</td> <td>" + oldBasicData2.Alt_Unit_Value_Y1 + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y1 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_X2 != oldBasicData2.Alt_Unit_Value_X2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X2</td> <td>" + oldBasicData2.Alt_Unit_Value_X2 + "</td><td>" + NewBasicData2.Alt_Unit_Value_X2 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure2 != oldBasicData2.Alt_Unit_Of_Measure2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Of Measure2</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure2 + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure2 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y2 != oldBasicData2.Alt_Unit_Value_Y2)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y2</td> <td>" + oldBasicData2.Alt_Unit_Value_Y2 + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y2 + "</td></tr>";
                if (NewBasicData2.Shipper_Gross_Weight != oldBasicData2.Shipper_Gross_Weight)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Shipper Gross Weight</td> <td>" + oldBasicData2.Shipper_Gross_Weight + "</td><td>" + NewBasicData2.Shipper_Gross_Weight + "</td></tr>";
                if (NewBasicData2.Shipper_Weight_Unit != oldBasicData2.Shipper_Weight_Unit)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Shipper Weight Unit</td> <td>" + oldBasicData2.Shipper_Weight_Unit + "</td><td>" + NewBasicData2.Shipper_Weight_Unit + "</td></tr>";
                if (NewBasicData2.Unit_Of_Measure_Usage != oldBasicData2.Unit_Of_Measure_Usage)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Unit Of Measure Usage</td> <td>" + oldBasicData2.Unit_Of_Measure_Usage + "</td><td>" + NewBasicData2.Unit_Of_Measure_Usage + "</td></tr>";
                if (NewBasicData2.Characteristic_Name != oldBasicData2.Characteristic_Name)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Characteristic Name</td> <td>" + oldBasicData2.Characteristic_Name + "</td><td>" + NewBasicData2.Characteristic_Name + "</td></tr>";
                if (NewBasicData2.Planned_Value_For_Unit_Measure != oldBasicData2.Planned_Value_For_Unit_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Planned Value For Unit Measure</td> <td>" + oldBasicData2.Planned_Value_For_Unit_Measure + "</td><td>" + NewBasicData2.Planned_Value_For_Unit_Measure + "</td></tr>";
                if (NewBasicData2.Batch_Spcf_Matl_Unit_Measure != oldBasicData2.Batch_Spcf_Matl_Unit_Measure)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Batch Spcf Matl Unit Measure</td> <td>" + oldBasicData2.Batch_Spcf_Matl_Unit_Measure + "</td><td>" + NewBasicData2.Batch_Spcf_Matl_Unit_Measure + "</td></tr>";

                if (NewBasicData2.Alt_Unit_Value_X3 != oldBasicData2.Alt_Unit_Value_X3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X3</td> <td>" + oldBasicData2.Alt_Unit_Value_X3 + "</td><td>" + NewBasicData2.Alt_Unit_Value_X3 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure3 != oldBasicData2.Alt_Unit_Of_Measure3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Of Measure3</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure3 + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure3 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y3 != oldBasicData2.Alt_Unit_Value_Y3)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y3</td> <td>" + oldBasicData2.Alt_Unit_Value_Y3 + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y3 + "</td></tr>";

                if (NewBasicData2.Alt_Unit_Value_X4 != oldBasicData2.Alt_Unit_Value_X4)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X4</td> <td>" + oldBasicData2.Alt_Unit_Value_X4 + "</td><td>" + NewBasicData2.Alt_Unit_Value_X4 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure4 != oldBasicData2.Alt_Unit_Of_Measure4)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Of Measure4</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure4 + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure4 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y4 != oldBasicData2.Alt_Unit_Value_Y4)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y4</td> <td>" + oldBasicData2.Alt_Unit_Value_Y4 + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y4 + "</td></tr>";

                if (NewBasicData2.Alt_Unit_Value_X5 != oldBasicData2.Alt_Unit_Value_X5)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X5</td> <td>" + oldBasicData2.Alt_Unit_Value_X5 + "</td><td>" + NewBasicData2.Alt_Unit_Value_X5 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure5 != oldBasicData2.Alt_Unit_Of_Measure5)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Of Measure5</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure5 + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure5 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y5 != oldBasicData2.Alt_Unit_Value_Y5)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y5</td> <td>" + oldBasicData2.Alt_Unit_Value_Y5 + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y5 + "</td></tr>";

                if (NewBasicData2.Alt_Unit_Value_X6 != oldBasicData2.Alt_Unit_Value_X6)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value X6</td> <td>" + oldBasicData2.Alt_Unit_Value_X6 + "</td><td>" + NewBasicData2.Alt_Unit_Value_X6 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Of_Measure6 != oldBasicData2.Alt_Unit_Of_Measure6)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Of Measure6</td> <td>" + oldBasicData2.Alt_Unit_Of_Measure6 + "</td><td>" + NewBasicData2.Alt_Unit_Of_Measure6 + "</td></tr>";
                if (NewBasicData2.Alt_Unit_Value_Y6 != oldBasicData2.Alt_Unit_Value_Y6)
                    Session[StaticKeys.ApprovalNote] += "<tr><td>Alt Unit Value Y6</td> <td>" + oldBasicData2.Alt_Unit_Value_Y6 + "</td><td>" + NewBasicData2.Alt_Unit_Value_Y6 + "</td></tr>";




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
    private void CheckIfChangesLog(BasicData2 NewBasicData2, BasicData2 oldBasicData2)
    {
        List<SMChange> _items = new List<SMChange>();
        try
        {
            WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "Start Data");
         if (NewBasicData2.Mat_Basic_Data2_Id > 0 && oldBasicData2.Mat_Basic_Data2_Id > 0) 
        {
            WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "Start Data 1");

                if (NewBasicData2.Length != oldBasicData2.Length)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "22" + NewBasicData2.Length + "/" + oldBasicData2.Length);
                    _items.Add(new SMChange { colFieldName = 22, colOldVal = oldBasicData2.Length, colNewVal = NewBasicData2.Length });

                }
                if (NewBasicData2.Width != oldBasicData2.Width)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "23" + NewBasicData2.Width + "/" + oldBasicData2.Width);
                    _items.Add(new SMChange { colFieldName = 23, colOldVal = oldBasicData2.Width, colNewVal = NewBasicData2.Width });

                }
                if (NewBasicData2.Height != oldBasicData2.Height)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "24" + NewBasicData2.Height + "/" + oldBasicData2.Height);
                    _items.Add(new SMChange { colFieldName = 24, colOldVal = oldBasicData2.Height, colNewVal = NewBasicData2.Height });

                }
                if (NewBasicData2.Unit_Of_Dimension != oldBasicData2.Unit_Of_Dimension)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "25" + NewBasicData2.Unit_Of_Dimension + "/" + oldBasicData2.Unit_Of_Dimension);
                    _items.Add(new SMChange { colFieldName = 25, colOldVal = oldBasicData2.Unit_Of_Dimension, colNewVal = NewBasicData2.Unit_Of_Dimension });

                }
                if (NewBasicData2.Desc_Language != oldBasicData2.Desc_Language)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1137" + NewBasicData2.Desc_Language + "/" + oldBasicData2.Desc_Language);
                    _items.Add(new SMChange { colFieldName = 1137, colOldVal = oldBasicData2.Desc_Language, colNewVal = NewBasicData2.Desc_Language });

                }
                if (NewBasicData2.Desc_Text != oldBasicData2.Desc_Text)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1139" + NewBasicData2.Desc_Text + "/" + oldBasicData2.Desc_Text);
                    _items.Add(new SMChange { colFieldName = 1139, colOldVal = oldBasicData2.Desc_Text, colNewVal = NewBasicData2.Desc_Text });

                }
                if (NewBasicData2.Desc_Language1 != oldBasicData2.Desc_Language1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1138" + NewBasicData2.Desc_Language1 + "/" + oldBasicData2.Desc_Language1);
                    _items.Add(new SMChange { colFieldName = 1138, colOldVal = oldBasicData2.Desc_Language1, colNewVal = NewBasicData2.Desc_Language1 });

                }
                if (NewBasicData2.Desc_Text1 != oldBasicData2.Desc_Text1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1140" + NewBasicData2.Desc_Text1 + "/" + oldBasicData2.Desc_Text1);
                    _items.Add(new SMChange { colFieldName = 1140, colOldVal = oldBasicData2.Desc_Text1, colNewVal = NewBasicData2.Desc_Text1 });

                }
                if (NewBasicData2.Basic_Data_Language != oldBasicData2.Basic_Data_Language)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1141" + NewBasicData2.Basic_Data_Language + "/" + oldBasicData2.Basic_Data_Language);
                    _items.Add(new SMChange { colFieldName = 1141, colOldVal = oldBasicData2.Basic_Data_Language, colNewVal = NewBasicData2.Basic_Data_Language });

                }
                if (NewBasicData2.Basic_Data_Text != oldBasicData2.Basic_Data_Text)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1143" + NewBasicData2.Basic_Data_Text + "/" + oldBasicData2.Basic_Data_Text);
                    _items.Add(new SMChange { colFieldName = 1143, colOldVal = oldBasicData2.Basic_Data_Text, colNewVal = NewBasicData2.Basic_Data_Text });

                }
                if (NewBasicData2.Basic_Data_Language1 != oldBasicData2.Basic_Data_Language1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1142" + NewBasicData2.Basic_Data_Language1 + "/" +  oldBasicData2.Basic_Data_Language1);
                    _items.Add(new SMChange { colFieldName = 1142, colOldVal = oldBasicData2.Basic_Data_Language1, colNewVal = NewBasicData2.Basic_Data_Language1 });

                }
                if (NewBasicData2.Basic_Data_Text1 != oldBasicData2.Basic_Data_Text1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1144" + NewBasicData2.Basic_Data_Text1 + "/" + oldBasicData2.Basic_Data_Text1);
                    _items.Add(new SMChange { colFieldName = 1144, colOldVal = oldBasicData2.Basic_Data_Text1, colNewVal = NewBasicData2.Basic_Data_Text1 });

                }
                if (NewBasicData2.Inspection_Language != oldBasicData2.Inspection_Language)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1145" + NewBasicData2.Inspection_Language + "/" + oldBasicData2.Inspection_Language);
                    _items.Add(new SMChange { colFieldName = 1145, colOldVal = oldBasicData2.Inspection_Language, colNewVal = NewBasicData2.Inspection_Language });

                }
                if (NewBasicData2.Inspection_Text != oldBasicData2.Inspection_Text)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1147" + NewBasicData2.Inspection_Text + "/" + oldBasicData2.Inspection_Text);
                    _items.Add(new SMChange { colFieldName = 1147, colOldVal = oldBasicData2.Inspection_Text, colNewVal = NewBasicData2.Inspection_Text });

                }
                if (NewBasicData2.Inspection_Language1 != oldBasicData2.Inspection_Language1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1146" + NewBasicData2.Inspection_Language1 + "/" + oldBasicData2.Inspection_Language1);
                    _items.Add(new SMChange { colFieldName = 1146, colOldVal = oldBasicData2.Inspection_Language1, colNewVal = NewBasicData2.Inspection_Language1 });

                }
                if (NewBasicData2.Inspection_Text1 != oldBasicData2.Inspection_Text1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1148" + NewBasicData2.Inspection_Text1 + "/" + oldBasicData2.Inspection_Text1);
                    _items.Add(new SMChange { colFieldName = 1148, colOldVal = oldBasicData2.Inspection_Text1, colNewVal = NewBasicData2.Inspection_Text1 });

                }
                if (NewBasicData2.Internal_Comment_Language != oldBasicData2.Internal_Comment_Language)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1149" + NewBasicData2.Internal_Comment_Language + "/" + oldBasicData2.Internal_Comment_Language);
                    _items.Add(new SMChange { colFieldName = 1149, colOldVal = oldBasicData2.Internal_Comment_Language, colNewVal = NewBasicData2.Internal_Comment_Language });

                }
                if (NewBasicData2.Internal_Comment_Text != oldBasicData2.Internal_Comment_Text)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1151" + NewBasicData2.Internal_Comment_Text + "/" + oldBasicData2.Internal_Comment_Text);
                    _items.Add(new SMChange { colFieldName = 1151, colOldVal = oldBasicData2.Internal_Comment_Text, colNewVal = NewBasicData2.Internal_Comment_Text });

                }
                if (NewBasicData2.Internal_Comment_Language1 != oldBasicData2.Internal_Comment_Language1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1150" + NewBasicData2.Internal_Comment_Language1 + "/" + oldBasicData2.Internal_Comment_Language1);
                    _items.Add(new SMChange { colFieldName = 1150, colOldVal = oldBasicData2.Internal_Comment_Language1, colNewVal = NewBasicData2.Internal_Comment_Language1 });

                }
                if (NewBasicData2.Internal_Comment_Text1 != oldBasicData2.Internal_Comment_Text1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1152" + NewBasicData2.Internal_Comment_Text1 + "/" + oldBasicData2.Internal_Comment_Text1);
                    _items.Add(new SMChange { colFieldName = 1152, colOldVal = oldBasicData2.Internal_Comment_Text1, colNewVal = NewBasicData2.Internal_Comment_Text1 });

                }
                if (NewBasicData2.Alt_Unit_Value_X != oldBasicData2.Alt_Unit_Value_X)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1153" + NewBasicData2.Alt_Unit_Value_X + "/" + oldBasicData2.Alt_Unit_Value_X);
                    _items.Add(new SMChange { colFieldName = 1153, colOldVal = oldBasicData2.Alt_Unit_Value_X, colNewVal = NewBasicData2.Alt_Unit_Value_X });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure != oldBasicData2.Alt_Unit_Of_Measure)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1156" + NewBasicData2.Alt_Unit_Of_Measure + "/" + oldBasicData2.Alt_Unit_Of_Measure);
                    _items.Add(new SMChange { colFieldName = 1156, colOldVal = oldBasicData2.Alt_Unit_Of_Measure, colNewVal = NewBasicData2.Alt_Unit_Of_Measure });

                }
                if (NewBasicData2.Alt_Unit_Value_Y != oldBasicData2.Alt_Unit_Value_Y)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1159" + NewBasicData2.Alt_Unit_Value_Y + "/" + oldBasicData2.Alt_Unit_Value_Y);
                    _items.Add(new SMChange { colFieldName = 1159, colOldVal = oldBasicData2.Alt_Unit_Value_Y, colNewVal = NewBasicData2.Alt_Unit_Value_Y });

                }
                if (NewBasicData2.Alt_Unit_Value_X1 != oldBasicData2.Alt_Unit_Value_X1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1154" + NewBasicData2.Alt_Unit_Value_X1 + "/" + oldBasicData2.Alt_Unit_Value_X1);
                    _items.Add(new SMChange { colFieldName = 1154, colOldVal = oldBasicData2.Alt_Unit_Value_X1, colNewVal = NewBasicData2.Alt_Unit_Value_X1 });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure1 != oldBasicData2.Alt_Unit_Of_Measure1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1157" + NewBasicData2.Alt_Unit_Of_Measure1 + "/" + oldBasicData2.Alt_Unit_Of_Measure1);
                    _items.Add(new SMChange { colFieldName = 1157, colOldVal = oldBasicData2.Alt_Unit_Of_Measure1, colNewVal = NewBasicData2.Alt_Unit_Of_Measure1 });

                }
                if (NewBasicData2.Alt_Unit_Value_Y1 != oldBasicData2.Alt_Unit_Value_Y1)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1160" + NewBasicData2.Alt_Unit_Value_Y1 + "/" + oldBasicData2.Alt_Unit_Value_Y1);
                    _items.Add(new SMChange { colFieldName = 1160, colOldVal = oldBasicData2.Alt_Unit_Value_Y1, colNewVal = NewBasicData2.Alt_Unit_Value_Y1 });

                }
                if (NewBasicData2.Alt_Unit_Value_X2 != oldBasicData2.Alt_Unit_Value_X2)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1155" + NewBasicData2.Alt_Unit_Value_X2 + "/" + oldBasicData2.Alt_Unit_Value_X2);
                    _items.Add(new SMChange { colFieldName = 1155, colOldVal = oldBasicData2.Alt_Unit_Value_X2, colNewVal = NewBasicData2.Alt_Unit_Value_X2 });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure2 != oldBasicData2.Alt_Unit_Of_Measure2)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1158" + NewBasicData2.Alt_Unit_Of_Measure2 + "/" + oldBasicData2.Alt_Unit_Of_Measure2);
                    _items.Add(new SMChange { colFieldName = 1158, colOldVal = oldBasicData2.Alt_Unit_Of_Measure2, colNewVal = NewBasicData2.Alt_Unit_Of_Measure2 });

                }
                if (NewBasicData2.Alt_Unit_Value_Y2 != oldBasicData2.Alt_Unit_Value_Y2)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1161" + NewBasicData2.Alt_Unit_Value_Y2 + "/" + oldBasicData2.Alt_Unit_Value_Y2);
                    _items.Add(new SMChange { colFieldName = 1161, colOldVal = oldBasicData2.Alt_Unit_Value_Y2, colNewVal = NewBasicData2.Alt_Unit_Value_Y2 });

                }
                if (NewBasicData2.Shipper_Gross_Weight != oldBasicData2.Shipper_Gross_Weight)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1162" + NewBasicData2.Shipper_Gross_Weight + "/" + oldBasicData2.Shipper_Gross_Weight);
                    _items.Add(new SMChange { colFieldName = 1162, colOldVal = oldBasicData2.Shipper_Gross_Weight, colNewVal = NewBasicData2.Shipper_Gross_Weight });

                }
                if (NewBasicData2.Shipper_Weight_Unit != oldBasicData2.Shipper_Weight_Unit)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1163" + NewBasicData2.Shipper_Weight_Unit + "/" + oldBasicData2.Shipper_Weight_Unit);
                    _items.Add(new SMChange { colFieldName = 1163, colOldVal = oldBasicData2.Shipper_Weight_Unit, colNewVal = NewBasicData2.Shipper_Weight_Unit });

                }
                if (NewBasicData2.Unit_Of_Measure_Usage != oldBasicData2.Unit_Of_Measure_Usage)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1164" + NewBasicData2.Unit_Of_Measure_Usage + "/" + oldBasicData2.Unit_Of_Measure_Usage);
                    _items.Add(new SMChange { colFieldName = 1164, colOldVal = oldBasicData2.Unit_Of_Measure_Usage, colNewVal = NewBasicData2.Unit_Of_Measure_Usage });

                }
                if (NewBasicData2.Characteristic_Name != oldBasicData2.Characteristic_Name)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1165" + NewBasicData2.Characteristic_Name + "/" + oldBasicData2.Characteristic_Name);
                    _items.Add(new SMChange { colFieldName = 1165, colOldVal = oldBasicData2.Characteristic_Name, colNewVal = NewBasicData2.Characteristic_Name });

                }
                if (NewBasicData2.Planned_Value_For_Unit_Measure != oldBasicData2.Planned_Value_For_Unit_Measure)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1166" + NewBasicData2.Planned_Value_For_Unit_Measure + "/" + oldBasicData2.Planned_Value_For_Unit_Measure);
                    _items.Add(new SMChange { colFieldName = 1166, colOldVal = oldBasicData2.Planned_Value_For_Unit_Measure, colNewVal = NewBasicData2.Planned_Value_For_Unit_Measure });

                }
                if (NewBasicData2.Batch_Spcf_Matl_Unit_Measure != oldBasicData2.Batch_Spcf_Matl_Unit_Measure)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1167" + NewBasicData2.Batch_Spcf_Matl_Unit_Measure + "/" + oldBasicData2.Batch_Spcf_Matl_Unit_Measure);
                    _items.Add(new SMChange { colFieldName = 1167, colOldVal = oldBasicData2.Batch_Spcf_Matl_Unit_Measure, colNewVal = NewBasicData2.Batch_Spcf_Matl_Unit_Measure });

                }

                if (NewBasicData2.Alt_Unit_Value_X3 != oldBasicData2.Alt_Unit_Value_X3)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1408" + NewBasicData2.Alt_Unit_Value_X3 + "/" + oldBasicData2.Alt_Unit_Value_X3);
                    _items.Add(new SMChange { colFieldName = 1408, colOldVal = oldBasicData2.Alt_Unit_Value_X3, colNewVal = NewBasicData2.Alt_Unit_Value_X3 });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure3 != oldBasicData2.Alt_Unit_Of_Measure3)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1409" + NewBasicData2.Alt_Unit_Of_Measure3 + "/" + oldBasicData2.Alt_Unit_Of_Measure3);
                    _items.Add(new SMChange { colFieldName = 1409, colOldVal = oldBasicData2.Alt_Unit_Of_Measure3, colNewVal = NewBasicData2.Alt_Unit_Of_Measure3 });

                }
                if (NewBasicData2.Alt_Unit_Value_Y3 != oldBasicData2.Alt_Unit_Value_Y3)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1410" + NewBasicData2.Alt_Unit_Value_Y3 + "/" + oldBasicData2.Alt_Unit_Value_Y3);
                    _items.Add(new SMChange { colFieldName = 1410, colOldVal = oldBasicData2.Alt_Unit_Value_Y3, colNewVal = NewBasicData2.Alt_Unit_Value_Y3 });

                }

                if (NewBasicData2.Alt_Unit_Value_X4 != oldBasicData2.Alt_Unit_Value_X4)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1411" + NewBasicData2.Alt_Unit_Value_X4 + "/" + oldBasicData2.Alt_Unit_Value_X4);
                    _items.Add(new SMChange { colFieldName = 1411, colOldVal = oldBasicData2.Alt_Unit_Value_X4, colNewVal = NewBasicData2.Alt_Unit_Value_X4 });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure4 != oldBasicData2.Alt_Unit_Of_Measure4)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1412" + NewBasicData2.Alt_Unit_Of_Measure4 + "/" + oldBasicData2.Alt_Unit_Of_Measure4);
                    _items.Add(new SMChange { colFieldName = 1412, colOldVal = oldBasicData2.Alt_Unit_Of_Measure4, colNewVal = NewBasicData2.Alt_Unit_Of_Measure4 });

                }
                if (NewBasicData2.Alt_Unit_Value_Y4 != oldBasicData2.Alt_Unit_Value_Y4)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1413" + oldBasicData2.Alt_Unit_Value_Y4 + "/" + NewBasicData2.Alt_Unit_Value_Y4);
                    _items.Add(new SMChange { colFieldName = 1413, colOldVal = oldBasicData2.Alt_Unit_Value_Y4, colNewVal = NewBasicData2.Alt_Unit_Value_Y4 });

                }

                if (NewBasicData2.Alt_Unit_Value_X5 != oldBasicData2.Alt_Unit_Value_X5)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1414" + oldBasicData2.Alt_Unit_Value_X5 + "/" + NewBasicData2.Alt_Unit_Value_X5);
                    _items.Add(new SMChange { colFieldName = 1414, colOldVal = oldBasicData2.Alt_Unit_Value_X5, colNewVal = NewBasicData2.Alt_Unit_Value_X5 });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure5 != oldBasicData2.Alt_Unit_Of_Measure5)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1415" + oldBasicData2.Alt_Unit_Of_Measure5 + "/" + NewBasicData2.Alt_Unit_Of_Measure5);
                    _items.Add(new SMChange { colFieldName = 1415, colOldVal = oldBasicData2.Alt_Unit_Of_Measure5, colNewVal = NewBasicData2.Alt_Unit_Of_Measure5 });

                }
                if (NewBasicData2.Alt_Unit_Value_Y5 != oldBasicData2.Alt_Unit_Value_Y5)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1416" + oldBasicData2.Alt_Unit_Value_Y5 + "/" + NewBasicData2.Alt_Unit_Value_Y5);
                    _items.Add(new SMChange { colFieldName = 1416, colOldVal = oldBasicData2.Alt_Unit_Value_Y5, colNewVal = NewBasicData2.Alt_Unit_Value_Y5 });

                }

                if (NewBasicData2.Alt_Unit_Value_X6 != oldBasicData2.Alt_Unit_Value_X6)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1417" + NewBasicData2.Alt_Unit_Value_X6 + "/" + oldBasicData2.Alt_Unit_Value_X6);
                    _items.Add(new SMChange { colFieldName = 1417, colOldVal = oldBasicData2.Alt_Unit_Value_X6, colNewVal = NewBasicData2.Alt_Unit_Value_X6 });

                }
                if (NewBasicData2.Alt_Unit_Of_Measure6 != oldBasicData2.Alt_Unit_Of_Measure6)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1418" + oldBasicData2.Alt_Unit_Of_Measure6 + "/" + NewBasicData2.Alt_Unit_Of_Measure6);
                    _items.Add(new SMChange { colFieldName = 1418, colOldVal = oldBasicData2.Alt_Unit_Of_Measure6, colNewVal = NewBasicData2.Alt_Unit_Of_Measure6 });

                }
                if (NewBasicData2.Alt_Unit_Value_Y6 != oldBasicData2.Alt_Unit_Value_Y6)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "1419" + NewBasicData2.Alt_Unit_Value_Y6 + "/" + oldBasicData2.Alt_Unit_Value_Y6);
                    _items.Add(new SMChange { colFieldName = 1419, colOldVal = oldBasicData2.Alt_Unit_Value_Y6, colNewVal = NewBasicData2.Alt_Unit_Value_Y6 });

                }





        }
        WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "End Data");
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
                WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "Start Insert");
                int ChangeSMatID1;
                ChangeSMatID1 = helperAccess.MaterialChange("4", Convert.ToString(Session[StaticKeys.MasterHeaderId]), Convert.ToString(Session[StaticKeys.LoggedIn_User_Id]));
                if (ChangeSMatID1 > 0)
                {
                    WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "Start Insert1");
                    foreach (var scItem in _items)
                    {
                        WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "Start Insert3");
                        helperAccess.MaterialChangeDetails(ChangeSMatID1, scItem.colFieldName, scItem.colOldVal, scItem.colNewVal);
                        WriteMatChangeLog("MatChangeLogBD2" + sdate + ".txt", "End Insert3");
                    }
                }
                _items = new List<SMChange>();
            }
        }
        catch (Exception ex)
        {
            _log.Error("CheckIfChangesLog1", ex);
            //    throw ex;
        }

    }

    private bool CheckValidBasicData2()
    {
        bool flag = true;
        try
        {
        if ((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 162) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 164))
        {
            if (txtLength.Text != "" || txtWidth.Text != "" || txtHeight.Text != "" || ddlUnitDimension.SelectedValue != "")
            {
                flag = false;
            }

            }
        }
        catch (Exception ex)
        { _log.Error("CheckValidBasicData2", ex); }

        return flag;
    }

    //private void ReadCommanData(string masterHeaderId)
    //{
    //    BasicDataAccess basicDataAccess = new BasicDataAccess();
    //    DataSet dstData = new DataSet();
    //    try
    //    {
    //        dstData = basicDataAccess.ReadCommanData(masterHeaderId);
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            lblMaterialNumber.Text = dstData.Tables[0].Rows[0]["Material_Number"].ToString();
    //            lblMaterialType.Text = dstData.Tables[0].Rows[0]["Material_Type"].ToString();
    //            lblIndstrySector.Text = dstData.Tables[0].Rows[0]["Industory_Sector"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void ReadBasicData2(string masterHeaderId)
    {
        BasicData2 ObjBasicData2 = new BasicData2();
        BasicDataAccess basicDataAccess = new BasicDataAccess();
        DataSet dstData = new DataSet();
        try
        {
            ObjBasicData2 = basicDataAccess.GetBasicData2(lblMasterHeaderId.Text);

            if (ObjBasicData2.Mat_Basic_Data2_Id > 0)
            {
                lblBasicData2Id.Text = ObjBasicData2.Mat_Basic_Data2_Id.ToString();
                txtLength.Text = ObjBasicData2.Length;
                txtWidth.Text = ObjBasicData2.Width;
                txtHeight.Text = ObjBasicData2.Height;
                ddlUnitDimension.SelectedValue = ObjBasicData2.Unit_Of_Dimension;

                ddlDescLanguage.SelectedValue = ObjBasicData2.Desc_Language;
                txtDescText.Text = ObjBasicData2.Desc_Text;
                ddlDescLanguage1.SelectedValue = ObjBasicData2.Desc_Language1;
                txtDescText1.Text = ObjBasicData2.Desc_Text1;
                ddlBasicDataLanguage.SelectedValue = ObjBasicData2.Basic_Data_Language;
                txtBasicDataText.Text = ObjBasicData2.Basic_Data_Text;
                ddlBasicDataLanguage1.SelectedValue = ObjBasicData2.Basic_Data_Language1;
                txtBasicDataText1.Text = ObjBasicData2.Basic_Data_Text1;
                ddlInspectionLanguage.SelectedValue = ObjBasicData2.Inspection_Language;
                txtInspectionText.Text = ObjBasicData2.Inspection_Text;
                ddlInspectionLanguage1.SelectedValue = ObjBasicData2.Inspection_Language1;
                txtInspectionText1.Text = ObjBasicData2.Inspection_Text1;
                ddlInternalCommentLanguage.SelectedValue = ObjBasicData2.Internal_Comment_Language;
                txtInternalCommentText.Text = ObjBasicData2.Internal_Comment_Text;
                ddlInternalCommentLanguage1.SelectedValue = ObjBasicData2.Internal_Comment_Language1;
                txtInternalCommentText1.Text = ObjBasicData2.Internal_Comment_Text1;

                txtAltUnitValueX.Text = ObjBasicData2.Alt_Unit_Value_X;
                ddlAltUnitOfMeasure.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure;
                txtAltUnitValueY.Text = ObjBasicData2.Alt_Unit_Value_Y;
                AltUnitOfMeasureSetUp();

                txtAltUnitValueX1.Text = ObjBasicData2.Alt_Unit_Value_X1;
                ddlAltUnitOfMeasure1.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure1;
                txtAltUnitValueY1.Text = ObjBasicData2.Alt_Unit_Value_Y1;
                AltUnitOfMeasure1SetUp();

                txtAltUnitValueX2.Text = ObjBasicData2.Alt_Unit_Value_X2;
                ddlAltUnitOfMeasure2.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure2;
                txtAltUnitValueY2.Text = ObjBasicData2.Alt_Unit_Value_Y2;
                AltUnitOfMeasure2SetUp();

  txtAltUnitValueX3.Text = ObjBasicData2.Alt_Unit_Value_X3;
                ddlAltUnitOfMeasure3.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure3;
                txtAltUnitValueY3.Text = ObjBasicData2.Alt_Unit_Value_Y3;
                AltUnitOfMeasure3SetUp();
 
                txtAltUnitValueX4.Text = ObjBasicData2.Alt_Unit_Value_X4;
                ddlAltUnitOfMeasure4.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure4;
                txtAltUnitValueY4.Text = ObjBasicData2.Alt_Unit_Value_Y4;
                AltUnitOfMeasure4SetUp();

                txtAltUnitValueX5.Text = ObjBasicData2.Alt_Unit_Value_X5;
                ddlAltUnitOfMeasure5.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure5;
                txtAltUnitValueY5.Text = ObjBasicData2.Alt_Unit_Value_Y5;
                AltUnitOfMeasure5SetUp();

                txtAltUnitValueX6.Text = ObjBasicData2.Alt_Unit_Value_X6;
                ddlAltUnitOfMeasure6.SelectedValue = ObjBasicData2.Alt_Unit_Of_Measure6;
                txtAltUnitValueY6.Text = ObjBasicData2.Alt_Unit_Value_Y6;
                AltUnitOfMeasure6SetUp();


                txtShipperGrossWeight.Text = ObjBasicData2.Shipper_Gross_Weight;
                ddlShipperWeightUnit.SelectedValue = ObjBasicData2.Shipper_Weight_Unit;
                ddlUnitOfMeasureUsage.SelectedValue = ObjBasicData2.Unit_Of_Measure_Usage;
                ddlCharacteristicName.SelectedValue = ObjBasicData2.Characteristic_Name;
                txtPlannedValueForUnitMeasure.Text = ObjBasicData2.Planned_Value_For_Unit_Measure;
                ddlBatchSpcfMatlUnitMeasure.SelectedValue = ObjBasicData2.Batch_Spcf_Matl_Unit_Measure;
            }
            else
            {
                lblBasicData2Id.Text = "0";

                ddlDescLanguage.SelectedValue = "E";
                ddlBasicDataLanguage.SelectedValue = "E";
                ddlInspectionLanguage.SelectedValue = "E";
                ddlInternalCommentLanguage.SelectedValue = "E";

                AltUnitOfMeasureSetUp();
                AltUnitOfMeasure1SetUp();
                AltUnitOfMeasure2SetUp();
 AltUnitOfMeasure3SetUp();
 AltUnitOfMeasure4SetUp();
 AltUnitOfMeasure5SetUp();
 AltUnitOfMeasure6SetUp();
            }

            ProportionUnitSetUp();

            DataSet ds = basicDataAccess.ReadBasicData1(lblMasterHeaderId.Text);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblBaseUnit1.Text = "   " + ds.Tables[0].Rows[0]["Base_Unit_Of_Measure_Full"].ToString();
                lblBaseUnit2.Text = lblBaseUnit1.Text;
                lblBaseUnit3.Text = lblBaseUnit1.Text;

                lblBaseUnit33.Text = lblBaseUnit1.Text;
                lblBaseUnit44.Text = lblBaseUnit1.Text;
                lblBaseUnit55.Text = lblBaseUnit1.Text;
                lblBaseUnit66.Text = lblBaseUnit1.Text;

                //Base_Unit_Of_Measure
                CompddlAltUnitOfMeasure.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();
                CompddlAltUnitOfMeasure1.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();
                CompddlAltUnitOfMeasure2.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();

                CompddlAltUnitOfMeasure3.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();
                CompddlAltUnitOfMeasure4.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();
                CompddlAltUnitOfMeasure5.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();
                CompddlAltUnitOfMeasure6.ValueToCompare = ds.Tables[0].Rows[0]["Base_Unit_Of_Measure"].ToString();
            }
        }
        catch (Exception ex)
        {
            _log.Error("ReadBasicData2", ex);
        }
    }

    private BasicData2 GetControlsValue()
    {
        BasicData2 objBasicData = new BasicData2();
        Utility objUtil = new Utility();
        try
        {
        objBasicData.Mat_Basic_Data2_Id = Convert.ToInt32(lblBasicData2Id.Text);
        objBasicData.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
        objBasicData.Length = txtLength.Text;
        objBasicData.Width = txtWidth.Text;
        objBasicData.Height = txtHeight.Text;
        objBasicData.Unit_Of_Dimension = ddlUnitDimension.SelectedValue;

        objBasicData.Desc_Language = ddlDescLanguage.SelectedValue;
        objBasicData.Desc_Text = txtDescText.Text;
        objBasicData.Desc_Language1 = ddlDescLanguage1.SelectedValue;
        objBasicData.Desc_Text1 = txtDescText1.Text;
        objBasicData.Basic_Data_Language = ddlBasicDataLanguage.SelectedValue;
        objBasicData.Basic_Data_Text = txtBasicDataText.Text;
        objBasicData.Basic_Data_Language1 = ddlBasicDataLanguage1.SelectedValue;
        objBasicData.Basic_Data_Text1 = txtBasicDataText1.Text;
        objBasicData.Inspection_Language = ddlInspectionLanguage.SelectedValue;
        objBasicData.Inspection_Text = txtInspectionText.Text;
        objBasicData.Inspection_Language1 = ddlInspectionLanguage1.SelectedValue;
        objBasicData.Inspection_Text1 = txtInspectionText1.Text;
        objBasicData.Internal_Comment_Language = ddlInternalCommentLanguage.SelectedValue;
        objBasicData.Internal_Comment_Text = txtInternalCommentText.Text;
        objBasicData.Internal_Comment_Language1 = ddlInternalCommentLanguage1.SelectedValue;
        objBasicData.Internal_Comment_Text1 = txtInternalCommentText1.Text;
        objBasicData.Alt_Unit_Value_X = txtAltUnitValueX.Text;
        objBasicData.Alt_Unit_Of_Measure = ddlAltUnitOfMeasure.SelectedValue;
        objBasicData.Alt_Unit_Value_Y = txtAltUnitValueY.Text;
        objBasicData.Alt_Unit_Value_X1 = txtAltUnitValueX1.Text;
        objBasicData.Alt_Unit_Of_Measure1 = ddlAltUnitOfMeasure1.SelectedValue;
        objBasicData.Alt_Unit_Value_Y1 = txtAltUnitValueY1.Text;
        objBasicData.Alt_Unit_Value_X2 = txtAltUnitValueX2.Text;
        objBasicData.Alt_Unit_Of_Measure2 = ddlAltUnitOfMeasure2.SelectedValue;
        objBasicData.Alt_Unit_Value_Y2 = txtAltUnitValueY2.Text;
        objBasicData.Shipper_Gross_Weight = txtShipperGrossWeight.Text;
        objBasicData.Shipper_Weight_Unit = ddlShipperWeightUnit.SelectedValue;
        objBasicData.Unit_Of_Measure_Usage = ddlUnitOfMeasureUsage.SelectedValue;
        objBasicData.Characteristic_Name = ddlCharacteristicName.SelectedValue;
        objBasicData.Planned_Value_For_Unit_Measure = txtPlannedValueForUnitMeasure.Text;
        objBasicData.Batch_Spcf_Matl_Unit_Measure = ddlBatchSpcfMatlUnitMeasure.SelectedValue;


        objBasicData.IsActive = 1;
        objBasicData.UserId = lblUserId.Text;
        objBasicData.TodayDate = objUtil.GetDate();
        objBasicData.IPAddress = objUtil.GetIpAddress();

  objBasicData.Alt_Unit_Value_X3 = txtAltUnitValueX3.Text;
        objBasicData.Alt_Unit_Of_Measure3 = ddlAltUnitOfMeasure3.SelectedValue;
        objBasicData.Alt_Unit_Value_Y3 = txtAltUnitValueY3.Text;

        objBasicData.Alt_Unit_Value_X4 = txtAltUnitValueX4.Text;
        objBasicData.Alt_Unit_Of_Measure4 = ddlAltUnitOfMeasure4.SelectedValue;
        objBasicData.Alt_Unit_Value_Y4 = txtAltUnitValueY4.Text;

        objBasicData.Alt_Unit_Value_X5 = txtAltUnitValueX5.Text;
        objBasicData.Alt_Unit_Of_Measure5 = ddlAltUnitOfMeasure5.SelectedValue;
        objBasicData.Alt_Unit_Value_Y5 = txtAltUnitValueY5.Text;

        objBasicData.Alt_Unit_Value_X6 = txtAltUnitValueX6.Text;
        objBasicData.Alt_Unit_Of_Measure6 = ddlAltUnitOfMeasure6.SelectedValue;
        objBasicData.Alt_Unit_Value_Y6 = txtAltUnitValueY6.Text;

        }
        catch (Exception ex)
        { _log.Error("GetControlsValue", ex); }
        return objBasicData;
    }

    private void ConfigureControl()
    {
        try
        {
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Basic2 obj = new SectionConfiguration.Basic2();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlAddNew, obj.GetClass(str));
        }
        catch (Exception ex)
        { _log.Error("ConfigureControl", ex); }
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