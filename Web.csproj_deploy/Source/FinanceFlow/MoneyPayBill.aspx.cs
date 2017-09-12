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

public partial class FinanceFlow_MoneyPayBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        AName.Text = UserInfo.AccountName;
        DataTable dt = CommClass.GetDataTable(string.Concat("select ApplyUsage from cw_flowmoney where flowid='", Request.QueryString["id"], "'"));
        foreach (DataRow row in dt.Rows)
        {
            ApplyUsage.Text += row["ApplyUsage"].ToString() + "；";
        }
        string money = CommClass.GetTableValue("cw_flowmoney", "sum(ReplyMoney)", string.Concat("flowid='", Request.QueryString["id"], "' group by flowid"));
        decimal m = TypeParse.StrToDecimal(money, 0);
        Money0.Text = PageClass.GetNumber2CN(m);
        Money1.Text = m.ToString("#,##0.00");
        PrintDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
    }
}
