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

public partial class BillManage_ReceiptShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            string[] arrReveiveState = { "已开", "已换开发票", "已并开发票" };
            string[] arrPayType = { "现金", "现金支票", "转账支票", "电汇凭证", "贷记凭证", "商业承兑汇票", "银行承兑汇票" };
            DataRow brow = CommClass.GetDataRow("select * from cw_billreceipt where id='" + Request.QueryString["bid"] + "'");
            ReceiveDate.Text = brow["ReceiveDate"].ToString();
            ReceiveNo.Text = brow["ReceiveNo"].ToString();
            PayReason.Text = brow["PayReason"].ToString();
            PayUnit.Text = brow["PayUnit"].ToString();
            PayMan.Text = brow["PayMan"].ToString();
            InvoiceNo.Text = brow["InvoiceNo"].ToString();
            ReceiveMoney.Text = brow["ReceiveMoney"].ToString();
            ReceiveMan.Text = brow["ReceiveMan"].ToString();
            DoBillMan.Text = brow["DoBillMan"].ToString();
            ReveiveState.Text = arrReveiveState[TypeParse.StrToInt(ReveiveState.Text, 0)];
            PayType.Text = arrPayType[TypeParse.StrToInt(PayType.Text, 0)];
        }
    }
}
