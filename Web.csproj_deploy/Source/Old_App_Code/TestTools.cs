using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

/// <summary>
/// 测试工具类
/// </summary>
public class TestTools
{
	public TestTools()
	{
		// TODO: 在此处添加构造函数逻辑
	}

    public static void OutLog(string log)
    {
        StreamWriter sw = File.AppendText(SysConfigs.GetAppDataFilePath("Log.txt"));
        sw.Write("DateTime:");
        sw.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
        sw.WriteLine(log);
        sw.Flush();
        sw.Close();
    }

    public static void OutLog(string log, bool isNew)
    {
        StreamWriter sw;
        if (isNew)
        {
            sw = new StreamWriter(SysConfigs.GetAppDataFilePath("Log.txt"));
        }
        else
        {
            sw = File.AppendText(SysConfigs.GetAppDataFilePath("Log.txt"));
        }
        sw.Write("DateTime:");
        sw.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss"));
        sw.WriteLine(log);
        sw.Flush();
        sw.Close();
    }
}
