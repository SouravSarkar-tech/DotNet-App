using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using log4net;
public partial class Transaction_BOMRecipe_ProdVersion : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    RecipeAccess objRecipeAccess = new RecipeAccess();

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
                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                        bool flag = false;
                        //8200055588 enable valid from date for requestor and master cell
                        if (deptId == "0" || deptId == "13")
                        {
                            txtProdVFrom.Enabled = true;
                            txtProdVTo.Enabled = true;
                        }
                        //8200055588 enable valid from date for requestor and master cell

                        //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                        if ((checkPrevioussection() && deptId == "0" && moduleId == "186") || (checkPrevioussection() && deptId == "0" && moduleId == "227") || (checkPrevioussection() && deptId == "0" && moduleId == "229") || (checkPrevioussection() && deptId == "0" && moduleId == "185"))
                        {
                            flag = true;
                        }
                        else if (deptId != "0")
                        {
                            flag = true;
                        }
                        //add new modules (227,228,229) for Create new BOM and Reciepe //BOM_8200050878
                        else if ((moduleId == "180") || (moduleId == "228"))
                        {
                            flag = true;
                        }
                        if (flag == true)
                        {
                            HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                            PopuplateDropDownList();
                            FillProdVersion();

                            if (((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (mode == "N" || mode == "M") && (Session[StaticKeys.RequestStatus].ToString() != "P")) || (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11"))
                            {
                                trButton.Visible = true;
                                btnProdSave.Visible = !btnNext.Visible;
                                if (!btnPrevious.Visible && !btnNext.Visible)
                                    btnProdSave.Visible = false;
                            }
                            //BOM_8200050878 for new Module created 227,228,229
                            //manali chavan
                            // if ((Session[StaticKeys.RecipeGroup].ToString() != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "232") || (Session[StaticKeys.SelectedModuleId].ToString() == "236")))
                            //{
                            //  trButton.Visible = true;
                            //btnProdSave.Visible = !btnNext.Visible;
                            //if (!btnPrevious.Visible && !btnNext.Visible)
                            //  btnProdSave.Visible = false;
                            //}
                            //DT_26-08-2020 
                            if ((txtProdVersion.Text != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
                            {
                                trButton.Visible = true;
                                btnProdSave.Visible = !btnNext.Visible;
                                if (!btnPrevious.Visible && !btnNext.Visible)
                                    btnProdSave.Visible = false;
                            }
                            //BOM_NWF_SDT05072019
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18")
                            {
                                trButton.Visible = true;
                                btnProdSave.Visible = !btnNext.Visible;
                                if (!btnPrevious.Visible && !btnNext.Visible)
                                    btnProdSave.Visible = false;
                            }
                            //BOM_NWF_EDT05072019
                            if ((mode == "M" || mode == "N") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27" && Session[StaticKeys.MaterialPlantId].ToString() == "19")
                            {
                                trButton.Visible = true;
                                btnProdSave.Visible = true;
                            }
                            ConfigureProdVersion();
                        }
                        else
                        {
                            Type cstype = this.GetType();
                            ClientScriptManager cs = Page.ClientScript;
                            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                            {
                                String cstext = "alert('";
                                cstext += "Please Fill Reciepe Header Details in Reciepe section or Operation details in Operation section ');";

                                //cstext += "Please select the request(s) and click on Mass Submit to send the request(s) for Mass processing.');";

                                //String cstext = "if(confirm('Is request processing Complete?')){RequestSubmitPage();};";
                                cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                                Response.Redirect("Reciepe.aspx");
                            }
                        }


                        if ((moduleId == "180" || moduleId == "185" || moduleId == "186" || moduleId == "228") && (deptId == "11"))
                        {
                            //reqddlRStatus.Visible = true;
                            ////lableddlRStatus.Visible = true;
                            ddlRStatus.Visible = true;
                            lblddlRStatus.Visible = true;
                            ddlRStatus.Enabled = true;
                            reqddlRStatus.Enabled = true;
                            lblddlRStatus.Visible = true;
                            ddlLock.Enabled = true;
                        }
                        //else if ((moduleId == "185" || moduleId == "186") && (deptId == "18") && (Session[StaticKeys.RecipeGroup].ToString() != "") && Session[StaticKeys.MaterialPlantId].ToString() == "10")
                        else if ((moduleId == "185" || moduleId == "186" || moduleId == "229" || moduleId == "227") && (deptId == "18") && (Session[StaticKeys.RecipeGroup].ToString() != ""))
                        {
                            ddlRStatus.Visible = true;
                            lblddlRStatus.Visible = true;
                            ddlRStatus.Enabled = true;
                            reqddlRStatus.Enabled = true;
                            lblddlRStatus.Visible = true;
                            ddlLock.Enabled = true;
                            //lblddlLock.Visible = true;

                            //reqddlLocks.Enabled = true;
                            //lblddlLock.Visible = true;
                        }
                        //else if ((moduleId == "180" || moduleId == "228") && (deptId == "18") && (txtProdAltBOM.Text.Trim() != "") && Session[StaticKeys.MaterialPlantId].ToString() == "10")
                        else if ((moduleId == "180" || moduleId == "228") && (deptId == "18") && (txtProdAltBOM.Text.Trim() != ""))
                        {
                            ddlRStatus.Visible = true;
                            lblddlRStatus.Visible = true;
                            ddlRStatus.Enabled = true;
                            reqddlRStatus.Enabled = true;
                            lblddlRStatus.Visible = true; 
                            ddlLock.Enabled = true;
                        }
                        else
                        {
                            ddlRStatus.Enabled = false;
                            reqddlRStatus.Enabled = false;
                            //reqddlRStatus.Visible = false;
                            ////lableddlRStatus.Visible = false;
                            ddlRStatus.Visible = false;
                            lblddlRStatus.Visible = false;
                            lblddlRStatus.Visible = false;
                             
                            ddlLock.Enabled = false;

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnProdSave_Click(object sender, EventArgs e)
    {
        try
        {

            //8200055588 enable valid from date for requestor and master cell
            DateTime ValidFrom = DateTime.ParseExact(txtProdVFrom.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime ValidTo = DateTime.ParseExact(txtProdVTo.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (ValidFrom > ValidTo)
            {
                lblMsg.Text = "Valid From date should be less than Valid To date.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            else
            {
                if (SaveProdVersion())
                {
                    FillProdVersion();
                    lblMsg.Text = "Header saved";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }


        }
        catch (Exception ex)
        { _log.Error("btnProdSave_Click", ex); }
    }

    //protected void txtProdVersion_TextChanged(object sender, EventArgs e)
    //{
    //Start Commenting by Nitish Rao 26-02-2018
    //if (txtProdVersion.Text != "")
    //{
    //    txtProdVersion.Text = Convert.ToInt32(txtProdVersion.Text).ToString("D4");
    //    //if (Session[StaticKeys.SelectedModuleId].ToString() == "180")
    //    //{
    //    //    if (txtDPGroup.Text != "" && txtProdVersion.Text != "")
    //    //    {
    //    //        ProdVersion objProdVer = objRecipeAccess.GetRecipeData(txtDPGroup.Text, txtProdVersion.Text, Session[StaticKeys.MaterialPlantId].ToString());
    //    //        if (objProdVer.RecipeGroup != null)
    //    //        {
    //    //            txtProdVerDesc.Text = objProdVer.ProdVersion_Text;
    //    //            txtProdFrom.Text = objProdVer.ProdFrom;
    //    //            txtProdTo.Text = objProdVer.ProdTo;
    //    //            txtDPGroupCntr.Text = objProdVer.GroupCntr;
    //    //            ddlLock.SelectedValue = objProdVer.Lock == "" ? " " : objProdVer.Lock;
    //    //            pnlMsg.Visible = false;
    //    //        }
    //    //        else
    //    //        {
    //    //            txtProdVerDesc.Text = "";
    //    //            txtProdFrom.Text = "";
    //    //            txtProdTo.Text = "";
    //    //            lblMsg.Text = "Invalid Recipe group and production version combination.";
    //    //            pnlMsg.Visible = true;
    //    //            pnlMsg.CssClass = "error";
    //    //        }
    //    //    }
    //    //}
    //    //else if (Session[StaticKeys.SelectedModuleId].ToString() == "186")
    //    //{
    //        int maxPrdVer = objRecipeAccess.GetMaxProdVerCreatedForMat(txtMaterialNo.Text, Session[StaticKeys.MaterialPlantId].ToString());
    //        if (maxPrdVer == 0)
    //        {
    //            //If no Prod Version is created for material
    //            if (Convert.ToInt32(txtProdVersion.Text) > maxPrdVer + 1)
    //            {
    //                lblMsg.Text = "No Prod version is created for material " + txtMaterialNo.Text + " .Mention Prod Version BOM as 0001.";
    //                pnlMsg.Visible = true;
    //                pnlMsg.CssClass = "error";
    //                txtProdVersion.Text = "";
    //            }
    //            else
    //            {
    //                pnlMsg.Visible = false;
    //            }
    //        }
    //        else if (maxPrdVer > 0)
    //        {
    //            //If Prod Version entered is less than the max Prod Version created
    //            if (Convert.ToInt32(txtProdVersion.Text) < maxPrdVer + 1)
    //            {
    //                lblMsg.Text = "Prod Version " + txtProdVersion.Text + " already exists for this material";
    //                pnlMsg.Visible = true;
    //                pnlMsg.CssClass = "error";
    //                txtProdVersion.Text = "";
    //            }
    //            //if Prod Version entered is greater the next Prod Version to be created
    //            else if ((Convert.ToInt32(txtProdVersion.Text) > maxPrdVer + 1))
    //            {
    //                lblMsg.Text = "Maximum Prod Version created for material " + txtMaterialNo.Text + " is " + maxPrdVer + ". Mention Prod Version as " + (maxPrdVer + 1);
    //                pnlMsg.Visible = true;
    //                pnlMsg.CssClass = "error";
    //                txtProdVersion.Text = "";
    //            }
    //            else
    //            {
    //                pnlMsg.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            pnlMsg.Visible = false;
    //        }
    //    //}            
    //}
    //end Commenting by Nitish Rao 26-02-2018
    //}

    protected void txtDPGroup_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //if (txtDPGroup.Text != "" && txtProdVersion.Text != "")
            if (txtDPGroup.Text != "")
            {
                ProdVersion objProdVer = objRecipeAccess.GetRecipeData(txtDPGroup.Text, txtMaterialNo.Text, Session[StaticKeys.MaterialPlantId].ToString());
                if (objProdVer.RecipeGroup != null)
                {
                    //txtProdVerDesc.Text = objProdVer.ProdVersion_Text;
                    //txtProdFrom.Text = objProdVer.ProdFrom;
                    //txtProdTo.Text = objProdVer.ProdTo;
                    txtDPGroupCntr.Text = objProdVer.GroupCntr;
                    //ddlLock.SelectedValue = objProdVer.Lock == "" ? " " : objProdVer.Lock;
                    pnlMsg.Visible = false;
                }
                else
                {
                    //txtProdVerDesc.Text = "";
                    //txtProdFrom.Text = "";
                    //txtProdTo.Text = "";
                    lblMsg.Text = "Invalid Recipe group for the given material and plant combination.";
                    pnlMsg.Visible = true;
                    pnlMsg.CssClass = "error";
                }
            }
        }
        catch (Exception ex)
        { _log.Error("txtDPGroup_TextChanged", ex); }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {

            //8200055588 enable valid from date for requestor and 
            DateTime ValidFrom = DateTime.ParseExact(txtProdVFrom.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime ValidTo = DateTime.ParseExact(txtProdVTo.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (ValidFrom > ValidTo)
            {
                lblMsg.Text = "Valid From date should be less than Valid To date.";
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
            }
            else
            {
                if (SaveProdVersion())
                {
                    FillProdVersion();
                    string pageURL = btnNext.CommandArgument.ToString();
                    Response.Redirect(pageURL);
                }
            }
        }
        catch (Exception ex)
        { _log.Error("btnNext_Click", ex); }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {

            if (SaveProdVersion())
            {
                FillProdVersion();
                string pageURL = btnPrevious.CommandArgument.ToString();
                Response.Redirect(pageURL);
            }
        }
        catch (Exception ex)
        { _log.Error("btnPrevious_Click", ex); }
    }

    public Boolean checkPrevioussection()
    {
        bool flg = false;
        try
        {

            RecipeAccess objRecipeAccess = new RecipeAccess();
            if (objRecipeAccess.CheckAllRecipeDetail(lblMasterHeaderId.Text) > 0)
            {
                flg = true;
            }
        }
        catch (Exception ex)
        { _log.Error("checkPrevioussection", ex); }
        return flg;
    }

    #endregion

    #region Methods

    private void PopuplateDropDownList()
    {
        try
        {

            helperAccess.PopuplateDropDownList(ddlDPTaskList, "pr_GetDropDownListByControlNameModuleType 'R','ddlDPTaskList'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlLock, "pr_GetDropDownListByControlNameModuleType 'R','ddlLock'", "LookUp_Desc", "LookUp_Code", "0");
            helperAccess.PopuplateDropDownList(ddlProdBOMUsage, "pr_GetDropDownListByControlNameModuleType 'B','ddlBOMUsage'", "LookUp_Desc", "LookUp_Code", "");
            helperAccess.PopuplateDropDownList(ddlRStatus, "pr_GetDropDownListByControlNameModuleType 'R','ddlRStatus'", "LookUp_Desc", "LookUp_Code", "");
        }
        catch (Exception ex)
        { _log.Error("PopuplateDropDownList", ex); }
    }

    private void FillProdVersion()
    {
        ProdVersion objProdVer = GetProdVersionData();
        try
        {
            if (objProdVer.ProdVersion_Id > 0)
            {
                lblProdVersionID.Text = objProdVer.ProdVersion_Id.ToString();
                txtMaterialNo.Text = objProdVer.MaterialNo;
                lblMatDesc.Text = objProdVer.MaterialDesc;

                txtProdVersion.Text = objProdVer.ProdVersionNo;

                txtProdVerDesc.Text = objProdVer.ProdVersion_Text;
                ddlLock.SelectedValue = objProdVer.Lock;
                txtProdFrom.Text = objProdVer.ProdFrom;
                txtProdTo.Text = objProdVer.ProdTo;
                txtProdUnit.Text = objProdVer.ProdUnit;
                txtProdVFrom.Text = objProdVer.ValidFrom;
                txtProdVTo.Text = objProdVer.ValidTo;
                ddlDPTaskList.SelectedValue = objProdVer.TaskListType;
                txtDPGroup.Text = objProdVer.RecipeGroup;
                txtDPGroupCntr.Text = objProdVer.GroupCntr;
                txtProdAltBOM.Text = objProdVer.AltBOM;
                ddlProdBOMUsage.SelectedValue = objProdVer.BOMUsage;
                Session[StaticKeys.ProdVersion] = objProdVer.ProdVersionNo;

                ddlRStatus.SelectedValue = objProdVer.RStatus;
            }
            else
            {
                lblProdVersionID.Text = "0";
                //txtMaterialNo.Text = Session[StaticKeys.BOMRecipeMatNo].ToString();
                //lblMatDesc.Text = Session[StaticKeys.BOMRecipeMatDesc].ToString();
                //txtProdUnit.Text = Session[StaticKeys.BOMRecipeBUOM].ToString();
                txtProdFrom.Text = "";
                txtProdTo.Text = "99999999";
                //if (Session[StaticKeys.SelectedModuleId].ToString() == "186")
                //{
                //    txtProdFrom.Text = Session[StaticKeys.BOMRecipeFrom].ToString();
                //    txtProdTo.Text = Session[StaticKeys.BOMRecipeTo].ToString();                    
                //}
                txtProdVerDesc.Text = Session[StaticKeys.BOMRecipeMatDesc].ToString();
                ddlDPTaskList.SelectedValue = "2";
                ddlLock.SelectedValue = "1";
                txtProdVFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtProdVTo.Text = "31/12/9999";
            }

            if (Session[StaticKeys.BOMRecipeBOMUsage] != null && Session[StaticKeys.BOMRecipeAltBOM] != null)
            {
                ddlProdBOMUsage.SelectedValue = Session[StaticKeys.BOMRecipeBOMUsage].ToString();
                txtProdAltBOM.Text = Session[StaticKeys.BOMRecipeAltBOM].ToString();
            }



            lblPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();

            txtMaterialNo.Text = Session[StaticKeys.BOMRecipeMatNo].ToString();
            lblMatDesc.Text = Session[StaticKeys.BOMRecipeMatDesc].ToString();
            txtProdUnit.Text = Session[StaticKeys.BOMRecipeBUOM].ToString();
            //to update the prod version lock status to Not Locked and QA is the logged in user Start
            //UpdateLockStatus();
            ConfigureHeaderControls();
            //to update the prod version lock status to Not Locked and QA is the logged in user End
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("FillProdVersion", ex);
        }
    }

    private void ConfigureHeaderControls()
    {
        try
        {


            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {
                ddlLock.Enabled = true;
                //txtProdVersion.Enabled = false;
                txtProdVerDesc.Enabled = false;
            }
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            //else if ((Session[StaticKeys.RecipeGroup].ToString() != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "232")||(Session[StaticKeys.SelectedModuleId].ToString() == "236")))
            //{
            //  ddlLock.Enabled = true;
            ////txtProdVersion.Enabled = false;
            //txtProdVerDesc.Enabled = false;
            //}
            //DT_26-08-2020 
            if ((txtProdVersion.Text != "") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "18") && ((Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229")))
            {
                ddlLock.Enabled = true;
                //txtProdVersion.Enabled = false;
                txtProdVerDesc.Enabled = false;
            }

        }
        catch (Exception ex)
        { _log.Error("ConfigureHeaderControls", ex); }

    }

    private void UpdateLockStatus()
    {
        try
        {


            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {
                objRecipeAccess.UpdateProdLockStatus(lblProdVersionID.Text, lblUserId.Text);
            }
        }
        catch (Exception ex)
        { _log.Error("UpdateLockStatus", ex); }
    }

    private ProdVersion GetProdVersionData()
    {
        return objRecipeAccess.GetProdVersionData(lblMasterHeaderId.Text);
    }

    private ProdVersion GetProdVerControlsValue()
    {
        ProdVersion ObjProdVersion = new ProdVersion();
        Utility objUtil = new Utility();
        try
        {

            ObjProdVersion.ProdVersion_Id = Convert.ToInt32(lblProdVersionID.Text);
            ObjProdVersion.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjProdVersion.MaterialNo = txtMaterialNo.Text;
            ObjProdVersion.MaterialDesc = lblMatDesc.Text;

            ObjProdVersion.ProdVersionNo = txtProdVersion.Text;

            ObjProdVersion.ProdVersion_Text = txtProdVerDesc.Text;
            ObjProdVersion.Lock = ddlLock.SelectedValue;
            ObjProdVersion.ProdFrom = txtProdFrom.Text;
            ObjProdVersion.ProdTo = txtProdTo.Text;
            ObjProdVersion.ProdUnit = txtProdUnit.Text;
            ObjProdVersion.ValidFrom = string.IsNullOrEmpty(txtProdVFrom.Text) ? "" : objUtil.GetYYYYMMDD(txtProdVFrom.Text);
            ObjProdVersion.ValidTo = objUtil.GetYYYYMMDD(txtProdVTo.Text);
            ObjProdVersion.TaskListType = ddlDPTaskList.SelectedValue;
            ObjProdVersion.RecipeGroup = txtDPGroup.Text;
            ObjProdVersion.GroupCntr = txtDPGroupCntr.Text;
            ObjProdVersion.AltBOM = txtProdAltBOM.Text;
            ObjProdVersion.BOMUsage = ddlProdBOMUsage.SelectedValue;

            ObjProdVersion.UserId = lblUserId.Text;
            ObjProdVersion.TodayDate = objUtil.GetDate();
            ObjProdVersion.IPAddress = objUtil.GetIpAddress();
            ObjProdVersion.Mode = lblMode.Text;
            ObjProdVersion.RStatus = ddlRStatus.SelectedValue;
        }
        catch (Exception ex)
        { _log.Error("GetProdVerControlsValue", ex); }
        return ObjProdVersion;
    }

    private bool SaveProdVersion()
    {
        bool flg = false;
        try
        {
            ProdVersion ObjProdVersion = GetProdVerControlsValue();
            //bool flg1 = ValidateProdData(ObjProdVersion);
            if (objRecipeAccess.SaveProdVersionData(ObjProdVersion) > 0)
            {
                flg = true;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("SaveProdVersion", ex);
        }
        return flg;
    }

    private void ConfigureProdVersion()
    {
        try
        {


            if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "180"))
            {
                txtMaterialNo.Enabled = false;
                lblMatDesc.Enabled = false;
                if (Session[StaticKeys.SelectedModuleId].ToString() == "186")
                {
                    txtDPGroup.Enabled = false;
                    txtDPGroupCntr.Enabled = false;
                }
                if (Session[StaticKeys.SelectedModuleId].ToString() == "180")
                {
                    //txtProdVerDesc.Enabled = false;
                    reqtxtDPGroup.Enabled = true;
                    reqtxtDPGroupCntr.Enabled = true;
                    //Start  Comment by Nitish Rao 15.02.2018 
                    //Commented for , Alternative BOM will auto generate by SAP
                    //reqtxtProdAltBOM.Enabled = true;
                    //End  Comment by Nitish Rao 15.02.2018 
                    reqddlProdBOMUsage.Enabled = true;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("ConfigureProdVersion", ex); }
    }

    #endregion

}