using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for PriceMasterAccess
/// </summary>
public class PriceMasterAccess
{
	public PriceMasterAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Save(PriceHeader ObjPrice)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Price_Header";
        int result = 0;


        hashPara.Add("@Price_Header_Id", ObjPrice.Price_Header_Id);
        hashPara.Add("@Master_Header_Id", ObjPrice.Master_Header_Id);
        hashPara.Add("@Material_Code", ObjPrice.Material_Code);
        hashPara.Add("@Batch_Id", ObjPrice.Batch_Id);
        hashPara.Add("@Validity_Date_From", ObjPrice.Validity_Date_From);
        hashPara.Add("@Customer_Code", ObjPrice.Customer_Code);
        hashPara.Add("@Vendor_Code", ObjPrice.Vendor_Code);
        hashPara.Add("@Plant_Id", ObjPrice.Plant_Id);
        hashPara.Add("@Price_Group", ObjPrice.Price_Group);
        hashPara.Add("@Processing_Status", ObjPrice.Processing_Status);
        hashPara.Add("@Base_Unit_of_Measure", ObjPrice.Base_Unit_of_Measure);
        hashPara.Add("@Trade_Price", ObjPrice.Trade_Price);
        hashPara.Add("@Excise_Duty", ObjPrice.Excise_Duty);
        hashPara.Add("@Education_Cess", ObjPrice.Education_Cess);
        hashPara.Add("@Sec_High_Edu_Cess", ObjPrice.Sec_High_Edu_Cess);
        hashPara.Add("@MRP", ObjPrice.MRP);
        hashPara.Add("@Rate_Unit", ObjPrice.Rate_Unit);
        hashPara.Add("@IsActive", ObjPrice.IsActive);
        hashPara.Add("@UserId", ObjPrice.UserId);
        hashPara.Add("@UserIp", ObjPrice.IPAddress);

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

    public int Save(PriceDetail ObjPrice)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Price_Detail";
        int result = 0;


        hashPara.Add("@Price_Detail_Id", ObjPrice.Price_Detail_Id);
        hashPara.Add("@Price_Header_Id", ObjPrice.Price_Header_Id);
        hashPara.Add("@Region_Id_Delivery_Plant", ObjPrice.Region_Id_Delivery_Plant);
        hashPara.Add("@Region_Id", ObjPrice.Region_Id);
        hashPara.Add("@Trade_Price", ObjPrice.Trade_Price);
        hashPara.Add("@IsActive", ObjPrice.IsActive);
        hashPara.Add("@UserId", ObjPrice.UserId);
        hashPara.Add("@UserIp", ObjPrice.IPAddress);

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


