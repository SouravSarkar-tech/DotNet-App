using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using System.Drawing;

public partial class Transaction_Customer_DasSystemInfo : System.Web.UI.Page
{
    DasSystemInfoAccess ObjCustomerGeneralAccess = new DasSystemInfoAccess();
    HelperAccess helperAccess = new HelperAccess();

    #region Page Event

    private int numOfRows = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {

                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();

                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                PopuplateDropDownList();
                FillGeneralData();

                string sectionId = lblSectionId.Text.ToString();
                string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                string mode = Session[StaticKeys.Mode].ToString();
                lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);

                if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                {
                    trButton.Visible = true;
                    btnSave.Visible = !btnNext.Visible;
                    grdAttachedDocs.Columns[1].Visible = true;
                    file_upload.Visible = true;
                }
                else
                {
                    grdAttachedDocs.Columns[1].Visible = false;
                    file_upload.Visible = false;
                }
                BindGridview();
                FillDeliveringPlant();
                ConfigControl();
            }
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string pageURL = btnPrevious.CommandArgument.ToString();
        Response.Redirect(pageURL);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;

            Response.Redirect("DasSystemInfo.aspx");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            string pageURL = btnNext.CommandArgument.ToString();
            Response.Redirect(pageURL);
        }
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
                    System.IO.File.Delete(Server.MapPath("CustomerDocuments") + "/" + lblUploadedFileName.Text);
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
                //throw ex;
            }
            finally
            {
                objDb.CloseConnection(objDb.cnnConnection);
                objDb = null;
                objTrans = null;
            }
        }
    }

    #endregion

    #region Private Methods

    private bool Save()
    {
        int flg = 0;
        bool boolFlg = false;

        HttpFileCollection fileCollection = Request.Files;
        string fileExtension = string.Empty;
        for (int i = 0; i < fileCollection.Count; i++)
        {
            HttpPostedFile uploadfile = fileCollection[i];
            if (uploadfile.ContentLength > 0)
            {
                fileExtension = Path.GetExtension(uploadfile.FileName).ToLower();
                if ((fileExtension == ".pdf") || (fileExtension == ".jpg") || (fileExtension == ".jpeg") || (fileExtension == ".bmp") || (fileExtension == ".gif") || (fileExtension == ".png") || fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    flg = 1;
                }
                else
                {
                    flg = 2;
                    break;
                }
            }
        }

        if (flg == 2)
        {
            lblMsg.Text = "Only .Jpg, .Jpeg, .bmp, .Gif, .Png, .Pdf, .xlsx, .xls, .doc, .docx files allowed.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
        else
        {
            boolFlg = SaveCustomerGeneral();
        }
        return boolFlg;
    }

    private bool SaveCustomerGeneral()
    {
        DasSystemInfo ObjCustGeneral = GetControlsValue();
        bool flg = false;
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                bool flag = false;
                if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "40")
                {
                    if ((txtOutstanding_0_30.Text == "") && (txtOutstanding_31_60.Text == "") && (txtOutstanding_61_90.Text == "") && (txtOutstanding_91_180.Text == "") && (txtOutstanding_Age_180.Text == ""))
                    {
                        flag = false;
                        string message = "alert('Age wise outstanding of Headquarter cannot be blank')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag == true)
                {
                    if (ObjCustomerGeneralAccess.Save(ObjCustGeneral) > 0 && (SaveDocuments(lblMasterHeaderId.Text)))
                    {

                        DasSystemInfoDetail objDasDetail = new DasSystemInfoDetail();
                        objDasDetail.Company_Party_Distr_Id = ObjCustGeneral.Master_Header_Id + "_CPDI";

                        int rowIndex = 0;
                        if (ViewState["Curtbl"] != null)

                        {

                            DataTable dt = (DataTable)ViewState["Curtbl"];

                            if (dt.Rows.Count > 0)

                            {

                                for (int i = 0; i < dt.Rows.Count; i++)

                                {

                                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");

                                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");

                                    objDasDetail.Company_Name = txtname.Text;//dt.Rows[i]["Company_Name"].ToString();

                                    objDasDetail.Monthly_Turnover = txtprice.Text;//dt.Rows[i]["Monthly_Turnover"].ToString();

                                    rowIndex++;

                                    ObjCustomerGeneralAccess.SaveDetail(objDasDetail);
                                }

                            }

                        }

                        scope.Complete();
                        flg = true;
                    }
                    else
                    {
                        lblMsg.Text = Messages.GetMessage(-1);
                        pnlMsg.CssClass = "error";
                        pnlMsg.Visible = true;
                    }
                }

            }
            FillGeneralData();
            BindAttachedDocuments(lblMasterHeaderId.Text);
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        return flg;
    }

    private DasSystemInfo GetCustomerGeneral()
    {
        return ObjCustomerGeneralAccess.GetCustomerGeneral1(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private DasSystemInfo GetControlsValue()
    {
        DasSystemInfo ObjCustGeneral = new DasSystemInfo();
        Utility objUtil = new Utility();
        //PopuplateDropDownList();
        ObjCustGeneral.ID = Convert.ToString(lblCustomerGeneralId.Text);
        ObjCustGeneral.Master_Header_Id = Session[StaticKeys.MasterHeaderId].ToString();
        ObjCustGeneral.Depot = ddlDeliveringPlant.SelectedValue;
        ObjCustGeneral.Division = ddlDivision.SelectedValue;
        ObjCustGeneral.Territory = ddlTerritory.SelectedValue;
        ObjCustGeneral.Structure_Of_Firm = ddlStructure_Of_Firm.SelectedValue;

        ObjCustGeneral.NameOfProprietor = txtNameOfProprietor.Text;
        ObjCustGeneral.Bank_Name = txtBank_Name.Text;
        ObjCustGeneral.Bank_Addr = txtBank_Addr.Text;
        ObjCustGeneral.Avail_Cheque = ddlAvail_Cheque.SelectedValue;
        ObjCustGeneral.Transporter_Name = txtTransporter_Name.Text;
        ObjCustGeneral.Add_Or_Replacement = ddlAdd_Or_Replacement.SelectedValue;
        ObjCustGeneral.Years_Pharma_Distr = txtYears_Pharma_Distr.Text;
        ObjCustGeneral.Channel_Of_Distr = txtChannel_Of_Distr.Text;
        ObjCustGeneral.Turnover_Three_Years = txtTurnover_Three_Years.Text;
        ObjCustGeneral.Bank_Stmt_Submitted = ddlBank_Stmt_Submitted.SelectedValue;
        ObjCustGeneral.Expected_Monthly_Sales = txtExpected_Monthly_Sales.Text;
        ObjCustGeneral.Justification_For_Appt = txtJustification_For_Appt.Text;
        ObjCustGeneral.Cur_Territory_Sales = txtCur_Territory_Sales.Text;
        ObjCustGeneral.Dist_In_Territory = txtDist_In_Territory.Text;
        ObjCustGeneral.Sales_Ratio_Per_Dist = txtSales_Ratio_Per_Dist.Text;
        ObjCustGeneral.StockValue = ddlStockValue.SelectedValue;
        ObjCustGeneral.Outstanding_0_30 = txtOutstanding_0_30.Text;
        ObjCustGeneral.Outstanding_31_60 = txtOutstanding_31_60.Text;
        ObjCustGeneral.Outstanding_61_90 = txtOutstanding_61_90.Text;
        ObjCustGeneral.Outstanding_91_180 = txtOutstanding_91_180.Text;
        ObjCustGeneral.Outstanding_Age_180 = txtOutstanding_Age_180.Text;
        ObjCustGeneral.Outstanding_Replacement = ddlOutstanding_Replacement.SelectedValue;
        ObjCustGeneral.Feeback = txtFeeback.Text;
        ObjCustGeneral.UserId = lblUserId.Text;
        ObjCustGeneral.IPAddress = objUtil.GetIpAddress();
        //Added By Nitin R , SDT03052019 , Des : to check request type for Customer IRF
        ObjCustGeneral.RDMLoggedInUserDeptId = Convert.ToString(Session[StaticKeys.LoggedIn_User_DeptId]);
        //Added By Nitin R , EDT03052019 , Des : to check request type for Customer IRF

        DasSystemInfoDetail objDasDetail = new DasSystemInfoDetail();
        int rowIndex = 0;
        if (ViewState["Curtbl"] != null)

        {

            DataTable dt = (DataTable)ViewState["Curtbl"];

            if (dt.Rows.Count > 0)

            {

                for (int i = 0; i < dt.Rows.Count; i++)

                {

                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");

                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");

                    objDasDetail.Company_Name = dt.Rows[i]["Company_Name"].ToString();

                    objDasDetail.Monthly_Turnover = dt.Rows[i]["Monthly_Turnover"].ToString();

                    rowIndex++;

                    ObjCustomerGeneralAccess.SaveDetail(objDasDetail);
                }

            }

        }

        return ObjCustGeneral;
    }

    private void FillGeneralData()
    {
        DasSystemInfo ObjCustGeneral = GetCustomerGeneral();
        //FillDeliveringPlant();
        int ID = Convert.ToInt32(ObjCustGeneral.ID);
        if (ID > 0)
        {
            lblCustomerGeneralId.Text = ObjCustGeneral.ID.ToString();
            lblMasterHeaderId.Text = ObjCustGeneral.Master_Header_Id.ToString();
            Session[StaticKeys.SelectedModulePlantGrp] = ObjCustGeneral.ModulePlantGroupCode;
            //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
            //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','0','0'", "Division_Name", "Division_Id", "");
            //STD07052019 
            helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivision " + Convert.ToString(Session[StaticKeys.DivTypeCusts]) + "", "Division", "ID", "");
            //ETD07052019 
            helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_Get_Delivering_Plant_List'" + lblMasterHeaderId.Text + "'", "Plant_Name", "Plant_Id", "");
            helperAccess.PopuplateDropDownList(ddlTerritory, "pr_Get_DAS_TERRITORY_MASTER", "TERRITORY", "ID", "");
            helperAccess.PopuplateDropDownList(ddlStructure_Of_Firm, "pr_Get_Das_Structure_Of_Firm", "FIRMSTRUCTURE", "ID", "");
            ddlDeliveringPlant.SelectedValue = ObjCustGeneral.Depot;
            ddlDivision.SelectedValue = ObjCustGeneral.Division;
            ddlTerritory.SelectedValue = ObjCustGeneral.Territory;
            ddlStructure_Of_Firm.SelectedValue = ObjCustGeneral.Structure_Of_Firm;
            txtNameOfProprietor.Text = ObjCustGeneral.NameOfProprietor;
            txtBank_Name.Text = ObjCustGeneral.Bank_Name;
            txtBank_Addr.Text = ObjCustGeneral.Bank_Addr;
            ddlAvail_Cheque.SelectedValue = ObjCustGeneral.Avail_Cheque;
            txtTransporter_Name.Text = ObjCustGeneral.Transporter_Name;
            ddlAdd_Or_Replacement.SelectedValue = ObjCustGeneral.Add_Or_Replacement;
            txtYears_Pharma_Distr.Text = ObjCustGeneral.Years_Pharma_Distr;
            txtChannel_Of_Distr.Text = ObjCustGeneral.Channel_Of_Distr;
            txtTurnover_Three_Years.Text = ObjCustGeneral.Turnover_Three_Years;
            ddlBank_Stmt_Submitted.SelectedValue = ObjCustGeneral.Bank_Stmt_Submitted;
            txtExpected_Monthly_Sales.Text = ObjCustGeneral.Expected_Monthly_Sales;
            txtJustification_For_Appt.Text = ObjCustGeneral.Justification_For_Appt;
            txtCur_Territory_Sales.Text = ObjCustGeneral.Cur_Territory_Sales;
            txtDist_In_Territory.Text = ObjCustGeneral.Dist_In_Territory;
            txtSales_Ratio_Per_Dist.Text = ObjCustGeneral.Sales_Ratio_Per_Dist;
            ddlStockValue.SelectedValue = ObjCustGeneral.StockValue;
            txtOutstanding_0_30.Text = ObjCustGeneral.Outstanding_0_30;
            txtOutstanding_31_60.Text = ObjCustGeneral.Outstanding_31_60;
            txtOutstanding_61_90.Text = ObjCustGeneral.Outstanding_61_90;
            txtOutstanding_91_180.Text = ObjCustGeneral.Outstanding_91_180;
            txtOutstanding_Age_180.Text = ObjCustGeneral.Outstanding_Age_180;
            ddlOutstanding_Replacement.SelectedValue = ObjCustGeneral.Outstanding_Replacement;
            txtFeeback.Text = ObjCustGeneral.Feeback;
            BindAttachedDocuments(lblMasterHeaderId.Text);
            SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
            DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dsdata.Tables[0].Rows.Count > 0)
            {
                if (dsdata.Tables[0].Rows[0]["Customer_Category"].ToString() == "DASIRF")
                {
                    //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
                    //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','0','0'", "Division_Name", "Division_Id", "");
                    //STD07052019 
                    helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivision " + Convert.ToString(Session[StaticKeys.DivTypeCusts]) + "", "Division", "ID", "");
                    //ETD07052019 
                    ddlDivision.SelectedValue = dsdata.Tables[0].Rows[0]["Division"].ToString();
                }
            }
            //DasSystemInfoDetail objDasDetail = new DasSystemInfoDetail();
            //int rowIndex = 0;
            //if (ViewState["Curtbl"] != null)

            //{

            //    DataTable dt = (DataTable)ViewState["Curtbl"];

            //    DataSet ds = ObjCustomerGeneralAccess.FillGridData(lblMasterHeaderId.Text);
            //    gvDetails.DataSource = ds;
            //    //gvDetails.DataBind();
            //    if (dt.Rows.Count > 0)

            //    {
            //        //DataRow drCurrentRow = null;
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

            //        {

            //            TextBox txtname = (TextBox)gvDetails.Rows[0].Cells[1].FindControl("txtName");

            //            TextBox txtprice = (TextBox)gvDetails.Rows[0].Cells[2].FindControl("txtPrice");
            //            txtname.Text = ds.Tables[0].Rows[i]["Company_Name"].ToString();

            //            txtprice.Text = ds.Tables[0].Rows[i]["Monthly_Turnover"].ToString();

            //            //AddNewRow();

            //            //drCurrentRow = dt.NewRow();

            //            //drCurrentRow["rowid"] = i + 1;

            //            //ds.Tables[0].Rows[i]["Company_Name"] = txtname.Text;

            //            //ds.Tables[0].Rows[i]["Monthly_Turnover"] = txtprice.Text;

            //            rowIndex++;
            //            //dt.Rows.Add(drCurrentRow);
            //        }


            //    }

            //}
        }
        else
        {
            //FillDeliveringPlant();
            lblCustomerGeneralId.Text = "0";
            Session[StaticKeys.SelectedModulePlantGrp] = ObjCustGeneral.ModulePlantGroupCode;
            SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
            DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
            if (dsdata.Tables[0].Rows.Count > 0)
            {
                if (dsdata.Tables[0].Rows[0]["Customer_Category"].ToString() == "DASIRF")
                {
                    //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
                    //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','0','0'", "Division_Name", "Division_Id", "");
                    //STD07052019 
                    helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivision " + Convert.ToString(Session[StaticKeys.DivTypeCusts]) + "", "Division", "ID", "");
                    //ETD07052019 
                    ddlDivision.SelectedValue = dsdata.Tables[0].Rows[0]["Division"].ToString();
                }
            }
        }

    }

    #endregion

    private void PopuplateDropDownList()
    {
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivisionList '" + lblMasterHeaderId.Text + "','SD1','" + lblCustomerGeneralId.Text + "','0','0'", "Division_Name", "Division_Id", "");
        //STD07052019 
        helperAccess.PopuplateDropDownList(ddlDivision, "pr_GetDivision " + Convert.ToString(Session[StaticKeys.DivTypeCusts]) + "", "Division", "ID", "");
        //ETD07052019 
        helperAccess.PopuplateDropDownList(ddlDeliveringPlant, "pr_Get_Delivering_Plant_List'" + lblMasterHeaderId.Text + "'", "Plant_Name", "Plant_Id", "");
        helperAccess.PopuplateDropDownList(ddlTerritory, "pr_Get_DAS_TERRITORY_MASTER", "TERRITORY", "ID", "");
        helperAccess.PopuplateDropDownList(ddlStructure_Of_Firm, "pr_Get_Das_Structure_Of_Firm", "FIRMSTRUCTURE", "ID", "");
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["Curtbl"] != null)

        {

            DataTable dt = (DataTable)ViewState["Curtbl"];

            DataRow drCurrentRow = null;

            int rowIndex = Convert.ToInt32(e.RowIndex);

            if (dt.Rows.Count > 1)

            {

                dt.Rows.Remove(dt.Rows[rowIndex]);

                drCurrentRow = dt.NewRow();

                ViewState["Curtbl"] = dt;

                gvDetails.DataSource = dt;

                gvDetails.DataBind();







                for (int i = 0; i < gvDetails.Rows.Count - 1; i++)

                {

                    gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);

                }

                SetOldData();

            }

        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    protected void FillDeliveringPlant()
    {
        DataSet ds = ObjCustomerGeneralAccess.FillDeliveringPlantData(lblMasterHeaderId.Text);
        if (ddlDeliveringPlant.SelectedValue == "")
        {
            ddlDeliveringPlant.SelectedValue = ds.Tables[0].Rows[0]["Plant_Id"].ToString();
        }
        else if (Convert.ToInt32(ddlDeliveringPlant.SelectedValue) > 0)
        {

        }
        else
        {
            ddlDeliveringPlant.SelectedValue = ds.Tables[0].Rows[0]["Plant_Id"].ToString();
        }
    }

    protected void BindGridview()

    {
        DataSet ds = ObjCustomerGeneralAccess.FillGridData(lblMasterHeaderId.Text);
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 0)
        { dt = ds.Tables[0]; }
        dt.Columns.Add("rowid", typeof(int));
        //dt.Columns.Add("Company_Name", typeof(string));
        //dt.Columns.Add("Monthly_Turnover", typeof(string));

        if (dt.Rows.Count > 0)
        {
            //dt = ds.Tables[0];
            //dt.Columns.Add("rowid", typeof(int));
        }
        else
        {
            DataRow dr = dt.NewRow();
            if (dt.Columns.Count <= 2)
            {
                dt.Columns.Add("Company_Name", typeof(string));
                dt.Columns.Add("Monthly_Turnover", typeof(string));
            }
            dr["rowid"] = 1;

            dr["Company_Name"] = string.Empty;

            dr["Monthly_Turnover"] = string.Empty;

            dt.Rows.Add(dr);
        }
        ViewState["Curtbl"] = dt;

        gvDetails.DataSource = dt;

        gvDetails.DataBind();
        int rowIndex = 0;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)

            {

                TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");

                TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");

                txtname.Text = dt.Rows[i]["Company_Name"].ToString();

                txtprice.Text = dt.Rows[i]["Monthly_Turnover"].ToString();

                rowIndex++;

            }
        }

    }

    private void AddNewRow()

    {
	//DTC160519
      //  SalesAreaAccess ObjSalesAreaAccess = new SalesAreaAccess();
     //   DataSet dsdata = ObjSalesAreaAccess.GetDivisionType(Convert.ToInt32(lblMasterHeaderId.Text));
      //  if (dsdata.Tables[0].Rows.Count > 0)
      //  {
         //   if (dsdata.Tables[0].Rows[0]["Customer_Category"].ToString() == "DASIRF")
          //  {
           //     //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
         //       ddlDivision.SelectedValue = dsdata.Tables[0].Rows[0]["Division"].ToString();
         //   }
       // }
        //helperAccess.PopuplateDropDownList(ddlDivision, "pr_Get_Division_List", "Division_Name", "Division_Id", "");
			//DTC160519
        int rowIndex = 0;




        if (ViewState["Curtbl"] != null)

        {

            DataTable dt = (DataTable)ViewState["Curtbl"];

            DataRow drCurrentRow = null;

            if (dt.Rows.Count > 0)

            {

                for (int i = 1; i <= dt.Rows.Count; i++)

                {

                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");

                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");

                    drCurrentRow = dt.NewRow();

                    drCurrentRow["rowid"] = i + 1;

                    dt.Rows[i - 1]["Company_Name"] = txtname.Text;

                    dt.Rows[i - 1]["Monthly_Turnover"] = txtprice.Text;

                    rowIndex++;

                }

                dt.Rows.Add(drCurrentRow);

                ViewState["Curtbl"] = dt;

                gvDetails.DataSource = dt;

                gvDetails.DataBind();

            }

        }

        else

        {

            Response.Write("ViewState Value is Null");

        }

        SetOldData();

    }

    private void SetOldData()

    {

        int rowIndex = 0;

        if (ViewState["Curtbl"] != null)

        {

            DataTable dt = (DataTable)ViewState["Curtbl"];

            if (dt.Rows.Count > 0)

            {

                for (int i = 0; i < dt.Rows.Count; i++)

                {

                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");

                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");

                    txtname.Text = dt.Rows[i]["Company_Name"].ToString();

                    txtprice.Text = dt.Rows[i]["Monthly_Turnover"].ToString();

                    rowIndex++;

                }

            }

        }

    }

    private void ConfigControl()
    {
        if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "40")
        {
            lbltxtCur_Territory_Sales.Visible = true;
            reqtxtCur_Territory_Sales.Enabled = true;
            lbltxtDist_In_Territory.Visible = true;
            reqtxtDist_In_Territory.Enabled = true;
            lbltxtSales_Ratio_Per_Dist.Visible = true;
            reqtxtSales_Ratio_Per_Dist.Enabled = true;
            //lbltxtOutstanding_0_30.Visible = true;
            //reqtxtOutstanding_0_30.Enabled = true;
            //lbltxtOutstanding_31_60.Visible = true;
            //reqtxtOutstanding_31_60.Enabled = true;
            //lbltxtOutstanding_61_90.Visible = true;
            //reqtxtOutstanding_61_90.Enabled = true;
            //lbltxtOutstanding_91_180.Visible = true;
            //reqtxtOutstanding_91_180.Enabled = true;
            //lbltxtOutstanding_Age_180.Visible = true;
            //reqtxtOutstanding_Age_180.Enabled = true;
        }
        else
        {
            lbltxtCur_Territory_Sales.Visible = false;
            reqtxtCur_Territory_Sales.Enabled = false;
            lbltxtDist_In_Territory.Visible = false;
            reqtxtDist_In_Territory.Enabled = false;
            lbltxtSales_Ratio_Per_Dist.Visible = false;
            reqtxtSales_Ratio_Per_Dist.Enabled = false;
            //lbltxtOutstanding_0_30.Visible = false;
            //reqtxtOutstanding_0_30.Enabled = false;
            //lbltxtOutstanding_31_60.Visible = false;
            //reqtxtOutstanding_31_60.Enabled = false;
            //lbltxtOutstanding_61_90.Visible = false;
            //reqtxtOutstanding_61_90.Enabled = false;
            //lbltxtOutstanding_91_180.Visible = false;
            //reqtxtOutstanding_91_180.Enabled = false;
            //lbltxtOutstanding_Age_180.Visible = false;
            //reqtxtOutstanding_Age_180.Enabled = false;
        }
    }

    #region Document Upload

    private void BindAttachedDocuments(string vendorId)
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
            }
            else
            {
                grdAttachedDocs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            objDb = null;
        }
    }

    private bool SaveDocuments(string vendorId)
    {
        VendorMasterAccess ObjVendorMasterAccess = new VendorMasterAccess();
        DataTable dt = ObjVendorMasterAccess.GetRequestNoByMasterHeaderId(lblMasterHeaderId.Text);

        Session[StaticKeys.RequestNo] = dt.Rows[0]["Request_No"].ToString();
        string savePath = "";
        string StrPath = "~/Transaction/Customer/CustomerDocuments/" + Session[StaticKeys.RequestNo].ToString() + "/";
        savePath = MapPath(StrPath);

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        try
        {
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];

                if (uploadfile.ContentLength > 0)
                {
                    UploadDocument(uploadfile, StrPath, savePath);
                }
            }
            return true;
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
        }
        else
        {
            flag = false;
            lblMsg.Text = "Error While Saving Customer Details.";
        }


        return flag;
    }

    #endregion

}
