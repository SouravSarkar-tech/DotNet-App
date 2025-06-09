using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Text;
using System.Data;
using System.Collections;
using log4net;

public partial class Reports_ChangeRequestStatus : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[StaticKeys.LoggedIn_User_Id] != null)
            {
                if (!IsPostBack)
                {
                    lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                    string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                    if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                    {
                        ReadModules();
                        ReadRequests();
                    }
                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ReadRequests();
        }
        catch (Exception ex)
        { _log.Error("btnSearch_Click", ex); }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            lblPk.Text = GetSelectedPkID();
            ViewReport(lblPk.Text);
        }
        catch (Exception ex)
        { _log.Error("btnView_Click", ex); }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            //pnlView.Visible = false;
            //pnlSearch.Visible = true;
            Response.Redirect("ChangeRequestStatus.aspx?pg=18", false);
        }
        catch (Exception ex)
        { _log.Error("btnView_Click", ex); }
    }

    //protected void grdNewRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //    }
    //}

    private void ReadRequests()
    {
        try
        {
            DataAccessLayer objDal = new DataAccessLayer();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT H.Master_Header_Id,H.Module_Id,H.Request_No,B1.Material_Number,B1.Material_Type FROM T_Master_Header H JOIN M_Module M ");
            query.Append(" ON H.Module_Id = M.Module_Id ");
            query.Append(" JOIN T_Mat_Basic_Data1 B1 ");
            query.Append(" ON H.Master_Header_Id = B1.Master_Header_Id ");
            query.Append(" WHERE M.ModuleType= 'M' AND H.Request_No LIKE '%" + txtRequestNo.Text + "%' AND H.Change_Ref_MH_Id is not null");
            // AND H.Request_Status = 'D' 
            if (ddlModuleSearch.SelectedValue != "0")
            {
                query.Append(" AND H.Module_Id = " + ddlModuleSearch.SelectedValue);
            }

            try
            {
                grdSearch.DataSource = objDal.FillDataSet(query.ToString(), "Table");
                grdSearch.DataBind();

            }
            catch (Exception ex)
            {
                //throw ex;
                _log.Error("ReadRequests", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("ReadRequests1", ex); }
    }

    private void ViewReport(string masterHeaderId)
    {
        try
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = new DataSet();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetChangeRequestDetail";
            hashPara.Add("@Master_Header_Id", masterHeaderId);
            try
            {
                grdOldRequest.DataSource = null;
                grdOldRequest.DataBind();
                grdNewRequest.DataSource = null;
                grdNewRequest.DataBind();
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if ((dstData.Tables[0].Rows.Count > 0) && (dstData.Tables[1].Rows.Count > 0))
                {
                    grdOldRequest.DataSource = dstData.Tables[0].DefaultView;
                    grdOldRequest.DataBind();

                    grdNewRequest.DataSource = dstData.Tables[1].DefaultView;
                    grdNewRequest.DataBind();

                    int iCount = grdOldRequest.Columns.Count;
                    string val1 = string.Empty;
                    string val2 = string.Empty;
                    for (int i = 3; i < iCount - 2; i++)
                    {
                        grdOldRequest.Columns[i].Visible = true;
                        grdNewRequest.Columns[i].Visible = true;
                        val1 = grdOldRequest.Rows[0].Cells[i].Text;
                        val2 = grdNewRequest.Rows[0].Cells[i].Text;

                        if (val1.ToUpper() == val2.ToUpper())
                        {
                            //grdOldRequest.Rows[0].Cells[i].Visible = false;
                            //grdNewRequest.Rows[0].Cells[i].Visible = false;

                            grdOldRequest.Columns[i].Visible = false;
                            grdNewRequest.Columns[i].Visible = false;
                        }

                    }

                    pnlView.Visible = true;
                    pnlSearch.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                _log.Error("ViewReport", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("ViewReport1", ex); }
    }


    private void ReadModules()
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            ddlModuleSearch.DataSource = objMatAccess.ReadModules("M");
            ddlModuleSearch.DataTextField = "Module_Name";
            ddlModuleSearch.DataValueField = "Module_Id";
            ddlModuleSearch.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("ReadModules", ex);

        }
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
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            _log.Error("GetSelectedPkID", ex);
        }
        return strPk;
    }
}