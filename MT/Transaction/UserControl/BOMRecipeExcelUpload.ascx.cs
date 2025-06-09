using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;
using System.Data;
using System.Data.OleDb;
using Accenture.MWT.DomainObject;

public partial class Transaction_UserControl_BOMRecipeExcelUpload : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SaveImport())
        {
            Response.Redirect("BOMRecipeBlock.aspx");
        }
        else
        {
            pnlMsg.Visible = true;
            pnlMsg.CssClass = "error";
            lblMsg.Text = "Error Occured during Updation";

            ModalPopupExtender.Show();
        }
    }

    protected void grvData_DataBound(object sender, EventArgs e)
    {
        bool flg = true;
        int i = 0;

        foreach (GridViewRow grv in grvData.Rows)
        {
            //GridViewRow grv = (GridViewRow)sender;

            string msg = "";

            Label lblPlant = (Label)grv.FindControl("lblPlant");
            Label lblRecipeGrp = (Label)grv.FindControl("lblRecipeGrp");
            Label lblRStatus = (Label)grv.FindControl("lblRStatus");
            Label lblAltBOM = (Label)grv.FindControl("lblAltBOM");
            Label lblBOMStatus = (Label)grv.FindControl("lblBOMStatus");
            Label lblProdVer = (Label)grv.FindControl("lblProdVer");
            Label lblProdLock = (Label)grv.FindControl("lblProdLock");
            Label lblMsg = (Label)grv.FindControl("lblMsg");

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");

            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");
            TextBox txtRecipeGrp = (TextBox)grv.FindControl("txtRecipeGrp");
            TextBox txtAltBOM = (TextBox)grv.FindControl("txtAltBOM");
            TextBox txtProdVer = (TextBox)grv.FindControl("txtProdVer");

            DropDownList ddlRStatus = (DropDownList)grv.FindControl("ddlRStatus");
            DropDownList ddlBOMStatus = (DropDownList)grv.FindControl("ddlBOMStatus");
            DropDownList ddlLock = (DropDownList)grv.FindControl("ddlLock");

            if (txtMaterialCode.Text == "")
            {
                grv.Visible = false;
            }
            else
            {
                i++;
                HelperAccess helperAccess = new HelperAccess();
                
                if (txtMaterialCode.Text == "")
                {
                    msg = msg + "Material Code Mandatory.";
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txtMaterialCode.Text, "^[\\d]{6,10}$"))
                {
                    msg = msg + "Material Code Invalid.";
                }       
                        
                if (msg == "")
                {
                    helperAccess.PopuplateDropDownList(ddlPlant, "pr_GetPlantList '0','MC','0','" + Session[StaticKeys.MaterialPlantId].ToString() + "'", "Plant_Name", "Plant_Code", "");

                    if (lblPlant.Text != "")
                    {
                        if (ddlPlant.Items.FindByValue(lblPlant.Text) == null)
                        {
                            msg = msg + "Invalid Plant Code.";
                        }
                        else
                        {
                            ddlPlant.SelectedValue = lblPlant.Text;
                        }
                    }
                    else
                    {
                        msg = msg + "Plant Code Mandatory";
                    }

                }

                if (msg == "")
                {
                    if (lblRecipeGrp.Text == "" && lblAltBOM.Text == "" && lblProdVer.Text == "")
                        msg = msg + "Enter atleast one component to be blocked/unblocked.";
                    else if (!(lblRecipeGrp.Text != "" || lblRecipeGrp.Text != "" || lblRecipeGrp.Text != ""))
                        msg = msg + "Enter atleast one component to be blocked/unblocked.";
                }

                if (msg == "")
                {
                    if (lblRecipeGrp.Text != "")
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(lblRecipeGrp.Text, "^[\\d]{8,10}$"))
                        {
                            msg = msg + "Invalid Recipe Group.";
                        }
                        else
                        {
                            string[] RecipeStatus = lblRStatus.Text.Split('-');
                            helperAccess.PopuplateDropDownList(ddlRStatus, "pr_GetDropDownListByControlNameModuleType 'R','ddlRStatus'", "LookUp_Desc", "LookUp_Code", "");

                            if (lblRStatus.Text == "")
                            {
                                msg = msg + "Recipe Status is Mandatory.";
                            }
                            else if (ddlRStatus.Items.FindByValue(RecipeStatus[0].Trim()) == null)
                            {
                                msg = msg + "Invalid Recipe Status.";
                            }
                            else
                            {
                                ddlRStatus.SelectedValue = RecipeStatus[0].Trim();
                            }
                        }                        
                    }                    
                }

                if (msg == "")
                {
                    if (lblAltBOM.Text != "")
                    {//NRDD Start
                     //if (!System.Text.RegularExpressions.Regex.IsMatch(lblAltBOM.Text, "^[\\d]$"))
                     //{
                     //    msg = msg + "Invalid Alternative BOM";
                     //}
                     //else
                     //{
                        string[] BOMStatus = lblBOMStatus.Text.Split('-');

                            if (lblBOMStatus.Text == "")
                            {
                                msg = msg + "BOM Status is Mandatory.";
                            }
                            else if (ddlBOMStatus.Items.FindByValue(BOMStatus[0].Trim()) == null)
                            {
                                msg = msg + "Invalid BOM Status.";
                            }
                            else
                            {
                                ddlBOMStatus.SelectedValue = BOMStatus[0].Trim();
                            }
                        //}//NRDD Start
                    }
                }

                if (msg == "")
                {
                    if (lblProdVer.Text != "")
                    {//NRDD Start
                     //if (!System.Text.RegularExpressions.Regex.IsMatch(lblProdVer.Text, "^[\\d]$"))
                     //{
                     //    msg = msg + "Invalid Prod version";
                     //}
                     //else
                     //{
                        string[] Lock = lblProdLock.Text.Split('-');
                            helperAccess.PopuplateDropDownList(ddlLock, "pr_GetDropDownListByControlNameModuleType 'R','ddlLock'", "LookUp_Desc", "LookUp_Code", "0");

                            if (lblProdLock.Text == "")
                            {
                                msg = msg + "Lock Status is Mandatory.";
                            }
                            else if (ddlLock.Items.FindByValue(Lock[0]) == null)
                            {
                                msg = msg + "Invalid Prod version Lock Status.";
                            }
                            else
                            {
                                ddlLock.SelectedValue = Lock[0];
                            }
                        //}//NRDD Start
                    }

                }

                Panel pnlMsg1 = (Panel)grv.FindControl("pnlMsg");

                if (msg == "")
                {
                    lblMsg.Text = "OK";
                    pnlMsg1.CssClass = "success";
                    pnlMsg1.Visible = true;
                }
                else
                {
                    flg = false;
                    lblMsg.Text = msg;
                    pnlMsg1.CssClass = "error";
                    pnlMsg1.Visible = true;
                }
            }
        }

        if (i > 0)
            btnAdd.Enabled = flg;
    }

    protected void Process_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        DataSet ds = new DataSet();

        string StrPath = "~/Transaction/BOMRecipe/BOMRecipeDocs/" + Session[StaticKeys.RequestNo].ToString() + "/";

        //HttpFileCollection fileCollection = Request.Files;

        if (fileUpload.HasFile)
        {

            HttpPostedFile uploadfile = fileUpload.PostedFile;//fileCollection[0];

            try
            {

                if (uploadfile.ContentLength > 0)
                {
                    //HttpPostedFile ObjHttpPostedFile = UploadBlock.SaveAs();

                    string fileExtension = System.IO.Path.GetExtension(uploadfile.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/tempfile/") + Session[StaticKeys.RequestNo].ToString() + fileExtension;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        fileUpload.SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["TABLE_NAME"].ToString() == "Sheet1$")
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        if (t > 0)
                        {
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                        }
                        else
                        {
                            //
                        }
                    }

                    grvData.DataSource = ds;
                }
                else
                {
                    grvData.DataSource = null;
                }

                grvData.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    pnlMsg.Visible = false;
                    ModalPopupExtender.Show();
                }
                else
                {
                    lblMsg.Text = "Empty Excel. Please provide data to be processed";
                    pnlMsg.CssClass = "error";
                    pnlMsg.Visible = true;
                }
            }
            catch
            {
                lblMsg.Text = "Error Occured \\Invalid Format.Please download and use the Excel format provided above.";
                pnlMsg.CssClass = "error";
                pnlMsg.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to be uploaded";
            pnlMsg.CssClass = "error";
            pnlMsg.Visible = true;
        }

    }
    

    protected bool SaveImport()
    {
        bool flg = true;
        foreach (GridViewRow grv in grvData.Rows)
        {
            Label lblPlant = (Label)grv.FindControl("lblPlant");
            Label lblBlockType = (Label)grv.FindControl("lblBlockType");
            Label lblSalesOrg = (Label)grv.FindControl("lblSalesOrg");
            Label lblDistributionChannel = (Label)grv.FindControl("lblDistributionChannel");
            Label lblMsg = (Label)grv.FindControl("lblMsg");                      

            DropDownList ddlPlant = (DropDownList)grv.FindControl("ddlPlant");

            TextBox txtMaterialCode = (TextBox)grv.FindControl("txtMaterialCode");            
            TextBox txtRemarks = (TextBox)grv.FindControl("txtRemarks");
            TextBox txtRecipeGrp = (TextBox)grv.FindControl("txtRecipeGrp");
            TextBox txtAltBOM = (TextBox)grv.FindControl("txtAltBOM");
            TextBox txtProdVer = (TextBox)grv.FindControl("txtProdVer");

            DropDownList ddlRStatus = (DropDownList)grv.FindControl("ddlRStatus");
            DropDownList ddlBOMStatus = (DropDownList)grv.FindControl("ddlBOMStatus");
            DropDownList ddlLock = (DropDownList)grv.FindControl("ddlLock");


            BOMAccess objBOMRcpBlockAccess = new BOMAccess();
            BOMRecipeBlock ObjBOMRecipeBlock = new BOMRecipeBlock();

            Utility objUtil = new Utility();

            ObjBOMRecipeBlock.BOMRecipe_Block_Id = 0;
            ObjBOMRecipeBlock.Master_Header_Id = Convert.ToInt32(Session[StaticKeys.MasterHeaderId].ToString());
            if (txtMaterialCode.Text != "")
            {

                ObjBOMRecipeBlock.Material_Number = txtMaterialCode.Text;               
                ObjBOMRecipeBlock.Plant_Id = ddlPlant.SelectedValue;
                ObjBOMRecipeBlock.Recipe_Group = txtRecipeGrp.Text; 
                ObjBOMRecipeBlock.Status = ddlRStatus.SelectedValue;
                ObjBOMRecipeBlock.AlternativeBOM = txtAltBOM.Text;
                ObjBOMRecipeBlock.BOMStatus = ddlBOMStatus.SelectedValue;
                ObjBOMRecipeBlock.ProdVersionNo = txtProdVer.Text;
                ObjBOMRecipeBlock.Lock = ddlLock.SelectedValue;

                ObjBOMRecipeBlock.Remarks = txtRemarks.Text;

                ObjBOMRecipeBlock.IsActive = "1";
                ObjBOMRecipeBlock.UserId = Session[StaticKeys.LoggedIn_User_Id].ToString();
                ObjBOMRecipeBlock.TodayDate = objUtil.GetDate();
                ObjBOMRecipeBlock.IPAddress = objUtil.GetIpAddress();


                if (objBOMRcpBlockAccess.SaveMass(ObjBOMRecipeBlock) != 1)
                {
                    flg = false;
                }
            }
        }

        return flg;
    }

}