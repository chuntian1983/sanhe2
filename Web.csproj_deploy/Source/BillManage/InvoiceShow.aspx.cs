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

public partial class BillManage_InvoiceShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            string[] arrInvoiceState = { "未使用", "已开票", "已作废", "已丢失" };
            string[] arrInvoiceType = { "增值税专用发票", "普通发票" };
            DataRow brow = CommClass.GetDataRow("select * from cw_billinvoice where id='" + Request.QueryString["bid"] + "'");
            BuyDate.Text = brow["BuyDate"].ToString();
            OldTestDate.Text = brow["OldTestDate"].ToString();
            InvoiceType.Text = arrInvoiceType[TypeParse.StrToInt(brow["InvoiceType"].ToString(), 0)];
            InvoiceCode.Text = brow["InvoiceCode"].ToString();
            InvoiceNo.Text = brow["InvoiceNo"].ToString();
            InvoiceMoney.Text = brow["InvoiceMoney"].ToString();
            DoBillMan.Text = brow["DoBillMan"].ToString();
            DoBillDate.Text = brow["DoBillDate"].ToString();
            CustomName.Text = brow["CustomName"].ToString();
            CustomTaxNo.Text = brow["CustomTaxNo"].ToString();
            DoBillMoney.Text = brow["DoBillMoney"].ToString();
            TaxRate.Text = brow["TaxRate"].ToString();
            TaxMoney.Text = brow["TaxMoney"].ToString();
            SumMoney.Text = brow["SumMoney"].ToString();
        }
    }
}
