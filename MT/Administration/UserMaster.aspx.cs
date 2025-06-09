using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Text;
using Accenture.MWT.LDAPHelper;
public partial class Administration_UserMaster : System.Web.UI.Page
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

                //if (Request.QueryString[StaticKeys.QueryStringMenuId] != null)
                //{
                string menuId = "3";//Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                UserRights(userProfileId, menuId);
                //SearchUser();
                ReadProfile();
                ReadCountry();
                ReadDepartments();
                //}
                //else
                //{
                //    btnView.Visible = false;
                //    btnModify.Visible = false;
                //    btnCreateNew.Visible = false;
                //}
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveUserDetail();
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        lblMode.Text = "";
        pnlAddNew.Visible = true;
        pnlSearch.Visible = false;
        pnlMsg.Visible = false;
        txtUserName.Enabled = true;
        btnSave.Visible = true;
        trPassword1.Visible = true;
        trPassword2.Visible = true;
        btnSave.Text = "Save";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        if (txtUserNameSearch.Text.Trim() != "")
        {
            SearchUser();
        }
        else
        {
            lblMsg.Text = "User Does Not Exist.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    protected void btnSearchDate_Click(object sender, EventArgs e)
    {
        txtUserNameSearch.Text = "";
        SearchUser();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMode.Text = "V";
        lblPk.Text = GetSelectedPkID();
        ReadUserDetailByUserId(lblPk.Text);
        pnlSearch.Visible = false;
        pnlAddNew.Visible = true;
        pnlMsg.Visible = false;
        btnSave.Visible = false;
        trPassword1.Visible = false;
        trPassword2.Visible = false;
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblMode.Text = "M";
        txtUserName.Enabled = false;
        lblPk.Text = GetSelectedPkID();
        ReadUserDetailByUserId(lblPk.Text);
        pnlSearch.Visible = false;
        btnSave.Visible = true;
        btnSave.Text = "Update";
        pnlAddNew.Visible = true;
        pnlMsg.Visible = false;
        trPassword1.Visible = false;
        trPassword2.Visible = false;
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
        SearchUser();
    }

    protected void btnCreateADUser_Click(object sender, EventArgs e)
    {
        ActiveDirectoryHelper ObjADH = new ActiveDirectoryHelper();
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        dstData = userAccess.ValidateUser(txtUserNameSearch.Text, "mwt");

        if (dstData.Tables[0].Rows.Count < 1)
        {
            ADUserDetail ObjADUser = ObjADH.GetUserByLoginName(txtUserNameSearch.Text);
            if (userAccess.SaveUserDetail(ObjADUser, "0") == 1)
            {
                lblMsg.Text = Messages.GetMessage(1);
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }
            else
            {
                lblMsg.Text = "User Does Not Exist.";// Messages.GetMessage(-1);
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "User Already Created.";// Messages.GetMessage(-1);
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        lblMode.Text = "M";
        txtUserName.Enabled = false;
        lblPk.Text = GetSelectedPkID();
        AssignDualRolesByUserName(lblPk.Text);
    }



    #endregion

    #region Private Functions

    private void SearchUser()
    {
        try
        {
            Utility ObjUtil = new Utility();
            UserAccess userAccess = new UserAccess();
            grdSearch.DataSource = userAccess.ReadUser(txtUserNameSearch.Text.Trim(), lblUserId.Text, ObjUtil.GetMMDDYYYY(txtFromDate.Text), ObjUtil.GetMMDDYYYY(txtToDate.Text));
            grdSearch.DataBind();

            if (grdSearch.Rows.Count <= 0)
            {
                lblMsg.Text = "User Does Not Exist.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        catch (Exception ex) { }
    }

    private void SaveUserDetail()
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        SqlTransaction objTrans;
        string tableName = "User_Master";
        string fieldsName = "User_id,UserName,Password,Full_Name,EmailId,Country_Id,Profile_Id,Department_Id,ReportingTo_Name,ReportingTo_Email,IsActive,CreatedBy,CreatedOn,CreatedIp";
        string fieldsValue = string.Empty;
        bool flg = false;
        int userId;
        string whereClause = string.Empty;
        string userName = Utility.RemoveSpecialChar(txtUserName.Text.Trim());
        string userProfileId = ddlProfile.SelectedValue;
        string fullName = Utility.RemoveSpecialChar(txtFullName.Text.Trim());
        string countryId = ddlCountry.SelectedValue;
        string email = Utility.RemoveSpecialChar(txtEmail.Text.Trim());
        string departmentId = ddlDepartment.SelectedValue;
        string reportingToName = Utility.RemoveSpecialChar(txtReportingTOName.Text.Trim());
        string reportingToEmail = Utility.RemoveSpecialChar(txtReprotingToEmail.Text.Trim());
        string password = Encryption.Encrypt(txtPassword.Text.Trim());

        try
        {
            objDal.OpenConnection(this.Page);
            objTrans = objDal.cnnConnection.BeginTransaction();
            if (lblMode.Text != "M")
            {
                userId = objDal.GetPK(tableName, "User_Id", ref objDal.cnnConnection, ref objTrans);
                fieldsValue = userId + ",'" + userName + "','" + password + "','" + fullName + "','" + email + "'," + countryId + "," + userProfileId + "," + departmentId + ",'" + reportingToName + "','" + reportingToEmail + "','True'," + lblUserId.Text + ",'" + objUtil.GetDate() + "','" + objUtil.GetIpAddress() + "'";
                flg = objDal.AddRecord(tableName, fieldsName, fieldsValue, ref objDal.cnnConnection, ref objTrans);
                lblMsg.Text = Messages.GetMessage(1);
            }
            else if (lblMode.Text == "M")
            {
                userId = Convert.ToInt32(lblPk.Text);
                fieldsName = "Full_Name$EmailId$country_Id$Profile_Id$Department_Id$ReportingTo_Name$ReportingTo_Email$IsActive$ModifiedBy$ModifiedOn$ModifiedIp$";
                fieldsValue = "'" + fullName + "'$'" + email + "'$" + countryId + "$" + userProfileId + "$" + departmentId + "$'" + reportingToName + "'$'" + reportingToEmail + "'$'True'$" + lblUserId.Text + "$'" + objUtil.GetDate() + "'$'" + objUtil.GetIpAddress() + "'$";
                whereClause = "User_Id = " + userId;
                flg = objDal.ModifyRecord(tableName, fieldsName, fieldsValue, whereClause, ref objDal.cnnConnection, ref objTrans);
                lblMsg.Text = Messages.GetMessage(2);
            }
            if (flg)
            {
                objTrans.Commit();
                SearchUser();
                pnlAddNew.Visible = false;
                pnlSearch.Visible = true;
                ClearControls();
                pnlMsg.CssClass = "success";
            }
            else
            {
                objTrans.Rollback();
                lblMsg.Text = Messages.GetMessage(0);
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
                btnView.Visible = false;
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

        txtUserName.Text = "";
        txtFullName.Text = "";
        txtEmail.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlProfile.SelectedIndex = 0;
    }

    private void AssignDualRolesByUserName(string userId)
    {
        UserAccess userAccess = new UserAccess();
        if (userAccess.AssignDualRolesByUserName(userId) == 1)
        {
            lblMsg.Text = "Additional role assigned to user successfully.";
            pnlMsg.CssClass = "success";
        }
        else
        {
            lblMsg.Text = "Error while assigning additional role.";
            pnlMsg.CssClass = "error";
        }
        pnlMsg.Visible = true;
    }

    #endregion

    #region Read Functions

    private void ReadUserDetailByUserId(string userId)
    {
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();

        try
        {
            dstData = userAccess.ReadUserDetailByUserId(userId);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                ddlProfile.SelectedValue = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                txtUserName.Text = dstData.Tables[0].Rows[0]["UserName"].ToString();
                txtFullName.Text = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                txtEmail.Text = dstData.Tables[0].Rows[0]["EmailId"].ToString();
                ddlCountry.SelectedValue = dstData.Tables[0].Rows[0]["Country_Id"].ToString();
                ddlDepartment.SelectedValue = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                txtReportingTOName.Text = dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString();
                txtReprotingToEmail.Text = dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString();

                pnlAddNew.Visible = true;
                pnlMsg.Visible = false;
            }
            else
            {
                pnlAddNew.Visible = false;
                lblMsg.Text = Messages.GetMessage(10);
                pnlMsg.Visible = true;
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

    private void ReadProfile()
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            ddlProfile.DataSource = userAccess.ReadProfile();
            ddlProfile.DataTextField = "Profile_Name";
            ddlProfile.DataValueField = "Profile_Id";
            ddlProfile.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadCountry()
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            ddlCountry.DataSource = userAccess.ReadCountry();
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadDepartments()
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            ddlDepartment.DataSource = userAccess.ReadDepartments();
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_Id";
            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// /8400000241
    ///  Add button for Deactive User fun on this event triggred
    ///  
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeactiveUser_Click(object sender, EventArgs e)
    {
        if (txtUserNameSearch.Text.Trim() != "")
        {
            UserAccess userAccess = new UserAccess();
            if (userAccess.UpdateStatuInDB(txtUserNameSearch.Text.Trim()) > 0)
            {
                lblMsg.Text = txtUserNameSearch.Text.Trim() + " Id has been De-Activated successfully";
                pnlMsg.CssClass = "success";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "User Does Not Exist / Already deactivated.";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

    }
    #endregion


}