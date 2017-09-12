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

public partial class AccountCollect_SubjectBalance : System.Web.UI.Page
{
    private decimal TBeginBalance0 = 0;
    private decimal TLead0 = 0;
    private decimal TOnloan0 = 0;
    private decimal TFinalBalance0 = 0;
    private decimal TBeginBalance1 = 0;
    private decimal TLead1 = 0;
    private decimal TOnloan1 = 0;
    private decimal TFinalBalance1 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            if (SysConfigs.SBR_ShowType == "0")
            {
                Response.Redirect("SubjectBalanceSGL.aspx", true);
            }
            GridView1.Attributes.Add("onselectstart", "return false;");
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            SelMonth.Attributes["onchange"] = "setMonth(this.value);";
            SelMonth.Text = DateTime.Now.Month.ToString("00");
            DataRowCollection rows = MainClass.GetDataRows("select levelname,collectunits from cw_collectlevel where unitid='" + Session["UnitID"] + "'");
            if (rows != null)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    CollectUnit.Items.Add(new ListItem(rows[i]["levelname"].ToString(), rows[i]["collectunits"].ToString()));
                }
            }
            InitWebControl();
        }
    }

    protected void InitWebControl()
    {
        switch (CollectUnit.SelectedValue)
        {
            case "000000":
                AName.Text = "";
                break;
            case "XXXXXX":
                AName.Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitName");
                break;
            default:
                AName.Text = CollectUnit.SelectedItem.Text;
                break;
        }
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 12; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
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
        FillDataTable1(BindTable, leftSubject.ToString(), true);
        FillDataTable1(BindTable, rightSubject.ToString(), false);
        if (CollectUnit.SelectedValue != "000000")
        {
            if (CollectUnit.SelectedValue == "")
            {
                ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】无汇总账套或下级单位！');</script>";
            }
            else
            {
                string _CollectUnit = "";
                StringBuilder AllAccount = new StringBuilder();
                StringBuilder NoCarryAccount = new StringBuilder();
                if (CollectUnit.SelectedValue == "XXXXXX")
                {
                    _CollectUnit = "-" + Session["UnitID"].ToString();
                }
                else
                {
                    _CollectUnit = CollectUnit.SelectedValue;
                }
                string[] _G = _CollectUnit.Split('-');
                AllAccount.Append(_G[0]);
                string[] UnitList = _G[1].Split('$');
                for (int i = 0; i < UnitList.Length; i++)
                {
                    GetCollectAccount(ref AllAccount, UnitList[i]);
                }
                if (AllAccount.Length == 0)
                {
                    ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】下级无汇总账套！');</script>";
                }
                else
                {
                    string[] AccountList = AllAccount.ToString().Split('$');
                    for (int i = 0; i < AccountList.Length; i++)
                    {
                        if (AccountList[i].Length == 0)
                        {
                            continue;
                        }
                        string LastCarryDate = MainClass.GetFieldFromID(AccountList[i], "LastCarryDate", "cw_account");
                        string StartAccountDate = MainClass.GetFieldFromID(AccountList[i], "StartAccountDate", "cw_account");
                        if (StartAccountDate == "" || LastCarryDate == "")
                        {
                            NoCarryAccount.Append("，" + MainClass.GetFieldFromID(AccountList[i], "accountname", "cw_account"));
                        }
                        else
                        {
                            if (string.Compare(StartAccountDate.Substring(0, 8), ReportDate.Text) > 0 || string.Compare(LastCarryDate.Substring(0, 8), ReportDate.Text) < 0)
                            {
                                NoCarryAccount.Append("，" + MainClass.GetFieldFromID(AccountList[i], "accountname", "cw_account"));
                            }
                            else
                            {
                                InitBindTable(BindTable, AccountList[i]);
                            }
                        }
                    }
                    if (NoCarryAccount.Length > 0)
                    {
                        ExeScript.Text = "<script>alert('以下账套尚未提交汇总数据：" + NoCarryAccount.ToString().Substring(1) + "');</script>";
                    }
                }
            }
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
        //插入数据
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
        //创建合计行
        DataRow NewRow = BindTable.NewRow();
        NewRow[0] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
        NewRow[1] = TBeginBalance0.ToString("#,##0.00");
        NewRow[2] = TLead0.ToString("#,##0.00");
        NewRow[3] = TOnloan0.ToString("#,##0.00");
        NewRow[4] = TFinalBalance0.ToString("#,##0.00");
        NewRow[5] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
        NewRow[6] = TBeginBalance1.ToString("#,##0.00");
        NewRow[7] = TLead1.ToString("#,##0.00");
        NewRow[8] = TOnloan1.ToString("#,##0.00");
        NewRow[9] = TFinalBalance1.ToString("#,##0.00");
        BindTable.Rows.Add(NewRow);
        BindTable.AcceptChanges();
        MainClass.CreateGridView("000013", BindTable, GridView1);
    }

    protected void GetCollectAccount(ref StringBuilder AllAccount, string UnitID)
    {
        DataSet ds = MainClass.GetDataSet("select id from cw_account where unitid='" + UnitID + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            AllAccount.Append(row["id"].ToString() + "$");
        }
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + UnitID + "'");
        if (rows != null)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                GetCollectAccount(ref AllAccount, rows[i]["id"].ToString());
            }
        }
    }

    protected void InitBindTable(DataTable BindTable, string AccountID)
    {
        UserInfo.AccountID = AccountID;
        FillDataTable0(BindTable, "subjectno like '1%' or subjectno='502' or subjectno='541' or subjectno='551'", true);
        FillDataTable0(BindTable, "subjectno like '2%' or subjectno like '3%' or subjectno='501' or subjectno='511' or subjectno='522' or subjectno='531' or subjectno='561'", false);
    }

    protected void FillDataTable0(DataTable BindTable, string SNo, bool isLeft)
    {
        DataRowCollection AllRows = CommClass.GetDataRows("select subjectno,BeginBalance,Lead,Onloan,FinalBalance from cw_viewsubjectsum"
            + " where yearmonth='" + ReportDate.Text + "' and parentno='000' and (" + SNo + ") order by subjectno asc");
        if (AllRows == null) { return; }
        for (int i = 0; i < AllRows.Count; i++)
        {
            string SelSQL = string.Empty;
            if (isLeft)
            {
                SelSQL = "F10='" + AllRows[i]["subjectno"].ToString() + "'";
            }
            else
            {
                SelSQL = "F11='" + AllRows[i]["subjectno"].ToString() + "'";
            }
            DataRow[] SelHasRow = BindTable.Select(SelSQL);
            decimal BeginBalance = decimal.Parse(AllRows[i]["BeginBalance"].ToString());
            decimal Lead = decimal.Parse(AllRows[i]["Lead"].ToString());
            decimal Onloan = decimal.Parse(AllRows[i]["Onloan"].ToString());
            decimal FinalBalance = decimal.Parse(AllRows[i]["FinalBalance"].ToString());
            if (SelHasRow.Length > 0)
            {
                if (isLeft)
                {
                    //合计科目余额数据
                    TBeginBalance0 += BeginBalance;
                    TLead0 += Lead;
                    TOnloan0 += Onloan;
                    TFinalBalance0 += FinalBalance;
                    //填充数据
                    BeginBalance += decimal.Parse(SelHasRow[0][1].ToString());
                    Lead += decimal.Parse(SelHasRow[0][2].ToString());
                    Onloan += decimal.Parse(SelHasRow[0][3].ToString());
                    FinalBalance += decimal.Parse(SelHasRow[0][4].ToString());
                    SelHasRow[0][1] = BeginBalance.ToString("#,##0.00");
                    SelHasRow[0][2] = Lead.ToString("#,##0.00");
                    SelHasRow[0][3] = Onloan.ToString("#,##0.00");
                    SelHasRow[0][4] = FinalBalance.ToString("#,##0.00");
                }
                else
                {
                    BeginBalance = 0 - BeginBalance;
                    FinalBalance = 0 - FinalBalance;
                    //合计科目余额数据
                    TBeginBalance1 += BeginBalance;
                    TLead1 += Lead;
                    TOnloan1 += Onloan;
                    TFinalBalance1 += FinalBalance;
                    //填充数据
                    BeginBalance += decimal.Parse(SelHasRow[0][6].ToString());
                    Lead += decimal.Parse(SelHasRow[0][7].ToString());
                    Onloan += decimal.Parse(SelHasRow[0][8].ToString());
                    FinalBalance += decimal.Parse(SelHasRow[0][9].ToString());
                    SelHasRow[0][6] = BeginBalance.ToString("#,##0.00");
                    SelHasRow[0][7] = Lead.ToString("#,##0.00");
                    SelHasRow[0][8] = Onloan.ToString("#,##0.00");
                    SelHasRow[0][9] = FinalBalance.ToString("#,##0.00");
                }
            }
        }
    }

    protected void FillDataTable1(DataTable BindTable, string SNo, bool isLeft)
    {
        DataRowCollection AllRows = MainClass.GetDataRows("select subjectno,subjectname from cw_subject where unitid='" + Session["UnitID"]
            + "' and parentno='000' and (" + SNo + ") order by subjectno asc");
        if (AllRows == null) { return; }
        DataRow NewRow;
        for (int i = 0; i < AllRows.Count; i++)
        {
            if (isLeft)
            {
                NewRow = BindTable.NewRow();
                NewRow[0] = AllRows[i]["subjectname"].ToString();
                NewRow[1] = "0.00";
                NewRow[2] = "0.00";
                NewRow[3] = "0.00";
                NewRow[4] = "0.00";
                NewRow[10] = AllRows[i]["subjectno"].ToString();
                BindTable.Rows.Add(NewRow);
            }
            else
            {
                if (i < BindTable.Rows.Count)
                {
                    BindTable.Rows[i][5] = AllRows[i]["subjectname"].ToString();
                    BindTable.Rows[i][6] = "0.00";
                    BindTable.Rows[i][7] = "0.00";
                    BindTable.Rows[i][8] = "0.00";
                    BindTable.Rows[i][9] = "0.00";
                    BindTable.Rows[i][11] = AllRows[i]["subjectno"].ToString();
                }
                else
                {
                    NewRow = BindTable.NewRow();
                    NewRow[5] = AllRows[i]["subjectname"].ToString();
                    NewRow[6] = "0.00";
                    NewRow[7] = "0.00";
                    NewRow[8] = "0.00";
                    NewRow[9] = "0.00";
                    NewRow[11] = AllRows[i]["subjectno"].ToString();
                    BindTable.Rows.Add(NewRow);
                }
            }
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
