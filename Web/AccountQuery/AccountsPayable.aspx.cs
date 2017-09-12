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

public partial class AccountQuery_AccountsPayable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000008$100003")) { return; }
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            DateTime AccountDate = MainClass.GetAccountDate();
            ReportDate.Text = AccountDate.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SelYear.Attributes["onchange"] = "OnSelChange(this.value,0);";
            SelMonth.Attributes["onchange"] = "OnSelChange(this.value,1);";
            MainClass.InitAccountYear(SelYear);
            SelYear.Text = AccountDate.Year.ToString();
            SelMonth.Text = AccountDate.Month.ToString("00");
            CreateGridView();
            //写入操作日志
            CommClass.WriteCTL_Log("100014", "账簿查询：应付账款");
            //--
        }
    }
    protected void CreateGridView()
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 4; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        DataRowCollection AllRows = CommClass.GetDataRows("select subjectno,subjectname,finalbalance from cw_viewsubjectsum where subjectno like '"
            + SysConfigs.AccPayable + "%' and yearmonth='" + SelYear.SelectedValue + "年" + SelMonth.SelectedValue + "月' order by subjectno asc");
        if (AllRows == null)
        {
            AllRows = CommClass.GetDataRows("select subjectno,subjectname,beginbalance as finalbalance from cw_subject where subjectno like '"
                + SysConfigs.AccPayable + "%' order by subjectno asc");
        }
        if (AllRows != null)
        {
            DataRow NewRow;
            decimal FinalBalance = 0;
            for (int i = 0; i < AllRows.Count; i++)
            {
                NewRow = BindTable.NewRow();
                FinalBalance = decimal.Parse(AllRows[i]["finalbalance"].ToString());
                if (FinalBalance > 0)
                {
                    NewRow[2] = "借";
                }
                else if (FinalBalance < 0)
                {
                    NewRow[2] = "贷";
                }
                else
                {
                    NewRow[2] = "平";
                }
                FinalBalance = Math.Abs(FinalBalance);
                NewRow[0] = AllRows[i]["subjectno"].ToString();
                NewRow[1] = AllRows[i]["subjectname"].ToString();
                NewRow[3] = FinalBalance.ToString("#,##0.00");
                BindTable.Rows.Add(NewRow);
            }
            BindTable.AcceptChanges();
        }
        CommClass.CreateGridView("000008", BindTable, GridView1);
        GridView1.Rows[0].Cells[2].Text = "应付款余额";
    }
    protected void QDataClick_Click(object sender, EventArgs e)
    {
        CreateGridView();
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
