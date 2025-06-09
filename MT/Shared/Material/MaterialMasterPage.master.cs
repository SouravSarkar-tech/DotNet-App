using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Accenture.MWT.DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Transactions;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Web.Configuration;
using log4net;

public partial class Shared_Material_MaterialMasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    string sdate = "";
    //List<string> IRFDivision = new List<string> { "1", "2", "3", "4", "5", "11", "12", "13", "14", "15", "16", "17", "18", "20", "21", "26", "27", "28", "29", "31", "32", "33", "34", "35" };
    List<string> IRFDivision = new List<string>();// { "1", "2", "3", "4", "5", "11", "12", "13", "14", "15", "16", "17", "18", "20", "21", "26", "27", "28", "29", "31", "32", "33", "34", "35", "36", "41", "42", "39", "53", "38", "55", "56", "57", "60", "61", "62", "63", "64", "65" };
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly. 
        // All webpages displaying an ASP.NET menu control must inherit this class. 
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            try
            {
                DateTime date = System.DateTime.Now;
                sdate = date.ToString("dd/MM/yyyy");
                sdate = sdate.Replace(@"/", "");

            }
            catch (Exception ex)
            {
                _log.Error("Page_Load", ex);
            }


            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoStore();

            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            if ((Session[StaticKeys.LoggedIn_User_Id] != null) && (Session[StaticKeys.MasterHeaderId] != null))
            {
                //lblRequestNo.Text = Session[StaticKeys.RequestNo].ToString();

                lnkRequestNo.Text = Session[StaticKeys.RequestNo].ToString();
                lnkRequestNo.Attributes.Add("OnClientClick", "OpenRequestHistory('" + Session[StaticKeys.MasterHeaderId].ToString() + "','0');");

                lblMaterialNo.Text = Session[StaticKeys.MaterialNo].ToString();
                lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

                lblPlant.Text = Session[StaticKeys.MaterialPlantName].ToString();
                lblStorageLocation.Text = Session[StaticKeys.MatStorageLocationName].ToString();

                lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
                lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                if (!IsPostBack)
                {
                    //if (Session[StaticKeys.LoggedIn_User_Profile] != null && Session[StaticKeys.SelectedModuleId] != null)
                    //{
                    MaterialMasterAccess objAccess = new MaterialMasterAccess();
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    lblMassRequestProcessId.Text = Session[StaticKeys.MassRequestProcessId].ToString();
                    ReadDeparmentListForRollback(lblMasterHeaderId.Text, userDeptId, moduleId);

                    string mode = Session[StaticKeys.Mode].ToString();


                    //if ((mode == "M" || mode == "N") && (lblMassRequestProcessId.Text == "0" || lblMassRequestProcessId.Text == ""))
                    if ((mode == "M" || mode == "N"))
                    {
                        if (Session[StaticKeys.MaterialProcessModuleId] != null)
                        {
                            if (Session[StaticKeys.MaterialProcessModuleId].ToString() != "")
                            {
                                moduleId = Session[StaticKeys.MaterialProcessModuleId].ToString();
                            }
                        }

                        btnRejectTo.Visible = objAccess.IsUserInitiator(moduleId, userDeptId, lblUserId.Text, lblMasterHeaderId.Text);
                        ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());

                        //if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R" || Session[StaticKeys.ActionType].ToString() == "E") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        //if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        //if ((Session[StaticKeys.MatPlantGrp].ToString() == "1") && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        //if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        //if ((Session[StaticKeys.MatPlantGrp].ToString() == "1") && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        //if ((Convert.ToString(ConfigurationManager.AppSettings["MMSMC"]) == "Y") && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        if ((Session[StaticKeys.MatPlantGrp].ToString() == "1") && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            if (!objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text) && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                            {
                                btnSAPUpload.Visible = true;
                                btnSubmit.Visible = false;

                                //Promotion code start
                                //if (btnRejectTo.Visible && (moduleId == "162" || moduleId == "164" || moduleId == "139" || moduleId == "145" || moduleId == "144" || moduleId == "171" ))
                                if (btnRejectTo.Visible && (moduleId == "162" || moduleId == "164" || moduleId == "139" || moduleId == "145" || moduleId == "144" || moduleId == "171" || moduleId == "195"))
                                //Promotion code end
                                {
                                    lblRejectionType.Text = "M";
                                    tdChkReject.Visible = true;
                                    tdDdlReject.Visible = false;
                                }
                            }
                            else
                            {
                                //btnSAPUpload.Visible = false;
                                //if (btnRejectTo.Visible)
                                //    btnRejectTo.Visible = !objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text);

                                //btnSubmit.Visible = true;
                                //if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                //    btnSubmit.Text = "SAP Uploaded";
                                //else
                                //    btnSubmit.Text = "Approve";

                                btnSAPUpload.Visible = false;
                                if (btnRejectTo.Visible)
                                {
                                    btnRejectTo.Visible = !objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text);
                                }
                                if (moduleId == "138" && !objAccess.IsProsolSPOCPending(lblMasterHeaderId.Text))//PROSOL_SDT09112021
                                {
                                    btnSubmit.Visible = false;
                                }
                                else
                                {
                                    btnSubmit.Visible = true;
                                }
                                if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                {
                                    btnSubmit.Text = "SAP Uploaded";
                                }
                                else
                                {
                                    btnSubmit.Text = "Approve";
                                }
                            }
                        }
                        //MSC_8300001775
                        else if ((Session[StaticKeys.ActionType].ToString() == "C") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                        {
                            if (Convert.ToString(ConfigurationManager.AppSettings["MMSMC"]) == "Y")
                            {
                                if (!objAccess.IsSAPintegrationMASSPending(lblMasterHeaderId.Text) && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                {
                                    btnSAPMassUpload.Visible = true;
                                    btnSubmit.Visible = false;

                                }
                                else
                                {
                                    btnSAPMassUpload.Visible = false;
                                    if (btnRejectTo.Visible)
                                        btnRejectTo.Visible = !objAccess.IsSAPintegrationMASSPending(lblMasterHeaderId.Text);

                                    btnSubmit.Visible = true;
                                    //if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                    //{
                                    //    btnSubmit.Text = "SAP Uploaded";
                                    //    btnSubmit.Enabled = false;
                                    //}
                                    //else
                                    btnSubmit.Text = "Approve";
                                }
                            }
                            else
                            {
                                btnSubmit.Text = "Approve";
                                btnSAPMassUpload.Visible = false;
                                btnSubmit.Visible = true;
                            }
                        }
                        //MSC_8300001775

                        //MSE_8300002156
                        else if ((Session[StaticKeys.ActionType].ToString() == "E") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && (objAccess.IsSAPMASSintegrationChkAval(lblMasterHeaderId.Text)))
                        {
                            if (!objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text) && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                            {
                                //btnSAPMassUpload.Visible = true;
                                btnSAPUpload.Visible = true;
                                btnSubmit.Visible = false;

                            }
                            else
                            {
                                btnSAPUpload.Visible = false;
                                //btnSAPMassUpload.Visible = false;
                                if (btnRejectTo.Visible)
                                    btnRejectTo.Visible = !objAccess.IsSAPintegrationPending(lblMasterHeaderId.Text);

                                btnSubmit.Visible = true;
                                if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                {
                                    //btnSubmit.Text = "SAP Uploaded";
                                    btnSubmit.Text = "Approve";
                                    //btnSubmit.Enabled = false;
                                }
                                else
                                    btnSubmit.Text = "Approve";
                            }
                        }
                        //MSE_8300002156

                        //MME_8300002156
                        else if ((Session[StaticKeys.ActionType].ToString() == "E") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13" && (!objAccess.IsSAPMASSintegrationChkAval(lblMasterHeaderId.Text)))
                        {
                            if (!objAccess.IsSAPintegrationMASSPending(lblMasterHeaderId.Text) && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                            {
                                btnSAPMassUpload.Visible = true;
                                btnSubmit.Visible = false;

                            }
                            else
                            {
                                btnSAPMassUpload.Visible = false;
                                if (btnRejectTo.Visible)
                                    btnRejectTo.Visible = !objAccess.IsSAPintegrationMASSPending(lblMasterHeaderId.Text);

                                btnSubmit.Visible = true;
                                btnSubmit.Text = "Approve";
                            }
                        }
                        //MME_8300002156

                        else
                        {
                            if (btnRejectTo.Visible)
                            {
                                btnSubmit.Text = "Approve";
                                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                                {
                                    if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                                        btnSubmit.Text = "SAP Uploaded";
                                }
                            }
                            //PROSOL_SDT09112021 Comment
                            //btnSubmit.Visible = btnRejectTo.Visible;
                            //PROSOL_SDT09112021 Comment
                            //PROSOL_SDT09112021 Add
                            if (moduleId == "138" && !objAccess.IsProsolSPOCPending(lblMasterHeaderId.Text))//PROSOL_SDT09112021
                            {
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                btnSubmit.Visible = btnRejectTo.Visible;
                            }
                            //PROSOL_SDT09112021 Add
                        }
                    }
                    else
                    {
                        btnRejectTo.Visible = false;
                        ReadSectionTabs(lblUserId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), Session[StaticKeys.SelectedModuleId].ToString());
                    }
                    //8400000359 S
                    FillDashBoard();
                    //8400000359 E
                }

                //MSC_8300001775
                //HelperAccess.ReqType == "SMC" && 
                if (Session[StaticKeys.MatPlantGrp].ToString() == "2" && Session[StaticKeys.ActionType].ToString() == "N")
                {
                    SingleMatChange();
                    imgSMChange.Visible = true;
                }
                else
                {
                    //litTabSMChange.Text = "";
                    imgSMChange.Visible = false;
                }

                //MSC_8300001775
            }
            else
            {
                Response.Redirect("../login.aspx");
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }



    /// <summary>
    /// 8400000359 
    /// </summary>
    private void FillDashBoard()
    {
        try
        {
            ZcapHsnMasterAccess zcapHsnaccess = new ZcapHsnMasterAccess();
            DataTable Dt1;

            Dt1 = zcapHsnaccess.GetRemarksByUser(Convert.ToInt32(lblMasterHeaderId.Text), Convert.ToString(lblMassRequestProcessId.Text));
            if (Dt1.Rows.Count > 0)
            {
                rptCommon.DataSource = Dt1;
                rptCommon.DataBind();
            }
            else
            {
                rptCommon.DataSource = null;
                rptCommon.DataBind();
            }
            //    rptCommon.DataSource = Dt1;
            //rptCommon.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillDashBoard", ex); }

    }



    /// <summary>
    /// MSC_8300001775
    /// </summary>
    private void SingleMatChange()
    {
        try
        {
            //StringBuilder strBuildersmc = new StringBuilder(); 
            MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
            DataSet ds = materialMasterAccess.ReadSMatChange(lblMasterHeaderId.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelDownload1.Visible = true;
                grdChangertp.DataSource = ds;
                grdChangertp.DataBind();
                //strBuildersmc.Append("<table class='mchgtable'>");
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    strBuildersmc.Append("<tr><td  colspan='3'> Change By :" + row["ChangeBy"] + "</td></tr>");
                //    strBuildersmc.Append("<tr><td  colspan='3'> Change Date :" + row["ChangeOn"] + "</td></tr>");
                //    strBuildersmc.Append("<tr><td> Field Name</td><td>Old Value</td><td> New value </td></tr>");
                //    strBuildersmc.Append("" + row["ChangeRemark"] + "");
                //    strBuildersmc.Append("<tr><td colspan='3'></td></tr>");
                //}
                //strBuildersmc.Append(" </table>");
                //litTabSMChange.Text = strBuildersmc.ToString();
            }
            else
            {
                ExcelDownload1.Visible = false;
                grdChangertp.DataSource = null;
                grdChangertp.DataBind();
            }
        }
        catch (Exception ex)
        { _log.Error("SingleMatChange", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProceedToSub_Click(object sender, EventArgs e)
    {
        //bool flg = false;
        //bool appflg = true;
        bool appflgk = true;
        try
        {
            if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0") && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "171") && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
            {//PROV-CCP-MM-941-23-0045 Start

                try
                {
                    if (MaterialMasterAccess.Get_IsKinaxisFilled(lblMasterHeaderId.Text, lblUserId.Text))
                    {
                        appflgk = true;
                    }
                    else
                    {
                        //appflg = false;
                        appflgk = false;
                    }
                }
                catch (Exception ex) { _log.Error("btnProceedToSub_Click", ex); }

                WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V End// ");
                
            }
            if (appflgk == true)
            {
                Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
                Response.Redirect("MaterialMaster.aspx", false);
            }
            else
            {
                lblMsg.Text = "Kindly enter Kinaxis field in Classification section before approving the request. <a href = '../Material/Classification.aspx'> Click here to update </a>";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //PROV-CCP-MM-941-23-0045 End

            //Session[StaticKeys.AddAlertMsg] = "Material Master Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";
            //Response.Redirect("MaterialMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnProceedToSub_Click", ex); }
    }

    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSAPMassUpload_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess objAccess = new MaterialMasterAccess();
        Utility objUtil = new Utility();
        String sstatus = "";
        DataSet ds;
        try
        {
            ds = objAccess.GetSyncMassData(lblMasterHeaderId.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
            }
            else
            {
                String sTypeOfMassUpdm = Session[StaticKeys.TypeOfMassUpdm].ToString();
                string sFlag = "C";
                if (sTypeOfMassUpdm == "11")
                {
                    MRPUpdation();
                }
                else if (sTypeOfMassUpdm == "12")
                {
                    SelectionMethodUpdation();
                }
                else if (sTypeOfMassUpdm == "13")
                {
                    PlannedPriceUpdation();
                }
                else if (sTypeOfMassUpdm == "14")
                {
                    TaggingBOMRecipeUpdation();
                }
                else if (sTypeOfMassUpdm == "15")
                {
                    ProductionHierarchyUpdation();
                }
                else if (sTypeOfMassUpdm == "16")
                {
                    OtherFieldsUpdation();
                }
                else if (sTypeOfMassUpdm == "17")
                {
                    AUOMUpdation();
                }
                else if (sTypeOfMassUpdm == "18")
                {
                    TAXMUpdation();
                }
                else if (sTypeOfMassUpdm == "19")
                {
                    INSPUpdation();
                }
                else if (sTypeOfMassUpdm == "20")
                {
                    CLASSUpdation();
                }
                else if (sTypeOfMassUpdm == "21")
                {
                    sFlag = "E";
                    MaterialMasterExt();
                }
                else if (sTypeOfMassUpdm == "22")
                {
                    sFlag = "E";
                    MaterialMasterExt();
                }
                //PROV-CCP-MM-941-23-0045 S
                else if (sTypeOfMassUpdm == "23")
                {
                    KinaxisUpdation();
                }
                //PROV-CCP-MM-941-23-0045 E
                objAccess.SaveMassSync(lblMasterHeaderId.Text, sstatus, lblUserId.Text, objUtil.GetIpAddress(), sFlag, true);
            }

        }
        catch (Exception ex)
        { _log.Error("btnSAPMassUpload_Click", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbackMsg_Click(object sender, EventArgs e)
    {

        MaterialMasterAccess objAccess = new MaterialMasterAccess();
        Utility objUtil = new Utility();
        String sstatus = "";
        DataSet ds;
        try
        {


            ds = objAccess.GetSyncMassData(lblMasterHeaderId.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
            }
            else
            {
                String sTypeOfMassUpdm = Session[StaticKeys.TypeOfMassUpdm].ToString();
                if (sTypeOfMassUpdm == "11")
                {
                    MRPUpdation();
                }
                else if (sTypeOfMassUpdm == "12")
                {
                    SelectionMethodUpdation();
                }
                else if (sTypeOfMassUpdm == "13")
                {
                    PlannedPriceUpdation();
                }
                else if (sTypeOfMassUpdm == "14")
                {
                    TaggingBOMRecipeUpdation();
                }
                else if (sTypeOfMassUpdm == "15")
                {
                    ProductionHierarchyUpdation();
                }
                else if (sTypeOfMassUpdm == "16")
                {
                    OtherFieldsUpdation();
                }
                else if (sTypeOfMassUpdm == "17")
                {
                    AUOMUpdation();
                }
                else if (sTypeOfMassUpdm == "18")
                {
                    TAXMUpdation();
                }
                else if (sTypeOfMassUpdm == "19")
                {
                    INSPUpdation();
                }
                else if (sTypeOfMassUpdm == "20")
                {
                    CLASSUpdation();
                }
                //PROV-CCP-MM-941-23-0045 S
                else if (sTypeOfMassUpdm == "23")
                {
                    KinaxisUpdation();
                }
                //PROV-CCP-MM-941-23-0045 E

                objAccess.SaveMassSync(lblMasterHeaderId.Text, sstatus, lblUserId.Text, objUtil.GetIpAddress(), "C", false);
            }

            Response.Redirect("MaterialMaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnbackMsg_Click", ex); }
    }

    /// <summary>
    /// MSC_8300001775
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnbackcanMsg_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialMaster.aspx", false);
    }

    /// <summary>
    /// PROV-CCP-MM-941-23-0045
    /// </summary>
    private void KinaxisUpdation()
    {
        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("KinaxisUpdation", ex);
            }


            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;
            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[16] {
                new DataColumn("Material_Code", typeof(string)),
                //new DataColumn("Material_Desc", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Kinaxis_SBU", typeof(string)),
                new DataColumn("Kinaxis_Market", typeof(string)),
                new DataColumn("Kinaxis_Selling_Country", typeof(string)),
                new DataColumn("Kinaxis_Business", typeof(string)),
                new DataColumn("Kinaxis_Division", typeof(string)),
                new DataColumn("Kinaxis_Therapy", typeof(string)),
                new DataColumn("Kinaxis_Dosage_Form", typeof(string)),
                new DataColumn("Kinaxis_Minimum_ShelfLife", typeof(string)),
                new DataColumn("Kinaxis_Marketing_Manager", typeof(string)),
                new DataColumn("Molecule_Details", typeof(string)),
                new DataColumn("Product_Group", typeof(string)),
                new DataColumn("Pack_Size", typeof(string)),
                new DataColumn("Strength_of_material", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_Kinaxis_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        // sqlBulkCopy.ColumnMappings.Add("Material_Desc", "Material_Desc");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_SBU", "Kinaxis_SBU");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Market", "Kinaxis_Market");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Selling_Country", "Kinaxis_Selling_Country");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Business", "Kinaxis_Business");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Division", "Kinaxis_Division");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Therapy", "Kinaxis_Therapy");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Dosage_Form", "Kinaxis_Dosage_Form");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Minimum_ShelfLife", "Kinaxis_Minimum_ShelfLife");
                        sqlBulkCopy.ColumnMappings.Add("Kinaxis_Marketing_Manager", "Kinaxis_Marketing_Manager");
                        sqlBulkCopy.ColumnMappings.Add("Molecule_Details", "Molecule_Details");
                        sqlBulkCopy.ColumnMappings.Add("Product_Group", "Product_Group");
                        sqlBulkCopy.ColumnMappings.Add("Pack_Size", "Pack_Size");
                        sqlBulkCopy.ColumnMappings.Add("Strength_of_material", "Strength_of_material");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("KinaxisUpdation", ex); }
    }

    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void MRPUpdation()
    {
        try
        {


            //Upload and save the file
            //Session[StaticKeys.RequestNo]   
            string sdate = "";
            try
            {
                DateTime date = System.DateTime.Now;
                sdate = date.ToString("dd/MM/yyyy");
                sdate = sdate.Replace(@"/", "");
                WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Start");
            }
            catch (Exception ex)
            {
                _log.Error("MRPUpdation", ex);

            }
            string StrPath = String.Empty;
            string extensionname = String.Empty;
            DataAccessLayer objDb = new DataAccessLayer();
            DataSet dstData = new DataSet();
            DocumentUploadAccess objDoc = new DocumentUploadAccess();
            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("MRPUpdation1", ex);
            }
            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;
            WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + StrPath + extensionname);

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03 
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + conString);
            conString = string.Format(conString, excelPath);
            WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + conString);

            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[37] {
                new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("MRP_Group", typeof(string)),
                new DataColumn("MRP_Type", typeof(string)),
                new DataColumn("MRP_Controller", typeof(string)),
                new DataColumn("Reorder_Point", typeof(string)),
                new DataColumn("Planning_time_fence", typeof(string)),
                new DataColumn("Lot_Size", typeof(string)),
                new DataColumn("Planning_Cycle", typeof(string)),
                new DataColumn("Minimum_Lot_Size", typeof(string)),
                new DataColumn("Maximum_Lot_Size", typeof(string)),
                new DataColumn("Fixed_Lot_Size", typeof(string)),
                new DataColumn("Maximum_Stock_Level", typeof(string)),
                new DataColumn("Rounding_Value", typeof(string)),
                new DataColumn("Storage_Cost_Indicator", typeof(string)),
                new DataColumn("Ordering_Costs", typeof(string)),
                new DataColumn("Procurement_Type", typeof(string)),
                new DataColumn("Special_Procurement_Type", typeof(string)),
                new DataColumn("Prod_Storage_Location", typeof(string)),
                new DataColumn("Quota_Arrangemen", typeof(string)),
                new DataColumn("Inhouse_Production_Time", typeof(string)),
                new DataColumn("Planned_Delivery_Time_In_Days", typeof(string)),

                new DataColumn("GR_Processing_Time", typeof(string)),
                new DataColumn("Scheduling_Margin_Key", typeof(string)),
                new DataColumn("Safety_Stock", typeof(string)),
                new DataColumn("Min_Safety_Stock", typeof(string)),
                new DataColumn("Coverage_Profile", typeof(string)),
                new DataColumn("Safety_Time_Ind", typeof(string)),
                new DataColumn("Safety_Time_Act_Cov_Days", typeof(string)),
                new DataColumn("Strategy_Group", typeof(string)),
                new DataColumn("Consumption_Mode", typeof(string)),
                new DataColumn("Forward_Consumption_Period", typeof(string)),
                new DataColumn("Backward_Consumption_Period", typeof(string)),
                new DataColumn("Mixed_MRP", typeof(string)),
                new DataColumn("MRP_Dep_Req", typeof(string)),
                new DataColumn("Individual_and_Coll_Reqmts", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))
            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;
                WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + lblMasterHeaderId.Text);

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Start" + dtExcelData);
                    oda.Fill(dtExcelData);
                    WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "End" + dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Satr TB");
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_MRP_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        sqlBulkCopy.ColumnMappings.Add("MRP_Group", "MRP_Group");
                        sqlBulkCopy.ColumnMappings.Add("MRP_Type", "MRP_Type");
                        sqlBulkCopy.ColumnMappings.Add("MRP_Controller", "MRP_Controller");
                        sqlBulkCopy.ColumnMappings.Add("Reorder_Point", "Reorder_Point");
                        sqlBulkCopy.ColumnMappings.Add("Planning_time_fence", "Planning_time_fence");
                        sqlBulkCopy.ColumnMappings.Add("Lot_Size", "Lot_Size_Mat_Plan");
                        sqlBulkCopy.ColumnMappings.Add("Planning_Cycle", "Planning_Cycle");
                        sqlBulkCopy.ColumnMappings.Add("Minimum_Lot_Size", "Min_Lot_Size");
                        sqlBulkCopy.ColumnMappings.Add("Maximum_Lot_Size", "Max_Lot_Size");
                        sqlBulkCopy.ColumnMappings.Add("Fixed_Lot_Size", "Fix_Lot_Size");
                        sqlBulkCopy.ColumnMappings.Add("Maximum_Stock_Level", "Max_Stock_Level");
                        sqlBulkCopy.ColumnMappings.Add("Rounding_Value", "Round_Value");
                        sqlBulkCopy.ColumnMappings.Add("Storage_Cost_Indicator", "Storage_Cost_Indicator");
                        sqlBulkCopy.ColumnMappings.Add("Ordering_Costs", "Ordering_Costs");
                        sqlBulkCopy.ColumnMappings.Add("Procurement_Type", "Proc_Type");
                        sqlBulkCopy.ColumnMappings.Add("Special_Procurement_Type", "Special_Proc_Type");//Special_Procurement_Type
                        sqlBulkCopy.ColumnMappings.Add("Prod_Storage_Location", "Prod_Stg_Loc");
                        sqlBulkCopy.ColumnMappings.Add("Quota_Arrangemen", "Quota_Arra");
                        sqlBulkCopy.ColumnMappings.Add("Inhouse_Production_Time", "Inh_Prod_Time");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Delivery_Time_In_Days", "Plan_Del_Time_Days");
                        sqlBulkCopy.ColumnMappings.Add("GR_Processing_Time", "GR_Proc_Time");
                        sqlBulkCopy.ColumnMappings.Add("Scheduling_Margin_Key", "Sch_Marg_Key");
                        sqlBulkCopy.ColumnMappings.Add("Safety_Stock", "Safety_Stock");
                        sqlBulkCopy.ColumnMappings.Add("Min_Safety_Stock", "Min_Safety_Stock");
                        sqlBulkCopy.ColumnMappings.Add("Coverage_Profile", "Cove_Profile");
                        sqlBulkCopy.ColumnMappings.Add("Safety_Time_Ind", "Safety_Time_Ind");
                        sqlBulkCopy.ColumnMappings.Add("Safety_Time_Act_Cov_Days", "Saf_Time_Act_Days");
                        sqlBulkCopy.ColumnMappings.Add("Strategy_Group", "Strategy_Group");
                        sqlBulkCopy.ColumnMappings.Add("Consumption_Mode", "Cons_Mode");
                        sqlBulkCopy.ColumnMappings.Add("Forward_Consumption_Period", "Fwd_Cons_Period");
                        sqlBulkCopy.ColumnMappings.Add("Backward_Consumption_Period", "Bwd_Cons_Period");
                        sqlBulkCopy.ColumnMappings.Add("Mixed_MRP", "Mixed_MRP");
                        sqlBulkCopy.ColumnMappings.Add("MRP_Dep_Req", "MRP_Dep_Req");
                        sqlBulkCopy.ColumnMappings.Add("Individual_and_Coll_Reqmts", "Ind_N_Coll_Req");

                        WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "End TB");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                        WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Done");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("MRPUpdationM", ex); }
    }
    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void SelectionMethodUpdation()
    {
        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {


            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("SelectionMethodUpdation1", ex);
            }


            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[4] {
                 new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Selection_Method", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_SMethod_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        sqlBulkCopy.ColumnMappings.Add("Selection_Method", "Selection_Method");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("SelectionMethodUpdation", ex); }
    }

    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void PlannedPriceUpdation()
    {
        try
        {


            //Upload and save the file
            //Session[StaticKeys.RequestNo]   
            string sdate = "";
            try
            {
                DateTime date = System.DateTime.Now;
                sdate = date.ToString("dd/MM/yyyy");
                sdate = sdate.Replace(@"/", "");
                WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Start");
            }
            catch (Exception ex)
            {
                _log.Error("PlannedPriceUpdation1", ex);
            }
            string StrPath = String.Empty;
            string extensionname = String.Empty;
            DataAccessLayer objDb = new DataAccessLayer();
            DataSet dstData = new DataSet();
            DocumentUploadAccess objDoc = new DocumentUploadAccess();
            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("PlannedPriceUpdation2", ex);
            }

            WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + StrPath + extensionname);

            //string StrPath = String.Empty;
            //if (Session[StaticKeys.TypeOfMassUpdm].ToString() == "1")
            //{
            //    StrPath = "~/Transaction/Material/MatChangeDoc/MRPExcelDoc/" + Session[StaticKeys.RequestNo].ToString() + "/";
            //}
            //string excelPath = Server.MapPath(StrPath) + Path.GetFileName(FileUpload1.PostedFile.FileName);

            //FileUpload1.SaveAs(excelPath);
            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;
            //string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;//"C:/Users/nitinrajeshirke/Documents/Nitin/MWT/MWT INDIA Final/MWT_INDIA/MT" + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03
                             //conString = ConfigurationManager.ConnectionStrings["StrPathC"].ConnectionString;
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + conString);

            conString = string.Format(conString, excelPath);
            WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + conString);

            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[9] {
                //new DataColumn("Material_code", typeof(string)), 
                //new DataColumn("Plant", typeof(string)),
                //new DataColumn("Planned_Price_1", typeof(string)),
                //new DataColumn("Planned_Price_1_date", typeof(string)),
                //new DataColumn("Planned_Price_2", typeof(string)),
                //new DataColumn("Planned_Price_2_date", typeof(string)),
                //new DataColumn("Planned_Price_3", typeof(string)),
                //new DataColumn("Planned_Price_3_date", typeof(string)),
                 new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Planned_Price_1", typeof(string)),
                new DataColumn("Planned_Price_1_date", typeof(string)),
                new DataColumn("Planned_Price_2", typeof(string)),
                new DataColumn("Planned_Price_2_date", typeof(string)),
                new DataColumn("Planned_Price_3", typeof(string)),
                new DataColumn("Planned_Price_3_date", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;
                WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "" + lblMasterHeaderId.Text);

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Start" + dtExcelData);
                    oda.Fill(dtExcelData);
                    WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "End" + dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Satr TB");
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_PPrice_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Number");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Id");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Price_1", "Planned_Price1");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Price_1_date", "Planned_Price_Date1");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Price_2", "Planned_Price2");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Price_2_date", "Planned_Price_Date2");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Price_3", "Planned_Price3");
                        sqlBulkCopy.ColumnMappings.Add("Planned_Price_3_date", "Planned_Price_Date3");
                        //sqlBulkCopy.ColumnMappings.Add("Material_code", "Material_Number");
                        //sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        //sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Id");
                        //sqlBulkCopy.ColumnMappings.Add("Planned_Price_1[2001.50]", "Planned_Price1");
                        //sqlBulkCopy.ColumnMappings.Add("Planned_Price_1_date[DD.MM.YYYY]", "Planned_Price_Date1");
                        //sqlBulkCopy.ColumnMappings.Add("Planned_Price_2[1.05]", "Planned_Price2");
                        //sqlBulkCopy.ColumnMappings.Add("Planned_Price_2_date[DD.MM.YYYY]", "Planned_Price_Date2");
                        //sqlBulkCopy.ColumnMappings.Add("Planned_Price_3[2.05]", "Planned_Price3");
                        //sqlBulkCopy.ColumnMappings.Add("Planned_Price_3_date[DD.MM.YYYY]", "Planned_Price_Date3");

                        WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "End TB");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                        WriteMassMatLog("CreateMassMatLog_" + sdate + ".txt", "Done");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("PlannedPriceUpdation", ex); }
    }

    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void TaggingBOMRecipeUpdation()
    {
        //Upload and save the file
        //Session[StaticKeys.RequestNo]   

        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {


            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("TaggingBOMRecipeUpdation1", ex);
            }

            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[7] {
                 new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Recipe_Group", typeof(string)),
                new DataColumn("Alternative_BOM", typeof(string)),
                new DataColumn("BOM_Usage", typeof(string)),
                new DataColumn("Costing_Lot_Size", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_TaggOfBOM_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        sqlBulkCopy.ColumnMappings.Add("Recipe_Group", "Recipe_Group");
                        sqlBulkCopy.ColumnMappings.Add("Alternative_BOM", "Alternative_BOM");
                        sqlBulkCopy.ColumnMappings.Add("BOM_Usage", "BOM_Usage");
                        sqlBulkCopy.ColumnMappings.Add("Costing_Lot_Size", "Costing_Lot_Size");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("TaggingBOMRecipeUpdation", ex); }
    }
    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void ProductionHierarchyUpdation()
    {
        //Upload and save the file
        //Session[StaticKeys.RequestNo]   

        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {


            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("ProductionHierarchyUpdation1", ex);
            }


            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;
            //string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[4] {
                 new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Product_Hierarchy", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_PHierarchy_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        sqlBulkCopy.ColumnMappings.Add("Product_Hierarchy", "Product_Hierarchy");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("ProductionHierarchyUpdation", ex); }
    }

    /// <summary>
    /// MSC_8300001775_DT160820
    /// </summary>
    private void OtherFieldsUpdation()
    {
        //Upload and save the file
        //Session[StaticKeys.RequestNo]   

        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {


            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("OtherFieldsUpdation1", ex);
            }


            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;
            //string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            switch (extension)
            {
                case ".xls": //Excel 97-03 
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[8] {
                 new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Storage_Loc", typeof(string)),
                new DataColumn("View_Name", typeof(string)),
                new DataColumn("Field_Name", typeof(string)),
                new DataColumn("Old_Value", typeof(string)),
                new DataColumn("New_Value", typeof(string)),
                new DataColumn("Master_Header_Id", typeof(string))

            });
                dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

                //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> '123456' and Material_Code is not null and Plant_Code is not null", excel_con))
                {
                    //oda.Fill(dtExcelData);
                    oda.Fill(dtExcelData);

                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_Other_TB";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
                        sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                        sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
                        sqlBulkCopy.ColumnMappings.Add("Storage_Loc", "Storage_Loc");
                        sqlBulkCopy.ColumnMappings.Add("View_Name", "View_Name");
                        sqlBulkCopy.ColumnMappings.Add("Field_Name", "Field_Name");
                        sqlBulkCopy.ColumnMappings.Add("Old_Value", "Old_Value");
                        sqlBulkCopy.ColumnMappings.Add("New_Value", "New_Value");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("OtherFieldsUpdation", ex); }
    }


    //private void AUOMUpdation()
    //{
    //    string StrPath = String.Empty;
    //    string extensionname = String.Empty;
    //    DataAccessLayer objDb = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    DocumentUploadAccess objDoc = new DocumentUploadAccess();
    //    try
    //    {
    //        dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
    //            extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }


    //    string extension = Path.GetExtension(extensionname).ToLower();
    //    string excelPath = StrPath;
    //    string conString = string.Empty;

    //    StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
    //                      StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
    //                       StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
    //            break;
    //    }
    //    conString = string.Format(conString, excelPath);
    //    using (OleDbConnection excel_con = new OleDbConnection(conString))
    //    {
    //        excel_con.Open();
    //        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //        DataTable dtExcelData = new DataTable();
    //        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //        dtExcelData.Columns.AddRange(new DataColumn[16] {
    //             new DataColumn("Material_Code", typeof(string)),
    //            new DataColumn("Plant_Code", typeof(string)),
    //            new DataColumn("Denominator", typeof(string)),
    //            new DataColumn("Alt_Unit_of_Measure", typeof(string)),
    //            new DataColumn("Numerator", typeof(string)),
    //            new DataColumn("Base_Unit_of_Measure", typeof(string)),
    //            new DataColumn("Gross_Weight", typeof(string)),
    //            new DataColumn("Weight_Unit", typeof(string)),
    //            new DataColumn("Unit_of_Weight_in_ISO", typeof(string)),
    //            new DataColumn("Length", typeof(string)),
    //            new DataColumn("Width", typeof(string)),
    //            new DataColumn("Height", typeof(string)),
    //            new DataColumn("Unit_of_Dimension_Length_Width_Height", typeof(string)),
    //            new DataColumn("Int_Article_No_EAN_UPC", typeof(string)),
    //            new DataColumn("Category_of_Int_Article_No_EAN", typeof(string)),
    //            new DataColumn("Master_Header_Id", typeof(string))

    //        });
    //        dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

    //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
    //        {
    //            oda.Fill(dtExcelData);
    //        }
    //        excel_con.Close();

    //        string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(consString))
    //        {
    //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //            {
    //                //Set the database table name
    //                sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_Mtun_TB";
    //                //[OPTIONAL]: Map the Excel columns with that of the database table
    //                sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
    //                sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
    //                sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
    //                sqlBulkCopy.ColumnMappings.Add("Denominator", "DENOMINATR");
    //                sqlBulkCopy.ColumnMappings.Add("Alt_Unit_of_Measure", "ALT_UNIT");
    //                sqlBulkCopy.ColumnMappings.Add("Numerator", "NUMERATOR");
    //                sqlBulkCopy.ColumnMappings.Add("Base_Unit_of_Measure", "BASIC_UOM");
    //                sqlBulkCopy.ColumnMappings.Add("Gross_Weight", "GROSS_WT");
    //                sqlBulkCopy.ColumnMappings.Add("Weight_Unit", "UNIT_OF_WT");
    //                sqlBulkCopy.ColumnMappings.Add("Unit_of_Weight_in_ISO", "UNIT_OF_WT_ISO");
    //                sqlBulkCopy.ColumnMappings.Add("Length", "LENGTH");
    //                sqlBulkCopy.ColumnMappings.Add("Width", "WIDTH");
    //                sqlBulkCopy.ColumnMappings.Add("Height", "HEIGHT");
    //                sqlBulkCopy.ColumnMappings.Add("Unit_of_Dimension_Length_Width_Height", "UNIT_DIM");
    //                sqlBulkCopy.ColumnMappings.Add("Int_Article_No_EAN_UPC", "EAN_UPC");
    //                sqlBulkCopy.ColumnMappings.Add("Category_of_Int_Article_No_EAN", "EAN_CAT");
    //                con.Open();
    //                sqlBulkCopy.WriteToServer(dtExcelData);
    //                con.Close();
    //            }
    //        }
    //    }
    //}


    private void AUOMUpdation()
    {
        try
        {


            string StrPath = String.Empty;
            string extensionname = String.Empty;
            DataAccessLayer objDb = new DataAccessLayer();
            DataSet dstData = new DataSet();
            DocumentUploadAccess objDoc = new DocumentUploadAccess();
            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("AUOMUpdation", ex);
            }

            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            int count = 0;
            try
            {
                SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

                System.Data.DataTable dt = ReadexcelAUOM(extension, StrPath);

                if (dt.Rows.Count > 2000)
                {

                    string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg" + msg);
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(msg);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
                else
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "Insert_Excel_T_Mat_Mass_Mtun_TB");
                    using (SqlCommand cmd = new SqlCommand("Insert_Excel_T_Mat_Mass_Mtun_TB"))
                    {
                        cmd.Connection = con;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (dt.Rows[i]["Material_Code"].ToString() != "" && dt.Rows[i]["Material_Code"].ToString() != "123456")
                            {
                                try
                                {
                                    cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Material_Code", dt.Rows[i]["Material_Code"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Plant_Code", dt.Rows[i]["Plant_Code"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Denominator", dt.Rows[i]["Denominator"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Alt_Unit_of_Measure", dt.Rows[i]["Alt_Unit_of_Measure"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Numerator", dt.Rows[i]["Numerator"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Base_Unit_of_Measure", dt.Rows[i]["Base_Unit_of_Measure"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Gross_Weight", dt.Rows[i]["Gross_Weight"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Weight_Unit", dt.Rows[i]["Weight_Unit"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Unit_of_Weight_in_ISO", dt.Rows[i]["Unit_of_Weight_in_ISO"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@LENGTH", dt.Rows[i]["Length"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@WIDTH", dt.Rows[i]["Width"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@HEIGHT", dt.Rows[i]["Height"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@UNIT_DIM", dt.Rows[i]["Unit_of_Dimension_Length_Width_Height"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@EAN_UPC", dt.Rows[i]["Int_Article_No_EAN_UP"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@EAN_CAT", dt.Rows[i]["Category_of_Int_Article_No_EAN"].ToString().Trim());

                                    SqlDataReader sdr = cmd.ExecuteReader();
                                    sdr.Close();

                                    cmd.Parameters.RemoveAt("@Master_Header_Id");
                                    cmd.Parameters.RemoveAt("@Material_Code");
                                    cmd.Parameters.RemoveAt("@Plant_Code");
                                    cmd.Parameters.RemoveAt("@Denominator");
                                    cmd.Parameters.RemoveAt("@Alt_Unit_of_Measure");
                                    cmd.Parameters.RemoveAt("@Numerator");
                                    cmd.Parameters.RemoveAt("@Base_Unit_of_Measure");
                                    cmd.Parameters.RemoveAt("@Gross_Weight");
                                    cmd.Parameters.RemoveAt("@Weight_Unit");
                                    cmd.Parameters.RemoveAt("@Unit_of_Weight_in_ISO");
                                    cmd.Parameters.RemoveAt("@LENGTH");
                                    cmd.Parameters.RemoveAt("@WIDTH");
                                    cmd.Parameters.RemoveAt("@HEIGHT");
                                    cmd.Parameters.RemoveAt("@UNIT_DIM");
                                    cmd.Parameters.RemoveAt("@EAN_UPC");
                                    cmd.Parameters.RemoveAt("@EAN_CAT");
                                    count += 1;
                                }
                                catch (Exception ex)
                                {
                                    _log.Error("AUOMUpdation1", ex);
                                    string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg1" + msg);
                                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    //sb.Append("<script type = 'text/javascript'>");
                                    //sb.Append("window.onload=function(){");
                                    //sb.Append("alert('");
                                    //sb.Append(msg);
                                    //sb.Append("')};");
                                    //sb.Append("</script>");
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                                }
                            }
                        }

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        if (count == 0 || count < dt.Rows.Count)
                        {
                            string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                            WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg2" + msg);
                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            //sb.Append("<script type = 'text/javascript'>");
                            //sb.Append("window.onload=function(){");
                            //sb.Append("alert('");
                            //sb.Append(msg);
                            //sb.Append("')};");
                            //sb.Append("</script>");
                            ////ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }
                        else
                        {
                            WriteMassMatLog("MatExtMass" + sdate + ".txt", "count" + count.ToString());
                            // lblcount.Text = count.ToString() + " Records Submitted into System. PDF will generate after successfull validation.";
                        }
                    }


                }


                try
                {

                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
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
                {
                    _log.Error("AUOMUpdation1", ex);

                }

            }
            catch (Exception ex)
            {
                _log.Error("AUOMUpdation2", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("AUOMUpdation0", ex); }
    }

    public System.Data.DataTable ReadexcelAUOM(string ext, string path)
    {
        //try
        //{
        //}
        //catch (Exception ex)
        //{ _log.Error("ReadexcelAUOM", ex); }
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Readexcel Start");
        string ConStr = "";
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ext" + ext);
        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "path" + path);
        //string query = "SELECT * FROM [Sheet1$]";
        string query = "SELECT * FROM [ConvirsionFactor$]";
        //string query = "SELECT * FROM [Sheet1$] where Material_Code is not null and Plant_Code is not null";
        //string query = "select * from [Sheet1$] where Material_Code is not null and Plant_Code is not null";
        //string query = "select Material_Code,Plant_Code from [Sheet1$]";
        //string query = "SELECT Material_Code,Plant_Code,Storage_Loc,Sales_Org,Dist_Channel, Ref_Material_Code, Ref_Plant_Code,Ref_Storage_Loc,Ref_Sales_Org,Ref_Dist_Channel, Tax_Data, Mat_Statistics_Grp,Mat_Pricing, Acc_Assignment_Grp, Item_Category_Grp, Availability_Check, Transportation_Grp, Loading_Grp,Profit_Center,Purchasing_Grp, Control_Code,MRP_Type, Reorder_Point,MRP_Controller, Lot_Size, Minimum_Lot_Size, Maximum_Lot_Size, Fixed_Lot_Size, Maximum_Stock_Level, Rounding_Value, Special_Procurement_Type, Prod_Storage_Location,Inhouse_Production_Time,Planned_Delivery_Time_In_Days,GR_Processing_Time, Scheduling_Margin_Key,Safety_Stock, Min_Safety_Stock, Coverage_Profile, Safety_Time,QM_Control_Key,Certificate_Type, Inspection_Type, Interval_next_periodic, Valuation_Class, Price_Control from [Sheet1$]";
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "query" + query);

        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);

        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl");
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + ds.Tables[0].Rows.Count);
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Material_Code" + ds.Tables[0].Rows[0]["Material_Code"].ToString());
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "lblMasterHeaderId" + lblMasterHeaderId.Text.Trim());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "foreach");

                if (i == 0)
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "c");
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 Start");
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 End");
                    }
                }
                else
                {
                    //WriteMassMatLog("MatExtMass" + sdate + ".txt", "dr1");
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 Start");
                        dr1[j] = dr.ItemArray[j].ToString();
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 End");
                    }
                    dt.Rows.Add(dr1);

                }
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + i);
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;
        }


    }

    private void TAXMUpdation()
    {

        try
        {

            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Start");


            string StrPath = String.Empty;
            string extensionname = String.Empty;
            DataAccessLayer objDb = new DataAccessLayer();
            DataSet dstData = new DataSet();
            DocumentUploadAccess objDoc = new DocumentUploadAccess();
            try
            {
                dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                    extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                _log.Error("TAXMUpdation1", ex);

            }


            string extension = Path.GetExtension(extensionname).ToLower();
            string excelPath = StrPath;
            string conString = string.Empty;

            StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
            int count = 0;
            try
            {
                SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

                System.Data.DataTable dt = ReadexcelTax(extension, StrPath);

                if (dt.Rows.Count > 2000)
                {

                    string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg" + msg);
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(msg);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
                else
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "Insert_Excel_T_Mat_Mass_Taxm_TB");
                    using (SqlCommand cmd = new SqlCommand("Insert_Excel_T_Mat_Mass_Taxm_TB"))
                    {
                        cmd.Connection = con;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (dt.Rows[i]["Material_Code"].ToString() != "" && dt.Rows[i]["Material_Code"].ToString() != "123456")
                            {
                                try
                                {
                                    cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Material_Code", dt.Rows[i]["Material_Code"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Plant_Code", dt.Rows[i]["Plant_Code"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@DEPCOUNTRY", dt.Rows[i]["Departure_Country"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@TAX_TYPE_1", dt.Rows[i]["Tax_Category"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@TAXCLASS_1", dt.Rows[i]["Tax_Classification_Material"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@TAX_IND", dt.Rows[i]["Tax_Indicator_Purchasing"].ToString().Trim());
                                    SqlDataReader sdr = cmd.ExecuteReader();
                                    sdr.Close();

                                    cmd.Parameters.RemoveAt("@Master_Header_Id");
                                    cmd.Parameters.RemoveAt("@Material_Code");
                                    cmd.Parameters.RemoveAt("@Plant_Code");
                                    cmd.Parameters.RemoveAt("@DEPCOUNTRY");
                                    cmd.Parameters.RemoveAt("@TAX_TYPE_1");
                                    cmd.Parameters.RemoveAt("@TAXCLASS_1");
                                    cmd.Parameters.RemoveAt("@TAX_IND");


                                    count += 1;
                                }
                                catch (Exception ex)
                                {
                                    _log.Error("TAXMUpdation2", ex);


                                    string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg1" + msg);
                                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    //sb.Append("<script type = 'text/javascript'>");
                                    //sb.Append("window.onload=function(){");
                                    //sb.Append("alert('");
                                    //sb.Append(msg);
                                    //sb.Append("')};");
                                    //sb.Append("</script>");
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                                }
                            }
                        }

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        if (count == 0 || count < dt.Rows.Count)
                        {
                            string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                            WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg2" + msg);
                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            //sb.Append("<script type = 'text/javascript'>");
                            //sb.Append("window.onload=function(){");
                            //sb.Append("alert('");
                            //sb.Append(msg);
                            //sb.Append("')};");
                            //sb.Append("</script>");
                            ////ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }
                        else
                        {
                            WriteMassMatLog("MatExtMass" + sdate + ".txt", "count" + count.ToString());
                            // lblcount.Text = count.ToString() + " Records Submitted into System. PDF will generate after successfull validation.";
                        }
                    }


                }


                try
                {

                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                    foreach (System.Diagnostics.Process p in process)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            try
                            {
                                p.Kill();
                            }
                            catch (Exception ex) { _log.Error("TAXMUpdation11", ex); }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("TAXMUpdation", ex);
                }

            }
            catch (Exception ex)
            {
                _log.Error("TAXMUpdation", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("TAXMUpdationm", ex); }
    }

    public System.Data.DataTable ReadexcelTax(string ext, string path)
    {
        //try
        //{


        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Readexcel Start");
        string ConStr = "";
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ext" + ext);
        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "path" + path);
        //string query = "SELECT * FROM [Sheet1$]";
        string query = "SELECT * FROM [Tax$]";
        //string query = "SELECT * FROM [Sheet1$] where Material_Code is not null and Plant_Code is not null";
        //string query = "select * from [Sheet1$] where Material_Code is not null and Plant_Code is not null";
        //string query = "select Material_Code,Plant_Code from [Sheet1$]";
        //string query = "SELECT Material_Code,Plant_Code,Storage_Loc,Sales_Org,Dist_Channel, Ref_Material_Code, Ref_Plant_Code,Ref_Storage_Loc,Ref_Sales_Org,Ref_Dist_Channel, Tax_Data, Mat_Statistics_Grp,Mat_Pricing, Acc_Assignment_Grp, Item_Category_Grp, Availability_Check, Transportation_Grp, Loading_Grp,Profit_Center,Purchasing_Grp, Control_Code,MRP_Type, Reorder_Point,MRP_Controller, Lot_Size, Minimum_Lot_Size, Maximum_Lot_Size, Fixed_Lot_Size, Maximum_Stock_Level, Rounding_Value, Special_Procurement_Type, Prod_Storage_Location,Inhouse_Production_Time,Planned_Delivery_Time_In_Days,GR_Processing_Time, Scheduling_Margin_Key,Safety_Stock, Min_Safety_Stock, Coverage_Profile, Safety_Time,QM_Control_Key,Certificate_Type, Inspection_Type, Interval_next_periodic, Valuation_Class, Price_Control from [Sheet1$]";
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "query" + query);
        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl");
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + ds.Tables[0].Rows.Count);
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Material_Code" + ds.Tables[0].Rows[0]["Material_Code"].ToString());
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "lblMasterHeaderId" + lblMasterHeaderId.Text.Trim());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "foreach");

                if (i == 0)
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "c");
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 Start");
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 End");
                    }
                }
                else
                {
                    //WriteMassMatLog("MatExtMass" + sdate + ".txt", "dr1");
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 Start");
                        dr1[j] = dr.ItemArray[j].ToString();
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 End");
                    }
                    dt.Rows.Add(dr1);

                }
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + i);
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;

        }
        //}
        //catch (Exception ex)
        //{ _log.Error("ReadexcelTax", ex); }
    }


    //private void INSPUpdation()
    //{
    //    string StrPath = String.Empty;
    //    string extensionname = String.Empty;
    //    DataAccessLayer objDb = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    DocumentUploadAccess objDoc = new DocumentUploadAccess();
    //    try
    //    {
    //        dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
    //            extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }


    //    string extension = Path.GetExtension(extensionname).ToLower();
    //    string excelPath = StrPath;
    //    string conString = string.Empty;

    //    StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
    //                      StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
    //                       StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
    //            break;
    //    }
    //    conString = string.Format(conString, excelPath);
    //    using (OleDbConnection excel_con = new OleDbConnection(conString))
    //    {
    //        excel_con.Open();
    //        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //        DataTable dtExcelData = new DataTable();
    //        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //        dtExcelData.Columns.AddRange(new DataColumn[6] {
    //             new DataColumn("Material_Code", typeof(string)),
    //            new DataColumn("Plant_Code", typeof(string)),
    //            new DataColumn("Inspection_Type", typeof(string)),
    //             new DataColumn("Preferred_Inspection_Type", typeof(string)),
    //              new DataColumn("Insp_Type_Mat_Combination_Active", typeof(string)),
    //            new DataColumn("Master_Header_Id", typeof(string))

    //        });
    //        dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

    //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
    //        {
    //            oda.Fill(dtExcelData);
    //        }
    //        excel_con.Close();

    //        string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(consString))
    //        {
    //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //            {
    //                //Set the database table name
    //                sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_Insp_TB";
    //                //[OPTIONAL]: Map the Excel columns with that of the database table
    //                sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
    //                sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
    //                sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
    //                sqlBulkCopy.ColumnMappings.Add("Inspection_Type", "INSP_SETUP");
    //                sqlBulkCopy.ColumnMappings.Add("Preferred_Inspection_Type", "PRE_INSP_TYP");
    //                sqlBulkCopy.ColumnMappings.Add("Insp_Type_Mat_Combination_Active", "AKTIV");
    //                con.Open();
    //                sqlBulkCopy.WriteToServer(dtExcelData);
    //                con.Close();
    //            }
    //        }
    //    }
    //}

    private void INSPUpdation()
    {
        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
            _log.Error("INSPUpdation", ex);
        }

        string extension = Path.GetExtension(extensionname).ToLower();
        string excelPath = StrPath;
        string conString = string.Empty;

        StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
        int count = 0;
        try
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            System.Data.DataTable dt = ReadexcelInsp(extension, StrPath);

            if (dt.Rows.Count > 2000)
            {

                string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";
                _log.Info(msg);
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append("alert('");
                //sb.Append(msg);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }
            else
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Insert_Excel_T_Mat_Mass_Insp_TB");
                using (SqlCommand cmd = new SqlCommand("Insert_Excel_T_Mat_Mass_Insp_TB"))
                {
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (dt.Rows[i]["Material_Code"].ToString() != "" && dt.Rows[i]["Material_Code"].ToString() != "123456")
                        {
                            try
                            {
                                cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
                                cmd.Parameters.AddWithValue("@Material_Code", dt.Rows[i]["Material_Code"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@Plant_Code", dt.Rows[i]["Plant_Code"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@INSP_SETUP", dt.Rows[i]["Inspection_Type"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@PRE_INSP_TYP", dt.Rows[i]["Preferred_Inspection_Type"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@AKTIV", dt.Rows[i]["Insp_Type_Mat_Combination_Active"].ToString().Trim());
                                SqlDataReader sdr = cmd.ExecuteReader();
                                sdr.Close();

                                cmd.Parameters.RemoveAt("@Master_Header_Id");
                                cmd.Parameters.RemoveAt("@Material_Code");
                                cmd.Parameters.RemoveAt("@Plant_Code");
                                cmd.Parameters.RemoveAt("@INSP_SETUP");
                                cmd.Parameters.RemoveAt("@PRE_INSP_TYP");
                                cmd.Parameters.RemoveAt("@AKTIV");

                                count += 1;
                            }
                            catch (Exception ex)
                            {
                                _log.Error("INSPUpdation1", ex);
                                string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                                WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg1" + msg);
                                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                //sb.Append("<script type = 'text/javascript'>");
                                //sb.Append("window.onload=function(){");
                                //sb.Append("alert('");
                                //sb.Append(msg);
                                //sb.Append("')};");
                                //sb.Append("</script>");
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                            }
                        }
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (count == 0 || count < dt.Rows.Count)
                    {
                        string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg2" + msg);
                        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        //sb.Append("<script type = 'text/javascript'>");
                        //sb.Append("window.onload=function(){");
                        //sb.Append("alert('");
                        //sb.Append(msg);
                        //sb.Append("')};");
                        //sb.Append("</script>");
                        ////ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                    else
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "count" + count.ToString());
                        // lblcount.Text = count.ToString() + " Records Submitted into System. PDF will generate after successfull validation.";
                    }
                }


            }


            try
            {

                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                foreach (System.Diagnostics.Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch (Exception ex) { _log.Error("INSPUpdation2", ex); }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("INSPUpdation3", ex);
            }

        }
        catch (Exception ex)
        {
            _log.Error("INSPUpdation4", ex);
        }
    }

    public System.Data.DataTable ReadexcelInsp(string ext, string path)
    {
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Readexcel Start");
        string ConStr = "";
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        string query = "SELECT * FROM [InspectionType$]";
        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + ds.Tables[0].Rows.Count);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "foreach");

                if (i == 0)
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "c");
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 Start");
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 End");
                    }
                }
                else
                {
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        dr1[j] = dr.ItemArray[j].ToString();
                    }
                    dt.Rows.Add(dr1);

                }
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + i);
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;
        }
    }


    //private void CLASSUpdation()
    //{
    //    string StrPath = String.Empty;
    //    string extensionname = String.Empty;
    //    DataAccessLayer objDb = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    DocumentUploadAccess objDoc = new DocumentUploadAccess();
    //    try
    //    {
    //        dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
    //            extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }


    //    string extension = Path.GetExtension(extensionname).ToLower();
    //    string excelPath = StrPath;
    //    string conString = string.Empty;

    //    StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
    //                      StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
    //                       StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
    //            break;
    //    }
    //    conString = string.Format(conString, excelPath);
    //    using (OleDbConnection excel_con = new OleDbConnection(conString))
    //    {
    //        excel_con.Open();
    //        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //        DataTable dtExcelData = new DataTable();
    //        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //        dtExcelData.Columns.AddRange(new DataColumn[7] {
    //             new DataColumn("Material_Code", typeof(string)),
    //            new DataColumn("Plant_Code", typeof(string)),
    //            new DataColumn("Class_Type", typeof(string)),
    //            new DataColumn("Class_Number", typeof(string)),
    //            new DataColumn("Internal_Characteristic", typeof(string)),
    //            new DataColumn("Characteristic_Value", typeof(string)),
    //            new DataColumn("Master_Header_Id", typeof(string))

    //        });
    //        dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

    //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
    //        {
    //            oda.Fill(dtExcelData);
    //        }
    //        excel_con.Close();

    //        string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(consString))
    //        {
    //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //            {
    //                //Set the database table name
    //                sqlBulkCopy.DestinationTableName = "dbo.T_Mat_Mass_Class_TB";
    //                //[OPTIONAL]: Map the Excel columns with that of the database table
    //                sqlBulkCopy.ColumnMappings.Add("Material_Code", "Material_Code");
    //                sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
    //                sqlBulkCopy.ColumnMappings.Add("Plant_Code", "Plant_Code");
    //                sqlBulkCopy.ColumnMappings.Add("Class_Type", "CLASS_TYPE");
    //                sqlBulkCopy.ColumnMappings.Add("Class_Number", "CLASS_NO");
    //                sqlBulkCopy.ColumnMappings.Add("Internal_Characteristic", "KEY1");
    //                sqlBulkCopy.ColumnMappings.Add("Characteristic_Value", "VALUE");
    //                con.Open();
    //                sqlBulkCopy.WriteToServer(dtExcelData);
    //                con.Close();
    //            }
    //        }
    //    }
    //}

    private void CLASSUpdation()
    {
        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
            _log.Error("CLASSUpdation", ex);
        }

        string extension = Path.GetExtension(extensionname).ToLower();
        string excelPath = StrPath;
        string conString = string.Empty;

        StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
        int count = 0;
        try
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            System.Data.DataTable dt = ReadexcelClass(extension, StrPath);

            if (dt.Rows.Count > 2000)
            {

                string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";
                _log.Info(msg);
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append("alert('");
                //sb.Append(msg);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }
            else
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Insert_Excel_T_Mat_Mass_Class_TB");
                using (SqlCommand cmd = new SqlCommand("Insert_Excel_T_Mat_Mass_Class_TB"))
                {
                    cmd.Connection = con;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (dt.Rows[i]["Material_Code"].ToString() != "" && dt.Rows[i]["Material_Code"].ToString() != "123456")
                        {
                            try
                            {
                                cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
                                cmd.Parameters.AddWithValue("@Material_Code", dt.Rows[i]["Material_Code"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@Plant_Code", dt.Rows[i]["Plant_Code"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@CLASS_TYPE", dt.Rows[i]["Class_Type"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@CLASS_NO", dt.Rows[i]["Class_Number"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@KEY1", dt.Rows[i]["Internal_Characteristic"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@VALUE", dt.Rows[i]["Characteristic_Value"].ToString().Trim());
                                SqlDataReader sdr = cmd.ExecuteReader();
                                sdr.Close();

                                cmd.Parameters.RemoveAt("@Master_Header_Id");
                                cmd.Parameters.RemoveAt("@Material_Code");
                                cmd.Parameters.RemoveAt("@Plant_Code");
                                cmd.Parameters.RemoveAt("@CLASS_TYPE");
                                cmd.Parameters.RemoveAt("@CLASS_NO");
                                cmd.Parameters.RemoveAt("@KEY1");
                                cmd.Parameters.RemoveAt("@VALUE");


                                count += 1;
                            }
                            catch (Exception ex)
                            {
                                _log.Error("CLASSUpdation11", ex);
                                string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                                WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg1" + msg);
                                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                //sb.Append("<script type = 'text/javascript'>");
                                //sb.Append("window.onload=function(){");
                                //sb.Append("alert('");
                                //sb.Append(msg);
                                //sb.Append("')};");
                                //sb.Append("</script>");
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                            }
                        }
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    if (count == 0 || count < dt.Rows.Count)
                    {
                        string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg2" + msg);
                        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        //sb.Append("<script type = 'text/javascript'>");
                        //sb.Append("window.onload=function(){");
                        //sb.Append("alert('");
                        //sb.Append(msg);
                        //sb.Append("')};");
                        //sb.Append("</script>");
                        ////ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                    else
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "count" + count.ToString());
                        // lblcount.Text = count.ToString() + " Records Submitted into System. PDF will generate after successfull validation.";
                    }
                }


            }


            try
            {

                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                foreach (System.Diagnostics.Process p in process)
                {
                    if (!string.IsNullOrEmpty(p.ProcessName))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch (Exception ex) { _log.Error("CLASSUpdation10", ex); }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("CLASSUpdation11", ex);
            }

        }
        catch (Exception ex)
        {
            _log.Error("CLASSUpdation12", ex);
        }
    }

    public System.Data.DataTable ReadexcelClass(string ext, string path)
    {
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Readexcel Start");
        string ConStr = "";
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        string query = "SELECT * FROM [Classification$]";
        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + ds.Tables[0].Rows.Count);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "foreach");

                if (i == 0)
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "c");
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 Start");
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 End");
                    }
                }
                else
                {
                    //WriteMassMatLog("MatExtMass" + sdate + ".txt", "dr1");
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 Start");
                        dr1[j] = dr.ItemArray[j].ToString();
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 End");
                    }
                    dt.Rows.Add(dr1);

                }
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + i);
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;
        }
    }


    /// <summary>
    /// MSE_8300002156
    /// </summary>
    private void MaterialMasterExtOldRunning()
    {
        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
            _log.Error("MaterialMasterExt", ex);
        }


        string extension = Path.GetExtension(extensionname).ToLower();
        string excelPath = StrPath;
        string conString = string.Empty;

        StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
        switch (extension)
        {
            case ".xls": //Excel 97-03
                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                          StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                break;
            case ".xlsx": //Excel 07 or higher
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                break;
        }
        conString = string.Format(conString, excelPath);
        using (OleDbConnection excel_con = new OleDbConnection(conString))
        {
            excel_con.Open();
            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            DataTable dtExcelData = new DataTable();
            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[34] {
                 new DataColumn("Master_Header_Id", typeof(string)),
                new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Storage_Loc", typeof(string)),
                new DataColumn("Sales_Org", typeof(string)),
                new DataColumn("Dist_Channel", typeof(string)),
                new DataColumn("Ref_Material_Code", typeof(string)),
                new DataColumn("Ref_Plant_Code", typeof(string)),
                new DataColumn("Ref_Storage_Loc", typeof(string)),
                new DataColumn("Ref_Sales_Org", typeof(string)),
                new DataColumn("Ref_Dist_Channel", typeof(string)),

                new DataColumn("Mat_Statistics_Grp", typeof(string)),
                new DataColumn("Mat_Pricing", typeof(string)),
                new DataColumn("Acc_Assignment_Grp", typeof(string)),
                new DataColumn("Item_Category_Grp", typeof(string)),

                //new DataColumn("Availability_Check", typeof(string)),
                //new DataColumn("Transportation_Grp", typeof(string)),
                //new DataColumn("Loading_Grp", typeof(string)),

                new DataColumn("Profit_Center", typeof(string)),
                new DataColumn("Purchasing_Grp", typeof(string)),

                //new DataColumn("Control_Code", typeof(string)),
                
                new DataColumn("MRP_Type", typeof(string)),
                new DataColumn("MRP_Controller", typeof(string)),
                new DataColumn("Lot_Size", typeof(string)),
                new DataColumn("Minimum_Lot_Size", typeof(string)),
                new DataColumn("Maximum_Lot_Size", typeof(string)),
                new DataColumn("Fixed_Lot_Size", typeof(string)),

                new DataColumn("Rounding_Value", typeof(string)),

                //new DataColumn("Prod_Storage_Location", typeof(string)),

                new DataColumn("Inhouse_Production_Time", typeof(string)),
                new DataColumn("Planned_Delivery_Time_In_Days", typeof(string)),

                //new DataColumn("GR_Processing_Time", typeof(string)),
                //new DataColumn("Scheduling_Margin_Key", typeof(string)),

                new DataColumn("Safety_Stock", typeof(string)),
                new DataColumn("Min_Safety_Stock", typeof(string)),
                new DataColumn("Coverage_Profile", typeof(string)),

                new DataColumn("Inspection_Type", typeof(string)),

                //new DataColumn("Interval_next_periodic", typeof(string)),
                //new DataColumn("QM_Proc_Active", typeof(string)),

                new DataColumn("QM_Control_Key", typeof(string)),
                new DataColumn("Certificate_Type", typeof(string)),

                new DataColumn("Valuation_Class", typeof(string)),
                new DataColumn("Price_Control", typeof(string))

                //new DataColumn("HRKFT", typeof(string)),
                //new DataColumn("KOSGR", typeof(string)),
                 

            });
            dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
            {
                oda.Fill(dtExcelData);
            }
            excel_con.Close();

            string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.T_MASS_EXTENSION_TB";
                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                    sqlBulkCopy.ColumnMappings.Add("Material_Code", "MATNR");
                    sqlBulkCopy.ColumnMappings.Add("Plant_Code", "WERKS");
                    sqlBulkCopy.ColumnMappings.Add("Storage_Loc", "LGORT");
                    sqlBulkCopy.ColumnMappings.Add("Sales_Org", "VKORG");
                    sqlBulkCopy.ColumnMappings.Add("Dist_Channel", "VTWEG");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Material_Code", "MATNR_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Plant_Code", "WERKS_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Storage_Loc", "LGORT_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Sales_Org", "VKORG_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Dist_Channel", "VTWEG_R");

                    sqlBulkCopy.ColumnMappings.Add("Mat_Statistics_Grp", "VERSG");
                    sqlBulkCopy.ColumnMappings.Add("Mat_Pricing", "KONDM");
                    sqlBulkCopy.ColumnMappings.Add("Acc_Assignment_Grp", "KTGRM");
                    sqlBulkCopy.ColumnMappings.Add("Item_Category_Grp", "MTPOS");

                    //sqlBulkCopy.ColumnMappings.Add("Availability_Check", "");
                    //sqlBulkCopy.ColumnMappings.Add("Transportation_Grp", "PRCTR");
                    //sqlBulkCopy.ColumnMappings.Add("Loading_Grp", "EKGRP");

                    sqlBulkCopy.ColumnMappings.Add("Profit_Center", "PRCTR");
                    sqlBulkCopy.ColumnMappings.Add("Purchasing_Grp", "EKGRP");

                    sqlBulkCopy.ColumnMappings.Add("MRP_Type", "DISMM");

                    sqlBulkCopy.ColumnMappings.Add("MRP_Controller", "DISPO");
                    sqlBulkCopy.ColumnMappings.Add("Lot_Size", "DISLS");
                    sqlBulkCopy.ColumnMappings.Add("Minimum_Lot_Size", "BSTMI");
                    sqlBulkCopy.ColumnMappings.Add("Maximum_Lot_Size", "BSTMA");
                    sqlBulkCopy.ColumnMappings.Add("Fixed_Lot_Size", "BSTFE");

                    sqlBulkCopy.ColumnMappings.Add("Rounding_Value", "BSTRF");

                    //sqlBulkCopy.ColumnMappings.Add("Prod_Storage_Location", "BSTRF");

                    sqlBulkCopy.ColumnMappings.Add("Inhouse_Production_Time", "DZEIT");
                    sqlBulkCopy.ColumnMappings.Add("Planned_Delivery_Time_In_Days", "PLIFZ");

                    //sqlBulkCopy.ColumnMappings.Add("GR_Processing_Time", "");
                    //sqlBulkCopy.ColumnMappings.Add("Scheduling_Margin_Key", "");

                    sqlBulkCopy.ColumnMappings.Add("Safety_Stock", "EISBE");
                    sqlBulkCopy.ColumnMappings.Add("Min_Safety_Stock", "EISLO");
                    sqlBulkCopy.ColumnMappings.Add("Coverage_Profile", "RWPRO");

                    sqlBulkCopy.ColumnMappings.Add("Inspection_Type", "ART");
                    //sqlBulkCopy.ColumnMappings.Add("Interval_next_periodic", "");
                    //sqlBulkCopy.ColumnMappings.Add("QM_Proc_Active", "");
                    sqlBulkCopy.ColumnMappings.Add("QM_Control_Key", "SSQSS");

                    sqlBulkCopy.ColumnMappings.Add("Certificate_Type", "QZGTP");
                    sqlBulkCopy.ColumnMappings.Add("Valuation_Class", "BKLAS");
                    sqlBulkCopy.ColumnMappings.Add("Price_Control", "VPRSV");
                    //sqlBulkCopy.ColumnMappings.Add("HRKFT", "");
                    //sqlBulkCopy.ColumnMappings.Add("KOSGR", "");  
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    con.Close();
                }
            }
        }
    }

    private void MaterialMasterExtNew()
    {

        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");

        }
        catch (Exception ex)
        {
            _log.Error("MaterialMasterExt", ex);
        }
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Start");


        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
            _log.Error("MaterialMasterExtNew", ex);
        }

        WriteMassMatLog("MatExtMass" + sdate + ".txt", "StrPath" + StrPath);
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "extensionname" + extensionname);

        string extension = Path.GetExtension(extensionname).ToLower();
        string excelPath = StrPath;
        string conString = string.Empty;

        StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
        switch (extension)
        {
            case ".xls": //Excel 97-03
                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                          StrPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                break;
            case ".xlsx": //Excel 07 or higher
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           StrPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                break;
        }
        conString = string.Format(conString, excelPath);

        WriteMassMatLog("MatExtMass" + sdate + ".txt", "conString" + conString);
        using (OleDbConnection excel_con = new OleDbConnection(conString))
        {
            excel_con.Open();
            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

            WriteMassMatLog("MatExtMass" + sdate + ".txt", "sheet1" + sheet1);
            DataTable dtExcelData = new DataTable();
            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[47] {
                 new DataColumn("Master_Header_Id", typeof(string)),
                new DataColumn("Material_Code", typeof(string)),
                new DataColumn("Plant_Code", typeof(string)),
                new DataColumn("Storage_Loc", typeof(string)),
                new DataColumn("Sales_Org", typeof(string)),
                new DataColumn("Dist_Channel", typeof(string)),
                new DataColumn("Ref_Material_Code", typeof(string)),
                new DataColumn("Ref_Plant_Code", typeof(string)),
                new DataColumn("Ref_Storage_Loc", typeof(string)),
                new DataColumn("Ref_Sales_Org", typeof(string)),
                new DataColumn("Ref_Dist_Channel", typeof(string)),
                new DataColumn("Tax_Data", typeof(string)),
                new DataColumn("Mat_Statistics_Grp", typeof(string)),
                new DataColumn("Mat_Pricing", typeof(string)),
                new DataColumn("Acc_Assignment_Grp", typeof(string)),
                new DataColumn("Item_Category_Grp", typeof(string)),
                new DataColumn("Availability_Check", typeof(string)),
                new DataColumn("Transportation_Grp", typeof(string)),
                new DataColumn("Loading_Grp", typeof(string)),
                new DataColumn("Profit_Center", typeof(string)),
                new DataColumn("Purchasing_Grp", typeof(string)),
                new DataColumn("Control_Code", typeof(string)),
                new DataColumn("MRP_Type", typeof(string)),
                new DataColumn("Reorder_Point", typeof(string)),
                new DataColumn("MRP_Controller", typeof(string)),
                new DataColumn("Lot_Size", typeof(string)),
                new DataColumn("Minimum_Lot_Size", typeof(string)),
                new DataColumn("Maximum_Lot_Size", typeof(string)),
                new DataColumn("Fixed_Lot_Size", typeof(string)),
                new DataColumn("Maximum_Stock_Level", typeof(string)),
                new DataColumn("Rounding_Value", typeof(string)),
                new DataColumn("Special_Procurement_Type", typeof(string)),
                new DataColumn("Prod_Storage_Location", typeof(string)),
                new DataColumn("Inhouse_Production_Time", typeof(string)),
                new DataColumn("Planned_Delivery_Time_In_Days", typeof(string)),
                new DataColumn("GR_Processing_Time", typeof(string)),
                new DataColumn("Scheduling_Margin_Key", typeof(string)),
                new DataColumn("Safety_Stock", typeof(string)),
                new DataColumn("Min_Safety_Stock", typeof(string)),
                new DataColumn("Coverage_Profile", typeof(string)),
                new DataColumn("Safety_Time", typeof(string)),
                new DataColumn("QM_Control_Key", typeof(string)),
                new DataColumn("Certificate_Type", typeof(string)),
                new DataColumn("Inspection_Type", typeof(string)),
                new DataColumn("Interval_next_periodic", typeof(string)),
                new DataColumn("Valuation_Class", typeof(string)),
                new DataColumn("Price_Control", typeof(string))
            });
            dtExcelData.Columns["Master_Header_Id"].DefaultValue = lblMasterHeaderId.Text;

            WriteMassMatLog("MatExtMass" + sdate + ".txt", "lblMasterHeaderId.Text" + lblMasterHeaderId.Text);

            WriteMassMatLog("MatExtMass" + sdate + ".txt", "dtExcelData" + dtExcelData);
            ////using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Master_Header_Id,Material_Code,Plant_Code,LEFT(Storage_Loc, CHARINDEX('-', Storage_Loc)-1),LEFT(Sales_Org, CHARINDEX('-', Sales_Org)-1),LEFT(Dist_Channel, CHARINDEX('-', Dist_Channel)-1),Ref_Material_Code,Ref_Plant_Code,LEFT(Ref_Storage_Loc, CHARINDEX('-', Ref_Storage_Loc)-1),LEFT(Ref_Sales_Org, CHARINDEX('-', Ref_Sales_Org)-1),LEFT(Ref_Dist_Channel, CHARINDEX('-', Ref_Dist_Channel)-1),Tax_Data,Mat_Statistics_Grp,LEFT(Mat_Pricing, CHARINDEX('-', Mat_Pricing)-1),Acc_Assignment_Grp,Item_Category_Grp,Availability_Check,Transportation_Grp,Loading_Grp,LEFT(Profit_Center, CHARINDEX('-', Profit_Center)-1),LEFT(Purchasing_Grp, CHARINDEX('-', Purchasing_Grp)-1),Control_Code,LEFT(MRP_Type, CHARINDEX('-', MRP_Type)-1),Reorder_Point,LEFT(MRP_Controller, CHARINDEX('-', MRP_Controller)-1),LEFT(Lot_Size, CHARINDEX('-', Lot_Size)-1),Minimum_Lot_Size,Maximum_Lot_Size,Fixed_Lot_Size,Maximum_Stock_Level,Rounding_Value,Special_Procurement_Type,Prod_Storage_Location,Inhouse_Production_Time,Planned_Delivery_Time_In_Days,GR_Processing_Time,Scheduling_Margin_Key,Safety_Stock,Min_Safety_Stock,Coverage_Profile,Safety_Time,LEFT(QM_Control_Key, CHARINDEX('-', QM_Control_Key)-1),Certificate_Type,Inspection_Type,Interval_next_periodic,Valuation_Class,Price_Control FROM[" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))

            ////using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Master_Header_Id,Material_Code,SUBSTRING(Storage_Loc, 1, 4) as 'Storage_Loc' FROM[" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
            ////using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Master_Header_Id,Material_Code,Plant_Code FROM[" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))

            //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where  Material_Code <> 123456 and Material_Code is not null and Plant_Code is not null", excel_con))
            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [MatExtension$] where Material_Code is not null and Plant_Code is not null", excel_con))
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "dtExcelData Start");
                oda.Fill(dtExcelData);
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "dtExcelData End");
            }
            excel_con.Close();
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "excel_con Close");
            string consString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "consString Open");
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.T_MASS_EXTENSION_TB";
                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Master_Header_Id", "Master_Header_Id");
                    sqlBulkCopy.ColumnMappings.Add("Material_Code", "MATNR");
                    sqlBulkCopy.ColumnMappings.Add("Plant_Code", "WERKS");
                    sqlBulkCopy.ColumnMappings.Add("Storage_Loc", "LGORT");
                    sqlBulkCopy.ColumnMappings.Add("Sales_Org", "VKORG");
                    sqlBulkCopy.ColumnMappings.Add("Dist_Channel", "VTWEG");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Material_Code", "MATNR_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Plant_Code", "WERKS_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Storage_Loc", "LGORT_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Sales_Org", "VKORG_R");
                    sqlBulkCopy.ColumnMappings.Add("Ref_Dist_Channel", "VTWEG_R");

                    sqlBulkCopy.ColumnMappings.Add("Tax_Data", "TAXDATA");//New

                    sqlBulkCopy.ColumnMappings.Add("Mat_Statistics_Grp", "VERSG");
                    sqlBulkCopy.ColumnMappings.Add("Mat_Pricing", "KONDM");
                    sqlBulkCopy.ColumnMappings.Add("Acc_Assignment_Grp", "KTGRM");
                    sqlBulkCopy.ColumnMappings.Add("Item_Category_Grp", "MTPOS");

                    //sqlBulkCopy.ColumnMappings.Add("Availability_Check", "MTVFP"); //New Move
                    //sqlBulkCopy.ColumnMappings.Add("Transportation_Grp", "TRAGR"); //New Move
                    //sqlBulkCopy.ColumnMappings.Add("Loading_Grp", "LADGR"); //New Move

                    sqlBulkCopy.ColumnMappings.Add("Profit_Center", "PRCTR");
                    sqlBulkCopy.ColumnMappings.Add("Purchasing_Grp", "EKGRP");


                    //sqlBulkCopy.ColumnMappings.Add("Control_Code", "STEUC"); //New move

                    sqlBulkCopy.ColumnMappings.Add("MRP_Type", "DISMM");

                    sqlBulkCopy.ColumnMappings.Add("Reorder_Point", "MINBE"); //New

                    sqlBulkCopy.ColumnMappings.Add("MRP_Controller", "DISPO");
                    sqlBulkCopy.ColumnMappings.Add("Lot_Size", "DISLS");
                    sqlBulkCopy.ColumnMappings.Add("Minimum_Lot_Size", "BSTMI");
                    sqlBulkCopy.ColumnMappings.Add("Maximum_Lot_Size", "BSTMA");
                    sqlBulkCopy.ColumnMappings.Add("Fixed_Lot_Size", "BSTFE");

                    sqlBulkCopy.ColumnMappings.Add("Maximum_Stock_Level", "MABST"); //New

                    sqlBulkCopy.ColumnMappings.Add("Rounding_Value", "BSTRF");

                    sqlBulkCopy.ColumnMappings.Add("Special_Procurement_Type", "SOBSL"); //New
                    sqlBulkCopy.ColumnMappings.Add("Prod_Storage_Location", "LGPRO"); //New 
                    sqlBulkCopy.ColumnMappings.Add("Planned_Delivery_Time_In_Days", "PLIFZ");
                    sqlBulkCopy.ColumnMappings.Add("Inhouse_Production_Time", "DZEIT");


                    //sqlBulkCopy.ColumnMappings.Add("GR_Processing_Time", "WEBAZ"); //New Move
                    //sqlBulkCopy.ColumnMappings.Add("Scheduling_Margin_Key", "FHORI"); //New Move 

                    sqlBulkCopy.ColumnMappings.Add("Safety_Stock", "EISBE");
                    sqlBulkCopy.ColumnMappings.Add("Min_Safety_Stock", "EISLO");
                    sqlBulkCopy.ColumnMappings.Add("Coverage_Profile", "RWPRO");

                    sqlBulkCopy.ColumnMappings.Add("Safety_Time", "SHZET"); //New 

                    sqlBulkCopy.ColumnMappings.Add("QM_Control_Key", "SSQSS");
                    sqlBulkCopy.ColumnMappings.Add("Certificate_Type", "QZGTP");
                    sqlBulkCopy.ColumnMappings.Add("Inspection_Type", "ART");

                    sqlBulkCopy.ColumnMappings.Add("Interval_next_periodic", "PRFRQ");//New  

                    sqlBulkCopy.ColumnMappings.Add("Valuation_Class", "BKLAS");
                    sqlBulkCopy.ColumnMappings.Add("Price_Control", "VPRSV");

                    sqlBulkCopy.ColumnMappings.Add("Transportation_Grp", "TRAGR"); //New Move
                    sqlBulkCopy.ColumnMappings.Add("Loading_Grp", "LADGR"); //New Move

                    sqlBulkCopy.ColumnMappings.Add("Scheduling_Margin_Key", "FHORI"); //New Move
                    sqlBulkCopy.ColumnMappings.Add("Control_Code", "STEUC"); //New move
                    sqlBulkCopy.ColumnMappings.Add("GR_Processing_Time", "WEBAZ"); //New Move

                    sqlBulkCopy.ColumnMappings.Add("Availability_Check", "MTVFP"); //New Move

                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    con.Close();
                }
            }

            WriteMassMatLog("MatExtMass" + sdate + ".txt", "consString Open");
        }
    }

    private void MaterialMasterExt()
    {


        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Start");


        string StrPath = String.Empty;
        string extensionname = String.Empty;
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();
        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                StrPath = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Path"].ToString());
                extensionname = Convert.ToString(dstData.Tables[0].Rows[0]["Document_Name"].ToString());
            }
        }
        catch (Exception ex)
        {
            _log.Error("MaterialMasterExt1", ex);
        }

        WriteMassMatLog("MatExtMass" + sdate + ".txt", "StrPath" + StrPath);
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "extensionname" + extensionname);

        string extension = Path.GetExtension(extensionname).ToLower();
        string excelPath = StrPath;
        string conString = string.Empty;

        StrPath = Convert.ToString(ConfigurationManager.AppSettings["StrPathC"]) + StrPath;
        int count = 0;
        //try
        //{
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "StrPath" + StrPath);
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        WriteMassMatLog("MatExtMass" + sdate + ".txt", "con" + con);


        System.Data.DataTable dt = ReadexcelExt(extension, StrPath);

        if (dt.Rows.Count > 2000)
        {

            string msg = "Maximum data upload limit exceeded, please upload 2000 or less records";
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg" + msg);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(msg);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
        else
        {
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Insert_Excel_MassExt_Data");
            using (SqlCommand cmd = new SqlCommand("Insert_Excel_MassExt_Data"))
            {
                cmd.Connection = con;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["Material_Code"].ToString() != "")
                    {
                        try
                        {
                            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "MATNR" + dt.Rows[i]["Material_Code"].ToString().Trim());

                            cmd.Parameters.AddWithValue("@Master_Header_Id", lblMasterHeaderId.Text.Trim());
                            cmd.Parameters.AddWithValue("@MATNR", dt.Rows[i]["Material_Code"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@WERKS", dt.Rows[i]["Plant_Code"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@LGORT", dt.Rows[i]["Storage_Loc"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@VKORG", dt.Rows[i]["Sales_Org"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@VTWEG", dt.Rows[i]["Dist_Channel"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@MATNR_R", dt.Rows[i]["Ref_Material_Code"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@WERKS_R", dt.Rows[i]["Ref_Plant_Code"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@LGORT_R", dt.Rows[i]["Ref_Storage_Loc"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@VKORG_R", dt.Rows[i]["Ref_Sales_Org"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@VTWEG_R", dt.Rows[i]["Ref_Dist_Channel"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@TAXDATA", dt.Rows[i]["Tax_Data"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@VERSG", dt.Rows[i]["Mat_Statistics_Grp"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@KONDM", dt.Rows[i]["Mat_Pricing"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@KTGRM", dt.Rows[i]["Acc_Assignment_Grp"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@MTPOS", dt.Rows[i]["Item_Category_Grp"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@PRCTR", dt.Rows[i]["Profit_Center"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@EKGRP", dt.Rows[i]["Purchasing_Grp"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@DISMM", dt.Rows[i]["MRP_Type"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@MINBE", dt.Rows[i]["Reorder_Point"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@DISPO", dt.Rows[i]["MRP_Controller"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@DISLS", dt.Rows[i]["Lot_Size"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@BSTMI", dt.Rows[i]["Minimum_Lot_Size"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@BSTMA", dt.Rows[i]["Maximum_Lot_Size"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@BSTFE", dt.Rows[i]["Fixed_Lot_Size"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@MABST", dt.Rows[i]["Maximum_Stock_Level"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@BSTRF", dt.Rows[i]["Rounding_Value"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@SOBSL", dt.Rows[i]["Special_Procurement_Type"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@LGPRO", dt.Rows[i]["Prod_Storage_Location"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@PLIFZ", dt.Rows[i]["Planned_Delivery_Time_In_Days"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@DZEIT", dt.Rows[i]["Inhouse_Production_Time"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@EISBE", dt.Rows[i]["Safety_Stock"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@EISLO", dt.Rows[i]["Min_Safety_Stock"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@RWPRO", dt.Rows[i]["Coverage_Profile"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@SHZET", dt.Rows[i]["Safety_Time"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@SSQSS", dt.Rows[i]["QM_Control_Key"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@QZGTP", dt.Rows[i]["Certificate_Type"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@ART", dt.Rows[i]["Inspection_Type"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@PRFRQ", dt.Rows[i]["Interval_next_periodic"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@BKLAS", dt.Rows[i]["Valuation_Class"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@VPRSV", dt.Rows[i]["Price_Control"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@TRAGR", dt.Rows[i]["Transportation_Grp"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@LADGR", dt.Rows[i]["Loading_Grp"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@FHORI", dt.Rows[i]["Scheduling_Margin_Key"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@STEUC", dt.Rows[i]["Control_Code"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@WEBAZ", dt.Rows[i]["GR_Processing_Time"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@MTVFP", dt.Rows[i]["Availability_Check"].ToString().Trim());

                            SqlDataReader sdr = cmd.ExecuteReader();
                            sdr.Close();

                            cmd.Parameters.RemoveAt("@Master_Header_Id");
                            cmd.Parameters.RemoveAt("@MATNR");
                            cmd.Parameters.RemoveAt("@WERKS");
                            cmd.Parameters.RemoveAt("@LGORT");
                            cmd.Parameters.RemoveAt("@VKORG");
                            cmd.Parameters.RemoveAt("@VTWEG");
                            cmd.Parameters.RemoveAt("@MATNR_R");
                            cmd.Parameters.RemoveAt("@WERKS_R");
                            cmd.Parameters.RemoveAt("@LGORT_R");
                            cmd.Parameters.RemoveAt("@VKORG_R");
                            cmd.Parameters.RemoveAt("@VTWEG_R");
                            cmd.Parameters.RemoveAt("@TAXDATA");
                            cmd.Parameters.RemoveAt("@VERSG");
                            cmd.Parameters.RemoveAt("@KONDM");
                            cmd.Parameters.RemoveAt("@KTGRM");
                            cmd.Parameters.RemoveAt("@MTPOS");
                            cmd.Parameters.RemoveAt("@PRCTR");
                            cmd.Parameters.RemoveAt("@EKGRP");
                            cmd.Parameters.RemoveAt("@DISMM");
                            cmd.Parameters.RemoveAt("@MINBE");
                            cmd.Parameters.RemoveAt("@DISPO");
                            cmd.Parameters.RemoveAt("@DISLS");
                            cmd.Parameters.RemoveAt("@BSTMI");
                            cmd.Parameters.RemoveAt("@BSTMA");
                            cmd.Parameters.RemoveAt("@BSTFE");
                            cmd.Parameters.RemoveAt("@MABST");
                            cmd.Parameters.RemoveAt("@BSTRF");
                            cmd.Parameters.RemoveAt("@SOBSL");
                            cmd.Parameters.RemoveAt("@LGPRO");
                            cmd.Parameters.RemoveAt("@PLIFZ");
                            cmd.Parameters.RemoveAt("@DZEIT");
                            cmd.Parameters.RemoveAt("@EISBE");
                            cmd.Parameters.RemoveAt("@EISLO");
                            cmd.Parameters.RemoveAt("@RWPRO");
                            cmd.Parameters.RemoveAt("@SHZET");
                            cmd.Parameters.RemoveAt("@SSQSS");
                            cmd.Parameters.RemoveAt("@QZGTP");
                            cmd.Parameters.RemoveAt("@ART");
                            cmd.Parameters.RemoveAt("@PRFRQ");
                            cmd.Parameters.RemoveAt("@BKLAS");
                            cmd.Parameters.RemoveAt("@VPRSV");
                            cmd.Parameters.RemoveAt("@TRAGR");
                            cmd.Parameters.RemoveAt("@LADGR");
                            cmd.Parameters.RemoveAt("@FHORI");
                            cmd.Parameters.RemoveAt("@STEUC");
                            cmd.Parameters.RemoveAt("@WEBAZ");
                            cmd.Parameters.RemoveAt("@MTVFP");




                            count += 1;
                        }
                        catch (Exception ex)
                        {
                            string msg = "Incorrect header name / sequence /Column Data Type, kindly refer input format on form &re - upload.";
                            WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg1" + msg);
                            _log.Error("MaterialMasterExt22", ex);
                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            //sb.Append("<script type = 'text/javascript'>");
                            //sb.Append("window.onload=function(){");
                            //sb.Append("alert('");
                            //sb.Append(msg);
                            //sb.Append("')};");
                            //sb.Append("</script>");
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (count == 0 || count < dt.Rows.Count)
                {
                    string msg = "Incorrect header name / sequence, kindly refer input format on form &re - upload.";
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "msg2" + msg);
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(msg);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    ////ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
                else
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "count" + count.ToString());
                    // lblcount.Text = count.ToString() + " Records Submitted into System. PDF will generate after successfull validation.";
                }
            }


        }


        try
        {

            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
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
        {
            _log.Error("MaterialMasterExt1Kill", ex);
        }

        //}
        //catch (Exception ex)
        //{
        //}
    }


    public System.Data.DataTable ReadexcelExt(string ext, string path)
    {
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "Readexcel Start");
        string ConStr = "";
        if (ext.Trim() == ".xls")
        {
            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
        }
        else if (ext.Trim() == ".xlsx")
        {
            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        }

        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ext" + ext);
        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "path" + path);
        //string query = "SELECT * FROM [Sheet1$]";
        string query = "SELECT * FROM [MAT_EXTN$]";
        //string query = "SELECT * FROM [Sheet1$] where Material_Code is not null and Plant_Code is not null";
        //string query = "select * from [Sheet1$] where Material_Code is not null and Plant_Code is not null";
        //string query = "select Material_Code,Plant_Code from [Sheet1$]";
        //string query = "SELECT Material_Code,Plant_Code,Storage_Loc,Sales_Org,Dist_Channel, Ref_Material_Code, Ref_Plant_Code,Ref_Storage_Loc,Ref_Sales_Org,Ref_Dist_Channel, Tax_Data, Mat_Statistics_Grp,Mat_Pricing, Acc_Assignment_Grp, Item_Category_Grp, Availability_Check, Transportation_Grp, Loading_Grp,Profit_Center,Purchasing_Grp, Control_Code,MRP_Type, Reorder_Point,MRP_Controller, Lot_Size, Minimum_Lot_Size, Maximum_Lot_Size, Fixed_Lot_Size, Maximum_Stock_Level, Rounding_Value, Special_Procurement_Type, Prod_Storage_Location,Inhouse_Production_Time,Planned_Delivery_Time_In_Days,GR_Processing_Time, Scheduling_Margin_Key,Safety_Stock, Min_Safety_Stock, Coverage_Profile, Safety_Time,QM_Control_Key,Certificate_Type, Inspection_Type, Interval_next_periodic, Valuation_Class, Price_Control from [Sheet1$]";
        WriteMassMatLog("MatExtMass" + sdate + ".txt", "query" + query);
        OleDbConnection conn = new OleDbConnection(ConStr);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand cmd = new OleDbCommand(query, conn);
        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        {
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl");
            DataSet ds = new DataSet();
            da.Fill(ds, "Excel_tbl");

            System.Data.DataTable dt = new System.Data.DataTable();
            int i = 0;
            WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + ds.Tables[0].Rows.Count);
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "Material_Code" + ds.Tables[0].Rows[0]["Material_Code"].ToString());
            //WriteMassMatLog("MatExtMass" + sdate + ".txt", "lblMasterHeaderId" + lblMasterHeaderId.Text.Trim());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "foreach");

                if (i == 0)
                {
                    WriteMassMatLog("MatExtMass" + sdate + ".txt", "c");
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 Start");
                        dt.Columns.Add(dr.ItemArray[j].ToString());
                        //dt.Columns.Add((dr.ItemArray[j].ToString().Trim()).Split('-').First());
                        WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray1 End");
                    }
                }
                else
                {
                    //WriteMassMatLog("MatExtMass" + sdate + ".txt", "dr1");
                    DataRow dr1 = dt.NewRow();
                    int c = dr.ItemArray.Count();
                    for (int j = 0; j <= c - 1; j++)
                    {
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 Start");
                        //dr1[j] = dr.ItemArray[j].ToString();
                        dr1[j] = (dr.ItemArray[j].ToString().Split('-').First());
                        //WriteMassMatLog("MatExtMass" + sdate + ".txt", "ItemArray2 End");
                    }
                    dt.Rows.Add(dr1);

                }
                WriteMassMatLog("MatExtMass" + sdate + ".txt", "Excel_tbl" + i);
                i = i + 1;
            }

            conn.Close();
            da.Dispose();
            conn.Dispose();
            cmd.Dispose();

            return dt;
        }
    }

    private void IRFDivisionListMet()
    {
        try
        {
            BasicDataAccess ObjBasicDataAccess = new BasicDataAccess();
            DataSet ds;
            ds = ObjBasicDataAccess.GetIRFDivisionList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string sValNew = Convert.ToString(row["Division_ID"]);
                    IRFDivision.Add(sValNew);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13") && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "171" || Session[StaticKeys.SelectedModuleId].ToString() == "145"))
            {
                //Added By Nitin R
                IRFDivisionListMet();
                //Added By Nitin R

                if (IRFDivision.Contains(Session[StaticKeys.SelectedDivision].ToString()))
                {
                    // Get a ClientScriptManager reference from the Page class.
                    ClientScriptManager cs = Page.ClientScript;

                    // Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                    {
                        cs.RegisterStartupScript(GetType(), "key", "ShowDptExtnsnPopup();", true);
                    }
                }
                else
                {
                    //SPNAIRCR_SDT05122019 Added by NR
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                    {
                        // Get a ClientScriptManager reference from the Page class.
                        ClientScriptManager cs = Page.ClientScript;

                        // Check to see if the startup script is already registered.
                        if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                        {
                            cs.RegisterStartupScript(GetType(), "key", "ShowApprovePopup();", true);
                        }
                    }//SPNAIRCR_SDT05122019 Added by NR
                    else
                    {
                        SubmitRequest();
                    }

                }
            }
            else
            {
                //SPNAIRCR_SDT05122019 Added by NR
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                {
                    // Get a ClientScriptManager reference from the Page class.
                    ClientScriptManager cs = Page.ClientScript;

                    // Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(GetType(), "PopupScript"))
                    {
                        cs.RegisterStartupScript(GetType(), "key", "ShowApprovePopup();", true);
                    }
                }//SPNAIRCR_SDT05122019 Added by NR
                else
                {
                    SubmitRequest();
                }
            }
        }
        catch (Exception ex)
        { _log.Error("btnSubmit_Click", ex); }
    }

    /// <summary>
    /// SPNAIRCR_SDT05122019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApproveRemarks_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitRequest(Utility.RemoveSpecialChar(txtApproveNote.Text));
        }
        catch (Exception ex)
        { _log.Error("btnApproveRemarks_Click", ex); }
    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
            if (RollbackRequest())
            {
                Response.Redirect("materialmaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (SafeTypeHandling.ConvertStringToInt32(lblMassRequestProcessId.Text) > 0)
            {
                Response.Redirect("MaterialMassProcess.aspx", false);
            }
            else
            {
                Response.Redirect("Materialmaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }
    }

    protected void btnMDMCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("BasicData1.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnMDMCancel_Click", ex); }
    }

    protected void btnMDMApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitRequest();
        }
        catch (Exception ex)
        { _log.Error("btnMDMApprove_Click", ex); }
    }

    private bool RollbackRequest()
    {
        MaterialMasterAccess objAccess = new MaterialMasterAccess();
        Utility objUtil = new Utility();
        bool flg = false;

        string strReject = "";

        try
        {
            if (lblRejectionType.Text == "M")
            {
                flg = true;
                for (int i = 0; i < ChkRejectTo.Items.Count; i++)
                {
                    if (ChkRejectTo.Items[i].Selected)
                    {
                        if (strReject == "")
                            strReject = ChkRejectTo.Items[i].Value;
                        else
                            strReject += "," + ChkRejectTo.Items[i].Value;


                    }
                }
            }
            else
            {
                strReject = ddlRejectTo.SelectedValue;
            }

            if (objAccess.RollbackRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectNote.Text), lblUserId.Text) > 0)
            {
                flg = true;
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-2);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _log.Error("RollbackRequest", ex);
            //throw ex;
        }
        return flg;
    }

    public bool IsValidURL(string url)
    {
        string str = "";
        try
        {
            if ((url != null) && (url != string.Empty))
            {
                string[] strArray = url.Substring(0, url.ToString().Length - 1).Split(new char[] { '/' });
                str = strArray[strArray.Length - 1];
            }
        }
        catch (Exception ex)
        { _log.Error("IsValidURL", ex); }
        return (str != string.Empty);
    }

    private void ReadSectionTabs(string userId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        StringBuilder strBuilder = new StringBuilder();

        //string currentPageSeq = Request.QueryString["pgseq"].ToString();
        string currentSectionId = ((Label)ContentPlaceHolder1.FindControl("lblSectionId")).Text;
        //string currentSectionId = Request.QueryString["sid"].ToString();
        string sectionId = string.Empty;
        string ActionType = Session[StaticKeys.ActionType].ToString();

        string selectedModuleID = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg;
        if (ActionType == "C")
            flg = false;
        else
            flg = true;

        bool flg2 = true;
        int sectionStatus = 0;
        try
        {
            dstData = userAccess.ReadSectionTabs(userId, departmentId, lblMasterHeaderId.Text);

            //dstData = userAccess.ReadSectionTabs(userId, departmentId, moduleId);

            strBuilder.Append("<table cellspacing='0' width='100%'>");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                strBuilder.Append("<tr><td class='navigationBox'>");
                sectionId = row["Section_Id"].ToString();
                sectionStatus = materialMasterAccess.CheckSectionStatus(sectionId, lblMasterHeaderId.Text);

                if (sectionStatus <= 0 && ActionType != "C")
                {
                    flg = false;
                }
                else if (sectionStatus > 0 && ActionType == "C")
                {
                    flg = true;
                }

                //else if (sectionStatus > 0 && ActionType != "C" && (Convert.ToInt32(selectedModuleID) == 162))
                //{
                //    flg = false;
                //}

                if (row["Section_Id"].ToString() != currentSectionId)
                {
                    if (sectionStatus > 0)
                    {
                        if (Convert.ToInt32(selectedModuleID) == 162 || Convert.ToInt32(selectedModuleID) == 164)
                        {
                            if (row["Section_Id"].ToString() == "3" || row["Section_Id"].ToString() == "12" || row["Section_Id"].ToString() == "8" || row["Section_Id"].ToString() == "13" || row["Section_Id"].ToString() == "14" || row["Section_Id"].ToString() == "16" || row["Section_Id"].ToString() == "51")
                            {
                                strBuilder.Append("<a href='" + row["Page_URL"] + "' class='ROHStatus'>" + row["Section_Name"] + "</a>");
                            }
                            else
                            {
                                strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                            }
                        }
                        else if (Convert.ToInt32(selectedModuleID) == 139 || Convert.ToInt32(selectedModuleID) == 144 || Convert.ToInt32(selectedModuleID) == 145 || Convert.ToInt32(selectedModuleID) == 171)
                        {
                            if (row["Section_Id"].ToString() == "3" || row["Section_Id"].ToString() == "12" || row["Section_Id"].ToString() == "8" || row["Section_Id"].ToString() == "13" || row["Section_Id"].ToString() == "14" || row["Section_Id"].ToString() == "16" || row["Section_Id"].ToString() == "10" || row["Section_Id"].ToString() == "1" || row["Section_Id"].ToString() == "21" || row["Section_Id"].ToString() == "7")
                            {
                                strBuilder.Append("<a href='" + row["Page_URL"] + "' class='ROHStatus'>" + row["Section_Name"] + "</a>");
                            }
                            else
                            {
                                strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                            }
                        }
                        else
                        {
                            strBuilder.Append("<a href='" + row["Page_URL"] + "' class='greenStatus'>" + row["Section_Name"] + "</a>");
                        }

                    }
                    else
                    {
                        strBuilder.Append("<a href='" + row["Page_URL"] + "' >" + row["Section_Name"] + "</a>");
                    }
                }
                else if (row["Section_Id"].ToString() == currentSectionId)
                {
                    strBuilder.Append("<a href='" + row["Page_URL"] + "' class='Active'>" + row["Section_Name"] + "</a>");
                }


                strBuilder.Append("</td></tr>");
            }

            if (ActionType == "C")
            {
                //strBuilder.Append("<tr><td class='NoteWhiteBackGround'><b><u>Note :</u></b>");
                //strBuilder.Append("<br />1. Enter '#' as Old/ New Value to denote Blank.");
                //strBuilder.Append("<br />2. Save only those sections that needs to be changed.");
                //strBuilder.Append("</td></tr>");
            }
            strBuilder.Append(" </table>");
            litTab.Text = strBuilder.ToString();

            trSideMenuTab.Width = "0px";

            if ((flg) && (flg2))
            {
                if (SafeTypeHandling.ConvertStringToInt32(lblMassRequestProcessId.Text) == 0)
                {
                    btnSubmit.Enabled = true;
                    if (!btnRejectTo.Visible && (Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "R" || Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B") && (Session[StaticKeys.Mode].ToString() == "M" || Session[StaticKeys.Mode].ToString() == "N"))
                    {
                        Type cstype = this.GetType();

                        //Get a ClientScriptManager reference from the Page class.
                        ClientScriptManager cs = Page.ClientScript;

                        //Check to see if the startup script is already registered.
                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            //String cstext = "alert('Please click on Submit to send the request for processing.');";
                            //String cstext = "if(confirm('Proceed for submiting?')){RequestSubmitPage();};";
                            //String cstext = "if(confirm('Proceed for submiting? Click cancel to continue editing')){RequestSubmitPage();};";
                            String cstext = "if(!confirm('Click OK to further modify current request.\\n Click CANCEL to proceed for Submit / Mass Submit\\n \\n [MASS SUBMIT will club similar requests and send a single requests to the approver.]')){RequestSubmitPage();};";
                            //String cstext = "if(confirm('You are about to delete 5 rows. \nWARNING: Strawberry cakes won\'t be effected!)){RequestSubmitPage();};";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
                    }
                }
                //MSC_8300001775 Start
                MaterialMasterAccess objAccess = new MaterialMasterAccess();
                string userDeptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string moduleIds = Session[StaticKeys.SelectedModuleId].ToString();
                string MasterHeaderIds = Session[StaticKeys.MasterHeaderId].ToString();
                //string MaterialNos = Session[StaticKeys.MaterialNo].ToString();
                bool bproceed = false;
                bproceed = objAccess.IsUserInitiator(moduleIds, userDeptId, lblUserId.Text, MasterHeaderIds);
                if (bproceed == false)
                {
                    if ((Session[StaticKeys.Mode].ToString() != "V") && (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0"))
                    {
                        btnProceedToSub.Enabled = true;
                        btnProceedToSub.Visible = true;
                    }
                    else
                    {
                        btnProceedToSub.Enabled = false;
                        btnProceedToSub.Visible = false;
                    }
                }
                else
                {
                    btnProceedToSub.Enabled = false;
                    btnProceedToSub.Visible = false;
                }
                //MSC_8300001775 End
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ReadSectionTabs", ex);
            //throw;
        }

    }

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        try
        {
            DataSet ds = materialMasterAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);

            ddlRejectTo.DataSource = ds;
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();

            ChkRejectTo.DataSource = ds;
            ChkRejectTo.DataTextField = "LevelName";
            ChkRejectTo.DataValueField = "Sequence";
            ChkRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadDeparmentListForRollback", ex);
            //throw ex;
        }
    }

    private void SubmitRequest(string remarks = "")
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = sdate.Replace(@"/", "");
            WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "Start");
        }
        catch (Exception ex)
        {
            _log.Error("SubmitRequest", ex);
        }

        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        bool appflg = true;
        bool appflgk = true;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "Start Dep " + Session[StaticKeys.LoggedIn_User_DeptId].ToString() + "lblUserId" + lblUserId.Text + "SelectedModuleId_" + Session[StaticKeys.SelectedModuleId].ToString());
                //if (materialMasterAccess.ApproveRequest(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "3" && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171") && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                {
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V Start// " + lblMasterHeaderId.Text + "_" + lblUserId.Text);

                    //  DataSet dstData = materialMasterAccess.Get_IsHSNGSTFilled(lblMasterHeaderId.Text, lblUserId.Text);
                    //if (dstData.Tables[0].Rows.Count > 0)
                    try
                    {
                        if (MaterialMasterAccess.Get_IsHSNGSTFilled(lblMasterHeaderId.Text, lblUserId.Text))
                        {
                            appflg = true;
                        }
                        else
                        { appflg = false; }
                    }
                    catch (Exception ex) { _log.Error("SubmitRequest11_0", ex); }

                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V End// " + appflg);
                }
                else if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "8" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27") && (Session[StaticKeys.SelectedModuleId].ToString() == "144") && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                {//PROV-CCP-MM-941-23-0045 Start
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V Start// " + lblMasterHeaderId.Text + "_" + lblUserId.Text);

                    try
                    {
                        if (MaterialMasterAccess.Get_IsKinaxisFilled(lblMasterHeaderId.Text, lblUserId.Text))
                        {
                            //appflg = true;
                            appflgk = true;
                        }
                        else
                        {
                            //appflg = false;
                            appflgk = false;
                        }
                    }
                    catch (Exception ex) { _log.Error("SubmitRequest11_0", ex); }

                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V End// " + appflg);
                    //PROV-CCP-MM-941-23-0045 End
                }

                else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0" && (Session[StaticKeys.SelectedModuleId].ToString() == "195") && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                {
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V0 // " + lblMasterHeaderId.Text + "_" + lblUserId.Text);
                    try
                    {
                        if (MaterialMasterAccess.Get_IsHSNGSTFilled(lblMasterHeaderId.Text, lblUserId.Text))
                        {
                            appflg = true;
                        }
                        else
                        {
                            appflg = false;
                        }
                    }
                    catch (Exception ex) { _log.Error("SubmitRequest11_1", ex); }
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V1 // " + appflg + "_" + Session[StaticKeys.LoggedIn_User_DeptId].ToString());
                }
                if (appflg == true && appflgk == true)
                {
                    //PROV-CCP-MM-941-23-0045 Start
                    //if (&& appflgk == true) in above condition
                    //PROV-CCP-MM-941-23-0045 End
                    //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End
                    //SPNAIRCR_SDT05122019 Added
                    Session[StaticKeys.ApprovalNote] += Convert.ToString(remarks);
                    //SPNAIRCR_SDT05122019 Added
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V2 // " + Session[StaticKeys.ApprovalNote]);

                    try
                    {
                        if (materialMasterAccess.ApproveRequestM(lblMasterHeaderId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, SafeTypeHandling.ConvertToString(Session[StaticKeys.ApprovalNote])) > 0)
                        {
                            WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V3_Scop // " + flg);
                            flg = true;
                            scope.Complete();
                            scope.Dispose();
                            Session[StaticKeys.ApprovalNote] = "";
                            WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V3_Scop // " + flg);
                        }
                    }
                    catch (TransactionException ex)
                    {
                        scope.Dispose();
                        _log.Error("SubmitRequest11_TX", ex);
                    }
                    catch (Exception ex) { _log.Error("SubmitRequest11_2", ex); }
                    //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
                }
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End
            }
            //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
            if (appflg == true && appflgk == true)
            {
                WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V4  //" + appflg + "V4  //" + flg);

                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End
                if (flg)
                {
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V5  //" + flg);
                    Response.Redirect("../Material/materialmaster.aspx", false);
                    //Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V6  //" + flg);
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
            }
            else if (appflgk == false)
            {//PROV-CCP-MM-941-23-0045 Start
                WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V7  //" + appflg + "V7  //" + flg);

                //lblMsg.Text = "Kindly enter Kinaxis field in Classification section before approving the request.";
                lblMsg.Text = "Kindly enter Kinaxis field in Classification section before approving the request. <a href = '../Material/Classification.aspx'> Click here to update </a>";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                // Response.Redirect("../Material/Classification.aspx", false);
                //PROV-CCP-MM-941-23-0045 Start
            }
            else
            {
                WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V7  //" + appflg + "V7  //" + flg);

                lblMsg.Text = "Kindly enter Control Code/HSN and GST rate in GST section before approving the request.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End

        }

        catch (Exception ex)
        {
            _log.Error("SubmitRequest11", ex);
            WriteMassMatLog("CreateApproveMatLog_" + sdate + ".txt", "V8  //" + ex.ToString());
            //throw ex;
            lblMsg.Text = ex.ToString();
        }
    }

    public void WriteMassMatLog(string strFileName, string strMessage)
    {
        try
        {
            //Path.GetTempPath()
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\MassMaterialLog", strFileName), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
            objStreamWriter.Close();
            objFilestream.Close();
            //return true;  
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }
    }
}