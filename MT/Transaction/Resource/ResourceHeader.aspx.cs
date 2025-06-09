using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Transactions;

public partial class Transaction_Resource_ResourceHeader : BasePage
{
    ResourceMasterAccess ObjResourceMasterAccess = new ResourceMasterAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.MasterHeaderId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    PopuplateDropDownList();
                    ClearData();
                    FillResourceMasterData();
                    ConfigureControl();


                    string pageSequence = Request.QueryString["pgseq"].ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    HelperAccess.SetNextPreviousSectionURL(pageSequence, moduleId, deptId, btnPrevious, btnNext);

                    string sectionId = Request.QueryString["sid"].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        trButton.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("ResourceMaster.aspx");
                }
            }
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string pageURL = btnPrevious.CommandArgument.ToString();
        Response.Redirect(pageURL);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveResourceMaster())
        {

            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            SubmitResourceMaster();
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SaveResourceMaster())
        {
            SubmitResourceMaster();
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    #endregion

    #region Method

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplatePlantList(ddlPlant, lblMasterHeaderId.Text, "RM", lblResourceMasterId.Text);
    }

    private void ClearData()
    {
        try
        {
            lblResourceMasterId.Text = "0";

            txtResource.Text = "";
            txtObjectName.Text = "";
            txtPersonRespWorkCenter.Text = "";
            txtStandardValueKey.Text = "";
            txtUnitOfMeasureStdValue.Text = "";
            txtUnitOfMeasureStdValue2.Text = "";
            txtFormulaCapReqIntProcess.Text = "";
            txtCapacityShortText.Text = "";
            txtCapacityPlannerGroup.Text = "";
            txtStartTime.Text = "";
            txtFinishTime.Text = "";
            txtCapacityUtilizationRate.Text = "";
            txtCumulativeLenBreakPerShift.Text = "";
            txtNumberOfIndCap.Text = "";
            txtBaseUOMCapacity.Text = "";
            txtFormulaDurationIntProcess.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtCostCenter.Text = "";
            txtActivityType.Text = "";
            txtActivityType2.Text = "";
            txtActivityUnit.Text = "";
            txtActivityUnit2.Text = "";
            txtFormulaKey.Text = "";
            txtFormulaKey2.Text = "";
            ClearSelectedValue(ddlPlant);
            PopuplateDropDownList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SaveResourceMaster()
    {
        bool flg = false;
        try
        {
            ResourceMaster ObjRes = GetControlsValue();
            if (ObjRes.Plant_Id != null)
            {
                if (ObjResourceMasterAccess.Save(ObjRes) > 0)
                {
                    ClearData();
                    flg = true;
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
            throw ex;
        }
        return flg;
    }

    public void SubmitResourceMaster()
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                {
                    if (materialMasterAccess.SaveMaterialHeader(lblMasterHeaderId.Text, moduleId, lblUserId.Text, "M") > 0)
                    {
                        flg = true;
                        scope.Complete();
                    }
                }
            }
            if (flg)
            {
                Response.Redirect("ResourceMaster.aspx?pg=20", false);
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
            throw ex;
        }
    }

    private ResourceMaster GetResourceMasterData()
    {
        return ObjResourceMasterAccess.GetResourceMaster(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private ResourceMaster GetControlsValue()
    {
        ResourceMaster ObjRes = new ResourceMaster();
        Utility objUtil = new Utility();

        try
        {
            ObjRes.Resource_Master_Id = Convert.ToInt32(lblResourceMasterId.Text);
            ObjRes.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjRes.Plant_Id = GetSelectedCheckedValue(ddlPlant);
            ObjRes.Resource = txtResource.Text;
            ObjRes.Object_Name = txtObjectName.Text;
            ObjRes.Person_Resp_WorkCenter = txtPersonRespWorkCenter.Text;
            ObjRes.Standard_Value_Key = txtStandardValueKey.Text;
            ObjRes.Unit_Of_Measure_Std_Value = txtUnitOfMeasureStdValue.Text;
            ObjRes.Unit_Of_Measure_Std_Value2 = txtUnitOfMeasureStdValue2.Text;
            ObjRes.Formula_Cap_Req_Int_Process = txtFormulaCapReqIntProcess.Text;
            ObjRes.Capacity_Short_Text = txtCapacityShortText.Text;
            ObjRes.Capacity_Planner_Group = txtCapacityPlannerGroup.Text;
            ObjRes.Start_Time = txtStartTime.Text;
            ObjRes.Finish_Time = txtFinishTime.Text;
            ObjRes.Capacity_Utilization_Rate = txtCapacityUtilizationRate.Text != "" ? Convert.ToInt32(txtCapacityUtilizationRate.Text) : 0;
            ObjRes.Cumulative_Len_Break_Per_Shift = txtCumulativeLenBreakPerShift.Text;
            ObjRes.Number_Of_Ind_Cap = txtNumberOfIndCap.Text;
            ObjRes.Base_UOM_Capacity = txtBaseUOMCapacity.Text;
            ObjRes.Formula_Duration_Int_Process = txtFormulaDurationIntProcess.Text;
            ObjRes.Start_Date = txtStartDate.Text;
            ObjRes.End_Date = txtEndDate.Text;
            ObjRes.Cost_Center = txtCostCenter.Text;
            ObjRes.Activity_Type = txtActivityType.Text;
            ObjRes.Activity_Type2 = txtActivityType2.Text;
            ObjRes.Activity_Unit = txtActivityUnit.Text;
            ObjRes.Activity_Unit2 = txtActivityUnit2.Text;
            ObjRes.Formula_Key = txtFormulaKey.Text;
            ObjRes.Formula_Key2 = txtFormulaKey2.Text;
            ObjRes.IsActive = 1;
            ObjRes.UserId = lblUserId.Text;
            ObjRes.TodayDate = objUtil.GetDate();
            ObjRes.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjRes;
    }

    private void FillResourceMasterData()
    {
        try
        {
            ResourceMaster ObjRes = GetResourceMasterData();
            if (ObjRes.Resource_Master_Id > 0)
            {
                lblResourceMasterId.Text = ObjRes.Resource_Master_Id.ToString();
                PopuplateDropDownList();
                SetSelectedValue(ddlPlant, ObjRes.Plant_Id);
                txtResource.Text = ObjRes.Resource;
                txtObjectName.Text = ObjRes.Object_Name;
                txtPersonRespWorkCenter.Text = ObjRes.Person_Resp_WorkCenter;
                txtStandardValueKey.Text = ObjRes.Standard_Value_Key;
                txtUnitOfMeasureStdValue.Text = ObjRes.Unit_Of_Measure_Std_Value;
                txtUnitOfMeasureStdValue2.Text = ObjRes.Unit_Of_Measure_Std_Value2;
                txtFormulaCapReqIntProcess.Text = ObjRes.Formula_Cap_Req_Int_Process;
                txtCapacityShortText.Text = ObjRes.Capacity_Short_Text;
                txtCapacityPlannerGroup.Text = ObjRes.Capacity_Planner_Group;
                txtStartTime.Text = ObjRes.Start_Time;
                txtFinishTime.Text = ObjRes.Finish_Time;
                txtCapacityUtilizationRate.Text = ObjRes.Capacity_Utilization_Rate.ToString();
                txtCumulativeLenBreakPerShift.Text = ObjRes.Cumulative_Len_Break_Per_Shift;
                txtNumberOfIndCap.Text = ObjRes.Number_Of_Ind_Cap;
                txtBaseUOMCapacity.Text = ObjRes.Base_UOM_Capacity;
                txtFormulaDurationIntProcess.Text = ObjRes.Formula_Duration_Int_Process;
                txtStartDate.Text = ObjRes.Start_Date;
                txtEndDate.Text = ObjRes.End_Date;
                txtCostCenter.Text = ObjRes.Cost_Center;
                txtActivityType.Text = ObjRes.Activity_Type;
                txtActivityType2.Text = ObjRes.Activity_Type2;
                txtActivityUnit.Text = ObjRes.Activity_Unit;
                txtActivityUnit2.Text = ObjRes.Activity_Unit2;
                txtFormulaKey.Text = ObjRes.Formula_Key;
                txtFormulaKey2.Text = ObjRes.Formula_Key2;
            }
            else
            {
                lblResourceMasterId.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ConfigureControl()
    {
        //string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        //SectionConfiguration.ResourceMaster obj = new SectionConfiguration.ResourceMaster();
        //SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }
    #endregion
}