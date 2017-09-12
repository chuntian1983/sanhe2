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

public partial class BillManage_ReceiptBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(ReceiveMoney, "0");
            UtilsPage.SetTextBoxCalendar(ReceiveDate);
            ReceiveMan.Text = UserInfo.RealName;
            DoBillMan.Text = UserInfo.RealName;
            ReceiptID.Value = Request.QueryString["bid"];
            if (ReceiptID.Value.Length > 0)
            {
                DataRow brow = CommClass.GetDataRow("select * from cw_billreceipt where id='" + ReceiptID.Value + "'");
                ReceiveDate.Text = brow["ReceiveDate"].ToString();
                ReceiveNo.Text = brow["ReceiveNo"].ToString();
                PayReason.Text = brow["PayReason"].ToString();
                PayUnit.Text = brow["PayUnit"].ToString();
                PayMan.Text = brow["PayMan"].ToString();
                PayType.Text = brow["PayType"].ToString();
                InvoiceNo.Text = brow["InvoiceNo"].ToString();
                ReceiveMoney.Text = brow["ReceiveMoney"].ToString();
                ReceiveMan.Text = brow["ReceiveMan"].ToString();
                DoBillMan.Text = brow["DoBillMan"].ToString();
                ReveiveState.Text = brow["ReveiveState"].ToString();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool isNew = (ReceiptID.Value.Length == 0);
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        if (isNew)
        {
            ReceiptID.Value = CommClass.GetRecordID("cw_billreceipt");
            DicFeilds["ID"] = ReceiptID.Value;
        }
        DicFeilds.Add("ReceiveDate", ReceiveDate.Text);
        DicFeilds.Add("ReceiveNo", ReceiveNo.Text);
        DicFeilds.Add("PayReason", PayReason.Text);
        DicFeilds.Add("PayUnit", PayUnit.Text);
        DicFeilds.Add("PayMan", PayMan.Text);
        DicFeilds.Add("PayType", PayType.SelectedValue);
        DicFeilds.Add("InvoiceNo", InvoiceNo.Text);
        DicFeilds.Add("ReceiveMoney", ReceiveMoney.Text);
        DicFeilds.Add("ReceiveMan", ReceiveMan.Text);
        DicFeilds.Add("DoBillMan", DoBillMan.Text);
        DicFeilds.Add("ReveiveState", ReveiveState.SelectedValue);
        if (isNew)
        {
            CommClass.ExecuteSQL("cw_billreceipt", DicFeilds);
        }
        else
        {
            CommClass.ExecuteSQL("cw_billreceipt", DicFeilds, string.Concat("id='", ReceiptID.Value, "'"));
        }
        PageClass.ExcuteScript(this.Page, "alert('收据保存成功！');window.close();");
        RefreshFlag.Value = "1";
    }
}
