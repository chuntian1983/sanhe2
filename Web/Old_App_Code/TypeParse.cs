using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// 类型转换类
/// </summary>
public class TypeParse
{
	public TypeParse()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static bool IsDouble(object expression)
    {
        return ((expression != null) && Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$"));
    }

    public static bool IsNumeric(object expression)
    {
        return ((expression != null) && IsNumeric(expression.ToString()));
    }

    public static bool IsNumeric(string expression)
    {
        if (expression != null)
        {
            string input = expression;
            if ((((input.Length > 0) && (input.Length <= 11)) && Regex.IsMatch(input, "^[-]?[0-9]*[.]?[0-9]*$")) && (((input.Length < 10) || ((input.Length == 10) && (input[0] == '1'))) || (((input.Length == 11) && (input[0] == '-')) && (input[1] == '1'))))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsNumericArray(string[] strNumber)
    {
        if (strNumber == null)
        {
            return false;
        }
        if (strNumber.Length < 1)
        {
            return false;
        }
        foreach (string str in strNumber)
        {
            if (!IsNumeric(str))
            {
                return false;
            }
        }
        return true;
    }

    public static bool StrToBool(object expression, bool defValue)
    {
        if (expression != null)
        {
            return StrToBool(expression, defValue);
        }
        return defValue;
    }

    public static bool StrToBool(string expression, bool defValue)
    {
        if (expression != null)
        {
            if (string.Compare(expression, "true", true) == 0)
            {
                return true;
            }
            if (string.Compare(expression, "false", true) == 0)
            {
                return false;
            }
        }
        return defValue;
    }

    public static decimal StrToDecimal(object strValue, decimal defValue)
    {
        if (strValue == null)
        {
            return defValue;
        }
        return StrToDecimal(strValue.ToString(), defValue);
    }

    public static decimal StrToDecimal(string strValue, decimal defValue)
    {
        if ((strValue == null) || (strValue.Length > 15))
        {
            return defValue;
        }
        decimal num = defValue;
        if ((strValue != null) && Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
        {
            decimal.TryParse(strValue, out num);
        }
        return num;
    }

    public static double StrToDouble(object strValue, double defValue)
    {
        if (strValue == null)
        {
            return defValue;
        }
        return StrToDouble(strValue.ToString(), defValue);
    }

    public static double StrToDouble(string strValue, double defValue)
    {
        if ((strValue == null) || (strValue.Length > 15))
        {
            return defValue;
        }
        double num = defValue;
        if ((strValue != null) && Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
        {
            double.TryParse(strValue, out num);
        }
        return num;
    }

    public static float StrToFloat(object strValue, float defValue)
    {
        if (strValue == null)
        {
            return defValue;
        }
        return StrToFloat(strValue.ToString(), defValue);
    }

    public static float StrToFloat(string strValue, float defValue)
    {
        if ((strValue == null) || (strValue.Length > 10))
        {
            return defValue;
        }
        float num = defValue;
        if ((strValue != null) && Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
        {
            float.TryParse(strValue, out num);
        }
        return num;
    }

    public static int StrToInt(object expression, int defValue)
    {
        if (expression != null)
        {
            return StrToInt(expression.ToString(), defValue);
        }
        return defValue;
    }

    public static int StrToInt(string str, int defValue)
    {
        int num;
        if ((string.IsNullOrEmpty(str) || (str.Trim().Length >= 11)) || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
        {
            return defValue;
        }
        if (int.TryParse(str, out num))
        {
            return num;
        }
        return Convert.ToInt32(StrToFloat(str, (float)defValue));
    }
}
