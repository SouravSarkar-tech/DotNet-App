using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CostingAccess
/// </summary>

namespace Accenture.MWT.DataAccess
{
    public class ClassificationAccess
    {
        public ClassificationAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Classification

        public int Save(Classification ObjClassification)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Classification";
            int result = 0;


            hashPara.Add("@Mat_Classification_Id", ObjClassification.Mat_Classification_Id);
            hashPara.Add("@Master_Header_Id", ObjClassification.Master_Header_Id);

            hashPara.Add("@Class_Type", ObjClassification.Class_Type);
            hashPara.Add("@Class", ObjClassification.Class);

            hashPara.Add("@Strength_of_mat_Pack_type", ObjClassification.Strength_of_mat_Pack_type);
            hashPara.Add("@Market", ObjClassification.Market);
            hashPara.Add("@NDC_No_LPI", ObjClassification.NDC_No_LPI);
            hashPara.Add("@NDC_No_LL", ObjClassification.NDC_No_LL);
            hashPara.Add("@HTS", ObjClassification.HTS);
            hashPara.Add("@ANDA", ObjClassification.ANDA);
            hashPara.Add("@FDA_No", ObjClassification.FDA_No);
            hashPara.Add("@LPI_Material_Identifier", ObjClassification.LPI_Material_Identifier);
            hashPara.Add("@Material_Grouping_for_MES", ObjClassification.Material_Grouping_for_MES);
            hashPara.Add("@Short_description_for_3PL", ObjClassification.Short_description_for_3PL);
            hashPara.Add("@Package_Presentation_3PL", ObjClassification.Package_Presentation_3PL);
            hashPara.Add("@Number_of_Tablet_3PL", ObjClassification.Number_of_Tablet_3PL);
            hashPara.Add("@Material_Category_A_3PL", ObjClassification.Material_Category_A_3PL);
            hashPara.Add("@Material_Category_B_3PL", ObjClassification.Material_Category_B_3PL);
            hashPara.Add("@Sorting_for_inventory_report", ObjClassification.Sorting_for_inventory_report);
            hashPara.Add("@Pack_size", ObjClassification.Pack_size);
            hashPara.Add("@Product_Group", ObjClassification.Product_Group);
            hashPara.Add("@DRUG_CATEGORY", ObjClassification.DRUG_CATEGORY);
            hashPara.Add("@MARKET_ENTRY_DATE", ObjClassification.MARKET_ENTRY_DATE);
            hashPara.Add("@PZN_HORMOSAN", ObjClassification.PZN_HORMOSAN);
            hashPara.Add("@StorageCondition", ObjClassification.StorageCondition);

            hashPara.Add("@Allowed_Manufacturers", ObjClassification.Allowed_Manufacturers);
            hashPara.Add("@HSAN_MATERIAL_IDENTIFIER", ObjClassification.HSAN_MATERIAL_IDENTIFIER);
            hashPara.Add("@Expiration_date_shelf_life", ObjClassification.Expiration_date_shelf_life);
            hashPara.Add("@Next_Insp_Date_for_Batch", ObjClassification.Next_Insp_Date_for_Batch);
            hashPara.Add("@Batch_number", ObjClassification.Batch_number);
            hashPara.Add("@ASSAY_ASIS", ObjClassification.ASSAY_ASIS);
            hashPara.Add("@MANUFACTURER", ObjClassification.MANUFACTURER);
            hashPara.Add("@Potency_as_is_basis", ObjClassification.Potency_as_is_basis);
            hashPara.Add("@Loss_on_Drying", ObjClassification.Loss_on_Drying);
            hashPara.Add("@Potency_as_is_basis1", ObjClassification.Potency_as_is_basis1);
            hashPara.Add("@RM402217", ObjClassification.RM402217);
            hashPara.Add("@RM323350", ObjClassification.RM323350);
            hashPara.Add("@SF110063", ObjClassification.SF110063);
            hashPara.Add("@SF900052", ObjClassification.SF900052);
            hashPara.Add("@IP4A0047", ObjClassification.IP4A0047);
            hashPara.Add("@Assay_by_GC", ObjClassification.Assay_by_GC);
            hashPara.Add("@External_Material_Group", ObjClassification.External_Material_Group);
            hashPara.Add("@Version_Number", ObjClassification.Version_Number);

