using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Transaction_UserControl_ucRequestDtl : System.Web.UI.UserControl
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
            FillRequestDetail();
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
    }

    public void FillRequestDetail()
    {
        if (mMasterHeaderId == null)
            mMasterHeaderId = "0";

        if (mMassRequestProcessId == null)
            mMassRequestProcessId = "0";

        if (SafeTypeHandling.ConvertStringToInt32(mMassRequestProcessId) > 0)
        {
            ds = ObjMasterAccess.GetMWTMasterDetailsByMassRequestProcessId(mMassRequestProcessId);
        }
        else if (SafeTypeHandling.ConvertStringToInt32(mMasterHeaderId) > 0)
        {
            ds = ObjMasterAccess.GetMWTMasterDetailsByMasterHeaderID(mMasterHeaderId);
        }
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRequestor.Text = ds.Tables[0].Rows[0]["Requestor"].ToString();
                lblRequestStatus.Text = ds.Tables[0].Rows[0]["Request_Status"].ToString();
                lblMasterType.Text = ds.Tables[0].Rows[0]["Master_Type"].ToString();
                lblRequestDate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
                lblRequestNo.Text = ds.Tables[0].Rows[0]["Request_No"].ToString();
                lblModule.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            else
            {
                string NotAppli = "N/A";

                lblRequestor.Text = NotAppli;
                lblRequestStatus.Text = NotAppli;
                lblMasterType.Text = NotAppli;
                lblRequestDate.Text = NotAppli;
                lblRequestNo.Text = NotAppli;
                lblModule.Text = NotAppli;
            }
        }
    }
}