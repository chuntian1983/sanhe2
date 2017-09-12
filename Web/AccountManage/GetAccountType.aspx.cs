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

public partial class AccountInit_GetAccountType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["SessionFlag"] == null)
        {
            Response.Write("alert('登录已过期，请重新登录！');");
            return;
        }
        string SubjectNo = Request.QueryString["no"];
        if (CommClass.GetFieldFromNo(SubjectNo, "AccountType") == "1")
        {
            Response.Write("$$('LeadV').value='';");
            Response.Write("$$('OnloanV').value='';");
            Response.Write("$$('LeadV').disabled='disabled';");
            Response.Write("$$('OnloanV').disabled='disabled';");
            Response.Write("$$('AccountType').value='1';");
        }
        else
        {
            Response.Write("setTimeout($$('NBalance').value,0);");
            Response.Write("$$('LeadV').disabled='';");
            Response.Write("$$('OnloanV').disabled='';");
            Response.Write("$$('AccountType').value='0';");
        }
        //获取科目当前余额
        string ThisMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
        string BeginBalance = CommClass.GetFieldFromNo(SubjectNo, "BeginBalance");
        string ThisBalance = CommClass.GetTableValue("cw_voucherentry", "sum(summoney)", string.Format("subjectno='{0}' and voucherdate like '{1}%'", SubjectNo, ThisMonth));
        decimal TotalBalance = TypeParse.StrToDecimal(BeginBalance, 0) + TypeParse.StrToDecimal(ThisBalance, 0);
        Response.Write(string.Concat("$$('SBalance').value='", TotalBalance.ToString(), "';"));
    }
}
