using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Transactions;
using ExcelLibrary.SpreadSheet;
using System.Data.OleDb;
using Accenture.MWT.DomainObject;
using System.Drawing;
using log4net;

public partial class Transaction_Material_MaterialMassProcess : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();

    bool submitflg = true;

    public static List<string> massReqNos;

    #region Page Event

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    string reqNo = Session[StaticKeys.RequestNo].ToString();
                    string[] res = { " - " };
                    string[] reqN = reqNo.Split(res, 0);


                    lblRequestNo.Text = reqN[0];//Session[StaticKeys.RequestNo].ToString();
                    lblMaterialNo.Text = "Mass Process";//Session[StaticKeys.MaterialNo].ToString();
                    lblSelectedModule.Text = Session[StaticKeys.SelectedModule].ToString();

                    lblRequestor.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                    lblLocation.Text = Session[StaticKeys.Requestor_Location].ToString();
                    lblContactNo.Text = Session[StaticKeys.Requestor_ContactNo].ToString();

                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    if (Session[StaticKeys.MasterHeaderId] != null)
                    {
                        lblMassRequestProcessId.Text = Session[StaticKeys.MassRequestProcessId].ToString();
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                        ReadMaterialMasterRequests();
                        ReadDeparmentListForRollback(lblMasterHeaderId.Text, deptId, moduleId);

                        FillMassRequestData();
                    }
                    else
                    {
                        Response.Redirect("MaterialMaster.aspx");
                    }

                    //8400000359 S
                    FillDashBoard();
                    //8400000359 E
                }
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
    protected void btnRejectTo_Click(object sender, EventArgs e)
    {
        try
        {
            ReadMaterialMasterRequests("R");
            divRejectTo.Visible = true;
        }
        catch (Exception ex)
        { _log.Error("btnRejectTo_Click", ex); }
    }

    protected void btnRejCanel_Click(object sender, EventArgs e)
    {
        try
        {
            ReadMaterialMasterRequests("");
            divRejectTo.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("btnRejCanel_Click", ex); }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MaterialMasterAccess materialMasterAccess = new MaterialMasterAccess();
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        bool appflg = true;
        bool appflgk = true;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //if (objMatAccess.MassApproveRequest(lblMassRequestProcessId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "3" && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171"))
                {
                    try
                    {
                        if (MaterialMasterAccess.Get_IsHSNGSTFilled(lblMasterHeaderId.Text, lblUserId.Text, lblMassRequestProcessId.Text))
                        {
                            appflg = true;
                        }
                        else
                        {
                            appflg = false;
                        }
                    }
                    catch (Exception ex) { _log.Error("btnSubmit_Click_0", ex); }
                }

                else if ((Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "8" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "27") && (Session[StaticKeys.SelectedModuleId].ToString() == "139" || Session[StaticKeys.SelectedModuleId].ToString() == "145" || Session[StaticKeys.SelectedModuleId].ToString() == "144" || Session[StaticKeys.SelectedModuleId].ToString() == "171") && (!MaterialMasterAccess.IsUserHasSChangeReq(lblMasterHeaderId.Text)))
                {//PROV-CCP-MM-941-23-0045 Start
                     
                    try
                    {
                        if (MaterialMasterAccess.Get_IsKinaxisFilled("0", lblUserId.Text, lblMassRequestProcessId.Text))
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
                     //PROV-CCP-MM-941-23-0045 End
                }


                if (appflg == true && appflgk == true)
                {
                    //PROV-CCP-MM-941-23-0045 Start
                    //if (&& appflgk == true) in above condition
                    //PROV-CCP-MM-941-23-0045 End
                    //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End

                    try
                    {
                        if (objMatAccess.MassApproveRequest(lblMassRequestProcessId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, SafeTypeHandling.ConvertToString(Session[StaticKeys.ApprovalNote])) > 0)
                        {
                            flg = true;
                            scope.Complete();
                            scope.Dispose();
                            Session[StaticKeys.ApprovalNote] = "";
                        }
                    }
                    catch (TransactionException ex)
                    {
                        scope.Dispose();
                        _log.Error("btnSubmit_Click_TX", ex);
                    }
                    catch (Exception ex) { _log.Error("btnSubmit_Click_TX2", ex); }
                    //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
                }
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End
            }
            //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
            if (appflg == true && appflgk == true)
            {
                //PROV-CCP-MM-941-23-0045 Start
                //if (&& appflgk == true) in above condition
                //PROV-CCP-MM-941-23-0045 End 
                    //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End

                    if (flg)
                {
                    massReqNos = null;
                    Response.Redirect("../Material/materialmaster.aspx");
                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
                //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver Start
            }
            else if (appflgk == false)
            {//PROV-CCP-MM-941-23-0045 Start

                //lblMsg.Text = "Kindly enter Kinaxis field in Classification section before approving the request.";

                lblMsg.Text = "Kindly enter Kinaxis field in Classification section before approving the request. <a href = 'Classification.aspx'> Click here to update </a>";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                //PROV-CCP-MM-941-23-0045 Start
            }
            else
            {
                lblMsg.Text = "Kindly enter Control Code/HSN and GST rate in GST section for each request before approving the request.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
            //Make HSN/SAC code & GST rate field mandatory for  3,4 & 5 series code for Excise approver End
        }
        catch (Exception ex)
        {
            _log.Error("btnSubmit_Click", ex);
        }
    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        try
        {
            if (RollbackRequest())
            {
                massReqNos = null;
                Response.Redirect("../Material/materialmaster.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnRollback_Click", ex); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            massReqNos = null;
            Response.Redirect("../Material/materialmaster.aspx", false);
        }
        catch (Exception ex)
        { _log.Error("btnCancel_Click", ex); }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "V";
            string primKey = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = primKey;
            Session[StaticKeys.Mode] = "V";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.MaterialNo] = "";

            if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            {
                Response.Redirect("MaterialBlock.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                Response.Redirect("MaterialChange.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "E")
            {
                Response.Redirect("MaterialExtension.aspx", false);
            }
            else
            {
                Response.Redirect("basicdata1.aspx", false);
            }
        }
        catch (Exception ex)
        { _log.Error("btnView_Click", ex); }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "M";
            string primKey = GetSelectedPkID();
            Session[StaticKeys.MasterHeaderId] = primKey;
            Session[StaticKeys.Mode] = "M";
            Session[StaticKeys.MaterialType] = "";
            Session[StaticKeys.MaterialNo] = "";
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
            {
                if (massReqNos == null)
                {
                    massReqNos = new List<string>();
                }
                massReqNos.Add(primKey);
            }

            if (Session[StaticKeys.ActionType].ToString() == "U" || Session[StaticKeys.ActionType].ToString() == "B")
            {
                Response.Redirect("MaterialBlock.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "C")
            {
                Response.Redirect("MaterialChange.aspx", false);
            }
            else if (Session[StaticKeys.ActionType].ToString() == "E")
            {
                Response.Redirect("MaterialExtension.aspx", false);
            }
            else
            {
                Response.Redirect("basicdata1.aspx");
            }
        }
        catch (Exception ex)
        { _log.Error("btnModify_Click", ex); }
    }

    protected void btnUnGroup_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    //Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;

                    Label lblMassRequestProcessDetailId = (Label)grv.FindControl("lblMassRequestProcessDetailId");
                    objMatAccess.UngroupRequestFromMassProcess(lblMassRequestProcessDetailId.Text, lblUserId.Text);
                }
            }
            ReadMaterialMasterRequests();
        }
        catch (Exception ex)
        {
            _log.Error("btnUnGroup_Click", ex);
        }
    }

    protected void btnReject_Ind_Click(object sender, EventArgs e)
    {
        GridViewRow grv = (GridViewRow)((Button)sender).Parent.Parent;

        Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
        string strPk = lblRequestID.Text;

        DropDownList ddlReject_To = grv.FindControl("ddlReject_To") as DropDownList;
        CheckBoxList ChkReject_To = grv.FindControl("ChkReject_To") as CheckBoxList;
        TextBox txtRejectionNote = grv.FindControl("txtRejectionNote") as TextBox;

        MaterialMasterAccess objAccess = new MaterialMasterAccess();
        Utility objUtil = new Utility();

        try
        {
            string strReject = "";

            if (lblRejectionType.Text == "M")
            {
                for (int i = 0; i < ChkReject_To.Items.Count; i++)
                {
                    if (ChkReject_To.Items[i].Selected)
                    {
                        if (strReject == "")
                            strReject = ChkReject_To.Items[i].Value;
                        else
                            strReject += "," + ChkReject_To.Items[i].Value;
                    }
                }
            }
            else
            {
                strReject = ddlReject_To.SelectedValue;
            }


            if (objAccess.RollbackRequest(strPk, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectionNote.Text), lblUserId.Text) > 0)
            {
                Label lblMassRequestProcessDetailId = (Label)grv.FindControl("lblMassRequestProcessDetailId");
                objMatAccess.UngroupRequestFromMassProcess(lblMassRequestProcessDetailId.Text, lblUserId.Text);

                ReadMaterialMasterRequests("");
                FillMassRequestData();

                lblMsg.Text = "Request Rejected and WithDrawn Sucessfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
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
            _log.Error("btnReject_Ind_Click", ex);
        }
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        GridViewRow grv = (GridViewRow)((Button)sender).Parent.Parent;
        Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
        try
        {
            if (objMatAccess.UpdateRequestComplete(lblRequestID.Text, lblUserId.Text) > 0)
            {
                lblMsg.Text = "Request updated Sucessfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                ReadMaterialMasterRequests();
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-2);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        { _log.Error("btnComplete_Click", ex); }
    }

    protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string mode = Session[StaticKeys.Mode].ToString();

            if (mode == "M" || mode == "N")
            {
                GridViewRow grv = e.Row;
                if (grv.RowType == DataControlRowType.DataRow)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    Label lblRequestStatus = grv.FindControl("lblRequestStatus") as Label;

                    DropDownList ddlReject_To = grv.FindControl("ddlReject_To") as DropDownList;
                    RequiredFieldValidator reqddlReject_To = grv.FindControl("reqddlReject_To") as RequiredFieldValidator;

                    CheckBoxList ChkReject_To = grv.FindControl("ChkReject_To") as CheckBoxList;
                    //CompareValidator reqChkReject_To = grv.FindControl("reqChkReject_To") as CompareValidator;

                    TextBox txtRejectionNote = grv.FindControl("txtRejectionNote") as TextBox;
                    Button btnReject_Ind = grv.FindControl("btnReject_Ind") as Button;
                    Button btnComplete = grv.FindControl("btnComplete") as Button;

                    if (lblRequestStatus.Text == "C" || lblRequestStatus.Text == "A")
                    {
                        ddlReject_To.Visible = false;
                        txtRejectionNote.Visible = false;
                        btnReject_Ind.Visible = false;
                        btnComplete.Visible = false;
                    }
                    else
                    {
                        grdSearch.Columns[8].Visible = true;
                        grdSearch.Columns[9].Visible = true;

                        if (btnSubmit.Visible && MasterAccess.Get_IsFinalApproval(lblRequestID.Text, lblUserId.Text))
                            btnSubmit.Visible = false;

                        if (!objMatAccess.IsSAPintegrationPending(lblRequestID.Text))
                        {
                            DataSet ds = objMatAccess.ReadDeparmentListForRollback(lblRequestID.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblModuleId.Text);

                            ddlReject_To.DataSource = ds;
                            ddlReject_To.DataTextField = "LevelName";
                            ddlReject_To.DataValueField = "Sequence";
                            ddlReject_To.DataBind();

                            ChkReject_To.DataSource = ds;
                            ChkReject_To.DataTextField = "LevelName";
                            ChkReject_To.DataValueField = "Sequence";
                            ChkReject_To.DataBind();

                            if (lblRejectionType.Text == "M")
                            {
                                ddlReject_To.Visible = false;
                                reqddlReject_To.Visible = false;
                                ChkReject_To.Visible = true;
                            }
                            else
                            {
                                ddlReject_To.Visible = true;
                                reqddlReject_To.Visible = true;
                                ChkReject_To.Visible = false;
                            }


                            txtRejectionNote.Visible = true;
                            btnReject_Ind.Visible = true;
                            btnComplete.Visible = false;
                        }
                        else
                        {
                            ddlReject_To.Visible = false;
                            ChkReject_To.Visible = false;
                            txtRejectionNote.Visible = false;
                            btnReject_Ind.Visible = false;
                            btnComplete.Visible = true;
                        }
                    }
                    //Srinidhi
                    if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0")
                    {
                        if (massReqNos != null)
                        {
                            if (massReqNos.Contains(lblRequestID.Text))
                                e.Row.BackColor = (Color)ColorTranslator.FromHtml("#EDF5FF");
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        { _log.Error("grdSearch_RowDataBound", ex); }
    }

    protected void Process_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        DataSet ds = new DataSet();

        string StrPath = "~/Transaction/Material/MaterialDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";

        //HttpFileCollection fileCollection = Request.Files;

        if (fileUpload.HasFile)
        {
            HttpPostedFile uploadfile = fileUpload.PostedFile;

            try
            {

                if (uploadfile.ContentLength > 0)
                {
                    string fileExtension = System.IO.Path.GetExtension(uploadfile.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/tempfile/") + Session[StaticKeys.RequestNo].ToString() + fileExtension;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            System.IO.File.Delete(fileLocation);
                        }
                        fileUpload.SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            //excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            //fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";


                            //DataTable dt1 = readExcel(excelConnectionString, "Select * from [Sheet1$]");

                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);


                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        //dt = readExcel(excelConnectionString, "Select * from [Sheet1$]");

                        if (dt == null)
                        {
                            return;
                        }

                        //ds.Tables.Add(dt);

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [Table$]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }

                    grvMassData.DataSource = ds;
                }
                else
                {
                    grvMassData.DataSource = null;
                }

                grvMassData.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlMsg.Visible = false;
                    ModalPopupExtenderI.Show();
                }
                else
                {
                    lblMsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Process_Click", ex);
                //lblMsg.Text = ex.Message;

                lblMsg.Text = "Error Occured \\Invalid Format.Please download and use the Excel format provided above.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to be uploaded";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (SaveImport())
            {
                Response.Redirect("MaterialMassProcess.aspx");
            }
            else
            {
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "error";
                lblMsg.Text = "Error Occured during Updation";

                ModalPopupExtenderI.Show();
            }
        }
        catch (Exception ex)
        { _log.Error("btnAdd_Click", ex); }
    }

    protected void grvMassData_DataBound1(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;
        try
        {
            foreach (GridViewRow grv in grvMassData.Rows)
            {
                string msg = "";

                Label lblPlant = (Label)grv.FindControl("lblPlant");
                Label lblDesc = (Label)grv.FindControl("lblDesc");
                Label lblMatType = (Label)grv.FindControl("lblMaterialType");
                Label lblDivision = (Label)grv.FindControl("lblDivision");
                Label lblProduct_Hierarchy = (Label)grv.FindControl("lblProduct_Hierarchy");
                Label lblProfit_Center = (Label)grv.FindControl("lblProfit_Center");
                Label lblValuation_Class = (Label)grv.FindControl("lblValuation_Class");
                Label lblPrice_Ctrl_Indicator = (Label)grv.FindControl("lblPrice_Ctrl_Indicator");
                Label lblMaterialGrp = (Label)grv.FindControl("lblMaterialGrp");
                Label lblModuleId = (Label)grv.FindControl("lblModuleId");
                Label lblMHId = (Label)grv.FindControl("lblMHId");
                Label lblMRP_Type = (Label)grv.FindControl("lblMRP_Type");
                Label lblMRP_Controller = (Label)grv.FindControl("lblMRP_Controller");
                Label lblLot_Size = (Label)grv.FindControl("lblLot_Size");
                Label lblControl_Code = (Label)grv.FindControl("lblControl_Code");

                if (lblPlant.Text == "" && lblDesc.Text == "" && lblMatType.Text == "")
                {
                    grv.Visible = false;
                }
                else
                {
                    i++;
                    HelperAccess helperAccess = new HelperAccess();

                    if (lblDesc.Text == "")
                    {
                        msg = msg + "Material Description is Mandatory.";
                    }
                    else if (!(lblDesc.Text.ToString().Length <= 40))
                    {
                        msg = msg + "Material Description cannot be more than 40 characters.";
                    }

                    //if (msg == "")
                    //{
                    //    DropDownList ddlMaterialGrp = (DropDownList)grv.FindControl("ddlMaterialGrp");
                    //    if (lblModuleId.Text == "138" || lblModuleId.Text == "147")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'EN,SPN'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "163")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'EN,MS,PS,PM,RM'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "162")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'RM,SPN'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "170")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'PS,CP'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "144")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'SF5107,SF5108,SF5109,SF5110,SF5111,SFRM'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "139")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'FG,F0'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "171")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'BF,BS,F0,FG,FP'", "LookUp_Desc", "LookUp_Code", "");
                    //    else if (lblModuleId.Text == "145")
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetMaterialGrp 'BS,F0,FG'", "LookUp_Desc", "LookUp_Code", "");
                    //    else
                    //        helperAccess.PopuplateDropDownList(ddlMaterialGrp, "pr_GetDropDownListByControlNameModuleType 'M','ddlMaterialGroup'", "LookUp_Desc", "LookUp_Code", "");

                    if (lblMaterialGrp.Text == "")
                        msg = msg + "Material Group is mandatory.";
                    //else if (ddlMaterialGrp.Items.FindByValue(lblMaterialGrp.Text.Trim() + "    ") == null)
                    //    msg = msg + "Invalid Material group.";
                    //}

                    if (msg == "")
                    {
                        //DropDownList ddlDivision = (DropDownList)grv.FindControl("ddlDivision");
                        //List<string> strDiv = new List<string>();
                        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMHId.Text + "','B1','" + 0 + "'", "Division_Name", "Division_Id", "");
                        //foreach (ListItem lstDiv in ddlDivision.Items)
                        //{
                        //    string[] str = Convert.ToString(lstDiv).Split('-');
                        //    strDiv.Add(str[0].Trim());
                        //}
                        if (lblDivision.Text == "")
                            msg = msg + "Division is Mandatory.";
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(lblDivision.Text, "^[\\d]{2}$"))
                            msg = msg + "Division must be 2 digit number.";
                        //else if (!strDiv.Contains(lblDivision.Text.Trim()))
                        //    msg = msg + "Invalid division.";
                    }
                    //if (msg == "")
                    //{
                    //    if (!(lblProduct_Hierarchy.Text.ToString().Length == 18))
                    //        msg = msg + "Product hierarachy must be 18 digit number.";
                    //}
                    if (msg == "")
                    {
                        //DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");
                        //DropDownList ddlProfitCenter = (DropDownList)grv.FindControl("ddlProfitCenter");

                        //helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");
                        //helperAccess.PopuplateDropDownList(ddlProfitCenter, "pr_GetProfitCenterByPlantId 'ddlProfitCenter','" + 13 + "','" + lblPlant.Text + "'", "LookUp_Desc", "LookUp_Code", "");

                        if (!System.Text.RegularExpressions.Regex.IsMatch(lblProfit_Center.Text, "^[\\d]{10}$"))
                            msg = msg + "Profit center must be 10 digit number (preeced the value with zero if required)";
                        //else if (ddlProfitCenter.Items.FindByValue(lblProfit_Center.Text.Trim()) == null)
                        //    msg = msg + "Invalid Profit center.";
                    }
                    if (msg == "")
                    {
                        // DropDownList ddlValuationClass = (DropDownList)grv.FindControl("ddlValuationClass");
                        //string AccountCat = MaterialHelper.GetAccountCategoryByModuleId(lblModuleId.Text);
                        //helperAccess.PopuplateDropDownList(ddlValuationClass, "pr_GetDropDownListByControlNameModuleType 'M','ddlValuationClass','" + 1 + "','" + AccountCat + "'", "LookUp_Desc", "LookUp_Code", "");

                        if (!System.Text.RegularExpressions.Regex.IsMatch(lblValuation_Class.Text, "^[\\d]{4}$"))
                            msg = msg + "Valuation class must be 4 digit number";
                        //else if (ddlValuationClass.Items.FindByValue(lblValuation_Class.Text.Trim()) == null)
                        //    msg = msg + "Invalid Valuation class.";
                    }
                    if (msg == "")
                    {
                        if (lblPrice_Ctrl_Indicator.Text == "")
                            msg = msg + "Price Control Indicator is Mandatory.";
                        else if (!(lblPrice_Ctrl_Indicator.Text.ToString().Length == 1))
                            msg = msg + "Price Control Indicator cannot be more than 1 character.";
                        //else if (lblPrice_Ctrl_Indicator.Text.ToString() != "S" && lblPrice_Ctrl_Indicator.Text.ToString() != "V")
                        //    msg = msg + "Invalid Price Control Indicator.";
                    }
                    //if (msg == "")
                    //{
                    //    DropDownList ddlMRPTypeMass = (DropDownList)grv.FindControl("ddlMRPTypeMass");
                    //    helperAccess.PopuplateDropDownList(ddlMRPTypeMass, "pr_GetDropDownListByControlNameModuleType 'M','ddlMrpType'", "LookUp_Desc", "LookUp_Code", "");
                    //    if (lblMRP_Type.Text == "")
                    //        msg = msg + "MRP Type is mandatory.";
                    //    else if (ddlMRPTypeMass.Items.FindByValue(lblMRP_Type.Text.Trim()) == null)
                    //        msg = msg + "Invalid MRP Type.";
                    //}
                    //if (msg == "")
                    //{
                    //    DropDownList ddlMRPControllerMass = (DropDownList)grv.FindControl("ddlMRPControllerMass");
                    //    helperAccess.PopuplateDropDownList(ddlMRPControllerMass, "pr_GetDropDownListByControlNameModuleTypePlantFilter 'M','ddlMrpController','" + 8 + "','" + lblPlant.Text + "'", "LookUp_Desc", "LookUp_Code", "");
                    //    if (ddlMRPControllerMass.Items.FindByValue(lblMRP_Controller.Text.Trim()) == null)
                    //        msg = msg + "Invalid MRP Controller.";
                    //}
                    //if (msg == "")
                    //{
                    //    DropDownList ddlLotSizeMass = (DropDownList)grv.FindControl("ddlLotSizeMass");
                    //    helperAccess.PopuplateDropDownList(ddlLotSizeMass, "pr_GetDropDownListByControlNameModuleType 'M','ddlLotSize'", "LookUp_Desc", "LookUp_Code", "");
                    //    if (ddlLotSizeMass.Items.FindByValue(lblLot_Size.Text.Trim()) == null)
                    //        msg = msg + "Invalid Lot Size.";
                    //}

                    //if (msg == "")
                    //{
                    //    List<string> strchp = new List<string>();
                    //    DropDownList ddlControlCodeMass = (DropDownList)grv.FindControl("ddlControlCodeMass");
                    //    helperAccess.PopuplateDropDownList(ddlControlCodeMass, "pr_GetDropDownListByControlNameModuleTypeByCountryID 'M','ddlControlCode','" + 7 + "',IN", "LookUp_Desc", "LookUp_Code", "");
                    //    foreach (ListItem lstchp in ddlControlCodeMass.Items)
                    //    {
                    //        strchp.Add(Convert.ToString(lstchp).Trim());
                    //    }

                    //    if (!strchp.Contains(lblDivision.Text.Trim()))
                    //        msg = msg + "Invalid Chapter ID.";
                    //}

                }


                Panel pnlMsg = (Panel)grv.FindControl("pnlMsg");
                Label lblMsg1 = (Label)grv.FindControl("lblMsg");


                if (msg == "")
                {
                    lblMsg1.Text = "OK";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;

                }
                else
                {
                    flg = false;
                    lblMsg1.Text = msg;
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;

                }
            }
            if (i > 0)
                btnAdd.Enabled = flg;

        }
        catch (Exception ex)
        { _log.Error("grvMassData_DataBound1", ex); }
    }

    #endregion

    #region Private Methods

    private string GetSelectedPkID()
    {
        string strPk = string.Empty;
        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
                    strPk = lblRequestID.Text;
                    Label lblModuleId = grv.FindControl("lblModuleId") as Label;
                    Label lblModuleName = grv.FindControl("lblModuleName") as Label;
                    Label lblRequest_No = grv.FindControl("lblRequestNo") as Label;
                    Label lblMasterCode = grv.FindControl("lblMasterCode") as Label;
                    Label lblActionType = grv.FindControl("lblActionType") as Label;
                    Label lblCreatedBy = grv.FindControl("lblCreatedBy") as Label;
                    Label lblLocation = grv.FindControl("lblLocation") as Label;
                    Label lblContactNo = grv.FindControl("lblContactNo") as Label;
                    Label lblPlantId = grv.FindControl("lblPlantId") as Label;
                    Label lblStorageLocation = grv.FindControl("lblStorageLocation") as Label;
                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                    Label lblMaterialShortDescription = grv.FindControl("lblMaterialShortDescription") as Label;
                    Label lblStorageLocationName = grv.FindControl("lblStorageLocationName") as Label;
                    Label lblSalesOrg = grv.FindControl("lblSalesOrg") as Label;
                    Label lblDistChnl = grv.FindControl("lblDistChnl") as Label;
                    //Depot Srinidhi
                    Label lblRequestType = grv.FindControl("lblRequestType") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text + " - " + lblRequest_No.Text;
                    Session[StaticKeys.MaterialNo] = (lblMasterCode.Text == "" ? "New Request" : lblMasterCode.Text) + " - " + lblMaterialShortDescription.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    Session[StaticKeys.MatStorageLocationId] = lblStorageLocation.Text;
                    Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;

                    Session[StaticKeys.MatStorageLocationName] = lblStorageLocationName.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;

                    Session[StaticKeys.MaterialSalesOrgId] = lblSalesOrg.Text;
                    Session[StaticKeys.MaterialDistChnlId] = lblDistChnl.Text;
                    //Depot Srinidhi
                    Session[StaticKeys.MarketType] = lblRequestType.Text;
                }

            }
        }
        catch (Exception ex)
        {
            _log.Error("GetSelectedPkID", ex);
            //throw ex;
        }
        return strPk;
    }

    private void ReadMaterialMasterRequests(string Action = "")
    {
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;
        DataSet dstData;

        try
        {
            dstData = objMatAccess.GetMaterialListByMassMaterialProcessID(lblMassRequestProcessId.Text, lblUserId.Text);
            grdSearch.DataSource = dstData.Tables[0].DefaultView;

            btnUnGroup.Visible = false;
            //grdSearch.DataBind();
            string mode = Session[StaticKeys.Mode].ToString();
            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = true;
                if (mode == "M" || mode == "N")
                {
                    btnView.Visible = false;
                    btnModify.Visible = true;

                    if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2" || (Session[StaticKeys.LoggedIn_User_FullName].ToString() == Session[StaticKeys.Requestor_User_Name].ToString()))
                    {
                        btnSubmit.Text = "Submit All";
                        btnUnGroup.Visible = true;
                        btnRejectTo.Visible = false;
                        //Srinidhi
                        fileUpload.Visible = false;
                        Process.Visible = false;
                        divHeader.Visible = false;
                    }
                    else
                    {
                        btnSubmit.Text = "Approve All";
                        btnRejectTo.Visible = objMatAccess.IsUserInitiator(lblModuleId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text);

                        if (!((SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 14) || (SafeTypeHandling.ConvertStringToInt32(Session[StaticKeys.LoggedIn_User_DeptId]) == 13)))
                        {
                            fileUpload.Visible = false;
                            Process.Visible = false;
                            divHeader.Visible = false;
                        }
                    }

                    if ((Session[StaticKeys.ActionType].ToString() == "N" || Session[StaticKeys.ActionType].ToString() == "M" || Session[StaticKeys.ActionType].ToString() == "R") && Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                    {
                        if (!objMatAccess.IsMassSAPintegrationPending(lblMassRequestProcessId.Text))
                        {
                            btnSubmit.Visible = submitflg;
                            btnSAPUpload.Visible = false;
                            btnRejectTo.Visible = false;
                        }
                        else
                        {

                            if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text, lblMassRequestProcessId.Text))
                            {
                                btnSubmit.Visible = false;
                                btnSAPUpload.Visible = true;

                                string Module = Session[StaticKeys.SelectedModuleId].ToString();

                                if (btnRejectTo.Visible && (Module == "162" || Module == "164"))
                                {
                                    lblRejectionType.Text = "M";
                                    tdChkReject.Visible = true;
                                    tdDdlReject.Visible = false;
                                }
                            }
                            else
                            {
                                btnSubmit.Visible = true;
                                btnSAPUpload.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        btnSAPUpload.Visible = false;
                    }
                }
                else
                {
                    btnRejectTo.Visible = false;
                    btnSubmit.Visible = false;
                    btnSAPUpload.Visible = false;
                    btnModify.Visible = false;

                    //Srinidhi
                    fileUpload.Visible = false;
                    Process.Visible = false;
                    divHeader.Visible = false;
                }

                //if (Action == "R")
                if (btnRejectTo.Visible)
                {
                    grdSearch.Columns[8].Visible = true;
                    grdSearch.Columns[9].Visible = true;
                }
                else
                {
                    grdSearch.Columns[8].Visible = false;
                    grdSearch.Columns[9].Visible = false;
                }
            }
            else
            {
                btnView.Visible = false;
                btnModify.Visible = false;
                btnRejectTo.Visible = false;
                btnSubmit.Visible = false;
                btnSAPUpload.Visible = false;
            }
            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadMaterialMasterRequests", ex);
            //throw ex;
        }
    }

    private bool RollbackRequest()
    {
        Utility objUtil = new Utility();
        bool flg = false;

        try
        {
            foreach (GridViewRow grv in grdSearch.Rows)
            {
                TextBox txtRejectionNote = (TextBox)grv.FindControl("txtRejectionNote");

                //if (txtRejectionNote.Text != "")
                //{
                Label lblMassRequestProcessDetailId = (Label)grv.FindControl("lblMassRequestProcessDetailId");

                objMatAccess.UpdateRejectionRemark(lblMassRequestProcessDetailId.Text, objUtil.ReplaceEscapeSequenceChar(txtRejectionNote.Text), lblUserId.Text);
                //}
            }
            string strReject = "";

            if (lblRejectionType.Text == "M")
            {
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

            if (objMatAccess.MassRollbackRequest(lblMassRequestProcessId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectNote.Text), lblUserId.Text) > 0)
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
            //throw ex;
            _log.Error("RollbackRequest", ex);
        }
        return flg;
    }

    public void ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId)
    {
        try
        {
            DataSet ds = objMatAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);

            ddlRejectTo.DataSource = ds;
            ddlRejectTo.DataTextField = "LevelName";
            ddlRejectTo.DataValueField = "Sequence";
            ddlRejectTo.DataBind();

            ChkRejectTo.DataSource = ds;//objMatAccess.ReadDeparmentListForRollback(masterHeaderId, departmentId, moduleId);
            ChkRejectTo.DataTextField = "LevelName";
            ChkRejectTo.DataValueField = "Sequence";
            ChkRejectTo.DataBind();
        }
        catch (Exception ex)
        {
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }

    protected void FillMassRequestData()
    {
        try
        {
            DataSet dstData = objMatAccess.GetMaterialDataByMassMaterialProcessID(lblMassRequestProcessId.Text, lblUserId.Text);

            grvMassRequestData.DataSource = dstData;
            grvMassRequestData.DataBind();
        }
        catch (Exception ex)
        { _log.Error("FillMassRequestData", ex); }
    }

    protected bool SaveImport()
    {
        bool flg = true;
        try
        {
            foreach (GridViewRow grv in grvMassData.Rows)
            {
                Label lblMHId = (Label)grv.FindControl("lblMHId");
                Label lblReqNo = (Label)grv.FindControl("lblReqNo");
                Label lblPlant = (Label)grv.FindControl("lblPlant");
                Label lblStorageLocation = (Label)grv.FindControl("lblStorageLocation");
                Label lblMaterialNo = (Label)grv.FindControl("lblMaterialNo");
                Label lblMaterialType = (Label)grv.FindControl("lblMaterialType");
                Label lblBUOM = (Label)grv.FindControl("lblBUOM");
                Label lblDesc = (Label)grv.FindControl("lblDesc");
                Label lblMaterialGrp = (Label)grv.FindControl("lblMaterialGrp");
                Label lblDivision = (Label)grv.FindControl("lblDivision");
                Label lblRemarks = (Label)grv.FindControl("lblRemarks");
                Label lblAcc_Assignment_Grp = (Label)grv.FindControl("lblAcc_Assignment_Grp");
                Label lblProduct_Hierarchy = (Label)grv.FindControl("lblProduct_Hierarchy");
                Label lblPur_Order_Unit_Measure = (Label)grv.FindControl("lblPur_Order_Unit_Measure");
                Label lblPurchasing_Value_Key = (Label)grv.FindControl("lblPurchasing_Value_Key");
                Label lblPurchasing_Group = (Label)grv.FindControl("lblPurchasing_Group");
                Label lblNo_Of_Mftr = (Label)grv.FindControl("lblNo_Of_Mftr");
                Label lblMftr_Part_No = (Label)grv.FindControl("lblMftr_Part_No");
                Label lblMftr_Part_Profile = (Label)grv.FindControl("lblMftr_Part_Profile");
                Label lblGR_Processing_Time = (Label)grv.FindControl("lblGR_Processing_Time");
                Label lblPur_Order_Text = (Label)grv.FindControl("lblPur_Order_Text");
                Label lblControl_Code = (Label)grv.FindControl("lblControl_Code");
                Label lblMRP_BUOM = (Label)grv.FindControl("lblMRP_BUOM");
                Label lblMRP_Type = (Label)grv.FindControl("lblMRP_Type");
                Label lblMRP_Controller = (Label)grv.FindControl("lblMRP_Controller");
                Label lblReorder_Point = (Label)grv.FindControl("lblReorder_Point");
                Label lblLot_Size = (Label)grv.FindControl("lblLot_Size");
                Label lblMin_Lot_Size = (Label)grv.FindControl("lblMin_Lot_Size");
                Label lblMax_Lot_Size = (Label)grv.FindControl("lblMax_Lot_Size");
                Label lblFixed_Lot_Size = (Label)grv.FindControl("lblFixed_Lot_Size");
                Label lblRounding_Value = (Label)grv.FindControl("lblRounding_Value");
                Label lblMax_Stock_Level = (Label)grv.FindControl("lblMax_Stock_Level");
                Label lblPlanning_Time_Fence = (Label)grv.FindControl("lblPlanning_Time_Fence");
                Label lblProduction_Unit = (Label)grv.FindControl("lblProduction_Unit");
                Label lblProcurement_Type = (Label)grv.FindControl("lblProcurement_Type");
                Label lblPlanned_Delivery_Time = (Label)grv.FindControl("lblPlanned_Delivery_Time");
                Label lblInHouse_Production_Time = (Label)grv.FindControl("lblInHouse_Production_Time");
                Label lblMin_Safety_Stock = (Label)grv.FindControl("lblMin_Safety_Stock");
                Label lblFxd_Lot_Size_Storage_Loc = (Label)grv.FindControl("lblFxd_Lot_Size_Storage_Loc");
                Label lblStorage_bin = (Label)grv.FindControl("lblStorage_bin");
                Label lblMin_Remaining_Shelf_Life = (Label)grv.FindControl("lblMin_Remaining_Shelf_Life");
                Label lblTotal_Shelf_Life_Days = (Label)grv.FindControl("lblTotal_Shelf_Life_Days");
                Label lblProfit_Center = (Label)grv.FindControl("lblProfit_Center");
                Label lblUnit_Issue = (Label)grv.FindControl("lblUnit_Issue");
                Label lblIs_QM_in_Procurement = (Label)grv.FindControl("lblIs_QM_in_Procurement");
                Label lblCertificate_Type = (Label)grv.FindControl("lblCertificate_Type");
                Label lblCtrl_Key_QM_Procurement = (Label)grv.FindControl("lblCtrl_Key_QM_Procurement");
                Label lblInterval_Nxt_Inspection = (Label)grv.FindControl("lblInterval_Nxt_Inspection");
                Label lblInspection_Type = (Label)grv.FindControl("lblInspection_Type");
                Label lblValuation_Class = (Label)grv.FindControl("lblValuation_Class");
                Label lblPrice_Ctrl_Indicator = (Label)grv.FindControl("lblPrice_Ctrl_Indicator");
                Label lblLot_Size_Prd_Cost_Est = (Label)grv.FindControl("lblLot_Size_Prd_Cost_Est");

                MaterialMass objMatMass = new MaterialMass();
                Utility objUtil = new Utility();

                objMatMass.MHId = Convert.ToInt32(lblMHId.Text);
                objMatMass.Plant_Id = lblPlant.Text;
                objMatMass.Storage_Location = lblStorageLocation.Text;
                objMatMass.Material_Code = lblMaterialNo.Text;
                objMatMass.Material_Type = lblMaterialType.Text;
                objMatMass.BUOM = lblBUOM.Text;
                objMatMass.Material_Desc = lblDesc.Text;
                objMatMass.Material_Grp = lblMaterialGrp.Text;
                objMatMass.Division = lblDivision.Text;
                objMatMass.Remarks = lblRemarks.Text;
                objMatMass.Acc_Assgnt_Grp = lblAcc_Assignment_Grp.Text;
                objMatMass.ProdHierarchy = lblProduct_Hierarchy.Text;
                objMatMass.Pur_Order_Unit_Measure = lblPur_Order_Unit_Measure.Text;
                objMatMass.Pur_Value_Key = lblPurchasing_Value_Key.Text;
                objMatMass.Pur_Group = lblPurchasing_Group.Text;
                objMatMass.No_Of_Manufacturer = lblNo_Of_Mftr.Text;
                objMatMass.Manufacturer_Part_No = lblMftr_Part_No.Text;
                objMatMass.Manufacturer_Part_Profile = lblMftr_Part_Profile.Text;
                objMatMass.GR_Processing_Time = lblGR_Processing_Time.Text;
                objMatMass.Purchase_Order_text = lblPur_Order_Text.Text;
                objMatMass.ControlCode = lblControl_Code.Text;
                objMatMass.MRP_BUOM = lblMRP_BUOM.Text;
                objMatMass.MRP_type = lblMRP_Type.Text;
                objMatMass.MRP_Controller = lblMRP_Controller.Text;
                objMatMass.Reorder_Point = lblReorder_Point.Text;
                objMatMass.Lot_Size = lblLot_Size.Text;
                objMatMass.Min_Lot_Size = lblMin_Lot_Size.Text;
                objMatMass.Max_Lot_Size = lblMax_Lot_Size.Text;
                objMatMass.Fixed_Lot_Size = lblFixed_Lot_Size.Text;
                objMatMass.Rounding_Value = lblRounding_Value.Text;
                objMatMass.Max_Stock_Level = lblMax_Stock_Level.Text;
                objMatMass.Planning_Time_Fence = lblPlanning_Time_Fence.Text;
                objMatMass.Prod_Unit = lblProduction_Unit.Text;
                objMatMass.Procurement_Type = lblProcurement_Type.Text;
                objMatMass.Delivery_Time = lblPlanned_Delivery_Time.Text;
                objMatMass.Inhouse_Prod_Time = lblInHouse_Production_Time.Text;
                objMatMass.Min_Safety_Stock = lblMin_Safety_Stock.Text;
                objMatMass.Fxd_Lot_Size_Storage_Loc = lblFxd_Lot_Size_Storage_Loc.Text;
                objMatMass.Storage_bin = lblStorage_bin.Text;
                objMatMass.Min_Rem_Shelf_Life = lblMin_Remaining_Shelf_Life.Text;
                objMatMass.Total_Shelf_Life = lblTotal_Shelf_Life_Days.Text;
                objMatMass.Profit_Center = lblProfit_Center.Text;
                objMatMass.Unit_Issue = lblUnit_Issue.Text;
                objMatMass.Is_QM_is_Procurement = lblIs_QM_in_Procurement.Text;
                objMatMass.Certificate_Type = lblCertificate_Type.Text;
                objMatMass.Ctrl_Key_QM_Procurement = lblCtrl_Key_QM_Procurement.Text;
                objMatMass.Interval_Nxt_Inspection = lblInterval_Nxt_Inspection.Text;
                objMatMass.Inspection_Type = lblInspection_Type.Text.Replace('/', ',');
                objMatMass.Valuation_Class = lblValuation_Class.Text;
                objMatMass.Price_Ctrl_Indicator = lblPrice_Ctrl_Indicator.Text;
                objMatMass.Lot_Size_Prd_Cost_Est = lblLot_Size_Prd_Cost_Est.Text;

                objMatMass.IsActive = 1;
                objMatMass.UserId = lblUserId.Text;
                objMatMass.TodayDate = objUtil.GetDate();
                objMatMass.IPAddress = objUtil.GetIpAddress();

                if (objMatAccess.SaveImport(objMatMass) != 1)
                {
                    flg = false;
                }

            }

        }
        catch (Exception ex)
        { _log.Error("SaveImport", ex); }
        return flg;
    }

    #endregion

    #region Excel Download

    protected void lnkExcelDwld_Click(object sender, EventArgs e)
    {
        try
        {
            DownLoadDataInExcel();
        }
        catch (Exception ex)
        { _log.Error("lnkExcelDwld_Click", ex); }
    }

    protected void lnkExcelDwld_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DownLoadDataInExcel();
        }
        catch (Exception ex)
        { _log.Error("lnkExcelDwld_Click", ex); }
    }

    protected void DownLoadDataInExcel()
    {
        try
        {
            string fileName = lblRequestNo.Text;

            DataSet dstData = objMatAccess.GetMaterialDataByMassMaterialProcessID(lblMassRequestProcessId.Text, lblUserId.Text);
            string filePath = Server.MapPath("../../tempFile/" + fileName + ".xls");
            if (System.IO.File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                System.IO.File.Delete(filePath);
            }

            CreateWorkbook(filePath, dstData);
            DownloadFile(filePath, fileName);
        }
        catch (Exception ex)
        { _log.Error("DownLoadDataInExcel", ex); }
    }

    public void CreateWorkbook(String filePath, DataSet dataset)
    {
        try
        {
            if (dataset.Tables.Count == 0)
                throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");

            Workbook workbook = new Workbook();
            CellStyle style = new CellStyle();
            style.BackColor = System.Drawing.Color.Beige;

            foreach (DataTable dt in dataset.Tables)
            {
                Worksheet worksheet = new Worksheet(dt.TableName);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    // Add column header
                    worksheet.Cells[0, i].Style = style;
                    worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);

                    // Populate row data
                    for (int j = 0; j < dt.Rows.Count; j++)
                        worksheet.Cells[j + 1, i] = new Cell(SafeTypeHandling.ConvertToString(dt.Rows[j][i]));

                }
                workbook.Worksheets.Add(worksheet);
            }

            workbook.Save(filePath);
        }
        catch (Exception ex)
        { _log.Error("CreateWorkbook", ex); }
    }

    private void DownloadFile(string filePath, string fileName)
    {
        try
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            fileName = fileName + ".xls";
            //string filePath = Server.MapPath("../../tempFile/" + fileName);

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

            Response.TransmitFile(filePath);

        }
        catch (Exception ex)
        { _log.Error("DownloadFile", ex); }
        //Response.End();
    }

    private void DeleteFile(string fileName)
    {
        try
        {
            string filePath = Server.MapPath("../../tempFile/");
            string type = "File";
            if (type == "File")
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                System.IO.File.Delete(filePath);
            }
            else if (type == "Directory")
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                System.IO.Directory.Delete(filePath, true);
            }
        }
        catch (Exception ex)
        { _log.Error("DeleteFile", ex); }
    }

    #endregion


}