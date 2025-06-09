using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ticket = "AjExMDAgABhwb3J0YWw6cHJhc2FkY2hlbmR3YW5rYXKIABNiYXNpY2F1dGhlbnRpY2F0aW9uAQAAAgADMDAwAwADRVBEBAAMMjAxMzEwMjEwOTQ4BQAEAAAACAoAAP8BTzCCAUsGCSqGSIb3DQEHAqCCATwwggE4AgEBMQswCQYFKw4DAhoFADALBgkqhkiG9w0BBwExggEXMIIBEwIBATBoMF8xCzAJBgNVBAYTAklOMRQwEgYDVQQIEwtNYWhhcmFzaHRyYTEPMA0GA1UEBxMGTXVtYmFpMQ4wDAYDVQQKEwVMdXBpbjELMAkGA1UECxMCSE8xDDAKBgNVBAMTA0VQRAIFAM!ngr0wCQYFKw4DAhoFAKBdMBgGCSqGSIb3DQEJAzELBgkqhkiG9w0BBwEwHAYJKoZIhvcNAQkFMQ8XDTEzMTAyMTA5NDgyOVowIwYJKoZIhvcNAQkEMRYEFNMEZJ50lSpKHxnhewg2K6A%2F9Ad5MAkGByqGSM44BAMELzAtAhUAi%2FVvu!u9SvxSOFXFcnJUWML8rYgCFCFoQI1lHInZlU!w9h2UXEM6eVjB";

        if (Request.Cookies["MYSAPSSO2"] != null)
        {
            ticket = HttpUtility.UrlDecode(Request.Cookies["MYSAPSSO2"].Value);
            //ticket = Request.Cookies["MYSAPSSO2"].Value;
        }

        txtticket.Text = ticket;
    }
}