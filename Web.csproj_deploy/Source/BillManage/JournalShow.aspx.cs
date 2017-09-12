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

public partial class BillManage_JournalShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            if (Request.QueryString["flag"] == "102")
            {
                PageTitle.InnerHtml = "银行存款日记账凭证";
            }
            else
            {
                DivSettle0.Style["display"] = "none";
                DivSettle1.Style["display"] = "none";
                DivSettle2.Style["display"] = "none";
            }
            TableID.Value = Request.QueryString["id"];
            if (TableID.Value.Length > 0)
            {
                DataRow brow = CommClass.GetDataRow("select * from cw_dayaccount where id='" + TableID.Value + "'");
                AccSubjectNo.Text = brow["AccSubjectNo"].ToString();
                AccCurrency.Text = brow["AccCurrency"].ToString();
                VoucherDate.Text = brow["VoucherDate"].ToString();
                DayNo.Text = brow["DayNo"].ToString();
                string[] vt = { "现付", "现收", "银付", "银收" };
                int strVoucherType=int.Parse(brow["VoucherType"].ToString());
                VoucherType.Text = vt[strVoucherType];
                VoucherNo.Text = brow["VoucherNo"].ToString();
                EntryNo.Text = brow["EntryNo"].ToString();
                SettleType.Text = brow["SettleType"].ToString();
                SettleNo.Text = brow["SettleNo"].ToString();
                SettleDate.Text = brow["SettleDate"].ToString();
                string accMoney = brow["AccMoney"].ToString();
                if (accMoney.StartsWith("-"))
                {
                    CreditMoney.Text = accMoney.Substring(1);
                }
                else
                {
                    DebitMoney.Text = accMoney;
                }
                LinkSubjectNo.Text = brow["LinkSubjectNo"].ToString();
                Handler.Text = brow["Handler"].ToString();
                DoBill.Text = brow["DoBill"].ToString();
                Notes.Text = brow["Notes"].ToString();
                string pic = brow["AttachFile"].ToString();
                if (pic.Length == 0)
                {
                    DivAPicture.Visible = false;
                }
                else
                {
                    APicture.ImageUrl = pic;
                }
            }
        }
    }
}
