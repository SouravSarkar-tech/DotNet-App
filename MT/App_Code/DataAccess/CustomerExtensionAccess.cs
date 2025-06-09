using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;
using System.Data.SqlClient;

namespace Accenture.MWT.DataAccess
{
    public class CustomerExtensionAccess
    {
        public CustomerExtensionAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(CustomerExtension ObjCustomerExtension)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Cust_Extension";
            int result = 0;

            hashPara.Add("@Cust_Extension_Id", ObjCustomerExtension.Cust_Extension_Id);
            hashPara.Add("@Master_Header_Id", ObjCustomerExtension.Master_Header_Id);
            hashPara.Add("@Customer_Code", ObjCustomerExtension.Customer_Code);
            hashPara.Add("@Company_Code", ObjCustomerExtension.Company_Code);
            hashPara.Add("@Customer_Acc_Grp", ObjCustomerExtension.Customer_Acc_Grp);
            hashPara.Add("@Customer_Desc", ObjCustomerExtension.Customer_Desc);
            hashPara.Add("@Sales_Organization_Id", ObjCustomerExtension.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_Id", ObjCustomerExtension.Distribution_Channel_Id);
            hashPara.Add("@Division_ID", ObjCustomerExtension.Division_ID);
            hashPara.Add("@countryKeyExport", ObjCustomerExtension.countryKeyExport);
            hashPara.Add("@SalesDistrict", ObjCustomerExtension.SalesDistrict);
            hashPara.Add("@SalesOffice", ObjCustomerExtension.SalesOffice);
            hashPara.Add("@SalesGroup", ObjCustomerExtension.SalesGroup);
            hashPara.Add("@Currency", ObjCustomerExtension.Currency);
            hashPara.Add("@DeliveringPlant", ObjCustomerExtension.DeliveringPlant);
            hashPara.Add("@PriceGroup", ObjCustomerExtension.PriceGroup);
            hashPara.Add("@InvoiceListSchedule", ObjCustomerExtension.InvoiceListSchedule);
            hashPara.Add("@InvoiceDates", ObjCustomerExtension.InvoiceDates);
            hashPara.Add("@Credit_Control_Area", ObjCustomerExtension.Credit_Control_Area);
            hashPara.Add("@Customer_credit_limit", ObjCustomerExtension.Customer_credit_limit);
            hashPara.Add("@Risk_category", ObjCustomerExtension.Risk_category);
            hashPara.Add("@Currency_Id", ObjCustomerExtension.Currency_Id);
            hashPara.Add("@Remarks", ObjCustomerExtension.Remarks);
            hashPara.Add("@IsActive", ObjCustomerExtension.IsActive);
            hashPara.Add("@UserId", ObjCustomerExtension.UserId);
            hashPara.Add("@UserIp", ObjCustomerExtension.IPAddress);

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

        public CustomerExtension GetCustomerExtension(int Cust_Extension_Id)
        {
            CustomerExtension ObjCustomerExtension = new CustomerExtension();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CustomerExtension_By_CustomerExtensionId";
            DataSet ds;

            hashPara.Add("@Cust_Extension_Id", Cust_Extension_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCustomerExtension.Cust_Extension_Id = dt.Rows[0]["Cust_Extension_Id"].ToString() == "" ? Cust_Extension_Id : Convert.ToInt32(dt.Rows[0]["Cust_Extension_Id"].ToString());
                        ObjCustomerExtension.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        //ObjCustomerExtension.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();

                        ObjCustomerExtension.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                        ObjCustomerExtension.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjCustomerExtension.Customer_Acc_Grp = dt.Rows[0]["Customer_Acc_Grp"].ToString();
                        ObjCustomerExtension.Customer_Desc = dt.Rows[0]["Customer_Desc"].ToString();
                        ObjCustomerExtension.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjCustomerExtension.Distribution_Channel_Id = dt.Rows[0]["Distribution_Channel_Id"].ToString();
                        ObjCustomerExtension.Division_ID = dt.Rows[0]["Division_ID"].ToString();
                        ObjCustomerExtension.countryKeyExport = dt.Rows[0]["countryKeyExport"].ToString();
                        ObjCustomerExtension.SalesDistrict = dt.Rows[0]["SalesDistrict"].ToString();
                        ObjCustomerExtension.SalesOffice = dt.Rows[0]["SalesOffice"].ToString();
                        ObjCustomerExtension.SalesGroup = dt.Rows[0]["SalesGroup"].ToString();
                        ObjCustomerExtension.Currency = dt.Rows[0]["Currency"].ToString();
                        ObjCustomerExtension.DeliveringPlant = dt.Rows[0]["DeliveringPlant"].ToString();
                        ObjCustomerExtension.PriceGroup = dt.Rows[0]["PriceGroup"].ToString();
                        ObjCustomerExtension.InvoiceListSchedule = dt.Rows[0]["InvoiceListSchedule"].ToString();
                        ObjCustomerExtension.InvoiceDates = dt.Rows[0]["InvoiceDates"].ToString();
                        ObjCustomerExtension.Credit_Control_Area = dt.Rows[0]["Credit_Control_Area"].ToString();
                        ObjCustomerExtension.Customer_credit_limit = dt.Rows[0]["Customer_credit_limit"].ToString();
                        ObjCustomerExtension.Risk_category = dt.Rows[0]["Risk_category"].ToString();
                        ObjCustomerExtension.Currency_Id = dt.Rows[0]["Currency_Id"].ToString();
                        ObjCustomerExtension.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjCustomerExtension;
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

        public DataSet GetCustomerExtensionData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CustomerExtension_By_MasterHeaderId";
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

        public int DeleteCustomerExtensionData(int Cust_Extension_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_CustomerExtension_By_Cust_Extension_Id";
            int result = 0;

            hashPara.Add("@Cust_Extension_Id", Cust_Extension_Id);

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
        /// CS_8200049196
        /// </summary>
        /// <param name="vMasterID"></param>
        /// <param name="vFlag"></param>
        /// <returns></returns>
        public int IsDuplicateEntry(int vMasterID, string vFlag, string vSalesOrg, string vDistChan, string vDivision)
        { 
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDuplicateSales_Area";
            int result = 0;

            hashPara.Add("@Master_Header_Id", vMasterID);
            hashPara.Add("@Flag", vFlag);
            hashPara.Add("@vSalesOrg", vSalesOrg);
            hashPara.Add("@vDistChan", vDistChan);
            hashPara.Add("@vDivision", vDivision);

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