using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Drawing.Design;

public partial class Administration_UPReportingManager : System.Web.UI.Page
{
    HelperAccess helperAccess = new HelperAccess();
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
                pnlAddNew.Visible = true;
                string menuId = "47";//Request.QueryString[StaticKeys.QueryStringMenuId].ToString();
                ReadProfile();
                ReadCountry();
                ReadDepartments();
                PopuplateDropDownList();
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
        string ManagerName = txtReportingTOName.Text;
        UpdateManagerDetails(ManagerName);

    }

    private void UpdateManagerDetails(string ManagerName)
    {
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        string flag;

        //string ManagerEmail = txtReprotingToEmail.Text;


        dstData = userAccess.ReadUserDetailByUserName(txtUserName.Text.Trim());
        if (dstData.Tables[0].Rows.Count != 0) 
        {
            var UserId = dstData.Tables[0].Rows[0]["User_Id"].ToString();
            if (UserId != "")
            {

                dstData = userAccess.readManagerDetail(txtReportingTOName.Text.Trim());
                if (dstData.Tables[0].Rows.Count != 0) 
                {
                    var ManagerId = dstData.Tables[0].Rows[0]["User_Id"].ToString();
                    if (ManagerId != "")
                    {
                        String[] module = GetSelectedCheckedValue(ddlModule);

                        string strMD = null;
                        string[] strMD1 = null;
                        for (int i = 0; i < module.Length; i++)
                        {
                            strMD1 = module[i].ToString().Split(new char[] { '-' });
                            strMD = strMD1[0];
                            flag = userAccess.UpdateReportingMan(UserId, ManagerName, strMD, "");
                            if (flag == "1")
                            {
                                lblMsg.Text = "Manager Not Exist.Please create User";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                //lblMsg.Font = new Font("Arial", 24, FontStyle.Bold);
                                pnlMsg1.Visible = true;
                            }
                            else if (flag == "3")
                            {
                                lblMsg.Text = "Manager Not Exist.Please create User";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                pnlMsg1.Visible = true;
                            }
                            else
                            {
                                lblMsg.Text = txtUserName.Text + " Updated New Reporting Manager " + ManagerName;
                                lblMsg.ForeColor = System.Drawing.Color.Green;
                                pnlMsg1.Visible = true;
                                //ClearControls();
                                txtUserName.Text = "";
                                txtFullName.Text = "";
                                txtEmail.Text = "";
                                txtReportingTOName.Text = "";
                                //txtReprotingToEmail.Text ="";
                                ddlCountry.SelectedIndex = 0;
                                ddlProfile.SelectedIndex = 0;
                                ddlDepartment.SelectedIndex = 0;
                                ddlModule.ClearSelection();
                                lableRddlModule.Text = "";
                                txtUserName.Enabled = true;
                                //break;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Manager Not Exist";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        pnlMsg1.Visible = true;
                    }
                   
                }
                else
                {
                    lblMsg.Text = "Manager Not Exist";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    pnlMsg1.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "User Not Exist.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                pnlMsg1.Visible = true;
            }

        }

        

        
    }
            
       
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    ClearControls();
    
    //}
    private void ClearControls()
    {

        txtUserName.Text = "";
        txtFullName.Text = "";
        txtEmail.Text = "";
        txtReportingTOName.Text ="";
        //txtReprotingToEmail.Text ="";
        ddlCountry.SelectedIndex = 0;
        ddlProfile.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlModule.ClearSelection();
        lableRddlModule.Text = "";
        lblMsg.Text = "";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        //lblPk.Text = GetSelectedPkID();
        ReadUserDetailByUserId(txtUserName.Text.Trim());
        btnSave.Visible = true;
       
        //pnlAddNew.Visible = true;
        //trPassword1.Visible = false;
        //trPassword2.Visible = false;
    }

    private void ReadUserDetailByUserId(string userName)
    {
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();

        try
        {
            dstData = userAccess.ReadUserDetailByUserName(userName);
            if (dstData.Tables[0].Rows.Count > 0)
            {
                lblMode.Text = "M";
                txtUserName.Enabled = false;
                btnSave.Text = "Update";
                pnlMsg1.Visible = false;
                ddlProfile.SelectedValue = dstData.Tables[0].Rows[0]["Profile_Id"].ToString();
                txtUserName.Text = dstData.Tables[0].Rows[0]["UserName"].ToString();
                txtFullName.Text = dstData.Tables[0].Rows[0]["Full_Name"].ToString();
                txtEmail.Text = dstData.Tables[0].Rows[0]["EmailId"].ToString();
                ddlCountry.SelectedValue = dstData.Tables[0].Rows[0]["Country_Id"].ToString();
                ddlDepartment.SelectedValue = dstData.Tables[0].Rows[0]["Department_Id"].ToString();
                txtReportingTOName.Text = dstData.Tables[0].Rows[0]["ReportingTo_Name"].ToString();
                //txtReprotingToEmail.Text = dstData.Tables[0].Rows[0]["ReportingTo_Email"].ToString();
                //pnlAddNew.Visible = true;
            }
            else
            {
               // pnlAddNew.Visible = false;
                lblMsg.Text = "User Not Exist.Please create User";//Messages.GetMessage(10);
                lblMsg.ForeColor = System.Drawing.Color.Red;
                pnlMsg1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayModule();
    }

    private void DisplayModule()
    {
        
        string[] InstallLoc = GetSelectedCheckedValue(ddlModule);
        
        if (InstallLoc.Length > 0)
        {
            string str14=null;
            for (int i = 0; i < InstallLoc.Length ; i++)
            {
                 str14 += InstallLoc[i].ToString()+",";
                
            }
            lableRddlModule.Text = "Selected Modules :  "+str14 ;
        }
        else
        {
            lableRddlModule.Text = "";
        }
    }

    protected string[] GetSelectedCheckedValue(CheckBoxList chkList)
    {
        try
        {
            string str = null;
            
            List<string> str1 = new List<string>();
            string[] str3; 

            //string[] str4; 
            //string str2 = null;
            int i=0;
            //string[] str1 = new string[i];
           
            foreach (ListItem item in chkList.Items)
            {
                
                //string[] Val = null;
                if (item.Selected)
                {
                    
                    string str2 = null;
                    str += item.Text + ",";
                    //Val = str.Split(new char[] { '-' });
                    //str1 += Val[0] + ",";
                    str2 = item.Text ; 
                    str1.Add(str2); 
                    i++;
                }
                
            }
            str3 = new string[str1.Count];
            //str3 = str1[str1.Count];
            str3 = str1.ToArray();

            //str4 = str1[0].;

            return str3;
           

        }
        catch
        {

            throw;
        }
    }
    protected void lnkRefreshddlModule_Click(object sender, EventArgs e)
    {
        DisplayModule();
    }

    private void PopuplateDropDownList()
    {
        helperAccess.PopuplateDropDownCheckBox(ddlModule, "pr_GetDropDownListModule", "LookUp_Desc", "LookUp_Code");
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
        txtUserName.Enabled = true;
    }
   
}