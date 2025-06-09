using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;

public partial class Transaction_Customer_CreaditMngtData : System.Web.UI.Page
{
    CreditMgntDataAccess ObjCreditMgntDataAccess = new CreditMgntDataAccess();
    HelperAccess helperAccess = new HelperAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                PopuplateDropDownList();



                string sectionId = lblSectionId.Text.ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string mode = Session[StaticKeys.Mode].ToString();
                lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                FillCreditMgntData();
                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    trButton.Visible = true;
                    btnSave.Visible = !btnNext.Visible;
                }
                ConfigureControl();
            }

        }
    }


    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (SaveCreditManagementData())
        {
            string pageURL = btnPrevious.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveCreditManagementData())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("CreaditMngtData.aspx");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SaveCreditManagementData())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    

    #region private Methods

    private void PopuplateDropDownList()
    {
        //
    }

    private bool SaveCreditManagementData()
    {
        bool flg = false;
        CreaditMgntData ObjCustCrdtMgmt = GetControlsValue();

        try
        {
            if (ObjCreditMgntDataAccess.Save(ObjCustCrdtMgmt) > 0)
            {
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
            throw ex;
        }
        return flg;
    }

    private CreaditMgntData GetCreditMgntData()
    {
        return ObjCreditMgntDataAccess.GetCreditMgntData(Convert.ToInt32(lblMasterHeaderId.Text));
    }


    private CreaditMgntData GetControlsValue()
    {
        CreaditMgntData ObjCustCrdtMgmt = new CreaditMgntData();
        Utility objUtil = new Utility();

        ObjCustCrdtMgmt.CreditMgnt_Data_Id = Convert.ToInt32(lblCreaditMgntDataId.Text);
        ObjCustCrdtMgmt.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        ObjCustCrdtMgmt.Total_Amt = txtTotalAmt.Text;
        ObjCustCrdtMgmt.Individual_Limit = txtIndividualLimit.Text;
        
        ObjCustCrdtMgmt.Credit_Control = ddlCredit_Control.Text;
        ObjCustCrdtMgmt.Customer_Acc_No = txtCustomer_Acc_No.Text;
        
        ObjCustCrdtMgmt.Date_Batch = objUtil.GetMMDDYYYY(txtDate_Batch.Text);
        ObjCustCrdtMgmt.Indicator_Blocked = txtIndicator_Blocked.Text;
        ObjCustCrdtMgmt.Credit_Group = txtCredit_Group.Text;
        ObjCustCrdtMgmt.Date_Batch1 = objUtil.GetMMDDYYYY(txtDate_Batch1.Text);
        ObjCustCrdtMgmt.Credit_info_number = txtCredit_info_number.Text;
        ObjCustCrdtMgmt.Date_Batch2 = objUtil.GetMMDDYYYY(txtDate_Batch2.Text);
        ObjCustCrdtMgmt.Cust_Credit_Group = txtCust_Credit_Group.Text;
        ObjCustCrdtMgmt.Date_Batch3 = objUtil.GetMMDDYYYY(txtDate_Batch3.Text);
        ObjCustCrdtMgmt.Customer_Group = txtCustomer_Group.Text;
        ObjCustCrdtMgmt.Reco_credit_limit = txtReco_credit_limit.Text;
        ObjCustCrdtMgmt.Currency_recommend = txtCurrency_recommend.Text;
        ObjCustCrdtMgmt.Date_Batch4 = objUtil.GetMMDDYYYY(txtDate_Batch4.Text);


        ObjCustCrdtMgmt.IsActive = 1;
        ObjCustCrdtMgmt.UserId = lblUserId.Text;
        ObjCustCrdtMgmt.TodayDate = objUtil.GetDate();
        ObjCustCrdtMgmt.IPAddress = objUtil.GetIpAddress();

        return ObjCustCrdtMgmt;
    }

    private void FillCreditMgntData()
    {
        CreaditMgntData ObjCustCrdtMgmt = GetCreditMgntData();
        if (ObjCustCrdtMgmt.CreditMgnt_Data_Id > 0)
        {
            lblCreaditMgntDataId.Text = ObjCustCrdtMgmt.CreditMgnt_Data_Id.ToString();
            lblMasterHeaderId.Text = ObjCustCrdtMgmt.Master_Header_Id.ToString();

            txtTotalAmt.Text = ObjCustCrdtMgmt.Total_Amt;
            txtIndividualLimit.Text = ObjCustCrdtMgmt.Individual_Limit;
            
            ddlCredit_Control.SelectedValue = ObjCustCrdtMgmt.Credit_Control.ToString();
            txtCustomer_Acc_No.Text = ObjCustCrdtMgmt.Customer_Acc_No.ToString();
            
            txtDate_Batch.Text = ObjCustCrdtMgmt.Date_Batch.ToString();
            txtIndicator_Blocked.Text = ObjCustCrdtMgmt.Indicator_Blocked.ToString();
            txtCredit_Group.Text = ObjCustCrdtMgmt.Credit_Group.ToString();
            txtDate_Batch1.Text = ObjCustCrdtMgmt.Date_Batch1.ToString();
            txtCredit_info_number.Text = ObjCustCrdtMgmt.Credit_info_number.ToString();
            txtDate_Batch2.Text = ObjCustCrdtMgmt.Date_Batch2.ToString();
            txtCust_Credit_Group.Text = ObjCustCrdtMgmt.Cust_Credit_Group.ToString();
            txtDate_Batch3.Text = ObjCustCrdtMgmt.Date_Batch3.ToString();
            txtCustomer_Group.Text = ObjCustCrdtMgmt.Customer_Group.ToString();
            txtReco_credit_limit.Text = ObjCustCrdtMgmt.Reco_credit_limit.ToString();
            txtCurrency_recommend.Text = ObjCustCrdtMgmt.Currency_recommend.ToString();
            txtDate_Batch4.Text = ObjCustCrdtMgmt.Date_Batch4.ToString();
        }
        else
        {
            lblCreaditMgntDataId.Text = "0";
        }
    }
    private void ConfigureControl()
    {
        CustomerGeneralAccess customerGeneralAccess = new CustomerGeneralAccess();
        DataSet ds = customerGeneralAccess.ModulePlantGroupCode(Session[StaticKeys.RequestNo].ToString());
        Session[StaticKeys.SelectedModulePlantGrp] = ds.Tables[0].Rows[0]["ModulePlantGroupCode"];
        string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        SectionConfiguration.Credit_management_data obj = new SectionConfiguration.Credit_management_data();
        SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    #endregion
}