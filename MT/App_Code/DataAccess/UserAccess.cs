using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using Accenture.MWT.LDAPHelper;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class UserAccess
    {
        public DataSet ValidateUserAD(string userName)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();

            string procName = "Proc_Validate_User_MTest";
            try
            {
                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                //Srinidhi
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@UserName", userName));
                cmd.Parameters.Add(new SqlParameter("@Password", "aAyh7s4l2yg="));

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "User_Master");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }
            return dstData;
        }

        public DataSet ValidateUser(string userName, string password)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();
            //MGR_UPD_SDT27062019
            //string procName = "Proc_Validate_User";

            string procName = "Proc_Validate_User_MTest";
            //MGR_UPD_SDT27062019
            try
            {
                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                //Srinidhi
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@UserName", userName));
                cmd.Parameters.Add(new SqlParameter("@Password", Encryption.Encrypt(password)));

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "User_Master");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }
            return dstData;
        }

        public DataSet ValidateGlobalUser(string userName, string password, string Profile)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();
            string procName = "pr_Validate_Global_User";

            try
            {
                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@UserName", userName));
                cmd.Parameters.Add(new SqlParameter("@Password", password));
                cmd.Parameters.Add(new SqlParameter("@Profile", Profile));
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "User_Master");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }
            return dstData;
        }

        public DataSet ValidateUserProsol(string userName, string password, string Profile)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();
            string procName = "pr_Validate_Prosol_User";

            try
            {
                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@UserName", userName));
                cmd.Parameters.Add(new SqlParameter("@Password", password));
                cmd.Parameters.Add(new SqlParameter("@Profile", Profile));
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "User_Master");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }
            return dstData;
        }


        public DataSet GetBulkUserList(string userName, string Flag, string StartDate = "", string EndDate = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Flag", Flag);
            hashPara.Add("@Full_Name", userName);
            hashPara.Add("@Start_Date", StartDate);
            hashPara.Add("@End_Date", EndDate);

            string procName = "pr_GetBulkUserList";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet GetBulkUserList1()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Flag", "U");

            string procName = "pr_GetBulkUserList";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet ReadUser(string userName, string userId, string StartDate = "", string EndDate = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@CreatedBy", userId);
            hashPara.Add("@Full_Name", userName);
            hashPara.Add("@Start_Date", StartDate);
            hashPara.Add("@End_Date", EndDate);

            string procName = "pr_Search_User_Master";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }


        public DataSet ReadUserOld(string userName, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT User_Id,UserName, Full_Name AS 'FullName',EmailId ");
            queryBuilder.Append(" FROM User_Master ");
            queryBuilder.Append(" WHERE CreatedBy = " + userId + " AND Full_Name LIKE '%" + userName + "%'");
            return objDal.FillDataSet(queryBuilder.ToString(), "User_Master");
        }

        public DataSet ReadUserDetailByUserId(string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = new DataSet();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT User_id,UserName, Full_Name ,EmailId, Country_Id,P.Profile_Id,Department_Id,ReportingTo_Name,ReportingTo_Email ");
            query.Append(" FROM User_Master U JOIN Profile_Master P");
            query.Append(" ON U.Profile_Id = P.Profile_Id");
            query.Append(" WHERE U.User_id = " + userId);

            return objDal.FillDataSet(query.ToString(), "C_MST_USER");
        }


        //************* this code add by rahul jadhav for Add utility for reporting manager updation**************//
        public DataSet ReadUserDetailByUserName(string userName)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = new DataSet();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT User_id,UserName, Full_Name ,EmailId, Country_Id,P.Profile_Id,Department_Id,ReportingTo_Name,ReportingTo_Email ");
            query.Append(" FROM User_Master U JOIN Profile_Master P");
            query.Append(" ON U.Profile_Id = P.Profile_Id");
            query.Append(" AND UserName = '" + userName + "' AND U.Profile_id='2'"); //AND Emp_Code is not null"); Remove Empcode from adminUtility 30/03/2018

            return objDal.FillDataSet(query.ToString(), "C_MST_USER");
        }

        public DataSet readManagerDetail(string username)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = new DataSet();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT User_id,UserName, Full_Name ,EmailId, Country_Id,P.Profile_Id,Department_Id,ReportingTo_Name,ReportingTo_Email ");
            query.Append(" FROM User_Master U JOIN Profile_Master P");
            query.Append(" ON U.Profile_Id = P.Profile_Id");
            query.Append(" AND UserName = '" + username + "' AND U.Profile_id='4' AND Department_id='28'"); //AND Emp_Code is not null");Remove Empcode from adminUtility 30/03/2018

            return objDal.FillDataSet(query.ToString(), "C_MST_USER");
        }

        public string UpdateReportingMan(string UserId, string ManagerName, string Module, string masterCategory)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_UpdateReportingMangerAdminutility";
            //DataSet ds;
            int result = 0;
            hashPara.Add("@user_Id", UserId);
            hashPara.Add("@Repman", ManagerName);
            //hashPara.Add("@RepmanEmail", ManagerEmail);
            hashPara.Add("@Module_Id", Module);
            //hashPara.Add("@Master_Category_Code", masterCategory);
            //hashPara.Add("@strMsg1", result);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                objDal = null;
            }
        }
        //END ************* this code add by rahul jadhav for Add utility for reporting manager updation**************//
        public DataSet ReadMenuByUserId(string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Menu_ID,View_Right,Add_Right,Update_Right,Delete_Right FROM User_Menu_Mapping WHERE User_Id = " + userId;
            return objDal.FillDataSet(query, "User_Menu_Mapping");
        }

        public DataSet ReadMenuByProfileId(string profileId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Menu_ID,View_Right,Add_Right,Update_Right,Delete_Right FROM Profile_Menu_Mapping WHERE Profile_Id = " + profileId;
            return objDal.FillDataSet(query, "Profile_Menu_Mapping");
        }

        public DataSet ReadMenus()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" SELECT Menu_ID, Menu_Name,Parent_Id,Page_URL ");
            queryBuilder.Append(" FROM Menu_Master ");
            queryBuilder.Append(" WHERE IsActive = 'TRUE' Order By Menu_ID");
            return objDal.FillDataSet(queryBuilder.ToString(), "Profile_Master");
        }

        public DataSet ReadProfile()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Profile_Id,Profile_Name FROM Profile_Master WHERE IsActive = 'TRUE' ORDER BY Profile_Name";
            return objDal.FillDataSet(query, "Profile_Master");
        }

        public DataSet ReadCountry()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Country_Id,Country_Name FROM Country_Master WHERE IsActive = 'TRUE' ORDER BY Country_Name";
            return objDal.FillDataSet(query, "Country_Master");
        }

        public DataSet ReadDepartments()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Department_Id,Department_Name FROM M_Department WHERE IsActive = 'True' ORDER BY Department_Name";
            return objDal.FillDataSet(query, "Department_Master");
        }

        public DataSet ReadDepartments(string moduleId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Department_Id,Department_Name from M_Department where Department_Id IN(select distinct Department_Id  from M_Approving_Authority where Module_Id = " + moduleId + ") ORDER BY Department_Name";
            return objDal.FillDataSet(query, "Department_Master");
        }

        public DataSet ReadSectionTabs(string userId, string departmentId, string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Generate_Section_Tabs";
            hashPara.Add("@UserId", userId);
            hashPara.Add("@DepartmentId", departmentId);
            hashPara.Add("@MasterHeaderId", MasterHeaderId);

            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public bool DeleteUserMenu(string userId, ref SqlConnection cnnDelete, ref SqlTransaction objTrans)
        {

            SqlCommand cmdDelete = default(SqlCommand);
            string query = string.Empty;
            bool flg = false;
            query = "DELETE FROM User_Menu_Mapping WHERE User_Id=" + userId;
            cmdDelete = new SqlCommand(query, cnnDelete);
            cmdDelete.Transaction = objTrans;

            try
            {
                cmdDelete.ExecuteNonQuery();
                flg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdDelete.Dispose();
            }
            return flg;

        }
        public int SaveUserDetailSSO(SSOusers ObjADUser, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            //MGR_UPD_SDT27062019 
            //string procName = "pr_InsUpd_User_Master";
            string procName = "pr_InsUpd_User_Master_MTest";
            //MGR_UPD_SDT27062019 
            int result = 0;
            string IpAddress = new Utility().GetIpAddress();
            ObjADUser.userPrincipalName = ObjADUser.userPrincipalName.Split('@')[0];
            ObjADUser.userPrincipalName = ObjADUser.userPrincipalName.Split('_')[0];
            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Profile_Id", "2");
            hashPara.Add("@UserName", ObjADUser.userPrincipalName);
            hashPara.Add("@Password", Encryption.Encrypt("mwt"));
            hashPara.Add("@Full_Name", ObjADUser.displayName);
            hashPara.Add("@EmailId", ObjADUser.mail);
            //MGR_UPD_SDT27062019  Commented by NR
            // hashPara.Add("@ReportingTo_Name", "");
            // hashPara.Add("@ReportingTo_Email", "");
            //MGR_UPD_SDT27062019  Commented by NR
            //MGR_UPD_SDT27062019  Updated by NR
            try
            {
                if (ObjADUser.manager != null)
                {
                    hashPara.Add("@ReportingTo_Name", ObjADUser.manager.displayName);
                    if (ObjADUser.manager.displayName != null)
                    {
                        hashPara.Add("@ReportingTo_Email", ObjADUser.manager.mail);
                    }
                    else
                    {
                        hashPara.Add("@ReportingTo_Email", "");
                    }
                }
                else
                {
                    hashPara.Add("@ReportingTo_Name", "");
                    hashPara.Add("@ReportingTo_Email", "");
                }
            }
            catch (Exception ex)
            {

            }
            //MGR_UPD_SDT27062019  Updated by NR
            hashPara.Add("@IsActive", "1");
            hashPara.Add("@CreatedBy", "1");
            hashPara.Add("@CreatedIP", IpAddress);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        public int SaveRMNSMDetailSSO(SSOusers ObjADUser, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_InsUpd_User_RM_NSM_Master";
            int result = 0;
            string IpAddress = new Utility().GetIpAddress();
            ObjADUser.manager.userPrincipalName = ObjADUser.manager.userPrincipalName.Split('@')[0];
            ObjADUser.manager.userPrincipalName = ObjADUser.manager.userPrincipalName.Split('_')[0];
            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Profile_Id", "4");
            hashPara.Add("@UserName", ObjADUser.manager.userPrincipalName);
            hashPara.Add("@Password", Encryption.Encrypt("mwt"));
            hashPara.Add("@Full_Name", ObjADUser.manager.displayName);
            hashPara.Add("@EmailId", ObjADUser.manager.mail);
            hashPara.Add("@ReportingTo_Name", "");
            hashPara.Add("@ReportingTo_Email", ""); 

            //hashPara.Add("@ReportingTo_Name", ObjADUser.ManagerName);
            //hashPara.Add("@ReportingTo_Email", ObjADUser.Manager.EmailAddress);

            hashPara.Add("@IsActive", "1");
            hashPara.Add("@CreatedBy", "1");
            hashPara.Add("@CreatedIP", IpAddress);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        public int SaveUserDetail(ADUserDetail ObjADUser, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            //MGR_UPD_SDT27062019 
            //string procName = "pr_InsUpd_User_Master";
            string procName = "pr_InsUpd_User_Master_MTest";
            //MGR_UPD_SDT27062019 
            int result = 0;
            string IpAddress = new Utility().GetIpAddress();

            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Profile_Id", "2");
            hashPara.Add("@UserName", ObjADUser.LoginName);
            hashPara.Add("@Password", Encryption.Encrypt("mwt"));
            hashPara.Add("@Full_Name", ObjADUser.FirstName + " " + ObjADUser.LastName);
            hashPara.Add("@EmailId", ObjADUser.EmailAddress);
            //MGR_UPD_SDT27062019  Commented by NR
            // hashPara.Add("@ReportingTo_Name", "");
            // hashPara.Add("@ReportingTo_Email", "");
            //MGR_UPD_SDT27062019  Commented by NR
            //MGR_UPD_SDT27062019  Updated by NR
            try
            {
                if (ObjADUser.ManagerName != null && ObjADUser.ManagerName != "")
                {
                    hashPara.Add("@ReportingTo_Name", ObjADUser.ManagerName);
                    if (ObjADUser.Manager != null)
                    {
                        hashPara.Add("@ReportingTo_Email", ObjADUser.Manager.EmailAddress); 
                    }
                    else
                    {
                        hashPara.Add("@ReportingTo_Email", "");
                    }
                }
                else
                {
                    hashPara.Add("@ReportingTo_Name", "");
                    hashPara.Add("@ReportingTo_Email", "");
                }
            }
            catch (Exception ex)
            {

            }
            //MGR_UPD_SDT27062019  Updated by NR
            hashPara.Add("@IsActive", "1");
            hashPara.Add("@CreatedBy", "1");
            hashPara.Add("@CreatedIP", IpAddress);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        /// <summary>
        /// 8400000241
        /// Add button for Deactive User fun on this event triggred
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public int UpdateStatuInDB(String LoginName)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_SaveUserStatus";
            int result = 0;

            hashPara.Add("@LoginName", LoginName);
            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        /// <summary>
        /// MGR_UPD_SDT27062019
        /// To Create New User/RM/NSM
        /// </summary>
        /// <param name="ObjADUser"></param>
        /// <returns></returns>
        public int SaveRMNSMDetail(ADUserDetail ObjADUser, string UserId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_InsUpd_User_RM_NSM_Master";
            int result = 0;
            string IpAddress = new Utility().GetIpAddress();

            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@Profile_Id", "4");
            hashPara.Add("@UserName", ObjADUser.LoginName);
            hashPara.Add("@Password", Encryption.Encrypt("mwt"));
            hashPara.Add("@Full_Name", ObjADUser.FirstName + " " + ObjADUser.LastName);
            hashPara.Add("@EmailId", ObjADUser.EmailAddress);

            try
            {
                if (ObjADUser.ManagerName != null && ObjADUser.ManagerName != "")
                {
                    hashPara.Add("@ReportingTo_Name", ObjADUser.ManagerName);
                    hashPara.Add("@ReportingTo_Email", ObjADUser.Manager.EmailAddress);
                }
                else
                {
                    hashPara.Add("@ReportingTo_Name", "");
                    hashPara.Add("@ReportingTo_Email", "");
                }
            }
            catch
            {

            }

            //hashPara.Add("@ReportingTo_Name", ObjADUser.ManagerName);
            //hashPara.Add("@ReportingTo_Email", ObjADUser.Manager.EmailAddress);

            hashPara.Add("@IsActive", "1");
            hashPara.Add("@CreatedBy", "1");
            hashPara.Add("@CreatedIP", IpAddress);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }


        public User_Master GetUserDataByUserId(string UserId)
        {
            User_Master ObjUser_Master = new User_Master();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetUserDataByUserId";
            DataSet ds;
            hashPara.Add("@User_Id", UserId);
            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjUser_Master.User_Id = Convert.ToInt32(dt.Rows[0]["User_Id"].ToString());
                        ObjUser_Master.Profile_Id = Convert.ToInt32(dt.Rows[0]["Profile_Id"].ToString());
                        ObjUser_Master.Country_Id = dt.Rows[0]["Country_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString());
                        ObjUser_Master.Department_Id = dt.Rows[0]["Department_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Department_Id"].ToString());
                        ObjUser_Master.UserName = dt.Rows[0]["UserName"].ToString();
                        ObjUser_Master.Password = dt.Rows[0]["Password"].ToString();
                        ObjUser_Master.Full_Name = dt.Rows[0]["Full_Name"].ToString();
                        ObjUser_Master.EmailId = dt.Rows[0]["EmailId"].ToString();
                        ObjUser_Master.ReportingTo_Name = dt.Rows[0]["ReportingTo_Name"].ToString();
                        ObjUser_Master.ReportingTo_Email = dt.Rows[0]["ReportingTo_Email"].ToString();
                        ObjUser_Master.Location = dt.Rows[0]["Location"].ToString();
                        ObjUser_Master.Cost_Centre = dt.Rows[0]["Cost_Centre"].ToString();
                        ObjUser_Master.ContactNo = dt.Rows[0]["ContactNo"].ToString();
                        ObjUser_Master.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjUser_Master.Department_Name = dt.Rows[0]["Department_Name"].ToString();
                    }
                }
                return ObjUser_Master;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }

        }

        public int SaveUserLocationDetails(User_Master ObjUser_Master)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_SaveUserLocationDetails";
            int result = 0;

            hashPara.Add("@User_Id", ObjUser_Master.User_Id);
            hashPara.Add("@Full_Name", ObjUser_Master.Full_Name);
            hashPara.Add("@EmailId", ObjUser_Master.EmailId);
            hashPara.Add("@Location", ObjUser_Master.Location);
            hashPara.Add("@Cost_Centre", ObjUser_Master.Cost_Centre);
            hashPara.Add("@ContactNo", ObjUser_Master.ContactNo);
            hashPara.Add("@Plant_Id", ObjUser_Master.Plant_Id);
            hashPara.Add("@CreatorUserId", ObjUser_Master.UserId);
            hashPara.Add("@CreatorUserIp", ObjUser_Master.IPAddress);
            hashPara.Add("@Department_Name", ObjUser_Master.Department_Name);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        public DataSet ReadApprovers(string module, string dept)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetApproversData";
            DataSet ds;

            hashPara.Add("@Module_Id", module);
            hashPara.Add("@Dept_Id", dept);
            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public DataSet ReadApproverDetailByAppId(string appID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetApproversDataByAppID";
            DataSet ds;

            hashPara.Add("@AuthID", appID);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public object ReadApproverNames(string deptId = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetApproverNames";
            DataSet ds;

            hashPara.Add("@DeptId", deptId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public object ReadReferenceCodes(string workflowType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetReferenceIDs";
            hashPara.Add("@workflowType", workflowType);
            DataSet ds;

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal = null;
            }
        }

        public int AssignDualRolesByUserName(string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_AssignDualRoles";
            int result = 0;
            string IpAddress = new Utility().GetIpAddress();

            hashPara.Add("@User_Id", userId);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        //Email Redirection Start
        public DataSet GetRequestorDetailsByMasterHeaderID(string masterHeaderID, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet dstData = new DataSet();
            string procName = "pr_GetRequestorDetailsByMasterHeaderID";

            try
            {
                objDal.OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objDal.cnnConnection;
                cmd.CommandText = procName;
                //Srinidhi
                cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                cmd.Parameters.Add(new SqlParameter("@Master_Header_Id", masterHeaderID));
                cmd.Parameters.Add(new SqlParameter("@User_Id", userId));

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dstData, "ReqData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
            }
            return dstData;
        }
        //Email Redirection End

        //New method for Save (Approve request from backend) by Swati on 07/06/2018


        //************* This code is added by Swati Mohandas for Approve Request from Backend **************//
        public int Save(Approve ObjApprove)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_From_Backend";
            int result = 0;

            hashPara.Add("@Request_No", ObjApprove.Request_No);
            hashPara.Add("@Department", ObjApprove.Department);
            hashPara.Add("@ApproveBy", ObjApprove.ApproveBy);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }
        //************* This code is added by Swati Mohandas for Approve Request from Backend **************//

        //Start Addition By Swati M Date: 16.10.2018
        public DataSet GetIfDivExist(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Check_If_Division_Type_Selected";
            hashPara.Add("@MasterHeaderId", MasterHeaderId);

            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet ReadSectionTabs_Customer(string userId, string departmentId, string MasterHeaderId, string DivisionType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Generate_Section_Tabs_Customer";
            hashPara.Add("@UserId", userId);
            hashPara.Add("@DepartmentId", departmentId);
            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@DivisionType", DivisionType);

            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }
        //End Addition By Swati M Date: 16.10.2018

        //Start Addition By Swati M Date: 11.12.2018
        public string UpdateReportingManager(string UserId, string ManagerName)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_UpdateReportingManager";
            //DataSet ds;
            int result = 0;
            hashPara.Add("@sUserADId", UserId);
            hashPara.Add("@sRMADId", ManagerName);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                objDal = null;
            }
        }
        //End Addition By Swati M Date: 11.12.2018

        //Start addition by Swati on 09.01.2018
        public DataSet ReadSectionTabs_PriceMaster(string userId, string departmentId, string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Generate_Section_Tabs_PriceMaster";
            hashPara.Add("@UserId", userId);
            hashPara.Add("@DepartmentId", departmentId);
            hashPara.Add("@MasterHeaderId", MasterHeaderId);

            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }
        //End


        public int SaveUserLog(string UserId, string flag)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_InsUpd_User_Master_Log";
            int result = 0;
            string IpAddress = new Utility().GetIpAddress();

            hashPara.Add("@User_Id", UserId);
            hashPara.Add("@CreatedIP", IpAddress);
            hashPara.Add("@flag", flag);
            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        public int SaveFB(Approve ObjApprove)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string IpAddress = new Utility().GetIpAddress();
            //string procName = "pr_Approve_Request_From_Backend_New";
            string procName = "sp_Approve_Request_From_BE";
            int result = 0;

            hashPara.Add("@Request_No", ObjApprove.Request_No);
            hashPara.Add("@ApprovedByDept", ObjApprove.Department);
            hashPara.Add("@ApproveBy", ObjApprove.ApproveBy);
            hashPara.Add("@sRemarks", ObjApprove.sRemarks);
            hashPara.Add("@stxtRemarks", ObjApprove.stxtRemarks);
            hashPara.Add("@Ipaddress", IpAddress);
            hashPara.Add("@ApproveByAdmin", ObjApprove.ApproveByAdmin);

            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }


        public int UpdateBOMRPFB(UpdateBOMFB ObjUpdateBOMFB)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string IpAddress = new Utility().GetIpAddress();
            string procName = "sp_Update_BOM_details_FB";
            int result = 0;

            hashPara.Add("@Request_No", ObjUpdateBOMFB.Request_No);
            hashPara.Add("@sRemarks", ObjUpdateBOMFB.sRemarks);
            hashPara.Add("@stxtRemarks", ObjUpdateBOMFB.stxtRemarks);
            hashPara.Add("@sCreatedIp", IpAddress);
            hashPara.Add("@sCreatedBy", ObjUpdateBOMFB.CreatedBy);
            hashPara.Add("@AltBOMCur", ObjUpdateBOMFB.AltBOMCur);
            hashPara.Add("@Recipe_GroupCur", ObjUpdateBOMFB.Recipe_GroupCur);
            hashPara.Add("@ProdVersionNoCur", ObjUpdateBOMFB.ProdVersionNoCur);
            hashPara.Add("@GroupCntrCur", ObjUpdateBOMFB.GroupCntrCur);
            hashPara.Add("@SAP_BOM_NoCur", ObjUpdateBOMFB.SAP_BOM_NoCur);


            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }


        public int UpdateSAPUserPass(UpdatePassword ObjUpdatePassword)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string IpAddress = new Utility().GetIpAddress();
            string procName = "sp_Update_M_SAPDetails_TB";
            int result = 0;

            hashPara.Add("@SAPUserID", ObjUpdatePassword.SAPUserID);
            hashPara.Add("@SAPPassword", ObjUpdatePassword.SAPPassword);
            //hashPara.Add("@SAPConfirmPassword", ObjUpdatePassword.SAPConfirmPassword);
            hashPara.Add("@sCreatedIp", IpAddress);
            hashPara.Add("@sCreatedBy", ObjUpdatePassword.CreatedBy);
            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

    }
}