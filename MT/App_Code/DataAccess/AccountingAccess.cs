using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

namespace Accenture.MWT.DataAccess
{
    public class AccountingAccess
    {
        public AccountingAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(Accounting1 ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Accounting1";
            int result = 0;


            hashPara.Add("@Mat_Accounting1_Id", ObjAcc.Mat_Accounting1_Id);
            hashPara.Add("@Master_Header_Id", ObjAcc.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjAcc.Plant_Id);

            hashPara.Add("@Valuation_Type", ObjAcc.Valuation_Type);
            hashPara.Add("@Valuation_Category", ObjAcc.Valuation_Category);
            hashPara.Add("@Valuation_Class", ObjAcc.Valuation_Class);
            hashPara.Add("@Price_Ctrl_Indicator", ObjAcc.Price_Ctrl_Indicator);
            hashPara.Add("@Moving_Avg_Price", ObjAcc.Moving_Avg_Price);
            hashPara.Add("@Standard_Price", ObjAcc.Standard_Price);
            hashPara.Add("@Price_Unit", ObjAcc.Price_Unit);

            hashPara.Add("@IsActive", ObjAcc.IsActive);
            hashPara.Add("@UserId", ObjAcc.UserId);
            hashPara.Add("@UserIp", ObjAcc.IPAddress);
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

        public int Save(Accounting2 ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Accounting2";
            int result = 0;


            hashPara.Add("@Mat_Accounting2_Id", ObjAcc.Mat_Accounting2_Id);
            hashPara.Add("@Master_Header_Id", ObjAcc.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjAcc.Plant_Id);

            hashPara.Add("@Commercial_Price1", ObjAcc.Commercial_Price1);
            hashPara.Add("@Commercial_Price2", ObjAcc.Commercial_Price2);
            hashPara.Add("@Commercial_Price3", ObjAcc.Commercial_Price3);

            hashPara.Add("@Tax_Price1", ObjAcc.Tax_Price1);
            hashPara.Add("@Tax_Price2", ObjAcc.Tax_Price2);
            hashPara.Add("@Tax_Price3", ObjAcc.Tax_Price3);

            hashPara.Add("@Relevant", ObjAcc.Relevant);
            hashPara.Add("@Pool_No_LIFO_Valuation", ObjAcc.Pool_No_LIFO_Valuation);
            hashPara.Add("@IsActive", ObjAcc.IsActive);
            hashPara.Add("@UserId", ObjAcc.UserId);
            hashPara.Add("@UserIp", ObjAcc.IPAddress);
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

        public Accounting1 GetAccounting1(int Mat_Accounting1_Id)
        {
            Accounting1 ObjAcc = new Accounting1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Accounting1_By_MatAccounting1Id";
            DataSet ds;

            hashPara.Add("@Mat_Accounting1_Id", Mat_Accounting1_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjAcc.Mat_Accounting1_Id = Convert.ToInt32(dt.Rows[0]["Mat_Accounting1_Id"].ToString());
                        ObjAcc.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjAcc.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();

                        ObjAcc.Valuation_Type = dt.Rows[0]["Valuation_Type"].ToString();
                        ObjAcc.Valuation_Category = dt.Rows[0]["Valuation_Category"].ToString();
                        ObjAcc.Valuation_Class = dt.Rows[0]["Valuation_Class"].ToString();
                        ObjAcc.Price_Ctrl_Indicator = dt.Rows[0]["Price_Ctrl_Indicator"].ToString();
                        ObjAcc.Moving_Avg_Price = dt.Rows[0]["Moving_Avg_Price"].ToString();
                        ObjAcc.Standard_Price = dt.Rows[0]["Standard_Price"].ToString();
                        ObjAcc.Price_Unit = dt.Rows[0]["Price_Unit"].ToString();
                    }
                }
                return ObjAcc;
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

        public Accounting2 GetAccounting2(int Mat_Accounting2_Id)
        {
            Accounting2 ObjAcc = new Accounting2();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Accounting2_By_MatAccounting2Id";
            DataSet ds;

            hashPara.Add("@Mat_Accounting2_Id", Mat_Accounting2_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjAcc.Mat_Accounting2_Id = Convert.ToInt32(dt.Rows[0]["Mat_Accounting2_Id"].ToString());
                        ObjAcc.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjAcc.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjAcc.Commercial_Price1 = dt.Rows[0]["Commercial_Price1"].ToString();
                        ObjAcc.Commercial_Price2 = dt.Rows[0]["Commercial_Price2"].ToString();
                        ObjAcc.Commercial_Price3 = dt.Rows[0]["Commercial_Price3"].ToString();

                        ObjAcc.Tax_Price1 = dt.Rows[0]["Tax_Price1"].ToString();
                        ObjAcc.Tax_Price2 = dt.Rows[0]["Tax_Price2"].ToString();
                        ObjAcc.Tax_Price3 = dt.Rows[0]["Tax_Price3"].ToString();

                        ObjAcc.Relevant = dt.Rows[0]["Relevant"].ToString().ToLower() == "true" ? 1 : 0;
                        ObjAcc.Pool_No_LIFO_Valuation = dt.Rows[0]["Pool_No_LIFO_Valuation"].ToString();
                    }
                }
                return ObjAcc;
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

        public DataSet GetAccountingData1(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Accounting1_By_MasterHeaderId";
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

        public DataSet GetAccountingData2(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Accounting2_By_MasterHeaderId";
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