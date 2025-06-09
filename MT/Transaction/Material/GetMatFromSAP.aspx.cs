using Accenture.MWT.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
//MSC_8300001775
public partial class Transaction_Material_GetMatFromSAP : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        lblSMConf.Text = "Are you sure you want to get material details from SAP.";
        //panelSMConf.CssClass = "error";
        //panelSMConf.Visible = true;

    //}

    //protected void btnSMConfOk_Click(object sender, EventArgs e)
    //{

        try
        {
            btnSMConfOk.Text = "Ok";

            //lblSMConf.Text = "Are you sure you want to get material details from SAP.";
            ////panelSMConf.CssClass = "error";
            ////panelSMConf.Visible = true;
            //HyperLink hl1 = new HyperLink();
            string Path = ConfigurationManager.AppSettings["SAPIntPath"];
            //btnSMConfOk.NavigateUrl = Path + "/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();

            btnSMConfOk.NavigateUrl = Path + "/GetMatFromSAP.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() +
                //"&mid=" + Session[StaticKeys.MasterHeaderId] +
                "&mco=" + Convert.ToString(Session[StaticKeys.mco]) + "&mty=" + Convert.ToString(Session[StaticKeys.mty]) +
                "&pla=" + Convert.ToString(Session[StaticKeys.pla]) + "&stg=" + Convert.ToString(Session[StaticKeys.stg]) +
                //"&pog=" + Convert.ToString(Session[StaticKeys.pog]) +
                "&sal=" + Convert.ToString(Session[StaticKeys.sal]) +
                "&dch=" + Convert.ToString(Session[StaticKeys.dch]) + "&sec=" + Convert.ToString(Session[StaticKeys.sec]);
            //+ "&sec=" + Convert.ToString(lblGetfromSAP.Text);
        }
        catch (Exception ex)
            { _log.Error("Page_Load1", ex); }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    private void FillHistoryGrid()
    {
        MasterAccess ObjMasterAccess = new MasterAccess();
        try
        {
        DataSet ds = ObjMasterAccess.GetSAPIntegrationDataByMasterHeaderID(Session[StaticKeys.MasterHeaderId].ToString());

        //grdSearch.DataSource = ds.Tables[0];
        //grdSearch.DataBind();

        if (ds.Tables[1].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows[0]["Request_Status"].ToString() == "P")
            {
                btnSMConfOk.Visible = true;
            }
            }
        }
        catch (Exception ex)
        { _log.Error("FillHistoryGrid", ex); }
    }
}