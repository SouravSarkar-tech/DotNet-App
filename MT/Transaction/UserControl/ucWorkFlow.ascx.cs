using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Transaction_UserControl_ucWorkFlow : System.Web.UI.UserControl
{
    private string mMasterHeaderId;
    private string mMassRequestProcessId;

    public string MasterHeaderId
    {
        get { return mMasterHeaderId; }
        set { mMasterHeaderId = value; }
    }

    public string MassRequestProcessId
    {
        get { return mMassRequestProcessId; }
        set { mMassRequestProcessId = value; }
    }
    MasterAccess ObjMasterAccess = new MasterAccess();

    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            FillWorkFlowHistoryGrid();
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
    }

    public void FillWorkFlowHistoryGrid()
    {
        if (mMasterHeaderId == null)
            mMasterHeaderId = "0";

        if (mMassRequestProcessId == null)
            mMassRequestProcessId = "0";

       // MasterAccess ObjMasterAccess = new MasterAccess();

       // DataSet ds;

        if (SafeTypeHandling.ConvertStringToInt32(mMassRequestProcessId) > 0)
        {
            ds = ObjMasterAccess.GetMWTWorkFlowDataByMassRequestProcessId(mMassRequestProcessId);
        }
        else if (SafeTypeHandling.ConvertStringToInt32(mMasterHeaderId) > 0)
        {
            ds = ObjMasterAccess.GetMWTWorkFlowDataByMasterHeaderID(mMasterHeaderId);
        }
        if (ds != null)
        {
            grdWorkFlowHistory.DataSource = ds.Tables[0];
            grdWorkFlowHistory.DataBind();

        rptWorkflow.DataSource = ds.Tables[1];
        rptWorkflow.DataBind();

            FillrptWorkflow(ds.Tables[1]);
        }
    }

    protected void FillrptWorkflow(DataTable dt)
    {
        int cnt = rptWorkflow.Items.Count;
        Label lblWorkflow;
        
        for (int i = 0; i < rptWorkflow.Items.Count; i++)
        {
            lblWorkflow = (Label)rptWorkflow.Items[i].FindControl("lblWorkflow");
            if (dt.Rows[i]["Actioned_On"].ToString() == "")
                lblWorkflow.BackColor = System.Drawing.Color.RosyBrown;
            else
                lblWorkflow.BackColor = System.Drawing.Color.YellowGreen;
            
            //Changes for showing the email ID of HOSAPmasters
            if (dt.Rows[i]["Department_Name"].ToString() == "Master Cell")
                //Change done by Swati on 27 Sept to display only Email ID for Master cell team 
                //lblWorkflow.Text = "<b>" + dt.Rows[i]["Department_Name"].ToString() + "</b><br /><span style='font-size:smaller'>" + dt.Rows[i]["Actioned_On"].ToString() + "</span>" + "<br /><span style='font-size:small'>" + dt.Rows[i]["Actioned_By"].ToString() + "(Hosapmasters@lupin.com)" + "</span>";            
                lblWorkflow.Text = "<b>" + dt.Rows[i]["Department_Name"].ToString() + "</b><br /><span style='font-size:smaller'>" + dt.Rows[i]["Actioned_On"].ToString() + "</span>" + "<br /><span style='font-size:small'>(Hosapmasters@lupin.com)" + "</span>";
            //end
            else
                lblWorkflow.Text = "<b>" + dt.Rows[i]["Department_Name"].ToString() + "</b><br /><span style='font-size:smaller'>" + dt.Rows[i]["Actioned_On"].ToString() + "</span>" + "<br /><span style='font-size:small'>" + dt.Rows[i]["Actioned_By"].ToString()  + "</span>";

            if (cnt == i + 1)
            {
                ((Image)rptWorkflow.Items[i].FindControl("imgArrow")).Visible = false;
            }
        }
    }
}