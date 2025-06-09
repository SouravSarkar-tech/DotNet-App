using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CustomerChangeAccess
/// </summary>
public class CustomerChangeAccess
{
    public CustomerChangeAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public int Save(CustomerChange ObjCustomerChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Customer_Change";
        int result = 0;


        hashPara.Add("@Customer_Change_Id", ObjCustomerChange.Customer_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjCustomerChange.Master_Header_Id);
        hashPara.Add("@Customer_Code", ObjCustomerChange.Customer_Code);
        hashPara.Add("@Customer_Desc", ObjCustomerChange.Customer_Desc);
        hashPara.Add("@Company_Code", ObjCustomerChange.Company_Code);
        hashPara.Add("@Customer_Acc_Grp", ObjCustomerChange.Customer_Acc_Grp);
        hashPara.Add("@Sales_Organisation_Id", ObjCustomerChange.Sales_Organisation_Id);
        hashPara.Add("@Distribution_Channel_Id", ObjCustomerChange.Distribution_Channel_Id);
        hashPara.Add("@Division_Id", ObjCustomerChange.Division_Id);

        hashPara.Add("@Customer_Change_Detail_Id", ObjCustomerChange.Customer_Change_Detail_Id);
        hashPara.Add("@Section_Id", ObjCustomerChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjCustomerChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjCustomerChange.Old_Value);
        hashPara.Add("@New_Value", ObjCustomerChange.New_Value);

        hashPara.Add("@IsActive", ObjCustomerChange.IsActive);
        hashPara.Add("@UserId", ObjCustomerChange.UserId);
        hashPara.Add("@UserIp", ObjCustomerChange.IPAddress);

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

    public int Save(CustomerChangeDetail ObjCustomerChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Customer_Change_Detail";
        int result = 0;


        hashPara.Add("@Customer_Change_Detail_Id", ObjCustomerChange.Customer_Change_Detail_Id);
        hashPara.Add("@Customer_Change_Id", ObjCustomerChange.Customer_Change_Id);
        hashPara.Add("@Section_Id", ObjCustomerChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjCustomerChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjCustomerChange.Old_Value);
        hashPara.Add("@New_Value", ObjCustomerChange.New_Value);
        hashPara.Add("@IsActive", ObjCustomerChange.IsActive);
        hashPara.Add("@UserId", ObjCustomerChange.UserId);
        hashPara.Add("@UserIp", ObjCustomerChange.IPAddress);

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


    public CustomerChange GetCustomerChange(int Customer_Change_Id)
    {
        CustomerChange ObjCustomerChange = new CustomerChange();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Customer_Change_By_CustomerChangeId";
        DataSet ds;

        hashPara.Add("@Customer_Change_Id", Customer_Change_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjCustomerChange.Customer_Change_Id = Convert.ToInt32(dt.Rows[0]["Customer_Change_Id"].ToString());
                    ObjCustomerChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjCustomerChange.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                    ObjCustomerChange.Customer_Desc = dt.Rows[0]["Customer_Desc"].ToString();
                    ObjCustomerChange.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                    ObjCustomerChange.Customer_Acc_Grp = dt.Rows[0]["Customer_Acc_Grp"].ToString();
                    ObjCustomerChange.Sales_Organisation_Id = dt.Rows[0]["Sales_Organisation_Id"].ToString();
                    ObjCustomerChange.Distribution_Channel_Id = dt.Rows[0]["Distribution_Channel_Id"].ToString();
                    ObjCustomerChange.Division_Id = dt.Rows[0]["Division_Id"].ToString();
                }
            }
            return ObjCustomerChange;
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

    public CustomerChangeDetail GetCustomerChangeDetail(int Customer_Change_Detail_Id)
    {
        CustomerChangeDetail ObjCustomerChange = new CustomerChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Customer_Change_Detail_By_CustomerChangeDetailId";
        DataSet ds;

        hashPara.Add("@Customer_Change_Detail_Id", Customer_Change_Detail_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjCustomerChange.Customer_Change_Detail_Id = Convert.ToInt32(dt.Rows[0]["Customer_Change_Detail_Id"].ToString());
                    ObjCustomerChange.Customer_Change_Id = Convert.ToInt32(dt.Rows[0]["Customer_Change_Id"].ToString());
                    ObjCustomerChange.Section_Id = Convert.ToInt32(dt.Rows[0]["Section_Id"].ToString());
                    ObjCustomerChange.Section_Feild_Master_Id = Convert.ToInt32(dt.Rows[0]["Section_Feild_Master_Id"].ToString());
                    ObjCustomerChange.Old_Value = dt.Rows[0]["Old_Value"].ToString();
                    ObjCustomerChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                    ObjCustomerChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                }
            }
            return ObjCustomerChange;
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

    public int DeleteCustomerChangeDetail(string Customer_Change_Detail_Id)
    {
        CustomerChangeDetail ObjCustomerChange = new CustomerChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Delete_Customer_Change_Detail_By_CustomerChangeDetailId";
        int result = 0;

        hashPara.Add("@Customer_Change_Detail_Id", Customer_Change_Detail_Id);

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

    public DataSet GetCustomerChangeData(string MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Customer_Change_By_MasterHeaderId";
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

    public DataSet GetCustomerChangeDetailData(int Customer_Change_Id)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Customer_Change_Detail_By_CustomerChangeId";
        DataSet ds;

        hashPara.Add("@Customer_Change_Id", Customer_Change_Id);

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