using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Request.QueryString["code"])
        {
            case "403":
                ShowMsg.InnerHtml = "访问被拒绝！";
                break;
            case "404":
                ShowMsg.InnerHtml = "无法找到该页！";
                break;
            default:
                ShowMsg.InnerHtml = "登录已过期或操作异常，请重新登录！";
                break;
        }
    }
}
