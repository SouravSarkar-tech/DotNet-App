using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;


/// <summary>
/// Summary description for VendorGeneralAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class VendorGeneralAccess
    {
        public VendorGeneralAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(VendorGeneral1 ObjVendorGeneral)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_General1";
            int result = 0;


            hashPara.Add("@Vendor_General1_Id", ObjVendorGeneral.Vendor_General1_Id);
            hashPara.Add("@Master_Header_Id", ObjVendorGeneral.Master_Header_Id);

            hashPara.Add("@Customer_Code", ObjVendorGeneral.Customer_Code);
            hashPara.Add("@Company_Code", ObjVendorGeneral.Company_Code);
            hashPara.Add("@Vendor_Group", ObjVendorGeneral.Vendor_Group);
            hashPara.Add("@Purchase_Org", ObjVendorGeneral.Purchase_Org);
            hashPara.Add("@Vendor_Category", ObjVendorGeneral.Vendor_Category);

            hashPara.Add("@Title", ObjVendorGeneral.Title);
            hashPara.Add("@Memo", ObjVendorGeneral.Memo);
            hashPara.Add("@Name1", ObjVendorGeneral.Name1);
            hashPara.Add("@Name2", ObjVendorGeneral.Name2);
            hashPara.Add("@Name3", ObjVendorGeneral.Name3);
            hashPara.Add("@Name4", ObjVendorGeneral.Name4);
            hashPara.Add("@Sort_Field", ObjVendorGeneral.Sort_Field);
            hashPara.Add("@HouseNo_Street", ObjVendorGeneral.HouseNo_Street);
            hashPara.Add("@Street4", ObjVendorGeneral.Street4);
            hashPara.Add("@Street5", ObjVendorGeneral.Street5);
            hashPara.Add("@PO_Box", ObjVendorGeneral.PO_Box);
            hashPara.Add("@City", ObjVendorGeneral.City);
            hashPara.Add("@Postal_Code", ObjVendorGeneral.Postal_Code);
            hashPara.Add("@District", ObjVendorGeneral.District);
            hashPara.Add("@PO_Box_Postal_Code", ObjVendorGeneral.PO_Box_Postal_Code);
            hashPara.Add("@Mobile_Num", ObjVendorGeneral.Mobile_Num);
            hashPara.Add("@Mobile_Num2", ObjVendorGeneral.Mobile_Num2);
            hashPara.Add("@CountryKey", ObjVendorGeneral.CountryKey);
            hashPara.Add("@Region", ObjVendorGeneral.Region);
            hashPara.Add("@LanguageAcc", ObjVendorGeneral.LanguageAcc);
            hashPara.Add("@First_Tele_No", ObjVendorGeneral.First_Tele_No);
            hashPara.Add("@Fax_NO", ObjVendorGeneral.Fax_NO);
            hashPara.Add("@Second_Tele_No", ObjVendorGeneral.Second_Tele_No);
            hashPara.Add("@Email_Address", ObjVendorGeneral.Email_Address);
            hashPara.Add("@Email_Address2", ObjVendorGeneral.Email_Address2);
            hashPara.Add("@Email_Address3", ObjVendorGeneral.Email_Address3);
            hashPara.Add("@Autorization_Gr", ObjVendorGeneral.Autorization_Gr);
            hashPara.Add("@Com_Id_TradingPat", ObjVendorGeneral.Com_Id_TradingPat);
            hashPara.Add("@Telex_number", ObjVendorGeneral.Telex_number);
            hashPara.Add("@Teletex_number", ObjVendorGeneral.Teletex_number);
            hashPara.Add("@Customer_Number", ObjVendorGeneral.Customer_Number);
            hashPara.Add("@Remarks", ObjVendorGeneral.Remarks);
            //GST Changes Start
            hashPara.Add("@SupplyPlace", ObjVendorGeneral.SupplyPlace);
            hashPara.Add("@ImpVendor", ObjVendorGeneral.ImpVendor);
            //GST Changes End
            hashPara.Add("@IsActive", ObjVendorGeneral.IsActive);
            hashPara.Add("@UserId", ObjVendorGeneral.UserId);
            hashPara.Add("@UserIp", ObjVendorGeneral.IPAddress);

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

        public VendorGeneral1 GetVendorGeneral1(int intMasterHeaderId)
        {
            VendorGeneral1 ObjVendorGeneral = new VendorGeneral1();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Vendor_General1_By_MasterHeaderId";
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
                        ObjVendorGeneral.Vendor_General1_Id = Convert.ToInt32(dt.Rows[0]["Vendor_General1_Id"].ToString());
                        ObjVendorGeneral.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjVendorGeneral.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjVendorGeneral.Customer_Code = dt.Rows[0]["Customer_Code"].ToString();
                        ObjVendorGeneral.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjVendorGeneral.Vendor_Group = dt.Rows[0]["Vendor_Group"].ToString();
                        ObjVendorGeneral.Purchase_Org = dt.Rows[0]["Purchase_Org"].ToString();
                        ObjVendorGeneral.Vendor_Desc = dt.Rows[0]["Vendor_Desc"].ToString();
                        ObjVendorGeneral.Vendor_Category = dt.Rows[0]["Vendor_Category"].ToString();
                        ObjVendorGeneral.Title = dt.Rows[0]["Title"].ToString();
                        ObjVendorGeneral.Memo = dt.Rows[0]["Memo"].ToString();
                        ObjVendorGeneral.Name1 = dt.Rows[0]["Name1"].ToString();
                        ObjVendorGeneral.Name2 = dt.Rows[0]["Name2"].ToString();
                        ObjVendorGeneral.Name3 = dt.Rows[0]["Name3"].ToString();
                        ObjVendorGeneral.Name4 = dt.Rows[0]["Name4"].ToString();
                        ObjVendorGeneral.Sort_Field = dt.Rows[0]["Sort_Field"].ToString();
                        ObjVendorGeneral.HouseNo_Street = dt.Rows[0]["HouseNo_Street"].ToString();
                        ObjVendorGeneral.Street4 = dt.Rows[0]["Street4"].ToString();
                        ObjVendorGeneral.Street5 = dt.Rows[0]["Street5"].ToString();
                        ObjVendorGeneral.PO_Box = dt.Rows[0]["PO_Box"].ToString();
                        ObjVendorGeneral.City = dt.Rows[0]["City"].ToString();
                        ObjVendorGeneral.Postal_Code = dt.Rows[0]["Postal_Code"].ToString();
                        ObjVendorGeneral.District = dt.Rows[0]["District"].ToString();
                        ObjVendorGeneral.PO_Box_Postal_Code = dt.Rows[0]["PO_Box_Postal_Code"].ToString();
                        ObjVendorGeneral.CountryKey = dt.Rows[0]["CountryKey"].ToString();
                        ObjVendorGeneral.Region = dt.Rows[0]["Region"].ToString();
                        ObjVendorGeneral.LanguageAcc = dt.Rows[0]["LanguageAcc"].ToString();
                        ObjVendorGeneral.Mobile_Num = dt.Rows[0]["Mobile_Num"].ToString();
                        ObjVendorGeneral.Mobile_Num2 = dt.Rows[0]["Mobile_Num2"].ToString();
                        ObjVendorGeneral.First_Tele_No = dt.Rows[0]["First_Tele_No"].ToString();
                        ObjVendorGeneral.Fax_NO = dt.Rows[0]["Fax_NO"].ToString();
                        ObjVendorGeneral.Second_Tele_No = dt.Rows[0]["Second_Tele_No"].ToString();
                        ObjVendorGeneral.Email_Address = dt.Rows[0]["Email_Address"].ToString();
                        ObjVendorGeneral.Email_Address2 = dt.Rows[0]["Email_Address2"].ToString();
                        ObjVendorGeneral.Email_Address3 = dt.Rows[0]["Email_Address3"].ToString();
                        ObjVendorGeneral.Autorization_Gr = dt.Rows[0]["Autorization_Gr"].ToString();
                        ObjVendorGeneral.Com_Id_TradingPat = dt.Rows[0]["Com_Id_TradingPat"].ToString();
                        ObjVendorGeneral.Telex_number = dt.Rows[0]["Telex_number"].ToString();
                        ObjVendorGeneral.Teletex_number = dt.Rows[0]["Teletex_number"].ToString();
                        ObjVendorGeneral.Customer_Number = dt.Rows[0]["Customer_Number"].ToString();
                        ObjVendorGeneral.Remarks = dt.Rows[0]["Remarks"].ToString();
                        //GST Changes Start
                        ObjVendorGeneral.SupplyPlace = dt.Rows[0]["SupplyPlace"].ToString();
                        ObjVendorGeneral.ImpVendor = dt.Rows[0]["ImpVendor"].ToString();
                        //GST Changes End
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



        public int Save2(VendorGeneral2 ObjVendorGeneral)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Vendor_General2";
            int result = 0;


            hashPara.Add("@Vendor_General2_Id", ObjVendorGeneral.Vendor_General2_Id);
            hashPara.Add("@Master_Header_Id", ObjVendorGeneral.Master_Header_Id);
            hashPara.Add("@Industry_key", ObjVendorGeneral.Industry_key);
            
            hashPara.Add("@VAT_Registration_Number", ObjVendorGeneral.VAT_Registration_Number);
            hashPara.Add("@PlaceBirth_WithholdingTax", ObjVendorGeneral.PlaceBirth_WithholdingTax);
            hashPara.Add("@DateBatch_Input", ObjVendorGeneral.DateBatch_Input);
            hashPara.Add("@KeySex_PersonWithholding_Tax", ObjVendorGeneral.KeySex_PersonWithholding_Tax);
            hashPara.Add("@Tax_Jurisdiction", ObjVendorGeneral.Tax_Jurisdiction);
            hashPara.Add("@Plant", ObjVendorGeneral.Plant);
            hashPara.Add("@Transportation_Zone_Goods", ObjVendorGeneral.Transportation_Zone_Goods);
            hashPara.Add("@Service_AgentProcedure_Group", ObjVendorGeneral.Service_AgentProcedure_Group);
            hashPara.Add("@Tax_Type", ObjVendorGeneral.Tax_Type);
            hashPara.Add("@Tax_Number_Type", ObjVendorGeneral.Tax_Number_Type);
            hashPara.Add("@Tax_Number1", ObjVendorGeneral.Tax_Number1);
            hashPara.Add("@Tax_Number2", ObjVendorGeneral.Tax_Number2);
            hashPara.Add("@Tax_Numbe_3", ObjVendorGeneral.Tax_Numbe_3);
            hashPara.Add("@Tax_Numbe_4", ObjVendorGeneral.Tax_Numbe_4);
            hashPara.Add("@Type_of_Business", ObjVendorGeneral.Type_of_Business);
            hashPara.Add("@Tax_Split", ObjVendorGeneral.Tax_Split);
            hashPara.Add("@External_Manufacturer_CodeNumber", ObjVendorGeneral.External_Manufacturer_CodeNumber);
            hashPara.Add("@Name_Representative", ObjVendorGeneral.Name_Representative);
            hashPara.Add("@Type_Industry", ObjVendorGeneral.Type_Industry);
            hashPara.Add("@Central_Deletion_MasterRecord", ObjVendorGeneral.Central_Deletion_MasterRecord);
            hashPara.Add("@DateBatch_Input2", ObjVendorGeneral.DateBatch_Input2);
            hashPara.Add("@VendorIndicator_Relevant", ObjVendorGeneral.VendorIndicator_Relevant);
            hashPara.Add("@Name_1", ObjVendorGeneral.Name_1);
            hashPara.Add("@Name_2", ObjVendorGeneral.Name_2);
            hashPara.Add("@Name_3", ObjVendorGeneral.Name_3);
            hashPara.Add("@First_Name", ObjVendorGeneral.First_Name);
            hashPara.Add("@Title", ObjVendorGeneral.Title);
            hashPara.Add("@FactoryCalendar_key", ObjVendorGeneral.FactoryCalendar_key);
            hashPara.Add("@Transportation_Chain", ObjVendorGeneral.Transportation_Chain);
            hashPara.Add("@StagingTime_Days_BatchInput", ObjVendorGeneral.StagingTime_Days_BatchInput);
            hashPara.Add("@CrossDocking_Relevant_CollectiveNumbering", ObjVendorGeneral.CrossDocking_Relevant_CollectiveNumbering);
            hashPara.Add("@Scheduling_Procedure", ObjVendorGeneral.Scheduling_Procedure);
            hashPara.Add("@Tax_Number_5", ObjVendorGeneral.Tax_Number_5);
            hashPara.Add("@ECC_Number", ObjVendorGeneral.ECC_Number);
            hashPara.Add("@Excise_Registration_No", ObjVendorGeneral.Excise_Registration_No);
            hashPara.Add("@Excise_Range", ObjVendorGeneral.Excise_Range);
            hashPara.Add("@Excise_Division", ObjVendorGeneral.Excise_Division);
            hashPara.Add("@Excise_Commissionerate", ObjVendorGeneral.Excise_Commissionerate);
            //GST Changes
            hashPara.Add("@GSTNo", ObjVendorGeneral.GSTNo);
            hashPara.Add("@VendorClass", ObjVendorGeneral.VendorClass);
            //GST Changes
            hashPara.Add("@IsActive", ObjVendorGeneral.IsActive);
            hashPara.Add("@UserId", ObjVendorGeneral.UserId);
            hashPara.Add("@UserIp", ObjVendorGeneral.IPAddress);

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

        public VendorGeneral2 GetVendorGeneral2(int intMasterHeaderId)
        {
            VendorGeneral2 ObjVendorGeneral = new VendorGeneral2();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Vendor_General2_By_MasterHeaderId";
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
                        ObjVendorGeneral.Vendor_General2_Id = Convert.ToInt32(dt.Rows[0]["Vendor_General2_Id"].ToString());
                        ObjVendorGeneral.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjVendorGeneral.Industry_key = dt.Rows[0]["Industry_key"].ToString();
                        
                        ObjVendorGeneral.VAT_Registration_Number = dt.Rows[0]["VAT_Registration_Number"].ToString();
                        ObjVendorGeneral.PlaceBirth_WithholdingTax = dt.Rows[0]["PlaceBirth_WithholdingTax"].ToString();
                        ObjVendorGeneral.DateBatch_Input = dt.Rows[0]["DateBatch_Input"].ToString();
                        ObjVendorGeneral.KeySex_PersonWithholding_Tax = dt.Rows[0]["KeySex_PersonWithholding_Tax"].ToString();
                        ObjVendorGeneral.Tax_Jurisdiction = dt.Rows[0]["Tax_Jurisdiction"].ToString();
                        ObjVendorGeneral.Plant = dt.Rows[0]["Plant"].ToString();
                        ObjVendorGeneral.Transportation_Zone_Goods = dt.Rows[0]["Transportation_Zone_Goods"].ToString();
                        ObjVendorGeneral.Service_AgentProcedure_Group = dt.Rows[0]["Service_AgentProcedure_Group"].ToString();
                        ObjVendorGeneral.Tax_Type = dt.Rows[0]["Tax_Type"].ToString();
                        ObjVendorGeneral.Tax_Number_Type = dt.Rows[0]["Tax_Number_Type"].ToString();
                        ObjVendorGeneral.Tax_Number1 = dt.Rows[0]["Tax_Number1"].ToString();
                        ObjVendorGeneral.Tax_Number2 = dt.Rows[0]["Tax_Number2"].ToString();
                        ObjVendorGeneral.Tax_Numbe_3 = dt.Rows[0]["Tax_Numbe_3"].ToString();
                        ObjVendorGeneral.Tax_Numbe_4 = dt.Rows[0]["Tax_Numbe_4"].ToString();
                        ObjVendorGeneral.Type_of_Business = dt.Rows[0]["Type_of_Business"].ToString();
                        ObjVendorGeneral.Tax_Split = dt.Rows[0]["Tax_Split"].ToString();
                        ObjVendorGeneral.External_Manufacturer_CodeNumber = dt.Rows[0]["External_Manufacturer_CodeNumber"].ToString();
                        ObjVendorGeneral.Name_Representative = dt.Rows[0]["Name_Representative"].ToString();
                        ObjVendorGeneral.Type_Industry = dt.Rows[0]["Type_Industry"].ToString();
                        ObjVendorGeneral.Central_Deletion_MasterRecord = dt.Rows[0]["Central_Deletion_MasterRecord"].ToString();
                        ObjVendorGeneral.DateBatch_Input2 = dt.Rows[0]["DateBatch_Input2"].ToString();
                        ObjVendorGeneral.VendorIndicator_Relevant = dt.Rows[0]["VendorIndicator_Relevant"].ToString();
                        ObjVendorGeneral.Name_1 = dt.Rows[0]["Name_1"].ToString();
                        ObjVendorGeneral.Name_2 = dt.Rows[0]["Name_2"].ToString();
                        ObjVendorGeneral.Name_3 = dt.Rows[0]["Name_3"].ToString();
                        ObjVendorGeneral.First_Name = dt.Rows[0]["First_Name"].ToString();
                        ObjVendorGeneral.Title = dt.Rows[0]["Title"].ToString();
                        ObjVendorGeneral.FactoryCalendar_key = dt.Rows[0]["FactoryCalendar_key"].ToString();
                        ObjVendorGeneral.Transportation_Chain = dt.Rows[0]["Transportation_Chain"].ToString();
                        ObjVendorGeneral.StagingTime_Days_BatchInput = dt.Rows[0]["StagingTime_Days_BatchInput"].ToString();
                        ObjVendorGeneral.CrossDocking_Relevant_CollectiveNumbering = dt.Rows[0]["CrossDocking_Relevant_CollectiveNumbering"].ToString();
                        ObjVendorGeneral.Scheduling_Procedure = dt.Rows[0]["Scheduling_Procedure"].ToString();
                        ObjVendorGeneral.Tax_Number_5 = dt.Rows[0]["Tax_Number_5"].ToString();
                        ObjVendorGeneral.ECC_Number = dt.Rows[0]["ECC_Number"].ToString();
                        ObjVendorGeneral.Excise_Registration_No = dt.Rows[0]["Excise_Registration_No"].ToString();
                        ObjVendorGeneral.Excise_Range = dt.Rows[0]["Excise_Range"].ToString();
                        ObjVendorGeneral.Excise_Division = dt.Rows[0]["Excise_Division"].ToString();
                        ObjVendorGeneral.Excise_Commissionerate = dt.Rows[0]["Excise_Commissionerate"].ToString();
                        //GST Changes
                        ObjVendorGeneral.GSTNo = dt.Rows[0]["GSTNo"].ToString();
                        ObjVendorGeneral.VendorClass = dt.Rows[0]["VendorClass"].ToString();
                        //GST Changes
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

        public DataSet GetVendorDuplicate(int MasterHeaderId, string PanNo, string Region_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDuplicateVendors";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);
            hashPara.Add("@Pan_No", PanNo);
            hashPara.Add("@Region_Id", Region_Id);

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