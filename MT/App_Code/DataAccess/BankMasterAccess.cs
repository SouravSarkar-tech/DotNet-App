using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for BankMasterAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class BankMasterAccess
    {
        public BankMasterAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int Save(BankMaster ObjBankMaster)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_m_Bank_Master";
            int result = 0;


            hashPara.Add("@Bank_Id", ObjBankMaster.Bank_Id);
            hashPara.Add("@Master_Header_Id", ObjBankMaster.Master_Header_Id);
            hashPara.Add("@Bank_Key", ObjBankMaster.Bank_Key);
            hashPara.Add("@Bank_Name", ObjBankMaster.Bank_Name);
            hashPara.Add("@Bank_Branch", ObjBankMaster.Bank_Branch);
            hashPara.Add("@House_No_Street", ObjBankMaster.House_No_Street);
            hashPara.Add("@City", ObjBankMaster.City);
            hashPara.Add("@Country_Id", ObjBankMaster.Country_Id);
            hashPara.Add("@Region_Id", ObjBankMaster.Region_Id);
            hashPara.Add("@Swift", ObjBankMaster.Swift);
            hashPara.Add("@Bank_Number", ObjBankMaster.Bank_Number);
            hashPara.Add("@Bank_Group", ObjBankMaster.Bank_Group);
            hashPara.Add("@Ref_Master_Header_Id", ObjBankMaster.Ref_Master_Header_Id);
            hashPara.Add("@IsActive", ObjBankMaster.IsActive);
            hashPara.Add("@UserId", ObjBankMaster.UserId);
            hashPara.Add("@UserIp", ObjBankMaster.IPAddress);

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

        public BankMaster GetBankMaster(int Bank_Id)
        {
            BankMaster ObjBankMaster = new BankMaster();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetBankDetailsByBankId";
            DataSet ds;

            hashPara.Add("@Bank_Id", Bank_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjBankMaster.Bank_Id = Convert.ToInt32(dt.Rows[0]["Bank_Id"].ToString());
                        ObjBankMaster.Master_Header_Id =dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBankMaster.Bank_Key = dt.Rows[0]["Bank_Key"].ToString();
                        ObjBankMaster.Bank_Name = dt.Rows[0]["Bank_Name"].ToString();
                        ObjBankMaster.Bank_Branch = dt.Rows[0]["Bank_Branch"].ToString();
                        ObjBankMaster.House_No_Street = dt.Rows[0]["House_No_Street"].ToString();
                        ObjBankMaster.City = dt.Rows[0]["City"].ToString();
                        ObjBankMaster.Country_Id = dt.Rows[0]["Country_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString());
                        ObjBankMaster.Region_Id = dt.Rows[0]["Region_Id"].ToString() == "" ? 0 :Convert.ToInt32(dt.Rows[0]["Region_Id"].ToString());
                        ObjBankMaster.Swift = dt.Rows[0]["Swift"].ToString();
                        ObjBankMaster.Bank_Number = dt.Rows[0]["Bank_Number"].ToString();
                        ObjBankMaster.Bank_Group = dt.Rows[0]["Bank_Group"].ToString();
                        ObjBankMaster.Ref_Master_Header_Id = dt.Rows[0]["Ref_Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Ref_Master_Header_Id"].ToString());
                    }
                }
                return ObjBankMaster;
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
        
        public BankMaster GetBankMasterByMasterHeaderId(string Master_Header_Id)
        {
            BankMaster ObjBankMaster = new BankMaster();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetBankDetailsByMasterHeaderId";
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
                        ObjBankMaster.Bank_Id = Convert.ToInt32(dt.Rows[0]["Bank_Id"].ToString());
                        ObjBankMaster.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBankMaster.Bank_Key = dt.Rows[0]["Bank_Key"].ToString();
                        ObjBankMaster.Bank_Name = dt.Rows[0]["Bank_Name"].ToString();
                        ObjBankMaster.Bank_Branch = dt.Rows[0]["Bank_Branch"].ToString();
                        ObjBankMaster.House_No_Street = dt.Rows[0]["House_No_Street"].ToString();
                        ObjBankMaster.City = dt.Rows[0]["City"].ToString();
                        ObjBankMaster.Country_Id = Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString());
                        ObjBankMaster.Region_Id = Convert.ToInt32(dt.Rows[0]["Region_Id"].ToString());
                        ObjBankMaster.Swift = dt.Rows[0]["Swift"].ToString();
                        ObjBankMaster.Bank_Number = dt.Rows[0]["Bank_Number"].ToString();
                        ObjBankMaster.Bank_Group = dt.Rows[0]["Bank_Group"].ToString();
                        ObjBankMaster.Ref_Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Ref_Master_Header_Id"].ToString());
                    }
                }
                return ObjBankMaster;
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

        public BankMaster GetBankMasterByRefMasterHeaderId(string RefMaster_Header_Id)
        {
            BankMaster ObjBankMaster = new BankMaster();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetBankDetailsByRefMasterHeaderId";
            DataSet ds;

            hashPara.Add("@Ref_Master_Header_Id", RefMaster_Header_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjBankMaster.Bank_Id = Convert.ToInt32(dt.Rows[0]["Bank_Id"].ToString());
                        ObjBankMaster.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBankMaster.Bank_Key = dt.Rows[0]["Bank_Key"].ToString();
                        ObjBankMaster.Bank_Name = dt.Rows[0]["Bank_Name"].ToString();
                        ObjBankMaster.Bank_Branch = dt.Rows[0]["Bank_Branch"].ToString();
                        ObjBankMaster.House_No_Street = dt.Rows[0]["House_No_Street"].ToString();
                        ObjBankMaster.City = dt.Rows[0]["City"].ToString();
                        ObjBankMaster.Country_Id = Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString());
                        ObjBankMaster.Region_Id = Convert.ToInt32(dt.Rows[0]["Region_Id"].ToString());
                        ObjBankMaster.Swift = dt.Rows[0]["Swift"].ToString();
                        ObjBankMaster.Bank_Number = dt.Rows[0]["Bank_Number"].ToString();
                        ObjBankMaster.Bank_Group = dt.Rows[0]["Bank_Group"].ToString();
                        ObjBankMaster.Ref_Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Ref_Master_Header_Id"].ToString());
                    }
                }
                return ObjBankMaster;
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

        public DataSet GetBankMasterListByBankKey(string BankKey)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetBankMasterListByBankKey";
            DataSet ds;

            hashPara.Add("@BankKey", BankKey);

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

        public BankMaster GetBankMasterBySwift(string Swift)
        {
            BankMaster ObjBankMaster = new BankMaster();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetBankDetailsBySWIFTCode";
            DataSet ds;

            hashPara.Add("@Swift", Swift);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjBankMaster.Bank_Id = Convert.ToInt32(dt.Rows[0]["Bank_Id"].ToString());
                        ObjBankMaster.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBankMaster.Bank_Key = dt.Rows[0]["Bank_Key"].ToString();
                        ObjBankMaster.Bank_Name = dt.Rows[0]["Bank_Name"].ToString();
                        ObjBankMaster.Bank_Branch = dt.Rows[0]["Bank_Branch"].ToString();
                        ObjBankMaster.House_No_Street = dt.Rows[0]["House_No_Street"].ToString();
                        ObjBankMaster.City = dt.Rows[0]["City"].ToString();
                        ObjBankMaster.Country_Id = dt.Rows[0]["Country_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString());
                        ObjBankMaster.Region_Id = dt.Rows[0]["Region_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Region_Id"].ToString());
                        ObjBankMaster.Swift = dt.Rows[0]["Swift"].ToString();
                        ObjBankMaster.Bank_Number = dt.Rows[0]["Bank_Number"].ToString();
                        ObjBankMaster.Bank_Group = dt.Rows[0]["Bank_Group"].ToString();
                        ObjBankMaster.Ref_Master_Header_Id = dt.Rows[0]["Ref_Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Ref_Master_Header_Id"].ToString());
                    }
                }
                return ObjBankMaster;
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

        public DataSet CheckBankExists(string Bank_Key, string Swift,string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_CheckBankExists";
            DataSet ds;

            hashPara.Add("@Bank_Key", Bank_Key);
            hashPara.Add("@Swift", Swift);
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            
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