    public PriceHeader GetPriceHeader(int Master_Header_Id)
    {
        PriceHeader ObjPrice = new PriceHeader();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Price_Header_By_MasterHeaderId";
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
                    ObjPrice.Price_Header_Id = Convert.ToInt32(dt.Rows[0]["Price_Header_Id"].ToString());
                    ObjPrice.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjPrice.Material_Code = dt.Rows[0]["Material_Code"].ToString();
                    ObjPrice.Batch_Id = Convert.ToInt32(dt.Rows[0]["Batch_Id"].ToString());
                    ObjPrice.Validity_Date_From = dt.Rows[0]["Validity_Date_From"].ToString();
                    ObjPrice.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                    ObjPrice.Vendor_Code = dt.Rows[0]["Vendor_Code"].ToString();
                    ObjPrice.Plant_Id = Convert.ToInt32(dt.Rows[0]["Plant_Id"].ToString());
                    ObjPrice.Price_Group = dt.Rows[0]["Price_Group"].ToString();
                    ObjPrice.Processing_Status = dt.Rows[0]["Processing_Status"].ToString();
                    ObjPrice.Base_Unit_of_Measure = dt.Rows[0]["Base_Unit_of_Measure"].ToString();
                    ObjPrice.Trade_Price = dt.Rows[0]["Trade_Price"].ToString();
                    ObjPrice.Excise_Duty = dt.Rows[0]["Excise_Duty"].ToString();
                    ObjPrice.Education_Cess = dt.Rows[0]["Education_Cess"].ToString();
                    ObjPrice.Sec_High_Edu_Cess = dt.Rows[0]["Sec_High_Edu_Cess"].ToString();
                    ObjPrice.MRP = dt.Rows[0]["MRP"].ToString();
                    ObjPrice.Rate_Unit = dt.Rows[0]["Rate_Unit"].ToString();
                }
            }
            return ObjPrice;
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

    public PriceDetail GetPriceDetail(int Price_Header_Id)
    {
        PriceDetail ObjPrice = new PriceDetail();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Price_Detail_By_PriceHeaderId";
        DataSet ds;

        hashPara.Add("@Price_Header_Id", Price_Header_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjPrice.Price_Detail_Id = Convert.ToInt32(dt.Rows[0]["Price_Detail_Id"].ToString());
                    ObjPrice.Price_Header_Id = Convert.ToInt32(dt.Rows[0]["Price_Header_Id"].ToString());
                    ObjPrice.Region_Id_Delivery_Plant = Convert.ToInt32(dt.Rows[0]["Region_Id_Delivery_Plant"].ToString());
                    ObjPrice.Region_Id = Convert.ToInt32(dt.Rows[0]["Region_Id"].ToString());
                    ObjPrice.Trade_Price = dt.Rows[0]["Trade_Price"].ToString();
                }
            }
            return ObjPrice;
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


    public DataSet GetPriceDetailData(int MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Price_Detail_By_MasterHeaderId";
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

    //Code Added By Swati for Price Master
    public int SavePrice(PriceMasterCreate ObjPrice)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        
        string procName = "pr_Ins_Upd_T_Price_Master";
        int result = 0;

        hashPara.Add("@ID", ObjPrice.ID);
        hashPara.Add("@Master_Header_Id", ObjPrice.Master_Header_Id);
        hashPara.Add("@Material_Code", ObjPrice.Material_Code);
        hashPara.Add("@Material_Desc", ObjPrice.Material_Desc);
        hashPara.Add("@Material_Group", ObjPrice.Material_Group);
        hashPara.Add("@Batch", ObjPrice.Batch);
        hashPara.Add("@ZTRP", ObjPrice.ZTRP);
        hashPara.Add("@ZMRP", ObjPrice.ZMRP);
        hashPara.Add("@ZSPL", ObjPrice.ZSPL);
        hashPara.Add("@Division", ObjPrice.Division);
        hashPara.Add("@Unit", ObjPrice.Unit);
        hashPara.Add("@CreatedBy", ObjPrice.UserId);
        hashPara.Add("@CreatedIP", ObjPrice.IPAddress);
        hashPara.Add("@dEffectivedate", ObjPrice.dEffectivedate);

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

    public PriceMasterCreate GetPriceMasterData(int Master_Header_Id)
    {
        PriceMasterCreate ObjPrice = new PriceMasterCreate();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Price_Master_By_MasterHeaderId";
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
                    ObjPrice.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                    ObjPrice.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjPrice.Material_Code = dt.Rows[0]["Material_Code"].ToString();
                    ObjPrice.Material_Desc = dt.Rows[0]["Material_Desc"].ToString();
                    ObjPrice.Material_Group = dt.Rows[0]["Material_Group"].ToString();
                    ObjPrice.Batch = dt.Rows[0]["Batch"].ToString();
                    ObjPrice.ZMRP = dt.Rows[0]["ZMRP"].ToString();
                    ObjPrice.ZSPL = dt.Rows[0]["ZSPL"].ToString();
                    ObjPrice.ZTRP = dt.Rows[0]["ZTRP"].ToString();
                    ObjPrice.Division = dt.Rows[0]["Division"].ToString();
                    ObjPrice.Unit = dt.Rows[0]["Unit"].ToString();
                }
            }
            return ObjPrice;
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

    public PriceMasterCreate GetMaterialCodeChangeData(string Material_Code)
    {
        PriceMasterCreate ObjPrice = new PriceMasterCreate();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Detail_By_MaterialCode";
        DataSet ds;

        hashPara.Add("@Material_Code", Material_Code);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjPrice.Material_Desc = dt.Rows[0]["Material_Desc"].ToString();
                    ObjPrice.Material_Group = dt.Rows[0]["Material_Group"].ToString();
                    ObjPrice.Division = dt.Rows[0]["Division"].ToString();
                }
            }
            return ObjPrice;
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


    public DataSet FillGridData(string Master_Header_id)
    {
        DataSet ds;
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Price_Master_By_MasterHeaderId";
        hashPara.Add("@Master_Header_id", Master_Header_id);
        ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        return ds;
    }

    public DataSet GetMaterialData(string Material_Code)
    {
        DataSet ds;
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_Detail_By_MaterialCode";
        hashPara.Add("@Material_Code", Material_Code);
        ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        return ds;
    }
    public int SaveDetail(PriceMasterCreate ObjPrice)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Price_Master_Draft";
        int result = 0;

        hashPara.Add("@Master_Header_Id", ObjPrice.Master_Header_Id);

        hashPara.Add("@CreatedBy", ObjPrice.UserId);
        hashPara.Add("@CreatedIP", ObjPrice.IPAddress);

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

    public DataSet GetReqData(string MasterHeaderID)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        return objDal.FillDataSet("select Pending_For_Seq from T_Master_Header where Master_Header_Id = " + MasterHeaderID, "Pending_For_Seq");
    }

    //End
}