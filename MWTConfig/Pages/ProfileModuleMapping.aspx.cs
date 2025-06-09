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
        List<M_Module> ModuleList = MWTEntities.M_Module.ToList<M_Module>();
        if (ModuleList.Count > 0)
        {
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
        }
        gvModule.DataSource = ModuleList;
        gvModule.DataBind();
    }
    protected void gvModule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gr = e.Row;
        if (gr.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnModuleId = gr.FindControl("hdnModuleId") as HiddenField;
            HiddenField hdnMappingId = gr.FindControl("hdnMappingId") as HiddenField;
            CheckBox chkActive = gr.FindControl("chkActive") as CheckBox;
            CheckBox chkView = gr.FindControl("chkView") as CheckBox;
            CheckBox chkAdd = gr.FindControl("chkAdd") as CheckBox;
            CheckBox chkUpdate = gr.FindControl("chkUpdate") as CheckBox;
            CheckBox chkDelete = gr.FindControl("chkDelete") as CheckBox;

            int ModuleId = Convert.ToInt32(hdnModuleId.Value);
            int ProfileId = Convert.ToInt32(ddlprofile.SelectedValue);

            Profile_Module_Mapping ObjProfile_Module_Mapping = (Profile_Module_Mapping)MWTEntities.Profile_Module_Mapping.Where(x => x.Module_Id == ModuleId && x.Profile_Id == ProfileId).FirstOrDefault();

            if (ObjProfile_Module_Mapping != null)
            {
                
                hdnMappingId.Value = ObjProfile_Module_Mapping.Mapping_Id.ToString();
                chkActive.Checked = ObjProfile_Module_Mapping.IsActive;
                chkView.Checked = (Boolean)(ObjProfile_Module_Mapping.View_Right == null ? false : ObjProfile_Module_Mapping.View_Right);
                chkAdd.Checked = (Boolean)(ObjProfile_Module_Mapping.Add_Right == null ? false : ObjProfile_Module_Mapping.Add_Right);
                chkUpdate.Checked = (Boolean)(ObjProfile_Module_Mapping.Update_Right == null ? false : ObjProfile_Module_Mapping.Update_Right);
                chkDelete.Checked = (Boolean)(ObjProfile_Module_Mapping.Delete_Right == null ? false : ObjProfile_Module_Mapping.Delete_Right);
            }

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlprofile.SelectedValue != "0")
            {
                foreach (GridViewRow gr in gvModule.Rows)
                {
                    HiddenField hdnModuleId = gr.FindControl("hdnModuleId") as HiddenField;
                    HiddenField hdnMappingId = gr.FindControl("hdnMappingId") as HiddenField;
                    CheckBox chkActive = gr.FindControl("chkActive") as CheckBox;
                    CheckBox chkView = gr.FindControl("chkView") as CheckBox;
                    CheckBox chkAdd = gr.FindControl("chkAdd") as CheckBox;
                    CheckBox chkUpdate = gr.FindControl("chkUpdate") as CheckBox;
                    CheckBox chkDelete = gr.FindControl("chkDelete") as CheckBox;

                    int ModuleId = Convert.ToInt32(hdnModuleId.Value);
                    int ProfileId = Convert.ToInt32(ddlprofile.SelectedValue);
                    int MappingId = Convert.ToInt32(hdnMappingId.Value);

                    Profile_Module_Mapping ObjProfile_Module_Mapping;
                    if (MappingId > 0)
                    {
                        ObjProfile_Module_Mapping = MWTEntities.Profile_Module_Mapping.Find(MappingId);
                    }
                    else
                    {
                        ObjProfile_Module_Mapping = new Profile_Module_Mapping();
                    }

                    ObjProfile_Module_Mapping.Module_Id = ModuleId;
                    ObjProfile_Module_Mapping.Profile_Id = ProfileId;
                    ObjProfile_Module_Mapping.IsActive = chkActive.Checked;
                    ObjProfile_Module_Mapping.View_Right = chkView.Checked;
                    ObjProfile_Module_Mapping.Add_Right = chkAdd.Checked;
                    ObjProfile_Module_Mapping.Update_Right = chkUpdate.Checked;
                    ObjProfile_Module_Mapping.Delete_Right = chkDelete.Checked;

                    if (!chkActive.Checked && MappingId > 0)
                    {
                        MWTEntities.Profile_Module_Mapping.Remove(ObjProfile_Module_Mapping);
                    }

                    if (MappingId == 0 && chkActive.Checked)
                    {
                        MWTEntities.Profile_Module_Mapping.Add(ObjProfile_Module_Mapping);
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