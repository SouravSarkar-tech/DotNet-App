using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_ModuleSectionConfigure : BasePage
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
                ddlCompany.DataSource = MWTEntities.pr_GetCompanyList(null,null,null);
                //ddlCompany.DataSource = CompanyList;
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });

                List<M_Plant_Group> PlantGroupList = MWTEntities.M_Plant_Group.ToList<M_Plant_Group>();
                ddlPlantGrp.DataSource = PlantGroupList;
                ddlPlantGrp.DataTextField = "Plant_group_Name";
                ddlPlantGrp.DataValueField = "Plant_Group_Id";
                ddlPlantGrp.DataBind();
                ddlPlantGrp.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });

                List<M_Department> DepartmentList = MWTEntities.M_Department.ToList<M_Department>();
                ddldepartment.DataSource = DepartmentList;
                ddldepartment.DataTextField = "Department_Name";
                ddldepartment.DataValueField = "Department_Id";
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, new ListItem { Text = "Requester", Value = "0" });
                //ddlPlantGrp.Items.Insert(0, new ListItem { Text = SelectText, Value = "" });

                
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


            if (MVal == "M" || MVal == "N")
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

            BindSEction();

            //List<SectionMaster> sectionList = MWTEntities.SectionMasters.Where(x => x.Module_Type == MVal).ToList<SectionMaster>();
            //ddlSection.DataSource = sectionList;
            //ddlSection.DataTextField = "Name";
            //ddlSection.DataValueField = "ID";
            //ddlSection.DataBind();
            //ddlSection.Items.Insert(0, new ListItem { Text = SelectText, Value = SelectValue });
        }
    }

    protected void ddlPlantGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSEction();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSEction();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSEction();
    }

    protected void gvSection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gr = e.Row;
        if (gr.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnID = gr.FindControl("hdnID") as HiddenField;
            HiddenField hdnMappingId = gr.FindControl("hdnMappingId") as HiddenField;
            CheckBox chkActive = gr.FindControl("chkActive") as CheckBox;
            CheckBox chkView = gr.FindControl("chkView") as CheckBox;
            CheckBox chkInput = gr.FindControl("chkInput") as CheckBox;
            CheckBox chkRequestor = gr.FindControl("chkRequestor") as CheckBox;
            TextBox txtSequence = gr.FindControl("txtSequence") as TextBox;
            TextBox txtApprovalDept = gr.FindControl("txtApprovalDept") as TextBox;

            int ddlCompanyId = Convert.ToInt32( ddlCompany.SelectedValue);
            int ddlModuleId = Convert.ToInt32( ddlModule.SelectedValue);
            int ddlPlantGrpId = Convert.ToInt32(ddlPlantGrp.SelectedValue);
            int ddlDepartmentId = Convert.ToInt32(ddldepartment.SelectedValue);

            int SectionId = Convert.ToInt32(hdnID.Value);

            Dept_Module_Section_Mapping DeptMapping = (Dept_Module_Section_Mapping)MWTEntities.Dept_Module_Section_Mapping.Where(x => x.Section_Id == SectionId && x.Department_Id == ddlDepartmentId && x.Module_Id == ddlModuleId && ((x.Plant_Group_Id == null ? 0 : x.Plant_Group_Id) == ddlPlantGrpId) && ((x.Company_Id == null ? 0 : x.Company_Id) == ddlCompanyId)).FirstOrDefault();

            if (DeptMapping != null)
            {
                hdnMappingId.Value = DeptMapping.Mapping_Id.ToString();
                chkActive.Checked = DeptMapping.IsActive;
                chkView.Checked = DeptMapping.View_Rights;
                chkInput.Checked = DeptMapping.Input_Rights;
                chkRequestor.Checked = DeptMapping.IsRequestor == null ? false : (Boolean)DeptMapping.IsRequestor;
                txtSequence.Text = DeptMapping.Sequence.ToString();
                txtApprovalDept.Text = DeptMapping.Approval_Dept;
            }
            
        }
    }

    void BindSEction()
    {

        try
        {
            int i = Convert.ToInt32(ddlModule.SelectedValue);

            var ModuleType = from x in MWTEntities.M_Module
                             where x.Module_Id == i
                             select x.ModuleType;
            string MVal="";
            if (ModuleType != null)
            {
                MVal = ModuleType.FirstOrDefault();
            
                List<SectionMaster> SectionMasterList = MWTEntities.SectionMasters.Where(x => x.Module_Type == MVal).ToList();

                gvSection.DataSource = SectionMasterList;
                gvSection.DataBind();
                btnSubmit.Visible = true;
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

            foreach (GridViewRow gr in gvSection.Rows)
            {
                HiddenField hdnID = gr.FindControl("hdnID") as HiddenField;
                HiddenField hdnMappingId = gr.FindControl("hdnMappingId") as HiddenField;
                CheckBox chkActive = gr.FindControl("chkActive") as CheckBox;
                CheckBox chkView = gr.FindControl("chkView") as CheckBox;
                CheckBox chkInput = gr.FindControl("chkInput") as CheckBox;
                CheckBox chkRequestor = gr.FindControl("chkRequestor") as CheckBox;
                TextBox txtSequence = gr.FindControl("txtSequence") as TextBox;
                TextBox txtApprovalDept = gr.FindControl("txtApprovalDept") as TextBox;

                int ddlCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                int ddlModuleId = Convert.ToInt32(ddlModule.SelectedValue);
                int ddlPlantGrpId = Convert.ToInt32(ddlPlantGrp.SelectedValue);
                int ddlDepartmentId = Convert.ToInt32(ddldepartment.SelectedValue);

                int MappingId = Convert.ToInt32(hdnMappingId.Value);

                Dept_Module_Section_Mapping objDeptMapping;

                if (MappingId > 0)
                {
                    objDeptMapping = MWTEntities.Dept_Module_Section_Mapping.Find(MappingId);
                }
                else
                {
                    objDeptMapping = new Dept_Module_Section_Mapping();
                }

                if (objDeptMapping != null)
                {
                    if (chkActive.Checked)
                    {
                        objDeptMapping.Module_Id = Convert.ToInt32(ddlModule.SelectedValue);
                        if (ddlPlantGrp.SelectedValue != "0")
                        {
                            objDeptMapping.Plant_Group_Id = Convert.ToInt32(ddlPlantGrp.SelectedValue);
                        }
                        if (ddlCompany.SelectedValue != "0")
                        {
                            objDeptMapping.Company_Id = Convert.ToInt32(ddlCompany.SelectedValue);
                        }

                        objDeptMapping.Section_Id = Convert.ToInt32(hdnID.Value);
                        objDeptMapping.Department_Id = Convert.ToInt32(ddldepartment.SelectedValue);
                        objDeptMapping.Sequence = Convert.ToInt32(txtSequence.Text);
                        objDeptMapping.View_Rights = chkView.Checked;
                        objDeptMapping.Input_Rights = chkInput.Checked;
                        objDeptMapping.IsRequestor = chkRequestor.Checked;
                        objDeptMapping.Approval_Dept = txtApprovalDept.Text;
                        
                        objDeptMapping.IsActive = chkActive.Checked;
                        objDeptMapping.CreatedOn = DateTime.Now;

                        if (MappingId < 1)
                        {
                            MWTEntities.Dept_Module_Section_Mapping.Add(objDeptMapping);
                        }
                    }
                    else if (MappingId > 0)
                    {
                        MWTEntities.Dept_Module_Section_Mapping.Remove(objDeptMapping);
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