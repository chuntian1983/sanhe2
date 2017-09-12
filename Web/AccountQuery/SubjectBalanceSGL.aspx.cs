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

public partial class AccountQuery_SubjectBalanceSGL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100006")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            GridView1.Attributes.Add("onselectstart", "return false;");
            DateTime AccountDate = MainClass.GetAccountDate();
            ReportDate.Text = AccountDate.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SelYear.Attributes["onchange"] = "OnSelChange(this.value,0);";
            SelMonth.Attributes["onchange"] = "OnSelChange(this.value,1);";
            SelMonth.SelectedIndex = AccountDate.Month - 1;
            MainClass.InitAccountYear(SelYear);
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：科目余额汇总表");
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
        if (ReportDate.Text == MainClass.GetAccountDate().ToString("yyyy年MM月"))
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
        CommClass.CreateGridView("000002", BindTable, GridView1);
    }
    protected void InitBindTable0(DataTable BindTable)
    {
        //账后账科目余额查询
        string QueryString = "$yearmonth='" + ReportDate.Text + "'";
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
        DataRowCollection AllRows = CommClass.GetDataRows("select * from cw_viewsubjectsum " + QueryString + " order by subjectno asc");
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
        string BDate = ReportDate.Text + "01日";
        string EDate = ReportDate.Text + "31日";
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
            BeginBalance.Add(ClsCalculate.GetSubjectSumDecimal(row["subjectno"].ToString(), "BeginBalance", ReportDate.Text));
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex < int.Parse(RowsCount.Value))
        {
            //设置单元格编辑属性
            for (int n = 1; n < 8; n++)
            {
                e.Row.Cells[n].Attributes.Add("onclick", "OnCellClick('" + e.Row.Cells[n].ClientID + "')");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
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
