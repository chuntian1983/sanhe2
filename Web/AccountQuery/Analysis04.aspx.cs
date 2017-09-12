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
using System.Reflection;

public partial class AccountQuery_Analysis04 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Request.QueryString["QType"])
        {
            case "1":
                ReportTitle.Value = "村级支出情况";
                if (!PageClass.CheckVisitQuot("000012$100015")) { return; }
                break;
            case "2":
                ReportTitle.Value = "村级福利费收入情况";
                if (!PageClass.CheckVisitQuot("000012$100016")) { return; }
                break;
            case "3":
                ReportTitle.Value = "村级福利费支出情况";
                if (!PageClass.CheckVisitQuot("000012$100017")) { return; }
                break;
            default:
                ReportTitle.Value = "村级收入情况";
                if (!PageClass.CheckVisitQuot("000012$100018")) { return; }
                break;
        }
        if (!IsPostBack)
        {
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            GetCommDetailInfo();
            //写入操作日志
            CommClass.WriteCTL_Log("100017", "报表分析：" + ReportTitle.Value);
            //--
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }
    protected void GetCommDetailInfo()
    {
        string QSubjectNo = string.Empty;
        string OutputMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue;
        if (QSMonth.SelectedValue != QEMonth.SelectedValue)
        {
            OutputMonth += "-" + QEMonth.SelectedValue;
        }
        ReportDate.Value = OutputMonth + "月";
        switch (Request.QueryString["QType"])
        {
            case "1":
                QSubjectNo = "SubjectNo like '502%' or SubjectNo like '541%' or SubjectNo like '551%'";
                break;
            case "2":
                QSubjectNo = "SubjectNo like '21201%' or SubjectNo like '21202%'";
                break;
            case "3":
                QSubjectNo = "SubjectNo like '212%' and SubjectNo<>'212' and SubjectNo not like '21201%' and SubjectNo not like '21202%'";
                break;
            default:
                QSubjectNo = "SubjectNo like '501%' or SubjectNo like '511%' or SubjectNo like '522%' or SubjectNo like '531%' or SubjectNo like '561%'";
                break;
        }
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 4; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //输出明细
        DataRow NewRow;
        decimal MonthSumMoney = 0;
        string sYearMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月";
        string eYearMonth = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月";
        DataTable ve = CommClass.GetDataTable("select VoucherDate,VoucherNo,VoucherID,VSummary,SumMoney from CW_VoucherEntry where voucherdate between '"
            + sYearMonth + "01日' and '" + eYearMonth + "31日' and (" + QSubjectNo + ") order by left(VoucherDate,8),VoucherNo");
        if (ve.Rows.Count > 0)
        {
            string CurrentMonth = string.Empty;
            foreach (DataRow row in ve.Rows)
            {
                bool DFlag = row["SumMoney"].ToString().IndexOf("-") == -1;
                if ((Request.QueryString["QType"] == "1" || Request.QueryString["QType"] == "3") ? !DFlag : DFlag)
                {
                    continue;
                }
                if (CurrentMonth.Length > 0 && CurrentMonth != row["VoucherDate"].ToString().Substring(0, 8))
                {
                    NewRow = BindTable.NewRow();
                    NewRow[0] = "&nbsp;";
                    NewRow[1] = "&nbsp;";
                    NewRow[2] = "<center>本月合计</center>";
                    NewRow[3] = MonthSumMoney.ToString("#,##0.00");
                    BindTable.Rows.Add(NewRow);
                    MonthSumMoney = 0;
                }
                NewRow = BindTable.NewRow();
                NewRow[0] = row["VoucherDate"].ToString();
                NewRow[1] = row["VoucherNo"].ToString() + "|" + row["VoucherID"].ToString();
                NewRow[2] = row["VSummary"].ToString();
                decimal SumMoney = Math.Abs(decimal.Parse(row["SumMoney"].ToString()));
                NewRow[3] = SumMoney.ToString("#,##0.00");
                MonthSumMoney += SumMoney;
                BindTable.Rows.Add(NewRow);
                CurrentMonth = row["VoucherDate"].ToString().Substring(0, 8);
            }
        }
        NewRow = BindTable.NewRow();
        NewRow[0] = "&nbsp;";
        NewRow[1] = "&nbsp;";
        NewRow[2] = "<center>本月合计</center>";
        NewRow[3] = MonthSumMoney.ToString("#,##0.00");
        BindTable.Rows.Add(NewRow);
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "100001";
        reportPage.DesignID = "000015";
        reportPage.ReportTitle = ReportTitle.Value;
        reportPage.ReportDate = ReportDate.Value;
        reportPage.BindTable = BindTable;
        reportPage.ShowPageContent = ShowPageContent;
        if (ViewState["IsToExcel"] == (object)"1")
        {
            reportPage.IsToExcel = "1";
            ViewState["IsToExcel"] = "0";
        }
        reportPage.ShowReportPage();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        ViewState["IsToExcel"] = "1";
        GetCommDetailInfo();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
