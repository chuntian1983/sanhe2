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

public partial class AccountQuery_Analysis03 : System.Web.UI.Page
{
    private string sYearMonth0 = string.Empty;
    private string eYearMonth0 = string.Empty;
    private string sYearMonth1 = string.Empty;
    private string eYearMonth1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000012$100007")) { return; }
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            GridView1.Attributes.Add("onselectstart", "return false;");
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            GetCommDetailInfo();
            //写入操作日志
            CommClass.WriteCTL_Log("100017", "报表分析：资产负债分析表");
            //--
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }

    protected void GetCommDetailInfo()
    {
        DataTable dataTable = new DataTable();
        int PreYear = int.Parse(QYear.SelectedValue) - 1;
        sYearMonth0 = PreYear.ToString() + "年" + QSMonth.SelectedValue + "月";
        eYearMonth0 = PreYear.ToString() + "年" + QEMonth.SelectedValue + "月";
        sYearMonth1 = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月";
        eYearMonth1 = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月";
        string OutputMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue;
        if (QSMonth.SelectedValue != QEMonth.SelectedValue)
        {
            OutputMonth += "-" + QEMonth.SelectedValue;
        }
        ReportDate.Text = OutputMonth + "月";
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 10; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //输出表头
        DataRow NewRow = BindTable.NewRow();
        NewRow[0] = "<center><font color=red>负&nbsp;&nbsp;&nbsp;&nbsp;债</font></center>";
        NewRow[1] = "<center><font color=red>上年同期</font></center>";
        NewRow[2] = "<center><font color=red>本期数</font></center>";
        NewRow[3] = "<center><font color=red>增减额</font></center>";
        NewRow[4] = "<center><font color=red>增减率</font></center>";
        NewRow[5] = "<center><font color=red>负债及所有者权益</font></center>";
        NewRow[6] = "<center><font color=red>上年同期</font></center>";
        NewRow[7] = "<center><font color=red>本期数</font></center>";
        NewRow[8] = "<center><font color=red>增减额</font></center>";
        NewRow[9] = "<center><font color=red>增减率</font></center>";
        BindTable.Rows.Add(NewRow);
        //输出数据行
        InsertRow(BindTable, "流动资产：", "", false, "流动负债：", "", false);
        InsertRow(BindTable, "  货币资金", "D101+102", true, "  短期借款", "S201", true);
        InsertRow(BindTable, "  短期投资", "S111", true, "  应付款项", "D202+241", true);
        InsertRow(BindTable, "  应收款项", "D112+113", true, "  应付工资", "S211", true);
        InsertRow(BindTable, "  存货", "D121+401", true, "  应付福利费", "S212", true);
        InsertRow(BindTable, "  流动资产合计", "CR02+R03+R04+R05", true, "  流动资产合计", "CR02+R03+R04+R05", true);
        InsertRow(BindTable, "农业资产：", "", false, "长期负债：", "", false);
        InsertRow(BindTable, "  农业资产", "S131", true, "  长期借款及应付款", "S221", true);
        InsertRow(BindTable, "长期资产：", "", false, "  一事一议资金", "S231", true);
        InsertRow(BindTable, "  长期投资", "S141", true, "  长期负债合计", "CR08+R09", true);
        InsertRow(BindTable, "固定资产：", "", false, "   负债合计", "CR06+R10", true);
        InsertRow(BindTable, "  固定资产原值", "S151", true, "所有者权益：", "", false);
        InsertRow(BindTable, "  减：累计折旧", "S152", true, "  资本", "S301", true);

        //InsertRow(BindTable, "  固定资产净值", "CR12-R13", true, "  公积金", "S311", true);
        //InsertRow(BindTable, "  固定资产清理", "S153", true, "  公益金", "S312", true);
        //InsertRow(BindTable, "  在建工程", "S154", true, "  未分配收益", "CT321+T322+T501+T511+T521+T522+T531+T561-F502-F541-F551", true);
        //InsertRow(BindTable, "  固定资产合计", "CR14+R15+R16", true, "  所有者权益合计", "CR13+R14+R15+R16", true);

        InsertRow(BindTable, "  固定资产净值", "CR12-R13", true, "  公积金公益金", "S311", true);
        InsertRow(BindTable, "  固定资产清理", "S153", true, "  未分配收益", "CT321+T322+T501+T511+T521+T522+T531+T561-F502-F541-F551", true);
        InsertRow(BindTable, "  在建工程", "S154", true, "  所有者权益合计", "CR13+R14+R15", true);
        InsertRow(BindTable, "  固定资产合计", "CR14+R15+R16", true, "", "", true);

        InsertRow(BindTable, "其他资产：", "", false, "", "", false);
        InsertRow(BindTable, "  无形资产", "S16102", true, "", "", false);
        InsertRow(BindTable, "  递延资产", "S16101", true, "", "", false);
        InsertRow(BindTable, "  其他长期资产", "S16103", true, "", "", false);
        InsertRow(BindTable, "  其他资产合计", "CR19+R20+R21", true, "", "", false);

        //InsertRow(BindTable, "  资产合计", "CR06+R08+R10+R17+R20", true, "负债及所有者权益合计", "CR11+R17", true);

        InsertRow(BindTable, "  资产合计", "CR06+R08+R10+R17+R20", true, "负债及所有者权益合计", "CR11+R16", true);

        //绑定表格字段
        GridView1.Columns.Clear();
        for (int i = 0; i < BindTable.Columns.Count; i++)
        {
            BoundField bf = new BoundField();
            bf.DataField = BindTable.Columns[i].ColumnName;
            bf.HtmlEncode = false;
            bf.ItemStyle.CssClass = "sbalance";
            GridView1.Columns.Add(bf);
        }
        //设置表格参数
        GridView1.DataSource = BindTable.DefaultView;
        GridView1.DataBind();
        GridView1.Columns[0].ItemStyle.Width = 100;
        GridView1.Columns[1].ItemStyle.Width = 80;
        GridView1.Columns[2].ItemStyle.Width = 80;
        GridView1.Columns[3].ItemStyle.Width = 80;
        GridView1.Columns[4].ItemStyle.Width = 60;
        GridView1.Columns[5].ItemStyle.Width = 120;
        GridView1.Columns[6].ItemStyle.Width = 80;
        GridView1.Columns[7].ItemStyle.Width = 80;
        GridView1.Columns[8].ItemStyle.Width = 80;
        GridView1.Columns[9].ItemStyle.Width = 60;
        GridView1.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[7].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[8].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        GridView1.Columns[9].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
    }

    private void InsertRow(DataTable BindTable, string ItemText0, string SubjectNo0, bool T0, string ItemText1, string SubjectNo1, bool T1)
    {
        DataRow NewRow = BindTable.NewRow();
        NewRow[0] = ItemText0.Replace(" ", "&nbsp;&nbsp;");
        NewRow[5] = ItemText1.Replace(" ", "&nbsp;&nbsp;");
        if (T0)
        {
            FillNewRow(NewRow, SubjectNo0, 0, false);
        }
        if (T1)
        {
            FillNewRow(NewRow, SubjectNo1, 5, true);
        }
        BindTable.Rows.Add(NewRow);
    }

    private void FillNewRow(DataRow NewRow, string SubjectNo, int RPos, bool isOnloan)
    {
        decimal PreData = 0;
        decimal ThisData = 0;
        decimal MarginData = 0;
        if (SubjectNo.StartsWith("S"))
        {
            PreData = GetMarginSum(SubjectNo.Substring(1), sYearMonth0, eYearMonth0, isOnloan);
            ThisData = GetMarginSum(SubjectNo.Substring(1), sYearMonth1, eYearMonth1, isOnloan);
        }
        if (SubjectNo.StartsWith("D"))
        {
            string[] SNO = SubjectNo.Substring(1).Split('+');
            PreData = GetMarginSum(SNO[0], sYearMonth0, eYearMonth0, isOnloan) + GetMarginSum(SNO[1], sYearMonth0, eYearMonth0, isOnloan);
            ThisData = GetMarginSum(SNO[0], sYearMonth1, eYearMonth1, isOnloan) + GetMarginSum(SNO[1], sYearMonth1, eYearMonth1, isOnloan);
        }
        if (SubjectNo.StartsWith("C"))
        {
            SubjectNo = SubjectNo.Substring(1);
            StringBuilder rows0 = new StringBuilder(SubjectNo);
            StringBuilder rows1 = new StringBuilder(SubjectNo);
            string[] CRow = SubjectNo.Split(new char[] { '+', '-', '*', '/' });
            for (int i = 0; i < CRow.Length; i++)
            {
                if (CRow[i].StartsWith("R"))
                {
                    int row = int.Parse(CRow[i].Substring(1));
                    if (NewRow.Table.Rows[row][1 + RPos].ToString().Length > 0)
                    {
                        rows0.Replace(CRow[i], "(" + NewRow.Table.Rows[row][1 + RPos].ToString() + ")");
                    }
                    else
                    {
                        rows0.Replace(CRow[i], "0");
                    }
                    if (NewRow.Table.Rows[row][2 + RPos].ToString().Length > 0)
                    {
                        rows1.Replace(CRow[i], "(" + NewRow.Table.Rows[row][2 + RPos].ToString() + ")");
                    }
                    else
                    {
                        rows1.Replace(CRow[i], "0");
                    }
                }
                else if (CRow[i].StartsWith("T"))
                {
                    rows0.Replace(CRow[i], "(" + GetMarginSum(CRow[i].Substring(1), sYearMonth0, eYearMonth0, true) + ")");
                    rows1.Replace(CRow[i], "(" + GetMarginSum(CRow[i].Substring(1), sYearMonth1, eYearMonth1, true) + ")");
                }
                else
                {
                    rows0.Replace(CRow[i], "(" + GetMarginSum(CRow[i].Substring(1), sYearMonth0, eYearMonth0, false) + ")");
                    rows1.Replace(CRow[i], "(" + GetMarginSum(CRow[i].Substring(1), sYearMonth1, eYearMonth1, false) + ")");
                }
            }
            PreData = decimal.Parse(ClsCalculate.JSEval(rows0.ToString().Replace(",", "")).ToString());
            ThisData = decimal.Parse(ClsCalculate.JSEval(rows1.ToString().Replace(",", "")).ToString());
        }
        MarginData = ThisData - PreData;
        NewRow[1 + RPos] = PreData == 0 ? "" : PreData.ToString("#,##0.00");
        NewRow[2 + RPos] = ThisData == 0 ? "" : ThisData.ToString("#,##0.00");
        NewRow[3 + RPos] = MarginData == 0 ? "" : MarginData.ToString("#,##0.00");
        if (MarginData > 0)
        {
            NewRow[4 + RPos] = PreData == 0 ? "100%" : (MarginData / PreData).ToString("P");
        }
        if (MarginData < 0)
        {
            NewRow[4 + RPos] = PreData == 0 ? "-100%" : (MarginData / PreData).ToString("P");
        }
    }

    private decimal GetMarginSum(string SubjectNo, string sYearMonth, string eYearMonth, bool isOnloan)
    {
        return ClsCalculate.GetMarginSum(SubjectNo, sYearMonth, eYearMonth, isOnloan);
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
