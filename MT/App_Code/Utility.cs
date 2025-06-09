using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace Accenture.MWT.DataAccess
{
    /// <summary>
    /// Created By 		    :  Manish Sharma
    /// Creation Date 		:  Feb-2006
    ///   
    /// Updated By           :  Daya Shankar Sharma
    /// Updated On           :  April 2013
    /// </summary>
    public class Utility
    {
        public string GetDDMMYYYY(string strDDMMYYYY)
        {
            if (strDDMMYYYY != "")
            {
                string strDay = "";
                string strMonth = "";
                string strYear = "";

                strDay = Convert.ToString(Convert.ToDateTime(strDDMMYYYY).Day);
                strMonth = Convert.ToString(Convert.ToDateTime(strDDMMYYYY).Month);
                strYear = Convert.ToString(Convert.ToDateTime(strDDMMYYYY).Year);

                if (strDay.Length == 1)
                {
                    strDay = "0" + strDay;
                }
                if (strMonth.Length == 1)
                {
                    strMonth = "0" + strMonth;
                }

                return (strDay + "/" + strMonth + "/" + strYear);
            }
            return null;
        }
        
        public string GetMMDDYYYY(string strMMDDYYYY)
        {
            if (strMMDDYYYY != "")
            {
                Array arrDate;
                arrDate = strMMDDYYYY.Split(Convert.ToChar("/"));
                return ((arrDate.GetValue(1).ToString().Trim()) + "/" + (arrDate.GetValue(0).ToString().Trim()) + "/" + (arrDate.GetValue(2).ToString().Trim()));

            }
            return "";
        }

        public string GetDDMMYYYYNew(string strYYYYMMDD)
        {
            if (strYYYYMMDD != "" && strYYYYMMDD != "1/1/1900 12:00:00 AM")
            {
                return Convert.ToDateTime(strYYYYMMDD).ToString("dd/MM/yyyy");
            }
            return "";
        }

        public string GetYYYYMMDD(string strDDMMYYYY)
        {
            if (strDDMMYYYY != "")
            {
                Array arrDate;
                arrDate = strDDMMYYYY.Split(Convert.ToChar("/"));
                return ((arrDate.GetValue(2).ToString().Trim()) + "-" + (arrDate.GetValue(1).ToString().Trim()) + "-" + (arrDate.GetValue(0).ToString().Trim()));

            }
            return "";
        }


        public string GetYYYYMMDDExcel(string strDDMMYYYY)
        {
            if (strDDMMYYYY != "")
            {
                Array arrDate;
                arrDate = strDDMMYYYY.Split(Convert.ToChar("/"));
                return ((arrDate.GetValue(2).ToString().Trim()) + "-" + (arrDate.GetValue(0).ToString().Trim()) + "-" + (arrDate.GetValue(1).ToString().Trim()));

            }
            return "";
        }

        public string GetDate(Char chrOnlyDate)
        {
            DateTime dt = DateTime.Now;
            string strTmpDate;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (chrOnlyDate == 'Y')
            {
                string[] arrTmpDate = new string[5];
                arrTmpDate = dt.ToString().Split(' ');
                strTmpDate = arrTmpDate[0];
                return strTmpDate;
            }
            else
            {
                strTmpDate = dt.ToString();
                return strTmpDate;
            }
        }
        
        public string GetDate()
        {
            DateTime dt = DateTime.Now;
            string strTmpDate;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern="yyyy-MM-dd";

            strTmpDate = dt.ToString();

            return (strTmpDate);
        }
        
        public static string RemoveSpecialChar(string sInputString)
        {
            string s;
            s = sInputString.Replace("'", "`").Trim();
            return s;
        }
        
        public string GetIpAddress()
        {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

        }
        
        public string Get24HourTime(string time, string ToD)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour;
            int minute;

            Array arrTime;
            arrTime = time.Split(Convert.ToChar(":"));

            hour = SafeTypeHandling.ConvertStringToInt32(arrTime.GetValue(0).ToString());
            minute = SafeTypeHandling.ConvertStringToInt32(arrTime.GetValue(1).ToString());

            if (ToD.ToUpper() == "PM") hour = (hour % 12) + 12;

            return new DateTime(year, month, day, hour, minute, 0).ToString("HH:mm");
        }
        
        public string Get12HourTime(string TimeIn24HourFormat)
        {
            return DateTime.ParseExact(TimeIn24HourFormat, "HH:mm", CultureInfo.CurrentCulture).ToString("h:mm tt");
        }
        
        public string GetMessage(int messageId)
        {
            switch (messageId)
            {
                case 1: return ("Record Saved Successfully");
                case 2: return ("Record Modified Successfully");
                case 3: return ("Record Deleted Successfully");
                case 4: return ("Duplicate Record Found");
                case 5: return ("Access Permission Denied");
                case 6: return ("Login Expired");
                case 7: return ("Are you sure, you want to Delete ?");
                case 8: return ("Access Denied.");
                case 10: return ("Please Specify Atleast One Searchable Item");
                case 11: return ("No Record Found");
                case 13: return ("Record No Longer Exist");
                case 14: return ("Record Can Not Be Deleted");
                case 15: return ("Record Saved Successfully");

                case 16: return ("Record Can Not Be Modified");
                case 17: return ("The following products can't be activated because their varient is InActive. Please activate varient first.");
                case 18: return ("The following varient can't be activated because their barnd is InActive. Please activate brand first.");
                case 19: return ("The following Brand can't be activated because their Sub-Category is InActive. Please activate Sub-Category first.");
                case 20: return ("The following Sub-Category can't be activated because their Category is InActive. Please activate Category first.");

                case 0: return ("Error Occurred");
                case -1: return ("Error While Saving Record");
                case -2: return ("Error While Modifying Record");
                case -3: return ("Error While Deleting Record");
                case -4: return ("Claim Date Cannot Be Greater Than Disbursement Date");
                case -5: return ("Disbursement Amount Cannot Be Greater Than Claim Amount");
                case -6: return ("Salary Already Disbursed for this month");
                case -15: return ("Error While Saving Record");
                default: return ("Please specified correct Error Number");

            }
        }
        
        public string ReturnConCatValue(string fieldsName)
        {
            string result = null;
            string dbType = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBType"].ToString());

            switch (Convert.ToInt32(dbType))
            {
                case 1:
                    result = fieldsName.Replace("$$$", "||");
                    break;
                case 2:
                    result = fieldsName.Replace("$$$", "+");
                    break;
            }

            return result;
        }

        /// <summary>
        /// This will use to replace special char line new line, space etc.. from multiline textbox.
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <returns>string</returns>
        /// <author>Daya Shankar</author>
        /// <createdOn>15 May 2013</createdOn>
        public string ReplaceEscapeSequenceChar(string inputString)
        {
            return inputString.Replace("\n", "<br/>").Replace("\"", "&#34;").Replace("\r", " ");
        }

        /// <summary>
        /// This will use to replace special char line new line, space etc.. from multiline textbox.
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <returns>string</returns>
        /// <author>Daya Shankar</author>
        /// <createdOn>15 May 2013</createdOn>
        public string SetEscapeSequenceChar(string inputString)
        {
            return inputString.Replace("<br/>", "\n").Replace("&#34;", "\"").Replace(" ", "\r");
        }

    }
}