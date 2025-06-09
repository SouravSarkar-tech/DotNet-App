using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Diagnostics;

public partial class Administration_RunMassProcess : System.Web.UI.Page
{
   
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //System.Diagnostics.Process Myprocess;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                }
                //Myprocess = new System.Diagnostics.Process();
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    public static void StartIExplorer()
    {
        ProcessStartInfo info
    = new ProcessStartInfo(@"E:\Mass_ExtSync\MWT_Sync.exe");
        info.UseShellExecute = false;
        info.RedirectStandardInput = true;
        info.RedirectStandardOutput = true;
        info.RedirectStandardError = true;

        //string password = "password";
        //SecureString securePassword = new SecureString();

        //for (int i = 0; i < password.Length; i++)
        //    securePassword.AppendChar(Convert.ToChar(password[i]));

        //info.UserName = "userName";
        //info.Password = securePassword;
        //info.Domain = "domain";

        try
        {
            Process.Start(info);
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            //Console.WriteLine(ex.Message);
            _log.Error("StartIExplorer", ex);
        }
    }
    protected void btnStartProgram_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            //StartIExplorer();
            //string StrPath = String.Empty;
            //StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathMP"]);
            //_log.Info("StrPath" + StrPath);
            ////Myprocess = new System.Diagnostics.Process();
            //Myprocess.StartInfo.CreateNoWindow = true;
            //_log.Info("CreateNoWindow");
            //Myprocess.StartInfo.UseShellExecute = true;
            //_log.Info("UseShellExecute");
            //Myprocess.StartInfo.WorkingDirectory = "E:/MWTQAS/Mass_ExtSync";// "C://Users//nitinrajeshirke//Documents//Nitin Develpments//MWT India//Mass_ExtSync";
            //_log.Info("WorkingDirectory" + StrPath);
            //Myprocess.StartInfo.FileName = "MWT_Sync.exe";
            //_log.Info("FileName");
            //Myprocess.StartInfo.Arguments = "MyArgument";
            //_log.Info("MyArgument");
            //Myprocess.Start();
            //_log.Info("Start");
            //ProcessStartInfo info = new ProcessStartInfo("E:\\MWTQAS\\Mass_ExtSync\\MWT_Sync.exe ");
            //info.Arguments = "-silent";
            //Process.Start(info);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string operation = "C";
                string spName = "pr_get_FlagStatus";
                SqlCommand sqlCmd = new SqlCommand(spName, con);
                SqlDataAdapter sqlDa;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Operation", operation);
                sqlCmd.Parameters.AddWithValue("@sFlag", "C");
                System.Data.DataTable dt1;
                sqlDa = new SqlDataAdapter(sqlCmd);
                dt1 = new System.Data.DataTable();
                sqlDa.Fill(dt1);

                if (dt1.Rows.Count > 0)
                { 
                    if (dt1.Rows[0]["sFlag"].ToString() == "P")
                    {
                        lblMsg.Text = "Process is already running.";
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;
                    }
                    else
                    {
                        try
                        {
                            SqlTransaction transaction1 = con.BeginTransaction("T2");
                            var selectCommand = new SqlCommand("update dbo.T_MassProgramFlagTB set sFlag = 'R'", con, transaction1);

                            var retValue = selectCommand.ExecuteNonQuery();
                            transaction1.Commit();

                        }
                        catch (Exception ex)
                        { _log.Error("transaction1", ex); }


                        lblMsg.Text = "Data transfer from MWT to SAP. Please check after few minutes";
                        pnlMsg.CssClass = "success";
                        pnlMsg.Visible = true;
                    }
                     
                }
                else
                {
                    lblMsg.Text = "Record is not available for process.";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                _log.Error("SpFlagStatus", ex);
            }


            //lblMsg.Text = "Data transfer from MWT to SAP. Please check after few minutes";
            //pnlMsg.CssClass = "success";
            //pnlMsg.Visible = true;

        }
        catch (Exception ex)
        { _log.Error("btnStartProgram_Click", ex); }
    }

    protected void btnStopProgram_Click(object sender, EventArgs e)
    {
        try
        {
            //string StrPath = String.Empty;
            //StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathMP"]);
            //Myprocess = new System.Diagnostics.Process();
            //Myprocess.StartInfo.CreateNoWindow = true;
            //Myprocess.StartInfo.UseShellExecute = true;
            //Myprocess.StartInfo.WorkingDirectory = StrPath;// "C://Users//nitinrajeshirke//Documents//Nitin Develpments//MWT India//Mass_ExtSync";
            //Myprocess.StartInfo.FileName = "MWT_Sync.exe";
            //Myprocess.StartInfo.Arguments = "MyArgument";
            //foreach (Process proc in Process.GetProcessesByName("MWT_Sync"))
            //{
            //    Myprocess.Kill();
            //}

            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("MWT_Sync");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("btnStopProgram_Click", ex); }
    }
}