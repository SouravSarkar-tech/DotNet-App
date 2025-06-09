using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for GLChangeAccess
/// </summary>
public class GLChangeAccess
{
    public GLChangeAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Save(GLChange ObjGLChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_GLMaster_Change";
        int result = 0;


        hashPara.Add("@GL_Change_Id", ObjGLChange.GL_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjGLChange.Master_Header_Id);
        hashPara.Add("@GL_Code", ObjGLChange.GL_Code);
        hashPara.Add("@Company_Code", ObjGLChange.Company_Code);
        hashPara.Add("@Account_Group", ObjGLChange.Account_Group);
        hashPara.Add("@GL_Desc", ObjGLChange.GL_Desc);

        hashPara.Add("@GL_Change_Detail_Id", ObjGLChange.GL_Change_Detail_Id);
        hashPara.Add("@Section_Id", ObjGLChange.Section_Id);
        hashPara.Add("@Section_Field_Master_Id", ObjGLChange.Section_Field_Master_Id);
        hashPara.Add("@Old_Value", ObjGLChange.Old_Value);
        hashPara.Add("@New_Value", ObjGLChange.New_Value);

        hashPara.Add("@CreatedBy", ObjGLChange.UserId);
        hashPara.Add("@CreatedIP", ObjGLChange.IPAddress);

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

    public int Save(GLChangeDetail ObjGLChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_GL_Change_Detail";
        int result = 0;


        hashPara.Add("@GL_Change_Detail_Id", ObjGLChange.GL_Change_Detail_Id);
        hashPara.Add("@GL_Change_Id", ObjGLChange.GL_Change_Id);
        hashPara.Add("@Section_Id", ObjGLChange.Section_Id);
        hashPara.Add("@Section_Field_Master_Id", ObjGLChange.Section_Field_Master_Id);
        hashPara.Add("@Old_Value", ObjGLChange.Old_Value);
        hashPara.Add("@New_Value", ObjGLChange.New_Value);
        hashPara.Add("@CreatedBy", ObjGLChange.UserId);
        hashPara.Add("@CreatedIP", ObjGLChange.IPAddress);

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


    public GLChange GetGLChange(int GL_Change_Id)
    {
        GLChange ObjGLChange = new GLChange();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_GL_Change_By_GLChangeId";
        DataSet ds;

        hashPara.Add("@GL_Change_Id", GL_Change_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjGLChange.GL_Change_Id = Convert.ToInt32(dt.Rows[0]["GL_Change_Id"].ToString());
                    ObjGLChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjGLChange.GL_Code = dt.Rows[0]["GL_Code"].ToString();
                    ObjGLChange.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                    ObjGLChange.Account_Group = dt.Rows[0]["Account_Group"].ToString();
                    ObjGLChange.GL_Desc = dt.Rows[0]["GL_Desc"].ToString();
                }
            }
            return ObjGLChange;
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

    public GLChangeDetail GetGLChangeDetail(int Master_Header_Id, int GL_Change_Detail_Id)
    {
        GLChangeDetail ObjGLChange = new GLChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_GL_Change_Detail_By_GLChangeDetailId";
        DataSet ds;

        hashPara.Add("@Master_Header_Id", Master_Header_Id);
        hashPara.Add("@GL_Change_Detail_Id", GL_Change_Detail_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjGLChange.GL_Change_Detail_Id = Convert.ToInt32(dt.Rows[0]["GL_Change_Detail_Id"].ToString());
                    ObjGLChange.GL_Change_Id = Convert.ToInt32(dt.Rows[0]["GL_Change_Id"].ToString());
                    ObjGLChange.Section_Id = Convert.ToInt32(dt.Rows[0]["Section_Id"].ToString());
                    ObjGLChange.Section_Field_Master_Id = Convert.ToInt32(dt.Rows[0]["Section_Field_Master_Id"].ToString());
                    if(dt.Rows.Count > 0)
                    {
                        ObjGLChange.Field = dt.Rows[0]["Section_Field_Master_Id"].ToString();
                        ObjGLChange.Old_Value = dt.Rows[0]["Old_Value"].ToString();
                        ObjGLChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                    }
                    if (dt.Rows.Count > 1)
                    {
                        ObjGLChange.Field2 = dt.Rows[1]["Section_Field_Master_Id"].ToString();
                        ObjGLChange.Old_Value2 = dt.Rows[1]["Old_Value"].ToString();
                        ObjGLChange.New_Value2 = dt.Rows[1]["New_Value"].ToString();
                    }
                    if (dt.Rows.Count > 2)
                    {
                        ObjGLChange.Field3 = dt.Rows[2]["Section_Field_Master_Id"].ToString();
                        ObjGLChange.Old_Value3 = dt.Rows[2]["Old_Value"].ToString();
                        ObjGLChange.New_Value3 = dt.Rows[2]["New_Value"].ToString();
                    }
                    if (dt.Rows.Count > 3)
                    {
                        ObjGLChange.Field4 = dt.Rows[3]["Section_Field_Master_Id"].ToString();
                        ObjGLChange.Old_Value4 = dt.Rows[3]["Old_Value"].ToString();
                        ObjGLChange.New_Value4 = dt.Rows[3]["New_Value"].ToString();
                    }
                    if (dt.Rows.Count > 4)
                    {
                        ObjGLChange.Field5 = dt.Rows[4]["Section_Field_Master_Id"].ToString();
                        ObjGLChange.Old_Value5 = dt.Rows[4]["Old_Value"].ToString();
                        ObjGLChange.New_Value5 = dt.Rows[4]["New_Value"].ToString();
                    }
                }
            }
            return ObjGLChange;
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

    public int DeleteGLChangeDetail(string GL_Change_Detail_Id)
    {
        GLChangeDetail ObjGLChange = new GLChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Delete_GL_Change_Detail_By_GLChangeDetailId";
        int result = 0;

        hashPara.Add("@GL_Change_Detail_Id", GL_Change_Detail_Id);

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

    public DataSet GetGLChangeData(string MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_GL_Change_By_MasterHeaderId";
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

    public DataSet GetGLChangeDetailData(int GL_Change_Id)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_GL_Change_Detail_By_GLChangeId";
        DataSet ds;

        hashPara.Add("@GL_Change_Id", GL_Change_Id);

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
}