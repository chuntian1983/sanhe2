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

public partial class AccountCollect_SubjectBalanceSGL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
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
        for (int i = 0; i < 8; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
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
                    GetSumDataRow(BindTable);
                    if (NoCarryAccount.Length > 0)
                    {
                        ExeScript.Text = "<script>alert('以下账套尚未提交汇总数据：" + NoCarryAccount.ToString().Substring(1) + "');</script>";
                    }
                }
            }
        }
        for (int i = BindTable.Rows.Count - 1; i < 10; i++)
        {
            if (BindTable.Rows.Count > 1)
            {
                BindTable.Rows.InsertAt(BindTable.NewRow(), BindTable.Rows.Count - 1);
            }
            else
            {
                BindTable.Rows.Add(BindTable.NewRow());
            }
        }
        BindTable.AcceptChanges();
        MainClass.CreateGridView("000002", BindTable, GridView1);
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

    private void GetSumDataRow(DataTable BindTable)
    {
        decimal BeginBalance = 0;
        decimal Lead = 0;
        decimal Onloan = 0;
        decimal FinalBalance = 0;
        DataRow[] CountRow = BindTable.Select("len(F0)=3");
        //合计科目余额数据
        for (int i = 0; i < CountRow.Length; i++)
        {
            if (CountRow[i][2].ToString() == "贷")
            {
                BeginBalance -= decimal.Parse(CountRow[i][3].ToString());
            }
            else
            {
                BeginBalance += decimal.Parse(CountRow[i][3].ToString());
            }
            Lead += decimal.Parse(CountRow[i][4].ToString());
            Onloan += decimal.Parse(CountRow[i][5].ToString());
            if (CountRow[i][6].ToString() == "贷")
            {
                FinalBalance -= decimal.Parse(CountRow[i][7].ToString());
            }
            else
            {
                FinalBalance += decimal.Parse(CountRow[i][7].ToString());
            }
        }
        //去除零余额科目
        if (!isShowZeroSubject.Checked)
        {
            for (int i = BindTable.Rows.Count - 1; i >= 0; i--)
            {
                if (BindTable.Rows[i][3].ToString() == "0.00"
                 && BindTable.Rows[i][4].ToString() == "0.00"
                 && BindTable.Rows[i][5].ToString() == "0.00"
                 && BindTable.Rows[i][7].ToString() == "0.00")
                {
                    BindTable.Rows[i].Delete();
                }
            }
        }
        //创建合计行
        DataRow NewRow = BindTable.NewRow();
        NewRow[1] = PageClass.PadRightM("<center>合", 20, "&nbsp;") + "计</center>";
        NewRow[4] = Lead.ToString("#,##0.00");
        NewRow[5] = Onloan.ToString("#,##0.00");
        PageClass.DoBalance(NewRow, BeginBalance, 2, 3);
        PageClass.DoBalance(NewRow, FinalBalance, 6, 7);
        BindTable.Rows.Add(NewRow);
    }

    /// <summary>
    /// 汇总报表数据
    /// </summary>
    /// <param name="BindTable"></param>
    protected void InitBindTable(DataTable BindTable, string AccountID)
    {
        UserInfo.AccountID = AccountID;
        string wh = "$yearmonth='" + ReportDate.Text + "'";
        if (!isShowDetail.Checked) { wh += "$parentno='000'"; }
        if (wh.Length > 0)
        {
            wh = " where " + wh.Substring(1).Replace("$", " and ");
        }
        DataRowCollection AllRows = CommClass.GetDataRows("select subjectno,subjectname,BeginBalance,Lead,Onloan from cw_viewsubjectsum "
            + wh + " order by subjectno");
        if (AllRows == null) { return; }
        DataRow NewRow;
        for (int i = 0; i < AllRows.Count; i++)
        {
            decimal BeginBalance = decimal.Parse(AllRows[i]["BeginBalance"].ToString());
            decimal Lead = decimal.Parse(AllRows[i]["Lead"].ToString());
            decimal Onloan = decimal.Parse(AllRows[i]["Onloan"].ToString());
            decimal FinalBalance = BeginBalance + Lead - Onloan;
            DataRow[] SelHasRow = BindTable.Select("F0='" + AllRows[i]["subjectno"].ToString() + "'");
            if (SelHasRow.Length == 0)
            {
                NewRow = BindTable.NewRow();
            }
            else
            {
                NewRow = SelHasRow[0];
                if (SelHasRow[0][2].ToString() == "贷")
                {
                    BeginBalance -= decimal.Parse(SelHasRow[0][3].ToString());
                }
                else
                {
                    BeginBalance += decimal.Parse(SelHasRow[0][3].ToString());
                }
                Lead += decimal.Parse(SelHasRow[0][4].ToString());
                Onloan += decimal.Parse(SelHasRow[0][5].ToString());
                if (SelHasRow[0][6].ToString() == "贷")
                {
                    FinalBalance -= decimal.Parse(SelHasRow[0][7].ToString());
                }
                else
                {
                    FinalBalance += decimal.Parse(SelHasRow[0][7].ToString());
                }
            }
            NewRow[0] = AllRows[i]["subjectno"].ToString();
            NewRow[1] = AllRows[i]["subjectname"].ToString();
            NewRow[4] = Lead.ToString("#,##0.00");
            NewRow[5] = Onloan.ToString("#,##0.00");
            PageClass.DoBalance(NewRow, BeginBalance, 2, 3);
            PageClass.DoBalance(NewRow, FinalBalance, 6, 7);
            if (SelHasRow.Length == 0)
            {
                BindTable.Rows.Add(NewRow);
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
