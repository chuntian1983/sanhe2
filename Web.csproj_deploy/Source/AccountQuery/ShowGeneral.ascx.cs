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

public partial class AccountQuery_ShowGeneral : System.Web.UI.UserControl
{
    private string _SubjectNo;
    private string _QSMonth;
    private string _QEMonth;
    private string _QYear;
    private string _ReportType;
    private string _PageType;
    private string _IsToExcel = "0";
    private string ReportTitle = string.Empty;

    public string SubjectNo
    {
        set { _SubjectNo = value; }
        get { return _SubjectNo; }
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
        CreateGridView();
    }
    protected void CreateGridView()
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 9; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //创建数据行
        DataRow NewRow;
        if (_SubjectNo == null || _SubjectNo.Length == 0)
        {
            NewRow = BindTable.NewRow();
            NewRow[4] = "上期结转";
            BindTable.Rows.Add(NewRow);
            BindTable.Rows.Add(BindTable.NewRow());
            NewRow = BindTable.NewRow();
            NewRow[4] = "本月合计";
            BindTable.Rows.Add(NewRow);
            NewRow = BindTable.NewRow();
            NewRow[4] = "本年累计";
            BindTable.Rows.Add(NewRow);
        }
        else
        {
            decimal LeadSum = 0;
            decimal OnloanSum = 0;
            decimal FinalBalance = 0;
            string SubjectNo = _SubjectNo;
            ReportTitle = CommClass.GetFieldFromNo(SubjectNo, "SubjectName");
            string sYearMonth = QYear + "年" + QSMonth + "月";
            string eYearMonth = QYear + "年" + QEMonth + "月";
            if (QSMonth != "01")
            {
                int pMonth = Convert.ToInt32(QSMonth) - 1;
                string pYearMonth = QYear + "年" + pMonth.ToString("00") + "月";
                LeadSum = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "leadsum", pYearMonth);
                OnloanSum = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "onloansum", pYearMonth);
            }
            FinalBalance = ClsCalculate.GetSubjectSumDecimal(SubjectNo, "beginbalance", sYearMonth);
            //上月余额结转
            NewRow = BindTable.NewRow();
            NewRow[4] = "上期结转";
            PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
            BindTable.Rows.Add(NewRow);
            //科目明细汇总
            DataSet ds = CommClass.GetDataSet("select * from cw_viewsubjectsum where yearmonth between '" + sYearMonth
                + "' and '" + eYearMonth + "' and SubjectNo='" + SubjectNo + "' order by yearmonth");
            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                DataRowCollection MonthVoucher = CommClass.GetDataRows("select voucherno,voucherdate from cw_voucher where voucherdate like '"
                    + rows["yearmonth"].ToString() + "%' order by voucherno");
                if (MonthVoucher == null) { continue; }
                NewRow = BindTable.NewRow();
                NewRow[0] = MonthVoucher[MonthVoucher.Count - 1]["VoucherDate"].ToString().Substring(5, 2);
                NewRow[1] = MonthVoucher[MonthVoucher.Count - 1]["VoucherDate"].ToString().Substring(8, 2);
                NewRow[2] = "记";
                NewRow[4] = "汇总：" + MonthVoucher[0]["VoucherNo"].ToString() + "～" + MonthVoucher[MonthVoucher.Count - 1]["VoucherNo"].ToString();
                LeadSum = decimal.Parse(rows["lead"].ToString());
                OnloanSum = decimal.Parse(rows["onloan"].ToString());
                FinalBalance = decimal.Parse(rows["finalbalance"].ToString());
                NewRow[5] = LeadSum == 0 ? "" : LeadSum.ToString("#,##0.00");
                NewRow[6] = OnloanSum == 0 ? "" : OnloanSum.ToString("#,##0.00");
                PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
                BindTable.Rows.Add(NewRow);
                OutputCollect(BindTable, "本月合计", LeadSum, OnloanSum, FinalBalance);
                LeadSum = decimal.Parse(rows["leadsum"].ToString());
                OnloanSum = decimal.Parse(rows["onloansum"].ToString());
                FinalBalance = decimal.Parse(rows["finalbalance"].ToString());
                OutputCollect(BindTable, "本年累计", LeadSum, OnloanSum, FinalBalance);
            }
            //账前账查询，统计当月凭证
            string ThisYearMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
            if (string.Compare(sYearMonth, ThisYearMonth) <= 0 && string.Compare(ThisYearMonth, eYearMonth) <= 0 && ds.Tables[0].Rows.Count == 0)
            {
                DataRowCollection VoucherEntry = CommClass.GetDataRows("select voucherno,voucherdate,subjectno,summoney from cw_voucherentry where voucherdate like '"
                    + ThisYearMonth + "%' order by voucherno");
                if (VoucherEntry != null)
                {
                    LeadSum = 0;
                    OnloanSum = 0;
                    decimal BeginBalance = 0;
                    decimal.TryParse(CommClass.GetFieldFromNo(SubjectNo, "BeginBalance"), out BeginBalance);
                    NewRow = BindTable.NewRow();
                    NewRow[0] = VoucherEntry[VoucherEntry.Count - 1]["VoucherDate"].ToString().Substring(5, 2);
                    NewRow[1] = VoucherEntry[VoucherEntry.Count - 1]["VoucherDate"].ToString().Substring(8, 2);
                    NewRow[2] = "记";
                    NewRow[4] = "汇总：" + VoucherEntry[0]["VoucherNo"].ToString() + "～" + VoucherEntry[VoucherEntry.Count - 1]["VoucherNo"].ToString();
                    for (int i = 0; i < VoucherEntry.Count; i++)
                    {
                        if (VoucherEntry[i]["subjectno"].ToString().StartsWith(SubjectNo))
                        {
                            if (VoucherEntry[i]["summoney"].ToString().StartsWith("-"))
                            {
                                OnloanSum -= decimal.Parse(VoucherEntry[i]["summoney"].ToString());
                            }
                            else
                            {
                                LeadSum += decimal.Parse(VoucherEntry[i]["summoney"].ToString());
                            }
                        }
                    }
                    FinalBalance = BeginBalance + LeadSum - OnloanSum;
                    NewRow[5] = LeadSum == 0 ? "" : LeadSum.ToString("#,##0.00");
                    NewRow[6] = OnloanSum == 0 ? "" : OnloanSum.ToString("#,##0.00");
                    PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
                    BindTable.Rows.Add(NewRow);
                    OutputCollect(BindTable, "本月合计", LeadSum, OnloanSum, FinalBalance);
                    OutputCollect(BindTable, "本年累计", LeadSum, OnloanSum, FinalBalance);
                }
            }
            //上月有余额本月无累计
            if (BindTable.Rows.Count == 1)
            {
                BindTable.Rows.Add(BindTable.NewRow());
                NewRow = BindTable.NewRow();
                NewRow[4] = "本月合计";
                BindTable.Rows.Add(NewRow);
                OutputCollect(BindTable, "本年累计", LeadSum, OnloanSum, FinalBalance);
            }
        }
        BindTable.AcceptChanges();
        //输出报表
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
    private void ShowReportPage(DataTable BindTable, string DesignID)
    {
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "100008";
        reportPage.DesignID = DesignID;
        reportPage.ReportTitle = ReportTitle;
        reportPage.ReportDate = QYear;
        reportPage.BindTable = BindTable;
        reportPage.ShowPageContent = ShowPageContent;
        reportPage.PageType = PageType;
        reportPage.IsToExcel = IsToExcel;
        reportPage.ShowReportPage();
    }
}
