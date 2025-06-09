using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
/// <summary>
/// Summary description for PlantStorageAccess
/// </summary>
public class PlantStorageAccess
{
    public PlantStorageAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public static List<TPlantStorage> GetPlantStorage(string SectionCode,string MasterHeaderID = null)
    //{
    //    List<TPlantStorage> objPLocation = new List<TPlantStorage>();
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    Hashtable hashPara = new Hashtable();
    //    string procName = "USP_Get_T_Mat_Plant_Storage";
    //    hashPara.Add("@MasterHeaderID", MasterHeaderID);
    //    hashPara.Add("@SectionCode", SectionCode);
    //    try
    //    {
    //        objDal.OpenConnection();
    //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
    //        DataTable dtSales = new DataTable();
    //        if (dsSales != null)
    //            dtSales = dsSales.Tables[0];

    //        objPLocation = dtSales.AsEnumerable().Select(x => new TPlantStorage
    //        {
    //            Mat_Plant_Storage_Id = x["Mat_Plant_Storage_Id"] != DBNull.Value ? Convert.ToInt32(x["Mat_Plant_Storage_Id"]) : 0,
    //            Master_Header_Id = x["Master_Header_Id"] != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0,
    //            Storage_bin = x["Storage_bin"].ToString(),
    //            Storage_Condition = x["Storage_Condition"].ToString(),
    //            Temperature_Cond_Ind = x["Temperature_Cond_Ind"].ToString(),
    //            Hazardous_Mat_No = x["Hazardous_Mat_No"].ToString(),
    //            Max_Storage_Period = x["Max_Storage_Period"] != DBNull.Value ? Convert.ToDecimal(x["Max_Storage_Period"]) : new Nullable<Decimal>(),
    //            Unit_Max_Storage_Period = x["Unit_Max_Storage_Period"].ToString(),
    //            Min_Remaining_Shelf_Life = x["Min_Remaining_Shelf_Life"] != DBNull.Value ? Convert.ToDecimal(x["Min_Remaining_Shelf_Life"]) : new Nullable<Decimal>(),
    //            Total_Shelf_Life_Days = x["Total_Shelf_Life_Days"] != DBNull.Value ? Convert.ToDecimal(x["Total_Shelf_Life_Days"]) : new Nullable<Decimal>(),
    //            Storage_Perc = x["Storage_Perc"] != DBNull.Value ? Convert.ToDecimal(x["Storage_Perc"]) : new Nullable<Decimal>(),
    //            Negative_Stock_Allowed = x["Negative_Stock_Allowed"] != DBNull.Value ? Convert.ToBoolean(x["Negative_Stock_Allowed"]) : false,
    //            CC_Indicator_Fixed = x["CC_Indicator_Fixed"] != DBNull.Value ? Convert.ToBoolean(x["CC_Indicator_Fixed"]) : false,
    //            Rnding_Rule_Calc_SLED = x["Rnding_Rule_Calc_SLED"].ToString(),
    //            Period_Ind_Shelf_Life_Exp_Dt = x["Period_Ind_Shelf_Life_Exp_Dt"].ToString(),
    //            IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false,
    //            PlantId = x["PlantID"].ToString(),
    //            PlantName = x["PlantName"].ToString(),
    //            StorageLocation = x["StorageLocation"].ToString(),
    //            StorageLocationID = x["StorageLocationID"].ToString()

    //        }
    //        ).ToList<TPlantStorage>();

    //        return objPLocation;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objDal.CloseConnection(objDal.cnnConnection);
    //        objDal = null;
    //    }

    //}

    //public static TPlantStorage GetPlantStorage(string MasterHeaderID, int ID, string SectionCode)
    //{
    //    TPlantStorage objPLocation = new TPlantStorage();
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    Hashtable hashPara = new Hashtable();
    //    string procName = "USP_Get_T_Mat_Plant_Storage";
    //    hashPara.Add("@MasterHeaderID", MasterHeaderID);
    //    hashPara.Add("@SectionCode", SectionCode);
    //    hashPara.Add("@Mat_Plant_Storage_Id", ID);

