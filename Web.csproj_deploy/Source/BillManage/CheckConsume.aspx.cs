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

public partial class BillManage_CheckConsume : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(ConsumeMoney, "0");
            UtilsPage.SetTextBoxCalendar(ConsumeDate);
            BillNo.Text = Request.QueryString["bno"];
            DataRow brow = CommClass.GetDataRow("select * from cw_billcheck where id='" + Request.QueryString["bid"] + "'");
            BillMoney.Value = brow["BillMoney"].ToString();
            BillBank.Text = brow["BillBank"].ToString();
            BillUsage.Text = brow["BillUsage"].ToString();
            ConsumeMoney.Text = BillMoney.Value;
            ConsumeMan.Text = UserInfo.RealName;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        DicFeilds.Add("ConsumeMan", ConsumeMan.Text);
        DicFeilds.Add("ConsumeDate", ConsumeDate.Text);
        DicFeilds.Add("ConsumeMoney", ConsumeMoney.Text);
        DicFeilds.Add("UseState", "3");
        string file = UtilsPage.UploadFiles();
        DicFeilds.Add("AttachFile", file);
        if (SameUpdate.Checked)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add(BillUsage.Text.Substring(0, BillUsage.Text.IndexOf(".")), ConsumeMoney.Text);
            Dictionary<string, string> c = new Dictionary<string, string>();
            c.Add(BillBank.Text.Substring(0, BillBank.Text.IndexOf(".")), ConsumeMoney.Text);
            string subID = CommOutCall.CreateNewVoucher("ZP", VSummary.Text, d, c, file, true, true);
        }
        CommClass.ExecuteSQL("cw_billcheck", DicFeilds, string.Concat("id='", Request.QueryString["bid"], "'"));
        PageClass.ExcuteScript(this.Page, "alert('支票核销成功！');window.close();");
        RefreshFlag.Value = "1";
    }
}
