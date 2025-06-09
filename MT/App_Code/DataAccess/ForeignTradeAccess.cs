using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for ForeignTradeAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class ForeignTradeAccess
    {
        public ForeignTradeAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(ForeignTrade ObjForeignTrade)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Foreign_Trade";
            int result = 0;


            hashPara.Add("@Mat_Foreign_Trade_Id", ObjForeignTrade.Mat_Foreign_Trade_Id);
            hashPara.Add("@Master_Header_Id", ObjForeignTrade.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjForeignTrade.Plant_Id);

            hashPara.Add("@Sales_Organization_Id", ObjForeignTrade.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjForeignTrade.Distribution_Channel_ID);

            hashPara.Add("@Commodity_Code", ObjForeignTrade.Commodity_Code);
            hashPara.Add("@Origin_Country_Id", ObjForeignTrade.Origin_Country_Id);
            hashPara.Add("@Origin_Region_Id", ObjForeignTrade.Origin_Region_Id);
            hashPara.Add("@Imp_Exp_Mat_Grp", ObjForeignTrade.Imp_Exp_Mat_Grp);
            hashPara.Add("@Preference_Indicator_Imp_Exp", ObjForeignTrade.Preference_Indicator_Imp_Exp);
            hashPara.Add("@Exception_Certificate", ObjForeignTrade.Exception_Certificate);
            hashPara.Add("@Control_Code", ObjForeignTrade.Control_Code);

            hashPara.Add("@Chapter_ID", ObjForeignTrade.Chapter_ID);
            hashPara.Add("@Subcontractors", ObjForeignTrade.Subcontractors);
            hashPara.Add("@Material_Type", ObjForeignTrade.Material_Type);
            hashPara.Add("@No_of_Goods_Receipts_per_Excise_Invoice", ObjForeignTrade.No_of_Goods_Receipts_per_Excise_Invoice);
            hashPara.Add("@Output_Material_For_ModVat", ObjForeignTrade.Output_Material_For_ModVat);
            hashPara.Add("@Remarks", ObjForeignTrade.Remarks);
            //GST start
            hashPara.Add("@GSTRate", ObjForeignTrade.GSTRate);
            hashPara.Add("@GSTReq", ObjForeignTrade.GSTReq);
            //GST End

            hashPara.Add("@IsActive", ObjForeignTrade.IsActive);
            hashPara.Add("@UserId", ObjForeignTrade.UserId);
            hashPara.Add("@UserIp", ObjForeignTrade.IPAddress);
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

        public ForeignTrade GetForeignTrade(int Mat_Foreign_Trade_Id)
        {
            ForeignTrade ObjForeignTrade = new ForeignTrade();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Foreign_Trade_By_MatForeignTradeId";
            DataSet ds;

            hashPara.Add("@Mat_Foreign_Trade_Id", Mat_Foreign_Trade_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjForeignTrade.Mat_Foreign_Trade_Id = Convert.ToInt32( dt.Rows[0]["Mat_Foreign_Trade_Id"].ToString());
                        ObjForeignTrade.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjForeignTrade.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();

                        ObjForeignTrade.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjForeignTrade.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();

                        ObjForeignTrade.Commodity_Code = dt.Rows[0]["Commodity_Code"].ToString();
                        ObjForeignTrade.Origin_Country_Id =dt.Rows[0]["Origin_Country_Id"].ToString();
                        ObjForeignTrade.Origin_Region_Id = dt.Rows[0]["Origin_Region_Id"].ToString();
                        ObjForeignTrade.Imp_Exp_Mat_Grp = dt.Rows[0]["Imp_Exp_Mat_Grp"].ToString();
                        ObjForeignTrade.Preference_Indicator_Imp_Exp = dt.Rows[0]["Preference_Indicator_Imp_Exp"].ToString();
                        ObjForeignTrade.Exception_Certificate = dt.Rows[0]["Exception_Certificate"].ToString();
                        ObjForeignTrade.Control_Code = dt.Rows[0]["Control_Code"].ToString();

                        ObjForeignTrade.Chapter_ID = dt.Rows[0]["Chapter_ID"].ToString();
                        ObjForeignTrade.Subcontractors = dt.Rows[0]["Subcontractors"].ToString().ToLower() == "true" ? "1" : "0";
                        ObjForeignTrade.Material_Type = dt.Rows[0]["Material_Type"].ToString();
                        ObjForeignTrade.No_of_Goods_Receipts_per_Excise_Invoice = dt.Rows[0]["No_of_Goods_Receipts_per_Excise_Invoice"].ToString();
                        ObjForeignTrade.Output_Material_For_ModVat = dt.Rows[0]["Output_Material_For_ModVat"].ToString();
                        ObjForeignTrade.Remarks = dt.Rows[0]["Remarks"].ToString();
                        //GST Start
                        ObjForeignTrade.GSTRate = dt.Rows[0]["GSTRate"].ToString();
                        ObjForeignTrade.GSTReq = dt.Rows[0]["GSTReq"].ToString();
                        //GST End
                    }
                }
                return ObjForeignTrade;
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

        public DataSet GetForeignTradeData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Foreign_Trade_By_MasterHeaderId";
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