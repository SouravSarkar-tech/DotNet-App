using System;
using System.Data;
using System.Web;
using System.Web.Security;
using Accenture.MWT.DataAccess;


public class MemberShipProvider : MembershipProvider
{

    /// <summary>
    /// Verifies that the specified user name and password exist in the data source.
    /// </summary>
    /// <param name="username">The name of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <author>Daya Shankar Sharma</author>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>

    public override bool ValidateUser(string username, string password)
    {
        UserAccess userAccess = new UserAccess();
        DataSet dstData = new DataSet();
        dstData = userAccess.ValidateUser(username, password);
        
        if (dstData.Tables[0].Rows.Count == 1)
        {
            HttpContext.Current.Session.Add(StaticKeys.LoggedIn_User_Id, dstData.Tables[0].Rows[0]["User_Id"].ToString());
            HttpContext.Current.Session.Add(StaticKeys.LoggedIn_User_Profile_Id, dstData.Tables[0].Rows[0]["Profile_Id"].ToString());
            HttpContext.Current.Session.Add(StaticKeys.LoggedIn_User_Profile, dstData.Tables[0].Rows[0]["Profile_Name"].ToString());
            HttpContext.Current.Session.Add(StaticKeys.LoggedIn_User_FullName, dstData.Tables[0].Rows[0]["Full_Name"].ToString());
            HttpContext.Current.Session.Add(StaticKeys.LoggedIn_User_LastLogin, dstData.Tables[0].Rows[0]["Last_Login_On"].ToString());
            HttpContext.Current.Session.Add(StaticKeys.LoggedIn_User_DeptId, dstData.Tables[0].Rows[0]["Department_Id"].ToString());
            return true;
        }
        return false;
    }

    public override string ApplicationName
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
        get { throw new NotImplementedException(); }
    }

    public override bool EnablePasswordRetrieval
    {
        get { throw new NotImplementedException(); }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
        get { throw new NotImplementedException(); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get { throw new NotImplementedException(); }
    }

    public override int MinRequiredPasswordLength
    {
        get { throw new NotImplementedException(); }
    }

    public override int PasswordAttemptWindow
    {
        get { throw new NotImplementedException(); }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
        get { throw new NotImplementedException(); }
    }

    public override string PasswordStrengthRegularExpression
    {
        get { throw new NotImplementedException(); }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get { throw new NotImplementedException(); }
    }

    public override bool RequiresUniqueEmail
    {
        get { throw new NotImplementedException(); }
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }

}

