using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Transactions;
using System.Drawing;
using System.Web.UI.HtmlControls;
using log4net;

public partial class Transaction_BOMRecipe_BOMRecipeMassProcess : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    BOMAccess objBOMRecipeAccess = new BOMAccess();
    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
    bool submitflg = true;
    public static List<string> massReqNos;

    #region PageEvents

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
                                                //lblMaterialNo.Text = "Mass Process";//Session[StaticKeys.MaterialNo].ToString();
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

                        ReadBOMRecipeMasterRequests();
                        ReadDeparmentListForRollback(lblMasterHeaderId.Text, deptId, moduleId);
                    }
                    else
                    {
                        Response.Redirect("BOMRecipeMaster.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }

    protected void btnRejectTo_Click(object sender, EventArgs e)
    {
        try
        {
            ReadBOMRecipeMasterRequests("R");
            divRejectTo.Visible = true;
        }
        catch (Exception ex)
        { _log.Error("btnRejectTo_Click", ex); }
    }

    protected void btnRejCanel_Click(object sender, EventArgs e)
    {
        try
        {
            ReadBOMRecipeMasterRequests("");
            divRejectTo.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("btnRejCanel_Click", ex); }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (objMatAccess.MassApproveRequest(lblMassRequestProcessId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text, "") > 0)
                {
                    flg = true;
                    scope.Complete();
                    Session[StaticKeys.ApprovalNote] = "";
                }
            }

            if (flg)
            {
                massReqNos = null;
                Response.Redirect("../BOMRecipe/BOMRecipeMaster.aspx");
            }
            else
            {
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
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
                Response.Redirect("../BOMRecipe/BOMRecipeMaster.aspx");
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
            Response.Redirect("../BOMRecipe/BOMRecipeMaster.aspx");
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
            //BOM_8200050878 for new Module created 227,228,229
            //manali chavan
            if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "187") || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                Response.Redirect("BOMHeaderComp.aspx");
            else if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                Response.Redirect("Reciepe.aspx");
            else if ((Session[StaticKeys.SelectedModuleId].ToString() == "188"))
                Response.Redirect("BOMRecipeChange.aspx");
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
            //BOM_8200050878 for new Module created 227,228,236
            //manali chavan
            if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "187") || (Session[StaticKeys.SelectedModuleId].ToString() == "228"))
                Response.Redirect("BOMHeaderComp.aspx");
            else if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                Response.Redirect("Reciepe.aspx");
            else if ((Session[StaticKeys.SelectedModuleId].ToString() == "188"))
                Response.Redirect("BOMRecipeChange.aspx");
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
            ReadBOMRecipeMasterRequests();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("btnUnGroup_Click", ex);
        }
    }

    protected void btnReject_Ind_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grv = (GridViewRow)((Button)sender).Parent.Parent;

            Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;
            string strPk = lblRequestID.Text;

            DropDownList ddlReject_To = grv.FindControl("ddlReject_To") as DropDownList;
            //CheckBoxList ChkReject_To = grv.FindControl("ChkReject_To") as CheckBoxList;
            TextBox txtRejectionNote = grv.FindControl("txtRejectionNote") as TextBox;

            Utility objUtil = new Utility();

            try
            {
                string strReject = "";

                if (lblRejectionType.Text == "M")
                {
                    //for (int i = 0; i < ChkReject_To.Items.Count; i++)
                    //{
                    //    if (ChkReject_To.Items[i].Selected)
                    //    {
                    //        if (strReject == "")
                    //            strReject = ChkReject_To.Items[i].Value;
                    //        else
                    //            strReject += "," + ChkReject_To.Items[i].Value;
                    //    }
                    //}
                }
                else
                {
                    strReject = ddlReject_To.SelectedValue;
                }


                if (objMatAccess.RollbackRequest(strPk, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), strReject, objUtil.ReplaceEscapeSequenceChar(txtRejectionNote.Text), lblUserId.Text) > 0)
                {
                    Label lblMassRequestProcessDetailId = (Label)grv.FindControl("lblMassRequestProcessDetailId");
                    objMatAccess.UngroupRequestFromMassProcess(lblMassRequestProcessDetailId.Text, lblUserId.Text);

                    ReadBOMRecipeMasterRequests("");

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
                //throw ex;
                _log.Error("btnReject_Ind_Click", ex);
            }

        }
        catch (Exception ex)
        { _log.Error("btnReject_Ind_Click", ex); }
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grv = (GridViewRow)((Button)sender).Parent.Parent;
            Label lblRequestID = grv.FindControl("lblPrimaryID") as Label;

            if (objMatAccess.UpdateRequestComplete(lblRequestID.Text, lblUserId.Text) > 0)
            {
                lblMsg.Text = "Request updated Sucessfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;

                ReadBOMRecipeMasterRequests();
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

                    //CheckBoxList ChkReject_To = grv.FindControl("ChkReject_To") as CheckBoxList;
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
                        grdSearch.Columns[6].Visible = true;
                        grdSearch.Columns[7].Visible = true;

                        if (btnSubmit.Visible && MasterAccess.Get_IsFinalApproval(lblRequestID.Text, lblUserId.Text) && Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "11")
                            btnSubmit.Visible = false;

                        if (!objMatAccess.IsSAPintegrationPending(lblRequestID.Text))
                        {
                            DataSet ds = objMatAccess.ReadDeparmentListForRollback(lblRequestID.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblModuleId.Text);

                            ddlReject_To.DataSource = ds;
                            ddlReject_To.DataTextField = "LevelName";
                            ddlReject_To.DataValueField = "Sequence";
                            ddlReject_To.DataBind();

                            //ChkReject_To.DataSource = ds;
                            //ChkReject_To.DataTextField = "LevelName";
                            //ChkReject_To.DataValueField = "Sequence";
                            //ChkReject_To.DataBind();

                            if (lblRejectionType.Text == "M")
                            {
                                ddlReject_To.Visible = false;
                                reqddlReject_To.Visible = false;
                                //ChkReject_To.Visible = true;
                            }
                            else
                            {
                                ddlReject_To.Visible = true;
                                reqddlReject_To.Visible = true;
                                //ChkReject_To.Visible = false;
                            }


                            txtRejectionNote.Visible = true;
                            btnReject_Ind.Visible = true;
                            btnComplete.Visible = false;
                        }
                        else
                        {
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                ddlReject_To.Visible = false;
                                //ChkReject_To.Visible = false;
                                txtRejectionNote.Visible = false;
                                btnReject_Ind.Visible = false;
                                btnComplete.Visible = true;

                            }
                            else
                            {
                                DataSet ds = objMatAccess.ReadDeparmentListForRollback(lblRequestID.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblModuleId.Text);

                                ddlReject_To.DataSource = ds;
                                ddlReject_To.DataTextField = "LevelName";
                                ddlReject_To.DataValueField = "Sequence";
                                ddlReject_To.DataBind();

                                //ChkReject_To.DataSource = ds;
                                //ChkReject_To.DataTextField = "LevelName";
                                //ChkReject_To.DataValueField = "Sequence";
                                //ChkReject_To.DataBind();

                                btnComplete.Visible = false;
                            }
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

    //protected void btnDone_Click(object sender, EventArgs e)
    //{
    //    Session[StaticKeys.SAPUserName] = txtUserName.Text;
    //    Session[StaticKeys.SAPPassword] = txtPassword.Text;

    //    ClientScript.RegisterStartupScript(GetType(), "key", "ShowSAPQAUploadPopup();", true);
    //}

    #endregion

    #region Methods

    private void ReadBOMRecipeMasterRequests(string Action = "")
    {
        Utility ObjUtil = new Utility();
        pnlMsg.Visible = false;
        DataSet dstData;
        try
        {
            dstData = objBOMRecipeAccess.GetBOMRecipeListByMassProcessID(lblMassRequestProcessId.Text, lblUserId.Text);
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

                    //if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2" || (Session[StaticKeys.LoggedIn_User_FullName].ToString() == Session[StaticKeys.Requestor_User_Name].ToString()))
                    //{
                    //    btnSubmit.Text = "Submit All";
                    //    btnUnGroup.Visible = true;
                    //    btnRejectTo.Visible = false;
                    //}
                    //else
                    //{
                    //    //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11" && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                    //    if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                    //    {
                    //        //BOM_8200050878 for new Module created 227,228,229
                    //        //manali chavan
                    //        if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                    //        {
                    //            UpdateRecipeProdStatusLock();
                    //            if (!objMatAccess.IsSAPQAIntegrationPending(lblMasterHeaderId.Text))
                    //            { btnSAPQAUpload.Visible = true; }
                    //            else
                    //            {
                    //                btnSubmit.Visible = true;
                    //                btnSubmit.Text = "Approve";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            btnSubmit.Visible = true;
                    //            btnSubmit.Text = "Approve All";
                    //        }
                    //    }
                    //    else
                    //        btnSubmit.Text = "Approve All";
                    //    btnRejectTo.Visible = objMatAccess.IsUserInitiator(lblModuleId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text);
                    //}

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
                            if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text, lblMassRequestProcessId.Text) || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                btnSubmit.Visible = false;
                                //Validating the BOM before integration Start
                                //btnSAPUpload.Visible = true;
                                if ((Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                                {
                                    if (objMatAccess.IsMassSAPValidationPending(lblMassRequestProcessId.Text))
                                        btnSAPValidate.Visible = true;
                                    else
                                        btnSAPUpload.Visible = true;
                                }
                                else
                                    btnSAPUpload.Visible = true;
                                //Validating the BOM before integration End

                                //string Module = Session[StaticKeys.SelectedModuleId].ToString();

                                if (btnRejectTo.Visible)
                                {
                                    //lblRejectionType.Text = "M";
                                    //tdChkReject.Visible = true;
                                    tdDdlReject.Visible = true;
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

                    if (Session[StaticKeys.LoggedIn_User_Profile_Id].ToString() == "2" || (Session[StaticKeys.LoggedIn_User_FullName].ToString() == Session[StaticKeys.Requestor_User_Name].ToString()))
                    {
                        btnSubmit.Text = "Submit All";
                        btnUnGroup.Visible = true;
                        btnRejectTo.Visible = false;
                    }
                    else
                    {
                        //if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11" && MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                        if (MasterAccess.Get_IsFinalApproval(lblMasterHeaderId.Text, lblUserId.Text))
                        {
                            //BOM_8200050878 for new Module created 227,228,229
                            //manali chavan
                            if ((Session[StaticKeys.SelectedModuleId].ToString() == "180") || (Session[StaticKeys.SelectedModuleId].ToString() == "185") || (Session[StaticKeys.SelectedModuleId].ToString() == "186") || (Session[StaticKeys.SelectedModuleId].ToString() == "227") || (Session[StaticKeys.SelectedModuleId].ToString() == "228") || (Session[StaticKeys.SelectedModuleId].ToString() == "229"))
                            {
                                UpdateRecipeProdStatusLock();
                                if (!objMatAccess.IsSAPQAIntegrationPending(lblMasterHeaderId.Text))
                                { btnSAPQAUpload.Visible = true;
                                    btnSAPUpload.Visible = false;
                                    btnSAPValidate.Visible = false;
                                    btnSubmit.Visible = false;
                                }
                                else
                                {
                                    btnSAPQAUpload.Visible = false;
                                    btnSAPUpload.Visible = false;
                                    btnSAPValidate.Visible = false;
                                    btnSubmit.Visible = true;
                                    btnSubmit.Text = "Approve";
                                }
                            }
                            else
                            {
                                btnSAPQAUpload.Visible = false;
                                btnSAPUpload.Visible = false;
                                btnSAPValidate.Visible = false;
                                btnSubmit.Visible = true;
                                btnSubmit.Text = "Approve All";
                            }
                        }
                        else
                            btnSubmit.Text = "Approve All";
                        btnRejectTo.Visible = objMatAccess.IsUserInitiator(lblModuleId.Text, Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text);
                    }

                }
                else
                {
                    btnRejectTo.Visible = false;
                    btnSubmit.Visible = false;
                    btnSAPUpload.Visible = false;
                    btnModify.Visible = false;
                }

                if (btnRejectTo.Visible)
                {
                    grdSearch.Columns[6].Visible = true;
                    grdSearch.Columns[7].Visible = true;
                }
                else
                {
                    grdSearch.Columns[6].Visible = false;
                    grdSearch.Columns[7].Visible = false;
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
            //throw ex;
            _log.Error("ReadBOMRecipeMasterRequests", ex);
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
                    Label lblPlantGroupId = grv.FindControl("lblPlantGroupId") as Label;
                    Label lblRequestType = grv.FindControl("lblRequestType") as Label;
                    Label lblPlantType = grv.FindControl("lblPlantType") as Label;

                    Session[StaticKeys.SelectedModuleId] = lblModuleId.Text;
                    Session[StaticKeys.SelectedModule] = lblModuleName.Text;
                    Session[StaticKeys.RequestNo] = lblRequestNo.Text + " - " + lblRequest_No.Text;
                    Session[StaticKeys.ActionType] = lblActionType.Text;
                    Session[StaticKeys.MaterialPlantId] = lblPlantId.Text;
                    Session[StaticKeys.MatPlantGrp] = lblPlantGroupId.Text;
                    Session[StaticKeys.PlantType] = lblPlantType.Text;

                    Session[StaticKeys.Requestor_User_Name] = lblCreatedBy.Text;
                    Session[StaticKeys.Requestor_Location] = lblLocation.Text;
                    Session[StaticKeys.Requestor_ContactNo] = lblContactNo.Text;

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
            //throw ex;
            _log.Error("ReadDeparmentListForRollback", ex);
        }
    }

    private void UpdateRecipeProdStatusLock()
    {
        RecipeAccess objRecipeAccess = new RecipeAccess();
        try
        {
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "11")
            {
                objRecipeAccess.UpdateMassProdLockStatus(lblMassRequestProcessId.Text, lblUserId.Text);
            }
        }
        catch (Exception ex)
        { _log.Error("UpdateRecipeProdStatusLock", ex); }
    }

    #endregion
}