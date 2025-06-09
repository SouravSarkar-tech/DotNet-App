using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialHelper
/// </summary>
public class MaterialHelper
{
    public MaterialHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetMaterialAccGrpByMaterialCode(string MaterialCode)
    {
        string MaterialAccGrp = "";

        int strcode = SafeTypeHandling.ConvertStringToInt32(MaterialCode);

        if (strcode >= 100000 && strcode < 199999) //ROH 1- Series
        { 
            MaterialAccGrp = "162";
        }
        else if (strcode >= 200000 && strcode < 299999)//VERP  2- Series
        { 
            MaterialAccGrp = "164";
        }
        else if (strcode >= 300000 && strcode < 399999)//HALB  3- Series
        {  
            MaterialAccGrp = "144";
        }
        else if (strcode >= 400000 && strcode < 499999)//FERT  4- Series
        {
            MaterialAccGrp = "139";
        }
        else if (strcode >= 500000 && strcode < 599999)//HAWA  5- Series
        { 
            MaterialAccGrp = "145";
        }
        else if ((strcode >= 600000 && strcode < 699999) || (strcode >= 6600000 && strcode < 6699999) 
            || (strcode >= 6700000 && strcode < 6799999))//ERSA  6- Series
        { 
            MaterialAccGrp = "138";
        }
        else if ((strcode >= 700000 && strcode < 799999) || (strcode >= 7700000 && strcode < 7799999))//HIBE  7- Series
        { 
            MaterialAccGrp = "147";
        }
        else if (strcode >= 800000 && strcode < 849999)//ZNBW  8- Series
        { 
            MaterialAccGrp = "171";
        }
        else if (strcode >= 850000 && strcode < 889999)//UNBW 85- Series
        { 
            MaterialAccGrp = "163";
        }
        else if ((strcode >= 890001 && strcode < 929999) || (strcode >= 930000 && strcode < 959999)
            || (strcode >= 960000 && strcode < 989999))//ZMBW  89/9- Series
        { 
            MaterialAccGrp = "170";
        }
        return MaterialAccGrp;
    }

    //GL Master Acc Group Addition by Swati
    public static string GetGLAccGrpByMaterialCode(string MaterialCode)
    {
        string GLAccGrp = "";

        int strcode = SafeTypeHandling.ConvertStringToInt32(MaterialCode);
        //SDT17052019 Change By NR , Desc : Get page path  from web config
        if (strcode >= 231001 && strcode < 299999)//ASET
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleASET"]); // "201";
        else if (strcode >= 140000 && strcode < 145999)//BNKL
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleBNKL"]); // "202";
        else if (strcode >= 221000 && strcode < 230999)//CASH
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleCASH"]); // "203";
        else if (strcode >= 400000 && strcode < 410999)//CONS
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleCONS"]); // "204";
        else if (strcode >= 199001 && strcode < 199999)//DEPN
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleDEPN"]); // "205";
        else if (strcode >= 411000 && strcode < 499999)//EXPN
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleEXPN"]); // "206";
        else if (strcode >= 200000 && strcode < 204999)//FXAS
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleFXAS"]); // "207";
        else if (strcode >= 100000 && strcode < 139999)//LIAB
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleLIAB"]); // "208";
        else if (strcode >= 205000 && strcode < 209999)//MATL
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleMATL"]); // "209";
        else if (strcode >= 900000 && strcode < 999999)//MISC
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleMISC"]); // "210";
        else if (strcode >= 146000 && strcode < 150999)//PABL
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModulePABL"]); // "211";
        else if (strcode >= 210000 && strcode < 220999)//RCBL
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleRCBL"]); //"212";
        else if (strcode >= 300000 && strcode < 399999)//REVN
            GLAccGrp = Convert.ToString(ConfigurationManager.AppSettings["ModuleREVN"]); //"213";
        //EDT17052019 Change By NR , Desc : Get page path  from web config
        return GLAccGrp;
    }
    //End

    public static string GetAccountCategoryByModuleId(string ModuleId)
    {
        string AccountCategory = "";
        switch (ModuleId)
        {
            case "162"://ROH 1- Series
                AccountCategory = "0001";
                break;
            case "164"://VERP  2- Series
                AccountCategory = "0004";
                break;
            case "144"://HALB  3- Series
                AccountCategory = "0001";
                break;
            case "139"://FERT  4- Series
                AccountCategory = "0001";
                break;
            case "145"://HAWA  5- Series
                AccountCategory = "0005";
                break;
            case "138"://ERSA  6- Series
                AccountCategory = "0003";
                break;
            case "147"://HIBE  7- Series
                AccountCategory = "0002";
                break;
            case "171"://ZNBW  8- Series
                AccountCategory = "0007";
                break;
            case "163"://UNBW 85- Series
                AccountCategory = "0007";
                break;
            case "170"://ZMBW  89/9- Series
                AccountCategory = "0007";
                break;
            //Promotion code start
            case "195"://PROM 8-series
                AccountCategory = "0007";
                break;
            //Promotion code End
            default:
                AccountCategory = "";
                break;
        }

        return AccountCategory;
    }

    public static string GetDefaultValuationClassByModuleId(string ModuleId)
    {
        string DefaultValuationClass = "";

        switch (ModuleId)
        {
            case "162"://ROH 1- Series
                DefaultValuationClass = "";
                break;
            case "164"://VERP  2- Series
                DefaultValuationClass = "";
                break;
            case "144"://HALB  3- Series
                DefaultValuationClass = "";
                break;
            case "139"://FERT  4- Series
                DefaultValuationClass = "";
                break;
            case "145"://HAWA  5- Series
                DefaultValuationClass = "";
                break;
            case "138"://ERSA  6- Series
                DefaultValuationClass = "6030";
                break;
            case "147"://HIBE  7- Series
                DefaultValuationClass = "4010";
                break;
            case "171"://ZNBW  8- Series
                DefaultValuationClass = "";
                break;
            case "163"://UNBW 85- Series
                DefaultValuationClass = "";
                break;
            case "170"://ZMBW  89/9- Series
                DefaultValuationClass = "7060";
                break;
            default:
                DefaultValuationClass = "";
                break;
        }

        return DefaultValuationClass;
    }
}