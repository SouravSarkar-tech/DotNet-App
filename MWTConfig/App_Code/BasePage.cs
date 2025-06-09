using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
        MWTEntities = new MW_TestEntities();
    }

    public MW_TestEntities MWTEntities;

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

    protected string GetSelectedCheckedValue(CheckBoxList chkList)
    {
        try
        {
            string str = null;
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                    str += item.Value + ",";
            }
            return str;

        }
        catch
        {

            throw;
        }
    }

    protected void SetSelectedValue(CheckBoxList chkList, string str)
    {
        try
        {
            if (!string.IsNullOrEmpty(str))
            {
                string[] strValue = str.Split(',');
                foreach (ListItem item in chkList.Items)
                {
                    for (var i = 0; i < strValue.Length; i++)
                    {
                        if (item.Value == strValue[i])
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }
        catch
        {

            throw;
        }
    }

    protected void ClearSelectedValue(CheckBoxList chkList)
    {
        try
        {
            foreach (ListItem item in chkList.Items)
            {
                item.Selected = false;
            }
        }
        catch
        {

            throw;
        }
    }

    protected string GetSelectedCheckedText(CheckBoxList chkList)
    {
        try
        {
            string str = "";
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                    str += item.Text + ",";
            }
            if (string.IsNullOrEmpty(str))
                str = SelectText;
            return str;

        }
        catch
        {

            throw;
        }
    }
}