using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

public partial class Transaction_BankMaster_BankData : System.Web.UI.Page
{
    MaterialMasterAccess objAccess = new MaterialMasterAccess();
    HelperAccess helperAccess = new HelperAccess();
    
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

                    ConfigureControl(); 

                    //string pageSequence = Request.QueryString["pgseq"].ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    //string sectionId = Request.QueryString["sid"].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, deptId, moduleId);
                    if (mode == "M" || mode == "N")
                    {
                        trButton.Visible = true;
                        btnSubmit.Visible = true;
                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, deptId, lblUserId.Text);
                    }
                    else
                    {
                        btnRejectTo.Visible = false;
                    }
                    
                }
                else
                {
                    Response.Redirect("BankMaster.aspx");
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess ObjMaterialMasterAccess = new MaterialMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {

                ucBankMaster1.Save();
                if (Session[StaticKeys.ActionType].ToString() != "C" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                {
                    btnSAPUpload.Visible = true;
                }
                else
                {
                    if (ObjMaterialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                    {
                        flg = true;
                    }
                }
                
                scope.Complete();
            }
            if (flg)
            {
                Response.Redirect("../BankMaster/BankMaster.aspx");
            }
            else
            {
                if (!btnSAPUpload.Visible)
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnRollback_Click(object sender, EventArgs e)
    {
        if (RollbackRequest())
        {
            Response.Redirect("../BankMaster/BankMaster.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../BankMaster/BankMaster.aspx");
    }
    
    #endregion

    #region Private Methods

    private bool RollbackRequest()
    {
        bool flg = false;
        try
        {
            MaterialMasterAccess objAccess = new MaterialMasterAccess();

            if (objAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), ddlRejectTo.SelectedValue, Utility.RemoveSpecialChar(txtRejectNote.Text), lblUserId.Text) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-2);
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

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {
            ddlRejectTo.DataSource = materialMasterAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ConfigureControl()
    {
        //string str = Session[StaticKeys.SelectedModulePlantGrp].ToString();
        //SectionConfiguration.Vendor_General obj = new SectionConfiguration.Vendor_General();
        //SectionConfiguration.FieldStatus.SetFieldStatus(pnlData, obj.GetClass(str));
    }

    #endregion
}