using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CustomerGeneralAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class CustomerGeneralAccess
    {
        Utility ObjUtil = new Utility();

        public CustomerGeneralAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(CustomerGeneral1 ObjCustGeneral)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Cust_General";
            int result = 0;

            hashPara.Add("@Cust_General1_Id", ObjCustGeneral.Cust_General1_Id);
            hashPara.Add("@Master_Header_Id", ObjCustGeneral.Master_Header_Id);

            hashPara.Add("@Customer_Code", ObjCustGeneral.Customer_Code);
            hashPara.Add("@Company_Code", ObjCustGeneral.Company_Code);
            hashPara.Add("@Customer_Acc_Grp", ObjCustGeneral.Customer_Acc_Grp);
            hashPara.Add("@Customer_Category", ObjCustGeneral.Customer_Category);

            hashPara.Add("@Title", ObjCustGeneral.Title);
            hashPara.Add("@Name1", ObjCustGeneral.Name1);
            hashPara.Add("@Name2", ObjCustGeneral.Name2);
            hashPara.Add("@Name3", ObjCustGeneral.Name3);
            hashPara.Add("@Name4", ObjCustGeneral.Name4);
            hashPara.Add("@Sort_Field", ObjCustGeneral.Sort_Field);
            hashPara.Add("@Name_CO", ObjCustGeneral.Name_CO);
            hashPara.Add("@HouseNo_Street", ObjCustGeneral.HouseNo_Street);
            hashPara.Add("@Street2", ObjCustGeneral.Street2);
            hashPara.Add("@Street3", ObjCustGeneral.Street3);
            hashPara.Add("@Street4", ObjCustGeneral.Street4);
            hashPara.Add("@Street5", ObjCustGeneral.Street5);
            hashPara.Add("@City", ObjCustGeneral.City);
            hashPara.Add("@Postal_Code", ObjCustGeneral.Postal_Code);
            hashPara.Add("@Different_City", ObjCustGeneral.Different_City);
            hashPara.Add("@District", ObjCustGeneral.District);
            hashPara.Add("@PO_Box", ObjCustGeneral.PO_Box);
            hashPara.Add("@PO_Box_Postal_Code", ObjCustGeneral.PO_Box_Postal_Code);
            hashPara.Add("@CountryKey", ObjCustGeneral.CountryKey);
            hashPara.Add("@Region", ObjCustGeneral.Region);
            hashPara.Add("@LanguageAcc", ObjCustGeneral.LanguageAcc);
            hashPara.Add("@Mobile_Num", ObjCustGeneral.Mobile_Num);
            hashPara.Add("@Mobile_Num2", ObjCustGeneral.Mobile_Num2);
            hashPara.Add("@First_Tele_No", ObjCustGeneral.First_Tele_No);
            hashPara.Add("@Second_Tele_No", ObjCustGeneral.Second_Tele_No);
            hashPara.Add("@Fax_NO", ObjCustGeneral.Fax_NO);
            hashPara.Add("@Email_Address", ObjCustGeneral.Email_Address);
            hashPara.Add("@Email_Address2", ObjCustGeneral.Email_Address2);
            hashPara.Add("@Email_Address3", ObjCustGeneral.Email_Address3);
            hashPara.Add("@Transportation_Zone", ObjCustGeneral.Transportation_Zone);
            hashPara.Add("@Tax_Jurisdiction", ObjCustGeneral.Tax_Jurisdiction);
            hashPara.Add("@Acc_No_Vendor", ObjCustGeneral.Acc_No_Vendor);
            hashPara.Add("@Com_Id_TradingPat", ObjCustGeneral.Com_Id_TradingPat);
            hashPara.Add("@Group_Key", ObjCustGeneral.Group_Key);
            hashPara.Add("@LiableVat", ObjCustGeneral.LiableVat);
            hashPara.Add("@Country_Code", ObjCustGeneral.Country_Code);
            hashPara.Add("@City_Code", ObjCustGeneral.City_Code);
            hashPara.Add("@IsActive", ObjCustGeneral.IsActive);
            hashPara.Add("@UserId", ObjCustGeneral.UserId);
            hashPara.Add("@UserIp", ObjCustGeneral.IPAddress);
            //Start Addition By Swati M Date: 12.10.2018
            hashPara.Add("@Remarks", ObjCustGeneral.Remarks);
            //End Addition By Swati M Date: 12.10.2018

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

        public CustomerGeneral1 GetCustomerGeneral1(int intMasterHeaderId)
        {
            CustomerGeneral1 ObjCustGeneral = new CustomerGeneral1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_General_By_MasterHeaderId";
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
                        ObjCustGeneral.Cust_General1_Id = Convert.ToInt32(dt.Rows[0]["Cust_General1_Id"].ToString());
                        ObjCustGeneral.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjCustGeneral.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                        ObjCustGeneral.Customer_Acc_Grp = dt.Rows[0]["Customer_Acc_Grp"].ToString();
                        ObjCustGeneral.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjCustGeneral.Customer_Category = dt.Rows[0]["Customer_Category"].ToString();

                        ObjCustGeneral.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjCustGeneral.Title = dt.Rows[0]["Title"].ToString();
                        ObjCustGeneral.Name1 = dt.Rows[0]["Name1"].ToString();
                        ObjCustGeneral.Name2 = dt.Rows[0]["Name2"].ToString();
                        ObjCustGeneral.Name3 = dt.Rows[0]["Name3"].ToString();
                        ObjCustGeneral.Name4 = dt.Rows[0]["Name4"].ToString();
                        ObjCustGeneral.Sort_Field = dt.Rows[0]["Sort_Field"].ToString();
                        ObjCustGeneral.Name_CO = dt.Rows[0]["Name_CO"].ToString();
                        ObjCustGeneral.HouseNo_Street = dt.Rows[0]["HouseNo_Street"].ToString();
                        ObjCustGeneral.Street2 = dt.Rows[0]["Street2"].ToString();
                        ObjCustGeneral.Street3 = dt.Rows[0]["Street3"].ToString();
                        ObjCustGeneral.Street4 = dt.Rows[0]["Street4"].ToString();
                        ObjCustGeneral.Street5 = dt.Rows[0]["Street5"].ToString();
                        ObjCustGeneral.City = dt.Rows[0]["City"].ToString();
                        ObjCustGeneral.Postal_Code = dt.Rows[0]["Postal_Code"].ToString();
                        ObjCustGeneral.Different_City = dt.Rows[0]["Different_City"].ToString();
                        ObjCustGeneral.District = dt.Rows[0]["District"].ToString();
                        ObjCustGeneral.PO_Box = dt.Rows[0]["PO_Box"].ToString();
                        ObjCustGeneral.PO_Box_Postal_Code = dt.Rows[0]["PO_Box_Postal_Code"].ToString();
                        ObjCustGeneral.CountryKey = dt.Rows[0]["CountryKey"].ToString();
                        ObjCustGeneral.Region = dt.Rows[0]["Region"].ToString();
                        ObjCustGeneral.LanguageAcc = dt.Rows[0]["LanguageAcc"].ToString();
                        ObjCustGeneral.Mobile_Num = dt.Rows[0]["Mobile_Num"].ToString();
                        ObjCustGeneral.Mobile_Num2 = dt.Rows[0]["Mobile_Num2"].ToString();
                        ObjCustGeneral.First_Tele_No = dt.Rows[0]["First_Tele_No"].ToString();
                        ObjCustGeneral.Second_Tele_No = dt.Rows[0]["Second_Tele_No"].ToString();
                        ObjCustGeneral.Fax_NO = dt.Rows[0]["Fax_NO"].ToString();
                        ObjCustGeneral.Email_Address = dt.Rows[0]["Email_Address"].ToString();
                        ObjCustGeneral.Email_Address2 = dt.Rows[0]["Email_Address2"].ToString();
                        ObjCustGeneral.Email_Address3 = dt.Rows[0]["Email_Address3"].ToString();
                        ObjCustGeneral.Transportation_Zone = dt.Rows[0]["Transportation_Zone"].ToString();
                        ObjCustGeneral.Tax_Jurisdiction = dt.Rows[0]["Tax_Jurisdiction"].ToString();
                        ObjCustGeneral.Acc_No_Vendor = dt.Rows[0]["Acc_No_Vendor"].ToString();

                        ObjCustGeneral.Com_Id_TradingPat = dt.Rows[0]["Com_Id_TradingPat"].ToString();
                        ObjCustGeneral.Group_Key = dt.Rows[0]["Group_Key"].ToString();
                        ObjCustGeneral.LiableVat = dt.Rows[0]["LiableVat"].ToString();
                        ObjCustGeneral.Country_Code = dt.Rows[0]["Country_Code"].ToString();
                        ObjCustGeneral.City_Code = dt.Rows[0]["City_Code"].ToString();
                        //Start Addition By Swati M Date: 12.10.2018
                        ObjCustGeneral.Remarks = dt.Rows[0]["Remarks"].ToString();
                        //End Addition By Swati M Date: 12.10.2018
                    }
                }
                return ObjCustGeneral;
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

        public int Save(CustomerGeneral2 ObjCustGeneral)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Cust_General2";
            int result = 0;


            hashPara.Add("@Cust_General2_Id", ObjCustGeneral.Cust_General2_Id);
            hashPara.Add("@Master_Header_Id", ObjCustGeneral.Master_Header_Id);

            hashPara.Add("@Industry_Key", ObjCustGeneral.Industry_Key);
            hashPara.Add("@Tax_type", ObjCustGeneral.Tax_type);
            hashPara.Add("@Tax_Number_Type", ObjCustGeneral.Tax_Number_Type);
            hashPara.Add("@Tax_Number1", ObjCustGeneral.Tax_Number1);
            hashPara.Add("@Tax_Number2", ObjCustGeneral.Tax_Number2);
            hashPara.Add("@Tax_Number3", ObjCustGeneral.Tax_Number3);
            hashPara.Add("@Tax_Number4", ObjCustGeneral.Tax_Number4);
            hashPara.Add("@Tax_Number5", ObjCustGeneral.Tax_Number5);
            hashPara.Add("@Type_of_Business", ObjCustGeneral.Type_of_Business);            
            hashPara.Add("@VAT_Reg", ObjCustGeneral.VAT_Reg);
            hashPara.Add("@ECC_Number", ObjCustGeneral.ECC_Number);
            hashPara.Add("@Excise_Registration_No", ObjCustGeneral.Excise_Registration_No);
            hashPara.Add("@Excise_Range", ObjCustGeneral.Excise_Range);
            hashPara.Add("@Excise_Division", ObjCustGeneral.Excise_Division);
            hashPara.Add("@Excise_Commissionerate", ObjCustGeneral.Excise_Commissionerate);
            hashPara.Add("@Customer_Claasifi", ObjCustGeneral.Customer_Claasifi);
            hashPara.Add("@Nielsen_Id", ObjCustGeneral.Nielsen_Id);
            hashPara.Add("@Region_Market", ObjCustGeneral.Region_Market);
            hashPara.Add("@AccNo_Payer", ObjCustGeneral.AccNo_Payer);
            hashPara.Add("@Payer_Allow_Doc", ObjCustGeneral.Payer_Allow_Doc);
            hashPara.Add("@Central_Delete_Flag", ObjCustGeneral.Central_Delete_Flag);
            hashPara.Add("@Central_Posting_Blk", ObjCustGeneral.Central_Posting_Blk);
            hashPara.Add("@Central_Order", ObjCustGeneral.Central_Order);
            hashPara.Add("@Central_Delivery", ObjCustGeneral.Central_Delivery);
            hashPara.Add("@Central_Billing", ObjCustGeneral.Central_Billing);
            hashPara.Add("@Legal_sts", ObjCustGeneral.Legal_sts);
            hashPara.Add("@Ind_Code1", ObjCustGeneral.Ind_Code1);
            hashPara.Add("@Ind_Code2", ObjCustGeneral.Ind_Code2);
            hashPara.Add("@Ind_Code3", ObjCustGeneral.Ind_Code3);
            hashPara.Add("@Ind_Code4", ObjCustGeneral.Ind_Code4);
            hashPara.Add("@Ind_Code5", ObjCustGeneral.Ind_Code5);
            hashPara.Add("@Attribute1", ObjCustGeneral.Attribute1);
            hashPara.Add("@Attribute2", ObjCustGeneral.Attribute2);
            hashPara.Add("@Attribute3", ObjCustGeneral.Attribute3);
            hashPara.Add("@Attribute4", ObjCustGeneral.Attribute4);
            hashPara.Add("@Attribute5", ObjCustGeneral.Attribute5);
            hashPara.Add("@Attribute6", ObjCustGeneral.Attribute6);
            hashPara.Add("@Attribute7", ObjCustGeneral.Attribute7);
            hashPara.Add("@Attribute8", ObjCustGeneral.Attribute8);
            hashPara.Add("@Attribute9", ObjCustGeneral.Attribute9);
            hashPara.Add("@Attribute10", ObjCustGeneral.Attribute10);

            //GST Changes
            hashPara.Add("@GSTNo", ObjCustGeneral.GSTNo);
            //GST Changes
            //CUST_8300001962 Start
            hashPara.Add("@RegisterPAN", ObjCustGeneral.RegisterPAN);
            hashPara.Add("@RegisterUnderGST", ObjCustGeneral.RegisterUnderGST);
            hashPara.Add("@PanReason", ObjCustGeneral.PanReason);
            //CUST_8300001962 End
            hashPara.Add("@IsActive", ObjCustGeneral.IsActive);
            hashPara.Add("@UserId", ObjCustGeneral.UserId);
            hashPara.Add("@UserIp", ObjCustGeneral.IPAddress);

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

        public CustomerGeneral2 GetCustomerGeneral2(int intMasterHeaderId)
        {
            CustomerGeneral2 ObjCustGeneral = new CustomerGeneral2();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_General2_By_MasterHeaderId";
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
                        ObjCustGeneral.Cust_General2_Id = Convert.ToInt32(dt.Rows[0]["Cust_General2_Id"].ToString());
                        ObjCustGeneral.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjCustGeneral.Industry_Key = dt.Rows[0]["Industry_Key"].ToString();
                        ObjCustGeneral.Tax_type = dt.Rows[0]["Tax_type"].ToString();
                        ObjCustGeneral.Tax_Number_Type = dt.Rows[0]["Tax_Number_Type"].ToString();
                        ObjCustGeneral.Tax_Number1 = dt.Rows[0]["Tax_Number1"].ToString();
                        ObjCustGeneral.Tax_Number2 = dt.Rows[0]["Tax_Number2"].ToString();
                        ObjCustGeneral.Tax_Number3 = dt.Rows[0]["Tax_Number3"].ToString();
                        ObjCustGeneral.Tax_Number4 = dt.Rows[0]["Tax_Number4"].ToString();
                        ObjCustGeneral.Tax_Number5 = dt.Rows[0]["Tax_Number5"].ToString();
                        ObjCustGeneral.Type_of_Business = dt.Rows[0]["Type_of_Business"].ToString();                        
                        ObjCustGeneral.VAT_Reg = dt.Rows[0]["VAT_Reg"].ToString();
                        ObjCustGeneral.ECC_Number = dt.Rows[0]["ECC_Number"].ToString();
                        ObjCustGeneral.Excise_Registration_No = dt.Rows[0]["Excise_Registration_No"].ToString();
                        ObjCustGeneral.Excise_Range = dt.Rows[0]["Excise_Range"].ToString();
                        ObjCustGeneral.Excise_Division = dt.Rows[0]["Excise_Division"].ToString();
                        ObjCustGeneral.Excise_Commissionerate = dt.Rows[0]["Excise_Commissionerate"].ToString();
                        ObjCustGeneral.Customer_Claasifi = dt.Rows[0]["Customer_Claasifi"].ToString();
                        ObjCustGeneral.Nielsen_Id = dt.Rows[0]["Nielsen_Id"].ToString();
                        ObjCustGeneral.Region_Market = dt.Rows[0]["Region_Market"].ToString();
                        ObjCustGeneral.AccNo_Payer = dt.Rows[0]["AccNo_Payer"].ToString();
                        ObjCustGeneral.Payer_Allow_Doc = dt.Rows[0]["Payer_Allow_Doc"].ToString().ToLower() == "true" ? 1 : 0;
                        ObjCustGeneral.Central_Delete_Flag = dt.Rows[0]["Central_Delete_Flag"].ToString().ToLower() == "true" ? 1 : 0;
                        ObjCustGeneral.Central_Posting_Blk = dt.Rows[0]["Central_Posting_Blk"].ToString().ToLower() == "true" ? 1 : 0;
                        ObjCustGeneral.Central_Order = dt.Rows[0]["Central_Order"].ToString();
                        ObjCustGeneral.Central_Delivery = dt.Rows[0]["Central_Delivery"].ToString();
                        ObjCustGeneral.Central_Billing = dt.Rows[0]["Central_Billing"].ToString();
                        ObjCustGeneral.Legal_sts = dt.Rows[0]["Legal_sts"].ToString();
                        ObjCustGeneral.Ind_Code1 = dt.Rows[0]["Ind_Code1"].ToString();
                        ObjCustGeneral.Ind_Code2 = dt.Rows[0]["Ind_Code2"].ToString();
                        ObjCustGeneral.Ind_Code3 = dt.Rows[0]["Ind_Code3"].ToString();
                        ObjCustGeneral.Ind_Code4 = dt.Rows[0]["Ind_Code4"].ToString();
                        ObjCustGeneral.Ind_Code5 = dt.Rows[0]["Ind_Code5"].ToString();
                        ObjCustGeneral.Attribute1 = dt.Rows[0]["Attribute1"].ToString();
                        ObjCustGeneral.Attribute2 = dt.Rows[0]["Attribute2"].ToString();
                        ObjCustGeneral.Attribute3 = dt.Rows[0]["Attribute3"].ToString();
                        ObjCustGeneral.Attribute4 = dt.Rows[0]["Attribute4"].ToString();
                        ObjCustGeneral.Attribute5 = dt.Rows[0]["Attribute5"].ToString();
                        ObjCustGeneral.Attribute6 = dt.Rows[0]["Attribute6"].ToString();
                        ObjCustGeneral.Attribute7 = dt.Rows[0]["Attribute7"].ToString();
                        ObjCustGeneral.Attribute8 = dt.Rows[0]["Attribute8"].ToString();
                        ObjCustGeneral.Attribute9 = dt.Rows[0]["Attribute9"].ToString();
                        ObjCustGeneral.Attribute10 = dt.Rows[0]["Attribute10"].ToString();

                        //GST Changes
                        ObjCustGeneral.GSTNo = dt.Rows[0]["GSTNo"].ToString();
                        //GST Changes

                        //CUST_8300001962 Start
                        ObjCustGeneral.RegisterPAN = dt.Rows[0]["RegisterPAN"].ToString();
                        ObjCustGeneral.RegisterUnderGST = dt.Rows[0]["RegisterUnderGST"].ToString();
                        ObjCustGeneral.PanReason = dt.Rows[0]["PanReason"].ToString();
                        //CUST_8300001962 End

                    }
                }
                return ObjCustGeneral;
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

        public int Save(CustomerGeneral3 ObjCustGeneral)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Cust_General3";
            int result = 0;


            hashPara.Add("@Cust_General3_Id", ObjCustGeneral.Cust_General3_Id);
            hashPara.Add("@Master_Header_Id", ObjCustGeneral.Master_Header_Id);

            hashPara.Add("@Fiscal_Year_Variant", ObjCustGeneral.Fiscal_Year_Variant);
            hashPara.Add("@Reference_Account", ObjCustGeneral.Reference_Account);
            hashPara.Add("@PO_Box_city", ObjCustGeneral.PO_Box_city);
            hashPara.Add("@Hierarchy_assignment", ObjCustGeneral.Hierarchy_assignment);
            hashPara.Add("@Central_sales", ObjCustGeneral.Central_sales);
            hashPara.Add("@Customer_condition1", ObjCustGeneral.Customer_condition1);
            hashPara.Add("@Customer_condition2", ObjCustGeneral.Customer_condition2);
            hashPara.Add("@Customer_condition3", ObjCustGeneral.Customer_condition3);
            hashPara.Add("@Customer_condition4", ObjCustGeneral.Customer_condition4);
            hashPara.Add("@Customer_condition5", ObjCustGeneral.Customer_condition5);
            hashPara.Add("@Uniform_Resource", ObjCustGeneral.Uniform_Resource);
            hashPara.Add("@Central_deletion", ObjCustGeneral.Central_deletion);
            hashPara.Add("@Unloading_Point", ObjCustGeneral.Unloading_Point);
            hashPara.Add("@Customer_factory", ObjCustGeneral.Customer_factory);
            hashPara.Add("@Contact_person_department", ObjCustGeneral.Contact_person_department);
            hashPara.Add("@First_name", ObjCustGeneral.First_name);
            hashPara.Add("@Country_Key", ObjCustGeneral.Country_Key);
            hashPara.Add("@Mobile_Num", ObjCustGeneral.Mobile_Num);
            hashPara.Add("@Mobile_Num2", ObjCustGeneral.Mobile_Num2);
            hashPara.Add("@First_Tele_No", ObjCustGeneral.First_Tele_No);
            hashPara.Add("@Second_Tele_No", ObjCustGeneral.Second_Tele_No);
            hashPara.Add("@Fax_NO", ObjCustGeneral.Fax_NO);
            hashPara.Add("@Email_Address", ObjCustGeneral.Email_Address);
            hashPara.Add("@Email_Address2", ObjCustGeneral.Email_Address2);
            hashPara.Add("@First_name_2", ObjCustGeneral.First_name_2);
            hashPara.Add("@Country_Key_2", ObjCustGeneral.Country_Key_2);
            hashPara.Add("@Mobile_Num_2", ObjCustGeneral.Mobile_Num_2);
            hashPara.Add("@Mobile_Num2_2", ObjCustGeneral.Mobile_Num2_2);
            hashPara.Add("@First_Tele_No_2", ObjCustGeneral.First_Tele_No_2);
            hashPara.Add("@Second_Tele_No_2", ObjCustGeneral.Second_Tele_No_2);
            hashPara.Add("@Fax_NO_2", ObjCustGeneral.Fax_NO_2);
            hashPara.Add("@Email_Address_2", ObjCustGeneral.Email_Address_2);
            hashPara.Add("@Email_Address2_2", ObjCustGeneral.Email_Address2_2);
            hashPara.Add("@First_name_3", ObjCustGeneral.First_name_3);
            hashPara.Add("@Country_Key_3", ObjCustGeneral.Country_Key_3);
            hashPara.Add("@Mobile_Num_3", ObjCustGeneral.Mobile_Num_3);
            hashPara.Add("@Mobile_Num2_3", ObjCustGeneral.Mobile_Num2_3);
            hashPara.Add("@First_Tele_No_3", ObjCustGeneral.First_Tele_No_3);
            hashPara.Add("@Second_Tele_No_3", ObjCustGeneral.Second_Tele_No_3);
            hashPara.Add("@Fax_NO_3", ObjCustGeneral.Fax_NO_3);
            hashPara.Add("@Email_Address_3", ObjCustGeneral.Email_Address_3);
            hashPara.Add("@Email_Address2_3", ObjCustGeneral.Email_Address2_3);
            hashPara.Add("@Form_address", ObjCustGeneral.Form_address);
            hashPara.Add("@Contact_person_function", ObjCustGeneral.Contact_person_function);
            hashPara.Add("@Partner_language", ObjCustGeneral.Partner_language);
            hashPara.Add("@Partner_gender", ObjCustGeneral.Partner_gender);
            hashPara.Add("@Marital_Status", ObjCustGeneral.Marital_Status);
            hashPara.Add("@Date_Batch", ObjCustGeneral.Date_Batch);
            hashPara.Add("@Contact_person_department_Cust", ObjCustGeneral.Contact_person_department_Cust);
            hashPara.Add("@VIP_Partner", ObjCustGeneral.VIP_Partner);
            hashPara.Add("@Partner_Authority", ObjCustGeneral.Partner_Authority);
            hashPara.Add("@Notes", ObjCustGeneral.Notes);

            hashPara.Add("@IsActive", ObjCustGeneral.IsActive);
            hashPara.Add("@UserId", ObjCustGeneral.UserId);
            hashPara.Add("@UserIp", ObjCustGeneral.IPAddress);

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

        public CustomerGeneral3 GetCustomerGeneral3(int intMasterHeaderId)
        {
            CustomerGeneral3 ObjCustGeneral = new CustomerGeneral3();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Cust_General3_By_MasterHeaderId";
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
                        ObjCustGeneral.Cust_General3_Id = Convert.ToInt32(dt.Rows[0]["Cust_General3_Id"].ToString());
                        ObjCustGeneral.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjCustGeneral.Fiscal_Year_Variant = dt.Rows[0]["Fiscal_Year_Variant"].ToString();
                        ObjCustGeneral.Reference_Account = dt.Rows[0]["Reference_Account"].ToString();
                        ObjCustGeneral.PO_Box_city = dt.Rows[0]["PO_Box_city"].ToString();
                        ObjCustGeneral.Hierarchy_assignment = dt.Rows[0]["Hierarchy_assignment"].ToString();
                        ObjCustGeneral.Central_sales = dt.Rows[0]["Central_sales"].ToString();
                        ObjCustGeneral.Customer_condition1 = dt.Rows[0]["Customer_condition1"].ToString();
                        ObjCustGeneral.Customer_condition2 = dt.Rows[0]["Customer_condition2"].ToString();
                        ObjCustGeneral.Customer_condition3 = dt.Rows[0]["Customer_condition3"].ToString();
                        ObjCustGeneral.Customer_condition4 = dt.Rows[0]["Customer_condition4"].ToString();
                        ObjCustGeneral.Customer_condition5 = dt.Rows[0]["Customer_condition5"].ToString();
                        ObjCustGeneral.Uniform_Resource = dt.Rows[0]["Uniform_Resource"].ToString();
                        ObjCustGeneral.Central_deletion = dt.Rows[0]["Central_deletion"].ToString();
                        ObjCustGeneral.Unloading_Point = dt.Rows[0]["Unloading_Point"].ToString();
                        ObjCustGeneral.Customer_factory = dt.Rows[0]["Customer_factory"].ToString();
                        ObjCustGeneral.Contact_person_department = dt.Rows[0]["Contact_person_department"].ToString();
                        ObjCustGeneral.First_name = dt.Rows[0]["First_name"].ToString();
                        ObjCustGeneral.Country_Key = dt.Rows[0]["Country_Key"].ToString();
                        ObjCustGeneral.Mobile_Num = dt.Rows[0]["Mobile_Num"].ToString();
                        ObjCustGeneral.Mobile_Num2 = dt.Rows[0]["Mobile_Num2"].ToString();
                        ObjCustGeneral.First_Tele_No = dt.Rows[0]["First_Tele_No"].ToString();
                        ObjCustGeneral.Second_Tele_No = dt.Rows[0]["Second_Tele_No"].ToString();
                        ObjCustGeneral.Fax_NO = dt.Rows[0]["Fax_NO"].ToString();
                        ObjCustGeneral.Email_Address = dt.Rows[0]["Email_Address"].ToString();
                        ObjCustGeneral.Email_Address2 = dt.Rows[0]["Email_Address2"].ToString();
                        ObjCustGeneral.First_name_2 = dt.Rows[0]["First_name_2"].ToString();
                        ObjCustGeneral.Country_Key_2 = dt.Rows[0]["Country_Key_2"].ToString();
                        ObjCustGeneral.Mobile_Num_2 = dt.Rows[0]["Mobile_Num_2"].ToString();
                        ObjCustGeneral.Mobile_Num2_2 = dt.Rows[0]["Mobile_Num2_2"].ToString();
                        ObjCustGeneral.First_Tele_No_2 = dt.Rows[0]["First_Tele_No_2"].ToString();
                        ObjCustGeneral.Second_Tele_No_2 = dt.Rows[0]["Second_Tele_No_2"].ToString();
                        ObjCustGeneral.Fax_NO_2 = dt.Rows[0]["Fax_NO_2"].ToString();
                        ObjCustGeneral.Email_Address_2 = dt.Rows[0]["Email_Address_2"].ToString();
                        ObjCustGeneral.Email_Address2_2 = dt.Rows[0]["Email_Address2_2"].ToString();

                        ObjCustGeneral.First_name_3 = dt.Rows[0]["First_name_3"].ToString();
                        ObjCustGeneral.Country_Key_3 = dt.Rows[0]["Country_Key_3"].ToString();
                        ObjCustGeneral.Mobile_Num_3 = dt.Rows[0]["Mobile_Num_3"].ToString();
                        ObjCustGeneral.Mobile_Num2_3 = dt.Rows[0]["Mobile_Num2_3"].ToString();
                        ObjCustGeneral.First_Tele_No_3 = dt.Rows[0]["First_Tele_No_3"].ToString();
                        ObjCustGeneral.Second_Tele_No_3 = dt.Rows[0]["Second_Tele_No_3"].ToString();
                        ObjCustGeneral.Fax_NO_3 = dt.Rows[0]["Fax_NO_3"].ToString();
                        ObjCustGeneral.Email_Address_3 = dt.Rows[0]["Email_Address_3"].ToString();
                        ObjCustGeneral.Email_Address2_3 = dt.Rows[0]["Email_Address2_3"].ToString();

                        ObjCustGeneral.Form_address = dt.Rows[0]["Form_address"].ToString();
                        ObjCustGeneral.Contact_person_function = dt.Rows[0]["Contact_person_function"].ToString();
                        ObjCustGeneral.Partner_language = dt.Rows[0]["Partner_language"].ToString();
                        ObjCustGeneral.Partner_gender = dt.Rows[0]["Partner_gender"].ToString();
                        ObjCustGeneral.Marital_Status = dt.Rows[0]["Marital_Status"].ToString();
                        ObjCustGeneral.Date_Batch = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Date_Batch"].ToString());
                        ObjCustGeneral.Contact_person_department_Cust = dt.Rows[0]["Contact_person_department_Cust"].ToString();
                        ObjCustGeneral.VIP_Partner = dt.Rows[0]["VIP_Partner"].ToString();
                        ObjCustGeneral.Partner_Authority = dt.Rows[0]["Partner_Authority"].ToString();
                        ObjCustGeneral.Notes = dt.Rows[0]["Notes"].ToString();
                    }
                }
                return ObjCustGeneral;
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

        public DataSet GetReportByMasterHeaderId(string Master_Header_id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_rpt_CustomerMaster";
            hashPara.Add("@Master_Header_id", Master_Header_id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }
        //Start Change By Swati : DT 20.12.2018
        public DataSet ModulePlantGroupCode (string Request_No)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_ModulePlantGroupCode";
            hashPara.Add("@Request_No", Request_No);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }
        //End
    }
}