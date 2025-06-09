using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Vendor_PartnerFunction : System.Web.UI.Page
{
    VendorChangeAccess ObjVendorChangeAccess = new VendorChangeAccess();
    HelperAccess helperAccess = new HelperAccess();
    //public bool isEditable { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    if (Session[StaticKeys.MasterHeaderId] != null)
                    {
                        lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
                        PopuplateDropDownList();
                        string sectionId = lblSectionId.Text.ToString();
                        string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
                        string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
                        string mode = Session[StaticKeys.Mode].ToString();
                        lblActionType.Text = Session[StaticKeys.ActionType].ToString();
                        lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();

                        HelperAccess.SetNextPreviousSectionURL(sectionId, lblMasterHeaderId.Text, deptId, btnPrevious, btnNext);
                        if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
                        {
                            lnkAddNew.Visible = true;
                            //isEditable = true;
                            trButton.Visible = true;
                            grvVendorChange.Columns[6].Visible = true;
                            PartnerFunExcelUpload.Visible = true;
                        }
                        else
                        {
                            if (Session[StaticKeys.LoggedIn_User_DeptId].ToString() == "13")
                            {
                                ExcelDownload1.Visible = true;
                                PartnerFunExcelUpload.Visible = false;
                            }
                        }
                        BindVendorChangeData();
                    }
                    else
                    {
                        Response.Redirect("VendorMaster.aspx");
                    }
                }
            }


            //if (Session[StaticKeys.LoggedIn_User_Id] != null)
            //{
            //    if (!IsPostBack)
            //    {
            //        lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
            //        if (Session[StaticKeys.MasterHeaderId] != null)
            //        {
            //            lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
            //            string sectionId = lblSectionId.Text.ToString();
            //            string moduleId = Session[StaticKeys.SelectedModuleId].ToString();
            //            string deptId = Session[StaticKeys.LoggedIn_User_DeptId].ToString();
            //            lblActionType.Text = Session[StaticKeys.ActionType].ToString();
            //            PopuplateDropDownList(); 
            //            string mode = Session[StaticKeys.Mode].ToString();
            //            if ((MaterialMasterAccess.IsUserHasInputRights(moduleId, deptId, sectionId, lblUserId.Text)) && (mode == "M" || mode == "N"))
            //            {
            //                trButton.Visible = true;
            //                btnAdd.Visible = true;
            //                grdPFunDetailAdd.Columns[0].Visible = true;
            //                isEditable = true;
            //                txtVendorCode.Enabled = true;
            //                txtVendorName.Enabled = true;
            //                txtVendorCode.ReadOnly = false;
            //                txtVendorName.ReadOnly = false;
            //            }
            //            else
            //            {
            //                isEditable = false;
            //                btnAdd.Visible = false;
            //                grdPFunDetailAdd.Columns[0].Visible = false;
            //                txtVendorCode.Enabled = false;
            //                txtVendorName.Enabled = false;
            //                txtVendorCode.ReadOnly = true;
            //                txtVendorName.ReadOnly = true;

            //            }
            //            FillPFunData(lblMode.Text);
            //        }
            //        else
            //        {
            //            Response.Redirect("VendorMaster.aspx");
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {

        }
    }

    private void BindVendorChangeData()
    {
        grvVendorChange.DataSource = ObjVendorChangeAccess.GetVendorChangeData(lblMasterHeaderId.Text);
        grvVendorChange.DataBind();
    }



    protected void btnclose_Click(Object sender, EventArgs e)
    {
        Response.Redirect("PartnerFunction.aspx");
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    //if (SaveVendorChangeData())
    //    //{
    //    //    ModalPopupExtender.Hide();

    //    //    lblMsg.Text = Messages.GetMessage(1);
    //    //    pnlMsg.CssClass = "success";
    //    //    pnlMsg.Visible = true;

    //    //    BindVendorChangeData();
    //    //}
    //    //else
    //    //{
    //    //    ModalPopupExtender.Show();
    //    //}
    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //isEditable = true;
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;

        txtVendorCode.ReadOnly = true;
        txtVendorCode.Enabled = false;
        txtVendorName.Enabled = false;
        txtVendorName.ReadOnly = true;

        ImageButton lnkEditValue = (ImageButton)sender;

        lblVendorChange.Text = lnkEditValue.CommandArgument;
        FillPFunData(lblMode.Text);
        //FillVendorChangeDetailData();
        //lblVendorChangeAction.Text = "E";
        ModalPopupExtender.Show();
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //isEditable = true;
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;


        ImageButton btnDelete = (ImageButton)sender;

        lblVendorChange.Text = btnDelete.CommandArgument;
        int id = Convert.ToInt32(lblVendorChange.Text);
        ObjVendorChangeAccess.DeleteMain(id);

        BindVendorChangeData();
        //FillVendorChangeDetailData();
        //lblVendorChangeAction.Text = "E";
        //ModalPopupExtender.Show();
    }

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    isEditable = true;
    //    ImageButton btnDelete = (ImageButton)sender;

    //    ObjVendorChangeAccess.DeleteVendorChangeDetail(btnDelete.CommandArgument);
    //    BindVendorChangeData();
    //    ModalPopupExtender.Show();
    //}

    protected void lnkAddValue_Click(object sender, EventArgs e)
    {
        ImageButton lnkAddValue = (ImageButton)sender;
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        //lblVendorChangeDetailId.Text = "0";
        //lblVendorChangeAction.Text = "F";
        //lblVendorChange.Text = lnkAddValue.CommandArgument;
        //FillVendorChangeDetailData();
        //BindVendorChangeData();
        ModalPopupExtender.Show();

    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        //isEditable = true;
        lblVendorChange.Text = "0";
        lblPartnerFunctionId.Text = "0";

        txtVendorCode.ReadOnly = false;
        txtVendorCode.Enabled = true;
        txtVendorName.Enabled = true;
        txtVendorName.ReadOnly = false;

        //lblVendorChangeAction.Text = "V";
        //lblVendorChangeDetailId.Text = "0";
        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        //FillVendorChangeData();
        ClearFun();

        ModalPopupExtender.Show();
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckIsValid())
        {
            lblMsg.Text = Messages.GetMessage(1);
            pnlMsg.CssClass = "success";
            pnlMsg.Visible = true;
            Response.Redirect("PartnerFunction.aspx");
        }
        else
        {
            lblMsg.Text = "Please fill atleast one feild.";
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
        }
    }

    private bool CheckIsValid()
    {
        bool flg = false;

        if (grvVendorChange.Rows.Count > 0)
            flg = true;

        return flg;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
    }

    protected void grvVendorChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblVendorChangeId = (Label)e.Row.FindControl("lblVendorChangeId");

            GridView grvVendorChangeDtl = (GridView)e.Row.FindControl("grvVendorChangeDtl");
            bindgrvVendorChangeDtl(Convert.ToInt32(lblVendorChangeId.Text), grvVendorChangeDtl);
            //grvVendorChangeDtl.Columns[6].Visible = lnkAddNew.Visible;
        }
    }


    private void bindgrvVendorChangeDtl(int VendorChangeId, GridView grvVendorChangeDtl)
    {
        grvVendorChangeDtl.DataSource = ObjVendorChangeAccess.GetVendorPfunChangeDetailData(VendorChangeId);
        grvVendorChangeDtl.DataBind();
    }

    private void FillPartnerFunData()
    {
        AddBlankRow();
    }

    /// <summary>
    /// Carve_LC17&LC23
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        helperAccess.PopuplateDropDownList(ddlPurchaseOrg, "pr_GetPurchaseOrgList 0", "Purchase_Org_Name", "Purchase_Org_Id");
        if (ddlCompanyCode.SelectedValue == "77")
        {
            ddlPurchaseOrg.SelectedValue = "8";
        }
        else if (ddlCompanyCode.SelectedValue == "78")
        {
            ddlPurchaseOrg.SelectedValue = "8";
        }
        else if (ddlCompanyCode.SelectedValue == "34")
        {
            ddlPurchaseOrg.SelectedValue = "4";
        }
        else if (ddlCompanyCode.SelectedValue == "79")
        {
            ddlPurchaseOrg.SelectedValue = "9";
        }
        else
        {
            ddlPurchaseOrg.SelectedValue = "2";
        }
        ModalPopupExtender.Show();
    }

    private void PopuplateDropDownList()
    {
        try
        {
            //HelperAccess helperAccess = new HelperAccess();
            helperAccess.PopuplateDropDownList(ddlCompanyCode, "pr_GetCompanyCodeList 0", "Company_Name", "Company_Id");
            helperAccess.PopuplateDropDownList(ddlPurchaseOrg, "pr_GetPurchaseOrgList 0", "Purchase_Org_Name", "Purchase_Org_Id");

            ddlCompanyCode.SelectedValue = "32";
            ddlPurchaseOrg.SelectedValue = "2";

            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            DataSet ds;
            ds = objMatAccess.ReadModules("V");
            ddlVendorAccGrp.DataSource = ds;
            ddlVendorAccGrp.DataTextField = "Module_Name";
            ddlVendorAccGrp.DataValueField = "Module_Id";
            ddlVendorAccGrp.DataBind();
            ddlVendorAccGrp.Items.Insert(0, new ListItem("---Select---", "0"));


        }
        catch (Exception ex) { }
    }

    protected void txtVendorCode_TextChanged(object sender, EventArgs e)
    {
        if (txtVendorCode.Text != "")
        {
            txtVendorCode.Text = txtVendorCode.Text.ToUpper();
            string str = txtVendorCode.Text.Substring(0, 1).ToUpper();
            string str1 = txtVendorCode.Text.Substring(0, 2);
            int strcode = SafeTypeHandling.ConvertStringToInt32(txtVendorCode.Text);

            switch (str)
            {
                case "L":
                    regtxtVendorCode.ValidationExpression = "^[\\S]{4}$";
                    ddlVendorAccGrp.SelectedValue = "26";

                    break;
                case "E":
                    regtxtVendorCode.ValidationExpression = "^[\\S]{7,10}$";
                    ddlVendorAccGrp.SelectedValue = "27";
                    break;

                default:
                    regtxtVendorCode.ValidationExpression = "^[\\d]{6}$";
                    if (strcode >= 100000 && strcode < 150000) //1-Series
                        ddlVendorAccGrp.SelectedValue = "18";
                    else if (strcode >= 150000 && strcode < 200000)//15-series
                        ddlVendorAccGrp.SelectedValue = "19";
                    else if (strcode >= 200000 && strcode < 250000)//2-series
                        ddlVendorAccGrp.SelectedValue = "20";
                    else if (strcode >= 250000 && strcode < 300000)//25-series
                        ddlVendorAccGrp.SelectedValue = "21";
                    else if (strcode >= 300000 && strcode < 350000)//3-series
                        ddlVendorAccGrp.SelectedValue = "22";
                    else if (strcode >= 400000 && strcode < 450000)//4-Series
                        ddlVendorAccGrp.SelectedValue = "24";
                    else if (strcode >= 450000 && strcode < 500000)//45-Series
                        ddlVendorAccGrp.SelectedValue = "25";
                    else if ((strcode >= 600000 && strcode < 650000)
                        || (strcode >= 678001 && strcode < 699999))//6-Series & 67-series
                        ddlVendorAccGrp.SelectedValue = "28";
                    else if (strcode >= 650000 && strcode < 700000)//65-Series
                        ddlVendorAccGrp.SelectedValue = "30";
                    else if (strcode >= 720000 && strcode < 730000)//72-Series
                        ddlVendorAccGrp.SelectedValue = "32";
                    else if (strcode >= 900000 && strcode < 999999)//9-Series
                        ddlVendorAccGrp.SelectedValue = "15";
                    else if (strcode >= 500000 && strcode < 599999)//5-Series Z068
                        ddlVendorAccGrp.SelectedValue = "194";
                    break;
            }
            ModalPopupExtender.Show();
            txtVendorName.Focus();
            //AddBlankRow();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // isEditable = true;
        if (Valid())
        {
            if (SaveHeader())
            {
                if (SaveDetail())
                {
                    ModalPopupExtender.Hide();
                    lblMsg.Text = Messages.GetMessage(1);
                    pnlMsg.CssClass = "success";
                    pnlMsg.Visible = true;
                    //FillPFunData(lblMode.Text);
                    BindVendorChangeData();
                }
                else
                {
                    ModalPopupExtender.Show();
                    lblMsg1.Text = "Error while saving Details";
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
            }
            else
            {
                ModalPopupExtender.Show();
                lblMsg1.Text = "Error while saving Details";
                pnlMsg1.CssClass = "error";
                pnlMsg1.Visible = true;
            }
        }
        else
        {
            ModalPopupExtender.Show();
            lblMsg1.Text = "Please enter valid Partner function data";
            pnlMsg1.CssClass = "error";
            pnlMsg1.Visible = true;

        }
    }

    private void FillPFunData(string mode)
    {
        VendorChange ObjVendorChange = GetPFunHeaderData();
        try
        {
            if (ObjVendorChange.Vendor_Change_Id > 0)
            {

                lblVendorChange.Text = ObjVendorChange.Vendor_Change_Id.ToString();

                VendorChange objVendorChange = ObjVendorChangeAccess.GetVendorChange(Convert.ToInt32(lblVendorChange.Text));

                lblVendorChange.Text = ObjVendorChange.Vendor_Change_Id.ToString();
                txtVendorCode.Text = ObjVendorChange.Customer_Code;
                txtVendorName.Text = ObjVendorChange.Vendor_Desc;
                ddlCompanyCode.Text = ObjVendorChange.Company_Code;
                ddlVendorAccGrp.SelectedValue = ObjVendorChange.Vendor_Group;
                ddlPurchaseOrg.SelectedValue = ObjVendorChange.Purchase_Org;

                //DataSet dsPFunDetails = new DataSet();
                //dsPFunDetails = objVendorChangeAccess.GetBOMDetail(lblBOMHeaderId.Text);

                grdPFunDetailAdd.DataSource = ObjVendorChangeAccess.GetPFunDetail(lblVendorChange.Text);
                grdPFunDetailAdd.DataBind();
                grdPFunDetailAdd.Visible = true;
            }
            else
            {
                lblVendorChange.Text = "0";
                AddBlankRow();
            }

        }
        catch (Exception ex)
        {
        }

    }

    private VendorChange GetPFunHeaderData()
    {
        //return ObjVendorChangeAccess.GetPFunHeaderData(lblMasterHeaderId.Text);

        return ObjVendorChangeAccess.GetPFunHeaderData(lblVendorChange.Text);
    }

    private bool Valid()
    {
        bool flg = false;
        if (grdPFunDetailAdd.Rows.Count != 0)
        {
            foreach (GridViewRow row in grdPFunDetailAdd.Rows)
            {

                string p1 = (row.FindControl("txtsVendor_Code") as TextBox).Text;
                string p2 = (row.FindControl("txtsVendor_Desc") as TextBox).Text;
                string p3 = (row.FindControl("ddlPfun_Lookup_Id") as DropDownList).SelectedValue;

                if (p1 == "" || p2 == "" || p3 == "0")
                {
                    flg = false;
                    return flg;
                }
                else
                {
                    flg = true;
                }
            }
        }
        return flg;
    }


    private bool SaveHeader()
    {
        bool flg = false;

        try
        {
            VendorChange ObjVendorChange = GetControlsValue();

            int pfunHeaderId = ObjVendorChangeAccess.SavePFunHeaderData(ObjVendorChange);
            if (pfunHeaderId > 0)
            {
                lblVendorChange.Text = pfunHeaderId.ToString();
                flg = true;

                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
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
        }
        return flg;
    }

    private bool SaveDetail()
    {
        bool flg = false;
        try
        {
            foreach (GridViewRow row in grdPFunDetailAdd.Rows)
            {
                VendorPartnerFun objVendorPartnerFun = new VendorPartnerFun();
                objVendorPartnerFun = GetPFunDetailData(row);
                if (ObjVendorChangeAccess.SavePFunDetails(objVendorPartnerFun) > 0)
                {
                    flg = true;
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return flg;
    }

    private VendorPartnerFun GetPFunDetailData(GridViewRow row)
    {
        VendorPartnerFun objVendorPartnerFun = new VendorPartnerFun();
        Utility objUtil = new Utility();
        try
        {
            Label lblVendor_PFun_Detail_Id = row.FindControl("lblVendor_PFun_Detail_Id") as Label;
            DropDownList ddlPfun_Lookup_Id = row.FindControl("ddlPfun_Lookup_Id") as DropDownList;
            TextBox txtsVendor_Code = row.FindControl("txtsVendor_Code") as TextBox;
            TextBox txtsVendor_Desc = row.FindControl("txtsVendor_Desc") as TextBox;

            objVendorPartnerFun.Vendor_Change_Id = Convert.ToInt32(lblVendorChange.Text);
            objVendorPartnerFun.Vendor_PFun_Detail_Id = Convert.ToInt32(lblVendor_PFun_Detail_Id.Text);
            objVendorPartnerFun.sPfun_Lookup_Code = ddlPfun_Lookup_Id.SelectedValue;
            objVendorPartnerFun.sVendor_Code = txtsVendor_Code.Text;
            objVendorPartnerFun.sVendor_Desc = txtsVendor_Desc.Text;
            objVendorPartnerFun.bIsActive = 1;
            objVendorPartnerFun.nCreatedBy = Convert.ToInt32(lblUserId.Text);
            objVendorPartnerFun.dCreatedOn = objUtil.GetDate();
            objVendorPartnerFun.sCreatedIp = objUtil.GetIpAddress();
        }
        catch (Exception ex)
        {

        }
        return objVendorPartnerFun;
    }

    private VendorChange GetControlsValue()
    {

        VendorChange ObjVendorChange = new VendorChange();
        Utility objUtil = new Utility();
        try
        {
            ObjVendorChange.Vendor_Change_Id = Convert.ToInt32(lblVendorChange.Text);
            ObjVendorChange.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
            ObjVendorChange.Customer_Code = txtVendorCode.Text;
            ObjVendorChange.Company_Code = ddlCompanyCode.SelectedValue;
            ObjVendorChange.Vendor_Group = ddlVendorAccGrp.SelectedValue;
            ObjVendorChange.Purchase_Org = ddlPurchaseOrg.SelectedValue;
            ObjVendorChange.Vendor_Desc = txtVendorName.Text;
            ObjVendorChange.IsActive = 1;
            ObjVendorChange.UserId = lblUserId.Text;
            ObjVendorChange.TodayDate = objUtil.GetDate();
            ObjVendorChange.IPAddress = objUtil.GetIpAddress();

        }
        catch (Exception ex)
        {

        }
        return ObjVendorChange;
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        // isEditable = true;
        AddBlankRow();
        ModalPopupExtender.Show();
    }

    private void AddBlankRow()
    {
        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();
        int tempId = 1;
        int j = 0;
        try
        {
            //Columns
            dt.Columns.Add(new DataColumn("Vendor_PFun_Detail_Id"));
            dt.Columns.Add(new DataColumn("sVendor_Code"));
            dt.Columns.Add(new DataColumn("sVendor_Desc"));
            dt.Columns.Add(new DataColumn("sPfun_Lookup_Code"));

            foreach (GridViewRow row in grdPFunDetailAdd.Rows)
            {
                dr = dt.NewRow();
                dr["Vendor_PFun_Detail_Id"] = (row.FindControl("lblVendor_PFun_Detail_Id") as Label).Text;
                dr["sVendor_Code"] = (row.FindControl("txtsVendor_Code") as TextBox).Text;
                dr["sVendor_Desc"] = (row.FindControl("txtsVendor_Desc") as TextBox).Text;
                dr["sPfun_Lookup_Code"] = (row.FindControl("ddlPfun_Lookup_Id") as DropDownList).SelectedValue;
                dt.Rows.Add(dr);
                tempId += 1;
            }


            if (tempId == 1)
                j = 1;
            for (int i = tempId; i < SafeTypeHandling.ConvertStringToInt32(txtNewRow.Text) + tempId; i++)
            {
                dr = dt.NewRow();
                dr["Vendor_PFun_Detail_Id"] = tempId;
                j++;
                dt.Rows.Add(dr);
            }



            dstData.Tables.Add(dt);
            dstData.AcceptChanges();

            //DataView dv = new DataView(dstData.Tables[0]);
            ////dv.Sort = "sVendor_Code Asc";
            //DataTable dtSorted = dv.ToTable();

            grdPFunDetailAdd.DataSource = dstData;
            grdPFunDetailAdd.DataBind();
            ViewState["dstPFunDetail"] = dstData;
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdPFunDetailAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // isEditable = true;
        Control ctl = e.CommandSource as Control;
        GridViewRow currentRow = ctl.NamingContainer as GridViewRow;

        DataRow dr;
        DataTable dt = new DataTable();
        DataSet dstData = new DataSet();

        if (e.CommandName == "D")
        {

            try
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Get the value of column from the DataKeys using the RowIndex.
                //int id = Convert.ToInt32(grdPFunDetailAdd.DataKeys[rowIndex].Values[0]);
                int id = Convert.ToInt32(rowIndex);
                ObjVendorChangeAccess.DeleteRow(id);

                grdPFunDetailAdd.DataSource = ObjVendorChangeAccess.GetPFunDetail(lblVendorChange.Text);
                grdPFunDetailAdd.DataBind();

                if (grdPFunDetailAdd.Rows.Count == 0)
                {
                    ClearFun();
                }

                //DataSet dsPFunDetails = new DataSet();
                //dsPFunDetails = objVendorChangeAccess.GetBOMDetail(lblBOMHeaderId.Text);




                dt.Columns.Add(new DataColumn("Vendor_PFun_Detail_Id"));
                dt.Columns.Add(new DataColumn("sVendor_Code"));
                dt.Columns.Add(new DataColumn("sVendor_Desc"));
                dt.Columns.Add(new DataColumn("sPfun_Lookup_Code"));

                foreach (GridViewRow row in grdPFunDetailAdd.Rows)
                {
                    dr = dt.NewRow();
                    dr["Vendor_PFun_Detail_Id"] = (row.FindControl("lblVendor_PFun_Detail_Id") as Label).Text;
                    dr["sVendor_Code"] = (row.FindControl("txtsVendor_Code") as TextBox).Text;
                    dr["sVendor_Desc"] = (row.FindControl("txtsVendor_Desc") as TextBox).Text;
                    dr["sPfun_Lookup_Code"] = (row.FindControl("ddlPfun_Lookup_Id") as DropDownList).SelectedValue;
                    dt.Rows.Add(dr);

                    //if (objBOMAccess.GetBOMDetail(dr["BOM_HeaderDetail_Id"].ToString(), lblBOMHeaderId.Text) > 0)
                    //{
                    //    BOMDetail objBOMDetail = GetBOMDetailData(row);
                    //    objBOMAccess.SaveBOMDetails(objBOMDetail);
                    //}
                }

                dstData.Tables.Add(dt);
                dstData.AcceptChanges();

                //if ((currentRow.FindControl("lblUpd_Flag") as Label).Text == "I" || (currentRow.FindControl("lblUpd_Flag") as Label).Text == "U" || (currentRow.FindControl("lblUpd_Flag") as Label).Text == "D")
                //{
                //    dstData.Tables[0].Rows[currentRow.RowIndex].Delete();
                //    dstData.AcceptChanges();
                //}

                //DataView dv = new DataView(dstData.Tables[0]);
                ////dv.Sort = "sVendor_Code Asc";
                //DataTable dtSorted = dv.ToTable();

                grdPFunDetailAdd.DataSource = dstData;
                grdPFunDetailAdd.DataBind();
                ViewState["dstPFunDetail"] = dstData;
            }
            catch (Exception ex)
            {

            }
        }

        pnlMsg.Visible = false;
        pnlMsg1.Visible = false;
        ModalPopupExtender.Show();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ClearFun()
    {
        try
        {
            lblVendorChange.Text = "0";
            lblPartnerFunctionId.Text = "0";
            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            PopuplateDropDownList();
            //AddBlankRow();

            grdPFunDetailAdd.DataSource = null;
            grdPFunDetailAdd.DataBind();
            if (grdPFunDetailAdd.Rows.Count < 1)
            {
                AddBlankRow();
            }
        }
        catch (Exception ex) { }
    }

    protected void grdPFunDetailAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtsVendor_Code = (TextBox)e.Row.FindControl("txtsVendor_Code");
                TextBox txtsVendor_Desc = (TextBox)e.Row.FindControl("txtsVendor_Desc");

                DropDownList ddlPfun_Lookup_Id = (DropDownList)e.Row.FindControl("ddlPfun_Lookup_Id");
                helperAccess.PopuplateDropDownList(ddlPfun_Lookup_Id, "pr_GetDropDownListByControlNameModuleType_Code 'V','ddlPfun_Lookup_Id'", "LookUp_Desc", "LookUp_Code");
                ddlPfun_Lookup_Id.SelectedValue = grdPFunDetailAdd.DataKeys[e.Row.RowIndex].Values[1].ToString();

                //if (isEditable == false)
                //{
                //    ddlPfun_Lookup_Id.Enabled = false;
                //    txtsVendor_Code.ReadOnly = true;
                //    txtsVendor_Desc.ReadOnly = true;
                //    txtsVendor_Code.Enabled = false;
                //    txtsVendor_Desc.Enabled = false;
                //}
                //else
                //{
                //    ddlPfun_Lookup_Id.Enabled = true;
                //    txtsVendor_Code.ReadOnly = false;
                //    txtsVendor_Desc.ReadOnly = false;
                //    txtsVendor_Code.Enabled = true;
                //    txtsVendor_Desc.Enabled = true;
                //}
            }
        }
        catch (Exception ex) { }
    }
}