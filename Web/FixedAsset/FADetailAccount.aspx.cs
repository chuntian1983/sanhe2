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

public partial class AccountQuery_FADetailAccount : System.Web.UI.Page
{
    private int RunLevel = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013$100022")) { return; }
        if (!IsPostBack)
        {
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            InitSubject("固定资产", SysConfigs.FixedAssetSubject);
            GetCommDetailInfo();
            //写入操作日志
            CommClass.WriteCTL_Log("100014", "资产明细账查询");
            //--
        }
    }

    protected void InitSubject(string CName, string ParentID)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = CommClass.GetDataSet("select id,cname from cw_assetclass where pid='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == SysConfigs.FixedAssetSubject) { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitSubject(row["cname"].ToString(), row["id"].ToString());
        }
        RunLevel--;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }

    protected void GetCommDetailInfo()
    {
        AssetNo.Value = QList.SelectedValue;
        AssetName.Value = QList.SelectedItem.Text.Replace(".", "");
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 11; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        //创建数据行
        DataRow NewRow;
        DataSet ds = new DataSet();
        string sYearMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月";
        string eYearMonth = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月";
        if (QSMonth.SelectedValue == QEMonth.SelectedValue)
        {
            QDateTime.Value = sYearMonth;
        }
        else
        {
            QDateTime.Value = string.Format("{0}年{1}月-{2}月", QYear.SelectedValue, QSMonth.SelectedValue, QEMonth.SelectedValue);
        }
        //上期结转
        decimal LastOldPrice = 0;
        decimal LastZheJiu = 0;
        decimal LastJingZhi = 0;
        NewRow = BindTable.NewRow();
        NewRow[3] = "上期结转";
        string PreYearMonth = Convert.ToDateTime(sYearMonth + "01日").AddMonths(-1).ToString("yyyy年MM月");
        ds = CommClass.GetDataSet("select OldPrice1,ZheJiu1 from cw_assetda where voucherdate like '" + PreYearMonth
            + "%' and classid like '" + QList.SelectedValue + "%' order by cardid");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            LastOldPrice += decimal.Parse(row["OldPrice1"].ToString());
            LastZheJiu += decimal.Parse(row["ZheJiu1"].ToString());
        }
        LastJingZhi += LastOldPrice - LastZheJiu;
        NewRow[6] = LastOldPrice.ToString("#,##0.00");
        NewRow[9] = LastZheJiu.ToString("#,##0.00");
        NewRow[10] = LastJingZhi.ToString("#,##0.00");
        BindTable.Rows.Add(NewRow);
        //明细汇总
        decimal ThisOldPrice = 0;
        decimal ThisOldPriceLead = 0;
        decimal ThisOldPriceOnloan = 0;
        decimal ThisZheJiu = 0;
        decimal ThisZheJiuLead = 0;
        decimal ThisZheJiuOnloan = 0;
        decimal ThisJingZhi = 0;
        ds = CommClass.GetDataSet("select id,VoucherID,VoucherDate,VSummary,OldPrice0,OldPrice1,ZheJiu0,ZheJiu1,ThisZheJiu from cw_assetda where voucherdate between '"
            + sYearMonth + "01日' and '" + eYearMonth + "31日' and classid like '" + QList.SelectedValue + "%' order by VoucherDate,VoucherID");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string CurrentMonth = ds.Tables[0].Rows[0]["VoucherDate"].ToString().Substring(5, 2);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (CurrentMonth != row["VoucherDate"].ToString().Substring(5, 2))
                {
                    //本期合计
                    NewRow = BindTable.NewRow();
                    NewRow[3] = "本期合计";
                    NewRow[4] = ThisOldPriceLead.ToString("#,##0.00");
                    NewRow[5] = ThisOldPriceOnloan.ToString("#,##0.00");
                    NewRow[6] = ThisOldPrice.ToString("#,##0.00");
                    NewRow[7] = ThisZheJiuLead.ToString("#,##0.00");
                    NewRow[8] = ThisZheJiuOnloan.ToString("#,##0.00");
                    NewRow[9] = ThisZheJiu.ToString("#,##0.00");
                    NewRow[10] = ThisJingZhi.ToString("#,##0.00");
                    BindTable.Rows.Add(NewRow);
                    ThisOldPrice = 0;
                    ThisOldPriceLead = 0;
                    ThisOldPriceOnloan = 0;
                    ThisZheJiu = 0;
                    ThisZheJiuLead = 0;
                    ThisZheJiuOnloan = 0;
                    ThisJingZhi = 0;
                    CurrentMonth = row["VoucherDate"].ToString().Substring(5, 2);
                }
                NewRow = BindTable.NewRow();
                NewRow[0] = row["VoucherDate"].ToString();
                string VoucherID = row["VoucherID"].ToString();
                if (VoucherID == "0000000000" || string.IsNullOrEmpty(VoucherID))
                {
                    NewRow[1] = "--------------";
                    NewRow[2] = "--------";
                }
                else
                {
                    NewRow[1] = VoucherID;
                    NewRow[2] = CommClass.GetFieldFromID(VoucherID, "VoucherNo", "cw_voucher");
                }
                NewRow[3] = row["VSummary"].ToString();
                decimal MyThisZheJiu = decimal.Parse(row["ThisZheJiu"].ToString());
                decimal MyOldPrice = decimal.Parse(row["OldPrice1"].ToString());
                decimal MyZheJiu = decimal.Parse(row["ZheJiu1"].ToString());
                decimal MyJingZhi = MyOldPrice - MyZheJiu;
                ThisOldPrice += MyOldPrice;
                ThisZheJiu += MyZheJiu;
                ThisJingZhi += MyJingZhi;
                decimal MyOldPrice0 = decimal.Parse(row["OldPrice0"].ToString());
                if (MyOldPrice0 != 0)
                {
                    ThisOldPrice += MyOldPrice0;
                    if (MyOldPrice0 > 0)
                    {
                        NewRow[4] = MyOldPrice0.ToString("#,##0.00");
                        ThisOldPriceLead += MyOldPrice0;
                    }
                    else
                    {
                        MyOldPrice0 = Math.Abs(MyOldPrice0);
                        NewRow[5] = MyOldPrice0.ToString("#,##0.00");
                        ThisOldPriceOnloan += MyOldPrice0;
                    }
                }
                NewRow[6] = MyOldPrice.ToString("#,##0.00");
                decimal MyZheJiu0 = decimal.Parse(row["ZheJiu0"].ToString());
                if (MyZheJiu0 != 0)
                {
                    if (MyZheJiu0 > 0)
                    {
                        MyThisZheJiu += MyZheJiu0;
                        ThisZheJiu += MyZheJiu0;
                    }
                    else
                    {
                        MyZheJiu0 = Math.Abs(MyZheJiu0);
                        NewRow[7] = MyZheJiu0.ToString("#,##0.00");
                        ThisZheJiuLead += MyZheJiu0;
                        ThisZheJiu -= MyZheJiu0;
                    }
                }
                ThisZheJiuOnloan += MyThisZheJiu;
                NewRow[8] = MyThisZheJiu.ToString("#,##0.00");
                NewRow[9] = MyZheJiu.ToString("#,##0.00");
                NewRow[10] = MyJingZhi.ToString("#,##0.00");
                BindTable.Rows.Add(NewRow);
            }
        }
        //本期合计
        NewRow = BindTable.NewRow();
        NewRow[3] = "本期合计";
        NewRow[4] = ThisOldPriceLead.ToString("#,##0.00");
        NewRow[5] = ThisOldPriceOnloan.ToString("#,##0.00");
        NewRow[6] = ThisOldPrice.ToString("#,##0.00");
        NewRow[7] = ThisZheJiuLead.ToString("#,##0.00");
        NewRow[8] = ThisZheJiuOnloan.ToString("#,##0.00");
        NewRow[9] = ThisZheJiu.ToString("#,##0.00");
        NewRow[10] = ThisJingZhi.ToString("#,##0.00");
        BindTable.Rows.Add(NewRow);
        BindTable.AcceptChanges();
        //输出数据
        ShowReportPage(BindTable, "000014");
    }
    private void ShowReportPage(DataTable BindTable, string DesignID)
    {
        //输出报表分页
        ReportPage reportPage = new ReportPage();
        reportPage.ReportTypeID = "100007";
        reportPage.DesignID = DesignID;
        reportPage.ReportTitle = "固 定 资 产 明 细 账";
        reportPage.ReportDate = QYear.SelectedValue;
        reportPage.BindTable = BindTable;
        reportPage.ShowPageContent = ShowPageContent;
        reportPage.Parameters = new string[] { AssetNo.Value, AssetName.Value, QDateTime.Value };
        if (ViewState["IsToExcel"] == (object)"1")
        {
            reportPage.IsToExcel = "1";
            ViewState["IsToExcel"] = "0";
        }
        reportPage.ShowReportPage();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        ViewState["IsToExcel"] = "1";
        GetCommDetailInfo();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
