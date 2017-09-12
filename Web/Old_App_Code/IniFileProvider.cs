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
using System.Runtime.InteropServices;
using System.Collections.Specialized;

/// <summary>
/// ini文件读取类
/// </summary>
public class IniFileProvider
{
    public string IniFilePath = string.Empty;

    public IniFileProvider()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //声明读写INI文件的API函数
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string Path);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retval, int size, string filePath);

    /// <summary>
    /// 写INI文件
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void WriteIniValue(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, this.IniFilePath);
    }

    /// <summary>
    /// 读INI文件
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public string ReadIniValue(string section, string key)
    {
        StringBuilder temp = new StringBuilder(1024);
        int i = GetPrivateProfileString(section, key, "NoDataItem", temp, 1024, this.IniFilePath);
        return temp.ToString();
    }

    public byte[] ReadIniValues(string section, string key)
    {
        byte[] temp = new byte[255];
        int i = GetPrivateProfileString(section, key, "", temp, 255, this.IniFilePath);
        return temp;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //静态方法区

    public static void WriteIniValue(string section, string key, string value, string filePath)
    {
        WritePrivateProfileString(section, key, value, filePath);
    }

    public static string ReadIniValue(string section, string key, string filePath)
    {
        StringBuilder temp = new StringBuilder(1024);
        int i = GetPrivateProfileString(section, key, "NoDataItem", temp, 1024, filePath);
        return temp.ToString();
    }

    public static byte[] ReadIniValues(string section, string key, string filePath)
    {
        byte[] temp = new byte[255];
        int i = GetPrivateProfileString(section, key, "", temp, 255, filePath);
        return temp;
    }

    /// <summary>
    /// 读取段或键列表
    /// </summary>
    /// <param name="section"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static StringCollection GetKeys(string section, string fileName)
    {
        byte[] buffer = new byte[1024];
        StringCollection items = new StringCollection();
        int bufLen = GetPrivateProfileString(section, null, string.Empty, buffer, buffer.Length, fileName);
        if (bufLen > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bufLen; i++)
            {
                if (buffer[i] != 0)
                {
                    sb.Append((char)buffer[i]);
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        items.Add(sb.ToString());
                        sb = new System.Text.StringBuilder();
                    }
                }
            }
        }
        return items;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
