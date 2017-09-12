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

public partial class AccountInit_StartAccount : System.Web.UI.Page
{
    private string G_AccountYear = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (Session["Powers"].ToString().IndexOf("000001") == -1)
        {
            Response.Clear();
            Response.Write("<script>window.close();alert('您无此操作权限！');</script>");
            Response.End();
        }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            string useDate = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo/UseDate");
            UseDate.Value = DateTime.Parse(useDate).ToString("yyyy年MM月dd日");
            //--
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            BackupDate.Attributes["onclick"] = "return confirm('您确定需要进行数据完全备份吗？');";
            UnitName.Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UserInfo.UnitID + "']", "UnitName");
            AccountName.Text = UserInfo.AccountName;
            CurrentDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            AccountDate.Attributes.Add("readonly", "readonly");
            AccountDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].AccountDate,'yyyy年mm月dd日')");
            decimal Balance = 0;
            decimal Balance1 = 0;
            decimal Balance2 = 0;
            decimal Profits = 0;
            decimal LeadSum = 0;
            decimal OnloanSum = 0;
            DataRowCollection rows = CommClass.GetDataRows("select subjectno,subjecttype,beginbalance from cw_subject where parentno='000'");
            if (rows == null)
            {
                Button1.Enabled = false;
            }
            else
            {
                string incomeSubject = SysConfigs.IncomeSubject;
                string expenseSubject = SysConfigs.ExpenseSubject;
                for (int i = 0; i < rows.Count; i++)
                {
                    Balance = decimal.Parse(rows[i]["beginbalance"].ToString());
                    switch (rows[i]["subjecttype"].ToString())
                    {
                        case "1":
                        case "4":
                            Balance1 += Balance;
                            break;
                        case "2":
                        case "3":
                        case "5":
                            Balance2 += Balance;
                            break;
                    }
                    string subjectNo = rows[i]["subjectno"].ToString();
                    if (incomeSubject.IndexOf(subjectNo) != -1)
                    {
                        Profits -= Balance;
                    }
                    if (expenseSubject.IndexOf(subjectNo) != -1)
                    {
                        Profits -= Balance;
                    }
                    if (Balance > 0)
                    {
                        LeadSum += Balance;
                    }
                    else
                    {
                        OnloanSum -= Balance;
                    }
                }
            }
            Balance = Balance1 + Balance2;
            decimal LeadOnloan = LeadSum - OnloanSum;
            Label1.Text = Balance1.ToString("#,##0.00").Replace("-", "");
            Label2.Text = Balance2.ToString("#,##0.00").Replace("-", "");
            Label3.Text = Profits.ToString("#,##0.00");
            Label4.Text = LeadSum.ToString("#,##0.00");
            Label5.Text = OnloanSum.ToString("#,##0.00");
            //--
            Label6.Text = LeadOnloan.ToString("#,##0.00####");
            Label7.Text = Balance.ToString("#,##0.00####");
            if (LeadSum != OnloanSum || Balance != 0)
            {
                Button1.Enabled = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.Compare(UseDate.Value.Substring(0, 8), AccountDate.Text.Substring(0, 8)) < 0)
        {
            PageClass.ShowAlertMsg(this.Page, string.Format("财务日期大于软件到期日期【{0}】，因此账套无法启用！", UseDate.Value));
            return;
        }
        //--
        Session["isStartAccount"] = "Yes";
        G_AccountYear = AccountDate.Text.Substring(0, 4);
        MainClass.ExecuteSQL(string.Format("update cw_account set accountdate='{0}',accountyear='|{1}',startaccountdate='{0}' where id='{2}'", AccountDate.Text, G_AccountYear, UserInfo.AccountID));
        //设置全年预算
        DataTable SubjectList = CommClass.GetDataTable("select subjectno from cw_subject where parentno='000' and (subjectno like '5%' or subjectno='212') order by subjectno");
        StringBuilder InsertBudget = new StringBuilder();
        InsertBudget.Append("insert into cw_subjectbudget(subjectno,budgetyear,budgetbalance)values");
        foreach (DataRow row in SubjectList.Rows)
        {
            SetSubjectBudget(row["subjectno"].ToString(), InsertBudget);
        }
        if (InsertBudget.ToString().EndsWith(","))
        {
            InsertBudget.Remove(InsertBudget.Length - 1, 1);
            CommClass.ExecuteSQL(InsertBudget.ToString());
        }
        InsertBudget.Length = 0;
        //--
        ExeScript.Text = "<script>alert('恭喜您，账套启用成功！');window.returnValue='" + AccountDate.Text + "';window.close();</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100002", "启用账套：" + UserInfo.AccountName);
        //期初建账自动备份
        if (SysConfigs.AutoBackupData == "1")
        {
            MySQLClass.BackupAllTable("期初建账备份（自动）");
        }
    }
    //设置全年预算
    private void SetSubjectBudget(string subjectNo, StringBuilder InsertBudget)
    {
        InsertBudget.AppendFormat("('{0}','{1}','0'),", subjectNo, G_AccountYear);
        DataTable child = CommClass.GetDataTable(string.Concat("select subjectno from cw_subject where parentno='", subjectNo, "'"));
        foreach (DataRow rowc in child.Rows)
        {
            SetSubjectBudget(rowc["subjectno"].ToString(), InsertBudget);
        }
    }
    protected void BackupDate_Click(object sender, EventArgs e)
    {
        if (MySQLClass.BackupAllTable("期初建账备份（手动）"))
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份成功！');</script>";
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份失败！');</script>";
        }
    }
}
