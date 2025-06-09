using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Accenture.MWT.DataAccess;

namespace Accenture.MWT.ExceptionUtil
{
    public class ExceptionHandling
    {
        /// <summary>
        /// Log Exception In Database
        /// </summary>
        /// <param name="errorIn"></param>
        /// <param name="errorMessage"></param>
        /// <param name="stackTrace"></param>
        /// <author>Days Shankar</author>
        /// <createdOn>9 May 2013</createdOn>
        public void LogException(string errorIn, string errorMessage, string stackTrace)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlTransaction objTrans;

            string tableName = string.Empty;
            string fieldsName = string.Empty;
            string fieldsValue = string.Empty;
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            string userId;
            try
            {
                userId = System.Web.HttpContext.Current.Session[StaticKeys.LoggedIn_User_Id].ToString();
            }
            catch
            {
                userId = "1";
            }

            string dateTime = objUtil.GetDate();

            tableName = "S_Error_Log";
            fieldsName = "ErrorIn,Error_Message,Stack_Trace,Date_Time,UserId,IpAddress";
            try
            {
                objDal.OpenConnection();
                objTrans = objDal.cnnConnection.BeginTransaction();

                fieldsValue = "'" + Utility.RemoveSpecialChar(errorIn) + "','" + Utility.RemoveSpecialChar(errorMessage) + "','" + Utility.RemoveSpecialChar(stackTrace.Replace("\n", "<br/>")) + "','" + dateTime + "'," + userId + ",'" + ipAddress + "'";
                if (objDal.AddRecord(tableName, fieldsName, fieldsValue, ref objDal.cnnConnection, ref objTrans))
                {
                    objTrans.Commit();
                }
                else
                {
                    objTrans.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
                objUtil = null;
                objTrans = null;
            }
        }



    }
}