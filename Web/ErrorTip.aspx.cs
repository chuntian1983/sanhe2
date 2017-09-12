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

public partial class _ErrorTip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowMachineCode.InnerHtml = string.Format("（当前机器码：{0}）", ValidateClass.GetMachineCode());
        if (string.IsNullOrEmpty(Request.QueryString["errTip"]))
        {
            ShowMsg.InnerHtml = "无信息！";
        }
        else
        {
            ShowMsg.InnerHtml = Request.QueryString["errTip"].Replace("[0]", "<").Replace("[1]", ">");
        }
    }
}
