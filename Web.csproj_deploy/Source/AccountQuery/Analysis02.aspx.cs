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
using System.Collections.Generic;

public partial class AccountQuery_Analysis02 : System.Web.UI.Page
{
    private string sYearMonth0;
    private string eYearMonth0;
    private string sYearMonth1;
    private string eYearMonth1;
    private decimal PaymentPreData = 0;
    private decimal PaymentThisData = 0;
    private decimal PaymentYearBudget = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000012$100020")) { return; }
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            GridView1.Attributes.Add("onselectstart", "return false;");
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            GetCommDetailInfo();
            //写入操作日志
            CommClass.WriteCTL_Log("100017", "报表分析：福利费收支分析表");
            //--
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }

    protected void GetCommDetailInfo()
    {
        //上期余额+本期增加-本期支出=期末余额
        string WelfarismSubject = SysConfigs.WelfarismSubject;
        //--
        DataTable dataTable = new DataTable();
        int PreYear = int.Parse(QYear.SelectedValue) - 1;
        sYearMonth0 = PreYear.ToString() + "年" + QSMonth.SelectedValue + "月";
        eYearMonth0 = PreYear.ToString() + "年" + QEMonth.SelectedValue + "月";
        sYearMonth1 = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月";
        eYearMonth1 = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月";
        string OutputMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue;
        if (QSMonth.SelectedValue != QEMonth.SelectedValue)
        {
            OutputMonth += "-" + QEMonth.SelectedValue;
        }
        ReportDate.Text = OutputMonth + "月";
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 7; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //输出表头
        DataRow NewRow = BindTable.NewRow();
        NewRow[0] = "<center><font color=red>项&nbsp;&nbsp;&nbsp;&nbsp;目</font></center>";
        NewRow[1] = "<center><font color=red>上年同期</font></center>";
        NewRow[2] = "<center><font color=red>本期数</font></center>";
        NewRow[3] = "<center><font color=red>增减额</font></center>";
        NewRow[4] = "<center><font color=red>增减率</font></center>";
        NewRow[5] = "<center><font color=red>全年预算</font></center>";
        NewRow[6] = "<center><font color=red>完成率</font></center>";
        BindTable.Rows.Add(NewRow);
        //上期结转同期对比分析
        DateTime LastYearMonth = Convert.ToDateTime(sYearMonth1).AddMonths(-1);
        string LastMonth0 = LastYearMonth.AddYears(-1).ToString("yyyy年MM月");
        string LastMonth1 = LastYearMonth.ToString("yyyy年MM月");
        decimal LastPreData = 0 - ClsCalculate.GetSubjectSumDecimal(WelfarismSubject, "finalbalance", LastMonth0);
        decimal LastThisData = 0 - ClsCalculate.GetSubjectSumDecimal(WelfarismSubject, "finalbalance", LastMonth1);
        decimal LastYearBudget = GetYearBudget(WelfarismSubject, LastYearMonth.Year.ToString());
        NewRow = BindTable.NewRow();
        FillNewRow(NewRow, "上期结转", LastPreData, LastThisData, LastYearBudget);
        BindTable.Rows.Add(NewRow);
        //获取福利费总账数据
        decimal WelfarePreData = GetMarginSum(WelfarismSubject, sYearMonth0, eYearMonth0, true);
        decimal WelfareThisData = GetMarginSum(WelfarismSubject, sYearMonth1, eYearMonth1, true);
        decimal WelfareYearBudget = GetYearBudget(WelfarismSubject, QYear.SelectedValue);
        //输出福利费支出明细科目
        DoInsert(BindTable);
        //输出本期支出汇总
        NewRow = BindTable.NewRow();
        FillNewRow(NewRow, "本期支出", PaymentPreData, PaymentThisData, PaymentYearBudget);
        BindTable.Rows.InsertAt(NewRow, 2);
        //输出本期增加汇总
        decimal PreData = 0;
        decimal ThisData = 0;
        decimal YearBudget = 0;
        string[] WelfarismIncrease = SysConfigs.WelfarismIncrease.Split('|');
        for (int i = 0; i < WelfarismIncrease.Length; i++)
        {
            PreData += GetMarginSum(WelfarismIncrease[i], sYearMonth0, eYearMonth0, true);
            ThisData += GetMarginSum(WelfarismIncrease[i], sYearMonth1, eYearMonth1, true);
            YearBudget += GetYearBudget(WelfarismIncrease[i], QYear.SelectedValue);
        }
        NewRow = BindTable.NewRow();
        FillNewRow(NewRow, "本期增加", PreData, ThisData, YearBudget);
        BindTable.Rows.InsertAt(NewRow, 2);
        //输出福利费余额
        WelfarePreData = LastPreData + PreData - PaymentPreData;
        WelfareThisData = LastThisData + ThisData - PaymentThisData;
        NewRow = BindTable.NewRow();
        FillNewRow(NewRow, "福利费余额", WelfarePreData, WelfareThisData, WelfareYearBudget);
        BindTable.Rows.Add(NewRow);
        //绑定表格字段
        GridView1.Columns.Clear();
        for (int i = 0; i < BindTable.Columns.Count; i++)
        {
            BoundField bf = new BoundField();
            bf.DataField = BindTable.Columns[i].ColumnName;
            bf.HtmlEncode = false;
            bf.ItemStyle.CssClass = "sbalance";
            GridView1.Columns.Add(bf);
        }
        //设置表格参数
        GridView1.DataSource = BindTable.DefaultView;
        GridView1.DataBind();
        GridView1.Columns[0].ItemStyle.Width = 160;
        GridView1.Columns[1].ItemStyle.Width = 110;
        GridView1.Columns[2].ItemStyle.Width = 110;
        GridView1.Columns[3].ItemStyle.Width = 110;
        GridView1.Columns[4].ItemStyle.Width = 60;
        GridView1.Columns[5].ItemStyle.Width = 110;
        GridView1.Columns[6].ItemStyle.Width = 60;
        GridView1.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            GridView1.Rows[i].Cells[0].Style["padding-left"] = "20px";
        }
    }

    private void DoInsert(DataTable BindTable)
    {
        int ShowPos = 0;
        PaymentPreData = 0;
        PaymentThisData = 0;
        PaymentYearBudget = 0;
        System.Text.StringBuilder sql = new System.Text.StringBuilder();
        string[] WelfarismIncrease = SysConfigs.WelfarismIncrease.Split('|');
        sql.AppendFormat("select subjectno,subjectname from cw_subject where parentno='{0}' ", SysConfigs.WelfarismSubject);
        for (int i = 0; i < WelfarismIncrease.Length; i++)
        {
            sql.AppendFormat("and SubjectNo not like '{0}%' ", WelfarismIncrease[i]);
        }
        sql.Append("order by subjectno");
        DataTable dataTable = CommClass.GetDataTable(sql.ToString());
        foreach(DataRow row in dataTable.Rows)
        {
            InsertRow(BindTable, (++ShowPos).ToString() + ".&nbsp;" + row[1].ToString(), row[0].ToString());
        }
    }

    private void InsertRow(DataTable BindTable, string ItemText, string SubjectNo)
    {
        decimal YearBudget = GetYearBudget(SubjectNo, QYear.SelectedValue);
        decimal PreData = GetMarginSum(SubjectNo, sYearMonth0, eYearMonth0, false);
        decimal ThisData = GetMarginSum(SubjectNo, sYearMonth1, eYearMonth1, false);
        DataRow NewRow = BindTable.NewRow();
        FillNewRow(NewRow, ItemText, PreData, ThisData, YearBudget);
        BindTable.Rows.Add(NewRow);
        PaymentPreData += PreData;
        PaymentThisData += ThisData;
        PaymentYearBudget += YearBudget;
    }

    private void FillNewRow(DataRow NewRow, string ItemText, decimal PreData, decimal ThisData, decimal YearBudget)
    {
        decimal MarginData = ThisData - PreData;
        NewRow[0] = ItemText;
        NewRow[1] = PreData == 0 ? "" : PreData.ToString("#,##0.00");
        NewRow[2] = ThisData == 0 ? "" : ThisData.ToString("#,##0.00");
        NewRow[3] = MarginData == 0 ? "" : MarginData.ToString("#,##0.00");
        if (MarginData > 0)
        {
            NewRow[4] = PreData == 0 ? "100%" : (MarginData / PreData).ToString("P");
        }
        if (MarginData < 0)
        {
            NewRow[4] = PreData == 0 ? "-100%" : (MarginData / PreData).ToString("P");
        }
        NewRow[5] = YearBudget == 0 ? "" : YearBudget.ToString("#,##0.00");
        if (YearBudget != 0)
        {
            NewRow[6] = ThisData == 0 ? "0.00%" : (ThisData / YearBudget).ToString("P");
        }
    }

    private decimal GetMarginSum(string SubjectNo, string sYearMonth, string eYearMonth, bool isOnloan)
    {
        return ClsCalculate.GetMarginSubjectSum(SubjectNo, sYearMonth, eYearMonth, isOnloan);
    }

    private decimal GetYearBudget(string subjectNo, string year)
    {
        decimal YearBudget = 0;
        decimal.TryParse(CommClass.GetTableValue("cw_subjectbudget", "budgetbalance", string.Concat("subjectno='", subjectNo, "' and budgetyear='", year, "'")), out YearBudget);
        return YearBudget;
    }

    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        PageClass.ToExcel(GridView1);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
