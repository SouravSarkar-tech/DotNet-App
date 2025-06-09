using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

public partial class Transaction_PriceMaster_PriceDetail : System.Web.UI.Page
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
                    
                    FillDataGrid();
                    

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
                //ConfigureControl();
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
        if (SavePriceDetail())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SavePriceDetail())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
    }

    

    #endregion

    #region Private Method
    
    
    private bool SavePriceDetail()
    {
        bool flag = false;
        Utility objUtil = new Utility();
        try
        {
            
            foreach (GridViewRow gr in grvData.Rows)
            {
                PriceDetail ObjPrice = new PriceDetail();

                Label lblPriceDetailId = (Label)gr.FindControl("lblPriceDetailId");
                Label lblPriceHeaderId = (Label)gr.FindControl("lblPriceHeaderId");
                Label lblPlantRegionId = (Label)gr.FindControl("lblPlantRegionId");
                Label lblRegionId = (Label)gr.FindControl("lblRegionId");

                TextBox txtTradePrice = (TextBox)gr.FindControl("txtTradePrice");

                ObjPrice.Price_Header_Id = Convert.ToInt32(lblPriceDetailId.Text);
                ObjPrice.Price_Header_Id = Convert.ToInt32(lblPriceHeaderId.Text);
                ObjPrice.Region_Id_Delivery_Plant = Convert.ToInt32(lblPlantRegionId.Text);
                ObjPrice.Region_Id = Convert.ToInt32(lblRegionId.Text);
                ObjPrice.Trade_Price = txtTradePrice.Text;
                ObjPrice.IsActive = 1;
                ObjPrice.UserId = lblUserId.Text;
                ObjPrice.TodayDate = objUtil.GetDate();
                ObjPrice.IPAddress = objUtil.GetIpAddress();

                ObjPriceHeaderAccess.Save(ObjPrice);
            }
            flag = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flag;
    }

    private void FillDataGrid()
    {
        try
        {
            DataSet ds;
            ds = ObjPriceHeaderAccess.GetPriceDetailData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblMsg.Text = "Please Fill the Price Header before filling this.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else
            {
                grvData.DataSource = ds;
                grvData.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}