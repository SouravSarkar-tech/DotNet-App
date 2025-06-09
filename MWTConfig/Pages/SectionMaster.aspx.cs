using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
public partial class Pages_SectionMaster : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSection();
        }
    }
    /// <summary>
    /// Bind the section
    /// </summary>
    private void BindSection()
    {
        List<SectionMaster> sectionList = MWTEntities.SectionMasters.ToList();
        bool isFirstRowHide = false;

        if (sectionList.Count == 0)
        {
            isFirstRowHide = true;
            sectionList.Add(new SectionMaster { ID = 0 });
        }
        gvSection.DataSource = sectionList;
        gvSection.DataBind();
        btnSubmit.Visible = true;
        if (isFirstRowHide)
        {
            gvSection.Rows[0].Visible = false;
        }

    }
    /// <summary>
    /// Submit all the section from the grid after editing the section.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvSection.Rows)
            {
                if (row != null)
                {
                    TextBox txtName = row.FindControl("txtNameI") as TextBox;
                    TextBox txtDescription = row.FindControl("txtDescriptionI") as TextBox;
                    TextBox txtSequence = row.FindControl("txtSequenceI") as TextBox;
                    CheckBox chkActive = row.FindControl("chkActive") as CheckBox;
                    TextBox txtShortName = row.FindControl("txtShortNameI") as TextBox;
                    HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                    int id;
                    if (int.TryParse(hdnID.Value, out id))
                    {
                        SectionMaster sectionMaster = MWTEntities.SectionMasters.Find(id);
                        if (sectionMaster != null)
                        {
                            sectionMaster.Name = txtName.Text;
                            sectionMaster.Decsription = txtDescription.Text;
                            sectionMaster.ShortName = txtShortName.Text;
                            sectionMaster.Active = chkActive.Checked;
                            if (!string.IsNullOrEmpty(txtSequence.Text))
                                sectionMaster.Sequence = Convert.ToInt32(txtSequence.Text);
                        }
                    }

                }
            }
            MWTEntities.SaveChanges();
            lblMessage.Text = "Submit successfully";
        }
        catch (Exception E)
        {

            lblMessage.Text = E.Message;
        }
    }
    /// <summary>
    /// Add a perticular section 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as Button).Parent.Parent as GridViewRow;
        if (row != null)
        {
            TextBox txtName = row.FindControl("txtNameF") as TextBox;
            TextBox txtShortName = row.FindControl("txtShortNameF") as TextBox;
            TextBox txtDEscription = row.FindControl("txtDEscriptionF") as TextBox;
            TextBox txtSequence = row.FindControl("txtSequenceF") as TextBox;
            SectionMaster secMaster = new SectionMaster();
            secMaster.Name = txtName.Text;
            secMaster.Decsription = txtDEscription.Text;
            secMaster.ShortName = txtShortName.Text;
            secMaster.IPAddress = IPAddress;
            secMaster.Active = true;
            if (!string.IsNullOrEmpty(txtSequence.Text))
                secMaster.Sequence = Convert.ToInt32(txtSequence.Text);
            MWTEntities.SaveChanges();
        }
    }
}