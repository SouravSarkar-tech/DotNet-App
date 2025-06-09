using Accenture.MWT.DomainObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserFieldsMasterAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class UserFieldsMasterAccess
    {
        /// <summary>
        /// Get details data by master header id
        /// </summary>
        /// <param name="Master_Header_Id"></param>
        /// <returns></returns>
        public DataSet GetDetailsData(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_T_BOM_UserFieldsTB_By_Master_Header_Id";
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

        /// <summary>
        /// delete records from table base on id and master header id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int DeleteRow(int id, int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_T_BOM_UserFieldsTB_By_Id";
            int result = 0;

            hashPara.Add("@Pk_BOM_UserFieldsId", id);
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
        /// create new temp row in grid details
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public int GetTempId(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetTempUFId";
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
        /// Save and update records details in DB
        /// </summary>
        /// <param name="objBOMuf"></param>
        /// <returns></returns>
        public int SaveDetails(BOMUserFields objBOMuf)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_BOM_UserFieldsTB";
            int result = 0;

            hashPara.Add("@Pk_BOM_UserFieldsId", objBOMuf.Pk_BOM_UserFieldsId);
            hashPara.Add("@Master_Header_Id", objBOMuf.Master_Header_Id);
            hashPara.Add("@sActivity", objBOMuf.sActivity);
            hashPara.Add("@sFieldkey", objBOMuf.sFieldkey);
            hashPara.Add("@sGFText1", objBOMuf.sGFText1);
            hashPara.Add("@sGFText2", objBOMuf.sGFText2);
            hashPara.Add("@sGFText3", objBOMuf.sGFText3);
            hashPara.Add("@sGFText4", objBOMuf.sGFText4);
            hashPara.Add("@sNFQty1", objBOMuf.sNFQty1);
            hashPara.Add("@sNFQty2", objBOMuf.sNFQty2);
            hashPara.Add("@sNFValue1", objBOMuf.sNFValue1);
            hashPara.Add("@sNFValue2", objBOMuf.sNFValue2);
            hashPara.Add("@dDTdate1", objBOMuf.dDTdate1);
            hashPara.Add("@dDTdate2", objBOMuf.dDTdate2);
            hashPara.Add("@bCBKX_Sche", objBOMuf.bCBKX_Sche);
            hashPara.Add("@bCBIndicator", objBOMuf.bCBIndicator); 

            hashPara.Add("@IsActive", objBOMuf.IsActive);
            hashPara.Add("@CreatedBy", objBOMuf.CreatedBy);
            hashPara.Add("@CreatedBy_IP", objBOMuf.CreatedBy_IP);

            hashPara.Add("@sQUNIT1", objBOMuf.sQUNIT1);
            hashPara.Add("@sQUNIT2", objBOMuf.sQUNIT2);
            hashPara.Add("@sVUNIT1", objBOMuf.sVUNIT1);
            hashPara.Add("@sVUNIT2", objBOMuf.sVUNIT2);

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
         

    }
}