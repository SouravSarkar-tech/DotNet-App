using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

public class ToolTip
{
    public string ReadToolTip(string controlId)
    {
        string retVal = "test";
        //try
        //{
        //    XDocument document = XDocument.Load("~/tooltip/tooltip.xml");
        //    var toolTip = (from r in document.Descendants("control").Where
        //                                   (r => (string)r.Attribute("id") == controlId)
        //                   select r.Element("toolTip").Value).FirstOrDefault();
        //    retVal = toolTip.ToString();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        return retVal;
    }
}