    //    try
    //    {
    //        objDal.OpenConnection();
    //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
    //        DataTable dtSales = new DataTable();
    //        if (dsSales != null)
    //            dtSales = dsSales.Tables[0];
    //        if (dtSales != null)
    //        {
    //            DataRow x = dtSales.Rows[0];


    //            objPLocation.Mat_Plant_Storage_Id = x["Mat_Plant_Storage_Id"] != DBNull.Value ? Convert.ToInt32(x["Mat_Plant_Storage_Id"]) : 0;
    //            objPLocation.Master_Header_Id = x["Master_Header_Id"] != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0;
    //            objPLocation.Storage_bin = x["Storage_bin"].ToString();
    //            objPLocation.Storage_Condition = x["Storage_Condition"].ToString();
    //            objPLocation.Temperature_Cond_Ind = x["Temperature_Cond_Ind"].ToString();
    //            objPLocation.Hazardous_Mat_No = x["Hazardous_Mat_No"].ToString();
    //            objPLocation.Max_Storage_Period = x["Max_Storage_Period"] != DBNull.Value ? Convert.ToDecimal(x["Max_Storage_Period"]) : new Nullable<Decimal>();
    //            objPLocation.Unit_Max_Storage_Period = x["Unit_Max_Storage_Period"].ToString();
    //            objPLocation.Min_Remaining_Shelf_Life = x["Min_Remaining_Shelf_Life"] != DBNull.Value ? Convert.ToDecimal(x["Min_Remaining_Shelf_Life"]) : new Nullable<Decimal>();
    //            objPLocation.Total_Shelf_Life_Days = x["Total_Shelf_Life_Days"] != DBNull.Value ? Convert.ToDecimal(x["Total_Shelf_Life_Days"]) : new Nullable<Decimal>();
    //            objPLocation.Storage_Perc = x["Storage_Perc"] != DBNull.Value ? Convert.ToDecimal(x["Storage_Perc"]) : new Nullable<Decimal>();
    //            objPLocation.Negative_Stock_Allowed = x["Negative_Stock_Allowed"] != DBNull.Value ? Convert.ToBoolean(x["Negative_Stock_Allowed"]) : false;
    //            objPLocation.CC_Indicator_Fixed = x["CC_Indicator_Fixed"] != DBNull.Value ? Convert.ToBoolean(x["CC_Indicator_Fixed"]) : false;
    //            objPLocation.Rnding_Rule_Calc_SLED = x["Rnding_Rule_Calc_SLED"].ToString();
    //            objPLocation.Period_Ind_Shelf_Life_Exp_Dt = x["Period_Ind_Shelf_Life_Exp_Dt"].ToString();
    //            objPLocation.IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false;
    //            objPLocation.PlantId = x["PlantID"].ToString();
    //            objPLocation.PlantName = x["PlantName"].ToString();
    //            objPLocation.StorageLocation = x["StorageLocation"].ToString();
    //            objPLocation.StorageLocationID = x["StorageLocationID"].ToString();

    //        }


    //        return objPLocation;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objDal.CloseConnection(objDal.cnnConnection);
    //        objDal = null;
    //    }

    //}

    //public bool SavePlantStorage(TPlantStorage objPLocation)
    //{
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    Hashtable hashPara = new Hashtable();

