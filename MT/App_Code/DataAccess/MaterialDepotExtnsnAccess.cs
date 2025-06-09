using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Accenture.MWT.DataAccess;
using System.Collections;
using Accenture.MWT.DomainObject;

/// <summary>
/// Summary description for MaterialDepotExtnsnAccess
/// </summary>
public class MaterialDepotExtnsnAccess
{
    public MaterialDepotExtnsnAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet GetMaterialDepotExtnsnData(string masterHeaderId, string userId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_DepotExtnsnData";
        DataSet ds;

        hashPara.Add("@Master_Header_Id", masterHeaderId);
        hashPara.Add("@User_Id", userId);

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

    public int SaveImport(MatDepotExtension objDeptExtnsn)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Ins_Upd_T_Mat_Extnsn_Data";
        int result = 0;


        hashPara.Add("@Mat_Extnsn_Data_Id", objDeptExtnsn.Mat_Extnsn_Data_Id);
        hashPara.Add("@Master_Header_Id", objDeptExtnsn.Master_Header_Id);
        hashPara.Add("@Plant_Group", objDeptExtnsn.Plant_Group);
        hashPara.Add("@Plant_Id", objDeptExtnsn.Plant_Id);
        hashPara.Add("@Purchasing_Group", objDeptExtnsn.Purchasing_Group);
        hashPara.Add("@MRP_Type", objDeptExtnsn.MRP_Type);
        hashPara.Add("@MRP_Controller", objDeptExtnsn.MRP_Controller);
        hashPara.Add("@Reorder_Point", objDeptExtnsn.Reorder_Point);
        hashPara.Add("@Lot_Size", objDeptExtnsn.Lot_Size);
        hashPara.Add("@Fixed_Lot_Size", objDeptExtnsn.Fixed_Lot_Size);
        hashPara.Add("@Rounding_Value", objDeptExtnsn.Rounding_Value);
        hashPara.Add("@Old_Material_Number", objDeptExtnsn.Old_Material_Number);
        hashPara.Add("@Range_Coverage_Profile", objDeptExtnsn.Range_Coverage_Profile);
        hashPara.Add("@Procurement_Type", objDeptExtnsn.Procurement_Type);
        hashPara.Add("@Safety_Time_WorkDays", objDeptExtnsn.Safety_Time_WorkDays);
        hashPara.Add("@Planned_Delivery_Time_Days", objDeptExtnsn.Planned_Delivery_Time_Days);
        hashPara.Add("@GR_Processing_Time", objDeptExtnsn.GR_Processing_Time);
        hashPara.Add("@Spl_Procurement_Type", objDeptExtnsn.Spl_Procurement_Type);
        hashPara.Add("@Fair_Share_Rule", objDeptExtnsn.Fair_Share_Rule);
        hashPara.Add("@Indi_Push_Distribution", objDeptExtnsn.Indi_Push_Distribution);
        hashPara.Add("@Loading_Group", objDeptExtnsn.Loading_Group);

        hashPara.Add("@IsActive", objDeptExtnsn.IsActive);
        hashPara.Add("@UserId", objDeptExtnsn.UserId);
        hashPara.Add("@UserIp", objDeptExtnsn.IPAddress);

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

    public DataSet GetMaterialDepotExtnsnLSMWData(string masterHeaderId, string userId)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        Hashtable hashPara = new Hashtable();
        string procName = "pr_Get_Material_DepotExtnsnLSMWData";
        DataSet ds;

        hashPara.Add("@Master_Header_Id", masterHeaderId);
        hashPara.Add("@User_Id", userId);

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