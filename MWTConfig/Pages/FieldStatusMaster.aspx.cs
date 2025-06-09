using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
public partial class Pages_FieldStatusMaster : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFieldStatus();
        }
    }
    /// <summary>
    /// Bind the field status
    /// </summary>
    void BindFieldStatus()
    {
        List<FieldStatusMaster> fieldSatatus = MWTEntities.FieldStatusMasters.ToList();
        bool isFirstRowHide = false;
        if (fieldSatatus.Count == 0)
        {
            isFirstRowHide = true;
            fieldSatatus.Add(new FieldStatusMaster { ID = 0, Active = false });
        }
        gvFieldStatus.DataSource = fieldSatatus;
        gvFieldStatus.DataBind();
        btnSubmit.Visible = true;
        if (isFirstRowHide)
            gvFieldStatus.Rows[0].Visible = false;
    }
    /// <summary>
    /// Add a Field Status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as Button).Parent.Parent as GridViewRow;
            if (row != null)
            {

                TextBox txtName = row.FindControl("txtNameF") as TextBox;
                TextBox txtDescription = row.FindControl("txtDescriptionF") as TextBox;
                CheckBox chkActive = row.FindControl("chkActive") as CheckBox;
                FieldStatusMaster fieldStatus = new FieldStatusMaster();
                fieldStatus.Name = txtName.Text;
                fieldStatus.Description = txtDescription.Text;
                fieldStatus.Active = true;
                fieldStatus.IPAddress = IPAddress;

                MWTEntities.FieldStatusMasters.Add(fieldStatus);
                MWTEntities.SaveChanges();
                BindFieldStatus();
            }
        }
        catch (Exception E)
        {

            lblMessage.Text = E.Message;
        }
    }
    /// <summary>
    /// Submit the field status from the grid after editing the field status.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ID;
        foreach (GridViewRow row in gvFieldStatus.Rows)
        {
            TextBox txtName = row.FindControl("txtNameI") as TextBox;
            TextBox txtDescription = row.FindControl("txtDescriptionI") as TextBox;
            CheckBox chkActive = row.FindControl("chkActive") as CheckBox;
            HiddenField hdnID = row.FindControl("hdnID") as HiddenField;

            if (int.TryParse(hdnID.Value, out ID))
            {
                FieldStatusMaster fieldStatus = MWTEntities.FieldStatusMasters.Find(ID);
                if (fieldStatus != null)
                {
                    fieldStatus.Name = txtName.Text;
                    fieldStatus.Description = txtDescription.Text;
                    fieldStatus.Active = chkActive.Checked;
                    fieldStatus.UpdatedOn = DateTime.Now;
                }
            }
        }

        MWTEntities.SaveChanges();
        BindFieldStatus();
        lblMessage.Text = "Submit successfully";
    }
}