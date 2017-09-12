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

public partial class AccountInit_GetDataFromAjax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["SessionFlag"] == null)
        {
            Response.Write("alert('登录已过期，请重新登录！');");
            return;
        }
        switch (Request.QueryString["flag"])
        {
            case "0":
                DateTime adate = MainClass.GetAccountDate();
                string yearmonth = adate.ToString("yyyy年MM月");
                string subjectno = Request.QueryString["sno"];
                //decimal begin = TypeParse.StrToDecimal(ClsCalculate.GetSubjectSum(subjectno, "beginbalance", yearmonth), 0);
                //decimal balance = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_dayaccount", "sum(AccMoney)", string.Concat("id<>'", Request.QueryString["id"], "' and (AccSubjectNo like '", subjectno, "%') and (VoucherDate like '", yearmonth, "%') and VoucherDate<='", yearmonth, "31日'"), "0"), 0);
                //balance += begin;
                decimal balance = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_dayaccount", "sum(AccMoney)", string.Concat("id<>'", Request.QueryString["id"], "' and (AccSubjectNo like '", subjectno, "%')"), "0"), 0);
                Response.Write("$('hidBalance').value='" + balance.ToString() + "';checkyue();");
                break;
        }
    }
}
