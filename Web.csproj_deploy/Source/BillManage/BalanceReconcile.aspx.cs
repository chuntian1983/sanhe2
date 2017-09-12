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

public partial class BillManage_BalanceCeconcile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(Balance1, 0);
            UtilsPage.SetTextBoxAutoValue(Balance2, 0);
            UtilsPage.SetTextBoxAutoValue(Bank1, 0);
            UtilsPage.SetTextBoxAutoValue(Bank2, 0);
            decimal balance0 = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_entry", "sum(SumMoney)", string.Concat("SubjectNo like '102%'")), 0);
            decimal balance1 = TypeParse.StrToDecimal(CommClass.GetSysPara("ReconcileBalance1"), 0);
            decimal balance2 = TypeParse.StrToDecimal(CommClass.GetSysPara("ReconcileBalance2"), 0);
            Balance0.Text = balance0.ToString("#0.00");
            Balance1.Text = balance1.ToString("#0.00");
            Balance2.Text = balance2.ToString("#0.00");
            Balance1.Attributes["onkeyup"] = "calbalance()";
            Balance2.Attributes["onkeyup"] = "calbalance()";
            decimal bank0 = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_billsettle", "sum(SettleMoney)", string.Concat("SettleSubject like '102%'")), 0);
            decimal bank1 = TypeParse.StrToDecimal(CommClass.GetSysPara("ReconcileBank1"), 0);
            decimal bank2 = TypeParse.StrToDecimal(CommClass.GetSysPara("ReconcileBank2"), 0);
            Bank0.Text = bank0.ToString("#0.00");
            Bank1.Text = bank1.ToString("#0.00");
            Bank2.Text = bank2.ToString("#0.00");
            Bank1.Attributes["onkeyup"] = "calbank()";
            Bank2.Attributes["onkeyup"] = "calbank()";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CommClass.SetSysPara("ReconcileBalance1", Balance1.Text);
        CommClass.SetSysPara("ReconcileBalance2", Balance2.Text);
        CommClass.SetSysPara("ReconcileBank1", Bank1.Text);
        CommClass.SetSysPara("ReconcileBank2", Bank2.Text);
        PageClass.ExcuteScript(this.Page, "alert('保存成功！');window.close();");
    }
}
