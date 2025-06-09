using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using Accenture.MWT.DomainObject;

public partial class Transaction_PriceMaster_PriceHeader : System.Web.UI.Page
{
    PriceMasterAccess ObjPriceHeaderAccess = new PriceMasterAccess();

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
                    FillPriceHeaderData();
                    

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
                    Response.Redirect("PriceMaster.aspx");
                }
                ConfigureControl();
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
        if (SavePriceHeader())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SavePriceHeader())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    #endregion

    #region Method

    private void PopuplateDropDownList()
    {
        HelperAccess helperAccess = new HelperAccess();
        helperAccess.PopuplateDropDownList(ddlMaterial, "pr_GetMaterialList " + lblMasterHeaderId.Text + ",'',0", "Material_Name", "Material_Code");
        helperAccess.PopuplateDropDownList(ddlBatch, "pr_GetBatchList " + lblMasterHeaderId.Text + ",'',0", "Batch_Name", "Batch_Id");
        helperAccess.PopuplateDropDownList(ddlCustomer, "pr_GetCustomerList " + lblMasterHeaderId.Text + ",'',0", "Customer_Name", "Customer_Code");
        helperAccess.PopuplateDropDownList(ddlVendor, "pr_GetVendorList " + lblMasterHeaderId.Text + ",'',0", "Vendor_Name", "Vendor_Code");
        helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList " + lblMasterHeaderId.Text + ",'ALL',0", "Plant_Name", "Plant_Id");
    }

    private void ClearData()
    {
        try
        {
            
            lblPriceHeaderId.Text = "0";
            ddlMaterial.SelectedValue = "0";
            ddlBatch.SelectedValue = "0";
            txtValidityDateFrom.Text = "";
            ddlCustomer.SelectedValue = "0";
            ddlVendor.SelectedValue = "0";
            ddlPlant.SelectedValue = "0";
            txtPriceGroup.Text = "";
            txtProcessingStatus.Text = "";
            txtBaseUnitofMeasure.Text = "";
            txtTradePrice.Text = "";
            txtExciseDuty.Text = "";
            txtEducationCess.Text = "";
            txtSecHighEduCess.Text = "";
            txtMRP.Text = "";
            txtRateUnit.Text = "";
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SavePriceHeader()
    {
        bool flg = true;
        try
        {
            PriceHeader ObjPrice = GetControlsValue();
            if (ObjPriceHeaderAccess.Save(ObjPrice) > 0)
            {
                FillPriceHeaderData();
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
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private PriceHeader GetPriceHeaderData()
    {
        return ObjPriceHeaderAccess.GetPriceHeader(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private PriceHeader GetControlsValue()
    {
        PriceHeader ObjPrice = new PriceHeader();
        Utility objUtil = new Utility();

        try
        {
            ObjPrice.Price_Header_Id = Convert.ToInt32(lblPriceHeaderId.Text);
            ObjPrice.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjPrice.Material_Code = ddlMaterial.SelectedValue.ToString();
            ObjPrice.Batch_Id = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
            ObjPrice.Validity_Date_From = objUtil.GetYYYYMMDD(txtValidityDateFrom.Text);
            ObjPrice.Customer_Code = ddlCustomer.SelectedValue;
            ObjPrice.Vendor_Code = ddlVendor.SelectedValue;
            ObjPrice.Plant_Id = Convert.ToInt32(ddlPlant.SelectedValue);
            ObjPrice.Price_Group = txtPriceGroup.Text;
            ObjPrice.Processing_Status = txtProcessingStatus.Text;
            ObjPrice.Base_Unit_of_Measure = txtBaseUnitofMeasure.Text;
            ObjPrice.Trade_Price = txtTradePrice.Text;
            ObjPrice.Excise_Duty = txtExciseDuty.Text;
            ObjPrice.Education_Cess = txtEducationCess.Text;
            ObjPrice.Sec_High_Edu_Cess = txtSecHighEduCess.Text;
            ObjPrice.MRP = txtMRP.Text;
            ObjPrice.Rate_Unit = txtRateUnit.Text;
            ObjPrice.IsActive = 1;
            ObjPrice.UserId = lblUserId.Text;
            ObjPrice.TodayDate = objUtil.GetDate();
            ObjPrice.IPAddress = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjPrice;
    }

    private void FillPriceHeaderData()
    {
        try
        {
            PriceHeader ObjPrice = GetPriceHeaderData();
            if (ObjPrice.Price_Header_Id > 0)
            {
                lblPriceHeaderId.Text = ObjPrice.Price_Header_Id.ToString();
                ddlMaterial.SelectedValue = ObjPrice.Material_Code.ToString();
                ddlBatch.SelectedValue = ObjPrice.Batch_Id.ToString();
                txtValidityDateFrom.Text = ObjPrice.Validity_Date_From;
                ddlCustomer.SelectedValue = ObjPrice.Customer_Code;
                ddlVendor.SelectedValue = ObjPrice.Vendor_Code;
                ddlPlant.SelectedValue = ObjPrice.Plant_Id.ToString();
                txtPriceGroup.Text = ObjPrice.Price_Group;
                txtProcessingStatus.Text = ObjPrice.Processing_Status;
                txtBaseUnitofMeasure.Text = ObjPrice.Base_Unit_of_Measure;
                txtTradePrice.Text = ObjPrice.Trade_Price;
                txtExciseDuty.Text = ObjPrice.Excise_Duty;
                txtEducationCess.Text = ObjPrice.Education_Cess;
                txtSecHighEduCess.Text = ObjPrice.Sec_High_Edu_Cess;
                txtMRP.Text = ObjPrice.MRP;
                txtRateUnit.Text = ObjPrice.Rate_Unit;
            }
            else
            {
                lblPriceHeaderId.Text = "0";
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
        //SectionConfiguration.PriceHeader obj = new SectionConfiguration.PriceHeader();
        //SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }
    #endregion
}