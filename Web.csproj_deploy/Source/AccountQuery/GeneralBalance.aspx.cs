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

public partial class AccountQuery_GeneralBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100006")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            DateTime AccountDate = MainClass.GetAccountDate();
            ReportDate.Value = AccountDate.ToString("yyyy年MM月");
            SelYear.Attributes["onchange"] = "OnSelChange(this.value,0);";
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Attributes["onchange"] = "SetReportDate()";
            SelYear.Attributes["onchange"] = "SetReportDate()";
            MainClass.InitAccountYear(SelYear);
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：总账余额表");
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
        if (ReportDate.Value == MainClass.GetAccountDate().ToString("yyyy年MM月"))
        {
            InitBindTable1(BindTable);
        }
        else
        {
            InitBindTable0(BindTable);
        }
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
        BindTable.AcceptChanges();
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "000000";
        reportPage.DesignID = "000002";
        reportPage.ReportTitle = "总账余额表";
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
    protected void InitBindTable0(DataTable BindTable)
    {
        //账后账科目余额查询
        string sm = string.Concat(SelYear.SelectedValue, "年", QSMonth.SelectedValue + "月");
        string em = string.Concat(SelYear.SelectedValue, "年", QEMonth.SelectedValue + "月");
        string QueryString = "$yearmonth>='" + sm + "'$yearmonth<='" + em + "'";
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
        if (SubjectNoS.Text != "")
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
        DataRowCollection AllRows = CommClass.GetDataRows("select subjectno,subjectname,BeginBalance,sum(Lead) Lead,sum(Onloan) Onloan from cw_viewsubjectsum " + QueryString + " group by subjectno order by subjectno,min(yearmonth)");
        if (AllRows == null) { return; }
        decimal CountBeginBalance = 0;
        decimal CountLead = 0;
        decimal CountOnloan = 0;
        decimal CountFinalBalance = 0;
        DataRow NewRow;
        for (int i = 0; i < AllRows.Count; i++)
        {
            NewRow = BindTable.NewRow();
            //合计单行数据
            decimal BeginBalance = decimal.Parse(AllRows[i]["BeginBalance"].ToString());
            decimal Lead = decimal.Parse(AllRows[i]["Lead"].ToString());
            decimal Onloan = decimal.Parse(AllRows[i]["Onloan"].ToString());
            decimal FinalBalance = BeginBalance + Lead - Onloan;
            //合计科目余额数据
            if (AllRows[i]["subjectno"].ToString().Length == 3)
            {
                CountBeginBalance += BeginBalance;
                CountLead += Lead;
                CountOnloan += Onloan;
                CountFinalBalance += FinalBalance;
            }
            //填充数据
            NewRow[0] = AllRows[i]["subjectno"].ToString();
            NewRow[1] = AllRows[i]["subjectname"].ToString();
            NewRow[3] = BeginBalance.ToString("#,##0.00").Replace("-", "");
            NewRow[4] = Lead.ToString("#,##0.00");
            NewRow[5] = Onloan.ToString("#,##0.00");
            PageClass.DoBalance(NewRow, BeginBalance, 2, 3);
            PageClass.DoBalance(NewRow, FinalBalance, 6, 7);
            if (BeginBalance != 0 || Lead != 0 || Onloan != 0 || FinalBalance != 0 || isShowZeroSubject.Checked)
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
        NewRow = BindTable.NewRow();
        NewRow[1] = PageClass.PadRightM("<center>合", 20, "&nbsp;") + "计</center>";
        NewRow[4] = CountLead.ToString("#,##0.00");
        NewRow[5] = CountOnloan.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, CountBeginBalance, 2, 3);
        PageClass.DoBalance(NewRow, CountFinalBalance, 6, 7);
        BindTable.Rows.Add(NewRow);
    }
    protected void InitBindTable1(DataTable BindTable)
    {
        //当月账前科目余额查询
        string BDate = ReportDate.Value + "01日";
        string EDate = ReportDate.Value + "31日";
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
        DataSet ds = CommClass.GetDataSet("select id,subjectno,subjectname from cw_subject " + QueryString + " order by subjectno asc");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            SubjectNo.Add(row["subjectno"].ToString());
            SubjectName.Add(row["subjectname"].ToString());
            BeginBalance.Add(ClsCalculate.GetSubjectSumDecimal(row["subjectno"].ToString(), "BeginBalance", ReportDate.Value));
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
        decimal CountBeginBalance = 0;
        decimal CountLead = 0;
        decimal CountOnloan = 0;
        decimal CountFinalBalance = 0;
        ////////////////////////////////////////
        for (int i = 0; i < SubjectNo.Count; i++)
        {
            MBeginBalance = BeginBalance[i];
            MLeadSum = 0;
            MOnloanSum = 0;
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
