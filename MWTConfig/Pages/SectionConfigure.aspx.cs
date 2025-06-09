using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HTMLEditor;
using DAL;

public partial class Pages_SectionConfigure : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                List<SectionMaster> sectionList = MWTEntities.SectionMasters.ToList<SectionMaster>();
                ddlSection.DataSource = sectionList;
                ddlSection.DataTextField = "Name";
                ddlSection.DataValueField = "ID";
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });
            }
        }
        catch (Exception E)
        {
            lblMessage.Text = E.Message;
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSEction();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
                GridViewRow row = (sender as Button).Parent.Parent as GridViewRow;
                if (row != null)
                {

                    int sectionID;
                    if (int.TryParse(ddlSection.SelectedValue, out sectionID))
                    {
                        TextBox txtFieldName = row.FindControl("txtFieldNameF") as TextBox;
                        TextBox txtFieldDisplayName = row.FindControl("txtFieldDisplayNameF") as TextBox;
                        TextBox txtFieldDescription = row.FindControl("txtFieldDescriptionF") as TextBox;
                        TextBox txtSAPName = row.FindControl("txtSAPNameF") as TextBox;
                        TextBox txtFieldLength = row.FindControl("txtFieldLengthF") as TextBox;
                        //Editor HtmlEFieldHelpTextF = row.FindControl("HtmlEFieldHelpTextF") as Editor;

                        SectionFieldMaster fSection = new SectionFieldMaster();
                        fSection.FieldName = txtFieldName.Text;
                        fSection.FeildDisplayName = txtFieldDisplayName.Text;
                        fSection.FieldDescription = txtFieldDescription.Text;
                        //fSection.Field_Help_Text = HtmlEFieldHelpTextF.Content;
                        fSection.SAP_Feild = txtSAPName.Text;
                        fSection.Feild_Length = txtFieldLength.Text == "" ? 0 : Convert.ToDecimal(txtFieldLength.Text);
                        fSection.FieldStatus = 3;
                        fSection.Active = true;
                        fSection.SectionID = sectionID;
                        fSection.IPAddress = IPAddress;

                        MWTEntities.SectionFieldMasters.Add(fSection);
                        MWTEntities.SaveChanges();

                        BindSEction();
                        lblMessage.Text = "New Field Added successfully";
                    }
                }
            }
        }
        catch (Exception E)
        {
            lblMessage.Text = E.Message;
        }
    }

    void BindSEction()
    {

        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
                bool isHideFirstRow = false;
                int sectionID;
                if (int.TryParse(ddlSection.SelectedValue, out sectionID))
                {
                    List<SectionFieldMaster> financeSec = MWTEntities.SectionFieldMasters.Where(x => x.SectionID == sectionID).ToList();
                    if (financeSec.Count == 0)
                    {
                        financeSec.Add(new SectionFieldMaster { ID = 0, Active = false });
                        isHideFirstRow = true;
                    }
                    gvSection.DataSource = financeSec;
                    gvSection.DataBind();
                    btnSubmit.Visible = true;

                    if (isHideFirstRow)
                    {
                        gvSection.Rows[0].Visible = false;
                    }

                }

            }

        }
        catch (Exception E)
        {
            lblMessage.Text = E.Message;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
                int sectionID;
                if (int.TryParse(ddlSection.SelectedValue, out sectionID))
                {
                    foreach (GridViewRow row in gvSection.Rows)
                    {
                        TextBox txtFieldName = row.FindControl("txtFieldNameI") as TextBox;
                        TextBox txtFieldDisplayName = row.FindControl("txtFieldDisplayNameI") as TextBox;
                        TextBox txtFieldDescription = row.FindControl("txtFieldDescriptionI") as TextBox;
                        // Editor HtmlEFieldHelpTextI = row.FindControl("HtmlEFieldHelpTextI") as Editor;
                        TextBox txtSAPName = row.FindControl("txtSAPNameI") as TextBox;
                        TextBox txtFieldLength = row.FindControl("txtFieldLengthI") as TextBox;
                        CheckBox chkActive = row.FindControl("chkActive") as CheckBox;
                        HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                        int ID;
                        if (int.TryParse(hdnID.Value, out ID))
                        {

                            SectionFieldMaster fSection = MWTEntities.SectionFieldMasters.Find(ID);
                            if (fSection != null)
                            {
                                fSection.FieldName = txtFieldName.Text;
                                fSection.FeildDisplayName = txtFieldDisplayName.Text;
                                fSection.FieldDescription = txtFieldDescription.Text;
                                fSection.SAP_Feild = txtSAPName.Text;
                                fSection.Feild_Length = txtFieldLength.Text == "" ? 0 : Convert.ToDecimal(txtFieldLength.Text);
                                fSection.FieldStatus = 3;
                                fSection.Active = chkActive.Checked;
                                fSection.IPAddress = IPAddress;
                            }
                        }
                    }
                }
            }
            MWTEntities.SaveChanges();
            BindSEction();
            lblMessage.Text = "Submit successfully";
        }
        catch (Exception E)
        {
            lblMessage.Text = E.Message;
        }


    }
    
}