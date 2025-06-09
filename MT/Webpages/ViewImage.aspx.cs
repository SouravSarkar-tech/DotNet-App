using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class Masters_ViewImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["fileurl"] != null)
        {
            string fileType = Path.GetExtension(Request.QueryString["file"].ToString());
            if (fileType == ".pdf")
            {
                string path = Server.MapPath(Request.QueryString["fileurl"].ToString());
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);

                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }
            }
            else
            {
                imageControl1.ImageUrl = Request.QueryString["fileurl"].ToString();
            }
        }
    }
}