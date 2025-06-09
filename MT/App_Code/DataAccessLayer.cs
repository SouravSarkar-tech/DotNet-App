using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accenture.MWT.DataAccess
{
    /// <summary>
    /// Created By 		    :  Manish Sharma
    /// Creation Date 		:  Feb-2006
    ///   
    /// Updated By           :  Daya Shankar Sharma
    /// Updated On           :  April 2013
    /// </summary>
    public class DataAccessLayer
    {
        public SqlConnection cnnConnection;
        public void OpenConnection(System.Web.UI.Page objSender)
        {
            LiteralControl litSender = new LiteralControl();
            string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            cnnConnection = new SqlConnection(strConnString);
            try
            {
                cnnConnection.Open();
            }
            catch (Exception ex)
            {
                litSender.Text = "<script language=JavaScript>window.alert('Connection Failed ')</scr";
                litSender.Text += "ipt>";
                objSender.Controls.Add(litSender);
            }
        }
        public void OpenConnection()
        {
            LiteralControl litSender = new LiteralControl();
            string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            cnnConnection = new SqlConnection(strConnString);
            try
            {
                //if (cnnConnection.State == ConnectionState.Closed)
                //{
                    cnnConnection.Open();
                //}
            }
            catch (Exception ex)
            {
                litSender.Text = "<script language=JavaScript>window.alert('Connection Failed ')</scr";
                litSender.Text += "ipt>";
                //objSender.Controls.Add(litSender);
            }
        }
        public void CloseConnection(SqlConnection cnnConnection)
        {
            try
            {
                if (cnnConnection.State == ConnectionState.Open)
                {
                    cnnConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CRUD Fuctions
        public bool AddRecord(string tableName, string fieldsName, string fieldsValue, ref SqlConnection cnnAdd, ref SqlTransaction objTrans)
        {
            string insertQuery;
            SqlCommand cmdAdd;

            insertQuery = "Insert Into " + tableName + " (" + fieldsName + ") Values(" + fieldsValue + ")";

            cmdAdd = new SqlCommand(insertQuery, cnnAdd);

            cmdAdd.Transaction = objTrans;
            //Srinidhi
            cmdAdd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmdAdd.ExecuteNonQuery();
                cmdAdd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool AddRecord(string tableName, string fieldsName, string fieldsValue, ref SqlConnection cnnAdd)
        {
            string insertQuery;
            SqlCommand cmdAdd;

            insertQuery = "Insert Into " + tableName + " (" + fieldsName + ") Values(" + fieldsValue + ")";

            cmdAdd = new SqlCommand(insertQuery, cnnAdd);

            //Srinidhi
            cmdAdd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmdAdd.ExecuteNonQuery();
                cmdAdd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool IsDuplicate(string tableName, string whereClause, ref SqlConnection cnnDuplicate, string pkColumn, Double pkValue, ref SqlTransaction objTrans)
        {
            bool functionReturnValue = true;
            SqlDataReader dtrDuplicate = default(SqlDataReader);
            SqlCommand cmdDuplicate = default(SqlCommand);
            string query = null;

            query = "SELECT * FROM " + tableName;

            if (!string.IsNullOrEmpty(whereClause))
            {
                query = query + " WHERE (" + whereClause + ")";
            }

            if (!string.IsNullOrEmpty(pkColumn))
            {
                query = query + " AND " + pkColumn + " <> " + pkValue;
            }

            cmdDuplicate = new SqlCommand(query, cnnDuplicate);

            try
            {
                //Check whether Transaction Object is nothing 
                if ((objTrans != null))
                {
                    cmdDuplicate.Transaction = objTrans;
                }

                dtrDuplicate = cmdDuplicate.ExecuteReader();
                if (dtrDuplicate.Read())
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                dtrDuplicate.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdDuplicate.Dispose();
                dtrDuplicate.Dispose();
            }
            return functionReturnValue;

        }

        public bool IsDuplicate(string tableName, string whereClause, ref SqlConnection cnnDuplicate)
        {
            bool functionReturnValue = false;

            SqlDataReader dtrDuplicate = default(SqlDataReader);
            SqlCommand cmdDuplicate = default(SqlCommand);
            string query = null;

            query = "SELECT * FROM " + tableName;

            if (!string.IsNullOrEmpty(whereClause))
            {
                query = query + " WHERE (" + whereClause + ")";
            }

            cmdDuplicate = new SqlCommand(query, cnnDuplicate);

            try
            {

                dtrDuplicate = cmdDuplicate.ExecuteReader();
                if (dtrDuplicate.Read())
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                dtrDuplicate.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdDuplicate.Dispose();
                dtrDuplicate.Dispose();
            }
            return functionReturnValue;

        }

        public bool IsDuplicate(string tableName, string whereClause, ref SqlConnection cnnDuplicate, ref SqlTransaction objTrans)//, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")] // ERROR: Optional parameters aren't supported in C# string strPKColumn, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# long intPKValue, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(null)] ref // ERROR: Optional parameters aren't supported in C# SqlTransaction objTrans) 
        {
            bool functionReturnValue = false;

            SqlDataReader dtrDuplicate = default(SqlDataReader);
            SqlCommand cmdDuplicate = default(SqlCommand);
            string query = null;

            query = "SELECT * FROM " + tableName;
            if (!string.IsNullOrEmpty(whereClause))
            {
                query = query + " WHERE (" + whereClause + ")";
            }

            cmdDuplicate = new SqlCommand(query, cnnDuplicate);

            try
            {
                if ((objTrans != null))
                {
                    cmdDuplicate.Transaction = objTrans;
                }
                dtrDuplicate = cmdDuplicate.ExecuteReader();
                if (dtrDuplicate.Read())
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                dtrDuplicate.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdDuplicate.Dispose();
                dtrDuplicate.Dispose();
            }
            return functionReturnValue;

        }
        //DNRCOMM//
        public bool IsDuplicate(string tableName, string whereClause)
        {
            bool functionReturnValue = false;

            SqlDataReader dtrDuplicate = default(SqlDataReader);
            SqlCommand cmdDuplicate = default(SqlCommand);
            string query = null;

            query = "SELECT * FROM " + tableName;

            if (!string.IsNullOrEmpty(whereClause))
            {
                query = query + " WHERE (" + whereClause + ")";
            }



            try
            {
                OpenConnection();
                cmdDuplicate = new SqlCommand(query, cnnConnection);
                dtrDuplicate = cmdDuplicate.ExecuteReader();
                if (dtrDuplicate.Read())
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                dtrDuplicate.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(cnnConnection);
                cmdDuplicate.Dispose();
                dtrDuplicate.Dispose();
            }
            return functionReturnValue;

        }

        public int GetPK(string tableName, string pkColumn, ref SqlConnection cnnPK, ref SqlTransaction objTrans)//, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# int intUnitId) 
        {

            SqlCommand cmdSelect = default(SqlCommand);
            int functionReturnValue = 0;
            string query = null;
            int intPK = 0;
            query = "SELECT (CASE WHEN  MAX(" + pkColumn + ") IS NULL THEN 1 ELSE MAX(" + pkColumn + ") + 1 END) AS NEXTVAL FROM " + tableName + "";

            cmdSelect = new SqlCommand(query, cnnPK);
            cmdSelect.Transaction = objTrans;
            //Srinidhi
            cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                intPK = Convert.ToInt32(cmdSelect.ExecuteScalar());
                functionReturnValue = intPK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdSelect.Dispose();
                cmdSelect = null;
                query = null;
            }
            return functionReturnValue;

        }

        public int GetPK(string tableName, string pkColumn, ref SqlConnection cnnPK)
        {

            SqlCommand cmdSelect = default(SqlCommand);
            int functionReturnValue = 0;
            string query = null;
            int intPK = 0;
            query = "SELECT (CASE WHEN  MAX(" + pkColumn + ") IS NULL THEN 1 ELSE MAX(" + pkColumn + ") + 1 END) AS NEXTVAL FROM " + tableName + "";

            cmdSelect = new SqlCommand(query, cnnPK);
            //Srinidhi
            cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                intPK = Convert.ToInt32(cmdSelect.ExecuteScalar());
                functionReturnValue = intPK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdSelect.Dispose();
                cmdSelect = null;
                query = null;
            }
            return functionReturnValue;

        }

        public int GetPKForManufacturerTable(string tableName, string manufacturerId, ref SqlConnection cnnPK, ref SqlTransaction objTrans)
        {

            SqlCommand cmdSelect = default(SqlCommand);
            int functionReturnValue = 0;
            string query = null;
            int intPK = 0;
            query = "SELECT PK_Val + 1 FROM M_PK_VAL WHERE Table_Name = '" + tableName + "' AND Manufacturer_Id = " + manufacturerId;

            cmdSelect = new SqlCommand(query, cnnPK);
            cmdSelect.Transaction = objTrans;
            //Srinidhi
            cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                intPK = Convert.ToInt32(cmdSelect.ExecuteScalar());
                functionReturnValue = intPK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdSelect.Dispose();
                cmdSelect = null;
                query = null;
            }
            return functionReturnValue;

        }

        public bool SetPkForManufacturerTable(string tableName, string pkValue, string manufacturerId, ref SqlConnection cnnModify, ref SqlTransaction objTrans)
        {
            SqlCommand cmdUpdate;

            string query = "UPDATE M_PK_VAL SET PK_Val = " + pkValue + " WHERE Table_Name = '" + tableName + "' AND Manufacturer_Id = " + manufacturerId;
            cmdUpdate = new SqlCommand(query, cnnModify);
            cmdUpdate.Transaction = objTrans;
            //Srinidhi
            cmdUpdate.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmdUpdate.ExecuteNonQuery();
                return (true);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cmdUpdate.Dispose();
            }
        }

        public int GetPKForDistributorTable(string tableName, string distributorId, ref SqlConnection cnnPK, ref SqlTransaction objTrans)
        {

            SqlCommand cmdSelect = default(SqlCommand);
            int functionReturnValue = 0;
            string query = null;
            int intPK = 0;
            query = "SELECT PK_Val + 1 FROM D_PK_VAL WHERE Table_Name = '" + tableName + "' AND Distributor_Id = " + distributorId;

            cmdSelect = new SqlCommand(query, cnnPK);
            cmdSelect.Transaction = objTrans;
            //Srinidhi
            cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                intPK = Convert.ToInt32(cmdSelect.ExecuteScalar());
                functionReturnValue = intPK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdSelect.Dispose();
                cmdSelect = null;
                query = null;
            }
            return functionReturnValue;

        }

        public bool SetPkForDistributorTable(string tableName, string pkValue, string distributorID, ref SqlConnection cnnModify, ref SqlTransaction objTrans)
        {
            SqlCommand cmdUpdate;

            string query = "UPDATE D_PK_VAL SET PK_Val = " + pkValue + " WHERE Table_Name = '" + tableName + "' AND Distributor_Id = " + distributorID;
            cmdUpdate = new SqlCommand(query, cnnModify);
            cmdUpdate.Transaction = objTrans;
            //Srinidhi
            cmdUpdate.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmdUpdate.ExecuteNonQuery();
                return (true);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cmdUpdate.Dispose();
            }
        }

        public bool ModifyRecord(string tableName, string fieldNames, string fieldValues, string whereClause, ref SqlConnection cnnModify, ref SqlTransaction objTrans)
        {
            SqlCommand cmdUpdate;
            Array arrField;
            Array arrValue;
            char[] arrCharSeperator;
            string seperator;
            StringBuilder queryBuilder = new StringBuilder();

            int intUBound;
            int intCounter;
            seperator = "$";
            arrCharSeperator = seperator.ToCharArray();

            arrField = fieldNames.Split(arrCharSeperator);
            arrValue = fieldValues.Split(arrCharSeperator);

            queryBuilder.Append("Update ");
            queryBuilder.Append(tableName);
            queryBuilder.Append(" Set ");

            if ((arrField.GetUpperBound(0)) == (arrValue.GetUpperBound(0)))
            {
                intUBound = Convert.ToInt16(arrField.GetUpperBound(0));
                for (intCounter = 0; intCounter < intUBound; intCounter++)
                {
                    if (intCounter == 0)
                    {
                        queryBuilder.Append(arrField.GetValue(intCounter));
                        queryBuilder.Append(" = ");
                        queryBuilder.Append(arrValue.GetValue(intCounter));
                    }
                    else
                    {
                        queryBuilder.Append(",");
                        queryBuilder.Append(arrField.GetValue(intCounter));
                        queryBuilder.Append(" = ");
                        queryBuilder.Append(arrValue.GetValue(intCounter));
                    }
                }
            }
            else
            {
                return (false);
            }

            queryBuilder.Append(" Where ");
            queryBuilder.Append(whereClause);
            cmdUpdate = new SqlCommand(queryBuilder.ToString(), cnnModify);
            cmdUpdate.Transaction = objTrans;
            //Srinidhi
            cmdUpdate.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmdUpdate.ExecuteNonQuery();
                return (true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdUpdate.Dispose();
            }
        }

        public bool DeleteRecord(string tableName, string whereClause, ref SqlConnection cnnDelete, ref SqlTransaction objTrans)
        {
            bool functionReturnValue = false;
            string query = null;
            string queryExist = null;
            SqlCommand cmdDelete = default(SqlCommand);
            SqlCommand cmdRecordExists = default(SqlCommand);
            int intCount = 0;

            queryExist = "SELECT COUNT(*) FROM " + tableName + " WHERE " + whereClause;
            query = "DELETE FROM " + tableName + " WHERE " + whereClause;

            cmdRecordExists = new SqlCommand(queryExist, cnnDelete);
            cmdDelete = new SqlCommand(query, cnnDelete);

            cmdRecordExists.Transaction = objTrans;
            cmdDelete.Transaction = objTrans;

            try
            {
                intCount = Convert.ToInt32(cmdRecordExists.ExecuteScalar());
                if (intCount >= 1)
                {
                    cmdDelete.ExecuteNonQuery();
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                cmdDelete.Dispose();
            }
            return functionReturnValue;

        }

        public string GetNumber(string moduleName, string distributorId, ref SqlConnection cnnPK, ref SqlTransaction objTrans)
        {
            SqlCommand cmdSelect = default(SqlCommand);
            StringBuilder queryBuilder = new StringBuilder();
            string number = string.Empty;
            queryBuilder.Append(" DECLARE @CounterSeq AS VARCHAR(5) ");
            queryBuilder.Append(" DECLARE @CounterNo AS INT");
            queryBuilder.Append(" SELECT @CounterNo = CAST(Counter_Seq AS INT) + 1 FROM D_MST_COUNTER WHERE Distributor_Id = " + distributorId + " AND Module_Name = '" + moduleName + "' AND IsActive = 'TRUE'");
            queryBuilder.Append(" SET @CounterSeq = RIGHT(CAST('0000'+ CAST(@CounterNo AS VARCHAR) AS VARCHAR(9)),5)");
            queryBuilder.Append(" SELECT Prefix+@CounterSeq+COALESCE(Suffix,'')+'/'+Financial_Year FROM D_MST_COUNTER WHERE Distributor_Id = " + distributorId + " AND Module_Name = '" + moduleName + "' AND IsActive = 'TRUE'");

            cmdSelect = new SqlCommand(queryBuilder.ToString(), cnnPK);
            cmdSelect.Transaction = objTrans;
            //Srinidhi
            cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                number = Convert.ToString(cmdSelect.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdSelect.Dispose();
                cmdSelect = null;
            }
            return number;

        }

        public bool SetNumber(string moduleName, string distributorId, ref SqlConnection cnnModify, ref SqlTransaction objTrans)
        {
            SqlCommand cmdUpdate;
            StringBuilder queryBuilder = new StringBuilder();

            queryBuilder.Append(" DECLARE @CounterSeq AS VARCHAR(5) ");
            queryBuilder.Append(" DECLARE @CounterNo AS INT");
            queryBuilder.Append(" SELECT @CounterNo = CAST(Counter_Seq AS INT) + 1 FROM D_MST_COUNTER WHERE Distributor_Id = " + distributorId + " AND Module_Name = '" + moduleName + "' AND IsActive = 'TRUE'");
            queryBuilder.Append(" SET @CounterSeq = RIGHT(CAST('0000'+ CAST(@CounterNo AS VARCHAR) AS VARCHAR(9)),5)");
            queryBuilder.Append(" UPDATE D_MST_COUNTER SET Counter_Seq =  @CounterSeq  WHERE Distributor_Id = " + distributorId + " AND Module_Name = '" + moduleName + "' AND IsActive = 'TRUE'");

            cmdUpdate = new SqlCommand(queryBuilder.ToString(), cnnModify);
            cmdUpdate.Transaction = objTrans;
            //Srinidhi
            cmdUpdate.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmdUpdate.ExecuteNonQuery();
                return (true);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cmdUpdate.Dispose();
            }

        }
        #endregion

        #region Return Single Value

        /// <summary>
        /// This fuction open and close connection by itself. //DNRCOMM//
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>string value</returns>
        public string ReturnString(string query)
        {
            string result = "";
            try
            {
                OpenConnection();
                SqlCommand cmdSelect = new SqlCommand(query, cnnConnection);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToString(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(cnnConnection);
            }
        }

        public string ReturnString(string tableName, string fieldsName, string whereClause, ref SqlConnection cnnReturnVal)
        {
            StringBuilder queryBuilder = new StringBuilder();
            string strResult = "";

            queryBuilder.Append("Select ");
            queryBuilder.Append(fieldsName);
            queryBuilder.Append("  From ");
            queryBuilder.Append(tableName);
            queryBuilder.Append(" Where ");
            queryBuilder.Append(whereClause);

            try
            {
                SqlCommand cmdSelect = new SqlCommand(queryBuilder.ToString(), cnnReturnVal);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                strResult = Convert.ToString(cmdSelect.ExecuteScalar());
                return (strResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ReturnString(string tableName, string fieldsName, string whereClause, ref SqlConnection cnnReturnVal, ref SqlTransaction objTrans)
        {
            StringBuilder queryBuilder = new StringBuilder();
            string result = "";

            queryBuilder.Append("Select ");
            queryBuilder.Append(fieldsName);
            queryBuilder.Append("  From ");
            queryBuilder.Append(tableName);
            queryBuilder.Append(" Where ");
            queryBuilder.Append(whereClause);

            try
            {
                SqlCommand cmdSelect = new SqlCommand(queryBuilder.ToString(), cnnReturnVal);
                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToString(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ReturnString(string query, ref SqlConnection cnnReturnVal)
        {
            string result = "";
            try
            {
                SqlCommand cmdSelect = new SqlCommand(query, cnnReturnVal);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToString(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ReturnString(string query, ref SqlConnection cnnReturnVal, ref SqlTransaction objTrans)
        {
            string result = "";

            try
            {
                SqlCommand cmdSelect = new SqlCommand(query, cnnReturnVal);
                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToString(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch
            {
                throw;
            }
        }
        public double ReturnDouble(string query, ref SqlConnection cnnConnection)
        {
            double result;
            SqlCommand cmdSelect;
            try
            {
                cmdSelect = new SqlCommand(query, cnnConnection);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToDouble(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double ReturnDouble(string query, ref SqlConnection cnnConnection, ref SqlTransaction objTrans)
        {
            double result;
            SqlCommand cmdSelect;
            try
            {
                cmdSelect = new SqlCommand(query, cnnConnection);
                cmdSelect.Transaction = objTrans;
                result = Convert.ToDouble(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DNRCOMM//
        public int ReturnInt(string query)
        {
            int result;
            SqlCommand cmdSelect;
            try
            {
                OpenConnection();
                cmdSelect = new SqlCommand(query, cnnConnection);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToInt32(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(cnnConnection);
            }
        }
        public int ReturnInt(string query, ref SqlConnection cnnConnection)
        {
            int result;
            SqlCommand cmdSelect;
            try
            {
                cmdSelect = new SqlCommand(query, cnnConnection);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToInt32(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ReturnInt(string query, ref SqlConnection cnnConnection, ref SqlTransaction objTrans)
        {
            int result;
            SqlCommand cmdSelect;
            try
            {
                cmdSelect = new SqlCommand(query, cnnConnection);
                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                result = Convert.ToInt32(cmdSelect.ExecuteScalar());
                return (result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ReturnExecuteNonQuery(CommandType cmdType, string commandString, Hashtable hashPara, ref SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = commandString;
                cmd.Connection = connection;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }

                cmd = SetInputParamaterList(cmd, hashPara);
                //Srinidhi
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                SqlParameter sqlParaReturn = cmd.Parameters.Add("@return", SqlDbType.Int);
                sqlParaReturn.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                int result = (int)sqlParaReturn.Value;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Fill DataSet
        public DataSet FillDataSet(string tableName, string fieldsName, string whereClause, ref SqlConnection cnnReturnDS)
        {
            SqlCommand cmdSelect = new SqlCommand();
            StringBuilder query = new StringBuilder();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            query.Append("Select ");
            query.Append(fieldsName);
            query.Append("  From ");
            query.Append(tableName);

            if (whereClause != string.Empty)
            {
                query.Append(" Where ");
                query.Append(whereClause);
            }
            try
            {
                cmdSelect.CommandText = query.ToString();
                cmdSelect.Connection = cnnReturnDS;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet, tableName);
                return (dstMyDataSet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet FillDataSet(string tableName, string fieldsName, string whereClause, ref SqlConnection cnnReturnDS, ref SqlTransaction objTrans)
        {
            SqlCommand cmdSelect = new SqlCommand();
            StringBuilder query = new StringBuilder();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            query.Append("Select ");
            query.Append(fieldsName);
            query.Append("  From ");
            query.Append(tableName);
            query.Append(" Where ");
            query.Append(whereClause);

            try
            {
                cmdSelect.CommandText = query.ToString();
                cmdSelect.Connection = cnnReturnDS;

                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet, tableName);
                return (dstMyDataSet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet FillDataSet(string query, string tableName, ref SqlConnection cnnReturnDS)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            try
            {
                cmdSelect.CommandText = query;
                cmdSelect.Connection = cnnReturnDS;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet, tableName);
                return (dstMyDataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet FillDataSet(CommandType cmdType, string query, ref SqlConnection cnnReturnDS)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            try
            {
                cmdSelect.CommandText = query;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmdSelect.CommandType = CommandType.StoredProcedure;
                }
                cmdSelect.Connection = cnnReturnDS;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet);

                return (dstMyDataSet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet FillDataSet(string query, string tableName, ref SqlConnection cnnReturnDS, ref SqlTransaction objTrans)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            try
            {
                cmdSelect.CommandText = query;
                cmdSelect.Connection = cnnReturnDS;
                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet, tableName);
                return (dstMyDataSet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DNRCOMM//
        /// <summary>
        /// This Function will fill DataSet. Do not need to pass connection object as parameter as this will open and close connection itself
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="tableName">TableName</param>
        /// <returns>DataSet</returns>
        public DataSet FillDataSet(string query, string tableName)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            try
            {
                OpenConnection();
                cmdSelect.CommandText = query;
                cmdSelect.Connection = cnnConnection;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet, tableName);
                return (dstMyDataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(cnnConnection);
            }
        }

        //DNRCOMM//
        public DataSet FillDataSet(string query)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            try
            {
                OpenConnection();
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "exec "+query;
                cmdSelect.Connection = cnnConnection;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet);
                return (dstMyDataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(cnnConnection);
            }
        }

        //DNRCOMM//
        /// <summary>
        /// This Function will fill DataSet. Do not need to pass connection object as parameter as this will open and close connection itself
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet FillDataSet(CommandType cmdType, string sql, Hashtable hashPara)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataAdapter adpSelect;
            DataSet dstMyDataSet = new DataSet();

            try
            {
                //if (cnnConnection.State == ConnectionState.Closed)
                //{ 
                    OpenConnection();
                //}
                cmdSelect.CommandText = sql;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmdSelect.CommandType = CommandType.StoredProcedure;
                }
                cmdSelect.Connection = cnnConnection;
                cmdSelect = SetInputParamaterList(cmdSelect, hashPara);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dstMyDataSet);
                return (dstMyDataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(cnnConnection);
            }
        }

        #endregion

        public int CheckAccessPermission(int userID, int moduleID, string accessType, ref SqlConnection cnnAccess)
        {
            int functionReturnValue = 0;

            string query = null;
            string strAccessColumn = null;
            SqlCommand cmdSelect = default(SqlCommand);
            int intCount = 0;

            if (userID != 0)
            {
                if ((accessType == "ADD"))
                {
                    strAccessColumn = "ADD_ACCESS";
                }
                else if ((accessType == "MOD"))
                {
                    strAccessColumn = "EDIT_ACCESS";
                }
                else if ((accessType == "DEL"))
                {
                    strAccessColumn = "DELETE_ACCESS";
                }
                else if ((accessType == "VIEW"))
                {
                    strAccessColumn = "VIEW_ACCESS";
                }

                query = "SELECT COUNT(*) FROM Module_Access_Matrix WHERE User_Id = " + userID + " AND Module_Id = " + moduleID + " AND " + strAccessColumn + " = 'True'";
                cmdSelect = new SqlCommand(query, cnnAccess);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                try
                {
                    intCount = Convert.ToInt32(cmdSelect.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    throw exp;
                }

                if (intCount == 0)
                {
                    functionReturnValue = -1;
                }
                else
                {
                    functionReturnValue = 1;
                }
            }

            else
            {
                functionReturnValue = 2;
            }
            return functionReturnValue;

        }
        public bool ReturnBool(string query, ref SqlConnection objConnection, ref SqlTransaction objTrans)
        {
            bool returnFlag = false;
            try
            {
                SqlCommand cmdSelect = new SqlCommand(query, objConnection);
                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                SqlDataReader dr = cmdSelect.ExecuteReader();
                if (dr.Read())
                {
                    returnFlag = true;
                }
                dr.Close();
                return returnFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ReturnBool(string procName, Hashtable hashPara, ref SqlConnection objConnection, ref SqlTransaction objTrans)
        {
            bool returnFlag = false;
            try
            {
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.CommandText = procName;
                cmdSelect.CommandType = CommandType.StoredProcedure;
                cmdSelect.Connection = objConnection;
                cmdSelect.Transaction = objTrans;
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);
                
                cmdSelect = SetInputParamaterList(cmdSelect, hashPara);

                SqlDataReader dr = cmdSelect.ExecuteReader();
                if (dr.Read())
                {
                    returnFlag = true;
                }
                dr.Close();
                return returnFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ReturnBool(string query, ref SqlConnection objConnection)
        {
            bool returnFlag = false;
            try
            {
                SqlCommand cmdSelect = new SqlCommand(query, objConnection);
                //Srinidhi
                cmdSelect.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                SqlDataReader dr = cmdSelect.ExecuteReader();
                if (dr.Read())
                {
                    returnFlag = true;
                }
                dr.Close();
                return returnFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArrayList ReturnArrayList(string textField, string valueField, string tableName, string whereClause, ref SqlConnection cnnReturnDS)
        {
            string strSQL;
            SqlDataAdapter objAdapter;
            DataSet dstData;

            try
            {

                strSQL = "Select " + textField + " As Text," + valueField + " As Value From " + tableName;

                if (whereClause != "")
                {
                    strSQL = strSQL + " WHERE " + whereClause;
                }

                objAdapter = new SqlDataAdapter(strSQL, cnnReturnDS);
                dstData = new DataSet();
                objAdapter.Fill(dstData, tableName);

                ArrayList lstArrList = new ArrayList();

                foreach (DataRow row in dstData.Tables[0].Rows)
                {
                    lstArrList.Add(new ListItem(row[0].ToString(), row[1].ToString()));
                }
                return lstArrList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ExecuteQuery(string query, ref SqlConnection cnnCon, ref SqlTransaction objTrans)
        {
            bool functionReturnValue = false;
            SqlCommand cmd = default(SqlCommand);
            cmd = new SqlCommand(query, cnnCon);

            cmd.Transaction = objTrans;
            //Srinidhi
            cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            try
            {
                cmd.ExecuteNonQuery();
                functionReturnValue = true;
            }

            catch (SqlException exp)
            {
                functionReturnValue = false;
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
            return functionReturnValue;

        }

        public SqlCommand SetInputParamaterList(SqlCommand cmd, Hashtable hashPara)
        {
            try
            {
                IDictionaryEnumerator myEnumerator = hashPara.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    cmd.Parameters.AddWithValue(myEnumerator.Key.ToString(), myEnumerator.Value);
                }
                return cmd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}