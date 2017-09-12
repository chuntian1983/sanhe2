using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 用户信息类
/// </summary>
public class UserInfo
{
    public UserInfo()
	{
		//--
	}

    public static void CheckSession()
    {
        if (HttpContext.Current.Session["SessionFlag"] == null)
        {
            PageClass.UrlRedirect(SysConfigs.DefaultPageUrl, 1);
        }
    }

    public static void CheckSession2()
    {
        if (HttpContext.Current.Session["SessionFlag"] == null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("<script>window.close();alert('登录已过期，请重新登录！');</script>");
            HttpContext.Current.Response.End();
        }
    }

    public static string SessionFlag
    {
        set { HttpContext.Current.Session["SessionFlag"] = value; }
    }

    public static string AccountID
    {
        get
        {
            if (HttpContext.Current.Session["AccountID"] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session["AccountID"].ToString();
            }
        }
        set
        {
            HttpContext.Current.Session["AccountID"] = value;
            HttpContext.Current.Session["AccountName"] = null;
        }
    }

    public static string AccountName
    {
        get
        {
            if (HttpContext.Current.Session["AccountName"] == null)
            {
                HttpContext.Current.Session["AccountName"] = MainClass.GetFieldFromID(AccountID, "accountname", "cw_account");
            }
            return HttpContext.Current.Session["AccountName"].ToString();
        }
        set { HttpContext.Current.Session["AccountName"] = value; }
    }

    public static string MyAccount
    {
        get
        {
            return HttpContext.Current.Session["MyAccount"].ToString();
        }
        set { HttpContext.Current.Session["MyAccount"] = value; }
    }

    public static string UserID
    {
        get
        {
            return HttpContext.Current.Session["UserID"].ToString();
        }
        set { HttpContext.Current.Session["UserID"] = value; }
    }

    public static string UserName
    {
        get
        {
            return HttpContext.Current.Session["UserName"].ToString();
        }
        set { HttpContext.Current.Session["UserName"] = value; }
    }

    public static string RealName
    {
        get
        {
            return HttpContext.Current.Session["RealName"].ToString();
        }
        set { HttpContext.Current.Session["RealName"] = value; }
    }

    public static string UserType
    {
        get
        {
            return HttpContext.Current.Session["UserType"].ToString();
        }
        set { HttpContext.Current.Session["UserType"] = value; }
    }

    public static string UnitID
    {
        get
        {
            return HttpContext.Current.Session["UnitID"].ToString();
        }
        set { HttpContext.Current.Session["UnitID"] = value; }
    }

    public static string Powers
    {
        get
        {
            return HttpContext.Current.Session["Powers"].ToString();
        }
        set { HttpContext.Current.Session["Powers"] = value; }
    }
}
