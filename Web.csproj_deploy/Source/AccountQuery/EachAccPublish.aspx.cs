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

public partial class AccountQuery_EachAccPublish : System.Web.UI.Page
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
            CommClass.WriteCTL_Log("100016", "报表查询：财务逐笔公开榜");
            //--
        }
    }
    protected void InitWebControl()
    {
        DataRow NewRow;
        decimal FinalBalance = 0;
        string ThisYearMonth = SelYear.SelectedValue + "年" + SelMonth.SelectedValue + "月";
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 9; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //上月余额结转
        switch (QType.SelectedValue)
        {
            case "0":
                FinalBalance += ClsCalculate.GetSubjectSumDecimal("101", "beginbalance", ThisYearMonth);
                break;
            case "1":
                FinalBalance += ClsCalculate.GetSubjectSumDecimal("102", "beginbalance", ThisYearMonth);
                break;
            case "2":
                DataTable DT = CommClass.GetDataTable("select subjectno from cw_subject where subjecttype='5' and parentno='000'");
                foreach (DataRow row in DT.Rows)
                {
                    FinalBalance += ClsCalculate.GetSubjectSumDecimal(row["subjectno"].ToString(), "beginbalance", ThisYearMonth);
                }
                break;
        }
        OutputCollect(BindTable, "上期结转", 0, 0, FinalBalance);
        //科目明细汇总
        string whereSubjectNo;
        switch (QType.SelectedValue)
        {
            case "1":
                whereSubjectNo = "and SubjectNo like '102%'";
                break;
            case "2":
                whereSubjectNo = "and SubjectNo like '5%'";
                break;
            default:
                whereSubjectNo = "and SubjectNo like '101%'";
                break;
        }
        DataTable dt = CommClass.GetDataTable("select * from CW_VoucherEntry where voucherdate like '" + ThisYearMonth + "%' " + whereSubjectNo + " order by id");
        if (dt.Rows.Count > 0)
        {
            decimal SumMoney = 0;
            decimal MLeadSum = 0;
            decimal MOnloanSum = 0;
            DataRowCollection rows = dt.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
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
                    MLeadSum += SumMoney;
                }
                else
                {
                    NewRow[6] = (-SumMoney).ToString("#,##0.00");
                    MOnloanSum -= SumMoney;
                }
                FinalBalance += SumMoney;
                PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
                BindTable.Rows.Add(NewRow);
            }
            OutputCollect(BindTable, "本月合计", MLeadSum, MOnloanSum, FinalBalance);
        }
        else
        {
            BindTable.Rows.Add(BindTable.NewRow());
            OutputCollect(BindTable, "本月合计", 0, 0, FinalBalance);
        }
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = (QType.SelectedIndex == 2 ? "100003" : "100002");
        reportPage.DesignID = "000001";
        reportPage.ReportTitle = QType.SelectedItem.Text;
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
        NewRow[4] = Summary;
        NewRow[5] = Lead == 0 ? "" : Lead.ToString("#,##0.00");
        NewRow[6] = Onloan == 0 ? "" : Onloan.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, FinalBalance, 7, 8);
        BindTable.Rows.Add(NewRow);
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
