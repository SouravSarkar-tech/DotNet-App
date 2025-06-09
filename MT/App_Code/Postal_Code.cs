using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Class_Postal_Code

public class Postal_Code
{

    public static string GetRegex(string str)
    {
        string str1 = str.Substring(0, 1);
        string str2 = str.Length > 2 ? str.Substring(0, 2) : str;

        switch (str1)
        {
            case "1":
                switch (str2)
                {
                    case "1":
                        switch (str)
                        {
                            case "1":
                                return "^[0-9]{06}$";
                        }
                        return "";
                    case "10":
                        switch (str)
                        {
                            case "10":
                                return "^[\\S]{0,10}";
                            case "100":
                                return "^[\\S]{0,10}";
                            case "101":
                                return "^[0-9]{05}$";
                            case "102":
                                return "^[\\S]{0,10}";
                            case "103":
                                return "^[\\S]{0,10}";
                            case "104":
                                return "^[\\S]{0,10}";
                            case "105":
                                return "^[0-9]{05}$";
                            case "106":
                                return "^[0-9]{03}$";
                            case "107":
                                return "^[0-9]{05}$";
                            case "108":
                                return "^[\\S]{0,10}";
                            case "109":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "11":
                        switch (str)
                        {
                            case "11":
                                return "^[\\S]{0,10}";
                            case "110":
                                return "^[0-9]{07}$";
                            case "111":
                                return "^[\\S]{0,10}";
                            case "112":
                                return "^[\\S]{0,10}";
                            case "113":
                                return "^[\\S]{0,10}";
                            case "114":
                                return "^[\\S]{0,10}";
                            case "115":
                                return "^[\\S]{0,10}";
                            case "116":
                                return "^[\\S]{0,10}";
                            case "117":
                                return "^[\\S]{0,07}$";
                            case "118":
                                //Code added and commented by Swati since postal code for Korea has changed from 6 to 5 digit 

                                //return "^[0-9]{3,3}-[0-9]{3,3}";
                                return "^[0-9]{5}";

                                //End Change
                            case "119":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "12":
                        switch (str)
                        {
                            case "12":
                                return "^[\\S]{0,10}";
                            case "120":
                                return "^[\\S]{0,10}";
                            case "121":
                                return "^[0-9]{06}$";
                            case "122":
                                return "^[\\S]{0,10}";
                            case "123":
                                return "^[\\S]{0,10}";
                            case "124":
                                return "^[\\S]{0,10}";
                            case "125":
                                return "^[0-9]{04}$";
                            case "126":
                                return "^[\\S]{0,10}";
                            case "127":
                                return "^[\\S]{0,10}";
                            case "128":
                                return "^[0-9]{03}$";
                            case "129":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "13":
                        switch (str)
                        {
                            case "13":
                                return "^[0-9]{04}$";
                            case "130":
                                return "^[0-9]{04}$";
                            case "131":
                                return "^[\\S]{0,10}";
                            case "132":
                                return "^[\\S]{0,10}";
                            case "133":
                                return "^[\\S]{0,10}";
                            case "134":
                                return "^[0-9]{05}$";
                            case "135":
                                return "^[\\S]{0,10}";
                            case "136":
                                return "^[0-9]{05}$";
                            case "137":
                                return "^[\\S]{0,10}";
                            case "138":
                                return "^[\\S]{0,10}";
                            case "139":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "14":
                        switch (str)
                        {
                            case "14":
                                return "^[\\S]{0,10}";
                            case "140":
                                return "^[\\S]{0,10}";
                            case "141":
                                return "^[\\S]{0,10}";
                            case "142":
                                return "^[\\S]{0,10}";
                            case "143":
                                return "^[\\S]{0,10}";
                            case "144":
                                return "^[\\S]{0,10}";
                            case "145":
                                return "^[\\S]{0,10}";
                            case "146":
                                return "^[\\S]{0,10}";
                            case "147":
                                return "^[\\S]{0,10}";
                            case "148":
                                return "^[\\S]{0,10}";
                            case "149":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "15":
                        switch (str)
                        {
                            case "15":
                                return "^[0-9]{04}$";
                            case "150":
                                return "^[\\S]{0,10}";
                            case "151":
                                return "^[\\S]{0,10}";
                            case "152":
                                return "^[0-9]{05}$";
                            case "153":
                                return "^[0-9]{05}$";
                            case "154":
                                return "^[\\S]{0,10}";
                            case "155":
                                return "^[\\S]{0,10}";
                            case "156":
                                return "^[\\S]{0,10}";
                            case "157":
                                return "^[\\S]{0,10}";
                            case "158":
                                return "^[\\S]{0,10}";
                            case "159":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "16":
                        switch (str)
                        {
                            case "16":
                                return "^[0-9]{0,07}$";
                            case "160":
                                return "^[\\S]{0,10}";
                            case "161":
                                return "^[0-9]{4,4}\\s[a-zA-Z]{2,2}";
                            case "162":
                                return "^[0-9]{04}$";
                            case "163":
                                return "^[0-9]{06}$";
                            case "164":
                                return "^[\\S]{0,10}";
                            case "165":
                                return "^[\\S]{0,10}";
                            case "166":
                                return "^[0-9]{04}$";
                            case "167":
                                return "^[\\S]{0,10}";
                            case "168":
                                return "^[\\S]{0,10}";
                            case "169":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "17":
                        switch (str)
                        {
                            case "17":
                                return "^[\\S]{0,10}";
                            case "170":
                                return "^[\\S]{0,10}";
                            case "171":
                                return "^[\\S]{0,10}";
                            case "172":
                                return "^[0-9]{04}$";
                            case "173":
                                return "^[\\S]{0,10}";
                            case "174":
                                return "^[0-9]{2,2}-[0-9]{3,3}";
                            case "175":
                                return "^[\\S]{0,10}";
                            case "176":
                                return "^[\\S]{0,10}";
                            case "177":
                                return "^[\\S]{0,10}";
                            case "178":
                                return "^[0-9]{04}$";
                            case "179":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "18":
                        switch (str)
                        {
                            case "18":
                                return "^[\\S]{0,10}";
                            case "180":
                                return "^[\\S]{0,10}";
                            case "181":
                                return "^[\\S]{0,10}";
                            case "182":
                                return "^[\\S]{0,10}";
                            case "183":
                                return "^[\\S]{0,10}";
                            case "184":
                                return "^[0-9]{07}$";
                            case "185":
                                return "^[0-9]{05}$";
                            case "186":
                                return "^[0-9]{06}$";
                            case "187":
                                return "^[\\S]{0,10}";
                            case "188":
                                return "^[0-9]{05}$";
                            case "189":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "19":
                        switch (str)
                        {
                            case "19":
                                return "^[\\S]{0,10}";
                            case "190":
                                return "^[\\S]{0,10}";
                            case "191":
                                return "^[\\S]{0,10}";
                            case "192":
                                return "^[0-9]{3,3}\\s[0-9]{2,2}";
                            case "193":
                                return "^[0-9]{0,06}$";
                            case "194":
                                return "^[\\S]{0,10}";
                            case "195":
                                return "^[0-9]{04}$";
                            case "196":
                                return "^[\\S]{0,10}";
                            case "197":
                                return "^[0-9]{3,3}\\s[0-9]{2,2}";
                            case "198":
                                return "^[\\S]{0,10}";
                            case "199":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "2":
                switch (str2)
                {
                    case "20":
                        switch (str)
                        {
                            case "20":
                                return "^[\\S]{0,10}";
                            case "200":
                                return "^[\\S]{0,10}";
                            case "201":
                                return "^[\\S]{0,10}";
                            case "202":
                                return "^[\\S]{0,10}";
                            case "203":
                                return "^[\\S]{0,10}";
                            case "204":
                                return "^[\\S]{0,10}";
                            case "205":
                                return "^[\\S]{0,10}";
                            case "206":
                                return "^[\\S]{0,10}";
                            case "207":
                                return "^[\\S]{0,10}";
                            case "208":
                                return "^[\\S]{0,10}";
                            case "209":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "21":
                        switch (str)
                        {
                            case "21":
                                return "^[\\S]{0,10}";
                            case "210":
                                return "^[\\S]{0,10}";
                            case "211":
                                return "^[0-9]{05}$";
                            case "212":
                                return "^[\\S]{0,10}";
                            case "213":
                                return "^[\\S]{0,10}";
                            case "214":
                                return "^[\\S]{0,10}";
                            case "215":
                                return "^[0-9]{04}$";
                            case "216":
                                return "^[\\S]{0,10}";
                            case "217":
                                return "^[\\S]{0,10}";
                            case "218":
                                return "^[0-9]{05}$";
                            case "219":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "22":
                        switch (str)
                        {
                            case "22":
                                return "^[0-9]{04}$";
                            case "220":
                                return "^[\\S]{0,10}";
                            case "221":
                                return "^[0-9]{03}$";
                            case "222":
                                return "^[\\S]{0,10}";
                            case "223":
                                return "^[0-9]{05}$";
                            case "224":
                                return "^[\\S]{0,10}";
                            case "225":
                                return "^[\\S]{0,10}";
                            case "226":
                                return "^\\d{5}(?:[-\\s]\\d{4})?$";
                            case "227":
                                return "^[\\S]{0,10}";
                            case "228":
                                return "^[\\S]{0,10}";
                            case "229":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "23":
                        switch (str)
                        {
                            case "23":
                                return "^[\\S]{0,10}";
                            case "230":
                                return "^[\\S]{0,10}";
                            case "231":
                                return "^[0-9]{04}$";
                            case "232":
                                return "^[\\S]{0,10}";
                            case "233":
                                return "^[\\S]{0,10}";
                            case "234":
                                return "^[0-9]{05}$";
                            case "235":
                                return "^[\\S]{0,10}";
                            case "236":
                                return "^[\\S]{0,10}";
                            case "237":
                                return "^[\\S]{0,10}";
                            case "238":
                                return "^[0-9]{05}$";
                            case "239":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "24":
                        switch (str)
                        {
                            case "24":
                                return "^[\\S]{0,10}";
                            case "240":
                                return "^[\\S]{0,10}";
                            case "241":
                                return "^[0-9]{05}$";
                            case "242":
                                return "^[0-9]{04}$";
                            case "243":
                                return "^[\\S]{0,10}";
                            case "244":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "25":
                        switch (str)
                        {
                            case "25":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "26":
                        switch (str)
                        {
                            case "26":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "27":
                        switch (str)
                        {
                            case "27":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "28":
                        switch (str)
                        {
                            case "28":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "29":
                        switch (str)
                        {
                            case "29":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "3":
                switch (str2)
                {
                    case "3":
                        switch (str)
                        {
                            case "3":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "30":
                        switch (str)
                        {
                            case "30":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "31":
                        switch (str)
                        {
                            case "31":
                                return "^[\\S]{09}$";
                        }
                        return "";
                    case "32":
                        switch (str)
                        {
                            case "32":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "33":
                        switch (str)
                        {
                            case "33":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "34":
                        switch (str)
                        {
                            case "34":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "35":
                        switch (str)
                        {
                            case "35":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "36":
                        switch (str)
                        {
                            case "36":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "37":
                        switch (str)
                        {
                            case "37":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "38":
                        switch (str)
                        {
                            case "38":
                                return "^[a-zA-Z][0-9][a-zA-Z]\\s[0-9][a-zA-Z][0-9]$";
                        }
                        return "";
                    case "39":
                        switch (str)
                        {
                            case "39":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "4":
                switch (str2)
                {
                    case "4":
                        switch (str)
                        {
                            case "4":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "40":
                        switch (str)
                        {
                            case "40":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "41":
                        switch (str)
                        {
                            case "41":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "42":
                        switch (str)
                        {
                            case "42":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "43":
                        switch (str)
                        {
                            case "43":
                                return "^[0-9]{04}$";
                        }
                        return "";
                    case "44":
                        switch (str)
                        {
                            case "44":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "45":
                        switch (str)
                        {
                            case "45":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "46":
                        switch (str)
                        {
                            case "46":
                                return "^[0-9a-zA-Z]{0,07}$";
                        }
                        return "";
                    case "47":
                        switch (str)
                        {
                            case "47":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "48":
                        switch (str)
                        {
                            case "48":
                                return "^[0-9]{06}$";
                        }
                        return "";
                    case "49":
                        switch (str)
                        {
                            case "49":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "5":
                switch (str2)
                {
                    case "5":
                        switch (str)
                        {
                            case "5":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "50":
                        switch (str)
                        {
                            case "50":
                                return "^[0-9]{04}$";
                        }
                        return "";
                    case "51":
                        switch (str)
                        {
                            case "51":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "52":
                        switch (str)
                        {
                            case "52":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "53":
                        switch (str)
                        {
                            case "53":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "54":
                        switch (str)
                        {
                            case "54":
                                return "^[0-9]{04}$";
                        }
                        return "";
                    case "55":
                        switch (str)
                        {
                            case "55":
                                return "^[0-9]{3,3}\\s[0-9]{2,2}";
                        }
                        return "";
                    case "56":
                        switch (str)
                        {
                            case "56":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "57":
                        switch (str)
                        {
                            case "57":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "58":
                        switch (str)
                        {
                            case "58":
                                return "^[0-9]{04}$";
                        }
                        return "";
                    case "59":
                        switch (str)
                        {
                            case "59":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "6":
                switch (str2)
                {
                    case "6":
                        switch (str)
                        {
                            case "6":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "60":
                        switch (str)
                        {
                            case "60":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "61":
                        switch (str)
                        {
                            case "61":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "62":
                        switch (str)
                        {
                            case "62":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "63":
                        switch (str)
                        {
                            case "63":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "64":
                        switch (str)
                        {
                            case "64":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "65":
                        switch (str)
                        {
                            case "65":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "66":
                        switch (str)
                        {
                            case "66":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "67":
                        switch (str)
                        {
                            case "67":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "68":
                        switch (str)
                        {
                            case "68":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "69":
                        switch (str)
                        {
                            case "69":
                                return "^[0-9]{05}$";
                        }
                        return "";
                }
                return "";

            case "7":
                switch (str2)
                {
                    case "7":
                        switch (str)
                        {
                            case "7":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "70":
                        switch (str)
                        {
                            case "70":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "71":
                        switch (str)
                        {
                            case "71":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "72":
                        switch (str)
                        {
                            case "72":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "73":
                        switch (str)
                        {
                            case "73":
                                return "^[0-9]{03}$";
                        }
                        return "";
                    case "74":
                        switch (str)
                        {
                            case "74":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "75":
                        switch (str)
                        {
                            case "75":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "76":
                        switch (str)
                        {
                            case "76":
                                return "^[\\S]{0,09}$";
                        }
                        return "";
                    case "77":
                        switch (str)
                        {
                            case "77":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "78":
                        switch (str)
                        {
                            case "78":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "79":
                        switch (str)
                        {
                            case "79":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "8":
                switch (str2)
                {
                    case "8":
                        switch (str)
                        {
                            case "8":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "80":
                        switch (str)
                        {
                            case "80":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "81":
                        switch (str)
                        {
                            case "81":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "82":
                        switch (str)
                        {
                            case "82":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "83":
                        switch (str)
                        {
                            case "83":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "84":
                        switch (str)
                        {
                            case "84":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "85":
                        switch (str)
                        {
                            case "85":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "86":
                        switch (str)
                        {
                            case "86":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "87":
                        switch (str)
                        {
                            case "87":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "88":
                        switch (str)
                        {
                            case "88":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "89":
                        switch (str)
                        {
                            case "89":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                }
                return "";

            case "9":
                switch (str2)
                {
                    case "9":
                        switch (str)
                        {
                            case "9":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "90":
                        switch (str)
                        {
                            case "90":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "91":
                        switch (str)
                        {
                            case "91":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "92":
                        switch (str)
                        {
                            case "92":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "93":
                        switch (str)
                        {
                            case "93":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "94":
                        switch (str)
                        {
                            case "94":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "95":
                        switch (str)
                        {
                            case "95":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "96":
                        switch (str)
                        {
                            case "96":
                                return "^[0-9]{05}$";
                        }
                        return "";
                    case "97":
                        switch (str)
                        {
                            case "97":
                                return "^[\\S]{0,10}";
                        }
                        return "";
                    case "98":
                        switch (str)
                        {
                            case "98":
                                return "^[0-9]{04}$";
                        }
                        return "";
                    case "99":
                        switch (str)
                        {
                            case "99":
                                return "^[0-9]{05}$";
                        }
                        return "";
                }
                return "";
            default:
                return "";
        }
    }
}

#endregion