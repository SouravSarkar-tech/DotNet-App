using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Accenture.MWT.DomainObject;
namespace Accenture.MWT.DataAccess
{
    /// <summary>
    /// Summary description for WMAccess
    /// </summary>
    public class WMAccess
    {
        public WMAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //#region WM1
        //public static List<TWM1> GetWM1(string SectionCode,string MasterHeaderID = null)
        //{
        //    List<TWM1> wm = new List<TWM1>();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "USP_Get_T_Mat_WareHouse_Mgmt1 ";
        //    hashPara.Add("@MasterHeaderID", MasterHeaderID);
        //    hashPara.Add("@SectionCode", SectionCode);

        //    try
        //    {
        //        objDal.OpenConnection();
        //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        //        DataTable dtSales = new DataTable();
        //        if (dsSales != null)
        //            dtSales = dsSales.Tables[0];
        //        wm = dtSales.AsEnumerable().Select(x => new TWM1
        //        {
        //            Mat_WareHouse_Mgmt1_Id = (x["Mat_WareHouse_Mgmt1_Id"]) != DBNull.Value ? Convert.ToInt32(x["Mat_WareHouse_Mgmt1_Id"]) : 0,
        //            Master_Header_Id = (x["Master_Header_Id"]) != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0,
        //            WM_Unit_Measure = x["WM_Unit_Measure"].ToString(),
        //            Is_Msg_Inventory_Mgmt = x["Is_Msg_Inventory_Mgmt"] != DBNull.Value ? Convert.ToBoolean(x["Is_Msg_Inventory_Mgmt"]) : false,
        //            Storage_Section_Ind = x["Storage_Section_Ind"].ToString(),
        //            Stor_Type_Ind_Stock_Placement = x["Stor_Type_Ind_Stock_Placement"].ToString(),
        //            Stor_Type_Ind_Stock_Removal = x["Stor_Type_Ind_Stock_Removal"].ToString(),
        //            Is_Allow_Add_Exist_Stock = x["Is_Allow_Add_Exist_Stock"] != DBNull.Value ? Convert.ToBoolean(x["Is_Allow_Add_Exist_Stock"]) : false,
        //            Bulk_Storage_Ind = x["Bulk_Storage_Ind"].ToString(),
        //            IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false,
        //            Warehouse = x["Warehouse"].ToString(),
        //            WarehouseID = x["WarehouseID"].ToString(),
        //            StorageTypeID = x["StorageTypeID"].ToString(),
        //            StorageType = x["StorageType"].ToString(),


        //        }
        //        ).ToList<TWM1>();
        //        return wm;

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

        //public static TWM1 GetWM1(int ID, string SectionCode, string MasterHeaderID)
        //{
        //    TWM1 wm = new TWM1();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "USP_Get_T_Mat_WareHouse_Mgmt1 ";
        //    hashPara.Add("@MasterHeaderID", MasterHeaderID);
        //    hashPara.Add("@SectionCode", SectionCode);
        //    hashPara.Add("@MatWareHouseMgmt1Id", ID);

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

        //            wm.Mat_WareHouse_Mgmt1_Id = (x["Mat_WareHouse_Mgmt1_Id"]) != DBNull.Value ? Convert.ToInt32(x["Mat_WareHouse_Mgmt1_Id"]) : 0;
        //            wm.Master_Header_Id = (x["Master_Header_Id"]) != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0;
        //            wm.WM_Unit_Measure = x["WM_Unit_Measure"].ToString();
        //            wm.Is_Msg_Inventory_Mgmt = x["Is_Msg_Inventory_Mgmt"] != DBNull.Value ? Convert.ToBoolean(x["Is_Msg_Inventory_Mgmt"]) : false;
        //            wm.Storage_Section_Ind = x["Storage_Section_Ind"].ToString();
        //            wm.Stor_Type_Ind_Stock_Placement = x["Stor_Type_Ind_Stock_Placement"].ToString();
        //            wm.Stor_Type_Ind_Stock_Removal = x["Stor_Type_Ind_Stock_Removal"].ToString();
        //            wm.Is_Allow_Add_Exist_Stock = x["Is_Allow_Add_Exist_Stock"] != DBNull.Value ? Convert.ToBoolean(x["Is_Allow_Add_Exist_Stock"]) : false;
        //            wm.Bulk_Storage_Ind = x["Bulk_Storage_Ind"].ToString();
        //            wm.IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false;
        //            wm.Warehouse = x["Warehouse"].ToString();
        //            wm.WarehouseID = x["WarehouseID"].ToString();
        //            wm.StorageTypeID = x["StorageTypeID"].ToString();
        //            wm.StorageType = x["StorageType"].ToString();

