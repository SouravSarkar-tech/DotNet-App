using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Accenture.MWT.DataAccess;

public class Authorization
{

    public DataSet UserRights(string profileId, string menuId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        DataSet dstData = new DataSet();
        string query = "SELECT View_Right,Add_Right,Update_Right,Delete_Right FROM Profile_Menu_Mapping WHERE Profile_Id = " + profileId + " AND Menu_ID = " + menuId;
        try
        {
            dstData = objDal.FillDataSet(query, "Profile_Menu_Mapping");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dstData;
    }

    //public DataSet UserRights(string userId, string menuId)
    //{
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    DataSet dstData = new DataSet();
    //    string query = "SELECT View_Right,Add_Right,Update_Right,Delete_Right FROM User_Menu_Mapping WHERE User_Id = " + userId + " AND Menu_ID = " + menuId;
    //    try
    //    {
    //        dstData = objDal.FillDataSet(query, "User_Menu_Mapping");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return dstData;
    //}
}