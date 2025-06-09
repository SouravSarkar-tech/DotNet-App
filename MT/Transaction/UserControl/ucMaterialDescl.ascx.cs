using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Web.Services;

public partial class Transaction_UserControl_ucMaterialDescl : System.Web.UI.UserControl
{
    BasicDataAccess matDescAccess = new BasicDataAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region PageEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                PopuplateDropDownList();
                ConfigureControls();
                FillData();
            }
        }
    }

    private void ConfigureControls()
    {
        if (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.SelectedModuleId]) == 147)
        {
            txtSupplyVolt.Enabled = false;
            ddlSupplyVolt.Enabled = false;
            txtFlameProof.Enabled = false;
            ddlFlameProof.Enabled = false;
            txtProtectionClass.Enabled = false;
            ddlProtectionClass.Enabled = false;
            txtIO.Enabled = false;


        }
        else
        {
            txtSupplyVolt.Enabled = true;
            ddlSupplyVolt.Enabled = true;
            txtFlameProof.Enabled = true;
            ddlFlameProof.Enabled = true;
            txtProtectionClass.Enabled = true;
            ddlProtectionClass.Enabled = true;
            txtIO.Enabled = true;
        }

    }

    protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSize.SelectedValue == "")
        {
            txtSize.Text = "";
            txtSize.Enabled = true;
            ddlSize.Enabled = false;
        }
        else
        {
            txtSize.Text = "";
            txtSize.Enabled = false;
        }
    }

    protected void txtSize_TextChanged(object sender, EventArgs e)
    {
        if (txtSize.Text == "")
        {
            ddlSize.SelectedValue = "";
            ddlSize.Enabled = true;
            txtSize.Enabled = false;
        }
        else
        {
            ddlSize.SelectedValue = "";
            ddlSize.Enabled = false;
        }
    }

    protected void ddlClassRatingGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClassRatingGrade.SelectedValue == "")
        {
            txtClassRatingGrade.Text = "";
            txtClassRatingGrade.Enabled = true;
            ddlClassRatingGrade.Enabled = false;
        }
        else
        {
            txtClassRatingGrade.Text = "";
            txtClassRatingGrade.Enabled = false;
        }
    }

    protected void txtClassRatingGrade_TextChanged(object sender, EventArgs e)
    {
        if (txtClassRatingGrade.Text == "")
        {
            ddlClassRatingGrade.SelectedValue = "";
            ddlClassRatingGrade.Enabled = true;
            txtClassRatingGrade.Enabled = false;
        }
        else
        {
            ddlClassRatingGrade.SelectedValue = "";
            ddlClassRatingGrade.Enabled = false;
        }
    }

    protected void ddlMFGStd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMFGStd.SelectedValue == "")
        {
            txtMFGStd.Text = "";
            txtMFGStd.Enabled = true;
            ddlMFGStd.Enabled = false;
        }
        else
        {
            txtMFGStd.Text = "";
            txtMFGStd.Enabled = false;
        }
    }

    protected void txtMFGStd_TextChanged(object sender, EventArgs e)
    {
        if (txtMFGStd.Text == "")
        {
            ddlMFGStd.SelectedValue = "";
            ddlMFGStd.Enabled = true;
            txtMFGStd.Enabled = false;
        }
        else
        {
            ddlMFGStd.SelectedValue = "";
            ddlMFGStd.Enabled = false;
        }
    }

    protected void ddlRangeCapacity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRangeCapacity.SelectedValue == "")
        {
            txtRangeCapacity.Text = "";
            txtRangeCapacity.Enabled = true;
            ddlRangeCapacity.Enabled = false;
        }
        else
        {
            txtRangeCapacity.Text = "";
            txtRangeCapacity.Enabled = false;
        }
    }

    protected void txtRangeCapacity_TextChanged(object sender, EventArgs e)
    {
        if (txtRangeCapacity.Text == "")
        {
            ddlRangeCapacity.SelectedValue = "";
            ddlRangeCapacity.Enabled = true;
            txtRangeCapacity.Enabled = false;
        }
        else
        {
            ddlRangeCapacity.SelectedValue = "";
            ddlRangeCapacity.Enabled = false;
        }
    }

    protected void ddlSupplyVolt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupplyVolt.SelectedValue == "")
        {
            txtSupplyVolt.Text = "";
            txtSupplyVolt.Enabled = true;
            ddlSupplyVolt.Enabled = false;
        }
        else
        {
            txtSupplyVolt.Text = "";
            txtSupplyVolt.Enabled = false;
        }
    }

    protected void txtSupplyVolt_TextChanged(object sender, EventArgs e)
    {
        if (txtSupplyVolt.Text == "")
        {
            ddlSupplyVolt.SelectedValue = "";
            ddlSupplyVolt.Enabled = true;
            txtSupplyVolt.Enabled = false;
        }
        else
        {
            ddlSupplyVolt.SelectedValue = "";
            ddlSupplyVolt.Enabled = false;
        }
    }

    protected void ddlFlameProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFlameProof.SelectedValue == "")
        {
            txtFlameProof.Text = "";
            txtFlameProof.Enabled = true;
            ddlFlameProof.Enabled = false;
        }
        else
        {
            txtFlameProof.Text = "";
            txtFlameProof.Enabled = false;
        }
    }

    protected void txtFlameProof_TextChanged(object sender, EventArgs e)
    {
        if (txtFlameProof.Text == "")
        {
            ddlFlameProof.SelectedValue = "";
            ddlFlameProof.Enabled = true;
            txtFlameProof.Enabled = false;
        }
        else
        {
            ddlFlameProof.SelectedValue = "";
            ddlFlameProof.Enabled = false;
        }
    }

    protected void ddlProcessConSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProcessConSize.SelectedValue == "")
        {
            txtProcessConSize.Text = "";
            txtProcessConSize.Enabled = true;
            ddlProcessConSize.Enabled = false;
        }
        else
        {
            txtProcessConSize.Text = "";
            txtProcessConSize.Enabled = false;
        }

    }

    protected void txtProcessConSize_TextChanged(object sender, EventArgs e)
    {
        if (txtProcessConSize.Text == "")
        {
            ddlProcessConSize.SelectedValue = "";
            ddlProcessConSize.Enabled = true;
            txtProcessConSize.Enabled = false;
        }
        else
        {
            ddlProcessConSize.SelectedValue = "";
            ddlProcessConSize.Enabled = false;
        }

    }

    protected void ddlProtectionClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProtectionClass.SelectedValue == "")
        {
            txtProtectionClass.Text = "";
            txtProtectionClass.Enabled = true;
            ddlProtectionClass.Enabled = false;
        }
        else
        {
            txtProtectionClass.Text = "";
            txtProtectionClass.Enabled = false;
        }
    }

    protected void txtProtectionClass_TextChanged(object sender, EventArgs e)
    {
        if (txtProtectionClass.Text == "")
        {
            ddlProtectionClass.SelectedValue = "";
            ddlProtectionClass.Enabled = true;
            txtProtectionClass.Enabled = false;
        }
        else
        {
            ddlProtectionClass.SelectedValue = "";
            ddlProtectionClass.Enabled = false;
        }
    }

    #endregion

    #region Methods

    private void FillData()
    {
        MaterialDesc objMatDesc = matDescAccess.GetMatDescByMasterHeaderId(lblMasterHeaderId.Text);
        if (objMatDesc.Mat_Material_Desc_Id > 0)
        {
            lblMatDescId.Text = Convert.ToString(objMatDesc.Mat_Material_Desc_Id);
            txtItemDesc.Text = objMatDesc.Item_Description;
            txtItemType.Text = objMatDesc.Item_Type;
            ddlMOC.SelectedValue = objMatDesc.MOC;

            if (((ListItem)(ddlSize.Items.FindByValue(objMatDesc.Size.Replace(@"\", "")))) != null)
                ddlSize.SelectedValue = objMatDesc.Size;
            else
                txtSize.Text = objMatDesc.Size;
            
            if (((ListItem)(ddlClassRatingGrade.Items.FindByValue(objMatDesc.Class_Rating_Grade.Replace(@"\", "")))) != null)
                ddlClassRatingGrade.SelectedValue = objMatDesc.Class_Rating_Grade;
            else
                txtClassRatingGrade.Text = objMatDesc.Class_Rating_Grade;

            if (((ListItem)(ddlMFGStd.Items.FindByValue(objMatDesc.MFG_Std.Replace(@"\", "")))) != null)
                ddlMFGStd.SelectedValue = objMatDesc.MFG_Std;
            else
                txtMFGStd.Text = objMatDesc.MFG_Std;

            if(((ListItem)(ddlRangeCapacity.Items.FindByValue(objMatDesc.Range_Capacity.Replace(@"\", "")))) != null)
                ddlRangeCapacity.SelectedValue = objMatDesc.Range_Capacity;
            else
                txtRangeCapacity.Text = objMatDesc.Range_Capacity;

            if (((ListItem)(ddlSupplyVolt.Items.FindByValue(objMatDesc.Supply_Voltage.Replace(@"\", "")))) != null)
                ddlSupplyVolt.SelectedValue = objMatDesc.Supply_Voltage;
            else
                txtSupplyVolt.Text = objMatDesc.Supply_Voltage;
                        
            if (((ListItem)(ddlFlameProof.Items.FindByValue(objMatDesc.Flame_Proof.Replace(@"\", "")))) != null)
                ddlFlameProof.SelectedValue = objMatDesc.Flame_Proof;
            else
                txtFlameProof.Text = objMatDesc.Flame_Proof;

            if (((ListItem)(ddlProtectionClass.Items.FindByValue(objMatDesc.Protection_Class.Replace(@"\", "")))) != null)
                ddlProtectionClass.SelectedValue = objMatDesc.Protection_Class;
            else
                txtProtectionClass.Text = objMatDesc.Protection_Class;

            if (((ListItem)(ddlProcessConSize.Items.FindByValue(objMatDesc.Process_Connection_Size.Replace(@"\", "")))) != null)
                ddlProcessConSize.SelectedValue = objMatDesc.Process_Connection_Size;
            else
                txtProcessConSize.Text = objMatDesc.Process_Connection_Size;
                        
            txtAvgLeastCnt.Text = objMatDesc.Accuracy_LeastCount;
            txtIO.Text = objMatDesc.Input_Output;
            txtManufacturerPartNo.Text = objMatDesc.Manufacturer_PartNo;
            txtMakeMachModelNo.Text = objMatDesc.Make_MachName_ModelNo;

            txtMatDesc.Text = objMatDesc.Material_Desc;
            hdnMatDesc.Value = objMatDesc.Material_Desc;
            txtCharacters.Text = objMatDesc.Material_Desc.Length.ToString();
        }
        else
        {
            lblMatDescId.Text = "0";
        }
    }

    public string Save()
    {
        string materialDesc = "";
        MaterialDesc objMatDesc = new MaterialDesc();

        try
        {
            //string desc = strUserName;

            objMatDesc = GetMatDescControlData();
            if (matDescAccess.SaveMatDesc(objMatDesc) > 0)
            {
                materialDesc = objMatDesc.Material_Desc;
                FillData();
            }
        }
        catch (Exception ex)
        {
        }

        return materialDesc;
    }

    private MaterialDesc GetMatDescControlData()
    {

        MaterialDesc objMaterialDesc = new MaterialDesc();
        Utility util = new Utility();
        
        try
        {
            objMaterialDesc.Mat_Material_Desc_Id = Convert.ToInt32(lblMatDescId.Text);
            objMaterialDesc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            objMaterialDesc.Item_Description = txtItemDesc.Text;
            objMaterialDesc.Item_Type = txtItemType.Text;
            objMaterialDesc.MOC = ddlMOC.SelectedValue;
            objMaterialDesc.Manufacturer_PartNo = txtManufacturerPartNo.Text;
            objMaterialDesc.Input_Output = txtIO.Text;
            objMaterialDesc.Accuracy_LeastCount = txtAvgLeastCnt.Text;
            objMaterialDesc.Material_Desc = hdnMatDesc.Value;

            if(ddlProcessConSize.SelectedValue != "")
                objMaterialDesc.Process_Connection_Size = ddlProcessConSize.SelectedValue;
            else
                objMaterialDesc.Process_Connection_Size = txtProcessConSize.Text;
            
            if (ddlSize.SelectedValue != "")
                objMaterialDesc.Size = ddlSize.SelectedValue;
            else
                objMaterialDesc.Size = txtSize.Text;

            if (ddlProtectionClass.SelectedValue != "")
                objMaterialDesc.Protection_Class = ddlProtectionClass.SelectedValue;
            else
                objMaterialDesc.Protection_Class = txtProtectionClass.Text;

            if (ddlSupplyVolt.SelectedValue != "")
                objMaterialDesc.Supply_Voltage = ddlSupplyVolt.SelectedValue;
            else
                objMaterialDesc.Supply_Voltage = txtSupplyVolt.Text;

            if (ddlRangeCapacity.SelectedValue != "")
                objMaterialDesc.Range_Capacity = ddlRangeCapacity.SelectedValue;
            else 
                objMaterialDesc.Range_Capacity = txtRangeCapacity.Text;

            if (ddlMFGStd.SelectedValue != "")
                objMaterialDesc.MFG_Std = ddlMFGStd.SelectedValue;
            else
                objMaterialDesc.MFG_Std = txtMFGStd.Text;            

            if (ddlFlameProof.SelectedValue != "")
                objMaterialDesc.Flame_Proof = ddlFlameProof.SelectedValue;
            else
                objMaterialDesc.Flame_Proof = txtFlameProof.Text;
            objMaterialDesc.Make_MachName_ModelNo = txtMakeMachModelNo.Text;

            if (ddlClassRatingGrade.SelectedValue != "")
                objMaterialDesc.Class_Rating_Grade = ddlClassRatingGrade.SelectedValue;
            else
                objMaterialDesc.Class_Rating_Grade = txtClassRatingGrade.Text;
           
            objMaterialDesc.UserId = lblUserId.Text;
            objMaterialDesc.TodayDate = util.GetDate();
            objMaterialDesc.IPAddress = util.GetIpAddress();
            objMaterialDesc.IsActive = 1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return objMaterialDesc;
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownList(ddlMOC, "pr_GetDropDownListByControlNameMOCSize 'M','ddlMOC',3", "LookUp_Desc", "LookUp_Code","");
        helperAccess.PopuplateDropDownList(ddlSize, "pr_GetDropDownListByControlNameMOCSize 'M','ddlSize',3", "LookUp_Desc", "LookUp_Code","");

        helperAccess.PopuplateDropDownList(ddlClassRatingGrade, "pr_GetDropDownListByControlNameMOCSize 'M','ddlClassRatingGrade',3", "LookUp_Desc", "LookUp_Code","");
        helperAccess.PopuplateDropDownList(ddlFlameProof, "pr_GetDropDownListByControlNameMOCSize 'M','ddlFlameProof',3", "LookUp_Desc", "LookUp_Code","");
        helperAccess.PopuplateDropDownList(ddlMFGStd, "pr_GetDropDownListByControlNameMOCSize 'M','ddlMFGStd',3", "LookUp_Desc", "LookUp_Code","");
        helperAccess.PopuplateDropDownList(ddlSupplyVolt, "pr_GetDropDownListByControlNameMOCSize 'M','ddlSupplyVolt',3", "LookUp_Desc", "LookUp_Code","");
        helperAccess.PopuplateDropDownList(ddlRangeCapacity, "pr_GetDropDownListByControlNameMOCSize 'M','ddlRangeCapacity',3", "LookUp_Desc", "LookUp_Code","");

        helperAccess.PopuplateDropDownList(ddlProcessConSize, "pr_GetDropDownListByControlNameMOCSize 'M','ddlProcessConSize',3", "LookUp_Desc", "LookUp_Code","");
        helperAccess.PopuplateDropDownList(ddlProtectionClass, "pr_GetDropDownListByControlNameMOCSize 'M','ddlProtectionClass',3", "LookUp_Desc", "LookUp_Code","");
    }

    #endregion
        
}