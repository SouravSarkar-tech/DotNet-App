using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;
/// <summary>
/// Summary description for GLBlockAccess
/// </summary>

namespace Accenture.MWT.DataAccess
{
    public class GLBlockAccess
    {
        public GLBlockAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(GLBlock ObjGLBlock)
        {
            DataSet ds = new DataSet();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_GL_Block_Test";
            int result = 0;


            hashPara.Add("@GL_Block_Id", ObjGLBlock.GL_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjGLBlock.Master_Header_Id);
            hashPara.Add("@Blocked_For_Creation", ObjGLBlock.Blocked_For_Creation);
            hashPara.Add("@Blocked_For_Posting", ObjGLBlock.Blocked_For_Posting);
            hashPara.Add("@Blocked_For_Planning", ObjGLBlock.Blocked_For_Planning);
            hashPara.Add("@Blocked_For_Posting_CC", ObjGLBlock.Blocked_For_Posting_CC);
            hashPara.Add("@Remarks", ObjGLBlock.Remarks);
            hashPara.Add("@UserId", ObjGLBlock.UserId);
            hashPara.Add("@UserIp", ObjGLBlock.IPAddress);

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

        public GLBlock GetGLBlock(int Master_Header_Id)
        {
            GLBlock ObjGLBlock = new GLBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_GLBlock_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjGLBlock.GL_Block_Id = dt.Rows[0]["GL_Block_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["GL_Block_Id"].ToString());
                        ObjGLBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? Master_Header_Id : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjGLBlock.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjGLBlock.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjGLBlock.GL_Code = dt.Rows[0]["GL_Code"].ToString();
                        ObjGLBlock.Blocked_For_Creation = dt.Rows[0]["Blocked_For_Creation"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjGLBlock.Blocked_For_Posting = dt.Rows[0]["Blocked_For_Posting"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjGLBlock.Blocked_For_Planning = dt.Rows[0]["Blocked_For_Planning"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjGLBlock.Blocked_For_Posting_CC = dt.Rows[0]["Blocked_For_Posting_CC"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjGLBlock.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjGLBlock;
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
    }
}