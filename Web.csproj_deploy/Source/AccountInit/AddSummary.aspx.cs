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

public partial class AccountInit_AddSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Contents = Server.UrlDecode(Request.QueryString["Contents"]);
        //写入操作日志
        CommClass.WriteCTL_Log("100004", "增加摘要：" + Contents);
        //--
        CommClass.ExecuteSQL("insert cw_summary(id,Contents)values('" + CommClass.GetRecordID("CW_Summary") + "','" + Contents + "')");
        Response.Write("alert('摘要加入成功！')");
    }
}
