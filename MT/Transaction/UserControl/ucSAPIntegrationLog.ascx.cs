using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;

public partial class Transaction_UserControl_ucSAPIntegrationLog : System.Web.UI.UserControl
{
    MasterAccess ObjMasterAccess = new MasterAccess();

    private string mMasterHeaderId;
    private string mMassRequestProcessId;

    public string MasterHeaderId
    {
        get { return mMasterHeaderId; }
        set { mMasterHeaderId = value; }
    }

    public string MassRequestProcessId
    {
        get { return mMassRequestProcessId; }
        set { mMassRequestProcessId = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[StaticKeys.LoggedIn_User_Id] != null)
        {
            FillSAPHistoryGrid();
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
    }

    public void FillSAPHistoryGrid()
    {
        Utility ObjUtility = new Utility();

        if (mMasterHeaderId == null)
            mMasterHeaderId = "0";

        if (mMassRequestProcessId == null)
            mMassRequestProcessId = "0";

        DataSet ds;

        if (SafeTypeHandling.ConvertStringToInt32(mMassRequestProcessId) > 0)
        {
            MaterialMasterAccess objMatAccess = new MaterialMasterAccess();
            //ds = objMatAccess.GetMaterialListByMassMaterialProcessID(mMassRequestProcessId, Session[StaticKeys.LoggedIn_User_Id].ToString());
            ds = objMatAccess.GetListByMassMaterialProcessID(mMassRequestProcessId, Session[StaticKeys.LoggedIn_User_Id].ToString());

            rptSAPIntegration.DataSource = ds.Tables[0];
        }
        else
        {
            ds = ObjMasterAccess.GetSAPIntegrationDataByMasterHeaderID(mMasterHeaderId);
            rptSAPIntegration.DataSource = ds.Tables[1];
        }

        rptSAPIntegration.DataBind();
        FillIntegrationData();


    }


    public void FillIntegrationData()
    {
        
        //for (int i = 0; i < rptSAPIntegration.Items.Count; i++)
        foreach(RepeaterItem rpti in rptSAPIntegration.Items)
        {
            Label lblMasterHeaderId = (Label)rpti.FindControl("lblMasterHeaderId");
            GridView grdSearch = (GridView)rpti.FindControl("grdSearch");

            grdSearch.DataSource = ObjMasterAccess.GetSAPIntegrationDataByMasterHeaderID(lblMasterHeaderId.Text);
            grdSearch.DataBind();

        }
    }
}