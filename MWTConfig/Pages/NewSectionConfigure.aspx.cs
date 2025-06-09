using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_NewSectionConfigure : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                List<M_Module> ModuleList = MWTEntities.M_Module.ToList<M_Module>();
                ddlModule.DataSource = ModuleList;
                ddlModule.DataTextField = "Module_Name";
                ddlModule.DataValueField = "Module_Id";
                ddlModule.DataBind();
                ddlModule.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });

                List<M_Company> CompanyList = MWTEntities.M_Company.ToList<M_Company>();
                ddlCompany.DataSource = MWTEntities.pr_GetCompanyList(null, null, null);
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });

                //List<M_Company> CompanyList = MWTEntities.M_Company.ToList<M_Company>();
                //ddlCompany.DataSource = CompanyList;
                //ddlCompany.DataTextField = "Name";
                //ddlCompany.DataValueField = "Company_Id";
                //ddlCompany.DataBind();
                //ddlCompany.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });

                List<M_Plant_Group> PlantGroupList = MWTEntities.M_Plant_Group.ToList<M_Plant_Group>();
                ddlPlantGrp.DataSource = PlantGroupList;
                ddlPlantGrp.DataTextField = "Plant_group_Name";
                ddlPlantGrp.DataValueField = "Plant_Group_Id";
                ddlPlantGrp.DataBind();
                ddlPlantGrp.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });

                ddlSection.DataSource = null;
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });


            }
        }
        catch (Exception E)
        {
            lblMessage.Text = E.Message;
        }
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(ddlModule.SelectedValue);

        var ModuleType = from x in MWTEntities.M_Module
                         where x.Module_Id == i
                         select x.ModuleType;
        if (ModuleType != null)
        {
            string MVal = ModuleType.FirstOrDefault();


            if (MVal == "M")
            {
                ddlPlantGrp.Visible = true;
                ddlCompany.Visible = false;
                ddlCompany.SelectedValue = "0";
            }
            else
            {
                ddlCompany.Visible = true;
                ddlPlantGrp.Visible = false;
                ddlPlantGrp.SelectedValue = "0";
            }

            List<SectionMaster> sectionList = MWTEntities.SectionMasters.Where(x => x.Module_Type == MVal).ToList<SectionMaster>();
            ddlSection.DataSource = sectionList;
            ddlSection.DataTextField = "Name";
            ddlSection.DataValueField = "ID";
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });
        }
    }

    protected void ddlPlantGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        BindSEction();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        BindSEction();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        BindSEction();
    }

    protected void gvSection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        List<FieldStatusMaster> FieldSatarus = MWTEntities.FieldStatusMasters.Where(x => x.Active == true).ToList();
        if (row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlFielsStatusMasterI = row.FindControl("ddlFieldStatusI") as DropDownList;
            TextBox txtApprovalDept = row.FindControl("txtApprovalDept") as TextBox;
            

            ddlFielsStatusMasterI.DataSource = FieldSatarus;
            ddlFielsStatusMasterI.DataTextField = "Name";
            ddlFielsStatusMasterI.DataValueField = "ID";
            ddlFielsStatusMasterI.DataBind();

            int ddlCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            int ddlModuleId = Convert.ToInt32(ddlModule.SelectedValue);
            int ddlPlantGrpId = Convert.ToInt32(ddlPlantGrp.SelectedValue);
            SectionFieldMaster fSection = row.DataItem as SectionFieldMaster;
            List<C_Section_Feild_Mapping> fFeildMappingList = MWTEntities.C_Section_Feild_Mapping.Where(x => x.Section_Feild_Master_Id == fSection.ID && x.Module_Id == ddlModuleId && ((x.Plant_Group_Id == null ? 0 : x.Plant_Group_Id) == ddlPlantGrpId) && ((x.Company_Id == null ? 0 : x.Company_Id) == ddlCompanyId)).ToList();
            //List<C_Section_Feild_Mapping> fFeildMappingList = MWTEntities.C_Section_Feild_Mapping.Where(x => x.Section_Feild_Master_Id == fSection.ID && x.Module_Id == ddlModuleId).ToList();

            C_Section_Feild_Mapping fFeildMapping = fFeildMappingList.FirstOrDefault();
            if (fFeildMapping != null)
            {
                ddlFielsStatusMasterI.SelectedValue = fFeildMapping.Feild_Status.ToString();
                txtApprovalDept.Text = fFeildMapping.Approval_Dept;
                HiddenField hdnMappingId = row.FindControl("hdnMappingId") as HiddenField;
                hdnMappingId.Value = fFeildMapping.Section_Feild_Mapping_Id.ToString();
            }

        }
        if (row.RowType == DataControlRowType.Footer)
        {


            DropDownList ddlFielsStatusMasterF = row.FindControl("ddlFieldStatusF") as DropDownList;
            ddlFielsStatusMasterF.DataSource = FieldSatarus;
            ddlFielsStatusMasterF.DataTextField = "Name";
            ddlFielsStatusMasterF.DataValueField = "ID";
            ddlFielsStatusMasterF.DataBind();
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
                        TextBox txtFieldDescription = row.FindControl("txtFieldDescriptionI") as TextBox;
                        DropDownList ddlFieldStatus = row.FindControl("ddlFieldStatusI") as DropDownList;
                        CheckBox chkActive = row.FindControl("chkActive") as CheckBox;
                        HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                        TextBox txtApprovalDept = row.FindControl("txtApprovalDept") as TextBox;
                        HiddenField hdnMappingId = row.FindControl("hdnMappingId") as HiddenField;
                        int ID;
                        int.TryParse(hdnMappingId.Value, out ID);
                        C_Section_Feild_Mapping fSectionFeildMapping;
                        if (ID > 0)
                        {
                            fSectionFeildMapping = MWTEntities.C_Section_Feild_Mapping.Find(ID);
                        }
                        else
                        {
                            fSectionFeildMapping = new C_Section_Feild_Mapping();
                        }

                        if (fSectionFeildMapping != null)
                        {
                            if (!chkActive.Checked && hdnID.Value != "0" && ID > 0)
                            {
                                MWTEntities.C_Section_Feild_Mapping.Remove(fSectionFeildMapping);
                            }
                            else
                            {
                                fSectionFeildMapping.Module_Id = Convert.ToInt32(ddlModule.SelectedValue);
                                if (ddlPlantGrp.SelectedValue != "0")
                                {
                                    fSectionFeildMapping.Plant_Group_Id = Convert.ToInt32(ddlPlantGrp.SelectedValue);
                                }
                                if (ddlCompany.SelectedValue != "0")
                                {
                                    fSectionFeildMapping.Company_Id = Convert.ToInt32(ddlCompany.SelectedValue);
                                }
                                fSectionFeildMapping.Section_Feild_Master_Id = Convert.ToInt32(hdnID.Value);
                                fSectionFeildMapping.Feild_Status = Convert.ToInt32(ddlFieldStatus.SelectedValue);
                                fSectionFeildMapping.IsActive = chkActive.Checked;
                                fSectionFeildMapping.Approval_Dept = txtApprovalDept.Text;
                                fSectionFeildMapping.CreatedIp = IPAddress;
                            }
                        }
                        if (ID < 1)
                        {
                            MWTEntities.C_Section_Feild_Mapping.Add(fSectionFeildMapping);
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