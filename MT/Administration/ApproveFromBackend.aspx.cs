using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Drawing.Design;
using System.Data.SqlClient;
using System.Collections;

public partial class Administration_ApproveFromBackend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                pnlAddNew.Visible = true;
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        UserAccess userAccess = new UserAccess();
        Approve ObjApprove = GetControlsValue();
        
        int flag;
        flag = userAccess.Save(ObjApprove);
        if(flag == 1)
        {
            txtRequestNo.Text = "";
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

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        txtRequestNo.Text = "";
        ddlDepartment.SelectedIndex = 0;
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        ddlApproveBy.SelectedIndex = 0;
        pnlMsg.Visible = false;
    }

    private void ReadDepartments(string Request_No)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Load_Department_By_Req_No";
        hashPara.Add("@Request_No", Request_No);
        try
        {
            //DNRCOMM//            objDal.OpenConnection();
            DataSet ds = new DataSet();
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_Id";
            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ObjApprove;
    }

    protected void btnLoadDept_Click(object sender, EventArgs e)
    {
        ReadDepartments(txtRequestNo.Text);
    }
}