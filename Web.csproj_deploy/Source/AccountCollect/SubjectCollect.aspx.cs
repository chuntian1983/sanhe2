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

public partial class AccountCollect_SubjectCollect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckQuery();");
            GridView1.Attributes.Add("onselectstart", "return false;");
            QSubject.Attributes["onclick"] = "SelSubject();";
            QSubject.Attributes["readonly"] = "readonly";
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
        ReportTitle.InnerHtml = "各村" + QSubject.Text + "汇总表";
        //创建数据表
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 8; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        if (CollectUnit.SelectedValue != "000000")
        {
            if (CollectUnit.SelectedValue == "")
            {
                ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】下级无汇总账套！');</script>";
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
                        }
                    }
                    GAccountList.Value = AllAccount.ToString();
                    InitBindTable(BindTable);
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
                BindTable.Rows.InsertAt(BindTable.NewRow(), BindTable.Rows.Count - 2);
            }
            else
            {
                BindTable.Rows.Add(BindTable.NewRow());
            }
        }
        BindTable.AcceptChanges();
        MainClass.CreateGridView("000010", BindTable, GridView1);
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
    protected void InitBindTable(DataTable BindTable)
    {
        string QueryString = "$yearmonth='" + ReportDate.Text + "'";
        if (SubjectNo.Value.Length > 0)
        {
            QueryString += "$subjectno='" + SubjectNo.Value + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1).Replace("$", " and ");
        }
        decimal _BeginBalance = 0;
        decimal _Lead = 0;
        decimal _Onloan = 0;
        decimal _FinalBalance = 0;
        DataRow NewRow;
        string[] AccountList = GAccountList.Value.Split('$');
        for (int i = 0; i < AccountList.Length; i++)
        {
            if (AccountList[i].Length == 0)
            {
                continue;
            }
            UserInfo.AccountID = AccountList[i];
            DataRow GetRow = CommClass.GetDataRow("select * from cw_viewsubjectsum " + QueryString + " order by subjectno");
            if (GetRow == null) { continue; }
            NewRow = BindTable.NewRow();
            //合计单行数据
            decimal BeginBalance = decimal.Parse(GetRow["BeginBalance"].ToString());
            decimal Lead = decimal.Parse(GetRow["Lead"].ToString());
            decimal Onloan = decimal.Parse(GetRow["Onloan"].ToString());
            decimal FinalBalance = BeginBalance + Lead - Onloan;
            //合计科目余额数据
            _BeginBalance += BeginBalance;
            _Lead += Lead;
            _Onloan += Onloan;
            _FinalBalance += FinalBalance;
            //填充数据
            if (BeginBalance > 0)
            {
                NewRow[2] = "借";
            }
            else if (BeginBalance < 0)
            {
                NewRow[2] = "贷";
            }
            else
            {
                NewRow[2] = "平";
            }
            if (FinalBalance > 0)
            {
                NewRow[6] = "借";
            }
            else if (FinalBalance < 0)
            {
                NewRow[6] = "贷";
            }
            else
            {
                NewRow[6] = "平";
            }
            NewRow[0] = "<center>" + AccountList[i] + "</center>";
            NewRow[1] = MainClass.GetFieldFromID(AccountList[i], "accountname", "cw_account");
            NewRow[3] = BeginBalance.ToString("#,##0.00").Replace("-", "");
            NewRow[4] = Lead.ToString("#,##0.00");
            NewRow[5] = Onloan.ToString("#,##0.00");
            NewRow[7] = FinalBalance.ToString("#,##0.00").Replace("-", "");
            BindTable.Rows.Add(NewRow);
        }
        //补充空行
        for (int i = BindTable.Rows.Count; i < 10; i++)
        {
            BindTable.Rows.Add(BindTable.NewRow());
        }
        //创建合计行
        NewRow = BindTable.NewRow();
        NewRow[1] = PageClass.PadRightM("<center>合", 20, "&nbsp;") + "计</center>";
        NewRow[2] = "-";
        NewRow[3] = _BeginBalance.ToString("#,##0.00").Replace("-", "");
        NewRow[4] = _Lead.ToString("#,##0.00");
        NewRow[5] = _Onloan.ToString("#,##0.00");
        NewRow[6] = "-";
        NewRow[7] = _FinalBalance.ToString("#,##0.00").Replace("-", "");
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
