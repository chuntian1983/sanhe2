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

public partial class BillManage_CheckReceive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            BillUsage.Attributes["onclick"] = "selSubject()";
            UtilsPage.SetTextBoxReadOnly(BillUsage);
            UtilsPage.SetTextBoxAutoValue(BillMoney, "0");
            UtilsPage.SetTextBoxCalendar(ReceiveDate);
            BillNo.Text = Request.QueryString["bno"];
            ReceiveMan.Text = UserInfo.RealName;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        DicFeilds.Add("ReceiveMan", ReceiveMan.Text);
        DicFeilds.Add("ReceiveDate", ReceiveDate.Text);
        DicFeilds.Add("BillUsage", BillUsage.Text);
        DicFeilds.Add("BillMoney", BillMoney.Text);
        DicFeilds.Add("UseState", "1");
        CommClass.ExecuteSQL("cw_billcheck", DicFeilds, string.Concat("id='", Request.QueryString["bid"], "'"));
        PageClass.ExcuteScript(this.Page, "alert('支票申领成功！');window.close();");
        RefreshFlag.Value = "1";
    }
}
