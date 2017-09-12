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

public partial class BillManage_InvoiceDoBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(DoBillMoney, "0");
            UtilsPage.SetTextBoxAutoValue(TaxRate, "0");
            UtilsPage.SetTextBoxAutoValue(DoBillMoney, "0");
            UtilsPage.SetTextBoxCalendar(DoBillDate);
            DoBillMoney.Attributes["onkeyup"] = "cal()";
            TaxRate.Attributes["onkeyup"] = "cal()";
            UtilsPage.SetTextBoxReadOnly(TaxMoney);
            UtilsPage.SetTextBoxReadOnly(SumMoney);
            CustomName.Attributes["onclick"] = "selSubject('CustomName','')";
            DoBillSubject.Attributes["onclick"] = "selSubject('DoBillSubject','&filter=101|102|112')";
            UtilsPage.SetTextBoxReadOnly(CustomName);
            UtilsPage.SetTextBoxReadOnly(DoBillSubject);
            InvoiceID.Value = Request.QueryString["bid"];
            if (Request.QueryString["red"] == "1")
            {
                OldInvoiceID.Value = InvoiceID.Value;
                DataRow brow = CommClass.GetDataRow("select * from cw_billinvoice where id='" + OldInvoiceID.Value + "'");
                InvoiceID.Value = CommClass.GetTableValue("cw_billinvoice", "id", "InvoiceState='0' and OldTestState='0' and InvoiceCode='" + brow["InvoiceCode"].ToString() + "' order by InvoiceCode,InvoiceNo", "");
                if (InvoiceID.Value.Length == 0)
                {
                    PageClass.ExcuteScript(this.Page, "alert('没有可用的发票！');window.close();");
                }
                else
                {
                    CustomName.Text = brow["CustomName"].ToString();
                    CustomTaxNo.Text = brow["CustomTaxNo"].ToString();
                    DoBillMoney.Text = brow["DoBillMoney"].ToString();
                    TaxRate.Text = brow["TaxRate"].ToString();
                    DoBillSubject.Text = brow["DoBillSubject"].ToString();
                    brow = CommClass.GetDataRow("select * from cw_billinvoice where id='" + InvoiceID.Value + "'");
                    BuyDate.Text = brow["BuyDate"].ToString();
                    OldTestDate.Text = brow["OldTestDate"].ToString();
                    InvoiceCode.Text = brow["InvoiceCode"].ToString();
                    InvoiceNo.Text = brow["InvoiceNo"].ToString();
                    InvoiceMoney.Value = brow["InvoiceMoney"].ToString();
                }
                redDiv0.Style["color"] = "red";
                redDiv1.Style["color"] = "red";
                DoBillMoney.Style["color"] = "red";
                TaxRate.Style["color"] = "red";
                TaxMoney.Style["color"] = "red";
                SumMoney.Style["color"] = "red";
            }
            else
            {
                DataRow brow = CommClass.GetDataRow("select * from cw_billinvoice where id='" + InvoiceID.Value + "'");
                BuyDate.Text = brow["BuyDate"].ToString();
                OldTestDate.Text = brow["OldTestDate"].ToString();
                InvoiceCode.Text = brow["InvoiceCode"].ToString();
                InvoiceNo.Text = brow["InvoiceNo"].ToString();
                DoBillMoney.Text = brow["InvoiceMoney"].ToString();
                InvoiceMoney.Value = DoBillMoney.Text;
            }
            DoBillMan.Text = UserInfo.RealName;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        DicFeilds.Add("CustomName", CustomName.Text);
        DicFeilds.Add("CustomTaxNo", CustomTaxNo.Text);
        DicFeilds.Add("DoBillMan", DoBillMan.Text);
        DicFeilds.Add("DoBillDate", DoBillDate.Text);
        DicFeilds.Add("TaxRate", TaxRate.Text);
        DicFeilds.Add("InvoiceState", "1");
        string file = UtilsPage.UploadFiles();
        DicFeilds.Add("AttachFile", file);
        if (Request.QueryString["red"] == "1")
        {
            if (SameUpdate.Checked)
            {
                DicFeilds.Add("DoBillSubject", DoBillSubject.Text);
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add(CustomName.Text.Substring(0, CustomName.Text.IndexOf(".")), SumMoney.Text);
                Dictionary<string, string> c = new Dictionary<string, string>();
                c.Add(DoBillSubject.Text.Substring(0, DoBillSubject.Text.IndexOf(".")), SumMoney.Text);
                string subID = CommOutCall.CreateNewVoucher("ZP", VSummary.Text, d, c, file, true, true);
            }
            DicFeilds.Add("DoBillMoney", "-" + DoBillMoney.Text);
            DicFeilds.Add("TaxMoney", "-" + TaxMoney.Text);
            DicFeilds.Add("SumMoney", "-" + SumMoney.Text);
            DicFeilds.Add("OldInvoiceID", OldInvoiceID.Value);
            DicFeilds.Add("IsRedInvoice", "1");
        }
        else
        {
            if (SameUpdate.Checked)
            {
                DicFeilds.Add("DoBillSubject", DoBillSubject.Text);
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add(DoBillSubject.Text.Substring(0, DoBillSubject.Text.IndexOf(".")), SumMoney.Text);
                Dictionary<string, string> c = new Dictionary<string, string>();
                c.Add(CustomName.Text.Substring(0, CustomName.Text.IndexOf(".")), SumMoney.Text);
                string subID = CommOutCall.CreateNewVoucher("ZP", VSummary.Text, d, c, file, true, true);
            }
            DicFeilds.Add("DoBillMoney", DoBillMoney.Text);
            DicFeilds.Add("TaxMoney", TaxMoney.Text);
            DicFeilds.Add("SumMoney", SumMoney.Text);
        }
        CommClass.ExecuteSQL("cw_billinvoice", DicFeilds, string.Concat("id='", InvoiceID.Value, "'"));
        PageClass.ExcuteScript(this.Page, "alert('发票保存成功！');window.close();");
        RefreshFlag.Value = "1";
    }
}
