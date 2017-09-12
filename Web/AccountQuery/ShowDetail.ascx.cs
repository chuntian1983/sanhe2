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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

public partial class AccountQuery_ShowDetail : System.Web.UI.UserControl
{
    private string _SubjectNo;
    private string _QSDay;
    private string _QEDay;
    private string _QSMonth;
    private string _QEMonth;
    private string _QYear;
    private string _ReportType;
    private string _PageType;
    private string _IsToExcel = "0";
    private string ReportTitle = string.Empty;
    private bool isDayAccount = false;

    public string SubjectNo
    {
        set { _SubjectNo = value; }
        get { return _SubjectNo; }
    }

    public string QSDay
    {
        set { _QSDay = value; }
        get { return _QSDay; }
    }

    public string QEDay
    {
        set { _QEDay = value; }
        get { return _QEDay; }
    }

    public string QSMonth
    {
        set { _QSMonth = value; }
        get { return _QSMonth; }
    }

    public string QEMonth
    {
        set { _QEMonth = value; }
        get { return _QEMonth; }
    }

    public string QYear
    {
        set { _QYear = value; }
        get { return _QYear; }
    }

    public string ReportType
    {
        set { _ReportType = value; }
        get { return _ReportType; }
    }

    public string PageType
    {
        set { _PageType = value; }
        get { return _PageType; }
    }

