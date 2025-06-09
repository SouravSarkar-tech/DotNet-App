using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_ProfileMenuMapping : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Profile_Master> ProfileList = MWTEntities.Profile_Master.ToList<Profile_Master>();
            ddlprofile.DataSource = ProfileList;
            ddlprofile.DataTextField = "Profile_Name";
            ddlprofile.DataValueField = "Profile_Id";
            ddlprofile.DataBind();
            ddlprofile.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });
        }
    }
    protected void ddlprofile_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillProfileMenuData();
    }

    private void FillProfileMenuData()
    {
        List<Menu_Master> MenuList = MWTEntities.Menu_Master.ToList<Menu_Master>();
        if (MenuList.Count > 0)
        {
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
        }
        gvMenu.DataSource = MenuList;
        gvMenu.DataBind();
    }
    protected void gvMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gr = e.Row;
        if (gr.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnMenuId = gr.FindControl("hdnMenuId") as HiddenField;
            HiddenField hdnMappingId = gr.FindControl("hdnMappingId") as HiddenField;
            CheckBox chkActive = gr.FindControl("chkActive") as CheckBox;
            CheckBox chkView = gr.FindControl("chkView") as CheckBox;
            CheckBox chkAdd = gr.FindControl("chkAdd") as CheckBox;
            CheckBox chkUpdate = gr.FindControl("chkUpdate") as CheckBox;
            CheckBox chkDelete = gr.FindControl("chkDelete") as CheckBox;
            TextBox txtSequence = gr.FindControl("txtSequence") as TextBox;

            int MenuId = Convert.ToInt32(hdnMenuId.Value);
            int ProfileId = Convert.ToInt32(ddlprofile.SelectedValue);

            Profile_Menu_Mapping objProfile_Menu_Mapping = (Profile_Menu_Mapping)MWTEntities.Profile_Menu_Mapping.Where(x => x.Menu_ID == MenuId && x.Profile_Id == ProfileId).FirstOrDefault();

            if (objProfile_Menu_Mapping != null)
            {
                
                hdnMappingId.Value = objProfile_Menu_Mapping.Mapping_Id.ToString();
                chkActive.Checked = objProfile_Menu_Mapping.IsActive;
                txtSequence.Text = objProfile_Menu_Mapping.Menu_Seq.ToString();
                chkView.Checked = (Boolean)(objProfile_Menu_Mapping.View_Right == null ? false : objProfile_Menu_Mapping.View_Right);
                chkAdd.Checked = (Boolean)(objProfile_Menu_Mapping.Add_Right == null ? false : objProfile_Menu_Mapping.Add_Right);
                chkUpdate.Checked = (Boolean)(objProfile_Menu_Mapping.Update_Right == null ? false : objProfile_Menu_Mapping.Update_Right);
                chkDelete.Checked = (Boolean)(objProfile_Menu_Mapping.Delete_Right == null ? false : objProfile_Menu_Mapping.Delete_Right);
            }

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlprofile.SelectedValue != "0")
            {
                foreach (GridViewRow gr in gvMenu.Rows)
                {
                    HiddenField hdnMenuId = gr.FindControl("hdnMenuId") as HiddenField;
                    HiddenField hdnMappingId = gr.FindControl("hdnMappingId") as HiddenField;
                    CheckBox chkActive = gr.FindControl("chkActive") as CheckBox;
                    CheckBox chkView = gr.FindControl("chkView") as CheckBox;
                    CheckBox chkAdd = gr.FindControl("chkAdd") as CheckBox;
                    CheckBox chkUpdate = gr.FindControl("chkUpdate") as CheckBox;
                    CheckBox chkDelete = gr.FindControl("chkDelete") as CheckBox;
                    TextBox txtSequence = gr.FindControl("txtSequence") as TextBox;

                    int MenuId = Convert.ToInt32(hdnMenuId.Value);
                    int ProfileId = Convert.ToInt32(ddlprofile.SelectedValue);
                    int MappingId = Convert.ToInt32(hdnMappingId.Value);

                    Profile_Menu_Mapping ObjProfile_Menu_Mapping;
                    if (MappingId > 0)
                    {
                        ObjProfile_Menu_Mapping = MWTEntities.Profile_Menu_Mapping.Find(MappingId);
                    }
                    else
                    {
                        ObjProfile_Menu_Mapping = new Profile_Menu_Mapping();
                    }

                    ObjProfile_Menu_Mapping.Menu_ID = MenuId;
                    ObjProfile_Menu_Mapping.Profile_Id = ProfileId;
                    ObjProfile_Menu_Mapping.Menu_Seq = txtSequence.Text == "" ? 0 : Convert.ToInt32(txtSequence.Text);
                    ObjProfile_Menu_Mapping.IsActive = chkActive.Checked;
                    ObjProfile_Menu_Mapping.View_Right = chkView.Checked;
                    ObjProfile_Menu_Mapping.Add_Right = chkAdd.Checked;
                    ObjProfile_Menu_Mapping.Update_Right = chkUpdate.Checked;
                    ObjProfile_Menu_Mapping.Delete_Right = chkDelete.Checked;

                    if (!chkActive.Checked && MappingId > 0)
                    {
                        MWTEntities.Profile_Menu_Mapping.Remove(ObjProfile_Menu_Mapping);
                    }

                    if (MappingId == 0 && chkActive.Checked)
                    {
                        MWTEntities.Profile_Menu_Mapping.Add(ObjProfile_Menu_Mapping);
                    }

                }
                MWTEntities.SaveChanges();
                FillProfileMenuData();
                lblMessage.Text = "Submit successfully";
            }
        }
        catch (Exception E)
        {
            lblMessage.Text = E.Message;
        }

    }
}