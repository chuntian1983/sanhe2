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

public partial class Contract_DueNoticePay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        AName.Text = UserInfo.AccountName;
        DataRow row = CommClass.GetDataRow("select LeaseHolder,ResourceName,EndLease from cw_resleasecard where id='" + Request.QueryString["lid"] + "'");
        LeaseHolder.Text = row["LeaseHolder"].ToString();
        ResourceName.Text = row["ResourceName"].ToString();
        DataRow row2 = CommClass.GetDataRow("select EndPay,PayMoney from cw_respayperiod where id='" + Request.QueryString["pid"] + "'");
        EndLease.Text = DateTime.Parse(row2["EndPay"].ToString()).ToString("yyyy年MM月dd日");
        PayMoney.Text = row2["PayMoney"].ToString();
        PrintDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
    }
}
