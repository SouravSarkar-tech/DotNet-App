using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using log4net;
public partial class Transaction_BOMRecipe_SAPQAIntegration : System.Web.UI.Page
{

    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            hyper.Text = "Release";

            //hyper.NavigateUrl = "../../SAPIntegration/SAPQAIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString() + "&usrName=" + Session[StaticKeys.SAPUserName].ToString() + "&pwd" + Session[StaticKeys.SAPPassword].ToString();
            hyper.NavigateUrl = "../../SAPIntegration/SAPQAIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();

            FillHistoryGrid();

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    private void FillHistoryGrid()
    {
        try
        {
            MasterAccess ObjMasterAccess = new MasterAccess();

            DataSet ds = ObjMasterAccess.GetSAPQAIntegrationDataByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString());

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
        catch (Exception ex)
        { _log.Error("FillHistoryGrid", ex); }
    }

}