        //        }

        //        return wm;

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

        //public bool SaveWM(TWM1 objWM)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_Mat_WareHouse_Mgmt1";
        //    int result = 0;

        //    try
        //    {
        //        hashPara.Add("@Mat_WareHouse_Mgmt1_Id", objWM.Mat_WareHouse_Mgmt1_Id);
        //        hashPara.Add("@Master_Header_Id", objWM.Master_Header_Id);
        //        hashPara.Add("@WM_Unit_Measure", objWM.WM_Unit_Measure);
        //        hashPara.Add("@Is_Msg_Inventory_Mgmt", objWM.Is_Msg_Inventory_Mgmt);
        //        hashPara.Add("@Storage_Section_Ind", objWM.Storage_Section_Ind);
        //        hashPara.Add("@Stor_Type_Ind_Stock_Placement", objWM.Stor_Type_Ind_Stock_Placement);
        //        hashPara.Add("@Stor_Type_Ind_Stock_Removal", objWM.Stor_Type_Ind_Stock_Removal);
        //        hashPara.Add("@Is_Allow_Add_Exist_Stock", objWM.Is_Allow_Add_Exist_Stock);
        //        hashPara.Add("@Bulk_Storage_Ind", objWM.Bulk_Storage_Ind);
        //        hashPara.Add("@IsActive", objWM.IsActive);
        //        hashPara.Add("@UserId", objWM.UserId);
        //        hashPara.Add("@UserIp", objWM.IPAddress);
        //        hashPara.Add("@StorageTypeID", objWM.StorageTypeID);
        //        hashPara.Add("@WarehouseID", objWM.WarehouseID);
        //        hashPara.Add("@SectionCode", objWM.SectionCode);


        //        objDal.OpenConnection();
        //        result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
        //        return (result > 0 ? true : false);
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

        //#endregion

        //#region WM2

