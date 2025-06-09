using Accenture.MWT.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administration_AddReportingManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        UserAccess userAccess = new UserAccess();
        string flag;
        flag = userAccess.UpdateReportingManager(txtRequestor.Text, txtApprover.Text);
        if (flag == "-1")
        {
            lblMsg.Text = "User does not Exist.Please create User.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            //lblMsg.Font = new Font("Arial", 24, FontStyle.Bold);
            pnlMsg.Visible = true;
        }
        else if (flag == "-2")
        {
            lblMsg.Text = "Reporting Manager does not Exist.Please create User.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            //lblMsg.Font = new Font("Arial", 24, FontStyle.Bold);
            pnlMsg.Visible = true;
        }
        else if (flag == "-3")
        {
            lblMsg.Text = "User and Reporting Manager cannot be the same.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            //lblMsg.Font = new Font("Arial", 24, FontStyle.Bold);
            pnlMsg.Visible = true;
        }
        else
        {
            lblMsg.Text = txtRequestor.Text + " Updated New Reporting Manager " + txtApprover.Text;
            lblMsg.ForeColor = System.Drawing.Color.Green;
            pnlMsg.Visible = true;
            //ClearControls();
            txtRequestor.Text = "";
            txtApprover.Text = "";
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        txtRequestor.Text = "";
        txtApprover.Text = "";
        lblMsg.Text = "";
    }
}