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

public partial class AccountInit_SetAccountPara : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000011$000000")) { return; }
        if (!IsPostBack)
        {
            DataRow row = MainClass.GetDataRow("select * from cw_account where id='" + UserInfo.AccountID + "'");
            AccountName.Text = row["AccountName"].ToString();
            Director.Text = row["Director"].ToString();
            YearCarryFlag.Text = row["YearCarryFlag"].ToString();
            YearCarryVoucher.Text = row["YearCarryVoucher"].ToString();
            UnitID.Text = Session["UnitID"].ToString();
            UnitName.Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitName");
            AccountID.Text = UserInfo.AccountID;
            StartAccountDate.Text = row["StartAccountDate"].ToString();
            AccountDate.Text = row["AccountDate"].ToString();
            LastCarryDate.Text = row["LastCarryDate"].ToString();
            string RealLastCarry = row["RealLastCarry"].ToString();
            if (LastCarryDate.Text.StartsWith(RealLastCarry) == false)
            {
                LastCarryDate.Text = string.Format("{0}（{1}）", LastCarryDate.Text, RealLastCarry);
            }
            SubjectGroup.Text = CommClass.GetSysPara("SubjectGroup");
            SetRedFigure.Text = CommClass.GetSysPara("RedFigure");
            SetTextBox(PageRowCount);
            SetTextBox(PageRowCount000000);
            SetTextBox(PageRowCount100001);
            SetTextBox(PageRowCount100002);
            SetTextBox(PageRowCount100004);
            SetTextBox(PageRowCount100007);
            SetTextBox(PageRowCount100008);
            SaveAccountPara.Attributes.Add("onclick", "return CheckSubmit();");
            RepairAlarmVoucher.Attributes["onclick"] = "return confirm('您确定修复超限凭证状态吗？');";
        }
    }
    private void SetTextBox(TextBox tbox)
    {
        tbox.Text = CommClass.GetSysPara(tbox.ID);
        if (tbox.Text == "NoDataItem")
        {
            tbox.Text = "0";
        }
        UtilsPage.SetTextBoxAutoValue(tbox, "0");
    }
    protected void SaveAccountPara_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_account", string.Format("accountname='{0}' and unitid='{1}' and id<>'{2}'", AccountName.Text, UnitID.Text, AccountID.Text)))
        {
            ExeScript.Text = "<script>alert('此账套名称【" + AccountName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            UserInfo.AccountName = AccountName.Text;
            MainClass.ExecuteSQL(string.Format("update cw_account set AccountName='{1}',Director='{2}',YearCarryFlag='{3}',YearCarryVoucher='{4}' where id='{0}'",
                AccountID.Text, AccountName.Text, Director.Text, YearCarryFlag.SelectedValue, YearCarryVoucher.Text));
            CommClass.SetSysPara("SubjectGroup", SubjectGroup.Text);
            CommClass.SetSysPara("RedFigure", SetRedFigure.SelectedValue);
            CommClass.SetSysPara("PageRowCount", PageRowCount.Text);
            CommClass.SetSysPara("PageRowCount000000", PageRowCount000000.Text);
            CommClass.SetSysPara("PageRowCount100001", PageRowCount100001.Text);
            CommClass.SetSysPara("PageRowCount100002", PageRowCount100002.Text);
            CommClass.SetSysPara("PageRowCount100004", PageRowCount100004.Text);
            CommClass.SetSysPara("PageRowCount100007", PageRowCount100007.Text);
            CommClass.SetSysPara("PageRowCount100008", PageRowCount100008.Text);
            ExeScript.Text = "<script language=javascript>alert('账套信息设置成功！');</script>";
            //写入操作日志
            CommClass.WriteCTL_Log("100011", "账套信息设置");
            //--
        }
    }
    protected void RepairAlarmVoucher_Click(object sender, EventArgs e)
    {
        MySQLClass.RepairAlarmVoucher();
    }
}
