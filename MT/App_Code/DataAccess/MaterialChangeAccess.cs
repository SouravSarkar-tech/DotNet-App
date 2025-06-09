using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for MaterialChangeAccess
/// </summary>
public class MaterialChangeAccess
{
    public MaterialChangeAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public int Save(MaterialChange ObjMaterialChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Material_Change";
        int result = 0;


        hashPara.Add("@Material_Change_Id", ObjMaterialChange.Material_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjMaterialChange.Master_Header_Id);
        hashPara.Add("@Material_Code", ObjMaterialChange.Material_Code);
        hashPara.Add("@Material_Desc", ObjMaterialChange.Material_Desc);
        hashPara.Add("@Plant_Id", ObjMaterialChange.Plant_Id);
        hashPara.Add("@Storage_Location", ObjMaterialChange.Storage_Location);
        hashPara.Add("@Material_Acc_Grp", ObjMaterialChange.Material_Acc_Grp);
        hashPara.Add("@Sales_Organisation_Id", ObjMaterialChange.Sales_Organisation_Id);
        hashPara.Add("@Distribution_Channel_Id", ObjMaterialChange.Distribution_Channel_Id);
        //hashPara.Add("@Division_Id", ObjMaterialChange.Division_Id);

        hashPara.Add("@Material_Change_Detail_Id", ObjMaterialChange.Material_Change_Detail_Id);
        hashPara.Add("@Section_Id", ObjMaterialChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjMaterialChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjMaterialChange.Old_Value);
        hashPara.Add("@New_Value", ObjMaterialChange.New_Value);

        hashPara.Add("@IsActive", ObjMaterialChange.IsActive);
        hashPara.Add("@UserId", ObjMaterialChange.UserId);
        hashPara.Add("@UserIp", ObjMaterialChange.IPAddress);

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
    /// MSC_8300001775
    /// </summary>
    /// <param name="ObjMaterialChange"></param>
    /// <returns></returns>
    public int SaveMass(MaterialChange ObjMaterialChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Material_Change_Mass";
        int result = 0;


        hashPara.Add("@Material_Change_Id", ObjMaterialChange.Material_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjMaterialChange.Master_Header_Id);
        hashPara.Add("@Material_Code", ObjMaterialChange.Material_Code);
        hashPara.Add("@Material_Desc", ObjMaterialChange.Material_Desc);
        hashPara.Add("@Plant_Id", ObjMaterialChange.Plant_Id);
        hashPara.Add("@Storage_Location", ObjMaterialChange.Storage_Location);
        hashPara.Add("@Material_Acc_Grp", ObjMaterialChange.Material_Acc_Grp);
        hashPara.Add("@Sales_Organisation_Id", ObjMaterialChange.Sales_Organisation_Id);
        hashPara.Add("@Distribution_Channel_Id", ObjMaterialChange.Distribution_Channel_Id);  
        hashPara.Add("@IsActive", ObjMaterialChange.IsActive);
        hashPara.Add("@UserId", ObjMaterialChange.UserId);
        hashPara.Add("@UserIp", ObjMaterialChange.IPAddress);

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
    /// //CTRL_SUB_SDT18112019 Added by NR
    /// </summary>
    /// <param name="MasterHeaderId"></param>
    /// <returns></returns>
    public DataSet GetRefModule(int MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_RefModule_By_MasterHeaderId";
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

    public int Save(MaterialChangeDetail ObjMaterialChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Material_Change_Detail";
        int result = 0;


        hashPara.Add("@Material_Change_Detail_Id", ObjMaterialChange.Material_Change_Detail_Id);
        hashPara.Add("@Material_Change_Id", ObjMaterialChange.Material_Change_Id);
        hashPara.Add("@Section_Id", ObjMaterialChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjMaterialChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjMaterialChange.Old_Value);
        hashPara.Add("@New_Value", ObjMaterialChange.New_Value);
        hashPara.Add("@IsActive", ObjMaterialChange.IsActive);
        hashPara.Add("@UserId", ObjMaterialChange.UserId);
        hashPara.Add("@UserIp", ObjMaterialChange.IPAddress);

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

    public MaterialChange GetMaterialChange(int Material_Change_Id)
    {
        MaterialChange ObjMaterialChange = new MaterialChange();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Change_By_MaterialChangeId";
        DataSet ds;

        hashPara.Add("@Material_Change_Id", Material_Change_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjMaterialChange.Material_Change_Id = Convert.ToInt32(dt.Rows[0]["Material_Change_Id"].ToString());
                    ObjMaterialChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjMaterialChange.Material_Code = dt.Rows[0]["Material_Code"].ToString();
                    ObjMaterialChange.Material_Desc = dt.Rows[0]["Material_Desc"].ToString();
                    ObjMaterialChange.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                    ObjMaterialChange.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                    ObjMaterialChange.Material_Acc_Grp = dt.Rows[0]["Material_Acc_Grp"].ToString();
                    ObjMaterialChange.Sales_Organisation_Id = dt.Rows[0]["Sales_Organisation_Id"].ToString();
                    ObjMaterialChange.Distribution_Channel_Id = dt.Rows[0]["Distribution_Channel_Id"].ToString();
                    //ObjMaterialChange.Division_Id = dt.Rows[0]["Division_Id"].ToString();
                }
            }
            return ObjMaterialChange;
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

    public MaterialChangeDetail GetMaterialChangeDetail(int Material_Change_Detail_Id)
    {
        MaterialChangeDetail ObjMaterialChange = new MaterialChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Change_Detail_By_MaterialChangeDetailId";
        DataSet ds;

        hashPara.Add("@Material_Change_Detail_Id", Material_Change_Detail_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjMaterialChange.Material_Change_Detail_Id = Convert.ToInt32(dt.Rows[0]["Material_Change_Detail_Id"].ToString());
                    ObjMaterialChange.Material_Change_Id = Convert.ToInt32(dt.Rows[0]["Material_Change_Id"].ToString());
                    ObjMaterialChange.Section_Id = Convert.ToInt32(dt.Rows[0]["Section_Id"].ToString());
                    ObjMaterialChange.Section_Feild_Master_Id = Convert.ToInt32(dt.Rows[0]["Section_Feild_Master_Id"].ToString());
                    ObjMaterialChange.Old_Value = dt.Rows[0]["Old_Value"].ToString();
                    ObjMaterialChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                    ObjMaterialChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                }
            }
            return ObjMaterialChange;
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

    public int DeleteMaterialChangeDetail(string Material_Change_Detail_Id)
    {
        MaterialChangeDetail ObjMaterialChange = new MaterialChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Delete_Material_Change_Detail_By_MaterialChangeDetailId";
        int result = 0;

        hashPara.Add("@Material_Change_Detail_Id", Material_Change_Detail_Id);

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

    public DataSet GetMaterialChangeData(string MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Change_By_MasterHeaderId";
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

    public DataSet GetMaterialChangeDetailData(int Material_Change_Id)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Change_Detail_By_MaterialChangeId";
        DataSet ds;

        hashPara.Add("@Material_Change_Id", Material_Change_Id);

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

    public int SaveImport(MaterialChange ObjMaterialChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Material_Change_Import";
        int result = 0;


        hashPara.Add("@Material_Change_Id", ObjMaterialChange.Material_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjMaterialChange.Master_Header_Id);
        hashPara.Add("@Material_Code", ObjMaterialChange.Material_Code);
        hashPara.Add("@Material_Desc", ObjMaterialChange.Material_Desc);
        hashPara.Add("@Plant_Id", ObjMaterialChange.Plant_Id);
        hashPara.Add("@Storage_Location", ObjMaterialChange.Storage_Location);
        hashPara.Add("@Material_Acc_Grp", ObjMaterialChange.Material_Acc_Grp);
        hashPara.Add("@Sales_Organisation_Id", ObjMaterialChange.Sales_Organisation_Id);
        hashPara.Add("@Distribution_Channel_Id", ObjMaterialChange.Distribution_Channel_Id);


        hashPara.Add("@Material_Change_Detail_Id", ObjMaterialChange.Material_Change_Detail_Id);
        hashPara.Add("@Section_Id", ObjMaterialChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjMaterialChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjMaterialChange.Old_Value);
        hashPara.Add("@New_Value", ObjMaterialChange.New_Value);

        hashPara.Add("@IsActive", ObjMaterialChange.IsActive);
        hashPara.Add("@UserId", ObjMaterialChange.UserId);
        hashPara.Add("@UserIp", ObjMaterialChange.IPAddress);

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