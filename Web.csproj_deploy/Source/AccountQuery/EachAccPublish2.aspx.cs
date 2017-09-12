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

public partial class AccountQuery_EachAccPublish2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100014")) { return; }
        if (!IsPostBack)
        {
            DateTime AccountDate = MainClass.GetAccountDate();
            MainClass.InitAccountYear(SelYear);
            SelYear.Text = AccountDate.Year.ToString();
            SelMonth.Text = AccountDate.Month.ToString("00");
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：现金、银行存款逐笔公开榜");
            //--
        }
    }
    protected void InitWebControl()
    {
        DataRow NewRow;
        string ThisYearMonth = SelYear.SelectedValue + "年" + SelMonth.SelectedValue + "月";
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 9; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //上月余额结转
        decimal FinalBalance = 0;
        FinalBalance += ClsCalculate.GetSubjectSumDecimal("101", "beginbalance", ThisYearMonth);
        FinalBalance += ClsCalculate.GetSubjectSumDecimal("102", "beginbalance", ThisYearMonth);
        OutputCollect(BindTable, "上月结转", 0, 0, FinalBalance);
        //科目明细汇总
        string whereSubjectNo = "and (SubjectNo like '101%' or SubjectNo like '102%')";
        DataTable dt = CommClass.GetDataTable("select * from CW_VoucherEntry where voucherdate like '" + ThisYearMonth + "%' " + whereSubjectNo + " order by id");
        if (dt.Rows.Count > 0)
        {
            decimal SumMoney = 0;
            decimal MLeadSum = 0;
            decimal MOnloanSum = 0;
            bool hasOutMonth = false;
            DataRowCollection rows = dt.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                NewRow = BindTable.NewRow();
                if (!hasOutMonth)
                {
                    NewRow[0] = rows[i]["VoucherDate"].ToString().Substring(5, 2);
                    hasOutMonth = true;
                }
                NewRow[1] = rows[i]["VoucherNo"].ToString() + "|" + rows[i]["VoucherID"].ToString();
                NewRow[2] = rows[i]["VSummary"].ToString();
                SumMoney = decimal.Parse(rows[i]["SumMoney"].ToString());
                if (SumMoney > 0)
                {
                    NewRow[3] = SumMoney.ToString("#,##0.00");
                    MLeadSum += SumMoney;
                }
                else
                {
                    NewRow[4] = (-SumMoney).ToString("#,##0.00");
                    MOnloanSum -= SumMoney;
                }
                FinalBalance += SumMoney;
                BindTable.Rows.Add(NewRow);
            }
            OutputCollect(BindTable, "<center>合&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;计</center>", MLeadSum, MOnloanSum, FinalBalance);
        }
        else
        {
            BindTable.Rows.Add(BindTable.NewRow());
            OutputCollect(BindTable, "<center>合&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;计</center>", 0, 0, FinalBalance);
        }
        //输出报表分页
        string townName = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", UserInfo.UnitID));
        ReportTitle.InnerHtml = string.Concat(townName, UserInfo.AccountName, "<br>", ThisYearMonth, "财务公开榜");
        DoBill.InnerHtml = string.Concat("制表：", townName, "会计服务中心");
        OpenDate.InnerHtml = string.Concat("张榜日期：", DateTime.Now.ToLongDateString());
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "100019";
        reportPage.DesignID = "000019";
        reportPage.PageType = "1";
        reportPage.ReportTitle = "财务公开榜";
        reportPage.ReportDate = ThisYearMonth;
        reportPage.BindTable = BindTable;
        reportPage.ShowPageContent = ShowPageContent;
        if (ViewState["IsToExcel"] == (object)"1")
        {
            reportPage.IsToExcel = "1";
            ViewState["IsToExcel"] = "0";
        }
        reportPage.ShowReportPage();
    }
    protected void OutputCollect(DataTable BindTable, string Summary, decimal Lead, decimal Onloan, decimal FinalBalance)
    {
        DataRow NewRow = BindTable.NewRow();
        NewRow[2] = Summary;
        NewRow[3] = Lead == 0 ? "" : Lead.ToString("#,##0.00");
        NewRow[4] = Onloan == 0 ? "" : Onloan.ToString("#,##0.00");
        NewRow[5] = FinalBalance.ToString("#,##0.00");
        BindTable.Rows.Add(NewRow);
    }
    public void DoBalance(DataRow dataRow, decimal BalanceValue, int DPos, int VPos)
    {
        decimal _BalanceValue = Math.Round(BalanceValue, 2);
        if (_BalanceValue == 0)
        {
            dataRow[VPos] = "0.00";
        }
        else if (_BalanceValue > 0)
        {
            dataRow[VPos] = _BalanceValue.ToString("#,##0.00");
        }
        else
        {
            dataRow[VPos] = _BalanceValue.ToString("#,##0.00").Replace("-", "");
        }
    }
    protected void QData_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        ViewState["IsToExcel"] = "1";
        InitWebControl();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
