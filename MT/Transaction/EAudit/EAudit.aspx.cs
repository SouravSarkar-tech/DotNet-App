using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;
using System.Data.SqlClient;
using Accenture.MWT.DomainObject;
using System.IO;
using System.Text.RegularExpressions;
using Saplin.Controls;
using System.Collections;

public partial class Transaction_EAudit_EAudit : BasePage
{
    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
    VendorGeneralAccess ObjVendorGeneralAccess = new VendorGeneralAccess();
    EAuditAccess objAuditAccess = new EAuditAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
			//Start Added By Nitish Rao 10/08/2018 for Maintaining scrolling position
            Page.MaintainScrollPositionOnPostBack = true;
			//End Added By Nitish Rao 10/08/2018 for Maintaining scrolling position
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                lblFrom.Text = Session[StaticKeys.Requestor_User_Name].ToString();
                if (Session[StaticKeys.SelectedModuleId] != null)
                {
                    lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                    //PopulateCheckBoxList();

                    string sectionId = lblSectionId.Text.ToString();
                    string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                    string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                    string mode = Session[StaticKeys.Mode].ToString();
                    lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                    lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();
                    PopulateDropDownList();
                    FillBasicAuditData();
                    //if (lblEAuditId.Text == "0")
                    //{
                    //    if (Save())
                    //    {
                    //        FillBasicAuditData();
                    //    }
                    //}
                    FillMaterialsGrid();
                    ConfigureControls();
					//Added by Nitish Rao 10/08/2018 for Manufacture Change
                    AuditData objAuditData = GetBasicAuditData();
                    if (objAuditData.EAudit_Form_Id > 0)
                    {
                        DataSet dstData = new DataSet();

                        dstData = objAuditAccess.GetMaterialsData(Convert.ToInt32(lblEAuditId.Text));
                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            btnNext.Enabled = false;
                        }
                    }
                    else
                    {
                        btnNext.Enabled = false;
                    }
                    //FillProductsGrid();
                    bool depid = Getdepartmentid_byworkflow(lblMasterHeaderId.Text);
                    Session["IsreviewerSavedData"] = "";
                    if (depid)
                    {
                        trRnD.Visible = true;
                        trRA.Visible = true;
                    }
                    else
                    {
                        trRnD.Visible = false;
                        trRA.Visible = false;
                    }
					//End Added by Nitish Rao 10/08/2018 for Manufacture Change
                    if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                    {
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0")
                        {
                            //trButton.Visible = true;
							//Start Added by Nitish Rao 10/08/2018 for Manufacture Change
                            btnSave.Visible = true;
                            btnNext.Visible = true;
                            grdAttachedDocs.Columns[1].Visible = true;
                            file_upload.Visible = true;
                            btnupload.Visible = true;
                            BindAttachedDocuments(lblMasterHeaderId.Text);
                            trAddNewMatRow.Visible = true;
                            lblCQAComm.Visible = true;
                            lblCQAComm_Desc.Visible = true;
                            getCQAComment(lblMasterHeaderId.Text);
							//End Added by Nitish Rao 10/08/2018 for Manufacture Change
                        }
                        else
                        {
                            btnSave.Visible = true;
                            btnNext.Visible = false;
                            grdAttachedDocs.Columns[1].Visible = false;
                            file_upload.Visible = false;
							//Start Added by Nitish Rao 10/08/2018 for Manufacture Change
                            Requiredfile_upload.Enabled = false;
                            btnupload.Visible = false;
                            lblCQAComm.Visible = false;
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "20" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "22")
                            {
                                lblCQAComm.Visible = true;
                                lblCQAComm_Desc.Visible = true;
                                getCQAComment(lblMasterHeaderId.Text);
                            }
                            else
                            {
                                lblCQAComm.Visible = false;
                                lblCQAComm_Desc.Visible = false;
                                lblCQAComm_Desc.Text = "";
                            }
							//End Added by Nitish Rao 10/08/2018 for Manufacture Change
                        }

                    }
                    else
                    {
                        grdAttachedDocs.Columns[1].Visible = false;
                        file_upload.Visible = false;
						//Start Added by Nitish Rao 10/08/2018 for Manufacture Change
                        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "0")
                        {
                            lblCQAComm.Visible = true;
                            lblCQAComm_Desc.Visible = true;
                            getCQAComment(lblMasterHeaderId.Text);
                        }
                        else
                        {
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "20" || Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "22")
                            {
                                lblCQAComm.Visible = true;
                                lblCQAComm_Desc.Visible = true;
                                getCQAComment(lblMasterHeaderId.Text);
                            }
                            else
                            {
                                lblCQAComm.Visible = false;
                                lblCQAComm_Desc.Visible = false;
                                lblCQAComm_Desc.Text = "";
                            }
                        }
						//End Added by Nitish Rao 10/08/2018 for Manufacture Change
                    }
                }
                else
                {
                    Response.Redirect("EAuditMaster.aspx");
                }

            }
        }
    }

    private bool Getdepartmentid_byworkflow(string text)
    {
        bool deptid = false;
        string str = "";
        try
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = objDal.FillDataSet("select w.Department_Id from T_Master_Header M inner join M_Approval_WorkFlow W on M.Workflow_Code = w.WorkFlow_Code where Master_Header_Id = '" + text + "' and w.Department_Id in ('20','22')", "table");
            if(dstData.Tables[0].Rows.Count != 0)
            {
                deptid = true;
            }
        }
        catch (Exception ex)
        {
            deptid = false;
        }
        return deptid;
    }

    private void getCQAComment(string text)
    {
        try
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = objDal.FillDataSet("select Remarks from T_Approval_Status where Master_Header_Id='" + text + "' and Approved_By_Dept_Id = '23'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                lblCQAComm_Desc.Text = row[0].ToString();
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
    {
        FillVendorData();
        //FillMaterialsGrid();
        //FillProductsGrid();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
			// Start Added by Nitish Rao 10/08/2018 for Manufacture Change
            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "0" && Session[StaticKeys.LoggedIn_User_DeptId].ToString() != "19")
            {
                Session["IsreviewerSavedData"] = "Y";
            }
            else
            {
                Session["IsreviewerSavedData"] = "N";
                btnNext.Enabled = true;
            }
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            //Button sub = (Button)Master.FindControl("btnSubmit");
            //sub.Enabled = true;
        }
        else
        {
            Session["IsreviewerSavedData"] = "N";
            btnNext.Enabled = false;
        }
		//End Added by Nitish Rao 10/08/2018 for Manufacture Change
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
		//Start Added by Nitish Rao 10/08/2018 for Manufacture Change
        AuditData objAuditData = GetBasicAuditData();

        if (objAuditData.EAudit_Form_Id > 0)
        {
            DataSet dstData = new DataSet();

            dstData = objAuditAccess.GetMaterialsData(Convert.ToInt32(lblEAuditId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                EAuditAccess ObjAuditMasterAccess = new EAuditAccess();
                if (ObjAuditMasterAccess.GenerateMassRequestProcess(lblMasterHeaderId.Text + "/", Session[StaticKeys.LoggedIn_User_DeptId].ToString(), lblUserId.Text) > 0)
                {
                    lblMsg.Text = "Request Generated Successfully";
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
                Session[StaticKeys.AddAlertMsg] = " Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.";

                Type cstype = this.GetType();

                //Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                //Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    //String cstext = "alert('Please click on Submit to send the request for processing.');";
                    //String cstext = "if(confirm('Proceed for submiting?')){RequestSubmitPage();};";
                    //String cstext = "if(confirm('Proceed for submiting? Click cancel to continue editing')){RequestSubmitPage();};";
                    String cstext = "if(!confirm(' Request No: " + Session[StaticKeys.RequestNo].ToString() + " created.'));";
                    //String cstext = "if(confirm('You are about to delete 5 rows. \nWARNING: Strawberry cakes won\'t be effected!)){RequestSubmitPage();};";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "RequestPage", "RequestPage()", true);
                //Response.Redirect("EAuditMaster.aspx");{RequestSubmitPage();}
            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Please fill required field Data";
                pnlMsg.CssClass = "error";
            }
        }
        else
        {
            pnlMsg.Visible = true;
            lblMsg.Text = "Please fill required field Data";
            pnlMsg.CssClass = "error";
        }
		//End Added by Nitish Rao 10/08/2018 for Manufacture Change
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRegionData();
        ddlRegion.Focus();
    }

    //protected void ddlYesNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlYesNo.SelectedValue.ToString() == "1")
    //    {
    //        helperAccess.PopuplateDropDownList(ddlMthdAnalysis, "pr_GetDropDownListByControlNameModuleType 'E','ddlMthdAnalysis'", "LookUp_Desc", "LookUp_Code", "");
    //        ddlMthdAnalysis.Enabled = true;
    //        lblMthd.Visible = true;
    //        reqddlMthdAnalysis.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlMthdAnalysis.SelectedValue = "";
    //        ddlMthdAnalysis.Enabled = false;
    //        lblMthd.Visible = false;
    //        reqddlMthdAnalysis.Enabled = false;
    //    }
    //    FillMaterialsGrid();
    //    FillProductsGrid();
    //}

    protected void lnkRefreshddlMarket_Click(object sender, EventArgs e)
    {
        DisplayMarket();
    }

    protected void ddlMarket_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayMarket();
    }

    protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
    {
        ExpectedCompletion();
    }

    protected void grdAttachedDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            DataAccessLayer objDb = new DataAccessLayer();
            SqlTransaction objTrans;
            Control ctl = e.CommandSource as Control;
            GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
            string documentId = grdAttachedDocs.DataKeys[CurrentRow.RowIndex].Value.ToString();
            Label lblUploadedFileName = grdAttachedDocs.Rows[CurrentRow.RowIndex].FindControl("lblUploadedFileName") as Label;

            try
            {
                objDb.OpenConnection(this.Page);
                objTrans = objDb.cnnConnection.BeginTransaction();

                if (objDb.DeleteRecord("T_Document_Upload", "Document_Upload_Id=" + documentId, ref objDb.cnnConnection, ref objTrans))
                {
                    System.IO.File.Delete(Server.MapPath("EAuditDocuments") + "/" + lblUploadedFileName.Text);
                    objTrans.Commit();
                    pnlMsg.Visible = false;
                    BindAttachedDocuments(lblMasterHeaderId.Text);
                }
                else
                {
                    objTrans.Rollback();
                    lblMsg.Text = "Error While Deleting File.";
                    pnlMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDb.CloseConnection(objDb.cnnConnection);
                objDb = null;
                objTrans = null;
            }
        }
    }

    protected void ddlReasonAudit_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetupReasonAudit();
    }

    protected void ddlLupinLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownCheckBoxes ddl = (DropDownCheckBoxes)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        string loc = "";
        string loc_text = "";
        loc = GetSelectedCheckedValue(ddl);
        loc_text = GetSelectedCheckeditem(ddl);
        ddl.Texts.SelectBoxCaption = "";
        //lblglocation.Text = "Lupin's Location :-" + loc_text;
        ddl.Texts.SelectBoxCaption = loc_text;
        TextBox txtOtherLC = (TextBox)row.FindControl("txtOtherLC");
        RequiredFieldValidator reqtxtOtherLC = (RequiredFieldValidator)row.FindControl("reqtxtOtherLC");
        try
        {
            if (loc.Contains("OTH"))
            {
                txtOtherLC.Enabled = true;
                reqtxtOtherLC.Enabled = true;
            }
            else
            {
                txtOtherLC.Enabled = false;
                reqtxtOtherLC.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void ddlPharmaStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownCheckBoxes ddl = (DropDownCheckBoxes)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        string PharmaStatus = "";
        string PharmaStatus_text = "";
        PharmaStatus = GetSelectedCheckedValue(ddl);
        PharmaStatus_text = GetSelectedCheckeditem(ddl);
        ddl.Texts.SelectBoxCaption = "";
        ddl.Texts.SelectBoxCaption = PharmaStatus_text;
        TextBox txtOtherPharmaStatus = (TextBox)row.FindControl("txtOtherPharmaStatus");
        RequiredFieldValidator reqOtherPharmaStatus = (RequiredFieldValidator)row.FindControl("reqOtherPharmaStatus");
        try
        {
            if (PharmaStatus.Contains("Others"))
            {
                txtOtherPharmaStatus.Enabled = true;
                reqOtherPharmaStatus.Enabled = true;
            }
            else
            {
                txtOtherPharmaStatus.Enabled = false;
                reqOtherPharmaStatus.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlMthdAnalysis_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownCheckBoxes ddl = (DropDownCheckBoxes)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        string MthdAnalysis = "";
        string MthdAnalysis_text = "";
        MthdAnalysis = GetSelectedCheckedValue(ddl);
        MthdAnalysis_text = GetSelectedCheckeditem(ddl);
        ddl.Texts.SelectBoxCaption = "";
        ddl.Texts.SelectBoxCaption = MthdAnalysis_text;
        TextBox txtOtherMthdAnalysis = (TextBox)row.FindControl("txtOtherMthdAnalysis");
        RequiredFieldValidator reqtxtOtherMthdAnalysis = (RequiredFieldValidator)row.FindControl("reqtxtOtherMthdAnalysis");

        try
        {
            if (MthdAnalysis.Contains("OTH"))
            {
                txtOtherMthdAnalysis.Enabled = true;
                reqtxtOtherMthdAnalysis.Enabled = true;
            }
            else
            {
                txtOtherMthdAnalysis.Enabled = false;
                reqtxtOtherMthdAnalysis.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlMatCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        string MatCategory = "";
        string MatCategory_text = "";
        MatCategory_text = ddl.SelectedItem.ToString().Trim();
        //MatCategory = GetSelectedCheckedValue(ddl);
        MatCategory = ddl.SelectedValue.ToString().Trim();

        //lblgMaterial.Text = "";
        //lblgMaterial.Text = "Material Catagory :-" + MatCategory_text;
        TextBox txtOtherMatCategory = (TextBox)row.FindControl("txtOtherMatCategory");
        RequiredFieldValidator reqtxtOtherMatCategory = (RequiredFieldValidator)row.FindControl("reqtxtOtherMatCategory");
        try
        {
            if (MatCategory == "OTH")
            {
                txtOtherMatCategory.Enabled = true;
                reqtxtOtherMatCategory.Enabled = true;
            }
            else
            {
                txtOtherMatCategory.Enabled = false;
                reqtxtOtherMatCategory.Enabled = false;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddBlankRow();
    }

    //protected void ddlQAgreement_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlQAgreement.SelectedValue.ToString() == "N")
    //    {
    //        txtJustification.Enabled = true;
    //        lblJustification.Visible = true;
    //        reqtxtJustification.Enabled = true;
    //    }
    //    else
    //    {
    //        txtJustification.Enabled = false;
    //        lblJustification.Visible = false;
    //        reqtxtJustification.Enabled = false;
    //    }
    //}

    #endregion

    #region Material Data

    private void FillMaterialsGrid()
    {
        DataSet dstData = new DataSet();

        dstData = objAuditAccess.GetMaterialsData(Convert.ToInt32(lblEAuditId.Text));
        if (dstData.Tables[0].Rows.Count > 0)
        {
            grdMaterials.DataSource = dstData.Tables[0].DefaultView;
            grdMaterials.DataBind();
        }
        else
        {
            //dstData.Tables[0].Rows.Add(dstData.Tables[0].NewRow());
            //grdMaterials.DataSource = dstData.Tables[0].DefaultView;
            //grdMaterials.DataBind();

            //int TotalColumns = grdMaterials.Rows[0].Cells.Count;
            //grdMaterials.Rows[0].Cells.Clear();
            //grdMaterials.Rows[0].Cells.Add(new TableCell());
            //grdMaterials.Rows[0].Cells[0].ColumnSpan = TotalColumns;
            //grdMaterials.Rows[0].Cells[0].Text = "No Record Found";
            AddBlankRow();

        }
    }

    private void AddBlankRow()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        int tempId = 1;
        int j = 0;
        string positionNo = "";
		//Start Added by Nitish Rao 10/08/2018 for Manufacture Change
        lblgridmsg.Visible = false;
        lblgridmsg.Text = "";
		//End Added by Nitish Rao 10/08/2018 for Manufacture Change
        try
        {
            //Columns
            dt.Columns.Add(new DataColumn("E_Audit_Material_Id"));
            dt.Columns.Add(new DataColumn("SerialNo"));
            dt.Columns.Add(new DataColumn("Material_Name"));
            dt.Columns.Add(new DataColumn("Product_Name"));
            dt.Columns.Add(new DataColumn("LupinLoc"));
            dt.Columns.Add(new DataColumn("Other_LupinLoc"));       // Added by Nitish Rao 10/08/2018 for Manufacture Change
            dt.Columns.Add(new DataColumn("Pharmacopical_Status"));
            dt.Columns.Add(new DataColumn("Other_Pharmacopical_Status")); // Added by Nitish Rao 10/08/2018 for Manufacture Change
            dt.Columns.Add(new DataColumn("AnalysisMethod"));
            dt.Columns.Add(new DataColumn("Other_AnalysisMethod")); // Added by Nitish Rao 10/08/2018 for Manufacture Change
            dt.Columns.Add(new DataColumn("MaterialCategory"));
            dt.Columns.Add(new DataColumn("Other_MaterialCategory")); // Added by Nitish Rao 10/08/2018 for Manufacture Change

            foreach (GridViewRow row in grdMaterials.Rows)
            {
                dr = dt.NewRow();
                dr["E_Audit_Material_Id"] = (row.FindControl("lblE_Audit_Material_Id") as Label).Text;
                dr["SerialNo"] = (row.FindControl("txtSNo") as TextBox).Text;
                dr["Material_Name"] = (row.FindControl("txtMatName") as TextBox).Text;
                dr["Product_Name"] = (row.FindControl("txtProdName") as TextBox).Text;
                //dr["LupinLoc"] = (row.FindControl("ddlLupinLoc") as DropDownList).SelectedValue;
                dr["LupinLoc"] = GetSelectedCheckedValue(row.FindControl("ddlLupinLoc") as DropDownCheckBoxes) == null ? "" : GetSelectedCheckedValue(row.FindControl("ddlLupinLoc") as DropDownCheckBoxes);

                dr["Other_LupinLoc"] = (row.FindControl("txtOtherLC") as TextBox).Text;
                dr["Pharmacopical_Status"] = GetSelectedCheckedValue(row.FindControl("ddlPharmaStatus") as DropDownCheckBoxes) == null ? "" : GetSelectedCheckedValue(row.FindControl("ddlPharmaStatus") as DropDownCheckBoxes);
                dr["Other_Pharmacopical_Status"] = (row.FindControl("txtOtherPharmaStatus") as TextBox).Text;
                //dr["AnalysisMethod"] = (row.FindControl("ddlMthdAnalysis") as DropDownList).SelectedValue;
                dr["AnalysisMethod"] = GetSelectedCheckedValue(row.FindControl("ddlMthdAnalysis") as DropDownCheckBoxes) == null ? "" : GetSelectedCheckedValue(row.FindControl("ddlMthdAnalysis") as DropDownCheckBoxes);
                dr["Other_AnalysisMethod"] = (row.FindControl("txtOtherMthdAnalysis") as TextBox).Text;
                dr["MaterialCategory"] = (row.FindControl("ddlMatCategory") as DropDownList).SelectedValue;
                dr["Other_MaterialCategory"] = (row.FindControl("txtOtherMatCategory") as TextBox).Text;

                dt.Rows.Add(dr);
                //positionNo = String.Format("{0:0000}", Convert.ToInt32(dr["Postion_No"]) + 10);
                tempId += 1;
            }
            int count = 0;
            if (txtNewRow.Text.Trim() == "")
            {
                txtNewRow.Text = "0";
            }
            count = dt.Rows.Count + Convert.ToInt32(txtNewRow.Text.Trim());
            if (dt.Rows.Count <= 5 && count <= 5)
            {
                for (int i = tempId; i < SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text) + tempId; i++)
                //for (int i = 0; i < 10; i++)
                {

                    dr = dt.NewRow();
                    dr["E_Audit_Material_Id"] = tempId;
                    dr["SerialNo"] = i;

                    dt.Rows.Add(dr);

                }

                dstData.Tables.Add(dt);
                dstData.AcceptChanges();

                DataView dv = new DataView(dstData.Tables[0]);
                dv.Sort = "SerialNo Asc";
                DataTable dtSorted = dv.ToTable();

                grdMaterials.DataSource = dtSorted;
                grdMaterials.DataBind();
                ViewState["dstMaterials"] = dstData;
            }
            else
            {
                lblgridmsg.Visible = true;
                lblgridmsg.Text = "More than 5 Rows not allowed";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataSet Pharmo = objAuditAccess.GetPharmaData();
        DataSet AnalysisMthd = objAuditAccess.GetAnalysisData();
        DataSet MatCategory = objAuditAccess.GetMatCategoryData();
        DataSet LupinLoc = objAuditAccess.GetLupinLoc();

        DataSet dstData = objAuditAccess.GetMaterialsData(Convert.ToInt32(lblEAuditId.Text));
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownCheckBoxes ddlPharmaStatus = (DropDownCheckBoxes)e.Row.FindControl("ddlPharmaStatus");

            if (ddlPharmaStatus != null)
            {
                ddlPharmaStatus.DataSource = Pharmo;
                ddlPharmaStatus.DataTextField = "LookUp_Desc";
                ddlPharmaStatus.DataValueField = "LookUp_Code";
                ddlPharmaStatus.DataBind();
                SetSelectedValue(ddlPharmaStatus, grdMaterials.DataKeys[e.Row.RowIndex].Values[1].ToString());
                string PharmaStatus_text = "";
                PharmaStatus_text = GetSelectedCheckeditem(ddlPharmaStatus);
                ddlPharmaStatus.Texts.SelectBoxCaption = PharmaStatus_text;
            }

            //DropDownList ddlMthdAnalysis = (DropDownList)e.Row.FindControl("ddlMthdAnalysis");
            DropDownCheckBoxes ddlMthdAnalysis = (DropDownCheckBoxes)e.Row.FindControl("ddlMthdAnalysis");

            if (ddlMthdAnalysis != null)
            {
                ddlMthdAnalysis.DataSource = AnalysisMthd;
                ddlMthdAnalysis.DataTextField = "LookUp_Desc";
                ddlMthdAnalysis.DataValueField = "LookUp_Code";
                ddlMthdAnalysis.DataBind();
                //ddlMthdAnalysis.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[2].ToString();
                SetSelectedValue(ddlMthdAnalysis, grdMaterials.DataKeys[e.Row.RowIndex].Values[2].ToString());
                string MthdAnalysis_text = "";
                MthdAnalysis_text = GetSelectedCheckeditem(ddlMthdAnalysis);
                ddlMthdAnalysis.Texts.SelectBoxCaption = MthdAnalysis_text;
            }

            DropDownList ddlMatCategory = (DropDownList)e.Row.FindControl("ddlMatCategory");
            if (ddlMatCategory != null)
            {
                ddlMatCategory.DataSource = MatCategory;
                ddlMatCategory.DataTextField = "LookUp_Desc";
                ddlMatCategory.DataValueField = "LookUp_Code";
                ddlMatCategory.DataBind();
                ddlMatCategory.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[3].ToString();
                ddlMatCategory.Items.Insert(0, new ListItem("---Select---", "0"));
            }

            // DropDownList ddlLupinLoc = (DropDownList)e.Row.FindControl("ddlLupinLoc");
            DropDownCheckBoxes ddlLupinLoc = (DropDownCheckBoxes)e.Row.FindControl("ddlLupinLoc");
            if (ddlLupinLoc != null)
            {
                ddlLupinLoc.DataSource = LupinLoc;
                ddlLupinLoc.DataTextField = "LookUp_Desc";
                ddlLupinLoc.DataValueField = "LookUp_Code";
                ddlLupinLoc.DataBind();
                //ddlLupinLoc.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[4].ToString();
                SetSelectedValue(ddlLupinLoc, grdMaterials.DataKeys[e.Row.RowIndex].Values[4].ToString());
                string loc_text = "";
                loc_text = GetSelectedCheckeditem(ddlLupinLoc);
                ddlLupinLoc.Texts.SelectBoxCaption = loc_text;
            }
            // Start Adding by Nitish Rao 10/08/2018 for Manufacture change
            string loc = GetSelectedCheckedValue(ddlLupinLoc);
            TextBox txtOtherLC = (TextBox)e.Row.FindControl("txtOtherLC");
            RequiredFieldValidator reqtxtOtherLC = (RequiredFieldValidator)e.Row.FindControl("reqtxtOtherLC");

            string PharmaStatus = GetSelectedCheckedValue(ddlPharmaStatus);
            TextBox txtOtherPharmaStatus = (TextBox)e.Row.FindControl("txtOtherPharmaStatus");
            RequiredFieldValidator reqOtherPharmaStatus = (RequiredFieldValidator)e.Row.FindControl("reqOtherPharmaStatus");

            string MthdAnalysis = GetSelectedCheckedValue(ddlMthdAnalysis);
            TextBox txtOtherMthdAnalysis = (TextBox)e.Row.FindControl("txtOtherMthdAnalysis");
            RequiredFieldValidator reqtxtOtherMthdAnalysis = (RequiredFieldValidator)e.Row.FindControl("reqtxtOtherMthdAnalysis");

            string lMatCategory = ddlMatCategory.SelectedValue.ToString().Trim();
            TextBox txtOtherMatCategory = (TextBox)e.Row.FindControl("txtOtherMatCategory");
            RequiredFieldValidator reqtxtOtherMatCategory = (RequiredFieldValidator)e.Row.FindControl("reqtxtOtherMatCategory");

            try
            {
                if (loc.Contains("OTH"))
                {
                    txtOtherLC.Enabled = true;
                    reqtxtOtherLC.Enabled = true;
                }
                else
                {
                    txtOtherLC.Enabled = false;
                    reqtxtOtherLC.Enabled = false;
                    txtOtherLC.Text = "";
                }

                if (PharmaStatus.Contains("Others"))
                {
                    txtOtherPharmaStatus.Enabled = true;
                    reqOtherPharmaStatus.Enabled = true;

                }
                else
                {
                    txtOtherPharmaStatus.Enabled = false;
                    reqOtherPharmaStatus.Enabled = false;
                    txtOtherPharmaStatus.Text = "";
                }

                if (MthdAnalysis.Contains("OTH"))
                {
                    txtOtherMthdAnalysis.Enabled = true;
                    reqtxtOtherMthdAnalysis.Enabled = true;

                }
                else
                {
                    txtOtherMthdAnalysis.Enabled = false;
                    reqtxtOtherMthdAnalysis.Enabled = false;
                    txtOtherMthdAnalysis.Text = "";
                }

                if (lMatCategory == "OTH")
                {
                    txtOtherMatCategory.Enabled = true;
                    reqtxtOtherMatCategory.Enabled = true;

                }
                else
                {
                    txtOtherMatCategory.Enabled = false;
                    reqtxtOtherMatCategory.Enabled = false;
                    txtOtherMatCategory.Text = "";
                }
                // End Adding by Nitish Rao 10/08/2018 for Manufacture change
            }
            catch (Exception ex)
            {

            }
        }

    }

    protected void grdMaterials_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Control ctl = e.CommandSource as Control;
        GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        if (e.CommandName == "D")
        {
            try
            {
                dt.Columns.Add(new DataColumn("E_Audit_Material_Id"));
                dt.Columns.Add(new DataColumn("SerialNo"));
                dt.Columns.Add(new DataColumn("Material_Name"));
                dt.Columns.Add(new DataColumn("Product_Name"));
                dt.Columns.Add(new DataColumn("LupinLoc"));
                dt.Columns.Add(new DataColumn("Other_LupinLoc"));
                dt.Columns.Add(new DataColumn("Pharmacopical_Status"));
                dt.Columns.Add(new DataColumn("Other_Pharmacopical_Status"));
                dt.Columns.Add(new DataColumn("AnalysisMethod"));
                dt.Columns.Add(new DataColumn("Other_AnalysisMethod"));
                dt.Columns.Add(new DataColumn("MaterialCategory"));
                dt.Columns.Add(new DataColumn("Other_MaterialCategory"));

                foreach (GridViewRow row in grdMaterials.Rows)
                {
                    dr = dt.NewRow();
                    Label lblUpd_Flag = row.FindControl("lblUpd_Flag") as Label;
                    dr["E_Audit_Material_Id"] = (row.FindControl("lblE_Audit_Material_Id") as Label).Text;
                    dr["SerialNo"] = (row.FindControl("txtSNo") as TextBox).Text;
                    dr["Material_Name"] = (row.FindControl("txtMatName") as TextBox).Text;
                    dr["Product_Name"] = (row.FindControl("txtProdName") as TextBox).Text;

                    //dr["LupinLoc"] = (row.FindControl("ddlLupinLoc") as DropDownList).SelectedValue;
                    dr["LupinLoc"] = (row.FindControl("ddlLupinLoc") as DropDownCheckBoxes).SelectedValue;
                    dr["Other_LupinLoc"] = (row.FindControl("txtOtherLC") as TextBox).Text;
                    dr["Pharmacopical_Status"] = GetSelectedCheckedValue(row.FindControl("ddlPharmaStatus") as DropDownCheckBoxes) == null ? "" : GetSelectedCheckedValue(row.FindControl("ddlPharmaStatus") as DropDownCheckBoxes);
                    dr["Other_Pharmacopical_Status"] = (row.FindControl("txtOtherPharmaStatus") as TextBox).Text;
                    //dr["AnalysisMethod"] = (row.FindControl("ddlMthdAnalysis") as DropDownList).SelectedValue;
                    dr["AnalysisMethod"] = (row.FindControl("ddlMthdAnalysis") as DropDownCheckBoxes).SelectedValue;
                    dr["Other_AnalysisMethod"] = (row.FindControl("txtOtherMthdAnalysis") as TextBox).Text;
                    dr["MaterialCategory"] = (row.FindControl("ddlMatCategory") as DropDownList).SelectedValue;
                    dr["Other_MaterialCategory"] = (row.FindControl("txtOtherMatCategory") as TextBox).Text;

                    if (currentRow == row)
                        lblUpd_Flag.Text = "D";
                    dt.Rows.Add(dr);
                }

                dstData.Tables.Add(dt);
                dstData.AcceptChanges();

                if ((currentRow.FindControl("lblUpd_Flag") as Label).Text == "I" || (currentRow.FindControl("lblUpd_Flag") as Label).Text == "U" || (currentRow.FindControl("lblUpd_Flag") as Label).Text == "D")
                {
                    if (objAuditAccess.GetMaterialDetail((currentRow.FindControl("lblE_Audit_Material_Id") as Label).Text, lblEAuditId.Text) > 0)
                    {
                        AuditMaterialsData objMatData = GetMaterialData(currentRow);
                        DataAccessLayer objDal = new DataAccessLayer();
                        SqlTransaction objTrans;
                        objDal.OpenConnection();
                        objTrans = objDal.cnnConnection.BeginTransaction();
                        if (objAuditAccess.DeleteMaterialData(Convert.ToInt32((currentRow.FindControl("txtSNo") as TextBox).Text), Convert.ToInt32((currentRow.FindControl("lblE_Audit_Material_Id") as Label).Text), ref objDal.cnnConnection, ref objTrans))
                        {
                            objTrans.Commit();
                        }
                        else
                        {
                            objTrans.Rollback();
                            lblMsg.Text = "Error";
                            pnlMsg.Visible = true;
                        }
                    }
                    dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
                    dstData.AcceptChanges();
                }

                DataView dv = new DataView(dstData.Tables[0]);
                dv.Sort = "SerialNo Asc";
                DataTable dtSorted = dv.ToTable();

                grdMaterials.DataSource = dtSorted;
                grdMaterials.DataBind();

                ViewState["dstMaterials"] = dstData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    //protected void grdMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    DataSet Pharmo = objAuditAccess.GetPharmaData();
    //    DataSet AnalysisMthd = objAuditAccess.GetAnalysisData();
    //    DataSet MatCategory = objAuditAccess.GetMatCategoryData();
    //    DataSet LupinLoc = objAuditAccess.GetLupinLoc();

    //    //DataSet dstData = objMatAccess.Testing();
    //    DataSet dstData = objAuditAccess.GetMaterialsData(Convert.ToInt32(lblEAuditId.Text));
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownCheckBoxes ddlPharmaStatus = (DropDownCheckBoxes)e.Row.FindControl("ddlPharmaStatus");
    //        //DropDownList ddlPharmaStatus = (DropDownList)e.Row.FindControl("ddlPharmaStatus");

    //        //helperAccess.PopuplateDropDownCheckBox(ddlMarket, "SELECT LookUp_Desc AS LookUp_Desc,LookUp_Code FROM M_LookUp_Audit WHERE Control_Name = 'ddlPharmo' AND IsActive = 1 and isnull(Is_Hidden,0) <> 1 ", "LookUp_Desc", "LookUp_Code");

    //        if (ddlPharmaStatus != null)
    //        {
    //            ddlPharmaStatus.DataSource = Pharmo;
    //            ddlPharmaStatus.DataTextField = "LookUp_Desc";
    //            ddlPharmaStatus.DataValueField = "LookUp_Code";
    //            ddlPharmaStatus.DataBind();
    //            //ddlPharmaStatus.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[1].ToString();
    //            SetSelectedValue(ddlPharmaStatus, grdMaterials.DataKeys[e.Row.RowIndex].Values[1].ToString());

    //        }

    //        DropDownList ddlMthdAnalysis = (DropDownList)e.Row.FindControl("ddlMthdAnalysis");

    //        if (ddlMthdAnalysis != null)
    //        {
    //            ddlMthdAnalysis.DataSource = AnalysisMthd;
    //            ddlMthdAnalysis.DataTextField = "LookUp_Desc";
    //            ddlMthdAnalysis.DataValueField = "LookUp_Code";
    //            ddlMthdAnalysis.DataBind();
    //            ddlMthdAnalysis.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[1].ToString();

    //        }

    //        DropDownList ddlMatCategory = (DropDownList)e.Row.FindControl("ddlMatCategory");

    //        if (ddlMatCategory != null)
    //        {
    //            ddlMatCategory.DataSource = MatCategory;
    //            ddlMatCategory.DataTextField = "LookUp_Desc";
    //            ddlMatCategory.DataValueField = "LookUp_Code";
    //            ddlMatCategory.DataBind();
    //            ddlMatCategory.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[1].ToString();

    //        }

    //        DropDownList ddlLupinLoc = (DropDownList)e.Row.FindControl("ddlLupinLoc");

    //        if (ddlLupinLoc != null)
    //        {
    //            ddlLupinLoc.DataSource = LupinLoc;
    //            ddlLupinLoc.DataTextField = "LookUp_Desc";
    //            ddlLupinLoc.DataValueField = "LookUp_Code";
    //            ddlLupinLoc.DataBind();
    //            ddlLupinLoc.SelectedValue = grdMaterials.DataKeys[e.Row.RowIndex].Values[1].ToString();

    //        }

    //    }

    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        //DropDownList ddlNewPharmaStatus = (DropDownList)e.Row.FindControl("ddlNewPharmaStatus");
    //        //ddlNewPharmaStatus.DataSource = Pharmo.Tables[0];
    //        //ddlNewPharmaStatus.DataBind();

    //        DropDownCheckBoxes ddlNewPharmaStatus = (DropDownCheckBoxes)e.Row.FindControl("ddlNewPharmaStatus");
    //        ddlNewPharmaStatus.DataSource = Pharmo.Tables[0];
    //        ddlNewPharmaStatus.DataTextField = "LookUp_Desc";
    //        ddlNewPharmaStatus.DataValueField = "LookUp_Code";
    //        ddlNewPharmaStatus.DataBind();

    //        DropDownList ddlNewMthdAnalysis = (DropDownList)e.Row.FindControl("ddlNewMthdAnalysis");
    //        ddlNewMthdAnalysis.DataSource = AnalysisMthd.Tables[0];
    //        ddlNewMthdAnalysis.DataBind();

    //        DropDownList ddlNewMatCategory = (DropDownList)e.Row.FindControl("ddlNewMatCategory");
    //        ddlNewMatCategory.DataSource = MatCategory.Tables[0];
    //        ddlNewMatCategory.DataBind();

    //        DropDownList ddlNewLupinLoc = (DropDownList)e.Row.FindControl("ddlNewLupinLoc");
    //        ddlNewLupinLoc.DataSource = LupinLoc.Tables[0];
    //        ddlNewLupinLoc.DataBind();

    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            TextBox txtNewSNo = (TextBox)e.Row.FindControl("txtNewSNo");
    //            txtNewSNo.Text = Convert.ToString(dstData.Tables[0].Rows.Count + 1);
    //        }
    //    }

    //}       

    //protected void grdMaterials_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    AuditMaterialsData objMatData = new AuditMaterialsData();
    //    Utility objUtil = new Utility();
    //    if (e.CommandName.Equals("Insert"))
    //    {
    //        TextBox txtNewSNo = (TextBox)grdMaterials.FooterRow.FindControl("txtNewSNo");
    //        TextBox txtNewMatName = (TextBox)grdMaterials.FooterRow.FindControl("txtNewMatName");
    //        TextBox txtNewProdName = (TextBox)grdMaterials.FooterRow.FindControl("txtNewProdName");
    //        //DropDownList ddlNewPharmaStatus = (DropDownList)grdMaterials.FooterRow.FindControl("ddlNewPharmaStatus");
    //        DropDownCheckBoxes ddlNewPharmaStatus = (DropDownCheckBoxes)grdMaterials.FooterRow.FindControl("ddlNewPharmaStatus");
    //        DropDownList ddlNewMthdAnalysis = (DropDownList)grdMaterials.FooterRow.FindControl("ddlNewMthdAnalysis");
    //        DropDownList ddlNewMatCategory = (DropDownList)grdMaterials.FooterRow.FindControl("ddlNewMatCategory");
    //        DropDownList ddlNewLupinLoc = (DropDownList)grdMaterials.FooterRow.FindControl("ddlNewLupinLoc");

    //        objMatData.E_Audit_Material_Id = 0;
    //        objMatData.EAudit_Form_Id = Convert.ToInt32(lblEAuditId.Text);
    //        //objMatData.SerialNo = Convert.ToInt32(txtNewSNo.Text);
    //        objMatData.SerialNo = 0;
    //        objMatData.Material_Name = txtNewMatName.Text;
    //        //objMatData.Pharmacopical_Status = ddlNewPharmaStatus.SelectedValue;
    //        objMatData.Pharmacopical_Status = GetSelectedCheckedValue(ddlNewPharmaStatus) == null ? "" : GetSelectedCheckedValue(ddlNewPharmaStatus);
    //        objMatData.AnalysisMethod = ddlNewMthdAnalysis.SelectedValue;
    //        objMatData.MaterialCategory = ddlNewMatCategory.SelectedValue;
    //        objMatData.Product_Name = txtNewProdName.Text;
    //        objMatData.LupinLoc = ddlNewLupinLoc.SelectedValue ;

    //        objMatData.UserId = lblUserId.Text;
    //        objMatData.TodayDate = objUtil.GetDate();
    //        objMatData.IPAddress = objUtil.GetIpAddress();
    //        objMatData.Mode = lblMode.Text;

    //        //if (objMatAccess.InsertPharmo(Convert.ToInt32(txtNewSNo.Text), txtNewMatName.Text, ddlNewPharmaStatus.SelectedValue, ref objDal.cnnConnection, ref objTrans))
    //        if (objAuditAccess.InsertMaterialsData(objMatData) > 0)
    //        {
    //            FillMaterialsGrid();
    //            //FillProductsGrid();
    //        }
    //        else
    //        {
    //            lblMsg.Text = "Error";
    //            pnlMsg.Visible = true;
    //        }

    //    }
    //}

    //protected void grdMaterials_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    AuditMaterialsData objMatData = new AuditMaterialsData();
    //    Utility objUtil = new Utility();

    //    Label lblE_Audit_Material_Id = (Label)grdMaterials.Rows[e.RowIndex].FindControl("lblE_Audit_Material_Id");
    //    TextBox txtSNo = (TextBox)grdMaterials.Rows[e.RowIndex].FindControl("txtSNo");
    //    TextBox txtMatName = (TextBox)grdMaterials.Rows[e.RowIndex].FindControl("txtMatName");
    //    TextBox txtProdName = (TextBox)grdMaterials.Rows[e.RowIndex].FindControl("txtProdName");
    //    //DropDownList ddlPharmaStatus = (DropDownList)grdMaterials.Rows[e.RowIndex].FindControl("ddlPharmaStatus");
    //    DropDownCheckBoxes ddlPharmaStatus = (DropDownCheckBoxes)grdMaterials.Rows[e.RowIndex].FindControl("ddlPharmaStatus");
    //    DropDownList ddlMthdAnalysis = (DropDownList)grdMaterials.Rows[e.RowIndex].FindControl("ddlMthdAnalysis");
    //    DropDownList ddlMatCategory = (DropDownList)grdMaterials.Rows[e.RowIndex].FindControl("ddlMatCategory");
    //    DropDownList ddlLupinLoc = (DropDownList)grdMaterials.Rows[e.RowIndex].FindControl("ddlLupinLoc");

    //    objMatData.E_Audit_Material_Id = Convert.ToInt32(lblE_Audit_Material_Id.Text);
    //    objMatData.EAudit_Form_Id = Convert.ToInt32(lblEAuditId.Text);
    //    objMatData.SerialNo = Convert.ToInt32(txtSNo.Text);
    //    objMatData.Material_Name = txtMatName.Text;
    //    objMatData.Product_Name = txtProdName.Text;
    //    objMatData.LupinLoc = ddlLupinLoc.SelectedValue;
    //    //objMatData.Pharmacopical_Status = ddlPharmaStatus.SelectedValue;
    //    objMatData.Pharmacopical_Status = GetSelectedCheckedValue(ddlPharmaStatus) == null ? "" : GetSelectedCheckedValue(ddlPharmaStatus);
    //    objMatData.AnalysisMethod = ddlMthdAnalysis.SelectedValue;
    //    objMatData.MaterialCategory = ddlMatCategory.SelectedValue;

    //    objMatData.UserId = lblUserId.Text;
    //    objMatData.TodayDate = objUtil.GetDate();
    //    objMatData.IPAddress = objUtil.GetIpAddress();
    //    objMatData.Mode = lblMode.Text;


    //    //if (objMatAccess.UpdatePharmo(txtMatName.Text, ddlPharmaStatus.SelectedValue, Convert.ToInt32(txtSNo.Text), ref objDal.cnnConnection, ref objTrans))
    //    if (objAuditAccess.InsertMaterialsData(objMatData) > 0)
    //    {

    //        grdMaterials.EditIndex = -1;
    //        FillMaterialsGrid();
    //        //FillProductsGrid();
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Error";
    //        pnlMsg.Visible = true;
    //    }
    //}

    //protected void grdMaterials_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    grdMaterials.EditIndex = e.NewEditIndex;
    //    FillMaterialsGrid();
    //    //FillProductsGrid();
    //}

    //protected void grdMaterials_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    grdMaterials.EditIndex = -1;
    //    FillMaterialsGrid();
    //    //FillProductsGrid();
    //}

    //protected void grdMaterials_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    SqlTransaction objTrans;
    //    objDal.OpenConnection();
    //    objTrans = objDal.cnnConnection.BeginTransaction();

    //    int id = Convert.ToInt32(grdMaterials.DataKeys[e.RowIndex].Values[0].ToString());
    //    Label lblMatId = (Label)grdMaterials.Rows[e.RowIndex].FindControl("lblE_Audit_Material_Id");
    //    int matId = Convert.ToInt32(lblMatId.Text);
    //    if (objAuditAccess.DeleteMaterialData(id, matId, ref objDal.cnnConnection, ref objTrans))
    //    {
    //        objTrans.Commit();
    //        FillMaterialsGrid();
    //        //FillProductsGrid();
    //    }
    //    else
    //    {
    //        objTrans.Rollback();
    //        lblMsg.Text = "Error";
    //        pnlMsg.Visible = true;
    //    }
    //}

    #endregion

    #region Methods

    private void PopulateDropDownList()
    {
        //helperAccess.PopuplateDropDownList(ddlLocation, "pr_GetDropDownListByControlNameModuleType 'E','ddlLocation'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlDept, "pr_GetDropDownListByControlNameModuleType 'E','ddlDept'", "LookUp_Desc", "LookUp_Code", "");
        //helperAccess.PopuplateDropDownList(ddlMarket, "pr_GetDropDownListByControlNameModuleType 'E','ddlMarket'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownCheckBox(ddlMarket, "pr_GetDropDownListByControlNameModuleType 'E','ddlMarket'", "LookUp_Desc", "LookUp_Code");
        helperAccess.PopuplateDropDownList(ddlPriority, "pr_GetDropDownListByControlNameModuleType 'E','ddlPriority'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlReasonAudit, "pr_GetDropDownListByControlNameModuleType 'E','ddlReasonAudit'", "LookUp_Desc", "LookUp_Code", "");
        helperAccess.PopuplateDropDownList(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry 0", "Region_Name", "Region_Id");
        //helperAccess.PopuplateDropDownList(ddlMthdAnalysis, "pr_GetDropDownListByControlNameModuleType 'E','ddlMthdAnalysis'", "LookUp_Desc", "LookUp_Code", "");
    }

    private void FillBasicAuditData()
    {
        AuditData objAuditData = GetBasicAuditData();
        try
        {
            if (objAuditData.EAudit_Form_Id > 0)
            {

                lblEAuditId.Text = objAuditData.EAudit_Form_Id.ToString();
                //ddlLocation.SelectedValue = objAuditData.Location;
                txtDateOfRequest.Text = objAuditData.RequestDate;
                ddlPriority.SelectedValue = objAuditData.Priority;
                //ddlDept.SelectedValue = objAuditData.Department;
                //txtDeptSpecify.Text = objAuditData.Spec_Dept;
                //ddlMarket.SelectedValue = objAuditData.Market;
                SetSelectedValue(ddlMarket, objAuditData.Market);
                txtMarketSpec.Text = objAuditData.Spec_Market;
                txtCustomerCode.Text = objAuditData.Customer_Code;
                txtName1.Text = objAuditData.Name1;
                txtName2.Text = objAuditData.Name2;
                txtName3.Text = objAuditData.Name3;
                txtName4.Text = objAuditData.Name4;
                txtHouseNo.Text = objAuditData.HouseNo_Street;
                txtStreet4.Text = objAuditData.Street4;
                txtStreet5.Text = objAuditData.Street5;
                txtPOBox.Text = objAuditData.PO_Box;
                txtPostalCode.Text = objAuditData.Postal_Code;
                txtCity.Text = objAuditData.City;
                txtDistrict.Text = objAuditData.District;
                txtPOBoxPostal.Text = objAuditData.PO_Box_Postal_Code;
                txtCountry.Text = objAuditData.CountryKey;
                txtRegion.Text = objAuditData.Region;
                //ddlYesNo.SelectedValue = objAuditData.AnalysisPerformed;
                //ddlMthdAnalysis.SelectedValue = objAuditData.AnalysisMethod;
                txtContactName.Text = objAuditData.Contact_Name;
                txtMobileNum.Text = objAuditData.Mobile_Num;
                txttelephone.Text = objAuditData.First_Tele_No;
                txtEmailAddress.Text = objAuditData.Email_Address;
                ddlReasonAudit.SelectedValue = objAuditData.Audit_Reason;
                txtAuditSpec.Text = objAuditData.Spec_Audit;
                txtApprovalStatus.Text = objAuditData.Prev_App_Status;
                txtRemarks.Text = objAuditData.Remarks;

                //txtTrials.Text = objAuditData.RDTrialsOver;
                //txtFeasibilityTrial.Text = objAuditData.RDFeasibilityTrial ;
                //txtSpecsFreezed.Text = objAuditData.RDSpecsFreezed;
                //txtSpecReq.Text = objAuditData.RDSpecReq;
                //txtMatCategory.Text = objAuditData.RDMaterialCategory;
                txtRNDComments.Text = objAuditData.RDComments;
                txtDMF.Text = objAuditData.RADMF;
                //Start Commented by Nitish Rao 14/08/2018
                //txtMatType.Text = objAuditData.RAMatType;
                //txtModReq.Text = objAuditData.RACatMod;
                //End Commented by Nitish Rao 14/08/2018
                txtRedefinedCat.Text = objAuditData.RAMatRedefined;
                txtAuditReq.Text = objAuditData.RAAuditNeeded;
                txtRA.Text = objAuditData.RAJoinAudit;
                //ddlQAgreement.SelectedValue = objAuditData.QASignOff;
                //txtJustification.Text = objAuditData.Justification;
                // Start Adding By nitish Rao   10/08/2018
                txtRAComments.Text = objAuditData.RAComments;
                txtRNDDMF.Text = objAuditData.RNDDMF;
                //txtRNDMatType.Text = objAuditData.RNDMatType;
                //txtRNDModReq.Text = objAuditData.RNDCatMod;
                txtRNDRedefinedCat.Text = objAuditData.RNDMatRedefined;
                txtRNDAuditReq.Text = objAuditData.RNDAuditNeeded;
                txtRND.Text = objAuditData.RNDJoinAudit;
                txtcountrymcode.Text = objAuditData.MobileExt;
                // End adding By Nitish Rao 10/08/2018
            }
            else
            {
                lblEAuditId.Text = "0";
                txtDateOfRequest.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            DisplayMarket();
            BindAttachedDocuments(lblMasterHeaderId.Text);
            ExpectedCompletion();
            SetupReasonAudit();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private AuditData GetBasicAuditData()
    {
        return objAuditAccess.GetAuditBasicData(lblMasterHeaderId.Text);
    }

    private void FillVendorData()
    {
        helperAccess.PopuplateDropDownList(ddlCountry, "pr_Get_CountryList", "Country_Name", "Country_Id");
        helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry 0", "Region_Name", "Region_Id");

        VendorGeneral1 objVendorData = GetVendorGeneral();
        if (objVendorData.Vendor_General1_Id > 0)
        {
            txtName1.Text = objVendorData.Name1.ToString();
            txtName2.Text = objVendorData.Name2.ToString();
            txtName3.Text = objVendorData.Name3.ToString();
            txtName4.Text = objVendorData.Name4.ToString();
            txtHouseNo.Text = objVendorData.HouseNo_Street.ToString();
            txtStreet4.Text = objVendorData.Street4.ToString();
            txtStreet5.Text = objVendorData.Street5.ToString();
            txtSortfield.Text = objVendorData.Sort_Field.ToString();
            txtCity.Text = objVendorData.City.ToString();
            txtDistrict.Text = objVendorData.District.ToString();
            txtPostalCode.Text = objVendorData.Postal_Code.ToString();
            ddlCountry.SelectedValue = objVendorData.CountryKey.ToString() == "" ? "0" : objVendorData.CountryKey.ToString();
            BindRegionData();
            ddlRegion.SelectedValue = objVendorData.Region.ToString() == "" ? "0" : objVendorData.Region.ToString();

            txtCountry.Text = ddlCountry.SelectedItem.ToString() == "---Select---" ? "" : ddlCountry.SelectedItem.ToString();
            txtRegion.Text = ddlRegion.SelectedItem.ToString() == "---Select---" ? "" : ddlRegion.SelectedItem.ToString();
            txtLanguage.Text = objVendorData.LanguageAcc.ToString();
            txtPOBox.Text = objVendorData.PO_Box.ToString();
            txtPOBoxPostal.Text = objVendorData.ToString();
        }
        else
        {
            txtName1.Text = "";
            txtName2.Text = "";
            txtName3.Text = "";
            txtName4.Text = "";
            txtHouseNo.Text = "";
            txtStreet4.Text = "";
            txtStreet5.Text = "";
            txtSortfield.Text = "";
            txtCity.Text = "";
            txtDistrict.Text = "";
            txtPostalCode.Text = "";
            txtCountry.Text = "";
            txtRegion.Text = "";
            txtLanguage.Text = "";
            txtPOBox.Text = "";
            txtPOBoxPostal.Text = "";
        }


    }

    private VendorGeneral1 GetVendorGeneral()
    {
        VendorGeneral1 obj = new VendorGeneral1();
        if (txtCustomerCode.Text != "")
        {
            //int master_header_id = objMatAccess.GetMasterHeaderId(txtCustomerCode.Text);
            //obj = ObjVendorGeneralAccess.GetVendorGeneral1(master_header_id);
            //if (obj.Vendor_General1_Id == 0)
            //{
            obj = objAuditAccess.GetVendorGeneral_FromSAP(txtCustomerCode.Text);
            //}
        }
        return obj;
    }

    private bool Save()
    {
        bool flg = false;
        EAuditAccess eAuditAccess = new EAuditAccess();
        try
        {
            //AuditData ObjAuditData = GetControlsValue();
            if (ValidateMaterialData())
            {

                if (SaveBasicAuditData() && SaveMaterials())
                {
                    Page.MaintainScrollPositionOnPostBack = false;
                    DataAccessLayer objDb = new DataAccessLayer();
                    DataSet dstData = new DataSet();
                    DocumentUploadAccess objDoc = new DocumentUploadAccess();

                    try
                    {
                        dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
                        if (dstData.Tables[0].Rows.Count > 0)
                        {
                            flg = true;

                        }
                        else
                        {
                            flg = false;
                            lblMsg.Text = Messages.GetMessage(-1);
                            lblMsg.Text = "Please Upload document";
                            pnlMsg.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objDb = null;
                    }
                    //Response.Redirect("EAuditMaster.aspx");
                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Please enter atleast one material data.";
                pnlMsg.CssClass = "error";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    private bool SaveMaterials()
    {
        bool flg = false;
        try
        {
            foreach (GridViewRow row in grdMaterials.Rows)
            {
                AuditMaterialsData objMaterialData = new AuditMaterialsData();
                objMaterialData = GetMaterialData(row);
                if (objAuditAccess.InsertMaterialsData(objMaterialData) > 0)
                {
                    flg = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return flg;
    }

    private bool SaveBasicAuditData()
    {
        bool flg = false;
        EAuditAccess eAuditAccess = new EAuditAccess();
        try
        {
            AuditData ObjAuditData = GetControlsValue();
            int auditFormId = eAuditAccess.SaveAuditData(ObjAuditData);
            if (auditFormId > 0)
            {
                lblEAuditId.Text = auditFormId.ToString();
                flg = true;
                //lblMsg.Text = "Header saved";
                //pnlMsg.CssClass = "success";
                //pnlMsg.Visible = true;
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
            throw ex;
        }
        return flg;
    }

    private AuditMaterialsData GetMaterialData(GridViewRow row)
    {
        AuditMaterialsData objMatData = new AuditMaterialsData();
        Utility objUtil = new Utility();

        Label lblE_Audit_Material_Id = (Label)row.FindControl("lblE_Audit_Material_Id");
        TextBox txtSNo = (TextBox)row.FindControl("txtSNo");
        TextBox txtMatName = (TextBox)row.FindControl("txtMatName");
        TextBox txtProdName = (TextBox)row.FindControl("txtProdName");

        DropDownCheckBoxes ddlPharmaStatus = (DropDownCheckBoxes)row.FindControl("ddlPharmaStatus");
        TextBox txtOtherPharmaStatus = (TextBox)row.FindControl("txtOtherPharmaStatus");

        //DropDownList ddlMthdAnalysis = (DropDownList)row.FindControl("ddlMthdAnalysis");
        DropDownCheckBoxes ddlMthdAnalysis = (DropDownCheckBoxes)row.FindControl("ddlMthdAnalysis");
        TextBox txtOtherMthdAnalysis = (TextBox)row.FindControl("txtOtherMthdAnalysis");

        DropDownList ddlMatCategory = (DropDownList)row.FindControl("ddlMatCategory");
        TextBox txtOtherMatCategory = (TextBox)row.FindControl("txtOtherMatCategory");

        //DropDownList ddlLupinLoc = (DropDownList)row.FindControl("ddlLupinLoc");
        DropDownCheckBoxes ddlLupinLoc = (DropDownCheckBoxes)row.FindControl("ddlLupinLoc");
        TextBox txtOtherLC = (TextBox)row.FindControl("txtOtherLC");

        objMatData.E_Audit_Material_Id = Convert.ToInt32(lblE_Audit_Material_Id.Text);
        objMatData.EAudit_Form_Id = Convert.ToInt32(lblEAuditId.Text);
        objMatData.SerialNo = Convert.ToInt32(txtSNo.Text);
        objMatData.Material_Name = txtMatName.Text;
        objMatData.Product_Name = txtProdName.Text;
        // Start Comment & Adding code Nitish Rao 10/08/2018
        //objMatData.LupinLoc = ddlLupinLoc.SelectedValue;
        objMatData.LupinLoc = GetSelectedCheckedValue(ddlLupinLoc) == null ? "" : GetSelectedCheckedValue(ddlLupinLoc);
        objMatData.Pharmacopical_Status = GetSelectedCheckedValue(ddlPharmaStatus) == null ? "" : GetSelectedCheckedValue(ddlPharmaStatus);
        //objMatData.AnalysisMethod = ddlMthdAnalysis.SelectedValue;
        objMatData.AnalysisMethod = GetSelectedCheckedValue(ddlMthdAnalysis) == null ? "" : GetSelectedCheckedValue(ddlMthdAnalysis);
        objMatData.MaterialCategory = ddlMatCategory.SelectedValue;

        // Start Comment & Adding code Nitish Rao 10/08/2018
        objMatData.UserId = lblUserId.Text;
        objMatData.TodayDate = objUtil.GetDate();
        objMatData.IPAddress = objUtil.GetIpAddress();
        objMatData.Mode = lblMode.Text;

        //Start Adding By Nitish Rao 10/08/2018 for Manufacture Changes
        objMatData.OtherLC = txtOtherLC.Text.Trim();
        objMatData.OtherPharmaStatus = txtOtherPharmaStatus.Text.Trim();
        objMatData.OtherMthdAnalysis = txtOtherMthdAnalysis.Text.Trim();
        objMatData.OtherMatCategory = txtOtherMatCategory.Text.Trim();
        //End Adding By Nitish Rao 10/08/2018 for Manufacture Changes

        return objMatData;
    }

    private AuditData GetControlsValue()
    {
        AuditData objAuditData = new AuditData();
        Utility objUtil = new Utility();

        objAuditData.EAudit_Form_Id = Convert.ToInt32(lblEAuditId.Text);
        objAuditData.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

        //objAuditData.Location = ddlLocation.SelectedValue;
        objAuditData.RequestDate = objUtil.GetYYYYMMDD(txtDateOfRequest.Text);
        objAuditData.Priority = ddlPriority.SelectedValue;
        objAuditData.Market = GetSelectedCheckedValue(ddlMarket) == null ? "" : GetSelectedCheckedValue(ddlMarket);
        objAuditData.Spec_Market = txtMarketSpec.Text;
        objAuditData.Customer_Code = txtCustomerCode.Text;
        objAuditData.Name1 = txtName1.Text;
        objAuditData.Name2 = txtName2.Text;
        objAuditData.Name3 = txtName3.Text;
        objAuditData.Name4 = txtName4.Text;
        objAuditData.HouseNo_Street = txtHouseNo.Text;
        objAuditData.Street4 = txtStreet4.Text;
        objAuditData.Street5 = txtStreet5.Text;
        objAuditData.PO_Box = txtPOBox.Text;
        objAuditData.Postal_Code = txtPostalCode.Text;
        objAuditData.City = txtCity.Text;
        objAuditData.District = txtDistrict.Text;
        objAuditData.PO_Box_Postal_Code = txtPOBoxPostal.Text;
        objAuditData.CountryKey = txtCountry.Text;
        objAuditData.Region = txtRegion.Text;
        objAuditData.Contact_Name = txtContactName.Text;
        objAuditData.Mobile_Num = txtMobileNum.Text;
        objAuditData.First_Tele_No = txttelephone.Text;
        objAuditData.Email_Address = txtEmailAddress.Text;
        objAuditData.Audit_Reason = ddlReasonAudit.SelectedValue;
        objAuditData.Spec_Audit = txtAuditSpec.Text;
        objAuditData.Prev_App_Status = txtApprovalStatus.Text;
        objAuditData.Remarks = txtRemarks.Text;

        //objAuditData.RDTrialsOver = txtTrials.Text;
        //objAuditData.RDFeasibilityTrial = txtFeasibilityTrial.Text;
        //objAuditData.RDSpecsFreezed = txtSpecsFreezed.Text;
        //objAuditData.RDSpecReq = txtSpecReq.Text;
        //objAuditData.RDMaterialCategory = txtMatCategory.Text;
        objAuditData.RDComments = txtRNDComments.Text;
        objAuditData.RADMF = txtDMF.Text;
        //Start Commented by Nitish Rao 14/08/2018
        //objAuditData.RAMatType = txtMatType.Text;
        //objAuditData.RACatMod = txtModReq.Text;
        //End Commented by Nitish Rao 14/08/2018
        objAuditData.RAMatRedefined = txtRedefinedCat.Text;
        objAuditData.RAAuditNeeded = txtAuditReq.Text;
        objAuditData.RAJoinAudit = txtRA.Text;
        //objAuditData.QASignOff = ddlQAgreement.SelectedValue;
        //objAuditData.Justification = txtJustification.Text;

        //Added by Nitish Rao 10/08/2018
        objAuditData.RAComments = txtRAComments.Text;
        objAuditData.RNDDMF = txtRNDDMF.Text;
        //objAuditData.RNDMatType = txtMatType.Text;
        //objAuditData.RNDCatMod = txtModReq.Text;
        objAuditData.RNDMatRedefined = txtRNDRedefinedCat.Text;
        objAuditData.RNDAuditNeeded = txtRNDAuditReq.Text;
        objAuditData.RNDJoinAudit = txtRND.Text;
        objAuditData.MobileExt = txtcountrymcode.Text;
        //Added by Nitish Rao 10/08/2018

        objAuditData.UserId = lblUserId.Text;
        objAuditData.TodayDate = objUtil.GetDate();
        objAuditData.IPAddress = objUtil.GetIpAddress();
        objAuditData.Mode = lblMode.Text;



        return objAuditData;
    }

    private void BindRegionData()
    {
        string Country = ddlCountry.SelectedValue == "#" ? "0" : ddlCountry.SelectedValue;
        helperAccess.PopuplateDropDownList(ddlRegion, "pr_Get_RegionListByCountry " + Country, "Region_Name", "Region_Id");
    }

    private bool SaveDocuments(string AuditId)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/EAudit/EAuditDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
        savePath = MapPath(StrPath);

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        try
        {
            bool flag = false;
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];

                if (uploadfile.ContentLength > 0)
                {
                    flag = UploadDocument(uploadfile, StrPath, savePath);
                }
            }
            return flag;
        }
        catch
        {
            return false;
        }
    }

    private bool UploadDocument(HttpPostedFile uploadfile, string StrPath, string savePath)
    {
        DocumentUpload ObjDoc = new DocumentUpload();
        DocumentUploadAccess ObjDocUploadAccess = new DocumentUploadAccess();
        bool flag = false;
        Utility objUtil = new Utility();

        Random sufix1 = new Random();
        string sufix = sufix1.NextDouble().ToString().Replace(".", "");
        string e = Path.GetExtension(uploadfile.FileName);
        e = e.ToLower();
        if (e == ".jpg" || e == ".jpeg" || e == ".pdf" || e == ".png" || e == ".bmp")
        {
            if (uploadfile.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();

                string uploadedFileName = lblMasterHeaderId.Text + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + sufix + Path.GetExtension(uploadfile.FileName);
                savePath = savePath + "\\" + uploadedFileName;

                ObjDoc.Document_Upload_Id = 0;
                ObjDoc.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
                ObjDoc.Request_No = Session[StaticKeys.RequestNo].ToString();
                ObjDoc.Document_Type = "";
                ObjDoc.Document_Name = Path.GetFileName(uploadfile.FileName);
                ObjDoc.Document_Path = StrPath + uploadedFileName;
                ObjDoc.Remarks = "";
                ObjDoc.IsActive = 1;
                ObjDoc.UserId = lblUserId.Text;
                ObjDoc.IPAddress = objUtil.GetIpAddress();

                uploadfile.SaveAs(savePath);

                ObjDocUploadAccess.Save(ObjDoc);

                flag = true;
                lblMsg.Text = "";
                pnlMsg.Visible = false;
            }
            else
            {
                flag = false;
                lblMsg.Text = "Error While Saving Material Details.";

                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            flag = false;
            lblMsg.Text = "Error Please upload Image / PDF file format only.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

        return flag;
    }

    private void BindAttachedDocuments(string MaterialId)
    {
        DataAccessLayer objDb = new DataAccessLayer();
        DataSet dstData = new DataSet();
        DocumentUploadAccess objDoc = new DocumentUploadAccess();

        try
        {
            dstData = objDoc.GetDocumentUploadData(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dstData.Tables[0].Rows.Count > 0)
            {
                grdAttachedDocs.DataSource = dstData.Tables[0].DefaultView;
                grdAttachedDocs.DataBind();
                grdAttachedDocs.Visible = true;
                Requiredfile_upload.Enabled = false;

            }
            else
            {
                grdAttachedDocs.Visible = false;
                Requiredfile_upload.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDb = null;
        }
    }

    private void DisplayMarket()
    {
        string Market = GetSelectedCheckedValue(ddlMarket);
        if (Market != null)
        {
            lableddlMarket.Text = "Proposed Market :  " + Market.Substring(0, Market.Length - 1);
        }
        else
        {
            lableddlMarket.Text = "";
        }

        if (lableddlMarket.Text.ToString().Contains("OTH"))
        {
            txtMarketSpec.Enabled = true;
            labelMarketSpec.Visible = true;
            reqtxtMarketSpec.Enabled = true;

        }
        else
        {
            txtMarketSpec.Enabled = false;
            labelMarketSpec.Visible = false;
            reqtxtMarketSpec.Enabled = false;
        }
    }

    private void ExpectedCompletion()
    {
        string priority = ddlPriority.SelectedValue.ToString();
        DateTime dtParam;
        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
        dtParam = Convert.ToDateTime(txtDateOfRequest.Text.ToString(), enGB);

        switch (priority)
        {
            case "NOR":
                lblPriorityDate.Text = "Tentative Completion : " + dtParam.AddMonths(4).ToString("d MMM yyyy");
                break;
            case "URG":
                //lblPriorityDate.Text = "Expected Completion : " + DateTime.Now.AddMonths(3).ToString("d MMM yyyy");
                lblPriorityDate.Text = "Tentative Completion : " + dtParam.AddMonths(3).ToString("d MMM yyyy");
                break;
            case "VUR":
                lblPriorityDate.Text = "Tentative Completion : " + dtParam.AddMonths(2).ToString("d MMM yyyy");
                break;
            case "CRU":
                lblPriorityDate.Text = "Tentative Completion : " + dtParam.AddMonths(1).ToString("d MMM yyyy");
                break;
            case "DMS":
                lblPriorityDate.Text = "Tentative Completion : " + dtParam.AddMonths(3).ToString("d MMM yyyy");
                break;
            case "OSS":
                lblPriorityDate.Text = "Tentative Completion : " + dtParam.AddMonths(6).ToString("d MMM yyyy");
                break;
            default:
                lblPriorityDate.Text = "";
                break;
        }
    }

    private void SetupReasonAudit()
    {
        if (ddlReasonAudit.SelectedValue == "OTH")
        {
            txtAuditSpec.Enabled = true;
            txtAuditSpec.Visible = true;
            labeltxtAuditSpec.Visible = true;
            reqtxtAuditSpec.Enabled = true;

        }
        else
        {
            txtAuditSpec.Enabled = false;
            txtAuditSpec.Visible = false;
            labeltxtAuditSpec.Visible = false;
            reqtxtAuditSpec.Enabled = false;

        }
    }

    private void ConfigureControls()
    {
        bool RATeam, RDTeam;
        RATeam = RDTeam = false;
        //R&D team
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "20")
            RDTeam = true;
        else if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "22")
            RATeam = true;

        txtRNDDMF.Enabled = RDTeam;
        txtRNDRedefinedCat.Enabled = RDTeam;
        txtRNDAuditReq.Enabled = RDTeam;
        txtRND.Enabled = RDTeam;
        txtRNDComments.Enabled = RDTeam;

        // Start Adding Nitish Rao 14/08/2018
        reqtxtRNDDMF.Enabled = RDTeam;
        reqtxtRNDRedefinedCat.Enabled = RDTeam;
        reqRNDtxtAuditReq.Enabled = RDTeam;
        reqtxtRND.Enabled = RDTeam;
        reqtxtRNDComments.Enabled = RDTeam;

        labelRNDDMF.Visible = RDTeam;
        labelRNDRedefinedCat.Visible = RDTeam;
        labelRNDAuditReq.Visible = RDTeam;
        lblRNDComments.Visible = RDTeam;
        labelRND.Visible = RDTeam;
        // End adding Nitish Rao 14/08/2018


        txtDMF.Enabled = RATeam;
        //txtMatType.Enabled = RATeam;
        //txtModReq.Enabled = RATeam;
        txtRedefinedCat.Enabled = RATeam;
        txtAuditReq.Enabled = RATeam;
        txtRA.Enabled = RATeam;
        txtRAComments.Enabled = RATeam;

        // Start Adding Nitish Rao 14/08/2018
        reqtxtDMF.Enabled = RATeam;
        //reqtxtMatType.Enabled = RATeam;
        //reqtxtModReq.Enabled = RATeam;
        reqtxtRedefinedCat.Enabled = RATeam;
        reqtxtAuditReq.Enabled = RATeam;
        reqtxtRA.Enabled = RATeam;
        reqtxtRAComments.Enabled = RATeam;

        labelDMF.Visible = RATeam;
        labelRedefinedCat.Visible = RATeam;
        labelAuditReq.Visible = RATeam;
        lblRAComments.Visible = RATeam;
        labelRA.Visible = RATeam;
        // End adding Nitish Rao 14/08/2018
    }

    private bool ValidateMaterialData()
    {
        bool flg = false;
        if (grdMaterials.Rows.Count != 0)
        {
            flg = true;
        }
        return flg;
    }

    #endregion

    //#region Product Data

    //protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //DataSet dstData = objMatAccess.TestingProd();
    //    DataSet dstData = objAuditAccess.GetProductsData(Convert.ToInt32(lblEAuditId.Text));
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        if (dstData.Tables[0].Rows.Count > 0)
    //        {
    //            TextBox txtNewPNo = (TextBox)e.Row.FindControl("txtNewPNo");
    //            txtNewPNo.Text = Convert.ToString(dstData.Tables[0].Rows.Count + 1);
    //        }
    //    }

    //}

    //private void FillProductsGrid()
    //{
    //    DataSet dstData = new DataSet();

    //    dstData = objAuditAccess.GetProductsData(Convert.ToInt32(lblEAuditId.Text));
    //    if (dstData.Tables[0].Rows.Count > 0)
    //    {
    //        grdProducts.DataSource = dstData.Tables[0].DefaultView;
    //        grdProducts.DataBind();
    //    }
    //    else
    //    {
    //        dstData.Tables[0].Rows.Add(dstData.Tables[0].NewRow());
    //        grdProducts.DataSource = dstData.Tables[0].DefaultView;
    //        grdProducts.DataBind();

    //        int TotalColumns = grdProducts.Rows[0].Cells.Count;
    //        grdProducts.Rows[0].Cells.Clear();
    //        grdProducts.Rows[0].Cells.Add(new TableCell());
    //        grdProducts.Rows[0].Cells[0].ColumnSpan = TotalColumns;
    //        grdProducts.Rows[0].Cells[0].Text = "No Record Found";
    //    }
    //}

    //protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    AuditProductsData objProdData = new AuditProductsData();
    //    Utility objUtil = new Utility();

    //    if (e.CommandName.Equals("Insert"))
    //    {
    //        TextBox txtNewPNo = (TextBox)grdProducts.FooterRow.FindControl("txtNewPNo");
    //        TextBox txtNewProdName = (TextBox)grdProducts.FooterRow.FindControl("txtNewProdName");


    //        objProdData.E_Audit_Product_Id = 0;
    //        objProdData.EAudit_Form_Id = Convert.ToInt32(lblEAuditId.Text);
    //        //objProdData.Serial_No  = Convert.ToInt32(txtNewPNo.Text);
    //        objProdData.Serial_No = 0;
    //        objProdData.Product_Name = txtNewProdName.Text;

    //        objProdData.UserId = lblUserId.Text;
    //        objProdData.TodayDate = objUtil.GetDate();
    //        objProdData.IPAddress = objUtil.GetIpAddress();
    //        objProdData.Mode = lblMode.Text;

    //        //if (objMatAccess.InsertPharmo(Convert.ToInt32(txtNewSNo.Text), txtNewMatName.Text, ddlNewPharmaStatus.SelectedValue, ref objDal.cnnConnection, ref objTrans))
    //        if (objAuditAccess.InsertProductsData(objProdData) > 0)
    //        {
    //            FillProductsGrid();
    //            FillMaterialsGrid();
    //        }
    //        else
    //        {
    //            lblMsg.Text = "Error";
    //            pnlMsg.Visible = true;
    //        }

    //    }
    //}

    //protected void grdProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    AuditProductsData objProdData = new AuditProductsData();
    //    Utility objUtil = new Utility();

    //    Label lblE_Audit_Product_Id = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblE_Audit_Product_Id");
    //    TextBox txtPNo = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtPNo");
    //    TextBox txtProdName = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtProdName");

    //    objProdData.E_Audit_Product_Id = Convert.ToInt32(lblE_Audit_Product_Id.Text);
    //    objProdData.EAudit_Form_Id = Convert.ToInt32(lblEAuditId.Text);
    //    objProdData.Serial_No = Convert.ToInt32(txtPNo.Text);
    //    objProdData.Product_Name = txtProdName.Text;

    //    objProdData.UserId = lblUserId.Text;
    //    objProdData.TodayDate = objUtil.GetDate();
    //    objProdData.IPAddress = objUtil.GetIpAddress();
    //    objProdData.Mode = lblMode.Text;

    //    if (objAuditAccess.InsertProductsData(objProdData) > 0)
    //    {
    //        grdProducts.EditIndex = -1;
    //        FillProductsGrid();
    //        FillMaterialsGrid();
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Error";
    //        pnlMsg.Visible = true;
    //    }
    //}

    //protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    grdProducts.EditIndex = e.NewEditIndex;
    //    FillProductsGrid();
    //    FillMaterialsGrid();
    //}

    //protected void grdProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    grdProducts.EditIndex = -1;
    //    FillProductsGrid();
    //    FillMaterialsGrid();
    //}

    //protected void grdProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    SqlTransaction objTrans;
    //    objDal.OpenConnection();
    //    objTrans = objDal.cnnConnection.BeginTransaction();

    //    int id = Convert.ToInt32(grdProducts.DataKeys[e.RowIndex].Values[0].ToString());
    //    Label lblProdId = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblE_Audit_Product_Id");
    //    int prodid = Convert.ToInt32(lblProdId.Text);

    //    if (objAuditAccess.DeleteProductData(id, prodid, ref objDal.cnnConnection, ref objTrans))
    //    {
    //        objTrans.Commit();
    //        FillProductsGrid();
    //        FillMaterialsGrid();
    //    }
    //    else
    //    {
    //        objTrans.Rollback();
    //        lblMsg.Text = "Error";
    //        pnlMsg.Visible = true;
    //    }
    //}





    //#endregion


    protected void btnupload_Click(object sender, EventArgs e)
    {
        bool flag = false;
        flag = SaveDocuments(lblMasterHeaderId.Text);

        BindAttachedDocuments(lblMasterHeaderId.Text);

    }

    [System.Web.Services.WebMethod]
    public static string call_s()
    {
        Transaction_EAudit_EAudit ba1 = new Transaction_EAudit_EAudit();
        ba1.Save();
        //return "Hello " + name + Environment.NewLine + "The Current Time is: "

        //    + DateTime.Now.ToString();
        //Save();
        string name = "s";
        return name;

    }
}



