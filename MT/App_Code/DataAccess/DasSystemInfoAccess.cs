using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for DasSystemInfoAccess
/// </summary>
/// 

namespace Accenture.MWT.DataAccess
{
    public class DasSystemInfoAccess
    {
        Utility ObjUtil = new Utility();

        public DasSystemInfoAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(DasSystemInfo ObjCustGeneral)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_CUST_DAS_SYSTEM_INFO";
            int result = 0;

            hashPara.Add("@ID", ObjCustGeneral.ID);
            hashPara.Add("@Master_Header_Id", ObjCustGeneral.Master_Header_Id);

            hashPara.Add("@Depot", ObjCustGeneral.Depot);
            hashPara.Add("@Division", ObjCustGeneral.Division);
            hashPara.Add("@Territory", ObjCustGeneral.Territory);
            hashPara.Add("@Structure_Of_Firm", ObjCustGeneral.Structure_Of_Firm);

            hashPara.Add("@NameOfProprietor", ObjCustGeneral.NameOfProprietor);
            hashPara.Add("@Bank_Name", ObjCustGeneral.Bank_Name);
            hashPara.Add("@Bank_Addr", ObjCustGeneral.Bank_Addr);
            hashPara.Add("@Avail_Cheque", ObjCustGeneral.Avail_Cheque);
            hashPara.Add("@Transporter_Name", ObjCustGeneral.Transporter_Name);
            hashPara.Add("@Add_Or_Replacement", ObjCustGeneral.Add_Or_Replacement);
            hashPara.Add("@Years_Pharma_Distr", ObjCustGeneral.Years_Pharma_Distr);
            hashPara.Add("@Channel_Of_Distr", ObjCustGeneral.Channel_Of_Distr);

            hashPara.Add("@Company_Party_Distr_Id", ObjCustGeneral.Master_Header_Id + "_CPDI");

            hashPara.Add("@Turnover_Three_Years", ObjCustGeneral.Turnover_Three_Years);
            hashPara.Add("@Bank_Stmt_Submitted", ObjCustGeneral.Bank_Stmt_Submitted);
            hashPara.Add("@Expected_Monthly_Sales", ObjCustGeneral.Expected_Monthly_Sales);
            hashPara.Add("@Justification_For_Appt", ObjCustGeneral.Justification_For_Appt);
            hashPara.Add("@Cur_Territory_Sales", ObjCustGeneral.Cur_Territory_Sales);
            hashPara.Add("@Dist_In_Territory", ObjCustGeneral.Dist_In_Territory);
            hashPara.Add("@Sales_Ratio_Per_Dist", ObjCustGeneral.Sales_Ratio_Per_Dist);
            hashPara.Add("@StockValue", ObjCustGeneral.StockValue);
            hashPara.Add("@Outstanding_0_30", ObjCustGeneral.Outstanding_0_30);
            hashPara.Add("@Outstanding_31_60", ObjCustGeneral.Outstanding_31_60);
            hashPara.Add("@Outstanding_61_90", ObjCustGeneral.Outstanding_61_90);
            hashPara.Add("@Outstanding_91_180", ObjCustGeneral.Outstanding_91_180);
            hashPara.Add("@Outstanding_Age_180", ObjCustGeneral.Outstanding_Age_180);
            hashPara.Add("@Outstanding_Replacement", ObjCustGeneral.Outstanding_Replacement);
            hashPara.Add("@Feeback", ObjCustGeneral.Feeback);
            hashPara.Add("@UserId", ObjCustGeneral.UserId);
            hashPara.Add("@UserIp", ObjCustGeneral.IPAddress);
            //Added By Nitin R , SDT03052019 , Des : to check request type for Customer IRF
            hashPara.Add("@RDMLoggedInUserDeptId", ObjCustGeneral.RDMLoggedInUserDeptId);
            //Added By Nitin R , EDT03052019 , Des : to check request type for Customer IRF

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

        public int SaveDetail(DasSystemInfoDetail objDasDetail)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_Company_Party_Distr_Detail";
            int result = 0;

            hashPara.Add("@ID", objDasDetail.ID);
            hashPara.Add("@Company_Party_Distr_Id", objDasDetail.Company_Party_Distr_Id);