        //public static List<TWM2> GetWM2(string SectionCode,  string MasterHeaderID = null)
        //{
        //    List<TWM2> wm = new List<TWM2>();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "USP_Get_T_Mat_WareHouse_Mgmt2 ";
        //    hashPara.Add("@MasterHeaderID", MasterHeaderID);
        //    hashPara.Add("@SectionCode", SectionCode);
        //    try
        //    {
        //        objDal.OpenConnection();
        //        DataSet dsSales = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        //        DataTable dtSales = new DataTable();
        //        if (dsSales != null)
        //            dtSales = dsSales.Tables[0];
        //        wm = dtSales.AsEnumerable().Select(x => new TWM2
        //        {
        //            Mat_WareHouse_Mgmt2_Id = x["Mat_WareHouse_Mgmt2_Id"] != DBNull.Value ? Convert.ToInt32(x["Mat_WareHouse_Mgmt2_Id"]) : 0,
        //            Master_Header_Id = x["Master_Header_Id"] != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0,
        //            Loading_Equipment_Quantity = x["Loading_Equipment_Quantity"] != DBNull.Value ? Convert.ToDecimal(x["Loading_Equipment_Quantity"]) : new Nullable<Decimal>(),
        //            Loading_Equipment_Quantity1 = x["Loading_Equipment_Quantity1"] != DBNull.Value ? Convert.ToDecimal(x["Loading_Equipment_Quantity1"]) : new Nullable<Decimal>(),
        //            Loading_Equipment_Quantity2 = x["Loading_Equipment_Quantity2"] != DBNull.Value ? Convert.ToDecimal(x["Loading_Equipment_Quantity2"]) : new Nullable<Decimal>(),
        //            Unit_Loading_Equip_Quan = x["Unit_Loading_Equip_Quan"].ToString(),
        //            Unit_Loading_Equip_Quan1 = x["Unit_Loading_Equip_Quan1"].ToString(),
        //            Unit_Loading_Equip_Quan2 = x["Unit_Loading_Equip_Quan2"].ToString(),
        //            Storage_Unit_Type = x["Storage_Unit_Type"].ToString(),
        //            Storage_Unit_Type1 = x["Storage_Unit_Type1"].ToString(),
        //            Storage_Unit_Type2 = x["Storage_Unit_Type2"].ToString(),
        //            IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false,
        //            Warehouse = x["Warehouse"].ToString(),
        //            WarehouseID = x["WarehouseID"].ToString(),
        //            StorageTypeID = x["StorageTypeID"].ToString(),
        //            StorageType = x["StorageType"].ToString(),

        //        }
        //        ).ToList<TWM2>();
        //        return wm;

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

        //public static TWM2 GetWM2(int ID, string SectionCode, string MasterHeaderID)
        //{
        //    TWM2 wm = new TWM2();
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "USP_Get_T_Mat_WareHouse_Mgmt2 ";
        //    hashPara.Add("@MasterHeaderID", MasterHeaderID);
        //    hashPara.Add("@SectionCode", SectionCode);
        //    hashPara.Add("@MatWareHouseMgmt2Id", ID);

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

        //            wm.Mat_WareHouse_Mgmt2_Id = x["Mat_WareHouse_Mgmt2_Id"] != DBNull.Value ? Convert.ToInt32(x["Mat_WareHouse_Mgmt2_Id"]) : 0;
        //            wm.Master_Header_Id = x["Master_Header_Id"] != DBNull.Value ? Convert.ToInt32(x["Master_Header_Id"]) : 0;
        //            wm.Loading_Equipment_Quantity = x["Loading_Equipment_Quantity"] != DBNull.Value ? Convert.ToDecimal(x["Loading_Equipment_Quantity"]) : new Nullable<Decimal>();
        //            wm.Loading_Equipment_Quantity1 = x["Loading_Equipment_Quantity1"] != DBNull.Value ? Convert.ToDecimal(x["Loading_Equipment_Quantity1"]) : new Nullable<Decimal>();
        //            wm.Loading_Equipment_Quantity2 = x["Loading_Equipment_Quantity2"] != DBNull.Value ? Convert.ToDecimal(x["Loading_Equipment_Quantity2"]) : new Nullable<Decimal>();
        //            wm.Unit_Loading_Equip_Quan = x["Unit_Loading_Equip_Quan"].ToString();
        //            wm.Unit_Loading_Equip_Quan1 = x["Unit_Loading_Equip_Quan1"].ToString();
        //            wm.Unit_Loading_Equip_Quan2 = x["Unit_Loading_Equip_Quan2"].ToString();
        //            wm.Storage_Unit_Type = x["Storage_Unit_Type"].ToString();
        //            wm.Storage_Unit_Type1 = x["Storage_Unit_Type1"].ToString();
        //            wm.Storage_Unit_Type2 = x["Storage_Unit_Type2"].ToString();
        //            wm.IsActive = x["IsActive"] != DBNull.Value ? Convert.ToBoolean(x["IsActive"]) : false;
        //            wm.Warehouse = x["Warehouse"].ToString();
        //            wm.WarehouseID = x["WarehouseID"].ToString();
        //            wm.StorageTypeID = x["StorageTypeID"].ToString();
        //            wm.StorageType = x["StorageType"].ToString();
        //        }

