using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Accenture.MWT.DomainObject;

namespace Accenture.MWT.DataAccess
{
    public class MaterialBlockAccess
    {
        public MaterialBlockAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(MaterialBlock ObjMaterialBlock)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Material_Block";
            int result = 0;

            hashPara.Add("@Material_Block_Id", ObjMaterialBlock.Material_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjMaterialBlock.Master_Header_Id);
            hashPara.Add("@Material_Number", ObjMaterialBlock.Material_Number);
            hashPara.Add("@Material_Type", ObjMaterialBlock.Material_Type);
            hashPara.Add("@Material_Short_Description", ObjMaterialBlock.Material_Short_Description);
            hashPara.Add("@Blocking_Level", ObjMaterialBlock.Blocking_Level);
            hashPara.Add("@Plant_Id", ObjMaterialBlock.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMaterialBlock.Storage_Location);
            hashPara.Add("@Sales_Organization_Id", ObjMaterialBlock.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjMaterialBlock.Distribution_Channel_ID);
            hashPara.Add("@Material_Status", ObjMaterialBlock.Material_Status);
            hashPara.Add("@Purchase_Status", ObjMaterialBlock.Purchase_Status);
            hashPara.Add("@Remarks", ObjMaterialBlock.Remarks);
            hashPara.Add("@IsActive", ObjMaterialBlock.IsActive);
            hashPara.Add("@UserId", ObjMaterialBlock.UserId);
            hashPara.Add("@UserIp", ObjMaterialBlock.IPAddress);

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

        public MaterialBlock GetMaterialBlock(int Material_Block_Id)
        {
            MaterialBlock ObjMaterialBlock = new MaterialBlock();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaterialBlock_By_MaterialBlockId";
            DataSet ds;

            hashPara.Add("@Material_Block_Id", Material_Block_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjMaterialBlock.Material_Block_Id = dt.Rows[0]["Material_Block_Id"].ToString() == "" ? Material_Block_Id : Convert.ToInt32(dt.Rows[0]["Material_Block_Id"].ToString());
                        ObjMaterialBlock.Master_Header_Id = dt.Rows[0]["Master_Header_Id"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());

                        ObjMaterialBlock.Material_Number = dt.Rows[0]["Material_Number"].ToString();
                        ObjMaterialBlock.Material_Type = dt.Rows[0]["Material_Type"].ToString();
                        ObjMaterialBlock.Material_Short_Description = dt.Rows[0]["Material_Short_Description"].ToString();
                        ObjMaterialBlock.Blocking_Level = dt.Rows[0]["Blocking_Level"].ToString();
                        ObjMaterialBlock.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjMaterialBlock.Storage_Location = dt.Rows[0]["Storage_Location"].ToString();
                        ObjMaterialBlock.Sales_Organization_Id = dt.Rows[0]["Sales_Organization_Id"].ToString();
                        ObjMaterialBlock.Distribution_Channel_ID = dt.Rows[0]["Distribution_Channel_ID"].ToString();
                        ObjMaterialBlock.Material_Status = dt.Rows[0]["Material_Status"].ToString();
                        ObjMaterialBlock.Purchase_Status = dt.Rows[0]["Purchase_Status"].ToString();
                        ObjMaterialBlock.Remarks = dt.Rows[0]["Remarks"].ToString();
                    }
                }
                return ObjMaterialBlock;
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

        public DataSet GetMaterialBlockData(int MasterHeaderId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_MaterialBlock_By_MasterHeaderId";
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

        public int DeleteMaterialBlockData(int Material_Block_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Delete_MaterialBlock_By_Material_Block_Id";
            int result = 0;

            hashPara.Add("@Material_Block_Id", Material_Block_Id);

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

         public int SaveMass(MaterialBlock ObjMaterialBlock)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Material_Block_Mass";
            int result = 0;

            hashPara.Add("@Material_Block_Id", ObjMaterialBlock.Material_Block_Id);
            hashPara.Add("@Master_Header_Id", ObjMaterialBlock.Master_Header_Id);
            hashPara.Add("@Material_Number", ObjMaterialBlock.Material_Number);
            hashPara.Add("@Material_Type", ObjMaterialBlock.Material_Type);
            hashPara.Add("@Material_Short_Description", ObjMaterialBlock.Material_Short_Description);
            hashPara.Add("@Blocking_Level", ObjMaterialBlock.Blocking_Level);
            hashPara.Add("@Plant_Id", ObjMaterialBlock.Plant_Id);
            hashPara.Add("@Storage_Location", ObjMaterialBlock.Storage_Location);
            hashPara.Add("@Sales_Organization_Id", ObjMaterialBlock.Sales_Organization_Id);
            hashPara.Add("@Distribution_Channel_ID", ObjMaterialBlock.Distribution_Channel_ID);
            hashPara.Add("@Material_Status", ObjMaterialBlock.Material_Status);
            hashPara.Add("@Purchase_Status", ObjMaterialBlock.Purchase_Status);
            hashPara.Add("@Remarks", ObjMaterialBlock.Remarks);
            hashPara.Add("@IsActive", ObjMaterialBlock.IsActive);
            hashPara.Add("@UserId", ObjMaterialBlock.UserId);
            hashPara.Add("@UserIp", ObjMaterialBlock.IPAddress);

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