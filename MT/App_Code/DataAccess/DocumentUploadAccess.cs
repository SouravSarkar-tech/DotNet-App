using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for DocumentUploadAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class DocumentUploadAccess
    {
        public DocumentUploadAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(DocumentUpload ObjDoc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Document_Upload";
            int result = 0;

            hashPara.Add("@Document_Upload_Id", ObjDoc.Document_Upload_Id);
            hashPara.Add("@Master_Header_Id", ObjDoc.Master_Header_Id);
            hashPara.Add("@Request_No", ObjDoc.Request_No);
            hashPara.Add("@Document_Type", ObjDoc.Document_Type);
            hashPara.Add("@Document_Name", ObjDoc.Document_Name);
            hashPara.Add("@Document_Path", ObjDoc.Document_Path);
            hashPara.Add("@Remarks", ObjDoc.Remarks);
            hashPara.Add("@IsActive", ObjDoc.IsActive);
            hashPara.Add("@UserId", ObjDoc.UserId);
            hashPara.Add("@UserIp", ObjDoc.IPAddress);

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
        /// MSC_8300001775_DT160820
        /// </summary>
        /// <param name="MasterHeaderId"></param>
        /// <returns></returns>
        public DataSet GetDocumentUploadData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Document_Upload_By_MasterHeaderId";
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

        public DataSet GetDocumentUploadDataBOM(int MasterHeaderId,string pDocument_Type)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Document_Upload_By_MasterHeaderId_bom";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);
            hashPara.Add("@pDocument_Type", pDocument_Type);
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

        public DataSet GetDocumentDownloadListByDate(string ModuleType, string App_Date)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDocumentDownloadListByDate";
            DataSet ds;

            hashPara.Add("@ModuleType", ModuleType);
            hashPara.Add("@App_Date", App_Date);

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

        public static int DeleteDocument(string Document_Upload_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Document_Upload";
            int result = 0;

            hashPara.Add("@Document_Upload_Id", Document_Upload_Id);
            //hashPara.Add("@Master_Header_Id", ObjDoc.Master_Header_Id);
            //hashPara.Add("@Request_No", ObjDoc.Request_No);
            //hashPara.Add("@Document_Type", ObjDoc.Document_Type);
            //hashPara.Add("@Document_Name", ObjDoc.Document_Name);
            //hashPara.Add("@Document_Path", ObjDoc.Document_Path);
            //hashPara.Add("@Remarks", ObjDoc.Remarks);
            //hashPara.Add("@IsActive", ObjDoc.IsActive);
            //hashPara.Add("@UserId", ObjDoc.UserId);
            //hashPara.Add("@UserIp", ObjDoc.IPAddress);

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

        public DataSet GetMandatoryDocList(int moduleId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mandatory_DocList_By_ModuleId";
            DataSet ds;

            hashPara.Add("@Module_Id", moduleId);

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