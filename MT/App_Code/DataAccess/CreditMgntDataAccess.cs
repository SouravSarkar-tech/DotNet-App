using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CreditMgntDataAccess
/// </summary>
/// 
namespace Accenture.MWT.DataAccess
{
    public class CreditMgntDataAccess
    {
        Utility ObjUtil = new Utility();

        public CreditMgntDataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(CreaditMgntData ObjCustCrdtMgmt)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_CreditMgnt_Data";
            int result = 0;


            hashPara.Add("@CreditMgnt_Data_Id", ObjCustCrdtMgmt.CreditMgnt_Data_Id);
            hashPara.Add("@Master_Header_Id", ObjCustCrdtMgmt.Master_Header_Id);

            hashPara.Add("@Credit_Control", ObjCustCrdtMgmt.Credit_Control);
            hashPara.Add("@Total_Amt", ObjCustCrdtMgmt.Total_Amt);
            hashPara.Add("@Individual_Limit", ObjCustCrdtMgmt.Individual_Limit);         
            hashPara.Add("@Customer_Acc_No", ObjCustCrdtMgmt.Customer_Acc_No);
            hashPara.Add("@Date_Batch", ObjCustCrdtMgmt.Date_Batch);
            hashPara.Add("@Indicator_Blocked", ObjCustCrdtMgmt.Indicator_Blocked);
            hashPara.Add("@Credit_Group", ObjCustCrdtMgmt.Credit_Group);
            hashPara.Add("@Date_Batch1", ObjCustCrdtMgmt.Date_Batch1);
            hashPara.Add("@Credit_info_number", ObjCustCrdtMgmt.Credit_info_number);
            hashPara.Add("@Date_Batch2", ObjCustCrdtMgmt.Date_Batch2);
            hashPara.Add("@Cust_Credit_Group", ObjCustCrdtMgmt.Cust_Credit_Group);
            hashPara.Add("@Date_Batch3", ObjCustCrdtMgmt.Date_Batch3);
            hashPara.Add("@Customer_Group", ObjCustCrdtMgmt.Customer_Group);
            hashPara.Add("@Reco_credit_limit", ObjCustCrdtMgmt.Reco_credit_limit);
            hashPara.Add("@Currency_recommend", ObjCustCrdtMgmt.Currency_recommend);
            hashPara.Add("@Date_Batch4", ObjCustCrdtMgmt.Date_Batch4);
           
            hashPara.Add("@IsActive", ObjCustCrdtMgmt.IsActive);
            hashPara.Add("@UserId", ObjCustCrdtMgmt.UserId);
            hashPara.Add("@UserIp", ObjCustCrdtMgmt.IPAddress);

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

        public CreaditMgntData GetCreditMgntData(int intMasterHeaderId)
        {
            CreaditMgntData ObjCustCrdtMgmt = new CreaditMgntData();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CreditMgnt_Data_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", intMasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCustCrdtMgmt.CreditMgnt_Data_Id = Convert.ToInt32(dt.Rows[0]["CreditMgnt_Data_Id"].ToString());
                        ObjCustCrdtMgmt.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());  

                        ObjCustCrdtMgmt.Credit_Control = dt.Rows[0]["Credit_Control"].ToString();
                        ObjCustCrdtMgmt.Total_Amt = dt.Rows[0]["Total_Amt"].ToString();
                        ObjCustCrdtMgmt.Individual_Limit = dt.Rows[0]["Individual_Limit"].ToString();

                        
                        ObjCustCrdtMgmt.Customer_Acc_No = dt.Rows[0]["Customer_Acc_No"].ToString();
                        
                        ObjCustCrdtMgmt.Date_Batch = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date_Batch"].ToString());
                        ObjCustCrdtMgmt.Indicator_Blocked = dt.Rows[0]["Indicator_Blocked"].ToString();
                        ObjCustCrdtMgmt.Credit_Group = dt.Rows[0]["Credit_Group"].ToString();
                        ObjCustCrdtMgmt.Date_Batch1 =  ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date_Batch1"].ToString());
                        ObjCustCrdtMgmt.Credit_info_number = dt.Rows[0]["Credit_info_number"].ToString();
                        ObjCustCrdtMgmt.Date_Batch2 =  ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date_Batch2"].ToString());
                        ObjCustCrdtMgmt.Cust_Credit_Group = dt.Rows[0]["Cust_Credit_Group"].ToString();
                        ObjCustCrdtMgmt.Date_Batch3 =  ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date_Batch3"].ToString());
                        ObjCustCrdtMgmt.Customer_Group = dt.Rows[0]["Customer_Group"].ToString();
                        ObjCustCrdtMgmt.Reco_credit_limit = dt.Rows[0]["Reco_credit_limit"].ToString();
                        ObjCustCrdtMgmt.Currency_recommend = dt.Rows[0]["Currency_recommend"].ToString();
                        ObjCustCrdtMgmt.Date_Batch4 =  ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date_Batch4"].ToString());
                    }
                }
                return ObjCustCrdtMgmt;
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