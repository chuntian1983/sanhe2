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

public partial class BillManage_CheckShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            string[] billType = { "", "现金支票", "转账支票", "电汇凭证", "贷记凭证" };
            DataRow brow = CommClass.GetDataRow("select * from cw_billcheck where id='" + Request.QueryString["bid"] + "'");
            BillBank.Text = brow["BillBank"].ToString();
            BillCurrency.Text = brow["BillBank"].ToString();
            BillType.Text = billType[TypeParse.StrToInt(brow["BillType"].ToString(), 0)];
            BillNo.Text = brow["BillNo"].ToString();
            BillDate.Text = brow["BillDate"].ToString();
            BillPeriod.Text = brow["BillPeriod"].ToString();
            ReceiveMan.Text = brow["ReceiveMan"].ToString();
            ReceiveDate.Text = brow["ReceiveDate"].ToString();
            BillUsage.Text = brow["BillUsage"].ToString();
            BillMoney.Text = brow["BillMoney"].ToString();
            ConsumeMan.Text = brow["ConsumeMan"].ToString();
            ConsumeDate.Text = brow["ConsumeDate"].ToString();
            ConsumeMoney.Text = brow["ConsumeMoney"].ToString();
        }
    }
}
