using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Transaction_Vendor_SAPIntegration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hyper.Text = "Upload to SAP";
        //hyper.NavigateUrl = "http://localhost/SAPIntegration/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();
        hyper.NavigateUrl = "../../SAPIntegration/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();

        FillHistoryGrid();
    }

    private void FillHistoryGrid()
    {
        MasterAccess ObjMasterAccess = new MasterAccess();

        DataSet ds = ObjMasterAccess.GetSAPIntegrationDataByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString());

        grdSearch.DataSource = ds.Tables[0];
        grdSearch.DataBind();

        if (ds.Tables[1].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows[0]["Request_Status"].ToString() == "P")
            {
                hyper.Visible = true;
            }
        }
    }
}