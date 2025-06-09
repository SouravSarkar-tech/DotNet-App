using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Accenture.MWT.DomainObject;


/// <summary>
/// Summary description for EAuditAccess
/// </summary>
/// 
namespace Accenture.MWT.DataAccess
{
    public class EAuditAccess
    {
        Utility ObjUtil = new Utility();

        public string mRequestNo { get; set; }

        /// <summary>
        /// DT28072020
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public DataSet ReadAditDeptHead(string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_ReadAditDeptHead";
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public object ReadModulesByModuleType(string moduleType)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_ReadModulesByModuleType";
            hashPara.Add("@ModuleType", moduleType);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }
        //, string Master_Category = "", string Reference_Id = ""
        public int SaveAuditHeader(string masterHeaderId, string moduleId, string userId, string flg, string depthead)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Save_Master_Header_Audit";
            int retVal = 0;
            int masterHeaderId2 = 0;

            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            //objCommand.Parameters.AddWithValue("@Company_Id", companyId);
            //objCommand.Parameters.AddWithValue("@Master_Category", Master_Category);
            objCommand.Parameters.AddWithValue("@Plant_Id", depthead);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter outRequestNo = objCommand.Parameters.Add("@RequestNo", SqlDbType.VarChar, 50);
            outRequestNo.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                retVal = (int)ret.Value;
                if (retVal > 0)
                {
                    masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
                    mRequestNo = SafeTypeHandling.ConvertToString(outRequestNo.Value);
                }

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
            return masterHeaderId2;
        }

        public DataSet SearchMasterRequests(string status, string requestNo, string userId, string moduleId, string moduleType, string sapcodeNo, string StartDate, string EndDate)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Status", status);
            hashPara.Add("@RequestNo", requestNo + "%");
            hashPara.Add("@SAPCode", sapcodeNo + "%");
            hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@ModuleType", moduleType);
            hashPara.Add("@Start_Date", StartDate);
            hashPara.Add("@End_Date", EndDate);

            string procName = "pr_Search_Master_Request_Audit";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataSet SearchauditRequests(string status, string moduleId, string moduleType, string sapcodeNo, string StartDate, string EndDate)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@Status", status);
            //hashPara.Add("@RequestNo", requestNo + "%");
            hashPara.Add("@SAPCode", sapcodeNo + "%");
            //hashPara.Add("@UserId", userId);
            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@ModuleType", moduleType);
            hashPara.Add("@Start_Date", StartDate);
            hashPara.Add("@End_Date", EndDate);

