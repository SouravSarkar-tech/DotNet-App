using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GLExtensionAccess
/// </summary>

namespace Accenture.MWT.DataAccess
{
    public class GLExtensionAccess
    {
        public GLExtensionAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int Save(GLExtension ObjGLExtension)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_GL_Extension";
            int result = 0;

            hashPara.Add("@GL_Extension_Id", ObjGLExtension.GL_Extension_Id);
            hashPara.Add("@Master_Header_Id", ObjGLExtension.Master_Header_Id);
            hashPara.Add("@GL_Code", ObjGLExtension.GL_Code);
            hashPara.Add("@Company_Code", ObjGLExtension.Company_Code);
            hashPara.Add("@Ref_Company_Code", ObjGLExtension.Ref_Company_Code);

            hashPara.Add("@Remarks", ObjGLExtension.Remarks);

            hashPara.Add("@UserId", ObjGLExtension.UserId);
            hashPara.Add("@UserIp", ObjGLExtension.IPAddress);

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

        public GLExtension GetGLExtension(int GL_Extension_Id)
        {
            GLExtension ObjGLExtension = new GLExtension();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_GLExtension_By_GLExtensionId";
            DataSet ds;

            hashPara.Add("@GL_Extension_Id", GL_Extension_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjGLExtension.GL_Extension_Id = dt.Rows[0]["GL_Extension_Id"].ToString() == "" ? GL_Extension_Id : Convert.ToInt32(dt.Rows[0]["GL_Extension_Id"].ToString());
                        ObjGLExtension.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjGLExtension.GL_Code = dt.Rows[0]["GL_Code"].ToString();
                        ObjGLExtension.GLGroup = dt.Rows[0]["GLGroup"].ToString();
                        ObjGLExtension.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjGLExtension.Ref_Company_Code = dt.Rows[0]["Ref_Company_Code"].ToString();
                        ObjGLExtension.Company_Name = dt.Rows[0]["Company_Name"].ToString();
                        ObjGLExtension.Remarks = dt.Rows[0]["Remarks"].ToString();

                    }
                }
                return ObjGLExtension;
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


        public GLExtension GetGLExtCompany(int MasterHeaderId)
        {
            GLExtension ObjGLExtension = new GLExtension();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_GLExtension_By_GLMasterHeaderId";
            DataSet ds;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        ObjGLExtension.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjGLExtension.Company_Code = dt.Rows[0]["Company_Code"].ToString();

                    }
                }
                return ObjGLExtension;
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



        public DataSet GetGLExtensionData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_GLExtension_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);

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

        public int DeleteGLExtensionData(int GL_Extension_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_GLExtension_By_GL_Extension_Id";
            int result = 0;

            hashPara.Add("@GL_Extension_Id", GL_Extension_Id);

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