using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for DashBoard
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class DashBoard
    {
        public DataTable GetDashBoardModuleList()
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetModuleListForDashBoard";
            ds = objDal.FillDataSet(procName);

            return ds.Tables[0];
        }

        public DataTable GetDashBoardByModuleId(string ModuleId,string User_ID = "")
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDashBoardData";
            hashPara.Add("@ModuleId", ModuleId);
            hashPara.Add("@User_Id", User_ID);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds.Tables[0];
        }

        public DataSet ReadProfileWiseModules(string profileId, string userId, string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Proc_Get_Profile_Wise_Module_Dash";
            hashPara.Add("@ProfileId", profileId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet ReadAllModules(string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string query = "SELECT Module_Id,Module_Name FROM M_Module WHERE ModuleType = '" + moduleType + "' AND IsActive = 'TRUE' ORDER BY Module_Name";
            return objDal.FillDataSet(query, "table");
        }

        public DataSet ReadPendingReqDetail(string moduleId, string seq, string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Proc_Dashboard_Search_Pending_Req_Detail";
            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@Seq", seq);
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }
    }
}