    //    try
    //    {
    //        string procName = "pr_Ins_Upd_T_Mat_Plant_Storage";
    //        int result = 0;
    //        hashPara.Add("@Mat_Plant_Storage_Id", objPLocation.Mat_Plant_Storage_Id);
    //        hashPara.Add("@Master_Header_Id", objPLocation.Master_Header_Id);
    //        hashPara.Add("@Storage_bin", objPLocation.Storage_bin);
    //        hashPara.Add("@Storage_Condition", objPLocation.Storage_Condition);
    //        hashPara.Add("@Temperature_Cond_Ind", objPLocation.Temperature_Cond_Ind);
    //        hashPara.Add("@Hazardous_Mat_No", objPLocation.Hazardous_Mat_No);
    //        hashPara.Add("@Max_Storage_Period", objPLocation.Max_Storage_Period);
    //        hashPara.Add("@Unit_Max_Storage_Period", objPLocation.Unit_Max_Storage_Period);
    //        hashPara.Add("@Min_Remaining_Shelf_Life", objPLocation.Min_Remaining_Shelf_Life);
    //        hashPara.Add("@Total_Shelf_Life_Days", objPLocation.Total_Shelf_Life_Days);
    //        hashPara.Add("@Storage_Perc", objPLocation.Storage_Perc);
    //        hashPara.Add("@Negative_Stock_Allowed", objPLocation.Negative_Stock_Allowed);
    //        hashPara.Add("@CC_Indicator_Fixed", objPLocation.CC_Indicator_Fixed);
    //        hashPara.Add("@Rnding_Rule_Calc_SLED", objPLocation.Rnding_Rule_Calc_SLED);
    //        hashPara.Add("@Period_Ind_Shelf_Life_Exp_Dt", objPLocation.Period_Ind_Shelf_Life_Exp_Dt);
    //        hashPara.Add("@IsActive", objPLocation.IsActive);
    //        hashPara.Add("@UserId", objPLocation.UserId);
    //        hashPara.Add("@UserIp", objPLocation.IPAddress);
    //        hashPara.Add("@SectionCode", objPLocation.SectionCode);
    //        hashPara.Add("@Plant_Id", objPLocation.PlantId);
    //        hashPara.Add("@StorageLocationID", objPLocation.StorageLocationID);

    //        objDal.OpenConnection();
    //        result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
    //        return (result > 0 ? true : false);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {
    //        objDal.CloseConnection(objDal.cnnConnection);
    //        objDal = null;
    //    }



    //}

    public int Save(PlantStorage ObjPlantStorage)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Mat_Plant_Storage";
        int result = 0;

        hashPara.Add("@Mat_Plant_Storage_Id", ObjPlantStorage.Mat_Plant_Storage_Id);
        hashPara.Add("@Master_Header_Id", ObjPlantStorage.Master_Header_Id);
        hashPara.Add("@Plant_Id", ObjPlantStorage.Plant_Id);
        hashPara.Add("@Storage_Location", ObjPlantStorage.Storage_Location);
        hashPara.Add("@Storage_bin", ObjPlantStorage.Storage_bin);
        hashPara.Add("@Storage_Condition", ObjPlantStorage.Storage_Condition);
        hashPara.Add("@Temperature_Cond_Ind", ObjPlantStorage.Temperature_Cond_Ind);
        hashPara.Add("@Hazardous_Mat_No", ObjPlantStorage.Hazardous_Mat_No);
        hashPara.Add("@Max_Storage_Period", ObjPlantStorage.Max_Storage_Period);
        hashPara.Add("@Unit_Max_Storage_Period", ObjPlantStorage.Unit_Max_Storage_Period);
        hashPara.Add("@Min_Remaining_Shelf_Life", ObjPlantStorage.Min_Remaining_Shelf_Life);
        hashPara.Add("@Total_Shelf_Life_Days", ObjPlantStorage.Total_Shelf_Life_Days);
        hashPara.Add("@Storage_Perc", ObjPlantStorage.Storage_Perc);
        hashPara.Add("@Negative_Stock_Allowed", ObjPlantStorage.Negative_Stock_Allowed);
        hashPara.Add("@CC_Indicator_Fixed", ObjPlantStorage.CC_Indicator_Fixed);
        hashPara.Add("@Rnding_Rule_Calc_SLED", ObjPlantStorage.Rnding_Rule_Calc_SLED);
        hashPara.Add("@Period_Ind_Shelf_Life_Exp_Dt", ObjPlantStorage.Period_Ind_Shelf_Life_Exp_Dt);
        hashPara.Add("@Label_Type", ObjPlantStorage.Label_Type);
        hashPara.Add("@Label_Form", ObjPlantStorage.Label_Form);
        hashPara.Add("@Profit_Center", ObjPlantStorage.Profit_Center);

