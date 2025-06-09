using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using System.Data;
using System.Drawing.Design;
using System.Data.SqlClient;
using System.Collections;
using log4net;
public partial class Administration_ApprovFBUtility : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    pnlAddNew.Visible = true;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            Approve ObjApprove = GetControlsValue();

            int flag;
            flag = userAccess.SaveFB(ObjApprove);
            if (flag == 1)
            {
                ClearData();
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

            }
            else if (flag == -2)
            {
                lblMsg.Text = "This request has already been approved by the selected department";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else if (flag == -3)
            {
                lblMsg.Text = "Approver is not mapped in the system for this department. Kindly contact admin team";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else if (flag == -4)
            {
                lblMsg.Text = "Kindly approve previous request in the Workflow sequence first";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }

        }
        catch (Exception ex)
        { _log.Error("btnApprove_Click", ex); }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    private void ClearData()
    {
        try
        {
            txtRequestNo.Text = "";
            ddlDepartment.SelectedIndex = 0;
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            //ddlApproveBy.SelectedIndex = 0;
            ddlRemarks.SelectedIndex = 0;
            txtRemarks.Text = "";
            pnlMsg.Visible = false;
            ddlApproveBy.SelectedIndex = 0;
            ddlApproveBy.Items.Clear();
            ddlApproveBy.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        { _log.Error("BtnClear_Click", ex); }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetUserByDepartment(txtRequestNo.Text.Trim(), ddlDepartment.SelectedValue);
        }
        catch (Exception ex)
        { _log.Error("ddlDepartment_SelectedIndexChanged", ex); }
    }

    private void GetUserByDepartment(string pRequest_No, string pDepartment)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        Hashtable hashPara = new Hashtable();
        string procName = "sp_GetUserByDepartment_FB";
        hashPara.Add("@Request_No", pRequest_No);
        hashPara.Add("@pDepartment", pDepartment);
        try
        {
            //DNRCOMM//objDal.OpenConnection();
            DataSet ds = new DataSet();
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            ddlApproveBy.DataSource = ds;
            ddlApproveBy.DataTextField = "User_Full_Name";
            ddlApproveBy.DataValueField = "User_Id";
            ddlApproveBy.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("GetUserByDepartment", ex);
        }
    }

    private void ReadDepartments(string Request_No)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Load_Department_By_Req_No_New";
        hashPara.Add("@Request_No", Request_No);
        try
        {
            //DNRCOMM//objDal.OpenConnection();
            DataSet ds = new DataSet();
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_Id";
            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadDepartments", ex);
        }
    }

    private Approve GetControlsValue()
    {
        Approve ObjApprove = new Approve();
        Utility objUtil = new Utility();

        try
        {
            ObjApprove.Request_No = Convert.ToString(txtRequestNo.Text);
            ObjApprove.Department = Convert.ToString(ddlDepartment.SelectedValue);
            ObjApprove.ApproveBy = ddlApproveBy.SelectedValue;
            ObjApprove.sRemarks = ddlRemarks.SelectedValue;
            ObjApprove.stxtRemarks = txtRemarks.Text.Trim();
            ObjApprove.ApproveByAdmin = lblUserId.Text.Trim();
        }
        catch (Exception ex)
        {
            _log.Error("GetControlsValue", ex);
        }
        return ObjApprove;
    }

    protected void btnLoadDept_Click(object sender, EventArgs e)
    {
        try
        {
            ReadDepartments(txtRequestNo.Text.Trim());

        }
        catch (Exception ex)
        { _log.Error("btnLoadDept_Click", ex); }
    }
}