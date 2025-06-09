using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;


/// <summary>
/// Summary description for SalesAreaAccess
/// </summary>
///
namespace Accenture.MWT.DataAccess
{

    public class SalesAreaAccess
    {
        Utility ObjUtil = new Utility();

        public SalesAreaAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int Save1(SalesArea1 ObjSalesArea)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Sales_Area1";
            int result = 0;


            hashPara.Add("@Cust_SalesArea1", ObjSalesArea.Cust_SalesArea1_Id);
            hashPara.Add("@Master_Header_Id", ObjSalesArea.Master_Header_Id);
            hashPara.Add("@Sales_Organization_Id", ObjSalesArea.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjSalesArea.Distribution_Channel_ID);
            hashPara.Add("@Division_ID", ObjSalesArea.Division_ID);
            hashPara.Add("@SalesDistrict", ObjSalesArea.SalesDistrict);
            
            hashPara.Add("@SalesOffice", ObjSalesArea.SalesOffice);
            hashPara.Add("@SalesGroup", ObjSalesArea.SalesGroup);
            hashPara.Add("@Currency", ObjSalesArea.Currency);
            hashPara.Add("@countryKeyExport", ObjSalesArea.countryKeyExport);

            hashPara.Add("@Credit_Control_Area", ObjSalesArea.Credit_Control_Area);
            hashPara.Add("@Customer_credit_limit", ObjSalesArea.Customer_credit_limit);
            hashPara.Add("@Risk_category", ObjSalesArea.Risk_category);
            hashPara.Add("@Currency_Id", ObjSalesArea.Currency_Id);
            hashPara.Add("@DeliveringPlant", ObjSalesArea.DeliveringPlant);
            hashPara.Add("@PriceGroup", ObjSalesArea.PriceGroup);
            hashPara.Add("@InvoiceListSchedule", ObjSalesArea.InvoiceListSchedule);
            hashPara.Add("@InvoiceDates", ObjSalesArea.InvoiceDates);

            hashPara.Add("@IsActive", ObjSalesArea.IsActive);
            hashPara.Add("@UserId", ObjSalesArea.UserId);
            hashPara.Add("@UserIp", ObjSalesArea.IPAddress);

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

        public int Save2(SalesArea2 ObjSalesArea)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Sales_Area2";
            int result = 0;


            hashPara.Add("@Cust_SalesArea2", ObjSalesArea.Cust_SalesArea2_Id);
            hashPara.Add("@Master_Header_Id", ObjSalesArea.Master_Header_Id);
            hashPara.Add("@Sales_Organization_Id", ObjSalesArea.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjSalesArea.Distribution_Channel_ID);
            hashPara.Add("@Division_ID", ObjSalesArea.Division_ID);

            hashPara.Add("@BilingBlockCust", ObjSalesArea.BilingBlockCust);
            hashPara.Add("@IndiCustRebate", ObjSalesArea.IndiCustRebate);
            hashPara.Add("@EXchangeRateTYpe", ObjSalesArea.EXchangeRateTYpe);
            hashPara.Add("@CustomerGroup1", ObjSalesArea.CustomerGroup1);
            hashPara.Add("@CustomerGroup2", ObjSalesArea.CustomerGroup2);
            hashPara.Add("@CustomerGroup3", ObjSalesArea.CustomerGroup3);
            hashPara.Add("@CustomerGroup4", ObjSalesArea.CustomerGroup4);
            hashPara.Add("@CustomerGroup5", ObjSalesArea.CustomerGroup5);
            hashPara.Add("@CustPayGuarantProc", ObjSalesArea.CustPayGuarantProc);
            hashPara.Add("@CreditControlArea", ObjSalesArea.CreditControlArea);
            hashPara.Add("@SalesBlockCust", ObjSalesArea.SalesBlockCust);
            hashPara.Add("@SwitchOffRound", ObjSalesArea.SwitchOffRound);
            hashPara.Add("@CustClassABC", ObjSalesArea.CustClassABC);
            hashPara.Add("@TaxCategory", ObjSalesArea.TaxCategory);
            hashPara.Add("@TaxClassificationCust", ObjSalesArea.TaxClassificationCust);
            hashPara.Add("@LicenceNumber", ObjSalesArea.LicenceNumber);
            hashPara.Add("@DateBatchInput", ObjSalesArea.DateBatchInput);
            hashPara.Add("@DateBatchin2", ObjSalesArea.DateBatchin2);
            hashPara.Add("@ConfirmationLicenses", ObjSalesArea.ConfirmationLicenses);

