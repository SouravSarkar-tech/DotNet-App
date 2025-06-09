using System;
using System.Collections.Generic;
using System.Text;

public class SafeTypeHandling
{
    public static string ConvertBooleanToString(object value)
    {
        string str = "No";
        if ((value == null) || (value == DBNull.Value))
        {
            return str;
        }
        if (!Convert.ToBoolean(value))
        {
            return "No";
        }
        return "Yes";
    }
    public static string ConvertListToString(Dictionary<string, string> list)
    {
        if (list == null)
        {
            return string.Empty;
        }
        string str = ";";
        string str2 = ";";
        StringBuilder builder = new StringBuilder();
        foreach (string str3 in list.Keys)
        {
            builder.Append(ConvertToString(str3));
            builder.Append(str2);
            builder.Append(ConvertToString(list[str3]));
            builder.Append(str);
        }
        return builder.ToString();
    }
    public static string ConvertListToString(List<string> list)
    {
        string str = ";#";
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            builder.Append(ConvertToString(list[i]));
            builder.Append(str);
        }
        return builder.ToString();
    }
    public static string ConvertListToString(List<string> list, string delimiter)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            builder.Append(ConvertToString(list[i]));
            builder.Append(delimiter);
        }
        return builder.ToString();
    }
    public static bool ConvertStringToBoolean(object value)
    {
        if ((value == null) || (value == DBNull.Value))
        {
            return false;
        }
        if (((string.Compare("true", value.ToString().ToLower(), true) != 0) && (string.Compare("yes", value.ToString().ToLower(), true) != 0)) && (string.Compare("1", value.ToString()) != 0))
        {
            return false;
        }
        return true;
    }
    public static float ConvertStringToFloat(object value)
    {
        float num;
        float.TryParse(value.ToString(), out num);
        return num;
    }
    public static int ConvertStringToInt32(object value)
    {
        int num;
        if (value == null)
        {
            return 0;
        }
        int.TryParse(value.ToString(), out num);
        return num;
    }
    public static long ConvertStringToLong(object value)
    {
        long num;
        long.TryParse(value.ToString(), out num);
        return num;
    }
    public static double ConvertStringToDouble(object value)
    {
        double num;
        double.TryParse(value.ToString(), out num);
        return num;
    }
    public static DateTime ConvertToDateTime(object value)
    {
        if ((value != null) && (value != DBNull.Value) && (value != ""))
        {
            return DateTime.Parse(value.ToString());
        }
        return DateTime.MinValue;
    }
    public static double ConvertToDouble(object value)
    {
        double num;
        double.TryParse(value.ToString(), out num);
        return num;
    }
    public static string ConvertToString(object value)
    {
        if ((value != null) && (value != DBNull.Value))
        {
            return Convert.ToString(value).Trim();
        }
        return string.Empty;
    }
    public static int ConvertDoubleToInt32(object value)
    {
        int num;
        int.TryParse(value.ToString(), out num);
        return num;
    }
    public static string TrimToNull(string source)
    {
        return string.IsNullOrWhiteSpace(source) ? null : source.Trim();
    }
}