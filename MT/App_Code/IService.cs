using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Accenture.MWT.DataAccess;
using Accenture.MWT.DomainObject;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    #region Check Functions
    [OperationContract]
    bool IsProfileNameExists(string profileName);

    [OperationContract]
    bool IsUserNameExists(string userName);

    [OperationContract]
    bool CheckLookUpHeaderValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpReceipeValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpCustomerValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpVendorValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpPriceValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpResourceValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpExciseValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckLookUpBOMValue(string controlId, string lookUpValue);

    [OperationContract]
    bool CheckBankValue(string CountryId, string lookUpValue);

    #endregion

    #region Autocomplete
    [OperationContract]
    List<string> AutoCompleteLookUpHeader(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpReceipe(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpCustomer(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpVendor(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpResource(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpExcise(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpPrice(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteLookUpBOM(string controlId, string lookUpValue);

    [OperationContract]
    List<string> AutoCompleteBankIFSC(string CountryId, string lookUpValue);

    //Srinidhi
    [OperationContract]
    List<string> AutoCompleteVendorName(string lookUpValue);
    //end

    //Auto complete BOM component UOM 
    [OperationContract]
    List<string> AutoCompleteMaterialUOM(string UOM, string materialNo, string plantCode);

    [OperationContract]
    List<string> AutoCompleteUOM(string UOM);

    [OperationContract]
    List<string> GetMaterial(string strMaterial, string Flag);

    [OperationContract]
    BOMDetail[] GetBOMDetail(string strMaterial);
    //Swati
    [OperationContract]
    List<string> AutoCompleteUserName(string lookUpValue);
    #endregion

    [OperationContract]
    string ReadToolTip(string controlId);
}
