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

public partial class AccountQuery_SubjectBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100006")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            if (SysConfigs.SBR_ShowType == "0")
            {
                Response.Redirect("SubjectBalanceSGL.aspx", true);
            }
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
        for (int i = 0; i < 10; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        string lastCarryMonth = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
        if (lastCarryMonth.Length == 0)
        {
            lastCarryMonth = "1900年01月01日";
        }
        else
        {
            lastCarryMonth = lastCarryMonth.Substring(0, 8);
        }
        if (string.Compare(ReportDate.Text, lastCarryMonth) <= 0)
        {
            InitBindTable(BindTable, false);
        }
        else
        {
            InitBindTable(BindTable, true);
        }
        //清空余额为零的单元格
        int[] ColumnPos ={ 1, 2, 3, 4, 6, 7, 8, 9 };
        for (int i = 0; i < BindTable.Rows.Count; i++)
        {
            for (int k = 0; k < ColumnPos.Length; k++)
            {
                if (BindTable.Rows[i][ColumnPos[k]].ToString() == "0.00")
                {
                    BindTable.Rows[i][ColumnPos[k]] = "&nbsp;";
                }
            }
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
        CommClass.CreateGridView("000013", BindTable, GridView1);
    }
    protected void InitBindTable(DataTable BindTable, bool isLastMonth)
    {
        StringBuilder leftSubject = new StringBuilder();
        StringBuilder rightSubject = new StringBuilder();
        leftSubject.Append("subjectno like '1%' or subjectno like '4%'");
        string[] expenseSubject = SysConfigs.ExpenseSubject.Split('|');
        for (int i = 0; i < expenseSubject.Length; i++)
        {
            leftSubject.AppendFormat(" or subjectno='{0}'", expenseSubject[i]);
        }
        rightSubject.Append("subjectno like '2%' or subjectno like '3%'");
        string[] incomeSubject = SysConfigs.IncomeSubject.Split('|');
        for (int i = 0; i < incomeSubject.Length; i++)
        {
            rightSubject.AppendFormat(" or subjectno='{0}'", incomeSubject[i]);
        }
        if (isLastMonth)
        {
            FillDataTable1(BindTable, leftSubject.ToString(), true);
            FillDataTable1(BindTable, rightSubject.ToString(), false);
        }
        else
        {
            FillDataTable0(BindTable, leftSubject.ToString(), true);
            FillDataTable0(BindTable, rightSubject.ToString(), false);
        }
    }
    /// <summary>
    /// 不包含账前账统计
    /// </summary>
    /// <param name="BindTable"></param>
    /// <param name="SNo"></param>
    /// <param name="isLeft"></param>
    protected void FillDataTable0(DataTable BindTable, string SNo, bool isLeft)
    {
        DataRowCollection AllRows = CommClass.GetDataRows("select subjectno,subjectname,BeginBalance,Lead,Onloan,FinalBalance from cw_viewsubjectsum"
            + " where yearmonth='" + ReportDate.Text + "' and parentno='000' and (" + SNo + ") order by subjectno asc");
        if (AllRows == null) { return; }
        decimal BeginBalance0 = 0;
        decimal Lead0 = 0;
        decimal Onloan0 = 0;
        decimal FinalBalance0 = 0;
        DataRow NewRow;
        for (int i = 0; i < AllRows.Count; i++)
        {
            decimal BeginBalance = decimal.Parse(AllRows[i]["BeginBalance"].ToString());
            decimal Lead = decimal.Parse(AllRows[i]["Lead"].ToString());
            decimal Onloan = decimal.Parse(AllRows[i]["Onloan"].ToString());
            decimal FinalBalance = decimal.Parse(AllRows[i]["FinalBalance"].ToString());
            //合计科目余额数据
            BeginBalance0 += BeginBalance;
            Lead0 += Lead;
            Onloan0 += Onloan;
            FinalBalance0 += FinalBalance;
            //填充数据
            if (isLeft)
            {
                NewRow = BindTable.NewRow();
                NewRow[0] = AllRows[i]["subjectname"].ToString();
                NewRow[1] = BeginBalance.ToString("#,##0.00");
                NewRow[2] = Lead.ToString("#,##0.00");
                NewRow[3] = Onloan.ToString("#,##0.00");
                NewRow[4] = FinalBalance.ToString("#,##0.00");
                BindTable.Rows.Add(NewRow);
            }
            else
            {
                BeginBalance = 0 - BeginBalance;
                FinalBalance = 0 - FinalBalance;
                if (i < BindTable.Rows.Count - 1)
                {
                    BindTable.Rows[i][5] = AllRows[i]["subjectname"].ToString();
                    BindTable.Rows[i][6] = BeginBalance.ToString("#,##0.00");
                    BindTable.Rows[i][7] = Lead.ToString("#,##0.00");
                    BindTable.Rows[i][8] = Onloan.ToString("#,##0.00");
                    BindTable.Rows[i][9] = FinalBalance.ToString("#,##0.00");
                }
                else
                {
                    NewRow = BindTable.NewRow();
                    NewRow[5] = AllRows[i]["subjectname"].ToString();
                    NewRow[6] = BeginBalance.ToString("#,##0.00");
                    NewRow[7] = Lead.ToString("#,##0.00");
                    NewRow[8] = Onloan.ToString("#,##0.00");
                    NewRow[9] = FinalBalance.ToString("#,##0.00");
                    BindTable.Rows.InsertAt(NewRow, BindTable.Rows.Count - 1);
                }
            }
        }
        //创建合计行
        if (isLeft)
        {
            NewRow = BindTable.NewRow();
            NewRow[0] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
            NewRow[1] = BeginBalance0.ToString("#,##0.00");
            NewRow[2] = Lead0.ToString("#,##0.00");
            NewRow[3] = Onloan0.ToString("#,##0.00");
            NewRow[4] = FinalBalance0.ToString("#,##0.00");
            BindTable.Rows.Add(NewRow);
        }
        else
        {
            BindTable.Rows[BindTable.Rows.Count - 1][5] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
            BindTable.Rows[BindTable.Rows.Count - 1][6] = (-BeginBalance0).ToString("#,##0.00");
            BindTable.Rows[BindTable.Rows.Count - 1][7] = Lead0.ToString("#,##0.00");
            BindTable.Rows[BindTable.Rows.Count - 1][8] = Onloan0.ToString("#,##0.00");
            BindTable.Rows[BindTable.Rows.Count - 1][9] = (-FinalBalance0).ToString("#,##0.00");
        }
    }
    /// <summary>
    /// 包含账前账统计
    /// </summary>
    /// <param name="BindTable"></param>
    /// <param name="SNo"></param>
    /// <param name="isLeft"></param>
    protected void FillDataTable1(DataTable BindTable, string SNo, bool isLeft)
    {
        DataRowCollection AllRows = CommClass.GetDataRows("select subjectno,subjectname,BeginBalance from cw_subject where parentno='000' and (" + SNo + ") order by subjectno asc");
        if (AllRows == null) { return; }
        decimal BeginBalance0 = 0;
        decimal Lead0 = 0;
        decimal Onloan0 = 0;
        decimal FinalBalance0 = 0;
        DataRow NewRow;
        string CurrentMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
        for (int i = 0; i < AllRows.Count; i++)
        {
            decimal Lead = 0;
            decimal Onloan = 0;
            decimal FinalBalance = 0;
            decimal BeginBalance = decimal.Parse(AllRows[i]["BeginBalance"].ToString());
            //账前账数据统计
            if (isHasNewValue.Checked)
            {
                DataRowCollection MonthEntry = CommClass.GetDataRows("select subjectno,summoney from CW_VoucherEntry where subjectno like '" + AllRows[i]["subjectno"].ToString()
                    + "%' and voucherdate like '" + CurrentMonth + "%' and delflag='0'");
                if (MonthEntry != null)
                {
                    for (int k = 0; k < MonthEntry.Count; k++)
                    {
                        if (MonthEntry[k]["summoney"].ToString().StartsWith("-"))
                        {
                            Onloan -= decimal.Parse(MonthEntry[k]["summoney"].ToString());
                        }
                        else
                        {
                            Lead += decimal.Parse(MonthEntry[k]["summoney"].ToString());
                        }
                    }
                    FinalBalance += Lead - Onloan;
                    if (CurrentMonth != ReportDate.Text)
                    {
                        Lead = 0;
                        Onloan = 0;
                    }
                }
            }
            FinalBalance += BeginBalance;
            //合计科目余额数据
            BeginBalance0 += BeginBalance;
            Lead0 += Lead;
            Onloan0 += Onloan;
            FinalBalance0 += FinalBalance;
            //填充数据
            if (isLeft)
            {
                NewRow = BindTable.NewRow();
                NewRow[0] = AllRows[i]["subjectname"].ToString();
                NewRow[1] = BeginBalance.ToString("#,##0.00");
                NewRow[2] = Lead.ToString("#,##0.00");
                NewRow[3] = Onloan.ToString("#,##0.00");
                NewRow[4] = FinalBalance.ToString("#,##0.00");
                BindTable.Rows.Add(NewRow);
            }
            else
            {
                BeginBalance = 0 - BeginBalance;
                FinalBalance = 0 - FinalBalance;
                if (i < BindTable.Rows.Count - 1)
                {
                    BindTable.Rows[i][5] = AllRows[i]["subjectname"].ToString();
                    BindTable.Rows[i][6] = BeginBalance.ToString("#,##0.00");
                    BindTable.Rows[i][7] = Lead.ToString("#,##0.00");
                    BindTable.Rows[i][8] = Onloan.ToString("#,##0.00");
                    BindTable.Rows[i][9] = FinalBalance.ToString("#,##0.00");
                }
                else
                {
                    NewRow = BindTable.NewRow();
                    NewRow[5] = AllRows[i]["subjectname"].ToString();
                    NewRow[6] = BeginBalance.ToString("#,##0.00");
                    NewRow[7] = Lead.ToString("#,##0.00");
                    NewRow[8] = Onloan.ToString("#,##0.00");
                    NewRow[9] = FinalBalance.ToString("#,##0.00");
                    BindTable.Rows.InsertAt(NewRow, BindTable.Rows.Count - 1);
                }
            }
        }
        //创建合计行
        if (isLeft)
        {
            NewRow = BindTable.NewRow();
            NewRow[0] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
            NewRow[1] = BeginBalance0.ToString("#,##0.00");
            NewRow[2] = Lead0.ToString("#,##0.00");
            NewRow[3] = Onloan0.ToString("#,##0.00");
            NewRow[4] = FinalBalance0.ToString("#,##0.00");
            BindTable.Rows.Add(NewRow);
        }
        else
        {
            BindTable.Rows[BindTable.Rows.Count - 1][5] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
            BindTable.Rows[BindTable.Rows.Count - 1][6] = (-BeginBalance0).ToString("#,##0.00");
            BindTable.Rows[BindTable.Rows.Count - 1][7] = Lead0.ToString("#,##0.00");
            BindTable.Rows[BindTable.Rows.Count - 1][8] = Onloan0.ToString("#,##0.00");
            BindTable.Rows[BindTable.Rows.Count - 1][9] = (-FinalBalance0).ToString("#,##0.00");
        }
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
