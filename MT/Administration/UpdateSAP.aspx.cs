using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using log4net;

public partial class Administration_UpdateSAP : System.Web.UI.Page
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
                    pnlAddNew.Visible = true;
                }
            }

        }
        catch (Exception ex)
        { _log.Error("Page_Load", ex); }
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UserAccess userAccess = new UserAccess();
        try
        {
            UpdatePassword ObjUpdatePassword = GetControlsValue();

            int flag;
            flag = userAccess.UpdateSAPUserPass(ObjUpdatePassword);
            if (flag == 1)
            {
                ClearData();
                lblMsg.Text = Messages.GetMessage(1);
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
        { _log.Error("btnUpdate_Click", ex); }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    private void ClearData()
    {
        try
        {
            txtSAPUserID.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = ""; 
            pnlMsg.Visible = false;
        }
        catch (Exception ex)
        { _log.Error("BtnClear_Click", ex); }
    }


    private UpdatePassword GetControlsValue()
    {
        UpdatePassword ObjUpdatePassword = new UpdatePassword();
        Utility objUtil = new Utility();

        try
        {
            ObjUpdatePassword.SAPUserID = Convert.ToString(txtSAPUserID.Text).Trim();
            ObjUpdatePassword.SAPPassword = Encryption.Encrypt(Convert.ToString(txtPassword.Text.Trim()));
            ObjUpdatePassword.CreatedBy = lblUserId.Text.Trim();
            //ObjUpdatePassword.SAPConfirmPassword = Encryption.Encrypt(Convert.ToString(txtConfirmPassword.Text.Trim()));

        }
        catch (Exception ex)
        {
            _log.Error("UpdatePassword", ex);
        }
        return ObjUpdatePassword;
    }

}