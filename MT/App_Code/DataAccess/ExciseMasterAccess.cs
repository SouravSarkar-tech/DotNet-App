using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

/// <summary>
/// Summary description for BOMAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class ExciseMasterAccess
    {
        public int SaveChapterId(ChapterId ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_Chapter";
            int result = 0;

            hashPara.Add("@ChapterPk_Id", ObjAcc.ChapterPk_Id);
            hashPara.Add("@Chapter_Id", ObjAcc.Chapter_Id);
            hashPara.Add("@UOM", ObjAcc.UOM);
            hashPara.Add("@Desc_Per_Law1", ObjAcc.Desc_Per_Law1);
            hashPara.Add("@Desc_Per_Law2", ObjAcc.Desc_Per_Law2);
            hashPara.Add("@Desc_Per_Law3", ObjAcc.Desc_Per_Law3);
            hashPara.Add("@Desc_Per_Law4", ObjAcc.Desc_Per_Law4);
            hashPara.Add("@Desc_Per_Law5", ObjAcc.Desc_Per_Law5);
            hashPara.Add("@Desc_Per_Law6", ObjAcc.Desc_Per_Law6);
            hashPara.Add("@Desc_Per_Law7", ObjAcc.Desc_Per_Law7);
            hashPara.Add("@Desc_Per_Law8", ObjAcc.Desc_Per_Law8);
            hashPara.Add("@UserId", ObjAcc.UserId);

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
        public int SaveMaterialChapterCombination(MaterialChapterCombination ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_Material_Chapter_Combination";
            int result = 0;

            hashPara.Add("@Mat_Ch_Combi_Id", ObjAcc.Mat_Ch_Combi_Id);
            hashPara.Add("@Material_No", ObjAcc.Material_No);
            hashPara.Add("@Plant", ObjAcc.Plant);
            hashPara.Add("@Chapter_ID", ObjAcc.Chapter_ID);
            hashPara.Add("@Mat_Subcontractors", ObjAcc.Mat_Subcontractors);
            hashPara.Add("@Material_Type", ObjAcc.Material_Type);
            hashPara.Add("@Number_Goods", ObjAcc.Number_Goods);
            hashPara.Add("@Indicator", ObjAcc.Indicator);
            hashPara.Add("@Declaration_date", ObjAcc.Declaration_date);
            hashPara.Add("@UserId", ObjAcc.UserId);

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

        public int SaveCENVATDetermination(CENVATDetermination ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_CENVAT_Determination";
            int result = 0;

            hashPara.Add("@CENVAT_Determination_Id", ObjAcc.CENVAT_Determination_Id);
            hashPara.Add("@Plant", ObjAcc.Plant);
            hashPara.Add("@Input_material", ObjAcc.Input_material);
            hashPara.Add("@Output_material", ObjAcc.Output_material);
            hashPara.Add("@Default_Indicator", ObjAcc.Default_Indicator);
            hashPara.Add("@Excise_Intimation", ObjAcc.Excise_Intimation);
            hashPara.Add("@UserId", ObjAcc.UserId);

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

        public int SaveVendorExciseDetails(VendorExciseDetails ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_Vendor_Excise_Details";
            int result = 0;

            hashPara.Add("@Vendor_Excise_Details_Id", ObjAcc.Vendor_Excise_Details_Id);
            hashPara.Add("@Account_No", ObjAcc.Account_No);
            hashPara.Add("@ECC_NO", ObjAcc.ECC_NO);
            hashPara.Add("@Excise_Reg_No", ObjAcc.Excise_Reg_No);
            hashPara.Add("@Excise_Range", ObjAcc.Excise_Range);
            hashPara.Add("@Excise_Division", ObjAcc.Excise_Division);
            hashPara.Add("@Excise_Commissionerate", ObjAcc.Excise_Commissionerate);
            hashPara.Add("@Central_Sales_Tax_No", ObjAcc.Central_Sales_Tax_No);
            hashPara.Add("@Local_Sales_Tax_No", ObjAcc.Local_Sales_Tax_No);
            hashPara.Add("@PAN", ObjAcc.PAN);
            hashPara.Add("@Excise_tax_indicator", ObjAcc.Excise_tax_indicator);
            hashPara.Add("@SSI", ObjAcc.SSI);
            hashPara.Add("@Type_Of_Vendor", ObjAcc.Type_Of_Vendor);
            hashPara.Add("@CENVAT", ObjAcc.CENVAT);
            hashPara.Add("@Service_Tax_Reg", ObjAcc.Service_Tax_Reg);
            hashPara.Add("@PAN_Ref_No", ObjAcc.PAN_Ref_No);
            hashPara.Add("@UserId", ObjAcc.UserId);

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

        public int SaveCustomerExciseDetails(CustomerExciseDetails ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_Customer_Excise_Details";
            int result = 0;

            hashPara.Add("@Customer_Excise_Detail_Id", ObjAcc.Customer_Excise_Detail_Id);
            hashPara.Add("@Customer_Number", ObjAcc.Customer_Number);
            hashPara.Add("@ECC_NO", ObjAcc.ECC_NO);
            hashPara.Add("@Excise_Reg_No", ObjAcc.Excise_Reg_No);
            hashPara.Add("@Excise_Range", ObjAcc.Excise_Range);
            hashPara.Add("@Excise_Division", ObjAcc.Excise_Division);
            hashPara.Add("@Excise_Commissionerate", ObjAcc.Excise_Commissionerate);
            hashPara.Add("@Central_Sales_Tax_No", ObjAcc.Central_Sales_Tax_No);
            hashPara.Add("@Local_Sales_Tax_No", ObjAcc.Local_Sales_Tax_No);
            hashPara.Add("@PAN", ObjAcc.PAN);
            hashPara.Add("@Excise_tax_indicator", ObjAcc.Excise_tax_indicator);
            hashPara.Add("@Service_Tax_Reg", ObjAcc.Service_Tax_Reg);
            hashPara.Add("@PAN_Ref_No", ObjAcc.PAN_Ref_No);
            hashPara.Add("@UserId", ObjAcc.UserId);

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

        public int SaveExciseTaxRate(ExciseTaxRate ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_Excise_Tax_Rate";
            int result = 0;

            hashPara.Add("Excise_Tax_Rate_Id", ObjAcc.Excise_Tax_Rate_Id);
            hashPara.Add("Chapter_ID", ObjAcc.Chapter_ID);
            hashPara.Add("Excise_tax_indicator", ObjAcc.Excise_tax_indicator);
            hashPara.Add("Date_from_rule_valid", ObjAcc.Date_from_rule_valid);
            hashPara.Add("Date_To_rule_valid", ObjAcc.Date_To_rule_valid);
            hashPara.Add("Rate_Excise_Duty", ObjAcc.Rate_Excise_Duty);
            hashPara.Add("Excise_Duty_Rate", ObjAcc.Excise_Duty_Rate);
            hashPara.Add("Rate_Unit", ObjAcc.Rate_Unit);
            hashPara.Add("Condition_pricing_unit", ObjAcc.Condition_pricing_unit);
            hashPara.Add("Condition_unit", ObjAcc.Condition_unit);
            hashPara.Add("Additional_Excise_Duty", ObjAcc.Additional_Excise_Duty);
            hashPara.Add("Special_Excise_Duty", ObjAcc.Special_Excise_Duty);
            hashPara.Add("NCCD_Rate", ObjAcc.NCCD_Rate);
            hashPara.Add("ECS_Rate", ObjAcc.ECS_Rate);
            hashPara.Add("AT1_rate", ObjAcc.AT1_rate);
            hashPara.Add("AT2_rate", ObjAcc.AT2_rate);
            hashPara.Add("AT3_rate", ObjAcc.AT3_rate);
            hashPara.Add("@UserId", ObjAcc.UserId);

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

        public int SaveExceptionMaterialExciseRate(ExceptionMaterialExciseRate ObjAcc)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_Ins_Upd_T_Excise_Exception_Material_Excise_Rate";
            int result = 0;

            hashPara.Add("@Exception_Material_Excise_Rate_Id", ObjAcc.Exception_Material_Excise_Rate_Id);
            hashPara.Add("@Plant", ObjAcc.Plant);
            hashPara.Add("@Material_Number", ObjAcc.Material_Number);
            hashPara.Add("@Account_No", ObjAcc.Account_No);
            hashPara.Add("@Date_from_valid", ObjAcc.Date_from_valid);
            hashPara.Add("@Type_Excise_duty", ObjAcc.Type_Excise_duty);
            hashPara.Add("@Date_to_valid", ObjAcc.Date_to_valid);
            hashPara.Add("@Chapter_ID", ObjAcc.Chapter_ID);
            hashPara.Add("@Rate_Excise_Duty", ObjAcc.Rate_Excise_Duty);
            hashPara.Add("@Excise_Duty_Rate", ObjAcc.Excise_Duty_Rate);
            hashPara.Add("@Rate_unit", ObjAcc.Rate_unit);
            hashPara.Add("@Condition_pricing_unit", ObjAcc.Condition_pricing_unit);
            hashPara.Add("@Condition_unit", ObjAcc.Condition_unit);
            hashPara.Add("@UserId", ObjAcc.UserId);

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