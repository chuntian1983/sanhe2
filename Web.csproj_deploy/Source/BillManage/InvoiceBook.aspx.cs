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

public partial class BillManage_InvoiceBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(InvoiceNo, "0");
            UtilsPage.SetTextBoxAutoValue(InvoiceCount, "0");
            UtilsPage.SetTextBoxAutoValue(InvoiceMoney, "0");
            UtilsPage.SetTextBoxCalendar(BuyDate);
            UtilsPage.SetTextBoxCalendar(OldTestDate);
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
        DicFeilds.Add("InvoiceState", "0");
        DicFeilds.Add("OldTestState", "0");
        DicFeilds.Add("IsRedInvoice", "0");
        DicFeilds.Add("InvoiceNo", "");
        DicFeilds.Add("ID", "");
        string format = new string('0', InvoiceNo.Text.Length);
        int count = int.Parse(InvoiceCount.Text);
        int start = int.Parse(InvoiceNo.Text);
        int end = start + count;
        for (int i = start; i < end; i++)
        {
            DicFeilds["ID"] = CommClass.GetRecordID("cw_billinvoice");
            DicFeilds["InvoiceNo"] = i.ToString(format);
            CommClass.ExecuteSQL("cw_billinvoice", DicFeilds);
        }
        InvoiceCode.Text = "";
        InvoiceNo.Text = "0";
        RefreshFlag.Value = "1";
        PageClass.ShowAlertMsg(this.Page, "发票登记成功！");
    }
}
