using Accenture.MWT.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Transaction_Common_ProsolReport : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    HelperAccess helperAccess = new HelperAccess();
    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
    Utility ObjUtil = new Utility();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    bindgridview();
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
    /// <summary>
    /// Created by manali chavan 
    /// method binds the data from pr_GetProsolReport sp in gridview.
    /// </summary>
    private void bindgridview()
    {
        try
        {
            //SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            //SqlCommand cmd = new SqlCommand("pr_GetProsolReport", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@mode", 'V');
            //SqlDataAdapter adpData = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //adpData.Fill(ds);
            //grdReport.DataSource = ds;
            //grdReport.DataBind();
            //con.Close();

            //try
            //{
            DataSet dstData = new DataSet();
            dstData = objMatAccess.GetProsolRecord(txtProsolid.Text, txtMwtReqNum.Text, ObjUtil.GetMMDDYYYY(txtFromdate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), "SS");
            grdReport.DataSource = dstData;
            grdReport.DataBind();
            //}
            //catch (Exception ex)
            //{

            //}
        }
        catch (Exception ex)
        { _log.Error("bindgridview", ex); }
    }
    /// <summary>
    /// btnSearch event
    /// Added by manali chavan
    /// onclick on button report will be  generated as per searched parameter.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            bindgridview();
            //DataSet dstData = new DataSet();
            //dstData = objMatAccess.GetProsolRecord(txtProsolid.Text, txtMwtReqNum.Text, ObjUtil.GetMMDDYYYY(txtFromdate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), "S");
            //grdReport.DataSource = dstData;
            //grdReport.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("btnSearch_Click", ex);
        }
    }

    protected void grdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdReport.PageIndex = e.NewPageIndex;
            bindgridview();
        }
        catch (Exception ex)
        {
            _log.Error("grdReport_PageIndexChanging", ex);
        }
    }
}