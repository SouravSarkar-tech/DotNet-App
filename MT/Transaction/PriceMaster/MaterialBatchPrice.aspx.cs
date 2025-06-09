using Accenture.MWT.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class Transaction_PriceMaster_MaterialBatchPrice : System.Web.UI.Page
{
    PriceMasterAccess ObjPriceMasterAccess = new PriceMasterAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
            if (Session[StaticKeys.MasterHeaderId] != null)
            {
                lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();

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
                }
                //SDT17052019 Commented By NR  
                //if (lblModuleId.Text == "220")
                //EDT17052019 Commented By NR  
                //SDT17052019 Change By NR , Desc : Get Module ID from web config
                if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMP"]))
                {
                    this.GridView1.Columns[5].Visible = false;
                }
                else
                {
                    this.GridView1.Columns[5].Visible = true;
                }

                setInitialData(lblMasterHeaderId.Text);
            }
            else
            {
                Response.Redirect("PriceMaster.aspx");
            }
        }
    }

    private PriceMasterCreate GetPriceMasterData()
    {
        return ObjPriceMasterAccess.GetPriceMasterData(Convert.ToInt32(lblMasterHeaderId.Text));
    }

    private void SetInitialRow()
    {
        string sdate = "";
        try
        {
            DateTime date = System.DateTime.Now;
            sdate = date.ToString("dd/MM/yyyy");
            sdate = date.ToString("dd/mm/yyyy");
            sdate = sdate.Replace(@"/", "");

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Material_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Material_Desc", typeof(string)));
            dt.Columns.Add(new DataColumn("Material_Group", typeof(string)));

            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("ZMRP", typeof(string)));
            dt.Columns.Add(new DataColumn("ZTRP", typeof(string)));
            dt.Columns.Add(new DataColumn("ZSPL", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Division", typeof(string)));
            dt.Columns.Add(new DataColumn("dEffectivedate", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["ID"] = string.Empty;
            dr["Material_Code"] = string.Empty;
            dr["Material_Desc"] = string.Empty;
            dr["Material_Group"] = string.Empty;
            dr["Batch"] = string.Empty;
            dr["ZMRP"] = string.Empty;
            dr["ZTRP"] = string.Empty;
            dr["ZSPL"] = string.Empty;
            dr["Unit"] = string.Empty;
            dr["Division"] = string.Empty;
            dr["dEffectivedate"] = null;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            WritePRMasterLog("CreatePRLog_" + sdate + ".txt", "ex" + ex.Message);
        }
    }

    private void AddNewRowToGrid()
    {
        setInitialData(lblMasterHeaderId.Text);
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataSet dtCurrentTable = (DataSet)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Tables[0].Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Tables[0].Rows.Count; i++)
                {
                    Utility ObjUtil = new Utility();
                    Literal box1 = (Literal)GridView1.Rows[rowIndex].Cells[1].FindControl("txtID");
                    TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("txtMaterial_Code");
                    TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtMaterial_Desc");
                    TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txtMaterial_Group");
                    //SDT17052019 Commented By NR  
                    //if (lblModuleId.Text == "219")
                    //EDT17052019 Commented By NR  
                    //SDT17052019 Change By NR , Desc : Get Module ID from web config
                    if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                    {
                        TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtBatch");
                    }
                    TextBox box6 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("txtZMRP");
                    TextBox box7 = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("txtZTRP");
                    TextBox box8 = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("txtZSPL");
                    TextBox box9 = (TextBox)GridView1.Rows[rowIndex].Cells[9].FindControl("txtUnit");
                    TextBox box10 = (TextBox)GridView1.Rows[rowIndex].Cells[10].FindControl("txtDivision");
                    TextBox box11 = (TextBox)GridView1.Rows[rowIndex].Cells[11].FindControl("txtdEffectivedate");
                    drCurrentRow = dtCurrentTable.Tables[0].NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    if (dtCurrentTable.Tables[0].Rows[i - 1]["ID"].ToString() != "")
                    {
                        dtCurrentTable.Tables[0].Rows[i - 1]["ID"] = box1.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["Material_Code"] = box2.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["Material_Desc"] = box3.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["Material_Group"] = box4.Text;
                        //SDT17052019 Commented By NR  
                        //if (lblModuleId.Text == "219")
                        //EDT17052019 Commented By NR  
                        //SDT17052019 Change By NR , Desc : Get Module ID from web config
                        if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                        {
                            TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtBatch");
                            dtCurrentTable.Tables[0].Rows[i - 1]["Batch"] = box5.Text;
                        }
                        dtCurrentTable.Tables[0].Rows[i - 1]["ZMRP"] = box6.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["ZTRP"] = box7.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["ZSPL"] = box8.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["Unit"] = box9.Text;
                        dtCurrentTable.Tables[0].Rows[i - 1]["Division"] = box10.Text;
                        string sdate = "";
                        try
                        {
                            DateTime date = System.DateTime.Now;
                            sdate = date.ToString("dd/MM/yyyy");
                            sdate = sdate.Replace(@"/", "");

                            if (box11.Text != "")
                            { dtCurrentTable.Tables[0].Rows[i - 1]["dEffectivedate"] = ObjUtil.GetDDMMYYYY(box11.Text); }
                            else { dtCurrentTable.Tables[0].Rows[i - 1]["dEffectivedate"] = null; }
                        }
                        catch (Exception ex)
                        {
                            WritePRMasterLog("CreatePRLog_" + sdate + ".txt", "ex1" + ex.Message);
                        }
                        rowIndex++;
                    }
                }
                dtCurrentTable.Tables[0].Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                GridView1.DataSource = dtCurrentTable;
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void setInitialData(string Master_Header_Id)
    {
        DataSet dst = ObjPriceMasterAccess.FillGridData(lblMasterHeaderId.Text);
        if (dst.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dst;
            GridView1.DataBind();
            ViewState["CurrentTable"] = dst;
        }
        if (ViewState["CurrentTable"] != null)
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataSet dt = (DataSet)ViewState["CurrentTable"];
                if (dt.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        Utility ObjUtil = new Utility();
                        Literal box1 = (Literal)GridView1.Rows[rowIndex].Cells[1].FindControl("txtID");
                        TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("txtMaterial_Code");
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtMaterial_Desc");
                        TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txtMaterial_Group");
                        //SDT17052019 Commented By NR  
                        //if (lblModuleId.Text == "219")
                        //EDT17052019 Commented By NR  
                        //SDT17052019 Change By NR , Desc : Get Module ID from web config
                        if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                        {
                            TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtBatch");
                        }
                        TextBox box6 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("txtZMRP");
                        TextBox box7 = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("txtZTRP");
                        TextBox box8 = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("txtZSPL");
                        TextBox box9 = (TextBox)GridView1.Rows[rowIndex].Cells[9].FindControl("txtUnit");
                        TextBox box10 = (TextBox)GridView1.Rows[rowIndex].Cells[10].FindControl("txtDivision");
                        TextBox box11 = (TextBox)GridView1.Rows[rowIndex].Cells[11].FindControl("txtdEffectivedate");

                        box1.Text = dt.Tables[0].Rows[i]["ID"].ToString();
                        box2.Text = dt.Tables[0].Rows[i]["Material_Code"].ToString();
                        box3.Text = dt.Tables[0].Rows[i]["Material_Desc"].ToString();
                        box4.Text = dt.Tables[0].Rows[i]["Material_Group"].ToString();
                        //SDT17052019 Commented By NR  
                        //if (lblModuleId.Text == "219")
                        //EDT17052019 Commented By NR  
                        //SDT17052019 Change By NR , Desc : Get Module ID from web config
                        if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                        {
                            TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtBatch");
                            box5.Text = dt.Tables[0].Rows[i]["Batch"].ToString();
                        }
                        box6.Text = dt.Tables[0].Rows[i]["ZMRP"].ToString();
                        box7.Text = dt.Tables[0].Rows[i]["ZTRP"].ToString();
                        box8.Text = dt.Tables[0].Rows[i]["ZSPL"].ToString();
                        box9.Text = dt.Tables[0].Rows[i]["Unit"].ToString();
                        box10.Text = dt.Tables[0].Rows[i]["Division"].ToString();



                        string sdate = "";
                        try
                        {
                            box11.Text = ObjUtil.GetDDMMYYYY(dt.Tables[0].Rows[i]["dEffectivedate"].ToString());
                            DateTime date = System.DateTime.Now;
                            sdate = date.ToString("dd/MM/yyyy");
                            sdate = date.ToString("dd/mm/yyyy");
                            sdate = sdate.Replace(@"/", "");
                            WritePRMasterLog("CreateSSOLog_" + sdate + ".txt", "displayName" + dt.Tables[0].Rows[i]["dEffectivedate"].ToString());

                        }
                        catch (Exception ex)
                        {
                            WritePRMasterLog("CreateSSOLog_" + sdate + ".txt", "displayName" + dt.Tables[0].Rows[i]["dEffectivedate"].ToString());
                        }
                        rowIndex++;
                    }
                    ViewState["CurrentTable"] = dt;
                }
                else if (dst.Tables[0].Rows.Count < 0)
                {
                    SetInitialRow();
                }
                else
                {
                    ViewState["CurrentTable"] = dst;
                    GridView1.DataSource = dst;
                    GridView1.DataBind();
                }
            }
        }
        else
        {
            SetInitialRow();
        }

    }

    protected void txtMaterial_Code_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        TextBox box2 = row.FindControl("txtMaterial_Code") as TextBox;
        box2.Text = ((TextBox)GridView1.Rows[row.RowIndex].Cells[2].FindControl("txtMaterial_Code")).Text;
        if (box2.Text != "")
        {
            DataSet dt = ObjPriceMasterAccess.GetMaterialData(box2.Text);

            TextBox txtMaterial_Desc = row.FindControl("txtMaterial_Desc") as TextBox;
            TextBox txtMaterial_Group = row.FindControl("txtMaterial_Group") as TextBox;
            TextBox txtUnit = row.FindControl("txtUnit") as TextBox;
            TextBox txtDivision = row.FindControl("txtDivision") as TextBox;
            if (dt.Tables.Count > 0)
            {


                txtMaterial_Desc.Text = dt.Tables[0].Rows[0]["Material_Desc"].ToString();
                txtMaterial_Group.Text = dt.Tables[0].Rows[0]["Material_Group"].ToString();
                txtUnit.Text = "INR";
                txtDivision.Text = dt.Tables[0].Rows[0]["Division"].ToString();
                pnlMsg.Visible = false;
            }
            else
            {
                lblMsg.Text = "Material Code not found in system";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
                txtMaterial_Desc.Text = "";
                txtMaterial_Group.Text = "";
                txtUnit.Text = "";
                txtDivision.Text = "";
            }
        }
    }

    protected void btnSaveRow_Click(object sender, EventArgs e)
    {


        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                PriceMasterCreate objDasDetail = new PriceMasterCreate();
                Utility ObjUtil = new Utility();
                objDasDetail.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);

                GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                Literal txtID = row.FindControl("txtID") as Literal;
                TextBox txtMaterial_Code = row.FindControl("txtMaterial_Code") as TextBox;
                TextBox txtMaterial_Desc = row.FindControl("txtMaterial_Desc") as TextBox;
                TextBox txtMaterial_Group = row.FindControl("txtMaterial_Group") as TextBox;
                //SDT17052019 Commented By NR  
                //if (lblModuleId.Text == "219")
                //EDT17052019 Commented By NR  
                //SDT17052019 Change By NR , Desc : Get Module ID from web config
                if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                {
                    TextBox txtBatch = row.FindControl("txtBatch") as TextBox;
                }
                TextBox txtZMRP = row.FindControl("txtZMRP") as TextBox;
                TextBox txtZTRP = row.FindControl("txtZTRP") as TextBox;
                TextBox txtZSPL = row.FindControl("txtZSPL") as TextBox;
                TextBox txtUnit = row.FindControl("txtUnit") as TextBox;
                TextBox txtDivision = row.FindControl("txtDivision") as TextBox;
                TextBox txtdEffectivedate = row.FindControl("txtdEffectivedate") as TextBox;
                //if (string.IsNullOrEmpty(GridView1.SelectedRow.Cells[1].Text))
                //if (GridView1.SelectedRow.Cells[1].Text == "&nbsp;")
                //if (Convert.ToInt32(((Literal)GridView1.Rows[row.RowIndex].Cells[1].FindControl("txtID")).Text) != 0)
                //{

                //}
                //else
                //{
                //objDasDetail.ID = Convert.ToInt32(GridView1.Rows[row.RowIndex].Cells[1].Text);
                //if (GridView1.Rows[row.RowIndex].Cells[1].Text == "")
                //{ }
                //else

                //objDasDetail.ID = Convert.ToInt32(((Literal)GridView1.Rows[row.RowIndex].Cells[1].FindControl("txtID")).Text);
                //}

                if (txtID.Text != "" && txtID.Text != "0" && txtID.Text != null)
                {
                    objDasDetail.ID = Convert.ToInt32(txtID.Text);
                }
                else
                {
                    objDasDetail.ID = 0;
                }

                objDasDetail.Material_Code = ((TextBox)GridView1.Rows[row.RowIndex].Cells[2].FindControl("txtMaterial_Code")).Text;
                objDasDetail.Material_Desc = ((TextBox)GridView1.Rows[row.RowIndex].Cells[3].FindControl("txtMaterial_Desc")).Text;
                objDasDetail.Material_Group = ((TextBox)GridView1.Rows[row.RowIndex].Cells[4].FindControl("txtMaterial_Group")).Text;
                //SDT17052019 Commented By NR  
                //if (lblModuleId.Text == "219")
                //EDT17052019 Commented By NR  
                //SDT17052019 Change By NR , Desc : Get Module ID from web config
                if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                {
                    objDasDetail.Batch = ((TextBox)GridView1.Rows[row.RowIndex].Cells[5].FindControl("txtBatch")).Text;
                }
                objDasDetail.ZMRP = ((TextBox)GridView1.Rows[row.RowIndex].Cells[6].FindControl("txtZMRP")).Text;
                objDasDetail.ZTRP = ((TextBox)GridView1.Rows[row.RowIndex].Cells[7].FindControl("txtZTRP")).Text;
                objDasDetail.ZSPL = ((TextBox)GridView1.Rows[row.RowIndex].Cells[8].FindControl("txtZSPL")).Text;
                objDasDetail.Unit = ((TextBox)GridView1.Rows[row.RowIndex].Cells[9].FindControl("txtUnit")).Text;
                objDasDetail.Division = ((TextBox)GridView1.Rows[row.RowIndex].Cells[10].FindControl("txtDivision")).Text;
                objDasDetail.dEffectivedate = ObjUtil.GetYYYYMMDD(((TextBox)GridView1.Rows[row.RowIndex].Cells[11].FindControl("txtdEffectivedate")).Text);
                string[] Division = objDasDetail.Division.Split('-');
                objDasDetail.Division = Division[0].Trim();

                bool flag = true;

                DataSet dst = ObjPriceMasterAccess.FillGridData(lblMasterHeaderId.Text);
                for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                {
                    if (dst.Tables[0].Rows.Count > 1)
                    {
                        if (objDasDetail.Material_Code != dst.Tables[0].Rows[i]["Material_Code"].ToString())
                        {
                            flag = true;

                        }
                        else
                        {
                            //SDT17052019 Commented By NR  
                            //if (lblModuleId.Text == "219")
                            //EDT17052019 Commented By NR  
                            //SDT17052019 Change By NR , Desc : Get Module ID from web config
                            if (lblModuleId.Text == Convert.ToString(ConfigurationManager.AppSettings["ModulePMMBP"]))
                            {
                                string Mat_Batch = objDasDetail.Material_Code + " - " + objDasDetail.Batch;
                                if (Mat_Batch != (dst.Tables[0].Rows[i]["Material_Code"].ToString() + " - " + dst.Tables[0].Rows[i]["Batch"].ToString()))
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            else
                            {
                                flag = false;
                                break;
                            }
                        }
                    }

                }
                if (flag == true)
                {
                    Utility objUtil = new Utility();
                    objDasDetail.UserId = lblUserId.Text;
                    objDasDetail.TodayDate = objUtil.GetDate();
                    objDasDetail.IPAddress = objUtil.GetIpAddress();
                    if (ObjPriceMasterAccess.SavePrice(objDasDetail) > 0)
                    {
                        scope.Complete();
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
                    lblMsg.Text = "Material Code cannot be same";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                PriceMasterCreate objDasDetail = new PriceMasterCreate();
                objDasDetail.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
                Utility objUtil = new Utility();
                objDasDetail.UserId = lblUserId.Text;
                objDasDetail.TodayDate = objUtil.GetDate();
                objDasDetail.IPAddress = objUtil.GetIpAddress();

                if (ObjPriceMasterAccess.SaveDetail(objDasDetail) > 0)
                {
                    scope.Complete();
                    Response.Redirect("MaterialBatchPrice.aspx");
                }
                else
                {
                    lblMsg.Text = Messages.GetMessage(-1);
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }



    public void WritePRMasterLog(string strFileName, string strMessage)
    {
        try
        {
            //Path.GetTempPath()
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\PRMaster", strFileName), FileMode.Append, FileAccess.Write);
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
