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
using System.Text.RegularExpressions;

public partial class AccountQuery_QuarterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            GridView1.Attributes.Add("onselectstart", "return false;");
            DateTime AccountDate = MainClass.GetAccountDate();
            SelYear.Attributes["onchange"] = "OnSelChange();";
            SelMonth.Attributes["onchange"] = "OnSelChange();";
            MainClass.InitAccountYear(SelYear);
            SelYear.Text = AccountDate.Year.ToString();
            string[] Quarter ={ "3", "6", "9", "12" };
            SelMonth.Text = Quarter[(AccountDate.Month + 2) / 3 - 1];
            ReportDate.Text = AccountDate.ToString("yyyy年") + SelMonth.SelectedItem.Text;
            ReportDate.Attributes["readonly"] = "readonly";
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：财务公开榜季表");
            //--
        }
    }

    protected void InitWebControl()
    {
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = "000006";
        clsCalculate.GV = GridView1;
        ////////////////////////////////////////////////
        CommClass.InitGridView("000006", "", GridView1);
        GridView1.Rows[0].Cells[2].Text = "本季数";
        GridView1.Rows[0].Cells[6].Text = "本季数";
        string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
        if (LastCarryDate.Length > 0)
        {
            LastCarryDate = LastCarryDate.Substring(0, 8);
        }
        else
        {
            LastCarryDate = DateTime.Now.ToString("yyyy年MM月");
        }
        int QStart = int.Parse(SelMonth.SelectedValue);
        int[] CellPos1 = new int[] { 2, 3, 6, 7 };
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            for (int k = 0; k < CellPos1.Length; k++)
            {
                if (GridView1.Rows[i].Cells[CellPos1[k]].Text != "&nbsp;" && GridView1.Rows[i].Cells[CellPos1[k]].Text.IndexOf("本表行列") == -1)
                {
                    decimal Sum = 0;
                    for (int m = 0; m <= 2; m++)
                    {
                        clsCalculate.ReportDate = SelYear.SelectedItem.Text + (QStart - m).ToString("00") + "月";
                        if (string.Compare(clsCalculate.ReportDate, LastCarryDate) <= 0)
                        {
                            if (CellPos1[k] == 2 || CellPos1[k] == 6)
                            {
                                Sum += decimal.Parse(clsCalculate.GetExprValue(GridView1.Rows[i].Cells[CellPos1[k]].Text).ToString());
                            }
                            else
                            {
                                Sum = decimal.Parse(clsCalculate.GetExprValue(GridView1.Rows[i].Cells[CellPos1[k]].Text).ToString());
                                break;
                            }
                        }
                    }
                    GridView1.Rows[i].Cells[CellPos1[k]].Text = Sum.ToString("#,##0.00");
                    if (GridView1.Rows[i].Cells[CellPos1[k]].Text == "0.00")
                    {
                        GridView1.Rows[i].Cells[CellPos1[k]].Text = "&nbsp;";
                    }
                }
            }
        }
        //计算汇总单元格
        clsCalculate.CalculateExpr(GridView1.Rows[GridView1.Rows.Count - 1].Cells[2]);
        clsCalculate.CalculateExpr(GridView1.Rows[GridView1.Rows.Count - 1].Cells[3]);
    }

    protected void QDataClick_Click(object sender, EventArgs e)
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
