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

public partial class AccountQuery_Analysis01 : System.Web.UI.Page
{
    private string sYearMonth0;
    private string eYearMonth0;
    private string sYearMonth1;
    private string eYearMonth1;
    private decimal TPreData = 0;
    private decimal TThisData = 0;
    private decimal TYearBudget = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000012$100019")) { return; }
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
            CommClass.WriteCTL_Log("100017", "报表分析：财务收支分析表");
            //--
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }

    protected void GetCommDetailInfo()
    {
        string[] cnNumber ={ "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
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
        int InsertRowPos = 1;
        decimal MarginData = 0;
        decimal YPreData = 0;
        decimal YThisData = 0;
        decimal YYearBudget = 0;
        YPreData = 0;
        YPreData = 0;
        YYearBudget = 0;
        //输出收入数据
        TPreData = 0;
        TThisData = 0;
        TYearBudget = 0;
        string[] IncomeSubject = SysConfigs.IncomeSubject.Split('|');
        for (int i = 0; i < IncomeSubject.Length; i++)
        {
            DoInsert(BindTable, string.Format("（{0}）{1}", cnNumber[i], CommClass.GetFieldFromNo(IncomeSubject[i], "SubjectName")), IncomeSubject[i]);
        }
        YPreData += TPreData;
        YThisData += TThisData;
        YYearBudget += TYearBudget;
        //总收入统计
        MarginData = TThisData - TPreData;
        NewRow = BindTable.NewRow();
        NewRow[0] = "<center>一、总收入<center>";
        NewRow[1] = TPreData == 0 ? "" : TPreData.ToString("#,##0.00");
        NewRow[2] = TThisData == 0 ? "" : TThisData.ToString("#,##0.00");
        NewRow[3] = MarginData == 0 ? "" : MarginData.ToString("#,##0.00");
        if (MarginData != 0)
        {
            NewRow[4] = TPreData == 0 ? (MarginData > 0 ? "100%" : "-100%") : (MarginData / TPreData).ToString("P");
        }
        NewRow[5] = TYearBudget == 0 ? "" : TYearBudget.ToString("#,##0.00");
        if (TYearBudget != 0)
        {
            NewRow[6] = TThisData == 0 ? "0.00%" : (TThisData / TYearBudget).ToString("P");
        }
        BindTable.Rows.InsertAt(NewRow, InsertRowPos);
        InsertRowPos = BindTable.Rows.Count;
        //输出支出数据
        TPreData = 0;
        TThisData = 0;
        TYearBudget = 0;
        string[] ExpenseSubject = SysConfigs.ExpenseSubject.Split('|');
        for (int i = 0; i < ExpenseSubject.Length; i++)
        {
            DoInsert(BindTable, string.Format("（{0}）{1}", cnNumber[i], CommClass.GetFieldFromNo(ExpenseSubject[i], "SubjectName")), ExpenseSubject[i]);
        }
        YPreData -= TPreData;
        YThisData -= TThisData;
        YYearBudget -= TYearBudget;
        //总支出统计
        MarginData = TThisData - TPreData;
        NewRow = BindTable.NewRow();
        NewRow[0] = "<center>二、总支出<center>";
        NewRow[1] = TPreData == 0 ? "" : TPreData.ToString("#,##0.00");
        NewRow[2] = TThisData == 0 ? "" : TThisData.ToString("#,##0.00");
        NewRow[3] = MarginData == 0 ? "" : MarginData.ToString("#,##0.00");
        if (MarginData != 0)
        {
            NewRow[4] = TPreData == 0 ? (MarginData > 0 ? "100%" : "-100%") : (MarginData / TPreData).ToString("P");
        }
        NewRow[5] = TYearBudget == 0 ? "" : TYearBudget.ToString("#,##0.00");
        if (TYearBudget != 0)
        {
            NewRow[6] = TThisData == 0 ? "0.00%" : (TThisData / TYearBudget).ToString("P");
        }
        BindTable.Rows.InsertAt(NewRow, InsertRowPos);
        //输出纯收入统计
        MarginData = YThisData - YPreData;
        NewRow = BindTable.NewRow();
        NewRow[0] = "<center>三、纯收入<center>";
        NewRow[1] = YPreData == 0 ? "" : YPreData.ToString("#,##0.00");
        NewRow[2] = YThisData == 0 ? "" : YThisData.ToString("#,##0.00");
        NewRow[3] = MarginData == 0 ? "" : MarginData.ToString("#,##0.00");
        if (MarginData != 0)
        {
            NewRow[4] = YPreData == 0 ? (MarginData > 0 ? "100%" : "-100%") : (MarginData / YPreData).ToString("P");
        }
        NewRow[5] = YYearBudget == 0 ? "" : YYearBudget.ToString("#,##0.00");
        if (YYearBudget != 0)
        {
            NewRow[6] = YThisData == 0 ? "0.00%" : (YThisData / YYearBudget).ToString("P");
        }
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
        GridView1.Columns[0].ItemStyle.Width = 200;
        GridView1.Columns[1].ItemStyle.Width = 100;
        GridView1.Columns[2].ItemStyle.Width = 100;
        GridView1.Columns[3].ItemStyle.Width = 100;
        GridView1.Columns[4].ItemStyle.Width = 60;
        GridView1.Columns[5].ItemStyle.Width = 100;
        GridView1.Columns[6].ItemStyle.Width = 60;
        GridView1.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
    }

    private void DoInsert(DataTable BindTable, string ItemText, string SubjectNo)
    {
        InsertRow(BindTable, ItemText, SubjectNo);
        DataTable dataTable = CommClass.GetDataTable("select subjectno,subjectname from cw_subject where parentno='" + SubjectNo + "' order by subjectno");
        int i = 1;
        foreach (DataRow row in dataTable.Rows)
        {
            InsertRow(BindTable, string.Format("&nbsp;&nbsp;&nbsp;{0}.&nbsp;{1}", (i++).ToString(), row[1].ToString()), row[0].ToString());
        }
    }

    private void InsertRow(DataTable BindTable, string ItemText, string SubjectNo)
    {
        bool isOnloan = (SysConfigs.ExpenseSubject.IndexOf(SubjectNo.Substring(0, 3)) == -1);
        decimal MarginData = 0;
        decimal YearBudget = GetYearBudget(SubjectNo, QYear.SelectedValue);
        decimal PreData = GetMarginSum(SubjectNo, sYearMonth0, eYearMonth0, isOnloan);
        decimal ThisData = GetMarginSum(SubjectNo, sYearMonth1, eYearMonth1, isOnloan);
        MarginData = ThisData - PreData;
        DataRow NewRow = BindTable.NewRow();
        NewRow[0] = ItemText;
        NewRow[1] = PreData == 0 ? "" : PreData.ToString("#,##0.00");
        NewRow[2] = ThisData == 0 ? "" : ThisData.ToString("#,##0.00");
        NewRow[3] = MarginData == 0 ? "" : MarginData.ToString("#,##0.00");
        if (MarginData != 0)
        {
            NewRow[4] = PreData == 0 ? (MarginData > 0 ? "100%" : "-100%") : (MarginData / PreData).ToString("P");
        }
        NewRow[5] = YearBudget == 0 ? "" : YearBudget.ToString("#,##0.00");
        if (YearBudget != 0)
        {
            NewRow[6] = ThisData == 0 ? "0.00%" : (ThisData / YearBudget).ToString("P");
        }
        BindTable.Rows.Add(NewRow);
        if (SubjectNo.Length == 3)
        {
            TPreData += PreData;
            TThisData += ThisData;
            TYearBudget += YearBudget;
        }
    }

    private decimal GetMarginSum(string SubjectNo, string sYearMonth, string eYearMonth, bool isOnloan)
    {
        return ClsCalculate.GetMarginSum(SubjectNo, sYearMonth, eYearMonth, isOnloan);
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