            hashPara.Add("@OrderProbabilityitem", ObjSalesArea.OrderProbabilityitem);
            hashPara.Add("@ItemProposal", ObjSalesArea.ItemProposal);
            hashPara.Add("@CustomerGroup", ObjSalesArea.CustomerGroup);
            hashPara.Add("@ShipperAccountCustVendor", ObjSalesArea.ShipperAccountCustVendor);
           
            hashPara.Add("@PricingProcuderAssCust", ObjSalesArea.PricingProcuderAssCust);
            hashPara.Add("@DeliveryPriority", ObjSalesArea.DeliveryPriority);
            hashPara.Add("@PriceListType", ObjSalesArea.PriceListType);
            hashPara.Add("@CustStatisticsGroup", ObjSalesArea.CustStatisticsGroup);
            hashPara.Add("@OrderCombinationIndi", ObjSalesArea.OrderCombinationIndi);
            hashPara.Add("@ShippingCondition", ObjSalesArea.ShippingCondition);
            
            hashPara.Add("@CompleteDeliverySalesOrder", ObjSalesArea.CompleteDeliverySalesOrder);
            hashPara.Add("@PartialItemLevel", ObjSalesArea.PartialItemLevel);
            hashPara.Add("@MaxPermittedDeliveries", ObjSalesArea.MaxPermittedDeliveries);
            hashPara.Add("@IncotermsPart1", ObjSalesArea.IncotermsPart1);
            hashPara.Add("@IncotermsPart2", ObjSalesArea.IncotermsPart2);
            hashPara.Add("@TermPaymentKey", ObjSalesArea.TermPaymentKey);
            hashPara.Add("@AccAssignmentCust", ObjSalesArea.AccAssignmentCust);
            hashPara.Add("@DeletionFlagCust", ObjSalesArea.DeletionFlagCust);
            hashPara.Add("@CustOrderBlock", ObjSalesArea.CustOrderBlock);
            hashPara.Add("@CustDeliveryBlock", ObjSalesArea.CustDeliveryBlock);
            hashPara.Add("@AuthorizationGroup", ObjSalesArea.AuthorizationGroup);

            hashPara.Add("@PartnerFunction", ObjSalesArea.PartnerFunction);
            hashPara.Add("@NumberSDBusinPartner", ObjSalesArea.NumberSDBusinPartner);
            hashPara.Add("@PartnerFunction2", ObjSalesArea.PartnerFunction2);
            hashPara.Add("@NumberSDBusinPartner2", ObjSalesArea.NumberSDBusinPartner2);
            hashPara.Add("@PartnerFunction3", ObjSalesArea.PartnerFunction3);
            hashPara.Add("@NumberSDBusinPartner3", ObjSalesArea.NumberSDBusinPartner3);
            hashPara.Add("@PartnerFunction4", ObjSalesArea.PartnerFunction4);
            hashPara.Add("@NumberSDBusinPartner4", ObjSalesArea.NumberSDBusinPartner4);
            hashPara.Add("@PartnerFunction5", ObjSalesArea.PartnerFunction5);
            hashPara.Add("@NumberSDBusinPartner5", ObjSalesArea.NumberSDBusinPartner5);
            hashPara.Add("@PartnerFunction6", ObjSalesArea.PartnerFunction6);
            hashPara.Add("@NumberSDBusinPartner6", ObjSalesArea.NumberSDBusinPartner6);
            hashPara.Add("@DefaultPartner", ObjSalesArea.DefaultPartner);
            hashPara.Add("@CateIndiTaxCodes", ObjSalesArea.CateIndiTaxCodes);
            hashPara.Add("@IsActive", ObjSalesArea.IsActive);
            hashPara.Add("@UserId", ObjSalesArea.UserId);
            hashPara.Add("@UserIp", ObjSalesArea.IPAddress);

