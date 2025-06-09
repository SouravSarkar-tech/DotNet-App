using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.Data;
using System.Configuration;
using Accenture.MWT.DataAccess;

public partial class Webpages_DocDownload : System.Web.UI.Page
{
    DocumentUploadAccess ObjDocumentUploadAccess = new DocumentUploadAccess();
    Utility ObjUtil = new Utility();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Document();
    }

    private void Document()
    {
        DataSet ds = ObjDocumentUploadAccess.GetDocumentDownloadListByDate(ddlModuleType.SelectedValue, ObjUtil.GetYYYYMMDD(txtDateFrom.Text));
        ZipFile zip = new ZipFile();
        var filesToInclude = new System.Collections.Generic.List<String>();

        string FolderName = "", FileName = "";
        string FileDir = ConfigurationManager.AppSettings["DllDir"].ToString();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            FileName = dr["Document_Path"].ToString().Replace("~", "");
            if (dr["FolderName"].ToString() != FolderName)
            {
                if (filesToInclude.Count > 0)
                    zip.AddFiles(filesToInclude, FolderName);

                FolderName = dr["FolderName"].ToString();
                zip.AddDirectoryByName(FolderName);
                filesToInclude.Clear();

                //filesToInclude.Add(System.IO.Path.Combine(FileDir, FileName));
                filesToInclude.Add(FileDir + FileName);
            }
            else
            {
                lblMsg.Text += String.Format("adding file: {0}<br/>\n", dr["Document_Name"].ToString());
                filesToInclude.Add(FileDir + FileName);

                string archiveName = String.Format("archive-{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "inline; filename=\"" + archiveName + "\"");
            }
        }
        zip.AddFiles(filesToInclude, FolderName);
        zip.Save(Response.OutputStream);
        //Response.Close();
    }
}