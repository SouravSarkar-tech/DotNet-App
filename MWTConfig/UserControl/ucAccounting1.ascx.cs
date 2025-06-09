using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using SectionConfiguration;

public partial class UserControl_usAccounting1 : BaseUserControl
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
        //List<SectionFieldMaster> sectionField = MWTEntities.SectionFieldMasters.Where(x => x.SectionID == (int)FieldStatus.enumSection.Accounting1).ToList();

        //FieldStatus.SetFieldStatus(pnlAccounting1, sectionField);
        
        Accounting1 account1 = new Accounting1();
         
        FieldStatus.SetFieldStatus(pnlAccounting1, account1);



    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtMovingAvgPrice.Text = null;
        txtPriceUnit.Text = null;
        txtStandardPrice.Text = null;
        ddlPriceControlIndicator.ClearSelection();
        ddlValuationCategory.ClearSelection();
        ddlValuationClass.ClearSelection();


    }
}