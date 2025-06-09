using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

public partial class Transaction_PriceMaster_SAPIntegration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hyper.Text = "Upload To SAP";

        //SDT17052019 Change By NR , Desc : Get page path  from web config
        string Path = ConfigurationManager.AppSettings["SAPIntPath"];
        hyper.NavigateUrl = Path + "/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();
        //EDT17052019 Change By NR , Desc : 
        // hyper.NavigateUrl = "http://mwtqas.lupin.com/SAPIntegration/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();
        //hyper.NavigateUrl = "../../SAPIntegration/SAPIntegration.aspx?uid=" + Session[StaticKeys.LoggedIn_User_Id].ToString() + "&mid=" + Session[StaticKeys.MasterHeaderId].ToString();

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
            //else
            //{
                bool sflag = false;
                MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
                DataSet ds1 = materialMasterAccess.ReadPR(Session[StaticKeys.MasterHeaderId].ToString());
                if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                {
                    //    if (MaterialMasterAccess.IsUserHasPRReq(lblMasterHeaderId.Text))
                    //{
                    string sdate = "";
                    try
                    {
                        DateTime date = System.DateTime.Now;
                        sdate = date.ToString("dd/MM/yyyy");
                        sdate = sdate.Replace(@"/", "");
                        WriteWFLog("CreateWFLog_" + sdate + ".txt", "Start of execution WF API");
                    }
                    catch (Exception ex)
                    { 
                    }

                    var responseText = "";
                    string docId = "";
                    string emailId = "";
                    try
                    {

                        docId = Convert.ToString(ds1.Tables[0].Rows[0]["sWFRequestNo"].ToString());
                        emailId = Convert.ToString(Session[StaticKeys.LoggedIn_User_Name]);
                        emailId = emailId + "@lupin.com";
                        WriteWFLog("CreateWFLog_" + sdate + ".txt", "reqId :" + docId + ", emailId:" + emailId + "");

                    string webAddr = Convert.ToString(ConfigurationManager.AppSettings["workflowLink"]) + "/completeTaskByMail?docId=" + docId + "&emailId=" + emailId + "&decision=APPROVED&formID=viaMail";
                    string username = Convert.ToString(ConfigurationManager.AppSettings["wfusername"]);
                    string password = Convert.ToString(ConfigurationManager.AppSettings["wfpassword"]); 

                    // string webAddr = "http://172.36.0.157:18086/workflowwebui/mobile/dtr/completeTaskByMail?docId=133084&emailId=alfreddsouza@lupin.com&decision=APPROVED&formID=viaMail";
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        httpWebRequest.Method = "POST";

                    //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("testing:123456");
                    //string val = System.Convert.ToBase64String(plainTextBytes);
                    //httpWebRequest.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

                    //var username = "restl2j";
                    //var password = "Lupin123";
                    //var username = "adminl2j";
                    //var password = "mQQ0f5S7";
                    string encoded = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
                        //string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                        //                               .GetBytes(username + ":" + password));
                        httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))                        {
                            responseText = streamReader.ReadToEnd();

                            if (responseText == "\"Success\"")
                            //if (responseText == "\"Fail\"")
                            {
                                WriteWFLog("CreateWFLog_" + sdate + ".txt", "Success Msg :" + responseText + "");
                                //BasicDataAccess basicDataAccess = new BasicDataAccess();
                                //basicDataAccess.UpdateRequestStatus(proslid, Convert.ToString(TxtRemarks.Text), "S", "");

                                //

                                sflag = true;
                            }
                            else
                            {
                                sflag = false;
                                WriteWFLog("CreateWFLog_" + sdate + ".txt", "Error Msg :" + responseText + "");

                            }
                        }
                    }
                    catch (WebException ex)
                    { 

                        Console.WriteLine(ex.Message);
                        WriteWFLog("CreateWFLog_" + sdate + ".txt", "Error Msg :" + ex.Message);
                    }
                    WriteWFLog("CreateWFLog_" + sdate + ".txt", "End of execution WF API");


                    //if (1 == 1)
                    //{
                    //    sflag = false;
                    //}
                    //else
                    //{
                    //    sflag = false;
                    //}
                }
                else
                {
                    sflag = true;
                }
           // }
        }
    }



    public void WriteWFLog(string strFileName, string strMessage)
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