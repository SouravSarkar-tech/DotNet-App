using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class BasicDataAccess
    {
        Utility ObjUtil = new Utility();

        public DataSet ReadCommanData(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            DataSet dstData = new DataSet();
            StringBuilder query = new StringBuilder();
            query.Append(" DECLARE @Values VARCHAR(100) ");
            query.Append(" SELECT @Values = COALESCE(@Values + ', ', '') + Industory_Sector FROM T_Basic_Date_Industory_Sector_Mapping  WHERE Master_Header_Id = " + masterHeaderId + " AND IsActive = 'TRUE' ");
            query.Append(" SELECT Material_Number,Material_Type,@Values AS 'Industory_Sector' ");
            query.Append(" FROM T_Mat_Basic_Data1 WHERE Master_Header_Id = " + masterHeaderId + " AND IsActive = 'TRUE' ");
            return objDal.FillDataSet(query.ToString(), "table");
        }

        public DataSet ReadBasicData1(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Get_Basic_Data1_By_MasterHeaderId";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public DataSet ReadBasicData2(string masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Get_Basic_Data2_By_MasterHeaderId";
            hashPara.Add("@MasterHeaderId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public DataSet GetIRFDivisionList()
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_IRFDivisionVal";
            DataSet ds;


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
        /// PROSOL_SDT16092019
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public DataSet GetProsolID(int masterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_ProsolID";
            hashPara.Add("@mId", masterHeaderId);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        /// <summary>
        /// PROSOL_SDT16092019
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public DataSet GetMWTID(string ProsolID)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MWTID";
            hashPara.Add("@mId", ProsolID);
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }


        //New Code added by Swati EDT:04022019


        public BasicData GetBasicData1(string masterHeaderId)
        {
            BasicData ObjBasicData = new BasicData();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Basic_Data1_By_MasterHeaderId";
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
                        ObjBasicData.Mat_Basic_Data1_Id = Convert.ToInt32(dt.Rows[0]["Mat_Basic_Data1_Id"].ToString());
                        ObjBasicData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBasicData.ModulePlantGroupCode = dt.Rows[0]["ModulePlantGroupCode"].ToString();
                        ObjBasicData.Material_Number = dt.Rows[0]["Material_Number"].ToString();
                        ObjBasicData.Material_Type = dt.Rows[0]["Material_Type"].ToString();
                        ObjBasicData.Industry_Sector = dt.Rows[0]["Industry_Sector"].ToString();
                        ObjBasicData.Base_Unit_Of_Measure = dt.Rows[0]["Base_Unit_Of_Measure"].ToString();
                        ObjBasicData.Material_Short_Description = dt.Rows[0]["Material_Short_Description"].ToString();
                        ObjBasicData.Material_Group = dt.Rows[0]["Material_Group"].ToString();
                        ObjBasicData.Old_Material_Number = dt.Rows[0]["Old_Material_Number"].ToString();
                        ObjBasicData.External_Material_Group = dt.Rows[0]["External_Material_Group"].ToString();
                        ObjBasicData.Lab_Design_Office = dt.Rows[0]["Lab_Design_Office"].ToString();
                        ObjBasicData.Division = dt.Rows[0]["Division"].ToString();
                        ObjBasicData.Product_Hierarchy = dt.Rows[0]["Product_Hierarchy"].ToString();
                        ObjBasicData.Cross_Plant_Material_Status = dt.Rows[0]["Cross_Plant_Material_Status"].ToString();
                        ObjBasicData.Valid_From = ObjUtil.GetDDMMYYYYNew(dt.Rows[0]["Valid_From"].ToString());
                        ObjBasicData.Gen_Item_Category_Grp = dt.Rows[0]["Gen_Item_Category_Grp"].ToString();
                        ObjBasicData.Prod_Inspect_Memo = dt.Rows[0]["Prod_Inspect_Memo"].ToString();
                        ObjBasicData.Gross_Weight = dt.Rows[0]["Gross_Weight"].ToString();
                        ObjBasicData.Net_Weight = dt.Rows[0]["Net_Weight"].ToString();
                        ObjBasicData.Weight_Unit = dt.Rows[0]["Weight_Unit"].ToString();
                        ObjBasicData.Volume = dt.Rows[0]["Volume"].ToString();
                        ObjBasicData.Volume_Unit = dt.Rows[0]["Volume_Unit"].ToString();
                        ObjBasicData.InterNational_Article_No = dt.Rows[0]["InterNational_Article_No"].ToString();
                        ObjBasicData.Category_InterN_Article_No = dt.Rows[0]["Category_InterN_Article_No"].ToString();
                        ObjBasicData.Material_Grp_Pack_Mtl = dt.Rows[0]["Material_Grp_Pack_Mtl"].ToString();
                        ObjBasicData.Reason_For_Creation = dt.Rows[0]["Reason_For_Creation"].ToString();
                        ObjBasicData.Remarks = dt.Rows[0]["Remarks"].ToString();
                        //CTRL_SUB_SDT06062019
                        ObjBasicData.sControlSubYN = dt.Rows[0]["sControlSubYN"].ToString();
                        //CTRL_SUB_SDT06062019
                        //DT05072023_BG_Type
                        ObjBasicData.sBGWCF = dt.Rows[0]["sBGWCF"].ToString();
                        //DT05072023_BG_Type
                        //PROV-CCP-MM-941-23-0045 in QAMS
                        ObjBasicData.sIsMatComm = dt.Rows[0]["sIsMatComm"].ToString();
                        //PROV-CCP-MM-941-23-0045 in QAMS
                    }
                    DataTable dt1 = ds.Tables[1];
                    if (dt1.Rows.Count > 0)
                    {
                        //CTRL_SUB_SDT06062019
                        ObjBasicData.ReqCreatedOn = dt1.Rows[0]["ReqCreatedOn"].ToString();
                        //CTRL_SUB_SDT06062019
                    }
                }
                return ObjBasicData;
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
        /// PROSOL_SDT16092019
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataSet GetMWTDetails(int Master_Header_Id)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "";
            try
            {
                hashPara.Add("@Master_Header_Id", Master_Header_Id);
                procName = "pr_Get_Master_Request";
            }
            catch (Exception ex)
            {

            }
            return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

        }

        public int ReCreateMWTRequest(string proslid, string txtRemarks)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Pr_RecreateMWTReq_Prosol";
            int result = 0;
            hashPara.Add("@sProsolID", proslid);
            hashPara.Add("@txtRemarks", txtRemarks);
            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }

        /// <summary>
        /// PROSOL_SDT16092019
        /// </summary>
        /// <param name="proslid"></param>
        /// <param name="txtRemarks"></param>
        /// <param name="sFlag"></param>
        /// <returns></returns>
        public int UpdateRequestStatus(string proslid, string txtRemarks, string sFlag, string sFlagPM)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "sp_Update_Status_Remark";
            int result = 0;
            hashPara.Add("@sProsolID", proslid);
            hashPara.Add("@txtRemarks", txtRemarks);
            hashPara.Add("@sFlag", sFlag);
            hashPara.Add("@sFlagPM", sFlagPM);
            try
            {
                objDal.OpenConnection();
                result = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }
        ///MSC_8300001775
        public BasicData2 GetBasicData2(string masterHeaderId)
        {
            BasicData2 ObjBasicData = new BasicData2();
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_Basic_Data2_By_MasterHeaderId";
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
                        ObjBasicData.Mat_Basic_Data2_Id = Convert.ToInt32(dt.Rows[0]["Mat_Basic_Data2_Id"].ToString());
                        ObjBasicData.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjBasicData.Length = dt.Rows[0]["Length"].ToString();
                        ObjBasicData.Width = dt.Rows[0]["Width"].ToString();
                        ObjBasicData.Height = dt.Rows[0]["Height"].ToString();
                        ObjBasicData.Unit_Of_Dimension = dt.Rows[0]["Unit_Of_Dimension"].ToString();
                        ObjBasicData.Desc_Language = dt.Rows[0]["Desc_Language"].ToString();
                        ObjBasicData.Desc_Text = dt.Rows[0]["Desc_Text"].ToString();
                        ObjBasicData.Desc_Language1 = dt.Rows[0]["Desc_Language1"].ToString();
                        ObjBasicData.Desc_Text1 = dt.Rows[0]["Desc_Text1"].ToString();
                        ObjBasicData.Basic_Data_Language = dt.Rows[0]["Basic_Data_Language"].ToString();
                        ObjBasicData.Basic_Data_Text = dt.Rows[0]["Basic_Data_Text"].ToString();
                        ObjBasicData.Basic_Data_Language1 = dt.Rows[0]["Basic_Data_Language1"].ToString();
                        ObjBasicData.Basic_Data_Text1 = dt.Rows[0]["Basic_Data_Text1"].ToString();
                        ObjBasicData.Inspection_Language = dt.Rows[0]["Inspection_Language"].ToString();
                        ObjBasicData.Inspection_Text = dt.Rows[0]["Inspection_Text"].ToString();
                        ObjBasicData.Inspection_Language1 = dt.Rows[0]["Inspection_Language1"].ToString();
                        ObjBasicData.Inspection_Text1 = dt.Rows[0]["Inspection_Text1"].ToString();
                        ObjBasicData.Internal_Comment_Language = dt.Rows[0]["Internal_Comment_Language"].ToString();
                        ObjBasicData.Internal_Comment_Text = dt.Rows[0]["Internal_Comment_Text"].ToString();
                        ObjBasicData.Internal_Comment_Language1 = dt.Rows[0]["Internal_Comment_Language1"].ToString();
                        ObjBasicData.Internal_Comment_Text1 = dt.Rows[0]["Internal_Comment_Text1"].ToString();
                        ObjBasicData.Alt_Unit_Value_X = dt.Rows[0]["Alt_Unit_Value_X"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure = dt.Rows[0]["Alt_Unit_Of_Measure"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y = dt.Rows[0]["Alt_Unit_Value_Y"].ToString();
                        ObjBasicData.Alt_Unit_Value_X1 = dt.Rows[0]["Alt_Unit_Value_X1"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure1 = dt.Rows[0]["Alt_Unit_Of_Measure1"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y1 = dt.Rows[0]["Alt_Unit_Value_Y1"].ToString();
                        ObjBasicData.Alt_Unit_Value_X2 = dt.Rows[0]["Alt_Unit_Value_X2"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure2 = dt.Rows[0]["Alt_Unit_Of_Measure2"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y2 = dt.Rows[0]["Alt_Unit_Value_Y2"].ToString();
                        ObjBasicData.Shipper_Gross_Weight = dt.Rows[0]["Shipper_Gross_Weight"].ToString();
                        ObjBasicData.Shipper_Weight_Unit = dt.Rows[0]["Shipper_Weight_Unit"].ToString();
                        ObjBasicData.Unit_Of_Measure_Usage = dt.Rows[0]["Unit_Of_Measure_Usage"].ToString();
                        ObjBasicData.Characteristic_Name = dt.Rows[0]["Characteristic_Name"].ToString();
                        ObjBasicData.Planned_Value_For_Unit_Measure = dt.Rows[0]["Planned_Value_For_Unit_Measure"].ToString();
                        ObjBasicData.Batch_Spcf_Matl_Unit_Measure = dt.Rows[0]["Batch_Spcf_Matl_Unit_Measure"].ToString();

                        ObjBasicData.Alt_Unit_Value_X3 = dt.Rows[0]["Alt_Unit_Value_X3"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure3 = dt.Rows[0]["Alt_Unit_Of_Measure3"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y3 = dt.Rows[0]["Alt_Unit_Value_Y3"].ToString();
                        ObjBasicData.Alt_Unit_Value_X4 = dt.Rows[0]["Alt_Unit_Value_X4"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure4 = dt.Rows[0]["Alt_Unit_Of_Measure4"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y4 = dt.Rows[0]["Alt_Unit_Value_Y4"].ToString();
                        ObjBasicData.Alt_Unit_Value_X5 = dt.Rows[0]["Alt_Unit_Value_X5"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure5 = dt.Rows[0]["Alt_Unit_Of_Measure5"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y5 = dt.Rows[0]["Alt_Unit_Value_Y5"].ToString();
                        ObjBasicData.Alt_Unit_Value_X6 = dt.Rows[0]["Alt_Unit_Value_X6"].ToString();
                        ObjBasicData.Alt_Unit_Of_Measure6 = dt.Rows[0]["Alt_Unit_Of_Measure6"].ToString();
                        ObjBasicData.Alt_Unit_Value_Y6 = dt.Rows[0]["Alt_Unit_Value_Y6"].ToString();

                    }
                }
                return ObjBasicData;
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

        public int SaveBasicData(BasicData objBasicData)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Basic_Data1";
            int result = 0;
            hashPara.Add("@Mat_Basic_Data1_Id", objBasicData.Mat_Basic_Data1_Id);
            hashPara.Add("@Master_Header_Id", objBasicData.Master_Header_Id);
            hashPara.Add("@Material_Number", objBasicData.Material_Number);
            hashPara.Add("@Material_Type", objBasicData.Material_Type);
            hashPara.Add("@Industry_Sector", objBasicData.Industry_Sector);
            hashPara.Add("@Base_Unit_Of_Measure", objBasicData.Base_Unit_Of_Measure);
            hashPara.Add("@Material_Short_Description", objBasicData.Material_Short_Description);
            hashPara.Add("@Material_Group", objBasicData.Material_Group);
            hashPara.Add("@Old_Material_Number", objBasicData.Old_Material_Number);
            hashPara.Add("@External_Material_Group", objBasicData.External_Material_Group);
            hashPara.Add("@Lab_Design_Office", objBasicData.Lab_Design_Office);
            hashPara.Add("@Division", objBasicData.Division);
            hashPara.Add("@Product_Hierarchy", objBasicData.Product_Hierarchy);
            hashPara.Add("@Cross_Plant_Material_Status", objBasicData.Cross_Plant_Material_Status);
            hashPara.Add("@Valid_From", objBasicData.Valid_From);
            hashPara.Add("@Gen_Item_Category_Grp", objBasicData.Gen_Item_Category_Grp);
            hashPara.Add("@Prod_Inspect_Memo", objBasicData.Prod_Inspect_Memo);
            hashPara.Add("@Gross_Weight", objBasicData.Gross_Weight);
            hashPara.Add("@Net_Weight", objBasicData.Net_Weight);
            hashPara.Add("@Weight_Unit", objBasicData.Weight_Unit);
            hashPara.Add("@Volume", objBasicData.Volume);
            hashPara.Add("@Volume_Unit", objBasicData.Volume_Unit);
            hashPara.Add("@InterNational_Article_No", objBasicData.InterNational_Article_No);
            hashPara.Add("@Category_InterN_Article_No", objBasicData.Category_InterN_Article_No);
            hashPara.Add("@Material_Grp_Pack_Mtl", objBasicData.Material_Grp_Pack_Mtl);
            hashPara.Add("@Reason_For_Creation", objBasicData.Reason_For_Creation);
            hashPara.Add("@Remarks", objBasicData.Remarks);

            //hashPara.Add("@IsActive", objBasicData.isa);
            hashPara.Add("@UserId", objBasicData.UserId);
            hashPara.Add("@UserIp", objBasicData.IPAddress);
            //CTRL_SUB_SDT06062019
            hashPara.Add("@sControlSubYN", objBasicData.sControlSubYN);
            //CTRL_SUB_EDT06062019
            //DT05072023_BG_Type
            hashPara.Add("@sBGWCF", objBasicData.sBGWCF);
            //DT05072023_BG_Type
            // PROV-CCP-MM-941-23-0045 in QAMS
            hashPara.Add("@sIsMatComm", objBasicData.sIsMatComm);
            // PROV-CCP-MM-941-23-0045 in QAMS
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
        ///MSC_8300001775
        public int SaveBasicData2(BasicData2 objBasicData)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_Basic_Data2";
            int result = 0;

            hashPara.Add("@Mat_Basic_Data2_Id", objBasicData.Mat_Basic_Data2_Id);
            hashPara.Add("@Master_Header_Id", objBasicData.Master_Header_Id);
            hashPara.Add("@Length", objBasicData.Length);
            hashPara.Add("@Width", objBasicData.Width);
            hashPara.Add("@Height", objBasicData.Height);
            hashPara.Add("@Unit_Of_Dimension", objBasicData.Unit_Of_Dimension);
            hashPara.Add("@Desc_Language", objBasicData.Desc_Language);
            hashPara.Add("@Desc_Text", objBasicData.Desc_Text);
            hashPara.Add("@Desc_Language1", objBasicData.Desc_Language1);
            hashPara.Add("@Desc_Text1", objBasicData.Desc_Text1);
            hashPara.Add("@Basic_Data_Language", objBasicData.Basic_Data_Language);
            hashPara.Add("@Basic_Data_Text", objBasicData.Basic_Data_Text);
            hashPara.Add("@Basic_Data_Language1", objBasicData.Basic_Data_Language1);
            hashPara.Add("@Basic_Data_Text1", objBasicData.Basic_Data_Text1);
            hashPara.Add("@Inspection_Language", objBasicData.Inspection_Language);
            hashPara.Add("@Inspection_Text", objBasicData.Inspection_Text);
            hashPara.Add("@Inspection_Language1", objBasicData.Inspection_Language1);
            hashPara.Add("@Inspection_Text1", objBasicData.Inspection_Text1);
            hashPara.Add("@Internal_Comment_Language", objBasicData.Internal_Comment_Language);
            hashPara.Add("@Internal_Comment_Text", objBasicData.Internal_Comment_Text);
            hashPara.Add("@Internal_Comment_Language1", objBasicData.Internal_Comment_Language1);
            hashPara.Add("@Internal_Comment_Text1", objBasicData.Internal_Comment_Text1);
            hashPara.Add("@Alt_Unit_Value_X", objBasicData.Alt_Unit_Value_X);
            hashPara.Add("@Alt_Unit_Of_Measure", objBasicData.Alt_Unit_Of_Measure);
            hashPara.Add("@Alt_Unit_Value_Y", objBasicData.Alt_Unit_Value_Y);
            hashPara.Add("@Alt_Unit_Value_X1", objBasicData.Alt_Unit_Value_X1);
            hashPara.Add("@Alt_Unit_Of_Measure1", objBasicData.Alt_Unit_Of_Measure1);
            hashPara.Add("@Alt_Unit_Value_Y1", objBasicData.Alt_Unit_Value_Y1);
            hashPara.Add("@Alt_Unit_Value_X2", objBasicData.Alt_Unit_Value_X2);
            hashPara.Add("@Alt_Unit_Of_Measure2", objBasicData.Alt_Unit_Of_Measure2);
            hashPara.Add("@Alt_Unit_Value_Y2", objBasicData.Alt_Unit_Value_Y2);
            hashPara.Add("@Shipper_Gross_Weight", objBasicData.Shipper_Gross_Weight);
            hashPara.Add("@Shipper_Weight_Unit", objBasicData.Shipper_Weight_Unit);
            hashPara.Add("@Unit_Of_Measure_Usage", objBasicData.Unit_Of_Measure_Usage);
            hashPara.Add("@Characteristic_Name", objBasicData.Characteristic_Name);
            hashPara.Add("@Planned_Value_For_Unit_Measure", objBasicData.Planned_Value_For_Unit_Measure);
            hashPara.Add("@Batch_Spcf_Matl_Unit_Measure", objBasicData.Batch_Spcf_Matl_Unit_Measure);

            hashPara.Add("@IsActive", objBasicData.IsActive);
            hashPara.Add("@UserId", objBasicData.UserId);
            hashPara.Add("@UserIp", objBasicData.IPAddress);

            hashPara.Add("@Alt_Unit_Value_X3", objBasicData.Alt_Unit_Value_X3);
            hashPara.Add("@Alt_Unit_Of_Measure3", objBasicData.Alt_Unit_Of_Measure3);
            hashPara.Add("@Alt_Unit_Value_Y3", objBasicData.Alt_Unit_Value_Y3);

            hashPara.Add("@Alt_Unit_Value_X4", objBasicData.Alt_Unit_Value_X4);
            hashPara.Add("@Alt_Unit_Of_Measure4", objBasicData.Alt_Unit_Of_Measure4);
            hashPara.Add("@Alt_Unit_Value_Y4", objBasicData.Alt_Unit_Value_Y4);

            hashPara.Add("@Alt_Unit_Value_X5", objBasicData.Alt_Unit_Value_X5);
            hashPara.Add("@Alt_Unit_Of_Measure5", objBasicData.Alt_Unit_Of_Measure5);
            hashPara.Add("@Alt_Unit_Value_Y5", objBasicData.Alt_Unit_Value_Y5);

            hashPara.Add("@Alt_Unit_Value_X6", objBasicData.Alt_Unit_Value_X6);
            hashPara.Add("@Alt_Unit_Of_Measure6", objBasicData.Alt_Unit_Of_Measure6);
            hashPara.Add("@Alt_Unit_Value_Y6", objBasicData.Alt_Unit_Value_Y6);

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

        public MaterialDesc GetMatDescByMasterHeaderId(string masterHeaderId)
        {
            MaterialDesc objMatDesc = new MaterialDesc();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Mat_MaterialDesc_By_MasterHeaderId";
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
                        objMatDesc.Mat_Material_Desc_Id = Convert.ToInt32(dt.Rows[0]["Mat_Material_Desc_Id"].ToString());
                        objMatDesc.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        objMatDesc.Item_Type = dt.Rows[0]["Item_Type"].ToString();
                        objMatDesc.Item_Description = dt.Rows[0]["Item_Description"].ToString();
                        objMatDesc.MOC = dt.Rows[0]["MOC"].ToString();
                        objMatDesc.Size = dt.Rows[0]["Size"].ToString();
                        objMatDesc.Process_Connection_Size = dt.Rows[0]["Process_Connection_Size"].ToString();
                        objMatDesc.Class_Rating_Grade = dt.Rows[0]["Class_Rating_Grade"].ToString();
                        objMatDesc.MFG_Std = dt.Rows[0]["MFG_Std"].ToString();
                        objMatDesc.Range_Capacity = dt.Rows[0]["Range_Capacity"].ToString();
                        objMatDesc.Accuracy_LeastCount = dt.Rows[0]["Accuracy_LeastCount"].ToString();
                        objMatDesc.Supply_Voltage = dt.Rows[0]["Supply_Voltage"].ToString();
                        objMatDesc.Flame_Proof = dt.Rows[0]["Flame_Proof"].ToString();
                        objMatDesc.Protection_Class = dt.Rows[0]["Protection_Class"].ToString();
                        objMatDesc.Input_Output = dt.Rows[0]["Input_Output"].ToString();
                        objMatDesc.Manufacturer_PartNo = dt.Rows[0]["Manufacturer_PartNo"].ToString();
                        objMatDesc.Make_MachName_ModelNo = dt.Rows[0]["Make_MachName_ModelNo"].ToString();
                        objMatDesc.Material_Desc = dt.Rows[0]["Material_Desc"].ToString();
                    }
                }
                return objMatDesc;
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

        public int SaveMatDesc(MaterialDesc objMatDesc)
        {
            // bool flag = false;

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Mat_MaterialDesc";
            int res = 0;

            hashPara.Add("@Mat_Material_Desc_Id", objMatDesc.Mat_Material_Desc_Id);
            hashPara.Add("@Master_Header_Id", objMatDesc.Master_Header_Id);
            hashPara.Add("@Item_Description", objMatDesc.Item_Description);
            hashPara.Add("@Item_Type", objMatDesc.Item_Type);
            hashPara.Add("@MOC", objMatDesc.MOC);
            hashPara.Add("@Size", objMatDesc.Size);
            hashPara.Add("@Process_Connection_Size", objMatDesc.Process_Connection_Size);
            hashPara.Add("@Class_Rating_Grade", objMatDesc.Class_Rating_Grade);
            hashPara.Add("@MFG_Std", objMatDesc.MFG_Std);
            hashPara.Add("@Range_Capacity", objMatDesc.Range_Capacity);
            hashPara.Add("@Accuracy_LeastCount", objMatDesc.Accuracy_LeastCount);
            hashPara.Add("@Supply_Voltage", objMatDesc.Supply_Voltage);
            hashPara.Add("@Flame_Proof", objMatDesc.Flame_Proof);
            hashPara.Add("@Protection_Class", objMatDesc.Protection_Class);
            hashPara.Add("@Input_Output", objMatDesc.Input_Output);
            hashPara.Add("@Manufacturer_PartNo", objMatDesc.Manufacturer_PartNo);
            hashPara.Add("@Make_MachName_ModelNo", objMatDesc.Make_MachName_ModelNo);
            hashPara.Add("@Material_Desc", objMatDesc.Material_Desc);

            hashPara.Add("@IsActive", objMatDesc.IsActive);
            hashPara.Add("@UserId", objMatDesc.UserId);
            hashPara.Add("@UserIp", objMatDesc.IPAddress);

            try
            {
                objDal.OpenConnection();
                res = objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
                return res;
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