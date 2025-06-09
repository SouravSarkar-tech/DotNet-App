using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for TaxAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class TaxAccess
    {
        public TaxAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(Taxes ObjTax)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Tax";
            int result = 0;


            hashPara.Add("@Mat_Tax_Id", ObjTax.Mat_Tax_Id);
            hashPara.Add("@Master_Header_Id", ObjTax.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjTax.Plant_Id);
            hashPara.Add("@Sales_Organization_Id", ObjTax.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjTax.Distribution_Channel_ID);
            hashPara.Add("@Tax_Category", ObjTax.Tax_Category);
            hashPara.Add("@Tax_Classification_Mat", ObjTax.Tax_Classification_Mat);
            hashPara.Add("@Tax_Category1", ObjTax.Tax_Category1);
            hashPara.Add("@Tax_Classification_Mat1", ObjTax.Tax_Classification_Mat1);
            hashPara.Add("@Tax_Category2", ObjTax.Tax_Category2);
            hashPara.Add("@Tax_Classification_Mat2", ObjTax.Tax_Classification_Mat2);
            hashPara.Add("@Tax_Category3", ObjTax.Tax_Category3);
            hashPara.Add("@Tax_Classification_Mat3", ObjTax.Tax_Classification_Mat3);
            hashPara.Add("@Tax_Category4", ObjTax.Tax_Category4);
            hashPara.Add("@Tax_Classification_Mat4", ObjTax.Tax_Classification_Mat4);
            hashPara.Add("@Tax_Category5", ObjTax.Tax_Category5);
            hashPara.Add("@Tax_Classification_Mat5", ObjTax.Tax_Classification_Mat5);
            hashPara.Add("@Tax_Category6", ObjTax.Tax_Category6);
            hashPara.Add("@Tax_Classification_Mat6", ObjTax.Tax_Classification_Mat6);
            hashPara.Add("@Tax_Category7", ObjTax.Tax_Category7);
            hashPara.Add("@Tax_Classification_Mat7", ObjTax.Tax_Classification_Mat7);
            hashPara.Add("@Tax_Category8", ObjTax.Tax_Category8);
            hashPara.Add("@Tax_Classification_Mat8", ObjTax.Tax_Classification_Mat8);
            hashPara.Add("@IsActive", ObjTax.IsActive);
            hashPara.Add("@UserId", ObjTax.UserId);
            hashPara.Add("@UserIp", ObjTax.IPAddress);
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

        public Taxes GetTax(int Mat_Tax_Id)
        {
            Taxes ObjTax = new Taxes();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Tax_By_MatTaxId";
            DataSet ds;

            hashPara.Add("@Mat_Tax_Id", Mat_Tax_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjTax.Mat_Tax_Id = Convert.ToInt32(dt.Rows[0]["Mat_Tax_Id"].ToString());
                        ObjTax.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjTax.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjTax.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjTax.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjTax.Tax_Category = dt.Rows[0]["Tax_Category"].ToString();
                        ObjTax.Tax_Classification_Mat = dt.Rows[0]["Tax_Classification_Mat"].ToString();
                        ObjTax.Tax_Category1 = dt.Rows[0]["Tax_Category1"].ToString();
                        ObjTax.Tax_Classification_Mat1 = dt.Rows[0]["Tax_Classification_Mat1"].ToString();
                        ObjTax.Tax_Category2 = dt.Rows[0]["Tax_Category2"].ToString();
                        ObjTax.Tax_Classification_Mat2 = dt.Rows[0]["Tax_Classification_Mat2"].ToString();
                        ObjTax.Tax_Category3 = dt.Rows[0]["Tax_Category3"].ToString();
                        ObjTax.Tax_Classification_Mat3 = dt.Rows[0]["Tax_Classification_Mat3"].ToString();
                        ObjTax.Tax_Category4 = dt.Rows[0]["Tax_Category4"].ToString();
                        ObjTax.Tax_Classification_Mat4 = dt.Rows[0]["Tax_Classification_Mat4"].ToString();
                        ObjTax.Tax_Category5 = dt.Rows[0]["Tax_Category5"].ToString();
                        ObjTax.Tax_Classification_Mat5 = dt.Rows[0]["Tax_Classification_Mat5"].ToString();
                        ObjTax.Tax_Category6 = dt.Rows[0]["Tax_Category6"].ToString();
                        ObjTax.Tax_Classification_Mat6 = dt.Rows[0]["Tax_Classification_Mat6"].ToString();
                        ObjTax.Tax_Category7 = dt.Rows[0]["Tax_Category7"].ToString();
                        ObjTax.Tax_Classification_Mat7 = dt.Rows[0]["Tax_Classification_Mat7"].ToString();
                        ObjTax.Tax_Category8 = dt.Rows[0]["Tax_Category8"].ToString();
                        ObjTax.Tax_Classification_Mat8 = dt.Rows[0]["Tax_Classification_Mat8"].ToString();
                    }
                }
                return ObjTax;
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

        public DataSet GetTaxData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Tax_By_MasterHeaderId";
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