            hashPara.Add("@TCSYesNo", ObjSalesArea.TCSYesNo);

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

        public SalesArea1 GetSalesArea1(int CustSalesArea1Id)
        {
            SalesArea1 ObjSalesArea = new SalesArea1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_SalesArea1_By_Cust_SalesArea1Id";
            DataSet ds;

            hashPara.Add("@Cust_SalesArea1_Id", CustSalesArea1Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjSalesArea.Cust_SalesArea1_Id = Convert.ToInt32(dt.Rows[0]["Cust_SalesArea1"].ToString());
                        ObjSalesArea.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjSalesArea.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjSalesArea.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjSalesArea.Division_ID = dt.Rows[0]["Division_ID"].ToString();

                        ObjSalesArea.SalesDistrict = dt.Rows[0]["SalesDistrict"].ToString();
                        ObjSalesArea.SalesOffice = dt.Rows[0]["SalesOffice"].ToString();
                        ObjSalesArea.SalesGroup = dt.Rows[0]["SalesGroup"].ToString();
                        ObjSalesArea.Currency = dt.Rows[0]["Currency"].ToString();
                        ObjSalesArea.DeliveringPlant = dt.Rows[0]["DeliveringPlant"].ToString();
                        ObjSalesArea.countryKeyExport = dt.Rows[0]["countryKeyExport"].ToString();
                        ObjSalesArea.PriceGroup = dt.Rows[0]["PriceGroup"].ToString();
                        ObjSalesArea.InvoiceListSchedule = dt.Rows[0]["InvoiceListSchedule"].ToString();
                        ObjSalesArea.InvoiceDates = dt.Rows[0]["InvoiceDates"].ToString();

                        ObjSalesArea.Credit_Control_Area = dt.Rows[0]["Credit_Control_Area"].ToString();
                        ObjSalesArea.Customer_credit_limit = dt.Rows[0]["Customer_credit_limit"].ToString();
                        ObjSalesArea.Risk_category = dt.Rows[0]["Risk_category"].ToString();
                        ObjSalesArea.Currency_Id = dt.Rows[0]["Currency_Id"].ToString();

                    }
                }
                return ObjSalesArea;
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

        public SalesArea2 GetSalesArea2(int intMasterHeaderId)
        {
            SalesArea2 ObjSalesArea = new SalesArea2();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_SalesArea2_By_MasterHeaderId";
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
                        ObjSalesArea.Cust_SalesArea2_Id = Convert.ToInt32(dt.Rows[0]["Cust_SalesArea2"].ToString());
                        ObjSalesArea.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjSalesArea.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjSalesArea.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjSalesArea.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjSalesArea.Division_ID = dt.Rows[0]["Division_ID"].ToString();

                        ObjSalesArea.BilingBlockCust = dt.Rows[0]["BilingBlockCust"].ToString();
                        ObjSalesArea.IndiCustRebate = dt.Rows[0]["IndiCustRebate"].ToString();
                        ObjSalesArea.EXchangeRateTYpe = dt.Rows[0]["EXchangeRateTYpe"].ToString();
                        ObjSalesArea.CustomerGroup1 = dt.Rows[0]["CustomerGroup1"].ToString();
                        ObjSalesArea.CustomerGroup2 = dt.Rows[0]["CustomerGroup2"].ToString();
                        ObjSalesArea.CustomerGroup3 = dt.Rows[0]["CustomerGroup3"].ToString();
                        ObjSalesArea.CustomerGroup4 = dt.Rows[0]["CustomerGroup4"].ToString();
                        ObjSalesArea.CustomerGroup5 = dt.Rows[0]["CustomerGroup5"].ToString();
                        ObjSalesArea.CustPayGuarantProc = dt.Rows[0]["CustPayGuarantProc"].ToString();
                        ObjSalesArea.CreditControlArea = dt.Rows[0]["CreditControlArea"].ToString();
                        ObjSalesArea.SalesBlockCust = dt.Rows[0]["SalesBlockCust"].ToString();
                        ObjSalesArea.SwitchOffRound = dt.Rows[0]["SwitchOffRound"].ToString();
                        ObjSalesArea.CustClassABC = dt.Rows[0]["CustClassABC"].ToString();
                        ObjSalesArea.TaxCategory = dt.Rows[0]["TaxCategory"].ToString();
                        ObjSalesArea.TaxClassificationCust = dt.Rows[0]["TaxClassificationCust"].ToString();
                        ObjSalesArea.LicenceNumber = dt.Rows[0]["LicenceNumber"].ToString();
                        ObjSalesArea.DateBatchInput = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["DateBatchInput"].ToString());
                        ObjSalesArea.DateBatchin2 = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["DateBatchin2"].ToString());
                        ObjSalesArea.ConfirmationLicenses = dt.Rows[0]["ConfirmationLicenses"].ToString();