            //PROV-CCP-MM-941-23-0045 Start
            hashPara.Add("@sKXSBU", ObjClassification.sKXSBU);
            hashPara.Add("@sKXMARKT", ObjClassification.sKXMARKT);
            hashPara.Add("@sKXSELLCTRY", ObjClassification.sKXSELLCTRY);
            hashPara.Add("@sKXBUSI", ObjClassification.sKXBUSI);
            hashPara.Add("@sKXDIV", ObjClassification.sKXDIV); 
            hashPara.Add("@sKXTHER", ObjClassification.sKXTHER);
            hashPara.Add("@sKXDOSFRM", ObjClassification.sKXDOSFRM);
            hashPara.Add("@sKXMINSL", ObjClassification.sKXMINSL);
            hashPara.Add("@sMKTMNGER", ObjClassification.sMKTMNGER);
            hashPara.Add("@sCS_MOLECULE", ObjClassification.sCS_MOLECULE);
            hashPara.Add("@sMGRPPX", ObjClassification.sMGRPPX);
            //PROV-CCP-MM-941-23-0045 End

            hashPara.Add("@IsActive", ObjClassification.IsActive);
            hashPara.Add("@UserId", ObjClassification.UserId);
            hashPara.Add("@UserIp", ObjClassification.IPAddress);
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

