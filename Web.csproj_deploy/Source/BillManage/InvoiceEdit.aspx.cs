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

public partial class BillManage_InvoiceEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(InvoiceNo, "0");
            UtilsPage.SetTextBoxAutoValue(InvoiceMoney, "0");
            UtilsPage.SetTextBoxCalendar(BuyDate);
            UtilsPage.SetTextBoxCalendar(OldTestDate);
            DataRow brow = CommClass.GetDataRow("select * from cw_billinvoice where id='" + Request.QueryString["bid"] + "'");
            BuyDate.Text = brow["BuyDate"].ToString();
            OldTestDate.Text = brow["OldTestDate"].ToString();
            InvoiceType.Text = brow["InvoiceType"].ToString();
            InvoiceCode.Text = brow["InvoiceCode"].ToString();
            InvoiceNo.Text = brow["InvoiceNo"].ToString();
            InvoiceMoney.Text = brow["InvoiceMoney"].ToString();
            HidInvoiceCode.Value = InvoiceCode.Text;
            SameUpdate.Text = string.Concat("同步更新{发票代码为：", InvoiceCode.Text, "}并未使用的所有发票");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        DicFeilds.Add("BuyDate", BuyDate.Text);
        DicFeilds.Add("OldTestDate", OldTestDate.Text);
        DicFeilds.Add("InvoiceType", InvoiceType.SelectedValue);
        DicFeilds.Add("InvoiceCode", InvoiceCode.Text);
        DicFeilds.Add("InvoiceMoney", InvoiceMoney.Text);
        if (SameUpdate.Checked)
        {
            CommClass.ExecuteSQL("cw_billinvoice", DicFeilds, string.Concat("InvoiceState='0' and OldTestState='0' and InvoiceCode='", HidInvoiceCode.Value, "'"));
        }
        DicFeilds.Add("InvoiceNo", InvoiceNo.Text);
        CommClass.ExecuteSQL("cw_billinvoice", DicFeilds, string.Concat("id='", Request.QueryString["bid"], "'"));
        PageClass.ShowAlertMsg(this.Page, "发票保存成功！");
        RefreshFlag.Value = "1";
    }
}
