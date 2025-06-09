using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using Accenture.MWT.DomainObject;
using System.Configuration;
using System.Web.Configuration;

namespace Accenture.MWT.DataAccess
{
    public class TANExemptionAccess 
    {

        /// <summary>
        /// create new temp row in grid details
        /// 8400000388
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int GetTempId(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetTANTempId";
            int result = 0;

            hashPara.Add("@MasterHeaderId", MasterHeaderId);

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

        /// <summary>
        /// Save and update records details in DB done
        /// 8400000388
        /// </summary>
        /// <param name="objZcapHsn"></param>
        /// <returns></returns>
        public int SaveDetails(TANExemptions objtan)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_TAN";
            int result = 0;

            hashPara.Add("@Pk_TANId", objtan.Pk_TANId);
            hashPara.Add("@Master_Header_Id", objtan.Master_Header_Id);
            hashPara.Add("@sSectionCode", objtan.sSectionCode);
            hashPara.Add("@sExemptNum", objtan.sExemptNum);
            hashPara.Add("@sExemptRate", objtan.sExemptRate);
            hashPara.Add("@dExemptFrom", objtan.dExemptFrom);
            hashPara.Add("@dExemptTo", objtan.dExemptTo);
            hashPara.Add("@sExemptReason", objtan.sExemptReason);
            hashPara.Add("@sWHTType", objtan.sWHTType);
            hashPara.Add("@sWtaxCode", objtan.sWtaxCode);
            hashPara.Add("@sExemThreshold", objtan.sExemThreshold);
            hashPara.Add("@sCurrency", objtan.sCurrency); 

            hashPara.Add("@bIsActive", objtan.IsActive);
            hashPara.Add("@nCreatedBy", objtan.CreatedBy);
            hashPara.Add("@sCreatedIp", objtan.CreatedIp);

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

        /// <summary>
        /// delete records from table base on id and master header id done
        /// 8400000388
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int DeleteRow(int id, int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_T_Vendor_TAN_By_Pk_TANId";
            int result = 0;

            hashPara.Add("@Pk_TANId", id);
            hashPara.Add("@MasterHeaderId", MasterHeaderId);
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


        /// <summary>
        /// Get details data by master header id done
        /// 8400000388
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public DataSet GetDetailsData(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_Vendor_TAN_By_Master_Header_Id";
            DataSet ds;

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