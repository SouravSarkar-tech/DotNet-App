using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Administration_ApproverMaster : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            if (!IsPostBack)
            {
                lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
                ReadDepartments();
                ReadApprovers();
            }
        }
    }

    protected void ddlModuleTypen_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReadModules(ddlModuleTypen.SelectedValue.ToString());
    }

    protected void ddlModuleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReadModulesF(ddlModuleType.SelectedValue.ToString());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchApprovers();
    }

    protected void grdApproverSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproverSearch.PageIndex = e.NewPageIndex;
        SearchApprovers();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMode.Text = "V";
        lblPk.Text = GetSelectedPkID();
        ReadApproverDetailByAppId(lblPk.Text);
        pnlAppSearch.Visible = false;
        pnlAddNew.Visible = true;
        pnlMsg.Visible = false;
        btnSave.Visible = false;
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        lblMode.Text = "M";
        //txtUserName.Enabled = false;
        lblPk.Text = GetSelectedPkID();
        ReadApproverDetailByAppId(lblPk.Text);
        pnlAppSearch.Visible = false;
        btnSave.Visible = true;
        btnSave.Text = "Update";
        pnlAddNew.Visible = true;
        pnlMsg.Visible = false;

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAddNew.Visible = false;
        pnlAppSearch.Visible = true;
        ClearControls();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            String[] module = GetSelectedCheckedValue(ddlModule);

            string strMD = null;
            string[] strMD1 = null;
            for (int i = 0; i < module.Length; i++)
            {
                strMD1 = module[i].ToString().Split(new char[] { '-' });
                strMD = strMD1[0];
                SaveApproverDetail(strMD);
            }

            ClearControls();
        }
        catch(Exception ex) { }
        //SaveApproverDetail();
    }

    protected string[] GetSelectedCheckedValuelbl(CheckBoxList chkList)
    {
        try
        {
            string str = null;

            List<string> str1 = new List<string>();
            string[] str3;
            int i = 0;
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                {
                    string str2 = null;
                    str += item.Text + ",";
                    str2 = item.Text;
                    str1.Add(str2);
                    i++;
                }

            }
            str3 = new string[str1.Count];
            str3 = str1.ToArray();
            return str3;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected string[] GetSelectedCheckedValue(CheckBoxList chkList)
    {
        try
        {
            string str = null;

            List<string> str1 = new List<string>();
            string[] str3; 
            int i = 0; 
            foreach (ListItem item in chkList.Items)
            { 
                if (item.Selected)
                { 
                    string str2 = null;
                    //str += item.Text + ","; 
                    //str2 = item.Text;
                    str += item.Value + ",";
                    str2 = item.Value;
                    str1.Add(str2);
                    i++;
                }

            }
            str3 = new string[str1.Count]; 
            str3 = str1.ToArray(); 
            return str3; 

        }
        catch(Exception ex) 
        {
             throw;
        }
    }
    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        lblMode.Text = "";
        //ReadApprovers(ddlApprover.SelectedValue.ToString());
        pnlAddNew.Visible = true;
        pnlAppSearch.Visible = false;
        pnlMsg.Visible = false;
        //txtUserName.Enabled = true;
        btnSave.Visible = true;
        //trPassword1.Visible = true;
        //trPassword2.Visible = true;
        btnSave.Text = "Save";
    }

    protected void ddlWorkflowType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkflowType.SelectedValue.ToString() != "")
        {
            if (ddlWorkflowType.SelectedValue.ToString() == "P" || ddlWorkflowType.SelectedValue.ToString() == "PH" || ddlWorkflowType.SelectedValue.ToString() == "R")
            {
                ReadReference(ddlWorkflowType.SelectedValue.ToString());
            }
            else if (ddlWorkflowType.SelectedValue.ToString() == "M")
            {
                ReadReference(ddlWorkflowType.SelectedValue.ToString());
            }
            else
            {
                lblMsg.Text = "Workflow type can be either P,PH or R. Select appropriate workflow type.";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            ddlReferenceID.Items.Clear();
            ddlReferenceID.Items.Insert(0, new ListItem("---Select---", ""));
        }

    }

    protected void ddlModuleSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ReadDepartments(ddlModuleSearch.SelectedValue.ToString());
        ReadDepartments();
    }

    #endregion

    #region Methods

    private void ReadDepartments()
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = userAccess.ReadDepartments();
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_Id";
            ddlDepartment.DataBind();

            ddlDeptName.Items.Clear();
            ddlDeptName.DataSource = userAccess.ReadDepartments();
            ddlDeptName.DataTextField = "Department_Name";
            ddlDeptName.DataValueField = "Department_Id";
            ddlDeptName.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadDepartments(string moduleId)
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = userAccess.ReadDepartments(moduleId);
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataValueField = "Department_Id";
            ddlDepartment.DataBind();

            ddlDeptName.Items.Clear();
            ddlDeptName.DataSource = userAccess.ReadDepartments(moduleId);
            ddlDeptName.DataTextField = "Department_Name";
            ddlDeptName.DataValueField = "Department_Id";
            ddlDeptName.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //private void ReadModules(string moduleType)
    //{
    //    MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
    //    try
    //    {
    //        ddlModuleSearch.Items.Clear();
    //        ddlModule.Items.Clear();
    //        if (moduleType != "")
    //        {                
    //            switch (moduleType)
    //            {
    //                case "M":
    //                    ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("M");
    //                    ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("M");
    //                    break;
    //                case "C":
    //                    ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("C");
    //                    ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("C");
    //                    break;
    //                case "V":
    //                    ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("V");
    //                    ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("V");
    //                    break;
    //                case "B":
    //                    ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("B");
    //                    ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("B");
    //                    break;
    //                default:
    //                    ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType(moduleType);
    //                    ddlModule.DataSource = objMatAccess.ReadModulesByModuleType(moduleType);
    //                    break;
    //            }

    //            ddlModuleSearch.DataTextField = "Module_Name";
    //            ddlModuleSearch.DataValueField = "Module_Id";
    //            ddlModuleSearch.DataBind();

    //            ddlModule.DataTextField = "Module_Name";
    //            ddlModule.DataValueField = "Module_Id";
    //            ddlModule.DataBind();
    //        }
    //        //ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", "0"));
    //        //ddlModule.Items.Insert(0, new ListItem("---Select---", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    private void ReadModulesF(string moduleType)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            ddlModuleSearch.Items.Clear(); 
            if (moduleType != "")
            {
                switch (moduleType)
                {
                    case "M":
                        ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("M");
                        //ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("M");
                        break;
                    case "C":
                        ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("C");
                        //ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("C");
                        break;
                    case "V":
                        ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("V");
                        // ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("V");
                        break;
                    case "B":
                        ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("B");
                        // ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("B");
                        break;
                    default:
                        ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType(moduleType);
                        //ddlModule.DataSource = objMatAccess.ReadModulesByModuleType(moduleType);
                        break;
                }

                ddlModuleSearch.DataTextField = "Module_Name";
                ddlModuleSearch.DataValueField = "Module_Id";
                ddlModuleSearch.DataBind();

                //ddlModule.DataTextField = "Module_Name";
                //ddlModule.DataValueField = "Module_Id";
                //ddlModule.DataBind();
            }
            //ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", "0"));
            //ddlModule.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void ReadModules(string moduleType)
    {
        MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
        try
        {
            //ddlModuleSearch.Items.Clear();
            ddlModule.Items.Clear();
            if (moduleType != "")
            {
                switch (moduleType)
                {
                    case "M":
                        //ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("M");
                        ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("M");
                        break;
                    case "C":
                        //ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("C");
                        ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("C");
                        break;
                    case "V":
                        //ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("V");
                        ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("V");
                        break;
                    case "B":
                        //ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType("B");
                        ddlModule.DataSource = objMatAccess.ReadModulesByModuleType("B");
                        break;
                    default:
                        //ddlModuleSearch.DataSource = objMatAccess.ReadModulesByModuleType(moduleType);
                        ddlModule.DataSource = objMatAccess.ReadModulesByModuleType(moduleType);
                        break;
                }

                // ddlModuleSearch.DataTextField = "Module_Name";
                //ddlModuleSearch.DataValueField = "Module_Id";
                // ddlModuleSearch.DataBind();

                ddlModule.DataTextField = "Module_Name";
                ddlModule.DataValueField = "Module_Id";
                ddlModule.DataBind();
            }
            //ddlModuleSearch.Items.Insert(0, new ListItem("---Select---", "0"));
            //ddlModule.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void SearchApprovers()
    {
        UserAccess userAccess = new UserAccess();
        grdApproverSearch.DataSource = userAccess.ReadApprovers(ddlModuleSearch.SelectedValue, ddlDepartment.SelectedValue);
        grdApproverSearch.DataBind();
    }

    private string GetSelectedPkID()
    {
        string strPk = string.Empty;
        try
        {
            foreach (GridViewRow grv in grdApproverSearch.Rows)
            {
                RadioButton rdo = grv.FindControl("rdoSelection") as RadioButton;
                if (rdo.Checked == true)
                {
                    Label lblAuthID = grv.FindControl("lblAuthID") as Label;
                    strPk = lblAuthID.Text;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strPk;
    }

    private void ReadApproverDetailByAppId(string appID)
    {
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();

        try
        {
            dstData = userAccess.ReadApproverDetailByAppId(appID);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                if (dstData.Tables[0].Rows[0]["WorkFlow_Type"].ToString() != "")
                {
                    string workflowType = dstData.Tables[0].Rows[0]["WorkFlow_Type"].ToString();
                    ReadReference(workflowType);
                    ddlReferenceID.SelectedValue = dstData.Tables[0].Rows[0]["Reference_Id"].ToString();
                }
                ddlModule.SelectedValue = dstData.Tables[0].Rows[0]["Module_Id"].ToString();
                txtWorkflowCode.Text = dstData.Tables[0].Rows[0]["WorkFlow_Code"].ToString();
                ddlWorkflowType.SelectedValue = dstData.Tables[0].Rows[0]["WorkFlow_Type"].ToString();
                ddlDeptName.SelectedValue = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                
                ReadApprovers(dstData.Tables[0].Rows[0]["Department_Id"].ToString());

                //ddlApprover.SelectedValue = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                txtApprover.Text = dstData.Tables[0].Rows[0]["UserName"].ToString();
                txtPriority.Text = dstData.Tables[0].Rows[0]["Priority"].ToString();
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

    private void ReadReference(string workflowType)
    {
        UserAccess userAccess = new UserAccess();
        ddlReferenceID.Items.Clear();
        try
        {
            ddlReferenceID.DataSource = userAccess.ReadReferenceCodes(workflowType);
            ddlReferenceID.DataTextField = "LookUp_Desc";
            ddlReferenceID.DataValueField = "LookUp_Code";
            ddlReferenceID.DataBind();
            ddlReferenceID.Items.Insert(0, new ListItem("---Select---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void ReadReferenceUser(string workflowType)
    {
        UserAccess userAccess = new UserAccess();
        ddlReferenceID.Items.Clear();
        try
        {
            //ddlReferenceID.DataSource = userAccess.ReadReferenceCodes(workflowType);
            //ddlReferenceID.DataTextField = "LookUp_Desc";
            //ddlReferenceID.DataValueField = "LookUp_Code";
            ddlReferenceID.DataSource = userAccess.ReadApproverNames();
            ddlReferenceID.DataTextField = "Full_Name";
            ddlReferenceID.DataValueField = "User_Id";
            ddlReferenceID.DataBind();
            ddlReferenceID.Items.Insert(0, new ListItem("---Select---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadApprovers()
    {
        //UserAccess userAccess = new UserAccess();
        //ddlApprover.Items.Clear();
        //try
        //{
        //    ddlApprover.DataSource = userAccess.ReadApproverNames();
        //    ddlApprover.DataTextField = "Full_Name";
        //    ddlApprover.DataValueField = "User_Id";
        //    ddlApprover.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    private void ReadApprovers(string deptId)
    {
        //UserAccess userAccess = new UserAccess();
        //ddlApprover.Items.Clear();
        //try
        //{
        //    ddlApprover.DataSource = userAccess.ReadApproverNames(deptId);
        //    ddlApprover.DataTextField = "Full_Name";
        //    ddlApprover.DataValueField = "User_Id";
        //    ddlApprover.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    private void ClearControls()
    {
        txtWorkflowCode.Text = "";
        ddlWorkflowType.SelectedIndex = 0;
        //ddlModule.SelectedIndex = 0;
        ddlModule.ClearSelection();
        ddlDeptName.SelectedIndex = 0;
        ddlReferenceID.SelectedIndex = 0;
        //ddlApprover.SelectedIndex = 0;
        txtApprover.Text = "";
        txtPriority.Text = "";
        lableRddlModule.Text = "";
        pnlMsg.Visible = false;
    }
    
    //private void SaveApproverDetail()
    //{
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    Utility objUtil = new Utility();
    //    SqlTransaction objTrans;
    //    string tableName = "M_Approving_Authority";
    //    string fieldsName = "Module_Id,WorkFlow_Code,WorkFlow_Type,Department_Id,Reference_Id,User_Id,Priority,IsActive,Active_From,Active_To,CreatedBy,CreatedIp";
    //    string fieldsValue = string.Empty;
    //    bool flg = false;
    //    int authId;
    //    string whereClause = string.Empty;
    //    string ModuleId = ddlModule.SelectedValue;
    //    string workflowCode = Utility.RemoveSpecialChar(txtWorkflowCode.Text.Trim());
    //    //if (Utility.RemoveSpecialChar(txtWorkflowCode.Text.Trim()) == "") { workflowCode = DBNull.Value };
    //    string workflowType = Utility.RemoveSpecialChar(ddlWorkflowType.SelectedValue.ToString().Trim());
    //    string referenceID = ddlReferenceID.SelectedValue;
    //    string user_Id = ddlApprover.SelectedValue;
    //    string departmentId = ddlDeptName.SelectedValue;
    //    string priority = Utility.RemoveSpecialChar(txtPriority.Text.Trim());
        
    //    try
    //    {
    //        objDal.OpenConnection(this.Page);
    //        objTrans = objDal.cnnConnection.BeginTransaction();
    //        if (lblMode.Text != "M")
    //        {
    //            //authId = objDal.GetPK(tableName, "Authority_Id", ref objDal.cnnConnection, ref objTrans);
    //            fieldsValue = ModuleId + ",'" + workflowCode + "','" + workflowType + "'," + departmentId + ",'" + referenceID + "'," + user_Id + "," + priority + "," + "'True','2014-10-01','2042-02-15'," + lblUserId.Text + ",'" + objUtil.GetIpAddress() + "'";
    //            flg = objDal.AddRecord(tableName, fieldsName, fieldsValue, ref objDal.cnnConnection, ref objTrans);
    //            lblMsg.Text = Messages.GetMessage(1);
    //        }
    //        else if (lblMode.Text == "M")
    //        {
    //            authId = Convert.ToInt32(lblPk.Text);
    //            //fieldsName = "Full_Name$EmailId$country_Id$Profile_Id$Department_Id$ReportingTo_Name$ReportingTo_Email$IsActive$ModifiedBy$ModifiedOn$ModifiedIp$";
    //            fieldsName = "Module_Id$WorkFlow_Code$WorkFlow_Type$Department_Id$Reference_Id$User_Id$Priority$IsActive$CreatedBy$CreatedIp$";
    //            fieldsValue = "" + ModuleId + "$'" + workflowCode + "'$'" + workflowType + "'$" + departmentId + "$" + referenceID + "$" + user_Id + "$" + priority + "$'True'$" + lblUserId.Text + "$'" + objUtil.GetIpAddress() + "'$";
    //            whereClause = "Authority_Id = " + authId;
    //            flg = objDal.ModifyRecord(tableName, fieldsName, fieldsValue, whereClause, ref objDal.cnnConnection, ref objTrans);
    //            lblMsg.Text = Messages.GetMessage(2);
    //        }
    //        if (flg)
    //        {
    //            objTrans.Commit();
    //            SearchApprovers();
    //            pnlAddNew.Visible = false;
    //            pnlAppSearch.Visible = true;
    //            ClearControls();
    //            pnlMsg.CssClass = "success";
    //        }
    //        else
    //        {
    //            objTrans.Rollback();
    //            lblMsg.Text = Messages.GetMessage(0);
    //            pnlMsg.CssClass = "error";
    //        }
    //        pnlMsg.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objDal.CloseConnection(objDal.cnnConnection);
    //        objDal = null;
    //        objTrans = null;
    //    }
    //}

    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeptName.SelectedValue != "")
            ReadApprovers(ddlDeptName.SelectedValue.ToString());
    }

    private void SaveApproverDetail(string strMD)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        Hashtable hashPara = new Hashtable();
        //string procName = "pr_Ins_Update_Approver_Detail";
        string procName = "pr_Ins_Update_Approver";
        int result = 0;

        string ModuleId = strMD;// ddlModule.SelectedValue;
        string workflowCode = Utility.RemoveSpecialChar(txtWorkflowCode.Text.Trim());
        string workflowType = Utility.RemoveSpecialChar(ddlWorkflowType.SelectedValue.ToString().Trim());
        string referenceID = ddlReferenceID.SelectedValue;
        //string user_Id = ddlApprover.SelectedValue;
        string user_Id = txtApprover.Text;
        string departmentId = ddlDeptName.SelectedValue;
        string priority = Utility.RemoveSpecialChar(txtPriority.Text.Trim());

        hashPara.Add("@Module_Id", ModuleId);

        if (workflowCode == "")
            hashPara.Add("@WorkFlow_Code", DBNull.Value);
        else
            hashPara.Add("@WorkFlow_Code", workflowCode);

        if (workflowType == "")
            hashPara.Add("@WorkFlow_Type", DBNull.Value);
        else
            hashPara.Add("@WorkFlow_Type", workflowType);

        hashPara.Add("@Department_Id", departmentId);
        if (referenceID == "")
            hashPara.Add("@Reference_Id", DBNull.Value);
        else
            hashPara.Add("@Reference_Id", referenceID);

        hashPara.Add("@User_Id", user_Id);
        hashPara.Add("@Priority", priority);
        if(lblMode.Text != "M")
            hashPara.Add("@Authority_Id", 0);
        else
            hashPara.Add("@Authority_Id", Convert.ToInt32(lblPk.Text));
        hashPara.Add("@IsActive", 1);
        hashPara.Add("@CreatedBy", lblUserId.Text);
        hashPara.Add("@CreatedIp", objUtil.GetIpAddress());

        try
        {
            objDal.OpenConnection();
            result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);

            if (result > 0)
            {
                //SearchApprovers();
                pnlAddNew.Visible = false;
                pnlAppSearch.Visible = true;
                //ClearControls();
                 lblMsg.Text = "Data Updated..";
                 pnlMsg.CssClass = "success";
            }
            else if (result == -2)
            {
                lblMsg.Text = "User does not exist in the system. Kindly contact admin to create User Profile";
                pnlMsg.CssClass = "error";
            }
            else
            {
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
        }

    }



    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayModule();
    }

    private void DisplayModule()
    {

        string[] InstallLoc = GetSelectedCheckedValuelbl(ddlModule);

        if (InstallLoc.Length > 0)
        {
            string str14 = null;
            for (int i = 0; i < InstallLoc.Length; i++)
            {
                str14 += InstallLoc[i].ToString() + ",";

            }
            lableRddlModule.Text = "Selected Modules :  " + str14;
        }
        else
        {
            lableRddlModule.Text = "";
        }
    }


    #endregion




}