        hashPara.Add("@IsActive", ObjPlantStorage.IsActive);
        hashPara.Add("@UserId", ObjPlantStorage.UserId);
        hashPara.Add("@UserIp", ObjPlantStorage.IPAddress);
        //CTRL_SUB_SDT06062019
        hashPara.Add("@sTypeofChemical", ObjPlantStorage.sTypeofChemical);
        hashPara.Add("@sIsMatCtrlSub", ObjPlantStorage.sIsMatCtrlSub);
        //CTRL_SUB_SDT06062019
        hashPara.Add("@IsDraft", false);
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

    public PlantStorage GetPlantStorage(int Mat_Plant_Storage_Id)
    {
        PlantStorage ObjPlantStorage = new PlantStorage();

        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Mat_Plant_Storage_By_MatPlantStorageId";
        DataSet ds;

        hashPara.Add("@Mat_Plant_Storage_Id", Mat_Plant_Storage_Id);

        try
        {
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ObjPlantStorage.Mat_Plant_Storage_Id = Convert.ToInt32(dt.Rows[0]["Mat_Plant_Storage_Id"].ToString());
                    ObjPlantStorage.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                    ObjPlantStorage.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                    ObjPlantStorage.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                    ObjPlantStorage.Storage_bin = dt.Rows[0]["Storage_bin"].ToString();
                    ObjPlantStorage.Storage_Condition = dt.Rows[0]["Storage_Condition"].ToString();
                    ObjPlantStorage.Temperature_Cond_Ind = dt.Rows[0]["Temperature_Cond_Ind"].ToString();
                    ObjPlantStorage.Hazardous_Mat_No = dt.Rows[0]["Hazardous_Mat_No"].ToString();
                    ObjPlantStorage.Max_Storage_Period = dt.Rows[0]["Max_Storage_Period"].ToString();
                    ObjPlantStorage.Unit_Max_Storage_Period = dt.Rows[0]["Unit_Max_Storage_Period"].ToString();
                    ObjPlantStorage.Min_Remaining_Shelf_Life = dt.Rows[0]["Min_Remaining_Shelf_Life"].ToString();
                    ObjPlantStorage.Total_Shelf_Life_Days = dt.Rows[0]["Total_Shelf_Life_Days"].ToString();
                    ObjPlantStorage.Storage_Perc = dt.Rows[0]["Storage_Perc"].ToString();
                    ObjPlantStorage.Negative_Stock_Allowed = dt.Rows[0]["Negative_Stock_Allowed"].ToString();
                    ObjPlantStorage.CC_Indicator_Fixed = dt.Rows[0]["CC_Indicator_Fixed"].ToString();
                    ObjPlantStorage.Rnding_Rule_Calc_SLED = dt.Rows[0]["Rnding_Rule_Calc_SLED"].ToString();
                    ObjPlantStorage.Period_Ind_Shelf_Life_Exp_Dt = dt.Rows[0]["Period_Ind_Shelf_Life_Exp_Dt"].ToString();
                    ObjPlantStorage.Label_Type = dt.Rows[0]["Label_Type"].ToString();
                    ObjPlantStorage.Label_Form = dt.Rows[0]["Label_Form"].ToString();
                    ObjPlantStorage.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                    //CTRL_SUB_SDT06062019 //SDT31072019
                    ObjPlantStorage.sTypeofChemical = dt.Rows[0]["sTypeofChemical"].ToString();
                    ObjPlantStorage.sIsMatCtrlSub = dt.Rows[0]["sIsMatCtrlSub"].ToString();
                    //CTRL_SUB_SDT06062019
                }
            }
            return ObjPlantStorage;
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

    public DataSet GetPlantStorageData(int MasterHeaderId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Mat_Plant_Storage_By_MasterHeaderId";
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
}