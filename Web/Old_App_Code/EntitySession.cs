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
/// EntitySession实体类
/// </summary>
public class EntitySession
{
	public EntitySession()
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
}