        //        return wm;

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

        //public bool SaveWM(TWM2 objWM)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_Mat_WareHouse_Mgmt2";
        //    int result = 0;

        //    try
        //    {
        //        hashPara.Add("@Mat_WareHouse_Mgmt2_Id", objWM.Mat_WareHouse_Mgmt2_Id);
        //        hashPara.Add("@Master_Header_Id", objWM.Master_Header_Id);
        //        hashPara.Add("@Loading_Equipment_Quantity", objWM.Loading_Equipment_Quantity);
        //        hashPara.Add("@Loading_Equipment_Quantity1", objWM.Loading_Equipment_Quantity1);
        //        hashPara.Add("@Loading_Equipment_Quantity2", objWM.Loading_Equipment_Quantity2);
        //        hashPara.Add("@Unit_Loading_Equip_Quan", objWM.Unit_Loading_Equip_Quan);
        //        hashPara.Add("@Unit_Loading_Equip_Quan1", objWM.Unit_Loading_Equip_Quan1);
        //        hashPara.Add("@Unit_Loading_Equip_Quan2", objWM.Unit_Loading_Equip_Quan2);
        //        hashPara.Add("@Storage_Unit_Type", objWM.Storage_Unit_Type);
        //        hashPara.Add("@Storage_Unit_Type1", objWM.Storage_Unit_Type1);
        //        hashPara.Add("@Storage_Unit_Type2", objWM.Storage_Unit_Type2);
        //        hashPara.Add("@IsActive", objWM.IsActive);
        //        hashPara.Add("@UserId", objWM.UserId);
        //        hashPara.Add("@UserIp", objWM.IPAddress);
        //        hashPara.Add("@StorageTypeID", objWM.StorageTypeID);
        //        hashPara.Add("@WarehouseID", objWM.WarehouseID);
        //        hashPara.Add("@SectionCode", objWM.SectionCode);



        //        objDal.OpenConnection();
        //        result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
        //        return (result > 0 ? true : false);
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


        //#endregion


        #region WareHouse Management 1

        public int Save(WareHouseMgmt1 ObjWareHouseMgmt)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_WareHouse_Mgmt1";
            int result = 0;


            hashPara.Add("@Mat_WareHouse_Mgmt1_Id", ObjWareHouseMgmt.Mat_WareHouse_Mgmt1_Id);
            hashPara.Add("@Master_Header_Id", ObjWareHouseMgmt.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjWareHouseMgmt.Plant_Id);
            hashPara.Add("@Warehouse_ID", ObjWareHouseMgmt.Warehouse_ID);
            hashPara.Add("@Storage_Type_ID", ObjWareHouseMgmt.Storage_Type_ID);
            hashPara.Add("@Capacity_Usage", ObjWareHouseMgmt.Capacity_Usage);
            hashPara.Add("@Capacity_Unit", ObjWareHouseMgmt.Capacity_Unit);
            hashPara.Add("@WM_Unit_Measure", ObjWareHouseMgmt.WM_Unit_Measure);
            hashPara.Add("@Is_Msg_Inventory_Mgmt", ObjWareHouseMgmt.Is_Msg_Inventory_Mgmt);
            hashPara.Add("@Storage_Section_Ind", ObjWareHouseMgmt.Storage_Section_Ind);
            hashPara.Add("@Stor_Type_Ind_Stock_Placement", ObjWareHouseMgmt.Stor_Type_Ind_Stock_Placement);
            hashPara.Add("@Stor_Type_Ind_Stock_Removal", ObjWareHouseMgmt.Stor_Type_Ind_Stock_Removal);
            hashPara.Add("@Is_Allow_Add_Exist_Stock", ObjWareHouseMgmt.Is_Allow_Add_Exist_Stock);
            hashPara.Add("@Bulk_Storage_Ind", ObjWareHouseMgmt.Bulk_Storage_Ind);
            hashPara.Add("@IsActive", ObjWareHouseMgmt.IsActive);
            hashPara.Add("@UserId", ObjWareHouseMgmt.UserId);
            hashPara.Add("@UserIp", ObjWareHouseMgmt.IPAddress);
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