    public string IsToExcel
    {
        set { _IsToExcel = value; }
        get { return _IsToExcel; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetDetailInfo();
    }
    protected void GetDetailInfo()
    {
        if (_SubjectNo == null || _SubjectNo.Length == 0)
        {
            DataTable BindTable = new DataTable();
            for (int i = 0; i < 9; i++)
            {
                BindTable.Columns.Add("F" + i.ToString());
            }
            DataRow NewRow = BindTable.NewRow();
            NewRow[4] = "上期结转";
            BindTable.Rows.Add(NewRow);
            BindTable.Rows.Add(BindTable.NewRow());
            NewRow = BindTable.NewRow();
            NewRow[4] = "本月合计";
            BindTable.Rows.Add(NewRow);
            NewRow = BindTable.NewRow();
            NewRow[4] = "本年累计";
            BindTable.Rows.Add(NewRow);
            string MSubjectTitle = PageClass.PadLeftM("", 35, "&nbsp;");
            string DSubjectTitle = PageClass.PadLR("科&nbsp;&nbsp;目", 16, "&nbsp;");
            ReportTitle = string.Concat(MSubjectTitle, "$", DSubjectTitle);
            ShowReportPage(BindTable, "000001");
        }
        else
        {
            string SubjectNo = _SubjectNo;
            string SubjectName = CommClass.GetFieldFromNo(SubjectNo, "SubjectName");
            string ParentSName = string.Empty;
            string MSubjectTitle = string.Empty;
            string DSubjectTitle = string.Empty;
            if (SubjectNo.Length >= 3)
            {
                ParentSName = CommClass.GetFieldFromNo(SubjectNo.Substring(0, 3), "subjectname");
                isDayAccount = (SubjectNo.StartsWith("101") || SubjectNo.StartsWith("102"));
                if (isDayAccount)
                {
                    MSubjectTitle = "日&nbsp;&nbsp;&nbsp;&nbsp;记&nbsp;&nbsp;&nbsp;&nbsp;账";
                }
                else
                {
                    if ((SubjectNo.StartsWith("131") || SubjectNo.StartsWith("132") || SubjectNo.StartsWith("151")))
                    {
                        MSubjectTitle = ParentSName + "&nbsp;&nbsp;&nbsp;&nbsp;明细分类账";
                    }
                    else
                    {
                        MSubjectTitle = SubjectName + "&nbsp;&nbsp;&nbsp;&nbsp;明细分类账";
                    }
                }
            }
            else
            {
                ParentSName = SubjectName;
                MSubjectTitle = SubjectName + "&nbsp;&nbsp;&nbsp;&nbsp;明细分类账";
            }
            DSubjectTitle = ParentSName + "&nbsp;&nbsp;科&nbsp;&nbsp;目&nbsp;&nbsp;" + SubjectName;
            ReportTitle = string.Concat(MSubjectTitle, "$", DSubjectTitle);
            if (CommClass.GetFieldFromNo(SubjectNo, "AccountType") == "1")
            {
                GetAmountDetailInfo(SubjectNo);
            }
            else
            {
                if (QYear == "-")
                {
                    GetDayDetailInfo(SubjectNo);
                }
                else
                {
                    GetCommDetailInfo(SubjectNo);
                }
            }
        }
    }
    protected void GetDayDetailInfo(string SubjectNo)
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 9; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //创建数据行
        DataRow NewRow;
        decimal YLeadSum = 0;
        decimal YOnloanSum = 0;
        decimal FinalBalance = 0;
        string[] arrPayType = { "现付", "现收", "银付", "银收", "无" };
        string wh = string.Concat("(AccSubjectNo like '", SubjectNo, "%') and VoucherDate<'", QSDay, "'");
        YLeadSum = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_dayaccount", "sum(AccMoney)", wh + " and AccMoney>0", "0"), 0);
        YOnloanSum = 0 - TypeParse.StrToDecimal(CommClass.GetTableValue("cw_dayaccount", "sum(AccMoney)", wh + " and AccMoney<0", "0"), 0);
        FinalBalance = YLeadSum - YOnloanSum;
        //上月余额结转
        NewRow = BindTable.NewRow();
        NewRow[4] = "上期结转";
        NewRow[5] = YLeadSum.ToString("#,##0.00");
        NewRow[6] = YOnloanSum.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
        BindTable.Rows.Add(NewRow);
        //科目明细汇总
        DataSet ds = CommClass.GetDataSet(string.Concat("select * from cw_dayaccount where (AccSubjectNo like '", SubjectNo, "%') and VoucherDate>='", QSDay, "' and VoucherDate<='", QEDay, "' order by VoucherDate"));
        if (ds.Tables[0].Rows.Count > 0)
        {
            decimal SumMoney = 0;
            decimal DLeadSum = 0;
            decimal DOnloanSum = 0;
            decimal MLeadSum = 0;
            decimal MOnloanSum = 0;
            DataRowCollection rows = ds.Tables[0].Rows;
            string CurrentDay = rows[0]["VoucherDate"].ToString();
            string CurrentMonth = rows[0]["VoucherDate"].ToString().Substring(5, 2);
            for (int i = 0; i < rows.Count; i++)
            {
                //输出合计累计数据
                if (CurrentDay != rows[i]["VoucherDate"].ToString())
                {
                    OutputCollect(BindTable, "本日合计", DLeadSum, DOnloanSum, FinalBalance);
                    DLeadSum = 0;
                    DOnloanSum = 0;
                    CurrentDay = rows[i]["VoucherDate"].ToString();
                }
                if (CurrentMonth != rows[i]["VoucherDate"].ToString().Substring(5, 2))
                {
                    OutputCollect(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance);
                    YLeadSum += MLeadSum;
                    YOnloanSum += MOnloanSum;
                    //OutputCollect(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance);
                    MLeadSum = 0;
                    MOnloanSum = 0;
                    CurrentMonth = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                }
                //输出凭证分录数据
                NewRow = BindTable.NewRow();
                NewRow[0] = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                NewRow[1] = rows[i]["VoucherDate"].ToString().Substring(8, 2);
                int at = TypeParse.StrToInt(rows[i]["VoucherType"].ToString(), 4);
                if (at > 4)
                {
                    at = 4;
                }
                NewRow[2] = arrPayType[at];
                NewRow[3] = rows[i]["VoucherNo"].ToString() + "|" + rows[i]["id"].ToString();
                NewRow[4] = rows[i]["Notes"].ToString();
                SumMoney = decimal.Parse(rows[i]["AccMoney"].ToString());
                if (SumMoney > 0)
                {
                    NewRow[5] = SumMoney.ToString("#,##0.00");
                    DLeadSum += SumMoney;
                    MLeadSum += SumMoney;
                }
                else
                {
                    NewRow[6] = (-SumMoney).ToString("#,##0.00");
                    DOnloanSum -= SumMoney;
                    MOnloanSum -= SumMoney;
                }
                FinalBalance += decimal.Parse(rows[i]["AccMoney"].ToString());
                PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
                BindTable.Rows.Add(NewRow);
            }
            //输出最后一个月的合计
            OutputCollect(BindTable, "本日合计", DLeadSum, DOnloanSum, FinalBalance);
            OutputCollect(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance);
            YLeadSum += MLeadSum;
            YOnloanSum += MOnloanSum;
            OutputCollect(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance);
        }
        else
        {
            BindTable.Rows.Add(BindTable.NewRow());
            NewRow = BindTable.NewRow();
            NewRow[4] = "本月合计";
            BindTable.Rows.Add(NewRow);
            OutputCollect(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance);
        }
        if (ReportType == "0")
        {
            ShowReportPage(BindTable, "010001");
        }
        else
        {
            ShowReportPage(BindTable, "010017");
        }
    }
    protected void GetCommDetailInfo(string SubjectNo)
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 9; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //创建数据行
        DataRow NewRow;
        decimal YLeadSum = 0;
        decimal YOnloanSum = 0;
        decimal FinalBalance = 0;
        string sYearMonth = QYear + "年" + QSMonth + "月";
        string eYearMonth = QYear + "年" + QEMonth + "月";
        if (QSMonth != "01")
        {
            int pMonth = Convert.ToInt32(QSMonth) - 1;
            string pYearMonth = QYear + "年" + pMonth.ToString("00") + "月";
            YLeadSum = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "leadsum", pYearMonth);
            YOnloanSum = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "onloansum", pYearMonth);
        }
        FinalBalance = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "beginbalance", sYearMonth);
        //上月余额结转
        NewRow = BindTable.NewRow();
        NewRow[4] = "上期结转";
        PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
        BindTable.Rows.Add(NewRow);
        //科目明细汇总
        DataSet ds = CommClass.GetDataSet("select * from CW_VoucherEntry where voucherdate between '"
            + sYearMonth + "01日' and '" + eYearMonth + "31日' and SubjectNo like '" + SubjectNo + "%' order by left(voucherdate,8),voucherno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            decimal SumMoney = 0;
            decimal DLeadSum = 0;
            decimal DOnloanSum = 0;
            decimal MLeadSum = 0;
            decimal MOnloanSum = 0;
            DataRowCollection rows = ds.Tables[0].Rows;
            string CurrentDay = rows[0]["VoucherDate"].ToString();
            string CurrentMonth = rows[0]["VoucherDate"].ToString().Substring(5, 2);
            for (int i = 0; i < rows.Count; i++)
            {
                //输出合计累计数据
                if (isDayAccount)
                {
                    if (CurrentDay != rows[i]["VoucherDate"].ToString())
                    {
                        OutputCollect(BindTable, "本日合计", DLeadSum, DOnloanSum, FinalBalance);
                        DLeadSum = 0;
                        DOnloanSum = 0;
                        CurrentDay = rows[i]["VoucherDate"].ToString();
                    }
                }
                if (CurrentMonth != rows[i]["VoucherDate"].ToString().Substring(5, 2))
                {
                    OutputCollect(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance);
                    YLeadSum += MLeadSum;
                    YOnloanSum += MOnloanSum;
                    //OutputCollect(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance);
                    MLeadSum = 0;
                    MOnloanSum = 0;
                    CurrentMonth = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                }
                //输出凭证分录数据
                NewRow = BindTable.NewRow();
                NewRow[0] = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                NewRow[1] = rows[i]["VoucherDate"].ToString().Substring(8, 2);
                NewRow[2] = "记";
                NewRow[3] = rows[i]["VoucherNo"].ToString() + "|" + rows[i]["VoucherID"].ToString();
                NewRow[4] = rows[i]["VSummary"].ToString();
                SumMoney = decimal.Parse(rows[i]["SumMoney"].ToString());
                if (SumMoney > 0)
                {
                    NewRow[5] = SumMoney.ToString("#,##0.00");
                    DLeadSum += SumMoney;
                    MLeadSum += SumMoney;
                }
                else
                {
                    NewRow[6] = (-SumMoney).ToString("#,##0.00");
                    DOnloanSum -= SumMoney;
                    MOnloanSum -= SumMoney;
                }
                FinalBalance += decimal.Parse(rows[i]["SumMoney"].ToString());
                PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
                BindTable.Rows.Add(NewRow);
            }
            //输出最后一个月的合计
            if (isDayAccount)
            {
                OutputCollect(BindTable, "本日合计", DLeadSum, DOnloanSum, FinalBalance);
            }
            OutputCollect(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance);
            YLeadSum += MLeadSum;
            YOnloanSum += MOnloanSum;
            OutputCollect(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance);
        }
        else
        {
            BindTable.Rows.Add(BindTable.NewRow());
            NewRow = BindTable.NewRow();
            NewRow[4] = "本月合计";
            BindTable.Rows.Add(NewRow);
            OutputCollect(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance);
        }
        if (ReportType == "0")
        {
            ShowReportPage(BindTable, "000001");
        }
        else
        {
            ShowReportPage(BindTable, "000017");
        }
    }
    protected void OutputCollect(DataTable BindTable, string Summary, decimal Lead, decimal Onloan, decimal FinalBalance)
    {
        DataRow NewRow = BindTable.NewRow();
        NewRow[4] = Summary;
        NewRow[5] = Lead == 0 ? "" : Lead.ToString("#,##0.00");
        NewRow[6] = Onloan == 0 ? "" : Onloan.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
        BindTable.Rows.Add(NewRow);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //数据金额账
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 数量金额张明细查询
    /// </summary>
    protected void GetAmountDetailInfo(string SubjectNo)
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 15; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //创建数据行
        DataRow NewRow;
        decimal YLeadSum = 0;
        decimal YOnloanSum = 0;
        decimal FinalBalance = 0;
        decimal YLeadSumA = 0;
        decimal YOnloanSumA = 0;
        decimal FinalBalanceA = 0;
        string sYearMonth = QYear + "年" + QSMonth + "月";
        string eYearMonth = QYear + "年" + QEMonth + "月";
        //上月余额结转
        NewRow = BindTable.NewRow();
        NewRow[4] = "上期结转";
        string SubjectID = CommClass.GetFieldFromNo(SubjectNo, "id");
        string BeginAmount = CommClass.GetTableValue("cw_subjectdata", "sum(amount)", "subjectid in (select id from cw_subject where subjectno like '" + SubjectNo + "%')");
        if (FinalBalance >= 0)
        {
            YLeadSumA += TypeParse.StrToDecimal(BeginAmount, 0);
        }
        else
        {
            YOnloanSumA += TypeParse.StrToDecimal(BeginAmount, 0);
        }
        //上月入出库汇总
        DataRowCollection PreDataRows = CommClass.GetDataRows("select amount,summoney from cw_voucherentrydata where subjectno like '" + SubjectNo + "%' and VoucherDate<'" + sYearMonth + "01日'");
        if (PreDataRows != null)
        {
            foreach (DataRow arow in PreDataRows)
            {
                decimal amount = decimal.Parse(arow["amount"].ToString());
                if (arow["summoney"].ToString().StartsWith("-"))
                {
                    YOnloanSumA += amount;
                }
                else
                {
                    YLeadSumA += amount;
                }
            }
        }
        FinalBalanceA += YLeadSumA - YOnloanSumA;
        NewRow[12] = FinalBalanceA.ToString("#,##0.00");
        YLeadSum = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "leadsum", sYearMonth);
        YOnloanSum = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "onloansum", sYearMonth);
        FinalBalance = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "beginbalance", sYearMonth);
        PageClass.DoBalance(NewRow, FinalBalance, 11, 14);
        BindTable.Rows.Add(NewRow);
        //科目明细汇总
        DataSet ds = CommClass.GetDataSet(string.Concat("select id,VoucherID,VoucherNo,VoucherDate,ESummary,Amount,Balance,SumMoney from CW_VoucherEntryData where voucherdate between '",
            sYearMonth, "01日' and '", eYearMonth, "31日' and SubjectNo like '", SubjectNo, "%' order by left(voucherdate,8),voucherno,id"));
        if (ds.Tables[0].Rows.Count > 0)
        {
            decimal MLeadSum = 0;
            decimal MOnloanSum = 0;
            decimal MLeadSumA = 0;
            decimal MOnloanSumA = 0;
            DataRowCollection rows = ds.Tables[0].Rows;
            string CurrentMonth = rows[0]["VoucherDate"].ToString().Substring(5, 2);
            for (int i = 0; i < rows.Count; i++)
            {
                //输出合计累计数据
                if (CurrentMonth != rows[i]["VoucherDate"].ToString().Substring(5, 2))
                {
                    OutputCollect2(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance, MLeadSumA, MOnloanSumA, FinalBalanceA);
                    YLeadSum += MLeadSum;
                    YOnloanSum += MOnloanSum;
                    YLeadSumA += MLeadSumA;
                    YOnloanSumA += MOnloanSumA;
                    //OutputCollect2(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance, YLeadSumA, YOnloanSumA, FinalBalanceA);
                    MLeadSum = 0;
                    MOnloanSum = 0;
                    MLeadSumA = 0;
                    MOnloanSumA = 0;
                    CurrentMonth = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                }
                //输出凭证分录数据
                NewRow = BindTable.NewRow();
                NewRow[0] = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                NewRow[1] = rows[i]["VoucherDate"].ToString().Substring(8, 2);
                NewRow[2] = "记";
                NewRow[3] = rows[i]["VoucherNo"].ToString() + "|" + rows[i]["VoucherID"].ToString();
                NewRow[4] = rows[i]["ESummary"].ToString();
                decimal balance = decimal.Parse(rows[i]["balance"].ToString());
                decimal amount = decimal.Parse(rows[i]["amount"].ToString());
                decimal price = 0;
                if (amount > 0)
                {
                    price = balance / amount;
                }
                if (rows[i]["SumMoney"].ToString().IndexOf("-") == -1)
                {
                    NewRow[5] = amount.ToString("#,##0.00");
                    NewRow[6] = price.ToString("#,##0.00");
                    MLeadSumA += amount;
                    FinalBalanceA += amount;
                    NewRow[7] = balance.ToString("#,##0.00");
                    MLeadSum += balance;
                    FinalBalance += balance;
                }
                else
                {
                    NewRow[8] = amount.ToString("#,##0.00");
                    NewRow[9] = price.ToString("#,##0.00");
                    MOnloanSumA += amount;
                    FinalBalanceA -= amount;
                    NewRow[10] = balance.ToString("#,##0.00");
                    MOnloanSum += balance;
                    FinalBalance -= balance;
                }
                NewRow[12] = FinalBalanceA.ToString("#,##0.00");
                PageClass.DoBalance(NewRow, FinalBalance, 11, 14);
                BindTable.Rows.Add(NewRow);
            }
            //输出最后一个月的合计
            OutputCollect2(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance, MLeadSumA, MOnloanSumA, FinalBalanceA);
            YLeadSum += MLeadSum;
            YOnloanSum += MOnloanSum;
            YLeadSumA += MLeadSumA;
            YOnloanSumA += MOnloanSumA;
            OutputCollect2(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance, YLeadSumA, YOnloanSumA, FinalBalanceA);
        }
        else
        {
            BindTable.Rows.Add(BindTable.NewRow());
            NewRow = BindTable.NewRow();
            NewRow[4] = "本月合计";
            BindTable.Rows.Add(NewRow);
            OutputCollect2(BindTable, "本年累计", YLeadSum, YOnloanSum, FinalBalance, YLeadSumA, YOnloanSumA, FinalBalanceA);
        }
        ShowReportPage(BindTable, "000009");
    }
    protected void OutputCollect2(DataTable BindTable, string Summary, decimal Lead, decimal Onloan, decimal FinalBalance, decimal LeadA, decimal OnloanA, decimal FinalBalanceA)
    {
        DataRow NewRow = BindTable.NewRow();
        NewRow[4] = Summary;
        NewRow[5] = LeadA.ToString("#,##0.00");
        NewRow[7] = Lead.ToString("#,##0.00");
        NewRow[8] = OnloanA.ToString("#,##0.00");
        NewRow[10] = Onloan.ToString("#,##0.00");
        NewRow[12] = FinalBalanceA.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, FinalBalance, 11, 14);
        BindTable.Rows.Add(NewRow);
    }
    private void ShowReportPage(DataTable BindTable, string DesignID)
    {
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "100004";
        reportPage.DesignID = DesignID;
        reportPage.ReportTitle = ReportTitle;
        if (QYear == "-")
        {
            reportPage.ReportDate = QSDay.Substring(0, 4);
        }
        else
        {
            reportPage.ReportDate = QYear;
        }
        reportPage.BindTable = BindTable;
        reportPage.ShowPageContent = ShowPageContent;
        reportPage.PageType = PageType;
        reportPage.IsToExcel = IsToExcel;
        reportPage.ShowReportPage();
    }
}
