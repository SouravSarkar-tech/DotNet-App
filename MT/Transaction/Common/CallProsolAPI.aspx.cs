using Accenture.MWT.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class Transaction_Common_CallProsolAPI : System.Web.UI.Page
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
    /// method binds data from pr_GetProsolRetriggerLog sp in gridview.
    /// </summary>
    private void bindgridview()
    {

        //SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        //SqlCommand cmd = new SqlCommand("pr_GetProsolRetriggerLog", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@mode", 'V');
        //SqlDataAdapter adpData = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adpData.Fill(ds);
        //grdReqDetails.DataSource = ds;
        //grdReqDetails.DataBind();
        //con.Close();

        try
        {

            DataSet dstData = new DataSet();
            dstData = objMatAccess.GetProsolRecord(txtProsolid.Text, txtMwtReqNum.Text, ObjUtil.GetMMDDYYYY(txtFromdate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text), "RC");
            grdReqDetails.DataSource = dstData;
            grdReqDetails.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("bindgridview", ex);
        }

    }

    protected void grdReqDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdReqDetails.PageIndex = e.NewPageIndex;
            bindgridview();
        }
        catch (Exception ex)
        { _log.Error("grdReqDetails_PageIndexChanging", ex); }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            bindgridview();
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }

    protected void grdReqDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                // etc.
            }
        }
        catch (Exception ex)
        { _log.Error("grdReqDetails_RowCommand", ex); }
    }

    protected void rdoSelection_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButton rdoSelection = (RadioButton)sender;
            GridViewRow grv = (GridViewRow)rdoSelection.Parent.Parent;

            Label lblMWTID = grv.FindControl("lblMWTID") as Label;
            TextBox TxtRemarks = grv.FindControl("TxtRemarks") as TextBox;
        }
        catch (Exception ex)
        { _log.Error("rdoSelection_CheckedChanged", ex); }
    }

    protected void BtnCreateReqInMWT_Click(object sender, EventArgs e)
    {
        try
        {
            Button rdoSelection = (Button)sender;
            GridViewRow grv = (GridViewRow)rdoSelection.Parent.Parent;
            Label lblsProsolID = grv.FindControl("lblsProsolID") as Label;
            Label lblMWTID = grv.FindControl("lblMWTID") as Label;
            Label lblsSAPMatNumber = grv.FindControl("lblsSAPMatNumber") as Label;
            TextBox TxtRemarks = grv.FindControl("TxtRemarks") as TextBox;

            if (lblMWTID.Text == "" && lblsSAPMatNumber.Text == "" && lblsProsolID.Text != "" && TxtRemarks.Text != "")
            {
                BasicDataAccess basicDataAccess = new BasicDataAccess();
                basicDataAccess.ReCreateMWTRequest(lblsProsolID.Text, Convert.ToString(TxtRemarks.Text));

            }
            else
            {

            }
        }
        catch (Exception ex)
        { _log.Error("BtnCreateReqInMWT_Click", ex); }
    }

    protected void BtnRetrigger_Click(object sender, EventArgs e)
    {
        try
        {
            Button rdoSelection = (Button)sender;
            GridViewRow grv = (GridViewRow)rdoSelection.Parent.Parent;
            Label lblsProsolID = grv.FindControl("lblsProsolID") as Label;
            Label lblsSAPMatNumber = grv.FindControl("lblsSAPMatNumber") as Label;
            TextBox TxtRemarks = grv.FindControl("TxtRemarks") as TextBox;

            if (lblsProsolID.Text != "" && lblsSAPMatNumber.Text != "" && TxtRemarks.Text != "")
            {
                string sdate = "";
                try
                {
                    DateTime date = System.DateTime.Now;
                    sdate = date.ToString("dd/MM/yyyy");
                    sdate = sdate.Replace(@"/", "");
                    WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Start of execution Prosol API");
                }
                catch (Exception ex)
                {
                    _log.Error("BtnRetrigger_Click0", ex);
                }

                var responseText = "";
                string proslid = "";
                try
                {
                    int matcode = Convert.ToInt32(lblsSAPMatNumber.Text);
                    proslid = Convert.ToString(lblsProsolID.Text);
                    WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "reqId :" + proslid + ", matcode:" + matcode + "");

                    string webAddr = Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"]) + "/prosol/api/matcode/updatematerialcode?reqId=" + proslid + "&matcode=" + matcode + "";

                    ////string webAddr = Convert.ToString(ConfigurationManager.AppSettings["ProsolLink"])+"" + Apipara;
                    //string webAddr = "http://prosolqa.lupin.com/prosol/api/matcode/updatematerialcode?" + Apipara;
                    //http://prosolqa.lupin.com/prosol/
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Method = "GET";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseText = streamReader.ReadToEnd();

                        if (responseText == "\"Success\"")
                        {
                            WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Success Msg :" + responseText + "");
                            BasicDataAccess basicDataAccess = new BasicDataAccess();
                            basicDataAccess.UpdateRequestStatus(proslid, Convert.ToString(TxtRemarks.Text), "S", "");

                            //
                        }
                        else
                        {
                            WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Error Msg :" + responseText + "");

                        }
                    }

                    //}
                    //else
                    //{

                    //}
                }
                catch (WebException ex)
                {
                    _log.Error("BtnRetrigger_Click11", ex);

                    Console.WriteLine(ex.Message);
                    WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "Error Msg :" + ex.Message);
                }
                WriteProsolLog("CreateProsolLog_" + sdate + ".txt", "End of execution Prosol API");

            }
            else
            {

            }
        }
        catch (Exception ex)
        { _log.Error("BtnRetrigger_Click", ex); }
    }


    /// <summary>
    /// PROSOL_SDT16092019
    /// </summary>
    /// <param name="strFileName"></param>
    /// <param name="strMessage"></param>
    public void WriteProsolLog(string strFileName, string strMessage)
    {
        try
        {
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ProsolLog", strFileName), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
            objStreamWriter.Close();
            objFilestream.Close();
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }
}