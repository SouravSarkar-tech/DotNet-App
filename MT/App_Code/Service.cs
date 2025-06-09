using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;
using System.Data;
using System.Xml.Linq;
using System.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    #region Check Functions
    public bool IsProfileNameExists(string profileName)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        string tableName = "Profile_Master";
        try
        {
            return objDal.IsDuplicate(tableName, "Profile_Name = '" + profileName + "'");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool IsUserNameExists(string userName)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        string tableName = "User_Master";
        try
        {
            return objDal.IsDuplicate(tableName, "UserName= '" + userName + "'");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool CheckLookUpHeaderValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM M_LookUp_Header WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpReceipeValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM M_LookUp_Receipe WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpCustomerValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM m_LookUp_Customer WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpVendorValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM m_LookUp_Vendor WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpPriceValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM m_LookUp_Price WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpResourceValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM m_LookUp_Resource WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpExciseValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM m_LookUp_Excise WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckLookUpBOMValue(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) LookUp_Code FROM m_LookUp_BOM WHERE Control_Name ='" + controlId + "' AND LookUp_Code = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    public bool CheckBankValue(string CountryId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (1) Swift from m_Bank_Master where Swift = '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND Country_Id = '" + CountryId + "' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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
    public bool CheckLookUpUserName(string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        try
        {
            objDal.OpenConnection();
            return objDal.ReturnBool("SELECT TOP (10) UserName FROM User_Master WHERE UserName like %'" + lookUpValue + "%' AND IsActive = 'TRUE'", ref objDal.cnnConnection);
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

    #endregion

    #region Autocomplete
    public List<string> AutoCompleteLookUpHeader(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT TOP 10 (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM M_LookUp_Header WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpReceipe(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT TOP 10 (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM M_LookUp_Receipe WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpCustomer(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT  TOP 10 (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_Customer WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpVendor(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            //string userId = System.Web.HttpContext.Current.Session[StaticKeys.LoggedIn_User_Id].ToString();
            //DataSet dstData = objDal.FillDataSet("SELECT  TOP 10  (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_Vendor WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            DataSet dstData = objDal.FillDataSet("SELECT (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_Vendor WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpPrice(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT  TOP 10  (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_Price WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpResource(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT  TOP 10  (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_Resource WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpExcise(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT  TOP 10  (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_Excise WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteLookUpBOM(string controlId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT  TOP 10  (LookUp_Code+ COALESCE('-'+LookUp_Desc,'')) AS LookUp_Desc FROM m_LookUp_BOM WHERE Control_Name ='" + controlId + "' AND LookUp_Code LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteBankIFSC(string CountryId, string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT  TOP 10  (Swift+ COALESCE('-'+Bank_Name + ' - ' + Bank_Branch,'')) AS LookUp_Desc from m_Bank_Master where Country_Id like '" + CountryId + "' AND Swift LIKE '" + Utility.RemoveSpecialChar(lookUpValue.Trim()) + "%' AND IsActive = 'TRUE' AND Bank_Key <> ''", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> GetMaterial(string strMaterial, string Flag)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("Select DISTINCT Material_Number from T_Mat_Basic_Data1 where Material_Number LIKE '" + Utility.RemoveSpecialChar(strMaterial.Trim()) + "%'", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["Material_Number"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }
    
    //BOM UOM Autocomplete
    public List<string> AutoCompleteMaterialUOM(string UOM, string materialNo, string plantCode)
    {
        BOMAccess objAccess = new BOMAccess();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objAccess.AutoCompleteMaterialUOM(UOM, materialNo, plantCode);
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["UOM"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteUOM(string UOM)
    {
        BOMAccess objAccess = new BOMAccess();
        List<string> list = new List<string>();
        try
        {
            //  DataSet dstData = objAccess.AutoCompleteUOM(UOM, materialNo, plantCode);
            DataSet dstData = objAccess.AutoCompleteUOM(UOM);
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["UOM"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    //Srinidhi
    public List<string> AutoCompleteVendorName(string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
        List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT top 10 SUBSTRING(vgt.Customer_Code, PATINDEX('%[^0 ]%', vgt.Customer_Code + ' '), LEN(vgt.Customer_Code)) + '-' + vg1.Name1 As LookUp_Desc from T_Vendor_General1 vg1 inner join T_Vendor_General_Type vgt on vg1.Master_Header_Id = vgt.Master_Header_Id where Name1 LIKE '" + lookUpValue + "%' and isnull(Customer_Code,'') <> '' and Customer_Code LIKE '000099%' and vgt.IsActive = 1", "table");
            foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["LookUp_Desc"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;
    }

    public List<string> AutoCompleteUserName(string lookUpValue)
    {
        DataAccessLayer objDal = new DataAccessLayer();
    List<string> list = new List<string>();
        try
        {
            DataSet dstData = objDal.FillDataSet("SELECT distinct TOP (10) UserName FROM User_Master WHERE UserName like '%" + lookUpValue + "%' AND IsActive = 'TRUE'", "table");
        foreach (DataRow row in dstData.Tables[0].Rows)
            {
                list.Add(row["UserName"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return list;

    }

    //public BOMDetail[] GetBOMDetail(string strMaterial)
    //{
    //    DataAccessLayer objDal = new DataAccessLayer();
    //    BOMDetail objBOMDetail = new BOMDetail();
    //    DataSet dstData = new DataSet();
    //    List<BOMDetail> list = new List<BOMDetail>();
    //    try
    //    {

    //        dstData = objDal.FillDataSet("Select Weight_Unit,  Base_Unit_Of_Measure, Base_Unit_Of_Measure from T_Mat_Basic_Data1 where Material_Number = '" + Utility.RemoveSpecialChar(strMaterial.Trim()) + "'", "table");
    //        foreach (DataRow dtrow in dstData.Tables[0].Rows)
    //        {
    //            BOMDetail objBOM = new BOMDetail();
    //            objBOM.Component_UOM = dtrow["Weight_Unit"].ToString();
    //            objBOM.Base_Quantity = dtrow["Base_Unit_Of_Measure"].ToString();
    //            objBOM.Base_Quantity_UOM = dtrow["Base_Unit_Of_Measure"].ToString();

    //            list.Add(objBOM);
    //        }
    //        return list.ToArray();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    #endregion

    public string ReadToolTip(string controlId)
    {
        ToolTip toolTip = new ToolTip();
        string retVal = toolTip.ReadToolTip(controlId);
        return retVal;
    }

    public BOMDetail[] GetBOMDetail(string strMaterial)
    {
        throw new NotImplementedException();
    }
}
