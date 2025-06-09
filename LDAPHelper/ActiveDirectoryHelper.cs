using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.DirectoryServices;
using System.Data;
using System.Collections;
using System.IO;

namespace Accenture.MWT.LDAPHelper
{
    public class ActiveDirectoryHelper
    {


        private DirectoryEntry _directoryEntry = null;

        private DirectoryEntry SearchRoot
        {
            get
            {
                if (_directoryEntry == null)
                {
                    _directoryEntry = new DirectoryEntry(LDAPPath, LDAPUser, LDAPPassword, AuthenticationTypes.Secure);
                }
                return _directoryEntry;
            }
        }

        private String LDAPPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPPath"];
            }
        }

        private String LDAPUser
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPUser"];
            }
        }

        private String LDAPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPPassword"];
            }
        }

        private String LDAPDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPDomain"];
            }
        }

        internal ADUserDetail GetUserByFullName(String userName)
        {
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(cn=" + userName + "))";
                SearchResult results = directorySearch.FindOne();

                if (results != null)
                {
                    DirectoryEntry user = new DirectoryEntry(results.Path, LDAPUser, LDAPPassword);
                    return ADUserDetail.GetUser(user);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        public ADUserDetail GetUserByLoginName(String userName)
        {
            string sdate = "";
            try
            {
                DateTime date = System.DateTime.Now;
                sdate = date.ToString("dd/MM/yyyy");
                sdate = sdate.Replace(@"/", "");
                //WriteLog("Exception_IsValidUserAD" + sdate + ".txt", "End of execution SSO");
            }
            catch(Exception ex)
            {
                //WriteLog("Exception_IsValidUserAD" + sdate + ".txt", "End of execution SSO");
                //DT07042022
                WriteLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of Date" + ex.ToString());
            }

            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
                SearchResult results = directorySearch.FindOne();

                if (results != null)
                {
                    //Print result in log DT07042022
                    WriteLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception of results" + results.ToString());

                    DirectoryEntry user = new DirectoryEntry(results.Path, LDAPUser, LDAPPassword);
                    return ADUserDetail.GetUser(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                //Print Exception in log DT07042022
                WriteLog("Exception_IsValidUserAD" + sdate + ".txt", "Exception GetUserByLoginName" + ex.ToString());


                throw;
            }
        }


        /// <summary>
        /// This function will take a DL or Group name and return list of users
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public List<ADUserDetail> GetUserFromGroup(String groupName)
        {
            List<ADUserDetail> userlist = new List<ADUserDetail>();
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=group)(SAMAccountName=" + groupName + "))";
                SearchResult results = directorySearch.FindOne();
                if (results != null)
                {

                    DirectoryEntry deGroup = new DirectoryEntry(results.Path, LDAPUser, LDAPPassword);
                    System.DirectoryServices.PropertyCollection pColl = deGroup.Properties;
                    int count = pColl["member"].Count;


                    for (int i = 0; i < count; i++)
                    {
                        string respath = results.Path;
                        string[] pathnavigate = respath.Split("CN".ToCharArray());
                        respath = pathnavigate[0];
                        string objpath = pColl["member"][i].ToString();
                        string path = respath + objpath;


                        DirectoryEntry user = new DirectoryEntry(path, LDAPUser, LDAPPassword);
                        ADUserDetail userobj = ADUserDetail.GetUser(user);
                        userlist.Add(userobj);
                        user.Close();
                    }
                }
                return userlist;
            }
            catch
            {
                return userlist;
            }

        }

        #region Get user with First Name

        public List<ADUserDetail> GetUsersByFirstName(string fName)
        {

            //UserProfile user;
            List<ADUserDetail> userlist = new List<ADUserDetail>();
            string filter = "";

            _directoryEntry = null;
            DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
            directorySearch.Asynchronous = true;
            directorySearch.CacheResults = true;
            //filter = string.Format("(givenName={0}*", fName);SAMAccountName
            filter = "(&(objectClass=user)(objectCategory=person)(SAMAccountName=" + fName + "*))";


            directorySearch.Filter = filter;

            SearchResultCollection userCollection = directorySearch.FindAll();
            foreach (SearchResult users in userCollection)
            {
                DirectoryEntry userEntry = new DirectoryEntry(users.Path, LDAPUser, LDAPPassword);
                ADUserDetail userInfo = ADUserDetail.GetUser(userEntry);

                userlist.Add(userInfo);

            }

            directorySearch.Filter = "(&(objectClass=group)(SAMAccountName=" + fName + "*))";
            SearchResultCollection results = directorySearch.FindAll();
            if (results != null)
            {

                foreach (SearchResult r in results)
                {
                    DirectoryEntry deGroup = new DirectoryEntry(r.Path, LDAPUser, LDAPPassword);

                    ADUserDetail agroup = ADUserDetail.GetUser(deGroup);
                    userlist.Add(agroup);
                }

            }
            return userlist;
        }

        #endregion


        #region AddUserToGroup
        public bool AddUserToGroup(string userlogin, string groupName)
        {
            try
            {
                _directoryEntry = null;
                ADManager admanager = new ADManager(LDAPDomain, LDAPUser, LDAPPassword);
                admanager.AddUserToGroup(userlogin, groupName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region RemoveUserToGroup
        public bool RemoveUserToGroup(string userlogin, string groupName)
        {
            try
            {
                _directoryEntry = null;
                ADManager admanager = new ADManager("xxx", LDAPUser, LDAPPassword);
                admanager.RemoveUserFromGroup(userlogin, groupName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region Authenticate

        public bool IsAuthenticated(String domain, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(LDAPPath, domainAndUsername, pwd);

            try
            {
                //Bind to the native AdsObject to force authentication.			
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                //search.PropertiesToLoad.Add("manager");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                //_path = result.Path;
                //_filterAttribute = (String)result.Properties["cn"][0];
                return true;

            }
            catch
            {
                return false;
            }


        }

        //public String GetGroups()
        //{
        //    DirectorySearcher search = new DirectorySearcher(_path);
        //    search.Filter = "(cn=" + _filterAttribute + ")";
        //    search.PropertiesToLoad.Add("memberOf");
        //    StringBuilder groupNames = new StringBuilder();

        //    try
        //    {
        //        SearchResult result = search.FindOne();

        //        int propertyCount = result.Properties["memberOf"].Count;

        //        String dn;
        //        int equalsIndex, commaIndex;

        //        for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
        //        {
        //            dn = (String)result.Properties["memberOf"][propertyCounter];

        //            equalsIndex = dn.IndexOf("=", 1);
        //            commaIndex = dn.IndexOf(",", 1);
        //            if (-1 == equalsIndex)
        //            {
        //                return null;
        //            }

        //            groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
        //            groupNames.Append("|");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error obtaining group names. " + ex.Message);
        //    }
        //    return groupNames.ToString();
        //}


        #endregion




        public void WriteLog(string strFileName, string strMessage)
        {
            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", "C:\\ProsolLog", strFileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage + ":" + System.DateTime.Now + "\n");
                objStreamWriter.Close();
                objFilestream.Close();
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
    }

}
