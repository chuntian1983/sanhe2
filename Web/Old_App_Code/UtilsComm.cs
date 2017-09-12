using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;

/// <summary>
/// 常用工具集合类
/// </summary>
public class UtilsComm
{
    public UtilsComm()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetFileContent(string path)
    {
        string result = string.Empty;
        string sFileName = HttpContext.Current.Server.MapPath(path);
        if (File.Exists(sFileName))
        {
            try
            {
                using (StreamReader sr = new StreamReader(sFileName))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch
            {
                result = "读取文件(" + path + ")出错";
            }
        }
        else
        {
            result = "找不到文件：" + path;
        }
        return result;
    }

    /// <summary> 
    /// 取单个字符的拼音声母 
    /// </summary> 
    /// <param name="c">要转换的单个汉字</param> 
    /// <returns>拼音声母</returns> 
    public static string GetPYChar(string c)
    {
        byte[] array = new byte[2];
        array = System.Text.Encoding.Default.GetBytes(c);
        int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
        if (i < 0xB0A1) return "*";
        if (i < 0xB0C5) return "A";
        if (i < 0xB2C1) return "B";
        if (i < 0xB4EE) return "C";
        if (i < 0xB6EA) return "D";
        if (i < 0xB7A2) return "E";
        if (i < 0xB8C1) return "F";
        if (i < 0xB9FE) return "G";
        if (i < 0xBBF7) return "H";
        if (i < 0xBFA6) return "G";
        if (i < 0xC0AC) return "K";
        if (i < 0xC2E8) return "L";
        if (i < 0xC4C3) return "M";
        if (i < 0xC5B6) return "N";
        if (i < 0xC5BE) return "O";
        if (i < 0xC6DA) return "P";
        if (i < 0xC8BB) return "Q";
        if (i < 0xC8F6) return "R";
        if (i < 0xCBFA) return "S";
        if (i < 0xCDDA) return "T";
        if (i < 0xCEF4) return "W";
        if (i < 0xD1B9) return "X";
        if (i < 0xD4D1) return "Y";
        if (i < 0xD7FA) return "Z";
        return "*";
    }

    /// 函数名称：MakeThumbnail
    /// 函数作者：朱坤堂
    /// 创建时间：2010-04-02
    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <returns>是否成功生成</returns>
    public static bool MakeThumbnail(string originalImagePath, string thumbnailPath, int Width, int Height)
    {
        try
        {
            System.Drawing.Image thumbnail;
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            thumbnail = originalImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
            thumbnail.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbnail.Dispose();
            originalImage.Dispose();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool ThumbnailCallback()
    {
        return false;
    }
}
