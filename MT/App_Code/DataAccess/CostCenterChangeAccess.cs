using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CostCenterChangeAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class CostCenterChangeAccess
    {
        public CostCenterChangeAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(CostCenterChange ObjCostCenterChange)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_CostCenter_Change";
            int result = 0;


            hashPara.Add("@CostCenter_Change_Id", ObjCostCenterChange.CostCenter_Change_Id);
            hashPara.Add("@Master_Header_Id", ObjCostCenterChange.Master_Header_Id);
            hashPara.Add("@Cost_Center", ObjCostCenterChange.Cost_Center);
            hashPara.Add("@Cost_Center_Name", ObjCostCenterChange.Cost_Center_Name);

            hashPara.Add("@CostCenter_Change_Detail_Id", ObjCostCenterChange.CostCenter_Change_Detail_Id);
            hashPara.Add("@Section_Id", ObjCostCenterChange.Section_Id);
            hashPara.Add("@Section_Field_Master_Id", ObjCostCenterChange.Section_Field_Master_Id);
            hashPara.Add("@Old_Value", ObjCostCenterChange.Old_Value);
            hashPara.Add("@New_Value", ObjCostCenterChange.New_Value);

            hashPara.Add("@CreatedBy", ObjCostCenterChange.UserId);
            hashPara.Add("@CreatedIP", ObjCostCenterChange.IPAddress);

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

        public int Save(CostCenterChangeDetail ObjCostCenterChange)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_CostCenter_Change_Detail";
            int result = 0;


            hashPara.Add("@CostCenter_Change_Detail_Id", ObjCostCenterChange.CostCenter_Change_Detail_Id);
            hashPara.Add("@CostCenter_Change_Id", ObjCostCenterChange.CostCenter_Change_Id);
            hashPara.Add("@Section_Id", ObjCostCenterChange.Section_Id);
            hashPara.Add("@Section_Field_Master_Id", ObjCostCenterChange.Section_Field_Master_Id);
            hashPara.Add("@Old_Value", ObjCostCenterChange.Old_Value);
            hashPara.Add("@New_Value", ObjCostCenterChange.New_Value);
            hashPara.Add("@CreatedBy", ObjCostCenterChange.UserId);
            hashPara.Add("@CreatedIP", ObjCostCenterChange.IPAddress);

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

        public CostCenterChange GetCostCenterChange(int CostCenter_Change_Id)
        {
            CostCenterChange ObjCostCenterChange = new CostCenterChange();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CostCenter_Change_By_CostCenterChangeId";
            DataSet ds;

            hashPara.Add("@CostCenter_Change_Id", CostCenter_Change_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCostCenterChange.CostCenter_Change_Id = Convert.ToInt32(dt.Rows[0]["CostCenter_Change_Id"].ToString());
                        ObjCostCenterChange.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjCostCenterChange.Cost_Center = dt.Rows[0]["Cost_Center"].ToString();
                        ObjCostCenterChange.Cost_Center_Name = dt.Rows[0]["Cost_Center_Name"].ToString();
                    }
                }
                return ObjCostCenterChange;
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

        public CostCenterChangeDetail GetCostCenterChangeDetail(int Master_Header_Id, int CostCenter_Change_Detail_Id)
        {
            CostCenterChangeDetail ObjCostCenterChange = new CostCenterChangeDetail();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CostCenter_Change_Detail_By_CostCenterChangeDetailId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@CostCenter_Change_Detail_Id", CostCenter_Change_Detail_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCostCenterChange.CostCenter_Change_Detail_Id = Convert.ToInt32(dt.Rows[0]["CostCenter_Change_Detail_Id"].ToString());
                        ObjCostCenterChange.CostCenter_Change_Id = Convert.ToInt32(dt.Rows[0]["CostCenter_Change_Id"].ToString());
                        ObjCostCenterChange.Section_Id = Convert.ToInt32(dt.Rows[0]["Section_Id"].ToString());
                        ObjCostCenterChange.Section_Field_Master_Id = Convert.ToInt32(dt.Rows[0]["Section_Field_Master_Id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            ObjCostCenterChange.Field = dt.Rows[0]["Section_Field_Master_Id"].ToString();
                            ObjCostCenterChange.Old_Value = dt.Rows[0]["Old_Value"].ToString();
                            ObjCostCenterChange.New_Value = dt.Rows[0]["New_Value"].ToString();
                        }
                        if (dt.Rows.Count > 1)
                        {
                            ObjCostCenterChange.Field2 = dt.Rows[1]["Section_Field_Master_Id"].ToString();
                            ObjCostCenterChange.Old_Value2 = dt.Rows[1]["Old_Value"].ToString();
                            ObjCostCenterChange.New_Value2 = dt.Rows[1]["New_Value"].ToString();
                        }
                        if (dt.Rows.Count > 2)
                        {
                            ObjCostCenterChange.Field3 = dt.Rows[2]["Section_Field_Master_Id"].ToString();
                            ObjCostCenterChange.Old_Value3 = dt.Rows[2]["Old_Value"].ToString();
                            ObjCostCenterChange.New_Value3 = dt.Rows[2]["New_Value"].ToString();
                        }
                        if (dt.Rows.Count > 3)
                        {
                            ObjCostCenterChange.Field4 = dt.Rows[3]["Section_Field_Master_Id"].ToString();
                            ObjCostCenterChange.Old_Value4 = dt.Rows[3]["Old_Value"].ToString();
                            ObjCostCenterChange.New_Value4 = dt.Rows[3]["New_Value"].ToString();
                        }
                        if (dt.Rows.Count > 4)
                        {
                            ObjCostCenterChange.Field5 = dt.Rows[4]["Section_Field_Master_Id"].ToString();
                            ObjCostCenterChange.Old_Value5 = dt.Rows[4]["Old_Value"].ToString();
                            ObjCostCenterChange.New_Value5 = dt.Rows[4]["New_Value"].ToString();
                        }
                    }
                }
                return ObjCostCenterChange;
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

        public int DeleteCostCenterChangeDetail(string CostCenter_Change_Detail_Id)
        {
            CostCenterChangeDetail ObjCostCenterChange = new CostCenterChangeDetail();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_CostCenter_Change_Detail_By_CostCenterChangeDetailId";
            int result = 0;

            hashPara.Add("@CostCenter_Change_Detail_Id", CostCenter_Change_Detail_Id);

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

        public DataSet GetCostCenterChangeData(string MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CostCenter_Change_By_MasterHeaderId";
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

        public DataSet GetCostCenterChangeDetailData(int CostCenter_Change_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CostCenter_Change_Detail_By_CostCenterChangeId";
            DataSet ds;

            hashPara.Add("@CostCenter_Change_Id", CostCenter_Change_Id);

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