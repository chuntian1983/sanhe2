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
using System.Text;

public partial class AccountQuery_SubjectBalanceDay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100006")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            QDate.Attributes.Add("readonly", "readonly");
            QDate.Text = MainClass.GetAccountDate().ToString("yyyy年MM月dd日");
            QDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].QDate,'yyyy年mm月dd日')");
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100014", "账簿查询：科目余额表");
            //--
        }
    }
    protected void InitWebControl()
    {
        //创建数据表
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 8; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        InitBindTable(BindTable);
        for (int i = BindTable.Rows.Count - 1; i < 10; i++)
        {
            if (BindTable.Rows.Count > 1)
            {
                BindTable.Rows.InsertAt(BindTable.NewRow(), BindTable.Rows.Count - 2);
            }
            else
            {
                BindTable.Rows.Add(BindTable.NewRow());
            }
        }
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "000000";
        reportPage.DesignID = "000002";
        reportPage.ReportTitle = "科 目 余 额 表";
        reportPage.ReportDate = QDate.Text;
        reportPage.BindTable = BindTable;
        reportPage.ShowPageContent = ShowPageContent;
        if (ViewState["IsToExcel"] == (object)"1")
        {
            reportPage.IsToExcel = "1";
            ViewState["IsToExcel"] = "0";
        }
        reportPage.ShowReportPage();
    }
    protected void InitBindTable(DataTable BindTable)
    {
        string EDate = QDate.Text;
        string ThisYearMonth = QDate.Text.Substring(0, 8);
        DateTime AccountDate = MainClass.GetAccountDate();
        if (string.Compare(ThisYearMonth, AccountDate.ToString("yyyy年MM月")) > 0)
        {
            ThisYearMonth = AccountDate.ToString("yyyy年MM月");
            EDate = ThisYearMonth + DateTime.DaysInMonth(AccountDate.Year, AccountDate.Month) + "日";
        }
        string BDate = ThisYearMonth + "01日";
        List<string> SubjectNo = new List<string>();
        List<string> SubjectName = new List<string>();
        List<decimal> BeginBalance = new List<decimal>();
        //获取科目列表
        string QueryString = string.Empty;
        if (!isShowDetail.Checked)
        {
            if (SubjectNoS.Text.Length != 0 && (SubjectNoE.Text.Length == 0 || SubjectNoS.Text.Length == SubjectNoE.Text.Length))
            {
                QueryString += "$length(SubjectNo)=" + SubjectNoS.Text.Length.ToString();
            }
            else
            {
                QueryString += "$parentno='000'";
            }
        }
        if (SubjectNoS.Text.Length > 0)
        {
            if (SubjectNoE.Text.Length > 0)
            {
                QueryString += "$SubjectNo between '" + SubjectNoS.Text + "' and '" + SubjectNoE.Text + "9999999999999999'";
            }
            else
            {
                QueryString += "$SubjectNo like '" + SubjectNoS.Text + "%'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1).Replace("$", " and ");
        }
        ClsCalculate clsCalculate = new ClsCalculate();
        DataSet ds = CommClass.GetDataSet("select id,subjectno,subjectname from cw_subject " + QueryString + " order by subjectno asc");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            SubjectNo.Add(row["subjectno"].ToString());
            SubjectName.Add(row["subjectname"].ToString());
            BeginBalance.Add(clsCalculate.GetSubjectSumFromDataTable(row["subjectno"].ToString(), "BeginBalance", ThisYearMonth));
        }
        //汇总本月科目借贷方余额以及生成期末余额
        ds = CommClass.GetDataSet("select subjectno,summoney from CW_VoucherEntry where voucherdate between '" + BDate + "' and '" + EDate + "' and delflag='0'");
        DataRow NewRow;
        decimal EntrySumMoney = 0;
        decimal MBeginBalance = 0;
        decimal MLeadSum = 0;
        decimal MOnloanSum = 0;
        decimal MFinalBalance = 0;
        ////////////////////////////////////////
        decimal CountLead = 0;
        decimal CountOnloan = 0;
        decimal CountBeginBalance = 0;
        decimal CountFinalBalance = 0;
        ////////////////////////////////////////
        List<string> MatchSubjects = new List<string>();
        ////////////////////////////////////////
        for (int i = 0; i < SubjectNo.Count; i++)
        {
            MLeadSum = 0;
            MOnloanSum = 0;
            MBeginBalance = BeginBalance[i];
            DataRow[] EntryList = ds.Tables[0].Select("subjectno like '" + SubjectNo[i] + "%'");
            for (int k = 0; k < EntryList.Length; k++)
            {
                decimal.TryParse(EntryList[k]["summoney"].ToString(), out EntrySumMoney);
                if (EntrySumMoney > 0)
                {
                    MLeadSum += EntrySumMoney;
                }
                else
                {
                    MOnloanSum -= EntrySumMoney;
                }
            }
            MFinalBalance = MBeginBalance + MLeadSum - MOnloanSum;
            if (SubjectNo[i].Length == 3)
            {
                CountLead += MLeadSum;
                CountOnloan += MOnloanSum;
                CountBeginBalance += MBeginBalance;
            }
            //填充数据
            NewRow = BindTable.NewRow();
            NewRow[0] = SubjectNo[i];
            NewRow[1] = SubjectName[i];
            NewRow[4] = MLeadSum.ToString("#,##0.00");
            NewRow[5] = MOnloanSum.ToString("#,##0.00");
            PageClass.DoBalance(NewRow, MBeginBalance, 2, 3);
            PageClass.DoBalance(NewRow, MFinalBalance, 6, 7);
            if (MBeginBalance != 0 || MLeadSum != 0 || MOnloanSum != 0 || MFinalBalance != 0 || isShowZeroSubject.Checked)
            {
                BindTable.Rows.Add(NewRow);
            }
        }
        //补充空行
        for (int i = BindTable.Rows.Count; i < 10; i++)
        {
            BindTable.Rows.Add(BindTable.NewRow());
        }
        //创建合计行
        CountFinalBalance = CountBeginBalance + CountLead - CountOnloan;
        NewRow = BindTable.NewRow();
        NewRow[1] = PageClass.PadRightM("<center>合", 20, "&nbsp;") + "计</center>";
        NewRow[4] = CountLead.ToString("#,##0.00");
        NewRow[5] = CountOnloan.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, CountBeginBalance, 2, 3);
        PageClass.DoBalance(NewRow, CountFinalBalance, 6, 7);
        BindTable.Rows.Add(NewRow);
    }
    protected void Button1_Click(object sender, EventArgs e)
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
