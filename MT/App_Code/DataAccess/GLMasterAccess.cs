using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for GLMasterAccess
/// </summary>
///     

namespace Accenture.MWT.DataAccess
{
    public class GLMasterAccess
    {
        public GLMasterAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public GLCreate1 GetGLMasterData(int MasterHeaderId)
        {
            GLCreate1 ObjGLMaster = new GLCreate1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_GLMaster_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjGLMaster.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                        ObjGLMaster.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjGLMaster.GL_Code = dt.Rows[0]["GL_Code"].ToString();
                        ObjGLMaster.Ref_GL_Code = dt.Rows[0]["Ref_GL_Code"].ToString();
                        ObjGLMaster.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjGLMaster.Account_Group = dt.Rows[0]["Account_Group"].ToString();
                        ObjGLMaster.PnL_BalanceSheet = dt.Rows[0]["PnL_BalanceSheet"].ToString();
                        ObjGLMaster.Short_Text = dt.Rows[0]["Short_Text"].ToString();
                        ObjGLMaster.GL_Acct_Long_Text = dt.Rows[0]["GL_Acct_Long_Text"].ToString();
                        ObjGLMaster.Language1 = dt.Rows[0]["Language1"].ToString();
                        ObjGLMaster.Language2 = dt.Rows[0]["Language2"].ToString();
                        ObjGLMaster.Rec_Account = dt.Rows[0]["Rec_Account"].ToString();
                        ObjGLMaster.Open_Item_Management = dt.Rows[0]["Open_Item_Management"].ToString();
                        ObjGLMaster.Line_Item_Display = dt.Rows[0]["Line_Item_Display"].ToString();
                        ObjGLMaster.Reason_For_Creation = dt.Rows[0]["Reason_For_Creation"].ToString();
                        ObjGLMaster.Remarks = dt.Rows[0]["Remarks"].ToString();
                        ObjGLMaster.Ref_Company_Code = dt.Rows[0]["Ref_Company_Code"].ToString();
                        ObjGLMaster.CostElementCategory = dt.Rows[0]["CostElementCategory"].ToString();

                        //S4HanaGLDT07122021
                        ObjGLMaster.GLAccType = dt.Rows[0]["GLAccType"].ToString();
                        ObjGLMaster.GLAccSubType = dt.Rows[0]["GLAccSubType"].ToString();
                        ObjGLMaster.ClearSpectoLedgerGPS = dt.Rows[0]["ClearSpectoLedgerGPS"].ToString();
                        //S4HanaGLDT07122021
                    }
                }
                return ObjGLMaster;
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

        public int Save(GLCreate1 ObjGLMaster)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_GLMaster_Create";
            int result = 0;


            hashPara.Add("@ID", ObjGLMaster.ID);
            hashPara.Add("@Master_Header_Id", ObjGLMaster.Master_Header_Id);

            hashPara.Add("@GL_Code", ObjGLMaster.GL_Code);
            hashPara.Add("@Ref_GL_Code", ObjGLMaster.Ref_GL_Code);
            hashPara.Add("@Company_Code", ObjGLMaster.Company_Code);
            hashPara.Add("@Account_Group", ObjGLMaster.Account_Group);
            hashPara.Add("@PnL_BalanceSheet", ObjGLMaster.PnL_BalanceSheet);
            hashPara.Add("@Short_Text", ObjGLMaster.Short_Text);

            hashPara.Add("@GL_Acct_Long_Text", ObjGLMaster.GL_Acct_Long_Text);
            hashPara.Add("@Language1", ObjGLMaster.Language1);
            hashPara.Add("@Language2", ObjGLMaster.Language2);
            hashPara.Add("@Rec_Account", ObjGLMaster.Rec_Account);
            hashPara.Add("@Open_Item_Management", ObjGLMaster.Open_Item_Management);
            hashPara.Add("@Line_Item_Display", ObjGLMaster.Line_Item_Display);
            hashPara.Add("@Reason_For_Creation", ObjGLMaster.Reason_For_Creation);
            hashPara.Add("@Remarks", ObjGLMaster.Remarks);
            hashPara.Add("@Ref_Company_Code", ObjGLMaster.Ref_Company_Code);
            hashPara.Add("@CostElementCategory", ObjGLMaster.CostElementCategory);

            hashPara.Add("@CreatedBy", ObjGLMaster.UserId);
            hashPara.Add("@CreatedIP", ObjGLMaster.IPAddress);


            hashPara.Add("@GLAccType", ObjGLMaster.GLAccType); //S4HanaGLDT07122021
            hashPara.Add("@GLAccSubType", ObjGLMaster.GLAccSubType); //S4HanaGLDT07122021
            hashPara.Add("@ClearSpectoLedgerGPS", ObjGLMaster.ClearSpectoLedgerGPS); //S4HanaGLDT07122021

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

        public DataSet CheckIfValid(string Master_Header_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();

            string procName = "pr_Check_GLCode_Exist";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        }

        public DataTable GetRequestNoByMasterHeaderId(string Master_Header_Id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetRequestNoByMasterHeaderId";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds.Tables[0];
        }
    }
}