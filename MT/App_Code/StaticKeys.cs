using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class StaticKeys
{
    public static string LoggedIn_User_Id = "LoggedInUserID";
    public static string LoggedIn_User_Name = "LoggedInUserName";
    public static string LoggedIn_User_FullName = "LoggedInUserFullName";
    public static string LoggedIn_User_LastLogin = "LoggedInUserLastLogin";
    public static string LoggedIn_User_Profile_Id = "LoggedInProfileId";
    public static string LoggedIn_User_Profile = "LoggedInProfile";
    public static string LoggedIn_User_Location = "LoggedInLocation";
    public static string LoggedIn_User_ContactNo = "LoggedInContactNo";
    public static string LoggedIn_User_DeptId = "LoggedInDeptId";
    public static string LoggedIn_User_DeptName = "LoggedInDeptName";
    public static string QueryStringMenuId = "pg";
    public static string SelectedModuleId = "SelectedModuleId";
    public static string MaterialProcessModuleId = "MaterialProcessModuleId";
    public static string SelectedModule = "SelectedModule";
    public static string MasterHeaderId = "MasterHeaderId";
    public static string MaterialNo = "MaterialNo";
    public static string MaterialType = "MaterialType";
    public static string MaterialPlantId = "MaterialPlantId";
    public static string MaterialSalesOrgId = "MaterialSalesOrgId";
    public static string MaterialDistChnlId = "MaterialDistChnlId";
    public static string MatStorageLocationId = "MaterialStorageLocationId";
    public static string MatPurchasingGroupId = "MaterialPurchasingGroupId";
    public static string MaterialPlantName = "MaterialPlantName";
    public static string MatStorageLocationName = "MatStorageLocationName";
    public static string SelectedModulePlantGrp = "SelectedModulePlantGrp";
    public static string MatPlantGrp = "MatPlantGrp";
    public static string Mode = "Mode";
    public static string ActionType = "ActionType";
    public static string RequestNo = "RequestNo";
    public static string MassRequestProcessId = "MassRequestProcessId";
    public static string PageSequence = "PageSequence";
    public static string SectionId = "SectionId";
    public static string IsLocationReq = "IsLocationReq";
    public static string Requestor_User_Name = "RequestorUserName";
    public static string Requestor_Location = "RequestorLocation";
    public static string Requestor_ContactNo = "RequestorContactNo";
    public static string Requestor_DeptName = "RequestorDeptName";
    public static string AddAlertMsg = "AddAlertMsg";
    public static string SearchStatus = "SearchStatus";
    public static string RoleCount = "RoleCount";
    public static string ApprovalNote = "Approval Note";
    public static string SelectedDivision = "SelectedDivision";
    public static string RequestStatus = "RequestStatus";
    public static string PendingFor = "PendingFor";

    public static string RptPurchGrp = "RptPurchGrp";
    public static string RptPlant = "RptPlant";
    public static string RptModule = "RptModule";
    public static string RptToDate = "RptToDate";
    public static string RptFromDate = "RptFromDate";
    public static string RptPendingDays = "RptPendingDays";
    public static string RptStatus = "RptStatus";
    public static string RptApprDept = "RptApprDept";
    public static string RptCreatedBy = "RptCreatedBy";

    public static string MatTypeSelected = "MatTypeSelected";
    public static string MarketType = "MarketType";

    //BOM recipe static values
    public static string PlantType = "PlantType";
    public static string BOMRecipeMatNo = "BOMRecipeMatNo";
    public static string BOMRecipeMatDesc = "BOMRecipeMatDesc";
    public static string BOMRecipeBUOM = "BOMRecipeBUOM";
    public static string BOMRecipeTo = "BOMRecipeTo";
    public static string BOMRecipeFrom = "BOMRecipeFrom";
    public static string BOMRecipeAltBOM = "BOMRecipeAltBOM";
    public static string BOMRecipeBOMUsage = "BOMRecipeBOMUsage";
    public static string BOMRecipeBaseQty = "BOMRecipeBaseQty";
    //BOM_8200050878 for new Module created 227,228,229
    //manali chavan
    public static string RecipeGroup = "RecipeGroup";
    public static string ProdVersion = "ProdVersion";

    public static string SAPUserName = "SAPUserName";
    public static string SAPPassword = "SAPPassword";

    //Email Redirection Start
    public static string MstrType = "MstrType";
    //Email Redirection End

    //Added Nitish Rao 30.05.2018
    public static string ReciepeID = "ReciepeID";
    //End Nitish Rao 30.05.2018
    public static string Moduletype = "Moduletype";
    public static string sapcode = "sapcode";

    public static string EXTDASIRF = "EXTDASIRF";
    public static string CREDASIRF = "CREDASIRF";
    public static string CHNDASIRF = "CHNDASIRF";
    //STD07052019 add div sestion for select defualt div in Sales
    public static string DivCusts = "DivCusts";
    public static string DivTypeCusts = "DivTypeCusts";
    //ETD07052019 add div sestion for select defualt div in Sales


    // Created By Manali SDT31052019
    public static string RptZone = "RptZone";
    public static string RptDivision = "RptDivision";
    public static string RptTerritory = "RptTerritory";
    public static string RptBusinessArea = "RptBusinessArea";
    public static string RptMaterialCode = "RptMaterialCode";
    // Created By Manali SDT31052019 

    //PROSOL_SDT16092019
    public static string LoggedIn_User_Pass = "LoggedIn_User_Pass";
    public static string LoggedIn_User_Prosol = "LoggedIn_User_Prosol";
    public static string MWTRequestNo_Prosol = "MWTRequestNo_Prosol";

    //PROSOL_SDT16092019

    //CTRL_SUB_SDT06062019
    public static string ctrlsubfieldval = "ctrlsubfieldval";
    //CTRL_SUB_EDT06062019

    //MSC_8300001775 Start
    public static string mco = "mco";
    public static string mty = "mty";
    public static string pla = "pla";
    public static string stg = "stg";
    public static string pog = "pog";
    public static string sal = "sal";
    public static string dch = "dch";
    public static string sec = "sec";
    public static string TypeOfMassUpdm = "TypeOfMassUpdm";
    //MSC_8300001775 End

    //LH01DT06052021 Start
    public static string SelectedddlCompany = "SelectedddlCompany";
    //LH01DT06052021 End

    //MSE_8300002156 Start
    public static string extmco = "extmco";
    public static string extmty = "extmty";
    public static string extpla = "extpla";
    public static string extstg = "extstg";
    public static string extpog = "extpog";
    public static string extsal = "extsal";
    public static string extdch = "extdch";
    public static string extsec = "extsec";
    public static string extvaltyp = "extvaltyp";
    public static string extrefpla = "extrefpla";
    public static string extrefstg = "extrefstg";
    //MSE_8300002156 End


    public static string fileName = "fileName";
    public static string UserNameSearch = "UserNameSearch";
    public static string rtpfromdate = "rtpfromdate";
    public static string rtptodate = "rtptodate";

    public static string ReqStatus = "ReqStatus";
    public static string ProfitCenterName = "ProfitCenterName";
    //8400000250 START
    public static string ModuleZCAP = "ModuleZCAP";
    public static string ModuleHSN = "ModuleHSN";
    public static string ModuleZHG = "ModuleZHG";
    //8400000250 END
    public static string ssouid = "ssouid";
}