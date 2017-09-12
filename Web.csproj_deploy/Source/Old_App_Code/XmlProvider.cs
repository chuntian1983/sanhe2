using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
/// Xml文档操作类
/// </summary>
public class XmlProvider
{
    private string a;
    private XmlDocument b;

    public XmlProvider(string XmlFile)
    {
        try
        {
            this.b = new XmlDocument();
            this.b.Load(XmlFile);
        }
        catch { }
        this.a = XmlFile;
    }

    public void DeleteNode(string NodeName)
    {
        string xpath = NodeName.Substring(0, NodeName.LastIndexOf("/"));
        this.b.SelectSingleNode(xpath).RemoveChild(this.b.SelectSingleNode(NodeName));
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    protected void Dispose(bool Diposing)
    {
        this.b = null;
    }

    public void InsertElement(string mainNode, string element, string content)
    {
        XmlNode node = this.b.SelectSingleNode(mainNode);
        XmlElement newChild = this.b.CreateElement(element);
        newChild.InnerText = content;
        node.AppendChild(newChild);
    }

    public void InsertElement(string mainNode, string element, string attrib, string attribContent, string content)
    {
        XmlNode node = this.b.SelectSingleNode(mainNode);
        XmlElement newChild = this.b.CreateElement(element);
        newChild.SetAttribute(attrib, attribContent);
        newChild.InnerText = content;
        node.AppendChild(newChild);
    }

    public void InsertNode(string mainNode, string childNode, string element, string content)
    {
        XmlNode node = this.b.SelectSingleNode(mainNode);
        XmlElement newChild = this.b.CreateElement(childNode);
        node.AppendChild(newChild);
        XmlElement element3 = this.b.CreateElement(element);
        element3.InnerText = content;
        newChild.AppendChild(element3);
    }

    public string ReadAttribute(string PathNode, string AttributeName)
    {
        try
        {
            return this.b.SelectSingleNode(PathNode).Attributes[AttributeName].Value;
        }
        catch
        {
            return "";
        }
    }

    public string ReadInnerText(string PathNode)
    {
        return this.b.SelectSingleNode(PathNode).InnerText;
    }

    public XmlNode ReadNode(string PathNode)
    {
        return this.b.SelectSingleNode(PathNode);
    }

    public void UpdateAttribute(string PathNode, string AttributeName, int AttributeValue)
    {
        this.b.SelectSingleNode(PathNode).Attributes[AttributeName].Value = AttributeValue.ToString();
    }

    public void UpdateAttribute(string PathNode, string AttributeName, string AttributeValue)
    {
        this.b.SelectSingleNode(PathNode).Attributes[AttributeName].Value = AttributeValue;
    }

    public void UpdateInnerText(string PathNode, string InnerText)
    {
        this.b.SelectSingleNode(PathNode).InnerText = InnerText;
    }

    public void Save()
    {
        try
        {
            this.b.Save(this.a);
        }
        catch { }
        this.b = null;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //xml缓存处理

    /// <summary>
    /// 根据键值获取缓存的XmlDocument对象
    /// </summary>
    /// <param name="xmlfile_key"></param>
    /// <returns></returns>
    public static XmlDocument GetXmlDocument(string xmlfile_key)
    {
        return (XmlDocument)HttpContext.Current.Cache[xmlfile_key];
    }

    /// <summary>
    /// 根据键值和xml文件完整路径获取xmlDocument对象
    /// </summary>
    /// <param name="xmlfile_key">20080222</param>
    /// <param name="xmlfile_path">XXXXX.xml</param>
    /// <returns></returns>
    public static XmlDocument GetXmlDocument(string xmlfile_key, string xmlfile_path)
    {
        try
        {
            if (null == HttpContext.Current.Cache[xmlfile_key])
            {
                System.Web.Caching.CacheDependency CacheDependencyXmlFile = new System.Web.Caching.CacheDependency(xmlfile_path);
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(xmlfile_path);
                HttpContext.Current.Cache.Insert(xmlfile_key, XmlDoc, CacheDependencyXmlFile);
                return XmlDoc;
            }
            return (XmlDocument)HttpContext.Current.Cache[xmlfile_key];
        }
        catch { HttpContext.Current.Cache.Remove(xmlfile_key); return null; }
    }

    /// <summary>
    /// 向缓存中插入一个XmlDocument对象
    /// </summary>
    /// <param name="xmlfile_key"></param>
    /// <param name="xmlfile_path"></param>
    /// <returns></returns>
    public static bool InsertXmlToCache(string xmlfile_key, string xmlfile_path)
    {
        try
        {
            System.Web.Caching.CacheDependency CacheDependencyXmlFile = new System.Web.Caching.CacheDependency(xmlfile_path);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(xmlfile_path);
            HttpContext.Current.Cache.Insert(xmlfile_key, XmlDoc, CacheDependencyXmlFile);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 从服务器缓存中移除指定项，该方法总是成功
    /// </summary>
    /// <param name="xmlfile_key">要移除项的关键字</param>
    public static void RemoveXmlInCache(string xmlfile_key)
    {
        HttpContext.Current.Cache.Remove(xmlfile_key);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //静态方法区

    public static string ReadAttribute(string XmlPath, string PathNode, string AttributeName)
    {
        string AttributeValue = string.Empty;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(XmlPath);
        try
        {
            AttributeValue = xmlDoc.SelectSingleNode(PathNode).Attributes[AttributeName].Value;
        }
        catch { }
        xmlDoc = null;
        return AttributeValue;
    }
    
    public static string ReadInnerText(string XmlPath, string PathNode)
    {
        string XmlInnerText = string.Empty;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(XmlPath);
        try
        {
            XmlInnerText = xmlDoc.SelectSingleNode(PathNode).InnerText;
        }
        catch { }
        xmlDoc = null;
        return XmlInnerText;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 解密XML文档
    /// </summary>
    /// <param name="xmlEncDoc"></param>
    /// <returns></returns>
    public static XmlDocument DecodeXML_Doc(XmlDocument xmlEncDoc)
    {
        string str_Private_Key
            = "BwIAAACkAABSU0EyAAQAAAEAAQCzHbzI4nC0GeYS5s/A/cRfOuy/1mfq1UZhOypchWKbZVOKW8Kg09+sAYlWhgIyaeBmKXbQB"
            + "+/St6XpU67XGP5Q6/3vm8sXFrYOR8kS9o0aOeMf/5TgqH4Jjzpj5GUMqaa5HhmtrQGXCkc9tL6fYG99AMssDGo105s27/LWAJ"
            + "KyxSW9yC+1TfNjJNx5xrXxxDwxrUq4JrRAbRqL86w1Sqaf9pbFb8ukLd1TCUcAJ3mGbju2jr/SSxNeTDACxQjv1/73c94fcyx"
            + "kcFjNmF1ZGhYySAe6xub/DeIQz9XT5kxKg738aUekuYhmrxLkPO7PeBDa8hWOCN1mSIYoyDgzP5jGicyB1iaSGLLxbS05CPB0"
            + "MMtzZ4Jtra1EJmwjEX5HB04uy1Ip5bYc9LkmxIEUo86P0YmNchnqsO/0GE+TL4WiX5kbYUmxTg+QkvMk4Dlb8vc83/ow7DMBd"
            + "0s+OdQw6ow/u9/NLElq5WpT3Zd4ek/TvMH7zjYP9jhqEKwZXXKIgFOKY3ea9M4I9weGsJZTF7pbUchbfY3hXSmflIJeQo63iU"
            + "gJ4pXzuZt3HvZAq8ofjbLPgAX885f9BCB5nrIknvjROU8sBDYBP92XLvlRLJmOe7U6tJjTFpI3Stey3qy9P333GS5SHhC+SOy"
            + "r7jkzPz/zYNGBHinJgdExWALhkAgco3+hgJyrbXKp5vH2kRQhC3MFm/bxtyQj8CjoiMgNrfK5pMCu1h+JYZXbawGzSXEqagngE3yEvWfuIEUmKwzypxE=";
        System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
        rsa.ImportCspBlob(Convert.FromBase64String(str_Private_Key));
        System.Security.Cryptography.Xml.EncryptedXml encXml = new System.Security.Cryptography.Xml.EncryptedXml(xmlEncDoc);
        encXml.AddKeyNameMapping("nongyoubsfinace", rsa);
        encXml.DecryptDocument();
        return xmlEncDoc;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