            hashPara.Add("@Company_Name", objDasDetail.Company_Name);
            hashPara.Add("@Monthly_Turnover", objDasDetail.Monthly_Turnover);
            
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

        public DasSystemInfo GetCustomerGeneral1(int intMasterHeaderId)
        {
            DasSystemInfo ObjCustGeneral = new DasSystemInfo();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_CUST_DAS_SYSTEM_INFO";
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
                        ObjCustGeneral.ID = dt.Rows[0]["ID"].ToString();
                        ObjCustGeneral.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString();
                        ObjCustGeneral.Depot = dt.Rows[0]["Depot"].ToString();
                        ObjCustGeneral.Division = dt.Rows[0]["Division"].ToString();
                        ObjCustGeneral.Territory = dt.Rows[0]["Territory"].ToString();
                        ObjCustGeneral.Structure_Of_Firm = dt.Rows[0]["Structure_Of_Firm"].ToString();

                        ObjCustGeneral.NameOfProprietor = dt.Rows[0]["NameOfProprietor"].ToString();
                        ObjCustGeneral.Bank_Name = dt.Rows[0]["Bank_Name"].ToString();
                        ObjCustGeneral.Bank_Addr = dt.Rows[0]["Bank_Addr"].ToString();
                        ObjCustGeneral.Avail_Cheque = dt.Rows[0]["Avail_Cheque"].ToString();
                        ObjCustGeneral.Transporter_Name = dt.Rows[0]["Transporter_Name"].ToString();
                        ObjCustGeneral.Add_Or_Replacement = dt.Rows[0]["Add_Or_Replacement"].ToString();
                        ObjCustGeneral.Years_Pharma_Distr = dt.Rows[0]["Years_Pharma_Distr"].ToString();
                        ObjCustGeneral.Turnover_Three_Years = dt.Rows[0]["Turnover_Three_Years"].ToString();
                        ObjCustGeneral.Bank_Stmt_Submitted = dt.Rows[0]["Bank_Stmt_Submitted"].ToString();
                        ObjCustGeneral.Expected_Monthly_Sales = dt.Rows[0]["Expected_Monthly_Sales"].ToString();
                        ObjCustGeneral.Justification_For_Appt = dt.Rows[0]["Justification_For_Appt"].ToString();
                        ObjCustGeneral.Cur_Territory_Sales = dt.Rows[0]["Cur_Territory_Sales"].ToString();
                        ObjCustGeneral.Dist_In_Territory = dt.Rows[0]["Dist_In_Territory"].ToString();
                        ObjCustGeneral.Sales_Ratio_Per_Dist = dt.Rows[0]["Sales_Ratio_Per_Dist"].ToString();
                        ObjCustGeneral.StockValue = dt.Rows[0]["StockValue"].ToString();
                        ObjCustGeneral.Outstanding_0_30 = dt.Rows[0]["Outstanding_0_30"].ToString();
                        ObjCustGeneral.Outstanding_31_60 = dt.Rows[0]["Outstanding_31_60"].ToString();
                        ObjCustGeneral.Outstanding_61_90 = dt.Rows[0]["Outstanding_61_90"].ToString();
                        ObjCustGeneral.Outstanding_91_180 = dt.Rows[0]["Outstanding_91_180"].ToString();
                        ObjCustGeneral.Outstanding_Age_180 = dt.Rows[0]["Outstanding_Age_180"].ToString();
                        ObjCustGeneral.Outstanding_Replacement = dt.Rows[0]["Outstanding_Replacement"].ToString();
                        ObjCustGeneral.Feeback = dt.Rows[0]["Feeback"].ToString();
                        ObjCustGeneral.Channel_Of_Distr = dt.Rows[0]["Channel_Of_Distr"].ToString();
                    }
                }
                return ObjCustGeneral;
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
            string procName = "pr_Fill_Company_Party_Distr_Detail";
            hashPara.Add("@Master_Header_id", Master_Header_id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }

        public DataSet FillDeliveringPlantData(string Master_Header_id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Delivering_Plant_List";
            hashPara.Add("@Master_Header_id", Master_Header_id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }
    }
}