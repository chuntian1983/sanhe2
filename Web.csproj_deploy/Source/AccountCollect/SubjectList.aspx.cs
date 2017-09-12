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

public partial class AccountCollect_SubjectList : System.Web.UI.Page
{
    public string TableWidth = "750";
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
        ReportTitle.InnerHtml = "科目余额横向过录表（" + ReportType.SelectedItem.Text + "）";
        //创建数据表
        DataTable BindTable = new DataTable();
        DataRowCollection rows = MainClass.GetDataRows("select subjectno,subjectname from cw_subject  where " + ReportType.SelectedValue
            + " and parentno='000' and unitid='" + Session["UnitID"].ToString() + "' order by subjectno");
        int _TableWidth = 160 + rows.Count * 124;
        if (rows == null)
        {
            return;
        }
        else
        {
            GridView1.Columns.Clear();
            BindTable.Columns.Add("F");
            BoundField head = new BoundField();
            head.ItemStyle.Width = 150;
            head.DataField = "F";
            head.HtmlEncode = false;
            head.ItemStyle.CssClass = "sbalance";
            GridView1.Columns.Add(head);
            for (int i = 0; i < rows.Count; i++)
            {
                //方向字段
                BindTable.Columns.Add("F0" + i.ToString());
                BoundField bf0 = new BoundField();
                bf0.HeaderText = rows[i]["subjectno"].ToString();
                bf0.ItemStyle.Width = 20;
                bf0.DataField = "F0" + i.ToString();
                bf0.HtmlEncode = false;
                bf0.ItemStyle.CssClass = "sbalance";
                bf0.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GridView1.Columns.Add(bf0);
                //余额字段
                BindTable.Columns.Add("F1" + i.ToString());
                BoundField bf1 = new BoundField();
                bf1.HeaderText = rows[i]["subjectno"].ToString();
                bf1.ItemStyle.Width = 100;
                bf1.DataField = "F1" + i.ToString();
                bf1.HtmlEncode = false;
                bf1.ItemStyle.CssClass = "sbalance";
                bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                GridView1.Columns.Add(bf1);
            }
            //输出收入合计
            if (ReportType.SelectedIndex == 5)
            {
                _TableWidth += 124;
                //方向字段
                BindTable.Columns.Add("F0" + rows.Count.ToString());
                BoundField bf0 = new BoundField();
                bf0.HeaderText = "000";
                bf0.ItemStyle.Width = 20;
                bf0.DataField = "F0" + rows.Count.ToString();
                bf0.HtmlEncode = false;
                bf0.ItemStyle.CssClass = "sbalance";
                bf0.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GridView1.Columns.Add(bf0);
                //余额字段
                BindTable.Columns.Add("F1" + rows.Count.ToString());
                BoundField bf1 = new BoundField();
                bf1.HeaderText = "000";
                bf1.ItemStyle.Width = 100;
                bf1.DataField = "F1" + rows.Count.ToString();
                bf1.HtmlEncode = false;
                bf1.ItemStyle.CssClass = "sbalance";
                bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                GridView1.Columns.Add(bf1);
            }
            //输出表格宽度
            if (_TableWidth > 750)
            {
                GridView1.Width = _TableWidth;
                TableWidth = _TableWidth.ToString();
            }
            else
            {
                GridView1.Width = 750;
            }
        }
        BindTable.Rows.Add(BindTable.NewRow());
        BindTable.Rows.Add(BindTable.NewRow());
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
        //补充空行
        for (int i = BindTable.Rows.Count; i < 10; i++)
        {
            BindTable.Rows.Add(BindTable.NewRow());
        }
        BindTable.AcceptChanges();
        GridView1.DataSource = BindTable;
        GridView1.DataBind();
        GridView1.Rows[0].Cells[0].Text = "<center>科目名称→<br><br>账套名称↓</center>";
        GridView1.Rows[0].Cells[0].RowSpan = 2;
        GridView1.Rows[1].Cells[0].Visible = false;
        for (int i = 1; i < GridView1.Columns.Count; i += 2)
        {
            int rowIndex = (i + 1) / 2 - 1;
            if (rowIndex < rows.Count)
            {
                GridView1.Rows[0].Cells[i].Width = 120;
                GridView1.Rows[0].Cells[i].ColumnSpan = 2;
                GridView1.Rows[0].Cells[i].Text = rows[rowIndex]["subjectname"].ToString();
                GridView1.Rows[0].Cells[i + 1].Visible = false;
                GridView1.Rows[1].Cells[i].Text = "方向";
                GridView1.Rows[1].Cells[i + 1].Text = "<center>科目余额</center>";
            }
        }
        //收入合计列表头
        if (ReportType.SelectedIndex == 5)
        {
            GridView1.Rows[0].Cells[GridView1.Columns.Count - 2].Width = 120;
            GridView1.Rows[0].Cells[GridView1.Columns.Count - 2].ColumnSpan = 2;
            GridView1.Rows[0].Cells[GridView1.Columns.Count - 2].Text = "收入合计";
            GridView1.Rows[0].Cells[GridView1.Columns.Count - 1].Visible = false;
            GridView1.Rows[1].Cells[GridView1.Columns.Count - 2].Text = "方向";
            GridView1.Rows[1].Cells[GridView1.Columns.Count - 1].Text = "<center>汇总余额</center>";
        }
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
        DataRow NewRow;
        string[] AccountList = GAccountList.Value.Split('$');
        for (int i = 0; i < AccountList.Length; i++)
        {
            if (AccountList[i].Length == 0)
            {
                continue;
            }
            UserInfo.AccountID = AccountList[i];
            NewRow = BindTable.NewRow();
            //填充数据
            decimal FinalBalance = 0;
            NewRow[0] = MainClass.GetFieldFromID(AccountList[i], "accountname", "cw_account");
            for (int k = 1; k < GridView1.Columns.Count; k += 2)
            {
                if (GridView1.Columns[k].HeaderText == "000")
                {
                    //收入合计
                    FinalBalance = decimal.Parse(NewRow[2].ToString().Replace(",", ""))
                        - decimal.Parse(NewRow[4].ToString().Replace(",", ""))
                        + decimal.Parse(NewRow[6].ToString().Replace(",", ""))
                        + decimal.Parse(NewRow[8].ToString().Replace(",", ""))
                        + decimal.Parse(NewRow[10].ToString().Replace(",", ""))
                        + decimal.Parse(NewRow[12].ToString().Replace(",", ""))
                        + decimal.Parse(NewRow[18].ToString().Replace(",", ""));
                }
                else
                {
                    //科目余额
                    FinalBalance = ClsCalculate.GetSubjectSumDecimal(GridView1.Columns[k].HeaderText, "FinalBalance", ReportDate.Text);
                }
                if (FinalBalance > 0)
                {
                    NewRow[k] = "借";
                }
                else if (FinalBalance < 0)
                {
                    NewRow[k] = "贷";
                }
                else
                {
                    NewRow[k] = "平";
                }
                NewRow[k + 1] = FinalBalance.ToString("#,##0.00").Replace("-", "");
            }
            BindTable.Rows.Add(NewRow);
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
