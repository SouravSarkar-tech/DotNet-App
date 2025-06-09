using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for VendorChangeAccess
/// </summary>
public class VendorChangeAccess
{
	public VendorChangeAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public int Save(VendorChange ObjVendorChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Vendor_Change";
        int result = 0;


        hashPara.Add("@Vendor_Change_Id", ObjVendorChange.Vendor_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjVendorChange.Master_Header_Id);
        hashPara.Add("@Customer_Code", ObjVendorChange.Customer_Code);
        hashPara.Add("@Company_Code", ObjVendorChange.Company_Code);
        hashPara.Add("@Vendor_Group", ObjVendorChange.Vendor_Group);
        hashPara.Add("@Purchase_Org", ObjVendorChange.Purchase_Org);
        hashPara.Add("@Vendor_Desc", ObjVendorChange.Vendor_Desc);

        hashPara.Add("@Vendor_Change_Detail_Id", ObjVendorChange.Vendor_Change_Detail_Id);
        hashPara.Add("@Section_Id", ObjVendorChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjVendorChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjVendorChange.Old_Value);
        hashPara.Add("@New_Value", ObjVendorChange.New_Value);
        hashPara.Add("@Remarks", ObjVendorChange.Remarks);

        hashPara.Add("@IsActive", ObjVendorChange.IsActive);
        hashPara.Add("@UserId", ObjVendorChange.UserId);
        hashPara.Add("@UserIp", ObjVendorChange.IPAddress);

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

    public int Save(VendorChangeDetail ObjVendorChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Vendor_Change_Detail";
        int result = 0;


        hashPara.Add("@Vendor_Change_Detail_Id", ObjVendorChange.Vendor_Change_Detail_Id);
        hashPara.Add("@Vendor_Change_Id", ObjVendorChange.Vendor_Change_Id);
        hashPara.Add("@Section_Id", ObjVendorChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjVendorChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjVendorChange.Old_Value);
        hashPara.Add("@New_Value", ObjVendorChange.New_Value);
        hashPara.Add("@Remarks", ObjVendorChange.Remarks);
        hashPara.Add("@IsActive", ObjVendorChange.IsActive);
        hashPara.Add("@UserId", ObjVendorChange.UserId);
        hashPara.Add("@UserIp", ObjVendorChange.IPAddress);

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


    public VendorChange GetVendorChange(int Vendor_Change_Id)
    {
        VendorChange ObjVendorChange = new VendorChange();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Vendor_Change_By_VendorChangeId";
        DataSet ds;

        hashPara.Add("@Vendor_Change_Id", Vendor_Change_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjVendorChange.Vendor_Change_Id = Convert.ToInt32(dt.Rows[0]["Vendor_Change_Id"].ToString());
                    ObjVendorChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjVendorChange.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                    ObjVendorChange.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                    ObjVendorChange.Vendor_Group = dt.Rows[0]["Vendor_Group"].ToString();
                    ObjVendorChange.Purchase_Org = dt.Rows[0]["Purchase_Org"].ToString();
                    ObjVendorChange.Vendor_Desc = dt.Rows[0]["Vendor_Desc"].ToString();
                }
            }
            return ObjVendorChange;
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

    public VendorChangeDetail GetVendorChangeDetail(int Vendor_Change_Detail_Id)
    {
        VendorChangeDetail ObjVendorChange = new VendorChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Vendor_Change_Detail_By_VendorChangeDetailId";
        DataSet ds;

        hashPara.Add("@Vendor_Change_Detail_Id", Vendor_Change_Detail_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjVendorChange.Vendor_Change_Detail_Id = Convert.ToInt32( dt.Rows[0]["Vendor_Change_Detail_Id"].ToString());
                    ObjVendorChange.Vendor_Change_Id = Convert.ToInt32(dt.Rows[0]["Vendor_Change_Id"].ToString());
                    ObjVendorChange.Section_Id = Convert.ToInt32(dt.Rows[0]["Section_Id"].ToString());
                    ObjVendorChange.Section_Feild_Master_Id = Convert.ToInt32(dt.Rows[0]["Section_Feild_Master_Id"].ToString());
                    ObjVendorChange.Old_Value = dt.Rows[0]["Old_Value"].ToString();
                    ObjVendorChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                    ObjVendorChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                    ObjVendorChange.Remarks = dt.Rows[0]["Remarks"].ToString();
                }
            }
            return ObjVendorChange;
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

    public int DeleteVendorChangeDetail(string Vendor_Change_Detail_Id)
    {
        VendorChangeDetail ObjVendorChange = new VendorChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Delete_Vendor_Change_Detail_By_VendorChangeDetailId";
        int result = 0;

        hashPara.Add("@Vendor_Change_Detail_Id", Vendor_Change_Detail_Id);

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

    public DataSet GetVendorChangeData(string MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Vendor_Change_By_MasterHeaderId";
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

    public DataSet GetVendorChangeDetailData(int Vendor_Change_Id)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Vendor_Change_Detail_By_VendorChangeId";
        DataSet ds;

        hashPara.Add("@Vendor_Change_Id", Vendor_Change_Id);

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

    //New WF change for Vendor Change Req when Vendor code is 9 series by Swati on 08/08/18
    public int SaveVendorHeaderVM(string masterHeaderId, string ModuleID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        SqlCommand objCommand = new SqlCommand();
        string procName = "pr_Save_Master_Header_Vendor_Change";
        int retVal = 0;
        int masterHeaderId2 = 0;
        objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
        objCommand.Parameters.AddWithValue("@ModuleID", ModuleID);
        SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
        ret.Direction = ParameterDirection.ReturnValue;

        try
        {
            objDal.OpenConnection();
            objCommand.Connection = objDal.cnnConnection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = procName;
            //Srinidhi
            objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);
            objCommand.ExecuteNonQuery();
            retVal = (int)ret.Value;
            if (retVal > 0)
            {
                masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(masterHeaderId);
            }
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
        return masterHeaderId2;
    }

    public DataSet GetVendorCode(string Master_Header_Id)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        DataSet dstData = objDal.FillDataSet("select Customer_Code from T_Vendor_Change where Master_Header_Id = '" + Master_Header_Id + "' AND IsActive = 'TRUE'", "table");
        return dstData;
    }
    //end

    //Added by Swati for Vendor Excel Upload on 13.12.2018
    public int SaveImport(VendorChange ObjVendorChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Vendor_Change_Import";
        int result = 0;


        hashPara.Add("@Vendor_Change_Id", ObjVendorChange.Vendor_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjVendorChange.Master_Header_Id);
        hashPara.Add("@Customer_Code", ObjVendorChange.Customer_Code);
        hashPara.Add("@Vendor_Desc", ObjVendorChange.Vendor_Desc);
        hashPara.Add("@Company_Code", ObjVendorChange.Company_Code);
        hashPara.Add("@Vendor_Group", ObjVendorChange.Vendor_Group);
        hashPara.Add("@Purchase_Org", ObjVendorChange.Purchase_Org);
        hashPara.Add("@Remarks", ObjVendorChange.Remarks);


        hashPara.Add("@Vendor_Change_Detail_Id", ObjVendorChange.Vendor_Change_Detail_Id);
        hashPara.Add("@Section_Id", ObjVendorChange.Section_Id);
        hashPara.Add("@Section_Feild_Master_Id", ObjVendorChange.Section_Feild_Master_Id);
        hashPara.Add("@Old_Value", ObjVendorChange.Old_Value);
        hashPara.Add("@New_Value", ObjVendorChange.New_Value);

        hashPara.Add("@IsActive", ObjVendorChange.IsActive);
        hashPara.Add("@UserId", ObjVendorChange.UserId);
        hashPara.Add("@UserIp", ObjVendorChange.IPAddress);

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
    //End Change

    //Start Change By Swati For ARIBA Change DB Config on 28.03.2019
    public DataSet GetStatus()
    {
        DataAccessLayer objDal = new DataAccessLayer();
        DataSet dstData = objDal.FillDataSet("select Status from M_Status", "table");
        return dstData;
    }
    //End Change


    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    /// <param name="ObjChangeHeader"></param>
    /// <returns></returns>
    public int SavePFunHeaderData(VendorChange ObjVendorChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Utility objUtil = new Utility();
        SqlCommand objCommand = new SqlCommand();
        string procName = "pr_Ins_Upd_PFun_T_Vendor_Change";
        int retVal = 0;
        int VChangeHeaderID = 0;

        objCommand.Parameters.AddWithValue("@Vendor_Change_Id", ObjVendorChange.Vendor_Change_Id);
        objCommand.Parameters.AddWithValue("@Master_Header_Id", ObjVendorChange.Master_Header_Id);
        objCommand.Parameters.AddWithValue("@Customer_Code", ObjVendorChange.Customer_Code);
        objCommand.Parameters.AddWithValue("@Company_Code", ObjVendorChange.Company_Code);
        objCommand.Parameters.AddWithValue("@Vendor_Group", ObjVendorChange.Vendor_Group);
        objCommand.Parameters.AddWithValue("@Purchase_Org", ObjVendorChange.Purchase_Org);
        objCommand.Parameters.AddWithValue("@Vendor_Desc", ObjVendorChange.Vendor_Desc);
        objCommand.Parameters.AddWithValue("@IsActive", ObjVendorChange.IsActive);  
        objCommand.Parameters.AddWithValue("@UserId", ObjVendorChange.UserId);
        objCommand.Parameters.AddWithValue("@IpAddress", ObjVendorChange.IPAddress);

        SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
        ret.Direction = ParameterDirection.ReturnValue;

        SqlParameter OutVChangeHeaderId = objCommand.Parameters.Add("@OutVChangeHeaderId", SqlDbType.Int);
        OutVChangeHeaderId.Direction = ParameterDirection.Output;
        try
        {
            objDal.OpenConnection();
            objCommand.Connection = objDal.cnnConnection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = procName; 
            objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            objCommand.ExecuteNonQuery();
            retVal = (int)ret.Value;
            if (retVal > 0)
            {
                VChangeHeaderID = SafeTypeHandling.ConvertStringToInt32(OutVChangeHeaderId.Value);
            }

        }
        catch (Exception ex)
        {
           // throw ex;
        }
        finally
        {
            objDal.CloseConnection(objDal.cnnConnection);
            objDal = null;
        }
        return VChangeHeaderID;
    }
     
    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    /// <param name="objVendorPartnerFun"></param>
    /// <returns></returns>
    public int SavePFunDetails(VendorPartnerFun objVendorPartnerFun)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Vendor_Partner_Fun";
        int result = 0;

        hashPara.Add("@Vendor_PFun_Detail_Id", objVendorPartnerFun.Vendor_PFun_Detail_Id);
        hashPara.Add("@Vendor_Change_Id", objVendorPartnerFun.Vendor_Change_Id);
        hashPara.Add("@sPfun_Lookup_Code", objVendorPartnerFun.sPfun_Lookup_Code);
        hashPara.Add("@sVendor_Code", objVendorPartnerFun.sVendor_Code);
        hashPara.Add("@sVendor_Desc", objVendorPartnerFun.sVendor_Desc);
        hashPara.Add("@bIsActive", objVendorPartnerFun.bIsActive);
        //hashPara.Add("@dCreatedOn", objVendorPartnerFun.dCreatedOn);
        hashPara.Add("@nCreatedBy", objVendorPartnerFun.nCreatedBy);
        hashPara.Add("@sCreatedIp", objVendorPartnerFun.sCreatedIp);

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
    /// PFun_DT06032020
    /// </summary>
    /// <param name="id"></param>
    public int DeleteRow(int id)
    {
        VendorChangeDetail ObjVendorChange = new VendorChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Delete_T_Vendor_Partner_Fun_By_Vendor_PFun_Detail_Id";
        int result = 0;

        hashPara.Add("@Vendor_PFun_Detail_Id", id);

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
    /// PFun_DT06032020
    /// </summary>
    /// <param name="id"></param>
    public int DeleteMain(int id)
    {
        VendorChangeDetail ObjVendorChange = new VendorChangeDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Delete_T_Vendor_Change_Partner_Fun_By_Vendor_Change_Id";
        int result = 0;

        hashPara.Add("@Vendor_Change_Id", id);

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
    /// PFun_DT06032020
    /// </summary>
    /// <param name="vendorChangeId"></param>
    /// <returns></returns>
    public VendorChange GetPFunHeaderData(string vendorChangeId)
    {
        VendorChange ObjVendorChange = new VendorChange();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_VendorChange_By_Vendor_Change_Id";
        DataSet ds;

        hashPara.Add("@Vendor_Change_Id", vendorChangeId);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ObjVendorChange.Vendor_Change_Id = Convert.ToInt32(dt.Rows[0]["Vendor_Change_Id"].ToString());
                    ObjVendorChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjVendorChange.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                    ObjVendorChange.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                    ObjVendorChange.Vendor_Group = dt.Rows[0]["Vendor_Group"].ToString();
                    ObjVendorChange.Purchase_Org = dt.Rows[0]["Purchase_Org"].ToString();
                    ObjVendorChange.Vendor_Desc = dt.Rows[0]["Vendor_Desc"].ToString(); 
                }
            }
            return ObjVendorChange;
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

    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    /// <param name="VendorChangeID"></param>
    /// <returns></returns>
    public DataSet GetPFunDetail(string VendorChangeID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        hashPara.Add("@Vendor_Change_Id", VendorChangeID);

        string procName = "pr_Get_Partner_Fun_Data_By_MasterHeaderId";
        return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
    }

    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    /// <param name="Vendor_Change_Id"></param>
    /// <returns></returns>
    public DataSet GetVendorPfunChangeDetailData(int Vendor_Change_Id)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Vendor_PFun_Change_Detail_By_VendorChangeId";
        DataSet ds;

        hashPara.Add("@Vendor_Change_Id", Vendor_Change_Id);

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

    /// <summary>
    /// PFun_DT06032020
    /// </summary>
    /// <param name="ObjVendorChange"></param>
    /// <returns></returns>
    public int SaveImportPF(VendorChangePFun ObjVendorChange)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Vendor_Change_Import_PF";
        int result = 0;

        hashPara.Add("@Vendor_Change_Id", ObjVendorChange.Vendor_Change_Id);
        hashPara.Add("@Master_Header_Id", ObjVendorChange.Master_Header_Id);
        hashPara.Add("@Customer_Code", ObjVendorChange.Customer_Code);
        hashPara.Add("@Vendor_Desc", ObjVendorChange.Vendor_Desc);
        hashPara.Add("@Company_Code", ObjVendorChange.Company_Code);
        hashPara.Add("@Vendor_Group", ObjVendorChange.Vendor_Group);
        hashPara.Add("@Purchase_Org", ObjVendorChange.Purchase_Org); 
        hashPara.Add("@Vendor_PFun_Detail_Id", ObjVendorChange.Vendor_PFun_Detail_Id);
        hashPara.Add("@sPfun_Lookup_Code", ObjVendorChange.sPfun_Lookup_Code);
        hashPara.Add("@sVendor_Code_link", ObjVendorChange.sVendor_Code_link);
        hashPara.Add("@sVendor_Desc_link", ObjVendorChange.sVendor_Desc_link);
        hashPara.Add("@IsActive", ObjVendorChange.IsActive);
        hashPara.Add("@UserId", ObjVendorChange.UserId);
        hashPara.Add("@UserIp", ObjVendorChange.IPAddress);

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