            string procName = "pr_Manufacture_Request_Report";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public bool IsUserInitiator(string moduleId, string deptId, string userId, string MasterHeaderId = "")
        {
            bool flg = true;
            if (moduleId == "")
            {
                moduleId = "0";
            }
            if (MasterHeaderId != "")
            {
                DataAccessLayer objDal1 = new DataAccessLayer();
                string query = "SELECT * FROM T_Master_Header WHERE Master_Header_Id = " + MasterHeaderId + " AND CreatedBy = " + userId + " AND IsActive = 'TRUE'";
                try
                {
                    objDal1.OpenConnection();
                    flg = !objDal1.ReturnBool(query, ref objDal1.cnnConnection);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objDal1.CloseConnection(objDal1.cnnConnection);
                    objDal1 = null;
                }
            }

            if (flg)
            {
                DataAccessLayer objDal = new DataAccessLayer();
                string query1 = "SELECT * FROM M_Approving_Authority WHERE Module_Id = " + moduleId + " AND Department_Id = " + deptId + " AND User_Id = " + userId + " AND IsActive = 'TRUE'";
                try
                {
                    objDal.OpenConnection();
                    flg = objDal.ReturnBool(query1, ref objDal.cnnConnection);
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
            return flg;
        }

        public DataSet ReadDeparmentListForRollback(string masterHeaderId, string departmentId, string moduleId = null)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@DepartmentId", departmentId);

            string procName = "pr_GetRollBackAllowedDepartment";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        //public int SaveAuditData(AuditData ObjAuditData)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();
        //    string procName = "pr_Ins_Upd_T_Audit_Request_Data";
        //    int result = 0;

        //    hashPara.Add("@EAudit_Form_Id", ObjAuditData.EAudit_Form_Id);
        //    hashPara.Add("@Master_Header_Id", ObjAuditData.Master_Header_Id);
        //    //hashPara.Add("@Location", ObjAuditData.Location);
        //    hashPara.Add("@RequestDate", ObjAuditData.RequestDate);
        //    hashPara.Add("@Priority", ObjAuditData.Priority);            
        //    hashPara.Add("@Market", ObjAuditData.Market);
        //    hashPara.Add("@Spec_Market", ObjAuditData.Spec_Market);
        //    hashPara.Add("@Customer_Code", ObjAuditData.Customer_Code);
        //    hashPara.Add("@Name1", ObjAuditData.Name1);
        //    hashPara.Add("@Name2", ObjAuditData.Name2);
        //    hashPara.Add("@Name3", ObjAuditData.Name3);
        //    hashPara.Add("@Name4", ObjAuditData.Name4);
        //    hashPara.Add("@HouseNo_Street", ObjAuditData.HouseNo_Street);
        //    hashPara.Add("@Street4", ObjAuditData.Street4);
        //    hashPara.Add("@Street5", ObjAuditData.Street5);
        //    hashPara.Add("@PO_Box", ObjAuditData.PO_Box);
        //    hashPara.Add("@City", ObjAuditData.City);
        //    hashPara.Add("@Postal_Code", ObjAuditData.Postal_Code);
        //    hashPara.Add("@District", ObjAuditData.District);
        //    hashPara.Add("@PO_Box_Postal_Code", ObjAuditData.PO_Box_Postal_Code);
        //    hashPara.Add("@CountryKey", ObjAuditData.CountryKey);
        //    hashPara.Add("@Region", ObjAuditData.Region);
        //    hashPara.Add("@Contact_Name", ObjAuditData.Contact_Name);
        //    hashPara.Add("@Mobile_Num", ObjAuditData.Mobile_Num);
        //    hashPara.Add("@First_Tele_No", ObjAuditData.First_Tele_No);
        //    hashPara.Add("@Email_Address", ObjAuditData.Email_Address);
        //    hashPara.Add("@Audit_Reason", ObjAuditData.Audit_Reason);
        //    hashPara.Add("@Spec_Audit", ObjAuditData.Spec_Audit);
        //    hashPara.Add("@Prev_App_Status", ObjAuditData.Prev_App_Status);
        //    hashPara.Add("@Remarks", ObjAuditData.Remarks);

        //    //hashPara.Add("@RDTrialsOver", ObjAuditData.RDTrialsOver);
        //    //hashPara.Add("@RDFeasibilityTrial", ObjAuditData.RDFeasibilityTrial);
        //    //hashPara.Add("@RDSpecsFreezed", ObjAuditData.RDSpecsFreezed);
        //    //hashPara.Add("@RDSpecReq", ObjAuditData.RDSpecReq);
        //    //hashPara.Add("@RDMaterialCategory", ObjAuditData.RDMaterialCategory);
        //    hashPara.Add("@RDComments", ObjAuditData.RDComments);
        //    hashPara.Add("@RADMF", ObjAuditData.RADMF);
        //    hashPara.Add("@RAMatType", ObjAuditData.RAMatType);
        //    hashPara.Add("@RACatMod", ObjAuditData.RACatMod);
        //    hashPara.Add("@RAMatRedefined", ObjAuditData.RAMatRedefined);
        //    hashPara.Add("@RAAuditNeeded", ObjAuditData.RAAuditNeeded);
        //    hashPara.Add("@RAJoinAudit", ObjAuditData.RAJoinAudit);
        //    //hashPara.Add("@QASignOff", ObjAuditData.QASignOff);
        //    //hashPara.Add("@Justification", ObjAuditData.Justification);

        //    hashPara.Add("@UserId", ObjAuditData.UserId);
        //    hashPara.Add("@UserIp", ObjAuditData.IPAddress);

        //    try
        //    {
        //        objDal.OpenConnection();
        //        result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
        //        return result;
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

        public int SaveAuditData(AuditData ObjAuditData)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_Ins_Upd_T_Audit_Request_Data";
            int retVal = 0;
            int auditFormId = 0;

            objCommand.Parameters.AddWithValue("@EAudit_Form_Id", ObjAuditData.EAudit_Form_Id);
            objCommand.Parameters.AddWithValue("@Master_Header_Id", ObjAuditData.Master_Header_Id);
            //objCommand.Parameters.AddWithValue("@Location", ObjAuditData.Location);
            objCommand.Parameters.AddWithValue("@RequestDate", ObjAuditData.RequestDate);
            objCommand.Parameters.AddWithValue("@Priority", ObjAuditData.Priority);
            objCommand.Parameters.AddWithValue("@Market", ObjAuditData.Market);
            objCommand.Parameters.AddWithValue("@Spec_Market", ObjAuditData.Spec_Market);
            objCommand.Parameters.AddWithValue("@Customer_Code", ObjAuditData.Customer_Code);
            objCommand.Parameters.AddWithValue("@Name1", ObjAuditData.Name1);
            objCommand.Parameters.AddWithValue("@Name2", ObjAuditData.Name2);
            objCommand.Parameters.AddWithValue("@Name3", ObjAuditData.Name3);
            objCommand.Parameters.AddWithValue("@Name4", ObjAuditData.Name4);
            objCommand.Parameters.AddWithValue("@HouseNo_Street", ObjAuditData.HouseNo_Street);
            objCommand.Parameters.AddWithValue("@Street4", ObjAuditData.Street4);
            objCommand.Parameters.AddWithValue("@Street5", ObjAuditData.Street5);
            objCommand.Parameters.AddWithValue("@PO_Box", ObjAuditData.PO_Box);
            objCommand.Parameters.AddWithValue("@City", ObjAuditData.City);
            objCommand.Parameters.AddWithValue("@Postal_Code", ObjAuditData.Postal_Code);
            objCommand.Parameters.AddWithValue("@District", ObjAuditData.District);
            objCommand.Parameters.AddWithValue("@PO_Box_Postal_Code", ObjAuditData.PO_Box_Postal_Code);
            objCommand.Parameters.AddWithValue("@CountryKey", ObjAuditData.CountryKey);
            objCommand.Parameters.AddWithValue("@Region", ObjAuditData.Region);
            objCommand.Parameters.AddWithValue("@Contact_Name", ObjAuditData.Contact_Name);
            objCommand.Parameters.AddWithValue("@Mobile_Num", ObjAuditData.Mobile_Num);
            objCommand.Parameters.AddWithValue("@MobileExt", ObjAuditData.MobileExt);
            objCommand.Parameters.AddWithValue("@First_Tele_No", ObjAuditData.First_Tele_No);
            objCommand.Parameters.AddWithValue("@Email_Address", ObjAuditData.Email_Address);
            objCommand.Parameters.AddWithValue("@Audit_Reason", ObjAuditData.Audit_Reason);
            objCommand.Parameters.AddWithValue("@Spec_Audit", ObjAuditData.Spec_Audit);
            objCommand.Parameters.AddWithValue("@Prev_App_Status", ObjAuditData.Prev_App_Status);
            objCommand.Parameters.AddWithValue("@Remarks", ObjAuditData.Remarks);

            //objCommand.Parameters.AddWithValue("@RDTrialsOver", ObjAuditData.RDTrialsOver);
            //objCommand.Parameters.AddWithValue("@RDFeasibilityTrial", ObjAuditData.RDFeasibilityTrial);
            //objCommand.Parameters.AddWithValue("@RDSpecsFreezed", ObjAuditData.RDSpecsFreezed);
            //objCommand.Parameters.AddWithValue("@RDSpecReq", ObjAuditData.RDSpecReq);
            //objCommand.Parameters.AddWithValue("@RDMaterialCategory", ObjAuditData.RDMaterialCategory);
            objCommand.Parameters.AddWithValue("@RDComments", ObjAuditData.RDComments);
            objCommand.Parameters.AddWithValue("@RADMF", ObjAuditData.RADMF);
            //objCommand.Parameters.AddWithValue("@RAMatType", ObjAuditData.RAMatType);
            //objCommand.Parameters.AddWithValue("@RACatMod", ObjAuditData.RACatMod);
            objCommand.Parameters.AddWithValue("@RAMatRedefined", ObjAuditData.RAMatRedefined);
            objCommand.Parameters.AddWithValue("@RAAuditNeeded", ObjAuditData.RAAuditNeeded);
            objCommand.Parameters.AddWithValue("@RAJoinAudit", ObjAuditData.RAJoinAudit);
            //objCommand.Parameters.AddWithValue("@QASignOff", ObjAuditData.QASignOff);
            //objCommand.Parameters.AddWithValue("@Justification", ObjAuditData.Justification);
            // Start Adding By nitish Rao   10/08/2018
            objCommand.Parameters.AddWithValue("@RAComments", ObjAuditData.RAComments);
            objCommand.Parameters.AddWithValue("@RNDDMF", ObjAuditData.RNDDMF);
            //objCommand.Parameters.AddWithValue("@RND_MatType", ObjAuditData.RAMatType);
            //objCommand.Parameters.AddWithValue("@RND_CatMod", ObjAuditData.RACatMod);
            objCommand.Parameters.AddWithValue("@RNDMatRedefined", ObjAuditData.RNDMatRedefined);
            objCommand.Parameters.AddWithValue("@RNDAuditNeeded", ObjAuditData.RNDAuditNeeded);
            objCommand.Parameters.AddWithValue("@RNDJoinAudit", ObjAuditData.RNDJoinAudit);
            // End adding By nitish Rao  10/08/2018
            objCommand.Parameters.AddWithValue("@UserId", ObjAuditData.UserId);
            objCommand.Parameters.AddWithValue("@UserIp", ObjAuditData.IPAddress);
            

            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outAuditFormId = objCommand.Parameters.Add("@OutAuditFormId", SqlDbType.Int);
            outAuditFormId.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                retVal = (int)ret.Value;
                if (retVal > 0)
                {
                    auditFormId = SafeTypeHandling.ConvertStringToInt32(outAuditFormId.Value);
                }

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
            return auditFormId;
        }

        public AuditData GetAuditBasicData(string masterHeaderId)
        {
            AuditData ObjAuditData = new AuditData();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Audit_Basic_Data_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", masterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjAuditData.EAudit_Form_Id = Convert.ToInt32(dt.Rows[0]["EAudit_Form_Id"].ToString());
                        ObjAuditData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        //ObjAuditData.Location = dt.Rows[0]["Location"].ToString();
                        ObjAuditData.RequestDate = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["RequestDate"].ToString());
                        ObjAuditData.Priority = dt.Rows[0]["Priority"].ToString();
                        ObjAuditData.Department = dt.Rows[0]["Department"].ToString();
                        ObjAuditData.Spec_Dept = dt.Rows[0]["Spec_Dept"].ToString();
                        ObjAuditData.Market = dt.Rows[0]["Market"].ToString();
                        ObjAuditData.Spec_Market = dt.Rows[0]["Spec_Market"].ToString();
                        ObjAuditData.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                        ObjAuditData.Name1 = dt.Rows[0]["Name1"].ToString();
                        ObjAuditData.Name2 = dt.Rows[0]["Name2"].ToString();
                        ObjAuditData.Name3 = dt.Rows[0]["Name3"].ToString();
                        ObjAuditData.Name4 = dt.Rows[0]["Name4"].ToString();
                        ObjAuditData.HouseNo_Street = dt.Rows[0]["HouseNo_Street"].ToString();
                        ObjAuditData.Street4 = dt.Rows[0]["Street4"].ToString();
                        ObjAuditData.Street5 = dt.Rows[0]["Street5"].ToString();
                        ObjAuditData.PO_Box = dt.Rows[0]["PO_Box"].ToString();
                        ObjAuditData.City = dt.Rows[0]["City"].ToString();
                        ObjAuditData.Postal_Code = dt.Rows[0]["Postal_Code"].ToString();
                        ObjAuditData.District = dt.Rows[0]["District"].ToString();
                        ObjAuditData.PO_Box_Postal_Code = dt.Rows[0]["PO_Box_Postal_Code"].ToString();
                        ObjAuditData.CountryKey = dt.Rows[0]["CountryKey"].ToString();
                        ObjAuditData.Region = dt.Rows[0]["Region"].ToString();
                        //ObjAuditData.AnalysisPerformed = dt.Rows[0]["AnalysisPerformed"].ToString();
                        //ObjAuditData.AnalysisMethod = dt.Rows[0]["AnalysisMethod"].ToString();
                        ObjAuditData.Contact_Name = dt.Rows[0]["Contact_Name"].ToString();
                        ObjAuditData.Mobile_Num = dt.Rows[0]["Mobile_Num"].ToString();
                        ObjAuditData.MobileExt = dt.Rows[0]["MobileExt"].ToString();
                        ObjAuditData.First_Tele_No = dt.Rows[0]["First_Tele_No"].ToString();
                        ObjAuditData.Email_Address = dt.Rows[0]["Email_Address"].ToString();
                        ObjAuditData.Audit_Reason = dt.Rows[0]["Audit_Reason"].ToString();
                        ObjAuditData.Spec_Audit = dt.Rows[0]["Spec_Audit"].ToString();
                        ObjAuditData.Prev_App_Status = dt.Rows[0]["Prev_App_Status"].ToString();
                        ObjAuditData.Remarks = dt.Rows[0]["Remarks"].ToString();

                        //ObjAuditData.RDTrialsOver = dt.Rows[0]["RDTrialsOver"].ToString();
                        //ObjAuditData.RDFeasibilityTrial = dt.Rows[0]["RDFeasibilityTrial"].ToString();
                        //ObjAuditData.RDSpecsFreezed = dt.Rows[0]["RDSpecsFreezed"].ToString();
                        //ObjAuditData.RDSpecReq = dt.Rows[0]["RDSpecReq"].ToString();
                        //ObjAuditData.RDMaterialCategory = dt.Rows[0]["RDMaterialCategory"].ToString();
                        ObjAuditData.RDComments = dt.Rows[0]["RDComments"].ToString();
                        ObjAuditData.RADMF = dt.Rows[0]["RADMF"].ToString();
                        //ObjAuditData.RAMatType = dt.Rows[0]["RAMatType"].ToString();
                        //ObjAuditData.RACatMod = dt.Rows[0]["RACatMod"].ToString();
                        ObjAuditData.RAMatRedefined = dt.Rows[0]["RAMatRedefined"].ToString();
                        ObjAuditData.RAAuditNeeded = dt.Rows[0]["RAAuditNeeded"].ToString();
                        ObjAuditData.RAJoinAudit = dt.Rows[0]["RAJoinAudit"].ToString();
                        //ObjAuditData.QASignOff = dt.Rows[0]["QASignOff"].ToString();
                        //ObjAuditData.Justification = dt.Rows[0]["Justification"].ToString();
                        // Start Adding Nitish Rao 10/08/2018
                        ObjAuditData.RAComments = dt.Rows[0]["RA_Comments"].ToString();
                        ObjAuditData.RNDDMF = dt.Rows[0]["RND_DMF"].ToString();
                        ObjAuditData.RNDMatType = dt.Rows[0]["RND_MatType"].ToString();
                        ObjAuditData.RNDCatMod = dt.Rows[0]["RND_CatMod"].ToString();
                        ObjAuditData.RNDMatRedefined = dt.Rows[0]["RND_MatRedefined"].ToString();
                        ObjAuditData.RNDAuditNeeded = dt.Rows[0]["RND_AuditNeeded"].ToString();
                        ObjAuditData.RNDJoinAudit = dt.Rows[0]["RND_JoinAudit"].ToString();
                        //End Adding Nitish Rao
                    }
                }
                return ObjAuditData;
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

        public DataSet GetMaterialsData(int eauditId)
        {
            //DataAccessLayer objDal = new DataAccessLayer();
            //return objDal.FillDataSet("Select * from T_Audit_Material_Details", "Materials");
            //AuditMaterialsData ObjAuditMaterialsData = new AuditMaterialsData();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Audit_Materials_Data_By_AuditFormId";
            DataSet ds;

            hashPara.Add("@EAudit_Form_Id", eauditId);

            try
            {
                return ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
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

        public DataSet GetProductsData(int eauditId)
        {
            //DataAccessLayer objDal = new DataAccessLayer();
            //return objDal.FillDataSet("Select * from T_Audit_Products_Details", "Products");

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Audit_Products_Data_By_AuditFormId";
            DataSet ds;

            hashPara.Add("@EAudit_Form_Id", eauditId);

            try
            {
                return ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
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

        public int InsertMaterialsData(AuditMaterialsData objMatData)
            {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Audit_Material_Data";
            int result = 0;

            hashPara.Add("@E_Audit_Material_Id", objMatData.E_Audit_Material_Id);
            hashPara.Add("@EAudit_Form_Id", objMatData.EAudit_Form_Id);
            hashPara.Add("@SerialNo", objMatData.SerialNo);
            hashPara.Add("@Material_Name", objMatData.Material_Name);
            hashPara.Add("@Product_Name", objMatData.Product_Name);
            hashPara.Add("@LupinLoc", objMatData.LupinLoc);
            hashPara.Add("@Pharmacopical_Status", objMatData.Pharmacopical_Status);
            hashPara.Add("@AnalysisMethod", objMatData.AnalysisMethod);
            hashPara.Add("@MaterialCategory", objMatData.MaterialCategory);
            //Start Adding By 10/08/2018 Nitish Rao
            hashPara.Add("@Other_LupinLoc", objMatData.OtherLC);
            hashPara.Add("@Other_Pharmacopical_Status", objMatData.OtherPharmaStatus);
            hashPara.Add("@Other_AnalysisMethod", objMatData.OtherMthdAnalysis);
            hashPara.Add("@Other_MaterialCategory", objMatData.OtherMatCategory);
            //End Adding By 10/08/2018 Nitish Rao
            hashPara.Add("@UserId", objMatData.UserId);
            hashPara.Add("@UserIp", objMatData.IPAddress);

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

        public bool DeleteMaterialData(int id, int matId, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.ExecuteQuery("Delete from T_Audit_Material_Details where E_Audit_Material_Id = " + matId + " and SerialNo = " + id + " and IsActive = 1", ref cnn, ref objTrans);
        }

        public int InsertProductsData(AuditProductsData objProdData)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Audit_Product_Data";
            int result = 0;

            hashPara.Add("@E_Audit_Product_Id", objProdData.E_Audit_Product_Id);
            hashPara.Add("@EAudit_Form_Id", objProdData.EAudit_Form_Id);
            hashPara.Add("@Serial_No", objProdData.Serial_No);
            hashPara.Add("@Product_Name", objProdData.Product_Name);

            hashPara.Add("@UserId", objProdData.UserId);
            hashPara.Add("@UserIp", objProdData.IPAddress);

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

        public bool DeleteProductData(int id,int prodId, ref SqlConnection cnn, ref SqlTransaction objTrans)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.ExecuteQuery("Delete from T_Audit_Products_Details where E_Audit_Product_Id = " + prodId + " and Serial_No = " + id + " and IsActive = 1", ref cnn, ref objTrans);
        }

        public DataSet GetPharmaData()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("SELECT LookUp_Desc AS LookUp_Desc,LookUp_Code FROM M_LookUp_Audit WHERE Control_Name = 'ddlPharmo' AND IsActive = 1 and isnull(Is_Hidden,0) <> 1 ", "PharmoStatus");
        }

        public DataSet GetAnalysisData()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("SELECT LookUp_Desc AS LookUp_Desc,LookUp_Code FROM M_LookUp_Audit WHERE Control_Name = 'ddlMthdAnalysis' AND IsActive = 1 and isnull(Is_Hidden,0) <> 1 ", "AnalysisMethod");
        }

        public int GenerateMassRequestProcess(string masterHeaderId, string approvedByDeptId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GenerateMassRequestProcess_Audit";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());

            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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

        public int GenerateCopyRequestAudit(string masterHeaderId, string moduleId, string userId, string flg, string depthead)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_GenerateCopyRequest_Audit";
            int retVal = 0;
            int masterHeaderId2 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@ModuleId", moduleId);
            objCommand.Parameters.AddWithValue("@Plant_Id", depthead);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@IpAddress", objUtil.GetIpAddress());
            objCommand.Parameters.AddWithValue("@Flg", flg);
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter outMasterHeaderId = objCommand.Parameters.Add("@OutMasterHeaderId", SqlDbType.Int);
            outMasterHeaderId.Direction = ParameterDirection.Output;

            SqlParameter outRequestNo = objCommand.Parameters.Add("@RequestNo", SqlDbType.VarChar, 50);
            outRequestNo.Direction = ParameterDirection.Output;

            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                retVal = (int)ret.Value;
                if (retVal > 0)
                {
                    masterHeaderId2 = SafeTypeHandling.ConvertStringToInt32(outMasterHeaderId.Value);
                    mRequestNo = SafeTypeHandling.ConvertToString(outRequestNo.Value);
                }
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
            return masterHeaderId2;
        }

        public int ApproveRequest(string masterHeaderId, string approvedByDeptId, string redirectedTo, string userId, string approvalNote = "")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Approve_Request_Audit";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@RedirectedTo", redirectedTo);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            hashPara.Add("@ApprovalComment", approvalNote);
            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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

        public int RollbackRequest(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Reject_Request_Audit";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@RollbackToWorkFlowId", rollbackToWorkflowSeq);
            hashPara.Add("@Remarks", remarks);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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
        
        public DataSet GetMatCategoryData()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("SELECT LookUp_Desc AS LookUp_Desc,LookUp_Code FROM M_LookUp_Audit WHERE Control_Name = 'ddlMatCategory' AND IsActive = 1 and isnull(Is_Hidden,0) <> 1 ", "MaterialCategory");
        }

        public int FinalRejectRequest(string masterHeaderId, string approvedByDeptId, string rollbackToWorkflowSeq, string remarks, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Final_Reject_Request_Audit";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@ApprovedByDept", approvedByDeptId);
            hashPara.Add("@RollbackToWorkFlowId", rollbackToWorkflowSeq);
            hashPara.Add("@Remarks", remarks);
            hashPara.Add("@UserId", userId);
            hashPara.Add("@IpAddress", objUtil.GetIpAddress());
            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
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

        public DataSet GetLupinLoc()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            return objDal.FillDataSet("SELECT LookUp_Desc AS LookUp_Desc,LookUp_Code FROM M_LookUp_Audit WHERE Control_Name = 'ddlLocation' AND IsActive = 1 and isnull(Is_Hidden,0) <> 1 ", "LupinLocation");
        }
        
        public DataSet ReadNextDeparmentIDForApproval(string masterHeaderId, string departmentId, string moduleId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            hashPara.Add("@DepartmentId", departmentId);
            //start added by Nitish Rao 14/08/2018 for dropdown issue
            hashPara.Add("@ModuleId", moduleId);
            //End added by Nitish Rao 14/08/2018 for dropdown issue
            string procName = "pr_GetNextApprovalDepartmentName_Audit";
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
            //return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure,procName,hashPara,

        }


        public VendorGeneral1 GetVendorGeneral_FromSAP(string vendorCode)
        {
            VendorGeneral1 ObjVendorGeneral = new VendorGeneral1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Vendor_General1_SAP_By_VendorCode";
            DataSet ds;

            hashPara.Add("@VendorCode", vendorCode);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjVendorGeneral.Vendor_General1_Id = Convert.ToInt32(dt.Rows[0]["Vendor_General1_Id"].ToString());
                        ObjVendorGeneral.Customer_Code = dt.Rows[0]["VendorCode"].ToString();
                        ObjVendorGeneral.Vendor_Group = dt.Rows[0]["Vendor_Group"].ToString();
                        ObjVendorGeneral.Name1 = dt.Rows[0]["Name1"].ToString();
                        ObjVendorGeneral.Name2 = dt.Rows[0]["Name2"].ToString();
                        ObjVendorGeneral.Name3 = dt.Rows[0]["Name3"].ToString();
                        ObjVendorGeneral.Name4 = dt.Rows[0]["Name4"].ToString();
                        ObjVendorGeneral.Sort_Field = dt.Rows[0]["Sort_Field"].ToString();
                        ObjVendorGeneral.HouseNo_Street = dt.Rows[0]["HouseNo_Street"].ToString();
                        ObjVendorGeneral.Street4 = dt.Rows[0]["Street4"].ToString();
                        ObjVendorGeneral.Street5 = dt.Rows[0]["Street5"].ToString();
                        ObjVendorGeneral.City = dt.Rows[0]["City"].ToString();
                        ObjVendorGeneral.District = dt.Rows[0]["District"].ToString();
                        ObjVendorGeneral.PO_Box = dt.Rows[0]["PO_Box"].ToString();
                        ObjVendorGeneral.Postal_Code = dt.Rows[0]["Postal_Code"].ToString();
                        ObjVendorGeneral.PO_Box_Postal_Code = dt.Rows[0]["PO_Box_Postal_Code"].ToString();
                        ObjVendorGeneral.Region = dt.Rows[0]["Region"].ToString();
                        ObjVendorGeneral.CountryKey = dt.Rows[0]["CountryKey"].ToString();
                        ObjVendorGeneral.LanguageAcc = dt.Rows[0]["LanguageAcc"].ToString();

                    }
                }
                return ObjVendorGeneral;
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

        public int GetMaterialDetail(string matId, string auditId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_MaterialDetail_Data_Exists";
            int result = 0;

            hashPara.Add("@E_Audit_Material_Id", matId);
            hashPara.Add("@EAudit_Form_Id", auditId);

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
