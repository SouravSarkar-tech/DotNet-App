using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;

public partial class Administration_ProfileMaster : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                string userProfileId = Session[StaticKeys.LoggedIn_User_Profile_Id].ToString();

                //Srinidhi <Start>
                //if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                //{
                //    string menuId = Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                //    UserRights(userProfileId, menuId);
                //    SearchProfile();
                //    ReadMenus();
                //}
                //else
                //{
                //    btnView.Visible = false;
                //    btnModify.Visible = false;
                //    btnCreateNew.Visible = false;
                //}

                //if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                //{
                    //string menuId = Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                string menuId = "4";
                    UserRights(userProfileId, menuId);
                    SearchProfile();
                    ReadMenus();
                //}
                //else
                //{
                //    btnView.Visible = false;
                //    btnModify.Visible = false;
                //    btnCreateNew.Visible = false;
                //}

                //Srinidhi <end>
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveProfile();
    }
    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        lblMode.Text = "";
        pnlAddNew.Visible = true;
        pnlSearch.Visible = false;
        pnlMsg.Visible = false;
        txtUserProfileName.Enabled = true;
        btnSave.Visible = true;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        SearchProfile();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMode.Text = "V";
        lblPk.Text = GetSelectedPkID();
        GetProfileDetail(lblPk.Text);
        ReadMenuByProfileId(lblPk.Text);
        pnlSearch.Visible = false;
        pnlAddNew.Visible = true;
        pnlMsg.Visible = false;
        btnSave.Visible = false;
        txtUserProfileName.Enabled = false;

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblMode.Text = "M";
        txtUserProfileName.Enabled = true;
        lblPk.Text = GetSelectedPkID();
        GetProfileDetail(lblPk.Text);
        ReadMenuByProfileId(lblPk.Text);
        pnlSearch.Visible = false;
        pnlAddNew.Visible = true;
        pnlMsg.Visible = false;
        btnSave.Visible = true;
        txtUserProfileName.Enabled = false;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAddNew.Visible = false;
        pnlSearch.Visible = true;
        ClearControls();
    }
    protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearch.PageIndex = e.NewPageIndex;
        SearchProfile();
    }
    protected void grdMenus_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdParentId = e.Row.FindControl("hdParentId") as HiddenField;
            HiddenField hdUrl = e.Row.FindControl("hdUrl") as HiddenField;
            CheckBox chkView = e.Row.FindControl("chkView") as CheckBox;
            CheckBox chkAdd = e.Row.FindControl("chkAdd") as CheckBox;
            CheckBox chkModify = e.Row.FindControl("chkModify") as CheckBox;
            CheckBox chkDelete = e.Row.FindControl("chkDelete") as CheckBox;

            if (string.IsNullOrEmpty(hdParentId.Value) && string.IsNullOrEmpty(hdUrl.Value))
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4B0082");
                e.Row.ForeColor = System.Drawing.Color.White;
                chkView.Visible = false;
                chkAdd.Visible = false;
                chkModify.Visible = false;
                chkDelete.Visible = false;
            }
            if (!string.IsNullOrEmpty(hdParentId.Value) && string.IsNullOrEmpty(hdUrl.Value))
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#6A5ACD");
                e.Row.ForeColor = System.Drawing.Color.White;
                chkView.Visible = false;
                chkAdd.Visible = false;
                chkModify.Visible = false;
                chkDelete.Visible = false;
            }
        }
    }

    #endregion

    #region Private Functions

    private void SearchProfile()
    {
        DataAccessLayer objDal = new DataAccessLayer();
        DataSet dstData = new DataSet();
        StringBuilder queryBuilder = new StringBuilder();
        queryBuilder.Append(" SELECT Profile_Id,Profile_Name FROM Profile_Master ");
        queryBuilder.Append(" WHERE Profile_Name LIKE '" + Utility.RemoveSpecialChar(txtUserProfileSearch.Text.Trim()) + "%'");

        try
        {
            objDal.OpenConnection(this.Page);
            dstData = objDal.FillDataSet(queryBuilder.ToString(), "Profile_Name", ref objDal.cnnConnection);
            grdSearch.DataSource = dstData.Tables[0].DefaultView;
            grdSearch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal.CloseConnection(objDal.cnnConnection);
            objDal = null;
        }
    }

    private void SaveProfile()
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        SqlTransaction objTrans;
        string tableName = "Profile_Master";
        string fieldsName = "Profile_Id,Profile_Name,IsActive,CreatedBy,CreatedOn";
        string fieldsValue = string.Empty;

        bool flg = false;
        int profileId;
        string whereClause = string.Empty;

        string userProfileName = txtUserProfileName.Text.Trim();

        try
        {
            objDal.OpenConnection(this.Page);
            objTrans = objDal.cnnConnection.BeginTransaction();
            if (lblMode.Text != "M")
            {
                profileId = objDal.GetPK(tableName, "Profile_Id", ref objDal.cnnConnection, ref objTrans);
                fieldsValue = profileId + ",'" + Utility.RemoveSpecialChar(userProfileName) + "','True'," + lblUserId.Text + ",'" + objUtil.GetDate() + "'";
                if (objDal.AddRecord(tableName, fieldsName, fieldsValue, ref objDal.cnnConnection, ref objTrans))
                {
                    flg = SaveProfileMenu(profileId.ToString(), ref objDal.cnnConnection, ref objTrans);
                    lblMsg.Text = Messages.GetMessage(1);
                }
            }
            else if (lblMode.Text == "M")
            {
                profileId = Convert.ToInt32(lblPk.Text);
                fieldsName = "Profile_Name$IsActive$ModifiedBy$ModifiedOn$";
                fieldsValue = "'" + Utility.RemoveSpecialChar(userProfileName) + "'$'True'$" + lblUserId.Text + "$'" + objUtil.GetDate() + "'$";
                whereClause = "Profile_Id = " + profileId;
                if (objDal.ModifyRecord(tableName, fieldsName, fieldsValue, whereClause, ref objDal.cnnConnection, ref objTrans))
                {
                    flg = SaveProfileMenu(profileId.ToString(), ref objDal.cnnConnection, ref objTrans);
                    lblMsg.Text = Messages.GetMessage(1);
                }
            }

            if (flg)
            {
                objTrans.Commit();
                SearchProfile();
                pnlAddNew.Visible = false;
                pnlSearch.Visible = true;
                ClearControls();
                pnlMsg.CssClass = "success";
            }
            else
            {
                objTrans.Rollback();
                lblMsg.Text = Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
            }
            pnlMsg.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal.CloseConnection(objDal.cnnConnection);
            objDal = null;
            objTrans = null;
        }
    }

    private bool SaveProfileMenu(string profileId, ref SqlConnection cnnCon, ref SqlTransaction objTrans)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();

        string tableName = "Profile_Menu_Mapping";
        string fieldsName = "Menu_ID,Profile_Id,View_Right,Add_Right,Update_Right,Delete_Right,CreatedBy,CreatedOn,CreatedIP";
        string fieldsValue = string.Empty;
        string viewRight = string.Empty;
        string addRight = string.Empty;
        string modifyRight = string.Empty;
        string deleteRight = string.Empty;
        string loggedInUserId = lblUserId.Text;
        bool flg = false;

        string todayDate = objUtil.GetDate();
        string IpAddress = objUtil.GetIpAddress();
        try
        {
            if (DeleteProfileMenu(profileId, ref cnnCon, ref objTrans))
            {
                foreach (GridViewRow row in grdMenus.Rows)
                {
                    CheckBox chkView = row.FindControl("chkView") as CheckBox;
                    CheckBox chkAdd = row.FindControl("chkAdd") as CheckBox;
                    CheckBox chkModify = row.FindControl("chkModify") as CheckBox;
                    CheckBox chkDelete = row.FindControl("chkDelete") as CheckBox;
                    HiddenField hdMenuId = row.FindControl("hdMenuId") as HiddenField;

                    if ((chkView.Checked) || (chkAdd.Checked) || (chkModify.Checked) || (chkDelete.Checked))
                    {
                        viewRight = chkView.Checked ? "True" : "False";
                        addRight = chkAdd.Checked ? "True" : "False";
                        modifyRight = chkModify.Checked ? "True" : "False";
                        deleteRight = chkDelete.Checked ? "True" : "False";
                        fieldsValue = hdMenuId.Value + "," + profileId + ",'" + viewRight + "','" + addRight + "','" + modifyRight + "','" + deleteRight + "'," + loggedInUserId + ",'" + DateTime.Now + "','" + objUtil.GetIpAddress() + "'";

                        flg = objDal.AddRecord(tableName, fieldsName, fieldsValue, ref cnnCon, ref objTrans);
                        if (!flg)
                        {
                            flg = false;
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return flg;
    }

    public bool DeleteProfileMenu(string profileId, ref SqlConnection cnnDelete, ref SqlTransaction objTrans)
    {

        SqlCommand cmdDelete = default(SqlCommand);
        string query = string.Empty;
        bool flg = false;
        query = "DELETE FROM Profile_Menu_Mapping WHERE Profile_Id=" + profileId;
        cmdDelete = new SqlCommand(query, cnnDelete);
        cmdDelete.Transaction = objTrans;

        try
        {
            cmdDelete.ExecuteNonQuery();
            flg = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmdDelete.Dispose();
        }
        return flg;

    }

    private void GetProfileDetail(string profileId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        DataSet dstData = new DataSet();
        string query = "SELECT Profile_Id,Profile_Name FROM Profile_Master WHERE Profile_Id = " + profileId;

        try
        {
            objDal.OpenConnection(this.Page);
            dstData = objDal.FillDataSet(query.ToString(), "User_master", ref objDal.cnnConnection);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                txtUserProfileName.Text = dstData.Tables[0].Rows[0]["Profile_Name"].ToString();
                ReadMenuByProfileId(profileId);
                pnlAddNew.Visible = true;
                pnlMsg.Visible = false;
            }
            else
            {
                pnlAddNew.Visible = false;
                lblMsg.Text = Messages.GetMessage(10);
                pnlMsg.Visible = true;
                pnlMsg.CssClass = "info";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDal.CloseConnection(objDal.cnnConnection);
            objDal = null;
        }
    }

    private void ReadMenuByProfileId(string profileId)
    {
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        try
        {

            dstData = userAccess.ReadMenuByProfileId(profileId);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dstData.Tables[0].Rows)
                {
                    foreach (GridViewRow gRow in grdMenus.Rows)
                    {
                        HiddenField hdMenuId = gRow.FindControl("hdMenuId") as HiddenField;
                        if (hdMenuId.Value == row[0].ToString())
                        {
                            CheckBox chkView = gRow.FindControl("chkView") as CheckBox;
                            CheckBox chkAdd = gRow.FindControl("chkAdd") as CheckBox;
                            CheckBox chkModify = gRow.FindControl("chkModify") as CheckBox;
                            CheckBox chkDelete = gRow.FindControl("chkDelete") as CheckBox;

                            chkView.Checked = SafeTypeHandling.ConvertStringToBoolean(row["View_Right"].ToString());
                            chkAdd.Checked = SafeTypeHandling.ConvertStringToBoolean(row["Add_Right"].ToString());
                            chkModify.Checked = SafeTypeHandling.ConvertStringToBoolean(row["Update_Right"].ToString());
                            chkDelete.Checked = SafeTypeHandling.ConvertStringToBoolean(row["Delete_Right"].ToString());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;
        }
        return strPk;
    }

    private void ReadMenus()
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            grdMenus.DataSource = userAccess.ReadMenus();
            grdMenus.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UserRights(string profileId, string menuId)
    {
        Authorization auth = new Authorization();
        DataSet dstData = new DataSet();
        try
        {
            dstData = auth.UserRights(profileId, menuId);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                btnView.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["View_Right"].ToString());
                btnModify.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["Update_Right"].ToString());
                btnCreateNew.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["Add_Right"].ToString());
            }
            else
            {
                btnModify.Visible = false;
                btnCreateNew.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ClearControls()
    {
        foreach (GridViewRow row in grdMenus.Rows)
        {
            CheckBox chkView = row.FindControl("chkView") as CheckBox;
            CheckBox chkAdd = row.FindControl("chkAdd") as CheckBox;
            CheckBox chkModify = row.FindControl("chkModify") as CheckBox;
            CheckBox chkDelete = row.FindControl("chkDelete") as CheckBox;
            chkView.Checked = false;
            chkAdd.Checked = false;
            chkModify.Checked = false;
            chkDelete.Checked = false;
        }
        txtUserProfileName.Text = "";
        lblMode.Text = "";
    }
    #endregion
}