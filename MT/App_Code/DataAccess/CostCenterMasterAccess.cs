using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for CostCenterMasterAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class CostCenterMasterAccess
    {
        public CostCenterMasterAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public CostCenterCreate GetCostCenterMasterData(int MasterHeaderId)
        {
            CostCenterCreate ObjCostCenterMaster = new CostCenterCreate();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_CostCenterMaster_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", MasterHeaderId);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjCostCenterMaster.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                        ObjCostCenterMaster.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjCostCenterMaster.Cost_Center = dt.Rows[0]["CostCenter"].ToString();
                        ObjCostCenterMaster.Ref_Cost_Center = dt.Rows[0]["Ref_Cost_Center"].ToString();
                        ObjCostCenterMaster.ValidFrom = dt.Rows[0]["ValidFrom"].ToString();
                        ObjCostCenterMaster.ValidTo = dt.Rows[0]["ValidTo"].ToString();
                        ObjCostCenterMaster.ControllingArea = dt.Rows[0]["ControllingArea"].ToString();
                        ObjCostCenterMaster.Cost_Center_Name = dt.Rows[0]["Cost_Center_Name"].ToString();
                        ObjCostCenterMaster.Cost_Center_Desc = dt.Rows[0]["Cost_Center_Desc"].ToString();
                        ObjCostCenterMaster.User_Responsible = dt.Rows[0]["User_Responsible"].ToString();
                        ObjCostCenterMaster.Person_Responsible = dt.Rows[0]["Person_Responsible"].ToString();
                        ObjCostCenterMaster.Department = dt.Rows[0]["Department"].ToString();
                        ObjCostCenterMaster.Cost_Center_Category = dt.Rows[0]["Cost_Center_Category"].ToString();
                        ObjCostCenterMaster.Hierarchy_Area = dt.Rows[0]["Hierarchy_Area"].ToString();
                        ObjCostCenterMaster.Company_Code = dt.Rows[0]["Company_Code"].ToString();
                        ObjCostCenterMaster.Business_Area = dt.Rows[0]["Business_Area"].ToString();
                        ObjCostCenterMaster.Profit_Center = dt.Rows[0]["Profit_Center"].ToString();
                    }
                }
                return ObjCostCenterMaster;
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

        public int Save(CostCenterCreate ObjCostCenterMaster)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_CostCenterMaster_Create";
            int result = 0;


            hashPara.Add("@ID", ObjCostCenterMaster.ID);
            hashPara.Add("@Master_Header_Id", ObjCostCenterMaster.Master_Header_Id);

            hashPara.Add("@Cost_Center", ObjCostCenterMaster.Cost_Center);
            hashPara.Add("@Ref_Cost_Center", ObjCostCenterMaster.Ref_Cost_Center);
            hashPara.Add("@Company_Code", ObjCostCenterMaster.Company_Code);
            //hashPara.Add("@ValidFrom", ObjCostCenterMaster.ValidFrom);
            //hashPara.Add("@ValidTo", ObjCostCenterMaster.ValidTo);
            hashPara.Add("@ControllingArea", ObjCostCenterMaster.ControllingArea);

            hashPara.Add("@Cost_Center_Name", ObjCostCenterMaster.Cost_Center_Name);
            hashPara.Add("@Cost_Center_Desc", ObjCostCenterMaster.Cost_Center_Desc);
            hashPara.Add("@User_Responsible", ObjCostCenterMaster.User_Responsible);
            hashPara.Add("@Person_Responsible", ObjCostCenterMaster.Person_Responsible);
            hashPara.Add("@Department", ObjCostCenterMaster.Department);
            hashPara.Add("@Cost_Center_Category", ObjCostCenterMaster.Cost_Center_Category);
            hashPara.Add("@Hierarchy_Area", ObjCostCenterMaster.Hierarchy_Area);
            hashPara.Add("@Business_Area", ObjCostCenterMaster.Business_Area);
            hashPara.Add("@Profit_Center", ObjCostCenterMaster.Profit_Center);

            hashPara.Add("@CreatedBy", ObjCostCenterMaster.UserId);
            hashPara.Add("@CreatedIP", ObjCostCenterMaster.IPAddress);

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

        //public DataSet CheckIfValid(string Master_Header_Id)
        //{
        //    DataAccessLayer objDal = new DataAccessLayer();
        //    Hashtable hashPara = new Hashtable();

        //    string procName = "pr_Check_CostCenterCode_Exist";
        //    hashPara.Add("@Master_Header_Id", Master_Header_Id);
        //    return objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
        //}

        public DataTable GetRequestNoByMasterHeaderId(string Master_Header_Id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetRequestNoByMasterHeaderId";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds.Tables[0];
        }

        public DataSet GetCostCenterMasterDataForValidation(string Master_Header_Id)
        {
            DataSet ds;
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetCostCenterMasterDataForValidation";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

            return ds;
        }
    }
}