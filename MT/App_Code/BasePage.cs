using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Accenture.MWT.DataAccess;

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
    }

    protected void Page_Init(object s, EventArgs e)
    {
        if (Session[StaticKeys.MasterHeaderId] != null)
        {
            _MasterHeaderId = Session[StaticKeys.MasterHeaderId].ToString();
            _userID = Convert.ToInt32(Session[StaticKeys.LoggedIn_User_Id].ToString());
            _IPAddress = new Utility().GetIpAddress();
        }
    }

    string _MasterHeaderId;
    int _userID;

    public int UserID
    {
        get { return _userID; }

    }

    string _IPAddress;

    public string IPAddress
    {
        get { return _IPAddress; }

    }


    public string MasterHeaderId
    {
        get { return _MasterHeaderId; }
        set { _MasterHeaderId = value; }
    }

    public string SelectText
    {
        get
        {
            return "--Select--";
        }
    }
    public string SelectValue
    {
        get
        {
            return "0";
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
                {
                    str += item.Value.Trim() + ",";
                }
                //string[] strValue = str.Split(',');
                //for (var i = 0; i < strValue.Length; i++)
                //{
                //    if (item.Count == strValue[i])
                //    {
                //        str += item.Value.Trim() + ",";
                //    }
                //    else
                //    {
                //        str += item.Value.Trim() + ",";
                //    }
                //}

            }
            return str;

        }
        catch
        {

            throw;
        }
    }

    protected string GetSelectedCheckeditem(CheckBoxList chkList)
    {
        try
        {
            string str = null;
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                    //MSC_8300001775
                    str += item.Text.Trim() + ",";
					//str += item.Text + ",";
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
                        if (item.Value == strValue[i].Trim())
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
                    //MSC_8300001775
                    str += item.Text.Trim() + ",";
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