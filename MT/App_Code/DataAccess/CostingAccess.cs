using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CostingAccess
/// </summary>

namespace Accenture.MWT.DataAccess
{
    public class CostingAccess
    {
        public CostingAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Costing 1

        public int Save(Costing1 ObjCost)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Costing1";
            int result = 0;


            hashPara.Add("@Mat_Costing1_Id", ObjCost.Mat_Costing1_Id);
            hashPara.Add("@Master_Header_Id", ObjCost.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjCost.Plant_Id);

            hashPara.Add("@Profit_Center", ObjCost.Profit_Center);
            hashPara.Add("@Origin_Group", ObjCost.Origin_Group);
            hashPara.Add("@Material_Related_Origin", ObjCost.Material_Related_Origin);
            hashPara.Add("@Alternative_BOM", ObjCost.Alternative_BOM);
            hashPara.Add("@BOM_Usage", ObjCost.BOM_Usage);
            hashPara.Add("@Key_Task_List_Grp", ObjCost.Key_Task_List_Grp);
            hashPara.Add("@Group_Counter", ObjCost.Group_Counter);
            hashPara.Add("@Task_List_Type", ObjCost.Task_List_Type);
            hashPara.Add("@Lot_Size_Prd_Cost_Est", ObjCost.Lot_Size_Prd_Cost_Est);
            hashPara.Add("@Spl_Procurement_Type", ObjCost.Spl_Procurement_Type);
            hashPara.Add("@Costing_Overhead_Grp", ObjCost.Costing_Overhead_Grp);
            hashPara.Add("@Is_Mat_Costed_Qnty_Struc", ObjCost.Is_Mat_Costed_Qnty_Struc);
            hashPara.Add("@Variance_Key", ObjCost.Variance_Key);
            hashPara.Add("@Do_Not_Cost", ObjCost.Do_Not_Cost);

            hashPara.Add("@IsActive", ObjCost.IsActive);
            hashPara.Add("@UserId", ObjCost.UserId);
            hashPara.Add("@UserIp", ObjCost.IPAddress);
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

        public Costing1 GetCosting1(int Mat_Costing1_Id)
        {
            Costing1 ObjCost = new Costing1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Costing1_By_MatCosting1Id";
            DataSet ds;

            hashPara.Add("@Mat_Costing1_Id", Mat_Costing1_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCost.Mat_Costing1_Id = Convert.ToInt32(dt.Rows[0]["Mat_Costing1_Id"].ToString());
                        ObjCost.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjCost.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjCost.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                        ObjCost.Origin_Group = dt.Rows[0]["Origin_Group"].ToString();
                        ObjCost.Material_Related_Origin = dt.Rows[0]["Material_Related_Origin"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCost.Alternative_BOM = dt.Rows[0]["Alternative_BOM"].ToString();
                        ObjCost.BOM_Usage = dt.Rows[0]["BOM_Usage"].ToString();
                        ObjCost.Key_Task_List_Grp = dt.Rows[0]["Key_Task_List_Grp"].ToString();
                        ObjCost.Group_Counter = dt.Rows[0]["Group_Counter"].ToString();
                        ObjCost.Task_List_Type = dt.Rows[0]["Task_List_Type"].ToString();
                        ObjCost.Lot_Size_Prd_Cost_Est = dt.Rows[0]["Lot_Size_Prd_Cost_Est"].ToString();
                        ObjCost.Spl_Procurement_Type = dt.Rows[0]["Spl_Procurement_Type"].ToString();
                        ObjCost.Costing_Overhead_Grp = dt.Rows[0]["Costing_Overhead_Grp"].ToString();

                        ObjCost.Is_Mat_Costed_Qnty_Struc = dt.Rows[0]["Is_Mat_Costed_Qnty_Struc"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjCost.Variance_Key = dt.Rows[0]["Variance_Key"].ToString();
                        ObjCost.Do_Not_Cost = dt.Rows[0]["Do_Not_Cost"].ToString().ToLower() == "true" ? "1" : "0";

                    }
                }
                return ObjCost;
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

        public DataSet GetCostingData1(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Costing1_By_MasterHeaderId";
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

        #region Costing 2

        public int Save(Costing2 ObjCost)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Costing2";
            int result = 0;


            hashPara.Add("@Mat_Costing2_Id", ObjCost.Mat_Costing2_Id);
            hashPara.Add("@Master_Header_Id", ObjCost.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjCost.Plant_Id);
            hashPara.Add("@Planned_Price1", ObjCost.Planned_Price1);
            hashPara.Add("@Planned_Price_Date1", ObjCost.Planned_Price_Date1);
            hashPara.Add("@Planned_Price2", ObjCost.Planned_Price2);
            hashPara.Add("@Planned_Price_Date2", ObjCost.Planned_Price_Date2);
            hashPara.Add("@Planned_Price3", ObjCost.Planned_Price3);
            hashPara.Add("@Planned_Price_Date3", ObjCost.Planned_Price_Date3);
            hashPara.Add("@IsActive", ObjCost.IsActive);
            hashPara.Add("@UserId", ObjCost.UserId);
            hashPara.Add("@UserIp", ObjCost.IPAddress);
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

        public Costing2 GetCosting2(int Mat_Costing2_Id)
        {
            Costing2 ObjCost = new Costing2();

            DataAccessLayer objDal = new DataAccessLayer();

            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Costing2_By_MatCosting2Id";
            DataSet ds;

            hashPara.Add("@Mat_Costing2_Id", Mat_Costing2_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCost.Mat_Costing2_Id = Convert.ToInt32(dt.Rows[0]["Mat_Costing2_Id"].ToString());
                        ObjCost.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjCost.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjCost.Planned_Price1 = dt.Rows[0]["Planned_Price1"].ToString();
                        ObjCost.Planned_Price_Date1 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Planned_Price_Date1"].ToString());
                        ObjCost.Planned_Price2 = dt.Rows[0]["Planned_Price2"].ToString();
                        ObjCost.Planned_Price_Date2 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Planned_Price_Date2"].ToString());
                        ObjCost.Planned_Price3 = dt.Rows[0]["Planned_Price3"].ToString();
                        ObjCost.Planned_Price_Date3 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Planned_Price_Date3"].ToString());
                    }
                }
                return ObjCost;
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

        public DataSet GetCostingData2(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Costing2_By_MasterHeaderId";
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