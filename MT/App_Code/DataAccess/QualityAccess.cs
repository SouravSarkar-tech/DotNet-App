using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for QualityAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class QualityAccess
    {
        public QualityAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(Quality ObjQuality)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Quality";
            int result = 0;

            hashPara.Add("@Mat_Quality_Id", ObjQuality.Mat_Quality_Id);
            hashPara.Add("@Master_Header_Id", ObjQuality.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjQuality.Plant_Id);
            hashPara.Add("@Unit_Issue", ObjQuality.Unit_Issue);
            hashPara.Add("@Is_QM_in_Procurement", ObjQuality.Is_QM_in_Procurement);
            hashPara.Add("@Certificate_Type", ObjQuality.Certificate_Type);
            hashPara.Add("@Ctrl_Key_QM_Procurement", ObjQuality.Ctrl_Key_QM_Procurement);
            hashPara.Add("@Is_Doc_Required", ObjQuality.Is_Doc_Required);
            hashPara.Add("@Catlog_Profile", ObjQuality.Catlog_Profile);
            hashPara.Add("@Mat_Auth_Grp_Activities", ObjQuality.Mat_Auth_Grp_Activities);
            hashPara.Add("@Interval_Nxt_Inspection", ObjQuality.Interval_Nxt_Inspection);
            hashPara.Add("@Inspection_Type", ObjQuality.Inspection_Type);

            hashPara.Add("@Min_Remaining_Shelf_Life", ObjQuality.Min_Remaining_Shelf_Life);
            hashPara.Add("@Total_Shelf_Life_Days", ObjQuality.Total_Shelf_Life_Days);
            
            hashPara.Add("@IsActive", ObjQuality.IsActive);
            hashPara.Add("@UserId", ObjQuality.UserId);
            hashPara.Add("@UserIp", ObjQuality.IPAddress);
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

        public Quality GetQuality(int Mat_Quality_Id)
        {
            Quality ObjQuality = new Quality();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Quality_By_MatQualityId";
            DataSet ds;

            hashPara.Add("@Mat_Quality_Id", Mat_Quality_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjQuality.Mat_Quality_Id = Convert.ToInt32( dt.Rows[0]["Mat_Quality_Id"].ToString());
                        ObjQuality.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjQuality.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjQuality.Unit_Issue = dt.Rows[0]["Unit_Issue"].ToString();
                        ObjQuality.Is_QM_in_Procurement = dt.Rows[0]["Is_QM_in_Procurement"].ToString();
                        ObjQuality.Certificate_Type = dt.Rows[0]["Certificate_Type"].ToString();
                        ObjQuality.Ctrl_Key_QM_Procurement = dt.Rows[0]["Ctrl_Key_QM_Procurement"].ToString();
                        ObjQuality.Is_Doc_Required = dt.Rows[0]["Is_Doc_Required"].ToString();
                        ObjQuality.Catlog_Profile = dt.Rows[0]["Catlog_Profile"].ToString();
                        ObjQuality.Mat_Auth_Grp_Activities = dt.Rows[0]["Mat_Auth_Grp_Activities"].ToString();
                        ObjQuality.Interval_Nxt_Inspection = dt.Rows[0]["Interval_Nxt_Inspection"].ToString();
                        ObjQuality.Inspection_Type = dt.Rows[0]["Inspection_Type"].ToString();

                        ObjQuality.Min_Remaining_Shelf_Life = dt.Rows[0]["Min_Remaining_Shelf_Life"].ToString();
                        ObjQuality.Total_Shelf_Life_Days = dt.Rows[0]["Total_Shelf_Life_Days"].ToString();
                        
                    }
                }
                return ObjQuality;
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
        /// LLM_DPT_SDT30072019
        /// </summary>
        /// <returns></returns>
        public DataSet GetLLMDPTPlantList()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_llmdptPlantValidation";
            DataSet ds;


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
        /// LLM_DPT_SDT30072019
        /// </summary>
        /// <returns></returns>
        public DataSet GetDPTPlantList()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_DPTPlantValidation";
            DataSet ds;


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
        /// LLM_DPT_SDT30072019
        /// </summary>
        /// <returns></returns>
        public DataSet GetLLMPlantList()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_LLMPlantValidation";
            DataSet ds;


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

        public DataSet GetQualityData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Quality_By_MasterHeaderId";
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

        //New Addition for HU tick Start
        //public int SaveQualityInspData(InspData objInspData)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_Mat_Quality_Inspection";
        //    int res = 0;

        //    hashPara.Add("@Mat_InspData_Id", objInspData.Mat_InspData_Id);
        //    hashPara.Add("@Master_Header_Id", objInspData.Master_Header_Id);
        //    hashPara.Add("@InspectionType", objInspData.InspectionType);
        //    hashPara.Add("@PostInspStock", objInspData.PostInspStock);
        //    hashPara.Add("@InspHU", objInspData.InspHU);
            
        //    hashPara.Add("@IsActive", objInspData.IsActive);
        //    hashPara.Add("@UserId", objInspData.UserId);
        //    hashPara.Add("@UserIp", objInspData.IPAddress);

        //    try
        //    {
        //        objDal.OpenConnection();
        //        res = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
        //        return res;
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

        //public InspData GetInspDataByMasterHeaderId(string master_header_id, string inspType)
        //{
        //    InspData objInspData = new InspData();

        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Get_Mat_Quality_Inspection_By_Master_Header_Id";
        //    DataSet ds;

        //    hashPara.Add("@Master_Header_Id", master_header_id);
        //    hashPara.Add("@InspectionType", inspType);

        //    try
        //    {
        //        ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        //        if (ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                objInspData.Mat_InspData_Id = Convert.ToInt32(dt.Rows[0]["Mat_InspData_Id"].ToString());
        //                objInspData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
        //                objInspData.InspectionType = dt.Rows[0]["InspectionType"].ToString();
        //                objInspData.PostInspStock = dt.Rows[0]["PostInspStock"].ToString();
        //                objInspData.InspHU = dt.Rows[0]["InspHU"].ToString();                     

        //            }
        //        }
        //        return objInspData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objDal = null;
        //    }
        //}
        //New Addition for HU tick End

      
    }
}