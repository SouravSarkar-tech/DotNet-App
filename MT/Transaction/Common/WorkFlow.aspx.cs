using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Transaction_Common_WorkFlow : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string mhid = "0";
            string mpid = "0";
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["MHID"] != null)
                        mhid = Request.QueryString["MHID"].ToString();

                    if (Request.QueryString["MPID"] != null)
                        mpid = Request.QueryString["MPID"].ToString();
                }
                //else
                //{
                //    mhid = "0";
                //}

                WorkFlow.MasterHeaderId = mhid;
                SAPIntegrationLog.MasterHeaderId = mhid;
                ucRequestDtl1.MasterHeaderId = mhid;

                WorkFlow.MassRequestProcessId = mpid;
                SAPIntegrationLog.MassRequestProcessId = mpid;
                ucRequestDtl1.MassRequestProcessId = mpid;
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
}