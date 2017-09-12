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

public partial class BillManage_BankStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxCalendar(VoucherDate, "yyyy年MM月dd日");
            UtilsPage.SetTextBoxCalendar(SettleDate);
            UtilsPage.SetTextBoxAutoValue(SettleMoney, "0");
            UtilsPage.SetTextBoxReadOnly(SettleSubject);
            SettleSubject.Attributes["onclick"] = "selSubject('SettleSubject','&filter=102')";
            BillCashID.Value = Request.QueryString["bid"];
            if (BillCashID.Value.Length > 0)
            {
                DataRow brow = CommClass.GetDataRow("select * from cw_billsettle where id='" + BillCashID.Value + "'");
                SettleSubject.Text = brow["SettleSubject"].ToString();
                SettleDate.Text = brow["SettleDate"].ToString();
                SettleType.Text = brow["SettleType"].ToString();
                SettleNo.Text = brow["SettleNo"].ToString();
                VoucherDate.Text = brow["VoucherDate"].ToString();
                VSummary.Text = brow["VSummary"].ToString();
                string doMoney = brow["SettleMoney"].ToString();
                if (doMoney.StartsWith("-"))
                {
                    SettleMoney.Text = doMoney.Substring(1);
                    DoDirect.SelectedIndex = 1;
                }
                else
                {
                    SettleMoney.Text = doMoney;
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool isNew = (BillCashID.Value.Length == 0);
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        if (isNew)
        {
            BillCashID.Value = CommClass.GetRecordID("cw_billsettle");
            DicFeilds["ID"] = BillCashID.Value;
            DicFeilds.Add("CheckState", "0");
        }
        DicFeilds.Add("SettleSubject", SettleSubject.Text);
        DicFeilds.Add("SettleDate", SettleDate.Text);
        DicFeilds.Add("SettleNo", SettleNo.Text);
        DicFeilds.Add("VoucherDate", VoucherDate.Text);
        DicFeilds.Add("VSummary", VSummary.Text);
        if (DoDirect.SelectedValue == "0")
        {
            DicFeilds.Add("SettleMoney", SettleMoney.Text);
        }
        else
        {
            DicFeilds.Add("SettleMoney", "-" + SettleMoney.Text);
        }
        DicFeilds.Add("SettleType", SettleType.SelectedValue);
        if (isNew)
        {
            CommClass.ExecuteSQL("cw_billsettle", DicFeilds);
        }
        else
        {
            CommClass.ExecuteSQL("cw_billsettle", DicFeilds, string.Concat("id='", BillCashID.Value, "'"));
        }
        PageClass.ExcuteScript(this.Page, "alert('保存成功！');window.close();");
        RefreshFlag.Value = "1";
    }
}