        public WareHouseMgmt1 GetWareHouseMgmt1(int Mat_WareHouse_Mgmt1_Id)
        {
            WareHouseMgmt1 ObjWareHouseMgmt = new WareHouseMgmt1();

            DataAccessLayer objDal = new DataAccessLayer();

            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_WareHouse_Mgmt1_By_MatWareHouseMgmt1Id";
            DataSet ds;

            hashPara.Add("@Mat_WareHouse_Mgmt1_Id", Mat_WareHouse_Mgmt1_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjWareHouseMgmt.Mat_WareHouse_Mgmt1_Id = Convert.ToInt32(dt.Rows[0]["Mat_WareHouse_Mgmt1_Id"].ToString());
                        ObjWareHouseMgmt.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjWareHouseMgmt.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjWareHouseMgmt.Warehouse_ID = dt.Rows[0]["Warehouse_ID"].ToString();
                        ObjWareHouseMgmt.Storage_Type_ID = dt.Rows[0]["Storage_Type_ID"].ToString();
                        ObjWareHouseMgmt.Capacity_Usage = dt.Rows[0]["Capacity_Usage"].ToString();
                        ObjWareHouseMgmt.Capacity_Unit = dt.Rows[0]["Capacity_Unit"].ToString();
                        ObjWareHouseMgmt.WM_Unit_Measure = dt.Rows[0]["WM_Unit_Measure"].ToString();
                        ObjWareHouseMgmt.Is_Msg_Inventory_Mgmt = dt.Rows[0]["Is_Msg_Inventory_Mgmt"].ToString();
                        ObjWareHouseMgmt.Storage_Section_Ind = dt.Rows[0]["Storage_Section_Ind"].ToString();
                        ObjWareHouseMgmt.Stor_Type_Ind_Stock_Placement = dt.Rows[0]["Stor_Type_Ind_Stock_Placement"].ToString();
                        ObjWareHouseMgmt.Stor_Type_Ind_Stock_Removal = dt.Rows[0]["Stor_Type_Ind_Stock_Removal"].ToString();
                        ObjWareHouseMgmt.Is_Allow_Add_Exist_Stock = dt.Rows[0]["Is_Allow_Add_Exist_Stock"].ToString();
                        ObjWareHouseMgmt.Bulk_Storage_Ind = dt.Rows[0]["Bulk_Storage_Ind"].ToString();
                    }
                }
                return ObjWareHouseMgmt;
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

