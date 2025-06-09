using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for MRPDataAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class MRPDataAccess
    {
        public MRPDataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(MRP1 ObjMRP)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_MRP1";
            int result = 0;

            hashPara.Add("@Mat_MRP1_Id", ObjMRP.Mat_MRP1_Id);
            hashPara.Add("@Master_Header_Id", ObjMRP.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjMRP.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMRP.Storage_Location);
            hashPara.Add("@Base_Unit_Of_Measure", ObjMRP.Base_Unit_Of_Measure);
            hashPara.Add("@Purchasing_Group", ObjMRP.Purchasing_Group);
            hashPara.Add("@MRP_Type", ObjMRP.MRP_Type);
            hashPara.Add("@MRP_Controller", ObjMRP.MRP_Controller);
            hashPara.Add("@Reorder_Point", ObjMRP.Reorder_Point);
            hashPara.Add("@Lot_Size", ObjMRP.Lot_Size);
            hashPara.Add("@Min_Lot_Size", ObjMRP.Min_Lot_Size);
            hashPara.Add("@Max_Lot_Size", ObjMRP.Max_Lot_Size);
            hashPara.Add("@Fixed_Lot_Size", ObjMRP.Fixed_Lot_Size);
            hashPara.Add("@Rounding_Value", ObjMRP.Rounding_Value);
            hashPara.Add("@Max_Stock_Level", ObjMRP.Max_Stock_Level);
            hashPara.Add("@ABC_Indicator", ObjMRP.ABC_Indicator);
            hashPara.Add("@Scrap", ObjMRP.Scrap);
            hashPara.Add("@Planning_Time_Fence", ObjMRP.Planning_Time_Fence);
            hashPara.Add("@Production_Unit", ObjMRP.Production_Unit);
            hashPara.Add("@MRP_Group", ObjMRP.MRP_Group);
            hashPara.Add("@Takt_Time", ObjMRP.Takt_Time);
            hashPara.Add("@Planning_Cycle", ObjMRP.Planning_Cycle);
            hashPara.Add("@Rounding_Profile", ObjMRP.Rounding_Profile);
            hashPara.Add("@Unit_Measure_Grp", ObjMRP.Unit_Measure_Grp);
            hashPara.Add("@IsActive", ObjMRP.IsActive);
            hashPara.Add("@UserId", ObjMRP.UserId);
            hashPara.Add("@UserIp", ObjMRP.IPAddress);
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

        public int Save(MRP2 ObjMRP)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_MRP2";
            int result = 0;

            hashPara.Add("@Mat_MRP2_Id", ObjMRP.Mat_MRP2_Id);
            hashPara.Add("@Master_Header_Id", ObjMRP.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjMRP.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMRP.Storage_Location);
            hashPara.Add("@Procurement_Type", ObjMRP.Procurement_Type);
            hashPara.Add("@Spl_Procurement_Type", ObjMRP.Spl_Procurement_Type);
            hashPara.Add("@Proposed_Supply_Area", ObjMRP.Proposed_Supply_Area);
            hashPara.Add("@Planned_Delivery_Time_Days", ObjMRP.Planned_Delivery_Time_Days);
            hashPara.Add("@InHouse_Production_Time", ObjMRP.InHouse_Production_Time);
            hashPara.Add("@Schedule_Margin_Key_Float", ObjMRP.Schedule_Margin_Key_Float);
            hashPara.Add("@Safety_Stock", ObjMRP.Safety_Stock);
            hashPara.Add("@Issue_Storage_Location", ObjMRP.Issue_Storage_Location);
            hashPara.Add("@Range_Coverage_Profile", ObjMRP.Range_Coverage_Profile);
            hashPara.Add("@Indicator_Bulk_Material", ObjMRP.Indicator_Bulk_Material);
            hashPara.Add("@Indicator_BackFlush", ObjMRP.Indicator_BackFlush);
            hashPara.Add("@Default_Storage_Loc_Ext_Proc", ObjMRP.Default_Storage_Loc_Ext_Proc);
            hashPara.Add("@Production_Sched_Profile", ObjMRP.Production_Sched_Profile);
            hashPara.Add("@Safety_Time_Indicator", ObjMRP.Safety_Time_Indicator);
            hashPara.Add("@Safety_Time_WorkDays", ObjMRP.Safety_Time_WorkDays);
            hashPara.Add("@Batch_Entry_Production", ObjMRP.Batch_Entry_Production);
            hashPara.Add("@Indicator_JIT_Delivery", ObjMRP.Indicator_JIT_Delivery);
            hashPara.Add("@Period_Profile_Safety_Time", ObjMRP.Period_Profile_Safety_Time);
            hashPara.Add("@Lower_Limit_Safety_Stock", ObjMRP.Lower_Limit_Safety_Stock);
            hashPara.Add("@Quota_Arrangement_Usage", ObjMRP.Quota_Arrangement_Usage);
            hashPara.Add("@GR_Processing_Time", ObjMRP.GR_Processing_Time);
            hashPara.Add("@Planning_Calander", ObjMRP.Planning_Calander);
            hashPara.Add("@Min_Safety_Stock", ObjMRP.Min_Safety_Stock);
            hashPara.Add("@IsActive", ObjMRP.IsActive);
            hashPara.Add("@UserId", ObjMRP.UserId);
            hashPara.Add("@UserIp", ObjMRP.IPAddress);
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

        public int Save(MRP3 ObjMRP)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_MRP3";
            int result = 0;

            hashPara.Add("@Mat_MRP3_Id", ObjMRP.Mat_MRP3_Id);
            hashPara.Add("@Master_Header_Id", ObjMRP.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjMRP.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMRP.Storage_Location);
            hashPara.Add("@Period_Indicator", ObjMRP.Period_Indicator);
            hashPara.Add("@Fiscal_Year_Variant", ObjMRP.Fiscal_Year_Variant);
            hashPara.Add("@Splitting_Indicator", ObjMRP.Splitting_Indicator);
            hashPara.Add("@Checking_Grp_Availability_Chk", ObjMRP.Checking_Grp_Availability_Chk);
            hashPara.Add("@Consumption_Mode", ObjMRP.Consumption_Mode);
            hashPara.Add("@BackWard_Consumption_Period", ObjMRP.BackWard_Consumption_Period);
            hashPara.Add("@Forward_Consumption_Period", ObjMRP.Forward_Consumption_Period);
            hashPara.Add("@Mixed_MRP_Indicator", ObjMRP.Mixed_MRP_Indicator);
            hashPara.Add("@Replenishment_Lead_Time", ObjMRP.Replenishment_Lead_Time);
            hashPara.Add("@Planning_Material", ObjMRP.Planning_Material);
            hashPara.Add("@Planning_Plant", ObjMRP.Planning_Plant);
            hashPara.Add("@Conv_Factor_Plng_Mat", ObjMRP.Conv_Factor_Plng_Mat);
            hashPara.Add("@Plng_Strategy_Grp", ObjMRP.Plng_Strategy_Grp);
            hashPara.Add("@IsActive", ObjMRP.IsActive);
            hashPara.Add("@UserId", ObjMRP.UserId);
            hashPara.Add("@UserIp", ObjMRP.IPAddress);
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

        public int Save(MRP4 ObjMRP)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_MRP4";
            int result = 0;

            hashPara.Add("@Mat_MRP4_Id", ObjMRP.Mat_MRP4_Id);
            hashPara.Add("@Master_Header_Id", ObjMRP.Master_Header_Id);

            hashPara.Add("@Plant_Id", ObjMRP.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMRP.Storage_Location);
            hashPara.Add("@Selection_Method", ObjMRP.Selection_Method);
            hashPara.Add("@Dependent_Req_Ind", ObjMRP.Dependent_Req_Ind);
            hashPara.Add("@Indicator_Req_Grping", ObjMRP.Indicator_Req_Grping);
            hashPara.Add("@Storage_Loc_MRP_Indicator", ObjMRP.Storage_Loc_MRP_Indicator);
            hashPara.Add("@ReOrder_Pt_Storage_Loc", ObjMRP.ReOrder_Pt_Storage_Loc);
            hashPara.Add("@Fxd_Lot_Size_Storage_Loc", ObjMRP.Fxd_Lot_Size_Storage_Loc);
            hashPara.Add("@Ind_Repetative_Mfg_Allowed", ObjMRP.Ind_Repetative_Mfg_Allowed);
            hashPara.Add("@Component_Scrap_Perc", ObjMRP.Component_Scrap_Perc);
            hashPara.Add("@Discontinuation_Indicator", ObjMRP.Discontinuation_Indicator);
            hashPara.Add("@Effective_Out_Date", ObjMRP.Effective_Out_Date);
            hashPara.Add("@Follow_Up_Mat", ObjMRP.Follow_Up_Mat);
            hashPara.Add("@Spl_Procur_Type_Stro_Loc", ObjMRP.Spl_Procur_Type_Stro_Loc);
            hashPara.Add("@MRP_Relevance_Dep_Req", ObjMRP.MRP_Relevance_Dep_Req);
            hashPara.Add("@Fair_Share_Rule", ObjMRP.Fair_Share_Rule);
            hashPara.Add("@Indi_Push_Distribution", ObjMRP.Indi_Push_Distribution);

            hashPara.Add("@IsActive", ObjMRP.IsActive);
            hashPara.Add("@UserId", ObjMRP.UserId);
            hashPara.Add("@UserIp", ObjMRP.IPAddress);
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


        public MRP1 GetMRP1(int Mat_MRP1_Id)
        {
            MRP1 ObjMRP = new MRP1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP1_By_MatMRP1Id";
            DataSet ds;

            hashPara.Add("@Mat_MRP1_Id", Mat_MRP1_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMRP.Mat_MRP1_Id = Convert.ToInt32(dt.Rows[0]["Mat_MRP1_Id"].ToString());
                        ObjMRP.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjMRP.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMRP.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMRP.Base_Unit_Of_Measure = dt.Rows[0]["Base_Unit_Of_Measure"].ToString();
                        ObjMRP.Purchasing_Group = dt.Rows[0]["Purchasing_Group"].ToString();
                        ObjMRP.MRP_Type = dt.Rows[0]["MRP_Type"].ToString();
                        ObjMRP.MRP_Controller = dt.Rows[0]["MRP_Controller"].ToString();
                        ObjMRP.Reorder_Point = dt.Rows[0]["Reorder_Point"].ToString();
                        ObjMRP.Lot_Size = dt.Rows[0]["Lot_Size"].ToString();
                        ObjMRP.Min_Lot_Size = dt.Rows[0]["Min_Lot_Size"].ToString();
                        ObjMRP.Max_Lot_Size = dt.Rows[0]["Max_Lot_Size"].ToString();
                        ObjMRP.Fixed_Lot_Size = dt.Rows[0]["Fixed_Lot_Size"].ToString();
                        ObjMRP.Rounding_Value = dt.Rows[0]["Rounding_Value"].ToString();
                        ObjMRP.Max_Stock_Level = dt.Rows[0]["Max_Stock_Level"].ToString();
                        ObjMRP.ABC_Indicator = dt.Rows[0]["ABC_Indicator"].ToString();
                        ObjMRP.Scrap = dt.Rows[0]["Scrap"].ToString();
                        ObjMRP.Planning_Time_Fence = dt.Rows[0]["Planning_Time_Fence"].ToString();
                        ObjMRP.Production_Unit = dt.Rows[0]["Production_Unit"].ToString();
                        ObjMRP.MRP_Group = dt.Rows[0]["MRP_Group"].ToString();
                        ObjMRP.Takt_Time = dt.Rows[0]["Takt_Time"].ToString();
                        ObjMRP.Planning_Cycle = dt.Rows[0]["Planning_Cycle"].ToString();
                        ObjMRP.Rounding_Profile = dt.Rows[0]["Rounding_Profile"].ToString();
                        ObjMRP.Unit_Measure_Grp = dt.Rows[0]["Unit_Measure_Grp"].ToString();
                        ObjMRP.IsActive = dt.Rows[0]["IsActive"].ToString().ToLower() == "true" ? 1 : 0;
                    }
                }
                return ObjMRP;
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

        public MRP2 GetMRP2(int Mat_MRP2_Id)
        {
            MRP2 ObjMRP = new MRP2();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP2_By_MatMRP2Id";
            DataSet ds;

            hashPara.Add("@Mat_MRP2_Id", Mat_MRP2_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMRP.Mat_MRP2_Id = Convert.ToInt32(dt.Rows[0]["Mat_MRP2_Id"].ToString());
                        ObjMRP.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjMRP.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMRP.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMRP.Procurement_Type = dt.Rows[0]["Procurement_Type"].ToString();
                        ObjMRP.Spl_Procurement_Type = dt.Rows[0]["Spl_Procurement_Type"].ToString();
                        ObjMRP.Proposed_Supply_Area = dt.Rows[0]["Proposed_Supply_Area"].ToString();
                        ObjMRP.Planned_Delivery_Time_Days = dt.Rows[0]["Planned_Delivery_Time_Days"].ToString();
                        ObjMRP.InHouse_Production_Time = dt.Rows[0]["InHouse_Production_Time"].ToString();
                        ObjMRP.Schedule_Margin_Key_Float = dt.Rows[0]["Schedule_Margin_Key_Float"].ToString();
                        ObjMRP.Safety_Stock = dt.Rows[0]["Safety_Stock"].ToString();
                        ObjMRP.Issue_Storage_Location = dt.Rows[0]["Issue_Storage_Location"].ToString();
                        ObjMRP.Range_Coverage_Profile = dt.Rows[0]["Range_Coverage_Profile"].ToString();
                        ObjMRP.Indicator_Bulk_Material = dt.Rows[0]["Indicator_Bulk_Material"].ToString();
                        ObjMRP.Indicator_BackFlush = dt.Rows[0]["Indicator_BackFlush"].ToString();
                        ObjMRP.Default_Storage_Loc_Ext_Proc = dt.Rows[0]["Default_Storage_Loc_Ext_Proc"].ToString();
                        ObjMRP.Production_Sched_Profile = dt.Rows[0]["Production_Sched_Profile"].ToString();
                        ObjMRP.Safety_Time_Indicator = dt.Rows[0]["Safety_Time_Indicator"].ToString();
                        ObjMRP.Safety_Time_WorkDays = dt.Rows[0]["Safety_Time_WorkDays"].ToString();
                        ObjMRP.Batch_Entry_Production = dt.Rows[0]["Batch_Entry_Production"].ToString();
                        ObjMRP.Indicator_JIT_Delivery = dt.Rows[0]["Indicator_JIT_Delivery"].ToString();
                        ObjMRP.Period_Profile_Safety_Time = dt.Rows[0]["Period_Profile_Safety_Time"].ToString();
                        ObjMRP.Lower_Limit_Safety_Stock = dt.Rows[0]["Lower_Limit_Safety_Stock"].ToString();
                        ObjMRP.Quota_Arrangement_Usage = dt.Rows[0]["Quota_Arrangement_Usage"].ToString();
                        ObjMRP.GR_Processing_Time = dt.Rows[0]["GR_Processing_Time"].ToString();
                        ObjMRP.Planning_Calander = dt.Rows[0]["Planning_Calander"].ToString();
                        ObjMRP.Min_Safety_Stock = dt.Rows[0]["Min_Safety_Stock"].ToString();
                    }
                }
                return ObjMRP;
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

        public MRP3 GetMRP3(int Mat_MRP3_Id)
        {
            MRP3 ObjMRP = new MRP3();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP3_By_MatMRP3Id";
            DataSet ds;

            hashPara.Add("@Mat_MRP3_Id", Mat_MRP3_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMRP.Mat_MRP3_Id = Convert.ToInt32(dt.Rows[0]["Mat_MRP3_Id"].ToString());
                        ObjMRP.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjMRP.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMRP.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMRP.Period_Indicator = dt.Rows[0]["Period_Indicator"].ToString();
                        ObjMRP.Fiscal_Year_Variant = dt.Rows[0]["Fiscal_Year_Variant"].ToString();
                        ObjMRP.Splitting_Indicator = dt.Rows[0]["Splitting_Indicator"].ToString();
                        ObjMRP.Checking_Grp_Availability_Chk = dt.Rows[0]["Checking_Grp_Availability_Chk"].ToString();
                        ObjMRP.Consumption_Mode = dt.Rows[0]["Consumption_Mode"].ToString();
                        ObjMRP.BackWard_Consumption_Period = dt.Rows[0]["BackWard_Consumption_Period"].ToString();
                        ObjMRP.Forward_Consumption_Period = dt.Rows[0]["Forward_Consumption_Period"].ToString();
                        ObjMRP.Mixed_MRP_Indicator = dt.Rows[0]["Mixed_MRP_Indicator"].ToString();
                        ObjMRP.Replenishment_Lead_Time = dt.Rows[0]["Replenishment_Lead_Time"].ToString();
                        ObjMRP.Planning_Material = dt.Rows[0]["Planning_Material"].ToString();
                        ObjMRP.Planning_Plant = dt.Rows[0]["Planning_Plant"].ToString();
                        ObjMRP.Conv_Factor_Plng_Mat = dt.Rows[0]["Conv_Factor_Plng_Mat"].ToString();
                        ObjMRP.Plng_Strategy_Grp = dt.Rows[0]["Plng_Strategy_Grp"].ToString();
                        ObjMRP.IsActive = dt.Rows[0]["IsActive"].ToString().ToLower() == "true" ? 1 : 0;
                    }
                }
                return ObjMRP;
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

        public MRP4 GetMRP4(int Mat_MRP4_Id)
        {
            MRP4 ObjMRP = new MRP4();

            Utility ObjUtil = new Utility();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP4_By_MatMRP4Id";
            DataSet ds;

            hashPara.Add("@Mat_MRP4_Id", Mat_MRP4_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMRP.Mat_MRP4_Id = Convert.ToInt32(dt.Rows[0]["Mat_MRP4_Id"].ToString());
                        ObjMRP.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjMRP.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMRP.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMRP.Selection_Method = dt.Rows[0]["Selection_Method"].ToString();
                        ObjMRP.Dependent_Req_Ind = dt.Rows[0]["Dependent_Req_Ind"].ToString();
                        ObjMRP.Indicator_Req_Grping = dt.Rows[0]["Indicator_Req_Grping"].ToString();
                        ObjMRP.Storage_Loc_MRP_Indicator = dt.Rows[0]["Storage_Loc_MRP_Indicator"].ToString();
                        ObjMRP.ReOrder_Pt_Storage_Loc = dt.Rows[0]["ReOrder_Pt_Storage_Loc"].ToString();
                        ObjMRP.Fxd_Lot_Size_Storage_Loc = dt.Rows[0]["Fxd_Lot_Size_Storage_Loc"].ToString();
                        ObjMRP.Ind_Repetative_Mfg_Allowed = dt.Rows[0]["Ind_Repetative_Mfg_Allowed"].ToString();
                        ObjMRP.Component_Scrap_Perc = dt.Rows[0]["Component_Scrap_Perc"].ToString();
                        ObjMRP.Discontinuation_Indicator = dt.Rows[0]["Discontinuation_Indicator"].ToString();
                        ObjMRP.Effective_Out_Date = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Effective_Out_Date"].ToString());
                        ObjMRP.Follow_Up_Mat = dt.Rows[0]["Follow_Up_Mat"].ToString();
                        ObjMRP.Spl_Procur_Type_Stro_Loc = dt.Rows[0]["Spl_Procur_Type_Stro_Loc"].ToString();
                        ObjMRP.MRP_Relevance_Dep_Req = dt.Rows[0]["MRP_Relevance_Dep_Req"].ToString();
                        ObjMRP.Fair_Share_Rule = dt.Rows[0]["Fair_Share_Rule"].ToString();
                        ObjMRP.Indi_Push_Distribution = dt.Rows[0]["Indi_Push_Distribution"].ToString();
                        ObjMRP.IsActive = dt.Rows[0]["IsActive"].ToString().ToLower() == "true" ? 1 : 0;
                    }
                }
                return ObjMRP;
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
        
        public DataSet GetMRPData1(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP1_By_MasterHeaderId";
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

        public DataSet GetMRPData2(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP2_By_MasterHeaderId";
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

        public DataSet GetMRPData3(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP3_By_MasterHeaderId";
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

        public DataSet GetMRPData4(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MRP4_By_MasterHeaderId";
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
}