                        ObjSalesArea.OrderProbabilityitem = dt.Rows[0]["OrderProbabilityitem"].ToString();
                        ObjSalesArea.ItemProposal = dt.Rows[0]["ItemProposal"].ToString();
                        ObjSalesArea.CustomerGroup = dt.Rows[0]["CustomerGroup"].ToString();
                        ObjSalesArea.ShipperAccountCustVendor = dt.Rows[0]["ShipperAccountCustVendor"].ToString();
                        
                        ObjSalesArea.PricingProcuderAssCust = dt.Rows[0]["PricingProcuderAssCust"].ToString();
                        ObjSalesArea.DeliveryPriority = dt.Rows[0]["DeliveryPriority"].ToString();
                        ObjSalesArea.PriceListType = dt.Rows[0]["PriceListType"].ToString();
                        ObjSalesArea.CustStatisticsGroup = dt.Rows[0]["CustStatisticsGroup"].ToString();
                        ObjSalesArea.OrderCombinationIndi = dt.Rows[0]["OrderCombinationIndi"].ToString();
                        ObjSalesArea.ShippingCondition = dt.Rows[0]["ShippingCondition"].ToString();
                        
                        ObjSalesArea.CompleteDeliverySalesOrder = dt.Rows[0]["CompleteDeliverySalesOrder"].ToString();
                        ObjSalesArea.PartialItemLevel = dt.Rows[0]["PartialItemLevel"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["PartialItemLevel"].ToString());
                        ObjSalesArea.MaxPermittedDeliveries = dt.Rows[0]["MaxPermittedDeliveries"].ToString();
                        ObjSalesArea.IncotermsPart1 = dt.Rows[0]["IncotermsPart1"].ToString();
                        ObjSalesArea.IncotermsPart2 = dt.Rows[0]["IncotermsPart2"].ToString();
                        ObjSalesArea.TermPaymentKey = dt.Rows[0]["TermPaymentKey"].ToString();
                        ObjSalesArea.AccAssignmentCust = dt.Rows[0]["AccAssignmentCust"].ToString();
                        ObjSalesArea.DeletionFlagCust = dt.Rows[0]["DeletionFlagCust"].ToString();
                        ObjSalesArea.CustOrderBlock = dt.Rows[0]["CustOrderBlock"].ToString();
                        ObjSalesArea.CustDeliveryBlock = dt.Rows[0]["CustDeliveryBlock"].ToString();
                        ObjSalesArea.AuthorizationGroup = dt.Rows[0]["AuthorizationGroup"].ToString();

