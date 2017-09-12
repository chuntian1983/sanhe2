using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// 自定义参数类
/// </summary>
public class DefConfigs
{
	public DefConfigs()
	{
		//构造函数
	}

    /// 函数名称：GetValueFromDefConfig
    /// 函数作者：朱坤堂
    /// 创建时间：2008-11-07
    /// <summary>
    /// 获取软件配置参数[DefConfig.xml]
    /// </summary>
    /// <param name="PathNode"></param>
    /// <returns></returns>
    public static string GetValueFromDefConfig(string key)
    {
        return XmlProvider.ReadAttribute(DefConfigFilePath, string.Format("SysConfig/{0}", key), "Value");
    }

    public static string DefConfigFilePath
    {
        get { return System.IO.Path.Combine(SysConfigs.baseDirectory, SysConfigs.GetValueFromWebConfig("DefConfigFilePath")); }
    }

    public static string ReportSignName
    {
        get { return GetValueFromDefConfig("ReportSignName"); }
    }

    public static string DomainUrl
    {
        get { return GetValueFromDefConfig("DomainUrl"); }
    }

    public static string InstallDir
    {
        get { return GetValueFromDefConfig("InstallDir"); }
    }

    public static string MySQL_BaseDir
    {
        get { return GetValueFromDefConfig("MySQL_BaseDir"); }
    }

    public static string MySQL_DataDir
    {
        get { return GetValueFromDefConfig("MySQL_DataDir"); }
    }

    public static string SerialNumber
    {
        get { return GetValueFromDefConfig("SerialNumber"); }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //报表底部签字数组
    ///////////////////////////////////////////////////////////////////////////////////////////////

    public static string GetReportSignName(int i)
    {
        return GetReportSignNameList()[i];
    }
    public static List<string> GetReportSignNameList()
    {
        List<string> SignName = new List<string>();
        if (HttpContext.Current.Session["ReportSignName"] == null)
        {
            string defineSignName = MainClass.GetTableValue("CW_ReportSignName", "ReportSignName", string.Format("UnitID='{0}'", UserInfo.UnitID));
            if (defineSignName == "NoDataItem" || defineSignName.Length == 0)
            {
                defineSignName = ReportSignName;
            }
            string[] ArraySignName = defineSignName.Split('$');
            if (ArraySignName.Length == 6)
            {
                SignName.Add(ArraySignName[0]);
                SignName.Add(ArraySignName[1]);
                SignName.Add(ArraySignName[2]);
                SignName.Add(ArraySignName[3]);
                SignName.Add(ArraySignName[4]);
                if (ArraySignName[5] == "1")
                {
                    SignName.Add(DateTime.Now.ToString("打印日期：yyyy年MM月dd日"));
                }
                else
                {
                    SignName.Add("");
                }
            }
            else
            {
                SignName.Add("");
                SignName.Add("");
                SignName.Add("");
                SignName.Add("");
                SignName.Add("");
                SignName.Add(DateTime.Now.ToString("打印日期：yyyy年MM月dd日"));
            }
            HttpContext.Current.Session["ReportSignName"] = SignName;
        }
        else
        {
            SignName = (List<string>)HttpContext.Current.Session["ReportSignName"];
        }
        return SignName;
    }
}
