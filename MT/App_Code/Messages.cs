using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Messages
{
    /// <summary>
    /// return message string based on provided message number
    /// </summary>
    /// <param name="msgNumber">Message Number</param>
    /// <returns>message string</returns>
    /// <author>Daya Shankar</author>
    /// <createdon>9 May 2013</createdon>
    public static string GetMessage(int msgNumber)
    {
        string errorMessage = string.Empty;
        try
        {
            errorMessage = HttpContext.GetGlobalResourceObject("Messages", msgNumber.ToString()).ToString();

            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = HttpContext.GetGlobalResourceObject("Messages", "DefaultErrorMsg").ToString();
            }
        }
        catch (Exception)
        {
            errorMessage = HttpContext.GetGlobalResourceObject("Messages", "DefaultErrorMsg").ToString();
        }
        return errorMessage;
    }

}