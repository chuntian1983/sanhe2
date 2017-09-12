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

public partial class AccountQuery_InternalDemand : System.Web.UI.Page
{
    private int RunLevel = 0;
    private ClsCalculate clsCalculate = new ClsCalculate();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100010")) { return; }
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
            InitSubject("内部往来", SysConfigs.InternalDemand);
            CreateGridView();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：内部往来");
            //--
        }
    }
    protected void InitSubject(string SubjectName, string ParentNo)
    {
        SubjectList.Items.Add(new ListItem(new string('.', RunLevel * 6) + SubjectName, ParentNo));
        DataSet ds = CommClass.GetDataSet("select subjectno,subjectname from cw_subject where parentno='" + ParentNo + "' order by subjectno asc");
        RunLevel++;
        if (ParentNo == SysConfigs.InternalDemand) { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitSubject(row["subjectname"].ToString(), row["subjectno"].ToString());
        }
        RunLevel--;
    }
    protected void CreateGridView()
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 3; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        for (int i = 3; i < 8; i++)
        {
            BindTable.Columns.Add("F" + i.ToString(), typeof(double));
        }
        if (SubjectName.Text.Length == 0)
        {
            string _SelectedValue = SubjectList.SelectedValue == "" ? SysConfigs.InternalDemand : SubjectList.SelectedValue;
            for (int i = 0; i < SubjectList.Items.Count; i++)
            {
                if (SubjectList.Items[i].Value.StartsWith(_SelectedValue))
                {
                    BindData(BindTable, SubjectList.Items[i].Value);
                }
            }
        }
        else
        {
            for (int i = 0; i < SubjectList.Items.Count; i++)
            {
                if (SubjectList.Items[i].Text.IndexOf(SubjectName.Text) != -1)
                {
                    BindData(BindTable, SubjectList.Items[i].Value);
                }
            }
        }
        CommClass.CreateGridView("000007", BindTable, GridView1, string.Format("{0} {1}", DataSortName.Value, DataSortType.Value));
        SetSortType(0, 0, "F0");
        SetSortType(0, 1, "F1");
        SetSortType(0, 2, "F2");
        SetSortType(1, 4, "F4");
        SetSortType(1, 5, "F5");
        SetSortType(1, 6, "F6");
        SetSortType(1, 7, "F7");
    }
    private void SetSortType(int rowIndex, int columnIndex, string sortName)
    {
        TableCell cell = GridView1.Rows[rowIndex].Cells[columnIndex];
        if (DataSortName.Value == sortName)
        {
            cell.Text += DataSortType.Value == "asc" ? "↑" : "↓";
        }
        cell.Attributes["style"] += ";cursor:hand";
        cell.Attributes["onclick"] = string.Format("SetSortType('{0}');", sortName);
    }
    protected void BindData(DataTable BindTable, string SubjectNo)
    {
        decimal BeginBalance = 0;
        decimal YearLead = 0;
        decimal YearOnloan = 0;
        decimal FinalBalance = 0;
        DataRow NewRow = BindTable.NewRow();
        string YearMonth = SelYear.SelectedValue + "年" + SelMonth.SelectedValue + "月";
        BeginBalance = clsCalculate.GetSubjectSumFromDataTable(SubjectNo, "beginbalance", SelYear.SelectedValue + "年01月");
        YearLead = clsCalculate.GetSubjectSumFromDataTable(SubjectNo, "leadsum", YearMonth);
        YearOnloan = clsCalculate.GetSubjectSumFromDataTable(SubjectNo, "onloansum", YearMonth);
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
        NewRow[0] = SubjectNo;
        NewRow[1] = CommClass.GetFieldFromNo(SubjectNo, "subjectname");
        //NewRow[3] = BeginBalance.ToString("#,##0.00").Replace("-", "");
        NewRow[3] = BeginBalance;
        NewRow[4] = YearLead.ToString("#,##0.00").Replace("-", "");
        NewRow[5] = YearOnloan.ToString("#,##0.00").Replace("-", "");
        FinalBalance = BeginBalance + YearLead - YearOnloan;
        if (FinalBalance > 0)
        {
            NewRow[6] = FinalBalance.ToString("#,##0.00");
            NewRow[7] = "0.00";
        }
        else
        {
            NewRow[6] = "0.00";
            NewRow[7] = FinalBalance.ToString("#,##0.00").Replace("-", "");
        }
        BindTable.Rows.Add(NewRow);
    }
    protected void QButton_Click(object sender, EventArgs e)
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
