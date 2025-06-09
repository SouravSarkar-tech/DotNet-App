using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using SectionConfiguration;
public partial class Pages_TestPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConfigureSection();


        }

    }

    private void ConfigureSection()
    {
        if (Request.QueryString["secid"] != null)
        {
            string secID = Request.QueryString["secid"].ToString();

            FieldStatus.enumSection eSection;
            if (Enum.TryParse(secID, out eSection))
            {
                switch (eSection)
                {
                    case FieldStatus.enumSection.Accounting1:
                        ucAccounting11.Visible = true;
                        break;
                    case FieldStatus.enumSection.Accounting2:
                        ucAccounting21.Visible = true;
                        break;

                }
            }

        }
    }




}