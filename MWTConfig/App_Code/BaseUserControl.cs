using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for BaseUserControl
/// </summary>
public class BaseUserControl : System.Web.UI.UserControl
{
    public MW_TestEntities MWTEntities;
	public BaseUserControl()
	{
		//
		// TODO: Add constructor logic here
		//
        MWTEntities = new MW_TestEntities();
	}
    public string SelectText = "--Select--";
    public string SelectValue = "0";
    public static string IPAddress
    {
        get
        {
            try
            {
                string result = string.Empty;
                string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    string[] ipRange = ip.Split(',');
                    int le = ipRange.Length - 1;
                    result = ipRange[0];
                }
                else
                {
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}