                        ObjSalesArea.PartnerFunction = dt.Rows[0]["PartnerFunction"].ToString();
                        ObjSalesArea.NumberSDBusinPartner = dt.Rows[0]["NumberSDBusinPartner"].ToString();
                        ObjSalesArea.PartnerFunction2 = dt.Rows[0]["PartnerFunction2"].ToString();
                        ObjSalesArea.NumberSDBusinPartner2 = dt.Rows[0]["NumberSDBusinPartner2"].ToString();
                        ObjSalesArea.PartnerFunction3 = dt.Rows[0]["PartnerFunction3"].ToString();
                        ObjSalesArea.NumberSDBusinPartner3 = dt.Rows[0]["NumberSDBusinPartner3"].ToString();
                        ObjSalesArea.PartnerFunction4 = dt.Rows[0]["PartnerFunction4"].ToString();
                        ObjSalesArea.NumberSDBusinPartner4 = dt.Rows[0]["NumberSDBusinPartner4"].ToString();
                        ObjSalesArea.PartnerFunction5 = dt.Rows[0]["PartnerFunction5"].ToString();
                        ObjSalesArea.NumberSDBusinPartner5 = dt.Rows[0]["NumberSDBusinPartner5"].ToString();
                        ObjSalesArea.PartnerFunction6 = dt.Rows[0]["PartnerFunction6"].ToString();
                        ObjSalesArea.NumberSDBusinPartner6 = dt.Rows[0]["NumberSDBusinPartner6"].ToString();
                        ObjSalesArea.DefaultPartner = dt.Rows[0]["DefaultPartner"].ToString();
                        ObjSalesArea.CateIndiTaxCodes = dt.Rows[0]["CateIndiTaxCodes"].ToString();
                        //TCSDT20072021 Start
                        ObjSalesArea.TCSYesNo = dt.Rows[0]["TCSYesNo"].ToString();
                        //TCSDT20072021 Start
                    }
                }
                return ObjSalesArea;
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

        public DataSet GetSalesArea1Data(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_SalesArea1_By_MasterHeaderId";
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

        public int DeleteSalesArea1Data(int CustSalesArea1Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_Cust_SalesArea1_By_Cust_SalesArea1Id";
            int result = 0;

            hashPara.Add("@Cust_SalesArea1_Id", CustSalesArea1Id);

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


        //DT 03.12.2018 by Swati to Autofill Division
        public DataSet GetDivisionType(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_DivisionType_By_MasterHeaderId";
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
        //End


        /// <summary>
        /// CS_8200049196
        /// </summary>
        /// <param name="ObjVendorChange"></param>
        /// <returns></returns>
        public int SaveImportCSales(SalesArea1 ObjSalesArea)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            //string procName = "pr_Ins_Upd_T_Sales_Area1_ExcelIMP";
            string procName = "pr_Ins_Upd_T_Sales_Area1";
            int result = 0;

            hashPara.Add("@Cust_SalesArea1", ObjSalesArea.Cust_SalesArea1_Id);
            hashPara.Add("@Master_Header_Id", ObjSalesArea.Master_Header_Id);
            hashPara.Add("@Sales_Organization_Id", ObjSalesArea.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjSalesArea.Distribution_Channel_ID);
            hashPara.Add("@Division_ID", ObjSalesArea.Division_ID);
            hashPara.Add("@SalesDistrict", ObjSalesArea.SalesDistrict);

            hashPara.Add("@SalesOffice", ObjSalesArea.SalesOffice);
            hashPara.Add("@SalesGroup", ObjSalesArea.SalesGroup);
            hashPara.Add("@Currency", ObjSalesArea.Currency);
            hashPara.Add("@countryKeyExport", ObjSalesArea.countryKeyExport);

            hashPara.Add("@Credit_Control_Area", ObjSalesArea.Credit_Control_Area);
            hashPara.Add("@Customer_credit_limit", ObjSalesArea.Customer_credit_limit);
            hashPara.Add("@Risk_category", ObjSalesArea.Risk_category);
            hashPara.Add("@Currency_Id", ObjSalesArea.Currency_Id);
            hashPara.Add("@DeliveringPlant", ObjSalesArea.DeliveringPlant);
            hashPara.Add("@PriceGroup", ObjSalesArea.PriceGroup);
            hashPara.Add("@InvoiceListSchedule", ObjSalesArea.InvoiceListSchedule);
            hashPara.Add("@InvoiceDates", ObjSalesArea.InvoiceDates);

            hashPara.Add("@IsActive", ObjSalesArea.IsActive);
            hashPara.Add("@UserId", ObjSalesArea.UserId);
            hashPara.Add("@UserIp", ObjSalesArea.IPAddress);

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