using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Collections;
using System.Text;
using log4net;

public partial class Reports_VendorChangeRequestDetail : System.Web.UI.Page
{
    private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                //string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();
                ReadRequests();

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
        pnlSearch.Visible = true;
        pnlView.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("btnBack_Click", ex); }
    }
    #endregion

    #region Private Function

    private void ReadRequests()
    {
        try
        { 
        DataAccessLayer objDal = new DataAccessLayer();
        StringBuilder query = new StringBuilder();

        query.Append(" SELECT H.Master_Header_Id,H.Module_Id,(H.Request_No + (case when isnull(Action_Type,'')='C' then '.' + CONVERT(varchar,Change_Version) else '' end)) AS 'Request_No',B1.Customer_Code,A.Vendor_Acc_Grp_Code	 FROM T_Master_Header H JOIN M_Module M  ");
        query.Append(" ON H.Module_Id = M.Module_Id  ");
        query.Append(" JOIN T_Vendor_General_Type B1  ");
        query.Append(" ON H.Master_Header_Id = B1.Master_Header_Id  ");
        query.Append(" JOIN M_Vendor_Acc_Grp A");
        query.Append(" ON A.Vendor_Acc_Grp_Id = B1.Vendor_Group");
        query.Append(" WHERE M.ModuleType= 'V' AND H.Request_No LIKE '%" + txtRequestNo.Text + "%' AND H.Module_Id = 15 AND H.Master_Header_Id IN (SELECT Change_Ref_MH_Id FROM T_Master_Header WHERE Module_Id = 15 AND Change_Ref_MH_Id IS NOT NULL)");

        try
        {
            grdSearch.DataSource = objDal.FillDataSet(query.ToString(), "Table");
            grdSearch.DataBind();

        }
        catch (Exception ex)
        {
                _log.Error("ReadRequests1", ex);
            }
        }
        catch (Exception ex)
        { _log.Error("ReadRequests", ex); }
    }


    private void ViewReport(string masterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        DataSet dstData = new DataSet();
        Hashtable hashPara = new Hashtable();

        string procName = "Proc_Report_Vendor_Change_Request_Detail";
        bool flg = false;
        int iCount = 0;

        hashPara.Add("@MasterHeaderId", masterHeaderId);

        try
        {
            grdRequest.DataSource = null;
            grdRequest.DataBind();

            dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                iCount = dstData.Tables[0].Columns.Count;
                for (int i = 5; i < iCount; i++)
                {
                    for (int a = 0; a < dstData.Tables[0].Rows.Count - 1; a++)
                    {
                        if (dstData.Tables[0].Rows[0][i].ToString() == dstData.Tables[0].Rows[a + 1][i].ToString())
                        {
                            flg = true;
                        }
                        else
                        {
                            flg = false;
                        }
                    }

                    if (flg)
                    {
                        dstData.Tables[0].Columns.RemoveAt(i);
                        dstData.AcceptChanges();
                        iCount = dstData.Tables[0].Columns.Count;
                        i = i - 1;
                    }
                }

                grdRequest.DataSource = dstData.Tables[0].DefaultView;
                grdRequest.DataBind();
                pnlView.Visible = true;
                pnlSearch.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _log.Error("ViewReport", ex);
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
            _log.Error("ViewReport", ex);
        }
        return strPk;
    }
    #endregion

}