        public DataSet GetWareHouseMgmtData1(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_WareHouse_Mgmt1_By_MasterHeaderId";
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

        #endregion

        #region WareHouse Management 2

        public int Save(WareHouseMgmt2 ObjWareHouseMgmt)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_WareHouse_Mgmt2";
            int result = 0;


            hashPara.Add("@Mat_WareHouse_Mgmt2_Id", ObjWareHouseMgmt.Mat_WareHouse_Mgmt2_Id);
            hashPara.Add("@Master_Header_Id", ObjWareHouseMgmt.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjWareHouseMgmt.Plant_Id);
            hashPara.Add("@Warehouse_ID", ObjWareHouseMgmt.Warehouse_ID);
            hashPara.Add("@Storage_Type_ID", ObjWareHouseMgmt.Storage_Type_ID);
            hashPara.Add("@Loading_Equipment_Quantity", ObjWareHouseMgmt.Loading_Equipment_Quantity);
            hashPara.Add("@Loading_Equipment_Quantity1", ObjWareHouseMgmt.Loading_Equipment_Quantity1);
            hashPara.Add("@Loading_Equipment_Quantity2", ObjWareHouseMgmt.Loading_Equipment_Quantity2);
            hashPara.Add("@Unit_Loading_Equip_Quan", ObjWareHouseMgmt.Unit_Loading_Equip_Quan);
            hashPara.Add("@Unit_Loading_Equip_Quan1", ObjWareHouseMgmt.Unit_Loading_Equip_Quan1);
            hashPara.Add("@Unit_Loading_Equip_Quan2", ObjWareHouseMgmt.Unit_Loading_Equip_Quan2);
            hashPara.Add("@Storage_Unit_Type", ObjWareHouseMgmt.Storage_Unit_Type);
            hashPara.Add("@Storage_Unit_Type1", ObjWareHouseMgmt.Storage_Unit_Type1);
            hashPara.Add("@Storage_Unit_Type2", ObjWareHouseMgmt.Storage_Unit_Type2);
            hashPara.Add("@IsActive", ObjWareHouseMgmt.IsActive);
            hashPara.Add("@UserId", ObjWareHouseMgmt.UserId);
            hashPara.Add("@UserIp", ObjWareHouseMgmt.IPAddress);
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

        public WareHouseMgmt2 GetWareHouseMgmt2(int Mat_WareHouse_Mgmt2_Id)
        {
            WareHouseMgmt2 ObjWareHouseMgmt = new WareHouseMgmt2();

            DataAccessLayer objDal = new DataAccessLayer();

            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_WareHouse_Mgmt2_By_MatWareHouseMgmt2Id";
            DataSet ds;

            hashPara.Add("@Mat_WareHouse_Mgmt2_Id", Mat_WareHouse_Mgmt2_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjWareHouseMgmt.Mat_WareHouse_Mgmt2_Id = Convert.ToInt32(dt.Rows[0]["Mat_WareHouse_Mgmt2_Id"].ToString());
                        ObjWareHouseMgmt.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjWareHouseMgmt.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjWareHouseMgmt.Warehouse_ID = dt.Rows[0]["Warehouse_ID"].ToString();
                        ObjWareHouseMgmt.Storage_Type_ID = dt.Rows[0]["Storage_Type_ID"].ToString();
                        ObjWareHouseMgmt.Loading_Equipment_Quantity = dt.Rows[0]["Loading_Equipment_Quantity"].ToString();
                        ObjWareHouseMgmt.Loading_Equipment_Quantity1 = dt.Rows[0]["Loading_Equipment_Quantity1"].ToString();
                        ObjWareHouseMgmt.Loading_Equipment_Quantity2 = dt.Rows[0]["Loading_Equipment_Quantity2"].ToString();
                        ObjWareHouseMgmt.Unit_Loading_Equip_Quan = dt.Rows[0]["Unit_Loading_Equip_Quan"].ToString();
                        ObjWareHouseMgmt.Unit_Loading_Equip_Quan1 = dt.Rows[0]["Unit_Loading_Equip_Quan1"].ToString();
                        ObjWareHouseMgmt.Unit_Loading_Equip_Quan2 = dt.Rows[0]["Unit_Loading_Equip_Quan2"].ToString();
                        ObjWareHouseMgmt.Storage_Unit_Type = dt.Rows[0]["Storage_Unit_Type"].ToString();
                        ObjWareHouseMgmt.Storage_Unit_Type1 = dt.Rows[0]["Storage_Unit_Type1"].ToString();
                        ObjWareHouseMgmt.Storage_Unit_Type2 = dt.Rows[0]["Storage_Unit_Type2"].ToString();
                    }
                }
                return ObjWareHouseMgmt;
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

        public DataSet GetWareHouseMgmtData2(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_WareHouse_Mgmt2_By_MasterHeaderId";
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


        #endregion
    }
}