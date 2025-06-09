using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;

public partial class Transaction_UserControl_ucBankMaster : System.Web.UI.UserControl
{
    BankMasterAccess ObjBankMasterAccess = new BankMasterAccess();
    HelperAccess helperAccess = new HelperAccess();

    public string RefMhId
    {
        set { lblRefMasterHeaderId.Text = value; }
    }

    public string BankMhId
    {
        get { return lblMasterHeaderId.Text; }
    }
    
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                if (Session[StaticKeys.SelectedModuleId].ToString() == "60")
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    PopuplateDropDownList();
                    FillData();
                }
                else
                {
                    lblMasterHeaderId.Text = "0";
                }
            }
        }
    }

    protected void ddlBankCountrykeyBNK_SelectedIndexChanged(object sender, EventArgs e)
    {
        //helperAccess.PopuplateComboBox(ddlRegionBNK, "pr_Get_RegionListByCountry " + ddlBankCountrykeyBNK.SelectedValue, "Region_Name", "Region_Id");
        helperAccess.PopuplateDropDownList(ddlRegionBNK, "pr_Get_RegionListByCountry " + ddlBankCountrykeyBNK.SelectedValue, "Region_Name", "Region_Id");
    }

    protected void txtBankKeyBNK_TextChanged(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        DataSet ds;
        if (txtBankKeyBNK.Text != "")
            ds = ObjBankMasterAccess.GetBankMasterListByBankKey(txtBankKeyBNK.Text);
        else
            ds = null;

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "Same Bank Key Already Exists.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }

    }

    #endregion


    #region Methods

    public void SetStartData()
    {
        PopuplateDropDownList();
        txtBankKeyBNK.Enabled = false;
        labletxtBankkeyBNK.Visible = false;
        reqtxtBankKey.Enabled = false;
        //FillData();
        txtBankKeyBNK.Text = "";
        txtBankNameBNK.Text = "";
        txtBankBranchBNK.Text = "";
        txtHouseNoStreetBNK.Text = "";
        txtCityBNK.Text = "";
        txtSwiftBNK.Text = "";
        txtBankNumberBNK.Text = "";
        txtBankGroupBNK.Text = "";
    }

    protected void FillData()
    {

        BankMaster ObjBankMaster = ObjBankMasterAccess.GetBankMasterByMasterHeaderId(lblMasterHeaderId.Text);
        if (ObjBankMaster != null)
        {
            lblBankId.Text = ObjBankMaster.Bank_Id.ToString();
            ddlBankCountrykeyBNK.SelectedValue = ObjBankMaster.Country_Id.ToString();
            //helperAccess.PopuplateComboBox(ddlRegionBNK, "pr_Get_RegionListByCountry " + ddlBankCountrykeyBNK.SelectedValue, "Region_Name", "Region_Id");
            helperAccess.PopuplateDropDownList(ddlRegionBNK, "pr_Get_RegionListByCountry " + ddlBankCountrykeyBNK.SelectedValue, "Region_Name", "Region_Id");
            ddlRegionBNK.SelectedValue = ObjBankMaster.Region_Id.ToString();
            txtBankKeyBNK.Text = ObjBankMaster.Bank_Key;
            txtBankNameBNK.Text = ObjBankMaster.Bank_Name;
            txtBankBranchBNK.Text = ObjBankMaster.Bank_Branch;
            txtHouseNoStreetBNK.Text = ObjBankMaster.House_No_Street;
            txtCityBNK.Text = ObjBankMaster.City;
            txtSwiftBNK.Text = ObjBankMaster.Swift;
            txtBankNumberBNK.Text = ObjBankMaster.Bank_Number;
            txtBankGroupBNK.Text = ObjBankMaster.Bank_Group;
            lblRefMasterHeaderId.Text = ObjBankMaster.Ref_Master_Header_Id.ToString();
        }

    }
    
    protected void PopuplateDropDownList()
    {
        //helperAccess.PopuplateComboBox(ddlBankCountrykeyBNK, "pr_Get_CountryList", "Country_Name", "Country_Id");
        //helperAccess.PopuplateComboBox(ddlRegionBNK, "pr_Get_RegionListByCountry 0", "Region_Name", "Region_Id");
        helperAccess.PopuplateDropDownList(ddlBankCountrykeyBNK, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlRegionBNK, "pr_Get_RegionListByCountry 0", "Region_Name", "Region_Id");
    }

    public void ClearData()
    {
        ddlBankCountrykeyBNK.SelectedValue = "0";
        ddlRegionBNK.SelectedValue = "0";
        txtBankKeyBNK.Text = "";
        txtBankNameBNK.Text = "";
        txtBankBranchBNK.Text = "";
        txtHouseNoStreetBNK.Text = "";
        txtCityBNK.Text = "";
        txtSwiftBNK.Text = "";
        txtBankNumberBNK.Text = "";
        txtBankGroupBNK.Text = "";

    }

    public BankMaster GetControlValue()
    {
        BankMaster ObjBankMaster = new BankMaster();
        Utility objUtil = new Utility();

        try
        {
            ObjBankMaster.Bank_Id = Convert.ToInt32(lblBankId.Text == "" ? "0" : lblBankId.Text);
            ObjBankMaster.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text == "" ? "0" : lblMasterHeaderId.Text);
            ObjBankMaster.Country_Id = Convert.ToInt32(ddlBankCountrykeyBNK.SelectedValue);
            ObjBankMaster.Region_Id = Convert.ToInt32(ddlRegionBNK.SelectedValue);
            ObjBankMaster.Bank_Key = txtBankKeyBNK.Text;
            ObjBankMaster.Bank_Name = txtBankNameBNK.Text;
            ObjBankMaster.Bank_Branch = txtBankBranchBNK.Text;
            ObjBankMaster.House_No_Street = txtHouseNoStreetBNK.Text;
            ObjBankMaster.City = txtCityBNK.Text;
            ObjBankMaster.Swift = txtSwiftBNK.Text;
            ObjBankMaster.Bank_Number = txtBankNumberBNK.Text;
            ObjBankMaster.Bank_Group = txtBankGroupBNK.Text;
            ObjBankMaster.Ref_Master_Header_Id = Convert.ToInt32(lblRefMasterHeaderId.Text == "" ? "0" : lblRefMasterHeaderId.Text);

            ObjBankMaster.IsActive = 1;
            ObjBankMaster.UserId = lblUserId.Text;
            ObjBankMaster.TodayDate = objUtil.GetDate();
            ObjBankMaster.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjBankMaster;
    }

    protected void SaveMasterHeader()
    {
        MaterialMasterAccess materialAccess = new MaterialMasterAccess();
        int masterHeaderId;
        try
        {
            masterHeaderId = materialAccess.SaveMaterialHeader("0", lblModuleId.Text, lblUserId.Text, "N");
            if (masterHeaderId > 0)
            {
                lblMasterHeaderId.Text = masterHeaderId.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ApproveRequest()
    {
        MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
        ObjMaterialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text);
    }

    public bool Save()
    {
        bool flag = false;
        DataSet ds = ObjBankMasterAccess.CheckBankExists(txtBankKeyBNK.Text, txtSwiftBNK.Text,lblMasterHeaderId.Text);

        if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
        {
            if (lblMasterHeaderId.Text == "" || lblMasterHeaderId.Text == "0")
            {
                SaveMasterHeader();
            }

            if (ObjBankMasterAccess.Save(GetControlValue()) > 0)
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
                flag = true;
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
            if (ds.Tables[0].Rows.Count != 0)
            {
                lblMsg.Text = "Bank with this IFSC Code Already Exists";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else if (ds.Tables[1].Rows.Count != 0)
            {
                lblMsg.Text = "Bank with this Bank Key Already Exists";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        return flag;
    }

    #endregion


    
}