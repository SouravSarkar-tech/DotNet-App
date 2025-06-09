using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using log4net;
public partial class Transaction_Common_WorkFlowSearch : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MasterAccess ObjMasterAccess = new MasterAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try { 
        string[] mhid = ObjMasterAccess.GetMWTMasterDetailsByRequestNo(txtRequestNo.Text.Trim());

        ucRequestDtl1.MassRequestProcessId = mhid[0];
        ucRequestDtl1.MasterHeaderId = mhid[1];
        ucRequestDtl1.FillRequestDetail();

        WorkFlow.MassRequestProcessId = mhid[0];
        WorkFlow.MasterHeaderId = mhid[1];
        WorkFlow.FillWorkFlowHistoryGrid();

        SAPIntegrationLog.MassRequestProcessId = mhid[0];
        SAPIntegrationLog.MasterHeaderId = mhid[1];
        SAPIntegrationLog.FillSAPHistoryGrid();
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }
}