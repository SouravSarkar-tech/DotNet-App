using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using SectionConfiguration;

public partial class ucUserControl_Accounting2 : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConfigureControl();
        }
    }

    private void ConfigureControl()
    {
       // List<SectionFieldMaster> sectionField = MWTEntities.SectionFieldMasters.Where(x => x.SectionID == (int)FieldStatus.enumSection.Accounting2).ToList();

       // FieldStatus.SetFieldStatus(pnlAccounting1, sectionField);
       
        Accounting2 account2 = new Accounting2();

        FieldStatus.SetFieldStatus(pnlAccounting1, account2);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        chkLifoFifo.Checked = false;
        ddlPoolNumberLifo.ClearSelection();
    }
}