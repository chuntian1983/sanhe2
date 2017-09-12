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
using System.Text;

public partial class FinanceFlow_CreateVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button2.Attributes["onclick"] = "return confirm('您确定需要执行“'+this.value+'”操作吗？');";
            UtilsPage.SetTextBoxReadOnly(DebitSubject);
            UtilsPage.SetTextBoxReadOnly(CreditSubject);
            UtilsPage.SetTextBoxReadOnly(ApplyUsage);
            UtilsPage.SetTextBoxReadOnly(ReplyMoney);
            DebitSubject.Attributes.Add("onclick", "SelectItem(1)");
            CreditSubject.Attributes.Add("onclick", "SelectItem(2)");
            DataRow row = CommClass.GetDataRow(string.Concat("select ApplyUsage,ReplyMoney from cw_flowmoney where id='", Request.QueryString["id"], "'"));
            ApplyUsage.Text = row["ApplyUsage"].ToString();
            ReplyMoney.Text = row["ReplyMoney"].ToString();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        VoucherID.Value = GetVoucherID();
        CommClass.ExecuteSQL(string.Concat("update cw_flowmoney set VoucherID='", VoucherID.Value, "' where id='", Request.QueryString["id"], "'"));
        PageClass.ExcuteScript(this.Page, string.Concat("window.returnValue='", VoucherID.Value, "';window.close();alert('操作成功！');"));
    }
    private string GetVoucherID()
    {
        DateTime accountDate = MainClass.GetAccountDate();
        string LastVoucherNo = CommClass.GetTableValue("cw_voucher", "voucherno", "voucherdate like '" + accountDate.ToString("yyyy年MM月") + "%' order by voucherno desc");
        if (LastVoucherNo == "NoDataItem")
        {
            LastVoucherNo = "100000";
        }
        int _LastVoucherNo = int.Parse(LastVoucherNo) + 1;
        DataSet Voucher = CommClass.GetDataSet("select * from cw_voucher where 1=2");
        DataRow NewDataRow = Voucher.Tables[0].NewRow();
        string VoucherID = CommClass.GetRecordID("CW_Voucher");
        NewDataRow["id"] = VoucherID;
        NewDataRow["voucherno"] = _LastVoucherNo.ToString();
        NewDataRow["voucherfrom"] = "FF";
        NewDataRow["voucherdate"] = accountDate.ToString("yyyy年MM月dd日");
        NewDataRow["IsAuditing"] = "0";
        NewDataRow["IsRecord"] = "0";
        NewDataRow["Director"] = MainClass.GetFieldFromID(UserInfo.AccountID, "director", "cw_account");
        NewDataRow["DoBill"] = Session["RealName"].ToString();
        NewDataRow["Assessor"] = "";
        NewDataRow["Accountant"] = "";
        NewDataRow["Addons"] = "";
        NewDataRow["AddonsCount"] = "";
        NewDataRow["IsHasAlarm"] = "0";
        NewDataRow["DelFlag"] = "0";
        Voucher.Tables[0].Rows.Add(NewDataRow);
        CommClass.UpdateDataSet(Voucher);
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('"
            + CommClass.GetRecordID("CW_Entry") + "','"
            + VoucherID + "','" + Notes.Text + "','"
            + DebitSubject.Text.Substring(0, DebitSubject.Text.IndexOf(".")) + "','"
            + ReplyMoney.Text + "')");
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('"
            + CommClass.GetRecordID("CW_Entry") + "','"
            + VoucherID + "','" + Notes.Text + "','"
            + CreditSubject.Text.Substring(0, CreditSubject.Text.IndexOf(".")) + "','-"
            + ReplyMoney.Text + "')");
        return VoucherID;
    }
}