        public Classification GetClassification(int Mat_Classification_Id)
        {
            Classification ObjClassification = new Classification();

            DataAccessLayer objDal = new DataAccessLayer();
            Utility ObjUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Classification_By_MatClassificationId";
            DataSet ds;

            hashPara.Add("@Mat_Classification_Id", Mat_Classification_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjClassification.Mat_Classification_Id = Convert.ToInt32(dt.Rows[0]["Mat_Classification_Id"].ToString());
                        ObjClassification.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjClassification.Class_Type = dt.Rows[0]["Class_Type"].ToString();
                        ObjClassification.Class = dt.Rows[0]["Class"].ToString();

                        ObjClassification.Strength_of_mat_Pack_type = dt.Rows[0]["Strength_of_mat_Pack_type"].ToString();
                        ObjClassification.Market = dt.Rows[0]["Market"].ToString();
                        ObjClassification.NDC_No_LPI = dt.Rows[0]["NDC_No_LPI"].ToString();
                        ObjClassification.NDC_No_LL = dt.Rows[0]["NDC_No_LL"].ToString();
                        ObjClassification.HTS = dt.Rows[0]["HTS"].ToString();
                        ObjClassification.ANDA = dt.Rows[0]["ANDA"].ToString();
                        ObjClassification.FDA_No = dt.Rows[0]["FDA_No"].ToString();
                        ObjClassification.LPI_Material_Identifier = dt.Rows[0]["LPI_Material_Identifier"].ToString();
                        ObjClassification.Material_Grouping_for_MES = dt.Rows[0]["Material_Grouping_for_MES"].ToString();
                        ObjClassification.Short_description_for_3PL = dt.Rows[0]["Short_description_for_3PL"].ToString();
                        ObjClassification.Package_Presentation_3PL = dt.Rows[0]["Package_Presentation_3PL"].ToString();
                        ObjClassification.Number_of_Tablet_3PL = dt.Rows[0]["Number_of_Tablet_3PL"].ToString();
                        ObjClassification.Material_Category_A_3PL = dt.Rows[0]["Material_Category_A_3PL"].ToString();
                        ObjClassification.Material_Category_B_3PL = dt.Rows[0]["Material_Category_B_3PL"].ToString();
                        ObjClassification.Sorting_for_inventory_report = dt.Rows[0]["Sorting_for_inventory_report"].ToString();
                        ObjClassification.Pack_size = dt.Rows[0]["Pack_size"].ToString();
                        ObjClassification.Product_Group = dt.Rows[0]["Product_Group"].ToString();
                        ObjClassification.DRUG_CATEGORY = dt.Rows[0]["DRUG_CATEGORY"].ToString();
                        ObjClassification.MARKET_ENTRY_DATE = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["MARKET_ENTRY_DATE"].ToString());
                        ObjClassification.PZN_HORMOSAN = dt.Rows[0]["PZN_HORMOSAN"].ToString();
                        ObjClassification.StorageCondition = dt.Rows[0]["StorageCondition"].ToString();

                        ObjClassification.Allowed_Manufacturers = dt.Rows[0]["Allowed_Manufacturers"].ToString();
                        ObjClassification.HSAN_MATERIAL_IDENTIFIER = dt.Rows[0]["HSAN_MATERIAL_IDENTIFIER"].ToString();
                        ObjClassification.Expiration_date_shelf_life = dt.Rows[0]["Expiration_date_shelf_life"].ToString();
                        ObjClassification.Next_Insp_Date_for_Batch = dt.Rows[0]["Next_Insp_Date_for_Batch"].ToString();
                        ObjClassification.Batch_number = dt.Rows[0]["Batch_number"].ToString();
                        ObjClassification.ASSAY_ASIS = dt.Rows[0]["ASSAY_ASIS"].ToString();
                        ObjClassification.MANUFACTURER = dt.Rows[0]["MANUFACTURER"].ToString();
                        ObjClassification.Potency_as_is_basis = dt.Rows[0]["Potency_as_is_basis"].ToString();
                        ObjClassification.Loss_on_Drying = dt.Rows[0]["Loss_on_Drying"].ToString();
                        ObjClassification.Potency_as_is_basis1 = dt.Rows[0]["Potency_as_is_basis1"].ToString();
                        ObjClassification.RM402217 = dt.Rows[0]["RM402217"].ToString();
                        ObjClassification.RM323350 = dt.Rows[0]["RM323350"].ToString();
                        ObjClassification.SF110063 = dt.Rows[0]["SF110063"].ToString();
                        ObjClassification.SF900052 = dt.Rows[0]["SF900052"].ToString();
                        ObjClassification.IP4A0047 = dt.Rows[0]["IP4A0047"].ToString();
                        ObjClassification.Assay_by_GC = dt.Rows[0]["Assay_by_GC"].ToString();
                        ObjClassification.External_Material_Group = dt.Rows[0]["External_Material_Group"].ToString();
                        ObjClassification.Version_Number = dt.Rows[0]["Version_Number"].ToString();
                        //PROV-CCP-MM-941-23-0045 Start
                        ObjClassification.sKXSBU = dt.Rows[0]["sKXSBU"].ToString();
                        ObjClassification.sKXMARKT = dt.Rows[0]["sKXMARKT"].ToString();
                        ObjClassification.sKXSELLCTRY = dt.Rows[0]["sKXSELLCTRY"].ToString();
                        ObjClassification.sKXBUSI = dt.Rows[0]["sKXBUSI"].ToString();
                        ObjClassification.sKXDIV = dt.Rows[0]["sKXDIV"].ToString();
                        ObjClassification.sKXTHER = dt.Rows[0]["sKXTHER"].ToString();
                        ObjClassification.sKXDOSFRM = dt.Rows[0]["sKXDOSFRM"].ToString();
                        ObjClassification.sKXMINSL = dt.Rows[0]["sKXMINSL"].ToString();
                        ObjClassification.sMKTMNGER = dt.Rows[0]["sMKTMNGER"].ToString();
                        ObjClassification.sCS_MOLECULE = dt.Rows[0]["sCS_MOLECULE"].ToString();
                        ObjClassification.sMGRPPX = dt.Rows[0]["sMGRPPX"].ToString();
                        //PROV-CCP-MM-941-23-0045 End
                    }
                }
                return ObjClassification;
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

        public DataSet GetClassificationData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Classification_By_MasterHeaderId";
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

        /// <summary>
        /// IND_DT14012020
        /// </summary>
        /// <param name="Module_Type"></param>
        /// <param name="ControlName"></param>
        /// <param name="Section"></param>
        /// <param name="Filter_Field"></param>
        /// <returns></returns>
        public DataSet GetClassList(string Module_Type, string ControlName, string Section, string Filter_Field)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDropDownListByControlNameModuleType_MultiClass";
            DataSet ds;

            hashPara.Add("@Module_Type", Module_Type);
            hashPara.Add("@ControlName", ControlName);
            hashPara.Add("@Section", Section);
            hashPara.Add("@Filter_Field", Filter_Field);

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


        #endregion

    }
}