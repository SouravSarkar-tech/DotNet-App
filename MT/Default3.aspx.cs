using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accenture.MWT.DataAccess;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.IsPostBack) return;

        MasterAccess ObjMasterAccess = new MasterAccess();

        DataSet dataSet = ObjMasterAccess.GetMWTWorkFlowDataByMasterHeaderID("175");
                

        GridView1.DataSource = dataSet;
        GridView1.DataBind(); 

    }
}