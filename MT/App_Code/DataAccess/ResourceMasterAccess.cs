using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for ResourceMasterAccess
/// </summary>
namespace Accenture.MWT.DataAccess
{
    public class ResourceMasterAccess
    {
        public ResourceMasterAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Save(ResourceMaster ObjRes)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Ins_Upd_T_Resource_Master";
            int result = 0;


            hashPara.Add("@Resource_Master_Id", ObjRes.Resource_Master_Id);
            hashPara.Add("@Master_Header_Id", ObjRes.Master_Header_Id);
            hashPara.Add("@Plant_Id", ObjRes.Plant_Id);
            hashPara.Add("@Resource", ObjRes.Resource);
            hashPara.Add("@Object_Name", ObjRes.Object_Name);
            hashPara.Add("@Person_Resp_WorkCenter", ObjRes.Person_Resp_WorkCenter);
            hashPara.Add("@Standard_Value_Key", ObjRes.Standard_Value_Key);
            hashPara.Add("@Unit_Of_Measure_Std_Value", ObjRes.Unit_Of_Measure_Std_Value);
            hashPara.Add("@Unit_Of_Measure_Std_Value2", ObjRes.Unit_Of_Measure_Std_Value2);
            hashPara.Add("@Formula_Cap_Req_Int_Process", ObjRes.Formula_Cap_Req_Int_Process);
            hashPara.Add("@Capacity_Short_Text", ObjRes.Capacity_Short_Text);
            hashPara.Add("@Capacity_Planner_Group", ObjRes.Capacity_Planner_Group);
            hashPara.Add("@Start_Time", ObjRes.Start_Time);
            hashPara.Add("@Finish_Time", ObjRes.Finish_Time);
            hashPara.Add("@Capacity_Utilization_Rate", ObjRes.Capacity_Utilization_Rate);
            hashPara.Add("@Cumulative_Len_Break_Per_Shift", ObjRes.Cumulative_Len_Break_Per_Shift);
            hashPara.Add("@Number_Of_Ind_Cap", ObjRes.Number_Of_Ind_Cap);
            hashPara.Add("@Base_UOM_Capacity", ObjRes.Base_UOM_Capacity);
            hashPara.Add("@Formula_Duration_Int_Process", ObjRes.Formula_Duration_Int_Process);
            hashPara.Add("@Start_Date", ObjRes.Start_Date);
            hashPara.Add("@End_Date", ObjRes.End_Date);
            hashPara.Add("@Cost_Center", ObjRes.Cost_Center);
            hashPara.Add("@Activity_Type", ObjRes.Activity_Type);
            hashPara.Add("@Activity_Type2", ObjRes.Activity_Type2);
            hashPara.Add("@Activity_Unit", ObjRes.Activity_Unit);
            hashPara.Add("@Activity_Unit2", ObjRes.Activity_Unit2);
            hashPara.Add("@Formula_Key", ObjRes.Formula_Key);
            hashPara.Add("@Formula_Key2", ObjRes.Formula_Key2);
            hashPara.Add("@IsActive", ObjRes.IsActive);
            hashPara.Add("@UserId", ObjRes.UserId);
            hashPara.Add("@UserIp", ObjRes.IPAddress);

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

        public ResourceMaster GetResourceMaster(int Master_Header_Id)
        {
            ResourceMaster ObjRes = new ResourceMaster();

            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_Get_Resource_Master_By_MasterHeaderId";
            DataSet ds;

            hashPara.Add("@Master_Header_Id", Master_Header_Id);

            try
            {
                ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ObjRes.Resource_Master_Id = Convert.ToInt32(dt.Rows[0]["Resource_Master_Id"].ToString());
                        ObjRes.Master_Header_Id = Convert.ToInt32(dt.Rows[0]["Master_Header_Id"].ToString());
                        ObjRes.Plant_Id = dt.Rows[0]["Plant_Id"].ToString();
                        ObjRes.Resource = dt.Rows[0]["Resource"].ToString();
                        ObjRes.Object_Name = dt.Rows[0]["Object_Name"].ToString();
                        ObjRes.Person_Resp_WorkCenter = dt.Rows[0]["Person_Resp_WorkCenter"].ToString();
                        ObjRes.Standard_Value_Key = dt.Rows[0]["Standard_Value_Key"].ToString();
                        ObjRes.Unit_Of_Measure_Std_Value = dt.Rows[0]["Unit_Of_Measure_Std_Value"].ToString();
                        ObjRes.Unit_Of_Measure_Std_Value2 = dt.Rows[0]["Unit_Of_Measure_Std_Value2"].ToString();
                        ObjRes.Formula_Cap_Req_Int_Process = dt.Rows[0]["Formula_Cap_Req_Int_Process"].ToString();
                        ObjRes.Capacity_Short_Text = dt.Rows[0]["Capacity_Short_Text"].ToString();
                        ObjRes.Capacity_Planner_Group = dt.Rows[0]["Capacity_Planner_Group"].ToString();
                        ObjRes.Start_Time = dt.Rows[0]["Start_Time"].ToString();
                        ObjRes.Finish_Time = dt.Rows[0]["Finish_Time"].ToString();
                        ObjRes.Capacity_Utilization_Rate = Convert.ToInt32(dt.Rows[0]["Capacity_Utilization_Rate"].ToString());
                        ObjRes.Cumulative_Len_Break_Per_Shift = dt.Rows[0]["Cumulative_Len_Break_Per_Shift"].ToString();
                        ObjRes.Number_Of_Ind_Cap = dt.Rows[0]["Number_Of_Ind_Cap"].ToString();
                        ObjRes.Base_UOM_Capacity = dt.Rows[0]["Base_UOM_Capacity"].ToString();
                        ObjRes.Formula_Duration_Int_Process = dt.Rows[0]["Formula_Duration_Int_Process"].ToString();
                        ObjRes.Start_Date = dt.Rows[0]["Start_Date"].ToString();
                        ObjRes.End_Date = dt.Rows[0]["End_Date"].ToString();
                        ObjRes.Cost_Center = dt.Rows[0]["Cost_Center"].ToString();
                        ObjRes.Activity_Type = dt.Rows[0]["Activity_Type"].ToString();
                        ObjRes.Activity_Type2 = dt.Rows[0]["Activity_Type2"].ToString();
                        ObjRes.Activity_Unit = dt.Rows[0]["Activity_Unit"].ToString();
                        ObjRes.Activity_Unit2 = dt.Rows[0]["Activity_Unit2"].ToString();
                        ObjRes.Formula_Key = dt.Rows[0]["Formula_Key"].ToString();
                        ObjRes.Formula_Key2 = dt.Rows[0]["Formula_Key2"].ToString();
                    }
                }
                return ObjRes;
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