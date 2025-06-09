using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using Accenture.MWT.LDAPHelper;
using System.Data;
using System.Drawing.Design;
using System.Data.SqlClient;
using System.Collections;
using log4net;

public partial class Administration_UpdateFBUtility : System.Web.UI.Page
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
            UpdateBOMFB ObjUpdateBOMFB = GetControlsValue();

            int flag;
            flag = userAccess.UpdateBOMRPFB(ObjUpdateBOMFB);
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
            txtRequestNo.Text = ""; 
            txtRemarks.Text = "";
            txtAlternativeBOM.Text = "";
            txtSAP_BOM_No.Text = "";
            txtRecipe_Group.Text = "";
            txtProdVersionNo.Text = "";
            txtRecipeGroupCntr.Text = "";

            ddlRemarks.SelectedIndex = 0;
            pnlMsg.Visible = false; 
        }
        catch (Exception ex)
        { _log.Error("BtnClear_Click", ex); }
    }


    private UpdateBOMFB GetControlsValue()
    {
        UpdateBOMFB ObjUpdateBOMFB = new UpdateBOMFB();
        Utility objUtil = new Utility();

        try
        {
            ObjUpdateBOMFB.Request_No = Convert.ToString(txtRequestNo.Text); 
            ObjUpdateBOMFB.sRemarks = ddlRemarks.SelectedValue;
            ObjUpdateBOMFB.stxtRemarks = txtRemarks.Text.Trim();
            ObjUpdateBOMFB.CreatedBy = lblUserId.Text.Trim();
            ObjUpdateBOMFB.AltBOMCur = Convert.ToString(txtAlternativeBOM.Text.Trim());
            ObjUpdateBOMFB.Recipe_GroupCur = Convert.ToString(txtRecipe_Group.Text.Trim());
            ObjUpdateBOMFB.ProdVersionNoCur = Convert.ToString(txtProdVersionNo.Text.Trim());
            ObjUpdateBOMFB.GroupCntrCur = Convert.ToString(txtRecipeGroupCntr.Text.Trim());
            ObjUpdateBOMFB.SAP_BOM_NoCur = Convert.ToString(txtSAP_BOM_No.Text.Trim()); 

        }
        catch (Exception ex)
        {
            _log.Error("UpdateBOMFB", ex);
        }
        return ObjUpdateBOMFB;
    }

}